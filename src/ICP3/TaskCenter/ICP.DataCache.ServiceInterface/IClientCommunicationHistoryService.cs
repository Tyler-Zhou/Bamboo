using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.MailCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
namespace ICP.DataCache.ServiceInterface1
{  
    /// <summary>
    /// 沟通历史记录客户端服务接口
    /// </summary>
    public interface IClientCommunicationHistoryService
    {  
        /// <summary>
        /// 业务面板获取沟通历史记录列表
        /// </summary>
        /// <param name="businessContext"></param>
        /// <returns></returns>
        List<CommunicationHistory> GetCommunicationHistoryList(BusinessOperationContext businessContext);
        /// <summary>
        /// 保存沟通历史记录
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        SingleResult SaveCommunicationHistoryEntry(CommunicationHistory entry);
        /// <summary>
        /// 获取记录下的所有附件(包含附件内容)
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="attachmentIds"></param>
        /// <returns></returns>
        List<AttachmentContent> GetAttachment(Guid messageId, List<Guid> attachmentIds);
        /// <summary>
        /// 暂未实现
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="attachmentName"></param>
        /// <returns></returns>
        AttachmentContent GetAttachment(Guid messageId, String attachmentName);
        /// <summary>
        /// 获取记录详细详细(不包含附件内容)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CommunicationHistory GetCommunicationHistoryDetailInfo(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="attachmentName"></param>
        void Open(Guid messageId,String attachmentName);
        /// <summary>
        /// 打开附件
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        string Open(Guid messageId, Guid attachmentId);
        /// <summary>
        /// 保存附件
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="attachmentId"></param>
        /// <param name="displayName"></param>
        void SaveAs(Guid messageId, Guid attachmentId, string displayName);

    }
}
