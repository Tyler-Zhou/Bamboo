using System;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.Common.ServiceInterface.DataObjects
{
    #region CustomerCombineList
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CustomerCombineList : CustomerList
    {
        bool isMain;
        /// <summary>
        /// 是否主客户
        /// </summary>
        public bool IsMain
        {
            get
            {
                return isMain;
            }
            set
            {
                if (isMain != value)
                {
                    isMain = value;
                    base.OnPropertyChanged("IsMain", value);
                }
            }
        }

        bool isAddTemp;
        /// <summary>
        /// 
        /// </summary>
        public bool IsAddTemp
        {
            get
            {
                return isAddTemp;
            }
            set
            {
                if (isAddTemp != value)
                {
                    isAddTemp = value;
                    base.OnPropertyChanged("IsAddTemp", value);
                }
            }
        }
    }
    #endregion

    #region CustomerList
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class CustomerList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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
                    base.OnPropertyChanged("ID", value);
                }
            }
        }

        CustomerType _type;
        /// <summary>
        /// 类型（0船东（Carrier）、1航空公司(Airline)、2货代（Forwarding）、3直客（DirectClient）、4拖车行（Trucker）、5报关行（Broker）、6仓储（Warehouse）、7铁路（Railway）、8快递（Express））
        /// </summary>
        [Required(CMessage = "类型", EMessage = "Type")]
        public CustomerType Type
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
                    base.OnPropertyChanged("Type", value);
                }
            }
        }

        bool _isagentofcarrier;
        /// <summary>
        /// 承运人（客户类型必须是货代才可以设置这个值）
        /// </summary>
        public bool IsAgentOfCarrier
        {
            get
            {
                return _isagentofcarrier;
            }
            set
            {
                if (_isagentofcarrier != value)
                {
                    _isagentofcarrier = value;
                    base.OnPropertyChanged("IsAgentOfCarrier", value);
                }
            }
        }

        string _code;
        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "代码", EMessage = "Code")]
        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                if (_code != value)
                {
                    _code = value;
                    base.OnPropertyChanged("Code", value);
                }
            }
        }

        string _keyword;
        /// <summary>
        /// 关键字
        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "关键字", EMessage = "KeyWord")]
        [Required(CMessage = "关键字", EMessage = "KeyWord")]
        public string KeyWord
        {
            get
            {
                return _keyword;
            }
            set
            {
                if (_keyword != value)
                {
                    _keyword = value;
                    base.OnPropertyChanged("KeyWord", value);
                }
            }
        }


        string _cshortname;
        /// <summary>
        /// 中文简称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "中文简称", EMessage = "CShortName")]
        [Required(CMessage = "中文简称", EMessage = "CShortName")]

        public string CShortName
        {
            get
            {
                return _cshortname;
            }
            set
            {
                if (_cshortname != value)
                {
                    _cshortname = value;
                    base.OnPropertyChanged("CShortName", value);
                }
            }
        }


        string _eshortname;
        /// <summary>
        /// 英文简称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "英文简称", EMessage = "EShortName")]
        [Required(CMessage = "英文简称", EMessage = "EShortName")]
        public string EShortName
        {
            get
            {
                return _eshortname;
            }
            set
            {
                if (_eshortname != value)
                {
                    _eshortname = value;
                    base.OnPropertyChanged("EShortName", value);
                }
            }
        }


        string _cname;
        /// <summary>
        /// 中文名
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "中文名", EMessage = "CName")]
        [Required(CMessage = "中文名", EMessage = "CName")]
        public string CName
        {
            get
            {
                return _cname;
            }
            set
            {
                if (_cname != value)
                {
                    _cname = value;
                    base.OnPropertyChanged("CName", value);
                }
            }
        }


        string _ename;
        /// <summary>
        /// 英文名
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "英文名", EMessage = "EName")]
        [Required(CMessage = "英文名", EMessage = "EName")]
        public string EName
        {
            get
            {
                return _ename;
            }
            set
            {
                if (_ename != value)
                {
                    _ename = value;
                    base.OnPropertyChanged("EName", value);
                }
            }
        }


        string _caddress;
        /// <summary>
        /// 中文地址
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "中文地址", EMessage = "CAddress")]
        [Required(CMessage = "中文地址", EMessage = "CAddress")]

        public string CAddress
        {
            get
            {
                return _caddress;
            }
            set
            {
                if (_caddress != value)
                {
                    _caddress = value;
                    base.OnPropertyChanged("CAddress", value);
                }
            }
        }


        string _eaddress;
        /// <summary>
        /// 英文地址
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "英文地址", EMessage = "EAddress")]
        [Required(CMessage = "英文地址", EMessage = "EAddress")]
        public string EAddress
        {
            get
            {
                return _eaddress;
            }
            set
            {
                if (_eaddress != value)
                {
                    _eaddress = value;
                    base.OnPropertyChanged("EAddress", value);
                }
            }
        }

        string _EnterpriseCodeType;
        /// <summary>
        /// 企业代码类型
        /// </summary>
        public string EnterpriseCodeType
        {
            get
            {
                return _EnterpriseCodeType;
            }
            set
            {
                if (_EnterpriseCodeType != value)
                {
                    _EnterpriseCodeType = value;
                    base.OnPropertyChanged("EnterpriseCodeType", value);
                }
            }
        }

        string _EnterpriseCode;
        /// <summary>
        /// 企业代码
        /// </summary>
        public string EnterpriseCode
        {
            get
            {
                return _EnterpriseCode;
            }
            set
            {
                if (_EnterpriseCode != value)
                {
                    _EnterpriseCode = value;
                    base.OnPropertyChanged("EnterpriseCode", value);
                }
            }
        }

        bool _iscompany;
        /// <summary>
        /// 公司货
        /// </summary>
        public bool IsCompany
        {
            get
            {
                return _iscompany;
            }
            set
            {
                if (_iscompany != value)
                {
                    _iscompany = value;
                    base.OnPropertyChanged("IsCompany", value);
                }
            }
        }

        string _countryname;
        /// <summary>
        /// 国家
        /// </summary>
        public string CountryName
        {
            get
            {
                return _countryname;
            }
            set
            {
                if (_countryname != value)
                {
                    _countryname = value;
                    base.OnPropertyChanged("CountryName", value);
                }
            }
        }
        string _countryEName;
        /// <summary>
        /// 国家英文名
        /// </summary>
        public string CountryEName
        {
            get
            {
                return _countryEName;
            }
            set
            {
                if (_countryEName != value)
                {
                    _countryEName = value;
                    base.OnPropertyChanged("CountryEName", value);
                }
            }
        }

        string _cityname;
        /// <summary>
        /// 城市
        /// </summary>
        public string CityName
        {
            get
            {
                return _cityname;
            }
            set
            {
                if (_cityname != value)
                {
                    _cityname = value;
                    base.OnPropertyChanged("CityName", value);
                }
            }
        }

        string _tel1;
        /// <summary>
        /// 电话1
        /// </summary>
        [StringLength(MaximumLength = 30, CMessage = "电话号码", EMessage = "Tel")]
        [RegularExpression(Pattern = StringExtensionMethods.TELRegularExpression, CMessage = "电话格式不正确。例:086-0755-111111-11", EMessage = "Tel format is not correct。Example:086-0755-111111-11", IsUseErrorTemplate = false)]
        public string Tel1
        {
            get
            {
                return _tel1;
            }
            set
            {
                if (_tel1 != value)
                {
                    _tel1 = value;
                    base.OnPropertyChanged("Tel1", value);
                }
            }
        }


        string _tel2;
        /// <summary>
        /// 电话2
        /// </summary>
        [StringLength(MaximumLength = 30, CMessage = "电话号码", EMessage = "Tel")]
        [RegularExpression(Pattern = StringExtensionMethods.TELRegularExpression, CMessage = "电话格式不正确。例:086-0755-111111-11", EMessage = "Tel format is not correct。Example:086-0755-111111-11", IsUseErrorTemplate = false)]
        public string Tel2
        {
            get
            {
                return _tel2;
            }
            set
            {
                if (_tel2 != value)
                {
                    _tel2 = value;
                    base.OnPropertyChanged("Tel2", value);
                }
            }
        }

        string _fax;
        /// <summary>
        /// 传真
        /// </summary>
        [StringLength(MaximumLength = 30, CMessage = "传真", EMessage = "Fax")]
        [RegularExpression(Pattern = StringExtensionMethods.TELRegularExpression, CMessage = "电话格式不正确。例:086-0755-111111-11", EMessage = "Tel format is not correct。Example:086-0755-111111-11", IsUseErrorTemplate = false)]
        public string Fax
        {
            get
            {
                return _fax;
            }
            set
            {
                if (_fax != value)
                {
                    _fax = value;
                    base.OnPropertyChanged("Fax", value);
                }
            }
        }

        string _email;
        /// <summary>
        /// 邮件地址
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "邮箱", EMessage = "Email")]
        public string EMail
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    base.OnPropertyChanged("EMail", value);
                }
            }
        }

        CustomerStateType _state = CustomerStateType.Valid;
        /// <summary>
        /// 状态
        /// </summary>
        public CustomerStateType State
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
                    base.OnPropertyChanged("State", value);
                }
            }
        }

        bool _isDangerous = false;
        /// <summary>
        /// 是否危险客户
        /// </summary>
        public bool IsDangerous
        {
            get
            {
                return _isDangerous;
            }
            set
            {
                if (_isDangerous != value)
                {
                    _isDangerous = value;
                    base.OnPropertyChanged("IsDangerous", value);
                }
            }
        }

        string _countryprovincename;
        /// <summary>
        /// 国家/省
        /// </summary>
        public string CountryProvinceName
        {
            get
            {
                return _countryprovincename;
            }
            set
            {
                if (_countryprovincename != value)
                {
                    _countryprovincename = value;
                    base.OnPropertyChanged("CountryProvinceName", value);
                }
            }
        }

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
                    base.OnPropertyChanged("Remark", value);
                }
            }
        }

        string _createbyname;
        /// <summary>
        /// 创建人
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
                    base.OnPropertyChanged("CreateByName", value);
                }
            }
        }

        /// <summary>
        /// 修改人
        /// </summary>
        public string updatebyname { get; set; }

        DateTime _createdate;
        /// <summary>
        /// 创建时间
        /// </summary>
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
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }


        CustomerCodeApplyState? _checkedstate;
        /// <summary>
        /// 审核
        /// </summary>
        public CustomerCodeApplyState? CheckedState
        {
            get
            {
                return _checkedstate;
            }
            set
            {
                if (_checkedstate != value)
                {
                    _checkedstate = value;
                    base.OnPropertyChanged("CheckedState", value);
                }
            }
        }

        bool _isauditing;
        /// <summary>
        /// 审核
        /// </summary>
        public bool IsAuditing
        {
            get
            {
                return _isauditing;
            }
            set
            {
                if (_isauditing != value)
                {
                    _isauditing = value;
                    base.OnPropertyChanged("IsAuditing", value);
                }
            }
        }

        

        DateTime? _updateDate;
        /// <summary>
        /// 行版本
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
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        int _term;
        /// <summary>
        /// 信用期限
        /// </summary>
        [Obsolete("Credit Term")]
        public int Term
        {
            get
            {
                return _term;
            }
            set
            {
                if (_term != value)
                {
                    _term = value;
                    base.OnPropertyChanged("Term", value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            CustomerList newObj = obj as CustomerList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    } 
    #endregion

    #region CustomerInfo
    /// <summary>
    /// 客户详细信息
    /// </summary>
    [Serializable]
    public partial class CustomerInfo : CustomerList
    {
        string _cbillname;
        /// <summary>
        /// 中文账单名
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "中文账单名", EMessage = "CBillName")]
        public string CBillName
        {
            get
            {
                return _cbillname;
            }
            set
            {
                if (_cbillname != value)
                {
                    _cbillname = value;
                    base.OnPropertyChanged("CBillName", value);
                }
            }
        }

        string _ebillname;
        /// <summary>
        /// 英文账单名
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "英文账单名", EMessage = "English Bill Name")]
        public string EBillName
        {
            get
            {
                return _ebillname;
            }
            set
            {
                if (_ebillname != value)
                {
                    _ebillname = value;
                    base.OnPropertyChanged("EBillName", value);
                }
            }
        }

        Guid _countryid;
        /// <summary>
        /// 国家
        /// </summary>
        [GuidRequired(CMessage = "国家", EMessage = "Country")]

        public Guid CountryID
        {
            get
            {
                return _countryid;
            }
            set
            {
                if (_countryid != value)
                {
                    _countryid = value;
                    base.OnPropertyChanged("CountryID", value);
                }
            }
        }

       

        Guid? _provinceid;
        /// <summary>
        /// 省/州
        /// </summary>
        public Guid? ProvinceID
        {
            get
            {
                return _provinceid;
            }
            set
            {
                if (_provinceid != value)
                {
                    _provinceid = value;
                    base.OnPropertyChanged("ProvinceID", value);
                }
            }
        }


        string _provincename;
        /// <summary>
        /// 省/州

        /// </summary>
        public string ProvinceName
        {
            get
            {
                return _provincename;
            }
            set
            {
                if (_provincename != value)
                {
                    _provincename = value;
                    base.OnPropertyChanged("ProvinceName", value);
                }
            }
        }


        Guid? _cityid;
        /// <summary>
        /// 城市
        /// </summary>
        public Guid? CityID
        {
            get
            {
                return _cityid;
            }
            set
            {
                if (_cityid != value)
                {
                    _cityid = value;
                    base.OnPropertyChanged("CityID", value);
                }
            }
        }


        

        string _postcode;
        /// <summary>
        /// 邮编
        /// </summary>
        [StringLength(MaximumLength = 10, CMessage = "邮编", EMessage = "PostCode")]

        [RegularExpression(Pattern = "^[0-9]*$", CMessage = "邮编格式不正确。例:632355", EMessage = "The PostCode format is not correct。example:632355", IsUseErrorTemplate = false)]
        public string PostCode
        {
            get
            {
                return _postcode;
            }
            set
            {
                if (_postcode != value)
                {
                    _postcode = value;
                    base.OnPropertyChanged("PostCode", value);
                }
            }
        }


        string _homepage;
        /// <summary>
        /// 主页
        /// </summary>
        [RegularExpression(Pattern = "http://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%]*)?", CMessage = "主页格式不正确。例:http://www.sina.com", EMessage = "The homepage format is not correct. example:http://www.sina.com", IsUseErrorTemplate = false)]
        [StringLength(MaximumLength = 100, CMessage = "主页", EMessage = "HomePage")]
        public string Homepage
        {
            get
            {
                return _homepage;
            }
            set
            {
                if (_homepage != value)
                {
                    _homepage = value;
                    base.OnPropertyChanged("Homepage", value);
                }
            }
        }


        Guid? _tradetermid;
        /// <summary>
        /// 贸易条款
        /// </summary>
        public Guid? TradeTermID
        {
            get
            {
                return _tradetermid;
            }
            set
            {
                if (_tradetermid != value)
                {
                    _tradetermid = value;
                    base.OnPropertyChanged("TradeTermID", value);
                }
            }
        }


        string _tradetermname;
        /// <summary>
        /// 贸易条款
        /// </summary>
        public string TradeTermName
        {
            get
            {
                return _tradetermname;
            }
            set
            {
                if (_tradetermname != value)
                {
                    _tradetermname = value;
                    base.OnPropertyChanged("TradeTermName", value);
                }
            }
        }


        Guid? _paymenttypeid;
        /// <summary>
        /// 付款方式
        /// </summary>
        public Guid? PaymentTypeID
        {
            get
            {
                return _paymenttypeid;
            }
            set
            {
                if (_paymenttypeid != value)
                {
                    _paymenttypeid = value;
                    base.OnPropertyChanged("PaymentTypeID", value);
                }
            }
        }


        string _paymenttypename;
        /// <summary>
        /// 付款方式
        /// </summary>
        public string PaymentTypeName
        {
            get
            {
                return _paymenttypename;
            }
            set
            {
                if (_paymenttypename != value)
                {
                    _paymenttypename = value;
                    base.OnPropertyChanged("PaymentTypeName", value);
                }
            }
        }


        TaxType? _taxidtype;
        /// <summary>
        /// 税务登记类型
        /// </summary>
        public TaxType? TaxIdType
        {
            get
            {
                return _taxidtype;
            }
            set
            {
                if (_taxidtype != value)
                {
                    _taxidtype = value;
                    base.OnPropertyChanged("TaxIdType", value);
                }
            }
        }


        string _taxidno;
        /// <summary>
        /// /// 税务登记号
        /// </summary>
        [StringLength(MaximumLength = 40, CMessage = "税务登记号", EMessage = "TaxIdNo")]
        public string TaxIdNo
        {
            get
            {
                return _taxidno;
            }
            set
            {
                if (_taxidno != value)
                {
                    _taxidno = value;
                    base.OnPropertyChanged("TaxIdNo", value);
                }
            }
        }

        string bankAccountNo;
        /// <summary>
        /// 银行帐号
        /// </summary>
        public string BankAccountNo
        {
            get
            {
                return bankAccountNo;
            }
            set
            {
                if (bankAccountNo != value)
                {
                    bankAccountNo = value;
                    base.OnPropertyChanged("BankAccountNo", value);
                }
            }
        }

        decimal _creditlimit;
        /// <summary>
        /// 信用限额
        /// </summary>
        public decimal CreditLimit
        {
            get
            {
                return _creditlimit;
            }
            set
            {
                if (_creditlimit != value)
                {
                    _creditlimit = value;
                    base.OnPropertyChanged("CreditLimit", value);
                }
            }
        }

        bool _iscompany;
        /// <summary>
        /// 是否公司货
        /// </summary>
        public bool IsCompany
        {
            get
            {
                return _iscompany;
            }
            set
            {
                if (_iscompany != value)
                {
                    _iscompany = value;
                    base.OnPropertyChanged("IsCompany", value);
                }
            }
        }

        string _FIRMCODE;
        /// <summary>
        /// 堆场或码头的代码(bug3791,国外需求)
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "FIRMCODE", EMessage = "FIRMCODE")]
        public string FIRMCODE
        {
            get
            {
                return _FIRMCODE;
            }
            set
            {
                if (_FIRMCODE != value)
                {
                    _FIRMCODE = value;
                    base.OnPropertyChanged("FIRMCODE", value);
                }
            }
        }

        


        Guid _createbyid;
        /// <summary>
        /// 创建人

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
                    base.OnPropertyChanged("CreateByID", value);
                }
            }
        }

        
    } 
    #endregion

    #region CustomerConfirmList
    /// <summary>
    /// CustomerConfirmList
    /// </summary>
    [Serializable]
    public partial class CustomerConfirmList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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
                    base.OnPropertyChanged("ID", value);
                }
            }
        }


        Guid _customerid;
        /// <summary>
        /// 客户ID
        /// </summary>
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
                    base.OnPropertyChanged("CustomerID", value);
                }
            }
        }


        string _customername;
        /// <summary>
        /// 客户名

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
                    base.OnPropertyChanged("CustomerName", value);
                }
            }
        }


        string _applicantname;
        /// <summary>
        /// 申请人

        /// </summary>
        public string ApplicantName
        {
            get
            {
                return _applicantname;
            }
            set
            {
                if (_applicantname != value)
                {
                    _applicantname = value;
                    base.OnPropertyChanged("ApplicantName", value);
                }
            }
        }


        DateTime _applydate;
        /// <summary>
        /// 申请时间
        /// </summary>
        [Required(CMessage = "申请日期", EMessage = "ApplyDate")]

        public DateTime ApplyDate
        {
            get
            {
                return _applydate;
            }
            set
            {
                if (_applydate != value)
                {
                    _applydate = value;
                    base.OnPropertyChanged("ApplyDate", value);
                }
            }
        }


        string _confirmorname;
        /// <summary>
        /// 确认人

        /// </summary>
        public string ConfirmorName
        {
            get
            {
                return _confirmorname;
            }
            set
            {
                if (_confirmorname != value)
                {
                    _confirmorname = value;
                    base.OnPropertyChanged("ConfirmorName", value);
                }
            }
        }


        DateTime? _confirmdate;
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? ConfirmDate
        {
            get
            {
                return _confirmdate;
            }
            set
            {
                if (_confirmdate != value)
                {
                    _confirmdate = value;
                    base.OnPropertyChanged("ConfirmDate", value);
                }
            }
        }


        string _applicantremark;
        /// <summary>
        /// 申请备注
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "申请备注", EMessage = "AppliCantRemark")]

        public string ApplicantRemark
        {
            get
            {
                return _applicantremark;
            }
            set
            {
                if (_applicantremark != value)
                {
                    _applicantremark = value;
                    base.OnPropertyChanged("ApplicantRemark", value);
                }
            }
        }


        string _confirmorremark;
        /// <summary>
        /// 确认备注
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "确认备注", EMessage = "ConfirmOrRemark")]

        public string ConfirmorRemark
        {
            get
            {
                return _confirmorremark;
            }
            set
            {
                if (_confirmorremark != value)
                {
                    _confirmorremark = value;
                    base.OnPropertyChanged("ConfirmorRemark", value);
                }
            }
        }


        CustomerCodeApplyState _state;
        /// <summary>
        /// 状态
        /// </summary>
        public CustomerCodeApplyState State
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
                    base.OnPropertyChanged("State", value);
                }
            }
        }


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }


        public override bool Equals(object obj)
        {
            CustomerConfirmList newObj = obj as CustomerConfirmList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    } 
    #endregion

    #region CustomerConfirmInfo
    /// <summary>
    /// CustomerConfirmInfo
    /// </summary>
    [Serializable]
    public partial class CustomerConfirmInfo : CustomerConfirmList
    {
        Guid _applicantid;
        /// <summary>
        /// 申请人ID
        /// </summary>
        [Required(CMessage = "申请人", EMessage = "Applicant")]

        public Guid ApplicantID
        {
            get
            {
                return _applicantid;
            }
            set
            {
                if (_applicantid != value)
                {
                    _applicantid = value;
                    base.OnPropertyChanged("ApplicantID", value);
                }
            }
        }


        Guid? _confirmorid;
        /// <summary>
        /// 确认人ID
        /// </summary>
        public Guid? ConfirmorID
        {
            get
            {
                return _confirmorid;
            }
            set
            {
                if (_confirmorid != value)
                {
                    _confirmorid = value;
                    base.OnPropertyChanged("ConfirmorID", value);
                }
            }
        }


    } 
    #endregion

    #region CustomerContactList
    /// <summary>
    /// CustomerContactList
    /// </summary>
    [Serializable]
    public partial class CustomerContactList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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
                    base.OnPropertyChanged("ID", value);
                }
            }
        }


        Guid _customerid;
        /// <summary>
        /// 客户ID
        /// </summary>
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
                    base.OnPropertyChanged("CustomerID", value);
                }
            }
        }


        string _customername;
        /// <summary>
        /// 客户名

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
                    base.OnPropertyChanged("CustomerName", value);
                }
            }
        }


        string _cname;
        /// <summary>
        /// 中文名

        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "中文名", EMessage = "CName")]

        [Required(CMessage = "中文名", EMessage = "CName")]
        public string CName
        {
            get
            {
                return _cname;
            }
            set
            {
                if (_cname != value)
                {
                    _cname = value;
                    base.OnPropertyChanged("CName", value);
                }
            }
        }


        string _ename;
        /// <summary>
        /// 英文名

        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "英文名", EMessage = "EName")]

        [Required(CMessage = "英文名", EMessage = "EName")]

        public string EName
        {
            get
            {
                return _ename;
            }
            set
            {
                if (_ename != value)
                {
                    _ename = value;
                    base.OnPropertyChanged("EName", value);
                }
            }
        }


        string _department;
        /// <summary>
        /// 部门
        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "部门", EMessage = "Department")]

        public string Department
        {
            get
            {
                return _department;
            }
            set
            {
                if (_department != value)
                {
                    _department = value;
                    base.OnPropertyChanged("Department", value);
                }
            }
        }


        string _position;
        /// <summary>
        /// 职位
        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "职位", EMessage = "Position")]
        public string Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (_position != value)
                {
                    _position = value;
                    base.OnPropertyChanged("Position", value);
                }
            }
        }


        string _tel;
        /// <summary>
        /// 电话
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "电话", EMessage = "Tel")]
        [Required(CMessage = "电话", EMessage = "Tel")]
        [RegularExpression(Pattern = StringExtensionMethods.TELRegularExpression, CMessage = "电话格式不正确。例:086-0755-111111-11", EMessage = "The Tel format is not correct. example:086-0755-111111-11", IsUseErrorTemplate = false)]
        public string Tel
        {
            get
            {
                return _tel;
            }
            set
            {
                if (_tel != value)
                {
                    _tel = value;
                    base.OnPropertyChanged("Tel", value);
                }
            }
        }

        string _fax;
        /// <summary>
        /// 传真
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "传真", EMessage = "Fax")]
        [RegularExpression(Pattern = StringExtensionMethods.TELRegularExpression, CMessage = "传真格式不正确。例:086-0755-111111-11", EMessage = "The Fax format is not correct. example:086-0755-111111-11", IsUseErrorTemplate = false)]
        public string Fax
        {
            get
            {
                return _fax;
            }
            set
            {
                if (_fax != value)
                {
                    _fax = value;
                    base.OnPropertyChanged("Fax", value);
                }
            }
        }


        string _mobile;
        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "手机号", EMessage = "Mobile")]
        public string Mobile
        {
            get
            {
                return _mobile;
            }
            set
            {
                if (_mobile != value)
                {
                    _mobile = value;
                    base.OnPropertyChanged("Mobile", value);
                }
            }
        }


        string _email;
        /// <summary>
        /// 邮件地址
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "邮件", EMessage = "EMail")]
        [RegularExpression(Pattern = "^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", CMessage = "邮箱格式不正确。例:xx@hotmail.com", EMessage = "The EMail Format is not Correct .example:xx@hotmail.com", IsUseErrorTemplate = false)]
        public string EMail
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    base.OnPropertyChanged("EMail", value);
                }
            }
        }


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
                    base.OnPropertyChanged("Remark", value);
                }
            }
        }


        CustomerContactType _type;
        /// <summary>
        /// 类型（主要、普通等）

        /// </summary>
        [Required(CMessage = "类型", EMessage = "Type")]
        public CustomerContactType Type
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
                    base.OnPropertyChanged("Type", value);
                }
            }
        }


        bool _isvalid;
        /// <summary>
        /// 有效
        /// </summary>
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
                    base.OnPropertyChanged("IsValid", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 创建人

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
                    base.OnPropertyChanged("CreateByID", value);
                }
            }
        }


        string _createbyname;
        /// <summary>
        /// 创建人

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
                    base.OnPropertyChanged("CreateByName", value);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 创建时间
        /// </summary>
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
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }


        public override bool Equals(object obj)
        {
            CustomerContactList newObj = obj as CustomerContactList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    } 
    #endregion

    #region CustomerContactInfo
    /// <summary>
    /// CustomerContactInfo
    /// </summary>
    [Serializable]
    public partial class CustomerContactInfo : CustomerContactList
    {
    } 
    #endregion

    #region CustomerPartnerList
    /// <summary>
    /// CustomerPartnerList
    /// </summary>
    [Serializable]
    public partial class CustomerPartnerList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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
                    base.OnPropertyChanged("ID", value);
                }
            }
        }


        Guid _customerid;
        /// <summary>
        /// 客户ID
        /// </summary>
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
                    base.OnPropertyChanged("CustomerID", value);
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
                    base.OnPropertyChanged("CustomerName", value);
                }
            }
        }


        Guid _partnerid;
        /// <summary>
        /// 合作者ID
        /// </summary>
        public Guid PartnerID
        {
            get
            {
                return _partnerid;
            }
            set
            {
                if (_partnerid != value)
                {
                    _partnerid = value;
                    base.OnPropertyChanged("PartnerID", value);
                }
            }
        }


        string _partnername;
        /// <summary>
        /// 名称
        /// </summary>
        public string PartnerName
        {
            get
            {
                return _partnername;
            }
            set
            {
                if (_partnername != value)
                {
                    _partnername = value;
                    base.OnPropertyChanged("PartnerName", value);
                }
            }
        }


        string _partnerkeyword;
        /// <summary>
        /// 关键字

        /// </summary>
        public string PartnerKeyword
        {
            get
            {
                return _partnerkeyword;
            }
            set
            {
                if (_partnerkeyword != value)
                {
                    _partnerkeyword = value;
                    base.OnPropertyChanged("PartnerKeyword", value);
                }
            }
        }


        string _partnercode;
        /// <summary>
        /// 代码
        /// </summary>
        public string PartnerCode
        {
            get
            {
                return _partnercode;
            }
            set
            {
                if (_partnercode != value)
                {
                    _partnercode = value;
                    base.OnPropertyChanged("PartnerCode", value);
                }
            }
        }


        string _partneraddress;
        /// <summary>
        /// 地址
        /// </summary>
        public string PartnerAddress
        {
            get
            {
                return _partneraddress;
            }
            set
            {
                if (_partneraddress != value)
                {
                    _partneraddress = value;
                    base.OnPropertyChanged("PartnerAddress", value);
                }
            }
        }


        string _partnertel;
        /// <summary>
        /// 电话
        /// </summary>
        public string PartnerTel
        {
            get
            {
                return _partnertel;
            }
            set
            {
                if (_partnertel != value)
                {
                    _partnertel = value;
                    base.OnPropertyChanged("PartnerTel", value);
                }
            }
        }

        string _partnerfax;
        /// <summary>
        /// 传真
        /// </summary>
        public string PartnerFax
        {
            get
            {
                return _partnerfax;
            }
            set
            {
                if (_partnerfax != value)
                {
                    _partnerfax = value;
                    base.OnPropertyChanged("PartnerFax", value);
                }
            }
        }

        Guid _createbyid;
        /// <summary>
        /// 创建人

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
                    base.OnPropertyChanged("CreateByID", value);
                }
            }
        }


        string _createbyname;
        /// <summary>
        /// 创建人

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
                    base.OnPropertyChanged("CreateByName", value);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 创建时间
        /// </summary>
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
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }


        public override bool Equals(object obj)
        {
            CustomerPartnerList newObj = obj as CustomerPartnerList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    } 
    #endregion

    #region CustomerPartnerInfo
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class CustomerPartnerInfo : CustomerPartnerList
    {
    } 
    #endregion

    #region CustomerMemoList
    /// <summary>
    /// CustomerMemoList
    /// </summary>
    [Serializable]
    public partial class CustomerMemoList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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
                    base.OnPropertyChanged("ID", value);
                }
            }
        }


        Guid _customerid;
        /// <summary>
        /// 客户ID
        /// </summary>
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
                    base.OnPropertyChanged("CustomerID", value);
                }
            }
        }


        Guid _memoid;
        /// <summary>
        /// 备注ID
        /// </summary>
        public Guid MemoID
        {
            get
            {
                return _memoid;
            }
            set
            {
                if (_memoid != value)
                {
                    _memoid = value;
                    base.OnPropertyChanged("MemoID", value);
                }
            }
        }


        string _subject;
        /// <summary>
        /// 主题
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "主题", EMessage = "Subject")]
        [Required(CMessage = "主题", EMessage = "Subject")]
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                if (_subject != value)
                {
                    _subject = value;
                    base.OnPropertyChanged("Subject", value);
                }
            }
        }


        string _content;
        /// <summary>
        /// 内容
        /// </summary>
        [Required(CMessage = "内容", EMessage = "Content")]
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                if (_content != value)
                {
                    _content = value;
                    base.OnPropertyChanged("Content", value);
                }
            }
        }





        MemoType _type;
        /// <summary>
        /// 类型
        /// </summary>
        [Required(CMessage = "类型", EMessage = "Type")]
        public MemoType Type
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
                    base.OnPropertyChanged("Type", value);
                }
            }
        }

        int _bizMemoType;
        /// <summary>
        /// 类型for UI
        /// </summary>
        public int BizMemoType
        {
            get
            {
                return _bizMemoType;
            }
            set
            {
                if (_bizMemoType != value)
                {
                    _bizMemoType = value;
                    base.OnPropertyChanged("BizMemoType", value);
                }
            }
        }

        MemoPriority _priority;
        /// <summary>
        /// 类型
        /// </summary>
        [Required(CMessage = "优先级", EMessage = "Priority")]
        public MemoPriority Priority
        {
            get
            {
                return _priority;
            }
            set
            {
                if (_priority != value)
                {
                    _priority = value;
                    base.OnPropertyChanged("Priority", value);
                }
            }
        }

        int _bizPriority;
        public int BizPriority
        {
            get
            {
                return _bizPriority;
            }
            set
            {
                if (_bizPriority != value)
                {
                    _bizPriority = value;
                    base.OnPropertyChanged("BizPriority", value);
                }
            }
        }

        Guid _createbyid;
        /// <summary>
        /// 创建人
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
                    base.OnPropertyChanged("CreateByID", value);
                }
            }
        }


        bool _IsShowUser;
        /// <summary>
        /// 显示给用户
        /// </summary>
        public bool IsShowUser
        {
            get
            {
                return _IsShowUser;
            }
            set
            {
                if (_IsShowUser != value)
                {
                    _IsShowUser = value;
                    base.OnPropertyChanged("IsShowUser", value);
                }
            }
        }

        bool _IsShowCustomer;
        /// <summary>
        /// 显示给用户
        /// </summary>
        public bool IsShowCustomer
        {
            get
            {
                return _IsShowCustomer;
            }
            set
            {
                if (_IsShowCustomer != value)
                {
                    _IsShowCustomer = value;
                    base.OnPropertyChanged("IsShowCustomer", value);
                }
            }
        }


        string _createbyname;
        /// <summary>
        /// 创建人
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
                    base.OnPropertyChanged("CreateByName", value);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 创建时间
        /// </summary>
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
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }


        public override bool Equals(object obj)
        {
            CustomerMemoList newObj = obj as CustomerMemoList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    } 
    #endregion

    #region CustomerMemoInfo
    /// <summary>
    /// CustomerMemoInfo
    /// </summary>
    [Serializable]
    public partial class CustomerMemoInfo : CustomerMemoList
    {
    } 
    #endregion

    #region CustomerInvoiceTitleInfo
    /// <summary>
    /// 客户发票抬头信息
    /// </summary>
    [Serializable]
    public partial class CustomerInvoiceTitleInfo : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

        #region ID
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
                    base.OnPropertyChanged("ID", value);
                }
            }
        }
        #endregion

        #region 客户ID
        Guid _customerid;
        /// <summary>
        /// 客户ID
        /// </summary>
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
                    base.OnPropertyChanged("CustomerID", value);
                }
            }
        }

        #endregion

        #region 公司
        public Guid CompanyID
        {
            get;
            set;
        }
        #endregion

        #region 代码
        private string code;
        /// <summary>
        /// 代码
        /// </summary>
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                if (value != code)
                {
                    code = value;
                    this.NotifyPropertyChanged(o => o.Code);
                }
            }
        }
        #endregion

        #region Name
        private string name;
        /// <summary>
        /// Name
        /// </summary>
        [Required(CMessage = "名称", EMessage = "Name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    this.NotifyPropertyChanged(o => o.Name);
                }
            }
        }
        #endregion

        #region 税号
        private string taxNo;
        /// <summary>
        /// 税号
        /// </summary>
        [Required(CMessage = "税号", EMessage = "TaxNo")]
        public string TaxNo
        {
            get
            {
                return taxNo;
            }
            set
            {
                if (value != taxNo)
                {
                    taxNo = value;
                    this.NotifyPropertyChanged(o => o.TaxNo);
                }
            }
        }
        #endregion

        #region 地址电话
        private string addressTel;
        /// <summary>
        /// 地址电话
        /// </summary>
        [Required(CMessage = "地址电话", EMessage = "AddressTel")]
        public string AddressTel
        {
            get
            {
                return addressTel;
            }
            set
            {
                if (value != addressTel)
                {
                    addressTel = value;
                    this.NotifyPropertyChanged(o => o.AddressTel);
                }
            }
        }
        #endregion

        #region   银行帐号
        private string bankAccountNo;
        /// <summary>
        /// 银行帐号
        /// </summary>
        [Required(CMessage = "银行帐号", EMessage = "BankAccountNo")]
        public string BankAccountNo
        {
            get
            {
                return bankAccountNo;
            }
            set
            {
                if (value != bankAccountNo)
                {
                    bankAccountNo = value;
                    this.NotifyPropertyChanged(o => o.BankAccountNo);
                }
            }
        }
        #endregion

        #region 类型
        private CustomerInvoiceType invoiceType;
        /// <summary>
        /// 发票类型
        /// </summary>
        public CustomerInvoiceType InvoiceType
        {
            get
            {
                return invoiceType;
            }
            set
            {
                if (value != invoiceType)
                {
                    invoiceType = value;
                    this.NotifyPropertyChanged(o => o.InvoiceType);
                }
            }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string InvoiceTypeName
        {
            get
            {
                return EnumHelper.GetDescription<CustomerInvoiceType>((CustomerInvoiceType)InvoiceType, false);
            }
        }
        #endregion

        #region 创建人
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid CreateByID
        {
            get;
            set;
        }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateByName
        {
            get;
            set;
        }
        #endregion

        #region 创建时间
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get;
            set;
        }
        #endregion

        #region 更新人
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid? UpdateBy
        {
            get;
            set;
        }
        #endregion

        #region 更新时间
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get;
            set;
        }
        #endregion

        #region 有效性
        private bool isValid;
        /// <summary>
        /// 有效性
        /// </summary>
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                if (isValid != value)
                {
                    isValid = value;
                    this.NotifyPropertyChanged(o => o.IsValid);
                }
            }
        }
        #endregion

        #region 最后使用时间
        public DateTime? LastUseDate
        {
            get;
            set;
        }
        #endregion

    } 
    #endregion

    #region ExCustomer
    /// <summary>
    /// 例外客户
    /// </summary>
    [Serializable]
    public class ExCustomer : BaseDataObject
    {

        Guid _id;
        /// <summary>
        /// id
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
                    base.OnPropertyChanged("ID", value);
                }
            }
        }

        Guid _companyid;
        /// <summary>
        /// CompanyID
        /// </summary>
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
                    base.OnPropertyChanged("CompanyID", value);
                }
            }
        }

        Guid _customerid;
        /// <summary>
        /// CompanyID
        /// </summary>
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
                    base.OnPropertyChanged("CustomerID", value);
                }
            }
        }


        Guid _createby;
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateBy
        {
            get
            {
                return _createby;
            }
            set
            {
                if (_createby != value)
                {
                    _createby = value;
                    base.OnPropertyChanged("CreateBy", value);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 创建时间
        /// </summary>
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
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }

        Guid? _updateby;
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid? UpdateBy
        {
            get
            {
                return _updateby;
            }
            set
            {
                if (_updateby != value)
                {
                    _updateby = value;
                    base.OnPropertyChanged("UpdateBy", value);
                }
            }
        }


        DateTime? _updatedate;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _updatedate;
            }
            set
            {
                if (_updatedate != value)
                {
                    _updatedate = value;
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        bool _isvalid;
        /// <summary>
        /// 创建时间
        /// </summary>
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
                    base.OnPropertyChanged("IsValid", value);
                }
            }
        }
    }
    #endregion

    #region CustomerList
    /// <summary>
    /// 
    /// </summary>
    public partial class CustomerList
    {
        /// <summary>
        /// 类型描述
        /// </summary>
        public string TypeDescription
        {
            get
            {
                if (this.Type == CustomerType.Unknown)
                {
                    return string.Empty;
                }
                else if (this.Type == CustomerType.Forwarding && this.IsAgentOfCarrier)
                {
                    return LocalData.IsEnglish ? "Agent Of Carrier" : "承运人";
                }
                else
                {
                    return EnumHelper.GetDescription<CustomerType>(this.Type, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                }
            }
        }

        /// <summary>
        /// 审核状态描述
        /// </summary>
        public string CheckedStateDescription
        {
            get
            {
                if (this.CheckedState == null
                    || this.CheckedState == CustomerCodeApplyState.All)
                {
                    return string.Empty;
                }
                else
                {
                    return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<CustomerCodeApplyState>(this.CheckedState.Value, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BizIsDangerous
        {
            get
            {
                if (this.IsDangerous)
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
        }

        /// <summary>
        /// 是否保留
        /// </summary>
        public bool IsPreserve { get; set; }
    } 
    #endregion
}
