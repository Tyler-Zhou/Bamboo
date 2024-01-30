using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    /// <summary>
    /// 发票列表
    /// </summary>
    [Serializable]
    public class InvoiceList : BaseDataObject
    {
        /// <summary>
        /// 根据主键判断是否新增数据
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        /// <summary>
        /// 已选择,客户端帮助属性
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// 发票列表
        /// </summary>
        public InvoiceList()
        {

        }
        #region ID
        Guid _ID;
        /// <summary>
        ///ID
        /// </summary>
        public Guid ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }
        #endregion

        #region No
        public string No
        {
            get;
            set;
        }
        #endregion

        #region CustomerAddressTel
        private string customerAddressTel;
        /// <summary>
        /// 客户地址&电话
        /// </summary>
        public string CustomerAddressTel
        {
            get { return customerAddressTel; }
            set
            {
                if (customerAddressTel != value)
                {
                    customerAddressTel = value;
                    this.NotifyPropertyChanged(o => o.CustomerAddressTel);
                }
            }
        }

        #endregion

        #region CustomerBankAccountNo
        private string customerBankAccountNo;
        /// <summary>
        /// 客户银行帐号
        /// </summary>
        public string CustomerBankAccountNo
        {
            get { return customerBankAccountNo; }
            set
            {
                if (customerBankAccountNo != value)
                {
                    customerBankAccountNo = value;
                    this.NotifyPropertyChanged(o => o.customerBankAccountNo);
                }
            }
        }
        #endregion

        #region CustomerTaxIDNo
        private string customerTaxIDNo;
        /// <summary>
        /// 客户税号
        /// </summary>
        [Required(CMessage = "客户税务号", EMessage = "CustomerTaxNo")]
        public string CustomerTaxIDNo
        {
            get { return customerTaxIDNo; }
            set
            {
                if (customerTaxIDNo != value)
                {
                    customerTaxIDNo = value;
                    this.NotifyPropertyChanged(o => o.CustomerTaxIDNo);
                }
            }
        }
        #endregion

        #region InvoiceNo
        string _InvoiceNo;
        /// <summary>
        ///发票号
        /// </summary>
        public string InvoiceNo
        {
            get { return _InvoiceNo; }
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

        #region InvoiceDate
        DateTime _InvoiceDate;
        /// <summary>
        ///InvoiceDate
        /// </summary>
        public DateTime InvoiceDate
        {
            get { return _InvoiceDate; }
            set
            {
                if (_InvoiceDate != value)
                {
                    _InvoiceDate = value;
                    this.NotifyPropertyChanged(o => o.InvoiceDate);
                }
            }
        }
        #endregion

        #region CustomerName
        string _CustomerName;
        /// <summary>
        ///CustomerName
        /// </summary>
        public string CustomerName
        {
            get { return _CustomerName; }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    this.NotifyPropertyChanged(o => o.CustomerName);
                }
            }
        }
        #endregion

        #region 发票抬头
        string _InvoiceTitle;
        /// <summary>
        ///发票抬头
        /// </summary>
        public string InvoiceTitle
        {
            get { return _InvoiceTitle; }
            set
            {
                if (_InvoiceTitle != value)
                {
                    _InvoiceTitle = value;
                    this.NotifyPropertyChanged(o => o.InvoiceTitle);
                }
            }
        }
        #endregion

        #region Amount
        string _Amounts;
        /// <summary>
        ///Amount
        /// </summary>
        public string Amounts
        {
            get { return _Amounts; }
            set
            {
                if (_Amounts != value)
                {
                    _Amounts = value;
                    this.NotifyPropertyChanged(o => o.Amounts);
                }
            }
        }
        #endregion

        #region BillNo
        string _BillNo;
        /// <summary>
        ///BillNo
        /// </summary>
        public string BillNo
        {
            get { return _BillNo; }
            set
            {
                if (_BillNo != value)
                {
                    _BillNo = value;
                    this.NotifyPropertyChanged(o => o.BillNo);
                }
            }
        }
        #endregion

        #region BLNo
        string _BLNo;
        /// <summary>
        ///BLNo
        /// </summary>
        public string BLNo
        {
            get { return _BLNo; }
            set
            {
                if (_BLNo != value)
                {
                    _BLNo = value;
                    this.NotifyPropertyChanged(o => o.BLNo);
                }
            }
        }
        #endregion

        #region ETD
        DateTime? _ETD;
        /// <summary>
        ///ETD
        /// </summary>
        public DateTime? ETD
        {
            get { return _ETD; }
            set
            {
                if (_ETD != value)
                {
                    _ETD = value;
                    this.NotifyPropertyChanged(o => o.ETD);
                }
            }
        }
        #endregion

        #region ExpressNo
        string _ExpressNo;
        /// <summary>
        ///ExpressNo
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

        #region ExpressDate
        DateTime? _ExpressDate;
        /// <summary>
        ///ExpressDate
        /// </summary>
        public DateTime? ExpressDate
        {
            get { return _ExpressDate; }
            set
            {
                if (_ExpressDate != value)
                {
                    _ExpressDate = value;
                    this.NotifyPropertyChanged(o => o.ExpressDate);
                }
            }
        }
        #endregion

        #region Remark
        string _Remark;
        /// <summary>
        ///Remark
        /// </summary>
        public string Remark
        {
            get { return _Remark; }
            set
            {
                if (_Remark != value)
                {
                    _Remark = value;
                    this.NotifyPropertyChanged(o => o.Remark);
                }
            }
        }
        #endregion

        #region Create Info

        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        //[GuidRequired(ErrorMessage = "建立人必须填写")]
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
                    {
                        _createbyid = value;
                        this.NotifyPropertyChanged(o => o.CreateByID);
                    }
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
                    {
                        _createbyname = value;
                        this.NotifyPropertyChanged(o => o.CreateByName);
                    }
                }
            }
        }

        DateTime? _createdate;
        /// <summary>
        /// 建立时间
        /// </summary>
        [Required(CMessage = "建立时间",EMessage="CreateDate")]
        public DateTime? CreateDate
        {
            get
            {
                return _createdate;
            }
            set
            {
                if (_createdate != value)
                {
                    {
                        _createdate = value;
                        this.NotifyPropertyChanged(o => o.CreateDate);
                    }
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
                    {
                        _updateDate = value;
                        this.NotifyPropertyChanged(o => o.UpdateDate);
                    }
                }
            }
        }
        #endregion

        #region IsValid

        bool _IsValid;
        /// <summary>
        ///_IsValid
        /// </summary>
        public bool IsValid
        {
            get { return _IsValid; }
            set
            {
                if (_IsValid != value)
                {
                    _IsValid = value;
                    this.NotifyPropertyChanged(o => o.IsValid);
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 发票信息
    /// </summary>
    [Serializable]
    public class InvoiceInfo : InvoiceList
    {
        #region SONo
        string _SONo;
        /// <summary>
        ///SONo
        /// </summary>
        public string SONo
        {
            get { return _SONo; }
            set
            {
                if (_SONo != value)
                {
                    _SONo = value;
                    this.NotifyPropertyChanged(o => o.SONo);
                }
            }
        }
        #endregion

        #region CtnTypeName
        string _CtnTypeName;
        /// <summary>
        ///CtnTypeName
        /// </summary>
        public string CtnTypeName
        {
            get { return _CtnTypeName; }
            set
            {
                if (_CtnTypeName != value)
                {
                    {
                        _CtnTypeName = value;
                        this.NotifyPropertyChanged(o => o.CtnTypeName);
                    }
                }
            }
        }
        #endregion

        #region CompanyID
        System.Guid _CompanyID;
        /// <summary>
        ///公司
        /// </summary>
        [GuidRequired(CMessage = "公司",EMessage="Company")]
        public System.Guid CompanyID
        {
            get { return _CompanyID; }
            set
            {
                if (_CompanyID != value)
                {
                    _CompanyID = value;
                    this.NotifyPropertyChanged(o => o.CompanyID);
                }
            }
        }
        #endregion

        #region CompanyName
        string _CompanyName;
        /// <summary>
        ///CompanyName
        /// </summary>
        public string CompanyName
        {
            get { return _CompanyName; }
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

        #region BankAccountNo
        string _BankAccountNo;
        /// <summary>
        ///CompanyName
        /// </summary>
        public string BankAccountNo
        {
            get { return _BankAccountNo; }
            set
            {
                if (_BankAccountNo != value)
                {
                    _BankAccountNo = value;
                    this.NotifyPropertyChanged(o => o.BankAccountNo);
                }
            }
        }
        #endregion 

        #region BusinessNo & TaxNo(打印发票用)

        public string BusinessNo {get; set;}

        public string TaxNo { get; set; }

        #endregion

        #region Bank1ID
        System.Guid _Bank1ID;
        /// <summary>
        ///Bank1ID
        /// </summary>
        [GuidRequired(CMessage = "银行1",EMessage="Bank1")]
        public System.Guid Bank1ID
        {
            get { return _Bank1ID; }
            set
            {
                if (_Bank1ID != value)
                {
                    _Bank1ID = value;
                    this.NotifyPropertyChanged(o => o.Bank1ID);
                }
            }
        }
        #endregion

        #region Bank1Name
        string _Bank1Name;
        /// <summary>
        ///Bank1Name
        /// </summary>
        public string Bank1Name
        {
            get { return _Bank1Name; }
            set
            {
                if (_Bank1Name != value)
                {
                    _Bank1Name = value;
                    this.NotifyPropertyChanged(o => o.Bank1Name);
                }
            }
        }
        #endregion

        #region Bank2ID
        System.Guid? _Bank2ID;
        /// <summary>
        ///Bank2ID
        /// </summary>
        public System.Guid? Bank2ID
        {
            get { return _Bank2ID; }
            set
            {
                if (_Bank2ID != value)
                {
                    _Bank2ID = value;
                    this.NotifyPropertyChanged(o => o.Bank2ID);
                }
            }
        }
        #endregion

        #region Bank2Name
        string _Bank2Name;
        /// <summary>
        ///Bank2Name
        /// </summary>
        public string Bank2Name
        {
            get { return _Bank2Name; }
            set
            {
                if (_Bank2Name != value)
                {
                    _Bank2Name = value;
                    this.NotifyPropertyChanged(o => o.Bank2Name);
                }
            }
        }
        #endregion

        #region Tax
        System.Decimal _Tax;
        /// <summary>
        ///Tax
        /// </summary>
        public System.Decimal Tax
        {
            get { return _Tax; }
            set
            {
                if (_Tax != value)
                {
                    _Tax = value;
                    this.NotifyPropertyChanged(o => o.Tax);
                }
            }
        }
        #endregion

        #region 装货港

        string _polname;
        /// <summary>
        /// 装货港
        /// </summary>
        public string POLName
        {
            get
            {
                return _polname;
            }
            set
            {
                if (_polname != value)
                {
                    {
                        _polname = value;
                        this.NotifyPropertyChanged(o => o.POLName);
                    }
                }
            }
        }

        #endregion

        #region 卸货港

        string _podname;
        /// <summary>
        /// 卸货港
        /// </summary>
        public string PODName
        {
            get
            {
                return _podname;
            }
            set
            {
                if (_podname != value)
                {
                    {
                        _podname = value;
                        this.NotifyPropertyChanged(o => o.PODName);
                    }
                }
            }
        }

        #endregion

        #region 交货地

        string _placeofdeliveryname;
        /// <summary>
        /// 交货地
        /// </summary>
        public string PlaceOfDeliveryName
        {
            get
            {
                return _placeofdeliveryname;
            }
            set
            {
                if (_placeofdeliveryname != value)
                {
                    {
                        _placeofdeliveryname = value;
                        this.NotifyPropertyChanged(o => o.PlaceOfDeliveryName);
                    }
                }
            }
        }

        #endregion

        #region 船名航次

        string _vessel;
        /// <summary>
        /// 船名
        /// </summary>
        public string Vessel
        {
            get
            {
                return _vessel;
            }
            set
            {
                if (_vessel != value)
                {
                    {
                        _vessel = value;
                        this.NotifyPropertyChanged(o => o.Vessel);
                    }
                }
            }
        }

        public String _voyage;

        /// <summary>
        /// 航次
        /// </summary>
        public String Voyage
        {
            get
            {
                return _voyage;
            }
            set
            {
                if (_voyage != value)
                {
                    {
                        _voyage = value;
                        this.NotifyPropertyChanged(o => o.Voyage);
                    }
                }
            }
        }

        #endregion

        #region 箱号

        string _ContainerNo;
        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNo
        {
            get
            {
                return _ContainerNo;
            }
            set
            {
                if (_ContainerNo != value)
                {
                    {
                        _ContainerNo = value;
                        this.NotifyPropertyChanged(o => o.ContainerNo);
                    }
                }
            }
        }

        #endregion

        #region 客户

        Guid? _CustomerID;
        /// <summary>
        /// CustomerID
        /// </summary>
        public Guid? CustomerID
        {
            get
            {
                return _CustomerID;
            }
            set
            {
                if (_CustomerID != value)
                {
                    {
                        _CustomerID = value;
                        this.NotifyPropertyChanged(o => o.CustomerID);
                    }
                }
            }
        }

        #endregion

        #region 抬头中英文名

        string _TitleCName;
        /// <summary>
        /// 抬头中文名
        /// </summary>
        [Required(CMessage = "发票抬头", EMessage = "IhvoiceTitle")]
        public string TitleCName
        {
            get
            {
                return _TitleCName;
            }
            set
            {
                if (_TitleCName != value)
                {
                    {
                        _TitleCName = value;
                        this.NotifyPropertyChanged(o => o.TitleCName);
                    }
                }
            }
        }

        string _TitleEName;
        /// <summary>
        /// 抬头英文名
        /// </summary>
        public string TitleEName
        {
            get
            {
                return _TitleEName;
            }
            set
            {
                if (_TitleEName != value)
                {
                    _TitleEName = value;
                    this.NotifyPropertyChanged(o => o.TitleEName);
                }
            }
        }

        #endregion

        #region 收款
        private string receivablesName;
        /// <summary>
        /// 收款人
        /// </summary>
        public string ReceivablesName
        {
            get
            {
                return receivablesName;
            }
            set
            {
                if (receivablesName != value)
                {
                    receivablesName = value;
                    this.NotifyPropertyChanged(o => o.ReceivablesName); 
                }
            }
        }
        #endregion

        #region 复核
        private string reviewName;
        /// <summary>
        /// 复核人
        /// </summary>
        public string ReviewName
        {
            get
            {
                return reviewName;
            }
            set
            {
                if (reviewName != value)
                {
                        reviewName = value;
                        this.NotifyPropertyChanged(o => o.ReviewName);
                }
            }
        }
        #endregion

        #region  发票类型
        private CustomerInvoiceType invoiceType;
        /// <summary>
        /// 发票类型
        /// </summary>
        [Required(CMessage = "发票类型", EMessage = "IhvoiceType")]
        public CustomerInvoiceType InvoiceType
        {
            get
            {
                return invoiceType;
            }
            set
            {
                if (invoiceType != value)
                {
                    invoiceType = value;
                    this.NotifyPropertyChanged(o=>o.InvoiceType);
                }
            }
        }
        #endregion

        /// <summary>
        /// BillList
        /// </summary>
        public List<BillList> BillList { get; set; }
        /// <summary>
        /// Fees
        /// </summary>
        public List<InvoiceFeeDate> Fees { get; set; }

    }

    /// <summary>
    /// 发票费用数据对象
    /// </summary>
    [Serializable]
    public partial class InvoiceFeeDate : BaseDataObject
    {
        /// <summary>
        ///  return ID == Guid.Empty; 
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

        Guid? _BillFeeId;
        /// <summary>
        /// 对应的帐单费用ID
        /// </summary>
        public Guid? BillFeeId
        {
            get
            {
                return _BillFeeId;
            }
            set
            {
                if (_BillFeeId != value)
                {
                    _BillFeeId = value;
                    this.NotifyPropertyChanged(o => o.BillFeeId);
                }
            }
        }

        #region ChargingCode
        Guid _chargingcodeid;
        /// <summary>
        /// 费用项目ID
        /// </summary>
        [GuidRequired(CMessage = "费用项目",EMessage="ChargingCode")]
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
        /// 费用项目代码
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
        #endregion

        #region Currency

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
        #endregion


        #region Rate

        decimal _rate;
        /// <summary>
        /// 费用币种对应账单币种的汇率
        /// </summary>
        [Required(CMessage = "费用币种对应账单币种的汇率",EMessage="Rate")]
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

        #region Quantity

        decimal _quantity;
        /// <summary>
        /// 数量
        /// </summary>
        [DecimalRequired(CMessage = "数量",EMessage="Quantity")]
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
        //[MaxStringLengthAttribute(20,"金额最大为21位")] ,针对string
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

        #region Create Info

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

        DateTime? _createdate;
        /// <summary>
        /// 建立时间
        /// </summary>
        [Required(CMessage = "建立时间",EMessage="CreateDate")]
        public DateTime? CreateDate
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
    }
    /// <summary>
    /// 客户信息
    /// </summary>
    [Serializable]
    public class CustomerInvoiceInfo : BaseDataObject
    {

        string customerName;
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                if (customerName != value)
                    customerName = value;
                this.NotifyPropertyChanged(o => o.CustomerName);
            }
        }

        string customerAddress;
        /// <summary>
        /// 客户地址
        /// </summary>
        public string CustomerAddress
        {
            get { return customerAddress; }
            set
            {
                if (customerAddress != value)
                    customerAddress = value;
                this.NotifyPropertyChanged(o => o.CustomerAddress);
            }
        }

        string customerFax;
        /// <summary>
        /// 传真
        /// </summary>
        public string CustomerFax
        {
            get { return customerFax; }
            set
            {
                if (customerFax != value)
                    customerFax = value;
                this.NotifyPropertyChanged(o => o.CustomerFax);
            }
        }

        string customerTel;
        /// <summary>
        /// 电话
        /// </summary>
        public string CustomerTel
        {
            get { return customerTel; }
            set
            {
                if (customerTel != value)
                    customerTel = value;
                this.NotifyPropertyChanged(o => o.CustomerTel);
            }
        }

        string customerType;
        /// <summary>
        /// 客户类型
        /// </summary>
        public string CustomerType
        {
            get { return customerType; }
            set
            {
                if (customerType != value)
                    customerType = value;
                this.NotifyPropertyChanged(o => o.CustomerType);
            }
        }
    }

    #region 免税收入明细
    [Serializable]
    public class InvoiceFreeReportInfo
    {
        public Guid ID { get; set; }
        public Int32 IndexNo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CustomerName { get; set; }
        public string BLNo { get; set; }
        public string BillNo { get; set; }
        public string ContractNo { get; set; }
        public string VesselName { get; set; }
        public string POL { get; set; }
        public string POD { get; set; }
        public string Delivery { get; set; }
        public string ExpressNo { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal USDAmount { get; set; }
        public decimal RMBAmount { get; set; }
        public decimal OtherAmount { get; set; }
    }

    [Serializable]
    public class InvoiceFreeReportTotal
    {
        public string TaxNo { get; set; }
        public string CompanyName { get; set; }
        public string CustomerName { get; set; }
        public String Date { get; set; }

        public string DateMonth { get; set; }

        public decimal TotalUSDAmount { get; set; }

        public decimal TotalRMBAmount { get; set; }
        public decimal TotalOtherAmount { get; set; }

        public int TotalCount { get; set; }

        
    }

    [Serializable]
    public class InvoiceFreeReportData
    {
        public InvoiceFreeReportTotal TotalInfo { get; set; }
        public List<InvoiceFreeReportInfo> DataList { get; set; }
    }
    #endregion



     /// <summary>
    ///简单发票信息
    /// </summary>
    [Serializable]
    public class ShortInvoiceInfo : BaseDataObject
    {
        string _invoiceNo;
        /// <summary>
        /// 客户类型
        /// </summary>
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set
            {
                if (_invoiceNo != value)
                    _invoiceNo = value;
                this.NotifyPropertyChanged(o => o.InvoiceNo);
            }
        }

        string _amount;
        /// <summary>
        /// 客户类型
        /// </summary>
        public string Amount
        {
            get { return _amount; }
            set
            {
                if (_amount != value)
                    _amount = value;
                this.NotifyPropertyChanged(o => o.Amount);
            }
        }
    }

}
