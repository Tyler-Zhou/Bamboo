namespace ICP.FCM.AirExport.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using System.Runtime.Serialization;

    #region  AirBookingList 订舱列表数据对象

    /// <summary>
    /// 订舱列表数据对象
    /// </summary>
    [Serializable]
    public partial class AirBookingList : BaseDataObject
    {
        /// <summary>
        /// 从订舱单列表到编辑界面时
        /// 是新增、编辑还是复制的模式
        /// </summary>
        public EditMode EditMode { get; set; }

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

        #region 状态

        AEOrderState _state;
        /// <summary>
        /// 状态（0待定、1已确认、2已接受、3已打回、4已取消、5已完成）
        /// </summary>
        public AEOrderState State
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

        #region 航班号

        string _filightNo;
        /// <summary>
        /// 航班号
        /// </summary>
        public string FilightNo
        {
            get
            {
                return _filightNo;
            }
            set
            {
                if (_filightNo != value)
                {
                    _filightNo = value;
                    base.OnPropertyChanged("FilightNo", value);
                }
            }
        }

        #endregion

        #region 航班号ID

        Guid? _filightId;
        /// <summary>
        /// 
        /// </summary>
        public Guid? FilightId
        {
            get
            {
                return _filightId;
            }
            set
            {
                if (_filightId != value)
                {
                    _filightId = value;
                    base.OnPropertyChanged("FilightId", value);
                }
            }
        }

        #endregion

        #region 航线

        string _fightNo;
        /// <summary>
        /// 航线
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "航线", EMessage = "FightNo")]
        public string FightNo
        {
            get
            {
                return _fightNo;
            }
            set
            {
                if (_fightNo != value)
                {
                    _fightNo = value;
                    this.NotifyPropertyChanged(p => p.FightNo);
                    //base.OnPropertyChanged("PlaceOfDeliveryName", value);
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

        #region 起运港

        string _departurename;
        /// <summary>
        /// 起运港
        /// </summary>
        public string DepartureName
        {
            get
            {
                return _departurename;
            }
            set
            {
                if (_departurename != value)
                {
                    _departurename = value;
                    base.OnPropertyChanged("DepartureName", value);
                }
            }
        }

        #endregion

        #region 目的港

        string _detinationname;
        /// <summary>
        /// 目的港
        /// </summary>
        public string DetinationName
        {
            get
            {
                return _detinationname;
            }
            set
            {
                if (_detinationname != value)
                {
                    _detinationname = value;
                    this.NotifyPropertyChanged(p => p.DetinationName);
                }
            }
        }

        #endregion

        #region 交货地(名称)

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
                    this.NotifyPropertyChanged(p => p.PlaceOfDeliveryName);
                    //base.OnPropertyChanged("PlaceOfDeliveryName", value);
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

        #region 通知人

        string _notifyname;
        /// <summary>
        /// 通知人
        /// </summary>
        public string NotifyName
        {
            get
            {
                return _notifyname;
            }
            set
            {
                if (_notifyname != value)
                {
                    _notifyname = value;
                    base.OnPropertyChanged("NotifyName", value);
                }
            }
        }

        #endregion

        #region 代理人

        string _agentname;
        /// <summary>
        /// 通知人
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

        #region 航空公司名称

        string _airCompanyname;
        /// <summary>
        /// 航空公司名称
        /// </summary>
        public string AirCompanyName
        {
            get
            {
                return _airCompanyname;
            }
            set
            {
                if (_airCompanyname != value)
                {
                    _airCompanyname = value;
                    base.OnPropertyChanged("AirCompanyName", value);
                }
            }
        }
        #endregion
        #region 航空公司ID

        Guid? _airCompanyid;
        /// <summary>
        /// 航空公司ID
        /// </summary>
        public Guid? AirCompanyId
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
                    base.OnPropertyChanged("AirCompanyId", value);
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

        //#region 港后客服

        //string _podContact;
        ///// <summary>
        ///// 港后客服
        ///// </summary>
        //public string PODContact
        //{
        //    get
        //    {
        //        return _podContact;
        //    }
        //    set
        //    {
        //        if (_podContact != value)
        //        {
        //            _podContact = value;
        //            base.OnPropertyChanged("PODContact", value);
        //        }
        //    }
        //}

        //#endregion

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

        //#region 装货单ID

        //Guid? _oceanshippingorderid;
        ///// <summary>
        ///// 装货单ID
        ///// </summary>
        //public Guid? AirShippingOrderID
        //{
        //    get
        //    {
        //        return _oceanshippingorderid;
        //    }
        //    set
        //    {
        //        if (_oceanshippingorderid != value)
        //        {
        //            _oceanshippingorderid = value;
        //            base.OnPropertyChanged("AirShippingOrderID", value);
        //        }
        //    }
        //}

        //#endregion

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

        #region 主提单列表

        List<BookingBLInfo> _oceanmbls;
        /// <summary>
        /// 主提单列表
        /// </summary>
        public List<BookingBLInfo> AirMBLs
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
                    base.OnPropertyChanged("AirMBLs", value);
                }
            }
        }

        #endregion

        #region 分提单列表

        List<BookingBLInfo> _oceanhbls;
        /// <summary>
        /// 分提单列表
        /// </summary>
        public List<BookingBLInfo> AirHBLs
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
                    base.OnPropertyChanged("AirHBLs", value);
                }
            }
        }

        #endregion
    }

    #endregion

    #region AirBookingInfo 订舱详细信息

    /// <summary>
    /// 订舱详细信息
    /// </summary>
    [Serializable]
    [KnownType(typeof(DryCargo))]
    [KnownType(typeof(ReeferCargo))]
    [KnownType(typeof(DangerousCargo))]
    [KnownType(typeof(AwkwardCargo))]
    public partial class AirBookingInfo : AirBookingList
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
        [GuidRequired(CMessage = "业务部门", EMessage = "SalesDepartment")]
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

        #region 空外部客服ID

        Guid? _OverSeasFilerID;
        /// <summary>
        /// 空外部客服ID
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

        #region 订舱ID

        Guid? _BookingerID;
        /// <summary>
        /// 订舱ID
        /// </summary>
        [GuidRequired(CMessage = "订舱员", EMessage = "Bookinger")]
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

        Guid? _bookingcustomerid;
        /// <summary>
        /// 订舱客户ID
        /// </summary>
        [GuidRequired(CMessage = "订舱客户", EMessage = "BookingCustomer")]
        public Guid? BookingCustomerID
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

        #region 起运港ID

        Guid _polid;
        /// <summary>
        /// 起运港ID
        /// </summary>
        [GuidRequired(CMessage = "起运港", EMessage = "POL")]
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

        #region 目的港ID

        Guid _podid;
        /// <summary>
        /// 目的港ID
        /// </summary>
        [GuidRequired(CMessage = "目的港", EMessage = "POD")]
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

        #endregion

        #region 船公司

        Guid _carrierid;
        /// <summary>
        /// 船公司ID
        /// </summary>
        public Guid CarrierID
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

        #region 航线

        Guid? _shippinglineid;
        /// <summary>
        /// 航线ID
        /// </summary>
        [GuidRequired(CMessage = "航线", EMessage = "ShippingLine")]
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

        #region 仓库

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
        [StringLength(MaximumLength = 1000, CMessage = "Master B/L出单要求", EMessage = "MBLRequirements")]
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
        [StringLength(MaximumLength = 1000, CMessage = "House B/L出单要求", EMessage = "HBLRequirements")]
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

        //string _customerrefno;
        ///// <summary>
        ///// 客户参考号
        ///// </summary>
        //[StringLength(20)]
        //public string CustomerRefNo
        //{
        //    get
        //    {
        //        return _customerrefno;
        //    }
        //    set
        //    {
        //        if (_customerrefno != value)
        //        {
        //            _customerrefno = value;
        //            base.OnPropertyChanged("CustomerRefNo", value);
        //        }
        //    }
        //}

        #region 装货单数据版本

        DateTime? _airShippingOrderUpdateDate;
        /// <summary>
        /// 装货单数据版本
        /// </summary>
        public DateTime? AirShippingOrderUpdateDate
        {
            get
            {
                return _airShippingOrderUpdateDate;
            }
            set
            {
                if (_airShippingOrderUpdateDate != value)
                {
                    _airShippingOrderUpdateDate = value;
                    base.OnPropertyChanged("AirShippingOrderUpdateDate", value);
                }
            }
        }

        #endregion

        #region 装货单ID

        Guid? _airshippingorderid;
        /// <summary>
        /// 装货单ID
        /// </summary>
        public Guid? AirShippingOrderID
        {
            get
            {
                return _airshippingorderid;
            }
            set
            {
                if (_airshippingorderid != value)
                {
                    _airshippingorderid = value;
                    base.OnPropertyChanged("AirShippingOrderID", value);
                }
            }
        }

        #endregion

        //DateTime? _confirmeddate;
        ///// <summary>
        ///// 确认时间
        ///// </summary>
        //public DateTime? ConfirmedDate
        //{
        //    get
        //    {
        //        return _confirmeddate;
        //    }
        //    set
        //    {
        //        if (_confirmeddate != value)
        //        {
        //            _confirmeddate = value;
        //            base.OnPropertyChanged("ConfirmedDate", value);
        //        }
        //    }
        //}
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
        [Required(CMessage = "接收委托单时间", EMessage = "SODate")]
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
    public class BookingBLInfo
    {
        /// <summary>
        /// 所属订舱单的GUID
        /// </summary>
        public Guid AirBookingID { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 提单号
        /// </summary>
        public string NO { get; set; }

        /// <summary>
        /// 提单状态
        /// </summary>
        public AEBLState State { get; set; }

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
        public Guid AirBookingID { get; set; }

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
}
