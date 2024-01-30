using System;

namespace ICP.FCM.Common.ServiceInterface.CompositeObjects
{
    #region CSP业务查询条件
    /// <summary>
    /// CSP业务查询条件
    /// </summary>
    [Serializable]
    public class SearchParameterBookingDelegate
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        public CSP_BOOKING_STATUS BookingStatus { get; set; }
        /// <summary>
        /// 运输模式
        /// </summary>
        public CSP_FREIGHTMETHODTYPE FreightMethodType { get; set; }
        /// <summary>
        /// 运输类型
        /// </summary>
        public CSP_SHIPMENTTYPE ShipmentType { get; set; }
        /// <summary>
        /// 搜索Key
        /// </summary>
        public string SearchKey { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string Sorting { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int MaxResultCount { get; set; }
        /// <summary>
        /// 跳过指定条数
        /// </summary>
        public int SkipCount { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }
    } 
    #endregion

    #region CSP业务查询条件
    /// <summary>
    /// CSP下载业务
    /// </summary>
    [Serializable]
    public class SearchParameterDownloadBookingDelegate
    {
        /// <summary>
        /// 下载业务ID
        /// </summary>
        public int[] BusinessID { get; set; }
    }
    #endregion
}
