using Microsoft.Practices.CompositeUI;
using ICP.Message.ServiceInterface;
namespace ICP.Message.Client
{
    public class MessageModuleInit : Microsoft.Practices.CompositeUI.ModuleInit
    {
        #region init
        Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;


        public MessageModuleInit([ServiceDependency]WorkItem rootWorkItem)
        {
            _rootWorkItem = rootWorkItem;
           
        }

        public override void AddServices()
        {
            _rootWorkItem.Services.AddNew<ClientMessageService, IClientMessageService>();
            //_rootWorkItem.Services.AddNew<ClientMessageService, IClientMessageService>();
            _rootWorkItem.Services.AddNew<MessageNotifyClientService, IMessageNotifyService>();

            base.AddServices();
        }

        #endregion

   
    }
}
