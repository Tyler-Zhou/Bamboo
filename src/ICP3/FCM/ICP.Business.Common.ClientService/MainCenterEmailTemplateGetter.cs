/*****
 *类说明:邮件模版实现类
 *创建者:王乐俊
 *创建时间: 2013-10-21
******/
using ICP.Business.Common.ServiceInterface;
using ICP.MailCenter.ServiceInterface;

namespace ICP.Business.Common.ClientService
{
    /// <summary>
    /// 邮件模版实现接口
    /// </summary>
    public class MainCenterEmailTemplateGetter : IMainCenterEmailTemplateGetter
    {
        private EmailTemplateGetvaluation _emailTemplateGetvaluation = null;
        public EmailTemplateGetvaluation EmailTemplateGetvaluation
        {
            get
            {
                if (_emailTemplateGetvaluation == null)
                {
                    _emailTemplateGetvaluation=new EmailTemplateGetvaluation();
                }
                return _emailTemplateGetvaluation;
            }
            set
            {
                _emailTemplateGetvaluation = value;
            }
        }

        #region IEmailTemplateGetter 成员
        /// <summary>
        /// 获取中文通用值
        /// </summary>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public EmailTemplateItemData Get(string itemKey, object[] values)
        {
            return EmailTemplateGetvaluation.Get("Common", string.Empty, itemKey, values);
        }
        /// <summary>
        /// 获取英文通用值
        /// </summary>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public EmailTemplateItemData GetEn(string itemKey, object[] values)
        {
            return EmailTemplateGetvaluation.Get("en-US", "Common", itemKey, values);
        }
        /// <summary>
        /// 获取中文指定段值
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public EmailTemplateItemData Get(string sectionName, string itemKey, object[] values)
        {
            return EmailTemplateGetvaluation.Get("zh-CN", sectionName, itemKey, values);
        }
        /// <summary>
        /// 获取英文指定段的值
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public EmailTemplateItemData GetEn(string sectionName, string itemKey, object[] values)
        {
            return EmailTemplateGetvaluation.Get("en-US", sectionName, itemKey, values);
        }

        public EmailTemplateItemData Get(string languageName, string sectionName, string itemKey, object[] values)
        {
            return EmailTemplateGetvaluation.Get(languageName, sectionName, itemKey, values);
        }

        /// <summary>
        /// 返回Message对象
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="isEnglish"></param>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public ICP.Message.ServiceInterface.Message ReturnMessage(ICP.Message.ServiceInterface.Message mail, bool isEnglish, string itemKey, object[] values)
        {
            return EmailTemplateGetvaluation.ReturnMessage(mail, isEnglish, itemKey, values);
        }

        #endregion

      
    }
}
