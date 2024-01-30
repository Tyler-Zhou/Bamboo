
namespace ICP.MailCenter.ServiceInterface
{

    /// <summary>
    /// 邮件模版实现类
    /// </summary>
    public class EmailTemplateGetter : IEmailTemplateGetter
    {

        public EmailTemplateGetvaluation _emailTemplateGetvaluation = null;
        public EmailTemplateGetvaluation emailTemplateGetvaluation
        {
            get
            {
                if (_emailTemplateGetvaluation == null)
                {
                    _emailTemplateGetvaluation = new EmailTemplateGetvaluation();
                }
                return _emailTemplateGetvaluation;
            }
            set
            {
                _emailTemplateGetvaluation = value;
            }
        }

        /// <summary>
        /// 获取中文通用值
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public EmailTemplateItemData Get(string itemKey, object[] values)
        {
            return emailTemplateGetvaluation.Get("Common", string.Empty, itemKey, values);
        }
        /// <summary>
        /// 获取英文通用值
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public EmailTemplateItemData GetEn(string itemKey, object[] values)
        {
            return emailTemplateGetvaluation.Get("en-US", "Common", itemKey, values);
        }
        /// <summary>
        /// 获取中文指定段值
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public EmailTemplateItemData Get(string sectionName, string itemKey, object[] values)
        {
            return emailTemplateGetvaluation.Get("zh-CN", sectionName, itemKey, values);
        }
        /// <summary>
        /// 获取英文指定段的值
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public EmailTemplateItemData GetEn(string sectionName, string itemKey, object[] values)
        {
            return emailTemplateGetvaluation.Get("en-US", sectionName, itemKey, values);
        }

        public EmailTemplateItemData Get(string languageName, string sectionName, string itemKey, object[] values)
        {
            return emailTemplateGetvaluation.Get(languageName, sectionName, itemKey, values);
        }

        /// <summary>
        /// 返回Message对象
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public ICP.Message.ServiceInterface.Message ReturnMessage(ICP.Message.ServiceInterface.Message mail, bool isEnglish, string itemKey, object[] values)
        {
            return emailTemplateGetvaluation.ReturnMessage(mail, isEnglish, itemKey, values);
        }

        //public string AppendHtmlBody(ICP.Message.ServiceInterface.Message messageInfo, string content, bool isEnglish)
        //{
        //    return emailTemplateGetvaluation.GetHtmlBody(messageInfo, content, isEnglish);
        //}

       
    }
}
