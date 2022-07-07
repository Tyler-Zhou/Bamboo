using Bamboo.Client.Core.ViewModels;
using Bamboo.Client.Extensions;
using Bamboo.Client.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace Bamboo.Client.ViewModels
{
    /// <summary>
    /// 设置视图模型
    /// </summary>
    public class SettingsViewModel : NavigationViewModel
    {
        #region 菜单
        /// <summary>
        /// MenuBars
        /// </summary>
        private ObservableCollection<MenuBar> _MenuBars = new ObservableCollection<MenuBar>();
        /// <summary>
        /// 菜单
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

        #region 服务
        /// <summary>
        /// 区域管理器
        /// </summary>
        private readonly IRegionManager _RegionManager;
        #endregion

        #region 命令
        /// <summary>
        /// 导航
        /// </summary>
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="provider"></param>
        public SettingsViewModel(IRegionManager regionManager, IContainerProvider provider) : base(regionManager,provider)
        {
            HeaderText = "设置";
            MenuBars = new ObservableCollection<MenuBar>();
            _RegionManager = regionManager;
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            CreateMenuBar();
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="obj"></param>
        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;

            _RegionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate(obj.NameSpace);
        }
        /// <summary>
        /// 菜单项
        /// </summary>
        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Palette", Title = "个性化", NameSpace = "SkinView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "系统设置", NameSpace = "" });
            MenuBars.Add(new MenuBar() { Icon = "Information", Title = "关于更多", NameSpace = "AboutView" });
        }
        #endregion
    }
}
