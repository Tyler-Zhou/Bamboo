using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using System.Runtime.Serialization;

namespace ICP.FCM.AirImport.ServiceInterface
{

    #region 业务列表

    /// <summary>
    /// 业务列表的所有字段
    /// 作者：罗丹
    /// 创建时间：2011-12-14 
    /// </summary>
    [Serializable]
    public partial class AirBusinessList : BaseDataObject
    {
        #region ID
        Guid _id;
        /// <summary>
        /// ID
        /// </summary>
        [Required(CMessage = "ID", EMessage = "ID")]
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

        #region 操作口岸
        Guid _companyid;
        /// <summary>
        /// 操作口岸ID
        /// </summary>
        [Required(CMessage = "操作口岸", EMessage = "Company")]
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

        string _companyname;
        /// <summary>
        /// 操作口岸名称
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
                    base.OnPropertyChanged("CompanyName", value);
                }
            }
        }

        #endregion

        #region 业务号
        string _no;
        /// <summary>
        /// 编号
        /// </summary>
        public string No
        {
            get
            {
                return _no;
            }
            set
            {
                if (_no != value)
                {
                    _no = value;
                    base.OnPropertyChanged("No", value);
                }
            }
        }

        #endregion

        #region 状态
        AIOrderState _state;
        /// <summary>
        /// 状态
        /// </summary>
        public AIOrderState State
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

        string stateDescription;
        /// <summary>
        /// 状态描述
        /// </summary>
        public string StateDescription
        {
            get
            {
                if (string.IsNullOrEmpty(stateDescription))
                {
                    try
                    {
                        stateDescription = EnumHelper.GetDescription<AIOrderState>(State, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                    }
                    catch
                    {
                        stateDescription = string.Empty;
                    }
                }
                return stateDescription;
            }
            set
            {
                stateDescription = value;
            }
        }

        #endregion

        #region MBLID
        Guid? mblID;
        /// <summary>
        /// MBLID
        /// </summary>
        public Guid? MBLID
        {
            get
            {
                return mblID;
            }
            set
            {
                if (mblID != value)
                {
                    mblID = value;
                    base.OnPropertyChanged("MBLID", value);
                }
            }
        }


        #endregion

        #region 主提单号
        string _mblno;
        /// <summary>
        /// MBLNo
        /// </summary>
        public string MBLNo
        {
            get
            {
                return _mblno;
            }
            set
            {
                if (_mblno != value)
                {
                    _mblno = value;
                    base.OnPropertyChanged("MBLNo", value);
                }
            }
        }

        #endregion

        #region 分提单号
        string _subno;
        /// <summary>
        /// SubNo
        /// </summary>
        public string SubNo
        {
            get
            {
                return _subno;
            }
            set
            {
                if (_subno != value)
                {
                    _subno = value;
                    base.OnPropertyChanged("SubNo", value);
                }
            }
        }

        #endregion

        #region 代理
        Guid? _agentid;
        /// <summary>
        /// 代理ID
        /// </summary>
        //[GuidRequired(ErrorMessage = "代理不能为空")]
        public Guid? AgentID
        {
            get
            {
                return _agentid;
            }
            set
            {
                if (_agentid != value)
                {
                    _agentid = value;
                    base.OnPropertyChanged("AgentID", value);
                }
            }
        }

        string _agentname;
        /// <summary>
        /// 代理名称
        /// </summary>
        public string AgentName
        {
            get
            {
                return _agentname;
            }
            set
            {
                if (_agentname != value)
                {
                    _agentname = value;
                    base.OnPropertyChanged("AgentName", value);
                }
            }
        }

        #endregion

        #region 客户

        Guid _customerid;
        /// <summary>
        /// 客户ID
        /// </summary>
        [GuidRequired(CMessage = "客户", EMessage = "Customer")]
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
                    base.OnPropertyChanged("CustomerName", value);
                }
            }
        }

        #endregion

        #region 是否已付款
        bool? _paid;
        /// <summary>
        /// 是否已付款
        /// </summary>
        public bool? Paid
        {
            get
            {
                return _paid;
            }
            set
            {
                if (_paid != value)
                {
                    _paid = value;
                    base.OnPropertyChanged("Paid", value);
                }
            }
        }

        #endregion

        #region 是否电放

        bool _istelex;
        /// <summary>
        /// 是否电放
        /// </summary>
        public bool IsTelex
        {
            get
            {
                return _istelex;
            }
            set
            {
                if (_istelex != value)
                {
                    _istelex = value;
                    base.OnPropertyChanged("IsTelex", value);
                }
            }
        }

        #endregion

        #region 是否已收到正本提单
        bool? _oblrcved;
        /// <summary>
        /// 是否已收到正本提单
        /// </summary>
        public bool? OBLRcved
        {
            get
            {
                return _oblrcved;
            }
            set
            {
                if (_oblrcved != value)
                {
                    _oblrcved = value;
                    base.OnPropertyChanged("OBLRcved", value);
                }
            }
        }

        #endregion

        #region 航班号

        string _flightNo;
        /// <summary>
        /// 航班号
        /// </summary>
        public string FlightNo
        {
            get
            {
                return _flightNo;
            }
            set
            {
                if (_flightNo != value)
                {
                    _flightNo = value;
                    base.OnPropertyChanged("FlightNo", value);
                }
            }
        }

        #endregion

        #region 装货港

        Guid _polid;
        /// <summary>
        /// 装货港ID
        /// </summary>
        [GuidRequired(CMessage = "装货港", EMessage = "POL")]
        public Guid POLID
        {
            get
            {
                return _polid;
            }
            set
            {
                if (_polid != value)
                {
                    _polid = value;
                    base.OnPropertyChanged("POLID", value);
                }
            }
        }

        string _polname;
        /// <summary>
        /// 装货港名称
        /// </summary>
        public string POLName
        {
            get
            {
                return _polname;
            }
            set
            {
                if (_polname != value)
                {
                    _polname = value;
                    base.OnPropertyChanged("POLName", value);
                }
            }
        }

        #endregion

        #region 卸货港
        Guid _podid;
        /// <summary>
        /// 卸货港ID
        /// </summary>
        [GuidRequired(CMessage = "卸货港", EMessage = "POD")]
        public Guid PODID
        {
            get
            {
                return _podid;
            }
            set
            {
                if (_podid != value)
                {
                    _podid = value;
                    base.OnPropertyChanged("PODID", value);
                }
            }
        }

        string _podname;
        /// <summary>
        /// 卸货港名称
        /// </summary>
        public string PODName
        {
            get
            {
                return _podname;
            }
            set
            {
                if (_podname != value)
                {
                    _podname = value;
                    base.OnPropertyChanged("PODName", value);
                }
            }
        }

        #endregion

        #region 交货地
        Guid _placeofdeliveryid;
        /// <summary>
        /// 交货地ID
        /// </summary>
        [GuidRequired(CMessage = "交货地", EMessage = "PlaceOfDelivery")]
        public Guid PlaceOfDeliveryID
        {
            get
            {
                return _placeofdeliveryid;
            }
            set
            {
                if (_placeofdeliveryid != value)
                {
                    _placeofdeliveryid = value;
                    base.OnPropertyChanged("PlaceOfDeliveryID", value);
                }
            }
        }

        string _placeofdeliveryname;
        /// <summary>
        /// 交货地名称
        /// </summary>
        public string PlaceOfDeliveryName
        {
            get
            {
                return _placeofdeliveryname;
            }
            set
            {
                if (_placeofdeliveryname != value)
                {
                    _placeofdeliveryname = value;
                    base.OnPropertyChanged("PlaceOfDeliveryName", value);
                }
            }
        }

        #endregion

        #region DETA
        public DateTime? dETA;
        /// <summary>
        /// D.ETA
        /// </summary>
        public DateTime? DETA
        {
            get
            {
                return dETA;
            }
            set
            {
                if (dETA != value)
                {
                    dETA = value;
                    base.OnPropertyChanged("DETA", value);
                }
            }
        }

        #endregion

        #region 放单类型
        FCMReleaseType _releasetype;
        /// <summary>
        /// 放单类型
        /// </summary>
        public FCMReleaseType ReleaseType
        {
            get
            {
                return _releasetype;
            }
            set
            {
                if (_releasetype != value)
                {
                    _releasetype = value;
                    base.OnPropertyChanged("ReleaseType", value);
                }
            }
        }

        /// <summary>
        /// 放货类型描述
        /// </summary>
        public string ReleaseTypeDescription
        {
            get
            {
                try
                {
                    return EnumHelper.GetDescription<FCMReleaseType>(ReleaseType, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        #endregion

        #region 文件
        private Guid? filerId;
        /// <summary>
        /// 文件ID
        /// </summary>
        [GuidRequired(CMessage = "文件", EMessage = "Filer")]
        public Guid? FilerId
        {
            get
            {
                return filerId;
            }
            set
            {
                if (filerId != value)
                {
                    filerId = value;
                    base.OnPropertyChanged("FilerId", value);
                }
            }
        }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FilerName
        {
            get;
            set;
        }
        #endregion

        #region 客服
        public Guid? customerService;
        /// <summary>
        /// 客服ID
        /// </summary>
        public Guid? CustomerService
        {
            get
            {
                return customerService;
            }
            set
            {
                if (customerService != value)
                {
                    customerService = value;
                    base.OnPropertyChanged("CustomerService", value);
                }
            }
        }

        /// <summary>
        /// 客服名称
        /// </summary>
        public string CustomerServiceName
        {
            get;
            set;
        }
        #endregion

        #region 港前客服

        Guid? polFilerID;
        /// <summary>
        /// 港前客服
        /// </summary>
        public Guid? POLFilerID
        {
            get
            {
                return polFilerID;
            }
            set
            {
                if (polFilerID != value)
                {
                    polFilerID = value;
                    base.OnPropertyChanged("POLFilerID", value);
                }
            }
        }

        string polFilerName;
        /// <summary>
        /// 港前客服
        /// </summary>
        public string POLFilerName
        {
            get
            {
                return polFilerName;
            }
            set
            {
                if (polFilerName != value)
                {
                    polFilerName = value;
                    base.OnPropertyChanged("POLFilerName", value);
                }
            }
        }

        #endregion

        #region 转关单号
        string _itno;
        /// <summary>
        /// ITNo
        /// </summary>
        public string ITNO
        {
            get
            {
                return _itno;
            }
            set
            {
                if (_itno != value)
                {
                    _itno = value;
                    base.OnPropertyChanged("ITNO", value);
                }
            }
        }

        #endregion

        #region 发货人
        Guid? _shipperid;
        /// <summary>
        /// 发货人
        /// </summary>
        public Guid? ShipperID
        {
            get
            {
                return _shipperid;
            }
            set
            {
                if (_shipperid != value)
                {
                    _shipperid = value;
                    base.OnPropertyChanged("ShipperID", value);
                }
            }
        }

        string _shippername;
        /// <summary>
        /// 发货人名称
        /// </summary>
        public string ShipperName
        {
            get
            {
                return _shippername;
            }
            set
            {
                if (_shippername != value)
                {
                    _shippername = value;
                    base.OnPropertyChanged("ShipperName", value);
                }
            }
        }

        #endregion

        #region 收货人
        Guid? _consigneeid;
        /// <summary>
        /// 收货人ID
        /// </summary>
        [GuidRequired(CMessage = "收货人", EMessage = "Consignee")]
        public Guid? ConsigneeID
        {
            get
            {
                return _consigneeid;
            }
            set
            {
                if (_consigneeid != value)
                {
                    _consigneeid = value;
                    base.OnPropertyChanged("ConsigneeID", value);
                }
            }
        }

        string _consigneename;
        /// <summary>
        /// 收货人名称
        /// </summary>
        public string ConsigneeName
        {
            get
            {
                return _consigneename;
            }
            set
            {
                if (_consigneename != value)
                {
                    _consigneename = value;
                    base.OnPropertyChanged("ConsigneeName", value);
                }
            }
        }

        #endregion

        #region 数量、重量、体积
        int _quantity;
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    base.OnPropertyChanged("Quantity", value);
                }
            }
        }

        Guid _quantityunitid;
        /// <summary>
        /// 数量单位ID
        /// </summary>
        public Guid QuantityUnitID
        {
            get
            {
                return _quantityunitid;
            }
            set
            {
                if (_quantityunitid != value)
                {
                    _quantityunitid = value;
                    base.OnPropertyChanged("QuantityUnitID", value);
                }
            }
        }

        string _quantityunitname;
        /// <summary>
        ///  数量单位名称
        /// </summary>
        public string QuantityUnitName
        {
            get
            {
                return _quantityunitname;
            }
            set
            {
                if (_quantityunitname != value)
                {
                    _quantityunitname = value;
                    base.OnPropertyChanged("QuantityUnitName", value);
                }
            }
        }

        /// <summary>
        /// 数量+数量单位
        /// </summary>
        public string QuantityUnit
        {
            get
            {
                return _quantity.ToString() + " " + QuantityUnitName;
            }
        }

        #endregion

        #region 重量

        decimal _weight;
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                    base.OnPropertyChanged("Weight", value);
                }
            }
        }

        Guid _weightunitid;
        /// <summary>
        /// 重量单位ID
        /// </summary>
        public Guid WeightUnitID
        {
            get
            {
                return _weightunitid;
            }
            set
            {
                if (_weightunitid != value)
                {
                    _weightunitid = value;
                    base.OnPropertyChanged("WeightUnitID", value);
                }
            }
        }

        string _weightunitname;
        /// <summary>
        /// 重量单位名称
        /// </summary>
        public string WeightUnitName
        {
            get
            {
                return _weightunitname;
            }
            set
            {
                _weightunitname = value;
                base.OnPropertyChanged("WeightUnitName", value);
            }
        }

        /// <summary>
        /// 重量+重量单位 
        /// </summary>
        public string WeightUnit
        {
            get
            {
                return _weight.ToString() + " " + WeightUnitName;
            }
        }

        #endregion

        #region 体积
        decimal _measurement;
        /// <summary>
        /// 体积
        /// </summary>
        public decimal Measurement
        {
            get
            {
                return _measurement;
            }
            set
            {
                if (_measurement != value)
                {
                    _measurement = value;
                    base.OnPropertyChanged("Measurement", value);
                }
            }
        }

        Guid _measurementunitid;
        /// <summary>
        /// 体积单位ID
        /// </summary>
        public Guid MeasurementUnitID
        {
            get
            {
                return _measurementunitid;
            }
            set
            {
                if (_measurementunitid != value)
                {
                    _measurementunitid = value;
                    base.OnPropertyChanged("MeasurementUnitID", value);
                }
            }
        }

        string _measurementunitname;
        /// <summary>
        /// 体积单位名称
        /// </summary>
        public string MeasurementUnitName
        {
            get
            {
                return _measurementunitname;
            }
            set
            {
                if (_measurementunitname != value)
                {
                    _measurementunitname = value;
                    base.OnPropertyChanged("MeasurementUnitName", value);
                }
            }
        }
        /// <summary>
        /// 体积+体积单位
        /// </summary>
        public string MeasurementUnit
        {
            get
            {
                return _measurement.ToString() + " " + MeasurementUnitName;
            }
        }

        #endregion

        #region 收单日期
        DateTime? _getbookingdate;
        /// <summary>
        /// 收单日期
        /// </summary>
        public DateTime? GetBookingDate
        {
            get
            {
                return _getbookingdate;
            }
            set
            {
                if (_getbookingdate != value)
                {
                    _getbookingdate = value;
                    base.OnPropertyChanged("GetBookingDate", value);
                }
            }
        }

        #endregion

        #region 揽货人
        Guid? _salesid;
        /// <summary>
        /// 揽货人ID
        /// </summary>
        public Guid? SalesID
        {
            get
            {
                return _salesid;
            }
            set
            {
                if (_salesid != value)
                {
                    _salesid = value;
                    base.OnPropertyChanged("SalesID", value);
                }
            }
        }

        string _salesname;
        /// <summary>
        /// 揽货人名称
        /// </summary>
        public string SalesName
        {
            get
            {
                return _salesname;
            }
            set
            {
                if (_salesname != value)
                {
                    _salesname = value;
                    base.OnPropertyChanged("SalesName", value);
                }
            }
        }

        #endregion

        #region 客户参考单号
        string _customerno;
        /// <summary>
        /// 客户参考单号
        /// </summary>
        public string CustomerNo
        {
            get
            {
                return _customerno;
            }
            set
            {
                if (_customerno != value)
                {
                    _customerno = value;
                    base.OnPropertyChanged("CustomerNo", value);
                }
            }
        }

        #endregion

        #region ETA、ETD
        DateTime? _etd;
        /// <summary>
        /// ETD
        /// </summary>
        public DateTime? ETD
        {
            get
            {
                return _etd;
            }
            set
            {
                if (_etd != value)
                {
                    _etd = value;
                    base.OnPropertyChanged("ETD", value);
                }
            }
        }

        DateTime? _eta;
        /// <summary>
        /// ETA
        /// </summary>
        public DateTime? ETA
        {
            get
            {
                return _eta;
            }
            set
            {
                if (_eta != value)
                {
                    _eta = value;
                    base.OnPropertyChanged("ETA", value);
                }
            }
        }

        #endregion

        #region 创建日期、放货日期
        DateTime _createdate;
        /// <summary>
        /// 创建日期
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

        DateTime? _releasedate;
        /// <summary>
        /// 放货日期
        /// </summary>
        public DateTime? ReleaseDate
        {
            get
            {
                return _releasedate;
            }
            set
            {
                if (_releasedate != value)
                {
                    _releasedate = value;
                    base.OnPropertyChanged("ReleaseDate", value);
                }
            }
        }

        #endregion

        #region 到港通知书

        bool? _isSentAN;

        /// <summary>
        /// 是否已经发送到港通知书
        /// </summary>
        public bool? IsSentAN
        {
            get
            {
                return _isSentAN;
            }
            set
            {
                if (_isSentAN != value)
                {
                    _isSentAN = value;
                    base.OnPropertyChanged("IsSentAN", value);
                }
            }
        }

        DateTime? _sentANDate;
        /// <summary>
        /// 发送到港通知书的时间
        /// </summary>
        public DateTime? SentANDate
        {
            get
            {
                return _sentANDate;
            }
            set
            {
                if (_sentANDate != value)
                {
                    _sentANDate = value;
                    base.OnPropertyChanged("SentANDate", value);
                }
            }
        }

        #endregion

        #region 是否有效

        private bool _isValid;
        /// <summary>
        /// 是否有效
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
                    base.OnPropertyChanged("IsValid", value);
                }
            }
        }
        #endregion

        #region 更新人

        string _updateByName;
        public string UpdateByName
        {
            get
            {
                return _updateByName;
            }

            set
            {
                if (_updateByName != value)
                {
                    _updateByName = value;
                    base.OnPropertyChanged("UpdateByName", value);
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
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        #endregion
    }

    #endregion

    #region 业务编辑界面
    /// <summary>
    /// 业务编辑界面用的所有字段
    /// 作者:周任平
    /// 时间:2011-05-17 12:00
    /// </summary>
    [Serializable]
    [KnownType(typeof(DryCargo))]
    [KnownType(typeof(ReeferCargo))]
    [KnownType(typeof(DangerousCargo))]
    [KnownType(typeof(AwkwardCargo))]
    public partial class AirBusinessInfo : AirBusinessList
    {
        #region HBLID
        Guid _hblid;
        /// <summary>
        /// HBLID
        /// </summary>
        public Guid HBLID
        {
            get
            {
                return _hblid;
            }
            set
            {
                if (_hblid != value)
                {
                    _hblid = value;
                    base.OnPropertyChanged("HBLID", value);
                }
            }
        }
        #endregion

        #region 贸易条款
        Guid _tradetermid;
        /// <summary>
        /// 贸易条款ID
        /// </summary>
        [Required(CMessage = "贸易条款", EMessage = "TradeTerm")]
        public Guid TradeTermID
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
        /// 贸易条款名称
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

        #endregion

        

        #region 揽货类型
        Guid _salestypeid;
        /// <summary>
        /// 揽货类型ID
        /// </summary>
        [Required(CMessage = "揽货类型", EMessage = "SalesType")]
        public Guid SalesTypeID
        {
            get
            {
                return _salestypeid;
            }
            set
            {
                if (_salestypeid != value)
                {
                    _salestypeid = value;
                    base.OnPropertyChanged("SalesTypeID", value);
                }
            }
        }

        string _salestypename;
        /// <summary>
        /// 揽货类型名称
        /// </summary>
        public string SalesTypeName
        {
            get
            {
                return _salestypename;
            }
            set
            {
                if (_salestypename != value)
                {
                    _salestypename = value;
                    base.OnPropertyChanged("SalesTypeName", value);
                }
            }
        }
        #endregion

        #region 揽货人部门

        Guid _salesdepartmentid;
        /// <summary>
        /// 揽货人部门ID
        /// </summary>
        public Guid SalesDepartmentID
        {
            get
            {
                return _salesdepartmentid;
            }
            set
            {
                if (_salesdepartmentid != value)
                {
                    _salesdepartmentid = value;
                    base.OnPropertyChanged("SalesDepartmentID", value);
                }
            }
        }


        string _salesdepartmentname;
        /// <summary>
        /// 揽货人部门名称
        /// </summary>
        public string SalesDepartmentName
        {
            get
            {
                return _salesdepartmentname;
            }
            set
            {
                if (_salesdepartmentname != value)
                {
                    _salesdepartmentname = value;
                    base.OnPropertyChanged("SalesDepartmentName", value);
                }
            }
        }

        #endregion

        #region 委托方式

        FCMBookingMode _bookingmode;

        /// <summary>
        /// 委托方式/订舱方式
        /// </summary>
        [Required(CMessage = "委托方式", EMessage = "BookingMode")]
        public FCMBookingMode BookingMode
        {
            get
            {
                return _bookingmode;
            }
            set
            {
                if (_bookingmode != value)
                {
                    _bookingmode = value;
                    base.OnPropertyChanged("BookingMode", value);
                }
            }
        }

        #endregion

        #region 委托日期

        DateTime _bookingdate;

        /// <summary>
        /// 委托日期
        /// </summary>
        [Required(CMessage = "委托日期", EMessage = "BookingDate")]
        public DateTime BookingDate
        {
            get
            {
                return _bookingdate;
            }
            set
            {
                if (_bookingdate != value)
                {
                    _bookingdate = value;
                    base.OnPropertyChanged("BookingDate", value);
                }
            }
        }

        #endregion

        #region 付款方式
        Guid? _paymenttermid;
        /// <summary>
        /// 付款方式ID
        /// </summary>
        public Guid? PaymentTermID
        {
            get
            {
                return _paymenttermid;
            }
            set
            {
                if (_paymenttermid != value)
                {
                    _paymenttermid = value;
                    base.OnPropertyChanged("PaymentTermID", value);
                }
            }
        }

        string _paymenttermname;
        /// <summary>
        /// 付款方式名称
        /// </summary>
        public string PaymentTermName
        {
            get
            {
                return _paymenttermname;
            }
            set
            {
                if (_paymenttermname != value)
                {
                    _paymenttermname = value;
                    base.OnPropertyChanged("PaymentTermName", value);
                }
            }
        }
        #endregion

        #region 运输条款
        Guid _transportclauseid;
        /// <summary>
        /// 运输条款ID
        /// </summary>
        [GuidRequired(CMessage = "运输条款(基本信息)", EMessage = "TransportClause")]
        public Guid TransportClauseID
        {
            get
            {
                return _transportclauseid;
            }
            set
            {
                if (_transportclauseid != value)
                {
                    _transportclauseid = value;
                    base.OnPropertyChanged("TransportClauseID", value);
                }
            }
        }


        string _transportclausename;
        /// <summary>
        /// 运输条款名称
        /// </summary>
        public string TransportClauseName
        {
            get
            {
                return _transportclausename;
            }
            set
            {
                if (_transportclausename != value)
                {
                    _transportclausename = value;
                    base.OnPropertyChanged("TransportClauseName", value);
                }
            }
        }

        #endregion

        #region 代理号
        string _agentno;
        /// <summary>
        /// 代理参考号
        /// </summary>
        public string AgentNo
        {
            get
            {
                return _agentno;
            }
            set
            {
                if (_agentno != value)
                {
                    _agentno = value;
                    base.OnPropertyChanged("AgentNo", value);
                }
            }
        }

        #endregion

        #region 发货人描述
        CustomerDescription _shipperdescription;
        /// <summary>
        /// 发货人描述
        /// </summary>
        public CustomerDescription ShipperDescription
        {
            get
            {
                return _shipperdescription;
            }
            set
            {
                if (_shipperdescription != value)
                {
                    _shipperdescription = value;
                    base.OnPropertyChanged("ShipperDescription", value);
                }
            }
        }

        #endregion

        #region 收货人
        CustomerDescription _consigneedescription;
        /// <summary>
        /// 收货人描述
        /// </summary>
        public CustomerDescription ConsigneeDescription
        {
            get
            {
                return _consigneedescription;
            }
            set
            {
                if (_consigneedescription != value)
                {
                    _consigneedescription = value;
                    base.OnPropertyChanged("ConsigneeDescription", value);
                }
            }
        }
        #endregion

        #region 通知人
        Guid? _notifypartyid;
        /// <summary>
        ///通知人ID
        /// </summary>
        public Guid? NotifyPartyID
        {
            get
            {
                return _notifypartyid;
            }
            set
            {
                if (_notifypartyid != value)
                {
                    _notifypartyid = value;
                    base.OnPropertyChanged("NotifyPartyID", value);
                }
            }
        }

        string _notifypartyname;
        /// <summary>
        /// 通知人名称
        /// </summary>
        public string NotifyPartyName
        {
            get
            {
                return _notifypartyname;
            }
            set
            {
                if (_notifypartyname != value)
                {
                    _notifypartyname = value;
                    base.OnPropertyChanged("NotifyPartyName", value);
                }
            }
        }

        CustomerDescription _notifypartydescription;
        /// <summary>
        /// 通知人描述
        /// </summary>
        public CustomerDescription NotifyPartyDescription
        {
            get
            {
                return _notifypartydescription;
            }
            set
            {
                if (_notifypartydescription != value)
                {
                    _notifypartydescription = value;
                    base.OnPropertyChanged("NotifyPartyDescription", value);
                }
            }
        }

        #endregion

        #region 客户描述
        CustomerDescription _customerdescription;
        /// <summary>
        /// 客户描述
        /// </summary>
        public CustomerDescription CustomerDescription
        {
            get
            {
                return _customerdescription;
            }
            set
            {
                if (_customerdescription != value)
                {
                    _customerdescription = value;
                    base.OnPropertyChanged("CustomerDescription", value);
                }
            }
        }

        #endregion

        #region 代理描述
        CustomerDescription _agentdescription;
        /// <summary>
        /// 代理描述
        /// </summary>
        public CustomerDescription AgentDescription
        {
            get
            {
                return _agentdescription;
            }
            set
            {
                if (_agentdescription != value)
                {
                    _agentdescription = value;
                    base.OnPropertyChanged("AgentDescription", value);
                }
            }
        }
        #endregion

        #region 取文件

        DateTime? _DOCPickupDate;
        /// <summary>
        /// 取文件时间
        /// </summary>
        public DateTime? DOCPickupDate
        {
            get
            {
                return _DOCPickupDate;
            }
            set
            {
                if (_DOCPickupDate != value)
                {
                    _DOCPickupDate = value;
                    base.OnPropertyChanged("DOCPickupDate", value);
                }
            }
        }

        string _dOCPickupBy;
        /// <summary>
        /// 取文件By
        /// </summary>
        public string DOCPickupBy
        {
            get
            {
                return _dOCPickupBy;
            }
            set
            {
                if (_dOCPickupBy != value)
                {
                    _dOCPickupBy = value;
                    base.OnPropertyChanged("DOCPickupBy", value);
                }
            }
        }

        #endregion

        #region 堆存开始时间 And 监管仓时间

        DateTime? _storageStartDate;
        /// <summary>
        /// 堆存开始时间(偶尔客人不付钱，要计录货物堆存的天数)
        /// </summary>
        public DateTime? StorageStartDate
        {
            get
            {
                return _storageStartDate;
            }
            set
            {
                if (_storageStartDate != value)
                {
                    _storageStartDate = value;
                    base.OnPropertyChanged("StorageStartDate", value);
                }
            }
        }

        DateTime? _warehouseArrivedON;
        /// <summary>
        /// 监管仓时间(空运货的飞机到了后，货物要先下到他们的仓库)
        /// </summary>
        public DateTime? WarehouseArrivedON
        {
            get
            {
                return _warehouseArrivedON;
            }
            set
            {
                if (_warehouseArrivedON != value)
                {
                    _warehouseArrivedON = value;
                    base.OnPropertyChanged("WarehouseArrivedON", value);
                }
            }
        }

        #endregion

        #region 品名

        string _commodity;
        /// <summary>
        /// 品名
        /// </summary>
        public string Commodity
        {
            get
            {
                return _commodity;
            }
            set
            {
                if (_commodity != value)
                {
                    _commodity = value;
                    base.OnPropertyChanged("Commodity", value);
                }
            }
        }

        #endregion

        #region 货物类型

        CargoType? cargoType;

        /// <summary>
        /// 货物类型
        /// </summary>       
        public CargoType? CargoType
        {
            get
            {
                if (this.CargoDescription != null)
                {
                    return (CargoType)Enum.Parse(typeof(CargoType), this.CargoDescription.Type.Replace("Cargo", string.Empty));
                }

                return null;
            }
        }

        #endregion

        #region 货物描述
        CargoDescription cargoDescription;
        /// <summary>
        /// 货物描述
        /// </summary>
        public CargoDescription CargoDescription
        {
            get
            {
                return cargoDescription;
            }
            set
            {
                if (cargoDescription != value)
                {
                    cargoDescription = value;
                    base.OnPropertyChanged("CargoDescription", value);
                }
            }
        }

        #endregion

        #region 仓储
        bool _iswarehouse;
        /// <summary>
        /// 是否仓库
        /// </summary>
        public bool IsWareHouse
        {
            get
            {
                return _iswarehouse;
            }
            set
            {
                if (_iswarehouse != value)
                {
                    _iswarehouse = value;
                    base.OnPropertyChanged("IsWareHouse", value);
                }
            }
        }

        #endregion

        #region 拖车
        bool _istruck;
        /// <summary>
        /// 是否拖车
        /// </summary>
        public bool IsTruck
        {
            get
            {
                return _istruck;
            }
            set
            {
                if (_istruck != value)
                {
                    _istruck = value;
                    base.OnPropertyChanged("IsTruck", value);
                }
            }
        }

        #endregion

        #region 报关
        bool _iscustoms;
        /// <summary>
        /// 是否报关
        /// </summary>
        public bool IsCustoms
        {
            get
            {
                return _iscustoms;
            }
            set
            {
                if (_iscustoms != value)
                {
                    _iscustoms = value;
                    base.OnPropertyChanged("IsCustoms", value);
                }
            }
        }

        #endregion

        #region 商检
        bool _iscommodityinspection;
        /// <summary>
        /// 是否需要商检
        /// </summary>
        public bool IsCommodityInspection
        {
            get
            {
                return _iscommodityinspection;
            }
            set
            {
                if (_iscommodityinspection != value)
                {
                    _iscommodityinspection = value;
                    base.OnPropertyChanged("IsCommodityInspection", value);
                }
            }
        }

        #endregion

        #region 质检
        bool _isquarantineinspection;
        /// <summary>
        /// 是否需要质检
        /// </summary>
        public bool IsQuarantineInspection
        {
            get
            {
                return _isquarantineinspection;
            }
            set
            {
                if (_isquarantineinspection != value)
                {
                    _isquarantineinspection = value;
                    base.OnPropertyChanged("IsQuarantineInspection", value);
                }
            }
        }
        #endregion

        #region 放货通知书

        bool _isreleasenotify;
        /// <summary>
        /// 是否需要放货通知书
        /// </summary>
        public bool IsReleaseNotify
        {
            get
            {
                return _isreleasenotify;
            }
            set
            {
                if (_isreleasenotify != value)
                {
                    _isreleasenotify = value;
                    base.OnPropertyChanged("IsReleaseNotify", value);
                }
            }
        }

        #endregion

        #region 转运
        bool _istransport;
        /// <summary>
        /// 是否转运
        /// </summary>
        public bool IsTransport
        {
            get
            {
                return _istransport;
            }
            set
            {
                if (_istransport != value)
                {
                    _istransport = value;
                    base.OnPropertyChanged("IsTransport", value);
                }
            }
        }

        #endregion

        #region 仓库信息
        Guid? _warehouseid;
        /// <summary>
        /// 仓库ID
        /// </summary>
        public Guid? WareHouseID
        {
            get
            {
                return _warehouseid;
            }
            set
            {
                if (_warehouseid != value)
                {
                    _warehouseid = value;
                    base.OnPropertyChanged("WareHouseID", value);
                }
            }
        }

        string _warehousename;
        /// <summary>
        /// 仓库名称
        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "仓库名称", EMessage = "WareHouseName")]
        public string WareHouseName
        {
            get
            {
                return _warehousename;
            }
            set
            {
                if (_warehousename != value)
                {
                    _warehousename = value;
                    base.OnPropertyChanged("WareHouseName", value);
                }
            }
        }

        DateTime? _wareHouseDate;
        /// <summary>
        /// 仓储时间
        /// </summary>
        public DateTime? WareHouseDate
        {
            get
            {
                return _wareHouseDate;
            }
            set
            {
                if (_wareHouseDate != value)
                {
                    _wareHouseDate = value;
                    base.OnPropertyChanged("WareHouseDate", value);
                }
            }
        }

        CustomerDescription _warehousedescription;
        /// <summary>
        /// 仓库描述
        /// </summary>
        public CustomerDescription WareHouseDescription
        {
            get
            {
                return _warehousedescription;
            }
            set
            {
                if (_warehousedescription != value)
                {
                    _warehousedescription = value;
                    base.OnPropertyChanged("WareHouseDescription", value);
                }
            }
        }

        #endregion

        #region 报关信息
        Guid? _customsid;
        /// <summary>
        /// 报关行ID
        /// </summary>
        public Guid? CustomsID
        {
            get
            {
                return _customsid;
            }
            set
            {
                if (_customsid != value)
                {
                    _customsid = value;
                    base.OnPropertyChanged("CustomsID", value);
                }
            }
        }

        string _customsname;
        /// <summary>
        /// 报关行名称
        /// </summary>
        public string CustomsName
        {
            get
            {
                return _customsname;
            }
            set
            {
                if (_customsname != value)
                {
                    _customsname = value;
                    base.OnPropertyChanged("CustomsName", value);
                }
            }
        }

        CustomerDescription _customsdescription;
        /// <summary>
        /// 报关行描述
        /// </summary>
        public CustomerDescription CustomsDescription
        {
            get
            {
                return _customsdescription;
            }
            set
            {
                if (_customsdescription != value)
                {
                    _customsdescription = value;
                    base.OnPropertyChanged("CustomsDescription", value);
                }
            }
        }
        #endregion

        #region 清关
        bool _isclearance;
        /// <summary>
        /// 是否清关
        /// </summary>
        public bool IsClearance
        {
            get
            {
                return _isclearance;
            }
            set
            {
                if (_isclearance != value)
                {
                    _isclearance = value;
                    base.OnPropertyChanged("IsClearance", value);
                }
            }
        }

        #endregion

        #region 清关日期
        DateTime? _clearancedate;
        /// <summary>
        /// 清关时间
        /// </summary>
        public DateTime? ClearanceDate
        {
            get
            {
                return _clearancedate;
            }
            set
            {
                if (_clearancedate != value)
                {
                    _clearancedate = value;
                    base.OnPropertyChanged("ClearanceDate", value);
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
                    base.OnPropertyChanged("Remark", value);
                }
            }
        }

        #endregion

        #region 创建人
        Guid _createdID;
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid CreateID
        {
            get
            {
                return _createdID;
            }
            set
            {
                if (_createdID != value)
                {
                    _createdID = value;
                    base.OnPropertyChanged("CreateID", value);
                }
            }
        }

        string createByName;
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateByName
        {
            get
            {
                return createByName;
            }
            set
            {
                if (createByName != value)
                {
                    createByName = value;
                    base.OnPropertyChanged("CreateByName", value);
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
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        #endregion

        #region 清关单号
        string _clearanceNo;
        /// <summary>
        /// 清关号
        /// </summary>
        public string ClearanceNo
        {
            get
            {
                return _clearanceNo;
            }
            set
            {
                if (_clearanceNo != value)
                {
                    _clearanceNo = value;
                    base.OnPropertyChanged("ClearanceNo", value);
                }
            }
        }
        #endregion

        #region MBL信息
        /// <summary>
        /// MBL信息
        /// </summary>
        public AirBusinessMBLList MBLInfo;
        #endregion

        #region HBLList信息
        /// <summary>
        /// HBL List信息
        /// </summary>
        public List<AirBusinessHBLList> HBLList;
        #endregion

        #region 费用 List信息
        /// <summary>
        /// 费用 List信息
        /// </summary>
        public List<AirImportFeeList> FeeList;
        #endregion
    }

    #endregion

    #region 下载列表界面
    /// <summary>
    /// 业务下载列表界面用的所有字段
    /// 作者:罗丹
    /// 时间:2011-12-16
    /// </summary>
    [Serializable]
    public partial class AirBusinessDownLoadList : BaseDataObject
    {
        #region ID
        private Guid id;
        /// <summary>
        /// 唯一键
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
                    base.OnPropertyChanged("ID", value);
                }
            }
        }

        #endregion

        #region 单号

        private string refNo;
        /// <summary>
        /// 单号
        /// </summary>
        public string RefNo
        {
            get
            {
                return refNo;
            }
            set
            {
                if (refNo != value)
                {
                    refNo = value;
                    base.OnPropertyChanged("RefNo", value);
                }
            }
        }
        #endregion

        #region 勾选
        bool isCheck;
        /// <summary>
        /// 是否勾选
        /// </summary>
        public bool IsCheck
        {
            get
            {
                return isCheck;
            }
            set
            {
                if (isCheck != value)
                {
                    isCheck = value;
                    base.OnPropertyChanged("IsCheck", value);
                }
            }
        }
        #endregion

        #region 下载状态
        DownLoadState _downLoadstate;
        /// <summary>
        /// 下载状态
        /// </summary>
        public DownLoadState DownLoadState
        {
            get
            {
                return _downLoadstate;
            }
            set
            {
                if (_downLoadstate != value)
                {
                    _downLoadstate = value;
                    base.OnPropertyChanged("DownLoadState", value);
                }
            }
        }

        string downLoadstateDescription;
        /// <summary>
        /// 下载状态描述
        /// </summary>
        public string DownLoadStateDescription
        {
            get
            {
                if (string.IsNullOrEmpty(downLoadstateDescription))
                {
                    downLoadstateDescription = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<DownLoadState>(_downLoadstate, LocalData.IsEnglish);
                }
                return downLoadstateDescription;
            }
            set
            {
                downLoadstateDescription = value;
            }
        }
        #endregion

        #region 提单状态
        AIBLState _hblstate;
        /// <summary>
        /// 提单状态
        /// </summary>
        public AIBLState HBLState
        {
            get
            {
                return _hblstate;
            }
            set
            {
                if (_hblstate != value)
                {
                    _hblstate = value;
                    base.OnPropertyChanged("HBLState", value);
                }
            }
        }

        string hblstateDescription;
        /// <summary>
        /// 提单状态描述
        /// </summary>
        public string HBLStateDescription
        {
            get
            {
                if (string.IsNullOrEmpty(hblstateDescription))
                {
                    hblstateDescription = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<AIBLState>(_hblstate, LocalData.IsEnglish); ;
                }
                return hblstateDescription;
            }
            set
            {
                hblstateDescription = value;
            }
        }
        #endregion

        #region 主提单号
        private string mBLNo;
        /// <summary>
        /// 主提单号
        /// </summary>
        public string MBLNo
        {
            get
            {
                return mBLNo;
            }
            set
            {
                if (mBLNo != value)
                {
                    mBLNo = value;
                    base.OnPropertyChanged("MBLNo", value);
                }
            }
        }

        #endregion

        #region 分提单号
        private string hblNo;
        /// <summary>
        /// 分提单号
        /// </summary>
        public string HBLNo
        {
            get
            {
                return hblNo;
            }
            set
            {
                if (hblNo != value)
                {
                    hblNo = value;
                    base.OnPropertyChanged("HBLNo", value);
                }
            }
        }

        #endregion

        #region 分提单号ID集合
        private string hblIDs;
        /// <summary>
        /// MBL ID list
        /// </summary>
        public string HBLIDs
        {
            get
            {
                return hblIDs;
            }
            set
            {
                hblIDs = value;
            }

        }
        #endregion

        #region 航班号

        string _flightNo;
        /// <summary>
        /// 航班号
        /// </summary>
        public string FlightNo
        {
            get
            {
                return _flightNo;
            }
            set
            {
                if (_flightNo != value)
                {
                    _flightNo = value;
                    base.OnPropertyChanged("FlightNo", value);
                }
            }
        }

        #endregion

        #region 装货港
        string polname;
        /// <summary>
        /// 装货港
        /// </summary>
        public string POLName
        {
            get
            {
                return polname;
            }
            set
            {
                if (polname != value)
                {
                    polname = value;
                    base.OnPropertyChanged("POLName", value);
                }
            }
        }

        #endregion

        #region ETD
        DateTime? etd;
        /// <summary>
        /// 离港日
        /// </summary>
        public DateTime? ETD
        {
            get
            {
                return etd;
            }
            set
            {
                if (etd != value)
                {
                    etd = value;
                    base.OnPropertyChanged("ETD", value);
                }
            }
        }
        #endregion

        #region 卸货港
        string podname;
        /// <summary>
        /// 卸货港
        /// </summary>
        public string PODName
        {
            get
            {
                return podname;
            }
            set
            {
                if (podname != value)
                {
                    podname = value;
                    base.OnPropertyChanged("PODName", value);
                }
            }
        }

        #endregion

        #region ETA
        DateTime? eta;
        /// <summary>
        /// 到港日
        /// </summary>
        public DateTime? ETA
        {
            get
            {
                return eta;
            }
            set
            {
                if (eta != value)
                {
                    eta = value;
                    base.OnPropertyChanged("ETA", value);
                }
            }
        }
        #endregion

        #region 交货地
        string placeofdeliverynames;
        /// <summary>
        /// 交货地
        /// </summary>
        public string PlaceOfDeliveryNames
        {
            get
            {

                return placeofdeliverynames;
            }
            set
            {
                if (placeofdeliverynames != value)
                {
                    placeofdeliverynames = value;
                    base.OnPropertyChanged("PlaceOfDeliveryNames", value);
                }
            }
        }

        #endregion

        #region 到交货地日
        DateTime? placeofdeliveryDates;
        /// <summary>
        /// 到交货地日
        /// </summary>
        public DateTime? PlaceofdeliveryDates
        {
            get
            {
                return placeofdeliveryDates;
            }
            set
            {
                if (placeofdeliveryDates != value)
                {
                    placeofdeliveryDates = value;
                    base.OnPropertyChanged("PlaceofdeliveryDates", value);
                }
            }
        }
        #endregion

        #region 收货人

        Guid consigneeID;
        /// <summary>
        /// 收货人ID
        /// </summary>
        public Guid ConsigneeID
        {
            get
            {
                return consigneeID;
            }
            set
            {
                if (consigneeID != value)
                {
                    consigneeID = value;
                    base.OnPropertyChanged("ConsigneeID", value);
                }
            }
        }
        string consigneename;
        /// <summary>
        /// 收货人名称
        /// </summary>
        public string ConsigneeName
        {
            get
            {
                return consigneename;
            }
            set
            {
                if (consigneename != value)
                {
                    consigneename = value;
                    base.OnPropertyChanged("ConsigneeName", value);
                }
            }
        }
        #endregion

        #region 港前客服

        public string pollFierName;
        /// <summary>
        /// 港前客服名称
        /// </summary>
        public string POLFilerName
        {
            get
            {
                return pollFierName;
            }
            set
            {
                if (pollFierName != value)
                {
                    pollFierName = value;
                    base.OnPropertyChanged("POLFilerName", value);
                }
            }
        }

        #endregion

        #region 代理公司 (港前)
        string agentName;
        /// <summary>
        /// 代理公司 (港前)
        /// </summary>
        public string AgentName
        {
            get
            {
                return agentName;
            }
            set
            {
                if (agentName != value)
                {
                    agentName = value;
                    base.OnPropertyChanged("AgentName", value);
                }
            }
        }

        #endregion

        #region 操作口岸

        Guid? _companyid;

        /// <summary>
        /// (港前)操作口岸ID -- 获取汇率用
        /// </summary>
        public Guid? CompanyID
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

        #endregion

        #region 创建人
        Guid _createdID;
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateID
        {
            get
            {
                return _createdID;
            }
            set
            {
                if (_createdID != value)
                {
                    _createdID = value;
                    base.OnPropertyChanged("CreateID", value);
                }
            }
        }
        #endregion

        #region 港后业务号
        /// <summary>
        /// 港后业务号
        /// </summary>
        public string PODRefNo { get; set; }
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
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        #endregion
    }
    #endregion

    #region HBL信息
    /// <summary>
    /// HBL信息列表
    /// 作者:周任平 
    /// 时间:2011-05-18 12:30
    /// </summary>
    [Serializable]
    public partial class AirBusinessHBLList : BaseDataObject
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
                    base.OnPropertyChanged("ID", value);
                }
            }
        }
        #endregion

        #region 业务ID
        Guid oiBookingID;
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OIBookingID
        {
            get
            {
                return oiBookingID;
            }
            set
            {
                if (oiBookingID != value)
                {
                    oiBookingID = value;
                    base.OnPropertyChanged("OIBookingID", value);
                }
            }
        }

        #endregion

        #region 提单号
        string _hblno;
        /// <summary>
        /// 提单号
        /// </summary>
        [Required(CMessage = "分提单号", EMessage = "HBLNo")]
        public string HBLNo
        {
            get
            {
                return _hblno;
            }
            set
            {
                if (_hblno != value)
                {
                    _hblno = value;
                    base.OnPropertyChanged("HBLNo", value);
                }
            }
        }
        #endregion

        #region 发货人
        Guid? _shipperid;
        /// <summary>
        /// 发货人
        /// </summary>
        public Guid? ShipperID
        {
            get
            {
                return _shipperid;
            }
            set
            {
                if (_shipperid != value)
                {
                    _shipperid = value;
                    base.OnPropertyChanged("ShipperID", value);
                }
            }
        }

        string _shippername;
        /// <summary>
        /// 发货人名称
        /// </summary>
        public string ShipperName
        {
            get
            {
                return _shippername;
            }
            set
            {
                if (_shippername != value)
                {
                    _shippername = value;
                    base.OnPropertyChanged("ShipperName", value);
                }
            }
        }
        #endregion

        #region 收货人描述
        CustomerDescription _shipperdescription;
        /// <summary>
        /// 发货人描述
        /// </summary>
        public CustomerDescription ShipperDescription
        {
            get
            {
                return _shipperdescription;
            }
            set
            {
                if (_shipperdescription != value)
                {
                    _shipperdescription = value;
                    base.OnPropertyChanged("ShipperDescription", value);
                }
            }
        }

        #endregion

        #region 收到正本时间
        DateTime? receiveOBLDate;
        /// <summary>
        /// 收到正本时间
        /// </summary>
        public DateTime? ReceiveOBLDate
        {
            get
            {
                return receiveOBLDate;
            }
            set
            {
                if (receiveOBLDate != value)
                {
                    receiveOBLDate = value;
                    base.OnPropertyChanged("ReceiveOBLDate", value);
                }
            }
        }
        #endregion

        #region 创建人
        Guid _createdID;
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateID
        {
            get
            {
                return _createdID;
            }
            set
            {
                if (_createdID != value)
                {
                    _createdID = value;
                    base.OnPropertyChanged("CreateID", value);
                }
            }
        }
        #endregion

        #region 创建时间
        DateTime? _createDate;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                if (_createDate != value)
                {
                    _createDate = value;
                    base.OnPropertyChanged("CreateDate", value);
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
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        #endregion
    }
    /// <summary>
    /// HBL详细信息
    /// 作者：周任平
    /// 时间：2011-05-18 12：30
    /// </summary>
    [Serializable]
    public partial class AirBusinessHBLInfo : AirBusinessHBLList
    {

    }
    #endregion

    #region MBL信息
    /// <summary>
    /// MBL列表信息
    /// </summary>
    [Serializable]
    public partial class AirBusinessMBLList : BaseDataObject
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
                    base.OnPropertyChanged("ID", value);
                }
            }
        }
        #endregion

        #region 主提单号
        string _mblno;
        /// <summary>
        /// 主提单号
        /// </summary>
        [Required(CMessage = "主提单号", EMessage = "MBLNo")]
        public string MBLNo
        {
            get
            {
                return _mblno;
            }
            set
            {
                if (_mblno != value)
                {
                    _mblno = value;
                    base.OnPropertyChanged("MBLNo", value);
                }
            }
        }

        #endregion

        #region 子提单号
        string _subno;
        /// <summary>
        /// 子提单号
        /// </summary>
        public string SubNo
        {
            get
            {
                return _subno;
            }
            set
            {
                if (_subno != value)
                {
                    _subno = value;
                    base.OnPropertyChanged("SubNo", value);
                }
            }
        }
        #endregion

        #region 航空公司

        Guid _airCompanyid;
        /// <summary>
        /// 航空公司
        /// </summary>
        [GuidRequired(CMessage = "航空公司", EMessage = "AirCompany")]
        public Guid AirCompanyID
        {
            get
            {
                return _airCompanyid;
            }
            set
            {
                if (_airCompanyid != value)
                {
                    _airCompanyid = value;
                    base.OnPropertyChanged("AirCompanyID", value);
                }
            }
        }

        string _airCompanyName;
        /// <summary>
        /// 航空公司名称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "航空公司", EMessage = "AirCompanyName")]
        public string AirCompanyName
        {
            get
            {
                return _airCompanyName;
            }
            set
            {
                if (_airCompanyName != value)
                {
                    _airCompanyName = value;
                    base.OnPropertyChanged("AirCompanyName", value);
                }
            }
        }

        CustomerDescription _airCompanydescription;
        /// <summary>
        /// 航空公司描述
        /// </summary>
        public CustomerDescription AirCompanyDescription
        {
            get
            {
                return _airCompanydescription;
            }
            set
            {
                if (_airCompanydescription != value)
                {
                    _airCompanydescription = value;
                    base.OnPropertyChanged("AirCompanyDescription", value);
                }
            }
        }
        #endregion

        #region 承运人
        Guid _agentofcarrierid;
        /// <summary>
        /// 承运人ID
        /// </summary>
        [GuidRequired(CMessage = "承运人", EMessage = "AgentOfCarrier")]
        public Guid AgentOfCarrierID
        {
            get
            {
                return _agentofcarrierid;
            }
            set
            {
                if (_agentofcarrierid != value)
                {
                    _agentofcarrierid = value;
                    base.OnPropertyChanged("AgentOfCarrierID", value);
                }
            }
        }

        string _agentofcarriername;
        /// <summary>
        /// 承运人名称
        /// </summary>
        public string AgentOfCarrierName
        {
            get
            {
                return _agentofcarriername;
            }
            set
            {
                if (_agentofcarriername != value)
                {
                    _agentofcarriername = value;
                    base.OnPropertyChanged("AgentOfCarrierName", value);
                }
            }
        }

        CustomerDescription _agentofcarrierdescription;
        /// <summary>
        /// 承运人描述
        /// </summary>
        public CustomerDescription AgentOfCarrierDescription
        {
            get
            {
                return _agentofcarrierdescription;
            }
            set
            {
                if (_agentofcarrierdescription != value)
                {
                    _agentofcarrierdescription = value;
                    base.OnPropertyChanged("AgentOfCarrierDescription", value);
                }
            }
        }
        #endregion

        #region 航班

        Guid? _flightID;
        /// <summary>
        /// 航班ID
        /// </summary>
        public Guid? FlightID
        {
            get
            {
                return _flightID;
            }
            set
            {
                if (_flightID != value)
                {
                    _flightID = value;
                    base.OnPropertyChanged("FlightID", value);
                }
            }
        }

        string _flightNo;
        /// <summary>
        /// 航班号
        /// </summary>
        public string FlightNo
        {
            get
            {
                return _flightNo;
            }
            set
            {
                if (_flightNo != value)
                {
                    _flightNo = value;
                    base.OnPropertyChanged("FlightNo", value);
                }
            }
        }

        #endregion

        #region FlightFlag/FlightCountry

        string _flightFlag;
        /// <summary>
        /// FlightFlag
        /// </summary>
        public string FlightFlag
        {
            get
            {
                return _flightFlag;
            }
            set
            {
                if (_flightFlag != value)
                {
                    _flightFlag = value;
                    base.OnPropertyChanged("FlightFlag", value);
                }
            }
        }

        string _flightCountry;
        /// <summary>
        /// FlightCountry
        /// </summary>
        public string FlightCountry
        {
            get
            {
                return _flightCountry;
            }
            set
            {
                if (_flightCountry != value)
                {
                    _flightCountry = value;
                    base.OnPropertyChanged("FlightCountry", value);
                }
            }
        }

        #endregion

        #region Manifest NO

        string _manifestNO;
        /// <summary>
        /// Manifest NO
        /// </summary>
        public string ManifestNO
        {
            get
            {
                return _manifestNO;
            }
            set
            {
                if (_manifestNO != value)
                {
                    _manifestNO = value;
                    base.OnPropertyChanged("ManifestNO", value);
                }
            }
        }

        #endregion

        #region G.O. Date

        DateTime? _gODate;
        /// <summary>
        /// G.O. Date
        /// </summary>
        public DateTime? GODate
        {
            get
            {
                return _gODate;
            }
            set
            {
                if (_gODate != value)
                {
                    _gODate = value;
                    base.OnPropertyChanged("GODate", value);
                }
            }
        }

        #endregion

        #region 提货地
        Guid _finalWareHouseID;
        /// <summary>
        /// 提货地
        /// </summary>
        public Guid FinalWareHouseID
        {
            get
            {
                return _finalWareHouseID;
            }
            set
            {
                if (_finalWareHouseID != value)
                {
                    _finalWareHouseID = value;
                    base.OnPropertyChanged("FinalWareHouseID", value);
                }
            }
        }

        string _finalWareHouseName;
        /// <summary>
        /// 提货地名称
        /// </summary>
        public string FinalWareHouseName
        {
            get
            {
                return _finalWareHouseName;
            }
            set
            {
                if (_finalWareHouseName != value)
                {
                    _finalWareHouseName = value;
                    base.OnPropertyChanged("FinalWareHouseName", value);
                }
            }
        }

        CustomerDescription _finalWareHousedescription;
        /// <summary>
        /// 提货地描述
        /// </summary>
        public CustomerDescription FinalWareHouseDescription
        {
            get
            {
                return _finalWareHousedescription;
            }
            set
            {
                if (_finalWareHousedescription != value)
                {
                    _finalWareHousedescription = value;
                    base.OnPropertyChanged("FinalWareHouseDescription", value);
                }
            }
        }
        #endregion

        #region 转关
        string _itno;
        /// <summary>
        /// 转关号
        /// </summary>
        public string ITNO
        {
            get
            {
                return _itno;
            }
            set
            {
                if (_itno != value)
                {
                    _itno = value;
                    base.OnPropertyChanged("ITNO", value);
                }
            }
        }

        DateTime? _itdate;
        /// <summary>
        /// 转关时间
        /// </summary>
        public DateTime? ITDate
        {
            get
            {
                return _itdate;
            }
            set
            {
                if (_itdate != value)
                {
                    _itdate = value;
                    base.OnPropertyChanged("ITDate", value);
                }
            }
        }

        string _itPalce;
        /// <summary>
        /// 转关地点
        /// </summary>
        public string ITPalce
        {
            get
            {
                return _itPalce;
            }
            set
            {
                if (_itPalce != value)
                {
                    _itPalce = value;
                    base.OnPropertyChanged("ITPalce", value);
                }
            }
        }
        #endregion

        #region 放货类型

        FCMReleaseType _releasetype;
        /// <summary>
        /// 放货类型
        /// </summary>
        [Required(CMessage = "放货类型", EMessage = "ReleaseType")]
        public FCMReleaseType ReleaseType
        {
            get
            {
                return _releasetype;
            }
            set
            {
                if (_releasetype != value)
                {
                    _releasetype = value;
                    base.OnPropertyChanged("ReleaseType", value);
                }
            }
        }

        string _releasetypedescription;
        /// <summary>
        /// 放货类型描述
        /// </summary>
        public string ReleaseTypeDescription
        {
            get
            {
                try
                {
                    return EnumHelper.GetDescription<FCMReleaseType>(ReleaseType, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        #endregion

        #region MBL运输方式
        Guid _mbltransportclauseid;
        /// <summary>
        /// MBL运输方式ID
        /// </summary>
        [GuidRequired(CMessage = "运输条款<MBL>", EMessage = "MBLTransportClause")]
        public Guid MBLTransportClauseID
        {
            get
            {
                return _mbltransportclauseid;
            }
            set
            {
                if (_mbltransportclauseid != value)
                {
                    _mbltransportclauseid = value;
                    base.OnPropertyChanged("MBLTransportClauseID", value);
                }
            }
        }

        string _mbltransportclausename;
        /// <summary>
        /// MBL运输方式名称
        /// </summary>
        public string MBLTransportClauseName
        {
            get
            {
                return _mbltransportclausename;
            }
            set
            {
                if (_mbltransportclausename != value)
                {
                    _mbltransportclausename = value;
                    base.OnPropertyChanged("MBLTransportClauseName", value);
                }
            }
        }
        #endregion

        #region 创建人
        Guid _saveID;
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid SaveByID
        {
            get
            {
                return _saveID;
            }
            set
            {
                if (_saveID != value)
                {
                    _saveID = value;
                    base.OnPropertyChanged("SaveByID", value);
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
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        #endregion
    }

    [Serializable]
    public partial class AirBusinessMBLInfo : AirBusinessMBLList
    {

    }
    #endregion

}
