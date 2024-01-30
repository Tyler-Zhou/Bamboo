using System;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIManagement;
using DevExpress.XtraEditors;

namespace ICP.FCM.OceanExport.UI.BL.MBL
{
    public partial class AddHblNum : BasePart
    {

        public int num;

        public AddHblNum()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                num = Convert.ToInt32(numQuantity.Value);
                this.FindForm().Close();
            }
            catch
            {
                XtraMessageBox.Show("请填写正确的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }
}
