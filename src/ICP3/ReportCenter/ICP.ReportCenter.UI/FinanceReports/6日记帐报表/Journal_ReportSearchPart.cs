using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.ReportCenter.UI.Comm;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.FinanceReports
{   
    /// <summary>
    /// 日记账报表
    /// </summary>
    [ToolboxItem(false)]
    public partial class Journal_ReportSearchPart : ReportBaseSearchPart
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


        public RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }

        #endregion

        #region  init

        public Journal_ReportSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.OnSearched = null;

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

            dteFrom.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);//本月头
            dteTo.DateTime = Utility.GetEndDate(DateTime.Now);//今天底
            chkAmount.CheckedChanged += delegate
            {
                seMaxAmount.Enabled = seMinAmount.Enabled = chkAmount.Checked;
            };
            chkCreateDate.CheckedChanged += delegate
            {
                dteFrom.Enabled = dteTo.Enabled = chkCreateDate.Checked;
            };
            //int.Parse(numMax.Value.ToString())
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

    

        /// <summary>
        /// From
        /// </summary>
        public DateTime? From
        {
            get
            {
                if (chkCreateDate.Checked == false) return null;

                return DateTime.Parse(dteFrom.DateTime.Date.ToShortDateString());
            }
        }

        /// <summary>
        /// To
        /// </summary>
        public DateTime? To
        {
            get
            {
                if (chkCreateDate.Checked == false) return null;

                return Utility.GetEndDate(dteTo.DateTime);
            }
        }

        /// <summary>
        /// MaxAmount
        /// </summary>
        public decimal? MaxAmount
        {
            get
            {
                if (chkAmount.Checked == false) return null;

                return seMaxAmount.Value;
            }
        }

        /// <summary>
        /// MinAmount
        /// </summary>
        public decimal? MinAmount
        {
            get
            {
                if (chkAmount.Checked == false) return null;

                return seMinAmount.Value;
            }
        }



        #endregion

        #region InterFaces

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
                List<JournalDetailReportData> list = FinanceReportService.GetJournalReportData
                    (this.From
                    ,this.To
                    ,this.MinAmount
                    ,this.MaxAmount
                    , companyIDs.ToArray());

                if (list == null || list.Count == 0)
                {
                    
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Data not find." : "找不到数据.");
                    return null;
                }

                List<ReportParameter> paramList = new List<ReportParameter>();

                paramList.Add(new ReportParameter("FromDate", dteFrom.DateTime.ToShortDateString()));
                paramList.Add(new ReportParameter("ToDate", dteTo.DateTime.ToShortDateString()));
                paramList.Add(new ReportParameter("CompanyName",this.chkcmbCompany.EditText));

                ReportDataSource listreitem = new ReportDataSource();
                SpecifyGLCode sdata = new SpecifyGLCode();

                #region 转为USD
                foreach (JournalDetailReportData item in list)
                {

                    string ForeignAmt = "-";
                    string RateInfo = "-";
                    if (item.CurrencyID != RateHelper.USDCurrencyID)
                    {
                        decimal Rate = RateHelper.GetRate(item.CurrencyID, RateHelper.USDCurrencyID, DateTime.Now, RateHelper.RateList);

                        decimal amt = item.DRAmount > 0 ? item.DRAmount / Rate : item.CRAmount / Rate;
                        ForeignAmt = "US$" + amt.ToString("F2");
                        RateInfo = "1 United States Dollars equals " + Rate.ToString("F5") + item.CurrencyName;
                    }

                    sdata.Table_Miscellaneous.AddTable_MiscellaneousRow(item.ID.ToString(),
                                                                           item.JournalPostDate,
                                                                           item.Remark,
                                                                           RateInfo,
                                                                           item.GLDescription,
                                                                           item.DRAmount.ToString("F2"),
                                                                           item.CRAmount.ToString("F2"),
                                                                           ForeignAmt);
                    
                }

                #endregion


                ReportData rd = new ReportData();
                rd.IsLocalReport = true;
                rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.MiscellaneousReport.rdlc";
                rd.Parameters = paramList;
                List<ReportDataSource> ds = new List<ReportDataSource>();
                ds.Add(new ReportDataSource("SpecifyGLCode_Table_Miscellaneous", sdata.Table_Miscellaneous));
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
