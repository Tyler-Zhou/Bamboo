using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.TMS.ServiceInterface
{
    /// <summary>
    /// 拖车业务类型
    /// </summary>
    public enum TruckBookingType
    {
        /// <summary>
        /// 出口
        /// </summary>
        [MemberDescription("出口", "Export")]
        Export = 1,

        /// <summary>
        /// 进口
        /// </summary>
        [MemberDescription("进口", "Import")]
        Import = 2

    }

    /// <summary>
    /// 查询拖车业务类型
    /// </summary>
    public enum SearchTruckBookingType
    {
        [MemberDescription("全部", "All")]
        All=0,
        /// <summary>
        /// 出口
        /// </summary>
        [MemberDescription("出口", "Export")]
        Export = 1,

        /// <summary>
        /// 进口
        /// </summary>
        [MemberDescription("进口", "Import")]
        Import = 2

    }

    /// <summary>
    /// 拖车业务时间查询类型
    /// </summary>
    public enum TruckBusinessDateSeachType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部", "All")]
        All = 0,

        /// <summary>
        /// 创建时间
        /// </summary>
        [MemberDescription("创建时间", "Create Date")]
        CreateDate = 1,

        /// <summary>
        /// 派车时间
        /// </summary>
        [MemberDescription("派车时间", "Truck Date")]
        TruckDate = 2

    }

    /// <summary>
    /// 拖车数据时间查询类型
    /// </summary>
    public enum TruckDateSeachType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部时间", "ALL Date")]
        ALL=0,
        /// <summary>
        /// 创建时间
        /// </summary>
        [MemberDescription("创建时间", "Create Date")]
        CreateDate = 1,

        /// <summary>
        /// 购买时间
        /// </summary>
        [MemberDescription("派车时间", "Buy Date")]
        BuyDate = 2

    }
    /// <summary>
    /// 拖车业务状态
    /// </summary>
    public enum SearchTruckBusinessState
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部", "ALL")]
        ALL = 0,
        /// <summary>
        /// 未派车
        /// </summary>
        [MemberDescription("未派车", "No Truck")]
        NoTruck = 1,
        /// <summary>
        /// 已派车
        /// </summary>
        [MemberDescription("已派车", "Trucked")]
        Trucked = 2,
        /// <summary>
        /// 已提柜
        /// </summary>
        [MemberDescription("已提柜", "PickAt")]
        PickAt = 3,
        /// <summary>
        /// 已交货
        /// </summary>
        [MemberDescription("已交货", "Delivery")]
        Delivery = 4,
        /// <summary>
        /// 已还柜
        /// </summary>
        [MemberDescription("已还柜", "Return")]
        Return = 5,
        /// <summary>
        /// 已完成
        /// </summary>
        [MemberDescription("已完成", "Completed")]
        Completed = 6
    }

    /// <summary>
    /// 拖车业务状态
    /// </summary>
    public enum TruckBusinessState
    {

        /// <summary>
        /// 未派车
        /// </summary>
        [MemberDescription("未派车", "No Truck")]
        NoTruck = 1,
        /// <summary>
        /// 已派车
        /// </summary>
        [MemberDescription("已派车", "Trucked")]
        Trucked = 2,
        /// <summary>
        /// 已提柜
        /// </summary>
        [MemberDescription("已提柜", "PickAt")]
        PickAt = 3,
        /// <summary>
        /// 已交货
        /// </summary>
        [MemberDescription("已交货", "Delivery")]
        Delivery = 4,
        /// <summary>
        /// 已还柜
        /// </summary>
        [MemberDescription("已还柜", "Return")]
        Return = 5,
        /// <summary>
        /// 已完成
        /// </summary>
        [MemberDescription("已完成", "Completed")]
        Completed = 6
    }


    /// <summary>
    /// 委托方式
    /// </summary>
    public enum BookingMode
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("不确定", "Unkown")]
        Unknown = 0,

        /// <summary>
        /// 传真订舱
        /// </summary>
        [MemberDescription("传真委托", "Fax")]
        Fax = 1,

        /// <summary>
        /// 邮件订舱
        /// </summary>
        [MemberDescription("邮件委托", "E-mail")]
        EMail = 2,

        /// <summary>
        /// 网上订舱
        /// </summary>
        [MemberDescription("网上委托", "Internet")]
        Internet = 3
    }

    /// <summary>
    /// 拖车编辑界面类型
    /// </summary>
    public enum TruckBookingEditType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 编辑
        /// </summary>
        Edit = 1,
        /// <summary>
        /// 派车
        /// </summary>
        Truck = 2
    }
    /// <summary>
    /// 下载出口提单状态
    /// </summary>
    public enum DownLoadState
    {
        /// <summary>
        /// 未下载
        /// </summary>
        [MemberDescription("未下载", "Pending")]
        Pending = 1,

        /// <summary>
        /// 下载中
        /// </summary>
        [MemberDescription("下载中", "Downloading")]
        Downloading = 2,

        /// <summary>
        /// 已下载
        /// </summary>
        [MemberDescription("已下载", "Downloaded")]
        Downloaded = 3,
    }
}
