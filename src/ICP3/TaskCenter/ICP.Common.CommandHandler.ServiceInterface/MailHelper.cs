using ICP.Framework.CommonLibrary.Client;
using ICP.MailCenter.UI;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;

namespace ICP.Common.CommandHandler.ServiceInterface
{
    /// <summary>
    /// 邮件中心关于邮件辅助类
    /// </summary>
    public class MailHelper
    {
        #region Fields & Property
        /// <summary>
        /// Work Item
        /// </summary>
        public static WorkItem RootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        /// <summary>
        /// 邮件主键ID
        /// </summary>
        public static Guid IMessageID
        {
            get { return GetMessageInfo().Id; }
        }

        /// <summary>
        /// 邮件发件人地址
        /// </summary>
        public static string SenderEmailAddress
        {
            get { return ClientProperties.EmailAddress; }
        }

        /// <summary>
        /// 邮件MessageID
        /// </summary>
        public static string MessageID
        {
            get { return ClientProperties.MessageID; }
        }

        /// <summary>
        /// 邮件主题
        /// </summary>
        public static string MailSubject
        {
            get { return ClientUtility.GetCurrentMailSubject(); }

        }
        /// <summary>
        /// 将一封邮件MailItem类型转换成Object对象，在使用该对象传参数的地方，就不需要引用Outlook组件
        /// </summary>
        public static object ObjMailItem
        {
            get { return ClientProperties.ObjMailItem; }
        } 
        #endregion

        /// <summary>
        /// 获取邮件消息实体(包含邮件原附件)
        /// </summary>
        /// <returns></returns>
        public static Message.ServiceInterface.Message GetMailInfo()
        {
            Message.ServiceInterface.Message messageInfo =
               RootWorkItem.State[Constants.CurrentMessageKey] as Message.ServiceInterface.Message;

            if (messageInfo != null && messageInfo.Attachments.Count > 0)
            {
                messageInfo.Attachments = GetAttachments();
            }

            return messageInfo;
        }

        /// <summary>
        /// 邮件中心获取上传到服务端数据库中邮件的实体
        /// </summary>
        /// <returns></returns>
        public static Message.ServiceInterface.Message GetMessageInfo()
        {
            return GetMessageInfo(RootWorkItem.State[Constants.CurrentMessageKey] as Message.ServiceInterface.Message);
        }

        /// <summary>
        /// 将Message实体中一些不需要的属性值过滤，减少网络带宽
        /// </summary>
        /// <param name="messageInfo"></param>
        /// <returns></returns>
        public static Message.ServiceInterface.Message GetMessageInfo(Message.ServiceInterface.Message messageInfo)
        {
           return ClientUtility.GetMessageInfo(messageInfo);
        }

        /// <summary>
        /// 邮件所有附件（映射本地客户端路劲）
        /// </summary>
        /// <returns></returns>
        public static List<AttachmentContent> GetAttachments()
        {
            return ClientUtility.GetRealAttachmentList();
        }
    }
}
