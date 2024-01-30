using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;

namespace ICP.DataCache.ServiceInterface
{  
    /// <summary>
    /// 业务Email/Fax/EDI历史记录实体类
    /// </summary>
   [Serializable]
   public class CommunicationHistory:Message.ServiceInterface.Message
    {
       /// <summary>
        /// 业务Email/Fax/EDI历史记录实体类
       /// </summary>
       public CommunicationHistory()
       {
           Flag = MessageFlag.UnRead;
           Attachments = new List<AttachmentContent>();
           Way = MessageWay.Send;
           Priority = MessagePriority.Normal;
           BodyFormat = BodyFormat.olFormatPlain;
       }
       /// <summary>
       /// 是否选择
       /// </summary>
       public  bool IsChoose { get; set; }
      /// <summary>
      /// 业务ID
      /// </summary>
       public Guid OperationId { get; set; }
       /// <summary>
       /// 业务类型
       /// </summary>
       public OperationType OperationType { get; set; }
       /// <summary>
       /// 表单类型
       /// </summary>
       public FormType FormType {get;set;}
       /// <summary>
       /// EntryId
       /// </summary>
       public string EntryId { get; set; }

       /// <summary>
       /// Remark
       /// </summary>
       public string Remark { get; set; }
       
    }
}
