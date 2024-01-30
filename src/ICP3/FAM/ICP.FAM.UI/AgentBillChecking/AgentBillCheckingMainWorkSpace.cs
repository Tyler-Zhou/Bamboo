using System;
using System.ComponentModel;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.Commands;
using DevExpress.XtraBars.Docking;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class AgentBillCheckingMainWorkSpace : XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public AgentBillCheckingMainWorkSpace()
        {
            InitializeComponent();
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

            dpSearch.Text = LocalData.IsEnglish ? "Search" : "查询";
        }



        [CommandHandler(ABCCommandConstants.Command_ABCShowSearch)]
        public void Command_ShowSearch(object o, EventArgs e)
        {
            if (dpSearch.Visibility == DockVisibility.Hidden)
            {
                dpSearch.Visibility = DockVisibility.Visible;
            }
            else
            {
                dpSearch.Visibility = DockVisibility.Hidden;
            }
            ToolbarWorkspace.SendToBack();
            Refresh();
        }
      

    }
}
