#region Comment

/*
 * 
 * FileName:    DataCacheOperationService.cs
 * CreatedOn:   2015/5/21 16:02:43
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 提供给外界调用的公共服务接口
    /// </summary>
    [ICPServiceHost]
    [ServiceContract]
    public interface IDataCacheOperationService
    {
        #region Outlook Plugin
        /// <summary>
        /// 获取所有联系人
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetAllContact();

        /// <summary>
        /// 获取邮件关联信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetAllOperationMessageRelation();

        /// <summary>
        /// 通过OperationID、OperationType获取本地缓存业务数据
        /// </summary>
        /// <param name="operationIDs">OperationID集合</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetOperationViewInfoByOperationIDs(Guid[] operationIDs);

        /// <summary>
        /// 获取所有本地缓存业务数据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetAllOperationViewInfo();


        /// <summary>
        /// 获取本地缓存邮件关联信息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        [OperationContract]
        List<OperationMessageRelation> GetLocalOperationMessageRelationByMessageIdAndReference(string messageId, string reference);

        /// <summary>
        /// 获取邮件关联信息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        [OperationContract]
        List<OperationMessageRelation> GetOperationMessageRelationByMessageIdAndReference(string messageId,
            string reference);
        /// <summary>
        /// 根据邮件ID，获取关联信息，返回Datatable
        /// </summary>
        /// <param name="messageID">邮件MessageID</param>
        /// <returns>DataTable</returns>
        [OperationContract]
        DataTable GetOperationMessageRelationDataTableByMessageID(string messageID);

        /// <summary>
        /// 根据邮件ID，获取关联信息，返回单个对象
        /// </summary>
        /// <param name="messageID">邮件MessageID</param>
        /// <returns>OperationMessageRelation</returns>
        [OperationContract]
        OperationMessageRelation GetOperationMessageRelationByMessageID(string messageID);


        /// <summary>
        /// 查找业务集合：固定SQL
        /// </summary>
        /// <param name="criteria">业务查询信息实体类</param>
        /// <rereturns>DataTable</rereturns>
        [OperationContract]
        DataTable GetOperationViewListFixed(BusinessQueryCriteria criteria);

        /// <summary>
        /// 联系人是否存在缓存
        /// </summary>
        /// <param name="ExternalEmails">联系人集合</param>
        /// <returns>是否存在:True存在；False不存在；</returns>
        [OperationContract]
        bool IsAllContactExsitCache(List<string> ExternalEmails);

        /// <summary>
        /// 保存本地缓存邮件关联业务信息
        /// </summary>
        /// <param name="relationMessages"></param>
        /// <returns></returns>
        [OperationContract]
        void SaveLocalOperationMessageRelation(OperationMessageRelation[] relationMessages);

        /// <summary>
        /// 保存邮件与业务的关联信息
        /// </summary>
        /// <param name="relationMessages"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveOperationMessageRelation")]
        ManyResult SaveOperationMessageRelation(OperationMessageRelation[] relationMessages);

        /// <summary>
        /// 保存联系人
        /// </summary>
        /// <param name="ContactParameters">联系人集合</param>
        [OperationContract]
        void SaveLocalOperationContactMail(List<OperationContactParameters> ContactParameters);

        /// <summary>
        /// 删除服务端数据库关联信息和本地缓存关联信息
        /// </summary>
        /// <param name="messageRelationIds">关联信息ID集合</param>
        /// <param name="updateDates">更新时间</param>
        [OperationContract]
        void RemoveAndSyncOperationMessageRelations(Guid[] messageRelationIds, DateTime?[] updateDates);

        /// <summary>
        /// 根据邮件地址，获取联系人类型
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns>如果本地不存在，则返回null</returns>
        [OperationContract]
        int? GetContactPersonType(string emailAddress);

        /// <summary>
        /// 关联的方法
        /// </summary>
        /// <param name="contactType">联系人类型</param>
        /// <param name="bussOperationContexts">需要关联的业务集合</param>
        /// <param name="message">Message对象</param>
        [OperationContract]
        void SaveContactList(ContactType contactType
            , List<BusinessOperationContext> bussOperationContexts, Message.ServiceInterface.Message message);
        /// <summary>
        /// 写入操作日志
        /// </summary>
        /// <param name="AssemblyNames">组件名称</param>
        /// <param name="ExecuteType">操作类型</param>
        /// <param name="ExecuteDescription">操作内容</param>
        /// <param name="Content">操作时长(ms)</param>
        [OperationContract]
        void WriteStopwatchLog(string AssemblyNames,string ExecuteType, string ExecuteDescription, string Content);
        #endregion
    }
}
