//-----------------------------------------------------------------------
// <copyright file="WorkItemList.cs" company="ICP">
//     Copyright (c) ICP. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.ServiceInterface.DataObject
{
    using System;
    using System.IO;
using System.Collections.Generic;
    using System.Xml;


    /// <summary>
    /// 任务列表数据对象
    /// </summary>
    [Serializable]
    public class WorkItemList
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 流程ID
        /// </summary>
        public Guid WorkFlowID { get; set; }

        /// <summary>
        /// 工作单号
        /// </summary>
        public string WorkNo { get; set; }

        /// <summary>
        /// 工作名
        /// </summary>
        public string WorkName { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        public string WorkFlowName { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public WorkItemState State { get; set; }

        /// <summary>
        /// 流程发起人ID
        /// </summary>
        public Guid OwnerUserID { get; set; }

        /// <summary>
        /// 流程发起人名称
        /// </summary>
        public string OwnerUserName { get; set; }

        /// <summary>
        /// 流程发起部门
        /// </summary>
        public Guid OrganizationID { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 任务完成时间
        /// </summary>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 任务执行人
        /// </summary>
        public Guid? ExecutorID { get; set; }

        /// <summary>
        /// 任务执行人名
        /// </summary>
        public string ExecutorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
     
        /// <summary>
        /// 流程实例ID
        /// </summary>
        public Guid WorkflowInstanceID
        {
            get;
            set;
        }
       
        /// <summary>
        /// 流程配置ID
        /// </summary>
        public Guid WorkflowConfigID
        {
            get;
            set;
        }

        /// <summary>
        /// 流程状态
        /// </summary>
        public WorkflowState WorkState
        {
            get;
            set;
        }
        /// <summary>
        /// 流程打印标题
        /// </summary>
        public string WorkFlowPrintTitle
        {
            get;
            set;
        }

        /// <summary>
        /// WorkItem 集合
        /// </summary>
        public List<WorkFlowData> WorkFlowDataList
        {
            get;
            set;
        }
        /// <summary>
        /// 执行人列表
        /// </summary>
        public List<WorkItemParticipantsList> workItemParticipantsList
        {
            get;
            set;
        }
    }


    /// <summary>
    /// 任务明细数据
    /// </summary>
    [Serializable]
    public class WorkItemInfo : WorkItemList
    {
        /// <summary>
        /// 是否为主表单
        /// </summary>
        public bool IsMain
        {
            get;
            set;
        }
        /// <summary>
        /// 表单ID
        /// </summary>
        public Guid FormID
        {
            get;
            set;
        }

        /// <summary>
        /// 当前对应的表单keyword
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
        /// 移动端数据结构
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
                if (_data==null&& 
                    !string.IsNullOrEmpty(FormDataString)&&
                    !string.IsNullOrEmpty(DataSchema))
                {
                    _data = new System.Data.DataSet();
                    _data.EnforceConstraints = false;
                    using (StringReader tw = new StringReader(DataSchema))
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

    }


    [Serializable]
    public class CurrentWorkItem
    {
        /// <summary>
        /// 流程ID
        /// </summary>
        public Guid WorkInfoID { get; set; }
        /// <summary>
        /// 申请部门
        /// </summary>
        public Guid ApplyDepartmentID { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 当前步的ID
        /// </summary>
        public Guid CurrentWorkItemID { get; set; }
        /// <summary>
        /// 当前步的表单
        /// </summary>
        public Guid CurrentWorkItemFormID { get; set; }

        public WorkflowState WorkflowState { get; set; }
        public WorkItemSearchStatus WorkflowItemState { get; set; }
        public DateTime? FinishDate { get; set; }
        public string CurrentWorkItemName { get; set; }

        public List<Guid> UserList { get; set; }
    }
    [Serializable]
    public class WorkItemExecutorIDList
    {
        public Guid WorkItemID { get; set; }
        public Guid UserID { get; set; }
    }

    [Serializable]
    public class CurrentWorkItemList
    {
        public List<CurrentWorkItem> DataList { get; set; }
        public List<WorkItemExecutorIDList> UserList { get; set; }
    }
}
