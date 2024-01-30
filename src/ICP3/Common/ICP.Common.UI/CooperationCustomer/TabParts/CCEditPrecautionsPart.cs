using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;

namespace ICP.Common.UI.CC
{
    [ToolboxItem(false)]
    public partial class CCEditPrecautionsPart : BasePart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public CCEditPrecautionsPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                }
            };
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Validate();
            bindingSource1.EndEdit();
            var findForm = this.FindForm();
            if (findForm == null) return;
            findForm.DialogResult = DialogResult.OK;
            findForm.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var findForm = this.FindForm();
            if (findForm == null)
                return;
            findForm.DialogResult = DialogResult.Cancel;
            findForm.Close();
        }
    }
}
