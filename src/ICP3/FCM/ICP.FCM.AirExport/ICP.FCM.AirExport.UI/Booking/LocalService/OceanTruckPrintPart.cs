using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;

using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FCM.AirExport.UI.Booking
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class AirTruckPrintPart : BaseEditPart
    {
        #region Service

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

        public AirTruckPrintPart()
        {
            InitializeComponent();
            Disposed += delegate {
                
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            if (LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            //labHead.Text = "标题";
            //labLOGO.Text = "图标";
            //labCompanyName.Text = "样式";
            //labAddress.Text = "抬头";
            //groupStyle.Text = "样式";
            //btnShow.Text = "确定";
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
            //Print();
        }

        //private void Print()
        //{
        //    try
        //    {
        //        BindingSource bs = new BindingSource();
        //        string reportFilePath = string.Empty;
        //        if (rdoStyle.SelectedIndex ==0)
        //        {
        //            PickupENReportData data = oeReportSrvice.GetPickupENReportData(_TruckID);
        //            data.CompanyName = txtCompanyName.Text.Trim();
        //            data.CompanyAddress = txtAddress.Text.Trim();

        //            StringBuilder telFaxBulider = new StringBuilder();
        //            if (txtTel.Text.Trim().Length > 0) telFaxBulider.Append("Tel:" + txtTel.Text.Trim());
        //            if (txtFax.Text.Trim().Length > 0)
        //            {
        //                if (telFaxBulider.Length > 0) telFaxBulider.Append(" ");

        //                telFaxBulider.Append("Fax:" + txtFax.Text.Trim());
        //            }
        //            data.CompanyTelFax = telFaxBulider.ToString();

        //            data.CurrentUserName = LocalData.UserInfo.LoginName + (txtEmail.Text.Trim().Length == 0 ? string.Empty : ("//" + txtEmail.Text.Trim()));
        //            bs.DataSource = data;
        //            reportFilePath = AirExportPrintHelper.GetOEReportPath() + "OE_PickupDelivery_EN.frx";
        //        }
        //        else
        //        {
        //            //短格式
        //            PickupENShortReportData data = oeReportSrvice.GetPickupENShortReportData(_TruckID);
        //            data.CompanyName = txtCompanyName.Text.Trim();
        //            data.CompanyAddress = txtAddress.Text.Trim();

        //            StringBuilder telFaxBulider = new StringBuilder();
        //            if (txtTel.Text.Trim().Length > 0) telFaxBulider.Append("Tel:" + txtTel.Text.Trim());
        //            if (txtFax.Text.Trim().Length > 0)
        //            {
        //                if (telFaxBulider.Length > 0) telFaxBulider.Append("\r\n");

        //                telFaxBulider.Append("Fax:" + txtFax.Text.Trim());
        //            }
        //            if (telFaxBulider.Length > 0)
        //            {
        //                data.CompanyAddress += "\r\n" + telFaxBulider.ToString();
        //            }
        //            data.CurrentUserName = LocalData.UserInfo.LoginName + (txtEmail.Text.Trim().Length == 0 ? string.Empty : ("//" + txtEmail.Text.Trim()));
        //            bs.DataSource = data;
        //            reportFilePath = AirExportPrintHelper.GetOEReportPath() + "OE_PickupDelivery_EN_Short.frx";
        //        }
 
        //        this.report1.Preview = this.previewControl1;
        //        report1.Load(reportFilePath);
        //        this.report1.RegisterData(bs, "ReportSource");
        //        this.report1.Show();
        //    }
        //    catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
        //}

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
