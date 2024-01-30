using System;
using System.ComponentModel;
using DevExpress.XtraEditors.Controls;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.ReportCenter.UI;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.ReleaseBL
{
    [ToolboxItem(false)]
    public partial class ReleaseBLDebtListPart : BaseListPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }


        public ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }
        #endregion


        #region init

        public ReleaseBLDebtListPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                reportView = null;
                _OperationCommonInfo = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        #endregion
        ReportViewBase reportView = null;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                reportView = Workitem.Items.AddNew<ReportViewBase>();
                reportView.Dock = DockStyle.Fill;
                pnlMain.Controls.Clear();
                pnlMain.Controls.Add(reportView);
            }
        }
        #region IListPart 成员

        OperationCommonInfo _OperationCommonInfo = null;

        /// <summary>
        /// OperationCommonInfo
        /// </summary>
        public override object DataSource
        {
            get { return _OperationCommonInfo; }
            set { BindingData(value); }
        }

        private void BindingData(object value)
        {
            _OperationCommonInfo = null;
            ReleaseBLList parentList = value as ReleaseBLList;
            if (parentList != null)
                _OperationCommonInfo = FCMCommonService.GetOperationCommonInfo(parentList.OperationID, parentList.OperationType);


            cmbCustomer.Properties.Items.Clear();
            if (_OperationCommonInfo == null)
            {
                Enabled = false;
            }
            else
            {
                Enabled = true;
                cmbCustomer.Properties.Items.Clear();
                foreach (var item in _OperationCommonInfo.TradeCustomers)
                {
                    cmbCustomer.Properties.Items.Add(new ImageComboBoxItem(item.EName, item.ID));
                }
                cmbCustomer.SelectedIndex = 0;
            }
        }
        #endregion

        private void btnShow_Click_1(object sender, EventArgs e)
        {
            if (cmbCustomer.EditValue == null) return;
            if (reportView == null) return;

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("StructType", "0"));
            if (rdoShow.SelectedIndex == 1)
            {
                paramList.Add(new ReportParameter("StructNodeId", "701ACD43-D49B-422B-83A9-ACB56B696995"));
            }
            else
            {
                paramList.Add(new ReportParameter("StructNodeId", _OperationCommonInfo.CompanyID.ToString()));
            }

            paramList.Add(new ReportParameter("ETD_Beginning_Date", "2000-01-01"));
            paramList.Add(new ReportParameter("ETD_Ending_Date", DateTime.Now.ToShortDateString()));
            paramList.Add(new ReportParameter("SalesSet", string.Empty));
            paramList.Add(new ReportParameter("ShipperSet", cmbCustomer.EditValue.ToString()));
            paramList.Add(new ReportParameter("JobType", ""));
            paramList.Add(new ReportParameter("VerifyFlag", "1"));
            paramList.Add(new ReportParameter("CompanyName", ReportCenterHelper.DefaultCompany.EShortName));//默认公司的英文名字
            paramList.Add(new ReportParameter("CompanyAddress", ReportCenterHelper.DefaultCompany.EAddress));
            paramList.Add(new ReportParameter("TelOrFax", ReportCenterHelper.DefaultCompanyTelAndFax));
            paramList.Add(new ReportParameter("JobPlace", "业务发生地:" + LocalData.UserInfo.DefaultCompanyName));
            paramList.Add(new ReportParameter("ConditionRemark", (LocalData.IsEnglish ? "部门" : "Department") + "  : " + LocalData.UserInfo.DefaultDepartmentName));
            paramList.Add(new ReportParameter("IsEnglish", LocalData.IsEnglish ? "1" : "0"));

            string reportName = string.Empty;
            reportName = "RPT_Debitnote_DetailDR";
            paramList.Add(new ReportParameter("OtherFields", "往来单位"));
            paramList.Add(new ReportParameter("ShippingLineSet", string.Empty));
            paramList.Add(new ReportParameter("Type", "0"));

            ReportData rd = new ReportData { Parameters = paramList, ReportName = reportName };

            reportView.ReportName = rd.ReportName;
            reportView.ParamList = rd.Parameters;
            if (string.IsNullOrEmpty(rd.ServiceReportPath))
                reportView.DisplayData();
            else
                reportView.DisplayData(rd.ServiceReportPath);
        }
    }
}
