using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface;
using ICP.FAM.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.ReportCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace ICP.ReportCenter.UI
{
    /// <summary>
    /// 利润表(集团汇总)
    /// </summary>
    [ToolboxItem(false)]
    public partial class ProfitDetailSearchPartAll : ReportBaseSearchPart
    {
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

        ///// <summary>
        ///// 消息提示服务
        ///// </summary>
        //public IMessageBoxService MessageBoxService
        //{
        //    get { return ServiceClient.GetService<IMessageBoxService>(); }
        //}
        #endregion

        #region 构造函数
        public ProfitDetailSearchPartAll()
        {
            InitializeComponent();
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
            operationDatePart1.SetLastMonth();

            chkIsTotalThisYear.Text = LocalData.IsEnglish ? "Total this year" : "是否本年累计";
        }
        #endregion

        #region 查询
        public override event SearchResultHandler OnSearched;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
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
                btnSearch.Enabled = true;
            }
        }
        public override object GetData()
        {
            List<Guid> companyIDs = chkcmbCompany.CompanyIDs;
            if (companyIDs == null || companyIDs.Count == 0)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Company must choose one." : "请至少选择一个公司.");
                return null;
            }
            try
            {
                #region 汇兑表

                List<ReportParameter> paramList = new List<ReportParameter>
                {
                    new ReportParameter("FromDate", operationDatePart1.FromDate.ToShortDateString()),
                    new ReportParameter("ToDate", operationDatePart1.ToDate.ToShortDateString()),
                    new ReportParameter("CompanyIDs", companyIDs.ToArray().Join()),
                    new ReportParameter("IsTotalThisYear", chkIsTotalThisYear.Checked.ToString()),
                    new ReportParameter("IsEnglish", LocalData.IsEnglish.ToString())
                };

                ReportData rd = new ReportData
                {
                    Parameters = paramList,
                    ReportName = "UFProfitTotalReportAll",
                    IsLocalReport = false
                };
                return rd;

                #endregion
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
