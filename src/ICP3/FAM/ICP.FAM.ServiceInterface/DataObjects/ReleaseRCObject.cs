using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    [Serializable]
    public class ReleaseRCList:BaseDataObject
    {
        /// <summary>
        /// 根据主键判断是否新增数据
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        #region  ID

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

        #endregion

        #region  ID

        Guid _OperationID;
        /// <summary>
        /// 所属业务ID
        /// </summary>
        public Guid OperationID
        {
            get { return _OperationID; }
            set
            {
                if (_OperationID != value)
                {
                    _OperationID = value;
                    this.NotifyPropertyChanged(o => o._OperationID);
                }
            }
        }
       
        #endregion

        string operationNo;

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
        /// 已选择,客户端帮助属性
        /// </summary>
        public bool Selected { get; set; }

        #region ReleaseBLState
        ReleaseRCState _releaseState;
        /// <summary>
        /// 放单状态
        /// </summary>
        public ReleaseRCState State
        {
            get
            {
                return _releaseState;
            }
            set
            {
                if (_releaseState != value)
                {
                    _releaseState = value;
                    this.NotifyPropertyChanged(o => o.State);
                }
            }
        }
        #endregion

        #region  ETD
        DateTime? _etd;
        /// <summary>
        /// 离港日
        /// </summary>
        public DateTime? ETD
        {
            get
            {
                return _etd;
            }
            set
            {
                if (_etd != value)
                {
                    _etd = value;
                    this.NotifyPropertyChanged(o => o.ETD);
                }
            }
        }
        #endregion

        #region  BlNo
        string _blNo;
        /// <summary>
        /// 提单号
        /// </summary>
        public string BlNo
        {
            get
            {
                return _blNo;
            }
            set
            {
                if (_blNo != value)
                {
                    _blNo = value;
                    this.NotifyPropertyChanged(o => o.BlNo);
                }
            }
        }
        #endregion

        #region AgentName
        string _AgentName;
        /// <summary>
        /// 代理
        /// </summary>
        public string AgentName
        {
            get
            {
                return _AgentName;
            }
            set
            {
                if (_AgentName != value)
                {
                    _AgentName = value;
                    this.NotifyPropertyChanged(o => o.AgentName);
                }
            }
        }
        #endregion

        #region FormType

        ICP.Framework.CommonLibrary.Common.FormType _BLType;
        /// <summary>
        /// 提单类型
        /// </summary>
        public ICP.Framework.CommonLibrary.Common.FormType BLType
        {
            get
            {
                return _BLType;
            }
            set
            {
                if (_BLType != value)
                {
                    _BLType = value;
                    this.NotifyPropertyChanged(o => o.BLType);
                }
            }
        }

        #endregion

        #region ContainerNos
        string _containerNos;
        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNos
        {
            get
            {
                return _containerNos;
            }
            set
            {
                if (_containerNos != value)
                {
                    _containerNos = value;
                    this.NotifyPropertyChanged(o => o.ContainerNos);
                }
            }
        }
        #endregion

        #region VesselVoyage
        string _vesselVoyage;
        /// <summary>
        /// 船名航次
        /// </summary>
        public string VesselVoyage
        {
            get
            {
                return _vesselVoyage;
            }
            set
            {
                if (_vesselVoyage != value)
                {
                    _vesselVoyage = value;
                    this.NotifyPropertyChanged(o => o.VesselVoyage);
                }
            }
        }
        #endregion

        //#region Customer
        //string _customer;
        ///// <summary>
        ///// 客户
        ///// </summary>
        //public string Customer
        //{
        //    get
        //    {
        //        return _customer;
        //    }
        //    set
        //    {
        //        if (_customer != value)
        //        {
        //            _customer = value;
        //            this.NotifyPropertyChanged(o => o.Customer);
        //        }
        //    }
        //}

        //Guid _customerID;
        ///// <summary>
        ///// 客户
        ///// </summary>
        //[GuidRequired(ErrorMessage = "客户必须填写")]
        //public Guid CustomerID
        //{
        //    get
        //    {
        //        return _customerID;
        //    }
        //    set
        //    {
        //        if (_customerID != value)
        //        {
        //            _customerID = value;
        //            this.NotifyPropertyChanged(o => o.CustomerID);
        //        }
        //    }
        //}

        //#endregion

        //#region CustomerContact

        //string _customerContact;
        ///// <summary>
        ///// 客户联系人
        ///// </summary>
        //public string CustomerContact
        //{
        //    get
        //    {
        //        return _customerContact;
        //    }
        //    set
        //    {
        //        if (_customerContact != value)
        //        {
        //            _customerContact = value;
        //            this.NotifyPropertyChanged(o => o.CustomerContact);
        //        }
        //    }
        //}
        //#endregion

        #region ETA

        DateTime? _eta;
        /// <summary>
        /// 到港日
        /// </summary>
        public DateTime? ETA
        {
            get
            {
                return _eta;
            }
            set
            {
                if (_eta != value)
                {
                    _eta = value;
                    this.NotifyPropertyChanged(o => o.ETA);
                }
            }
        }
        #endregion

        #region FETA

        DateTime? _feta;
        /// <summary>
        /// 到港日
        /// </summary>
        public DateTime? FETA
        {
            get
            {
                return _feta;
            }
            set
            {
                if (_feta != value)
                {
                    _feta = value;
                    this.NotifyPropertyChanged(o => o.FETA);
                }
            }
        }
        #endregion

        #region ConsigneeName
        string _consigneeName;
        /// <summary>
        /// 收货人
        /// </summary>
        public string ConsigneeName
        {
            get
            {
                return _consigneeName;
            }
            set
            {
                if (_consigneeName != value)
                {
                    _consigneeName = value;
                    this.NotifyPropertyChanged(o => o.ConsigneeName);
                }
            }
        }
        #endregion

        #region ConsigneeContact
        string _consigneeContact;
        /// <summary>
        /// 收货人描述
        /// </summary>
        public string ConsigneeContact
        {
            get
            {
                return _consigneeContact;
            }
            set
            {
                if (_consigneeContact != value)
                {
                    _consigneeContact = value;
                    this.NotifyPropertyChanged(o => o.ConsigneeContact);
                }
            }
        }
        #endregion

        #region ReleaseType
        ReleaseType _releaseType;
        /// <summary>
        /// 类型(正本 1 ,电放 2)
        /// </summary>
        public ReleaseType ReleaseType
        {
            get
            {
                return _releaseType;
            }
            set
            {
                if (_releaseType != value)
                {
                    _releaseType = value;
                    this.NotifyPropertyChanged(o => o.ReleaseType);
                }
            }
        }
        #endregion

        #region PaymentTerm

        string _paymentTerm;
        /// <summary>
        /// 付款方式
        /// </summary>
        public string PaymentTerm
        {
            get
            {
                return _paymentTerm;
            }
            set
            {
                if (_paymentTerm != value)
                {
                    _paymentTerm = value;
                    this.NotifyPropertyChanged(o => o.PaymentTerm);
                }
            }
        }
        #endregion

        #region IsMonthlyStatement
        bool _isMonthlyStatement;
        /// <summary>
        /// 是否月结
        /// </summary>
        public bool IsMonthlyStatement
        {
            get
            {
                return _isMonthlyStatement;
            }
            set
            {
                if (_isMonthlyStatement != value)
                {
                    _isMonthlyStatement = value;
                    this.NotifyPropertyChanged(o => o.IsMonthlyStatement);
                }
            }
        }
        #endregion

        #region IsAlwayTelex
        bool _IsAlwayTelex;
        /// <summary>
        /// 总电放
        /// </summary>
        public bool IsAlwayTelex
        {
            get
            {
                return _IsAlwayTelex;
            }
            set
            {
                if (_IsAlwayTelex != value)
                {
                    _IsAlwayTelex = value;
                    this.NotifyPropertyChanged(o => o.IsAlwayTelex);
                }
            }
        }
        #endregion

        #region IsApplyTelex
        bool _isApplyTelex;
        /// <summary>
        /// 已收电放申请
        /// </summary>
        public bool IsApplyTelex
        {
            get
            {
                return _isApplyTelex;
            }
            set
            {
                if (_isApplyTelex != value)
                {
                    _isApplyTelex = value;
                    this.NotifyPropertyChanged(o => o.IsApplyTelex);
                }
            }
        }
        #endregion

        #region IsWriteOff
        bool _IsWriteOff;
        /// <summary>
        /// 已销帐
        /// </summary>
        public bool IsWriteOff
        {
            get
            {
                return _IsWriteOff;
            }
            set
            {
                if (_IsWriteOff != value)
                {
                    _IsWriteOff = value;
                    this.NotifyPropertyChanged(o => o.IsWriteOff);
                }
            }
        }
        #endregion

        #region IsPaid

        bool _IsPaid;
        /// <summary>
        /// 到帐
        /// </summary>
        public bool IsPaid
        {
            get
            {
                return _IsPaid;
            }
            set
            {
                if (_IsPaid != value)
                {
                    _IsPaid = value;
                    this.NotifyPropertyChanged(o => o.IsPaid);
                }
            }
        }
        #endregion

        #region ReleaseDate
        DateTime? _releaseDate;
        /// <summary>
        /// 放单日期
        /// </summary>
        public DateTime? ReleaseDate
        {
            get
            {
                return _releaseDate;
            }
            set
            {
                if (_releaseDate != value)
                {
                    _releaseDate = value;
                    this.NotifyPropertyChanged(o => o.ReleaseDate);
                }
            }
        }

        string _releaseBy;
        /// <summary>
        /// 放单人
        /// </summary>
        public string ReleaseBy
        {
            get
            {
                return _releaseBy;
            }
            set
            {
                if (_releaseBy != value)
                {
                    _releaseBy = value;
                    this.NotifyPropertyChanged(o => o.ReleaseBy);
                }
            }
        }
        #endregion


        /// <summary>
        /// 放货公司
        /// </summary>
        public Guid RcCompanyID
        {
            get;
            set;
        }

        string _rcCompanyName;
        /// <summary>
        /// 放货分公司
        /// </summary>
        public string RcCompanyName
        {
            get
            {
                return _rcCompanyName;
            }
            set
            {
                if (_rcCompanyName != value)
                {
                    _rcCompanyName = value;
                    this.NotifyPropertyChanged(o => o.RcCompanyName);
                }
            }
        }

        string _rcBy;
        /// <summary>
        /// 放货人
        /// </summary>
        public string RcBy
        {
            get
            {
                return _rcBy;
            }
            set
            {
                if (_rcBy != value)
                {
                    _rcBy = value;
                    this.NotifyPropertyChanged(o => o.RcBy);
                }
            }
        }

        DateTime? _rcDate;
        /// <summary>
        /// 放货日期
        /// </summary>
        public DateTime? RcDate
        {
            get
            {
                return _rcDate;
            }
            set
            {
                if (_rcDate != value)
                {
                    _rcDate = value;
                    this.NotifyPropertyChanged(o => o.RcDate);
                }
            }
        }


        #region HasNoticedTelex
        bool _hasNoticedTelex;
        /// <summary>
        /// 已通知电放
        /// </summary>
        public bool HasNoticedTelex
        {
            get
            {
                return _hasNoticedTelex;
            }
            set
            {
                if (_hasNoticedTelex != value)
                {
                    _hasNoticedTelex = value;
                    this.NotifyPropertyChanged(o => o.HasNoticedTelex);
                }
            }
        }
        #endregion

        #region AgentReceivedTelex
        bool _AgentReceivedTelex;
        /// <summary>
        /// 代理已收到电放通知
        /// </summary>
        public bool AgentReceivedTelex
        {
            get
            {
                return _AgentReceivedTelex;
            }
            set
            {
                if (_AgentReceivedTelex != value)
                {
                    _AgentReceivedTelex = value;
                    this.NotifyPropertyChanged(o => o.AgentReceivedTelex);
                }
            }
        }
        #endregion

        #region TelexNo
        string _TelexNo;
        /// <summary>
        /// 电放号
        /// </summary>
        public string TelexNo
        {
            get
            {
                return _TelexNo;
            }
            set
            {
                if (_TelexNo != value)
                {
                    _TelexNo = value;
                    this.NotifyPropertyChanged(o => o.TelexNo);
                }
            }
        }
        #endregion

        #region HasSentOriginal
        bool _hasSentOriginal;
        /// <summary>
        /// 已发正本
        /// </summary>
        public bool HasSentOriginal
        {
            get
            {
                return _hasSentOriginal;
            }
            set
            {
                if (_hasSentOriginal != value)
                {
                    _hasSentOriginal = value;
                    this.NotifyPropertyChanged(o => o.HasSentOriginal);
                }
            }
        }
        #endregion

        #region ExpressOrderNo
        string _expressOrderNo;
        /// <summary>
        /// 快递号
        /// </summary>
        public string ExpressOrderNo
        {
            get
            {
                return _expressOrderNo;
            }
            set
            {
                if (_expressOrderNo != value)
                {
                    _expressOrderNo = value;
                    this.NotifyPropertyChanged(o => o.ExpressOrderNo);
                }
            }
        }
        #endregion

        /// <summary>
        /// 电放号或快递号
        /// </summary>
        public string TelexNoOrExpressOrderNo
        {
            get
            {
                if (this.ReleaseType == ReleaseType.Telex)
                    return this.TelexNo;
                else if (this.ReleaseType == ReleaseType.Original)
                    return this.ExpressOrderNo;
                else
                    return string.Empty;
            }
        }

        #region RcRemark
        string _RcRemark;
        /// <summary>
        /// 放货备注
        /// </summary>
        public string RcRemark
        {
            get
            {
                return _RcRemark;
            }
            set
            {
                if (_RcRemark != value)
                {
                    _RcRemark = value;
                    this.NotifyPropertyChanged(o => o.RcRemark);
                }
            }
        }
        #endregion

        #region Create Info

        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        [Required(CMessage = "建立人",EMessage="CreateBy")]
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
                    this.NotifyPropertyChanged(o => o.CreateByID);
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
                    this.NotifyPropertyChanged(o => o.CreateByName);
                }
            }
        }

        DateTime _createdate;
        /// <summary>
        /// 建立时间
        /// </summary>
        [Required(CMessage = "建立时间",EMessage="CreateDate")]
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
                    this.NotifyPropertyChanged(o => o.CreateDate);
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
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }

        DateTime? _blupdateDate;
        /// <summary>
        /// 更新时间-做数据版本控制用
        /// </summary>
        public DateTime? BLUpdateDate
        {
            get
            {
                return _blupdateDate;
            }
            set
            {
                if (_blupdateDate != value)
                {
                    _blupdateDate = value;
                    this.NotifyPropertyChanged(o => o.BLUpdateDate);
                }
            }
        }

        #endregion

        #region ApplyDate

        DateTime? _ApplyDate;
        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime? ApplyDate
        {
            get
            {
                return _ApplyDate;
            }
            set
            {
                if (_ApplyDate != value)
                {
                    _ApplyDate = value;
                    this.NotifyPropertyChanged(o => o.ApplyDate);
                }
            }
        }

        #endregion

        #region OperationType

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
                    this.NotifyPropertyChanged(o => o._OperationType);
                }
            }
        }
        #endregion
    }
}
