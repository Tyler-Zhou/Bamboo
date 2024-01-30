#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/3/24 18:03:18
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
using Cityocean.Crawl.CommonLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cityocean.Crawl.ServiceInterface
{
    /// <summary>
    /// 任务-码头船期
    /// </summary>
    [Serializable]
    public sealed class TaskTerminalVesselSchedule
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        [TableField]
        public Guid TaskID { get; set; }

        /// <summary>
        /// 码头ID
        /// </summary>
        [TableField]
        public Guid TerminalID { get; set; }

        /// <summary>
        /// 船期(HTML)
        /// </summary>
        [TableField]
        public string HTMLContent { get; set; }

        /// <summary>
        /// HTML描述
        /// </summary>
        [TableField]
        public string HTMLDescription
        {
            get;
            set;
        }
        /// <summary>
        /// 船期(JSON)
        /// </summary>
        [TableField]
        public string JSONContent { get; set; }
        /// <summary>
        /// 解析描述
        /// </summary>
        [TableField]
        public string JSONDescription { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [TableField]
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 码头编码
        /// </summary>
        public string TerminalCode { get; set; }

        /// <summary>
        /// 网站ID
        /// </summary>
        public Guid WebsiteID { get; set; }

        /// <summary>
        /// 网站编码
        /// </summary>
        public string WebsiteCode { get; set; }

        private List<TerminalVesselSchedule> _ListContent;
        /// <summary>
        /// 码头船期列表
        /// </summary>
        [JsonIgnore]
        public List<TerminalVesselSchedule> ListContent
        {
            get { return _ListContent ?? (_ListContent = new List<TerminalVesselSchedule>()); }
        }
        
        /// <summary>
        /// 处理状态
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public HandleStatus HandleStatus { get; set; }
    }
}
