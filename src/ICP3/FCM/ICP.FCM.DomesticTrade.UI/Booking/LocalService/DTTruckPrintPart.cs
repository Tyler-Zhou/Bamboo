using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using ICP.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.DomesticTrade.ServiceInterface;

using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.ReportObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FCM.DomesticTrade.UI.Booking
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class DTTruckPrintPart : BaseEditPart
    {
        #region Service
        public IDTReportDataService DTReportDataSrvice
        {
            get
            {
                return ServiceClient.GetService<IDTReportDataService>();
            }
        }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public DomesticTradePrintHelper DomesticTradePrintHelper
        {
            get
            {
                return ClientHelper.Get<DomesticTradePrintHelper, DomesticTradePrintHelper>();
            }
        }
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        #endregion

        #region 属性

        bool needCloseBL = false;

        #endregion

        #region init

        public DTTruckPrintPart()
        {
            InitializeComponent();
            Disposed += delegate {
                
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
            SetCompanyInfo();
        }

        private void SetCompanyInfo()
        {
            ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
            if (configure == null) return;

            CustomerInfo customer = CustomerService.GetCustomerInfo(configure.CustomerID);
            if (customer == null) return;

            txtCompanyName.Text = customer.EName;
            txtAddress.Text = customer.EAddress;
            txtTel.Text = customer.Tel1;
            txtFax.Text = customer.Fax;
            txtEmail.Text = UserService.GetUserInfo(LocalData.UserInfo.LoginID).EMail;
        }

        #endregion

        #region Print

        private void btnShow_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void Print()
        {
            try
            {
                BindingSource bs = new BindingSource();
                string reportFilePath = string.Empty;
                if (rdoStyle.SelectedIndex ==0)
                {
                    PickupENReportData data = DTReportDataSrvice.GetPickupENReportData(_TruckID);
                    data.CompanyName = txtCompanyName.Text.Trim();
                    data.CompanyAddress = txtAddress.Text.Trim();

                    StringBuilder telFaxBulider = new StringBuilder();
                    if (txtTel.Text.Trim().Length > 0) telFaxBulider.Append("Tel:" + txtTel.Text.Trim());
                    if (txtFax.Text.Trim().Length > 0)
                    {
                        if (telFaxBulider.Length > 0) telFaxBulider.Append(" ");

                        telFaxBulider.Append("Fax:" + txtFax.Text.Trim());
                    }
                    data.CompanyTelFax = telFaxBulider.ToString();

                    data.CurrentUserName = LocalData.UserInfo.LoginName + (txtEmail.Text.Trim().Length == 0 ? string.Empty : ("//" + txtEmail.Text.Trim()));
                    bs.DataSource = data;
                    reportFilePath = DomesticTradePrintHelper.GetOEReportPath() + "OE_PickupDelivery_EN.frx";
                }
                else
                {
                    //短格式
                    PickupENShortReportData data = DTReportDataSrvice.GetPickupENShortReportData(_TruckID);
                    data.CompanyName = txtCompanyName.Text.Trim();
                    data.CompanyAddress = txtAddress.Text.Trim();

                    StringBuilder telFaxBulider = new StringBuilder();
                    if (txtTel.Text.Trim().Length > 0) telFaxBulider.Append("Tel:" + txtTel.Text.Trim());
                    if (txtFax.Text.Trim().Length > 0)
                    {
                        if (telFaxBulider.Length > 0) telFaxBulider.Append("\r\n");

                        telFaxBulider.Append("Fax:" + txtFax.Text.Trim());
                    }
                    if (telFaxBulider.Length > 0)
                    {
                        data.CompanyAddress += "\r\n" + telFaxBulider.ToString();
                    }
                    data.CurrentUserName = LocalData.UserInfo.LoginName + (txtEmail.Text.Trim().Length == 0 ? string.Empty : ("//" + txtEmail.Text.Trim()));
                    bs.DataSource = data;
                    reportFilePath = DomesticTradePrintHelper.GetOEReportPath() + "OE_PickupDelivery_EN_Short.frx";
                }

                report1.Preview = previewControl1.previewControl1;
                report1.Load(reportFilePath);
                report1.RegisterData(bs, "ReportSource");
                report1.Show();
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
        }

        #endregion

        #region IPart 成员
        Guid _TruckID = Guid.Empty;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "TruckID" && item.Value !=null)
                {
                    _TruckID = new Guid(item.Value.ToString());
                }
            }
        }
        #endregion

        #region 其它事件

        private void chkManualTitle_CheckedChanged(object sender, EventArgs e)
        {
            paneTitle.Enabled = !chkManualTitle.Checked;
        }

        #endregion
    }
}
