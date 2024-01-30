using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.WF.Controls.Form.CustomerExpense
{
    [ToolboxItem(false)]
    public partial class WFCustomerExpenseMainWorkspace : BasePart
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public WFCustomerExpenseMainWorkspace()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.ToolbarWorkspace);

                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.TouchListWorkspace);
                    Workitem.Workspaces.Remove(this.ListWorkspace);
                    Workitem.Workspaces.Remove(this.CommissionLogWorkspace);

                    this.SearchWorkspace.PerformLayout();
                    this.ToolbarWorkspace.PerformLayout();
                    this.ListWorkspace.PerformLayout();
                    this.TouchListWorkspace.PerformLayout();
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
