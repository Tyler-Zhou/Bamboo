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
    /// 水单信息
    /// </summary>
    [Serializable]
    public class BankReceiptListInfo : BaseDataObject
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

        #region 客户
        
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

        #region 审核人
        Guid? _approvalBy;
        /// <summary>
        /// 审核人ID
        /// </summary>
        [GuidRequired(CMessage = "审核人", EMessage = "approvalBy")]
        public Guid? ApprovalBy
        {
            get { return _approvalBy; }
            set
            {
                if (_approvalBy != value)
                {
                    _approvalBy = value;
                    this.NotifyPropertyChanged(o => o.ApprovalBy);
                }
            }
        }

        string _approvalName;
        /// <summary>
        /// 审核人名称
        /// </summary>
        public string ApprovalName
        {
            get { return _approvalName; }
            set
            {
                if (_approvalName != value)
                {
                    _approvalName = value;
                    this.NotifyPropertyChanged(o => o.ApprovalName);
                }
            }
        }
        #endregion

        #region 审核时间
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ApprovalDate
        {
            get;
            set;
        }
        #endregion

        #region 总金额
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal Amount { get; set; }
        #endregion

        #region 状态
        /// <summary>
        /// 状态
        /// </summary>
        public BankReceiptStatus Status
        {
            get;
            set;
        }
        #endregion

        #region 有效性
        bool isValid;
        /// <summary>
        /// 有效性Ture.有效；False.无效
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
        /// <summary>
        /// 备注
        /// </summary>
        public String Remark
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

        #region 更新人ID
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid UpdateBy
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
    }

    /// <summary>
    /// 水单信息
    /// </summary>
    [Serializable]
    public class BankReceiptInfo:BankReceiptListInfo
    {
        Guid? _customerID;
        /// <summary>
        /// 客户ID
        /// </summary>
        [GuidRequired(CMessage = "客户", EMessage = "Customer")]
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
    }
}

