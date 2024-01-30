#region Comment

/*
 * 
 * FileName:    FormLoading.cs
 * CreatedOn:   2014/5/21 星期三 11:42:18
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->LoadingUtility：
 *          加载窗体显示帮助类，默认在父窗体中间显示
 *      ->ThreadMethodHelper:
 *          进程辅助类，用于显示关闭加载窗体
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace ICP.Document
{
    /// <summary>
    /// 加载窗体显示帮助类
    /// </summary>
    public static class LoadingUtility
    {
        #region 加载窗体
        private static Dictionary<int, FormLoading> dicForms = new Dictionary<int, FormLoading>();
        #endregion

        #region ShowForm
        /// <summary>
        /// ShowFormLoading
        /// </summary>
        public static int ShowFormLoading()
        {
            return Show(CreateThreadMethodHelperInfo("Loading...", false, null));
        }
        /// <summary>
        /// ShowFormLoading
        /// </summary>
        /// <param name="message">message</param>
        public static int ShowFormLoading(string message)
        {
            return Show(CreateThreadMethodHelperInfo(message, false, null));
        }
        /// <summary>
        /// 设置在父窗口中间显示
        /// </summary>
        /// <param name="point"></param>
        /// <param name="message"></param>
        public static int ShowFormLoading(Control parentControl, string message)
        {
            return Show(CreateThreadMethodHelperInfo(message, true, new Rectangle(parentControl.PointToScreen(parentControl.Location), parentControl.Size)));
        }
        /// <summary>
        /// 启动进程显示窗体
        /// </summary>
        /// <param name="message">窗体显示消息</param>
        /// <param name="isSetStartPosition">显示位置</param>
        /// <param name="site">大小</param>
        /// <returns></returns>
        private static ThreadMethodHelper CreateThreadMethodHelperInfo(string message, bool isSetStartPosition, Rectangle? site)
        {
            return new ThreadMethodHelper() { Message = message, SetStartPosition = isSetStartPosition, Site = site };
        }
        /// <summary>
        /// ShowFormLoading
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="waitTime">waitTime</param>
        private static int Show(ThreadMethodHelper helper)
        {
            System.Threading.Thread _Thread = new System.Threading.Thread(new ParameterizedThreadStart(ShowFormLoadingByMessage));
            int managedThreadID = _Thread.ManagedThreadId;
            if (dicForms.Count > 0)
            {
                if (dicForms.ContainsKey(managedThreadID))
                {
                    FormLoading FormLoading = dicForms[managedThreadID];
                    InnerClose(FormLoading);
                    dicForms.Remove(managedThreadID);
                }
                dicForms.Clear();
            }
            dicForms.Add(managedThreadID, null);
            helper.ManagedThreadID = managedThreadID;

            _Thread.Start(helper);
            return managedThreadID;

        }
        /// <summary>
        /// CloseFormLoading
        /// </summary>
        public static void CloseFormLoading(int managedThreadID)
        {
            lock (dicForms)
            {
                if (dicForms.ContainsKey(managedThreadID))
                {
                    FormLoading FormLoading = dicForms[managedThreadID];
                    InnerClose(FormLoading);
                    dicForms.Remove(managedThreadID);
                }
            }

        }
        /// <summary>
        /// 关闭加载窗体
        /// </summary>
        /// <param name="FormLoading">加载窗体对象</param>
        private static void InnerClose(FormLoading FormLoading)
        {
            if (FormLoading != null)
            {
                if (FormLoading.InvokeRequired)
                {
                    Action a = new Action(() => FormLoading.Close());
                    FormLoading.Invoke(a);
                }
                else
                {
                    FormLoading.Close();
                }
            }
        }
        #endregion

        /// <summary>
        /// 通过传入消息内容显示加载窗体
        /// </summary>
        /// <param name="o"></param>
        static void ShowFormLoadingByMessage(object o)
        {
            ThreadMethodHelper _helper = o as ThreadMethodHelper;
            FormLoading FormLoading = new FormLoading();

            IntPtr handle = FormLoading.Handle;
            lock (dicForms)
            {
                if (!dicForms.ContainsKey(_helper.ManagedThreadID))
                {
                    return;
                }
                dicForms[_helper.ManagedThreadID] = FormLoading;
            }
            InnerShow(_helper, FormLoading);
        }
        /// <summary>
        /// 初始化加载窗体显示
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="FormLoading"></param>
        private static void InnerShow(ThreadMethodHelper helper, FormLoading FormLoading)
        {
            string message = (string.IsNullOrEmpty(helper.Message) ? "Loading..." : helper.Message);

            if (helper.SetStartPosition)
            {
                FormLoading.StartPosition = FormStartPosition.Manual;

                Rectangle site = helper.Site.Value;
                Point siteLocation = site.Location;
                FormLoading.Location = new System.Drawing.Point(siteLocation.X + site.Width / 2 - FormLoading.Width / 2, siteLocation.Y + site.Height / 2 - FormLoading.Height / 2);
            }

            FormLoading.ShowDialog(message);


        }
    }

    /// <summary>
    /// 启动线程参数铺助类
    /// </summary>
    public class ThreadMethodHelper
    {
        /// <summary>
        /// 显示消息值
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 显示的位置信息
        /// </summary>
        public Rectangle? Site { get; set; }
        /// <summary>
        /// 是否设置起始位置
        /// </summary>
        public bool SetStartPosition { get; set; }
        /// <summary>
        /// 窗体创建时所在的托管线程ID
        /// </summary>
        public int ManagedThreadID { get; set; }

    }
}
