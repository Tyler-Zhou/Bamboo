using Client.Models;
using System;

namespace Client.Common
{
    /// <summary>
    /// 应用程序上下文
    /// </summary>
    public static class ApplicationContext
    {
        /// <summary>
        /// 任务计时器时间间隔
        /// </summary>
        public static TimeSpan TaskTimeSpan { get; set; } = TimeSpan.FromSeconds(0.1);
        /// <summary>
        /// 自动保存时间间隔
        /// </summary>
        public static TimeSpan AutoSaveTimeSpan { get; set; } = new TimeSpan(0, 5, 0);
        /// <summary>
        /// 文化名称
        /// </summary>
        public static ApplicationSetting Setting { get; set; }
    }
}
