using System.ComponentModel;
using Microsoft.Practices.CompositeUI;

namespace ICP.Sys.UI.UserManage.MiniFinder
{
    /// <summary>
    /// UMFS= UserMiniFinderSingle
    /// </summary>
    [ToolboxItem(false)]
    public partial class UMFS_MainWorkspace : DevExpress.XtraEditors.XtraUserControl
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        public UMFS_MainWorkspace()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.ListWorkspace);
                    Workitem.Workspaces.Remove(this.OrganizationListWorkspace);
                    this.ListWorkspace.PerformLayout();
                    this.OrganizationListWorkspace.PerformLayout();
                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    this.PerformLayout();
                }
            };

        }
        #endregion

    }
}

