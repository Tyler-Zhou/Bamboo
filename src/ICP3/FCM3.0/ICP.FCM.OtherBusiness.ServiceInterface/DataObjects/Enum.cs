//-----------------------------------------------------------------------
// <copyright file="Enum.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.OtherBusiness.ServiceInterface.DataObjects
{
    using ICP.Framework.CommonLibrary.Attributes;


    /// <summary>
    /// 报关类型
    /// </summary>
    public enum CustomsType
    {
        /// <summary>
        /// 不确定        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 转关
        /// </summary>
        [MemberDescription("转关")]
        转关 = 1,

        /// <summary>
        /// 清关
        /// </summary>
        [MemberDescription("清关")]
        清关 = 2,

        /// <summary>
        /// 买单
        /// </summary>
        [MemberDescription("买单")]
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
        /// 退关        /// </summary>
        [MemberDescription("退关")]
        退关 = 8,

        /// <summary>
        /// 退运        /// </summary>
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
        /// 业务号        /// </summary>
        [MemberDescription("业务号", "Op No.")]
        Operation = 1,

        /// <summary>
        /// 航班号        /// </summary>
        [MemberDescription("航班号", "SO No.")]
        FilightNo = 2,

        /// <summary>
        /// 提单号        /// </summary>
        [MemberDescription("提单号", "BL No.")]
        BL = 3,    

        /// <summary>
        /// 帐单号        /// </summary>
        [MemberDescription("帐单号", "Bill No.")]
        Bill = 4,

        /// <summary>
        /// 合约号        /// </summary>
        [MemberDescription("合约号", "SC No.")]
        Contract = 5
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
        /// 航空公司
        /// </summary>
        [MemberDescription("航空公司", "Air Company")]
        AirCompany = 2,

        /// <summary>
        /// 承运人        /// </summary>
        [MemberDescription("承运人", "Carrier")]
        Carrier = 3,

        /// <summary>
        /// 发货人        /// </summary>
        [MemberDescription("发货人", "Shipper")]
        Shipper = 4,

        /// <summary>
        /// 收货人        /// </summary>
        [MemberDescription("收货人", "Consignee")]
        Consignee = 5,

        ///// <summary>
        ///// 通知人        ///// </summary>
        //[MemberDescription("通知人", "Notify")]
        //NotifyPart = 6,

        /// <summary>
        /// 对单人        /// </summary>
        [MemberDescription("对单人", "Checker")]
        Checker = 6,

        /// <summary>
        /// 代理
        /// </summary>
        [MemberDescription("代理", "Agent")]
        Agent = 7
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
        /// 业务员        /// </summary>
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
    /// 地点查询类型
    /// </summary>
    public enum PortSearchType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部地点", "All Location")]
        All = 0,  

        /// <summary>
        /// 起运港        /// </summary>
        [MemberDescription("起运港", "Departure")]
        Departure = 1,      

        /// <summary>
        /// 目的港        /// </summary>
        [MemberDescription("目的港", "Detination")]
        Detination = 2,

        /// <summary>
        /// 交货地        /// </summary>
        [MemberDescription("交货地", "Place Of Delivery")]
        PlaceOfDelivery = 3   
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
        /// 创建日        /// </summary>
        [MemberDescription("创建日", "Create Date")]
        CreateDate = 1,

        /// <summary>
        /// 订舱日        /// </summary>
        [MemberDescription("订舱日", "Booking Date")]
        BookingDate = 2,

        /// <summary>
        /// 起航日
        /// </summary>
        [MemberDescription("起航日", "ETD")]
        ETD = 3,

        /// <summary>
        /// 到达日        /// </summary>
        [MemberDescription("到达日", "ETA")]
        ETA = 4
    }

    /// <summary>
    /// ACI Entry Type IntransitGoodsToUS = 23,ImportedGoods = 24,FROB = 26
    /// </summary>
    public enum ACIEntryType
    {
        /// <summary>
        /// 不确定        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Intransit Goods to US
        /// </summary>
        IntransitGoodsToUS = 23,

        /// <summary>
        /// Imported Goods
        /// </summary>
        ImportedGoods = 24,

        /// <summary>
        /// FROB
        /// </summary>
        FROB = 26
    }

    /// <summary>
    /// AMS Entry Type(PorttoPort=60, InlandTransit = 61,TransitExport = 62,ImmediateReexport = 63,StayonBoard = 64
    /// </summary>
    public enum AMSEntryType
    {
        /// <summary>
        /// 不确定        /// </summary>
        Unknown = 0,

        /// <summary>
        /// PorttoPort
        /// </summary>
        PorttoPort = 60,

        /// <summary>
        /// InlandTransit
        /// </summary>
        InlandTransit = 61,

        /// <summary>
        /// TransitExport
        /// </summary>
        TransitExport = 62,

        /// <summary>
        /// ImmediateReexport
        /// </summary>
        ImmediateReexport = 63,

        /// <summary>
        /// StayonBoard
        /// </summary>
        StayonBoard = 64
    }

    /// <summary>
    /// 仓单类型
    /// </summary>
    public enum WarehouseServiceType
    {
        /// <summary>
        /// 不确定        /// </summary>
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
    /// 提单类型（1:MAWB，2:HAWB）    /// </summary>
    public enum BLType
    {
        /// <summary>
        /// 不确定        /// </summary>
        Unknown = 0,

        /// <summary>
        /// MAWB
        /// </summary>
        MAWB = 1,

        /// <summary>
        /// HAWB
        /// </summary>
        HAWB = 2
    }

    /// <summary>
    /// 运价等级
    /// </summary>
    public enum RateClass
    {
        /// <summary>
        /// 不确定
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 最低运费Minimum Charge
        /// </summary>
        M = 1,

        /// <summary>
        /// 45公斤以下（或100公斤以下）运价 Normal Rate
        /// </summary>
        N = 2,

        /// <summary>
        /// 45公斤以上运价 Quantity Rate
        /// </summary>
        Q=3,

        /// <summary>
        /// 制定商品运价Specific commodity Rate
        /// </summary>
        C=4,

        /// <summary>
        /// 等级货物附减运价Class Rate Reduction
        /// </summary>
        R=5,

        /// <summary>
        /// 等级货物附加运价Class Rate Surcharge
        /// </summary>
        S=6,

        /// <summary>
        /// 集装化设备附加运价Unit load Device Basic Charge or Rate
        /// </summary>
        U=7,

        /// <summary>
        /// 集装化设备附加运价Unit load Device Additional Rate
        /// </summary>
        E=8,

        /// <summary>
        /// 集装化设备附加说明Unit load Device Additional Information
        /// </summary>
        X=9,

        /// <summary>
        /// 集装化设备折扣Unit load Device Discount
        /// </summary>
        Y=10
    }
    public enum OtOperationType
    {
        /// <summary>
        /// 本地业务
        /// </summary>
        [MemberDescription("本地业务")]
        CustomsDeclaration = 1,

        /// <summary>
        /// 代订舱
        /// </summary>
        [MemberDescription("代订舱")]
        Booking = 2,


        /// <summary>
        /// 其他业务
        /// </summary>
        [MemberDescription("其他业务")]
        Others = 3,
    }
}
