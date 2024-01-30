namespace ICP.FCM.AirExport.ServiceInterface.DataObjects
{
    using System;
    using ICP.Framework.CommonLibrary.Common;

    #region Container

    #region  AirContainerList

    /// <summary>
    /// 箱
    /// </summary>
    [Serializable]
    public partial class AirContainerList : BaseDataObject
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

        Guid _AirBookingID;
        /// <summary>
        /// 装货单ID
        /// </summary>
        public Guid AirBookingID
        {
            get
            {
                return _AirBookingID;
            }
            set
            {
                if (_AirBookingID != value)
                {
                    _AirBookingID = value;
                    base.OnPropertyChanged("AirBookingID", value);
                }
            }
        }

        string _shippingorderno;
        /// <summary>
        /// 装货单号（有些船公司是每个柜提供一个号的）
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

        string _no;
        /// <summary>
        /// 箱号
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "箱号", EMessage = "NO")]
        [Required(CMessage = "箱号", EMessage = "NO")]
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


        Guid _typeid;
        /// <summary>
        /// 箱型ID
        /// </summary>
        [GuidRequired(CMessage = "箱型",EMessage="Type")]
        public Guid TypeID
        {
            get
            {
                return _typeid;
            }
            set
            {
                if (_typeid != value)
                {
                    _typeid = value;
                    base.OnPropertyChanged("TypeID", value);
                }
            }
        }


        string _typename;
        /// <summary>
        /// 箱型
        /// </summary>
        public string TypeName
        {
            get
            {
                return _typename;
            }
            set
            {
                if (_typename != value)
                {
                    _typename = value;
                    base.OnPropertyChanged("TypeName", value);
                }
            }
        }


        string _sealno;
        /// <summary>
        /// 铅封号
        /// </summary>
        [StringLength(MaximumLength = 500, CMessage = "铅封号", EMessage = "SealNo")]
        [Required(CMessage = "铅封号", EMessage = "SealNo")]
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


        bool _issoc;
        /// <summary>
        /// 是否客户自有箱
        /// </summary>
        [Required(CMessage = "是否客户自有箱",EMessage="IsSOC")]
        public bool IsSOC
        {
            get
            {
                return _issoc;
            }
            set
            {
                if (_issoc != value)
                {
                    _issoc = value;
                    base.OnPropertyChanged("IsSOC", value);
                }
            }
        }


        bool _ispartof;
        /// <summary>
        /// 是否一个柜子出两套或两套以上的提单（这里仅仅只整箱情况）
        /// </summary>
        [Required(CMessage = "是否一个柜子出两套或两套以上的提单（这里仅仅只整箱情况）",EMessage="IsPartOf")]
        public bool IsPartOf
        {
            get
            {
                return _ispartof;
            }
            set
            {
                if (_ispartof != value)
                {
                    _ispartof = value;
                    base.OnPropertyChanged("IsPartOf", value);
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

        string _UpdateByName;
        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateByName
        {
            get
            {
                return _UpdateByName;
            }
            set
            {
                if (_UpdateByName != value)
                {
                    _UpdateByName = value;
                    base.OnPropertyChanged("UpdateByName", value);
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

        Guid? _updateByID;
        /// <summary>
        /// 修改人ID
        /// </summary>
        public Guid? UpdateByID
        {
            get
            {
                return _updateByID;
            }
            set
            {
                if (_updateByID != value)
                {
                    _updateByID = value;
                    base.OnPropertyChanged("UpdateByID", value);
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

        DateTime? _DeliveryDate;
        /// <summary>
        /// 出发日
        /// </summary>
        public DateTime? DeliveryDate
        {
            get
            {
                return _DeliveryDate;
            }
            set
            {
                if (_DeliveryDate != value)
                {
                    _DeliveryDate = value;
                    base.OnPropertyChanged("DeliveryDate", value);
                }
            }
        }

        DateTime? _ArriveDate;
        /// <summary>
        /// 到达日
        /// </summary>
        public DateTime? ArriveDate
        {
            get
            {
                return _ArriveDate;
            }
            set
            {
                if (_ArriveDate != value)
                {
                    _ArriveDate = value;
                    base.OnPropertyChanged("ArriveDate", value);
                }
            }
        }

        DateTime? _ReturnDate;
        /// <summary>
        /// 还柜日
        /// </summary>
        public DateTime? ReturnDate
        {
            get
            {
                return _ReturnDate;
            }
            set
            {
                if (_ReturnDate != value)
                {
                    _ReturnDate = value;
                    base.OnPropertyChanged("ReturnDate", value);
                }
            }
        }

        string _DriverName;
        /// <summary>
        /// 司机
        /// </summary>
        public string DriverName
        {
            get
            {
                return _DriverName;
            }
            set
            {
                if (_DriverName != value)
                {
                    _DriverName = value;
                    base.OnPropertyChanged("DriverName", value);
                }
            }
        }

        string _CarNo;
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNo
        {
            get
            {
                return _CarNo;
            }
            set
            {
                if (_CarNo != value)
                {
                    _CarNo = value;
                    base.OnPropertyChanged("CarNo", value);
                }
            }
        }
    }

    #endregion

    #region AirContainerCargoList

    /// <summary>
    /// 货物
    /// </summary>
    [Serializable]
    public partial class AirContainerCargoList : BaseDataObject
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

        Guid _BLID;
        /// <summary>
        /// BLID
        /// </summary>
        public Guid BLID
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

        string _MBLNo;
        /// <summary>
        /// MBLNO
        /// </summary>
        public string MBLNo
        {
            get
            {
                return _MBLNo;
            }
            set
            {
                if (_MBLNo != value)
                {
                    _MBLNo = value;
                    base.OnPropertyChanged("MBLNo", value);
                }
            }
        }

        Guid _oceancontainerid;
        /// <summary>
        /// 箱ID
        /// </summary>
        public Guid AirContainerID
        {
            get
            {
                return _oceancontainerid;
            }
            set
            {
                if (_oceancontainerid != value)
                {
                    _oceancontainerid = value;
                    base.OnPropertyChanged("AirContainerID", value);
                }
            }
        }


        string _marks;
        /// <summary>
        /// 唛头
        /// </summary>
        [StringLength(MaximumLength=400,CMessage="唛头",EMessage="Marks")]
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
        [StringLength(MaximumLength=400,CMessage="品名",EMessage="Commdity")]
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
        /// 数量
        /// </summary>
        [Required(CMessage = "数量",EMessage="Quantity")]
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
        /// 数量单位ID
        /// </summary>
        [GuidRequired(CMessage = "数量单位",EMessage="QuantityUnit")]
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
        [Required(CMessage = "重量",EMessage="Weight")]
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
        [GuidRequired(CMessage = "重量单位",EMessage="WeightUnit")]
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
        [Required(CMessage = "体积",EMessage="Measurement")]
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
        [GuidRequired(CMessage = "体积单位",EMessage="MeasurementUnit")]
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

    #endregion
}
