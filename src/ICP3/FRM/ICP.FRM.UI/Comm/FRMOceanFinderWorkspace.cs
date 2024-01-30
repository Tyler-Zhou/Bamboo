using System;
using System.ComponentModel;
using DevExpress.XtraBars.Docking;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.Geography.Location
{
    [ToolboxItem(false)]
    public partial class FRMOceanFinderWorkspace : BasePart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        #endregion

        #region init

        public FRMOceanFinderWorkspace()
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
                   
                    ListWorkspace.PerformLayout();
                    ToolBarWorkspace.PerformLayout();
                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };

            if (LocalData.IsEnglish == false) SetCnText();
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
            if (dpSearch.Visibility == DockVisibility.Visible)
                dpSearch.Visibility = DockVisibility.Hidden;
            else
                dpSearch.Visibility = DockVisibility.Visible;
        }

        #endregion


    }
}
