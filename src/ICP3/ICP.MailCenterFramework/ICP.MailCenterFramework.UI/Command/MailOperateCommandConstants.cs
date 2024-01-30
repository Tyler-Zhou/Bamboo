#region Comment

/*
 * 
 * FileName:    MailOperateCommandConstants.cs
 * CreatedOn:   2014/7/9 16:22:20
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->邮件操作命令常量定义
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion


namespace ICP.MailCenterFramework.UI
{
    /// <summary>
    /// 邮件操作命令常量
    /// </summary>
    public class MailOperateCommandConstants
    {
        /// <summary>
        /// 新邮件
        /// </summary>
        public const string Command_NewEmail = "Command_NewEmail";
        /// <summary>
        /// 回复邮件(至发送人)
        /// </summary>
        public const string Command_ReplyEmailToSender = "Command_ReplyEmailToSender";
        /// <summary>
        /// 回复全部(至所有人)
        /// </summary>
        public const string Command_ReplyEmailToAll = "Command_ReplyEmailToAll";
        /// <summary>
        /// 回复全部(至所有人包含附件)
        /// </summary>
        public const string Command_ReplyEmailToAllContainsAttachment = "Command_ReplyEmailToAllContainsAttachment";
        /// <summary>
        /// 邮件归档
        /// </summary>
        public const string Command_EmailArchiving = "Command_EmailArchiving";
        /// <summary>
        /// 从ICP同步通讯录
        /// </summary>
        public const string Command_SynchronAddressBook = "Command_SynchronAddressBook";
        /// <summary>
        /// 批量关联
        /// </summary>
        public const string Command_AssociatedBatch = "Command_AssociatedBatch";
    }
}
