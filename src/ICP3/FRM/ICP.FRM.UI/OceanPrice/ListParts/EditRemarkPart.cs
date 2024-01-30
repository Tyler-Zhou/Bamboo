using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FRM.UI.OceanPrice
{
    [ToolboxItem(false)]
    public partial class EditRemarkPart : BasePart
    {
        public EditRemarkPart()
        {
            InitializeComponent();
        }

        public string Remark
        {
            get { return txtRemark.Rtf; }
        }

        public void SetSouce(string remark)
        {
            txtRemark.Rtf = remark;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().DialogResult = DialogResult.Cancel;
            FindForm().Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();
        }
    }
}
