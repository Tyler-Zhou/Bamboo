#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/11/1 10:05:50
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 时间间隔转换帮助类
    /// </summary>
    public static class TimeSpanConvertHelper
    {
        /// <summary>
        /// 时间间隔
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStringSeconds(this TimeSpan value)
        {
            return string.Format("{0}s", value.TotalSeconds.ToString("0.00"));
        }

        /// <summary>
        /// 时间间隔
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStringMilliseconds(this TimeSpan value)
        {
            return string.Format("{0}s", value.TotalMilliseconds.ToString("0.00"));
        }
    }
}
