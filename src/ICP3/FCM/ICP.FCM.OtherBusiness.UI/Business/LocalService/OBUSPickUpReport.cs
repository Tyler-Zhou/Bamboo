using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Common.UI.ReportView;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.Sys.ServiceInterface;

namespace ICP.FCM.OtherBusiness.UI.Business
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OBUSPickUpReport : CompositeReportViewPart
    {
        #region Service

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        public IOtherBusinessService OtherBusinessService
        {
            get
            {
                return ServiceClient.GetService<IOtherBusinessService>();
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

        #endregion

        #region 字段及属性定义

        Guid _HARVESTCompanyId = new Guid("e563e2c9-6ca7-412b-99a4-f246379720c0");//温哥华 公司

        new string CompanyName;
        string CompanyTel;
        string CompanyFax;
        string CompanyAddress;

        /// <summary>
        /// 
        /// </summary>
        public string CompanyNameM
        {
            get
            {
                return this.txtCompanyName.Text.Trim().ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
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

        public OBUSPickUpReport()
        {
            InitializeComponent();
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
            pickUpReportData.BillToDescription = _currentTruckInfo.BillToDescription.ToString(true);
            //pickUpReportData.CntrRetrun = _businessInfo.MBLInfo.ReturnLocationName;
            pickUpReportData.Commodity = _currentTruckInfo.Commodity;
            pickUpReportData.PickupDate = _currentTruckInfo.LoadingTime == null ? string.Empty : _currentTruckInfo.LoadingTime.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            pickUpReportData.DeliveryDate = _currentTruckInfo.DeliveryDate == null ? string.Empty : _currentTruckInfo.DeliveryDate.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            pickUpReportData.BillingReferenceNo = _businessInfo.NO;
            pickUpReportData.Carrier = _businessInfo.CarrierName;
            pickUpReportData.VesselVoyageNo = _businessInfo.VesselVoyage;
            pickUpReportData.MasterBLNo = _businessInfo.Mblno;
            //pickUpReportData.ITNo = _businessInfo.ITNO;
            //if (_businessInfo.CargoDescription != null && _businessInfo.CargoDescription.Cargo != null)
            //{
            //    pickUpReportData.GoodsDescription = _businessInfo.CargoDescription.Cargo.ToString(true);
            //}

            pickUpReportData.PKGS = _businessInfo.Quantity + _businessInfo.QuantityUnitName;
            pickUpReportData.Weight = _businessInfo.Weight + _businessInfo.WeightUnitName;
            pickUpReportData.Measurement = _businessInfo.Measurement + _businessInfo.MeasurementUnitName;

            string hblno = string.Empty;
            string amsno = string.Empty;
            //if (_businessInfo.HBLList != null && _businessInfo.HBLList.Count > 0)
            //{
            //    foreach (var hbl in _businessInfo.HBLList)
            //    {
            //        if (!string.IsNullOrEmpty(hblno) && !string.IsNullOrEmpty(hbl.HBLNo))
            //        {
            //            hblno += ",";
            //        }

            //        if (!string.IsNullOrEmpty(amsno) && !string.IsNullOrEmpty(hbl.AMSNo))
            //        {
            //            amsno += ",";
            //        }

            //        hblno += hbl.HBLNo;
            //        amsno += hbl.AMSNo;
            //    }
            //}

            pickUpReportData.HouseBLNo = _businessInfo.Hblno;
            pickUpReportData.AMSHouseBLNo = amsno;
            pickUpReportData.POD = _businessInfo.PodName;
            pickUpReportData.PlaceOfDelivery = _businessInfo.FinalDestinationName;
            pickUpReportData.ETA = _businessInfo.Eta == null ? string.Empty : _businessInfo.Eta.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            pickUpReportData.DETA = _businessInfo.Feta == null ? string.Empty : _businessInfo.Feta.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            if (_businessInfo.CompanyID == _HARVESTCompanyId)
            {
                pickUpReportData.SPECIALINSTRUCTION = string.Empty;
            }
            else
            {
                pickUpReportData.SPECIALINSTRUCTION = "CITY OCEAN REQUIRES ALL DELIVERIES TO BE LIVE-UNLOAD BASIS. IT IS TRUCKER'S RESPONSIBILITY TO RETURN THE EMPTY EQUIPMENTS PROMPTLY AFTER DELIVERY. CITY OCEAN WILL NOT BE RESPONSIBLE FOR ANY DRIVER DETENTION OR PER-DIEM WITHOUT PRIOR NOTIFICATION AND AUTHORIZATION.";
            }

            #region 箱号
            string containerNos = string.Empty;
            List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.OBContainerList> containerList = OtherBusinessService.GetOtherContainerList(_businessInfo.ID, _businessInfo.CompanyID);
            if (containerList != null && containerList.Count > 0)
            {
                foreach (var box in containerList)
                {
                    if (!string.IsNullOrEmpty(box.No) && !string.IsNullOrEmpty(containerNos))
                    {
                        containerNos += ", ";
                    }

                    containerNos += box.No;

                    if (!string.IsNullOrEmpty(box.TypeName))
                    {
                        containerNos += "/" + box.TypeName;
                    }

                    if (!string.IsNullOrEmpty(box.SealNo))
                    {
                        containerNos += "/" + box.SealNo;
                    }
                }
            }

            pickUpReportData.ContainerNOs = containerNos;
            #endregion
            //#region 箱号 加上免堆期
            //string containerNos = string.Empty;
            //foreach (var box in _businessInfo.ContainerList)
            //{
            //    //if (_currentTruckInfo.ContainerIDList.Contains(new Guid(box.ID.ToString())))
            //    //{
            //    if (!string.IsNullOrEmpty(box.No) && !string.IsNullOrEmpty(containerNos))
            //    {
            //        containerNos += ", ";
            //    }

            //    containerNos += box.No;
            //    if (!string.IsNullOrEmpty(box.ContainerTypeName))
            //    {
            //        containerNos += "/" + box.ContainerTypeName;
            //    }

            //    if (!string.IsNullOrEmpty(box.SealNo))
            //    {
            //        containerNos += "/" + box.SealNo;
            //    }

            //    if (box.LFDate != null)
            //    {
            //        //containerNos +="/" +box.ContainerTypeName+"/"+box.SealNo+"/"+box.LFDate.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            //        containerNos += "/Last Free day:" + box.LFDate.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            //    }
            //    //}
            //}

            //pickUpReportData.ContainerNOs = containerNos;
            //#endregion

            string reportFilePath = System.Windows.Forms.Application.StartupPath + "\\Reports\\OtherBusiness\\" + "OBpickupdelivery.frx";
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

        TruckInfo _currentTruckInfo = null;
        OtherBusinessInfo _businessInfo = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            string truckInfoKey = "OtherTruckInfo";
            string idKey = "OtherBookingListID";
            string companyIdKey = "CompanyID";

            if (values.ContainsKey(truckInfoKey))
            {
                _currentTruckInfo = values[truckInfoKey] as TruckInfo;
            }
            if (values.ContainsKey(idKey))
            {
                Guid businessID = (Guid)values[idKey];
                Guid companyID = (Guid)values[companyIdKey];
                _businessInfo = OtherBusinessService.GetOtherBusinessInfo(businessID, companyID);
            }          
        }

        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (_currentTruckInfo == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties = new ICP.Message.ServiceInterface.MessageUserPropertiesObject();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.Other ;
            message.UserProperties.OperationId = _currentTruckInfo.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = _currentTruckInfo.ID;

            return message;
        }

        #endregion  
    }
}
