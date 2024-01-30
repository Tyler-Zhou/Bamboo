#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/9/15 11:44:06
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Cityocean.Crawl.ServiceInterface
{
    /// <summary>
    /// Terminal JSON Result - List
    /// </summary>
    [Serializable]
    public class EJSONTerminalList
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 异常消息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// Containers baseInfoLst
        /// </summary>
        [JsonIgnore]
        public List<EJSONTerminalContainer> Containers { get; set; }
    }
}
