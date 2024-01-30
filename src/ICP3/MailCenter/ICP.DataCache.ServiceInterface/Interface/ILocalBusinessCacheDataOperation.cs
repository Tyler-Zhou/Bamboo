using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 业务缓存数据操作接口
    /// </summary>
    //[ICPServiceHost]
    //[ServiceContract]
    public interface ILocalBusinessCacheDataOperation
    {
        #region Document
        /// <summary>
        /// 获取某个业务下的指定文档Id的所有文档副本内容
        /// 如果对应Id的文档副本不存在,则返回Null值
        /// </summary>
        /// <param name="documentIds"></param>
        /// <returns></returns>
        ///[OperationContract]
        List<ContentInfo> GetDocumentCopyContent(List<Guid> documentIds);

        /// <summary>
        /// 是否存在文档
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        ///[OperationContract]
        bool IsDocumentExists(Guid documentId);

        /// <summary>
        /// 获取文档的Html副本
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///[OperationContract]
        ContentInfo GetDocumentHtmlContent(Guid id);

        /// <summary>
        /// 根据业务ID获取缓存文件
        /// </summary>
        /// <param name="OperationId"></param>
        /// <returns></returns>
        ///[OperationContract]
        List<DocumentInfo> GetDocumentListInfo(Guid OperationId);

        /// <summary>
        /// 获取文档的内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///[OperationContract]
        ContentInfo GetDocumentContent(Guid id);

        /// <summary>
        /// 获取文档名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///[OperationContract]
        String GetDocumentName(Guid id);

        /// <summary>
        /// 保存文档
        /// </summary>
        /// <param name="documents"></param>
        ///[OperationContract]
        void SaveDocumentList(List<DocumentInfo> documents);

        /// <summary>
        /// 保存文件正本到数据库
        /// </summary>
        /// <param name="info"></param>
        ///[OperationContract]
        void SaveDocumentContent(ContentInfo info);

        /// <summary>
        /// 保存文档副本到数据库
        /// </summary>
        /// <param name="info"></param>
        ///[OperationContract]
        void SaveHtmlDocument(ContentInfo info);

        /// <summary>
        /// 删除文档
        /// </summary>
        /// <param name="id"></param>
        ///[OperationContract]
        void DeleteDocument(Guid id);

        /// <summary>
        /// 删除多个文档
        /// </summary>
        /// <param name="ids"></param>
        [OperationContract(Name = "DeleteDocument1")]
        void DeleteDocument(List<Guid> ids);

        /// <summary>
        /// 保存文档
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        ///[OperationContract]
        bool SaveDocument(DocumentInfo document);

        /// <summary>
        /// 保存多个文档
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveDocument1")]
        bool SaveDocument(DocumentInfo[] documents);

        /// <summary>
        /// 文档保存到远程数据库成功后,更新本地文档Id,UpdateDate
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="results"></param>
        ///[OperationContract]
        void UpdateDocumentRelation(DocumentInfo[] documents, ManyResult results);

        /// <summary>
        /// 更新文档上传状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="state"></param>
        ///[OperationContract]
        void ChangeDocumentUploadState(Guid[] ids, UploadState state);

        /// <summary>
        /// 获取文档的详细信息，包含文件内容和副本
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///[OperationContract]
        DocumentInfo GetDocumentDetailInfo(Guid id);

        /// <summary>
        /// 保存文档
        /// </summary>
        /// <param name="newDocuments"></param>
        /// <param name="deleteIds"></param>
        ///[OperationContract]
        void Save(List<DocumentInfo> newDocuments, List<Guid> deleteIds);

        #endregion

        #region Operation Contact
        /// <summary>
        /// 获取所有联系人
        /// </summary>
        /// <returns></returns>
        ///[OperationContract]
        DataTable GetAllContact();
        /// <summary>
        /// 根据邮件地址，获取联系人类型
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns>如果本地不存在，则返回null</returns>
        ///[OperationContract]
        int? GetContactPersonType(string emailAddress);

        /// <summary>
        /// 通过邮件域名获取联系人类型
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        ///[OperationContract]
        int? GetContactPersonTypeByMailDomain(string emailAddress);

        /// <summary>
        /// 保存联系人类型信息
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="type"></param>
        ///[OperationContract]
        void SaveContactPersonType(string emailAddress, int type);

        /// <summary>
        /// 保存业务联系人到业务联系人缓存表
        /// </summary>
        ///<param name="contactList"></param>
        ///[OperationContract]
        void SaveContacts(List<OperationContactInfo> contactList);

        /// <summary>
        /// 联系人是否存在缓存
        /// </summary>
        /// <param name="ExternalEmails"></param>
        /// <returns></returns>
        ///[OperationContract]
        bool IsAllContactExsitCache(List<string> ExternalEmails);

        /// <summary>
        /// 保存用户自定义列表信息
        /// </summary>
        /// <param name="customInfo"></param>
        ///[OperationContract]
        void SaveCustomGridInfo(UserCustomGridInfo customInfo);

        /// <summary>
        /// 删除相同业务号的联系人
        /// </summary>
        ///[OperationContract]
        string RemoveSameOperationContacts(Guid? relationID, Guid operationId, string xmlContacts);

        /// <summary>
        /// 获取单票业务的ContactMail
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        ///[OperationContract]
        string GetSingleOperationContactMail(Guid operationId, OperationType operationType);

        /// <summary>
        /// 保存单个业务的ContactMail
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="operationType"></param>
        /// <param name="contactMail"></param>
        ///[OperationContract]
        void SingleSaveOperationContactMail(Guid operationID, OperationType operationType, string contactMail);

        /// <summary>
        /// 保存一封邮件所有的业务联系人
        /// </summary>
        /// <param name="ContactParameters"></param>
        ///[OperationContract]
        void SaveOperationContactMail(List<OperationContactParameters> ContactParameters);

        /// <summary>
        /// 清空本公司联系人
        /// </summary>
        ///[OperationContract]
        void ClearOperationContactEMail();

        #endregion

        #region Custom Grid Info
        /// <summary>
        /// 获取用户自定义列表信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="templateCode"></param>
        /// <returns></returns>
        ///[OperationContract]
        UserCustomGridInfo GetCustomGridInfo(Guid userId, string templateCode);
        #endregion

        #region Language
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullNames"></param>
        /// <param name="formNames"></param>
        /// <returns></returns>
        ///[OperationContract]
        DataTable GetMultiLanguageList(string[] fullNames, string[] formNames);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        ///[OperationContract]
        void DeleteMultiLanguageList(Guid[] ids);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        ///[OperationContract]
        void SaveMultiLanguageList(DataTable dt);
        #endregion

        #region OperationMessageRelation
        /// <summary>
        /// 获取邮件关联信息
        /// </summary>
        /// <returns></returns>
        ///[OperationContract]
        DataTable GetAllOperationMessageRelation();

        /// <summary>
        /// 批量上传关联信息
        /// </summary>
        ///[OperationContract]
        void UploadMessageRelation4Batch();
        

        /// <summary>
        /// 获取邮件关联信息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        ///[OperationContract]
        List<OperationMessageRelation> GetOperationMessageRelationByMessageIdAndReference(string messageId, string reference);

        /// <summary>
        /// 根据邮件ID，获取关联信息，返回Datatable
        /// </summary>
        ///[OperationContract]
        DataTable GetOperationMessageRelationByMessageId(string messageId);

        /// <summary>
        /// 获取缓存业务信息和邮件关联信息
        /// </summary>
        /// <param name="criteria"></param>
        ///<param name="messageID">消息中的MessageID特性值</param>
        ///<param name="reference">消息中的Reference特性值</param>
        /// <returns></returns>
        ///[OperationContract]
        BusinessQueryResult Get(BusinessQueryCriteria criteria, string messageID, string reference);

        /// <summary>
        /// 保存邮件关联信息
        /// </summary>
        /// <param name="relationMessages"></param>
        /// <returns></returns>                
        ///[OperationContract]
        void SaveOperationMessageRelation(OperationMessageRelation[] relationMessages);

        /// <summary>
        /// 根据MessageID删除邮件与业务的关联信息
        /// </summary>
        /// <param name="messageIDs"></param>
        ///[OperationContract]
        void RemoveOperationMessageRelation(List<string> messageIDs);

        /// <summary>
        /// 根据OperationID来删除邮件也业务的关联信息
        /// </summary>
        /// <param name="operationIDs"></param>
        [OperationContract(Name = "RemoveOperationMessageRelation1")]
        void RemoveOperationMessageRelation(Guid[] operationIDs);

        /// <summary>
        /// 根据主键删除关联信息
        /// </summary>
        /// <param name="messageRelationIDs"></param>
        ///[OperationContract]
        void RemoveOperationMessageRelations(Guid[] messageRelationIDs);

        /// <summary>
        ///该邮件是否有关联信息
        /// </summary>
        /// <param name="messageID"></param>
        /// <returns></returns>
        ///[OperationContract]
        bool HasLocalOperationMessageRelation(string messageID);

        /// <summary>
        /// 获取业务的关联信息
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="operationIds"></param>
        /// <returns></returns>
        ///[OperationContract]
        List<OperationMessageRelation> GetOperationMessages(string messageID, Guid[] operationIds);

        /// <summary>
        /// 获取没有上传带服务端的所有关联信息
        /// </summary>
        /// <returns></returns>
        ///[OperationContract]
        OperationMessageRelation[] GetLocalMessageRealtionsByUploadServer();

        /// <summary>
        /// 通过是否备份状态获取没有上传到服务端的所有所有关联信息
        /// </summary>
        /// <returns></returns>
        ///[OperationContract]
        OperationMessageRelation[] GetLocalMessageRealtionsByBackupMail();

        /// <summary>
        /// 根据邮件ID获取邮件关联信息
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        ///[OperationContract]
        OperationMessageRelation GetOperationMessageRelationByMessageID(string messageId);

        /// <summary>
        /// 保存邮件备份状态
        /// </summary>
        /// <param name="relationMessages">关联信息集合</param>
        /// <returns></returns>                
        ///[OperationContract]
        void SaveBackUpMailState(OperationMessageRelation[] relationMessages);

        /// <summary>
        /// 删除本地缓存由于程序原因，影响数据不正常而导致批量上传失败的数据。
        /// </summary>
        ///[OperationContract]
        bool RemoveJunkOperationMessageRelationData();

        /// <summary>
        /// 重置异常数据
        /// </summary>
        ///[OperationContract]
        bool ResetExceptionData();
        #endregion

        #region Business
        /// <summary>
        /// 获取业务数据(部分列)：ID,No,RefNo
        /// </summary>
        /// <returns></returns>
        DataTable GetConciseOperationViewInfo();

        /// <summary>
        /// 获取所有本地缓存业务数据
        /// </summary>
        /// <returns></returns>
        ///[OperationContract]
        DataTable GetAllOperationViewInfo();

        /// <summary>
        /// 通过OperationID、OperationType获取本地缓存业务数据
        /// </summary>
        /// <param name="operationIDs">OperationID集合</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetOperationViewInfoByOperationIDs(Guid[] operationIDs);

        /// <summary>
        /// 获取缓存业务信息：配置文件组建SQL
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        ///[OperationContract]
        DataTable GetOperationViewList(BusinessQueryCriteria criteria);

        /// <summary>
        /// 查找业务集合：固定SQL
        /// </summary>
        ///[OperationContract]
        DataTable GetOperationViewListFixed(BusinessQueryCriteria criteria);

        /// <summary>
        /// 更新本地缓存业务数据
        /// </summary>
        /// <param name="operationIds"></param>
        /// <param name="operationType"></param>
        /// <param name="dt"></param>
        ///[OperationContract]
        void UpdateLocalBusinessData(List<Guid> operationIds, OperationType operationType, DataTable dt);

        /// <summary>
        /// 获取本地缓存业务数据
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        ///[OperationContract]
        DataTable GetOperationViewInfo(Guid operationID, OperationType type);

        /// <summary>
        /// 获取业务参与这相关信息
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        ///[OperationContract]
        DataTable GetOperationAssistantInfo(Guid operationID, OperationType operationType);

        /// <summary>
        /// 根据邮件主题匹配单号获取业务
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        ///[OperationContract]
        DataTable GetOperationViewListBySubjectKeyWord(BusinessQueryCriteria criteria);

        /// <summary>
        /// 判断业务是否在本地缓存存在
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        ///[OperationContract]
        bool IsShipmentExists(Guid operationId, OperationType operationType);

        /// <summary>
        /// 推送业务数据到Outlook
        /// </summary>
        /// <param name="operationIDs">业务ID集合</param>
        void PushBusinessDataToOutlook(Guid[] operationIDs);
        #endregion

        #region Operation Log

        /// <summary>
        /// 批量保存用户操作日志到数据库
        /// </summary>
        ///[OperationContract]
        void UploadUserOperationLog();

        ///// <summary>
        ///// 记录操作日志
        ///// </summary>
        ///// <param name="OperationTime">操作时间</param>
        ///// <param name="AssemblyNames">dll类</param>
        ///// <param name="FunctionName">操作内容</param>
        ///// <param name="Content">操作时长(ms)</param>
        /////[OperationContract]
        //void Add(DateTime OperationTime, string AssemblyNames, string FunctionName, string Content);

        ///// <summary>
        ///// 记录操作日志
        ///// </summary>
        ///// <param name="OperationTime">操作时间</param>
        ///// <param name="AssemblyNames">dll类</param>
        ///// <param name="ExecuteType">操作类型</param>
        ///// <param name="OperationContent">操作内容</param>
        ///// <param name="OperationDuration">操作时长(ms)</param>
        /////[OperationContract]
        //void AddOperationLog(DateTime OperationTime, string AssemblyNames, string ExecuteType, string OperationContent, string OperationDuration);

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="logID">日志ID</param>
        /// <param name="OperationTime">操作时间</param>
        /// <param name="AssemblyNames">dll类</param>
        /// <param name="ExecuteType">操作类型</param>
        /// <param name="OperationContent">操作内容</param>
        /// <param name="OperationDuration">操作时长(ms)</param>
        void AddOperationLog(Guid logID, DateTime OperationTime, string AssemblyNames, string ExecuteType, string OperationContent, string OperationDuration);

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="logID">日志ID</param>
        /// <param name="F1">操作内容(F1)</param>
        /// <param name="F2">操作内容2(F2)</param>
        /// <param name="F3">操作内容3(F3)</param>
        /// <param name="F4">是否待定(F4)</param>
        /// <param name="F5">F5</param>
        /// <param name="F6">操作时长[ms](F6))</param>
        /// <param name="F7">F7</param>
        /// <param name="F8">F8</param>
        /// <param name="F9">F9</param>
        /// <param name="F10">F10</param>
        ///[OperationContract]
        void UpdateOperationLog(Guid logID, string F1, string F2, string F3, bool F4, bool F5, string F6, string F7, string F8, string F9, string F10);

        /// <summary>
        /// 获取所有操作日志
        /// </summary>
        ///[OperationContract]
        DataTable GetOperationLog();

        /// <summary>
        /// 清除所有操作日志
        /// </summary>
        /// <param name="strWhere">Where 条件</param>
        ///[OperationContract]
        void ClearOperationLog(string strWhere);

        #endregion

        #region Other
        /// <summary>
        /// 读取本地文件：关联信息
        /// </summary>
        void ReadLocalFileOperationMessage();

        /// <summary>
        /// 读取本地文件：操作联系人
        /// </summary>
        void ReadLocalFileOperationContact();

        /// <summary>
        /// 返回协助同事列表信息
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="date">当前时间</param>
        /// <returns></returns>
        ///[OperationContract]
        List<Guid> GetUserAssistsList(Guid userId, DateTime date);

        /// <summary>
        /// 批量上传邮件备份到服务器
        /// </summary>
        ///[OperationContract]
        void UploadMailEntity4Batch();
        #endregion
    }
}
