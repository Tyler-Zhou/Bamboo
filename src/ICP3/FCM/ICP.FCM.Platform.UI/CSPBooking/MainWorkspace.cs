using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIManagement;
using DevExpress.XtraTab;

namespace ICP.FCM.Platform.UI.CSPBooking
{
    /// <summary>
    /// CSP Booking布局面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class MainWorkspace : BasePart
    {
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        public MainWorkspace()
        {
            InitializeComponent();

            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ToolbarWorkspace);

                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(FastSearchWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Workspaces.Remove(EventListWorkspace);
                    Workitem.Workspaces.Remove(FaxMailEDIListWorkspace);
                    Workitem.Workspaces.Remove(DocumentListWorkspace);

                    SearchWorkspace.PerformLayout();
                    ToolbarWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();
                    EventListWorkspace.PerformLayout();
                    FaxMailEDIListWorkspace.PerformLayout();
                    DocumentListWorkspace.PerformLayout();
                    FastSearchWorkspace.PerformLayout();

                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    PerformLayout();

                }
            };
        }
        /// <summary>
        /// 重写加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                FastSearchWorkspace.Show();
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                this.ListWorkspace.Dock = DockStyle.Fill;

                ToolbarWorkspace.SendToBack();
                xtab.SelectedPageChanged += new TabPageChangedEventHandler(SelectedPageChanged);
            }
        }

        void SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {

        }

        [CommandHandler(CSPBookingConstants.COMMAND_SHOWSEARCH)]
        public void Command_ShowSearch(object o, EventArgs e)
        {
            if (FastSearchWorkspace.Visible)
            {
                FastSearchWorkspace.Hide();
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                this.ListWorkspace.Dock = DockStyle.Fill;
            }
            else
            {
                FastSearchWorkspace.Show();
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                this.ListWorkspace.Dock = DockStyle.Fill;
            }
            ToolbarWorkspace.SendToBack();
            this.Refresh();
        }

        public event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler TabSelectedPageChanged;
        private void xtab_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (TabSelectedPageChanged != null)
                TabSelectedPageChanged(sender, e);
        }
    }
}
