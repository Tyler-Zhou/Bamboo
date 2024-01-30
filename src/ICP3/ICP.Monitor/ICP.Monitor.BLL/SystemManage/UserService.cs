#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/11 15:15:02
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using ICP.Monitor.Interface.SystemManage;
using ICP.Monitor.Model.Framework;

namespace ICP.Monitor.BLL.SystemManage
{
    public class UserService : IUserService
    {
        /// <summary>
        /// DAL User Service
        /// </summary>
        DAL.SystemManage.UserService _UserService = new DAL.SystemManage.UserService();

        private WebAppContext _WebAppContext;
        /// <summary>
        /// Web上下文
        /// </summary>
        public WebAppContext WebAppContext
        {
            get
            {
                return _WebAppContext;
            }
            set
            {
                _WebAppContext = value;
                _UserService.WebAppContext = _WebAppContext;
            }
        }

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userCode">用户名或代码</param>
        /// <param name="password">密码(是否MD5加密后的值?)</param>
        /// <param name="macAddress">客户端Mac地址</param>
        /// <param name="loginTime">客户端登录时间</param>
        /// <returns></returns>
        public ELoginInfo AuthUser(string userCode, string password, string macAddress, DateTime loginTime)
        {
            if (string.IsNullOrEmpty(userCode))
            {
                throw new ApplicationException("帐号不能为空.");
            }
            return _UserService.AuthUser(userCode,password,macAddress,loginTime);
        }
    }
}
