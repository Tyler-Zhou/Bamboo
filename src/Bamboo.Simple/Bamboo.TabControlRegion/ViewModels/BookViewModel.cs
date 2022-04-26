using Prism.Commands;
using Prism.Regions;

namespace Bamboo.TabControlRegion.ViewModels
{
    public class BookViewModel: BaseViewModel
    {

        public BookViewModel(IRegionManager regionManager):base(regionManager)
        {
            Title = "书籍";
        }

        
    }
}
