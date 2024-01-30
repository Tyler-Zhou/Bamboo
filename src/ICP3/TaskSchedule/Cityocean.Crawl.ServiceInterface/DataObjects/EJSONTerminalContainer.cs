#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/9/15 14:07:23
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace Cityocean.Crawl.ServiceInterface
{
    /// <summary>
    /// Terminal JSON Result - baseInfoLst
    /// </summary>
    [Serializable]
    public class EJSONTerminalContainer
    {
        /// <summary>
        /// 柜号
        /// </summary>
        public string ctnrno { get; set; }
        /// <summary>
        /// 提单号
        /// </summary>
        public string blno { get; set; }
        /// <summary>
        /// 进出口类型
        /// </summary>
        public string ieid { get; set; }
        /// <summary>
        /// 码头编码
        /// </summary>
        public string terminalcd { get; set; }
        /// <summary>
        /// 报关状态
        /// </summary>
        public string customstatus { get; set; }
        /// <summary>
        /// 报关时间
        /// </summary>
        public string customtime { get; set; }
        /// <summary>
        /// 进场时间
        /// </summary>
        public string intime { get; set; }
        /// <summary>
        /// 出场时间
        /// </summary>
        public string outtime { get; set; }
    }
}
