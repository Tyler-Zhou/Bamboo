//-----------------------------------------------------------------------
// <copyright file="Enum.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects
{
    using ICP.Framework.CommonLibrary.Attributes;



    /// <summary>
    /// 签单类型
    /// </summary>
    public enum IssueType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("不确定", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 正常
        /// </summary>
        [MemberDescription("正常", "Normal")]
        Normal = 1,

        /// <summary>
        /// 顺签
        /// </summary>
        [MemberDescription("顺签", "Post_date")]
        Post_date = 2,

        /// <summary>
        /// 倒签
        /// </summary>
        [MemberDescription("倒签", "Anti_date")]
        Anti_date = 3,

        /// <summary>
        /// 预借
        /// </summary>
        [MemberDescription("预借", "Advanced")]
        Advanced = 4,
    }


    /// <summary>
    /// 报关类型
    /// </summary>
    public enum CustomsType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("不确定", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 转关
        /// </summary>
        [MemberDescription("转关", "")]
        转关 = 1,

        /// <summary>
        /// 清关
        /// </summary>
        [MemberDescription("清关", "")]
        清关 = 2,

        /// <summary>
        /// 买单
        /// </summary>
        [MemberDescription("买单", "")]
        买单 = 3,

        /// <summary>
        /// 包单
        /// </summary>
        [MemberDescription("包单")]
        包单 = 4,

        /// <summary>
        /// 证书
        /// </summary>
        [MemberDescription("证书")]
        证书 = 5,

        /// <summary>
        /// 入仓
        /// </summary>
        [MemberDescription("入仓")]
        入仓 = 6,

        /// <summary>
        /// 进口
        /// </summary>
        [MemberDescription("进口")]
        进口 = 7,

        /// <summary>
        /// 退关
        /// </summary>
        [MemberDescription("退关")]
        退关 = 8,

        /// <summary>
        /// 退运
        /// </summary>
        [MemberDescription("退运")]
        退运 = 9
    }

    /// <summary>
    /// 单号搜索类型
    /// </summary>
    public enum NoSearchType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部单号", "All No")]
        All = 0,

        /// <summary>
        /// 业务号
        /// </summary>
        [MemberDescription("业务号", "Op No.")]
        Operation = 1,

        /// <summary>
        /// 订舱号
        /// </summary>
        [MemberDescription("订舱号", "SO No.")]
        ShippingOrder = 2,

        /// <summary>
        /// 提单号
        /// </summary>
        [MemberDescription("提单号", "BL No.")]
        BL = 3,

        /// <summary>
        /// 箱号
        /// </summary>
        [MemberDescription("箱号", "CTN No.")]
        Container = 4,

        /// <summary>
        /// 帐单号
        /// </summary>
        [MemberDescription("帐单号", "Bill No.")]
        Bill = 5,

        /// <summary>
        /// 合约号
        /// </summary>
        [MemberDescription("合约号", "SC No.")]
        Contract = 6
    }

    /// <summary>
    /// 客户搜索类型
    /// </summary>
    public enum CustomerSearchType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部客户", "All Customer")]
        All = 0,

        /// <summary>
        /// 客户
        /// </summary>
        [MemberDescription("客户", "Customer")]
        Customer = 1,

        /// <summary>
        /// 船东
        /// </summary>
        [MemberDescription("船东", "Ship Owner")]
        ShipOwner = 2,

        /// <summary>
        /// 承运人
        /// </summary>
        [MemberDescription("承运人", "Carrier")]
        Carrier = 3,

        /// <summary>
        /// 发货人
        /// </summary>
        [MemberDescription("发货人", "Shipper")]
        Shipper = 4,

        /// <summary>
        /// 收货人
        /// </summary>
        [MemberDescription("收货人", "Consignee")]
        Consignee = 5,

        /// <summary>
        /// 通知人
        /// </summary>
        [MemberDescription("通知人", "Notify")]
        NotifyPart = 6,

        /// <summary>
        /// 对单人
        /// </summary>
        [MemberDescription("对单人", "Checker")]
        Checker = 7,

        /// <summary>
        /// 代理
        /// </summary>
        [MemberDescription("代理", "Agent")]
        Agent = 8
    }

    /// <summary>
    /// 航次搜索类型
    /// </summary>
    public enum VoyageSearchType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部航次", "All Voyage")]
        All = 0,

        /// <summary>
        /// 驳船
        /// </summary>
        [MemberDescription("驳船", "PreVoayge")]
        PreVoayge = 1,

        /// <summary>
        /// 大船
        /// </summary>
        [MemberDescription("大船", "Voyage")]
        Voyage = 2
    }

    /// <summary>
    /// 用户搜索类型
    /// </summary>
    public enum UserSearchType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部用户", "All User")]
        All = 0,

        /// <summary>
        /// 业务员
        /// </summary>
        [MemberDescription("业务员", "Sales")]
        Sales = 1,

        /// <summary>
        /// 客服
        /// </summary>
        [MemberDescription("客服", "Customer Service")]
        CustomerService = 2,

        /// <summary>
        /// 订舱
        /// </summary>
        [MemberDescription("订舱", "Booking")]
        Booking = 3,

        /// <summary>
        /// 文件
        /// </summary>
        [MemberDescription("文件", "File")]
        File = 4
    }

    /// <summary>
    /// 港口搜索类型
    /// </summary>
    public enum PortSearchType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部地点", "All Location")]
        All = 0,

        /// <summary>
        /// 收货地
        /// </summary>
        [MemberDescription("收货地", "Place Of Receipt")]
        PlaceOfReceipt = 1,

        /// <summary>
        /// 装货港
        /// </summary>
        [MemberDescription("装货港", "POL")]
        POL = 2,


        /// <summary>
        /// 中转港
        /// </summary>
        [MemberDescription("中转港", "Transhipment Port")]
        TranshipmentPort = 3,

        /// <summary>
        /// 卸货港
        /// </summary>
        [MemberDescription("卸货港", "POD")]
        POD = 4,

        /// <summary>
        /// 交货地
        /// </summary>
        [MemberDescription("交货地", "Place Of Delivery")]
        PlaceOfDelivery = 5,

        /// <summary>
        /// 最终目的地
        /// </summary>
        [MemberDescription("最终目的地", "Final Destination")]
        FinalDestination = 6
    }

    /// <summary>
    /// 日期搜索类型
    /// </summary>
    public enum DateSearchType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部日期", "All Date")]
        All = 0,

        /// <summary>
        /// 创建日
        /// </summary>
        [MemberDescription("创建日", "Create Date")]
        CreateDate = 1,

        /// <summary>
        /// 订舱日
        /// </summary>
        [MemberDescription("订舱日", "Booking Date")]
        BookingDate = 2,


        /// <summary>
        /// 离港日
        /// </summary>
        [MemberDescription("离港日", "ETD")]
        ETD = 3,

        /// <summary>
        /// 到港日
        /// </summary>
        [MemberDescription("到港日", "ETA")]
        ETA = 4
    }

    /// <summary>
    /// 海出提单日期搜索类型(加分发日期)，放单日期
    /// </summary>
    public enum DateSearchDispatchType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部日期", "All Date")]
        All = 0,

        /// <summary>
        /// 创建日
        /// </summary>
        [MemberDescription("创建日", "Create Date")]
        CreateDate = 1,

        /// <summary>
        /// 订舱日
        /// </summary>
        [MemberDescription("订舱日", "Booking Date")]
        BookingDate = 2,


        /// <summary>
        /// 离港日
        /// </summary>
        [MemberDescription("离港日", "ETD")]
        ETD = 3,

        /// <summary>
        /// 到港日
        /// </summary>
        [MemberDescription("到港日", "ETA")]
        ETA = 4,

        /// <summary>
        /// 分发日期
        /// </summary>
        [MemberDescription("分发日期", "DispatchDate")]
        DispatchDate = 5,

        /// <summary>
        /// 放单日期
        /// </summary>
        [MemberDescription("放单日期", "ReleaseDate")]
        ReleaseDate = 6
    }

    /// <summary>
    /// 申报货物性质
    /// </summary>
    public enum CargoTypeForAMS
    {
        [MemberDescription("Unknown", "Unknown")]
        Unknown = 0,
        [MemberDescription("Standard", "Standard")]
        Standard = 1,
        [MemberDescription("ToOrderShipments", "ToOrderShipments")]
        ToOrderShipments = 2,
        [MemberDescription("HouseholdGoodsOrPersonalEffects", "HouseholdGoodsOrPersonalEffects")]
        HouseholdGoodsOrPersonalEffects = 3,
        [MemberDescription("Government,Military", "Government,Military")]
        GovernmentMilitary = 4,
        [MemberDescription("Diplomatic", "Diplomatic")]
        Diplomatic = 5,
        [MemberDescription("Carnets", "Carnets")]
        Carnets = 6,
        [MemberDescription("U.SGoodsReturned", "U.SGoodsReturned")]
        USGoodsReturned = 7,
        [MemberDescription("FTZShipments", "FTZShipments")]
        FTZShipments = 8,
        [MemberDescription("InternationalMailShipments", "InternationalMailShipments")]
        InternationalMailShipments = 9,
        [MemberDescription("OuterContinentalShelfShipments", "OuterContinentalShelfShipments")]
        OuterContinentalShelfShipments = 10,
        [MemberDescription("Informal", "Informal")]
        Informal = 11
    }

    /// <summary>
    /// ShipmentType 
    /// </summary>
    public enum ShipmentType
    {
        [MemberDescription("Unknown", "Unknown")]
        Unknown = 0,
        [MemberDescription("FCL", "FCL")]
        FCL = 1,
        [MemberDescription("LCL", "LCL")]
        LCL = 2
    }

    /// <summary>
    /// 进口商税号类型
    /// </summary>
    public enum ImportRefType
    {
        [MemberDescription("Unknown", "Unknown")]
        Unknown = 0,
        [MemberDescription("SCAC", "SCAC")]
        SCAC = 5,
        [MemberDescription("EIN", "EIN")]
        EIN = 1,
        [MemberDescription("SSN", "SSN")]
        SSN = 2,
        [MemberDescription("CBP", "CBP")]
        CBP = 3,
        [MemberDescription("Passport", "Passport")]
        Passport = 4
    }

    /// <summary>
    /// BondRef#: 
    /// </summary>
    public enum BondRef
    {
        [MemberDescription("Unknown", "Unknown")]
        Unknown = 0,
        [MemberDescription("EIN", "EIN")]
        EIN = 1,
        [MemberDescription("SSN", "SSN")]
        SSN = 2,
        [MemberDescription("CBP", "CBP")]
        CBP = 3
    }
    /// <summary>
    /// 收货人和买家税号类型
    /// </summary>
    public enum ConsigneeAndBuyerType
    {
        [MemberDescription("Unknown", "Unknown")]
        Unknown = 0,
        [MemberDescription("EIN", "EIN")]
        EIN = 1,
        [MemberDescription("SSN", "SSN")]
        SSN = 2,
        [MemberDescription("CBP", "CBP")]
        CBP = 3,
        [MemberDescription("Passport", "Passport")]
        Passport = 4
    }

    /// <summary>
    /// BondActivityCode
    /// </summary>
    public enum BondActivityCode
    {
        [MemberDescription("Unknown", "Unknown")]
        Unknown = 0,
        [MemberDescription("Importer Or Broker", "Importer Or Broker")]
        ImporterOrBroker = 1,
        [MemberDescription("Custodian Of Bonded Merchandise", "Custodian Of Bonded Merchandise")]
        CustodianOfBondedMerchandise = 2,
        [MemberDescription("International Carrier", "International Carrier")]
        InternationalCarrier = 3,
        [MemberDescription("Foreign Trade Zone Operator", "Foreign Trade Zone Operator")]
        ForeignTradeZoneOperator = 4,
        [MemberDescription("ISF Bond", "ISF Bond")]
        ISFBond = 5
    }

    /// <summary>
    /// 确认装船类型（0不确定，1驳船确定，2大船确定3全部）
    /// </summary>
    public enum ConfirmOnBoardType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("不确定", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 驳船
        /// </summary>
        [MemberDescription("驳船", "PreConfirm")]
        PreConfirm = 1,

        /// <summary>
        /// 大船
        /// </summary>
        [MemberDescription("大船", "Confirm")]
        Confirm = 2,

        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部", "All")]
        All = 3
    }

    /// <summary>
    /// 装船类型
    /// </summary>
    public enum VoyageShowType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("不确定", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 驳船
        /// </summary>
        [MemberDescription("驳船", "PreConfirm")]
        PreConfirm = 1,

        /// <summary>
        /// 大船
        /// </summary>
        [MemberDescription("大船", "Confirm")]
        Confirm = 2,

        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部", "All")]
        All = 3
    }

    /// <summary>
    /// 仓单类型
    /// </summary>
    public enum WarehouseServiceType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 进仓
        /// </summary>
        [MemberDescription("入库", "In")]
        In = 1,

        /// <summary>
        /// 出仓
        /// </summary>
        [MemberDescription("出库", "Out")]
        Out = 2
    }

    /// <summary>
    /// 邮件订舱/补料唯一代码
    /// </summary>
    public enum EmailBookingSICode
    {
        /// <summary>
        /// 深圳中海订舱
        /// </summary>
        SZCSCLBooking = 1,
        /// <summary>
        /// 广州中海订舱
        /// </summary>
        GZCSCLBooking = 2
    }

    /// <summary>
    /// 选择合约运价还是询价
    /// </summary>
    public enum SelectType
    {
        /// <summary>
        /// 选择合约
        /// </summary>
        Contract = 1,

        /// <summary>
        /// 询价
        /// </summary>
        InquirePrice = 2
    }
}
