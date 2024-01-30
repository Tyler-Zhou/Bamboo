using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using ICP.Sys.ServiceInterface.DataObjects;

namespace ICP.ReportCenter.ServiceInterface.DataObjects
{
    [Serializable]
    public partial class UserDetailInfo : BaseDataObject
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
        /// <summary>
        /// Address
        /// </summary>
        public string Address
        {
            get;
            set;
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
        string _Department;
        /// <summary>
        /// Department
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

        public string Area { get; set; }

        public string Company { get; set; }

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
        [RegularExpression(Pattern = "^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", CMessage = "邮件格式不正确。例:xx@hotmail.com", EMessage = "The Email Format is not correct. example:xx@hotmail.com", IsUseErrorTemplate = false)]
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


        string _Gender;
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender
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

        /// <summary>
        /// 公司电话
        /// </summary>
        public string CompanyTel
        {
            get;
            set;
        }

        /// <summary>
        /// 公司传真
        /// </summary>
        public string CompanyFax
        {
            get;
            set;
        }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress
        {
            get;
            set;
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

        string _roleName;
        /// <summary>
        /// 角色
        /// </summary>
        public string RoleName
        {
            get
            {
                return _roleName;
            }
            set
            {
                if (_roleName != value)
                {
                    _roleName = value;
                    base.OnPropertyChanged("RoleName", value);
                }
            }
        }

        Guid _parentID;
        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid ParentID
        {
            get
            {
                return _parentID;
            }
            set
            {
                if (_parentID != value)
                {
                    _parentID = value;
                    base.OnPropertyChanged("ParentID", value);
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


    }
    [Serializable]
    public partial class FullDepartmentObject : BaseDataObject
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID
        {
            get;
            set;
        }

        /// <summary>
        /// ParentID
        /// </summary>
        public Guid ParentID
        {
            get;
            set;
        }

        /// <summary>
        /// 部门结构名称
        /// </summary>
        public string FullName
        {
            get;
            set;
        }

        /// <summary>
        /// 1：根目录(PCH国际) 2:根目录下一级(物流部)3：根目录下一级的下一级(远东区)4:根目录下一级的下一级的下一级(某部门)
        /// </summary>
        public int Type
        {
            get;
            set;
        }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// 公司传真
        /// </summary>
        public string CompanyFax { get; set; }
        /// <summary>
        /// 公司电话
        /// </summary>
        public string CompanyTel { get; set; }

    }
    [Serializable]
    public partial class ContactObject : BaseDataObject
    {
        public List<FullDepartmentObject> FullDepartmentList
        {
            get;
            set;
        }

        public List<UserDetailInfo> UserDetailInfoList
        {
            get;
            set;
        }
    }
}
