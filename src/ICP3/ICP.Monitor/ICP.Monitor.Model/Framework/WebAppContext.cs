#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/3 16:11:45
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace ICP.Monitor.Model.Framework
{
    /// <summary>
    /// Web程序上下文
    /// </summary>
    [Serializable]
    public class WebAppContext
    {
        /// <summary>
        /// 会话ID
        /// </summary>
        public string SessionID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// 语言环境
        /// </summary>
        public bool IsEnglish { get; set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnLine
        {
            get { return string.IsNullOrEmpty(UserName); }
        }
        /// <summary>
        /// 局域网IP
        /// </summary>
        public string InternetIP { get; set; }
        /// <summary>
        /// 内部IP
        /// </summary>
        public string IntranetIP { get; set; }
        /// <summary>
        ///物理地址
        /// </summary>
        public string MacAddress { get; set; }
        /// <summary>
        /// 是否是Get请求
        /// </summary>
        public bool IsGet { get; set; }
        /// <summary>
        /// 是否是Ajax请求
        /// </summary>
        public bool IsAjax { get; set; }
        /// <summary>
        /// 异常捕捉
        /// </summary>
        public string Exception { get; set; }
    }
}
