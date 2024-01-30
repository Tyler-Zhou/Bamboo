using System;
using System.ComponentModel;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.Commands;
using DevExpress.XtraBars.Docking;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class BankMainWorkSpace : XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        public BankMainWorkSpace()
        {
            InitializeComponent();
           dpSearch.Text= LocalData.IsEnglish?"Search":"查询";
           Disposed += delegate {

               if (Workitem != null)
               {
                   Workitem.Workspaces.Remove(ToolbarWorkspace);

                   Workitem.Workspaces.Remove(SearchWorkspace);
                   Workitem.Workspaces.Remove(BankAccountListWorkspace);
                   Workitem.Workspaces.Remove(ListWorkspace);
                   Workitem.Items.Remove(this);

                   SearchWorkspace.PerformLayout();
                   ToolbarWorkspace.PerformLayout();
                   ListWorkspace.PerformLayout();
                   BankAccountListWorkspace.PerformLayout();
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
