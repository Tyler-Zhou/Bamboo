using System;
using System.Diagnostics;

namespace Reader.Client.Extensions
{
    /// <summary>
    /// Stopwatch 扩展
    /// </summary>
    public static class StopwatchExtensions
    {
        /// <summary>
        /// 转换为日志字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ElapsedToLogString(this Stopwatch input)
        {
            try
            {
                if (input!=null)
                {
                    return input.Elapsed.ToLogString();
                }
            }
            catch(Exception ex)
            {
                return $"{nameof(input)}:{ex.Message}";
            }
            return "";
        }
    }
}
