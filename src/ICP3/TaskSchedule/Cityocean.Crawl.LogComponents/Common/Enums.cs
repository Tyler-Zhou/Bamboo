#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/1/4 星期四 14:09:05
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace Cityocean.Crawl.LogComponents
{
    /// <summary>
    /// 日志等级
    /// </summary>
    [Serializable]
    public enum LogLevel
    {
        /// <summary>
        /// 
        /// </summary>
        Debug,
        /// <summary>
        /// 
        /// </summary>
        Info,
        /// <summary>
        /// 
        /// </summary>
        Error,
        /// <summary>
        /// 
        /// </summary>
        Warn,
        /// <summary>
        /// 
        /// </summary>
        Fatal
    }
}
