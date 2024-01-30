using System.ServiceModel;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Message.ServiceInterface
{
    [ServiceContract]
    public interface IBusinessMessageService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SingleResultData Resend(Message message);
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [OperationContract]
        void Open(Message message);

    }
}
