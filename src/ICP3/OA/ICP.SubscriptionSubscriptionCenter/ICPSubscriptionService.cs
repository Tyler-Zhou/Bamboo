using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;
using ICP.Operation.Common.ServiceInterface;
using ICP.SubscriptionPublish.ServiceInterface;

namespace ICP.SubscriptionPublish.Client
{  
    /// <summary>
    /// 
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ICPSubscriptionService : SubscriptionManager<IBusinessOperationCallbackService>, IICPSubscriptionService
    {

    }
}
