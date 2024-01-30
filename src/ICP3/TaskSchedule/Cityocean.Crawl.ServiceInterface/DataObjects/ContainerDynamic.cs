#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/17 11:51:13
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using Cityocean.Crawl.CommonLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cityocean.Crawl.ServiceInterface
{
    /// <summary>
    /// 货物动态
    /// </summary>
    [Serializable]
    public sealed class ContainerDynamic
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        [TableField]
        public Guid ParentID { get; set; }
        /// <summary>
        /// 事件索引
        /// </summary>
        [TableField]
        public int EventIndex { get; set; }
        /// <summary>
        /// 事件时间
        /// </summary>
        [TableField]
        public DateTime EventTime { get; set; }

        /// <summary>
        /// 事件地点
        /// </summary>
        [TableField]
        public string Station { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        [TableField]
        public string StateDescription { get; set; }
        /// <summary>
        /// 箱状态
        /// </summary>
        [TableField]
        [JsonConverter(typeof(StringEnumConverter))]
        public ContainerState State { get; set; }

        /// <summary>
        /// 船名
        /// </summary>
        [TableField]
        public string VesselName { get; set; }

        /// <summary>
        /// 航次
        /// </summary>
        [TableField]
        public string VoyageNumber { get; set; }
        /// <summary>
        /// 是否预计时间
        /// </summary>
        [TableField]
        public bool IsEST { get; set; }
    }
}
