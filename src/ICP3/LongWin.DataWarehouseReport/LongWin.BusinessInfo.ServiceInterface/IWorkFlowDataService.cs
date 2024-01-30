using System;
using System.Collections.Generic;
using System.Text;
using Agilelabs.Framework;
using LongWin.BusinessInfo.ServiceInterface.DataObject;

namespace LongWin.BusinessInfo.ServiceInterface
{
    /// <summary>
    /// 此模块用来处理在工作流中产生的数据维护，由原来的数据复制
    /// 改为监视工作流中的数据更改
    /// </summary>
    [ServiceInfomation("IWorkFlowDataService", Agilelabs.Framework.ServiceType.Business)]
    public interface IWorkFlowDataService
    {
        /// <summary>
        /// 在工作完成以后，插入一条记录
        /// </summary>
        /// <param name="happenDate">发生日期</param>
        /// <param name="happenPeriod">发生期间</param>
        /// <param name="deptID">所属部门</param>
        /// <param name="userID">所属职员</param>
        /// <param name="amount">金额</param>
        /// <param name="feeProperty">费用性质</param>
        /// <param name="no">申请的单号</param>
        /// <param name="feeCode">费用项目</param>
        /// <param name="feeProperty">费用性质</param>
        /// <param name="amount">金额</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        [FunctionInfomation("InsertCostFee")]
        Guid SaveCostFee(Guid CostFeeId, DateTime happenDate, Guid deptID, Guid userID, decimal amount, short feeProperty, string no,
            Guid[] costItemIDs, decimal[] amounts, string[] remarks);

        /// <summary>
        /// 在工作完成以后，插入一条记录
        /// </summary>
        /// <param name="happenDate">发生日期</param>
        /// <param name="happenPeriod">发生期间</param>
        /// <param name="deptID">所属部门</param>
        /// <param name="userID">所属职员</param>
        /// <param name="amount">金额</param>
        /// <param name="feeProperty">费用性质</param>
        /// <param name="no">申请的单号</param>
        /// <param name="feeCode">费用项目</param>
        /// <param name="feeProperty">费用性质</param>
        /// <param name="amount">金额</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        [FunctionInfomation("SaveCostFee")]
        Guid SaveFee(Guid CostFeeId, DateTime happenDate, Guid deptID, Guid userID, decimal amount, short feeProperty, string no,
            Guid[] costItemIDs,string[] currencys, decimal[] amounts, string[] remarks);


        /// <summary>
        /// 删除取消的工作申请费用记录
        /// </summary>
        /// <param name="CostFeeId">主键</param>
        /// <returns></returns>
        [FunctionInfomation("DeleteCostFee")]
        void DeleteCostFee(Guid CostFeeId);

        [FunctionInfomation("SaveDeficitOperationWorkFlowLog")]
        void SaveDeficitOperationWorkFlowLog(Guid operationId, string operationType, Guid workflowId, Guid createBy);

        [FunctionInfomation("RemoveDeficitOperationWorkFlowLog")]
        void RemoveDeficitOperationWorkFlowLog(Guid operationId);

        [FunctionInfomation("GetWorkFlowIdByOperationId")]
        Guid? GetWorkFlowIdByOperationId(Guid operationId);


        [FunctionInfomation("GetOperationIdByOperationNo")]
        List<OperationSearchResult> GetOperationIdByOperationNo(string[] oprationNos, Guid[] companyIds);
    }
}
