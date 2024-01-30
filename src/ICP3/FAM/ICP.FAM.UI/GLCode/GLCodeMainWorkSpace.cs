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
    public partial class GLCodeMainWorkSpace : XtraUserControl
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        #endregion

        public GLCodeMainWorkSpace()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Workspaces.Remove(ToolBarWorkspace);
                    Workitem.Items.Remove(this);

                    SearchWorkspace.PerformLayout();
                    ToolBarWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();

                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };

            dpSearch.Text = LocalData.IsEnglish ? "Search" : "查询";
        }
        [CommandHandler(GLCodeCommandConstants.Command_GLCodeShowSearch)]
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
            ToolBarWorkspace.SendToBack();
            Refresh();
        }


    }
}
