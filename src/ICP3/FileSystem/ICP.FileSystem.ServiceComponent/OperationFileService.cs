using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using ICP.FileSystem.ServiceInterface;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
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
        public DocumentStream ServiceTransferFileToClint(DocumentStream FileInfo)
        {
            DocumentStream ReturnDocumentStream = new DocumentStream();
            try
            {
                ReturnDocumentStream = InnerGetContent(new[] { FileInfo.Id }, FileInfo.IsDownCopy, FileInfo.DataSearchTypeCode).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ReturnDocumentStream;
        }

        private List<DocumentStream> InnerGetContent(Guid[] ids, bool isCopy, DataSearchType dataSearchType)
        {
            List<DocumentStream> contents;
            try
            {
                DataSet dsResult = null;
                DataSet dds = null;
                DataSet sds = null;
                WriteLog(string.Format("DataSearchType:{0}\r\n", EnumHelper.GetDescription(dataSearchType, false)));
                dds = InnerGetContentDataSet(ids, isCopy, true);
                if (dataSearchType != DataSearchType.Local && dds == null)
                {
                    sds = InnerGetContentDataSet(ids, isCopy, false);
                }
                dsResult = DataSetHelper.MergeSet(dds, sds);
                if (dsResult == null || dsResult.Tables.Count < 1)
                {
                    return new List<DocumentStream>();
                }

                #region Comment Code
                //Database db = DatabaseFactory.CreateDatabase();
                //DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationFileInfo");
                //db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
                //db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                //db.AddInParameter(dbCommand, "@IsCopy", DbType.Boolean, isCopy);
                //DataSet set = db.ExecuteDataSet(dbCommand);
                //if (set == null || set.Tables.Count < 1)
                //return new List<DocumentStream>(); 
                #endregion
                contents = (from c in dsResult.Tables[0].AsEnumerable()
                            select new DocumentStream
                            {
                                Content = new MemoryStream(c.Field<byte[]>("Body")),
                                Id = c.Field<Guid>("Id"),
                                Name = c.Field<String>("Name"),
                                DataSearchTypeCode = dataSearchType,
                                OperationID = c.Field<Guid>("OperationID"),   //业务操作ID
                                TypeCode = (DocumentType?)c.Field<byte>("DocumentType")    //文档类型：单证类型
                            }).ToList();
            }
            catch (Exception ex)
            {
                WriteLog("InnerGetContent\r\n" + ex.Message);
                throw ex;
            }
            return contents;
        }

        private DataSet InnerGetContentDataSet(Guid[] ids, bool isCopy, bool isDefaultDB)
        {
            DataSet set = null;
            try
            {
                //WriteLog("Begin Download");
                Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationFileInfo");
                db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.AddInParameter(dbCommand, "@IsCopy", DbType.Boolean, isCopy);
                set = db.ExecuteDataSet(dbCommand);
                //WriteLog("Over\r\n");
                if (set == null || set.Tables.Count < 1)
                    return null;
            }
            catch (Exception ex)
            {
                WriteLog("Method:InnerGetContentDataSet\r\n获取文档出现异常\r\n" + ex.Message);
                set = null;
            }
            return set;
        }

        /// <summary>
        /// 客户端通知服务端从临时目录读取文件存入FCMDoc
        /// </summary>
        /// <param name="documentInfo">文档实体</param>
        public void UploadOperationFileByInfo(DocumentInfo documentInfo)
        {
            string FileNameSourcePath = string.Empty;
            string FileNameCopyPath = string.Empty;
            string FileNameMergePath = string.Empty;
            string ExceptionString = string.Empty;
            ExceptionString = documentInfo.CreateByName;
            try
            {
                //1.从临时目录读取文件,以byte[]
                string savaPath = ICPPathUtility.TempPathCacheFile();
                string fileName = documentInfo.Id.ToString() + Path.GetExtension(documentInfo.Name);
                if (!savaPath.EndsWith("\\")) savaPath += "\\";
                string[] FileName = new string[2];
                FileName[0] = savaPath + documentInfo.Name;
                FileNameSourcePath = FileName[0];
                FileName[1] = savaPath + "copy" + Path.GetFileNameWithoutExtension(FileName[0]) + ".pdf";
                FileNameCopyPath = FileName[1];
                savaPath += fileName;
                FileNameMergePath = savaPath;
                //拆分文件流
                ExceptionString += "从临时目录读取文件,拆分文件流\r\n";
                ExceptionString += "文件名" + savaPath + "\r\n";
                Stream GetFileSream = new FileStream(savaPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                int[] dividesize = new int[2];
                dividesize[0] = FirstFileStreamSizeDictionary[documentInfo.Id];
                dividesize[1] = (int)(GetFileSream.Length - dividesize[0]);
                BinaryReader SplitFileReader = new BinaryReader(GetFileSream);
                byte[] TempBytes;
                string rfile = Path.GetFileNameWithoutExtension(savaPath);
                //分为两份
                for (int i = 0; i < 2; i++)
                {
                    //根据文件名称和文件打开模式来初始化FileStream文件流实例
                    FileStream TempStream = new FileStream(
                        FileName[i], FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    //以FileStream实例来创建、初始化BinaryWriter书写器实例 
                    BinaryWriter TempWriter = new BinaryWriter(TempStream);
                    //从大文件中读取指定大小数据
                    TempBytes = SplitFileReader.ReadBytes(dividesize[i]);
                    //把此数据写入小文件
                    TempWriter.Write(TempBytes);
                    //关闭书写器，形成小文件
                    TempWriter.Close();
                    //关闭文件流
                    TempStream.Close();
                }
                GetFileSream.Close();

                ExceptionString += "写入数据库\r\n";
                ManyResult results = null;

                Guid[] ids = { documentInfo.Id };
                Guid[] operationIds = { documentInfo.OperationID };
                int[] formTypes = { documentInfo.FormType.GetHashCode() };
                int[] documentTypes = { documentInfo.DocumentType.GetHashCode() };
                int[] fileSources = { documentInfo.FileSources.GetHashCode() };
                String[] names = { documentInfo.Name };
                Guid?[] formid = { documentInfo.FormId };

                DataTable dtBody = FileSystemUtility.CreateAttachmentTable("Bodies");
                DataTable dtCopy = FileSystemUtility.CreateAttachmentTable("Copies");

                //流转换为字节
                Stream ContentStream = new FileStream(FileName[0], FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] Content = ArrayHelper.ReadAllBytesFromStream(ContentStream);
                ContentStream.Close();
                Stream ContentcopyStream = new FileStream(FileName[1], FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] Contentcopy = ArrayHelper.ReadAllBytesFromStream(ContentcopyStream);
                ContentcopyStream.Close();

                dtBody.Rows.Add(documentInfo.OperationID, documentInfo.Name, Content);
                dtCopy.Rows.Add(documentInfo.OperationID, documentInfo.Name, Contentcopy);

                String[] remarks = { documentInfo.Remark };

                DateTime?[] updateDates = { documentInfo.UpdateDate };

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOperationFileInfo");
                db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.ToArray().Join());

                db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, operationIds.Join());
                db.AddInParameter(dbCommand, "@FormTypes", DbType.String, formTypes.Join());
                db.AddInParameter(dbCommand, "@FormIds", DbType.String, formid.Join());
                db.AddInParameter(dbCommand, "@DocumentTypes", DbType.String, documentTypes.Join());
                db.AddInParameter(dbCommand, "@Names", DbType.String, names.Join());
                SqlParameter parameterBodies = new SqlParameter("@Bodies", dtBody);
                parameterBodies.Direction = ParameterDirection.Input;
                parameterBodies.SqlDbType = SqlDbType.Structured;
                parameterBodies.TypeName = "oa.uttFiles";
                dbCommand.Parameters.Add(parameterBodies);

                SqlParameter parameterCopies = new SqlParameter("@CopyBodies", dtCopy);
                parameterCopies.Direction = ParameterDirection.Input;
                parameterCopies.SqlDbType = SqlDbType.Structured;
                parameterCopies.TypeName = "oa.uttFiles";
                dbCommand.Parameters.Add(parameterCopies);

                db.AddInParameter(dbCommand, "@Remarks", DbType.String, remarks.Join());
                db.AddInParameter(dbCommand, "@FileSources", DbType.String, fileSources.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, false);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, documentInfo.UpdateBy);

                results = db.ManyResult(dbCommand, new[] { "ID", "UpdateDate" });
                ExceptionString += "写入数据库完毕\r\n";
                //删除缓存文件
                if (File.Exists(FileNameSourcePath))
                {
                    File.Delete(FileNameSourcePath);
                }
                if (File.Exists(FileNameCopyPath))
                {
                    File.Delete(FileNameCopyPath);
                }
                if (File.Exists(FileNameMergePath))
                {
                    File.Delete(FileNameMergePath);
                }
            }
            catch (Exception ex)
            {
                ExceptionString += ex.Message;
                WriteLog("UploadOperationFileByInfo\r\n" + ExceptionString);
                if (File.Exists(FileNameSourcePath))
                {
                    File.Delete(FileNameSourcePath);
                }
                if (File.Exists(FileNameCopyPath))
                {
                    File.Delete(FileNameCopyPath);
                }
                if (File.Exists(FileNameMergePath))
                {
                    File.Delete(FileNameMergePath);
                }
                throw new Exception("UploadOperationFileByInfo\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 验证当前业务下是否包含传入的文件名集合
        /// </summary>
        /// <param name="fileNames"></param>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public List<DocumentInfo> IsExistFileNames(List<string> fileNames, Guid operationId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationFileListForNames");
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationId);
                db.AddInParameter(dbCommand, "@Names", DbType.String, fileNames.ToArray().Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                DataSet set = db.ExecuteDataSet(dbCommand);
                if (set == null || set.Tables.Count < 1)
                    return new List<DocumentInfo>();
                List<DocumentInfo> documents = (from document in set.Tables[0].AsEnumerable()
                                                select new DocumentInfo
                                                {
                                                    Id = document.Field<Guid>("Id"),
                                                    Name = document.Field<String>("Name"),
                                                    DocumentType = DocumentType.Other,
                                                    UpdateDate = document.Field<DateTime?>("UpdateDate")
                                                }).ToList();

                return documents;
            }
            catch (Exception ex)
            {
                WriteLog("IsExistFileNames\r\n" + ex.Message);
                throw ex;
            }

        }

        /// <summary>
        /// 覆盖保存前删除数据库数据
        /// </summary>
        /// <param name="newDocuments"></param>
        /// <param name="listDeleteIds"></param>
        /// <param name="updateDates"></param>
        /// <param name="UserId"></param>
        public void DeleteFileBeforeSave(List<Guid> listDeleteIds, List<DateTime?> updateDates, Guid UserId)
        {
            try
            {
                Delete(listDeleteIds, updateDates, UserId);
            }
            catch (Exception ex)
            {
                WriteLog("DeleteFileBeforeSave\r\n" + ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="updateDates"></param>
        /// <param name="UserId"></param>
        public void Delete(List<Guid> ids, List<DateTime?> updateDates, Guid UserId)
        {
            if (ids == null || ids.Count <= 0)
                return;
            ApplicationContext context = ApplicationContext.Current;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOperationFileInfo");
            db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.ToArray().Join());
            db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.ToArray().Join());
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, UserId);

            ManyResult result = db.ManyResult(dbCommand, new string[] { "OperationID", "OperationType" });
        }
    }
}
