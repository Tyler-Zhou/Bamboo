using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.AirExport.UI.Booking
{
    [ToolboxItem(false)]
    public partial class BookingMainWorkspace : XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public BookingMainWorkspace()
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
                    Workitem.Workspaces.Remove(EventListWorkspace);
                    Workitem.Workspaces.Remove(DocumentListWorkspace);
                    Workitem.Workspaces.Remove(FaxMailEDIListWorkspace);
                
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
            if (LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            dpSearch.Text = "查询";
            tabDocumentList.Text = LocalData.IsEnglish ? "Document List" : "文档列表";
            tabFaxMailList.Text = LocalData.IsEnglish ? "Fax/Mail/EDI" : "传真/邮件/EDI";
            tabMemoList.Text = LocalData.IsEnglish ? "Event" : "事件";
        }

        protected override void OnLoad(EventArgs e)
        {
            FastSearchWorkspace.Show();
            dpSearch.Visibility = DockVisibility.Hidden;
            ListWorkspace.Dock = DockStyle.Fill;

            base.OnLoad(e);
            ToolbarWorkspace.SendToBack();
        }

        [CommandHandler(AEBookingCommandConstants.Command_ShowSearch)]
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
