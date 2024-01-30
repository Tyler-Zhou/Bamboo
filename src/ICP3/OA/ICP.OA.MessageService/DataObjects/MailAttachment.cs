using System;

namespace ICP.Message.ServiceInterface
{   
    /// <summary>
    /// 邮件附件类
    /// </summary>
    [Serializable]
    public sealed class MailAttachment
    {
        public String FilePath
        {
            get;
            set;
        }
        private MailAttachmentType type = MailAttachmentType.olByValue;
        /// <summary>
        /// 附件类型，默认为ByValue
        /// </summary>
        public MailAttachmentType Type
        {
            get { return type; }
            set { type = value; }
        }
        public int Position
        {
            get;
            set;
        }
        public String DisplayName
        {
            get;
            set;
        }


    }
}
