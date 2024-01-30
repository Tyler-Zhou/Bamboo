using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.ServiceInterface.DataObjects
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class FunctionList : BaseDataObject
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


        string _commandname;
        /// <summary>
        /// 命令名
        /// </summary>
        public string CommandName
        {
            get
            {
                return _commandname;
            }
            set
            {
                if (_commandname != value)
                {
                    _commandname = value;
                    base.OnPropertyChanged("CommandName", value);
                }
            }
        }


        string _site;
        /// <summary>
        /// 菜单项组容器位置
        /// </summary>
        public string Site
        {
            get
            {
                return _site;
            }
            set
            {
                if (_site != value)
                {
                    _site = value;
                    base.OnPropertyChanged("Site", value);
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


        DateTime? _updatedate;
        /// <summary>
        /// 版本(控制并发冲突)
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


        Guid _parentid;
        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid ParentID
        {
            get
            {
                return _parentid;
            }
            set
            {
                if (_parentid != value)
                {
                    _parentid = value;
                    base.OnPropertyChanged("ParentID", value);
                }
            }
        }


        string _parentname;
        /// <summary>
        /// 父节点名
        /// </summary>
        public string ParentName
        {
            get
            {
                return _parentname;
            }
            set
            {
                if (_parentname != value)
                {
                    _parentname = value;
                    base.OnPropertyChanged("ParentName", value);
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


        FunctionType _functiontype;
        /// <summary>
        /// 节点类型
        /// </summary>
        public FunctionType FunctionType
        {
            get
            {
                return _functiontype;
            }
            set
            {
                if (_functiontype != value)
                {
                    _functiontype = value;
                    base.OnPropertyChanged("FunctionType", value);
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
            FunctionList newObj = obj as FunctionList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class FunctionInfo : FunctionList
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

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class RoleAndUserPermissionInfo : BaseDataObject
    {
        List<RolePermissionList> _rolepermissions;
        /// <summary>
        /// 角色权限列表
        /// </summary>
        public List<RolePermissionList> RolePermissions
        {
            get
            {
                return _rolepermissions;
            }
            set
            {
                if (_rolepermissions != value)
                {
                    _rolepermissions = value;
                    base.OnPropertyChanged("RolePermissions", value);
                }
            }
        }


        List<UserPermissionList> _userpermissions;
        /// <summary>
        /// 用户权限列表
        /// </summary>
        public List<UserPermissionList> UserPermissions
        {
            get
            {
                return _userpermissions;
            }
            set
            {
                if (_userpermissions != value)
                {
                    _userpermissions = value;
                    base.OnPropertyChanged("UserPermissions", value);
                }
            }
        }


        Guid _permissionid;
        /// <summary>
        /// 功能项或则动作项ID
        /// </summary>
        public Guid PermissionId
        {
            get
            {
                return _permissionid;
            }
            set
            {
                if (_permissionid != value)
                {
                    _permissionid = value;
                    base.OnPropertyChanged("PermissionId", value);
                }
            }
        }


    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class RolePermissionList : BaseDataObject
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


        Guid _permissionid;
        /// <summary>
        /// 功能项或则菜单项ID
        /// </summary>
        [Required(CMessage = "功能项或则菜单项",EMessage="Permission")]
        public Guid PermissionID
        {
            get
            {
                return _permissionid;
            }
            set
            {
                if (_permissionid != value)
                {
                    _permissionid = value;
                    base.OnPropertyChanged("PermissionID", value);
                }
            }
        }

        string _permissionname;
        /// <summary>
        /// 功能项或则菜单项ID
        /// </summary>
        public string PermissionName
        {
            get
            {
                return _permissionname;
            }
            set
            {
                if (_permissionname != value)
                {
                    _permissionname = value;
                    base.OnPropertyChanged("PermissionName", value);
                }
            }
        }


        Guid _roleid;
        /// <summary>
        /// 角色ID
        /// </summary>
        [Required(CMessage = "角色",EMessage="Role")]
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
        /// 角色名
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
            RolePermissionList newObj = obj as RolePermissionList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class UserPermissionList : BaseDataObject
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


        Guid _permissionid;
        /// <summary>
        /// 功能项或则菜单项ID
        /// </summary>
        [Required(CMessage = "功能项或则菜单项",EMessage="Permission")]

        public Guid PermissionID
        {
            get
            {
                return _permissionid;
            }
            set
            {
                if (_permissionid != value)
                {
                    _permissionid = value;
                    base.OnPropertyChanged("PermissionID", value);
                }
            }
        }


        string _permissionName;
        /// <summary>
        /// 功能项或则菜单项名称
        /// </summary>
        public string PermissionName
        {
            get
            {
                return _permissionName;
            }
            set
            {
                if (_permissionName != value)
                {
                    _permissionName = value;
                    base.OnPropertyChanged("PermissionName", value);
                }
            }
        }

        Guid _organizationid;
        /// <summary>
        /// 组织节点ID
        /// </summary>
        public Guid OrganizationID
        {
            get
            {
                return _organizationid;
            }
            set
            {
                if (_organizationid != value)
                {
                    _organizationid = value;
                    base.OnPropertyChanged("OrganizationID", value);
                }
            }
        }


        string _organizationname;
        /// <summary>
        /// 组织节点名
        /// </summary>
        [Required(CMessage = "组织节点名",EMessage="OrganizationName")]

        public string OrganizationName
        {
            get
            {
                return _organizationname;
            }
            set
            {
                if (_organizationname != value)
                {
                    _organizationname = value;
                    base.OnPropertyChanged("OrganizationName", value);
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
        /// 用户名
        /// </summary>
        [Required(CMessage = "用户名",EMessage="UserName")]

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
            UserPermissionList newObj = obj as UserPermissionList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class ModuleList : BaseDataObject
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


        string _assemblyname;
        /// <summary>
        /// 程序集
        /// </summary>
        public string AssemblyName
        {
            get
            {
                return _assemblyname;
            }
            set
            {
                if (_assemblyname != value)
                {
                    _assemblyname = value;
                    base.OnPropertyChanged("AssemblyName", value);
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
            ModuleList newObj = obj as ModuleList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class ModuleInfo : ModuleList
    {
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

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class UIItemList : BaseDataObject
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
        [Required(CMessage = "代码",EMessage="Code")]
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
        /// 英文名
        /// </summary>
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


        SiteType? _site;
        /// <summary>
        /// 菜单项组容器位置
        /// </summary>
        public SiteType? Site
        {
            get
            {
                return _site;
            }
            set
            {
                if (_site != value)
                {
                    _site = value;
                    base.OnPropertyChanged("Site", value);
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


        Guid? _functionid;
        /// <summary>
        /// 功能ID
        /// </summary>
        public Guid? FunctionID
        {
            get
            {
                return _functionid;
            }
            set
            {
                if (_functionid != value)
                {
                    _functionid = value;
                    base.OnPropertyChanged("FunctionID", value);
                }
            }
        }


        Guid? _parentid;
        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? ParentID
        {
            get
            {
                return _parentid;
            }
            set
            {
                if (_parentid != value)
                {
                    _parentid = value;
                    base.OnPropertyChanged("ParentID", value);
                }
            }
        }


        string _parentname;
        /// <summary>
        /// 父节点名
        /// </summary>
        public string ParentName
        {
            get
            {
                return _parentname;
            }
            set
            {
                if (_parentname != value)
                {
                    _parentname = value;
                    base.OnPropertyChanged("ParentName", value);
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


        UIConfigItemType _functiontype;
        /// <summary>
        /// 节点类型
        /// </summary>
        public UIConfigItemType FunctionType
        {
            get
            {
                return _functiontype;
            }
            set
            {
                if (_functiontype != value)
                {
                    _functiontype = value;
                    base.OnPropertyChanged("FunctionType", value);
                }
            }
        }


        string _hierarchycode;
        /// <summary>
        /// 级联代码
        /// </summary>
        public string HierarchyCode
        {
            get
            {
                return _hierarchycode;
            }
            set
            {
                if (_hierarchycode != value)
                {
                    _hierarchycode = value;
                    base.OnPropertyChanged("HierarchyCode", value);
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
            UIItemList newObj = obj as UIItemList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class UIItemInfo : UIItemList
    {
        Guid _createbyid;
        /// <summary>
        /// 创建人ID
        /// </summary>
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
                    base.OnPropertyChanged("CreateByID", value);
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


        string _commandname;
        /// <summary>
        /// 命令名
        /// </summary>
        public string CommandName
        {
            get
            {
                return _commandname;
            }
            set
            {
                if (_commandname != value)
                {
                    _commandname = value;
                    base.OnPropertyChanged("CommandName", value);
                }
            }
        }


    }
}
