using System.ServiceModel;

namespace ICP.Message.ServiceInterface
{
    [ServiceContract]
   public interface IMessageNotifyService
    {
        [OperationContract(IsOneWay = true)]
        void ChangeState(Message[] messages, MessageType type);
    }
}
