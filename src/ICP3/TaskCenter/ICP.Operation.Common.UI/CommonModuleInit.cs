using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using ICP.SubscriptionPublish.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.UIElements;
using System;
using System.Collections.Generic;

namespace ICP.Operation.Common.UI
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
            RootWorkItem.Services.AddNew<BusinessOperationHandleService, IBusinessOperationHandleService>();
            RootWorkItem.Services.AddNew<BusinessOperationCallbackService, IBusinessOperationCallbackService>();
            //RootWorkItem.Services.AddNew<MessageBoxService, IMessageBoxService>();
            RootWorkItem.Services.AddNew<MessageSentCallbackService, IMessageSentCallbackService>();
        }
        /// <summary>
        /// 注册回调服务，挂载应用程序退出事件，退订回调服务
        /// </summary>
        public override void Load()
        {
            base.Load();
            IBusinessOperationCallbackService callbackService =
                RootWorkItem.Services.Get<IBusinessOperationCallbackService>();
            IICPSubscriptionService subscriptionService =
                ServiceClient.GetService<IICPSubscriptionService>(callbackService);
            subscriptionService.Subscribe(string.Empty, new List<Guid>(), LocalData.ClientId);
            if (LocalData.ApplicationType ==
                ApplicationType.EmailCenter)
            {
                RootWorkItem.Services.Add<IICPSubscriptionService>(subscriptionService);
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
                if (LocalData.ApplicationType == ApplicationType.EmailCenter)
                {
                    IICPSubscriptionService subscriptionService = RootWorkItem.Services.Get<IICPSubscriptionService>();
                    subscriptionService.Unsubscribe(string.Empty);
                }

            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
    }
}
