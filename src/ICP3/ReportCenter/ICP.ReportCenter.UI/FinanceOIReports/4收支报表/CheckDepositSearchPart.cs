using System;
using System.Collections.Generic;
using System.ComponentModel;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.ReportCenter.UI.FinanceOIReports
{
    [ToolboxItem(false)]
    public partial class CheckDepositSearchPart : ReportBaseSearchPart
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
        #endregion

        #region  init

        public CheckDepositSearchPart()
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

            dteFrom.DateTime = DateTime.Now;
            dteTo.DateTime = DateTime.Now;

            Utility.BulidComboboxItem<CheckDepositDateType>(cmbDateType, 2);
            Utility.BulidComboboxItem<CheckDepositGroupType>(cmbGroupBy, 0);

            Utility.BulidComboboxItem<CheckDepositSortByType>(cmbSortByType, 0);
        }

        #endregion

        #region 属性

        /// <summary>
        /// 排序说明
        /// </summary>
        public string SortByString
        {
            get
            {
                if (cmbGroupBy.SelectedIndex == 0)
                {
                    return "Vendor/Customer";
                }
                else if (cmbGroupBy.SelectedIndex == 1)
                {
                    return "Bank";
                }
                else 
                {
                    return "Date";
                }
            }
        }

        /// <summary>
        /// 报表说明
        /// </summary>
        public string ReportType
        {
            get
            {
                List<string> strs = new List<string>();
                if (rdoAR.Checked) strs.Add("Check");

                if (rdoAR.Checked ) strs.Add("Deposit");

                if (cmbDateType.SelectedIndex == 0) strs.Add("InputDate");
                else if (cmbDateType.SelectedIndex == 1) strs.Add("DuDate");
                else if (cmbDateType.SelectedIndex == 2) strs.Add("CheckDate");

                strs.Add(SortByString);

                string tempString = string.Empty;
                for (int i = 0; i < strs.Count-1; i++)
                {
                    tempString += strs[i] + ",";
                }
                return tempString;
            }
        }

        /// <summary>
        /// 排序说明
        /// </summary>
        public CheckType CheckType
        {
            get
            {
                if (rdoAR.Checked) return CheckType.AR;
                else if (rdoAP.Checked) return CheckType.AP;
                else return CheckType.None;
            }
        }

        #endregion

        #region event
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

        #endregion

        #region ISearchPart 成员
        SearchParameter _SearchParameter;

        public override object GetData()
        {
            try
            {
                _SearchParameter = new SearchParameter
                {
                    checkType = this.CheckType,
                    CompanyIDs = this.treeBoxSalesDep.CompanyIDs,
                    dateType = (short)(CheckDepositDateType)(Enum.Parse(typeof(CheckDepositDateType), cmbDateType.EditValue.ToString())),
                    from = DateTime.Parse(dteFrom.DateTime.Date.ToShortDateString()),
                    to = Utility.GetEndDate(dteTo.DateTime),
                    groupBy = (short)(CheckDepositGroupType)(Enum.Parse(typeof(CheckDepositGroupType), cmbGroupBy.EditValue.ToString()))
                };


                List<RepCheckData> list = this.FinanceReportService.GetCheckListReportData(
                    _SearchParameter.checkType
                    , _SearchParameter.dateType
                    , _SearchParameter.from
                    , _SearchParameter.to
                    , _SearchParameter.groupBy
                    , _SearchParameter.CompanyIDs.ToArray());

                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("DateBegin", dteFrom.DateTime.ToShortDateString()));
                paramList.Add(new ReportParameter("DateEnd", dteTo.DateTime.ToShortDateString()));
                paramList.Add(new ReportParameter("ReportType", this.ReportType));
                paramList.Add(new ReportParameter("SortByString", this.SortByString));
                paramList.Add(new ReportParameter("GroupType", cmbGroupBy.SelectedIndex.ToString()));
                paramList.Add(new ReportParameter("DateType", _SearchParameter.dateType.ToString()));
                ReportData rd = new ReportData();
                rd.IsLocalReport = true;
                rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.RPT_CheckDepositReport.rdlc";
                rd.Parameters = paramList;
                List<ReportDataSource> ds = new List<ReportDataSource>();
                ds.Add(new ReportDataSource("RepCheckData", list));
                rd.DataSource = ds;
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

        public override ReportData GetDrillthroughData(string reportEmbeddedResource, IList<ReportParameter> parameters)
        {
            try
            {

                Guid checkID = new Guid(parameters[0].Values[0].ToString());

                //List<RepCheckDetailData> list = new List<RepCheckDetailData>();

                List<RepCheckDetailData> list = FinanceReportService.GetCheckDetailReportData(
                                                _SearchParameter.checkType,
                                                 _SearchParameter.dateType,
                                                 _SearchParameter.from,
                                                 _SearchParameter.to,
                                                 _SearchParameter.groupBy,
                                                 checkID,
                                                 _SearchParameter.CompanyIDs.ToArray());

                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("SortBy", cmbSortByType.SelectedIndex.ToString()));//0支票号,1日期


                ReportData rd = new ReportData();
                List<ReportDataSource> ds = new List<ReportDataSource>();
                rd.Parameters = paramList;
                ds.Add(new ReportDataSource("RepCheckDetailData", list));
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
            public CheckType checkType { get; set; }
            public List<Guid> CompanyIDs { get; set; }
            public short dateType { get; set; }
            public DateTime? from { get; set; }
            public DateTime? to { get; set; }
            public short groupBy { get; set; }
        }

    }
}
