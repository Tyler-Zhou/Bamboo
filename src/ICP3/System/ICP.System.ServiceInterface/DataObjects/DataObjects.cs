using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.ServiceInterface.DataObjects
{


    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class AuthcodeList : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一ID
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


        string _authcode;
        /// <summary>
        /// 网卡地址
        /// </summary>
        public string AuthCode
        {
            get
            {
                return _authcode;
            }
            set
            {
                if (_authcode != value)
                {
                    _authcode = value;
                    base.OnPropertyChanged("AuthCode", value);
                }
            }
        }


        string _sendername;
        /// <summary>
        /// 发送人名
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


        DateTime _senddate;
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendDate
        {
            get
            {
                return _senddate;
            }
            set
            {
                if (_senddate != value)
                {
                    _senddate = value;
                    base.OnPropertyChanged("SendDate", value);
                }
            }
        }


        string _approvername;
        /// <summary>
        /// 审批人
        /// </summary>
        public string ApproverName
        {
            get
            {
                return _approvername;
            }
            set
            {
                if (_approvername != value)
                {
                    _approvername = value;
                    base.OnPropertyChanged("ApproverName", value);
                }
            }
        }


        DateTime _approvedate;
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime ApproveDate
        {
            get
            {
                return _approvedate;
            }
            set
            {
                if (_approvedate != value)
                {
                    _approvedate = value;
                    base.OnPropertyChanged("ApproveDate", value);
                }
            }
        }


        bool _state;
        /// <summary>
        /// 状态
        /// </summary>
        public bool State
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


        DateTime? _updatedate;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _updatedate;
            }
            set
            {
                if (_updatedate != value)
                {
                    _updatedate = value;
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
            AuthcodeList newObj = obj as AuthcodeList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class AuthcodeInfo : AuthcodeList
    {
        string _senderremark;
        /// <summary>
        /// 申请备注
        /// </summary>
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


        string _approverremark;
        /// <summary>
        /// 审批备注
        /// </summary>
        public string ApproverRemark
        {
            get
            {
                return _approverremark;
            }
            set
            {
                if (_approverremark != value)
                {
                    _approverremark = value;
                    base.OnPropertyChanged("ApproverRemark", value);
                }
            }
        }


        Guid _senderid;
        /// <summary>
        /// 发送人Id
        /// </summary>
        public Guid SenderId
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
                    base.OnPropertyChanged("SenderId", value);
                }
            }
        }


        Guid _approverid;
        /// <summary>
        /// 处理人Id
        /// </summary>
        public Guid ApproverId
        {
            get
            {
                return _approverid;
            }
            set
            {
                if (_approverid != value)
                {
                    _approverid = value;
                    base.OnPropertyChanged("ApproverId", value);
                }
            }
        }


    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class ErrorLogList : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一ID
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


        string _projectname;
        /// <summary>
        /// 项目
        /// </summary>
        public string ProjectName
        {
            get
            {
                return _projectname;
            }
            set
            {
                if (_projectname != value)
                {
                    _projectname = value;
                    base.OnPropertyChanged("ProjectName", value);
                }
            }
        }


        string _errorcontent;
        /// <summary>
        /// 错误内容
        /// </summary>
        public string ErrorContent
        {
            get
            {
                return _errorcontent;
            }
            set
            {
                if (_errorcontent != value)
                {
                    _errorcontent = value;
                    base.OnPropertyChanged("ErrorContent", value);
                }
            }
        }


        string _sendername;
        /// <summary>
        /// 发送人
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
        /// 发送时间
        /// </summary>
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
        /// 处理人
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


        DateTime _solvedate;
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime SolveDate
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


        string _solverremark;
        /// <summary>
        /// 备注
        /// </summary>
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


        short _state;
        /// <summary>
        /// 状态
        /// </summary>
        public short State
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


        DateTime? _updatedate;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _updatedate;
            }
            set
            {
                if (_updatedate != value)
                {
                    _updatedate = value;
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
            ErrorLogList newObj = obj as ErrorLogList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class ErrorLogInfo : ErrorLogList
    {
        Guid _senderid;
        /// <summary>
        /// 发送人Id
        /// </summary>
        public Guid SenderId
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
                    base.OnPropertyChanged("SenderId", value);
                }
            }
        }


        Guid _solverid;
        /// <summary>
        /// 处理人Id
        /// </summary>
        public Guid SolverId
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
                    base.OnPropertyChanged("SolverId", value);
                }
            }
        }


    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class ActionList : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一ID
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


        string _code;
        /// <summary>
        /// 代码
        /// </summary>
        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                if (_code != value)
                {
                    _code = value;
                    base.OnPropertyChanged("Code", value);
                }
            }
        }


        string _cname;
        /// <summary>
        /// 中文名 
        /// </summary>
        public string CName
        {
            get
            {
                return _cname;
            }
            set
            {
                if (_cname != value)
                {
                    _cname = value;
                    base.OnPropertyChanged("CName", value);
                }
            }
        }


        string _ename;
        /// <summary>
        /// 英文名
        /// </summary>
        public string EName
        {
            get
            {
                return _ename;
            }
            set
            {
                if (_ename != value)
                {
                    _ename = value;
                    base.OnPropertyChanged("EName", value);
                }
            }
        }


        string _description;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    base.OnPropertyChanged("Description", value);
                }
            }
        }


        string _isvalid;
        /// <summary>
        /// 是否有效
        /// </summary>
        public string IsValid
        {
            get
            {
                return _isvalid;
            }
            set
            {
                if (_isvalid != value)
                {
                    _isvalid = value;
                    base.OnPropertyChanged("IsValid", value);
                }
            }
        }


        DateTime? _updatedate;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _updatedate;
            }
            set
            {
                if (_updatedate != value)
                {
                    _updatedate = value;
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
            ActionList newObj = obj as ActionList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class ActionInfo : ActionList
    {
        bool _enable;
        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool Enable
        {
            get
            {
                return _enable;
            }
            set
            {
                if (_enable != value)
                {
                    _enable = value;
                    base.OnPropertyChanged("Enable", value);
                }
            }
        }


        string _createby;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy
        {
            get
            {
                return _createby;
            }
            set
            {
                if (_createby != value)
                {
                    _createby = value;
                    base.OnPropertyChanged("CreateBy", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid CreateById
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
                    base.OnPropertyChanged("CreateById", value);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 创建时间
        /// </summary>
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
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }
    }

    #region FTPServerConfig

    /// <summary>
    /// FTP服务器配置信息
    /// </summary>
    [Serializable]
    public class FTPServerConfig
    {
        public FTPServerConfig()
        {
        }

        public FTPServerConfig(
            string host,
            string user,
            string password,
            string basePath)
        {
            this.Host = host;
            this.User = user;
            this.Password = password;
            this.BasePath = basePath;
        }

        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 本地路径
        /// </summary>
        public string BasePath { get; set; }
    }

    #endregion
}
