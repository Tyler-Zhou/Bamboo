#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/5 9:49:51
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
    /// 公用服务接口
    /// </summary>
    public interface ICommonService
    {
        /// <summary>
        /// 获取船东配置
        /// </summary>
        /// <param name="paramCrawlType">抓取类型</param>
        /// <returns></returns>
        List<CrawlConfig> GetWebsiteConfigs(CrawlType? paramCrawlType = null);
        /// <summary>
        /// 获取船东信息
        /// </summary>
        /// <param name="paramCarrierInfo">船东信息</param>
        /// <param name="paramCOwner">船东所属</param>
        /// <returns></returns>
        List<ECarrierInfo> GetCarrierInfos(string paramCarrierInfo, CarrierOwner paramCOwner);
        /// <summary>
        /// 添加船东
        /// </summary>
        void AddCarrier(string paramOwner);
        /// <summary>
        /// 保存异常日志
        /// </summary>
        /// <param name="description">错误描述</param>
        /// <param name="createTime">发生日期</param>
        void SaveErrorLog(string description, DateTime createTime);
    }
}
