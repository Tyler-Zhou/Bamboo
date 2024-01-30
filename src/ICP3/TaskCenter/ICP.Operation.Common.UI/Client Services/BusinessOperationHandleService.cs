using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using System;

namespace ICP.Operation.Common.UI
{   
    /// <summary>
    ///  FCM回调客户端功能实现类
    /// </summary>
    public class BusinessOperationHandleService : IBusinessOperationHandleService
    {
        /// <summary>
        /// 业务操作完成
        /// </summary>
        public event EventHandler<CommonEventArgs<BusinessOperationParameter>> BusinessOperationCompleted;

        /// <summary>
        /// 转接给回调客户端实现类处理
        /// </summary>
        /// <param name="paramter"></param>
        public void HandleBusinessOperation(BusinessOperationParameter paramter)
        {
            if (BusinessOperationCompleted != null)
            {
                BusinessOperationCompleted(this, new CommonEventArgs<BusinessOperationParameter>(paramter));
            }
        }


    }
}
