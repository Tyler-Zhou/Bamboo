#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/1/9 星期二 15:51:37
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
    /// 
    /// </summary>
    [Serializable]
    public class TaskCrawlData
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        [TableField]
        public Guid TaskID { get; set; }
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
        /// 更新时间
        /// </summary>
        [TableField]
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// 备用字段1
        /// </summary>
        [TableField]
        public string SpareField1 { get; set; }
        /// <summary>
        /// 备用字段2
        /// </summary>
        [TableField]
        public string SpareField2 { get; set; }
        /// <summary>
        /// 备用字段3
        /// </summary>
        [TableField]
        public string SpareField3 { get; set; }
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
