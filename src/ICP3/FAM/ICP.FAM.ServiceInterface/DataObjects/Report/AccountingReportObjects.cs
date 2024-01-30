using System;
using System.Collections.Generic;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.ServiceInterface.DataObjects
{

    #region LedgerData

    /// <summary>
    /// LedgerData
    /// </summary>
    [Serializable]
    public class LedgerData
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// GLId
        /// </summary>
        public Guid GLId { get; set; }
        /// <summary>
        /// GLCode
        /// </summary>
        public string GLCode { get; set; }
        /// <summary>
        /// GLDescription
        /// </summary>
        public string GLDescription { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// BeginBalance
        /// </summary>
        public decimal BeginBalance { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        public decimal Balance { get; set; }


        /// <summary>
        /// CrAmt
        /// </summary>
        public decimal CrAmt { get; set; }
        /// <summary>
        /// DrAmt
        /// </summary>
        public decimal DrAmt { get; set; }

        /// <summary>
        /// OrgAmt
        /// </summary>
        public decimal OrgAmt { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// BillType
        /// </summary>
        public ReportBillType BillType { get; set; }
        /// <summary>
        /// BillId
        /// </summary>
        public Guid BillId { get; set; }
        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// CustomerFinanceCode
        /// </summary>
        public string CustomerFinanceCode { get; set; }
        /// <summary>
        /// OperationNo
        /// </summary>
        public string OperationNo { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// CustomerID
        /// </summary>
        public Guid CustomerID { get; set; }
    }

    #endregion

    #region GLData

    /// <summary>
    /// 一个List加一个统计
    /// </summary>
    [Serializable]
    public class GLDataAndTotalInfo
    {
        public List<GLData> GLDataList { get; set; }
        public GLDataTotal DataTottal { get; set; }
    }

    /// <summary>
    /// 两个List
    /// </summary>
    [Serializable]
    public class GLDataList
    {
        public List<GLData> LieqList { get; set; }

        public List<GLData> AssetsList { get; set; }
    }


    /// <summary>
    /// GL统计数据
    /// </summary>
    [Serializable]
    public class GLDataTotal
    {
        public decimal IncomeAmount
        {
            get;
            set;
        }

        public decimal CostAmount
        {
            get;
            set;
        }
        public decimal CrossIncome
        {
            get;
            set;
        }
        public decimal OtherAmount
        {
            get;
            set;
        }

    }

    /// <summary>
    /// GLData
    /// </summary>
    [Serializable]
    public class GLData
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        public ChargingGroupType CategoryType { get; set; }

        /// <summary>
        /// CategoryTypeCName
        /// </summary>
        public string CategoryTypeCName { get; set; }

        /// <summary>
        /// CategoryTypeEName
        /// </summary>
        public string CategoryTypeEName { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        public string Category
        {
            get
            {
                return CategoryType.ToString();
            }
        }
        /// <summary>
        /// Type
        /// </summary>
        public GLTypeData Type { get; set; }
        /// <summary>
        /// BeginBalance
        /// </summary>
        public decimal BeginBalance { get; set; }
        /// <summary>
        /// Debit
        /// </summary>
        public decimal Debit { get; set; }
        /// <summary>
        /// Credit
        /// </summary>
        public decimal Credit { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// BillSource
        /// </summary>
        public OperationType BillSource { get; set; }
    }



    /// <summary>
    /// GLTypeData
    /// </summary>
    [Serializable]
    public class GLTypeData
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// CName
        /// </summary>
        public string CName { get; set; }
        /// <summary>
        /// EName
        /// </summary>
        public string EName { get; set; }
        /// <summary>
        /// Parent
        /// </summary>
        public GLTypeData Parent { set; get; }
    }

    #endregion

    #region  CheckData
    /// <summary>
    /// PaymentCheckData
    /// </summary>
    [Serializable]
    public class CheckData
    {
        /// <summary>
        /// Id
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// No
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// CheckDate
        /// </summary>
        public System.DateTime CheckDate { get; set; }
        /// <summary>
        /// WriteOffAmount
        /// </summary>
        public decimal? WriteOffAmount { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// CheckNo
        /// </summary>
        public string CheckNo { get; set; }
        /// <summary>
        /// BankDate
        /// </summary>
        public DateTime? BankDate { get; set; }
        /// <summary>
        /// CompanyId
        /// </summary>
        public System.Guid CompanyId { get; set; }
        /// <summary>
        /// CompanyName
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// CustomerId
        /// </summary>
        public System.Guid CustomerId { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// BankAccountId
        /// </summary>
        public System.Guid BankAccountId { get; set; }
        /// <summary>
        /// BankAccountDescription
        /// </summary>
        public string BankAccountDescription { get; set; }
    }

    #endregion

    #region  ReportBillData
    /// <summary>
    /// ReportBillData
    /// </summary>
    [Serializable]
    public class ReportBillData
    {
        /// <summary>
        /// Id
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// CompanyName
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// AccountDate
        /// </summary>
        public System.DateTime AccountDate { get; set; }
        /// <summary>
        /// OperationNo
        /// </summary>
        public string OperationNo { get; set; }
        /// <summary>
        /// CheckUserName
        /// </summary>
        public string CheckUserName { get; set; }
        /// <summary>
        /// RefNo
        /// </summary>
        public string RefNo { get; set; }
        /// <summary>
        /// No
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// Fees
        /// </summary>
        public List<ChargeList> Fees { get; set; }

    }

    #endregion

    #region  BankOutStandingData
    /// <summary>
    /// BankOutStandingData
    /// </summary>
    [Serializable]
    public class BankOutStandingData
    {
        /// <summary>
        /// BankId
        /// </summary>
        public System.Guid BankId { get; set; }
        /// <summary>
        /// BankName
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// BankAccountId
        /// </summary>
        public Guid BankAccountId { get; set; }
        /// <summary>
        /// BankAccountNo
        /// </summary>
        public string BankAccountNo { get; set; }
        /// <summary>
        /// Deposit
        /// </summary>
        public decimal Deposit { get; set; }
        /// <summary>
        /// CheckPaid
        /// </summary>
        public decimal CheckPaid { get; set; }

    }

    #endregion

    #region  BankOutStandingDetailData
    /// <summary>
    /// BankOutStandingDetailData
    /// </summary>
    [Serializable]
    public class BankOutStandingDetailData
    {
        /// <summary>
        /// CheckDate
        /// </summary>
        public DateTime CheckDate { get; set; }
        /// <summary>
        /// CheckNo
        /// </summary>
        public string CheckNo { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// CheckPaid
        /// </summary>
        public decimal CheckPaid { get; set; }
        /// <summary>
        /// Deposit
        /// </summary>
        public decimal Deposit { get; set; }
        /// <summary>
        /// BankDate
        /// </summary>
        public DateTime? BankDate { get; set; }

    }

    #endregion

    #region 日记帐详细信息

    /// <summary>
    /// 日记帐明细
    /// </summary>
    [Serializable]
    public class JournalDetailReportData
    {
        ///<summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        ///<summary>
        /// JournalID
        /// </summary>
        public Guid JournalID { get; set; }
        ///<summary>
        /// JournalPostDate
        /// </summary>
        public DateTime JournalPostDate { get; set; }
        ///<summary>
        /// GLID
        /// </summary>
        public Guid GLID { get; set; }
        ///<summary>
        /// GLDescription
        /// </summary>
        public string GLDescription { get; set; }

        ///<summary>
        /// CurrencyID
        /// </summary>
        public Guid CurrencyID { get; set; }

        ///<summary>
        /// 币种名
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal DRAmount { get; set; }

        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal CRAmount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
    #endregion

    #region 凭证列表

    /// <summary>
    /// 凭证列表
    /// </summary>
    [Serializable]
    public class VoucherLedgerData
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// GLId
        /// </summary>
        public Guid GLId { get; set; }
        /// <summary>
        /// GLCode
        /// </summary>
        public string GLCode { get; set; }
        /// <summary>
        /// CompanyID
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// GLDescription
        /// </summary>
        public string GLDescription { get; set; }
        /// <summary>
        /// MakeDate
        /// </summary>
        public DateTime MakeDate { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal BeginBalance { get; set; }
        /// <summary>
        /// CrAmt
        /// </summary>
        public decimal CrAmt { get; set; }
        /// <summary>
        /// DrAmt
        /// </summary>
        public decimal DrAmt { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// OrgAmt
        /// </summary>
        public decimal OrgAmt { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// BillType
        /// </summary>
        public short BillType { get; set; }
        /// <summary>
        /// BillId
        /// </summary>
        public Guid BillId { get; set; }
        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// CustomerFinanceCode
        /// </summary>
        public string CustomerFinanceCode { get; set; }
        /// <summary>
        /// OperationNo
        /// </summary>
        public string OperationNo { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// CustomerShortName
        /// </summary>
        public string CustomerShortName { get; set; }
        /// <summary>
        /// DepartmentName
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// EmployeeName
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// VoucherSeqNo
        /// </summary>
        public string VoucherSeqNo { get; set; }

        /// <summary>
        /// OrgAmtIsZero
        /// </summary>
        public bool OrgAmtIsZero { get; set; }
    }

    #endregion

    #region AgingReport
    /// <summary>
    /// AgingReportData
    /// </summary>
    [Serializable]
    public class AgingReportData
    {
        /// <summary>
        /// CustomerID
        /// </summary>
        public Guid CustomerID { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal PastDue { get; set; }
        /// <summary>
        /// Current
        /// </summary>
        public decimal Current { get; set; }
        /// <summary>
        /// Less30
        /// </summary>
        public decimal Less30 { get; set; }
        /// <summary>
        /// Over30
        /// </summary>
        public decimal Over30 { get; set; }
        /// <summary>
        /// Over45
        /// </summary>
        public decimal Over45 { get; set; }
        /// <summary>
        /// Over60
        /// </summary>
        public decimal Over60 { get; set; }
        /// <summary>
        /// Over90
        /// </summary>
        public decimal Over90 { get; set; }
        /// <summary>
        /// MainCurBalance
        /// </summary>
        public decimal MainCurBalance { get; set; }

        /// <summary>
        /// 信用额度
        /// </summary>
        public decimal CreditLimit { get; set; }

        /// <summary>
        /// 信用期限
        /// </summary>
        public decimal Terms { get; set; }

    }
    /// <summary>
    /// AgingReportDetailData
    /// </summary>
    [Serializable]
    public class AgingReportDetailData : AgingReportData
    {
        /// <summary>
        /// RefNo
        /// </summary>
        public string RefNo { get; set; }
        /// <summary>
        /// BillId
        /// </summary>
        public Guid BillId { get; set; }
        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// FianceDate
        /// </summary>
        public DateTime FianceDate { get; set; }
        /// <summary>
        /// DRAmt
        /// </summary>
        public decimal DRAmt { get; set; }
        /// <summary>
        /// CRAmt
        /// </summary>

        public decimal CRAmt { get; set; }
        /// <summary>
        /// DestAmount
        /// </summary>
        public decimal DestAmount { get; set; }

        /// <summary>
        /// 揽货人
        /// </summary>
        public string SalesName { get; set; }

        /// <summary>
        /// 客服
        /// </summary>
        public string CustomerServiceName { get; set; }
    }

    /// <summary>
    /// AgingReportFeeData
    /// </summary>
    [Serializable]
    public class AgingReportFeeData
    {
        /// <summary>
        /// RefNo
        /// </summary>
        public string FeeItemName { get; set; }
        /// <summary>
        /// BillNo
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// FianceDate
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// DRAmt
        /// </summary>
        public decimal DrAmt { get; set; }
        /// <summary>
        /// CRAmt
        /// </summary>
        public decimal CrAmt { get; set; }
    }


    #endregion

    #region 收支报表数据对象
    /// <summary>
    /// RepCheckData
    /// </summary>
    [Serializable]
    public class RepCheckData
    {
        /// <summary>
        /// 支票Id
        /// </summary>
        public Guid CheckID { get; set; }

        /// <summary>
        /// 根据选择的不同返回的不同
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// 根据选择的不同返回的不同
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 根据选择的不同返回的不同
        /// </summary>
        public string GroupId1 { get; set; }

        /// <summary>
        /// 根据选择的不同返回的不同
        /// </summary>
        public string GroupName1 { get; set; }

        /// <summary>
        /// 应收
        /// </summary>
        public decimal Deposit { get; set; }
        /// <summary>
        /// 应付
        /// </summary>
        public decimal CheckPaid { get; set; }

        /// <summary>
        /// 应收-应付;总计
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// DateTime
        /// </summary>
        public string DateTime { get; set; }
    }
    /// <summary>
    /// RepCheckDetailData
    /// </summary>
    [Serializable]
    public class RepCheckDetailData : RepCheckData
    {
        /// <summary>
        /// 日期,根据用户的选择,返回相应的CreateDate或者BankDate(确认到帐的时间)
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 支票号
        /// </summary>
        public string CheckNo { get; set; }

        /// <summary>
        /// 银行
        /// </summary>
        public string BankName { get; set; }
    }

    /// <summary>
    /// 加拿大Check/Deposit报表数据对象
    /// </summary>
    [Serializable]
    public class RepCACheckDepositData
    {
        /// <summary>
        /// 支票Id
        /// </summary>
        public Guid CheckID { get; set; }

        /// <summary>
        /// 时间；0：InputDate,1:BankDate;2:PaidDate
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 往来单位的Id
        /// </summary>
        public Guid BillToId { get; set; }

        /// <summary>
        /// 往来单位的名字
        /// </summary>
        public string BillToName { get; set; }

        /// <summary>
        /// 账单号
        /// </summary>
        public string BillNo { get; set; }

        /// <summary>
        /// 业务参考号
        /// </summary>
        public string OperationNo { get; set; }

        /// <summary>
        /// 账单（客户）参考号
        /// </summary>
        public string BillRefNo { get; set; }

        /// <summary>
        /// 支票号
        /// </summary>
        public string CheckNo { get; set; }

        /// <summary>
        /// 银行
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 支票金额
        /// </summary>
        public decimal CheckAmount { get; set; }
        /// <summary>
        /// 支票的核销金额
        /// </summary>
        public decimal CheckWriteOffAmt { get; set; }
        /// <summary>
        /// 支票币种
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 账单金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 账单的币种
        /// </summary>
        public string BillCurrency { get; set; }
        /// <summary>
        /// 核销的账单金额(币种与账单相同)
        /// </summary>
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// 未核销金额-币种与账单相同
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 预收预付数据对象
    /// </summary>
    [Serializable]
    public class PrepaidInAdvanceData
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? ID { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 往来单位的名字
        /// </summary>
        public Guid? CustomerID { get; set; }

        /// <summary>
        /// 核销日期
        /// </summary>
        public DateTime CheckDate { get; set; }

        /// <summary>
        /// 科目名称
        /// </summary>
        public string GLName { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
    }

    /// <summary>
    /// 预收预付余额清单数据对象
    /// </summary>
    [Serializable]
    public class GLCheckBalanceData
    {
      
        /// <summary>
        /// 单号
        /// </summary>
        public string BillNo { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public string EShortName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string BillType { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
    }

    #endregion

    #region 进口代理对帐 暂时搁置

    [Serializable]
    public class AgentStatementReportDateTotal
    {
        /// <summary>
        /// 公司信息
        /// </summary>
        public CompanyInfo CompanyInfo { get; set; }

        /// <summary>
        /// 主列表数据列表
        /// </summary>
        public List<AgentStatementReportDate> MasterDataList { get; set; }

        /// <summary>
        /// 主表&明细列表数据
        /// </summary>
        public List<AgentStatementReportMasterAndDetailData> MasterAndDetailDataList { get; set; }

    }

    [Serializable]
    public class CompanyInfo
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// 公司电话
        /// </summary>
        public string CompanyTel { get; set; }
        /// <summary>
        /// 公司传真
        /// </summary>
        public string CompanyFax { get; set; }
    }

    /// <summary>
    /// AgentStatementReportDate
    /// </summary>
    [Serializable]
    public class AgentStatementReportDate
    {
        /// <summary>
        /// BillToId
        /// </summary>
        public Guid BillToID { get; set; }
        /// <summary>
        /// BillToName
        /// </summary>
        public string BillToName { get; set; }
        /// <summary>
        /// BillToAddress
        /// </summary>
        public string BillToAddress { get; set; }
        /// <summary>
        /// BillToTelFax
        /// </summary>
        public string BillToTelFax { get; set; }
        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// OperationType
        /// </summary>
        public OperationType OperationType { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// OurRefNo
        /// </summary>
        public string OurRefNo { get; set; }
        /// <summary>
        /// YourRefNo
        /// </summary>
        public string YourRefNo { get; set; }
        /// <summary>
        /// HBLNo
        /// </summary>
        public string HBLNo { get; set; }
        /// <summary>
        /// DRAmount
        /// </summary>
        public decimal DRAmount { get; set; }
        /// <summary>
        /// CRAmount
        /// </summary>
        public decimal CRAmount { get; set; }
        /// <summary>
        /// PaidAmount
        /// </summary>
        public decimal PaidAmount { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// IsPaid
        /// </summary>
        public bool IsPaid { get; set; }
        /// <summary>
        /// BillId
        /// </summary>
        public Guid BillID { get; set; }
        /// <summary>
        /// CompanyId
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// CompanyEName
        /// </summary>
        public string CompanyEName { get; set; }
    }

    /// <summary>
    /// AgentStatementReportMasterAndDetailData
    /// </summary>
    [Serializable]
    public class AgentStatementReportMasterAndDetailData
    {
        /// <summary>
        /// CompanyId
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// CompanyEName
        /// </summary>
        public string CompanyEName { get; set; }
        /// <summary>
        /// BillToId
        /// </summary>
        public Guid BillToID { get; set; }
        /// <summary>
        /// BillToName
        /// </summary>
        public string BillToName { get; set; }
        /// <summary>
        /// BillToAddress
        /// </summary>
        public string BillToAddress { get; set; }
        /// <summary>
        /// BillToTelFax
        /// </summary>
        public string BillToTelFax { get; set; }
        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// OperationType
        /// </summary>
        public OperationType OperationType { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// OurRefNo
        /// </summary>
        public string OurRefNo { get; set; }
        /// <summary>
        /// YourRefNo
        /// </summary>
        public string YourRefNo { get; set; }
        /// <summary>
        /// HBLNo
        /// </summary>
        public string HBLNo { get; set; }
        /// <summary>
        /// DRAmount
        /// </summary>
        public decimal DRAmount { get; set; }
        /// <summary>
        /// CRAmount
        /// </summary>
        public decimal CRAmount { get; set; }
        /// <summary>
        /// PaidAmount
        /// </summary>
        public decimal PaidAmount { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// IsPaid
        /// </summary>
        public bool IsPaid { get; set; }
        /// <summary>
        /// MBLNo
        /// </summary>
        public string MBLNo { get; set; }
        /// <summary>
        /// AgentRefNo
        /// </summary>
        public string AgentRefNo { get; set; }
        /// <summary>
        /// Commodity
        /// </summary>
        public string Commodity { get; set; }
        /// <summary>
        /// POLAndETD
        /// </summary>
        public string POLAndETD { get; set; }
        /// <summary>
        /// PODAndETA
        /// </summary>
        public string PODAndETA { get; set; }
        /// <summary>
        /// FDeatAndFETA
        /// </summary>
        public string FDeatAndFETA { get; set; }
        /// <summary>
        /// KGSAndLBS
        /// </summary>
        public string KGSAndLBS { get; set; }
        /// <summary>
        /// CBMAndCFT
        /// </summary>
        public string CBMAndCFT { get; set; }
        /// <summary>
        /// VesselName
        /// </summary>
        public string VesselName { get; set; }
        /// <summary>
        /// PKGS
        /// </summary>
        public string PKGS { get; set; }
        /// <summary>
        /// FeeItemName
        /// </summary>
        public string FeeItemName { get; set; }
        /// <summary>
        /// Payment
        /// </summary>
        public string Payment { get; set; }
        /// <summary>
        /// Revenue
        /// </summary>
        public decimal Revenue { get; set; }
        /// <summary>
        /// Cost
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// BillDate
        /// </summary>
        public DateTime BillDate { get; set; }
        /// <summary>
        /// PostDate
        /// </summary>
        public DateTime PostDate { get; set; }
        /// <summary>
        /// DueDate
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// Terms
        /// </summary>
        public int Terms { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// BillId
        /// </summary>
        public Guid BillID { get; set; }
        /// <summary>
        /// BillSource
        /// </summary>
        public string BillSource { get; set; }
    }

    /// <summary>
    /// AgentStatementReportDetailDate
    /// </summary>
    [Serializable]
    public class AgentStatementReportDetailDate
    {

        /// <summary>
        /// BillId
        /// </summary>
        public Guid BillID { get; set; }
        /// <summary>
        /// CompanyId
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// CompanyEName
        /// </summary>
        public string CompanyEName { get; set; }

        /// <summary>
        /// BillToId
        /// </summary>
        public Guid BillToID { get; set; }
        /// <summary>
        /// BillToName
        /// </summary>
        public string BillToName { get; set; }
        /// <summary>
        /// BillToAddress
        /// </summary>
        public string BillToAddress { get; set; }
        /// <summary>
        /// BillToTelFax
        /// </summary>
        public string BillToTelFax { get; set; }
        /// <summary>
        /// InvoiceNo
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// OurRefNo
        /// </summary>
        public string OurRefNo { get; set; }
        /// <summary>
        /// AgentRefNo
        /// </summary>
        public string AgentRefNo { get; set; }
        /// <summary>
        /// HBLNo
        /// </summary>
        public string HBLNo { get; set; }
        /// <summary>
        /// MBLNo
        /// </summary>
        public string MBLNo { get; set; }
        /// <summary>
        /// Commodity
        /// </summary>
        public string Commodity { get; set; }
        /// <summary>
        /// POLAndETD
        /// </summary>
        public string POLAndETD { get; set; }
        /// <summary>
        /// PODAndETA
        /// </summary>
        public string PODAndETA { get; set; }
        /// <summary>
        /// FDeatAndFETA
        /// </summary>
        public string FDeatAndFETA { get; set; }
        /// <summary>
        /// KGSAndLBS
        /// </summary>
        public string KGSAndLBS { get; set; }
        /// <summary>
        /// CBMAndCFT
        /// </summary>
        public string CBMAndCFT { get; set; }
        /// <summary>
        /// VesselName
        /// </summary>
        public string VesselName { get; set; }
        /// <summary>
        /// PKGS
        /// </summary>
        public string PKGS { get; set; }
        /// <summary>
        /// FeeItemName
        /// </summary>
        public string FeeItemName { get; set; }
        /// <summary>
        /// DRAmount
        /// </summary>
        public decimal DRAmount { get; set; }
        /// <summary>
        /// CRAmount
        /// </summary>
        public decimal CRAmount { get; set; }
        /// <summary>
        /// Payment
        /// </summary>
        public string Payment { get; set; }
        /// <summary>
        /// Revenue
        /// </summary>
        public decimal Revenue { get; set; }
        /// <summary>
        /// Cost
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// BillDate
        /// </summary>
        public DateTime BillDate { get; set; }
        /// <summary>
        /// PostDate
        /// </summary>
        public DateTime PostDate { get; set; }
        /// <summary>
        /// DueDate
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// Terms
        /// </summary>
        public int Terms { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// BillSource
        /// </summary>
        public string BillSource { get; set; }
    }


    /// <summary>
    /// AgentStatementDateTypeEnum //BillDate =0 ,ETD =1
    /// </summary>
    public enum AgentStatementDateTypeEnum
    {
        BillDate = 0,
        ETD = 1
    }

    /// <summary>
    /// 代理对帐单的排序字段枚举  ETD =0,InvoiceDate =1,OurRefNo=2,AgentName=3,RefNo=4,MBLNo=5
    /// </summary>
    public enum AgentStatementSortByEnum
    {
        ETD = 0,
        InvoiceDate = 1,
        OurRefNo = 2,
        AgentName = 3,
        RefNo = 4,
        MBLNo = 5
    }


    /// <summary>
    /// AgentStatementBillStateEnum //All =0 ,Open =1,Paid=2
    /// </summary>
    public enum AgentStatementBillStateEnum
    {
        All = 0,
        Open = 1,
        Paid = 2
    }
    #endregion

    #region 本地代理对帐

    /// <summary>
    /// LocalStatementReportData
    /// </summary>
    [Serializable]
    public class LocalStatementReportData
    {
        /// <summary>
        /// BillToId
        /// </summary>
        public Guid BillToId { get; set; }
        /// <summary>
        /// BillToName
        /// </summary>
        public string BillToName { get; set; }
        /// <summary>
        /// BillToAddress
        /// </summary>
        public string BillToAddress { get; set; }
        /// <summary>
        /// BillToTelFax
        /// </summary>
        public string BillToTelFax { get; set; }
        /// <summary>
        /// InvoiceDate
        /// </summary>
        public DateTime InvoiceDate { get; set; }
        /// <summary>
        /// InvoiceNo
        /// </summary>
        public string InvoiceNo { get; set; }
        /// <summary>
        /// RefNo
        /// </summary>
        public string RefNo { get; set; }
        /// <summary>
        /// HBLNo
        /// </summary>
        public string HBLNo { get; set; }
        /// <summary>
        /// CustomerRefNo
        /// </summary>
        public string CustomerRefNo { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// PaidAmount
        /// </summary>
        public decimal PaidAmount { get; set; }
        /// <summary>
        /// PayAmount
        /// </summary>
        public decimal PayAmount { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// CheckNo
        /// </summary>
        public string CheckNo { get; set; }
        /// <summary>
        /// BillToCode
        /// </summary>
        public string BillToCode { get; set; }
        /// <summary>
        /// DuDate
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// ContainerNO
        /// </summary>
        public string ContainerNO { get; set; }
        /// <summary>
        /// BillID
        /// </summary>
        public Guid BillID { get; set; }

        /// <summary>
        /// ETD
        /// </summary>
        public DateTime ETD { get; set; }

        /// <summary>
        /// ETA
        /// </summary>
        public DateTime ETA { get; set; }
    }
    /// <summary>
    /// LocalStatementReportDetailData
    /// </summary>
    [Serializable]
    public class LocalStatementReportDetailData
    {
        /// <summary>
        /// BillToId
        /// </summary>
        public Guid BillToId { get; set; }
        /// <summary>
        /// BillToName
        /// </summary>
        public string BillToName { get; set; }
        /// <summary>
        /// BillToAddress
        /// </summary>
        public string BillToAddress { get; set; }
        /// <summary>
        /// BillToTelFax
        /// </summary>
        public string BillToTelFax { get; set; }
        /// <summary>
        /// BillToCode
        /// </summary>
        public string BillToCode { get; set; }
        /// <summary>
        /// BillToAttn
        /// </summary>
        public string BillToAttn { get; set; }
        /// <summary>
        /// MBLNo
        /// </summary>
        public string MBLNo { get; set; }
        /// <summary>
        /// HBLNo
        /// </summary>
        public string HBLNo { get; set; }
        /// <summary>
        /// InvoiceNo
        /// </summary>
        public string InvoiceNo { get; set; }
        /// <summary>
        /// InvoiceDate
        /// </summary>
        public DateTime InvoiceDate { get; set; }
        /// <summary>
        /// RefNo
        /// </summary>
        public string RefNo { get; set; }
        /// <summary>
        /// CustomerRefNo
        /// </summary>
        public string CustomerRefNo { get; set; }
        /// <summary>
        /// Shipper
        /// </summary>
        public string Shipper { get; set; }
        /// <summary>
        /// Consignee
        /// </summary>
        public string Consignee { get; set; }
        /// <summary>
        /// Notify
        /// </summary>
        public string Notify { get; set; }
        /// <summary>
        /// Carrier
        /// </summary>
        public string Carrier { get; set; }
        /// <summary>
        /// VesselNo
        /// </summary>
        public string VesselNo { get; set; }
        /// <summary>
        /// POLAndETD
        /// </summary>
        public string POLAndETD { get; set; }
        /// <summary>
        /// PODAndETA
        /// </summary>
        public string PODAndETA { get; set; }
        /// <summary>
        /// FDestAndETA
        /// </summary>
        public string FDestAndETA { get; set; }
        /// <summary>
        /// Commodity
        /// </summary>
        public string Commodity { get; set; }
        /// <summary>
        /// PKGS
        /// </summary>
        public string PKGS { get; set; }
        /// <summary>
        /// KGSAndLBS
        /// </summary>
        public string KGSAndLBS { get; set; }
        /// <summary>
        /// CBMAndCFT
        /// </summary>
        public string CBMAndCFT { get; set; }
        /// <summary>
        /// CarrierBook
        /// </summary>
        public string CarrierBook { get; set; }
        /// <summary>
        /// FeeItemName
        /// </summary>
        public string FeeItemName { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// Qty
        /// </summary>
        public decimal Qty { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// DueDate
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// Terms
        /// </summary>
        public string Terms { get; set; }
        /// <summary>
        /// BillId
        /// </summary>
        public Guid BillId { get; set; }
        /// <summary>
        /// TotalAmount
        /// </summary>
        public string TotalAmount { get; set; }
        /// <summary>
        /// PaidAmount
        /// </summary>
        public string PaidAmount { get; set; }
        /// <summary>
        /// FinAmount
        /// </summary>
        public string FinAmount { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// PublicMemo
        /// </summary>
        public string PublicMemo { get; set; }
        /// <summary>
        /// ShipToName
        /// </summary>
        public string ShipToName { get; set; }
        /// <summary>
        /// ShipToAddress
        /// </summary>
        public string ShipToAddress { get; set; }
        /// <summary>
        /// ShipToTelFax
        /// </summary>
        public string ShipToTelFax { get; set; }
        /// <summary>
        /// ContainerNo
        /// </summary>
        public string ContainerNo { get; set; }

        /// <summary>
        /// CompanyName
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// CompanyAddress
        /// </summary>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// CompanyTel
        /// </summary>
        public string CompanyTel { get; set; }
        /// <summary>
        /// CompanyFax
        /// </summary>
        public string CompanyFax { get; set; }

    }


    /// <summary>
    /// 代理对帐单的排序字段枚举  ETD =0,InvoiceDate =1
    /// </summary>
    public enum StatementOrderByEnum
    {
        /// <summary>
        /// ETD
        /// </summary>
        ETD = 0,
        /// <summary>
        /// InvoiceDate
        /// </summary>
        InvoiceDate = 1
    }

    public enum LocalStatementOrderByEnum
    {
        /// <summary>
        /// BillDate(InvoiceDate)
        /// </summary>
        BillDate = 0,
        /// <summary>
        /// Due Date
        /// </summary>
        DueDate = 1
    }


    /// <summary>
    /// 代理对帐单的排序字段枚举  All =0,1:Open未完全付;2:paid已付了一部分
    /// </summary>
    public enum StatementBillStateEnum
    {
        /// <summary>
        /// All
        /// </summary>
        All = 0,
        /// <summary>
        /// Open
        /// </summary>
        Open = 1,
        /// <summary>
        /// Paid
        /// </summary>
        Paid = 2
    }
    #endregion
}
