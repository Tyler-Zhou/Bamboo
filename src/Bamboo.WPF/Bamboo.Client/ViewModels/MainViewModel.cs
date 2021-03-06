using Bamboo.Client.Core.Common;
using Bamboo.Client.Core.Interface;
using Bamboo.Client.Extensions;
using Bamboo.Client.Interface;
using Bamboo.Client.Models;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Bamboo.Client.ViewModels
{
    /// <summary>
    /// 主窗体视图模型
    /// </summary>
    public class MainViewModel : BindableBase, IConfigureService, INavigationService
    {
        #region 用户名
        /// <summary>
        /// UserName
        /// </summary>
        private string _UserName = "";
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 菜单集合
        /// <summary>
        /// MenuBars
        /// </summary>
        private ObservableCollection<MenuBar> _MenuBars = new ObservableCollection<MenuBar>();
        /// <summary>
        /// 菜单集合
        /// </summary>
        public ObservableCollection<MenuBar> MenuBars
        {
            get { return _MenuBars; }
            set
            {
                _MenuBars = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 容器提供服务
        /// </summary>
        private readonly IContainerProvider _ContainerProvider;
        /// <summary>
        /// 区域管理器
        /// </summary>
        private readonly IRegionManager _RegionManager;
        /// <summary>
        /// 区域导航日志服务
        /// </summary>
        private IRegionNavigationJournal _NavigationJournal;
        /// <summary>
        /// 调色板帮助类
        /// </summary>
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        #endregion

        #region 命令(Command)
        /// <summary>
        /// 菜单导航命令
        /// </summary>
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        /// <summary>
        /// 前一个展示区域
        /// </summary>
        public DelegateCommand MovePrevCommand { get; private set; }
        /// <summary>
        /// 后一个展示区域
        /// </summary>
        public DelegateCommand MoveNextCommand { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public DelegateCommand LoginOutCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 主窗体视图模型
        /// </summary>
        /// <param name="containerProvider"></param>
        /// <param name="regionManager"></param>
        public MainViewModel(IContainerProvider containerProvider, IRegionManager regionManager)
        {
            _ContainerProvider = containerProvider;
            _RegionManager = regionManager;
            MenuBars = new ObservableCollection<MenuBar>();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            MovePrevCommand = new DelegateCommand(() => { if (_NavigationJournal != null && _NavigationJournal.CanGoBack) _NavigationJournal.GoBack(); });
            MoveNextCommand = new DelegateCommand(() => { if (_NavigationJournal != null && _NavigationJournal.CanGoForward) _NavigationJournal.GoForward(); });
            LoginOutCommand = new DelegateCommand(() =>
            {
                //注销当前用户
                App.LoginOut(_ContainerProvider);
            });
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 配置主窗体内容项
        /// </summary>
        public void ConfigureContent()
        {
            UserName = ApplicationContext.UserName;
            CreateMenuBar();
            Navigate(new MenuBar() { Icon = "Home", Title = "首页", NameSpace = "IndexView" });
        }

        /// <summary>
        /// 配置主题
        /// </summary>
        public void ConfigureTheme()
        {
            ModifyTheme(theme => theme.SetBaseTheme(ApplicationContext.IsDarkTheme ? Theme.Dark : Theme.Light));
        }

        /// <summary>
        /// 修改主题
        /// </summary>
        /// <param name="modificationAction"></param>
        private void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            modificationAction?.Invoke(theme);
            paletteHelper.SetTheme(theme);
        }

        /// <summary>
        /// 配置画板颜色
        /// </summary>
        public void ConfigureHueColor()
        {
            Color hue = (Color)ColorConverter.ConvertFromString(ApplicationContext.HueColor);
            ITheme theme = paletteHelper.GetTheme();
            theme.PrimaryLight = new ColorPair(hue.Lighten());
            theme.PrimaryMid = new ColorPair(hue);
            theme.PrimaryDark = new ColorPair(hue.Darken());
            paletteHelper.SetTheme(theme);
        }

        /// <summary>
        /// 导航到视图
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="navigationParams">导航参数</param>
        public void NavigationToView(string viewName, NavigationParameters navigationParams)
        {
            Navigate(new MenuBar { NameSpace = viewName, NavigationParams = navigationParams });
        }
        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="obj">菜单对象</param>
        void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;
            _RegionManager?.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, back =>
            {
                _NavigationJournal = back.Context.NavigationService.Journal;
            }, obj.NavigationParams);
        }
        /// <summary>
        /// 设置菜单项
        /// </summary>
        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "首页", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "Library", Title = "图书馆", NameSpace = "BookView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });
        }
        #endregion
    }
}
