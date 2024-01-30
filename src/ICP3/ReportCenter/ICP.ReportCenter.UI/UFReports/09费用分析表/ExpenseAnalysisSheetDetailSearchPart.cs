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
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.FinanceReports
{
    public partial class ExpenseAnalysisSheetDetailSearchPart : ReportBaseSearchPart
    {
        public ExpenseAnalysisSheetDetailSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.OnSearched = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

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
        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }
        #endregion

        #region 属性 
        string currencyName;
        public string CurrencyName
        {
            get
            {
                if (string.IsNullOrEmpty(currencyName))
                {
                    ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                    if (configure != null)
                    {
                        currencyName = configure.StandardCurrency;
                    }
                }
                return currencyName;
            }
        }
        #endregion

        #region Load
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
            //报表类型
            List<EnumHelper.ListItem<ExpenseType>> formats = EnumHelper.GetEnumValues<ExpenseType>(LocalData.IsEnglish);
            cmbExpenseType.Properties.BeginUpdate();
            cmbExpenseType.Properties.Items.Clear();
            foreach (var item in formats)
            {
                cmbExpenseType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbExpenseType.Properties.EndUpdate();
            this.cmbExpenseType.SelectedIndex = 0;

            List<EnumHelper.ListItem<ExpenseHappenType>> happenType = EnumHelper.GetEnumValues<ExpenseHappenType>(LocalData.IsEnglish);
            this.cmbHappen.Properties.BeginUpdate();
            this.cmbHappen.Properties.Items.Clear();
            foreach (var item in happenType)
            {
                cmbHappen.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbHappen.Properties.EndUpdate();
            this.cmbHappen.SelectedIndex = 0;

            if (!LocalCommonServices.PermissionService.HaveActionPermission(ActionConstants.Report_Total))
            {
                this.chkTotal.Visible = this.cmbHappen.Visible = this.labHappenType.Visible = false;
                this.chkTotal.Enabled = this.cmbHappen.Enabled = this.labHappenType.Enabled = false;
            }
            this.cmbHappen.Enabled = false;
        }
        #endregion

        #region 查询
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        SearchParameter _SearchParameter = new SearchParameter();
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
                _SearchParameter = new SearchParameter
                {
                    CompanyIDs = companyIDs,
                    FromDate = DateTime.Parse(this.operationDatePart1.FromDate.ToShortDateString()),
                    ToDate = DateTime.Parse(this.operationDatePart1.ToDate.ToShortDateString()),
                    ExpenseType = (ExpenseType)cmbExpenseType.EditValue,
                    IsCheckGL=this.chkCheckGL.Checked,
                    HappenType = (ExpenseHappenType)cmbHappen.EditValue,
                };
                ReportData rd = new ReportData();
                if (this.chkTotal.Checked)
                {
                    #region 汇总表
                    List<CompanyExpenseAnalysisSheet> list = ReportCenterService.GetExpenseAnalysisSheet(
                                                         _SearchParameter.ExpenseType,
                                                         _SearchParameter.HappenType,
                                                         _SearchParameter.FromDate,
                                                         _SearchParameter.ToDate,
                                                         _SearchParameter.IsCheckGL);

                    List<ReportParameter> paramList = new List<ReportParameter>();
                    paramList.Add(new ReportParameter("Title", ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<ExpenseType>(_SearchParameter.ExpenseType, LocalData.IsEnglish)));
                    paramList.Add(new ReportParameter("FromDate", _SearchParameter.FromDate.ToShortDateString()));
                    paramList.Add(new ReportParameter("ToDate", _SearchParameter.ToDate.ToShortDateString()));
                    paramList.Add(new ReportParameter("CurrencyName", CurrencyName));
                    paramList.Add(new ReportParameter("TypeName", _SearchParameter.HappenType == ExpenseHappenType.Month ? "本月发生数" : "本年累计数"));

      
                    rd.IsLocalReport = true;
                    rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.UFExpenseAnalysisReport.rdlc";
                    rd.Parameters = paramList;
                    List<ReportDataSource> ds = new List<ReportDataSource>();
                    ds.Add(new ReportDataSource("GLDataReport", list));
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
                    List<ExpenseAnalysisSheet> list = ReportCenterService.GetExpenseAnalysisSheetDetail(_SearchParameter.CompanyIDs.ToArray(),
                                                                                                  _SearchParameter.ExpenseType,
                                                                                                   _SearchParameter.FromDate,
                                                                                                   _SearchParameter.ToDate,
                                                                                                   _SearchParameter.IsCheckGL);

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

                    paramList.Add(new ReportParameter("Title", ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<ExpenseType>(_SearchParameter.ExpenseType, LocalData.IsEnglish)));
                    paramList.Add(new ReportParameter("FromDate", _SearchParameter.FromDate.ToShortDateString()));
                    paramList.Add(new ReportParameter("ToDate", _SearchParameter.ToDate.ToShortDateString()));
                    paramList.Add(new ReportParameter("CompanyName", companystr));
                    paramList.Add(new ReportParameter("CurrencyName", StandardCurrency));

                    rd.IsLocalReport = true;
                    rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.UFExpenseAnalysisDetailReport.rdlc";
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
        #endregion

        #region 子表
        public override ReportData GetDrillthroughData(string reportEmbeddedResource, IList<ReportParameter> parameters)
        {
            //if (parameters == null)
            //{
            //    return null;
            //}
            //if (parameters[0].Values[0] == null)
            //{
            //    return null;
            //}
            //Guid id = new Guid(parameters[0].Values[0].ToString());

            //FinanceClientService.ShowLedgerInfo(id);

            Utility.ShoLedgerInfo(reportEmbeddedResource,parameters);

            return null;
        }
        #endregion

        class SearchParameter
        { 
            public List<Guid> CompanyIDs{get;set;}
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public ExpenseType ExpenseType { get; set; }
            public ExpenseHappenType HappenType { get; set; }

            public bool IsCheckGL { get; set; }
        }

        #region 月预算
        private void btnBudget_Click(object sender, EventArgs e)
        {
            FinanceClientService.ShowFeeYearMonthBudgetPart();
        }
        #endregion

        #region 汇总
        private void chkTotal_CheckedChanged(object sender, EventArgs e)
        {
            this.chkcmbCompany.Enabled = !this.chkTotal.Checked;
            this.cmbHappen.Enabled = this.chkTotal.Checked; 
        }
        #endregion
    }
}
