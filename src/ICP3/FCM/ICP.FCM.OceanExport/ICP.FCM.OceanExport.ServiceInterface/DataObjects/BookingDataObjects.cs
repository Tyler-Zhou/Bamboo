using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects
{
    #region  OceanBookingList 订舱列表数据对象

    /// <summary>
    /// 订舱列表数据对象
    /// </summary>
    [Serializable]
    public partial class OceanBookingList : BaseDataObject
    {
        #region 基本信息
        #region 编辑模式
        /// <summary>
        /// 从订舱单列表到编辑界面时
        /// 是新增、编辑还是复制的模式
        /// </summary>
        public EditMode EditMode { get; set; }
        #endregion

        #region 是否新增
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }
        #endregion

        #region 唯一键/ID

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

        #endregion

        #region 业务号

        string _no;
        /// <summary>
        /// 业务号
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

        OEOrderState _state;
        /// <summary>
        /// 状态（0待定、1已确认、2已接受、3已打回、4已取消、5已完成）
        /// </summary>
        public OEOrderState State
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

        #endregion

        #region 公司
        Guid _companyid;
        /// <summary>
        /// 口岸公司ID
        /// </summary>
        [GuidRequired(CMessage = "公司", EMessage = "Company")]
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
        #endregion

        #region 客户

        string _customername;
        /// <summary>
        /// 客户
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

        #region 业务类型

        FCMOperationType _OEOperationType;
        /// <summary>
        /// 业务类型
        /// </summary>
        public FCMOperationType OEOperationType
        {
            get
            {
                return _OEOperationType;
            }
            set
            {
                if (_OEOperationType != value)
                {
                    _OEOperationType = value;
                    base.OnPropertyChanged("OEOperationType", value);
                }
            }
        }

        /// <summary>
        /// 业务类型描述
        /// 仅用于显示订舱单列表的时候
        /// </summary>
        public string OEOperationTypeDescription { get; set; }

        #endregion

        #region 订舱人

        string _BookingerName;
        /// <summary>
        /// 订舱人
        /// </summary>
        public string BookingerName
        {
            get
            {
                return _BookingerName;
            }
            set
            {
                if (_BookingerName != value)
                {
                    _BookingerName = value;
                    base.OnPropertyChanged("BookingerName", value);
                }
            }
        }

        #endregion

        #region 文件名称

        string _filerName;
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FilerName
        {
            get
            {
                return _filerName;
            }
            set
            {
                if (_filerName != value)
                {
                    _filerName = value;
                    base.OnPropertyChanged("FilerName", value);
                }
            }
        }

        #endregion

        #region 港后客服

        string _podContact;
        /// <summary>
        /// 港后客服
        /// </summary>
        public string PODContact
        {
            get
            {
                return _podContact;
            }
            set
            {
                if (_podContact != value)
                {
                    _podContact = value;
                    base.OnPropertyChanged("PODContact", value);
                }
            }
        }

        #endregion

        #region 接收委托单日期

        DateTime _bookingDate;
        /// <summary>
        /// 接收委托单日期
        /// </summary>
        [Required(CMessage = "接收委托单日期", EMessage = "BookingDate")]
        public DateTime BookingDate
        {
            get
            {
                return _bookingDate;
            }
            set
            {
                if (_bookingDate != value)
                {
                    _bookingDate = value;
                    base.OnPropertyChanged("BookingDate", value);
                }
            }
        }

        #endregion
        #endregion

        #region 委托信息
        #region 发货人

        string _shippername;
        /// <summary>
        /// 发货人
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

        string _consigneename;
        /// <summary>
        /// 收货人
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

        #region 通知人
        string _notifyPartyname;
        /// <summary>
        /// 通知人
        /// </summary>
        public string NotifyPartyname
        {
            get
            {
                return _notifyPartyname;
            }
            set
            {
                if (_notifyPartyname != value)
                {
                    _notifyPartyname = value;
                    base.OnPropertyChanged("NotifyPartyname", value);
                }
            }
        }
        #endregion

        #region 收货地

        string _placeofreceiptname;
        /// <summary>
        /// 收货地
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

        #region 装货港

        string _polname;
        /// <summary>
        /// 装货港
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

        string _podname;
        /// <summary>
        /// 卸货港
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

        string _placeofdeliveryname;
        /// <summary>
        /// 交货地
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
        #endregion

        #region 订舱信息
        #region 订舱号

        string _oceanshippingorderno;
        /// <summary>
        /// 订舱号
        /// </summary>
        [StringLength(CMessage = "订舱号", EMessage = "OceanShippingOrderNo")]
        public string OceanShippingOrderNo
        {
            get
            {
                return _oceanshippingorderno;
            }
            set
            {
                if (_oceanshippingorderno != value)
                {
                    _oceanshippingorderno = value;
                    base.OnPropertyChanged("OceanShippingOrderNo", value);
                }
            }
        }

        #endregion

        #region 订舱日

        DateTime? _soDate;
        /// <summary>
        /// 订舱日
        /// </summary>
        public DateTime? SODate
        {
            get
            {
                return _soDate;
            }
            set
            {
                if (_soDate != value)
                {
                    _soDate = value;
                    base.OnPropertyChanged("SODate", value);
                }
            }
        }

        #endregion

        #region 承运人

        string _agentofcarriername;
        /// <summary>
        /// 承运人
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

        #endregion

        #region 船公司

        Guid? _carrierid;
        /// <summary>
        /// 船公司ID
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

        #endregion

        #region 船公司名称

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

        #endregion

        #region 船公司简码

        private string _carrierCode;

        ///<summary>
        ///</summary>
        public string CarrierCode
        {
            get { return _carrierCode; }
            set
            {
                if (_carrierCode != value)
                {
                    _carrierCode = value;
                    base.OnPropertyChanged("Code", value);
                }
            }
        }

        #endregion

        #region 船名航次

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
        #endregion
        
        #region MBL号

        string _mblno;
        /// <summary>
        /// MBL号
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

        #region HBL号

        string _hblno;
        /// <summary>
        /// HBL号
        /// </summary>
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

        #region 箱号

        string _ContainerNo;
        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNo
        {
            get
            {
                return _ContainerNo;
            }
            set
            {
                if (_ContainerNo != value)
                {
                    _ContainerNo = value;
                    base.OnPropertyChanged("ContainerNo", value);
                }
            }
        }

        #endregion

        #region 离港日

        DateTime? _etd;
        /// <summary>
        /// 离港日
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

        #endregion

        #region 到港日

        DateTime? _eta;
        /// <summary>
        /// 到港日
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
        DateTime? _PORETD;
        /// <summary>
        /// 收货地离港日
        /// </summary>
        public DateTime? PORETD
        {
            get
            {
                return _PORETD;
            }
            set
            {
                if (_PORETD != value)
                {
                    _PORETD = value;
                    base.OnPropertyChanged("PORETD", value);
                }
            }
        }

        #endregion

        #region 截关日

        DateTime? _closingdate;
        /// <summary>
        /// 截关日
        /// </summary>
        //[Required(CMessage = "截关日", EMessage = "Closing Date")]
        public DateTime? ClosingDate
        {
            get
            {
                return _closingdate;
            }
            set
            {
                if (_closingdate != value)
                {
                    _closingdate = value;
                    base.OnPropertyChanged("ClosingDate", value);
                }
            }
        }

        #endregion

        #region 揽货人名称

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

        #region 海外部客服

        string _OverSeasFilerName;
        /// <summary>
        /// 海外部客服
        /// </summary>
        public string OverSeasFilerName
        {
            get
            {
                return _OverSeasFilerName;
            }
            set
            {
                if (_OverSeasFilerName != value)
                {
                    _OverSeasFilerName = value;
                    base.OnPropertyChanged("OverSeasFilerName", value);
                }
            }
        }

        #endregion

        #region 装货单ID

        Guid? _oceanshippingorderid;
        /// <summary>
        /// 装货单ID
        /// </summary>
        public Guid? OceanShippingOrderID
        {
            get
            {
                return _oceanshippingorderid;
            }
            set
            {
                if (_oceanshippingorderid != value)
                {
                    _oceanshippingorderid = value;
                    base.OnPropertyChanged("OceanShippingOrderID", value);
                }
            }
        }

        #endregion

        #region 订舱客户

        string _bookingcustomername;
        /// <summary>
        /// 订舱客户
        /// </summary>
        public string BookingCustomerName
        {
            get
            {
                return _bookingcustomername;
            }
            set
            {
                if (_bookingcustomername != value)
                {
                    _bookingcustomername = value;
                    base.OnPropertyChanged("BookingCustomerName", value);
                }
            }
        }

        #endregion

        #region 代理人

        string _agentname;
        /// <summary>
        /// 代理人
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

        #region 已付

        bool _paid;
        /// <summary>
        /// 已付
        /// </summary>
        public bool Paid
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

        #region 是否有效

        bool _IsValid;
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            get
            {
                return _IsValid;
            }
            set
            {
                if (_IsValid != value)
                {
                    _IsValid = value;
                    base.OnPropertyChanged("IsValid", value);
                }
            }
        }

        #endregion

        #region 订舱发货人

        string _bookingshippername;
        /// <summary>
        /// 订舱发货人
        /// </summary>
        public string BookingShipperName
        {
            get
            {
                return _bookingshippername;
            }
            set
            {
                if (_bookingshippername != value)
                {
                    _bookingshippername = value;
                    base.OnPropertyChanged("BookingShipperName", value);
                }
            }
        }

        #endregion

        #region 收货人

        string _bookingconsigneename;
        /// <summary>
        /// 订舱收货人
        /// </summary>
        public string BookingConsigneeName
        {
            get
            {
                return _bookingconsigneename;
            }
            set
            {
                if (_bookingconsigneename != value)
                {
                    _bookingconsigneename = value;
                    base.OnPropertyChanged("BookingConsigneeName", value);
                }
            }
        }

        #endregion

        #region 订舱通知人
        string _bookingnotifyPartyname;
        /// <summary>
        /// 订舱通知人
        /// </summary>
        public string BookingNotifyPartyname
        {
            get
            {
                return _bookingnotifyPartyname;
            }
            set
            {
                if (_bookingnotifyPartyname != value)
                {
                    _bookingnotifyPartyname = value;
                    base.OnPropertyChanged("BookingNotifyPartyname", value);
                }
            }
        }
        #endregion

        #region 业务下载状态
        bool _downstate;
        /// <summary>
        /// 业务下载状态
        /// </summary>
        public bool DownState
        {
            get
            {
                return _downstate;
            }
            set
            {
                if (_downstate != value)
                {
                    _downstate = value;
                    base.OnPropertyChanged("DownState", value);
                }
            }
        }

        #endregion

        #region 提单是否都已放单
        bool _isAllRBLD;
        /// <summary>
        ///提单是否都已放单
        /// </summary>
        public bool IsAllRBLD
        {
            get
            {
                return _isAllRBLD;
            }
            set
            {
                if (_isAllRBLD != value)
                {
                    _isAllRBLD = value;
                    base.OnPropertyChanged("IsAllRBLD", value);
                }
            }
        }

        #endregion

        #region 主提单列表

        List<BookingBLInfo> _oceanmbls;
        /// <summary>
        /// 主提单列表
        /// </summary>
        public List<BookingBLInfo> OceanMBLs
        {
            get
            {
                return _oceanmbls;
            }
            set
            {
                if (_oceanmbls != value)
                {
                    _oceanmbls = value;
                    base.OnPropertyChanged("OceanMBLs", value);
                }
            }
        }

        #endregion

        #region 分提单列表

        List<BookingBLInfo> _oceanhbls;
        /// <summary>
        /// 分提单列表
        /// </summary>
        public List<BookingBLInfo> OceanHBLs
        {
            get
            {
                return _oceanhbls;
            }
            set
            {
                if (_oceanhbls != value)
                {
                    _oceanhbls = value;
                    base.OnPropertyChanged("OceanHBLs", value);
                }
            }
        }

        #endregion

        #region 分提单列表 集装箱

        List<BookingContainerInfo> _BookingContainers;
        /// <summary>
        /// 分提单列表
        /// </summary>
        public List<BookingContainerInfo> BookingContainers
        {
            get
            {
                return _BookingContainers;
            }
            set
            {
                if (_BookingContainers != value)
                {
                    _BookingContainers = value;
                    base.OnPropertyChanged("BookingContainers", value);
                }
            }
        }

        #endregion

        #region 建立人

        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID
        {
            get
            {
                return _createbyid;
            }
            set
            {
                if (_createbyid != value)
                {
                    _createbyid = value;
                    base.OnPropertyChanged("CreateByID", value);
                }
            }
        }

        string _createbyname;
        /// <summary>
        /// 建立人
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
                    base.OnPropertyChanged("CreateByName", value);
                }
            }
        }

        #endregion

        #region 创建时间

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
                    base.OnPropertyChanged("CreateDate", value);
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
                return this._updateByName;
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

    #region OceanBookingInfo 订舱详细信息

    /// <summary>
    /// 订舱详细信息
    /// </summary>
    [Serializable]
    [KnownType(typeof(DryCargo))]
    [KnownType(typeof(ReeferCargo))]
    [KnownType(typeof(DangerousCargo))]
    [KnownType(typeof(AwkwardCargo))]
    public partial class OceanBookingInfo : OceanBookingList
    {
        #region 口岸公司

        string _companyname;
        /// <summary>
        /// 口岸公司名称
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

        #region 客户ID

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

        #endregion

        #region 贸易条款

        Guid _tradetermid;
        /// <summary>
        /// 贸易条款ID
        /// </summary>
        [GuidRequired(CMessage = "贸易条款", EMessage = "TradeTerm")]
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
        /// 贸易条款
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
        [GuidRequired(CMessage = "揽货方式", EMessage = "SalesType")]
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

        #region 订舱方式

        FCMBookingMode _bookingmode;
        /// <summary>
        /// 订舱方式（0电话、1邮件、2电子订舱）
        /// </summary>
        [Required(CMessage = "订舱方式", EMessage = "BookingMode")]
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

        #region 揽货人ID

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

        #endregion

        #region 揽货人部门

        Guid _salesdepartmentid;
        /// <summary>
        /// 揽货人部门ID
        /// </summary>
        [GuidRequired(CMessage = "业务所属部门", EMessage = "SalesDepartment")]
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

        #region 运输条款

        Guid _transportclauseid;
        /// <summary>
        /// 运输条款ID
        /// </summary>
        [GuidRequired(CMessage = "运输条款", EMessage = "Transport Clause")]
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

        #region 海外部客服ID

        Guid? _OverSeasFilerID;
        /// <summary>
        /// 海外部客服ID
        /// </summary>
        public Guid? OverSeasFilerID
        {
            get
            {
                return _OverSeasFilerID;
            }
            set
            {
                if (_OverSeasFilerID != value)
                {
                    _OverSeasFilerID = value;
                    base.OnPropertyChanged("OverSeasFilerID", value);
                }
            }
        }

        #endregion

        #region 客服ID

        Guid? _BookingerID;
        /// <summary>
        /// 订舱ID
        /// </summary>
        [GuidRequired(CMessage = "客服", EMessage = "Customer Service")]
        public Guid? BookingerID
        {
            get
            {
                return _BookingerID;
            }
            set
            {
                if (_BookingerID != value)
                {
                    _BookingerID = value;
                    base.OnPropertyChanged("BookingerID", value);
                }
            }
        }

        #endregion

        #region 订舱员名称
        string _BookingByName;
        /// <summary>
        /// 订舱员名称
        /// </summary>
        public string BookingByName
        {
            get
            {
                return _BookingByName;
            }
            set
            {
                if (_BookingByName != value)
                {
                    _BookingByName = value;
                    base.OnPropertyChanged("BookingByName", value);
                }
            }
        }
        #endregion

        #region 订舱ID
        Guid? _BookingByID;
        /// <summary>
        /// BookingerByName
        /// </summary>
        [GuidRequired(CMessage = "订舱员", EMessage = "BookingerID")]
        public Guid? BookingByID
        {
            get
            {
                return _BookingByID;
            }
            set
            {
                if (_BookingByID != value)
                {
                    _BookingByID = value;
                    base.OnPropertyChanged("BookingByID", value);
                }
            }
        }
        #endregion

        #region 文件ID

        Guid? _filerId;
        /// <summary>
        /// 文件ID
        /// </summary>
        public Guid? FilerId
        {
            get
            {
                return _filerId;
            }
            set
            {
                if (_filerId != value)
                {
                    _filerId = value;
                    base.OnPropertyChanged("FilerId", value);
                }
            }
        }

        #endregion

        #region 订舱客户

        Guid _bookingcustomerid;
        /// <summary>
        /// 订舱客户ID
        /// </summary>
        [GuidRequired(CMessage = "订舱客户", EMessage = "BookingCustomer")]
        public Guid BookingCustomerID
        {
            get
            {
                return _bookingcustomerid;
            }
            set
            {
                if (_bookingcustomerid != value)
                {
                    _bookingcustomerid = value;
                    base.OnPropertyChanged("BookingCustomerID", value);
                }
            }
        }


        ICP.Framework.CommonLibrary.Common.CustomerDescription _bookingcustomerdescription;
        /// <summary>
        /// 订舱客户详细信息
        /// </summary>
        public ICP.Framework.CommonLibrary.Common.CustomerDescription BookingCustomerDescription
        {
            get
            {
                return _bookingcustomerdescription;
            }
            set
            {
                if (_bookingcustomerdescription != value)
                {
                    _bookingcustomerdescription = value;
                    base.OnPropertyChanged("BookingCustomerDescription", value);
                }
            }
        }

        #endregion

        #region 发货人

        Guid? _shipperid;
        /// <summary>
        /// 发货人ID
        /// </summary>
        [GuidRequired(CMessage = "发货人", EMessage = "Shipper")]
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


        ICP.Framework.CommonLibrary.Common.CustomerDescription _shipperdescription;
        /// <summary>
        /// 发货人详细信息
        /// </summary>
        public ICP.Framework.CommonLibrary.Common.CustomerDescription ShipperDescription
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

        Guid? _consigneeid;
        /// <summary>
        /// 收货人ID
        /// </summary>
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


        ICP.Framework.CommonLibrary.Common.CustomerDescription _consigneedescription;
        /// <summary>
        /// 收货人详细信息
        /// </summary>
        public ICP.Framework.CommonLibrary.Common.CustomerDescription ConsigneeDescription
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

        #region 收货地ID

        Guid? _placeofreceiptid;
        /// <summary>
        /// 收货地ID
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

        #endregion

        #region 收货地地址

        string _placeofreceiptaddress;
        /// <summary>
        /// 收货地地址
        /// </summary>

        public string PlaceOfReceiptAddress
        {
            get
            {
                return _placeofreceiptaddress;
            }
            set
            {
                if (_placeofreceiptaddress != value)
                {
                    _placeofreceiptaddress = value;
                    base.OnPropertyChanged("PlaceOfReceiptAddress", value);
                }
            }
        }

        #endregion

        #region 装货港ID

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

        #endregion

        #region 卸货港ID

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

        #endregion

        #region 交货地ID

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

        #endregion

        #region 交货地地址

        string _placeofdeliveryaddress;
        /// <summary>
        /// 交货地地址
        /// </summary>
        public string PlaceOfDeliveryAddress
        {
            get
            {
                return _placeofdeliveryaddress;
            }
            set
            {
                if (_placeofdeliveryaddress != value)
                {
                    _placeofdeliveryaddress = value;
                    base.OnPropertyChanged("PlaceOfDeliveryAddress", value);
                }
            }
        }

        #endregion

        #region 最终目的地

        Guid? _FinalDestinationID;
        /// <summary>
        /// 最终目的地ID
        /// </summary>
        public Guid? FinalDestinationID
        {
            get
            {
                return _FinalDestinationID;
            }
            set
            {
                if (_FinalDestinationID != value)
                {
                    _FinalDestinationID = value;
                    base.OnPropertyChanged("FinalDestinationID", value);
                }
            }
        }

        string _FinalDestinationName;
        /// <summary>
        /// 最终目的地名称
        /// </summary>
        public string FinalDestinationName
        {
            get
            {
                return _FinalDestinationName;
            }
            set
            {
                if (_FinalDestinationName != value)
                {
                    _FinalDestinationName = value;
                    base.OnPropertyChanged("FinalDestinationName", value);
                }
            }
        }

        #endregion

        #region 品名

        string _commodity;
        /// <summary>
        /// 品名
        /// </summary>
        [StringLength(MaximumLength = 400, CMessage = "品名", EMessage = "Commdity")]

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

        #region 包装数量

        int _quantity;
        /// <summary>
        /// 包装数量
        /// </summary>
        [Required(CMessage = "包装数量", EMessage = "Quantity")]
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

        #endregion

        #region 包装单位

        Guid? _quantityunitid;
        /// <summary>
        /// 包装单位ID
        /// </summary>
        public Guid? QuantityUnitID
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
        /// 包装单位名称
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

        #endregion

        #region 重量

        decimal _weight;
        /// <summary>
        /// 重量
        /// </summary>
        [Required(CMessage = "重量", EMessage = "Weight")]
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


        Guid? _weightunitid;
        /// <summary>
        /// 重量单位ID
        /// </summary>
        public Guid? WeightUnitID
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
                if (_weightunitname != value)
                {
                    _weightunitname = value;
                    base.OnPropertyChanged("WeightUnitName", value);
                }
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


        Guid? _measurementunitid;
        /// <summary>
        /// 体积单位ID
        /// </summary>
        public Guid? MeasurementUnitID
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

        #endregion

        #region 货物类型

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
            //set
            //{
            //    if (_cargotype != value)
            //    {
            //        _cargotype = value;
            //        base.OnPropertyChanged("CargoType", value);
            //    }
            //}
        }

        #endregion

        #region 货物描述

        CargoDescription _cargodescription;
        /// <summary>
        /// 货物描述
        /// </summary>
        public CargoDescription CargoDescription
        {
            get
            {
                return _cargodescription;
            }
            set
            {
                if (_cargodescription != value)
                {
                    _cargodescription = value;
                    base.OnPropertyChanged("CargoDescription", value);
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

        #region 本地服务

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


        bool _iswarehouse;
        /// <summary>
        /// 是否仓储
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


        bool _iscommodityinspection;
        /// <summary>
        /// 是否商检
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


        bool _isquarantineinspection;
        /// <summary>
        /// 是否动植检
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

        #region 代理

        Guid? _agentid;
        /// <summary>
        /// 代理ID
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


        ICP.Framework.CommonLibrary.Common.CustomerDescription _agentdescription;
        /// <summary>
        /// 代理详细信息
        /// </summary>
        public ICP.Framework.CommonLibrary.Common.CustomerDescription AgentDescription
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

        #region 承运人

        Guid _agentofcarrierid;
        /// <summary>
        /// 承运人ID
        /// </summary>
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


        Guid? _contractid;
        /// <summary>
        /// 合约号ID
        /// </summary>
        public Guid? ContractID
        {
            get
            {
                return _contractid;
            }
            set
            {
                if (_contractid != value)
                {
                    _contractid = value;
                    base.OnPropertyChanged("ContractID", value);
                }
            }
        }

        #endregion

        #region 合约名

        string _contractname;
        /// <summary>
        /// 合约名
        /// </summary>
        public string ContractName
        {
            get
            {
                return _contractname;
            }
            set
            {
                if (_contractname != value)
                {
                    _contractname = value;
                    base.OnPropertyChanged("ContractName", value);
                }
            }
        }
        #endregion

        #region ItemCode

        string _itemcode;
        /// <summary>
        /// ItemCode
        /// </summary>
        public string ItemCode
        {
            get
            {
                return _itemcode;
            }
            set
            {
                if (_itemcode != value)
                {
                    _itemcode = value;
                    base.OnPropertyChanged("ItemCode", value);
                }
            }
        }
        #endregion

        #region 是否有合约

        bool _iscontract;
        /// <summary>
        /// 是否有合约
        /// </summary>
        public bool IsContract
        {
            get
            {
                return _iscontract;
            }
            set
            {
                if (_iscontract != value)
                {
                    _iscontract = value;
                    base.OnPropertyChanged("IsContract", value);
                }
            }
        }

        #endregion

        #region 是否只出MBL

        bool _isonlymbl;
        /// <summary>
        /// 是否只出MBL
        /// </summary>
        public bool IsOnlyMBL
        {
            get
            {
                return _isonlymbl;
            }
            set
            {
                if (_isonlymbl != value)
                {
                    _isonlymbl = value;
                    base.OnPropertyChanged("IsOnlyMBL", value);
                }
            }
        }

        #endregion

        #region 驳船

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

        #endregion

        #region 大船

        Guid? _voyageid;
        /// <summary>
        /// 大船ID
        /// </summary>
        public Guid? VoyageID
        {
            get
            {
                return _voyageid;
            }
            set
            {
                if (_voyageid != value)
                {
                    _voyageid = value;
                    base.OnPropertyChanged("VoyageID", value);
                }
            }
        }


        string _voyagename;
        /// <summary>
        /// 二程航次名称
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

        #region 航线

        Guid? _shippinglineid;
        /// <summary>
        /// 航线ID
        /// </summary>
        //[GuidRequired(CMessage = "航线", EMessage = "ShippingLine")]
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
                    base.OnPropertyChanged("ShippingLineID", value);
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

        #region 截柜日

        DateTime? _cyclosingdate;
        /// <summary>
        /// 截柜日
        /// </summary>
        public DateTime? CYClosingDate
        {
            get
            {
                return _cyclosingdate;
            }
            set
            {
                if (_cyclosingdate != value)
                {
                    _cyclosingdate = value;
                    base.OnPropertyChanged("CYClosingDate", value);
                }
            }
        }

        #endregion

        #region 截VGM日

        DateTime? _vgmcutoffdate;
        /// <summary>
        /// 截VMG日
        /// </summary>
        public DateTime? VGMCutOffDate
        {
            get
            {
                return _vgmcutoffdate;
            }
            set
            {
                if (_vgmcutoffdate != value)
                {
                    _vgmcutoffdate = value;
                    base.OnPropertyChanged("VGMCutOffDate", value);
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

        #region 截文件日

        DateTime? _docclosingdate;
        /// <summary>
        /// 截文件日
        /// </summary>
        public DateTime? DOCClosingDate
        {
            get
            {
                return _docclosingdate;
            }
            set
            {
                if (_docclosingdate != value)
                {
                    _docclosingdate = value;
                    base.OnPropertyChanged("DOCClosingDate", value);
                }
            }
        }

        #endregion

        #region 截仓日

        DateTime? _closingWarehousedate;
        /// <summary>
        /// 截仓日
        /// </summary>
        public DateTime? ClosingWarehousedate
        {
            get
            {
                return _closingWarehousedate;
            }
            set
            {
                if (_closingWarehousedate != value)
                {
                    _closingWarehousedate = value;
                    base.OnPropertyChanged("ClosingWarehousedate", value);
                }
            }
        }

        #endregion

        #region 仓库/ 提柜地点

        Guid? _WarehouseID;
        /// <summary>
        /// 仓库ID
        /// </summary>
        public Guid? WarehouseID
        {
            get
            {
                return _WarehouseID;
            }
            set
            {
                if (_WarehouseID != value)
                {
                    _WarehouseID = value;
                    base.OnPropertyChanged("WarehouseID", value);
                }
            }
        }

        string _WarehouseName;
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WarehouseName
        {
            get
            {
                return _WarehouseName;
            }
            set
            {
                if (_WarehouseName != value)
                {
                    _WarehouseName = value;
                    base.OnPropertyChanged("WarehouseName", value);
                }
            }
        }

        DateTime? _PickupEarliestDate;
        /// <summary>
        /// 最早提柜时间
        /// </summary>
        public DateTime? PickupEarliestDate
        {
            get
            {
                return _PickupEarliestDate;
            }
            set
            {
                if (_PickupEarliestDate != value)
                {
                    _PickupEarliestDate = value;
                    base.OnPropertyChanged("PickupEarliestDate", value);
                }
            }

        }

        #endregion

        #region 还柜地点

        Guid? _ReturnLocationID;
        /// <summary>
        /// 还柜地点ID
        /// </summary>
        public Guid? ReturnLocationID
        {
            get
            {
                return _ReturnLocationID;
            }
            set
            {
                if (_ReturnLocationID != value)
                {
                    _ReturnLocationID = value;
                    base.OnPropertyChanged("ReturnLocationID", value);
                }
            }
        }

        string _ReturnLocationName;
        /// <summary>
        /// 还柜地点名称
        /// </summary>
        public string ReturnLocationName
        {
            get
            {
                return _ReturnLocationName;
            }
            set
            {
                if (_ReturnLocationName != value)
                {
                    _ReturnLocationName = value;
                    base.OnPropertyChanged("ReturnLocationName", value);
                }
            }
        }

        #endregion

        #region 估计客户交货时间

        DateTime? _estimateddeliverydate;
        /// <summary>
        /// 估计客户交货时间
        /// </summary>
        public DateTime? EstimatedDeliveryDate
        {
            get
            {
                return _estimateddeliverydate;
            }
            set
            {
                if (_estimateddeliverydate != value)
                {
                    _estimateddeliverydate = value;
                    base.OnPropertyChanged("EstimatedDeliveryDate", value);
                }
            }
        }

        #endregion

        #region 客户实际交货日期

        DateTime? _deliverydate;
        /// <summary>
        /// 客户实际交货日期
        /// </summary>
        public DateTime? DeliveryDate
        {
            get
            {
                return _deliverydate;
            }
            set
            {
                if (_deliverydate != value)
                {
                    _deliverydate = value;
                    base.OnPropertyChanged("DeliveryDate", value);
                }
            }
        }

        #endregion

        #region 期望出运时间

        DateTime? _expectedshipdate;
        /// <summary>
        /// 期望出运时间
        /// </summary>
        public DateTime? ExpectedShipDate
        {
            get
            {
                return _expectedshipdate;
            }
            set
            {
                if (_expectedshipdate != value)
                {
                    _expectedshipdate = value;
                    base.OnPropertyChanged("ExpectedShipDate", value);
                }
            }
        }

        #endregion

        #region 期望到达时间

        DateTime? _expectedarrivedate;
        /// <summary>
        /// 期望到达时间
        /// </summary>
        public DateTime? ExpectedArriveDate
        {
            get
            {
                return _expectedarrivedate;
            }
            set
            {
                if (_expectedarrivedate != value)
                {
                    _expectedarrivedate = value;
                    base.OnPropertyChanged("ExpectedArriveDate", value);
                }
            }
        }

        #endregion

        #region Master B/L付费条款

        Guid? _mblpaymenttermid;
        /// <summary>
        /// Master B/L付费条款ID
        /// </summary>
        public Guid? MBLPaymentTermID
        {
            get
            {
                return _mblpaymenttermid;
            }
            set
            {
                if (_mblpaymenttermid != value)
                {
                    _mblpaymenttermid = value;
                    base.OnPropertyChanged("MBLPaymentTermID", value);
                }
            }
        }

        string _mblpaymenttermname;
        /// <summary>
        /// Master B/L付费条款名称
        /// </summary>
        public string MBLPaymentTermName
        {
            get
            {
                return _mblpaymenttermname;
            }
            set
            {
                if (_mblpaymenttermname != value)
                {
                    _mblpaymenttermname = value;
                    base.OnPropertyChanged("MBLPaymentTermName", value);
                }
            }
        }

        #endregion

        #region Master B/L放单方式（0正本，1电放）

        FCMReleaseType? _mblreleasetype;
        /// <summary>
        /// Master B/L放单方式（0正本，1电放）
        /// </summary>
        public FCMReleaseType? MBLReleaseType
        {
            get
            {
                return _mblreleasetype;
            }
            set
            {
                if (_mblreleasetype != value)
                {
                    _mblreleasetype = value;
                    base.OnPropertyChanged("MBLReleaseType", value);
                }
            }
        }

        #endregion

        #region Master B/L出单要求

        string _mblrequirements;
        /// <summary>
        /// Master B/L出单要求
        /// </summary>
        [StringLength(MaximumLength = 1000, CMessage = "B/L出单要求", EMessage = "MBLRequierMent")]
        public string MBLRequirements
        {
            get
            {
                return _mblrequirements;
            }
            set
            {
                if (_mblrequirements != value)
                {
                    _mblrequirements = value;
                    base.OnPropertyChanged("MBLRequirements", value);
                }
            }
        }

        #endregion

        #region House B/L付费条款

        Guid? _hblpaymenttermid;
        /// <summary>
        /// House B/L付费条款ID
        /// </summary>
        public Guid? HBLPaymentTermID
        {
            get
            {
                return _hblpaymenttermid;
            }
            set
            {
                if (_hblpaymenttermid != value)
                {
                    _hblpaymenttermid = value;
                    base.OnPropertyChanged("HBLPaymentTermID", value);
                }
            }
        }


        string _hblpaymenttermname;
        /// <summary>
        /// House B/L付费条款名称
        /// </summary>
        public string HBLPaymentTermName
        {
            get
            {
                return _hblpaymenttermname;
            }
            set
            {
                if (_hblpaymenttermname != value)
                {
                    _hblpaymenttermname = value;
                    base.OnPropertyChanged("HBLPaymentTermName", value);
                }
            }
        }

        #endregion

        #region House B/L放单方式（0正本，1电放）

        FCMReleaseType? _hblreleasetype;
        /// <summary>
        /// House B/L放单方式（0正本，1电放）
        /// </summary>
        public FCMReleaseType? HBLReleaseType
        {
            get
            {
                return _hblreleasetype;
            }
            set
            {
                if (_hblreleasetype != value)
                {
                    _hblreleasetype = value;
                    base.OnPropertyChanged("HBLReleaseType", value);
                }
            }
        }

        #endregion

        #region House B/L出单要求

        string _hblrequirements;
        /// <summary>
        /// House B/L出单要求
        /// </summary>
        [StringLength(MaximumLength = 1000, CMessage = "B/L出单要求", EMessage = "HBLRequireMent")]
        public string HBLRequirements
        {
            get
            {
                return _hblrequirements;
            }
            set
            {
                if (_hblrequirements != value)
                {
                    _hblrequirements = value;
                    base.OnPropertyChanged("HBLRequirements", value);
                }
            }
        }

        #endregion

        #region 备注

        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaximumLength = 1000, CMessage = "备注", EMessage = "Remark")]
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

        #region 装货单数据版本

        DateTime? _oceanShippingOrderUpdateDate;
        /// <summary>
        /// 装货单数据版本
        /// </summary>
        public DateTime? OceanShippingOrderUpdateDate
        {
            get
            {
                return _oceanShippingOrderUpdateDate;
            }
            set
            {
                if (_oceanShippingOrderUpdateDate != value)
                {
                    _oceanShippingOrderUpdateDate = value;
                    base.OnPropertyChanged("OceanShippingOrderUpdateDate", value);
                }
            }
        }

        #endregion

        #region 是否需要生成账单
        /// <summary>
        /// 是否需要生成账单(有箱有提单的时候才会需要生成)
        /// </summary>
        public bool IsCreateBill
        {
            get
            {
                if (((OceanMBLs != null && OceanMBLs.Count > 0) || (OceanHBLs != null && OceanHBLs.Count > 0)) && (BookingContainers != null && BookingContainers.Count > 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region 提单抬头
        /// <summary>
        /// 抬头NAME
        /// </summary>
        public string BLTitleName { get; set; }

        /// <summary>
        /// 提单抬头
        /// </summary>
        Guid? _bLTitleID;
        public Guid? BLTitleID
        {
            get
            {
                return _bLTitleID;
            }
            set
            {
                if (_bLTitleID != value)
                {
                    _bLTitleID = value;
                    base.OnPropertyChanged("BLTitleID", value);
                }
            }
        }
        #endregion

        #region  订单号是否为邮件发送判断条件
        /// <summary>
        /// 订单号是否为邮件发送判断条件
        /// </summary>
        public bool NoCheck { get; set; }
        #endregion

        #region 截关日是否为邮件发送判断条件
        /// <summary>
        /// 截关日是否为邮件发送判断条件
        /// </summary>
        public bool ClosingDateCheck { get; set; }
        #endregion

        #region 箱信息是否为邮件发送判断条件
        /// <summary>
        /// 箱信息是否为邮件发送判断条件
        /// </summary>
        public bool ContainerCheck { get; set; }
        #endregion

        #region 装货港是否为邮件发送判断条件
        /// <summary>
        /// 装货港是否为邮件发送判断条件
        /// </summary>
        public bool POLCheck { get; set; }
        #endregion

        #region 卸货港是否为邮件发送判断条件
        /// <summary>
        /// 卸货港是否为邮件发送判断条件
        /// </summary>
        public bool PODCheck { get; set; }
        #endregion

        #region 揽货人是否为邮件发送判断条件
        /// <summary>
        /// 揽货人是否为邮件发送判断条件
        /// </summary>
        public bool SalesNameCheck { get; set; }
        #endregion

        #region 客户信息是否为邮件发送判断条件
        /// <summary>
        /// 客户信息是否为邮件发送判断条件
        /// </summary>
        public bool CustomerCheck { get; set; }
        #endregion

        #region 订舱员信息是否为邮件发送判断条件

        public bool BookingByCheck { get; set; }
        #endregion

        #region 是否第三地付款
        bool _IsThirdPlacePay;
        /// <summary>
        /// 是否第三地付款
        /// </summary>
        public bool IsThirdPlacePay
        {
            get
            {
                return _IsThirdPlacePay;
            }
            set
            {
                if (_IsThirdPlacePay != value)
                {
                    _IsThirdPlacePay = value;
                    base.OnPropertyChanged("IsThirdPlacePay", value);
                }
            }
        }

        #endregion

        #region 第三付款地名称
        string _CollectbyAgentName;
        /// <summary>
        /// 第三付款地
        /// </summary>
        public string CollectbyAgentName
        {
            get
            {
                return _CollectbyAgentName;
            }
            set
            {
                if (_CollectbyAgentName != value)
                {
                    _CollectbyAgentName = value;
                    base.OnPropertyChanged("CollectbyAgentName", value);
                }
            }
        }
        #endregion

        #region 第三付款地ID
        Guid? _CollectbyAgentID;
        /// <summary>
        /// 第三付款地ID
        /// </summary>
        public Guid? CollectbyAgentID
        {
            get
            {
                return _CollectbyAgentID;
            }
            set
            {
                if (_CollectbyAgentID != value)
                {
                    _CollectbyAgentID = value;
                    base.OnPropertyChanged("CollectbyAgentID", value);
                }
            }
        }
        #endregion

        #region MyRegion  订舱人
        Guid? _bookingPartyID;
        /// <summary>
        /// 订舱人
        /// </summary>
        //[GuidRequired(CMessage = "订舱人", EMessage = "BookingPartyID")]
        public Guid? BookingPartyID
        {
            get
            {
                return _bookingPartyID;
            }
            set
            {
                if (_bookingPartyID != value)
                {
                    _bookingPartyID = value;
                    base.OnPropertyChanged("BookingPartyID", value);
                }
            }
        }


        string _bookingPartyName;
        /// <summary>
        /// 订舱人Name
        /// </summary>
        public string BookingPartyName
        {
            get
            {
                return _bookingPartyName;
            }
            set
            {
                if (_bookingPartyName != value)
                {
                    _bookingPartyName = value;
                    base.OnPropertyChanged("BookingPartyName", value);
                }
            }
        }



        #endregion

        #region ScacCode
        String _scacCode;
        public String ScacCode
        {
            get
            {
                return _scacCode;

            }
            set
            {
                if (_scacCode != value)
                {
                    _scacCode = value;
                    base.OnPropertyChanged("ScacCode", value);
                }
            }
        }
        #endregion

        #region 订舱发货人
        Guid? _bookingShipperID;
        /// <summary>
        /// 订舱发货人ID
        /// </summary>
        //[GuidRequired(CMessage = "订舱发货人", EMessage = "BookingShipper")]
        public Guid? BookingShipperID
        {
            get
            {
                return _bookingShipperID;
            }
            set
            {
                if (_bookingShipperID != value)
                {
                    _bookingShipperID = value;
                    base.OnPropertyChanged("BookingShipperID", value);
                }
            }
        }


        ICP.Framework.CommonLibrary.Common.CustomerDescription _bookingShipperdescription;
        /// <summary>
        /// 订舱发货人详细信息
        /// </summary>
        public ICP.Framework.CommonLibrary.Common.CustomerDescription BookingShipperdescription
        {
            get
            {
                return _bookingShipperdescription;
            }
            set
            {
                if (_bookingShipperdescription != value)
                {
                    _bookingShipperdescription = value;
                    base.OnPropertyChanged("BookingShipperdescription", value);
                }
            }
        }
        #endregion

        #region 订舱收货人ID

        Guid? _bookingConsigneeID;
        /// <summary>
        /// 订舱收货人ID
        /// </summary>
        //[GuidRequired(CMessage = "订舱收货人", EMessage = "BookingConsignee")]
        public Guid? BookingConsigneeID
        {
            get
            {
                return _bookingConsigneeID;
            }
            set
            {
                if (_bookingConsigneeID != value)
                {
                    _bookingConsigneeID = value;
                    base.OnPropertyChanged("BookingConsigneeID", value);
                }
            }
        }


        ICP.Framework.CommonLibrary.Common.CustomerDescription _bookingConsigneedescription;
        /// <summary>
        /// 订舱收货人详细信息
        /// </summary>
        public ICP.Framework.CommonLibrary.Common.CustomerDescription BookingConsigneedescription
        {
            get
            {
                return _bookingConsigneedescription;
            }
            set
            {
                if (_bookingConsigneedescription != value)
                {
                    _bookingConsigneedescription = value;
                    base.OnPropertyChanged("BookingConsigneedescription", value);
                }
            }
        }
        #endregion

        #region 订舱通知人
        Guid? _bookingNotifyPartyID;
        /// <summary>
        /// 订舱通知人ID
        /// </summary>
        //[GuidRequired(CMessage = "订舱通知人", EMessage = "BookingNotifyParty")]
        public Guid? BookingNotifyPartyID
        {
            get
            {
                return _bookingNotifyPartyID;
            }
            set
            {
                if (_bookingNotifyPartyID != value)
                {
                    _bookingNotifyPartyID = value;
                    base.OnPropertyChanged("BookingNotifyPartyID", value);
                }
            }
        }


        ICP.Framework.CommonLibrary.Common.CustomerDescription _bookingNotifyPartydescription;
        /// <summary>
        /// 订舱发货人详细信息
        /// </summary>
        public ICP.Framework.CommonLibrary.Common.CustomerDescription BookingNotifyPartydescription
        {
            get
            {
                return _bookingNotifyPartydescription;
            }
            set
            {
                if (_bookingNotifyPartydescription != value)
                {
                    _bookingNotifyPartydescription = value;
                    base.OnPropertyChanged("BookingShipperdescription", value);
                }
            }
        }

        #endregion

        #region 提箱要求
        string _pickupRequirement;
        /// <summary>
        /// 提箱要求
        /// </summary>
        [StringLength(MaximumLength = 1000, CMessage = "提箱要求", EMessage = "PickupRequirement")]
        public string PickupRequirement
        {
            get
            {
                return _pickupRequirement;
            }
            set
            {
                if (_pickupRequirement != value)
                {
                    _pickupRequirement = value;
                    base.OnPropertyChanged("PickupRequirement", value);
                }
            }
        }
        #endregion

        #region 订舱说明
        string _bookingExplanation;
        /// <summary>
        /// 订舱说明
        /// </summary>
        [StringLength(MaximumLength = 1000, CMessage = "订舱说明", EMessage = "BookingExplanation")]
        public string BookingExplanation
        {
            get
            {
                return _bookingExplanation;
            }
            set
            {
                if (_bookingExplanation != value)
                {
                    _bookingExplanation = value;
                    base.OnPropertyChanged("BookingExplanation", value);
                }
            }
        }
        #endregion

        #region 是否保险
        bool _isInsurance;
        /// <summary>
        /// 是否保险
        /// </summary>
        public bool IsInsurance
        {
            get
            {
                return _isInsurance;
            }
            set
            {
                if (_isInsurance != value)
                {
                    _isInsurance = value;
                    base.OnPropertyChanged("IsInsurance", value);
                }
            }
        }
        #endregion

        #region 是否熏蒸
        bool _isFumigation;
        /// <summary>
        /// 是否熏蒸
        /// </summary>
        public bool IsFumigation
        {
            get
            {
                return _isFumigation;
            }
            set
            {
                if (_isFumigation != value)
                {
                    _isFumigation = value;
                    base.OnPropertyChanged("IsFumigation", value);
                }
            }
        }
        #endregion

        #region 是否木质包装
        bool _isWoodPacking;
        /// <summary>
        /// 是否熏蒸
        /// </summary>
        public bool IsWoodPacking
        {
            get
            {
                return _isWoodPacking;
            }
            set
            {
                if (_isWoodPacking != value)
                {
                    _isWoodPacking = value;
                    base.OnPropertyChanged("IsWoodPacking", value);
                }
            }
        }



        #endregion

        #region 唛头
        string _marks;
        /// <summary>
        /// 订舱说明
        /// </summary>
        [StringLength(MaximumLength = 1000, CMessage = "唛头", EMessage = "Marks")]
        public string Marks
        {
            get
            {
                return _marks;
            }
            set
            {
                if (_marks != value)
                {
                    _marks = value;
                    base.OnPropertyChanged("Marks", value);
                }
            }
        }
        #endregion

        #region MBL运输条款
        Guid? _mBLTransportClauseID;
        /// <summary>
        /// 运输条款ID
        /// </summary>
        //[GuidRequired(CMessage = "MBL运输条款", EMessage = "MBLTransportClause")]
        public Guid? MBLTransportClauseID
        {
            get
            {
                return _mBLTransportClauseID;
            }
            set
            {
                if (_mBLTransportClauseID != value)
                {
                    _mBLTransportClauseID = value;
                    base.OnPropertyChanged("MBLTransportClauseID", value);
                }
            }
        }

        #endregion

        #region MBLTransportClauseName
        string _mbltransportclausename;
        /// <summary>
        /// mbl运输条款名称
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

        #region 承运人代发AMS
        bool _isCarrierSendAMS;
        /// <summary>
        /// 是否承运人代发AMS
        /// </summary>
        public bool IsCarrierSendAMS
        {
            get
            {
                return _isCarrierSendAMS;
            }
            set
            {
                if (_isCarrierSendAMS != value)
                {
                    _isCarrierSendAMS = value;
                    base.OnPropertyChanged("IsCarrierSendAMS", value);
                }
            }
        }
        #endregion

        #region 截AMS日

        DateTime? _amsclosingdate;
        /// <summary>
        /// 截AMS
        /// </summary>
        public DateTime? AMSClosingDate
        {
            get
            {
                return _amsclosingdate;
            }
            set
            {
                if (_amsclosingdate != value)
                {
                    _amsclosingdate = value;
                    base.OnPropertyChanged("AMSClosingDate", value);
                }
            }
        }

        #endregion

        #region 通知人
        Guid? _notifyPartyID;
        /// <summary>
        /// 通知人ID
        /// </summary>
        //[GuidRequired(CMessage = "通知人", EMessage = "NotifyParty")]
        public Guid? NotifyPartyID
        {
            get
            {
                return _notifyPartyID;
            }
            set
            {
                if (_notifyPartyID != value)
                {
                    _notifyPartyID = value;
                    base.OnPropertyChanged("NotifyPartyID", value);
                }
            }
        }

        ICP.Framework.CommonLibrary.Common.CustomerDescription _notifyPartydescription;
        /// <summary>
        /// 通知人详细信息
        /// </summary>
        public ICP.Framework.CommonLibrary.Common.CustomerDescription NotifyPartydescription
        {
            get
            {
                return _notifyPartydescription;
            }
            set
            {
                if (_notifyPartydescription != value)
                {
                    _notifyPartydescription = value;
                    base.OnPropertyChanged("NotifyPartydescription", value);
                }
            }
        }

        #endregion

        #region ShippingOrders是否第三地付款
        bool _IsThirdPlacePayOrder;
        /// <summary>
        /// ShippingOrder是否第三地付款
        /// </summary>
        public bool IsThirdPlacePayOrder
        {
            get
            {
                return _IsThirdPlacePayOrder;
            }
            set
            {
                if (_IsThirdPlacePayOrder != value)
                {
                    _IsThirdPlacePayOrder = value;
                    base.OnPropertyChanged("IsThirdPlacePayOrder", value);
                }
            }
        }

        #endregion

        #region ShippingOrders第三付款地名称
        string _CollectbyAgentNameOrder;
        /// <summary>
        /// 第三付款地
        /// </summary>
        public string CollectbyAgentNameOrder
        {
            get
            {
                return _CollectbyAgentNameOrder;
            }
            set
            {
                if (_CollectbyAgentNameOrder != value)
                {
                    _CollectbyAgentNameOrder = value;
                    base.OnPropertyChanged("CollectbyAgentNameOrder", value);
                }
            }
        }
        #endregion

        #region ShippingOrder第三付款地ID
        Guid? _CollectbyAgentOrderID;
        /// <summary>
        /// ShippingOrder第三付款地ID
        /// </summary>
        public Guid? CollectbyAgentOrderID
        {
            get
            {
                return _CollectbyAgentOrderID;
            }
            set
            {
                if (_CollectbyAgentOrderID != value)
                {
                    _CollectbyAgentOrderID = value;
                    base.OnPropertyChanged("CollectbyAgentOrderID", value);
                }
            }
        }
        #endregion

        #region 截铁路日
        DateTime? _railCutOff;
        /// <summary>
        /// 截铁路日
        /// </summary>
        public DateTime? RailCutOff
        {
            get
            {
                return _railCutOff;
            }
            set
            {
                if (_railCutOff != value)
                {
                    _railCutOff = value;
                    base.OnPropertyChanged("RailCutOff", value);
                }
            }
        }
        #endregion

        #region BookingRefNo
        string _bookingRefNo;
        /// <summary>
        /// 参考号
        /// </summary>
        public string BookingRefNo
        {
            get
            {
                return _bookingRefNo;
            }
            set
            {
                if (_bookingRefNo != value)
                {
                    _bookingRefNo = value;
                    base.OnPropertyChanged("BookingRefNo", value);
                }
            }
        }
        #endregion

        #region 是否灵活提柜
        bool _okToSub;
        /// <summary>
        /// 是否灵活提柜
        /// </summary>
        public bool OkToSub
        {
            get
            {
                return _okToSub;
            }
            set
            {
                if (_okToSub != value)
                {
                    _okToSub = value;
                    base.OnPropertyChanged("OkToSub", value);
                }
            }
        }
        #endregion

        #region 清关单号

        /// <summary>
        /// 清关单号
        /// </summary>
        string _cusClearanceNo;

        public string CusClearanceNo
        {
            get
            {
                return _cusClearanceNo;
            }
            set
            {
                if (_cusClearanceNo != value)
                {
                    _cusClearanceNo = value;
                    base.OnPropertyChanged("CusClearanceNo", value);
                }
            }
        }

        #endregion

        #region 运费已包含

        public string FreightIncludedids
        {
            get;
            set;
        }

        public string FreightIncludedString
        {
            get;
            set;
        }

        #endregion

        #region 业务日期
        DateTime? _operationdate;
        /// <summary>
        /// 业务日期
        /// </summary>
        public DateTime? OperationDate
        {
            get
            {
                return _operationdate;
            }
            set
            {
                if (_operationdate != value)
                {
                    _operationdate = value;
                    base.OnPropertyChanged("OperationDate", value);
                }
            }
        }
        #endregion

        #region HSCode(PIL EDI发送SI需要)
        string _HSCode;
        /// <summary>
        /// HSCode(PIL EDI发送SI需要)
        /// </summary>
        public string HSCode
        {
            get
            {
                return _HSCode;
            }
            set
            {
                if (_HSCode != value)
                {
                    _HSCode = value;
                    base.OnPropertyChanged("HSCode", value);
                }
            }
        }
        #endregion

        #region 询价面板实体类
        InquirePricePartInfo _inquirePricePartInfo;
        /// <summary>
        /// 询价面板实体类
        /// </summary>
        public InquirePricePartInfo InquirePricePartInfo
        {
            get
            {
                return _inquirePricePartInfo;
            }
            set
            {
                if (_inquirePricePartInfo != value)
                {
                    _inquirePricePartInfo = value;
                    base.OnPropertyChanged("InquirePricePartInfo", value);
                }
            }
        }

        #endregion

        #region 报价信息
        QuotedPricePartInfo _QuotedPricePartInfo;
        /// <summary>
        /// 报价面板实体类
        /// </summary>
        public QuotedPricePartInfo QuotedPricePartInfo
        {
            get
            {
                return _QuotedPricePartInfo;
            }
            set
            {
                if (_QuotedPricePartInfo != value)
                {
                    _QuotedPricePartInfo = value;
                    base.OnPropertyChanged("QuotedPricePartInfo", value);
                }
            }
        }

        #endregion
    }



    #endregion

    #region ANL配置信息
    /// <summary>
    ///  ANL订舱信息
    /// </summary>
    [Serializable]
    [Obsolete("是否无效待定")]
    public class NBEDIANLBOOKINGObj : OceanBookingInfo
    {
        #region 预约号

        string _bookno;
        /// <summary>
        /// 预约号
        /// </summary>
        public string BookNo
        {
            get
            {
                return _bookno;
            }
            set
            {
                if (_bookno != value)
                {
                    _bookno = value;
                    base.OnPropertyChanged("BookNo", value);
                }
            }
        }

        #endregion

        #region 箱

        string _container;
        /// <summary>
        /// 箱
        /// </summary>
        public string Container
        {
            get
            {
                return _container;
            }
            set
            {
                if (_container != value)
                {
                    _container = value;
                    base.OnPropertyChanged("Container", value);
                }
            }
        }

        #endregion

        #region 封条号

        string _sealno;
        /// <summary>
        /// 封条号
        /// </summary>
        public string SealNo
        {
            get
            {
                return _sealno;
            }
            set
            {
                if (_sealno != value)
                {
                    _sealno = value;
                    base.OnPropertyChanged("SealNo", value);
                }
            }
        }

        #endregion

        #region 货物类型

        int _cagotype = 0;
        /// <summary>
        /// 货物类型
        /// </summary>
        public int CagoType
        {
            get
            {
                return _cagotype;
            }
            set
            {
                if (_cagotype != value)
                {
                    _cagotype = value;
                    base.OnPropertyChanged("CagoType", value);
                }
            }
        }

        #endregion

        #region 约号

        string _sqno;
        /// <summary>
        /// 约号
        /// </summary>
        public string SQNO
        {
            get
            {
                return _sqno;
            }
            set
            {
                if (_sqno != value)
                {
                    _sqno = value;
                    base.OnPropertyChanged("SQNO", value);
                }
            }
        }

        #endregion

        #region 船代

        string _agent;
        /// <summary>
        /// 船代
        /// </summary>
        public string Agent
        {
            get
            {
                return _agent;
            }
            set
            {
                if (_agent != value)
                {
                    _agent = value;
                    base.OnPropertyChanged("Agent", value);
                }
            }
        }

        #endregion

        #region 摄氏度

        string _centigrade;
        /// <summary>
        /// 摄氏度
        /// </summary>
        public string Centigrade
        {
            get
            {
                return _centigrade;
            }
            set
            {
                if (_centigrade != value)
                {
                    _centigrade = value;
                    base.OnPropertyChanged("Centigrade", value);
                }
            }
        }

        #endregion

        #region 华氏度

        string _centigradef;
        /// <summary>
        /// 华氏度
        /// </summary>
        public string CentigradeF
        {
            get
            {
                return _centigradef;
            }
            set
            {
                if (_centigradef != value)
                {
                    _centigradef = value;
                    base.OnPropertyChanged("CentigradeF", value);
                }
            }
        }

        #endregion

        #region 危险品类型

        string _dangerousclass;
        /// <summary>
        /// 危险品类型
        /// </summary>
        public string DangerousClass
        {
            get
            {
                return _dangerousclass;
            }
            set
            {
                if (_dangerousclass != value)
                {
                    _dangerousclass = value;
                    base.OnPropertyChanged("DangerousClass", value);
                }
            }
        }

        #endregion

        #region 危险品性质

        string _dangerousproperty;
        /// <summary>
        /// 危险品性质
        /// </summary>
        public string DangerousProperty
        {
            get
            {
                return _dangerousproperty;
            }
            set
            {
                if (_dangerousproperty != value)
                {
                    _dangerousproperty = value;
                    base.OnPropertyChanged("DangerousProperty", value);
                }
            }
        }

        #endregion

        #region 危险品页号

        string _dangerouspage;
        /// <summary>
        /// 危险品页号
        /// </summary>
        public string DangerousPage
        {
            get
            {
                return _dangerouspage;
            }
            set
            {
                if (_dangerouspage != value)
                {
                    _dangerouspage = value;
                    base.OnPropertyChanged("DangerousPage", value);
                }
            }
        }

        #endregion

        #region 危险品编号

        string _dangerousno;
        /// <summary>
        /// 危险品编号
        /// </summary>
        public string DangerousNo
        {
            get
            {
                return _dangerousno;
            }
            set
            {
                if (_dangerousno != value)
                {
                    _dangerousno = value;
                    base.OnPropertyChanged("DangerousNo", value);
                }
            }
        }

        #endregion

    }
    #endregion

    #region ShippingOrderList

    /// <summary>
    ///  ShippingOrderList
    /// </summary>
    [Serializable]
    public partial class ShippingOrderList : BaseDataObject
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

        string _no;
        /// <summary>
        /// 订舱号
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

        Guid? _freightRateID;

        /// <summary>
        /// 合约ID
        /// </summary>
        public Guid? FreightRateID
        {
            get
            {
                return _freightRateID;
            }
            set
            {
                _freightRateID = value;
                base.OnPropertyChanged("FreightRateID", value);
            }
        }

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

        #region Carrier

        Guid _CarrierID;
        /// <summary>
        /// 船东
        /// </summary>
        public Guid CarrierID
        {
            get { return _CarrierID; }
            set
            {
                if (_CarrierID != value)
                {
                    _CarrierID = value;
                    base.OnPropertyChanged("CarrierID", value);
                }
            }
        }

        string _carriername;
        /// <summary>
        /// 船东
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

        #endregion

        #region AgentOfCarrier

        string _agentofcarriername;
        /// <summary>
        /// 承运人
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

        Guid? _agentofcarrierID;
        /// <summary>
        /// 承运人
        /// </summary>
        public Guid? AgentofcarrierID
        {
            get
            {
                return _agentofcarrierID;
            }
            set
            {
                if (_agentofcarrierID != value)
                {
                    _agentofcarrierID = value;
                    base.OnPropertyChanged("AgentofcarrierID", value);
                }
            }
        }

        #endregion

        #region Voyage

        string _prevoyagename;
        /// <summary>
        /// 头程航次
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


        Guid? _prevoyageid;
        /// <summary>
        /// 头程航次ID
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


        Guid? _voyageid;
        /// <summary>
        /// 二程航次ID
        /// </summary>
        public Guid? VoyageID
        {
            get
            {
                return _voyageid;
            }
            set
            {
                if (_voyageid != value)
                {
                    _voyageid = value;
                    base.OnPropertyChanged("VoyageID", value);
                }
            }
        }


        string _voyagename;
        /// <summary>
        /// 二程航次名
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

        #region 日期

        DateTime _soDate;
        /// <summary>
        /// 接收委托单时间
        /// </summary>
        [Required(CMessage = "接收委托单时间", EMessage = "SoDate")]
        public DateTime SODate
        {
            get
            {
                return _soDate;
            }
            set
            {
                if (_soDate != value)
                {
                    _soDate = value;
                    base.OnPropertyChanged("_soDate", value);
                }
            }
        }

        DateTime? _eta;
        /// <summary>
        /// 到港日
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


        DateTime? _etd;
        /// <summary>
        /// 离港日
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

        DateTime? _closingdate;
        /// <summary>
        /// 截关日
        /// </summary>
        public DateTime? ClosingDate
        {
            get
            {
                return _closingdate;
            }
            set
            {
                if (_closingdate != value)
                {
                    _closingdate = value;
                    base.OnPropertyChanged("ClosingDate", value);
                }
            }
        }

        DateTime? _docclosingdate;
        /// <summary>
        /// 截文件日
        /// </summary>
        public DateTime? DOCClosingDate
        {
            get
            {
                return _docclosingdate;
            }
            set
            {
                if (_docclosingdate != value)
                {
                    _docclosingdate = value;
                    base.OnPropertyChanged("DOCClosingDate", value);
                }
            }
        }

        DateTime? _cyclosingdate;
        /// <summary>
        /// 截柜日
        /// </summary>
        public DateTime? CYClosingDate
        {
            get
            {
                return _cyclosingdate;
            }
            set
            {
                if (_cyclosingdate != value)
                {
                    _cyclosingdate = value;
                    base.OnPropertyChanged("CYClosingDate", value);
                }
            }
        }

        #endregion

        #region ReturnLocation

        Guid? _ReturnLocationID;
        /// <summary>
        /// 还重地ID
        /// </summary>
        public Guid? ReturnLocationID
        {
            get
            {
                return _ReturnLocationID;
            }
            set
            {
                if (_ReturnLocationID != value)
                {
                    _ReturnLocationID = value;
                    base.OnPropertyChanged("ReturnLocationID", value);
                }
            }
        }

        string _ReturnLocationName;
        /// <summary>
        /// 还重地名称
        /// </summary>
        public string ReturnLocationName
        {
            get
            {
                return _ReturnLocationName;
            }
            set
            {
                if (_ReturnLocationName != value)
                {
                    _ReturnLocationName = value;
                    base.OnPropertyChanged("ReturnLocationName", value);
                }
            }
        }

        #endregion

        #region createby

        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID
        {
            get
            {
                return _createbyid;
            }
            set
            {
                if (_createbyid != value)
                {
                    _createbyid = value;
                    base.OnPropertyChanged("CreateByID", value);
                }
            }
        }

        string _createbyname;
        /// <summary>
        /// 建立人
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
                    base.OnPropertyChanged("CreateByName", value);
                }
            }
        }

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
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }

        #endregion

        public override string ToString()
        {
            return this.No;
        }
    }

    #endregion

    #region BookingBLInfo 订舱的提单信息

    /// <summary>
    /// 订舱的提单信息
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "OEBookingBLInfo")]
    public class BookingBLInfo
    {
        /// <summary>
        /// 所属订舱单的GUID
        /// </summary>
        public Guid OceanBookingID { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 提单号
        /// </summary>
        public string NO { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo { get; set; }

        /// <summary>
        /// 提单状态
        /// </summary>
        public OEBLState State { get; set; }

        /// <summary>
        /// 提单状态
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    }

    #endregion

    #region BookingContainerInfo 订舱的箱信息

    /// <summary>
    /// 订舱的箱信息
    /// </summary>
    [Serializable]
    public class BookingContainerInfo
    {
        /// <summary>
        /// 所属订舱单的GUID
        /// </summary>
        public Guid OceanBookingID { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 箱号
        /// </summary>
        public string NO { get; set; }

        /// <summary>
        /// 箱型
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 箱型的GUID
        /// </summary>
        public Guid TypeId { get; set; }
    }

    #endregion

    #region BookingContainerInfo CSCLBookingEDIInfo

    /// <summary>
    /// CSCL EDI 订舱信息
    /// </summary>
    [Serializable]
    public class CSCLBookingInfo : BaseDataObject
    {
        private Guid oceanBookingID;
        /// <summary>
        /// 所属订舱单的GUID
        /// </summary>
        public Guid OceanBookingID
        {
            get
            {
                return oceanBookingID;
            }
            set
            {
                if (oceanBookingID != value)
                {
                    oceanBookingID = value;
                    base.OnPropertyChanged("OceanBookingID", value);
                }
            }
        }
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
        private string scno;
        /// <summary>
        /// 美线服务合同号
        /// </summary>
        public string SCNO
        {
            get
            {
                return scno;
            }
            set
            {
                if (scno != value)
                {
                    scno = value;
                    base.OnPropertyChanged("SCNO", value);
                }
            }
        }
        private string scacCode;
        /// <summary>
        /// SCACCode
        /// </summary>
        public string SCACCode
        {
            get
            {
                return scacCode;
            }
            set
            {
                if (scacCode != value)
                {
                    scacCode = value;
                    base.OnPropertyChanged("SCACCode", value);
                }
            }
        }
        private string hblNO;
        /// <summary>
        /// HBLNO
        /// </summary>
        public string HBLNO
        {
            get
            {
                return hblNO;
            }
            set
            {
                if (hblNO != value)
                {
                    hblNO = value;
                    base.OnPropertyChanged("HBLNO", value);
                }
            }
        }
        private string shipper;
        /// <summary>
        /// 发货人
        /// </summary>
        public string Shipper
        {
            get
            {
                return shipper;
            }
            set
            {
                if (shipper != value)
                {
                    shipper = value.Replace(System.Environment.NewLine, "^");
                    base.OnPropertyChanged("Shipper", value);
                }
            }
        }
        private string realShipper;
        /// <summary>
        /// 真实发货人
        /// </summary>
        public string RealShipper
        {
            get
            {
                return realShipper;
            }
            set
            {
                if (realShipper != value)
                {
                    realShipper = value.Replace(System.Environment.NewLine, "^");
                    base.OnPropertyChanged("RealShipper", value);
                }
            }
        }
        private string consignee;
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee
        {
            get
            {
                return consignee;
            }
            set
            {
                if (consignee != value)
                {
                    consignee = value.Replace(System.Environment.NewLine, "^");
                    base.OnPropertyChanged("Consignee", value);
                }
            }
        }
        private string realConsignee;
        /// <summary>
        /// 真实收货人
        /// </summary>
        public string RealConsignee
        {
            get
            {
                return realConsignee;
            }
            set
            {
                if (realConsignee != value)
                {
                    realConsignee = value.Replace(System.Environment.NewLine, "^");
                    base.OnPropertyChanged("RealConsignee", value);
                }
            }
        }
        private string notify;
        /// <summary>
        /// 通知人
        /// </summary>
        public string Notify
        {
            get
            {
                return notify;
            }
            set
            {
                if (notify != value)
                {
                    notify = value.Replace(System.Environment.NewLine, "^");
                    base.OnPropertyChanged("Notify", value);
                }
            }
        }
        private string realNotify;
        /// <summary>
        /// 真实通知人
        /// </summary>
        public string RealNotify
        {
            get
            {
                return realNotify;
            }
            set
            {
                if (realNotify != value)
                {
                    realNotify = value.Replace(System.Environment.NewLine, "^");
                    base.OnPropertyChanged("RealNotify", value);
                }
            }
        }
        private string cargoDescUS;
        /// <summary>
        /// 英文品名
        /// </summary>
        public string CargoDescUS
        {
            get
            {
                return cargoDescUS;
            }
            set
            {
                if (cargoDescUS != value)
                {
                    cargoDescUS = value.Replace(System.Environment.NewLine, "^");
                    base.OnPropertyChanged("CargoDescUS", value);
                }
            }
        }
        private string remarksCN;
        /// <summary>
        /// 附加说明（中文）
        /// </summary>
        public string RemarksCN
        {
            get
            {
                return remarksCN;
            }
            set
            {
                if (remarksCN != value)
                {
                    remarksCN = value.Replace(System.Environment.NewLine, "^");
                    base.OnPropertyChanged("RemarksCN", value);
                }
            }
        }
        private string bookingRemarksCN;
        /// <summary>
        /// 订舱要求（中文）
        /// </summary>
        public string BookingRemarksCN
        {
            get
            {
                return bookingRemarksCN;
            }
            set
            {
                if (bookingRemarksCN != value)
                {
                    bookingRemarksCN = value.Replace(System.Environment.NewLine, "^");
                    base.OnPropertyChanged("BookingRemarksCN", value);
                }
            }
        }

        private string bookingNO;
        /// <summary>
        /// 业务号/订舱号
        /// </summary>
        public string BookingNO
        {
            get
            {
                return bookingNO;
            }
            set
            {
                if (bookingNO != value)
                {
                    bookingNO = value;
                    base.OnPropertyChanged("BookingNO", value);
                }
            }
        }
        private string marks;
        /// <summary>
        /// 唛头
        /// </summary>
        public string Marks
        {
            get
            {
                return marks;
            }
            set
            {
                if (marks != value)
                {
                    marks = value;
                    base.OnPropertyChanged("Marks", value);
                }
            }
        }
        private string deliveryTerm;
        /// <summary>
        /// 运输条款
        /// </summary>
        public string DeliveryTerm
        {
            get
            {
                return deliveryTerm;
            }
            set
            {
                if (deliveryTerm != value)
                {
                    deliveryTerm = value;
                    base.OnPropertyChanged("DeliveryTerm", value);
                }
            }
        }
        private string releaseCargoType;
        /// <summary>
        /// 放货方式
        /// </summary>
        public string ReleaseCargoType
        {
            get
            {
                return releaseCargoType;
            }
            set
            {
                if (releaseCargoType != value)
                {
                    releaseCargoType = value;
                    base.OnPropertyChanged("ReleaseCargoType", value);
                }
            }
        }
        private string hsCode;
        /// <summary>
        /// HSCode
        /// </summary>
        public string HSCode
        {
            get
            {
                return hsCode;
            }
            set
            {
                if (hsCode != value)
                {
                    hsCode = value;
                    base.OnPropertyChanged("HSCode", value);
                }
            }
        }

        public Guid SaveByID { get; set; }
        public DateTime? UpdateDate { get; set; }

    }

    #endregion

    #region cscl配置信息
    public class CSCLBookingConfig
    {
        public Guid ID { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        public string Pwd { get; set; }
    }
    #endregion

    #region NotAMSList
    public class NotAMSList
    {
        public Guid ID { get; set; }

        public string No { get; set; }

        public DateTime EndAmsDate { get; set; }

        /// <summary>
        /// 经过时间 timespan*10 分钟
        /// </summary>
        public int timespan = 0;
    }
    #endregion
}
