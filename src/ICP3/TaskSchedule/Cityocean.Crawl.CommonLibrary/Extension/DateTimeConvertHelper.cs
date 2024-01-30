#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/9/7 16:46:07
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Text;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 时间类型转换帮助类
    /// </summary>
    public static class DateTimeConvertHelper
    {
        /// <summary>
        /// 把时间数组转换为字符串
        /// </summary>
        /// <param name="values">Guid数组</param>
        /// <returns>返回转换后的字符串</returns>
        public static string Join(this DateTime[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (DateTime value in values)
            {
                result.Append(CommonConstants.DIVIDED_SYMBOL);

                if (value == DateTime.MinValue)
                {
                    result.Append("");
                }
                else
                {
                    result.Append(value.ToString(CommonConstants.DATETIME_FORMAT) + "." + value.Millisecond.ToString("000"));
                }
            }

            result = result.Remove(0, CommonConstants.DIVIDED_SYMBOL.Length);

            return result.ToString();
        }
        /// <summary>
        /// 把时间数组转换为字符串
        /// </summary>
        /// <param name="values">Guid数组</param>
        /// <returns>返回转换后的字符串</returns>
        public static string Join(this DateTime?[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (DateTime? value in values)
            {
                result.Append(CommonConstants.DIVIDED_SYMBOL);

                if (value.HasValue == false || value == DateTime.MinValue)
                {
                    result.Append("");
                }
                else
                {
                    result.Append(value.Value.ToString(CommonConstants.DATETIME_FORMAT) + "." + value.Value.Millisecond.ToString("000"));
                }
            }

            result = result.Remove(0, CommonConstants.DIVIDED_SYMBOL.Length);

            return result.ToString();
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNull(this DateTime? value)
        {
            return value == null;
        }
        /// <summary>
        /// 是否为最小值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsMinValue(this DateTime value)
        {
            return value == DateTime.MinValue;
        }
        /// <summary>
        /// 是否为空或最小值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrMinValue(this DateTime? value)
        {
            return value.IsNull() || value.Value.IsMinValue();
        }
        /// <summary>
        /// 时间间隔
        /// </summary>
        /// <param name="dateTime1"></param>
        /// <param name="dateTime2"></param>
        /// <returns></returns>
        public static TimeSpan Interval(this DateTime dateTime1, DateTime dateTime2)
        {
            return dateTime2 - dateTime1;
        }
        /// <summary>
        /// 时间间隔(Day)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="compareValue"></param>
        /// <returns></returns>
        public static int DayInterval(this DateTime value, DateTime compareValue)
        {
            TimeSpan tsResult = compareValue - value;
            return tsResult.Days;
        }
        /// <summary>
        /// 时间间隔(Day)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="compareValue"></param>
        /// <returns></returns>
        public static int SecondsInterval(this DateTime value, DateTime compareValue)
        {
            TimeSpan tsResult = compareValue - value;
            return tsResult.Seconds;
        } 
    }
}
