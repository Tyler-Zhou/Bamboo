#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/9/7 16:32:30
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringConvertHelper
    {
        /// <summary>
        /// 尝试实例化Guid
        /// 异常时返回Guid.Empty
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static Guid NewGuid(this string input)
        {
            try
            {
                return new Guid(input);
            }
            catch
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// 简单判断是否JSON格式
        /// 判断规则:起始结束是否为"{}[]"
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static bool IsJson(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }

        /// <summary>
        /// 是否存在目录
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static bool IsExistsDirectory(this string input)
        {
            return Directory.Exists(input);
        }

        /// <summary>
        /// 是否为空字符串
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        /// <summary>
        /// 转换小写后比较字符串
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="input2">比较字符串</param>
        /// <returns></returns>
        public static bool LowerContains(this string input, string input2)
        {
            if (input.IsNullOrEmpty() || input2.IsNullOrEmpty())
                return false;
            return input.Trim().ToLower().Contains(input2.Trim().ToLower());
        }

        /// <summary>
        /// 格式化HTML
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static string FormatHTML(this string input)
        {
            if (input.IsNullOrEmpty())
                return "";
            input = input.Replace("<", "\r\n<");
            input = input.Replace(">", ">\r\n");
            return input;
        }

        /// <summary>
        /// 还原HTML
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static string ReductionHTML(this string input)
        {
            input = input.Replace("\r\n<", "<");
            input = input.Replace(">\r\n", ">");
            return input;
        }

        /// <summary>
        /// HTML To JSON
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns>返回过滤后的字符串</returns>
        public static string HTMLToJSON(this string input)
        {
            input = input.Replace("<", "&lt;");
            input = input.Replace(">", "&gt;");
            input = input.Replace(" ", "&nbsp;");
            input = input.Replace("\n", "<br>");
            input = input.Replace("&", "&amp;");
            return input;
        }

        /// <summary>
        /// HTML To JSON
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns>返回过滤后的字符串</returns>
        public static string JSONToHTML(this string input)
        {
            input = input.Replace("&lt;", "<");
            input = input.Replace("&gt;", ">");
            input = input.Replace("&nbsp;", " ");
            input = input.Replace("<br>", "\n");
            input = input.Replace("&amp;", "&");
            return input;
        }

        /// <summary>
        /// 过滤SQL语句,防止注入
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns>返回过滤后的字符串</returns>
        public static string FilterSql(this string input)
        {
            if (input.IsNullOrEmpty())
                return input;
            //危险字符
            input = input.Replace(";", "");
            input = input.Replace("&", "&amp;");
            input = input.Replace("<", "&lt;");
            input = input.Replace(">", "&gt;");
            input = input.Replace("'", "");
            input = input.Replace("--", "");
            input = input.Replace("/", "");
            input = input.Replace("%", "");
            //关键字
            input = input.ToUpper().Replace("'", "");
            input = input.ToUpper().Replace("TRUNCATE", "");
            input = input.ToUpper().Replace("CHAR", "");
            input = input.ToUpper().Replace("DECLARE", "");
            input = input.ToUpper().Replace("JOIN", "");
            input = input.ToUpper().Replace("AND", "");
            input = input.ToUpper().Replace("CHR", "");
            input = input.ToUpper().Replace("MID", "");
            input = input.ToUpper().Replace("MASTER", "");
            input = input.ToUpper().Replace("DELETE", "");
            input = input.ToUpper().Replace("DROP", "");
            input = input.ToUpper().Replace("SELECT", "");
            input = input.ToUpper().Replace("UPDATE", "");
            input = input.ToUpper().Replace("INSERT", "");
            input = input.ToUpper().Replace("--", "");
            return input;
        }

        /// <summary>
        /// 从路径中提取文件名
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static string ExtractFileName(this string input)
        {
            if (input.IsNullOrEmpty())
                return input;
            string file = input.Substring(input.LastIndexOf("\\", StringComparison.Ordinal) + 1);//去掉了路径
            string fileName = file.Substring(0, file.LastIndexOf(".", StringComparison.Ordinal));//去掉了后缀名
            return fileName;
        }

        #region Regex.IsMatch

        /// <summary>
        /// 检测是否匹配
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="paramExpression">表达式</param>
        /// <param name="isThrowException">是否抛出异常</param>
        /// <param name="timeoutSeconds">超时时间(s)</param>
        /// <returns></returns>
        public static bool RegexIsMatch(this string input, string paramExpression, bool isThrowException = false, int timeoutSeconds = 10)
        {
            if (input.IsNullOrEmpty() || paramExpression.IsNullOrEmpty())
                return false;
            try
            {
                return Regex.IsMatch(input, paramExpression, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(timeoutSeconds));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("RegexIsMatch:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 判断一个字符串是否为合法整数(不限制长度)
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static bool IsInteger(this string input)
        {
            const string pattern = @"^\d*$";
            return input.RegexIsMatch(pattern);
        }

        /// <summary>
        /// 检测是否空白页
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static bool RegexIsMatchBlankPage(this string input)
        {
            if (input.Trim().IsNullOrEmpty())
                return true;
            return input.RegexIsMatch(@"<body>[\n\r\s]*</body>");
        }
        #endregion

        #region Regex.Match
        /// <summary>
        /// 匹配单行
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="paramExpression">匹配表达式</param>
        /// <param name="isThrowException">是否抛出异常</param>
        /// <param name="timeoutSeconds">超时时间(s)</param>
        /// <returns></returns>
        public static Match RegexMatchSingleline(this string input, string paramExpression, bool isThrowException = true, int timeoutSeconds = 10)
        {
            if (input.IsNullOrEmpty())
                throw new Exception("RegexMatchSingleline:input is empty");
            if (paramExpression.IsNullOrEmpty())
                throw new Exception("RegexMatchSingleline:expression is empty");
            try
            {
                return Regex.Match(input, paramExpression, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(timeoutSeconds));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("RegexMatchSingleline:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 匹配字符串
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="paramExpression">匹配表达式</param>
        /// <param name="isThrowException">是否抛出异常</param>
        /// <param name="paramSecondsTimeOut">超时时间(s)</param>
        /// <returns></returns>
        public static string RegexMatchString(this string input, string paramExpression, bool isThrowException = true, int paramSecondsTimeOut = 10)
        {
            if (input.IsNullOrEmpty() || paramExpression.IsNullOrEmpty())
                return "";
            Match m = input.RegexMatchSingleline(paramExpression, isThrowException, paramSecondsTimeOut);
            if (!m.IsNull())
                return m.Groups.Count > 1 ? m.Groups[1].Value : "";
            return input;
        }

        /// <summary>
        /// 匹配字符串
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="paramExpression">匹配表达式</param>
        /// <param name="paramGroupName">组名</param>
        /// <returns></returns>
        public static string RegexMatchString(this string input, string paramExpression,string paramGroupName)
        {
            if (input.IsNullOrEmpty() || paramExpression.IsNullOrEmpty())
                return "";
            Match m = input.RegexMatchSingleline(paramExpression, false);
            return !m.IsNull() ? m.Groups[paramGroupName].Value : "";
        }

        /// <summary>
        /// 匹配字符串
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="paramSecondsTimeOut">超时时间(s)</param>
        /// <returns></returns>
        public static string GetBody(this string input, int paramSecondsTimeOut = 10)
        {
            if (input.IsNullOrEmpty())
                return "";
            string body = input.FormatHTML();
            body = body.RegexMatchString(@"(<body[^>]*>[\s\S]*<\/body>)", false, paramSecondsTimeOut);
            body = body.RegexReplace(@"<script[^>]*>.*?[^<]*</script>", "", false, paramSecondsTimeOut);
            body = body.RegexReplace(@"<style[^>]*>.*?[^<]*</style>", "", false, paramSecondsTimeOut);
            body = body.RegexReplace(@"<!-[^-]*-.*?[^-]*->", "", false, paramSecondsTimeOut);
            body = body.RegexReplace(@"[\n\r\s]+<", "\n<", false, paramSecondsTimeOut);
            body = body.RegexReplace(@">[\n\r\s]+", ">", false, paramSecondsTimeOut);
            return body.Trim();
        }

        /// <summary>
        /// 匹配HTML嵌套标签
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="paramTag">HTML</param>
        /// <returns></returns>
        public static string RegexMatchHtmlTag(this string input, string paramTag)
        {
            if (input.IsNullOrEmpty() || paramTag.IsNullOrEmpty())
                return "";
            string result = input.RegexMatchHtmlTagByID(paramTag);
            if (result.IsNullOrEmpty())
                result = input.RegexMatchHtmlTagByName(paramTag);
            if (result.IsNullOrEmpty())
                result = input.RegexMatchHtmlTagByClass(paramTag);
            return result;
        }

        /// <summary>
        /// 匹配HTML嵌套标签
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="paramID">HTML ID</param>
        /// <returns></returns>
        public static string RegexMatchHtmlTagByID(this string input, string paramID)
        {
            if (input.IsNullOrEmpty())
                return "";
            string expression = string.Format(@"<(?<HtmlTag>[\w]+)[^>]*\s[iI][dD]=(?<Quote>[""']?){0}(?(Quote)\k<Quote>)[""']?[^>]*>((?<Nested><\k<HtmlTag>[^>]*>)|</\k<HtmlTag>>(?<-Nested>)|.*?)*</\k<HtmlTag>>", paramID);
            Match m = input.RegexMatchSingleline(expression);
            return m.IsNull() ? "" : m.Groups[0].Value;
        }

        /// <summary>
        /// 匹配HTML嵌套标签
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="paramClass">HTML Class Name</param>
        /// <returns></returns>
        public static string RegexMatchHtmlTagByName(this string input, string paramClass = "")
        {
            if (input.IsNullOrEmpty())
                return "";
            string expression = string.Format(@"<(?<HtmlTag>[\w]+)[^>]*\sname=(?<Quote>[""']?){0}(?(Quote)\k<Quote>)[""']?[^>]*>((?<Nested><\k<HtmlTag>[^>]*>)|</\k<HtmlTag>>(?<-Nested>)|.*?)*</\k<HtmlTag>>", paramClass);
            Match m = input.RegexMatchSingleline(expression);
            return m.IsNull() ? "" : m.Groups[0].Value;
        } 

        /// <summary>
        /// 匹配HTML嵌套标签
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="paramClass">HTML Class Name</param>
        /// <returns></returns>
        public static string RegexMatchHtmlTagByClass(this string input, string paramClass = "")
        {
            if (input.IsNullOrEmpty())
                return "";
            string expression = string.Format(@"<(?<HtmlTag>[\w]+)[^>]*\sclass=(?<Quote>[""']?){0}(?(Quote)\k<Quote>)[""']?[^>]*>((?<Nested><\k<HtmlTag>[^>]*>)|</\k<HtmlTag>>(?<-Nested>)|.*?)*</\k<HtmlTag>>", paramClass);
            Match m = input.RegexMatchSingleline(expression);
            return m.IsNull() ? "" : m.Groups[0].Value;
        } 
        #endregion

        #region Regex.Matches
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">匹配字符串</param>
        /// <param name="paramExpression">匹配表达式</param>
        /// <param name="timeoutSeconds">超时时间(s)</param>
        /// <returns></returns>
        public static MatchCollection RegexMatchesSingleline(this string input, string paramExpression, int timeoutSeconds = 10)
        {
            if (input.IsNullOrEmpty())
                throw new Exception("RegexMatchesSingleline:input is empty");
            if (paramExpression.IsNullOrEmpty())
                throw new Exception("RegexMatchesSingleline:expression is empty");
            try
            {
                return Regex.Matches(input, paramExpression, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(timeoutSeconds));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("RegexMatchSingleline:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 匹配多行
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="paramExpression">表达式</param>
        /// <param name="timeoutSeconds">超时时间(s)</param>
        /// <returns></returns>
        public static MatchCollection RegexMatchesMultiline(this string input, string paramExpression, int timeoutSeconds = 10)
        {
            if (input.IsNullOrEmpty())
                throw new Exception("RegexMatchesMultiline:input is empty");
            if (paramExpression.IsNullOrEmpty())
                throw new Exception("RegexMatchesMultiline:expression is empty");
            try
            {
                return Regex.Matches(input, paramExpression, RegexOptions.IgnoreCase | RegexOptions.Multiline, TimeSpan.FromSeconds(timeoutSeconds));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("RegexMatchSingleline:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 匹配多行
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="paramExpression">表达式</param>
        /// <param name="paramSecondsTimeOut">超时时间(s)</param>
        /// <returns></returns>
        public static string[] RegexMatchsStringArray(this string input, string paramExpression, int paramSecondsTimeOut = 10)
        {
            if (input.IsNullOrEmpty() || paramExpression.IsNullOrEmpty())
                return new[] { "" };
            MatchCollection ms = input.RegexMatchesMultiline(paramExpression, paramSecondsTimeOut);
            return ms.Count <= 0 ? new[] { "" } : (from Match subm in ms select subm.Groups[0].Value).ToArray();
        } 
        #endregion

        #region Regex.Replace
        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="paramExpression">表达式</param>
        /// <param name="paramReplaceString">替换字符串</param>
        /// <param name="isThrowException">是否抛出异常</param>
        /// <param name="timeoutSeconds">超时时间(s)</param>
        /// <returns></returns>
        public static string RegexReplace(this string input, string paramExpression, string paramReplaceString, bool isThrowException = true, int timeoutSeconds = 10)
        {
            if (input.IsNullOrEmpty() || paramExpression.IsNullOrEmpty())
                return "";
            try
            {
                return Regex.Replace(input, paramExpression, paramReplaceString,
                    RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(timeoutSeconds));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("RegexMatchSingleline:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 提取字符串中的数字
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static string RegexExtractNumber(this string input)
        {
            return input.IsNullOrEmpty() ? "" : input.RegexReplace(@"[^0-9]+", "");
        }
        /// <summary>
        /// 筛选HTML获取Text
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static string GetText(this string input)
        {
            if (input.IsNullOrEmpty())
                return "";
            const string regEx_html = "<.*?>";
            input = input.RegexReplace(regEx_html, "", false);
            input = input.RegexReplace("&nbsp;", "", false);
            input = input.RegexReplace(@"[\r\n]+", " ", false);
            return input.Trim();
        }
        #endregion

        /// <summary>
        /// 根据分隔字符分割字符串
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="paramSeparateChar"></param>
        /// <returns></returns>
        public static string[] RegexMultiCharSplit(this string input, string paramSeparateChar)
        {
            return Regex.Split(input, paramSeparateChar, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 字符串转换数字
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns>时间</returns>
        public static int TryToInt(this string input)
        {
            if (input.IsNullOrEmpty())
                return 0;
            try
            {
                return Convert.ToInt32(input);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 把String数组转换为字符串
        /// </summary>
        /// <param name="values">String数组</param>
        /// <returns>返回转换后的字符串</returns>
        public static string Join(this string[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (string value in values)
            {
                result.Append(CommonConstants.DIVIDED_SYMBOL);

                result.Append(value);
            }

            if (result.Length > 0)
            {
                result = result.Remove(0, CommonConstants.DIVIDED_SYMBOL.Length);
            }

            return result.ToString();
        }

        /// <summary>
        /// 把String数组转换为字符串
        /// </summary>
        /// <param name="values">String数组</param>
        /// <param name="divideSymbol">分隔符</param>
        /// <returns>返回转换后的字符串</returns>
        public static string Join(this string[] values, string divideSymbol)
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
        /// 字符串转换成时间类型
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="paramValidDays">有效天数</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string input, int paramValidDays = 0)
        {
            string strFormat = "";
            try
            {
                if (input.IsNullOrEmpty())
                    throw new Exception("字符串为空，无法转换时间类型");
                input = input.Trim();
                input = input.Replace("T24:", "T00:");
                if (input.RegexIsMatch(@"^\d{4}\-\d{1,2}-\d{1,2}T\d{1,2}\:\d{1,2}\:\d{1,2}$"))
                {
                    strFormat = "yyyy-MM-ddTHH:mm:ss";
                }
                if (input.RegexIsMatch(@"^\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}$"))
                {
                    strFormat = "yyyy-MM-dd HH:mm:ss";
                }
                if (input.RegexIsMatch(@"^\d{4}-\d{2}-\d{4}:\d{2}:\d{2}$"))
                {
                    strFormat = "yyyy-MM-ddHH:mm:ss";
                }
                if (input.RegexIsMatch(@"^\d{2}\/\d{2}\/\d{4}$"))
                {
                    strFormat = "dd/MM/yyyy";
                }
                if (input.RegexIsMatch(@"^\d{1,2}\/\d{1,2}\/\d{2}[\s]+\d{1,2}\:\d{1,2}$"))
                {
                    strFormat = "M/d/yy H:m";
                }
                if (input.RegexIsMatch(@"^\d{2}\s\d{2}\s\d{4}\s,\s\d{2}:\d{2}$"))
                {
                    strFormat = "dd MM yyyy, HH:mm";
                }
                if (input.RegexIsMatch(@"^\d{1}\s\d{2}\s\d{4}\s,\s\d{2}:\d{2}$"))
                {
                    strFormat = "d MM yyyy, HH:mm";
                }
                if (input.RegexIsMatch(@"^\d{2}\s[a-z]{3}\s\d{4},\s\d{2}:\d{2}\s[a-z]{3,4}$"))
                {
                    input = input.RegexReplace(@"^(\d{2}\s+[a-z]{3}\s+\d{4},\s\d{2}:\d{2})\s+[a-z]{3,4}$", "$1");
                    strFormat = "dd MMM yyyy, HH:mm";
                }
                DateTime dt = strFormat.IsNullOrEmpty() ? Convert.ToDateTime(input) : DateTime.ParseExact(input, strFormat, CultureInfo.CurrentCulture);
                if (dt.Year < DateTime.Now.AddYears(-10).Year)
                {
                    throw new Exception("年份无效");
                }
                if (paramValidDays > 0)
                {
                    if (dt.DayInterval(DateTime.Now.AddDays(-paramValidDays)) > 0)
                    {
                        throw new Exception(string.Format("[{0}]时间无效", dt.ToString("yyyy-MM-dd HH:mm:ss")));
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("[{0}]转换失败;转换格式[{1}]异常:{2}", input, strFormat, ex.Message));
            }
        }

        /// <summary>
        /// 字符串转换成时间类型
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="paramFormat">格式化字符串</param>
        /// <param name="paramCultureName">地区属性名称</param>
        /// <param name="paramValidDays">有效天数</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string input, string paramFormat, string paramCultureName, int paramValidDays = 0)
        {
            try
            {
                if (input.IsNullOrEmpty())
                    throw new Exception("字符串为空，无法转换时间类型");
                input = input.Trim();
                input = input.Replace("T24:", "T00:");
                if (input.RegexIsMatch(@"^\d{2}\s[a-z]{3}\s\d{4},\s\d{2}:\d{2}\s[a-z]{3,4}$"))
                {
                    input = Regex.Replace(input, @"^(\d{2}\s+[a-z]{3}\s+\d{4},\s\d{2}:\d{2})\s+[a-z]{3,4}$", "$1", RegexOptions.IgnoreCase);
                }
                CultureInfo curCultureInfo = paramCultureName.IsNullOrEmpty()
                    ? CultureInfo.CurrentCulture
                    : new CultureInfo(paramCultureName);
                DateTime dt = DateTime.ParseExact(input, paramFormat, curCultureInfo);
                if (dt.Year < DateTime.Now.AddYears(-10).Year)
                {
                    throw new Exception("年份无效");
                }
                if (paramValidDays > 0)
                {
                    if (dt.DayInterval(DateTime.Now.AddDays(-paramValidDays)) > 0)
                    {
                        throw new Exception(string.Format("[{0}]时间无效", dt.ToString("yyyy-MM-dd HH:mm:ss")));
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("[{0}]转换失败;转换格式[{1}]异常:{2}", input, paramFormat, ex.Message));
            }
        }

        /// <summary>
        /// 字符串转换成可空时间类型
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns>时间</returns>
        public static DateTime? ToDateTimeNull(this string input)
        {
            if (input.IsNullOrEmpty())
                return null;
            try
            {
                return input.ToDateTime();
            }
            catch
            {
                return null;
            }
        }

    }
}
