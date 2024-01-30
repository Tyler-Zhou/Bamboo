namespace ICP.FRM.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic;
    using ICP.Framework.CommonLibrary.Common;
    using System.Xml.Serialization;


    #region 查询海运询价返回数据

    /// <summary>
    /// 查询海运询价返回数据
    /// </summary>
    [Serializable]
    public class InquierOceanRatesResult
    {
        /// <summary>
        /// InquierPriceListsd
        /// </summary>
        public List<InquierOceanRate> InquierOceanRateList { get; set; }

        /// <summary>
        /// 最大的单位列表
        /// </summary>
        public List<InquireUnit> MaxUnits { get; set; }

        public List<UnReadDiscussingCount> UnReadCountList { get; set; }
    }

    #endregion

    #region 查询空运询价返回数据

    /// <summary>
    /// 查询空运询价返回数据
    /// </summary>
    [Serializable]
    public class InquierAirRatesResult
    {
        /// <summary>
        /// InquierPriceLists
        /// </summary>
        public List<InquierAirRate> InquierAirRateList { get; set; }

        /// <summary>
        /// 最大的单位列表
        /// </summary>
        public List<InquireUnit> MaxUnits { get; set; }

        public List<UnReadDiscussingCount> UnReadCountList { get; set; }
    }

    #endregion

    #region 查询拖车询价返回数据

    /// <summary>
    /// 查询拖车询价返回数据
    /// </summary>
    [Serializable]
    public class InquierTruckingRatesResult
    {
        /// <summary>
        /// InquierPriceLists
        /// </summary>
        public List<InquierTruckingRate> InquierTruckingRateList { get; set; }

        /// <summary>
        /// 最大的单位列表
        /// </summary>
        public List<InquireUnit> MaxUnits { get; set; }

        public List<UnReadDiscussingCount> UnReadCountList { get; set; }
    }

    #endregion

    #region 询价基础数据模型(海运询价,空运询价，拖车询价数据对象都继承它)

    /// <summary>
    /// 询价基础数据模型(海运询价,空运询价，拖车询价数据对象都继承它)
    /// </summary>
    [Serializable]
    public class BaseInquireRate : BaseDataObject
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 主记录ID(用于回复子询价)
        /// </summary>
        public Guid? MainRecordID { get; set; }

        /// <summary>
        /// 询价单号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 未确认业务
        /// </summary>
        public Boolean HasUnconfirmedShipment { get; set; }

        #region 是否有未读的消息
       
        /// <summary>
        /// 是否有未读的信息
        /// </summary>
        public bool HasUnRead { get; set; }
       
        #endregion

        private string _strUnitRateString;
        /// <summary>
        /// UnitRate String
        /// </summary>
        public string UnitRateString
        {
            get { return _strUnitRateString; }
            set { _strUnitRateString = value; }
        }
        #region 总附加费
        private string _totalSurcharge;
        /// <summary>
        /// 总附加费
        /// </summary>
        public string TotalSurcharge
        {
            get { return _totalSurcharge; }
            set { _totalSurcharge = value; }
        }
        #endregion

        #region 是否回复价格 (如：价格列20GP,40HP,40GP都没有价格，即定义为没有回复价格)--貌似在UI端没用

        /// <summary>
        /// 是否都没有回复价格 
        /// </summary>
        public bool IsNoPriceAll { get; set; }
        
        #endregion

        /////// <summary>
        /////// Discussing.To.ID
        /////// </summary>
        ////public Guid DiscussingToID { get; set; }

        /////// <summary>
        /////// Discussing.To.Name
        /////// </summary>
        ////public string DiscussingToName { get; set; }

        ///// <summary>
        ///// Type
        ///// </summary>
        //public InquierType Type { get; set; }

        /// <summary>
        /// 是否主询价(暂定当ID＝ParentID时，IsMain＝true，数据库不需要)
        /// </summary>
        public bool IsMain { get; set; }      

        ///// <summary>
        ///// State(Inquier=1,Responded =2)
        ///// </summary>
        //public InquierState State { get; set; }

        #region 询价人和回复人

        /// <summary>
        /// 建立人，即询价人，一般为业务员
        /// </summary>
        public Guid? InquireByID{ get; set; }

        /// <summary>
        /// 建立人，即询价人，一般为业务员
        /// 1.添加询价时临时存储询价人名称
        /// 2.发送商务通知时存储询价人名称
        /// </summary>
        public string InquireByName { get; set; }

        /// <summary>
        /// 回复人
        /// </summary>
        public Guid? RespondByID { get; set; }

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

    #endregion

        #region 询价人列表

        public List<InquirePriceInquireBys> InquireBysList { get; set; }

        #endregion

        #region 船东

        /// <summary>
        /// 船东ID
        /// </summary>
        public Guid? CarrierID { get; set; }

        /// <summary>
        /// 船东
        /// </summary>
        public string CarrierName { get; set; }

        #endregion

        #region 运输条款

        /// <summary>
        /// 运输条款ID
        /// </summary>
        public Guid? TransportClauseID { get; set; }

        /// <summary>
        /// 运输条款
        /// </summary>
        public string TransportClauseName { get; set; }

        #endregion

        #region Port

        /// <summary>
        /// 装货港ID
        /// </summary>
        public Guid? POLID { get; set; }

        /// <summary>
        /// 装货港
        /// </summary>
        public string POLName { get; set; }

        ///// <summary>
        ///// 中转港ID
        ///// </summary>
        //public Guid? VIAID { get; set; }

        ///// <summary>
        ///// 中转港
        ///// </summary>
        //public string VIAName { get; set; }

        /// <summary>
        /// 卸货港ID
        /// </summary>
        public Guid? PODID { get; set; }

        /// <summary>
        /// 卸货港
        /// </summary>
        public string PODName { get; set; }

        /// <summary>
        /// 交货地ID
        /// </summary>
        public Guid? PlaceOfDeliveryID { get; set; }

        /// <summary>
        /// 交货地
        /// </summary>
        public string PlaceOfDeliveryName { get; set; }

        #endregion

        #region 品名

        /// <summary>
        /// 品名
        /// </summary>
        public string Commodity { get; set; }

        #endregion

        #region 币种

        /// <summary>
        /// CurrencyID
        /// </summary>
        public Guid? CurrencyID { get; set; }

        /// <summary>
        /// CurrencyName
        /// </summary>
        public string CurrencyName { get; set; }

        #endregion      

        #region 时间从 到

        /// <summary>
        /// DurationFrom
        /// </summary>
        public DateTime? DurationFrom { get; set; }

        /// <summary>
        /// DurationTo
        /// </summary>
        public DateTime? DurationTo { get; set; }

        #endregion

        #region 箱型列表

        ///// <summary>
        ///// UnitRates
        ///// </summary>
        //public List<FrmUnitRateList> UnitRates { get; set; }
        /// <summary>
        /// OceanItemUnits
        /// </summary>
        [XmlArray("UnitRates")]
        [XmlArrayItem("UnitRate")]
        //public List<FrmUnitRateList> UnitRates { get; set; }
        public List<InquireUnit> UnitRates { get; set; }

        #endregion      

        #region 备注

        public string Remark { get; set; }

        #endregion

        #region 是否共享(标识为共享后才可以在查询过程中查到)

        /// <summary>
        /// 是否共享(标识为共享后才可以在查询过程中查到)
        /// </summary>
        public bool Shared { get; set; }

        #endregion

        #region General Info

        #region 航线

        /// <summary>
        /// 航线ID
        /// </summary>
        public Guid? ShippingLineID{ get; set; }      

        /// <summary>
        /// 航线名称
        /// </summary>
        public string ShippingLineName{ get; set; }

        #endregion

        #region 客户

        /// <summary>
        /// CustomerID
        /// </summary>
        public Guid? CustomerID { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }

        #endregion

        #region Text of Rate Unit

        public string RateUnitText { get; set; }

        #endregion

        #region 期望船东
        
        ///// <summary>
        ///// 业务员输入的CarrierID
        ///// </summary>
        //public Guid ExpCarrierID { get; set; }

        /// <summary>
        /// 业务员输入的Carrier
        /// </summary>
        public string ExpCarrierName { get; set; }

        #endregion

        #region 期望品名

        /// <summary>
        /// 业务员输入的品名
        /// </summary>
        public string ExpCommodity { get; set; }
        #endregion
        #region 期望运输条款

        /// <summary>
        /// 业务员输入的TermID
        /// </summary>
        public Guid? ExpTransportClauseID { get; set; }

        /// <summary>
        /// 业务员输入的Term
        /// </summary>
        public string ExpTransportClauseName { get; set; }
        #endregion

        #region 重量 体积
        /// <summary>
        /// 重量
        /// </summary>
        public string CargoWeight { get; set; }

        /// <summary>
        /// 体积
        /// </summary>
        public string Measurement { get; set; }

        #endregion

        #region 期望杂

        /// <summary>
        /// CargoReady
        /// </summary>
        public string CargoReady { get; set; }

        /// <summary>
        /// EstimateTimeOfDelivery
        /// </summary>
        public string EstimateTimeOfDelivery { get; set; }

        /// <summary>
        /// 是否会出booking
        /// </summary>
        public bool IsWillBooking { get; set; }

        /// <summary>
        /// 期望价格
        /// </summary>
        public string ExpPrice { get; set; }

        #endregion

        #region 新建询价时的询问内容

        /// <summary>
        /// Discussing
        /// </summary>
        public string DiscussingWhenNew { get; set; }

        #endregion

        #endregion

        #region 创建时间和行版本

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }      

        /// <summary>
        /// 行版本
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// 用户所在时区的更新时间
        /// </summary>
        public DateTimeOffset? DTime { get; set; }

        
    }

    #endregion

    #region 海运询价

    [Serializable]
    public class InquierOceanRate : BaseInquireRate
    {      
        #region 附加费

        /// <summary>
        /// SurCharge
        /// </summary>
        public string SurCharge { get; set; }

        #endregion     

        //#region 询价人列表

        //public List<InquirePriceInquireBys> InquirePriceInquireBysList { get; set; }

        //#endregion
    }

    #endregion

    #region 空运询价

    [Serializable]
    public class InquierAirRate : BaseInquireRate
    {
        public string Schedule { get; set; }
        public string Routing { get; set; }
        public string CartonsOrPallets { get; set; }
        public string MAWB { get; set; }
        public string HAWB { get; set; }

        //#region 询价人列表

        //public List<InquirePriceInquireBys> InquirePriceInquireBysList { get; set; }

        //#endregion
    }

    #endregion

    #region 拖车询价

    [Serializable]
    public class InquierTruckingRate : BaseInquireRate
    {
        public decimal? Rate { get; set; }
        public decimal? FUEL { get; set; }
        public decimal? Total { get; set; }
        public string ZipCode { get; set; }
        public string CartonsOrPallets { get; set; }

        //#region 询价人列表

        //public List<InquirePriceInquireBys> InquirePriceInquireBysList { get; set; }

        //#endregion
    }

    #endregion
    
    #region Discussing

    /// <summary>
    /// Discussing
    /// </summary>
    [Serializable]
    public class InquireDiscussing : BaseDataObject
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

        ///// <summary>
        ///// InquierPriceID
        ///// </summary>
        //public Guid InquierPriceID { get; set; }

        ///// <summary>
        ///// From
        ///// </summary>
        //public string From
        //{
        //    get
        //    {
        //        string from = string.Empty;
        //        from += CreateByName + " ";
        //        from += CreateDate.ToShortTimeString() + " " + CreateDate.ToShortDateString();
        //        return from;
        //    }
        //}

        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 显示到UI的Content
        /// </summary>
        public string BizContent
        {
            get
            {
                return "To " + DiscussingTOName + ":\n\t" + Content;
            }
        }

        /// <summary>
        /// Discussing TO
        /// </summary>
        public Guid DiscussingTOID{ get; set; }

        /// <summary>
        /// Discussing TO
        /// </summary>
        public string DiscussingTOName { get; set; }

        /// <summary>
        /// 是否读了
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Discussing.FROM
        /// </summary>
        public string DiscussingFromName { get; set; }

        /// <summary>
        /// Discussing.FROM
        /// </summary>
        public Guid DiscussingFromID { get; set; }

        /// <summary>
        /// Sent Time
        /// </summary>
        public DateTimeOffset SentTime { get; set; }

        public string BizSentTime
        {
            get
            {
                return this.SentTime.ToLocalTime().ToString(@"MM/dd HH:mm");
            }
        }
    }

    /// <summary>
    /// 箱型单位
    /// </summary>
    [Serializable]
    public class InquireUnit : BaseDataObject
    {
        /// <summary>
        /// IsNew
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
                    base.OnPropertyChanged("ID", value);
                }
            }
        }

        Guid _unitid;
        /// <summary>
        /// 运价ID
        /// </summary>
        public Guid UnitID
        {
            get
            {
                return _unitid;
            }
            set
            {
                if (_unitid != value)
                {
                    _unitid = value;
                    base.OnPropertyChanged("UnitID", value);
                }
            }
        }

        string _unitname;
        /// <summary>
        /// 运价单位名
        /// </summary>
        public string UnitName
        {
            get
            {
                return _unitname;
            }
            set
            {
                if (_unitname != value)
                {
                    _unitname = value;
                    base.OnPropertyChanged("UnitName", value);
                }
            }
        }

        decimal _rate;
        /// <summary>
        /// 价钱
        /// </summary>
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
                    base.OnPropertyChanged("Rate", value);
                }
            }
        }
    }

    [Serializable]
    public class UnReadDiscussingCount
    {
        public InquierType Type { get; set; }
        public int CountOfUnreply { get; set; }
    }

    #endregion

    #region 询价询问人

    [Serializable]
    public class InquirePriceInquireBys : BaseDataObject
    {
        /// <summary>
        /// IsNew
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
                    base.OnPropertyChanged("ID", value);
                }
            }
        }

        Guid? _inquirePriceID;
        /// <summary>
        /// 询价ID
        /// </summary>
        public Guid? InquirePriceID
        {
            get
            {
                return _inquirePriceID;
            }
            set
            {
                if (_inquirePriceID != value)
                {
                    _inquirePriceID = value;
                    base.OnPropertyChanged("InquirePriceID", value);
                }
            }
        }

        Guid _inquireByID;
        /// <summary>
        /// 询价询问人ID
        /// </summary>
        public Guid InquireByID
        {
            get
            {
                return _inquireByID;
            }
            set
            {
                if (_inquireByID != value)
                {
                    _inquireByID = value;
                    base.OnPropertyChanged("InquireByID", value);
                }
            }
        }

        string _inquireBy;
        /// <summary>
        /// 询价询问人
        /// </summary>
        public string InquireBy
        {
            get
            {
                return _inquireBy;
            }
            set
            {
                if (_inquireBy != value)
                {
                    _inquireBy = value;
                    base.OnPropertyChanged("InquireBy", value);
                }
            }
        }

        string _inquireByEName;
        /// <summary>
        /// 询价询问人英文名
        /// </summary>
        public string InquireByEName
        {
            get
            {
                return _inquireByEName;
            }
            set
            {
                if (_inquireByEName != value)
                {
                    _inquireByEName = value;
                    base.OnPropertyChanged("InquireByEName", value);
                }
            }
        }

        string _inquireByCName;
        /// <summary>
        /// 询价询问人中文名
        /// </summary>
        public string InquireByCName
        {
            get
            {
                return _inquireByCName;
            }
            set
            {
                if (_inquireByCName != value)
                {
                    _inquireByCName = value;
                    base.OnPropertyChanged("InquireByCName", value);
                }
            }
        }

        DateTime _inquireDate;
        /// <summary>
        /// 询价日期
        /// </summary>
        public DateTime InquireDate
        {
            get
            {
                return _inquireDate;
            }
            set
            {
                if (_inquireDate != value)
                {
                    _inquireDate = value;
                    base.OnPropertyChanged("InquireDate", value);
                }
            }
        }

        Boolean _Handled;
        /// <summary>
        /// 询价处理状态
        /// </summary>
        public Boolean Handled
        {
            get { return _Handled; }
            set { _Handled = value; }
        }
    }

    #endregion
}
