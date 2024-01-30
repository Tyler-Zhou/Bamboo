namespace ICP.FCM.AirExport.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Client;

    #region BL

    #region AirBLList 提单列表数据对象

    /// <summary>
    ///  AirBLList 提单列表数据对象
    /// </summary>
    [Serializable]
    public partial class AirBLList : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

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

        /// <summary>
        /// 业务联系人
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
        /// 提单类型
        /// </summary>
        public AWBType AWBType
        {
            get
            {
                if (this.ID == this.MBLID)
                {
                    return AWBType.MAWB;
                }
                else
                {
                    return AWBType.HAWB;
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

        Guid _oceanbookingid;
        /// <summary>
        /// 业务ID
        /// </summary>
        [GuidRequired(CMessage = "业务号", EMessage = "AirBookingNo")]
        public Guid AirBookingID
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
                    base.OnPropertyChanged("AirBookingID", value);
                }
            }
        }

        Guid _mblid;
        /// <summary>
        /// MBLID
        /// </summary>
        public Guid MBLID
        {
            get
            {
                return _mblid;
            }
            set
            {
                if (_mblid != value)
                {
                    _mblid = value;
                    base.OnPropertyChanged("MBLID", value);
                }
            }
        }

        Guid _companyid;
        /// <summary>
        /// CompanyID
        /// </summary>
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

        string _refno;
        /// <summary>
        /// 业务号
        /// </summary>
        public string RefNo
        {
            get
            {
                return _refno;
            }
            set
            {
                if (_refno != value)
                {
                    _refno = value;
                    base.OnPropertyChanged("RefNo", value);
                }
            }
        }


        string _no;
        /// <summary>
        /// 提单号
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


        string _checkername;
        /// <summary>
        /// 对单人
        /// </summary>
        public string CheckerName
        {
            get
            {
                return _checkername;
            }
            set
            {
                if (_checkername != value)
                {
                    _checkername = value;
                    base.OnPropertyChanged("CheckerName", value);
                }
            }
        }


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

        string _notifypartyname;
        /// <summary>
        /// 通知人
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


        string _agentname;
        /// <summary>
        /// 代理
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

        string _airCompanyName;
        /// <summary>
        /// 航空公司Name
        /// </summary>
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

        string _customerName;
        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerName
        {
            get
            {
                return _customerName;
            }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    base.OnPropertyChanged("CustomerName", value);
                }
            }
        }

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

        DateTime? _etd;
        /// <summary>
        /// 起航日
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
                    base.OnPropertyChanged("DetinationName", value);
                }
            }
        }


        DateTime? _eta;
        /// <summary>
        /// 到达日
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

        int _quantity;
        /// <summary>
        /// 包装数量
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

        decimal _grossKGS;
        /// <summary>
        /// 毛重KGS
        /// </summary>
        [Required(CMessage = "毛重", EMessage = "GrossKGS")]
        public decimal GrossKGS
        {
            get
            {
                return _grossKGS;
            }
            set
            {
                if (_grossKGS != value)
                {
                    _grossKGS = value;
                    base.OnPropertyChanged("GrossKGS", value);
                }
            }
        }

        decimal _grossLBS;
        /// <summary>
        /// 毛重LBS
        /// </summary>
        [Required(CMessage = "毛重", EMessage = "GrossLBS")]
        public decimal GrossLBS
        {
            get
            {
                return _grossLBS;
            }
            set
            {
                if (_grossLBS != value)
                {
                    _grossLBS = value;
                    base.OnPropertyChanged("GrossLBS", value);
                }
            }
        }

        decimal _chargeKGS;
        /// <summary>
        /// 计费重量KGS
        /// </summary>
        [Required(CMessage = "计费重量", EMessage = "ChargeKGS")]
        public decimal ChargeKGS
        {
            get
            {
                return _chargeKGS;
            }
            set
            {
                if (_chargeKGS != value)
                {
                    _chargeKGS = value;
                    base.OnPropertyChanged("ChargeKGS", value);
                }
            }
        }

        decimal _chargeLBS;
        /// <summary>
        /// 计费重量LBS
        /// </summary>
        [Required(CMessage = "计费重量", EMessage = "ChargeLBS")]
        public decimal ChargeLBS
        {
            get
            {
                return _chargeLBS;
            }
            set
            {
                if (_chargeLBS != value)
                {
                    _chargeLBS = value;
                    base.OnPropertyChanged("ChargeLBS", value);
                }
            }
        }

        bool _rbld;
        /// <summary>
        /// 是否放单
        /// </summary>
        public bool RBLD
        {
            get 
            {
                return _rbld;
            }
            set 
            {
                if (_rbld != value)
                {
                    _rbld = value;
                    base.OnPropertyChanged("RBLD", value);
                }
            }
        }

        bool _isClosed;
        /// <summary>
        /// 是否关单
        /// </summary>
        public bool IsClosed
        {
            get
            {
                return _isClosed;
            }
            set
            {
                if (_isClosed != value)
                {
                    _isClosed = value;
                    base.OnPropertyChanged("IsClosed", value);
                }
            }
        }

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

        AEBLState _state;
        /// <summary>
        /// 提单状态
        /// </summary>
        public AEBLState State
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
        /// <summary>
        /// 状态名(只读,根据状态和中英文环境返回字串)
        /// </summary>
        public string StateName
        {
            get
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<AEBLState>(_state, LocalData.IsEnglish);
            }
        }

        FCMReleaseType _releasetype;
        /// <summary>
        /// 电放类型
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
        /// 电放类型(只读,根据电放类型和中英文环境返回字串)
        /// </summary>
        public string ReleaseTypeName
        {
            get
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<FCMReleaseType>(_releasetype, LocalData.IsEnglish);
            }
        }

        string _createbyname;
        /// <summary>
        /// 操作
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

        bool _existfees;
        /// <summary>
        /// 是否挂有费用
        /// </summary>
        public bool ExistFees
        {
            get
            {
                return _existfees;
            }
            set
            {
                if (_existfees != value)
                {
                    _existfees = value;
                    base.OnPropertyChanged("ExistFees", value);
                }
            }
        }

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

        Guid? _filightNoID;
        /// <summary>
        /// 航班号ID
        /// </summary>
        public Guid? FilightNoID
        {
            get
            {
                return _filightNoID;
            }
            set
            {
                if (_filightNoID != value)
                {
                    _filightNoID = value;
                    base.OnPropertyChanged("FilightNoID", value);
                }
            }
        }

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

        string _BookingerName;
        /// <summary>
        /// 客服
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

        string _FilerName;
        /// <summary>
        /// 文件
        /// </summary>
        public string FilerName
        {
            get
            {
                return _FilerName;
            }
            set
            {
                if (_FilerName != value)
                {
                    _FilerName = value;
                    base.OnPropertyChanged("FilerName", value);
                }
            }
        }

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

        Guid? _notifypartyid;
        /// <summary>
        /// 通知人ID
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

        int _HBLCount;
        /// <summary>
        /// HBL数量,仅在列表上有这个属性
        /// </summary>
        public int HBLCount
        {
            get
            {
                return _HBLCount;
            }
            set
            {
                if (_HBLCount != value)
                {
                    _HBLCount = value;
                    base.OnPropertyChanged("HBLCount", value);
                }
            }
        }

        bool _Isvalid;
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            get
            {
                return _Isvalid;
            }
            set
            {
                if (_Isvalid != value)
                {
                    _Isvalid = value;
                    base.OnPropertyChanged("IsValid", value);
                }
            }
        }

        Guid? _AirOrderID;
        /// <summary>
        /// 装货单ID
        /// </summary>
        public Guid? AirOrderID
        {
            get
            {
                return _AirOrderID;
            }
            set
            {
                if (_AirOrderID != value)
                {
                    _AirOrderID = value;
                    base.OnPropertyChanged("AirOrderID", value);
                }
            }
        }

        DateTime? _AirOrderUpdateDate;
        /// <summary>
        /// 装货单更新日期
        /// </summary>
        public DateTime? AirOrderUpdateDate
        {
            get
            {
                return _AirOrderUpdateDate;
            }
            set
            {
                if (_AirOrderUpdateDate != value)
                {
                    _AirOrderUpdateDate = value;
                    base.OnPropertyChanged("AirOrderUpdateDate", value);
                }
            }
        }

    }

    #endregion

    #region  AirHBLInfo HBL详细信息

    /// <summary>
    /// AirHBLInfo
    /// </summary>
    [Serializable]
    public partial class AirHBLInfo : AirBLList
    {
        string _mblno;
        /// <summary>
        /// 主提单号
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

        short? _numberoforiginal;
        /// <summary>
        /// 复制份数
        /// </summary>
        public short? NumberOfOriginal
        {
            get
            {
                return _numberoforiginal;
            }
            set
            {
                if (_numberoforiginal != value)
                {
                    _numberoforiginal = value;
                    base.OnPropertyChanged("NumberOfOriginal", value);
                }
            }
        }


        Guid? _checkerid;
        /// <summary>
        /// 对单人ID（关联客户信息）
        /// </summary>
        public Guid? CheckerID
        {
            get
            {
                return _checkerid;
            }
            set
            {
                if (_checkerid != value)
                {
                    _checkerid = value;
                    base.OnPropertyChanged("CheckerID", value);
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

        string _shipperAccountNo;
        /// <summary>
        /// 发货人账号
        /// </summary>
        public string ShipperAccountNo
        {
            get
            {
                return _shipperAccountNo;
            }
            set
            {
                if (_shipperAccountNo != value)
                {
                    _shipperAccountNo = value;
                    base.OnPropertyChanged("ShipperAccountNo", value);
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

        string _consigneeAccountNo;
        /// <summary>
        /// 收货人账号
        /// </summary>
        public string ConsigneeAccountNo
        {
            get
            {
                return _consigneeAccountNo;
            }
            set
            {
                if (_consigneeAccountNo != value)
                {
                    _consigneeAccountNo = value;
                    base.OnPropertyChanged("ConsigneeAccountNo", value);
                }
            }
        }

        CustomerDescription _notifypartydescription;
        /// <summary>
        /// 通知人详细信息
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

        string _agentAccountNo;
        /// <summary>
        /// 代理账号
        /// </summary>
        public string AgentAccountNo
        {
            get
            {
                return _agentAccountNo;
            }
            set
            {
                if (_agentAccountNo != value)
                {
                    _agentAccountNo = value;
                    base.OnPropertyChanged("AgentAccountNo", value);
                }
            }
        }

        DateTime? _MBLUpdateDate;
        /// <summary>
        /// 主提单更新时间
        /// </summary>
        public DateTime? MBLUpdateDate
        {
            get
            {
                return _MBLUpdateDate;
            }
            set
            {
                if (_MBLUpdateDate != value)
                {
                    _MBLUpdateDate = value;
                    base.OnPropertyChanged("MBLUpdateDate", value);
                }
            }
        }

        Guid _departureid;
        /// <summary>
        /// 起运港ID
        /// </summary>
        [GuidRequired(CMessage = "起运港", EMessage = "Departure")]
        public Guid DepartureID
        {
            get
            {
                return _departureid;
            }
            set
            {
                if (_departureid != value)
                {
                    _departureid = value;
                    base.OnPropertyChanged("DepartureID", value);
                }
            }
        }

        string _departurecode;
        /// <summary>
        /// 起运港代码
        /// </summary>
        public string DepartureCode
        {
            get
            {
                return _departurecode;
            }
            set
            {
                if (_departurecode != value)
                {
                    _departurecode = value;
                    base.OnPropertyChanged("DepartureCode", value);
                }
            }
        }

        Guid? _agentofcarrierID;
        /// <summary>
        /// 承运人ID
        /// </summary>
        [GuidRequired(CMessage = "承运人", EMessage = "AgentOfCarrier")]
        public Guid? AgentOfCarrierID
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
                    base.OnPropertyChanged("AgentOfCarrierID", value);
                }
            }
        }

        Guid _airCompanyID;
        /// <summary>
        /// 航空公司ID
        /// </summary>
        [GuidRequired(CMessage = "航空公司", EMessage = "AirCompany")]
        public Guid AirCompanyID
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

        string _transhipmentPort1;
        /// <summary>
        /// 中转港1
        /// </summary>
        public string TranshipmentPort1
        {
            get
            {
                return _transhipmentPort1;
            }
            set
            {
                if (_transhipmentPort1 != value)
                {
                    _transhipmentPort1 = value;
                    base.OnPropertyChanged("TranshipmentPort1", value);
                }
            }
        }

        string _transhipmentPort1By;
        /// <summary>
        /// 中转港1 By
        /// </summary>
        public string TranshipmentPort1By
        {
            get
            {
                return _transhipmentPort1By;
            }
            set
            {
                if (_transhipmentPort1By != value)
                {
                    _transhipmentPort1By = value;
                    base.OnPropertyChanged("TranshipmentPort1By", value);
                }
            }
        }

        string _transhipmentPort2;
        /// <summary>
        /// 中转港2
        /// </summary>
        public string TranshipmentPort2
        {
            get
            {
                return _transhipmentPort2;
            }
            set
            {
                if (_transhipmentPort2 != value)
                {
                    _transhipmentPort2 = value;
                    base.OnPropertyChanged("TranshipmentPort2", value);
                }
            }
        }

        string _transhipmentPort2By;
        /// <summary>
        /// 中转港2 By
        /// </summary>
        public string TranshipmentPort2By
        {
            get
            {
                return _transhipmentPort2By;
            }
            set
            {
                if (_transhipmentPort2By != value)
                {
                    _transhipmentPort2By = value;
                    base.OnPropertyChanged("TranshipmentPort2By", value);
                }
            }
        }

        string _transhipmentPort3;
        /// <summary>
        /// 中转港3
        /// </summary>
        public string TranshipmentPort3
        {
            get
            {
                return _transhipmentPort3;
            }
            set
            {
                if (_transhipmentPort3 != value)
                {
                    _transhipmentPort3 = value;
                    base.OnPropertyChanged("TranshipmentPort3", value);
                }
            }
        }

        string _transhipmentPort3By;
        /// <summary>
        /// 中转港3 By
        /// </summary>
        public string TranshipmentPort3By
        {
            get
            {
                return _transhipmentPort3By;
            }
            set
            {
                if (_transhipmentPort3By != value)
                {
                    _transhipmentPort3By = value;
                    base.OnPropertyChanged("TranshipmentPort3By", value);
                }
            }
        }

        Guid _detinationid;
        /// <summary>
        /// 目的港ID
        /// </summary>
        [GuidRequired(CMessage = "目的港", EMessage = "Detination")]
        public Guid DetinationID
        {
            get
            {
                return _detinationid;
            }
            set
            {
                if (_detinationid != value)
                {
                    _detinationid = value;
                    base.OnPropertyChanged("DetinationID", value);
                }
            }
        }


        string _detinationcode;
        /// <summary>
        /// 目的港代码
        /// </summary>
        public string DetinationCode
        {
            get
            {
                return _detinationcode;
            }
            set
            {
                if (_detinationcode != value)
                {
                    _detinationcode = value;
                    base.OnPropertyChanged("DetinationCode", value);
                }
            }
        }

        Guid? _placeofdeliveryid;
        /// <summary>
        /// 交货地ID
        /// </summary>
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

        string _placeofdeliverycode;
        /// <summary>
        /// 交货地代码
        /// </summary>
        public string PlaceOfDeliveryCode
        {
            get
            {
                return _placeofdeliverycode;
            }
            set
            {
                if (_placeofdeliverycode != value)
                {
                    _placeofdeliverycode = value;
                    base.OnPropertyChanged("PlaceOfDeliveryCode", value);
                }
            }
        }

        Guid? _paymenttermid;
        /// <summary>
        /// 付费条款ID
        /// </summary>
        [GuidRequired(CMessage = "付款方式", EMessage = "PaymentTerm")]
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

        Guid? _Bookingpaymenttermid;
        /// <summary>
        /// Booking中的MBL付款方式ID
        /// </summary>
        public Guid? BookingPaymentTermID
        {
            get
            {
                return _Bookingpaymenttermid;
            }
            set
            {
                if (_Bookingpaymenttermid != value)
                {
                    _Bookingpaymenttermid = value;
                    base.OnPropertyChanged("BookingPaymentTermID", value);
                }
            }
        }

        Guid? _Otherpaymenttermid;
        /// <summary>
        /// 其它付款方式ID
        /// </summary>
        public Guid? OtherPaymentTermID
        {
            get
            {
                return _Otherpaymenttermid;
            }
            set
            {
                if (_Otherpaymenttermid != value)
                {
                    _Otherpaymenttermid = value;
                    base.OnPropertyChanged("OtherPaymentTermID", value);
                }
            }
        }

        string _paymenttermname;
        /// <summary>
        /// 付费条款
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

        string _otherpaymenttermname;
        /// <summary>
        /// 其它付费条款
        /// </summary>
        public string OtherPaymentTermName
        {
            get
            {
                return _otherpaymenttermname;
            }
            set
            {
                if (_otherpaymenttermname != value)
                {
                    _otherpaymenttermname = value;
                    base.OnPropertyChanged("OtherPaymentTermName", value);
                }
            }
        }

        string _declaredValueForCarriage;
        /// <summary>
        /// 运输声明价值
        /// </summary>
        public string DeclaredValueForCarriage
        {
            get
            {
                return _declaredValueForCarriage;
            }
            set
            {
                if (_declaredValueForCarriage != value)
                {
                    _declaredValueForCarriage = value;
                    base.OnPropertyChanged("DeclaredValueForCarriage", value);
                }
            }
        }

        string _DeclaredValueForCustoms;
        /// <summary>
        /// 海关声明价值
        /// </summary>
        public string DeclaredValueForCustoms
        {
            get
            {
                return _DeclaredValueForCustoms;
            }
            set
            {
                if (_DeclaredValueForCustoms != value)
                {
                    _DeclaredValueForCustoms = value;
                    base.OnPropertyChanged("DeclaredValueForCustoms", value);
                }
            }
        }

        string _insuranceAmount;
        /// <summary>
        /// 保险金额
        /// </summary>
        public string InsuranceAmount
        {
            get
            {
                return _insuranceAmount;
            }
            set
            {
                if (_insuranceAmount != value)
                {
                    _insuranceAmount = value;
                    base.OnPropertyChanged("InsuranceAmount", value);
                }
            }
        }

        bool _chargeableWeightUnitIsKGS;
        public bool ChargeableWeightUnitIsKGS
        {
            get
            {
                return _chargeableWeightUnitIsKGS;
            }
            set
            {
                if (_chargeableWeightUnitIsKGS != value)
                {
                    _chargeableWeightUnitIsKGS = value;
                    base.OnPropertyChanged("ChargeableWeightUnitIsKGS", value);
                }
            }
        }

        string _freightdescription;
        /// <summary>
        /// 运费描述
        /// </summary>
        [StringLength(MaximumLength = 500, CMessage = "运费描述", EMessage = "FreightDescription")]
        public string FreightDescription
        {
            get
            {
                return _freightdescription;
            }
            set
            {
                if (_freightdescription != value)
                {
                    _freightdescription = value;
                    base.OnPropertyChanged("FreightDescription", value);
                }
            }
        }

        DateTime? _releasedate;
        /// <summary>
        /// 放单日期
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

        //DateTime? _bLDate;
        ///// <summary>
        ///// 提单日期
        ///// </summary>
        //public DateTime? BLDate
        //{
        //    get
        //    {
        //        return _bLDate;
        //    }
        //    set
        //    {
        //        if (_bLDate != value)
        //        {
        //            _bLDate = value;
        //            base.OnPropertyChanged("BLDate", value);
        //        }
        //    }
        //}

        Guid _quantityunitid;
        /// <summary>
        /// 包装单位ID
        /// </summary>
        [GuidRequired(CMessage = "包装单位", EMessage = "QuantityUnit")]
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
        /// 包装单位
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
        [GuidRequired(CMessage = "体积单位", EMessage = "MeasurementUnit")]
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
        /// 体积单位
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

        Guid _currencyID;
        /// <summary>
        /// 币种
        /// </summary>
        [GuidRequired(CMessage = "币种", EMessage = "Currency")]
        public Guid CurrencyID
        {
            get
            {
                return _currencyID;
            }
            set
            {
                if (_currencyID != value)
                {
                    _currencyID = value;
                    base.OnPropertyChanged("CurrencyID", value);
                }
            }
        }

        decimal _rateCharge;
        /// <summary>
        /// RateCharge
        /// </summary>
        public decimal RateCharge
        {
            get
            {
                return _rateCharge;
            }
            set
            {
                if (_rateCharge != value)
                {
                    _rateCharge = value;
                    base.OnPropertyChanged("RateCharge", value);
                }
            }
        }

        decimal _rateAmount;
        /// <summary>
        /// RateAmount
        /// </summary>
        public decimal RateAmount
        {
            get
            {
                return _rateAmount;
            }
            set
            {
                if (_rateAmount != value)
                {
                    _rateAmount = value;
                    base.OnPropertyChanged("RateAmount", value);
                }
            }
        }

        string _handingInformation;
        /// <summary>
        /// 操作信息
        /// </summary>
        public string HandingInformation
        {
            get
            {
                return _handingInformation;
            }
            set
            {
                if (_handingInformation != value)
                {
                    _handingInformation = value;
                    base.OnPropertyChanged("HandingInformation", value);
                }
            }
        }

        //string _accountInformation;
        ///// <summary>
        ///// 支付信息
        ///// </summary>
        //public string AccountInformation 
        //{
        //    get
        //    {
        //        return _accountInformation;
        //    }
        //    set
        //    {
        //        if (_accountInformation != value)
        //        {
        //            _accountInformation = value;
        //            base.OnPropertyChanged("AccountInformation", value);
        //        }
        //    }
        //}

        public List<OtherChargeItem> OtherChargeList { get; set; }

        string _otherChargeDescription;
        /// <summary>
        /// 其它费用备注
        /// </summary>
        public string OtherChargeDescription
        {
            get
            {
                return _otherChargeDescription;
            }
            set
            {
                if (_otherChargeDescription != value)
                {
                    _otherChargeDescription = value;
                    base.OnPropertyChanged("OtherChargeDescription", value);
                }
            }
        }

        string _marks;
        /// <summary>
        /// 唛头
        /// </summary>
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

        string _goodsdescription;
        /// <summary>
        /// 货物描述
        /// </summary>
        public string GoodsDescription
        {
            get
            {
                return _goodsdescription;
            }
            set
            {
                if (_goodsdescription != value)
                {
                    _goodsdescription = value;
                    base.OnPropertyChanged("GoodsDescription", value);
                }
            }
        }

        decimal _Tax;
        /// <summary>
        /// 税款
        /// </summary>
        public decimal Tax
        {
            get
            {
                return _Tax;
            }
            set
            {
                if (_Tax != value)
                {
                    _Tax = value;
                    base.OnPropertyChanged("Tax", value);
                }
            }
        }

        decimal _valuationCharge;
        /// <summary>
        /// ValuationCharge
        /// </summary>
        public decimal ValuationCharge
        {
            get
            {
                return _valuationCharge;
            }
            set
            {
                if (_valuationCharge != value)
                {
                    _valuationCharge = value;
                    base.OnPropertyChanged("ValuationCharge", value);
                }
            }
        }

        decimal _agentCharger;
        /// <summary>
        /// AgentCharger
        /// </summary>
        public decimal AgentCharger
        {
            get
            {
                return _agentCharger;
            }
            set
            {
                if (_agentCharger != value)
                {
                    _agentCharger = value;
                    base.OnPropertyChanged("AgentCharger", value);
                }
            }
        }

        decimal _carrierCharger;
        /// <summary>
        /// CarrierCharger
        /// </summary>
        public decimal CarrierCharger
        {
            get
            {
                return _carrierCharger;
            }
            set
            {
                if (_carrierCharger != value)
                {
                    _carrierCharger = value;
                    base.OnPropertyChanged("CarrierCharger", value);
                }
            }
        }

        string _currencyConversionRate;
        /// <summary>
        /// CurrencyConversionRate
        /// </summary>
        public string CurrencyConversionRate
        {
            get
            {
                return _currencyConversionRate;
            }
            set
            {
                if (_currencyConversionRate != value)
                {
                    _currencyConversionRate = value;
                    base.OnPropertyChanged("CurrencyConversionRate", value);
                }
            }
        }

        string _destinationCurrencyAmount;
        /// <summary>
        /// DestinationCurrencyAmount
        /// </summary>
        public string DestinationCurrencyAmount
        {
            get
            {
                return _destinationCurrencyAmount;
            }
            set
            {
                if (_destinationCurrencyAmount != value)
                {
                    _destinationCurrencyAmount = value;
                    base.OnPropertyChanged("DestinationCurrencyAmount", value);
                }
            }
        }

        string _chargesAtDestination;
        /// <summary>
        /// ChargesAtDestination
        /// </summary>
        public string ChargesAtDestination
        {
            get
            {
                return _chargesAtDestination;
            }
            set
            {
                if (_chargesAtDestination != value)
                {
                    _chargesAtDestination = value;
                    base.OnPropertyChanged("ChargesAtDestination", value);
                }
            }
        }

        string _agentIATACode;
        /// <summary>
        /// AgentIATACode 
        /// </summary>
        public string AgentIATACode
        {
            get
            {
                return _agentIATACode;
            }
            set
            {
                if (_agentIATACode != value)
                {
                    _agentIATACode = value;
                    base.OnPropertyChanged("AgentIATACode", value);
                }
            }
        }

        Guid _issueplaceid;
        /// <summary>
        /// 签发地
        /// </summary>
        [GuidRequired(CMessage = "签发地", EMessage = "IsSuePlace")]
        public Guid IssuePlaceID
        {
            get
            {
                return _issueplaceid;
            }
            set
            {
                if (_issueplaceid != value)
                {
                    _issueplaceid = value;
                    base.OnPropertyChanged("IssuePlaceID", value);
                }
            }
        }

        string _issueplacename;
        /// <summary>
        /// 签发地
        /// </summary>
        public string IssuePlaceName
        {
            get
            {
                return _issueplacename;
            }
            set
            {
                if (_issueplacename != value)
                {
                    _issueplacename = value;
                    base.OnPropertyChanged("IssuePlaceName", value);
                }
            }
        }

        Guid _issuebyid;
        /// <summary>
        /// 签发人ID
        /// </summary>
        [GuidRequired(CMessage = "签发人", EMessage = "IsSueBy")]
        public Guid IssueByID
        {
            get
            {
                return _issuebyid;
            }
            set
            {
                if (_issuebyid != value)
                {
                    _issuebyid = value;
                    base.OnPropertyChanged("IssueByID", value);
                }
            }
        }

        string _issuebyname;
        /// <summary>
        /// 签发人
        /// </summary>
        public string IssueByName
        {
            get
            {
                return _issuebyname;
            }
            set
            {
                if (_issuebyname != value)
                {
                    _issuebyname = value;
                    base.OnPropertyChanged("IssueByName", value);
                }
            }
        }

        DateTime? _issuedate;
        /// <summary>
        /// 签发时间
        /// </summary>
        public DateTime? IssueDate
        {
            get
            {
                return _issuedate;
            }
            set
            {
                if (_issuedate != value)
                {
                    _issuedate = value;
                    base.OnPropertyChanged("IssueDate", value);
                }
            }
        }

        RateClass _rateClass;
        /// <summary>
        /// 运价等级
        /// </summary>
        public RateClass RateClass
        {
            get
            {
                return _rateClass;
            }
            set
            {
                if (_rateClass != value)
                {
                    _rateClass = value;
                    base.OnPropertyChanged("RateClass", value);
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

        bool _isRequestAgent;
        /// <summary>
        /// 是否已申请代理
        /// </summary>
        public bool IsRequestAgent
        {
            get
            {
                return _isRequestAgent;
            }
            set
            {
                if (_isRequestAgent != value)
                {
                    _isRequestAgent = value;
                    base.OnPropertyChanged("IsRequestAgent", value);
                }
            }
        }


        string _ContainerQtyDescription;
        /// <summary>
        /// 集装箱件数合计字符串
        /// </summary>
        public string ContainerQtyDescription
        {
            get
            {
                return _ContainerQtyDescription;
            }
            set
            {
                if (_ContainerQtyDescription != value)
                {
                    _ContainerQtyDescription = value;
                    base.OnPropertyChanged("ContainerQtyDescription", value);
                }
            }
        }


    }

    #endregion

    #region AirMBLInfo MBL详细信息

    /// <summary>
    /// AirMBLInfo
    /// </summary>
    [Serializable]
    public partial class AirMBLInfo : AirBLList
    {
        short? _numberoforiginal;
        /// <summary>
        /// 复制份数
        /// </summary>
        public short? NumberOfOriginal
        {
            get
            {
                return _numberoforiginal;
            }
            set
            {
                if (_numberoforiginal != value)
                {
                    _numberoforiginal = value;
                    base.OnPropertyChanged("NumberOfOriginal", value);
                }
            }
        }


        Guid? _checkerid;
        /// <summary>
        /// 对单人ID（关联客户信息）
        /// </summary>
        public Guid? CheckerID
        {
            get
            {
                return _checkerid;
            }
            set
            {
                if (_checkerid != value)
                {
                    _checkerid = value;
                    base.OnPropertyChanged("CheckerID", value);
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

        string _shipperAccountNo;
        /// <summary>
        /// 发货人账号
        /// </summary>
        public string ShipperAccountNo
        {
            get
            {
                return _shipperAccountNo;
            }
            set
            {
                if (_shipperAccountNo != value)
                {
                    _shipperAccountNo = value;
                    base.OnPropertyChanged("ShipperAccountNo", value);
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

        string _consigneeAccountNo;
        /// <summary>
        /// 收货人账号
        /// </summary>
        public string ConsigneeAccountNo
        {
            get
            {
                return _consigneeAccountNo;
            }
            set
            {
                if (_consigneeAccountNo != value)
                {
                    _consigneeAccountNo = value;
                    base.OnPropertyChanged("ConsigneeAccountNo", value);
                }
            }
        }

        CustomerDescription _notifypartydescription;
        /// <summary>
        /// 通知人详细信息
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

        string _agentAccountNo;
        /// <summary>
        /// 代理账号
        /// </summary>
        public string AgentAccountNo
        {
            get
            {
                return _agentAccountNo;
            }
            set
            {
                if (_agentAccountNo != value)
                {
                    _agentAccountNo = value;
                    base.OnPropertyChanged("AgentAccountNo", value);
                }
            }
        }

        //string _AgentText;
        ///// <summary>
        ///// 代理文本(用于打印)
        ///// </summary>
        //public string AgentText
        //{
        //    get
        //    {
        //        return _AgentText;
        //    }
        //    set
        //    {
        //        if (_AgentText != value)
        //        {
        //            _AgentText = value;
        //            base.OnPropertyChanged("AgentText", value);
        //        }
        //    }
        //}      

        Guid _departureid;
        /// <summary>
        /// 起运港ID
        /// </summary>
        [GuidRequired(CMessage = "起运港", EMessage = "Departure")]
        public Guid DepartureID
        {
            get
            {
                return _departureid;
            }
            set
            {
                if (_departureid != value)
                {
                    _departureid = value;
                    base.OnPropertyChanged("DepartureID", value);
                }
            }
        }

        string _departurecode;
        /// <summary>
        /// 起运港代码
        /// </summary>
        public string DepartureCode
        {
            get
            {
                return _departurecode;
            }
            set
            {
                if (_departurecode != value)
                {
                    _departurecode = value;
                    base.OnPropertyChanged("DepartureCode", value);
                }
            }
        }

        Guid? _agentofcarrierID;
        /// <summary>
        /// 承运人ID
        /// </summary>
        [GuidRequired(CMessage = "承运人", EMessage = "AgentOfCarrier")]
        public Guid? AgentOfCarrierID
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
                    base.OnPropertyChanged("AgentOfCarrierID", value);
                }
            }
        }

        Guid _airCompanyID;
        /// <summary>
        /// 航空公司ID
        /// </summary>
        [GuidRequired(CMessage = "航空公司", EMessage = "AirCompany")]
        public Guid AirCompanyID
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

        string _transhipmentPort1;
        /// <summary>
        /// 中转港1
        /// </summary>
        public string TranshipmentPort1
        {
            get
            {
                return _transhipmentPort1;
            }
            set
            {
                if (_transhipmentPort1 != value)
                {
                    _transhipmentPort1 = value;
                    base.OnPropertyChanged("TranshipmentPort1", value);
                }
            }
        }

        string _transhipmentPort1By;
        /// <summary>
        /// 中转港1 By
        /// </summary>
        public string TranshipmentPort1By
        {
            get
            {
                return _transhipmentPort1By;
            }
            set
            {
                if (_transhipmentPort1By != value)
                {
                    _transhipmentPort1By = value;
                    base.OnPropertyChanged("TranshipmentPort1By", value);
                }
            }
        }

        string _transhipmentPort2;
        /// <summary>
        /// 中转港2
        /// </summary>
        public string TranshipmentPort2
        {
            get
            {
                return _transhipmentPort2;
            }
            set
            {
                if (_transhipmentPort2 != value)
                {
                    _transhipmentPort2 = value;
                    base.OnPropertyChanged("TranshipmentPort2", value);
                }
            }
        }

        string _transhipmentPort2By;
        /// <summary>
        /// 中转港2 By
        /// </summary>
        public string TranshipmentPort2By
        {
            get
            {
                return _transhipmentPort2By;
            }
            set
            {
                if (_transhipmentPort2By != value)
                {
                    _transhipmentPort2By = value;
                    base.OnPropertyChanged("TranshipmentPort2By", value);
                }
            }
        }

        string _transhipmentPort3;
        /// <summary>
        /// 中转港3
        /// </summary>
        public string TranshipmentPort3
        {
            get
            {
                return _transhipmentPort3;
            }
            set
            {
                if (_transhipmentPort3 != value)
                {
                    _transhipmentPort3 = value;
                    base.OnPropertyChanged("TranshipmentPort3", value);
                }
            }
        }

        string _transhipmentPort3By;
        /// <summary>
        /// 中转港3 By
        /// </summary>
        public string TranshipmentPort3By
        {
            get
            {
                return _transhipmentPort3By;
            }
            set
            {
                if (_transhipmentPort3By != value)
                {
                    _transhipmentPort3By = value;
                    base.OnPropertyChanged("TranshipmentPort3By", value);
                }
            }
        }

        Guid _detinationid;
        /// <summary>
        /// 目的港ID
        /// </summary>
        [GuidRequired(CMessage = "目的港", EMessage = "Detination")]
        public Guid DetinationID
        {
            get
            {
                return _detinationid;
            }
            set
            {
                if (_detinationid != value)
                {
                    _detinationid = value;
                    base.OnPropertyChanged("DetinationID", value);
                }
            }
        }


        string _detinationcode;
        /// <summary>
        /// 目的港代码
        /// </summary>
        public string DetinationCode
        {
            get
            {
                return _detinationcode;
            }
            set
            {
                if (_detinationcode != value)
                {
                    _detinationcode = value;
                    base.OnPropertyChanged("DetinationCode", value);
                }
            }
        }

        Guid? _placeofdeliveryid;
        /// <summary>
        /// 交货地ID
        /// </summary>
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

        string _placeofdeliverycode;
        /// <summary>
        /// 交货地代码
        /// </summary>
        public string PlaceOfDeliveryCode
        {
            get
            {
                return _placeofdeliverycode;
            }
            set
            {
                if (_placeofdeliverycode != value)
                {
                    _placeofdeliverycode = value;
                    base.OnPropertyChanged("PlaceOfDeliveryCode", value);
                }
            }
        }

        Guid? _paymenttermid;
        /// <summary>
        /// 付费条款ID
        /// </summary>
        [GuidRequired(CMessage = "付款方式", EMessage = "PaymentTerm")]
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

        Guid? _Bookingpaymenttermid;
        /// <summary>
        /// Booking中的MBL付款方式ID
        /// </summary>
        public Guid? BookingPaymentTermID
        {
            get
            {
                return _Bookingpaymenttermid;
            }
            set
            {
                if (_Bookingpaymenttermid != value)
                {
                    _Bookingpaymenttermid = value;
                    base.OnPropertyChanged("BookingPaymentTermID", value);
                }
            }
        }

        Guid? _Otherpaymenttermid;
        /// <summary>
        /// 其它付款方式ID
        /// </summary>
        public Guid? OtherPaymentTermID
        {
            get
            {
                return _Otherpaymenttermid;
            }
            set
            {
                if (_Otherpaymenttermid != value)
                {
                    _Otherpaymenttermid = value;
                    base.OnPropertyChanged("OtherPaymentTermID", value);
                }
            }
        }

        string _paymenttermname;
        /// <summary>
        /// 付费条款
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

        string _otherpaymenttermname;
        /// <summary>
        /// 其它付费条款
        /// </summary>
        public string OtherPaymentTermName
        {
            get
            {
                return _otherpaymenttermname;
            }
            set
            {
                if (_otherpaymenttermname != value)
                {
                    _otherpaymenttermname = value;
                    base.OnPropertyChanged("OtherPaymentTermName", value);
                }
            }
        }

        string _declaredValueForCarriage;
        /// <summary>
        /// 运输声明价值
        /// </summary>
        public string DeclaredValueForCarriage
        {
            get
            {
                return _declaredValueForCarriage;
            }
            set
            {
                if (_declaredValueForCarriage != value)
                {
                    _declaredValueForCarriage = value;
                    base.OnPropertyChanged("DeclaredValueForCarriage", value);
                }
            }
        }

        string _DeclaredValueForCustoms;
        /// <summary>
        /// 海关声明价值
        /// </summary>
        public string DeclaredValueForCustoms
        {
            get
            {
                return _DeclaredValueForCustoms;
            }
            set
            {
                if (_DeclaredValueForCustoms != value)
                {
                    _DeclaredValueForCustoms = value;
                    base.OnPropertyChanged("DeclaredValueForCustoms", value);
                }
            }
        }

        string _insuranceAmount;
        /// <summary>
        /// 保险金额
        /// </summary>
        public string InsuranceAmount
        {
            get
            {
                return _insuranceAmount;
            }
            set
            {
                if (_insuranceAmount != value)
                {
                    _insuranceAmount = value;
                    base.OnPropertyChanged("InsuranceAmount", value);
                }
            }
        }

        bool _chargeableWeightUnitIsKGS;
        public bool ChargeableWeightUnitIsKGS
        {
            get
            {
                return _chargeableWeightUnitIsKGS;
            }
            set
            {
                if (_chargeableWeightUnitIsKGS != value)
                {
                    _chargeableWeightUnitIsKGS = value;
                    base.OnPropertyChanged("ChargeableWeightUnitIsKGS", value);
                }
            }
        }

        bool _iATAChargeableWeightUnitIsKGS;
        public bool IATAChargeableWeightUnitIsKGS
        {
            get
            {
                return _iATAChargeableWeightUnitIsKGS;
            }
            set
            {
                if (_iATAChargeableWeightUnitIsKGS != value)
                {
                    _iATAChargeableWeightUnitIsKGS = value;
                    base.OnPropertyChanged("IATAChargeableWeightUnitIsKGS", value);
                }
            }
        }

        string _freightdescription;
        /// <summary>
        /// 运费描述
        /// </summary>
        [StringLength(MaximumLength = 500, CMessage = "运费描述", EMessage = "FreightDescription")]
        public string FreightDescription
        {
            get
            {
                return _freightdescription;
            }
            set
            {
                if (_freightdescription != value)
                {
                    _freightdescription = value;
                    base.OnPropertyChanged("FreightDescription", value);
                }
            }
        }

        DateTime? _releasedate;
        /// <summary>
        /// 放单日期
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

        //DateTime? _bLDate;
        ///// <summary>
        ///// 提单日期
        ///// </summary>
        //public DateTime? BLDate 
        //{
        //    get
        //    {
        //        return _bLDate;
        //    }
        //    set
        //    {
        //        if (_bLDate != value)
        //        {
        //            _bLDate = value;
        //            base.OnPropertyChanged("BLDate", value);
        //        }
        //    }
        //}

        Guid _quantityunitid;
        /// <summary>
        /// 包装单位ID
        /// </summary>
        [GuidRequired(CMessage = "包装单位", EMessage = "QuantityUnit")]
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
        /// 包装单位
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

        //Guid _weightunitid;
        ///// <summary>
        ///// 重量单位ID
        ///// </summary>
        //[GuidRequired(ErrorMessage = "重量单位必须填写")]
        //public Guid WeightUnitID
        //{
        //    get
        //    {
        //        return _weightunitid;
        //    }
        //    set
        //    {
        //        if (_weightunitid != value)
        //        {
        //            _weightunitid = value;
        //            base.OnPropertyChanged("WeightUnitID", value);
        //        }
        //    }
        //}


        //string _weightunitname;
        ///// <summary>
        ///// 重量单位
        ///// </summary>
        //public string WeightUnitName
        //{
        //    get
        //    {
        //        return _weightunitname;
        //    }
        //    set
        //    {
        //        if (_weightunitname != value)
        //        {
        //            _weightunitname = value;
        //            base.OnPropertyChanged("WeightUnitName", value);
        //        }
        //    }
        //}

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
        [GuidRequired(CMessage = "体积单位", EMessage = "MeasurementUnit")]
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
        /// 体积单位
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

        Guid _currencyID;
        /// <summary>
        /// 币种
        /// </summary>
        [GuidRequired(CMessage = "币种", EMessage = "Currency")]
        public Guid CurrencyID
        {
            get
            {
                return _currencyID;
            }
            set
            {
                if (_currencyID != value)
                {
                    _currencyID = value;
                    base.OnPropertyChanged("CurrencyID", value);
                }
            }
        }

        Guid? _iATACurrencyID;
        /// <summary>
        /// IATA币种
        /// </summary>
        public Guid? IATACurrencyID
        {
            get
            {
                return _iATACurrencyID;
            }
            set
            {
                if (_iATACurrencyID != value)
                {
                    _iATACurrencyID = value;
                    base.OnPropertyChanged("IATACurrencyID", value);
                }
            }
        }

        decimal _rateCharge;
        /// <summary>
        /// RateCharge
        /// </summary>
        public decimal RateCharge
        {
            get
            {
                return _rateCharge;
            }
            set
            {
                if (_rateCharge != value)
                {
                    _rateCharge = value;
                    base.OnPropertyChanged("RateCharge", value);
                }
            }
        }

        decimal _iATARateCharge;
        /// <summary>
        /// IATARateCharge
        /// </summary>
        public decimal IATARateCharge
        {
            get
            {
                return _iATARateCharge;
            }
            set
            {
                if (_iATARateCharge != value)
                {
                    _iATARateCharge = value;
                    base.OnPropertyChanged("IATARateCharge", value);
                }
            }
        }

        decimal _rateAmount;
        /// <summary>
        /// RateAmount
        /// </summary>
        public decimal RateAmount
        {
            get
            {
                return _rateAmount;
            }
            set
            {
                if (_rateAmount != value)
                {
                    _rateAmount = value;
                    base.OnPropertyChanged("RateAmount", value);
                }
            }
        }

        decimal _iATARateAmount;
        /// <summary>
        /// IATARateAmount
        /// </summary>
        public decimal IATARateAmount
        {
            get
            {
                return _iATARateAmount;
            }
            set
            {
                if (_iATARateAmount != value)
                {
                    _iATARateAmount = value;
                    base.OnPropertyChanged("IATARateAmount", value);
                }
            }
        }

        //string _chargeableWeightUnitIName;
        ///// <summary>
        ///// ChargeableWeightUnitIName
        ///// </summary>
        //public string ChargeableWeightUnitIName
        //{
        //    get
        //    {
        //        return _chargeableWeightUnitIName;
        //    }
        //    set
        //    {
        //        if (_chargeableWeightUnitIName != value)
        //        {
        //            _chargeableWeightUnitIName = value;
        //            base.OnPropertyChanged("ChargeableWeightUnitIName", value);
        //        }
        //    }
        //}

        //string _iATAWeightUnitName;
        ///// <summary>
        ///// IATAWeightUnitName
        ///// </summary>
        //public string IATAWeightUnitName
        //{
        //    get
        //    {
        //        return _iATAWeightUnitName;
        //    }
        //    set
        //    {
        //        if (_iATAWeightUnitName != value)
        //        {
        //            _iATAWeightUnitName = value;
        //            base.OnPropertyChanged("IATAWeightUnitName", value);
        //        }
        //    }
        //}

        string _handingInformation;
        /// <summary>
        /// 操作信息
        /// </summary>
        public string HandingInformation
        {
            get
            {
                return _handingInformation;
            }
            set
            {
                if (_handingInformation != value)
                {
                    _handingInformation = value;
                    base.OnPropertyChanged("HandingInformation", value);
                }
            }
        }

        //string _accountInformation;
        ///// <summary>
        ///// 支付信息
        ///// </summary>
        //public string AccountInformation 
        //{
        //    get
        //    {
        //        return _accountInformation;
        //    }
        //    set
        //    {
        //        if (_accountInformation != value)
        //        {
        //            _accountInformation = value;
        //            base.OnPropertyChanged("AccountInformation", value);
        //        }
        //    }
        //}

        public List<OtherChargeItem> OtherChargeList { get; set; }

        string _otherChargeDescription;
        /// <summary>
        /// 其它费用备注
        /// </summary>
        public string OtherChargeDescription
        {
            get
            {
                return _otherChargeDescription;
            }
            set
            {
                if (_otherChargeDescription != value)
                {
                    _otherChargeDescription = value;
                    base.OnPropertyChanged("OtherChargeDescription", value);
                }
            }
        }

        string _marks;
        /// <summary>
        /// 唛头
        /// </summary>
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

        string _goodsdescription;
        /// <summary>
        /// 货物描述
        /// </summary>
        public string GoodsDescription
        {
            get
            {
                return _goodsdescription;
            }
            set
            {
                if (_goodsdescription != value)
                {
                    _goodsdescription = value;
                    base.OnPropertyChanged("GoodsDescription", value);
                }
            }
        }

        decimal _Tax;
        /// <summary>
        /// 税款
        /// </summary>
        public decimal Tax
        {
            get
            {
                return _Tax;
            }
            set
            {
                if (_Tax != value)
                {
                    _Tax = value;
                    base.OnPropertyChanged("Tax", value);
                }
            }
        }

        decimal _valuationCharge;
        /// <summary>
        /// ValuationCharge
        /// </summary>
        public decimal ValuationCharge
        {
            get
            {
                return _valuationCharge;
            }
            set
            {
                if (_valuationCharge != value)
                {
                    _valuationCharge = value;
                    base.OnPropertyChanged("ValuationCharge", value);
                }
            }
        }

        decimal _agentCharger;
        /// <summary>
        /// AgentCharger
        /// </summary>
        public decimal AgentCharger
        {
            get
            {
                return _agentCharger;
            }
            set
            {
                if (_agentCharger != value)
                {
                    _agentCharger = value;
                    base.OnPropertyChanged("AgentCharger", value);
                }
            }
        }

        decimal _carrierCharger;
        /// <summary>
        /// CarrierCharger
        /// </summary>
        public decimal CarrierCharger
        {
            get
            {
                return _carrierCharger;
            }
            set
            {
                if (_carrierCharger != value)
                {
                    _carrierCharger = value;
                    base.OnPropertyChanged("CarrierCharger", value);
                }
            }
        }

        string _currencyConversionRate;
        /// <summary>
        /// CurrencyConversionRate
        /// </summary>
        public string CurrencyConversionRate
        {
            get
            {
                return _currencyConversionRate;
            }
            set
            {
                if (_currencyConversionRate != value)
                {
                    _currencyConversionRate = value;
                    base.OnPropertyChanged("CurrencyConversionRate", value);
                }
            }
        }

        string _destinationCurrencyAmount;
        /// <summary>
        /// DestinationCurrencyAmount
        /// </summary>
        public string DestinationCurrencyAmount
        {
            get
            {
                return _destinationCurrencyAmount;
            }
            set
            {
                if (_destinationCurrencyAmount != value)
                {
                    _destinationCurrencyAmount = value;
                    base.OnPropertyChanged("DestinationCurrencyAmount", value);
                }
            }
        }

        string _chargesAtDestination;
        /// <summary>
        /// ChargesAtDestination
        /// </summary>
        public string ChargesAtDestination
        {
            get
            {
                return _chargesAtDestination;
            }
            set
            {
                if (_chargesAtDestination != value)
                {
                    _chargesAtDestination = value;
                    base.OnPropertyChanged("ChargesAtDestination", value);
                }
            }
        }

        string _agentIATACode;
        /// <summary>
        /// AgentIATACode 
        /// </summary>
        public string AgentIATACode
        {
            get
            {
                return _agentIATACode;
            }
            set
            {
                if (_agentIATACode != value)
                {
                    _agentIATACode = value;
                    base.OnPropertyChanged("AgentIATACode", value);
                }
            }
        }

        Guid _issueplaceid;
        /// <summary>
        /// 签发地
        /// </summary>
        [GuidRequired(CMessage = "签发地", EMessage = "IsSuePlace")]
        public Guid IssuePlaceID
        {
            get
            {
                return _issueplaceid;
            }
            set
            {
                if (_issueplaceid != value)
                {
                    _issueplaceid = value;
                    base.OnPropertyChanged("IssuePlaceID", value);
                }
            }
        }

        string _issueplacename;
        /// <summary>
        /// 签发地
        /// </summary>
        public string IssuePlaceName
        {
            get
            {
                return _issueplacename;
            }
            set
            {
                if (_issueplacename != value)
                {
                    _issueplacename = value;
                    base.OnPropertyChanged("IssuePlaceName", value);
                }
            }
        }

        Guid _issuebyid;
        /// <summary>
        /// 签发人ID
        /// </summary>
        [GuidRequired(CMessage = "签发人", EMessage = "IsSueBy")]
        public Guid IssueByID
        {
            get
            {
                return _issuebyid;
            }
            set
            {
                if (_issuebyid != value)
                {
                    _issuebyid = value;
                    base.OnPropertyChanged("IssueByID", value);
                }
            }
        }

        string _issuebyname;
        /// <summary>
        /// 签发人
        /// </summary>
        public string IssueByName
        {
            get
            {
                return _issuebyname;
            }
            set
            {
                if (_issuebyname != value)
                {
                    _issuebyname = value;
                    base.OnPropertyChanged("IssueByName", value);
                }
            }
        }

        DateTime? _issuedate;
        /// <summary>
        /// 签发时间
        /// </summary>
        public DateTime? IssueDate
        {
            get
            {
                return _issuedate;
            }
            set
            {
                if (_issuedate != value)
                {
                    _issuedate = value;
                    base.OnPropertyChanged("IssueDate", value);
                }
            }
        }

        RateClass _rateClass;
        /// <summary>
        /// 运价等级
        /// </summary>
        public RateClass RateClass
        {
            get
            {
                return _rateClass;
            }
            set
            {
                if (_rateClass != value)
                {
                    _rateClass = value;
                    base.OnPropertyChanged("RateClass", value);
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

        string _HAWBNos;
        /// <summary>
        /// HBL NOs
        /// </summary>
        public string HAWBNos
        {
            get
            {
                return _HAWBNos;
            }
            set
            {
                if (_HAWBNos != value)
                {
                    _HAWBNos = value;
                    base.OnPropertyChanged("HAWBNos", value);
                }
            }
        }

        bool _isRequestAgent;
        /// <summary>
        /// 是否已申请代理
        /// </summary>
        public bool IsRequestAgent
        {
            get
            {
                return _isRequestAgent;
            }
            set
            {
                if (_isRequestAgent != value)
                {
                    _isRequestAgent = value;
                    base.OnPropertyChanged("IsRequestAgent", value);
                }
            }
        }


        string _ContainerQtyDescription;
        /// <summary>
        /// 集装箱件数合计字符串
        /// </summary>
        public string ContainerQtyDescription
        {
            get
            {
                return _ContainerQtyDescription;
            }
            set
            {
                if (_ContainerQtyDescription != value)
                {
                    _ContainerQtyDescription = value;
                    base.OnPropertyChanged("ContainerQtyDescription", value);
                }
            }
        }


    }

    #endregion

    #endregion

    #region AirBLContainerList

    /// <summary>
    /// AirBLContainerList
    /// </summary>
    [Serializable]
    public partial class AirBLContainerList : AirContainerList
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        #region

        string _marks;
        /// <summary>
        /// 唛头
        /// </summary>
        [StringLength(MaximumLength = 400, CMessage = "唛头", EMessage = "Marks")]
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

        int _quantity;
        /// <summary>
        /// 件数
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

        #region IDs

        Guid? _BLID;
        /// <summary>
        /// BLID
        /// </summary>
        public Guid? BLID
        {
            get
            {
                return _BLID;
            }
            set
            {
                if (_BLID != value)
                {
                    _BLID = value;
                    base.OnPropertyChanged("BLID", value);
                }
            }
        }

        Guid? _CargoID;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid? CargoID
        {
            get
            {
                return _CargoID;
            }
            set
            {
                if (_CargoID != value)
                {
                    _CargoID = value;
                    base.OnPropertyChanged("CargoID", value);
                }
            }
        }

        #endregion

        DateTime? _cargoUpdateDate;
        /// <summary>
        /// 更新时间-做数据版本控制用
        /// </summary>
        public DateTime? CargoUpdateDate
        {
            get
            {
                return _cargoUpdateDate;
            }
            set
            {
                if (_cargoUpdateDate != value)
                {
                    _cargoUpdateDate = value;
                    base.OnPropertyChanged("CargoUpdateDate", value);
                }
            }
        }

        Guid? _CargoCreateBy;
        /// <summary>
        /// 货物创建人
        /// </summary>
        public Guid? CargoCreateBy
        {
            get
            {
                return _CargoCreateBy;
            }
            set
            {
                if (_CargoCreateBy != value)
                {
                    _CargoCreateBy = value;
                    base.OnPropertyChanged("CargoCreateBy", value);
                }
            }
        }

        DateTime? _CargoCreatedate;
        /// <summary>
        /// 货物建立时间
        /// </summary>
        public DateTime? CargoCreateDate
        {
            get
            {
                return _CargoCreatedate;
            }
            set
            {
                if (_CargoCreatedate != value)
                {
                    _CargoCreatedate = value;
                    base.OnPropertyChanged("CargoCreateDate", value);
                }
            }
        }

        bool _relation;
        /// <summary>
        /// 已关联
        /// </summary>
        public bool Relation
        {
            get
            {
                return _relation;
            }
            set
            {
                if (_relation != value)
                {
                    _relation = value;
                    base.OnPropertyChanged("Relation", value);
                }
            }
        }
    }

    #endregion

    #region 通过事务保存所需要的参数对象

    /// <summary>
    /// 保存MBL的参数
    /// </summary>
    [Serializable]
    public class SaveMBLInfoParameter
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? id { get; set; }
        /// <summary>
        /// 订舱ID
        /// </summary>
        public Guid airBookingID { get; set; }
        /// <summary>
        /// MBL号
        /// </summary>
        public string mblNo { get; set; }
        /// <summary>
        /// 提单份数
        /// </summary>
        public short? numberOfOriginal { get; set; }

        /// <summary>
        /// 对单人
        /// </summary>
        public Guid? checkerID { get; set; }
        /// <summary>
        /// 发货人
        /// </summary>
        public Guid? shipperID { get; set; }
        /// <summary>
        /// 发货人描述
        /// </summary>
        public CustomerDescription shipperDescription { get; set; }

        /// <summary>
        /// 发货人账号
        /// </summary>
        public string ShipperAccountNo { get; set; }

        /// <summary>
        /// 收货人账号
        /// </summary>
        public string ConsigneeAccountNo { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public Guid? consigneeID { get; set; }
        /// <summary>
        /// 收货人描述
        /// </summary>
        public CustomerDescription consigneeDescription { get; set; }
        /// <summary>
        /// 通知人
        /// </summary>
        public Guid? notifyPartyID { get; set; }
        /// <summary>
        /// 通知人描述
        /// </summary>
        public CustomerDescription notifyPartyDescription { get; set; }
        /// <summary>
        /// 代理
        /// </summary>
        public Guid? agentID { get; set; }
        /// <summary>
        /// 代理描述
        /// </summary>
        public CustomerDescription agentDescription { get; set; }

        /// <summary>
        /// 承运人ID
        /// </summary>
        public Guid? AgentOfCarrierID { get; set; }

        public string AgentAccountNo { get; set; }

        /// <summary>
        /// 航班号ID
        /// </summary>
        public Guid? FilightNoID { get; set; }
        /// <summary>
        /// 航空公司ID
        /// </summary>
        public Guid? AirCompanyID { get; set; }
        /// <summary>
        /// 收货地
        /// </summary>
        public Guid? placeOfReceiptID { get; set; }
        /// <summary>
        /// 收货地名
        /// </summary>
        public string placeOfReceiptName { get; set; }
        /// <summary>
        /// 头程船名航次
        /// </summary>
        public Guid? preVoyageID { get; set; }
        /// <summary>
        /// 船名航次
        /// </summary>
        public Guid? voyageID { get; set; }
        /// <summary>
        /// 装货港
        /// </summary>
        public Guid DepartureID { get; set; }
        /// <summary>
        /// 装货港名
        /// </summary>
        public string DepartureName { get; set; }

        public DateTime? ETD { get; set; }

        public DateTime? ETA { get; set; }

        /// <summary>
        /// 卸货港
        /// </summary>
        public Guid DetinationID { get; set; }
        /// <summary>
        /// 卸货港名
        /// </summary>
        public string DetinationName { get; set; }
        /// <summary>
        /// 交货地
        /// </summary>
        public Guid? placeOfDeliveryID { get; set; }
        ///// <summary>
        ///// 交货地名
        ///// </summary>
        //public string placeOfDeliveryName { get; set; }
        /// <summary>
        /// 最终目的地
        /// </summary>
        public Guid? finalDestinationID { get; set; }
        /// <summary>
        /// 最终目的地名
        /// </summary>
        public string finalDestinationName { get; set; }
        /// <summary>
        /// 运输条款
        /// </summary>
        public Guid transportClauseID { get; set; }

        /// <summary>
        /// 中转港1
        /// </summary>
        public string TranshipmentPort1 { get; set; }
        /// <summary>
        /// 中转港1 By
        /// </summary>
        public string TranshipmentPort1By { get; set; }
        /// <summary>
        /// 中转港2
        /// </summary>
        public string TranshipmentPort2 { get; set; }
        /// <summary>
        /// 中转港2 By
        /// </summary>
        public string TranshipmentPort2By { get; set; }
        /// <summary>
        /// 中转港3
        /// </summary>
        public string TranshipmentPort3 { get; set; }
        /// <summary>
        /// 中转港3 By
        /// </summary>
        public string TranshipmentPort3By { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public Guid? paymentTermID { get; set; }

        /// <summary>
        ///  Other付款方式
        /// </summary>
        public Guid? OtherPaymentTermID { get; set; }

        /// <summary>
        /// 运输声明价值
        /// </summary>
        public string DeclaredValueForCarriage { get; set; }
        /// <summary>
        /// 海关声明价值
        /// </summary>
        public string DeclaredValueForCustoms { get; set; }
        /// <summary>
        /// 保险金额
        /// </summary>
        public string InsuranceAmount { get; set; }

        /// <summary>
        /// 运价等级
        /// </summary>
        public RateClass RateClass { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public Guid? CurrencyID { get; set; }
        /// <summary>
        /// IATA币种
        /// </summary>
        public Guid? IATACurrencyID { get; set; }
        /// <summary>
        /// RateCharge
        /// </summary>
        public decimal RateCharge { get; set; }
        /// <summary>
        /// IATARateCharge
        /// </summary>
        public decimal IATARateCharge { get; set; }
        /// <summary>
        /// RateAmount
        /// </summary>
        public decimal RateAmount { get; set; }
        /// <summary>
        /// IATARateAmount
        /// </summary>
        public decimal IATARateAmount { get; set; }

        /// <summary>
        /// 操作信息
        /// </summary>
        public string HandingInformation { get; set; }

        public string OtherChargeList { get; set; }

        /// <summary>
        /// 其它费用备注
        /// </summary>
        public string OtherChargeDescription { get; set; }

        /// <summary>
        /// 税款
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// ValuationCharge
        /// </summary>
        public decimal ValuationCharge { get; set; }

        /// <summary>
        /// AgentCharger
        /// </summary>
        public decimal AgentCharger { get; set; }

        /// <summary>
        /// CarrierCharger
        /// </summary>
        public decimal CarrierCharger { get; set; }

        /// <summary>
        /// CurrencyConversionRate
        /// </summary>
        public string CurrencyConversionRate { get; set; }

        /// <summary>
        /// DestinationCurrencyAmount
        /// </summary>
        public string DestinationCurrencyAmount { get; set; }

        /// <summary>
        /// ChargesAtDestination
        /// </summary>
        public string ChargesAtDestination { get; set; }

        public bool ChargeableWeightUnitIsKGS { get; set; }
        public bool IATAChargeableWeightUnitIsKGS { get; set; }

        /// <summary>
        /// AgentIATACode 
        /// </summary>
        public string AgentIATACode { get; set; }

        /// <summary>
        /// 提单日期
        /// </summary>
        public DateTime? BLDate { get; set; }

        /// <summary>
        /// 费用描述
        /// </summary>
        public string freightDescription { get; set; }
        /// <summary>
        /// 电放方式
        /// </summary>
        public FCMReleaseType releaseType { get; set; }
        /// <summary>
        /// 电放日期
        /// </summary>
        public DateTime? releaseDate { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 数量单位
        /// </summary>
        public Guid quantityUnitID { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public decimal weight { get; set; }

        /// <summary>
        /// 计费重量
        /// </summary>
        public decimal ChargeableWeight { get; set; }

        /// <summary>
        /// 重量单位
        /// </summary>
        public Guid weightUnitID { get; set; }
        /// <summary>
        /// 体积
        /// </summary>
        public decimal measurement { get; set; }
        /// <summary>
        /// 体积单位
        /// </summary>
        public Guid measurementUnitID { get; set; }
        /// <summary>
        /// 货物标示
        /// </summary>
        public string marks { get; set; }
        /// <summary>
        /// 货物描述
        /// </summary>
        public string goodsDescription { get; set; }

        /// <summary>
        /// 箱数或件数合计
        /// </summary>
        public string ctnQtyInfo { get; set; }
        /// <summary>
        /// 签发地
        /// </summary>
        public Guid issuePlaceID { get; set; }
        /// <summary>
        /// 签发人
        /// </summary>
        public Guid issueByID { get; set; }
        /// <summary>
        /// 签发日期
        /// </summary>
        public DateTime? issueDate { get; set; }


        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? updateDate { get; set; }
    }

    [Serializable]
    public class OtherChargeItem : BaseDataObject
    {
        string _chargeName;
        /// <summary>
        /// 费用名称
        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "费用名称", EMessage = "ChargeName")]
        public string ChargeName
        {
            get
            {
                return _chargeName;
            }
            set
            {
                if (_chargeName != value)
                {
                    _chargeName = value;
                    base.OnPropertyChanged("ChargeName", value);
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
    }

    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    [System.Xml.Serialization.XmlRootAttribute(IsNullable = false, ElementName = "BLOtherCharges")]
    public class OtherChargeXML
    {
        public OtherChargeXML()
        {
            this.Items = new List<OtherChargeItemXML>();
        }

        [System.Xml.Serialization.XmlElement("OtherChargeItem")]
        public List<OtherChargeItemXML> Items { get; set; }
    }

    [Serializable]
    public class OtherChargeItemXML : BaseDataObject
    {
        string _chargeName;
        /// <summary>
        /// 费用名称
        /// </summary>
        // [XmlElement("ChargeName")]
        public string ChargeName
        {
            get
            {
                return _chargeName;
            }
            set
            {
                if (_chargeName != value)
                {
                    _chargeName = value;
                    base.OnPropertyChanged("ChargeName", value);
                }
            }
        }

        string _amount;
        /// <summary>
        /// 金额
        /// </summary>
        // [XmlElement("Amount")]
        public string Amount
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
    }

    /// <summary>
    /// 保存HBL的参数
    /// </summary>
    [Serializable]
    public class SaveHBLInfoParameter
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? id { get; set; }
        /// <summary>
        /// 订舱ID
        /// </summary>
        public Guid airBookingID { get; set; }
        /// <summary>
        /// MBL号
        /// </summary>
        public string mblNo { get; set; }

        public Guid? mblId { get; set; }

        public string hblNo { get; set; }

        /// <summary>
        /// 提单份数
        /// </summary>
        public short? numberOfOriginal { get; set; }

        /// <summary>
        /// 对单人
        /// </summary>
        public Guid? checkerID { get; set; }
        /// <summary>
        /// 发货人
        /// </summary>
        public Guid? shipperID { get; set; }
        /// <summary>
        /// 发货人描述
        /// </summary>
        public CustomerDescription shipperDescription { get; set; }

        /// <summary>
        /// 发货人账号
        /// </summary>
        public string ShipperAccountNo { get; set; }

        /// <summary>
        /// 收货人账号
        /// </summary>
        public string ConsigneeAccountNo { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public Guid? consigneeID { get; set; }
        /// <summary>
        /// 收货人描述
        /// </summary>
        public CustomerDescription consigneeDescription { get; set; }
        /// <summary>
        /// 通知人
        /// </summary>
        public Guid? notifyPartyID { get; set; }
        /// <summary>
        /// 通知人描述
        /// </summary>
        public CustomerDescription notifyPartyDescription { get; set; }
        /// <summary>
        /// 代理
        /// </summary>
        public Guid? agentID { get; set; }
        /// <summary>
        /// 代理描述
        /// </summary>
        public CustomerDescription agentDescription { get; set; }

        /// <summary>
        /// 承运人ID
        /// </summary>
        public Guid? AgentOfCarrierID { get; set; }

        public string AgentAccountNo { get; set; }

        /// <summary>
        /// 航班号ID
        /// </summary>
        public Guid? FilightNoID { get; set; }
        /// <summary>
        /// 航空公司ID
        /// </summary>
        public Guid? AirCompanyID { get; set; }
        /// <summary>
        /// 收货地
        /// </summary>
        public Guid? placeOfReceiptID { get; set; }
        /// <summary>
        /// 收货地名
        /// </summary>
        public string placeOfReceiptName { get; set; }
        /// <summary>
        /// 头程船名航次
        /// </summary>
        public Guid? preVoyageID { get; set; }
        /// <summary>
        /// 船名航次
        /// </summary>
        public Guid? voyageID { get; set; }
        /// <summary>
        /// 装货港
        /// </summary>
        public Guid DepartureID { get; set; }
        /// <summary>
        /// 装货港名
        /// </summary>
        public string DepartureName { get; set; }

        public DateTime? ETD { get; set; }

        public DateTime? ETA { get; set; }

        /// <summary>
        /// 卸货港
        /// </summary>
        public Guid DetinationID { get; set; }
        /// <summary>
        /// 卸货港名
        /// </summary>
        public string DetinationName { get; set; }
        /// <summary>
        /// 交货地
        /// </summary>
        public Guid? placeOfDeliveryID { get; set; }
        ///// <summary>
        ///// 交货地名
        ///// </summary>
        //public string placeOfDeliveryName { get; set; }
        /// <summary>
        /// 最终目的地
        /// </summary>
        public Guid? finalDestinationID { get; set; }
        /// <summary>
        /// 最终目的地名
        /// </summary>
        public string finalDestinationName { get; set; }
        /// <summary>
        /// 运输条款
        /// </summary>
        public Guid transportClauseID { get; set; }

        /// <summary>
        /// 中转港1
        /// </summary>
        public string TranshipmentPort1 { get; set; }
        /// <summary>
        /// 中转港1 By
        /// </summary>
        public string TranshipmentPort1By { get; set; }
        /// <summary>
        /// 中转港2
        /// </summary>
        public string TranshipmentPort2 { get; set; }
        /// <summary>
        /// 中转港2 By
        /// </summary>
        public string TranshipmentPort2By { get; set; }
        /// <summary>
        /// 中转港3
        /// </summary>
        public string TranshipmentPort3 { get; set; }
        /// <summary>
        /// 中转港3 By
        /// </summary>
        public string TranshipmentPort3By { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public Guid? paymentTermID { get; set; }

        /// <summary>
        ///  Other付款方式
        /// </summary>
        public Guid? OtherPaymentTermID { get; set; }

        /// <summary>
        /// 运输声明价值
        /// </summary>
        public string DeclaredValueForCarriage { get; set; }
        /// <summary>
        /// 海关声明价值
        /// </summary>
        public string DeclaredValueForCustoms { get; set; }
        /// <summary>
        /// 保险金额
        /// </summary>
        public string InsuranceAmount { get; set; }

        /// <summary>
        /// 运价等级
        /// </summary>
        public RateClass RateClass { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public Guid? CurrencyID { get; set; }

        /// <summary>
        /// RateCharge
        /// </summary>
        public decimal RateCharge { get; set; }

        /// <summary>
        /// RateAmount
        /// </summary>
        public decimal RateAmount { get; set; }

        /// <summary>
        /// 操作信息
        /// </summary>
        public string HandingInformation { get; set; }

        public string OtherChargeList { get; set; }

        /// <summary>
        /// 其它费用备注
        /// </summary>
        public string OtherChargeDescription { get; set; }

        /// <summary>
        /// 税款
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// ValuationCharge
        /// </summary>
        public decimal ValuationCharge { get; set; }

        /// <summary>
        /// AgentCharger
        /// </summary>
        public decimal AgentCharger { get; set; }

        /// <summary>
        /// CarrierCharger
        /// </summary>
        public decimal CarrierCharger { get; set; }

        /// <summary>
        /// CurrencyConversionRate
        /// </summary>
        public string CurrencyConversionRate { get; set; }

        /// <summary>
        /// DestinationCurrencyAmount
        /// </summary>
        public string DestinationCurrencyAmount { get; set; }

        /// <summary>
        /// ChargesAtDestination
        /// </summary>
        public string ChargesAtDestination { get; set; }

        public bool ChargeableWeightUnitIsKGS { get; set; }

        /// <summary>
        /// AgentIATACode 
        /// </summary>
        public string AgentIATACode { get; set; }

        /// <summary>
        /// 提单日期
        /// </summary>
        public DateTime? BLDate { get; set; }

        /// <summary>
        /// 费用描述
        /// </summary>
        public string freightDescription { get; set; }
        /// <summary>
        /// 电放方式
        /// </summary>
        public FCMReleaseType releaseType { get; set; }
        /// <summary>
        /// 电放日期
        /// </summary>
        public DateTime? releaseDate { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 数量单位
        /// </summary>
        public Guid quantityUnitID { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public decimal weight { get; set; }

        /// <summary>
        /// 计费重量
        /// </summary>
        public decimal ChargeableWeight { get; set; }

        /// <summary>
        /// 重量单位
        /// </summary>
        public Guid weightUnitID { get; set; }
        /// <summary>
        /// 体积
        /// </summary>
        public decimal measurement { get; set; }
        /// <summary>
        /// 体积单位
        /// </summary>
        public Guid measurementUnitID { get; set; }
        /// <summary>
        /// 货物标示
        /// </summary>
        public string marks { get; set; }
        /// <summary>
        /// 货物描述
        /// </summary>
        public string goodsDescription { get; set; }

        /// <summary>
        /// 箱数或件数合计
        /// </summary>
        public string ctnQtyInfo { get; set; }
        /// <summary>
        /// 签发地
        /// </summary>
        public Guid issuePlaceID { get; set; }
        /// <summary>
        /// 签发人
        /// </summary>
        public Guid issueByID { get; set; }
        /// <summary>
        /// 签发日期
        /// </summary>
        public DateTime? issueDate { get; set; }


        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? updateDate { get; set; }

        /// <summary>
        /// MBL更新时间
        /// </summary>
        public DateTime? mblUpdateDate { get; set; }
    }

    #endregion
}
