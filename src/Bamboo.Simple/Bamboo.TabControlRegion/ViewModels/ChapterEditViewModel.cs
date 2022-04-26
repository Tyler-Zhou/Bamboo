using Prism.Regions;

namespace Bamboo.TabControlRegion.ViewModels
{
    public class ChapterEditViewModel : BaseViewModel
    {
        public ChapterEditViewModel(IRegionManager regionManager) : base(regionManager)
        {
            Title = "编辑章节";
        }
    }
}
