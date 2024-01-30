using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.CustomerManager
{
    [ToolboxItem(false)]
    public partial class FindInvoiceTitleMainWorkSpace : DevExpress.XtraEditors.XtraUserControl
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public FindInvoiceTitleMainWorkSpace()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.FindInvoiceTitleSearchWorkspace);
                    Workitem.Workspaces.Remove(this.FindInvoiceTitleListWorkspace);
                    Workitem.Items.Remove(this);

                    this.FindInvoiceTitleSearchWorkspace.PerformLayout();
                    this.FindInvoiceTitleListWorkspace.PerformLayout();
                    this.PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };

            dpSearch.Text = LocalData.IsEnglish ? "Search" : "查询";
        }
    }
}
