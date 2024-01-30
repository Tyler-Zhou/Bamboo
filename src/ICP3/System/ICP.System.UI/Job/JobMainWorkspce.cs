using System.ComponentModel;
using Microsoft.Practices.CompositeUI;

namespace ICP.Sys.UI.Job
{
    [ToolboxItem(false)]
    public partial class JobMainWorkspace : DevExpress.XtraEditors.XtraUserControl
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        public JobMainWorkspace()
        {
            InitializeComponent();
            this.Disposed += delegate
            {  
                
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.EditWorkspace);
                    Workitem.Workspaces.Remove(this.OrganizationWorkspace);
                    Workitem.Workspaces.Remove(this.ListWorkspace);
                    this.EditWorkspace.PerformLayout();
                    this.OrganizationWorkspace.PerformLayout();
                    this.ListWorkspace.PerformLayout();

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
            dpOrganization.Text = "组织结构";
        }
        #endregion


        #region barItem

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        #endregion
    }
}
