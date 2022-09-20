using Prism.Regions;

namespace FSClient.Interfaces
{
    /// <summary>
    /// 主窗体服务
    /// </summary>
    public interface IWindowService
    {
        /// <summary>
        /// 默认视图
        /// </summary>
        void DefaultView();

        /// <summary>
        /// 导航到视图
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="navigationParams">导航参数</param>
        void NavigationToView(string viewName, NavigationParameters navigationParams);
    }
}
