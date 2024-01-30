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
    public partial class InvoiceMainWorkSpace : XtraUserControl
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public InvoiceMainWorkSpace()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ToolbarWorkspace);
                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Workspaces.Remove(DocumentListWorkspace);
                    Workitem.Items.Remove(this);

                    SearchWorkspace.PerformLayout();
                    ToolbarWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();
                    DocumentListWorkspace.PerformLayout();

                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };

            dpSearch.Text = LocalData.IsEnglish ? "Search" : "查询";
        }

        [CommandHandler(InvoiceCommandConstants.Command_InvoiceShowSearch)]
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
