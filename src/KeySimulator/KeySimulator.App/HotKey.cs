using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeySimulator.App
{
    internal delegate void HotKeyCallBackHanlder();

    /// <summary>
    /// 
    /// </summary>
    internal class HotKey
    {
        /// <summary>
        /// 区分不同的快捷键
        /// </summary>
        int keyid = 100;
        /// <summary>
        /// 每一个key对于一个处理函数
        /// </summary>
        Dictionary<int, HotKeyCallBackHanlder> keymap = new Dictionary<int, HotKeyCallBackHanlder>();

        //如果函数执行成功，返回值不为0。
        //如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool RegisterHotKey(IntPtr hWnd,                //要定义热键的窗口的句柄
           int id,                     //定义热键ID（不能与其它ID重复）
            int modifiers,   //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效
            Keys vk                     //定义热键的内容
            );
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool UnregisterHotKey(
            IntPtr hWnd,                //要取消热键的窗口的句柄
            int id                      //要取消热键的ID
            );


        /// <summary>
        /// 注册快捷键
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="modifiers"></param>
        /// <param name="vk"></param>
        /// <param name="callBack"></param>
        /// <exception cref="Exception"></exception>
        internal void Regist(IntPtr hWnd, int modifiers, Keys vk, HotKeyCallBackHanlder callBack)
        {
            int id = keyid++;
            if (!RegisterHotKey(hWnd, id, modifiers, vk))
                throw new Exception("注册失败！");
            keymap[id] = callBack;
        }

        /// <summary>
        /// 注销快捷键
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="callBack"></param>
        internal void UnRegist(IntPtr hWnd, HotKeyCallBackHanlder callBack)
        {
            foreach (KeyValuePair<int, HotKeyCallBackHanlder> var in keymap)
            {
                if (var.Value == callBack)
                {
                    UnregisterHotKey(hWnd, var.Key);
                    return;
                }
            }
        }

        /// <summary>
        /// 快捷键消息处理
        /// </summary>
        /// <param name="m"></param>
        internal void ProcessHotKey(Message m)
        {
            if (m.Msg == 0x312)
            {
                int id = m.WParam.ToInt32();
                HotKeyCallBackHanlder callback;
                if (keymap.TryGetValue(id, out callback))
                    callback();
            }
        }

    }
}
