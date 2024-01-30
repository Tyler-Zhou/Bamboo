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
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.UFReports
{
    public partial class Customer3ColumnGLBalanceSearchPart : ReportBaseSearchPart
    {
        public Customer3ColumnGLBalanceSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.OnSearched = null;
                if (this.customerFinder != null)
                {
                    this.customerFinder.Dispose();
                    this.customerFinder = null;
                }
                if (this.glCodeFinder != null)
                {
                    this.glCodeFinder.Dispose();
                    this.glCodeFinder = null;
                }
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
            Guid customerID=Guid.Empty;
            Guid glID=Guid.Empty;
            if (this.txtCusomer.Tag == null || this.txtCusomer.Tag == DBNull.Value)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Customer must choose one." : "请选择一个客户.");
                this.txtCusomer.Focus();
                return null;
            }
            else
            {
                customerID = new Guid(this.txtCusomer.Tag.ToString());
            }

            if (this.txtGLCode.Tag == null||this.txtGLCode.Tag ==DBNull.Value)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "GLCode must choose one." : "请选择一个会计科目.");
                this.txtGLCode.Focus();
                return null;
            }
            else
            {
                glID = new Guid(this.txtGLCode.Tag.ToString());
            }
            try
            {
                _SearchParameter = new SearchParameter
                                  {
                                      CompanyIDs = companyIDs,
                                      CustomerID = customerID,
                                      Years=Convert.ToInt32(this.cmbYears.EditValue),
                                      GlID = glID,
                                      NoAccounting=this.chkNoAccounting.Checked
                                  };

                List<Customer3ColumnGLBalance> list = FinanceReportService.GetCustomer3ColumnGLBalanceList(
                                                                    _SearchParameter.CompanyIDs.ToArray(),
                                                                    _SearchParameter.CustomerID,
                                                                    _SearchParameter.GlID,
                                                                    _SearchParameter.Years,
                                                                    true);

                List<ReportParameter> paramList = new List<ReportParameter>();

                DateTime fromDate=new DateTime(_SearchParameter.Years,1,1);
                DateTime toDate=new DateTime(_SearchParameter.Years,12,31);

                paramList.Add(new ReportParameter("CompanyName", this.chkcmbCompany.CompanyString));
                paramList.Add(new ReportParameter("CustomerName", this.txtCusomer.Text));
                paramList.Add(new ReportParameter("GLCodeName", this.txtGLCode.Text));
                paramList.Add(new ReportParameter("FromDate",fromDate.ToShortDateString()));
                paramList.Add(new ReportParameter("ToDate", toDate.ToShortDateString()));
                paramList.Add(new ReportParameter("GLID", _SearchParameter.GlID.ToString()));
                paramList.Add(new ReportParameter("CustomerID", _SearchParameter.CustomerID.ToString()));

                ReportData rd = new ReportData();
                rd.IsLocalReport = true;
                rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.UFCustomer3ColumnGLBalance.rdlc";
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
        private IDisposable customerFinder, glCodeFinder;
        private void InitControls()
        {
            //客户搜索器
          customerFinder=  SearchBoxAdapter.RegisterSingleSearchBox(DataFindClientService, txtCusomer, ICP.Common.ServiceInterface.CommonFinderConstants.CustoemrFinder);
            //科目搜索器
          glCodeFinder=  SearchBoxAdapter.RegisterGLCodeSingleSearchBox(DataFindClientService, this.txtGLCode,false,this.chkcmbCompany.CompanyIDs);

            //年份
            Utility.BulidYearCombBox(this.cmbYears);

        }
        #endregion

        #region 子表
        public override ReportData GetDrillthroughData(string reportEmbeddedResource, IList<ReportParameter> parameters)
        {
            if (parameters == null)
            {
                return null;
            }

            List<Guid> glIDs = new List<Guid>();
            List<Guid> customerIDs = new List<Guid>();

            if (parameters[3].Values[0] != null)
            {
                return null;
            }
            glIDs.Add(new Guid(parameters[3].Values[0].ToString()));

            if (parameters[4].Values[0] != null)
            {
                return null;
            }
            glIDs.Add(new Guid(parameters[4].Values[0].ToString()));

            DateTime fromDate = Convert.ToDateTime(parameters[0].Values[0].ToString());
            DateTime toDate = Convert.ToDateTime(parameters[1].Values[0].ToString());


            List<CustomerGLDetail> list = FinanceReportService.GetCustomerGLDetailList(
                                         _SearchParameter.CompanyIDs.ToArray(),
                                         customerIDs.ToArray(),
                                         glIDs.ToArray(),
                                         fromDate,
                                         toDate,
                                         ICP.Common.ServiceInterface.DataObjects.GLCodeLedgerStyle.AMOUNT);

            ReportData rd = new ReportData();
            List<ReportDataSource> ds = new List<ReportDataSource>();
            ds.Add(new ReportDataSource("GLDataReport", list));
            rd.DataSource = ds;

            return rd;

        }
        #endregion

        class SearchParameter
        {
            public List<Guid> CompanyIDs{get;set;}
            public Guid CustomerID { get; set; }
            public Guid GlID { get; set; }
            public int Years { get; set; }
            public bool NoAccounting { get; set; }
        }

    }
}
