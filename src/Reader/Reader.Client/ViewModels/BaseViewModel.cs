using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Reader.Client.Extensions;
using Reader.Client.Interfaces;
using Reader.Client.Models;

namespace Reader.Client.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseViewModel : BindableBase, INavigationAware
    {
        #region 成员(Member)
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 容器提供者(DryICO)
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
        #endregion

        #region 命令(Command)
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 基本视图模型
        /// </summary>
        /// <param name="containerProvider"></param>
        public BaseViewModel(IContainerProvider containerProvider)
        {
            ContainerProvider = containerProvider;
            _ApplicationService = ContainerProvider.Resolve<IApplicationService>();
            _EventAggregator = ContainerProvider.Resolve<IEventAggregator>();
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
            _ApplicationService.NavigationToView(viewName, navigationParams);
        }

        /// <summary>
        /// 正在加载
        /// </summary>
        /// <param name="IsOpen">是否打开</param>
        public void ShowLoading(bool IsOpen)
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
        public void ShowInformation(string message)
        {
            _EventAggregator.ShowMessage(new TipsInfo() {Content = message, Type = EnumTipsType.Information });
        }

        /// <summary>
        /// 显示警告信息
        /// </summary>
        /// <param name="message">消息</param>
        public void ShowWarning(string message)
        {
            _EventAggregator.ShowMessage(new TipsInfo() { Content = message, Type = EnumTipsType.Warning });
        }

        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="message">消息</param>
        public void ShowError(string message)
        {
            _EventAggregator.ShowMessage(new TipsInfo() { Content = message, Type = EnumTipsType.Error });
        }
        #endregion
    }
}
