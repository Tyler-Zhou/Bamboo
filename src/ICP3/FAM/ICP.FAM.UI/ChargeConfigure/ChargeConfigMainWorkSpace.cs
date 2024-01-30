using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI.ChargeConfigure
{
    public partial class ChargeConfigMainWorkSpace : XtraUserControl
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public ChargeConfigMainWorkSpace()
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
