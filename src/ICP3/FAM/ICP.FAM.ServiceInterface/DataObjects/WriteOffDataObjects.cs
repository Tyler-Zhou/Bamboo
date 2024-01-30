using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.ServiceInterface.DataObjects
{

    #region 销账编辑界面--账单/费用

    /// <summary>
    /// 销账单编辑界面上“账单/费用”列表用到的实体
    /// </summary>
    [Serializable]
    public class WriteOffBill : BaseDataObject
    {
        #region ID
        public Guid ID
        {
            get;
            set;
        }
        #endregion

        bool selected;
        /// <summary>
        /// 选择
        /// </summary>
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                if (selected != value)
                {
                    selected = value;
                    this.NotifyPropertyChanged(o => o.Selected);
                }
            }
        }

        #region 费用

        Guid chargeID;
        /// <summary>
        /// 业务费用ID
        /// </summary>
        public Guid ChargeID
        {
            get
            {
                return chargeID;
            }
            set
            {
                if (chargeID != value)
                {
                    chargeID = value;
                    this.NotifyPropertyChanged(o => o.ChargeID);
                }
            }
        }
        private Guid chargingCodeID;
        /// <summary>
        /// 费用代码ID
        /// </summary>
        public Guid ChargingCodeID
        {
            get
            {
                return chargingCodeID;
            }
            set
            {
                if (chargingCodeID != value)
                {
                    chargingCodeID = value;
                    this.NotifyPropertyChanged(o => o.ChargingCodeID);
                }
            }
        }

        string chargeName;
        /// <summary>
        /// 费用名称
        /// 如果是“账单模式”，则为空
        /// </summary>
        public string ChargeName
        {
            get
            {
                return chargeName;
            }
            set
            {
                if (chargeName != value)
                {
                    chargeName = value;
                    this.NotifyPropertyChanged(o => o.ChargeName);
                }
            }
        }
        #endregion

        #region 账单ID

        Guid billID;
        /// <summary>
        /// 账单ID
        /// </summary>
        public Guid BillID
        {
            get
            {
                return billID;
            }
            set
            {
                if (billID != value)
                {
                    billID = value;
                    this.NotifyPropertyChanged(o => o.BillID);
                }
            }
        }

        #endregion

        #region 账单号
        string billNo;
        /// <summary>
        /// 账单号
        /// </summary>
        public string BillNo
        {
            get
            {
                return billNo;
            }
            set
            {
                if (billNo != value)
                {
                    billNo = value;
                    this.NotifyPropertyChanged(o => o.BillNo);
                }
            }
        }
        #endregion

        #region 币种
        Guid currencyID;
        /// <summary>
        /// 币种ID
        /// </summary>
        public Guid CurrencyID
        {
            get
            {
                return currencyID;
            }
            set
            {
                if (currencyID != value)
                {
                    currencyID = value;
                    this.NotifyPropertyChanged(o => o.CurrencyID);
                }
            }
        }

        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyName { get; set; }
        #endregion

        #region 费用金额
        Decimal chargeAmount;
        /// <summary>
        /// 费用金额
        /// </summary>
        public Decimal ChargeAmount
        {
            get
            {
                return chargeAmount;
            }
            set
            {
                if (chargeAmount != value)
                {
                    chargeAmount = value;
                    this.NotifyPropertyChanged(o => o.ChargeAmount);
                }
            }
        }
        #endregion

        #region 金额
        Decimal amount;
        /// <summary>
        /// 金额
        /// </summary>
        public Decimal Amount
        {
            get
            {
                return amount;
            }
            set
            {
                if (amount != value)
                {
                    amount = value;
                    this.NotifyPropertyChanged(o => o.Amount);
                }
            }
        }

        #endregion

        #region 帐单金额
        Decimal _billAmount;
        /// <summary>
        /// 金额
        /// </summary>
        public Decimal BillAmount
        {
            get
            {
                return _billAmount;
            }
            set
            {
                if (_billAmount != value)
                {
                    _billAmount = value;
                    this.NotifyPropertyChanged(o => o.BillAmount);
                }
            }
        }

        #endregion

        #region 可核销金额
        Decimal availbeWriteOffAmount;
        /// <summary>
        /// 可核销金额
        /// </summary>
        public Decimal AvailbeWriteOffAmount
        {
            get
            {
                return availbeWriteOffAmount;
            }
            set
            {
                if (availbeWriteOffAmount != value)
                {
                    availbeWriteOffAmount = value;
                    this.NotifyPropertyChanged(o => o.AvailbeWriteOffAmount);
                }
            }

        }

        /// <summary>
        /// 剩下的核销金额 =  可核销金额 - 本次核销金额
        /// </summary>
        public Decimal RemainedWriteOffAmount
        {
            get
            {
                return AvailbeWriteOffAmount - WriteOffAmount;
            }
        }


        #endregion

        #region 核销金额
        Decimal writeOffAmount;
        /// <summary>
        /// 核销金额
        /// </summary>
        public Decimal WriteOffAmount
        {
            get
            {
                return writeOffAmount;
            }
            set
            {
                if (writeOffAmount != value)
                {
                    writeOffAmount = value;
                    this.NotifyPropertyChanged(o => o.WriteOffAmount);
                }
            }
        }

        #endregion

        #region 汇率

        Decimal exchangeRate;
        /// <summary>
        /// 汇率
        /// </summary>
        [Required(CMessage = "汇率必须大于0", EMessage = "The Rate must more than zero", IsUseErrorTemplate = false)]
        public Decimal ExchangeRate
        {
            get
            {
                return exchangeRate;
            }
            set
            {
                if (exchangeRate != value)
                {
                    exchangeRate = value;
                    this.NotifyPropertyChanged(o => o.ExchangeRate);
                }
            }
        }
        #endregion

        #region 核销金额
        Decimal finalAmount;
        /// <summary>
        /// 核销金额(折合)--已无效
        /// </summary>
        public Decimal FinalAmount
        {
            get
            {
                return finalAmount;
            }
            set
            {
                if (finalAmount != value)
                {
                    finalAmount = value;
                    this.NotifyPropertyChanged(o => o.FinalAmount);
                }
            }
        }
        #endregion

        #region 本位币
        /// <summary>
        /// 本们币汇率
        /// </summary>
        public decimal StandardCurrencyRate
        {
            get;
            set;
        }
        /// <summary>
        /// 核销金额(本位币)
        /// </summary>
        public decimal StandardCurrencyAmount
        {
            get;
            set;
        }
        #endregion

        #region 业务号
        string refNo;
        /// <summary>
        /// 业务号
        /// </summary>
        public string RefNo
        {
            get
            {
                return refNo;
            }
            set
            {
                if (refNo != value)
                {
                    refNo = value;
                    this.NotifyPropertyChanged(o => o.RefNo);
                }
            }
        }
        #endregion

        #region 方向
        public FeeWay Way
        {
            get;
            set;
        }
        #endregion

        #region 是否为佣金
        /// <summary>
        /// 是否为佣金
        /// </summary>
        public bool IsCommission
        {
            get;
            set;
        }
        #endregion

        #region 更新时间
        private DateTime? updateDate;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return updateDate;
            }
            set
            {
                if (updateDate != value)
                {
                    updateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }

        #endregion

        #region 费用更新时间
        private DateTime? chargeUpdateDate;
        /// <summary>
        /// 费用更新时间
        /// </summary>
        public DateTime? ChargeUpdateDate
        {
            get
            {
                return chargeUpdateDate;
            }
            set
            {
                if (chargeUpdateDate != value)
                {
                    chargeUpdateDate = value;
                    this.NotifyPropertyChanged(o => o.ChargeUpdateDate);
                }
            }
        }

        private DateTime? billUpdateDate;
        /// <summary>
        /// 账单更新时间
        /// </summary>
        public DateTime? BillUpdateDate
        {
            get
            {
                return billUpdateDate;
            }
            set
            {
                if (billUpdateDate != value)
                {
                    billUpdateDate = value;
                    this.NotifyPropertyChanged(o => o.BillUpdateDate);
                }
            }
        }
        #endregion

        #region 预收预付信息
        /// <summary>
        /// 预收/预付 币种
        /// </summary>
        public Guid? PreCurrencyID
        {
            get;
            set;
        }
        /// <summary>
        /// 预收预付ID
        /// </summary>
        public Guid? PrepaymentID
        {
            get;
            set;
        }
        /// <summary>
        /// 预收/预付 金额
        /// </summary>
        public Decimal? PreAmount
        {
            get;
            set;
        }
        #endregion
    }

    #endregion

    #region 销账编辑界面--其它项目
    /// <summary>
    /// 销账单编辑界面的财务费用明细
    /// </summary>
    [Serializable]
    public class WriteOffCharge : BaseDataObject
    {
        #region ID

        Guid _id;
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }

        #endregion

        #region
        /// <summary>
        /// 销帐单ID
        /// </summary>
        public Guid CheckID
        {
            get;
            set;
        }

        #endregion

        #region 方向
        #region FeeWay

        FeeWay _way;
        /// <summary>
        /// 方向（0:应收,1:应付）
        /// </summary>
        public FeeWay Way
        {
            get
            {
                return _way;
            }
            set
            {
                if (_way != value)
                {
                    _way = value;
                    this.NotifyPropertyChanged(o => o.Way);
                }
            }
        }

        #endregion
        #endregion

        #region 账单号
        string billNo;
        /// <summary>
        /// 账单号
        /// </summary>
        public string BillNo
        {
            get
            {
                return billNo;
            }
            set
            {
                if (billNo != value)
                {
                    billNo = value;
                    this.NotifyPropertyChanged(o => o.BillNo);
                }
            }
        }
        #endregion

        #region 客户
        Guid? _customerID;
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid? CustomerID
        {
            get
            {
                return _customerID;
            }
            set
            {
                if (_customerID != value)
                {
                    _customerID = value;
                    this.NotifyPropertyChanged(o => o.CustomerID);
                }
            }
        }

        string _customerName;
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get
            {
                return _customerName;
            }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    this.NotifyPropertyChanged(o => o.CustomerName);
                }
            }
        }


        #endregion

        #region 会计科目

        Guid glID;
        /// <summary>
        /// 会计科目ID
        /// </summary>
        [GuidRequired(CMessage = "会计科目", EMessage = "GL")]
        public Guid GLID
        {
            get
            {
                return glID;
            }
            set
            {
                if (glID != value)
                {
                    glID = value;
                    this.NotifyPropertyChanged(o => o.GLID);
                }
            }
        }


        string glDescription;
        /// <summary>
        /// 会计科目名称
        /// </summary>
        public string GLDescription
        {
            get
            {
                return glDescription;
            }
            set
            {
                if (glDescription != value)
                {
                    glDescription = value;
                    this.NotifyPropertyChanged(o => o.GLDescription);
                }
            }
        }

        public string GLFullName
        {
            get;
            set;
        }
        public Guid? ForeignCurrencyID
        {
            get;
            set;
        }
        #endregion

        #region 币种
        Guid _currencyID;
        /// <summary>
        /// 币种ID
        /// </summary>
        [Required(CMessage = "币种", EMessage = "Currency")]
        public Guid CurrencyID
        {
            get
            {
                return _currencyID;
            }
            set
            {
                if (_currencyID != value)
                {
                    _currencyID = value;
                    this.NotifyPropertyChanged(o => o.CurrencyID);
                }
            }

        }
        string _currencyName;
        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyName
        {
            get
            {
                return _currencyName;
            }
            set
            {
                if (_currencyName != value)
                {
                    _currencyName = value;
                    this.NotifyPropertyChanged(o => o.CurrencyName);
                }
            }
        }
        #endregion

        #region 金额
        decimal _amount;
        /// <summary>
        /// 金额
        /// </summary>
        [Required(CMessage = "金额必须大于0", EMessage = "Amount must more than zero", IsUseErrorTemplate = false)]
        public decimal Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    this.NotifyPropertyChanged(o => o.Amount);
                }
            }
        }
        #endregion

        #region 本位币
        /// <summary>
        /// 本位币汇率
        /// </summary>
        public decimal StandardCurrencyRate
        {
            get;
            set;
        }
        /// <summary>
        /// 金额(本位币)
        /// </summary>
        public decimal StandardCurrencyAmount
        {
            get;
            set;
        }
        #endregion

        #region 汇率
        decimal _exchangeRate;
        /// <summary>
        /// 汇率
        /// </summary>
        [Required(CMessage = "汇率必须大于0", EMessage = "The ExChangeRate must more than zero", IsUseErrorTemplate = false)]
        public decimal ExchangeRate
        {
            get
            {
                return _exchangeRate;
            }
            set
            {
                if (_exchangeRate != value)
                {
                    _exchangeRate = value;
                    this.NotifyPropertyChanged(o => o.ExchangeRate);
                }
            }
        }

        #endregion

        #region 更新时间
        private DateTime? updateDate;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return updateDate;
            }
            set
            {
                if (updateDate != value)
                {
                    updateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }

        #endregion

        #region 备注
        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                if (_remark != value)
                {
                    _remark = value;
                    this.NotifyPropertyChanged(o => o.Remark);
                }
            }
        }

        #endregion

        #region 参考号
        /// <summary>
        /// 参考号
        /// </summary>
        public Guid? RefID
        {
            get;
            set;
        }

        public Boolean? IsWriteOff
        {
            get;
            set;
        }
        #endregion

    }

    #endregion

    #region 销账编辑界面--币种信息
    /// <summary>
    /// 销帐币种金额列表
    /// </summary>
    [Serializable]
    public class OperationCurrencyAmountList : BaseDataObject
    {
        #region ID
        Guid id;
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }

        #endregion

        #region 币种

        Guid currencyID;
        /// <summary>
        /// 币种ID
        /// </summary>
        [Required(CMessage = "币种", EMessage = "Currency")]
        public Guid CurrencyID
        {
            get
            {
                return currencyID;
            }
            set
            {
                if (currencyID != value)
                {
                    currencyID = value;
                    this.NotifyPropertyChanged(o => o.CurrencyID);
                }
            }
        }

        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyName
        {
            get;
            set;
        }

        #region 原币种
        Guid _sourceCurrencyID;
        /// <summary>
        /// 原币种ID
        /// </summary>     
        public Guid SourceCurrencyID
        {
            get
            {
                return _sourceCurrencyID;
            }
            set
            {
                if (_sourceCurrencyID != value)
                {
                    _sourceCurrencyID = value;
                    this.NotifyPropertyChanged(o => o.SourceCurrencyID);
                }
            }
        }
        #endregion

        #endregion

        #region 总金额
        decimal totalAmount;
        /// <summary>
        /// 金额
        /// </summary>
        public decimal TotalAmount
        {
            get
            {
                return totalAmount;
            }
            set
            {
                if (totalAmount != value)
                {
                    totalAmount = value;
                    this.NotifyPropertyChanged(o => o.TotalAmount);
                }
            }
        }
        /// <summary>
        /// 本位币金额
        /// </summary>
        public decimal StandardCurrencyAmount
        {
            get;
            set;
        }
        #endregion

        #region 银行
        Guid bankAccountID;
        /// <summary>
        /// 银行账号ID
        /// </summary>
        [GuidRequired(CMessage = "银行", EMessage = "BankAccount")]
        public Guid BankAccountID
        {
            get
            {
                return bankAccountID;
            }
            set
            {
                if (bankAccountID != value)
                {
                    bankAccountID = value;
                    this.NotifyPropertyChanged(o => o.BankAccountID);
                }
            }
        }
        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName
        {
            get;
            set;
        }
        #endregion

        #region 凭证号
        /// <summary>
        /// 凭证号
        /// </summary>
        public String VoucherSeqNo
        {
            get;
            set;
        }
        #endregion

        #region 到账&到账时间
        private bool isReached;
        /// <summary>
        /// 是否到账
        /// </summary>
        public bool IsReached
        {
            get
            {
                return isReached;
            }
            set
            {
                if (isReached != value)
                {
                    isReached = value;
                    this.NotifyPropertyChanged(o => o.IsReached);
                }
            }
        }

        Guid? bankByID;
        /// <summary>
        /// 到账人
        /// </summary>
        public Guid? BankByID
        {
            get
            {
                return bankByID;
            }
            set
            {
                if (bankByID != value)
                {
                    bankByID = value;
                    this.NotifyPropertyChanged(o => o.BankByID);
                }
            }
        }

        private DateTime? bankDate;
        /// <summary>
        /// 到账时间
        /// </summary>
        public DateTime? BankDate
        {
            get
            {
                return bankDate;
            }
            set
            {
                if (bankDate != value)
                {
                    bankDate = value;
                    this.NotifyPropertyChanged(o => o.BankDate);
                }
            }
        }
        #endregion

        #region 账单金额合计
        decimal totalBillAmount;
        /// <summary>
        /// 账单金额合计
        /// </summary>
        public decimal TotalBillAmount
        {
            get
            {
                return totalBillAmount;
            }
            set
            {
                if (totalBillAmount != value)
                {
                    totalBillAmount = value;
                }
            }
        }

        /// <summary>
        /// 账单金额(本位币)
        /// </summary>
        public decimal StandardCurrencyBillAmount
        {
            get;
            set;
        }
        #endregion

        #region 其它金额合计
        decimal totalOtherAmount;
        /// <summary>
        /// 其它金额合计
        /// </summary>
        public decimal TotalOtherAmount
        {
            get
            {
                return totalOtherAmount;
            }
            set
            {
                if (totalOtherAmount != value)
                {
                    totalOtherAmount = value;
                    this.NotifyPropertyChanged(o => o.TotalOtherAmount);
                }
            }
        }
        /// <summary>
        /// 其它费用合计(本位币)
        /// </summary>
        public decimal StandardCurrencyOtherAmount
        {
            get;
            set;
        }
        #endregion

        #region 是否支持银企直连
        /// <summary>
        /// 是否支持银企直连
        /// </summary>
        public bool IsSupportDirectBank
        {
            get;
            set;
        } 
        #endregion

        #region 银行流水ID
        Guid? bankTransactionID;
        /// <summary>
        /// 银行流水ID
        /// </summary>
        public Guid? BankTransactionID
        {
            get
            {
                return bankTransactionID;
            }
            set
            {
                if (bankTransactionID != value)
                {
                    bankTransactionID = value;
                    this.NotifyPropertyChanged(o => o.BankTransactionID);
                }
            }
        }
        #endregion

        #region 银行流水号码
        string bankTransactionNO;
        /// <summary>
        /// 银行流水号码
        /// </summary>
        public string BankTransactionNO
        {
            get
            {
                return bankTransactionNO;
            }
            set
            {
                if (bankTransactionNO != value)
                {
                    bankTransactionNO = value;
                    this.NotifyPropertyChanged(o => o.BankTransactionNO);
                }
            }
        }
        #endregion

        #region 关联类型
        BTAWType associationtype;
        /// <summary>
        /// 关联类型
        /// </summary>
        public BTAWType AssociationType
        {
            get
            {
                return associationtype;
            }
            set
            {
                if (associationtype != value)
                {
                    associationtype = value;
                    this.NotifyPropertyChanged(o => o.AssociationType);
                }
            }
        }
        #endregion

        #region 更新时间
        private DateTime? updateDate;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return updateDate;
            }
            set
            {
                if (updateDate != value)
                {
                    updateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }

        #endregion

    }
    #endregion

    #region 销账编辑界面--销账信息
    /// <summary>
    /// 销账列表上用到的实体
    /// 创建时间：2011-07-11 15:52
    /// 作者：熊中方
    /// </summary>
    [Serializable]
    public class WriteOffItemInfo : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }
        #region  ID

        Guid _id;

        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }
        #endregion

        #region 单号
        string _no;

        /// <summary>
        /// 单号
        /// </summary>
        public string No
        {
            get { return _no; }
            set
            {
                if (_no != value)
                {
                    _no = value;
                    this.NotifyPropertyChanged(o => o.No);
                }
            }
        }

        #endregion

        #region  方向
        /// <summary>
        /// Way
        /// </summary>
        public FeeWay Way { get; set; }

        #endregion

        #region 公司
        Guid _companyID;
        /// <summary>
        /// 公司ID
        /// </summary>
        [GuidRequired(CMessage = "公司", EMessage = "Company")]
        public Guid CompanyID
        {
            get { return _companyID; }
            set
            {
                if (_companyID != value)
                {
                    _companyID = value;
                    this.NotifyPropertyChanged(o => o.CompanyID);
                }
            }
        }

        string _companyName;

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                if (_companyName != value)
                {
                    _companyName = value;
                    this.NotifyPropertyChanged(o => o.CompanyName);
                }
            }
        }

        #endregion

        #region 支票号
        string _checkNo;

        /// <summary>
        /// 支票号
        /// </summary>
        public string CheckNo
        {
            get { return _checkNo; }
            set
            {
                if (_checkNo != value)
                {
                    _checkNo = value;
                    this.NotifyPropertyChanged(o => o.CheckNo);
                }
            }
        }

        #endregion

        #region 发票号
        string invoiceNo;
        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNo
        {
            get
            {
                return invoiceNo;
            }
            set
            {
                if (invoiceNo != value)
                {
                    invoiceNo = value;
                    this.NotifyPropertyChanged(o => o.InvoiceNo);
                }
            }
        }
        #endregion

        #region 银行水单ID
        Guid bankReceiptID;
        /// <summary>
        /// 银行水单ID
        /// </summary>
        public Guid BankReceiptID
        {
            get
            {
                return bankReceiptID;
            }
            set
            {
                if (bankReceiptID != value)
                {
                    bankReceiptID = value;
                    this.NotifyPropertyChanged(o => o.BankReceiptID);
                }
            }
        }
        #endregion

        #region 银行水单号码
        string bankReceiptNO;
        /// <summary>
        /// 银行水单号码
        /// </summary>
        public string BankReceiptNO
        {
            get
            {
                return bankReceiptNO;
            }
            set
            {
                if (bankReceiptNO != value)
                {
                    bankReceiptNO = value;
                    this.NotifyPropertyChanged(o => o.BankReceiptNO);
                }
            }
        }
        #endregion

        #region 客户
        Guid _customerID;
        /// <summary>
        /// 客户ID
        /// </summary>
        [GuidRequired(CMessage = "客户", EMessage = "Customer")]
        public Guid CustomerID
        {
            get
            {
                return _customerID;
            }
            set
            {
                if (_customerID != value)
                {
                    _customerID = value;
                    this.NotifyPropertyChanged(o => o.CustomerID);
                }
            }
        }

        string _customerName;

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    this.NotifyPropertyChanged(o => o.CustomerName);
                }
            }
        }
        #endregion

        #region 实际收付款人
        Guid _payCustomerID;
        /// <summary>
        /// 实际收付款人 客户ID
        /// </summary>
        public Guid PayCustomerID
        {
            get
            {
                return _payCustomerID;
            }
            set
            {
                if (_payCustomerID != value)
                {
                    _payCustomerID = value;
                    this.NotifyPropertyChanged(o => o.PayCustomerID);
                }
            }
        }

        string _paycustomerName;

        /// <summary>
        /// 实际收付款人 客户名称
        /// </summary>
        public string PayCustomerName
        {
            get { return _paycustomerName; }
            set
            {
                if (_paycustomerName != value)
                {
                    _paycustomerName = value;
                    this.NotifyPropertyChanged(o => o.PayCustomerName);
                }
            }
        }
        #endregion

        #region 实际收付款银行账号
        string _paybankaccountno;
        /// <summary>
        /// 实际收付款银行账号
        /// </summary>
        public string PayBankAccountNo
        {
            get { return _paybankaccountno; }
            set
            {
                if (_paybankaccountno != value)
                {
                    _paybankaccountno = value;
                    this.NotifyPropertyChanged(o => o.PayBankAccountNo);
                }
            }
        }
        #endregion

        #region 实际收付款银开户行
        string _paybankname;

        /// <summary>
        /// 实际收付款银开户行
        /// </summary>
        public string PayBankName
        {
            get { return _paybankname; }
            set
            {
                if (_paybankname != value)
                {
                    _paybankname = value;
                    this.NotifyPropertyChanged(o => o.PayBankName);
                }
            }
        }
        #endregion

        #region 实际收付款银支行
        string _paybankbranchname;

        /// <summary>
        /// 实际收付款银行支行
        /// </summary>
        public string PayBankBranchName
        {
            get { return _paybankbranchname; }
            set
            {
                if (_paybankbranchname != value)
                {
                    _paybankbranchname = value;
                    this.NotifyPropertyChanged(o => o.PayBankBranchName);
                }
            }
        }
        #endregion

        #region 实际收付款银行号(行联号)
        string _paybanknumber;

        /// <summary>
        /// 实际收付款银行号(行联号)
        /// </summary>
        public string PayBankNumber
        {
            get { return _paybanknumber; }
            set
            {
                if (_paybanknumber != value)
                {
                    _paybanknumber = value;
                    this.NotifyPropertyChanged(o => o.PayBankNumber);
                }
            }
        }
        #endregion

        #region 销账日期

        DateTime checkDate;
        /// <summary>
        /// 核销日期
        /// </summary>
        [Required(CMessage = "核销日期", EMessage = "CheckDate")]
        public DateTime CheckDate
        {
            get { return checkDate; }
            set
            {
                if (checkDate != value)
                {
                    checkDate = value;
                    this.NotifyPropertyChanged(o => o.CheckDate);
                }
            }
        }
        #endregion

        #region 最后更新时间
        DateTime? updateDate;
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return updateDate;
            }
            set
            {
                if (updateDate != value)
                {
                    updateDate = value;
                    this.NotifyPropertyChanged(o => o.updateDate);
                }
            }




        }
        #endregion

        #region 有效性
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            get;
            set;
        }
        #endregion

        #region 备注
        string _remark;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set
            {
                if (_remark != value)
                {
                    _remark = value;
                    this.NotifyPropertyChanged(o => o.Remark);
                }
            }
        }
        #endregion

        #region 是否多币种
        /// <summary>
        /// 是否多币种
        /// </summary>
        public bool IsMultCurrency
        {
            get;
            set;
        }
        #endregion

        #region 审核人
        /// <summary>
        /// 审核人ID
        /// </summary>
        public Guid? AuditorID
        {
            get;
            set;
        }
        #endregion

        #region 创建人
        Guid _createdID;
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid CreateID
        {
            get
            {
                return _createdID;
            }
            set
            {
                if (_createdID != value)
                {
                    _createdID = value;
                    base.OnPropertyChanged("CreateID", value);
                }
            }
        }

        string createByName;
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateByName
        {
            get
            {
                return createByName;
            }
            set
            {
                if (createByName != value)
                {
                    createByName = value;
                    base.OnPropertyChanged("CreateByName", value);
                }
            }
        }

        #endregion

        #region 销帐模式
        CheckMode _checkmode;
        /// <summary>
        /// 销帐模式
        /// </summary>
        public CheckMode CheckMode
        {
            get
            {
                return _checkmode;
            }
            set
            {
                if (_checkmode != value)
                {
                    _checkmode = value;
                    this.NotifyPropertyChanged(o => o.CheckMode);
                }
            }
        }
        #endregion

        #region 是否共享
        bool _IsPublic;
        /// <summary>
        /// 是否共享
        /// </summary>
        public bool IsPublic
        {
            get
            {
                return _IsPublic;
            }
            set
            {
                if (_IsPublic != value)
                {
                    _IsPublic = value;
                    this.NotifyPropertyChanged(o => o.IsPublic);
                }
            }
        }
        #endregion
    }
    #endregion

    #region 销账列表界面
    /// <summary>
    /// 销账列表上用到的实体
    /// 创建时间：2011-07-11 15:52
    /// 作者：熊中方
    /// </summary>
    [Serializable]
    public class WriteOffItemList : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        #region  ID

        Guid _id;

        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }
        #endregion

        #region 销账ID
        public Guid CheckID
        {
            get;
            set;
        }
        #endregion

        #region  方向
        /// <summary>
        /// 方向
        /// </summary>
        public FeeWay Type { get; set; }

        #endregion

        #region 是否多币种
        public bool IsMultCurrency
        {
            get;
            set;
        }
        #endregion

        #region 选择
        /// <summary>
        /// 选择
        /// </summary>
        public bool IsCheck
        {
            get;
            set;
        }

        #endregion

        #region 单号
        string _no;

        /// <summary>
        /// 单号
        /// </summary>
        public string No
        {
            get { return _no; }
            set
            {
                if (_no != value)
                {
                    _no = value;
                    this.NotifyPropertyChanged(o => o.No);
                }
            }
        }
        #endregion

        #region 支票号
        string _checkNo;
        /// <summary>
        /// 支票号
        /// </summary>
        public string CheckNo
        {
            get { return _checkNo; }
            set
            {
                if (_checkNo != value)
                {
                    _checkNo = value;
                    this.NotifyPropertyChanged(o => o.CheckNo);
                }
            }
        }
        #endregion

        #region 公司ID
        Guid _companyID;
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID
        {
            get { return _companyID; }
            set
            {
                if (_companyID != value)
                {
                    _companyID = value;
                    this.NotifyPropertyChanged(o => o.CompanyID);
                }
            }
        }
        #endregion

        #region 客户

        Guid _customerID;
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID
        {
            get
            {
                return _customerID;
            }
            set
            {
                if (_customerID != value)
                {
                    _customerID = value;
                    this.NotifyPropertyChanged(o => o.CustomerID);
                }
            }
        }

        string _customerName;

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    this.NotifyPropertyChanged(o => o.CustomerName);
                }
            }
        }
        #endregion

        #region 银行账号
        Guid? _bankAccountID;

        /// <summary>
        /// 银行账号ID
        /// </summary>
        public Guid? BankAccountID
        {
            get { return _bankAccountID; }
            set
            {
                if (_bankAccountID != value)
                {
                    _bankAccountID = value;
                    this.NotifyPropertyChanged(o => o.BankAccountID);
                }
            }
        }

        string _bankAccount;

        /// <summary>
        /// 银行账号
        /// </summary>
        public string BankAccount
        {
            get { return _bankAccount; }
            set
            {
                if (_bankAccount != value)
                {
                    _bankAccount = value;
                    this.NotifyPropertyChanged(o => o.BankAccount);
                }
            }
        }

        #endregion

        #region 币种
        Guid _currencyID;

        /// <summary>
        /// 币种
        /// </summary>
        public Guid CurrencyID
        {
            get { return _currencyID; }
            set
            {
                if (_currencyID != value)
                {
                    _currencyID = value;
                    this.NotifyPropertyChanged(o => o.CurrencyID);
                }
            }
        }


        string _currency;

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency
        {
            get { return _currency; }
            set
            {
                if (_currency != value)
                {
                    _currency = value;
                    this.NotifyPropertyChanged(o => o.Currency);
                }
            }
        }

        #endregion

        #region 金额
        Decimal amount;
        /// <summary>
        /// 金额
        /// </summary>
        public Decimal Amount
        {
            get { return amount; }
            set
            {
                if (amount != value)
                {
                    amount = value;
                    this.NotifyPropertyChanged(o => o.Amount);
                }
            }
        }
        #endregion

        #region 实际金额
        Decimal finalAmount;
        /// <summary>
        /// 实际金额
        /// </summary>
        public Decimal FinalAmount
        {
            get { return finalAmount; }
            set
            {
                if (finalAmount != value)
                {
                    finalAmount = value;
                    this.NotifyPropertyChanged(o => o.FinalAmount);
                }
            }
        }

        #endregion

        #region 核销日期
        DateTime? _writeOffDate;

        /// <summary>
        /// 核销日期
        /// </summary>
        public DateTime? WriteOffDate
        {
            get { return _writeOffDate; }
            set
            {
                if (_writeOffDate != value)
                {
                    _writeOffDate = value;
                    this.NotifyPropertyChanged(o => o.WriteOffDate);
                }
            }
        }

        #endregion

        #region 到账日期
        DateTime? _reachedDate;

        /// <summary>
        /// 到账日期
        /// </summary>
        public DateTime? ReachedDate
        {
            get { return _reachedDate; }
            set
            {
                if (_reachedDate != value)
                {
                    _reachedDate = value;
                    this.NotifyPropertyChanged(o => o.ReachedDate);
                }
            }
        }

        #endregion

        #region 例证号

        string _voucherSeqNo;

        /// <summary>
        /// 凭证号
        /// </summary>
        public string VoucherSeqNo
        {
            get { return _voucherSeqNo; }
            set
            {
                if (_voucherSeqNo != value)
                {
                    _voucherSeqNo = value;
                    this.NotifyPropertyChanged(o => o.VoucherSeqNo);
                }
            }
        }

        string _auditBy;
        #endregion

        #region 审核人

        /// <summary>
        /// 是否已审核
        /// </summary>
        public bool IsAuditor
        {
            get
            {
                return !string.IsNullOrEmpty(ApprovalByName);
            }
        }
        /// <summary>
        /// 审核人ID
        /// </summary>
        public Guid? AuditByID
        {
            get;
            set;
        }

        /// <summary>
        /// 审核人
        /// </summary>
        public string ApprovalByName
        {
            get { return _auditBy; }
            set
            {
                if (_auditBy != value)
                {
                    _auditBy = value;
                    this.NotifyPropertyChanged(o => o.ApprovalByName);
                }
            }
        }

        #endregion

        #region 到账人
        /// <summary>
        /// 到账人ID
        /// </summary>
        public Guid? ReachedByID
        {
            get;
            set;
        }


        string _reachedBy;

        /// <summary>
        /// 到账人
        /// </summary>
        public string BankByName
        {
            get { return _reachedBy; }
            set
            {
                if (_reachedBy != value)
                {
                    _reachedBy = value;
                    this.NotifyPropertyChanged(o => o.BankByName);
                }
            }
        }

        #endregion

        #region 创建人
        string _createdBy;

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedByName
        {
            get { return _createdBy; }
            set
            {
                if (_createdBy != value)
                {
                    _createdBy = value;
                    this.NotifyPropertyChanged(o => o.CreatedByName);
                }
            }
        }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid CreatedByID
        {
            get;
            set;
        }

        #endregion

        #region 创建日期
        DateTime createDate;
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return createDate;
            }
            set
            {
                if (createDate != value)
                {
                    createDate = value;
                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }

        }
        #endregion

        #region 更新时间
        private DateTime? updateDate;
        /// <summary>
        /// 币种金额 信息更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return updateDate;
            }
            set
            {
                if (updateDate != value)
                {
                    updateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }

        /// <summary>
        /// 销账单的更新时间
        /// </summary>
        public DateTime? CheckUpdateDate
        {
            get;
            set;
        }

        #endregion

        #region 备注

        string _remark;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set
            {
                if (_remark != value)
                {
                    _remark = value;
                    this.NotifyPropertyChanged(o => o.Remark);
                }
            }
        }

        #endregion

        #region 是否到账


        /// <summary>
        /// 是否到账
        /// </summary>
        public bool HasRecoginized
        {
            get
            {
                return !string.IsNullOrEmpty(this.BankByName);
            }

        }
        #endregion

        #region 有效性
        /// <summary>
        /// 有效性
        /// </summary>
        public bool IsValid
        {
            get;
            set;
        }
        #endregion

        #region 作废时间
        /// <summary>
        /// 作废时间
        /// </summary>
        public DateTime? VoidDate
        {
            get;
            set;
        }
        #endregion

    }
    #endregion

    #region 销账--凭证明细

    /// <summary>
    /// 凭证明细
    /// </summary>
    [Serializable]
    public class CredentialsDetailList : BaseDataObject
    {
        #region  ID

        Guid _id;

        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }
        #endregion

        #region
        Guid writeOffID;
        /// <summary>
        /// 核销ID
        /// </summary>
        public Guid WriteOffID
        {
            get
            {
                return writeOffID;
            }
            set
            {
                if (writeOffID != value)
                {
                    writeOffID = value;
                    this.NotifyPropertyChanged(o => o.WriteOffID);
                }
            }


        }

        #endregion

        #region GLID
        Guid glID;
        /// <summary>
        /// GLID
        /// </summary>
        [GuidRequired(CMessage = "会计科目", EMessage = "GL")]
        public Guid GLID
        {
            get
            {
                return glID;
            }
            set
            {
                if (glID != value)
                {
                    glID = value;
                    this.NotifyPropertyChanged(o => o.GLID);
                }
            }
        }
        #endregion

        #region GL
        string gl;
        /// <summary>
        /// GL
        /// </summary>
        public string GL
        {
            get
            {
                return gl;
            }
            set
            {
                if (gl != value)
                {
                    gl = value;
                    this.NotifyPropertyChanged(o => o.GL);
                }
            }
        }
        #endregion

        #region Remark
        string remark;
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                if (remark != value)
                {
                    remark = value;
                    this.NotifyPropertyChanged(o => o.Remark);
                }
            }
        }
        #endregion

        #region OrgDebit

        decimal orgDebit;
        /// <summary>
        /// 原始借方
        /// </summary>
        public decimal OrgDebit
        {
            get
            {
                return orgDebit;
            }
            set
            {
                if (orgDebit != value)
                {
                    orgDebit = value;
                    this.NotifyPropertyChanged(o => o.orgDebit);
                }
            }
        }

        #endregion


        #region OrgCredit
        decimal orgCredit;
        /// <summary>
        /// 原始贷方
        /// </summary>
        public decimal OrgCredit
        {
            get
            {
                return orgCredit;
            }
            set
            {
                if (orgCredit != value)
                {
                    orgCredit = value;
                    this.NotifyPropertyChanged(o => o.OrgCredit);
                }
            }
        }

        #endregion


        #region Rate
        decimal rate;
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate
        {
            get
            {
                return rate;
            }
            set
            {
                if (rate != value)
                {
                    rate = value;
                    this.NotifyPropertyChanged(o => o.Rate);
                }
            }
        }

        #endregion

        #region debit

        decimal debit;
        /// <summary>
        /// 借方
        /// </summary>
        public decimal Debit
        {
            get
            {
                return debit;
            }
            set
            {
                if (debit != value)
                {
                    debit = value;
                    this.NotifyPropertyChanged(o => o.Debit);
                }
            }
        }

        #endregion


        #region Credit
        decimal credit;
        /// <summary>
        /// 贷方
        /// </summary>
        public decimal Credit
        {
            get
            {
                return credit;
            }
            set
            {
                if (credit != value)
                {
                    credit = value;
                    this.NotifyPropertyChanged(o => o.Credit);
                }
            }
        }

        #endregion

        #region 客户

        Guid _customerID;

        /// <summary>
        /// 客户ID
        /// </summary>
        [GuidRequired(CMessage = "客户", EMessage = "Customer")]
        public Guid CustomerID
        {
            get
            {
                return _customerID;
            }
            set
            {
                if (_customerID != value)
                {
                    _customerID = value;
                    this.NotifyPropertyChanged(o => o.CustomerID);
                }
            }
        }

        string _customerName;
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    this.NotifyPropertyChanged(o => o.CustomerName);
                }
            }
        }

        #endregion

        #region UpdateDate

        DateTime? updateDate;
        /// <summary>
        /// UpdateDate
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return updateDate;
            }
            set
            {
                if (updateDate != value)
                {
                    updateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }
        #endregion


    }

    #endregion

    #region 销账--预收预付信息
    /// <summary>
    /// 预收预付信息
    /// </summary>
    [Serializable]
    public class PrepaymentList : BaseDataObject
    {
        /// <summary>
        /// 是否选择
        /// </summary>
        public bool IsCheck
        {
            get;
            set;
        }
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID
        {
            get;
            set;
        }
        /// <summary>
        /// CheckID
        /// </summary>
        public Guid CheckID
        {
            get;
            set;
        }
        /// <summary>
        /// 销账单号
        /// </summary>
        public string CheckNo
        {
            get;
            set;
        }
        /// <summary>
        /// 销账时间
        /// </summary>
        public DateTime CheckDate
        {
            get;
            set;
        }
        /// <summary>
        /// 账单号
        /// </summary>
        public string BillNo
        {
            get;
            set;
        }
        /// <summary>
        /// 方向
        /// </summary>
        public FeeWay Way
        {
            get;
            set;
        }
        /// <summary>
        /// 币种
        /// </summary>
        public Guid CurrencyID
        {
            get;
            set;
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount
        {
            get;
            set;
        }
        /// <summary>
        /// 已销账金额
        /// </summary>
        public decimal PayAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 销账金额
        /// </summary>
        public decimal CheckAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get;
            set;
        }
        /// <summary>
        /// 参考ID(费用ID)
        /// </summary>
        public Guid RefID
        {
            get;
            set;
        }

    }
    #endregion

    #region 销账--解锁信息
    [Serializable]
    public class UntieLockCheckResult
    {
        public int Type { get; set; }
        public Guid ID { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
    #endregion

    
}
