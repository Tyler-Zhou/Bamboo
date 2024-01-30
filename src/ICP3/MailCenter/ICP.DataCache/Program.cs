#region Comment

/*
 * 
 * FileName:    Program.cs
 * CreatedOn:   2015/5/18 16:43:01
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.DataCache
{
    static class Program
    {
        private static System.Windows.Forms.ApplicationContext context;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool flag;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out flag);
            if (flag)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Application.ThreadException += Application_ThreadException;
                //Application.Run(new FormDataChache());

                FormDataChache frmDataCache = new FormDataChache();
                frmDataCache.Size = new System.Drawing.Size(0, 0);

                frmDataCache.Show();
                frmDataCache.Visible = false;

                context = new System.Windows.Forms.ApplicationContext();
                Application.Run(context);

                // 释放 System.Threading.Mutex 一次
                mutex.ReleaseMutex();
            }
            else
            {
                Application.Exit();
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogHelper.SaveLog("CurrentDomain_UnhandledException");
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogHelper.SaveLog("CurrentDomain_UnhandledException");
            LogHelper.SaveLog(CommonHelper.BuildExceptionString(e.Exception));
        }
    }
}
