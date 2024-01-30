using System;
using System.IO;
using ICP.Framework.CommonLibrary.Helper;
using System.Xml;

namespace ICP.WF.ServiceInterface.DataObject
{
    /// <summary>
    /// 流程数据对象
    /// </summary>
    [Serializable]
    public class WorkFlowData
    {
        /// <summary>
        /// 是否主表单
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// 流程ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 流程名
        /// </summary>
        public string WorkName { get; set; }

        /// <summary>
        /// 工作名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 凭证号
        /// </summary>
        public string VoucherNo { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 流程完成时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 流程申请人ID
        /// </summary>
        public Guid OwnerUserId { get; set; }

        /// <summary>
        /// 流程申请人名称
        /// </summary>
        public string OwnerUserName { get; set; }

        /// <summary>
        /// 申请部门ID
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// 申请部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 工作流状态
        /// </summary>
        public WorkflowState State { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string StateDescription
        {
            get
            {
                try
                {
                    return EnumHelper.GetDescription<WorkflowState>(State, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 当前环节工作项
        /// </summary>
        public Guid? CurrentWorkItemId { get; set; }


        /// <summary>
        /// 当前环节工作项
        /// </summary>
        public string CurrentWorkItemName { get; set; }


        /// <summary>
        /// 当前环节执行人ID
        /// </summary>
        public Guid? CurrentWorkItemExcutorID { get; set; }


        /// <summary>
        /// 当前环节执行人名称
        /// </summary>
        public string CurrentWorkItemExcutorName { get; set; }

        /// <summary>
        /// 当前环节执行状态
        /// </summary>
        public WorkItemState? CurrentWorkItemState { get; set; }


        /// <summary>
        /// 当前环节状态描述
        /// </summary>
        public string CurrentStateDescription
        {
            get
            {
                if (CurrentWorkItemState == null)
                {
                    return string.Empty;
                }
                else
                {
                    try
                    {
                        return EnumHelper.GetDescription<WorkItemState>((WorkItemState)this.CurrentWorkItemState, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                    }
                    catch
                    {
                        return string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// 任务状态(查询列表中用到)
        /// </summary>
        public WorkItemSearchStatus WorkItemState
        {
            get;
            set;
        }
        /// <summary>
        /// 任务状态描述
        /// </summary>
        public string WorkItemStateDescription
        {
            get
            {
                try
                {
                    return EnumHelper.GetDescription<WorkItemSearchStatus>((WorkItemSearchStatus)this.WorkItemState, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                }
                catch
                {
                    return string.Empty;
                }      
            }
        }

        /// <summary>
        /// 父类(任务工具用到)
        /// </summary>
        public Guid ParentID { get; set; }

        /// <summary>
        /// 当前对应的表单KeyWord
        /// </summary>
        public string ProfileKey { get; set; }

        /// <summary>
        /// 当前对应的表单内容
        /// </summary>
        public string FormSchema { get; set; }

        /// <summary>
        /// 当前表单关联的数据
        /// </summary>
        public string FormDataString { get; set; }

        /// <summary>
        /// 数据结构
        /// </summary>
        public string DataSchema { get; set; }

        /// <summary>
        /// 数据结构
        /// </summary>
        public string WebDataSchema { get; set; }

        System.Data.DataSet _data;
        /// <summary>
        /// 当前表单关联的数据
        /// </summary>
        public System.Data.DataSet FormData
        {
            get
            {
                if (_data == null
                    && !string.IsNullOrEmpty(FormDataString)
                    && !string.IsNullOrEmpty(DataSchema))
                {
                    _data = new System.Data.DataSet();
                    _data.EnforceConstraints = false;
                    using (TextReader tw = new StringReader(DataSchema))
                    {
                        _data.ReadXmlSchema(tw);
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.LoadXml(this.FormDataString);
                        XmlNode leaveDate = xmldoc.FirstChild.SelectSingleNode("//MainTable//LeaveDate");
                        if (leaveDate != null)
                            leaveDate.InnerText = Convert.ToDateTime(leaveDate.InnerText).ToLocalTime().ToString("s");
                        XmlNode to = xmldoc.FirstChild.SelectSingleNode("//MainTable//To");
                        if (to != null)
                            to.InnerText = Convert.ToDateTime(to.InnerText).ToLocalTime().ToString("s");
                        if (leaveDate != null && to != null)
                            this.FormDataString = xmldoc.InnerXml;
                        _data.ReadXml(new StringReader(this.FormDataString));
                        tw.Close();
                    }
                }

                return _data;
            }
            set
            {
                _data = value;
            }
        }

        /// <summary>
        /// 任务执行人
        /// </summary>
        public Guid? ExecutorID { get; set; }

        /// <summary>
        /// 任务执行人名
        /// </summary>
        public string ExecutorName { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string CName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EName { get; set; }

        /// <summary>
        /// 流程配置ID
        /// </summary>
        public Guid WorkFlowConfigID { get; set; }
    }

    
}
