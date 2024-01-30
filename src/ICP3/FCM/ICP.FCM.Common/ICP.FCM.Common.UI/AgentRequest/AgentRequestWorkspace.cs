using System;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.FCM.Common.UI.AgentRequest
{
    [ToolboxItem(false)]
    public partial class AgentRequestWorkspace : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public AgentRequestWorkspace()
        {
            InitializeComponent();

            this.Disposed += delegate
            {
                if (Workitem != null)
                {  
                    Workitem.Workspaces.Remove(this.ToolbarWorkspace);

                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                   
                    Workitem.Workspaces.Remove(this.ListWorkspace);
                    this.SearchWorkspace.PerformLayout();
                    this.ToolbarWorkspace.PerformLayout();
                    this.ListWorkspace.PerformLayout();
                  
                    this.SearchWorkspace.Dispose();
                    this.SearchWorkspace = null;
                    this.ToolbarWorkspace.Dispose();
                    this.ToolbarWorkspace = null;
                    this.ListWorkspace.Dispose();
                    this.ListWorkspace = null;
                    
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
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ToolbarWorkspace.SendToBack();
        }

        [CommandHandler(OEAgentRequesCommandConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object o, EventArgs e)
        {

            if(dpSearch.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible)
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            else
                dpSearch.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }
    }
}
