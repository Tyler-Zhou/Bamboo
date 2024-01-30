using System;
using DevExpress.XtraBars.Docking;
using ICP.Framework.ClientComponents.UIFramework;
using System.ComponentModel;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Business.Common.UI.QuotedPrice
{
    /// <summary>
    /// Quoted Price WorkSpace
    /// </summary>
    [ToolboxItem(false)]
    public partial class QuotedPriceMainWorkspace : BasePart
    {
        #region Services & Delegate
        #region Services
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; } 
        #endregion

        #region Delegate
        /// <summary>
        /// Tab 选项改变
        /// </summary>
        public event CancelEventHandler PageChanging; 
        #endregion
        #endregion

        #region Init
        /// <summary>
        /// Quoted Price WorkSpace
        /// </summary>
        public QuotedPriceMainWorkspace()
        {
            InitializeComponent();
            InitControls();
            InitMessage();
            RegisterEvent();
            Disposed += (sender, arg) =>
            {
                UnRegisterEvent();
                if (RootWorkItem != null)
                {
                    RootWorkItem.Workspaces.Remove(ToolBarPartWorkspace);
                    RootWorkItem.Workspaces.Remove(SearchPartWorkspace);
                    RootWorkItem.Workspaces.Remove(ListPartWorkspace);
                    RootWorkItem.Workspaces.Remove(RatesListPartWorkspace);

                    RootWorkItem.Items.Remove(this);

                    SearchPartWorkspace.PerformLayout();
                    ToolBarPartWorkspace.PerformLayout();
                    ListPartWorkspace.PerformLayout();
                    RatesListPartWorkspace.PerformLayout();

                    PerformLayout();
                    RootWorkItem.Dispose();
                    RootWorkItem = null;
                }
            };
        } 
        #endregion

        #region Controls Event
        /// <summary>
        /// 显示查询面板
        /// </summary>
        [CommandHandler(QPCommandConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object sender, EventArgs e)
        {
            dpSearch.Visibility = SearchPartWorkspace.Visible ? DockVisibility.Hidden : DockVisibility.Visible;
            Refresh();
        }

        #endregion

        #region Custom Method

        private void InitControls()
        {
            FastSearchPartWorkspace.Height = 0;

            #region Init Language

            if (!LocalData.IsEnglish)
                dpSearch.Text = "查询";

            #endregion
        }

        private void InitMessage()
        {
            RegisterMessage("Tip", LocalData.IsEnglish ? "Tip" : "提示");
            RegisterMessage("CurrentChanging",LocalData.IsEnglish
                ? "The current quoted price is changed and has not yet been saved. \r\nClicks [Yes] to save the changes and go to the next quoted price. \r\nClicks [No] to desert all of changing. \r\nClicks [Cancel] to return."
                : "当前报价有更改未保存. \r\n点击[Yes]将保存并切换到下一条报价. \r\n点击[No]放弃所有更改. \r\n点击[Cancel]返回."
                );
            RegisterMessage("SaveSuccessfully", LocalData.IsEnglish? "Save Successfully":"保存成功");
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        private void UnRegisterEvent()
        {
        } 
        #endregion
    }
}
