using System.ComponentModel;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.TelexApply
{
    [ToolboxItem(false)]
    public partial class TelexApplyMainWorkspace : XtraUserControl
    {
        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public TelexApplyMainWorkspace()
        {
            InitializeComponent();

            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ToolbarWorkspace);
                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Workspaces.Remove(EditWorkspace);
                    Workitem.Workspaces.Remove(DocumentListWorkspace);
                    Workitem.Items.Remove(this);
                    SearchWorkspace.PerformLayout();
                    ToolbarWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();
                    EditWorkspace.PerformLayout();
                    DocumentListWorkspace.PerformLayout();

                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                   
                }
            };

            dpSearch.Text = LocalData.IsEnglish ? "Search" : "查询";
        }
    }
}
