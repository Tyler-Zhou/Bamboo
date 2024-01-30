using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Common.UI;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects.Report;
using ICP.Common.ServiceInterface.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FAM.UI
{
    public partial class InvoiceCountPart : BasePart
    {
        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }

        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }
        #endregion

        public InvoiceReportType ReportType
        {
            get;
            set;
        }

        public InvoiceCountPart()
        {
            InitializeComponent();
            Disposed += delegate {

                cmbCompany.OnFirstEnter -= OnCmbCompanyEnter;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            };
        }

        private void InvoiceCountPart_Load(object sender, EventArgs e)
        {
            cmbCompany.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID,LocalData.UserInfo.DefaultCompanyName);

            if (ReportType == InvoiceReportType.OperationInvoice)
            {
                int beginDay = 0;
                int endDay = 0;
                string weekstr = DateTime.Now.DayOfWeek.ToString();
                switch (weekstr)
                {
                    case "Monday":
                        endDay = -1; 
                        break;
                    case "Tuesday":
                        endDay = -2; 
                        break;
                    case "Wednesday":
                         endDay = -3; 
                         break;
                    case "Thursday": 
                         endDay = -4; 
                         break;
                    case "Friday": 
                         endDay = -5; 
                         break;
                    case "Saturday":
                         endDay = -6; 
                         break;
                    case "Sunday":
                         endDay = -7; 
                         break;
                }
                beginDay = endDay - 6;
                dtpBeginDate.EditValue = DateTime.Now.AddDays(beginDay);
                dtpEndDate.EditValue = DateTime.Now.AddDays(endDay);
            }
            else
            { 
                dtpBeginDate.EditValue = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
                dtpEndDate.EditValue = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day);
            }
            cmbCompany.OnFirstEnter += OnCmbCompanyEnter;

        }
        private void OnCmbCompanyEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.BindCompanyByUser(cmbCompany, false);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string message = string.Empty;

            if (cmbCompany.EditValue == null)
            {
                message = LocalData.IsEnglish ? "Please select Company" : "请选择公司";
                XtraMessageBox.Show(message);
                cmbCompany.Focus();
                return;
            }
            if (dtpBeginDate.EditValue == null)
            {
                message = LocalData.IsEnglish ? "Please select Begin Date" : "请选择开始日期";
                XtraMessageBox.Show(message);
                dtpBeginDate.Focus();
                return;
            }
            if (dtpEndDate.EditValue == null)
            {
                message = LocalData.IsEnglish ? "Please select End Date" : "请选择结束日期";
                XtraMessageBox.Show(message);
                dtpEndDate.Focus();
                return;
            }

            if (ReportType == InvoiceReportType.InvoiceCount)
            {
                ///发票统计
                ShowReportView();
            }
            if (ReportType == InvoiceReportType.OperationInvoice)
            {
                //开票统计
                ShowOperationInvoiceReport();
            }
            else if (ReportType == InvoiceReportType.DutyFreeDetail)
            { 
                //免税收入明细表
                ShowFreeReport();
            }

            FindForm().Close();
        }
        #region 免税明细
        private void ShowFreeReport()
        {
            Guid companyID = new Guid(cmbCompany.EditValue.ToString());
            InvoiceFreeReportData baseReport = new InvoiceFreeReportData();
            baseReport = FinanceService.GetInvoiceFreeList(companyID, dtpBeginDate.DateTime, dtpEndDate.DateTime);
            if (baseReport == null)
            {
                return;
            }
            try
            {
                IReportViewer viewer = ReportViewService.ShowReportViewer("免税收入明细表", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
                string fileName = Application.StartupPath + "\\Reports\\FAM\\Invoice\\";
                
                 fileName += "InvoiceFreeDetail.frx";
                
                Dictionary<string, object> reportSource = new Dictionary<string, object>();
                reportSource.Add("InvoiceData", baseReport.DataList);
                reportSource.Add("InvoiceTotal", baseReport.TotalInfo);

                viewer.BindData(fileName, reportSource, null);
            }
            catch (Exception ex)
            {
                btnOK.Enabled = true;
                btnClose.Enabled = true;
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }

        }
        #endregion

        #region 开票统计
        private void ShowOperationInvoiceReport()
        {
            Guid companyID = new Guid(cmbCompany.EditValue.ToString());
            InvoieCountBaseReport baseReport = new InvoieCountBaseReport();

            List<InvoiceCountReport> DataList = FinanceService.GetOperationInvoiceReport(companyID, dtpBeginDate.DateTime, dtpEndDate.DateTime);
            if (DataList == null )
            {
                return;
            }
            try
            {
                IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Invoice Count" : "开票统计", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
                string fileName = Application.StartupPath + "\\Reports\\FAM\\Invoice\\";

                 fileName += "OperationInvoiceReport.frx";

                 baseReport.TotalList = new List<InvoiceCountReportTotal>();
                 baseReport.DataList = DataList;

                Dictionary<string, object> reportSource = new Dictionary<string, object>();
                reportSource.Add("InvoiceCountDataList", baseReport.DataList);
                reportSource.Add("InvoiceCountBaseReport", baseReport);
                reportSource.Add("InvoiceCountTotalInfo", baseReport.TotalList);

                viewer.BindData(fileName, reportSource, null);
            }
            catch (Exception ex)
            {
                btnOK.Enabled = true;
                btnClose.Enabled = true;
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }


        }
        #endregion

        #region 发票统计
        /// <summary>
        /// 发票统计
        /// </summary>
        private void ShowReportView()
        {
            Guid companyID = new Guid(cmbCompany.EditValue.ToString());
            InvoieCountBaseReport baseReport = new InvoieCountBaseReport();
            baseReport = FinanceService.GetInvoieCountReport(companyID, dtpBeginDate.DateTime, dtpEndDate.DateTime, LocalData.IsEnglish);
            if (baseReport == null)
            {
                return;
            }
            try
            {
               
                baseReport.CompanyName = cmbCompany.Text;
                
                IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Invoice Count" : "发票统计", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
                string fileName = Application.StartupPath + "\\Reports\\FAM\\Invoice\\";
                if (LocalData.IsEnglish)
                {
                    fileName += "InvoiceCount_EN.frx";
                }
                else
                {
                    fileName += "InvoiceCount_CN.frx";
                }
                Dictionary<string, object> reportSource = new Dictionary<string, object>();
                reportSource.Add("InvoiceCountDataList", baseReport.DataList);
                reportSource.Add("InvoiceCountBaseReport", baseReport);
                reportSource.Add("InvoiceCountTotalInfo", baseReport.TotalList);


                viewer.BindData(fileName, reportSource, null);
            }
            catch (Exception ex)
            {
                btnOK.Enabled = true;
                btnClose.Enabled = true;
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }


        }

        #endregion

    }
}
