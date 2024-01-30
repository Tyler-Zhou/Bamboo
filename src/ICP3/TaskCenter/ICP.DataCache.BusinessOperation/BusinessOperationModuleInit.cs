using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.CompositeUI;
using ICP.DataCache.ServiceInterface1;
using ICP.DataSynchronization.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Attributes;
using System.Threading;

namespace ICP.DataCache.BusinessOperation1
{
   public class BusinessOperationModuleInit:ModuleInit
    {
        /// <summary>
        /// 根WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }
      // [ServiceDependency]
       // public IClientCustomDataGridService ClientCustomDataGridService { get; set; }
        /// <summary>
        /// 注入客户端服务
        /// </summary>
        public override void AddServices()
        {
            RootWorkItem.Services.AddNew<ClientCommunicationHistoryService, IClientCommunicationHistoryService>();
            RootWorkItem.Services.AddNew<ClientFileService, IClientFileService>();
            //RootWorkItem.Services.AddNew<DocumentNotifyClientService, DocumentNotifyClientService>();
            RootWorkItem.Services.AddNew<DocumentNotifyService, IDocumentNotifyService>();
            RootWorkItem.Services.AddNew<ClientCustomDataGridService,IClientCustomDataGridService>();
            RootWorkItem.Services.AddNew<ClientBusinessContactService, IClientBusinessContactService>();
            //LocalServiceHost.RegisterComponent(typeof(ICPServiceHostAttribute), typeof(IClientCustomDataGridService));
            //LocalServiceHost.RegisterComponent(typeof(ICPServiceHostAttribute), typeof(IClientBusinessContactService));
        }

       
    }
}
