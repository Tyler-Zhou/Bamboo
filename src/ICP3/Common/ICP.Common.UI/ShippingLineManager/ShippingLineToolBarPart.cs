using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.Common.UI.ShippingLineManager
{
    public partial class ShippingLineToolBarPart : BaseToolBar
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public ShippingLineToolBarPart()
        {
            InitializeComponent();
            BulidCommond();
        }


        private void BulidCommond() 
        {
            barAdd.ItemClick += delegate { Workitem.Commands[ShippingLineCommandConstants.Command_AddData].Execute(); };
            barInvalid.ItemClick += delegate { Workitem.Commands[ShippingLineCommandConstants.Command_Invalid].Execute(); };
            this.barClose.ItemClick += new ItemClickEventHandler(barClose_ItemClick);
        }

        void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            var findForm = this.FindForm();
            if (findForm != null) findForm.Close();
        }
    }
}
