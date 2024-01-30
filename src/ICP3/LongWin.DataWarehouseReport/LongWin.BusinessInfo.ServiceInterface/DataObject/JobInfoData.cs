using System;
using System.Collections.Generic;
using System.Text;

namespace LongWin.BusinessInfo.ServiceInterface.DataObject
{
    [Serializable]
   public class JobInfoData
    {
        bool _selected;
        /// <summary>
        /// 选择
        /// </summary>
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        Guid _ConsignId;
        /// <summary>
        /// 委托ID
        /// </summary>
        public Guid ConsignId
        {
            get { return _ConsignId; }
            set { _ConsignId = value; }
        }

        Guid _ConsignerId;
        /// <summary>
        /// 委托人ID
        /// </summary>
        public Guid ConsignerId
        {
            get { return _ConsignerId; }
            set { _ConsignerId = value; }
        }

        string _SalesName;
        /// <summary>
        /// 业务员
        /// </summary>
        public string SalesName
        {
            get { return _SalesName; }
            set { _SalesName = value; }
        }

        string _VesselVoyage;
        /// <summary>
        /// 船名航次
        /// </summary>
        public string VesselVoyage
        {
            get { return _VesselVoyage; }
            set { _VesselVoyage = value; }
        }

        string _OperatorName;
        /// <summary>
        /// 操作员
        /// </summary>
        public string OperatorName
        {
            get { return _OperatorName; }
            set { _OperatorName = value; }
        }

        DateTime _ETA;
        /// <summary>
        /// ETA
        /// </summary>
        public DateTime ETA
        {
            get { return _ETA; }
            set { _ETA = value; }
        }

        DateTime _ETD;
        /// <summary>
        /// ETD
        /// </summary>
        public DateTime ETD
        {
            get { return _ETD; }
            set { _ETD = value; }
        }

       Decimal _TEU;
        /// <summary>
        /// 箱量
        /// </summary>
       public Decimal TEU
        {
            get { return _TEU; }
            set { _TEU = value; }
        }

        decimal _AmountUSDByCR;
        /// <summary>
        /// 应付美金
        /// </summary>
        public decimal AmountUSDByCR
        {
            get { return _AmountUSDByCR; }
            set { _AmountUSDByCR = value; }
        }

        decimal _AmountUSDByDR;
        /// <summary>
        /// 应收美金
        /// </summary>
        public decimal AmountUSDByDR
        {
            get { return _AmountUSDByDR; }
            set { _AmountUSDByDR = value; }
        }

        decimal _ProfitByUSD;
        /// <summary>
        /// 利润
        /// </summary>
        public decimal ProfitByUSD
        {
            get { return _ProfitByUSD; }
            set { _ProfitByUSD = value; }
        }

        string _AgentName;
        /// <summary>
        /// 代理
        /// </summary>
        public string AgentName
        {
            get { return _AgentName; }
            set { _AgentName = value; }
        }
        string _ConsignerEName;
        /// <summary>
        /// 委托人名称(英文)
        /// </summary>
        public string ConsignerEName
        {
            get { return _ConsignerEName; }
            set { _ConsignerEName = value; }
        }

        string _ConsignerCName;
        /// <summary>
        /// 委托人名称(中文)
        /// </summary>
        public string ConsignerCName
        {
            get { return _ConsignerCName; }
            set { _ConsignerCName = value; }
        }

        string _DestinationName;
        /// <summary>
        /// 目的地
        /// </summary>
        public string DestinationName
        {
            get { return _DestinationName; }
            set { _DestinationName = value; }
        }

        string _LoadPortname;
        /// <summary>
        /// 装货港口
        /// </summary>
        public string LoadPortname
        {
            get { return _LoadPortname; }
            set { _LoadPortname = value; }
        }

        string _DiscPortName;
        /// <summary>
        /// 卸货港口
        /// </summary>
        public string DiscPortName
        {
            get { return _DiscPortName; }
            set { _DiscPortName = value; }
        }

        string _ContainerNo;
        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNo
        {
            get { return _ContainerNo; }
            set { _ContainerNo = value; }
        }

        string _BLNO;
        /// <summary>
        /// 提单号
        /// </summary>
        public string BLNO
        {
            get { return _BLNO; }
            set { _BLNO = value; }
        }

        //bool _IsVerifyed;
        ///// <summary>
        ///// 
        ///// </summary>
        //public bool IsVerifyed
        //{
        //    get { return _IsVerifyed; }
        //    set { _IsVerifyed = value; }
        //}

        /// <summary>
        /// 收款状态
        /// </summary>
        public ENVerifyed ENVerifyedState { get; set; }
        /// <summary>
        /// 收款状态
        /// </summary>
        public CNVerifyed CNVerifyedState { get; set; }

        string _JobCode;
        /// <summary>
        /// 业务号
        /// </summary>
        public string JobCode
        {
            get { return _JobCode; }
            set { _JobCode = value; }
        }

        decimal _weight;
        /// <summary>
        /// 货物重量
        /// </summary>
        public decimal Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }


        public decimal CommisionAmount { get; set; }
    }

    public enum ENVerifyed
    {
        /// <summary>
        /// 未收款
        /// </summary>
        UnVerifyed=0,
        /// <summary>
        /// 全部已收款
        /// </summary>
        AllVerifyed=1,
        /// <summary>
        /// 部分已收款
        /// </summary>
        PartVerifyed=2
    }
    public enum CNVerifyed
    {
        /// <summary>
        /// 未收款
        /// </summary>
        未收款 = 0,
        /// <summary>
        /// 全部已收款
        /// </summary>
        全部已收款 = 1,
        /// <summary>
        /// 部分已收款
        /// </summary>
        部分已收款 = 2
    }
}
