using Bamboo.Library.Client.Interface;
using Bamboo.Library.Client.Service;
using Bamboo.Library.Client.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace Bamboo.Library.Client
{
    /// <summary>
    /// 图书馆模块
    /// </summary>
    [Module(ModuleName = "Library")]
    public class LibraryModule : IModule
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="containerProvider"></param>
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
        /// <summary>
        /// 注入 服务、视图/视图模型
        /// </summary>
        /// <param name="containerRegistry"></param>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Service
            containerRegistry.Register<IBookService, BookService>();
            //View & ViewModel
            containerRegistry.RegisterForNavigation<BookView>();
        }
    }
}