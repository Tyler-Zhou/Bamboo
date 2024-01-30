#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/9/7 16:38:23
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
    /// Guid转换帮助类
    /// </summary>
    public static class GuidConvertHelper
    {
        /// <summary>
        /// Guid是否为空
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this Guid id)
        {
            return id == Guid.Empty;
        }

        /// <summary>
        /// Guid是否为空
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paramGuidString"></param>
        /// <returns></returns>
        public static bool EqualsString(this Guid id,string paramGuidString)
        {
            return !paramGuidString.IsNullOrEmpty() && paramGuidString.ToUpper().Equals(("" + id).ToUpper());
        }

        /// <summary>
        /// Guid是否为空
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string ToUpperString(this Guid id)
        {
            return ("" + id).ToUpper();
        }

        /// <summary>
        /// 把可为null的Guid转换为Guid
        /// </summary>
        /// <param name="value">可为空Guid</param>
        /// <returns>转换后的Guid对象</returns>
        public static Guid ToGuid(this Guid? value)
        {
            if (!value.HasValue)
            {
                return Guid.Empty;
            }
            else
            {
                return value.Value;
            }
        }

        /// <summary>
        /// Guid? 是否为空
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this Guid? id)
        {
            return id == null || id == Guid.Empty;
        }

        /// <summary>
        /// 把Guid数组转换为字符串
        /// </summary>
        /// <param name="values">Guid数组</param>
        /// <returns>返回转换后的字符串</returns>
        public static string Join(this Guid[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (Guid value in values)
            {
                result.Append(CommonConstants.DIVIDED_SYMBOL);

                if (value == Guid.Empty)
                {
                    result.Append("");
                }
                else
                {
                    result.Append(value.ToString());
                }
            }

            if (result.Length > 0)
            {
                result = result.Remove(0, CommonConstants.DIVIDED_SYMBOL.Length);
            }

            return result.ToString();
        }

        /// <summary>
        /// 把Guid数组转换为字符串
        /// </summary>
        /// <param name="values">Guid数组</param>
        /// <returns>返回转换后的字符串</returns>
        public static string Join(this Guid?[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (Guid? value in values)
            {
                result.Append(CommonConstants.DIVIDED_SYMBOL);

                if (value == null || value == Guid.Empty)
                {
                    result.Append("");
                }
                else
                {
                    result.Append(value.ToString());
                }
            }

            if (result.Length > 0)
            {
                result = result.Remove(0, CommonConstants.DIVIDED_SYMBOL.Length);
            }

            return result.ToString();
        } 
    }
}
