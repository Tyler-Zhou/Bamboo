using System;
using System.Collections.Generic; 
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FRM.ServiceInterface.DataObjects;


namespace ICP.FRM.UI.InquireRates
{

    #region 客户端数据源

    /// <summary>
    /// 询价基础数据模型(海运询价,空运询价，拖车询价数据对象都继承它)
    /// </summary>
    public class ClientBaseInquireRate : BaseDataObject
    {
        public override bool IsNew { get { return ID.IsNullOrEmpty(); } }
        public bool IsNewRecord { get; set; }   //copy时设为true,保存后设为false

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

        #region ParentID

        Guid? _MainRecordID;
        /// <summary>
        /// 主记录ID(用于回复子询价)
        /// </summary>
        public Guid? MainRecordID
        {
            get { return _MainRecordID; }
            set
            {
                if (_MainRecordID != value)
                {
                    _MainRecordID = value;
                    this.NotifyPropertyChanged(o => o.MainRecordID);
                }
            }
        }

        #endregion

        #region 询价单号

        string _No;
        /// <summary>
        /// 询价单号
        /// </summary>
        public string No
        {
            get { return _No; }
            set
            {
                if (_No != value)
                {
                    _No = value;
                    this.NotifyPropertyChanged(o => o.No);
                }
            }
        }

        #endregion

        #region 未确认业务

        /// <summary>
        /// 未确认业务
        /// </summary>
        public Boolean HasUnconfirmedShipment { get; set; }

        #endregion

        #region 是否有未读的消息
        bool _hasUnRead;

        /// <summary>
        /// 是否有未读的信息
        /// </summary>
        public bool HasUnRead
        {
            get { return _hasUnRead; }
            set
            {
                if (_hasUnRead != value)
                {
                    _hasUnRead = value;
                    this.NotifyPropertyChanged(o => o.HasUnRead);
                }
            }
        }
        #endregion

        #region 是否回复价格(如：价格列20GP,40HP,40GP都没有价格，即定义为没有回复价格)

        bool _IsNoPriceAll;

        /// <summary>
        /// 是否都没回复价格(如：价格列20GP,40HP,40GP都没有价格，即定义为没有回复价格)
        /// </summary>
        public bool IsNoPriceAll
        {
            get { return _IsNoPriceAll; }
            set
            {
                if (_IsNoPriceAll != value)
                {
                    _IsNoPriceAll = value;
                    this.NotifyPropertyChanged(o => o.IsNoPriceAll);
                }
            }
        }

        #endregion

        #region 询价人和回复人
        Guid? _InquireByID;
        /// <summary>
        /// 建立人，即询价人，一般为业务员
        /// </summary>
        public Guid? InquireByID
        {
            get
            {
                return _InquireByID;
            }
            set
            {
                if (_InquireByID != value)
                {
                    _InquireByID = value;
                    base.OnPropertyChanged("InquireByID", value);
                }
            }
        }

        string _InquireByName;

        /// <summary>
        /// 建立人，即询价人，一般为业务员
        /// </summary>
        public string InquireByName
        {
            get
            {
                return _InquireByName;
            }
            set
            {
                if (_InquireByName != value)
                {
                    _InquireByName = value;
                    base.OnPropertyChanged("InquireByName", value);
                }
            }
        }

        Guid? _respondByID;
        /// <summary>
        /// 回复人
        /// </summary>
        public Guid? RespondByID
        {
            get
            {
                return _respondByID;
            }
            set
            {
                if (_respondByID != value)
                {
                    _respondByID = value;
                    base.OnPropertyChanged("RespondByID", value);
                }
            }
        }

        /// <summary>
        /// 回复人
        /// </summary>
        public string RespondByName { get; set; }

        #endregion

        #region 讨论List信息

        /// <summary>
        /// 讨论List信息
        /// </summary>
        public List<InquireDiscussing> DiscussingList;

        #endregion

        #region 船东
        Guid? _CarrierID;
        /// <summary>
        /// 船东ID
        /// </summary>
        //[GuidRequired(ErrorMessage = "Carrier Must Input.")]
        public Guid? CarrierID
        {
            get
            {
                return _CarrierID;
            }
            set
            {
                if (_CarrierID != value)
                {
                    _CarrierID = value;
                    base.OnPropertyChanged("CarrierID", value);
                }
            }
        }

        string _CarrierName;

        /// <summary>
        /// 船东
        /// </summary>
        public string CarrierName
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

        #region 运输条款

        Guid? _transportClauseID;
        /// <summary>
        /// 运输条款ID
        /// </summary>
        public Guid? TransportClauseID
        {
            get
            {
                return _transportClauseID;
            }
            set
            {
                if (_transportClauseID != value)
                {
                    _transportClauseID = value;
                    base.OnPropertyChanged("TransportClauseID", value);
                }
            }
        }

        string _transportClauseName;
        /// <summary>
        /// 运输条款
        /// </summary>
        public string TransportClauseName
        {
            get { return _transportClauseName; }
            set
            {
                if (_transportClauseName != value)
                {
                    _transportClauseName = value;
                    this.NotifyPropertyChanged(o => o.TransportClauseName);
                }
            }
        }

        #endregion

        #region Port

        Guid? _POLID;
        /// <summary>
        /// 装货港ID
        /// </summary>
        [GuidRequired(CMessage="装货港",EMessage = "POL")]
        public Guid? POLID
        {
            get { return _POLID; }
            set
            {
                if (_POLID != value)
                {
                    _POLID = value;
                    this.NotifyPropertyChanged(o => o.POLID);
                }
            }
        }

        string _POLName;
        /// <summary>
        /// 装货港
        /// </summary>
        public string POLName
        {
            get { return _POLName; }
            set
            {
                if (_POLName != value)
                {
                    _POLName = value;
                    this.NotifyPropertyChanged(o => o.POLName);
                }
            }
        }

        ///// <summary>
        ///// 中转港ID
        ///// </summary>
        //public Guid? VIAID { get; set; }

        ///// <summary>
        ///// 中转港
        ///// </summary>
        //public string VIAName { get; set; }

        Guid? _PODID;
        /// <summary>
        /// 卸货港ID
        /// </summary>
        [GuidRequired(CMessage="卸货港",EMessage = "POD")]
        public Guid? PODID
        {
            get { return _PODID; }
            set
            {
                if (_PODID != value)
                {
                    _PODID = value;
                    this.NotifyPropertyChanged(o => o.PODID);
                }
            }
        }

        string _PODName;
        /// <summary>
        /// 卸货港
        /// </summary>
        public string PODName
        {
            get { return _PODName; }
            set
            {
                if (_PODName != value)
                {
                    _PODName = value;
                    this.NotifyPropertyChanged(o => o.PODName);
                }
            }
        }

        Guid? _PlaceOfDeliveryID;
        /// <summary>
        /// 交货地ID
        /// </summary>
        public Guid? PlaceOfDeliveryID
        {
            get { return _PlaceOfDeliveryID; }
            set
            {
                if (_PlaceOfDeliveryID != value)
                {
                    _PlaceOfDeliveryID = value;
                    this.NotifyPropertyChanged(o => o.PlaceOfDeliveryID);
                }
            }
        }

        string _PlaceOfDeliveryName;
        /// <summary>
        /// 交货地
        /// </summary>
        public string PlaceOfDeliveryName
        {
            get { return _PlaceOfDeliveryName; }
            set
            {
                if (_PlaceOfDeliveryName != value)
                {
                    _PlaceOfDeliveryName = value;
                    this.NotifyPropertyChanged(o => o.PlaceOfDeliveryName);
                }
            }
        }

        #endregion

        #region 品名

        string _Commodity;
        /// <summary>
        /// 品名
        /// </summary>
        public string Commodity
        {
            get { return _Commodity; }
            set
            {
                if (_Commodity != value)
                {
                    _Commodity = value;
                    this.NotifyPropertyChanged(o => o.Commodity);
                }
            }
        }

        #endregion

        #region 币种
        Guid? _CurrencyID;
        /// <summary>
        /// CurrencyID
        /// </summary>
        public Guid? CurrencyID
        {
            get { return _CurrencyID; }
            set
            {
                if (_CurrencyID != value)
                {
                    _CurrencyID = value;
                    this.NotifyPropertyChanged(o => o.CurrencyID);
                }
            }
        }

        string _CurrencyName;
        /// <summary>
        /// CurrencyName
        /// </summary>
        public string CurrencyName
        {
            get { return _CurrencyName; }
            set
            {
                if (_CurrencyName != value)
                {
                    _CurrencyName = value;
                    this.NotifyPropertyChanged(o => o.CurrencyName);
                }
            }
        }

        #endregion

        #region 时间从 到

        DateTime? _DurationFrom;
        /// <summary>
        /// DurationFrom
        /// </summary>
        public DateTime? DurationFrom
        {
            get { return _DurationFrom; }
            set
            {
                if (_DurationFrom != value)
                {
                    _DurationFrom = value;
                    this.NotifyPropertyChanged(o => o.DurationFrom);
                }
            }
        }

        DateTime? _DurationTo;
        /// <summary>
        /// DurationTo
        /// </summary>
        public DateTime? DurationTo
        {
            get { return _DurationTo; }
            set
            {
                if (_DurationTo != value)
                {
                    _DurationTo = value;
                    this.NotifyPropertyChanged(o => o.DurationTo);
                }
            }
        }

        #endregion

        #region 备注
        string _Remark;
        /// <summary>
        /// 备注
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

        #region 是否共享(标识为共享后才可以在查询过程中查到)

        bool _Shared;
        /// <summary>
        /// 是否共享(标识为共享后才可以在查询过程中查到)
        /// </summary>
        public bool Shared
        {
            get { return _Shared; }
            set
            {
                if (_Shared != value)
                {
                    _Shared = value;
                    this.NotifyPropertyChanged(o => o.Shared);
                }
            }
        }

        #endregion

        #region General Info

        #region 航线

        Guid? _shippinglineid;
        /// <summary>
        /// 航线ID
        /// </summary>
        [GuidRequired(CMessage="航线",EMessage = "Shipline")]
        public Guid? ShippingLineID
        {
            get
            {
                return _shippinglineid;
            }
            set
            {
                if (_shippinglineid != value)
                {
                    _shippinglineid = value;
                    this.NotifyPropertyChanged(o => o.ShippingLineID);
                }
            }
        }

        string _shippinglinename;
        /// <summary>
        /// 航线名称
        /// </summary>
        public string ShippingLineName
        {
            get
            {
                return _shippinglinename;
            }
            set
            {
                if (_shippinglinename != value)
                {
                    _shippinglinename = value;
                    base.OnPropertyChanged("ShippingLineName", value);
                }
            }
        }

        #endregion

        #region 客户

        Guid? _CustomerID;
        /// <summary>
        /// CustomerID
        /// </summary>
        [GuidRequired(CMessage="客户",EMessage = "Customer")]
        public Guid? CustomerID
        {
            get { return _CustomerID; }
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
        /// CustomerName
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

        #region Text of Rate Unit

        public string RateUnitText { get; set; }

        #endregion

        #region 期望船东

        string _ExpCarrierName;
        /// <summary>
        /// 业务员输入的Carrier
        /// </summary>
        public string ExpCarrierName
        {
            get { return _ExpCarrierName; }
            set
            {
                if (_ExpCarrierName != value)
                {
                    _ExpCarrierName = value;
                    this.NotifyPropertyChanged(o => o.ExpCarrierName);
                }
            }
        }

        #endregion

        #region 期望品名

        string _ExpCommodity;
        /// <summary>
        /// 业务员输入的品名
        /// </summary>
        [StringLength(MaximumLength=2000,CMessage="期望品名",EMessage="ExpCommddity")]
        [Required(CMessage = "期望品名", EMessage = "ExpCommodity")]
        public string ExpCommodity
        {
            get { return _ExpCommodity; }
            set
            {
                if (_ExpCommodity != value)
                {
                    _ExpCommodity = value;
                    this.NotifyPropertyChanged(o => o.ExpCommodity);
                }
            }
        }

        #endregion

        #region 期望运输条款

        Guid? _ExpTransportClauseID;
        /// <summary>
        /// 业务员输入的TermID
        /// </summary>
        public Guid? ExpTransportClauseID
        {
            get { return _ExpTransportClauseID; }
            set
            {
                if (_ExpTransportClauseID != value)
                {
                    _ExpTransportClauseID = value;
                    this.NotifyPropertyChanged(o => o.ExpTransportClauseID);
                }
            }
        }

        string _ExpTransportClauseName;
        /// <summary>
        /// 业务员输入的Term
        /// </summary>
        public string ExpTransportClauseName
        {
            get { return _ExpTransportClauseName; }
            set
            {
                if (_ExpTransportClauseName != value)
                {
                    _ExpTransportClauseName = value;
                    this.NotifyPropertyChanged(o => o.ExpTransportClauseName);
                }
            }
        }

        #endregion

        #region 重量 体积

        string _CargoWeight;
        /// <summary>
        /// 重量
        /// </summary>
        public string CargoWeight
        {
            get { return _CargoWeight; }
            set
            {
                if (_CargoWeight != value)
                {
                    _CargoWeight = value;
                    this.NotifyPropertyChanged(o => o.CargoWeight);
                }
            }
        }

        string _Measurement;
        /// <summary>
        /// 体积
        /// </summary>
        public string Measurement
        {
            get { return _Measurement; }
            set
            {
                if (_Measurement != value)
                {
                    _Measurement = value;
                    this.NotifyPropertyChanged(o => o.Measurement);
                }
            }
        }

        #endregion

        #region 期望杂

        string _CargoReady;
        /// <summary>
        /// CargoReady
        /// </summary>
        public string CargoReady
        {
            get { return _CargoReady; }
            set
            {
                if (_CargoReady != value)
                {
                    _CargoReady = value;
                    this.NotifyPropertyChanged(o => o.CargoReady);
                }
            }
        }

        string _EstimateTimeOfDelivery;
        /// <summary>
        /// EstimateTimeOfDelivery
        /// </summary>
        public string EstimateTimeOfDelivery
        {
            get { return _EstimateTimeOfDelivery; }
            set
            {
                if (_EstimateTimeOfDelivery != value)
                {
                    _EstimateTimeOfDelivery = value;
                    this.NotifyPropertyChanged(o => o.EstimateTimeOfDelivery);
                }
            }
        }

        bool _IsWillBooking;
        /// <summary>
        /// 是否会出booking
        /// </summary>
        public bool IsWillBooking
        {
            get { return _IsWillBooking; }
            set
            {
                if (_IsWillBooking != value)
                {
                    _IsWillBooking = value;
                    this.NotifyPropertyChanged(o => o.IsWillBooking);
                }
            }
        }

        string _ExpPrice;
        /// <summary>
        /// 期望价格
        /// </summary>
        public string ExpPrice
        {
            get { return _ExpPrice; }
            set
            {
                if (_ExpPrice != value)
                {
                    _ExpPrice = value;
                    this.NotifyPropertyChanged(o => o.ExpPrice);
                }
            }
        }

        #endregion

        #region 新建询价时的询问内容

        string _DiscussingWhenNew;
        /// <summary>
        /// Discussing
        /// </summary>
        public string DiscussingWhenNew
        {
            get { return _DiscussingWhenNew; }
            set
            {
                if (_DiscussingWhenNew != value)
                {
                    _DiscussingWhenNew = value;
                    this.NotifyPropertyChanged(o => o.DiscussingWhenNew);
                }
            }
        }

        #endregion

        #endregion

        public List<InquireUnit> RateUnitList { get; set; }

        #region 创建时间和行版本

        DateTime _CreateDate;
        /// <summary>
        /// 创建时间
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

        DateTime? _UpdateDate;
        /// <summary>
        /// 行版本
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

        DateTimeOffset? _DTime;
        /// <summary>
        /// 用户所在时区的更新时间
        /// </summary>
        public DateTimeOffset? DTime
        {
            get { return _DTime; }
            set
            {
                if (_DTime != value)
                {
                    _DTime = value;
                    this.NotifyPropertyChanged(o => o.DTime);
                }
            }
        }

        /// <summary>
        /// 列表显示用
        /// </summary>
        public string BizUpdateTime
        {
            get
            {
                if (DTime == null)
                {
                    return string.Empty;
                }

                return DTime.Value.LocalDateTime.ToShortDateString();
            }
        }

        #endregion
    }

    #region 海运询价

    /// <summary>
    /// 询价基础数据模型(海运询价,空运询价，拖车询价数据对象都继承它)
    /// </summary>
    public class ClientInquierOceanRate : ClientBaseInquireRate
    {
        #region 附加费

        string _SurCharge;
        /// <summary>
        /// SurCharge
        /// </summary>
        [StringLength(MaximumLength =500,CMessage="附加费",EMessage="SurCharge")]
        public string SurCharge
        {
            get { return _SurCharge; }
            set
            {
                if (_SurCharge != value)
                {
                    _SurCharge = value;
                    this.NotifyPropertyChanged(o => o.SurCharge);
                }
            }
        }

        #endregion

        #region Rates

        #region Rate_45FR
        decimal? _Rate_45FR;
        /// <summary>
        ///Rate_45FR
        /// </summary>
        public decimal? Rate_45FR
        {
            get { return _Rate_45FR; }
            set
            {
                if (_Rate_45FR != value)
                {
                    _Rate_45FR = value;
                    this.NotifyPropertyChanged(o => o.Rate_45FR);
                }
            }
        }
        #endregion

        #region Rate_40RF
        decimal? _Rate_40RF;
        /// <summary>
        ///Rate_40RF
        /// </summary>
        public decimal? Rate_40RF
        {
            get { return _Rate_40RF; }
            set
            {
                if (_Rate_40RF != value)
                {
                    _Rate_40RF = value;
                    this.NotifyPropertyChanged(o => o.Rate_40RF);
                }
            }
        }
        #endregion

        #region Rate_45HT
        decimal? _Rate_45HT;
        /// <summary>
        ///Rate_45HT
        /// </summary>
        public decimal? Rate_45HT
        {
            get { return _Rate_45HT; }
            set
            {
                if (_Rate_45HT != value)
                {
                    _Rate_45HT = value;
                    this.NotifyPropertyChanged(o => o.Rate_45HT);
                }
            }
        }
        #endregion

        #region Rate_20RF
        decimal? _Rate_20RF;
        /// <summary>
        ///Rate_20RF
        /// </summary>
        public decimal? Rate_20RF
        {
            get { return _Rate_20RF; }
            set
            {
                if (_Rate_20RF != value)
                {
                    _Rate_20RF = value;
                    this.NotifyPropertyChanged(o => o.Rate_20RF);
                }
            }
        }
        #endregion

        #region Rate_20HQ
        decimal? _Rate_20HQ;
        /// <summary>
        ///Rate_20HQ
        /// </summary>
        public decimal? Rate_20HQ
        {
            get { return _Rate_20HQ; }
            set
            {
                if (_Rate_20HQ != value)
                {
                    _Rate_20HQ = value;
                    this.NotifyPropertyChanged(o => o.Rate_20HQ);
                }
            }
        }
        #endregion

        #region Rate_20TK
        decimal? _Rate_20TK;
        /// <summary>
        ///Rate_20TK
        /// </summary>
        public decimal? Rate_20TK
        {
            get { return _Rate_20TK; }
            set
            {
                if (_Rate_20TK != value)
                {
                    _Rate_20TK = value;
                    this.NotifyPropertyChanged(o => o.Rate_20TK);
                }
            }
        }
        #endregion

        #region Rate_20GP
        decimal? _Rate_20GP;
        /// <summary>
        ///Rate_20GP
        /// </summary>
        public decimal? Rate_20GP
        {
            get { return _Rate_20GP; }
            set
            {
                if (_Rate_20GP != value)
                {
                    _Rate_20GP = value;
                    this.NotifyPropertyChanged(o => o.Rate_20GP);
                }
            }
        }
        #endregion

        #region Rate_40TK
        decimal? _Rate_40TK;
        /// <summary>
        ///Rate_40TK
        /// </summary>
        public decimal? Rate_40TK
        {
            get { return _Rate_40TK; }
            set
            {
                if (_Rate_40TK != value)
                {
                    _Rate_40TK = value;
                    this.NotifyPropertyChanged(o => o.Rate_40TK);
                }
            }
        }
        #endregion

        #region Rate_40OT
        decimal? _Rate_40OT;
        /// <summary>
        ///Rate_40OT
        /// </summary>
        public decimal? Rate_40OT
        {
            get { return _Rate_40OT; }
            set
            {
                if (_Rate_40OT != value)
                {
                    _Rate_40OT = value;
                    this.NotifyPropertyChanged(o => o.Rate_40OT);
                }
            }
        }
        #endregion

        #region Rate_20FR
        decimal? _Rate_20FR;
        /// <summary>
        ///Rate_20FR
        /// </summary>
        public decimal? Rate_20FR
        {
            get { return _Rate_20FR; }
            set
            {
                if (_Rate_20FR != value)
                {
                    _Rate_20FR = value;
                    this.NotifyPropertyChanged(o => o.Rate_20FR);
                }
            }
        }
        #endregion

        #region Rate_45GP
        decimal? _Rate_45GP;
        /// <summary>
        ///Rate_45GP
        /// </summary>
        public decimal? Rate_45GP
        {
            get { return _Rate_45GP; }
            set
            {
                if (_Rate_45GP != value)
                {
                    _Rate_45GP = value;
                    this.NotifyPropertyChanged(o => o.Rate_45GP);
                }
            }
        }
        #endregion

        #region Rate_40GP
        decimal? _Rate_40GP;
        /// <summary>
        ///Rate_40GP
        /// </summary>
        public decimal? Rate_40GP
        {
            get { return _Rate_40GP; }
            set
            {
                if (_Rate_40GP != value)
                {
                    _Rate_40GP = value;
                    this.NotifyPropertyChanged(o => o.Rate_40GP);
                }
            }
        }
        #endregion

        #region Rate_45RF
        decimal? _Rate_45RF;
        /// <summary>
        ///Rate_45RF
        /// </summary>
        public decimal? Rate_45RF
        {
            get { return _Rate_45RF; }
            set
            {
                if (_Rate_45RF != value)
                {
                    _Rate_45RF = value;
                    this.NotifyPropertyChanged(o => o.Rate_45RF);
                }
            }
        }
        #endregion

        #region Rate_20RH
        decimal? _Rate_20RH;
        /// <summary>
        ///Rate_20RH
        /// </summary>
        public decimal? Rate_20RH
        {
            get { return _Rate_20RH; }
            set
            {
                if (_Rate_20RH != value)
                {
                    _Rate_20RH = value;
                    this.NotifyPropertyChanged(o => o.Rate_20RH);
                }
            }
        }
        #endregion

        #region Rate_45OT
        decimal? _Rate_45OT;
        /// <summary>
        ///Rate_45OT
        /// </summary>
        public decimal? Rate_45OT
        {
            get { return _Rate_45OT; }
            set
            {
                if (_Rate_45OT != value)
                {
                    _Rate_45OT = value;
                    this.NotifyPropertyChanged(o => o.Rate_45OT);
                }
            }
        }
        #endregion

        #region Rate_40NOR
        decimal? _Rate_40NOR;
        /// <summary>
        ///Rate_40NOR
        /// </summary>
        public decimal? Rate_40NOR
        {
            get { return _Rate_40NOR; }
            set
            {
                if (_Rate_40NOR != value)
                {
                    _Rate_40NOR = value;
                    this.NotifyPropertyChanged(o => o.Rate_40NOR);
                }
            }
        }
        #endregion

        #region Rate_40FR
        decimal? _Rate_40FR;
        /// <summary>
        ///Rate_40FR
        /// </summary>
        public decimal? Rate_40FR
        {
            get { return _Rate_40FR; }
            set
            {
                if (_Rate_40FR != value)
                {
                    _Rate_40FR = value;
                    this.NotifyPropertyChanged(o => o.Rate_40FR);
                }
            }
        }
        #endregion

        #region Rate_20OT
        decimal? _Rate_20OT;
        /// <summary>
        ///Rate_20OT
        /// </summary>
        public decimal? Rate_20OT
        {
            get { return _Rate_20OT; }
            set
            {
                if (_Rate_20OT != value)
                {
                    _Rate_20OT = value;
                    this.NotifyPropertyChanged(o => o.Rate_20OT);
                }
            }
        }
        #endregion

        #region Rate_45TK
        decimal? _Rate_45TK;
        /// <summary>
        ///Rate_45TK
        /// </summary>
        public decimal? Rate_45TK
        {
            get { return _Rate_45TK; }
            set
            {
                if (_Rate_45TK != value)
                {
                    _Rate_45TK = value;
                    this.NotifyPropertyChanged(o => o.Rate_45TK);
                }
            }
        }
        #endregion

        #region Rate_20NOR
        decimal? _Rate_20NOR;
        /// <summary>
        ///Rate_20NOR
        /// </summary>
        public decimal? Rate_20NOR
        {
            get { return _Rate_20NOR; }
            set
            {
                if (_Rate_20NOR != value)
                {
                    _Rate_20NOR = value;
                    this.NotifyPropertyChanged(o => o.Rate_20NOR);
                }
            }
        }
        #endregion

        #region Rate_40HT
        decimal? _Rate_40HT;
        /// <summary>
        ///Rate_40HT
        /// </summary>
        public decimal? Rate_40HT
        {
            get { return _Rate_40HT; }
            set
            {
                if (_Rate_40HT != value)
                {
                    _Rate_40HT = value;
                    this.NotifyPropertyChanged(o => o.Rate_40HT);
                }
            }
        }
        #endregion

        #region Rate_40RH
        decimal? _Rate_40RH;
        /// <summary>
        ///Rate_40RH
        /// </summary>
        public decimal? Rate_40RH
        {
            get { return _Rate_40RH; }
            set
            {
                if (_Rate_40RH != value)
                {
                    _Rate_40RH = value;
                    this.NotifyPropertyChanged(o => o.Rate_40RH);
                }
            }
        }
        #endregion

        #region Rate_45RH
        decimal? _Rate_45RH;
        /// <summary>
        ///Rate_45RH
        /// </summary>
        public decimal? Rate_45RH
        {
            get { return _Rate_45RH; }
            set
            {
                if (_Rate_45RH != value)
                {
                    _Rate_45RH = value;
                    this.NotifyPropertyChanged(o => o.Rate_45RH);
                }
            }
        }
        #endregion

        #region Rate_45HQ
        decimal? _Rate_45HQ;
        /// <summary>
        ///Rate_45HQ
        /// </summary>
        public decimal? Rate_45HQ
        {
            get { return _Rate_45HQ; }
            set
            {
                if (_Rate_45HQ != value)
                {
                    _Rate_45HQ = value;
                    this.NotifyPropertyChanged(o => o.Rate_45HQ);
                }
            }
        }
        #endregion

        #region Rate_20HT
        decimal? _Rate_20HT;
        /// <summary>
        ///Rate_20HT
        /// </summary>
        public decimal? Rate_20HT
        {
            get { return _Rate_20HT; }
            set
            {
                if (_Rate_20HT != value)
                {
                    _Rate_20HT = value;
                    this.NotifyPropertyChanged(o => o.Rate_20HT);
                }
            }
        }
        #endregion

        #region Rate_40HQ
        decimal? _Rate_40HQ;
        /// <summary>
        ///Rate_40HQ
        /// </summary>
        public decimal? Rate_40HQ
        {
            get { return _Rate_40HQ; }
            set
            {
                if (_Rate_40HQ != value)
                {
                    _Rate_40HQ = value;
                    this.NotifyPropertyChanged(o => o.Rate_40HQ);
                }
            }
        }
        #endregion
        #endregion

        #region 方法

        /// <summary>
        /// 把所有箱属性置为空
        /// </summary>
        public void SetALLRateNull()
        {
            Rate_45FR = null;
            Rate_40RF = null;
            Rate_45HT = null;
            Rate_20RF = null;
            Rate_20HQ = null;
            Rate_20TK = null;
            Rate_20GP = null;
            Rate_40TK = null;
            Rate_40OT = null;
            Rate_20FR = null;
            Rate_45GP = null;
            Rate_40GP = null;
            Rate_45RF = null;
            Rate_20RH = null;
            Rate_45OT = null;
            Rate_40NOR = null;
            Rate_40FR = null;
            Rate_20OT = null;
            Rate_45TK = null;
            Rate_20NOR = null;
            Rate_40HT = null;
            Rate_40RH = null;
            Rate_45RH = null;
            Rate_45HQ = null;
            Rate_20HT = null;
            Rate_40HQ = null;
        }

        /// <summary>
        /// 把所有不需要用到的箱属性置为空,用到的设为0
        /// </summary>
        /// <param name="units">根据一个OceanUnitList列表生成</param>
        public void BulidRateToZeroByOceanUints(List<InquireUnit> units)
        {
            SetALLRateNull();
            foreach (var item in units)
            {
                switch (item.UnitName)
                {
                    case "45FR": Rate_45FR = 0; break;
                    case "40RF": Rate_40RF = 0; break;
                    case "45HT": Rate_45HT = 0; break;
                    case "20RF": Rate_20RF = 0; break;
                    case "20HQ": Rate_20HQ = 0; break;
                    case "20TK": Rate_20TK = 0; break;
                    case "20GP": Rate_20GP = 0; break;
                    case "40TK": Rate_40TK = 0; break;
                    case "40OT": Rate_40OT = 0; break;
                    case "20FR": Rate_20FR = 0; break;
                    case "45GP": Rate_45GP = 0; break;
                    case "40GP": Rate_40GP = 0; break;
                    case "45RF": Rate_45RF = 0; break;
                    case "20RH": Rate_20RH = 0; break;
                    case "45OT": Rate_45OT = 0; break;
                    case "40NOR": Rate_40NOR = 0; break;
                    case "40FR": Rate_40FR = 0; break;
                    case "20OT": Rate_20OT = 0; break;
                    case "45TK": Rate_45TK = 0; break;
                    case "20NOR": Rate_20NOR = 0; break;
                    case "40HT": Rate_40HT = 0; break;
                    case "40RH": Rate_40RH = 0; break;
                    case "45RH": Rate_45RH = 0; break;
                    case "45HQ": Rate_45HQ = 0; break;
                    case "20HT": Rate_20HT = 0; break;
                    case "40HQ": Rate_40HQ = 0; break;
                }
            }
        }

        /// <summary>
        /// 验证所有运费.如果所有运费都为0 返回False
        /// </summary>
        /// <returns></returns>
        public bool ValidateHasRate()
        {
            if (Rate_45FR.IsNullOrZero() == false) return true;
            if (Rate_40RF.IsNullOrZero() == false) return true;
            if (Rate_45HT.IsNullOrZero() == false) return true;
            if (Rate_20RF.IsNullOrZero() == false) return true;
            if (Rate_20HQ.IsNullOrZero() == false) return true;
            if (Rate_20TK.IsNullOrZero() == false) return true;
            if (Rate_20GP.IsNullOrZero() == false) return true;
            if (Rate_40TK.IsNullOrZero() == false) return true;
            if (Rate_40OT.IsNullOrZero() == false) return true;
            if (Rate_20FR.IsNullOrZero() == false) return true;
            if (Rate_45GP.IsNullOrZero() == false) return true;
            if (Rate_40GP.IsNullOrZero() == false) return true;
            if (Rate_45RF.IsNullOrZero() == false) return true;
            if (Rate_20RH.IsNullOrZero() == false) return true;
            if (Rate_45OT.IsNullOrZero() == false) return true;
            if (Rate_40NOR.IsNullOrZero() == false) return true;
            if (Rate_40FR.IsNullOrZero() == false) return true;
            if (Rate_20OT.IsNullOrZero() == false) return true;
            if (Rate_45TK.IsNullOrZero() == false) return true;
            if (Rate_20NOR.IsNullOrZero() == false) return true;
            if (Rate_40HT.IsNullOrZero() == false) return true;
            if (Rate_40RH.IsNullOrZero() == false) return true;
            if (Rate_45RH.IsNullOrZero() == false) return true;
            if (Rate_45HQ.IsNullOrZero() == false) return true;
            if (Rate_20HT.IsNullOrZero() == false) return true;
            if (Rate_40HQ.IsNullOrZero() == false) return true;

            return false;
        }

        #endregion

        #region 询价人列表

        public List<InquirePriceInquireBys> InquirePriceInquireBysList { get; set; }

        #endregion

        /// <summary>
        /// 视图模板代码
        /// </summary>
        public string ViewCode { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string AdvanceQueryString { get; set; }

        /// <summary>
        /// 节点业务类型
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// 返回数据条数
        /// </summary>
        public int TopCount { get; set; }
    }

    #endregion

    #region 空运询价

    public class ClientInquierAirRate : ClientBaseInquireRate
    {
        string _Schedule;
        /// <summary>
        /// Schedule
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="航班时刻表",EMessage="Schedule")]
        public string Schedule
        {
            get { return _Schedule; }
            set
            {
                if (_Schedule != value)
                {
                    _Schedule = value;
                    this.NotifyPropertyChanged(o => o.Schedule);
                }
            }
        }
    
        string _Routing;
        /// <summary>
        /// Routing
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="路线",EMessage="Routing")]
        public string Routing
        {
            get { return _Routing; }
            set
            {
                if (_Routing != value)
                {
                    _Routing = value;
                    this.NotifyPropertyChanged(o => o.Routing);
                }
            }
        }

        string _CartonsOrPallets;
        /// <summary>
        /// Routing
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="纸盒或托盘",EMessage="CartonsOrPallets")]
        public string CartonsOrPallets
        {
            get { return _CartonsOrPallets; }
            set
            {
                if (_CartonsOrPallets != value)
                {
                    _CartonsOrPallets = value;
                    this.NotifyPropertyChanged(o => o.CartonsOrPallets);
                }
            }
        }

        string _MAWB;
        /// <summary>
        /// MAWB
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="主提单",EMessage="MAWB")]
        public string MAWB
        {
            get { return _MAWB; }
            set
            {
                if (_MAWB != value)
                {
                    _MAWB = value;
                    this.NotifyPropertyChanged(o => o.MAWB);
                }
            }
        }

        string _HAWB;
        /// <summary>
        /// HAWB
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="分提单",EMessage="HAWB")]
        public string HAWB
        {
            get { return _HAWB; }
            set
            {
                if (_HAWB != value)
                {
                    _HAWB = value;
                    this.NotifyPropertyChanged(o => o.HAWB);
                }
            }
        }

        #region Rates

        #region MIN
        decimal? _Rate_MIN;
        /// <summary>
        ///Rate_MIN
        /// </summary>
        public decimal? Rate_MIN
        {
            get { return _Rate_MIN; }
            set
            {
                if (_Rate_MIN != value)
                {
                    _Rate_MIN = value;
                    this.NotifyPropertyChanged(o => o.Rate_MIN);
                }
            }
        }
        #endregion

        #region +45
        decimal? _Rate_45;
        /// <summary>
        ///Rate_40RF
        /// </summary>
        public decimal? Rate_45
        {
            get { return _Rate_45; }
            set
            {
                if (_Rate_45 != value)
                {
                    _Rate_45 = value;
                    this.NotifyPropertyChanged(o => o.Rate_45);
                }
            }
        }
        #endregion

        #region +100
        decimal? _Rate_100;
        /// <summary>
        ///Rate_45HT
        /// </summary>
        public decimal? Rate_100
        {
            get { return _Rate_100; }
            set
            {
                if (_Rate_100 != value)
                {
                    _Rate_100 = value;
                    this.NotifyPropertyChanged(o => o.Rate_100);
                }
            }
        }
        #endregion

        #region +300
        decimal? _Rate_300;
        /// <summary>
        ///Rate_20RF
        /// </summary>
        public decimal? Rate_300
        {
            get { return _Rate_300; }
            set
            {
                if (_Rate_300 != value)
                {
                    _Rate_300 = value;
                    this.NotifyPropertyChanged(o => o.Rate_300);
                }
            }
        }
        #endregion

        #region +500
        decimal? _Rate_500;
        /// <summary>
        ///Rate_20HQ
        /// </summary>
        public decimal? Rate_500
        {
            get { return _Rate_500; }
            set
            {
                if (_Rate_500 != value)
                {
                    _Rate_500 = value;
                    this.NotifyPropertyChanged(o => o.Rate_500);
                }
            }
        }
        #endregion

        #region +800
        decimal? _Rate_800;
        /// <summary>
        ///Rate_20TK
        /// </summary>
        public decimal? Rate_800
        {
            get { return _Rate_800; }
            set
            {
                if (_Rate_800 != value)
                {
                    _Rate_800 = value;
                    this.NotifyPropertyChanged(o => o.Rate_800);
                }
            }
        }
        #endregion

        #region +1000
        decimal? _Rate_1000;
        /// <summary>
        ///Rate_20GP
        /// </summary>
        public decimal? Rate_1000
        {
            get { return _Rate_1000; }
            set
            {
                if (_Rate_1000 != value)
                {
                    _Rate_1000 = value;
                    this.NotifyPropertyChanged(o => o.Rate_1000);
                }
            }
        }
        #endregion

        #region +1300
        decimal? _Rate_1300;
        /// <summary>
        ///Rate_40TK
        /// </summary>
        public decimal? Rate_1300
        {
            get { return _Rate_1300; }
            set
            {
                if (_Rate_1300 != value)
                {
                    _Rate_1300 = value;
                    this.NotifyPropertyChanged(o => o.Rate_1300);
                }
            }
        }
        #endregion

        #endregion

        #region 方法

        /// <summary>
        /// 把所有箱属性置为空
        /// </summary>
        public void SetALLRateNull()
        {
            Rate_MIN = null;
            Rate_45 = null;
            Rate_100 = null;
            Rate_300 = null;
            Rate_500 = null;
            Rate_800 = null;
            Rate_1000 = null;
            Rate_1300 = null;
        }

        /// <summary>
        /// 把所有不需要用到的箱属性置为空,用到的设为0
        /// </summary>
        /// <param name="units">根据一个OceanUnitList列表生成</param>
        public void BulidRateToZeroByOceanUints(List<InquireUnit> units)
        {
            SetALLRateNull();
            foreach (var item in units)
            {
                switch (item.UnitName)
                {
                    case "MIN": Rate_MIN = 0; break;
                    case "+45": Rate_45 = 0; break;
                    case "+100": Rate_100 = 0; break;
                    case "+300": Rate_300 = 0; break;
                    case "+500": Rate_500 = 0; break;
                    case "+800": Rate_800 = 0; break;
                    case "+1000": Rate_1000 = 0; break;
                    case "+1300": Rate_1300 = 0; break;
                }
            }
        }

        /// <summary>
        /// 验证所有运费.如果所有运费都为0 返回False
        /// </summary>
        /// <returns></returns>
        public bool ValidateHasRate()
        {
            if (Rate_MIN.IsNullOrZero() == false) return true;
            if (Rate_45.IsNullOrZero() == false) return true;
            if (Rate_100.IsNullOrZero() == false) return true;
            if (Rate_300.IsNullOrZero() == false) return true;
            if (Rate_500.IsNullOrZero() == false) return true;
            if (Rate_800.IsNullOrZero() == false) return true;
            if (Rate_1000.IsNullOrZero() == false) return true;
            if (Rate_1300.IsNullOrZero() == false) return true;

            return false;
        }

        #endregion

        #region 询价人列表

        public List<InquirePriceInquireBys> InquirePriceInquireBysList { get; set; }

        #endregion
    }

    #endregion

    #region 拖车询价

    public class ClientInquierTruckingRate : ClientBaseInquireRate
    {
       decimal? _Rate;
        /// <summary>
       /// _Rate
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="价格",EMessage="Rate")]
       public decimal? Rate
        {
            get { return _Rate; }
            set
            {
                if (_Rate != value)
                {
                    _Rate = value;
                    this.NotifyPropertyChanged(o => o.Rate);
                }
            }
        }
      
        decimal? _FUEL;
        /// <summary>
        /// _FUEL
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="燃油附加费",EMessage="FUEL")]
        public decimal? FUEL
        {
            get { return _FUEL; }
            set
            {
                if (_FUEL != value)
                {
                    _FUEL = value;
                    this.NotifyPropertyChanged(o => o.FUEL);
                }
            }
        }
     
        decimal? _Total;
        /// <summary>
        /// _Total
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="拖车价格+燃油附费",EMessage="Total")]
        public decimal? Total
        {
            get { return _Total; }
            set
            {
                if (_Total != value)
                {
                    _Total = value;
                    this.NotifyPropertyChanged(o => o.Total);
                }
            }
        }

        string _ZipCode;
        /// <summary>
        /// _ZipCode
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="ZipCode",EMessage="ZipCode")]
        public string ZipCode
        {
            get { return _ZipCode; }
            set
            {
                if (_ZipCode != value)
                {
                    _ZipCode = value;
                    this.NotifyPropertyChanged(o => o.ZipCode);
                }
            }
        }

        string _CartonsOrPallets;
        /// <summary>
        /// Routing
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="纸盒或托盘",EMessage="CartonsOrPallets")]
        public string CartonsOrPallets
        {
            get { return _CartonsOrPallets; }
            set
            {
                if (_CartonsOrPallets != value)
                {
                    _CartonsOrPallets = value;
                    this.NotifyPropertyChanged(o => o.CartonsOrPallets);
                }
            }
        }

        #region 询价人列表

        public List<InquirePriceInquireBys> InquirePriceInquireBysList { get; set; }

        #endregion
    }

    #endregion

    #endregion     
}
