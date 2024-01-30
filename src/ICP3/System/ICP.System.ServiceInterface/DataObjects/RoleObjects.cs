using System;
using ICP.Framework.CommonLibrary.Common;
namespace ICP.Sys.ServiceInterface.DataObjects
{
    /// <summary>
    /// 角色岗位列表
    /// </summary>
    [Serializable]
    public partial class Role2OrganizationJobList : BaseDataObject
    {

        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// ID
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



        Guid _roleid;
        /// <summary>
        /// 角色
        /// </summary>
        [GuidRequired(CMessage = "角色",EMessage="Role")]
        public Guid RoleID
        {
            get
            {
                return _roleid;
            }
            set
            {
                if (_roleid != value)
                {
                    _roleid = value;
                    base.OnPropertyChanged("RoleID", value);
                }
            }
        }


        string _rolename;
        /// <summary>
        /// 角色
        /// </summary>
        public string RoleName
        {
            get
            {
                return _rolename;
            }
            set
            {
                if (_rolename != value)
                {
                    _rolename = value;
                    base.OnPropertyChanged("RoleName", value);
                }
            }
        }


        Guid _organizationjobid;
        /// <summary>
        /// 岗位
        /// </summary>
        [GuidRequired(CMessage = "岗位",EMessage="OrganizationJob")]
        public Guid OrganizationJobID
        {
            get
            {
                return _organizationjobid;
            }
            set
            {
                if (_organizationjobid != value)
                {
                    _organizationjobid = value;
                    base.OnPropertyChanged("OrganizationJobID", value);
                }
            }
        }


        string _organizationjobname;
        /// <summary>
        /// 岗位
        /// </summary>
        public string OrganizationJobName
        {
            get
            {
                return _organizationjobname;
            }
            set
            {
                if (_organizationjobname != value)
                {
                    _organizationjobname = value;
                    base.OnPropertyChanged("OrganizationJobName", value);
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

        string _createbyname;
        /// <summary>
        /// 创建人
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
                    base.OnPropertyChanged("CreateByName", value);
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
            Role2OrganizationJobList newObj = obj as Role2OrganizationJobList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }


    }


    /// <summary>
    /// 角色列表
    /// </summary>
    [Serializable]
    public partial class RoleList : BaseDataObject
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


        string _cname;
        /// <summary>
        /// 中文名称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "中文名", EMessage = "CName")]
        [Required(CMessage = "中文名",EMessage="CName")]
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
        /// 英文名称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "英文名", EMessage = "EName")]
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


        string _description;
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(MaximumLength = 500, CMessage = "描述", EMessage = "Description")]

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



        string _createby;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateByName
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
                    base.OnPropertyChanged("CreateByName", value);
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
            RoleList newObj = obj as RoleList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }


    /// <summary>
    /// 角色信息
    /// </summary>
    [Serializable]
    public partial class RoleInfo : RoleList
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
    }
}
