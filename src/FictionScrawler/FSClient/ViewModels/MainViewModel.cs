using FSClient.Common;
using FSClient.Extensions;
using FSClient.Interfaces;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace FSClient.ViewModels
{
    /// <summary>
    /// 主界面视图模型
    /// </summary>
    public class MainViewModel: BindableBase, IWindowService
    {
        #region 成员(Member)
        /// <summary>
        /// 应用程序标题
        /// </summary>
        public string ApplicationTitle
        {
            get
            {
                string title = "ApplicationTitle".FindResourceDictionary();
                string versionNo = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                return $"{title} V{versionNo}";
            }
        }

        private string _CurrentTime = "";
        /// <summary>
        /// 当前时间
        /// </summary>
        public string CurrentTime
        {
            get
            {
                return _CurrentTime;
            }
            set
            {
                _CurrentTime = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 方法集合
        /// </summary>
        private static List<Func<Task>> _Functions = new List<Func<Task>>();
        /// <summary>
        /// 时间计时器
        /// </summary>
        DispatcherTimer _DateTimeTimer = new DispatcherTimer();
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 日志服务
        /// </summary>
        ILogger _Logger;
        /// <summary>
        /// 区域管理器
        /// </summary>
        private readonly IRegionManager _RegionManager;
        #endregion

        #region 命令(Commands)
        /// <summary>
        /// 导航到视图
        /// </summary>
        public DelegateCommand<string> NavigateViewCommand { get; private set; }
        /// <summary>
        /// 退出程序
        /// </summary>
        public DelegateCommand ExitApplicationCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="regionManager"></param>
        /// <param name="settingService"></param>
        public MainViewModel(ILogger logger, IRegionManager regionManager)
        {
            _Logger = logger;
            _RegionManager = regionManager;
            NavigateViewCommand = new DelegateCommand<string>(NavigateView);
            ExitApplicationCommand = new DelegateCommand(ExitApplication);
            InitData();
        }
        #endregion

        #region 事件(Event)
        /// <summary>
        /// 
        /// </summary>
        private void DateTimeTimer_Tick(object sender, EventArgs e)
        {
            CurrentTime =$"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}  ";
        }

        #endregion

        #region 重写方法(Override)

        #endregion

        #region 方法(Method)
        /// <summary>
        /// 初始化界面数据
        /// </summary>
        private void InitData()
        {
            _DateTimeTimer.Interval = TimeSpan.FromSeconds(1);
            _DateTimeTimer.Tick += DateTimeTimer_Tick;
            _DateTimeTimer.Start();
        }
        /// <summary>
        /// 导航到视图
        /// </summary>
        /// <param name="viewName">视图名称</param>
        private void NavigateView(string viewName)
        {
            Navigate(viewName);
        }

        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="navigationParams">导航参数</param>
        void Navigate(string viewName, NavigationParameters navigationParams = null)
        {
            if (string.IsNullOrWhiteSpace(viewName))
                return;
            _RegionManager?.Regions[PrismConstant.MAIN_VIEW_REGION_NAME].RequestNavigate(viewName, back =>
            {
                if (back.Error != null)
                    _Logger.LogError(back.Error.Message);
            }, navigationParams);
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        public bool ClosingWindow()
        {
            return !Task.Run(() => ExecuteAllFunction().Result).Result;
        }
        /// <summary>
        /// 退出应用程序
        /// </summary>
        void ExitApplication()
        {
            var result = Task.Run(() => ExecuteAllFunction().Result).Result;
            Application.Current.Shutdown();
        }
        /// <summary>
        /// 执行所有方法
        /// </summary>
        /// <returns></returns>
        async Task<bool> ExecuteAllFunction()
        {
            if (_Functions != null && _Functions.Count > 0)
            {
                var functions = _Functions.Select(command => command());
                await Task.WhenAll(functions);

                return _Functions.Count > 0;
            }
            return true;
        }
        #endregion

        #region IWindowService
        /// <summary>
        /// 默认视图
        /// </summary>
        public void DefaultView()
        {
            Navigate("IndexView");
        }
        /// <summary>
        /// 导航到视图
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="navigationParams">导航参数</param>
        public void NavigationToView(string viewName, NavigationParameters navigationParams)
        {
            Navigate(viewName,navigationParams);
        }
        #endregion
    }
}
