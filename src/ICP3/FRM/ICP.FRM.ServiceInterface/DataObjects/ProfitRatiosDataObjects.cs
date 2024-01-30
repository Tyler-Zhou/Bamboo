using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using System;
using System.Collections.Generic;

namespace ICP.FRM.ServiceInterface.DataObjects
{
    /// <summary>
    /// 业务统计列表
    /// </summary>
    [Serializable]
    public class BusinessStatisticsList : BaseDataObject
    {
        #region 操作口岸

        Guid _ComapnyID;
        /// <summary>
        /// 操作口岸ID
        /// </summary>
        [GuidRequired(CMessage = "操作口岸 ID", EMessage = "Company ID")]
        public Guid CompanyID
        {
            get
            {
                return _ComapnyID;
            }
            set
            {
                if (_ComapnyID != value)
                {
                    _ComapnyID = value;
                    base.OnPropertyChanged("CompanyID", value);
                }
            }
        }
        string _ComapnyName;
        /// <summary>
        /// 操作口岸
        /// </summary>
        [ViewDescription(CDescription = "操作口岸", EDescription ="Company")]
        public string CompanyName
        {
            get
            {
                return _ComapnyName;
            }
            set
            {
                if (_ComapnyName != value)
                {
                    _ComapnyName = value;
                    base.OnPropertyChanged("CompanyName", value);
                }
            }
        }

        #endregion

        #region 合约

        Guid _ContractID;
        /// <summary>
        /// 合约ID
        /// </summary>
        [GuidRequired(CMessage = "合约ID", EMessage = "ContractID")]
        public Guid ContractID
        {
            get
            {
                return _ContractID;
            }
            set
            {
                if (_ContractID != value)
                {
                    _ContractID = value;
                    base.OnPropertyChanged("ContractID", value);
                }
            }
        }
        string _ContractNo;
        /// <summary>
        /// 合约号
        /// </summary>
        [ViewDescription(CDescription = "合约号", EDescription = "Contract No")]
        public string ContractNo
        {
            get
            {
                return _ContractNo;
            }
            set
            {
                if (_ContractNo != value)
                {
                    _ContractNo = value;
                    base.OnPropertyChanged("ContractNo", value);
                }
            }
        }


        Guid _ContractBaseItemID;
        /// <summary>
        /// 合约基本港运价ID
        /// </summary>
        [GuidRequired(CMessage = "合约基本港运价ID", EMessage = "ContractBaseItemID")]
        public Guid ContractBaseItemID
        {
            get
            {
                return _ContractBaseItemID;
            }
            set
            {
                if (_ContractBaseItemID != value)
                {
                    _ContractBaseItemID = value;
                    base.OnPropertyChanged("ContractBaseItemID", value);
                }
            }
        }
        #endregion

        #region 船东
        string _CarrierName;
        /// <summary>
        /// 船东
        /// </summary>
        [ViewDescription(CDescription = "船东", EDescription = "Carrier Name")]
        public string CarrierName
        {
            get
            {
                return _CarrierName;
            }
            set
            {
                if (_CarrierName != value)
                {
                    _CarrierName = value;
                    base.OnPropertyChanged("CarrierName", value);
                }
            }
        }
        #endregion

        #region 船名/航次

        Guid _VesselID;
        /// <summary>
        /// 船名ID
        /// </summary>
        [GuidRequired(CMessage = "船名ID", EMessage = "VesselID")]
        public Guid VesselID
        {
            get
            {
                return _VesselID;
            }
            set
            {
                if (_VesselID != value)
                {
                    _VesselID = value;
                    base.OnPropertyChanged("VesselID", value);
                }
            }
        }
        string _VesselName;
        /// <summary>
        /// 船名
        /// </summary>
        [ViewDescription(CDescription = "船名", EDescription = "Vessel Name")]
        public string VesselName
        {
            get
            {
                return _VesselName;
            }
            set
            {
                if (_VesselName != value)
                {
                    _VesselName = value;
                    base.OnPropertyChanged("VesselName", value);
                }
            }
        }

        string _VoyageName;
        /// <summary>
        /// 航次
        /// </summary>
        [ViewDescription(CDescription = "航次", EDescription = "Voyage Name")]
        public string VoyageName
        {
            get
            {
                return _VoyageName;
            }
            set
            {
                if (_VoyageName != value)
                {
                    _VoyageName = value;
                    base.OnPropertyChanged("VoyageName", value);
                }
            }
        }
        #endregion

        #region 箱

        string _ContainerDescription;
        /// <summary>
        /// 箱描述
        /// </summary>
        public string ContainerDescription
        {
            get
            {
                return _ContainerDescription;
            }
            set
            {
                if (_ContainerDescription != value)
                {
                    _ContainerDescription = value;
                    base.OnPropertyChanged("ContainerDescription", value);
                }
            }
        }

        IDictionary<string, string> _dicContainer;
        IDictionary<string, string> dicContainer
        {
            get
            {
                if (_dicContainer == null || _dicContainer.Count <= 0)
                    _dicContainer = ContainerDescription.MatchByRegex(@"(?<ContainerType>[\da-zA-Z]{1,})[\s]*[\/][\s]*(?<ContainerRate>[\d\.]{1,})[\s]*[\*][\s]*(?<ContainerVolume>[\d]{1,})");
                return _dicContainer;
            }
        }

        /// <summary>
        /// 箱型
        /// </summary>
        [ViewDescription(CDescription = "箱型", EDescription = "Container Type")]
        public string ContainerTypeName
        {
            get
            {
                if(!ContainerDescription.Contains(";"))
                {
                    return dicContainer["ContainerType"];
                }
                return "";
            }
        }

        /// <summary>
        /// 单箱型运价
        /// </summary>
        [ViewDescription(CDescription = "运价", EDescription = "Container Rate")]
        public decimal ContainerRate
        {
            get
            {
                if (!ContainerDescription.Contains(";"))
                {
                    return Convert.ToDecimal(dicContainer["ContainerRate"]);
                }
                return 0;
            }
        }
        /// <summary>
        /// 箱量
        /// </summary>
        [ViewDescription(CDescription = "箱量", EDescription = "Container Volume")]
        public int ContainerVolume
        {
            get
            {
                if (!ContainerDescription.Contains(";"))
                {
                    return Convert.ToInt32(dicContainer["ContainerVolume"]);
                }
                return 0;
            }
        }
        public 

        #endregion

        #region 业务信息
        Guid _OperationID;
        /// <summary>
        /// 业务ID
        /// </summary>
        [GuidRequired(CMessage = "业务ID", EMessage = "OperationID")]
        public Guid OperationID
        {
            get
            {
                return _OperationID;
            }
            set
            {
                if (_OperationID != value)
                {
                    _OperationID = value;
                    base.OnPropertyChanged("OperationID", value);
                }
            }
        }
        string _OperationNo;
        /// <summary>
        /// 业务号
        /// </summary>
        [ViewDescription(CDescription = "业务号", EDescription = "Operation No")]
        public string OperationNo
        {
            get
            {
                return _OperationNo;
            }
            set
            {
                if (_OperationNo != value)
                {
                    _OperationNo = value;
                    base.OnPropertyChanged("OperationNo", value);
                }
            }
        }


        DateTime _OperationDate;
        /// <summary>
        /// Gate In Date/ETD
        /// </summary>
        [ViewDescription(CDescription = "Gate In Date/ETD", EDescription = "Gate In Date/ETD")]
        public DateTime OperationDate
        {
            get
            {
                return _OperationDate;
            }
            set
            {
                if (_OperationDate != value)
                {
                    _OperationDate = value;
                    base.OnPropertyChanged("OperationDate", value);
                }
            }
        }

        string _BookingNo;
        /// <summary>
        /// SO No
        /// </summary>
        [ViewDescription(CDescription = "订舱号", EDescription = "Booking No")]
        public string BookingNo
        {
            get
            {
                return _BookingNo;
            }
            set
            {
                if (_BookingNo != value)
                {
                    _BookingNo = value;
                    base.OnPropertyChanged("BookingNo", value);
                }
            }
        }
        #endregion

        #region POL Name

        string _POLName;
        /// <summary>
        /// POL Name
        /// </summary>
        [ViewDescription(CDescription = "装货港", EDescription = "POL")]
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
                    base.OnPropertyChanged("POLName", value);
                }
            }
        }

        #endregion

        #region POD Name

        string _PODName;
        /// <summary>
        /// POD Name
        /// </summary>
        [ViewDescription(CDescription = "卸货港", EDescription = "POD")]
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
                    base.OnPropertyChanged("PODName", value);
                }
            }
        }

        #endregion

        #region Place Of Delivery Name

        string _PlaceOfDeliveryName;
        /// <summary>
        /// Place Of Delivery Name
        /// </summary>
        [ViewDescription(CDescription = "交货地", EDescription = "Place Of Delivery")]
        public string PlaceOfDeliveryName
        {
            get
            {
                return _PlaceOfDeliveryName;
            }
            set
            {
                if (_PlaceOfDeliveryName != value)
                {
                    _PlaceOfDeliveryName = value;
                    base.OnPropertyChanged("PlaceOfDeliveryName", value);
                }
            }
        }

        #endregion

        #region 调整金额
        decimal _AdjustmentAmount;
        /// <summary>
        /// 调整金额
        /// </summary>
        [ViewDescription(CDescription = "调整金额(USD)", EDescription = "Adjustment Amount(USD)")]
        public decimal AdjustmentAmount
        {
            get
            {
                return _AdjustmentAmount;
            }
            set
            {
                if (_AdjustmentAmount != value)
                {
                    _AdjustmentAmount = value;
                    base.OnPropertyChanged("AdjustmentAmount", value);
                }
            }
        }
        #endregion

    }
    /// <summary>
    /// 利润调整
    /// </summary>
    [Serializable]
    public class ProfitRatiosAdjustment : BaseDataObject
    {
        #region 合约

        Guid _ContractBaseItemID;
        /// <summary>
        /// 合约基本港运价ID
        /// </summary>
        [GuidRequired(CMessage = "合约基本港运价ID", EMessage = "ContractBaseItemID")]
        public Guid ContractBaseItemID
        {
            get
            {
                return _ContractBaseItemID;
            }
            set
            {
                if (_ContractBaseItemID != value)
                {
                    _ContractBaseItemID = value;
                    base.OnPropertyChanged("ContractBaseItemID", value);
                }
            }
        }
        #endregion

        #region 箱型

        Guid _ContainerTypeID;
        /// <summary>
        /// 箱型ID
        /// </summary>
        [GuidRequired(CMessage = "箱型ID", EMessage = "Container Type ID")]
        public Guid ContainerTypeID
        {
            get
            {
                return _ContainerTypeID;
            }
            set
            {
                if (_ContainerTypeID != value)
                {
                    _ContainerTypeID = value;
                    base.OnPropertyChanged("ContainerTypeID", value);
                }
            }
        }
        string _ContainerTypeName;
        /// <summary>
        /// 箱型名称
        /// </summary>
        public string ContainerTypeName
        {
            get
            {
                return _ContainerTypeName;
            }
            set
            {
                if (_ContainerTypeName != value)
                {
                    _ContainerTypeName = value;
                    base.OnPropertyChanged("ContainerTypeName", value);
                }
            }
        }

        #endregion

        #region 原始金额

        decimal _OriginalAmount;
        /// <summary>
        /// 原始金额
        /// </summary>
        public decimal OriginalAmount
        {
            get
            {
                return _OriginalAmount;
            }
            set
            {
                if (_OriginalAmount != value)
                {
                    _OriginalAmount = value;
                    base.OnPropertyChanged("OriginalAmount", value);
                }
            }
        }

        #endregion

        #region 调整金额

        decimal _Amount;
        /// <summary>
        /// 调整金额
        /// </summary>
        public decimal Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    base.OnPropertyChanged("Amount", value);
                }
            }
        }

        #endregion

        #region 调整后金额

        decimal _AdjustmentAfterAmount;
        /// <summary>
        /// 调整后金额
        /// </summary>
        public decimal AdjustmentAfterAmount
        {
            get
            {
                return _AdjustmentAfterAmount;
            }
            set
            {
                if (_AdjustmentAfterAmount != value)
                {
                    _AdjustmentAfterAmount = value;
                    base.OnPropertyChanged("AdjustmentAfterAmount", value);
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 查询参数利润配比
    /// </summary>
    [Serializable]
    public class QueryCriteria4ProfitRatios
    {
        private Guid[] _CompanyIDs = new Guid[] { };
        /// <summary>
        /// 口岸ID
        /// </summary>
        public Guid[] CompanyIDs { get { return _CompanyIDs; } set { _CompanyIDs = value; } }

        private Guid[] _CarrierIDs = new Guid[] { };
        /// <summary>
        /// 船东ID
        /// </summary>
        public Guid[] CarrierIDs { get { return _CarrierIDs; } set { _CarrierIDs = value; } }

        private Guid[] _VesselIDs = new Guid[] { };
        /// <summary>
        /// 船名ID
        /// </summary>
        public Guid[] VesselIDs { get { return _VesselIDs; } set { _VesselIDs = value; } }

        private Guid[] _ShippingLineIDs = new Guid[] { };
        /// <summary>
        /// 航线ID
        /// </summary>
        public Guid[] ShippingLineIDs { get { return _ShippingLineIDs; } set { _ShippingLineIDs = value; } }

        private Guid[] _POLIDs = new Guid[] { };
        /// <summary>
        /// 装货港
        /// </summary>
        public Guid[] POLIDs { get { return _POLIDs; } set { _POLIDs = value; } }


        private Guid[] _PODIDs = new Guid[] { };
        /// <summary>
        /// 卸货港
        /// </summary>
        public Guid[] PODIDs { get { return _PODIDs; } set { _PODIDs = value; } }

        private Guid[] _PlaceOfDeliveryIDs = new Guid[] { };
        /// <summary>
        /// 交货地
        /// </summary>
        public Guid[] PlaceOfDeliveryIDs { get { return _PlaceOfDeliveryIDs; } set { _PlaceOfDeliveryIDs = value; } }
        /// <summary>
        /// 合约号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 订舱号
        /// </summary>
        public string BookingNo { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo { get; set; }
        /// <summary>
        /// 非录入合约号
        /// </summary>
        public bool IsNonContractNo { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 查询参数利润配比调整
    /// </summary>
    [Serializable]
    public class QueryCriteria4Adjustment
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }
        /// <summary>
        /// 合约ID
        /// </summary>
        public Guid ContractBaseItemID { get; set; }
    }
}
