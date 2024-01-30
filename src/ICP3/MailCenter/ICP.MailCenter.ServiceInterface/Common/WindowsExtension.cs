using System;
using System.Runtime.InteropServices;

namespace ICP.MailCenter.ServiceInterface
{
    /// <summary>
    /// windows扩展
    /// </summary>
    public static class WindowsExtension
    {
        public const int WM_SETTEXT = 0x000C;
        public const UInt32 SWP_HIDEWINDOW = 0x0080;
        public const UInt32 SWP_SHOWWINDOW = 0x0040;
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        public static readonly IntPtr HWND_TOP = new IntPtr(0);


        [DllImport("User32.dll")]
        public static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlasgs);

        /// <summary>
        /// 前置
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        // 隐藏与显示滚滚东条
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);
        /// <summary>
        /// 获取窗口句柄
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        //[DllImport("User32.dll", EntryPoint = "FindWindow")]
        //public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

        /// <summary>
        /// 设置控件主题
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="pszSubAppName"></param>
        /// <param name="pszSubIdList"></param>
        /// <returns></returns>
        [DllImport("uxtheme.dll")]
        public static extern int SetWindowTheme(
            [In] IntPtr hwnd,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszSubAppName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszSubIdList);
        /// <summary>
        /// 显示应用程序窗口
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        /// <summary>
        /// 异步显示程序窗口
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        /// <summary>
        /// 把窗口置顶
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        //消息发送API
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(
            IntPtr hWnd,        // 信息发往的窗口的句柄
            int Msg,            // 消息ID
            int wParam,         // 参数1
            int lParam            // 参数2
        );

        /// <summary>
        /// 文件是否正在被使用
        /// </summary>
        /// <param name="lpPathName"></param>
        /// <param name="iReadWrite"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);  
    }
    /// <summary>
    /// Window handles (HWND) used for hWndInsertAfter
    /// </summary>
    public partial class HWND
    {
        public static readonly IntPtr
             NoTopMost = new IntPtr(-2),
             TopMost = new IntPtr(-1),
             Top = new IntPtr(0),
             Bottom = new IntPtr(1);
    }

    /// <summary>
    /// SetWindowPos Flags
    /// </summary>
    public static class SWP
    {
        public static readonly uint
        NOSIZE = 0x0001,
        NOMOVE = 0x0002,
        NOZORDER = 0x0004,
        NOREDRAW = 0x0008,
        NOACTIVATE = 0x0010,
        DRAWFRAME = 0x0020,
        FRAMECHANGED = 0x0020,
        SHOWWINDOW = 0x0040,
        HIDEWINDOW = 0x0080,
        NOCOPYBITS = 0x0100,
        NOOWNERZORDER = 0x0200,
        NOREPOSITION = 0x0200,
        NOSENDCHANGING = 0x0400,
        DEFERERASE = 0x2000,
        ASYNCWINDOWPOS = 0x4000;


        public static int WM_CLOSE = 0x0010;
        public static int OF_READWRITE = 2;
        public static int OF_SHARE_DENY_NONE = 0x40;
    }
}
