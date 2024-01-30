#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/10 15:10:27
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

namespace Cityocean.Crawl.ServiceInterface
{
    /// <summary>
    /// 货物跟踪服务
    /// </summary>
    public interface ICargoTrackingService
    {
        /// <summary>
        /// 箱号
        /// </summary>
        Guid SingleID { get; set; }

        /// <summary>
        /// 抓取数据
        /// </summary>
        void CrawlerData();
        /// <summary>
        /// 解析数据
        /// </summary>
        void AnalysisData();

        /// <summary>
        /// 获取船东箱动态
        /// </summary>
        /// <param name="paramCOCarrierID">鹏城海船东ID</param>
        /// <param name="paramBLNo">提单号</param>
        /// <param name="paramCTNRNo">箱号</param>
        /// <returns>箱动态(List列表)</returns>
        List<ContainerDynamic> GetCargoTracking(Guid paramCOCarrierID, string paramBLNo, string paramCTNRNo);
    }
}
