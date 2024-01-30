using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FAM.UI
{
    public partial class ReportViewLedgerInfo : XtraForm
    {
        public ReportViewLedgerInfo()
        {
            InitializeComponent();
        }
        public Guid ID
        {
            get;
            set;
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                BindDataInof();
            }
        }
        private void BindDataInof()
        {
            if (ID == null || ID == Guid.Empty)
            {
                return;

            }
            LedgerMasters MasterInfo=FinanceService.GetLedgerMastersInfo(ID, LocalData.IsEnglish);
            if (MasterInfo == null)
            {
                return;
            }

            txtNo.Text = MasterInfo.No;
            if (MasterInfo.DATE != null)
            {
                dteDate.DateTime = MasterInfo.DATE.Value;
            }
            if (MasterInfo.ReceiptQty != null)
            {
                txtReceiptQty.Text = MasterInfo.ReceiptQty.ToString();
            }
            labAuditorName.Text = MasterInfo.Auditor;
            labFinanceManagerName.Text = MasterInfo.FinanceManager;
            labCashierName.Text = MasterInfo.Cashier;
            labCreateName.Text = MasterInfo.Creator;


            List<Ledgers> Detailist = MasterInfo.DetailList;

            Ledgers totalInfo = new Ledgers();
            totalInfo.Remark = "合计";
            totalInfo.CRAmt = (from d in Detailist select d.CRAmt).Sum();
            totalInfo.DRAmt = (from d in Detailist select d.DRAmt).Sum();

            Detailist.Add(totalInfo);

            bsList.DataSource = Detailist;
            bsList.ResetBindings(false);
        }

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            Ledgers item = bsList.Current as Ledgers;
            if (item == null)
            {
                item = new Ledgers();
            }
            dteDetailData.DateTime = item.Date;
            txtCustomerName.Text = item.Customer;
            txtDeptName.Text = item.Dept;
            txtPersonalName.Text = item.Personal;

        }

    }
}
