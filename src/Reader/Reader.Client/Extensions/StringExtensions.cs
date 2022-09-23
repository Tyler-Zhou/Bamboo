namespace Reader.Client.Extensions
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 查找资源字典资源
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
    }
}
