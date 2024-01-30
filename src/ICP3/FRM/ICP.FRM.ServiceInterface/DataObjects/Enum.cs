using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.FRM.ServiceInterface.DataObjects
{
    /// <summary>
    ///合约类型(1:BCO直客约,2:NVOCC,3:全部)
    /// </summary>
    public enum ContractType
    {
       
        /// <summary>
        /// BCO直客约
        /// </summary>
        BCO = 1,

        /// <summary>
        /// 同行约
        /// </summary>
        NVOCC = 2,

         /// <summary>
        /// 全部
        /// </summary>
        BOTH =3,
    }

    /// <summary>
    /// 运价类型(1:Contract,2:Market,3:LCL(暂未用到),4:SellingRate,5:CA TO DOOR)
    /// </summary>
    public enum RateType
    {
        /// <summary>
        /// Contract
        /// </summary>
        [MemberDescription("Contract", "Contract")]
        Contract = 1,

        /// <summary>
        /// Market
        /// </summary>
        [MemberDescription("Market", "Market")]
        Market = 2,

        /// <summary>
        /// SellingRate
        /// </summary>
        [MemberDescription("Selling Rate", "Selling Rate")]
        SellingRate=4,

        /// <summary>
        /// CA TO DOOR
       
        /// </summary>
        [MemberDescription("CA TO DOOR", "CA TO DOOR")]
        CATODOOR = 5,
    }

    /// <summary>
    /// 合约状态 (1,Draft2.Published,3.Invalidated,4.Expired
    /// </summary>
    public enum OceanState
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [MemberDescription("Draft", "Draft")]
        Draft = 1,

        /// <summary>
        /// 已经发布
        /// </summary>
        [MemberDescription("Published", "Published")]
        Published = 2,

        /// <summary>
        /// 无效
        /// </summary>
        [MemberDescription("Invalidated", "Invalidated")]
        Invalidated = 3,

        /// <summary>
        /// 已过期
        /// </summary>
        [MemberDescription("Expired", "Expired")]
        Expired = 4
    }

    /// <summary>
    /// Arbitrary类型 (1,Original 2 Destination
    /// </summary>
    public enum GeographyType
    {
        /// <summary>
        /// None
        /// </summary>
        [System.Xml.Serialization.XmlEnum(Name = "0")]
        [MemberDescription("", "")]
        None= 0,
        /// <summary>
        /// 起始地
        /// </summary>
        [System.Xml.Serialization.XmlEnum(Name = "1")]
        [MemberDescription("Original", "Original")]
        Original=1,
        /// <summary>
        /// 目的地
        /// </summary>
        [System.Xml.Serialization.XmlEnum(Name = "2")]
        [MemberDescription("Destination", "Destination")]
        Destination=2,
    }
    /// <summary>
    /// Arbitrary运输方式
    /// </summary>
    public enum ModeOfTransport
    {
        /// <summary>
        /// None
        /// </summary>
        [System.Xml.Serialization.XmlEnum(Name = "0")]
        [MemberDescription("", "")]
        None = 0,
        /// <summary>
        /// 驳船
        /// </summary>
        [System.Xml.Serialization.XmlEnum(Name = "1")]
        [MemberDescription("Feeder", "Feeder")]
        Feeder = 1,
        /// <summary>
        /// 拖车
        /// </summary>
        [System.Xml.Serialization.XmlEnum(Name = "2")]
        [MemberDescription("Truck", "Truck")]
        Truck = 2,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// None
        /// </summary>
        [System.Xml.Serialization.XmlEnum(Name = "0")]
        [MemberDescription("", "")]
        None = 0,

        /// <summary>
        /// 发货人
        /// </summary>
        [System.Xml.Serialization.XmlEnum(Name="1")]
        [MemberDescription("Shipper", "Shipper")]
        Shipper = 1,

        /// <summary>
        /// 收货人
        /// </summary>
        [System.Xml.Serialization.XmlEnum(Name = "2")]
        [MemberDescription("Consignee", "Consignee")]
        Consignee = 2,
    }

    /// <summary>
    /// 运价状态
    /// </summary>
    public enum OceanItemState
    {
        /// <summary>
        /// 有效
        /// </summary>
        [System.Xml.Serialization.XmlEnum(Name = "1")]
        Valid = 1,

        /// <summary>
        /// 无效
        /// </summary>
        [System.Xml.Serialization.XmlEnum(Name = "1")]
        Invalid = 2,

        ///// <summary>
        ///// 已经删除
        ///// </summary>
        //[System.Xml.Serialization.XmlEnum(Name = "1")]
        //Deleted = 2
    }

    /// <summary>
    /// OceanPermission
    /// </summary>
    public enum OceanPermission
    {
        None=0,
        /// <summary>
        /// 查看
        /// </summary>
        View = 1,

        /// <summary>
        /// 修改
        /// </summary>
        Edit = 3,

        /// <summary>
        /// 完全控制
        /// </summary>
        Manager = 7
    }

    /// <summary>
    /// OceanPermissionMode
    /// </summary>
    public enum OceanPermissionMode
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// General
        /// </summary>
        General = 1,
        /// <summary>
        /// Customize
        /// </summary>
        Customize = 2,
        /// <summary>
        /// Exclude
        /// </summary>
        Exclude = 3

    }


    /// <summary>
    /// 使用对象（对应岗位或用户）
    /// </summary>
    public enum UserObjectType
    {
        /// <summary>
        /// 岗位
        /// </summary>
        Job = 1,

        /// <summary>
        /// 用户
        /// </summary>
        User = 2
    }
    /// <summary>
    /// 窗体类型
    /// </summary>
    public enum FormType
    { 
        /// <summary>
        /// 列表
        /// </summary>
        List=1,
        /// <summary>
        /// 编辑界面
        /// </summary>
        Edit=2
    }
    /// <summary>
    /// 港口类型
    /// </summary>
    public enum PortType
    {
        /// <summary>
        /// POL
        /// </summary>
        POL = 1,

        /// <summary>
        /// POD
        /// </summary>
        POD = 2,

        /// <summary>
        /// Delivery
        /// </summary>
        Delivery = 3,

        /// <summary>
        /// FinalDestination
        /// </summary>
        FinalDestination=4
    }

    /// <summary>
    /// 查询运价状态
    /// </summary>
    public enum SearchPriceStatus
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("All", "All")]
        All = 0,

        /// <summary>
        /// 有效的
        /// </summary>
        [MemberDescription("EFFECTIVE", "EFFECTIVE")]
        EFFECTIVE = 1,

        /// <summary>
        /// 未开始使用的
        /// </summary>
        [MemberDescription("WILL BE EFFECTIVE", "WILL BE EFFECTIVE")]
        WILLBEEFFECTIVE = 2,

        /// <summary>
        /// 无效的
        /// </summary>
        [MemberDescription("EXPIRED", "EXPIRED")]
        EXPIRED = 3

    }

    /// <summary>
    /// 查询运价类型
    /// </summary>
    public enum SearchRateType
    {

        /// <summary>
        /// 全部
        /// </summary>
        All=0,
        /// <summary>
        /// Contract
        /// </summary>
        Contract = 1,

        /// <summary>
        /// Market
        /// </summary>
        Market = 2,

        /// <summary>
        /// LCL
        /// </summary>
        LCL=3,

        /// <summary>
        /// Selling
        /// </summary>
        Selling=4,

        /// <summary>
        /// SearchRateType
        /// </summary>
        CATODOOR= 5,

        /// <summary>
        /// Inquiry
        /// </summary>
        Inquiry=7

    }
    /// <summary>
    /// 海运运价类型，查询
    /// </summary>
    public enum OceanTypeBySearch
    {

        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("All", "All")]
        All = 0,
        /// <summary>
        /// Contract
        /// </summary>
        [MemberDescription("Cost", "Cost")]
        Cost = 1,

        /// <summary>
        /// Selling
        /// </summary>
        [MemberDescription("Selling", "Selling")]
        Selling = 2,

        /// <summary>
        /// Inquiry
        /// </summary>
        [MemberDescription("Inquiry", "Inquiry")]
        Inquiry = 3

    }
    /// <summary>
    /// 面板显示/关闭  类型
    /// </summary>
    public enum PnalVisibleType
    { 
        /// <summary>
        /// 显示
        /// </summary>
        Show=1,
        /// <summary>
        /// 关闭
        /// </summary>
        Close=2,
    }

    /// <summary>
    /// 海运查询的权限
    /// </summary>
    public enum SearchPricePermission
    {
        /// <summary>
        /// 通用
        /// </summary>
        General=1,
        /// <summary>
        /// 查询底价
        /// </summary>
        ViewReserve=2,
    }

    /// <summary>
    /// InquierState(Inquier=1,Responded =2
    /// </summary>
    public enum InquierState
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// 询问
        /// </summary>
        Inquire = 1,

        /// <summary>
        /// 回复
        /// </summary>
        Responded = 2,

        /// <summary>
        /// 转移
        /// </summary>
        Transit = 3
    }

    /// <summary>
    /// 询价类型(1.海运,2.空运,3拖车
    /// </summary>
    public enum InquierType
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Ocean
        /// </summary>
        [MemberDescription("Ocean Rates", "Ocean Rates")] 
        OceanRates = 1,
        /// <summary>
        /// Air
        /// </summary>
        [MemberDescription("Air Rates", "Air Rates")] 
        AirRates = 2,

        /// <summary>
        /// Trucking
        /// </summary>
        [MemberDescription("Trucking Rates", "Trucking Rates")] 
        TruckingRates = 3
    
    }
    /// <summary>
    /// 修改状态
    /// </summary>
    public enum ChangeState
    {
        None = 0,
        New = 1,
        Changed = 2,
    }

    /// <summary>
    ///调整类型
    /// </summary>
    public enum AdjustmnetType
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// 中间值
        /// </summary>
        [MemberDescription("中间值", "Average Value")] 
        AverageValue = 1,

        /// <summary>
        /// 差额
        /// </summary>
        [MemberDescription("差额", "Difference")] 
        Difference = 2,
    }

}

