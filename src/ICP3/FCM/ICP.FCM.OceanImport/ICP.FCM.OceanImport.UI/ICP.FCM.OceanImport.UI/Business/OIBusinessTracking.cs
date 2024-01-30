using System;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.OceanImport.UI
{
    [ToolboxItem(false)]
    public partial class OIBusinessTracking : BaseEditPart
    {
        public OIBusinessTracking()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.gcBoxList.DataSource = null;
                this.gcBoxList.Click -= this.gcBoxList_Click;
                
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

        private void gcBoxList_Click(object sender, EventArgs e)
        {

        }
    }
}
