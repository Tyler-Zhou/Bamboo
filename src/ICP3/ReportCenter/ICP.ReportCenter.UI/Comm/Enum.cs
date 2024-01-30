using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.ReportCenter.UI
{
    /// <summary>
    /// 客户自定义列表类型
    /// </summary>
    public enum ECLGroupBy
    {
        /// <summary>
        /// 操作口岸
        /// </summary>
        [MemberDescription("业务发生地", "Business Place")]
        Company = 1,
        /// <summary>
        /// 船名航次
        /// </summary>
        [MemberDescription("船名航次", "Vessel Voyage")]
        VesselVoyage = 2,
        /// <summary>
        /// 交货地
        /// </summary>
        [MemberDescription("交货地", "Delivery Place")]
        DeliveryPlace = 3,
        /// <summary>
        /// 目的地
        /// </summary>
        [MemberDescription("目的地", "Destination Place")]
        DestinationPlace = 4,
    }
}
