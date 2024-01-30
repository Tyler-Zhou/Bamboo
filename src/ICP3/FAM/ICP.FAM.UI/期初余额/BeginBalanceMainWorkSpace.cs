using System.ComponentModel;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class ImportUFBeginBalanceMainWorkSpace : XtraUserControl
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public ImportUFBeginBalanceMainWorkSpace()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Items.Remove(this);
                    ListWorkspace.PerformLayout();
                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };

        }

    }
}
