//-----------------------------------------------------------------------
// <copyright file="Enum.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using ICP.Framework.CommonLibrary.Attributes;
using System;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    #region 公共
    #region 订单状态
    /// <summary>
    /// 订单状态
    /// </summary>
    [Flags]
    [Serializable]
    public enum OrderState
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
        /// 取消订舱
        /// </summary>
        [MemberDescription("取消订舱", "Cancel Booking")]
        CancelBooking = 5,

        /// <summary>
        /// 已装驳船
        /// </summary>
        [MemberDescription("已装驳船", "LoadPreVoyage")]
        LoadPreVoyage = 6,

        /// <summary>
        /// 已装大船
        /// </summary>
        [MemberDescription("已装大船", "LoadVoyage")]
        LoadVoyage = 7,

        /// <summary>
        /// 已放货
        /// </summary>
        [MemberDescription("已放货", "Release")]
        Release = 8,

        /// <summary>
        /// 已关单
        /// </summary>
        [MemberDescription("已关单", "Closed")]
        Closed = 9,

        /// <summary>
        /// 已提柜
        /// </summary>
        [MemberDescription("已提柜", "PickUp")]
        PickUp = 10,

        /// <summary>
        /// 已完成
        /// </summary>
        [MemberDescription("已完成", "Return")]
        Return = 11,
    } 
    #endregion

    #region 海运业务角色类型
    /// <summary>
    /// 海运业务角色类型
    /// </summary>
    [Flags]
    [Serializable]
    public enum RoleType
    {
        /// <summary>
        /// 业务员
        /// </summary>
        [MemberDescription("业务员", "Sales")]
        Sales = 1,
        /// <summary>
        /// 订舱员
        /// </summary>
        [MemberDescription("订舱员", "SO By")]
        SOBy,
        /// <summary>
        /// 文件员
        /// </summary>
        [MemberDescription("文件员", "Doc By")]
        DocBy,
        /// <summary>
        /// 海外部客服
        /// </summary>
        [MemberDescription("海外部客服", "Overseas Customer Service")]
        OverseasCS,
        /// <summary>
        /// 港后客服
        /// </summary>
        [MemberDescription("港后客服", "Agent Customer Service")]
        AgentCS,
        /// <summary>
        /// 放单员
        /// </summary>
        [MemberDescription("放单员", "Release By")]
        ReleaseBy,
        /// <summary>
        /// 放货员
        /// </summary>
        [MemberDescription("放货员", "Release Cargo By")]
        RCBy,

    } 
    #endregion

    #region 日期搜索类型
    /// <summary>
    /// 日期搜索类型
    /// </summary>
    [Flags]
    [Serializable]
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
    #endregion

    #region 付款方式
    /// <summary>
    /// 付款方式
    /// </summary>
    [Serializable]
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
    #endregion

    #region 单证类型
    /// <summary>
    /// 单证类型
    /// </summary>
    [Serializable]
    public enum FCMDocumentType
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
    #endregion

    #region 文档交接方式
    /// <summary>
    /// 文档交接方式
    /// </summary>
    [Serializable]
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
    #endregion

    #region 邮件重要性
    /// <summary>
    /// 邮件重要性（0普通、1高、2低）
    /// </summary>
    [Serializable]
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
    #endregion

    #region 货物类型
    /// <summary>
    /// 货物类型
    /// </summary>
    [Serializable]
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
    #endregion

    #region 申请代理的状态
    /// <summary>
    /// 申请代理的状态
    /// </summary>
    [Serializable]
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
    #endregion

    #region 申请的代理类型
    /// <summary>
    /// 申请的代理类型
    /// </summary>
    [Serializable]
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
    #endregion

    #region 绑定类型
    /// <summary>
    /// 绑定类型
    /// </summary>
    [Serializable]
    public enum FCMBindType
    {
        /// <summary>
        /// 不指定，由LocalData.IsEnglish来确定
        /// </summary>
        [MemberDescription("不确定", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 代码
        /// </summary>
        [MemberDescription("代码", "Code")]
        Code = 1,

        /// <summary>
        /// 英文名称
        /// </summary>
        [MemberDescription("英文名称", "English Name")]
        EName = 2,

        /// <summary>
        /// 中文名称
        /// </summary>
        [MemberDescription("中文名称", "Chinese Name")]
        CName = 3,
    } 
    #endregion

    #region 子列表类型
    /// <summary>
    /// 子列表类型
    /// </summary>
    [Serializable]
    public enum ShowingChildType
    {
        /// <summary>
        /// 
        /// </summary>
        [MemberDescription("备忘录", "Memo")]
        Memo,
        /// <summary>
        /// 
        /// </summary>
        [MemberDescription("传真/邮件", "Fax / Email")]
        FaxEmail,
        /// <summary>
        /// 
        /// </summary>
        [MemberDescription("文档", "Docment")]
        Docment
    } 
    #endregion

    #region 货物装箱方式
    /// <summary>
    /// 业务类型(货物装箱方式)
    /// </summary>
    [Serializable]
    public enum FCMOperationType
    {
        /// <summary>
        /// 其他
        /// </summary>
        [MemberDescription("其他", "Other")]
        Other = 0,
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
    #endregion

    #region 放单类型
    /// <summary>
    /// 放单类型
    /// </summary>
    [Serializable]
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
    #endregion

    #region 放单类型
    /// <summary>
    /// 放单类型
    /// </summary>
    [Serializable]
    public enum FCMReleaseState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("", "")]
        Unknown = 0,

        /// <summary>
        /// 已创建
        /// </summary>
        [MemberDescription("未放单", "Hold")]
        Created = 1,

        /// <summary>
        /// 已放单
        /// </summary>
        [MemberDescription("已放单", "Release BL")]
        Released = 3,

        /// <summary>
        /// 已接收
        /// </summary>
        [MemberDescription("已接收", "Rcv RBL")]
        Received = 4,

        /// <summary>
        /// 已放货
        /// </summary>
        [MemberDescription("已放货", "Release Cargo")]
        RC = 5,
    } 
    #endregion

    #region 委托方式
    /// <summary>
    /// 委托方式
    /// </summary>
    [Serializable]
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
        Internet = 3,
        /// <summary>
        /// CSP
        /// </summary>
        [MemberDescription("CSP下载", "CSP Download")]
        CSP = 4,
    } 
    #endregion

    #region PO状态
    /// <summary>
    /// PO状态（1待处理、2已确认、3全部发货、4部分发货、5取消订单）
    /// </summary>
    [Serializable]
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
    #endregion

    #region 提单类型
    /// <summary>
    /// 提单类型（1:HBL，2:MBL,3:Declare HBL）
    /// </summary>
    [Serializable]
    public enum FCMBLType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("Unknown", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// MBL
        /// </summary>
        [MemberDescription("MBL", "MBL")]
        MBL = 1,

        /// <summary>
        /// HBL
        /// </summary>
        [MemberDescription("HBL", "HBL")]
        HBL = 2,
        /// <summary>
        /// Declare HBL
        /// </summary>
        [MemberDescription("预配分提单", "DeclareHBL")]
        DeclareHBL = 3,
    } 
    #endregion

    #region 海进业务/海出提单列表左边查询
    /// <summary>
    /// 放单（1:已放单，2.未放单）
    /// </summary>
    [Serializable]
    public enum ReleaseBLSearchStatue
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 已放单
        /// </summary>
        ReleaseBL = 1,
        /// <summary>
        /// 未放单
        /// </summary>
        UnReleaseBL = 2
    }
    /// <summary>
    /// 放货（1:已放货，2.未放货）
    /// </summary>
    [Serializable]
    public enum ReleaseRCSearchStatue
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 已放货
        /// </summary>
        ReleaseRC = 1,
        /// <summary>
        /// 未放货
        /// </summary>
        UnReleaseRC = 2
    }
    /// <summary>
    /// 申请放单（1:已申请放单，2.未申请放单）
    /// </summary>
    [Serializable]
    public enum ApplyReleaseSearchStatue
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 已申请放单
        /// </summary>
        ApplyRelease = 1,
        /// <summary>
        /// 未申请放单
        /// </summary>
        UnApplyRelease = 2
    }
    /// <summary>
    /// 签收（1:已签收，2.未签收）
    /// </summary>
    [Serializable]
    public enum ReceiveRCSearchStatue
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 已签收
        /// </summary>
        ReceiveRC = 1,
        /// <summary>
        /// 未签收
        /// </summary>
        UnReceiveRC = 2
    }

    /// <summary>
    /// 申请放货
    /// </summary>
    [Serializable]
    public enum ApplyRCSearchStatue
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 已申请放货
        /// </summary>
        ApplyRC = 1,
        /// <summary>
        /// 未申请放货
        /// </summary>
        UnApplyRC = 2
    }
    /// <summary>
    /// 催港前放单
    /// </summary>
    [Serializable]
    public enum URBSearchStatue
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 已催单
        /// </summary>
        URB = 1,
        /// <summary>
        /// 未催单
        /// </summary>
        UnURB = 2
    }
    /// <summary>
    /// 催客户付款
    /// </summary>
    [Serializable]
    public enum UDNSearchStatue
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 已催款
        /// </summary>
        UDN = 1,
        /// <summary>
        /// 未催款
        /// </summary>
        UnUDN = 2
    }

    /// <summary>
    /// 同意放货
    /// </summary>
    [Serializable]
    public enum ARCSearchStatue
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 已催款
        /// </summary>
        ARC = 1,
        /// <summary>
        /// 未催款
        /// </summary>
        UnARC = 2
    }
    /// <summary>
    /// 海出查询方案
    /// </summary>
    [Serializable]
    public enum ReleaseProgram
    {
        /// <summary>
        /// 自定义
        /// </summary>
        [MemberDescription("自定义", "Custom")]
        Custom = 0,

        /// <summary>
        /// 未申请放单
        /// </summary>
        [MemberDescription("未申请放单", "Not-Aplied Release BL")]
        NotApliedReleaseBL = 1,

        /// <summary>
        /// 已申请放单
        /// </summary>
        [MemberDescription("已申请放单", "Applied Release BL")]
        AppliedReleaseBL = 2,

        /// <summary>
        /// 未下达放单指令
        /// </summary>
        [MemberDescription("未下达放单指令", "Not-Released BL")]
        NotReleasedBL = 3,

        /// <summary>
        /// 已下达放单指令
        /// </summary>
        [MemberDescription("已下达放单指令", "Released BL")]
        ReleasedBL = 4,

        /// <summary>
        /// 已签收放单指令
        /// </summary>
        [MemberDescription("已签收放单指令", "Accepted Release BL")]
        AcceptedReleaseBL = 5,

        /// <summary>
        /// 已放货
        /// </summary>
        [MemberDescription("已放货", "Released Cargo")]
        ReleasedCargo = 6,

        /// <summary>
        /// 已收到正本
        /// </summary>
        [MemberDescription("已收到正本", "Received OB/L")]
        ReleasedOBL = 7,

    }

    /// <summary>
    /// 海进查询方案
    /// </summary>
    [Serializable]
    public enum ReleaseCargoProgram
    {
        /// <summary>
        /// 自定义
        /// </summary>
        [MemberDescription("自定义", "Custom")]
        Custom = 0,

        /// <summary>
        /// 未下达放单指令
        /// </summary>
        [MemberDescription("未下达放单指令", "Not-Released BL")]
        NotReleasedBL = 1,

        /// <summary>
        /// 已下达放单指令
        /// </summary>
        [MemberDescription("已下达放单指令", "Released BL")]
        ReleasedBL = 2,

        /// <summary>
        /// 已签收放单指令
        /// </summary>
        [MemberDescription("已签收放单指令", "Accepted Release BL")]
        AcceptedReleaseBL = 3,

        /// <summary>
        /// 未申请放单
        /// </summary>
        [MemberDescription("未申请放货", "Not-Aplied Release Cargo")]
        NotApliedReleaseCargo = 4,

        /// <summary>
        /// 已申请放单
        /// </summary>
        [MemberDescription("已申请放货", "Applied Release Cargo")]
        AppliedReleaseCargo = 5,

        /// <summary>
        /// 已放货
        /// </summary>
        [MemberDescription("已放货", "Released Cargo")]
        ReleasedCargo = 6,

    }

    #endregion

    #endregion

    #region 海进
    #region 海运进口订单状态
    /// <summary>
    /// 海运进口订单状态
    /// </summary>
    [Serializable]
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
    #endregion

    #region 海运提单状态
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

    #endregion

    #region 海出

    #region 海出订单状态
    /// <summary>
    /// 海出订单状态
    /// </summary>
    [Serializable]
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
        [MemberDescription("已装大船", "LoadVoyage")]
        LoadVoyage = 6,
        /// <summary>
        /// 已关单
        /// </summary>
        [MemberDescription("已关单", "Closed")]
        Closed = 7,
        /// <summary>
        /// 取消订舱
        /// </summary>
        [MemberDescription("取消订舱", "Cancel Booking")]
        CancelBooking = 8,
    } 
    #endregion

    /// <summary>
    /// 海出提单状态
    /// </summary>
    [Serializable]
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
    /// <summary>
    /// 装船状态
    /// </summary>
    [Serializable]
    public enum OEApplication
    {
        /// <summary>
        /// 未装船
        /// </summary>
        [MemberDescription("未装船", "Not")]
        Not = 1,
        /// <summary>
        /// 已装驳船
        /// </summary>
        [MemberDescription("已装驳船", "Feeder")]
        Partially = 2,
        /// <summary>
        /// 已装大船
        /// </summary>
        [MemberDescription("已装大船", "Monther")]
        Complete = 3,

    }
    /// <summary>
    /// 完成状态 
    /// </summary>
    [Serializable]
    public enum OECompletionStatus
    {
        /// <summary>
        /// 未完成
        /// </summary>
        [MemberDescription("未完成", "Not")]
        Not = 1,
        /// <summary>
        /// 完成部分
        /// </summary>
        [MemberDescription("部分完成", "Partially Completed")]
        Partially = 2,
        /// <summary>
        /// 已完成
        /// </summary>
        [MemberDescription("已完成", "Complete")]
        Complete = 3,
    }

    #endregion

    #region 空进
    /// <summary>
    /// 空进订单状态
    /// </summary>
    [Serializable]
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
    [Serializable]
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
    [Serializable]
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
    [Serializable]
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
    [Serializable]
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
        Closed = 3,


        /// <summary>
        /// 已放单
        /// </summary>
        [MemberDescription("已放单")]
        RBLD = 4

    }


    /// <summary>
    /// 其他业务提单状态
    /// </summary>
    [Serializable]
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
    [Serializable]
    public enum DTOrderState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("未知", "Unknown")]
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
        [MemberDescription("已订舱", "Booking Confirmed")]
        BookingConfirmed = 4,
        /// <summary>
        /// 已装驳船
        /// </summary>
        [MemberDescription("已装驳船", "Load Pre Voyage")]
        LoadPreVoyage = 5,
        /// <summary>
        /// 已装大船
        /// </summary>
        [MemberDescription("已装大船", "Load Voyage")]
        LoadVoyage = 6,
        /// <summary>
        /// 已关单
        /// </summary>
        [MemberDescription("已关单", "Closed")]
        Closed = 7,
    }


    /// <summary>
    /// 内贸提单状态(1草稿,对单中,对单完成,完成)
    /// </summary>
    [Serializable]
    public enum DTBLState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("未知", "Unknown")]
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

    #region 运价单号搜索类型
    /// <summary>
    /// 单号搜索类型
    /// </summary>
    [Serializable]
    public enum QPNoSearchType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部单号", "All No")]
        All = 0,
        /// <summary>
        /// 报价单号
        /// </summary>
        [MemberDescription("报价单号", "Quoted Price No.")]
        QuotedPriceNo = 1,
    } 
    #endregion

    #region 港口搜索类型
    /// <summary>
    /// 港口搜索类型
    /// </summary>
    [Serializable]
    public enum QPPortSearchType
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
        PlaceOfDelivery = 5
    } 
    #endregion

    #region 日期搜索类型
    /// <summary>
    /// 日期搜索类型
    /// </summary>
    [Serializable]
    public enum QPDateSearchType
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
        CreateDate = 1
    } 
    #endregion

    #region 空运运单类型
    /// <summary>
    /// 空运运单(Airway Bill)类型
    /// </summary>
    [Flags]
    [Serializable]
    public enum AWBType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 航空主运单号(MASTER AIRWAY BILL)
        /// </summary>
        [MemberDescription("主运单", "MAWB")]
        MAWB = 1,

        /// <summary>
        /// 航空分运单号(HOUSE AIRWAY BILL)
        /// </summary>
        [MemberDescription("分运单", "HAWB")]
        HAWB = 2
    }
    #endregion
}
