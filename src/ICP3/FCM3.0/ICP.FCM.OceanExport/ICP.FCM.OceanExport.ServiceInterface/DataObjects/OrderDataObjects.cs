namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using System.Linq;
    using ICP.FAM.ServiceInterface.DataObjects;
    using System.Runtime.Serialization;

    #region OrderList

    /// <summary>
    /// 订单查询列表用到的字段
    /// 作者：熊中方
    /// 创建时间：2011-05-11 10:51
    /// </summary>
    [KnownType(typeof(DryCargo))]
    [KnownType(typeof(ReeferCargo))]
    [KnownType(typeof(DangerousCargo))]
    [KnownType(typeof(AwkwardCargo))]
    [Serializable]
    public partial class OceanOrderList : BaseDataObject
    {
        /// <summary>
        /// 从订单列表到编辑界面时
        /// 是新增、编辑还是复制的模式
        /// </summary>
        public EditMode EditMode { get; set; }

        #region ID(唯一键)

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

                    this.NotifyPropertyChanged(o => o.ID);

                    //base.OnPropertyChanged("ID", value);
                }
            }
        }

        #endregion

        #region 状态

        OEOrderState _state;

        /// <summary>
        /// 状态（1待订舱、2已打回、3已订舱、4已放单、5已关单）
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

                    this.NotifyPropertyChanged(p => p.State);
                    //base.OnPropertyChanged("State", value);
                }
            }
        }

        #endregion

        #region 业务号

        string _refNo;
        /// <summary>
        /// 业务号
        /// </summary>
        public string RefNo
        {
            get
            {
                return _refNo;
            }
            set
            {
                if (_refNo != value)
                {
                    _refNo = value;
                    this.NotifyPropertyChanged(p => p.RefNo);
                    //base.OnPropertyChanged("RefNo", value);
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

                    this.NotifyPropertyChanged(p => p.CustomerName);
                    //base.OnPropertyChanged("CustomerName", value);
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

                    this.NotifyPropertyChanged(p => p.ShipperName);
                    //base.OnPropertyChanged("ShipperName", value);
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

                    this.NotifyPropertyChanged(p => p.PlaceOfReceiptName);
                    //base.OnPropertyChanged("PlaceOfReceiptName", value);
                }
            }
        }

        #endregion

        #region 装货港(名称)

        string _polname;
        /// <summary>
        /// 装货港名称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "装货港名称", EMessage = "POLName")]
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

                    this.NotifyPropertyChanged(p => p.POLName);
                    //base.OnPropertyChanged("POLName", value);
                }
            }
        }

        #endregion

        #region 卸货港(名称)

        string _podname;
        /// <summary>
        /// 卸货港名称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "卸货港名称", EMessage = "PODName")]

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

                    this.NotifyPropertyChanged(p => p.PODName);
                    //base.OnPropertyChanged("PODName", value);
                }
            }
        }

        #endregion

        #region 交货地(名称)

        string _placeofdeliveryname;
        /// <summary>
        /// 交货地名称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "交货地", EMessage = "PlaceOfDeliveryName")]
        [Required(CMessage = "交货地", EMessage = "PlaceOfDeliveryName")]
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
                    this.NotifyPropertyChanged(p => p.PlaceOfDeliveryName);
                    //base.OnPropertyChanged("PlaceOfDeliveryName", value);
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
        /// 在列表中显示对应语言的“业务类型”枚举值的描述信息
        /// </summary>    
        public string BookingTypeDescription { get; set; }

        #endregion

        #region 揽货人

        string _salesname;
        /// <summary>
        /// 揽货人
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

        #region 创建日

        DateTime _createdate;
        /// <summary>
        /// 创建日
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

        #region 订舱

        Guid? _bookingerID;
        /// <summary>
        /// 操作ID
        /// (根据是否当前用户,会启用/禁用某些按钮)
        /// </summary>
        public Guid? BookingerID
        {
            get
            {
                return _bookingerID;
            }
            set
            {
                if (_bookingerID != value)
                {
                    _bookingerID = value;
                    base.OnPropertyChanged("BookingerID", value);
                }
            }
        }

        string _bookingerName;
        /// <summary>
        /// 订舱员
        /// </summary>
        public string BookingerName
        {
            get
            {
                return _bookingerName;
            }
            set
            {
                if (_bookingerName != value)
                {
                    _bookingerName = value;
                    base.OnPropertyChanged("BookingerName", value);
                }
            }
        }

        #endregion

        #region 文件

        string _filer;
        /// <summary>
        /// 文件
        /// </summary>
        public string Filer
        {
            get
            {
                return _filer;
            }
            set
            {
                if (_filer != value)
                {
                    _filer = value;
                    base.OnPropertyChanged("Filer", value);
                }
            }
        }

        #endregion

        #region 港后客服

        string _podContact;
        /// <summary>
        /// 港后客服/目的港联系人(只显示，数据库无此字段)
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

        #region 打回原因

        string _reason;

        /// <summary>
        /// 打回原因
        /// </summary>
        public string Reason
        {
            get
            {
                return _reason;
            }
            set
            {
                if (_reason != value)
                {
                    _reason = value;
                    base.OnPropertyChanged("Reason", value);
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

        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        #region 更新时间
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

        #region 建立人

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


        Guid _createbyid;
        /// <summary>
        /// 建立人ID
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

        #endregion
    }

    #endregion

    #region OrderInfo
    /// <summary>
    /// 订单编辑用到的所有字段（继承列表中已出现的字段）
    /// 作者：熊中方
    /// 创建时间：2011-05-11 10:51
    /// </summary>
    [Serializable]
    [KnownType(typeof(ReeferCargo))]
    [KnownType(typeof(DryCargo))]
    [KnownType(typeof(DangerousCargo))]
    [KnownType(typeof(AwkwardCargo))]
    public partial class OceanOrderInfo : OceanOrderList
    {
        #region 口岸公司

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

        #region 揽货人

        Guid? _salesid;
        /// <summary>
        /// 业务员ID
        /// </summary>
        [GuidRequired(CMessage = "业务员", EMessage = "Sales")]
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

        #region 揽货部门

        Guid _salesdepartmentid;

        /// <summary>
        /// 揽货部门ID
        /// </summary>
        [GuidRequired(CMessage = "揽货部门", EMessage = "SalesDepartment")]
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
        /// 揽货部门名称
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
        [GuidRequired(CMessage = "运输条款", EMessage = "TransportClause")]
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

        #region 委托方式

        FCMBookingMode _bookingmode;
        /// <summary>
        /// 委托方式/订舱方式（0电话、1邮件、2电子订舱）
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


        CustomerDescription _bookingcustomerdescription;
        /// <summary>
        /// 订舱客户详细信息
        /// </summary>
        public CustomerDescription BookingCustomerDescription
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


        string _bookingcustomername;
        /// <summary>
        /// 订舱客户名称
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

        #region 发货人

        Guid? _shipperid;
        /// <summary>
        /// 发货人ID
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


        CustomerDescription _shipperdescription;
        /// <summary>
        /// 发货人详细信息
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


        CustomerDescription _consigneedescription;
        /// <summary>
        /// 收货人详细信息
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

        #region 收货地

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

        #endregion

        #region 交货地

        Guid _placeofdeliveryid;
        /// <summary>
        /// 交货地ID
        /// </summary>
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

                    this.NotifyPropertyChanged(p => p.PlaceOfDeliveryID);
                    //base.OnPropertyChanged("PlaceOfDeliveryID", value);
                }
            }
        }

        #endregion

        #region 最终目的地

        Guid _finalDestinationId;
        /// <summary>
        /// 最终目的地ID
        /// </summary>
        [GuidRequired(CMessage = "最终目的地", EMessage = "FinalDestination")]
        public Guid FinalDestinationId
        {
            get
            {
                return _finalDestinationId;
            }
            set
            {
                if (_finalDestinationId != value)
                {
                    _finalDestinationId = value;
                    base.OnPropertyChanged("FinalDestinationId", value);
                }
            }
        }

        string _finalDestinationName;
        /// <summary>
        /// 最终目的地名称
        /// </summary>
        public string FinalDestinationName
        {
            get
            {
                return _finalDestinationName;
            }
            set
            {
                if (_finalDestinationName != value)
                {
                    _finalDestinationName = value;
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

        #region 数量

        int _quantity;
        /// <summary>
        /// 数量
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

        #region 数量单位

        Guid? _quantityunitid;
        /// <summary>
        /// 数量单位ID
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
        /// 数量单位名称
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

        #endregion

        #region 重量单位

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

        #endregion

        #region 体积单位

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

        string _agentName;

        /// <summary>
        /// 代理名称
        /// </summary>
        public string AgentName
        {
            get
            {
                return _agentName;
            }
            set
            {
                if (_agentName != value)
                {
                    _agentName = value;
                    base.OnPropertyChanged("AgentName", value);
                }
            }
        }


        CustomerDescription _agentdescription;
        /// <summary>
        /// 代理详细信息
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

        #region 船东(船公司)

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

        #region 估计交货

        DateTime? _estimateddeliverydate;
        /// <summary>
        /// 估计交货日期
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

        #region 期望出运日

        DateTime? _expectedshipdate;
        /// <summary>
        /// 期望出运日
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

        #region 期望到达

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

        #region 本地服务(仓储、拖车、报关、商检、质检)

        #region 是否仓储

        bool _iswarehouse;
        /// <summary>
        /// 是否仓储
        /// </summary>
        public bool IsWarehouse
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
                    base.OnPropertyChanged("IsWarehouse", value);
                }
            }
        }

        #endregion

        #region 是否拖车

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

        #region 是否报关

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

        #region 是否商检

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

        #endregion

        #region 质检(是否动植检)？

        bool _isquarantineinspection;
        /// <summary>
        /// 质检(是否动植检)
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

        #region MBL

        #region 付款方式

        Guid? _mblpaymenttermid;
        /// <summary>
        /// Master B/L付款方式ID
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
        /// Master B/L付费方式名称
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

        #region 放单类型

        FCMReleaseType? _mblreleasetype;
        /// <summary>
        /// Master B/L放单类型（0正本，1电放）
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

        #region 备注

        string _mblrequirements;
        /// <summary>
        /// Master B/L出单要求
        /// </summary>
        [StringLength(MaximumLength = 1000, CMessage = "B/L出单要求", EMessage = "MBLRequirement")]
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

        #endregion

        #region HBL

        #region 付款方式

        Guid? _hblpaymenttermid;
        /// <summary>
        /// House B/L付款方式ID
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
        /// House B/L付款方式名称
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

        #region 放单类型

        FCMReleaseType? _hblreleasetype;
        /// <summary>
        /// House B/L放单类型（0正本，1电放）
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

        #region 备注

        string _hblrequirements;
        /// <summary>
        /// House B/L出单要求
        /// </summary>
        [StringLength(MaximumLength = 1000, CMessage = "B/L出单要求", EMessage = "HBLRequirement")]
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

        #endregion
    }

    #endregion

    #region PO

    /// <summary>
    /// OceanBookingPOList
    /// </summary>
    [Serializable]
    public partial class OceanBookingPOList : BaseDataObject
    {
        private bool _associated;

        /// <summary>
        /// 是否和当前箱关联
        /// 此项不需要保存到数据库
        /// </summary>
        public bool Associated
        {
            get
            {
                return _associated;
            }
            set
            {
                if (_associated != value)
                {
                    _associated = value;
                    base.OnPropertyChanged("Associated", value);
                }
            }
        }

        /// <summary>
        /// 是否新建数据
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


        Guid _ownerid;
        /// <summary>
        /// 所属ID
        /// </summary>
        public Guid OwnerID
        {
            get
            {
                return _ownerid;
            }
            set
            {
                if (_ownerid != value)
                {
                    _ownerid = value;
                    base.OnPropertyChanged("OwnerID", value);
                }
            }
        }

        Guid? _relationID;
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid? RelationID
        {
            get
            {
                return _relationID;
            }
            set
            {
                if (_relationID != value)
                {
                    _relationID = value;
                    base.OnPropertyChanged("RelationID", value);
                }
            }
        }


        string _no;
        /// <summary>
        /// 订单号
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "订单号", EMessage = "No")]
        [Required(CMessage = "订单号", EMessage = "No")]
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


        string _podescription;
        /// <summary>
        /// 订单描述
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "订单描述", EMessage = "PoDescription")]
        public string PODescription
        {
            get
            {
                return _podescription;
            }
            set
            {
                if (_podescription != value)
                {
                    _podescription = value;
                    base.OnPropertyChanged("PODescription", value);
                }
            }
        }


        Guid? _vendorid;
        /// <summary>
        /// 卖主ID
        /// </summary>
        public Guid? VendorID
        {
            get
            {
                return _vendorid;
            }
            set
            {
                if (_vendorid != value)
                {
                    _vendorid = value;
                    base.OnPropertyChanged("VendorID", value);
                }
            }
        }


        string _vendorname;
        /// <summary>
        /// 卖主
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "卖主", EMessage = "VendorName")]
        public string VendorName
        {
            get
            {
                return _vendorname;
            }
            set
            {
                if (_vendorname != value)
                {
                    _vendorname = value;
                    base.OnPropertyChanged("VendorName", value);
                }
            }
        }


        Guid? _buyerid;
        /// <summary>
        /// 买主ID
        /// </summary>
        public Guid? BuyerID
        {
            get
            {
                return _buyerid;
            }
            set
            {
                if (_buyerid != value)
                {
                    _buyerid = value;
                    base.OnPropertyChanged("BuyerID", value);
                }
            }
        }


        string _buyername;
        /// <summary>
        /// 买主
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "买主", EMessage = "BuyerName")]
        public string BuyerName
        {
            get
            {
                return _buyername;
            }
            set
            {
                if (_buyername != value)
                {
                    _buyername = value;
                    base.OnPropertyChanged("BuyerName", value);
                }
            }
        }


        string _finaldestination;
        /// <summary>
        /// 最终目的地
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "最终目的地", EMessage = "FinalDestination")]
        public string FinalDestination
        {
            get
            {
                return _finaldestination;
            }
            set
            {
                if (_finaldestination != value)
                {
                    _finaldestination = value;
                    base.OnPropertyChanged("FinalDestination", value);
                }
            }
        }


        DateTime? _inwarehousedate;
        /// <summary>
        /// 入仓时间
        /// </summary>
        public DateTime? InWarehouseDate
        {
            get
            {
                return _inwarehousedate;
            }
            set
            {
                if (_inwarehousedate != value)
                {
                    _inwarehousedate = value;
                    base.OnPropertyChanged("InWarehouseDate", value);
                }
            }
        }


        DateTime? _orderdate;
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? OrderDate
        {
            get
            {
                return _orderdate;
            }
            set
            {
                if (_orderdate != value)
                {
                    _orderdate = value;
                    base.OnPropertyChanged("OrderDate", value);
                }
            }
        }


        FCMPOState _state;
        /// <summary>
        /// 状态
        /// </summary>
        public FCMPOState State
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


        Guid _createbyid;
        /// <summary>
        /// 建立人ID
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
        /// 建立日期
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

        List<OceanPOItemList> _items;
        /// <summary>
        /// Item列表
        /// </summary>
        public List<OceanPOItemList> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (_items != value)
                {
                    _items = value;

                    base.OnPropertyChanged("Items", value);
                }
            }
        }
    }

    /// <summary>
    /// OceanBookingPOInfo
    /// </summary>
    [Serializable]
    public partial class OceanBookingPOInfo : OceanBookingPOList
    {
    }


    /// <summary>
    /// OceanPOItemList
    /// </summary>
    [Serializable]
    public partial class OceanPOItemList : BaseDataObject
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

        bool _associated;

        /// <summary>
        /// 是否和当前Container关联
        /// </summary>
        public bool Associated
        {
            get
            {
                return _associated;
            }
            set
            {
                if (_associated != value)
                {
                    _associated = value;
                    base.OnPropertyChanged("Associated", value);
                }
            }
        }

        Guid _poid;
        /// <summary>
        /// PO ID
        /// </summary>
        public Guid POID
        {
            get
            {
                return _poid;
            }
            set
            {
                if (_poid != value)
                {
                    _poid = value;
                    base.OnPropertyChanged("POID", value);
                }
            }
        }


        Guid? _relationID;
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid? RelationID
        {
            get
            {
                return _relationID;
            }
            set
            {
                if (_relationID != value)
                {
                    _relationID = value;
                    base.OnPropertyChanged("RelationID", value);
                }
            }
        }


        string _no;
        /// <summary>
        /// 产品编号
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "产品编号", EMessage = "No")]
        [Required(CMessage = "产品编号", EMessage = "No")]
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


        string _description;
        /// <summary>
        /// 产品名称
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "产品名称", EMessage = "Description")]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    base.OnPropertyChanged("Description", value);
                }
            }
        }


        string _color;
        /// <summary>
        /// 颜色
        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "颜色", EMessage = "Color")]
        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    base.OnPropertyChanged("Color", value);
                }
            }
        }


        string _size;
        /// <summary>
        /// 大小
        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "大小", EMessage = "Size")]
        public string Size
        {
            get
            {
                return _size;
            }
            set
            {
                if (_size != value)
                {
                    _size = value;
                    base.OnPropertyChanged("Size", value);
                }
            }
        }


        decimal _volume;
        /// <summary>
        /// 体积
        /// </summary>
        public decimal Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                if (_volume != value)
                {
                    _volume = value;
                    base.OnPropertyChanged("Volume", value);
                }
            }
        }


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


        int _cartons;
        /// <summary>
        /// 装箱数量
        /// </summary>
        public int Cartons
        {
            get
            {
                return _cartons;
            }
            set
            {
                if (_cartons != value)
                {
                    _cartons = value;
                    base.OnPropertyChanged("Cartons", value);
                }
            }
        }


        int _units;
        /// <summary>
        /// 件数
        /// </summary>
        public int Units
        {
            get
            {
                return _units;
            }
            set
            {
                if (_units != value)
                {
                    _units = value;
                    base.OnPropertyChanged("Units", value);
                }
            }
        }


        string _htscode;
        /// <summary>
        /// 海关编码
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "海关编码", EMessage = "HTSCode")]
        public string HTSCode
        {
            get
            {
                return _htscode;
            }
            set
            {
                if (_htscode != value)
                {
                    _htscode = value;
                    base.OnPropertyChanged("HTSCode", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 建立人ID
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
        /// 建立日期
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
    }

    /// <summary>
    /// OceanPOItemInfo
    /// </summary>
    [Serializable]
    public partial class OceanPOItemInfo : OceanPOItemList
    {
    }

    #endregion

    #region Fee

    /// <summary>
    /// OceanBookingFeeList
    /// </summary>
    [Serializable]
    public partial class OceanBookingFeeList : BaseDataObject
    {

        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

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


        Guid _bookingfeeid;
        /// <summary>
        /// 费用ID
        /// </summary>
        public Guid BookingFeeID
        {
            get
            {
                return _bookingfeeid;
            }
            set
            {
                if (_bookingfeeid != value)
                {
                    _bookingfeeid = value;
                    base.OnPropertyChanged("BookingFeeID", value);
                }
            }
        }


        Guid _oceanbookingid;
        /// <summary>
        /// 订舱ID
        /// </summary>
        public Guid OceanBookingID
        {
            get
            {
                return _oceanbookingid;
            }
            set
            {
                if (_oceanbookingid != value)
                {
                    _oceanbookingid = value;
                    base.OnPropertyChanged("OceanBookingID", value);
                }
            }
        }


        Guid _customerid;
        /// <summary>
        /// 客户
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
        /// 客户名
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


        Guid _chargingcodeid;
        /// <summary>
        /// 费用代码
        /// </summary>
        [GuidRequired(CMessage = "费用代码", EMessage = "ChargingCode")]
        public Guid ChargingCodeID
        {
            get
            {
                return _chargingcodeid;
            }
            set
            {
                if (_chargingcodeid != value)
                {
                    _chargingcodeid = value;
                    base.OnPropertyChanged("ChargingCodeID", value);
                }
            }
        }


        string _chargingcodename;
        /// <summary>
        /// 费用代码
        /// </summary>
        public string ChargingCodeName
        {
            get
            {
                return _chargingcodename;
            }
            set
            {
                if (_chargingcodename != value)
                {
                    _chargingcodename = value;
                    base.OnPropertyChanged("ChargingCodeName", value);
                }
            }
        }


        Guid _currencyid;
        /// <summary>
        /// 币种ID
        /// </summary>
        [GuidRequired(CMessage = "币种", EMessage = "Currency")]
        public Guid CurrencyID
        {
            get
            {
                return _currencyid;
            }
            set
            {
                if (_currencyid != value)
                {
                    _currencyid = value;
                    base.OnPropertyChanged("CurrencyID", value);
                }
            }
        }


        string _currency;
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency
        {
            get
            {
                return _currency;
            }
            set
            {
                if (_currency != value)
                {
                    _currency = value;
                    base.OnPropertyChanged("Currency", value);
                }
            }
        }


        decimal _quantity;
        /// <summary>
        /// 数量
        /// </summary>
        [DecimalRequired(CMessage = "数量", EMessage = "Quantity")]
        public decimal Quantity
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


        decimal _unitprice;
        /// <summary>
        /// 单价
        /// </summary>
        [DecimalRequired(CMessage = "单价", EMessage = "UnitPrice")]
        public decimal UnitPrice
        {
            get
            {
                return _unitprice;
            }
            set
            {
                if (_unitprice != value)
                {
                    _unitprice = value;
                    base.OnPropertyChanged("UnitPrice", value);
                }
            }
        }


        FeeWay _way;
        /// <summary>
        /// 方向
        /// </summary>
        public FeeWay Way
        {
            get
            {
                return _way;
            }
            set
            {
                if (_way != value)
                {
                    _way = value;
                    base.OnPropertyChanged("Way", value);
                }
            }
        }


        decimal _amount;
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    base.OnPropertyChanged("Amount", value);
                }
            }
        }


        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "备注", EMessage = "Remark")]

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


        Guid _createbyid;
        /// <summary>
        /// 建立人ID
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
        /// 建立日期
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
    }

    #endregion
}
