using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using System.ComponentModel;

namespace ICP.FAM.UI.BankTransaction
{
    [ToolboxItem(false)]
    public partial class MainWorkspace : BasePart
    {
        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public MainWorkspace()
        {
            InitializeComponent();

            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(BankTransactionToolbarWorkspace);
                    Workitem.Workspaces.Remove(BankTransactionSearchWorkspace);
                    Workitem.Workspaces.Remove(BankTransactionListWorkspace);
                    Workitem.Workspaces.Remove(BankTransactionEditWorkspace);
                    Workitem.Items.Remove(this);
                    BankTransactionSearchWorkspace.PerformLayout();
                    BankTransactionToolbarWorkspace.PerformLayout();
                    BankTransactionListWorkspace.PerformLayout();
                    BankTransactionEditWorkspace.PerformLayout();

                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                   
                }
            };

            dpSearch.Text = LocalData.IsEnglish ? "Search" : "查询";
        }
    }
}
