using System;
using System.Collections.Generic;
using System.ComponentModel;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.FinanceReports
{   
    /// <summary>
    /// 银行对账表
    /// </summary>
    [ToolboxItem(false)]
    public partial class BankstandingSearchPart : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceReportService FinanceReportService
        {
            get
            {
                return ServiceClient.GetService<IFinanceReportService>();
            }
        }

        #endregion

        #region  init

        public BankstandingSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.OnSearched = null;
                this._SearchParameter = null;
                if (Workitem != null)
                { 
                    Workitem.Items.Remove(this);
                    this.Workitem = null;
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
            dteTo.DateTime = Utility.GetEndDate(DateTime.Now);//今天
        }

        #endregion

        #region ISearchPart 成员

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

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

        #region 属性

        private string _DetailForBankstandingName = "DetailForBankstanding";

   

        /// <summary>
        /// CompanyString
        /// </summary>
        /// <returns></returns>
        public string CompanyString
        {
            get
            {
                return LocalData.UserInfo.DefaultCompanyName;
            }
        }

        #endregion

        #region InterFaces
        /// <summary>
        /// 缓存上一次查询的参数,用于下钻时作为搜索的依据
        /// </summary>
        SearchParameter _SearchParameter = null;
        public override object GetData()
        {
            List<Guid> companyIDs = this.chkcmbCompany.CompanyIDs;
            if (companyIDs == null || companyIDs.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Company must choose one." : "请至少选择一个公司.");
                return null;
            }

            try
            {
                _SearchParameter = new SearchParameter { To = DateTime.Parse(dteTo.DateTime.ToShortDateString()), CompanyIDs = companyIDs, HasBankDate = chkNoBankDate.Checked };
                List<BankOutStandingData> list = FinanceReportService.GetBankOutStandingDataTotal(
                  Utility.GetEndDate(_SearchParameter.To)
                  , _SearchParameter.CompanyIDs.ToArray()
                  , _SearchParameter.HasBankDate);

                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("EndingDate", dteTo.DateTime.ToShortDateString()));
                paramList.Add(new ReportParameter("CompanyName", this.chkcmbCompany.EditText));
                paramList.Add(new ReportParameter("IsHasBankdate", _SearchParameter.HasBankDate.ToString()));

                ReportData rd = new ReportData();
                rd.IsLocalReport = true;
                rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.BankOutstandingTotal.rdlc";
                rd.Parameters = paramList;
                List<ReportDataSource> ds = new List<ReportDataSource>();
                ds.Add(new ReportDataSource("BankOutStandingData", list));
                rd.DataSource = ds;
                return rd;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, (LocalData.IsEnglish ? "Get Report Data Failed." : "获取报表数据失败.") + ex.ToString());
                return null;
            }
        }

        public override ReportData GetDrillthroughData(string reportEmbeddedResource, IList<ReportParameter> parameters)
        {
            if (_SearchParameter == null) return null;

            try
            {
                List<BankOutStandingDetailData> data = FinanceReportService.GetBankOutStandingDataDetail
                    (new Guid(parameters[0].Values[0].ToString())
                    ,_SearchParameter.To
                    , _SearchParameter.CompanyIDs.ToArray()
                    , _SearchParameter.HasBankDate);


                ReportData rd = new ReportData();
                List<ReportDataSource> ds = new List<ReportDataSource>();
                ds.Add(new ReportDataSource("BankOutStandingDetailData", data));
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

        class SearchParameter
        {
            public DateTime To { get; set; }
            public List<Guid> CompanyIDs { get; set; }
            public bool HasBankDate { get; set; }
        }

    }
}
