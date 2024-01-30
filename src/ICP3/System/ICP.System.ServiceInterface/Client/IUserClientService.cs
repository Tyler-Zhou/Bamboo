using System;

namespace ICP.Sys.ServiceInterface.Client
{
    /// <summary>
    /// 用户管理客户端服务
    /// </summary>
    public interface IUserClientService
    {
        /// <summary>
        /// 显示邮件帐号信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        object ShowEMailAccountInfo(Guid userID);


        /// <summary>
        ///  显示用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        object ShowUserInfo(Guid userID);
    }
}
