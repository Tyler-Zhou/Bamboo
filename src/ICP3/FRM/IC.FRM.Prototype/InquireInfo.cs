using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace IC.FRM.Prototype
{
    public partial class InquireInfo : XtraUserControl
    {
        public InquireInfo()
        {
            InitializeComponent();

            LoadData();
        }

        void LoadData()
        {
            List<ContainerPrice> containerPrice = new List<ContainerPrice>();
            containerPrice.Add(new ContainerPrice() { 
                Container = "20GP",
                ExpectedPrice = "30000",
                Volumn = "20"
            });
            gridControlConPrice.DataSource = containerPrice;
        }

        private void xtraTabPage3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textEdit5_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void InquireInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
