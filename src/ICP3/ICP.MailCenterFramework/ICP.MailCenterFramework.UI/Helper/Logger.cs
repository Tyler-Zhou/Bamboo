using ICP.Framework.CommonLibrary.Client;
using System;
using System.Windows.Forms;

namespace ICP.MailCenterFramework.UI
{
   public class Logger
    {
        static log4net.ILog _log;

        /// <summary>
        /// 一个已经配置好了的日志记录器
        /// 日志输出路径是应用程序的当前运行目录下的子目录LogFiles
        /// </summary>
        static public log4net.ILog Log
        {
            get
            {
                return _log;
            }
        }

        static Logger()
        {
            log4net.Appender.RollingFileAppender rfa = new log4net.Appender.RollingFileAppender();
            rfa.Name = "HighAppender";

            if (Application.ProductName == "Internet Information Services")
            {
                Random rd = new Random();
                rfa.File = string.Format("C:\\ICP\\Logs\\{0}\\{1}\\{2}\\", Application.ProductName, Application.ProductVersion, rd.Next(0, 999));
            }
            else
            {
                rfa.File = string.Format("{0}\\LogFiles\\", string.IsNullOrEmpty(LocalData.MainPath) ? Application.StartupPath : LocalData.MainPath);
            }

            rfa.AppendToFile = true;
            rfa.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Date;
            rfa.DatePattern = "yyyy-MM-dd.'txt'";
            rfa.StaticLogFileName = false;

            log4net.Layout.PatternLayout pl = new log4net.Layout.PatternLayout();
            pl.ConversionPattern = "%d [%t] %-5p %c [%x] %X{auth} - Line:%L %m%n";
            pl.Header = string.Format("----------------------{0}--{1}--{2}----------------------\r\n",
                Application.ProductName,
                Application.ProductVersion,
                DateTime.Now);
            pl.Footer = string.Format("----------------------{0}--{1}--{2}----------------------\r\n",
                Application.ProductName,
                Application.ProductVersion,
                DateTime.Now);

            rfa.Layout = pl;

            log4net.Config.BasicConfigurator.Configure(rfa);
            rfa.ImmediateFlush = true;

            rfa.ActivateOptions();

            _log = log4net.LogManager.GetLogger("ICP");
        }
    }
}
