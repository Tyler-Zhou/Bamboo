#region Comment

/*
 * 
 * FileName:    Program.cs
 * CreatedOn:   2014/5/14 星期三 9:36:16
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->程序启动类
 *      ->1.判断ICPMain程序是否启动
 *      ->2.检测当前程序是否有相同实例在运行，有则跳转至运行程序
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ICP.Document
{
    /// <summary>
    /// 程序启动类
    /// </summary>
    static class Program
    {
        #region 主运行程序
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //处理未捕获的异常
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //处理非UI线程异常
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                //1.检测ICP是否正在运行
                Process icpMain = ClientUtility.GetProcessByName("ICPMain");
                if (icpMain != null)
                {
                    MessageBox.Show("ICP Offline could not launch because ICP now is running.", "System Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //2.查找当前运行进程
                Process instance = RunningInstance();
                if (instance == null)   //没有实例在运行
                {
                    //注册皮肤
                    DevExpress.UserSkins.OfficeSkins.Register();
                    DevExpress.UserSkins.BonusSkins.Register();
                    DevExpress.Skins.SkinManager.EnableFormSkins();
                    //启动程序
                    ProgramStart appStart = new ProgramStart();
                    Application.Run(appStart);
                }
                else
                {
                    //已经有一个实例在运行
                    HandleRunningInstance(instance);
                }
            }
            catch (Exception ex)
            {
                FormException.ShowException(ex);
            }
        } 
        #endregion

        #region  确保程序只运行一个实例
        private static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();

            Process runProcess = ClientUtility.GetProcessByName(current.ProcessName);
            if (current.Id != runProcess.Id)
            {
                //保证要打开的进程同已经存在的进程来自同一文件路径
                if (Assembly.GetExecutingAssembly().Location.Replace("/", "//") == current.MainModule.FileName)
                {
                    //返回已经存在的进程
                    return runProcess;
                }
            }
            return null;
        }

        private static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, 1);  //调用api函数，正常显示窗口
            SetForegroundWindow(instance.MainWindowHandle); //将窗口放置最前端
        }
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(System.IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(System.IntPtr hWnd);
        #endregion

        #region 全局异常监听
        /// <summary>
        /// 进程异常监听
        /// </summary>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception error = e.Exception as Exception;
            FormException.ShowException(ExceptionType.ThreadEx, error, e.ToString());
        }
        /// <summary>
        /// 未处理异常监听
        /// </summary>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception error = e.ExceptionObject as Exception;
            FormException.ShowException(ExceptionType.UnhandledEx, error, e.ToString());
        } 
        #endregion
    }
}
