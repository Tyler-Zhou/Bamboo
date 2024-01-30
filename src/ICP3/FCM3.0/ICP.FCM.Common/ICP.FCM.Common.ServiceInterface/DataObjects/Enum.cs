//-----------------------------------------------------------------------
// <copyright file="Enum.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    using ICP.Framework.CommonLibrary.Attributes;


    #region 公共

    ///// <summary>
    ///// 核销单业务类型
    ///// </summary>
    //public enum VerificaSheetOperationType 
    //{
    //    /// <summary>
    //    /// 海运出口
    //    /// </summary>
    //    [MemberDescription("海运出口", "OceanExport")]
    //    OceanExport = 1,

    //    /// <summary>
    //    /// 空运出口
    //    /// </summary>
    //    [MemberDescription("空运出口", "AirExport")]
    //    AirExport = 3,

    //    /// <summary>
    //    /// 其他业务
    //    /// </summary>
    //    [MemberDescription("其他业务", "OtherBusiness")]
    //    OtherBusiness = 5,
    //}

    /// <summary>
    /// 日期搜索类型
    /// </summary>
    public enum DateSearchTypeForDataFinder
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

        ///// <summary>
        ///// 订舱日
        ///// </summary>
        //[MemberDescription("订舱日", "Booking Date")]
        //BookingDate = 2,


        /// <summary>
        /// 离港日
        /// </summary>
        [MemberDescription("离港日", "ETD")]
        ETD = 2,

        /// <summary>
        /// 到港日
        /// </summary>
        [MemberDescription("到港日", "ETA")]
        ETA = 3
    }

    /// <summary>
    /// 付款方式
    /// </summary>
    public enum PaymentType
    {
        /// <summary>
        /// 预付
        /// </summary>
        [MemberDescription("预付")]
        PP = 0,

        /// <summary>
        /// 到付
        /// </summary>
        [MemberDescription("到付")]
        CC = 1
    }


    /// <summary>
    /// 单证类型
    /// </summary>
    public enum DocumentType
    {
        /// <summary>
        /// 核销单
        /// </summary>
        [MemberDescription("核销单", "Verification sheet")]
        VerificationSheet = 0,

        /// <summary>
        /// 提单
        /// </summary>
        [MemberDescription("提单", "Bill of lading ")]
        BL = 1
    }

    /// <summary>
    /// 文档交接方式
    /// </summary>
    public enum DocumentReleaseMode
    {
        /// <summary>
        /// 快递
        /// </summary>
        [MemberDescription("快递", "Express")]
        Express = 0,

        /// <summary>
        /// 当面
        /// </summary>
        [MemberDescription("当面", "Personally")]
        Personally = 1
    }

    /// <summary>
    /// 重要性（0普通、1高、2低）
    /// </summary>
    public enum EMailPriority
    {
        /// <summary>
        /// 普通
        /// </summary>
        [MemberDescription("普通")]
        Normal,

        /// <summary>
        /// 高
        /// </summary>
        [MemberDescription("高")]
        High,

        /// <summary>
        /// 低
        /// </summary>
        [MemberDescription("低")]
        Low
    }

    /// <summary>
    /// 货物类型
    /// </summary>
    public enum CargoType
    {
        /// <summary>
        /// 干货
        /// </summary>
        [MemberDescription("干货")]
        Dry = 0,

        /// <summary>
        /// 冷藏货
        /// </summary>
        [MemberDescription("冷藏货")]
        Reefer = 1,

        /// <summary>
        /// 危险货
        /// </summary>
        [MemberDescription("危险货")]
        Dangerous = 2,

        /// <summary>
        /// 特种货
        /// </summary>
        [MemberDescription("特种货")]
        Awkward = 3
    }

    /// <summary>
    /// 申请代理的状态
    /// </summary>
    public enum AgentRequestStateEnum
    {
        /// <summary>
        /// 不确定
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 申请中
        /// </summary>
        [MemberDescription("申请中")]
        Requesting,
        /// <summary>
        /// 已回复
        /// </summary>
        [MemberDescription("已回复")]
        Respond,
        /// <summary>
        /// 打回
        /// </summary>
        [MemberDescription("打回")]
        Reject
    }

    /// <summary>
    /// 申请的代理类型
    /// </summary>
    public enum AgentType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 普通
        /// </summary>
        [MemberDescription("普通")]
        Normal = 1,

        /// <summary>
        /// 第三方代理
        /// </summary>
        [MemberDescription("第三方代理")]
        ThirdParty = 2,

        /// <summary>
        /// 需要对收款有特殊要求的代理
        /// </summary>
        [MemberDescription("需要对收款有特殊要求的代理")]
        Special = 3
    }

    /// <summary>
    /// 表单类型(1:主提单,2:分提单,3:托运单,4:装箱单,5:账单,6:派车单,7:报关单)
    /// </summary>
    public enum FormType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 托运单
        /// </summary>
        Booking = 1,

        /// <summary>
        /// 装货单
        /// </summary>
        ShippingOrder = 2,

        /// <summary>
        /// 主提单
        /// </summary>
        MBL = 3,

        /// <summary>
        /// 分提单
        /// </summary>
        HBL = 4,

        /// <summary>
        /// 仓储单
        /// </summary>
        Warehouse = 5,

        /// <summary>
        /// 派车单
        /// </summary>
        Truck = 6,

        /// <summary>
        /// 报关单
        /// </summary>
        Customs = 7,

        /// <summary>
        /// Bill
        /// </summary>
        Bill = 8,

        /// <summary>
        /// 箱
        /// </summary>
        Container = 9,

        /// <summary>
        /// 放单
        /// </summary>
        ReleaseBL=10

    }
    /// <summary>
    /// 绑定类型
    /// </summary>
    public enum FCMBindType
    {
        /// <summary>
        /// 不指定，由LocalData.IsEnglish来确定
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 代码
        /// </summary>
        Code = 1,

        /// <summary>
        /// 英文名称
        /// </summary>
        EName = 2,

        /// <summary>
        /// 中文名称
        /// </summary>
        CName = 3,
    }

    /// <summary>
    /// 子列表类型
    /// </summary>
    public enum ShowingChildType
    {
        Memo, FaxEmail, Docment
    }
    /// <summary>
    /// 业务类型(货物装箱方式)
    /// </summary>
    public enum FCMOperationType
    {   
        Other=0,
        /// <summary>
        /// 整箱
        /// </summary>
        [MemberDescription("整箱", "FCL")]
        FCL = 1,

        /// <summary>
        /// 拼箱
        /// </summary>
        [MemberDescription("拼箱", "LCL")]
        LCL = 2,

        /// <summary>
        /// 散杂货
        /// </summary>
        [MemberDescription("散杂货", "BULK")]
        BULK = 3
    }
    /// <summary>
    /// 放单类型
    /// </summary>
    public enum FCMReleaseType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("不确定", "ReleaseType")]
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
        /// Sea way
        /// </summary>
        [MemberDescription("Sea way", "Sea way")]
        SeaWay = 3,
    }

    /// <summary>
    /// 委托方式
    /// </summary>
    public enum FCMBookingMode
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
    /// PO状态（1待处理、2已确认、3全部发货、4部分发货、5取消订单）
    /// </summary>
    public enum FCMPOState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("不确定", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 待处理
        /// </summary>
        [MemberDescription("待处理", "Processing")]
        Processing = 1,

        /// <summary>
        /// 已确认
        /// </summary>
        [MemberDescription("已确认", "Confirmed")]
        Confirmed = 2,

        /// <summary>
        /// 全部发货
        /// </summary>
        [MemberDescription("全部发货", "AllSent")]
        AllSent = 3,

        /// <summary>
        /// 部分发货
        /// </summary>
        [MemberDescription("部分发货", "Partial")]
        Partial = 4,

        /// <summary>
        /// 取消订单
        /// </summary>
        [MemberDescription("取消订单", "Cancel")]
        Cancel = 5
    }

    /// <summary>
    /// 提单类型（1:HBL，2:MBL）
    /// </summary>
    public enum FCMBLType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("Unknown")]
        Unknown = 0,

        /// <summary>
        /// MBL
        /// </summary>
        [MemberDescription("MBL")]
        MBL = 1,

        /// <summary>
        /// HBL
        /// </summary>
        [MemberDescription("HBL")]
        HBL = 2
    }
    #endregion

    #region 海进
    /// <summary>
    /// 海运进口订单状态
    /// </summary>
    public enum OIOrderState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("不确定", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 新订单
        /// </summary>
        [MemberDescription("新订单", "NewOrder")]
        NewOrder = 1,

        /// <summary>
        /// 已打回
        /// </summary>
        [MemberDescription("已打回", "Rejected")]
        Rejected = 2,

        /// <summary>
        /// 已审核
        /// </summary>
        [MemberDescription("已审核", "Checked")]
        Checked = 3,

        /// <summary>
        /// 已订舱
        /// </summary>
        [MemberDescription("已订舱", "BookingConfirmed")]
        BookingConfirmed = 4,

        /// <summary>
        /// 已装船
        /// </summary>
        [MemberDescription("已装船", "LoadVoyage")]
        LoadVoyage = 5,

        /// <summary>
        /// 已发送到港通知
        /// </summary>
        [MemberDescription("已发送到港通知", "PODNotify")]
        PODNotify = 6,

        /// <summary>
        /// 已放货
        /// </summary>
        [MemberDescription("已放货", "Release")]
        Release = 7,

        /// <summary>
        /// 已关单
        /// </summary>
        [MemberDescription("已关单", "Closed")]
        Closed = 8,
        /// <summary>
        /// 已提柜
        /// </summary>
        [MemberDescription("已提柜", "PickUp")]
        PickUp = 9,

        /// <summary>
        /// 已完成
        /// </summary>
        [MemberDescription("已完成", "Return")]
        Return = 10,
    }


    /// <summary>
    /// 下载时,海运提单状态
    /// </summary>
    public enum DownLoadBLState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 草稿
        /// </summary>
        [MemberDescription("草稿", "Draft")]
        Draft = 1,

        /// <summary>
        /// 对单中
        /// </summary>
        [MemberDescription("对单中", "Checking")]
        Checking = 2,

        /// <summary>
        /// 对单完成
        /// </summary>
        [MemberDescription("完成", "Checked")]
        Checked = 3,

        /// <summary>
        /// 已放单
        /// </summary>
        [MemberDescription("已放单", "Release")]
        Release = 4,

    }

    #endregion

    #region 海出

    /// <summary>
    /// 海出订单状态
    /// </summary>
    public enum OEOrderState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("不确定", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 待订舱
        /// </summary>
        [MemberDescription("新订单", "NewOrder")]
        NewOrder = 1,

        /// <summary>
        /// 已打回订单
        /// </summary>
        [MemberDescription("已打回", "Rejected")]
        Rejected = 2,


        /// <summary>
        /// 已审核
        /// </summary>
        [MemberDescription("已审核", "Checked")]
        Checked = 3,

        /// <summary>
        /// 已订舱
        /// </summary>
        [MemberDescription("已订舱", "BookingConfirmed")]
        BookingConfirmed = 4,

        /// <summary>
        /// 已装驳船
        /// </summary>
        [MemberDescription("已装驳船", "LoadPreVoyage")]
        LoadPreVoyage = 5,


        /// <summary>
        /// 已装大船
        /// </summary>
        [MemberDescription("已装大船", "LoadPreVoyage")]
        LoadVoyage = 6,

        /// <summary>
        /// 已关单
        /// </summary>
        [MemberDescription("已关单", "Closed")]
        Closed = 7,
      
    }

    /// <summary>
    /// 海出提单状态
    /// </summary>
    public enum OEBLState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("不确定", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 草稿
        /// </summary>
        [MemberDescription("草稿", "Draft")]
        Draft = 1,

        /// <summary>
        /// 对单中
        /// </summary>
        [MemberDescription("对单中", "Checking")]
        Checking = 2,

        /// <summary>
        /// 对单完成
        /// </summary>
        [MemberDescription("完成", "Checked")]
        Checked = 3,

        /// <summary>
        /// 已放单
        /// </summary>
        [MemberDescription("已放单", "Release")]
        Release = 4,
    }

    #endregion

    #region 空进
    /// <summary>
    /// 空进订单状态
    /// </summary>
    public enum AIOrderState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 新订单
        /// </summary>
        [MemberDescription("新订单", "NewOrder")]
        NewOrder = 1,

        /// <summary>
        /// 已打回
        /// </summary>
        [MemberDescription("已打回", "Rejected")]
        Rejected = 2,

        /// <summary>
        /// 已审核
        /// </summary>
        [MemberDescription("已审核", "Checked")]
        Checked = 3,

        /// <summary>
        /// 已发送到货通知
        /// </summary>
        [MemberDescription("已发送到货通知", "Arrival Notice Sended")]
        ArrivalNoticeSended = 4,

        /// <summary>
        /// 已放货
        /// </summary>
        [MemberDescription("已放货", "Release")]
        Release = 5,

        /// <summary>
        /// 已关单
        /// </summary>
        [MemberDescription("已关单", "Closed")]
        Closed = 6,


    }
    /// <summary>
    /// 空进提单状态
    /// </summary>
    public enum AIBLState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 草稿
        /// </summary>
        [MemberDescription("草稿", "Draft")]
        Draft = 1,

        /// <summary>
        /// 对单中
        /// </summary>
        [MemberDescription("对单中", "Checking")]
        Checking = 2,

        /// <summary>
        /// 对单完成
        /// </summary>
        [MemberDescription("完成", "Checked")]
        Checked = 3,

        /// <summary>
        /// 已放单
        /// </summary>
        [MemberDescription("已放单", "Release")]
        Release = 4,

    }



    #endregion

    #region 空出
    /// <summary>
    /// 空出订单状态
    /// </summary>
    public enum AEOrderState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 新订单
        /// </summary>
        [MemberDescription("新订单", "NewOrder")]
        NewOrder = 1,

        /// <summary>
        /// 已打回
        /// </summary>
        [MemberDescription("已打回", "Rejected")]
        Rejected = 2,

        /// <summary>
        /// 已审核
        /// </summary>
        [MemberDescription("已审核", "Checked")]
        Checked = 3,

        /// <summary>
        /// 已订舱
        /// </summary>
        [MemberDescription("已订舱", "BookingConfirmed")]
        BookingConfirmed = 4,

        /// <summary>
        /// 已确认MBL
        /// </summary>
        [MemberDescription("已确认MBL", "Confirmed")]
        Confirmed = 5,

        /// <summary>
        /// 已关单
        /// </summary>
        [MemberDescription("已关单", "Closed")]
        Closed = 6,

    }

    /// <summary>
    /// 空出提单状态
    /// </summary>
    public enum AEBLState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 草稿
        /// </summary>
        [MemberDescription("草稿")]
        Draft = 1,

        /// <summary>
        /// 对单中
        /// </summary>
        [MemberDescription("对单中")]
        Checking = 2,

        /// <summary>
        /// 对单完成
        /// </summary>
        [MemberDescription("完成")]
        Checked = 3,

        /// <summary>
        /// 已放单
        /// </summary>
        [MemberDescription("已放单")]
        Release = 4,
    }

    #endregion

    #region 其他业务
    /// <summary>
    /// 其他业务订单状态
    /// </summary>
    public enum OBOrderState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 新订单
        /// </summary>
        [MemberDescription("新订单")]
        NewOrder = 1,

        ///// <summary>
        ///// 已打回订单
        ///// </summary>
        //[MemberDescription("已打回")]
        //Rejected = 2,


        ///// <summary>
        ///// 已审核
        ///// </summary>
        //[MemberDescription("已审核")]
        //Checked = 3,

        ///// <summary>
        ///// 已订舱
        ///// </summary>
        //[MemberDescription("已订舱")]
        //BookingConfirmed = 4,

        /// <summary>
        /// 已确认
        /// </summary>
        [MemberDescription("已确认")]
        MBLConfirmed = 2,

        /// <summary>
        /// 已关单
        /// </summary>
        [MemberDescription("已关单")]
        Closed = 3
    }


    /// <summary>
    /// 其他业务提单状态
    /// </summary>
    public enum OBBLState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 草稿
        /// </summary>
        [MemberDescription("草稿")]
        Draft = 1,

        /// <summary>
        /// 对单中
        /// </summary>
        [MemberDescription("对单中")]
        Checking = 2,

        /// <summary>
        /// 对单完成
        /// </summary>
        [MemberDescription("完成")]
        Checked = 3,

        /// <summary>
        /// 已放单
        /// </summary>
        [MemberDescription("已放单")]
        Release = 4,
    }
    #endregion

    #region 内贸

    /// <summary>
    /// 内贸订单状态
    /// </summary>
    public enum DTOrderState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 待订舱
        /// </summary>
        [MemberDescription("新订单")]
        NewOrder = 1,

        /// <summary>
        /// 已打回订单
        /// </summary>
        [MemberDescription("已打回")]
        Rejected = 2,


        /// <summary>
        /// 已审核
        /// </summary>
        [MemberDescription("已审核")]
        Checked = 3,

        /// <summary>
        /// 已订舱
        /// </summary>
        [MemberDescription("已订舱")]
        BookingConfirmed = 4,

        /// <summary>
        /// 已装驳船
        /// </summary>
        [MemberDescription("已装驳船")]
        LoadPreVoyage = 5,


        /// <summary>
        /// 已装大船
        /// </summary>
        [MemberDescription("已装大船")]
        LoadVoyage = 6,

        /// <summary>
        /// 已关单
        /// </summary>
        [MemberDescription("已关单")]
        Closed = 7,
    }


    /// <summary>
    /// 内贸提单状态(1草稿,对单中,对单完成,完成)
    /// </summary>
    public enum DTBLState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 草稿
        /// </summary>
        [MemberDescription("草稿")]
        Draft = 1,

        /// <summary>
        /// 对单中
        /// </summary>
        [MemberDescription("对单中")]
        Checking = 2,

        /// <summary>
        /// 对单完成
        /// </summary>
        [MemberDescription("完成")]
        Checked = 3,

        /// <summary>
        /// 已放单
        /// </summary>
        [MemberDescription("已放单")]
        Release = 4,
    }
    #endregion


}
