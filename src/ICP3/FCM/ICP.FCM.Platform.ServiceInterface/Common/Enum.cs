using ICP.Framework.CommonLibrary.Attributes;
using System;

namespace ICP.FCM.Platform.ServiceInterface
{
    /// <summary>
    /// 签单类型
    /// </summary>
    [Serializable]
    public enum BookingStatus
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [MemberDescription("草稿", "Draft")]
        Draft = 0,

        /// <summary>
        /// 取消订舱
        /// </summary>
        [MemberDescription("取消订舱", "Cancelled")]
        Cancelled = 1,

        /// <summary>
        /// 顺签
        /// </summary>
        [MemberDescription("顺签", "Submitted")]
        Submitted = 2,

        /// <summary>
        /// 倒签
        /// </summary>
        [MemberDescription("倒签", "Booked")]
        Booked = 3,

        /// <summary>
        /// 预借
        /// </summary>
        [MemberDescription("预借", "WaitingForPricing")]
        WaitingForPricing = 4,

        /// <summary>
        /// 等待买家确认报价
        /// </summary>
        [MemberDescription("等待买家确认报价", "WaitingForBuyer")]
        WaitingForBuyer = 5,

        /// <summary>
        /// 等待卖家确认价格（贸易条款是卖家付款）
        /// </summary>
        [MemberDescription("等待卖家确认价格", "WaitingForSellerr")]
        WaitingForSellerr = 6,

        /// <summary>
        /// 业务员已确认取消
        /// </summary>
        [MemberDescription("业务员已确认取消", "ConfirmCancelled")]
        ConfirmCancelled = 7,
    }
}
