using DevExpress.XtraBars;
using ICP.FCM.OtherBusiness.UI.Common;
using Microsoft.Practices.CompositeUI;
using System.ComponentModel;

namespace ICP.FCM.OtherBusiness.UI.ECommerce
{
    /// <summary>
    /// 电商物流工具栏
    /// </summary>
    [ToolboxItem(false)]
    public class OBECMainToolBar : OBMainToolBar
    {

        #region Service
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public new WorkItem Workitem { get; set; }

        #endregion


        /// <summary>
        /// 电商物流工具栏
        /// </summary>
        public OBECMainToolBar()
        {
            barVerifiSheet.Visibility = BarItemVisibility.Never;
            barPickUp.Visibility = BarItemVisibility.Never;

            BulidCommond();
            SetPermissions();
        }

        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            barPrint.Visibility = BarItemVisibility.Never;
            barVerifiSheet.Visibility = BarItemVisibility.Never;
            barPickUp.Visibility = BarItemVisibility.Never;
            barRemark.Visibility = BarItemVisibility.Never;
        }
        private void BulidCommond()
        {
            barAdd.ItemClick += delegate { Workitem.Commands[OBECCommandConstants.Command_AddData].Execute(); };
            barRefresh.ItemClick += delegate { Workitem.Commands[OBECCommandConstants.Command_RefreshData].Execute(); };
            barEdit.ItemClick += delegate { Workitem.Commands[OBECCommandConstants.Command_EditData].Execute(); };
            barCopy.ItemClick += delegate { Workitem.Commands[OBECCommandConstants.Command_CopyData].Execute(); };
            barCancel.ItemClick += delegate { Workitem.Commands[OBECCommandConstants.Command_CancelData].Execute(); };
            barOpContact.ItemClick += delegate { Workitem.Commands[OBECCommandConstants.Command_OpContact].Execute(); };
            barProfit.ItemClick += delegate { Workitem.Commands[OBECCommandConstants.Command_Profit].Execute(); };
            barSearch.ItemClick += delegate { Workitem.Commands[OBECCommandConstants.Command_ShowSearch].Execute(); };
            barBill.ItemClick += delegate { Workitem.Commands[OBECCommandConstants.Command_Bill].Execute(); };
            barVerifiSheet.ItemClick += delegate { Workitem.Commands[OBECCommandConstants.Command_VerifiSheet].Execute(); };
            barPickUp.ItemClick += delegate { Workitem.Commands[OBECCommandConstants.Command_PickUp].Execute(); };
            barClose.ItemClick += delegate { FindForm().Close(); };
        }
    }
}
