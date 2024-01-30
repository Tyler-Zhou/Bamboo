using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.OceanImport.UI
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

                    //this.panelContainer1.ActiveChildChanged += new DevExpress.XtraBars.Docking.DockPanelEventHandler(panelContainer1_ActiveChildChanged);
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
            dpSearch.Text =LocalData.IsEnglish?"Search": "查询";
            this.tabMemoList.Text = LocalData.IsEnglish ? "Memo" : "备忘录";
            this.tabFaxMailList.Text = LocalData.IsEnglish ? "Fax/Mail/EDI" : "邮件/传真/EDI";
            this.tabDocumentList.Text = LocalData.IsEnglish ? "Document List" : "文档列表";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {

                FastSearchWorkspace.Show();
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                this.ListWorkspace.Dock = DockStyle.Fill;

                ToolbarWorkspace.SendToBack();
            }
        }

        [CommandHandler(OIOrderCommandConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object o, EventArgs e)
        {
            if (FastSearchWorkspace.Visible)
            {
                SearchWorkspace.Show();
                FastSearchWorkspace.Hide();
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                this.ListWorkspace.Dock = DockStyle.Fill;
            }
            else
            {
                FastSearchWorkspace.Show();
                SearchWorkspace.Hide();
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
                OceanOrderList currentRow = Workitem.State[OIOrderStateConstants.CurrentRow] as OceanOrderList;
                if (currentRow == null) return;
                fcmCommonClientService.ShowDocumentList(currentRow.ID, ICP.Framework.CommonLibrary.Common.OperationType.OceanExport, this.Workitem, OIOrderWorkSpaceConstants.DocumentListWorkspace);
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
                OceanOrderList currentRow = Workitem.State[OIOrderStateConstants.CurrentRow] as OceanOrderList;
                if (currentRow == null) return;
                fcmCommonClientService.ShowMailFaxLogList(currentRow.ID, ICP.Framework.CommonLibrary.Common.OperationType.OceanExport, currentRow.ID, this.Workitem, OIOrderWorkSpaceConstants.FaxMailEDIListWorkspace);
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
                OceanOrderList currentRow = Workitem.State[OIOrderStateConstants.CurrentRow] as OceanOrderList;
                if (currentRow == null) return;
                fcmCommonClientService.ShowMemoList(currentRow.ID, ICP.Framework.CommonLibrary.Common.OperationType.OceanExport, this.Workitem, OIOrderWorkSpaceConstants.MemoListWorkspace);
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
