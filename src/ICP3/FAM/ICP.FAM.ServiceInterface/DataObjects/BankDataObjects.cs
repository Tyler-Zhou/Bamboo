using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    #region 银行列表信息(BankList)
    /// <summary>
    /// 银行列表信息(BankList)
    /// </summary>
    [Serializable]
    public partial class BankList : BaseDataObject
    {
        /// <summary>
        /// 是否新建数据（尚未保存到数据库的）
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        /// <summary>
        /// 初始化银行信息
        /// </summary>
        public BankList()
        {

        }
        #region 主键ID

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

        #endregion

        #region 所属公司

        string _companyname;
        /// <summary>
        /// 所属公司名称
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

        #region 中文简称

        string _cshortname;
        /// <summary>
        /// 中文简称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "中文简称", EMessage = "CShortName")]
        [Required(CMessage = "中文简称",EMessage="CShortName")]
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
                    this.NotifyPropertyChanged(o => o.CShortName);
                }
            }
        }

        #endregion

        #region 英文简称

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
                    this.NotifyPropertyChanged(o => o.EShortName);
                }
            }
        }

        #endregion

        #region 中文名称

        string _cname;
        /// <summary>
        /// 中文名称
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "中文名称", EMessage = "CName")]
        [Required(CMessage = "中文名称", EMessage = "CName")]
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
                    this.NotifyPropertyChanged(o => o.CName);
                }
            }
        }

        #endregion

        #region 英文名称

        string _ename;
        /// <summary>
        /// 英文名称
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "英文名称", EMessage = "EName")]
        [Required(CMessage = "英文名称", EMessage = "EName")]
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
                    this.NotifyPropertyChanged(o => o.EName);
                }
            }
        }

        #endregion

        #region 是否有效

        bool _isvalid;
        /// <summary>
        /// 是否有效
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
                    this.NotifyPropertyChanged(o => o.IsValid);
                }
            }
        }

        #endregion

        #region 建立人

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

        #endregion

        #region 建立时间

        DateTime _createdate;
        /// <summary>
        /// 建立时间
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
                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }
        }

        #endregion

        #region 更新时间-做数据版本控制用

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

        #region 传真

        string _fax;
        /// <summary>
        /// 传真
        /// </summary>
        [StringLength(MaximumLength =30, CMessage = "传真", EMessage = "Fax")]
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
                    this.NotifyPropertyChanged(o => o.Fax);
                }
            }
        }

        #endregion

        #region 电话

        string _tel;
        /// <summary>
        /// 电话
        /// </summary>
        [StringLength(MaximumLength = 30, CMessage = "电话", EMessage = "Tel")]
        public string Tel1
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
                    this.NotifyPropertyChanged(o => o.Tel1);
                }
            }
        }

        #endregion

        #region 联系人

        string _contact;

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact
        {
            get
            {
                return _contact;
            }
            set
            {
                if (_contact != value)
                {
                    _contact = value;

                    this.NotifyPropertyChanged(o => o.Contact);
                }
            }
        }

        #endregion

        #region 总行数
        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalRowCount
        {
            get;
            set;
        }
        #endregion

        #region 总页数
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get;
            set;
        }

        #endregion


        #region 当前页

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage
        {
            get;
            set;
        }

        #endregion
    }

    #endregion

    #region 银行信息(BankInfo)
    /// <summary>
    /// 银行信息(BankInfo)
    /// </summary>
    [Serializable]
    public partial class BankInfo : BankList
    {
        Guid _companyId;

        /// <summary>
        /// 所属公司ID
        /// </summary>
        [Required(CMessage = "公司",EMessage="Company")]
        public Guid CompanyId
        {
            get
            {
                return _companyId;
            }
            set
            {
                if (_companyId != value)
                {
                    _companyId = value;

                    this.NotifyPropertyChanged(o => o.CompanyId);
                }
            }
        }

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

        #region 客户名称

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

        #region 客户的ID

        Guid? _customerId;

        /// <summary>
        /// 客户的ID
        /// </summary>
        public Guid? CustomerId
        {
            get
            {
                return _customerId;
            }
            set
            {
                if (_customerId != value)
                {
                    _customerId = value;

                    this.NotifyPropertyChanged(o => o.CustomerId);
                }
            }
        }

        #endregion

        #region 英文地址

        string _eaddress;
        /// <summary>
        /// 英文地址
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "英文地址", EMessage = "EAddress")]
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
                    this.NotifyPropertyChanged(o => o.EAddress);
                }
            }
        }

        #endregion

        #region 邮政编码

        string _postcode;
        /// <summary>
        /// 邮政编码
        /// </summary>
        [StringLength(MaximumLength = 10, CMessage = "邮编", EMessage = "PostCode")]
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
                    this.NotifyPropertyChanged(o => o.PostCode);
                }
            }
        }

        #endregion

        #region 中文地址

        string _caddress;
        /// <summary>
        /// 中文地址
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "中文地址", EMessage = "CAddress")]
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
                    this.NotifyPropertyChanged(o => o.CAddress);
                }
            }
        }

        #endregion

        /// <summary>
        /// BankInfo
        /// </summary>
        public BankInfo()
        {
            this.BankAccountInfoList = new List<BankAccountInfo>();
        }
        /// <summary>
        /// BankAccountInfoList
        /// </summary>
        public List<BankAccountInfo> BankAccountInfoList { get; set; }
    }

    #endregion

    #region 银行账号列表(BankAccountList)
    /// <summary>
    /// 银行账号列表(BankAccountList)
    /// </summary>
    [Serializable]
    public partial class BankAccountList : BaseDataObject
    {
        #region IsNew
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } } 
        #endregion

        #region 唯一键
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
        #endregion

        #region 银行ID
        Guid _bankid;
        /// <summary>
        /// 银行ID
        /// </summary>
        public Guid BankID
        {
            get
            {
                return _bankid;
            }
            set
            {
                if (_bankid != value)
                {
                    _bankid = value;
                    this.NotifyPropertyChanged(o => o.BankID);
                }
            }
        } 
        #endregion

        #region 银行账号
        string _accountno;
        /// <summary>
        /// 银行账号
        /// </summary>
        [Required(CMessage = "银行账号", EMessage = "AccountNo")]
        public string AccountNo
        {
            get
            {
                return _accountno;
            }
            set
            {
                if (_accountno != value)
                {
                    _accountno = value;
                    this.NotifyPropertyChanged(o => o.AccountNo);
                }
            }
        } 
        #endregion

        #region 币种id
        Guid _currencyID;
        /// <summary>
        /// 币种id
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
        #endregion

        #region 币种
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

        #region 账号币种
        /// <summary>
        /// 账号币种
        /// </summary>
        public string AccountCurrencyName
        {
            get;
            set;
        } 
        #endregion

        #region 银行币种
        /// <summary>
        /// 银行币种
        /// </summary>
        public string CurrencyEName
        {
            get;
            set;
        } 
        #endregion

        #region 银行名称
        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; } 
        #endregion

        #region 会计科目中文名称
        string _glcname;
        /// <summary>
        /// 会计科目中文名称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "会计科目中文名", EMessage = "GLCName")]
        public string GLCName
        {
            get
            {
                return _glcname;
            }
            set
            {
                if (_glcname != value)
                {
                    _glcname = value;
                    this.NotifyPropertyChanged(o => o.GLCName);
                }
            }
        } 
        #endregion

        #region 会计科目完整名称
        /// <summary>
        /// 会计科目完整名称
        /// </summary>
        public string GLFullName
        {
            get;
            set;
        } 
        #endregion

        #region 会计科目英文名称
        string _glename;
        /// <summary>
        /// 会计科目英文名称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "会计科目英文名", EMessage = "GLEName")]
        public string GLEName
        {
            get
            {
                return _glename;
            }
            set
            {
                if (_glename != value)
                {
                    _glename = value;
                    this.NotifyPropertyChanged(o => o.GLEName);
                }
            }
        } 
        #endregion

        #region 是否有效
        bool _isvalid;
        /// <summary>
        /// 是否有效
        /// </summary>
        [Required(CMessage = "是否有效", EMessage = "IsValid")]
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
        #endregion

        #region 默认
        bool _isdefualt;
        /// <summary>
        /// 默认
        /// </summary>
        public bool IsDefualt
        {
            get
            {
                return _isdefualt;
            }
            set
            {
                if (_isdefualt != value)
                {
                    _isdefualt = value;
                    this.NotifyPropertyChanged(o => o.IsDefualt);
                }
            }
        } 
        #endregion

        #region 建立人
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
        #endregion

        #region 建立时间
        DateTime _createdate;
        /// <summary>
        /// 建立时间
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
                    this.NotifyPropertyChanged(o => o.CreateDate);
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

        #region 是否开票账号
        /// <summary>
        /// 是否开票账号
        /// </summary>
        public bool IsInvoiceAccount
        {
            get;
            set;
        } 
        #endregion

        #region 会计科目名称
        /// <summary>
        /// 会计科目名称
        /// </summary>
        public string GLName { get; set; } 
        #endregion

        #region 操作口岸ID
        /// <summary>
        /// 操作口岸ID
        /// </summary>
        public Guid CompanyID { get; set; } 
        #endregion

        #region 是否对公银行
        bool _isopen;
        /// <summary>
        /// 是否对公银行
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return _isopen;
            }
            set
            {
                if (_isopen != value)
                {
                    _isopen = value;
                    this.NotifyPropertyChanged(o => o.IsOpen);
                }
            }
        }
        #endregion

        #region 是否支持银企直连
        bool _issupportdirectbank;
        /// <summary>
        /// 是否支持银企直连
        /// </summary>
        public bool IsSupportDirectBank
        {
            get
            {
                return _issupportdirectbank;
            }
            set
            {
                if (_issupportdirectbank != value)
                {
                    _issupportdirectbank = value;
                    this.NotifyPropertyChanged(o => o.IsSupportDirectBank);
                }
            }
        } 
        #endregion

        #region 更新时间-做数据版本控制用

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
    /// 银行账号自定义比较类
    /// </summary>
    public class BankAccountBankIDComparer : IEqualityComparer<BankAccountList>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool Equals(BankAccountList a, BankAccountList b)
        {
            if (Object.ReferenceEquals(a, null) || Object.ReferenceEquals(b, null))
                return false;
            return a.BankID == b.BankID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public int GetHashCode(BankAccountList a)
        {
            if (Object.ReferenceEquals(a, null)) return 0;
            int hashCode = a.BankID.GetHashCode();
            return hashCode;
        }
    }
    #endregion

    #region 账号详细信息(BankAccountInfo)
    /// <summary>
    /// 账号详细信息(BankAccountInfo)
    /// </summary>
    [Serializable]
    public partial class BankAccountInfo : BankAccountList
    {
        Guid? _glid;
        /// <summary>
        /// 会计科目ID
        /// </summary>
        [Required(CMessage = "会计科目",EMessage="GL")]
        public Guid? GLID
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


        Guid _currencyid;
        /// <summary>
        /// 币种
        /// </summary> 
        [Required(CMessage = "币种",EMessage="Currency")]
        public new Guid CurrencyID
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


        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        [GuidRequired(CMessage = "建立人",EMessage="CreateBy")]
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

        bool _isShowInInvoiceBankList;
        /// <summary>
        /// 是否显示在发票的银行列表
        /// </summary>
        public bool IsShowInInvoiceBankList
        {
            get
            {
                return _isShowInInvoiceBankList;
            }
            set
            {
                if (_isShowInInvoiceBankList != value)
                {
                    _isShowInInvoiceBankList = value;
                    this.NotifyPropertyChanged(o => o.IsShowInInvoiceBankList);
                }
            }
        }

       
    }

    #endregion

    #region 银行流水(BankTransactionInfo)
    /// <summary>
    /// 银行流水(BankTransactionInfo)
    /// </summary>
    [Serializable]
    public class BankTransactionInfo
    {
        /// <summary>
        /// 操作口岸ID
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// 银行流水ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string BusinessNO { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string FlowWaterNO { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string AccountNO { get; set; }
        /// <summary>
        /// 交易对方银行账号
        /// </summary>
        public string RelativeAccountNo { get; set; }
        /// <summary>
        /// 交易对方账户名称
        /// </summary>
        public string RelativeAccountName { get; set; }
        /// <summary>
        /// 交易对方账户支行
        /// </summary>
        public string RelativeBankName { get; set; }
        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyName { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal TransactionAmount { get; set; }
        /// <summary>
        /// 借方金额
        /// </summary>
        public decimal DebitAmount 
        { 
            get 
            { 
                if(DebitCreditFlag=="D")
                {
                    return -TransactionAmount;
                }
                return 0.00m;
            } 
        }
        /// <summary>
        /// 贷方金额
        /// </summary>
        public decimal CreditAmount
        {
            get
            {
                if (DebitCreditFlag == "C")
                {
                    return TransactionAmount;
                }
                return 0.00m;
            }
        }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime OperationDateTime { get; set; }
        /// <summary>
        /// 借贷标志
        /// </summary>
        public string DebitCreditFlag { get; set; }
        /// <summary>
        /// 交易摘要
        /// </summary>
        public string Remark { get; set; }
    }
    #endregion

    #region 关联银行流水到销账数据(BankTransaction2Checks)
    /// <summary>
    /// 关联银行流水到销账数据(BankTransaction2Checks)
    /// </summary>
    [Serializable]
    public class BankTransaction2Checks : BaseDataObject
    {
        /// <summary>
        /// 银行流水ID
        /// </summary>
        public Guid BankTransactionID { get; set; }
        /// <summary>
        /// 销账ID
        /// </summary>
        public Guid ChecksID { get; set; }
        /// <summary>
        /// 销账汇总ID
        /// </summary>
        public Guid ChecksAmountID { get; set; }
        /// <summary>
        /// 销账单号
        /// </summary>
        public string ChecksNO { get; set; }
        /// <summary>
        /// 销账方向
        /// </summary>
        public FeeWay ChecksWay { get; set; }
        /// <summary>
        /// 销账金额
        /// </summary>
        public decimal ChecksAmount { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string BankAccountName { get; set; }
        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyName { get; set; }
        /// <summary>
        /// 销账人
        /// </summary>
        public string Checkser { get; set; }
        /// <summary>
        /// 销账日期
        /// </summary>
        public DateTime ChecksDate { get; set; }
    }
    #endregion
}
