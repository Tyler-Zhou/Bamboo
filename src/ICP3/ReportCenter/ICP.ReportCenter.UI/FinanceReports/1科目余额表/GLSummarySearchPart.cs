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
    /// 科目余额报表
    /// </summary>
    [ToolboxItem(false)]
    public partial class GLSummarySearchPart : ReportBaseSearchPart
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

        public GLSummarySearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
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
            dteFrom.DateTime = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(-1);//上月头
            dteTo.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);//上月底

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

            List<ReportBillType> billTypes = this.ucReportType.BillTypes;
            if (billTypes == null || billTypes.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "BillType must choose one." : "请至少选择一种帐单类型.");
                return null;
            }

            try
            {
                _SearchParameter = new SearchParameter { From = DateTime.Parse(dteFrom.DateTime.ToShortDateString()), To = dteTo.DateTime, CompanyIDs = companyIDs, BillTypes = billTypes };

                List<GLData> list = FinanceReportService.GetGLSummary(_SearchParameter.From
                    ,Utility.GetEndDate(_SearchParameter.To)
                    , _SearchParameter.CompanyIDs.ToArray()
                    , _SearchParameter.BillTypes.ToArray());

                List<ReportParameter> paramList = new List<ReportParameter>();

                paramList.Add(new ReportParameter("DateFrom", dteFrom.DateTime.ToShortDateString()));
                paramList.Add(new ReportParameter("DateTo", dteTo.DateTime.ToShortDateString()));

                paramList.Add(new ReportParameter("User", LocalData.UserInfo.LoginName));
                paramList.Add(new ReportParameter("ReportType", this.ucReportType.RepotTypeString));
                paramList.Add(new ReportParameter("CreateDate", DateTime.Now.ToShortDateString()));
                paramList.Add(new ReportParameter("CompanyName",this.chkcmbCompany.EditText));

                ReportData rd = new ReportData();
                rd.IsLocalReport = true;
                rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.GL_SummaryReport.rdlc";
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
                CheckData data = FinanceReportService.GetCheckData(BillId);


                string customerName = string.Empty;
                string companyName = string.Empty;
                string no = string.Empty;


                if (data != null)
                {
                    customerName = data.CustomerName;
                    companyName = data.CompanyName;
                    no = data.No;
                }

                checkDatas.Add(data);

                if (checkDatas == null) return null;

                List<ReportParameter> paramList = new List<ReportParameter>();


                paramList.Add(new ReportParameter("CompanyName",companyName));
                paramList.Add(new ReportParameter("CustomerName", customerName));
                paramList.Add(new ReportParameter("No", no));

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

        ReportData GetDatailData(IList<ReportParameter> parameters)
        {
            try
            {
                List<LedgerData> data = FinanceReportService.GetGLDetail(parameters[0].Values[0].ToString()
                    , _SearchParameter.From
                    , Utility.GetEndDate(_SearchParameter.To)
                    , _SearchParameter.CompanyIDs.ToArray()
                    , _SearchParameter.BillTypes.ToArray());


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

        #endregion

        class SearchParameter
        {
            public DateTime From{get;set;}
            public DateTime To{get;set;}
            public List<Guid> CompanyIDs{get;set;}
            public List<ReportBillType> BillTypes{get;set;}
        }
    }
}
