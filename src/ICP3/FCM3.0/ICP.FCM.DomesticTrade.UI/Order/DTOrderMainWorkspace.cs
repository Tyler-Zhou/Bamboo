using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.DomesticTrade.UI.Order
{
    [ToolboxItem(false)]
    public partial class DTOrderMainWorkspace : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IFCMCommonClientService fcmCommonClientService { get; set; }

        public DTOrderMainWorkspace()
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

        [CommandHandler(DTOrderCommandConstants.Command_ShowSearch)]
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

    enum ShowingChildType
    {
        Memo, FaxEmail, Docment
    }
}
