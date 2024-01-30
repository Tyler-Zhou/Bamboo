using System;
using System.Collections.Generic;
using System.ComponentModel;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.FinanceOIReports
{
    [ToolboxItem(false)]
    public partial class PrepaidInAdvanceSearchPart : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ICP.FAM.ServiceInterface.IFinanceReportService FinanceReportService
        {
            get
            {
                return ServiceClient.GetService<ICP.FAM.ServiceInterface.IFinanceReportService>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        #endregion

        #region  init

        public PrepaidInAdvanceSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.OnSearched = null;
                this._SearchParameter = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
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
            //科目搜索器
            SearchBoxAdapter.RegisterGLCodeSingleSearchBox(DataFindClientService, this.txtGLCode, false, this.chkcmbCompany.CompanyIDs);
            dteFrom.DateTime = DateTime.Now.AddMonths(-1);
            dteTo.DateTime = DateTime.Now;
        }

        #endregion

        #region 属性

        #endregion

        #region event
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.btnSearch.Enabled = false;
            try
            {
                if (OnSearched != null)
                    using (new CursorHelper())
                    {
                        OnSearched(this, GetData());
                    }

            }
            finally
            {
                this.btnSearch.Enabled = true;
            }
        }

        #endregion

        #region ISearchPart 成员
        SearchParameter _SearchParameter;

        public override object GetData()
        {
            try
            {
                List<Guid> companyIDs = this.chkcmbCompany.CompanyIDs;
                if (companyIDs == null || companyIDs.Count == 0)
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Company must choose one." : "请至少选择一个公司.");
                    return null;
                }
                Guid? customerID = null;
                Guid? glID = null;

                if (this.txtCustomer.Tag != null && this.txtCustomer.Tag != DBNull.Value)
                {
                    customerID = new Guid(txtCustomer.Tag.ToString());
                }

                if (this.txtGLCode.Tag != null && this.txtGLCode.Tag != DBNull.Value)
                {
                    glID = new Guid(txtGLCode.Tag.ToString());
                }

                _SearchParameter = new SearchParameter
                {
                    CompanyIDs = companyIDs,
                    GlCodeID = glID,
                    from = DateTime.Parse(dteFrom.DateTime.Date.ToShortDateString()),
                    to = Utility.GetEndDate(dteTo.DateTime),
                    CustomerID = customerID
                };

                ReportData rd = new ReportData();
                if (chkBalance.Checked)
                {
                    List<GLCheckBalanceData> list = this.FinanceReportService.GetGLCheckBalanceData(
                     _SearchParameter.from
                   , _SearchParameter.to
                   , _SearchParameter.CompanyIDs.ToArray()
                   , _SearchParameter.CustomerID
                   , _SearchParameter.GlCodeID);

                    List<ReportParameter> paramList = new List<ReportParameter>();
                    Guid[] CompanyIDArray = _SearchParameter.CompanyIDs.ToArray();

                    paramList.Add(new ReportParameter("StartDate", _SearchParameter.from.ToShortDateString()));
                    paramList.Add(new ReportParameter("EndingDate", _SearchParameter.to.ToShortDateString()));
                    paramList.Add(new ReportParameter("CompanyIds", chkcmbCompany.EditText));
                    paramList.Add(new ReportParameter("CustomerId", _SearchParameter.CustomerID == null ? null : _SearchParameter.CustomerID.ToString()));
                    paramList.Add(new ReportParameter("GLID", _SearchParameter.GlCodeID == null ? null : _SearchParameter.GlCodeID.ToString()));

                    rd.IsLocalReport = true;
                    rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.RPT_PrepaidInAdvanceCheckBalanceReport.rdlc";
                    rd.Parameters = paramList;
                    List<ReportDataSource> ds = new List<ReportDataSource>();
                    ds.Add(new ReportDataSource("RepCACheckDepositData", list));
                    rd.DataSource = ds;
                }
                else
                {
                    List<PrepaidInAdvanceData> list = this.FinanceReportService.GetPrepaidInAdvanceData(
                                        _SearchParameter.from
                                      , _SearchParameter.to
                                      , _SearchParameter.CompanyIDs.ToArray()
                                      , _SearchParameter.CustomerID
                                      , _SearchParameter.GlCodeID);

                    List<ReportParameter> paramList = new List<ReportParameter>();
                    Guid[] CompanyIDArray = _SearchParameter.CompanyIDs.ToArray();

                    paramList.Add(new ReportParameter("StartDate", _SearchParameter.from.ToShortDateString()));
                    paramList.Add(new ReportParameter("EndingDate", _SearchParameter.to.ToShortDateString()));
                    paramList.Add(new ReportParameter("CompanyIds", chkcmbCompany.EditText));
                    paramList.Add(new ReportParameter("CustomerId", _SearchParameter.CustomerID == null ? null : _SearchParameter.CustomerID.ToString()));
                    paramList.Add(new ReportParameter("GLID", _SearchParameter.GlCodeID == null ? null : _SearchParameter.GlCodeID.ToString()));

                    rd.IsLocalReport = true;
                    rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.RPT_PrepaidInAdvanceReport.rdlc";
                    rd.Parameters = paramList;
                    List<ReportDataSource> ds = new List<ReportDataSource>();
                    ds.Add(new ReportDataSource("RepCACheckDepositData", list));
                    rd.DataSource = ds;
                }
                return rd;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, (LocalData.IsEnglish ? "Get Report Data Failed." : "获取报表数据失败.") + ex.ToString());
                return null;
            }
        }
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        #endregion

        class SearchParameter
        {
            public List<Guid> CompanyIDs { get; set; }
            public Guid? CustomerID { get; set; }
            public Guid? GlCodeID { get; set; }
            public DateTime from { get; set; }
            public DateTime to { get; set; }
        }

        private void txtGLCode_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGLCode.Text))
            {
                txtGLCode.Tag = null;
            }
        }

        private void chkBalance_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBalance.Checked)
            {
                dteFrom.Enabled = false;
            }
            else
                dteFrom.Enabled = true;
        }

    }
}
