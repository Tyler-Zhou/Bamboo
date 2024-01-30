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
        /// 保存邮件到MessageDoc
        /// </summary>
        /// <param name="document"></param>
        public void SaveMailItemToMessageDoc(DocumentInfo document)
        {
            if (document == null)
                return;
            Stopwatch swTotaltime = new Stopwatch();
            swTotaltime.Start();

            ApplicationContext context = ApplicationContext.Current;
            DataTable dtBody = FileSystemUtility.CreateAttachmentTable("Bodies");

            dtBody.Rows.Add(document.Id, document.Name, InfoStreamConvertByte(document));

            ManyResult results = null;
            //bool isSaveFailed = false;
            string errorMessage = string.Empty;
            //List<Guid> documentIds = new List<Guid>();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.SaveMailFiles");

            SqlParameter parameterBodies = new SqlParameter("@Bodies", dtBody);
            parameterBodies.Direction = ParameterDirection.Input;
            parameterBodies.SqlDbType = SqlDbType.Structured;
            parameterBodies.TypeName = "oa.uttFiles";
            dbCommand.Parameters.Add(parameterBodies);

            try
            {
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                //isSaveFailed = true;
                errorMessage = string.Format("SaveMailItem Failed DocumentID:[{0}] [Total:{1}ms]  \r\n{2}", document.Id, swTotaltime.ElapsedMilliseconds, ex.Message);
                //documentIds.Add(document.Id);
                Framework.CommonLibrary.LogHelper.SaveLog(errorMessage);
                throw new Exception(errorMessage);
            }
        }
    }
}
