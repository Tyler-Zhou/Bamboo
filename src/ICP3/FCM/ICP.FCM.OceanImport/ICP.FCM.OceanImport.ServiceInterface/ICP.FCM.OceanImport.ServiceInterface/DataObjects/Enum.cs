//-----------------------------------------------------------------------
// <copyright file="Enum.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.OceanImport.ServiceInterface
{
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

        ///// <summary>
        ///// 装货号
        ///// </summary>
        //[MemberDescription("装货号", "SO No.")]
        //SONo = 2,

        /// <summary>
        /// 提单号
        /// </summary>
        [MemberDescription("提单号", "BL No")]
        BLNo =3,

        /// <summary>
        /// 箱号
        /// </summary>
        [MemberDescription("箱号", "Container No")]
        ContainerNo = 4,

        ///// <summary>
        ///// 账单号
        ///// </summary>
        //[MemberDescription("账单号", "BillNo")]
        //BillNo = 5,

        ///// <summary>
        ///// 合约号
        ///// </summary>
        //[MemberDescription("合约号", "ContractNo")]
        //ContractNo = 6,

        /// <summary>
        /// 转关号
        /// </summary>
        [MemberDescription("转关号", "I.T.No")]
        ITNo = 7
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
        /// 船公司 
        /// </summary>
        [MemberDescription("船公司", "Carrier")]
        Carrier = 2,

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
        Agent = 8,

        /// <summary>
        /// 仓库
        /// </summary>
        [MemberDescription("仓库", "WareHouse")]
        WareHouse = 9,

        ///// <summary>
        ///// 拖车行
        ///// </summary>
        //[MemberDescription("拖车行", "Trucker")]
        //Trucker = 10,

        /// <summary>
        /// 报关行
        /// </summary>
        [MemberDescription("报关行", "CustomsBroker")]
        CustomsBroker = 11

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
        /// 收货地
        /// </summary>
        [MemberDescription("收货地", "Place Of Receipt")]
        PlaceOfReceipt=1,

        /// <summary>
        /// 装货港
        /// </summary>
        [MemberDescription("装货港", "POL")]
        POL = 2,

        //不需要 3、中转港

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
        FinalDestination = 6,

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
        /// 交货地日
        /// </summary>
        [MemberDescription("交货地日", "D.ETA")]
        DETA = 5,

        /// <summary>
        /// 最终目的地日
        /// </summary>
        [MemberDescription("最终目的地日", "F.ETA")]
        FETA = 6,

        /// <summary>
        /// 放货日
        /// </summary>
        [MemberDescription("放货日", "ReleaseDate")]
        ReleaseDate = 7,

        /// <summary>
        /// 免堆日
        /// </summary>
        [MemberDescription("免堆日", "Last Free Date")]
        LastFreeDate = 8,
        
        /// <summary>
        /// 放单日期
        /// </summary>
        [MemberDescription("放单日期", "Release BL Date")]
        ReleaseBLDate = 9

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
    /// 下载出口提单的下载状态
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


    /// <summary>
    /// 日期类型
    /// </summary>
    public enum DateType
    {
        /// <summary>
        /// 离港日
        /// </summary>
        [MemberDescription("离港日", "ETD")]
        ETD = 1,

        /// <summary>
        /// 到港日
        /// </summary>
        [MemberDescription("到港日", "ETA")]
        ETA = 2,
    }

    /// <summary>
    /// 放单状态
    /// </summary>
    public enum ReleaseCarogoState
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部", "ALL")]
        Unknown = 0,

        /// <summary>
        /// 已放单
        /// </summary>
        [MemberDescription("已放单", "RBLD")]
        RBLD = 1,
    }


}
