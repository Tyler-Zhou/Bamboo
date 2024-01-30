#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/7 16:50:24
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
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Cityocean.Crawl.ServiceInterface
{
    /// <summary>
    /// 船期实体
    /// </summary>
    [Serializable]
    public sealed class TerminalVesselSchedule : ICloneable
    {
        /// <summary>
        /// 码头ID
        /// </summary>
        [TableField]
        public Guid TerminalID { get; set; }

        /// <summary>
        /// 船名
        /// </summary>
        [TableField]
        public string VesselName { get; set; }

        /// <summary>
        /// 进口航次
        /// </summary>
        [TableField]
        public string InVoyageNo { get; set; }

        /// <summary>
        /// 进口航次-编号
        /// </summary>
        [TableField]
        public string InVoyageNumber { get; set; }
        /// <summary>
        /// 进口航次-航向
        /// </summary>
        [TableField]
        public string InVoyageDirection { get; set; }

        /// <summary>
        /// 出口航次
        /// </summary>
        [TableField]
        public string OutVoyageNo { get; set; }

        /// <summary>
        /// 出口航次-编号
        /// </summary>
        [TableField]
        public string OutVoyageNumber { get; set; }
        /// <summary>
        /// 出口航次-航向
        /// </summary>
        [TableField]
        public string OutVoyageDirection { get; set; }

        /// <summary>
        /// 到港日
        /// </summary>
        [TableField]
        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime? ArrivalDate { get; set; }

        /// <summary>
        /// 离港日
        /// </summary>
        [TableField]
        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime? DepartureDate { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        [TableField]
        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime UpdateDate { get; set; }

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
            var obj = formatter.Deserialize(stream) as TerminalVesselSchedule;
            return obj;
        }
    }
}
