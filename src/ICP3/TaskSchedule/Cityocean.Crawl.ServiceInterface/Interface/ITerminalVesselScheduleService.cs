#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/7 13:38:38
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.ServiceModel;

namespace Cityocean.Crawl.ServiceInterface
{
    /// <summary>
    /// 服务-码头船期
    /// </summary>
    [ServiceContract]
    public interface ITerminalVesselScheduleService
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime StartDate { get; set; }
        /// <summary>
        /// 抓取数据
        /// </summary>
        void CrawlerData();
        /// <summary>
        /// 解析数据
        /// </summary>
        void AnalysisData();
    }
}
