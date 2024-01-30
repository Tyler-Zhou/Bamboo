using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects
{
    /// <summary>
    /// 合约信息数据对象
    /// </summary>
    [Serializable]
    public partial class FreightList : BaseDataObject
    {
        #region ID
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
        #endregion

        #region 合约ID
        Guid _OceanId;
        /// <summary>
        /// 合约ID
        /// </summary>
        public Guid OceanId
        {
            get
            {
                return _OceanId;
            }
            set
            {
                if (_OceanId != value)
                {
                    _OceanId = value;
                    base.OnPropertyChanged("OceanId", value);
                }
            }
        }
        #endregion

        #region 船公司
        string carrier;
        /// <summary>
        /// 船公司
        /// </summary>
        public string Carrier
        {
            get { return carrier; }
            set
            {
                carrier = value;
                base.OnPropertyChanged("Carrier", value);
            }
        }
        #endregion

        #region 承运人
        string transport;
        /// <summary>
        /// 承运人
        /// </summary>
        public string Transport
        {
            get { return transport; }
            set
            {
                transport = value;
                base.OnPropertyChanged("Transport", value);
            }
        }
        #endregion

        #region 合约号
        string freightNo;
        /// <summary>
        /// 合约号
        /// </summary>
        public string FreightNo
        {
            get { return freightNo; }
            set
            {
                freightNo = value;
                base.OnPropertyChanged("FreightNo", value);
            }
        }
        #endregion

        #region 装货港
        string pol;
        /// <summary>
        /// 装货港
        /// </summary>
        public string Pol
        {
            get { return pol; }
            set
            {
                pol = value;
                base.OnPropertyChanged("Pol", value);
            }
        }
        #endregion

        #region 卸货港
        string pod;
        /// <summary>
        /// 卸货港
        /// </summary>
        public string Pod
        {
            get { return pod; }
            set
            {
                pod = value;
                base.OnPropertyChanged("Pod", value);
            }
        }
        #endregion

        #region 交货地
        /// <summary>
        /// 交货地
        /// </summary>
        public string DeliveryName
        {
            get;
            set;
        }
        #endregion

        #region 目的地
        string destination;
        /// <summary>
        /// 目的港
        /// </summary>
        public string Destination
        {
            get { return destination; }
            set
            {
                destination = value;
                base.OnPropertyChanged("Destination", value);
            }
        }
        #endregion

        DateTime? beginDate;
        /// <summary>
        /// 有效开始日期
        /// </summary>
        public DateTime? BeginDate
        {
            get { return beginDate; }
            set
            {
                beginDate = value;
                base.OnPropertyChanged("BeginDate", value);
            }
        }
        DateTime? endDate;
        /// <summary>
        /// 有效结束日期
        /// </summary>
        public DateTime? EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                base.OnPropertyChanged("EndDate", value);
            }
        }

        string shipper;
        /// <summary>
        /// 发货人
        /// </summary>
        public string Shipper
        {
            get { return shipper; }
            set
            {
                shipper = value;
                base.OnPropertyChanged("Shipper", value);
            }
        }

        string consignee;
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee
        {
            get { return consignee; }
            set
            {
                consignee = value;
                base.OnPropertyChanged("Consignee", value);
            }
        }

        string goods;
        /// <summary>
        /// 品名
        /// </summary>
        public string Goods
        {
            get { return goods; }
            set
            {
                goods = value;
                base.OnPropertyChanged("Goods", value);
            }
        }

        string additionalCharges;
        /// <summary>
        /// 附加费
        /// </summary>
        public string AdditionalCharges
        {
            get { return additionalCharges; }
            set
            {
                additionalCharges = value;
                base.OnPropertyChanged("AdditionalCharges", value);
            }
        }

        string notifyPart;
        /// <summary>
        /// 通知人
        /// </summary>
        public string NotifyPart
        {
            get { return notifyPart; }
            set
            {
                notifyPart = value;
                base.OnPropertyChanged("NotifyPart", value);
            }
        }

        string paymentTreaty;
        /// <summary>
        /// 付款条约
        /// </summary>
        public string PaymentTreaty
        {
            get { return paymentTreaty; }
            set
            {
                paymentTreaty = value;
                base.OnPropertyChanged("PaymentTreaty", value);
            }
        }

        DateTime? closingDate;
        /// <summary>
        /// 截关日
        /// </summary>
        public DateTime? ClosingDate
        {
            get { return closingDate; }
            set
            {
                closingDate = value;
                base.OnPropertyChanged("ClosingDate", value);
            }
        }

        string transitClause;
        /// <summary>
        /// 运输条款
        /// </summary>
        public string TransitClause
        {
            get { return transitClause; }
            set
            {
                transitClause = value;
                base.OnPropertyChanged("TransitClause", value);
            }
        }

        /// <summary>
        /// Term
        /// </summary>
        public string Term
        {
            get;
            set;
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
                remark = value;
                base.OnPropertyChanged("Remark", value);
            }
        }

        #region Rates

        #region Rate_45FR
         Decimal? rate_45FR;
        /// <summary>
        ///Rate_45FR
        /// </summary>
        public  Decimal? Rate_45FR
        {
            get
            {
                return rate_45FR;
            }
            set
            {
                rate_45FR = value;
            }

        }
        #endregion

        #region Rate_40RF
         Decimal? rate_40RF;
        /// <summary>
        ///Rate_40RF
        /// </summary>
        public  Decimal? Rate_40RF
        {
            get
            {
                return rate_40RF ;
            }
            set
            {
                rate_40RF = value;
            }

        }
        #endregion

        #region Rate_45HT
         Decimal? rate_45HT;
        /// <summary>
        ///Rate_45HT
        /// </summary>
        public  Decimal? Rate_45HT
        {
            get
            {
                return rate_45HT ;
            }
            set
            {
                rate_45HT = value;
            }
        }
        #endregion

        #region Rate_20RF
         Decimal? rate_20RF;
        /// <summary>
        ///Rate_20RF
        /// </summary>
        public  Decimal? Rate_20RF
        {
            get
            {
                return rate_20RF;
            }
            set
            {
                rate_20RF = value;
            }
        }
        #endregion

        #region Rate_20HQ
         Decimal? rate_20HQ;
        /// <summary>
        ///Rate_20HQ
        /// </summary>
        public  Decimal? Rate_20HQ
        {
            get
            {
                return rate_20HQ ;
            }
            set
            {
                rate_20HQ = value;
            }
        }
        #endregion

        #region Rate_20TK
         Decimal? rate_20TK;
        /// <summary>
        ///Rate_20TK
        /// </summary>
        public  Decimal? Rate_20TK
        {
            get
            {
                return rate_20TK;
            }
            set
            {
                rate_20TK = value;
            }
        }
        #endregion

        #region Rate_20GP
         Decimal? rate_20GP;
        /// <summary>
        ///Rate_20GP
        /// </summary>
        public  Decimal? Rate_20GP
        {
            get
            {
                return rate_20GP;
            }
            set
            {
                rate_20GP = value;
            }
        }
        #endregion

        #region Rate_40TK
         Decimal? rate_40TK;
        /// <summary>
        ///Rate_40TK
        /// </summary>
        public  Decimal? Rate_40TK
        {
            get
            {
                return rate_40TK ;
            }
            set
            {
                rate_40TK = value;
            }
        }
        #endregion

        #region Rate_40OT
         Decimal? rate_40OT;
        /// <summary>
        ///Rate_40OT
        /// </summary>
        public  Decimal? Rate_40OT
        {
            get
            {
                return rate_40OT;
            }
            set
            {
                rate_40OT = value;
            }
        }
        #endregion

        #region Rate_20FR
        Decimal? rate_20FR;
        /// <summary>
        ///Rate_20FR
        /// </summary>
        public Decimal? Rate_20FR
        {
            get
            {
                return rate_20FR;
            }
            set
            {
                rate_20FR = value;
            }
        }
        #endregion

        #region Rate_45GP
        Decimal? rate_45GP;
        /// <summary>
        ///Rate_45GP
        /// </summary>
        public Decimal? Rate_45GP
        {
            get
            {
                return rate_45GP;
            }
            set
            {
                rate_45GP = value;
            }
        }
        #endregion

        #region Rate_40GP
        Decimal? rate_40GP;
        /// <summary>
        ///Rate_40GP
        /// </summary>
        public Decimal? Rate_40GP
        {
            get
            {
                return rate_40GP;
            }
            set
            {
                rate_40GP = value;
            }
        }
        #endregion

        #region Rate_45RF
        Decimal? rate_45RF;
        /// <summary>
        ///Rate_45RF
        /// </summary>
        public Decimal? Rate_45RF
        {
            get
            {
                return rate_45RF ;
            }
            set
            {
                rate_45RF = value;
            }
        }
        #endregion

        #region Rate_20RH
        Decimal? rate_20RH;
        /// <summary>
        ///Rate_20RH
        /// </summary>
        public Decimal? Rate_20RH
        {
            get
            {
                return rate_20RH;
            }
            set
            {
                rate_20RH = value;
            }
        }
        #endregion

        #region Rate_45OT
        Decimal? rate_45OT;
        /// <summary>
        ///Rate_45OT
        /// </summary>
        public Decimal? Rate_45OT
        {
            get
            {
                return rate_45OT ;
            }
            set
            {
                rate_45OT = value;
            }
        }
        #endregion

        #region Rate_40NOR
         Decimal? rate_40NOR;
        /// <summary>
        ///Rate_40NOR
        /// </summary>
        public  Decimal? Rate_40NOR
        {
            get
            {
                return rate_40NOR;
            }
            set
            {
                rate_40NOR = value;
            }
        }
        #endregion

        #region Rate_40FR
         Decimal? rate_40FR;
        /// <summary>
        ///Rate_40FR
        /// </summary>
        public  Decimal? Rate_40FR
        {
            get
            {
                return rate_40FR ;
            }
            set
            {
                rate_40FR = value;
            }
        }
        #endregion

        #region Rate_20OT
         Decimal? rate_20OT;
        /// <summary>
        ///Rate_20OT
        /// </summary>
        public  Decimal? Rate_20OT
        {
            get
            {
                return rate_20OT ;
            }
            set
            {
                rate_20OT = value;
            }
        }
        #endregion

        #region Rate_45TK
         Decimal? rate_45TK;
        /// <summary>
        ///Rate_45TK
        /// </summary>
        public  Decimal? Rate_45TK
        {
            get
            {
                return rate_45TK;
            }
            set
            {
                rate_45TK = value;
            }
        }
        #endregion

        #region Rate_20NOR
         Decimal? rate_20NOR;
        /// <summary>
        ///Rate_20NOR
        /// </summary>
        public  Decimal? Rate_20NOR
        {
            get
            {
                return rate_20NOR;
            }
            set
            {
                rate_20NOR = value;
            }
        }
        #endregion

        #region Rate_40HT
         Decimal? rate_40HT;
        /// <summary>
        ///Rate_40HT
        /// </summary>
        public  Decimal? Rate_40HT
        {
            get
            {
                return rate_40HT;
            }
            set
            {
                rate_40HT = value;
            }
        }
        #endregion

        #region Rate_40RH
         Decimal? rate_40RH;
        /// <summary>
        ///Rate_40RH
        /// </summary>
        public  Decimal? Rate_40RH
        {
            get
            {
                return rate_40RH ;
            }
            set
            {
                rate_40RH = value;
            }
        }
        #endregion

        #region Rate_45RH
         Decimal? rate_45RH;
        /// <summary>
        ///Rate_45RH
        /// </summary>
        public  Decimal? Rate_45RH
        {
            get
            {
                return rate_45RH;
            }
            set
            {
                rate_45RH = value;
            }
        }
        #endregion

        #region Rate_45HQ
         Decimal? rate_45HQ;
        /// <summary>
        ///Rate_45HQ
        /// </summary>
        public  Decimal? Rate_45HQ
        {
            get
            {
                return rate_45HQ;
            }
            set
            {
                rate_45HQ = value;
            }
        }
        #endregion

        #region Rate_20HT
         Decimal? rate_20HT;
        /// <summary>
        ///Rate_20HT
        /// </summary>
        public  Decimal? Rate_20HT
        {
            get
            {
                return rate_20HT ;
            }
            set
            {
                rate_20HT = value;
            }
        }
        #endregion

        #region Rate_40HQ
         Decimal? rate_40HQ;
        /// <summary>
        ///Rate_40HQ
        /// </summary>
        public  Decimal? Rate_40HQ
        {
            get
            {
                return rate_40HQ ;
            }
            set
            {
                rate_40HQ = value;
            }
        }
        #endregion

        public  Decimal? Rate_53HQ
        {
            get;
            set;
        }
        #endregion
    }


    [Serializable]
    public class FreightDataList : BaseDataObject
    {
        public List<FreightList> DataList { get; set; }
        public List<String> UnitList { get; set; }
    }



    /// <summary>
    /// FrmUnitRateList
    /// </summary>
    [Serializable]
    public class FrmUnitRateList : BaseDataObject
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

        Guid _parentID;
        /// <summary>
        /// 运价ID
        /// </summary>
        public Guid ParentID
        {
            get
            {
                return _parentID;
            }
            set
            {
                if (_parentID != value)
                {
                    _parentID = value;
                    base.OnPropertyChanged("ParentID", value);
                }
            }
        }

        Guid _unitid;
        /// <summary>
        /// 运价ID
        /// </summary>
        public Guid UnitID
        {
            get
            {
                return _unitid;
            }
            set
            {
                if (_unitid != value)
                {
                    _unitid = value;
                    base.OnPropertyChanged("UnitID", value);
                }
            }
        }

        string _unitname;
        /// <summary>
        /// 运价单位名
        /// </summary>
        public string UnitName
        {
            get
            {
                return _unitname;
            }
            set
            {
                if (_unitname != value)
                {
                    _unitname = value;
                    base.OnPropertyChanged("UnitName", value);
                }
            }
        }

        decimal _rate;
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Rate
        {
            get
            {
                return _rate;
            }
            set
            {
                if (_rate != value)
                {
                    _rate = value;
                    base.OnPropertyChanged("Rate", value);
                }
            }
        }
    }

}
