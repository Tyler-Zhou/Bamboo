using System;
using System.Collections.Generic;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;

using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.EDI.ServiceInterface;
using ICP.EDI.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanExport.UI
{
    public partial class CSCLBookingEditPart : DevExpress.XtraEditors.XtraUserControl, IChildPart
    {
        #region Service
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }

        IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }

        IEDIService EDIService
        {
            get
            {
                return ServiceClient.GetService<IEDIService>();
            }
        }

        #endregion

        [State("IsOrderPO")]
        public bool IsOrderPO { get; set; }

        public void SetService(WorkItem workItem)
        {
            this.Workitem = workItem;
        }

        #region 属性

        bool _isdDesignMode = true;
        public bool IsDesignMode
        {
            get
            {
                return _isdDesignMode;
            }
            set
            {
                _isdDesignMode = value;
            }
        }

        public OceanBookingInfo BookingInfo { get; set; }

        #endregion

        //#region init

        public CSCLBookingEditPart()
        {
            InitializeComponent();

            this.Disposed += delegate
            {
                this.BookingInfo = null;
                this.DataChanged = null;
                this.bindingSource1.DataSource = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            cmbBLTitle.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("CITY OCEAN LOGISTICS CO.,LTD.", "CITY OCEAN LOGISTICS CO.,LTD."));
            cmbBLTitle.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("TOP SHIPPING LOGISTICS CO.,LTD.", "TOP SHIPPING LOGISTICS CO.,LTD."));
        }

        #region IChildPart 成员

        public event EventHandler DataChanged;

        bool _isChanged = false;
        public bool IsChanged
        {
            get
            {
                if (_isChanged == false)
                {
                    CSCLBookingInfo cscl = DataSource as CSCLBookingInfo;
                    if (cscl != null)
                    {
                        if (cscl.IsDirty) return true;
                    }
                }

                return _isChanged;
            }
        }

        public bool ValidateData()
        {
            this.bindingSource1.EndEdit();
            bool isScrr = true;

            if (DataSource == null)
            {
                //界面尚未加载过
                return true;
            }
            return isScrr;
        }

        public void AfterSaved()
        {
            _isChanged = false;
        }

        public object DataSource { get { return bindingSource1.DataSource; } }

        public void SetSource(object value)
        {
            if (value == null) return;

            _isChanged = false;
            this.bindingSource1.DataSource = value;
            this.bindingSource1.ResetBindings(false);
        }


        #endregion


        #region 初始化数据

        public Guid OrderId { get; set; }
        bool c = true;
        CSCLBookingInfo csclBookingInfo = null;
        public void InitData(OceanBookingInfo bookingInfo)
        {
            BookingInfo = bookingInfo;
            if (bookingInfo != null)
            {
                if (bookingInfo.CarrierCode != "CSCL")
                {
                    groupControl3.Enabled = false;
                    chkAMS.Enabled = false;
                    return;
                }

                this.OrderId = bookingInfo.ID;

                if (c)
                {
                    csclBookingInfo = OceanExportService.GetCsclBookingInfo(this.OrderId);
                    c = false;
                }
                if (csclBookingInfo == null)
                {
                    csclBookingInfo = new CSCLBookingInfo();
                    //csclBookingInfo.Shipper = LocalData.UserInfo.DefaultCompanyName; //bookingInfo.ShipperDescription.Name + "^" + bookingInfo.ShipperDescription.Address;
                    if (bookingInfo.AgentDescription != null)
                        csclBookingInfo.Consignee = bookingInfo.AgentDescription.Name + System.Environment.NewLine + bookingInfo.AgentDescription.Address.Replace(System.Environment.NewLine, " ");
                    else
                        csclBookingInfo.Consignee = bookingInfo.ConsigneeDescription.Name + System.Environment.NewLine + bookingInfo.ConsigneeDescription.Address.Replace(System.Environment.NewLine, " ");
                    csclBookingInfo.Notify = "SAME AS CONSIGNEE.";
                    //csclBookingInfo.DeliveryTerm = bookingInfo.TransportClauseName;
                    //csclBookingInfo.RemarksCN = bookingInfo.Remark;
                }
                this.SetSource(csclBookingInfo);
                if (chkAMS.Checked)
                    groupControl1.Visible = true;
                else
                    groupControl1.Visible = false;

            }
        }

        #endregion

        #region 保存

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public CSCLBookingSaveRequest SaveCsclBookingInfo(Guid orderId)
        {
            CSCLBookingSaveRequest result = null;
            if (this.ValidateData())
            {
                CSCLBookingInfo info = DataSource as CSCLBookingInfo;
                if (info != null)
                {
                    info.OceanBookingID = orderId;
                    info.SaveByID = LocalData.UserInfo.LoginID;
                    result = ConvertOjbect(info);
                }
                else
                {
                    result = new CSCLBookingSaveRequest();
                    result.OceanBookingID = orderId;
                    result.SaveByID = LocalData.UserInfo.LoginID;
                }
            }
            return result;
        }

        private CSCLBookingSaveRequest ConvertOjbect(CSCLBookingInfo info)
        {
            CSCLBookingSaveRequest res = new CSCLBookingSaveRequest();
            res.BookingNO = info.BookingNO;
            res.BookingRemarksCN = info.BookingRemarksCN;
            res.CargoDescUS = info.CargoDescUS;
            res.Consignee = info.Consignee;
            res.DeliveryTerm = info.DeliveryTerm;
            res.HBLNO = info.HBLNO;
            res.ID = info.ID;
            res.Marks = info.Marks;
            res.Notify = info.Notify;
            res.OceanBookingID = info.OceanBookingID;
            res.RealConsignee = info.RealConsignee;
            res.RealNotify = info.RealNotify;
            res.RealShipper = info.RealShipper;
            res.ReleaseCargoType = info.ReleaseCargoType;
            res.RemarksCN = info.RemarksCN;
            res.SaveByID = info.SaveByID;
            res.SCACCode = info.SCACCode;
            res.SCNO = info.SCNO;
            res.Shipper = info.Shipper;
            res.DeliveryTerm = info.DeliveryTerm;
            res.ReleaseCargoType = info.ReleaseCargoType;
            res.HSCode = info.HSCode;
            res.UpdateDate = info.UpdateDate;
            return res;
        }

        public void RefreshUI(CSCLBookingSaveRequest cscl)
        {
            CSCLBookingInfo c = new CSCLBookingInfo();
            c.BookingNO = cscl.BookingNO;
            c.BookingRemarksCN = cscl.BookingRemarksCN;
            c.CargoDescUS = cscl.CargoDescUS;
            c.Consignee = cscl.Consignee;
            c.DeliveryTerm = cscl.DeliveryTerm;
            c.HBLNO = cscl.HBLNO;
            c.HSCode = cscl.HSCode;
            c.ID = cscl.ID;
            c.IsDirty = false;
            c.Marks = cscl.Marks;
            c.Notify = cscl.Notify;
            c.OceanBookingID = cscl.OceanBookingID;
            c.RealConsignee = cscl.RealConsignee;
            c.RealNotify = cscl.RealNotify;
            c.RealShipper = cscl.RealShipper;
            c.ReleaseCargoType = cscl.ReleaseCargoType;
            c.RemarksCN = cscl.RemarksCN;
            c.SCACCode = cscl.SCACCode;
            c.SCNO = cscl.SCNO;
            c.Shipper = cscl.Shipper;
            c.UpdateDate = cscl.UpdateDate;

            SetSource(c);
            this.AfterSaved();
        }

        #endregion

        private void chkAMS_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAMS.Checked)
                groupControl1.Visible = true;
            else
                groupControl1.Visible = false;
        }

        public string CompanyAddress { get; set; }

        private void cmbBLTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CompanyAddress))
            {
                EDIConfigItem config = EDIService.GetEDIConfig(LocalData.UserInfo.DefaultCompanyID, "CSCL_Booking_NorthChina", new Guid("69B85E12-6208-432C-8D8E-D2E345239047"));
                if (config != null)
                    CompanyAddress = config.ComapnyAddress;
            }
            csclBookingInfo.Shipper = txtShipper.Text = cmbBLTitle.Text + System.Environment.NewLine + CompanyAddress;
            List<LocationList> locationList = GeographyService.GetLocationList(BookingInfo.PODName, null, null, true, null, null, true, 100);
            LocationList res = locationList.Find(delegate(LocationList l) { return l.EName == BookingInfo.PODName; });
            if (cmbBLTitle.SelectedItem.ToString() == "CITY OCEAN LOGISTICS CO.,LTD.")
            {
                //卸货港 美：CTYO 加：8F6P
                if (res.CountryProvinceName.Trim() == "加拿大")
                    csclBookingInfo.SCACCode= txtSCACCode.Text = "8F6P";
                else if (res.CountryProvinceName.Trim() == "美国")
                    csclBookingInfo.SCACCode = txtSCACCode.Text = "CTYO";
            }
            else
            {
                //美：TPHJ 加：8F6N
                if (res.CountryProvinceName.Trim() == "加拿大")
                    csclBookingInfo.SCACCode = txtSCACCode.Text = "8F6N";
                else if (res.CountryProvinceName.Trim() == "美国")
                    csclBookingInfo.SCACCode = txtSCACCode.Text = "TPHJ";
            }
        }

    }
}
