using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common; 
using ICP.Framework.CommonLibrary.Helper;
using System.Runtime.Serialization;

namespace ICP.FRM.ServiceInterface.DataObjects
{
    #region 客户端数据源
    /// <summary>
    /// 数据处理基类
    /// 为提高系统性能而特意将字段公用，请勿在数据访问以外的场合使用带"_"开头的字段
    /// </summary>
    [Serializable]
    public class ClientContainerRateObject : BaseDataObject
    {
        #region Rates
        #region Rate_45FR
       public decimal? _Rate_45FR;
        /// <summary>
        ///Rate_45FR
        /// </summary>
        public decimal? Rate_45FR
        {
            get { return _Rate_45FR; }
            set
            {
                if (_Rate_45FR != value)
                {
                    _Rate_45FR = value;
                    this.NotifyPropertyChanged(o => o.Rate_45FR);
                }
            }
        }
        #endregion

        #region Rate_40RF
        public decimal? _Rate_40RF;
        /// <summary>
        ///Rate_40RF
        /// </summary>
        public decimal? Rate_40RF
        {
            get { return _Rate_40RF; }
            set
            {
                if (_Rate_40RF != value)
                {
                    _Rate_40RF = value;
                    this.NotifyPropertyChanged(o => o.Rate_40RF);
                }
            }
        }
        #endregion

        #region Rate_45HT
        public decimal? _Rate_45HT;
        /// <summary>
        ///Rate_45HT
        /// </summary>
        public decimal? Rate_45HT
        {
            get { return _Rate_45HT; }
            set
            {
                if (_Rate_45HT != value)
                {
                    _Rate_45HT = value;
                    this.NotifyPropertyChanged(o => o.Rate_45HT);
                }
            }
        }
        #endregion

        #region Rate_20RF
        public decimal? _Rate_20RF;
        /// <summary>
        ///Rate_20RF
        /// </summary>
        public decimal? Rate_20RF
        {
            get { return _Rate_20RF; }
            set
            {
                if (_Rate_20RF != value)
                {
                    _Rate_20RF = value;
                    this.NotifyPropertyChanged(o => o.Rate_20RF);
                }
            }
        }
        #endregion

        #region Rate_20HQ
        public decimal? _Rate_20HQ;
        /// <summary>
        ///Rate_20HQ
        /// </summary>
        public decimal? Rate_20HQ
        {
            get { return _Rate_20HQ; }
            set
            {
                if (_Rate_20HQ != value)
                {
                    _Rate_20HQ = value;
                    this.NotifyPropertyChanged(o => o.Rate_20HQ);
                }
            }
        }
        #endregion

        #region Rate_20TK
        public decimal? _Rate_20TK;
        /// <summary>
        ///Rate_20TK
        /// </summary>
        public decimal? Rate_20TK
        {
            get { return _Rate_20TK; }
            set
            {
                if (_Rate_20TK != value)
                {
                    _Rate_20TK = value;
                    this.NotifyPropertyChanged(o => o.Rate_20TK);
                }
            }
        }
        #endregion

        #region Rate_20GP
        public decimal? _Rate_20GP;
        /// <summary>
        ///Rate_20GP
        /// </summary>
        public decimal? Rate_20GP
        {
            get { return _Rate_20GP; }
            set
            {
                if (_Rate_20GP != value)
                {
                    _Rate_20GP = value;
                    this.NotifyPropertyChanged(o => o.Rate_20GP);
                }
            }
        }
        #endregion

        #region Rate_40TK
        public decimal? _Rate_40TK;
        /// <summary>
        ///Rate_40TK
        /// </summary>
        public decimal? Rate_40TK
        {
            get { return _Rate_40TK; }
            set
            {
                if (_Rate_40TK != value)
                {
                    _Rate_40TK = value;
                    this.NotifyPropertyChanged(o => o.Rate_40TK);
                }
            }
        }
        #endregion

        #region Rate_40OT
        public decimal? _Rate_40OT;
        /// <summary>
        ///Rate_40OT
        /// </summary>
        public decimal? Rate_40OT
        {
            get { return _Rate_40OT; }
            set
            {
                if (_Rate_40OT != value)
                {
                    _Rate_40OT = value;
                    this.NotifyPropertyChanged(o => o.Rate_40OT);
                }
            }
        }
        #endregion

        #region Rate_20FR
        public decimal? _Rate_20FR;
        /// <summary>
        ///Rate_20FR
        /// </summary>
        public decimal? Rate_20FR
        {
            get { return _Rate_20FR; }
            set
            {
                if (_Rate_20FR != value)
                {
                    _Rate_20FR = value;
                    this.NotifyPropertyChanged(o => o.Rate_20FR);
                }
            }
        }
        #endregion

        #region Rate_45GP
        public decimal? _Rate_45GP;
        /// <summary>
        ///Rate_45GP
        /// </summary>
        public decimal? Rate_45GP
        {
            get { return _Rate_45GP; }
            set
            {
                if (_Rate_45GP != value)
                {
                    _Rate_45GP = value;
                    this.NotifyPropertyChanged(o => o.Rate_45GP);
                }
            }
        }
        #endregion

        #region Rate_40GP
        public decimal? _Rate_40GP;
        /// <summary>
        ///Rate_40GP
        /// </summary>
        public decimal? Rate_40GP
        {
            get { return _Rate_40GP; }
            set
            {
                if (_Rate_40GP != value)
                {
                    _Rate_40GP = value;
                    this.NotifyPropertyChanged(o => o.Rate_40GP);
                }
            }
        }
        #endregion

        #region Rate_45RF
        public decimal? _Rate_45RF;
        /// <summary>
        ///Rate_45RF
        /// </summary>
        public decimal? Rate_45RF
        {
            get { return _Rate_45RF; }
            set
            {
                if (_Rate_45RF != value)
                {
                    _Rate_45RF = value;
                    this.NotifyPropertyChanged(o => o.Rate_45RF);
                }
            }
        }
        #endregion

        #region Rate_20RH
        public decimal? _Rate_20RH;
        /// <summary>
        ///Rate_20RH
        /// </summary>
        public decimal? Rate_20RH
        {
            get { return _Rate_20RH; }
            set
            {
                if (_Rate_20RH != value)
                {
                    _Rate_20RH = value;
                    this.NotifyPropertyChanged(o => o.Rate_20RH);
                }
            }
        }
        #endregion

        #region Rate_45OT
        public decimal? _Rate_45OT;
        /// <summary>
        ///Rate_45OT
        /// </summary>
        public decimal? Rate_45OT
        {
            get { return _Rate_45OT; }
            set
            {
                if (_Rate_45OT != value)
                {
                    _Rate_45OT = value;
                    this.NotifyPropertyChanged(o => o.Rate_45OT);
                }
            }
        }
        #endregion

        #region Rate_40NOR
        public decimal? _Rate_40NOR;
        /// <summary>
        ///Rate_40NOR
        /// </summary>
        public decimal? Rate_40NOR
        {
            get { return _Rate_40NOR; }
            set
            {
                if (_Rate_40NOR != value)
                {
                    _Rate_40NOR = value;
                    this.NotifyPropertyChanged(o => o.Rate_40NOR);
                }
            }
        }
        #endregion

        #region Rate_40FR
        public decimal? _Rate_40FR;
        /// <summary>
        ///Rate_40FR
        /// </summary>
        public decimal? Rate_40FR
        {
            get { return _Rate_40FR; }
            set
            {
                if (_Rate_40FR != value)
                {
                    _Rate_40FR = value;
                    this.NotifyPropertyChanged(o => o.Rate_40FR);
                }
            }
        }
        #endregion

        #region Rate_20OT
        public decimal? _Rate_20OT;
        /// <summary>
        ///Rate_20OT
        /// </summary>
        public decimal? Rate_20OT
        {
            get { return _Rate_20OT; }
            set
            {
                if (_Rate_20OT != value)
                {
                    _Rate_20OT = value;
                    this.NotifyPropertyChanged(o => o.Rate_20OT);
                }
            }
        }
        #endregion

        #region Rate_45TK
        public decimal? _Rate_45TK;
        /// <summary>
        ///Rate_45TK
        /// </summary>
        public decimal? Rate_45TK
        {
            get { return _Rate_45TK; }
            set
            {
                if (_Rate_45TK != value)
                {
                    _Rate_45TK = value;
                    this.NotifyPropertyChanged(o => o.Rate_45TK);
                }
            }
        }
        #endregion

        #region Rate_20NOR
        public decimal? _Rate_20NOR;
        /// <summary>
        ///Rate_20NOR
        /// </summary>
        public decimal? Rate_20NOR
        {
            get { return _Rate_20NOR; }
            set
            {
                if (_Rate_20NOR != value)
                {
                    _Rate_20NOR = value;
                    this.NotifyPropertyChanged(o => o.Rate_20NOR);
                }
            }
        }
        #endregion

        #region Rate_40HT
        public decimal? _Rate_40HT;
        /// <summary>
        ///Rate_40HT
        /// </summary>
        public decimal? Rate_40HT
        {
            get { return _Rate_40HT; }
            set
            {
                if (_Rate_40HT != value)
                {
                    _Rate_40HT = value;
                    this.NotifyPropertyChanged(o => o.Rate_40HT);
                }
            }
        }
        #endregion

        #region Rate_40RH
        public decimal? _Rate_40RH;
        /// <summary>
        ///Rate_40RH
        /// </summary>
        public decimal? Rate_40RH
        {
            get { return _Rate_40RH; }
            set
            {
                if (_Rate_40RH != value)
                {
                    _Rate_40RH = value;
                    this.NotifyPropertyChanged(o => o.Rate_40RH);
                }
            }
        }
        #endregion

        #region Rate_45RH
        public decimal? _Rate_45RH;
        /// <summary>
        ///Rate_45RH
        /// </summary>
        public decimal? Rate_45RH
        {
            get { return _Rate_45RH; }
            set
            {
                if (_Rate_45RH != value)
                {
                    _Rate_45RH = value;
                    this.NotifyPropertyChanged(o => o.Rate_45RH);
                }
            }
        }
        #endregion

        #region Rate_45HQ
        public decimal? _Rate_45HQ;
        /// <summary>
        ///Rate_45HQ
        /// </summary>
        public decimal? Rate_45HQ
        {
            get { return _Rate_45HQ; }
            set
            {
                if (_Rate_45HQ != value)
                {
                    _Rate_45HQ = value;
                    this.NotifyPropertyChanged(o => o.Rate_45HQ);
                }
            }
        }
        #endregion

        #region Rate_20HT
        public decimal? _Rate_20HT;
        /// <summary>
        ///Rate_20HT
        /// </summary>
        public decimal? Rate_20HT
        {
            get { return _Rate_20HT; }
            set
            {
                if (_Rate_20HT != value)
                {
                    _Rate_20HT = value;
                    this.NotifyPropertyChanged(o => o.Rate_20HT);
                }
            }
        }
        #endregion

        #region Rate_40HQ
        public decimal? _Rate_40HQ;
        /// <summary>
        ///Rate_40HQ
        /// </summary>
        public decimal? Rate_40HQ
        {
            get { return _Rate_40HQ; }
            set
            {
                if (_Rate_40HQ != value)
                {
                    _Rate_40HQ = value;
                    this.NotifyPropertyChanged(o => o.Rate_40HQ);
                }
            }
        }
        #endregion

        #region Rate_53HQ
        public decimal? _Rate_53HQ;
        /// <summary>
        ///Rate_53HQ
        /// </summary>
        public decimal? Rate_53HQ
        {
            get { return _Rate_53HQ; }
            set
            {
                if (_Rate_53HQ != value)
                {
                    _Rate_53HQ = value;
                    this.NotifyPropertyChanged(o => o.Rate_53HQ);
                }
            }
        }
        #endregion
        #endregion

        #region 方法

        /// <summary>
        /// 把所有箱属性置为空
        /// </summary>
        public void SetALLRateNull()
        {
            this.Rate_45FR = null;
            this.Rate_40RF = null;
            this.Rate_45HT = null;
            this.Rate_20RF = null;
            this.Rate_20HQ = null;
            this.Rate_20TK = null;
            this.Rate_20GP = null;
            this.Rate_40TK = null;
            this.Rate_40OT = null;
            this.Rate_20FR = null;
            this.Rate_45GP = null;
            this.Rate_40GP = null;
            this.Rate_45RF = null;
            this.Rate_20RH = null;
            this.Rate_45OT = null;
            this.Rate_40NOR = null;
            this.Rate_40FR = null;
            this.Rate_20OT = null;
            this.Rate_45TK = null;
            this.Rate_20NOR = null;
            this.Rate_40HT = null;
            this.Rate_40RH = null;
            this.Rate_45RH = null;
            this.Rate_45HQ = null;
            this.Rate_20HT = null;
            this.Rate_40HQ = null;
            this.Rate_53HQ = null;
        }

        /// <summary>
        /// 把所有不需要用到的箱属性置为空,用到的设为0
        /// </summary>
        /// <param name="units">根据一个OceanUnitList列表生成</param>
        public void BulidRateToZeroByOceanUints(List<OceanUnitList> units)
        {
            this.SetALLRateNull();
            foreach (var item in units)
            {
                switch (item.UnitName)
                {
                    case "45FR": this.Rate_45FR = 0; break;
                    case "40RF": this.Rate_40RF = 0; break;
                    case "45HT": this.Rate_45HT = 0; break;
                    case "20RF": this.Rate_20RF = 0; break;
                    case "20HQ": this.Rate_20HQ = 0; break;
                    case "20TK": this.Rate_20TK = 0; break;
                    case "20GP": this.Rate_20GP = 0; break;
                    case "40TK": this.Rate_40TK = 0; break;
                    case "40OT": this.Rate_40OT = 0; break;
                    case "20FR": this.Rate_20FR = 0; break;
                    case "45GP": this.Rate_45GP = 0; break;
                    case "40GP": this.Rate_40GP = 0; break;
                    case "45RF": this.Rate_45RF = 0; break;
                    case "20RH": this.Rate_20RH = 0; break;
                    case "45OT": this.Rate_45OT = 0; break;
                    case "40NOR": this.Rate_40NOR = 0; break;
                    case "40FR": this.Rate_40FR = 0; break;
                    case "20OT": this.Rate_20OT = 0; break;
                    case "45TK": this.Rate_45TK = 0; break;
                    case "20NOR": this.Rate_20NOR = 0; break;
                    case "40HT": this.Rate_40HT = 0; break;
                    case "40RH": this.Rate_40RH = 0; break;
                    case "45RH": this.Rate_45RH = 0; break;
                    case "45HQ": this.Rate_45HQ = 0; break;
                    case "20HT": this.Rate_20HT = 0; break;
                    case "40HQ": this.Rate_40HQ = 0; break;
                    case "53HQ": this.Rate_53HQ = 0; break;

                }
            }
        }

        /// <summary>
        /// 验证所有运费.如果所有运费都为0 返回False
        /// </summary>
        /// <returns></returns>
        public bool ValidateHasRate()
        {
            if (this.Rate_45FR.IsNullOrZero() == false) return true;
            if (this.Rate_40RF.IsNullOrZero() == false) return true;
            if (this.Rate_45HT.IsNullOrZero() == false) return true;
            if (this.Rate_20RF.IsNullOrZero() == false) return true;
            if (this.Rate_20HQ.IsNullOrZero() == false) return true;
            if (this.Rate_20TK.IsNullOrZero() == false) return true;
            if (this.Rate_20GP.IsNullOrZero() == false) return true;
            if (this.Rate_40TK.IsNullOrZero() == false) return true;
            if (this.Rate_40OT.IsNullOrZero() == false) return true;
            if (this.Rate_20FR.IsNullOrZero() == false) return true;
            if (this.Rate_45GP.IsNullOrZero() == false) return true;
            if (this.Rate_40GP.IsNullOrZero() == false) return true;
            if (this.Rate_45RF.IsNullOrZero() == false) return true;
            if (this.Rate_20RH.IsNullOrZero() == false) return true;
            if (this.Rate_45OT.IsNullOrZero() == false) return true;
            if (this.Rate_40NOR.IsNullOrZero() == false) return true;
            if (this.Rate_40FR.IsNullOrZero() == false) return true;
            if (this.Rate_20OT.IsNullOrZero() == false) return true;
            if (this.Rate_45TK.IsNullOrZero() == false) return true;
            if (this.Rate_20NOR.IsNullOrZero() == false) return true;
            if (this.Rate_40HT.IsNullOrZero() == false) return true;
            if (this.Rate_40RH.IsNullOrZero() == false) return true;
            if (this.Rate_45RH.IsNullOrZero() == false) return true;
            if (this.Rate_45HQ.IsNullOrZero() == false) return true;
            if (this.Rate_20HT.IsNullOrZero() == false) return true;
            if (this.Rate_40HQ.IsNullOrZero() == false) return true;
            if (this.Rate_53HQ.IsNullOrZero() == false) return true;
            return false;
        }

        #endregion
    }
    /// <summary>
    /// BasePort数据
    /// 为提高系统性能而特意将字段公用，请勿在数据访问以外的场合使用带"_"开头的字段
    /// </summary>
    [Serializable]
    public class ClientBasePortList : ClientContainerRateObject
    {
        #region 属性

        public override bool IsNew { get { return ID.IsNullOrEmpty(); } }
        public string ErrorInfo { get; set; }

        public bool HasError
        {
            get { return !ErrorInfo.IsNullOrEmpty(); }
        }

        public bool IsMax
        {
            get;
            set;
        }



        public ChangeState ChangeState
        {
            get
            {
                if (IsNew) return ChangeState.New;
                else if (IsDirty) return ChangeState.Changed;
                else return ChangeState.None;
            }
        }

        /// <summary>
        /// 行数(导入时使用)
        /// </summary>
        public int RowIndex
        {
            get;
            set;
        }

        #region ID
        public Guid _ID;
        /// <summary>
        ///ID
        /// </summary>
        public Guid ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }
        #endregion

        #region OceanID
        public Guid? _OceanID;
        /// <summary>
        ///OceanID
        /// </summary>
        public Guid? OceanID
        {
            get { return _OceanID; }
            set
            {
                if (_OceanID != value)
                {
                    _OceanID = value;
                    this.NotifyPropertyChanged(o => o.OceanID);
                }
            }
        }
        #endregion

        #region Account
        public string _Account;
        /// <summary>
        ///Accounts
        /// </summary>
        [StringLength(CMessage="大客户",EMessage="Account")]
        public string Account
        {
            get { return _Account; }
            set
            {
                if (_Account != value)
                {
                    _Account = value;
                    this.NotifyPropertyChanged(o => o.Account);
                }
            }
        }
        #endregion

        #region AccountType
        public AccountType _AccountType;
        /// <summary>
        ///AccountType
        /// </summary>
        public AccountType AccountType
        {
            get { return _AccountType; }
            set
            {
                if (_AccountType != value)
                {
                    _AccountType = value;
                    this.NotifyPropertyChanged(o => o.AccountType);
                }
            }
        }
        #endregion

        #region CarrierID
        public Guid? _CarrierID;
        /// <summary>
        ///CarrierID
        /// </summary>
        public Guid? CarrierID
        {
            get { return _CarrierID; }
            set
            {
                if (_CarrierID != value)
                {
                    _CarrierID = value;
                    this.NotifyPropertyChanged(o => o.CarrierID);
                }
            }
        }
        #endregion

        #region CarrierName
        public string _CarrierName;
        /// <summary>
        ///CarrierName
        /// </summary>
        public string CarrierName
        {
            get { return _CarrierName; }
            set
            {
                if (_CarrierName != value)
                {
                    _CarrierName = value;
                    this.NotifyPropertyChanged(o => o.CarrierName);
                }
            }
        }
        #endregion

        #region Ports
        #region POLID
        public Guid _POLID;
        /// <summary>
        ///POLID
        /// </summary>
        [GuidRequired(CMessage="装货港",EMessage = "POL")]
        public Guid POLID
        {
            get { return _POLID; }
            set
            {
                if (_POLID != value)
                {
                    _POLID = value;
                    this.NotifyPropertyChanged(o => o.POLID);
                }
            }
        }
        #endregion

        #region POLName
        public string _POLName;
        /// <summary>
        ///POLName
        /// </summary>
        public string POLName
        {
            get { return _POLName; }
            set
            {
                if (_POLName != value)
                {
                    _POLName = value;
                    this.NotifyPropertyChanged(o => o.POLName);
                }
            }
        }

        public string OLDPOLName
        {
            get;
            set;
        }
        #endregion

        #region VIAID
        public Guid? _VIAID;
        /// <summary>
        ///VIAID
        /// </summary>
        public Guid? VIAID
        {
            get { return _VIAID; }
            set
            {
                if (_VIAID != value)
                {
                    _VIAID = value;
                    this.NotifyPropertyChanged(o => o.VIAID);
                }
            }
        }
        #endregion

        #region VIAName
        public string _VIAName;
        /// <summary>
        ///VIAName
        /// </summary>
        public string VIAName
        {
            get { return _VIAName; }
            set
            {
                if (_VIAName != value)
                {
                    _VIAName = value;
                    this.NotifyPropertyChanged(o => o.VIAName);
                }
            }
        }
        public string OLDVIAName
        {
            get;
            set;
        }
        #endregion

        #region PODID
        public Guid _PODID;
        /// <summary>
        ///PODID
        /// </summary>
        [GuidRequired(CMessage="卸货港",EMessage = "POD")]
        public Guid PODID
        {
            get { return _PODID; }
            set
            {
                if (_PODID != value)
                {
                    _PODID = value;
                    this.NotifyPropertyChanged(o => o.PODID);
                }
            }
        }
        #endregion

        #region PODName
        public string _PODName;
        /// <summary>
        ///PODName
        /// </summary>
        public string PODName
        {
            get { return _PODName; }
            set
            {
                if (_PODName != value)
                {
                    _PODName = value;
                    this.NotifyPropertyChanged(o => o.PODName);
                }
            }
        }

        public string OLDPODName
        {
            get;
            set;
        }
        #endregion

        #region PlaceOfDeliveryID
        public Guid? _PlaceOfDeliveryID;
        /// <summary>
        ///PlaceOfDeliveryID
        /// </summary>
        public Guid? PlaceOfDeliveryID
        {
            get { return _PlaceOfDeliveryID; }
            set
            {
                if (_PlaceOfDeliveryID != value)
                {
                    _PlaceOfDeliveryID = value;
                    this.NotifyPropertyChanged(o => o.PlaceOfDeliveryID);
                }
            }
        }
        #endregion

        #region PlaceOfDeliveryName
        public string _PlaceOfDeliveryName;
        /// <summary>
        ///PlaceOfDeliveryName
        /// </summary>
        public string PlaceOfDeliveryName
        {
            get { return _PlaceOfDeliveryName; }
            set
            {
                if (_PlaceOfDeliveryName != value)
                {
                    _PlaceOfDeliveryName = value;
                    this.NotifyPropertyChanged(o => o.PlaceOfDeliveryName);
                }
            }
        }
        public string OLDDeliveryName
        {
            get;
            set;
        }
        #endregion

        #endregion

        #region FromDate
        public DateTime? _FromDate;
        /// <summary>
        ///FromDate
        /// </summary>
        public DateTime? FromDate
        {
            get { return _FromDate; }
            set
            {
                if (_FromDate != value)
                {
                    _FromDate = value;
                    this.NotifyPropertyChanged(o => o.FromDate);
                }
            }
        }
        #endregion

        #region ToDate
        public DateTime? _ToDate;
        /// <summary>
        ///ToDate
        /// </summary>
        public DateTime? ToDate
        {
            get { return _ToDate; }
            set
            {
                if (_ToDate != value)
                {
                    _ToDate = value;
                    this.NotifyPropertyChanged(o => o.ToDate);
                }
            }
        }
        #endregion

        #region OriginArb
        public bool _OriginArb;
        /// <summary>
        ///OriginArb
        /// </summary>
        public bool OriginArb
        {
            get { return _OriginArb; }
            set
            {
                if (_OriginArb != value)
                {
                    _OriginArb = value;
                    this.NotifyPropertyChanged(o => o.OriginArb);
                }
            }
        }
        #endregion

        #region DestArb
        public bool _DestArb;
        /// <summary>
        ///DestArb
        /// </summary>
        public bool DestArb
        {
            get { return _DestArb; }
            set
            {
                if (_DestArb != value)
                {
                    _DestArb = value;
                    this.NotifyPropertyChanged(o => o.DestArb);
                }
            }
        }
        #endregion

        #region Comm
        public string _Comm;
        /// <summary>
        ///Comm
        /// </summary>
        [StringLength(MaximumLength=2000,CMessage="品名",EMessage="Comm")]
        public string Comm
        {
            get { return _Comm; }
            set
            {
                if (_Comm != value)
                {
                    _Comm = value;
                    this.NotifyPropertyChanged(o => o.Comm);
                }
            }
        }
        #endregion

        #region ItemCode
        public string _ItemCode;
        /// <summary>
        ///ItemCode
        /// </summary>
        public string ItemCode
        {
            get { return _ItemCode; }
            set
            {
                if (_ItemCode != value)
                {
                    _ItemCode = value;
                    this.NotifyPropertyChanged(o => o.ItemCode);
                }
            }
        }
        #endregion

        #region TransportClauseID
        public Guid _TransportClauseID;
        /// <summary>
        ///TransportClauseID
        /// </summary>
        [GuidRequired(CMessage="运输条款",EMessage = "Term")]
        public Guid TransportClauseID
        {
            get { return _TransportClauseID; }
            set
            {
                if (_TransportClauseID != value)
                {
                    _TransportClauseID = value;
                    this.NotifyPropertyChanged(o => o.TransportClauseID);
                }
            }
        }
        #endregion

        #region TransportClauseName
        public string _TransportClauseName;
        /// <summary>
        ///TransportClauseName
        /// </summary>
        public string TransportClauseName
        {
            get { return _TransportClauseName; }
            set
            {
                if (_TransportClauseName != value)
                {
                    _TransportClauseName = value;
                    this.NotifyPropertyChanged(o => o.TransportClauseName);
                }
            }
        }
        #endregion

        #region SurCharge
        public string _SurCharge;
        /// <summary>
        ///SurCharge
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="附加费",EMessage="SurCharge")]
        public string SurCharge
        {
            get { return _SurCharge; }
            set
            {
                if (_SurCharge != value)
                {
                    _SurCharge = value;
                    this.NotifyPropertyChanged(o => o.SurCharge);
                }
            }
        }
        #endregion

        #region No
        public int? _No;
        /// <summary>
        ///No
        /// </summary>
        public int? No
        {
            get { return _No; }
            set
            {
                if (_No != value)
                {
                    _No = value;
                    this.NotifyPropertyChanged(o => o.No);
                }
            }
        }

        public int? IndexNo
        {
            get;
            set;
        }
        #endregion

        #region ClosingDate
        public string _ClosingDate;
        /// <summary>
        ///ClosingDate
        /// </summary>
        public string ClosingDate
        {
            get { return _ClosingDate; }
            set
            {
                if (_ClosingDate != value)
                {
                    _ClosingDate = value;
                    this.NotifyPropertyChanged(o => o.ClosingDate);
                }
            }
        }
        #endregion

        #region TransitTime
        public string _TransitTime;
        /// <summary>
        ///TransitTime
        /// </summary>
        [StringLength(MaximumLength=120,CMessage="航程",EMessage="TransitTime")]
        public string TransitTime
        {
            get { return _TransitTime; }
            set
            {
                if (_TransitTime != value)
                {
                    _TransitTime = value;
                    this.NotifyPropertyChanged(o => o.TransitTime);
                }
            }
        }
        #endregion

        #region Description
        public string _Description;
        /// <summary>
        ///Description
        /// </summary>
        [StringLength(MaximumLength=2000,CMessage="说明",EMessage="Description")]
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    this.NotifyPropertyChanged(o => o.Description);
                }
            }
        }
        #endregion

        #region CreateInfo
        #region CreateByID
        public Guid _CreateByID;
        /// <summary>
        ///CreateByID
        /// </summary>
        public Guid CreateByID
        {
            get { return _CreateByID; }
            set
            {
                if (_CreateByID != value)
                {
                    _CreateByID = value;
                    this.NotifyPropertyChanged(o => o.CreateByID);
                }
            }
        }
        #endregion

        #region CreateByName
        public string _CreateByName;
        /// <summary>
        ///CreateByName
        /// </summary>
        public string CreateByName
        {
            get { return _CreateByName; }
            set
            {
                if (_CreateByName != value)
                {
                    _CreateByName = value;
                    this.NotifyPropertyChanged(o => o.CreateByName);
                }
            }
        }
        #endregion

        #region CreateDate
        public DateTime _CreateDate;
        /// <summary>
        ///CreateDate
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set
            {
                if (_CreateDate != value)
                {
                    _CreateDate = value;
                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }
        }
        #endregion

        #region UpdateDate
        public DateTime? _UpdateDate;
        /// <summary>
        ///UpdateDate
        /// </summary>
        public DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set
            {
                if (_UpdateDate != value)
                {
                    _UpdateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }
        #endregion
        #endregion

        #region CopyID
        /// <summary>
        /// 复制于哪条BastPort,用于关联AddFee时用
        /// </summary>
        public Guid? CopyID
        {
            get;
            set;
        }
        #endregion

        public string CurrencyName
        {
            get;
            set;
        }

        #endregion

    }


    [Serializable]
    public class OceanRateToExcel
    {
        public List<OceanRateDataObject> DataList
        {
            get;
            set;
        }

        public List<string> UnitList
        {
            get;
            set;
        }

    }

    [Serializable]
    public class OceanRateDataObject : ClientContainerRateObject
    {

        public string CarrierName { get; set; }
        public string POLName { get; set; }
        public string VIAName { get; set; }
        public string PODName { get; set; }
        public string DeliveryName { get; set; }
        public string FinalDestinationName { get; set; }
        public string ItemCode { get; set; }
        public string Comm { get; set; }
        public string Term { get; set; }
        public string SurCharge { get; set; }
        public string CLS { get; set; }
        public string TT { get; set; }
        public DateTime? DurationForm { get; set; }
        public DateTime? DurationTo { get; set; }
        public string Description { get; set; }
        public string CurrencyName { get; set; }

    }

    [Serializable]
    [KnownType(typeof(List<ClientBasePortList>))]
    [KnownType(typeof(List<string>))]
    public sealed class BasePortRateList
    {
       public List<object> Data { get; set; }
    }
    /// <summary>
    /// Arbitrary数据
    /// </summary>

    [Serializable]
    public class ClientArbitraryList : ClientContainerRateObject
    {
        #region 属性

        public override bool IsNew { get { return ID.IsNullOrEmpty(); } }

        public string ErrorInfo { get; set; }

        public bool HasError
        {
            get { return !ErrorInfo.IsNullOrEmpty(); }
        }

        public ChangeState ChangeState
        {
            get
            {
                if (IsNew) return ChangeState.New;
                else if (IsDirty) return ChangeState.Changed;
                else return ChangeState.None;
            }
        }

        #region ID
        Guid _ID;
        /// <summary>
        ///ID
        /// </summary>
        public Guid ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }
        #endregion

        #region OceanID
        Guid? _OceanID;
        /// <summary>
        ///OceanID
        /// </summary>
        public Guid? OceanID
        {
            get { return _OceanID; }
            set
            {
                if (_OceanID != value)
                {
                    _OceanID = value;
                    this.NotifyPropertyChanged(o => o.OceanID);
                }
            }
        }
        #endregion

        #region GeographyType
        GeographyType _GeographyType;
        /// <summary>
        ///GeographyType
        /// </summary>
        public GeographyType GeographyType
        {
            get { return _GeographyType; }
            set
            {
                if (_GeographyType != value)
                {
                    _GeographyType = value;
                    this.NotifyPropertyChanged(o => o.GeographyType);
                }
            }
        }


        #endregion

        #region GeographyType
        ModeOfTransport _ModeOfTransport;
        /// <summary>
        ///TransportType
        /// </summary>
        public ModeOfTransport ModeOfTransport
        {
            get { return _ModeOfTransport; }
            set
            {
                if (_ModeOfTransport != value)
                {
                    _ModeOfTransport = value;
                    this.NotifyPropertyChanged(o => o.ModeOfTransport);
                }
            }
        }


        #endregion

        #region Ports
        #region POLID
        Guid _POLID;
        /// <summary>
        ///POLID
        /// </summary>
        public Guid POLID
        {
            get { return _POLID; }
            set
            {
                if (_POLID != value)
                {
                    _POLID = value;
                    this.NotifyPropertyChanged(o => o.POLID);
                }
            }
        }
        #endregion

        #region POLName
        string _POLName;
        /// <summary>
        ///POLName
        /// </summary>
        public string POLName
        {
            get { return _POLName; }
            set
            {
                if (_POLName != value)
                {
                    _POLName = value;
                    this.NotifyPropertyChanged(o => o.POLName);
                }
            }
        }
        #endregion

        #region PODID
        Guid _PODID;
        /// <summary>
        ///PODID
        /// </summary>
        public Guid PODID
        {
            get { return _PODID; }
            set
            {
                if (_PODID != value)
                {
                    _PODID = value;
                    this.NotifyPropertyChanged(o => o.PODID);
                }
            }
        }
        #endregion

        #region PODName
        string _PODName;
        /// <summary>
        ///PODName
        /// </summary>
        public string PODName
        {
            get { return _PODName; }
            set
            {
                if (_PODName != value)
                {
                    _PODName = value;
                    this.NotifyPropertyChanged(o => o.PODName);
                }
            }
        }
        #endregion

        #endregion

        #region ItemCode
        string _ItemCode;
        /// <summary>
        ///ItemCode
        /// </summary>
        public string ItemCode
        {
            get { return _ItemCode; }
            set
            {
                if (_ItemCode != value)
                {
                    _ItemCode = value;
                    this.NotifyPropertyChanged(o => o.ItemCode);
                }
            }
        }
        #endregion


        #region Remark
        private string remark;
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                if (remark != value)
                {
                    this.NotifyPropertyChanged(o => o.Remark);
                    remark = value;
                }
            }
        }
        #endregion

        #region TransportClauseID
        Guid _TransportClauseID;
        /// <summary>
        ///TransportClauseID
        /// </summary>
        public Guid TransportClauseID
        {
            get { return _TransportClauseID; }
            set
            {
                if (_TransportClauseID != value)
                {
                    _TransportClauseID = value;
                    this.NotifyPropertyChanged(o => o.TransportClauseID);
                }
            }
        }
        #endregion

        #region TransportClauseName
        string _TransportClauseName;
        /// <summary>
        ///TransportClauseName
        /// </summary>
        public string TransportClauseName
        {
            get { return _TransportClauseName; }
            set
            {
                if (_TransportClauseName != value)
                {
                    _TransportClauseName = value;
                    this.NotifyPropertyChanged(o => o.TransportClauseName);
                }
            }
        }
        #endregion

        #region No
        int? _No;
        /// <summary>
        ///No
        /// </summary>
        public int? No
        {
            get { return _No; }
            set
            {
                if (_No != value)
                {
                    _No = value;
                    this.NotifyPropertyChanged(o => o.No);
                }
            }
        }
        #endregion

        #region CreateInfo
        #region CreateByID
        Guid _CreateByID;
        /// <summary>
        ///CreateByID
        /// </summary>
        public Guid CreateByID
        {
            get { return _CreateByID; }
            set
            {
                if (_CreateByID != value)
                {
                    _CreateByID = value;
                    this.NotifyPropertyChanged(o => o.CreateByID);
                }
            }
        }
        #endregion

        #region CreateByName
        string _CreateByName;
        /// <summary>
        ///CreateByName
        /// </summary>
        public string CreateByName
        {
            get { return _CreateByName; }
            set
            {
                if (_CreateByName != value)
                {
                    _CreateByName = value;
                    this.NotifyPropertyChanged(o => o.CreateByName);
                }
            }
        }
        #endregion

        #region CreateDate
        DateTime _CreateDate;
        /// <summary>
        ///CreateDate
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set
            {
                if (_CreateDate != value)
                {
                    _CreateDate = value;
                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }
        }
        #endregion

        #region UpdateDate
        DateTime? _UpdateDate;
        /// <summary>
        ///UpdateDate
        /// </summary>
        public DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set
            {
                if (_UpdateDate != value)
                {
                    _UpdateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }
        #endregion
        #endregion
        #endregion

        /// <summary>
        /// 此项在仅在BasePort中查看已关联的Arbitrary用到
        /// </summary>
        public GeographyType AssociatedType { get; set; }
    }
    /// <summary>
    /// AdditionalFee数据
    /// </summary>
    [Serializable]
    public class ClientAdditionalFeeList : ClientContainerRateObject
    {
        public override bool IsNew { get { return ID.IsNullOrEmpty(); } }

        public bool Selected { get; set; }
        /// <summary>
        /// 关联运价条目
        /// </summary>
        public int AssociatedCount
        {
            get { return BaseRateIDs == null ? 0 : BaseRateIDs.Count; }
        }
        /// <summary>
        ///OceanItemIDs
        /// </summary>
        public List<Guid> BaseRateIDs { get; set; }

        #region ID
        Guid _ID;
        /// <summary>
        ///ID
        /// </summary>
        public Guid ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }
        #endregion

        #region OceanID
        Guid _OceanID;
        /// <summary>
        ///OceanID
        /// </summary>
        public Guid OceanID
        {
            get { return _OceanID; }
            set
            {
                if (_OceanID != value)
                {
                    _OceanID = value;
                    this.NotifyPropertyChanged(o => o.OceanID);
                }
            }
        }
        #endregion

        #region ChargingCodeID
        Guid _ChargingCodeID;
        /// <summary>
        ///ChargingCodeID
        /// </summary>
        [GuidRequired(CMessage="费用",EMessage = "Fee")]
        public Guid ChargingCodeID
        {
            get { return _ChargingCodeID; }
            set
            {
                if (_ChargingCodeID != value)
                {
                    _ChargingCodeID = value;
                    this.NotifyPropertyChanged(o => o.ChargingCodeID);
                }
            }
        }
        #endregion

        #region ChargingCode
        string _ChargingCode;
        /// <summary>
        ///ChargingCode
        /// </summary>
        public string ChargingCode
        {
            get { return _ChargingCode; }
            set
            {
                if (_ChargingCode != value)
                {
                    _ChargingCode = value;
                    this.NotifyPropertyChanged(o => o.ChargingCode);
                }
            }
        }
        #endregion

        #region ChargingCodeDescription
        string _ChargingCodeDescription;
        /// <summary>
        ///ChargingCodeDescription
        /// </summary>
        public string ChargingCodeDescription
        {
            get { return _ChargingCodeDescription; }
            set
            {
                if (_ChargingCodeDescription != value)
                {
                    _ChargingCodeDescription = value;
                    this.NotifyPropertyChanged(o => o.ChargingCodeDescription);
                }
            }
        }
        #endregion

        #region CustomerID
        Guid? _CustomerID;
        /// <summary>
        ///CustomerID
        /// </summary>
        public Guid? CustomerID
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
        #endregion

        #region CustomerName
        string _CustomerName;
        /// <summary>
        ///CustomerName
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
        #endregion

        #region CurrencyID
        Guid _CurrencyID;
        /// <summary>
        ///CurrencyID
        /// </summary>
        [GuidRequired(CMessage="币种",EMessage = "Currency")]
        public Guid CurrencyID
        {
            get { return _CurrencyID; }
            set
            {
                if (_CurrencyID != value)
                {
                    _CurrencyID = value;
                    this.NotifyPropertyChanged(o => o.CurrencyID);
                }
            }
        }
        #endregion

        #region CurrencyName
        string _CurrencyName;
        /// <summary>
        ///CurrencyName
        /// </summary>
        public string CurrencyName
        {
            get { return _CurrencyName; }
            set
            {
                if (_CurrencyName != value)
                {
                    _CurrencyName = value;
                    this.NotifyPropertyChanged(o => o.CurrencyName);
                }
            }
        }
        #endregion

        #region CreateByID
        Guid _CreateByID;
        /// <summary>
        ///CreateByID
        /// </summary>
        public Guid CreateByID
        {
            get { return _CreateByID; }
            set
            {
                if (_CreateByID != value)
                {
                    _CreateByID = value;
                    this.NotifyPropertyChanged(o => o.CreateByID);
                }
            }
        }
        #endregion

        #region CreateByName
        string _CreateByName;
        /// <summary>
        ///CreateByName
        /// </summary>
        public string CreateByName
        {
            get { return _CreateByName; }
            set
            {
                if (_CreateByName != value)
                {
                    _CreateByName = value;
                    this.NotifyPropertyChanged(o => o.CreateByName);
                }
            }
        }
        #endregion

        #region CreateDate
        DateTime _CreateDate;
        /// <summary>
        ///CreateDate
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set
            {
                if (_CreateDate != value)
                {
                    _CreateDate = value;
                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }
        }
        #endregion

        #region UpdateDate
        DateTime? _UpdateDate;
        /// <summary>
        ///UpdateDate
        /// </summary>
        public DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set
            {
                if (_UpdateDate != value)
                {
                    _UpdateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }
        #endregion

        #region FromDate
        DateTime? _FromDate;
        /// <summary>
        ///FromDate
        /// </summary>
        public DateTime? FromDate
        {
            get { return _FromDate; }
            set
            {
                if (_FromDate != value)
                {
                    _FromDate = value;
                    this.NotifyPropertyChanged(o => o.FromDate);
                }
            }
        }
        #endregion

        #region ToDate
        DateTime? _ToDate;
        /// <summary>
        ///ToDate
        /// </summary>
        public DateTime? ToDate
        {
            get { return _ToDate; }
            set
            {
                if (_ToDate != value)
                {
                    _ToDate = value;
                    this.NotifyPropertyChanged(o => o.ToDate);
                }
            }
        }
        #endregion

        #region IsSpecialFee
        bool _isspecialfee;
        /// <summary>
        /// 是否特殊费用（如果是特殊费用，那么必须与运价项目关联才有效）
        /// </summary>
        public bool IsSpecialFee
        {
            get
            {
                return _isspecialfee;
            }
            set
            {
                if (_isspecialfee != value)
                {
                    _isspecialfee = value;
                    base.OnPropertyChanged("IsSpecialFee", value);
                }
            }
        }
        #endregion

        #region Percent
        short _Percent;
        /// <summary>
        /// 百分比
        /// </summary>
        public short Percent
        {
            get
            {
                return _Percent;
            }
            set
            {
                if (_Percent != value)
                {
                    _Percent = value;
                    base.OnPropertyChanged("Percent", value);
                }
            }
        }
        #endregion

        #region Remark
        string _Remark;
        /// <summary>
        /// 说明
        /// </summary>
        public string Remark
        {
            get
            {
                return _Remark;
            }
            set
            {
                if (_Remark != value)
                {
                    _Remark = value;
                    base.OnPropertyChanged("Remark", value);
                }
            }
        }
        #endregion

    }
    /// <summary>
    /// BaseRates
    /// </summary>
    [Serializable]
    public class ClientBaseRatesList : ClientContainerRateObject
    {
        public bool CurrentAssoc { get; set; }
        public bool OriginalAssoc { get; set; }
        public bool IsInCondition { get; set; }

        #region ID
        Guid _ID;
        /// <summary>
        ///ID
        /// </summary>
        public Guid ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }
        #endregion

        #region OceanID
        Guid _OceanID;
        /// <summary>
        ///ID
        /// </summary>
        public Guid OceanID
        {
            get { return _OceanID; }
            set
            {
                if (_OceanID != value)
                {
                    _OceanID = value;
                    this.NotifyPropertyChanged(o => o.OceanID);
                }
            }
        }
        #endregion

        #region OriginalArbitraryID
        Guid? _OriginalArbitraryID;
        /// <summary>
        ///OriginalArbitraryID
        /// </summary>
        public Guid? OriginalArbitraryID
        {
            get { return _OriginalArbitraryID; }
            set
            {
                if (_OriginalArbitraryID != value)
                {
                    _OriginalArbitraryID = value;
                    this.NotifyPropertyChanged(o => o.OriginalArbitraryID);
                }
            }
        }
        #endregion

        #region BasePortID
        Guid _BasePortID;
        /// <summary>
        ///BasePortID
        /// </summary>
        public Guid BasePortID
        {
            get { return _BasePortID; }
            set
            {
                if (_BasePortID != value)
                {
                    _BasePortID = value;
                    this.NotifyPropertyChanged(o => o.BasePortID);
                }
            }
        }
        #endregion

        #region DestinationArbitraryID
        Guid? _DestinationArbitraryID;
        /// <summary>
        ///DestinationArbitraryID
        /// </summary>
        public Guid? DestinationArbitraryID
        {
            get { return _DestinationArbitraryID; }
            set
            {
                if (_DestinationArbitraryID != value)
                {
                    _DestinationArbitraryID = value;
                    this.NotifyPropertyChanged(o => o.DestinationArbitraryID);
                }
            }
        }
        #endregion

        #region OriginalArbitraryNo
        int? _OriginalArbitraryNo;
        /// <summary>
        ///OriginalArbitraryNo
        /// </summary>
        public int? OriginalArbitraryNo
        {
            get { return _OriginalArbitraryNo; }
            set
            {
                if (_OriginalArbitraryNo != value)
                {
                    _OriginalArbitraryNo = value;
                    this.NotifyPropertyChanged(o => o.OriginalArbitraryNo);
                }
            }
        }
        #endregion

        #region BasePortNo
        int? _BasePortNo;
        /// <summary>
        ///BasePortNo
        /// </summary>
        public int? BasePortNo
        {
            get { return _BasePortNo; }
            set
            {
                if (_BasePortNo != value)
                {
                    _BasePortNo = value;
                    this.NotifyPropertyChanged(o => o.BasePortNo);
                }
            }
        }
        #endregion

        #region BasePortIndexNo
        int? _BasePortIndexNo;
        public int? BasePortIndexNo
        {
            set
            {
                _BasePortIndexNo = value;
            }
            get
            {
                if (_BasePortIndexNo != null)
                {
                    return _BasePortIndexNo;
                }
                else
                {
                    return BasePortNo;
                }
            }
        }
        #endregion


        #region DestinationArbitraryNo
        int? _DestinationArbitraryNo;
        /// <summary>
        ///DestinationArbitraryNo
        /// </summary>
        public int? DestinationArbitraryNo
        {
            get { return _DestinationArbitraryNo; }
            set
            {
                if (_DestinationArbitraryNo != value)
                {
                    _DestinationArbitraryNo = value;
                    this.NotifyPropertyChanged(o => o.DestinationArbitraryNo);
                }
            }
        }
        #endregion

        #region Account
        string _Account;
        /// <summary>
        ///Account
        /// </summary>
        public string Account
        {
            get { return _Account; }
            set
            {
                if (_Account != value)
                {
                    _Account = value;
                    this.NotifyPropertyChanged(o => o.Account);
                }
            }
        }
        #endregion

        #region AccountType
        AccountType _AccountType;
        /// <summary>
        ///AccountType
        /// </summary>
        public AccountType AccountType
        {
            get { return _AccountType; }
            set
            {
                if (_AccountType != value)
                {
                    _AccountType = value;
                    this.NotifyPropertyChanged(o => o.AccountType);
                }
            }
        }
        #endregion

        #region CarrierID
        Guid? _CarrierID;
        /// <summary>
        ///CarrierID
        /// </summary>
        public Guid? CarrierID
        {
            get { return _CarrierID; }
            set
            {
                if (_CarrierID != value)
                {
                    _CarrierID = value;
                    this.NotifyPropertyChanged(o => o.CarrierID);
                }
            }
        }
        #endregion

        #region CarrierName
        string _CarrierName;
        /// <summary>
        ///CarrierName
        /// </summary>
        public string CarrierName
        {
            get { return _CarrierName; }
            set
            {
                if (_CarrierName != value)
                {
                    _CarrierName = value;
                    this.NotifyPropertyChanged(o => o.CarrierName);
                }
            }
        }
        #endregion

        #region POLID
        Guid _POLID;
        /// <summary>
        ///POLID
        /// </summary>
        public Guid POLID
        {
            get { return _POLID; }
            set
            {
                if (_POLID != value)
                {
                    _POLID = value;
                    this.NotifyPropertyChanged(o => o.POLID);
                }
            }
        }
        /// <summary>
        /// 旧的POLID
        /// </summary>
        public Guid OLDPOLID
        {
            get;
            set;
        }
        #endregion

        #region POLName
        string _POLName;
        /// <summary>
        ///POLName
        /// </summary>
        public string POLName
        {
            get { return _POLName; }
            set
            {
                if (_POLName != value)
                {
                    _POLName = value;
                    this.NotifyPropertyChanged(o => o.POLName);
                }
            }
        }
        /// <summary>
        /// 原始POLNAME
        /// </summary>
        public string OLDPOLName
        {
            get;
            set;
        }
        #endregion

        #region VIAID
        Guid? _VIAID;
        /// <summary>
        ///VIAID
        /// </summary>
        public Guid? VIAID
        {
            get { return _VIAID; }
            set
            {
                if (_VIAID != value)
                {
                    _VIAID = value;
                    this.NotifyPropertyChanged(o => o.VIAID);
                }
            }
        }
        /// <summary>
        /// 旧的VIAID
        /// </summary>
        public Guid? OLDVIAID
        {
            get;
            set;
        }
        #endregion

        #region VIAName
        string _VIAName;
        /// <summary>
        ///VIAName
        /// </summary>
        public string VIAName
        {
            get { return _VIAName; }
            set
            {
                if (_VIAName != value)
                {
                    _VIAName = value;
                    this.NotifyPropertyChanged(o => o.VIAName);
                }
            }
        }
        /// <summary>
        /// 原始VIAName
        /// </summary>
        public string OLDVIAName
        {
            get;
            set;
        }
        #endregion

        #region PODID
        Guid _PODID;
        /// <summary>
        ///PODID
        /// </summary>
        public Guid PODID
        {
            get { return _PODID; }
            set
            {
                if (_PODID != value)
                {
                    _PODID = value;
                    this.NotifyPropertyChanged(o => o.PODID);
                }
            }
        }
        /// <summary>
        /// 旧的PODID
        /// </summary>
        public Guid? OLDPODID
        {
            get;
            set;
        }
        #endregion

        #region PODName
        string _PODName;
        /// <summary>
        ///PODName
        /// </summary>
        public string PODName
        {
            get { return _PODName; }
            set
            {
                if (_PODName != value)
                {
                    _PODName = value;
                    this.NotifyPropertyChanged(o => o.PODName);
                }
            }
        }
        public string OLDPODName
        {
            get;
            set;
        }
        #endregion

        #region PlaceOfDeliveryID
        Guid _PlaceOfDeliveryID;
        /// <summary>
        ///PlaceOfDeliveryID
        /// </summary>
        public Guid PlaceOfDeliveryID
        {
            get { return _PlaceOfDeliveryID; }
            set
            {
                if (_PlaceOfDeliveryID != value)
                {
                    _PlaceOfDeliveryID = value;
                    this.NotifyPropertyChanged(o => o.PlaceOfDeliveryID);
                }
            }
        }
        /// <summary>
        /// 旧的DeliveryID
        /// </summary>
        public Guid? OLDDeliveryID
        {
            get;
            set;
        }
        #endregion

        #region PlaceOfDeliveryName
        string _PlaceOfDeliveryName;
        /// <summary>
        ///PlaceOfDeliveryName
        /// </summary>
        public string PlaceOfDeliveryName
        {
            get { return _PlaceOfDeliveryName; }
            set
            {
                if (_PlaceOfDeliveryName != value)
                {
                    _PlaceOfDeliveryName = value;
                    this.NotifyPropertyChanged(o => o.PlaceOfDeliveryName);
                }
            }
        }
        /// <summary>
        /// 原始OLDDeliveryName
        /// </summary>
        public string OLDDeliveryName
        {
            get;
            set;
        }

        #endregion

        #region 
        public Guid? FinalDestinationID { get; set; }
        public string FinalDestinationName { get; set; }

        #endregion

        #region FromDate
        DateTime? _FromDate;
        /// <summary>
        ///FromDate
        /// </summary>
        public DateTime? FromDate
        {
            get { return _FromDate; }
            set
            {
                if (_FromDate != value)
                {
                    _FromDate = value;
                    this.NotifyPropertyChanged(o => o.FromDate);
                }
            }
        }
        #endregion

        #region ToDate
        DateTime? _ToDate;
        /// <summary>
        ///ToDate
        /// </summary>
        public DateTime? ToDate
        {
            get { return _ToDate; }
            set
            {
                if (_ToDate != value)
                {
                    _ToDate = value;
                    this.NotifyPropertyChanged(o => o.ToDate);
                }
            }
        }
        #endregion

        #region Comm
        string _Comm;
        /// <summary>
        ///Comm
        /// </summary>
        public string Comm
        {
            get { return _Comm; }
            set
            {
                if (_Comm != value)
                {
                    _Comm = value;
                    this.NotifyPropertyChanged(o => o.Comm);
                }
            }
        }
        #endregion

        #region ItemCode
        string _ItemCode;
        /// <summary>
        ///ItemCode
        /// </summary>
        public string ItemCode
        {
            get { return _ItemCode; }
            set
            {
                if (_ItemCode != value)
                {
                    _ItemCode = value;
                    this.NotifyPropertyChanged(o => o.ItemCode);
                }
            }
        }
        #endregion

        #region TransportClauseID
        Guid _TransportClauseID;
        /// <summary>
        ///TransportClauseID
        /// </summary>
        public Guid TransportClauseID
        {
            get { return _TransportClauseID; }
            set
            {
                if (_TransportClauseID != value)
                {
                    _TransportClauseID = value;
                    this.NotifyPropertyChanged(o => o.TransportClauseID);
                }
            }
        }
        #endregion

        #region TransportClauseName
        string _TransportClauseName;
        /// <summary>
        ///TransportClauseName
        /// </summary>
        public string TransportClauseName
        {
            get { return _TransportClauseName; }
            set
            {
                if (_TransportClauseName != value)
                {
                    _TransportClauseName = value;
                    this.NotifyPropertyChanged(o => o.TransportClauseName);
                }
            }
        }
        #endregion

        #region UnitRates
        List<ICP.FRM.ServiceInterface.DataObjects.FrmUnitRateList> _UnitRates;
        /// <summary>
        ///UnitRates
        /// </summary>
        public List<ICP.FRM.ServiceInterface.DataObjects.FrmUnitRateList> UnitRates
        {
            get { return _UnitRates; }
            set
            {
                if (_UnitRates != value)
                {
                    _UnitRates = value;
                    this.NotifyPropertyChanged(o => o.UnitRates);
                }
            }
        }
        #endregion

        #region SurCharge
        string _SurCharge;
        /// <summary>
        ///SurCharge
        /// </summary>
        public string SurCharge
        {
            get { return _SurCharge; }
            set
            {
                if (_SurCharge != value)
                {
                    _SurCharge = value;
                    this.NotifyPropertyChanged(o => o.SurCharge);
                }
            }
        }
        #endregion

        #region ClosingDate
        string _ClosingDate;
        /// <summary>
        ///ClosingDate
        /// </summary>
        public string ClosingDate
        {
            get { return _ClosingDate; }
            set
            {
                if (_ClosingDate != value)
                {
                    _ClosingDate = value;
                    this.NotifyPropertyChanged(o => o.ClosingDate);
                }
            }
        }
        #endregion

        #region TransitTime
        string _TransitTime;
        /// <summary>
        ///TransitTime
        /// </summary>
        public string TransitTime
        {
            get { return _TransitTime; }
            set
            {
                if (_TransitTime != value)
                {
                    _TransitTime = value;
                    this.NotifyPropertyChanged(o => o.TransitTime);
                }
            }
        }
        #endregion

        #region Description
        string _Description;
        /// <summary>
        ///Description
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    this.NotifyPropertyChanged(o => o.Description);
                }
            }
        }
        #endregion

        #region CreateByID
        Guid _CreateByID;
        /// <summary>
        ///CreateByID
        /// </summary>
        public Guid CreateByID
        {
            get { return _CreateByID; }
            set
            {
                if (_CreateByID != value)
                {
                    _CreateByID = value;
                    this.NotifyPropertyChanged(o => o.CreateByID);
                }
            }
        }
        #endregion

        #region CreateByName
        string _CreateByName;
        /// <summary>
        ///CreateByName
        /// </summary>
        public string CreateByName
        {
            get { return _CreateByName; }
            set
            {
                if (_CreateByName != value)
                {
                    _CreateByName = value;
                    this.NotifyPropertyChanged(o => o.CreateByName);
                }
            }
        }
        #endregion

        #region CreateDate
        DateTime _CreateDate;
        /// <summary>
        ///CreateDate
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set
            {
                if (_CreateDate != value)
                {
                    _CreateDate = value;
                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }
        }
        #endregion

        #region UpdateDate
        DateTime? _UpdateDate;
        /// <summary>
        ///UpdateDate
        /// </summary>
        public DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set
            {
                if (_UpdateDate != value)
                {
                    _UpdateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }
        #endregion


    }


    [Serializable]
    [KnownType(typeof(List<ClientBaseRatesList>))]
    [KnownType(typeof(List<string>))]
    public sealed class BaseRateList
    {
        public List<object> Data { get; set; }
    }

    #endregion
}
