#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/1/4 星期四 14:13:04
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
    /// 日志信息
    /// </summary>
    [Serializable]
    public sealed class LogMessage
    {
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel Level { get; set; }
        /// <summary>
        /// 日志异常
        /// </summary>
        public Exception Exception { get; set; }
    }
}
