using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.ServiceInterface.DataObjects
{


    #region 日期记帐列表
    /// <summary>
    /// 日记帐列表
    /// </summary>
    [Serializable]
    public class JournalList : BaseDataObject
    {
        /// <summary>
        /// 是否新建数据（尚未保存到数据库的）
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }
        /// <summary>
        /// 初始化日志信息
        /// </summary>
        public JournalList()
        {

        }
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

        #region 单号
        string no;
        /// <summary>
        /// 单号
        /// </summary>
        public string No
        {
            get
            {
                return no;
            }
            set
            {
                if (no != value)
                {
                    no = value;
                    this.NotifyPropertyChanged(o => o.no);
                }
            }
        }
        #endregion

        #region 公司名称

        string companyName;
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                if (companyName != value)
                {
                    companyName = value;
                    this.NotifyPropertyChanged(o => o.CompanyName);
                }
            }

        }

        #endregion

        #region 应收
        string drAmount;
        /// <summary>
        /// 应收 (币种+金额)
        /// </summary>
        public string DRAmount
        {
            get
            {
                return drAmount;
            }
            set
            {
                if (drAmount != value)
                {
                    drAmount = value;
                    this.NotifyPropertyChanged(o => o.DRAmount);
                }
            }
        }

        #endregion

        #region 应付
        string crAmount;
        /// <summary>
        /// 应付(币种+金额)
        /// </summary>
        public string CRAmount
        {
            get
            {
                return crAmount;
            }
            set
            {
                if (crAmount != value)
                {
                    crAmount = value;
                    this.NotifyPropertyChanged(o => o.CRAmount);
                }
            }
        }

        #endregion

        #region 日期
        DateTime postDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
        /// <summary>
        /// 日期
        /// </summary>
        [Required(CMessage = "日期",EMessage="PostDate")]
        public DateTime PostDate
        {
            get
            {
                return postDate;
            }
            set
            {
                if (postDate != value)
                {
                    postDate = value;
                    this.NotifyPropertyChanged(o => o.PostDate);
                }
            }
        }

        #endregion

        #region 创建人
        string createName;
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateName
        {
            get
            {
                return createName;
            }
            set
            {
                if (createName != value)
                {
                    createName = value;
                    this.NotifyPropertyChanged(o => o.CreateName);
                }
            }
        }

        #endregion

        #region 创建日期
        DateTime createDate;
        /// <summary>
        ///  创建日期
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

        #region 最后更新日期
        DateTime? updateDate;
        /// <summary>
        /// 最后更新日期
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

        #region 是否有效
        bool isValid;
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
                    this.NotifyPropertyChanged(o => o.isValid);
                }
            }
        }
        #endregion

        #region 备注
        string remark;
        /// <summary>
        /// 备注
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

    #region 日记帐详细信息

    /// <summary>
    /// 日记帐详细信息
    /// </summary>
    [Serializable]
    public class JournalInfo : JournalList
    {
        #region 公司ID

        Guid companyID;
        /// <summary>
        /// 公司ID
        /// </summary>
        [GuidRequired(CMessage = "公司",EMessage="Company")]
        public Guid CompanyID
        {
            get
            {
                return companyID;
            }
            set
            {
                if (companyID != value)
                {
                    companyID = value;
                    this.NotifyPropertyChanged(o => o.CompanyID);
                }
            }

        }

        #endregion

        #region 创建人ID
        Guid createId;
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateId
        {
            get
            {
                return createId;
            }
            set
            {
                if (createId != value)
                {
                    createId = value;
                    this.NotifyPropertyChanged(o => o.CreateId);
                }
            }
        }
        #endregion

        /// <summary>
        /// 明细列表
        /// </summary>
        public List<JournalDetail> DetailList
        {
            get;
            set;
        }

    }
    #endregion

    #region 日记帐明细
    /// <summary>
    /// 日记帐明细
    /// </summary>
    [Serializable]
    public class JournalDetail : BaseDataObject
    {
        #region ID
        Guid? id;
        /// <summary>
        /// ID
        /// </summary>
        public Guid? ID
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

        #region JournalID
        Guid journalID;
        /// <summary>
        /// 日记帐ID
        /// </summary>
        public Guid JournalID
        {
            get
            {
                return journalID;
            }
            set
            {
                if (journalID != value)
                {
                    journalID = value;
                    this.NotifyPropertyChanged(o => o.JournalID);
                }
            }

        }

        #endregion

        #region 会计科目ID
        Guid glID;
        /// <summary>
        /// 会计科目ID
        /// </summary>
        [GuidRequired(CMessage = "会计科目",EMessage="GL")]
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

        #region 会计科目
        string glDescription;
        /// <summary>
        /// 会计科目名
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
        #endregion

        #region 币种
        Guid currencyID;
        /// <summary>
        /// 币种ID
        /// </summary>
        [GuidRequired(CMessage = "币种",EMessage="Currency")]
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
        #endregion

        #region 币种
        string currencyName;
        /// <summary>
        /// 币种名
        /// </summary>
        public string CurrencyName
        {
            get
            {
                return currencyName;
            }
            set
            {
                if (currencyName != value)
                {
                    currencyName = value;
                    this.NotifyPropertyChanged(o => o.CurrencyName);
                }
            }
        }
        #endregion

        #region 应收金额
        decimal drAmount;
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal DRAmount
        {
            get
            {
                return drAmount;
            }
            set
            {
                if (drAmount != value)
                {
                    drAmount = value;
                    this.NotifyPropertyChanged(o => o.DRAmount);
                }
            }
        }
        #endregion

        #region 应付金额
        decimal crAmount;
        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal CRAmount
        {
            get
            {
                return crAmount;
            }
            set
            {
                if (crAmount != value)
                {
                    crAmount = value;
                    this.NotifyPropertyChanged(o => o.CRAmount);
                }
            }
        }
        #endregion

        #region 客户ID

        Guid? customerID;
        /// <summary>
        /// 客户ID
        /// </summary>
        [GuidRequired(CMessage = "客户", EMessage = "Customer")]
        public Guid? CustomerID
        {
            get
            {
                return customerID;
            }
            set
            {
                if (customerID != value)
                {
                    customerID = value;
                    this.NotifyPropertyChanged(o => o.CustomerID);
                }
            }

        }

        #endregion

        #region 客户名称

        string customerName;
        /// <summary>
        /// 客户名称
        /// </summary>
        [GuidRequired(CMessage = "客户名称", EMessage = "CustomerName")]
        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                if (customerName != value)
                {
                    customerName = value;
                    this.NotifyPropertyChanged(o => o.CustomerName);
                }
            }

        }

        #endregion

        #region 备注
        string reamrk;
        /// <summary>
        /// 备注
        /// </summary>
        [Required(CMessage = "备注",EMessage="Remark")]
        public string Remark
        {
            get
            {
                return reamrk;
            }
            set
            {
                if (reamrk != value)
                {
                    reamrk = value;
                    this.NotifyPropertyChanged(o => o.Remark);
                }
            }
        }

        #endregion

        #region 更新时间
        DateTime? updateDates;
        /// <summary>
        /// 备注
        /// </summary>
        public DateTime? UpdateDates
        {
            get
            {
                return updateDates;
            }
            set
            {
                if (updateDates != value)
                {
                    updateDates = value;
                    this.NotifyPropertyChanged(o => o.Remark);
                }
            }
        }

        #endregion

    }

    #endregion

    #region 管理费用年月初预算值
    [Serializable]
    public class FeeYearMonthBudgetList : BaseDataObject
    {
        #region ID
        private Guid id;
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
                    this.NotifyPropertyChanged(o => o.id);
                }
            }
        }
        #endregion

        #region ParentID
        public Guid ParentID { get; set; }
        #endregion

        public Guid GLID { get; set; }
        public string GLName { get; set; }

        #region Remark
        private string remark;
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

        #region Amount
        private decimal amount;
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

        public DateTime? CreateDate { get; set; }

        public string CreateName { get; set; }

        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// 子节点个数
        /// </summary>
        public int ChildCount { get; set; }

        public string GLCode { get; set; }
    }
    #endregion

}
