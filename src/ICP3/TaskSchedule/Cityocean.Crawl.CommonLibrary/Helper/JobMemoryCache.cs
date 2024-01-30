#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/12/13 星期三 14:29:54
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections.Generic;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 任务缓存管理
    /// </summary>
    public sealed class JobMemoryCache : IDisposable
    {
        /// <summary>
        /// 内存中的任务
        /// </summary>
        static Dictionary<string, string> jobMemoryCache = new Dictionary<string, string>();
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="paramKey">键</param>
        /// <param name="paramValue">值</param>
        /// <returns></returns>
        public static bool AddJob(string paramKey,string paramValue)
        {
            if (paramKey.IsNullOrEmpty())
                return false;
            if (jobMemoryCache.ContainsKey(paramKey))
                return false;
            jobMemoryCache.Add(paramKey, paramValue);
            return true;
        }

        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="paramKey">键</param>
        /// <returns></returns>
        public static void RemoveJob(string paramKey)
        {
            if (paramKey.IsNullOrEmpty())
                return;
            if (!jobMemoryCache.ContainsKey(paramKey))
                return;
            jobMemoryCache.Remove(paramKey);
        }

        #region IDisposable 成员
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (jobMemoryCache != null)
            {
                jobMemoryCache.Clear();
                jobMemoryCache = null;
            }
        }

        #endregion
    }
}
