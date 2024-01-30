using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;

namespace ICP.Sys.UI.WorkSpaceView
{
    public partial class WorkSpaceViewMainWorkSpace : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public WorkSpaceViewMainWorkSpace()
        {
            InitializeComponent();

            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.ToolWorkSpace);
                    Workitem.Workspaces.Remove(this.ListWorkSpace);
                    Workitem.Workspaces.Remove(this.OPListWorkSpace);
                    Workitem.Workspaces.Remove(this.UserListWorkSpace);
                    Workitem.Workspaces.Remove(this.RoleListWorkSpace);
                    Workitem.Items.Remove(this);

                    this.ToolWorkSpace.PerformLayout();
                    this.ListWorkSpace.PerformLayout();
                    this.UserListWorkSpace.PerformLayout();
                    this.PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };

        }
    }
}
