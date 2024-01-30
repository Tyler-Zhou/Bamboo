namespace ICP.FAM.UI.WriteOff
{
    #region 销账界面容器名
    /// <summary>
    /// 销账界面容器名
    /// </summary>
    public class WriteOffWorkSpace
    {
        /// <summary>
        /// 
        /// </summary>
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        /// <summary>
        /// 
        /// </summary>
        public const string SearchWorkspace = "SearchWorkspace";
        /// <summary>
        /// 
        /// </summary>
        public const string ListWorkspace = "ListWorkspace";
        /// <summary>
        /// 
        /// </summary>
        public const string FastSearchWorkspace = "FastSearchWorkspace";
        /// <summary>
        /// 
        /// </summary>
        public const string CommonTabs = "CommonTabs";
        /// <summary>
        /// 
        /// </summary>
        public const string MultiSelection = "MultiSelection";
        /// <summary>
        /// 
        /// </summary>
        public const string EventListWorkspace = "EventListWorkspace";
    }
    #endregion

    #region 销账命令集
    /// <summary>
    /// 
    /// </summary>
    public class WriteOffCommands
    {
        /// <summary>
        /// 应收
        /// </summary>
        public const string Command_WriteOff_AddData_DR = "WriteOff_AddData_DR";

        /// <summary>
        /// 应付
        /// </summary>
        public const string Command_WriteOff_AddData_CR = "WriteOff_AddData_CR";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_EditData = "WriteOff_EditData";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_DeleteData = "Command_DeleteData";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_VoidData = "Command_VoidData";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_AddInvoice = "Command_AddInvoice";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_EditCurrencyRate = "Command_EditCurrencyRate";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_ListCredentials = "Command_ListCredentials";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_SearchBills = "Command_SearchBills";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_ShowSearch = "Command_ShowSearch";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_Auditor = "Command_Auditor";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_UnAuditor = "Command_UnAuditor";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_PrintCheck = "Command_PrintCheck";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_CredentialsPrint = "Command_CredentialsPrint";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_UntieLock = "Command_UntieLock";
        /// <summary>
        /// 直连银行支付
        /// </summary>
        public const string Command_DirectBank = "Command_DirectBank";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_SetCurrencyRate = "Command_SetCurrencyRate";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_AllCheck = "Command_AllCheck";
        /// <summary>
        /// 删除账单
        /// </summary>
        public const string Command_DeleteBills = "Command_DeleteBills";
        /// <summary>
        /// 选取账单
        /// </summary>
        public const string Command_AutoBillsFinde = "Command_AutoBillsFinde";
        /// <summary>
        /// 到账
        /// </summary>
        public const string Command_Bullion = "Command_Bullion";
        /// <summary>
        /// 取消到账
        /// </summary>
        public const string Command_CancelBullion = "Command_CancelBullion";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_AllowMultiSelection = "Command_AllowMultiSelection";

        /// <summary>
        /// 智能查找账单，以后应该是特殊搜索器活着服务
        /// 先暂时用命令实现界面切换
        /// </summary>
        public const string Command_BrightBillsFinder = "Command_BrightBillsFinder";
        
    }
    #endregion
}
