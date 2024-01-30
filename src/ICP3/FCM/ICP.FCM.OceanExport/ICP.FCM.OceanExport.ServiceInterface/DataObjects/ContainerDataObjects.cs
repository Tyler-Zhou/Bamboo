﻿namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects
{
    using System;
    using Framework.CommonLibrary.Common;

    #region Container

    #region  OceanContainerList

    /// <summary>
    /// 箱
    /// </summary>
    [Serializable]
    public partial class OceanContainerList : BaseDataObject
    {
        /// <summary>
        /// 用在HBL箱列表中，选择输入所在HBL的箱
        /// </summary>
        public bool IsSelected { get; set; }

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

        Guid _OceanBookingID;
        /// <summary>
        /// 装货单ID
        /// </summary>
        public Guid OceanBookingID
        {
            get
            {
                return _OceanBookingID;
            }
            set
            {
                if (_OceanBookingID != value)
                {
                    _OceanBookingID = value;
                    base.OnPropertyChanged("OceanBookingID", value);
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
        [StringLength(MaximumLength = 20, CMessage = "箱号", EMessage = "No")]
        [Required(CMessage = "箱号", EMessage = "No")]
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
        [GuidRequired(CMessage = "箱型", EMessage = "Type")]
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

        string _ctnoper;
        /// <summary>
        /// CTNOper
        /// </summary>    
        public string CTNOper
        {
            get
            {
                return _ctnoper;
            }
            set
            {
                if (_ctnoper != value)
                {
                    _ctnoper = value;
                    base.OnPropertyChanged("CTNOper", value);
                }
            }
        }

       

        decimal _vgmcrossweight;
        /// <summary>
        /// VGMCrossWeight
        /// </summary>    
        public decimal VGMCrossWeight
        {
            get
            {
                return _vgmcrossweight;
            }
            set
            {
                if (_vgmcrossweight != value)
                {
                    _vgmcrossweight = value;
                    base.OnPropertyChanged("VGMCrossWeight", value);
                }
            }
        }

        string _vgmmethod;
        /// <summary>
        /// VGMMethod
        /// </summary>    
        public string VGMMethod
        {
            get
            {
                return _vgmmethod;
            }
            set
            {
                if (_vgmmethod != value)
                {
                    _vgmmethod = value;
                    base.OnPropertyChanged("VGMMethod", value);
                }
            }
        }

        string _pono;
        /// <summary>
        /// PONO
        /// </summary>    
        public string PONO
        {
            get
            {
                return _pono;
            }
            set
            {
                if (_pono != value)
                {
                    _pono = value;
                    base.OnPropertyChanged("PONO", value);
                }
            }
        }

        bool _issoc;
        /// <summary>
        /// 是否客户自有箱
        /// </summary>
        [Required(CMessage = "是否客户自有箱", EMessage = "IsSoc")]
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
        [Required(CMessage = "是否一个柜子出两套或两套以上的提单（这里仅仅只整箱情况）", EMessage = "IsPartOf")]
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

        private string _HSCode;
        /// <summary>
        /// HSCode
        /// </summary>
        public string HSCode
        {
            get
            {
                return _HSCode;
            }
            set
            {
                if (_HSCode != value)
                {
                    _HSCode = value;
                    base.OnPropertyChanged("HSCode", value);
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

    #region OceanContainerCargoList

    /// <summary>
    /// 货物
    /// </summary>
    [Serializable]
    public partial class OceanContainerCargoList : BaseDataObject
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

        string _BLNo;
        /// <summary>
        /// 分提单号
        /// </summary>
        public string BLNo
        {
            get
            {
                return _BLNo;
            }
            set
            {
                if (_BLNo != value)
                {
                    _BLNo = value;
                    base.OnPropertyChanged("BLNo", value);
                }
            }
        }

        Guid _MBLID;
        /// <summary>
        /// MBLID
        /// </summary>
        public Guid MBLID
        {
            get
            {
                return _MBLID;
            }
            set
            {
                if (_MBLID != value)
                {
                    _MBLID = value;
                    base.OnPropertyChanged("MBLID", value);
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
        public Guid OceanContainerID
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
                    base.OnPropertyChanged("OceanContainerID", value);
                }
            }
        }

        string _OceanContainerNo;
        /// <summary>
        /// 箱号
        /// </summary>
        public string OceanContainerNo
        {
            get
            {
                return _OceanContainerNo;
            }
            set
            {
                if (_OceanContainerNo != value)
                {
                    _OceanContainerNo = value;
                    base.OnPropertyChanged("OceanContainerNo", value);
                }
            }
        }

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
        [StringLength(CMessage = "品名", EMessage = "Commdity")]
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

        string _hscode;
        /// <summary>
        /// HSCode
        /// </summary>
        [StringLength(CMessage = "HSCode", EMessage = "HSCode")]
        public string HSCode
        {
            get
            {
                return _hscode;
            }
            set
            {
                if (_hscode != value)
                {
                    _hscode = value;
                    base.OnPropertyChanged("HSCode", value);
                }
            }
        }

        int _quantity;
        /// <summary>
        /// 数量
        /// </summary>
        [Required(CMessage = "数量", EMessage = "Quantity")]
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
        [GuidRequired(CMessage = "数量单位", EMessage = "QuantityUnit")]
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
        [Required(CMessage = "体积", EMessage = "Measurement")]
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

        string _dangerousclass;
        /// <summary>
        /// 危险品类型
        /// </summary>
        [StringLength(CMessage = "危险品类型", EMessage = "Dangerous Class")]
        public string DangerousClass
        {
            get
            {
                return _dangerousclass;
            }
            set
            {
                if (_dangerousclass != value)
                {
                    _dangerousclass = value;
                    base.OnPropertyChanged("DangerousClass", value);
                }
            }
        }

        string _dangerousno;
        /// <summary>
        /// 危险品编号
        /// </summary>
        [StringLength(CMessage = "危险品编号", EMessage = "Dangerous No")]
        public string DangerousNo
        {
            get
            {
                return _dangerousno;
            }
            set
            {
                if (_dangerousno != value)
                {
                    _dangerousno = value;
                    base.OnPropertyChanged("DangerousNo", value);
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
