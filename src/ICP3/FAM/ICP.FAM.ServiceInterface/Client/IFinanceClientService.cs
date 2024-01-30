using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 财务客户端访问方法
    /// </summary>
    public interface IFinanceClientService
    {
        /// <summary>
        /// 显示操作帐单列表
        /// </summary>
        /// <param name="operationCommonInfo">BillCommonInfo</param>
        /// <param name="workspaceName">workspaceName,传入空值以一个对话框形式打开</param>
        void ShowBillList(OperationCommonInfo operationCommonInfo, string workspaceName);

        /// <summary>
        /// 显示财务账单列表
        /// </summary>
        /// <param name="criteria"></param>
        void ShowBillList(BillListQueryCriteria criteria);

        /// <summary>
        /// 显示核销单编辑界面
        /// </summary>
        /// <param name="dicList">数据源</param>
        /// <param name="title">title</param>
        /// <param name="workspaceName">workspaceName,传入空值以一个对话框形式打开</param>
        void ShowWriteOffEditor(string title,Dictionary<string, object> dicList, string workspaceName);

        /// <summary>
        /// 显示电放申请单或显示电放申请单录入界面
        /// </summary>
        /// <param name="businessId">业务唯一ID</param>
        /// <param name="refNo">业务参考号</param>
        void ShowSingleBusinessTelexApply(Guid businessId, string refNo);

        /// <summary>
        /// 显示凭证信息
        /// </summary>
        /// <param name="masterID"></param>
        void ShowLedgerInfo(Guid masterID);

        /// <summary>
        /// 显示管理费用月预算信息
        /// </summary>
        void ShowFeeYearMonthBudgetPart();

        /// <summary>
        /// 批量新增账单
        /// </summary>
        /// <param name="editPartSaved">编辑完成后事件</param>
        void BatchAddBill(PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 打印批量账单
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="billIDs">账单列表</param>
        /// <param name="userID">打印人</param>
        void PrintBatchBill(Guid customerID, Guid companyID, Guid[] billIDs, Guid userID);
    }
}
