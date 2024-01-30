using ICP.FileSystem.ServiceComponent;
using System;
using System.ServiceProcess;

namespace ICP.FileSystem.Service
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new ICPFileSystemService() 
            };
            ServiceBase.Run(ServicesToRun);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject != null)
            {
                Exception ex = e.ExceptionObject as Exception;
                if (ex != null)
                {
                    //TODO:记录日志
                    new FileSystemService().WriteLog("UnhandledExceptionEventArgs\r\n" + ex.Message + "\r\n" + ex.StackTrace.ToString());
                }
            }
        }

    }
}
