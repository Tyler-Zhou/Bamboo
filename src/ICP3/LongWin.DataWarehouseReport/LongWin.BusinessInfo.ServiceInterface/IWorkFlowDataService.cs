using System;
using System.Collections.Generic;
using System.Text;
using Agilelabs.Framework;
using LongWin.BusinessInfo.ServiceInterface.DataObject;

namespace LongWin.BusinessInfo.ServiceInterface
{
    /// <summary>
    /// ��ģ�����������ڹ������в���������ά������ԭ�������ݸ���
    /// ��Ϊ���ӹ������е����ݸ���
    /// </summary>
    [ServiceInfomation("IWorkFlowDataService", Agilelabs.Framework.ServiceType.Business)]
    public interface IWorkFlowDataService
    {
        /// <summary>
        /// �ڹ�������Ժ󣬲���һ����¼
        /// </summary>
        /// <param name="happenDate">��������</param>
        /// <param name="happenPeriod">�����ڼ�</param>
        /// <param name="deptID">��������</param>
        /// <param name="userID">����ְԱ</param>
        /// <param name="amount">���</param>
        /// <param name="feeProperty">��������</param>
        /// <param name="no">����ĵ���</param>
        /// <param name="feeCode">������Ŀ</param>
        /// <param name="feeProperty">��������</param>
        /// <param name="amount">���</param>
        /// <param name="remark">��ע</param>
        /// <returns></returns>
        [FunctionInfomation("InsertCostFee")]
        Guid SaveCostFee(Guid CostFeeId, DateTime happenDate, Guid deptID, Guid userID, decimal amount, short feeProperty, string no,
            Guid[] costItemIDs, decimal[] amounts, string[] remarks);

        /// <summary>
        /// �ڹ�������Ժ󣬲���һ����¼
        /// </summary>
        /// <param name="happenDate">��������</param>
        /// <param name="happenPeriod">�����ڼ�</param>
        /// <param name="deptID">��������</param>
        /// <param name="userID">����ְԱ</param>
        /// <param name="amount">���</param>
        /// <param name="feeProperty">��������</param>
        /// <param name="no">����ĵ���</param>
        /// <param name="feeCode">������Ŀ</param>
        /// <param name="feeProperty">��������</param>
        /// <param name="amount">���</param>
        /// <param name="remark">��ע</param>
        /// <returns></returns>
        [FunctionInfomation("SaveCostFee")]
        Guid SaveFee(Guid CostFeeId, DateTime happenDate, Guid deptID, Guid userID, decimal amount, short feeProperty, string no,
            Guid[] costItemIDs,string[] currencys, decimal[] amounts, string[] remarks);


        /// <summary>
        /// ɾ��ȡ���Ĺ���������ü�¼
        /// </summary>
        /// <param name="CostFeeId">����</param>
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
