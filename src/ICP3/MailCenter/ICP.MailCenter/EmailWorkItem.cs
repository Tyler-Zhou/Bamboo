using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.MailCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 邮件中心WorkItem
    /// </summary>
    public partial class EmailWorkItem : WorkItem
    {
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }

        public Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> SmartParts
        {
            get { return RootWorkItem.SmartParts; }
        }

        /// <summary>
        /// 启动时打开主界面
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        EmailListPart listPart = null;
        private void Show()
        {
            //默认使用的是Outlook2007
            MailUtility.AutoSyncDisable(false);
            MailUtility.SingleProcess(false);

            MailCenterMainWorkspace mainSpce = this.SmartParts.Get<MailCenterMainWorkspace>("MailCenterMainWorkspace");

            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<MailCenterMainWorkspace>("MailCenterMainWorkspace");

                EmailFolderPart folderPart = SmartParts.AddNew<EmailFolderPart>(MailCenterWorkSpaceConstants.EmailFolderPart);
                SearchFolderPart searchFolderPart = SmartParts.AddNew<SearchFolderPart>(MailCenterWorkSpaceConstants.SearchFolderPart);

                EmailToolBarPart toolPart = SmartParts.AddNew<EmailToolBarPart>(MailCenterWorkSpaceConstants.EmailToolBarPart);
                EmailDetailPart detailPart = SmartParts.AddNew<EmailDetailPart>(MailCenterWorkSpaceConstants.EmailDetailPart);
                UCMailSearch mailSearchPart = SmartParts.AddNew<UCMailSearch>(MailCenterWorkSpaceConstants.EmailSearchPart);
                listPart = SmartParts.AddNew<EmailListPart>(MailCenterWorkSpaceConstants.EmailListPart);

                IWorkspace searchFolderWorkSpace =
                    (IWorkspace)this.Workspaces[MailCenterWorkSpaceConstants.SearchFoldersWorkSpace];
                searchFolderWorkSpace.Show(searchFolderPart);

                IWorkspace folderWorkSpace = (IWorkspace)this.Workspaces[MailCenterWorkSpaceConstants.FolderWorkSpace];
                folderWorkSpace.Show(folderPart);

                IWorkspace toolWorkspace = (IWorkspace)this.Workspaces[MailCenterWorkSpaceConstants.ToolWorkSpace];
                toolWorkspace.Show(toolPart);

                IWorkspace listWorkSpace = (IWorkspace)this.Workspaces[MailCenterWorkSpaceConstants.ListWorkSpace];
                listWorkSpace.Show(listPart);

                IWorkspace detailWorkSpace = (IWorkspace)this.Workspaces[MailCenterWorkSpaceConstants.DetailWorkSpace];
                detailWorkSpace.Show(detailPart);

                IWorkspace searchWorkSpace = (IWorkspace)this.Workspaces[MailCenterWorkSpaceConstants.SearchWorkSpace];
                searchWorkSpace.Show(mailSearchPart);

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Email" : "邮件";
                mainWorkspace.Show(mainSpce, smartPartInfo);
                AfterWorking();

            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }

        void AfterWorking()
        {
            if (listPart != null)
            {
                listPart.RemoveSelectionChangedHandler();
            }
            RootWorkItem.Commands[MailCenterCommandConstants.Command_SelectionChanged].Execute();
            if (listPart != null)
            {
                listPart.AddSelectionChangeHandler();
            }
        }


    }

    /// <summary>
    /// 业务信息命令常量
    /// </summary>
    public partial class MailCenterCommandConstants
    {
        public const string Command_OpenSearchMail = "Command_OpenSearchMail";

        public const string Command_OpenAttContextMenu = "Command_OpenAttContextMenu";

        public const string Command_Carrier = "Command_Carrier";

        public const string Command_AddNewMail = "Command_AddNewMail";

        public const string Command_UploadCopyFile = "Command_UploadCopyFile";

        public const string Command_SearchContent = "Command_SearchContent";

        public const string Command_OpenAccInfo = "Command_OpenAccInfo";

        public const string Command_ReplyMail = "Command_ReplyMail";

        public const string Command_ReplyALL = "Command_ReplyALL";

        public const string Command_Forward = "Command_Forward";

        public const string Command_OpenSearchForm = "Command_OpenSearchForm";

        public const string Command_CloseMainForm = "Command_CloseMainForm";

        public const string Command_SelectionChanged = "Command_SelectionChanged";

        public const string Command_CurrentFolderChanged = "Command_CurrentFolderChanged";

        public const string Command_CurrentMailItemChanged = "Command_CurrentMailItemChanged";

        public const string CurrentSelection = "Command_CurrentSelection";

        public const string CurrentNodeText = "CurrentNodeText";

        public const string CurrentEntryID = "CurrentEntryID";

        public const string TargetFolder = "TargetFolder";
        //邮件归档
        public const string Command_EmailArchiving = "Command_EmailArchiving";
    }

    public partial class MailCenterWorkSpaceConstants
    {
        public const string EmailDetailPart = "EmailDetailPart";
        public const string EmailToolBarPart = "EmailToolBarPart";
        public const string EmailListPart = "EmailListPart";
        public const string EmailFolderPart = "EmailFolderPart";
        public const string EmailSearchPart = "EmailSearchPart";
        public const string ToolWorkSpace = "deckWorkspaceTool";
        public const string SearchWorkSpace = "mailSearchdeckWorkspace";
        public const string FolderWorkSpace = "deckWorkspaceFolder";
        public const string SearchFolderPart = "SearchFolderPart";
        public const string SearchFoldersWorkSpace = "deckSearchFolders";

        public const string ListWorkSpace = "deckWorkspaceEmailList";

        public const string DetailWorkSpace = "deckWorkspaceEmailDetail";

        public const string UploadSIWorkspace = "UploadSIWorkspace";

        public const string EmailMainPart = "EmailMainPart";

        public const string SynchronFolders_Toolbar = "SynchronFolders_Toolbar";

        public const string Command_ItemSend = "ItemSend";

        public const string Command_MoveMailToFolder = "Command_MoveMailToFolder";

        public const string Command_SynchronFolders = "Command_SynchronFolders";

        public const string Command_MailCenterInfo = "Command_MailCenterInfo";

        public const string Command_RefershAllExpandNodes = "Command_RefershAllExpandNodes";

        public const string Command_RefershFolderList = "Command_RefershFolderList";

    }
}
