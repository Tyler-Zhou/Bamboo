//-----------------------------------------------------------------------
// <copyright file="Enum.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using ICP.Framework.CommonLibrary.Attributes;
using System;

namespace ICP.FCM.Common.ServiceInterface
{
    #region 海运提单类型
    /// <summary>
    /// 提单类型(Bill Of Lading Type)
    /// </summary>
    [Flags]
    [Serializable]
    public enum BillOfLadingType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = 0,
        /// <summary>
        /// 船东提单(MASTER BILL OF LADING)
        /// </summary>
        [MemberDescription("船东提单", "MBL")]
        MBL = 1,
        /// <summary>
        /// 货代提单(HOUSE BILL OF LADING)
        /// </summary>
        [MemberDescription("货代提单", "HBL")]
        HBL = 2,
        /// <summary>
        /// 预配提单(DECLARATION BILL OF LADING)
        /// </summary>
        [MemberDescription("预配提单", "DBL")]
        DBL = 3,
    }
    #endregion

    #region 海运提单类型
    /// <summary>
    /// 提单类型(Bill Of Lading Type)
    /// </summary>
    [Flags]
    [Serializable]
    public enum BLAMSState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = 0,
        /// <summary>
        /// 未发送(Unsent)
        /// </summary>
        [MemberDescription("未发送", "Unsent")]
        Unsent = 1,
        /// <summary>
        /// 部分发送(Partially sent)
        /// </summary>
        [MemberDescription("部分发送", "Partially Sent")]
        PartiallySent = 2,
        /// <summary>
        /// 已发送(Send)
        /// </summary>
        [MemberDescription("已发送", "Send")]
        Send = 3,
        /// <summary>
        /// 已确认(Confirmed)
        /// </summary>
        [MemberDescription("已确认", "Confirmed")]
        Confirmed = 4,
    }
    #endregion

    #region 放单状态
    /// <summary>
    /// 放单状态
    /// </summary>
    [Flags]
    [Serializable]
    public enum OceanReleaseState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 未放单
        /// </summary>
        [MemberDescription("未放单", "Hold")]
        Hold = 1,

        /// <summary>
        /// 已放单
        /// </summary>
        [MemberDescription("已放单", "Release BL")]
        Released = 3,

        /// <summary>
        /// 已接收(代理已收到放单通知)
        /// </summary>
        [MemberDescription("已接收", "Rcv RBL")]
        Received = 4,

        /// <summary>
        /// 已放货(代理已放货)
        /// </summary>
        [MemberDescription("已放货", "Release Cargo")]
        RC = 5,
    }
    #endregion

    #region 放单类型
    /// <summary>
    /// 放单类型
    /// </summary>
    [Serializable]
    public enum OceanReleaseType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("不确定", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 正本
        /// </summary>
        [MemberDescription("正本", "Original")]
        Original = 1,

        /// <summary>
        /// 电放
        /// </summary>
        [MemberDescription("电放", "Telex")]
        Telex = 2,

        /// <summary>
        /// 海单(Sea Way Bill)
        /// </summary>
        [MemberDescription("Sea way", "Sea way")]
        SeaWay = 3,
    }
    #endregion

    #region 报价目标类型
    /// <summary>
    /// 报价目标类型
    /// </summary>
    [Flags]
    [Serializable]
    public enum QPTargetType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("不确定", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 发货人
        /// </summary>
        [MemberDescription("发货人", "Shipper")]
        Shipper = 1,

        /// <summary>
        /// 收货人
        /// </summary>
        [MemberDescription("收货人", "Consignee")]
        Consignee = 2,

        /// <summary>
        /// 客户
        /// </summary>
        [MemberDescription("客户", "Customer")]
        Customer = 3,
    }
    #endregion

    #region 报价-付款方式
    /// <summary>
    /// 付款方式
    /// </summary>
    [Flags]
    [Serializable]
    public enum QPPaymentType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 预付
        /// </summary>
        [MemberDescription("预付", "Prepaid")]
        Prepaid = 1,

        /// <summary>
        /// 到付
        /// </summary>
        [MemberDescription("到付", "Collect")]
        Collect = 2,

        /// <summary>
        /// 第三方结算
        /// </summary>
        [MemberDescription("第三方结算", "Third Party Billing")]
        ThirdPartyBilling = 3
    }
    #endregion

    #region 委托人类型
    /// <summary>
    /// 委托人类型
    /// </summary>
    [Serializable]
    public enum DelegateByType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 发货人
        /// </summary>
        [MemberDescription("发货人", "Shipper")]
        Shipper = 1,

        /// <summary>
        /// 收货人
        /// </summary>
        [MemberDescription("收货人", "Consignee")]
        Consignee = 2,
    } 
    #endregion

    #region 派送方式
    /// <summary>
    /// 派送方式(DeliveryType)
    /// </summary>
    [Serializable]
    public enum DELIVERYTYPE
    {
        /// <summary>
        /// 未知
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = 0,
        /// <summary>
        /// 快递
        /// </summary>
        [MemberDescription("快递", "Express")]
        Express = 1,
        /// <summary>
        /// 拖车
        /// </summary>
        [MemberDescription("拖车", "Truck")]
        Truck = 2,
    } 
    #endregion

    

    #region 签单类型
    /// <summary>
    /// 签单类型
    /// </summary>
    [Serializable]
    public enum CSP_BOOKING_STATUS
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = -1,
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
    #endregion

    #region 货运方式
    /// <summary>
    /// 货运方式
    /// </summary>
    [Serializable]
    public enum CSP_FREIGHTMETHODTYPE
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = 0,
        /// <summary>
        /// 船运
        /// </summary>
        [MemberDescription("海运", "Ocean")]
        Ocean = 1,
        /// <summary>
        /// 空运
        /// </summary>
        [MemberDescription("空运", "Air")]
        Air = 2,
    } 
    #endregion

    #region FBA货运方式
    /// <summary>
    /// FBA货运方式(DeliveryType)
    /// </summary>
    [Serializable]
    public enum CSP_FBAFREIGHTMETHODTYPE
    {
        /// <summary>
        /// 未知
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = 0,
        /// <summary>
        /// 海运+拖车
        /// </summary>
        [MemberDescription("海运+拖车", "OceanTruck")]
        OceanTruck = 1,
        /// <summary>
        /// 快递
        /// </summary>
        [MemberDescription("快递", "Express")]
        Express = 2,
        /// <summary>
        /// 海运+快递
        /// </summary>
        [MemberDescription("海运+快递", "OceanExpress")]
        OceanExpress = 3,
        /// <summary>
        /// 空运+快递
        /// </summary>
        [MemberDescription("空运+快递", "AirExpress")]
        AirExpress = 4,
    }
    #endregion

    #region 运输类型
    /// <summary>
    /// 运输类型
    /// </summary>
    [Serializable]
    public enum CSP_SHIPMENTTYPE
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = -1,
        /// <summary>
        /// 满载集装箱（整箱）
        /// </summary>
        [MemberDescription("整箱", "FCL")]
        FCL = 0,
        /// <summary>
        /// 散装
        /// </summary>
        [MemberDescription("散装", "LCL")]
        LCL = 1,
    } 
    #endregion

    #region 送达方式
    /// <summary>
    /// 送达方式
    /// </summary>
    [Serializable]
    public enum DeliveryMethodType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = -1,
        /// <summary>
        /// 
        /// </summary>
        DeliveryGoodsByMyself = 0,
        /// <summary>
        /// 
        /// </summary>
        PickUpByCityocean = 1
    } 
    #endregion

    #region 运单事件类型
    /// <summary>
    /// 运单事件类型
    /// </summary>
    [Serializable]
    public enum CSP_SHIPMENT_EVENT_TYPE
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = -1,
        /// <summary>
        /// 操作流程事件
        /// </summary>
        [MemberDescription("操作流程事件", "Procedure Event")]
        ProcedureEvent = 0,

        /// <summary>
        /// 运输状态事件
        /// </summary>
        [MemberDescription("运输状态事件", "Shipment Status Event")]
        ShipmentStatusEvent = 1,

        /// <summary>
        /// 其它事件
        /// </summary>
        [MemberDescription("其它事件", "Other Event")]
        OthersEvent = 2
    } 
    #endregion

    #region 贸易方式
    /// <summary>
    /// 贸易方式
    /// </summary>
    [Serializable]
    public enum CSP_TRADETYPE
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = 0,
        /// <summary>
        /// 一般
        /// </summary>
        [MemberDescription("常规", "General")]
        General = 1,
        /// <summary>
        /// 亚马逊提供的代发货业务(Fulfillment by Amazon)
        /// </summary>
        /// <remarks>亚马逊仓储派送</remarks>
        [MemberDescription("亚马逊物流配送", "Fulfillment by Amazon")]
        FBA = 2,
        /// <summary>
        /// 由卖家自行发货(Fulfilment By Merchant)
        /// </summary>
        /// <remarks>卖家自发货</remarks>
        [MemberDescription("由卖家自行发货", "Fulfilment By Merchant")]
        FBM = 3
    }
    #endregion

    #region CSP联系人类型
    /// <summary>
    /// CSP联系人类型
    /// </summary>
    [Serializable]
    public enum CSP_CONTACTTYPE
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = 0,
        /// <summary>
        /// 业务员
        /// </summary>
        [MemberDescription("业务员", "Business User")]
        BusinessUser = 1,
        /// <summary>
        /// 岗前客服
        /// </summary>
        [MemberDescription("岗前客服", "Pre Port Service User")]
        PrePortServiceUser = 2,

        /// <summary>
        /// 港后客服
        /// </summary>
        [MemberDescription("港后客服", "Post Port Service User")]
        PostPortServiceUser = 3,

        /// <summary>
        /// 订舱员
        /// </summary>
        [MemberDescription("订舱员", "Booking User")]
        BookingUser = 4,

        /// <summary>
        /// 其它客服
        /// </summary>
        [MemberDescription("其它客服", "Otherr")]
        Other = 5,
        /// <summary>
        /// 总客服
        /// </summary>
        [MemberDescription("总客服", "MainServiceUser")]
        MainServiceUser = 6,

        /// <summary>
        /// 总客服
        /// </summary>
        [MemberDescription("文件", "FileUser")]
        FileUser = 7,
    }
    #endregion

    #region CSP事件表单类型
    /// <summary>
    /// CSP事件表单类型
    /// </summary>
    [Serializable]
    public enum CSP_EVENTFORMTYPE
    {
        /// <summary>
        /// The unknown
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 表示托运单事件
        /// </summary>
        [MemberDescription("表示托运单事件", "Booking")]
        Booking = 1,

        /// <summary>
        /// 表示运单事件
        /// </summary>
        [MemberDescription("表示运单事件", "Shipment")]
        Shipment = 2,

        /// <summary>
        /// 表示提单事件
        /// </summary>
        [MemberDescription("表示提单事件", "ShipmentItem")]
        ShipmentItem = 3
    } 
    #endregion
}
