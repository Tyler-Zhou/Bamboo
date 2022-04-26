using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Bamboo.TabControlRegion.ViewModels
{
    public class BaseViewModel: BindableBase
    {
        private string _Title;

        public string Title
        {
            get 
            { 
                return _Title; 
            }
            set 
            { 
                _Title = value;
                RaisePropertyChanged();
            }
        }

        IRegionManager _RegionManager;

        public DelegateCommand<object> CloseTabCommand { get; }

        public BaseViewModel(IRegionManager regionManager)
        {
            _RegionManager = regionManager;
            CloseTabCommand = new DelegateCommand<object>(OnExecuteCloseCommand);
        }

        private void OnExecuteCloseCommand(object tabItem)
        {
            _RegionManager.Regions["TabControlRegion"].Remove(tabItem);
        }
    }
}
