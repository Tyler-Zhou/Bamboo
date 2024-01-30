using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using System.Runtime.Serialization;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FCM.OceanImport.ServiceInterface
{

    #region 业务列表
    /// <summary>
    /// 业务查询列表的所有字段
    /// 作者：周任平
    /// 创建时间：2011-05-17 12:00
    /// </summary>
    [Serializable]
    public partial class OceanBusinessList : BaseDataObject
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
        OIOrderState _state;
        /// <summary>
        /// 状态
        /// </summary>
        public OIOrderState State
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
                        stateDescription = EnumHelper.GetDescription<OIOrderState>(State, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
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
        [GuidRequired(CMessage = "代理", EMessage = "Agent")]
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


        ICP.Framework.CommonLibrary.Common.CustomerDescription _customerdescription;
        /// <summary>
        /// 客户联系人详细信息
        /// </summary>
        public ICP.Framework.CommonLibrary.Common.CustomerDescription CustomerDescription
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

        #region FETA
        DateTime? _feta;
        /// <summary>
        ///  F.ETA 
        /// </summary>
        public DateTime? FETA
        {
            get
            {
                return _feta;
            }
            set
            {
                if (_feta != value)
                {
                    _feta = value;
                    base.OnPropertyChanged("FETA", value);
                }
            }
        }
        #endregion

        #region 船名、航次
        Guid _vesselid;
        /// <summary>
        /// 船名ID
        /// </summary>
        public Guid VesselID
        {
            get
            {
                return _vesselid;
            }
            set
            {
                if (_vesselid != value)
                {
                    _vesselid = value;
                    base.OnPropertyChanged("VesselID", value);
                }
            }
        }

        string _vesselName;
        /// <summary>
        /// 船名
        /// </summary>
        public string VesselName
        {
            get
            {
                return _vesselName;
            }
            set
            {
                if (_vesselName != value)
                {
                    _vesselName = value;
                    base.OnPropertyChanged("VesselName", value);
                }
            }
        }

        string _voyage;
        /// <summary>
        /// 航次
        /// </summary>
        public string Voyage
        {
            get
            {
                return _voyage;
            }
            set
            {
                _voyage = value;
                base.OnPropertyChanged("Voyage", value);
            }
        }

        /// <summary>
        /// 船名航次
        /// </summary>
        public string VesselVoyage
        {
            get;
            set;
        }

        #endregion

        #region 箱号
        string _containerno;
        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNo
        {
            get
            {
                return _containerno;
            }
            set
            {
                if (_containerno != value)
                {
                    _containerno = value;
                    base.OnPropertyChanged("ContainerNo", value);
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
        Guid? _placeofdeliveryid;
        /// <summary>
        /// 交货地ID
        /// </summary>
        [GuidRequired(CMessage = "交货地", EMessage = "PlaceOfDelivery")]
        public Guid? PlaceOfDeliveryID
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

        #region 海外部客服

        Guid? _overSeasFilerId;
        /// <summary>
        /// 海外部客服ID
        /// </summary>
        public Guid? OverSeasFilerId
        {
            get
            {
                return _overSeasFilerId;
            }
            set
            {
                if (_overSeasFilerId != value)
                {
                    _overSeasFilerId = value;
                    base.OnPropertyChanged("OverSeasFilerId", value);
                }
            }
        }

        string _overSeasFilerName;
        /// <summary>
        /// 海外部客服名称
        /// </summary>
        public string OverSeasFilerName
        {
            get
            {
                return _overSeasFilerName;
            }
            set
            {
                if (_overSeasFilerName != value)
                {
                    _overSeasFilerName = value;
                    base.OnPropertyChanged("OverSeasFilerName", value);
                }
            }
        }

        #endregion

        #region 拖车客服

        Guid? _localCSId;
        /// <summary>
        /// 拖车客服ID
        /// </summary>
        public Guid? LocalCSId
        {
            get
            {
                return _localCSId;
            }
            set
            {
                if (_localCSId != value)
                {
                    _localCSId = value;
                    base.OnPropertyChanged("LocalCSId", value);
                }
            }
        }

        string _localCSName;
        /// <summary>
        /// 拖车客服名称
        /// </summary>
        public string LocalCSName
        {
            get
            {
                return _localCSName;
            }
            set
            {
                if (_localCSName != value)
                {
                    _localCSName = value;
                    base.OnPropertyChanged("LocalCSName", value);
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

        #region 最终目的地
        Guid? _finaldestinationid;
        /// <summary>
        /// 最终目的地ID
        /// </summary>
        public Guid? FinalDestinationID
        {
            get
            {
                return _finaldestinationid;
            }
            set
            {
                if (_finaldestinationid != value)
                {
                    _finaldestinationid = value;
                    base.OnPropertyChanged("FinalDestinationID", value);
                }
            }
        }

        string _finaldestinationname;
        /// <summary>
        /// 最终目的地名称
        /// </summary>
        public string FinalDestinationName
        {
            get
            {
                return _finaldestinationname;
            }
            set
            {
                if (_finaldestinationname != value)
                {
                    _finaldestinationname = value;
                    base.OnPropertyChanged("FinalDestinationName", value);
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

        #region 进港日
        DateTime? _gateindate;
        /// <summary>
        /// 进港日
        /// </summary>
        public DateTime? GateInDate
        {
            get
            {
                return _gateindate;
            }
            set
            {
                if (_gateindate != value)
                {
                    _gateindate = value;
                    base.OnPropertyChanged("GateInDate", value);
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

        #region 放货进度

        /// <summary>
        /// 放单日期
        /// </summary>
        DateTime? _releaseBLDate;
        /// <summary>
        /// 放单日期
        /// </summary>
        public DateTime? ReleaseBLDate
        {
            get
            {
                return _releaseBLDate;
            }
            set
            {
                if (_releaseBLDate != value)
                {
                    _releaseBLDate = value;
                    base.OnPropertyChanged("ReleaseBLDate", value);
                }
            }
        }

        /// <summary>
        /// 电放号
        /// </summary>
        string _telexNo;
        /// <summary>
        /// 电放号
        /// </summary>
        public string TelexNo
        {
            get
            {
                return _telexNo;
            }
            set
            {
                if (_telexNo != value)
                {
                    _telexNo = value;
                    base.OnPropertyChanged("TelexNo", value);
                }
            }
        }
        /// <summary>
        /// 已下达的放单指令
        /// </summary>
        bool _isRelease;
        /// <summary>
        /// 已下达的放单指令
        /// </summary>
        public bool IsRelease
        {
            get
            {
                return _isRelease;
            }
            set
            {
                if (_isRelease != value)
                {
                    _isRelease = value;
                    base.OnPropertyChanged("IsRelease", value);
                }
            }
        }
        /// <summary>
        /// 港后已收到放单指令
        /// </summary>
        bool _isReceiveNotice;
        /// <summary>
        /// 港后已收到放单指令
        /// </summary>
        public bool IsReceiveNotice
        {
            get
            {
                return _isReceiveNotice;
            }
            set
            {
                if (_isReceiveNotice != value)
                {
                    _isReceiveNotice = value;
                    base.OnPropertyChanged("IsReceiveNotice", value);
                }
            }
        }
        /// <summary>
        /// 已申请放货
        /// </summary>
        bool _isApplyRC;
        /// <summary>
        /// 已申请放货
        /// </summary>
        public bool IsApplyRC
        {
            get
            {
                return _isApplyRC;
            }
            set
            {
                if (_isApplyRC != value)
                {
                    _isApplyRC = value;
                    base.OnPropertyChanged("IsApplyRC", value);
                }
            }
        }
        /// <summary>
        /// 同意放货
        /// </summary>
        bool _isAgreeRC;
        /// <summary>
        /// 同意放货
        /// </summary>
        public bool IsAgreeRC
        {
            get
            {
                return _isAgreeRC;
            }
            set
            {
                if (_isAgreeRC != value)
                {
                    _isAgreeRC = value;
                    base.OnPropertyChanged("IsAgreeRC", value);
                }
            }
        }
        /// <summary>
        /// 收到MBL正本
        /// </summary>
        bool _isOMBLRcved;
        /// <summary>
        /// 收到MBL正本
        /// </summary>
        public bool IsOMBLRcved
        {
            get
            {
                return _isOMBLRcved;
            }
            set
            {
                if (_isOMBLRcved != value)
                {
                    _isOMBLRcved = value;
                    base.OnPropertyChanged("IsOMBLRcved", value);
                }
            }
        }
        /// <summary>
        /// 财务寄出MBL
        /// </summary>
        bool _IsMailDMBL;
        /// <summary>
        /// 财务寄出MBL
        /// </summary>
        public bool IsMailDMBL
        {
            get
            {
                return _IsMailDMBL;
            }
            set
            {
                if (_IsMailDMBL != value)
                {
                    _IsMailDMBL = value;
                    base.OnPropertyChanged("IsMailDMBL", value);
                }
            }
        }
        /// <summary>
        /// 关帐
        /// </summary>
        bool _IsACCLOS;
        /// <summary>
        /// 关帐
        /// </summary>
        public bool IsACCLOS
        {
            get
            {
                return _IsACCLOS;
            }
            set
            {
                if (_IsACCLOS != value)
                {
                    _IsACCLOS = value;
                    base.OnPropertyChanged("IsACCLOS", value);
                }
            }
        }

        /// <summary>
        /// 已催港前放单
        /// </summary>
        bool _isNoticeRelease;
        /// <summary>
        /// 已催港前放单
        /// </summary>
        public bool IsNoticeRelease
        {
            get
            {
                return _isNoticeRelease;
            }
            set
            {
                if (_isNoticeRelease != value)
                {
                    _isNoticeRelease = value;
                    base.OnPropertyChanged("IsNoticeRelease", value);
                }
            }
        }
        /// <summary>
        /// 已催客户付款
        /// </summary>
        bool _isNoticePay;
        /// <summary>
        /// 已催客户付款
        /// </summary>
        public bool IsNoticePay
        {
            get
            {
                return _isNoticePay;
            }
            set
            {
                if (_isNoticePay != value)
                {
                    _isNoticePay = value;
                    base.OnPropertyChanged("IsNoticePay", value);
                }
            }
        }
        /// <summary>
        /// 已放货
        /// </summary>
        bool _isReleaseCargo;
        /// <summary>
        /// 已放货
        /// </summary>
        public bool IsReleaseCargo
        {
            get
            {
                return _isReleaseCargo;
            }
            set
            {
                if (_isReleaseCargo != value)
                {
                    _isReleaseCargo = value;
                    base.OnPropertyChanged("IsReleaseCargo", value);
                }
            }
        }

        #endregion

        #region IsWriteOff
        bool _IsWriteOff;
        /// <summary>
        /// 已销帐
        /// </summary>
        public bool IsWriteOff
        {
            get
            {
                return _IsWriteOff;
            }
            set
            {
                if (_IsWriteOff != value)
                {
                    _IsWriteOff = value;
                    this.NotifyPropertyChanged(o => o.IsWriteOff);
                }
            }
        }
        #endregion

        #region IsPaid

        bool _IsPaid;
        /// <summary>
        /// 到帐
        /// </summary>
        public bool IsPaid
        {
            get
            {
                return _IsPaid;
            }
            set
            {
                if (_IsPaid != value)
                {
                    _IsPaid = value;
                    this.NotifyPropertyChanged(o => o.IsPaid);
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
    public partial class OceanBusinessInfo : OceanBusinessList
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

        #region 业务类型

        FCMOperationType _oIOperationType;
        /// <summary>
        /// 业务类型
        /// </summary>
        [Required(CMessage = "业务类型", EMessage = "OIOperationType")]
        public FCMOperationType OIOperationType
        {
            get
            {
                return _oIOperationType;
            }
            set
            {
                if (_oIOperationType != value)
                {
                    _oIOperationType = value;
                    base.OnPropertyChanged("OIOperationType", value);
                }
            }
        }

        /// <summary>
        /// 在列表中显示对应语言的“业务类型”枚举值的描述信息
        /// </summary>    
        public string BookingTypeDescription { get; set; }

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

        #region 收货地
        Guid? _placeofreceiptid;
        /// <summary>
        /// 收货地
        /// </summary>
        public Guid? PlaceOfReceiptID
        {
            get
            {
                return _placeofreceiptid;
            }
            set
            {
                if (_placeofreceiptid != value)
                {
                    _placeofreceiptid = value;
                    base.OnPropertyChanged("PlaceOfReceiptID", value);
                }
            }
        }

        string _placeofreceiptname;
        /// <summary>
        /// 收货地名称
        /// </summary>
        public string PlaceOfReceiptName
        {
            get
            {
                return _placeofreceiptname;
            }
            set
            {
                if (_placeofreceiptname != value)
                {
                    _placeofreceiptname = value;
                    base.OnPropertyChanged("PlaceOfReceiptName", value);
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
        string goodDescription;
        /// <summary>
        /// 货物描述
        /// </summary>
        public string GoodDescription
        {
            get
            {
                return goodDescription;
            }
            set
            {
                if (goodDescription != value)
                {
                    goodDescription = value;
                    base.OnPropertyChanged("GoodDescription", value);
                }
            }
        }
        #endregion

        #region 箱信息(集装箱描述)

        ContainerDescription _containerdescription;
        /// <summary>
        /// 箱信息(集装箱描述)
        /// </summary>
        public ContainerDescription ContainerDescription
        {
            get
            {
                return _containerdescription;
            }
            set
            {
                if (_containerdescription != value)
                {
                    _containerdescription = value;
                    base.OnPropertyChanged("ContainerDescription", value);
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

        #region 清关号
        string _clearanceNo;
        /// <summary>
        /// 清关单号
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

        #region MBL信息
        /// <summary>
        /// MBL信息
        /// </summary>
        public OceanBusinessMBLList MBLInfo;
        #endregion

        #region HBLList信息
        /// <summary>
        /// HBL List信息
        /// </summary>
        public List<OceanBusinessHBLList> HBLList;
        #endregion

        #region 集装箱List信息
        /// <summary>
        /// 集装箱List信息
        /// </summary>
        public List<OIBusinessContainerList> ContainerList;
        #endregion

        #region 费用 List信息
        /// <summary>
        /// 费用 List信息
        /// </summary>
        public List<OceanImportFeeList> FeeList;
        #endregion

        #region PO List信息
        /// <summary>
        /// PO List信息
        /// </summary>
        public List<OceanImportPOList> POList;
        #endregion

        #region 关联的集装箱IDList信息
        public List<Guid> BoxIDList;
        #endregion


        /// <summary>
        /// 运费已包含
        /// </summary>
        public string FreightIncludedIds { get; set; }

        /// <summary>
        /// 运费已包含
        /// </summary>
        public string FreightIncludedCodes { get; set; }

    }

    #endregion

    #region 下载列表界面
    /// <summary>
    /// 业务下载列表界面用的所有字段
    /// 作者:周任平
    /// 时间:2011-05-17 12:00
    /// </summary>
    [Serializable]
    public partial class OceanBusinessDownLoadList : BaseDataObject
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

        #region 下载时间



        DateTime? _downLoadDate;
        /// <summary>
        /// 下载时间
        /// </summary>
        public DateTime? DownLoadDate
        {
            get
            {
                return _downLoadDate;
            }
            set
            {
                if (_downLoadDate != value)
                {
                    _downLoadDate = value;
                    base.OnPropertyChanged("DownLoadDate", value);
                }
            }
        }

        #endregion

        #region 提单状态
        OEBLState _hblstate;
        /// <summary>
        /// 提单状态
        /// </summary>
        public OEBLState HBLState
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
                    hblstateDescription = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<OEBLState>(_hblstate, LocalData.IsEnglish); ;
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

        #region 箱号
        string _containerNos;
        /// <summary>
        /// 箱号列表
        /// </summary>
        public string ContainerNos
        {
            get
            {
                return _containerNos;
            }
            set
            {
                if (_containerNos != value)
                {
                    _containerNos = value;
                    base.OnPropertyChanged("ContainerNos", value);
                }
            }
        }

        #endregion

        #region 船名、航次
        string _vesselvoyage;
        /// <summary>
        /// 船名航次
        /// </summary>
        public string VesselVoyage
        {
            get
            {
                return _vesselvoyage;
            }
            set
            {
                if (_vesselvoyage != value)
                {
                    _vesselvoyage = value;
                    base.OnPropertyChanged("VesselVoyage", value);
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

        #region 操作口岸

        Guid? _agentid;

        /// <summary>
        /// 港后代理ID
        /// </summary>
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

        #region 港后业务ID
        /// <summary>
        /// 港后业务ID
        /// </summary>
        public Guid RefID { get; set; }
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

        #region 文档状态
        DocumentState _DocumentState;
        /// <summary>
        /// 文档分发状态
        /// </summary>
        public DocumentState DocumentState
        {
            get
            {
                return _DocumentState;
            }
            set
            {
                if (_DocumentState != value)
                {
                    _DocumentState = value;
                    base.OnPropertyChanged("DocumentState", value);
                }
            }
        }
        #endregion

        #region 分发文档状态
        string documentStateDescription;
        /// <summary>
        /// 下载状态描述
        /// </summary>
        public string DocumentStateDescription
        {
            get
            {
                if (string.IsNullOrEmpty(documentStateDescription))
                {
                    documentStateDescription = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<DocumentState>(_DocumentState, LocalData.IsEnglish);
                }
                return documentStateDescription;
            }
            set
            {
                documentStateDescription = value;
            }
        }
        #endregion

        #region 是否重新分发
        private bool isAgainDispatch;
        /// <summary>
        /// 是否重新分发
        /// </summary>
        public bool IsAgainDispatch
        {
            get
            {
                return isAgainDispatch;
            }
            set
            {
                if (isAgainDispatch != value)
                {
                    isAgainDispatch = value;
                    base.OnPropertyChanged("IsAgainDispatch", value);
                }
            }
        }
        #endregion

        #region 指派给
        private string _AssignToName;
        public string AssignToName
        {
            get
            {
                return _AssignToName;
            }
            set
            {
                if (_AssignToName != value)
                {
                    _AssignToName = value;
                    base.OnPropertyChanged("AssignToName", value);
                }
            }
        }

        #endregion

        #region 业务ID
        Guid _OceanBookingID;
        public Guid OceanBookingID
        {
            get
            {
                return _OceanBookingID;
            }
            set
            {
                if (_OceanBookingID != value)
                {
                    _OceanBookingID = value;
                    base.OnPropertyChanged("OceanBookingID", value);
                }
            }
        }
        #endregion

        #region 港前业务No
        string _No;
        public string No
        {
            get
            {
                return _No;
            }
            set
            {
                if (_No != value)
                {
                    _No = value;
                    base.OnPropertyChanged("No", value);
                }
            }
        }
        #endregion

        #region 分文件ID
        Guid _OceanAgentDispatchID;
        public Guid OceanAgentDispatchID
        {
            get
            {
                return _OceanAgentDispatchID;
            }
            set
            {
                if (_OceanAgentDispatchID != value)
                {
                    _OceanAgentDispatchID = value;
                    base.OnPropertyChanged("OceanAgentDispatchID", value);
                }
            }
        }
        #endregion

        #region  记录错误行
        public bool IsError { get; set; }
        #endregion

        #region 记录错误信息
        public string ErrorInfo { get; set; }
        #endregion

        #region 业务创建时间
        public DateTime CreateDate { get; set; }
        #endregion

        #region 放单
        /// <summary>
        ///  放单
        /// </summary>
        bool _isreleasebl;
        public bool IsReleaseBL
        {
            get { return _isreleasebl; }
            set { if (_isreleasebl != value) _isreleasebl = value; base.OnPropertyChanged("IsReleaseBL", value); }
        }
        #endregion

        #region 是否重新分发
        private bool isAgainRevise;
        /// <summary>
        /// 是否重新修订
        /// </summary>
        public bool IsAgainRevise
        {
            get
            {
                return isAgainRevise;
            }
            set
            {
                if (isAgainRevise != value)
                {
                    isAgainRevise = value;
                    base.OnPropertyChanged("IsAgainRevise", value);
                }
            }
        }
        #endregion
    }
    #endregion

    #region 集装箱列表

    /// <summary>
    /// 集装箱列表界面
    /// 作者: 周任平 
    /// 时间：2011-05-17 16:00
    /// </summary>
    [Serializable]
    public partial class OIBusinessContainerList : BaseDataObject
    {

        #region ID
        private Guid? id;
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
                    base.OnPropertyChanged("ID", value);
                }
            }
        }
        #endregion

        #region 关联
        bool isrelation;
        /// <summary>
        /// 关联
        /// </summary>
        public bool IsRelation
        {
            get
            {
                return isrelation;

            }
            set
            {
                if (isrelation != value)
                {
                    isrelation = value;
                    base.OnPropertyChanged("IsRelation", value);
                }
            }
        }
        #endregion

        #region 箱号
        string _no;
        /// <summary>
        /// 编号
        /// </summary>
        [Required(CMessage = "箱号", EMessage = "ContainerNo")]
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

        #region 箱型
        [GuidRequired(CMessage = "箱型", EMessage = "ContainerType")]
        private Guid containerTypeID;

        /// <summary>
        /// 箱型ID
        /// </summary>
        public Guid ContainerTypeID
        {
            get
            {
                return containerTypeID;
            }
            set
            {
                if (containerTypeID != value)
                {
                    containerTypeID = value;
                    base.OnPropertyChanged("ContainerTypeID", value);
                }
            }
        }

        private string containerTypeName;
        /// <summary>
        /// 箱型
        /// </summary>
        public string ContainerTypeName
        {
            get
            {
                return containerTypeName;
            }
            set
            {
                if (containerTypeName != value)
                {
                    containerTypeName = value;
                    base.OnPropertyChanged("ContainerTypeName", value);
                }
            }
        }

        #endregion

        #region 封条号
        private string sealNo;
        /// <summary>
        /// 封条号
        /// </summary>
        public string SealNo
        {
            get
            {
                return sealNo;
            }
            set
            {
                if (sealNo != value)
                {
                    sealNo = value;
                    base.OnPropertyChanged("SealNo", value);
                }
            }
        }
        #endregion

        #region 重量
        decimal? _weight;
        /// <summary>
        /// 重量
        /// </summary>
        public decimal? Weight
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
        #endregion

        #region 体积
        decimal? _measurement;
        /// <summary>
        /// 体积
        /// </summary>
        public decimal? Measurement
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
        #endregion

        #region 数量
        int? _quantity;
        /// <summary>
        /// 数量
        /// </summary>
        public int? Quantity
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
        #endregion

        #region 提货单号
        private string blNo;
        /// <summary>
        /// 提货单号
        /// </summary>
        public string BLNo
        {
            get
            {
                return blNo;
            }
            set
            {
                if (blNo != value)
                {
                    blNo = value;
                    base.OnPropertyChanged("BLNo", value);
                }
            }
        }
        #endregion

        #region G.O.Date
        DateTime? godate;
        /// <summary>
        /// 监管仓日
        /// </summary>
        public DateTime? GODate
        {
            get
            {
                return godate;
            }
            set
            {
                if (godate != value)
                {
                    godate = value;
                    base.OnPropertyChanged("GODate", value);
                }
            }
        }
        #endregion

        #region LFDate
        DateTime? lfDate;
        /// <summary>
        /// 最后免堆日期
        /// </summary>
        public DateTime? LFDate
        {
            get
            {
                return lfDate;
            }
            set
            {
                if (lfDate != value)
                {
                    lfDate = value;
                    base.OnPropertyChanged("LFDate", value);
                }
            }
        }
        #endregion

        #region 可以提货日
        private DateTime? availableDate;
        /// <summary>
        /// 可以提货日
        /// </summary>
        public DateTime? AvailableDate
        {
            get
            {
                return availableDate;
            }
            set
            {
                if (availableDate != value)
                {
                    availableDate = value;
                    base.OnPropertyChanged("AvailableDate", value);
                }
            }
        }
        #endregion

        #region 发送提货日
        private DateTime? sendPickUPDate;
        /// <summary>
        /// 发送提货日
        /// </summary>
        public DateTime? SendPickUPDate
        {
            get
            {
                return sendPickUPDate;
            }
            set
            {
                if (sendPickUPDate != value)
                {
                    sendPickUPDate = value;
                    base.OnPropertyChanged("SendPickUPDate", value);
                }
            }
        }
        #endregion

        #region 地点
        private string location;
        /// <summary>
        /// 地点
        /// </summary>
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                if (location != value)
                {
                    location = value;
                    base.OnPropertyChanged("Location", value);
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

        #region 分单
        private bool isPartOf;
        /// <summary>
        /// 分单
        /// </summary>
        public bool IsPartOf
        {
            get
            {
                return isPartOf;
            }
            set
            {
                if (isPartOf != value)
                {
                    isPartOf = value;
                    base.OnPropertyChanged("IsPartOf", value);
                }
            }
        }
        #endregion

        #region 提柜时间
        private DateTime? _pickUpDate;
        /// <summary>
        /// 提柜时间
        /// </summary>
        public DateTime? PickUpDate
        {
            get
            {
                return _pickUpDate;
            }
            set
            {
                if (_pickUpDate != value)
                {
                    _pickUpDate = value;
                    base.OnPropertyChanged("PickUpDate", value);
                }
            }
        }
        #endregion

        #region 还空时间
        private DateTime? _returnDate;
        /// <summary>
        /// 还空时间
        /// </summary>
        public DateTime? ReturnDate
        {
            get
            {
                return _returnDate;
            }
            set
            {
                if (_returnDate != value)
                {
                    _returnDate = value;
                    base.OnPropertyChanged("ReturnDate", value);
                }
            }
        }
        #endregion

        #region DeliveryTime
       
        private DateTime? _deliveryTime;
        public DateTime? DeliveryTime 
        {
            get 
            {
                return _deliveryTime;
            }
            set        
            {
                if (_deliveryTime != value)
                {
                    _deliveryTime = value;
                    base.OnPropertyChanged("DeliveryTime", value);
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


        Guid? _refID;
        /// <summary>
        /// 关联海出箱ID  
        /// joe 2013-07-18
        /// </summary>
        public Guid? RefID
        {
            get
            {
                return _refID;
            }
            set
            {
                if (_refID != value)
                {
                    _refID = value;
                    base.OnPropertyChanged("RefID", value);
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 集装箱详细信息 
    /// 作者：周任平 
    /// 时间：2011-05-17 16:00
    /// </summary>
    [Serializable]
    public partial class OIBusinessContainerInfo : OIBusinessContainerList
    {
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
        DateTime _createDate;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
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
    }

    #endregion

    #region HBL信息
    /// <summary>
    /// HBL信息列表
    /// 作者:周任平 
    /// 时间:2011-05-18 12:30
    /// </summary>
    [Serializable]
    public partial class OceanBusinessHBLList : BaseDataObject
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

        #region AMSNO
        string amsno;
        /// <summary>
        /// AMSNo
        /// </summary>
        public string AMSNo
        {
            get
            {
                return amsno;
            }
            set
            {
                if (amsno != value)
                {
                    amsno = value;
                    base.OnPropertyChanged("AMSNo", value);
                }
            }
        }
        #endregion

        #region ISFNo
        string isfno;
        /// <summary>
        /// ISFNo
        /// </summary>
        public string ISFNo
        {
            get
            {
                return isfno;
            }
            set
            {
                if (isfno != value)
                {
                    isfno = value;
                    base.OnPropertyChanged("ISFNo", value);
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

        #region 品名
        private string goodsInfo;
        /// <summary>
        /// 品名
        /// </summary>
        public string GoodsInfo
        {
            get
            {
                return goodsInfo;
            }
            set
            {
                if (goodsInfo != value)
                {
                    goodsInfo = value;
                    this.NotifyPropertyChanged(o => o.GoodsInfo);
                }
            }
        }
        #endregion

        #region 数量
        private Int32? qty;
        /// <summary>
        /// 品名
        /// </summary>
        public Int32? Qty
        {
            get
            {
                return qty;
            }
            set
            {
                if (qty != value)
                {
                    qty = value;
                    this.NotifyPropertyChanged(o => o.Qty);
                }
            }
        }
        #endregion

        #region 重量
        private Decimal? weight;
        /// <summary>
        /// 重量
        /// </summary>
        public Decimal? Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (weight != value)
                {
                    weight = value;
                    this.NotifyPropertyChanged(o => o.Weight);
                }
            }
        }
        #endregion

        #region 体积
        private Decimal? measurement;
        /// <summary>
        /// 体积
        /// </summary>
        public Decimal? Measurement
        {
            get
            {
                return measurement;
            }
            set
            {
                if (measurement != value)
                {
                    measurement = value;
                    this.NotifyPropertyChanged(o => o.Measurement);
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

        #region 是否放单
        bool isrelease;
        /// <summary>
        /// 是否放单
        /// </summary>
        public bool IsRelease
        {
            get
            {
                return isrelease;

            }
            set
            {
                if (isrelease != value)
                {
                    isrelease = value;
                    base.OnPropertyChanged("IsRelease", value);
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
    public partial class OceanBusinessHBLInfo : OceanBusinessHBLList
    {

    }
    #endregion

    #region MBL信息
    /// <summary>
    /// MBL列表信息
    /// </summary>
    [Serializable]
    public partial class OceanBusinessMBLList : BaseDataObject
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

        #region 船公司
        Guid? _carrierid;
        /// <summary>
        /// 船公司
        /// </summary>
        public Guid? CarrierID
        {
            get
            {
                return _carrierid;
            }
            set
            {
                if (_carrierid != value)
                {
                    _carrierid = value;
                    base.OnPropertyChanged("CarrierID", value);
                }
            }
        }

        string _carriername;
        /// <summary>
        /// 船公司名称
        /// </summary>
        public string CarrierName
        {
            get
            {
                return _carriername;
            }
            set
            {
                if (_carriername != value)
                {
                    _carriername = value;
                    base.OnPropertyChanged("CarrierName", value);
                }
            }
        }

        CustomerDescription _carrierdescription;
        /// <summary>
        /// 船公司描述
        /// </summary>
        public CustomerDescription CarrierDescription
        {
            get
            {
                return _carrierdescription;
            }
            set
            {
                if (_carrierdescription != value)
                {
                    _carrierdescription = value;
                    base.OnPropertyChanged("CarrierDescription", value);
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

        #region 驳船
        Guid? _prevoyageid;
        /// <summary>
        /// 驳船ID
        /// </summary>
        public Guid? PreVoyageID
        {
            get
            {
                return _prevoyageid;
            }
            set
            {
                if (_prevoyageid != value)
                {
                    _prevoyageid = value;
                    base.OnPropertyChanged("PreVoyageID", value);
                }
            }
        }

        string _prevoyagename;
        /// <summary>
        /// 驳船名称
        /// </summary>
        public string PreVoyageName
        {
            get
            {
                return _prevoyagename;
            }
            set
            {
                if (_prevoyagename != value)
                {
                    _prevoyagename = value;
                    base.OnPropertyChanged("PreVoyageName", value);
                }
            }
        }
        #endregion

        #region 大船
        Guid _voyageID;
        /// <summary>
        /// 大船ID
        /// </summary>
        [GuidRequired(CMessage = "大船", EMessage = "Voyage")]
        public Guid VoyageID
        {
            get
            {
                return _voyageID;
            }
            set
            {
                if (_voyageID != value)
                {
                    _voyageID = value;
                    base.OnPropertyChanged("VoyageID", value);
                }
            }
        }

        string _voyagename;
        /// <summary>
        /// 大船名称
        /// </summary>
        public string VoyageName
        {
            get
            {
                return _voyagename;
            }
            set
            {
                if (_voyagename != value)
                {
                    _voyagename = value;
                    base.OnPropertyChanged("VoyageName", value);
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

        #region 还柜地
        Guid? _returnlocationid;
        /// <summary>
        /// 还柜地ID
        /// </summary>
        public Guid? ReturnLocationID
        {
            get
            {
                return _returnlocationid;
            }
            set
            {
                if (_returnlocationid != value)
                {
                    _returnlocationid = value;
                    base.OnPropertyChanged("ReturnLocationID", value);
                }
            }
        }

        string _returnlocationname;
        /// <summary>
        /// 还柜地名称
        /// </summary>
        public string ReturnLocationName
        {
            get
            {
                return _returnlocationname;
            }
            set
            {
                if (_returnlocationname != value)
                {
                    _returnlocationname = value;
                    base.OnPropertyChanged("ReturnLocationName", value);
                }
            }
        }

        CustomerDescription _returnlocationdescription;
        /// <summary>
        /// 还柜地描述
        /// </summary>
        public CustomerDescription ReturnLocationDescription
        {
            get
            {
                return _returnlocationdescription;
            }
            set
            {
                if (_returnlocationdescription != value)
                {
                    _returnlocationdescription = value;
                    base.OnPropertyChanged("ReturnLocationDescription", value);
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

        #region 进港日

        DateTime? _gateindate;
        /// <summary>
        /// 进港日
        /// </summary>
        public DateTime? GateInDate
        {
            get
            {
                return _gateindate;
            }
            set
            {
                if (_gateindate != value)
                {
                    _gateindate = value;
                    base.OnPropertyChanged("GateInDate", value);
                }
            }
        }

        #endregion

        #region 合约ID
        Guid? _freightrateid;
        /// <summary>
        /// 合约ID
        /// </summary>
        public Guid? FreightRateID
        {
            get
            {
                return _freightrateid;
            }
            set
            {
                if (_freightrateid != value)
                {
                    _freightrateid = value;
                    base.OnPropertyChanged("FreightRateID", value);
                }
            }
        }

        #endregion

        #region 合约号
        string _contractno;
        /// <summary>
        /// 合约号
        /// </summary>
        public string ContractNo
        {
            get
            {
                return _contractno;
            }
            set
            {
                if (_contractno != value)
                {
                    _contractno = value;
                    base.OnPropertyChanged("ContractNo", value);
                }
            }
        }

        #endregion

        #region 操作备注(O.P.Notes)
        string _opnotes;
        /// <summary>
        /// 操作备注(O.P.Notes)
        /// </summary>
        public string OPNotes
        {
            get
            {
                return _opnotes;
            }
            set
            {
                if (_opnotes != value)
                {
                    _opnotes = value;
                    base.OnPropertyChanged("OPNotes", value);
                }
            }
        }
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class OceanBusinessMBLInfo : OceanBusinessMBLList
    {

    }
    #endregion

    #region 海进下载后返回结果集

    /// <summary>
    /// 海进下载后返回结果集
    /// </summary>
    [Serializable]
    public class OIAfterDownLoadRerurnData
    {
        public List<string> PODRefNoList { get; set; }
        public List<Guid> PODRefIdList { get; set; }
        public List<OceanBusinessList> BusinessList { get; set; }
    }

    #endregion
}
