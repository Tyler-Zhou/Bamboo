namespace CRM.Client.Interfaces
{
    /// <summary>
    /// 配置服务
    /// </summary>
    public interface IConfigureService
    {
        /// <summary>
        /// 配置内容
        /// </summary>
        void ConfigureContent();

        /// <summary>
        /// 选择语言
        /// </summary>
        /// <param name="culture">文化</param>
        void ConfigLanguage(string culture);
    }
}
