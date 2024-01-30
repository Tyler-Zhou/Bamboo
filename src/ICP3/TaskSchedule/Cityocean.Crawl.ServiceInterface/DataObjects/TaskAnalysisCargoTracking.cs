#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/11/12 12:31:11
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using Cityocean.Crawl.CommonLibrary;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cityocean.Crawl.ServiceInterface
{
    /// <summary>
    /// 解析动态任务
    /// </summary>
    [Serializable]
    public sealed class TaskAnalysisCargoTracking
    {
        /// <summary>
        /// ID
        /// </summary>
        [TableField]
        public Guid ID { get; set; }

        /// <summary>
        /// 动态描述
        /// </summary>
        [TableField]
        public string JSONContent { get; set; }

        /// <summary>
        /// 失败描述
        /// </summary>
        [TableField]
        public string JSONDescription { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [TableField]
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public HandleStatus HandleStatus { get; set; }
        /// <summary>
        /// 箱ID
        /// </summary>
        public Guid ContainerID { get; set; }
        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNO { get; set; }

        /// <summary>
        /// MBL NO
        /// </summary>
        public string MBLNO { get; set; }

        /// <summary>
        /// 箱动态HTML
        /// </summary>
        public string HTMLContent { get; set; }

        /// <summary>
        /// 网站ID
        /// </summary>
        public Guid WebsiteID { get; set; }

        /// <summary>
        /// 网站编码
        /// </summary>
        public string WebsiteCode { get; set; }

    }
}
