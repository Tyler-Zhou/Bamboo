using ICP.DataCache.ServiceInterface;
using ICP.MailCenter.ServiceInterface;
using ICP.MailCenter.UI;
using Microsoft.Practices.CompositeUI;

namespace ICP.DataCache.LocalOperation
{
    public class LocalOperationModuleInit : ModuleInit
    {
        /// <summary>
        /// 根WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }

        /// <summary>
        /// 注入客户端服务
        /// </summary>
        public override void AddServices()
        {
            RootWorkItem.Services.AddNew<OutlookService, IOutLookService>();
            RootWorkItem.Services.AddNew<DataCacheOperationService, IDataCacheOperationService>();
            RootWorkItem.Services.AddNew<LocalBusinessCacheDataOperation, ILocalBusinessCacheDataOperation>();
            RootWorkItem.Services.AddNew<LocalBusinessContactService, ILocalBusinessContactService>();
        }
    }
}
