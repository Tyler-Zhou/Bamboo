namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Client;
    using System.Runtime.Serialization;

    #region BL

    #region OceanBLList 提单列表数据对象

    /// <summary>
    ///  OceanBLList 提单列表数据对象
    /// </summary>
    [Serializable]
    public partial class OceanBLList : BaseDataObject
    {

        ///// <summary>
        ///// 业务联系人
        ///// </summary>
        //Guid _senderID;
        //public Guid SenderID
        //{
        //    get
        //    {
        //        return _senderID;
        //    }
        //    set
        //    {
        //        if (_senderID != value)
        //        {
        //            _senderID = value;
        //            base.OnPropertyChanged("SenderID", value);
        //        }
        //    }
        //}


        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        #region 提单类型
        /// <summary>
        /// 提单类型
        /// </summary>
        public FCMBLType BLType
        {
            get
            {
                if (this.ID == this.MBLID)
                {
                    return FCMBLType.MBL;
                }
                else
                {
                    return FCMBLType.HBL;
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

        #endregion

        #region ID
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

        #region 订舱ID
        Guid _oceanbookingid;
        /// <summary>
        /// 业务ID
        /// </summary>
        [GuidRequired(CMessage = "业务", EMessage = "OceanBooking")]
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

        #endregion

        #region  航线
        /// <summary>
        /// 航线
        /// </summary>
        public Guid? ShippingLineID
        {
            get;
            set;
        }
        #endregion

        #region  MBLID
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

        #endregion

        #region  操作口岸ID
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

        #endregion

        #region 业务号
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


        #endregion

        #region 提单号
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

        #endregion

        #region 对单人
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


        #endregion

        #region 代理名称
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

        #endregion

        #region 船东
        public Guid? CarrierID
        {
            get;
            set;
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

        #region 客户名称
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

        #endregion

        #region 箱号列表
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

        #region 头程船名航次
        string _prevesselvoyage;
        /// <summary>
        /// 头程船名航次
        /// </summary>
        public string PreVesselVoyage
        {
            get
            {
                return _prevesselvoyage;
            }
            set
            {
                if (_prevesselvoyage != value)
                {
                    _prevesselvoyage = value;
                    base.OnPropertyChanged("PreVesselVoyage", value);
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

        #region POL
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

        #region ETD
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

        #region PreETD
        DateTime? _preETD;
        /// <summary>
        /// 驳船ETD
        /// </summary>
        public DateTime? PreETD
        {
            get
            {
                return _preETD;
            }
            set
            {
                if (_preETD != value)
                {
                    _preETD = value;
                    base.OnPropertyChanged("PreETD", value);
                }
            }
        }

        #endregion

        #region POD
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

        #region ETA
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

        #region 最终目的地
        string _finaldestinationname;
        /// <summary>
        /// 最终目的地
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

        #region 状态
        OEBLState _state;
        /// <summary>
        /// 提单状态
        /// </summary>
        public OEBLState State
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
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<OEBLState>(_state, LocalData.IsEnglish);
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
        /// 电放类型(只读,根据电放类型和中英文环境返回字串)
        /// </summary>
        public string ReleaseTypeName
        {
            get
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<FCMReleaseType>(_releasetype, LocalData.IsEnglish);
            }
        }

        #endregion


        #region 创建人
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

        #endregion

        #region 创建时间
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

        #region 更新时间--数据版本控制
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
        //BLType _bltype;
        ///// <summary>
        ///// 提单状态
        //// </summary>
        //public BLType BLType
        //{
        //    get
        //    {
        //        return _bltype;
        //    }
        //    set
        //    {
        //        if (_bltype != value)
        //        {
        //            _bltype = value;
        //            base.OnPropertyChanged("BLType", value);
        //        }
        //    }
        //}

        #region 是否有费用
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

        #region 订舱号
        string _SONO;
        /// <summary>
        /// 订舱号
        /// </summary>
        public string SONO
        {
            get
            {
                return _SONO;
            }
            set
            {
                if (_SONO != value)
                {
                    _SONO = value;
                    base.OnPropertyChanged("SONO", value);
                }
            }
        }

        #endregion

        #region 订舱人
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

        #region 文件
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

        #endregion

        #region 海外部客服
        string _OverseasFilerName;
        /// <summary>
        /// 海外部客服
        /// </summary>
        public string OverseasFilerName
        {
            get
            {
                return _OverseasFilerName;
            }
            set
            {
                if (_OverseasFilerName != value)
                {
                    _OverseasFilerName = value;
                    base.OnPropertyChanged("OverseasFilerName", value);
                }
            }
        }

        #endregion

        #region 签单类型
        IssueType _IssueType;
        /// <summary>
        /// 签单类型
        /// </summary>
        public IssueType IssueType
        {
            get
            {
                return _IssueType;
            }
            set
            {
                if (_IssueType != value)
                {
                    _IssueType = value;
                    base.OnPropertyChanged("IssueType", value);
                }
            }
        }

        #endregion

        #region 签单类型
        /// <summary>
        /// 签单类型(只读,根据签单类型和中英文环境返回字串)
        /// </summary>
        public string IssueTypeName
        {
            get
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<IssueType>(_IssueType, LocalData.IsEnglish);
            }
        }

        #endregion

        #region 发货人ID
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

        #endregion

        #region 收货人ID
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

        #endregion

        #region 通知人ID
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

        #endregion

        #region HBL数据
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

        #endregion


        #region 是否有效
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

        #endregion

        #region 装货单ID
        Guid? _ShippingOrderID;
        /// <summary>
        /// 装货单ID
        /// </summary>
        public Guid? ShippingOrderID
        {
            get
            {
                return _ShippingOrderID;
            }
            set
            {
                if (_ShippingOrderID != value)
                {
                    _ShippingOrderID = value;
                    base.OnPropertyChanged("ShippingOrderID", value);
                }
            }
        }

        #endregion

        #region 装货单更新日期
        DateTime? _ShippingOrderUpdateDate;
        /// <summary>
        /// 装货单更新日期
        /// </summary>
        public DateTime? ShippingOrderUpdateDate
        {
            get
            {
                return _ShippingOrderUpdateDate;
            }
            set
            {
                if (_ShippingOrderUpdateDate != value)
                {
                    _ShippingOrderUpdateDate = value;
                    base.OnPropertyChanged("ShippingOrderUpdateDate", value);
                }
            }
        }

        #endregion

        #region 是否更新了合约或付款方式
        private bool isChargePayOrCon = false;
        /// <summary>
        /// 是否更新了合约或付款方式
        /// </summary>
        public bool IsChargePayOrCon
        {
            get
            {
                return isChargePayOrCon;
            }
            set
            {
                isChargePayOrCon = value;
            }
        }
        #endregion

        #region 是否存在合约
        private bool isHasContract = false;
        /// <summary>
        /// 是否存在合约
        /// </summary>
        public bool IsHasContract
        {
            get
            {
                return isHasContract;
            }
            set
            {
                isHasContract = value;
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
                    IsHasContract = _contractno != string.Empty ? true : false;
                    base.OnPropertyChanged("ContractNo", value);
                }
            }
        }

        public bool IsContract
        {
            get
            {
                return !string.IsNullOrEmpty(ContractNo);
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
                    IsChargePayOrCon = true;
                    base.OnPropertyChanged("ContractID", value);
                }
            }
        }

        #endregion
    }

    #endregion

    #region  OceanHBLInfo HBL详细信息

    /// <summary>
    /// OceanHBLInfo HBL详细信息
    /// </summary>
    [Serializable]
    public partial class OceanHBLInfo : OceanBLList
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
        /// 提单审核人ID（关联客户信息）
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



        AMSEntryType? _AMSEntryType;
        /// <summary>
        /// AMSEntryType(PorttoPort=60, InlandTransit = 61,TransitExport = 62,ImmediateReexport = 63,StayonBoard = 64
        /// </summary>
        public AMSEntryType? AMSEntry
        {
            get
            {
                return _AMSEntryType;
            }
            set
            {
                if (_AMSEntryType != value)
                {
                    _AMSEntryType = value;
                    base.OnPropertyChanged("AMSEntry", value);
                }
            }
        }

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


        string _placeofreceiptcode;
        /// <summary>
        /// 收货地代码
        /// </summary>
        public string PlaceOfReceiptCode
        {
            get
            {
                return _placeofreceiptcode;
            }
            set
            {
                if (_placeofreceiptcode != value)
                {
                    _placeofreceiptcode = value;
                    base.OnPropertyChanged("PlaceOfReceiptCode", value);
                }
            }
        }


        string _placeofreceiptname;
        /// <summary>
        /// 收货地名称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "交货地名称", EMessage = "PlaceOfReceiptName")]
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

        Guid _polid;
        /// <summary>
        /// 装货港代码
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

        Guid _podid;
        /// <summary>
        /// 卸货港代码
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


        string _polcode;
        /// <summary>
        /// 装货港代码
        /// </summary>
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


        string _transhipmentportname;
        /// <summary>
        /// 中转港
        /// </summary>
        public string TranshipmentPortName
        {
            get
            {
                return _transhipmentportname;
            }
            set
            {
                if (_transhipmentportname != value)
                {
                    _transhipmentportname = value;
                    base.OnPropertyChanged("TranshipmentPortName", value);
                }
            }
        }


        string _podcode;
        /// <summary>
        /// 卸货港代码
        /// </summary>
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


        string _finaldestinationcode;
        /// <summary>
        /// 最终目的地代码
        /// </summary>
        public string FinalDestinationCode
        {
            get
            {
                return _finaldestinationcode;
            }
            set
            {
                if (_finaldestinationcode != value)
                {
                    _finaldestinationcode = value;
                    base.OnPropertyChanged("FinalDestinationCode", value);
                }
            }
        }


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
        /// 运输条款
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


        Guid? _paymenttermid;
        /// <summary>
        /// 付款方式ID
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
                    IsChargePayOrCon = true;
                    base.OnPropertyChanged("PaymentTermID", value);
                }
            }
        }

        Guid? _Bookingpaymenttermid;
        /// <summary>
        /// Booking中的HBL付款方式ID
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


        string _paymenttermname;
        /// <summary>
        /// 付款方式
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


        VoyageShowType _voyageshowtype;
        /// <summary>
        /// 航次显示类型（1驳船、2大船、3全部）
        /// </summary>
        public VoyageShowType VoyageShowType
        {
            get
            {
                return _voyageshowtype;
            }
            set
            {
                if (_voyageshowtype != value)
                {
                    _voyageshowtype = value;
                    // base.OnPropertyChanged("VoyageShowType", value);
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


        Guid? _weightunitid;
        /// <summary>
        /// 重量单位ID
        /// </summary>
        [GuidRequired(CMessage = "重量单位", EMessage = "WeightUnit")]
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
        /// 重量单位
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
        [GuidRequired(CMessage = "体积单位", EMessage = "MeasurementUnit")]
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


        bool _iswoodpacking;
        /// <summary>
        /// 是否木质包装
        /// </summary>
        public bool IsWoodPacking
        {
            get
            {
                return _iswoodpacking;
            }
            set
            {
                if (_iswoodpacking != value)
                {
                    _iswoodpacking = value;
                    base.OnPropertyChanged("IsWoodPacking", value);
                }
            }
        }


        string _containerdescription;
        /// <summary>
        /// 集装箱详细信息
        /// </summary>
        public string ContainerDescription
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


        Guid _issueplaceid;
        /// <summary>
        /// 签发地ID
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

        string _amsno;
        /// <summary>
        /// AMS（AUTOMATED MANIFEST SYSTEM自动舱单系统）号码
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "AMS号码", EMessage = "AMSNo")]
        public string AMSNo
        {
            get
            {
                return _amsno;
            }
            set
            {
                if (_amsno != value)
                {
                    _amsno = value;
                    base.OnPropertyChanged("AMSNo", value);
                }
            }
        }


        string _amsshippername;
        /// <summary>
        /// AMS发货人
        /// </summary>
        public string AMSShipperName
        {
            get
            {
                return _amsshippername;
            }
            set
            {
                if (_amsshippername != value)
                {
                    _amsshippername = value;
                    base.OnPropertyChanged("AMSShipperName", value);
                }
            }
        }


        CustomerDescription _amsshipperdescription;
        /// <summary>
        /// AMS发货人详细信息
        /// </summary>
        public CustomerDescription AMSShipperDescription
        {
            get
            {
                return _amsshipperdescription;
            }
            set
            {
                if (_amsshipperdescription != value)
                {
                    _amsshipperdescription = value;
                    base.OnPropertyChanged("AMSShipperDescription", value);
                }
            }
        }


        string _amsconsigneename;
        /// <summary>
        /// AMS收货人
        /// </summary>
        public string AMSConsigneeName
        {
            get
            {
                return _amsconsigneename;
            }
            set
            {
                if (_amsconsigneename != value)
                {
                    _amsconsigneename = value;
                    base.OnPropertyChanged("AMSConsigneeName", value);
                }
            }
        }

        CustomerDescription _amsconsigneedescription;
        /// <summary>
        /// AMS收货人详细信息
        /// </summary>
        public CustomerDescription AMSConsigneeDescription
        {
            get
            {
                return _amsconsigneedescription;
            }
            set
            {
                if (_amsconsigneedescription != value)
                {
                    _amsconsigneedescription = value;
                    base.OnPropertyChanged("AMSConsigneeDescription", value);
                }
            }
        }

        string _amsnotifypartyname;
        /// <summary>
        /// AMS通知人
        /// </summary>
        public string AMSNotifyPartyName
        {
            get
            {
                return _amsnotifypartyname;
            }
            set
            {
                if (_amsnotifypartyname != value)
                {
                    _amsnotifypartyname = value;
                    base.OnPropertyChanged("AMSNotifyPartyName", value);
                }
            }
        }


        CustomerDescription _amsnotifypartydescription;
        /// <summary>
        /// AMS通知人详细信息
        /// </summary>
        public CustomerDescription AMSNotifyPartyDescription
        {
            get
            {
                return _amsnotifypartydescription;
            }
            set
            {
                if (_amsnotifypartydescription != value)
                {
                    _amsnotifypartydescription = value;
                    base.OnPropertyChanged("AMSNotifyPartyDescription", value);
                }
            }
        }

        string _isfno;
        /// <summary>
        /// ISF（IMPORTER SECURITY FILING进口安全申报）号码
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "ISF号码", EMessage = "ISFNo")]
        public string ISFNo
        {
            get
            {
                return _isfno;
            }
            set
            {
                if (_isfno != value)
                {
                    _isfno = value;
                    base.OnPropertyChanged("ISFNo", value);
                }
            }
        }

        ACIEntryType? _acientrytype;
        /// <summary>
        /// 24=Imported Goods 23=Intransit Goods to US 26=FROB
        /// </summary>
        public ACIEntryType? ACIEntryType
        {
            get
            {
                return _acientrytype;
            }
            set
            {
                if (_acientrytype != value)
                {
                    _acientrytype = value;
                    base.OnPropertyChanged("ACIEntryType", value);
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

        string _WoodPacking;
        /// <summary>
        /// 木质包装的描述
        /// </summary>
        public string WoodPacking
        {
            get
            {
                return _WoodPacking;
            }
            set
            {
                if (_WoodPacking != value)
                {
                    _WoodPacking = value;
                    base.OnPropertyChanged("WoodPacking", value);
                }
            }
        }

        string _ctnQtyInfo;
        /// <summary>
        /// 集装箱件数合计
        /// </summary>
        public string CtnQtyInfo
        {
            get
            {
                return _ctnQtyInfo;
            }
            set
            {
                if (_ctnQtyInfo != value)
                {
                    _ctnQtyInfo = value;
                    base.OnPropertyChanged("CtnQtyInfo", value);
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

    #region OceanMBLInfo MBL详细信息

    /// <summary>
    /// OceanMBLInfo
    /// </summary>
    [KnownType(typeof(CustomerDescription))]
    [Serializable]
    public partial class OceanMBLInfo : OceanBLList
    {
        //Guid _polid;
        ///// <summary>
        ///// 装货港
        //// </summary>
        //[GuidRequired(ErrorMessage = "装货港必须填写")]
        //public Guid POLID
        //{
        //    get
        //    {
        //        return _polid;
        //    }
        //    set
        //    {
        //        if (_polid != value)
        //        {
        //            _polid = value;
        //            base.OnPropertyChanged("POLID", value);
        //        }
        //    }
        //}

        //Guid _podid;
        ///// <summary>
        ///// 卸货港
        //// </summary>
        //[GuidRequired(ErrorMessage = "卸货港必须填写")]
        //public Guid PODID
        //{
        //    get
        //    {
        //        return _podid;
        //    }
        //    set
        //    {
        //        if (_podid != value)
        //        {
        //            _podid = value;
        //            base.OnPropertyChanged("PODID", value);
        //        }
        //    }
        //}


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

        string _AgentText;
        /// <summary>
        /// 代理文本(用于打印)
        /// </summary>
        public string AgentText
        {
            get
            {
                return _AgentText;
            }
            set
            {
                if (_AgentText != value)
                {
                    _AgentText = value;
                    base.OnPropertyChanged("AgentText", value);
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


        string _placeofreceiptcode;
        /// <summary>
        /// 收货地代码
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "收货地代码", EMessage = "PlaceOfReceiptCode")]
        public string PlaceOfReceiptCode
        {
            get
            {
                return _placeofreceiptcode;
            }
            set
            {
                if (_placeofreceiptcode != value)
                {
                    _placeofreceiptcode = value;
                    base.OnPropertyChanged("PlaceOfReceiptCode", value);
                }
            }
        }

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

        Guid _polid;
        /// <summary>
        /// 交货地ID
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

        string _polcode;
        /// <summary>
        /// 装货港代码
        /// </summary>
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


        string _podcode;
        /// <summary>
        /// 卸货港代码
        /// </summary>
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


        string _finaldestinationcode;
        /// <summary>
        /// 最终目的地代码
        /// </summary>
        public string FinalDestinationCode
        {
            get
            {
                return _finaldestinationcode;
            }
            set
            {
                if (_finaldestinationcode != value)
                {
                    _finaldestinationcode = value;
                    base.OnPropertyChanged("FinalDestinationCode", value);
                }
            }
        }


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
        /// 运输条款
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

        Guid? _paymenttermid;
        /// <summary>
        /// 付费条款ID
        /// </summary>
        [GuidRequired(CMessage = "付费条款", EMessage = "PaymentTerm")]
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
                    IsChargePayOrCon = true;
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


        VoyageShowType _voyageshowtype;
        /// <summary>
        /// 航次显示类型（1驳船、2大船、3全部）
        /// </summary>
        public VoyageShowType VoyageShowType
        {
            get
            {
                return _voyageshowtype;
            }
            set
            {
                if (_voyageshowtype != value)
                {
                    _voyageshowtype = value;
                    //base.OnPropertyChanged("VoyageShowType", value);
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
        [GuidRequired(CMessage = "重量单位", EMessage = "WeightUnit")]
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
        /// 重量单位
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


        bool _iswoodpacking;
        /// <summary>
        /// 是否木质包装
        /// </summary>
        public bool IsWoodPacking
        {
            get
            {
                return _iswoodpacking;
            }
            set
            {
                if (_iswoodpacking != value)
                {
                    _iswoodpacking = value;
                    base.OnPropertyChanged("IsWoodPacking", value);
                }
            }
        }


        string _containerdescription;
        /// <summary>
        /// 集装箱详细信息
        /// </summary>
        public string ContainerDescription
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

        ConfirmOnBoardType _confirmonboardtype;
        /// <summary>
        /// 确认装船类型（0不确定，1前程确定，2确定）
        /// </summary>
        public ConfirmOnBoardType ConfirmOnBoardType
        {
            get
            {
                return _confirmonboardtype;
            }
            set
            {
                if (_confirmonboardtype != value)
                {
                    _confirmonboardtype = value;
                    base.OnPropertyChanged("ConfirmOnBoardType", value);
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

        string _HBLNos;
        /// <summary>
        /// HBL NOs
        /// </summary>
        public string HBLNos
        {
            get
            {
                return _HBLNos;
            }
            set
            {
                if (_HBLNos != value)
                {
                    _HBLNos = value;
                    base.OnPropertyChanged("HBLNos", value);
                }
            }
        }

        string _WoodPacking;
        /// <summary>
        /// 木质包装的描述
        /// </summary>
        public string WoodPacking
        {
            get
            {
                return _WoodPacking;
            }
            set
            {
                if (_WoodPacking != value)
                {
                    _WoodPacking = value;
                    base.OnPropertyChanged("WoodPacking", value);
                }
            }
        }

        string _ctnQtyInfo;
        /// <summary>
        /// 集装箱件数合计
        /// </summary>
        public string CtnQtyInfo
        {
            get
            {
                return _ctnQtyInfo;
            }
            set
            {
                if (_ctnQtyInfo != value)
                {
                    _ctnQtyInfo = value;
                    base.OnPropertyChanged("CtnQtyInfo", value);
                }
            }
        }

        bool _showPreVoyage;
        /// <summary>
        /// 显示驳船
        /// </summary>
        public bool ShowPreVoyage
        {
            get
            {
                return _showPreVoyage;
            }
            set
            {
                if (_showPreVoyage != value)
                {
                    _showPreVoyage = value;
                    base.OnPropertyChanged("ShowPreVoyage", value);
                }
            }
        }

        bool _showVoyage;
        /// <summary>
        /// 显示大船
        /// </summary>
        public bool ShowVoyage
        {
            get
            {
                return _showVoyage;
            }
            set
            {
                if (_showVoyage != value)
                {
                    _showVoyage = value;
                    base.OnPropertyChanged("ShowVoyage", value);
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

    #region OceanBLContainerList

    /// <summary>
    /// OceanBLContainerList
    /// </summary>
    [Serializable]
    public partial class OceanBLContainerList : OceanContainerList
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
    [KnownType(typeof(CustomerDescription))]
    [Serializable]
    public class SaveMBLInfoParameter
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? id { get; set; }
        /// <summary>
        /// 订舱
        /// </summary>
        public Guid oceanBookingID { get; set; }
        /// <summary>
        /// MBL号
        /// </summary>
        public string mblNo { get; set; }
        /// <summary>
        /// 提单份数
        /// </summary>
        public short? numberOfOriginal { get; set; }
        /// <summary>
        /// 航次显示类型
        /// </summary>
        public VoyageShowType voyageShowType { get; set; }
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
        public Guid polID { get; set; }
        /// <summary>
        /// 装货港名
        /// </summary>
        public string polName { get; set; }
        /// <summary>
        /// 卸货港
        /// </summary>
        public Guid podID { get; set; }
        /// <summary>
        /// 卸货港名
        /// </summary>
        public string podName { get; set; }
        /// <summary>
        /// 交货地
        /// </summary>
        public Guid? placeOfDeliveryID { get; set; }
        /// <summary>
        /// 交货地名
        /// </summary>
        public string placeOfDeliveryName { get; set; }
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
        /// 付款方式
        /// </summary>
        public Guid? paymentTermID { get; set; }
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
        /// 是否木质包装
        /// </summary>
        public bool isWoodPacking { get; set; }
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
        /// 木质包装的字符串
        /// </summary>
        public string woodPacking { get; set; }
        /// <summary>
        /// 签单类型
        /// </summary>
        public IssueType issueType { get; set; }
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID { get; set; }

        /// <summary>
        /// 代理文本(用于打印)
        /// </summary>
        public string AgentText { get; set; }
        /// <summary>
        /// 合约ID
        /// </summary>
        public Guid? FreightRateID { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? updateDate { get; set; }
        public DateTime? PreETD;
        public DateTime? ETD;
        public DateTime? ETA;
        public DateTime? ClosingDate;
        public DateTime? CYClosingDate;
        public DateTime? DOCClosingDate;

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
        /// 订舱
        /// </summary>
        public Guid oceanBookingID { get; set; }
        /// <summary>
        /// mblID
        /// </summary>
        public Guid mblID { get; set; }
        /// <summary>
        /// mblNO,如果传入新的MBLNO则在系统自动生成一个新的MBL
        /// </summary>
        public string mblNO { get; set; }
        /// <summary>
        /// 提单号
        /// </summary>
        public string hblNo { get; set; }
        /// <summary>
        /// 提单份数
        /// </summary>
        public short? numberOfOriginal { get; set; }
        /// <summary>
        /// 航次显示类型
        /// </summary>
        public VoyageShowType voyageShowType { get; set; }
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
        /// 二程航次
        /// </summary>
        public Guid? voyageID { get; set; }
        /// <summary>
        /// 装货港
        /// </summary>
        public Guid polID { get; set; }
        /// <summary>
        /// 装货港名
        /// </summary>
        public string polName { get; set; }
        /// <summary>
        /// 卸货港
        /// </summary>
        public Guid podID { get; set; }
        /// <summary>
        /// 卸货港名
        /// </summary>
        public string podName { get; set; }
        /// <summary>
        /// 交货地
        /// </summary>
        public Guid? placeOfDeliveryID { get; set; }
        /// <summary>
        /// 交货地名
        /// </summary>
        public string placeOfDeliveryName { get; set; }
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
        /// 付款方式
        /// </summary>
        public Guid? paymentTermID { get; set; }
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
        public decimal? weight { get; set; }
        /// <summary>
        /// 重量单位
        /// </summary>
        public Guid? weightUnitID { get; set; }
        /// <summary>
        /// 体积
        /// </summary>
        public decimal? measurement { get; set; }
        /// <summary>
        /// 体积单位
        /// </summary>
        public Guid? measurementUnitID { get; set; }
        /// <summary>
        /// 货物标示
        /// </summary>
        public string marks { get; set; }
        /// <summary>
        /// 货物描述
        /// </summary>
        public string goodsDescription { get; set; }
        /// <summary>
        /// 是否木质包装
        /// </summary>
        public bool isWoodPacking { get; set; }
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
        /// AMS No
        /// </summary>
        public string amsNo { get; set; }
        /// <summary>
        /// AMS发货人描述
        /// </summary>
        public CustomerDescription amsShipperDescription { get; set; }
        /// <summary>
        /// AMS收货人描述
        /// </summary>
        public CustomerDescription amsConsigneeDescription { get; set; }
        /// <summary>
        /// AMS通知人描述
        /// </summary>
        public CustomerDescription amsNotifyPartyDescription { get; set; }
        /// <summary>
        /// ISF No
        /// </summary>
        public string isfNo { get; set; }
        /// <summary>
        /// ACI 进口类型
        /// </summary>
        public ACIEntryType? aciEntryType { get; set; }
        /// <summary>
        /// AMS 进口类型
        /// </summary>
        public AMSEntryType? amsEntryType { get; set; }
        /// <summary>
        /// 木质包装的字符串
        /// </summary>
        public string woodPacking { get; set; }
        /// <summary>
        /// 签单类型
        /// </summary>
        public IssueType issueType { get; set; }
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
        public DateTime? PreETD { get; set; }
        public DateTime? ETD { get; set; }
        public DateTime? ETA { get; set; }
    }

    /// <summary>
    /// 保存提单的箱的参数
    /// </summary>
    [Serializable]
    public class SaveBLContainerParameter
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid oceanBookingID { get; set; }
        /// <summary>
        /// 提单ID
        /// </summary>
        public Guid blID { get; set; }
        /// <summary>
        /// 关联
        /// </summary>
        public bool[] relations { get; set; }
        /// <summary>
        /// OceanContainers箱ID列表
        /// </summary>
        public Guid?[] ids { get; set; }
        /// <summary>
        /// OceanContainers箱ID列表
        /// </summary>
        public Guid?[] cargoIds { get; set; }
        /// <summary>
        /// 箱号列表
        /// </summary>
        public string[] containerNos { get; set; }
        /// <summary>
        /// 装货单号列表
        /// </summary>
        public string[] containerSOs { get; set; }
        /// <summary>
        /// 箱型列表
        /// </summary>
        public Guid[] containerTypeIDs { get; set; }
        /// <summary>
        /// 封条号列表
        /// </summary>
        public string[] containerSealNos { get; set; }
        /// <summary>
        /// 唛头列表
        /// </summary>
        public string[] containerMarks { get; set; }
        /// <summary>
        /// 品名列表
        /// </summary>
        public string[] containerCommoditys { get; set; }
        /// <summary>
        /// 数量列表
        /// </summary>
        public int[] containerQuantitys { get; set; }
        /// <summary>
        /// 箱重量列表
        /// </summary>
        public decimal[] containerWeights { get; set; }
        /// <summary>
        /// 箱体积列表
        /// </summary>
        public decimal[] containerMeasurements { get; set; }
        /// <summary>
        /// 是否客户自有箱列表
        /// </summary>
        public bool[] containerIsSOCs { get; set; }
        /// <summary>
        /// 是否一个柜子出两套或两套以上的提单
        /// </summary>
        public bool[] containerIsPartOfs { get; set; }
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID { get; set; }
        /// <summary>
        /// OceanContainers更新时间-做数据版本用
        /// </summary>
        public DateTime?[] updateDates { get; set; }
        /// <summary>
        /// 货物更新时间-做数据版本用
        /// </summary>
        public DateTime?[] cargoUpdateDates { get; set; }

    }

    #endregion
}
