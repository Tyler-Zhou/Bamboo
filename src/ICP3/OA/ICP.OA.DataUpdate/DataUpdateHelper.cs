using System;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Data;
namespace ICP.OA.DataUpdate
{  
    /// <summary>
    /// 邮件传真数据升级辅助类
    /// </summary>
   public class DataUpdateHelper
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
        string fileRootPath = System.Configuration.ConfigurationSettings.AppSettings["fileRootPath"];
       public void Update()
       {
           Console.WriteLine("开始升级。。。");
           try
           {
               Console.WriteLine("开始读取文件数据。。。");
               string[] bodyFiles = Directory.GetFiles(fileRootPath, "*_Content.bin", SearchOption.AllDirectories);
               string[] attachmentFiles = Directory.GetFiles(fileRootPath, "*.pdf", SearchOption.AllDirectories);
               Console.WriteLine("目标文件获取成功。。。");
               DataTable dtBody = GetBodyTable(bodyFiles);
               Console.WriteLine("Body文件转换成功。。。");
               Console.WriteLine("Body文件数量:"+dtBody.Rows.Count.ToString());
               DataTable dtAttachments = GetAttachmentsTable(attachmentFiles);
               
               Console.WriteLine("Attachments文件转换成功。。。");
               Console.WriteLine("Attachments文件数量:" + dtAttachments.Rows.Count.ToString());
               SqlConnection sqlConnection = new SqlConnection(connectionString);
               SqlCommand sqlCommand = new SqlCommand();
               sqlCommand.CommandText = "OA.uspUpdateAttachmentAndBody";
               sqlCommand.CommandType = CommandType.StoredProcedure;
               sqlCommand.Connection = sqlConnection;

               SqlParameter parameterAttachments = new SqlParameter("@Attachments", dtAttachments);
               parameterAttachments.Direction = ParameterDirection.Input;
               parameterAttachments.SqlDbType = SqlDbType.Structured;
               //需要确定表类型名称
               parameterAttachments.TypeName = "oa.uttMessageAttachmentForUpdate";
               sqlCommand.Parameters.Add(parameterAttachments);

               SqlParameter parameterBody = new SqlParameter("@Bodies", dtBody);
               parameterBody.Direction = ParameterDirection.Input;
               parameterBody.SqlDbType = SqlDbType.Structured;
               //需要确定表类型名称
               parameterBody.TypeName = "oa.uttMessageBodyForUpdate";
               sqlCommand.Parameters.Add(parameterBody);
               Console.WriteLine("提交数据到数据库。。。");
               sqlConnection.Open();

               sqlCommand.CommandTimeout = 1000 * 60 * 60;
               sqlCommand.ExecuteNonQuery();
               sqlConnection.Close();
               Console.WriteLine("升级完成。。。");
               Console.ReadLine();
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.Message);
               Console.ReadLine();
           }
       }

       private DataTable GetAttachmentsTable(string[] attachmentFiles)
       {
           return InnerGetTable(attachmentFiles,true);
       }
       private DataTable InnerGetTable(string[] fileNames,bool isAttachmentFile)
       {
           DataTable dt = GetTable();
           if (fileNames == null || fileNames.Length <= 0)
               return dt;
           int count=fileNames.Length;
           for (int i = 0; i < count; i++)
           {
               DataRow rowNew = dt.NewRow();
               string filePath = fileNames[i];
               string fileName = Path.GetFileName(filePath);
               if (fileName.Length < 36)
                   continue;
               int index = fileName.IndexOf("_");
              
               string guidString = fileName.Substring(0, index);
               if (!System.Text.RegularExpressions.Regex.IsMatch(guidString, @"^\w{8}-(\w{4}-){3}\w{12}$"))
                   continue;
               Guid id = new Guid(guidString);
               rowNew["IMessageID"] = id;
               if (isAttachmentFile)
               {
                   rowNew["Name"] = fileName.Substring(index+1);
               }
               using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
               {
                    
                   byte[] content = new byte[(int)fs.Length];
                   
                   fs.Read(content, 0, content.Length);
                   rowNew["Content"] = content;
                   fs.Close();
               }
               dt.Rows.Add(rowNew);

           }
           return dt;
       }

       private DataTable GetBodyTable(string[] bodyFiles)
       {
           DataTable dt = CreateBodyTable();
            if (bodyFiles == null || bodyFiles.Length <= 0)
               return dt;
           int count=bodyFiles.Length;
           for (int i = 0; i < count; i++)
           {
               DataRow rowNew = dt.NewRow();
               string filePath = bodyFiles[i];
               string fileName = Path.GetFileName(filePath);
               if (fileName.Length < 36)
                   continue;
               string[] fileTokens = fileName.Split('_');
               string guidString = fileTokens[0];
               if (!System.Text.RegularExpressions.Regex.IsMatch(guidString, @"^\w{8}-(\w{4}-){3}\w{12}$"))
                   continue;
               Guid id = new Guid(guidString);
               rowNew["IMessageID"] = id;
               using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
               {
                   byte[] content = new byte[(int)fs.Length];
                   fs.Read(content, 0, content.Length);
                   rowNew["Content"] = System.Text.UnicodeEncoding.GetEncoding("GB2312").GetString(content);
                   fs.Close();
               }
               dt.Rows.Add(rowNew);
           }
           return dt;
           
       }
       private DataTable GetTable()
       {
           DataTable dt = new DataTable("oa.uttMessageAttachmentsAndMailBody");
           dt.Columns.Add("IMessageID", typeof(Guid));
           dt.Columns.Add("Name", typeof(string));
           dt.Columns.Add("Content", typeof(byte[]));
           return dt;

       }
       private DataTable CreateBodyTable()
       {
           DataTable dt = new DataTable();
           dt.Columns.Add("IMessageID", typeof(Guid));
         
           dt.Columns.Add("Content", typeof(string));
           return dt;
       }
   
     
    
    }
}
