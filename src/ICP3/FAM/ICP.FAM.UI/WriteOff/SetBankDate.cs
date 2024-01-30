using System;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FAM.UI.WriteOff
{
    public partial class SetBankDate : BasePart
    {
        public SetBankDate()
        {
            InitializeComponent();
        }

        public DateTime BankDate
        {
            get
            {
                return DateTime.SpecifyKind(dteBankDate.DateTime,DateTimeKind.Unspecified);
            }
        }

        private void SetBankDate_Load(object sender, EventArgs e)
        {
            dteBankDate.DateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
        }

        private void btnClose_Click(object sender, EventArgs e)
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
