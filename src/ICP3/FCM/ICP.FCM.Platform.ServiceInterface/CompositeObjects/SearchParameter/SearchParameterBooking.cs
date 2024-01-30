using System;

namespace ICP.FCM.Platform.ServiceInterface
{
    /// <summary>
    /// Booking查询参数
    /// </summary>
    [Serializable]
    public class SearchParameterBooking
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        public BookingStatus BookingStatus { get; set; }
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
    }
}
