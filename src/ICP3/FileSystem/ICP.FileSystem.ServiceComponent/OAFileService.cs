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
        /// 保存文档到OADoc
        /// </summary>
        /// <param name="document"></param>
        public void SaveDocumentToOADoc(DocumentInfo document)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                DataTable dtBody = FileSystemUtility.CreateAttachmentTable("Bodies");
                byte[] data = InfoStreamConvertByte(document);
                dtBody.Rows.Add(document.Id, document.Name, data);

                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommandWithTimeout("oa.SaveOADocumentFiles");

                SqlParameter parameterBodies = new SqlParameter("@Bodies", dtBody);
                parameterBodies.Direction = ParameterDirection.Input;
                parameterBodies.SqlDbType = SqlDbType.Structured;
                parameterBodies.TypeName = "oa.uttFiles";
                dbCommand.Parameters.Add(parameterBodies);
            }
            catch (Exception ex)
            {
                throw;
            }

            try
            {
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取OA文档
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public DocumentStream GetOADocumentContent(DocumentStream info)
        {
            return InnerOAGetContent(new Guid[] { info.Id }, false).FirstOrDefault();
        }

        private IEnumerable<DocumentStream> InnerOAGetContent(Guid[] ids, bool isCopy)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspGetOADocumentInfo");
            db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            db.AddInParameter(dbCommand, "@IsCopy", DbType.Boolean, isCopy);
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1)
            {
                throw (new Exception("无此文档" + ids[0].ToString()));
            }
            List<DocumentStream> contents = (from c in set.Tables[0].AsEnumerable()
                                             select new DocumentStream
                                             {
                                                 Content = new MemoryStream(c.Field<byte[]>("Body")),
                                                 Id = c.Field<Guid>("Id"),
                                                 Name = c.Field<String>("Name")
                                             }).ToList();
            return contents;
        }
    }
}
