using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.ComponentModel;

namespace ICP.FAM.UI.BankTransaction.Finder
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
                    Workitem.Workspaces.Remove(SingleFinder_Search_BankTransaction);
                    Workitem.Workspaces.Remove(SingleFinder_List_BankTransaction);
                    Workitem.Workspaces.Remove(SingleFinder_ToolBar_BankTransaction);

                    SingleFinder_Search_BankTransaction.PerformLayout();
                    SingleFinder_ToolBar_BankTransaction.PerformLayout();
                    SingleFinder_List_BankTransaction.PerformLayout();
                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };
        }

        #endregion

        #region Workitem Common

        [CommandHandler(BankTransactionFinderConstants.COMMANDSINGLEFINDERSHOWSEARCH)]
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
