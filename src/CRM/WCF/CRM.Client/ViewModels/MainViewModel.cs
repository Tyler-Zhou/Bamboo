using CRM.Client.Common;
using CRM.Client.Extensions;
using CRM.Client.Interfaces;
using CRM.Client.Models;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace CRM.Client.ViewModels
{
    /// <summary>
    /// 主界面视图模型
    /// </summary>
    public class MainViewModel : BindableBase, INavigationService, IWindowService
    {
        #region 成员(Member)
        /// <summary>
        /// 应用程序标题
        /// </summary>
        public string ApplicationTitle
        {
            get
            {
                string title = "ApplicationTitle".FindResourceDictionary();
                string versionNo = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                return $"{title} V{versionNo}";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private bool? _DialogResult;
        /// <summary>
        /// 弹窗结果
        /// </summary>
        public bool? DialogResult
        {
            get { return _DialogResult; }
            set
            {
                _DialogResult = value;
                RaisePropertyChanged();
            }
        }
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

        #region 命令(Commands)
        /// <summary>
        /// 
        /// </summary>
        public DelegateCommand ExitCommand { get; private set; }
        /// <summary>
        /// 客户管理
        /// </summary>
        public DelegateCommand<string> NavigationCommand { get; private set; }

        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="regionManager"></param>
        /// <param name="settingService"></param>
        public MainViewModel(ILogger logger, IRegionManager regionManager)
        {
            _Logger = logger;
            _RegionManager = regionManager;
            NavigationCommand = new DelegateCommand<string>(NavigationToView);
            ExitCommand = new DelegateCommand(ExitApp);
        }
        #endregion

        #region 重写方法(Override)

        #endregion

        #region 方法(Method)
        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="menuBar">菜单对象</param>
        void Navigate(MenuBar menuBar)
        {
            if (menuBar == null || string.IsNullOrWhiteSpace(menuBar.ViewName))
                return;
            _RegionManager?.Regions[PrismConstant.MAIN_VIEW_REGION_NAME].RequestNavigate(menuBar.ViewName, back =>
            {
                if (back.Error != null)
                    _Logger.LogError(back.Error.Message);
            }, menuBar.NavigationParams);
        }
        
        /// <summary>
        /// 关闭窗体
        /// </summary>
        public bool ClosingWindow()
        {
            return !Task.Run(() => ExecuteAllFunction().Result).Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        private void NavigationToView(string viewName)
        {
            Navigate(new MenuBar { ViewName = viewName });
        }

        /// <summary>
        /// 退出
        /// </summary>
        void ExitApp()
        {
            var result = Task.Run(() => ExecuteAllFunction().Result).Result;
            Application.Current.Shutdown();
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
        #endregion

        #region INavigationService
        /// <summary>
        /// 导航到视图
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="navigationParams"></param>
        public void NavigationToView(string viewName, NavigationParameters navigationParams)
        {
            Navigate(new MenuBar { ViewName = viewName, NavigationParams = navigationParams });
        }
        #endregion

        #region IWindowService
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
