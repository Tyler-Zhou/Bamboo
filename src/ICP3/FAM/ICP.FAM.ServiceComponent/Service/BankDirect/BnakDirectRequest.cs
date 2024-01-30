using System;

namespace ICP.FAM.ServiceComponent
{
    /// <summary>
    /// 银行API请求对象
    /// </summary>
    [Serializable]
    public class BnakDirectRequest
    {
        #region 用户ID
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid userid { get; set; }
        #endregion

        #region 请求时间
        /// <summary>
        /// 请求时间
        /// </summary>
        public string date { get; set; }
        #endregion

        #region 银行编码
        /// <summary>
        /// 银行编码
        /// </summary>
        public string bankcode { get; set; }

        #endregion

        #region 请求数据
        /// <summary>
        /// 请求数据
        /// </summary>
        public string data { set; get; }
        #endregion
    }
}
