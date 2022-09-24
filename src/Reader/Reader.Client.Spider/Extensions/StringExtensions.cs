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
        /// <param name="groupName">组名</param>
        /// <param name="isDebug">是否调试</param>
        /// <returns></returns>
        public static string RegexText(this string input,string regexExpression,string groupName="R", bool isDebug = false)
        {
            StringBuilder regexText =new StringBuilder();
            try
            {
                if (!string.IsNullOrEmpty(input))
                {
                    MatchCollection matches = Regex.Matches(input, regexExpression, RegexOptions.Compiled | RegexOptions.Singleline);
                    foreach (Match match in matches)
                    {
                        GroupCollection groups = match.Groups;
                        foreach (Group group in groups)
                        {
                            if (groupName.Equals(group.Name))
                                return group.Value;
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
