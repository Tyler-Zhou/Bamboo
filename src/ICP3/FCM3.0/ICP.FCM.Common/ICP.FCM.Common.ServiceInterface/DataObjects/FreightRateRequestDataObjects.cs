namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic;
    using System.Text; 
    using ICP.Framework.CommonLibrary.Common;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;

    /// <summary>
    /// FreightRateRequestList
    /// </summary>
    [Serializable]
    public partial class FreightRateRequestList : BaseDataObject
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


        Guid _oceanshippingorderid;
        /// <summary>
        /// 装货单ID
        /// </summary>
        public Guid OceanShippingOrderID
        {
            get
            {
                return _oceanshippingorderid;
            }
            set
            {
                if (_oceanshippingorderid != value)
                {
                    _oceanshippingorderid = value;
                    base.OnPropertyChanged("OceanShippingOrderID", value);
                }
            }
        }


        string _sendername;
        /// <summary>
        /// 申请人
        /// </summary>
        public string SenderName
        {
            get
            {
                return _sendername;
            }
            set
            {
                if (_sendername != value)
                {
                    _sendername = value;
                    base.OnPropertyChanged("SenderName", value);
                }
            }
        }


        DateTime _senderdate;
        /// <summary>
        /// 申请时间
        /// </summary>
        [Required(CMessage = "申请时间",EMessage="SenderDate")]
        public DateTime SenderDate
        {
            get
            {
                return _senderdate;
            }
            set
            {
                if (_senderdate != value)
                {
                    _senderdate = value;
                    base.OnPropertyChanged("SenderDate", value);
                }
            }
        }


        string _solvername;
        /// <summary>
        /// 审批人
        /// </summary>
        public string SolverName
        {
            get
            {
                return _solvername;
            }
            set
            {
                if (_solvername != value)
                {
                    _solvername = value;
                    base.OnPropertyChanged("SolverName", value);
                }
            }
        }


        DateTime? _solvedate;
        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime? SolveDate
        {
            get
            {
                return _solvedate;
            }
            set
            {
                if (_solvedate != value)
                {
                    _solvedate = value;
                    base.OnPropertyChanged("SolveDate", value);
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
        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj">obj</param>
        /// <returns>bool</returns>
        public override bool Equals(object obj)
        {
            FreightRateRequestList newObj = obj as FreightRateRequestList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// FreightRateRequestInfo
    /// </summary>
    [Serializable]
    public partial class FreightRateRequestInfo : FreightRateRequestList
    {
        Guid? _solverid;
        /// <summary>
        /// 审批人ID
        /// </summary>
        public Guid? SolverID
        {
            get
            {
                return _solverid;
            }
            set
            {
                if (_solverid != value)
                {
                    _solverid = value;
                    base.OnPropertyChanged("SolverID", value);
                }
            }
        }


        Guid _senderid;
        /// <summary>
        /// 申请人
        /// </summary>
        public Guid SenderID
        {
            get
            {
                return _senderid;
            }
            set
            {
                if (_senderid != value)
                {
                    _senderid = value;
                    base.OnPropertyChanged("SenderID", value);
                }
            }
        }


        string _senderremark;
        /// <summary>
        /// 申请备注
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="申请备注",EMessage="SenderRemark")]

        public string SenderRemark
        {
            get
            {
                return _senderremark;
            }
            set
            {
                if (_senderremark != value)
                {
                    _senderremark = value;
                    base.OnPropertyChanged("SenderRemark", value);
                }
            }
        }


        string _solverremark;
        /// <summary>
        /// 审批备注
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="审批备注",EMessage="SolverRemark")]

        public string SolverRemark
        {
            get
            {
                return _solverremark;
            }
            set
            {
                if (_solverremark != value)
                {
                    _solverremark = value;
                    base.OnPropertyChanged("SolverRemark", value);
                }
            }
        }


        Guid? _freightrateid;
        /// <summary>
        /// 运价ID（关联运价明细信息）
        /// </summary>
        public Guid? FreightRateID
        {
            get
            {
                return _freightrateid;
            }
            set
            {
                if (_freightrateid != value)
                {
                    _freightrateid = value;
                    base.OnPropertyChanged("FreightRateID", value);
                }
            }
        }


        string _freightratename;
        /// <summary>
        /// 运价名
        /// </summary>
        public string FreightRateName
        {
            get
            {
                return _freightratename;
            }
            set
            {
                if (_freightratename != value)
                {
                    _freightratename = value;
                    base.OnPropertyChanged("FreightRateName", value);
                }
            }
        }


    }
}