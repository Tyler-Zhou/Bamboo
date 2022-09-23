using Microsoft.Extensions.Logging;
using Prism.Regions;
using Reader.Client.Common;
using Reader.Client.Interfaces;

namespace Reader.Client.Services
{
    /// <summary>
    /// 应用程序服务
    /// </summary>
    public class ApplicationService: IApplicationService
    {
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

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="regionManager"></param>
        public ApplicationService(ILogger logger, IRegionManager regionManager)
        {
            _Logger = logger;
            _RegionManager = regionManager;
        }
        #endregion

        #region 方法(Method)
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
        #endregion

        #region IApplicationService
        /// <summary>
        /// 导航到视图
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="navigationParams">导航参数</param>
        public void NavigationToView(string viewName, NavigationParameters navigationParams=null)
        {
            Navigate(viewName, navigationParams);
        }
        #endregion
    }
}
