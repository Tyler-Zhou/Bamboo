
//-----------------------------------------------------------------------
// <copyright file="IWorkFlowService.cs" company="ICP">
//     Copyright (c) ICP. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.WF.ServiceInterface.DataObject;
    using System.Data;
    using System.ServiceModel;

    /// <summary>
    /// 工作流管理接口
    /// </summary>
    [ServiceInfomation("工作流服务", ServiceType.Business)]
    [ServiceContract]
    public interface IWorkflowService
    {
        #region 发起流程--根据ID(内部调用)
        /// <summary>
        /// 发起流程
        /// </summary>
        /// <param name="workflowConfigID">流程配置ID</param>
        /// <param name="workName">工作名</param>
        /// <param name="data">对应申请表单的数据</param>
        /// <param name="startByID">发起人</param>
        /// <param name="startDepartmentId">流程申请部门</param>
        /// <param name="isValidateData">是否需要验证日期</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        WorkItemInfo StartWorkflowInternal(
            Guid workflowConfigID,
            string workName,
            bool isGenerateNo,
            DataSet data,
            Guid startByID,
            Guid startDepartmentId,
            bool isValidateData);
        #endregion

        #region 发起流程--根据Key(外部调用)
        /// <summary>
        /// 发起流程(客户端外部接口调用)
        /// </summary>
        /// <param name="key">流程Key</param>
        /// <param name="workName">流程名称</param>
        /// <param name="isGenerateNo">是否生成单号</param>
        /// <param name="data">数据</param>
        /// <param name="startByID">发起人</param>
        /// <param name="startDepartmentId">发起人部门</param>
        /// <returns></returns>
        [OperationContract]
        WorkItemInfo StartWorkflow(
            string key,
            string workName,
            DataSet data,
            Guid startByID,
            Guid startDepartmentId,
            bool isValidateData);
        #endregion

        #region 申请任务
        /// <summary>
        /// 申请任务
        /// </summary>
        /// <param name="workflowID">流程ID</param>
        /// <param name="workFlowKey">流程关键字</param>
        /// <param name="callId">发起人</param>
        /// <param name="callIdDepartment">发起人所在部门</param>
        /// <param name="workflowName">工作名</param>
        /// <param name="formFile">关联的表单文件</param>
        /// <param name="formCName">任务中文名</param>
        /// <param name="formEName">任务英文名</param>
        /// <param name="data">表单对应的数据源</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        Guid ApplyTask(
            Guid workflowID,
            string formFile,
            string formCName,
            string formEName,
            Guid applierID,
            System.Data.DataSet data,
            Guid[] participants);
        #endregion

        #region 保存任务信息
        /// <summary>
        /// 保存任务信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="workflowInstanceID">流程实例ID</param>
        /// <param name="name">工作名</param>
        /// <param name="isGenerateNo">生成单号</param>
        /// <param name="applicantID">申请人</param>
        /// <param name="applyDepartmentID">申请部门</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="finishDate">结束时间</param>
        /// <param name="state">状态</param>
        /// <param name="workflowConfigID">流程配置ID</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回</returns>
        [FunctionInfomation()]
        [OperationContract]
        SingleResult SaveWorkflowInfo(
            Guid? id,
            Guid workflowInstanceID,
            string name,
            bool isGenerateNo,
            Guid applicantID,
            Guid applyDepartmentID,
            DateTime? startDate,
            DateTime? finishDate,
            WorkflowState state,
            Guid workflowConfigID,
            Guid saveByID);

        #endregion

        #region 取消流程
        /// <summary>
        /// 取消流程
        /// </summary>
        /// <param name="workflowID">流程id</param>
        /// <param name="cancelByID">取消人</param>
        [FunctionInfomation()]
        [OperationContract]
        void CancelWorkflow(
            Guid workflowID,
            Guid cancelByID);
        #endregion

        #region 完成任务
        /// <summary>
        /// 完成任务
        /// </summary>
        /// <param name="workitemID">任务ID</param>
        /// <param name="finishByID">执行人</param>
        /// <param name="data">表单对应的数据</param>
        /// <param name="enqueueItem">书签信息</param>
        /// <returns>成功完成true,否则返回false.</returns>
        [FunctionInfomation()]
        [OperationContract]
        [FaultContract(typeof(WorkflowExecutorNullExceptionInfo))]
        [FaultContract(typeof(WorkflowSqlExceptionInfo))]
        bool FinishTask(
            Guid workitemID,
            Guid finishByID,
            System.Data.DataSet data,
            EnqueueItem enqueueItem);

        #endregion

        #region 获得任务的详细信息
        /// <summary>
        /// 获取任务的详细信息
        /// </summary>
        /// <param name="workitemID">任务ID</param>
        /// <returns>返回任务的详细信息</returns>
        [FunctionInfomation()]
        [OperationContract]
        WorkItemInfo GetWorkitemInfo(Guid workitemID);
        #endregion

        #region 获得流程所有的环节的简要数据(用于流程图查看)
        /// <summary>
        /// 获取流程所有环节的简要数据(用于流程图查看)
        /// </summary>
        /// <param name="workInfoID">工作流ID</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        FlowChartInfo GetFlowChartInfo(Guid workInfoID);
        #endregion

        #region 保存制定任务的数据
        /// <summary>
        /// 保存制定任务的数据
        /// </summary>
        /// <param name="workitemID">任务id</param>
        /// <param name="saveByID">修改人</param>
        /// <param name="data">任务数据</param>
        [FunctionInfomation()]
        [OperationContract]
        void SetWorkItemData(
            Guid workitemID,
            Guid saveByID,
            System.Data.DataSet data);
        #endregion

        #region 指派任务
        /// <summary>
        /// 指派任务
        /// </summary>
        /// <param name="workItemId">任务id </param>
        /// <param name="transmitByID">指派人 </param>
        /// <param name="participants">所指派的人列表</param>
        [FunctionInfomation()]
        [OperationContract]
        void TransmitWorkItem(
            Guid workItemId,
            Guid transmitByID,
            Guid[] participants);
        #endregion

        #region 获得流程列表
        /// <summary>
        /// 获取流程列表
        /// </summary>
        /// <param name="userID">当前用户</param>
        /// <param name="workflowName">流程名</param>
        /// <param name="workName">工作名</param>
        /// <param name="no">单号</param>
        /// <param name="applicantorName">申请人</param>
        /// <param name="type">查询类型</param>
        /// <param name="stateList">状态列表</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="endFromDate">开始时间</param>
        /// <param name="endToDate">开始时间</param>
        /// <param name="maxResult">最大记录数量</param>
        /// <returns>返回流程列表</returns>
        [FunctionInfomation()]
        [OperationContract]
        string GetWorkList(
            Guid userID,
            Guid? depID,
            string workflowName,
            string workName,
            string no,
            string applicantorName,
            WorkListSearchType? type,
            WorkItemSearchStatus[] stateList,
            DateTime? fromDate,
            DateTime? toDate,
            DateTime? endFromDate,
            DateTime? endToDate,
            int maxResult);
        #endregion

        #region 获得流程数据(外部接口时使用)
        /// <summary>
        /// 获得流程数据(外部接口时使用)
        /// </summary>
        /// <param name="workInfoID"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        WorkFlowData GetWorkFlowData(Guid workInfoID, Guid userID);
        #endregion

        #region 获得指定任务环节列表
        /// <summary>
        /// 获取指定流程任务环节列表()
        /// </summary>
        /// <param name="workflowID">流程id</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<WorkItemInfo> GetWorkItemDetailList(Guid workflowID);
        #endregion

        #region 获得所有的数据字典列表
        /// <summary>
        /// 获取指定流程所有数据的字典列表(与业务交互时候使用)
        /// </summary>
        /// <param name="workflowInstanceId"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        DataCollector GetDataCollect(Guid workflowInstanceId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workflowInstanceId"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        string GetDataCollectString(Guid workflowInstanceId);
        #endregion

        #region 更新主表中的字段
        /// <summary>
        /// 更新主表单里面对应字段的数据
        /// </summary>
        /// <param name="workflowInstanceId">工作流实例ID</param>
        /// <param name="filedNames"></param>
        /// <param name="values"></param>
        /// <param name="modes"></param>
        [FunctionInfomation()]
        [OperationContract]
        void SaveMainWorkItemData(
             Guid workflowInstanceId,
             string[] fieldNames,
             object[] fieldValues,
             RewriteMode[] modes,
             Guid saveByID);
        #endregion

        #region 新建任务
        /// <summary>
        /// 新建任务
        /// </summary>
        /// <param name="workflowInstanceID">流程ID</param>
        /// <param name="formCName">中文名</param>
        /// <param name="formEName">英文名</param>
        /// <param name="formFile">对应的表单</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="participants">签收人</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        Guid NewTask
            (Guid workflowInstanceID,
            string formFile,
            string formCName,
            string formEName,
            Guid applierID,
            System.Data.DataSet data,
            Guid[] participants);
        #endregion

        #region 获取流程的详细信息
        /// <summary>
        /// 获取流程详细信息
        /// </summary>
        /// <param name="id">流程ID</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        WorkInfosInfo GetWorkflowInfo(Guid id);
        #endregion

        #region 获取流程的信息--根据流程ID
        /// <summary>
        /// 获取流程信息
        /// </summary>
        /// <param name="workflowInstanceID">流程实例ID</param>
        /// <returns></returns>
        [OperationContract]
        WorkItemInfo GetMainWorkitemInfo(Guid workflowInstanceID);
        #endregion

        #region 获取流程的信息--根据用户ID
        /// <summary>
        /// 获取流程信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetWorkInfo(Guid userID, Guid id);

        #endregion

        #region 是否可以进行取消
        /// <summary>
        /// 是否可以进行取消操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsCancel(Guid id);
        #endregion

        #region 获得主表单的数据
        /// <summary>
        /// 获得主表单的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        WorkItemInfo GetMainItem(Guid id);
        #endregion

        #region 审核流程
        /// <summary>
        /// 审核流程
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="updateDate">更新日期</param>
        /// <param name="isMerger">是否需要合并凭证号</param>
        /// <param name="isCheck">True为审核，Fasel为取消审核</param>
        /// <param name="audirotBy">审核人</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        ManyResult AuditorWork(Guid[] ids, bool isMerger, bool isCheck, Guid audirotBy);
        #endregion

        #region 批量完成流程
        /// <summary>
        /// 批量完成流程
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="finishBy"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<CurrentWorkItem> MultiFinishWork(Guid[] ids, Guid finishBy);
        #endregion

        #region 删除流程的CostFee纪录
        [FunctionInfomation()]
        [OperationContract]
        void RemoveCostFee(Guid workflowID);
        #endregion

    }
}
