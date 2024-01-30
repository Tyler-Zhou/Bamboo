
namespace ICP.MailCenter.ServiceInterface
{
    /// <summary>
    /// 邮件模版客户端接口
    /// </summary>
    public interface IEmailTemplateGetter
    {
        /// <summary>
        /// 获取中文通用值
        /// </summary>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        EmailTemplateItemData Get(string itemKey, object[] values);

        /// <summary>
        /// 获取英文通用值
        /// </summary>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        EmailTemplateItemData GetEn(string itemKey, object[] values);

        /// <summary>
        /// 获取中文指定段值
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        EmailTemplateItemData Get(string sectionName, string itemKey, object[] values);

        /// <summary>
        /// 获取英文指定段的值
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        EmailTemplateItemData GetEn(string sectionName, string itemKey, object[] values);

        EmailTemplateItemData Get(string languageName, string sectionName, string itemKey, object[] values);
        /// <summary>
        /// 返回Message对象
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        ICP.Message.ServiceInterface.Message ReturnMessage(ICP.Message.ServiceInterface.Message mail, bool isEnglish, string itemKey, object[] values);

       
    }
}
