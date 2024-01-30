#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/3/30 18:00:43
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using Cityocean.Crawl.CommonLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Cityocean.Crawl.ServiceInterface
{
    /// <summary>
    /// 任务-货物跟踪
    /// </summary>
    [Serializable]
    public sealed class TaskCargoTracking
    {
        /// <summary>
        /// 任务ID(ContainerID)
        /// </summary>
        [TableField]
        public Guid TaskID { get; set; }

        /// <summary>
        /// CityOcean 船东ID
        /// </summary>
        [TableField]
        public Guid COCarrierID { get; set; }
        
        /// <summary>
        /// 箱动态HTML
        /// </summary>
        [TableField]
        public string HTMLContent { get; set; }

        /// <summary>
        /// 失败描述
        /// </summary>
        [TableField]
        public string HTMLDescription { get; set; }
        /// <summary>
        /// 未确认船东
        /// </summary>
        [TableField]
        public bool UnConfirmedCarrier { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [TableField]
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// 提单No
        /// </summary>
        public string BLNO { get; set; }
        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNO { get; set; }

        /// <summary>
        /// CityOcean 船东编码
        /// </summary>
        public string COCarrierCode { get; set; }

        /// <summary>
        /// ETD
        /// </summary>
        public DateTime ETD { get; set; }
        
        
        /// <summary>
        /// 网站ID
        /// </summary>
        public Guid WebsiteID { get; set; }
        /// <summary>
        /// 网站编码
        /// </summary>
        public string WebsiteCode { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public HandleStatus HandleStatus { get; set; }
    }
}
