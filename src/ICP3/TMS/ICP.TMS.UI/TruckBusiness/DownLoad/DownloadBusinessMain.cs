using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.TMS.UI
{
    [ToolboxItem(false)]
    public partial class DownloadBusinessMain : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public DownloadBusinessMain()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.ToolbarWorkspace);
                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.ListWorkspace);

                    this.SearchWorkspace.PerformLayout();
                    this.ToolbarWorkspace.PerformLayout();
                    this.ListWorkspace.PerformLayout();
                   

                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    this.PerformLayout();
                   
                }
            };
         
        }



        protected override void OnLoad(EventArgs e)
        {
            dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            this.ListWorkspace.Dock = DockStyle.Fill;

            base.OnLoad(e);
            ToolbarWorkspace.SendToBack();
        }

        [CommandHandler(TMSDownLoadCommandConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object o, EventArgs e)
        {
            if (dpSearch.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Hidden)
            {
                dpSearch.Show();
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                this.ListWorkspace.Dock = DockStyle.Fill;
            }
            else
            {
                dpSearch.Hide();
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                this.ListWorkspace.Dock = DockStyle.Fill;
            }
            ToolbarWorkspace.SendToBack();
            this.Refresh();
        }

    }
}
