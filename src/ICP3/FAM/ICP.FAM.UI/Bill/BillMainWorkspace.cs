using System;
using System.ComponentModel;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.Bill
{
    [ToolboxItem(false)]
    public partial class BillMainWorkspace : XtraUserControl
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        #endregion

        #region 初始化
        public BillMainWorkspace()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ToolbarWorkspace);
                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Workspaces.Remove(OperationToolBarWorkspace);
                    Workitem.Workspaces.Remove(OperationListWorkspace);
                    Workitem.Items.Remove(this);

                    SearchWorkspace.PerformLayout();
                    ToolbarWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();
                    OperationToolBarWorkspace.PerformLayout();
                    OperationListWorkspace.PerformLayout();
                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };

            dockPanel1.Text = LocalData.IsEnglish ? "Search" : "查询";
        }
        #endregion

        #region Workitem Common
        [CommandHandler(BillCommandConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object sender, EventArgs e)
        {
            if (SearchWorkspace.Visible)
            {
                dockPanel1.Visibility = DockVisibility.Hidden;
            }
            else
            {
                dockPanel1.Visibility = DockVisibility.Visible;
            }
            Refresh();
        }

        [CommandHandler(BillCommandConstants.Command_ShowSelected)]
        public void Command_ShowSelected(object sender, EventArgs e)
        {
            splitContainerControl1.Collapsed = !splitContainerControl1.Collapsed;
        }

        #endregion
    }
}
