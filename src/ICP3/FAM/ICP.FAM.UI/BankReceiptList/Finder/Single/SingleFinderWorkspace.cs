using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.ComponentModel;

namespace ICP.FAM.UI.BankReceiptList.Finder
{
    [ToolboxItem(false)]
    public partial class SingleFinderWorkspace : BasePart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        #endregion

        #region init

        public SingleFinderWorkspace()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(SingleFinder_Search_BankReceipt);
                    Workitem.Workspaces.Remove(SingleFinder_List_BankReceipt);
                    Workitem.Workspaces.Remove(SingleFinder_ToolBar_BankReceipt);

                    SingleFinder_Search_BankReceipt.PerformLayout();
                    SingleFinder_ToolBar_BankReceipt.PerformLayout();
                    SingleFinder_List_BankReceipt.PerformLayout();
                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };
        }

        #endregion

        #region Workitem Common

        [CommandHandler(BankReceiptFinderConstants.COMMANDSINGLEFINDERSHOWSEARCH)]
        public void Command_ShowSearch(object sender, EventArgs e)
        {
            if (dpSearch.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible)
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            else
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }

        #endregion
    }
}
