using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.FCM.OtherBusiness.ServiceInterface.DataObjects
{
    #region  其他业务列表数据对象

    /// <summary>
    /// 其他业务列表数据对象
    /// </summary>
    [Serializable]
    public class OtherBusinessList : BaseDataObject
    {
        #region 编辑模式
        /// <summary>
        /// 从订舱单列表到编辑界面时
        /// 是新增、编辑还是复制的模式
        /// </summary>
        public EditMode EditMode { get; set; } 
        #endregion

        #region 业务联系人
        /// <summary>
        /// 业务联系人
        /// </summary>
        string _ContactName;
        public string ContactName
        {
            get { return _ContactName; }
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
            get { return _ContactID; }
            set
            {
                if (_ContactID != value)
                {
                    _ContactID = value;
                    base.OnPropertyChanged("ContactID", value);
                }
            }

        } 
        #endregion

        #region IsNew
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } } 
        #endregion

        #region 非空唯一键
        Guid _id;
        public Guid ID
        {
            get { return _id; }
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

        #region 公司口岸
        Guid companyID;
        /// <summary>
        /// 公司ID
        /// </summary>
        [GuidRequired(CMessage = "公司", EMessage = "Company")]
        public Guid CompanyID
        {
            get { return companyID; }
            set
            {
                if (companyID != value)
                {
                    companyID = value;
                    this.NotifyPropertyChanged(o => o.CompanyID);
                }
            }
        }
        string companyName;
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get { return companyName; }
            set
            {
                if (companyName != value)
                {
                    companyName = value;
                    this.NotifyPropertyChanged(o => o.CompanyName);
                }
            }
        }
        #endregion

        #region 单号
        string _no;

        public string NO
        {
            get { return _no; }
            set
            {
                if (_no != value)
                {
                    _no = value;
                    base.OnPropertyChanged("NO", value);
                }
            }
        }
        #endregion

        #region 订单状态（0待定、1已确认、2已接受、3已打回、4已取消、5已完成）
        OBOrderState _state;
        public OBOrderState State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    base.OnPropertyChanged("State", value);
                }
            }
        }
        #endregion

        #region  主提单号
        string _mblno;

        public string Mblno
        {
            get { return _mblno; }
            set
            {
                if (_mblno != value)
                {
                    _mblno = value;
                    base.OnPropertyChanged("Mblno", value);
                }
            }
        }
        #endregion

        #region 分提单号
        string _hblno;

        public string Hblno
        {
            get { return _hblno; }
            set
            {
                if (_hblno != value)
                {
                    _hblno = value;
                    base.OnPropertyChanged("Hblno", value);
                }
            }
        }
        #endregion

        #region 快递单号
        string expressno;
        /// <summary>
        /// 快递单号
        /// </summary>
        public string ExpressNo
        {
            get { return expressno; }
            set
            {
                if (expressno != value)
                {
                    expressno = value;
                    base.OnPropertyChanged("ExpressNo", value);
                };
            }
        }

        #endregion

        #region 客户ID
        Guid? _customerID;
        public Guid? CustomerID
        {
            get { return _customerID; }
            set
            {
                if (_customerID != value)
                {
                    _customerID = value;
                    base.OnPropertyChanged("CustomerID", value);
                }
            }
        }
        #endregion

        #region 客户
        string customerName;
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                if (customerName != value)
                {
                    customerName = value;
                    base.OnPropertyChanged("CustomerName", value);
                }
            }
        }
        #endregion

        #region 最终交货时间
        DateTime? _feta;
        public DateTime? Feta
        {
            get { return _feta; }
            set
            {
                if (_feta != value)
                {
                    _feta = value;
                    base.OnPropertyChanged("Feta", value);
                }
            }
        }
        #endregion

        #region 船名航次
        string vesselVoyage;

        public string VesselVoyage
        {
            get { return vesselVoyage; }
            set
            {
                if (vesselVoyage != value)
                {
                    vesselVoyage = value;
                    base.OnPropertyChanged("VesselVoyage", value);
                }
            }
        }
        #endregion

        #region 装货港
        string polName;
        /// <summary>
        /// 装货港
        /// </summary>
        public string PolName
        {
            get { return polName; }
            set
            {
                if (polName != value)
                {
                    polName = value;
                    base.OnPropertyChanged("PolName", value);
                }
            }
        }
        #endregion

        #region 装货港ID
        Guid? polID;
        /// <summary>
        /// 装货港ID
        /// </summary>
        //[GuidRequired(CMessage = "装货港", EMessage = "POL")]
        public Guid? PolID
        {
            get { return polID; }
            set
            {
                if (polID != value)
                {
                    polID = value;
                    base.OnPropertyChanged("PolID", value);
                }
            }
        }
        #endregion

        #region 收货地地址

        string _placeofreceiptaddress;
        /// <summary>
        /// 收货地地址
        /// </summary>

        public string PlaceOfReceiptAddress
        {
            get
            {
                return _placeofreceiptaddress;
            }
            set
            {
                if (_placeofreceiptaddress != value)
                {
                    _placeofreceiptaddress = value;
                    base.OnPropertyChanged("PlaceOfReceiptAddress", value);
                }
            }
        }

        #endregion

        #region 卸货港ID
        Guid? podID;
        //[GuidRequired(CMessage = "卸货港",EMessage="POD")]
        public Guid? PodID
        {
            get { return podID; }
            set
            {
                if (podID != value)
                {
                    podID = value;
                    base.OnPropertyChanged("PodID", value);
                }
            }
        }
        #endregion

        #region 卸货港
        string podName;
        public string PodName
        {
            get { return podName; }
            set
            {
                if (podName != value)
                {
                    podName = value;
                    base.OnPropertyChanged("PodName", value);
                }
            }
        }
        #endregion

        #region 交货地地址

        string _placeofdeliveryaddress;
        /// <summary>
        /// 交货地地址
        /// </summary>
        public string PlaceOfDeliveryAddress
        {
            get
            {
                return _placeofdeliveryaddress;
            }
            set
            {
                if (_placeofdeliveryaddress != value)
                {
                    _placeofdeliveryaddress = value;
                    base.OnPropertyChanged("PlaceOfDeliveryAddress", value);
                }
            }
        }

        #endregion

        #region etd
        DateTime? etd;

        public DateTime? Etd
        {
            get { return etd; }
            set
            {
                if (etd != value)
                {
                    etd = value;
                    base.OnPropertyChanged("Etd", value);
                }
            }
        }
        #endregion

        #region eta
        DateTime? eta;

        public DateTime? Eta
        {
            get { return eta; }
            set
            {
                if (eta != value)
                {
                    eta = value;
                    base.OnPropertyChanged("Eta", value);
                }
            }
        }
        #endregion

        #region 最终交货地ID
        Guid? finalDestinationID;
        //[GuidRequired(CMessage = "交货地", EMessage = "FinalDestination")]
        public Guid? FinalDestinationID
        {
            get { return finalDestinationID; }
            set
            {
                if (finalDestinationID != value)
                {
                    finalDestinationID = value;
                    base.OnPropertyChanged("FinalDestinationID", value);
                }
            }
        }
        #endregion

        #region 最终交货地
        string finalDestinationName;

        public string FinalDestinationName
        {
            get { return finalDestinationName; }
            set
            {
                if (finalDestinationName != value)
                {
                    finalDestinationName = value;
                    base.OnPropertyChanged("FinalDestinationName", value);
                }
            }
        }
        #endregion

        #region 订舱号
        string soNo;
        /// <summary>
        /// 订舱号
        /// </summary>
        public string SoNo
        {
            get { return soNo; }
            set
            {
                if (soNo != value)
                {
                    soNo = value;
                    this.NotifyPropertyChanged(o => SoNo);
                }
            }
        }
        #endregion

        #region 船东ID
        Guid? carrierID;
        /// <summary>
        /// 船东ID
        /// </summary>
        public Guid? CarrierID
        {
            get { return carrierID; }
            set
            {
                if (carrierID != value)
                {
                    carrierID = value;
                    base.OnPropertyChanged("CarrierID", value);
                }
            }
        }
        #endregion

        #region 船东
        string carrierName;
        /// <summary>
        /// 船东
        /// </summary>
        public string CarrierName
        {
            get { return carrierName; }
            set
            {
                if (carrierName != value)
                {
                    carrierName = value;
                    base.OnPropertyChanged("CarrierName", value);
                };
            }
        }
        #endregion

        #region 承运人ID
        /// <summary>
        /// 承运人ID
        /// </summary>
        Guid? agentofCarrierID;

        public Guid? AgentofCarrierID
        {
            get { return agentofCarrierID; }
            set
            {
                if (agentofCarrierID != value)
                {
                    agentofCarrierID = value;
                    base.OnPropertyChanged("AgentofCarrierID", value);
                };
            }
        }
        #endregion

        #region 承运人
        string agengofCarrierName;
        /// <summary>
        /// 承运人
        /// </summary>
        public string AgengofCarrierName
        {
            get { return agengofCarrierName; }
            set
            {
                if (agengofCarrierName != value)
                {
                    agengofCarrierName = value;
                    base.OnPropertyChanged("AgengofCarrierName", value);
                };
            }
        }
        #endregion

        #region 发货人ID
        /// <summary>
        /// 发货人ID
        /// </summary>
        Guid? shipperID;
        public Guid? ShipperID
        {
            get { return shipperID; }
            set
            {
                if (shipperID != value)
                {
                    shipperID = value;
                    base.OnPropertyChanged("ShipperID", value);
                };
            }
        } 
        #endregion

        #region 发货人
        string shipperName;
        /// <summary>
        /// 发货人
        /// </summary>
        public string ShipperName
        {
            get { return shipperName; }
            set
            {
                if (shipperName != value)
                {
                    shipperName = value;
                    base.OnPropertyChanged("ShipperName", value);
                };
            }
        }
        #endregion

        #region 收货人ID
        Guid? consigneeID;
        /// <summary>
        /// 收货人ID
        /// </summary>
        public Guid? ConsigneeID
        {
            get { return consigneeID; }
            set
            {
                if (consigneeID != value)
                {
                    consigneeID = value;
                    base.OnPropertyChanged("ConsigneeID", value);
                };
            }
        }
        #endregion

        #region 收货人
        string consigneeName;
        /// <summary>
        /// 收货人
        /// </summary>
        public string ConsigneeName
        {
            get { return consigneeName; }
            set
            {
                if (consigneeName != value)
                {
                    consigneeName = value;
                    base.OnPropertyChanged("ConsigneeName", value);
                };
            }
        }
        #endregion

        #region 通知人ID
        /// <summary>
        /// 通知人ID
        /// </summary>
        Guid? notifyPartyID;

        public Guid? NotifyPartyID
        {
            get { return notifyPartyID; }
            set
            {
                if (notifyPartyID != value)
                {
                    notifyPartyID = value;
                    base.OnPropertyChanged("NotifyPartyID", value);
                };
            }
        } 
        #endregion

        #region 通知人
        /// <summary>
        /// 通知人
        /// </summary>
        string notifyPartyName;

        public string NotifyPartyName
        {
            get { return notifyPartyName; }
            set
            {
                if (notifyPartyName != value)
                {
                    notifyPartyName = value;
                    base.OnPropertyChanged("NotifyPartyName", value);
                };
            }
        } 
        #endregion

        #region 包装
        /// <summary>
        /// 包装
        /// </summary>
        int quantity;

        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    base.OnPropertyChanged("Quantity", value);
                };
            }
        } 
        #endregion

        #region 包装单位ID
        /// <summary>
        /// 包装单位ID
        /// </summary>
        Guid? quantityUnitID;

        public Guid? QuantityUnitID
        {
            get { return quantityUnitID; }
            set
            {
                if (quantityUnitID != value)
                {
                    quantityUnitID = value;
                    base.OnPropertyChanged("QuantityUnitID", value);
                };
            }
        } 
        #endregion

        #region 包装单位
        /// <summary>
        /// 包装单位
        /// </summary>
        string quantityUnitName;

        public string QuantityUnitName
        {
            get { return quantityUnitName; }
            set
            {
                if (quantityUnitName != value)
                {
                    quantityUnitName = value;
                    base.OnPropertyChanged("QuantityUnitName", value);
                };
            }
        } 
        #endregion

        #region 重量
        /// <summary>
        /// 重量
        /// </summary>
        decimal weight;

        public decimal Weight
        {
            get { return weight; }
            set
            {
                if (weight != value)
                {
                    weight = value;
                    base.OnPropertyChanged("Weight", value);
                };
            }
        } 
        #endregion

        #region 重量单位ID
        /// <summary>
        /// 重量单位ID
        /// </summary>
        Guid? weightUnitID;

        public Guid? WeightUnitID
        {
            get { return weightUnitID; }
            set
            {
                if (weightUnitID != value)
                {
                    weightUnitID = value;
                    base.OnPropertyChanged("WeightUnitID", value);
                };
            }
        } 
        #endregion

        #region 重量单位
        /// <summary>
        /// 重量单位
        /// </summary>
        string weightUnitName;

        public string WeightUnitName
        {
            get { return weightUnitName; }
            set
            {
                if (weightUnitName != value)
                {
                    weightUnitName = value;
                    base.OnPropertyChanged("WeightUnitName", value);
                };
            }
        } 
        #endregion

        #region 体积
        /// <summary>
        /// 体积
        /// </summary>
        decimal measurement;

        public decimal Measurement
        {
            get { return measurement; }
            set
            {
                if (measurement != value)
                {
                    measurement = value;
                    base.OnPropertyChanged("Measurement", value);
                };
            }
        } 
        #endregion

        #region 体积单位ID
        /// <summary>
        /// 体积单位ID
        /// </summary>
        Guid? measurementUnitID;

        public Guid? MeasurementUnitID
        {
            get { return measurementUnitID; }
            set
            {
                if (measurementUnitID != value)
                {
                    measurementUnitID = value;
                    base.OnPropertyChanged("MeasurementUnitID", value);
                };
            }
        } 
        #endregion

        #region 体积单位
        /// <summary>
        /// 体积单位
        /// </summary>
        string measurementUnitName;

        public string MeasurementUnitName
        {
            get { return measurementUnitName; }
            set
            {
                if (measurementUnitName != value)
                {
                    measurementUnitName = value;
                    base.OnPropertyChanged("MeasurementUnitName", value);
                };
            }
        } 
        #endregion

        #region 揽货人
        /// <summary>
        /// 揽货人
        /// </summary>
        string salesName;

        public string SalesName
        {
            get { return salesName; }
            set
            {
                if (salesName != value)
                {
                    salesName = value;
                    base.OnPropertyChanged("SalesName", value);
                };
            }
        } 
        #endregion

        #region 揽货人ID
        /// <summary>
        /// 揽货人ID
        /// </summary>
        Guid? salesID;

        public Guid? SalesID
        {
            get { return salesID; }
            set
            {
                if (salesID != value)
                {
                    salesID = value;
                    base.OnPropertyChanged("SalesID", value);
                };
            }
        } 
        #endregion

        #region 更新人
        string _updateByName;
        public string UpdateByName
        {
            get { return _updateByName; }
            set
            {
                if (_updateByName != value)
                {
                    _updateByName = value;
                    base.OnPropertyChanged("UpdateByName", value);
                };
            }
        }

        #endregion

        #region 更新时间
        DateTime? updateDate;

        public DateTime? UpdateDate
        {
            get { return updateDate; }
            set
            {
                if (updateDate != value)
                {
                    updateDate = value;
                    base.OnPropertyChanged("UpdateDate", value);
                };
            }
        } 
        #endregion

        #region 创建时间
        DateTime? createDate;

        public DateTime? CreateDate
        {
            get { return createDate; }
            set
            {
                if (createDate != value)
                {
                    createDate = value;
                    base.OnPropertyChanged("CreateDate", value);
                };
            }
        } 
        #endregion

        #region 箱号
        string containerNo;

        public string ContainerNo
        {
            get { return containerNo; }
            set
            {
                if (containerNo != value)
                {
                    containerNo = value;
                    base.OnPropertyChanged("ContainerNo", value);
                };
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

        #region 是否放单
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
        #endregion
    }
    #endregion

    #region 其他业务详细信息
    /// <summary>
    /// 其他业务详细信息
    /// </summary>
    [Serializable]
    public partial class OtherBusinessInfo : OtherBusinessList
    {
        #region 揽货部门ID
        Guid salesDepartmentID;
        /// <summary>
        /// 揽货部门ID
        /// </summary>        
        public Guid SalesDepartmentID
        {
            get { return salesDepartmentID; }
            set
            {
                if (salesDepartmentID != value)
                {
                    salesDepartmentID = value;
                    this.NotifyPropertyChanged(o => o.SalesDepartmentID);
                }
            }
        } 
        #endregion

        #region 揽货部门名称
        string salesDepartmentName;
        /// <summary>
        /// 揽货部门名称
        /// </summary>
        public string SalesDepartmentName
        {
            get { return salesDepartmentName; }
            set
            {
                if (salesDepartmentName != value)
                {
                    salesDepartmentName = value;
                    this.NotifyPropertyChanged(o => o.SalesDepartmentName);
                }
            }
        } 
        #endregion

        Guid? operationID;
        /// <summary>
        /// ID
        /// </summary>
        public Guid? OperationID
        {
            get { return operationID; }
            set
            {
                if (operationID != value)
                {
                    operationID = value;
                    this.NotifyPropertyChanged(o => o.OperationID);
                }
            }
        }

        string operationNo;
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo
        {
            get { return operationNo; }
            set
            {
                if (operationNo != value)
                {
                    operationNo = value;
                    this.NotifyPropertyChanged(o => o.OperationNo);
                }
            }
        }

        /// <summary>
        /// 海外部客服
        /// </summary>
        string _overseasFilerName;

        public string OverseasFilerName
        {
            get { return _overseasFilerName; }
            set
            {
                if (_overseasFilerName != value)
                {
                    _overseasFilerName = value;
                    base.OnPropertyChanged("OverseasFilerName", value);
                };
            }
        }

        /// <summary>
        /// 海外部客服ID
        /// </summary>
        Guid? _overseasFilerID;

        public Guid? OverseasFilerID
        {
            get { return _overseasFilerID; }
            set
            {
                if (_overseasFilerID != value)
                {
                    _overseasFilerID = value;
                    base.OnPropertyChanged("OverseasFilerID", value);
                };
            }
        }

        /// <summary>
        /// 业务日期
        /// </summary>
        DateTime? operationDate;

        public DateTime? OperationDate
        {
            get { return operationDate; }
            set
            {
                if (operationDate != value)
                {
                    operationDate = value;
                    this.NotifyPropertyChanged(o => o.OperationDate);
                }
            }
        }
        Guid? paymentTypeID;
        /// <summary>
        /// 付款方式ID
        /// </summary>
        public Guid? PaymentTypeID
        {
            get { return paymentTypeID; }
            set
            {
                if (paymentTypeID != value)
                {
                    paymentTypeID = value;
                    this.NotifyPropertyChanged(o => o.PaymentTypeID);
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
        CustomerDescription _notifydescription;
        /// <summary>
        /// 通知人详细信息
        /// </summary>
        public CustomerDescription NotifyDescription
        {
            get
            {
                return _notifydescription;
            }
            set
            {
                if (_notifydescription != value)
                {
                    _notifydescription = value;
                    base.OnPropertyChanged("NotifyDescription", value);
                }
            }
        }
        string paymentTypeName;
        /// <summary>
        /// 付款方式
        /// </summary>
        public string PaymentTypeName
        {
            get { return paymentTypeName; }
            set
            {
                if (paymentTypeName != value)
                {
                    paymentTypeName = value;
                    this.NotifyPropertyChanged(o => o.PaymentTypeName);
                }
            }
        }

        string commodity;
        /// <summary>
        /// 品名
        /// </summary>
        [StringLength(MaximumLength = 400, CMessage = "品名", EMessage = "Commdity")]

        public string Commodity
        {
            get
            {
                return commodity;
            }
            set
            {
                if (commodity != value)
                {
                    commodity = value;
                    this.NotifyPropertyChanged(o => o.Commodity);
                }
            }
        }
        string remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set
            {
                if (remark != value)
                {
                    remark = value;
                    this.NotifyPropertyChanged(o => o.Remark);
                }
            }
        }
        Guid? createByID;
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid? CreateByID
        {
            get { return createByID; }
            set
            {
                if (createByID != value)
                {
                    createByID = value;
                    this.NotifyPropertyChanged(o => o.CreateByID);
                }
            }
        }

        string createByName;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateByName
        {
            get { return createByName; }
            set
            {
                if (createByName != value)
                {
                    createByName = value;
                    this.NotifyPropertyChanged(o => o.CreateByName);
                }
            }
        }
        string contractNo;
        /// <summary>
        /// 合约号
        /// </summary>
        public string ContractNo
        {
            get { return contractNo; }
            set
            {
                if (contractNo != value)
                {
                    contractNo = value;
                    this.NotifyPropertyChanged(o => o.ContractNo);
                }
            }
        }

        string operatorName;
        /// <summary>
        /// 操作
        /// </summary>
        public string OperatorName
        {
            get { return operatorName; }
            set
            {
                if (operatorName != value)
                {
                    operatorName = value;
                    this.NotifyPropertyChanged(o => o.OperatorName);
                }
            }
        }
        Guid? operatorID;

        public Guid? OperatorID
        {
            get { return operatorID; }
            set
            {
                if (operatorID != value)
                {
                    operatorID = value;
                    this.NotifyPropertyChanged(o => o.OperatorID);
                }
            }
        }

        Guid? voyageID;
        /// <summary>
        /// 船名航次ID
        /// </summary>
        public Guid? VoyageID
        {
            get { return voyageID; }
            set
            {
                if (voyageID != value)
                {
                    voyageID = value;
                    this.NotifyPropertyChanged(o => o.VoyageID);
                }
            }
        }

        OtOperationType otOperationType;
        /// <summary>
        /// 业务类型（0报关业务、1拖车、2订舱、3其他业务）
        /// </summary>
        public OtOperationType OtOperationType
        {
            get { return otOperationType; }
            set
            {
                if (otOperationType != value)
                {
                    otOperationType = value;
                    this.NotifyPropertyChanged(o => o.OtOperationType);
                }
            }
        }


        Guid? deliveryAtID;
        /// <summary>
        /// 交货地ID
        /// </summary>
        public Guid? DeliveryAtID
        {
            get { return deliveryAtID; }
            set
            {
                if (deliveryAtID != value)
                {
                    deliveryAtID = value;
                    this.NotifyPropertyChanged(o => DeliveryAtID);
                }
            }
        }
        string deliveryAtName;
        /// <summary>
        /// 交货地ID
        /// </summary>
        public string DeliveryAtName
        {
            get { return deliveryAtName; }
            set
            {
                if (deliveryAtName != value)
                {
                    deliveryAtName = value;
                    this.NotifyPropertyChanged(o => DeliveryAtName);
                }
            }
        }

        #region 本地服务

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


        bool _iswarehouse;
        /// <summary>
        /// 是否仓储
        /// </summary>
        public bool IsWareHouse
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
                    base.OnPropertyChanged("IsWareHouse", value);
                }
            }
        }


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


        bool _isquarantineinspection;
        /// <summary>
        /// 是否动植检
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
        Guid? customsBrokerID;
        /// <summary>
        /// 报关行ID
        /// </summary>
        public Guid? CustomsBrokerID
        {
            get { return customsBrokerID; }
            set
            {
                if (customsBrokerID != value)
                {
                    customsBrokerID = value;
                    this.NotifyPropertyChanged(o => CustomsBrokerID);
                }
            }
        }

        string customsBrokerName;
        /// <summary>
        /// 报关行描述
        /// </summary>
        public string CustomsBrokerName
        {
            get { return customsBrokerName; }
            set
            {
                if (customsBrokerName != value)
                {
                    customsBrokerName = value;
                    this.NotifyPropertyChanged(o => CustomsBrokerName);
                }
            }
        }

        Guid? warehouseID;
        /// <summary>
        /// 仓储ID
        /// </summary>
        public Guid? WarehouseID
        {
            get { return warehouseID; }
            set
            {
                if (warehouseID != value)
                {
                    warehouseID = value;
                    this.NotifyPropertyChanged(o => WarehouseID);
                }
            }
        }

        string warehouseName;
        /// <summary>
        /// 仓储描述
        /// </summary>
        public string WarehouseName
        {
            get { return warehouseName; }
            set
            {
                if (warehouseName != value)
                {
                    warehouseName = value;
                    this.NotifyPropertyChanged(o => WarehouseName);
                }
            }
        }
        #endregion

        #region CurrencyID
        Guid? _currencyid;
        /// <summary>
        /// 币种ID
        /// </summary>
        public Guid? CurrencyID
        {
            get { return _currencyid; }
            set
            {
                if (_currencyid != value)
                {
                    _currencyid = value;
                    this.NotifyPropertyChanged(o => o.CurrencyID);
                }
            }
        }
        #endregion

        #region CostAmount
        decimal _costamount;
        /// <summary>
        /// 金额
        /// </summary>
        public decimal CostAmount
        {
            get { return _costamount; }
            set
            {
                if (_costamount != value)
                {
                    _costamount = value;
                    this.NotifyPropertyChanged(o => o.CostAmount);
                }
            }
        }
        #endregion

        #region RevenueTon
        decimal _revenueton;
        /// <summary>
        /// 计费吨
        /// </summary>
        public decimal RevenueTon
        {
            get { return _revenueton; }
            set
            {
                if (_revenueton != value)
                {
                    _revenueton = value;
                    this.NotifyPropertyChanged(o => o.RevenueTon);
                }
            }
        }
        #endregion
    } 
    #endregion

    #region 箱信息
    /// <summary>
    /// 箱信息
    /// </summary>
    [Serializable]
    public partial class OBContainerList : BaseDataObject
    {
        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    this.NotifyPropertyChanged(o => ID);
                }
            }
        }
        string no;
        /// <summary>
        /// 箱号
        /// </summary>
        public string No
        {
            get { return no; }
            set
            {
                if (no != value)
                {
                    no = value;
                    this.NotifyPropertyChanged(o => No);
                }
            }
        }
        string sealNo;

        public string SealNo
        {
            get { return sealNo; }
            set
            {
                if (sealNo != value)
                {
                    sealNo = value;
                    this.NotifyPropertyChanged(o => SealNo);
                }
            }
        }
        string soNo;

        public string SoNo
        {
            get { return soNo; }
            set
            {
                if (soNo != value)
                {
                    soNo = value;
                    this.NotifyPropertyChanged(o => SoNo);
                }
            }
        }
        Guid? typeID;

        public Guid? TypeID
        {
            get { return typeID; }
            set
            {
                if (typeID != value)
                {
                    typeID = value;
                    this.NotifyPropertyChanged(o => TypeID);
                }
            }
        }

        string typeName;

        public string TypeName
        {
            get { return typeName; }
            set
            {
                if (typeName != value)
                {
                    typeName = value;
                    this.NotifyPropertyChanged(o => TypeName);
                }
            }
        }
        int quantity;

        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    this.NotifyPropertyChanged(o => Quantity);
                };
            }
        }
        string quantityName;

        public string QuantityName
        {
            get { return quantityName; }
            set
            {
                if (quantityName != value)
                {
                    quantityName = value;
                    this.NotifyPropertyChanged(o => QuantityName);
                };
            }
        }
        decimal weight;

        public decimal Weight
        {
            get { return weight; }
            set
            {
                if (weight != value)
                {
                    weight = value;
                    this.NotifyPropertyChanged(o => Weight);
                };
            }
        }
        string commodity;

        public string Commodity
        {
            get { return commodity; }
            set
            {
                if (commodity != value)
                {
                    commodity = value;
                    this.NotifyPropertyChanged(o => Commodity);
                };
            }
        }
        Guid? quantityUnitID;

        public Guid? QuantityUnitID
        {
            get { return quantityUnitID; }
            set
            {
                if (quantityUnitID != value)
                {
                    quantityUnitID = value;
                    this.NotifyPropertyChanged(o => QuantityUnitID);
                };
            }
        }
        Guid? weightUnitID;

        public Guid? WeightUnitID
        {
            get { return weightUnitID; }
            set
            {
                if (weightUnitID != value)
                {
                    weightUnitID = value;
                    this.NotifyPropertyChanged(o => WeightUnitID);
                }; ;
            }
        }
        Guid? measurementUnitID;

        public Guid? MeasurementUnitID
        {
            get { return measurementUnitID; }
            set
            {
                if (measurementUnitID != value)
                {
                    measurementUnitID = value;
                    this.NotifyPropertyChanged(o => MeasurementUnitID);
                };
            }
        }
        /// <summary>
        /// 体积
        /// </summary>
        decimal measurement;

        public decimal Measurement
        {
            get { return measurement; }
            set
            {
                if (measurement != value)
                {
                    measurement = value;
                    base.OnPropertyChanged("Measurement", value);
                };
            }
        }

        Guid? createByID;

        public Guid? CreateByID
        {
            get { return createByID; }
            set
            {
                if (createByID != value)
                {
                    createByID = value;
                    base.OnPropertyChanged("CreateByID", value);
                };
            }
        }

        string createByName;

        public string CreateByName
        {
            get { return createByName; }
            set
            {
                if (createByName != value)
                {
                    createByName = value;
                    base.OnPropertyChanged("CreateByName", value);
                };
            }
        }

        string updateByName;

        public string UpdateByName
        {
            get { return updateByName; }
            set
            {
                if (updateByName != value)
                {
                    updateByName = value;
                    base.OnPropertyChanged("UpdateByName", value);
                };
            }
        }

        Guid? updateByID;

        public Guid? UpdateByID
        {
            get { return updateByID; }
            set
            {
                if (updateByID != value)
                {
                    updateByID = value;
                    base.OnPropertyChanged("UpdateByID", value);
                };
            }
        }

        DateTime? createDate;

        public DateTime? CreateDate
        {
            get { return createDate; }
            set
            {
                if (createDate != value)
                {
                    createDate = value;
                    base.OnPropertyChanged("CreateDate", value);
                };
            }
        }

        DateTime? updateDate;

        public DateTime? UpdateDate
        {
            get { return updateDate; }
            set
            {
                if (updateDate != value)
                {
                    updateDate = value;
                    base.OnPropertyChanged("UpdateDate", value);
                };
            }
        }

    } 
    #endregion

    #region 费用信息
    /// <summary>
    /// 费用信息
    /// </summary>
    [Serializable]
    public partial class OBFeeList : BaseDataObject
    {

        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

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


        Guid _oceanbookingid;
        /// <summary>
        /// 订舱ID
        /// </summary>
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


        Guid _customerid;
        /// <summary>
        /// 客户
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


        string _customername;
        /// <summary>
        /// 客户名
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
        /// 费用代码
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
}