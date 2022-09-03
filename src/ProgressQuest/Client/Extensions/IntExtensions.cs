using System.Collections.Generic;

namespace Client.Extensions
{
    /// <summary>
    /// 整型扩展方法
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// 转换成罗马数字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToRomanNumber(this int input)
        {
            string res = string.Empty;
            List<int> val = new List<int> { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            List<string> str = new List<string> { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            for (int i = 0; i < val.Count; ++i)
            {
                while (input >= val[i])
                {
                    input -= val[i];
                    res += str[i];
                }
            }
            return res;
        }
    }
}
