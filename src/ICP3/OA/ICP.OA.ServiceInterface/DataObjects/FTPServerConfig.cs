

namespace ICP.OA.ServiceInterface.DataObjects
{
    using System;

    /// <summary>
    /// FTP服务器配置信息
    /// </summary>
    [Serializable]
    public class FTPServerConfig
    {
        public FTPServerConfig()
        {
        }

        public FTPServerConfig(
            string host,
            string user,
            string password,
            string basePath)
        {
            this.Host = host;
            this.User = user;
            this.Password = password;
            this.BasePath = basePath;
        }

        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 本地路径
        /// </summary>
        public string BasePath { get; set; }
    }
}
