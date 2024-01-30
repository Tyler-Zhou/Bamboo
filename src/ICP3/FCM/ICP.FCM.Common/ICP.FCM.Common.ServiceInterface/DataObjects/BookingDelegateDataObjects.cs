#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/6/22 星期五 14:33:56
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    #region CSP舱单委托列表显示对象
    /// <summary>
    /// CSP舱单委托
    /// </summary>
    [Serializable]
    public class BookingDelegateList : BaseDataObject
    {
        #region 舱单映射ID
        int _BookingMapID;
        /// <summary>
        /// 舱单映射ID
        /// </summary>
        public int BookingMapID
        {
            get
            {
                return _BookingMapID;
            }
            set
            {
                if (_BookingMapID != value)
                {
                    _BookingMapID = value;
                    OnPropertyChanged("BookingMapID", value);
                }
            }
        }
        #endregion

        #region 舱单号
        /// <summary>
        /// 舱单号
        /// </summary>
        public string BookingNo { get; set; }
        #endregion

        #region 舱单名称
        string _BookingName;
        /// <summary>
        /// 舱单名称
        /// </summary>
        public string BookingName
        {
            get
            {
                return _BookingName;
            }
            set
            {
                if (_BookingName != value)
                {
                    _BookingName = value;
                    OnPropertyChanged("BookingName", value);
                }
            }
        }
        #endregion

        #region 运输方式
        private CSP_FREIGHTMETHODTYPE _FreightMethodType;
        /// <summary>
        /// 运输方式
        /// </summary>
        public CSP_FREIGHTMETHODTYPE FreightMethodType
        {
            get
            {
                return _FreightMethodType;
            }
            set
            {
                if (_FreightMethodType != value)
                {
                    _FreightMethodType = value;
                    OnPropertyChanged("FreightMethodType", value);
                }
            }
        }
        /// <summary>
        /// 运输方式名称
        /// </summary>
        public string FreightMethodTypeName
        {
            get
            {
                return EnumHelper.GetDescription(FreightMethodType, ApplicationContext.Current.IsEnglish);
            }
        }
        #endregion

        #region FBA运输方式
        private CSP_FBAFREIGHTMETHODTYPE _FBAFreightMethodType;
        /// <summary>
        /// FBA运输方式
        /// </summary>
        public CSP_FBAFREIGHTMETHODTYPE FBAFreightMethodType
        {
            get
            {
                return _FBAFreightMethodType;
            }
            set
            {
                if (_FBAFreightMethodType != value)
                {
                    _FBAFreightMethodType = value;
                    OnPropertyChanged("FBAFreightMethodType", value);
                }
            }
        }
        /// <summary>
        /// FBA运输方式名称
        /// </summary>
        public string FBAFreightMethodTypeName
        {
            get
            {
                return EnumHelper.GetDescription(FBAFreightMethodType, ApplicationContext.Current.IsEnglish);
            }
        }
        #endregion

        #region 派送方式
        private DELIVERYTYPE _DeliveryType;
        /// <summary>
        /// 派送方式
        /// </summary>
        public DELIVERYTYPE DeliveryType
        {
            get
            {
                return _DeliveryType;
            }
            set
            {
                if (_DeliveryType != value)
                {
                    _DeliveryType = value;
                    OnPropertyChanged("DeliveryType", value);
                }
            }
        }
        /// <summary>
        /// 派送方式名称
        /// </summary>
        public string DeliveryTypeName
        {
            get
            {
                return EnumHelper.GetDescription(DeliveryType, ApplicationContext.Current.IsEnglish);
            }
        }
        #endregion

        #region 运输类型
        private CSP_SHIPMENTTYPE _ShipmentType;
        /// <summary>
        /// 运输类型
        /// </summary>
        public CSP_SHIPMENTTYPE ShipmentType
        {
            get
            {
                return _ShipmentType;
            }
            set
            {
                if (_ShipmentType != value)
                {
                    _ShipmentType = value;
                    OnPropertyChanged("ShipmentType", value);
                }
            }
        }

        /// <summary>
        /// 运输类型名称
        /// </summary>
        public string ShipmentTypeName
        {
            get
            {
                return EnumHelper.GetDescription(ShipmentType, ApplicationContext.Current.IsEnglish);
            }
        }
        #endregion

        #region 贸易条款
        string _IncoTermName;
        /// <summary>
        /// 贸易条款
        /// </summary>
        public string IncoTermName
        {
            get
            {
                return _IncoTermName;
            }
            set
            {
                if (_IncoTermName != value)
                {
                    _IncoTermName = value;
                    OnPropertyChanged("IncoTermName", value);
                }
            }
        }
        #endregion

        #region 业务类型
        OperationType _OperationType;
        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperationType
        {
            get
            {
                return _OperationType;
            }
            set
            {
                if (_OperationType != value)
                {
                    _OperationType = value;
                    OnPropertyChanged("OperationType", value);
                }
            }
        }
        #endregion

        #region 委托时间
        DateTime _BookingDate;
        /// <summary>
        /// 委托时间
        /// </summary>
        public DateTime BookingDate
        {
            get
            {
                return _BookingDate;
            }
            set
            {
                if (_BookingDate != value)
                {
                    _BookingDate = value;
                    OnPropertyChanged("BookingDate", value);
                }
            }
        }
        #endregion

        #region 贸易类型
        CSP_TRADETYPE _TradeType;
        /// <summary>
        /// 贸易类型
        /// </summary>
        public CSP_TRADETYPE TradeType
        {
            get
            {
                return _TradeType;
            }
            set
            {
                if (_TradeType != value)
                {
                    _TradeType = value;
                    OnPropertyChanged("TradeType", value);
                }
            }
        }
        /// <summary>
        /// 贸易类型名称
        /// </summary>
        public string TradeTypeName
        {
            get
            {
                return EnumHelper.GetDescription(TradeType, ApplicationContext.Current.IsEnglish);
            }
        }
        #endregion

        #region 运输条款名称
        string _TransportClauseName;
        /// <summary>
        /// 运输条款名称
        /// </summary>
        public string TransportClauseName
        {
            get
            {
                return _TransportClauseName;
            }
            set
            {
                if (_TransportClauseName != value)
                {
                    _TransportClauseName = value;
                    OnPropertyChanged("TransportClauseName", value);
                }
            }
        }
        #endregion
        
        #region 是否需要拖车
        bool _IsTruck;
        /// <summary>
        /// 是否需要拖车
        /// </summary>
        public bool IsTruck
        {
            get
            {
                return _IsTruck;
            }
            set
            {
                if (_IsTruck != value)
                {
                    _IsTruck = value;
                    OnPropertyChanged("IsTruck", value);
                }
            }
        }
        #endregion

        #region 是否报关
        bool _IsDeclaration;
        /// <summary>
        /// 是否报关
        /// </summary>
        public bool IsDeclaration
        {
            get
            {
                return _IsDeclaration;
            }
            set
            {
                if (_IsDeclaration != value)
                {
                    _IsDeclaration = value;
                    OnPropertyChanged("IsDeclaration", value);
                }
            }
        }
        #endregion

        #region 是否保险
        bool _IsInsurance;
        /// <summary>
        /// 是否保险
        /// </summary>
        public bool IsInsurance
        {
            get
            {
                return _IsInsurance;
            }
            set
            {
                if (_IsInsurance != value)
                {
                    _IsInsurance = value;
                    OnPropertyChanged("IsInsurance", value);
                }
            }
        }
        #endregion

        #region ETD For POL
        DateTime? _ETDForPOL;
        /// <summary>
        /// ETD For POL
        /// </summary>
        public DateTime? ETDForPOL
        {
            get
            {
                return _ETDForPOL;
            }
            set
            {
                if (_ETDForPOL != value)
                {
                    _ETDForPOL = value;
                    OnPropertyChanged("ETDForPOL", value);
                }
            }
        }
        #endregion

        #region ETA For POD
        DateTime? _ETAForPOD;
        /// <summary>
        /// ETA For POD
        /// </summary>
        public DateTime? ETAForPOD
        {
            get
            {
                return _ETAForPOD;
            }
            set
            {
                if (_ETAForPOD != value)
                {
                    _ETAForPOD = value;
                    OnPropertyChanged("ETAForPOD", value);
                }
            }
        }
        #endregion

        #region 唛头
        string _Marks;
        /// <summary>
        /// 唛头
        /// </summary>
        public string Marks
        {
            get
            {
                return _Marks;
            }
            set
            {
                if (_Marks != value)
                {
                    _Marks = value;
                    OnPropertyChanged("Marks", value);
                }
            }
        }
        #endregion

        #region 品名
        string _Commodity;
        /// <summary>
        /// 品名
        /// </summary>
        public string Commodity
        {
            get
            {
                return _Commodity;
            }
            set
            {
                if (_Commodity != value)
                {
                    _Commodity = value;
                    OnPropertyChanged("Commodity", value);
                }
            }
        }
        #endregion

        #region 柜信息
        string _Containers;
        /// <summary>
        /// 柜信息
        /// </summary>
        public string Containers
        {
            get
            {
                return _Containers;
            }
            set
            {
                if (_Containers != value)
                {
                    _Containers = value;
                    OnPropertyChanged("Containers", value);
                }
            }
        }
        #endregion

        #region 柜信息
        /// <summary>
        /// 柜信息
        /// </summary>
        public ContainerDescription ContainerDescription
        {
            get
            {
                return new ContainerDescription(Containers);
            }
        }
        #endregion

        #region 客户
        string _CustomerName;
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get
            {
                return _CustomerName;
            }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    OnPropertyChanged("CustomerName", value);
                }
            }
        }
        #endregion

        #region 发货人
        string _ShipperName;
        /// <summary>
        /// 发货人名称
        /// </summary>
        public string ShipperName
        {
            get
            {
                return _ShipperName;
            }
            set
            {
                if (_ShipperName != value)
                {
                    _ShipperName = value;
                    OnPropertyChanged("ShipperName", value);
                }
            }
        }
        #endregion

        #region 收货人
        string _ConsigneeName;
        /// <summary>
        /// 收货人名称
        /// </summary>
        public string ConsigneeName
        {
            get
            {
                return _ConsigneeName;
            }
            set
            {
                if (_ConsigneeName != value)
                {
                    _ConsigneeName = value;
                    OnPropertyChanged("ConsigneeName", value);
                }
            }
        }
        #endregion

        #region 装货港
        string _POLName;
        /// <summary>
        /// 装货港名称
        /// </summary>
        public string POLName
        {
            get
            {
                return _POLName;
            }
            set
            {
                if (_POLName != value)
                {
                    _POLName = value;
                    OnPropertyChanged("POLName", value);
                }
            }
        }
        #endregion

        #region 装货港地址
        string _POLAddress;
        /// <summary>
        /// 装货港地址
        /// </summary>
        public string POLAddress
        {
            get
            {
                return _POLAddress;
            }
            set
            {
                if (_POLAddress != value)
                {
                    _POLAddress = value;
                    OnPropertyChanged("POLAddress", value);
                }
            }
        }

        #endregion

        #region 卸货港
        string _PODName;
        /// <summary>
        /// 卸货港名称
        /// </summary>
        public string PODName
        {
            get
            {
                return _PODName;
            }
            set
            {
                if (_PODName != value)
                {
                    _PODName = value;
                    OnPropertyChanged("PODName", value);
                }
            }
        }
        #endregion

        #region 卸货港地址
        string _PODAddress;
        /// <summary>
        /// 卸货港地址
        /// </summary>
        public string PODAddress
        {
            get
            {
                return _PODAddress;
            }
            set
            {
                if (_PODAddress != value)
                {
                    _PODAddress = value;
                    OnPropertyChanged("PODAddress", value);
                }
            }
        }

        #endregion

        #region 数量、数量单位
        int _Quantity;
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                if (_Quantity != value)
                {
                    _Quantity = value;
                    OnPropertyChanged("Quantity", value);
                }
            }
        }

        string _QuantityUnitName;
        /// <summary>
        /// 数量单位
        /// </summary>
        public string QuantityUnitName
        {
            get
            {
                return _QuantityUnitName;
            }
            set
            {
                if (_QuantityUnitName != value)
                {
                    _QuantityUnitName = value;
                    OnPropertyChanged("QuantityUnitName", value);
                }
            }
        }
        #endregion

        #region 重量、重量单位
        decimal _Weight;
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight
        {
            get
            {
                return _Weight;
            }
            set
            {
                if (_Weight != value)
                {
                    _Weight = value;
                    OnPropertyChanged("Weight", value);
                }
            }
        }
        string _WeightUnitName;
        /// <summary>
        /// 重量单位名称
        /// </summary>
        public string WeightUnitName
        {
            get
            {
                return _WeightUnitName;
            }
            set
            {
                if (_WeightUnitName != value)
                {
                    _WeightUnitName = value;
                    OnPropertyChanged("WeightUnitName", value);
                }
            }
        }
        #endregion

        #region 体积、体积单位
        decimal _Measurement;
        /// <summary>
        /// 体积
        /// </summary>
        public decimal Measurement
        {
            get
            {
                return _Measurement;
            }
            set
            {
                if (_Measurement != value)
                {
                    _Measurement = value;
                    OnPropertyChanged("Measurement", value);
                }
            }
        }

        string _MeasurementUnitName;
        /// <summary>
        /// 体积单位名称
        /// </summary>
        public string MeasurementUnitName
        {
            get
            {
                return _MeasurementUnitName;
            }
            set
            {
                if (_MeasurementUnitName != value)
                {
                    _MeasurementUnitName = value;
                    OnPropertyChanged("MeasurementUnitName", value);
                }
            }
        }

        #endregion

        #region 业务员名称
        string _SalesName;
        /// <summary>
        /// 业务员名称
        /// </summary>
        public string SalesName
        {
            get
            {
                return _SalesName;
            }
            set
            {
                if (_SalesName != value)
                {
                    _SalesName = value;
                    OnPropertyChanged("SalesName", value);
                }
            }
        }
        #endregion

        #region 建立人
        string _CreateByName;
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName
        {
            get
            {
                return _CreateByName;
            }
            set
            {
                if (_CreateByName != value)
                {
                    _CreateByName = value;
                    OnPropertyChanged("CreateByName", value);
                }
            }
        }
        #endregion

        #region 建立时间
        DateTime _CreateDate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                if (_CreateDate != value)
                {
                    _CreateDate = value;
                    OnPropertyChanged("CreateDate", value);
                }
            }
        }
        #endregion

        #region 更新时间
        DateTime? _UpdateDate;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _UpdateDate;
            }
            set
            {
                if (_UpdateDate != value)
                {
                    _UpdateDate = value;
                    OnPropertyChanged("UpdateDate", value);
                }
            }
        }
        #endregion
    }
    #endregion

    #region CSP舱单委托
    /// <summary>
    /// CSP舱单委托
    /// </summary>
    [Serializable]
    public class BookingDelegate : BookingDelegateList
    {
        #region 唯一键/ID

        Guid _ID;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged("ID", value);
                }
            }
        }

        #endregion

        #region 是否新增
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }
        #endregion

        #region 是否默认
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault
        {
            get;
            set;
        }
        #endregion

        #region 贸易条款
        Guid _IncoTermID;
        /// <summary>
        /// 贸易条款
        /// </summary>
        public Guid IncoTermID
        {
            get
            {
                return _IncoTermID;
            }
            set
            {
                if (_IncoTermID != value)
                {
                    _IncoTermID = value;
                    OnPropertyChanged("TradeTermID", value);
                }
            }
        }
        #endregion

        #region 运输条款ID
        Guid _TransportClauseID;
        /// <summary>
        /// 运输条款ID
        /// </summary>
        public Guid TransportClauseID
        {
            get
            {
                return _TransportClauseID;
            }
            set
            {
                if (_TransportClauseID != value)
                {
                    _TransportClauseID = value;
                    OnPropertyChanged("TransportClauseID", value);
                }
            }
        }
        #endregion

        #region 客户
        Guid _CustomerID;
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID
        {
            get
            {
                return _CustomerID;
            }
            set
            {
                if (_CustomerID != value)
                {
                    _CustomerID = value;
                    OnPropertyChanged("CustomerID", value);
                }
            }
        }
        CustomerDescription _CustomerDescription;
        /// <summary>
        /// 客户描述
        /// </summary>
        public CustomerDescription CustomerDescription
        {
            get
            {
                return _CustomerDescription;
            }
            set
            {
                if (_CustomerDescription != value)
                {
                    _CustomerDescription = value;
                    OnPropertyChanged("CustomerDescription", value);
                }
            }
        }
        #endregion

        #region 发货人
        Guid _ShipperID;
        /// <summary>
        /// 发货人ID
        /// </summary>
        public Guid ShipperID
        {
            get
            {
                return _ShipperID;
            }
            set
            {
                if (_ShipperID != value)
                {
                    _ShipperID = value;
                    OnPropertyChanged("ShipperID", value);
                }
            }
        }
        CustomerDescription _ShipperDescription;
        /// <summary>
        /// 发货人描述
        /// </summary>
        public CustomerDescription ShipperDescription
        {
            get
            {
                return _ShipperDescription;
            }
            set
            {
                if (_ShipperDescription != value)
                {
                    _ShipperDescription = value;
                    OnPropertyChanged("ShipperDescription", value);
                }
            }
        }
        #endregion

        #region 收货人
        Guid? _ConsigneeID;
        /// <summary>
        /// 收货人ID
        /// </summary>
        public Guid? ConsigneeID
        {
            get
            {
                return _ConsigneeID;
            }
            set
            {
                if (_ConsigneeID != value)
                {
                    _ConsigneeID = value;
                    OnPropertyChanged("ConsigneeID", value);
                }
            }
        }
        CustomerDescription _ConsigneeDescription;
        /// <summary>
        /// 收货人描述
        /// </summary>
        public CustomerDescription ConsigneeDescription
        {
            get
            {
                return _ConsigneeDescription;
            }
            set
            {
                if (_ConsigneeDescription != value)
                {
                    _ConsigneeDescription = value;
                    OnPropertyChanged("ConsigneeDescription", value);
                }
            }
        }
        #endregion

        #region 装货港
        Guid? _POLID;
        /// <summary>
        /// 装货港ID
        /// </summary>
        public Guid? POLID
        {
            get
            {
                return _POLID;
            }
            set
            {
                if (_POLID != value)
                {
                    _POLID = value;
                    OnPropertyChanged("POLID", value);
                }
            }
        }
        #endregion

        #region 装货港地址映射ID
        int _POLAddressMapID;
        /// <summary>
        /// 装货港地址映射ID
        /// </summary>
        public int POLAddressMapID
        {
            get
            {
                return _POLAddressMapID;
            }
            set
            {
                if (_POLAddressMapID != value)
                {
                    _POLAddressMapID = value;
                    OnPropertyChanged("POLAddressMapID", value);
                }
            }
        }
        #endregion

        #region 卸货港
        Guid? _PODID;
        /// <summary>
        /// 卸货港ID
        /// </summary>
        public Guid? PODID
        {
            get
            {
                return _PODID;
            }
            set
            {
                if (_PODID != value)
                {
                    _PODID = value;
                    OnPropertyChanged("PODID", value);
                }
            }
        }
        #endregion

        #region 卸货港地址映射ID
        int _PODAddressMapID;
        /// <summary>
        /// 卸货港地址映射ID
        /// </summary>
        public int PODAddressMapID
        {
            get
            {
                return _PODAddressMapID;
            }
            set
            {
                if (_PODAddressMapID != value)
                {
                    _PODAddressMapID = value;
                    OnPropertyChanged("PODAddressMapID", value);
                }
            }
        }
        #endregion

        #region 数量单位
        Guid? _QuantityUnitID;
        /// <summary>
        /// 数量单位ID
        /// </summary>
        public Guid? QuantityUnitID
        {
            get
            {
                return _QuantityUnitID;
            }
            set
            {
                if (_QuantityUnitID != value)
                {
                    _QuantityUnitID = value;
                    OnPropertyChanged("QuantityUnitID", value);
                }
            }
        }
        #endregion

        #region 重量单位ID
        Guid? _WeightUnitID;
        /// <summary>
        /// 重量单位ID
        /// </summary>
        public Guid? WeightUnitID
        {
            get
            {
                return _WeightUnitID;
            }
            set
            {
                if (_WeightUnitID != value)
                {
                    _WeightUnitID = value;
                    OnPropertyChanged("WeightUnitID", value);
                }
            }
        }

        #endregion

        #region 体积单位ID
        Guid? _MeasurementUnitID;
        /// <summary>
        /// 体积单位ID
        /// </summary>
        public Guid? MeasurementUnitID
        {
            get
            {
                return _MeasurementUnitID;
            }
            set
            {
                if (_MeasurementUnitID != value)
                {
                    _MeasurementUnitID = value;
                    OnPropertyChanged("MeasurementUnitID", value);
                }
            }
        }

        #endregion

        #region 业务员ID
        Guid _SalesID;
        /// <summary>
        /// 业务员ID
        /// </summary>
        public Guid SalesID
        {
            get
            {
                return _SalesID;
            }
            set
            {
                if (_SalesID != value)
                {
                    _SalesID = value;
                    OnPropertyChanged("SalesID", value);
                }
            }
        }
        #endregion

        #region 取消备注
        string _CancelRemark;
        /// <summary>
        /// 取消备注
        /// </summary>
        public string CancelRemark
        {
            get
            {
                return _CancelRemark;
            }
            set
            {
                if (_CancelRemark != value)
                {
                    _CancelRemark = value;
                    OnPropertyChanged("CancelRemark", value);
                }
            }
        }
        #endregion

        #region 是否移除
        /// <summary>
        /// 是否移除
        /// </summary>
        public bool IsRemove
        {
            get
            {
                return !string.IsNullOrEmpty(CancelRemark);
            }
        }
        #endregion

        #region 建立人
        Guid _CreateByID;
        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID
        {
            get
            {
                return _CreateByID;
            }
            set
            {
                if (_CreateByID != value)
                {
                    _CreateByID = value;
                    OnPropertyChanged("CreateByID", value);
                }
            }
        }
        #endregion

        #region 业务类型
        OperationType _OperationType;
        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperationType
        {
            get
            {
                return _OperationType;
            }
            set
            {
                if (_OperationType != value)
                {
                    _OperationType = value;
                    OnPropertyChanged("OperationType", value);
                }
            }
        }
        #endregion
        
    }
    #endregion
}
