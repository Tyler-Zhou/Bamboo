#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/3/15 星期四 16:58:49
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.FCM.OtherBusiness.UI.Common;
using DevExpress.XtraBars.Docking;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.FCM.OtherBusiness.UI.Order
{
    /// <summary>
    /// 其他业务-订单
    /// </summary>
    [ToolboxItem(false)]
    public class OBOrderMainWorkSpace : OBMainWorkspace
    {
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public new WorkItem Workitem { get; set; }
        /// <summary>
        /// 其他业务-订单
        /// </summary>
        public OBOrderMainWorkSpace()
        {
        }
        #region

        /// <summary>
        /// 显示查询面板
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        [CommandHandler(OBOCommandConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object o, EventArgs e)
        {
            if (FastSearchWorkspace.Visible)
            {
                FastSearchWorkspace.Hide();
                dpSearch.Visibility = DockVisibility.Visible;
                ListWorkspace.Dock = DockStyle.Fill;
            }
            else
            {
                FastSearchWorkspace.Show();
                dpSearch.Visibility = DockVisibility.Hidden;
                ListWorkspace.Dock = DockStyle.Fill;
            }
            ToolbarWorkspace.SendToBack();
            Refresh();
        }

        #endregion
    }
}
