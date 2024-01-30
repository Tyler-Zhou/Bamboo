#region Comment

/*
 * 
 * FileName:    DocumentModel.cs
 * CreatedOn:   2014/5/14 星期三 16:27:08
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->文档数据DB交互类
 *      ->1.GetDocumentContentByID:通过文档ID获取文档具体内容信息
 *      ->2.GetDocumentListByOperationID:通过业务数据操作ID获取文档列表信息
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ICP.Document
{
    /// <summary>
    /// 文档数据DB交互类
    /// </summary>
    public class DocumentModel
    {
        /// <summary>
        /// 通过ID获取文档内容信息
        /// </summary>
        /// <param name="paramID">GUID ID</param>
        /// <returns></returns>
        public DocumentInfo GetDocumentContentByID(Guid paramID)
        {
            DocumentInfo item = null;
            try
            {
                String queryString = "SELECT * FROM [fcm.FileStorage]";

                SqlCeHelper sqlObject = new SqlCeHelper(ClientConstants.CurrentDBPath);
                sqlObject.AddWhereFields("ID", paramID.ToString());
                DataTable dt = sqlObject.ReturnDataTable(queryString);
                if (dt == null || dt.Rows.Count <= 0)
                    return new DocumentInfo();
                item = (from entry in dt.AsEnumerable()
                                     select new DocumentInfo
                                             {
                                                 Id = entry.Field<Guid>("ID"),
                                                 DName = entry.Field<String>("Name"),
                                                 Content=entry.Field<Byte[]>("Content"),
                                                 DType =(DocumentType?)entry.Field<byte>("TypeCode")
                                             }).FirstOrDefault();
            }
            catch
            {
                item = null;
            }

            return item;
        }

        /// <summary>
        /// 通过操作ID获取所有文档
        /// </summary>
        /// <param name="OperationID">操作ID</param>
        /// <returns></returns>
        public List<DocumentInfo> GetDocumentListByOperationID(Guid OperationID)
        {
            List<DocumentInfo> docList = null;
            try
            {
                String queryString = "SELECT * FROM [fcm.FileStorage]";

                SqlCeHelper sqlObject = new SqlCeHelper(ClientConstants.CurrentDBPath);

                sqlObject.AddWhereFields("OperationID", OperationID.ToString());

                DataTable dt = sqlObject.ReturnDataTable(queryString);
                
                if (dt == null || dt.Rows.Count <= 0)
                    return docList;

                docList = new List<DocumentInfo>();

                docList = (from entry in dt.AsEnumerable()
                           select new DocumentInfo
                                  {
                                      Id = entry.Field<Guid>("Id"),
                                      DName = entry.Field<String>("Name"),
                                      DType =(DocumentType?)entry.Field<byte>("TypeCode")
                                  }).ToList();
            }
            catch
            {
                docList = null;
            }
            return docList;
        }
    }
}
