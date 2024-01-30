using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.OtherBusiness.UI.Common
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    public partial class OBMainWorkspace : XtraUserControl
    {
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OBMainWorkspace()
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
                    Workitem.Workspaces.Remove(EventListWorkspace);
                    Workitem.Workspaces.Remove(FaxMailEDIListWorkspace);
                    Workitem.Workspaces.Remove(DocumentListWorkspace);
              
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
            if (!LocalData.IsEnglish) SetCnText();
        }

        private void SetCnText()
        {
            tabMemoList.Text = "事件列表";
            tabFaxMailList.Text =  "邮件/传真/EDI";
            tabDocumentList.Text = "文档列表";
        }

        protected override void OnLoad(EventArgs e)
        {
            FastSearchWorkspace.Show();
            dpSearch.Visibility = DockVisibility.Hidden;
            ListWorkspace.Dock = DockStyle.Fill;

            base.OnLoad(e);
            ToolbarWorkspace.SendToBack();
        }


        
    }
}

