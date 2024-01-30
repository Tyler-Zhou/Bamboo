using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FRM.UI.ProfitRatios
{   
    /// <summary>
    /// 利润配比调整布局页
    /// </summary>
    [ToolboxItem(false)]
    public partial class PRMainWorkspace : BasePart
    {
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PRMainWorkspace()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
           
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.ToolBarWorkspace);

                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.FastSearchWorkspace);
                    Workitem.Workspaces.Remove(this.ListWorkspace);
                    Workitem.Workspaces.Remove(this.ProfitRatiosWorkspace);
                   

                    this.SearchWorkspace.PerformLayout();
                    this.ToolBarWorkspace.PerformLayout();
                    this.ListWorkspace.PerformLayout();
                    this.ProfitRatiosWorkspace.PerformLayout();
                    this.FastSearchWorkspace.PerformLayout();
                  
                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    this.PerformLayout();
                   
                }
            };

            if (!LocalData.IsDesignMode && !LocalData.IsEnglish)
            {
                SetCnText();
            }
            
        }

        private void SetCnText()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                FastSearchWorkspace.Hide();
                ListWorkspace.Dock = DockStyle.Fill;
                ToolBarWorkspace.SendToBack();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        [CommandHandler(ProfitRatiosCommandConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object o, EventArgs e)
        {
            if (FastSearchWorkspace.Visible)
            {
                FastSearchWorkspace.Hide();
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                this.ListWorkspace.Dock = DockStyle.Fill;
                SearchWorkspace.Show();
            }
            else
            {
                SearchWorkspace.Hide();
                FastSearchWorkspace.Show();
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                this.ListWorkspace.Dock = DockStyle.Fill;
            }
            ToolBarWorkspace.SendToBack();
            this.Refresh();
        }
    }
}
