using Prism.Regions;

namespace Reader.Client.Interfaces
{
    /// <summary>
    /// 应用程序服务
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// 导航到视图
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="navigationParams">导航参数</param>
        void NavigationToView(string viewName, NavigationParameters navigationParams=null);
    }
}
