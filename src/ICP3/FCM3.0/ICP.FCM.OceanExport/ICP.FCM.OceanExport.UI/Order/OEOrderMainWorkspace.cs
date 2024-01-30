using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.OceanExport.UI.Order
{
    [ToolboxItem(false)]
    public partial class OEOrderMainWorkspace : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IFCMCommonClientService fcmCommonClientService { get; set; }

        public OEOrderMainWorkspace()
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

            SetCnText();
        }

        private void SetCnText()
        {
            dpSearch.Text = "查询";
            this.tabDocumentList.Text = LocalData.IsEnglish ? "Document List" : "文档列表";
            this.tabFaxMailEDIList.Text = LocalData.IsEnglish ? "Fax/Mail/EDI" : "传真/邮件/EDI";
            this.tabMemoList.Text = LocalData.IsEnglish ? "Memo" : "备忘录";
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode)
            {
                base.OnLoad(e);
                FastSearchWorkspace.Show();
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                this.ListWorkspace.Dock = DockStyle.Fill;
                ToolbarWorkspace.SendToBack();
            }
        }

        [CommandHandler(OEOrderCommandConstants.Command_ShowSearch)]
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
            ToolbarWorkspace.SendToBack();
            this.Refresh();
        }


        #region 日志


        ShowingChildType? showingChildType = null;
        [CommandHandler(OEOrderCommandConstants.Command_Document)]
        public void Command_Docment(object sender, EventArgs e)
        {
            if (showingChildType == null || showingChildType.Value != ShowingChildType.Docment)
            {
                OceanOrderList currentRow = Workitem.State[OEOrderStateConstants.CurrentRow] as OceanOrderList;
                if (currentRow == null) return;
                fcmCommonClientService.ShowDocumentList(currentRow.ID, ICP.Framework.CommonLibrary.Common.OperationType.OceanExport, this.Workitem, OEOrderWorkSpaceConstants.DocumentListWorkspace);
                showingChildType = ShowingChildType.Docment;
            }
            else
            {
                showingChildType = null;
            }
        }

        [CommandHandler(OEOrderCommandConstants.Command_OEOFaxEmail)]
        public void Command_FaxEmail(object sender, EventArgs e)
        {
            if (showingChildType == null || showingChildType.Value != ShowingChildType.FaxEmail)
            {
                OceanOrderList currentRow = Workitem.State[OEOrderStateConstants.CurrentRow] as OceanOrderList;
                if (currentRow == null) return;
                fcmCommonClientService.ShowMailFaxLogList(currentRow.ID, ICP.Framework.CommonLibrary.Common.OperationType.OceanExport, currentRow.ID, this.Workitem, OEOrderWorkSpaceConstants.FaxMailEDIListWorkspace);
                showingChildType = ShowingChildType.FaxEmail;
            }
            else
            {
                showingChildType = null;
            }
        }

        //[CommandHandler(OEOrderCommandConstants.Command_Memo)]
        //public void Command_Memo(object sender, EventArgs e)
        //{
        //    if (showingChildType == null || showingChildType.Value != ShowingChildType.Memo)
        //    {
        //        OceanOrderList currentRow = Workitem.State[OEOrderStateConstants.CurrentRow] as OceanOrderList;
        //        if (currentRow == null) return;
        //        fcmCommonClientService.ShowMemoList(currentRow.ID, ICP.Framework.CommonLibrary.Client.OperationType.OceanExport, this.Workitem, OEOrderWorkSpaceConstants.MemoListWorkspace);
        //        showingChildType = ShowingChildType.Memo;
        //    }
        //    else
        //    {
        //        showingChildType = null;
        //    }

        //}

        #endregion
    }

    enum ShowingChildType
    {
        Memo, FaxEmail, Docment
    }
}
