
namespace ICP.OA.UI
{
    public class Constants
    {
        public const string MainWorkspace = "MainWorkspace";
    }

    /// <summary>
    /// OA模块常量
    /// </summary>
    public class FunctionConstants
    {
        /// <summary>
        /// 邮件列表
        /// </summary>
        public const string OA_EMAILLIST = "OA_EMAILLIST";

        /// <summary>
        /// 文档列表
        /// </summary>
        public const string OA_DOCUMENTLIST = "OA_DOCUMENTLIST";

        /// <summary>
        /// 商务信息
        /// </summary>
        public const string OA_BUSINESSLIST = "OA_BUSINESSLIST";

        /// <summary>
        /// 通讯录
        /// </summary>
        public const string OA_ADDRESSLIST = "OA_ADDRESSLIST";
        /// <summary>
        /// 邮件中心日志
        /// </summary>
        public const string OA_MAILCENTERMEMO = "OA_MAILCENTERMEMO";

        /// <summary>
        /// 订舱统计
        /// </summary>
        public const string OA_BOOKINGLIST = "OA_BOOKINGLIST";

        /// <summary>
        /// 公告列表
        /// </summary>
        public const string OA_BULLETINLIST = "OA_BULLETINLIST";

        /// <summary>
        /// 员工信息
        /// </summary>
        public const string OA_UserInfoList = "OA_UserInfoList";

        public const string OA_FAXHALLList = "OA_FAXHALLList";

    }

    public class SearchFieldConstants
    {
        public static readonly string[] ResultValue = new string[] { "ID", "Code", "EName", "CName" };
        public static readonly string[] UserResultValue = new string[] { "ID", "Code", "EName", "CName,Mail" };
        public const string CodeName = @"Code/Name";
        public const string Name = "Name";
        public const string Code = "Code";
    }
}
