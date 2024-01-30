using DevExpress.XtraBars.Docking;
using DevExpress.XtraTab;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ICP.Business.Common.UI.CSPBooking
{
    /// <summary>
    /// CSP Booking布局面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class PartMainWorkspace : BasePart
    {
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        public PartMainWorkspace()
        {
            InitializeComponent();

            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(CSPBooking_Layout_Toolbar_WorkSpace);

                    Workitem.Workspaces.Remove(CSPBooking_Layout_Search_WorkSpace);
                    Workitem.Workspaces.Remove(CSPBooking_Layout_FastSearch_WorkSpace);
                    Workitem.Workspaces.Remove(CSPBooking_Layout_List_WorkSpace);
                    Workitem.Workspaces.Remove(EventListWorkspace);
                    Workitem.Workspaces.Remove(FaxMailEDIListWorkspace);
                    Workitem.Workspaces.Remove(DocumentListWorkspace);

                    CSPBooking_Layout_Search_WorkSpace.PerformLayout();
                    CSPBooking_Layout_Toolbar_WorkSpace.PerformLayout();
                    CSPBooking_Layout_List_WorkSpace.PerformLayout();
                    EventListWorkspace.PerformLayout();
                    FaxMailEDIListWorkspace.PerformLayout();
                    DocumentListWorkspace.PerformLayout();
                    CSPBooking_Layout_FastSearch_WorkSpace.PerformLayout();

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
                CSPBooking_Layout_FastSearch_WorkSpace.Show();
                dpSearch.Visibility = DockVisibility.Hidden;
                CSPBooking_Layout_List_WorkSpace.Dock = DockStyle.Fill;

                CSPBooking_Layout_Toolbar_WorkSpace.SendToBack();
                xtab.SelectedPageChanged += new TabPageChangedEventHandler(SelectedPageChanged);
            }
        }

        void SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {

        }

        [CommandHandler(CSPBKConstants.COMMAND_SHOWSEARCH)]
        public void Command_ShowSearch(object o, EventArgs e)
        {
            if (CSPBooking_Layout_FastSearch_WorkSpace.Visible)
            {
                CSPBooking_Layout_FastSearch_WorkSpace.Hide();
                dpSearch.Visibility = DockVisibility.Visible;
                CSPBooking_Layout_List_WorkSpace.Dock = DockStyle.Fill;
            }
            else
            {
                CSPBooking_Layout_FastSearch_WorkSpace.Show();
                dpSearch.Visibility = DockVisibility.Hidden;
                CSPBooking_Layout_List_WorkSpace.Dock = DockStyle.Fill;
            }
            CSPBooking_Layout_Toolbar_WorkSpace.SendToBack();
            Refresh();
        }

        public event CurrentChangedHandler TabSelectedPageChanged;
        private void xtab_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (TabSelectedPageChanged != null)
                TabSelectedPageChanged(sender, e);
        }
    }
}
