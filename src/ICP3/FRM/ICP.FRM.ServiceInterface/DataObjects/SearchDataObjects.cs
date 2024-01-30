using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using System.Runtime.Serialization;

namespace ICP.FRM.ServiceInterface.DataObjects
{

    #region 海运运价查询列表
    /// <summary>
    /// 海运运价查询列表
    /// </summary>
    [Serializable]
    [KnownType(typeof(FrmUnitRateInfo))]
    public class SearchOceanRateList : BaseDataObject
    {
        #region Base Info
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 运价ID
        /// </summary>
        //public Guid OceanID { get; set; }

        ///// <summary>
        ///// BasePortID
        ///// </summary>
        //public Guid BasePortID { get; set; }

        ///// <summary>
        ///// OriginalArbitraryID
        ///// </summary>
        //public Guid? OriginalArbitraryID { get; set; }
        ///// <summary>
        ///// DestinationArbitraryID
        ///// </summary>
        //public Guid? DestinationArbitraryID { get; set; }
        ///// <summary>
        ///// OCEANS State
        ///// </summary>
        //public OceanState OceanState { get; set; }


        public bool IsCheck { get; set; }

        /// <summary>
        /// 运价状态
        /// </summary>
        public SearchPriceStatus Statue { get; set; }

        /// <summary>
        /// 运价类型
        /// </summary>
        public SearchRateType SearchrateType { get; set; }
        /// <summary>
        /// CarrierName
        /// </summary>
        public string CarrierName { get; set; }

        /// <summary>
        /// POL
        /// </summary>
        public string POLName { get; set; }

        /// <summary>
        /// VIA
        /// </summary>
        public string VIAName { get; set; }
        /// <summary>
        /// POD
        /// </summary>
        public string PODName { get; set; }
        /// <summary>
        /// Delivery
        /// </summary>
        public string DeliveryName { get; set; }

        /// <summary>
        /// FinalDestinationName
        /// </summary>
        public string FinalDestinationName { get; set; }

        /// <summary>
        /// Commodity
        /// </summary>
        public string Commodity { get; set; }

        /// <summary>
        /// Term
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// SurCharge
        /// </summary>
        private string _SurCharge;
        public string SurCharge
        {
            get { return _SurCharge; }
            set
            {
                if (_SurCharge != value)
                {
                    _SurCharge = value;
                    base.OnPropertyChanged("SurCharge", value);
                }

            }
        }
        /// <summary>
        /// CLS
        /// </summary>
        public string CLS { get; set; }
        /// <summary>
        /// TT
        /// </summary>
        public string TT { get; set; }

        /// <summary>
        /// Duration(From)
        /// </summary>
        private DateTime _DurationFrom;
        public DateTime DurationFrom
        {
            get { return _DurationFrom; }
            set
            {
                if (_DurationFrom != value)
                {
                    _DurationFrom = value;
                    base.OnPropertyChanged("DurationFrom", value);
                }
            }
        }

        /// <summary>
        /// Duration(To)
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private DateTime _DurationTo;
        public DateTime DurationTo
        {
            get { return _DurationTo; }
            set
            {
                if (_DurationTo != value)
                {
                    _DurationTo = value;
                    base.OnPropertyChanged("DurationTo", value);
                }
            }
        }

        /// <summary>
        /// 询价备注
        /// </summary>
        private string _Remark;
        public string Remark
        {
            get { return _Remark; }
            set
            {
                if (_Remark != value)
                {
                    _Remark = value;
                    base.OnPropertyChanged("Remark", value);
                }
            }
        }

        /// <summary>
        /// Description
        /// </summary>       
        public string Description { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// PRO数量
        /// </summary>
        public Int32 PROCount { get; set; }


        #endregion

        #region Rates

        #region Rate_45FR
        decimal? rate_45FR;
        /// <summary>
        ///Rate_45FR
        /// </summary>
        public decimal? Rate_45FR
        {
            get
            {
                return rate_45FR == null ? 0 : rate_45FR;
            }
            set
            {
                rate_45FR = value;
            }

        }
        #endregion

        #region Rate_40RF
        decimal? rate_40RF;
        /// <summary>
        ///Rate_40RF
        /// </summary>
        public decimal? Rate_40RF
        {
            get
            {
                return rate_40RF == null ? 0 : rate_40RF;
            }
            set
            {
                rate_40RF = value;
            }

        }
        #endregion

        #region Rate_45HT
        decimal? rate_45HT;
        /// <summary>
        ///Rate_45HT
        /// </summary>
        public decimal? Rate_45HT
        {
            get
            {
                return rate_45HT == null ? 0 : rate_45HT;
            }
            set
            {
                rate_45HT = value;
            }
        }
        #endregion

        #region Rate_20RF
        decimal? rate_20RF;
        /// <summary>
        ///Rate_20RF
        /// </summary>
        public decimal? Rate_20RF
        {
            get
            {
                return rate_20RF == null ? 0 : rate_20RF;
            }
            set
            {
                rate_20RF = value;
            }
        }
        #endregion

        #region Rate_20HQ
        decimal? rate_20HQ;
        /// <summary>
        ///Rate_20HQ
        /// </summary>
        public decimal? Rate_20HQ
        {
            get
            {
                return rate_20HQ == null ? 0 : rate_20HQ;
            }
            set
            {
                rate_20HQ = value;
            }
        }
        #endregion

        #region Rate_20TK
        decimal? rate_20TK;
        /// <summary>
        ///Rate_20TK
        /// </summary>
        public decimal? Rate_20TK
        {
            get
            {
                return rate_20TK == null ? 0 : rate_20TK;
            }
            set
            {
                rate_20TK = value;
            }
        }
        #endregion

        #region Rate_20GP
        decimal? rate_20GP;
        /// <summary>
        ///Rate_20GP
        /// </summary>
        public decimal? Rate_20GP
        {
            get
            {
                return rate_20GP == null ? 0 : rate_20GP;
            }
            set
            {
                rate_20GP = value;
            }
        }
        #endregion

        #region Rate_40TK
        decimal? rate_40TK;
        /// <summary>
        ///Rate_40TK
        /// </summary>
        public decimal? Rate_40TK
        {
            get
            {
                return rate_40TK == null ? 0 : rate_40TK;
            }
            set
            {
                rate_40TK = value;
            }
        }
        #endregion

        #region Rate_40OT
        decimal? rate_40OT;
        /// <summary>
        ///Rate_40OT
        /// </summary>
        public decimal? Rate_40OT
        {
            get
            {
                return rate_40OT == null ? 0 : rate_40OT;
            }
            set
            {
                rate_40OT = value;
            }
        }
        #endregion

        #region Rate_20FR
        decimal? rate_20FR;
        /// <summary>
        ///Rate_20FR
        /// </summary>
        public decimal? Rate_20FR
        {
            get
            {
                return rate_20FR == null ? 0 : rate_20FR;
            }
            set
            {
                rate_20FR = value;
            }
        }
        #endregion

        #region Rate_45GP
        decimal? rate_45GP;
        /// <summary>
        ///Rate_45GP
        /// </summary>
        public decimal? Rate_45GP
        {
            get
            {
                return rate_45GP == null ? 0 : rate_45GP;
            }
            set
            {
                rate_45GP = value;
            }
        }
        #endregion

        #region Rate_40GP
        decimal? rate_40GP;
        /// <summary>
        ///Rate_40GP
        /// </summary>
        public decimal? Rate_40GP
        {
            get
            {
                return rate_40GP == null ? 0 : rate_40GP;
            }
            set
            {
                rate_40GP = value;
            }
        }
        #endregion

        #region Rate_45RF
        decimal? rate_45RF;
        /// <summary>
        ///Rate_45RF
        /// </summary>
        public decimal? Rate_45RF
        {
            get
            {
                return rate_45RF == null ? 0 : rate_45RF;
            }
            set
            {
                rate_45RF = value;
            }
        }
        #endregion

        #region Rate_20RH
        decimal? rate_20RH;
        /// <summary>
        ///Rate_20RH
        /// </summary>
        public decimal? Rate_20RH
        {
            get
            {
                return rate_20RH == null ? 0 : rate_20RH;
            }
            set
            {
                rate_20RH = value;
            }
        }
        #endregion

        #region Rate_45OT
        decimal? rate_45OT;
        /// <summary>
        ///Rate_45OT
        /// </summary>
        public decimal? Rate_45OT
        {
            get
            {
                return rate_45OT == null ? 0 : rate_45OT;
            }
            set
            {
                rate_45OT = value;
            }
        }
        #endregion

        #region Rate_40NOR
        decimal? rate_40NOR;
        /// <summary>
        ///Rate_40NOR
        /// </summary>
        public decimal? Rate_40NOR
        {
            get
            {
                return rate_40NOR == null ? 0 : rate_40NOR;
            }
            set
            {
                rate_40NOR = value;
            }
        }
        #endregion

        #region Rate_40FR
        decimal? rate_40FR;
        /// <summary>
        ///Rate_40FR
        /// </summary>
        public decimal? Rate_40FR
        {
            get
            {
                return rate_40FR == null ? 0 : rate_40FR;
            }
            set
            {
                rate_40FR = value;
            }
        }
        #endregion

        #region Rate_20OT
        decimal? rate_20OT;
        /// <summary>
        ///Rate_20OT
        /// </summary>
        public decimal? Rate_20OT
        {
            get
            {
                return rate_20OT == null ? 0 : rate_20OT;
            }
            set
            {
                rate_20OT = value;
            }
        }
        #endregion

        #region Rate_45TK
        decimal? rate_45TK;
        /// <summary>
        ///Rate_45TK
        /// </summary>
        public decimal? Rate_45TK
        {
            get
            {
                return rate_45TK == null ? 0 : rate_45TK;
            }
            set
            {
                rate_45TK = value;
            }
        }
        #endregion

        #region Rate_20NOR
        decimal? rate_20NOR;
        /// <summary>
        ///Rate_20NOR
        /// </summary>
        public decimal? Rate_20NOR
        {
            get
            {
                return rate_20NOR == null ? 0 : rate_20NOR;
            }
            set
            {
                rate_20NOR = value;
            }
        }
        #endregion

        #region Rate_40HT
        decimal? rate_40HT;
        /// <summary>
        ///Rate_40HT
        /// </summary>
        public decimal? Rate_40HT
        {
            get
            {
                return rate_40HT == null ? 0 : rate_40HT;
            }
            set
            {
                rate_40HT = value;
            }
        }
        #endregion

        #region Rate_40RH
        decimal? rate_40RH;
        /// <summary>
        ///Rate_40RH
        /// </summary>
        public decimal? Rate_40RH
        {
            get
            {
                return rate_40RH == null ? 0 : rate_40RH;
            }
            set
            {
                rate_40RH = value;
            }
        }
        #endregion

        #region Rate_45RH
        decimal? rate_45RH;
        /// <summary>
        ///Rate_45RH
        /// </summary>
        public decimal? Rate_45RH
        {
            get
            {
                return rate_45RH == null ? 0 : rate_45RH;
            }
            set
            {
                rate_45RH = value;
            }
        }
        #endregion

        #region Rate_45HQ
        decimal? rate_45HQ;
        /// <summary>
        ///Rate_45HQ
        /// </summary>
        public decimal? Rate_45HQ
        {
            get
            {
                return rate_45HQ == null ? 0 : rate_45HQ;
            }
            set
            {
                rate_45HQ = value;
            }
        }
        #endregion

        #region Rate_20HT
        decimal? rate_20HT;
        /// <summary>
        ///Rate_20HT
        /// </summary>
        public decimal? Rate_20HT
        {
            get
            {
                return rate_20HT == null ? 0 : rate_20HT;
            }
            set
            {
                rate_20HT = value;
            }
        }
        #endregion

        #region Rate_40HQ
        decimal? rate_40HQ;
        /// <summary>
        ///Rate_40HQ
        /// </summary>
        public decimal? Rate_40HQ
        {
            get
            {
                return rate_40HQ == null ? 0 : rate_40HQ;
            }
            set
            {
                rate_40HQ = value;
            }
        }
        #endregion

        #region Rate_53HQ
        decimal? rate_53HQ;
        /// <summary>
        ///Rate_53HQ
        /// </summary>
        public decimal? Rate_53HQ
        {
            get
            {
                return rate_53HQ == null ? 0 : rate_53HQ;
            }
            set
            {
                rate_53HQ = value;
            }
        }
        #endregion

        #endregion

        #region SalesRates

        #region Rate_45FR_Sales
        decimal? rate_45FR_Sales;
        /// <summary>
        ///Rate_45FR_Sales
        /// </summary>
        public decimal? Rate_45FR_Sales
        {
            get
            {
                return rate_45FR_Sales == null ? 0 : rate_45FR_Sales;
            }
            set
            {
                rate_45FR_Sales = value;
            }

        }
        #endregion

        #region Rate_40RF_Sales
        decimal? rate_40RF_Sales;
        /// <summary>
        ///Rate_40RF_Sales
        /// </summary>
        public decimal? Rate_40RF_Sales
        {
            get
            {
                return rate_40RF_Sales == null ? 0 : rate_40RF_Sales;
            }
            set
            {
                rate_40RF_Sales = value;
            }

        }
        #endregion

        #region Rate_45HT_Sales
        decimal? rate_45HT_Sales;
        /// <summary>
        ///Rate_45HT_Sales
        /// </summary>
        public decimal? Rate_45HT_Sales
        {
            get
            {
                return rate_45HT_Sales == null ? 0 : rate_45HT_Sales;
            }
            set
            {
                rate_45HT_Sales = value;
            }
        }
        #endregion

        #region Rate_20RF_Sales
        decimal? rate_20RF_Sales;
        /// <summary>
        ///Rate_20RF_Sales
        /// </summary>
        public decimal? Rate_20RF_Sales
        {
            get
            {
                return rate_20RF_Sales == null ? 0 : rate_20RF_Sales;
            }
            set
            {
                rate_20RF_Sales = value;
            }
        }
        #endregion

        #region Rate_20HQ_Sales
        decimal? rate_20HQ_Sales;
        /// <summary>
        ///Rate_20HQ_Sales
        /// </summary>
        public decimal? Rate_20HQ_Sales
        {
            get
            {
                return rate_20HQ_Sales == null ? 0 : rate_20HQ_Sales;
            }
            set
            {
                rate_20HQ_Sales = value;
            }
        }
        #endregion

        #region Rate_20TK_Sales
        decimal? rate_20TK_Sales;
        /// <summary>
        ///Rate_20TK_Sales
        /// </summary>
        public decimal? Rate_20TK_Sales
        {
            get
            {
                return rate_20TK_Sales == null ? 0 : rate_20TK_Sales;
            }
            set
            {
                rate_20TK_Sales = value;
            }
        }
        #endregion

        #region Rate_20GP_Sales
        decimal? rate_20GP_Sales;
        /// <summary>
        ///Rate_20GP_Sales
        /// </summary>
        public decimal? Rate_20GP_Sales
        {
            get
            {
                return rate_20GP_Sales == null ? 0 : rate_20GP_Sales;
            }
            set
            {
                rate_20GP_Sales = value;
            }
        }
        #endregion

        #region Rate_40TK_Sales
        decimal? rate_40TK_Sales;
        /// <summary>
        ///Rate_40TK_Sales
        /// </summary>
        public decimal? Rate_40TK_Sales
        {
            get
            {
                return rate_40TK_Sales == null ? 0 : rate_40TK_Sales;
            }
            set
            {
                rate_40TK_Sales = value;
            }
        }
        #endregion

        #region Rate_40OT_Sales
        decimal? rate_40OT_Sales;
        /// <summary>
        ///Rate_40OT_Sales
        /// </summary>
        public decimal? Rate_40OT_Sales
        {
            get
            {
                return rate_40OT_Sales == null ? 0 : rate_40OT_Sales;
            }
            set
            {
                rate_40OT_Sales = value;
            }
        }
        #endregion

        #region Rate_20FR_Sales
        decimal? rate_20FR_Sales;
        /// <summary>
        ///Rate_20FR_Sales
        /// </summary>
        public decimal? Rate_20FR_Sales
        {
            get
            {
                return rate_20FR_Sales == null ? 0 : rate_20FR_Sales;
            }
            set
            {
                rate_20FR_Sales = value;
            }
        }
        #endregion

        #region Rate_45GP_Sales
        decimal? rate_45GP_Sales;
        /// <summary>
        ///Rate_45GP_Sales
        /// </summary>
        public decimal? Rate_45GP_Sales
        {
            get
            {
                return rate_45GP_Sales == null ? 0 : rate_45GP_Sales;
            }
            set
            {
                rate_45GP_Sales = value;
            }
        }
        #endregion

        #region Rate_40GP_Sales
        decimal? rate_40GP_Sales;
        /// <summary>
        ///Rate_40GP_Sales
        /// </summary>
        public decimal? Rate_40GP_Sales
        {
            get
            {
                return rate_40GP_Sales == null ? 0 : rate_40GP_Sales;
            }
            set
            {
                rate_40GP_Sales = value;
            }
        }
        #endregion

        #region Rate_45RF_Sales
        decimal? rate_45RF_Sales;
        /// <summary>
        ///Rate_45RF_Sales
        /// </summary>
        public decimal? Rate_45RF_Sales
        {
            get
            {
                return rate_45RF_Sales == null ? 0 : rate_45RF_Sales;
            }
            set
            {
                rate_45RF_Sales = value;
            }
        }
        #endregion

        #region Rate_20RH_Sales
        decimal? rate_20RH_Sales;
        /// <summary>
        ///Rate_20RH_Sales
        /// </summary>
        public decimal? Rate_20RH_Sales
        {
            get
            {
                return rate_20RH_Sales == null ? 0 : rate_20RH_Sales;
            }
            set
            {
                rate_20RH_Sales = value;
            }
        }
        #endregion

        #region Rate_45OT_Sales
        decimal? rate_45OT_Sales;
        /// <summary>
        ///Rate_45OT_Sales
        /// </summary>
        public decimal? Rate_45OT_Sales
        {
            get
            {
                return rate_45OT_Sales == null ? 0 : rate_45OT_Sales;
            }
            set
            {
                rate_45OT_Sales = value;
            }
        }
        #endregion

        #region Rate_40NOR_Sales
        decimal? rate_40NOR_Sales;
        /// <summary>
        ///Rate_40NOR_Sales
        /// </summary>
        public decimal? Rate_40NOR_Sales
        {
            get
            {
                return rate_40NOR_Sales == null ? 0 : rate_40NOR_Sales;
            }
            set
            {
                rate_40NOR_Sales = value;
            }
        }
        #endregion

        #region Rate_40FR_Sales
        decimal? rate_40FR_Sales;
        /// <summary>
        ///Rate_40FR_Sales
        /// </summary>
        public decimal? Rate_40FR_Sales
        {
            get
            {
                return rate_40FR_Sales == null ? 0 : rate_40FR_Sales;
            }
            set
            {
                rate_40FR_Sales = value;
            }
        }
        #endregion

        #region Rate_20OT_Sales
        decimal? rate_20OT_Sales;
        /// <summary>
        ///Rate_20OT_Sales
        /// </summary>
        public decimal? Rate_20OT_Sales
        {
            get
            {
                return rate_20OT_Sales == null ? 0 : rate_20OT_Sales;
            }
            set
            {
                rate_20OT_Sales = value;
            }
        }
        #endregion

        #region Rate_45TK_Sales
        decimal? rate_45TK_Sales;
        /// <summary>
        ///Rate_45TK_Sales
        /// </summary>
        public decimal? Rate_45TK_Sales
        {
            get
            {
                return rate_45TK_Sales == null ? 0 : rate_45TK_Sales;
            }
            set
            {
                rate_45TK_Sales = value;
            }
        }
        #endregion

        #region Rate_20NOR_Sales
        decimal? rate_20NOR_Sales;
        /// <summary>
        ///Rate_20NOR_Sales
        /// </summary>
        public decimal? Rate_20NOR_Sales
        {
            get
            {
                return rate_20NOR_Sales == null ? 0 : rate_20NOR_Sales;
            }
            set
            {
                rate_20NOR_Sales = value;
            }
        }
        #endregion

        #region Rate_40HT_Sales
        decimal? rate_40HT_Sales;
        /// <summary>
        ///Rate_40HT_Sales
        /// </summary>
        public decimal? Rate_40HT_Sales
        {
            get
            {
                return rate_40HT_Sales == null ? 0 : rate_40HT_Sales;
            }
            set
            {
                rate_40HT_Sales = value;
            }
        }
        #endregion

        #region Rate_40RH_Sales
        decimal? rate_40RH_Sales;
        /// <summary>
        ///Rate_40RH_Sales
        /// </summary>
        public decimal? Rate_40RH_Sales
        {
            get
            {
                return rate_40RH_Sales == null ? 0 : rate_40RH_Sales;
            }
            set
            {
                rate_40RH_Sales = value;
            }
        }
        #endregion

        #region Rate_45RH_Sales
        decimal? rate_45RH_Sales;
        /// <summary>
        ///Rate_45RH_Sales
        /// </summary>
        public decimal? Rate_45RH_Sales
        {
            get
            {
                return rate_45RH_Sales == null ? 0 : rate_45RH_Sales;
            }
            set
            {
                rate_45RH_Sales = value;
            }
        }
        #endregion

        #region Rate_45HQ_Sales
        decimal? rate_45HQ_Sales;
        /// <summary>
        ///Rate_45HQ_Sales
        /// </summary>
        public decimal? Rate_45HQ_Sales
        {
            get
            {
                return rate_45HQ_Sales == null ? 0 : rate_45HQ_Sales;
            }
            set
            {
                rate_45HQ_Sales = value;
            }
        }
        #endregion

        #region Rate_20HT_Sales
        decimal? rate_20HT_Sales;
        /// <summary>
        ///Rate_20HT_Sales
        /// </summary>
        public decimal? Rate_20HT_Sales
        {
            get
            {
                return rate_20HT_Sales == null ? 0 : rate_20HT_Sales;
            }
            set
            {
                rate_20HT_Sales = value;
            }
        }
        #endregion

        #region Rate_40HQ_Sales
        decimal? rate_40HQ_Sales;
        /// <summary>
        ///Rate_40HQ_Sales
        /// </summary>
        public decimal? Rate_40HQ_Sales
        {
            get
            {
                return rate_40HQ_Sales == null ? 0 : rate_40HQ_Sales;
            }
            set
            {
                rate_40HQ_Sales = value;
            }
        }
        #endregion

        #region Rate_53HQ_Sales
        decimal? rate_53HQ_Sales;
        /// <summary>
        ///Rate_53HQ_Sales
        /// </summary>
        public decimal? Rate_53HQ_Sales
        {
            get
            {
                return rate_53HQ_Sales == null ? 0 : rate_53HQ_Sales;
            }
            set
            {
                rate_53HQ_Sales = value;
            }
        }
        #endregion
        /// <summary>
        /// 排序
        /// </summary>
        public decimal? OrdreRate { get; set; }
        #endregion

        /// <summary>
        /// 详细运价明细
        /// </summary>        
        public List<FrmUnitRateInfo> UnitList { get; set; }
    }

    #region SearchPortList
    /// <summary>
    /// 港口列表
    /// </summary>
    [Serializable]
    public class SearchPortList
    {
        /// <summary>
        /// 港口代码
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 港口名称
        /// </summary>
        public String PortName { get; set; }
        /// <summary>
        /// 港口类型
        /// </summary>
        public PortType Porttype { get; set; }
    }
    #endregion

    #region 明细
    /// <summary>
    /// 查询海运运价中BaseInfo
    /// </summary>
    [Serializable]
    [KnownType(typeof(FrmUnitRateInfo))]
    public class SearchOceanBaseInfo : BaseDataObject
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// OceanID
        /// </summary>
        public Guid OceanID { get; set; }


        /// <summary>
        /// Type
        /// </summary>
        public SearchRateType Type { get; set; }

        /// <summary>
        /// Base Port Number
        /// </summary>
        public string BasePortNumber { get; set; }

        /// <summary>
        /// Origin Arb Number
        /// </summary>
        public string OriginArbNumber { get; set; }

        /// <summary>
        /// Dest Arb Number
        /// </summary>
        public string DestArbNumber { get; set; }

        /// <summary>
        /// POL
        /// </summary>
        public string POL { get; set; }
        /// <summary>
        /// VIA
        /// </summary>
        public string VIA { get; set; }

        /// <summary>
        /// POD
        /// </summary>
        public string POD { get; set; }

        /// <summary>
        /// Delivery
        /// </summary>
        public string Delivery { get; set; }

        /// <summary>
        /// Contract No
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// CLS
        /// </summary>
        public string CLS { get; set; }

        /// <summary>
        /// Surcharge
        /// </summary>
        public string Surcharge { get; set; }

        /// <summary>
        /// T/T
        /// </summary>
        public string TT { get; set; }

        /// <summary>
        /// Term
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Updater
        /// </summary>
        public string Updater { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Carrier
        /// </summary>
        public string Carrier { get; set; }

        /// <summary>
        /// ItemCode
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// Commodity
        /// </summary>
        public string Commodity { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// SearchOceanUnitList
        /// </summary>
        public List<FrmUnitRateInfo> UnitList { get; set; }

        /// <summary>
        /// Remark Details
        /// </summary>
        public string RemarkDetails { get; set; }

        /// <summary>
        /// 文件数量
        /// </summary>
        public int FilesCount { get; set; }
    }

    /// <summary>
    /// 箱型信息
    /// </summary>
    [Serializable]
    public class SearchOceanContainerRates : BaseDataObject
    {
        /// <summary>
        /// 箱型
        /// </summary>
        public string ContainerType { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }


    }

    /// <summary>
    /// 合约详细信息
    /// </summary>
    [Serializable]
    public class SearchOceanContractInfo : BaseDataObject
    {
        public Guid ID { get; set; }

        public string Carrier { get; set; }

        public string ContractNo { get; set; }

        public List<SCNInfo> SCNList { get; set; }


        public string ShiplineName { get; set; }

        public string AccountType { get; set; }

        public string Account { get; set; }
    }
    [Serializable]
    public class SCNInfo
    {
        public string ShipperName { get; set; }

        public string ConsigneeName { get; set; }

        public string NotifyName { get; set; }
    }
    #endregion

    #endregion

    #region Air

    /// <summary>
    /// SearchAirRateList
    /// </summary>
    [Serializable]
    [KnownType(typeof(FrmUnitRateList))]
    public class SearchAirRateList : BaseDataObject
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 运价状态(修改为在服务端中直接返回)
        /// </summary>
        public SearchPriceStatus Statue { get; set; }

        /// <summary>
        /// CarrierName
        /// </summary>
        public string CarrierName { get; set; }
        /// <summary>
        /// From
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// To
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Commodity
        /// </summary>
        public string Commodity { get; set; }
        /// <summary>
        /// Schedule
        /// </summary>
        public string Schedule { get; set; }
        /// <summary>
        /// Routing
        /// </summary>
        public string Routing { get; set; }
        /// <summary>
        /// DurationFrom
        /// </summary>
        public DateTime DurationFrom { get; set; }
        /// <summary>
        /// DurationTo
        /// </summary>
        public DateTime DurationTo { get; set; }

        /// <summary>
        /// 详细运价明细
        /// </summary>
        public List<FrmUnitRateList> UnitList { get; set; }

        /// <summary>
        /// RespondBy
        /// </summary>
        public string RespondBy { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }

    }

    #endregion

    #region Truck

    /// <summary>
    /// SearchAirRateList
    /// </summary>
    [Serializable]
    public class SearchTruckRateList : BaseDataObject
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 运价状态(修改为在服务端中直接返回)
        /// </summary>
        public SearchPriceStatus Statue { get; set; }

        /// <summary>
        /// CarrierName
        /// </summary>
        public string CarrierName { get; set; }
        /// <summary>
        /// From
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// To
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
        public decimal? Rate { get; set; }
        /// <summary>
        /// FUEL
        /// </summary>
        public decimal? FUEL { get; set; }
        /// <summary>
        /// Total
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// ZipCode
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// DurationFrom
        /// </summary>
        public DateTime? DurationFrom { get; set; }
        /// <summary>
        /// DurationTo
        /// </summary>
        public DateTime? DurationTo { get; set; }

        /// <summary>
        /// RespondBy
        /// </summary>
        public string RespondBy { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }

    }

    #endregion
}
