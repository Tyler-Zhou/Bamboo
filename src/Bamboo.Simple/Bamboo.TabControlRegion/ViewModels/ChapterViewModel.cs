using Prism.Regions;

namespace Bamboo.TabControlRegion.ViewModels
{
    public class ChapterViewModel : BaseViewModel
    {
        public ChapterViewModel(IRegionManager regionManager) : base(regionManager)
        {
            Title = "章节";
        }
    }
}
