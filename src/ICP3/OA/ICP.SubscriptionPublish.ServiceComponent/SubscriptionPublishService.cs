using ICP.SubscriptionPublish.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.SubscriptionPublish.ServiceComponent
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class SubscriptionPublishService : SubscriptionManager<ISubscriptionPublishNotifyService>, ISubscriptionPublishService
    {

    }
}
