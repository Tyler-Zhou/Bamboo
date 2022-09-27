using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using Reader.Client.Events;
using Reader.Client.Extensions;
using Reader.Client.Interfaces;
using Reader.Client.Models;
using Reader.Client.Spider;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Reader.Client.ViewModels
{
    /// <summary>
    /// 首页模型
    /// </summary>
    public class IndexViewModel : BaseViewModel
    {
        #region 成员(Member)

        #region 当前书籍
        private BookModel _CurrentBook;
        /// <summary>
        /// 当前书籍
        /// </summary>
        public BookModel CurrentBook
        {
            get
            {
                return _CurrentBook;
            }
            set
            {
                _CurrentBook = value;
                RaisePropertyChanged(nameof(CurrentBook));
            }
        }
        #endregion

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

        #region 编辑面板可见
        bool _EditVisibility = false;
        /// <summary>
        /// 编辑面板可见
        /// </summary>
        public bool EditVisibility
        {
            get
            {
                return _EditVisibility;
            }
            set
            {
                _EditVisibility = value;
                RaisePropertyChanged(nameof(EditVisibility));
            }
        }
        #endregion

        #region 书源集合
        private ObservableCollection<BookSourceModel> _BookSources;
        /// <summary>
        /// 书源集合
        /// </summary>
        public ObservableCollection<BookSourceModel> BookSources
        {
            get
            {
                return _BookSources;
            }
            set
            {
                _BookSources = value;
                RaisePropertyChanged(nameof(BookSources));
            }
        }
        #endregion

        #region 单书源选中项
        private BaseDataModel _SingleSelectBookSource = null;
        /// <summary>
        /// 单书源选中项
        /// </summary>
        public BaseDataModel SingleSelectBookSource
        {
            get
            {
                if (_SingleSelectBookSource == null)
                {
                    _SingleSelectBookSource = SingleBookSources.FirstOrDefault();
                }
                return _SingleSelectBookSource;
            }
            set
            {
                _SingleSelectBookSource = value;
                RaisePropertyChanged(nameof(SingleSelectBookSource));
            }
        }
        #endregion

        #region 单书源集合
        private ObservableCollection<BaseDataModel> _SingleBookSources;
        /// <summary>
        /// 单书源集合
        /// </summary>
        public ObservableCollection<BaseDataModel> SingleBookSources
        {
            get
            {
                if (_SingleBookSources == null)
                    _SingleBookSources = new ObservableCollection<BaseDataModel>();
                if (_SingleBookSources.Count <= 0)
                {
                    foreach (var item in BookSources)
                    {
                        _SingleBookSources.Add(new BaseDataModel() { Name = $"{item.ID}", Description = item.Name });
                    }
                }
                return _SingleBookSources;
            }
            set
            {
                _SingleBookSources = value;
                RaisePropertyChanged(nameof(SingleSelectBookSource));
            }
        }
        #endregion
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 书籍服务
        /// </summary>
        IBookService _BookService;
        /// <summary>
        /// 书源服务
        /// </summary>
        IBookSourceService _BookSourceService;
        /// <summary>
        /// 下载任务
        /// </summary>
        IDownloadTaskService _DownloadTaskService;
        /// <summary>
        /// 章节服务
        /// </summary>
        IChapterService _ChapterService;
        #endregion

        #region 命令(Commands)
        /// <summary>
        /// 下载
        /// </summary>
        public DelegateCommand<BookModel> DownloadCommand { get; private set; }
        /// <summary>
        /// 编辑书籍
        /// </summary>
        public DelegateCommand<BookModel> EditBookCommand { get; private set; }
        /// <summary>
        /// 编辑书籍
        /// </summary>
        public DelegateCommand AddBookCommand { get; private set; }
        /// <summary>
        /// 保存书籍
        /// </summary>
        public DelegateCommand SaveBookCommand { get; private set; }
        /// <summary>
        /// 取消保存
        /// </summary>
        public DelegateCommand CancelSaveCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 首页模型
        /// </summary>
        /// <param name="containerProvider"></param>
        public IndexViewModel(IContainerProvider containerProvider) : base(containerProvider)
        {
            _BookService = containerProvider.Resolve<IBookService>();
            _BookSourceService = containerProvider.Resolve<IBookSourceService>();
            _DownloadTaskService = containerProvider.Resolve<IDownloadTaskService>();
            _ChapterService = containerProvider.Resolve<IChapterService>();
            DownloadCommand = new DelegateCommand<BookModel>(Download);
            EditBookCommand = new DelegateCommand<BookModel>(EditBook);
            AddBookCommand = new DelegateCommand(AddBook);
            SaveBookCommand = new DelegateCommand(SaveBook);
            CancelSaveCommand = new DelegateCommand(CancelSave);
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
            BookSources = _BookSourceService.GetAll();
            Books = _BookService.GetAll();
            EditVisibility = false;
        }

        /// <summary>
        /// 下载选中书籍
        /// </summary>
        /// <param name="model">书籍对象</param>
        void Download(BookModel model)
        {
            try
            {
                if (model == null
                    || string.IsNullOrWhiteSpace(model.Key)
                    || string.IsNullOrWhiteSpace(model.Link)
                    )
                    return;
                NavigationToView("DownloadView", null);
                Task.Run(() =>
                {
                    try
                    {
                        if (_DownloadTaskService.GetAll(model.Key).Count <= 0)
                        {
                            BookSourceModel bookSource = BookSources.SingleOrDefault(item => item.ID.Equals(model.SourceID));
                            if (bookSource != null)
                            {
                                FictionSpider fictionSpider = new FictionSpider(bookSource);
                                ObservableCollection<DownloadTaskModel> downloadTasks = fictionSpider.ReplenishBookReturnBookTask(model);
                                _BookService.Save(model);
                                foreach (DownloadTaskModel downloadTask in downloadTasks)
                                {
                                    _DownloadTaskService.Save(downloadTask);
                                }
                            }
                        }
                        AddDownloadTask(model.Key, model.Name);
                    }
                    catch (Exception ex)
                    {
                        ShowError($"首页下载书籍出现异常:{ex.Message}");
                    }
                });
            }
            catch (Exception ex)
            {
                ShowError($"下载书籍出现异常:{ex.Message}");
            }
        }

        /// <summary>
        /// 安吉b书籍
        /// </summary>
        /// <param name="model">书籍对象</param>
        void EditBook(BookModel model)
        {
            if (InvalidData())
                return;
            if (CurrentBook != null)
            {
                _BookService.Save(CurrentBook);
            }
            CurrentBook = model;
            SingleSelectBookSource = SingleBookSources.SingleOrDefault(item=>item.Name.Equals($"{model.SourceID}"));
            EditVisibility = true;
        }
        /// <summary>
        /// 添加书籍
        /// </summary>
        void AddBook()
        {
            if(InvalidData())
                return;
            if (CurrentBook != null)
            {
                _BookService.Save(CurrentBook);
            }
            EditVisibility = true;
            CurrentBook = new BookModel() { ID=Guid.NewGuid(),UpdateTime = DateTime.Now};
            Books.Add(CurrentBook);
        }
        /// <summary>
        /// 保存书籍
        /// </summary>
        void SaveBook()
        {
            if (InvalidData())
                return;
            CurrentBook.SourceID = new Guid(SingleSelectBookSource.Name);
            _BookService.Save(CurrentBook);
            BookModel model = Books.SingleOrDefault(item => item.ID.Equals(CurrentBook.ID));
            if (model != null)
            {
                model = CurrentBook;
            }
            else
            {
                Books.Add(CurrentBook);
            }
            RaisePropertyChanged(nameof(Books));
            EditVisibility = false;
        }
        /// <summary>
        /// 取消保存
        /// </summary>
        void CancelSave()
        {
            CurrentBook = null;
            EditVisibility = false;
        }
        /// <summary>
        /// 无效数据
        /// </summary>
        /// <returns></returns>
        bool InvalidData()
        {
            if (CurrentBook == null)
                return false;
            if (string.IsNullOrWhiteSpace(CurrentBook.Name)
                || string.IsNullOrWhiteSpace(CurrentBook.Link)
                || (SingleSelectBookSource==null)
                )
                return true;
            return false;
        }
        /// <summary>
        /// 添加下载任务
        /// </summary>
        /// <param name="taskKey">任务 Key</param>
        /// <param name="taskName">任务名称</param>
        void AddDownloadTask(string taskKey, string taskName)
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
