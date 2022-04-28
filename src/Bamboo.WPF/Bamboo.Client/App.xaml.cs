using Bamboo.Client.Core.Common;
using Bamboo.Client.Core.Helper;
using Bamboo.Client.Core.Interface;
using Bamboo.Client.Interface;
using Bamboo.Client.Service;
using Bamboo.Client.ViewModels;
using Bamboo.Client.Views;
using DryIoc;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Services.Dialogs;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Bamboo.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        #region 成员(Member)
        /// <summary>
        /// 
        /// </summary>
        Mutex mutex;
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 日志服务
        /// </summary>
        ILogger _Logger;
        #endregion

        #region 事件(Event)
        /// <summary>
        /// 调度程序未处理的异常
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //通常全局异常捕捉的都是致命信息
            _Logger.LogCritical($"{ e.Exception.StackTrace },{ e.Exception.Message }");
        }

        /// <summary>
        /// 未观察到的任务异常
        /// </summary>
        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            _Logger.LogCritical($"{ e.Exception.StackTrace },{ e.Exception.Message }");
        }
        /// <summary>
        /// 未处理的异常
        /// </summary>
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
                _Logger.LogCritical($"{ ex.StackTrace },{ ex.Message }");

            //记录dump文件
            DumpHelper.TryDump($"dumps\\Bamboo_{ DateTime.Now.ToString("HH-mm-ss-ms") }.dmp");
        }
        #endregion

        #region 重写方法(Override)
        /// <summary>
        /// 创建外壳
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            bool createNew;
            mutex = new Mutex(true, "Bamboo", out createNew);
            if (!createNew)
                Environment.Exit(0);
            //UI线程未捕获异常处理事件
            DispatcherUnhandledException += OnDispatcherUnhandledException;
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
            //多线程异常
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            return Container.Resolve<MainView>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void OnInitialized()
        {
            #region 获取配置信息
            ApplicationContext.Account = Configure.Current.GetValue("Account");
            ApplicationContext.IsDarkTheme = Configure.Current.GetValue<bool>("IsDarkTheme", "True");
            ApplicationContext.HueColor = Configure.Current.GetValue("HueColor", "#FF424242");
            ApplicationContext.DefaultPageSize = Configure.Current.GetValue<int>("DefaultPageSize", "20");
            #endregion

            var dialog = Container.Resolve<IDialogService>();

            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }



                var service = Current.MainWindow.DataContext as IConfigureService;
                if (service != null)
                {
                    service.ConfigureContent();
                    service.ConfigureTheme();
                    service.ConfigureHueColor();
                }
                base.OnInitialized();
            });
        }
        /// <summary>
        /// 加载模块
        /// </summary>
        /// <returns></returns>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            //指定模块加载方式为从文件夹中以反射发现并加载module(推荐用法)
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }
        /// <summary>
        /// 注册 (视图、视图模型、服务)
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var factory = new NLogLoggerFactory();
            _Logger = factory.CreateLogger("Bamboo");
            //注入到Prism DI容器中
            containerRegistry.RegisterInstance(_Logger);

            //serviceKey
            containerRegistry.GetContainer()
                .Register<ClientService>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));
            containerRegistry.GetContainer().RegisterInstance(@"http://localhost:5031/", serviceKey: "webUrl");
            //Service
            containerRegistry.Register<IConfigureService, MainViewModel>();
            containerRegistry.Register<INavigationService, MainViewModel>();
            containerRegistry.Register<IAuthenticationService, AuthenticationService>();
            containerRegistry.Register<IDialogHostService, DialogHostService>();

            //View & ViewModel
            containerRegistry.RegisterForNavigation<MsgView, MsgViewModel>();
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterDialog<LoginView, LoginViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<AboutView, AboutViewModel>();

        }
        #endregion

        #region 方法(Method)

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="containerProvider">容器服务</param>
        public static void LoginOut(IContainerProvider containerProvider)
        {
            Current.MainWindow.Hide();

            var dialog = containerProvider.Resolve<IDialogService>();

            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }

                Current.MainWindow.Show();
            });
        }

        #endregion
    }
}
