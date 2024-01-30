using System;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI.Commands;
using DevExpress.XtraBars.Docking;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.TMS.UI
{
    [ToolboxItem(false)]
    public partial class TruckBookingsMainWorkSpace : BasePart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public TruckBookingsMainWorkSpace()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.ToolbarWorkspace);
                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.ListWorkspace);
                    Workitem.Workspaces.Remove(this.EventListWorkspace);
                    Workitem.Workspaces.Remove(this.FaxMailListWorkspace);
                    Workitem.Workspaces.Remove(this.DocumentListWorkspace);

                    this.SearchWorkspace.PerformLayout();
                    this.ToolbarWorkspace.PerformLayout();
                    this.ListWorkspace.PerformLayout();
                    this.EventListWorkspace.PerformLayout();
                    this.FaxMailListWorkspace.PerformLayout();
                    this.DocumentListWorkspace.PerformLayout();
                   

                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    this.PerformLayout();
                }
            };

            SetCtl();
            this.RegisterMessage("TruckBookings", LocalData.IsEnglish ? "Trucking List" : "拖车业务");
        }

        void SetCtl()
        {
            this.dpSearch.Text = LocalData.IsEnglish ? "Search" : "查询";
            this.tabDocumentList.Text = LocalData.IsEnglish ? "Document List" : "文档列表";
            this.tabFaxMailList.Text = LocalData.IsEnglish ? "Fax/Mail/EDI" : "传真/邮件/EDI";
            this.tabMemoList.Text = LocalData.IsEnglish ? "Event" : "事件列表";
        }

        [CommandHandler(TruckBookingsCommandConstants.Command_ShowSearch)]
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
            this.Refresh();
        }
    }
}
