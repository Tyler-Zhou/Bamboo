using System;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIManagement;
using DevExpress.XtraEditors;

namespace ICP.FCM.OceanExport.UI.BL.MBL
{
    public partial class TypeOfGood : BasePart
    {
        public TypeOfGood()
        {
            InitializeComponent();
        }

        public int goodType = 0;
        public string Centigrade;
        public string CentigradeF;
        public string DangerousClass;
        public string DangerousProperty;
        public string DangerousPage;
        public string DangerousNo;

        private void btnOK_Click(object sender, EventArgs e)
        {
            string message = CheckSet();
            if (string.IsNullOrEmpty(message))
            {
                goodType = rdoTyoe.SelectedIndex + 1;
                Centigrade = txtC.Text;
                CentigradeF = txtF.Text;
                DangerousClass = txtClass.Text;
                DangerousProperty = txtProperty.Text;
                DangerousPage = txtPage.Text;
                DangerousNo = txtUNNO.Text;
            }
            else
            {
                XtraMessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.FindForm() != null)
            {
                this.FindForm().DialogResult = DialogResult.OK;
                this.FindForm().Close();
            }
        }

        private string CheckSet()
        {
            string message = string.Empty;
            if (rdoTyoe.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(txtClass.Text) || string.IsNullOrEmpty(txtUNNO.Text))
                {
                    message += "您选择的是危险品，必须输入危险品类型和编号！";
                }
            }
            else if (rdoTyoe.SelectedIndex == 0)
            {
                if (string.IsNullOrEmpty(txtC.Text))
                {
                    message += "您选择的是冷藏品，必须输入摄氏度！";
                }
            }
            else if (rdoTyoe.SelectedIndex < 0)
            {
                message += "您还未选择特殊类型！";
            }

            return message;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.FindForm() != null)
            {
                this.FindForm().Close();
            }
        }

        private void TypeOfGood_Load(object sender, EventArgs e)
        {
            rdoTyoe.SelectedIndex = goodType;
            txtC.Text = Centigrade;
            txtF.Text = CentigradeF;
            txtClass.Text = DangerousClass;
            txtProperty.Text = DangerousProperty;
            txtPage.Text = DangerousPage;
            txtUNNO.Text = DangerousNo;
        }


    }
}
