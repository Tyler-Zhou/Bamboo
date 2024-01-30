using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.FinanceReports
{  
    /// <summary>
    /// 资产负债表
    /// </summary>
    [ToolboxItem(false)]
    public partial class BalanceSheetSearchPart : ReportBaseSearchPart
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

        public BalanceSheetSearchPart()
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

                this.rbStandard.Checked = true;
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
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (OnSearched != null)
                    OnSearched(this, GetData());

            }
            finally
            {
                this.btnSearch.Enabled = true;
                this.Cursor = Cursors.Default;
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
                _SearchParameter = new SearchParameter { From = DateTime.Parse(dteFrom.DateTime.ToShortDateString()), To = DateTime.Parse(dteTo.DateTime.ToShortDateString()), CompanyIDs = companyIDs};
                GLDataList list = FinanceReportService.GetBalanceSheet(_SearchParameter.From
                  , Utility.GetEndDate(_SearchParameter.To)
                  , _SearchParameter.CompanyIDs.ToArray());

                if (list == null) return null;

                List<GLData> lieqList = new List<GLData>();
                List<GLData> assetsList = new List<GLData>();

                lieqList = list.LieqList;
                assetsList =list.AssetsList;

                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("DateTo", dteTo.DateTime.ToString("MM-dd-yy")));
                paramList.Add(new ReportParameter("ReportType", string.Empty));
                paramList.Add(new ReportParameter("User", LocalData.UserInfo.LoginName));
                paramList.Add(new ReportParameter("CreateDate", DateTime.Now.ToString("MM-dd-yy")));
                paramList.Add(new ReportParameter("CompanyName",this.chkcmbCompany.EditText));

                ReportData rd = new ReportData();
                rd.IsLocalReport = true;
                rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.BalanceSheetReport.rdlc";
                rd.Parameters = paramList;
                List<ReportDataSource> ds = new List<ReportDataSource>();

                ds.Add(new ReportDataSource("BalanceSheetDataSource", assetsList));
                ds.Add(new ReportDataSource("BalanceSheetDataSource1", lieqList));
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

        ReportData GetDatailData(IList<ReportParameter> parameters)
        {
            try
            {

                List<ReportBillType> billTypes = new List<ReportBillType>();
                //billTypes.Add(ReportBillType.AR);
                //billTypes.Add(ReportBillType.Deposit);
                //billTypes.Add(ReportBillType.Check);
                //billTypes.Add(ReportBillType.DRCR);
                //billTypes.Add(ReportBillType.AP);
                //billTypes.Add(ReportBillType.Journal);

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
