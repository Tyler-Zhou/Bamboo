using System;
using System.Collections.Generic;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.ServiceInterface;
using ICP.Message.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.DataCache.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FCM.Common.ServiceComponent
{
    /// <summary>
    /// 外部代理分发文件实现类
    /// </summary>
    public class ExternalAgentDocumentDispatchService : IExternalAgentDocumentDispatchService
    {
        /// <summary>
        /// 当前登陆用户ID
        /// </summary>
        public Guid UserId
        {
            get { return ApplicationContext.Current.UserId; }
        }
        /// <summary>
        /// 语言环境
        /// </summary>
        public bool IsEnglish
        {
            get { return ApplicationContext.Current.IsEnglish; }
        }
        /// <summary>
        /// 当前登陆用户名
        /// </summary>
        public string UserName
        {
            get { return ApplicationContext.Current.Username; }
        }


        IMessageService messageService;
        IFileService fileService;
        IUserService userService;
        /// <summary>
        /// 通过构造函数注入服务
        /// </summary>
        public ExternalAgentDocumentDispatchService(IMessageService messageService, IFileService fileService, IUserService userService)
        {
            this.messageService = messageService;
            this.fileService = fileService;
            this.userService = userService;
        }


        /// <summary>
        /// 分发外部代理文件
        /// </summary>
        /// <param name="agent">外部代理邮件地址</param>
        /// <param name="context">上下文类对象</param>
        /// <param name="documentIds">文档列表ID集合</param>
        /// <param name="isAgain"></param>
        public void Send(object agent, BusinessOperationContext context, List<Guid> documentIds, bool isAgain)
        {
            string toAddress = agent as string;
            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.CreateBy = UserId;
            string sendAddress = userService.GetUserInfo(UserId).EMail;
            message.SendFrom = sendAddress;
            message.SendTo = toAddress;
            message.UserProperties = GetUserProperties(context);
            //向模版传入的值
            string[] templateValues = { context.SONO, context.SONO, UserName };
            //通过邮件模版获取邮件标题和正本
            EmailTemplateItemData template = IsEnglish ?
                ServiceClient.GetClientService<IEmailTemplateGetter>().GetEn("DocumentDispatch", templateValues)
                : ServiceClient.GetClientService<IEmailTemplateGetter>().Get("DocumentDispatch", templateValues);
            message.Subject = template.Subject;
            message.Body = template.Body;   
            message.HasAttachment = true;
            List<ContentInfo> documents = fileService.GetDocumentContents(documentIds);
            message.Attachments = GetAttachments(documents);
            try
            {              
                messageService.Send(message);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private List<AttachmentContent> GetAttachments(IEnumerable<ContentInfo> documents)
        {
            List<AttachmentContent> contents = new List<AttachmentContent>();
            foreach (ContentInfo document in documents)
            {
                contents.Add(new AttachmentContent { Content = document.Content, Name = document.Name, DisplayName = document.Name });
            }

            return contents;
        }

        private MessageUserPropertiesObject GetUserProperties(BusinessOperationContext context)
        {
            MessageUserPropertiesObject properties = new MessageUserPropertiesObject
            {
                OperationId = context.OperationID,
                OperationType = context.OperationType,
                FormId = context.FormId,
                FormType = context.FormType
            };
            return properties;
        }
    }
}
