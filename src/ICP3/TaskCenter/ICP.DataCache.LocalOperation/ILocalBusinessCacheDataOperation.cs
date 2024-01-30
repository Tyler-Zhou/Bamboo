using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface1;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.DataObjects;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.DataCache.LocalOperation1
{  
    /// <summary>
    /// 业务缓存数据操作接口
    /// </summary>
    public interface ILocalBusinessCacheDataOperation
    {  
     
 
        /// <summary>
        /// 获取某个业务下的指定文档Id的所有文档副本内容
        /// 如果对应Id的文档副本不存在,则返回Null值
        /// </summary>
        /// <param name="documentIds"></param>
        /// <returns></returns>
        List<ContentInfo> GetDocumentCopyContent(List<Guid> documentIds);
        /// <summary>
        /// 是否存在文档
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        bool IsDocumentExists(Guid documentId);
        /// <summary>
        /// 获取文档的Html副本
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ContentInfo GetDocumentHtmlContent(Guid id);
        /// <summary>
        /// 获取文档的内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ContentInfo GetDocumentContent(Guid id);
        /// <summary>
        /// 获取文档名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        String GetDocumentName(Guid id);
        /// <summary>
        /// 保存文档
        /// </summary>
        /// <param name="documents"></param>
        void SaveDocumentList(List<DocumentInfo> documents);
        /// <summary>
        /// 保存文件正本到数据库
        /// </summary>
        /// <param name="info"></param>
        void SaveDocumentContent(ContentInfo info);
        /// <summary>
        /// 保存文档副本到数据库
        /// </summary>
        /// <param name="info"></param>
        void SaveHtmlDocument(ContentInfo info);
        /// <summary>
        /// 删除文档
        /// </summary>
        /// <param name="id"></param>
        void DeleteDocument(Guid id);
        /// <summary>
        /// 删除多个文档
        /// </summary>
        /// <param name="ids"></param>
        void DeleteDocument(List<Guid> ids);
        /// <summary>
        /// 保存文档
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
         bool SaveDocument(DocumentInfo document);
        /// <summary>
         /// 保存多个文档
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
         bool SaveDocument(DocumentInfo[] documents);
        /// <summary>
        /// 文档保存到远程数据库成功后,更新本地文档Id,UpdateDate
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="results"></param>
         void UpdateDocumentRelation(DocumentInfo[] documents, ManyResult results);
        /// <summary>
        /// 更新文档上传状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="state"></param>
         void ChangeDocumentUploadState(Guid[] ids, UploadState state);
        /// <summary>
        /// 获取文档的详细信息，包含文件内容和副本
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         DocumentInfo GetDocumentDetailInfo(Guid id);
         void Save(List<DocumentInfo> newDocuments, List<Guid> deleteIds);

         /// <summary>
         /// 根据邮件地址，获取联系人类型
         /// </summary>
         /// <param name="emailAddress"></param>
         /// <returns>如果本地不存在，则返回null</returns>
         int? GetContactPersonType(string emailAddress);
        /// <summary>
        /// 保存联系人类型信息
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="type"></param>
         void SaveContactPersonType(string emailAddress, int type);
        /// <summary>
        /// 获取用户自定义列表信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="listType"></param>
        /// <returns></returns>
         UserCustomGridInfo GetCustomGridInfo(Guid userId, ListFormType listType);





        /// <summary>
        /// 保存用户自定义列表信息
        /// </summary>
        /// <param name="customInfo"></param>
         void SaveCustomGridInfo(UserCustomGridInfo customInfo);

         #region  joe 2013-05-20 添加
         /// <summary>
         /// 获取用户自定义列表信息
         /// </summary>
         /// <param name="userId"></param>
         /// <param name="listType"></param>
         /// <returns></returns>
         UserCustomGridInfo GetCustomGridInfo(Guid userId, string templateCode);
         #endregion
    

    }
}
