
namespace ICP.Common.ServiceInterface
{
    /// <summary>
    /// 过滤SQL关键字类
    /// </summary>
    public class SqlFilterHelper
    {
        /// <summary>
        /// 过滤SQL关键字
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string SqlFilter(string Str)
        {
            Str = Str.Replace("'", "");
            Str = Str.Replace("&", " &amp");
            Str = Str.Replace("<", "&lt");
            Str = Str.Replace(">", "&gt");
            Str = Str.Replace("delete", "");
            Str = Str.Replace("update", "");
            Str = Str.Replace("insert", "");
            Str = Str.Replace("'", " ''");
            // 半角封号替换为全角封号，防止多语句执行 
            Str = Str.Replace(";", " ；");
            // 半角括号替换为全角括号 
            Str = Str.Replace("(", "（");
            Str = Str.Replace(")", "）");
            // 去除执行存储过程的命令关键字 
            Str = Str.Replace("Exec", "");
            Str = Str.Replace("Execute", "");
            // 去除系统存储过程或扩展存储过程关键字 
            Str = Str.Replace("xp_", "x p_");
            Str = Str.Replace("sp_", "s p_");
            // 防止16进制注入 
            Str = Str.Replace("0x", "0 x");
            return Str;
        }
    }
}
