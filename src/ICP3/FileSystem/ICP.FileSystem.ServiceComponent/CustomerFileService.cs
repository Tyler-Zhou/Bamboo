using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using ICP.FileSystem.ServiceInterface;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ICP.FileSystem.ServiceComponent
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FileSystemService
    {
        /// <summary>
        /// 服务端发送文件到客户端
        /// </summary>
        /// <param name="FileInfo"></param>
        /// <returns></returns>
        public DocumentStream GetCustomerDocumentContent(DocumentStream FileInfo)
        {
            try
            {
                DocumentStream contents = new DocumentStream();
                try
                {
                    DataSet dsResult = null;
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetCustomerDocumnetContent");
                    db.AddInParameter(dbCommand, "@IDs", DbType.Guid, FileInfo.Id);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                    dsResult = db.ExecuteDataSet(dbCommand);
                    if (dsResult == null || dsResult.Tables.Count < 1)
                    {
                        return new DocumentStream();
                    }
                    contents = (from c in dsResult.Tables[0].AsEnumerable()
                                select new DocumentStream
                                {
                                    Content = new MemoryStream(c.Field<byte[]>("Body")),
                                    Id = c.Field<Guid>("Id"),
                                    Name = c.Field<String>("Name"),
                                }).FirstOrDefault();
                return contents;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存文档到Customer Doc
        /// </summary>
        /// <param name="document"></param>
        public void SaveDocumentToCustomerDoc(DocumentInfo document)
        {
            
            try
            {
                DataTable dtBody = FileSystemUtility.CreateAttachmentTable("Bodies");
                byte[] data = InfoStreamConvertByte(document);
                dtBody.Rows.Add(document.Id, document.Name, data);
                SqlParameter parameterBodies = new SqlParameter("@Bodies", dtBody);
                parameterBodies.Direction = ParameterDirection.Input;
                parameterBodies.SqlDbType = SqlDbType.Structured;
                parameterBodies.TypeName = "oa.uttFiles";

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.SaveCustomerDocumentFiles");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, document.Id);
                db.AddInParameter(dbCommand, "@Name", DbType.String, document.Name);
                db.AddInParameter(dbCommand, "@DocumentType", DbType.Int16, document.DocumentType);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, document.OperationID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, document.CustomerID);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, document.Remark);
                dbCommand.Parameters.Add(parameterBodies);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.String, document.UpdateDate);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, document.CreateBy);
                db.ExecuteNonQuery(dbCommand);
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取客户文档
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<DocumentInfo> GetCustomerDocumentList(BusinessOperationContext context)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCustomerFileList");
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, context.CompanyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, context.CustomerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                DataSet set = db.ExecuteDataSet(dbCommand);
                if (set == null || set.Tables.Count < 1)
                    return new List<DocumentInfo>();

                List<DocumentInfo> documents = (from document in set.Tables[0].AsEnumerable()
                                                select new DocumentInfo
                                                {
                                                    Id = document.Field<Guid>("Id"),
                                                    Name = document.Field<String>("Name"),
                                                    DocumentType = (DocumentType)document.Field<byte>("DocumentType"),
                                                    CreateByName = document.Field<String>("CreateByName"),
                                                    CreateDate = document.Field<DateTime>("CreateDate"),
                                                    State = UploadState.Successed,
                                                }).ToList();
                return documents;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="updateDates"></param>
        /// <param name="UserId"></param>
        public void DeleteCustomerDocumentList(List<Guid> ids, List<DateTime?> updateDates, Guid UserId)
        {
            try
            {
                if (ids == null || ids.Count <= 0)
                    return;
                ApplicationContext context = ApplicationContext.Current;
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveCustomerFileInfo");
                db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.ToArray().Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.ToArray().Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, UserId);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "OperationID", "OperationType" });
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
