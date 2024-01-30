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
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.ToolbarWorkspace);
                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.ListWorkspace);
                    Workitem.Items.Remove(this);
                }
            };
        }

        #endregion
    }
}
