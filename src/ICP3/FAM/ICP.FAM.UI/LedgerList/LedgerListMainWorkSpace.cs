using System;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraBars.Docking;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI
{
    public partial class LedgerListMainWorkSpace : XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public LedgerListMainWorkSpace()
        {
            InitializeComponent();
            dpSearch.Text = LocalData.IsEnglish ? "Search" : "查询";
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ToolbarWorkspace);

                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);
                   
                    Workitem.Items.Remove(this);

                    SearchWorkspace.PerformLayout();
                    ToolbarWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();
                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }

            };
        }

        [CommandHandler(LedgerListCommandConstants.CommandShowSearch)]
        public void Command_ShowSearch(object o, EventArgs e)
        {
            if (dpSearch.Visibility == DockVisibility.Hidden)
                dpSearch.Visibility = DockVisibility.Visible;
            else
                dpSearch.Visibility = DockVisibility.Hidden;
            ToolbarWorkspace.SendToBack();
            Refresh();
        }
    }
}
