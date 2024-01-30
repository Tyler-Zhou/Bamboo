using System;

namespace ICP.ReportCenter.ServiceInterface
{
    /// <summary>
    /// 服务器报表的登录信息
    /// </summary>
    [Serializable]
    public class ReportServerInfo
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string ReportUrl { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public string ReportUser { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string ReportUserPSW { get; set; }
    }
}
