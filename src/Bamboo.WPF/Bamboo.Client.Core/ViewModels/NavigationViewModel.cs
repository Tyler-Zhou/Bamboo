using Bamboo.Client.Core.Extensions;
using Bamboo.Client.Core.Interface;
using Bamboo.Client.Core.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace Bamboo.Client.Core.ViewModels
{
    /// <summary>
    /// 导航视图模型
    /// </summary>
    public class NavigationViewModel : BindableBase, INavigationAware
    {
        #region 成员(Member)
        #region (TabItem)头文本
        /// <summary>
        /// HeaderText
        /// </summary>
        private string _HeaderText;
        /// <summary>
        /// (TabItem)头文本
        /// </summary>
        public string HeaderText
        {
            get
            {
                return _HeaderText;
            }
            set
            {
                _HeaderText = value;
                RaisePropertyChanged();
            }
        }
        #endregion 
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 容器提供者(DryICO)
        /// </summary>
        private readonly IContainerProvider _ContainerProvider;
        /// <summary>
        /// 事件聚合器
        /// </summary>
        public readonly IEventAggregator _EventAggregator;
        /// <summary>
        /// 导航服务
        /// </summary>
        public readonly INavigationService _NavigationService;
        /// <summary>
        /// 
        /// </summary>
        IRegionManager _RegionManager;
        #endregion

        #region 命令(Command)
        /// <summary>
        /// 关闭Tab Item
        /// </summary>
        public DelegateCommand<object> CloseTabCommand { get; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 导航视图模型
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="containerProvider"></param>
        public NavigationViewModel(IRegionManager regionManager,IContainerProvider containerProvider)
        {
            _RegionManager = regionManager;
            _ContainerProvider = containerProvider;
            _EventAggregator = containerProvider.Resolve<IEventAggregator>();
            _NavigationService = containerProvider.Resolve<INavigationService>();
            CloseTabCommand = new DelegateCommand<object>(OnExecuteCloseCommand);
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 是否可以处理请求的导航行为,当前视图/模型是否可以重用
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>true:</remarks>
        /// <returns></returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        /// <summary>
        /// 从本页面转到其它页面时
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>NavigationContext包含目标页面的URI</remarks>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        /// <summary>
        /// 从其它页面导航至本页面时
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>NavigationContext包含传递过来的参数</remarks>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
        /// <summary>
        /// 导航到视图
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="navigationParams">导航参数</param>
        public virtual void NavigationToView(string viewName, NavigationParameters navigationParams)
        {
            _NavigationService.NavigationToView(viewName, navigationParams);
        }
        /// <summary>
        /// 正在加载
        /// </summary>
        /// <param name="IsOpen">是否打开</param>
        public void UpdateLoading(bool IsOpen)
        {
            _EventAggregator.UpdateLoading(new LoadingModel()
            {
                IsOpen = IsOpen
            });
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息</param>
        public void SendMessage(string message)
        {
            _EventAggregator.SendMessage(message);
        }

        private void OnExecuteCloseCommand(object tabItem)
        {
            _RegionManager.Regions["MainViewRegion"].Remove(tabItem);
        }
        #endregion
    }
}
