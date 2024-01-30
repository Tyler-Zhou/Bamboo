using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.AirExport.UI.Order
{
    [ToolboxItem(false)]
    public partial class OrderMainWorkspace : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        [ServiceDependency]
        public IFCMCommonClientService fcmCommonClientService { get; set; }

        public OrderMainWorkspace()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.ToolbarWorkspace);
                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.FastSearchWorkspace);
                    Workitem.Workspaces.Remove(this.ListWorkspace);
                    Workitem.Workspaces.Remove(this.DocumentListWorkspace);
                    Workitem.Workspaces.Remove(this.FaxMailEDIListWorkspace);
                    Workitem.Workspaces.Remove(this.MemoListWorkspace);

                    Workitem.Items.Remove(this);
                }
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            dpSearch.Text = "查询";
            this.tabDocumentList.Text = LocalData.IsEnglish ? "Document List" : "文档列表";
            this.tabFaxMailList.Text = LocalData.IsEnglish ? "Fax/Mail/EDI" : "传真/邮件/EDI";
            this.tabMemoList.Text = LocalData.IsEnglish ? "Memo" : "备忘录";
        }

        protected override void OnLoad(EventArgs e)
        {
            FastSearchWorkspace.Show();
            dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            this.ListWorkspace.Dock = DockStyle.Fill;

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
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                this.ListWorkspace.Dock = DockStyle.Fill;
            }
            else
            {
                FastSearchWorkspace.Show();
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                this.ListWorkspace.Dock = DockStyle.Fill;
            }
            ToolbarWorkspace.SendToBack();
            this.Refresh();
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
                fcmCommonClientService.ShowDocumentList(currentRow.ID, ICP.Framework.CommonLibrary.Common.OperationType.AirExport, this.Workitem, AEOrderWorkSpaceConstants.DocumentListWorkspace);
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
                fcmCommonClientService.ShowMailFaxLogList(currentRow.ID, ICP.Framework.CommonLibrary.Common.OperationType.AirExport,currentRow.ID, this.Workitem, AEOrderWorkSpaceConstants.FaxMailEDIListWorkspace);
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
        //        fcmCommonClientService.ShowMemoList(currentRow.ID, ICP.Framework.CommonLibrary.Client.OperationType.AirExport, this.Workitem, AEOrderWorkSpaceConstants.MemoListWorkspace);
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
