namespace Bamboo.Client.Core.Common
{
    /// <summary>
    /// 程序上下文
    /// </summary>
    public static class ApplicationContext
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public static int UserId { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public static string Account { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public static string UserName { get; set; }
        /// <summary>
        /// 是否深色模式
        /// </summary>
        public static bool IsDarkTheme { get; set; }
        /// <summary>
        /// 画板颜色
        /// </summary>
        public static string HueColor { get; set; }
        /// <summary>
        /// 默认每页记录大小
        /// </summary>
        public static int DefaultPageSize { get; set; }
        /// <summary>
        /// 基本服务链接
        /// </summary>
        public static string BaseServiceUrl { get; set; }

    }
}
