using FSClient.Core;
using FSClient.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;

namespace FSClient.ViewModels
{
    /// <summary>
    /// 查询视图模型
    /// </summary>
    public class QueryViewModel : BaseViewModel
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

        /// <summary>
        /// 书籍集合
        /// </summary>
        public ObservableCollection<BookModel> Books{get; set;}
        /// <summary>
        /// 小说解析器
        /// </summary>
        FictionParser _FictionParser;
        #endregion

        #region 服务(Services)
        /// <summary>
        /// 爬虫服务
        /// </summary>
        IScrawlerService _ScrawlerService;
        #endregion

        #region 命令(Commands)
        /// <summary>
        /// 查询
        /// </summary>
        public DelegateCommand QueryCommand { get; private set; }
        /// <summary>
        /// 下载
        /// </summary>
        public DelegateCommand<BookModel> DownloadCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 查询视图模型
        /// </summary>
        /// <param name="containerProvider"></param>
        /// <param name="scrawlerService"></param>
        public QueryViewModel(IContainerProvider containerProvider, IScrawlerService scrawlerService) : base(containerProvider)
        {
            _ScrawlerService = scrawlerService;
            QueryCommand = new DelegateCommand(Query);
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
            Books = new ObservableCollection<BookModel>();
        }
        /// <summary>
        /// 下载选中书籍
        /// </summary>
        /// <param name="keyWord">输入搜索词</param>
        private void Query()
        {
            if (string.IsNullOrWhiteSpace(KeyWord))
                return;

            BookSourceModel bookSource = new BookSourceModel()
            {
                Key = Guid.NewGuid(),
                Name = "笔趣阁库小说",
                Url = "https://www.shuquge.com/",
                SearchUrl = "https://www.xxbiqudu.com/modules/article/search.php?searchkey=",
                Group = "网络小说",
                XPath_List = "//table[starts-with(@class,'grid')]/tr",
                XPath_Name = "//td[1]/a",
                XPath_Type = "",
                XPath_Author = "//td[3]",
                XPath_Status = "//td[6]",
            };
            FictionParser fictionParser = new FictionParser(bookSource);
            Books = fictionParser.QueryBooks(KeyWord);
            RaisePropertyChanged(nameof(Books));
        }

        /// <summary>
        /// 下载选中书籍
        /// </summary>
        /// <param name="model">书籍对象</param>
        private void Download(BookModel model)
        {

        }
        #endregion
    }
}
