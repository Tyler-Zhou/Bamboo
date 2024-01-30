using System;
using ICP.Framework.CommonLibrary.Common;
using System.Xml.Serialization;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    #region Business

    /// <summary>
    /// 业务列表对象
    /// </summary>
    [Serializable]
    public partial class BusinessList : BaseDataObject
    {
        /// <summary>
        /// 判断是否新
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }


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
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }
        /// <summary>
        /// 业务状态
        /// </summary>
        OIOrderState _State;
        public OIOrderState State
        {
            get { return _State; }
            set
            {
                if (_State != value)
                {
                    _State = value;
                    this.NotifyPropertyChanged(o => o.State);
                }
            }
        }

        /// <summary>
        /// 港后客服
        /// </summary>
        string _OffshoreCS;
        public string OffshoreCS
        {
            get { return _OffshoreCS; }
            set
            {
                if (_OffshoreCS != value)
                {
                    _OffshoreCS = value;
                    this.NotifyPropertyChanged(o => o.OffshoreCS);
                }
            }
        }
        /// <summary>
        /// 客服
        /// </summary>
        string _customerService;
        public string CustomerService
        {
            get { return _customerService; }
            set
            {
                if (_customerService != value)
                {
                    _customerService = value;
                    this.NotifyPropertyChanged(o => o.CustomerService);
                }
            }
        }

        /// <summary>
        /// 文件员
        /// </summary>
        string _SIBy;
        public string SIBy
        {
            get { return _SIBy; }
            set
            {
                if (_SIBy != value)
                {
                    _SIBy = value;
                    this.NotifyPropertyChanged(o => o.SIBy);
                }
            }
        }

        /// <summary>
        ///离港日
        /// </summary>
        string _eTD;
        public string ETD
        {
            get { return _eTD; }
            set
            {
                if (_eTD != value)
                {
                    _eTD = value;
                    this.NotifyPropertyChanged(o => o.ETD);
                }
            }
        }
        /// <summary>
        /// 到港日
        /// </summary>
        string _eTA;
        public string ETA
        {
            get { return _eTA; }
            set
            {
                if (_eTA != value)
                {
                    _eTA = value;
                    this.NotifyPropertyChanged(o => o.ETA);
                }
            }
        }


        /// <summary>
        /// C/N附件
        /// </summary>
        string _cnCopy;
        public string CNCopy
        {
            get { return _cnCopy; }
            set
            {
                if (_cnCopy != value)
                {
                    _cnCopy = value;
                    this.NotifyPropertyChanged(o => o.CNCopy);
                }
            }
        }

        /// <summary>
        /// MBL附件
        /// </summary>
        string _mblCopy;
        public string MBLCopy
        {
            get { return _mblCopy; }
            set
            {
                if (_mblCopy != value)
                {
                    _mblCopy = value;
                    this.NotifyPropertyChanged(o => o.MBLCopy);
                }
            }
        }

        /// <summary>
        /// SO附件
        /// </summary>
        string _soCopy;
        public string SOCopy
        {
            get { return _soCopy; }
            set
            {
                if (_soCopy != value)
                {
                    _soCopy = value;
                    this.NotifyPropertyChanged(o => o.SOCopy);
                }
            }
        }

        /// <summary>
        /// 提单号
        /// </summary>
        string _blNO;
        public string BLNO
        {
            get { return _blNO; }
            set
            {
                if (_blNO != value)
                {
                    _blNO = value;
                    this.NotifyPropertyChanged(o => o.BLNO);
                }
            }
        }
        /// <summary>
        ///创建日期
        /// </summary>
        DateTime _createByDate;
        public DateTime CreateByDate
        {
            get { return _createByDate; }
            set
            {
                if (_createByDate != value)
                {
                    _createByDate = value;
                    this.NotifyPropertyChanged(o => o.CreateByDate);
                }
            }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        DateTime _updateByDate;
        public DateTime UpdateByDate
        {
            get { return _updateByDate; }
            set
            {
                if (_updateByDate != value)
                {
                    _updateByDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateByDate);
                }
            }
        }

        /// <summary>
        /// 更新人
        /// </summary>
        Guid _updateBy;
        public Guid UpdateBy
        {
            get { return _updateBy; }
            set
            {
                if (_updateBy != value)
                {
                    _updateBy = value;
                    this.NotifyPropertyChanged(o => o.UpdateBy);
                }
            }
        }

        string _delivery;
        public string Delivery
        {
            get { return _delivery; }
            set
            {
                if (_delivery != value)
                {
                    _delivery = value;
                    this.NotifyPropertyChanged(o => o.Delivery);
                }
            }
        }

        /// <summary>
        ///装货港
        /// </summary>
        string _pol;
        public string POL
        {
            get { return _pol; }
            set
            {
                if (_pol != value)
                {
                    _pol = value;
                    this.NotifyPropertyChanged(o => o.POL);
                }
            }
        }

        /// <summary>
        /// 船名航次
        /// </summary>
        string _vesselVoyage;
        public string VesselVoyage
        {
            get { return _vesselVoyage; }
            set
            {
                if (_vesselVoyage != value)
                {
                    _vesselVoyage = value;
                    this.NotifyPropertyChanged(o => o.VesselVoyage);
                }
            }
        }


        /// <summary>
        /// 创建人
        /// </summary>
        Guid _createBy;
        public Guid CreateBy
        {
            get { return _createBy; }
            set
            {
                if (_createBy != value)
                {
                    _createBy = value;
                    this.NotifyPropertyChanged(o => o.CreateBy);
                }
            }

        }



        /// <summary>
        /// 代理
        /// </summary>
        string _Agent;
        public string Agent
        {
            get { return _Agent; }
            set
            {
                if (_Agent != value)
                {
                    _Agent = value;
                    this.NotifyPropertyChanged(o => o.Agent);
                }
            }
        }

        /// <summary>
        /// 船公司 
        /// </summary>
        string _Carrier;
        public string Carrier
        {
            get { return _Carrier; }
            set
            {
                if (_Carrier != value)
                {
                    _Carrier = value;
                    this.NotifyPropertyChanged(o => o.Carrier);
                }
            }
        }

        /// <summary>
        /// 选择
        /// </summary>
        bool _selected;
        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    this.NotifyPropertyChanged(o => o.Selected);
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
                    this.NotifyPropertyChanged(o => o.SalesName);
                }
            }
        }

        string _StateDescription;
        /// <summary>
        /// 状态描述
        /// </summary>
        public string StateDescription
        {
            get { return _StateDescription; }
            set
            {
                if (_StateDescription != value)
                {
                    _StateDescription = value;
                    this.NotifyPropertyChanged(o => o.StateDescription);
                }
            }
        }

        string _CompanyName;
        /// <summary>
        /// 公司
        /// </summary>
        public string CompanyName
        {
            get { return _CompanyName; }
            set
            {
                if (_CompanyName != value)
                {
                    _CompanyName = value;
                    this.NotifyPropertyChanged(o => o.CompanyName);
                }
            }
        }


        Guid _CompanyId;
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID
        {
            get { return _CompanyId; }
            set
            {
                if (_CompanyId != value)
                {
                    _CompanyId = value;
                    this.NotifyPropertyChanged(o => o.CompanyID);
                }
            }
        }


        Guid? _AgentID;
        /// <summary>
        /// 代理ID
        /// </summary>
        public Guid? AgentID
        {
            get { return _AgentID; }
            set
            {
                if (_AgentID != value)
                {
                    _AgentID = value;
                    this.NotifyPropertyChanged(o => o.AgentID);
                }
            }
        }

        Guid _SolutionID;
        /// <summary>
        /// 公司对应的解决方案ID
        /// </summary>
        public Guid SolutionID
        {
            get { return _SolutionID; }
            set
            {
                if (_SolutionID != value)
                {
                    _SolutionID = value;
                    this.NotifyPropertyChanged(o => o.SolutionID);
                }
            }
        }

        string _operationNo;
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNO
        {
            get { return _operationNo; }
            set
            {
                if (_operationNo != value)
                {
                    _operationNo = value;
                    this.NotifyPropertyChanged(o => o.OperationNO);
                }
            }
        }

        OperationType _OperationType;
        /// <summary>
        /// OperationType
        /// </summary>
        public OperationType OperationType
        {
            get { return _OperationType; }
            set
            {
                if (_OperationType != value)
                {
                    _OperationType = value;
                    this.NotifyPropertyChanged(o => o.OperationType);
                }
            }
        }

        string _CustomerName;
        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerName
        {
            get { return _CustomerName; }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    this.NotifyPropertyChanged(o => o.CustomerName);
                }
            }
        }


        Guid _CustomerID;
        /// <summary>
        /// 客户
        /// </summary>
        public Guid CustomerID
        {
            get { return _CustomerID; }
            set
            {
                if (_CustomerID != value)
                {
                    _CustomerID = value;
                    this.NotifyPropertyChanged(o => o.CustomerID);
                }
            }
        }

        string _GoodsNmae;
        /// <summary>
        /// 品名
        /// </summary>
        public string GoodsNmae
        {
            get { return _GoodsNmae; }
            set
            {
                if (_GoodsNmae != value)
                {
                    _GoodsNmae = value;
                    this.NotifyPropertyChanged(o => o.GoodsNmae);
                }
            }
        }



        string _RefNO;
        /// <summary>
        /// 参考号
        /// </summary>
        public string RefNO
        {
            get { return _RefNO; }
            set
            {
                if (_RefNO != value)
                {
                    _RefNO = value;
                    this.NotifyPropertyChanged(o => o.RefNO);
                }
            }
        }

        string _OperationDescription;
        /// <summary>
        /// 业务描述
        /// </summary>
        public string OperationDescription
        {
            get { return _OperationDescription; }
            set
            {
                if (_OperationDescription != value)
                {
                    _OperationDescription = value;
                    this.NotifyPropertyChanged(o => o.OperationDescription);
                }
            }
        }

        decimal _AR;
        /// <summary>
        /// 应收
        /// </summary>
        public decimal AR
        {
            get
            {
                return _AR;
            }
            set
            {
                if (_AR != value)
                {
                    _AR = value;
                    this.NotifyPropertyChanged(o => o.AR);
                }
            }
        }

        decimal _AP;
        /// <summary>
        /// 应付
        /// </summary>
        public decimal AP
        {
            get
            {
                return _AP;
            }
            set
            {
                if (_AP != value)
                {
                    _AP = value;
                    this.NotifyPropertyChanged(o => o.AP);
                }
            }
        }

        decimal _Profit;
        /// <summary>
        /// 利润
        /// </summary>
        public decimal Profit
        {
            get
            {
                return _Profit;
            }
            set
            {
                if (_Profit != value)
                {
                    _Profit = value;
                    this.NotifyPropertyChanged(o => o.Profit);
                }
            }
        }


        string _ARDescription;
        /// <summary>
        /// 应收文本
        /// </summary>
        public string ARDescription
        {
            get
            {
                return _ARDescription;
            }
            set
            {
                if (_ARDescription != value)
                {
                    _ARDescription = value;
                    this.NotifyPropertyChanged(o => o.ARDescription);
                }
            }
        }

        string _APDescription;
        /// <summary>
        /// 应付文本
        /// </summary>
        public string APDescription
        {
            get
            {
                return _APDescription;
            }
            set
            {
                if (_APDescription != value)
                {
                    _APDescription = value;
                    this.NotifyPropertyChanged(o => o.APDescription);
                }
            }
        }

        string _ProfitDescription;
        /// <summary>
        /// 利润
        /// </summary>
        public string ProfitDescription
        {
            get
            {
                return _ProfitDescription;
            }
            set
            {
                if (_ProfitDescription != value)
                {
                    _ProfitDescription = value;
                    this.NotifyPropertyChanged(o => o.ProfitDescription);
                }
            }
        }

        Guid _DefaultCurrencyID;
        /// <summary>
        /// 默认币种ID
        /// </summary>
        public Guid DefaultCurrencyID
        {
            get
            {
                return _DefaultCurrencyID;
            }
            set
            {
                if (_DefaultCurrencyID != value)
                {
                    _DefaultCurrencyID = value;
                    this.NotifyPropertyChanged(o => o.DefaultCurrencyID);
                }
            }
        }

        string _DefaultCurrencyName;
        /// <summary>
        /// 默认币种
        /// </summary>
        public string DefaultCurrencyName
        {
            get
            {
                return _DefaultCurrencyName;
            }
            set
            {
                if (_DefaultCurrencyName != value)
                {
                    _DefaultCurrencyName = value;
                    this.NotifyPropertyChanged(o => o.DefaultCurrencyName);
                }
            }
        }
        /// <summary>
        /// SONO
        /// </summary>
        /// 
        string _sono;
        public string SONO
        {
            get { return _sono; }
            set
            {
                if (_sono != value)
                {
                    _sono = value;
                    this.NotifyPropertyChanged(o => o.SONO);
                }
            }
        }
        /// <summary>
        /// ContainerNo
        /// </summary>
        /// 
        string _containerNos;
        public string ContainerNos
        {
            get { return _containerNos; }
            set
            {
                if (_containerNos != value)
                {
                    _containerNos = value;
                    this.NotifyPropertyChanged(o => o.ContainerNos);
                }
            }
        }
        /// <summary>
        ///箱形
        /// </summary>
        string _containerDesc;
        public string ContainerDesc
        {
            get { return _containerDesc; }
            set
            {
                if (_containerDesc != value)
                {
                    _containerDesc = value;
                    this.NotifyPropertyChanged(o => o.ContainerDesc);
                }
            }
        }
        /// <summary>
        /// 主提单号
        /// </summary>
        /// 
        string _HBLs;
        public string HBLs
        {
            get { return _HBLs; }
            set
            {
                if (_HBLs != value)
                {
                    _HBLs = value;
                    this.NotifyPropertyChanged(o => o.HBLs);
                }
            }
        }
        /// <summary>
        /// 分提单号
        /// </summary>
        /// 
        string _MBLs;
        public string MBLs
        {
            get { return _MBLs; }
            set
            {
                if (_MBLs != value)
                {
                    _MBLs = value;
                    this.NotifyPropertyChanged(o => o.MBLs);
                }
            }
        }
        /// <summary>
        /// 主提单号ID
        /// </summary>
        /// 
        string _mblID;
        public string MBLID
        {
            get { return _mblID; }
            set
            {
                if (_mblID != value)
                {
                    _mblID = value;
                    this.NotifyPropertyChanged(o => o.MBLID);
                }
            }
        }
        /// <summary>
        /// 分提单号ID
        /// </summary>
        /// 
        string _hblID;
        public string HBLID
        {
            get { return _hblID; }
            set
            {
                if (_hblID != value)
                {
                    _hblID = value;
                    this.NotifyPropertyChanged(o => o.HBLID);
                }
            }
        }
        /// <summary>
        /// 卸货港
        /// </summary>
        /// 
        string _pod;
        public string POD
        {
            get { return _pod; }
            set
            {
                if (_pod != value)
                {
                    _pod = value;
                    this.NotifyPropertyChanged(o => o.POD);
                }
            }
        }
        /// <summary>
        /// 商品
        /// </summary>
        /// 
        string _commdity;
        public string Commodity
        {
            get { return _commdity; }
            set
            {
                if (_commdity != value)
                {
                    _commdity = value;
                    this.NotifyPropertyChanged(o => o.Commodity);
                }
            }
        }
    }

    /// <summary>
    /// 帐单列表的业务列表对象
    /// </summary>
    [Serializable]
    public partial class FollowingBusinessList : BusinessList
    {
        string _BillToName;
        /// <summary>
        /// 往来单位
        /// </summary>
        public string BillToName
        {
            get { return _BillToName; }
            set
            {
                if (_BillToName != value)
                {
                    _BillToName = value;
                    this.NotifyPropertyChanged(o => o.BillToName);
                }
            }
        }

        Guid _BillToID;
        /// <summary>
        /// 往来单位
        /// </summary>
        public Guid BillToID
        {
            get { return _BillToID; }
            set
            {
                if (_BillToID != value)
                {
                    _BillToID = value;
                    this.NotifyPropertyChanged(o => o.BillToID);
                }
            }
        }
    }

    #endregion

    #region OperationParameter

    /// <summary>
    /// 业务搜索参数抽象类
    /// </summary>
    [Serializable]
    public class OperationParameter
    {
        /// <summary>
        /// 提单号
        /// </summary>
        public string BlNo { get; set; }
        /// <summary>
        /// 箱号
        /// </summary>
        public string CtnNo { get; set; }
        /// <summary>
        /// 费用ID集合
        /// </summary>
        public string ChargeCodeIDs{get;set; }
    }

    /// <summary>
    /// 海运
    /// </summary>
    [Serializable]
    [XmlRoot("OperationParameter")]
    public class OceanParameter : OperationParameter
    {
        public bool IsAPCCFM { get; set; }
        public bool IsARC { get; set; }

        public bool IsRBLD { get; set; }

        /// <summary>
        /// 订舱号
        /// </summary>
        public string SONo { get; set; }
        /// <summary>
        /// 船名
        /// </summary>
        public string Vessel { get; set; }
        /// <summary>
        /// 航次
        /// </summary>
        public string VoayeNo { get; set; }
        /// <summary>
        /// ETDFrom
        /// </summary>
        public DateTime? ETDFrom { get; set; }
        /// <summary>
        /// ETDTo
        /// </summary>
        public DateTime? ETDTo { get; set; }
        /// <summary>
        /// ETAFrom
        /// </summary>
        public DateTime? ETAFrom { get; set; }
        /// <summary>
        /// ETATo
        /// </summary>
        public DateTime? ETATo { get; set; }
    }

    /// <summary>
    /// 空运
    /// </summary>
    [Serializable]
    [XmlRoot("OperationParameter")]
    public class AirParameter : OperationParameter
    {
        /// <summary>
        /// ETDFrom
        /// </summary>
        public DateTime? ETDFrom { get; set; }
        /// <summary>
        /// ETDTo
        /// </summary>
        public DateTime? ETDTo { get; set; }
        /// <summary>
        /// ETAFrom
        /// </summary>
        public DateTime? ETAFrom { get; set; }
        /// <summary>
        /// ETATo
        /// </summary>
        public DateTime? ETATo { get; set; }
    }

    /// <summary>
    /// 报关
    /// </summary>
    [Serializable]
    [XmlRoot("OperationParameter")]
    public class CustomsParameter : OperationParameter
    {
        //public string No { get; set; }
        //public short CustomsType { get; set; }
        //public string Fee { get; set; }
        //public string Location { get; set; }
        //public string SONo { get; set; }
        //public string CustomsDeclarationNo { get; set; }
        //public string WriteOffNo { get; set; }
        //public string ContainerNo { get; set; }
    }

    /// <summary>
    /// 海运
    /// </summary>
    [Serializable]
    [XmlRoot("OperationParameter")]
    public class OtherParameter : OperationParameter
    {
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }


    #endregion

    #region 运费付乞数据

    /// <summary>
    /// 运费付讫数据对象
    /// </summary>
    [Serializable]
    public class PaymentFreightItem
    {
        /// <summary>
        /// 工作流名称
        /// </summary>
        public string WorkFlowName { get; set; }

        /// <summary>
        /// 提单号
        /// </summary>
        public string BLNo { get; set; }

        public Guid CompanyID { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public string PaymentMode { get; set; }

        /// <summary>
        /// 收款单位ID
        /// </summary>
        public Guid BillToID { get; set; }

        /// <summary>
        /// 收款单位名称
        /// </summary>
        public string BillToName { get; set; }

        /// <summary>
        /// 帐单号
        /// </summary>
        public string BillNo { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 帐号
        /// </summary>
        public string BankNo { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        public string GoodsNmae { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        /// 原始金额
        /// </summary>
        public string OriginalityAmount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
    [Serializable]
    public class BusinessSearchParameter
    {
        public BusinessSearchParameter() { DataPageInfo = new DataPageInfo(); }

        public Guid[] companyIDs { get; set; }
        public string operationNo { get; set; }
        public string blNo { get; set; }
        public string ctnNo { get; set; }
        public string customer { get; set; }
        public string sales { get; set; }
        public string filerID { get; set; }
        public decimal? minProfit { get; set; }
        public decimal? maxProfit { get; set; }
        public OperationType? operationType { get; set; }
        public OperationParameter parameter { get; set; }
        public DataPageInfo DataPageInfo { get; set; }
    }
    #endregion

}
