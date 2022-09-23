using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Reader.Client.Common;
using Reader.Client.Extensions;
using Reader.Client.Interfaces;
using Reader.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Reader.Client.ViewModels
{
    /// <summary>
    /// 主界面视图模型
    /// </summary>
    public class MainViewModel : BindableBase
    {
        #region 成员(Member)

        #region 应用程序标题
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
        #endregion

        /// <summary>
        /// 方法集合
        /// </summary>
        private static List<Func<Task>> _Functions = new List<Func<Task>>();

        #endregion

        #region 服务(Service)
        /// <summary>
        /// 容器提供者(DryICO)
        /// </summary>
        public readonly IContainerProvider ContainerProvider;
        /// <summary>
        /// 应用程序服务
        /// </summary>
        public readonly IApplicationService _ApplicationService;
        #endregion

        #region 命令(Commands)
        /// <summary>
        /// 导航到视图
        /// </summary>
        public DelegateCommand<string> NavigateViewCommand { get; private set; }
        /// <summary>
        /// 退出程序
        /// </summary>
        public DelegateCommand ExitApplicationCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerProvider">容器提供者(DryICO)</param>
        public MainViewModel(IContainerProvider containerProvider)
        {
            ContainerProvider = containerProvider;
            _ApplicationService = ContainerProvider.Resolve<IApplicationService>();
            NavigateViewCommand = new DelegateCommand<string>(NavigateView);
            ExitApplicationCommand = new DelegateCommand(ExitApplication);
            InitData();
        }
        #endregion

        #region 重写方法(Override)

        #endregion

        #region 方法(Method)
        /// <summary>
        /// 初始化界面数据
        /// </summary>
        private void InitData()
        {
           
        }
        /// <summary>
        /// 导航到视图
        /// </summary>
        /// <param name="viewName">视图名称</param>
        private void NavigateView(string viewName)
        {
            _ApplicationService.NavigationToView(viewName);
        }
        
        /// <summary>
        /// 关闭窗体
        /// </summary>
        public bool ClosingWindow()
        {
            return !Task.Run(() => ExecuteAllFunction().Result).Result;
        }
        /// <summary>
        /// 退出应用程序
        /// </summary>
        void ExitApplication()
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
    }
}
