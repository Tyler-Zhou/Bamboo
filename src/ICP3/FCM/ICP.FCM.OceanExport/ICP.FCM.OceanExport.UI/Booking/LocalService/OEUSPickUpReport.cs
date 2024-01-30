using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Common.UI.ReportView;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;

namespace ICP.FCM.OceanExport.UI.Booking
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OEUSPickUpReport : CompositeReportViewPart
    {
        #region Service

        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        public ICP.Common.ServiceInterface.ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICP.Common.ServiceInterface.ICustomerService>();
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
        public WorkItem WorkItem
        {
            get;
            set;
        }

        #endregion

        #region 字段及属性定义
        string CompanyName;
        string CompanyTel;
        string CompanyFax;
        string CompanyAddress;

        public string CompanyNameM
        {
            get
            {
                return this.txtCompanyName.Text.Trim().ToString();
            }
        }
        public string CompanyAdddress
        {
            get
            {
                return this.txtCompanyAddress.Text.Trim().ToString();
            }
        }
        public string Tel
        {
            get
            {
                return this.txtTelephone.Text.Trim().ToString();
            }
        }
        public string Fax
        {
            get
            {
                return this.txtFax.Text.Trim().ToString();
            }
        }
        public string Email
        {
            get
            {
                return this.txtEmail.Text.Trim().ToString();
            }

        }
        public bool Manual
        {
            get
            {
                return this.chkManualTitle.Checked;
            }
        }

        #endregion

        public OEUSPickUpReport()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this._businessInfo = null;
                this._currentTruckInfo = null;
                this.chkManualTitle.CheckedChanged -= this.chkManualTitle_CheckedChanged;
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }

        protected override void LoadData()
        {
            rdoArrivalNotice.SelectedIndex = 0;
            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(_businessInfo.CompanyID);
            CustomerInfo customerInfo = CustomerService.GetCustomerInfo(configureInfo.CustomerID);
            this.txtCompanyAddress.Text = this.CompanyAddress = customerInfo.EAddress;
            this.txtCompanyName.Text = this.CompanyName = customerInfo.EName;
            this.txtTelephone.Text = this.CompanyTel = customerInfo.Tel1;
            this.txtFax.Text = this.CompanyFax = customerInfo.Fax;
            this.txtCompanyName.Properties.ReadOnly = true;
            this.txtCompanyAddress.Properties.ReadOnly = true;
            this.txtTelephone.Properties.ReadOnly = true;
            this.txtFax.Properties.ReadOnly = true;
            this.txtEmail.Properties.ReadOnly = true;         
        }
        private void chkManualTitle_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkManualTitle.Checked)
            {
                this.txtCompanyName.Properties.ReadOnly = false;
                this.txtCompanyAddress.Properties.ReadOnly = false;
                this.txtTelephone.Properties.ReadOnly = false;
                this.txtFax.Properties.ReadOnly = false;
                this.txtEmail.Properties.ReadOnly = false;
                this.txtCompanyName.Text = "";
                this.txtCompanyName.Focus();
            }
            else
            {
                this.txtCompanyName.Properties.ReadOnly = true;
                this.txtCompanyAddress.Properties.ReadOnly = true;
                this.txtTelephone.Properties.ReadOnly = true;
                this.txtFax.Properties.ReadOnly = true;
                this.txtEmail.Properties.ReadOnly = true;
            }
        }

        protected override void Query()
        {
            if (rdoArrivalNotice.SelectedIndex == 0)
            {
                PrintPickUpDelivery();
            }
            else if (rdoArrivalNotice.SelectedIndex == 1)
            {
                //PrintPickUpDeliveryShort();
            }
        }

        #region Print

        private void PrintPickUpDelivery()
        {
            PickUpReportData pickUpReportData = new PickUpReportData();
            pickUpReportData.CompanyAddress = this.CompanyAddress.Replace("\r\n", "\r");
            string TelFax = string.Empty;
            if (this.Manual)
            {
                TelFax = "TEl:" + this.Tel + " FAX:" + this.Fax + " Email:" + this.Email;
                pickUpReportData.CompanyName = this.CompanyNameM;
            }
            else
            {
                TelFax = "TEl:" + this.CompanyTel + " FAX:" + this.CompanyFax;
                pickUpReportData.CompanyName = this.CompanyName;
            }

            pickUpReportData.CompanyTelFax = TelFax;
            pickUpReportData.CurrentDate = DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            UserInfo userInfo = UserService.GetUserInfo(LocalData.UserInfo.LoginID);
            pickUpReportData.CurrentUserEmail = LocalData.UserInfo.LoginName + " /Email:" + userInfo.EMail;
            pickUpReportData.TruckerDescription = _currentTruckInfo.TruckerDescription.ToString(true);
            pickUpReportData.PickUpAtDescription = _currentTruckInfo.ShipperDescription.ToString(true);
            pickUpReportData.DeliveryAtDescription = _currentTruckInfo.DeliveryAtDescription.ToString(true);
            pickUpReportData.BillToDescription = _currentTruckInfo.BillToDescription== null? string.Empty: _currentTruckInfo.BillToDescription.ToString(true);
            pickUpReportData.CntrRetrun = _currentTruckInfo.PUEmptyCNTRDescription== null? string.Empty: _currentTruckInfo.PUEmptyCNTRDescription.ToString(true);
            pickUpReportData.Commodity = _currentTruckInfo.Commodity;
            pickUpReportData.TotalPkgs = _currentTruckInfo.TotalPkgs;
            pickUpReportData.PickupDate = _currentTruckInfo.LoadingTime == null ? string.Empty : _currentTruckInfo.LoadingTime.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            pickUpReportData.DeliveryDate = _currentTruckInfo.DeliveryDate == null ? string.Empty : _currentTruckInfo.DeliveryDate.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            pickUpReportData.BillingReferenceNo = _businessInfo.No;
            pickUpReportData.Carrier = _businessInfo.CarrierName;
            pickUpReportData.VesselVoyageNo = _businessInfo.VoyageName;
            //pickUpReportData.MasterBLNo = _businessInfo.MBLNo;
            //pickUpReportData.GoodsDescription = _businessInfo.CargoDescription.Cargo.ToString(true);
            pickUpReportData.PKGS = _businessInfo.Quantity + _businessInfo.QuantityUnitName;
            pickUpReportData.Weight = _businessInfo.Weight+ _businessInfo.WeightUnitName;
            pickUpReportData.Measurement = _businessInfo.Measurement+ _businessInfo.MeasurementUnitName;
            pickUpReportData.SoNO = _businessInfo.OceanShippingOrderNo;
            pickUpReportData.CutOffDate = _businessInfo.ClosingDate == null ? string.Empty : _businessInfo.ClosingDate.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            pickUpReportData.FreigtDescription = _currentTruckInfo.FreigtDescription;
            pickUpReportData.Remark = _currentTruckInfo.Remark;
            pickUpReportData.CtnrDes = _currentTruckInfo.ContainerDescription.ToString();
            pickUpReportData.TruckingCharge = _currentTruckInfo.TruckingCharge;


            string hblNo = string.Empty;
            if (_businessInfo.OceanHBLs != null && _businessInfo.OceanHBLs.Count > 0)
            {
                foreach (var hbl in _businessInfo.OceanHBLs)
                {
                    if (!string.IsNullOrEmpty(hblNo))
                    {
                        hblNo += ", ";
                    }
                    hblNo += hbl.NO;
                }
            }

            pickUpReportData.HouseBLNo = hblNo;
            //如果收货地为空，就默认收货地的值为装货港
            pickUpReportData.POR = (string.IsNullOrEmpty(_businessInfo.PlaceOfReceiptName) ? _businessInfo.POLName : _businessInfo.PlaceOfReceiptName);
            pickUpReportData.POD = _businessInfo.PODName;
            pickUpReportData.Final = _businessInfo.FinalDestinationName;
            pickUpReportData.PlaceOfDelivery = _businessInfo.PlaceOfReceiptName;
            pickUpReportData.ETA = _businessInfo.ETA == null ? string.Empty : _businessInfo.ETA.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    
            string reportFilePath = System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanExport\\" + "OEpickupdelivery.frx";
            Dictionary<string, object> dicData = new Dictionary<string, object>();
            dicData.Add("ReportData", pickUpReportData);

            try
            {
                reportViewer.BindData(reportFilePath, dicData, null, null);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        #endregion

        #region IPart 成员

        OceanTruckInfo _currentTruckInfo = null;
        OceanBookingInfo _businessInfo = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            string truckInfoKey = "OceanTruckInfo";
            string idKey = "OceanBookingListID";

            if (values.ContainsKey(truckInfoKey))
            {
                _currentTruckInfo = values[truckInfoKey] as OceanTruckInfo;
                //_businessInfo = oeService.GetOceanBookingInfo(idKey);  
            }
            if (values.ContainsKey(idKey))
            {
                Guid businessID = (Guid)values[idKey];
                _businessInfo = OceanExportService.GetOceanBookingInfo(businessID);
            }
           
        }

        #endregion  
    }
}
