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
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Reporting.WinForms;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.UFReports
{
    public partial class Personal3ColumnGLBalanceSearchPart : ReportBaseSearchPart
    {
        public Personal3ColumnGLBalanceSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.OnSearched = null;
                if (this.glCodeFinder != null)
                {
                    this.glCodeFinder.Dispose();
                    this.glCodeFinder = null;
                }
                if (this.userFinder != null)
                {
                    this.userFinder.Dispose();
                    this.userFinder = null;
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
            //公司
            List<Guid> companyIDs = this.chkcmbCompany.CompanyIDs;
            if (companyIDs == null || companyIDs.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Company must choose one." : "请至少选择一个公司.");
                this.chkcmbCompany.Focus();
                return null;
            }
            //个人
            Guid userID = Guid.Empty;
            if (this.txtUser.Tag == null || this.txtUser.Tag == DBNull.Value)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "User must choose one." : "请至少选择一个用户.");
                this.txtUser.Focus();
                return null;
            }
            else
            {
                userID =new Guid(this.txtUser.Tag.ToString());
            }
            //科目
            Guid glID = Guid.Empty;
            if (this.txtGLCode.Tag == null || this.txtGLCode.Tag == DBNull.Value)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "GLCode must choose one." : "请至少选择一个会计科目.");
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
                    DepartmentIDs = this.treeBoxSalesDep.GetAllEditValue,
                    PersonalID = userID,
                    GLId = glID,
                    FormDate = DateTime.Parse(this.operationDatePart1.FromDate.ToShortDateString()),
                    ToDate = DateTime.Parse(this.operationDatePart1.ToDate.ToShortDateString())
                };


                List<Personal3ColumnGLBalance> list = FinanceReportService.GetPersonal3ColumnGLBalanceList(
                                                     _SearchParameter.CompanyIDs.ToArray(),
                                                     _SearchParameter.DepartmentIDs.ToArray(),
                                                     _SearchParameter.PersonalID,
                                                     _SearchParameter.GLId,
                                                     _SearchParameter.FormDate,
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

                paramList.Add(new ReportParameter("CompanyName", companystr));
                paramList.Add(new ReportParameter("PersonalName", this.txtUser.Text));
                paramList.Add(new ReportParameter("GLCodeName", this.txtGLCode.Text));
                paramList.Add(new ReportParameter("FromDate", _SearchParameter.FormDate.ToShortDateString()));
                paramList.Add(new ReportParameter("ToDate", _SearchParameter.ToDate.ToShortDateString()));
                paramList.Add(new ReportParameter("GLID", _SearchParameter.GLId.ToString()));
                paramList.Add(new ReportParameter("PersonalID", _SearchParameter.PersonalID.ToString()));
                paramList.Add(new ReportParameter("CurrencyName", CurrencyName));

                ReportData rd = new ReportData();
                rd.IsLocalReport = true;
                rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.UFPersonal3ColumnGLBalance.rdlc";
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
        private IDisposable userFinder, glCodeFinder;
        private void InitControls()
        {
            //用户搜索器
           userFinder=  SearchBoxAdapter.RegisterSingleSearchBox(DataFindClientService, txtUser, ICP.Sys.ServiceInterface.SystemFinderConstants.UserFinder);
            //科目搜索器
           glCodeFinder = SearchBoxAdapter.RegisterGLCodeSingleSearchBox(DataFindClientService, this.txtGLCode, false, this.chkcmbCompany.CompanyIDs);

            this.treeBoxSalesDep.Tag = null;
            this.treeBoxSalesDep.EditValue = null;
            this.treeBoxSalesDep.EditText = null;

            this.operationDatePart1.SetLastMonth();
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
            List<Guid> personalIDs = new List<Guid>();

            if (parameters[3].Values[0] != null)
            {
                return null;
            }
            glIDs.Add(new Guid(parameters[3].Values[0].ToString()));

            if (parameters[4].Values[0] != null)
            {
                return null;
            }
            personalIDs.Add(new Guid(parameters[4].Values[0].ToString()));

            DateTime fromDate = Convert.ToDateTime(parameters[0].Values[0].ToString());
            DateTime toDate = Convert.ToDateTime(parameters[1].Values[0].ToString());


            List<CustomerGLDetail> list = FinanceReportService.GetCustomerGLDetailList(
                                         _SearchParameter.CompanyIDs.ToArray(),
                                         personalIDs.ToArray(),
                                         glIDs.ToArray(),
                                         fromDate,
                                         toDate,
                                         GLCodeLedgerStyle.AMOUNT);

            ReportData rd = new ReportData();
            List<ReportDataSource> ds = new List<ReportDataSource>();
            ds.Add(new ReportDataSource("GLDataReport", list));
            rd.DataSource = ds;

            return rd;

        }
        #endregion


        class SearchParameter
        {
            public List<Guid> CompanyIDs { get; set; }
            public List<Guid> DepartmentIDs { get; set; }
            public Guid PersonalID { get; set; }
            public Guid GLId { get; set; }
            public DateTime FormDate { get; set; }
            public DateTime ToDate { get; set; }
            public GLCodeProperty Property { get; set; }
            public bool NoAccounting { get; set; }
        }
    }
}
