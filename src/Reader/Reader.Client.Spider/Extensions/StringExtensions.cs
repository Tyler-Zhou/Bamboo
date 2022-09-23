using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Reader.Client.Spider.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 根据正则表达式匹配结果
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="regexExpression">正则表达式</param>
        /// <param name="isDebug">是否调试</param>
        /// <returns></returns>
        public static string RegexText(this string input,string regexExpression, bool isDebug = true)
        {
            StringBuilder regexText =new StringBuilder();
            try
            {
                if (!string.IsNullOrEmpty(input))
                {
                    MatchCollection collection = Regex.Matches(input, regexExpression);
                    if (collection.Count>0)
                    {
                        List<string> listResult = new List<string>();
                        int count = collection.Count;
                        for (int i = 0; i < count; i++)
                        {
                            string item = collection[i].Value;
                            if (!listResult.Contains(item))
                            {
                                regexText.Append(item);
                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                if (isDebug)
                    regexText.Append($"{input}:{regexExpression}:{ex.Message}");
            }
            return regexText.ToString();
        }
    }
}
