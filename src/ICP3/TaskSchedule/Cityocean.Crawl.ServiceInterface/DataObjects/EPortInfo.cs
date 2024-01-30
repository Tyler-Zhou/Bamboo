#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/5/9 15:27:14
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Runtime.Serialization;

namespace Cityocean.Crawl.ServiceInterface
{
    /// <summary>
    /// 港口信息
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://sample", Name = "PortInfo")]
    public class EPortInfo
    {
        /// <summary>
        /// 港口ID
        /// </summary>
        [DataMember(Name = "ID")]
        public string ID { get; set; }
        /// <summary>
        /// 港口编码
        /// </summary>
        [DataMember(Name = "Code")]
        public string Code { get; set; }
        /// <summary>
        /// 港口名称
        /// </summary>
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        /// <summary>
        /// 港口描述
        /// </summary>
        [DataMember(Name = "Description")]
        public string Description { get; set; }
        /// <summary>
        /// 港口所属国家
        /// </summary>
        [DataMember(Name = "Country")]
        public string Country { get; set; }
    }
}
