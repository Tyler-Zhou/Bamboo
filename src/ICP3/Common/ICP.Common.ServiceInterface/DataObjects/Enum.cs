using System;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.Common.ServiceInterface.DataObjects
{

    /// <summary>
    /// 客户自定义列表类型
    /// </summary>
    public enum ListFormType
    {
        /// <summary>
        /// 邮件中心客户面板
        /// </summary>
        MailLink4Customer = 1,
        /// <summary>
        ///邮件中心承运人面板
        /// </summary>
        MailLink4Carrier = 2,
        /// <summary>
        /// 邮件中心承运人SO面板
        /// </summary>
        MailLink4CarrierSO = 3,
        /// <summary>
        /// 邮件中心承运人MBL面板
        /// </summary>
        MailLink4CarrierMBL = 4,
        /// <summary>
        /// 邮件中心承运人AP面板
        /// </summary>
        MailLink4CarrierAP = 5,
        /// <summary>
        /// 邮件中心未知联系人面板
        /// </summary>
        MailLink4Unknown = 6,
        /// <summary>
        /// 通知客服有新订单| 通知订舱员Booking已更改        
        /// </summary>
        MailLink4NewBooking = 7,
        /// <summary>
        /// 通知商务员有新询价
        /// </summary>
        MailLink4NewInquireRate = 8,
        /// <summary>
        /// 通知客服有已回复询价
        /// </summary>
        MailLink4InquireRateReplied = 9,
        /// <summary>
        /// 向业务员确定应收费用
        /// </summary>
        MailLink4RateAP = 10,
        /// <summary>
        /// 要求业务员保证此业务的盈利性
        /// </summary>
        MailLink4BookingProfitable = 11,
        /// <summary>
        /// 邮件中心承运人AN面板
        /// </summary>
        MailLink4CarrierAN=12,
        /// <summary>
        /// 邮件中心4合1面板
        /// </summary>
        MailLink4in1 = 13,

    }
    /// <summary>
    /// 费用项目类型
    /// </summary>
    public enum ChargingGroupType
    {
        /// <summary>
        /// 定级节点
        /// </summary>
        Root = 0,

        /// <summary>
        /// 资产
        /// </summary>
        [MemberDescription("资产", "Assets")]
        Assets = 1,


        /// <summary>
        /// 负债
        /// </summary>
        [MemberDescription("负债", "Liabilities")]
        Liabilities = 2,

        /// <summary>
        /// 所有者权益
        /// </summary>
        [MemberDescription("所有者权益", "OwnerEquity")]
        OwnerEquity = 3,

        /// <summary>
        /// 成本
        /// </summary>
        [MemberDescription("成本", "Costs")]
        Costs = 4,

        /// <summary>
        /// 损益
        /// </summary>
        [MemberDescription("损益", "Income")]
        Income = 5,

        /// <summary>
        /// 费用
        /// </summary>
        [MemberDescription("费用", "Expense")]
        Expense = 6
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ReportParameterType
    {
        /// <summary>
        /// 字符串
        /// </summary>
        [MemberDescription("字符串")]
        //[System.Xml.Serialization.XmlEnum(Name = "1")]
        String = 1,

        /// <summary>
        /// 布尔
        /// </summary>
        [MemberDescription("布尔")]
        //[System.Xml.Serialization.XmlEnum(Name = "2")]
        Bool = 2,

        /// <summary>
        /// 数字
        /// </summary>
        [MemberDescription("数字")]
        //[System.Xml.Serialization.XmlEnum(Name = "3")]
        Digital = 3
    }

    /// <summary>
    /// 客户类型
    /// </summary>
    public enum CustomerType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [MemberDescription("未知","Unknown")]
        Unknown = 0,

        /// <summary>
        /// 船东
        /// </summary>
        [MemberDescription("船东","Carrier")]
        Carrier = 1,

        /// <summary>
        /// 航空公司
        /// </summary>
        [MemberDescription("航空公司","Airline")]
        Airline = 2,

        /// <summary>
        /// 货代
        /// </summary>
        [MemberDescription("货代","Freight Forwarding")]
        Forwarding = 3,

        /// <summary>
        /// 直客
        /// </summary>
        [MemberDescription("直客","Terminal Client")]
        DirectClient = 4,

        /// <summary>
        /// 拖车行
        /// </summary>
        [MemberDescription("拖车行","Truck Company")]
        Trucker = 5,

        /// <summary>
        /// 报关行
        /// </summary>
        [MemberDescription("报关行", "Customs Broker")]
        Broker = 6,

        /// <summary>
        /// 仓储
        /// </summary>
        [MemberDescription("仓储","Warehouse")]
        Warehouse = 7,

        /// <summary>
        /// 堆场
        /// </summary>
        [MemberDescription("堆场", "Storage Yard")]
        Storage = 8,

        /// <summary>
        /// 铁路
        /// </summary>
        [MemberDescription("铁路","Railway")]
        Railway = 9,

        /// <summary>
        /// 快递
        /// </summary>
        [MemberDescription("快递","Express")]
        Express = 10,

        /// <summary>
        /// 码头
        /// </summary>
        [MemberDescription("码头","Terminal")]
        Terminal = 11,

        /// <summary>
        /// 其它
        /// </summary>
        [MemberDescription("其它","Other")]
        Other = 12
    }

    /// <summary>
    /// 客户状态
    /// </summary>
    public enum CustomerStateType
    {
        /// <summary>
        /// 有效
        /// </summary>
        [MemberDescription("有效","Valid")]
        Valid = 1,

        /// <summary>
        /// 无效
        /// </summary>
        [MemberDescription("无效","Invalid")]
        Invalid = 2,
    }

    /// <summary>
    /// 代码申请人所属区域
    /// </summary>
    public enum CodeApplicantArea
    {
        /// <summary>
        /// 远东区
        /// </summary>
        [MemberDescription("远东区","Far East")]
        FarEast = 1,

        /// <summary>
        /// 北美区
        /// </summary>
        [MemberDescription("北美区", "North America")]
        NorthAmerica = 2,

        /// <summary>
        /// 越南公司
        /// </summary>
        [MemberDescription("越南公司", "Vietnam")]
        Vietnam = 3,
    }

    /// <summary>
    /// 客户代码申请状态
    /// </summary>
    public enum CustomerCodeApplyState
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部","All")]
        All = 0,

        /// <summary>
        /// 未申请
        /// </summary>
        [MemberDescription("未申请", "UnApply")]
        UnApply = 1,

        /// <summary>
        /// 正在处理。
        /// </summary>
        [MemberDescription("正在处理", "Processing")]
        Processing = 2,

        /// <summary>
        /// 已经通过
        /// </summary>
        [MemberDescription("已经通过", "Approved")]
        Passed = 3,

        /// <summary>
        /// 未通过
        /// </summary>
        [MemberDescription("未通过", "Not Approved")]
        Unpassed = 4
    }

    /// <summary>
    /// 客户联系人类型
    /// </summary>
    public enum CustomerContactType
    {
        /// <summary>
        /// 主要
        /// </summary>
        [MemberDescription("主要")]
        Primary,

        /// <summary>
        /// 次要
        /// </summary>
        [MemberDescription("次要")]
        Secondary,

        /// <summary>
        /// 正常
        /// </summary>
        [MemberDescription("正常")]
        Normal
    }
    

    /// <summary>
    /// 字典类型
    /// </summary>
    public enum DataDictionaryType
    {
        /// <summary>
        /// 
        /// </summary>
        [MemberDescription(" ", " ")]
        None = 0,

        /// <summary>
        /// 付费条款
        /// </summary>
        [MemberDescription("付费条款", "PaymentTerm")]
        PaymentTerm = 1,

        /// <summary>
        /// 运输条款
        /// </summary>
        [MemberDescription("运输条款", "Transport Clause")]
        TransportClause = 2,

        /// <summary>
        /// 包装单位
        /// </summary>
        [MemberDescription("包装单位", "Quantity Unit")]
        QuantityUnit = 3,

        /// <summary>
        /// 重量单位
        /// </summary>
        [MemberDescription("重量单位", "Weight Unit")]
        WeightUnit = 4,

        /// <summary>
        /// 体积单位
        /// </summary>
        [MemberDescription("体积单位", "Measurement Unit")]
        MeasurementUnit = 5,

        /// <summary>
        /// 贸易条款
        /// </summary>
        [MemberDescription("贸易条款", "Trade Term")]
        TradeTerm = 6,

        /// <summary>
        /// 揽货方式
        /// </summary>
        [MemberDescription("揽货方式", "Sales Type")]
        SalesType = 7,


        /// <summary>
        /// 计价单位
        /// </summary>
        [MemberDescription("计价单位", "Valuation Unit")]
        ValuationUnit = 8,


        /// <summary>
        /// 流程分类
        /// </summary>
        [MemberDescription("流程分类", "Workflow Category")]
        WorkflowCategory = 9,


        /// <summary>
        /// 运价等级
        /// </summary>
        [MemberDescription("运价等级", "RateClass")]
        RateClass = 10,


        /// <summary>
        /// 空运计量单位
        /// </summary>
        [MemberDescription("空运计量单位", "Air Unit")]
        AirUnit = 11,

        /// <summary>
        /// 影视项目
        /// </summary>
        [MemberDescription("影视项目", "Movie Projects")]
        MovieProjects = 12,

        /// <summary>
        /// 行业(Industry)
        /// </summary>
        [MemberDescription("行业", "Industry")]
        Industry = 98,
    }

    

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum DefaultValueType
    {
        /// <summary>
        /// 
        /// </summary>
        [MemberDescription("")]
        None = 0,

        /// <summary>
        /// 付款方式
        /// </summary>
        [MemberDescription("付款方式")]
        Payment = 1,

        /// <summary>
        /// 运输条款
        /// </summary>
        [MemberDescription("运输条款")]
        TransportClause = 2,

        /// <summary>
        /// 包装类型
        /// </summary>
        [MemberDescription("包装类型")]
        PackType = 3,

        /// <summary>
        /// 重量单位
        /// </summary>
        [MemberDescription("重量单位")]
        WeightUnits = 4,

        /// <summary>
        /// 体积单位
        /// </summary>
        [MemberDescription("体积单位")]
        VolUnits = 5,

        /// <summary>
        /// 贸易条款
        /// </summary>
        [MemberDescription("贸易条款")]
        TradeType = 6,

        /// <summary>
        /// 揽货方式
        /// </summary>
        [MemberDescription("揽货方式")]
        SalesType = 7,


        /// <summary>
        /// 计价单位
        /// </summary>
        [MemberDescription("计价单位")]
        ValuationUnit = 8,

        /// <summary>
        /// 币种
        /// </summary>
        [MemberDescription("币种")]
        Currency = 9
    }


    /// <summary>
    /// 费用代码节点类型
    /// </summary>
    public enum ChargeGroupNodeType
    {
        /// <summary>
        /// 根节点

        /// </summary>
        [MemberDescription("根")]
        Root = 1,

        /// <summary>
        /// 组

        /// </summary>
        [MemberDescription("组")]
        Group = 2,
    }

    /// <summary>
    /// 会计科目节点类型
    /// </summary>
    public enum GLCodeNodeType
    {
        /// <summary>
        /// 组
        /// </summary>
        [MemberDescription("组")]
        Group,

        /// <summary>
        /// 项

        /// </summary>
        [MemberDescription("项")]
        Item
    }

    /// <summary>
    /// 费用代码种类
    /// </summary>
    public enum ChargeCodeCategory
    {
        /// <summary>
        /// 管理成本
        /// </summary>
        [MemberDescription("管理成本")]
        ManagementCost,

        /// <summary>
        /// 运输成本
        /// </summary>
        [MemberDescription("运输成本")]
        TransportationCost,

        /// <summary>
        /// 业务费用
        /// </summary>
        [MemberDescription("业务费用")]
        OperationalExpenditures
    }

    #region 账单日期类型
    /// <summary>
    /// 账单日期类型
    /// </summary>
    public enum InvoiceDateType
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [MemberDescription("创建时间")]
        CreateTime,

        /// <summary>
        /// 业务时间
        /// </summary>
        [MemberDescription("业务时间")]
        ETD
    } 
    #endregion

    ///// <summary>
    ///// 会计科目配置类型
    ///// </summary>
    //public enum GLConfigType
    //{
    //    /// <summary>
    //    /// 费用项目
    //    /// </summary>
    //    [MemberDescription("费用项目")]
    //    CostItem = 1,

    //    /// <summary>
    //    /// 预收预付
    //    /// </summary>
    //    [MemberDescription("预收预付")]
    //    PreincomePrepayment = 2,

    //    /// <summary>
    //    /// 主营业收入

    //    /// </summary>
    //    [MemberDescription("主营业收入")]
    //    MainBusinessIncome = 3,

    //    /// <summary>
    //    /// 代收代付
    //    /// </summary>
    //    [MemberDescription("代收代付")]
    //    DRCR = 4,

    //    /// <summary>
    //    /// 汇兑损益
    //    /// </summary>
    //    [MemberDescription("汇兑损益")] 
    //    ExchangeGainsAndLosses = 5
    //}

    #region 国家省份类型
    /// <summary>
    /// 国家省份类型
    /// </summary>
    public enum CountryProvinceType
    {
        /// <summary>
        /// 国家
        /// </summary>
        [MemberDescription("国家")]
        Country = 1,

        /// <summary>
        /// 省份
        /// </summary>
        [MemberDescription("省份")]
        Province = 2
    } 
    #endregion

    #region 税务类型
    /// <summary>
    /// 税务类型
    /// </summary>
    public enum TaxType
    {
        /// <summary>
        /// Employer Identification Number 
        /// </summary>
        [MemberDescription("雇主编号", "Employer Identification Number")]
        EIN,

        /// <summary>
        /// Social Security Number
        /// </summary>
        [MemberDescription("社会安全号码", "Social Security Number")]
        SSN,

        /// <summary>
        ///  Individual Taxpayer Identification Number 
        /// </summary>
        [MemberDescription("个人税务编号", "Individual Tax Payer Identification Number")]
        ITIN,

        /// <summary>
        /// Adopted Tax Payer Identification Number
        /// </summary>
        [MemberDescription("纳税人识别号", "Adopted Tax Payer Identification Number")]
        ATIN
    } 
    #endregion

    #region 代码规则中年格式
    /// <summary>
    /// 代码规则中年格式
    /// </summary>
    public enum CodeYearFormart
    {
        /// <summary>
        /// 短格式 
        /// </summary>
        [MemberDescription("短格式")]
        Short = 2,

        /// <summary>
        /// 长格式
        /// </summary>
        [MemberDescription("长格式")]
        Long = 4
    } 
    #endregion

    #region DefaultType
    /// <summary>
    /// 
    /// </summary>
    public enum DefaultType
    {
    } 
    #endregion

    #region ViewStyle
    /// <summary>
    /// 
    /// </summary>
    public enum ViewStyle
    {
        /// <summary>
        /// 
        /// </summary>
        All,
        /// <summary>
        /// 
        /// </summary>
        Selected,
        /// <summary>
        /// 
        /// </summary>
        UnSelected
    } 
    #endregion

    #region 绑定类型
    /// <summary>
    /// 绑定类型
    /// </summary>
    public enum DataBindType
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
    #endregion

    #region 配置类型
    /// <summary>
    /// 配置类型
    /// </summary>
    public enum ConfigureType
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 系统单号
        /// </summary>
        SystemNo = 1,
        /// <summary>
        /// EDI
        /// </summary>
        EDI = 2,
        /// <summary>
        /// 抬头
        /// </summary>
        BLTitle = 3,
    } 
    #endregion

    #region EDI字典类型
    /// <summary>
    /// EDI字典类型
    /// </summary>
    public enum EDIDicType
    {
        /// <summary>
        /// AMS 包装单位
        /// </summary>
        AMS = 0,
        /// <summary>
        /// 韩进包装单位
        /// </summary>
        HANJINPackCode = 1,
        /// <summary>
        /// PIL 货物代码
        /// </summary>
        PILCommodCode = 2
    } 
    #endregion

    #region 发票抬头
    /// <summary>
    /// 发票抬头
    /// </summary>
    public enum CustomerInvoiceType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [MemberDescription("未知")]
        Unknown = 0,
        /// <summary>
        /// 专用发票
        /// </summary>
        [MemberDescription("专用发票")]
        Dedicated = 1,
        /// <summary>
        /// 普通发票
        /// </summary>
        [MemberDescription("普通发票")]
        Ordinary = 2,
        /// <summary>
        /// 电子发票
        /// </summary>
        [MemberDescription("电子发票")]
        Electronic = 3
    } 
    #endregion

    #region 汇率类型
    /// <summary>
    /// 汇率类型
    /// </summary>
    public enum ExchangeType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [MemberDescription("未知")]
        Unknown = 0,

        /// <summary>
        /// 账单
        /// </summary>
        [MemberDescription("账单")]
        Bill = 1,

        /// <summary>
        /// 发票
        /// </summary>
        [MemberDescription("发票")]
        Invoice = 2,
    } 
    #endregion

    #region 动作类型
    /// <summary>
    /// 动作类型
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// 新增
        /// </summary>
        [MemberDescription("新增")]
        Create = 0,
        /// <summary>
        /// 编辑
        /// </summary>
        [MemberDescription("编辑")]
        Edit,
        /// <summary>
        /// 删除
        /// </summary>
        [MemberDescription("删除")]
        Delete,
    } 
    #endregion

    #region 会计科目类型
    /// <summary>
    /// 会计科目类型
    /// </summary>
    public enum GLCodeType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 资产
        /// </summary>
        [MemberDescription("资产", "ASSETS")]
        ASSETS = 1,

        /// <summary>
        /// 负债
        /// </summary>
        [MemberDescription("负债", "LIABILITIES")]
        LIABILITIES = 2,

        /// <summary>
        /// 权益
        /// </summary>
        [MemberDescription("权益", "INCOME")]
        INCOME = 3,

        /// <summary>
        /// 成本
        /// </summary>
        [MemberDescription("成本", "COST")]
        COST = 4,

        /// <summary>
        /// 损益
        /// </summary>
        [MemberDescription("损益", "EQUITY")]
        EQUITY = 5,


    } 
    #endregion

    #region 会计科目-帐页格式
    /// <summary>
    /// 会计科目-帐页格式
    /// </summary>
    public enum GLCodeLedgerStyle
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 金额式
        /// </summary>
        [MemberDescription("金额式", "AMOUNT")]
        AMOUNT = 1,

        /// <summary>
        /// 外币金额式
        /// </summary>
        [MemberDescription("外币金额式", "OUTGOAMOUNT")]
        OUTGOAMOUNT = 2,

        /// <summary>
        /// 数量金额式
        /// </summary>
        [MemberDescription("数量金额式", "NUMAMOUNT")]
        NUMAMOUNT = 3,

        /// <summary>
        /// AMOUNTFOREIGN
        /// </summary>
        [MemberDescription("数量式外币", "AMOUNTFOREIGN")]
        AMOUNTFOREIGN = 4

    } 
    #endregion

    #region 会计科目--科目性质
    /// <summary>
    /// 会计科目--科目性质
    /// </summary>
    public enum GLCodeProperty
    {
        /// <summary>
        /// 未知
        /// </summary>
        [MemberDescription("平", "Balance")]
        Unknown = 0,
        /// <summary>
        /// 借方
        /// </summary>
        [MemberDescription("借方", "Debit")]
        Debit = 1,
        /// <summary>
        /// 贷方
        /// </summary>
        [MemberDescription("贷方", "Credit")]
        Credit = 2
    } 
    #endregion

    #region 分发文档状态
    /// <summary>
    /// 分发文档状态
    /// </summary>
    public enum DispatchStare
    {
        /// <summary>
        /// 准备分发
        /// </summary>
        [MemberDescription("准备分发", "Ready to Dispatch")]
        Ready = 1,
        /// <summary>
        /// 等待分发
        /// </summary>
        [MemberDescription("等待分发", "Wait Dispatch")]
        Wait = 2,
        /// <summary>
        /// 正在分发
        /// </summary>
        [MemberDescription("正在分发", "Dispatching")]
        Dispatching = 3,
        /// <summary>
        /// 分发完成
        /// </summary>
        [MemberDescription("分发完成", "Dispatched")]
        Dispatched = 4,
        /// <summary>
        /// 分发失败
        /// </summary>
        [MemberDescription("分发失败", "Dispatch Fail")]
        Failure = 5,
        /// <summary>
        /// 签收
        /// </summary>
        [MemberDescription("签收", "Accept")]
        Accept = 6
    } 
    #endregion

    #region PrecautionsType
    /// <summary>
    ///   仓储、拖车、报关、商检、熏蒸、产地证
    /// </summary>
    public enum PrecautionsType
    {
        /// <summary>
        /// 仓储
        /// </summary>
        WareHouse,
        /// <summary>
        /// 拖车
        /// </summary>
        Truck,
        /// <summary>
        /// 报关
        /// </summary>
        Customs,
        /// <summary>
        /// 商检
        /// </summary>
        CommodityInspection,
        /// <summary>
        /// 熏蒸
        /// </summary>
        Fumigated,
        /// <summary>
        /// 产地证
        /// </summary>
        Certificate,
        /// <summary>
        /// 
        /// </summary>
        SI,
        /// <summary>
        /// 
        /// </summary>
        Invoice,

    } 
    #endregion

    #region PartnerType
    /// <summary>
    /// 
    /// </summary>
    public enum PartnerType
    {
        /// <summary>
        /// 
        /// </summary>
        Shipper,
        /// <summary>
        /// 
        /// </summary>
        Consignee,
        /// <summary>
        /// 
        /// </summary>
        NotifyParty
    }
    #endregion

    #region ACIEntryType
    /// <summary>
    /// 提前商业信息(Advance Commercial Information)ACI Entry Type IntransitGoodsToUS = 23,ImportedGoods = 24,FROB = 26
    /// </summary>
    [Flags]
    [Serializable]
    public enum ACIEntryType
    {
        /// <summary>
        /// 不确定(Unknown)
        /// </summary>
        //[MemberDescription("不确定", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 将货物运往美国(Intransit Goods to US)
        /// </summary>
        //[MemberDescription("将货物运往美国", "Intransit Goods to US")]
        IntransitGoodsToUS = 23,

        /// <summary>
        /// 导入的商品(Imported Goods)
        /// </summary>
        //[MemberDescription("导入的商品", "ImportedGoods")]
        ImportedGoods = 24,

        /// <summary>
        /// FROB
        /// </summary>
        //[MemberDescription("FROB", "FROB")]
        FROB = 26
    }
    #endregion

    #region AMSEntryType
    /// <summary>
    /// 美国仓单系统(America manifest system) Entry Type(港到港=60, 内陆运输 = 61,越境出口 = 62,立即重新出口 = 63,留在船上 = 64
    /// </summary>
    [Flags]
    [Serializable]
    public enum AMSEntryType
    {
        /// <summary>
        /// 不确定(Unknown)
        /// </summary>
        //[MemberDescription("Unknown", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 港到港(Port To Port)
        /// </summary>
        //[MemberDescription("港到港", "Port To Port")]
        PorttoPort = 60,

        /// <summary>
        /// 内陆运输(Inland Transit)
        /// </summary>
        //[MemberDescription("内陆运输", "Inland Transit")]
        InlandTransit = 61,

        /// <summary>
        /// 越境出口(Transit Export)
        /// </summary>
        //[MemberDescription("越境出口", "Transit Export")]
        TransitExport = 62,

        /// <summary>
        /// 立即重新出口(Immediate Re Export)
        /// </summary>
        //[MemberDescription("立即重新出口", "Immediate Re Export")]
        ImmediateReexport = 63,

        /// <summary>
        /// 留在船上(Stay On Board)
        /// </summary>
        //[MemberDescription("留在船上", "Stay On Board")]
        StayonBoard = 64
    }
    #endregion
}
