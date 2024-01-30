using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;

namespace ICP.Common.UI.CC
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CCMainWorkspace : BasePart
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public CCMainWorkspace()
        {
            InitializeComponent();

            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.ToolBarWorkspace);
                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.ListWorkspace);

                    Workitem.Workspaces.Remove(this.CustoemrArchivesWorkspace);
                    Workitem.Workspaces.Remove(this.PartnerWorkspace);
                    Workitem.Workspaces.Remove(this.BusinessWorkspace);
                    Workitem.Workspaces.Remove(this.ReportWorkspace);

                    Workitem.Items.Remove(this);

                    this.ToolBarWorkspace.PerformLayout();
                    this.SearchWorkspace.PerformLayout();
                    this.ListWorkspace.PerformLayout();
                    this.CustoemrArchivesWorkspace.PerformLayout();
                    this.PartnerWorkspace.PerformLayout();
                    this.BusinessWorkspace.PerformLayout();
                    this.ReportWorkspace.PerformLayout();



                    this.PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };

            if (!DesignMode) { InitMessage(); }
        }

        private void InitMessage()
        {
            this.RegisterMessage("Titel", "CC");
            //this.RegisterMessage("CurrentChanging", "The current contract is changed and has not yet been saved. \r\nClicks Yes to save the changes and go to the next conatct. \r\nClicks No to desert all of changing. \r\nClicks Cancel to return.");
            //this.RegisterMessage("SaveSuccessfully", "Save Successfully");
        }
    }
}
