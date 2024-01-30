using System;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraBars.Docking;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI.BankReceiptList
{
    public partial class BankReceiptListMainWorkSpace : XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public BankReceiptListMainWorkSpace()
        {
            InitializeComponent();
            dpSearch.Text = LocalData.IsEnglish ? "Search" : "查询";
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(BankReceiptList_ToolbarWorkspace);

                    Workitem.Workspaces.Remove(BankReceiptList_SearchWorkspace);
                    Workitem.Workspaces.Remove(BankReceiptList_ListWorkspace);
                   
                    Workitem.Items.Remove(this);

                    BankReceiptList_SearchWorkspace.PerformLayout();
                    BankReceiptList_ToolbarWorkspace.PerformLayout();
                    BankReceiptList_ListWorkspace.PerformLayout();
                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }

            };
        }

        [CommandHandler(BankReceiptListCommandConstants.CommandShowSearch)]
        public void Command_ShowSearch(object o, EventArgs e)
        {
            if (dpSearch.Visibility == DockVisibility.Hidden)
                dpSearch.Visibility = DockVisibility.Visible;
            else
                dpSearch.Visibility = DockVisibility.Hidden;
            BankReceiptList_ToolbarWorkspace.SendToBack();
            Refresh();
        }
    }
}
