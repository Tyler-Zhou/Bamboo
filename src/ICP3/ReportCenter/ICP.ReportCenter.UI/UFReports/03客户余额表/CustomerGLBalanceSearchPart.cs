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
using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Controls;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Reporting.WinForms;
using ICP.ReportCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.UFReports
{
    [ToolboxItem(false)]
    public partial class CustomerGLBalanceSearchPart : ReportBaseSearchPart
    {
        public CustomerGLBalanceSearchPart()
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
            //余额方向
            List<GLCodeProperty> propertyList = new List<GLCodeProperty>();
            foreach (CheckedListBoxItem item in this.cmbBalanceDirection.Properties.Items)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    propertyList.Add((GLCodeProperty)item.Value);
                }
            }
            if (propertyList.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Balance Direction must choose one." : "请至少选择一个余额方向.");
                return null;
            }
            //客户&科目
            List<Guid> customerIDList = new List<Guid>();
            List<Guid> glIDList = new List<Guid>();
            if (this.txtCustomerID.Tag != null)
            { 
                customerIDList=this.txtCustomerID.Tag as List<Guid>;
            }
            if (this.txtGLCode.Tag != null)
            { 
                glIDList=this.txtGLCode.Tag as List<Guid>;
            }

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

            try
            {
                _SearchParameter = new SearchParameter { 
                                            CompanyIDs = companyIDs ,
                                            CustomerIDs = customerIDList,
                                            GLIDs=glIDList,
                                            Propertys = propertyList,
                                            FromDate = DateTime.Parse(this.operationDatePart1.FromDate.ToShortDateString()),
                                            ToDate = DateTime.Parse(this.operationDatePart1.ToDate.ToShortDateString()),
                                            Format = (GLCodeLedgerStyle)cmbReportFormat.EditValue
                };

                List<CustomerGLBalance> list = FinanceReportService.GetCustomerGLBalanceList(
                                                                         _SearchParameter.CompanyIDs.ToArray(),
                                                                         _SearchParameter.CustomerIDs.ToArray(),
                                                                         _SearchParameter.GLIDs.ToArray(),
                                                                         _SearchParameter.FromDate,
                                                                         _SearchParameter.ToDate,
                                                                         _SearchParameter.Propertys.ToArray(),
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
               paramList.Add(new ReportParameter("CurrencyName", cmbCurrencyList.Visible ? cmbCurrencyList.Text : StandardCurrency));


               ReportData rd = new ReportData();
               rd.IsLocalReport = true;
               if (_SearchParameter.Format == GLCodeLedgerStyle.AMOUNT)
                   rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.UFCustomerGLBalance.rdlc";
               else if (_SearchParameter.Format == GLCodeLedgerStyle.OUTGOAMOUNT)
                   rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.UFCustomerFCGLBalance.rdlc";
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
            SearchBoxAdapter.RegisterGLCodeMultipleSearchBox(DataFindClientService, this.txtGLCode,this.chkcmbCompany.CompanyIDs);

            //余额方向
            Utility.BulidBalanceDirectionComBox(cmbBalanceDirection);

            //报表格式
            Utility.BulidGLCodeLedgerStyle(cmbReportFormat);

            this.cmbCurrencyList.OnFirstEnter += this.OncmbCurrencyListFirstEnter;
        }

        private void OncmbCurrencyListFirstEnter(object sender, EventArgs e)
        {

            List<CurrencyList> currencyList = ConfigureService.GetCurrencyList(null, null, null, true, 0);
            this.cmbCurrencyList.Properties.BeginUpdate();
            this.cmbCurrencyList.Properties.Items.Clear();
            foreach (var item in currencyList)
            {
                this.cmbCurrencyList.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Code, item.ID));
            }
            this.cmbCurrencyList.Properties.Items.Insert(0, new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", null));
            this.cmbCurrencyList.Properties.EndUpdate();
        }
        #endregion

        #region 子表
        public override ReportData GetDrillthroughData(string reportEmbeddedResource, IList<ReportParameter> parameters)
        {
            if(parameters==null)
            {
                return null;
            }

            if (reportEmbeddedResource.Contains("UFCustomerGLDetailReport") || reportEmbeddedResource.Contains("UFCustomerFCGLDetailReport"))
            {
                string searchType = parameters[5].Values[0].ToString();
                List<Guid> glIDs = new List<Guid>();
                List<Guid> customerIDs = new List<Guid>();
                Guid searchID = Guid.Empty;
             
                if (parameters[3].Values[0] == null)
                {
                    return null;
                }
                searchID = new Guid(parameters[3].Values[0].ToString());
                glIDs.Add(searchID);

                if (parameters[4].Values[0] == null)
                {
                    return null;
                }
                searchID = new Guid(parameters[4].Values[0].ToString());
                customerIDs.Add(searchID);
           

                List<CustomerGLDetail> list = FinanceReportService.GetCustomerGLDetailList(
                                             _SearchParameter.CompanyIDs.ToArray(),
                                             customerIDs.ToArray(),
                                             glIDs.ToArray(),
                                             _SearchParameter.FromDate,
                                             _SearchParameter.ToDate,
                                             _SearchParameter.Format);

                ReportData rd = new ReportData();
                List<ReportDataSource> ds = new List<ReportDataSource>();
                ds.Add(new ReportDataSource("GLDataReport", list));
                rd.DataSource = ds;

                return rd;
            }
            else if (reportEmbeddedResource.Contains("UFShowLedgerReport"))
            {
                #region 进入显示凭证明细
                Utility.ShoLedgerInfo(reportEmbeddedResource, parameters);

                return null;
                #endregion
            }
            return null;
  
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

        class SearchParameter
        { 
           public List<Guid> CompanyIDs{get;set;}
           public List<Guid> CustomerIDs { get; set; }
           public List<Guid> GLIDs { get; set; }
           public DateTime FromDate { get; set; }
           public DateTime ToDate { get; set; }
           public List<GLCodeProperty> Propertys { get; set; }
           public GLCodeLedgerStyle Format { get; set; }
        }

        private void cmbReportFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReportFormat.SelectedIndex == 1)
            {
                labelControl1.Visible = true;
                cmbCurrencyList.Visible = true;
            }
            else
            {
                labelControl1.Visible = false;
                cmbCurrencyList.Visible = false;
            }
        }
      
    }
}
