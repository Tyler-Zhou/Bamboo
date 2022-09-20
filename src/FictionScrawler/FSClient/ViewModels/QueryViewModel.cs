using FSClient.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
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
        private string _QueryText = "";
        /// <summary>
        /// 查询文本
        /// </summary>
        public string QueryText
        {
            get
            {
                return _QueryText;
            }
            set
            {
                _QueryText = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// 书籍集合
        /// </summary>
        public ObservableCollection<BookModel> Books{get; set;}
        #endregion

        #region 服务(Services)
        #endregion

        #region 命令(Commands)
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
        public QueryViewModel(IContainerProvider containerProvider) : base(containerProvider)
        {
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
            Books = new ObservableCollection<BookModel>()
            {
                new BookModel()
                {
                    Key = "0001",
                    Name = "书籍1",
                    Author="作者1"
                },
                new BookModel()
                {
                    Key = "0002",
                    Name = "书籍1",
                    Author="作者2"
                },
            };
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
