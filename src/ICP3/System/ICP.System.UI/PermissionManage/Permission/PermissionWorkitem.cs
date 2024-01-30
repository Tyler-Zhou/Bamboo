using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.Sys.UI.PermissionManage.Permission
{
    public class PermissionWorkitem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            PermissionMainWorkspace permissionMainSpce = this.SmartParts.Get<PermissionMainWorkspace>("PermissionMainWorkspace");
            if (permissionMainSpce == null)
            {
                permissionMainSpce = this.SmartParts.AddNew<PermissionMainWorkspace>("PermissionMainWorkspace");

                #region AddPart

                PermissionMenuListPart permissionMenuListPart = this.SmartParts.AddNew<PermissionMenuListPart>();
                IWorkspace menuWorkspace = (IWorkspace)this.Workspaces[PermissionWorkSpaceConstants.MenuWorkspace];
                menuWorkspace.Show(permissionMenuListPart);

                PermissionToolbarListPart permissionToolbarListPart = this.SmartParts.AddNew<PermissionToolbarListPart>();
                IWorkspace toolbarWorkspace = (IWorkspace)this.Workspaces[PermissionWorkSpaceConstants.ToolbarWorkspace];
                toolbarWorkspace.Show(permissionToolbarListPart);

                PermissionStatusbarListPart permissionStatusbarListPart = this.SmartParts.AddNew<PermissionStatusbarListPart>();
                IWorkspace statusbarWorkspace = (IWorkspace)this.Workspaces[PermissionWorkSpaceConstants.StatusbarWorkspace];
                statusbarWorkspace.Show(permissionStatusbarListPart);

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Permission Manage" : "界面配置";
                mainWorkspace.Show(permissionMainSpce, smartPartInfo);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(permissionMainSpce);
            }
        }
    }

    public class PermissionWorkSpaceConstants
    {
        public const string MenuWorkspace = "MenuWorkspace";
        public const string ToolbarWorkspace = "ToolbarWorkspace";
        public const string StatusbarWorkspace = "StatusbarWorkspace";
    }
}
