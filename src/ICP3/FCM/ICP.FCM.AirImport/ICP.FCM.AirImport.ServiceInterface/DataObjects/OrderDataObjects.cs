﻿namespace ICP.FCM.AirImport.ServiceInterface
{
    using System;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Helper;
    using ICP.FAM.ServiceInterface.DataObjects;
    using System.Runtime.Serialization;

    #region OrderList

    /// <summary>
    /// 进口订单查询列表用到的字段
    /// </summary>
    [Serializable]
    public partial class AirOrderList : BaseDataObject
    {
        /// <summary>
        /// 从订单列表到编辑界面时
        /// 是新增、编辑还是复制的模式
        /// </summary>
        public EditMode EditMode { get; set; }
      
        /// <summary>
        ///业务联系人
        /// </summary>
        string _ContactName;
        public string ContactName
        {
            get
            {
                return _ContactName;
            }
            set
            {
                if (_ContactName != value)
                {
                    _ContactName = value;
                    base.OnPropertyChanged("ContactName", value);
                }
            }
        }

        /// <summary>
        /// 业务联系人ID
        /// </summary>
        Guid _ContactID;
        public Guid ContactID
        {
            get
            {
                return _ContactID;
            }
            set
            {
                if (_ContactID != value)
                {
                    _ContactID = value;
                    base.OnPropertyChanged("ContactID", value);
                }
            }

        }

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
        [GuidRequired(CMessage = "操作口岸", EMessage = "Company")]
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
                    if (this.State == AIOrderState.Unknown)
                    {
                        stateDescription = string.Empty;
                    }
                    else
                    {
                        try
                        {
                            stateDescription = EnumHelper.GetDescription<AIOrderState>(this.State, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                        }
                        catch
                        {
                            stateDescription = string.Empty;
                        }
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
                    base.OnPropertyChanged("RefNo", value);
                }
            }
        }

        #endregion

        #region 代理

        string _agentName;
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

        #region 航空公司

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

        #region 装货港(名称)

        string _polname;
        /// <summary>
        /// 装货港名称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "装货港", EMessage = "POL")]
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

        #region 卸货港(名称)

        string _podname;
        /// <summary>
        /// 卸货港名称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "卸货港", EMessage = "POD")]

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
                    base.OnPropertyChanged("PlaceOfDeliveryName", value);
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

        #region 到交货地日

        DateTime? _arriveDateDate;
        /// <summary>
        /// 到交货地日
        /// </summary>
        public DateTime? ArriveDate
        {
            get
            {
                return _arriveDateDate;
            }
            set
            {
                if (_arriveDateDate != value)
                {
                    _arriveDateDate = value;
                    base.OnPropertyChanged("ArriveDate", value);
                }
            }
        }

        #endregion

        #region 放货日

        DateTime? _deliveryOfGoodsDate;
        /// <summary>
        /// 放货日
        /// </summary>
        public DateTime? DeliveryOfGoodsDate
        {
            get
            {
                return _deliveryOfGoodsDate;
            }
            set
            {
                if (_deliveryOfGoodsDate != value)
                {
                    _deliveryOfGoodsDate = value;
                    base.OnPropertyChanged("DeliveryOfGoodsDate", value);
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

        #region 客服

        Guid? _customerContactID;
        /// <summary>
        /// 客服ID
        /// </summary>
        public Guid? CustomerContactID
        {
            get
            {
                return _customerContactID;
            }
            set
            {
                if (_customerContactID != value)
                {
                    _customerContactID = value;
                    base.OnPropertyChanged("CustomerContactID", value);
                }
            }
        }

        string _customerContactName;
        /// <summary>
        /// 客服(名称)
        /// </summary>
        public string CustomerContactName
        {
            get
            {
                return _customerContactName;
            }
            set
            {
                if (_customerContactName != value)
                {
                    _customerContactName = value;
                    base.OnPropertyChanged("CustomerContactName", value);
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
        /// GetHashCode
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

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
    /// </summary>
    [Serializable]
    [KnownType(typeof(DryCargo))]
    [KnownType(typeof(ReeferCargo))]
    [KnownType(typeof(DangerousCargo))]
    [KnownType(typeof(AwkwardCargo))]
    public partial class AirOrderInfo : AirOrderList
    {
        

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
        [GuidRequired(CMessage = "揽货类型", EMessage = "SalesType")]
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
        /// 揽货人ID
        /// </summary>
        [GuidRequired(CMessage = "揽货人", EMessage = "Sales")]
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

        Guid? _salesdepartmentid;

        /// <summary>
        /// 揽货部门ID
        /// </summary>
        [GuidRequired(CMessage = "揽货部门", EMessage = "SalesDepartment")]
        public Guid? SalesDepartmentID
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

        #region 装货港

        Guid? _polid;
        /// <summary>
        /// 装货港ID
        /// </summary>
        [GuidRequired(CMessage = "装货港", EMessage = "POL")]
        public Guid? POLID
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

        Guid? _podid;
        /// <summary>
        /// 卸货港ID
        /// </summary>
        [GuidRequired(CMessage = "卸货港", EMessage = "POD")]
        public Guid? PODID
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
                    try
                    {
                        return (CargoType)Enum.Parse(typeof(CargoType), this.CargoDescription.Type.Replace("Cargo", string.Empty));
                    }
                    catch { }
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

        //#region 箱信息(集装箱描述)

        //ContainerDescription _containerdescription;
        ///// <summary>
        ///// 箱信息(集装箱描述)
        ///// </summary>
        //public ContainerDescription ContainerDescription
        //{
        //    get
        //    {
        //        return _containerdescription;
        //    }
        //    set
        //    {
        //        if (_containerdescription != value)
        //        {
        //            _containerdescription = value;
        //            base.OnPropertyChanged("ContainerDescription", value);
        //        }
        //    }
        //}

        //#endregion

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

        #region 航空公司

        Guid? _airCompanyID;
        /// <summary>
        /// 航空公司ID
        /// </summary>
        public Guid? AirCompanyID
        {
            get
            {
                return _airCompanyID;
            }
            set
            {
                if (_airCompanyID != value)
                {
                    _airCompanyID = value;
                    base.OnPropertyChanged("AirCompanyID", value);
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

        #region 仓库
        public Guid? WareHouseID
        {
            get;
            set;
        }
        public string WareHouseName
        {
            get;
            set;
        }
        #endregion

        #region 报关行
        public Guid? CustomsID
        {
            get;
            set;
        }
        public string CustomsName
        {
            get;
            set;
        }
        #endregion

        #region HBL

        #region 放货类型

        FCMReleaseType? _hblreleasetype;
        /// <summary>
        /// House B/L放货类型（0正本，1电放）
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

        #endregion
    }

    #endregion

    #region Fee

    /// <summary>
    /// AirBookingFeeList
    /// </summary>
    [Serializable]
    public partial class AirImportFeeList : BaseDataObject
    {

        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

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

        #region 费用ID
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
        #endregion

        #region 业务ID
        Guid _oibookingid;
        /// <summary>
        /// 订舱ID
        /// </summary>
        public Guid OIBookingID
        {
            get
            {
                return _oibookingid;
            }
            set
            {
                if (_oibookingid != value)
                {
                    _oibookingid = value;
                    base.OnPropertyChanged("OIBookingID", value);
                }
            }
        }
        #endregion

        #region 客户
        Guid _customerid;
        /// <summary>
        /// 客户
        /// </summary>
        [GuidRequired(CMessage = "费用客户", EMessage = "Customer")]
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

        #region 费用
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
        /// 费用名称
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
        #endregion

        #region 币种
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
        #endregion

        #region 数量
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
        #endregion

        #region 单价
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
        #endregion

        #region 收/付类型
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
        #endregion

        #region 金额
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
        #endregion

        #region 备注
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
        #endregion

        #region 保存人
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

        #endregion

        #region 更新时间
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


        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    #endregion
}
