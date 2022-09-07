using Client.Common;
using System.Text.RegularExpressions;

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
                if (string.IsNullOrWhiteSpace(input))
                    return "";
                return System.Windows.Application.Current.FindResource(input).ToString();
            }
            catch
            {
                return input;
            }
        }

        /// <summary>
        /// 附加不定冠词
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="quality">数量</param>
        /// <returns></returns>
        public static string AdditionalIndefiniteArticle(this string input, int quality)
        {
            try
            {
                if (quality == 1)
                {
                    if ("en-US".Equals(ApplicationContext.Setting.CultureName))
                    {
                        //英文判断元音
                        if ("AEIOUaeiou".IndexOf(input.Substring(0, 1)) > 0)
                        {
                            input = "DataGridArticleAn".FindResourceDictionary() + " " + input;
                        }
                        else
                        {
                            input = "DataGridArticleA".FindResourceDictionary() + " " + input;
                        }
                        return input;
                    }
                    else
                    {
                        return "DataGridArticleAn".FindResourceDictionary() + input;
                    }
                }
                return input;
            }
            catch
            {
                return input;
            }
        }

        /// <summary>
        /// 附加定冠词
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="quality">数量</param>
        /// <returns></returns>
        public static string AdditionalDefiniteArticle(this string input, int quality)
        {
            try
            {
                if (quality == 1)
                {
                    if ("en-US".Equals(ApplicationContext.Setting.CultureName))
                    {
                        input = "DataGridArticleThe".FindResourceDictionary() + " " + input;
                        return input;
                    }
                    else
                    {
                        input = "DataGridArticleThe".FindResourceDictionary() + input;
                    }
                }
                return input;
            }
            catch
            {
                return input;
            }
        }

        /// <summary>
        /// 单词变成复数形式
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static string ToPlural(this string input)
        {
            if ("en-US".Equals(ApplicationContext.Setting.CultureName))
            {
                Regex plural1 = new Regex("(?<keep>[^aeiou])y$");
                Regex plural2 = new Regex("(?<keep>[aeiou]y)$");
                Regex plural3 = new Regex("(?<keep>[sxzh])$");
                Regex plural4 = new Regex("(?<keep>[^sxzhy])$");

                if (plural1.IsMatch(input))
                    return plural1.Replace(input, "${keep}ies");
                else if (plural2.IsMatch(input))
                    return plural2.Replace(input, "${keep}s");
                else if (plural3.IsMatch(input))
                    return plural3.Replace(input, "${keep}es");
                else if (plural4.IsMatch(input))
                    return plural4.Replace(input, "${keep}s");
            }
            else
            {
                input = input + "DataGridPlural".FindResourceDictionary();
            }
            return input;
        }
    }
}
