using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;

namespace ICP.WF.Controls.Form.CustomerExpense
{
    [ToolboxItem(false)]
    public partial class WFCustomerExpenseToolBar : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public WFCustomerExpenseToolBar()
        {
            InitializeComponent();
            this.Disposed += delegate {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        #region IEditPart成员

        void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }
        #endregion

    }
}
