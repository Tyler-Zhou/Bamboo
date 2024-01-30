using System;
using System.ComponentModel;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Commands;
using DevExpress.XtraBars.Docking;
using Microsoft.Practices.CompositeUI;

namespace ICP.FRM.UI.SearchRate
{
    /// <summary>
    /// 查询海运运价主工作区
    /// </summary>
    [ToolboxItem(false)]
    public partial class SearchOceanMainWorkspace : XtraUserControl
    {
        /// <summary>
        /// 查询海运运价主工作区
        /// </summary>
        public SearchOceanMainWorkspace()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ToolbarWorkspace);
                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Workspaces.Remove(BaseInfoWorkspace);
                    Workitem.Workspaces.Remove(ContractWorkspace);
                    Workitem.Workspaces.Remove(RemarkWorkspace);
                    Workitem.Items.Remove(this);
                    SearchWorkspace.PerformLayout();
                    ToolbarWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();
                    BaseInfoWorkspace.PerformLayout();
                    ContractWorkspace.PerformLayout();
                    RemarkWorkspace.PerformLayout();
                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };
        }

        #region 服务
        /// <summary>
        /// 查询海运运价WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 初始化
        /// <summary>
        /// 查询运价面板高度
        /// </summary>
        private string SearchOceanPartHeight = "SearchOceanPartHeight";
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                tabDetail.BackColor = panel1.BackColor;

                if (!LocalCommonServices.PermissionService.HaveActionPermission(PermissionCommandConstants.SEARCHOCEAN_VIEWCONTRACTNO))
                {
                    tabContractInfo.PageVisible = false;
                }

            }

        }  


        #endregion

        #region 显示/关闭查询面板
        /// <summary>
        /// 显示隐藏查询面板
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        [CommandHandler(SearchOceanCommandConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object o, EventArgs e)
        {
            if (dpSearch.Visibility == DockVisibility.Hidden)
            {
                dpSearch.Visibility = DockVisibility.Visible;
            }
            else
            {
                dpSearch.Visibility = DockVisibility.Hidden;
            }
            ToolbarWorkspace.SendToBack();
            Refresh();
        }
        #endregion
        bool _IsShowFile = false;
        bool _IsShowRemark = false;

        #region EventSubscription
       
        /// <summary>
        /// 显示备注面板
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        //[EventSubscription(SearchOceanEventBrokerConstants.EventBroker_ShowRemark)]
        [CommandHandler(SearchOceanCommandConstants.Command_ShowRemark)]
        public void Command_ShowRemark(object o, EventArgs e)
        {
            if (!_IsShowRemark)
            {
                RemarkWorkspace.Visible = true;
            }
            else
            {
                RemarkWorkspace.Visible = false;
            }
            _IsShowFile = false;
            _IsShowRemark = !_IsShowRemark;
        }
        
        /// <summary>
        /// 显示附件面板
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        //[EventSubscription(SearchOceanEventBrokerConstants.EventBroker_ShowAttachment)]
        [CommandHandler(SearchOceanCommandConstants.Command_ShowAttachment)]
        public void Command_ShowAttachment(object o, EventArgs e)
        {
            if (!_IsShowFile)
            {
                RemarkWorkspace.Visible = true;
            }
            else
            {
                RemarkWorkspace.Visible = false;
            }

            _IsShowRemark = false;
            _IsShowFile = !_IsShowFile;
        }

        #endregion


    }
}
