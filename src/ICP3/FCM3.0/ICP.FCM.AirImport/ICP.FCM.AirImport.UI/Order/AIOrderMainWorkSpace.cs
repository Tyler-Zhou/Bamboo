using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.AirImport.UI
{
    [ToolboxItem(false)]
    public partial class OIOrderMainWorkSpace : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        [ServiceDependency]
        public IFCMCommonClientService fcmCommonClientService { get; set; }

        public OIOrderMainWorkSpace()
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

                   // this.panelContainer1.ActiveChildChanged += new DevExpress.XtraBars.Docking.DockPanelEventHandler(panelContainer1_ActiveChildChanged);
                }
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        /// <summary>
        /// 明细中，活动的面板发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void panelContainer1_ActiveChildChanged(object sender, DevExpress.XtraBars.Docking.DockPanelEventArgs e)
        {
           //panelContainer1.ActiveChild
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
            Workitem.Commands[OIOrderCommandConstants.Command_FaxEmail].Execute();
            Workitem.Commands[OIOrderCommandConstants.Command_Memo].Execute();
            Workitem.Commands[OIOrderCommandConstants.Command_Document].Execute();
            base.OnLoad(e);
            ToolbarWorkspace.SendToBack();
        }

        [CommandHandler(OIOrderCommandConstants.Command_ShowSearch)]
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
        [CommandHandler(OIOrderCommandConstants.Command_Document)]
        public void Command_Docment(object sender, EventArgs e)
        {   
            if (showingChildType == null || showingChildType.Value != ShowingChildType.Docment)
            {
                AirOrderList currentRow = Workitem.State[OIOrderStateConstants.CurrentRow] as AirOrderList;
                if (currentRow == null) return;
                fcmCommonClientService.ShowDocumentList(currentRow.ID, ICP.Framework.CommonLibrary.Common.OperationType.AirImport, this.Workitem, OIOrderWorkSpaceConstants.DocumentListWorkspace);
                showingChildType = ShowingChildType.Docment;
            }
            else
            {
                showingChildType = null;
            }
        }

        [CommandHandler(OIOrderCommandConstants.Command_FaxEmail)]
        public void Command_FaxEmail(object sender, EventArgs e)
        {
            if (showingChildType == null || showingChildType.Value != ShowingChildType.FaxEmail)
            {
                AirOrderList currentRow = Workitem.State[OIOrderStateConstants.CurrentRow] as AirOrderList;
                if (currentRow == null) return;
                fcmCommonClientService.ShowMailFaxLogList(currentRow.ID, ICP.Framework.CommonLibrary.Common.OperationType.AirImport, currentRow.ID, this.Workitem, OIOrderWorkSpaceConstants.FaxMailEDIListWorkspace);
                showingChildType = ShowingChildType.FaxEmail;
            }
            else
            {
                showingChildType = null;
            }
        }

        [CommandHandler(OIOrderCommandConstants.Command_Memo)]
        public void Command_Memo(object sender, EventArgs e)
        {
            if (showingChildType == null || showingChildType.Value != ShowingChildType.Memo)
            {
                AirOrderList currentRow = Workitem.State[OIOrderStateConstants.CurrentRow] as AirOrderList;
                if (currentRow == null) return;
                fcmCommonClientService.ShowMemoList(currentRow.ID, ICP.Framework.CommonLibrary.Common.OperationType.AirImport, this.Workitem, OIOrderWorkSpaceConstants.MemoListWorkspace);
                showingChildType = ShowingChildType.Memo;
            }
            else
            {
                showingChildType = null;
            }

        }

        #endregion
    }

    enum ShowingChildType
    {
        Memo, FaxEmail, Docment
    }
}


