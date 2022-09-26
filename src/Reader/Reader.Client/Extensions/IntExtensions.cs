namespace Reader.Client.Extensions
{
    /// <summary>
    /// Int扩展方法
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// 左补全位数
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="number">位数</param>
        /// <param name="prefix">前缀</param>
        /// <param name="suffix">后缀</param>
        /// <returns></returns>
        public static string Completion(this int input, int number, string prefix, string suffix)
        {
            return $"{prefix}{("" + input).PadLeft(number, '0')}{suffix}";
        }
    }
}
