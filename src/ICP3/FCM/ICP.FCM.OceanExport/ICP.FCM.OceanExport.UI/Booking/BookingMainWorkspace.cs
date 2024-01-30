using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.OceanExport.UI.Booking
{
    [ToolboxItem(false)]
    public partial class BookingMainWorkspace : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public bool IsSelectedCargoTracking
        {
            get
            {
                if (xtab.SelectedTabPage == tabCargoTracking)
                    return true;
                return false;
            }
        }

        public BookingMainWorkspace()
        {
            InitializeComponent();

            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.ToolbarWorkspace);

                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.FastSearchWorkspace);
                    Workitem.Workspaces.Remove(this.ListWorkspace);
                    Workitem.Workspaces.Remove(this.EventListWorkspace);
                    Workitem.Workspaces.Remove(this.FaxMailEDIListWorkspace);
                    Workitem.Workspaces.Remove(this.DocumentListWorkspace);
                    Workitem.Workspaces.Remove(this.BLListWorkSpace);
                    Workitem.Workspaces.Remove(this.CargoTrackingWorkspace);

                    this.BLListWorkSpace.PerformLayout();
                    this.CargoTrackingWorkspace.PerformLayout();
                    this.SearchWorkspace.PerformLayout();
                    this.ToolbarWorkspace.PerformLayout();
                    this.ListWorkspace.PerformLayout();
                    this.EventListWorkspace.PerformLayout();
                    this.FaxMailEDIListWorkspace.PerformLayout();
                    this.DocumentListWorkspace.PerformLayout();
                    this.FastSearchWorkspace.PerformLayout();

                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    this.PerformLayout();

                }
            };
            //if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            this.dpSearch.Text = "查询";
            this.tabDocumentList.Text = "文档列表";
            this.tabFaxMailList.Text = "传真/邮件/EDI";
            this.tabMemoList.Text = "事件列表";
            this.tabCargoTracking.Text = "货物跟踪";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                FastSearchWorkspace.Show();
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                this.ListWorkspace.Dock = DockStyle.Fill;

                ToolbarWorkspace.SendToBack();
                if (!LocalData.IsEnglish)
                {
                    SetCnText();
                }
                //this.dpSearch.Text = LocalData.IsEnglish ? "Search" : "查询";
                xtab.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(SelectedPageChanged);
            }
        }

        void SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            // Workitem.State[OEBookingCommandConstants.SelectedTabPageIndex] = xtab.SelectedTabPageIndex;
            //this.Workitem.Commands[OEBookingCommandConstants.Command_TabChanged].Execute();

        }

        [CommandHandler(OEBookingCommandConstants.Command_ShowSearch)]
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
