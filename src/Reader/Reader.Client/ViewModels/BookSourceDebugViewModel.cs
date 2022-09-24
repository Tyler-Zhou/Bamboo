using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using Reader.Client.Models;
using Reader.Client.Spider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Client.ViewModels
{
    /// <summary>
    /// 书源调试
    /// </summary>
    public class BookSourceDebugViewModel : BaseViewModel
    {
        #region 成员(Member)

        private BookSourceModel _CurrentSource;
        /// <summary>
        /// 书源
        /// </summary>
        public BookSourceModel CurrentSource
        {
            get
            {
                if(_CurrentSource==null && BookSources!=null && BookSources.Count>0)
                {
                    _CurrentSource = BookSources.FirstOrDefault();
                }
                return _CurrentSource;
            }
            set
            {
                _CurrentSource = value;
                RaisePropertyChanged(nameof(CurrentSource));
            }
        }

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

        #endregion

        #region 服务(Services)
        #endregion

        #region 命令(Commands)
        /// <summary>
        /// 查询
        /// </summary>
        public DelegateCommand<BookSourceModel> EditSourceCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 书源调试
        /// </summary>
        /// <param name="containerProvider"></param>
        public BookSourceDebugViewModel(IContainerProvider containerProvider) : base(containerProvider)
        {
            EditSourceCommand = new DelegateCommand<BookSourceModel>(EditSource);
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
            BookSources = new ObservableCollection<BookSourceModel>()
            {
                new BookSourceModel()
                {
                    ID = Guid.NewGuid(),
                    Name = "笔趣阁库小说",
                    Link = "https://www.shuquge.com/",
                    SearchLink = "https://www.xxbiqudu.com/modules/article/search.php?searchkey=",
                    Group = "网络小说",
                    SearchXPathList = "//table[starts-with(@class,'grid')]/tr",
                    SearchXPathName = "//td[1]/a",
                    SearchXPathTag = "",
                    SearchXPathAuthor = "//td[3]",
                    SearchXPathStatus = "//td[6]",
                },
            };
        }
        /// <summary>
        /// 编辑书源
        /// </summary>
        /// <param name="model"></param>
        private void EditSource(BookSourceModel model)
        {
            CurrentSource = model;
        }
        #endregion
    }
}
