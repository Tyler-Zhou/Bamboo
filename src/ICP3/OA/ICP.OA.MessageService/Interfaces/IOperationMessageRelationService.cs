using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Message.ServiceInterface
{

    /// <summary>
    ///操作日志服务类接口
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IOperationMessageRelationService
    {
        /// <summary>
        /// 保存消息关联操作日志
        /// </summary>
        /// <param name="message"></param>
        [OperationContract]
        ManyResult SaveOperationMailMessage(OperationMessageRelation[] relations);
        /// <summary>
        ///根据消息Id获取操作关联日志
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [OperationContract]
        List<OperationMessageRelation> Get(Guid imessageId);

        /// <summary>
        /// 根据消息的MessageId属性获取关联日志
        /// </summary>
        /// <param name="imessageId"></param>
        /// <returns></returns>
        [OperationContract]
        List<OperationMessageRelation> GetByMessageId(string messageId);
        /// <summary>
        /// 根据消息的MessageId和Reference属性获取邮件关联信息，以MessageId所找到的关联为主，Reference为辅
        /// </summary>
        /// <param name="messageId">消息的MessageId属性</param>
        /// <param name="reference">消息的Reference属性，其值为祖先的MessageId通过空格拼接</param>
        /// <returns></returns>
        [OperationContract]
        List<OperationMessageRelation> GetByMessageIdAndReference(string messageId, string reference);
        /// <summary>
        /// 判断是否存在关联
        /// </summary>
        /// <param name="messageId">消息的MessageID特性</param>
        /// <returns></returns>
        [OperationContract]
        bool ExistsRelation(string messageId);
        /// <summary>
        /// 判断关联是否发送更改,如发送更改则返回关联,如果关联已被删除，则返回的关联中HasData为false，否则返回null
        /// </summary>
        /// <param name="messageId">消息的MessageID特性</param>
        /// <param name="updateDate">消息的更改时间</param>
        /// <returns></returns>
        [OperationContract]
        OperationMessageRelation CheckRelationIsChanged(string messageId, DateTime? updateDate);


        /// <summary>
        /// 批量保存邮件与邮件的关联信息和保存邮件所有外部联系人与业务的关联信息
        /// </summary>
        /// <param name="messageRelations"></param>
        /// <returns></returns>
        [OperationContract]
        Guid[] SaveMessageRelations(byte[] bytes);

        /// <summary>
        /// 删除业务关联信息
        /// </summary>
        /// <param name="operationMessageIDs"></param>
        /// <returns></returns>
        [OperationContract]
        bool RemoveOperationMessagesByIDs(Guid[] operationMessageIDs, DateTime?[] updateDates);
    }
}
