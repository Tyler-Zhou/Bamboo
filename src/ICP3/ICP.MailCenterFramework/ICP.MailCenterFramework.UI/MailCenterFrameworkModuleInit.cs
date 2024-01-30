using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.MailCenter.ServiceInterface;
using ICP.MailCenter.UI;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;

namespace ICP.MailCenterFramework.UI
{
    public class MailCenterFrameworkModuleInit : ModuleInit
    {
        /// <summary>
        /// 根WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }

        public override void AddServices()
        {
            RootWorkItem.Services.AddNew<MailCenterTemplateService, IMailCenterTemplateService>();
            RootWorkItem.Services.AddNew<EmailTemplateGetter, IEmailTemplateGetter>();

            //ICP.MailCenter.Business.UI
            if (!RootWorkItem.Services.Contains<IDocumentNotifyService>())
            {
                RootWorkItem.Services.AddNew<DocumentNotifyService, IDocumentNotifyService>();
            }
            ServiceClient.GetService<IBusinessQueryService>();
            ServiceClient.GetService<ICP.FCM.OceanExport.ServiceInterface.IOceanExportService>();
            ServiceClient.GetService<IOperationMessageRelationService>();
        }
    }
}
