using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using System;

namespace ICP.Operation.Common.UI
{   
    /// <summary>
    /// 消息发送成功后回调处理实现类
    /// </summary>
    public class MessageSentCallbackService : IMessageSentCallbackService
    {  
        /// <summary>
        /// 消息发送成功后,引发此事件
        /// </summary>
        public event EventHandler<CommonEventArgs<MessageSentParameter>> MessageSent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        public void HandleMessageSent(Guid operationId, OperationType operationType)
        {
            if (MessageSent != null)
            {  
                MessageSentParameter parameter=new MessageSentParameter { OperationId=operationId,OperationType=operationType };
                MessageSent(this, new CommonEventArgs<MessageSentParameter>(parameter));
            }
        }
    }
}
