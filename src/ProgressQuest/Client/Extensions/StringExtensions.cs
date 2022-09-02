using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Extensions
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 转换成罗马数字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FindResourceDictionary(this string input)
        {
            try
            {
                return System.Windows.Application.Current.FindResource(input).ToString();
            }
            catch (Exception ex)
            {
                return input;
            }
        }
    }
}
