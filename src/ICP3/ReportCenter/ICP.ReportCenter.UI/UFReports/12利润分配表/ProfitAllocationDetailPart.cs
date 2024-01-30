using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.Framework.ClientComponents;
using ICP.Framework.CommonLibrary.Client;
using ICP.ReportCenter.ServiceInterface;
using ICP.ReportCenter.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.ReportCenter.UI
{
    public partial class ProfitAllocationDetailPart : ReportBaseSearchPart
    {
        public ProfitAllocationDetailPart()
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

            if (!LocalCommonServices.PermissionService.HaveActionPermission(ActionConstants.Report_Total))
            {
                this.chkTotal.Visible = false;
                this.chkTotal.Enabled = false;
            }
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
            List<Guid> companyIDs = this.chkcmbCompany.CompanyIDs;
            if (!this.chkTotal.Checked&&(companyIDs == null || companyIDs.Count == 0))
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Company must choose one." : "请至少选择一个公司.");
                return null;
            }
            try
            {
                ReportData rd = new ReportData();
                if (this.chkTotal.Checked)
                {
                    #region 汇总
                    //上年年度损益调整
                    List<CompanyBalanceSheet> list = ReportCenterService.GetProfitAllocationTotalList(
                                                          this.operationDatePart1.FromDate,
                                                          this.operationDatePart1.ToDate);
                    
                    List<ProfitTotalReport> tlist = ReportCenterService.GetProfitTotalList(
                                                             this.operationDatePart1.FromDate,
                                                             this.operationDatePart1.ToDate);

                    List<CompanyBalanceSheet> datalist = new List<CompanyBalanceSheet>();
                    foreach (ProfitTotalReport report in tlist)
                    {
                        CompanyBalanceSheet data = ConvertSheet(report);
                        datalist.Add(data);
                    }
                    foreach (CompanyBalanceSheet sheet in list)
                    {
                        if (sheet.GLName == "一、当月净利润累计")
                        {
                            continue;
                        }
                        else if (sheet.GLName == "二、可供分配的利润")
                        {
                            sheet.GLName = "七、可供分配的利润";
                            datalist.Add(sheet);
                        }
                        else if (sheet.GLName == "三、可供投资者分配的利润")
                        {
                            sheet.GLName = "八、可供投资者分配的利润";
                            datalist.Add(sheet);
                        }
                        else if (sheet.GLName == "四、未分配利润")
                        {
                            sheet.GLName = "九、未分配利润";
                            datalist.Add(sheet);
                        }
                        else
                        {
                            datalist.Add(sheet);
                        }
                    }



                    List<ReportParameter> paramList = new List<ReportParameter>();
                    paramList.Add(new ReportParameter("FromDate", this.operationDatePart1.FromDate.ToShortDateString()));
                    paramList.Add(new ReportParameter("ToDate", this.operationDatePart1.ToDate.ToShortDateString()));
                    paramList.Add(new ReportParameter("CurrencyName", "RMB"));

                    rd.IsLocalReport = true;
                    rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.UFProfitAllocationTotalReport.rdlc";
                    rd.Parameters = paramList;
                    List<ReportDataSource> ds = new List<ReportDataSource>();
                    ds.Add(new ReportDataSource("GLDataReport", datalist));
                    rd.DataSource = ds;
                    #endregion
                }
                else
                {
                    ConfigureInfo configure = new ConfigureInfo();
                    string StandardCurrency = "";
                    if (companyIDs.Count > 0)
                    {
                        foreach (Guid companyID in companyIDs)
                        {
                            configure = ConfigureService.GetCompanyConfigureInfo(companyID, LocalData.IsEnglish);
                            if (configure == null)
                            {
                                continue;
                            }

                            if (string.IsNullOrEmpty(StandardCurrency))
                            {
                                StandardCurrency = configure.StandardCurrency;
                            }

                            if (StandardCurrency.ToUpper() != configure.StandardCurrency.ToUpper())
                            {
                                MessageBoxService.ShowWarning("所选公司本位币不统一不能一起查询！", "提醒", MessageBoxButtons.OK);
                                return null;
                            }
                        }
                    }    

                    #region 明细表
                    List<ProfitAllocationDetailReport> list = ReportCenterService.GetProfitAllocationDetailList(companyIDs.ToArray(),
                                                               this.operationDatePart1.FromDate,
                                                               this.operationDatePart1.ToDate);

                    List<ReportParameter> paramList = new List<ReportParameter>();

                    string companystr = string.Empty;
                    string[] newCompanyStr = chkcmbCompany.EditText.Split(',');
                    List<string> mycompanys = new List<string>();
                    if (newCompanyStr.Length > 0)
                    {
                        foreach (string com in newCompanyStr)
                        {
                            if (com.IndexOf("办公室") > 0)
                            {
                                mycompanys.Add(com);
                            }
                            if (com.IndexOf("区") < 0 && com.IndexOf("东南亚") < 0)
                            {
                                mycompanys.Add(com);
                            }
                        }
                    }
                    if (mycompanys.Count > 0)
                    {
                        foreach (string com in mycompanys)
                        {
                            companystr += com + ",";
                        }
                        companystr = companystr.Substring(0, companystr.Length - 1);
                    }

                    paramList.Add(new ReportParameter("FromDate", this.operationDatePart1.FromDate.ToShortDateString()));
                    paramList.Add(new ReportParameter("ToDate", this.operationDatePart1.ToDate.ToShortDateString()));
                    paramList.Add(new ReportParameter("CompanyName", companystr));
                    paramList.Add(new ReportParameter("CurrencyName", StandardCurrency));

                    rd.IsLocalReport = true;
                    rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.UFProfitAllocationDetailReport.rdlc";
                    rd.Parameters = paramList;
                    List<ReportDataSource> ds = new List<ReportDataSource>();
                    ds.Add(new ReportDataSource("GLDataReport", list));
                    rd.DataSource = ds;
                    #endregion
                }
              
                return rd;

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, (LocalData.IsEnglish ? "Get Report Data Failed." : "获取报表数据失败.") + ex.ToString());
                return null;
            } 
        }

        private CompanyBalanceSheet ConvertSheet(ProfitTotalReport robject)
        {
            CompanyBalanceSheet sours = new CompanyBalanceSheet();
            sours.CQAmount = robject.YCQAmount;
            sours.DLAmount = robject.YDLAmount;
            sours.GLName = robject.GLName;
            sours.GLType = Convert.ToInt32(robject.GLType);
            sours.GZAmount = robject.YGZAmount;
            sours.NBAmount = robject.YNBAmount;
            sours.QDAmount = robject.YQDAmount;
            sours.SHAmount = robject.YSHAmount;
            sours.SZAmount = robject.YSZAmount;
            sours.TJAmount = robject.YTJAmount;
            sours.XMAmount = robject.YXMAmount;
            sours.YDAmount = robject.YYDAmount;

            return sours;
        }
        #endregion

        private void chkTotal_CheckedChanged(object sender, EventArgs e)
        {
            this.chkcmbCompany.Enabled = !this.chkTotal.Checked;
        }
    }
}
