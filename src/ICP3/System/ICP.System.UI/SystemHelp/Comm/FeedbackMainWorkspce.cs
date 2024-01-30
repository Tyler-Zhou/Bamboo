using Microsoft.Practices.CompositeUI;

namespace ICP.Sys.UI.SystemHelp
{
    public partial class FeedbackMainWorkspce : ICP.Framework.ClientComponents.UIFramework.BasePart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion


        public FeedbackMainWorkspce()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.FeedbackMainWorkspace);
                    Workitem.Workspaces.Remove(this.FeedbackMainWorkspace);
        
                    this.FeedbackMainWorkspace.PerformLayout();
                  

                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    this.PerformLayout();
                }
            };            
        }


    }
}
