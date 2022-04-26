using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Bamboo.TabControlRegion.ViewModels
{

    /// <summary>
    /// 
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// 
        /// </summary>
        private IRegionManager _RegionManager;
        /// <summary>
        /// 
        /// </summary>
        public DelegateCommand<string> NavigateCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionManager"></param>
        public MainWindowViewModel(IRegionManager regionManager)
        {
            _RegionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(NavigateToTabPage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private void NavigateToTabPage(string path)
        {
            if (path != null)
                _RegionManager.RequestNavigate("TabControlRegion", path);
        }
    }
}
