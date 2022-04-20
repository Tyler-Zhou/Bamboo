using System;
using System.Text;

namespace Bamboo.Server.Models
{
    /// <summary>
    /// 日志消息
    /// </summary>
    public class LogMessage
    {
        /// <summary>
        /// IP
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 日志信息
        /// </summary>
        public string LogInfo { get; set; }

        /// <summary>
        /// 跟踪信息
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder strInfo = new StringBuilder();
            strInfo.Append("1. 操作时间: " + OperationTime + " \r\n");
            strInfo.Append("2. 操作人: " + OperationName + " \r\n");
            strInfo.Append("3. Ip  : " + IpAddress + "\r\n");
            strInfo.Append("4. 错误内容: " + LogInfo + "\r\n");
            strInfo.Append("5. 跟踪: " + StackTrace + "\r\n");
            strInfo.Append("-----------------------------------------------------------------------------------------------------------------------------\r\n");
            return strInfo.ToString();
        }
    }
}
