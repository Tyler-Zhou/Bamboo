using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.Common.UI.Configure.ChargingCode
{
    [ToolboxItem(false)]
    public partial class ChargingCodeMultiFinderSelectedToolBar : ICP.Framework.ClientComponents.UIFramework.BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        public ChargingCodeMultiFinderSelectedToolBar()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {   
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        #endregion

        #region barItem

        private void barRemoveAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[ChargingCodeCommonConstants.Common_FinderRemoveAll].Execute();
        }

        private void barRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[ChargingCodeCommonConstants.Common_FinderRemove].Execute();
        }

        private void barConfirm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[ChargingCodeCommonConstants.Common_FinderConfirm].Execute();
            var findForm = this.FindForm();
            if (findForm != null) findForm.Close();
        }

        #endregion

        #region BaseEdit成员

        public override object DataSource
        {
            get
            {
                return null;
            }
            set
            {
                SolutionChargingCodeList data = value as SolutionChargingCodeList;
                if (data == null) barRemove.Enabled =barRemoveAll.Enabled = false;
                else barRemove.Enabled =barRemoveAll.Enabled = true;
            }
        }

        #endregion
    }
}
