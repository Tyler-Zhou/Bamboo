using System;
using System.ComponentModel;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraTab;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.ReleaseRC
{
    [ToolboxItem(false)]
    public partial class ReleaseRCMainWorkspace : BasePart
    {
        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public ReleaseRCMainWorkspace()
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

            dpSearch.Text = LocalData.IsEnglish ? "Search" : "查询";
        }

        [CommandHandler(ReleaseRCCommondConstants.Commond_ShowSearch)]
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
            Workitem.Commands[ReleaseRCCommondConstants.Command_ChangedTab].Execute();
        }
    }
}
