using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FAM.UI.WriteOff.Dialogs
{
    public partial class BillsFastSelector : XtraUserControl
    {

        public BillsFastSelector()
        {
            InitializeComponent();

            Load += new EventHandler(BillsFastSelector_Load);
            Disposed += delegate {
                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList.Dispose();
            
            };
        }

        void BillsFastSelector_Load(object sender, EventArgs e)
        {
            List<CurrencyBillList> list = new List<CurrencyBillList>();

            for (int i = 0; i < 15; i++)
            {
                CurrencyBillList temp = UIModelHelper.GetNormalObject<CurrencyBillList>();
                temp.OperationNO = "SZGSOE11070050" + i.ToString();
                temp.BillNO = temp.OperationNO + "A";
                temp.BillRefNO = temp.OperationNO + "A";

                list.Add(temp);
            }

            bsList.DataSource = list;

            gvMain.BestFitColumns();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ////return DialogResult.OK;
        }
    }
}
