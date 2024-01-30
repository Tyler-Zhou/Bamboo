using System;

namespace KeySimulator.App
{
    /// <summary>
    /// 定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
    /// </summary>
    [Flags()]
    internal enum KeyModifiers
    {
        None = 0,
        Alt = 1,
        Ctrl = 2,
        Shift = 4,
        WindowsKey = 8
    }

    /// <summary>
    /// 命令类型
    /// </summary>
    internal enum CommandType
    {
        /// <summary>
        /// 输入
        /// </summary>
        INPUT = 1,

        /// <summary>
        /// 运行
        /// </summary>
        RUN = 2,

        /// <summary>
        /// 按键
        /// </summary>
        KEY = 3,

        /// <summary>
        /// 暂停
        /// </summary>
        SLEEP = 4,

        /// <summary>
        /// 鼠标移动
        /// </summary>
        MOUSE_MOVE = 5,

        /// <summary>
        /// 鼠标单击
        /// </summary>
        MOUSE_CLICK = 6,

        /// <summary>
        /// 鼠标双击
        /// </summary>
        MOUSE_DBCLICK = 7,

        /// <summary>
        /// 窗口截屏
        /// </summary>
        SCREEN = 8,

        /// <summary>
        /// 全屏截屏
        /// </summary>
        ALL_SCREEN = 9,
    }

    [Flags]
    internal enum MouseEventFlag : uint
    {
        Move = 0x0001,
        LeftDown = 0x0002,
        LeftUp = 0x0004,
        RightDown = 0x0008,
        RightUp = 0x0010,
        MiddleDown = 0x0020,
        MiddleUp = 0x0040,
        XDown = 0x0080,
        XUp = 0x0100,
        Wheel = 0x0800,
        VirtualDesk = 0x4000,
        Absolute = 0x8000,
    }
}
