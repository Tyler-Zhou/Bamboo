

//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="ICP">
//     Copyright (c) ICP. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.UI
{
    using System;

    /// <summary>
    /// 命令常量
    /// </summary>
    public class CommandConstants
    {
        /// <summary>
        /// 打开设计器命令常量
        /// </summary>
        public const string Command_OpenWorkList = "WF_WorkList";

        public const string Command_DeleteLedger = "WF_DELETELEDGER";

        /// <summary>
        /// 新建命令常量
        /// </summary>
        public const string Command_WorkList_New = "Command_WorkList_New";
        /// <summary>
        /// 审核
        /// </summary>
        public const string Command_WorkList_Auditor = "Command_WorkList_Auditor";
        /// <summary>
        /// 审核(合并凭证号)
        /// </summary>
        public const string Command_WorkList_AuditorMerger = "Command_WorkList_AuditorMerger";
        /// <summary>
        /// 取消审核
        /// </summary>
        public const string Command_WorkList_UnAuditor = "Command_WorkList_UnAuditor";

        /// <summary>
        /// 编辑
        /// </summary>
        public const string Command_WorkList_Edit = "CommandWorkListEdit";

        /// <summary>
        /// 取消
        /// </summary>
        public const string Command_WorkList_Cancel = "CommandWorkListCancel";

        /// <summary>
        /// 打印
        /// </summary>
        public const string Comand_WorkList_Print = "ComandWorkListPrint";

        /// <summary>
        /// 打印报表
        /// </summary>
        public const string Comand_WorkList_PrintReport = "ComandWorkListPrintReport";

        /// <summary>
        /// 流程图
        /// </summary>
        public const string Command_WorkList_FlowChat = "CommandWorkListFlowChat";

        /// <summary>
        /// 刷新
        /// </summary>
        public const string Command_WorkList_Refresh = "CommandWorkListRefresh";
        /// <summary>
        /// 批量编辑权限
        /// </summary>
        public const string Command_WorkList_WF_MULTIFINISH = "WF_MULTIFINISH";

        /// <summary>
        /// 关闭
        /// </summary>
        public const string Command_WorkList_Close = "CommandWorkListClose";

        public static readonly string[] ResultValue = new string[] { "ID", "Code", "EName", "CName" };
        public const string CodeName = @"Code/Name";
        public const string Name = "Name";
        public const string Code = "Code";


        public static Guid NewID =new Guid("E4765E59-453D-4C0E-B457-BD6E28341339");
    }
    /// <summary>
    /// 出纳支付表单中的字段名
    /// </summary>
    public class CashierAuditorFormDataColumns
    {
        public static string MasterRemark = "Remark";
        public static string Opinion = "Opinion";
        public static string FeeDate = "FeeDate";
        public static string GLID = "GLID";
        public static string DetailRemark = "Remarks";
        public static string OrgAmt = "OrgAmt";
        public static string DRAmt = "DRAmt";
        public static string CRAmt = "CRAmt";
        public static string CustomerID = "CustomerID";
        public static string DeptID = "DeptID";
        public static string UserID = "UserID";
        public static string CurrencyID = "CurrencyID";
        public static string GLFullName = "GLFullName";
        public static string CustomerName = "CustomerName";
        public static string DeptName = "DeptName";
        public static string UserName = "UserName";
    }

    /// <summary>
    /// 财务审核表单中的字段名
    /// </summary>
    public class AccountAuditorFormDataColumns
    {
        public static string MasterRemark = "Remark";
        public static string Opinion = "Opinion";
        public static string ReceiptQty = "ReceiptQty";
        public static string GLID = "GLID";
        public static string DetailRemark = "Remarks";
        public static string OrgAmt = "OrgAmt";
        public static string DRAmt = "DRAmt";
        public static string CRAmt = "CRAmt";
        public static string CustomerID = "CustomerID";
        public static string DeptID = "DeptID";
        public static string UserID = "UserID";
        public static string CurrencyID = "CurrencyID";
        public static string GLFullName = "GLFullName";
        public static string CustomerName = "CustomerName";
        public static string DeptName = "DeptName";
        public static string UserName = "UserName";
    }

}
