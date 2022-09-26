﻿using Prism.Commands;
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
    /// 搜索视图模型
    /// </summary>
    public class SearchViewModel: BaseViewModel
    {
        #region 成员(Member)

        #region 查询文本
        private string _KeyWord = "";
        /// <summary>
        /// 查询文本
        /// </summary>
        public string KeyWord
        {
            get { return _KeyWord; }
            set
            {
                _KeyWord = value;
                RaisePropertyChanged(nameof(KeyWord));
            }
        }
        #endregion

        #region 精确搜索
        private bool _PreciseSearch = true;
        /// <summary>
        /// 精确搜索
        /// </summary>
        public bool PreciseSearch
        {
            get { return _PreciseSearch; }
            set
            {
                _PreciseSearch = value;
                RaisePropertyChanged(nameof(PreciseSearch));
            }
        }
        #endregion

        #region 书源类型选中项
        private ComboBoxModel _SelectBookSourceType = null;
        /// <summary>
        /// 书源类型选中项
        /// </summary>
        public ComboBoxModel SelectBookSourceType
        {
            get
            {
                if (_SelectBookSourceType == null)
                {
                    _SelectBookSourceType = BookSourceTypes.FirstOrDefault();
                }
                return _SelectBookSourceType;
            }
            set
            {
                _SelectBookSourceType = value;
                if ("Single".Equals(_SelectBookSourceType.Name))
                {
                    SingleVisible = true;
                    MultipleVisible = false;
                }
                else
                {
                    SingleVisible = false;
                    MultipleVisible = true;
                    PreciseSearch = true;
                }
                RaisePropertyChanged(nameof(SelectBookSourceType));
            }
        }
        #endregion

        #region 书源类型集合
        private ObservableCollection<ComboBoxModel> _BookSourceTypes;
        /// <summary>
        /// 书源类型集合
        /// </summary>
        public ObservableCollection<ComboBoxModel> BookSourceTypes
        {
            get
            {
                if (_BookSourceTypes == null)
                    _BookSourceTypes = new ObservableCollection<ComboBoxModel>();
                if (_BookSourceTypes.Count <= 0)
                {
                    _BookSourceTypes.Add(new ComboBoxModel() {Name="Single", Description = "单源" });
                    _BookSourceTypes.Add(new ComboBoxModel() {Name= "Multiple", Description = "多源" });
                }
                return _BookSourceTypes;
            }
            set
            {
                _BookSourceTypes = value;
                RaisePropertyChanged(nameof(BookSourceTypes));
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

        #region 单书源可见
        private bool _SingleVisible = true;
        /// <summary>
        /// 单书源可见
        /// </summary>
        public bool SingleVisible
        {
            get { return _SingleVisible; }
            set { _SingleVisible = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 单书源选中项
        private ComboBoxModel _SingleSelectBookSource = null;
        /// <summary>
        /// 单书源选中项
        /// </summary>
        public ComboBoxModel SingleSelectBookSource
        {
            get
            {
                if (_SingleSelectBookSource==null)
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
        private ObservableCollection<ComboBoxModel> _SingleBookSources;
        /// <summary>
        /// 单书源集合
        /// </summary>
        public ObservableCollection<ComboBoxModel> SingleBookSources
        {
            get
            {
                if (_SingleBookSources == null)
                    _SingleBookSources = new ObservableCollection<ComboBoxModel>();
                if(_SingleBookSources.Count<=0)
                {
                    foreach (var item in BookSources)
                    {
                        _SingleBookSources.Add(new ComboBoxModel() {Name=item.Name, Description =item.Name });
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

        #region 多书源可见
        private bool _MultipleVisible = false;
        /// <summary>
        /// 多书源可见
        /// </summary>
        public bool MultipleVisible
        {
            get { return _MultipleVisible; }
            set { _MultipleVisible = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 多源选中项
        private ComboBoxModel _MultipleSelectBookSource = null;
        /// <summary>
        /// 多源选中项
        /// </summary>
        public ComboBoxModel MultipleSelectBookSource
        {
            get
            {
                if(_MultipleSelectBookSource==null)
                {
                    _MultipleSelectBookSource = MultipleBookSources.FirstOrDefault();

                }
                return _MultipleSelectBookSource;
            }
            set
            {
                _MultipleSelectBookSource = value;
                RaisePropertyChanged(nameof(MultipleSelectBookSource));
            }
        }
        #endregion

        #region 多书源集合
        private ObservableCollection<ComboBoxModel> _MultipleBookSources;
        /// <summary>
        /// 多书源集合
        /// </summary>
        public ObservableCollection<ComboBoxModel> MultipleBookSources
        {
            get
            {
                if (_MultipleBookSources == null)
                    _MultipleBookSources = new ObservableCollection<ComboBoxModel>();
                if (_MultipleBookSources.Count <= 0)
                {
                    _MultipleBookSources.Add(new ComboBoxModel() {Name="All", Description = $"所有({BookSources.Count()})" });
                    foreach (var item in BookSources.GroupBy(item=>item.Group))
                    {
                        _MultipleBookSources.Add(new ComboBoxModel() {Name=item.Key, Description = $"{item.Key}({item.Count()})" });
                    }
                }
                return _MultipleBookSources;
            }
            set
            {
                _MultipleBookSources = value;
                RaisePropertyChanged(nameof(MultipleBookSources));
            }
        }
        #endregion

        #region 书籍集合
        ObservableCollection<BookModel> _Books;
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

        #region 服务(Services)
        /// <summary>
        /// 书源服务
        /// </summary>
        IBookSourceService _BookSourceService;
        /// <summary>
        /// 书籍服务
        /// </summary>
        IBookService _BookService;
        /// <summary>
        /// 下载任务
        /// </summary>
        IDownloadTaskService _DownloadTaskService;
        #endregion

        #region 命令(Commands)
        /// <summary>
        /// 查询
        /// </summary>
        public DelegateCommand SearchCommand { get; private set; }
        /// <summary>
        /// 下载
        /// </summary>
        public DelegateCommand<BookModel> DownloadCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 搜索视图模型
        /// </summary>
        /// <param name="containerProvider"></param>
        public SearchViewModel(IContainerProvider containerProvider) : base(containerProvider)
        {
            _BookSourceService = containerProvider.Resolve<IBookSourceService>();
            _BookService = containerProvider.Resolve<IBookService>();
            _DownloadTaskService = containerProvider.Resolve<IDownloadTaskService>();
            SearchCommand = new DelegateCommand(SearchBookList);
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

        #region 方法(Methods)
        /// <summary>
        /// 初始化界面数据
        /// </summary>
        private void InitData()
        {
            BookSources = _BookSourceService.GetAll();
            Books = new ObservableCollection<BookModel>();
        }
        /// <summary>
        /// 下载选中书籍
        /// </summary>
        private void SearchBookList()
        {
            if (string.IsNullOrWhiteSpace(KeyWord))
                return;
            Books.Clear();
            ShowLoading(true);
            try
            {
                if ("Single".Equals(SelectBookSourceType.Name))
                {
                    BookSourceModel bookSource = BookSources.SingleOrDefault(item => item.Name.Equals(SingleSelectBookSource.Name));
                    FictionSpider fictionSpider = new FictionSpider(bookSource);
                    Books.AddRange(fictionSpider.SearchBookList(KeyWord, PreciseSearch));
                }
                else
                {
                    if ("All".Equals(MultipleSelectBookSource.Name))
                    {
                        foreach (var item in BookSources)
                        {
                            FictionSpider fictionSpider = new FictionSpider(item);
                            Books.AddRange(fictionSpider.SearchBookList(KeyWord, PreciseSearch));
                        }
                    }
                    else
                    {
                        foreach (var item in BookSources.Where(item => item.Group.Equals(MultipleSelectBookSource.Name)))
                        {
                            FictionSpider fictionSpider = new FictionSpider(item);
                            Books.AddRange(fictionSpider.SearchBookList(KeyWord, PreciseSearch));
                        }
                    }
                }
                if (Books.Count <= 0)
                {
                    ShowInformation("无匹配查询结果，请更换关键词后重试！");
                }
                else
                {
                    ShowInformation($"查找成功，相关数据[{Books.Count}]条");
                }

                RaisePropertyChanged(nameof(Books));
            }catch(Exception ex)
            {
                ShowError($"查找书籍出现异常:{ex.Message}");
            }
            finally
            {
                ShowLoading(false);
            }
        }

        /// <summary>
        /// 下载选中书籍
        /// </summary>
        /// <param name="model">书籍对象</param>
        private void Download(BookModel model)
        {
            NavigationToView("DownloadView", null);
            Task.Run(() => {
                try
                {
                    BookModel bookModel = Books.SingleOrDefault(item => item.ID.Equals(model.ID));
                    BookSourceModel bookSource = BookSources.SingleOrDefault(item => item.ID.Equals(model.SourceID));
                    FictionSpider fictionSpider = new FictionSpider(bookSource);
                    ObservableCollection<DownloadTaskModel> downloadTasks = fictionSpider.ReplenishBookReturnBookTask(bookModel);
                    _BookService.Save(bookModel);
                    foreach (DownloadTaskModel downloadTask in downloadTasks)
                    {
                        _DownloadTaskService.Save(downloadTask);
                    }
                    AddDownloadTask(bookModel.Key,bookModel.Name);
                }
                catch (Exception ex)
                {
                    ShowError($"下载书籍出现异常:{ex.Message}");
                }
            });
        }

        /// <summary>
        /// 添加下载任务
        /// </summary>
        /// <param name="taskKey">任务 Key</param>
        /// <param name="taskName">任务名称</param>
        private void AddDownloadTask(string taskKey,string taskName)
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
