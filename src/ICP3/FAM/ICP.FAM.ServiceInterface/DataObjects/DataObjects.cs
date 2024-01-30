namespace ICP.FAM.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic;
    using ICP.Framework.CommonLibrary.Common;



    #region PaymentRequestList

    /// <summary>
    /// 付款申请列表
    /// </summary>
    [Serializable]
    public class PaymentRequestList
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 流程ID
        /// </summary>
        public Guid WorkFlowID { get; set; }

        /// <summary>
        /// 工作名
        /// </summary>
        public string WorkFlowName { get; set; }

        /// <summary>
        /// 付款申请单号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public Guid CreateByID { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string CreateByName { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public string CreateDate { get; set; }

        /// <summary>
        /// 付款单ID
        /// </summary>
        public Guid? CheckID { get; set; }

        /// <summary>
        /// 付款单号
        /// </summary>
        public string CheckNo { get; set; }
    }

    /// <summary>
    /// 付款申请明细列表
    /// </summary>
    public class PaymentRequestItemList
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 付款申请ID
        /// </summary>
        public Guid PaymentRequestID { get; set; }

        /// <summary>
        /// 费用ID
        /// </summary>
        public Guid ChargeID { get; set; }

        /// <summary>
        /// 费用代码
        /// </summary>
        public string ChargeCode { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }


        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 销账汇率
        /// </summary>
        public decimal WriteOffRate { get; set; }


        /// <summary>
        /// 销账金额
        /// </summary>
        public decimal WriteOffAmount { get; set; }


    }

    #endregion

    #region CheckList

    /// <summary>
    /// CheckList
    /// </summary>
    [Serializable]
    public partial class CheckList : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一键
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


        string _no;
        /// <summary>
        /// 收付款单号
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "收付款单号", EMessage = "No")]

        public string No
        {
            get
            {
                return _no;
            }
            set
            {
                if (_no != value)
                {
                    _no = value;
                    this.NotifyPropertyChanged(o => o.No);
                }
            }
        }


        CheckType _type;
        /// <summary>
        /// 类型（0:收款,1:付款）
        /// </summary>
        [Required(CMessage = "类型",EMessage="Type")]

        public CheckType Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    this.NotifyPropertyChanged(o => o.Type);
                }
            }
        }


        string _companyname;
        /// <summary>
        /// 公司
        /// </summary>
        public string CompanyName
        {
            get
            {
                return _companyname;
            }
            set
            {
                if (_companyname != value)
                {
                    _companyname = value;
                    this.NotifyPropertyChanged(o => o.CompanyName);
                }
            }
        }


        string _customername;
        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerName
        {
            get
            {
                return _customername;
            }
            set
            {
                if (_customername != value)
                {
                    _customername = value;
                    this.NotifyPropertyChanged(o => o.CustomerName);
                }
            }
        }


        string _bankaccountname;
        /// <summary>
        /// 银行账户
        /// </summary>
        public string BankAccountName
        {
            get
            {
                return _bankaccountname;
            }
            set
            {
                if (_bankaccountname != value)
                {
                    _bankaccountname = value;
                    this.NotifyPropertyChanged(o => o.BankAccountName);
                }
            }
        }


        string _checkno;
        /// <summary>
        /// 支票号
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "支票号", EMessage = "CheckNo")]

        [Required(CMessage = "支票号",EMessage="CheckNo")]

        public string CheckNo
        {
            get
            {
                return _checkno;
            }
            set
            {
                if (_checkno != value)
                {
                    _checkno = value;
                    this.NotifyPropertyChanged(o => o.CheckNo);
                }
            }
        }


        decimal _amount;
        /// <summary>
        /// 金额
        /// </summary>
        [Required(CMessage = "金额",EMessage="Amount")]

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


        string _checkbyname;
        /// <summary>
        /// 核销人
        /// </summary>
        public string CheckByName
        {
            get
            {
                return _checkbyname;
            }
            set
            {
                if (_checkbyname != value)
                {
                    _checkbyname = value;
                    this.NotifyPropertyChanged(o => o.CheckByName);
                }
            }
        }


        DateTime _checkdate;
        /// <summary>
        /// 核销日期
        /// </summary>
        [Required(CMessage = "核销日期",EMessage="CheckDate")]

        public DateTime CheckDate
        {
            get
            {
                return _checkdate;
            }
            set
            {
                if (_checkdate != value)
                {
                    _checkdate = value;
                    this.NotifyPropertyChanged(o => o.CheckDate);
                }
            }
        }


        string _bankbyname;
        /// <summary>
        /// 到账人
        /// </summary>
        public string BankByName
        {
            get
            {
                return _bankbyname;
            }
            set
            {
                if (_bankbyname != value)
                {
                    _bankbyname = value;
                    this.NotifyPropertyChanged(o => o.BankByName);
                }
            }
        }


        DateTime? _bankdate;
        /// <summary>
        /// 银行日期
        /// </summary>
        public DateTime? BankDate
        {
            get
            {
                return _bankdate;
            }
            set
            {
                if (_bankdate != value)
                {
                    _bankdate = value;
                    this.NotifyPropertyChanged(o => o.BankDate);
                }
            }
        }


        string _approvalbyname;
        /// <summary>
        /// 审核人
        /// </summary>
        public string ApprovalByName
        {
            get
            {
                return _approvalbyname;
            }
            set
            {
                if (_approvalbyname != value)
                {
                    _approvalbyname = value;
                    this.NotifyPropertyChanged(o => o.ApprovalByName);
                }
            }
        }


        DateTime? _approvaldate;
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ApprovalDate
        {
            get
            {
                return _approvaldate;
            }
            set
            {
                if (_approvaldate != value)
                {
                    _approvaldate = value;
                    this.NotifyPropertyChanged(o => o.ApprovalDate);
                }
            }
        }


        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaximumLength = 500, CMessage = "备注", EMessage = "Remark")]

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


        bool _isshare;
        /// <summary>
        /// 是否共享（如果选择否，那么只有自己已经上级可以查看）
        /// </summary>
        [Required(CMessage = "是否共享",EMessage="IsShare")]

        public bool IsShare
        {
            get
            {
                return _isshare;
            }
            set
            {
                if (_isshare != value)
                {
                    _isshare = value;
                    this.NotifyPropertyChanged(o => o.IsShare);
                }
            }
        }


        string _voucherno;
        /// <summary>
        /// 凭证号（由系统在到账后自动生成）
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "凭证号", EMessage = "VoucherNo")]

        [Required(CMessage = "凭证号",EMessage="VoucherNo")]

        public string VoucherNo
        {
            get
            {
                return _voucherno;
            }
            set
            {
                if (_voucherno != value)
                {
                    _voucherno = value;
                    this.NotifyPropertyChanged(o => o.VoucherNo);
                }
            }
        }


        string _createbyname;
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName
        {
            get
            {
                return _createbyname;
            }
            set
            {
                if (_createbyname != value)
                {
                    _createbyname = value;
                    this.NotifyPropertyChanged(o => o.CreateByName);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 建立日期
        /// </summary>
        [Required(CMessage = "建立日期",EMessage="CreateDate")]

        public DateTime CreateDate
        {
            get
            {
                return _createdate;
            }
            set
            {
                if (_createdate != value)
                {
                    _createdate = value;
                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }
        }


        bool _isvalid;
        /// <summary>
        /// 是否有效
        /// </summary>
        [Required(CMessage = "是否有效",EMessage="IsValid")]

        public bool IsValid
        {
            get
            {
                return _isvalid;
            }
            set
            {
                if (_isvalid != value)
                {
                    _isvalid = value;
                    this.NotifyPropertyChanged(o => o.IsValid);
                }
            }
        }

        #region 重写  GetHashCode Equals
        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            CheckList newObj = obj as CheckList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
        #endregion
    }

    /// <summary>
    /// CheckInfo
    /// </summary>
    [Serializable]
    public partial class CheckInfo : CheckList
    {
        /// <summary>
        /// CheckInfo
        /// </summary>
        public CheckInfo()
        {
            this.CheckItems = new List<CheckItemList>();
            this.Expenses = new List<ExpenseList>();
        }

        Guid _bankaccountid;
        /// <summary>
        /// 银行账户ID
        /// </summary>
        [Required(CMessage = "银行账户",EMessage="BankAccount")]

        public Guid BankAccountID
        {
            get
            {
                return _bankaccountid;
            }
            set
            {
                if (_bankaccountid != value)
                {
                    _bankaccountid = value;
                    this.NotifyPropertyChanged(o => o.BankAccountID);
                }
            }
        }


        Guid _customerid;
        /// <summary>
        /// 客户ID
        /// </summary>
        [GuidRequired(CMessage = "客户",EMessage="Customer")]

        public Guid CustomerID
        {
            get
            {
                return _customerid;
            }
            set
            {
                if (_customerid != value)
                {
                    _customerid = value;
                    this.NotifyPropertyChanged(o => o.CustomerID);
                }
            }
        }


        Guid _companyid;
        /// <summary>
        /// 公司ID
        /// </summary>
        [GuidRequired(CMessage = "公司",EMessage="Company")]

        public Guid CompanyID
        {
            get
            {
                return _companyid;
            }
            set
            {
                if (_companyid != value)
                {
                    _companyid = value;
                    this.NotifyPropertyChanged(o => o.CompanyID);
                }
            }
        }


        Guid _checkbyid;
        /// <summary>
        /// 核销人
        /// </summary>
        [GuidRequired(CMessage = "核销人",EMessage="CheckBy")]

        public Guid CheckByID
        {
            get
            {
                return _checkbyid;
            }
            set
            {
                if (_checkbyid != value)
                {
                    _checkbyid = value;
                    this.NotifyPropertyChanged(o => o.CheckByID);
                }
            }
        }


        Guid? _bankbyid;
        /// <summary>
        /// 到账人
        /// </summary>
        public Guid? BankByID
        {
            get
            {
                return _bankbyid;
            }
            set
            {
                if (_bankbyid != value)
                {
                    _bankbyid = value;
                    this.NotifyPropertyChanged(o => o.BankByID);
                }
            }
        }


        Guid? _approvalbyid;
        /// <summary>
        /// 审核人
        /// </summary>
        public Guid? ApprovalByID
        {
            get
            {
                return _approvalbyid;
            }
            set
            {
                if (_approvalbyid != value)
                {
                    _approvalbyid = value;
                    this.NotifyPropertyChanged(o => o.ApprovalByID);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID
        {
            get
            {
                return _createbyid;
            }
            set
            {
                if (_createbyid != value)
                {
                    _createbyid = value;
                    this.NotifyPropertyChanged(o => o.CreateByID);
                }
            }
        }


        List<CheckItemList> _checkitems;
        /// <summary>
        /// 销帐明细列表
        /// </summary>
        public List<CheckItemList> CheckItems
        {
            get
            {
                return _checkitems;
            }
            set
            {
                if (_checkitems != value)
                {
                    _checkitems = value;
                    this.NotifyPropertyChanged(o => o.CheckItems);
                }
            }
        }


        List<ExpenseList> _expenses;
        /// <summary>
        /// 财务费用列表
        /// </summary>
        public List<ExpenseList> Expenses
        {
            get
            {
                return _expenses;
            }
            set
            {
                if (_expenses != value)
                {
                    _expenses = value;
                    this.NotifyPropertyChanged(o => o.Expenses);
                }
            }
        }


    }

    #endregion

    #region CheckItemList

    /// <summary>
    /// CheckItemList
    /// </summary>
    [Serializable]
    public partial class CheckItemList : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一键
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


        Guid _checkid;
        /// <summary>
        /// 收付款单ID
        /// </summary>
        public Guid CheckID
        {
            get
            {
                return _checkid;
            }
            set
            {
                if (_checkid != value)
                {
                    _checkid = value;
                    this.NotifyPropertyChanged(o => o.CheckID);
                }
            }
        }


        Guid _chargeid;
        /// <summary>
        /// 业务费用ID
        /// </summary>
        public Guid ChargeID
        {
            get
            {
                return _chargeid;
            }
            set
            {
                if (_chargeid != value)
                {
                    _chargeid = value;
                    this.NotifyPropertyChanged(o => o.ChargeID);
                }
            }
        }


        string _billno;
        /// <summary>
        /// 帐单号
        /// </summary>
        public string BillNo
        {
            get
            {
                return _billno;
            }
            set
            {
                if (_billno != value)
                {
                    _billno = value;
                    this.NotifyPropertyChanged(o => o.BillNo);
                }
            }
        }


        string _currency;
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency
        {
            get
            {
                return _currency;
            }
            set
            {
                if (_currency != value)
                {
                    _currency = value;
                    this.NotifyPropertyChanged(o => o.Currency);
                }
            }
        }


        decimal _amount;
        /// <summary>
        /// 金额
        /// </summary>
        [Required(CMessage = "金额",EMessage="Amount")]

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


        decimal _writeoffrate;
        /// <summary>
        /// 核销汇率
        /// </summary>
        [Required(CMessage = "核销汇率",EMessage="WriteOffRate")]

        public decimal WriteOffRate
        {
            get
            {
                return _writeoffrate;
            }
            set
            {
                if (_writeoffrate != value)
                {
                    _writeoffrate = value;
                    this.NotifyPropertyChanged(o => o.WriteOffRate);
                }
            }
        }


        decimal _writeoffamount;
        /// <summary>
        /// 核销金额
        /// </summary>
        [Required(CMessage = "核销金额",EMessage="WriteOffAmount")]

        public decimal WriteOffAmount
        {
            get
            {
                return _writeoffamount;
            }
            set
            {
                if (_writeoffamount != value)
                {
                    _writeoffamount = value;
                    this.NotifyPropertyChanged(o => o.WriteOffAmount);
                }
            }
        }


        decimal _costamount;
        /// <summary>
        ///  折合金额
        /// </summary>
        [Required(CMessage = "金额",EMessage="CostAmount")]

        public decimal CostAmount
        {
            get
            {
                return _costamount;
            }
            set
            {
                if (_costamount != value)
                {
                    _costamount = value;
                    this.NotifyPropertyChanged(o => o.CostAmount);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        [Required(CMessage = "建立人",EMessage="CreateBy")]

        public Guid CreateByID
        {
            get
            {
                return _createbyid;
            }
            set
            {
                if (_createbyid != value)
                {
                    _createbyid = value;
                    this.NotifyPropertyChanged(o => o.CreateByID);
                }
            }
        }


        string _createbyname;
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName
        {
            get
            {
                return _createbyname;
            }
            set
            {
                if (_createbyname != value)
                {
                    _createbyname = value;
                    this.NotifyPropertyChanged(o => o.CreateByName);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 建立日期
        /// </summary>
        [Required(CMessage = "建立日期",EMessage="CreateDate")]

        public DateTime CreateDate
        {
            get
            {
                return _createdate;
            }
            set
            {
                if (_createdate != value)
                {
                    _createdate = value;
                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }
        }

        #region 重写  GetHashCode Equals
        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            CheckItemList newObj = obj as CheckItemList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
        #endregion
    }

    #endregion

    #region ExpenseList
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class ExpenseList : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一键
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


        Guid _checkid;
        /// <summary>
        /// 核销ID
        /// </summary>
        public Guid CheckId
        {
            get
            {
                return _checkid;
            }
            set
            {
                if (_checkid != value)
                {
                    _checkid = value;
                    this.NotifyPropertyChanged(o => o.CheckId);
                }
            }
        }


        Guid? _customerid;
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid? CustomerId
        {
            get
            {
                return _customerid;
            }
            set
            {
                if (_customerid != value)
                {
                    _customerid = value;
                    this.NotifyPropertyChanged(o => o.CustomerId);
                }
            }
        }


        string _billno;
        /// <summary>
        /// 账单号
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "账单号", EMessage = "BillNo")]

        public string BillNo
        {
            get
            {
                return _billno;
            }
            set
            {
                if (_billno != value)
                {
                    _billno = value;
                    this.NotifyPropertyChanged(o => o.BillNo);
                }
            }
        }


        Guid _glid;
        /// <summary>
        /// 会计科目ID
        /// </summary>
        [GuidRequired(CMessage = "会计科目", EMessage = "GLID")]

        public Guid GLID
        {
            get
            {
                return _glid;
            }
            set
            {
                if (_glid != value)
                {
                    _glid = value;
                    this.NotifyPropertyChanged(o => o.GLID);
                }
            }
        }


        string _gldescription;
        /// <summary>
        /// 描述（默认为会计科目名称）
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "会计科目", EMessage = "GLDescription")]

        public string GLDescription
        {
            get
            {
                return _gldescription;
            }
            set
            {
                if (_gldescription != value)
                {
                    _gldescription = value;
                    this.NotifyPropertyChanged(o => o.GLDescription);
                }
            }
        }


        Guid _currencyid;
        /// <summary>
        /// 币种ID
        /// </summary>
        [GuidRequired(CMessage = "币种",EMessage="Currency")]

        public Guid CurrencyID
        {
            get
            {
                return _currencyid;
            }
            set
            {
                if (_currencyid != value)
                {
                    _currencyid = value;
                    this.NotifyPropertyChanged(o => o.CurrencyID);
                }
            }
        }


        string _currencyname;
        /// <summary>
        /// 币种
        /// </summary>
        public string CurrencyName
        {
            get
            {
                return _currencyname;
            }
            set
            {
                if (_currencyname != value)
                {
                    _currencyname = value;
                    this.NotifyPropertyChanged(o => o.CurrencyName);
                }
            }
        }


        decimal _rate;
        /// <summary>
        /// 
        /// </summary>
        [Required(CMessage = "汇率",EMessage="Rate")]

        public decimal Rate
        {
            get
            {
                return _rate;
            }
            set
            {
                if (_rate != value)
                {
                    _rate = value;
                    this.NotifyPropertyChanged(o => o.Rate);
                }
            }
        }


        decimal _amount;
        /// <summary>
        /// 金额
        /// </summary>
        [DecimalRequired(CMessage = "金额",EMessage="Amount")]

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


        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaximumLength=100,CMessage="备注",EMessage="Remark")]

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


        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        [Required(CMessage = "建立人",EMessage="CreateBy")]

        public Guid CreateByID
        {
            get
            {
                return _createbyid;
            }
            set
            {
                if (_createbyid != value)
                {
                    _createbyid = value;
                    this.NotifyPropertyChanged(o => o.CreateByID);
                }
            }
        }


        string _createbyname;
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName
        {
            get
            {
                return _createbyname;
            }
            set
            {
                if (_createbyname != value)
                {
                    _createbyname = value;
                    this.NotifyPropertyChanged(o => o.CreateByName);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 建立日期
        /// </summary>
        [Required(CMessage = "建立日期",EMessage="CreateDate")]

        public DateTime CreateDate
        {
            get
            {
                return _createdate;
            }
            set
            {
                if (_createdate != value)
                {
                    _createdate = value;
                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }
        }

        #region 重写  GetHashCode Equals
        /// <summary>
        /// GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            ExpenseList newObj = obj as ExpenseList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }

        #endregion
    }

    #endregion


    #region 凭证

    /// <summary>
    /// 凭证明细
    /// </summary>
    [Serializable]
    public class LedgerList : BaseDataObject
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
        Guid refID;
        /// <summary>
        /// 参考ID
        /// </summary>
        public Guid RefID
        {
            get
            {
                return refID;
            }
            set
            {
                if (refID != value)
                {
                    refID = value;
                    this.NotifyPropertyChanged(o => o.RefID);
                }
            }


        }

        #endregion

        #region GLID
        Guid glID;
        /// <summary>
        /// GLID
        /// </summary>
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
}