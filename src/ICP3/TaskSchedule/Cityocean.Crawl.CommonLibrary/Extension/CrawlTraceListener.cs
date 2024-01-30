#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/12/29 星期五 17:44:52
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 自定义监听日志
    /// </summary>
    public class CrawlTraceListener : TraceListener
    {
        /// <summary>
        /// 
        /// </summary>
        public string FilePath { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public CrawlTraceListener(string filePath)
        {
            FilePath = filePath;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public override void Write(string message)
        {
            WriteLine(message, "");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="category"></param>
        public override void Write(object o, string category)
        {
            WriteLine(o, category);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public override void WriteLine(string message)
        {
            WriteLine(message, "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="category"></param>
        public override void WriteLine(object o, string category)
        {
            string message = string.Empty;
            if (!string.IsNullOrEmpty(category))
            {
                message = category + ":";
            }
            var exception = o as Exception;
            if (exception != null)//如果参数对象o是与Exception类兼容,输出异常消息+堆栈,否则输出o.ToString()
            {
                var ex = exception;
                message += ex.Message + Environment.NewLine;
                message += ex.StackTrace;
            }
            else if (null != o)
            {
                message += o.ToString();
            }
            string LogFile = string.Empty;
            if (category.IsNullOrEmpty())
                LogFile = GlobalVariable.ProgramDirectory + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            else
                LogFile = GlobalVariable.ProgramDirectory + category + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            StreamWriter writer = new StreamWriter(new FileStream(LogFile, FileMode.Append, FileAccess.Write), Encoding.GetEncoding("GBK"));
            writer.WriteLine(message);
            writer.Close();
        }
    }
}
