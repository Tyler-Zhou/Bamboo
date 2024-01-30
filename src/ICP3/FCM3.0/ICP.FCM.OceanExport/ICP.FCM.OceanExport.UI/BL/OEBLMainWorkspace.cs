using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.OceanExport.UI.BL
{
    [ToolboxItem(false)]
    public partial class OEBLMainWorkspace : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public OEBLMainWorkspace()
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
                    Workitem.Workspaces.Remove(this.MemoListWorkspace);
                    Workitem.Workspaces.Remove(this.FaxMailEDIListWorkspace);
                    Workitem.Workspaces.Remove(this.DocumentListWorkspace);

                    Workitem.Items.Remove(this);
                }
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            dpSearch.Text = "查询";
            this.tabMemoList.Text = LocalData.IsEnglish ? "Memo" : "备忘录";
            this.tabFaxMailList.Text = LocalData.IsEnglish ? "Fax/Mail/EDI" : "邮件/传真/EDI";
            this.tabDocumentList.Text = LocalData.IsEnglish ? "Document List" : "文档列表";
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

        [CommandHandler(OEBLCommandConstants.Command_ShowSearch)]
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

        public Microsoft.Practices.CompositeUI.SmartParts.ISmartPartInfo GetSmartPartInfo(Type smartPartInfoType)
        {
            // Implement ISmartPartInfoProvider in the containing smart part. Required in order for contained smart part infos to work.
            // Microsoft.Practices.CompositeUI.SmartParts.ISmartPartInfoProvider ensureProvider = this;
            return this.infoProvider.GetSmartPartInfo(smartPartInfoType);

        }
    }
}
