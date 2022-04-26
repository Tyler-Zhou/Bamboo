namespace Bamboo.Client.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfigureService
    {
        /// <summary>
        /// 
        /// </summary>
        void ConfigureContent();
        /// <summary>
        /// 配置主题
        /// </summary>
        void ConfigureTheme();
        /// <summary>
        /// 配置画板颜色
        /// </summary>
        void ConfigureHueColor();
    }
}
