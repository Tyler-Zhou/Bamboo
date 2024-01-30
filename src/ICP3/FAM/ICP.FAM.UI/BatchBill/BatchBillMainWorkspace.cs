using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.FAM.UI.BatchBill
{
    /// <summary>
    /// 编辑账单界面WorkSpace
    /// </summary>
    [ToolboxItem(false)]
    public partial class BatchBillMainWorkspace : BasePart
    {
        #region Services
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; } 
        #endregion

        #region Init
        /// <summary>
        /// 
        /// </summary>
        public BatchBillMainWorkspace()
        {
            InitializeComponent();
            RegisterEvent();
            Disposed += (sender,arg)=>
            {
                UnRegisterEvent();
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Items.Remove(this);
                    ListWorkspace.PerformLayout();
                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                FastSearchWorkspace.Show();
                dpSearch.Visibility = DockVisibility.Hidden;
                ListWorkspace.Dock = DockStyle.Fill;

                ToolbarWorkspace.SendToBack();
                if (!LocalData.IsEnglish)
                {
                    SetCnText();
                }
            }
        } 
        #endregion

        #region CommandHandler
        [CommandHandler(BatchBillCommandConstants.Command_ShowSearch)]
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

        #region Custom Method
        /// <summary>
        /// 设置中文
        /// </summary>
        private void SetCnText()
        {
            dpSearch.Text = "查询";
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        void RegisterEvent()
        {
            dpSearch.ClosedPanel += dpSearch_ClosedPanel;
        }

        void dpSearch_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            FastSearchWorkspace.Show();
        }
        /// <summary>
        /// 移除事件
        /// </summary>
        void UnRegisterEvent()
        {
            dpSearch.ClosedPanel -= dpSearch_ClosedPanel;
        }
        #endregion
    }
}
