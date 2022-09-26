using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Reader.Client.Common;
using Reader.Client.Events;
using Reader.Client.Extensions;
using Reader.Client.Interfaces;
using Reader.Client.Services;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Reader.Client.ViewModels
{
    /// <summary>
    /// 主界面视图模型
    /// </summary>
    public class MainViewModel : BindableBase
    {
        #region 成员(Member)

        #region 应用程序标题
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
        #endregion

        #endregion

        #region 服务(Service)
        /// <summary>
        /// 容器提供者(DryIOC)
        /// </summary>
        public readonly IContainerProvider ContainerProvider;
        /// <summary>
        /// 事件聚合器
        /// </summary>
        public readonly IEventAggregator _EventAggregator;
        /// <summary>
        /// 应用程序服务
        /// </summary>
        public readonly IApplicationService _ApplicationService;
        /// <summary>
        /// 设置服务
        /// </summary>
        ISettingService _SettingService;
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
        /// <param name="containerProvider">容器提供者(DryIOC)</param>
        public MainViewModel(IContainerProvider containerProvider)
        {
            ContainerProvider = containerProvider;
            _EventAggregator = ContainerProvider.Resolve<IEventAggregator>();
            _ApplicationService = ContainerProvider.Resolve<IApplicationService>();
            _SettingService = ContainerProvider.Resolve<ISettingService>();

            NavigateViewCommand = new DelegateCommand<string>(NavigateView);
            ExitApplicationCommand = new DelegateCommand(ExitApplication);
            InitData();
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
            _ApplicationService.AddFunction(SaveSetting);
        }
        /// <summary>
        /// 导航到视图
        /// </summary>
        /// <param name="viewName">视图名称</param>
        private void NavigateView(string viewName)
        {
            _ApplicationService.NavigationToView(viewName);
        }
        
        /// <summary>
        /// 关闭窗体
        /// </summary>
        public bool ClosingWindow()
        {
            return !_ApplicationService.ExitApplication();
        }
        /// <summary>
        /// 退出应用程序
        /// </summary>
        void ExitApplication()
        {
            if(_ApplicationService.ExitApplication())
            {
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        async Task<bool> SaveSetting()
        {
            return await _SettingService.SaveAsync("\\", "Setting", ReaderContext.Setting);
        }

        /// <summary>
        /// 正在加载
        /// </summary>
        /// <param name="IsOpen">是否打开</param>
        void ShowLoading(bool IsOpen)
        {
            _EventAggregator.ShowLoading(new LoadingModel()
            {
                IsOpen = IsOpen
            });
        }

        /// <summary>
        /// 显示普通信息
        /// </summary>
        /// <param name="message">消息</param>
        void ShowInformation(string message)
        {
            _EventAggregator.ShowMessage(new TipsInfo() { Content = message, Type = EnumTipsType.Information });
        }

        /// <summary>
        /// 显示警告信息
        /// </summary>
        /// <param name="message">消息</param>
        void ShowWarning(string message)
        {
            _EventAggregator.ShowMessage(new TipsInfo() { Content = message, Type = EnumTipsType.Warning });
        }

        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="message">消息</param>
        void ShowError(string message)
        {
            _EventAggregator.ShowMessage(new TipsInfo() { Content = message, Type = EnumTipsType.Error });
        }
        #endregion
    }
}
