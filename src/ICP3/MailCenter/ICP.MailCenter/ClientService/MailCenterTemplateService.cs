using ICP.MailCenter.ServiceInterface;


namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 调用邮件模板发送邮件服务实现
    /// </summary>
    public class MailCenterTemplateService : IMailCenterTemplateService
    {

        public OutlookService OutLookService
        {
            get { return new OutlookService(); }
        }
        /// <summary>
        /// 提取邮件信息内容
        /// </summary>
        public EmailTemplateGetter EmailTemplateGetter
        {
            get { return new EmailTemplateGetter();}
        }
        //ServiceClient.GetService<IOutLookService>()
        public void SendMailWithTemplate(ICP.Message.ServiceInterface.Message mail, bool isEnglish, string sectionName, string itemKey, object[] values)
        {
            EmailTemplateItemData template = null;
            if (isEnglish)
            {
                template = EmailTemplateGetter.Get("en-US", sectionName, itemKey, values);
            }
            else
            {
                template = EmailTemplateGetter.Get("zh-CN", sectionName, itemKey, values);
            }
            mail.Body = template.Body;
            mail.Subject = template.Subject;
            OutLookService.Send(mail);
        }

        public void SendMailWithTemplate(ICP.Message.ServiceInterface.Message mail, bool isEnglish, string itemKey, object[] values)
        {
            EmailTemplateItemData template = null;
            if (isEnglish)
            {
                template = EmailTemplateGetter.Get("en-US", "Common", itemKey, values);
            }
            else
            {
                template = EmailTemplateGetter.Get("zh-CN", "Common", itemKey, values);
            }
            if (template != null)
            {
                mail.Body = template.Body;
                mail.Subject = template.Subject;
            }
            OutLookService.Send(mail);
        }
        public void SendMailWithTemplateEN(ICP.Message.ServiceInterface.Message mail, string sectionName, string itemKey, object[] values)
        {
            EmailTemplateItemData template = EmailTemplateGetter.Get("en-US", sectionName, itemKey, values);
            mail.Body = template.Body;
            mail.Subject = template.Subject;
            OutLookService.Send(mail);
        }

        public void SendMailWithTemplateCN(ICP.Message.ServiceInterface.Message mail, string sectionName, string itemKey, object[] values)
        {
            EmailTemplateItemData template = EmailTemplateGetter.Get("zh-CN", sectionName, itemKey, values);
            mail.Body = template.Body;
            mail.Subject = template.Subject;
            OutLookService.Send(mail);
        }


        public void SendMailWithTemplateEN(ICP.Message.ServiceInterface.Message mail, string itemKey, object[] values)
        {
            EmailTemplateItemData template = EmailTemplateGetter.Get("en-US", "Common", itemKey, values);
            mail.Body = template.Body;
            mail.Subject = template.Subject;
            OutLookService.Send(mail);
        }

        /// <summary>
        /// 调用邮件英文模板Common节点弹出邮件发送界面
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        public void SendMailWithTemplateCN(ICP.Message.ServiceInterface.Message mail, string itemKey, object[] values)
        {
            EmailTemplateItemData template = EmailTemplateGetter.Get("zh-CN", "Common", itemKey, values);
            mail.Body = template.Body;
            mail.Subject = template.Subject;
            OutLookService.Send(mail);
        }
        /// <summary>
        /// 邮件自动发送
        /// </summary>
        /// <param name="mail">邮件信息实体类</param>
        /// <param name="isEnglish">是否发送英文版本</param>
        /// <param name="itemKey">发送邮件模版名称</param>
        /// <param name="values">发送模版内容</param>
        public void AutoSendMailWithTemplate(ICP.Message.ServiceInterface.Message mail, bool isEnglish, string itemKey, object[] values)
        {

            EmailTemplateItemData template = null;
            if (isEnglish)
            {
                template = EmailTemplateGetter.Get("en-US", "Common", itemKey, values);
            }
            else
            {
                template = EmailTemplateGetter.Get("zh-CN", "Common", itemKey, values);
            }
            mail.Body = template.Body;
            mail.Subject = template.Subject;
            OutLookService.AutoSend(mail);
        }

        
    }
}
