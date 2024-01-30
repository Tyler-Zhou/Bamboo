using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;

namespace ICP.WF.Controls.Form.Commission
{
    [ToolboxItem(false)]
    public partial class WFBusinessToolBar : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public WFBusinessToolBar()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            };
        }


        #region IEditPart成员

        void barSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[WFBusinessCommandConstants.Command_Select].Execute();
        }

        void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }
        #endregion

    }
}
