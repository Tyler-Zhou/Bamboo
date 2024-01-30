using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Threading;
using ICP.MailCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.DataCache.ServiceInterface1
{
    public class DataCacheUtility
    {
        static DataCacheUtility()
        {
            if (!Directory.Exists(HtmlTempPath))
            {
                Directory.CreateDirectory(HtmlTempPath);
            }
            if (!Directory.Exists(ContentTempPath))
            {
                Directory.CreateDirectory(ContentTempPath);
            }
            if (!Directory.Exists(MailTempRootPath))
            {
                Directory.CreateDirectory(MailTempRootPath);
            }
            if (!Directory.Exists(ThumbImageRootPath))
            {
                Directory.CreateDirectory(ThumbImageRootPath);
            }

        }
        private static String basePath = System.AppDomain.CurrentDomain.BaseDirectory;
        public static String HtmlTempPath = Path.Combine(basePath, "pdftemp");
        public static String ContentTempPath = Path.Combine(basePath, "filetemp");
        public static String MailTempRootPath = Path.Combine(basePath, "mail");
        public static string ThumbImageRootPath = Path.Combine(basePath, "ThumbImages");
        private static Hashtable _properties = new Hashtable(10);
        private static object objSyn = new object();
        public static List<String> GetTypePropertyNames(Type type)
        {
            if (_properties.ContainsKey(type))
                return _properties[type] as List<String>;
            List<String> properties = type.GetType().GetProperties().Select<PropertyInfo, String>(property => property.Name).Where(name => name != "Id").ToList();
            if (!_properties.ContainsKey(type))
            {
                lock (objSyn.GetType())
                {
                    if (!_properties.ContainsKey(type))
                    {
                        _properties.Add(type, properties);
                    }
                }
            }
            return properties;
        }
        public static Dictionary<String, String> GetTypePropertyNameAndValue(object obj)
        {
            Type type = obj.GetType();
            List<String> columnNames = DataCacheUtility.GetTypePropertyNames(type);
            Dictionary<String, String> dic = new Dictionary<String, String>();
            foreach (String name in columnNames)
            {
                dic.Add(name, type.GetProperty(name).GetValue(obj, null).ToString());
            }
            return dic;
        }
        public static UserCustomGridInfo ConvertTableToUserCustomGridInfo(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
                return null;
            UserCustomGridInfo customInfo = (from row in dt.AsEnumerable()
                                             select new UserCustomGridInfo()
                                                         {
                                                             Id = row.Field<Guid>("Id"),
                                                             TemplateCode = row.Field<String>("TemplateCode"),
                                                             UserId = row.Field<Guid?>("UserId"),
                                                             UpdateDate = row.Field<DateTime?>("UpdateDate"),

                                                         }).FirstOrDefault();
            UserCustomGridInfo xmlInfo = SerializerHelper.DeserializeFromString<UserCustomGridInfo>(dt.Rows[0].Field<string>("ColumnData"));
            customInfo.Columns = xmlInfo.Columns;
            return customInfo;

        }
        public static List<DocumentInfo> ConvertTableToDocumentInfoList(DataTable dt)
        {
            List<DocumentInfo> documents = new List<DocumentInfo>();
            if (dt == null || dt.Rows.Count <= 0)
                return documents;
            try
            {
                documents = (from row in dt.AsEnumerable()
                             select new DocumentInfo()
                             {
                                 OperationID = row.Field<Guid>("OperationId"),
                                 Id = row.Field<Guid>("Id"),
                                 //CommunicationHistoryId = row.Field<Int32>("CommunicationHistoryId"),
                                 CreateBy = row.Field<Guid>("CreateBy"),
                                 Remark = row.Field<string>("Remark"),
                                 FormType = (FormType)row.Field<Byte>("FormType"),
                                 UpdateBy = row.Field<Guid?>("UpdateBy"),

                                 Name = row.Field<String>("Name"),
                                 Type = (OperationType)row.Field<Byte>("OperationType"),
                                 DocumentType = (DocumentType)row.Field<byte>("DocumentType"),
                                 UpdateDate = row.Field<DateTime?>("UpdateDate"),
                                 CreateDate = row.Field<DateTime>("CreateDate"),
                                 State = (UploadState)row.Field<byte>("UploadState")
                             }).ToList();
                return documents;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public static List<CommunicationHistory> ConvertTableToCommunicationHistoryList(DataTable dt)
        {
            List<CommunicationHistory> list = new List<CommunicationHistory>();
            if (dt == null || dt.Rows.Count <= 0)
                return list;
            list = (from row in dt.AsEnumerable()
                    select new CommunicationHistory()
                    {
                        OperationId = row.Field<Guid>("OperationId"),
                        Subject = row.Field<String>("Subject"),
                        SendFrom = row.Field<String>("SendFrom"),
                        SendTo = row.Field<String>("SendTo"),
                        State = row.Field<MessageState>("State"),
                        Body = row.Field<String>("TextBody"),
                        HasAttachment = row.Field<bool>("HasAttachment"),
                        Id = row.Field<Guid>("RemoteId"),
                        Type = row.Field<MessageType>("Type"),
                        //UpdateDate = row.Field<DateTime>("UpdateDate"),
                        CreateDate = row.Field<DateTime>("CreateDate")
                    }).ToList();
            return list;
        }
        public static bool CheckFileExists(String path, bool throwExceptionIfNotExists)
        {
            if (!System.IO.File.Exists(path))
            {
                if (throwExceptionIfNotExists)
                {
                    throw new FileNotFoundException(path);
                }
                return false;
            }
            return true;
        }
        public static Byte[] ReadFileContentFromDisk(String filePath)
        {
            CheckFileExists(filePath, true);
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] content = new byte[(int)fs.Length];
                fs.Read(content, 0, content.Length);
                fs.Close();

                return content;
            }

        }
        public static String SaveFileContentToDisk(ContentInfo content)
        {
            return InnerSaveFileToDisk(content, false);
        }
        private static String InnerSaveFileToDisk(ContentInfo content, bool isHtmlCopy)
        {
            string rootPath = isHtmlCopy ? HtmlTempPath : ContentTempPath;
            String tempDirectoryPath = Path.Combine(rootPath, content.Id.ToString());
            EnsureDirectoryExists(tempDirectoryPath);
            String filePath = Path.Combine(tempDirectoryPath, content.Name);

            WriteToDisk(filePath, content.Content);

            return filePath;
        }
        private static void WriteToDisk(string filePath, Byte[] content)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                stream.Write(content, 0, content.Length);
            }
        }

        public static void SaveFileContentToDisk(ContentInfo info, String fileSavePath)
        {
            string directoryName = Path.GetDirectoryName(fileSavePath);
            EnsureDirectoryExists(directoryName);
            if (!System.IO.File.Exists(fileSavePath))
            {
                WriteToDisk(fileSavePath, info.Content);

            }

        }
        public static void SaveFileContentToDisk(List<ContentInfo> infos, string basePath)
        {
            foreach (ContentInfo copy in infos)
            {
                SaveFileContentToDisk(copy, Path.Combine(basePath, copy.Name));
            }
        }

        public static void SaveFileContentToDiskAsync(List<ContentInfo> infos, String basePath)
        {
            WaitCallback callback = (info) =>
            {
                ContentInfo content = info as ContentInfo;
                SaveFileContentToDisk(content, Path.Combine(basePath, content.Name));

            };
            Action<ContentInfo> action = (info) =>
            {
                ThreadPool.QueueUserWorkItem(callback, info);
            };
            Array.ForEach(infos.ToArray(), action);
        }
        private static void EnsureDirectoryExists(string directoryName)
        {
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
        }

        public static string SaveFileHtmlContentToDisk(ContentInfo content)
        {
            return InnerSaveFileToDisk(content, true);
        }
        public static string GetIdsString(Guid[] ids)
        {
            StringBuilder builder = new StringBuilder(100);
            foreach (Guid id in ids)
            {
                builder.AppendFormat("'{0}',", id.ToString());
            }
            return builder.ToString(0, builder.Length - 1);
        }
        public static DataTable CreateHistoryAttachmentTable(string tableName)
        {
            DataTable dt = new DataTable(tableName);
            dt.Columns.Add("Id", typeof(Guid));
            dt.Columns.Add("IMessageID", typeof(Guid));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Body", typeof(Byte[]));
            DataColumn column = new DataColumn("UpdateDate", typeof(DateTime));

            column.AllowDBNull = true;

            dt.Columns.Add(column);
            column = new DataColumn("RowIndex", typeof(int));
            dt.Columns.Add(column);

            return dt;
        }
        public static DataTable CreateAttachmentTable(string tableName)
        {
            DataTable dt = new DataTable(tableName);
            dt.Columns.Add("Id", typeof(Guid));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Content", typeof(Byte[]));


            return dt;

        }
        /// <summary>
        /// 构造查询条件语句
        /// </summary>
        /// <param name="list"></param>
        /// <param name="names"></param>
        /// <param name="isValueList"></param>
        /// <returns></returns>
        public static string GetExpression(List<string> list, List<string> names, bool isValueList)
        {
            if (list == null || list.Count <= 0 || names == null || names.Count <= 0)
                return string.Empty;
            List<string> result = new List<string>();
            if (isValueList)
            {
                foreach (string name in names)
                {
                    string temp = (from no in list
                                   select string.Format("[{0}] like '%{1}%' ", name, no)).ToList().Aggregate((a, b) => a + " OR " + b);
                    result.Add(temp);
                }

            }
            else
            {
                return GetExpression(names, list, true);

            }
            string totalCondition = (from condition in result
                                     select condition).Aggregate((a, b) => a + " OR " + b);
            return totalCondition;
        }
    }
}
