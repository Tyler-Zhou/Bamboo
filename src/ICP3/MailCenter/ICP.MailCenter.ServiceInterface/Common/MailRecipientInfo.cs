using System.Collections.Generic;

namespace ICP.MailCenter.ServiceInterface
{
    /// <summary>
    /// 邮件接收方信息类
    /// </summary>
    public partial class MailRecipientInfo
    {
        /// <summary>
        /// 邮件接收者类型 0:sender 1:To 2:cc 3:BCC
        /// </summary>
        public int RecipientType { get; set; }

        public List<Contacts> Contacts { get; set; }
    }
    /// <summary>
    /// 联系人
    /// </summary>
    public partial class Contacts
    {
        /// <summary>
        /// 显示名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string EmailAddress { get; set; }
    }

    /// <summary>
    /// outlook单个文件夹信息
    /// </summary>
    public partial class FolderInfo
    {
        public string Name { get; set; }

        public string FullPath { get; set; }

        public string EntryID { get; set; }
    }
}
