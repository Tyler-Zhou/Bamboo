using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.AirImport.UI
{
    [ToolboxItem(false)]
    public partial class OIBusinessTracking : BaseEditPart
    {
        public OIBusinessTracking()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

    }
}
