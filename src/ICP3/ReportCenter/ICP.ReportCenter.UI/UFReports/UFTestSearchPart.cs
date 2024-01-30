using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.ReportCenter.ServiceInterface;
using Microsoft.Reporting.WinForms;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI
{
    public partial class UFTestSearchPart : ReportBaseSearchPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public IReportCenterService ReportCenterService
        {
            get
            {
                return ServiceClient.GetService<IReportCenterService>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
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

        public UFTestSearchPart()
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
            //开始时间&结束时间
            dteFromDate.DateTime = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(-1);//上月头
            dteToDate.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);//上月底
            //科目搜索器
            SearchBoxAdapter.RegisterGLCodeSingleSearchBox(DataFindClientService, this.txtGLCode, false, new List<Guid>());
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            base.RaiseSearched();
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
                this.btnSearch.Enabled=true;
            }
        }

        public override object GetData()
        {
            Guid glID = Guid.Empty;
            if (this.txtGLCode.Tag == null || this.txtGLCode.Text == string.Empty)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "GL must choose one." : "请选择一个科目.");
            }
            else
            {
                glID = new Guid(this.txtGLCode.Tag.ToString());
            }

            try
            {
                _SearchParameter = new SearchParameter
                {
                    GLID = glID,
                    FromDate = DateTime.Parse(this.dteFromDate.DateTime.ToShortDateString()),
                    ToDate = DateTime.Parse(this.dteToDate.DateTime.ToShortDateString())
                };

                List<GLDetailData> list = ReportCenterService.GetGLDetailBalanceList(_SearchParameter.GLID, _SearchParameter.FromDate, _SearchParameter.ToDate);

                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("FromDate", _SearchParameter.FromDate.ToShortDateString()));
                paramList.Add(new ReportParameter("ToDate", _SearchParameter.ToDate.ToShortDateString()));
                paramList.Add(new ReportParameter("CompanyName", string.Empty));
                paramList.Add(new ReportParameter("GLCodeName", this.txtGLCode.Text));
                paramList.Add(new ReportParameter("GLID", _SearchParameter.GLID.ToString()));
                paramList.Add(new ReportParameter("CurrencyName", CurrencyName));

                ReportData rd = new ReportData();
                rd.IsLocalReport = true;
                rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.UFTestReport.rdlc";
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
    }

            class SearchParameter
        { 
            public Guid GLID{get;set;}
            public DateTime FromDate{get;set;}
            public DateTime ToDate{get;set;}
        }
}
