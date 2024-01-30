#region Comment

/*
 * 
 * FileName:    ExceptionUtility.cs
 * CreatedOn:   2014/5/15 星期四 15:30:56
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->日志记录公用操作库
 *      ->1.WriteLog:写入普通操作日志
 *      ->2.WriteErrorLog:写入异常日志
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.IO;
using System.Text;

namespace ICP.Document
{
    /// <summary>
    /// Log Utility
    /// </summary>
    public class LogUtility
    {
        #region 写日志
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="paramLogName"></param>
        /// <param name="paramLogContent"></param>
        public static void WriteLog(string paramLogName,string paramLogContent)
        {
            try
            {
                string logPath = System.Windows.Forms.Application.StartupPath + "\\LogFiles\\";
                string FileName = String.Format("ICP.Document{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                string FileFullPath = logPath + FileName;
                string WriteText = string.Empty;
                using (System.IO.TextWriter write = System.IO.File.AppendText(FileFullPath))
                {
                    write.WriteLine("----------------------" + paramLogName + "--" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "----------------------");
                    write.WriteLine("************************************************************************************");
                    write.WriteLine(paramLogContent);
                    write.WriteLine("************************************************************************************");
                    write.Close();
                }
            }
            catch
            {
            }
        }
        #endregion  
    }
}
