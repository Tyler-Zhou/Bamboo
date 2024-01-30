using System;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Client;
using System.Windows.Forms;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.Commands;
using DevExpress.XtraEditors.Repository;
using Microsoft.Practices.CompositeUI.Services;
using ICP.MailCenter.ServiceInterface;
using ICP.Framework.ClientComponents.Service;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.UIManagement;

namespace ICPMailCenter
{
    /// <summary>
    /// 主程序入口
    /// </summary>
    public class MainApplication : CABDevExpress.XtraFormApplicationBase<WorkItem, frmMailCenter>, IServiceContainer
    {
        /// <summary>
        /// 启动应用程序
        /// <remarks>所做事项：
        /// 1.从ICP主程序获取登录用户信息(LocalData)
        /// 2.设置皮肤
        /// </remarks>
        /// </summary>
        protected override void Start()
        {
            SetAppSkinAndFont();
            Application.Run(Shell); // 在当前线程上开始运行标准应用程序消息循环，并使指定窗体可见。
        }
        /// <summary>
        /// 设置应用程序皮肤主题
        /// </summary>
        private static void SetAppSkinAndFont()
        {
            DevExpress.UserSkins.OfficeSkins.Register();
            if (!DevExpress.Skins.SkinManager.AllowFormSkins)
                DevExpress.Skins.SkinManager.EnableFormSkins();

            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(LocalData.SkinName);//ICP皮肤样式
        }  
      

        protected override void AddBuilderStrategies(Microsoft.Practices.ObjectBuilder.Builder builder)
        {

            //添加远程事件连接的Strategy
            base.AddBuilderStrategies(builder);
            builder.Strategies.Add(new ServiceProxyStrategy(), BuilderStage.PreCreation);
            builder.Strategies.Add(PermissionStrategy.Instance, BuilderStage.PostInitialization);

        }
        protected override void AfterShellCreated()
        {
            base.AfterShellCreated();

            AddClientService();

            ICommandAdapterMapService mapService = ServiceCollection.Get<ICommandAdapterMapService>();
            mapService.Register(typeof(RepositoryItem), typeof(RepositoryItemCommandAdapter));

 
            RootWorkItem.UIExtensionSites.RegisterSite("statusbar", Shell.status);

        

            ICP.Framework.ClientComponents.Controls.TabbedMdiWorkspace wp = new ICP.Framework.ClientComponents.Controls.TabbedMdiWorkspace(Shell);
            
            wp.SmartPartActivated += this.OnSmartPartActivated;

            wp.SmartPartClosing += this.OnSmartPartClosing;

            wp.SmartPartClosed += this.OnSmartPartClosed;


            RootWorkItem.Workspaces.Add(wp, "MainWorkspace");

            Shell.ErrorListControl.CurrentOwner = Shell;
            LocalCommonServices.ErrorTrace = (IErrorTraceService)Shell.ErrorListControl;// 错误提示服务 用于在窗体下方的DockPanel里面显示多条异常信息
            LocalCommonServices.Statusbar = (IStatusbar)Shell;  // 状态栏服务

            ServiceCollection.Remove<IModuleEnumerator>();
            ServiceCollection.Remove<IAuthenticationService>();

            ModuleEnumerator cp = new ModuleEnumerator();// 邮件中心模块加载器

            ServiceCollection.Add<IModuleEnumerator>(cp);
          
            Shell.Activate();//frmMailCenter窗体  激活窗体并给予它焦点
            LocalData.MainFormHandle = this.Shell.Handle;//获取控件绑定到的窗口句柄
        }
        private void OnSmartPartActivated(object sender, WorkspaceEventArgs e)
        {
            Control control = (Control)e.SmartPart;
            if (control != null)
            {

                control = control.FindForm();//检索控件所在的窗体
                Shell.ErrorListControl.CurrentOwner = control;
                Shell.ErrorListControl.FilterAll(control);
            }
        }
        private void OnSmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            Control control = (Control)e.SmartPart;

            if (control != null)
            {
                Shell.ErrorListControl.RemoveAll(control);

            }
        }
        private void OnSmartPartClosed(object sender, WorkspaceEventArgs e)
        {
            Control control = (Control)e.SmartPart;
            control.Dispose();
            control = null;
            this.Shell.PerformLayout();
        }
        void AddClientService()
        {
            ServiceCollection.Add<IMainForm>(Shell);
            ServiceClient.AddClientService(typeof(IServiceContainer), this);
            ServiceCollection.AddNew<DataUIManageService, IDataUIManageService>();
            ServiceCollection.Add<IErrorTraceService>(Shell.ErrorListControl);
            ServiceCollection.AddNew<MailCenterManageService, IMailCenterManageService>();
            ServiceCollection.AddNew<ICP.Framework.ClientComponents.UIFramework.DefaultUIBuilder, ICP.Framework.ClientComponents.UIFramework.IUIBuilder>();
            ServiceCollection.AddNew<DefaultDataFinderFactory, IDataFinderFactory>();
            ServiceCollection.AddNew<DataFindClientService, IDataFindClientService>();
            LocalCommonServices.ErrorTrace = Shell.ErrorListControl;

        }
        #region IServiceContainer 成员

        public Microsoft.Practices.CompositeUI.Collections.ServiceCollection ServiceCollection
        {
            get { return RootWorkItem.Services; }
        }

        T IServiceContainer.GetService<T>()
        {
            return (T)ServiceCollection.Get(typeof(T));
        }

        object IServiceContainer.GetService(Type type)
        {
            return ServiceCollection.Get(type);
        }

        #endregion
    }
}
