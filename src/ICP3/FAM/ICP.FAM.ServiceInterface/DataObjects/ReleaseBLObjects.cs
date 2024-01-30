namespace ICP.FAM.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic;
    using Framework.CommonLibrary.Common;
    using FCM.Common.ServiceInterface.DataObjects;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// 放单列表上的数据的实体
    /// 创建时间：2011-07-11 15:52
    /// 作者：熊中方
    /// </summary>
    [Serializable]
    public class ReleaseBLList : BaseDataObject
    {
        #region IsNew
        /// <summary>
        /// 根据主键判断是否新增数据
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } } 
        #endregion

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

        #region Selected
        /// <summary>
        /// 已选择,客户端帮助属性
        /// </summary>
        public bool Selected { get; set; } 
        #endregion

        #region ReleaseBLState
        ReleaseBLState _releaseState;
        /// <summary>
        /// 放单状态
        /// </summary>
        public ReleaseBLState State
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

        #region BLID
        Guid? _blID;
        /// <summary>
        /// 提单ID
        /// </summary>
        public Guid? BLID
        {
            get
            {
                return _blID;
            }
            set
            {
                if (_blID != value)
                {
                    _blID = value;
                    this.NotifyPropertyChanged(o => o.BLID);
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

        FormType _FormType;
        /// <summary>
        /// 提单类型
        /// </summary>
        public FormType FormType
        {
            get
            {
                return _FormType;
            }
            set
            {
                if (_FormType != value)
                {
                    _FormType = value;
                    this.NotifyPropertyChanged(o => o.FormType);
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

        #region Customer
        string _customer;
        /// <summary>
        /// 客户
        /// </summary>
        public string Customer
        {
            get
            {
                return _customer;
            }
            set
            {
                if (_customer != value)
                {
                    _customer = value;
                    this.NotifyPropertyChanged(o => o.Customer);
                }
            }
        }
        #endregion

        #region CustomerCName
        string _customerCName;
        /// <summary>
        /// 客户中文名称
        /// </summary>
        public string CustomerCName
        {
            get
            {
                return _customerCName;
            }
            set
            {
                if (_customerCName != value)
                {
                    _customerCName = value;
                    this.NotifyPropertyChanged(o => o.CustomerCName);
                }
            }
        }
        #endregion

        #region CustomerEName
        string _customerEName;
        /// <summary>
        /// 客户英文名称
        /// </summary>
        public string CustomerEName
        {
            get { return _customerEName; }
            set
            {
                if (_customerEName != value)
                {
                    _customerEName = value;
                    this.NotifyPropertyChanged(o => o.CustomerEName);
                }
            }
        }
        #endregion

        #region CustomerID
        Guid _customerID;
        /// <summary>
        /// 客户
        /// </summary>
        [GuidRequired(CMessage = "客户", EMessage = "Customer")]
        public Guid CustomerID
        {
            get
            {
                return _customerID;
            }
            set
            {
                if (_customerID != value)
                {
                    _customerID = value;
                    this.NotifyPropertyChanged(o => o.CustomerID);
                }
            }
        } 
        #endregion

        #region CustomerContact

        string _customerContact;
        /// <summary>
        /// 客户联系人
        /// </summary>
        public string CustomerContact
        {
            get
            {
                return _customerContact;
            }
            set
            {
                if (_customerContact != value)
                {
                    _customerContact = value;
                    this.NotifyPropertyChanged(o => o.CustomerContact);
                }
            }
        }
        #endregion

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

        #region IsAlwaySWB
        bool _IsAlwaySWB;
        /// <summary>
        /// 总SWB
        /// </summary>
        public bool IsAlwaySWB
        {
            get
            {
                return _IsAlwaySWB;
            }
            set
            {
                if (_IsAlwaySWB != value)
                {
                    _IsAlwaySWB = value;
                    this.NotifyPropertyChanged(o => o._IsAlwaySWB);
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

        #region ArEmail
        string _ArEmail;
        /// <summary>
        /// 电放号
        /// </summary>
        public string ArEmail
        {
            get
            {
                return _ArEmail;
            }
            set
            {
                if (_ArEmail != value)
                {
                    _ArEmail = value;
                    this.NotifyPropertyChanged(o => o.ArEmail);
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

        #region TelexNoOrExpressOrderNo
        /// <summary>
        /// 电放号或快递号
        /// </summary>
        public string TelexNoOrExpressOrderNo
        {
            get
            {
                if (ReleaseType == ReleaseType.Telex)
                    return TelexNo;
                //else if (this.ReleaseType == ReleaseType.Original)
                //    return this.ExpressOrderNo; 
                else
                    return string.Empty;
            }
        } 
        #endregion

        #region Remark
        string _Remark;
        /// <summary>
        /// 备注
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
                    this.NotifyPropertyChanged(o => o.Remark);
                }
            }
        }
        #endregion

        #region Create Info

        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        [Required(CMessage = "建立人", EMessage = "CreateBy")]
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
        [Required(CMessage = "建立时间", EMessage = "CreateDate")]
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

        #region ConsigneeCName
        string _ConsigneeCName;
        /// <summary>
        /// 收货人中文名称
        /// </summary>
        public string ConsigneeCName
        {
            get { return _ConsigneeCName; }
            set
            {
                if (_ConsigneeCName != value)
                {
                    _ConsigneeCName = value;
                    this.NotifyPropertyChanged(o => o.ConsigneeCName);
                }
            }
        }
        #endregion

        #region ConsigneeEName
        string _ConsigneeEName;
        /// <summary>
        /// 收货人英文名称
        /// </summary>
        public string ConsigneeEName
        {
            get { return _ConsigneeEName; }
            set
            {
                if (_ConsigneeEName != value)
                {
                    _ConsigneeEName = value;
                    this.NotifyPropertyChanged(o => o.ConsigneeEName);
                }
            }
        }
        #endregion

        #region OperationNo
        string _OperationNo;
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo
        {
            get { return _OperationNo; }
            set
            {
                if (_OperationNo != value)
                {
                    _OperationNo = value;
                    this.NotifyPropertyChanged(o => o.OperationNo);
                }
            }
        }
        #endregion

        #region 提单类型
        /// <summary>
        /// 提单类型
        /// </summary>
        public FCMBLType BLType
        {
            get
            {
                if (MBLID != null && MBLID.Value != Guid.Empty)
                {
                    return FCMBLType.MBL;
                }
                else
                {
                    return FCMBLType.HBL;
                }
            }
        }

        #endregion

        #region MBLID
        Guid? _mBLID;
        public Guid? MBLID
        {
            get
            {
                return _mBLID;
            }
            set
            {
                if (_mBLID != value)
                {
                    _mBLID = value;
                    base.OnPropertyChanged("MBLID", value);
                }
            }
        }

        #endregion

        #region AgentID
        Guid? _agentID;
        public Guid? AgentID
        {
            get
            {
                return _agentID;
            }
            set
            {
                if (_agentID != value)
                {
                    _agentID = value;
                    base.OnPropertyChanged("AgentID", value);
                }
            }
        }

        #endregion

        #region FilerEmail
        /// <summary>
        /// 客服Email
        /// </summary>
        public string FilerEmail { get; set; } 
        #endregion

        #region POLFilerEmail
        /// <summary>
        /// 目的港客服Email
        /// </summary>
        public string POLFilerEmail { get; set; } 
        #endregion

        #region 放单类型&状态
        /// <summary>
        /// 是否取消过放单，放单时即时发送邮件
        /// </summary>
        bool _isCRelease;
        public bool IsCRelease
        {
            get { return _isCRelease; }
            set { if (_isCRelease != value) _isCRelease = value; base.OnPropertyChanged("IsCRelease", value); }
        }

        /// <summary>
        ///  申请放单
        /// </summary>
        bool _rbla;
        public bool RBLA
        {
            get { return _rbla; }
            set { if (_rbla != value) _rbla = value; base.OnPropertyChanged("RBLA", value); }
        }
        /// <summary>
        ///  放单
        /// </summary>
        bool _rbld;
        public bool RBLD
        {
            get { return _rbld; }
            set { if (_rbld != value) _rbld = value; base.OnPropertyChanged("RBLD", value); }
        }
        /// <summary>
        ///  申请放单
        /// </summary>
        bool _rblrcv;
        public bool RBLRcv
        {
            get { return _rblrcv; }
            set { if (_rblrcv != value) _rblrcv = value; base.OnPropertyChanged("RBLRcv", value); }
        }

        /// <summary>
        ///  收到正本放单
        /// </summary>
        bool _oblrec;
        public bool OBLRec
        {
            get { return _oblrec; }
            set { if (_oblrec != value) _oblrec = value; base.OnPropertyChanged("OBLRec", value); }
        }

        /// <summary>
        /// 放货
        /// </summary>
        bool _blrc;
        public bool BLRC
        {
            get { return _blrc; }
            set { if (_blrc != value) _blrc = value; base.OnPropertyChanged("BLRC", value); }
        }

        #endregion

        #region IsMBL
        /// <summary>
        /// 是否为MBL
        /// </summary>
        bool isMBL;
        /// <summary>
        /// 
        /// </summary>
        public bool IsMBL
        {
            get
            {
                return isMBL;
            }
            set
            {
                if (isMBL != value)
                {
                    isMBL = value;
                    base.OnPropertyChanged("IsMBL", value);
                }
            }
        } 
        #endregion

        #region IsNotReForHbl
        /// <summary>
        /// HBL是否没有放单
        /// </summary>
        bool isNotReForHbl;
        /// <summary>
        /// HBL是否没有放单
        /// </summary>
        public bool IsNotReForHbl
        {
            get
            {
                return isNotReForHbl;
            }
            set
            {
                if (isNotReForHbl != value)
                {
                    isNotReForHbl = value;
                    base.OnPropertyChanged("IsNotReForHbl", value);
                }
            }
        } 
        #endregion

        #region 是否为指定货MBL
        /// <summary>
        /// 是否为指定货MBL
        /// </summary>
        bool isOverSeaMBL;
        /// <summary>
        /// 是否为指定货MBL
        /// </summary>
        public bool IsOverSeaMBL
        {
            get
            {
                return isOverSeaMBL;
            }
            set
            {
                if (isOverSeaMBL != value)
                {
                    isOverSeaMBL = value;
                    base.OnPropertyChanged("IsOverMBL", value);
                }
            }
        }

        #endregion

        #region 是否不自动发送放单邮件
        /// <summary>
        /// 是否不自动发送放单邮件MBL
        /// </summary>
        bool isExRelease;
        /// <summary>
        /// 是否为指定货MBL
        /// </summary>
        public bool IsExRelease
        {
            get
            {
                return isExRelease;
            }
            set
            {
                if (isExRelease != value)
                {
                    isExRelease = value;
                    base.OnPropertyChanged("IsExRelease", value);
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 放单代理联系人的邮箱
    /// </summary>
    [Serializable]
    public class ContactList : BaseDataObject
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 代理ID
        /// </summary>
        public Guid AgentID { get; set; }
        /// <summary>
        /// 联系人Email
        /// </summary>
        public string ContactEmail { get; set; }
        /// <summary>
        /// 期限时间后发送邮件
        /// </summary>
        public int EmailSendTime { get; set; }

    }

    /// <summary>
    /// 需要催放单列表
    /// </summary>
    [Serializable]
    public class ReleaseAndArList : BaseDataObject
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// OperationID
        /// </summary>
        public Guid OperationID { get; set; }

        /// <summary>
        /// CompanyID
        /// </summary>
        public Guid CompanyID { get; set; }

        /// <summary>
        /// CompanyName
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// No
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// SONO
        /// </summary>
        public string SONO { get; set; }

        /// <summary>
        /// 揽货人ID
        /// </summary>
        public Guid? SalesID { get; set; }

        /// <summary>
        /// 揽货人名称
        /// </summary>
        public string SalesName { get; set; }

        /// <summary>
        /// 揽货人邮箱
        /// </summary>
        public string SalesEmail { get; set; }

        /// <summary>
        /// 客服ID
        /// </summary>
        public Guid? BookingerID { get; set; }

        /// <summary>
        /// 客服名称
        /// </summary>
        public string BookingerName { get; set; }

        /// <summary>
        /// 客服邮箱
        /// </summary>
        public string BookingerEmail { get; set; }

        /// <summary>
        /// ETA
        /// </summary>
        public DateTime ETA { get; set; }

        /// <summary>
        /// 卸货港ID
        /// </summary>
        public Guid? PODID { get; set; }

        /// <summary>
        /// 卸货港名称
        /// </summary>
        public string PODName { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid? CustomerID { get; set; }

        /// <summary>
        /// 距离ETA天数
        /// </summary>
        public int Days { get; set; }
    }

      /// <summary>
    /// 例外放单列表
    /// </summary>
    [Serializable]
    public class ExRelease : BaseDataObject
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// ReleaseID
        /// </summary>
        public Guid ReleaseID { get; set; }

        /// <summary>
        /// CompanyID
        /// </summary>
        public Guid CreateBy { get; set; }

        /// <summary>
        /// CompanyName
        /// </summary>
        public DateTime CreateDate { get; set; }
    }

    /// <summary>
    /// ReleasePageList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [KnownType(typeof(ReleaseBLList))]
    public class ReleasePageList : PageList
    {
        /// <summary>
        /// 
        /// </summary>
        public ReleasePageList()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="dataPageInfo"></param>
        /// <returns></returns>
        static public ReleasePageList Create<T>(List<T> list, DataPageInfo dataPageInfo)
        {
            var dc = new ReleasePageList();
            dc.innerList = new List<object>(list.Cast<object>());
            dc.DataPageInfo = dataPageInfo;
            return dc;
        }
        /// <summary>
        /// 已创建行数. 需求:显示汇总：您有xx条新放单记录(count 放单 if 状态=已创建)
        /// </summary>
        public int CreatedCount { get; set; }

        /// <summary>
        /// 已签收行数.需求:显示汇总：xx条未放单的记录(count 放单 if 状态=已签收)
        /// </summary>
        public int IssueCount { get; set; }
    }
}
