using Client.Common;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using Client.ViewModels;
using Client.Views;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Client
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
            base.OnInitialized();
            var service = Current.MainWindow.DataContext as IConfigureService;
            if (service != null)
            {
                service.ConfigureContent();
            }
            var result = Task.Run(() => InitSetting().Result).Result;
        }

        /// <summary>
        /// 
        /// </summary>
        async Task<bool> InitSetting()
        {
            var settingService = Container.Resolve<ISettingService>();
            if (await settingService.GetAsync<ApplicationSetting>("Setting") == null)
            {
                await settingService.SaveAsync("Setting", new ApplicationSetting() { CultureName = Thread.CurrentThread.CurrentUICulture.Name });
            }
            ApplicationContext.Setting = await settingService.GetAsync<ApplicationSetting>("Setting");

            //设置资源,摘自：https://www.cnblogs.com/horan/archive/2012/04/20/wpf-multilanguage.html
            List<ResourceDictionary> dictionaryList = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Current.Resources.MergedDictionaries)
            {
                dictionaryList.Add(dictionary);
            }
            string requestedCulture = string.Format(@"Resources\StringResource.{0}.xaml", ApplicationContext.Setting.CultureName);
            ResourceDictionary resourceDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedCulture));
            if (resourceDictionary == null)
            {
                requestedCulture = @"Resources\StringResource.xaml";
                resourceDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedCulture));
            }
            if (resourceDictionary != null)
            {
                Current.Resources.MergedDictionaries.Remove(resourceDictionary);
                Current.Resources.MergedDictionaries.Add(resourceDictionary);
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
            _Logger = factory.CreateLogger("ProgressQuest");
            //注入到Prism DI容器中
            containerRegistry.RegisterInstance(_Logger);
            //Service
            containerRegistry.Register<IConfigureService, MainViewModel>();
            containerRegistry.RegisterSingleton<IWindowService, MainViewModel>();
            containerRegistry.Register<INavigationService, MainViewModel>();
            containerRegistry.Register<ISettingService, SettingService>();
            containerRegistry.Register<ICacheService, CacheService>();

            //View & ViewModel
            containerRegistry.RegisterForNavigation<NewGameView, NewGameViewModel>();
            containerRegistry.RegisterForNavigation<GameView, GameViewModel>();
            containerRegistry.RegisterForNavigation<ArchiveView, ArchiveViewModel>();
        }
        #endregion

        #region 方法(Method)
        #endregion
    }
}
