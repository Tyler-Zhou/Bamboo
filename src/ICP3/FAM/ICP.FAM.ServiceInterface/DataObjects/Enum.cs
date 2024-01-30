using ICP.Framework.CommonLibrary.Attributes;
using System;
namespace ICP.FAM.ServiceInterface.DataObjects
{
    #region 账单类型
    /// <summary>
    /// 账单类型（1:应收,2:应付,3:代理）
    /// </summary>
    public enum BillType
    {
        /// <summary>
        /// 无
        /// </summary>
        [MemberDescription("无", "None")]
        None = 0,
        /// <summary>
        /// 应收
        /// </summary>
        [MemberDescription("应收", "AR")]
        AR = 1,

        /// <summary>
        /// 应付
        /// </summary>
        [MemberDescription("应付", "AP")]
        AP = 2,

        /// <summary>
        /// 代理
        /// </summary>
        [MemberDescription("代理", "D/C")]
        DC = 3
    } 
    #endregion

    #region 账单状态
    /// <summary>
    /// 账单状态（1:已创建、2已审核、3已对账、4已核销、5已到账)
    /// </summary>
    public enum BillState
    {
        /// <summary>
        /// 无
        /// </summary>
        [MemberDescription("")]
        None = 0,

        /// <summary>
        /// 已创建
        /// </summary>
        [MemberDescription("已创建", "Created")]
        Created = 1,

        /// <summary>
        /// 已审核
        /// </summary>
        [MemberDescription("已审核", "Approved")]
        Approved = 2,

        /// <summary>
        /// 已对账
        /// </summary>
        [MemberDescription("已对账", "Checked")]
        Checked = 3,

        /// <summary>
        /// 已销账
        /// </summary>
        [MemberDescription("已销账", "WriteOff")]
        WriteOff = 4,

        /// <summary>
        /// 已到账
        /// </summary>
        [MemberDescription("已到账", "Paid")]
        Paid = 5
    } 
    #endregion

    #region 费用方向
    /// <summary>
    /// 费用方向（1:应收,2:应付）
    /// </summary>
    [Flags]
    [Serializable]
    public enum FeeWay
    {
        /// <summary>
        /// 无
        /// </summary>
        [MemberDescription("无", "None")]
        None = 0,
        /// <summary>
        /// 应收
        /// </summary>
        [MemberDescription("应收", "AR")]
        AR = 1,

        /// <summary>
        /// 应付
        /// </summary>
        [MemberDescription("应付", "AP")]
        AP = 2
    } 
    #endregion

    #region 费用类型
    /// <summary>
    /// 费用类型类型（1:正常,2:分摊,3:运价）
    /// </summary>
    public enum FeeType
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 分摊
        /// </summary>
        Apportionment = 2,

        /// <summary>
        /// 运价
        /// </summary>
        Price = 3,
        /// <summary>
        /// 返利
        /// </summary>
        Rebate = 4,
    } 
    #endregion

    #region 收付款单类型
    /// <summary>
    /// 收付款单类型 0-None;1-收款单;2-付款单;
    /// </summary>
    public enum CheckType
    {
        /// <summary>
        /// None
        /// </summary>
        [MemberDescription("全部", "All")]
        None = 0,
        /// <summary>
        /// 收款单
        /// </summary>
        [MemberDescription("收款单", "AR")]
        AR = 1,
        /// <summary>
        /// 付款单
        /// </summary>
        [MemberDescription("付款单", "AP")]
        AP = 2,

    }
    #endregion

    #region 帐单的日期搜索类型
    /// <summary>
    /// 帐单的日期搜索类型
    /// </summary>
    public enum DateSearchType
    {
        /// <summary>
        /// 没时间
        /// </summary>
        None,

        /// <summary>
        /// 帐单日期
        /// </summary>
        BillDate,

        /// <summary>
        /// 业务时间
        /// </summary>
        OperationDate
    }
    #endregion

    #region 财务状态
    /// <summary>
    /// 财务状态
    /// </summary>
    public enum FinanceState
    {
        /// <summary>
        /// 未付款
        /// </summary>
        None,

        /// <summary>
        /// 部分付款
        /// </summary>
        Part,

        /// <summary>
        /// 全部付款
        /// </summary>
        All
    }
    #endregion

    #region 放单状态
    /// <summary>
    /// 放单状态(已创建=1、已签收=2、已放单=3、已接收=4)
    /// </summary>
    public enum ReleaseBLState
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 已创建
        /// </summary>
        [MemberDescription("未放单", "Hold")]
        Created = 1,

        ///// <summary>
        ///// 已签收
        ///// </summary>
        //[MemberDescription("已签收", "Issue")]
        //Issue = 2,

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

    #region 放单状态
    /// <summary>
    /// 放单状态(已放单=3、已接收=4、已放货=5)
    /// </summary>
    public enum ReleaseRCState
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 已创建
        /// </summary>
        [MemberDescription("未放单", "Hold")]
        Created = 1,

        /// <summary>
        /// 已放单
        /// </summary>
        [MemberDescription("已放单", "RBL")]
        RBL = 3,

        /// <summary>
        /// 已接收
        /// </summary>
        [MemberDescription("已接收", "Rcv RBL")]
        RcvRBL = 4,

        /// <summary>
        /// 已放货
        /// </summary>
        [MemberDescription("已放货", "RC")]
        RC = 5,

    }
    #endregion

    #region 放单状态
    /// <summary>
    /// 放单状态(正本 1 ,电放 2)
    /// </summary>
    public enum ReleaseType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 正本
        /// </summary>
        [MemberDescription("O", "O")]
        Original = 1,
        /// <summary>
        /// 电放
        /// </summary>
        [MemberDescription("T", "T")]
        Telex = 2,

        /// <summary>
        /// sea way
        /// </summary>
        [MemberDescription("sea way", "sea way")]
        seaway = 3,

        /// <summary>
        ///正本改为电放
        /// </summary>
        [MemberDescription("changed fm O to T", "changed fm O to T")]
        OconvT = 4,

        /// <summary>
        ///电放改为正本
        /// </summary>
        ///  
        [MemberDescription("changed fm T to O", "changed fm T to O")]
        TconvO = 5,


    }
    #endregion

    #region 核销币种类型
    /// <summary>
    /// 核销币种类型
    /// </summary>
    public enum WriteOffType
    {
        /// <summary>
        /// 单币种
        /// </summary>
        Single = 0,
        /// <summary>
        /// 多个币种
        /// </summary>
        Muitl = 1
    }
    #endregion

    #region 销帐模式
    /// <summary>
    /// 销帐模式
    /// </summary>
    public enum CheckMode
    {
        /// <summary>
        /// 账单
        /// </summary>
        Bill = 1,
        /// <summary>
        /// 费用
        /// </summary>
        Charge = 2,

    }
    #endregion

    #region 报关类型
    /// <summary>
    /// 报关类型
    /// </summary>
    public enum CustomsType
    {
        /// <summary>
        /// 清关 
        /// </summary>
        [MemberDescription("清关", "CustomsClearance")]
        CustomsClearance = 1,

        /// <summary>
        /// 转关
        /// </summary>
        [MemberDescription("转关", "CustomsTransferring")]
        CustomsTransferring = 2,

        /// <summary>
        /// 买单
        /// </summary>
        [MemberDescription("买单", "Pay")]
        Pay = 3,

        /// <summary>
        /// 包单
        /// </summary>
        [MemberDescription("包单", "Single")]
        Single = 4,

        /// <summary>
        /// 证书
        /// </summary>
        [MemberDescription("证书", "Certificate")]
        Certificate = 5,

        /// <summary>
        /// 入仓
        /// </summary>
        [MemberDescription("入仓", "InWareHouse")]
        InWareHouse = 6,

        /// <summary>
        /// 进口
        /// </summary>
        [MemberDescription("进口", "Import")]
        Import = 7,

        /// <summary>
        /// 退关
        /// </summary>
        [MemberDescription("退关", "Shut")]
        Shut = 8,

        /// <summary>
        /// 退运
        /// </summary>
        [MemberDescription("退运", "Returned")]
        Returned = 9,


        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部", "All")]
        All = 10
    }
    #endregion

    #region 对帐单的状态
    /// <summary>
    /// 对帐单的状态
    /// </summary>
    public enum AgentBillCheckStatusEnum
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,
        /// <summary>
        /// 创建（发起代理）
        /// </summary>
        [MemberDescription("创建", "Created")]
        Created = 1,

        /// <summary>
        /// 已发起对帐（发起代理）
        /// </summary>
        [MemberDescription("已发起对账", "StartCheck")]
        StartCheck = 2,

        /// <summary>
        /// 核对帐单中（核对代理）
        /// </summary>
        [MemberDescription("核对帐单中", "Checking")]
        Checking = 3,

        /// <summary>
        /// 已通知修改帐单（核对代理）
        /// </summary>
        [MemberDescription("已通知修改帐单", "NotifiedBillOwner")]
        NotifiedBillOwner = 4,

        /// <summary>
        /// 已完成对帐（核对代理）
        /// </summary>
        [MemberDescription("完成对账", "Completed")]
        Completed = 5
    }
    #endregion

    #region 对账类型
    /// <summary>
    /// 对账类型
    /// </summary>
    public enum AgentBillCheckType
    {
        /// <summary>
        /// 内部代理对账
        /// </summary>
        [MemberDescription("内部代理对账", "InternalAgentBill")]
        InternalAgentBill = 1
    }
    #endregion

    #region 账单查询 审核状态
    /// <summary>
    /// 账单查询 审核状态
    /// </summary>
    public enum BillSearchAuditorStatue
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部", "All")]
        All = 0,
        /// <summary>
        /// 已审核
        /// </summary>
        [MemberDescription("已审核", "Auditor")]
        Auditor = 1,
        /// <summary>
        /// 未审核
        /// </summary>
        [MemberDescription("未审核", "UnAuditor")]
        UnAuditor = 2
    }
    #endregion

    #region 账单查询 核销状态
    /// <summary>
    /// 账单查询 核销状态
    /// </summary>
    public enum BillSearchWriteOffStatue
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 已核销
        /// </summary>
        WriteOff = 1,
        /// <summary>
        /// 未核销
        /// </summary>
        UnWriteOff = 2
    }
    #endregion

    #region 账单查询 支付类型
    /// <summary>
    /// 账单查询 支付类型
    /// </summary>
    [Flags]
    [Serializable]
    public enum BillSearchPaymentWay
    {
        /// <summary>
        /// 所有
        /// </summary>
        All = 0,
        /// <summary>
        /// 直连
        /// </summary>
        Direct = 1
    }
    #endregion

    #region 账单查询 收付类型
    /// <summary>
    /// 账单查询 收付类型
    /// </summary>
    [Flags]
    [Serializable]
    public enum BillSearchFeeWay
    {
        /// <summary>
        /// 
        /// </summary>
        All = 0,
        /// <summary>
        /// 应收
        /// </summary>
        AR = 1,
        /// <summary>
        /// 应付
        /// </summary>
        AP = 2
    }
    #endregion

    #region 账单查询 发票状态
    /// <summary>
    /// 账单查询 发票状态
    /// </summary>
    public enum BillSearchInvoiceStatue
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 已开发票
        /// </summary>
        Yes = 1,
        /// <summary>
        /// 未开发票
        /// </summary>
        No = 2,

    }
    #endregion

    #region 核销查询日期类型
    /// <summary>
    /// 核销查询日期类型
    /// </summary>
    public enum WriteOffSearchDateType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部", "All")]
        All = 0,
        /// <summary>
        /// 销帐日期
        /// </summary>
        [MemberDescription("销帐日期", "Check Date")]
        WriteOffDate = 1,
        /// <summary>
        /// 到帐日期
        /// </summary>
        [MemberDescription("到帐日期", "Reached Date")]
        ReachedDate = 2,

    }
    #endregion

    #region 账单方案
    /// <summary>
    /// 账单方案
    /// </summary>
    public enum BillProgram
    {
        /// <summary>
        /// 自定义
        /// </summary>
        [MemberDescription("自定义", "Custom")]
        Custom = 0,

        /// <summary>
        /// 审核
        /// </summary>
        [MemberDescription("审核", "Auditor")]
        Auditor = 1,

        /// <summary>
        /// 应收核销
        /// </summary>
        [MemberDescription("应收核销", "Deposit WriteOff")]
        DepositWriteOff = 2,

        /// <summary>
        /// 应付核销
        /// </summary>
        [MemberDescription("应付核销", "Check WriteOff")]
        CheckWriteOff = 3,

        /// <summary>
        /// 付款申请
        /// </summary>
        [MemberDescription("付款申请", "Payment Request")]
        PaymentRequest = 4,

        /// <summary>
        /// 业务管理成本
        /// </summary>
        [MemberDescription("业务管理成本", "Operation Management")]
        OperationManagement = 5,

        /// <summary>
        /// 开发票
        /// </summary>
        [MemberDescription("开发票", "Invoicing")]
        Invoicing = 6,

        /// <summary>
        /// 催款单
        /// </summary>
        [MemberDescription("催款单", "Dun")]
        Dun = 7

    }
    #endregion

    #region 到账状态
    /// <summary>
    /// 到账状态
    /// </summary>
    public enum PaidStatus
    {

        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 未到账
        /// </summary>
        [MemberDescription("未到账", "None")]
        None = 1,

        /// <summary>
        /// 部分到账
        /// </summary>
        [MemberDescription("部分到账", "Part")]
        Part = 2,


        /// <summary>
        /// 全部到账
        /// </summary>
        [MemberDescription("全部到账", "All")]
        All = 3,


    }
    #endregion

    #region 付款单分组方式
    /// <summary>
    /// 付款单分组方式
    /// </summary>
    public enum DunGroupType
    {
        /// <summary>
        /// 按账单号汇总
        /// </summary>
        [MemberDescription("按账单号汇总", "Bill Total")]
        BillTotal = 1,
        /// <summary>
        /// 按应收明细汇总
        /// </summary>
        [MemberDescription("按应收明细汇总", "Cost Detail Total")]
        CostDetailTotal = 2,
    }
    #endregion

    #region 月结客户类型
    /// <summary>
    /// 月结客户类型
    /// </summary>
    public enum MonthlyCustomerType
    {
        /// <summary>
        /// 账单客户
        /// </summary>
        [MemberDescription("账单客户", "BillCustomer")]
        BillCustomer = 1,

        /// <summary>
        /// 发货人
        /// </summary>
        [MemberDescription("发货人", "Shipper")]
        Shipper = 2,

        /// <summary>
        /// 对单人
        /// </summary>
        [MemberDescription("对单人", "Checker")]
        Checker = 3,
    }
    #endregion

    #region 月结协议-付款日类型

    /// <summary>
    /// 记账日类型
    /// </summary>
    public enum CalculateTermType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = 0,
        /// <summary>
        /// 计费日期
        /// </summary>
        [MemberDescription("计费日期", "Billing Date")]
        BillingDate = 1,
        /// <summary>
        /// ETD之后
        /// </summary>
        [MemberDescription("ETD", "ETD")]
        ETD = 2,
        /// <summary>
        /// ETA之后
        /// </summary>
        [MemberDescription("ETA", "ETA")]
        ETA = 3,
    }

    #endregion

    #region 凭证主表类型
    /// <summary>
    /// 凭证主表类型
    /// </summary>
    public enum LedgerMasterType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 记帐凭证
        /// </summary>
        [MemberDescription("记帐凭证", "Billing Voucher")]
        Billing = 1,

        /// <summary>
        /// 实收实付
        /// </summary>
        [MemberDescription("实收实付", "Account Voucher")]
        Account = 2,

        /// <summary>
        /// 管理成本
        /// </summary>
        [MemberDescription("管理成本", "Commision")]
        Commision = 3,

        /// <summary>
        /// 结转凭证
        /// </summary>
        [MemberDescription("结转凭证", "CarryForward")]
        CarryForward = 4,

        /// <summary>
        /// 汇总损益
        /// </summary>
        [MemberDescription("汇总损益", "Exchange")]
        Exchange = 99,
    }
    #endregion

    #region 凭证明细类型
    /// <summary>
    /// 凭证明细类型
    /// </summary>
    public enum LedgerDetailType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 应收
        /// </summary>
        [MemberDescription("应收", "AP")]
        AP = 1,

        /// <summary>
        /// 应付
        /// </summary>
        [MemberDescription("应付", "AR")]
        AR = 2,

        /// <summary>
        /// 代理
        /// </summary>
        [MemberDescription("代理", "DRCR")]
        DRCR = 3,

        /// <summary>
        /// 实付
        /// </summary>
        [MemberDescription("实付", "Payment")]
        Payment = 4,

        /// <summary>
        /// 实收
        /// </summary>
        [MemberDescription("实收", "Receivables")]
        Receivables = 5,

        /// <summary>
        /// 日记帐
        /// </summary>
        [MemberDescription("日记帐", "JOURNAL")]
        JOURNAL = 6,

        /// <summary>
        /// 报销
        /// </summary>
        [MemberDescription("报销", "Commision")]
        Commision = 7,

        /// <summary>
        /// 期初余额
        /// </summary>
        [MemberDescription("期初余额", "BeginBalance")]
        BeginBalance = 8,

        /// <summary>
        /// 汇总损益
        /// </summary>
        [MemberDescription("汇总损益", "Exchange")]
        Exchange = 99,
    }
    #endregion

    #region 凭证主表状态
    /// <summary>
    /// 凭证主表状态
    /// </summary>
    public enum LedgerMasterStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 制单
        /// </summary>
        [MemberDescription("制单", "CreateBy")]
        CreateBy = 1,

        /// <summary>
        /// 出纳签字
        /// </summary>
        [MemberDescription("出纳签字", "CashierChecked")]
        CashierChecked = 2,

        /// <summary>
        /// 财务主管签字
        /// </summary>
        [MemberDescription("财务主管签字", "FinanceManagerChecked")]
        FinanceManagerChecked = 3,

        /// <summary>
        /// 审核
        /// </summary>
        [MemberDescription("审核", "Auditor")]
        Auditor = 4,

        /// <summary>
        /// 记账
        /// </summary>
        [MemberDescription("记账", "KeepAccounts")]
        KeepAccounts = 5,

        /// <summary>
        /// 结账
        /// </summary>
        [MemberDescription("结账", "CloseAccounts")]
        CloseAccounts = 6,
    }
    #endregion

    #region 凭证查询金额类型
    /// <summary>
    /// 凭证查询金额类型
    /// </summary>
    public enum LedgerSearchAmountType
    {
        /// <summary>
        /// 
        /// </summary>
        All = 0,
        /// <summary>
        /// 金额
        /// </summary>
        Amount = 1,
        /// <summary>
        /// 外币金额
        /// </summary>
        FCAmount = 2,
    }
    #endregion

    #region 修改状态
    /// <summary>
    /// 修改状态
    /// </summary>
    public enum ChangeState
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,
        /// <summary>
        /// 
        /// </summary>
        New = 1,
        /// <summary>
        /// 
        /// </summary>
        Changed = 2,
    }
    #endregion

    #region 帐龄表
    /// <summary>
    /// 帐龄表
    /// </summary>
    public enum AgingDateState
    {
        /// <summary>
        /// 
        /// </summary>
        All = 0,
        /// <summary>
        /// 
        /// </summary>
        Over90 = 1,
        /// <summary>
        /// 
        /// </summary>
        Over60 = 2,
        /// <summary>
        /// 
        /// </summary>
        Over45 = 3,
        /// <summary>
        /// 
        /// </summary>
        Over30 = 4,
        /// <summary>
        /// 
        /// </summary>
        Less30 = 5,
    }
    #endregion

    #region 费用预算类型
    /// <summary>
    /// 费用预算类型
    /// </summary>
    public enum FeeMonthBudgetType
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,
        /// <summary>
        /// 管理费用
        /// </summary>
        Cost = 1,
        /// <summary>
        /// 财务费用
        /// </summary>
        Finance = 2
    }
    #endregion

    #region 帐龄表Term客户选取枚举
    /// <summary>
    /// 帐龄表Term客户选取枚举
    /// </summary>
    public enum TermType
    {
        /// <summary>
        /// 显示全部
        /// </summary>
        All = 1,

        /// <summary>
        /// 只显示Term客户
        /// </summary>
        Term = 2,

        /// <summary>
        /// 显示非Term客户
        /// </summary>
        UnTerm = 3,
    }
    #endregion

    #region 帐龄表Insured客户选取枚举
    /// <summary>
    /// 帐龄表Insured客户选取枚举
    /// </summary>
    public enum InsuredType
    {
        /// <summary>
        /// 显示全部
        /// </summary>
        All = 1,

        /// <summary>
        /// 只显示Insured客户
        /// </summary>
        Insured = 2,

        /// <summary>
        /// 显示非Insured客户
        /// </summary>
        UnInsured = 3,
    }
    #endregion

    #region 凭证查询类型
    /// <summary>
    /// 凭证查询类型
    /// </summary>
    public enum VoucherSearchType
    {
        /// <summary>
        /// 首张
        /// </summary>
        Home = 1,
        /// <summary>
        /// 上一张
        /// </summary>
        Up = 2,
        /// <summary>
        /// 下一张
        /// </summary>
        Next = 3,
        /// <summary>
        /// 末张
        /// </summary>
        End = 4

    }
    #endregion

    #region 电放类型
    /// <summary>
    /// 电放类型
    /// </summary>
    public enum TelexType
    {
        /// <summary>
        /// 所有
        /// </summary>
        All = 0,
        /// <summary>
        /// 电放
        /// </summary>
        Telex = 1,
        /// <summary>
        /// Sea Way B/L
        /// </summary>
        SWB = 2,
    }
    #endregion

    #region 水单状态
    /// <summary>
    /// 水单状态 (未知=0 新建=1 已验证=2 已销账=3)
    /// </summary>
    public enum BankReceiptStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = 0,

        /// <summary>
        /// 新建
        /// </summary>
        [MemberDescription("新建", "Created")]
        Created = 1,

        /// <summary>
        /// 已验证
        /// </summary>
        [MemberDescription("已验证", "Verified")]
        Verified = 2,

        /// <summary>
        /// 已销账
        /// </summary>
        [MemberDescription("已销账", "WriteOff")]
        WriteOff = 3
    }
    #endregion
}


