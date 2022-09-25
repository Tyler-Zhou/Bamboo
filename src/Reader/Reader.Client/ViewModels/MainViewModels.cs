using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Reader.Client.Common;
using Reader.Client.Events;
using Reader.Client.Extensions;
using Reader.Client.Interfaces;
using Reader.Client.Models;
using Reader.Client.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Reader.Client.ViewModels
{
    /// <summary>
    /// 主界面视图模型
    /// </summary>
    public class MainViewModel : BindableBase
    {
        #region 成员(Member)

        #region 应用程序标题
        /// <summary>
        /// 应用程序标题
        /// </summary>
        public string ApplicationTitle
        {
            get
            {
                string title = "ApplicationTitle".FindResourceDictionary();
                string versionNo = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                return $"{title} V{versionNo}";
            }
        }
        #endregion

        /// <summary>
        /// 方法集合
        /// </summary>
        private static List<Func<Task>> _Functions = new List<Func<Task>>();

        #endregion

        #region 服务(Service)
        /// <summary>
        /// 容器提供者(DryICO)
        /// </summary>
        public readonly IContainerProvider ContainerProvider;
        /// <summary>
        /// 事件聚合器
        /// </summary>
        public readonly IEventAggregator _EventAggregator;
        /// <summary>
        /// 应用程序服务
        /// </summary>
        public readonly IApplicationService _ApplicationService;
        /// <summary>
        /// 书源服务
        /// </summary>
        IBookSourceService _BookSourceService;
        /// <summary>
        /// 书籍下载任务
        /// </summary>
        IBookTaskService _BookTaskService;
        /// <summary>
        /// 章节服务
        /// </summary>
        IChapterService _ChapterService;
        #endregion

        #region 命令(Commands)
        /// <summary>
        /// 导航到视图
        /// </summary>
        public DelegateCommand<string> NavigateViewCommand { get; private set; }
        /// <summary>
        /// 开始下载
        /// </summary>
        public DelegateCommand StartDownloadCommand { get; private set; }
        /// <summary>
        /// 退出程序
        /// </summary>
        public DelegateCommand ExitApplicationCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerProvider">容器提供者(DryICO)</param>
        public MainViewModel(IContainerProvider containerProvider)
        {
            ContainerProvider = containerProvider;
            _EventAggregator = ContainerProvider.Resolve<IEventAggregator>();
            _ApplicationService = ContainerProvider.Resolve<IApplicationService>();
            _BookSourceService = ContainerProvider.Resolve<IBookSourceService>();
            _BookTaskService = ContainerProvider.Resolve<IBookTaskService>();
            _ChapterService = ContainerProvider.Resolve<IChapterService>();

            StartDownloadCommand = new DelegateCommand(StartDownload);
            NavigateViewCommand = new DelegateCommand<string>(NavigateView);
            ExitApplicationCommand = new DelegateCommand(ExitApplication);
            InitData();
        }
        #endregion

        #region 重写方法(Override)

        #endregion

        #region 方法(Method)
        /// <summary>
        /// 初始化界面数据
        /// </summary>
        private void InitData()
        {
           
        }
        /// <summary>
        /// 导航到视图
        /// </summary>
        /// <param name="viewName">视图名称</param>
        private void NavigateView(string viewName)
        {
            _ApplicationService.NavigationToView(viewName);
        }
        
        /// <summary>
        /// 关闭窗体
        /// </summary>
        public bool ClosingWindow()
        {
            return !Task.Run(() => ExecuteAllFunction().Result).Result;
        }
        /// <summary>
        /// 退出应用程序
        /// </summary>
        void ExitApplication()
        {
            var result = Task.Run(() => ExecuteAllFunction().Result).Result;
            Application.Current.Shutdown();
        }
        /// <summary>
        /// 执行所有方法
        /// </summary>
        /// <returns></returns>
        async Task<bool> ExecuteAllFunction()
        {
            if (_Functions != null && _Functions.Count > 0)
            {
                var functions = _Functions.Select(command => command());
                await Task.WhenAll(functions);

                return _Functions.Count > 0;
            }
            return true;
        }
        /// <summary>
        /// 开始下载
        /// </summary>
        void StartDownload()
        {
            Task.Run(() =>
            {
                ObservableCollection<BookSourceModel> bookSources = _BookSourceService.GetAll();
                ObservableCollection<BookTaskModel> bookTasks = _BookTaskService.GetAll();
                foreach (BookTaskModel bookTask in bookTasks)
                {
                    BookSourceModel bookSource = bookSources.SingleOrDefault(item => item.ID.Equals(bookTask.SourceID));

                    //书源不为空
                    if (bookSource != null)
                    {
                        Task.Run(() => StartBookTask(bookTask, bookSource));
                    }
                    
                }
            });
        }

        void StartBookTask(BookTaskModel bookTask, BookSourceModel bookSource)
        {
            int[] TotolChapterCount = new int[1];
            ConcurrentQueue<ChapterTaskModel> queue = new ConcurrentQueue<ChapterTaskModel>();
            // 创建线程池 4 个线程
            Thread[] ThreadPool = new Thread[4];
            // 初始化
            for (sbyte i = 0; i < ThreadPool.Length; i++)
            {
                ThreadPool[i] = new Thread(new ThreadStart(new DownloadService(queue, bookSource, _ChapterService, TotolChapterCount).Start));
                ThreadPool[i].Start();
            }
            // 向线程池提交任务
            foreach (ChapterTaskModel chapterTask in bookTask.ChapterTasks.Where(item => !item.IsDownload))
            {
                queue.Enqueue(chapterTask);
                Thread.Sleep(5000);
            }
            while (true) // 等待所以章节下载完
            {
                if (TotolChapterCount[0] == bookTask.ChapterTasks.Count)
                {
                    break;
                }
                Thread.Sleep(1000);
                continue;
            }
        }

        /// <summary>
        /// 正在加载
        /// </summary>
        /// <param name="IsOpen">是否打开</param>
        public void ShowLoading(bool IsOpen)
        {
            _EventAggregator.ShowLoading(new LoadingModel()
            {
                IsOpen = IsOpen
            });
        }

        /// <summary>
        /// 显示普通信息
        /// </summary>
        /// <param name="message">消息</param>
        public void ShowInformation(string message)
        {
            _EventAggregator.ShowMessage(new TipsInfo() { Content = message, Type = EnumTipsType.Information });
        }

        /// <summary>
        /// 显示警告信息
        /// </summary>
        /// <param name="message">消息</param>
        public void ShowWarning(string message)
        {
            _EventAggregator.ShowMessage(new TipsInfo() { Content = message, Type = EnumTipsType.Warning });
        }

        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="message">消息</param>
        public void ShowError(string message)
        {
            _EventAggregator.ShowMessage(new TipsInfo() { Content = message, Type = EnumTipsType.Error });
        }
        #endregion
    }
}
