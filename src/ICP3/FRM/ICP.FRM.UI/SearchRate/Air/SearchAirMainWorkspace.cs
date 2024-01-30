using System;
using System.ComponentModel;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI;

namespace ICP.FRM.UI.SearchRate
{
    [ToolboxItem(false)]
    public partial class SearchAirMainWorkspace : XtraUserControl
    {
        public SearchAirMainWorkspace()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ToolbarWorkspace);
                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Workspaces.Remove(BaseInfoWorkspace);
                    Workitem.Items.Remove(this);
                    ToolbarWorkspace.PerformLayout();
                    SearchWorkspace.PerformLayout();
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

        [CommandHandler(SearchAirCommandConstants.Command_ShowSearch)]
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
