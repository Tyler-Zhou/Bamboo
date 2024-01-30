#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/10 15:09:38
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace Cityocean.Crawl.ServiceInterface
{
    /// <summary>
    /// 码头箱状态服务
    /// </summary>
    public interface ITerminalContainerStatusService
    {
        /// <summary>
        /// 箱号
        /// </summary>
        Guid TaskID { get; set; }
        /// <summary>
        /// 抓取数据
        /// </summary>
        void CrawlerData();
    }
}
