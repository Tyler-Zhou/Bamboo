using Client.Common;
using Client.Interfaces;
using Client.Models;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    /// <summary>
    /// 主界面视图模型
    /// </summary>
    public class MainViewModel : BindableBase, IConfigureService, INavigationService, IWindowService
    {
        #region 成员(Member)
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
        /// <summary>
        /// 设置服务
        /// </summary>
        private readonly ISettingService _SettingService;

        #endregion

        #region 命令(Commands)
        /// <summary>
        /// 新游戏
        /// </summary>
        public DelegateCommand NewGameCommand { get; private set; }
        /// <summary>
        /// 选择存档
        /// </summary>
        public DelegateCommand SelectArchiveCommand { get; private set; }
        /// <summary>
        /// 选择语言
        /// </summary>
        public DelegateCommand<string> SwitchLanguageCommand { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public DelegateCommand ExitGameCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="regionManager"></param>
        /// <param name="settingService"></param>
        public MainViewModel(ILogger logger, IRegionManager regionManager, ISettingService settingService)
        {
            _Logger = logger;
            _RegionManager = regionManager;
            _SettingService = settingService;
            NewGameCommand = new DelegateCommand(NewGame);
            SelectArchiveCommand = new DelegateCommand(SelectArchive);
            SwitchLanguageCommand = new DelegateCommand<string>(SwitchLanguage);
            ExitGameCommand = new DelegateCommand(ExitGame);
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
        /// 新游戏
        /// </summary>
        private void NewGame()
        {
            Navigate(new MenuBar { ViewName = "NewGameView" });
        }
        /// <summary>
        /// 选择存档
        /// </summary>
        private void SelectArchive()
        {
            Navigate(new MenuBar { ViewName = "ArchiveView" });
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        public bool ClosingWindow()
        {
            return !Task.Run(() => ExecuteAllFunction().Result).Result;
        }
        /// <summary>
        /// 退出游戏
        /// </summary>
        void ExitGame()
        {
            var result = Task.Run(() => ExecuteAllFunction().Result).Result;
            Application.Current.Shutdown();
        }
        private void SwitchLanguage(string culture)
        {
            ConfigLanguage(culture);
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

        #region IConfigureService
        /// <summary>
        /// 配置主窗体内容项
        /// </summary>
        public void ConfigureContent()
        {
            //Navigate(new MenuBar() { ViewName = "GameView" });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <remarks>摘自：https://www.cnblogs.com/horan/archive/2012/04/20/wpf-multilanguage.html</remarks>
        public void ConfigLanguage(string culture)
        {
            if (ApplicationContext.Setting.CultureName.Equals(culture))
                return;
            ApplicationContext.Setting.CultureName = culture;

            _SettingService.SaveAsync("Setting", ApplicationContext.Setting);
            ExitGame();
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
