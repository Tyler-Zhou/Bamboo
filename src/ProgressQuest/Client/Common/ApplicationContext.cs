using Client.Models;

namespace Client.Common
{
    /// <summary>
    /// 应用程序上下文
    /// </summary>
    public static class ApplicationContext
    {
        /// <summary>
        /// 文化名称
        /// </summary>
        public static ApplicationSetting Setting { get; set; }
    }
}
