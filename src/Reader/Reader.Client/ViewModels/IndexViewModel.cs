using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using Reader.Client.Events;
using Reader.Client.Extensions;
using Reader.Client.Interfaces;
using Reader.Client.Models;
using Reader.Client.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Reader.Client.ViewModels
{
    /// <summary>
    /// 首页模型
    /// </summary>
    public class IndexViewModel : BaseViewModel
    {
        #region 成员(Member)

        #region 书籍集合
        private ObservableCollection<BookModel> _Books;
        /// <summary>
        /// 书籍集合
        /// </summary>
        public ObservableCollection<BookModel> Books
        {
            get
            {
                return _Books;
            }
            set
            {
                _Books = value;
                RaisePropertyChanged(nameof(Books));
            }
        }
        #endregion

        #endregion

        #region 服务(Service)
        /// <summary>
        /// 书籍服务
        /// </summary>
        IBookService _BookService;
        #endregion

        #region 命令(Commands)
        /// <summary>
        /// 下载
        /// </summary>
        public DelegateCommand<BookModel> DownloadCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 首页模型
        /// </summary>
        /// <param name="containerProvider"></param>
        public IndexViewModel(IContainerProvider containerProvider) : base(containerProvider)
        {
            _BookService = containerProvider.Resolve<IBookService>();
            DownloadCommand = new DelegateCommand<BookModel>(Download);
            InitData();
        }
        #endregion

        #region 重写方法(Override)
        /// <summary>
        /// 是否可以处理请求的导航行为,当前视图/模型是否可以重用
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>true:</remarks>
        /// <returns></returns>
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        /// <summary>
        /// 从本页面转到其它页面时
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>NavigationContext包含目标页面的URI</remarks>
        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
        /// <summary>
        /// 从其它页面导航至本页面时
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>NavigationContext包含传递过来的参数</remarks>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 初始化界面数据
        /// </summary>
        void InitData()
        {
            Books = _BookService.GetAll();
        }

        /// <summary>
        /// 下载选中书籍
        /// </summary>
        /// <param name="model">书籍对象</param>
        private void Download(BookModel model)
        {
            try
            {
                NavigationToView("DownloadView", null);
                AddDownloadTask(model.Key, model.Name);
            }
            catch (Exception ex)
            {
                ShowError($"下载书籍出现异常:{ex.Message}");
            }
        }

        /// <summary>
        /// 添加下载任务
        /// </summary>
        /// <param name="taskKey">任务 Key</param>
        /// <param name="taskName">任务名称</param>
        private void AddDownloadTask(string taskKey, string taskName)
        {
            try
            {
                _EventAggregator.SendTask(new TaskModel() { Key = taskKey, Name = taskName });
            }
            catch (Exception ex)
            {
                ShowError($"添加下载任务出现异常:{ex.Message}");
            }
        }
        #endregion
    }
}
