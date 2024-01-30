using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    /// <summary>
    /// 月结协议列表中的实体对象
    /// </summary>
    [Serializable]
    public class MonthlyClosingEntryList : BaseDataObject
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

        #region 有效性

        bool _isValid;
        /// <summary>
        /// 有效性
        /// </summary>
        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                if (_isValid != value)
                {
                    _isValid = value;

                    this.NotifyPropertyChanged(o => o.IsValid);
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

        #region 申请人姓名
        string _applybyname;
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplyByName
        {
            get
            {
                return _applybyname;
            }
            set
            {
                if (_applybyname != value)
                {
                    _applybyname = value;
                    this.NotifyPropertyChanged(o => o.ApplyByName);
                }
            }
        }
        #endregion

        #region 协议的有效期

        DateTime? _validDate;

        /// <summary>
        /// 协议的有效期（截止日期）
        /// </summary>
        public DateTime? ValidDate
        {
            get
            {
                return _validDate;
            }
            set
            {
                if (_validDate != value)
                {
                    _validDate = value;

                    this.NotifyPropertyChanged(o => o.ValidDate);
                }
            }
        }

        #endregion

        #region 利润

        decimal _profit;
        /// <summary>
        /// 利润
        /// </summary>
        public decimal Profit
        {
            get
            {
                return _profit;
            }
            set
            {
                if (_profit != value)
                {
                    _profit = value;

                    this.NotifyPropertyChanged(o => o._profit);
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 月结协议的详细信息
    /// </summary>
    [Serializable]
    public class MonthlyClosingAgreement : MonthlyClosingEntryList
    {
        #region 客户的ID

        Guid _customerId;

        /// <summary>
        /// 客户的ID
        /// </summary>
        [GuidRequired(CMessage = "客户 ID", EMessage = "Customer ID")]
        public Guid CustomerId
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

        #region 生成月结的客户类型
        string _customerTypes;
        /// <summary>
        /// 生成月结的客户类型
        /// </summary>
        public string CustomerTypes
        {
            get
            {
                return _customerTypes;
            }
            set
            {
                if (_customerTypes != value)
                {
                    _customerTypes = value;

                    this.NotifyPropertyChanged(o => o.CustomerTypes);
                }
            }
        }
        #endregion

        #region 是否新增
        /// <summary>
        /// 是否新增
        /// </summary>
        public override bool IsNew
        {
            get
            {
                return ID == Guid.Empty;
            }
        }
        #endregion

        #region 是否投保

        bool _IsInsured;

        /// <summary>
        /// 是否投保
        /// </summary>
        public bool IsInsured
        {
            get
            {
                return _IsInsured;
            }
            set
            {
                if (_IsInsured != value)
                {
                    _IsInsured = value;

                    this.NotifyPropertyChanged(o => o.IsInsured);
                }
            }
        }

        #endregion

        #region 风险等级

        RiskRatingLevel _RiskRating;

        /// <summary>
        /// 风险等级
        /// </summary>
        public RiskRatingLevel RiskRating
        {
            get
            {
                return _RiskRating;
            }
            set
            {
                if (_RiskRating != value)
                {
                    _RiskRating = value;

                    this.NotifyPropertyChanged(o => o.RiskRating);
                }
            }
        }

        #endregion

        #region 承保金额

        decimal _AutomaticApprovalLevel;

        /// <summary>
        /// 承保金额
        /// </summary>
        public decimal InsuredAmount
        {
            get
            {
                return _AutomaticApprovalLevel;
            }
            set
            {
                if (_AutomaticApprovalLevel != value)
                {
                    _AutomaticApprovalLevel = value;

                    this.NotifyPropertyChanged(o => o.InsuredAmount);
                }
            }
        }

        #endregion

        #region 投保时间

        DateTime? _InsuredDate;

        /// <summary>
        /// 投保时间
        /// </summary>
        public DateTime? InsuredDate
        {
            get
            {
                return _InsuredDate;
            }
            set
            {
                if (_InsuredDate != value)
                {
                    _InsuredDate = value;

                    this.NotifyPropertyChanged(o => o.InsuredDate);
                }
            }
        }

        #endregion

        #region 创建人

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

                    this.NotifyPropertyChanged(o => o.CreateBy);
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

        #region 操作口岸配置
        List<MonthlyClosingAgreement2Company> _Option2Company = null;
        /// <summary>
        /// 操作口岸配置
        /// </summary>
        public List<MonthlyClosingAgreement2Company> Option2Company
        {
            get
            {
                if (_Option2Company == null)
                    _Option2Company = new List<MonthlyClosingAgreement2Company>();
                return _Option2Company;
            }
            set
            {
                _Option2Company = value;
            }
        } 
        #endregion
    }

    /// <summary>
    /// 月结协议对应操作口岸
    /// </summary>
    [Serializable]
    public class MonthlyClosingAgreement2Company : BaseDataObject
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

        #region MonthlyClosingEntryID

        Guid _MonthlyClosingEntryID;

        /// <summary>
        /// MonthlyClosingEntryID
        /// </summary>
        public Guid MonthlyClosingEntryID
        {
            get
            {
                return _MonthlyClosingEntryID;
            }
            set
            {
                if (_MonthlyClosingEntryID != value)
                {
                    _MonthlyClosingEntryID = value;

                    this.NotifyPropertyChanged(o => o.MonthlyClosingEntryID);
                }
            }
        }

        #endregion

        #region 公司ID
        Guid _companyId;
        /// <summary>
        ///  公司ID
        /// </summary>
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
        #endregion

        #region 公司名称

        string _companyname;

        /// <summary>
        /// 公司名称
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

        #region 维护人员ID
        Guid? _userid;
        /// <summary>
        ///  维护人员ID
        /// </summary>
        public Guid? UserID
        {
            get
            {
                return _userid;
            }
            set
            {
                if (_companyId != value)
                {
                    _userid = value;

                    this.NotifyPropertyChanged(o => o.UserID);
                }
            }
        }
        #endregion

        #region 付款日类型
        CalculateTermType _calculateTermType;
        /// <summary>
        /// 付款日类型
        /// </summary>
        public CalculateTermType CalculateTermType
        {
            get
            {
                return _calculateTermType;
            }
            set
            {
                if (_calculateTermType != value)
                {
                    _calculateTermType = value;

                    this.NotifyPropertyChanged(o => o.CalculateTermType);
                }
            }
        }
        #endregion

        #region 付款日
        int _paymentDate;
        /// <summary>
        /// 付款日
        /// </summary>
        public int PaymentDate
        {
            get
            {
                return _paymentDate;
            }
            set
            {
                if (_paymentDate != value)
                {
                    _paymentDate = value;

                    this.NotifyPropertyChanged(o => o.PaymentDate);
                }
            }
        }
        #endregion

        #region 信用期限
        int _creditDate;
        /// <summary>
        /// 信用期限
        /// </summary>
        public int CreditDate
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

        #region 信用额度
        Decimal? _creditAmount;
        /// <summary>
        /// 信用额度
        /// </summary>
        public Decimal? CreditAmount
        {
            get
            {
                return _creditAmount;
            }
            set
            {
                if (_creditAmount != value)
                {
                    _creditAmount = value;
                    this.NotifyPropertyChanged(o => o.CreditAmount);
                }
            }
        }
        #endregion

        #region 单箱货物估值（单位 USD）
        Decimal? _estimatedvalue;
        /// <summary>
        /// 单箱货物估值（单位 USD）
        /// </summary>
        public Decimal? Estimatedvalue
        {
            get
            {
                return _estimatedvalue;
            }
            set
            {
                if (_estimatedvalue != value)
                {
                    _estimatedvalue = value;
                    this.NotifyPropertyChanged(o => o.Estimatedvalue);
                }
            }
        }
        #endregion

        #region 申请人/业务员

        Guid _applybyid;
        /// <summary>
        /// 申请人/业务员 ID
        /// </summary>
        public Guid ApplyByID
        {
            get
            {
                return _applybyid;
            }
            set
            {
                if (_applybyid != value)
                {
                    _applybyid = value;
                    this.NotifyPropertyChanged(o => o.ApplyByID);
                }
            }
        }

        string _applybyname;
        /// <summary>
        /// 申请人/业务员 姓名
        /// </summary>
        public string ApplyByName
        {
            get
            {
                return _applybyname;
            }
            set
            {
                if (_applybyname != value)
                {
                    _applybyname = value;
                    this.NotifyPropertyChanged(o => o.ApplyByName);
                }
            }
        }

        #endregion

        #region 申请时间

        DateTime? _applyTime;

        /// <summary>
        /// 申请时间
        /// </summary>
        [Required(CMessage = "申请时间", EMessage = "ApplyTime")]
        public DateTime? ApplyTime
        {
            get
            {
                return _applyTime;
            }
            set
            {
                if (_applyTime != value)
                {
                    _applyTime = value;

                    this.NotifyPropertyChanged(o => o.ApplyTime);
                }
            }
        }

        #endregion

        #region 协议的有效期

        DateTime? _validDate;

        /// <summary>
        /// 协议的有效期（截止日期）
        /// </summary>
        public DateTime? ValidDate
        {
            get
            {
                return _validDate;
            }
            set
            {
                if (_validDate != value)
                {
                    _validDate = value;

                    this.NotifyPropertyChanged(o => o.ValidDate);
                }
            }
        }

        #endregion

        #region 备注

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

        #endregion

        #region 有效性

        bool _isValid;
        /// <summary>
        /// 有效性
        /// </summary>
        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                if (_isValid != value)
                {
                    _isValid = value;

                    this.NotifyPropertyChanged(o => o.IsValid);
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
    /// 月结客户
    /// </summary>
    [Serializable]
    public class MonthlyClosingCustomer
    {
        /// <summary>
        /// 协议ID
        /// </summary>
        public Guid CustomerID { get; set; }
        /// <summary>
        /// 是否承运人
        /// </summary>
        public bool IsAgentOfCarrier { get; set; }
        /// <summary>
        /// 记账日类型
        /// </summary>
        public CalculateTermType CalculateTermType { get; set; }
        /// <summary>
        /// 付款日
        /// </summary>
        public int PaymentDate { get; set; }
        /// <summary>
        /// 信用期限
        /// </summary>
        public int CreditTerm { get; set; }
    }

    /// <summary>
    /// 客户账单偏好设置
    /// </summary>
    [Serializable]
    public class CustomerPreferencesInfo : BaseDataObject
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

        #region CustomerID

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

                    this.NotifyPropertyChanged(o => o.CustomerID);
                }
            }
        }

        #endregion

        #region CustomerName

        string _customername;

        /// <summary>
        /// 客户名称
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

        #endregion

        #region CompanyID

        Guid _companyid;

        /// <summary>
        /// 公司ID
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

                    this.NotifyPropertyChanged(o => o.CompanyID);
                }
            }
        }

        #endregion

        #region CompanyName

        string _companyname;

        /// <summary>
        /// 公司名称
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

        #region InvoiceTitle

        string _invoicetitle;

        /// <summary>
        /// 账单Title
        /// </summary>
        public string InvoiceTitle
        {
            get
            {
                return _invoicetitle;
            }
            set
            {
                if (_invoicetitle != value)
                {
                    _invoicetitle = value;

                    this.NotifyPropertyChanged(o => o.InvoiceTitle);
                }
            }
        }

        #endregion

        #region NotifyPaymentDay

        int _notifypaymentday;

        /// <summary>
        /// 账单Title
        /// </summary>
        public int NotifyPaymentDay
        {
            get
            {
                return _notifypaymentday;
            }
            set
            {
                if (_notifypaymentday != value)
                {
                    _notifypaymentday = value;

                    this.NotifyPropertyChanged(o => o.NotifyPaymentDay);
                }
            }
        }

        #endregion

        #region Tue

        decimal _tue;

        /// <summary>
        /// Tue
        /// </summary>
        public decimal Tue
        {
            get
            {
                return _tue;
            }
            set
            {
                if (_tue != value)
                {
                    _tue = value;

                    this.NotifyPropertyChanged(o => o.Tue);
                }
            }
        }

        #endregion  

        #region NotifyMail

        string _notifymail;

        /// <summary>
        /// 账单邮箱
        /// </summary>
        public string NotifyMail
        {
            get
            {
                return _notifymail;
            }
            set
            {
                if (_notifymail != value)
                {
                    _notifymail = value;

                    this.NotifyPropertyChanged(o => o.NotifyMail);
                }
            }
        }

        #endregion

        #region ShipTo

        string _shipto;

        /// <summary>
        /// ShipTo
        /// </summary>
        public string ShipTo
        {
            get
            {
                return _shipto;
            }
            set
            {
                if (_shipto != value)
                {
                    _shipto = value;

                    this.NotifyPropertyChanged(o => o.ShipTo);
                }
            }
        }

        #endregion

        #region NotifyContact

        string _notifycontact;

        /// <summary>
        /// 账单电话
        /// </summary>
        public string NotifyContact
        {
            get
            {
                return _notifycontact;
            }
            set
            {
                if (_notifycontact != value)
                {
                    _notifycontact = value;

                    this.NotifyPropertyChanged(o => o.NotifyContact);
                }
            }
        }

        #endregion

        #region PdfAssembled

        byte _pdfassembled;

        /// <summary>
        /// PDF组成
        /// </summary>
        public byte PdfAssembled
        {
            get
            {
                return _pdfassembled;
            }
            set
            {
                if (_pdfassembled != value)
                {
                    _pdfassembled = value;

                    this.NotifyPropertyChanged(o => o.PdfAssembled);
                }
            }
        }

        #endregion

        #region OtherAttachments

        byte _otherattachments;

        /// <summary>
        /// PDF组成
        /// </summary>
        public byte OtherAttachments
        {
            get
            {
                return _otherattachments;
            }
            set
            {
                if (_otherattachments != value)
                {
                    _otherattachments = value;

                    this.NotifyPropertyChanged(o => o.OtherAttachments);
                }
            }
        }

        #endregion

        #region IsNeedPO

        bool _isneedpo;

        /// <summary>
        /// 提供PO#
        /// </summary>
        public bool IsNeedPO
        {
            get
            {
                return _isneedpo;
            }
            set
            {
                if (_isneedpo != value)
                {
                    _isneedpo = value;

                    this.NotifyPropertyChanged(o => o.IsNeedPO);
                }
            }
        }

        #endregion

        #region CreateBy

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

                    this.NotifyPropertyChanged(o => o.CreateBy);
                }
            }
        }

        #endregion

        #region CreateByName

        string _createbyname;

        /// <summary>
        /// 创建人名称
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

        #region CreateDate

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

                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }
        }

        #endregion

        #region UpdateBy

        Guid? _updateby;

        /// <summary>
        /// 修改人
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

                    this.NotifyPropertyChanged(o => o.UpdateBy);
                }
            }
        }

        #endregion

        #region UpdateByName

        string _updatebyname;

        /// <summary>
        /// 修改人名称
        /// </summary>
        public string UpdateByName
        {
            get
            {
                return _updatebyname;
            }
            set
            {
                if (_updatebyname != value)
                {
                    _updatebyname = value;

                    this.NotifyPropertyChanged(o => o.UpdateByName);
                }
            }
        }

        #endregion

        #region UpdateDate

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

                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PaymentMail : BaseDataObject
    {
        #region Type

        string _type;

        /// <summary>
        /// 类型
        /// </summary>
        public string Type
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

        #region Name

        string _name;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    this.NotifyPropertyChanged(o => o.Name);
                }
            }
        }

        #endregion

        #region Mail

        string _mail;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                if (_mail != value)
                {
                    _mail = value;
                    this.NotifyPropertyChanged(o => o.Mail);
                }
            }
        }

        #endregion
    }
}
