using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Reporting.WinForms;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using ICP.ReportCenter.ServiceInterface;
using ICP.ReportCenter.ServiceInterface.DataObjects;


namespace ICP.ReportCenter.UI
{
    [ToolboxItem(false)]
    public partial class ProfitTotalSearchPart : ReportBaseSearchPart
    {
        public ProfitTotalSearchPart()
        {
            InitializeComponent();
        }

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        public ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        public IFinanceReportService FinanceReportService
        {
            get
            {
                return ServiceClient.GetService<IFinanceReportService>();
            }
        }
        public IReportCenterService ReportCenterService
        {
            get
            {
                return ServiceClient.GetService<IReportCenterService>();
            }
        }

        #endregion

        #region 初始化
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
            this.operationDatePart1.SetLastMonth();

            //List<EnumHelper.ListItem<ExpenseHappenType>> happenType = EnumHelper.GetEnumValues<ExpenseHappenType>(LocalData.IsEnglish);
            //this.cmbHappen.Properties.BeginUpdate();
            //this.cmbHappen.Properties.Items.Clear();
            //foreach (var item in happenType)
            //{
            //    cmbHappen.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            //}
            //cmbHappen.Properties.EndUpdate();
            //this.cmbHappen.SelectedIndex = 0;
        }
        #endregion

        #region 查询
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.btnSearch.Enabled = false;
            try
            {
                if (OnSearched != null)
                {
                    using (new CursorHelper())
                    {
                        OnSearched(this, GetData());
                    }
                }
            }
            finally
            {
                this.btnSearch.Enabled = true;
            }
        }
        public override object GetData()
        {
            try
            {

                List<ProfitTotalReport> list = ReportCenterService.GetProfitTotalList(
                                                                 this.operationDatePart1.FromDate,
                                                                 this.operationDatePart1.ToDate);

                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("FromDate", operationDatePart1.FromDate.ToShortDateString()));
                paramList.Add(new ReportParameter("ToDate", operationDatePart1.ToDate.ToShortDateString()));
                paramList.Add(new ReportParameter("CurrencyName", "RMB"));

                ReportData rd = new ReportData();
                rd.IsLocalReport = true;
                rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.UFProfitTotalReport.rdlc";
                rd.Parameters = paramList;
                List<ReportDataSource> ds = new List<ReportDataSource>();
                ds.Add(new ReportDataSource("GLDataReport", list));
                rd.DataSource = ds;
                return rd;

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, (LocalData.IsEnglish ? "Get Report Data Failed." : "获取报表数据失败.") + ex.ToString());
                return null;
            } 
        }
        #endregion

    }
}
