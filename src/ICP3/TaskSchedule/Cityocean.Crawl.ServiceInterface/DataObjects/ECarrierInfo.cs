#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/5/9 15:43:57
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
    /// 船东实体
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://sample", Name = "CarrierInfo")]
    public class ECarrierInfo
    {
        /// <summary>
        /// 船东ID
        /// </summary>
        [DataMember(Name = "ID")]
        public string ID { get; set; }
        /// <summary>
        /// 船东编码
        /// </summary>
        [DataMember(Name = "Code")]
        public string Code { get; set; }
    }
}
