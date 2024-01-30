using System;
using System.Collections.Generic;

namespace ICP.Message.ServiceInterface
{
    /// <summary>
    /// 邮件实体类
    /// </summary>
    [Serializable]
    public class Mail
    {

        public Mail()
        {
            UserProperties = new MessageUserPropertiesObject();
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public String Subject
        {
            get;
            set;
        }


        public String SenderName
        {
            get;
            set;

        }
        public String SenderAddress
        {
            get;
            set;
        }
        public String To
        {
            get;
            set;
        }

        public DateTime SendTime
        {
            get;
            set;
        }

        public String CC
        {
            get;
            set;
        }

        public String Body
        {
            get;
            set;
        }
        public BodyFormat BodyFormat
        {
            get;
            set;
        }
        public String HtmlBody
        {
            get;
            set;
        }

        public List<MailAttachment> Attachments
        {
            get;
            set;
        }
        public MessageUserPropertiesObject UserProperties { get; set; }

    }

}
