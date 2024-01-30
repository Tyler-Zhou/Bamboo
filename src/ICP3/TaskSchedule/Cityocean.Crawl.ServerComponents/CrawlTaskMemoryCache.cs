#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/1/8 星期一 14:02:02
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using Cityocean.Crawl.CommonLibrary;
using System;
using System.Collections.Generic;

namespace Cityocean.Crawl.ServerComponents
{
    /// <summary>
    /// 抓取任务内存缓存
    /// </summary>
    public sealed class CrawlTaskMemoryCache : IDisposable
    {
        static Dictionary<Guid, string> taskMemoryCache = new Dictionary<Guid, string>();
        /// <summary>
        /// 
        /// </summary>
        static readonly object synObj = new object();

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="key"></param>
        /// <param name="taskDescription"></param>
        /// <returns></returns>
        public static bool Add(Guid key,string taskDescription)
        {

            if (taskDescription.IsNullOrEmpty())
                return false;
            lock (synObj)
            {
                if (taskMemoryCache.ContainsKey(key))
                    throw new Exception(string.Format("已存在:Key[{0}]Description[{1}]", key, taskDescription));
                taskMemoryCache.Add(key, taskDescription);
            }
            return true;
        }
        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="id"></param>
        public static void Remove(Guid id)
        {
            lock (synObj)
            {
                taskMemoryCache.Remove(id);
            }
        }
        /// <summary>
        /// 清除所有任务
        /// </summary>
        public static void Clear()
        {
            taskMemoryCache.Clear();
        }
        /// <summary>
        /// 释放任务对象
        /// </summary>
        public void Dispose()
        {
            if (taskMemoryCache == null) return;
            taskMemoryCache.Clear();
            taskMemoryCache = null;
        }
    }
}
