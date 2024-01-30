#region Comment

/*
 * 
 * FileName:    ClientUtility.cs
 * CreatedOn:   2014/5/14 星期三 17:56:27
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->客户端常量定义
 * History：
 * 
 * 
 * 
 * 
 */

#endregion


namespace ICP.Document
{
    /// <summary>
    /// 客户端常量
    /// </summary>
    public class ClientConstants
    {
        /// <summary>
        /// 当前账号
        /// </summary>
        public static string CurrentAccount = "";
        /// <summary>
        /// 当前缓存数据库文件路径
        /// </summary>
        public static string CurrentDBPath = "";
        /// <summary>
        /// 缓存数据库文件完整名
        /// </summary>
        public static string DBFullName = "ICP35DataCache.sdf";

        public static string PreviewAppName = "ICP.FilePreviewService.exe";
    }
}
