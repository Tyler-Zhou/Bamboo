using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.FCM.OtherBusiness.UI.Common;
using DevExpress.XtraBars.Docking;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.FCM.OtherBusiness.UI.ECommerce
{
    /// <summary>
    /// 其他业务-电商物流
    /// </summary>
    [ToolboxItem(false)]
    public class OBECMainWorkSpace : OBMainWorkspace
    {
        /// <summary>
        /// 其他业务-电商物流
        /// </summary>
        [ServiceDependency]
        public new WorkItem Workitem { get; set; }
        /// <summary>
        /// 其他业务-电商物流
        /// </summary>
        public OBECMainWorkSpace()
        {
        }
        #region

        /// <summary>
        /// 显示查询面板
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        [CommandHandler(OBECCommandConstants.Command_ShowSearch)]
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

