using System.ComponentModel;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;

namespace ICP.FRM.UI.InquireRates
{
    [ToolboxItem(false)]
    public partial class InquireAirRatesMainWorkspace : XtraUserControl
    {
        public InquireAirRatesMainWorkspace()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Workspaces.Remove(CommunicationHistoryWorkspace);
                    Workitem.Workspaces.Remove(GeneralInfoWorkspace);
                    Workitem.Items.Remove(this);

                    ListWorkspace.PerformLayout();
                    CommunicationHistoryWorkspace.PerformLayout();
                    GeneralInfoWorkspace.PerformLayout();


                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    PerformLayout();
                }
            };
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion      
    }
}
