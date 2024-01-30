using System;

namespace ICP.Message.ServiceInterface
{
    /// <summary>
    /// 消息发送服务接口
    /// </summary>
    public interface IClientMessageService : IMessageService, IBusinessMessageService
    {
        /// <summary>
        /// 邮件发送成功后事件
        /// </summary>
        event EventHandler<MessageSendFinishEventArgs> MessageSent;
        /// <summary>
        /// 显示发送界面
        /// </summary>
        /// <param name="entry"></param>
        /// <returns>点击发送,返回True，取消发送返回False</returns>
        bool ShowSendForm(Message message);
        /// <summary>
        /// 显示预览界面
        /// </summary>
        /// <param name="entry"></param>
        void ShowReadForm(Message message);
        /// <summary>
        /// 发送并保存日志
        /// </summary>
        /// <param name="message"></param>
        void SendAndSaveLog(Message message);
        /// <summary>
        /// 将邮件文件转换为PDF文件
        /// </summary>
        /// <param name="mailFile"></param>
        /// <returns></returns>
        string ConvertMailToPDF(string mailFile);

        /// <summary>
        /// 打开Msg文件
        /// </summary>
        /// <param name="MsgFilePath"></param>
        void Open(string MsgFilePath);
        /// <summary>
        /// 通过传真或者邮件中心打开消息
        /// </summary>
        /// <param name="id">消息主键Id</param>
        void Open(Guid id);
        /// <summary>
        /// 答复全部
        /// </summary>
        /// <param name="originalId">原始ID</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="message">Message</param>
        void ReplyAll(Guid originalId, DateTime? updateDate, Message message);
        /// <summary>
        /// 答复全部(含附件)
        /// </summary>
        /// <param name="message">Message</param>
        void ReplyAllAttachment(Message message);
        /// <summary>
        /// 通过EntryID得到MailItem对象
        /// </summary>
        /// <param name="entryID">Mailtem EntryID</param>
        /// <param name="messageID">MessageID</param>
        object GetMailItemByEntryID(string entryID,string messageID);

        /// <summary>
        /// 获取MailItem并打开
        /// </summary>
        /// <param name="entryID">EntryID</param>
        /// <param name="messageID">MessageID</param>
        void GetMailItemAndOpen(string entryID, string messageID);
        /// <summary>
        /// 把Message转换为Msg文件，并在邮件客户端显示
        /// </summary>
        /// <param name="message"></param>
        void ConvertMessageToMsg(Message message);

        /// <summary>
        /// 传入messageID得到附件流另存msg文件后打开
        /// </summary>
        /// <param name="messageId">消息ID</param>
        string SaveAstMsgFile(string messageId);
        /// <summary>
        /// 打开Msg文件
        /// </summary>
        /// <param name="messageId">邮件ID</param>
        void OpenMsgFile(string messageId);
    }
    /// <summary>
    /// 传真发送完成事件参数类
    /// </summary>
    public class MessageSendFinishEventArgs : EventArgs
    {
        Guid oldMessageId;
        Guid newMessageId;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="guid">email或传真发送成功后返回的日志Id</param>
        public MessageSendFinishEventArgs(Guid oldId, Guid newId)
        {
            oldMessageId = oldId;
            newMessageId = newId;
        }
        /// <summary>
        /// 旧传真日志ID
        /// </summary>
        public Guid OldMessageId
        {
            get { return oldMessageId; }

        }
        /// <summary>
        /// 保存后产生的日志Id
        /// </summary>
        public Guid NewMessageId
        {
            get { return newMessageId; }
        }
    }
}
