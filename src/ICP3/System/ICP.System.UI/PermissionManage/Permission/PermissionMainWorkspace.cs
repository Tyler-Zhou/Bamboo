using System.ComponentModel;
using Microsoft.Practices.CompositeUI;

namespace ICP.Sys.UI.PermissionManage.Permission
{
    [ToolboxItem(false)]
    public partial class PermissionMainWorkspace : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public PermissionMainWorkspace()
        {
            InitializeComponent();

            this.Disposed += delegate
            {
                if (Workitem != null)
                {

                    Workitem.Workspaces.Remove(this.ToolbarWorkspace);

                    Workitem.Workspaces.Remove(this.MenuWorkspace);
                    
                    Workitem.Workspaces.Remove(this.StatusbarWorkspace);
                    this.MenuWorkspace.PerformLayout();
                    this.ToolbarWorkspace.PerformLayout();
                    this.StatusbarWorkspace.PerformLayout();
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
            tpMenu.Text = "名称";
            tpStatusbar.Text = "状态栏";
            tpToolbar.Text = "工具栏";
           
        }
    }
}
