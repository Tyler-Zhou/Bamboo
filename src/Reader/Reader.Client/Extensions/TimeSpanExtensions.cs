using System;

namespace Reader.Client.Extensions
{
    /// <summary>
    /// 时间类型扩展
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// 查找资源字典资源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToLogString(this TimeSpan input)
        {
            try
            {
                return input.ToString(@"d\.mm\:ss\.fff");
            }
            catch
            {
                return "";
            }
        }
    }
}
