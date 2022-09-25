using ImTools;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using Reader.Client.Extensions;
using Reader.Client.Interfaces;
using Reader.Client.Models;
using Reader.Client.Spider;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Reader.Client.ViewModels
{
    
    /// <summary>
    /// 书源调试
    /// </summary>
    public class DebugViewModel : BaseViewModel
    {
        #region 成员(Member)

        #region 查询文本
        private string _KeyWord = "仙逆";
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

        #region 结果文本
        StringBuilder stringBuilder = new StringBuilder();
        /// <summary>
        /// 结果文本
        /// </summary>
        public string ResultText
        {
            get
            {
                return stringBuilder.ToString();
            }
            set
            {
                stringBuilder.AppendLine(value);
                RaisePropertyChanged(nameof(ResultText));
            }
        }
        #endregion

        #region 当前书源
        private BookSourceModel _CurrentSource;
        /// <summary>
        /// 当前书源
        /// </summary>
        public BookSourceModel CurrentSource
        {
            get
            {
                if (_CurrentSource == null)
                    _CurrentSource = new BookSourceModel() { ID = Guid.NewGuid() };
                if (string.IsNullOrWhiteSpace(_CurrentSource.Name) && BookSources != null && BookSources.Count > 0)
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

        Stopwatch _Stopwatch = new Stopwatch();

        #endregion

        #region 服务(Services)
        /// <summary>
        /// 书源服务
        /// </summary>
        IBookSourceService _BookSourceService;
        #endregion

        #region 命令(Commands)
        /// <summary>
        /// 编辑源
        /// </summary>
        public DelegateCommand<BookSourceModel> EditCommand { get; private set; }
        /// <summary>
        /// 调试
        /// </summary>
        public DelegateCommand DebugCommand { get; private set; }
        /// <summary>
        /// 保存源
        /// </summary>
        public DelegateCommand SaveCommand { get; private set; }
        /// <summary>
        /// 生成源
        /// </summary>
        public DelegateCommand GenerateCommand { get; private set; }
        /// <summary>
        /// 添加源
        /// </summary>
        public DelegateCommand AddCommand { get; private set; }
        /// <summary>
        /// 删除源
        /// </summary>
        public DelegateCommand RemoveCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 书源调试
        /// </summary>
        /// <param name="containerProvider"></param>
        public DebugViewModel(IContainerProvider containerProvider) : base(containerProvider)
        {
            _BookSourceService = containerProvider.Resolve<IBookSourceService>();
            AddCommand = new DelegateCommand(AddSource);
            EditCommand = new DelegateCommand<BookSourceModel>(EditSource);
            RemoveCommand = new DelegateCommand(RemoveSource);
            DebugCommand = new DelegateCommand(DebugSource);
            SaveCommand = new DelegateCommand(SaveSource);
            GenerateCommand = new DelegateCommand(GenerateSource);
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
        }

        

        /// <summary>
        /// 无效数据
        /// </summary>
        /// <returns></returns>
        private bool InvalidData()
        {
            if (string.IsNullOrWhiteSpace(CurrentSource.Name) 
                || string.IsNullOrWhiteSpace(CurrentSource.Link)
                )
                return true;
            return false;
        }
        /// <summary>
        /// 添加源
        /// </summary>
        private void AddSource()
        {
            if (InvalidData())
                return;
            BookSourceModel model = new BookSourceModel();
            model.ID = Guid.NewGuid();
            model.Name = "新书源";
            BookSources.Insert(0,model);
            CurrentSource = model;
        }
        /// <summary>
        /// 编辑书源
        /// </summary>
        /// <param name="model"></param>
        private void EditSource(BookSourceModel model)
        {
            CurrentSource = model;
        }
        /// <summary>
        /// 删除源
        /// </summary>
        private void RemoveSource()
        {
            BookSources.Remove(CurrentSource);
        }
        /// <summary>
        /// 
        /// </summary>
        private void DebugSource()
        {
            stringBuilder.Clear();
            try
            {
                SaveSource();
                _Stopwatch.Start();
                CurrentSource.IsDebug = true;
                FictionSpider fictionSpider = new FictionSpider(CurrentSource);
                fictionSpider.OnWriteDebugLog = WriteDebugLog;
                ObservableCollection<BookModel> books= fictionSpider.SearchBookList(KeyWord,true);
                if(books!=null && books.Count>0)
                {
                    BookModel model = books.FirstOrDefault();
                    ObservableCollection<DownloadTaskModel> downloadTask = fictionSpider.ReplenishBookReturnBookTask(model);
                    if(downloadTask != null && downloadTask.Count>0)
                    {
                        fictionSpider.GetChapter(downloadTask.FirstOrDefault());
                    }
                }
            }
            catch (Exception ex)
            {
                WriteDebugLog(ex.Message);
            }
            finally
            {
                if(_Stopwatch != null)
                {
                    _Stopwatch.Stop();
                    _Stopwatch.Reset();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void SaveSource()
        {
            _BookSourceService.Save(CurrentSource);
        }
        /// <summary>
        /// 
        /// </summary>
        private void GenerateSource()
        {
            stringBuilder.Clear();
            CurrentSource.IsDebug = false;
            _BookSourceService.Save(CurrentSource);
            ResultText = _BookSourceService.Generate(CurrentSource);
        }
        /// <summary>
        /// 写入调试日志
        /// </summary>
        /// <param name="logString"></param>
        private void WriteDebugLog(string logString)
        {
            ResultText = $"[{_Stopwatch.ElapsedToLogString()}]{logString}";
        }
        #endregion
    }
}
