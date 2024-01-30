using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FRM.ServiceInterface
{
    #region 商务周报列表对象
    /// <summary>
    /// 商务周报数据
    /// </summary>
    [Serializable]
    public class WeeklyReportDataInfo
    {
        /// <summary>
        /// 商务周报信息
        /// </summary>
        public List<BusinessWeeklyReportInfo> WeeklyReportDataList
        {
            get;
            set;
        }

        /// <summary>
        /// 经理批注
        /// </summary>
        public BusinessWeeklyReportList_Manager ManagerInfo
        {
            get;
            set;
        }

    
    }


    /// <summary>
    /// 商务周报表列表数据对象
    /// </summary>
    [Serializable]
    public class BusinessWeeklyReportList : BaseDataObject
    {
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

        #region 月日期
        string _MonthDate;
        /// <summary>
        ///月日期 
        /// </summary>
        public string MonthDate
        {
            get { return _MonthDate; }
            set 
            {
                if (MonthDate != value)
                {
                    _MonthDate = value;
                    this.NotifyPropertyChanged(o => o.MonthDate);
                }
            }
        }
        #endregion

        #region 周日期
        String _WeeklyDate;
        /// <summary>
        ///周日期
        /// </summary>
        public String WeeklyDate
        {
            get { return _WeeklyDate; }
            set
            {
                if (_WeeklyDate != value)
                {
                    _WeeklyDate = value;
                    this.NotifyPropertyChanged(o => o.WeeklyDate);
                }
            }
        }
        #endregion

        #region 口岸名称
        String _DivisionName;
        /// <summary>
        ///口岸名称
        /// </summary>
        public String DivisionName
        {
            get { return _DivisionName; }
            set
            {
                if (_DivisionName != value)
                {
                    _DivisionName = value;
                    this.NotifyPropertyChanged(o => o.DivisionName);
                }
            }
        }
        #endregion

        #region 航线名称
        String _ShiplineName;
        /// <summary>
        ///航线名称
        /// </summary>
        public String ShiplineName
        {
            get { return _ShiplineName; }
            set
            {
                if (_ShiplineName != value)
                {
                    _ShiplineName = value;
                    this.NotifyPropertyChanged(o => o.ShiplineName);
                }
            }
        }
        #endregion

        #region  船东&承运人名称
        String _CarrierName;
        /// <summary>
        /// 船东&承运人名称
        /// </summary>
        public String CarrierName
        {
            get { return _CarrierName; }
            set
            {
                if (_CarrierName != value)
                {
                    _CarrierName = value;
                    this.NotifyPropertyChanged(o => o.CarrierName);
                }
            }
        }
        #endregion

        #region  船东&承运人代码
        String _CarrierCode;
        /// <summary>
        /// 船东&承运人代码
        /// </summary>
        public String CarrierCode
        {
            get { return _CarrierCode; }
            set
            {
                if (_CarrierCode != value)
                {
                    _CarrierCode = value;
                    this.NotifyPropertyChanged(o => o.CarrierCode);
                }
            }
        }
        #endregion

        #region Rates
        String _Rates;
        /// <summary>
        /// Rates
        /// </summary> 
        public String Rates
        {
            get { return _Rates; }
            set
            {
                if (_Rates != value)
                {
                    _Rates = value;
                    this.NotifyPropertyChanged(o => o.Rates);
                }
            }
        }
        #endregion

        #region ShippingSpace
        String _ShippingSpace;
        /// <summary>
        /// ShippingSpace
        /// </summary>
        public String ShippingSpace
        {
            get { return _ShippingSpace; }
            set
            {
                if (_ShippingSpace != value)
                {
                    _ShippingSpace = value;
                    this.NotifyPropertyChanged(o => o.ShippingSpace);
                }
            }
        }
        #endregion

        #region Description
        String _Description;
        /// <summary>
        ///Description
        /// </summary>
        public String Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    this.NotifyPropertyChanged(o => o.Description);
                }
            }
        }
        #endregion

        #region 创建人名称
        String _CreateByName;
        /// <summary>
        ///创建人名称
        /// </summary>
        public String CreateByName
        {
            get { return _CreateByName; }
            set
            {
                if (_CreateByName != value)
                {
                    _CreateByName = value;
                    this.NotifyPropertyChanged(o => o.CreateByName);
                }
            }
        }
        #endregion

        #region 创建时间 
        DateTime _CreateDate;
        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set
            {
                if (_CreateDate != value)
                {
                    _CreateDate = value;
                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }
        }
        #endregion

        #region 最后更新时间
        DateTime? _UpdateDate;
        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set
            {
                if (_UpdateDate != value)
                {
                    _UpdateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }
        #endregion

    }

    /// <summary>
    /// 商务周报显示数据
    /// </summary>
    [Serializable]
    public class BusinessWeeklyReportData : BaseDataObject
    {
        /// <summary>
        /// 贸易区ID
        /// </summary>
        public Guid ShiplineID { get; set; }
        /// <summary>
        /// 贸易区
        /// </summary>
        public string ShiplineName { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
       /// <summary>
        /// 市场/舱位概况及分析
       /// </summary>
        public string Marketing { get; set; }
        /// <summary>
        /// 销售及配货建议
        /// </summary>
        public string SellingGuide { get; set; }
        /// <summary>
        /// 总经理批注
        /// </summary>
        public string MangerComments { get; set; }
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string BtnName { get { return "Edit"; } }

        /// <summary>
        /// 是否有更新
        /// </summary>
        public bool IsUpdate { get; set; }

        /// <summary>
        /// 分组代码
        /// </summary>
        public string OrderByCode { get; set; }
    }



    #endregion

    #region 商务周报详细信息
    /// <summary>
    /// 商务周报表详细信息数据对象
    /// </summary>
    [Serializable]
    public class BusinessWeeklyReportInfo : BusinessWeeklyReportList
    {
        #region 口岸ID
        Guid _DivisionID;
        /// <summary>
        ///口岸ID
        /// </summary>
        [GuidRequired(CMessage = "Loading Port", EMessage = "Loading Port")]
        public Guid DivisionID
        {
            get { return _DivisionID; }
            set
            {
                if (_DivisionID != value)
                {
                    _DivisionID = value;
                    this.NotifyPropertyChanged(o => o.DivisionID);
                }
            }
        }
        #endregion

        #region 航线ID
        Guid? _ShiplineID;
        /// <summary>
        ///航线ID
        /// </summary>
        [GuidRequired(CMessage = "Trading area", EMessage = "Trading area")]
        public Guid? ShiplineID
        {
            get { return _ShiplineID; }
            set
            {
                if (_ShiplineID != value)
                {
                    _ShiplineID = value;
                    this.NotifyPropertyChanged(o => o.ShiplineID);
                }
            }
        }
        #endregion

        #region 船东&承运人ID
        Guid? _CarrierID;
        /// <summary>
        ///船东&承运人ID
        /// </summary>
        public Guid? CarrierID
        {
            get { return _CarrierID; }
            set
            {
                if (_CarrierID != value)
                {
                    _CarrierID = value;
                    this.NotifyPropertyChanged(o => o.CarrierID);
                }
            }
        }
        #endregion

        #region 创建人ID
        Guid _CreateByID;
        /// <summary>
        ///创建人ID
        /// </summary>
        [GuidRequired(CMessage = "创建人", EMessage = "Create Name")]
        public Guid CreateByID
        {
            get { return _CreateByID; }
            set
            {
                if (_CreateByID != value)
                {
                    _CreateByID = value;
                    this.NotifyPropertyChanged(o => o.CreateByID);
                }
            }
        }
        #endregion
    }
    #endregion

    #region 商务周报经理信息
    /// <summary>
    /// 商务周报表列表数据对象
    /// </summary>
    [Serializable]
    public class BusinessWeeklyReportList_Manager : BaseDataObject
    {
        #region ID
        Guid _ID;
        /// <summary>
        /// 唯一键
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

        #region 月日期
        string _MonthDate;
        /// <summary>
        /// 月日期
        /// </summary>
        public string MonthDate
        {
            get { return _MonthDate; }
            set
            {
                if (_MonthDate != value)
                {
                    _MonthDate = value;
                    this.NotifyPropertyChanged(o => o.MonthDate);
                }
            }
        }
        #endregion

        #region 周日期
        string _WeeklyDate;
        /// <summary>
        /// 周日期
        /// </summary>
        public string WeeklyDate
        {
            get { return _WeeklyDate; }
            set
            {
                if (_WeeklyDate != value)
                {
                    _WeeklyDate = value;
                    this.NotifyPropertyChanged(o => o.WeeklyDate);
                }
            }
        }
        #endregion

        #region 公司名称
        Guid companyID;
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID
        {
            get { return companyID; }
            set
            {
                if (companyID != value)
                {
                    companyID = value;
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

        #region 贸易区
        private Guid shipLineID;
        /// <summary>
        /// 贸易区ID
        /// </summary>
        public Guid ShipLineID
        {
            get
            {
                return shipLineID;
            }
            set
            {
                if (shipLineID != value)
                {
                    shipLineID = value;
                    this.NotifyPropertyChanged(o => o.ShipLineID);
                }
                
            }
        }
        /// <summary>
        /// 贸易区名称
        /// </summary>
        public string ShipLineName
        {
            get;
            set;
        }
        #endregion

        #region 职位名称
        Guid jobID;
        /// <summary>
        /// 职位ID
        /// </summary>
        public Guid JobID
        {
            get { return jobID; }
            set
            {
                if (jobID != value)
                {
                    jobID = value;
                    this.NotifyPropertyChanged(o => o.JobID);
                }
            }
        }
        string _JobName;
        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName
        {
            get { return _JobName; }
            set
            {
                if (_JobName != value)
                {
                    _JobName = value;
                    this.NotifyPropertyChanged(o => o.JobName);
                }
            }
        }
        #endregion

        #region 详细信息
        string _Description;
        /// <summary>
        /// 详细信息
        /// </summary>
        [Required(CMessage = "Description", EMessage = "Description")]
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    this.NotifyPropertyChanged(o => o.Description);
                }
            }
        }
        #endregion

        #region CreateInfo
        public DateTime? UpdateDate
        {
            get;
            set;
        }
        Guid _CreateByID;
        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID
        {
            get { return _CreateByID; }
            set
            {
                if (_CreateByID != value)
                {
                    _CreateByID = value;
                    this.NotifyPropertyChanged(o => o.CreateByID);
                }
            }
        }

        string _CreateByName;
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName
        {
            get { return _CreateByName; }
            set
            {
                if (_CreateByName != value)
                {
                    _CreateByName = value;
                    this.NotifyPropertyChanged(o => o.Description);
                }
            }
        }

        DateTime _CreateDate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set
            {
                if (_CreateDate != value)
                {
                    _CreateDate = value;
                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }
        }
        #endregion
    }
    #endregion

    #region 商务周报、业务员
    /// <summary>
    /// 商务周报表列表数据对象
    /// </summary>
    [Serializable]
    public class BusinessWeeklyReportList_Sales : BaseDataObject
    {
        #region ID
        Guid _ID;
        /// <summary>
        /// 唯一键
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

        #region 周日期
        string _WeeklyDate;
        /// <summary>
        /// 周日期
        /// </summary>
        public string WeeklyDate
        {
            get { return _WeeklyDate; }
            set
            {
                if (_WeeklyDate != value)
                {
                    _WeeklyDate = value;
                    this.NotifyPropertyChanged(o => o.WeeklyDate);
                }
            }
        }
        #endregion
 
        #region 详细信息
        string _Description;
        /// <summary>
        /// 详细信息
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    this.NotifyPropertyChanged(o => o.Description);
                }
            }
        }
        #endregion

        #region CreateInfo

        Guid _CreateByID;
        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID
        {
            get { return _CreateByID; }
            set
            {
                if (_CreateByID != value)
                {
                    _CreateByID = value;
                    this.NotifyPropertyChanged(o => o.CreateByID);
                }
            }
        }

        string _CreateByName;
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName
        {
            get { return _CreateByName; }
            set
            {
                if (_CreateByName != value)
                {
                    _CreateByName = value;
                    this.NotifyPropertyChanged(o => o.Description);
                }
            }
        }

        DateTime _CreateDate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set
            {
                if (_CreateDate != value)
                {
                    _CreateDate = value;
                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }
        }
        #endregion
    }
    #endregion

    #region 订舱统计报表
    [Serializable]
    public class BookingReportData
    {
        public Guid ID { get; set; }
        public DateTime? ClosingDate { get; set; }
        public DateTime? ETD { get; set; }
        public string VoyageName { get; set; }
        public string SONO { get; set; }
        public string POLName { get; set; }
        public string PODName { get; set; }
        public string ContainerType { get; set; }
        public decimal ContainerCount { get; set; }
        public string SalesType { get; set; }
        public string CustomerName { get; set; }
        public string SalesName { get; set; }
        public string FilerName { get; set; }
        public string CustomerService { get; set; }
        public string CancelRemark { get; set; }
        public string Remark { get; set; }
        public decimal Profit { get; set; }
        public string Commodities { get; set; }
        public string ContractNo { get; set; }
        public string CompanyName { get; set; }
        public string ShipLineName { get;set; }
        public string CarrierName { get; set; }
    }
    #endregion

}
