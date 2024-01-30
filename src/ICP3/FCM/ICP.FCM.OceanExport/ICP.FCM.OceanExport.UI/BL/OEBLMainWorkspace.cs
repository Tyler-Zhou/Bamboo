using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.OceanExport.UI.BL
{
    /// <summary>
    /// 提单主工作区
    /// </summary>
    [ToolboxItem(false)]
    public partial class OEBLMainWorkspace : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }

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
                    Workitem.Workspaces.Remove(this.EventListWorkspace);
                    Workitem.Workspaces.Remove(this.FaxMailEDIListWorkspace);
                    Workitem.Workspaces.Remove(this.DocumentListWorkspace);
                    Workitem.Workspaces.Remove(this.AcceptWorkspace);
                    Workitem.Workspaces.Remove(this.CargoTrackingWorkspace);
                    this.AcceptWorkspace.PerformLayout();
                    Workitem.Items.Remove(this);

                    this.SearchWorkspace.PerformLayout();
                    this.ToolbarWorkspace.PerformLayout();
                    this.ListWorkspace.PerformLayout();
                    this.EventListWorkspace.PerformLayout();
                    this.FaxMailEDIListWorkspace.PerformLayout();
                    this.DocumentListWorkspace.PerformLayout();
                    this.FastSearchWorkspace.PerformLayout();
                    this.CargoTrackingWorkspace.PerformLayout();



                    this.PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }

            };
            if (!LocalData.IsEnglish) SetCnText();
        }

        private void SetCnText()
        {
            dpSearch.Text = "查询";
            this.tabMemoList.Text = "事件";
            this.tabFaxMailList.Text = "邮件/传真/EDI";
            this.tabDocumentList.Text = "文档列表";
            this.tabAcceptList.Text = "分发/修订历史";
            this.tabCargoTracking.Text = "货物跟踪";
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!LocalData.IsDesignMode)
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


        public bool IsSelectedCargoTracking
        {
            get
            {
                if (xtab.SelectedTabPage == this.tabCargoTracking)
                {
                    return true;
                }
                return false;
            }
        }


        public bool IsSelectedDocumentList
        {
            get
            {
                if (xtab.SelectedTabPage == this.tabDocumentList)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsSelectedEventList
        {
            get
            {
                if (xtab.SelectedTabPage == this.tabMemoList)
                {

                    return true;
                }
                return false;
            }
        }

        public bool IsSelectedFaxMailList 
        {
            get 
            {
                if (xtab.SelectedTabPage==this.tabFaxMailList)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsSelectedAcceptList 
        {
            get 
            {
                if (xtab.SelectedTabPage==this.tabAcceptList)
                {
                    return true;
                }
                return false;
            }
        }



        public event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler TabSelectedPageChanged;
        private void xtab_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (TabSelectedPageChanged != null)
            {
                TabSelectedPageChanged(sender, e);
            }
        }
    }
}
