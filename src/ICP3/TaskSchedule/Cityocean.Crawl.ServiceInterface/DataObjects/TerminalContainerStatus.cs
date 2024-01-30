#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/10/26 17:40:40
 *
 * Description:
 *         ->码头箱状态
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
    /// 码头箱状态
    /// </summary>
    [Serializable]
    public sealed class TerminalContainerStatus
    {
        /// <summary>
        /// 箱ID
        /// </summary>
        [TableField]
        public Guid ContainerID { get; set; }
        /// <summary>
        /// 码头ID
        /// </summary>
        [TableField]
        public Guid TerminalID { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public HandleStatus HandleStatus { get; set; }
        /// <summary>
        /// 报关状态
        /// </summary>
        [TableField]
        public string CustomStatus { get; set; }
        /// <summary>
        /// 报关时间
        /// </summary>
        [TableField]
        public DateTime? CustomTime { get; set; }
        /// <summary>
        /// 进场时间
        /// </summary>
        [TableField]
        public DateTime? InTime { get; set; }
        /// <summary>
        /// 出场时间
        /// </summary>
        [TableField]
        public DateTime? OutTime { get; set; }
        /// <summary>
        /// 出场时间
        /// </summary>
        [TableField]
        public DateTime? LastFreeDate { get; set; }
        /// <summary>
        /// 码头箱状态(HTML)
        /// </summary>
        [TableField]
        public string HTMLContent { get; set; }
        /// <summary>
        /// HTML错误描述
        /// </summary>
        [TableField]
        public string HTMLDescription { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [TableField]
        public DateTime UpdateDate { get; set; }
    }
}
