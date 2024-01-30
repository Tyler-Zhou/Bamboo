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

namespace ICP.ReportCenter.UI.FinanceReports
{
    public partial class CompanyBalanceSheetSearchPart : ReportBaseSearchPart
    {
        public CompanyBalanceSheetSearchPart()
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
              try
            {
                _SearchParameter = new SearchParameter
                {
                    FromDate=DateTime.Parse(this.operationDatePart1.FromDate.ToShortDateString()),
                    ToDate = DateTime.Parse(this.operationDatePart1.ToDate.ToShortDateString())
                };
                List<CompanyBalanceSheet> list = ReportCenterService.GetCompanyBalanceSheet(
                                                           _SearchParameter.FromDate,
                                                           _SearchParameter.ToDate);
                  
              List<ReportParameter> paramList = new List<ReportParameter>();
              paramList.Add(new ReportParameter("FromDate", _SearchParameter.FromDate.ToShortDateString()));
              paramList.Add(new ReportParameter("ToDate", _SearchParameter.ToDate.ToShortDateString()));
              paramList.Add(new ReportParameter("CurrencyName", CurrencyName));

              ReportData rd = new ReportData();
              rd.IsLocalReport = true;
              rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.UFCompanyBalanceSheetlReport.rdlc";
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
            public DateTime FromDate{get;set;}
            public DateTime ToDate{get;set;}
        }
       
    }
}
