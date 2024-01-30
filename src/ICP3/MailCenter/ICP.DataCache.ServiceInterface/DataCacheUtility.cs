﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Threading;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Common.ServiceInterface;
using ICP.FileSystem.ServiceInterface;

namespace ICP.DataCache.ServiceInterface
{
    public class DataCacheUtility
    {
        private static Hashtable _properties = new Hashtable(10);
        private static object objSyn = new object();
        public static List<String> GetTypePropertyNames(Type type)
        {
            if (_properties.ContainsKey(type))
                return _properties[type] as List<String>;
            List<String> properties = type.GetType().GetProperties().Select<PropertyInfo, String>(property => property.Name).Where(name => name != "Id").ToList();
            if (!_properties.ContainsKey(type))
            {
                lock (objSyn)
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
            List<String> columnNames = GetTypePropertyNames(type);
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
                                                             TemplateCode = row.Field<String>("TemplateCode"),        // 操作视图代码  joe 2013-06-07   
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
                                 Type = (OperationType)row.Field<Byte>("Type"),
                                 DocumentType = (DocumentType)row.Field<byte>("DocumentType"),
                                 UpdateDate = row.Field<DateTime?>("UpdateDate"),
                                 CreateDate = row.Field<DateTime>("CreateDate"),
                                 State = (UploadState)row.Field<byte>("UploadState")
                             }).ToList();
                return documents;
            }
            catch (Exception ex)
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
                        CreateDate = row.Field<DateTime>("CreateDate")
                    }).ToList();
            return list;
        }
        /// <summary>
        /// 将DataSet转换成OperationContactInfo
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<OperationContactInfo> ConvertTableToOperationContactInfo(DataTable dt)
        {
            List<OperationContactInfo> list = new List<OperationContactInfo>();
            if (dt == null || dt.Rows.Count <= 0)
                return list;
            list = (from row in dt.AsEnumerable()
                    select new OperationContactInfo()
                    {
                        ID = row.Field<Guid>("ID"),
                        Mail = row.Field<String>("Mail"),
                        CustomerID = row.Field<Guid?>("CustomerID"),
                        Customer = row.Field<bool>("IsCustomer"),
                        Carrier = row.Field<bool>("IsCarrier"),
                        OE = row.Field<bool>("IsOE"),
                        OI = row.Field<bool>("IsOI"),
                        AE = row.Field<bool>("IsAE"),
                        AI = row.Field<bool>("IsAI"),
                        Trk = row.Field<bool>("IsTrk"),
                        Other = row.Field<bool>("IsOther"),
                        UpdateDate = row.Field<DateTime>("UpdateDate")
                    }).ToList();
            return list;
        }

        public static String SaveFileContentToDisk(ContentInfo content)
        {
            return InnerSaveFileToDisk(content, false);
        }
        private static String InnerSaveFileToDisk(ContentInfo content, bool isHtmlCopy)
        {
            string rootPath = isHtmlCopy ? IOHelper.HtmlTempPath : IOHelper.ContentTempPath;
            String tempDirectoryPath = Path.Combine(rootPath, content.Id.ToString());
            IOHelper.EnsureDirectoryExists(tempDirectoryPath);
            String filePath = Path.Combine(tempDirectoryPath, content.Name);

            IOHelper.WriteToDisk(filePath, content.Content);

            return filePath;
        }

        /// <summary>
        /// 保存ContentInfo集合，返回保存路径集合
        /// </summary>
        /// <param name="contentlist"></param>
        /// <returns></returns>
        public static List<string> SaveFileListToDisk(List<ContentInfo> contentlist)
        {
            List<string> filepaths = new List<string>();
            foreach (ContentInfo item in contentlist)
            {
                string filepath = InnerSaveFileToDisk(item, false);
                filepaths.Add(filepath); 
            }
            return filepaths;
            //WaitCallback callback = (info) =>
            //{
            //    ContentInfo content = info as ContentInfo;
            //    string filepath = InnerSaveFileToDisk(content, false);
            //    filepaths.Add(filepath);
            //};

            //Action<ContentInfo> action = (info) =>
            //{
            //    ThreadPool.QueueUserWorkItem(callback, info);
            //};
            //Array.ForEach(contentlist.ToArray(), action);
            //return filepaths;
        }

        public static void SaveFileContentToDisk(ContentInfo info, String fileSavePath)
        {
            string directoryName = Path.GetDirectoryName(fileSavePath);
            IOHelper.EnsureDirectoryExists(directoryName);
            if (!System.IO.File.Exists(fileSavePath))
            {
                IOHelper.WriteToDisk(fileSavePath, info.Content);

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
