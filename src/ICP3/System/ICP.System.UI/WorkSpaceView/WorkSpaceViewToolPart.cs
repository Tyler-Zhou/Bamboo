using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Sys.UI.WorkSpaceView
{
    public partial class WorkSpaceViewToolPart : BaseToolBar
    {
        public WorkSpaceViewToolPart()
        {
            InitializeComponent();
        }
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        #endregion

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[WorkSpaceViewConstants.WorkSpaceView_Command_Refresh].Execute();
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.FindForm() != null)
            {
                this.FindForm().Close();
            }
        }
    }
}
