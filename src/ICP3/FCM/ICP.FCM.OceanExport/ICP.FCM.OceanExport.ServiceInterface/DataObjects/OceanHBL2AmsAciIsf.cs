using System;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects
{
    /// <summary>
    /// OceanHBL2AmsAciIsf:实体类
    /// </summary>
    [Serializable]
    public partial class OceanHBL2AmsAciIsf : BaseDataObject
    {
        public OceanHBL2AmsAciIsf()
        { }
        #region Model
        private Guid _id;
        private Guid _oceanhblid;
        private string _vesselName;
        private string _imo;
        private Guid? _flag;
        private string _flagName;
        private string _mark;
        private CustomerDescriptionForAMS _shipper = new CustomerDescriptionForAMS();
        private CustomerDescriptionForAMS _consignee = new CustomerDescriptionForAMS();
        private CustomerDescriptionForAMS _notifyparty = new CustomerDescriptionForAMS();
        private CustomerDescriptionForAMS _seller = new CustomerDescriptionForAMS();
        private CustomerDescriptionForAMS _buyer = new CustomerDescriptionForAMS();
        private CustomerDescriptionForAMS _shipto = new CustomerDescriptionForAMS();
        private CustomerDescriptionForAMS _manufacturer = new CustomerDescriptionForAMS();
        private CustomerDescriptionForAMS _stuffinglocation = new CustomerDescriptionForAMS();
        private CustomerDescriptionForAMS _consolidator = new CustomerDescriptionForAMS();
        private CustomerDescriptionForAMS _bookingPartyInfo = new CustomerDescriptionForAMS();
        private string _shipperDesc;
        private string _consigneeDesc;
        private string _notifypartyDesc;
        private string _sellerDesc;
        private string _buyerDesc;
        private string _shiptoDesc;
        private string _manufacturerDesc;
        private string _stuffinglocationDesc;
        private string _consolidatorDesc;
        private string _bookingPartyInfoDesc;
        private ImportRefType? _isfimporteridtype;
        private string _isfimporterid;
        private string _isfimporterfirstname;
        private string _isfimporterlastname;
        private DateTime? _isfimporterdateofbirth;
        private Guid? _isfimportercountryofissuance;
        private string _isfimportercountryofissuanceName;
        private CargoTypeForAMS? _cargoTypeForAMS;
        private BondRef? _bondreferencetype;
        private string _bondreferencenumber;
        private BondActivityCode? _bondactivitycode;
        private ShipmentType? _shipmenttype;
        private ConsigneeAndBuyerType? _importerofrecordnumberqualifier;
        private string _importerofrecordnumber;
        private string _importerofrecordfirstname;
        private string _importerofrecordlastname;
        private DateTime? _importerofrecorddob;
        private Guid? _importerofpassportissuancecountry;
        private string _importerofpassportissuancecountryName;
        private ConsigneeAndBuyerType? _consigneenumberqualifier;
        private string _consigneenumber;
        private string _consigneefirstname;
        private string _consigneelastname;
        private DateTime? _consigneepassportdob;
        private Guid? _consigneepassportissuancecountry;
        private string _consigneepassportissuancecountryName;
        private List<ContainerForAMS> _container = null;
        private List<ContainerDetailsForAMS> _containerDetailsForAMS = null;
        private Guid _createBy;
        private DateTime _createDate;
        private Guid? _updateBy;
        private DateTime? _updateDate;
        private string _voyageNumber;
        private string _lastPortOfCall;
        private string _firstPorOtfCall;
        private string _portofloading;
        private DateTime? _etd;
        private DateTime? _firstPortOfCallDate;
        /// <summary>
        /// 到达美国第一个港口的时间
        /// </summary>
        public DateTime? FirstPortOfCallDate
        {
            get { return _firstPortOfCallDate; }
            set
            {
                if (_firstPortOfCallDate != value)
                {
                    _firstPortOfCallDate = value;
                    base.OnPropertyChanged("FirstPortOfCallDate", value);
                }
            }
        }

        /// <summary>
        /// 装货时间
        /// </summary>
        public DateTime? Etd
        {
            get { return _etd; }
            set
            {
                if (_etd != value)
                {
                    _etd = value;
                    base.OnPropertyChanged("Portofloading", value);
                }
            }
        }
        /// <summary>
        /// 装货港
        /// </summary>
        public string PortOfLoading
        {
            get { return _portofloading; }
            set
            {
                if (_portofloading != value)
                {
                    _portofloading = value;
                    base.OnPropertyChanged("Portofloading", value);
                }
            }
        }

        /// <summary>
        /// 进入美国之前最后一个港口
        /// </summary>
        public string LastPortOfCall
        {
            get { return _lastPortOfCall; }
            set
            {
                if (_lastPortOfCall != value)
                {
                    _lastPortOfCall = value;
                    base.OnPropertyChanged("LastPortOfCall", value );
                }
            }
        }
        /// <summary>
        /// 进入美国之后第一个港口
        /// </summary>
        public string FirstPorOtfCall
        {
            get { return _firstPorOtfCall; }
            set
            {
                if (_firstPorOtfCall != value)
                {
                    _firstPorOtfCall = value;
                    base.OnPropertyChanged("FirstPorOtfCall", value);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public Guid ID
        {
            set { _id = value; }
            get { return _id; }
        }

        public string ConsigneePassportIssuanceCountryName
        {
            get { return _consigneepassportissuancecountryName; }
            set
            {
                if (_consigneepassportissuancecountryName != value)
                {
                    _consigneepassportissuancecountryName = value;
                    base.OnPropertyChanged("ConsigneePassportIssuanceCountryName", value);
                }
            }
        }

        public string ImporterOfPassportIssuanceCountryName
        {
            get { return _importerofpassportissuancecountryName; }
            set
            {
                if (_importerofpassportissuancecountryName != value)
                {
                    _importerofpassportissuancecountryName = value;
                    base.OnPropertyChanged("ImporterOfPassportIssuanceCountryName", value);
                }
            }
        }

        public string IsfImporterCountryOfIssuanceName
        {
            get { return _isfimportercountryofissuanceName; }
            set
            {
                if (_isfimportercountryofissuanceName != value)
                {
                    _isfimportercountryofissuanceName = value;
                    base.OnPropertyChanged("IsfImporterCountryOfIssuanceName", value);
                }
            }
        }
        /// <summary>
        /// 申报货物性质 
        /// </summary>
        public CargoTypeForAMS? CargoTypeForAMS
        {
            get { return _cargoTypeForAMS; }
            set
            {
                if (_cargoTypeForAMS != value)
                {
                    _cargoTypeForAMS = value;
                    base.OnPropertyChanged("CargoTypeForAMS", value);
                }
            }
        }

        /// <summary>
        /// fcm.oceanHBLs_id
        /// </summary>
        public Guid OceanHBLID
        {
            set
            {
                if (_oceanhblid != value)
                {
                    _oceanhblid = value;
                    base.OnPropertyChanged("OceanHBLID", value);
                }
            }
            get { return _oceanhblid; }
        }

        public string VesselName
        {
            get { return _vesselName; }
            set
            {
                if (_vesselName != value)
                {
                    _vesselName = value;
                    base.OnPropertyChanged("VesselName", value);
                }
            }
        }
        public string IMO
        {
            get { return _imo; }
            set
            {
                if (_imo != value)
                {
                    _imo = value;
                    base.OnPropertyChanged("IMO", value);
                }
            }
        }
        public Guid? Flag
        {
            get { return _flag; }
            set
            {
                if (_flag != value)
                {
                    _flag = value;
                    base.OnPropertyChanged("Flag", value);
                }
            }
        }
        public string FlagName
        {
            get { return _flagName; }
            set
            {
                if (_flagName != value)
                {
                    _flagName = value;
                    base.OnPropertyChanged("FlagName", value);
                }
            }
        }

        /// <summary>
        /// 航次
        /// </summary>
        public string VoyageNumber
        {
            get { return _voyageNumber; }
            set
            {
                if (_voyageNumber != value)
                {
                    _voyageNumber = value;
                    base.OnPropertyChanged("VoyageNumber", value);
                }
            }
        }

        /// <summary>
        /// value=A/B/C/D/E/F/G
        /// </summary>
        public string Mark
        {
            set
            {
                if (_mark != value)
                {
                    _mark = value;
                    base.OnPropertyChanged("Mark", value);
                }
            }
            get { return _mark; }
        }
        /// <summary>
        /// 发货人
        /// </summary>
        public CustomerDescriptionForAMS Shipper
        {
            set
            {
                if (_shipper != value)
                {
                    _shipper = value;
                    base.OnPropertyChanged("Shipper", value);
                }
            }
            get { return _shipper; }
        }
        /// <summary>
        /// 收货人
        /// </summary>
        public CustomerDescriptionForAMS Consignee
        {
            set
            {
                if (_consignee != value)
                {
                    _consignee = value;
                    base.OnPropertyChanged("Consignee", value);
                }
            }
            get { return _consignee; }
        }
        /// <summary>
        /// 通知人（一般情况可不填此列）
        /// </summary>
        public CustomerDescriptionForAMS NotifyParty
        {
            set
            {
                if (_notifyparty != value)
                {
                    _notifyparty = value;
                    base.OnPropertyChanged("NotifyParty", value);
                }
            }
            get { return _notifyparty; }
        }
        /// <summary>
        /// 出口商
        /// </summary>
        public CustomerDescriptionForAMS Seller
        {
            set
            {
                if (_seller != value)
                {
                    _seller = value;
                    base.OnPropertyChanged("Seller", value);
                }
            }
            get { return _seller; }
        }
        /// <summary>
        /// 买家
        /// </summary>
        public CustomerDescriptionForAMS Buyer
        {
            set
            {
                if (_buyer != value)
                {
                    _buyer = value;
                    base.OnPropertyChanged("Buyer", value);
                }
            }
            get { return _buyer; }
        }
        /// <summary>
        /// 送货地点
        /// </summary>
        public CustomerDescriptionForAMS ShipTo
        {
            set
            {
                if (_shipto != value)
                {
                    _shipto = value;
                    base.OnPropertyChanged("ShipTo", value);
                }
            }
            get { return _shipto; }
        }
        /// <summary>
        /// 制造商
        /// </summary>
        public CustomerDescriptionForAMS Manufacturer
        {
            set
            {
                if (_manufacturer != value)
                {
                    _manufacturer = value;
                    base.OnPropertyChanged("Manufacturer", value);
                }
            }
            get { return _manufacturer; }
        }
        /// <summary>
        /// 装箱地点
        /// </summary>
        public CustomerDescriptionForAMS StuffingLocation
        {
            set
            {
                if (_stuffinglocation != value)
                {
                    _stuffinglocation = value;
                    base.OnPropertyChanged("StuffingLocation", value);
                }
            }
            get { return _stuffinglocation; }
        }
        /// <summary>
        /// 装箱整合者
        /// </summary>
        public CustomerDescriptionForAMS Consolidator
        {
            set
            {
                if (_consolidator != value)
                {
                    _consolidator = value;
                    base.OnPropertyChanged("Consolidator", value);
                }
            }
            get { return _consolidator; }
        }

        /// <summary>
        /// 订舱人
        /// </summary>
        public CustomerDescriptionForAMS BookingPartyInfo
        {
            get { return _bookingPartyInfo; }
            set
            {
                if (_bookingPartyInfo != value)
                {
                    _bookingPartyInfo = value;
                    base.OnPropertyChanged("BookingPartyInfo", value);
                }
            }
        }

        /// <summary>
        /// 发货人详细
        /// </summary>
        public string ShipperDesc
        {
            set
            {
                _shipperDesc = value;
            }
            get { return _shipperDesc; }
        }
        /// <summary>
        /// 收货人详细
        /// </summary>
        public string ConsigneeDesc
        {
            set
            {
                _consigneeDesc = value;
            }
            get { return _consigneeDesc; }
        }
        /// <summary>
        /// 通知人详细（一般情况可不填此列）
        /// </summary>
        public string NotifyPartyDesc
        {
            set
            {
                _notifypartyDesc = value;
            }
            get { return _notifypartyDesc; }
        }
        /// <summary>
        /// 出口商详细
        /// </summary>
        public string SellerDesc
        {
            set
            {
                _sellerDesc = value;
            }
            get { return _sellerDesc; }
        }
        /// <summary>
        /// 买家详细
        /// </summary>
        public string BuyerDesc
        {
            set
            {
                _buyerDesc = value;
            }
            get { return _buyerDesc; }
        }
        /// <summary>
        /// 送货地点详细
        /// </summary>
        public string ShipToDesc
        {
            set
            {
                _shiptoDesc = value;
            }
            get { return _shiptoDesc; }
        }
        /// <summary>
        /// 制造商详细
        /// </summary>
        public string ManufacturerDesc
        {
            set
            {
                _manufacturerDesc = value;
            }
            get { return _manufacturerDesc; }
        }
        /// <summary>
        /// 装箱地点详细
        /// </summary>
        public string StuffingLocationDesc
        {
            set
            {
                _stuffinglocationDesc = value;
            }
            get { return _stuffinglocationDesc; }
        }
        /// <summary>
        /// 装箱整合者详细
        /// </summary>
        public string ConsolidatorDesc
        {
            set
            {
                _consolidatorDesc = value;
            }
            get { return _consolidatorDesc; }
        }

        /// <summary>
        /// 订舱人详细
        /// </summary>
        public string BookingPartyInfoDesc
        {
            get { return _bookingPartyInfoDesc; }
            set { _bookingPartyInfoDesc = value; }
        }
        /// <summary>
        /// 进口商税号类型
        /// </summary>
        public ImportRefType? ISFImporterIDType
        {
            set
            {
                if (_isfimporteridtype != value)
                {
                    _isfimporteridtype = value;
                    base.OnPropertyChanged("ISFImporterIDType", value);
                }
            }
            get { return _isfimporteridtype; }
        }
        /// <summary>
        /// 进口商税号
        /// </summary>
        public string ISFImporterID
        {
            set
            {
                if (_isfimporterid != value)
                {
                    _isfimporterid = value;
                    base.OnPropertyChanged("ISFImporterID", value);
                }
            }
            get { return _isfimporterid; }
        }
        /// <summary>
        /// 进口商名称1
        /// </summary>
        public string ISFImporterFirstName
        {
            set
            {
                if (_isfimporterfirstname != value)
                {
                    _isfimporterfirstname = value;
                    base.OnPropertyChanged("ISFImporterFirstName", value);
                }
            }
            get { return _isfimporterfirstname; }
        }
        /// <summary>
        /// 进口商名称2
        /// </summary>
        public string ISFImporterLastName
        {
            set
            {
                if (_isfimporterlastname != value)
                {
                    _isfimporterlastname = value;
                    base.OnPropertyChanged("ISFImporterLastName", value);
                }
            }
            get { return _isfimporterlastname; }
        }
        /// <summary>
        /// 进口商税号签发日期
        /// </summary>
        public DateTime? ISFImporterDateOfBirth
        {
            set
            {
                if (_isfimporterdateofbirth != value)
                {
                    _isfimporterdateofbirth = value;
                    base.OnPropertyChanged("ISFImporterDateOfBirth", value);
                }
            }
            get { return _isfimporterdateofbirth; }
        }
        /// <summary>
        /// 进口商税号签发国
        /// </summary>
        public Guid? ISFImporterCountryOfIssuance
        {
            set
            {
                if (_isfimportercountryofissuance != value)
                {
                    _isfimportercountryofissuance = value;
                    base.OnPropertyChanged("ISFImporterCountryOfIssuance", value);
                }
            }
            get { return _isfimportercountryofissuance; }
        }
        /// <summary>
        /// 进口商税号类型
        /// </summary>
        public BondRef? BondReferenceType
        {
            set
            {
                if (_bondreferencetype != value)
                {
                    _bondreferencetype = value;
                    base.OnPropertyChanged("BondReferenceType", value);
                }
            }
            get { return _bondreferencetype; }
        }
        /// <summary>
        /// 进口商税号
        /// </summary>
        public string BondReferenceNumber
        {
            set
            {
                if (_bondreferencenumber != value)
                {
                    _bondreferencenumber = value;
                    base.OnPropertyChanged("BondReferenceNumber", value);
                }
            }
            get { return _bondreferencenumber; }
        }
        /// <summary>
        /// 进口商类型
        /// </summary>
        public BondActivityCode? BondActivityCode
        {
            set
            {
                if (_bondactivitycode != value)
                {
                    _bondactivitycode = value;
                    base.OnPropertyChanged("BondActivityCode", value);
                }
            }
            get { return _bondactivitycode; }
        }
        /// <summary>
        /// 装箱类型
        /// </summary>
        public ShipmentType? ShipmentType
        {
            set
            {
                if (_shipmenttype != value)
                {
                    _shipmenttype = value;
                    base.OnPropertyChanged("ShipmentType", value);
                }
            }
            get { return _shipmenttype; }
        }
        /// <summary>
        /// 买家税号类型
        /// </summary>
        public ConsigneeAndBuyerType? ImporterOfRecordNumberQualifier
        {
            set
            {
                if (_importerofrecordnumberqualifier != value)
                {
                    _importerofrecordnumberqualifier = value;
                    base.OnPropertyChanged("ImporterOfRecordNumberQualifier", value);
                }
            }
            get { return _importerofrecordnumberqualifier; }
        }
        /// <summary>
        /// 买家税号
        /// </summary>
        public string ImporterOfRecordNumber
        {
            set
            {
                if (_importerofrecordnumber != value)
                {
                    _importerofrecordnumber = value;
                    base.OnPropertyChanged("ImporterOfRecordNumber", value);
                }
            }
            get { return _importerofrecordnumber; }
        }
        /// <summary>
        /// 买家名称1
        /// </summary>
        public string ImporterOfRecordFirstName
        {
            set
            {
                if (_importerofrecordfirstname != value)
                {
                    _importerofrecordfirstname = value;
                    base.OnPropertyChanged("ImporterOfRecordFirstName", value);
                }
            }
            get { return _importerofrecordfirstname; }
        }
        /// <summary>
        /// 买家名称2
        /// </summary>
        public string ImporterOfRecordLastName
        {
            set
            {
                if (_importerofrecordlastname != value)
                {
                    _importerofrecordlastname = value;
                    base.OnPropertyChanged("ImporterOfRecordLastName", value);
                }
            }
            get { return _importerofrecordlastname; }
        }
        /// <summary>
        /// 买家税号签发日期
        /// </summary>
        public DateTime? ImporterOfRecordDOB
        {
            set
            {
                if (_importerofrecorddob != value)
                {
                    _importerofrecorddob = value;
                    base.OnPropertyChanged("ImporterOfRecordDOB", value);
                }
            }
            get { return _importerofrecorddob; }
        }
        /// <summary>
        /// 买家税号签发国
        /// </summary>
        public Guid? ImporterOfPassportIssuanceCountry
        {
            set
            {
                if (_importerofpassportissuancecountry != value)
                {
                    _importerofpassportissuancecountry = value;
                    base.OnPropertyChanged("ImporterOfPassportIssuanceCountry", value);
                }
            }
            get { return _importerofpassportissuancecountry; }
        }
        /// <summary>
        /// 收货人税号类型
        /// </summary>
        public ConsigneeAndBuyerType? ConsigneeNumberQualifier
        {
            set
            {
                if (_consigneenumberqualifier != value)
                {
                    _consigneenumberqualifier = value;
                    base.OnPropertyChanged("ConsigneeNumberQualifier", value);
                }
            }
            get { return _consigneenumberqualifier; }
        }
        /// <summary>
        /// 收货人税号
        /// </summary>
        public string ConsigneeNumber
        {
            set
            {
                if (_consigneenumber != value)
                {
                    _consigneenumber = value;
                    base.OnPropertyChanged("ConsigneeNumber", value);
                }
            }
            get { return _consigneenumber; }
        }
        /// <summary>
        /// 收货人名称1
        /// </summary>
        public string ConsigneeFirstName
        {
            set
            {
                if (_consigneefirstname != value)
                {
                    _consigneefirstname = value;
                    base.OnPropertyChanged("ConsigneeFirstName", value);
                }
            }
            get { return _consigneefirstname; }
        }
        /// <summary>
        /// 收货人名称2
        /// </summary>
        public string ConsigneeLastName
        {
            set
            {
                if (_consigneelastname != value)
                {
                    _consigneelastname = value;
                    base.OnPropertyChanged("ConsigneeLastName", value);
                }
            }
            get { return _consigneelastname; }
        }
        /// <summary>
        /// 收货人税号签发日期
        /// </summary>
        public DateTime? ConsigneePassportDOB
        {
            set
            {
                if (_consigneepassportdob != value)
                {
                    _consigneepassportdob = value;
                    base.OnPropertyChanged("ConsigneePassportDOB", value);
                }
            }
            get { return _consigneepassportdob; }
        }
        /// <summary>
        /// 收货人税号签发国
        /// </summary>
        public Guid? ConsigneePassportIssuanceCountry
        {
            set
            {
                if (_consigneepassportissuancecountry != value)
                {
                    _consigneepassportissuancecountry = value;
                    base.OnPropertyChanged("ConsigneePassportIssuanceCountry", value);
                }
            }
            get { return _consigneepassportissuancecountry; }
        }

        /// <summary>
        /// 箱信息xml
        /// </summary>
        public List<ContainerForAMS> Container
        {
            get { return _container; }
            set
            {
                if (_container != value)
                {
                    _container = value;
                    base.OnPropertyChanged("Container", value);
                }
            }
        }
        /// <summary>
        /// ISF HSCode
        /// </summary>
        public List<ContainerDetailsForAMS> ContainerDetails
        {
            get { return _containerDetailsForAMS; }
            set
            {
                if (_containerDetailsForAMS != value)
                {
                    _containerDetailsForAMS = value;
                    base.OnPropertyChanged("ContainerDetailsForAMS", value);
                }
            }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get { return _updateDate; }
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
        /// 更新人
        /// </summary>
        public Guid? UpdateBy
        {
            get { return _updateBy; }
            set
            {
                if (_updateBy != value)
                {
                    _updateBy = value;
                    base.OnPropertyChanged("UpdateBy", value);
                }
            }
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate
        {
            get { return _createDate; }
            set
            {
                if (_createDate != value)
                {
                    _createDate = value;
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateBy
        {
            get { return _createBy; }
            set
            {
                if (_createBy != value)
                {
                    _createBy = value;
                    base.OnPropertyChanged("CreateBy", value);
                }
            }
        }
        #endregion Model

    }
    /// <summary>
    /// ams箱信息
    /// </summary>
    [Serializable]
    [XmlRoot("Container")]
    public partial class ContainerForAMS : BaseDataObject
    {
        public ContainerForAMS()
        {
            ContainerNumber = string.Empty;
            Seal = string.Empty;
            Kilos = string.Empty;
            Quantity = string.Empty;
            UnitOfMeasure = string.Empty;
            FreeFormDescription = string.Empty;
        }
        /// <summary>
        /// 箱号
        /// </summary>
        [XmlElement("ContainerNumber")]
        public string ContainerNumber { get; set; }
        /// <summary>
        /// 封条号
        /// </summary>
        [XmlElement("Seal")]
        public string Seal { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        [XmlElement("Kilos")]
        public string Kilos { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [XmlElement("Quantity")]
        public string Quantity { get; set; }
        /// <summary>
        /// 数量单位
        /// </summary>
        [XmlElement("UnitOfMeasure")]
        public string UnitOfMeasure { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [XmlElement("FreeFormDescription")]
        public string FreeFormDescription { get; set; }
    }
    /// <summary>
    /// ISF HSCode
    /// </summary>
    [Serializable]
    [XmlRoot("ContainerDetail")]
    public partial class ContainerDetailsForAMS : BaseDataObject
    {
        public ContainerDetailsForAMS()
        {
            HarmonizedTariffCode = string.Empty;
            CountryOfOrigin = Guid.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("HarmonizedTariffCode")]
        public string HarmonizedTariffCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("CountryOfOrigin")]
        public Guid? CountryOfOrigin { get; set; }
        [XmlElement("CountryName")]
        public string CountryName { get; set; }
    }
}

