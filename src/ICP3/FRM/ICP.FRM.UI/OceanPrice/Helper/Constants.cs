
namespace ICP.FRM.UI.OceanPrice
{
    #region OPWorkSpaceConstants

    public class OPWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolBarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string ContractWorkspace = "ContractWorkspace";
        public const string BPRWorkspace = "BPRWorkspace";
        public const string ARWorkspace = "ARWorkspace";
        public const string AFWorkspace = "AFWorkspace";
        public const string PermissionsWorkspace = "PermissionsWorkspace";
        public const string AttachmentWorkspace = "AttachmentWorkspace";
    }

    #endregion

    #region OPCommonConstants

    public class OPCommonConstants
    {
        public const string Command_MoveToContract = "Command_MoveToContract";
        public const string Command_SaveData = "Command_SaveData";

        public const string Command_AddData = "Command_AddData";
        public const string Command_InsterNewData = "Command_InsterNewData";

        public const string Command_DeleteData = "Command_DeleteData";

        public const string Command_PublishPauseData = "Command_PublishPauseData";
        public const string Command_InvalidateResumeData = "Command_InvalidateResumeData";

        public const string Command_ShowSearch = "Command_ShowSearch";
        public const string Command_CopyData = "Command_CopyData";
        public const string Command_MaxOceanItem = "Command_MaxOceanItem";
        public const string Command_RefreshData = "Command_Refresh";
        public const string Command_ExportToExcel = "Command_ExportToExcel";

        public const string Command_Compare = "Command_Compare";
        //public const string Command_Inquiery = "Command_Inquiery";//未完成
        /// <summary>
        /// 拷贝运价时，将tab页切换到契约明细页签
        /// </summary>
        public const string Command_FirstTabFocused = "Command_FirstTabFocused";

        public const string Command_TabChanged = "Command_TabChanged";
    }

    public class OPStateConstants
    {
        public const string State_OceanUnitData = "State_OceanUnitData";
    }

    #endregion
}
