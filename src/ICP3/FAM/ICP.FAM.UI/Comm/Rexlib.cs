using System.Text.RegularExpressions;

namespace ICP.FAM.UI.Comm
{
    /// <summary>
    /// 正则表达式匹配字符串类
    /// </summary>
    public class Rexlib
    {
        /// <summary>
        /// 判断是否为正整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsValidNumber(string str)
        {
            return Regex.IsMatch(str, "^[0-9]*[1-9][0-9]*$");
        }
    }
}
