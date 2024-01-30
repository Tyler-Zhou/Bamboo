using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.SubscriptionPublish.ServiceInterface;
using Microsoft.Practices.CompositeUI.UIElements;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 沟通公共模块入口类
    /// </summary>
    public class CommonModuleInit : ModuleInit
    {
        /// <summary>
        /// 根WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }
        /// <summary>
        /// 添加回调服务及回调客户端实现服务
        /// </summary>
        public override void AddServices()
        {
            IUIElementAdapterFactoryCatalog catalog = RootWorkItem.Services.Get<IUIElementAdapterFactoryCatalog>();
            catalog.RegisterFactory(new GridMenuUIAdapterFactory());
            this.RootWorkItem.Services.AddNew<BusinessOperationHandleService, IBusinessOperationHandleService>();
            this.RootWorkItem.Services.AddNew<BusinessOperationCallbackService, IBusinessOperationCallbackService>();
            this.RootWorkItem.Services.AddNew<MessageBoxService, IMessageBoxService>();
        }
        /// <summary>
        /// 注册回调服务，挂载应用程序退出事件，退订回调服务
        /// </summary>
        public override void Load()
        {
            base.Load();
            IBusinessOperationCallbackService callbackService =
                this.RootWorkItem.Services.Get<IBusinessOperationCallbackService>();
            IICPSubscriptionService subscriptionService =
                ServiceClient.GetService<IICPSubscriptionService>(callbackService);
            subscriptionService.Subscribe(string.Empty, new List<Guid>(), LocalData.ClientId);
            if (ICP.Framework.CommonLibrary.Client.LocalData.ApplicationType ==
                ICP.Framework.CommonLibrary.Common.ApplicationType.EmailCenter)
            {
                this.RootWorkItem.Services.Add<IICPSubscriptionService>(subscriptionService);
            }
            IMainForm frmMain = ServiceClient.GetClientService<IMainForm>();
            frmMain.ApplicationExit += new EventHandler(frmMain_ApplicationExit);
        }
        /// <summary>
        /// 退出时取消回调订阅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmMain_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
                if (ICP.Framework.CommonLibrary.Client.LocalData.ApplicationType == ICP.Framework.CommonLibrary.Common.ApplicationType.EmailCenter)
                {
                    IICPSubscriptionService subscriptionService = this.RootWorkItem.Services.Get<IICPSubscriptionService>();
                    subscriptionService.Unsubscribe(string.Empty);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
