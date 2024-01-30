namespace ICP.ReportCenter.UI.Common.Helper
{
    using ICP.Framework.CommonLibrary.Common;
    using System;
    using System.Text;
    /// <summary>
    /// 类型转换帮助类
    /// </summary>
    public static class ReportTypeConvertHelper
    {
        /// <summary>
        /// 把时间数组转换为字符串
        /// </summary>
        /// <param name="values">Guid数组</param>
        /// <returns>返回转换后的字符串</returns>
        public static string Join4Report(this DateTime[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (DateTime value in values)
            {
                result.Append(ReportCommonConstants.DividedSymbol);

                if (value == DateTime.MinValue)
                {
                    result.Append("");
                }
                else
                {
                    result.Append(value.ToString(GlobalConstants.DATETIMEFORMAT) + "." + value.Millisecond.ToString("000"));
                }
            }

            result = result.Remove(0, ReportCommonConstants.DividedSymbol.Length);

            return result.ToString();
        }


        /// <summary>
        /// 把时间数组转换为字符串
        /// </summary>
        /// <param name="values">Guid数组</param>
        /// <returns>返回转换后的字符串</returns>
        public static string Join4Report(this DateTime?[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (DateTime? value in values)
            {
                result.Append(ReportCommonConstants.DividedSymbol);

                if (value.HasValue == false || value == DateTime.MinValue)
                {
                    result.Append("");
                }
                else
                {
                    result.Append(value.Value.ToString(GlobalConstants.DATETIMEFORMAT) + "." + value.Value.Millisecond.ToString("000"));
                }
            }

            result = result.Remove(0, ReportCommonConstants.DividedSymbol.Length);

            return result.ToString();
        }


        /// <summary>
        /// 把decimal数组转换为字符串
        /// </summary>
        /// <param name="values">Int数组</param>
        /// <param name="preserDigits">保留位数</param>
        /// <returns>转换后的字符串</returns>
        public static string Join4Report(
            this decimal[] values,
            int preserDigits)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            string formart = string.Format("F{0}", preserDigits);

            StringBuilder result = new StringBuilder();

            foreach (decimal value in values)
            {
                result.Append(ReportCommonConstants.DividedSymbol);

                result.Append(value.ToString(formart));
            }

            result = result.Remove(0, ReportCommonConstants.DividedSymbol.Length);

            return result.ToString();
        }


        /// <summary>
        /// 把Guid数组转换为字符串
        /// </summary>
        /// <param name="values">Guid数组</param>
        /// <returns>返回转换后的字符串</returns>
        public static string Join4Report(this Guid[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (Guid value in values)
            {
                result.Append(ReportCommonConstants.DividedSymbol);

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
                result = result.Remove(0, ReportCommonConstants.DividedSymbol.Length);
            }

            return result.ToString();
        }

        /// <summary>
        /// 把Guid数组转换为字符串
        /// </summary>
        /// <param name="values">Guid数组</param>
        /// <returns>返回转换后的字符串</returns>
        public static string Join4Report(this Guid?[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (Guid? value in values)
            {
                result.Append(ReportCommonConstants.DividedSymbol);

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
                result = result.Remove(0, ReportCommonConstants.DividedSymbol.Length);
            }

            return result.ToString();
        }


        /// <summary>
        /// 把Int数组转换为字符串
        /// </summary>
        /// <param name="values">Int数组</param>
        /// <returns>转换后的字符串</returns>
        public static string Join4Report(this int[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (int value in values)
            {
                result.Append(ReportCommonConstants.DividedSymbol);

                result.Append(value.ToString());
            }

            if (result.Length > 0)
            {
                result = result.Remove(0, ReportCommonConstants.DividedSymbol.Length);
            }

            return result.ToString();
        }


        /// <summary>
        /// 把Short数组转换为字符串
        /// </summary>
        /// <param name="values">Int数组</param>
        /// <returns>转换后的字符串</returns>
        public static string Join4Report(this short[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (short value in values)
            {
                result.Append(ReportCommonConstants.DividedSymbol);

                result.Append(value.ToString());
            }

            if (result.Length > 0)
            {
                result = result.Remove(0, ReportCommonConstants.DividedSymbol.Length);
            }

            return result.ToString();
        }


        /// <summary>
        /// 把Int数组转换为字符串
        /// </summary>
        /// <param name="values">Int数组</param>
        /// <returns>转换后的字符串</returns>
        public static string Join4Report(this bool[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();

            foreach (bool value in values)
            {
                result.Append(ReportCommonConstants.DividedSymbol);

                if (value)
                {
                    result.Append("1");
                }
                else
                {
                    result.Append("0");
                }
            }

            if (result.Length > 0)
            {
                result = result.Remove(0, ReportCommonConstants.DividedSymbol.Length);
            }

            return result.ToString();
        }

        /// <summary>
        /// 把String数组转换为字符串
        /// </summary>
        /// <param name="values">String数组</param>
        /// <returns>返回转换后的字符串</returns>
        public static string Join4Report(this string[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (string value in values)
            {
                result.Append(ReportCommonConstants.DividedSymbol);

                result.Append(value);
            }

            if (result.Length > 0)
            {
                result = result.Remove(0, ReportCommonConstants.DividedSymbol.Length);
            }

            return result.ToString();
        }

        /// <summary>
        /// 把String数组转换为字符串
        /// </summary>
        /// <param name="values">String数组</param>
        /// <param name="divideSymbol">分隔符</param>
        /// <returns>返回转换后的字符串</returns>
        public static string Join4Report(this string[] values, string divideSymbol)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (string value in values)
            {
                result.Append(divideSymbol);

                result.Append(value);
            }

            if (result.Length > 0)
            {
                result = result.Remove(0, divideSymbol.Length);
            }

            return result.ToString();
        }


        /// <summary>
        /// 把Short数组转换为字符串
        /// </summary>
        /// <param name="values">Int数组</param>
        /// <returns>转换后的字符串</returns>
        public static string Join4Report<T>(this T[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (object value in values)
            {
                result.Append(ReportCommonConstants.DividedSymbol);

                if (value == null)
                {
                    continue;
                }

                if (value.GetType().IsEnum)
                {
                    result.Append(((int)value));
                }
                else
                {
                    result.Append(value.ToString());
                }
            }

            if (result.Length > 0)
            {
                result = result.Remove(0, ReportCommonConstants.DividedSymbol.Length);
            }

            return result.ToString();
        }

    }
}
