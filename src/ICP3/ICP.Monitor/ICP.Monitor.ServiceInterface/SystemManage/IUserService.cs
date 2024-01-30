#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/11 15:13:51
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using ICP.Monitor.Model.Framework;

namespace ICP.Monitor.Interface.SystemManage
{
    public interface IUserService : IBLLBaseService
    {
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userCode">用户名或代码</param>
        /// <param name="password">密码(是否MD5加密后的值?)</param>
        /// <param name="macAddress">客户端Mac地址</param>
        /// <param name="loginTime">客户端登录时间</param>
        /// <returns></returns>
        ELoginInfo AuthUser(string userCode, string password, string macAddress, DateTime loginTime);
    }
}
