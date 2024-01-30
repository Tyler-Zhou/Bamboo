#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/3/13 星期二 14:10:18
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using DevExpress.XtraBars;
using ICP.FCM.Common.ServiceInterface.Common;
using ICP.FCM.OtherBusiness.UI.Common;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.OtherBusiness.UI.Order
{
    /// <summary>
    /// 其他业务订单工具栏
    /// </summary>
    public class OBOrderMainToolBar : OBMainToolBar
    {
        /// <summary>
        /// 是否隐藏快速搜索栏
        /// </summary>
        protected override bool IsHidebarVerifiSheet { get { return true; } }

        /// <summary>
        /// 其他业务-工具栏
        /// </summary>
        public OBOrderMainToolBar()
        {
            barVerifiSheet.Visibility = BarItemVisibility.Never;
            barPickUp.Visibility = BarItemVisibility.Never;
            BulidCommond();
            SetPermissions();
        }

        /// <summary>
        /// 构建Command
        /// </summary>
        private void BulidCommond()
        {
            barAdd.ItemClick += delegate { Workitem.Commands[OBOCommandConstants.Command_AddData].Execute(); };
            barRefresh.ItemClick += delegate { Workitem.Commands[OBOCommandConstants.Command_RefreshData].Execute(); };
            barEdit.ItemClick += delegate { Workitem.Commands[OBOCommandConstants.Command_EditData].Execute(); };
            barCopy.ItemClick += delegate { Workitem.Commands[OBOCommandConstants.Command_CopyData].Execute(); };
            barCancel.ItemClick += delegate { Workitem.Commands[OBOCommandConstants.Command_CancelData].Execute(); };
            barOpContact.ItemClick += delegate { Workitem.Commands[OBOCommandConstants.Command_OpContact].Execute(); };
            barProfit.ItemClick += delegate { Workitem.Commands[OBOCommandConstants.Command_Profit].Execute(); };
            barSearch.ItemClick += delegate { Workitem.Commands[OBOCommandConstants.Command_ShowSearch].Execute(); };
            barBill.ItemClick += delegate { Workitem.Commands[OBOCommandConstants.Command_Bill].Execute(); };
            barPickUp.ItemClick += delegate { Workitem.Commands[OBOCommandConstants.Command_PickUp].Execute(); };
            barClose.ItemClick += delegate
            {
                var findForm = FindForm();
                if (findForm != null) findForm.Close();
            };
        }

        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            if (!LocalCommonServices.PermissionService.HaveActionPermission(FCMPermissionsConstants.FCM_EditOrder))
            {
                barAdd.Visibility = BarItemVisibility.Never;
                barCopy.Visibility = BarItemVisibility.Never;
                barCancel.Visibility = BarItemVisibility.Never;
                barBill.Visibility = BarItemVisibility.Never;
                barVerifiSheet.Visibility = BarItemVisibility.Never;
                barPickUp.Visibility = BarItemVisibility.Never;
                barRemark.Visibility = BarItemVisibility.Never;
            }
        }
    }
}
