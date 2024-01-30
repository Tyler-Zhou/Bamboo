using System;
using System.Drawing;
using DevExpress.XtraEditors;

namespace ICP.FAM.UI.WriteOff
{
    public partial class UCRemarkControl : XtraUserControl
    {
        public UCRemarkControl()
        {
            InitializeComponent();
        }


        public string Remark
        {
            get
            {
                return txtRemark.Text;
            }
            set
            {
                txtRemark.Text = value;
            }
        }

        public void ShowControl(object sender, EventArgs e)
        {
            SuspendLayout();

            Show();
            txtRemark.Focus();

            TextEdit textEdit = (TextEdit)sender;

            int x= textEdit.Location.X ;
            int y = textEdit.Location.Y-75;

            Location = new Point(x,y);


            Width = textEdit.Width+2;
            Height = 70;

            ResumeLayout(false);

        }
    }
}
