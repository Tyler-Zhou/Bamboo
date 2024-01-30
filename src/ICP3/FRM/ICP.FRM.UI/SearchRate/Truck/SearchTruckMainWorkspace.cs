using System;
using System.ComponentModel;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI;

namespace ICP.FRM.UI.SearchRate
{
    [ToolboxItem(false)]
    public partial class SearchTruckMainWorkspace : XtraUserControl
    {
        public SearchTruckMainWorkspace()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {

                    Workitem.Workspaces.Remove(ToolbarWorkspace);

                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(BaseInfoWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);
              
                    Workitem.Items.Remove(this);
                    SearchWorkspace.PerformLayout();
                    ToolbarWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();
                    BaseInfoWorkspace.PerformLayout();
                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        #endregion

        [CommandHandler(SearchTruckCommandConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object sender, EventArgs e)
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
    }
}
