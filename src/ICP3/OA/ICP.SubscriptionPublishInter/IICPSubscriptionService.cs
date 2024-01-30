using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Operation.Common.ServiceInterface;

namespace ICP.SubscriptionPublish.ServiceInterface
{
    /// <summary>
    /// ICP回调服务订阅接口 
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IBusinessOperationCallbackService))]
    [ICPServiceHost]
    public interface IICPSubscriptionService : ISubscriptionService
    {

    }
}
