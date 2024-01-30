using System;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Sys.UI.UserManage
{
    [ToolboxItem(false)]
    public partial class UserMainWorkspace : DevExpress.XtraEditors.XtraUserControl
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        public UserMainWorkspace()
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
                    Workitem.Workspaces.Remove(this.MailToolBarWorkspace);
                    Workitem.Workspaces.Remove(this.MailEditWorkspace);
                    Workitem.Workspaces.Remove(this.MailListWorkspace);
                    this.SearchWorkspace.PerformLayout();
                    this.EditWorkspace.PerformLayout();
                    this.ListWorkspace.PerformLayout();
                    this.OrgJobWorkspace.PerformLayout();
                    this.MailToolBarWorkspace.PerformLayout();
                    this.MailEditWorkspace.PerformLayout();
                    this.MailListWorkspace.PerformLayout();

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
            dpMailAccount.Text = "邮件配置";
            dpFunction.Text = "功能项";
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

        public void SetOrgJobEnabled(bool isEnabled)
        {
            OrgJobWorkspace.Enabled = isEnabled;
        }
        #endregion
    }
}

