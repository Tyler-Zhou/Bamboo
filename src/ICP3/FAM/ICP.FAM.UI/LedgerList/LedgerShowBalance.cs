using System;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Base;
using ICP.Framework.ClientComponents.UIManagement;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FAM.UI.LedgerList
{
    public partial class LedgerShowBalance : BasePart
    {
        public LedgerShowBalance()
        {
            InitializeComponent();
        }

        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        #endregion


        public Guid myGLid { get; set; }
        public string myGL { get; set; }
        public Guid myCompanyID { get; set; }
        public string myCompany { get; set; }
        public string myCurrency { get; set; }
        public DateTime endDate { get; set; }
        public List<GLBlance> myDataList { get; set; }
        public decimal myRate { get; set; }



        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }

        private void InitControls()
        {
            lab_Auxiliarytext.Text = myCompany;
            lab_Currencytext.Text = myCurrency;
            lab_gltext.Text = myGL;
            lab_time.Text = endDate.ToString("yyyy.MM");
            gcMain.DataSource = myDataList;
            gvDetails.RefreshData();
        }

        private void gvDetails_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "FCurrency" && e.Value != null && e.Value != DBNull.Value && myRate != 0)
            {
                e.DisplayText = ((decimal)e.Value / myRate).ToString("N2");
            }

        }
    }
}
