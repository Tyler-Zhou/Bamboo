using Prism.Regions;
using System.Threading.Tasks;
using System;

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

        /// <summary>
        /// 添加方法
        /// </summary>
        /// <param name="func"></param>
        void AddFunction(Func<Task> func);

        /// <summary>
        /// 退出应用程序
        /// </summary>
        bool ExitApplication();
    }
}
