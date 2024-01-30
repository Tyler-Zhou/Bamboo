using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Common;
using System.Data.Linq.Mapping;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    /// <summary>
    /// 凭证列表信息
    /// </summary>
    public class LedgerListInfo : BaseDataObject
    {
        #region ID
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID
        {
            get;
            set;
        }
        #endregion
        
        #region 状态
        /// <summary>
        /// 状态
        /// </summary>
        public LedgerMasterStatus Status
        {
            get;
            set;
        }
        #endregion

        #region 单号
        /// <summary>
        /// 单号
        /// </summary>
        public String No
        {
            get;
            set;
        }
        #endregion

        #region 参考号
        /// <summary>
        /// 参考号
        /// </summary>
        public String RefNo
        {
            get;
            set;
        }
        #endregion

        #region 创建时间

        public DateTime DATE
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
        #endregion

        #region 备注
        /// <summary>
        /// 备注
        /// </summary>
        public String Remark
        {
            get;
            set;
        }
        #endregion

        #region 借方
        /// <summary>
        /// 借方
        /// </summary>
        public Decimal CRAmt
        {
            get;
            set;
        }
        #endregion

        #region 贷方
        /// <summary>
        /// 贷方
        /// </summary>
        public Decimal DRAmt
        {
            get;
            set;
        }
        #endregion

        #region 类型
        /// <summary>
        /// 类型
        /// </summary>
        public LedgerMasterType Type
        {
            get;
            set;
        }
        #endregion

        #region 有效性
        /// <summary>
        /// 有效性Ture.有效；False.无效
        /// </summary>
        public bool IsValid
        {
            get;
            set;
        }
        #endregion

        #region 创建人ID
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid CreateBy
        {
            get;
            set;
        }
        #endregion

        #region 创建人
        /// <summary>
        /// 创建人
        /// </summary>
        public String CreateUser
        {
            get;
            set;
        }
        #endregion

        #region 审核人ID
        /// <summary>
        /// 审核人ID
        /// </summary>
        public Guid AuditID
        {
            get;
            set;
        }
        #endregion

        #region 审核人
        /// <summary>
        /// 审核人
        /// </summary>
        public String Auditor
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

        #region 审核时间
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditDate
        {
            get;
            set;
        }
        #endregion

        #region 出纳员
        /// <summary>
        /// 出纳员
        /// </summary>
        public String Cashier
        {
            get;
            set;
        }
        #endregion

        #region 出纳时间
        /// <summary>
        /// 出纳时间
        /// </summary>
        public DateTime? CashierDate
        {
            get;
            set;
        }
        #endregion

        #region 财务主管签字时间
        /// <summary>
        /// 财务主管签字时间
        /// </summary>
        public DateTime? FinanceManagerDate
        {
            get;
            set;
        }
        #endregion

        public bool IsCarryOver { get; set; }
    }

    /// <summary>
    /// 凭证主表
    /// </summary>
    [Serializable]
    public class LedgerMasters : BaseDataObject
    {
        /// <summary>
        /// 是否新建数据（尚未保存到数据库的）
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }


        #region 私有变量

        private System.Guid _ID;

        private string _No;

        private LedgerMasterStatus _Status;

        private System.Nullable<int> _ReceiptQty;

        private LedgerMasterType _Type;

        private System.Nullable<System.Guid> _RefId;

        private string _RefNo;

        private string _OperationNo;

        private System.Guid _CompanyID;

        private System.Nullable<System.Guid> _FinanceManagerID;

        private System.Nullable<System.Guid> _AccountingID;

        private System.Nullable<System.Guid> _AuditID;

        private System.Nullable<System.DateTime> _AuditDate;

        private System.Nullable<System.Guid> _CashierID;

        private System.Nullable<System.Guid> _TransactorID;

        private System.Guid _CreateBy;

        private System.DateTime _CreateDate;

        private System.Nullable<System.Guid> _UpdateBy;

        private System.Nullable<System.DateTime> _UpdateDate;

        private System.Nullable<System.DateTime> _DATE;

        private System.Nullable<bool> _IsValid;

        private System.Nullable<System.DateTime> _CashierDate;

        private System.Nullable<System.DateTime> _FinanceManagerDate;

        #endregion

        #region 公用属性

        public System.Guid ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }

        public string No
        {
            get
            {
                return this._No;
            }
            set
            {
                if ((this._No != value))
                {
                    this._No = value;
                    this.NotifyPropertyChanged(o => o.No);
                }
            }
        }

        public LedgerMasterStatus Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                if ((this._Status != value))
                {
                    this._Status = value;
                    this.NotifyPropertyChanged(o => o.Status);
                }
            }
        }

        public System.Nullable<int> ReceiptQty
        {
            get
            {
                return this._ReceiptQty;
            }
            set
            {
                if ((this._ReceiptQty != value))
                {
                    this._ReceiptQty = value;
                    this.NotifyPropertyChanged(o => o.ReceiptQty);
                }
            }
        }

        [Required(CMessage = "类型", EMessage = "Type")]
        public LedgerMasterType Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                if ((this._Type != value))
                {
                    this._Type = value;
                    this.NotifyPropertyChanged(o => o.Type);
                }
            }
        }

        public System.Nullable<System.Guid> RefId
        {
            get
            {
                return this._RefId;
            }
            set
            {
                if ((this._RefId != value))
                {
                    this._RefId = value;
                    this.NotifyPropertyChanged(o => o.RefId);
                }
            }
        }

        public string RefNo
        {
            get
            {
                return this._RefNo;
            }
            set
            {
                if ((this._RefNo != value))
                {
                    this._RefNo = value;
                    this.NotifyPropertyChanged(o => o.RefNo);
                }
            }
        }

        public string OperationNo
        {
            get
            {
                return this._OperationNo;
            }
            set
            {
                if ((this._OperationNo != value))
                {
                    this._OperationNo = value;
                    this.NotifyPropertyChanged(o => o.OperationNo);
                }
            }
        }

        [GuidRequired(CMessage = "所属公司", EMessage = "CompanyID")]
        public System.Guid CompanyID
        {
            get
            {
                return this._CompanyID;
            }
            set
            {
                if ((this._CompanyID != value))
                {
                    this._CompanyID = value;
                    this.NotifyPropertyChanged(o => o.CompanyID);
                }
            }
        }

        public string CompanyName
        {
            get;
            set;
        }


        public System.Nullable<System.Guid> FinanceManagerID
        {
            get
            {
                return this._FinanceManagerID;
            }
            set
            {
                if ((this._FinanceManagerID != value))
                {
                    this._FinanceManagerID = value;
                    this.NotifyPropertyChanged(o => o.FinanceManagerID);
                }
            }
        }

        public System.Nullable<System.Guid> AccountingID
        {
            get
            {
                return this._AccountingID;
            }
            set
            {
                if ((this._AccountingID != value))
                {
                    this._AccountingID = value;
                    this.NotifyPropertyChanged(o => o.AccountingID);
                }
            }
        }

        public System.Nullable<System.Guid> AuditID
        {
            get
            {
                return this._AuditID;
            }
            set
            {
                if ((this._AuditID != value))
                {
                    this._AuditID = value;
                    this.NotifyPropertyChanged(o => o.AuditID);
                }
            }
        }

       public System.Nullable<System.DateTime> AuditDate
        {
            get
            {
                return this._AuditDate;
            }
            set
            {
                if ((this._AuditDate != value))
                {
                    this._AuditDate = value;
                    this.NotifyPropertyChanged(o => o.AuditDate);
                }
            }
        }

       public System.Nullable<System.Guid> CashierID
        {
            get
            {
                return this._CashierID;
            }
            set
            {
                if ((this._CashierID != value))
                {
                    this._CashierID = value;
                    this.NotifyPropertyChanged(o => o.CashierID);
                }
            }
        }

       public System.Nullable<System.Guid> TransactorID
        {
            get
            {
                return this._TransactorID;
            }
            set
            {
                if ((this._TransactorID != value))
                {
                    this._TransactorID = value;
                    this.NotifyPropertyChanged(o => o.TransactorID);
                }
            }
        }

        public System.Guid CreateBy
        {
            get
            {
                return this._CreateBy;
            }
            set
            {
                if ((this._CreateBy != value))
                {
                    this._CreateBy = value;
                    this.NotifyPropertyChanged(o => o.CreateBy);
                }
            }
        }

        public System.DateTime CreateDate
        {
            get
            {
                return this._CreateDate;
            }
            set
            {
                if ((this._CreateDate != value))
                {
                    this._CreateDate = value;
                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }
        }

        public System.Nullable<System.Guid> UpdateBy
        {
            get
            {
                return this._UpdateBy;
            }
            set
            {
                if ((this._UpdateBy != value))
                {
                    this._UpdateBy = value;
                    this.NotifyPropertyChanged(o => o.UpdateBy);
                }
            }
        }
         public System.Nullable<System.DateTime> UpdateDate
        {
            get
            {
                return this._UpdateDate;
            }
            set
            {
                if ((this._UpdateDate != value))
                {
                    this._UpdateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }

         [Required(CMessage = "日期", EMessage = "Date")]
       public System.Nullable<System.DateTime> DATE
        {
            get
            {
                return this._DATE;
            }
            set
            {
                if ((this._DATE != value))
                {
                    this._DATE = value;
                    this.NotifyPropertyChanged(o => o.DATE);
                }
            }
        }

        public System.Nullable<bool> IsValid
        {
            get
            {
                return this._IsValid;
            }
            set
            {
                if ((this._IsValid != value))
                {
                    this._IsValid = value;
                    this.NotifyPropertyChanged(o => o.IsValid);
                }
            }
        }

       public System.Nullable<System.DateTime> CashierDate
        {
            get
            {
                return this._CashierDate;
            }
            set
            {
                if ((this._CashierDate != value))
                {
                    this._CashierDate = value;
                    this.NotifyPropertyChanged(o => o.CashierDate);
                }
            }
        }

        public System.Nullable<System.DateTime> FinanceManagerDate
        {
            get
            {
                return this._FinanceManagerDate;
            }
            set
            {
                if ((this._FinanceManagerDate != value))
                {
                    this._FinanceManagerDate = value;
                    this.NotifyPropertyChanged(o => o.FinanceManagerDate);
                }
            }
        }

        private bool isCarryOver;
        /// <summary>
        /// 是否为结转费用
        /// </summary>
        public bool IsCarryOver
        {
            get
            {
                return isCarryOver;
            }
            set
            {
                if (isCarryOver != value)
                {
                    this.isCarryOver = value;
                    this.NotifyPropertyChanged(o => o.IsCarryOver);
                }
            }
        }

        public string MaxNo { get; set; }
        #endregion

        #region 制单人
        String creator;
        /// <summary>
        /// 制单人
        /// </summary>
         public String Creator
        {
            get
            {
                return creator;
            }
            set
            {
                if (creator != value)
                {
                    creator = value;
                    this.NotifyPropertyChanged(o => o.Creator);
                }
            }
        }
        #endregion

        #region 审核人
        String auditor;
        /// <summary>
        /// 审核人
        /// </summary>
       public String Auditor
        {
            get
            {
                return auditor;
            }
            set
            {
                if (auditor != value)
                {
                    auditor = value;
                    this.NotifyPropertyChanged(o => o.Auditor);
                }
            }
        }
        #endregion

        #region 记账人
        String accountant;
        /// <summary>
        /// 记账人
        /// </summary>
        public String Accountant
        {
            get
            {
                return accountant;
            }
            set
            {
                if (accountant != value)
                {
                    accountant = value;
                    this.NotifyPropertyChanged(o => o.Accountant);
                }
            }
        }
        #endregion

        #region 出纳人
        String cashier;
        /// <summary>
        /// 出纳人
        /// </summary>
        public String Cashier
        {
            get
            {
                return cashier;
            }
            set
            {
                if (cashier != value)
                {
                    cashier = value;
                    this.NotifyPropertyChanged(o => o.Cashier);
                }
            }
        }
        #endregion

        #region 财务主管
        String financeManager;
        /// <summary>
        /// 财务主管
        /// </summary>
        public String FinanceManager
        {
            get
            {
                return financeManager;
            }
            set
            {
                if (financeManager != value)
                {
                    financeManager = value;
                    this.NotifyPropertyChanged(o => o.FinanceManager);
                }
            }
        }
        #endregion

        #region 经办人
        String transactor;
        /// <summary>
        /// 经办人
        /// </summary>
         public String Transactor
        {
            get
            {
                return transactor;
            }
            set
            {
                if (transactor != value)
                {
                    transactor = value;
                    this.NotifyPropertyChanged(o => o.Transactor);
                }
            }
        }
        #endregion

        #region 明细列表

        public List<Ledgers> DetailList { get; set; }

        #endregion
    }

    /// <summary>
    /// 期初余额数据
    /// </summary>
    [Serializable]
    public class BeginBalances : BaseDataObject
    {
        /// <summary>
        /// 是否新建数据（尚未保存到数据库的）
        /// </summary>
        public override bool IsNew { get { return Id == Guid.Empty; } }

        #region 
        private Guid _Id;

        public System.Guid Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this._Id = value;
                    this.NotifyPropertyChanged(o => o.Id);
                }
            }
        }

        #endregion

        #region
       private Guid? _CustomerId;
       public Guid? CustomerId
        {
            get
            {
                return this._CustomerId;
            }
            set
            {
                if ((this._CustomerId != value))
                {
                    this._CustomerId = value;
                    this.NotifyPropertyChanged(o => o.CustomerId);
                }
            }
        }
       public string CustomerCode
       {
           get;
           set;
       }
       public string CustomerName
       {
           get;
           set;
       }

        #endregion

        #region
       private Guid _GLId;
       [GuidRequired(CMessage = "会计科目", EMessage = "GLCode")]
       public System.Guid GLId
        {
            get
            {
                return this._GLId;
            }
            set
            {
                if ((this._GLId != value))
                {
                    this._GLId = value;
                    this.NotifyPropertyChanged(o => o.GLId);
                }
            }
        }
       public string GLCode
       {
           get;
           set;
       }
        public string GLName
        {
            get;set;
        }
        private GLCodeProperty glCodeProperty;
        public GLCodeProperty GLCodeProperty
        {
            get
            {
                return this.glCodeProperty;
            }
            set
            {
                if ((this.glCodeProperty != value))
                {
                    this.glCodeProperty = value;
                    this.NotifyPropertyChanged(o => o.GLCodeProperty);
                }
            }
        }
        public GLCodeType GLCodeType
        {
            get;
            set;
        }
       #endregion

        #region
        private decimal _CRAmt;
        private System.Guid _CompanyId;
        public decimal CRAmt
        {
            get
            {
                return this._CRAmt;
            }
            set
            {
                if ((this._CRAmt != value))
                {
                    this._CRAmt = value;
                    this.NotifyPropertyChanged(o => o.CRAmt);
                }
            }
        }
        #endregion

        #region
        private decimal _DRAmt;
        public decimal DRAmt
        {
            get
            {
                return this._DRAmt;
            }
            set
            {
                if ((this._DRAmt != value))
                {
                    this._DRAmt = value;
                    this.NotifyPropertyChanged(o => o.DRAmt);
                }
            }
        }
        #endregion

        #region

        private string _Remark;
        [Required(CMessage = "摘要", EMessage = "Remark")]
        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                if ((this._Remark != value))
                {
                    this._Remark = value;
                    this.NotifyPropertyChanged(o => o.Remark);
                }
            }
        }
        #endregion

        #region
        public System.Guid CompanyId
        {
            get
            {
                return this._CompanyId;
            }
            set
            {
                if ((this._CompanyId != value))
                {
                    this._CompanyId = value;
                    this.NotifyPropertyChanged(o => o.CompanyId);
                }
            }
        }
        #endregion

        #region
        private decimal _OrgAmt;
        public decimal OrgAmt
        {
            get
            {
                return this._OrgAmt;
            }
            set
            {
                if ((this._OrgAmt != value))
                {
                    this._OrgAmt = value;
                    this.NotifyPropertyChanged(o => o.OrgAmt);
                }
            }
        }
        #endregion

        #region
        private decimal _Rate;
        public decimal Rate
        {
            get
            {
                return this._Rate;
            }
            set
            {
                if ((this._Rate != value))
                {
                    this._Rate = value;
                    this.NotifyPropertyChanged(o => o.Rate);
                }
            }
        }
        #endregion

        #region
        private Guid _CreateBy;

        public System.Guid CreateBy
        {
            get
            {
                return this._CreateBy;
            }
            set
            {
                if ((this._CreateBy != value))
                {
                    this._CreateBy = value;
                    this.NotifyPropertyChanged(o => o.CreateBy);
                }
            }
        }
        #endregion

        #region

        private DateTime _CreateDate;

        public System.DateTime CreateDate
        {
            get
            {
                return this._CreateDate;
            }
            set
            {
                if ((this._CreateDate != value))
                {
                    this._CreateDate = value;
                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }
        }
        #endregion

        #region

        private Guid? _UpdateBy;
        public Guid? UpdateBy
        {
            get
            {
                return this._UpdateBy;
            }
            set
            {
                if ((this._UpdateBy != value))
                {
                    this._UpdateBy = value;
                    this.NotifyPropertyChanged(o => o.UpdateBy);
                }
            }
        }
        #endregion

        #region
        private DateTime? _UpdateDate;
        public DateTime? UpdateDate
        {
            get
            {
                return this._UpdateDate;
            }
            set
            {
                if ((this._UpdateDate != value))
                {
                    this._UpdateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }
        #endregion

        #region
        private Guid? _DeptID;
        public Guid? DeptID
        {
            get
            {
                return this._DeptID;
            }
            set
            {
                if ((this._DeptID != value))
                {
                    this._DeptID = value;
                    this.NotifyPropertyChanged(o => o.DeptID);
                }
            }
        }
        public string DeptName
        {
            get;set;
        }
        #endregion

        #region
        private Guid? _PersonalID;
        public System.Guid? PersonalID
        {
            get
            {
                return this._PersonalID;
            }
            set
            {
                if ((this._PersonalID != value))
                {
                    this._PersonalID = value;
                    this.NotifyPropertyChanged(o => o.PersonalID);
                }
            }
        }
        public String PersonalName
        {
            get;
            set;
        }
        #endregion

        #region
        private decimal balance;
        public decimal Balance
        {
            get
            {
                return balance;
            }
            set
            {
                if (balance != value)
                {
                    balance = value;
                    this.NotifyPropertyChanged(o => o.Balance);
                }
            }

        }
        #endregion

        public Int32 Year
        {
            get;
            set;
        }
        public bool IsCustomerCheck
        {
            get;
            set;
        }
        public bool IsDepartmentCheck
        {
            get;
            set;
        }
        public bool IsPersonalCheck
        {
            get;
            set;
        }
        public int RowCount
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 凭证明细表
    /// </summary>
    [Serializable]
    public class Ledgers : BaseDataObject
    {
        /// <summary>
        /// 是否新建数据（尚未保存到数据库的）
        /// </summary>
        public override bool IsNew { get { return Id == Guid.Empty; } }

        public string ErrorInfo
        {
            get;
            set;
        }
        public bool HasError
        {
            get
            {
                return !ErrorInfo.IsNullOrEmpty();
            }
        }

        public ChangeState ChangeState
        {
            get
            {
                if (IsNew) return ChangeState.New;
                else if (IsDirty) return ChangeState.Changed;
                else return ChangeState.None;
            }
        }

        public bool IsCarryOver
        {
            get;
            set;
        }

        #region 私有变量

        private Guid _Id;

        private Guid? _CustomerId;

        private Guid _GLId;

        private DateTime _Date;

        private decimal _CRAmt;

        private decimal _DRAmt;

        private string _Remark;

        private LedgerDetailType _Type;

        private Guid? _RefId;

        private string _RefNo;

        private System.Guid _CompanyId;

        private decimal _OrgAmt;

        private decimal _Rate;

        private string _OperationNo;

        private Guid _CreateBy;

        private DateTime _CreateDate;

        private Guid? _UpdateBy;

        private DateTime? _UpdateDate;

        private Guid? _MasterID;

        private Guid? _DepID;

        private Guid? _UserID;

        #endregion

        #region 公用属性

        public System.Guid Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this._Id = value;
                    this.NotifyPropertyChanged(o => o.Id);
                }
            }
        }

        public Guid? CustomerId
        {
            get
            {
                return this._CustomerId;
            }
            set
            {
                if ((this._CustomerId != value))
                {
                    this._CustomerId = value;
                    this.NotifyPropertyChanged(o => o.CustomerId);
                }
            }
        }


        [GuidRequired(CMessage = "会计科目", EMessage = "GLCode")]
        public System.Guid GLId
        {
            get
            {
                return this._GLId;
            }
            set
            {
                if ((this._GLId != value))
                {
                    this._GLId = value;
                    this.NotifyPropertyChanged(o => o.GLId);
                }
            }
        }

        [Required(CMessage = "日期", EMessage = "Date")]
        public System.DateTime Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                if ((this._Date != value))
                {
                    this._Date = value;
                    this.NotifyPropertyChanged(o => o.Date);
                }
            }
        }

        public decimal CRAmt
        {
            get
            {
                return this._CRAmt;
            }
            set
            {
                if ((this._CRAmt != value))
                {
                    this._CRAmt = value;
                    this.NotifyPropertyChanged(o => o.CRAmt);
                }
            }
        }

        public decimal DRAmt
        {
            get
            {
                return this._DRAmt;
            }
            set
            {
                if ((this._DRAmt != value))
                {
                    this._DRAmt = value;
                    this.NotifyPropertyChanged(o => o.DRAmt);
                }
            }
        }

        [Required(CMessage = "摘要", EMessage = "Remark")]
        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                if ((this._Remark != value))
                {
                    this._Remark = value;
                    this.NotifyPropertyChanged(o => o.Remark);
                }
            }
        }

        public LedgerDetailType Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                if ((this._Type != value))
                {
                    this._Type = value;
                    this.NotifyPropertyChanged(o => o.Type);
                }
            }
        }

        public Guid? RefId
        {
            get
            {
                return this._RefId;
            }
            set
            {
                if ((this._RefId != value))
                {
                    this._RefId = value;
                    this.NotifyPropertyChanged(o => o.RefId);
                }
            }
        }

        public string RefNo
        {
            get
            {
                return this._RefNo;
            }
            set
            {
                if ((this._RefNo != value))
                {
                    this._RefNo = value;
                    this.NotifyPropertyChanged(o => o.RefNo);
                }
            }
        }

        public System.Guid CompanyId
        {
            get
            {
                return this._CompanyId;
            }
            set
            {
                if ((this._CompanyId != value))
                {
                    this._CompanyId = value;
                    this.NotifyPropertyChanged(o => o.CompanyId);
                }
            }
        }

        public decimal OrgAmt
        {
            get
            {
                return this._OrgAmt;
            }
            set
            {
                if ((this._OrgAmt != value))
                {
                    this._OrgAmt = value;
                    this.NotifyPropertyChanged(o => o.OrgAmt);
                }
            }
        }

        public decimal Rate
        {
            get
            {
                return this._Rate;
            }
            set
            {
                if ((this._Rate != value))
                {
                    this._Rate = value;
                    this.NotifyPropertyChanged(o => o.Rate);
                }
            }
        }

        /// <summary>
        /// 外币
        /// </summary>
        public Guid? ForeignCurrencyID { get; set; }

        public string OperationNo
        {
            get
            {
                return this._OperationNo;
            }
            set
            {
                if ((this._OperationNo != value))
                {
                    this._OperationNo = value;
                    this.NotifyPropertyChanged(o => o.OperationNo);
                }
            }
        }

        public System.Guid CreateBy
        {
            get
            {
                return this._CreateBy;
            }
            set
            {
                if ((this._CreateBy != value))
                {
                    this._CreateBy = value;
                    this.NotifyPropertyChanged(o => o.CreateBy);
                }
            }
        }

        public System.DateTime CreateDate
        {
            get
            {
                return this._CreateDate;
            }
            set
            {
                if ((this._CreateDate != value))
                {
                    this._CreateDate = value;
                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }
        }

        public System.Nullable<System.Guid> UpdateBy
        {
            get
            {
                return this._UpdateBy;
            }
            set
            {
                if ((this._UpdateBy != value))
                {
                    this._UpdateBy = value;
                    this.NotifyPropertyChanged(o => o.UpdateBy);
                }
            }
        }

        public System.Nullable<System.DateTime> UpdateDate
        {
            get
            {
                return this._UpdateDate;
            }
            set
            {
                if ((this._UpdateDate != value))
                {
                    this._UpdateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }

        public System.Nullable<System.Guid> MasterID
        {
            get
            {
                return this._MasterID;
            }
            set
            {
                if ((this._MasterID != value))
                {
                    this._MasterID = value;
                    this.NotifyPropertyChanged(o => o.MasterID);
                }
            }
        }

        public Guid? DepID
        {
            get
            {
                return this._DepID;
            }
            set
            {
                if ((this._DepID != value))
                {
                    this._DepID = value;
                    this.NotifyPropertyChanged(o => o.DepID);
                }
            }
        }

        public System.Guid? UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                if ((this._UserID != value))
                {
                    this._UserID = value;
                    this.NotifyPropertyChanged(o => o.UserID);
                }
            }
        }
        public String UserName
        {
            get;
            set;
        }

        #endregion

        #region 扩展变量

        /// <summary>
        /// 会计科目
        /// </summary>
        private string _GLName;

        /// <summary>
        /// 客户
        /// </summary>
        private string _Customer;

        /// <summary>
        /// 部门
        /// </summary>
        private string _Dept;

        /// <summary>
        /// 个人
        /// </summary>
        private string _Personal;

        #endregion

        #region 扩展属性

        public string GLName
        {
            get
            {
                return this._GLName;
            }
            set
            {
                if ((this._GLName != value))
                {
                    this._GLName = value;
                    this.NotifyPropertyChanged(o => o.GLName);
                }
            }
        }

        private string glFullName;
        /// <summary>
        /// 全称
        /// </summary>
        public string GLFullName
        {

            get
            {
                return glFullName;
            }
            set
            {
                if (glFullName != value)
                {
                    glFullName = value;
                    this.NotifyPropertyChanged(o => o.GLFullName);
                }
            }

        }
        public string GLCode
        {
            get;
            set;
        }

        public string GLDescription
        {
            get
            {
                return "(" + GLCode + ")" + GLFullName;
            }
        }

        private GLCodeProperty glCodeProperty;
        /// <summary>
        /// 科目借贷方向
        /// </summary>
        public GLCodeProperty GLCodeProperty
        {
            get
            {
                return glCodeProperty;
            }
            set
            {
                if (glCodeProperty != value)
                {
                    glCodeProperty = value;
                    this.NotifyPropertyChanged(o => o.GLCodeProperty);
                }
            }
        }


        private decimal balance;
        public decimal Balance
        {
            get
            {
                return balance;
            }
            set
            {
                if (balance != value)
                {
                    balance = value;
                    this.NotifyPropertyChanged(o => o.Balance);
                }
            }

        }

        public string Customer
        {
            get
            {
                return this._Customer;
            }
            set
            {
                if ((this._Customer != value))
                {
                    this._Customer = value;
                    this.NotifyPropertyChanged(o => o.Customer);
                }
            }
        }

        public string CustomerCode
        {
            get;
            set;
        }

        public string Dept
        {
            get
            {
                return this._Dept;
            }
            set
            {
                if ((this._Dept != value))
                {
                    this._Dept = value;
                    this.NotifyPropertyChanged(o => o.Dept);
                }
            }
        }

        public string Personal
        {
            get
            {
                return this._Personal;
            }
            set
            {
                if ((this._Personal != value))
                {
                    this._Personal = value;
                    this.NotifyPropertyChanged(o => o.Personal);
                }
            }
        }

        #endregion

        #region 辅助核算
        /// <summary>
        /// 客户往来
        /// </summary>
        public bool IsCustomerCheck { get; set; }
        /// <summary>
        /// 部门往来
        /// </summary>
        public bool IsDepartmentCheck { get; set; }
        /// <summary>
        /// 个人往来
        /// </summary>
        public bool IsPersonalCheck { get; set; }
        #endregion
    }


    /// <summary>
    /// 打印凭证
    /// </summary>
    [Serializable]
    public class PrintLedgerMasterReports : BaseDataObject
    {
        /// <summary>
        /// ID
        /// </summary>
        public System.Guid ID
        {
            get;
            set;
        }
        /// <summary>
        /// 凭证号
        /// </summary>
        public string No
        {
            get;
            set;
        }
        /// <summary>
        /// 单据数
        /// </summary>
        public int ReceiptQty
        {
            get;
            set;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public LedgerMasterType HdType
        {
            get;
            set;
        }

        /// <summary>
        /// 公司
        /// </summary>
        public System.Guid CompanyID
        {
            get;
            set;
        }

        /// <summary>
        /// 公司缩写代码
        /// </summary>
        public String CompanyShortCode
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public System.Guid CreateBy
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateDate
        {
            get;
            set;
        }

        /// <summary>
        /// 日期
        /// </summary>
        public String DATE
        {
            get;
            set;
        }

        /// <summary>
        /// 制单人
        /// </summary>
        public String Creator
        {
            get;
            set;
        }

        /// <summary>
        /// 审核人
        /// </summary>
        public String Auditor
        {
            get;
            set;
        }
        /// <summary>
        /// 记账人
        /// </summary>
        public String Accountant
        {
            get;
            set;
        }

        /// <summary>
        /// 出纳人
        /// </summary>
        public String Cashier
        {
            get;
            set;
        }

        /// <summary>
        /// 编制/核算单位
        /// </summary>
        public String Organization
        {
            get;
            set;
        }
        /// <summary>
        /// 币种
        /// </summary>
        public String Currency
        {
            get;
            set;
        }

        /// <summary>
        /// 大写金额
        /// </summary>
        public String FiguresAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 借方合计金额
        /// </summary>
        public decimal TotalCRAmt
        {
            get;
            set;
        }

        /// <summary>
        /// 贷方合计金额
        /// </summary>
        public decimal TotalDRAmt
        {
            get;
            set;
        }

        /// <summary>
        /// 财务主管
        /// </summary>
        public String FinanceManager
        {
            get;
            set;
        }
        /// <summary>
        /// 经办人
        /// </summary>
        public String Transactor
        {
            get;
            set;
        }

        /// <summary>
        /// 总经理
        /// </summary>
        public String GeneralManager
        {
            get;
            set;
        }

        /// <summary>
        /// 一张凭证中记录所在页
        /// </summary>
        public int Page
        {
            get;
            set;
        }

        /// <summary>
        /// 一张凭证的总打印页数
        /// </summary>
        public int TotalPages
        {
            get;
            set;
        }

        public List<Ledgers> DetailList { get; set; }



        #region 私有变量

        private System.Nullable<System.Guid> _CustomerId;

        private System.Guid _GLId;

        private System.DateTime _Date;

        private decimal _CRAmt;

        private decimal _DRAmt;

        private string _Remark;

        private LedgerDetailType _Type;

        private Guid? _RefId;

        private string _RefNo;

        private System.Guid _CompanyId;

        private decimal _OrgAmt;

        private decimal _Rate;

        private string _OperationNo;

        private System.Guid _CreateBy;

        private System.DateTime _CreateDate;

        private System.Nullable<System.Guid> _UpdateBy;

        private System.Nullable<System.DateTime> _UpdateDate;

        private System.Nullable<System.Guid> _MasterID;

        private System.Nullable<System.Guid> _DepID;

        private Guid? _UserID;

        #endregion

        #region 公用属性

        public System.Nullable<System.Guid> CustomerId
        {
            get
            {
                return this._CustomerId;
            }
            set
            {
                if ((this._CustomerId != value))
                {
                    this._CustomerId = value;
                    this.NotifyPropertyChanged(o => o.CustomerId);
                }
            }
        }

        public System.Guid GLId
        {
            get
            {
                return this._GLId;
            }
            set
            {
                if ((this._GLId != value))
                {
                    this._GLId = value;
                    this.NotifyPropertyChanged(o => o.GLId);
                }
            }
        }

        public System.DateTime Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                if ((this._Date != value))
                {
                    this._Date = value;
                    this.NotifyPropertyChanged(o => o.Date);
                }
            }
        }

        public decimal CRAmt
        {
            get
            {
                return this._CRAmt;
            }
            set
            {
                if ((this._CRAmt != value))
                {
                    this._CRAmt = value;
                    this.NotifyPropertyChanged(o => o.CRAmt);
                }
            }
        }

        public decimal DRAmt
        {
            get
            {
                return this._DRAmt;
            }
            set
            {
                if ((this._DRAmt != value))
                {
                    this._DRAmt = value;
                    this.NotifyPropertyChanged(o => o.DRAmt);
                }
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                if ((this._Remark != value))
                {
                    this._Remark = value;
                    this.NotifyPropertyChanged(o => o.Remark);
                }
            }
        }

        public LedgerDetailType Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                if ((this._Type != value))
                {
                    this._Type = value;
                    this.NotifyPropertyChanged(o => o.Type);
                }
            }
        }

        public Guid? RefId
        {
            get
            {
                return this._RefId;
            }
            set
            {
                if ((this._RefId != value))
                {
                    this._RefId = value;
                    this.NotifyPropertyChanged(o => o.RefId);
                }
            }
        }

        public string RefNo
        {
            get
            {
                return this._RefNo;
            }
            set
            {
                if ((this._RefNo != value))
                {
                    this._RefNo = value;
                    this.NotifyPropertyChanged(o => o.RefNo);
                }
            }
        }

        public System.Guid CompanyId
        {
            get
            {
                return this._CompanyId;
            }
            set
            {
                if ((this._CompanyId != value))
                {
                    this._CompanyId = value;
                    this.NotifyPropertyChanged(o => o.CompanyId);
                }
            }
        }

        public decimal OrgAmt
        {
            get
            {
                return this._OrgAmt;
            }
            set
            {
                if ((this._OrgAmt != value))
                {
                    this._OrgAmt = value;
                    this.NotifyPropertyChanged(o => o.OrgAmt);
                }
            }
        }

        public decimal Rate
        {
            get
            {
                return this._Rate;
            }
            set
            {
                if ((this._Rate != value))
                {
                    this._Rate = value;
                    this.NotifyPropertyChanged(o => o.Rate);
                }
            }
        }

        public string OperationNo
        {
            get
            {
                return this._OperationNo;
            }
            set
            {
                if ((this._OperationNo != value))
                {
                    this._OperationNo = value;
                    this.NotifyPropertyChanged(o => o.OperationNo);
                }
            }
        }

        //public System.Guid CreateBy
        //{
        //    get
        //    {
        //        return this._CreateBy;
        //    }
        //    set
        //    {
        //        if ((this._CreateBy != value))
        //        {
        //            this._CreateBy = value;
        //            this.NotifyPropertyChanged(o => o.CreateBy);
        //        }
        //    }
        //}

        //public System.DateTime CreateDate
        //{
        //    get
        //    {
        //        return this._CreateDate;
        //    }
        //    set
        //    {
        //        if ((this._CreateDate != value))
        //        {
        //            this._CreateDate = value;
        //            this.NotifyPropertyChanged(o => o.CreateDate);
        //        }
        //    }
        //}

        public System.Nullable<System.Guid> UpdateBy
        {
            get
            {
                return this._UpdateBy;
            }
            set
            {
                if ((this._UpdateBy != value))
                {
                    this._UpdateBy = value;
                    this.NotifyPropertyChanged(o => o.UpdateBy);
                }
            }
        }

        public System.Nullable<System.DateTime> UpdateDate
        {
            get
            {
                return this._UpdateDate;
            }
            set
            {
                if ((this._UpdateDate != value))
                {
                    this._UpdateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }

        public System.Nullable<System.Guid> MasterID
        {
            get
            {
                return this._MasterID;
            }
            set
            {
                if ((this._MasterID != value))
                {
                    this._MasterID = value;
                    this.NotifyPropertyChanged(o => o.MasterID);
                }
            }
        }

        public System.Nullable<System.Guid> DepID
        {
            get
            {
                return this._DepID;
            }
            set
            {
                if ((this._DepID != value))
                {
                    this._DepID = value;
                    this.NotifyPropertyChanged(o => o.DepID);
                }
            }
        }

        public System.Guid? UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                if ((this._UserID != value))
                {
                    this._UserID = value;
                    this.NotifyPropertyChanged(o => o.UserID);
                }
            }
        }
        public String UserName
        {
            get;
            set;
        }

        #endregion

        #region 扩展变量

        /// <summary>
        /// 会计科目
        /// </summary>
        private string _GLName;

        /// <summary>
        /// 客户
        /// </summary>
        private string _Customer;

        /// <summary>
        /// 部门
        /// </summary>
        private string _Dept;

        /// <summary>
        /// 个人
        /// </summary>
        private string _Personal;

        #endregion

        #region 扩展属性

        public string GLName
        {
            get
            {
                return this._GLName;
            }
            set
            {
                if ((this._GLName != value))
                {
                    this._GLName = value;
                    this.NotifyPropertyChanged(o => o.GLName);
                }
            }
        }

        private string glFullName;
        /// <summary>
        /// 全称
        /// </summary>
        public string GLFullName
        {

            get
            {
                return glFullName;
            }
            set
            {
                if (glFullName != value)
                {
                    glFullName = value;
                    this.NotifyPropertyChanged(o => o.GLFullName);
                }
            }

        }

        /// <summary>
        /// 科目借贷方向
        /// </summary>
        public GLCodeProperty GLCodeProperty
        {
            get;
            set;
        }

        public string Customer
        {
            get
            {
                return this._Customer;
            }
            set
            {
                if ((this._Customer != value))
                {
                    this._Customer = value;
                    this.NotifyPropertyChanged(o => o.Customer);
                }
            }
        }

        public string Dept
        {
            get
            {
                return this._Dept;
            }
            set
            {
                if ((this._Dept != value))
                {
                    this._Dept = value;
                    this.NotifyPropertyChanged(o => o.Dept);
                }
            }
        }

        public string Personal
        {
            get
            {
                return this._Personal;
            }
            set
            {
                if ((this._Personal != value))
                {
                    this._Personal = value;
                    this.NotifyPropertyChanged(o => o.Personal);
                }
            }
        }

        #endregion
      
    }

    /// <summary>
    /// 导入凭证数据
    /// </summary>
    [Serializable]
    public class ImportLedgers : BaseDataObject
    {
        #region ErrorInfo
        public string ErrorInfo{get;set;}
        #endregion

        #region ID
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID
        {
            get;
            set;
        }
        #endregion

        #region GL
        /// <summary>
        /// 科目ID
        /// </summary>
        public Guid GLID
        {
            get;
            set;
        }
        /// <summary>
        /// 科目代码
        /// </summary>
        public string GLCode
        {
            get;
            set;
        }

        /// <summary>
        /// 科目名称
        /// </summary>
        public string GLName
        {
            get;
            set;
        }
        #endregion
     
        #region 客户
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID
        {
            get;
            set;
        }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string CustomerCode
        {
            get;
            set;
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get;
            set;
        }
        #endregion

        #region 部门
        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid DepID
        {
            get;
            set;
        }
        /// <summary>
        /// 部门代码
        /// </summary>
        public string DepCode
        {
            get;
            set;
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepName
        {
            get;
            set;
        }
        #endregion

        #region 员工 
        /// <summary>
        /// 员工ID
        /// </summary>
        public Guid UserID
        {
            get;
            set;
        }
        /// <summary>
        /// 员工代码
        /// </summary>
        public string UserCode
        {
            get;
            set;
        }
        /// <summary>
        /// 员工名称
        /// </summary>
        public string UserName
        {
            get;
            set;
        }
        #endregion

        #region 原币金额 
        /// <summary>
        /// 原币金额 
        /// </summary>
        public Decimal? OrgAmt
        {
            get;
            set;
        }
        #endregion

        #region 余额方向
        /// <summary>
        /// 余额方向
        /// </summary>
        public GLCodeProperty BalanceType
        {
            get;
            set;
        }
        #endregion

        #region 余额
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance
        {
            get;
            set;
        }
        #endregion
    }

    /// <summary>
    /// 用友与ICP数据的关联
    /// </summary>
    [Serializable]
    public class UFCode2ICP
    {
        public Guid CompanyID { get; set; }
        public Int32 DataType { get; set; }
        public string UFCode { get; set; }
        public string ICPName { get; set; }
    }

    [Serializable]
    public class UserEMailList
    {
        public Guid UserID { get; set; }

        public string CName{get;set;}
        public string EMail { get; set; }
    }

    [Serializable]
    public class DeficitOperationInfo
    {
        public Guid FilerID { get; set; }
        public string OperationNo { get; set; }
    }

    #region 销账--解锁信息
    [Serializable]
    public class UntieLockLedgerResult
    {
        public int Type { get; set; }
        public Guid ID { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
    #endregion


    #region 科目统计信息
    [Serializable]
    public class GLBlance
    {
        public string Item { get; set; }
        public string Direction { get; set; }
        public decimal Amount { get; set; }
        public decimal FCurrency { get; set; }
        public decimal Count { get; set; }
    }
    #endregion

}

