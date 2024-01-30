using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 消息发送成功后回调处理接口
    /// </summary>
   public interface IMessageSentCallbackService
    {
        /// <summary>
        /// 消息发送成功后,引发此事件
        /// </summary>
        event EventHandler<CommonEventArgs<MessageSentParameter>> MessageSent;
        /// <summary>
        /// 消息发送成功后，调用邮件中心和任务中心相关联处理动作
        /// </summary>
        ///<param name="parameter"></param>
        void HandleMessageSent(Guid operationId,OperationType operationType);
    }
    /// <summary>
    /// 消息发送回调参数类
    /// </summary>
    public class MessageSentParameter
    {
        public Guid OperationId { get; set; }
        public OperationType OperationType { get; set; }
    }
}
