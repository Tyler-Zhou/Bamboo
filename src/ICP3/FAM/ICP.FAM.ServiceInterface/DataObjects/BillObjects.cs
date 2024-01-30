using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Client;
    using System.Xml.Serialization;
    using System.Text;
    using System.Runtime.Serialization;
using System.Data;


    public class BillTotalInfo
    {
        public Guid CurrencyID
        {
            get;
            set;
        }
        public String CurrencyName
        {
            get;
            set;
        }
        public FeeWay Way
        {
            get;
            set;
        }

        public Decimal Amount
        {
            get;
            set;
        }
    }
    #region Bill

    /// <summary>
    /// 帐单列表对象
    /// </summary>
    [Serializable]
    public partial class BillList : BaseDataObject
    {
        /// <summary>
        ///  return ID == Guid.Empty; 
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        /// <summary>
        ///  客户端帮助属性
        /// </summary>
        public bool Selected { get; set; }

        #region 业务ID
        private Guid operationID;
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID
        {
            get
            {
                return operationID;

            }
            set
            {
                if (operationID != value)
                {
                    operationID = value;
                    this.NotifyPropertyChanged(o => o.OperationID);
                }
            }

        }
        #endregion

        #region 业务号
        /// <summary>
        /// 业务号
        /// </summary>
        public String OperationNo { get; set; }
        #endregion

        #region 业务类型
        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperationType
        {
            get;
            set;
        }
        #endregion

        #region 审核人
        Guid? _AuditorID;
        /// <summary>
        /// 审核人ID
        /// </summary>
        public Guid? AuditorID
        {
            get
            {
                return _AuditorID;
            }
            set
            {
                if (_AuditorID != value)
                {
                    _AuditorID = value;
                    this.NotifyPropertyChanged(o => o.AuditorID);
                }
            }
        }
        #endregion

        #region 审核人Email Address
        string _AuditorEmail;
        /// <summary>
        /// 审核人Email地址
        /// </summary>
        public string AuditorEmail
        {
            get
            {
                return _AuditorEmail;
            }
            set
            {
                if (_AuditorEmail != value)
                {
                    _AuditorEmail = value;
                    this.NotifyPropertyChanged(o => o.AuditorEmail);
                }
            }
        }
        #endregion

        #region State

        BillState _state;
        /// <summary>
        /// 账单状态（1:已创建、2已审核、3已对账、4已核销、5已到账)
        /// </summary>
        public BillState State
        {
            get
            {
                return _state;
            }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    this.NotifyPropertyChanged(o => o.State);
                }
            }
        }
        #endregion

        #region IsAuditing ReadOnly
        /// <summary>
        /// 已审核(只读,返回状态是否为已审核后的状态
        /// </summary>
        public bool IsAuditing
        {
            get
            {
                return (short)State >= (short)BillState.Approved;
            }
        }
        #endregion

        #region ID
        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        [Required(CMessage = "唯一键", EMessage = "ID")]
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

        #region No

        string _no;
        /// <summary>
        /// 账单号
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "账单号", EMessage = "No")]
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

        #endregion

        #region FormNo

        string _FormNo;
        /// <summary>
        /// 参考号
        /// </summary>
        public string FormNo
        {
            get
            {
                return _FormNo;
            }
            set
            {
                if (_FormNo != value)
                {
                    _FormNo = value;
                    this.NotifyPropertyChanged(o => o.FormNo);
                }
            }
        }
        #endregion

        #region Customer
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID { get; set; }

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
        /// <summary>
        /// 客户税务号
        /// </summary>
        public string CustomerTaxNo
        {
            get;
            set;
        }
        /// <summary>
        /// 客户电话地址
        /// </summary>
        public string CustomerAddressTel
        {
            get;
            set;
        }
        /// <summary>
        /// 客户银行帐号
        /// </summary>
        public string CustomerBankAccountNo
        {
            get;
            set;
        }
        #endregion

        #region CustomerDescription
        FAMCustomerDescription _CustomerDescription;
        /// <summary>
        /// 客户
        /// </summary>
        public FAMCustomerDescription CustomerDescription
        {
            get
            {
                return _CustomerDescription;
            }
            set
            {
                if (_CustomerDescription != value)
                {
                    _CustomerDescription = value;
                    this.NotifyPropertyChanged(o => o.CustomerDescription);
                }
            }
        }
        #endregion

        #region Type
        BillType _type;
        /// <summary>
        /// 账单类型（0:应收,1:应付,2:代理）
        /// </summary>
        [Required(CMessage = "账单类型", EMessage = "Type")]
        public BillType Type
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

        #endregion

        #region AccountDate

        DateTime _AccountDate;
        /// <summary>
        /// 账单日
        /// </summary>
        [Required(CMessage = "账单日", EMessage = "AccountDate")]
        public DateTime AccountDate
        {
            get
            {
                return _AccountDate;
            }
            set
            {
                if (_AccountDate != value)
                {
                    _AccountDate = value;
                    this.NotifyPropertyChanged(o => o.AccountDate);
                }
            }
        }
        #endregion

        #region DueDate

        DateTime _duedate;
        /// <summary>
        /// 到期日
        /// </summary>
        [Required(CMessage = "到期日", EMessage = "DueDate")]
        public DateTime DueDate
        {
            get
            {
                return _duedate;
            }
            set
            {
                if (_duedate != value)
                {
                    _duedate = value;
                    this.NotifyPropertyChanged(o => o.DueDate);
                }
            }
        }
        #endregion

        #region 金额描述 ,核销金额描述, 未核销金额描述

        string _amount;
        /// <summary>
        /// 金额描述
        /// </summary>
        public string AmountDescription
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
                    this.NotifyPropertyChanged(o => o.AmountDescription);
                }
            }
        }


        string _payamount;
        /// <summary>
        /// 核销金额描述
        /// </summary>
        public string PayAmountDescription
        {
            get
            {
                return _payamount;
            }
            set
            {
                if (_payamount != value)
                {
                    _payamount = value;
                    this.NotifyPropertyChanged(o => o.PayAmountDescription);
                }
            }
        }

        string _BalanceDescription;
        /// <summary>
        /// 未核销金额描述
        /// </summary>
        public string BalanceDescription
        {
            get
            {
                return _BalanceDescription;
            }
            set
            {
                if (_BalanceDescription != value)
                {
                    _BalanceDescription = value;
                    this.NotifyPropertyChanged(o => o.BalanceDescription);
                }
            }
        }

        #endregion

        #region 发票号

        string _InvoiceNo;
        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNo
        {
            get
            {
                return _InvoiceNo;
            }
            set
            {
                if (_InvoiceNo != value)
                {
                    _InvoiceNo = value;
                    this.NotifyPropertyChanged(o => o.InvoiceNo);
                }
            }
        }

        #endregion

        #region 支票号

        string _CheckNo;
        /// <summary>
        /// 支票号
        /// </summary>
        public string CheckNo
        {
            get
            {
                return _CheckNo;
            }
            set
            {
                if (_CheckNo != value)
                {
                    _CheckNo = value;
                    this.NotifyPropertyChanged(o => o.CheckNo);
                }
            }
        }

        #endregion

        #region Company
        Guid _companyid;
        /// <summary>
        /// 公司ID
        /// </summary>
        [GuidRequired(CMessage = "公司", EMessage = "Company")]
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
        #endregion

        #region Create Info

        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        [Required(CMessage = "建立人", EMessage = "CreateBy")]
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
        /// 建立时间
        /// </summary>
        [Required(CMessage = "建立时间", EMessage = "CreateDate")]
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

        DateTime? _updateDate;
        /// <summary>
        /// 更新时间-做数据版本控制用
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                if (_updateDate != value)
                {
                    _updateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }
        #endregion

        #region CurrencyAmountDatas
        /// <summary>
        /// 帐单币种金额列表
        /// </summary>
        public List<CurrencyAmountData> CurrencyAmounts { get; set; }
        #endregion

        #region Fees

        List<ChargeList> _fees;
        /// <summary>
        /// 费用列表
        /// </summary>
        public List<ChargeList> Fees
        {
            get
            {
                return _fees;
            }
            set
            {
                if (_fees != value)
                {
                    _fees = value;
                    this.NotifyPropertyChanged(o => o.Fees);
                }
            }
        }
        #endregion

        #region 支票号

        bool _IsSpecial;
        /// <summary>
        /// 是否特殊帐单
        /// </summary>
        public bool IsSpecial
        {
            get
            {
                return _IsSpecial;
            }
            set
            {
                if (_IsSpecial != value)
                {
                    _IsSpecial = value;
                    this.NotifyPropertyChanged(o => o.IsSpecial);
                }
            }
        }

        #endregion

        #region 到帐日期
        /// <summary>
        /// 到帐日期
        /// </summary>
        public string BankDates
        {
            get;
            set;
        }
        #endregion

        #region 账单的来源1为海出2为海进 joe 2013-08-21
        /// <summary>
        /// 账单的来源1为海出2为海进 
        /// </summary>
        public int? FromType
        {
            get;
            set;
        }
        #endregion

        #region 增值税费用总额
        private Decimal? vATAmount;
        /// <summary>
        /// 增值税费用总额
        /// </summary>
        public Decimal? VATAmout
        {
            get
            {
                return vATAmount;
            }
            set
            {
                if (vATAmount != value)
                {
                    vATAmount = value;
                    this.NotifyPropertyChanged(o => o.VATAmout);
                }
            }
        }
        #endregion

        /// <summary>
        /// 代理ID
        /// </summary>
        public Guid? AgentID
        {
            get;
            set;
        }

        /// <summary>
        /// 承运人
        /// </summary>
        public bool IsAgentOfCarrier
        {
            get;
            set;
        }

        /// <summary>
        /// 是否已和承运人确认费用
        /// </summary>
        public bool IsAPCCfm
        {
            get;
            set;
        }

        /// <summary>
        /// 销帐日期
        /// </summary>
        public string CheckDates
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 帐单明细
    /// </summary>
    [Serializable]
    public partial class BillInfo : BillList
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BillInfo()
        {
            this.CurrencyRates = new List<CurrencyRateData>();
        }

        #region PayCurrencyId

        Guid? _PayCurrencyId;
        /// <summary>
        /// 按一种币种支付.为空时说明不是按一种币种支付的
        /// </summary>
        public Guid? PayCurrencyId
        {
            get
            {
                return _PayCurrencyId;
            }
            set
            {
                if (_PayCurrencyId != value)
                {
                    _PayCurrencyId = value;
                    this.NotifyPropertyChanged(o => o.PayCurrencyId);
                }
            }
        }

        bool _IsPadByOnecCurrency;
        /// <summary>
        /// 客户端的辅助属性
        /// </summary>
        public bool IsPadByOnecCurrency
        {
            get
            {
                return _IsPadByOnecCurrency;
            }
            set
            {
                if (_IsPadByOnecCurrency != value)
                {
                    _IsPadByOnecCurrency = value;
                    this.NotifyPropertyChanged(o => o.IsPadByOnecCurrency);
                }
            }
        }

        #endregion

        #region CurrencyRates

        List<CurrencyRateData> _CurrencyRates;
        /// <summary>
        /// 币种汇率列表
        /// </summary>
        public List<CurrencyRateData> CurrencyRates
        {
            get
            {
                return _CurrencyRates;
            }
            set
            {
                if (_CurrencyRates != value)
                {
                    _CurrencyRates = value;
                    this.NotifyPropertyChanged(o => o.CurrencyRates);
                }
            }
        }

        #endregion

        #region Customer

        Guid _customerid;
        /// <summary>
        /// 客户ID
        /// </summary>
        [GuidRequired(CMessage = "客户", EMessage = "Customer")]
        public new Guid CustomerID
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

        #endregion

        #region Form ID Type NO

        Guid _formid;
        /// <summary>
        /// 表单ID
        /// </summary>
        public Guid FormID
        {
            get
            {
                return _formid;
            }
            set
            {
                if (_formid != value)
                {
                    _formid = value;
                    this.NotifyPropertyChanged(o => o.FormID);
                }
            }
        }

        FormType _formtype;
        /// <summary>
        /// 表单类型
        /// </summary>
        public FormType FormType
        {
            get
            {
                return _formtype;
            }
            set
            {
                if (_formtype != value)
                {
                    _formtype = value;
                    this.NotifyPropertyChanged(o => o.FormType);
                }
            }
        }

        #endregion

        #region CustomerRefNo

        string _CustomerRefNo;
        /// <summary>
        /// 寄单客户
        /// </summary>
        public string CustomerRefNo
        {
            get
            {
                return _CustomerRefNo;
            }
            set
            {
                if (_CustomerRefNo != value)
                {
                    _CustomerRefNo = value;
                    this.NotifyPropertyChanged(o => o.CustomerRefNo);
                }
            }
        }
        #endregion

        #region Remark

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

        #region IsSend

        bool _IsSend;
        /// <summary>
        /// 是否已发送
        /// </summary>
        public bool IsSend
        {
            get
            {
                return _IsSend;
            }
            set
            {
                if (_IsSend != value)
                {
                    _IsSend = value;
                    this.NotifyPropertyChanged(o => o.IsSend);
                }
            }
        }
        #endregion

        #region ShipToID
        private Guid? shipToID;
        /// <summary>
        /// 
        /// </summary>
        public Guid? ShipToID
        {
            get
            {
                return shipToID;
            }
            set
            {
                if (shipToID != value)
                {
                    shipToID = value;
                    this.NotifyPropertyChanged(o => o.ShipToID);
                }
            }
        }
        #endregion

        #region ShipToName
        private string shipToName;
        /// <summary>
        /// 
        /// </summary>
        public string ShipToName
        {
            get
            {
                return shipToName;
            }
            set
            {
                if (shipToName != value)
                {
                    shipToName = value;
                    this.NotifyPropertyChanged(o => o.ShipToName);
                }
            }
        } 
        #endregion

        #region 是否开具增值税发票
        private bool isVATInvoiced;
        /// <summary>
        /// 是否开具增值税发票
        /// </summary>
        public bool IsVATInvoiced
        {
            get
            {
                return isVATInvoiced;
            }
            set
            {
                if (isVATInvoiced != value)
                {
                    isVATInvoiced = value;
                    this.NotifyPropertyChanged(o => o.IsVATInvoiced);
                }
            }
        }
        #endregion

        #region 税率
        private Decimal? taxrate;
        /// <summary>
        /// 税率
        /// </summary>
        public Decimal? Taxrate
        {
            get
            {
                return taxrate;
            }
            set
            {
                if (taxrate != value)
                {
                    taxrate = value;
                    this.NotifyPropertyChanged(o => o.Taxrate);
                }
            }
        }
        #endregion

        #region ReviseHistoryFees
        List<ChargeList> reviseHistoryFees;
        /// <summary>
        /// 费用修订历史列表
        /// </summary>
        public List<ChargeList> ReviseHistoryFees
        {
            get
            {
                return reviseHistoryFees;
            }
            set
            {
                if (reviseHistoryFees != value)
                {
                    reviseHistoryFees = value;
                    this.NotifyPropertyChanged(o => o.ReviseHistoryFees);
                }
            }
        }
        #endregion

        #region 港前费用对应的港后的费用是否销账，或港后费用对应港前的费用是否销账 joe 2013-08-21
        /// <summary>
        ///  港前费用对应的港后的费用是否销账，或港后费用对应港前的费用是否销账，
        /// </summary>
        public bool AgentFeeIsPayed
        {
            get;
            set;
        }
        #endregion

        

        #region 信用期限

        int? _creditDate;
        /// <summary>
        /// 信用期限
        /// </summary>
        public int? CreditDate
        {
            get
            {
                return _creditDate;
            }
            set
            {
                if (_creditDate != value)
                {
                    _creditDate = value;

                    this.NotifyPropertyChanged(o => o.CreditDate);
                }
            }
        }

        #endregion

        /// <summary>
        /// 代理客户是否为承运人
        /// </summary>
        public Boolean IsAgentOfCarrier
        {
            get;
            set;
        }
    }

    #endregion

    #region ChargeList

    /// <summary>
    /// 费用列表
    /// </summary>
    [Serializable]
    public partial class ChargeList : BaseDataObject
    {
        /// <summary>
        /// 账单号
        /// </summary>
        public string BillNo;
        /// <summary>
        ///  return ID == Guid.Empty; 
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        /// <summary>
        /// 客户端辅助字段
        /// </summary>
        public bool Selected { get; set; }

        public bool IsAllowEdit { get; set; }
        /// <summary>
        ///  this.Type = FeeType.Normal;this.Way = FeeWay.AR;
        /// </summary>
        public ChargeList()
        {
            this.Type = FeeType.Normal;
            this.Way = FeeWay.AR;
        }

        #region ID
        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        [Required(CMessage = "唯一键", EMessage = "ID")]
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

        #region BillID

        Guid _billid;
        /// <summary>
        /// 账单ID
        /// </summary>
        public Guid BillID
        {
            get
            {
                return _billid;
            }
            set
            {
                if (_billid != value)
                {

                    _billid = value;
                    this.NotifyPropertyChanged(o => o.BillID);
                }
            }
        }
        #endregion

        #region ChargingCode
        Guid _chargingcodeid;
        /// <summary>
        /// 费用项目ID
        /// </summary>
        [GuidRequired(CMessage = "费用项目", EMessage = "ChargingCode")]
        public Guid ChargingCodeID
        {
            get
            {
                return _chargingcodeid;
            }
            set
            {
                if (_chargingcodeid != value)
                {
                    _chargingcodeid = value;
                    this.NotifyPropertyChanged(o => o.ChargingCodeID);
                }
            }
        }

        string _chargingcode;
        /// <summary>
        /// 费用项目代码(名称)
        /// </summary>
        public string ChargingCode
        {
            get
            {
                return _chargingcode;
            }
            set
            {
                if (_chargingcode != value)
                {
                    _chargingcode = value;
                    this.NotifyPropertyChanged(o => o.ChargingCode);
                }
            }
        }

        string _feecode;
        /// <summary>
        /// 费用项目代码
        /// </summary>
        public string FeeCode
        {
            get
            {
                return _feecode;
            }
            set
            {
                if (_feecode != value)
                {
                    _feecode = value;
                    this.NotifyPropertyChanged(o => o.FeeCode);
                }
            }
        }

        string _chargingDescription;
        /// <summary>
        /// 费用项目描述
        /// </summary>
        public string ChargingDescription
        {
            get
            {
                return _chargingDescription;
            }
            set
            {
                if (_chargingDescription != value)
                {
                    _chargingDescription = value;
                    this.NotifyPropertyChanged(o => o.ChargingDescription);
                }
            }
        }

        #endregion

        #region Currency

        Guid _currencyid;
        /// <summary>
        /// 币种ID
        /// </summary>
        [GuidRequired(CMessage = "币种", EMessage = "Currency")]
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
        #endregion

        #region Rate

        decimal _rate;
        /// <summary>
        /// 费用币种对应账单币种的汇率
        /// </summary>
        [Required(CMessage = "费用币种对应账单币种的汇率", EMessage = "Rate")]
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
        #endregion

        #region Unit

        Guid _unitid;
        /// <summary>
        /// 计量单位ID（票和计费吨在数据字典中，箱型在箱信息表中）
        /// </summary>
        [GuidRequired(CMessage = "计量单位", EMessage = "Unit")]
        public Guid UnitID
        {
            get
            {
                return _unitid;
            }
            set
            {
                if (_unitid != value)
                {
                    _unitid = value;
                    this.NotifyPropertyChanged(o => o.UnitID);
                }
            }
        }

        string _unitname;
        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitName
        {
            get
            {
                return _unitname;
            }
            set
            {
                if (_unitname != value)
                {
                    _unitname = value;
                    this.NotifyPropertyChanged(o => o.UnitName);
                }
            }
        }
        #endregion

        #region UnitPrice

        decimal _unitprice;
        /// <summary>
        /// 单价
        /// </summary>
        [DecimalRequired(CMessage = "单价", EMessage = "UnitPrice")]
        public decimal UnitPrice
        {
            get
            {
                return _unitprice;
            }
            set
            {
                if (_unitprice != value)
                {
                    _unitprice = value;
                    this.NotifyPropertyChanged(o => o.UnitPrice);
                }
            }
        }
        #endregion

        #region Quantity

        decimal _quantity;
        /// <summary>
        /// 数量
        /// </summary>
        [DecimalRequired(CMessage = "数量", EMessage = "Quantity")]
        public decimal Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    this.NotifyPropertyChanged(o => o.Quantity);
                }
            }
        }
        #endregion

        #region Amount

        decimal _amount;
        /// <summary>
        /// 总额
        /// </summary>
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

        #region 帐单金额()
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

        #region 已销账
        /// <summary>
        /// 已销账金额
        /// </summary>
        public decimal PayAmount
        {
            get;
            set;
        }
        #endregion

        #region Remark
        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "备注", EMessage = "Remark")]
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

        public string ChargeName
        {
            get;
            set;
        }

        public DateTime? BillUpdateDate
        {
            get;
            set;
        }

        /// <summary>
        /// 是否为佣金
        /// </summary>
        public bool IsCommission { get; set; }

        #region Create Info

        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        [Required(CMessage = "建立人", EMessage = "CreateBy")]
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
        /// 建立时间
        /// </summary>
        [Required(CMessage = "建立时间", EMessage = "CreateDate")]
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

        DateTime? _updateDate;
        /// <summary>
        /// 更新时间-做数据版本控制用
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                if (_updateDate != value)
                {
                    _updateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }
        #endregion

        #region FeeWay

        FeeWay _way;
        /// <summary>
        /// 方向（1:应收,2:应付）
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

        #region FeeType

        FeeType _type;
        /// <summary>
        /// 类型（1:正常,2:分摊,3:运价）
        /// </summary>
        public FeeType Type
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

        #endregion

        #region IsAgent

        bool _isagent;
        /// <summary>
        /// 是否代收代付费用
        /// </summary>
        public bool IsAgent
        {
            get
            {
                return _isagent;
            }
            set
            {
                if (_isagent != value)
                {
                    _isagent = value;
                    this.NotifyPropertyChanged(o => o.IsAgent);
                }
            }
        }

        #endregion

        #region IsSecondSale

        bool _issecondsale;
        /// <summary>
        /// 是否为2次销售
        /// </summary>
        public bool IsSecondSale
        {
            get
            {
                return _issecondsale;
            }
            set
            {
                if (_issecondsale != value)
                {
                    _issecondsale = value;
                    this.NotifyPropertyChanged(o => o.IsSecondSale);
                }
            }
        }

        #endregion

        #region 已开票金额InvoiceAmount

        decimal _invoiceAmount;
        /// <summary>
        /// 已开票金额
        /// </summary>
        public decimal InvoiceAmount
        {
            get
            {
                return _invoiceAmount;
            }
            set
            {
                if (_invoiceAmount != value)
                {
                    _invoiceAmount = value;
                    this.NotifyPropertyChanged(o => o.InvoiceAmount);
                }
            }
        }

        #endregion

        #region 费用的来源1为海出2为海进 joe 2013-08-21
        /// <summary>
        ///  费用的来源1为海出2为海进 
        /// </summary>
        public int? FromType
        {
            get;
            set;
        }
        #endregion

        #region 费用的来源1为海出2为海进 joe 2013-08-21
        /// <summary>
        ///  费用的来源1为海出2为海进 
        /// </summary>
        public bool IsCancel
        {
            get;
            set;
        }
        #endregion

        #region 是否开增值税发票
        private bool isVATInvoiced;
        /// <summary>
        /// 是否开增值税发票
        /// </summary>
        public bool IsVATInvoiced
        {
            get
            {
                return isVATInvoiced;
            }
            set
            {
                if (isVATInvoiced != value)
                {
                    isVATInvoiced = value;
                    this.NotifyPropertyChanged(o => o.IsVATInvoiced);
                }
            }
        }
        #endregion


        #region 马来西亚是否开发票
        private bool isGST;
        /// <summary>
        /// 马来西亚是否开发票
        /// </summary>
        public bool IsGST
        {
            get
            {
                return isGST;
            }
            set
            {
                if (isGST != value)
                {
                    isGST = value;
                    this.NotifyPropertyChanged(o => o.IsGST);
                }
            }
        }
        #endregion

        /// <summary>
        /// 是否已分发
        /// </summary>
        public bool IsDispatch
        {
            get;
            set;
        }


        /// <summary>
        /// 税率(马来西亚报表显示使用)
        /// </summary>
        public decimal TaxRate
        {
            get;
            set;
        }

        #region 选择的业务信息
        /// <summary>
        /// 选择的业务信息
        /// </summary>
        public OperationCommonInfo ChooseOperationInfo
        {
            get;
            set;
        }
        #endregion

        #region OperationNo(业务号)
        string _operationno;
        /// <summary>
        /// 业务号码
        /// </summary>
        public string OperationNo
        {
            get
            {
                return _operationno;
            }
            set
            {
                if (_operationno != value)
                {
                    _operationno = value;
                    this.NotifyPropertyChanged(o => o.OperationNo);
                }
            }
        }

        #endregion

        #region 柜号(ContainerNo)

        string _containerNo;
        /// <summary>
        /// 柜号(ContainerNo)
        /// </summary>
        public string ContainerNo
        {
            get
            {
                return _containerNo;
            }
            set
            {
                if (_containerNo != value)
                {
                    _containerNo = value;
                    this.NotifyPropertyChanged(o => o.ContainerNo);
                }
            }
        }
        #endregion

        #region 柜号ID(ContainerID)

        Guid? _containerid;
        /// <summary>
        /// 柜号ID(ContainerID)
        /// </summary>
        public Guid? ContainerID
        {
            get
            {
                return _containerid;
            }
            set
            {
                if (_containerid != value)
                {
                    _containerid = value;
                    this.NotifyPropertyChanged(o => o.ContainerID);
                }
            }
        }
        #endregion

    }

    /// <summary>
    /// 费用明细
    /// </summary>
    [Serializable]
    public class ChargesInfo : ChargeList
    {


    }

    /// <summary>
    /// 批量帐单数据对象
    /// </summary>
    [Serializable]
    public partial class BatchChargeList : BaseDataObject
    {
        /// <summary>
        ///  return ID == Guid.Empty; 
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        [Required(CMessage = "唯一键", EMessage = "ID")]
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

        #region Operation

        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNO { get; set; }

        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }

        #endregion

        decimal _amount;
        /// <summary>
        /// 应付金额
        /// </summary>
        [DecimalRequired(CMessage = "金额", EMessage = "Amount")]
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

        #region 利润

        /// <summary>
        /// 调整后利润
        /// </summary>
        public decimal Profit { get; set; }

        decimal _originalProfit;
        /// <summary>
        /// 利润
        /// </summary>
        public decimal OriginalProfit
        {
            get
            {
                return _originalProfit;
            }
            set
            {
                if (_originalProfit != value)
                {
                    _originalProfit = value;
                    this.NotifyPropertyChanged(o => o.OriginalProfit);
                }
            }
        }

        #endregion
    }

    #endregion

    #region CurrencyBillData 帐单列表对象

    /// <summary>
    /// 业务列表对象
    /// </summary>
    [Serializable]
    public partial class CurrencyBillList : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        #region  ID
        Guid _id;
        /// <summary>
        /// 账单ID
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

        #region 唯一键(账单ID+收付类型+币种+是否佣金)
        /// <summary>
        /// 当前数据的唯一键
        /// </summary>
        public string CurrentID
        {
            get
            {
                return ID.ToString() + "&&" + Way.ToString() + "&&" + CurrencyID.ToString() + "&&" + IsCommission.ToString();
            }
        }

        #endregion

        #region 帐单号 BillNO
        string _BillNO;
        /// <summary>
        /// 帐单号
        /// </summary>
        public string BillNO
        {
            get { return _BillNO; }
            set
            {
                if (_BillNO != value)
                {
                    _BillNO = value;
                    this.NotifyPropertyChanged(o => o.BillNO);
                }
            }
        }
        #endregion

        #region 提单号
        /// <summary>
        /// 提单号
        /// </summary>
        public string BLNo
        {
            get;
            set;
        }
        #endregion

        #region 业务类型
        private OperationType operType;
        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperType
        {
            get
            {
                return operType;
            }
            set
            {
                if (operType != value)
                {
                    operType = value;
                    this.NotifyPropertyChanged(o => o.OperType);
                }
            }
        }
        #endregion

        #region BillType

        BillType _BillType;
        /// <summary>
        /// 帐单类型（1:应收,2:应付,3:代理）
        /// </summary>
        public BillType BillType
        {
            get { return _BillType; }
            set
            {
                if (_BillType != value)
                {
                    _BillType = value;
                    this.NotifyPropertyChanged(o => o.BillType);
                }
            }
        }

        #endregion

        #region 收付类型
        /// <summary>
        /// 收付类型
        /// </summary>
        public FeeWay Way
        {
            get;
            set;
        }
        #endregion

        #region 业务ID

        Guid _operationID;
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID
        {
            get { return _operationID; }
            set
            {
                if (_operationID != value)
                {
                    _operationID = value;
                    this.NotifyPropertyChanged(o => o.OperationID);
                }
            }
        }

        #endregion

        #region 业务号 OperationNO
        string _operationNo;
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNO
        {
            get { return _operationNo; }
            set
            {
                if (_operationNo != value)
                {
                    _operationNo = value;
                    this.NotifyPropertyChanged(o => o.OperationNO);
                }
            }
        }

        #endregion

        #region 币种

        Guid _currencyID;
        /// <summary>
        /// 币种ID
        /// </summary>
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


        #endregion

        #region 客户

        Guid _CustomerID;
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID
        {
            get
            {
                return _CustomerID;
            }
            set
            {
                if (_CustomerID != value)
                {
                    _CustomerID = value;
                    this.NotifyPropertyChanged(o => o.CustomerID);
                }
            }
        }

        string _CustomerName;
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get
            {
                return _CustomerName;
            }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    this.NotifyPropertyChanged(o => o.CustomerName);
                }
            }
        }
        public string CustomerCName { get; set; }
        public string CustomerEName { get; set; }
        /// <summary>
        /// 客户税务号
        /// </summary>
        public string CustomerTaxNo
        {
            get;
            set;
        }
        /// <summary>
        /// 客户电话地址
        /// </summary>
        public string CustomerAddressTel
        {
            get;
            set;
        }
        /// <summary>
        /// 客户银行帐号
        /// </summary>
        public string CustomerBankAccountNo
        {
            get;
            set;
        }
        #endregion

        #region 公司
        Guid _CompanyID;
        /// <summary>
        /// CompanyID
        /// </summary>
        public Guid CompanyID
        {
            get
            {
                return _CompanyID;
            }
            set
            {
                if (_CompanyID != value)
                {
                    _CompanyID = value;
                    this.NotifyPropertyChanged(o => o.CompanyID);
                }
            }
        }
        string _CompanyName;
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get
            {
                return _CompanyName;
            }
            set
            {
                if (_CompanyName != value)
                {
                    _CompanyName = value;
                    this.NotifyPropertyChanged(o => o.CompanyName);
                }
            }
        }

        #endregion

        #region 金额 Amount
        decimal _Amount;
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount
        {
            get
            {
                if (Way == FeeWay.AP)
                {
                    return 0 - _Amount;
                }
                else
                {
                    return _Amount;
                }
            }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    this.NotifyPropertyChanged(o => o.Amount);
                }
            }
        }


        #endregion

        #region 已核销金额 WriteOffAmount
        decimal _writeOffAmount;
        /// <summary>
        /// 已核销金额
        /// </summary>
        public decimal WriteOffAmount
        {
            get
            {
                if (Way == FeeWay.AP)
                {
                    return 0 - _writeOffAmount;
                }
                else
                {
                    return _writeOffAmount;
                }
            }
            set
            {
                if (_writeOffAmount != value)
                {
                    _writeOffAmount = value;
                    this.NotifyPropertyChanged(o => o.WriteOffAmount);
                }
            }
        }

        #endregion

        #region 未核销金额描述

        string _BalanceDescription;
        /// <summary>
        /// 未核销金额描述
        /// </summary>
        public string BalanceDescription
        {
            get
            {
                return _BalanceDescription;
            }
            set
            {
                if (_BalanceDescription != value)
                {
                    _BalanceDescription = value;
                    this.NotifyPropertyChanged(o => o.BalanceDescription);
                }
            }
        }

        #endregion

        #region 账单参考号
        private string _billRefNO;
        /// <summary>
        /// 帐单参考号
        /// </summary>
        public string BillRefNO
        {
            get
            {
                return _billRefNO;
            }
            set
            {
                if (_billRefNO != value)
                {
                    _billRefNO = value;
                    this.NotifyPropertyChanged(o => o.BillRefNO);
                }
            }
        }

        #endregion

        #region 客户参考号
        private string _customerRefNo;
        /// <summary>
        /// 客户参考号
        /// </summary>
        public string CustomerRefNo
        {
            get
            {
                return _customerRefNo;
            }
            set
            {
                if (_customerRefNo != value)
                {
                    _customerRefNo = value;
                    this.NotifyPropertyChanged(o => o.CustomerRefNo);
                }
            }
        }

        #endregion

        #region 审核
        private string _checkBy;
        /// <summary>
        /// 审核人
        /// </summary>
        public string CheckBy
        {
            get
            {
                return _checkBy;
            }
            set
            {
                if (_checkBy != value)
                {
                    _checkBy = value;
                    this.NotifyPropertyChanged(o => o.CheckBy);
                }
            }
        }
        #endregion

        #region 审核日期
        private DateTime? _checkDate;
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? CheckDate
        {
            get
            {
                return _checkDate;
            }
            set
            {
                if (_checkDate != value)
                {
                    _checkDate = value;
                    this.NotifyPropertyChanged(o => o.CheckDate);
                }
            }
        }
        #endregion

        #region 已审核
        private bool _checked;
        /// <summary>
        /// 已审核
        /// </summary>
        public bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    this.NotifyPropertyChanged(o => o.Checked);
                }
            }
        }

        #endregion

        #region 到账

        /// <summary>
        /// 到账
        /// </summary>
        public bool IsPaid
        {
            get
            {
                return Paid == PaidStatus.All || Paid == PaidStatus.Part;
            }
        }

        /// <summary>
        /// 到账状态
        /// </summary>
        public PaidStatus Paid
        {
            get;
            set;
        }
        #endregion

        #region 计费日期
        private DateTime? accountDate;
        /// <summary>
        /// 计费日期
        /// </summary>
        public DateTime? AccountDate
        {
            get
            {
                return accountDate;
            }
            set
            {
                if (accountDate != value)
                {
                    accountDate = value;
                    this.NotifyPropertyChanged(o => o.AccountDate);
                }
            }
        }

        #endregion

        #region 制单人
        /// <summary>
        /// 制单人ID
        /// </summary>
        public Guid CreateBy { get; set; }

        /// <summary>
        /// 制单人
        /// </summary>
        public string CreateByName { get; set; }

        #endregion

        #region 发票号

        private string _invoiceNo;
        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNo
        {
            get
            {
                return _invoiceNo;
            }
            set
            {
                if (_invoiceNo != value)
                {
                    _invoiceNo = value;
                    this.NotifyPropertyChanged(o => o.InvoiceNo);
                }
            }
        }
        #endregion

        #region 快递单号
        string _ExpressNo;
        /// <summary>
        ///快递单号
        /// </summary>
        public string ExpressNo
        {
            get { return _ExpressNo; }
            set
            {
                if (_ExpressNo != value)
                {
                    _ExpressNo = value;
                    this.NotifyPropertyChanged(o => o.ExpressNo);
                }
            }
        }
        #endregion

        #region 选择
        /// <summary>
        /// 选择
        /// </summary>
        public bool Selected { get; set; }
        #endregion

        #region 状态
        private BillState _state;
        /// <summary>
        /// 状态
        /// </summary>
        public BillState State
        {
            get
            {
                return _state;
            }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    this.NotifyPropertyChanged(o => o.State);
                }
            }
        }
        #endregion

        #region 最后更新日期
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime? UpdateDate
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

        #region RBLD
        /// <summary>
        /// RBLD
        /// </summary>
        public bool RBLD
        {
            get;
            set;
        }
        #endregion

        #region 支付币种
        /// <summary>
        /// 支付币种ID
        /// </summary>
        public Guid? PayCurrencyID
        {
            get;
            set;
        }
        /// <summary>
        /// 支付币种名称
        /// </summary>
        public string PayCurrencyName
        {
            get;
            set;
        }
        #endregion

        #region 汇率
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate
        {
            get;
            set;
        }
        #endregion

        #region 付款方式
        /// <summary>
        /// 对应业务的付款方式
        /// </summary>
        public short PayType
        {
            get
            {
                if (PayTypeName == "PP")
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
        /// <summary>
        ///付款方式名称
        /// </summary>
        public string PayTypeName
        {
            get;
            set;
        }
        #endregion

        #region 账单列表状态

        /// <summary>
        /// 账单状态(账单列表显示用)
        /// </summary>
        public string BillListState
        {
            get
            {
                if (this.State == BillState.Paid)
                {
                    return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription(this.Paid, LocalData.IsEnglish);
                }

                if (this.WriteOffAmount != 0 && Math.Abs(this.WriteOffAmount) < Math.Abs(this.Amount))
                {
                    return LocalData.IsEnglish ? "PartWriteOff" : "部分销账";
                }

                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription(this.State, LocalData.IsEnglish);
            }
        }

        #endregion

        #region 只是打印纽约支票模板用到
        /// <summary>
        /// 只是打印纽约支票模板用到
        /// </summary>
        public string BillOrCustomerRefNo
        {
            get;
            set;
        }
        #endregion

        #region 信用期限

        int? _creditDate;
        /// <summary>
        /// 信用期限
        /// </summary>
        public int? CreditDate
        {
            get
            {
                return _creditDate;
            }
            set
            {
                if (_creditDate != value)
                {
                    _creditDate = value;

                    this.NotifyPropertyChanged(o => o.CreditDate);
                }
            }
        }

        #endregion

        #region 到期日

        DateTime _duedate;
        /// <summary>
        /// 到期日
        /// </summary>
        [Required(CMessage = "到期日", EMessage = "DueDate")]
        public DateTime DueDate
        {
            get
            {
                return _duedate;
            }
            set
            {
                if (_duedate != value)
                {
                    _duedate = value;
                    this.NotifyPropertyChanged(o => o.DueDate);
                }
            }
        }
        #endregion

    }

    #endregion

    #region 业务信息(开发票时使用)
    /// <summary>
    /// 业务信息(开发时使用)
    /// </summary>
    [Serializable]
    public class BusinessByInvoice
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID
        {
            get;
            set;
        }

        /// <summary>
        /// 公司名称
        /// </summary>
        public String CompanyName { get; set; }

        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public String OperationNo { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType Opertype { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID { get; set; }
        /// <summary>
        /// 客户中文名称
        /// </summary>
        public String CustomerCName { get; set; }
        /// <summary>
        /// 客户英文名称
        /// </summary>
        public String CustomerEName { get; set; }
        /// <summary>
        /// 客户地址/电话
        /// </summary>
        public String CustomerAddressTel { get; set; }
        /// <summary>
        /// 客户银行帐号
        /// </summary>
        public String CustomerBankAccountNo { get; set; }
        /// <summary>
        /// 客户税务号
        /// </summary>
        public String CustomerTaxIDNo { get; set; }
        /// <summary>
        /// ETA
        /// </summary>
        public DateTime? ETA { get; set; }
        /// <summary>
        /// ETD
        /// </summary>
        public DateTime? ETD { get; set; }
        /// <summary>
        /// 装货港
        /// </summary>
        public String POLName { get; set; }
        /// <summary>
        /// 卸货港
        /// </summary>
        public String PODName { get; set; }
        /// <summary>
        /// 箱号
        /// </summary>
        public String CtnNo { get; set; }
        /// <summary>
        /// 箱型
        /// </summary>
        public String CtnTypeName { get; set; }
        /// <summary>
        /// 订舱号
        /// </summary>
        public String SoNo { get; set; }
        /// <summary>
        /// 交货地
        /// </summary>
        public string PlaceOfDeliveryName { get; set; }
        /// <summary>
        /// 提单号
        /// </summary>
        public String BLNo { get; set; }
        /// <summary>
        /// 船名航次
        /// </summary>
        public String VesselVoyage { get; set; }

        public String Vessel { get; set; }
        public String Voyage { get; set; }

    }
    #endregion

    #region FAMCustomerDescription
    /// <summary>
    /// 客户描述信息
    /// </summary>
    [Serializable]
    public class FAMCustomerDescription
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FAMCustomerDescription()
        {
            this.Name = string.Empty;
            this.Address = string.Empty;
            this.Tel = string.Empty;
            this.Fax = string.Empty;
        }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [XmlElement("Address")]
        public string Address { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [XmlElement("Tel")]
        public string Tel { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [XmlElement("Fax")]
        public string Fax { get; set; }

        /// <summary>
        /// 转换为字符串描述
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        /// <returns>返回字符串描述</returns>
        public string ToString(bool isEnglish)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(this.Name) == false)
            {
                sb.AppendLine(this.Name);
                sb.AppendLine(this.Address);
                sb.AppendLine("TEL:" + this.Tel);
                sb.AppendLine("FAX:" + this.Fax);
            }
            return sb.ToString();
        }
    }

    #endregion

    #region 获取账单列表返回结果集

    /// <summary>
    /// 获取账单列表返回结果集
    /// </summary>
    [Serializable]
    [KnownType(typeof(CurrencyBillList))]
    [KnownType(typeof(PageList))]
    public class BillListAllData
    {
        public PageList PageList { get; set; }
        public List<BillListTotalInfo> TotalInfoList { get; set; }
    }

    #endregion

    #region 账单列表合计信息

    /// <summary>
    /// 账单列表合计信息
    /// </summary>
    [Serializable]
    public class BillListTotalInfo
    {
        public Guid CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        public decimal DRAmount { get; set; }
        public decimal CRAmount { get; set; }
        public decimal Balance { get; set; }
    }

    #endregion

    #region  财务账单列表 页面加载查询类
    /// <summary>
    /// 财务账单列表 页面加载查询类
    /// </summary>
    public class BillListQueryCriteria
    {
        public List<Guid> OperationIds { get; set; }
    }
    #endregion

    #region AgingReport
    /// <summary>
    /// AgingReportData
    /// </summary>
    [Serializable]
    public class CustomerAgingList
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
        /// CustomerID
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        public decimal Balance { get; set; }
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

        /// <summary>
        /// 货值
        /// </summary>
        public decimal ShptWorth { get; set; }

        /// <summary>
        /// 逾期
        /// </summary>
        public decimal PastDu { get; set; }

        /// <summary>
        /// 逾期30天
        /// </summary>
        public decimal PastDu30 { get; set; }

        /// <summary>
        /// 逾期多天
        /// </summary>
        public decimal PastDueAmount { get; set; }

        /// <summary>
        /// 最后日志
        /// </summary>
        public string LastMemo { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastUpdate { get; set; }
        /// <summary>
        /// 最后更新人
        /// </summary>
        public string LastUpdateBy { get; set; }
    }
    #endregion

    #region AgingLogAtts
    /// <summary>
    /// AgingLogAtts
    /// </summary>
    [Serializable]
    public class CustomerAgingLogs
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public Guid CompanyID { get; set; }

        /// <summary>
        /// CustomerID
        /// </summary>
        public Guid CustomerID { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// CustomerMark
        /// </summary>
        public byte CustomerMark { get; set; }

        /// <summary>
        /// MemoTime
        /// </summary>
        public DateTime MemoTime { get; set; }

        /// <summary>
        /// Subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        public byte Priority { get; set; }

        /// <summary>
        /// CreateBy
        /// </summary>
        public Guid CreateBy { get; set; }
        /// <summary>
        /// CreateByName
        /// </summary>
        public string CreateByName { get; set; }

        /// <summary>
        /// 日志列表
        /// </summary>
        public List<CustomerAgingLogAtts> logAtts { get; set; }
    }
    #endregion

    #region AgingLogAtts
    /// <summary>
    /// AgingLogAtts
    /// </summary>
    [Serializable]
    public class CustomerAgingLogAtts
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// AgingLogID
        /// </summary>
        public Guid AgingLogID { get; set; }

        /// <summary>
        /// FileID
        /// </summary>
        public Guid FileID { get; set; }

        /// <summary>
        /// FileName
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// CreateByID
        /// </summary>
        public Guid CreateBy { get; set; }

        /// <summary>
        /// CreateBy
        /// </summary>
        public string CreateByName { get; set; }

        /// <summary>
        /// CreateOn
        /// </summary>
        public DateTime CreateOn { get; set; }

        /// <summary>
        /// filebyte
        /// </summary>
        public byte[] filebyte { get; set; }

    }
    #endregion

    #region LocalFeeConfigure
    /// <summary>
    /// LocalFeeConfigure
    /// </summary>
    [Serializable]
    public class LocalFeeConfigure
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int No { get; set; }

        /// <summary>
        /// IsCarrier
        /// </summary>
        public bool IsCarrier { get; set; }

        /// <summary>
        /// CarrierID
        /// </summary>
        public Guid[] CarrierIDs { get; set; }

        /// <summary>
        /// CarrierName
        /// </summary>
        public string CarrierNames { get; set; }

        /// <summary>
        /// IsLocation
        /// </summary>
        public bool IsLocation { get; set; }

        /// <summary>
        /// LocationIDs
        /// </summary>
        public Guid[] LocationIDs { get; set; }

        /// <summary>
        /// LocationNames
        /// </summary>
        public string LocationNames { get; set; }

        /// <summary>
        /// IsShippingLine
        /// </summary>
        public bool IsShippingLine { get; set; }

        /// <summary>
        /// ShippingLineIDs
        /// </summary>
        public Guid[] ShippingLineIDs { get; set; }

        /// <summary>
        /// ShippingLineNames
        /// </summary>
        public string ShippingLineNames { get; set; }

        /// <summary>
        /// IsShippingLine
        /// </summary>
        public bool IsCommpany { get; set; }

        /// <summary>
        /// CompanyIDs
        /// </summary>
        public Guid[] CompanyIDs { get; set; }

        /// <summary>
        /// CompanyNames
        /// </summary>
        public string CompanyNames { get; set; }

        /// <summary>
        /// ChargeID
        /// </summary>
        public Guid ChargeID { get; set; }

        /// <summary>
        /// CompanyName
        /// </summary>
        public string ChargeName { get; set; }

        /// <summary>
        /// CurrencyID
        /// </summary>
        public Guid CurrencyID { get; set; }

        /// <summary>
        /// CurrencyName
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public string Prices { get; set; }

        /// <summary>
        /// ChargeUnit
        /// </summary>
        public byte ChargeUnit { get; set; }

        /// <summary>
        /// ChargeUnit
        /// </summary>
        public string ChargeUnitName { get; set; }

        /// <summary>
        /// StartDate
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// EndDate
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// IsValid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// CreateByID
        /// </summary>
        public Guid CreateBy { get; set; }

        /// <summary>
        /// CreateBy
        /// </summary>
        public string CreateByName { get; set; }

        /// <summary>
        /// CreateDate
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// UpdateBy
        /// </summary>
        public Guid? UpdateBy { get; set; }

        /// <summary>
        /// UpdateByName
        /// </summary>
        public string UpdateByName { get; set; }

        /// <summary>
        /// UpdateDate
        /// </summary>
        public DateTime? UpdateDate { get; set; }

    }
    #endregion

    #region AddLocalFeeList
    /// <summary>
    /// AddLocalFeeList
    /// </summary>
    [Serializable]
    public class AddLocalFeeList
    {
        /// <summary>
        /// CarrierID
        /// </summary>
        public Guid ChargeID { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// ChargeEname
        /// </summary>
        public string ChargeEname { get; set; }

        /// <summary>
        /// ChargeCname
        /// </summary>
        public string ChargeCname { get; set; }

        /// <summary>
        /// CurrencyCode
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// CurrencyID
        /// </summary>
        public Guid CurrencyID { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// qty
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// CustomerID
        /// </summary>
        public Guid CustomerID { get; set; }

        /// <summary>
        /// IsShippingLine
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Way
        /// </summary>
        public int Way { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// BillID
        /// </summary>
        public Guid? BillID { get; set; }

        /// <summary>
        /// FeeID
        /// </summary>
        public Guid? FeeID { get; set; }

        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo { get; set; }

        /// <summary>
        /// BillAmount
        /// </summary>
        public decimal BillAmount { get; set; }

        /// <summary>
        /// CompanyNames
        /// </summary>
        public int BillWay { get; set; }

        /// <summary>
        /// IsSelected
        /// </summary>
        public bool IsSelected { get; set; }
    }
    #endregion

    /// <summary>
    /// ConTypes
    /// </summary>
    [Serializable]
    public class ConTypes
    {
        public decimal _20FR { get; set; }
        public decimal _20GP { get; set; }
        public decimal _20HQ { get; set; }
        public decimal _20HT { get; set; }
        public decimal _20NOR { get; set; }
        public decimal _20OT { get; set; }
        public decimal _20RF { get; set; }
        public decimal _20RH { get; set; }
        public decimal _20TK { get; set; }
        public decimal _4OFR { get; set; }
        public decimal _40GP { get; set; }
        public decimal _40HQ { get; set; }
        public decimal _40HT { get; set; }
        public decimal _40NOR { get; set; }
        public decimal _40OT { get; set; }
        public decimal _40RF { get; set; }
        public decimal _4ORH { get; set; }
        public decimal _4OTK { get; set; }
        public decimal _45FR { get; set; }
        public decimal _45GP { get; set; }
        public decimal _45HQ { get; set; }
        public decimal _45HP { get; set; }
        public decimal _45HT { get; set; }
        public decimal _45OT { get; set; }
        public decimal _45RF { get; set; }
        public decimal _45RH { get; set; }
        public decimal _45TK { get; set; }
        public decimal _53HQ { get; set; }
    }
}
