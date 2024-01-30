using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.WF.ServiceInterface.DataObject;
using System.ServiceModel;
using ICP.FAM.ServiceInterface.DataObjects.Report;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.WF.ServiceInterface
{
    /// <summary>
    /// 工作流配置服务接口
    /// </summary>
    [ServiceInfomation("工作业务数据服务接口", ServiceType.Business)]
    [ServiceContract]
    public interface IWorkFlowDataService
    {
        #region 插入报销费用信息
        /// <summary>
        /// 插入报销费用信息
        /// </summary>
        /// <param name="workFlowID">流程ID</param>
        /// <param name="happenDate">费用日期</param>
        /// <param name="deptID">部门ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="bankOrglID">银行帐号 会计科目ID(isPay=1时为银行帐号;isPay=0时为会计科目ID)</param>
        /// <param name="accountingID">会计ID</param>
        /// <param name="generalManagerID">总经理ID</param>
        /// <param name="cashierID">出纳ID</param>
        /// <param name="financeManagerID">财务经理ID<ID/param>
        ///  <param name="isPay">是否需要支付</param>
        /// <param name="receiptQty">附据数量</param>
        /// <param name="amount">金额</param>
        /// <param name="feeProperty">报销性质</param>
        /// <param name="no">单号</param>
        /// <param name="costItemIDs">费用集合</param>
        /// <param name="currencys">币种集合</param>
        /// <param name="amounts">金额集合</param>
        /// <param name="remarks">备注集合</param>
        /// <param name="glID">会计科目ID</param>
        /// <param name="departmentID">部门核算ID</param>
        /// <param name="personalID">个人往来ID</param>
        /// <param name="customerID">客户往来ID</param>  
        /// <returns></returns>
        [FunctionInfomation("SaveCostFee")]
        [OperationContract]
        Guid SaveFee(Guid workFlowID,
                     DateTime happenDate,
                     Guid deptID,
                     Guid userID,
                     Guid bankOrglID,
                     Guid accountingID,
                     Guid generalManagerID,
                     Guid cashierID,
                     Guid financeManagerID,
                     bool isPay,
                     decimal? receiptQty,
                     decimal amount,
                     short feeProperty,
                     string no,
                     Guid[] costItemIDs,
                     string[] currencys,
                     decimal[] amounts,
                     string[] remarks,
                     Guid? glID,
                     Guid? departmentID,
                     Guid? personalID,
                     Guid? customerID);

        #endregion


        /// <summary>
        /// 插入报销费用信息(不需要支付)
        /// </summary>
        /// <param name="workFlowID">流程ID</param>
        /// <param name="happenDate">费用日期</param>
        /// <param name="deptID">部门ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="deptIDs">部门ID集合</param>
        /// <param name="userIDs">用户ID集合</param>
        /// <param name="customerIDs">客户ID集合</param>        
        /// <param name="glIDs">会计科目ID集合(isPay=1时为银行帐号;isPay=0时为会计科目ID)</param>
        /// <param name="accountingID">会计ID</param>
        /// <param name="generalManagerID">总经理ID</param>
        /// <param name="cashierID">出纳ID</param>
        /// <param name="financeManagerID">财务经理ID<ID/param>
        ///  <param name="isPay">是否需要支付</param>
        /// <param name="receiptQty">附据数量</param>
        /// <param name="amount">金额</param>
        /// <param name="feeProperty">报销性质</param>
        /// <param name="no">单号</param>
        /// <param name="costItemIDs">费用集合</param>
        /// <param name="currencys">币种集合</param>
        /// <param name="amounts">金额集合</param>
        /// <param name="remarks">备注集合</param>
        /// <returns></returns>
        [FunctionInfomation("SaveCostFee_DoNotPay")]
        [OperationContract]
        Guid SaveFee_DoNotPay(Guid workFlowID,
                     DateTime happenDate,
                     Guid deptID,
                     Guid userID,
                     Guid[] deptIDs,
                     Guid[] userIDs,
                     Guid[] customerIDs,
                     Guid[] glIDs,
                     Guid accountingID,
                     Guid generalManagerID,
                     Guid cashierID,
                     Guid financeManagerID,
                     bool isPay,
                     decimal? receiptQty,
                     decimal amount,
                     short feeProperty,
                     string no,
                     Guid[] costItemIDs,
                     string[] currencys,
                     decimal[] amounts,
                     string[] remarks);

        /// <summary>
        /// 删除取消的工作申请费用记录
        /// </summary>
        /// <param name="wordFlowID">流程ID</param>
        /// <returns></returns>
        [FunctionInfomation("DeleteCostFee")]
        [OperationContract]
        void DeleteCostFee(Guid wordFlowID);

        /// <summary>
        /// 保存亏损审批流程日志
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="workflowId">流程ID</param>
        /// <param name="createBy">创建人</param>
        [FunctionInfomation("SaveDeficitOperationWorkFlowLog")]
        [OperationContract]
        void SaveDeficitOperationWorkFlowLog(Guid? operationId, string operationType, Guid workflowId, Guid createBy);

        /// <summary>
        /// 删除亏损审批流程日志
        /// </summary>
        /// <param name="operationId"></param>
        [FunctionInfomation("RemoveDeficitOperationWorkFlowLog")]
        [OperationContract]
        void RemoveDeficitOperationWorkFlowLog(Guid operationId);

        /// <summary>
        /// 根据业务ID获得对应的流程ID
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        [FunctionInfomation("GetWorkFlowIdByOperationId")]
        [OperationContract]
        Guid? GetWorkFlowIdByOperationId(Guid operationId);

        /// <summary>
        /// 获得流程业务列表信息
        /// </summary>
        /// <param name="oprationNos">业务单号集合</param>
        /// <param name="companyIds">公司ID集合</param>
        /// <returns></returns>
        [FunctionInfomation("GetOperationIdByOperationNo")]
        [OperationContract]
        List<OperationSearchResult> GetOperationIdByOperationNo(string[] oprationNos, Guid[] companyIds);

        /// <summary>
        /// 根据流程ID获取支票打印数据
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [FunctionInfomation("GetCheckReportData")]
        [OperationContract]
        CashReportData GetCheckReportData(Guid workflowId);

        #region 插入流程费用
         /// <summary>
        /// 保存费用信息
        /// </summary>
        /// <param name="workFlowId">流程ID</param>
        /// <param name="no">单号</param>
        /// <param name="feeDate">费用时间</param>
        /// <param name="departmentId">申请部门</param>
        /// <param name="userId">申请人</param>
        /// <param name="accountingID">会计ID</param>
        /// <param name="generalManagerID">总经理ID</param>
        /// <param name="financeManagerID">财务经理</param>
        /// <param name="cashierID">出纳</param>
        /// <param name="receiptQty">单据数</param>
        /// <param name="feeProperty">费用类型</param>
        /// <param name="glIds">科目ID</param>
        /// <param name="currencyCodes">币种ID</param>
        /// <param name="dramounts">借方金额</param>
        /// <param name="cramounts">贷方金额</param>
        /// <param name="remarks">摘要</param>
        /// <param name="customerIDs">客户ID</param>
        /// <param name="depIDs">部门ID</param>
        /// <param name="userIDs">个人ID</param>
        Guid SaveCostFeeNew(
            Guid workFlowId,
            string no,
            DateTime feeDate,
            Guid departmentId,
            Guid userId,
            Guid movieProjectID,
            Guid accountingID,
            Guid generalManagerID,
            Guid financeManagerID,
            Guid cashierID,
            string receiptQty,
            short feeProperty,
            Guid[] glIds,
            string[] currencyCodes,
            decimal[] dramounts,
            decimal[] cramounts,
            string[] remarks,
            string[] customerIDs,
            string[] depIDs,
            string[] userIDs);
        #endregion

        #region 复制流程
        /// <summary>
        /// 复制流程
        /// </summary>
        /// <param name="workFlowID"></param>
        /// <param name="departmentID"></param>
        /// <param name="userID"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        [FaultContract(typeof(WorkflowExecutorNullExceptionInfo))]
        [FaultContract(typeof(WorkflowSqlExceptionInfo))]
        SingleResult CopyWorkFlowData(
            Guid workFlowID,
            Guid departmentID,
            Guid userID,
            DateTime startDate);
        #endregion
    }
}
