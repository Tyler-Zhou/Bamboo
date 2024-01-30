using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.Common.ServiceInterface.Common
{
     /// <summary>
    /// 搜索器常量    /// </summary>
    public class FCMFinderConstants
    {
        /// <summary>
        /// 业务搜索器
        /// </summary>
        public const string BusinessFinder = "BusinessFinder";

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
        /// 修改订单
        /// </summary>
        public const string FCM_EditOrder = "FCM_EDITORDER";

        /// 北美客服专员
        /// </summary>
        public const string FCM_NAServices = "FCM_NASERVICES";
    }

}
