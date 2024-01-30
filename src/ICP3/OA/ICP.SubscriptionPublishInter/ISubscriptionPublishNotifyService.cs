using System;
using System.Collections.Generic;
using System.ServiceModel;
using ICP.Message.ServiceInterface;
using ICP.DataCache.ServiceInterface;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.FileSystem.ServiceInterface;

namespace ICP.SubscriptionPublish.ServiceInterface
{
    /// <summary>
    /// 契约指定回调方法
    /// </summary>
    [ServiceKnownType(typeof(DocumentInfo[]))]
    [ServiceKnownType(typeof(ManyResult))]
    [ServiceKnownType(typeof(SingleResult))]
    [ServiceKnownType(typeof(List<SingleResult>))]
    [ServiceKnownType(typeof(Guid[]))]
    [ServiceContract]
    public interface ISubscriptionPublishNotifyService
    {   
        /// <summary>
        /// 消息状态更改回调
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="type"></param>
        [OperationContract(IsOneWay = true)]
        void ChangeState(ICP.Message.ServiceInterface.Message[] messages, MessageType type);
        /// <summary>
        /// 通知公告发布
        /// <param name="data">公共信息</param>
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void Notify(BulletinData data);
        /// <summary>
        /// 文档上传状态更改服务
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <param name="generateIds"></param>
        [OperationContract(IsOneWay = true)]
        void Upload(NotifyType type, object data, object generateIds);
        /// <summary>
        /// 消息发送后回调刷新用户界面
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        [OperationContract(IsOneWay = true)]
        void SendMessage(Guid[] operationIds, OperationType[] operationTypes);

    }
}
