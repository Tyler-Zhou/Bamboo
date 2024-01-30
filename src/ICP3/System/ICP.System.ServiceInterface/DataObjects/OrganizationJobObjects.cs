using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.ServiceInterface.DataObjects
{

    /// <summary>
    /// 组织结构岗位列表
    /// </summary>
    [Serializable]
    public partial class Organization2JobList : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _relationid;
        /// <summary>
        /// 关联ID（如果是职位就是job表的ID,如果是组织结构就是org...表的ID）
        /// </summary>
        public Guid RelationID
        {
            get
            {
                return _relationid;
            }
            set
            {
                if (_relationid != value)
                {
                    _relationid = value;
                    base.OnPropertyChanged("RelationID", value);
                }
            }
        }


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

        string _nodename;
        /// <summary>
        /// 子节点名
        /// </summary>
        public string Name
        {
            get
            {
                return _nodename;
            }
            set
            {
                if (_nodename != value)
                {
                    _nodename = value;
                    base.OnPropertyChanged("Name", value);
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

        Guid _parentid;
        /// <summary>
        /// 角色
        /// </summary>
        [GuidRequired(CMessage = "父亲节点", EMessage = "Parent")]
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
        /// 父节点名称
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


        /// <summary>
        /// 全称
        /// </summary>
        public string FullName
        {
            get
            {
                if (this.Type == OrganizationJobType.Job)
                {
                    return this.ParentName + "->" + this.Name;
                }
                else
                {
                    return this.Name;
                }
            }
        }

        /// <summary>
        /// 结构全称
        /// </summary>
        public string StructureFullName { get; set; }


        /// <summary>
        /// OrganizationJobType
        /// </summary>
        public OrganizationJobType Type { get; set; }

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
            Organization2JobList newObj = obj as Organization2JobList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }



    /// <summary>
    /// 岗位列表
    /// </summary>
    [Serializable]
    public partial class JobList : BaseDataObject
    {
        /// <summary>
        /// isNew
        /// </summary>
        public override bool IsNew { get { return ID == GlobalConstants.NewRowID; } }

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
        [StringLength(MaximumLength = 20, CMessage = "代码", EMessage = "Code")]
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
        /// 中文名称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "中文名", EMessage = "CName")]
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


        Guid? _parentid;
        /// <summary>
        /// 父子节点
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
            JobList newObj = obj as JobList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class JobInfo : JobList
    {
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
    /// 组织结构列表
    /// </summary>
    [Serializable]
    public partial class OrganizationList : BaseDataObject
    {
        /// <summary>
        /// 
        /// </summary>
        public override bool IsNew { get { return ID == GlobalConstants.NewRowID; } }

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
        [StringLength(MaximumLength = 20, CMessage = "代码", EMessage = "Code")]
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


        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault
        {
            get;
            set;
        }

        string _cshortname;
        /// <summary>
        /// 中文简称
        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "中文简称", EMessage = "CShortName")]
        [Required(CMessage = "中文简称", EMessage = "CShortName")]
        public string CShortName
        {
            get
            {
                return _cshortname;
            }
            set
            {
                if (_cshortname != value)
                {
                    _cshortname = value;
                    base.OnPropertyChanged("CShortName", value);
                }
            }
        }


        string _eshortname;
        /// <summary>
        /// 英文简称
        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "英文简称", EMessage = "EShortName")]
        [Required(CMessage = "英文简称", EMessage = "EShortName")]
        public string EShortName
        {
            get
            {
                return _eshortname;
            }
            set
            {
                if (_eshortname != value)
                {
                    _eshortname = value;
                    base.OnPropertyChanged("EShortName", value);
                }
            }
        }

        string _fullName;
        /// <summary>
        /// 全称
        /// </summary>
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    base.OnPropertyChanged("FullName", value);
                }
            }
        }


        OrganizationType _type;
        /// <summary>
        /// 类型
        /// </summary>
        public OrganizationType Type
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
            OrganizationList newObj = obj as OrganizationList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class OrganizationInfo : OrganizationList
    {
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
