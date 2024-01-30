using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Operation.Common.ServiceInterface
{   
    /// <summary>
    ///  FCM回调客户端功能实现类
    /// </summary>
    public class BusinessOperationHandleService : IBusinessOperationHandleService
    {
        public event EventHandler<CommonEventArgs<BusinessOperationParameter>> BusinessOperationCompleted;

        public void HandleBusinessOperation(BusinessOperationParameter paramter)
        {
            if (BusinessOperationCompleted != null)
            {
                BusinessOperationCompleted(this, new CommonEventArgs<BusinessOperationParameter>(paramter));
            }
        }


    }
}
