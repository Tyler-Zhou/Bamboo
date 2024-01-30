using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;
using Microsoft.Office.Interop.Outlook;


namespace ICP.MailCenter.ServiceInterface
{
    /// <summary>
    /// Outlook服务接口
    /// </summary>
    [EmailCenterServiceHost]
    [ServiceContract]
    public interface IOutLookService
    {
        /// <summary>
        /// Add a New Email 
        /// </summary>
        /// <param name="mail"></param>
       
        void AddNewEmail(Message.ServiceInterface.Message mail);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendTo"></param>
        void AddNewEmail(string sendTo);

        /// <summary>
        /// Reply a email to sender
        /// </summary>
        /// <param name="item"></param>
        void ReplyToSender(object item);

        /// <summary>
        /// Reply to All
        /// </summary>
        /// <param name="item"></param>
        void ReplyToAll(object item);

        /// <summary>
        /// 答复所有邮件(包含附件)
        /// </summary>
        /// <param name="item">答复邮件对象</param>
        void ReplyToAllContainsAttachment(object item);

        /// <summary>
        /// Forward email
        /// </summary>
        /// <param name="item"></param>
        void Forward(object item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        [OperationContract(IsOneWay = true)]
        void Open(Message.ServiceInterface.Message mail);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        [OperationContract(IsOneWay = true)]
        void ReplyAll(Message.ServiceInterface.Message mail);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        [OperationContract(IsOneWay = true)]
        void Reply(Message.ServiceInterface.Message mail);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        [OperationContract(IsOneWay = true)]
        void Forward(Message.ServiceInterface.Message mail);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        [OperationContract(IsOneWay = true)]
        void Send(Message.ServiceInterface.Message mail);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        [OperationContract(IsOneWay = true)]
        void Resend(Message.ServiceInterface.Message mail);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        [OperationContract(IsOneWay = true)]
        void AutoSend(Message.ServiceInterface.Message mail);
        /// <summary>
        /// 通过MessageID查询邮件
        /// </summary>
        /// <param name="strMessageID">MessageID</param>
        /// <returns>MailItem</returns>
        MailItem GetMailItemByMessageID(string strMessageID);

        /// <summary>
        /// 通过MessageID查询并转换成Byte[]
        /// </summary>
        /// <param name="strEntryID">Mail EntryID(GUID)</param>
        /// <param name="strMessageID">MessageID</param>
        /// <param name="strIMessageID">IMessageID</param>
        /// <returns>byte[]</returns>
        byte[] GetByteByMessageID(string strEntryID, string strMessageID,string strIMessageID);
        /// <summary>
        /// 获取邮件另存目录
        /// </summary>
        /// <param name="strEntryID">邮件EntryID</param>
        /// <param name="strMessageID">邮件MessageID</param>
        /// <param name="strIMessageID">消息Guid</param>
        /// <returns>另存目录</returns>
        string GetMailItemSaveAsPath(string strEntryID, string strMessageID, string strIMessageID);

        /// <summary>
        ///打开msg文件
        /// </summary>
        /// <param name="MsgFilePath"></param>
        [OperationContract(Name="OpenMsg",IsOneWay=true)]
        void Open(string MsgFilePath);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailFile"></param>
        /// <returns></returns>
        [OperationContract]
        string ConvertMailToPDF(string mailFile);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [OperationContract(IsOneWay = true)]
        string ConvertMessageToMsg(Message.ServiceInterface.Message message);
    }
}
