#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/3/31 17:00:00
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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cityocean.Crawl.ServiceInterface
{
    /// <summary>
    /// 任务-码头箱状态
    /// </summary>
    [Serializable]
    public sealed class TaskTerminalContainerStatus
    {
        /// <summary>
        /// 箱ID(ContainerID)
        /// </summary>
        [TableField]
        public Guid TaskID { get; set; }
        /// <summary>
        /// 码头ID
        /// </summary>
        [TableField]
        public Guid TerminalID { get; set; }
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
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public HandleStatus HandleStatus { get; set; }

        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNO { get; set; }
        
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

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            var obj = formatter.Deserialize(stream) as TaskTerminalContainerStatus;
            return obj;
        }
    }
}
