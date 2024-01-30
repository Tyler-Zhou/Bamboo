#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/6/16 17:07:47
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion
using System;

namespace Cityocean.Crawl.CommonTool
{
    /// <summary>
    /// 爬虫发生错误事件参数
    /// </summary>
    public sealed class ParamError
    {
        /// <summary>
        /// 任务对象
        /// </summary>
        public dynamic TaskObject { get; set; }
        /// <summary>
        /// 进程ID
        /// </summary>
        public int ProcessId { get; set; }
        
        /// <summary>
        /// 错误(异常)对象
        /// </summary>
        public Exception Exception { get; set; }
        /// <summary>
        /// 爬虫发生错误事件参数
        /// </summary>
        /// <param name="paramTaskObject">任务对象</param>
        /// <param name="paramProcessId">进程ID</param>
        /// <param name="paramException">错误(异常)对象</param>
        public ParamError(dynamic paramTaskObject, int paramProcessId, Exception paramException)
        {
            TaskObject = paramTaskObject;
            ProcessId = paramProcessId;
            Exception = paramException;
        }
    }
}
