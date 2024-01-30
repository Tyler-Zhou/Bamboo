using DevExpress.XtraEditors;
using System;

namespace ICP.FAM.UI.Invoice
{
    public partial class SetTCVersionForm : XtraForm
    {
        string tcVersion = string.Empty;

        public string TCVersion
        {
            get { return tcVersion; }
            //set { tcVersion = value; }
        }
        public SetTCVersionForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            tcVersion = txtVersion.Text;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
