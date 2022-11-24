using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Chapter0201.Contracts
{
    /// <summary>
    /// 处理订单服务
    /// </summary>
    [ServiceContract(Namespace = "http://www.tyler.com/ProcessOrder")]
    public interface IProcessOrder
    {
        [OperationContract(Action = "urn:SubmitOrder")]
        void SubmitOrder(Message order);
    }
}
