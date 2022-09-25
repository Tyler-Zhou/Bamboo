using Microsoft.Extensions.Logging;
using Prism.Regions;
using Reader.Client.Common;
using Reader.Client.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Reader.Client.Services
{
    /// <summary>
    /// 应用程序服务
    /// </summary>
    public class ApplicationService: IApplicationService
    {
        #region 成员(Member)
        /// <summary>
        /// 方法集合
        /// </summary>
        private static List<Func<Task>> _Functions = new List<Func<Task>>();
        #endregion

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

        /// <summary>
        /// 退出应用程序
        /// </summary>
        public bool ExitApplication()
        {
            return Task.Run(() => ExecuteAllFunction().Result).Result;
        }

        /// <summary>
        /// 执行所有方法
        /// </summary>
        /// <returns></returns>
        async Task<bool> ExecuteAllFunction()
        {
            if (_Functions != null && _Functions.Count > 0)
            {
                var functions = _Functions.Select(command => command());
                await Task.WhenAll(functions);

                return _Functions.Count > 0;
            }
            return true;
        }

        /// <summary>
        /// 添加方法
        /// </summary>
        /// <param name="func"></param>
        public void AddFunction(Func<Task> func)
        {
            _Functions.Add(func);
        }

        #endregion
    }
}
