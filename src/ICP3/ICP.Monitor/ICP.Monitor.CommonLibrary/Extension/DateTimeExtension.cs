#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/23 16:43:30
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace ICP.Monitor.Common.Extension
{
    /// <summary>
    /// DateTime Extender
    /// </summary>
    public static class DateTimeExtension
    {
        #region DateTime Duration for Seconds
        /// <summary>
        /// 获取时间间隔(秒)
        /// </summary>
        /// <returns>间隔秒数</returns>
        public static short Duration4Seconds(this DateTime objDTime, DateTime endDateTime)
        {
            TimeSpan ts = endDateTime.Subtract(objDTime).Duration();
            return Convert.ToInt16(ts.TotalSeconds);
        }
        #endregion
    }
}
