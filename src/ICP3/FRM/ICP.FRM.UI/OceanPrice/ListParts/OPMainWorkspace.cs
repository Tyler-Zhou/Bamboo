using DevExpress.XtraBars.Docking;
using DevExpress.XtraTab;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.ComponentModel;

namespace ICP.FRM.UI.OceanPrice
{
    /// <summary>
    /// 合约主面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class OPMainWorkspace : BasePart
    {
        #region Services
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; } 
        #endregion

        #region Fields
        /// <summary>
        /// 界面是否最大尺寸
        /// </summary>
        bool _isMaxOceanItem = false;
        /// <summary>
        /// 线程ID:进度面板
        /// </summary>
        static int theradID = 0;
        /// <summary>
        /// 页改变时事件
        /// </summary>
        public event CancelEventHandler PageChanging; 
        #endregion

        #region Init
        /// <summary>
        /// 合约主面板
        /// </summary>
        public OPMainWorkspace()
        {
            InitializeComponent();

            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ToolBarWorkspace);
                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Workspaces.Remove(ContractWorkspace);
                    Workitem.Workspaces.Remove(BPRWorkspace);
                    Workitem.Workspaces.Remove(ARWorkspace);
                    Workitem.Workspaces.Remove(AFWorkspace);
                    Workitem.Workspaces.Remove(PermissionsWorkspace);
                    Workitem.Workspaces.Remove(AttachmentWorkspace);

                    Workitem.Items.Remove(this);

                    SearchWorkspace.PerformLayout();
                    ToolBarWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();
                    ContractWorkspace.PerformLayout();
                    BPRWorkspace.PerformLayout();
                    ARWorkspace.PerformLayout();
                    AFWorkspace.PerformLayout();

                    PermissionsWorkspace.PerformLayout();
                    AttachmentWorkspace.PerformLayout();

                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };

            if (!DesignMode) { InitMessage(); }
        } 
        #endregion

        #region Window Event
        private void xtabMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            Workitem.Commands[OPCommonConstants.Command_TabChanged].Execute();
            try
            {
                LoadingServce.CloseLoadingForm(theradID);
            }
            catch { }
        }

        private void xtabMain_SelectedPageChanging(object sender, TabPageChangingEventArgs e)
        {
            try
            {
                theradID = LoadingServce.ShowLoadingForm("Loading...");
            }
            catch { }
            if (xtabMain.SelectedTabPageIndex == 0 && PageChanging != null)
            {
                CancelEventArgs er = new CancelEventArgs();
                PageChanging(this, er);
                e.Cancel = er.Cancel;
            }
        } 
        #endregion

        #region Command
        /// <summary>
        /// 扩展或缩小界面
        /// </summary>
        [CommandHandler(OPCommonConstants.Command_MaxOceanItem)]
        public void Command_MaxOceanItem(object sender, EventArgs e)
        {
            splitContainerControl1.Collapsed = !_isMaxOceanItem;
            _isMaxOceanItem = !_isMaxOceanItem;
        }
        /// <summary>
        /// 激活首个TabPage
        /// </summary>
        [CommandHandler(OPCommonConstants.Command_FirstTabFocused)]
        public void Command_FirstTabFocused(object sender, EventArgs e)
        {
            if (xtabMain.SelectedTabPageIndex != 0)
            {
                xtabMain.SelectedTabPageIndex = 0;
            }
        }
        /// <summary>
        /// 显示查询面板
        /// </summary>
        [CommandHandler(OPCommonConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object sender, EventArgs e)
        {
            if (SearchWorkspace.Visible)
            {
                dpSearch.Visibility = DockVisibility.Hidden;
            }
            else
            {
                dpSearch.Visibility = DockVisibility.Visible;
            }
            Refresh();
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        [CommandHandler(OPCommonConstants.Command_InsterNewData)]
        public void Command_InsterNewData(object sender, EventArgs e)
        {
            if (splitContainerControl1.Collapsed) splitContainerControl1.Collapsed = false;

            if (xtabMain.SelectedTabPageIndex != 0)
            {
                xtabMain.SelectedPageChanging -= xtabMain_SelectedPageChanging;
                xtabMain.SelectedPageChanged -= xtabMain_SelectedPageChanged;

                xtabMain.SelectedTabPageIndex = 0;

                xtabMain.SelectedPageChanging += xtabMain_SelectedPageChanging;
                xtabMain.SelectedPageChanged += xtabMain_SelectedPageChanged;
            }
        } 
        #endregion

        #region Method
        /// <summary>
        /// 由于在UIAdapter类中使用ErrorTrace会出错,所以在此处提出错误
        /// </summary>
        /// <param name="ex">Exception</param>
        public void ShowException(Exception ex)
        {
            LocalCommonServices.ErrorTrace.SetErrorInfo(this, ex);
        }

        /// <summary>
        /// 初始化提示信息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("Titel", "Ocean Price");
            RegisterMessage("CurrentChanging", "The current contract is changed and has not yet been saved. \r\nClicks Yes to save the changes and go to the next conatct. \r\nClicks No to desert all of changing. \r\nClicks Cancel to return.");
            RegisterMessage("SaveSuccessfully", "Save Successfully");
        } 
        #endregion

    }
}
