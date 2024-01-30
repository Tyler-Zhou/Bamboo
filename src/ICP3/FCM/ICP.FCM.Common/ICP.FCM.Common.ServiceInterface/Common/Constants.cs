
namespace ICP.FCM.Common.ServiceInterface.Common
{

    /// <summary>
    /// FCM通用常量
    /// </summary>
    public class FCMCommonContants
    {
        /// <summary>
        /// CSP订舱单下载
        /// </summary>
        public const string COMMAND_CSPBOOKING_DOWNLOAD = "Command_CSPBooking_Download";
    }

    /// <summary>
    /// 搜索器常量
    /// </summary>
    public class FCMFinderConstants
    {
        /// <summary>
        /// 业务搜索器
        /// </summary>
        public const string BusinessFinder = "BusinessFinder";
        /// <summary>
        /// 海出、空出
        /// </summary>
        public const string BusinessFinderForOEAE = "BusinessFinderForOEAE";

        /// <summary>
        /// 业务搜索器,搜索业务类型为海出，空出，其它业务的业务
        /// </summary>
        public const string BusinessFinderForOEAEOB = "BusinessFinderForOEAEOB";

        /// <summary>
        /// 业务搜索器,搜索业务类型为海进的业务
        /// </summary>
        public const string BusinessFinderForOI = "BusinessFinderForOI";
    }

    /// <summary>
    /// 权限常量
    /// </summary>
    public class FCMPermissionsConstants
    {
        /// <summary>
        /// 修改订单
        /// </summary>
        public const string FCM_EditOrder = "FCM_EDITORDER";

        /// <summary>
        /// 北美客服专员
        /// </summary>
        public const string FCM_NAServices = "FCM_NASERVICES";
    }

}
