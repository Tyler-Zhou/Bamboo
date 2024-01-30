using Microsoft.Practices.CompositeUI;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.DataCache.BusinessOperation
{
    public class BusinessOperationModuleInit : ModuleInit
    {
        /// <summary>
        /// 根WorkItem
        /// </summary>

        public WorkItem RootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }
        // [ServiceDependency]
        // public IClientCustomDataGridService ClientCustomDataGridService { get; set; }
        /// <summary>
        /// 注入客户端服务
        /// </summary>
        public override void AddServices()
        {
            RootWorkItem.Services.AddNew<ClientBusinessContactService, IClientBusinessContactService>();
            RootWorkItem.Services.AddNew<ClientBusinessOperationService, IClientBusinessOperationService>();
            RootWorkItem.Services.AddNew<ClientCommunicationHistoryService, IClientCommunicationHistoryService>();
            RootWorkItem.Services.AddNew<ClientFileService, IClientFileService>();
            if (LocalData.ApplicationType == ICP.Framework.CommonLibrary.Common.ApplicationType.ICP)
            {
               if (!RootWorkItem.Services.Contains<IDocumentNotifyService>())
               {
                   RootWorkItem.Services.AddNew<DocumentNotifyService, IDocumentNotifyService>();
               }
            }
           // RootWorkItem.Services.AddNew<ClientCustomDataGridService, IClientCustomDataGridService>();
            RootWorkItem.Services.AddNew<ClientFrameworkInitializeService, IClientFrameworkInitializeService>();
        }
    }
}
