#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/3/14 星期三 19:08:26
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using ICP.FCM.OtherBusiness.UI.Common;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.FCM.OtherBusiness.UI.Business
{
    /// <summary>
    /// WorkSpace
    /// </summary>
    public class OBBMainWorkSpace : OBMainWorkspace
    {
        /// <summary>
        /// 显示查询面板
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        [CommandHandler(OBBCommandConstants.Command_ShowSearch)]
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
    }
}
