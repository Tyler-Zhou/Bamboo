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
    public partial class CheckDepositCASearchPart : ReportBaseSearchPart
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

        public CheckDepositCASearchPart()
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

            Utility.BulidComboboxItem<CheckDepositDateType>(cmbDateType, 2);
            Utility.BulidComboboxItem<CheckDepositGroupType>(cmbGroupBy, 0);
        }

        #endregion

        #region 属性

        /// <summary>
        /// 排序说明
        /// </summary>
        public CheckType CheckType
        {
            get
            {
                if (rdoAR.Checked) return CheckType.AR;
                else return CheckType.AP;
            }
        }

        public string DateTypeString
        {
            get
            {
                CheckDepositDateType dateType = (CheckDepositDateType)(Enum.Parse(typeof(CheckDepositDateType), cmbDateType.EditValue.ToString()));

                if (dateType== CheckDepositDateType.CreateDate)
                {
                    //建立时间
                    return "Input Date: ";
                }
                else if (dateType== CheckDepositDateType.DuDate)
                {
                    //到账时间
                    return "Bank Date: ";
                }
                //核销时间
                return "Paid Date: ";
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
                _SearchParameter = new SearchParameter
                {
                    checkType = this.CheckType,
                    CompanyIDs = this.treeBoxSalesDep.CompanyIDs,
                    dateType = (short)(CheckDepositDateType)(Enum.Parse(typeof(CheckDepositDateType), cmbDateType.EditValue.ToString())),
                    from = DateTime.Parse(dteFrom.DateTime.Date.ToShortDateString()),
                    to = Utility.GetEndDate(dteTo.DateTime),
                    groupBy = (short)(CheckDepositGroupType)(Enum.Parse(typeof(CheckDepositGroupType), cmbGroupBy.EditValue.ToString()))
                };


                List<RepCACheckDepositData> list = this.FinanceReportService.GetCheckListReportDataCA(
                    _SearchParameter.checkType
                    , _SearchParameter.dateType
                    , _SearchParameter.from
                    , _SearchParameter.to
                    , _SearchParameter.groupBy
                    , _SearchParameter.CompanyIDs.ToArray());

                List<ReportParameter> paramList = new List<ReportParameter>();
                string typename = string.Empty;
                if (rdoAR.Checked)typename = "Deposit";
                else typename = "Check";

                paramList.Add(new ReportParameter("DateBegin", _SearchParameter.from.ToShortDateString()));
                paramList.Add(new ReportParameter("DateEnd", _SearchParameter.to.ToShortDateString()));
                paramList.Add(new ReportParameter("strDateType", DateTypeString));
                paramList.Add(new ReportParameter("typename", typename));
                paramList.Add(new ReportParameter("username", LocalData.UserInfo.LoginName));


                ReportData rd = new ReportData();
                rd.IsLocalReport = true;
                rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.RPT_CheckDepositReportCA.rdlc";
                rd.Parameters = paramList;
                List<ReportDataSource> ds = new List<ReportDataSource>();
                ds.Add(new ReportDataSource("RepCACheckDepositData", list));
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
        #endregion

        class SearchParameter
        {
            public CheckType checkType { get; set; }
            public List<Guid> CompanyIDs { get; set; }
            public short dateType { get; set; }
            public DateTime from { get; set; }
            public DateTime to { get; set; }
            public short groupBy { get; set; }
        }

    }
}
