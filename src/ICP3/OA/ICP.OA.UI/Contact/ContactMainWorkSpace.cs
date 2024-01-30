using System.ComponentModel;
using ICP.Framework.ClientComponents.UIManagement;
using Microsoft.Practices.CompositeUI;

namespace ICP.OA.UI.Contact
{
    /// <summary>
    /// 通讯录搜索界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class ContactMainWorkSpace : BasePart
    {
        #region Services
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 构造函数
        public ContactMainWorkSpace()
        {
            InitializeComponent();

            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(this.SearchWorkspace);
                    Workitem.Workspaces.Remove(this.ReportWorkspace);
                    this.SearchWorkspace.PerformLayout();
                    this.ReportWorkspace.PerformLayout();
                    Workitem.Items.Remove(this);
                    Workitem.Dispose();
                    Workitem = null;
                    this.PerformLayout();
                }

            };
        }

        #endregion

    }
}
