using System;
using System.ComponentModel;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraTab;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.ReleaseBL
{
    [ToolboxItem(false)]
    public partial class ReleaseBLMainWorkspace : BasePart
    {
        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public ReleaseBLMainWorkspace()
        {
            InitializeComponent();

            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ToolbarWorkspace);
                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);

                    Workitem.Workspaces.Remove(EventWorkspace);
                    Workitem.Workspaces.Remove(NextJobsWorkspace);
                    Workitem.Workspaces.Remove(BillListWorkspace);
                    Workitem.Workspaces.Remove(DebtWorkspace);
                    Workitem.Workspaces.Remove(ContactListspace);
                    Workitem.Workspaces.Remove(FaxMailEDIListWorkspace);
                    Workitem.Items.Remove(this);

                    SearchWorkspace.PerformLayout();
                    ToolbarWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();
                    EventWorkspace.PerformLayout();
                    NextJobsWorkspace.PerformLayout();
                    BillListWorkspace.PerformLayout();
                    ContactListspace.PerformLayout();

                    BillListWorkspace.PerformLayout();
                    ContactListspace.PerformLayout();
                    FaxMailEDIListWorkspace.PerformLayout();

                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;

                }
            };

            dpSearch.Text = LocalData.IsEnglish ? "Search" : "查询";
            tabMemo.Text = LocalData.IsEnglish ? "Event List" : "事件列表";
            


        }

        [CommandHandler(ReleaseBLCommondConstants.Commond_ShowSearch)]
        public void Command_ShowSearch(object o, EventArgs e)
        {
            if (SearchWorkspace.Visible)
            {
                dpSearch.Visibility = DockVisibility.Hidden;
            }
            else
            {
                dpSearch.Visibility = DockVisibility.Visible;
            }
            Refresh();
        }

        private void xtab_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            Workitem.Commands[ReleaseBLCommondConstants.Command_ChangedTab].Execute();
        }
    }
}
