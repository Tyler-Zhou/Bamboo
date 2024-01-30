namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic;
    using System.Text; 
    using ICP.Framework.CommonLibrary.Common;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;

    /// <summary>
    /// AgentRequestList
    /// </summary>
    [Serializable]
    public partial class AgentRequestList : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        string _operationNo;
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo
        {
            get
            {
                return _operationNo;
            }
            set
            {
                if (_operationNo != value)
                {
                    _operationNo = value;
                    base.OnPropertyChanged("OperationNo", value);
                }
            }
        }

        string _pod;
        /// <summary>
        /// 卸货港名称
        /// </summary>
        public string POD
        {
            get
            {
                return _pod;
            }
            set
            {
                if (_pod != value)
                {
                    _pod = value;
                    base.OnPropertyChanged("POD", value);
                }
            }
        }


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


        Guid _operationID;
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID
        {
            get
            {
                return _operationID;
            }
            set
            {
                if (_operationID != value)
                {
                    _operationID = value;
                    base.OnPropertyChanged("OperationID", value);
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


        DateTime _sendDate;
        /// <summary>
        /// 申请时间
        /// </summary>
        [Required(CMessage = "申请时间",EMessage="SendDate")]
        public DateTime SendDate
        {
            get
            {
                return _sendDate;
            }
            set
            {
                if (_sendDate != value)
                {
                    _sendDate = value;
                    base.OnPropertyChanged("SendDate", value);
                }
            }
        }


        AgentType _type;
        /// <summary>
        /// 要求代理类型(0:普通,1:第三方代理,2:需要对收款有特殊要求的代理)
        /// </summary>
        [Required(CMessage = "代理类型",EMessage="Type")]
        public AgentType Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    base.OnPropertyChanged("Type", value);
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


        AgentRequestStateEnum _state;
        /// <summary>
        /// 代理状态(0:不确定,1:申请中,2:已回复3.打回)
        /// </summary>
        public AgentRequestStateEnum State
        {
            get
            {
                return _state;
            }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    base.OnPropertyChanged("State", value);
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
            AgentRequestList newObj = obj as AgentRequestList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// AgentRequestInfo
    /// </summary>
    [Serializable]
    public partial class AgentRequestInfo : AgentRequestList
    {
        Guid _senderid;
        /// <summary>
        /// 申请人
        /// </summary>
        [GuidRequired(CMessage = "申请人",EMessage="Sender")]
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


        Guid? _agentid;
        /// <summary>
        /// 代理ID
        /// </summary>
        public Guid? AgentID
        {
            get
            {
                return _agentid;
            }
            set
            {
                if (_agentid != value)
                {
                    _agentid = value;
                    base.OnPropertyChanged("AgentID", value);
                }
            }
        }


        string _agentname;
        /// <summary>
        /// 代理
        /// </summary>
        public string AgentName
        {
            get
            {
                return _agentname;
            }
            set
            {
                if (_agentname != value)
                {
                    _agentname = value;
                    base.OnPropertyChanged("AgentName", value);
                }
            }
        }


    }
}
