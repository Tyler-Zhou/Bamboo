using System;

namespace ICP.DataSynchronization.ServiceInterface
{
    /// <summary>
    /// 邮件文件搜素参数类
    /// </summary>
    [Serializable]
    public sealed class MailSearchCriteria
    {
        /// <summary>
        /// 邮件MessageId
        /// </summary>
        public String MessageId { get; set; }
        /// <summary>
        /// 邮件自定义属性的键值
        /// </summary>
        public String PropertyId { get; set; }
        /// <summary>
        /// 邮件的某个接收人地址(用于定位邮件所属的文件夹)
        /// </summary>
        public String RecepientAddress { get; set; }

        public MailSearchCriteria() { }
        public MailSearchCriteria(String messageId, String propertyId, String recepientAddress)
            : this()
        {
            this.MessageId = messageId;
            this.PropertyId = propertyId;
            this.RecepientAddress = recepientAddress;
        }
        public MailSearchCriteria(String messageId, String recepientAddress) : this(messageId, String.Empty, recepientAddress) { }
        // public MailSearchCriteria(String propertyId, String recepientAddress) : this(String.Empty, propertyId, recepientAddress) { }

        public override String ToString()
        {
            return String.Format("MessageId: {0},PropertyId: {1},RecepientAddress: {2}", MessageId, PropertyId, RecepientAddress);
        }

    }

}
