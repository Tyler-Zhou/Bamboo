using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.MailCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;
using ICP.Message.ServiceInterface;

namespace ICP.DataCache.ServiceInterface1
{  
    [ServiceContract]
   public interface ICommunicationHistoryService
    {  
        [OperationContract]
       List<CommunicationHistory> GetCommunicationHistoryList(BusinessOperationContext businessContext);
        [OperationContract]
        ManyResult[] SaveCommunicationHistoryEntry(CommunicationHistory entry);
        [OperationContract(Name = "GetAttachmentWithAttachmentId")]
        List<AttachmentContent> GetAttachment(Guid mailId, List<Guid> attachmentIds);
        [OperationContract(Name="GetAttachmentWithAttachmentName")]
       AttachmentContent GetAttachment(Guid mailId, String attachmentName);
        [OperationContract]
        CommunicationHistory GetCommunicationHistoryDetailInfo(Guid id);
    }
}
