using System;
using System.Collections.Generic;
using System.Data;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 客户端业务操作接口
    /// </summary>
    public interface IClientBusinessOperationService : IClientBusinessContactService
    {     /// <summary>
        /// 获取缓存业务信息：配置文件组建SQL
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="isKeyWordQuery"></param>
        /// <returns></returns>
        DataTable GetLocalOperationViewList(BusinessQueryCriteria criteria);

        /// <summary>
        /// 查找业务集合：固定SQL
        /// </summary>
        DataTable GetOperationViewListFixed(BusinessQueryCriteria criteria);
        /// <summary>
        /// 获取本地消息与业务关联信息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        List<OperationMessageRelation> GetLocalOperationMessageRelationByMessageIdAndReference(string messageId, string reference);
        /// <summary>
        /// 获取消息与业务关联信息，如果本地获取不到，则从服务端获取
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        List<OperationMessageRelation> GetOperationMessageRelationByMessageIdAndReference(string messageId, string reference);
        /// <summary>
        /// 获取本地业务业务关联信息
        /// </summary>
        /// <param name="operationIDs"></param>
        /// <returns></returns>
        List<OperationMessageRelation> GetOperationMessages(string messageID, Guid[] operationIDs);
        /// <summary>
        /// 获取缓存业务信息和邮件关联信息
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="messageID"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        BusinessQueryResult Get(BusinessQueryCriteria criteria, string messageID, string reference);

        /// <summary>
        /// 保存邮件关联业务信息
        /// </summary>
        /// <param name="relationMessage"></param>
        /// <returns></returns>
        ManyResult SaveOperationMessageRelation(OperationMessageRelation[] relationMessages);
        /// <summary>
        /// 根据MessageID删除邮件与业务的关联信息
        /// </summary>
        /// <param name="messageIDs"></param>
        void RemoveOperationMessageRelation(List<string> messageIDs);

        /// <summary>
        /// 主键ID去删除关联信息
        /// </summary>
        void RemoveAndSyncOperationMessageRelations(Guid[] messageRelationIds, DateTime?[] updateDates);

        /// <summary>
        /// 更新本地缓存业务数据
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <param name="dt"></param>
        void UpdateLocalBusinessData(Guid operationId, OperationType operationType, DataTable dt);
        /// <summary>
        /// 批量更新缓存业务数据
        /// </summary>
        /// <param name="operationIds"></param>
        /// <param name="operationType"></param>
        /// <param name="dt"></param>
        void UpdateLocalBusinessData(List<Guid> operationIds, OperationType operationType, DataTable dt);
        /// <summary>
        /// 批量更新本地数据
        /// </summary>
        /// <param name="operationIds"></param>
        /// <param name="operationType"></param>
        void UpdateLocalBusinessData(List<Guid> operationIds, OperationType operationType);

        /// <summary>
        /// 更新本地缓存业务数据
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        void UpdateLocalBusinessData(Guid operationId, OperationType operationType);
        /// <summary>
        /// 获取本地缓存业务数据
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        DataTable GetLocalOperationViewInfo(Guid operationId, OperationType operationType);

        /// <summary>
        /// 获取业务参与者相关信息
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        DataTable GetOperationAssistantInfo(Guid operationID, OperationType operationType);
        /// <summary>
        /// 清空业务联系人邮件地址
        /// </summary>
        void ClearOperationContactEMail();
        /// <summary>
        /// 根据邮件主题从本地缓存获取业务信息
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        DataTable GetLocalOperationViewListBySubjectKeyWord(BusinessQueryCriteria criteria);


        /// <summary>
        /// 判断业务是否在本地缓存存在
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        bool IsShipmentExists(Guid operationId, OperationType operationType);

        /// <summary>
        /// 判断是否存在关联
        /// </summary>
        /// <param name="messageId">消息的MessageID特性</param>
        /// <returns></returns>
        bool ExistsRelation(string messageId);


        /// <summary>
        /// 判断关联是否发送更改,如发送更改则返回关联,如果关联已被删除，则返回的关联中HasData为false，否则返回null
        /// </summary>
        /// <param name="messageId">消息的MessageID特性</param>
        /// <param name="updateDate">消息的更改时间</param>
        /// <returns></returns>
        OperationMessageRelation CheckRelationIsChanged(string messageId, DateTime? updateDate);

        /// <summary>
        /// 保存本地缓存邮件关联业务信息
        /// </summary>
        /// <param name="operationRelation"></param>
        /// <returns></returns>
        void SaveLocalOperationMessageRelation(OperationMessageRelation[] operationRelations);

        /// <summary>
        /// 保存消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        ManyResult[] SaveMessage(Message.ServiceInterface.Message message);

        /// <summary>
        /// 保存关联，如果关联不存在，则保存消息
        /// </summary>
        /// <param name="relationMessage"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        List<OperationMessageRelation> EnsureAndSaveMailMessageReference(OperationMessageRelation relationMessage,
                                                       Message.ServiceInterface.Message message);
        /// <summary>
        /// 本地关联缓存表是否存在该邮件的关联信息
        /// </summary>
        /// <param name="messageID"></param>
        /// <returns></returns>
        bool HasLocalOperationMessageRelation(string messageID);
        /// <summary>
        /// 根据邮件MessageID获取邮件邮件关联信息
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        OperationMessageRelation GetOperationMessageRelationByMessageID(string messageId);
        /// <summary>
        /// 保存邮件业务联系人
        /// </summary>
        /// <param name="ContactParameters"></param>
        void SaveLocalOperationContactMail(List<OperationContactParameters> ContactParameters);
        /// <summary>
        /// 联系人是否存在缓存
        /// </summary>
        /// <param name="ExternalEmails"></param>
        /// <returns></returns>
        bool IsAllContactExsitCache(List<string> ExternalEmails);
        /// <summary>
        /// 根据邮件ID，获取关联信息，返回Datatable
        /// </summary>
        DataTable GetOperationMessageRelationByMessageId(string messageId);

        /// <summary>
        /// 根据邮件地址，获取联系人类型
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns>如果本地不存在，则返回null</returns>
        int? GetContactPersonType(string emailAddress);
    }
}
