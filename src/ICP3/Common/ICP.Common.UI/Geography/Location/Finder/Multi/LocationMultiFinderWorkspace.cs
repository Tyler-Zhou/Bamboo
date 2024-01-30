using System;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.Geography.Location
{
    [ToolboxItem(false)]
    public partial class LocationMultiFinderWorkspace : ICP.Framework.ClientComponents.UIFramework.BasePart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        #endregion

        #region init

        public LocationMultiFinderWorkspace()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.ListWorkspace);
                    Workitem.Workspaces.Remove(this.ToolBarWorkspace);
                    Workitem.Workspaces.Remove(this.SelectedListWorkspace);
                    Workitem.Workspaces.Remove(this.SelectedToolBarWorkspace);
                    Workitem.Items.Remove(this);

                    this.ListWorkspace.PerformLayout();
                    this.SearchWorkspace.PerformLayout();
                    this.ToolBarWorkspace.PerformLayout();
                    this.ListWorkspace.PerformLayout();
                    this.SelectedListWorkspace.PerformLayout();
                    this.SelectedToolBarWorkspace.PerformLayout();
                

                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    this.PerformLayout();
                }
            };

            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            dpSearch.Text = LocalData.IsEnglish ? "Search" : "查询";
        }

        #endregion

        #region Workitem Common

        [CommandHandler(LocationCommonConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object sender, EventArgs e)
        {
            if (dpSearch.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible)
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            else
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }

        #endregion
    }
}
