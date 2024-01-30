using System;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.WF.Controls.Form.Commission
{
    [ToolboxItem(false)]
    public partial class WFBusinessMainWorkspace : BasePart
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public WFBusinessMainWorkspace()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {  
                    Workitem.Workspaces.Remove(this.ToolbarWorkspace);

                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.SelectListWorkspace);
                    Workitem.Workspaces.Remove(this.ListWorkspace);
                    Workitem.Workspaces.Remove(this.CommissionLogWorkspace);
            
                    this.SearchWorkspace.PerformLayout();
                    this.ToolbarWorkspace.PerformLayout();
                    this.ListWorkspace.PerformLayout();
                    this.SelectListWorkspace.PerformLayout();
                    this.CommissionLogWorkspace.PerformLayout();

                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    this.PerformLayout();
                }
            };

            dockPanel1.Text = LocalData.IsEnglish ? "Search" : "查询";
        }

    }
}
