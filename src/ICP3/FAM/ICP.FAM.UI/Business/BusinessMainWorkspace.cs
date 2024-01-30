using System;
using System.ComponentModel;
using DevExpress.XtraBars.Docking;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.Business
{
    [ToolboxItem(false)]
    public partial class BusinessMainWorkspace : BasePart
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public BusinessMainWorkspace()
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

            dockPanel1.Text = LocalData.IsEnglish ? "Search" : "查询";
        }

        [CommandHandler(BusinessCommandConstants.Command_ShowSearch)]
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
    }
}
