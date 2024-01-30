using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common; 
using System.Xml;
using System.Runtime.Serialization;

namespace ICP.WF.ServiceInterface.DataObject
{

    #region 表单配置器
    /// <summary>
    /// 表单配置器列表
    /// </summary>
    [Serializable]
    public partial class FormProfilesList : BaseDataObject
    {

        Guid _id;
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                base.OnPropertyChanged("ID", value);
            }
        }

        string _cname;
        /// <summary>
        /// 中文名称
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "中文名称", EMessage = "CName")]
        [Required(CMessage="中文名称",EMessage="CName")]
        public string CName
        {
            get
            {
                return _cname;
            }
            set
            {
                _cname = value;
                base.OnPropertyChanged("CName", value);
            }
        }

        string _ename;
        /// <summary>
        /// 英文名称
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "英文名称", EMessage = "EName")]
        [Required(CMessage = "英文名称", EMessage = "EName")]
        public string EName
        {
            get
            {
                return _ename;
            }
            set
            {
                _ename = value;
                base.OnPropertyChanged("EName", value);
            }
        }

        XmlDocument _formdata;
        /// <summary>
        /// 表单数据
        /// </summary>
        [Required(CMessage = "表单数据", EMessage = "FormData")]
        public XmlDocument FormData
        {
            get
            {
                return _formdata;
            }
            set
            {
                _formdata = value;
                base.OnPropertyChanged("FormData", value);
            }
        }

        XmlDocument _datascheme;
        /// <summary>
        /// 表单数据架构
        /// </summary>
        [Required(CMessage = "数据架构", EMessage = "DataScheme")]
        public XmlDocument DataScheme
        {
            get
            {
                return _datascheme;
            }
            set
            {
                _datascheme = value;
                base.OnPropertyChanged("DataScheme", value);
            }
        }

        string _version;
        /// <summary>
        /// 版本
        /// </summary>
        [StringLength(MaximumLength = 10, CMessage = "版本号", EMessage = "Version")]
        [Required(CMessage = "版本号", EMessage = "Version")]
        public string Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
                base.OnPropertyChanged("Version", value);
            }
        }

        Guid _createby;
        /// <summary>
        /// 建立人
        /// </summary>
        [Required(CMessage = "建立人", EMessage = "CreateBy")]
        public Guid CreateBy
        {
            get
            {
                return _createby;
            }
            set
            {
                _createby = value;
                base.OnPropertyChanged("CreateBy", value);
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
                _createdate = value;
                base.OnPropertyChanged("CreateDate", value);
            }
        }

        Guid? _updateby;
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid? UpdateBy
        {
            get
            {
                return _updateby;
            }
            set
            {
                _updateby = value;
                base.OnPropertyChanged("UpdateBy", value);
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
                _updatedate = value;
                base.OnPropertyChanged("UpdateDate", value);
            }
        }

    }

    /// <summary>
    /// 表单配置器信息
    /// </summary>
    [Serializable]
    public partial class FormProfilesInfo : FormProfilesList
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
    /// <summary>
    /// 获取指定流程所有数据的字典列表实体类
    /// </summary>
    [Serializable]
    [KnownType(typeof(object[]))]
    public class DataCollector
    {
        public Dictionary<string, object> DataCollect
        {
            get;
            set;
        }

    }
    #endregion

    #region 流程配置器
    /// <summary>
    /// 流程配置器列表
    /// </summary>
    [Serializable]
    public partial class WorkFlowConfigsList : BaseDataObject
    {

        Guid _id;
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                base.OnPropertyChanged("ID", value);
            }
        }

        string _cdescription;
        /// <summary>
        /// 中文描述
        /// </summary>
        [StringLength(MaximumLength = 500, CMessage = "中文描述", EMessage = "CDescription")]
        [Required(CMessage = "中文描述", EMessage = "CDescription")]
        public string CDescription
        {
            get
            {
                return _cdescription;
            }
            set
            {
                if (_cdescription != value)
                {
                    _cdescription = value;
                    base.OnPropertyChanged("CDescription", value);
                }
            }
        }

        string _edescription;
        /// <summary>
        /// 英文描述
        /// </summary>
        [StringLength(MaximumLength = 500, CMessage = "英文描述", EMessage = "EDescription")]
        [Required(CMessage = "英文描述", EMessage = "EDescription")]
        public string EDescription
        {
            get
            {
                return _edescription;
            }
            set
            {
                _edescription = value;
                base.OnPropertyChanged("EDescription", value);
            }
        }

        string _cprinttitle;
        /// <summary>
        /// 中文打印标题
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "中文打印标题", EMessage = "CPrintTitle")]
        [Required(CMessage = "中文打印标题", EMessage = "CPrintTitle")]
        public string CPrintTitle
        {
            get
            {
                return _cprinttitle;
            }
            set
            {
                _cprinttitle = value;
                base.OnPropertyChanged("CPrintTitle", value);
            }
        }

        string _eprinttitle;
        /// <summary>
        /// 英文打印标题
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "英文打印标题", EMessage = "EPrintTitle")]
        [Required(CMessage = "英文打印标题", EMessage = "EPrintTitle")]
        public string EPrintTitle
        {
            get
            {
                return _eprinttitle;
            }
            set
            {
                _eprinttitle = value;
                base.OnPropertyChanged("EPrintTitle", value);
            }
        }

        bool _isoa;
        /// <summary>
        /// 是否办公流程
        /// </summary>
        [Required(CMessage = "是否办公流程", EMessage = "IsOA")]
        public bool IsOA
        {
            get
            {
                return _isoa;
            }
            set
            {
                _isoa = value;
                base.OnPropertyChanged("IsOA", value);
            }
        }

        byte _days;
        /// <summary>
        /// 有效天数
        /// </summary>
        [Required(CMessage = "有效天数", EMessage = "Days")]
        public byte Days
        {
            get
            {
                return _days;
            }
            set
            {
                _days = value;
                base.OnPropertyChanged("Days", value);
            }
        }

        string _keyword;
        /// <summary>
        /// 关键代码
        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "关键代码", EMessage = "KeyWord")]
        [Required(CMessage = "关键代码", EMessage = "KeyWord")]
        public string KeyWord
        {
            get
            {
                return _keyword;
            }
            set
            {
                _keyword = value;
                base.OnPropertyChanged("KeyWord", value);
            }
        }

        Guid _createby;
        /// <summary>
        /// 建立人
        /// </summary>
        [Required(CMessage = "创建人", EMessage = "CreateBy")]
        public Guid CreateBy
        {
            get
            {
                return _createby;
            }
            set
            {
                _createby = value;
                base.OnPropertyChanged("CreateBy", value);
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
                _createdate = value;
                base.OnPropertyChanged("CreateDate", value);
            }
        }

        Guid? _updateby;
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid? UpdateBy
        {
            get
            {
                return _updateby;
            }
            set
            {
                _updateby = value;
                base.OnPropertyChanged("UpdateBy", value);
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
                _updatedate = value;
                base.OnPropertyChanged("UpdateDate", value);
            }
        }

    }

    /// <summary>
    /// 流程配置器详细信息
    /// </summary>
    [Serializable]
    public partial class WorkFlowConfigsInfo : WorkFlowConfigsList
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

    #endregion

    #region 流程权限 

    /// <summary>
    /// 流程访问权限信息
    /// </summary>
    [Serializable]
    public partial class WorkFlowPermissionsList : BaseDataObject
    {
    
        Guid  _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid  ID 
        {
            get
            {
                return _id ;
            }
            set
            {
                _id = value;
                base.OnPropertyChanged("ID", value);
            }
        }
    
        Guid  _workflowconfigid;
        /// <summary>
        /// 流程配置ID
        /// </summary>
        [Required(CMessage = "流程配置ID", EMessage = "WorkFlowConfigID")]
        public Guid  WorkFlowConfigID 
        {
            get
            {
                return _workflowconfigid ;
            }
            set
            {
                _workflowconfigid = value;
                base.OnPropertyChanged("WorkFlowConfigID", value);
            }
        }
    
        Guid?  _organizationid;
        /// <summary>
        /// 组织架构ID
        /// </summary>
        public Guid?  OrganizationID 
        {
            get
            {
                return _organizationid ;
            }
            set
            {
                _organizationid = value;
                base.OnPropertyChanged("OrganizationID", value);
            }
        }
    
        Guid?  _useobjectid;
        /// <summary>
        /// 使用对象ID（对应岗位或用户）
        /// </summary>
        public Guid?  UseObjectID 
        {
            get
            {
                return _useobjectid ;
            }
            set
            {
                _useobjectid = value;
                base.OnPropertyChanged("UseObjectID", value);
            }
        }
    
        byte?  _userobjecttype;
        /// <summary>
        /// 使用对象类型（1:岗位,2:用户）
        /// </summary>
        public byte?  UserObjectType 
        {
            get
            {
                return _userobjecttype ;
            }
            set
            {
                _userobjecttype = value;
                base.OnPropertyChanged("UserObjectType", value);
            }
        }
    
        Guid  _createby;
        /// <summary>
        /// 建立人
        /// </summary>
        [Required(CMessage = "建立人", EMessage = "CreateBy")]
        public Guid  CreateBy 
        {
            get
            {
                return _createby ;
            }
            set
            {
                _createby = value;
                base.OnPropertyChanged("CreateBy", value);
            }
        }
    
        DateTime  _createdate;
        /// <summary>
        /// 建立时间
        /// </summary>
        [Required(CMessage = "建立时间", EMessage = "CreateDate")]
        public DateTime  CreateDate 
        {
            get
            {
                return _createdate ;
            }
            set
            {
                _createdate = value;
                base.OnPropertyChanged("CreateDate", value);
            }
        }
    
        Guid?  _updateby;
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid?  UpdateBy 
        {
            get
            {
                return _updateby ;
            }
            set
            {
                _updateby = value;
                base.OnPropertyChanged("UpdateBy", value);
            }
        }
    
        DateTime?  _updatedate;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime?  UpdateDate 
        {
            get
            {
                return _updatedate ;
            }
            set
            {
                _updatedate = value;
                base.OnPropertyChanged("UpdateDate", value);
            }
        }
    
    }

    /// <summary>
    /// 流程访问权限信息
    /// </summary>
    [Serializable]
    public partial class WorkFlowPermissionsInfo : WorkFlowPermissionsList
    {

    }
    #endregion

    #region 流程子表

    /// <summary>
    /// 流程概况
    /// </summary>
    [Serializable]
    public partial class WorkFlowProfilesList : BaseDataObject
    {
    
        Guid  _id;
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid  ID 
        {
            get
            {
                return _id ;
            }
            set
            {
                _id = value;
                base.OnPropertyChanged("ID", value);
            }
        }
    
        Guid  _workflowconfigsid;
        /// <summary>
        /// 流程配置ID
        /// </summary>
        [Required(CMessage = "流程配置ID", EMessage = "WorkFlowConfigsID")]
        public Guid  WorkFlowConfigsID 
        {
            get
            {
                return _workflowconfigsid ;
            }
            set
            {
                _workflowconfigsid = value;
                base.OnPropertyChanged("WorkFlowConfigsID", value);
            }
        }
    
        string  _cname;
        /// <summary>
        /// 中文名称
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "中文名称", EMessage = "CName")]
        [Required(CMessage = "中文名称", EMessage = "CName")]
        public string  CName 
        {
            get
            {
                return _cname ;
            }
            set
            {
                _cname = value;
                base.OnPropertyChanged("CName", value);
            }
        }
    
        string  _ename;
        /// <summary>
        /// 英文名称
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "英文名称", EMessage = "EName")]
        [Required(CMessage = "英文名称", EMessage = "EName")]
        public string  EName 
        {
            get
            {
                return _ename ;
            }
            set
            {
                _ename = value;
                base.OnPropertyChanged("EName", value);
            }
        }
    
        XmlDocument  _workflowdata;
        /// <summary>
        /// 流程定义数据
        /// </summary>
        [Required(CMessage = "流程定义数据", EMessage = "WorkFlowData")]
        public XmlDocument  WorkFlowData 
        {
            get
            {
                return _workflowdata ;
            }
            set
            {
                _workflowdata = value;
                base.OnPropertyChanged("WorkFlowData", value);
            }
        }
    
        XmlDocument  _ruledata;
        /// <summary>
        /// 规则数据
        /// </summary>
        [Required(CMessage = "规则数据", EMessage = "RuleData")]
        public XmlDocument  RuleData 
        {
            get
            {
                return _ruledata ;
            }
            set
            {
                _ruledata = value;
                base.OnPropertyChanged("RuleData", value);
            }
        }
    
        string  _version;
        /// <summary>
        /// 版本
        /// </summary>
        [StringLength(MaximumLength = 10, CMessage = "版本号", EMessage = "Version")]
        [Required(CMessage = "版本号", EMessage = "DataScheme")]
        public string  Version 
        {
            get
            {
                return _version ;
            }
            set
            {
                _version = value;
                base.OnPropertyChanged("Version", value);
            }
        }
    
        Guid  _createby;
        /// <summary>
        /// 建立人
        /// </summary>
        [Required(CMessage = "建立人", EMessage = "CreateBy")]
        public Guid  CreateBy 
        {
            get
            {
                return _createby ;
            }
            set
            {
                _createby = value;
                base.OnPropertyChanged("CreateBy", value);
            }
        }
    
        DateTime  _createdate;
        /// <summary>
        /// 建立时间
        /// </summary>
        [Required(CMessage = "建立时间", EMessage = "CreateDate")]
        public DateTime  CreateDate 
        {
            get
            {
                return _createdate ;
            }
            set
            {
                _createdate = value;
                base.OnPropertyChanged("CreateDate", value);
            }
        }
    
        Guid?  _updateby;
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid?  UpdateBy 
        {
            get
            {
                return _updateby ;
            }
            set
            {
                _updateby = value;
                base.OnPropertyChanged("UpdateBy", value);
            }
        }
    
        DateTime?  _updatedate;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime?  UpdateDate 
        {
            get
            {
                return _updatedate ;
            }
            set
            {
                _updatedate = value;
                base.OnPropertyChanged("UpdateDate", value);
            }
        }
    
    }

    /// <summary>
    /// 流程概况详细信息
    /// </summary>
    [Serializable]
    public partial class WorkFlowProfilesInfo : WorkFlowProfilesList
    {

    }
    #endregion

    #region 流程信息
    /// <summary>
    /// 流程信息列表
    /// </summary>
    [Serializable]
    public partial class WorkInfosList : BaseDataObject
    {
    
        Guid  _id;
        /// <summary>
        /// 主键ID
        /// </summary>

        public Guid  ID 
        {
            get
            {
                return _id ;
            }
            set
            {
                _id = value;
                base.OnPropertyChanged("ID", value);
            }
        }
    
        string  _no;
        /// <summary>
        /// 单号
        /// </summary>
        [StringLength(MaximumLength = 50, CMessage = "单号", EMessage = "No")]
        [Required(CMessage = "单号", EMessage = "No")]
        public string  No 
        {
            get
            {
                return _no ;
            }
            set
            {
                _no = value;
                base.OnPropertyChanged("No", value);
            }
        }
    
        string  _name;
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(MaximumLength = 250, CMessage = "名称", EMessage = "Name")]
        [Required(CMessage = "名称", EMessage = "Name")]
        public string  Name 
        {
            get
            {
                return _name ;
            }
            set
            {
                _name = value;
                base.OnPropertyChanged("Name", value);
            }
        }
    
        Guid  _applicantid;
        /// <summary>
        /// 申请人ID
        /// </summary>
        [Required(CMessage = "申请人ID", EMessage = "ApplicantID")]
        public Guid  ApplicantID 
        {
            get
            {
                return _applicantid ;
            }
            set
            {
                _applicantid = value;
                base.OnPropertyChanged("ApplicantID", value);
            }
        }
        /// <summary>
        /// 申请人名
        /// </summary>
        public string ApplicantName { get; set; }

    
        Guid  _applydepartmentid;
        /// <summary>
        /// 申请部门ID
        /// </summary>
        [Required(CMessage = "申请部门ID", EMessage = "ApplicantDepartmentID")]
        public Guid  ApplicantDepartmentID 
        {
            get
            {
                return _applydepartmentid ;
            }
            set
            {
                _applydepartmentid = value;
                base.OnPropertyChanged("ApplicantDepartmentID", value);
            }
        }

        public string ApplicantDepartmentName { get; set; }
    
        DateTime  _startdate;
        /// <summary>
        /// 开始时间
        /// </summary>
        [Required(CMessage = "开始时间", EMessage = "StartDate")]
        public DateTime  StartDate 
        {
            get
            {
                return _startdate ;
            }
            set
            {
                _startdate = value;
                base.OnPropertyChanged("StartDate", value);
            }
        }
    
        DateTime?  _finishdate;
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime?  FinishDate 
        {
            get
            {
                return _finishdate ;
            }
            set
            {
                _finishdate = value;
                base.OnPropertyChanged("FinishDate", value);
            }
        }


        WorkflowState _state = WorkflowState.None;
        /// <summary>
        /// 状态(ACTIVATED = 1,NOTPASS = 2, CANCEL = 3,COMPLETED = 4)
        /// </summary>
        public WorkflowState State 
        {
            get
            {
                return _state ;
            }
            set
            {
                _state = value;
                base.OnPropertyChanged("State", value);
            }
        }
    
        Guid  _workflowconfigid;
        /// <summary>
        /// 流程配置ID
        /// </summary>
        [Required(CMessage = "流程配置ID", EMessage = "WorkFlowConfigID")]
        public Guid  WorkFlowConfigID 
        {
            get
            {
                return _workflowconfigid ;
            }
            set
            {
                _workflowconfigid = value;
                base.OnPropertyChanged("WorkFlowConfigID", value);
            }
        }
        /// <summary>
        /// 流程配置关键字
        /// </summary>
        public string WorkFlowConfigKey { get; set; }
        /// <summary>
        /// 流程实例ID
        /// </summary>
        public Guid WorkflowInstanceID { get; set; }
    
        Guid  _mainworkinfoid;
        /// <summary>
        /// 主流程ID
        /// </summary>
        [Required(CMessage = "主流程ID", EMessage = "MainWorkInfoID")]
        public Guid  MainWorkInfoID 
        {
            get
            {
                return _mainworkinfoid ;
            }
            set
            {
                _mainworkinfoid = value;
                base.OnPropertyChanged("MainWorkInfoID", value);
            }
        }
    
    }

    /// <summary>
    /// 流程详细信息
    /// </summary>
    [Serializable]
    public partial class WorkInfosInfo : WorkInfosList
    {
    
    }

    #endregion

    #region 流程参与者信息
    /// <summary>
    /// 流程参与者
    /// </summary>
    [Serializable]
    public partial class WorkItemParticipantsList : BaseDataObject
    {
    
        Guid  _id;
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid  ID 
        {
            get
            {
                return _id ;
            }
            set
            {
                _id = value;
                base.OnPropertyChanged("ID", value);
            }
        }
    
        Guid  _workitemid;
        /// <summary>
        /// 工作任务ID
        /// </summary>
        [Required(CMessage = "工作任务ID", EMessage = "WorkItemID")]
        public Guid  WorkItemID 
        {
            get
            {
                return _workitemid ;
            }
            set
            {
                _workitemid = value;
                base.OnPropertyChanged("WorkItemID", value);
            }
        }
    
        Guid  _participantid;
        /// <summary>
        /// 参与者ID
        /// </summary>
        [Required(CMessage = "参与者ID", EMessage = "ParticipantID")]
        public Guid  ParticipantID 
        {
            get
            {
                return _participantid ;
            }
            set
            {
                _participantid = value;
                base.OnPropertyChanged("ParticipantID", value);
            }
        }
    
        bool  _isexecutor;
        /// <summary>
        /// 是否执行者
        /// </summary>
        [Required(CMessage = "是否执行者", EMessage = "IsExecutor")]
        public bool  IsExecutor 
        {
            get
            {
                return _isexecutor ;
            }
            set
            {
                _isexecutor = value;
                base.OnPropertyChanged("IsExecutor", value);
            }
        }
    
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class WorkItemParticipantsInfo : WorkItemParticipantsList
    {

    }
    #endregion

    #region 任务列表
    /// <summary>
    /// 任务列表
    /// </summary>
    [Serializable]
    public partial class WorkItemsList : BaseDataObject
    {
    
        Guid  _id;
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid  ID 
        {
            get
            {
                return _id ;
            }
            set
            {
                _id = value;
                base.OnPropertyChanged("ID", value);
            }
        }
    
        Guid  _workinfoid;
        /// <summary>
        /// 流程ID
        /// </summary>
        /// </summary>
        [Required(CMessage = "流程ID", EMessage = "WorkInfoID")]
        public Guid  WorkInfoID 
        {
            get
            {
                return _workinfoid ;
            }
            set
            {
                _workinfoid = value;
                base.OnPropertyChanged("WorkInfoID", value);
            }
        }
    
        string  _cname;
        /// <summary>
        /// 中文名称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "中文名称", EMessage = "CName")]
        [Required(CMessage = "中文名称", EMessage = "CName")]
        public string  CName 
        {
            get
            {
                return _cname ;
            }
            set
            {
                _cname = value;
                base.OnPropertyChanged("CName", value);
            }
        }
    
        string  _ename;
        /// <summary>
        /// 英文名称
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "英文名称", EMessage = "EName")]
        [Required(CMessage = "英文名称", EMessage = "EName")]
        public string  EName 
        {
            get
            {
                return _ename ;
            }
            set
            {
                _ename = value;
                base.OnPropertyChanged("EName", value);
            }
        }
    
        Guid?  _executorid;
        /// <summary>
        /// 执行者ID
        /// </summary>
        public Guid?  ExecutorID 
        {
            get
            {
                return _executorid ;
            }
            set
            {
                _executorid = value;
                base.OnPropertyChanged("ExecutorID", value);
            }
        }
    
        DateTime  _startdate;
        /// <summary>
        /// 开始时间
        /// </summary>
        [Required(CMessage = "开始时间", EMessage = "StartDate")]
        public DateTime  StartDate 
        {
            get
            {
                return _startdate ;
            }
            set
            {
                _startdate = value;
                base.OnPropertyChanged("StartDate", value);
            }
        }
    
        DateTime?  _finishdate;
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime?  FinishDate 
        {
            get
            {
                return _finishdate ;
            }
            set
            {
                _finishdate = value;
                base.OnPropertyChanged("FinishDate", value);
            }
        }
    
        byte  _state=0;
        /// <summary>
        /// 状态( Waiting=1, Processing=2,Finished,=3)
        /// </summary>
        public byte  State 
        {
            get
            {
                return _state ;
            }
            set
            {
                _state = value;
                base.OnPropertyChanged("State", value);
            }
        }
    
        Guid  _formprofileid;
        /// <summary>
        /// 表单定义ID
        /// </summary>
        [Required(CMessage = "表单ID", EMessage = "FormProfileID")]
        public Guid  FormProfileID 
        {
            get
            {
                return _formprofileid ;
            }
            set
            {
                _formprofileid = value;
                base.OnPropertyChanged("FormProfileID", value);
            }
        }
    
        XmlDocument  _datacontent;
        /// <summary>
        /// 表单内容
        /// </summary>
        public XmlDocument  DataContent 
        {
            get
            {
                return _datacontent ;
            }
            set
            {
                _datacontent = value;
                base.OnPropertyChanged("DataContent", value);
            }
        }
    
        bool  _ismain;
        /// <summary>
        /// 是否主表单
        /// </summary>
        [Required(CMessage = "是否主表单", EMessage = "IsMain")]
        public bool  IsMain 
        {
            get
            {
                return _ismain ;
            }
            set
            {
                _ismain = value;
                base.OnPropertyChanged("IsMain", value);
            }
        }
    
        Guid?  _assigneeid;
        /// <summary>
        /// 指派人ID
        /// </summary>
        public Guid?  AssigneeID 
        {
            get
            {
                return _assigneeid ;
            }
            set
            {
                _assigneeid = value;
                base.OnPropertyChanged("AssigneeID", value);
            }
        }
    
    }

    /// <summary>
    /// -
    /// </summary>
    [Serializable]
    public partial class WorkItemsInfo : WorkItemsList
    {

    }

    #endregion


    [Serializable]
    public class ReturnWorlEmailInfo
    {
        /// <summary>
        /// 申请人
        /// </summary>
        public Guid ApplyID
        {
            get;
            set;
        }
        /// <summary>
        /// 执行人ID
        /// </summary>
        public Guid ExecutorID
        {
            get;
            set;
        }
        /// <summary>
        /// 单号
        /// </summary>
        public string No
        {
            get;
            set;
        }
        /// <summary>
        /// 新单号
        /// </summary>
        public string NewNo
        {
            get;
            set;
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 打回人
        /// </summary>
        public Guid ReturnUserID
        {
            get;
            set;
        }
        /// <summary>
        /// 新单号
        /// </summary>
        public string WorkName
        {
            get;
            set;
        }
        /// <summary>
        /// 打回原因
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 业务信息
    /// </summary>
    [Serializable]
    public class OperationSearchResult
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo { get; set; }

        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationId { get; set; }
    }

}
