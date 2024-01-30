using CRM.Client.Interfaces;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Regions;

namespace CRM.Client.ViewModels
{
    /// <summary>
    /// 客户管理
    /// </summary>
    public class CustomerViewModel: BaseViewModel
    {
        #region 成员(Member)
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 日志服务
        /// </summary>
        ILogger _Logger;
        #endregion

        #region 命令(Command)

        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="cacheService"></param>
        /// <param name="windowService">主窗体服务</param>
        /// <param name="logger"></param>
        public CustomerViewModel(IContainerProvider provider, ILogger logger) : base(provider)
        {
            _Logger = logger;
            InitData();
        }
        #endregion

        #region 事件(Event)

        #endregion

        #region 重写方法(Override)
        /// <summary>
        /// 是否可以处理请求的导航行为,当前视图/模型是否可以重用
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>true:</remarks>
        /// <returns></returns>
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        /// <summary>
        /// 从本页面转到其它页面时
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>NavigationContext包含目标页面的URI</remarks>
        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
        /// <summary>
        /// 从其它页面导航至本页面时
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>NavigationContext包含传递过来的参数</remarks>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 初始化数据
        /// </summary>
        void InitData()
        {
            
        }
        #endregion

        #region 异步方法(Async Method)

        #endregion
    }
}
