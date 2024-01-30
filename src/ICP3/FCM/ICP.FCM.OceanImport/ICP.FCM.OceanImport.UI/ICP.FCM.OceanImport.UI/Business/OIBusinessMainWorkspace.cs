using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.FCM.OceanImport.UI
{
    [ToolboxItem(false)]
    public partial class OIBusinessMainWorkSpace : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public OIBusinessMainWorkSpace()
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
                    Workitem.Workspaces.Remove(this.EventListWorkspace);
                    Workitem.Workspaces.Remove(this.FaxMailEDIListWorkspace);
                    Workitem.Workspaces.Remove(this.DocumentListWorkspace);
                    Workitem.Workspaces.Remove(this.AcceptWorkspace);
                    Workitem.Items.Remove(this);

                    this.SearchWorkspace.PerformLayout();
                    this.ToolbarWorkspace.PerformLayout();
                    this.ListWorkspace.PerformLayout();
                    this.EventListWorkspace.PerformLayout();
                    this.FaxMailEDIListWorkspace.PerformLayout();
                    this.DocumentListWorkspace.PerformLayout();
                    this.FastSearchWorkspace.PerformLayout();
                    this.AcceptWorkspace.PerformLayout();
                    this.PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };
            if (!ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish) SetCnText();
        }

        private void SetCnText()
        {
            dpSearch.Text = "查询";
            this.tabDocumentList.Text = "文档列表";
            this.tabFaxMailList.Text = "传真/邮件/EDI";
            this.tabMemoList.Text = "事件";
            this.tabBillList.Text = "账单列表";
            this.tabDebt.Text = "欠款记录";
            this.tabAcceptList.Text = "分发/修订历史";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                this.ListWorkspace.Dock = DockStyle.Fill;

                ToolbarWorkspace.SendToBack();
            }
        }

        [CommandHandler(OIBusinessCommandConstants.Command_ShowSearch)]
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


    }
}
