using System;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Sys.UI.Role
{
    [ToolboxItem(false)]
    public partial class RoleMainWorkspace : DevExpress.XtraEditors.XtraUserControl
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        public RoleMainWorkspace()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.EditWorkspace);
                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.ListWorkspace);
                    Workitem.Workspaces.Remove(this.OrgJobWorkspace);
                    Workitem.Workspaces.Remove(this.EditWorkspace);

                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.OrgJobWorkspace);
                    Workitem.Workspaces.Remove(this.ListWorkspace);
                    this.SearchWorkspace.PerformLayout();
                    this.OrgJobWorkspace.PerformLayout();
                    this.ListWorkspace.PerformLayout();
                    this.EditWorkspace.PerformLayout();
                  

                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    this.PerformLayout();

                }
            };

            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            dpEdit.Text = "编辑";
            dpOrgJob.Text = "职位";
            dpSearch.Text = "查询";
            dpFunction.Text = "功能项";
        }

        #endregion

        #region Workitem Common

        [CommandHandler(RoleCommonConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object sender, EventArgs e)
        {
            if (dpSearch.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible)
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            else
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }

        #endregion
    }
}
