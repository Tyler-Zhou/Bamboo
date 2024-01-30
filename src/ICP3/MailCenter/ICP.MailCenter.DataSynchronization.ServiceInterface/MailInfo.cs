using System;
using System.Collections.Generic;

namespace ICP.DataSynchronization.ServiceInterface
{    
    /// <summary>
    /// 邮件信息实体类
    /// </summary>
    [Serializable]
   public class MailInfo
    {
        public String MessageId { get; set; }
        public String Subject { get; set; }
        public String PlainTextBody { get; set; }
        public String HtmlBody { get; set; }
        public String PropertyId { get; set; }
        public List<Attachment> Attachments { get; set; }
        
    }
}
