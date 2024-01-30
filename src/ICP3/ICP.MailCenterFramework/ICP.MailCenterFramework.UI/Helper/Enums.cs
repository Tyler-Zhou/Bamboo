using System;

namespace ICP.MailCenterFramework.UI
{
    /// <summary>
    /// 主热键：None、Ctrl、Alt、Shift
    /// </summary>
    [Flags()]
    public enum KeyModifiers
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Alt
        /// </summary>
        Alt = 1,
        /// <summary>
        /// Ctrl
        /// </summary>
        Control = 2,
        /// <summary>
        /// Shift
        /// </summary>
        Shift = 4,
        /// <summary>
        /// Windows
        /// </summary>
        Windows = 8
    }
}
