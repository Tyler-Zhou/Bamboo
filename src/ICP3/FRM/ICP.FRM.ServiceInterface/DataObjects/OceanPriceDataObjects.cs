namespace ICP.FRM.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic; 
    using ICP.Framework.CommonLibrary.Common;
    using System.Xml.Serialization;

    #region 合约

    /// <summary>
    /// 海运合约单位
    /// </summary>
    [Serializable]
    public class OceanUnitList : BaseDataObject
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


        Guid _oceanid;
        /// <summary>
        /// 合约ID
        /// </summary>
        public Guid OceanID
        {
            get
            {
                return _oceanid;
            }
            set
            {
                if (_oceanid != value)
                {
                    _oceanid = value;
                    base.OnPropertyChanged("OceanID", value);
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

        short _rowPosition;
        /// <summary>
        /// 行位置
        /// </summary>
        public short RowPosition
        {
            get
            {
                return _rowPosition;
            }
            set
            {
                if (_rowPosition != value)
                {
                    _rowPosition = value;
                    base.OnPropertyChanged("RowPosition", value);
                }
            }
        }
    }

    /// <summary>
    /// 海运合约列表
    /// </summary>
    [Serializable]
    public class OceanList : BaseDataObject
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


        string _contractno;
        /// <summary>
        /// 合约号
        /// </summary>
        [StringLength(MaximumLength= 50,CMessage="合约名",EMessage="ContractNo")]
        [Required(CMessage = "合约号", EMessage = "ContractNo")]
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


        string _contractname;
        /// <summary>
        /// 合约名
        /// </summary>
        [StringLength(MaximumLength=200,CMessage="合约名",EMessage="ContractName")]
        [Required(CMessage="合约名",EMessage = "ContractName")]
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


        ContractType _ContractType;
        /// <summary>
        /// 合约类型(1:BCO直客约,2:NVOCC,3:全部)
        /// </summary>
        [Required(CMessage="合约类型",EMessage = "ContractType")]
        public ContractType ContractType
        {
            get
            {
                return _ContractType;
            }
            set
            {
                if (_ContractType != value)
                {
                    _ContractType = value;
                    base.OnPropertyChanged("ContractType", value);
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


        string _shippinglinename;
        /// <summary>
        /// 航线
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


        string _shippernames;
        /// <summary>
        /// 发货人
        /// </summary>
        public string ShipperNames
        {
            get
            {
                return _shippernames;
            }
            set
            {
                if (_shippernames != value)
                {
                    _shippernames = value;
                    base.OnPropertyChanged("ShipperNames", value);
                }
            }
        }


        string _consigneenames;
        /// <summary>
        /// 发货人
        /// </summary>
        public string ConsigneeNames
        {
            get
            {
                return _consigneenames;
            }
            set
            {
                if (_consigneenames != value)
                {
                    _consigneenames = value;
                    base.OnPropertyChanged("ConsigneeNames", value);
                }
            }
        }


        string _NotifyPartyNames;
        /// <summary>
        /// 通知人
        /// </summary>
        public string NotifyPartyNames
        {
            get
            {
                return _NotifyPartyNames;
            }
            set
            {
                if (_NotifyPartyNames != value)
                {
                    _NotifyPartyNames = value;
                    base.OnPropertyChanged("NotifyPartyNames", value);
                }
            }
        }


        string _paymenttermname;
        /// <summary>
        /// 通知人
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


        string _currencyname;
        /// <summary>
        /// 币种
        /// </summary>
        public string CurrencyName
        {
            get
            {
                return _currencyname;
            }
            set
            {
                if (_currencyname != value)
                {
                    _currencyname = value;
                    base.OnPropertyChanged("CurrencyName", value);
                }
            }
        }


        DateTime _fromdate;
        /// <summary>
        /// 从
        /// </summary>

        public DateTime FromDate
        {
            get
            {
                return _fromdate;
            }
            set
            {
                if (_fromdate != value)
                {
                    _fromdate = value;
                    base.OnPropertyChanged("FromDate", value);
                }
            }
        }


        DateTime _todate;
        /// <summary>
        /// 到
        /// </summary>

        public DateTime ToDate
        {
            get
            {
                return _todate;
            }
            set
            {
                if (_todate != value)
                {
                    _todate = value;
                    base.OnPropertyChanged("ToDate", value);
                }
            }
        }




        RateType _ratetype;
        /// <summary>
        /// 运价类型(1:Contract,2:Market,3:LCL,4:Selling)
        /// </summary>
        public RateType RateType
        {
            get
            {
                return _ratetype;
            }
            set
            {
                if (_ratetype != value)
                {
                    _ratetype = value;
                    base.OnPropertyChanged("RateType", value);
                }
            }
        }


        string _publishername;
        /// <summary>
        /// 发布人
        /// </summary>
        public string PublisherName
        {
            get
            {
                return _publishername;
            }
            set
            {
                if (_publishername != value)
                {
                    _publishername = value;
                    base.OnPropertyChanged("PublisherName", value);
                }
            }
        }


        DateTime? _publishdate;
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? PublishDate
        {
            get
            {
                return _publishdate;
            }
            set
            {
                if (_publishdate != value)
                {
                    _publishdate = value;
                    base.OnPropertyChanged("PublishDate", value);
                }
            }
        }

        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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

        OceanState _state;
        /// <summary>
        /// 状态
        /// </summary>
        public OceanState State
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

        Guid? _publisherid;
        /// <summary>
        /// 发布人ID
        /// </summary>
        public Guid? PublisherID
        {
            get
            {
                return _publisherid;
            }
            set
            {
                if (_publisherid != value)
                {
                    _publisherid = value;
                    base.OnPropertyChanged("PublisherID", value);
                }
            }
        }

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

        DateTime _createdate;
        /// <summary>
        /// 建立时间
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

        Guid? _carrierid;
        /// <summary>
        /// 船东ID
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

        /// <summary>
        /// 运价单位
        /// </summary>
        public List<OceanUnitList> OceanUnits { get; set; }
        


        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            OceanList newObj = obj as OceanList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }

        OceanPermission _Permission;
        /// <summary>
        /// 是否管理员
        /// </summary>
        public OceanPermission Permission
        {
            get
            {
                return _Permission;
            }
            set
            {
                if (_Permission != value)
                {
                    _Permission = value;
                    base.OnPropertyChanged("Permission", value);
                }
            }
        }

        public OceanPermissionMode PermissionMode { get; set; }
    }

    /// <summary>
    /// 海运合约客户列表
    /// </summary>
    [Serializable]
    public class OceanCustomers : BaseDataObject
    {
        public string ShipperName { get; set; }

        Guid? _shipperid;
        /// <summary>
        /// 发货人ID
        /// </summary>
        ///[GuidRequired(ErrorMessage = "Shipper Must Input.")]
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

        public string ConsigneeName { get; set; }

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


        public string NotifyName { get; set; }

        Guid? _notifyid;
        /// <summary>
        /// 通知货人ID
        /// </summary>
        public Guid? NotifyID
        {
            get
            {
                return _notifyid;
            }
            set
            {
                if (_notifyid != value)
                {
                    _notifyid = value;
                    base.OnPropertyChanged("NotifyID", value);
                }
            }
        }
    }

    /// <summary>
    /// 海运合约详细对象
    /// </summary>
    [Serializable]
    public class OceanInfo : OceanList
    {

        Guid? _shippinglineid;
        /// <summary>
        /// 航线ID
        /// </summary>
        [GuidRequired(CMessage="航线",EMessage = "ShipLine")]
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

        #region 只读的收发ID通列表
        /// <summary>
        /// 发货人ID
        /// </summary>
        public Guid?[] ShipperIDs
        {
            get
            {
                if (OceanCustomers == null || OceanCustomers.Count == 0) return new Guid?[] { };
                List<Guid?> temp = new List<Guid?>();

                foreach (var item in OceanCustomers)
                {
                    temp.Add(item.ShipperID);
                }
                return temp.ToArray();
            }
        }

        /// <summary>
        /// 收货人ID
        /// </summary>
        public Guid?[] ConsigneeIDs
        {
            get
            {
                if (OceanCustomers == null || OceanCustomers.Count == 0) return new Guid?[] { };
                List<Guid?> temp = new List<Guid?>();

                foreach (var item in OceanCustomers)
                {
                    temp.Add(item.ConsigneeID);
                }
                return temp.ToArray();
            }
        }


        /// <summary>
        /// 通知人ID
        /// </summary>
        public Guid?[] NotifyIDs
        {
            get
            {
                if (OceanCustomers == null || OceanCustomers.Count == 0) return new Guid?[] { };
                List<Guid?> temp = new List<Guid?>();

                foreach (var item in OceanCustomers)
                {
                    temp.Add(item.NotifyID);
                }
                return temp.ToArray();
            }
        }

        #endregion


        Guid _currencyid;
        /// <summary>
        /// 币种ID
        /// </summary>
        [GuidRequired(CMessage="币种",EMessage = "Currency")]
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


        Guid? _paymenttermid;
        /// <summary>
        /// 付费条款ID
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


        List<OceanCustomers> _oceanCustomers;
        /// <summary>
        /// 客户组
        /// </summary>
        public List<OceanCustomers> OceanCustomers
        {
            get
            {
                return _oceanCustomers;
            }
            set
            {
                if (_oceanCustomers != value)
                {
                    _oceanCustomers = value;
                    base.OnPropertyChanged("OceanCustomers", value);
                }
            }
        }

    }

    #endregion

    #region BasePort & Arbitrary

    /// <summary>
    /// 此类仅作为服务端和客户端中转作用
    /// </summary>
    [Serializable]
    public class BasePortList : BaseDataObject
    {

        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 海运运价ID
        /// </summary>
        public Guid? OceanID { get; set; }

        /// <summary>
        /// 客户组名称
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 客户类型
        /// </summary>
        public AccountType AccountType { get; set; }

        /// <summary>
        /// 船东ID
        /// </summary>
        public Guid? CarrierID { get; set; }

        /// <summary>
        /// 船东
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string CarrierName { get; set; }

        #region Port

        /// <summary>
        /// 装货港ID
        /// </summary>
        public Guid POLID { get; set; }

        /// <summary>
        /// 装货港
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string POLName { get; set; }

        /// <summary>
        /// VIAID
        /// </summary>
        public Guid? VIAID { get; set; }

        /// <summary>
        /// 中转港
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string VIAName { get; set; }

        /// <summary>
        /// 卸货港ID
        /// </summary>
        public Guid PODID { get; set; }

        /// <summary>
        /// 卸货港
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string PODName { get; set; }

        /// <summary>
        /// 交货地ID
        /// </summary>
        public Guid? PlaceOfDeliveryID { get; set; }

        /// <summary>
        /// 交货地
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string PlaceOfDeliveryName { get; set; }

        #endregion

        #region From To
        /// <summary>
        /// 从
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// 到
        /// </summary>
        public DateTime? ToDate { get; set; }

        #endregion

        /// <summary>
        /// Origin Arbitrary
        /// </summary>
        public bool OriginArb { get; set; }

        /// <summary>
        /// Origin Arbitrary
        /// </summary>
        public bool DestArb { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        public string Comm { get; set; }

        /// <summary>
        /// ItemCode
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 运输条款ID
        /// </summary>
        public Guid TransportClauseID { get; set; }

        /// <summary>
        /// 运输条款
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string TransportClauseName { get; set; }

        /// <summary>
        /// OceanItemUnits
        /// </summary>
        [XmlArray("UnitRates")]
        [XmlArrayItem("UnitRate")]
        public List<FrmUnitRateList> UnitRates { get; set; }


        /// <summary>
        /// 附加费描述
        /// </summary>
        public string SurCharge { get; set; }


        /// <summary>
        /// 截关日
        /// </summary>
        public string ClosingDate { get; set; }


        /// <summary>
        /// 航程
        /// </summary>
        public string TransitTime { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        #region CreateInfo

        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID { get; set; }

        /// <summary>
        /// 建立人
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string CreateByName { get; set; }

        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 行版本
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        #endregion

        /// <summary>
        /// No
        /// </summary>
        public int? No { get; set; }
    }

    /// <summary>
    /// 此类仅作为服务端和客户端中转作用
    /// </summary>
    [Serializable]
    public class ArbitraryList : BaseDataObject
    {

        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 海运运价ID
        /// </summary>
        public Guid? OceanID { get; set; }

        /// <summary>
        /// GeographyType
        /// </summary>
        public GeographyType GeographyType { get; set; }

        /// <summary>
        /// 运输方式
        /// </summary>
        public ModeOfTransport ModeOfTransport { get; set; }

        #region Port

        /// <summary>
        /// 装货港ID
        /// </summary>
        public Guid POLID { get; set; }

        /// <summary>
        /// 装货港
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string POLName { get; set; }

        /// <summary>
        /// 卸货港ID
        /// </summary>
        public Guid PODID { get; set; }

        /// <summary>
        /// 卸货港
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string PODName { get; set; }

        #endregion

        /// <summary>
        /// ItemCode
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 运输条款ID
        /// </summary>
        public Guid TransportClauseID { get; set; }

        /// <summary>
        /// 运输条款
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string TransportClauseName { get; set; }

        /// <summary>
        /// OceanItemUnits
        /// </summary>
        [XmlArray("UnitRates")]
        [XmlArrayItem("UnitRate")]
        public List<FrmUnitRateList> UnitRates { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// No
        /// </summary>
        public int? No { get; set; }

        #region CreateInfo

        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID { get; set; }

        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName { get; set; }

        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 行版本
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        #endregion

        /// <summary>
        /// AssociatedType
        /// </summary>
        public GeographyType AssociatedType { get; set; }

    }

    #endregion

    #region AdditionalFess

    /// <summary>
    /// AdditionalFeeList
    /// </summary>
    [Serializable]
    public class AdditionalFeeList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty; } }

        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 关联运价条目
        /// </summary>
        [XmlIgnore]
        public int AssociatedCount { get; set; }

        /// <summary>
        /// 海运运价ID
        /// </summary>
        public Guid OceanID { get; set; }

        /// <summary>
        /// 海运运价项目ID
        /// </summary>
        public List<Guid> BaseRateIDs { get; set; }

        /// <summary>
        /// 费用项目ID
        /// </summary>
        public Guid ChargingCodeID { get; set; }

        /// <summary>
        /// 费用项目代码
        /// </summary>
        [XmlIgnore]
        public string ChargingCode { get; set; }

        /// <summary>
        /// 费用项目代码
        /// </summary>
        [XmlIgnore]
        public string ChargingCodeDescription { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid? CustomerID { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        [XmlIgnore]
        public string CustomerName { get; set; }


        /// <summary>
        /// 币种ID
        /// </summary>
        public Guid CurrencyID { get; set; }


        /// <summary>
        /// 币种
        /// </summary>
        [XmlIgnore]
        public string CurrencyName { get; set; }

        /// <summary>
        /// 运价费用列表
        /// </summary>
        [XmlArray("Rates")]
        [XmlArrayItem("Rate")]
        public List<FrmUnitRateList> UnitRates { get; set; }

        /// <summary>
        /// 百分比
        /// </summary>
        public short Percent { get; set; }

        /// <summary>
        /// 是否特殊费用（如果是特殊费用，那么必须与运价项目关联才有效）
        /// </summary>
        public bool IsSpecialFee { get; set; }

        #region CreateInfo

        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID { get; set; }

        /// <summary>
        /// 建立人
        /// </summary>
        [XmlIgnore]
        public string CreateByName { get; set; }

        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 行版本
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        #endregion

        #region From To
        /// <summary>
        /// 从
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// 到
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }

        #endregion

        public override bool Equals(object obj)
        {
            AdditionalFeeList newObj = obj as AdditionalFeeList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }


    #endregion

    #region BaseRates

    /// <summary>
    /// 此类仅作为服务端和客户端中转作用
    /// </summary>
    [Serializable]
    public class BaseRatesList : BaseDataObject
    {

        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 海运运价ID
        /// </summary>
        public Guid OceanID { get; set; }


        /// <summary>
        /// OriginalArbitraryID
        /// </summary>
        public Guid? OriginalArbitraryID { get; set; }

        /// <summary>
        /// BasePortID
        /// </summary>
        public Guid BasePortID { get; set; }

        /// <summary>
        /// DestinationArbitraryID
        /// </summary>
        public Guid? DestinationArbitraryID { get; set; }

        /// <summary>
        /// OriginalArbitraryNo
        /// </summary>
        public int? OriginalArbitraryNo { get; set; }

        /// <summary>
        /// BasePortNo
        /// </summary>
        public int? BasePortNo { get; set; }

        /// <summary>
        /// DestinationArbitraryNo
        /// </summary>
        public int? DestinationArbitraryNo { get; set; }


        /// <summary>
        /// 客户组名称
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 客户类型
        /// </summary>
        public AccountType AccountType { get; set; }

        /// <summary>
        /// 船东ID
        /// </summary>
        public Guid? CarrierID { get; set; }

        /// <summary>
        /// 船东
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string CarrierName { get; set; }

        #region Port

        /// <summary>
        /// 装货港ID
        /// </summary>
        public Guid POLID { get; set; }

        /// <summary>
        /// 装货港
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string POLName { get; set; }

        /// <summary>
        /// VIAID
        /// </summary>
        public Guid? VIAID { get; set; }

        /// <summary>
        /// 中转港
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string VIAName { get; set; }

        /// <summary>
        /// 卸货港ID
        /// </summary>
        public Guid PODID { get; set; }

        /// <summary>
        /// 卸货港
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string PODName { get; set; }

        /// <summary>
        /// 交货地ID
        /// </summary>
        public Guid PlaceOfDeliveryID { get; set; }

        /// <summary>
        /// 交货地
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string PlaceOfDeliveryName { get; set; }

        #endregion

        #region From To
        /// <summary>
        /// 从
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// 到
        /// </summary>
        public DateTime? ToDate { get; set; }

        #endregion

        /// <summary>
        /// 品名
        /// </summary>
        public string Comm { get; set; }

        /// <summary>
        /// ItemCode
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 运输条款ID
        /// </summary>
        public Guid TransportClauseID { get; set; }

        /// <summary>
        /// 运输条款
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string TransportClauseName { get; set; }

        /// <summary>
        /// OceanItemUnits
        /// </summary>
        [XmlArray("UnitRates")]
        [XmlArrayItem("UnitRate")]
        public List<FrmUnitRateList> UnitRates { get; set; }


        /// <summary>
        /// 附加费描述
        /// </summary>
        public string SurCharge { get; set; }

        /// <summary>
        /// 截关日
        /// </summary>
        public string ClosingDate { get; set; }

        /// <summary>
        /// 航程
        /// </summary>
        public string TransitTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }

        #region CreateInfo

        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID { get; set; }

        /// <summary>
        /// 建立人
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string CreateByName { get; set; }

        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 行版本
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        #endregion
    }

    #endregion

    #region  Permission
    /// <summary>
    /// OceanPermissionList
    /// </summary>
    [Serializable]
    public class OceanPermissionList : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        UserObjectType _Type;
        /// <summary>
        /// UserObjectType
        /// </summary>
        public UserObjectType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    base.OnPropertyChanged("Type", value);
                }
            }
        }


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

        Guid _oceanid;
        /// <summary>
        /// 文件夹ID
        /// </summary>
        [Required(CMessage="合约",EMessage = "OceanID")]
        public Guid OceanID
        {
            get
            {
                return _oceanid;
            }
            set
            {
                if (_oceanid != value)
                {
                    _oceanid = value;
                    base.OnPropertyChanged("OceanID", value);
                }
            }
        }

        OceanPermission _Permission;
        /// <summary>
        /// 是否管理员
        /// </summary>
        public OceanPermission Permission
        {
            get
            {
                return _Permission;
            }
            set
            {
                if (_Permission != value)
                {
                    _Permission = value;
                    base.OnPropertyChanged("Permission", value);
                }
            }
        }

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

        Guid? _userid;
        /// <summary>
        /// 使用对象ID
        /// </summary>
        public Guid? UserID
        {
            get
            {
                return _userid;
            }
            set
            {
                if (_userid != value)
                {
                    _userid = value;
                    base.OnPropertyChanged("UserID", value);
                }
            }
        }


        string _username;
        /// <summary>
        /// 使用对象
        /// </summary>
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    base.OnPropertyChanged("UserName", value);
                }
            }
        }

        Guid? _organizationid;
        /// <summary>
        /// 
        /// </summary>
        public Guid? OrganizationID
        {
            get
            {
                return _organizationid;
            }
            set
            {
                if (_organizationid != value)
                {
                    _organizationid = value;
                    base.OnPropertyChanged("OrganizationID", value);
                }
            }
        }


        string _organizationname;
        /// <summary>
        /// 组织架构
        /// </summary>
        public string OrganizationName
        {
            get
            {
                return _organizationname;
            }
            set
            {
                if (_organizationname != value)
                {
                    _organizationname = value;
                    base.OnPropertyChanged("OrganizationName", value);
                }
            }
        }


        Guid? _jobid;
        /// <summary>
        /// 使用对象ID
        /// </summary>
        public Guid? JobID
        {
            get
            {
                return _jobid;
            }
            set
            {
                if (_jobid != value)
                {
                    _jobid = value;
                    base.OnPropertyChanged("JobID", value);
                }
            }
        }


        string _jobName;
        /// <summary>
        /// 使用对象
        /// </summary>
        public string JobName
        {
            get
            {
                return _jobName;
            }
            set
            {
                if (_jobName != value)
                {
                    _jobName = value;
                    base.OnPropertyChanged("JobName", value);
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
        /// 建立时间
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
        /// 建立时间
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

    #region File

    /// <summary>
    /// OceanFileList
    /// </summary>
    [Serializable]
    public class OceanFileList : BaseDataObject
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


        Guid _oceanid;
        /// <summary>
        /// 海运运价ID
        /// </summary>
        public Guid OceanID
        {
            get
            {
                return _oceanid;
            }
            set
            {
                if (_oceanid != value)
                {
                    _oceanid = value;
                    base.OnPropertyChanged("OceanID", value);
                }
            }
        }


        Guid _fileid;
        /// <summary>
        /// 文件ID
        /// </summary>
        public Guid FileID
        {
            get
            {
                return _fileid;
            }
            set
            {
                if (_fileid != value)
                {
                    _fileid = value;
                    base.OnPropertyChanged("FileID", value);
                }
            }
        }


        string _filename;
        /// <summary>
        /// 文件名
        /// </summary>
        [Required(CMessage="文件名",EMessage = "FileName")]
        public string FileName
        {
            get
            {
                return _filename;
            }
            set
            {
                if (_filename != value)
                {
                    _filename = value;
                    base.OnPropertyChanged("FileName", value);
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
        /// 建立时间
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
        /// 行版本

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

        /// <summary>
        /// 文档权限
        /// </summary>
        public OceanPermission Permission { get; set; }

        public override bool Equals(object obj)
        {
            OceanFileList newObj = obj as OceanFileList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    #endregion

    #region Feeder
    /// <summary>
    /// 邮件消息日志集合
    /// <example>
    /// 
    /// </example>
    /// </summary>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    [System.Xml.Serialization.XmlRootAttribute(IsNullable = false, ElementName = "OceanFeeders")]
    public class OceanFeederCollect
    {
        public OceanFeederCollect()
        {
            this.OceanFeeders = new List<OceanFeederList>();
        }

        [System.Xml.Serialization.XmlElement("OceanFeeder")]
        public List<OceanFeederList> OceanFeeders { get; set; }
    }


    ////[XmlType]
    [Serializable]
    public class OceanFeederList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty; } }



        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        [XmlElement("ID")]
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



        Guid _oceanid;
        /// <summary>
        /// 海运运价ID
        /// </summary>
        [XmlElement("OceanID")]
        public Guid OceanID
        {
            get
            {
                return _oceanid;
            }
            set
            {
                if (_oceanid != value)
                {
                    _oceanid = value;
                    base.OnPropertyChanged("OceanID", value);
                }
            }
        }


        Guid _polid;
        /// <summary>
        /// 装货港ID
        /// </summary>
        [XmlElement("POLID")]
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
        [XmlIgnore]
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

        string _polcode;
        /// <summary>
        /// 装货港代码
        /// </summary>
        [XmlIgnore]
        public string POLCode
        {
            get
            {
                return _polcode;
            }
            set
            {
                if (_polcode != value)
                {
                    _polcode = value;
                    base.OnPropertyChanged("POLCode", value);
                }
            }
        }


        Guid _podid;
        /// <summary>
        /// 卸货港ID
        /// </summary>
        [XmlElement("PODID")]
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
        [XmlIgnore]
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

        string _podcode;
        /// <summary>
        /// 卸货港代码
        /// </summary>
        [XmlIgnore]
        public string PODCode
        {
            get
            {
                return _podcode;
            }
            set
            {
                if (_podcode != value)
                {
                    _podcode = value;
                    base.OnPropertyChanged("PODCode", value);
                }
            }
        }

        string _type;
        /// <summary>
        /// 类型
        /// </summary>
        [StringLength(MaximumLength=2000,CMessage="类型",EMessage="Type")]
        [XmlElement("Type")]
        public string Type
        {
            get
            {
                if (_type == null) return string.Empty;
                else return _type;
            }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    base.OnPropertyChanged("Type", value);
                }
            }
        }

        bool? _isQuotation;
        /// <summary>
        /// 类型
        /// </summary>
        [XmlElement("IsQuotation")]
        public bool? IsQuotation
        {
            get
            {
                if (_isQuotation == null) return false;
                else return _isQuotation;
            }
            set
            {
                if (_isQuotation != value)
                {
                    _isQuotation = value;
                    base.OnPropertyChanged("IsQuotation", value);
                }
            }
        }

        string _comm;
        /// <summary>
        /// 品名
        /// </summary>
        [StringLength(MaximumLength=2000,CMessage="品名",EMessage="Comm")]
        [XmlElement("Comm")]
        public string Comm
        {
            get
            {
                if (_comm == null) return string.Empty;
                else return _comm;
            }
            set
            {
                if (_comm != value)
                {
                    _comm = value;
                    base.OnPropertyChanged("Comm", value);
                }
            }
        }


        Guid _transportclauseid;
        /// <summary>
        /// 运输条款ID
        /// </summary>
        [GuidRequired(CMessage="运输条款",EMessage = "Term")]
        [XmlElement("TransportClauseID")]
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
        /// 运输条款
        /// </summary>
        [XmlIgnore]
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

        List<Guid> _oceanitemids;
        /// <summary>
        /// 海运运价项目ID
        /// </summary>
        [XmlIgnore]
        public List<Guid> OceanItemIDs
        {
            get
            {
                return _oceanitemids;
            }
            set
            {
                if (_oceanitemids != value)
                {
                    _oceanitemids = value;
                    base.OnPropertyChanged("OceanItemIDs", value);
                }
            }
        }





        /// <summary>
        /// 运价费用列表
        /// </summary>
        [XmlArray("UnitRates")]
        [XmlArrayItem("UnitRate")]
        public List<OceanFeederUnitRateList> UnitRates { get; set; }



        bool _isspecialfee;
        /// <summary>
        /// 是否特殊费用（如果是特殊费用，那么必须与运价项目关联才有效）
        /// </summary>
        [XmlElement("IsSpecialFee")]
        public bool IsSpecialFee
        {
            get
            {
                return _isspecialfee;
            }
            set
            {
                if (_isspecialfee != value)
                {
                    _isspecialfee = value;
                    base.OnPropertyChanged("IsSpecialFee", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        [XmlElement("CreateByID")]
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
        [XmlIgnore()]
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
        /// 建立时间
        /// </summary>
        [XmlElement("CreateDate")]
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
        /// 行版本
        /// </summary>
        [XmlElement("UpdateDate")]
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

        [XmlElement("RowIndex")]
        public int RowIndex { get; set; }

        [XmlIgnore()]
        public bool Selected { get; set; }


        public override bool Equals(object obj)
        {
            OceanFeederList newObj = obj as OceanFeederList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }


    [Serializable]
    public class OceanFeederUnitRateList : BaseDataObject
    {
        public override bool Equals(object obj)
        {
            OceanFeederUnitRateList newObj = obj as OceanFeederUnitRateList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }

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


        Guid _oceanFeederID;
        /// <summary>
        /// 驳船费用ID
        /// </summary>
        public Guid OceanFeederID
        {
            get
            {
                return _oceanFeederID;
            }
            set
            {
                if (_oceanFeederID != value)
                {
                    _oceanFeederID = value;
                    base.OnPropertyChanged("OceanFeederID", value);
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
        /// 运价
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



    /// <summary>
    /// 附加费运价关联信息
    /// </summary>
    [Serializable]
    public class AdditionalFee2ItemList
    {
        /// <summary>
        /// 关联表ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 其他费用ID
        /// </summary>
        public Guid AdditionalFeeID { get; set; }

        /// <summary>
        /// 运价ID
        /// </summary>
        public Guid OceanItemID { get; set; }
    }

    /// <summary>
    /// 驳船运价关联信息
    /// </summary>
    [Serializable]
    public class OceanFeeder2ItemList
    {
        /// <summary>
        /// 关联表ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 驳船ID
        /// </summary>
        public Guid OceanFeederID { get; set; }

        /// <summary>
        /// 运价ID
        /// </summary>
        public Guid OceanItemID { get; set; }
    }

    #endregion

    #region FTPServerConfig

    /// <summary>
    /// FTP服务器配置信息
    /// </summary>
    [Serializable]
    public class FTPServerConfig
    {
        public FTPServerConfig()
        {
        }

        public FTPServerConfig(
            string host,
            string user,
            string password,
            string basePath)
        {
            this.Host = host;
            this.User = user;
            this.Password = password;
            this.BasePath = basePath;
        }

        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 本地路径
        /// </summary>
        public string BasePath { get; set; }
    }

    #endregion

    #region SaveCollect

    /// <summary>
    /// 运价集合
    /// <example>
    /// 
    /// </example>
    /// </summary>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    [System.Xml.Serialization.XmlRootAttribute(IsNullable = false, ElementName = "BasePortItems")]
    public class BasePortCollect
    {
        /// <summary>
        /// BasePortCollect
        /// </summary>
        public BasePortCollect()
        {
            this.BasePortItem = new List<BasePortList>();
        }

        /// <summary>
        /// BasePortItems
        /// </summary>
        [System.Xml.Serialization.XmlElement("BasePortItem")]
        public List<BasePortList> BasePortItem { get; set; }


    }

    /// <summary>
    /// 运价集合
    /// <example>
    /// 
    /// </example>
    /// </summary>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    [System.Xml.Serialization.XmlRootAttribute(IsNullable = false, ElementName = "ArbitraryItems")]
    public class ArbitraryCollect
    {
        /// <summary>
        /// BasePortCollect
        /// </summary>
        public ArbitraryCollect()
        {
            this.ArbitraryItem = new List<ArbitraryList>();
        }

        /// <summary>
        /// BasePortItems
        /// </summary>
        [System.Xml.Serialization.XmlElement("ArbitraryItem")]
        public List<ArbitraryList> ArbitraryItem { get; set; }
    }

    /// <summary>
    /// AdditionalFeeCollect
    /// </summary>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    [System.Xml.Serialization.XmlRootAttribute(IsNullable = false, ElementName = "AdditionalFees")]
    public class AdditionalFeeCollect
    {
        /// <summary>
        /// AdditionalFeeCollect
        /// </summary>
        public AdditionalFeeCollect()
        {
            this.AdditionalFees = new List<AdditionalFeeList>();
        }

        /// <summary>
        /// AdditionalFees
        /// </summary>
        [System.Xml.Serialization.XmlElement("AdditionalFee")]
        public List<AdditionalFeeList> AdditionalFees { get; set; }
    }

    /// <summary>
    /// PermissionsCollect
    /// </summary>
    [Serializable]
    public class PermissionsModeCollect
    {
        /// <summary>
        /// Mode
        /// </summary>
        public OceanPermissionMode Mode { get; set; }

        /// <summary>
        /// OceanUpdateDate
        /// </summary>
        public DateTime? OceanUpdateDate { get; set; }
    }


    /// <summary>
    /// PermissionsCollect
    /// </summary>
    [Serializable]
    public class PermissionsCollect
    {
        /// <summary>
        /// orgJobPermissionIds
        /// </summary>
        public List<Guid?> permissionIds { get; set; }
        /// <summary>
        ///  job时为organizationIds,User时为Null
        /// </summary>
        public List<Guid?> organizationIds { get; set; }
        /// <summary>
        /// job时为jobIds,User时为UserID
        /// </summary>
        public List<Guid?> userObjectIDs { get; set; }

        /// <summary>
        /// types
        /// </summary>
        public List<UserObjectType?> types { get; set; }

        /// <summary>
        /// userPermissions
        /// </summary>
        public List<OceanPermission> permissions { get; set; }
        /// <summary>
        /// userUpdateDates
        /// </summary>
        public List<DateTime?> updateDates { get; set; }

    }

    #endregion

    #region OceanSavedResult

    /// <summary>
    /// OceanSavedResult
    /// </summary>
    [Serializable]
    public class OceanSavedResult
    {
        /// <summary>
        /// OceanUpdateDate
        /// </summary>
        public DateTime? OceanUpdateDate { get; set; }

        /// <summary>
        /// BasePortResult
        /// </summary>
        public ManyResult BasePortResult { get; set; }
        /// <summary>
        /// ArbitraryResult
        /// </summary>
        public ManyResult ArbitraryResult { get; set; }

        /// <summary>
        /// AdditionalFeeResult
        /// </summary>
        public ManyResult AdditionalFeeResult { get; set; }

        /// <summary>
        /// PermissionsModeResult
        /// </summary>
        public SingleResultData PermissionsModeResult { get; set; }

        /// <summary>
        /// PermissionsResult
        /// </summary>
        public ManyResult PermissionsResult { get; set; }
    }

    #endregion

    #region Compar
    /// <summary>
    /// 海运运价数据对比
    /// </summary>
    [Serializable]
    public class OceanContractComparDataList
    {
        /// <summary>
        /// 箱型
        /// </summary>
        public List<string> UnitList
        { 
            get; 
            set; 
        }
        /// <summary>
        /// 对比数据
        /// </summary>
        public List<OceanContractCompar> DataList
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 比较合约
    /// </summary>
    [Serializable]
    public class OceanContractCompar
    {
        #region Base
        public string IDS
        {
            get
            {
                if (C1_ID == null && C2_ID == null)
                {
                    return string.Empty;
                }
                else if (C1_ID != null && C2_ID == null)
                {
                    return C1_ID.Value.ToString();
                }
                else if (C1_ID == null && C2_ID != null)
                {
                    return C2_ID.Value.ToString();
                }
                else if (C1_ID != null && C2_ID != null)
                {
                    return C1_ID.Value.ToString() + "," + C2_ID.Value.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string POL
        {
            get
            {
                if (!string.IsNullOrEmpty(this.C1_POL))
                {
                    return C1_POL;
                }
                else if (!string.IsNullOrEmpty(this.C2_POL))
                {
                    return C2_POL;
                }
                return string.Empty;
            }
        }

        public string VIL
        {
            get
            {
                if (!string.IsNullOrEmpty(this.C1_VIL))
                {
                    return C1_VIL;
                }
                else if (!string.IsNullOrEmpty(this.C2_VIL))
                {
                    return C2_VIL;
                }
                return string.Empty;
            }
        }

        public string POD
        {
            get
            {
                if (!string.IsNullOrEmpty(this.C1_POD))
                {
                    return C1_POD;
                }
                else if (!string.IsNullOrEmpty(this.C2_POD))
                {
                    return C2_POD;
                }
                return string.Empty;
            }
        }

        public string Delivery
        {
            get
            {
                if (!string.IsNullOrEmpty(this.C1_Delivery))
                {
                    return C1_Delivery;
                }
                else if (!string.IsNullOrEmpty(this.C2_Delivery))
                {
                    return C2_Delivery;
                }
                return string.Empty;
            }
        }

        public string Term
        {
            get
            {
                if (!string.IsNullOrEmpty(this.C1_Term))
                {
                    return C1_Term;
                }
                else if (!string.IsNullOrEmpty(this.C2_Term))
                {
                    return C2_Term;
                }
                return string.Empty;
            }
        }

        public string ItemCode
        {
            get
            {
                if (!string.IsNullOrEmpty(this.C1_ItemCode))
                {
                    return C1_ItemCode;
                }
                else if (!string.IsNullOrEmpty(this.C2_ItemCode))
                {
                    return C2_ItemCode;
                }
                return string.Empty;
            }
        }

        #endregion

        #region C1_信息

        #region C1_POL
        /// <summary>
        /// C1_POL
        /// </summary>
        public string C1_POL
        {
            get;
            set;
        }
        #endregion

        #region C1_VIL
        /// <summary>
        /// C1_VIL
        /// </summary>
        public string C1_VIL
        {
            get;
            set;
        }
        #endregion

        #region C1_POD
        /// <summary>
        /// C1_POD
        /// </summary>
        public string C1_POD
        {
            get;
            set;
        }
        
        #endregion

        #region C1_Delivery
        /// <summary>
        /// C1_Delivery
        /// </summary>
        public string C1_Delivery
        {
            get;
            set;
        }
        
        #endregion

        #region C1_Term
        /// <summary>
        /// C1_Term
        /// </summary>
        public string C1_Term
        {
            get;
            set;
        }
        #endregion

        #region C1_ItemCode
        /// <summary>
        /// C1_ItemCode
        /// </summary>
        public string C1_ItemCode
        {
            get;
            set;
        }
        
        #endregion

        #region C1_ID
        /// <summary>
        /// C1_ID
        /// </summary>
        public Guid? C1_ID
        {
            get;
            set;
        }
        #endregion

        #region C1_45FR
        decimal? c1_45FR;
        /// <summary>
        ///C1_45FR
        /// </summary>
        public decimal? C1_45FR
        {
            get
            {
                return c1_45FR == null ? 0 : c1_45FR;
            }
            set
            {
                c1_45FR = value;
            }

        }
        #endregion

        #region C1_40RF
        decimal? c1_40RF;
        /// <summary>
        ///C1_40RF
        /// </summary>
        public decimal? C1_40RF
        {
            get
            {
                return c1_40RF == null ? 0 : c1_40RF;
            }
            set
            {
                c1_40RF = value;
            }

        }
        #endregion

        #region C1_45HT
        decimal? c1_45HT;
        /// <summary>
        ///C1_45HT
        /// </summary>
        public decimal? C1_45HT
        {
            get
            {
                return c1_45HT == null ? 0 : c1_45HT;
            }
            set
            {
                c1_45HT = value;
            }
        }
        #endregion

        #region C1_20RF
        decimal? c1_20RF;
        /// <summary>
        ///C1_20RF
        /// </summary>
        public decimal? C1_20RF
        {
            get
            {
                return c1_20RF == null ? 0 : c1_20RF;
            }
            set
            {
                c1_20RF = value;
            }
        }
        #endregion

        #region C1_20HQ
        decimal? c1_20HQ;
        /// <summary>
        ///C1_20HQ
        /// </summary>
        public decimal? C1_20HQ
        {
            get
            {
                return c1_20HQ == null ? 0 : c1_20HQ;
            }
            set
            {
                c1_20HQ = value;
            }
        }
        #endregion

        #region C1_20TK
        decimal? c1_20TK;
        /// <summary>
        ///C1_20TK
        /// </summary>
        public decimal? C1_20TK
        {
            get
            {
                return c1_20TK == null ? 0 : c1_20TK;
            }
            set
            {
                c1_20TK = value;
            }
        }
        #endregion

        #region C1_20GP
        decimal? c1_20GP;
        /// <summary>
        ///C1_20GP
        /// </summary>
        public decimal? C1_20GP
        {
            get
            {
                return c1_20GP == null ? 0 : c1_20GP;
            }
            set
            {
                c1_20GP = value;
            }
        }
        #endregion

        #region C1_40TK
        decimal? c1_40TK;
        /// <summary>
        ///C1_40TK
        /// </summary>
        public decimal? C1_40TK
        {
            get
            {
                return c1_40TK == null ? 0 : c1_40TK;
            }
            set
            {
                c1_40TK = value;
            }
        }
        #endregion

        #region C1_40OT
        decimal? c1_40OT;
        /// <summary>
        ///C1_40OT
        /// </summary>
        public decimal? C1_40OT
        {
            get
            {
                return c1_40OT == null ? 0 : c1_40OT;
            }
            set
            {
                c1_40OT = value;
            }
        }
        #endregion

        #region C1_20FR
        decimal? c1_20FR;
        /// <summary>
        ///C1_20FR
        /// </summary>
        public decimal? C1_20FR
        {
            get
            {
                return c1_20FR == null ? 0 : c1_20FR;
            }
            set
            {
                c1_20FR = value;
            }
        }
        #endregion

        #region C1_45GP
        decimal? c1_45GP;
        /// <summary>
        ///C1_45GP
        /// </summary>
        public decimal? C1_45GP
        {
            get
            {
                return c1_45GP == null ? 0 : c1_45GP;
            }
            set
            {
                c1_45GP = value;
            }
        }
        #endregion

        #region C1_40GP
        decimal? c1_40GP;
        /// <summary>
        ///C1_40GP
        /// </summary>
        public decimal? C1_40GP
        {
            get
            {
                return c1_40GP == null ? 0 : c1_40GP;
            }
            set
            {
                c1_40GP = value;
            }
        }
        #endregion

        #region C1_45RF
        decimal? c1_45RF;
        /// <summary>
        ///C1_45RF
        /// </summary>
        public decimal? C1_45RF
        {
            get
            {
                return c1_45RF == null ? 0 : c1_45RF;
            }
            set
            {
                c1_45RF = value;
            }
        }
        #endregion

        #region C1_20RH
        decimal? c1_20RH;
        /// <summary>
        ///C1_20RH
        /// </summary>
        public decimal? C1_20RH
        {
            get
            {
                return c1_20RH == null ? 0 : c1_20RH;
            }
            set
            {
                c1_20RH = value;
            }
        }
        #endregion

        #region C1_45OT
        decimal? c1_45OT;
        /// <summary>
        ///C1_45OT
        /// </summary>
        public decimal? C1_45OT
        {
            get
            {
                return c1_45OT == null ? 0 : c1_45OT;
            }
            set
            {
                c1_45OT = value;
            }
        }
        #endregion

        #region C1_40NOR
        decimal? c1_40NOR;
        /// <summary>
        ///C1_40NOR
        /// </summary>
        public decimal? C1_40NOR
        {
            get
            {
                return c1_40NOR == null ? 0 : c1_40NOR;
            }
            set
            {
                c1_40NOR = value;
            }
        }
        #endregion

        #region C1_40FR
        decimal? c1_40FR;
        /// <summary>
        ///C1_40FR
        /// </summary>
        public decimal? C1_40FR
        {
            get
            {
                return c1_40FR == null ? 0 : c1_40FR;
            }
            set
            {
                c1_40FR = value;
            }
        }
        #endregion

        #region C1_20OT
        decimal? c1_20OT;
        /// <summary>
        ///C1_20OT
        /// </summary>
        public decimal? C1_20OT
        {
            get
            {
                return c1_20OT == null ? 0 : c1_20OT;
            }
            set
            {
                c1_20OT = value;
            }
        }
        #endregion

        #region C1_45TK
        decimal? c1_45TK;
        /// <summary>
        ///C1_45TK
        /// </summary>
        public decimal? C1_45TK
        {
            get
            {
                return c1_45TK == null ? 0 : c1_45TK;
            }
            set
            {
                c1_45TK = value;
            }
        }
        #endregion

        #region C1_20NOR
        decimal? c1_20NOR;
        /// <summary>
        ///C1_20NOR
        /// </summary>
        public decimal? C1_20NOR
        {
            get
            {
                return c1_20NOR == null ? 0 : c1_20NOR;
            }
            set
            {
                c1_20NOR = value;
            }
        }
        #endregion

        #region C1_40HT
        decimal? c1_40HT;
        /// <summary>
        ///C1_40HT
        /// </summary>
        public decimal? C1_40HT
        {
            get
            {
                return c1_40HT == null ? 0 : c1_40HT;
            }
            set
            {
                c1_40HT = value;
            }
        }
        #endregion

        #region C1_40RH
        decimal? c1_40RH;
        /// <summary>
        ///C1_40RH
        /// </summary>
        public decimal? C1_40RH
        {
            get
            {
                return c1_40RH == null ? 0 : c1_40RH;
            }
            set
            {
                c1_40RH = value;
            }
        }
        #endregion

        #region C1_45RH
        decimal? c1_45RH;
        /// <summary>
        ///C1_45RH
        /// </summary>
        public decimal? C1_45RH
        {
            get
            {
                return c1_45RH == null ? 0 : c1_45RH;
            }
            set
            {
                c1_45RH = value;
            }
        }
        #endregion

        #region C1_45HQ
        decimal? c1_45HQ;
        /// <summary>
        ///C1_45HQ
        /// </summary>
        public decimal? C1_45HQ
        {
            get
            {
                return c1_45HQ == null ? 0 : c1_45HQ;
            }
            set
            {
                c1_45HQ = value;
            }
        }
        #endregion

        #region C1_20HT
        decimal? c1_20HT;
        /// <summary>
        ///C1_20HT
        /// </summary>
        public decimal? C1_20HT
        {
            get
            {
                return c1_20HT == null ? 0 : c1_20HT;
            }
            set
            {
                c1_20HT = value;
            }
        }
        #endregion

        #region C1_40HQ
        decimal? c1_40HQ;
        /// <summary>
        ///C1_40HQ
        /// </summary>
        public decimal? C1_40HQ
        {
            get
            {
                return c1_40HQ == null ? 0 : c1_40HQ;
            }
            set
            {
                c1_40HQ = value;
            }
        }
        #endregion

        #region C1_53HQ
        decimal? c1_53HQ;
        /// <summary>
        ///C1_53HQ
        /// </summary>
        public decimal? C1_53HQ
        {
            get
            {
                return c1_53HQ == null ? 0 : c1_53HQ;
            }
            set
            {
                c1_53HQ = value;
            }
        }
        #endregion

        #endregion

        #region C2_信息

        #region C2_ID
        /// <summary>
        /// C2ID
        /// </summary>
        public Guid? C2_ID
        {
            get;
            set;
        }
        #endregion

        #region C2_POL
        /// <summary>
        /// C2_POL
        /// </summary>
        public string C2_POL
        {
            get;
            set;
        }
        #endregion

        #region C2_VIL
        /// <summary>
        /// C2_VIL
        /// </summary>
        public string C2_VIL
        {
            get;
            set;
        }
        #endregion

        #region C2_POD
        /// <summary>
        /// C2_POD
        /// </summary>
        public string C2_POD
        {
            get;
            set;
        }

        #endregion

        #region C2_Delivery
        /// <summary>
        /// C2_Delivery
        /// </summary>
        public string C2_Delivery
        {
            get;
            set;
        }

        #endregion

        #region C2_Term
        /// <summary>
        /// C2_Term
        /// </summary>
        public string C2_Term
        {
            get;
            set;
        }
        #endregion

        #region C2_ItemCode
        /// <summary>
        /// C2_ItemCode
        /// </summary>
        public string C2_ItemCode
        {
            get;
            set;
        }

        #endregion
        
        #region C2_45FR
        decimal? c2_45FR;
        /// <summary>
        ///C2_45FR
        /// </summary>
        public decimal? C2_45FR
        {
            get
            {
                return c2_45FR == null ? 0 : c2_45FR;
            }
            set
            {
                c2_45FR = value;
            }

        }
        #endregion

        #region C2_40RF
        decimal? c2_40RF;
        /// <summary>
        ///C2_40RF
        /// </summary>
        public decimal? C2_40RF
        {
            get
            {
                return c2_40RF == null ? 0 : c2_40RF;
            }
            set
            {
                c2_40RF = value;
            }

        }
        #endregion

        #region C2_45HT
        decimal? c2_45HT;
        /// <summary>
        ///C2_45HT
        /// </summary>
        public decimal? C2_45HT
        {
            get
            {
                return c2_45HT == null ? 0 : c2_45HT;
            }
            set
            {
                c2_45HT = value;
            }
        }
        #endregion

        #region C2_20RF
        decimal? c2_20RF;
        /// <summary>
        ///C2_20RF
        /// </summary>
        public decimal? C2_20RF
        {
            get
            {
                return c2_20RF == null ? 0 : c2_20RF;
            }
            set
            {
                c2_20RF = value;
            }
        }
        #endregion

        #region C2_20HQ
        decimal? c2_20HQ;
        /// <summary>
        ///C2_20HQ
        /// </summary>
        public decimal? C2_20HQ
        {
            get
            {
                return c2_20HQ == null ? 0 : c2_20HQ;
            }
            set
            {
                c2_20HQ = value;
            }
        }
        #endregion

        #region C2_20TK
        decimal? c2_20TK;
        /// <summary>
        ///C2_20TK
        /// </summary>
        public decimal? C2_20TK
        {
            get
            {
                return c2_20TK == null ? 0 : c2_20TK;
            }
            set
            {
                c2_20TK = value;
            }
        }
        #endregion

        #region C2_20GP
        decimal? c2_20GP;
        /// <summary>
        ///C2_20GP
        /// </summary>
        public decimal? C2_20GP
        {
            get
            {
                return c2_20GP == null ? 0 : c2_20GP;
            }
            set
            {
                c2_20GP = value;
            }
        }
        #endregion

        #region C2_40TK
        decimal? c2_40TK;
        /// <summary>
        ///C2_40TK
        /// </summary>
        public decimal? C2_40TK
        {
            get
            {
                return c2_40TK == null ? 0 : c2_40TK;
            }
            set
            {
                c2_40TK = value;
            }
        }
        #endregion

        #region C2_40OT
        decimal? c2_40OT;
        /// <summary>
        ///C2_40OT
        /// </summary>
        public decimal? C2_40OT
        {
            get
            {
                return c2_40OT == null ? 0 : c2_40OT;
            }
            set
            {
                c2_40OT = value;
            }
        }
        #endregion

        #region C2_20FR
        decimal? c2_20FR;
        /// <summary>
        ///C2_20FR
        /// </summary>
        public decimal? C2_20FR
        {
            get
            {
                return c2_20FR == null ? 0 : c2_20FR;
            }
            set
            {
                c2_20FR = value;
            }
        }
        #endregion

        #region C2_45GP
        decimal? c2_45GP;
        /// <summary>
        ///C2_45GP
        /// </summary>
        public decimal? C2_45GP
        {
            get
            {
                return c2_45GP == null ? 0 : c2_45GP;
            }
            set
            {
                c2_45GP = value;
            }
        }
        #endregion

        #region C2_40GP
        decimal? c2_40GP;
        /// <summary>
        ///C2_40GP
        /// </summary>
        public decimal? C2_40GP
        {
            get
            {
                return c2_40GP == null ? 0 : c2_40GP;
            }
            set
            {
                c2_40GP = value;
            }
        }
        #endregion

        #region C2_45RF
        decimal? c2_45RF;
        /// <summary>
        ///C2_45RF
        /// </summary>
        public decimal? C2_45RF
        {
            get
            {
                return c2_45RF == null ? 0 : c2_45RF;
            }
            set
            {
                c2_45RF = value;
            }
        }
        #endregion

        #region C2_20RH
        decimal? c2_20RH;
        /// <summary>
        ///C2_20RH
        /// </summary>
        public decimal? C2_20RH
        {
            get
            {
                return c2_20RH == null ? 0 : c2_20RH;
            }
            set
            {
                c2_20RH = value;
            }
        }
        #endregion

        #region C2_45OT
        decimal? c2_45OT;
        /// <summary>
        ///C2_45OT
        /// </summary>
        public decimal? C2_45OT
        {
            get
            {
                return c2_45OT == null ? 0 : c2_45OT;
            }
            set
            {
                c2_45OT = value;
            }
        }
        #endregion

        #region C2_40NOR
        decimal? c2_40NOR;
        /// <summary>
        ///C2_40NOR
        /// </summary>
        public decimal? C2_40NOR
        {
            get
            {
                return c2_40NOR == null ? 0 : c2_40NOR;
            }
            set
            {
                c2_40NOR = value;
            }
        }
        #endregion

        #region C2_40FR
        decimal? c2_40FR;
        /// <summary>
        ///C2_40FR
        /// </summary>
        public decimal? C2_40FR
        {
            get
            {
                return c2_40FR == null ? 0 : c2_40FR;
            }
            set
            {
                c2_40FR = value;
            }
        }
        #endregion

        #region C2_20OT
        decimal? c2_20OT;
        /// <summary>
        ///C2_20OT
        /// </summary>
        public decimal? C2_20OT
        {
            get
            {
                return c2_20OT == null ? 0 : c2_20OT;
            }
            set
            {
                c2_20OT = value;
            }
        }
        #endregion

        #region C2_45TK
        decimal? c2_45TK;
        /// <summary>
        ///C2_45TK
        /// </summary>
        public decimal? C2_45TK
        {
            get
            {
                return c2_45TK == null ? 0 : c2_45TK;
            }
            set
            {
                c2_45TK = value;
            }
        }
        #endregion

        #region C2_20NOR
        decimal? c2_20NOR;
        /// <summary>
        ///C2_20NOR
        /// </summary>
        public decimal? C2_20NOR
        {
            get
            {
                return c2_20NOR == null ? 0 : c2_20NOR;
            }
            set
            {
                c2_20NOR = value;
            }
        }
        #endregion

        #region C2_40HT
        decimal? c2_40HT;
        /// <summary>
        ///C2_40HT
        /// </summary>
        public decimal? C2_40HT
        {
            get
            {
                return c2_40HT == null ? 0 : c2_40HT;
            }
            set
            {
                c2_40HT = value;
            }
        }
        #endregion

        #region C2_40RH
        decimal? c2_40RH;
        /// <summary>
        ///C2_40RH
        /// </summary>
        public decimal? C2_40RH
        {
            get
            {
                return c2_40RH == null ? 0 : c2_40RH;
            }
            set
            {
                c2_40RH = value;
            }
        }
        #endregion

        #region C2_45RH
        decimal? c2_45RH;
        /// <summary>
        ///C2_45RH
        /// </summary>
        public decimal? C2_45RH
        {
            get
            {
                return c2_45RH == null ? 0 : c2_45RH;
            }
            set
            {
                c2_45RH = value;
            }
        }
        #endregion

        #region C2_45HQ
        decimal? c2_45HQ;
        /// <summary>
        ///C2_45HQ
        /// </summary>
        public decimal? C2_45HQ
        {
            get
            {
                return c2_45HQ == null ? 0 : c2_45HQ;
            }
            set
            {
                c2_45HQ = value;
            }
        }
        #endregion

        #region C2_20HT
        decimal? c2_20HT;
        /// <summary>
        ///C2_20HT
        /// </summary>
        public decimal? C2_20HT
        {
            get
            {
                return c2_20HT == null ? 0 : c2_20HT;
            }
            set
            {
                c2_20HT = value;
            }
        }
        #endregion

        #region C2_40HQ
        decimal? c2_40HQ;
        /// <summary>
        ///C2_40HQ
        /// </summary>
        public decimal? C2_40HQ
        {
            get
            {
                return c2_40HQ == null ? 0 : c2_40HQ;
            }
            set
            {
                c2_40HQ = value;
            }
        }
        #endregion

        #region C2_53HQ
        decimal? c2_53HQ;
        /// <summary>
        ///C1_53HQ
        /// </summary>
        public decimal? C2_53HQ
        {
            get
            {
                return c2_53HQ == null ? 0 : c2_53HQ;
            }
            set
            {
                c2_53HQ = value;
            }
        }
        #endregion

        #endregion

        #region GAP

        #region Gap_45FR
        /// <summary>
        ///Gap_45FR
        /// </summary>
        public decimal? Gap_45FR
        {
            get
            {
                decimal? gap_45FR = C1_45FR - C2_45FR;
                return gap_45FR;
            }   
        }
        #endregion

        #region Gap_40RF
        /// <summary>
        ///Gap_40RF
        /// </summary>
        public decimal? Gap_40RF
        {
            get
            {
                decimal? gap_40RF = C1_40RF - C2_40RF;
                return gap_40RF;
            }

        }
        #endregion

        #region Gap_45HT
        /// <summary>
        ///Gap_45HT
        /// </summary>
        public decimal? Gap_45HT
        {
            get
            {
                decimal? gap_45HT = C1_45HT - C2_45HT;
                return gap_45HT;
            }
        }
        #endregion

        #region Gap_20RF
        /// <summary>
        ///Gap_20RF
        /// </summary>
        public decimal? Gap_20RF
        {

            get
            {
                decimal? gap_20RF = C1_20RF - C2_20RF;
                return gap_20RF;
            }
        }
        #endregion

        #region Gap_20HQ
        /// <summary>
        ///Gap_20HQ
        /// </summary>
        public decimal? Gap_20HQ
        {
            get
            {
                decimal? gap_20HQ = C1_20HQ - C2_20HQ;
                return gap_20HQ;
            }
        }
        #endregion

        #region Gap_20TK
        /// <summary>
        ///Gap_20TK
        /// </summary>
        public decimal? Gap_20TK
        {
            get
            {
                decimal? gap_20TK = C1_20TK - C2_20TK;
                return gap_20TK;
            }
        }
        #endregion

        #region Gap_20GP
        /// <summary>
        ///Gap_20GP
        /// </summary>
        public decimal? Gap_20GP
        {
            get
            {
                decimal? gap_20GP = C1_20GP - C2_20GP;
                return gap_20GP;
            }
        }
        #endregion

        #region Gap_40TK
        /// <summary>
        ///Gap_40TK
        /// </summary>
        public decimal? Gap_40TK
        {
            get
            {
                decimal? gap_40TK = C1_40TK - C2_40TK;
                return gap_40TK;
            }
        }
        #endregion

        #region Gap_40OT
        /// <summary>
        ///Gap_40OT
        /// </summary>
        public decimal? Gap_40OT
        {
            get
            {
                decimal? gap_40OT = C1_40OT - C2_40OT;
                return gap_40OT;
            }
        }
        #endregion

        #region Gap_20FR
        /// <summary>
        ///Gap_20FR
        /// </summary>
        public decimal? Gap_20FR
        {
            get
            {
                decimal? gap_20FR = C1_20FR - C2_20FR;
                return gap_20FR;
            }
        }
        #endregion

        #region Gap_45GP
        /// <summary>
        ///Gap_45GP
        /// </summary>
        public decimal? Gap_45GP
        {
            get
            {
                decimal? gap_45GP = C1_45GP - C2_45GP;
                return gap_45GP;
            }
        }
        #endregion

        #region Gap_40GP
        /// <summary>
        ///Gap_40GP
        /// </summary>
        public decimal? Gap_40GP
        {
            get
            {
                decimal? gap_40GP = C1_40GP - C2_40GP;
                return gap_40GP;
            }
        }
        #endregion

        #region Gap_45RF
        /// <summary>
        ///Gap_45RF
        /// </summary>
        public decimal? Gap_45RF
        {
            get
            {
                decimal? gap_45RF = C1_45RF - C2_45RF;
                return gap_45RF;
            }
        }
        #endregion

        #region Gap_20RH
        /// <summary>
        ///Gap_20RH
        /// </summary>
        public decimal? Gap_20RH
        {
            get
            {
                decimal? gap_20RH = C1_20RH - C2_20RH;
                return gap_20RH;
            }
        }
        #endregion

        #region Gap_45OT
        /// <summary>
        ///Gap_45OT
        /// </summary>
        public decimal? Gap_45OT
        {
            get
            {
                decimal? gap_45OT = C1_45OT - C2_45OT;
                return gap_45OT;
            }
        }
        #endregion

        #region Gap_40NOR
        /// <summary>
        ///Gap_40NOR
        /// </summary>
        public decimal? Gap_40NOR
        {
            get
            {
                decimal? gap_40NOR = C1_40NOR - C2_40NOR;
                return gap_40NOR;
            }
        }
        #endregion

        #region Gap_40FR
        /// <summary>
        ///Gap_40FR
        /// </summary>
        public decimal? Gap_40FR
        {
            get
            {
                decimal? gap_40FR = C1_40FR - C2_40FR;
                return gap_40FR;
            }
        }
        #endregion

        #region Gap_20OT
        /// <summary>
        ///Gap_20OT
        /// </summary>
        public decimal? Gap_20OT
        {
            get
            {
                decimal? gap_20OT = C1_20OT - C2_20OT;
                return gap_20OT;
            }
        }
        #endregion

        #region Gap_45TK
        /// <summary>
        ///Gap_45TK
        /// </summary>
        public decimal? Gap_45TK
        {
            get
            {
                decimal? gap_45TK = C1_45TK - C2_45TK;
                return gap_45TK;
            }
        }
        #endregion

        #region Gap_20NOR
        /// <summary>
        ///Gap_20NOR
        /// </summary>
        public decimal? Gap_20NOR
        {
            get
            {
                decimal? gap_20NOR = C1_20NOR - C2_20NOR;
                return gap_20NOR;
            }
        }
        #endregion

        #region Gap_40HT
        /// <summary>
        ///Gap_40HT
        /// </summary>
        public decimal? Gap_40HT
        {
            get
            {
                decimal? gap_40HT = C1_40HT - C2_40HT;
                return gap_40HT;
            }
        }
        #endregion

        #region Gap_40RH
        /// <summary>
        ///Gap_40RH
        /// </summary>
        public decimal? Gap_40RH
        {
            get
            {
                decimal? gap_40RH = C1_40RH - C2_40RH;
                return gap_40RH;
            }
        }
        #endregion

        #region Gap_45RH
        /// <summary>
        ///Gap_45RH
        /// </summary>
        public decimal? Gap_45RH
        {
            get
            {
                decimal? gap_45RH = C1_45RH - C2_45RH;
                return gap_45RH;
            }
        }
        #endregion

        #region Gap_45HQ
        /// <summary>
        ///Gap_45HQ
        /// </summary>
        public decimal? Gap_45HQ
        {
            get
            {
                decimal? gap_45HQ = C1_45HQ - C2_45HQ;
                return gap_45HQ;
            }
        }
        #endregion

        #region Gap_20HT
        /// <summary>
        ///Gap_20HT
        /// </summary>
        public decimal? Gap_20HT
        {
            get
            {
                decimal? gap_20HT = C1_20HT - C2_20HT;
                return gap_20HT;
            }
        }
        #endregion

        #region Gap_40HQ
        /// <summary>
        ///Gap_40HQ
        /// </summary>
        public decimal? Gap_40HQ
        {
            get
            {
                decimal? gap_40HQ = C1_40HQ - C2_40HQ;
                return gap_40HQ;
            }
        }
        #endregion

        #region Gap_53HQ
        /// <summary>
        ///Gap_40HQ
        /// </summary>
        public decimal? Gap_53HQ
        {
            get
            {
                decimal? gap_53HQ = C1_53HQ - C2_53HQ;
                return gap_53HQ;
            }
        }
        #endregion

        #endregion
    }
    #endregion


    #region OceanRateFeeDetails
    /// <summary>
    /// 运价费用明细
    /// </summary>
    [Serializable]
    public class OceanRateFeeDetail
    {
        /// <summary>
        /// 合约下箱型列表
        /// </summary>
        public List<string> UnitList
        {
            get;
            set;
        } 

        /// <summary>
        /// BasePort箱型列表
        /// </summary>
        public List<string> BasePortUnitList
        {
            get;
            set;
        }
        /// <summary>
        /// Additional箱型列表
        /// </summary>
        public List<string> AdditionalUnitList
        {
            get;
            set;
        }

        /// <summary>
        ///  AdditionalFee箱型列表
        /// </summary>
        public List<string> AdditionalFeeUnitList
        {
            get;
            set;
        }

        /// <summary>
        /// BasePort信息
        /// </summary>
        public ClientBasePortList BasePortInfo
        {
            get;
            set;
        }
        /// <summary>
        /// ArbitraryList
        /// </summary>
        public List<ClientArbitraryList> Arbitrarys
        {
            get;
            set;
        }
        /// <summary>
        /// Additional Fee
        /// </summary>
        public List<ClientAdditionalFeeList> AdditionalFees
        {
            get;
            set;
        }

    }
    #endregion

}
