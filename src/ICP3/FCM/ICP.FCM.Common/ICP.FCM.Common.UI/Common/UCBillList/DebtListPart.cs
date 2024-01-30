using System;
using System.ComponentModel;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.ReportCenter.UI;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.Common.UI.UCDebtList
{
    [ToolboxItem(false)]
    public partial class UCDebtListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart, IDataBind 
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ICP.FCM.Common.ServiceInterface.IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<ICP.FCM.Common.ServiceInterface.IFCMCommonService>();
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

        public UCDebtListPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this._OperationCommonInfo = null;
                this.context = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    if (this.reportView != null)
                    {
                        this.Workitem.Items.Remove(this.reportView);
                        this.reportView = null;
                    }
                    Workitem = null;
                }
            };
        }

        #endregion
        ReportViewBase reportView = null;
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

        BusinessOperationContext context = null;
        private void BindingData(object value)
        {
            context = value as BusinessOperationContext;

            if (context != null)
            {
                _OperationCommonInfo = FCMCommonService.GetOperationCommonInfo(context.OperationID, context.OperationType);

            }
            cmbCustomer.Properties.Items.Clear();
            if (_OperationCommonInfo == null)
            {
                this.Enabled = false;
            }
            else
            {
                this.Enabled = true;
                cmbCustomer.Properties.Items.Clear();
                foreach (var item in _OperationCommonInfo.TradeCustomers)
                {
                    cmbCustomer.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.EName, item.ID));
                }
                cmbCustomer.SelectedIndex = 0;
            }
        }
        bool isAddReportViewControl = false;
        private void AddReportViewControl()
        {
            if (isAddReportViewControl)
                return;
            reportView = Workitem.Items.AddNew<ReportViewBase>();
            reportView.Dock = DockStyle.Fill;
            this.pnlMain.Controls.Clear();
            this.pnlMain.Controls.Add(reportView);
            isAddReportViewControl = true;
        }
        #endregion

        private void btnShow_Click_1(object sender, EventArgs e)
        {
            if (cmbCustomer.EditValue == null) return;
            AddReportViewControl();

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
            paramList.Add(new ReportParameter("ShipperSet", this.cmbCustomer.EditValue.ToString()));
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

        #region IDataBind 成员

        public void ControlsReadOnly(bool flg)
        {
            cmbCustomer.Enabled =btnShow.Enabled =labBillTo.Enabled = true;
        }

        public void DataBind(BusinessOperationContext business)
        {
            BindingData(business);
        }

        #endregion
    }
}
