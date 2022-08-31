using Client.Common;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Client.ViewModels
{
    public class MainViewModel: BindableBase, IConfigureService, INavigationService
    {
        #region 成员(Member)

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
        public MainViewModel(ILogger logger,IRegionManager regionManager, ISettingService settingService)
        {
            _Logger= logger;
            _RegionManager = regionManager;
            _SettingService= settingService;
            NewGameCommand = new DelegateCommand(NewGame);
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
        /// <param name="viewName">视图名称</param>
        void Navigate(MenuBar menuBar)
        {
            if (menuBar==null || string.IsNullOrWhiteSpace(menuBar.ViewName))
                return;
            _RegionManager?.Regions[PrismConstant.MAIN_VIEW_REGION_NAME].RequestNavigate(menuBar.ViewName, back =>
            {
                if(back.Error!=null)
                    _Logger.LogError(back.Error.Message);
            },menuBar.NavigationParams);
        }
        /// <summary>
        /// 新游戏
        /// </summary>
        private void NewGame()
        {
            Navigate(new MenuBar {ViewName= "NewGameView" });
        }
        /// <summary>
        /// 退出游戏
        /// </summary>
        private void ExitGame()
        {
            Application.Current.Shutdown();
        }
        private void SwitchLanguage(string culture)
        {
            ConfigLanguage(culture);
        }
        /// <summary>
        /// 配置主窗体内容项
        /// </summary>
        public void ConfigureContent()
        {
            Navigate(new MenuBar() {ViewName = "GameView" });
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

            _SettingService.SaveAsync("Setting",ApplicationContext.Setting);
            ExitGame();
        }

        public void NavigationToView(string viewName, NavigationParameters navigationParams)
        {
            Navigate(new MenuBar { ViewName = viewName, NavigationParams = navigationParams });
        }
        #endregion
    }
}
