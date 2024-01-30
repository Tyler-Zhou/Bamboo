using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Microsoft.Practices.CompositeUI.WinForms;

namespace LongWin.DataWarehouseReport.WinUI
{
    /// <summary>
    /// 报表中心的WorkSpace,
    /// 共开放了 两个属性和一个 方法 
    /// 方法：DisplaySearchPart()-显示查询面板
    /// 属性：SearchSpace
    ///       ListSpace
    /// </summary>
    public partial class ReportMainSpace : UserControl
    {
        public ReportMainSpace()
        {
            InitializeComponent(); 
        }
        /// <summary>
        /// 显示查询面板
        /// </summary>
        public void DisplaySearchPart()
        {
            this.ultraDockManager1.PaneFromControl(this.tableLayoutPanel1).Show();
            this.ultraDockManager1.PaneFromControl(this.tableLayoutPanel1).Pinned = true;
        }

        /// <summary>
        /// 查询面板的显示区
        /// </summary>
        public DeckWorkspace SearchSpace
        {
            get { return this.workSpaceSearch; }
        }
        /// <summary>
        /// 内容显示区

        /// </summary>
        public DeckWorkspace ListSpace
        {
            get { return this.workSpaceList; }
        }

        private void ReportMainSpace_Load(object sender, EventArgs e)
        {

            tableLayoutPanel1.Text = Utility.GetValueString("查询", "查询");
        }

    }
}
