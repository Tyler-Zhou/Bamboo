using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.AirExport.UI.Order
{
    [ToolboxItem(false)]
    public partial class OrderMainWorkspace : XtraUserControl
    {   
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }
        public IFCMCommonClientService FCMCommonClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFCMCommonClientService>();
            }
        }

        public OrderMainWorkspace()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ToolbarWorkspace);
                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(FastSearchWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Workspaces.Remove(DocumentListWorkspace);
                    Workitem.Workspaces.Remove(FaxMailEDIListWorkspace);
                    Workitem.Workspaces.Remove(EventListWorkspace);

                  
                    SearchWorkspace.PerformLayout();
                    ToolbarWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();
                    EventListWorkspace.PerformLayout();
                    FaxMailEDIListWorkspace.PerformLayout();
                    DocumentListWorkspace.PerformLayout();
                    FastSearchWorkspace.PerformLayout();

                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    PerformLayout();
                }
            };
            if (LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            dpSearch.Text = "查询";
            tabDocumentList.Text = LocalData.IsEnglish ? "Document List" : "文档列表";
            tabFaxMailList.Text = LocalData.IsEnglish ? "Fax/Mail/EDI" : "传真/邮件/EDI";
            tabMemoList.Text = LocalData.IsEnglish ? "Event" : "事件";
        }

        protected override void OnLoad(EventArgs e)
        {
            FastSearchWorkspace.Show();
            dpSearch.Visibility = DockVisibility.Hidden;
            ListWorkspace.Dock = DockStyle.Fill;

            base.OnLoad(e);
            ToolbarWorkspace.SendToBack();
        }


        #region

        [CommandHandler(AEOrderCommandConstants.Command_ShowSearch)]
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

        #region 日志


        ShowingChildType? showingChildType = null;
        [CommandHandler(AEOrderCommandConstants.Command_Document)]
        public void Command_Docment(object sender, EventArgs e)
        {
            if (showingChildType == null || showingChildType.Value != ShowingChildType.Docment)
            {
                AirOrderList currentRow = Workitem.State[AEOrderStateConstants.CurrentRow] as AirOrderList;
                if (currentRow == null) return;
                FCMCommonClientService.ShowDocumentList(currentRow.ID, OperationType.AirExport,Workitem,AEOrderWorkSpaceConstants.DocumentListWorkspace);
                showingChildType = ShowingChildType.Docment;
            }
            else
            {
                showingChildType = null;
            }
        }

        [CommandHandler(AEOrderCommandConstants.Command_FaxEmail)]
        public void Command_FaxEmail(object sender, EventArgs e)
        {
            if (showingChildType == null || showingChildType.Value != ShowingChildType.FaxEmail)
            {
                AirOrderList currentRow = Workitem.State[AEOrderStateConstants.CurrentRow] as AirOrderList;
                if (currentRow == null) return;
                FCMCommonClientService.ShowMailFaxLogList(currentRow.ID, OperationType.AirExport,currentRow.ID,Workitem,AEOrderWorkSpaceConstants.FaxMailEDIListWorkspace);
                showingChildType = ShowingChildType.FaxEmail;
            }
            else
            {
                showingChildType = null;
            }
        }

        //[CommandHandler(AEOrderCommandConstants.Command_Memo)]
        //public void Command_Memo(object sender, EventArgs e)
        //{
        //    if (showingChildType == null || showingChildType.Value != ShowingChildType.Memo)
        //    {
        //        AirOrderList currentRow = Workitem.State[AEOrderStateConstants.CurrentRow] as AirOrderList;
        //        if (currentRow == null) return;
        //        FCMCommonClientService.ShowMemoList(currentRow.ID, ICP.Framework.CommonLibrary.Client.OperationType.AirExport, this.Workitem, AEOrderWorkSpaceConstants.EventListWorkspace);
        //        showingChildType = ShowingChildType.Memo;
        //    }
        //    else
        //    {
        //        showingChildType = null;
        //    }

        //}
        #endregion
        #endregion
        enum ShowingChildType
        {
            Memo, FaxEmail, Docment
        }
    }
}
