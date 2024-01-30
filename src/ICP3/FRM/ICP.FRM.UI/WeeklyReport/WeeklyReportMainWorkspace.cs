using System.ComponentModel;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;

namespace ICP.FRM.UI
{
    [ToolboxItem(false)]
    public partial class WeeklyReportMainWorkspace : XtraUserControl
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        #endregion

        #region 初始化
        public WeeklyReportMainWorkspace()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Items.Remove(this);
 
                    ListWorkspace.PerformLayout();
                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };
        }
        #endregion


    }
}
