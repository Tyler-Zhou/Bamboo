using System;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FCM.OceanExport.UI.Booking.BaseEdit
{
    public partial class AddSoNo : BaseEditPart
    {
        public AddSoNo()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.AddS = null;
            };
        }

        public delegate void AddSoNOs(string SoNO);
        public event AddSoNOs AddS;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (AddS != null)
            {
                this.AddS(this.txtSoNo.Text);
            }
        }
    }
}
