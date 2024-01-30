using Prism.Regions;

namespace CRM.Client.Interfaces
{
    /// <summary>
    /// 导航服务
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// 导航到视图
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="navigationParams">导航参数</param>
        void NavigationToView(string viewName, NavigationParameters navigationParams);
    }
}
