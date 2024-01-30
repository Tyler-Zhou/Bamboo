using System;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.ReportCenter.UI
{
    /// <summary>
    /// 报表中心的WorkSpace,
    /// 共开放了 两个属性和一个 方法 
    /// 方法：DisplaySearchPart()-显示查询面板
    /// 属性：SearchSpace
    ///       ListSpace
    /// </summary>
    [ToolboxItem(false)]
    public partial class ReportMainSpace : BasePart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init 

        public ReportMainSpace()
        {
            InitializeComponent();
            
            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.ReportWorkspace);
      
                    this.SearchWorkspace.PerformLayout();
                    this.ReportWorkspace.PerformLayout();
                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    this.PerformLayout();
                }
            };
        }

        #endregion

        #region 接口
        [CommandHandler(CommandConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object sender, EventArgs e)
        {
            if (SearchWorkspace.Visible)
            {
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            }
            else
            {
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            }
            this.Refresh();
        }

       ///  <summary>
        /// 查询面板的显示区
        /// </summary>
        public DeckWorkspace SearchSpace
        {
            get { return this.SearchWorkspace; }
        }

        /// <summary>
        /// 内容显示区
        /// </summary>
        public DeckWorkspace ListSpace
        {
            get { return this.ReportWorkspace; }
        }

        #endregion

    }
}
