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
using ICP.ReportCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.UFReports
{
    public partial class CustomerGLDetailSearchPart : ReportBaseSearchPart
    {
        public CustomerGLDetailSearchPart()
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

        #region 查询
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
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
        SearchParameter _SearchParameter = new SearchParameter();
        public override object GetData()
        {
            List<Guid> companyIDs = this.chkcmbCompany.CompanyIDs;
            if (companyIDs == null || companyIDs.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Company must choose one." : "请至少选择一个公司.");
                return null;
            }
            List<Guid> customerIDList = new List<Guid>();
            List<Guid> glIDList = new List<Guid>();
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

            if (this.txtCustomer.Tag != null && this.txtCustomer.Tag != DBNull.Value)
            { 
                customerIDList=this.txtCustomer.Tag as List<Guid>;
            }
            if (this.txtGLCode.Tag != null && this.txtGLCode.Tag != DBNull.Value)
            {
                glIDList = this.txtGLCode.Tag as List<Guid>;
            }
            try
            {
                _SearchParameter = new SearchParameter
                {
                    CompanyIDs = companyIDs,
                    CustomerIDs = customerIDList,
                    GLIDs = glIDList,
                    FromDate = DateTime.Parse(this.operationDatePart1.FromDate.ToShortDateString()),
                    ToDate = DateTime.Parse(this.operationDatePart1.ToDate.ToShortDateString()),
                    Format = (GLCodeLedgerStyle)cmbReportFormat.EditValue
                };

                List<CustomerGLDetail> list = FinanceReportService.GetCustomerGLDetailList(
                                             _SearchParameter.CompanyIDs.ToArray(),
                                             _SearchParameter.CustomerIDs.ToArray(),
                                             _SearchParameter.GLIDs.ToArray(),
                                             _SearchParameter.FromDate,
                                             _SearchParameter.ToDate,
                                             _SearchParameter.Format);

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
               paramList.Add(new ReportParameter("CurrencyName", StandardCurrency));
               paramList.Add(new ReportParameter("GLName", this.txtGLCode.Text));
               paramList.Add(new ReportParameter("CustomerName", this.txtCustomer.Text));

               ReportData rd = new ReportData();
               rd.IsLocalReport = true;
               if (_SearchParameter.Format == GLCodeLedgerStyle.AMOUNT)
               {
                   rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.CustomerDetail.UFCustomerGLDetailReport";
               }
               else if (_SearchParameter.Format== GLCodeLedgerStyle.OUTGOAMOUNT)
               {
                   rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.CustomerDetail.UFCustomerFCGLDetailReport";
               }
               if (string.IsNullOrEmpty(this.txtGLCode.Text) && !string.IsNullOrEmpty(this.txtCustomer.Text))
               { 
                   //客户
                   rd.ReportName = rd.ReportName + "Customer";
               }
               else if (!string.IsNullOrEmpty(this.txtGLCode.Text) && string.IsNullOrEmpty(this.txtCustomer.Text))
               {
                   //科目
                   rd.ReportName = rd.ReportName + "GL";
               }
               else if (!string.IsNullOrEmpty(this.txtGLCode.Text) && !string.IsNullOrEmpty(this.txtCustomer.Text))
               {
                   //科目+客户
                   rd.ReportName = rd.ReportName + "GLCustomer";
               }


               rd.ReportName = rd.ReportName + ".rdlc";


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
            SearchBoxAdapter.RegisterGLCodeMultipleSearchBox(DataFindClientService, this.txtGLCode, this.chkcmbCompany.CompanyIDs);

            //报表格式
            Utility.BulidGLCodeLedgerStyle(cmbReportFormat);
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

            Utility.ShoLedgerInfo(reportEmbeddedResource, parameters);

            return null;
        }
        #endregion

        class SearchParameter
        {
           public List<Guid> CompanyIDs{get;set;}
           public List<Guid> CustomerIDs{get;set;}
           public List<Guid> GLIDs{get;set;}
           public DateTime   FromDate{get;set;}
           public DateTime ToDate{get;set;}
           public GLCodeLedgerStyle Format { get; set; }
        }
    }
}
