//-----------------------------------------------------------------------
// <copyright file="Enum.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.AirImport.ServiceInterface
{
    using ICP.Framework;
    using ICP.Framework.CommonLibrary.Attributes;


    public enum DateValueType
    {
        [MemberDescription("不确定", "Unkown")]
        Unkown = 0,

        [MemberDescription("一周内", "One Week")]
        OneWeek = 1,

        [MemberDescription("一月内", "One Month")]
        OneMonth = 2,

        [MemberDescription("一年内", "One Year")]
        OneYear = 3
    }

    /// <summary>
    /// 提单类型（0:HBL，1:MBL）
    /// </summary>
    public enum FCMBLType
    {
        /// <summary>
        /// HBL
        /// </summary>
        HBL = 0,

        /// <summary>
        /// MBL
        /// </summary>
        MBL = 1
    }

    #region 进口业务查询
    /// <summary>
    /// 进口业务单号查询
    /// </summary>
    public enum OIBusinessNoSearchType
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
        /// 航班号
        /// </summary>
        [MemberDescription("航班号", "Flight No.")]
        FlightNo = 2,

        /// <summary>
        /// 提单号
        /// </summary>
        [MemberDescription("提单号", "BL No.")]
        BL = 3,

        ///// <summary>
        ///// 帐单号
        ///// </summary>
        //[MemberDescription("帐单号", "Bill No.")]
        //Bill = 4,

        ///// <summary>
        ///// 合约号
        ///// </summary>
        //[MemberDescription("合约号", "SC No.")]
        //Contract = 5

        /// <summary>
        /// 转关号
        /// </summary>
        [MemberDescription("转关号", "I.T.No")]
        ITNo = 4
    }

    /// <summary>
    /// 进口业务客户查询
    /// </summary>
    public enum OIBusinessCustomerSearchType
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
        /// 承运人
        /// </summary>
        [MemberDescription("承运人", "AgentOfCarrier")]
        AgentOfCarrier = 3,

        /// <summary>
        /// 发货人
        /// </summary>
        [MemberDescription("发货人", "Shipper")]
        Shipper = 4,

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
        /// 代理
        /// </summary>
        [MemberDescription("代理", "Proxy")]
        Agent = 7,

        /// <summary>
        /// 仓库
        /// </summary>
        [MemberDescription("仓库", "WareHouse")]
        WareHouse = 8,

        ///// <summary>
        ///// 拖车行
        ///// </summary>
        //[MemberDescription("拖车行", "Trucker")]
        //Trucker = 10,

        /// <summary>
        /// 报关行
        /// </summary>
        [MemberDescription("报关行", "CustomsBroker")]
        CustomsBroker = 9
    }


    /// <summary>
    /// 进口业务港口查询
    /// </summary>
    public enum OIBusinessPortSearchType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部地点", "All Port")]
        All = 0,     

        /// <summary>
        /// 起运港
        /// </summary>
        [MemberDescription("起运港", "Departure")]
        Departure = 1,

        /// <summary>
        /// 目的港
        /// </summary>
        [MemberDescription("目的港", "Detination")]
        Detination = 2,

        /// <summary>
        /// 交货地
        /// </summary>
        [MemberDescription("交货地", "Place Of Delivery")]
        PlaceOfDelivery = 3
    }


    /// <summary>
    /// 进口业务管理日期搜索类型
    /// </summary>
    public enum OIBusinessDateSearchType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部日期", "All Date")]
        All = 0,

        /// <summary>
        /// 创建日
        /// </summary>
        [MemberDescription("创建日期", "Create Date")]
        CreateDate = 1,

        //不需要2订舱日

        /// <summary>
        /// 起航日
        /// </summary>
        [MemberDescription("起航日", "ETD")]
        ETD = 2,

        /// <summary>
        /// 到达日
        /// </summary>
        [MemberDescription("到达日", "ETA")]
        ETA = 3,

        /// <summary>
        /// 交货地日
        /// </summary>
        [MemberDescription("到交货地日", "D.ETA")]
        DETA = 4,

        /// <summary>
        /// 放货日
        /// </summary>
        [MemberDescription("放货日", "ReleaseDate")]
        ReleaseDate = 5
    }

    /// <summary>
    /// 进口业务时间内容查询
    /// </summary>
    public enum OIBusinessDateValueSearchType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("不确定", "UnKnow")]
        UnKnow = 0,

        /// <summary>
        /// 一周肉
        /// </summary>
        [MemberDescription("一周内", "Last Week")]
        LastWeek = 1,

        /// <summary>
        /// 一月内
        /// </summary>
        [MemberDescription("一月内", "This Month")]
        ThisMonth = 2,

        /// <summary>
        /// 一年内
        /// </summary>
        [MemberDescription("一年内", "Last Year")]
        LastYear = 3,

    }

    #endregion



    /// <summary>
    /// 下载出口提单状态
    /// </summary>
    public enum DownLoadState
    {
        /// <summary>
        /// 不确定
        /// </summary>
        Unknown = 0,

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





    public enum DateType
    {
        /// <summary>
        /// 离港日
        /// </summary>
        [MemberDescription("离港日")]
        ETD = 1,

        /// <summary>
        /// 到港日
        /// </summary>
        [MemberDescription("到港日")]
        ETA = 2,
    }


}
