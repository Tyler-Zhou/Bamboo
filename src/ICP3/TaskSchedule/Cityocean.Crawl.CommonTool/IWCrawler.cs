#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/6/16 18:14:44
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
    /// 
    /// </summary>
    public interface IWCrawler
    {
        /// <summary>
        /// 进程ID
        /// </summary>
        int ProcessId { get; set; }
        /// <summary>
        /// 爬虫启动动作
        /// </summary>
        Action<ParamStart> StartAction { get; set; }
        /// <summary>
        /// 爬虫完成动作
        /// </summary>
        Action<ParamCompleted> CompletedAction { get; set; }
        /// <summary>
        /// 爬虫出错动作
        /// </summary>
        Action<ParamError> ErrorAction { get; set; }
        /// <summary>
        /// 爬虫停止动作
        /// </summary>
        Action<ParamStop> StopAction { get; set; }

        /// <summary>
        /// 爬虫
        /// </summary>
        /// <param name="paramObject">任务对象</param>
        /// <param name="paramConfig">配置</param>
        /// <param name="paramCommandTimeOut">命令超时秒数</param>
        /// <param name="paramTaskTimeOut">任务超时时间</param>
        /// <returns></returns>
        void StartCrawl(object paramObject, dynamic paramConfig, int paramCommandTimeOut, TimeSpan paramTaskTimeOut);
    }
}
