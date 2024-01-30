using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;
using ICP.Message.ServiceInterface;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface ICommunicationHistoryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessContext"></param>
        /// <returns></returns>
        [OperationContract]
        List<CommunicationHistory> GetCommunicationHistoryList(BusinessOperationContext businessContext);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        [OperationContract]
        ManyResult[] SaveCommunicationHistoryEntry(CommunicationHistory entry);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailId"></param>
        /// <param name="attachmentIds"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetAttachmentWithAttachmentId")]
        List<AttachmentContent> GetAttachment(Guid mailId, List<Guid> attachmentIds);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailId"></param>
        /// <param name="attachmentName"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetAttachmentWithAttachmentName")]
        AttachmentContent GetAttachment(Guid mailId, String attachmentName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        CommunicationHistory GetCommunicationHistoryDetailInfo(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contactstage"></param>
        [OperationContract]
        void SetCommunicationHistoryStage(Guid id, string contactstage);
    }
}
