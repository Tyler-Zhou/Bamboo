using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.MailCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.OA.ServiceInterface.DataObjects;
using System.Runtime.Serialization;
using ICP.Message.ServiceInterface;

namespace ICP.DataCache.ServiceInterface1
{  
    /// <summary>
    /// 业务Email/Fax/EDI历史记录实体类
    /// </summary>
   [Serializable]
   public class CommunicationHistory:Message.ServiceInterface.Message
    {
       public CommunicationHistory()
       {
           Flag = MessageFlag.UnRead;
           Attachments = new List<AttachmentContent>();
           Way = MessageWay.Send;
           Priority = MessagePriority.Normal;
           BodyFormat = BodyFormat.olFormatPlain;
       }
      
       public Guid OperationId { get; set; }
       public ICP.Framework.CommonLibrary.Common.OperationType OperationType { get; set; }
       public ICP.Framework.CommonLibrary.Common.FormType FormType {get;set;}
     
       
    }
}
