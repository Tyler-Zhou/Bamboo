using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Reader.Client.Events;
using Reader.Client.Extensions;
using Reader.Client.Interfaces;
using Reader.Client.Models;
using Reader.Client.Spider;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reader.Client.Services
{
    /// <summary>
    /// 下载进程
    /// </summary>
    public class TaskProgress : BindableBase
    {
        #region 成员(Member)

        #region 下载任务 Key
        private string _Key = "";
        /// <summary>
        /// 下载任务 Key
        /// </summary>
        public string Key
        {
            get
            {
                return _Key;
            }
            set
            {
                _Key = value;
                RaisePropertyChanged(nameof(Key));
            }
        }
        #endregion

        #region 下载任务名称
        private string _Name = "";
        /// <summary>
        /// 下载任务名称
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        #endregion

        #region 当前位置
        private double _Position = 0;
        /// <summary>
        /// 当前位置
        /// </summary>
        public double Position
        {
            get
            {
                return _Position;
            }
            set
            {
                _Position = value;
                RaisePropertyChanged(nameof(Position));
            }
        }
        #endregion

        #region 最大值
        private double _MaxValue = 0;
        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue
        {
            get
            {
                return _MaxValue;
            }
            set
            {
                _MaxValue = value;
                RaisePropertyChanged(nameof(MaxValue));
            }
        }
        #endregion

        #region 工具栏提示
        private string _ToolTip = "";
        /// <summary>
        /// 工具栏提示
        /// </summary>
        public string ToolTip
        {
            get
            {
                return _ToolTip;
            }
            set
            {
                _ToolTip = value;
                RaisePropertyChanged(nameof(ToolTip));
            }
        }
        #endregion

        #region 是否暂停
        bool _IsPause = false;
        /// <summary>
        /// 是否暂停
        /// </summary>
        bool IsPause
        {
            get
            {
                return _IsPause;
            }
            set
            {
                _IsPause = value;
                RaisePropertyChanged(nameof(IsPause));
            }
        }
        #endregion

        #region 是否停止
        bool _IsStop = false;
        /// <summary>
        /// 是否停止
        /// </summary>
        bool IsStop
        {
            get
            {
                bool isStop = true;
                foreach (Thread item in _ThreadPool)
                {
                    if (item.ThreadState != ThreadState.Stopped)
                        isStop = false;
                }

                return isStop;
            }
            set
            {
                _IsStop = value;
            }
        }
        #endregion

        /// <summary>
        /// 下载任务队列
        /// </summary>
        ConcurrentQueue<DownloadTaskModel> DownloadTaskQueue = new ConcurrentQueue<DownloadTaskModel>();
        /// <summary>
        /// 创建线程池 4 个线程
        /// </summary>
        Thread[] _ThreadPool = new Thread[4];
        /// <summary>
        /// 写入文件锁
        /// </summary>
        private static readonly object FileLock = new object();
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 事件聚合器
        /// </summary>
        IEventAggregator _EventAggregator;
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
        /// <summary>
        /// 应用程序服务
        /// </summary>
        IApplicationService _ApplicationService;
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 下载进程
        /// </summary>
        /// <param name="containerProvider"></param>
        /// <param name="taskKey"></param>
        /// <param name="taskName"></param>
        public TaskProgress(IContainerProvider containerProvider, string taskKey, string taskName)
        {
            Key = taskKey;
            Name = taskName;
            _BookSourceService = containerProvider.Resolve<IBookSourceService>();
            _DownloadTaskService = containerProvider.Resolve<IDownloadTaskService>();
            _ChapterService = containerProvider.Resolve<IChapterService>();
            _ApplicationService = containerProvider.Resolve<IApplicationService>();
            _EventAggregator = containerProvider.Resolve<IEventAggregator>();

            InitData();
            _ApplicationService.AddFunction(StopDownload);
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 
        /// </summary>
        public bool Pause()
        {
            IsPause = true;
            return true;
        }

        /// <summary>
        /// 继续
        /// </summary>
        public bool Continue()
        {
            IsPause = false;
            return true;
        }

        /// <summary>
        /// 停止
        /// </summary>
        public bool Stop()
        {
            return StopDownload().Result;
        }
        #endregion

        #region 私有方法(Private Method)
        /// <summary>
        /// 初始化
        /// </summary>
        void InitData()
        {
            ObservableCollection<DownloadTaskModel> DownloadTasks = _DownloadTaskService.GetAll(Key);
            if(DownloadTasks !=null && DownloadTasks.Count>0)
            {
                //开启线程池
                for (sbyte i = 0; i < _ThreadPool.Length; i++)
                {
                    _ThreadPool[i] = new Thread(new ThreadStart(DownloadTask));
                    _ThreadPool[i].Start();
                }
                // 向线程池提交任务
                foreach (DownloadTaskModel chapterTask in DownloadTasks)
                {
                    DownloadTaskQueue.Enqueue(chapterTask);
                }
            }
            MaxValue = DownloadTaskQueue.Count;
            Position = 0;
            ToolTip = $"总下载项目{MaxValue},当前{Position}项";
        }
        /// <summary>
        /// 下载
        /// </summary>
        void DownloadTask()
        {
            try
            {
                while (!_IsStop) // 深度爬取
                {
                    //暂停时一直跳转
                    if (!_IsStop && IsPause)
                    {
                        Thread.Sleep(3 * 1000);
                        continue;
                    }
                    DownloadTaskModel downloadTask = null;
                    try
                    {
                        if (DownloadTaskQueue.TryDequeue(out downloadTask) == false)
                        {
                            Thread.Sleep(5 * 1000);
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        _EventAggregator.ShowMessage(new TipsInfo() { Content = $"从队列获取任务出现异常:{ex.Message}", Type = EnumTipsType.Error });
                        Thread.Sleep(10 * 1000);
                        continue;
                    }
                    try
                    {
                        lock (FileLock)
                        {
                            BookSourceModel bookSource = _BookSourceService.SingleOrDefault(downloadTask.SourceID);
                            if (bookSource == null)
                                throw new Exception($"未找到书源:{downloadTask.Key}");
                            FictionSpider _FictionSpider = new FictionSpider(bookSource);
                            ChapterModel chapter = _FictionSpider.GetChapter(downloadTask);
                            _ChapterService.Save(chapter);
                            //删除任务
                            downloadTask.IsDownload = true;
                            _DownloadTaskService.Save(downloadTask);
                            Position++;
                            ToolTip = $"总{MaxValue}项,当前完成{Position}项  链接[{downloadTask.Link}]";
                            if(Position >= MaxValue)
                                _IsStop = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        _EventAggregator.ShowMessage(new TipsInfo() { Content = $"获取/保存章节内容异常:{ex.Message}", Type = EnumTipsType.Error });
                    }
                }
            }
            catch (Exception ex)
            {
                _EventAggregator.ShowMessage(new TipsInfo() { Content = $"下载异常:{ex.Message}", Type = EnumTipsType.Error });
            }
        }
        /// <summary>
        /// 停止下载
        /// </summary>
        Task<bool> StopDownload()
        {
            IsStop = true;
            return Task.Run(() =>
            {
                while (true)
                {
                    if (IsStop)
                        break;
                }
                return true;
            });
        }
        #endregion
    }
}
