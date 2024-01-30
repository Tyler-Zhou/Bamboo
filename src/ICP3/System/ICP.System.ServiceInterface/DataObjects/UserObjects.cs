using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.ServiceInterface.DataObjects
{
    /// <summary>
    /// 用户列表
    /// </summary>
    [Serializable]
    public partial class UserList : BaseDataObject
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
        [StringLength(MaximumLength = 50, CMessage = "代码", EMessage = "Code")]
        [Required(CMessage = "代码", EMessage = "Code")]
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
        [StringLength(MaximumLength = 50, CMessage = "中文名", EMessage = "CName")]
        [Required(CMessage = "中文名", EMessage = "CName")]
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
        [StringLength(MaximumLength = 50, CMessage = "英文名", EMessage = "EName")]
        [Required(CMessage = "英文名", EMessage = "EName")]
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


        string _email;
        /// <summary>
        /// 邮箱
        /// </summary>
        [RegularExpression(Pattern = "^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", CMessage = "邮件格式不正确。例:xx@hotmail.com", EMessage = "The Email Format is not correct. example:xx@hotmail.com",IsUseErrorTemplate=false)]
        public string EMail
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    base.OnPropertyChanged("EMail", value);
                }
            }
        }


        GenderType _Gender;
        /// <summary>
        /// 性别
        /// </summary>
        public GenderType Gender
        {
            get
            {
                return _Gender;
            }
            set
            {
                if (_Gender != value)
                {
                    _Gender = value;
                    base.OnPropertyChanged("Gender", value);
                }
            }
        }


        string _tel;
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel
        {
            get
            {
                return _tel;
            }
            set
            {
                if (_tel != value)
                {
                    _tel = value;
                    base.OnPropertyChanged("Tel", value);
                }
            }
        }


        string _fax;
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax
        {
            get
            {
                return _fax;
            }
            set
            {
                if (_fax != value)
                {
                    _fax = value;
                    base.OnPropertyChanged("Fax", value);
                }
            }
        }


        string _mobile;
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile
        {
            get
            {
                return _mobile;
            }
            set
            {
                if (_mobile != value)
                {
                    _mobile = value;
                    base.OnPropertyChanged("Mobile", value);
                }
            }
        }


        bool _isvalid;
        /// <summary>
        /// 有效
        /// </summary>
        public bool IsValid
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


        DateTime? _updateDate;
        /// <summary>
        /// 版本(控制并发冲突)
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
            UserList newObj = obj as UserList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }

        public string OrganizationName
        {
            get;
            set;
        }
        public Guid JobID
        {
            get;
            set;
        }
        public string JobName
        {
            get;
            set;
        }
    }


    /// <summary>
    /// 用户信息
    /// </summary>
    [Serializable]
    public partial class UserInfo : UserList
    {
        /// <summary>
        /// 
        /// </summary>
        public UserInfo()
        {
            this.MailAccounts = new List<UserMailAccountList>();
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
        public DateTime Birthday { get; set; }
        public string Address { get;set; }

        /// <summary>
        /// MailAccounts
        /// </summary>
        public List<UserMailAccountList> MailAccounts { get; set; }
    }
    /// <summary>
    /// UserOrganizationTreeList
    /// </summary>
    [Serializable]
    public partial class UserOrganizationTreeList : OrganizationList
    {
        bool _isdefault;
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault
        {
            get
            {
                return _isdefault;
            }
            set
            {
                if (_isdefault != value)
                {
                    _isdefault = value;
                    base.OnPropertyChanged("IsDefault", value);
                }
            }
        }

        bool _hasPermission;
        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool HasPermission
        {
            get
            {
                return _hasPermission;
            }
            set
            {
                if (_hasPermission != value)
                {
                    _hasPermission = value;
                    base.OnPropertyChanged("HasPermission", value);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class User2OrganizationJobList : BaseDataObject
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


        Guid _userid;
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID
        {
            get
            {
                return _userid;
            }
            set
            {
                if (_userid != value)
                {
                    _userid = value;
                    base.OnPropertyChanged("UserID", value);
                }
            }
        }


        Guid _OrganizationJobID;
        /// <summary>
        /// 职位ID 
        /// </summary>
        [Required(CMessage = "职位",EMessage="OrganizationJob")]
        public Guid OrganizationJobID
        {
            get
            {
                return _OrganizationJobID;
            }
            set
            {
                if (_OrganizationJobID != value)
                {
                    _OrganizationJobID = value;
                    base.OnPropertyChanged("OrganizationJobID", value);
                }
            }
        }


        string _organizationJobName;
        /// <summary>
        /// 职位
        /// </summary>
        public string OrganizationJobName
        {
            get
            {
                return _organizationJobName;
            }
            set
            {
                if (_organizationJobName != value)
                {
                    _organizationJobName = value;
                    base.OnPropertyChanged("OrganizationJobName", value);
                }
            }
        }

        bool _isdefault;
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault
        {
            get
            {
                return _isdefault;
            }
            set
            {
                if (_isdefault != value)
                {
                    _isdefault = value;
                    base.OnPropertyChanged("IsDefault", value);
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

        DateTime _createdate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
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

        DateTime? _updateDate;
        /// <summary>
        /// 版本(控制并发冲突)
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
            User2OrganizationJobList newObj = obj as User2OrganizationJobList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class UserConnectionList : BaseDataObject
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


        string _username;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    base.OnPropertyChanged("UserName", value);
                }
            }
        }


        string _connectusername;
        /// <summary>
        /// 接收工作人
        /// </summary>
        public string ConnectUserName
        {
            get
            {
                return _connectusername;
            }
            set
            {
                if (_connectusername != value)
                {
                    _connectusername = value;
                    base.OnPropertyChanged("ConnectUserName", value);
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


        bool _isvalid;
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
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
            UserConnectionList newObj = obj as UserConnectionList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class UserConnectionInfo : UserConnectionList
    {
        Guid _userid;
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID
        {
            get
            {
                return _userid;
            }
            set
            {
                if (_userid != value)
                {
                    _userid = value;
                    base.OnPropertyChanged("UserID", value);
                }
            }
        }


        Guid _connectuserid;
        /// <summary>
        /// 接收人ID
        /// </summary>
        public Guid ConnectUserID
        {
            get
            {
                return _connectuserid;
            }
            set
            {
                if (_connectuserid != value)
                {
                    _connectuserid = value;
                    base.OnPropertyChanged("ConnectUserID", value);
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


    }

    #region Mail

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class MailAccount
    {
        /// <summary>
        /// 邮件帐号ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMail { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get
            {
                return this.EMail + "(" + this.UserName + ")";
            }
        }
    }

    /// <summary>
    /// UserInfoEmail
    /// </summary>
    [Serializable]
    public class UserInfoEmail
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// 中文用户名
        /// </summary>
        public string CName { get; set; }

        /// <summary>
        /// 英文用户名
        /// </summary>
        public string EName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 电话号
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// HOST
        /// </summary>
        public string MailIncomingHost { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string MailIncomingPassword { get; set; }

    }

    /// <summary>
    /// UserMailAccountList
    /// </summary>
    [Serializable]
    public partial class UserMailAccountList : BaseDataObject
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


        Guid _userid;
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID
        {
            get
            {
                return _userid;
            }
            set
            {
                if (_userid != value)
                {
                    _userid = value;
                    base.OnPropertyChanged("UserID", value);
                }
            }
        }

        string _username;
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    base.OnPropertyChanged("UserName", value);
                }
            }
        }

        string _email;
        /// <summary>
        /// 邮件
        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "邮件", EMessage = "Email")]
        [Required( CMessage = "邮件", EMessage = "Email")]
        [RegularExpression(Pattern = "^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", CMessage = "邮件格式不正确。例:xx@hotmail.com", EMessage = "The Email Format is not correct. example:xx@hotmail.com",IsUseErrorTemplate=false)]
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value == null ? null : value.Trim();
                    base.OnPropertyChanged("Email", value);
                }
            }
        }


        bool _isdefault;
        /// <summary>
        /// 
        /// </summary>
        public bool IsDefault
        {
            get
            {
                return _isdefault;
            }
            set
            {
                if (_isdefault != value)
                {
                    _isdefault = value;
                    base.OnPropertyChanged("IsDefault", value);
                }
            }
        }


        MailProtocol _mailincomingprotocol = MailProtocol.POP;
        /// <summary>
        /// 
        /// </summary>
        public MailProtocol MailIncomingProtocol
        {
            get
            {
                return _mailincomingprotocol;
            }
            set
            {
                if (_mailincomingprotocol != value)
                {
                    _mailincomingprotocol = value;
                    base.OnPropertyChanged("MailIncomingProtocol", value);
                }
            }
        }


        string _mailincominghost;
        /// <summary>
        /// 
        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "邮件服务器", EMessage = "MailIncomingHost")]
        [Required(CMessage = "邮件服务器", EMessage = "MailIncomingHost")]
        [RegularExpression(Pattern="([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%]*)?", CMessage = "邮件服务器格式不正确。例:mail.sina.com",EMessage="TheMailIncomingHost Format is not correct. example:mail.sina.com",IsUseErrorTemplate=false)]
        public string MailIncomingHost
        {
            get
            {
                return _mailincominghost;
            }
            set
            {
                if (_mailincominghost != value)
                {
                    if (string.IsNullOrEmpty(_mailoutgoinghost))
                    {
                        _mailoutgoinghost = value == null ? null : value.Trim();
                    }
                    _mailincominghost = value == null ? null : value.Trim();
                    base.OnPropertyChanged("MailIncomingHost", value);
                }
            }
        }


        int _mailincomingport = 110;
        /// <summary>
        /// 
        /// </summary>
        public int MailIncomingPort
        {
            get
            {
                return _mailincomingport;
            }
            set
            {
                if (_mailincomingport != value)
                {

                    _mailincomingport = value;
                    base.OnPropertyChanged("MailIncomingPort", value);
                }
            }
        }


        string _mailincominglogin;
        /// <summary>
        /// 登录名
        /// </summary>
        [Required(CMessage = "登录名",EMessage="MailInCommingLogin")]
        [StringLength(MaximumLength=50, CMessage = "登录名", EMessage = "MailInCommingLogin")]
        public string MailIncomingLogin
        {
            get
            {
                return _mailincominglogin;
            }
            set
            {
                if (_mailincominglogin != value)
                {
                    if (string.IsNullOrEmpty(_mailoutgoinglogin))
                    {
                        _mailoutgoinglogin = value;
                    }

                    _mailincominglogin = value;
                    base.OnPropertyChanged("MailIncomingLogin", value);
                }
            }
        }


        string _mailincomingpassword;
        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(CMessage = "登录密码",EMessage="MailIncomingPassword")]
        [StringLength(MaximumLength = 50, CMessage = "登录密码", EMessage = "MailIncomingPassword")]
        public string MailIncomingPassword
        {
            get
            {
                return _mailincomingpassword;
            }
            set
            {
                if (_mailincomingpassword != value)
                {
                    if (string.IsNullOrEmpty(_mailioutgoingpassword))
                    {
                        _mailioutgoingpassword = value;
                    }

                    _mailincomingpassword = value;
                    base.OnPropertyChanged("MailIncomingPassword", value);
                }
            }
        }


        MailProtocol _mailoutgoingprotocol = MailProtocol.SMTP;
        /// <summary>
        /// 
        /// </summary>
        public MailProtocol MailOutgoingProtocol
        {
            get
            {
                return _mailoutgoingprotocol;
            }
            set
            {
                if (_mailoutgoingprotocol != value)
                {
                    _mailoutgoingprotocol = value;
                    base.OnPropertyChanged("MailOutgoingProtocol", value);
                }
            }
        }


        string _mailoutgoinghost;
        /// <summary>
        /// 
        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "邮件服务器", EMessage = "MailOutgoingHost")]
        [Required(CMessage = "邮件服务器", EMessage = "MailOutgoingHost")]
        [RegularExpression(Pattern = "([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%]*)?", CMessage = "邮件服务器格式不正确。例:mail.sina.com", EMessage = "The MailOutgoingHost Format is not correct. example:mail.sina.com", IsUseErrorTemplate = false)]
        public string MailOutgoingHost
        {
            get
            {
                return _mailoutgoinghost;
            }
            set
            {
                if (_mailoutgoinghost != value)
                {
                    if (string.IsNullOrEmpty(_mailincominghost))
                    {
                        _mailincominghost = value == null ? null : value.Trim();
                    }

                    _mailoutgoinghost = value == null ? null : value.Trim();
                    base.OnPropertyChanged("MailOutgoingHost", value);
                }
            }
        }


        int _mailoutgoingport = 25;
        /// <summary>
        /// 
        /// </summary>
        public int MailOutgoingPort
        {
            get
            {
                return _mailoutgoingport;
            }
            set
            {
                if (_mailoutgoingport != value)
                {
                    _mailoutgoingport = value;
                    base.OnPropertyChanged("MailOutgoingPort", value);
                }
            }
        }


        string _mailoutgoinglogin;
        /// <summary>
        /// 登录名
        /// </summary>
        [Required(CMessage = "登录名",EMessage="MaiOutgoingLogin")]
        [StringLength(MaximumLength = 50, CMessage = "登录名", EMessage = "MaiOutgoingLogin")]
        public string MailOutgoingLogin
        {
            get
            {
                return _mailoutgoinglogin;
            }
            set
            {
                if (_mailoutgoinglogin != value)
                {
                    if (string.IsNullOrEmpty(_mailincominglogin))
                    {
                        _mailincominglogin = value;
                    }

                    _mailoutgoinglogin = value;
                    base.OnPropertyChanged("MailOutgoingLogin", value);
                }
            }
        }


        string _mailioutgoingpassword;
        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(CMessage = "登录密码",EMessage="MailOutgoingPassword")]
        [StringLength(MaximumLength = 50, CMessage = "登录密码", EMessage = "MailOutgoingPassword")]
        public string MailOutgoingPassword
        {
            get
            {
                return _mailioutgoingpassword;
            }
            set
            {
                if (_mailioutgoingpassword != value)
                {
                    if (string.IsNullOrEmpty(_mailincomingpassword))
                    {
                        _mailincomingpassword = value;
                    }


                    _mailioutgoingpassword = value;
                    base.OnPropertyChanged("MailOutgoingPassword", value);
                }
            }
        }


        string _friendlyname;
        /// <summary>
        /// 
        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "名字", EMessage = "FriendlyName")]
        public string FriendlyName
        {
            get
            {
                return _friendlyname;
            }
            set
            {
                if (_friendlyname != value)
                {
                    _friendlyname = value;
                    base.OnPropertyChanged("FriendlyName", value);
                }
            }
        }


        bool _getmailatlogin;
        /// <summary>
        /// 
        /// </summary>
        public bool GetMailAtLogin
        {
            get
            {
                return _getmailatlogin;
            }
            set
            {
                if (_getmailatlogin != value)
                {
                    _getmailatlogin = value;
                    base.OnPropertyChanged("GetMailAtLogin", value);
                }
            }
        }


        bool _isvalid;
        /// <summary>
        /// 
        /// </summary>
        public bool IsValid
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


        DateTime? _updateDate;
        /// <summary>
        /// 版本(控制并发冲突)
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
            UserMailAccountList newObj = obj as UserMailAccountList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class UserMailAccountInfo : UserMailAccountList
    {

    }

    #endregion

    /// <summary>
    /// UserDetailInfo
    /// </summary>
    [Serializable]
    public class UserDetailInfo : UserList
    {
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

        #region  Birthday
        DateTime? _Birthday;
        /// <summary>
        /// Birthday
        /// </summary>
        public DateTime? Birthday
        {
            get
            {
                return _Birthday;
            }
            set
            {
                if (_Birthday != value)
                {
                    _Birthday = value;
                    base.OnPropertyChanged("Birthday", value);
                }
            }
        }
        #endregion

        #region  Address
        string _Address;
        /// <summary>
        /// Address
        /// </summary>
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                if (_Address != value)
                {
                    _Address = value;
                    base.OnPropertyChanged("Address", value);
                }
            }
        }
        #endregion

        #region Email Password
        string _password;
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string EmailPassword
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    base.OnPropertyChanged("EmailPassword", value);
                }
            }
        }
#endregion

        #region  Department

        public Guid DepartmentID
        {
            get;
            set;
        }
        /// <summary>
        /// 默认部门简称
        /// </summary>
        public string DepartmentName
        {
            get;
            set;
        }

        string _Department;
        /// <summary>
        /// 默认部门全称
        /// </summary>
        public string Department
        {
            get
            {
                return _Department;
            }
            set
            {
                if (_Department != value)
                {
                    _Department = value;
                    base.OnPropertyChanged("Department", value);
                }
            }
        }
        #endregion

        #region  Remark
        string _Remark;
        /// <summary>
        /// Remark
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
    /// UserDetailInfo
    /// </summary>
    [Serializable]
    public class EmailAccountSignature : BaseDataObject
    {
        Guid _id;
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
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
                    base.OnPropertyChanged("Id", value);
                }
            }
        }

        Guid _acountid;
        /// <summary>
        /// AccountID
        /// </summary>
        public Guid AccountID
        {
            get
            {
                return _acountid;
            }
            set
            {
                if (_acountid != value)
                {
                    _acountid = value;
                    base.OnPropertyChanged("AccountID", value);
                }
            }
        }

        int _type;
        /// <summary>
        /// Type
        /// </summary>
        public int Type
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

        string _signature;
        /// <summary>
        /// Signature
        /// </summary>
        public string Signature
        {
            get
            {
                return _signature;
            }
            set
            {
                if (_signature != value)
                {
                    _signature = value;
                    base.OnPropertyChanged("Signature", value);
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

        DateTime _createbydate;
        /// <summary>
        /// CreateByDate
        /// </summary>
        public DateTime CreateByDate
        {
            get
            {
                return _createbydate;
            }
            set
            {
                if (_createbydate != value)
                {
                    _createbydate = value;
                    base.OnPropertyChanged("CreateByDate", value);
                }
            }
        }

        Guid? _updatebyid;
        /// <summary>
        /// UpdateById
        /// </summary>
        public Guid? UpdateById
        {
            get
            {
                return _updatebyid;
            }
            set
            {
                if (_updatebyid != value)
                {
                    _updatebyid = value;
                    base.OnPropertyChanged("UpdateById", value);
                }
            }
        }

        DateTime? _updatebydate;
        /// <summary>
        /// UpdateByDate
        /// </summary>
        public DateTime? UpdateByDate
        {
            get
            {
                return _updatebydate;
            }
            set
            {
                if (_updatebydate != value)
                {
                    _updatebydate = value;
                    base.OnPropertyChanged("UpdateByDate", value);
                }
            }
        }

    }
}
