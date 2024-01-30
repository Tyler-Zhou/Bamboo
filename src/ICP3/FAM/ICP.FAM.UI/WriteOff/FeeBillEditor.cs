using System.ComponentModel;

using Microsoft.Practices.CompositeUI;

using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FAM.UI.WriteOff
{
    [ToolboxItem(false)]
    public partial class FeeBillEditor : BaseEditPart
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public FeeBillEditor()
        {
            InitializeComponent();

            DevHelper.FormatSpinEdit(spinEdit1, 2);
        }
    }
}
