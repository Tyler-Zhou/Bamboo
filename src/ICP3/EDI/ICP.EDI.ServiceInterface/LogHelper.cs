using System;
using System.Text;
using System.IO;

namespace ICP.EDI.ServiceInterface
{
    public class LogHelper
    {
        public static void SaveLog(string message)
        {
            string str = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ":" + message + Environment.NewLine;
            string path = @"D:\log\log.txt";//文件路径

            StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);
            sw.Write(str);
            sw.Close();


        }
    }
}
