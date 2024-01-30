using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using System.Xml.Serialization;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.ServiceInterface.DataObjects
{

    #region Configure
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class ConfigureBillList : BaseDataObject
    {
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
                if (_id == value) return;
                _id = value;
                base.OnPropertyChanged("ID", value);
            }
        }

        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

        Guid _configureid;
        /// <summary>
        /// 公司配置
        /// </summary>
        public Guid ConfigureID
        {
            get
            {
                return _configureid;
            }
            set
            {
                if (_configureid == value) return;
                _configureid = value;
                base.OnPropertyChanged("ConfigureID", value);
            }
        }



        string _titlecname;
        /// <summary>
        /// 标题(中文)
        /// </summary>
        public string TitleCName
        {
            get
            {
                return _titlecname;
            }
            set
            {
                if (_titlecname != value)
                {
                    _titlecname = value;
                    base.OnPropertyChanged("TitleCName", value);
                }
            }
        }

        string _titleename;
        /// <summary>
        ///标题(英文)
        /// </summary>
        public string TitleEName
        {
            get
            {
                return _titleename;
            }
            set
            {
                if (_titleename != value)
                {
                    _titleename = value;
                    base.OnPropertyChanged("TitleEName", value);
                }
            }

        }

        string _CTitle;
        /// <summary>
        /// 签字(中文)
        /// </summary>
        public string CTitle
        {
            get
            {
                return _CTitle;
            }
            set
            {
                if (_CTitle != value)
                {
                    _CTitle = value;
                    base.OnPropertyChanged("CTitle", value);
                }
            }
        }


        string _ETitle;
        /// <summary>
        /// 签字(英文)
        /// </summary>
        public string ETitle
        {
            get
            {
                return _ETitle;
            }
            set
            {
                if (_ETitle != value)
                {
                    _ETitle = value;
                    base.OnPropertyChanged("ETitle", value);
                }
            }
        }


        string _LogoFileName;
        /// <summary>
        /// 附件名
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "附件名", EMessage = "LogoFileName")]
        public string LogoFileName
        {
            get
            {
                return _LogoFileName;
            }
            set
            {
                if (_LogoFileName != value)
                {
                    _LogoFileName = value;
                    base.OnPropertyChanged("LogoFileName", value);
                }
            }
        }




        bool _isdefault;
        /// <summary>
        /// 状态
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
        DateTime? _updateDate;
        /// <summary>
        /// 行版本
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
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            CustomerConfirmList newObj = obj as CustomerConfirmList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class ConfigureCustomerList : CustomerList
    {
        Guid _CompanyId;
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID
        {
            get { return _CompanyId; }
            set
            {
                if (_CompanyId != value)
                {
                    _CompanyId = value;
                    base.OnPropertyChanged("CompanyID", value);
                }
            }
        }

        Guid _ConfigureID;
        /// <summary>
        /// 配置ID
        /// </summary>
        public Guid ConfigureID
        {
            get { return _ConfigureID; }
            set
            {
                if (_ConfigureID != value)
                {
                    _ConfigureID = value;
                    base.OnPropertyChanged("ConfigureID", value);
                }
            }
        }
    }
    #endregion

    #region Country

    [Serializable]
    public partial class CountryList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        string _code;
        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(MaximumLength = 2, CMessage = "代码", EMessage = "Code")]

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
        /// 英文名

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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            CountryList newObj = obj as CountryList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class CountryInfo : CountryList
    {
        Guid _createbyid;
        /// <summary>
        /// 创建人

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


    }


    [Serializable]
    public partial class CountryProvinceList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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
        /// 中文名

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
        /// 英文名

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


        CountryProvinceType _type;
        /// <summary>
        /// 类型
        /// </summary>
        public CountryProvinceType Type
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


        Guid? _parentid;
        /// <summary>
        /// 国家/省ID
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
        /// 国家/省

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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            CountryProvinceList newObj = obj as CountryProvinceList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }

    [Serializable]
    public partial class CountryProvinceInfo : CountryProvinceList
    {
        Guid _createbyid;
        /// <summary>
        /// 创建人

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


    }

    #endregion

    #region Location

    [Serializable]
    public partial class LocationList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        string _code;
        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(MaximumLength = 10, CMessage = "代码", EMessage = "Code")]

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
        /// 英文名

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


        string _countryprovincename;
        /// <summary>
        /// 地区
        /// </summary>

        public string CountryProvinceName
        {
            get
            {
                return _countryprovincename;
            }
            set
            {
                if (_countryprovincename != value)
                {
                    _countryprovincename = value;
                    base.OnPropertyChanged("CountryProvinceName", value);
                }
            }
        }


        bool _isocean;
        /// <summary>
        /// 海运
        /// </summary>
        [Required(CMessage = "是否属于海运", EMessage = "IsOcean")]
        public bool IsOcean
        {
            get
            {
                return _isocean;
            }
            set
            {
                if (_isocean != value)
                {
                    _isocean = value;
                    base.OnPropertyChanged("IsOcean", value);
                }
            }
        }


        bool _isair;
        /// <summary>
        /// 空运
        /// </summary>
        [Required(CMessage = "是否属于空运", EMessage = "IsAir")]
        public bool IsAir
        {
            get
            {
                return _isair;
            }
            set
            {
                if (_isair != value)
                {
                    _isair = value;
                    base.OnPropertyChanged("IsAir", value);
                }
            }
        }


        bool _isother;
        /// <summary>
        /// 其他
        /// </summary>
        [Required(CMessage = "是否属于其他", EMessage = "IsOther")]
        public bool IsOther
        {
            get
            {
                return _isother;
            }
            set
            {
                if (_isother != value)
                {
                    _isother = value;
                    base.OnPropertyChanged("IsOther", value);
                }
            }
        }


        bool _isvalid;
        /// <summary>
        /// 有效
        /// </summary>
        [Required(CMessage = "是否有效", EMessage = "IsValid")]
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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            LocationList newObj = obj as LocationList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }

    /// <summary>
    /// 根据名称获取港口信息接口用到的对象
    /// </summary>
    [Serializable]
    public class PortNames
    {
        public string OriginName { get; set; }
        public string EName { get; set; }
        public string CName { get; set; }
        public Guid? ID { get; set; }
    }

    [Serializable]
    public partial class LocationInfo : LocationList
    {
        Guid _createbyid;
        /// <summary>
        /// 创建人

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


        Guid _countryid;
        /// <summary>
        /// 国家ID
        /// </summary>
        //[GuidRequired(ErrorMessage = "国家必须填写")]
        public Guid CountryID
        {
            get
            {
                return _countryid;
            }
            set
            {
                if (_countryid != value)
                {
                    _countryid = value;
                    base.OnPropertyChanged("CountryID", value);
                }
            }
        }


        string _countryname;
        /// <summary>
        /// 国家名

        /// </summary>
        public string CountryName
        {
            get
            {
                return _countryname;
            }
            set
            {
                if (_countryname != value)
                {
                    _countryname = value;
                    base.OnPropertyChanged("CountryName", value);
                }
            }
        }


        Guid? _provinceid;
        /// <summary>
        /// 省/州ID
        /// </summary>
        public Guid? ProvinceID
        {
            get
            {
                return _provinceid;
            }
            set
            {
                if (_provinceid != value)
                {
                    _provinceid = value;
                    base.OnPropertyChanged("ProvinceID", value);
                }
            }
        }


        string _provincename;
        /// <summary>
        /// 省/州名
        /// </summary>
        public string ProvinceName
        {
            get
            {
                return _provincename;
            }
            set
            {
                if (_provincename != value)
                {
                    _provincename = value;
                    base.OnPropertyChanged("ProvinceName", value);
                }
            }
        }


    }

    /// <summary>
    /// 街道和邮编信息（AMS）
    /// </summary>
    [Serializable]
    public class PostalCodeInfo
    {
        public Guid ID { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        public Guid LocationID { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string PostalCode { get; set; }
    }

    #endregion

    #region Container

    [Serializable]
    public partial class ContainerList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        string _code;
        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(MaximumLength = 10, CMessage = "代码", EMessage = "Code")]

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


        string _isocode;
        /// <summary>
        /// 国际代码
        /// </summary>
        [StringLength(MaximumLength = 10, CMessage = "国际代码", EMessage = "ISOCode")]
        [Required(CMessage = "国际代码", EMessage = "ISOCode")]
        public string ISOCode
        {
            get
            {
                return _isocode;
            }
            set
            {
                if (_isocode != value)
                {
                    _isocode = value;
                    base.OnPropertyChanged("ISOCode", value);
                }
            }
        }


        decimal _teu;
        /// <summary>
        /// 箱量
        /// </summary>
        [Required(CMessage = "TEU", EMessage = "TEU")]

        public decimal TEU
        {
            get
            {
                return _teu;
            }
            set
            {
                if (_teu != value)
                {
                    _teu = value;
                    base.OnPropertyChanged("TEU", value);
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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            ContainerList newObj = obj as ContainerList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class ContainerInfo : ContainerList
    {
        Guid _createbyid;
        /// <summary>
        /// 创建人

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


    }
    #endregion

    #region TransportClause

    [Serializable]
    public partial class TransportClauseList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        string _originalcode;
        /// <summary>
        /// 起始地

        /// </summary>
        public string OriginalCode
        {
            get
            {
                return _originalcode;
            }
            set
            {
                if (_originalcode != value)
                {
                    _originalcode = value;
                    base.OnPropertyChanged("OriginalCode", value);
                }
            }
        }


        string _destinationcode;
        /// <summary>
        /// 目的地

        /// </summary>
        public string DestinationCode
        {
            get
            {
                return _destinationcode;
            }
            set
            {
                if (_destinationcode != value)
                {
                    _destinationcode = value;
                    base.OnPropertyChanged("DestinationCode", value);
                }
            }
        }


        string _code;
        /// <summary>
        /// 代码(起始地代码-目的地代码)
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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            TransportClauseList newObj = obj as TransportClauseList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class TransportClauseInfo : TransportClauseList
    {
        Guid _originalcodeid;
        /// <summary>
        /// 起始地代码ID
        /// </summary>
        [GuidRequired(CMessage = "起始港", EMessage = "OriginalCode")]

        public Guid OriginalCodeID
        {
            get
            {
                return _originalcodeid;
            }
            set
            {
                if (_originalcodeid != value)
                {
                    _originalcodeid = value;
                    base.OnPropertyChanged("OriginalCodeID", value);
                }
            }
        }


        Guid _destinationcodeid;
        /// <summary>
        /// 目的地代码ID
        /// </summary>
        [GuidRequired(CMessage = "到达港", EMessage = "DestinationCode")]

        public Guid DestinationCodeID
        {
            get
            {
                return _destinationcodeid;
            }
            set
            {
                if (_destinationcodeid != value)
                {
                    _destinationcodeid = value;
                    base.OnPropertyChanged("DestinationCodeID", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 创建人

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


    }

    #endregion

    #region Commodity
    [Serializable]
    public partial class CommodityList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        string _cname;
        /// <summary>
        /// 中文名

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
        /// 英文名

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


        Guid? _parentid;
        /// <summary>
        /// 父ID
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
        /// 父名
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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            CommodityList newObj = obj as CommodityList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class CommodityInfo : CommodityList
    {
        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaximumLength = 500, CMessage = "备注", EMessage = "Remark")]

        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                if (_remark != value)
                {
                    _remark = value;
                    base.OnPropertyChanged("Remark", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 创建人

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


    }
    #endregion

    #region ShippingLine

    [Serializable]
    public partial class ShippingLineList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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

        Guid? _ParentID;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid? ParentID
        {
            get
            {
                return _ParentID;
            }
            set
            {
                if (_ParentID != value)
                {
                    _ParentID = value;
                    base.OnPropertyChanged("ParentID", value);
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
        /// 中文名

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
        /// 英文名

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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            ShippingLineList newObj = obj as ShippingLineList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class ShippingLineInfo : ShippingLineList
    {
        Guid _createbyid;
        /// <summary>
        /// 创建人

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


    }

    #endregion

    #region DataDictionary
    [Serializable]
    public partial class DataDictionaryList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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
        /// 中文名

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
        /// 英文名

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


        DataDictionaryType _type;
        /// <summary>
        /// 类型
        /// </summary>
        [Required(CMessage = "类型", EMessage = "Type")]

        public DataDictionaryType Type
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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            DataDictionaryList newObj = obj as DataDictionaryList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class DataDictionaryInfo : DataDictionaryList
    {
        Guid _createbyid;
        /// <summary>
        /// 创建人

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


    }
    #endregion

    #region Flight

    [Serializable]
    public partial class FlightList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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

        string _airlinename;
        /// <summary>
        /// 航空公司
        /// </summary>
        public string AirlineName
        {
            get
            {
                return _airlinename;
            }
            set
            {
                if (_airlinename != value)
                {
                    _airlinename = value;
                    base.OnPropertyChanged("AirlineName", value);
                }
            }
        }

        string _no;
        /// <summary>
        /// 航班号
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "航班号", EMessage = "No")]
        [Required(CMessage = "航班号", EMessage = "No")]
        public string No
        {
            get
            {
                return _no;
            }
            set
            {
                if (_no != value)
                {
                    _no = value;
                    base.OnPropertyChanged("No", value);
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

        DateTime? _updateDate;
        /// <summary>
        /// 行版本
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

        public override bool Equals(object obj)
        {
            FlightList newObj = obj as FlightList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }

    [Serializable]
    public partial class FlightInfo : FlightList
    {
        Guid _airlineid;
        /// <summary>
        /// 航空公司ID
        /// </summary>
        [GuidRequired(CMessage = "航空公司", EMessage = "AirLine")]
        public Guid AirlineID
        {
            get
            {
                return _airlineid;
            }
            set
            {
                if (_airlineid != value)
                {
                    _airlineid = value;
                    base.OnPropertyChanged("AirlineID", value);
                }
            }
        }

        Guid _createbyid;
        /// <summary>
        /// 创建人
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
    }

    #endregion

    #region ChargingCode
    [Serializable]
    public partial class ChargingCodeList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        string _groupname;
        /// <summary>
        /// 组名
        /// </summary>
        public string GroupName
        {
            get
            {
                return _groupname;
            }
            set
            {
                if (_groupname != value)
                {
                    _groupname = value;
                    base.OnPropertyChanged("GroupName", value);
                }
            }
        }


        string _code;
        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(MaximumLength = 10, CMessage = "代码", EMessage = "Code")]

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
        /// 英文名

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


        bool _iscommission;
        /// <summary>
        /// 佣金
        /// </summary>
        [Required(CMessage = "是否佣金", EMessage = "IsCommission")]

        public bool IsCommission
        {
            get
            {
                return _iscommission;
            }
            set
            {
                if (_iscommission != value)
                {
                    _iscommission = value;
                    base.OnPropertyChanged("IsCommission", value);
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


        Guid _groupid;
        /// <summary>
        /// 分组ID
        /// </summary>
        [GuidRequired(CMessage = "组", EMessage = "Group")]
        public Guid GroupID
        {
            get
            {
                return _groupid;
            }
            set
            {
                if (_groupid != value)
                {
                    _groupid = value;
                    base.OnPropertyChanged("GroupID", value);
                }
            }
        }


        string _grouphierarchycode;
        /// <summary>
        /// 级联代码
        /// </summary>
        public string GroupHierarchyCode
        {
            get
            {
                return _grouphierarchycode;
            }
            set
            {
                if (_grouphierarchycode != value)
                {
                    _grouphierarchycode = value;
                    base.OnPropertyChanged("GroupHierarchyCode", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 创建人

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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            ChargingCodeList newObj = obj as ChargingCodeList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class ChargingCodeInfo : ChargingCodeList
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class ChargingGroupList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        string _code;
        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(MaximumLength = 10, CMessage = "代码", EMessage = "Code")]

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
        /// 英文名

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


        Guid _parentid;
        /// <summary>
        /// 父ID
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
        /// 父名
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


        ChargeGroupNodeType _nodetype;
        /// <summary>
        /// 节点类型(0:组,1:项)
        /// </summary>
        public ChargeGroupNodeType NodeType
        {
            get
            {
                return _nodetype;
            }
            set
            {
                if (_nodetype != value)
                {
                    _nodetype = value;
                    base.OnPropertyChanged("NodeType", value);
                }
            }
        }


        ChargeCodeCategory _category;
        /// <summary>
        /// 类型
        /// </summary>
        [Required(CMessage = "创建人", EMessage = "CateGory")]

        public ChargeCodeCategory Category
        {
            get
            {
                return _category;
            }
            set
            {
                if (_category != value)
                {
                    _category = value;
                    base.OnPropertyChanged("Category", value);
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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            ChargingGroupList newObj = obj as ChargingGroupList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class ChargingGroupInfo : ChargingGroupList
    {
        Guid _createbyid;
        /// <summary>
        /// 创建人

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


    }

    #endregion

    #region Currency
    /// <summary>
    /// 币种
    /// </summary>
    [Serializable]
    public partial class CurrencyList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        string _code;
        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(MaximumLength = 10, CMessage = "代码", EMessage = "Code")]
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


        string _countryname;
        /// <summary>
        /// 国家名
        /// </summary>
        public string CountryName
        {
            get
            {
                return _countryname;
            }
            set
            {
                if (_countryname != value)
                {
                    _countryname = value;
                    base.OnPropertyChanged("CountryName", value);
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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本
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
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            CurrencyList newObj = obj as CurrencyList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class CurrencyInfo : CurrencyList
    {
        Guid _createbyid;
        /// <summary>
        /// 创建人

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


        Guid _countryid;
        /// <summary>
        /// 国家ID
        /// </summary>
        [GuidRequired(CMessage = "国家", EMessage = "Country")]

        public Guid CountryID
        {
            get
            {
                return _countryid;
            }
            set
            {
                if (_countryid != value)
                {
                    _countryid = value;
                    base.OnPropertyChanged("CountryID", value);
                }
            }
        }


    }

    #endregion

    #region ConfigureKey
    [Serializable]
    public partial class ConfigureKeyList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        string _code;
        /// <summary>
        /// 代码
        /// </summary>
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


        string _name;
        /// <summary>
        /// 中文名

        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "中文名", EMessage = "ChineseName")]
        [Required(CMessage = "中文名", EMessage = "ChineseName")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    base.OnPropertyChanged("Name", value);
                }
            }
        }

        public override bool Equals(object obj)
        {
            ConfigureKeyList newObj = obj as ConfigureKeyList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class ConfigureKeyValueList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        Guid _configureid;
        /// <summary>
        /// 配置ID
        /// </summary>
        public Guid ConfigureID
        {
            get
            {
                return _configureid;
            }
            set
            {
                if (_configureid != value)
                {
                    _configureid = value;
                    base.OnPropertyChanged("ConfigureID", value);
                }
            }
        }


        string _configurekeyname;
        /// <summary>
        /// 键

        /// </summary>
        public string ConfigureKeyName
        {
            get
            {
                return _configurekeyname;
            }
            set
            {
                if (_configurekeyname != value)
                {
                    _configurekeyname = value;
                    base.OnPropertyChanged("ConfigureKeyName", value);
                }
            }
        }


        Guid _configurekeyid;
        /// <summary>
        /// 配置键ID
        /// </summary>
        [GuidRequired(CMessage = "配置键", EMessage = "ConfigureKey")]

        public Guid ConfigureKeyID
        {
            get
            {
                return _configurekeyid;
            }
            set
            {
                if (_configurekeyid != value)
                {
                    _configurekeyid = value;
                    base.OnPropertyChanged("ConfigureKeyID", value);
                }
            }
        }


        string _value;
        /// <summary>
        /// 值

        /// </summary>
        [Required(CMessage = "值", EMessage = "Value")]

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    base.OnPropertyChanged("Value", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 创建人

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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            ConfigureKeyValueList newObj = obj as ConfigureKeyValueList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class ConfigureKeyValueInfo : ConfigureKeyValueList
    {
    }


    #endregion

    #region  Configure
    /// <summary>
    /// 公司配置(列表展示)
    /// </summary>
    [Serializable]
    public partial class ConfigureList : BaseDataObject
    {
        /// <summary>
        /// 
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        string _companyname;
        /// <summary>
        /// 公司
        /// </summary>
        [Required(CMessage = "公司名称", EMessage = "CompanyName")]

        public string CompanyName
        {
            get
            {
                return _companyname;
            }
            set
            {
                if (_companyname != value)
                {
                    _companyname = value;
                    base.OnPropertyChanged("CompanyName", value);
                }
            }
        }


        string _customername;
        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerName
        {
            get
            {
                return _customername;
            }
            set
            {
                if (_customername != value)
                {
                    _customername = value;
                    base.OnPropertyChanged("CustomerName", value);
                }
            }
        }


        string _solutionname;
        /// <summary>
        /// 解决方案
        /// </summary>
        public string SolutionName
        {
            get
            {
                return _solutionname;
            }
            set
            {
                if (_solutionname != value)
                {
                    _solutionname = value;
                    base.OnPropertyChanged("SolutionName", value);
                }
            }
        }


        string _issueplacename;
        /// <summary>
        /// 签发地
        /// </summary>
        public string IssuePlaceName
        {
            get
            {
                return _issueplacename;
            }
            set
            {
                if (_issueplacename != value)
                {
                    _issueplacename = value;
                    base.OnPropertyChanged("IssuePlaceName", value);
                }
            }
        }

        #region 商务关账日
        DateTime? businessclosingdate;
        /// <summary>
        /// 商务关账日
        /// </summary>
        public DateTime? BusinessClosingDate
        {
            get
            {
                return businessclosingdate;
            }
            set
            {
                if (businessclosingdate != value)
                {
                    businessclosingdate = value;
                    base.OnPropertyChanged("BusinessClosingDate", value);
                }
            }
        }
        #endregion

        #region 计费关帐日
        DateTime? _chargingClosingdate;
        /// <summary>
        /// 计费关帐日
        /// </summary>
        public DateTime? ChargingClosingdate
        {
            get
            {
                return _chargingClosingdate;
            }
            set
            {
                if (_chargingClosingdate != value)
                {
                    _chargingClosingdate = value;
                    base.OnPropertyChanged("ChargingClosingdate", value);
                }
            }
        } 
        #endregion

        #region 财务关帐日
        DateTime? _accountingClosingdate;
        /// <summary>
        /// 财务关帐日
        /// </summary>
        public DateTime? AccountingClosingdate
        {
            get
            {
                return _accountingClosingdate;
            }
            set
            {
                if (_accountingClosingdate != value)
                {
                    _accountingClosingdate = value;
                    base.OnPropertyChanged("AccountingClosingdate", value);
                }
            }
        } 
        #endregion

        string _shortcode;
        /// <summary>
        /// 公司代码
        /// </summary>
        [StringLength(MaximumLength = 10, CMessage = "简码", EMessage = "ShortCode")]
        [Required(CMessage = "简码", EMessage = "ShortCode")]

        public string ShortCode
        {
            get
            {
                return _shortcode;
            }
            set
            {
                if (_shortcode != value)
                {
                    _shortcode = value;
                    base.OnPropertyChanged("ShortCode", value);
                }
            }
        }


        Guid _standardcurrencyid;
        /// <summary>
        /// 本位币ID
        /// </summary>
        [GuidRequired(CMessage = "本位币", EMessage = "StandardCurrency")]

        public Guid StandardCurrencyID
        {
            get
            {
                return _standardcurrencyid;
            }
            set
            {
                if (_standardcurrencyid != value)
                {
                    _standardcurrencyid = value;
                    base.OnPropertyChanged("StandardCurrencyID", value);
                }
            }
        }


        Guid _defaultcurrencyid;
        /// <summary>
        /// 默认币种ID
        /// </summary>
        [GuidRequired(CMessage = "默认币种", EMessage = "DefaultCurrentcy")]
        public Guid DefaultCurrencyID
        {
            get
            {
                return _defaultcurrencyid;
            }
            set
            {
                if (_defaultcurrencyid != value)
                {
                    _defaultcurrencyid = value;
                    base.OnPropertyChanged("DefaultCurrencyID", value);
                }
            }
        }


        Guid _solutionid;
        /// <summary>
        /// 解决方案ID
        /// </summary>
        [GuidRequired(CMessage = "解决方案", EMessage = "Solution")]
        public Guid SolutionID
        {
            get
            {
                return _solutionid;
            }
            set
            {
                if (_solutionid != value)
                {
                    _solutionid = value;
                    base.OnPropertyChanged("SolutionID", value);
                }
            }
        }

        Guid _customerid;
        /// <summary>
        /// 客户ID
        /// </summary>
        [GuidRequired(CMessage = "客户", EMessage = "Customer")]
        public Guid CustomerID
        {
            get
            {
                return _customerid;
            }
            set
            {
                if (_customerid != value)
                {
                    _customerid = value;
                    base.OnPropertyChanged("CustomerID", value);
                }
            }
        }


        Guid? _issueplaceid;
        /// <summary>
        /// 签发地ID
        /// </summary>
        public Guid? IssuePlaceID
        {
            get
            {
                return _issueplaceid;
            }
            set
            {
                if (_issueplaceid != value)
                {
                    _issueplaceid = value;
                    base.OnPropertyChanged("IssuePlaceID", value);
                }
            }
        }


        Guid _companyid;
        /// <summary>
        /// 公司ID
        /// </summary>
        [GuidRequired(CMessage = "公司", EMessage = "Company")]

        public Guid CompanyID
        {
            get
            {
                return _companyid;
            }
            set
            {
                if (_companyid != value)
                {
                    _companyid = value;
                    base.OnPropertyChanged("CompanyID", value);
                }
            }
        }


        string _standardcurrency;
        /// <summary>
        /// 本位币
        /// </summary>
        public string StandardCurrency
        {
            get
            {
                return _standardcurrency;
            }
            set
            {
                if (_standardcurrency != value)
                {
                    _standardcurrency = value;
                    base.OnPropertyChanged("StandardCurrency", value);
                }
            }
        }


        string _defaultcurrency;
        /// <summary>
        /// 默认币
        /// </summary>
        public string DefaultCurrency
        {
            get
            {
                return _defaultcurrency;
            }
            set
            {
                if (_defaultcurrency != value)
                {
                    _defaultcurrency = value;
                    base.OnPropertyChanged("DefaultCurrency", value);
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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本
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
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            ConfigureList newObj = obj as ConfigureList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 0;
        }
    }


    /// <summary>
    /// 公司配置(编辑)
    /// </summary>
    [Serializable]
    public partial class ConfigureInfo : ConfigureList
    {
        #region 创建人
        Guid _createbyid;
        /// <summary>
        /// 创建人
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
        #endregion

        #region 默认代理描述
        string _defaultAgentDescription;
        /// <summary>
        /// 默认代理描述
        /// </summary>
        public string DefaultAgentDescription
        {
            get
            {
                return _defaultAgentDescription;
            }
            set
            {
                if (_defaultAgentDescription != value)
                {
                    _defaultAgentDescription = value;
                    base.OnPropertyChanged("DefaultAgentDescription", value);
                }
            }
        } 
        #endregion

        #region 税控版本号
        string _taxControlVersion;
        /// <summary>
        /// 税控版本号
        /// </summary>
        public string TaxControlVersion
        {
            get
            {
                return _taxControlVersion;
            }
            set
            {
                if (_taxControlVersion != value)
                {
                    _taxControlVersion = value;
                    base.OnPropertyChanged("TaxControlVersion", value);
                }
            }
        } 
        #endregion

        #region 抬头NAME
        /// <summary>
        /// 抬头NAME
        /// </summary>
        public string BLTitleName { get; set; } 
        #endregion

        #region 抬头ID
        Guid _bLTitleID;
        /// <summary>
        /// 抬头ID
        /// </summary>
        [GuidRequired(CMessage = "提单抬头", EMessage = "BLTitle")]
        public Guid BLTitleID
        {
            get
            {
                return _bLTitleID;
            }
            set
            {
                if (_bLTitleID != value)
                {
                    _bLTitleID = value;
                    base.OnPropertyChanged("BLTitleID", value);
                }
            }
        } 
        #endregion

        #region 是否开增值税发票
        bool _IsVATinvoice;
        /// <summary>
        /// 是否开增值税发票
        /// </summary>
        public bool IsVATinvoice
        {
            get
            {
                return _IsVATinvoice;
            }
            set
            {
                if (_IsVATinvoice != value)
                {
                    _IsVATinvoice = value;
                    base.OnPropertyChanged("IsVATinvoice", value);
                }
            }
        } 
        #endregion

        #region 增值税费用名称
        string _vATFEEname;
        /// <summary>
        /// 增值税费用名称
        /// </summary>
        public string VATFeeName
        {
            get
            {
                return _vATFEEname;
            }
            set
            {
                if (_vATFEEname != value)
                {
                    _vATFEEname = value;
                    base.OnPropertyChanged("VATFeeName", value);
                }
            }
        } 
        #endregion

        #region 增值税费用名称
        Guid? _vATFeeID;
        /// <summary>
        /// 增值税费用名称
        /// </summary>
        public Guid? VATFeeID
        {
            get
            {
                return _vATFeeID;
            }
            set
            {
                if (_vATFeeID != value)
                {
                    _vATFeeID = value;
                    base.OnPropertyChanged("VATFeeID", value);
                }
            }
        } 
        #endregion

        #region 税率
        Decimal? _VATrate;
        /// <summary>
        /// 税率
        /// </summary>
        public Decimal? VATrate
        {
            get
            {
                return _VATrate;
            }
            set
            {
                if (_VATrate != value)
                {
                    _VATrate = value;
                    base.OnPropertyChanged("VATrate", value);
                }
            }
        } 
        #endregion

        #region 增值税Code
        string _VATFeeCode;
        /// <summary>
        /// 增值税费用code
        /// </summary>
        public string VATFeeCode
        {
            get { return _VATFeeCode; }
            set
            {
                if (_VATFeeCode != value)
                {
                    _VATFeeCode = value;
                    base.OnPropertyChanged("VATFeeCode", value);
                }
            }
        }
        #endregion

        #region 放单人邮箱
        string _releaseremail;
        /// <summary>
        /// 放单人邮箱
        /// </summary>
        public string ReleaserEmail
        {
            get { return _releaseremail; }
            set
            {
                if (_releaseremail != value)
                {
                    _releaseremail = value;
                    base.OnPropertyChanged("ReleaserEmail", value);
                }
            }
        }
        #endregion

        #region 招行一网通用户
        string cmbUserID;
        /// <summary>
        /// 招行一网通用户
        /// </summary>
        public string CMBNetComUserID
        {
            get
            {
                return cmbUserID;
            }
            set
            {
                if (cmbUserID != value)
                {
                    cmbUserID = value;
                    base.OnPropertyChanged("CMBNetComUserID", value);
                }
            }
        } 
        #endregion

        
    }

    #endregion

    #region  Solution
    [Serializable]
    public partial class SolutionChargingCodeList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

        bool _isselected;
        /// <summary>
        /// 供界面处理使用

        /// </summary>
        public bool IsSelected
        {
            get
            {
                return _isselected;
            }
            set
            {
                if (_isselected != value)
                {
                    _isselected = value;
                    base.OnPropertyChanged("IsSelected", value);
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


        Guid _solutionid;
        /// <summary>
        /// 解决方案ID
        /// </summary>
        public Guid SolutionID
        {
            get
            {
                return _solutionid;
            }
            set
            {
                if (_solutionid != value)
                {
                    _solutionid = value;
                    base.OnPropertyChanged("SolutionID", value);
                }
            }
        }

        public string Code
        {
            get;
            set;
        }

        public string EName
        {
            get;
            set;
        }

        Guid _parentid;
        /// <summary>
        /// 组ID
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
        /// 组名
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


        string _grouphierarchycode;
        /// <summary>
        /// 级联代码
        /// </summary>
        public string GroupHierarchyCode
        {
            get
            {
                return _grouphierarchycode;
            }
            set
            {
                if (_grouphierarchycode != value)
                {
                    _grouphierarchycode = value;
                    base.OnPropertyChanged("GroupHierarchyCode", value);
                }
            }
        }


        Guid _chargingcodeid;
        /// <summary>
        /// 费用信息ID
        /// </summary>
        [GuidRequired(CMessage = "费用代码", EMessage = "ChargingCode")]

        public Guid ChargingCodeID
        {
            get
            {
                return _chargingcodeid;
            }
            set
            {
                if (_chargingcodeid != value)
                {
                    _chargingcodeid = value;
                    base.OnPropertyChanged("ChargingCodeID", value);
                }
            }
        }


        string _chargingcodename;
        /// <summary>
        /// 费用项目
        /// </summary>
        public string ChargingCodeName
        {
            get
            {
                return _chargingcodename;
            }
            set
            {
                if (_chargingcodename != value)
                {
                    _chargingcodename = value;
                    base.OnPropertyChanged("ChargingCodeName", value);
                }
            }
        }


        bool _isagent;
        /// <summary>
        /// 代收代付
        /// </summary>
        public bool IsAgent
        {
            get
            {
                return _isagent;
            }
            set
            {
                if (_isagent != value)
                {
                    _isagent = value;
                    base.OnPropertyChanged("IsAgent", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 创建人

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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            SolutionChargingCodeList newObj = obj as SolutionChargingCodeList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class SolutionChargingCodeInfo : SolutionChargingCodeList
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class SolutionCurrencyList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        Guid _solutionid;
        /// <summary>
        /// 解决方案ID
        /// </summary>
        public Guid SolutionID
        {
            get
            {
                return _solutionid;
            }
            set
            {
                if (_solutionid != value)
                {
                    _solutionid = value;
                    base.OnPropertyChanged("SolutionID", value);
                }
            }
        }


        Guid _currencyid;
        /// <summary>
        /// 币种ID
        /// </summary>
        [GuidRequired(CMessage = "币种", EMessage = "Currency")]
        public Guid CurrencyID
        {
            get
            {
                return _currencyid;
            }
            set
            {
                if (_currencyid != value)
                {
                    _currencyid = value;
                    base.OnPropertyChanged("CurrencyID", value);
                }
            }
        }


        string _currencyname;
        /// <summary>
        /// 币种名
        /// </summary>
        public string CurrencyName
        {
            get
            {
                return _currencyname;
            }
            set
            {
                if (_currencyname != value)
                {
                    _currencyname = value;
                    base.OnPropertyChanged("CurrencyName", value);
                }
            }
        }


        Guid _standardcurrencyid;
        /// <summary>
        /// 是否本位币种
        /// </summary>
        public Guid StandardCurrencyID
        {
            get
            {
                return _standardcurrencyid;
            }
            set
            {
                if (_standardcurrencyid != value)
                {
                    _standardcurrencyid = value;
                    base.OnPropertyChanged("StandardCurrencyID", value);
                }
            }
        }

        Guid _defaultcurrencyid;
        /// <summary>
        /// 是否默认币种
        /// </summary>
        public Guid DefaultCurrencyID
        {
            get
            {
                return _defaultcurrencyid;
            }
            set
            {
                if (_defaultcurrencyid != value)
                {
                    _defaultcurrencyid = value;
                    base.OnPropertyChanged("DefaultCurrencyID", value);
                }
            }
        }

        Guid _createbyid;
        /// <summary>
        /// 创建人
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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本
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
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            SolutionCurrencyList newObj = obj as SolutionCurrencyList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class SolutionCurrencyInfo : SolutionCurrencyList
    {
    }


    [Serializable]
    public partial class SolutionExchangeRateList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        Guid _solutionid;
        /// <summary>
        /// 解决方案ID
        /// </summary>
        public Guid SolutionID
        {
            get
            {
                return _solutionid;
            }
            set
            {
                if (_solutionid != value)
                {
                    _solutionid = value;
                    base.OnPropertyChanged("SolutionID", value);
                }
            }
        }

        ExchangeType exchangeType;
        /// <summary>
        /// 汇率类型
        /// </summary>
        public ExchangeType ExchangeType
        {
            get
            {
                return exchangeType;
            }
            set
            {
                exchangeType = value;
            }
        }



        string _sourcecurrency;
        /// <summary>
        /// 源币种

        /// </summary>
        public string SourceCurrency
        {
            get
            {
                return _sourcecurrency;
            }
            set
            {
                if (_sourcecurrency != value)
                {
                    _sourcecurrency = value;
                    base.OnPropertyChanged("SourceCurrency", value);
                }
            }
        }


        Guid _sourcecurrencyid;
        /// <summary>
        /// 源币种ID
        /// </summary>
        [GuidRequired(CMessage = "源币种", EMessage = "SourceCurrency")]

        public Guid SourceCurrencyID
        {
            get
            {
                return _sourcecurrencyid;
            }
            set
            {
                if (_sourcecurrencyid != value)
                {
                    _sourcecurrencyid = value;
                    base.OnPropertyChanged("SourceCurrencyID", value);
                }
            }
        }


        string _targetcurrency;
        /// <summary>
        /// 目标币种
        /// </summary>
        public string TargetCurrency
        {
            get
            {
                return _targetcurrency;
            }
            set
            {
                if (_targetcurrency != value)
                {
                    _targetcurrency = value;
                    base.OnPropertyChanged("TargetCurrency", value);
                }
            }
        }


        Guid _targetcurrencyid;
        /// <summary>
        /// 目标币种ID
        /// </summary>
        [GuidRequired(CMessage = "目标", EMessage = "TargetCurrency")]

        public Guid TargetCurrencyID
        {
            get
            {
                return _targetcurrencyid;
            }
            set
            {
                if (_targetcurrencyid != value)
                {
                    _targetcurrencyid = value;
                    base.OnPropertyChanged("TargetCurrencyID", value);
                }
            }
        }


        DateTime _fromdate;
        /// <summary>
        /// 开始日期

        /// </summary>
        [Required(CMessage = "开始时间", EMessage = "FormDate")]

        public DateTime FromDate
        {
            get
            {
                return _fromdate;
            }
            set
            {
                if (_fromdate != value)
                {
                    _fromdate = value;
                    base.OnPropertyChanged("FromDate", value);
                }
            }
        }


        DateTime _todate;
        /// <summary>
        /// 结束日期
        /// </summary>
        [Required(CMessage = "结束时间", EMessage = "ToDate")]

        public DateTime ToDate
        {
            get
            {
                return _todate;
            }
            set
            {
                if (_todate != value)
                {
                    _todate = value;
                    base.OnPropertyChanged("ToDate", value);
                }
            }
        }


        decimal _rate;
        /// <summary>
        /// 汇率
        /// </summary>
        [Required(CMessage = "汇率", EMessage = "Rate")]

        public decimal Rate
        {
            get
            {
                return _rate;
            }
            set
            {
                if (_rate != value)
                {
                    _rate = value;
                    base.OnPropertyChanged("Rate", value);
                }
            }
        }


        bool _isvalid;
        /// <summary>
        /// 有效
        /// </summary>
        [Required(CMessage = "是否有效", EMessage = "IsValid")]

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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            SolutionExchangeRateList newObj = obj as SolutionExchangeRateList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }

        Guid _createbyid;
        /// <summary>
        /// 创建人
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
    }


    [Serializable]
    public partial class SolutionExchangeRateInfo : SolutionExchangeRateList
    {

    }


    [Serializable]
    public partial class SolutionList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        string _cname;
        /// <summary>
        /// 中文名

        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "中文名", EMessage = "CName")]

        [Required(ErrorMessage = "中文名")]

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


        InvoiceDateType _invoicedatetype;
        /// <summary>
        /// 日期类型
        /// </summary>
        [Required(CMessage = "日期类型", EMessage = "InvoiceDateType")]

        public InvoiceDateType InvoiceDateType
        {
            get
            {
                return _invoicedatetype;
            }
            set
            {
                if (_invoicedatetype != value)
                {
                    _invoicedatetype = value;
                    base.OnPropertyChanged("InvoiceDateType", value);
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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            SolutionList newObj = obj as SolutionList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class SolutionInfo : SolutionList
    {
        Guid _createbyid;
        /// <summary>
        /// 创建人

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

        bool _isAccountShare;
        /// <summary>
        /// 是否财务共享
        /// </summary>
        public bool IsAccountShare
        {
            get
            {
                return _isAccountShare;
            }
            set
            {
                if (_isAccountShare != value)
                {
                    _isAccountShare = value;
                    base.OnPropertyChanged("IsAccountShare", value);
                }
            }
        }


        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaximumLength = 2000, CMessage = "备注", EMessage = "Remark")]

        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                if (_remark != value)
                {
                    _remark = value;
                    base.OnPropertyChanged("Remark", value);
                }
            }
        }
    }

    /// <summary>
    /// 会计科目配置类型
    /// </summary>
    [Serializable]
    public partial class GLConfigType : BaseDataObject
    {
        int _index;

        /// <summary>
        /// 唯一键
        /// </summary>
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                if (_index != value)
                {
                    _index = value;
                    base.OnPropertyChanged("Index", value);
                }
            }
        }

        string _name;
        /// <summary>
        /// 类型名
        /// </summary>     
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    base.OnPropertyChanged("Name", value);
                }
            }
        }
    }

    [Serializable]
    public partial class SolutionGLConfigList : BaseDataObject
    {
        public SolutionGLConfigList()
        {
            //this.Type = GLConfigType.CostItem;
        }

        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        Guid _solutionid;
        /// <summary>
        /// 解决方案ID
        /// </summary>
        public Guid SolutionID
        {
            get
            {
                return _solutionid;
            }
            set
            {
                if (_solutionid != value)
                {
                    _solutionid = value;
                    base.OnPropertyChanged("SolutionID", value);
                }
            }
        }
        public Guid? CompanyID
        {
            get;
            set;
        }

        //GLConfigType _type;
        ///// <summary>
        ///// 类型
        ///// </summary>
        //public GLConfigType Type
        //{
        //    get
        //    {
        //        return _type;
        //    }
        //    set
        //    {
        //        if (_type != value)
        //        {
        //            _type = value;
        //            base.OnPropertyChanged("Type", value);
        //        }
        //    }
        //}

        int _gLConfigTypeID;
        /// <summary>
        /// 会计科目配置类型ID
        /// </summary>
        public int GLConfigTypeID
        {
            get
            {
                return _gLConfigTypeID;
            }
            set
            {
                if (_gLConfigTypeID != value)
                {
                    _gLConfigTypeID = value;
                    base.OnPropertyChanged("GLConfigTypeID", value);
                }
            }
        }

        string _gLConfigTypeName;
        /// <summary>
        /// 会计科目配置类型Name
        /// </summary>
        public string GLConfigTypeName
        {
            get
            {
                return _gLConfigTypeName;
            }
            set
            {
                if (_gLConfigTypeName != value)
                {
                    _gLConfigTypeName = value;
                    base.OnPropertyChanged("GLConfigTypeName", value);
                }
            }
        }

        string _chargingcodename;
        /// <summary>
        /// 费用项目
        /// </summary>
        public string ChargingCodeName
        {
            get
            {
                return _chargingcodename;
            }
            set
            {
                if (_chargingcodename != value)
                {
                    _chargingcodename = value;
                    base.OnPropertyChanged("ChargingCodeName", value);
                }
            }
        }


        string _currencyname;
        /// <summary>
        /// 币种
        /// </summary>
        public string CurrencyName
        {
            get
            {
                return _currencyname;
            }
            set
            {
                if (_currencyname != value)
                {
                    _currencyname = value;
                    base.OnPropertyChanged("CurrencyName", value);
                }
            }
        }


        string _drglcodename;
        /// <summary>
        /// 应收会计科目
        /// </summary>
        public string DRGLCodeName
        {
            get
            {
                return _drglcodename;
            }
            set
            {
                if (_drglcodename != value)
                {
                    _drglcodename = value;
                    base.OnPropertyChanged("DRGLCodeName", value);
                }
            }
        }

        public string DRGLFullName
        {
            get;
            set;
        }
        public string CRGLFullName
        {
            get;
            set;
        }
        string _crglcodename;
        /// <summary>
        /// 应付会计科目
        /// </summary>
        public string CRGLCodeName
        {
            get
            {
                return _crglcodename;
            }
            set
            {
                if (_crglcodename != value)
                {
                    _crglcodename = value;
                    base.OnPropertyChanged("CRGLCodeName", value);
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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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
        /// 创建人


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


        Guid? _chargingcodeid;
        /// <summary>
        /// 费用项目ID（当类型为0费用项目时不可为空）
        /// </summary>
        public Guid? ChargingCodeID
        {
            get
            {
                return _chargingcodeid;
            }
            set
            {
                if (_chargingcodeid != value)
                {
                    _chargingcodeid = value;
                    base.OnPropertyChanged("ChargingCodeID", value);
                }
            }
        }


        Guid _currencyid;
        /// <summary>
        /// 币种ID
        /// </summary>
        [GuidRequired(ErrorMessage = "币种必须填写")]

        public Guid CurrencyID
        {
            get
            {
                return _currencyid;
            }
            set
            {
                if (_currencyid != value)
                {
                    _currencyid = value;
                    base.OnPropertyChanged("CurrencyID", value);
                }
            }
        }


        Guid _drglcodeid;
        /// <summary>
        /// 应收会计科目ID
        /// </summary>
        [GuidRequired(ErrorMessage = "应收会计科目必须填写")]

        public Guid DRGLCodeID
        {
            get
            {
                return _drglcodeid;
            }
            set
            {
                if (_drglcodeid != value)
                {
                    _drglcodeid = value;
                    base.OnPropertyChanged("DRGLCodeID", value);
                }
            }
        }


        Guid _crglcodeid;
        /// <summary>
        /// 应付会计科目ID
        /// </summary>
        [GuidRequired(ErrorMessage = "应付会计科目必须填写")]

        public Guid CRGLCodeID
        {
            get
            {
                return _crglcodeid;
            }
            set
            {
                if (_crglcodeid != value)
                {
                    _crglcodeid = value;
                    base.OnPropertyChanged("CRGLCodeID", value);
                }
            }
        }


        public override bool Equals(object obj)
        {
            SolutionGLConfigList newObj = obj as SolutionGLConfigList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class SolutionGLConfigInfo : SolutionGLConfigList
    {



    }


    [Serializable]
    public partial class SolutionGLCodeList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

        #region 选择
        /// <summary>
        /// 选择
        /// </summary>
        public bool Selected { get; set; }
        #endregion

        public bool IsLeaf { get; set; }

        #region ID
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
        #endregion

        #region 解决方案 ID
        Guid _solutionid;
        /// <summary>
        /// 解决方案ID
        /// </summary>
        public Guid SolutionID
        {
            get
            {
                return _solutionid;
            }
            set
            {
                if (_solutionid != value)
                {
                    _solutionid = value;
                    base.OnPropertyChanged("SolutionID", value);
                }
            }
        }
        #endregion

        #region 科目代码
        string _code;
        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "", EMessage = "Code")]
        [Required(CMessage = "", EMessage = "Code")]
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

        #endregion

        #region 科目代码名称
        string _glCodeName;
        /// <summary>
        /// 科目代码名称
        /// </summary>
        public string GLCodeName
        {
            get
            {
                return _glCodeName = "(" + Code + ")-" + (LocalData.IsEnglish ? EfullName : FullName);
            }
            set
            {
                if (_glCodeName != value)
                {
                    _glCodeName = value;
                    base.OnPropertyChanged("GLCodeName", value);
                }
            }
        }

        #endregion

        #region 组名

        string _groupByname;
        /// <summary>
        /// 组名
        /// </summary>
        public string GroupByName
        {
            get
            {
                return _groupByname;
            }
            set
            {
                if (_groupByname != value)
                {
                    _groupByname = value;
                    base.OnPropertyChanged("GroupByName", value);
                }
            }
        }

        #endregion

        #region 组ID
        Guid _groupByid;
        /// <summary>
        /// 组ID
        /// </summary>
        public Guid GroupByID
        {
            get
            {
                return _groupByid;
            }
            set
            {
                if (_groupByid != value)
                {
                    _groupByid = value;
                    base.OnPropertyChanged("GroupByID", value);
                }
            }
        }
        #endregion

        #region 上级名称

        string _parentname;
        /// <summary>
        /// 上级名称
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

        #endregion

        #region 上级ID
        Guid? _parentid;
        /// <summary>
        /// 上级ID
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
        #endregion

        #region 联级代码
        string _grouphierarchycode;
        /// <summary>
        /// 分组级联代码
        /// </summary>
        public string GroupHierarchyCode
        {
            get
            {
                return _grouphierarchycode;
            }
            set
            {
                if (_grouphierarchycode != value)
                {
                    _grouphierarchycode = value;
                    base.OnPropertyChanged("GroupHierarchyCode", value);
                }
            }
        }
        #endregion


        #region 联组代码
        private string hierarchyCode;
        /// <summary>
        /// 联组代码
        /// </summary>
        public string HierarchyCode
        {
            get
            {
                return hierarchyCode;
            }
            set
            {
                if (hierarchyCode != value)
                {
                    hierarchyCode = value;
                    base.OnPropertyChanged("HierarchyCode", value);
                }
            }
        }
        #endregion

        #region 级别代码
        private Int32? levelCode;
        /// <summary>
        /// 级别代码
        /// </summary>
        public Int32? LevelCode
        {
            get
            {
                return levelCode;
            }
            set
            {
                if (levelCode != value)
                {
                    levelCode = value;
                    base.OnPropertyChanged("LevelCode", value);
                }
            }
        }

        #endregion

        #region 节点类型
        GLCodeNodeType _glcodenodetype;
        /// <summary>
        /// 节点类型
        /// </summary>
        public GLCodeNodeType GLCodeNodeType
        {
            get
            {
                return _glcodenodetype;
            }
            set
            {
                if (_glcodenodetype != value)
                {
                    _glcodenodetype = value;
                    base.OnPropertyChanged("GLCodeNodeType", value);
                }
            }
        }
        #endregion

        #region 科目类型
        private GLCodeType glCodeType;
        /// <summary>
        /// 科目类型
        /// </summary>
        public GLCodeType GLCodeType
        {
            get
            {
                return glCodeType;
            }
            set
            {
                if (glCodeType != value)
                {
                    glCodeType = value;
                    base.OnPropertyChanged("GLCodeType", value);
                }
            }
        }
        #endregion

        #region 帐页格式
        private GLCodeLedgerStyle ledgerStyle;
        /// <summary>
        /// 帐页格式
        /// </summary>
        public GLCodeLedgerStyle LedgerStyle
        {
            get
            {
                return ledgerStyle;
            }
            set
            {
                if (ledgerStyle != value)
                {
                    ledgerStyle = value;
                    base.OnPropertyChanged("LedgerStyle", value);
                }
            }
        }
        #endregion

        #region 科目性质(余额方向)
        private GLCodeProperty glcodeProperty;
        /// <summary>
        /// 科目性质(余额方向)
        /// </summary>
        public GLCodeProperty GLCodeProperty
        {
            get
            {
                return glcodeProperty;
            }
            set
            {
                if (glcodeProperty != value)
                {
                    glcodeProperty = value;
                    base.OnPropertyChanged("GLCodeProperty", value);
                }
            }
        }
        #endregion

        #region 外币核算
        private bool isForeignCheck;
        /// <summary>
        /// 外币核算
        /// </summary>
        public bool IsForeignCheck
        {
            get
            {
                return isForeignCheck;
            }
            set
            {
                if (isForeignCheck != value)
                {
                    isForeignCheck = value;
                    base.OnPropertyChanged("IsForeignCheck", value);
                }
            }
        }
        #endregion

        #region  外币ID
        private Guid? foreignCurrencyID;
        /// <summary>
        /// 外币ID
        /// </summary>
        public Guid? ForeignCurrencyID
        {
            get
            {
                return foreignCurrencyID;
            }
            set
            {
                if (foreignCurrencyID != value)
                {
                    foreignCurrencyID = value;
                    base.OnPropertyChanged("ForeignCurrencyID", value);
                }
            }
        }
        #endregion

        #region 外币名称
        private string foreignCurrencyName;
        /// <summary>
        /// 外币名称
        /// </summary>
        public string ForeignCurrencyName
        {
            get
            {
                return foreignCurrencyName;
            }
            set
            {
                if (foreignCurrencyName != value)
                {
                    foreignCurrencyName = value;
                    this.NotifyPropertyChanged(o => o.ForeignCurrencyName);
                }
            }

        }

        #endregion

        #region 数量核算
        private bool isNumberCheck;
        /// <summary>
        /// 数量核算
        /// </summary>
        public bool IsNumberCheck
        {
            get
            {
                return isNumberCheck;
            }
            set
            {
                if (isNumberCheck != value)
                {
                    isNumberCheck = value;
                    this.NotifyPropertyChanged(o => o.IsNumberCheck);
                }
            }
        }
        #endregion

        #region 计量单位
        private string unitName;
        /// <summary>
        /// 计量单位 
        /// </summary>
        public string UnitName
        {
            get
            {
                return unitName;
            }
            set
            {
                if (unitName != value)
                {
                    unitName = value;
                    this.NotifyPropertyChanged(o => o.UnitName);
                }
            }
        }
        #endregion

        #region 部门核算
        private bool isDepartmentCheck;
        /// <summary>
        /// 部门核算
        /// </summary>
        public bool IsDepartmentCheck
        {
            get
            {
                return isDepartmentCheck;
            }
            set
            {
                if (isDepartmentCheck != value)
                {
                    isDepartmentCheck = value;
                    this.NotifyPropertyChanged(o => o.IsDepartmentCheck);
                }
            }
        }
        #endregion

        #region 个人核算
        private bool isPersonalCheck;
        /// <summary>
        /// 个人核算
        /// </summary>
        public bool IsPersonalCheck
        {
            get
            {
                return isPersonalCheck;
            }
            set
            {
                if (isPersonalCheck != value)
                {
                    isPersonalCheck = value;
                    this.NotifyPropertyChanged(o => o.IsPersonalCheck);
                }
            }
        }
        #endregion

        #region 往来单位
        private bool isCustomerCheck;
        /// <summary>
        /// 往来单位
        /// </summary>
        public bool IsCustomerCheck
        {
            get
            {
                return isCustomerCheck;
            }
            set
            {
                if (isCustomerCheck != value)
                {
                    isCustomerCheck = value;
                    this.NotifyPropertyChanged(o => o.IsCustomerCheck);
                }
            }
        }
        #endregion

        #region  日记帐
        private bool isJournal;
        /// <summary>
        /// 往来单位
        /// </summary>
        public bool IsJournal
        {
            get
            {
                return isJournal;
            }
            set
            {
                if (isJournal != value)
                {
                    isJournal = value;
                    this.NotifyPropertyChanged(o => o.IsJournal);
                }
            }
        }

        #endregion

        #region 银行帐
        private bool isBankAccount;
        /// <summary>
        /// 银行帐
        /// </summary>
        public bool IsBankAccount
        {
            get
            {
                return isBankAccount;
            }
            set
            {
                if (isBankAccount != value)
                {
                    isBankAccount = value;
                    this.NotifyPropertyChanged(o => o.IsBankAccount);
                }
            }
        }
        #endregion

        #region 中文名
        string _cname;
        /// <summary>
        /// 中文名
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
        #endregion

        #region   英文名
        string _ename;
        /// <summary>
        /// 英文名
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
        #endregion

        #region 中文全称
        string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }
        #endregion

        #region 英文全称
        string _efullName;
        public string EfullName
        {
            get { return _efullName; }
            set { _efullName = value; }
        }
        #endregion

        #region 有效性
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
        #endregion

        #region 创建人
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
        #endregion

        #region 创建时间
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
        #endregion

        #region 更新时间
        DateTime? _updateDate;
        /// <summary>
        /// 行版本
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

        #endregion

        #region 创建人
        Guid _createbyid;
        /// <summary>
        /// 创建人
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

        #endregion

        #region 描述
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

        #endregion

        #region 辅助核算描述
        public string AidCheckDescription
        {
            get
            {
                string str = string.Empty;
                if (LocalData.IsEnglish)
                {
                    if (IsDepartmentCheck)
                    {
                        str += "Department Check;";
                    }
                    if (IsPersonalCheck)
                    {
                        str += "Personal Check;";
                    }
                    if (IsCustomerCheck)
                    {
                        str += "Customer Check;";
                    }
                }
                else
                {
                    if (IsDepartmentCheck)
                    {
                        str += "部门核算;";
                    }
                    if (IsPersonalCheck)
                    {
                        str += "个人核算;";
                    }
                    if (IsCustomerCheck)
                    {
                        str += "客户往来;";
                    }
                }
                if (!string.IsNullOrEmpty(str))
                {
                    str = str.Substring(0, str.Length - 1);
                }
                return str;
            }
        }
        #endregion

        #region 是否为费用
        bool? isFee;
        /// <summary>
        /// 是否为费用
        /// </summary>
        public bool? IsFee
        {
            get
            {
                return isFee;
            }
            set
            {
                if (isFee != value)
                {
                    isFee = value;
                    base.OnPropertyChanged("IsFee", value);
                }
            }
        }

        #endregion

        #region 公司ID

        Guid? companyID;
        public Guid? CompanyID
        {
            get
            {
                return companyID;
            }
            set
            {
                if (companyID != value)
                {
                    companyID = value;
                    base.OnPropertyChanged("CompanyID", value);
                }
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            SolutionGLCodeList newObj = obj as SolutionGLCodeList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }

    [Serializable]
    public partial class GL2COMPANY : BaseDataObject
    {
        #region ID
        Guid _id;
        /// <summary>
        /// 创建人
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
        #endregion

        #region GLID
        Guid _glid;
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid GLID
        {
            get
            {
                return _glid;
            }
            set
            {
                if (_glid != value)
                {
                    _glid = value;
                    base.OnPropertyChanged("GLID", value);
                }
            }
        }
        #endregion

        #region CompanyID
        Guid _companyid;
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CompanyID
        {
            get
            {
                return _companyid;
            }
            set
            {
                if (_companyid != value)
                {
                    _companyid = value;
                    base.OnPropertyChanged("CompanyID", value);
                }
            }
        }
        #endregion

        #region Code
        string _code;
        /// <summary>
        /// 创建人
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
        #endregion
    }

    [Serializable]
    public partial class SolutionGLCodeInfo : SolutionGLCodeList
    {


    }


    [Serializable]
    public partial class SolutionAgentList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        Guid _solutionid;
        /// <summary>
        /// 解决方案ID
        /// </summary>
        public Guid SolutionID
        {
            get
            {
                return _solutionid;
            }
            set
            {
                if (_solutionid != value)
                {
                    _solutionid = value;
                    base.OnPropertyChanged("SolutionID", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 创建人

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


        Guid _agentid;
        /// <summary>
        /// 代理ID（对应公共客户信息）
        /// </summary>
        [GuidRequired(CMessage = "代理", EMessage = "Agent")]

        public Guid AgentID
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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaximumLength = 500, CMessage = "备注", EMessage = "Remark")]

        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                if (_remark != value)
                {
                    _remark = value;
                    base.OnPropertyChanged("Remark", value);
                }
            }
        }


        public override bool Equals(object obj)
        {
            SolutionAgentList newObj = obj as SolutionAgentList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class SolutionAgentInfo : SolutionAgentList
    {
    }


    [Serializable]
    public partial class SolutionGLGroupList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        string _code;
        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(MaximumLength = 10, CMessage = "代码", EMessage = "Code")]

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
        /// 英文名

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


        Guid? _parentid;
        /// <summary>
        /// 父ID
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
        /// 父名
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

        private ChargingGroupType _type;

        public ChargingGroupType Type
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

        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            SolutionGLGroupList newObj = obj as SolutionGLGroupList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class SolutionGLGroupInfo : SolutionGLGroupList
    {
        Guid _createbyid;
        /// <summary>
        /// 创建人

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


    }


    [Serializable]
    public partial class SolutionCodeRuleList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        Guid _solutionid;
        /// <summary>
        /// 解决方案ID
        /// </summary>
        public Guid SolutionID
        {
            get
            {
                return _solutionid;
            }
            set
            {
                if (_solutionid != value)
                {
                    _solutionid = value;
                    base.OnPropertyChanged("SolutionID", value);
                }
            }
        }


        Guid _configurekeyid;
        /// <summary>
        /// 配置键ID
        /// </summary>
        [GuidRequired(CMessage = "配置键", EMessage = "ConfigureKey")]

        public Guid ConfigureKeyID
        {
            get
            {
                return _configurekeyid;
            }
            set
            {
                if (_configurekeyid != value)
                {
                    _configurekeyid = value;
                    base.OnPropertyChanged("ConfigureKeyID", value);
                }
            }
        }


        string _configurekeyname;
        /// <summary>
        /// 键

        /// </summary>
        public string ConfigureKeyName
        {
            get
            {
                return _configurekeyname;
            }
            set
            {
                if (_configurekeyname != value)
                {
                    _configurekeyname = value;
                    base.OnPropertyChanged("ConfigureKeyName", value);
                }
            }
        }


        string _description;
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "描述", EMessage = "Description")]

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


        bool _isincludecompanycode;
        /// <summary>
        /// 包含公司代码
        /// </summary>
        [Required(CMessage = "包含公司代码", EMessage = "IsIncludeCompanyCode")]

        public bool IsIncludeCompanyCode
        {
            get
            {
                return _isincludecompanycode;
            }
            set
            {
                if (_isincludecompanycode != value)
                {
                    _isincludecompanycode = value;
                    base.OnPropertyChanged("IsIncludeCompanyCode", value);
                }
            }
        }


        string _codeprefix;
        /// <summary>
        /// 前缀
        /// </summary>
        [StringLength(MaximumLength = 6, CMessage = "前缀", EMessage = "CodePrefix")]

        public string CodePrefix
        {
            get
            {
                return _codeprefix;
            }
            set
            {
                if (_codeprefix != value)
                {
                    _codeprefix = value;
                    base.OnPropertyChanged("CodePrefix", value);
                }
            }
        }


        CodeYearFormart _codeyear;
        /// <summary>
        /// 年
        /// </summary>
        [Required(CMessage = "年份", EMessage = "CodeYear")]

        public CodeYearFormart CodeYear
        {
            get
            {
                return _codeyear;
            }
            set
            {
                if (_codeyear != value)
                {
                    _codeyear = value;
                    base.OnPropertyChanged("CodeYear", value);
                }
            }
        }


        bool _codemonth;
        /// <summary>
        /// 月

        /// </summary>
        [Required(CMessage = "月份", EMessage = "CodeMonth")]

        public bool CodeMonth
        {
            get
            {
                return _codemonth;
            }
            set
            {
                if (_codemonth != value)
                {
                    _codemonth = value;
                    base.OnPropertyChanged("CodeMonth", value);
                }
            }
        }


        short _codesnlength;
        /// <summary>
        /// 序列号长度

        /// </summary>
        [Required(CMessage = "序列号长度", EMessage = "CodeSnlength")]

        public short CodeSNLength
        {
            get
            {
                return _codesnlength;
            }
            set
            {
                if (_codesnlength != value)
                {
                    _codesnlength = value;
                    base.OnPropertyChanged("CodeSNLength", value);
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


        Guid _createbyid;
        /// <summary>
        /// 创建人

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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            SolutionCodeRuleList newObj = obj as SolutionCodeRuleList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class SolutionCodeRuleInfo : SolutionCodeRuleList
    {
    }

    #endregion

    #region  Vessel
    [Serializable]
    public partial class VesselList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        /// <summary>
        /// IMO
        /// </summary>
        string _imo;

        public string IMO
        {
            get { return _imo; }
            set
            {
                if (_imo != value)
                {
                    _imo = value;
                    base.OnPropertyChanged("IMO", value);
                }
            }
        }

        /// <summary>
        /// UNCode
        /// </summary>
        string _uncode;
        public string UNCode
        {

            get { return _uncode; }
            set
            {
                if (_uncode != value)
                {
                    _uncode = value;
                    base.OnPropertyChanged("UNCode", value);
                }
            }
        }


        /// <summary>
        /// 船籍
        /// </summary>
        Guid? _registration;

        public Guid? Registration
        {
            get { return _registration; }
            set
            {
                if (_registration != value)
                {
                    _registration = value;
                    base.OnPropertyChanged("Registration", value);
                }
            }
        }
        /// <summary>
        /// 船籍
        /// </summary>
        string _registrationName;

        public string RegistrationName
        {
            get { return _registrationName; }
            set
            {
                if (_registrationName != value)
                {
                    _registrationName = value;
                    base.OnPropertyChanged("VesselName", value);
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


        string _name;
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "名称", EMessage = "Name")]

        [Required(CMessage = "名称", EMessage = "Name")]

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    base.OnPropertyChanged("Name", value);
                }
            }
        }


        string _carriername;
        /// <summary>
        /// 船东
        /// </summary>
        public string CarrierName
        {
            get
            {
                return _carriername;
            }
            set
            {
                if (_carriername != value)
                {
                    _carriername = value;
                    base.OnPropertyChanged("CarrierName", value);
                }
            }
        }
        public string CarrierCode
        {
            get;
            set;
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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            VesselList newObj = obj as VesselList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }




    [Serializable]
    public partial class VesselInfo : VesselList
    {
        Guid _createbyid;
        /// <summary>
        /// 创建人

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


        Guid _carrierid;
        /// <summary>
        /// 船东ID
        /// </summary>
        [GuidRequired(CMessage = "船东", EMessage = "Carrier")]

        public Guid CarrierID
        {
            get
            {
                return _carrierid;
            }
            set
            {
                if (_carrierid != value)
                {
                    _carrierid = value;
                    base.OnPropertyChanged("CarrierID", value);
                }
            }
        }
    }

    #endregion

    #region  Voyage

    [Serializable]
    public partial class VoyageList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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
        string _vesselAndNo;
        /// <summary>
        /// 船名和航次格式化显示 格式：船名/航次
        /// </summary>
        public string VesselAndNo
        {
            get
            {
                return _vesselAndNo;
            }
            set
            {
                if (_vesselAndNo != value)
                {
                    _vesselAndNo = value;
                    base.OnPropertyChanged("VesselAndNo", value);
                }
            }
        }

        string _vesselname;
        /// <summary>
        /// 船名
        /// </summary>
        public string VesselName
        {
            get
            {
                return _vesselname;
            }
            set
            {
                if (_vesselname != value)
                {
                    _vesselname = value;
                    base.OnPropertyChanged("VesselName", value);
                }
            }
        }

        string _no;
        /// <summary>
        /// 航次
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "航次", EMessage = "No")]

        [Required(CMessage = "航次", EMessage = "No")]

        public string No
        {
            get
            {
                return _no;
            }
            set
            {
                if (_no != value)
                {
                    _no = value;
                    base.OnPropertyChanged("No", value);
                }
            }
        }


        /// <summary>
        /// UNCode
        /// </summary>
        string _uncode;
        public string UNCode
        {

            get { return _uncode; }
            set
            {
                if (_uncode != value)
                {
                    _uncode = value;
                    base.OnPropertyChanged("UNCode", value);
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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        public override bool Equals(object obj)
        {
            VoyageList newObj = obj as VoyageList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }


    [Serializable]
    public partial class VoyageInfo : VoyageList
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


        Guid _vesselid;
        /// <summary>
        /// 船名ID
        /// </summary>
        [GuidRequired(CMessage = "船名", EMessage = "Vessel")]
        public Guid VesselID
        {
            get
            {
                return _vesselid;
            }
            set
            {
                if (_vesselid != value)
                {
                    _vesselid = value;
                    base.OnPropertyChanged("VesselID", value);
                }
            }
        }
    }
    #endregion

    #region VoyageETDETAList
    /// <summary>
    /// 船名航次的EDA,ETD
    /// </summary>
    [Serializable]
    public partial class VoyageETDETAList : BaseDataObject
    {
        public DateTime? ETD { get; set; }
        public DateTime? ETA { get; set; }
    } 
    #endregion

    #region  EDI
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class EDIConfigureList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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

        Guid _serviceConfigureKeyID;
        /// <summary>
        /// 服务配置KeyID
        /// </summary>
        [GuidRequired(CMessage = "服务名", EMessage = "ServiceConfigureKey")]
        public Guid ServiceConfigureKeyID
        {
            get
            {
                return _serviceConfigureKeyID;
            }
            set
            {
                if (_serviceConfigureKeyID != value)
                {
                    _serviceConfigureKeyID = value;
                    base.OnPropertyChanged("ServiceConfigureKeyID", value);
                }
            }
        }

        public string Code
        {
            get;
            set;
        }

        string _configurekeyname;
        /// <summary>
        /// 服务名
        /// </summary>
        public string ServiceConfigureKeyName
        {
            get
            {
                return _configurekeyname;
            }
            set
            {
                if (_configurekeyname != value)
                {
                    _configurekeyname = value;
                    base.OnPropertyChanged("ConfigureKeyName", value);
                }
            }
        }

        //string _code;
        ///// <summary>
        ///// 代码
        ///// </summary>
        //[Required(ErrorMessage = "代码必须填写")]
        //public string Code
        //{
        //    get
        //    {
        //        return _code;
        //    }
        //    set
        //    {
        //        if (_code != value)
        //        {
        //            _code = value;
        //            base.OnPropertyChanged("Code", value);
        //        }
        //    }
        //}

        string _Component;
        /// <summary>
        /// 组件
        /// </summary>
        [Required(CMessage = "组件", EMessage = "Component")]
        public string Component
        {
            get
            {
                return _Component;
            }
            set
            {
                if (_Component != value)
                {
                    _Component = value;
                    base.OnPropertyChanged("Component", value);
                }
            }
        }

        string _FTP;
        /// <summary>
        /// FTP目录
        /// </summary>
        public string FTP
        {
            get
            {
                return _FTP;
            }
            set
            {
                if (_FTP != value)
                {
                    _FTP = value;
                    base.OnPropertyChanged("FTP", value);
                }
            }
        }

        string _FileFormat;
        /// <summary>
        /// 文件格式
        /// </summary>
        [Required(CMessage = "文件格式", EMessage = "FileFormat")]
        public string FileFormat
        {
            get
            {
                return _FileFormat;
            }
            set
            {
                if (_FileFormat != value)
                {
                    _FileFormat = value;
                    base.OnPropertyChanged("FileFormat", value);
                }
            }
        }

        string _DataFormat;
        /// <summary>
        /// 数据格式
        /// </summary>
        [Required(CMessage = "数据格式", EMessage = "DataFormat")]
        public string DataFormat
        {
            get
            {
                return _DataFormat;
            }
            set
            {
                if (_DataFormat != value)
                {
                    _DataFormat = value;
                    base.OnPropertyChanged("DataFormat", value);
                }
            }
        }

        string _RegularFile;
        /// <summary>
        /// 规则文件
        /// </summary>
        public string RegularFile
        {
            get
            {
                return _RegularFile;
            }
            set
            {
                if (_RegularFile != value)
                {
                    _RegularFile = value;
                    base.OnPropertyChanged("RegularFile", value);
                }
            }
        }

        string _StoredProcedure;
        /// <summary>
        ///存储过程名称
        /// </summary>
        public string StoredProcedure
        {
            get
            {
                return _StoredProcedure;
            }
            set
            {
                if (_StoredProcedure != value)
                {
                    _StoredProcedure = value;
                    base.OnPropertyChanged("StoredProcedure", value);
                }
            }
        }

        Guid? _carrierID;
        /// <summary>
        /// 船东ID
        /// </summary>
        public Guid? CarrierID
        {
            get
            {
                return _carrierID;
            }
            set
            {
                if (_carrierID != value)
                {
                    _carrierID = value;
                    base.OnPropertyChanged("CarrierID", value);
                }
            }
        }

        string _carriername;
        /// <summary>
        /// 船公司
        /// </summary>
        public string CarrierName
        {
            get
            {
                return _carriername;
            }
            set
            {
                if (_carriername != value)
                {
                    _carriername = value;
                    base.OnPropertyChanged("CarrierName", value);
                }
            }
        }


        EDIUploadMode _uploadMode;
        /// <summary>
        /// 发送模式（1:EMail,2:FTP）
        /// </summary>
        public EDIUploadMode UploadMode
        {
            get
            {
                return _uploadMode;
            }
            set
            {
                if (_uploadMode != value)
                {
                    _uploadMode = value;
                    base.OnPropertyChanged("UploadMode", value);
                }
            }
        }



        EDIMode _ediMode;
        /// <summary>
        /// edi类型（订舱/补料）
        /// </summary>
        public EDIMode EDIMode
        {
            get { return _ediMode; }
            set
            {
                if (_ediMode != value)
                {
                    _ediMode = value;
                    base.OnPropertyChanged("EDIMode", value);
                }
            }
        }

        string _serverAddress;
        /// <summary>
        /// 服务器地址
        /// </summary>
        [Required(CMessage = "服务器地址", EMessage = "ServerAddress")]
        [StringLength(MaximumLength = 100, CMessage = "服务器地址", EMessage = "ServerAddress")]
        public string ServerAddress
        {
            get
            {
                return _serverAddress;
            }
            set
            {
                if (_serverAddress != value)
                {
                    _serverAddress = value;
                    base.OnPropertyChanged("ServerAddress", value);
                }
            }
        }


        string _userName;
        /// <summary>
        /// 帐号
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "帐号", EMessage = "UserName")]
        [Required(CMessage = "帐号", EMessage = "UserName")]
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    base.OnPropertyChanged("UserName", value);
                }
            }
        }

        string _password;
        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(MaximumLength = 20, CMessage = "密码", EMessage = "PassWord")]
        [Required(CMessage = "密码", EMessage = "PassWord")]
        public string Password
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
                    base.OnPropertyChanged("Password", value);
                }
            }
        }

        string _ReceiveAddress;
        /// <summary>
        /// 反馈地址（如果是邮件就是收件人地址，如果是FTP就是路径）
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "反馈地址", EMessage = "ReceiveAddress")]
        //[RegularExpression(Pattern = "^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", CMessage = "邮箱格式不正确。例:xx@hotmail.com", EMessage = "Email Format is not correct. example:xx@hotmail.com", IsUseErrorTemplate = false)]
        public string ReceiveAddress
        {
            get
            {
                return _ReceiveAddress;
            }
            set
            {
                if (_ReceiveAddress != value)
                {
                    _ReceiveAddress = value;
                    base.OnPropertyChanged("ReceiveAddress", value);
                }
            }
        }

        byte _receivertype;
        /// <summary>
        /// 接收方类型
        /// </summary>
        public byte ReceiverType
        {
            get
            {
                return _receivertype;
            }
            set
            {
                if (_receivertype != value)
                {
                    _receivertype = value;
                    base.OnPropertyChanged("ReceiverType", value);
                }
            }
        }

        Guid _createbyid;
        /// <summary>
        /// 创建人
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
                    //base.OnPropertyChanged("CreateDate", value);
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

        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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
                    // base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }


        public override bool Equals(object obj)
        {
            EDIConfigureList newObj = obj as EDIConfigureList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }

    #endregion

    #region  Report

    [Serializable]
    public partial class ReportCompanyConfigureList : ConfigureList
    {
        public ReportCompanyConfigureList()
        {
            Parameters = new List<ReportParameterList>();
        }

        public List<ReportParameterList> Parameters { get; set; }
    }

    [Serializable]
    public partial class ReportConfigureList : BaseDataObject
    {
        public ReportConfigureList()
        {
            Parameters = new List<ReportParameterList>();
        }

        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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

        string _cDescription;
        /// <summary>
        /// 中文描述
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "中文描述", EMessage = "CDescription")]
        [Required(CMessage = "中文描述", EMessage = "CDescription")]
        public string CDescription
        {
            get
            {
                return _cDescription;
            }
            set
            {
                if (_cDescription != value)
                {
                    _cDescription = value;
                    base.OnPropertyChanged("CDescription", value);
                }
            }
        }

        string _eDescription;
        /// <summary>
        /// 英文描述
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "英文描述", EMessage = "EDescription")]
        [Required(CMessage = "英文描述", EMessage = "EDescription")]
        public string EDescription
        {
            get
            {
                return _eDescription;
            }
            set
            {
                if (_eDescription != value)
                {
                    _eDescription = value;
                    base.OnPropertyChanged("EDescription", value);
                }
            }
        }

        int? _reportTypeID;
        /// <summary>
        /// 报表类型ID
        /// </summary>
        public int? ReportTypeID
        {
            get
            {
                return _reportTypeID;
            }
            set
            {
                if (_reportTypeID != value)
                {
                    _reportTypeID = value;
                    base.OnPropertyChanged("ReportTypeID", value);
                }
            }
        }

        string _reportTypeName;
        /// <summary>
        /// 报表类型Name
        /// </summary>
        public string ReportTypeName
        {
            get
            {
                return _reportTypeName;
            }
            set
            {
                if (_reportTypeName != value)
                {
                    _reportTypeName = value;
                    base.OnPropertyChanged("ReportTypeName", value);
                }
            }
        }

        public List<ReportParameterList> Parameters { get; set; }

        bool _isValid;
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                if (_isValid != value)
                {
                    _isValid = value;
                    base.OnPropertyChanged("IsValid", value);
                }
            }
        }

        Guid _createbyid;
        /// <summary>
        /// 创建人
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

        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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

        public override bool Equals(object obj)
        {
            ReportConfigureList newObj = obj as ReportConfigureList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }

    [Serializable]
    public partial class ReportType : BaseDataObject
    {
        int _index;

        /// <summary>
        /// 唯一键
        /// </summary>
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                if (_index != value)
                {
                    _index = value;
                    base.OnPropertyChanged("Index", value);
                }
            }
        }

        string _name;
        /// <summary>
        /// 报表类型名
        /// </summary>     
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    base.OnPropertyChanged("Name", value);
                }
            }
        }
    }

    [Serializable]
    public class ReportParameterList : BaseDataObject
    {
        public ReportParameterList()
        {
            CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            ParameterType = ReportParameterType.String;
        }

        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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

        Guid _reportID;
        /// <summary>
        /// 报表配置ID
        /// </summary>
        public Guid ReportID
        {
            get
            {
                return _reportID;
            }
            set
            {
                if (_reportID != value)
                {
                    _reportID = value;
                    base.OnPropertyChanged("ReportID", value);
                }
            }
        }

        string _code;
        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "参数代码", EMessage = "Code")]
        [Required(CMessage = "参数代码", EMessage = "Code")]
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

        string _cDescription;
        /// <summary>
        /// 中文描述
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "中文描述", EMessage = "CDescription")]
        //[Required(ErrorMessage = "中文描述必须填写")]
        public string CDescription
        {
            get
            {
                return _cDescription;
            }
            set
            {
                if (_cDescription != value)
                {
                    _cDescription = value;
                    base.OnPropertyChanged("CDescription", value);
                }
            }
        }

        string _eDescription;
        /// <summary>
        /// 英文描述
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "英文描述", EMessage = "EDescription")]
        //[Required(ErrorMessage = "英文描述必须填写")]
        public string EDescription
        {
            get
            {
                return _eDescription;
            }
            set
            {
                if (_eDescription != value)
                {
                    _eDescription = value;
                    base.OnPropertyChanged("EDescription", value);
                }
            }
        }

        ReportParameterType _type;
        /// <summary>
        /// 参数类型
        /// </summary>
        [Required(CMessage = "参数类型", EMessage = "Type")]
        public ReportParameterType ParameterType
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
                    base.OnPropertyChanged("ParameterType", value);
                }
            }
        }

        Guid _createbyid;
        /// <summary>
        /// 创建人
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

        string _value;
        /// <summary>
        /// 参数值(根据参数类型自行转换值)
        /// </summary>
        public string ParameterValue
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    base.OnPropertyChanged("ParameterValue", value);
                }
            }
        }

        object _parameterValueObject;
        /// <summary>
        /// 参数值(根据参数类型转换值)
        /// </summary>
        public object ParameterValueObject
        {
            get
            {
                return _parameterValueObject;
            }
            set
            {
                if (_parameterValueObject != value)
                {
                    _parameterValueObject = value;
                    base.OnPropertyChanged("ParameterValueObject", value);
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

        DateTime? _updateDate;
        /// <summary>
        /// 行版本
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

        public bool isNewTag { get; set; }

        public override bool Equals(object obj)
        {
            ReportParameterList newObj = obj as ReportParameterList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }

    [Serializable]
    public class XMLReportParameterList : BaseDataObject
    {
        public XMLReportParameterList()
        {
            CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            ParameterType = ReportParameterType.String;
        }

        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        // [XmlElement("ID")]
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


        Guid _reportID;
        /// <summary>
        /// 报表配置ID
        /// </summary>
        // [XmlElement("ReportID")]
        [XmlIgnore]
        public Guid ReportID
        {
            get
            {
                return _reportID;
            }
            set
            {
                if (_reportID != value)
                {
                    _reportID = value;
                    base.OnPropertyChanged("ReportID", value);
                }
            }
        }

        string _code;
        /// <summary>
        /// 代码
        /// </summary>
        // [XmlElement("Code")]
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

        string _cDescription;
        /// <summary>
        /// 中文描述
        /// </summary>
        // [XmlElement("CDescription")]
        public string CDescription
        {
            get
            {
                return _cDescription;
            }
            set
            {
                if (_cDescription != value)
                {
                    _cDescription = value;
                    base.OnPropertyChanged("CDescription", value);
                }
            }
        }

        string _eDescription;
        /// <summary>
        /// 英文描述
        /// </summary>
        // [XmlElement("EDescription")]
        public string EDescription
        {
            get
            {
                return _eDescription;
            }
            set
            {
                if (_eDescription != value)
                {
                    _eDescription = value;
                    base.OnPropertyChanged("EDescription", value);
                }
            }
        }

        //string _cname;
        ///// <summary>
        ///// 中文名
        ///// </summary>
        // [XmlElement("CName")]
        //public string CName
        //{
        //    get
        //    {
        //        return _cname;
        //    }
        //    set
        //    {
        //        if (_cname != value)
        //        {
        //            _cname = value;
        //            base.OnPropertyChanged("CName", value);
        //        }
        //    }
        //}


        //string _ename;
        ///// <summary>
        ///// 中文名
        ///// </summary>
        ////[XmlElement("EName")]
        //public string EName
        //{
        //    get
        //    {
        //        return _ename;
        //    }
        //    set
        //    {
        //        if (_ename != value)
        //        {
        //            _ename = value;
        //            base.OnPropertyChanged("EName", value);
        //        }
        //    }
        //}


        //string _description;
        ///// <summary>
        ///// 描述
        ///// </summary>
        ////[XmlElement("Description")]
        //public string Description
        //{
        //    get
        //    {
        //        return _description;
        //    }
        //    set
        //    {
        //        if (_description != value)
        //        {
        //            _description = value;
        //            base.OnPropertyChanged("Description", value);
        //        }
        //    }
        //}



        ReportParameterType _type;
        /// <summary>
        /// 类型
        /// </summary>
        //[XmlElement("ParameterType")]
        public ReportParameterType ParameterType
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
                    base.OnPropertyChanged("ParameterType", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 创建人
        /// </summary>
        [XmlIgnore]
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

        string _value;
        /// <summary>
        /// 创建人
        /// </summary>
        //[XmlElement("ParameterValue")]
        public string ParameterValue
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    base.OnPropertyChanged("ParameterValue", value);
                }
            }
        }



        string _createbyname;
        /// <summary>
        /// 创建人
        /// </summary>
        [XmlIgnore]
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
        [XmlIgnore]
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
        /// 行版本
        /// </summary>
        [XmlIgnore]
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


        public override bool Equals(object obj)
        {
            XMLReportParameterList newObj = obj as XMLReportParameterList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }

    /// <summary>
    /// 邮件消息日志集合
    /// <example>
    /// 
    /// </example>
    /// </summary>
    [Serializable()]
    [XmlType()]
    [XmlRoot(IsNullable = false, ElementName = "Reports")]
    public class ReportCollect
    {
        public ReportCollect()
        {
            Reports = new List<ReportItem>();
        }

        [XmlElement("Report")]
        public List<ReportItem> Reports { get; set; }
    }

    /// <summary>
    /// 邮件消息日志集合
    /// <example>
    /// 
    /// </example>
    /// </summary>
    [Serializable()]
    [XmlType()]
    [XmlRoot(IsNullable = false, ElementName = "Reports")]
    public class CompanyReportCollect
    {
        public CompanyReportCollect()
        {
            Companies = new List<CompanyReportItem>();
        }

        [XmlElement("Company")]
        public List<CompanyReportItem> Companies { get; set; }
    }

    public class CompanyReportItem
    {
        /// <summary>
        /// 公司ID 
        /// </summary>
        [XmlAttribute("ID")]
        public Guid ID { get; set; }

        [XmlElement("Report")]
        public List<ReportItem> Reports { get; set; }
    }

    /// <summary>
    /// 邮件消息日志集合
    /// <example>
    /// 
    /// </example>
    /// </summary>
    [Serializable()]
    [XmlType()]
    [XmlRoot(IsNullable = false, ElementName = "Parameters")]
    public class ReportParameterCollect
    {
        public ReportParameterCollect()
        {
            Parameters = new List<XMLReportParameterList>();
        }



        [XmlElement("Parameter")]
        public List<XMLReportParameterList> Parameters { get; set; }
    }

    /// <summary>
    /// 邮件消息日志集合
    /// <example>
    /// 
    /// </example>
    /// </summary>
    [Serializable()]
    [XmlType()]
    [XmlRoot(IsNullable = false, ElementName = "Report")]
    public class ReportItem
    {
        public ReportItem()
        {
            Parameters = new ReportParameterCollect();
        }

        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        [XmlAttribute("ID")]
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
                }
            }
        }


        public ReportParameterCollect Parameters { get; set; }
    }

    [Serializable()]
    [XmlType()]
    public class ReportCompanyItem
    {
        public ReportCompanyItem()
        {
            Parameters = new ReportParameterCollect();
        }

        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        [XmlAttribute("ID")]
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
                }
            }
        }


        public ReportParameterCollect Parameters { get; set; }
    }


    [Serializable]
    public partial class CompanyReportConfigureList : ReportConfigureList
    {
        Guid _configureID;

        /// <summary>
        /// 服务配置KeyID
        /// </summary>
        public Guid ConfigureID
        {
            get
            {
                return _configureID;
            }
            set
            {
                if (_configureID != value)
                {
                    _configureID = value;
                    base.OnPropertyChanged("ConfigureID", value);
                }
            }
        }

    }

    [Serializable]
    public partial class CompanyEDIConfigureList : EDIConfigureList
    {
        Guid _configureID;

        /// <summary>
        /// 服务配置KeyID
        /// </summary>
        public Guid ConfigureID
        {
            get
            {
                return _configureID;
            }
            set
            {
                if (_configureID != value)
                {
                    _configureID = value;
                    base.OnPropertyChanged("ConfigureID", value);
                }
            }
        }
    }

    [Serializable]
    public partial class ConfigureDefaultValueList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        Guid _solutionid;
        /// <summary>
        /// 解决方案ID
        /// </summary>
        public Guid SolutionID
        {
            get
            {
                return _solutionid;
            }
            set
            {
                if (_solutionid != value)
                {
                    _solutionid = value;
                    base.OnPropertyChanged("SolutionID", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 创建人

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


        Guid _defaultValueID;
        /// <summary>
        /// 默认值ID（对应公共客户信息）
        /// </summary>
        [GuidRequired(CMessage = "默认值", EMessage = "DefaultValue")]

        public Guid DefaultValueID
        {
            get
            {
                return _defaultValueID;
            }
            set
            {
                if (_defaultValueID != value)
                {
                    _defaultValueID = value;
                    base.OnPropertyChanged("DefaultValueID", value);
                }
            }
        }


        string _defaultValuename;
        /// <summary>
        /// 代理
        /// </summary>
        public string DefaultValueName
        {
            get
            {
                return _defaultValuename;
            }
            set
            {
                if (_defaultValuename != value)
                {
                    _defaultValuename = value;
                    base.OnPropertyChanged("DefaultValueName", value);
                }
            }
        }


        DefaultValueType _type;
        /// <summary>
        /// 有效
        /// </summary>
        public DefaultValueType Type
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


        DateTime? _updateDate;
        /// <summary>
        /// 行版本

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


        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaximumLength = 500, CMessage = "备注", EMessage = "Remark")]

        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                if (_remark != value)
                {
                    _remark = value;
                    base.OnPropertyChanged("Remark", value);
                }
            }
        }


        public override bool Equals(object obj)
        {
            ConfigureDefaultValueList newObj = obj as ConfigureDefaultValueList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }

    #endregion

    #region CostItemData

    /// <summary>
    /// 成本项目，工作流中的
    /// </summary>
    [Serializable]
    public class CostItemData
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string CName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EName { get; set; }

        /// <summary>
        /// 全名
        /// </summary>
        public string EFullName { get; set; }

        /// <summary>
        /// 全名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 父项
        /// </summary>
        public Guid? ParentID { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public string NodeCode { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        public Guid GroupID { get; set; }
    }

    #endregion

    #region AuthcodeInfo
    /// <summary>
    /// Mac地址管理
    /// </summary>
    [Serializable]
    public partial class AuthcodeInfo : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty || ID == GlobalConstants.NewRowID; } }

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


        string _authcode;
        /// <summary>
        /// Mac网卡地址
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

        string _physicalID;
        /// <summary>
        /// PhysicalID
        /// </summary>
        public string PhysicalID
        {
            get
            {
                return _physicalID;
            }
            set
            {
                if (_physicalID != value)
                {
                    _physicalID = value;
                    base.OnPropertyChanged("PhysicalID", value);
                }
            }
        }


        /// <summary>
        /// 创建人
        /// </summary>
        Guid _senderid;

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


        string _sendername;
        /// <summary>
        /// 创建人姓名
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
        /// 创建时间
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


        string _senderremark;
        /// <summary>
        /// 备注
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

        Guid _approvalid;
        /// <summary>
        /// 申请人ID
        /// </summary>
        public Guid ApprovalId
        {
            get
            {
                return _approvalid;
            }
            set
            {
                if (_approvalid != value)
                {
                    _approvalid = value;
                    base.OnPropertyChanged("ApprovalId", value);
                }
            }
        }


        string _approvalname;
        /// <summary>
        /// 申请人名称
        /// </summary>
        public string ApprovalName
        {
            get
            {
                return _approvalname;
            }
            set
            {
                if (_approvalname != value)
                {
                    _approvalname = value;
                    base.OnPropertyChanged("ApprovalName", value);
                }
            }
        }

        int _state;
        /// <summary>
        /// MAC状态
        /// </summary>
        public int State
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
    } 
    #endregion

    #region ConfigureListForEDI
    /// <summary>
    /// ConfigureListForEDI
    /// </summary>
    public class ConfigureListForEDI : ConfigureList
    {
        public bool IsSelected { get; set; }
        public Guid EDIConfigureID { get; set; }
        public string ToAddress { get; set; }
        public string CSCLWebURL { get; set; }
        public string CSCLLoginName { get; set; }
        public string CSCLPassword { get; set; }
        public string CompanyAddress { get; set; }
        public string BookingOfficeCode { get; set; }
    } 
    #endregion

    #region EDIDictCode
    [Serializable]
    public class EDIDictCodeList
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// AMS Code
        /// </summary>
        public string AMSCode { get; set; }
        /// <summary>
        /// AMS Name
        /// </summary>
        public string AMSName { get; set; }
        /// <summary>
        /// ACI Code
        /// </summary>
        public string ACICode { get; set; }
        /// <summary>
        /// ACI Name
        /// </summary>
        public string ISOCode { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
    #endregion

    #region
    public class GetTop5MenuOfCommandNameViaUserID
    {
        public string FunctionName { get; set; }
        public int Count { get; set; }
    }

    public class GetUserPasswordUpdate
    {
        public DateTime updatetime { get; set; }

    }

    #endregion

}
