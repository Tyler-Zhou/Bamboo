using System;
using System.ComponentModel;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.Commands;
using DevExpress.XtraBars.Docking;
using Microsoft.Practices.CompositeUI;

namespace ICP.FRM.UI.BookingReport
{
    [ToolboxItem(false)]
    public partial class BookingReportMainWorkspace : XtraUserControl
    {
        public BookingReportMainWorkspace()
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
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        #endregion

        #region 显示/关闭查询面板
        [CommandHandler(BookingReportCommonConstants.Command_ShowSearch)]
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
        #endregion


    }
}
