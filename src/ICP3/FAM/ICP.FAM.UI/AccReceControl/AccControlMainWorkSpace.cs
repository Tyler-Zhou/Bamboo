using System;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI.Commands;
using DevExpress.XtraBars.Docking;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FAM.UI.AccReceControl
{
    [ToolboxItem(false)]
    public partial class AccControlMainWorkSpace : BasePart
    {
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }

        public AccControlMainWorkSpace()
        {
            InitializeComponent();
            dpSearch.Text = LocalData.IsEnglish ? "Search" : "查询";
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ToolbarWorkspace);
                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(LogListWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Items.Remove(this);

                    SearchWorkspace.PerformLayout();
                    ToolbarWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();
                    LogListWorkspace.PerformLayout();
                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }

            };
        }

        [CommandHandler(BankCommandConstants.Command_BankShowSearch)]
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
