using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Platform;
using System;
using System.Collections.Generic;

namespace ICP.FCM.Common.ServiceComponent.JSONObjects
{
    #region CSP舱单状态
    /// <summary>
    /// CSP舱单状态
    /// </summary>
    [Serializable]
    public class BookingStatusForCSPAPI
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 新状态
        /// </summary>
        public CSP_BOOKING_STATUS newStatus { get; set; }

    }
    #endregion

    #region CSP舱单状态-单个
    /// <summary>
    /// CSP舱单状态-单个
    /// </summary>
    [Serializable]
    public class BillStatusForCSPAPIItem : PlatformResponseContent<BookingStatusForCSPAPI>
    {
    }
    #endregion

    #region CSP舱单状态状态-列表
    /// <summary>
    /// CSP舱单状态-列表
    /// </summary>
    [Serializable]
    public class BillStatusForCSPAPIList : PlatformResponseBaseList<List<BookingStatusForCSPAPI>>
    {
    }
    #endregion
}
