#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/11 16:01:07
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace ICP.Monitor.Web.Models
{
    [Serializable]
    public class AuthDataInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }
    }
}