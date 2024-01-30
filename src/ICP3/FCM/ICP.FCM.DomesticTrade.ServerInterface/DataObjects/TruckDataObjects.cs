﻿namespace ICP.FCM.DomesticTrade.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic; 
    using Common.ServiceInterface.DataObjects;
    using Framework.CommonLibrary.Common;

    #region Truck

    /// <summary>
    /// OceanTruckInfo
    /// </summary>
    [Serializable]
    public partial class DTTruckInfo : BaseDataObject
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


        Guid _oceanshippingorderid;
        /// <summary>
        /// 装货单ID
        /// </summary>
        public Guid OceanBookingID
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


        string _shippingorderno;
        /// <summary>
        /// 订舱号
        /// </summary>
        public string ShippingOrderNo
        {
            get
            {
                return _shippingorderno;
            }
            set
            {
                if (_shippingorderno != value)
                {
                    _shippingorderno = value;
                    base.OnPropertyChanged("ShippingOrderNo", value);
                }
            }
        }


        string _NO;
        /// <summary>
        /// 派车单号
        /// </summary>
        public string NO
        {
            get
            {
                return _NO;
            }
            set
            {
                if (_NO != value)
                {
                    _NO = value;
                    base.OnPropertyChanged("NO", value);
                }
            }
        }


        DateTime? _bookingtime;
        /// <summary>
        /// 订舱时间
        /// </summary>
        public DateTime? BookingTime
        {
            get
            {
                return _bookingtime;
            }
            set
            {
                if (_bookingtime != value)
                {
                    _bookingtime = value;
                    base.OnPropertyChanged("BookingTime", value);
                }
            }
        }


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


        Guid? _carrierId;
        /// <summary>
        /// 船公司GUID
        /// </summary>
        public Guid? CarrierID
        {
            get
            {
                return _carrierId;
            }
            set
            {
                if (_carrierId != value)
                {
                    _carrierId = value;
                    base.OnPropertyChanged("CarrierID", value);
                }
            }
        }


        string _carrier;
        /// <summary>
        /// 船公司名称
        /// </summary>
        public string CarrierName
        {
            get
            {
                return _carrier;
            }
            set
            {
                if (_carrier != value)
                {
                    _carrier = value;
                    base.OnPropertyChanged("CarrierName", value);
                }
            }
        }


        Guid _truckerid;
        /// <summary>
        /// 拖车行ID
        /// </summary>
        [GuidRequired(CMessage = "拖车行",EMessage="Trucker")]
        public Guid TruckerID
        {
            get
            {
                return _truckerid;
            }
            set
            {
                if (_truckerid != value)
                {
                    _truckerid = value;
                    base.OnPropertyChanged("TruckerID", value);
                }
            }
        }


        string _truckername;
        /// <summary>
        /// 拖车公司
        /// </summary>
        public string TruckerName
        {
            get
            {
                return _truckername;
            }
            set
            {
                if (_truckername != value)
                {
                    _truckername = value;
                    base.OnPropertyChanged("TruckerName", value);
                }
            }
        }


        CustomerDescription _truckerdescription;
        /// <summary>
        /// 拖车公司描述
        /// </summary>
        public CustomerDescription TruckerDescription
        {
            get
            {
                return _truckerdescription;
            }
            set
            {
                if (_truckerdescription != value)
                {
                    _truckerdescription = value;
                    base.OnPropertyChanged("TruckerDescription", value);
                }
            }
        }


        Guid? _pickupatid;
        /// <summary>
        /// 提柜地点ID（关联客户）
        /// </summary>
        public Guid? PickUpAtID
        {
            get
            {
                return _pickupatid;
            }
            set
            {
                if (_pickupatid != value)
                {
                    _pickupatid = value;
                    base.OnPropertyChanged("PickUpAtID", value);
                }
            }
        }


        string _pickupatname;
        /// <summary>
        /// 提柜地点名称（关联客户）
        /// </summary>
        public string PickUpAtName
        {
            get
            {
                return _pickupatname;
            }
            set
            {
                if (_pickupatname != value)
                {
                    _pickupatname = value;
                    base.OnPropertyChanged("PickUpAtName", value);
                }
            }
        }


        CustomerDescription _pickupatdescription;
        /// <summary>
        /// 提柜地点描述
        /// </summary>
        public CustomerDescription PickUpAtDescription
        {
            get
            {
                return _pickupatdescription;
            }
            set
            {
                if (_pickupatdescription != value)
                {
                    _pickupatdescription = value;
                    base.OnPropertyChanged("PickUpAtDescription", value);
                }
            }
        }


        Guid _shipperid;
        /// <summary>
        /// 装货地ID
        /// </summary>
        [GuidRequired(CMessage = "装货地",EMessage="Shipper Place")]
        public Guid ShipperID
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
        /// 装货地
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


        CustomerDescription _shipperdescription;
        /// <summary>
        /// 装货地描述
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


        DateTime _loadingtime;
        /// <summary>
        /// 装货时间
        /// </summary>
        [Required(CMessage = "装货时间",EMessage="LoadingTime")]
        public DateTime LoadingTime
        {
            get
            {
                return _loadingtime;
            }
            set
            {
                if (_loadingtime != value)
                {
                    _loadingtime = value;
                    base.OnPropertyChanged("LoadingTime", value);
                }
            }
        }


        Guid _deliveryatid;
        /// <summary>
        /// 还柜地点ID（关联客户）
        /// </summary>
        [GuidRequired(CMessage="还柜地",EMessage="DeliveryAt")]
        public Guid DeliveryAtID
        {
            get
            {
                return _deliveryatid;
            }
            set
            {
                if (_deliveryatid != value)
                {
                    _deliveryatid = value;
                    base.OnPropertyChanged("DeliveryAtID", value);
                }
            }
        }


        string _deliveryatname;
        /// <summary>
        /// 还柜地点名称（关联客户）
        /// </summary>
        public string DeliveryAtName
        {
            get
            {
                return _deliveryatname;
            }
            set
            {
                if (_deliveryatname != value)
                {
                    _deliveryatname = value;
                    base.OnPropertyChanged("DeliveryAtName", value);
                }
            }
        }


        CustomerDescription _deliveryatdescription;
        /// <summary>
        /// 还柜地点描述
        /// </summary>
        public CustomerDescription DeliveryAtDescription
        {
            get
            {
                return _deliveryatdescription;
            }
            set
            {
                if (_deliveryatdescription != value)
                {
                    _deliveryatdescription = value;
                    base.OnPropertyChanged("DeliveryAtDescription", value);
                }
            }
        }



        bool _isdrivinglicence;
        /// <summary>
        /// 是否需要司机本
        /// </summary>
        public bool IsDrivingLicence
        {
            get
            {
                return _isdrivinglicence;
            }
            set
            {
                if (_isdrivinglicence != value)
                {
                    _isdrivinglicence = value;
                    base.OnPropertyChanged("IsDrivingLicence", value);
                }
            }
        }


        Guid? _customsbrokerid;
        /// <summary>
        /// 报关公司ID
        /// </summary>
        public Guid? CustomsBrokerID
        {
            get
            {
                return _customsbrokerid;
            }
            set
            {
                if (_customsbrokerid != value)
                {
                    _customsbrokerid = value;
                    base.OnPropertyChanged("CustomsBrokerID", value);
                }
            }
        }


        string _customsbrokername;
        /// <summary>
        /// 报关公司名称（关联客户）
        /// </summary>
        public string CustomsBrokerName
        {
            get
            {
                return _customsbrokername;
            }
            set
            {
                if (_customsbrokername != value)
                {
                    _customsbrokername = value;
                    base.OnPropertyChanged("CustomsBrokerName", value);
                }
            }
        }


        CustomerDescription _customsbrokerdescription;
        /// <summary>
        /// 报关公司描述
        /// </summary>
        public CustomerDescription CustomsBrokerDescription
        {
            get
            {
                return _customsbrokerdescription;
            }
            set
            {
                if (_customsbrokerdescription != value)
                {
                    _customsbrokerdescription = value;
                    base.OnPropertyChanged("CustomsBrokerDescription", value);
                }
            }
        }


        string _freigtdescription;
        /// <summary>
        /// 运费描述
        /// </summary>
        public string FreigtDescription
        {
            get
            {
                return _freigtdescription;
            }
            set
            {
                if (_freigtdescription != value)
                {
                    _freigtdescription = value;
                    base.OnPropertyChanged("FreigtDescription", value);
                }
            }
        }


        List<DTTruckContainerInfo> _containers;
        /// <summary>
        /// 箱列表
        /// </summary>
        public List<DTTruckContainerInfo> Containers
        {
            get
            {
                return _containers;
            }
            set
            {
                if (_containers != value)
                {
                    _containers = value;
                    base.OnPropertyChanged("Containers", value);
                }
            }
        }


        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaximumLength = 500, CMessage = "备注", EMessage = "Remark")]

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
        /// 更新时间
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
    /// 拖车数据
    /// </summary>
    [Serializable]
    public partial class DTTruckContainerInfo : DTContainerList
    {

        Guid _ownerid;
        /// <summary>
        /// 所属单的ID
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

        Guid _containerid;
        /// <summary>
        /// 箱ID
        /// </summary>
        public Guid ContainerID
        {
            get
            {
                return _containerid;
            }
            set
            {
                if (_containerid != value)
                {
                    _containerid = value;
                    base.OnPropertyChanged("ContainerID", value);
                }
            }
        }
    }
    
    #endregion
}