using System;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.OA.UI.Bulletin
{
    [ToolboxItem(false)]
    public partial class BulletinMainWorkspace : BasePart
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public BulletinMainWorkspace()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.ToolBarWorkspace);
                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.ListWorkspace);
                    

                    this.ToolBarWorkspace.PerformLayout();
                    this.ToolBarWorkspace.Dispose();
                    this.ToolBarWorkspace = null;

                    this.SearchWorkspace.PerformLayout();
                    this.SearchWorkspace.Dispose();
                    this.SearchWorkspace = null;
                    this.ListWorkspace.PerformLayout();
                    this.ListWorkspace.Dispose();
                    this.ListWorkspace = null;
                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    this.PerformLayout();
                }
            };
            if (!LocalData.IsDesignMode) { InitMessage(); }
        }

        private void InitMessage()
        {
            this.RegisterMessage("Titel", LocalData.IsEnglish ? "Bulletin List" : "公告列表");
        }

        [CommandHandler(BulletinCommonConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object sender, EventArgs e)
        {
            if (SearchWorkspace.Visible)
            {
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            }
            else
            {
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            }
            this.Refresh();
        }

    }
}
