using System.ComponentModel;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.Common.UI.Finder
{
    [ToolboxItem(false)]
    public partial class BusinessFinderWorkspace : DevExpress.XtraEditors.XtraUserControl
    {
         #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        #endregion

        #region 初始化
        public BusinessFinderWorkspace()
        {
            InitializeComponent();
            this.Disposed += delegate
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

              
            };
        }

        #endregion
    }
}
