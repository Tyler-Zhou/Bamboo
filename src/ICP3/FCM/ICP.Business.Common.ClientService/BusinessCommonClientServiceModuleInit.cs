using Microsoft.Practices.CompositeUI;
using ICP.Business.Common.ServiceInterface;
using ICP.Framework.CommonLibrary;

namespace ICP.Business.Common.ClientService
{
    /// <summary>
    /// FCM公共服务模块加载入口类
    /// </summary>
    public class BusinessCommonClientServiceModuleInit : ModuleInit
    {
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }
        /// <summary>
        /// 注入客户端服务
        /// </summary>
        public override void AddServices()
        {
            RootWorkItem.Services.AddNew<ICPCommonOperationService, IICPCommonOperationService>();
            RootWorkItem.Services.AddNew<SyncBusinessService, ISyncBusinessService>();
            RootWorkItem.Services.AddNew<MainCenterEmailTemplateGetter, IMainCenterEmailTemplateGetter>();
        }
    }
}
