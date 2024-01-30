using System;

namespace ICP.EDI.ServiceInterface.DataObjects
{
    /// <summary>
    /// 邮件对象
    /// </summary>
    [Serializable]
    public class EMailInfo
    {
        /// <summary>
        /// 邮件接收人

        /// </summary>
        public string TO { get; set; }

        /// <summary>
        /// 邮件发送人 
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 抄送

        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// 暗抄送

        /// </summary>
        public string BCC { get; set; }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 附件路径
        /// </summary>
        public string AttachmentFile { get; set; }
    }

    /// <summary>
    /// FTP信息
    /// </summary>
    [Serializable]
    public class FTPFileInfo
    {
        /// <summary>
        /// FTP地址
        /// </summary>
        public string FTPService { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 文件夹路径
        /// </summary>
        public string ClientFilePath { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
