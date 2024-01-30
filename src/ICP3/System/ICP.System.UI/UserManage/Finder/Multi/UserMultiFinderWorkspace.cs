using System;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Sys.UI.UserManage.Finder
{
    [ToolboxItem(false)]
    public partial class UserMultiFinderWorkspace : DevExpress.XtraEditors.XtraUserControl
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        public UserMultiFinderWorkspace()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {

                    Workitem.Workspaces.Remove(this.EditWorkspace);
                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.ListWorkspace);
                    Workitem.Workspaces.Remove(this.ToolBarWorkspace);
                    Workitem.Workspaces.Remove(this.SelectedListWorkspace);
                    Workitem.Workspaces.Remove(this.SelectedToolBarWorkspace);

                    this.SearchWorkspace.PerformLayout();
                    this.EditWorkspace.PerformLayout();
                    this.ListWorkspace.PerformLayout();
                    this.ToolBarWorkspace.PerformLayout();
                    this.SelectedListWorkspace.PerformLayout();
                    this.SelectedToolBarWorkspace.PerformLayout();

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
            dpSelected.Text = "编辑";
            dpSearch.Text = "查询";
        }

        #endregion

        #region Workitem Common

        [CommandHandler(UserCommonConstants.Command_ShowSearch)]
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

