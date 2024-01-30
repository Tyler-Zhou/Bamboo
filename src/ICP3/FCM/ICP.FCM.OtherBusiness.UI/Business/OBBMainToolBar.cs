#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/3/14 星期三 18:47:39
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using DevExpress.XtraBars;
using ICP.FCM.OtherBusiness.UI.Common;

namespace ICP.FCM.OtherBusiness.UI.Business
{
    /// <summary>
    /// 其他业务-工具栏
    /// </summary>
    public class OBBMainToolBar : OBMainToolBar
    {
        /// <summary>
        /// 其他业务-工具栏
        /// </summary>
        public OBBMainToolBar()
        {
            barVerifiSheet.Visibility = BarItemVisibility.Always;
            barPickUp.Visibility = BarItemVisibility.Always;
            BulidCommond();
        }

        /// <summary>
        /// 构建Command
        /// </summary>
        private void BulidCommond()
        {
            barAdd.ItemClick += delegate
            {
                Workitem.Commands[OBBCommandConstants.Command_AddData].Execute();
            };
            barRefresh.ItemClick += delegate { Workitem.Commands[OBBCommandConstants.Command_RefreshData].Execute(); };
            barEdit.ItemClick += delegate { Workitem.Commands[OBBCommandConstants.Command_EditData].Execute(); };
            barCopy.ItemClick += delegate { Workitem.Commands[OBBCommandConstants.Command_CopyData].Execute(); };
            barCancel.ItemClick += delegate { Workitem.Commands[OBBCommandConstants.Command_CancelData].Execute(); };
            barOpContact.ItemClick += delegate { Workitem.Commands[OBBCommandConstants.Command_OpContact].Execute(); };
            barProfit.ItemClick += delegate { Workitem.Commands[OBBCommandConstants.Command_Profit].Execute(); };
            barSearch.ItemClick += delegate { Workitem.Commands[OBBCommandConstants.Command_ShowSearch].Execute(); };
            barBill.ItemClick += delegate { Workitem.Commands[OBBCommandConstants.Command_Bill].Execute(); };
            barVerifiSheet.ItemClick += delegate { Workitem.Commands[OBBCommandConstants.Command_VerifiSheet].Execute(); };
            barPickUp.ItemClick += delegate { Workitem.Commands[OBBCommandConstants.Command_PickUp].Execute(); };
            barClose.ItemClick += delegate
            {
                var findForm = FindForm();
                if (findForm != null) findForm.Close();
            };
        }
    }
}
