using FSClient.Interfaces;
using FSClient.ViewModels;
using FSClient.Views;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace FSClient
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        #region 成员(Member)
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
            _Logger.LogCritical($"{e.Exception.StackTrace},{e.Exception.Message}");
        }

        /// <summary>
        /// 未观察到的任务异常
        /// </summary>
        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            _Logger.LogCritical($"{e.Exception.StackTrace},{e.Exception.Message}");
        }
        /// <summary>
        /// 未处理的异常
        /// </summary>
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
                _Logger.LogCritical($"{ex.StackTrace},{ex.Message}");
        }
        #endregion

        #region 重写方法(Override)
        /// <summary>
        /// 创建外壳
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
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
            InitContent();
            base.OnInitialized();
        }

        /// <summary>
        /// 初始化内容
        /// </summary>
        bool InitContent()
        {
            var service = Current.MainWindow.DataContext as IWindowService;
            if (service != null)
            {
                service.DefaultView();
            }
            return true;
        }

        /// <summary>
        /// 注册 (视图、视图模型、服务)
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var factory = new NLogLoggerFactory();
            _Logger = factory.CreateLogger("FictionScrawler");
            //注入到Prism DI容器中
            containerRegistry.RegisterInstance(_Logger);
            //Service
            containerRegistry.Register<IWindowService, MainViewModel>();

            //View & ViewModel
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<QueryView, QueryViewModel>();

        }
        #endregion

        #region 方法(Method)
        #endregion
    }
}
