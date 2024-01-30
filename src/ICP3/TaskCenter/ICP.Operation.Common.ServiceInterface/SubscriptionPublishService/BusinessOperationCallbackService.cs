using System.ServiceModel;
using ICP.DataCache.ServiceInterface;
using Microsoft.Practices.CompositeUI;

namespace ICP.Operation.Common.ServiceInterface
{   
    /// <summary>
    /// 业务回调服务实现类
    /// </summary>
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BusinessOperationCallbackService : IBusinessOperationCallbackService
    {
        [ServiceDependency]
        public IBusinessOperationHandleService BusinessOperationHandleService { get; set; }

        [ServiceDependency]
        public IDocumentNotifyService DocumentNotifyService { get; set; }



        #region IEmailCenterCallbackService 成员
        /// <summary>
        /// 转接给回调客户端实现类处理
        /// </summary>
        /// <param name="parameter"></param>
        public void HandleBusinessOperation(BusinessOperationParameter parameter)
        {
            BusinessOperationHandleService.HandleBusinessOperation(parameter);
        }

        public void Upload(NotifyType type, object data, object generateIds)
        {
            DocumentNotifyService.Upload(type, data, generateIds);
        }

        #endregion
    }
}
