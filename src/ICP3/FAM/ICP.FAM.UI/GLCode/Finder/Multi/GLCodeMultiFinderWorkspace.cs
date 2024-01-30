using System;
using System.ComponentModel;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.FAM.UI.GLCode.Finder
{
    [ToolboxItem(false)]
    public partial class GLCodeMultiFinderWorkspace : XtraUserControl
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        public GLCodeMultiFinderWorkspace()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {

                    Workitem.Workspaces.Remove(EditWorkspace);
                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Workspaces.Remove(ToolBarWorkspace);
                    Workitem.Workspaces.Remove(SelectedListWorkspace);
                    Workitem.Workspaces.Remove(SelectedToolBarWorkspace);

                    SearchWorkspace.PerformLayout();
                    EditWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();
                    ToolBarWorkspace.PerformLayout();
                    SelectedListWorkspace.PerformLayout();
                    SelectedToolBarWorkspace.PerformLayout();

                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    PerformLayout();

                }
            };

            if (LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            dpSelected.Text = "编辑";
            dpSearch.Text = "查询";
        }

        #endregion

        #region Workitem Common

        [CommandHandler(GLCodeCommandConstants.Command_GLCodeShowSearch)]
        public void Command_ShowSearch(object sender, EventArgs e)
        {
            if (dpSearch.Visibility == DockVisibility.Visible)
                dpSearch.Visibility = DockVisibility.Hidden;
            else
                dpSearch.Visibility = DockVisibility.Visible;
        }

        #endregion
    }
}

