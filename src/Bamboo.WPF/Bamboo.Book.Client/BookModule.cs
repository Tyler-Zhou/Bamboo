using Bamboo.Book.Client.Interface;
using Bamboo.Book.Client.Service;
using Bamboo.Book.Client.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace Bamboo.Book.Client
{
    /// <summary>
    /// 书籍模块
    /// </summary>
    [Module(ModuleName = "Book")]
    public class BookModule : IModule
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