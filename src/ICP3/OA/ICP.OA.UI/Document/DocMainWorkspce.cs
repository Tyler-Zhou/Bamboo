using System.ComponentModel;
using Microsoft.Practices.CompositeUI;

namespace ICP.OA.UI.Document
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class DocMainWorkspce : DevExpress.XtraEditors.XtraUserControl
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public DocMainWorkspce()
        {
            InitializeComponent();

            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.UserWorkspace);
                    Workitem.Workspaces.Remove(this.JobWorkspace);
                    Workitem.Workspaces.Remove(this.MainViewWorkspce);
                    Workitem.Items.Remove(this);
                   

                    this.UserWorkspace.PerformLayout();
                    this.UserWorkspace.Dispose();
                    this.UserWorkspace = null;
                    this.JobWorkspace.PerformLayout();
                    this.JobWorkspace.Dispose();
                    this.JobWorkspace = null;
                    this.MainViewWorkspce.PerformLayout();
                    this.MainViewWorkspce.Dispose();
                    this.MainViewWorkspce = null;
                    Workitem.Dispose();
                    Workitem = null;
                    this.PerformLayout();

                }
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            dpJob .Text = "职位";
            dpUser .Text = "用户";
        }
    }
}
