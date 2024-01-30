using System;
using System.Collections.Generic;

namespace ICP.WF.ServiceInterface.DataObject
{
    /// <summary>
    /// 工作流配置项
    /// </summary>
    [Serializable]
    public class WorkFlowConfigInfo:ICP.Framework.CommonLibrary.Common.BaseDataObject
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public Guid CategoryID { get; set; }

        /// <summary>
        /// 分类名 
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 流程英文描述
        /// </summary>
        public string EDescription { get; set; }

        /// <summary>
        /// 流程中文描述
        /// </summary>
        public string CDescription { get; set; }

        /// <summary>
        /// 是否业务入口
        /// </summary>
        public bool IsOA { get; set; }

        /// <summary>
        /// 每个月提前N天停用
        /// </summary>
        public int Days{get;set;}

        /// <summary>
        /// 流程路径
        /// </summary>
        public string WorkFlowFileContent { get; set; }

        /// <summary>
        /// 规则文件内容
        /// </summary>
        public string RuleFileContent { get; set; }

        /// <summary>
        /// 子表ID
        /// </summary>
        public Guid ProFileID { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 流程英文描述
        /// </summary>
        public string EPrintTitle { get; set; }

        /// <summary>
        /// 流程中文描述
        /// </summary>
        public string CPrintTitle { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// 用户权限列表
        /// </summary>
        public List<WorkFlowConfigUserPermissionInfo> UserPermissions { get; set; }

        /// <summary>
        /// 职位权限列表
        /// </summary>
        public List<WorkFlowConfigJobPermissionInfo> JobPermissions { get; set; }

    }


     [Serializable]
    public class WorkFlowTypeCS
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
        /// 名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 父节点
        /// </summary>
        public Guid? ParentID
        {
            get;
            set;
        }

    }

    [Serializable]
    public partial class WorkFlowConfigPermissionInfo:ICP.Framework.CommonLibrary.Common.BaseDataObject
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
                if (_id != value)
                {
                    _id = value;
                    base.OnPropertyChanged("ID", value);
                }
            }
        }


        Guid _workflowConfigID;
        /// <summary>
        /// 流程配置ID
        /// </summary>
        public Guid WorkflowConfigID
        {
            get
            {
                return _workflowConfigID;
            }
            set
            {
                if (_workflowConfigID != value)
                {
                    _workflowConfigID = value;
                    base.OnPropertyChanged("WorkflowConfigID", value);
                }
            }
        }
        
    }

    [Serializable]
    public partial class WorkFlowConfigUserPermissionInfo : WorkFlowConfigPermissionInfo
    {


        Guid? _userid;
        /// <summary>
        /// 使用对象ID
        /// </summary>
        public Guid? UserID
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
        /// 使用对象
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


        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    }

    [Serializable]
    public partial class WorkFlowConfigJobPermissionInfo : WorkFlowConfigPermissionInfo
    {
        Guid? _organizationid;
        /// <summary>
        /// 
        /// </summary>
        public Guid? OrganizationID
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
        /// 组织架构
        /// </summary>
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


        Guid? _jobid;
        /// <summary>
        /// 使用对象ID
        /// </summary>
        public Guid? JobID
        {
            get
            {
                return _jobid;
            }
            set
            {
                if (_jobid != value)
                {
                    _jobid = value;
                    base.OnPropertyChanged("JobID", value);
                }
            }
        }


        string _jobName;
        /// <summary>
        /// 使用对象
        /// </summary>
        public string JobName
        {
            get
            {
                return _jobName;
            }
            set
            {
                if (_jobName != value)
                {
                    _jobName = value;
                    base.OnPropertyChanged("JobName", value);
                }
            }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    }

}
