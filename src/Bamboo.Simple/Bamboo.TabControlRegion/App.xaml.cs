using Bamboo.TabControlRegion.ViewModels;
using Bamboo.TabControlRegion.Views;
using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using System.Windows;

namespace Bamboo.TabControlRegion
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<BookView, BookViewModel>();
            containerRegistry.RegisterForNavigation<ChapterView, ChapterViewModel>();
            containerRegistry.RegisterForNavigation<ChapterEditView, ChapterEditViewModel>();
        }


    }
}
