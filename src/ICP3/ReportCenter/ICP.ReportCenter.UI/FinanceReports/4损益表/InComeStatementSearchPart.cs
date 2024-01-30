using System;
using System.Collections.Generic;
using System.ComponentModel;

using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.FinanceReports
{
    /// <summary>
    /// 损益表
    /// </summary>
    [ToolboxItem(false)]
    public partial class InComeStatementSearchPart : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public IFinanceReportService FinanceReportService
        {
            get
            {
                return ServiceClient.GetService<IFinanceReportService>();
            }
        }

        #endregion

        #region  init

        public InComeStatementSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
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

            dteFrom.DateTime = DateTime.Now.Date;//今天
            dteTo.DateTime = Utility.GetEndDate(DateTime.Now);

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

        private string _DetailForGLSummaryName = "DetailForGLSummary";

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
                bool isNASolutions = false;

                if (LocalData.UserInfo.DefaultCompanyID != null && LocalData.UserInfo.DefaultCompanyID != Guid.Empty)
                {
                    ConfigureInfo configInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                    if (configInfo != null && configInfo.SolutionID != Guid.Empty &&
                        (configInfo.SolutionID == new Guid("2A254061-0465-4B07-81CF-E18198B45802")
                     || configInfo.SolutionID == new Guid("F8B3FA70-1AB2-DD11-B89A-001372F6CE5E")))
                    {
                        //北美 与 加拿大的 解决方案
                        isNASolutions = true;
                    }
                }


                _SearchParameter = new SearchParameter { From = DateTime.Parse(dteFrom.DateTime.ToShortDateString()), To = DateTime.Parse(dteTo.DateTime.ToShortDateString()), CompanyIDs = companyIDs };
                GLDataAndTotalInfo items = FinanceReportService.GetIncome(_SearchParameter.From
                  , Utility.GetEndDate(_SearchParameter.To)
                  , _SearchParameter.CompanyIDs.ToArray(),
                  isNASolutions);



                List<GLData> list = new List<GLData>();
                GLDataTotal total = new GLDataTotal();
                //List<CurrencyList> clist = new List<CurrencyList>();
                string Currency = "";
                if (chkcmbCompany.EditValue != null)
                {
                    foreach (object company in chkcmbCompany.EditValue)
                    {
                        ConfigureInfo configInfo = ConfigureService.GetCompanyConfigureInfo((Guid)company);
                        if (configInfo != null)
                        {
                            Currency = configInfo.StandardCurrency;
                            break;
                        }
                    }
                }

                else
                {
                    ConfigureInfo configInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                    Currency = configInfo.StandardCurrency;
                }


                list = items.GLDataList;
                total = items.DataTottal;

                if (total == null)
                {
                    total = new GLDataTotal();
                }


                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("DateFrom", dteFrom.DateTime.ToString("MM/dd/yy").Replace("-", "/")));
                paramList.Add(new ReportParameter("DateTo", dteTo.DateTime.ToString("MM/dd/yy").Replace("-", "/")));
                paramList.Add(new ReportParameter("ReportType", "Accrual Basis"));
                paramList.Add(new ReportParameter("User", LocalData.UserInfo.LoginName));
                paramList.Add(new ReportParameter("CreateDate", DateTime.Now.ToString("MM/dd/yy").Replace("-", "/")));
                paramList.Add(new ReportParameter("CompanyName", this.chkcmbCompany.EditText));
                paramList.Add(new ReportParameter("InComeTotal", total.IncomeAmount.ToString()));
                paramList.Add(new ReportParameter("CostOfSales", total.CostAmount.ToString()));
                paramList.Add(new ReportParameter("otherAmount", total.OtherAmount.ToString()));
                paramList.Add(new ReportParameter("CrossProfit", total.CrossIncome.ToString()));
                paramList.Add(new ReportParameter("IsNASolutions", isNASolutions.ToString()));
                paramList.Add(new ReportParameter("CurrencyCode", Currency));


                ReportData rd = new ReportData();
                rd.IsLocalReport = true;
                rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.InComeStatement.rdlc";
                rd.Parameters = paramList;
                List<ReportDataSource> ds = new List<ReportDataSource>();


                ds.Add(new ReportDataSource("IncomeStatementDataSource", list));
                rd.DataSource = ds;
                return rd;
            }
            catch (Exception ex)
            {
                ICP.Framework.ClientComponents.Controls.Utility.ShowMessage((LocalData.IsEnglish ? "Get Report Data Failed." : "获取报表数据失败.") + ex.Message);
                //LocalCommonServices.ErrorTrace.SetErrorInfo(this, (LocalData.IsEnglish ? "Get Report Data Failed." : "获取报表数据失败.") + ex.ToString());
                return null;
            }
        }

        public override ReportData GetDrillthroughData(string reportEmbeddedResource, IList<ReportParameter> parameters)
        {
            if (_SearchParameter == null) return null;

            if (reportEmbeddedResource.Contains(_DetailForGLSummaryName))
            {
                return GetDatailData(parameters);
            }
            else
            {
                return GetCheckedReportForGLDetailData(parameters);
            }

        }

        ReportData GetDatailData(IList<ReportParameter> parameters)
        {
            try
            {

                List<ReportBillType> billTypes = new List<ReportBillType>();
                billTypes.Add(ReportBillType.AR);
                billTypes.Add(ReportBillType.Deposit);
                billTypes.Add(ReportBillType.Check);
                billTypes.Add(ReportBillType.DRCR);
                billTypes.Add(ReportBillType.AP);
                billTypes.Add(ReportBillType.Journal);
                billTypes.Add(ReportBillType.Clearance);

                List<LedgerData> data = FinanceReportService.GetGLDetail(parameters[0].Values[0].ToString()
                    , _SearchParameter.From
                    , Utility.GetEndDate(_SearchParameter.To)
                    , _SearchParameter.CompanyIDs.ToArray()
                    , billTypes.ToArray());

                ReportData rd = new ReportData();
                List<ReportDataSource> ds = new List<ReportDataSource>();
                ds.Add(new ReportDataSource("LedgerData", data));
                rd.DataSource = ds;
                return rd;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, (LocalData.IsEnglish ? "Get Report Data Failed." : "获取报表数据失败.") + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 由于数据原因未验证正确性 TO DO
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private ReportData GetCheckedReportForGLDetailData(IList<ReportParameter> parameters)
        {
            Guid BillId = new Guid(parameters[0].Values[0].ToString());
            string billTypeString = parameters[1].Values[0].ToString();
            ReportBillType billType = (ReportBillType)Enum.Parse(typeof(ReportBillType), billTypeString);

            if (billType == ReportBillType.Check || billType == ReportBillType.Deposit)
            {
                List<CheckData> checkDatas = new List<CheckData>();
                checkDatas.Add(FinanceReportService.GetCheckData(BillId));
                if (checkDatas == null) return null;

                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("CompanyName", checkDatas[0].CompanyName));
                paramList.Add(new ReportParameter("CustomerName", checkDatas[0].CustomerName));
                paramList.Add(new ReportParameter("No", checkDatas[0].No));

                ReportData rd = new ReportData();
                rd.Parameters = paramList;
                List<ReportDataSource> ds = new List<ReportDataSource>();

                if (billType == ReportBillType.Check)
                    ds.Add(new ReportDataSource("PaymentCheckData", checkDatas));
                else
                    ds.Add(new ReportDataSource("DepositCheckData", checkDatas));

                rd.DataSource = ds;
                return rd;
            }
            else
            {
                ReportBillData reportBillData = FinanceReportService.GetReportBillData(BillId);
                if (reportBillData == null) return null;

                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("CompanyName", reportBillData.CompanyName));
                paramList.Add(new ReportParameter("CustomerName", reportBillData.CustomerName));
                paramList.Add(new ReportParameter("FinanceDate", reportBillData.AccountDate.ToShortDateString()));
                paramList.Add(new ReportParameter("OperationNo", reportBillData.OperationNo));
                paramList.Add(new ReportParameter("PostUserName", reportBillData.CheckUserName));// customerBillData.PostUserName));
                paramList.Add(new ReportParameter("RefNo", reportBillData.RefNo));
                paramList.Add(new ReportParameter("No", reportBillData.No));

                ReportData rd = new ReportData();
                rd.Parameters = paramList;
                List<ReportDataSource> ds = new List<ReportDataSource>();
                ds.Add(new ReportDataSource("CustomerBillFeeData", reportBillData.Fees));
                rd.DataSource = ds;
                return rd;
            }
        }



        #endregion

        class SearchParameter
        {
            public DateTime From { get; set; }
            public DateTime To { get; set; }
            public List<Guid> CompanyIDs { get; set; }
        }

    }
}
