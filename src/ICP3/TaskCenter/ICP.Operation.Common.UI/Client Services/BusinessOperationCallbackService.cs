using ICP.DataCache.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System.ServiceModel;

namespace ICP.Operation.Common.UI
{   
    /// <summary>
    /// 业务回调服务实现类
    /// </summary>
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BusinessOperationCallbackService : IBusinessOperationCallbackService
    {
        /// <summary>
        /// 业务回调客户端功能实现接口
        /// </summary>
        [ServiceDependency]
        public IBusinessOperationHandleService BusinessOperationHandleService { get; set; }

        /// <summary>
        /// 文档通知服务
        /// </summary>
        [ServiceDependency]
        public IDocumentNotifyService DocumentNotifyService { get; set; }

        #region IEmailCenterCallbackService 成员
        /// <summary>
        /// 转接给回调客户端实现类处理
        /// </summary>
        /// <param name="parameter">邮件业务关联参数</param>
        public void HandleBusinessOperation(BusinessOperationParameter parameter)
        {
            BusinessOperationHandleService.HandleBusinessOperation(parameter);
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="type">文档回调通知类型</param>
        /// <param name="data">数据</param>
        /// <param name="generateIds">生成ID列表</param>
        public void Upload(NotifyType type, object data, object generateIds)
        {
            DocumentNotifyService.Upload(type, data, generateIds);
        }

        #endregion
    }
}
