using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    #region 对账单
    /// <summary>
    /// 代理对帐单
    /// </summary>
    [Serializable]
    public class AgnetBillCheckList : BaseDataObject
    {
        #region ID
        private Guid id;
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
        /// 对账单号
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
                    this.NotifyPropertyChanged(o => o.No);
                }
            }
        }
        #endregion

        #region 对账信息
        string billCheckInfo;
        /// <summary>
        /// 对账信息
        /// </summary>
        public string BillCheckInfo
        {
            get
            {
                billCheckInfo = "发起代理: " + LaunchCompanyName + "(" + LaunchUserName + ")  核对代理: " + CheckCompanyName + "(" + CheckUserName + ")";
                return billCheckInfo;
            }
            set
            {
                if (billCheckInfo != value)
                {
                    billCheckInfo = value;
                    this.NotifyPropertyChanged(o => o.BillCheckInfo);
                }
            }
        }
        #endregion

        #region 发起单位
        Guid launchCompanyID;
        /// <summary>
        ///  发起单位ID
        /// </summary>
        [GuidRequired(CMessage = "发起单位",EMessage="LaunchCompany")]
        public Guid LaunchCompanyID
        {
            get
            {
                return launchCompanyID;
            }
            set
            {
                if (launchCompanyID != value)
                {
                    launchCompanyID = value;
                    this.NotifyPropertyChanged(o => o.launchCompanyID);
                }
            }
        }

        string launchCompanyName;
        /// <summary>
        /// 发起单位名称
        /// </summary>
        public string LaunchCompanyName
        {
            get
            {
                return launchCompanyName;
            }
            set
            {
                if (launchCompanyName != value)
                {
                    launchCompanyName = value;
                    this.NotifyPropertyChanged(o => o.LaunchCompanyName);
                }
            }
        }

        #endregion

        #region 发起人
        Guid launchUserID;
        /// <summary>
        /// 发起人ID
        /// </summary>
        [GuidRequired(CMessage = "发起人",EMessage="LaunchUser")]
        public Guid LaunchUserID
        {
            get
            {
                return launchUserID;
            }
            set
            {
                if (launchUserID != value)
                {
                    launchUserID = value;
                    this.NotifyPropertyChanged(o => o.LaunchUserID);
                }
            }
        }

        string launchUserName;
        /// <summary>
        /// 发起人名称
        /// </summary>
        public string LaunchUserName
        {
            get
            {
                return launchUserName;
            }
            set
            {
                if (launchUserName != value)
                {
                    launchUserName = value;
                    this.NotifyPropertyChanged(o => o.LaunchUserName);
                }
            }
        }

        #endregion

        #region 核对单位
        Guid checkCompanyID;
        /// <summary>
        /// 核对单位
        /// </summary>
        [GuidRequired(CMessage = "核对单位",EMessage="CheckCompany")]
        public Guid CheckCompanyID
        {
            get
            {
                return checkCompanyID;
            }
            set
            {
                if (checkCompanyID != value)
                {
                    checkCompanyID = value;
                    this.NotifyPropertyChanged(o => o.CheckCompanyID);
                }
            }
        }

        string checkCompanyName;
        /// <summary>
        /// 核对公司名称
        /// </summary>
        public string CheckCompanyName
        {
            get
            {
                return checkCompanyName;
            }
            set
            {
                if (checkCompanyName != value)
                {
                    checkCompanyName = value;
                    this.NotifyPropertyChanged(o => o.CheckCompanyName);
                }
            }
        }

        #endregion

        #region 核对人
        Guid checkUserID;
        /// <summary>
        /// 核对人ID
        /// </summary>
        [GuidRequired(CMessage = "核对人",EMessage="CheckUser")]
        public Guid CheckUserID
        {
            get
            {
                return checkUserID;
            }
            set
            {
                if (checkUserID != value)
                {
                    checkUserID = value;
                    this.NotifyPropertyChanged(o => o.CheckUserID);
                }
            }
        }

        string checkUserName;
        /// <summary>
        /// 核对人名称
        /// </summary>
        public string CheckUserName
        {
            get
            {
                return checkUserName;
            }
            set
            {
                if (checkUserName != value)
                {
                    checkUserName = value;
                    this.NotifyPropertyChanged(o => o.CheckUserName);
                }
            }
        }


        #endregion

        #region 业务类型

        string operValues;
        /// <summary>
        /// 业务类型值
        /// </summary>
        [Required(CMessage = "业务类型",EMessage="OperValue")]
        public string OperValues
        {
            get
            {
                return operValues;
            }
            set
            {
                if (operValues != value)
                {
                    operValues = value;
                    this.NotifyPropertyChanged(o => o.OperValues);
                }
            }

        }

        /// <summary>
        /// 业务类型代码(替换后的)
        /// </summary>
        public string OperationTypes
        {
            get
            {
                return OperValues.Replace(", ", ICP.Framework.CommonLibrary.Common.GlobalConstants.DividedSymbol);
            }
        }
        string operTexts;
        /// <summary>
        /// 业务类型名称
        /// </summary>
        public string OperTexts
        {
            get
            {
                return operTexts;
            }
            set
            {
                if (operTexts != value)
                {
                    operTexts = value;
                    this.NotifyPropertyChanged(o => o.OperTexts);
                }
            }

        }


        #endregion

        #region ETD
        DateTime endingETD;
        /// <summary>
        /// ETD截止日
        /// </summary>
        [Required(CMessage = "ETD截止日期",EMessage="EndingETD")]
        public DateTime EndingETD
        {
            get
            {
                return endingETD;
            }
            set
            {
                if (endingETD != value)
                {
                    endingETD = value;
                    this.NotifyPropertyChanged(o => o.EndingETD);
                }
            }
        }

        #endregion

        #region 对账范围
        string billCheckRange;
        /// <summary>
        /// 对账范围
        /// </summary>
        public string BillCheckRange
        {
            get
            {
                billCheckRange = OperTexts + " " + EndingETD.ToShortDateString();
               
                return billCheckRange;
            }
            set
            {
                billCheckRange = value;
            }
        }
        #endregion

        #region 状态
        AgentBillCheckStatusEnum status;
        /// <summary>
        /// 状态
        /// </summary>
        public AgentBillCheckStatusEnum Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status != value)
                {
                    status = value;
                    this.NotifyPropertyChanged(o => o.Status);
                }
            }
        }
        #endregion

        #region 创建人

        Guid createID;
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateID
        {
            get
            {
                return createID;
            }
            set
            {
                if (createID != value)
                {
                    createID = value;
                    this.NotifyPropertyChanged(o => o.CreateID);
                }
            }
        }

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

        #region 创建时间
        DateTime createDate;
        /// <summary>
        /// 创建信息
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

        #region 最后更新时间

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

    #region 对账单明细
    /// <summary>
    /// 代理对帐单明细
    /// </summary>
    [Serializable]
    public class AgentBillCheckDetail
    {
        /// <summary>
        /// ETD(Estimated Time of Departure)
        /// </summary>
        public DateTime ETD
        {
            set;
            get;
        }

        /// <summary>
        /// 提单号（指的是HBL或MBL）
        /// </summary>
        /// <remarks>
        /// 来源于帐单的代理参考号
        /// </remarks>
        public string BLNO
        {
            set;
            get;
        }
        /// <summary>
        /// 币种ID
        /// </summary>
        public Guid CurrencyID
        {
            get;
            set;
        }

        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyName
        {
            get;
            set;
        }

        /// <summary>
        /// 发起代理的帐单号(可包含多个帐单号)
        /// </summary>
        /// <example>
        /// 例：OESZGS11070522,OESZGS11070336
        /// </example>
        public string LaunchBillNOs
        {
            get;
            set;
        }

        /// <summary>
        /// 发起代理的帐单个数
        /// </summary>
        public int LaunchBillNOsCount
        {
            get;
            set;
        }

        /// <summary>
        /// 发起代理的应收金额
        /// </summary>
        public decimal LaunchDebit
        {
            get;
            set;
        }

        /// <summary>
        /// 发起代理的应付金额
        /// </summary>
        public decimal LaunchCredit
        {
            get;
            set;
        }

        /// <summary>
        /// 发起代理的余额
        /// </summary>
        public decimal LaunchBalance
        {
            get;
            set;
        }

        /// <summary>
        /// 两边代理余额相加的差额
        /// </summary>
        public decimal Gap
        {
            get;
            set;
        }

        /// <summary>
        /// 核对代理的帐单号(可包含多个帐单号)
        /// </summary>
        /// <example>
        /// 例：OESZGS11070522,OESZGS11070336
        /// </example>
        public string CheckBillNOs
        {
            get;
            set;
        }

        /// <summary>
        /// 核对代理的帐单个数
        /// </summary>
        public int CheckBillNOsCount
        {
            get;
            set;
        }

        /// <summary>
        /// 核对代理的应收金额
        /// </summary>
        public decimal CheckDebit
        {
            get;
            set;
        }

        /// <summary>
        /// 核对代理的应付金额
        /// </summary>
        public decimal CheckCredit
        {
            get;
            set;
        }

        /// <summary>
        /// 核对代理的余额
        /// </summary>
        public decimal CheckBalance
        {
            get;
            set;
        }


    }
    #endregion

}
