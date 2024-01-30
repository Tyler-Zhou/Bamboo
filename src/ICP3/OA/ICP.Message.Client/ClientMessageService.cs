using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using ICP.MailCenter.UI;
using ICP.Message.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.OA.ServiceInterface;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Common.ServiceInterface;
using ICP.FileSystem.ServiceInterface;

namespace ICP.Message.Client
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class ClientMessageService : IClientMessageService
    {
        #region 成员变量
        #region 服务
        /// <summary>
        /// Message Service
        /// </summary>
        public IMessageService MessageService
        {
            get
            {
                return ServiceClient.GetService<IMessageService>();
            }
        }
        /// <summary>
        /// Fax Client Service
        /// </summary>
        public IFaxClientService FaxClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFaxClientService>();
            }
        }
        /// <summary>
        /// 沟通历史记录客户端服务接口
        /// </summary>
        public IClientCommunicationHistoryService ClientCommunicationHistoryService
        {
            get
            {
                return ServiceClient.GetClientService<IClientCommunicationHistoryService>();
            }
        }

        public OutlookService GetOutlookService()
        {
            //string savePath = PreSend(message, actionType);
            // if (!string.IsNullOrEmpty(savePath))
            // {
            ClientHelper.EnsureEmailCenterAppStarted();
            //}
            return new OutlookService();

        }

        /// <summary>
        /// 获取Outlook实例但是Outlook不置前
        /// </summary>
        /// <returns></returns>
        public OutlookService GetOutlookServiceAndNotAppStarted() 
        {
            return new OutlookService();
        }


        /// <summary>
        ///  MailBee服务
        /// </summary>
        public IMailBeeService MailBeeService
        {
            get { return ServiceClient.GetService<IMailBeeService>(); }
        }
        #endregion

        #region 委托事件
        /// <summary>
        /// 消息发送
        /// </summary>
        public event EventHandler<MessageSendFinishEventArgs> MessageSent;
        #endregion 
        #endregion

        /// <summary>
        /// 打开Message
        /// 通过Message KeyID(GUID)获取Message后通过重写方法Open打开
        /// </summary>
        /// <param name="id">MessageID(GUID)</param>
        public void Open(Guid id)
        {
            ICP.Message.ServiceInterface.Message message = Get(id);
            if (message != null)
            {
                Open(message);
            }

        }
        /// <summary>
        /// 通过Msg路径从Outlook打开邮件
        /// </summary>
        /// <param name="MsgFilePath">Msg路径</param>
        public void Open(string MsgFilePath)
        {
            GetOutlookService().Open(MsgFilePath);
        }
        /// <summary>
        /// 通过MessageID打开
        /// 判断类型：传真和EDI通过界面打开，邮件通过Outlook打开
        /// </summary>
        /// <param name="message">Message Info</param>
        public void Open(ICP.Message.ServiceInterface.Message message)
        {
            if (message.Type == MessageType.Fax || message.Type == MessageType.EDI)
            {
                FaxClientService.ShowReadForm(message);
            }
            else if (message.Type == MessageType.Email)
            {
                GetMailItemAndOpen(message.EntryID, message.MessageId,message.MessageId, message);
            }
        }
        /// <summary>
        /// 答复
        /// </summary>
        /// <param name="originalId">原始ID</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="message">Message</param>
        public void Reply(Guid originalId, DateTime? updateDate, ICP.Message.ServiceInterface.Message message)
        {
            if (message.Type == MessageType.Fax)
            {
                if (FaxClientService.ShowSendForm(message))
                {
                    MessageService.Reply(originalId, updateDate, message);
                }
            }
            else if (message.Type == MessageType.Email)
            {
                object obj = GetMailItemByMessage(message);
                GetOutlookService().ReplyToSender(obj);
            }
        }
        /// <summary>
        /// 答复全部
        /// </summary>
        /// <param name="originalId">原始ID</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="message">Message</param>
        public void ReplyAll(Guid originalId, DateTime? updateDate, ICP.Message.ServiceInterface.Message message)
        {
            if (message.Type == MessageType.Fax)
            {
                if (FaxClientService.ShowSendForm(message))
                {
                    MessageService.Reply(originalId, updateDate, message);
                }
            }
            else if (message.Type == MessageType.Email)
            {
                object obj = GetMailItemByMessage(message);
                GetOutlookService().ReplyToAll(obj);
            }
        }

        /// <summary>
        /// 答复全部(含附件)
        /// </summary>
        /// <param name="message">Message</param>
        public void ReplyAllAttachment(ICP.Message.ServiceInterface.Message message)
        {
            object obj = GetMailItemByMessage(message);
            GetOutlookService().ReplyToAllContainsAttachment(obj);
        }
        /// <summary>
        /// 转发
        /// </summary>
        /// <param name="originalId">原始ID</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="message">Message</param>
        public void Forward(Guid originalId, DateTime? updateDate, ICP.Message.ServiceInterface.Message message)
        {
            if (message.Type == MessageType.Fax)
            {
                if (FaxClientService.ShowSendForm(message))
                {
                    MessageService.Forward(originalId, updateDate, message);
                }
            }
            else if (message.Type == MessageType.Email)
            {
                GetOutlookService().Forward(message);
            }
        }

        /// <summary>
        /// 获取MailItem并打开
        /// </summary>
        /// <param name="entryID">EntryID</param>
        /// <param name="messageID">IMessageID</param>
        public void GetMailItemAndOpen(string entryID, string iMessageID)
        {
            GetMailItemAndOpen(entryID, iMessageID,string.Empty, null);
        }
        /// <summary>
        /// 获取MailItem并打开
        /// </summary>
        /// <param name="entryID">EntryID</param>
        /// <param name="iMessageID">IMessageID</param>
        /// <param name="strMessage">MessageID</param>
        /// <param name="message">Message实体</param>
        private void GetMailItemAndOpen(string entryID, string iMessageID,string strMessage
            , ServiceInterface.Message message)
        {
            //EntryID不为空时在本地查找邮件
            object obj = null;
            if (!string.IsNullOrEmpty(entryID))
                obj = GetOutlookService().GetMailItemByEntryID(entryID);
            //if (obj == null)
            //{
            //    if (message == null)
            //        message = Get(new Guid(iMessageID));
            //    obj = GetOutlookService().GetMailItemByMessageID(strMessage);
            //}
            if (obj != null)
                GetOutlookService().Open(obj);
            else
            {
                if (message == null)
                    message = Get(new Guid(iMessageID));
                if (message != null)
                {
                    string mPath = GetMailPathByMessage(message);
                    GetOutlookService().OpenMailByProcess(mPath);
                }
            }
        }
        /// <summary>
        /// 重发
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        public SingleResultData Resend(ICP.Message.ServiceInterface.Message message)
        {
            try
            {
                if (message.Type == MessageType.Fax)
                {
                    FaxClientService.Resend(message);
                }
                else if (message.Type == MessageType.Email)
                {

                    GetOutlookService().Resend(message);
                }
                ManyResult result = ChangeState(new Guid[] { message.Id }, new MessageState[] { MessageState.Sending }, new DateTime?[] { message.UpdateDate });
                SingleResultData singleResult = new SingleResultData();
                singleResult.ID = result.Items[0].GetValue<Guid>("ID");
                singleResult.UpdateDate = result.Items[0].GetValue<DateTime?>("UpdateDate");
                return singleResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 传入Message，打开发送界面.返回发送状态
        /// 判断类型：传真通过界面打开，邮件判断用户属性不为空则打开发送界面，否则通过OutLook发送
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>发送状态</returns>
        public bool ShowSendForm(ICP.Message.ServiceInterface.Message message)
        {

            if (message.Type == MessageType.Fax)
            {
                return FaxClientService.ShowSendForm(message);
            }
            else if (message.Type == MessageType.Email)
            {
                if (message.UserProperties != null && !string.IsNullOrEmpty(message.UserProperties.CustomerService))
                {
                    FaxClientService.ShowSendForm(message);
                }
                else
                {
                    GetOutlookServiceAndNotAppStarted().Send(message);
                }
            }
            return true;
        }
        /// <summary>
        /// 传入Message，打开发送界面
        /// 判断类型：传真通过界面打开，否则通过OutLook打开
        /// </summary>
        /// <param name="message">Message</param>
        public void ShowReadForm(ICP.Message.ServiceInterface.Message message)
        {
            if (message.Type == MessageType.Fax || message.Type == MessageType.EDI)
            {
                FaxClientService.ShowReadForm(message);
            }
            else if (message.Type == MessageType.Email)
            {
                GetOutlookServiceAndNotAppStarted().Open(message);
            }
        }
        /// <summary>
        /// 发送及其保存日志
        /// </summary>
        /// <param name="message">Message</param>
        public void SendAndSaveLog(ICP.Message.ServiceInterface.Message message)
        {
            if (message.Type == MessageType.Fax)
            {
                SingleResult result = Send(message);
                message.MessageId = result.GetValue<string>("MessageID");
                message.SendFrom = result.GetValue<string>("SendFrom");
                Save(message);
            }
            else if (message.Type == MessageType.Email)
            {

                GetOutlookService().AutoSend(message);
            }

        }
        private string PreSend(ICP.Message.ServiceInterface.Message message, ActionType actionType)
        {
            string saveFileName = string.Empty;
            if (!ClientHelper.IsEmailCenterAppExists())
            {
                saveFileName = GetSavePath();
                SerializerHelper.SerializeToXMLDocument(new MessageParameter { ActionType = actionType, Message = message }, saveFileName);
            }
            return saveFileName;
        }
        private string GetSavePath()
        {
            string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "emailtemp");
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string fileName = string.Format("{0}.{1}", Guid.NewGuid(), "xml");
            return System.IO.Path.Combine(path, fileName);
        }
        /// <summary>
        /// 通过ID获取Message Info
        /// </summary>
        /// <param name="id">MessageInfo.ID</param>
        /// <returns></returns>
        public ICP.Message.ServiceInterface.Message Get(Guid id)
        {
            try
            {
                ICP.Message.ServiceInterface.Message message = MessageService.Get(id);
                if (message == null) return null;
                if (message.HasAttachment)
                {
                    List<Guid> attachmentIds = (from attachment in message.Attachments
                                                select attachment.Id).ToList();
                    message.Attachments = ClientCommunicationHistoryService.GetAttachment(id, attachmentIds);
                }

                return message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通过MessageID获取Message Info
        /// </summary>
        /// <param name="messageId">Message.MessageID</param>
        /// <returns>Message对象</returns>
        public Message.ServiceInterface.Message GetMessageByMessageId(string messageId)
        {
            try
            {
                ICP.Message.ServiceInterface.Message message = MessageService.GetMessageByMessageId(messageId);
                return message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ManyResult[] Save(ICP.Message.ServiceInterface.Message message)
        {
            if (message.HasAttachment && message.Attachments != null && message.Attachments.Count > 0)
            {
                foreach (AttachmentContent content in message.Attachments)
                {
                    if (!string.IsNullOrEmpty(content.ClientPath) && content.Content == null)
                    {
                        content.Content = IOHelper.ReadFileContentFromDisk(content.ClientPath);
                    }
                }
            }

            return MessageService.Save(message);
        }

        public void Remove(Guid[] ids, DateTime?[] updateDates)
        {
            MessageService.Remove(ids, updateDates);
        }

        public SingleResultData ChangeFlag(Guid id, MessageFlag flag, DateTime? updateDate)
        {
            return MessageService.ChangeFlag(id, flag, updateDate);
        }

        public ManyResult ChangeState(Guid[] id, MessageState[] state, DateTime?[] updateDate)
        {
            return MessageService.ChangeState(id, state, updateDate);
        }


        /// <summary>
        /// 通过EntryID得到MailItem对象
        /// </summary>
        /// <param name="entryID">Mailtem EntryID</param>
        /// <param name="messageID">MessageID(GUID,Message ID)</param>
        public object GetMailItemByEntryID(string entryID, string messageID)
        {
            object obj = null;
            if (!string.IsNullOrEmpty(entryID))
                obj = GetOutlookService().GetMailItemByEntryID(entryID);
            if (obj == null)
            {
                ServiceInterface.Message message = Get(new Guid(messageID));
                if (message != null)
                {
                    obj  = GetMailItemByMessage(message);
                }
            }
            return obj;
        }

        /// <summary>
        /// 通过Message实体获取邮件路径
        /// </summary>
        /// <param name="message">Message实体</param>
        /// <returns>文件路径</returns>
        private string GetMailPathByMessage(ServiceInterface.Message message)
        {
            string path = string.Empty;
            if (message != null)
            {
                try
                {
                    //OpenMsgFile(message.Id.ToString());
                    //将附件表存储msg文件或imap文件转换成Msg后返回路径
                    path = SaveAstMsgFile(message.Id.ToString());

                    if (string.IsNullOrEmpty(path))
                    {
                        //将Message转换成MailItem并另存到临时目录
                        path = GetOutlookService().ConvertMessage2Msg(message, false);
                    }
                }
                catch (Exception ex)
                {
                    WriteLog("", ex);
                }
            }
            return path;
        }

        
        /// <summary>
        /// 通过EntryID得到MailItem对象
        /// </summary>
        /// <param name="message">Message Info</param>
        public object GetMailItemByMessage(ServiceInterface.Message message)
        {
            object obj = null;
            //EntryID不为空时在本地查找邮件
            if (!string.IsNullOrEmpty(message.EntryID))
                obj = GetOutlookService().GetMailItemByEntryID(message.EntryID);
            //if (obj == null && !string.IsNullOrEmpty(message.MessageId))
            //    obj = GetOutlookService().GetMailItemByMessageID(message.MessageId);
            return obj ?? (obj = GetMailItemByMessageInfo(message));
        }

        /// <summary>
        /// 通过MessageInfo获取MailItem
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>MailItem object</returns>
        public object GetMailItemByMessageInfo(ServiceInterface.Message message)
        {
            object obj = null;
            if (message != null)
            {
                //将附件表存储msg文件或imap文件转换成Msg后返回路径
                string path = SaveAstMsgFile(message.Id.ToString());
                //路径不为空则调用转换MailItem方法，否则调用Message转换成MailItem方法
                obj = !string.IsNullOrEmpty(path) ? GetOutlookService().GetMailItemByFilePath(path) : GetOutlookService().GetMailItemByMessage(message);
            }
            return obj;
        }

        /// <summary>
        /// 传入messageID得到附件流另存msg文件后打开
        /// </summary>
        /// <param name="messageId">消息ID</param>
        public string SaveAstMsgFile(string messageId)
        {
            string targetPath = string.Empty;
            List<ContentInfo> listContentInfo = MessageService.GetMailInfo(messageId);
            if (listContentInfo.Any())
            {
                foreach (var c in listContentInfo)
                {
                    //判断文件类型,msg文件直接写入磁盘
                    //另存路径
                    targetPath = System.IO.Path.GetTempPath() + messageId + ".msg";
                    if (c.FileType.Equals("msg"))
                    {
                        MemoryStream m = new MemoryStream(c.Content);
                        FileStream fs = new FileStream(targetPath, FileMode.OpenOrCreate);
                        m.WriteTo(fs);
                        m.Close();
                        fs.Close();
                    }
                    else //非MSG文件则通过转换
                    {
                        try
                        {
                            MailBee.Mime.MailMessage mailMessage = new MailBee.Mime.MailMessage();
                            mailMessage.LoadMessage(c.Content);
                            MailBee.Outlook.MsgConvert MConvert = new MailBee.Outlook.MsgConvert();
                            MConvert.MailMessageToMsg(mailMessage, targetPath);
                        }
                        catch (Exception ex)
                        {
                            targetPath = "";
                            WriteLog("", ex);
                        }
                    }
                }
            }
            return targetPath;
        }

        public SingleResult Send(ICP.Message.ServiceInterface.Message message)
        {
            SingleResult result = null;
            int count = message.Attachments.Count;
            for (int i = 0; i < count; i++)
            {
                if (message.Attachments[i].Content == null)
                {
                    message.Attachments[i].Id = Guid.NewGuid();
                    string attachmentClientPath = message.Attachments[i].ClientPath;
                    message.Attachments[i].Content = IOHelper.ReadFileContentFromDisk(attachmentClientPath);
                }
            }
            Guid messageId = Guid.NewGuid();
            if (message.Type == MessageType.Fax)
            {
                result = FaxClientService.Send(message);
                //TriggerFaxSentEvent(result);
            }
            else if (message.Type == MessageType.Email)
            {
                if (message.UserProperties != null && !string.IsNullOrEmpty(message.UserProperties.CustomerService))
                {
                    result = FaxClientService.Send(message);
                }
                else
                {
                    GetOutlookService().AutoSend(message);
                }

            }

            return result;
        }


        public void ChangeState(string[] messageIds, MessageState[] states, MessageType type)
        {
            MessageService.ChangeState(messageIds, states, type);
        }

        public void Transfer(List<ConfigureObjects> userCompanyList, Guid defaultCompanyID)
        {
            MessageService.Transfer(userCompanyList, defaultCompanyID);
        }

        public List<FaxMessageObjects> GetMessageInfoByCompanyID(Guid companyID)
        {
            return MessageService.GetMessageInfoByCompanyID(companyID);
        }

        public ManyResult ChangeFaxState(Guid[] ids, Guid?[] folderIDs, ReceiveFaxState[] states, DateTime?[] updateDates, DateTime?[] faxUpdateDates)
        {
            return MessageService.ChangeFaxState(ids, folderIDs, states, updateDates, faxUpdateDates);
        }
        public Message.ServiceInterface.Message GetMessageInfoById(Guid id)
        {
            return MessageService.GetMessageInfoById(id);
        }

        public string ConvertMailToPDF(string mailFile)
        {
            return GetOutlookService().ConvertMailToPDF(mailFile);
        }
        
        public void ConvertMessageToMsg(ICP.Message.ServiceInterface.Message message)
        {
            GetOutlookService().ConvertMessageToMsg(message);
        }

        /// <summary>
        /// 打开Msg文件
        /// </summary>
        /// <param name="messageId">邮件的ID</param>
        public void OpenMsgFile(string messageId)
        {
            //读取到文件的路径
            string path = SaveAstMsgFile(messageId);
            if (!string.IsNullOrEmpty(path))
            {
                GetOutlookService().Open(path);
            }
            else
            {
                //处理以前的邮件打开的方式
                Message.ServiceInterface.Message message = GetMessageInfoById(new Guid(messageId));
                if (message != null)
                {
                    Open(message);
                }
            }
        }

        #region IMessageService 成员

        /// <summary>
        /// 返回当前业务的客户的邮件地址
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="values">客户类型</param>
        public List<ICP.Message.ServiceInterface.Message> CustomerMailList(Guid operationId, string values)
        {
            return MessageService.CustomerMailList(operationId, values);
        }
        public List<ICP.Message.ServiceInterface.Message> RuturnMailList(Guid userId, string words, int wordType, string refNo, int refNoType, string customerName, int customerType, int phase, string mails, int mailType, int dateType, DateTime fromDate, DateTime endDate)
        {
            return MessageService.RuturnMailList(userId, words, wordType, refNo, refNoType, customerName, customerType, phase, mails, mailType, dateType, fromDate, endDate);
        }
        public List<ContentInfo> GetMailInfo(string messageId)
        {
            return MessageService.GetMailInfo(messageId);
        }
        #endregion

        #region 写日志

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        void WriteLog(string title, Exception ex)
        {
            try
            {
                string logPath = LocalData.MainPath + "\\LogFiles\\";
                string FileName = String.Format("ICP.Message.Client{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                string FileFullPath = logPath + FileName;
                string WriteText = string.Empty;
                using (System.IO.TextWriter write = System.IO.File.AppendText(FileFullPath))
                {
                    write.WriteLine("----------------------ICP.Message.Client" + title + "--" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "----------------------");
                    write.WriteLine("************************************************************************************");
                    write.WriteLine("Message:" + ex.Message);
                    write.WriteLine("StackTrace:" + ex.StackTrace);
                    write.WriteLine("************************************************************************************");
                    write.Close();
                }
            }
            catch
            {
            }
        }
        #endregion
    }
}
