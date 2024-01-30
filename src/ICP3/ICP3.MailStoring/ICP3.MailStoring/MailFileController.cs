using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ICP3.MailStoring
{
    /// <summary>
    /// 邮件文件控制器
    /// </summary>
    public class MailFileController
    {
        /// <summary>
        /// 获取邮件文件的MessageID
        /// </summary>
        public string GetMsgidFromMailFile(string file)
        {
            string mailBody;
            mailBody = File.ReadAllText(file);
            string msgID;
            msgID = Regex.Match(mailBody, "Message-ID: .*", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture).Value.ToLower().Replace("message-id: ", "").Trim();

            if (msgID == "")
            {
                //针对换行定义的Message-ID
                //Environment.NewLine
                int startMsgIDIndex = mailBody.IndexOf("Message-ID:", StringComparison.OrdinalIgnoreCase);
                int endMsgIDIndex = mailBody.IndexOf(Environment.NewLine, startMsgIDIndex + 15);
                msgID = mailBody.Substring(startMsgIDIndex, endMsgIDIndex - startMsgIDIndex).ToLower().Replace("message-id:", "").Replace(Environment.NewLine, "").Trim();
            }

            if (msgID == "")
                throw new Exception("异常，获取的MSGID为空，文件：" + file);
            else
                return msgID;
        }

        /// <summary>
        /// 将邮件文件存储到邮件数据库
        /// 应该支持批量的上传，最好一次上传10个文件。
        /// </summary>
        public void SaveMailfileToDB(List<MailRecModule> unsavedMailRecs)
        {
            DataTable dtBody = CreateAttachmentTable("Bodies");

            for (int i=unsavedMailRecs.Count-1; i>=0; i--)
            {
                dtBody.Rows.Add(
                                unsavedMailRecs[i].IMessageID,
                                unsavedMailRecs[i].IMessageID + ".imap", 
                                File.ReadAllBytes(unsavedMailRecs[i].FilePath));
            }
            SaveMailfileDBCommand.Parameters["@Bodies"].Value = dtBody;
            
            global::System.Data.ConnectionState previousConnectionState = SaveMailfileDBCommand.Connection.State;
            if (((SaveMailfileDBCommand.Connection.State & global::System.Data.ConnectionState.Open)
                    != global::System.Data.ConnectionState.Open))
            {
                SaveMailfileDBCommand.Connection.Open();

            }
            int returnValue;
            try
            {
                returnValue = SaveMailfileDBCommand.ExecuteNonQuery();
            }
            finally
            {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed))
                {
                    SaveMailfileDBCommand.Connection.Close();
                }
            }
        }

        DbCommand _SaveMailfileDBCommand;
        /// <summary>
        /// 获取执行oa.SaveMailFiles的命令实例。
        /// </summary>
        DbCommand SaveMailfileDBCommand
        {
            get
            {
                if (_SaveMailfileDBCommand == null)
                {
                    _SaveMailfileDBCommand = new global::System.Data.SqlClient.SqlCommand();
                    _SaveMailfileDBCommand.Connection = new global::System.Data.SqlClient.SqlConnection(global::ICP3.MailStoring.Properties.Settings.Default.ICP3ConnectionString);
                    _SaveMailfileDBCommand.CommandText = "oa.SaveMailFiles";
                    _SaveMailfileDBCommand.CommandTimeout = 30000;
                    _SaveMailfileDBCommand.CommandType = global::System.Data.CommandType.StoredProcedure;

                    SqlParameter parameterBodies = new SqlParameter("@Bodies", SqlDbType.Structured);
                    parameterBodies.Direction = ParameterDirection.Input;
                    parameterBodies.TypeName = "oa.uttFiles";
                    _SaveMailfileDBCommand.Parameters.Add(parameterBodies);
                }

                return _SaveMailfileDBCommand;
            }
        }

        DataTable CreateAttachmentTable(string tableName)
        {
            DataTable dt = new DataTable(tableName);
            dt.Columns.Add("Id", typeof(Guid));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Content", typeof(Byte[]));

            return dt;
        }
    }
}