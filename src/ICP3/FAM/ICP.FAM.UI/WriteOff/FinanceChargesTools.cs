using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.FAM.UI.WriteOff
{
    public partial class FinanceChargesTools : BaseToolBar
    {
        public FinanceChargesTools()
        {
            InitializeComponent();
            Disposed += delegate {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            };
        }

        [ServiceDependency]
        private WorkItem Workitem
        {
            get;
            set;
        }


        public void SetService(WorkItem workitem)
        {
            Workitem = workitem;
        }

        private void barAdjustment_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_EditCurrencyRate].Execute();
        }

        private void barSelect_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_AutoBillsFinde].Execute();
        }

        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_DeleteBills].Execute();
        }
   
    }
}
