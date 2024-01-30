#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/10 星期二 15:33:53
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion


namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    using Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;
    using System;
    using System.Collections.Generic;

    #region QuotedPriceList 报价集合信息

    /// <summary>
    /// 报价集合信息
    /// </summary>
    [Serializable]
    public partial class QuotedPriceOrderList : BaseDataObject
    {
        #region 编辑模式
        /// <summary>
        /// 从报价单列表到编辑界面时
        /// 是新增、编辑还是复制的模式
        /// </summary>
        public EditMode EditMode { get; set; }
        #endregion

        #region 唯一键/ID

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

        #region 报价单号

        string _no;
        /// <summary>
        /// 业务号
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
                    OnPropertyChanged("No", value);
                }
            }
        }

        #endregion

        #region 报价目标类型
        QPTargetType _qptargetType;
        /// <summary>
        /// 报价目标类型
        /// </summary>
        public QPTargetType TargetType
        {
            get
            {
                return _qptargetType;
            }
            set
            {
                if (_qptargetType != value)
                {
                    _qptargetType = value;
                    OnPropertyChanged("TargetType", value);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TargetTypeName {
            get
            {
                return EnumHelper.GetDescription(TargetType, ApplicationContext.Current.IsEnglish);
            }
        }
        #endregion

        #region 客户ID
        Guid? _customerid;
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid? CustomerID
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
                    OnPropertyChanged("CustomerID", value);
                }
            }
        }

        #endregion

        #region 客户名称
        string _customername;
        /// <summary>
        /// 客户名称
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
                    OnPropertyChanged("CustomerName", value);
                }
            }
        }

        #endregion

        #region 运输条款
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
                    OnPropertyChanged("TransportClauseName", value);
                }
            }
        }
        #endregion

        #region 品名

        string _commodity;
        /// <summary>
        /// 品名
        /// </summary>
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
                    OnPropertyChanged("Commodity", value);
                }
            }
        }

        #endregion

        #region 报价人

        private string _quoteByName;
        /// <summary>
        /// 报价人
        /// </summary>
        public string QuoteByName
        {
            get
            {
                return _quoteByName;
            }
            set
            {
                if (_quoteByName != value)
                {
                    _quoteByName = value;
                    OnPropertyChanged("QuoteName", value);
                }
            }
        }
        #endregion

        #region 确认报价人

        private string _confirmedName;
        /// <summary>
        /// 确认报价人
        /// </summary>
        public string ConfirmedName
        {
            get
            {
                return _confirmedName;
            }
            set
            {
                if (_confirmedName != value)
                {
                    _confirmedName = value;
                    OnPropertyChanged("ConfirmedName", value);
                }
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
                    OnPropertyChanged("IsValid", value);
                }
            }
        }

        #endregion

        #region 付款方式
        QPPaymentType _qppaymenttype;
        /// <summary>
        /// 付款方式
        /// </summary>
        public QPPaymentType PaymentType
        {
            get
            {
                return _qppaymenttype;
            }
            set
            {
                if (_qppaymenttype != value)
                {
                    _qppaymenttype = value;
                    OnPropertyChanged("PaymentType", value);
                }
            }
        }
        /// <summary>
        /// 付款方式名称
        /// </summary>
        public string PaymentTypeName
        {
            get
            {
                return EnumHelper.GetDescription(PaymentType, ApplicationContext.Current.IsEnglish);
            }
        }
        #endregion

        #region 报价开始时间
        /// <summary>
        /// 报价开始时间
        /// </summary>
        DateTime? _fromDate;
        /// <summary>
        /// 报价开始时间
        /// </summary>
        public DateTime? FromDate
        {
            get
            {
                return _fromDate;
            }
            set
            {
                if (_fromDate != value)
                {
                    _fromDate = value;
                    OnPropertyChanged("FromDate", value);
                }
            }
        }
        #endregion

        #region 报价结束时间
        /// <summary>
        /// 报价结束时间
        /// </summary>
        DateTime? _toDate;
        /// <summary>
        /// 报价开始时间
        /// </summary>
        public DateTime? ToDate
        {
            get
            {
                return _toDate;
            }
            set
            {
                if (_toDate != value)
                {
                    _toDate = value;
                    OnPropertyChanged("ToDate", value);
                }
            }
        }
        #endregion

        #region 创建人

        Guid _createbyid;
        /// <summary>
        /// 创建人
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
                    OnPropertyChanged("CreateByID", value);
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
                    OnPropertyChanged("CreateByName", value);
                }
            }
        }

        #endregion

        #region 创建时间

        DateTime _createdate;
        /// <summary>
        /// 创建时间
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
                    OnPropertyChanged("CreateDate", value);
                }
            }
        }

        #endregion

        #region 更新人
        string _updateByName;
        /// <summary>
        /// 更新人
        /// </summary>
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
                    OnPropertyChanged("UpdateByName", value);
                }
            }
        }

        #endregion

        #region 更新时间-做数据版本控制用

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
                    OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        #endregion
    }
    #endregion

    #region QuotedPriceInfo 报价详细信息

    /// <summary>
    /// 报价详细信息
    /// </summary>
    [Serializable]
    public partial class QuotedPriceOrderInfo : QuotedPriceOrderList
    {
        #region 客户详细信息
        CustomerDescription _customerdescription;
        /// <summary>
        /// 客户详细信息
        /// </summary>
        public CustomerDescription CustomerDescription
        {
            get
            {
                return _customerdescription;
            }
            set
            {
                if (_customerdescription != value)
                {
                    _customerdescription = value;
                    OnPropertyChanged("CustomerDescription", value);
                }
            }
        }
        #endregion

        #region 运输条款ID
        Guid _transportclauseid;
        /// <summary>
        /// 运输条款ID
        /// </summary>
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
                    OnPropertyChanged("TransportClauseID", value);
                }
            }
        }
        #endregion

        #region 报价人ID

        private Guid? _quoteBy;
        /// <summary>
        /// 报价人ID
        /// </summary>
        public Guid? QuoteBy
        {
            get
            {
                return _quoteBy;
            }
            set
            {
                if (_quoteBy != value)
                {
                    _quoteBy = value;
                    OnPropertyChanged("QuoteBy", value);
                }
            }
        }
        #endregion

        #region 报价Remark
        string _remark;
        /// <summary>
        /// 报价价格
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
                    OnPropertyChanged("Remark", value);
                }
            }
        }
        #endregion

        #region 确认报价人ID

        private Guid? _confirmedBy;
        /// <summary>
        /// 确认报价人ID
        /// </summary>
        public Guid? ConfirmedBy
        {
            get
            {
                return _confirmedBy;
            }
            set
            {
                if (_confirmedBy != value)
                {
                    _confirmedBy = value;
                    OnPropertyChanged("ConfirmedBy", value);
                }
            }
        }
        #endregion

        #region 报价价格列表
        List<QuotedPriceRatesList> _ratesList = new List<QuotedPriceRatesList>();
        /// <summary>
        /// 报价价格列表
        /// </summary>
        public List<QuotedPriceRatesList> RatesList
        {
            get
            {
                return _ratesList;
            }
            set
            {
                if (_ratesList != value)
                {
                    _ratesList = value;
                    OnPropertyChanged("RatesList", value);
                }
            }
        }
        #endregion
    }

    #endregion

    #region QuotedPricePartInfo 报价面板信息
    /// <summary>
    /// 报价面板信息
    /// </summary>
    [Serializable]
    public class QuotedPricePartInfo
    {
        /// <summary>
        /// 报价单ID
        /// </summary>
        public Guid? QuotedPriceID
        {
            get;
            set;
        }

        /// <summary>
        /// 报价单No
        /// </summary>
        public String QuotedPriceNo
        {
            get;
            set;
        }

        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid? CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 运输条款ID
        /// </summary>
        public Guid TransportClauseID
        {
            get;
            set;
        }

        /// <summary>
        /// 运输条款
        /// </summary>
        public string TransportClauseName
        {
            get;
            set;
        }
        /// <summary>
        /// 品名
        /// </summary>
        public string Commodity
        {
            get;
            set;
        }

        /// <summary>
        /// 装货港ID
        /// </summary>
        public Guid POLID
        {
            get;
            set;
        }

        /// <summary>
        /// 装货港
        /// </summary>
        public string POLName
        {
            get;
            set;
        }

        /// <summary>
        /// 卸货港ID
        /// </summary>
        public Guid PODID
        {
            get;
            set;
        }

        /// <summary>
        /// 卸货港
        /// </summary>
        public string PODName
        {
            get;
            set;
        }

        /// <summary>
        /// 收货地ID
        /// </summary>
        public Guid? PlaceOfReceiptID
        {
            get;
            set;
        }

        /// <summary>
        /// 收货地名称
        /// </summary>
        public string PlaceOfReceiptName
        {
            get;
            set;
        }

        /// <summary>
        /// 交货地ID
        /// </summary>
        public Guid PlaceOfDeliveryID
        {
            get;
            set;
        }

        /// <summary>
        /// 交货地
        /// </summary>
        public string PlaceOfDeliveryName
        {
            get;
            set;
        }

    }
    #endregion

    #region Quoted Price Rates 柜报价信息
    /// <summary>
    /// Quoted Price Rates
    /// </summary>
    [Serializable]
    public class QuotedPriceRatesList : BaseDataObject
    {
        #region 唯一键/ID

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
                    OnPropertyChanged("ID", value);
                }
            }
        }

        #endregion

        #region 报价单ID

        Guid _quotedpriceid;
        /// <summary>
        /// 报价单ID
        /// </summary>
        public Guid QuotedPriceID
        {
            get
            {
                return _quotedpriceid;
            }
            set
            {
                if (_quotedpriceid != value)
                {
                    _quotedpriceid = value;
                    OnPropertyChanged("QuotedPriceID", value);
                }
            }
        }

        #endregion

        #region 装货港ID

        Guid _polid;
        /// <summary>
        /// 装货港ID
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
                    OnPropertyChanged("POLID", value);
                }
            }
        }

        #endregion

        #region 装货港

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
                    OnPropertyChanged("POLName", value);
                }
            }
        }

        #endregion

        #region 卸货港ID

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
                    OnPropertyChanged("PODID", value);
                }
            }
        }

        #endregion

        #region 卸货港

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
                    OnPropertyChanged("PODName", value);
                }
            }
        }

        #endregion

        #region 收货地ID
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
                    OnPropertyChanged("PlaceOfReceiptID", value);
                }
            }
        }
        #endregion

        #region 收货地
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
                    OnPropertyChanged("PlaceOfReceiptName", value);
                }
            }
        }
        #endregion

        #region 交货地ID

        Guid _placeofdeliveryid;
        /// <summary>
        /// 交货地ID
        /// </summary>
        [GuidRequired(CMessage = "交货地", EMessage = "Place Of Delivery")]
        public Guid PlaceOfDeliveryID
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

                    this.NotifyPropertyChanged(p => p.PlaceOfDeliveryID);
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
                    OnPropertyChanged("PlaceOfDeliveryName", value);
                }
            }
        }

        #endregion

        #region 船公司集合

        string _carrier;
        /// <summary>
        /// 船公司集合
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "交货地", EMessage = "Place Of Delivery")]
        public string Carrier
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
                    OnPropertyChanged("Carrier", value);
                }
            }
        }

        #endregion

        #region TT

        short _tt;
        /// <summary>
        /// 交货地
        /// </summary>
        public short TT
        {
            get
            {
                return _tt;
            }
            set
            {
                if (_tt != value)
                {
                    _tt = value;
                    OnPropertyChanged("TT", value);
                }
            }
        }

        #endregion

        #region Unit20

        int _unit20;
        /// <summary>
        /// Unit20
        /// </summary>
        public int Unit20
        {
            get
            {
                return _unit20;
            }
            set
            {
                if (_unit20 != value)
                {
                    _unit20 = value;
                    OnPropertyChanged("Unit20", value);
                }
            }
        }

        #endregion

        #region Unit40

        int _unit40;
        /// <summary>
        /// Unit40
        /// </summary>
        public int Unit40
        {
            get
            {
                return _unit40;
            }
            set
            {
                if (_unit40 != value)
                {
                    _unit40 = value;
                    OnPropertyChanged("Unit40", value);
                }
            }
        }

        #endregion

        #region Unit40HQ

        int _unit40HQ;
        /// <summary>
        /// Unit40HQ
        /// </summary>
        public int Unit40HQ
        {
            get
            {
                return _unit40HQ;
            }
            set
            {
                if (_unit40HQ != value)
                {
                    _unit40HQ = value;
                    OnPropertyChanged("Unit40HQ", value);
                }
            }
        }

        #endregion

        #region Unit45

        int _unit45;
        /// <summary>
        /// Unit45
        /// </summary>
        public int Unit45
        {
            get
            {
                return _unit45;
            }
            set
            {
                if (_unit45 != value)
                {
                    _unit45 = value;
                    OnPropertyChanged("Unit45", value);
                }
            }
        }

        #endregion

        #region 附加费描述
        string _surchargedescription;
        /// <summary>
        /// 附加费描述
        /// </summary>
        public string SurchargeDescription
        {
            get
            {
                return _surchargedescription;
            }
            set
            {
                if (_surchargedescription != value)
                {
                    _surchargedescription = value;
                    OnPropertyChanged("SurchargeDescription", value);
                }
            }
        }
        /// <summary>
        /// 附加费明细
        /// </summary>
        public List<QPSurcharge> Surcharges
        {
            get
            {
                if(SurchargeDescription.IsNullOrEmpty())
                {
                    return new List<QPSurcharge>();
                }
                return JSONSerializerHelper.DeserializeFromJson<List<QPSurcharge>>(SurchargeDescription);
            }
            set
            {
                SurchargeDescription = JSONSerializerHelper.SerializeToJson(value);
            }
        }
        #endregion

        #region 创建人

        Guid _createbyid;
        /// <summary>
        /// 创建人
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
                    OnPropertyChanged("CreateByID", value);
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
                    OnPropertyChanged("CreateByName", value);
                }
            }
        }

        #endregion

        #region 创建时间

        DateTime _createdate;
        /// <summary>
        /// 创建时间
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
                    OnPropertyChanged("CreateDate", value);
                }
            }
        }

        #endregion

        #region 更新人
        string _updateByName;
        /// <summary>
        /// 更新人
        /// </summary>
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
                    OnPropertyChanged("UpdateByName", value);
                }
            }
        }

        #endregion

        #region 更新时间-做数据版本控制用

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
                    OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        #endregion
    }
    #endregion

    #region 报价-附加费信息
    /// <summary>
    /// Quoted Price Surcharge
    /// </summary>
    [Serializable]
    public class QPSurcharge: BaseDataObject
    {
        /// <summary>
        /// 报价线路ID
        /// </summary>
        public Guid RateID { get; set; }
        /// <summary>
        /// 费用ID
        /// </summary>
        public Guid ChargeID { get; set; }
        /// <summary>
        /// 费用名称
        /// </summary>
        public string ChargeName { get; set; }
        /// <summary>
        /// 币种ID
        /// </summary>
        public Guid CurrencyID { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 每柜/票
        /// </summary>
        public string PerTypeName { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 报价
        /// </summary>
        public decimal Price { get
            {
                return Quantity * UnitPrice;
            }
        }
    }
    #endregion

    #region 报价单报表数据实体对象
    /// <summary>
    /// 报价单报表数据实体对象
    /// </summary>
    [Serializable]
    public class QPOrderReportData
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public QPOrderReportData()
        {
            RatesList = new List<QPRatesReportData>();
        }

        #region 唯一键/ID
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID { get; set; }

        #endregion

        #region 报价单号

        /// <summary>
        /// 业务号
        /// </summary>
        public string No { get; set; }

        #endregion

        #region 报价目标类型
        /// <summary>
        /// 报价目标类型
        /// </summary>
        public string TargetTypeName { get; set; } 
        #endregion

        #region 客户名称
        private string _customer;

        /// <summary>
        /// 客户名称
        /// </summary>
        public string Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                _customer = _customer.Replace("^", Environment.NewLine);
            }
        }

        #endregion

        #region 客户XML描述
        /// <summary>
        /// 客户XML描述
        /// </summary>
        public CustomerDescription CustomerDesc { get; set; }
        #endregion

        #region 客户详细信息
        /// <summary>
        /// 客户详细信息
        /// </summary>
        public string StrCustomerDescription
        {
            get
            {
                return CustomerDesc.ToString(true);
            }
        }
        #endregion

        #region 品名

        /// <summary>
        /// 品名
        /// </summary>
        public string Commodity { get; set; }

        #endregion

        #region 运输条款
        /// <summary>
        /// 运输条款
        /// </summary>
        public string TransportClause { get; set; }
        #endregion

        #region 付款类型
        /// <summary>
        /// 付款类型
        /// </summary>
        public string PaymentTypeName { get; set; }
        #endregion

        #region 报价Remark

        /// <summary>
        /// 报价Remark
        /// </summary>
        public string Remark { get; set; }
        #endregion

        #region 报价人

        private string _quoteBy;
        /// <summary>
        /// 报价人ID
        /// </summary>
        public string QuoteBy
        {
            get { return _quoteBy; }
            set
            {
                _quoteBy = value;
                _quoteBy = _quoteBy.Replace("^", Environment.NewLine);
            }
        }
        #endregion

        #region 报价人手机号码
        /// <summary>
        /// 报价人手机号码
        /// </summary>
        public string QuoteReferenceNumber { get; set; }
        #endregion

        #region 报价人座机
        /// <summary>
        /// 报价人座机
        /// </summary>
        public string QuoteTelephone { get; set; }
        #endregion

        #region 报价人传真
        /// <summary>
        /// 报价人传真
        /// </summary>
        public string QuoteFax { get; set; }
        #endregion

        #region 报价人邮箱地址
        /// <summary>
        /// 报价人邮箱地址
        /// </summary>
        public string QuoteEMail { get; set; }
        #endregion

        #region 报价人描述

        private string _quoteByDescription;
        /// <summary>
        /// 报价人描述
        /// </summary>
        public string QuoteByDescription
        {
            get { return _quoteByDescription; }
            set
            {
                _quoteByDescription = value;
                _quoteByDescription = _quoteByDescription.Replace("^", Environment.NewLine);
            }
        }

        #endregion

        #region 条款
        /// <summary>
        /// 条款
        /// </summary>
        public string Terms { get; set; }
        #endregion

        #region 有效期

        /// <summary>
        /// 有效期
        /// </summary>
        public string ValidityDate { get; set; }
        #endregion

        #region 请求时间

        /// <summary>
        /// 请求时间
        /// </summary>
        public string RequestDate { get; set; }

        /// <summary>
        /// 报价时间
        /// </summary>
        public string EffectiveStartDate { get; set; }

        #endregion

        #region 报价价格列表
        /// <summary>
        /// 报价价格列表
        /// </summary>
        public List<QPRatesReportData> RatesList
        {
            get;
            set;
        }
        #endregion

        #region 报价价格列表
        List<QPSurcharge> _SurCharges;
        /// <summary>
        /// 报价价格列表
        /// </summary>
        public List<QPSurcharge> SurCharges
        {
            get
            {
                _SurCharges = new List<QPSurcharge>();
                if (RatesList == null)
                    RatesList = new List<QPRatesReportData>();
                foreach (var item in RatesList)
                {
                    _SurCharges.AddRange(item.Surcharges);
                }
                return _SurCharges;
            }
        }
        #endregion
    }
    #endregion

    #region 报价价格报表数据实体对象
    /// <summary>
    /// 报价价格报表数据实体对象
    /// </summary>
    [Serializable]
    public class QPRatesReportData
    {
        /// <summary>
        /// 报价线路ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 船东
        /// </summary>
        public string Carrier
        {
            get;
            set;
        }

        /// <summary>
        /// 收货地名称
        /// </summary>
        public string PlaceOfReceiptName
        {
            get;
            set;
        }

        /// <summary>
        /// 装货港
        /// </summary>
        public string POLName
        {
            get;
            set;
        }

        /// <summary>
        /// 卸货港
        /// </summary>
        public string PODName
        {
            get;
            set;
        }

        /// <summary>
        /// 交货地
        /// </summary>
        public string PlaceOfDeliveryName
        {
            get;
            set;
        }

        /// <summary>
        /// TT
        /// </summary>
        public string TT
        {
            get;
            set;
        }

        /// <summary>
        /// Unit20
        /// </summary>
        public string Unit20
        {
            get;
            set;
        }

        /// <summary>
        /// Unit40
        /// </summary>
        public string Unit40
        {
            get;
            set;
        }

        /// <summary>
        /// Unit40HQ
        /// </summary>
        public string Unit40HQ
        {
            get;
            set;
        }

        /// <summary>
        /// Unit45
        /// </summary>
        public string Unit45
        {
            get;
            set;
        }

        /// <summary>
        /// 附加费描述
        /// </summary>
        public string SurchargeDescription { get; set; }

        /// <summary>
        /// 附加费明细
        /// </summary>
        public List<QPSurcharge> Surcharges
        {
            get
            {
                List<QPSurcharge> _surCharge = new List<QPSurcharge>();
                if (!SurchargeDescription.IsNullOrEmpty())
                {
                    _surCharge = JSONSerializerHelper.DeserializeFromJson<List<QPSurcharge>>(SurchargeDescription);
                }
                if(!Unit20.IsNullOrEmpty())
                {
                    _surCharge.Add(new QPSurcharge { ChargeName = "20 Standard Container", Quantity = 1, UnitPrice = Convert.ToDecimal(Unit20) });
                }
                if (!Unit40.IsNullOrEmpty())
                {
                    _surCharge.Add(new QPSurcharge { ChargeName = "40 Standard Container", Quantity = 1, UnitPrice = Convert.ToDecimal(Unit40) });
                }
                if (!Unit40HQ.IsNullOrEmpty())
                {
                    _surCharge.Add(new QPSurcharge { ChargeName = "40 High Cube Container", Quantity = 1, UnitPrice = Convert.ToDecimal(Unit40HQ) });
                }
                if (!Unit45.IsNullOrEmpty())
                {
                    _surCharge.Add(new QPSurcharge { ChargeName = "45 Standard Container", Quantity = 1, UnitPrice = Convert.ToDecimal(Unit45) });
                }
                foreach (var item in _surCharge)
                {
                    item.RateID = ID;
                }
                return _surCharge;
            }
        }
    }
    #endregion
}
