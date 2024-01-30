using System;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.WF.ServiceInterface
{
    /// <summary>
    /// 业务列表对象
    /// </summary>
    [Serializable]
    public partial class WFBusinessList : BaseDataObject
    {

        /// <summary>
        /// 选择
        /// </summary>
        public bool Selected { get; set; }

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

        decimal _ECCost;
        /// <summary>
        /// 电商费用
        /// </summary>
        public decimal ECCost
        {
            get
            {
                return _ECCost;
            }
            set
            {
                if (_ECCost != value)
                {
                    _ECCost = value;
                    this.NotifyPropertyChanged(o => o.ECCost);
                }
            }
        }

        decimal _Ratio;
        /// <summary>
        /// 利润配比
        /// </summary>
        public decimal Ratio
        {
            get
            {
                return _Ratio;
            }
            set
            {
                if (_Ratio != value)
                {
                    _Ratio = value;
                    this.NotifyPropertyChanged(o => o.Ratio);
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

        /// <summary>
        /// 佣金金额
        /// </summary>
        public decimal CommissionAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 已支付的佣金金额
        /// </summary>
        public decimal CommissionPayAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 佣金金额
        /// </summary>
        public Guid CommissionCurrencyID
        {
            get;
            set;
        }
        /// <summary>
        /// 收款状态
        /// </summary>
        public PaidStatus PaidStatus
        {
            get;
            set;
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

        /// <summary>
        /// 利润备注
        /// </summary>
        public string CommissionAmountDescription
        {
            get;
            set;
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
    }


    /// <summary>
    /// 日志记录
    /// </summary>
    [Serializable]
    public class WFCommissionLogList
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID
        {
            get;
            set;
        }
        /// <summary>
        /// 工作号
        /// </summary>
        public string WorkFlowNo
        {
            get;
            set;
        }
        /// <summary>
        /// 工作名
        /// </summary>
        public string WorkName
        {
            get;
            set;
        }
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperactioNo
        {
            get;
            set;
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateName
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get;
            set;
        }




    }


}
