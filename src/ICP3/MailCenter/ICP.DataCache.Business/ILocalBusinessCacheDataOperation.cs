using System;
using System.Collections.Generic;
using System.Data;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;

namespace ICP.DataCache.LocalOperation
{
    ///// <summary>
    ///// 业务缓存数据操作接口
    ///// </summary>
    //public interface ILocalBusinessCacheDataOperation
    //{

    //    #region Document
    //    /// <summary>
    //    /// 获取某个业务下的指定文档Id的所有文档副本内容
    //    /// 如果对应Id的文档副本不存在,则返回Null值
    //    /// </summary>
    //    /// <param name="documentIds"></param>
    //    /// <returns></returns>
    //    List<ContentInfo> GetDocumentCopyContent(List<Guid> documentIds);
    //    /// <summary>
    //    /// 是否存在文档
    //    /// </summary>
    //    /// <param name="documentId"></param>
    //    /// <returns></returns>
    //    bool IsDocumentExists(Guid documentId);
    //    /// <summary>
    //    /// 获取文档的Html副本
    //    /// </summary>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    ContentInfo GetDocumentHtmlContent(Guid id);
    //    /// <summary>
    //    /// 根据业务ID获取缓存文件
    //    /// </summary>
    //    /// <param name="OperationId"></param>
    //    /// <returns></returns>
    //    List<DocumentInfo> GetDocumentListInfo(Guid OperationId);
    //    /// <summary>
    //    /// 获取文档的内容
    //    /// </summary>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    ContentInfo GetDocumentContent(Guid id);
    //    /// <summary>
    //    /// 获取文档名称
    //    /// </summary>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    String GetDocumentName(Guid id);
    //    /// <summary>
    //    /// 保存文档
    //    /// </summary>
    //    /// <param name="documents"></param>
    //    void SaveDocumentList(List<DocumentInfo> documents);
    //    /// <summary>
    //    /// 保存文件正本到数据库
    //    /// </summary>
    //    /// <param name="info"></param>
    //    void SaveDocumentContent(ContentInfo info);
    //    /// <summary>
    //    /// 保存文档副本到数据库
    //    /// </summary>
    //    /// <param name="info"></param>
    //    void SaveHtmlDocument(ContentInfo info);
    //    /// <summary>
    //    /// 删除文档
    //    /// </summary>
    //    /// <param name="id"></param>
    //    void DeleteDocument(Guid id);
    //    /// <summary>
    //    /// 删除多个文档
    //    /// </summary>
    //    /// <param name="ids"></param>
    //    void DeleteDocument(List<Guid> ids);
    //    /// <summary>
    //    /// 保存文档
    //    /// </summary>
    //    /// <param name="document"></param>
    //    /// <returns></returns>
    //    bool SaveDocument(DocumentInfo document);
    //    /// <summary>
    //    /// 保存多个文档
    //    /// </summary>
    //    /// <param name="documents"></param>
    //    /// <returns></returns>
    //    bool SaveDocument(DocumentInfo[] documents);
    //    /// <summary>
    //    /// 文档保存到远程数据库成功后,更新本地文档Id,UpdateDate
    //    /// </summary>
    //    /// <param name="documents"></param>
    //    /// <param name="results"></param>
    //    void UpdateDocumentRelation(DocumentInfo[] documents, ManyResult results);
    //    /// <summary>
    //    /// 更新文档上传状态
    //    /// </summary>
    //    /// <param name="ids"></param>
    //    /// <param name="state"></param>
    //    void ChangeDocumentUploadState(Guid[] ids, UploadState state);
    //    /// <summary>
    //    /// 获取文档的详细信息，包含文件内容和副本
    //    /// </summary>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    DocumentInfo GetDocumentDetailInfo(Guid id);
    //    void Save(List<DocumentInfo> newDocuments, List<Guid> deleteIds); 
    //    #endregion

    //    #region Operation Contact
    //    /// <summary>
    //    /// 根据邮件地址，获取联系人类型
    //    /// </summary>
    //    /// <param name="emailAddress"></param>
    //    /// <returns>如果本地不存在，则返回null</returns>
    //    int? GetContactPersonType(string emailAddress);
    //    /// <summary>
    //    /// 通过邮件域名获取联系人类型
    //    /// </summary>
    //    /// <param name="emailAddress"></param>
    //    /// <returns></returns>
    //    int? GetContactPersonTypeByMailDomain(string emailAddress);
    //    /// <summary>
    //    /// 保存联系人类型信息
    //    /// </summary>
    //    /// <param name="emailAddress"></param>
    //    /// <param name="type"></param>
    //    void SaveContactPersonType(string emailAddress, int type);
    //    /// <summary>
    //    /// 保存业务联系人到业务联系人缓存表
    //    /// </summary>
    //    ///<param name="contactList"></param>
    //    void SaveContacts(List<OperationContactInfo> contactList);
    //    /// <summary>
    //    /// 联系人是否存在缓存
    //    /// </summary>
    //    /// <param name="ExternalEmails"></param>
    //    /// <returns></returns>
    //    bool IsAllContactExsitCache(List<string> ExternalEmails);
    //    /// <summary>
    //    /// 保存用户自定义列表信息
    //    /// </summary>
    //    /// <param name="customInfo"></param>
    //    void SaveCustomGridInfo(UserCustomGridInfo customInfo);
    //    /// <summary>
    //    /// 删除相同业务号的联系人
    //    /// </summary>
    //    string RemoveSameOperationContacts(Guid? relationID, Guid operationId, string xmlContacts);
    //    /// <summary>
    //    /// 获取单票业务的ContactMail
    //    /// </summary>
    //    /// <param name="operationId"></param>
    //    /// <param name="operationType"></param>
    //    /// <returns></returns>
    //    string GetSingleOperationContactMail(Guid operationId, OperationType operationType);
    //    /// <summary>
    //    /// 保存单个业务的ContactMail
    //    /// </summary>
    //    /// <param name="operationID"></param>
    //    /// <param name="operationType"></param>
    //    /// <param name="contactMail"></param>
    //    void SingleSaveOperationContactMail(Guid operationID, OperationType operationType, string contactMail);
    //    /// <summary>
    //    /// 保存一封邮件所有的业务联系人
    //    /// </summary>
    //    /// <param name="ContactParameters"></param>
    //    void SaveOperationContactMail(List<OperationContactParameters> ContactParameters);
    //    /// <summary>
    //    /// 清空本公司联系人
    //    /// </summary>
    //    void ClearOperationContactEMail();
    //    #endregion

    //    #region Custom Grid Info
    //    /// <summary>
    //    /// 获取用户自定义列表信息
    //    /// </summary>
    //    /// <param name="userId"></param>
    //    /// <param name="listType"></param>
    //    /// <returns></returns>
    //    UserCustomGridInfo GetCustomGridInfo(Guid userId, string templateCode); 
    //    #endregion

    //    #region Language
    //    void DeleteMultiLanguageList(Guid[] ids);
    //    void SaveMultiLanguageList(DataTable dt); 
    //    #endregion

    //    #region OperationMessageRelation
    //    DataTable GetMultiLanguageList(string[] fullNames, string[] formNames);
    //    /// <summary>
    //    /// 获取缓存业务信息：配置文件组建SQL
    //    /// </summary>
    //    /// <param name="criteria"></param>
    //    /// <returns></returns>
    //    DataTable GetOperationViewList(BusinessQueryCriteria criteria);

    //    /// <summary>
    //    /// 查找业务集合：固定SQL
    //    /// </summary>
    //    DataTable GetOperationViewListFixed(BusinessQueryCriteria criteria);
    //    /// <summary>
    //    /// 获取邮件关联信息
    //    /// </summary>
    //    /// <param name="message"></param>
    //    /// <returns></returns>
    //    List<OperationMessageRelation> GetOperationMessageRelationByMessageIdAndReference(string messageId, string reference);

    //    /// <summary>
    //    /// 根据邮件ID，获取关联信息，返回Datatable
    //    /// </summary>
    //    DataTable GetOperationMessageRelationByMessageId(string messageId);
    //    /// <summary>
    //    /// 获取缓存业务信息和邮件关联信息
    //    /// </summary>
    //    /// <param name="criteria"></param>
    //    ///<param name="messageID">消息中的MessageID特性值</param>
    //    ///<param name="reference">消息中的Reference特性值</param>
    //    /// <returns></returns>
    //    BusinessQueryResult Get(BusinessQueryCriteria criteria, string messageID, string reference);

    //    /// <summary>
    //    /// 保存邮件关联信息
    //    /// </summary>
    //    /// <param name="relationMessage"></param>
    //    /// <returns></returns>                
    //    void SaveOperationMessageRelation(OperationMessageRelation[] relationMessages);
    //    /// <summary>
    //    /// 根据MessageID删除邮件与业务的关联信息
    //    /// </summary>
    //    /// <param name="messageIDs"></param>
    //    void RemoveOperationMessageRelation(List<string> messageIDs);
    //    /// <summary>
    //    /// 根据OperationID来删除邮件也业务的关联信息
    //    /// </summary>
    //    /// <param name="operationIDs"></param>
    //    void RemoveOperationMessageRelation(Guid[] operationIDs);
    //    /// <summary>
    //    /// 根据主键删除关联信息
    //    /// </summary>
    //    /// <param name="messageRelationIDs"></param>
    //    void RemoveOperationMessageRelations(Guid[] messageRelationIDs);
        
    //    /// <summary>
    //    ///该邮件是否有关联信息
    //    /// </summary>
    //    /// <param name="messageID"></param>
    //    /// <returns></returns>
    //    bool HasLocalOperationMessageRelation(string messageID);

    //    /// <summary>
    //    /// 获取业务的关联信息
    //    /// </summary>
    //    /// <param name="operationIds"></param>
    //    /// <returns></returns>
    //    List<OperationMessageRelation> GetOperationMessages(string messageID, Guid[] operationIds);
    //    /// <summary>
    //    /// 获取没有上传带服务端的所有关联信息
    //    /// </summary>
    //    /// <returns></returns>
    //    OperationMessageRelation[] GetLocalMessageRealtionsByUploadServer();
    //    /// <summary>
    //    /// 通过是否备份状态获取没有上传到服务端的所有所有关联信息
    //    /// </summary>
    //    /// <returns></returns>
    //    OperationMessageRelation[] GetLocalMessageRealtionsByBackupMail();
    //    /// <summary>
    //    /// 根据邮件ID获取邮件关联信息
    //    /// </summary>
    //    /// <param name="messageId"></param>
    //    /// <returns></returns>
    //    OperationMessageRelation GetOperationMessageRelationByMessageID(string messageId);
    //    /// <summary>
    //    /// 保存邮件备份状态
    //    /// </summary>
    //    /// <param name="relationMessages">关联信息集合</param>
    //    /// <returns></returns>                
    //    void SaveBackUpMailState(OperationMessageRelation[] relationMessages);
    //    /// <summary>
    //    /// 删除本地缓存由于程序原因，影响数据不正常而导致批量上传失败的数据。
    //    /// </summary>
    //    bool RemoveJunkOperationMessageRelationData();

    //    /// <summary>
    //    /// 重置异常数据
    //    /// </summary>
    //    bool ResetExceptionData();
    //    #endregion

    //    #region Business
    //    /// <summary>
    //    /// 更新本地缓存业务数据
    //    /// </summary>
    //    /// <param name="operationId"></param>
    //    /// <param name="operationType"></param>
    //    /// <param name="dt"></param>
    //    void UpdateLocalBusinessData(List<Guid> operationIds, OperationType operationType, DataTable dt);

    //    /// <summary>
    //    /// 获取本地缓存业务数据
    //    /// </summary>
    //    /// <param name="operationID"></param>
    //    /// <param name="type"></param>
    //    /// <returns></returns>
    //    DataTable GetOperationViewInfo(Guid operationID, OperationType type);

    //    /// <summary>
    //    /// 获取业务参与这相关信息
    //    /// </summary>
    //    /// <param name="operationID"></param>
    //    /// <param name="operationType"></param>
    //    /// <returns></returns>
    //    DataTable GetOperationAssistantInfo(Guid operationID, OperationType operationType);

    //    /// <summary>
    //    /// 根据邮件主题匹配单号获取业务
    //    /// </summary>
    //    /// <param name="keyWord"></param>
    //    /// <returns></returns>
    //    DataTable GetOperationViewListBySubjectKeyWord(BusinessQueryCriteria criteria);
    //    /// <summary>
    //    /// 判断业务是否在本地缓存存在
    //    /// </summary>
    //    /// <param name="operationId"></param>
    //    /// <param name="operationType"></param>
    //    /// <returns></returns>
    //    bool IsShipmentExists(Guid operationId, OperationType operationType); 
    //    #endregion
        
    //}
}
