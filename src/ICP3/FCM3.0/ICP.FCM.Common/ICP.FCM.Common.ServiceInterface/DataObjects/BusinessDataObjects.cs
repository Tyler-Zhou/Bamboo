namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    using System; 
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Client;
using System.Collections.Generic;

    public sealed class VoyageSelectionInfo
    {
        public VoyageType VoyageType { get; set; }
        public Guid? VoyageId { get; set; }
    }
    /// <summary>
    /// 航次类型 驳船或者大船
    /// </summary>
    public enum VoyageType
    {
        PreVoyage,
        Voyage
    }

    #region 根据POL,POR,POD,PRVessel,Vessel获取ETD,ETA信息
    /// <summary>
    /// 航次相关日期信息
    /// </summary>
    [Serializable]
    public class VoyageDateInfo
    {
        //public DateTime? PETD { get; set; }
        public DateTime? ETA;
        public DateTime? ETD;
        public Guid? PortId;
        public Guid? VoyageId;
        /// <summary>
        /// 截关日
        /// </summary>
        public DateTime? ClosingDate { get; set; }
        /// <summary>
        /// 截柜日
        /// </summary>
        public DateTime? CYClosingDate { get; set; }
        /// <summary>
        /// 截文件日
        /// </summary>
        public DateTime? DOCClosingDate { get; set; }

    }
    #endregion

    #region  业务列表数据对象

    /// <summary>
    /// 业务列表数据对象
    /// </summary>
    [Serializable]
    public partial class BusinessData : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

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

        #region 业务类型：海进出，空进出，其它业务等

        OperationType _operationType;
        public OperationType OperationType
        {
            get
            {
                return _operationType;
            }
            set
            {
                if (_operationType != value)
                {
                    _operationType = value;
                    base.OnPropertyChanged("OperationType", value);
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

        #region 公司(发送EDI时使用)
        /// <summary>
        /// 公司
        /// </summary>
        public Guid CompanyID
        {
            get;
            set;
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

        #region 订舱号

        string _oceanshippingorderno;
        /// <summary>
        /// 订舱号
        /// </summary>
        [StringLength(MaximumLength=20,CMessage="订舱号",EMessage="OceanShippingOrderNo")]
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

        #endregion

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

        #region 截关日

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

        #region 订舱

        string _BookingerName;
        /// <summary>
        /// 订舱
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

        #region 接收委托单日期

        DateTime _bookingDate;
        /// <summary>
        /// 接收委托单日期
        /// </summary>
        [Required(CMessage = "接收委托单日期",EMessage="BookingDate")]
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
    }
    #endregion
}
