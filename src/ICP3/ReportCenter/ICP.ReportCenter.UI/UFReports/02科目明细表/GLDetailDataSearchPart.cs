using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.ReportCenter.UI.UFReports
{
    public partial class GLDetailDataSearchPart : ReportBaseSearchPart
    {
        public GLDetailDataSearchPart()
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
            //开始时间&结束时间
            this.operationDatePart1.SetLastMonth();
            //科目搜索器
            SearchBoxAdapter.RegisterGLCodeSingleSearchBox(DataFindClientService, this.txtGLCode, false, this.chkcmbCompany.CompanyIDs);

            //报表格式
            Utility.BulidGLCodeLedgerStyle(cmbReportFormat);
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
            if (companyIDs == null || companyIDs.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Company must choose one." : "请至少选择一个公司.");
                return null;
            }
            Guid glID = Guid.Empty;
            if (this.txtGLCode.Tag == null || this.txtGLCode.Text==string.Empty)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "GL must choose one." : "请选择一个科目.");
                return null;

            }
            else
            {
                glID = new Guid(this.txtGLCode.Tag.ToString());
            }
            ConfigureInfo configure = new ConfigureInfo();
            string StandardCurrency = "";

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
                    MessageBoxService.ShowInfo("所选公司本位币不统一不能一起查询！", "提醒", MessageBoxButtons.OK);
                    return null;
                }
            }

              try
            {
                _SearchParameter = new SearchParameter
                {
                    CompanyIDs = companyIDs,
                    GLID = glID,
                    FromDate = DateTime.Parse(this.operationDatePart1.FromDate.ToShortDateString()),
                    ToDate = DateTime.Parse(this.operationDatePart1.ToDate.ToShortDateString())
                };
                  List<GLDetailData> list=new List<GLDetailData>();
                  //if ((GLCodeLedgerStyle)cmbReportFormat.EditValue ==  GLCodeLedgerStyle.AMOUNT)
                  //list = FinanceReportService.GetGLDetailDataList(_SearchParameter.CompanyIDs.ToArray(),
                  //                                             _SearchParameter.GLID,
                  //                                             _SearchParameter.FromDate,
                  //                                             _SearchParameter.ToDate);
                  //else if ((GLCodeLedgerStyle)cmbReportFormat.EditValue == GLCodeLedgerStyle.OUTGOAMOUNT)

                list = FinanceReportService.GetFCGLDetailBalanceList(_SearchParameter.CompanyIDs.ToArray(),
                                                               _SearchParameter.GLID,
                                                               _SearchParameter.FromDate,
                                                               _SearchParameter.ToDate);
                  
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

              paramList.Add(new ReportParameter("FromDate", _SearchParameter.FromDate.ToShortDateString()));
              paramList.Add(new ReportParameter("ToDate", _SearchParameter.ToDate.ToShortDateString()));
              paramList.Add(new ReportParameter("CompanyName", companystr));
              paramList.Add(new ReportParameter("GLCodeName", this.txtGLCode.Text));
              paramList.Add(new ReportParameter("GLID", _SearchParameter.GLID.ToString()));
              paramList.Add(new ReportParameter("CurrencyName", StandardCurrency));

              ReportData rd = new ReportData();
              rd.IsLocalReport = true;
              if ((GLCodeLedgerStyle)cmbReportFormat.EditValue ==  GLCodeLedgerStyle.AMOUNT)
                  rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.UFGLDetailReport.rdlc";
              else if ((GLCodeLedgerStyle)cmbReportFormat.EditValue == GLCodeLedgerStyle.OUTGOAMOUNT)
                  rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.UFFCGLDetailReport.rdlc";
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
            public List<Guid> CompanyIDs{get;set;}
            public Guid GLID{get;set;}
            public DateTime FromDate{get;set;}
            public DateTime ToDate{get;set;}
        }
       
    }
}
