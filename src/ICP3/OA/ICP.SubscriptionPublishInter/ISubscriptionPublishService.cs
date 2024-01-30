using System.ServiceModel;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.SubscriptionPublish.ServiceInterface
{
    /// <summary>
    /// 订阅接口 声明通信契约
    /// </summary>
    [ServiceContract(CallbackContract = typeof(ISubscriptionPublishNotifyService))]
    public interface ISubscriptionPublishService : ISubscriptionService
    {
    }
}
