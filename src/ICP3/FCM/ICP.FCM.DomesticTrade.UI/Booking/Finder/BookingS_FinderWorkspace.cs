using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.FCM.DomesticTrade.UI.Booking.Finder
{
    [ToolboxItem(false)]
    public partial class BookingS_FinderWorkspace : XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public BookingS_FinderWorkspace()
        {
            InitializeComponent();

            Disposed += delegate
            {
                if (Workitem != null)
                {

                    Workitem.Workspaces.Remove(ToolbarWorkspace);

                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(FastSearchWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);
                   


                    SearchWorkspace.PerformLayout();
                    ToolbarWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();
                    FastSearchWorkspace.PerformLayout();

                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    PerformLayout();

                }
            };
            if (LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            dpSearch.Text = "查询";
        }

        [CommandHandler(DTBookingCommandConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object o, EventArgs e)
        {
            if (FastSearchWorkspace.Visible)
            {
                FastSearchWorkspace.Hide();
                dpSearch.Visibility = DockVisibility.Visible;
                ListWorkspace.Dock = DockStyle.Fill;
            }
            else
            {
                FastSearchWorkspace.Show();
                dpSearch.Visibility = DockVisibility.Hidden;
                ListWorkspace.Dock = DockStyle.Fill;
            }
            ToolbarWorkspace.SendToBack();
            Refresh();
        }
    }
}
