using System;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System.Diagnostics;
using System.Reflection;
using ICP.DataCache.ServiceInterface;

namespace ICP.FCM.OceanImport.UI
{
    [ToolboxItem(false)]
    public partial class OIBusinessDownloadMain : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public OIBusinessDownloadMain()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.ToolbarWorkspace);
                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.ListWorkspace);
                    Workitem.Workspaces.Remove(this.FeeWorkspace);
                    Workitem.Workspaces.Remove(this.DocumentListWorkspace);
                    Workitem.Workspaces.Remove(this.OperationPartWorkspace);
                    Workitem.Workspaces.Remove(this.AcceptWorkspace);

                    this.SearchWorkspace.PerformLayout();
                    this.ToolbarWorkspace.PerformLayout();
                    this.ListWorkspace.PerformLayout();
               
                    this.FeeWorkspace.PerformLayout();
                    this.DocumentListWorkspace.PerformLayout();
                    this.OperationPartWorkspace.PerformLayout();
                    this.AcceptWorkspace.PerformLayout();

                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    this.PerformLayout();
                }
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            dpSearch.Text = "查询";
            this.tabDocumentList.Text = "文档列表";
            this.tabFeeList.Text = "费用列表";
            this.tabAcceptList.Text = "分发/修订历史";
        }

        protected override void OnLoad(EventArgs e)
        {
            dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            //this.ListWorkspace.Dock = DockStyle.Fill;

            base.OnLoad(e);
            ToolbarWorkspace.SendToBack();
        }

        [CommandHandler(OIBusinessDownLoadCommandConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object o, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            if (dpSearch.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Hidden)
            {
                dpSearch.Show();
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                //this.ListWorkspace.Dock = DockStyle.Fill;
            }
            else
            {
                dpSearch.Hide();
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                //this.ListWorkspace.Dock = DockStyle.Fill;
            }
            ToolbarWorkspace.SendToBack();
            this.Refresh();

            MethodBase method = MethodBase.GetCurrentMethod();
            StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName,"SEARCH", "业务下载查找");
        }

    }
}
