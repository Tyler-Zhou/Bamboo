using System.ComponentModel;
using Microsoft.Practices.CompositeUI;

namespace ICP.Common.UI.CC
{
    [ToolboxItem(false)]
    public partial class CCReportPart : ICP.Framework.ClientComponents.UIFramework.BasePart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public CCReportPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
    }
}
