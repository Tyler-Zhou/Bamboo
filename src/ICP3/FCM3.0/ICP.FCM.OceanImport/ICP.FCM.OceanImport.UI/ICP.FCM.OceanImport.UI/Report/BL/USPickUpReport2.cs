using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Common.UI.ReportView;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;
using ICP.OA.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanImport.UI.Report.BL
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class USPickUpReport2 : CompositeReportViewPart
    {
        #region Service
        [ServiceDependency]
        public IOIReportDataService OIReportSrvice { get; set; }
        [ServiceDependency]
        public IOceanImportService oiService { get; set; }

        [ServiceDependency]
        public IConfigureService configureService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ICustomerService CustomerService { get; set; }

        #endregion

        #region 字段及属性定义

        Guid _HARVESTCompanyId = new Guid("e563e2c9-6ca7-412b-99a4-f246379720c0");//温哥华 公司

        string CompanyName;
        string CompanyTel;
        string CompanyFax;
        string CompanyAddress;


        //public bool Short
        //{
        //    get
        //    {
        //        return (bool)this.rdoArrivalNotice.EditValue;
        //    }
        //}

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

        public USPickUpReport2()
        {
            InitializeComponent();
        }

        protected override void LoadData()
        {
            rdoArrivalNotice.SelectedIndex = 0;
            ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(_businessInfo.CompanyID);
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
            pickUpReportData.PickUpAtDescription = _currentTruckInfo.PickUpAtDescription.ToString(true);
            pickUpReportData.DeliveryAtDescription = _currentTruckInfo.DeliveryAtDescription.ToString(true);
            pickUpReportData.BillToDescription = _currentTruckInfo.BillToDescription.ToString(true);
            pickUpReportData.CntrRetrun = _businessInfo.MBLInfo.ReturnLocationName;
            pickUpReportData.Commodity = _currentTruckInfo.Commodity;
            pickUpReportData.PickupDate = _currentTruckInfo.PickUpDate == null ? string.Empty : _currentTruckInfo.PickUpDate.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            pickUpReportData.DeliveryDate = _currentTruckInfo.DeliveryDate == null ? string.Empty : _currentTruckInfo.DeliveryDate.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            pickUpReportData.BillingReferenceNo = _businessInfo.No;
            pickUpReportData.Carrier = _businessInfo.MBLInfo.CarrierName;
            pickUpReportData.VesselVoyageNo = _businessInfo.MBLInfo.VoyageName;
            pickUpReportData.MasterBLNo = _businessInfo.MBLInfo.MBLNo;
            pickUpReportData.ITNo = _businessInfo.ITNO;
            if (_businessInfo.CargoDescription != null && _businessInfo.CargoDescription.Cargo != null)
            {
                pickUpReportData.GoodsDescription = _businessInfo.CargoDescription.Cargo.ToString(true);
            }

            pickUpReportData.PKGS = _businessInfo.QuantityUnit;
            pickUpReportData.Weight = _businessInfo.WeightUnit;
            pickUpReportData.Measurement = _businessInfo.MeasurementUnit;

            string hblno = string.Empty;
            string amsno = string.Empty;
            if (_businessInfo.HBLList != null && _businessInfo.HBLList.Count > 0)
            {
                foreach (var hbl in _businessInfo.HBLList)
                {
                    if (!string.IsNullOrEmpty(hblno) && !string.IsNullOrEmpty(hbl.HBLNo))
                    {
                        hblno += ",";
                    }

                    if (!string.IsNullOrEmpty(amsno) && !string.IsNullOrEmpty(hbl.AMSNo))
                    {
                        amsno += ",";
                    }

                    hblno += hbl.HBLNo;
                    amsno += hbl.AMSNo;
                }
            }

            pickUpReportData.HouseBLNo = hblno;
            pickUpReportData.AMSHouseBLNo = amsno;
            pickUpReportData.POD = _businessInfo.PODName;
            pickUpReportData.PlaceOfDelivery = _businessInfo.FinalDestinationName;
            pickUpReportData.ETA = _businessInfo.ETA == null ? string.Empty : _businessInfo.ETA.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            pickUpReportData.DETA = _businessInfo.DETA == null ? string.Empty : _businessInfo.DETA.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            if (_businessInfo.CompanyID == _HARVESTCompanyId)
            {
                pickUpReportData.SPECIALINSTRUCTION = string.Empty;
            }
            else
            {
                pickUpReportData.SPECIALINSTRUCTION = "CITY OCEAN REQUIRES ALL DELIVERIES TO BE LIVE-UNLOAD BASIS. IT IS TRUCKER'S RESPONSIBILITY TO RETURN THE EMPTY EQUIPMENTS PROMPTLY AFTER DELIVERY. CITY OCEAN WILL NOT BE RESPONSIBLE FOR ANY DRIVER DETENTION OR PER-DIEM WITHOUT PRIOR NOTIFICATION AND AUTHORIZATION.";
            }

            #region 箱号 加上免堆期
            string containerNos = string.Empty;
            foreach (var box in _businessInfo.ContainerList)
            {
                //if (_currentTruckInfo.ContainerIDList.Contains(new Guid(box.ID.ToString())))
                //{
                if (!string.IsNullOrEmpty(box.No) && !string.IsNullOrEmpty(containerNos))
                {
                    containerNos += ", ";
                }

                containerNos += box.No;
                if (!string.IsNullOrEmpty(box.ContainerTypeName))
                {
                    containerNos += "/" + box.ContainerTypeName;
                }

                if (!string.IsNullOrEmpty(box.SealNo))
                {
                    containerNos += "/" + box.SealNo;
                }

                if (box.LFDate != null)
                {
                    //containerNos +="/" +box.ContainerTypeName+"/"+box.SealNo+"/"+box.LFDate.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    containerNos += "/Last Free day:" + box.LFDate.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                }
                //}
            }

            pickUpReportData.ContainerNOs = containerNos;
            #endregion

            string reportFilePath = System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanImport\\" + "OIpickupdelivery.frx";
            Dictionary<string, object> dicData = new Dictionary<string, object>();
            dicData.Add("ReportData", pickUpReportData);

            try
            {
                reportViewer.BindData(reportFilePath, dicData, null, GetOperationInfo());
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        #endregion

        #region IPart 成员

        OceanImportTruckInfo _currentTruckInfo = null;
        OceanBusinessInfo _businessInfo = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            string truckInfoKey = "OceanImportTruckInfo";
            string idKey = "BusinessID";

            if (values.ContainsKey(truckInfoKey))
            {
                _currentTruckInfo = values[truckInfoKey] as OceanImportTruckInfo;
                _businessInfo = oiService.GetBusinessInfo(_currentTruckInfo.OIBookingID);
            }
            if (values.ContainsKey(idKey))
            {
                Guid businessID = (Guid)values[idKey];
                _businessInfo = oiService.GetBusinessInfoByEdit(businessID);
            }

        }

        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (_currentTruckInfo == null)
                return null;

            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanImport;
            message.UserProperties.OperationId = _currentTruckInfo.OIBookingID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = _currentTruckInfo.ID;
            return message;
        }
        #endregion
    }
}
