using Reader.Client.Interfaces;
using Reader.Client.Models;
using Reader.Client.Spider;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Reader.Client.Services
{
    /// <summary>
    /// 下载服务
    /// </summary>
    public class DownloadService
    {
        #region 成员(Member)
        /// <summary>
        /// 任务队列
        /// </summary>
        private ConcurrentQueue<ChapterTaskModel> _ChapterTasks { get; set; }
        /// <summary>
        /// 当前下载任务
        /// </summary>
        private ChapterTaskModel ChapterTask;
        /// <summary>
        /// 写入文件锁
        /// </summary>
        private static readonly object FileLock = new object();
        /// <summary>
        /// 任务完成计数锁
        /// </summary>
        private static readonly object CountLock = new object();
        /// <summary>
        /// 总任务数
        /// </summary>
        private readonly int[] __TotalChapterCount = new int[1];
        /// <summary>
        /// 爬虫
        /// </summary>
        private readonly FictionSpider _FictionSpider;
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 章节服务
        /// </summary>
        IChapterService _ChapterService;
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 下载服务
        /// </summary>
        /// <param name="chapterTasks">章节任务</param>
        /// <param name="bookSource">书源对象模型</param>
        /// <param name="chapterService">章节服务</param>
        /// <param name="count">章节服务</param>
        public DownloadService(ConcurrentQueue<ChapterTaskModel> chapterTasks, BookSourceModel bookSource, IChapterService chapterService,int[] count)
        {
            _ChapterTasks = chapterTasks;
            _FictionSpider = new FictionSpider(bookSource);
            _ChapterService = chapterService;
            __TotalChapterCount = count;
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            try
            {
                while (true) // 深度爬取
                {
                    try
                    {
                        if (_ChapterTasks.TryDequeue(out ChapterTask) == false)
                        {
                            Thread.Sleep(5 * 1000);
                            continue;
                        }
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(10 * 1000);
                        continue;
                    }

                    try
                    {
                        lock (FileLock)
                        {
                            ChapterModel chapter = _FictionSpider.GetChapter(ChapterTask);
                            _ChapterService.Save(chapter);
                        }
                        //只能有一个线程访问
                        lock (CountLock)
                        {
                            //完成一个计数一次
                            __TotalChapterCount[0]++;
                            ChapterTask.IsDownload = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"获取/保存章节内容异常:{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"下载异常:{ex.Message}");
            }
        } 
        #endregion
    }
}
