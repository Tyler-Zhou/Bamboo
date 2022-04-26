using Prism.Mvvm;

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

    }
}
