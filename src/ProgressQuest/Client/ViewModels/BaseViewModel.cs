using Client.Interfaces;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace Client.ViewModels
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
        private readonly IContainerProvider _ContainerProvider;
        /// <summary>
        /// 导航服务
        /// </summary>
        public readonly INavigationService _NavigationService;
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
            _ContainerProvider = containerProvider;
            _NavigationService = _ContainerProvider.Resolve<INavigationService>();
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
        #endregion
    }
}
