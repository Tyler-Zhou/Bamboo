using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Common.UI.ReportView;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Sys.ServiceInterface.DataObjects;
using System.IO;
using System.Linq;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.Message.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FCM.OceanImport.UI.Report
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OIArrivalNotice2 : CompositeReportViewPart
    {
        #region Service
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IOIReportDataService OIReportDataService
        {
            get
            {
                return ServiceClient.GetService<IOIReportDataService>();
            }
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }

        public IMessageService MessageService
        {
            get
            {
                return ServiceClient.GetService<IMessageService>();
            }
        }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        /// <summary>
        /// 公共客户管理服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        #endregion

        #region 属性

        private Guid _companyID;
        /// <summary>
        /// 当前口岸ID
        /// </summary>
        public Guid Company
        {
            get
            {
                return _companyID;
            }
            set
            {
                _companyID = value;
            }
        }

        #endregion

        public OIArrivalNotice2()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                this.Disposed += delegate
                {
                    _arrivalNoticeTitle = null;
                    _currentBusinessData = null;
                    _currentBusinessInfo = null;
                    _ndArrivalNoticeTitle = null;
                    this.rdoArrivalNotice.SelectedIndexChanged -= this.rdoArrivalNotice_SelectedIndexChanged;
                    if (this.WorkItem != null)
                    {
                        this.WorkItem.Items.Remove(this);
                        this.WorkItem = null;
                    }
                };
            }
        }

        static List<string> _arrivalNoticeTitle;
        static List<string> _ndArrivalNoticeTitle;

        private void InitArrivalNoticeTitleData()
        {
            if (_arrivalNoticeTitle == null || _arrivalNoticeTitle.Count <= 0)
            {
                _arrivalNoticeTitle = new List<string>();
                _arrivalNoticeTitle.Add("ARRIVAL NOTICE/FREIGHT INVOICE");
                _arrivalNoticeTitle.Add("ARRIVAL NOTICE");
                _arrivalNoticeTitle.Add("REVISED ARRIVAL NOTICE");
                _arrivalNoticeTitle.Add("REVISED ARRIVAL NOTICE/FREIGHT INVOICE");
                _arrivalNoticeTitle.Add("I.T.NOTIFICATION");
                _arrivalNoticeTitle.Add("ARRIVAL CONFIRMATION NOTIC");
                _arrivalNoticeTitle.Add("GENERAL ORDER NOTICE");
                _arrivalNoticeTitle.Add("TOP SHIPPING LOGISTICS CO., LTD");
            }

        }
        private void InitSecondArrivalNoticeTitleData()
        {
            if (_ndArrivalNoticeTitle == null || _ndArrivalNoticeTitle.Count <= 0)
            {
                _ndArrivalNoticeTitle = new List<string>();
                _ndArrivalNoticeTitle.Add("AVAILABILITY NOTICE");
                _ndArrivalNoticeTitle.Add("PICK UP NUMBER NOTICE");
            }
        }
        protected override void LoadData()
        {
            InitArrivalNoticeTitleData();
            InitSecondArrivalNoticeTitleData();
            InitHBLNo();

            rdoArrivalNotice.SelectedIndex = 0;
            rdoGrpTornoto.SelectedIndex = 0;
            foreach (string item in _arrivalNoticeTitle)
            {
                cboReportTitle.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item));
            }
            this.cboReportTitle.SelectedIndex = 0;
            this.rdoArrivalNotice.SelectedIndexChanged += new EventHandler(rdoArrivalNotice_SelectedIndexChanged);
        }
        /// <summary>
        /// 初始化HBLNo
        /// </summary>
        private void InitHBLNo()
        {
            List<OceanBusinessHBLList> hblList = OceanImportService.GetOIBookingHBLList(_currentBusinessInfo.ID);
            this.cmbHBLNo.Properties.Items.Clear();
            this.cmbHBLNo.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", Guid.Empty));
            foreach (OceanBusinessHBLList item in hblList)
            {
                this.cmbHBLNo.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.HBLNo, item.ID));
            }

        }

        private void rdoArrivalNotice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoArrivalNotice.SelectedIndex == 0)
            {
                this.cboReportTitle.Properties.Items.Clear();

                foreach (var item in _arrivalNoticeTitle)
                {
                    cboReportTitle.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item));
                }

                this.cboReportTitle.SelectedIndex = 0;
            }
            else
            {
                this.cboReportTitle.Properties.Items.Clear();

                foreach (var item in _ndArrivalNoticeTitle)
                {
                    cboReportTitle.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item));
                }

                this.cboReportTitle.SelectedIndex = 0;
            }
        }

        public override object DataSource
        {
            get { return _companyID; }
            set { BindingData(value); }
        }


        public void BindingData(object data)
        {
            if (data == null)
            {
                rdoGrpTornoto.Visible = false;
                return;
            }
            _companyID = (Guid)data;
            if (_companyID != null && _companyID.ToString().Equals("4E0AF31B-C70E-44EE-B2FF-DDC91A6BC75B", StringComparison.OrdinalIgnoreCase))
            {
                rdoGrpTornoto.Visible = true;
            }
            else
            {
                rdoGrpTornoto.Visible = false;
            }
        }

        protected override void Query()
        {

            #region 到港通知书

            if (rdoArrivalNotice.SelectedIndex == 0)
            {
                PrintArrivalNotice();
            }
            else if (rdoArrivalNotice.SelectedIndex == 1)
            {
                Print2ArrivalNotice();
            }

            #endregion
        }

        public Guid? HBLID
        {
            get
            {
                if (this.cmbHBLNo.EditValue != null && ((Guid)this.cmbHBLNo.EditValue) != Guid.Empty)
                {
                    return (Guid)this.cmbHBLNo.EditValue;
                }
                else
                {
                    return null;
                }

            }
        }

        private void Print2ArrivalNotice()
        {
            ArrivalNoticeReportData arrivalNoticeReportData = OIReportDataService.GetArrivalNoticeReportData(_currentBusinessData.ID, HBLID);
            ArrivalNotice2ReportData arrivalNotice2ReportData = new ArrivalNotice2ReportData();
            arrivalNotice2ReportData.ArrivalNoticeContainers = arrivalNoticeReportData.ContainerList;
            arrivalNotice2ReportData.ArrivalNoticeData = arrivalNoticeReportData.ArrivalNoticeData;

            #region build parms

            string ContainerSizeInfo = string.Empty;
            if (ContainerSizeInfo != string.Empty)
            {
                arrivalNotice2ReportData.ArrivalNoticeData.NoOfPackages = arrivalNotice2ReportData.ArrivalNoticeData.NoOfPackages + "\r\n" + ContainerSizeInfo;
            }

            arrivalNotice2ReportData.ArrivalNoticeData.Title = this.cboReportTitle.Text.ToString();
            arrivalNotice2ReportData.ArrivalNoticeData.CurrentDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            string Show = string.Empty;
            if (arrivalNotice2ReportData.ArrivalNoticeContainers.Count > 10)
            {
                arrivalNotice2ReportData.ArrivalNoticeData.Show = "true";
            }
            else
            {
                arrivalNotice2ReportData.ArrivalNoticeData.Show = "false";
            }

            List<ArrivalNoticeData> ArrivalNoticeDatas = new List<ArrivalNoticeData>();
            if (rdoGrpTornoto.Visible == true)
            {
                if (rdoGrpTornoto.SelectedIndex == 0)
                {
                    arrivalNotice2ReportData.ArrivalNoticeData.companyName = "Harvest Logistic Corporation Toronto Office";
                    arrivalNotice2ReportData.ArrivalNoticeData.companyAddress = "Rm22, 201-70 East Beaver Creek Rd, Richmond Hill, ON, L4B 1B3";
                    arrivalNotice2ReportData.ArrivalNoticeData.CompanyTelFax = "Tel: (905)-597-0282, Fax: (905)-597-0284";
                }
                else
                {
                    arrivalNotice2ReportData.ArrivalNoticeData.companyName = "Harvest Logistic Corporation";
                    arrivalNotice2ReportData.ArrivalNoticeData.companyAddress = "621-470 Granville Street.Vancouver,B.C.,V6C 1V5";
                    arrivalNotice2ReportData.ArrivalNoticeData.CompanyTelFax = "Tel: 604-687-6372; Fax: 604-687-6305";
                }
            }
            ArrivalNoticeDatas.Add(arrivalNotice2ReportData.ArrivalNoticeData);

            #endregion

            string reportFilePath = System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanImport\\" + "RptOIArrivalNotice_2th.frx";
            BindingSource bs = new BindingSource();
            bs.DataSource = ArrivalNoticeDatas;
            BindingSource bsContainerInfo = new BindingSource();
            bsContainerInfo.DataSource = arrivalNotice2ReportData.ArrivalNoticeContainers;

            Dictionary<string, object> dicData = new Dictionary<string, object>();
            dicData.Add("AGTArrivalNotice_ArrivalNotice", bs);
            dicData.Add("LongWin_Forwarding_ServiceInterface_OceanImport_ContainerInfoReportData", bsContainerInfo);
            dicData.Add(ICP.Common.ServiceInterface.CommonConstants.DocumentNameKey, "ArrivalNotice2");
            dicData.Add(ICP.Common.ServiceInterface.CommonConstants.DocumentTypeKey, DocumentType.AN);

            if (_message == null)
            {
                CustomerInfo customerinfo = GetCustomerInfo(_currentBusinessInfo.ID);
                if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
                {
                    dicData.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
                }

                if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
                {
                    dicData.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
                }
                reportViewer.BindData(reportFilePath, dicData, null, GetOperationInfo(_currentBusinessData.No));
            }
            else
            {
                reportViewer.BindData(reportFilePath, dicData, null, _message);
            }


        }

        private void PrintArrivalNotice()
        {
            ArrivalNoticeReportData arrivalNoticeReportData = OIReportDataService.GetArrivalNoticeReportData(_currentBusinessData.ID, HBLID);

            arrivalNoticeReportData.ArrivalNoticeData.PRemark = string.Empty;
            string reportCode = "OI_AN_REMARK";
            if (_currentBusinessInfo.CompanyID == new Guid("3835F05C-2779-E511-99BA-0026551CA878"))
            {
                reportCode = "OI_AN_JDY_REMARK";
            }
            CompanyReportConfigureList reportConfigure = ConfigureService.GetReportConfigureList(_currentBusinessInfo.CompanyID, reportCode);
            if (reportConfigure != null && reportConfigure.Parameters != null && reportConfigure.Parameters.Count > 0)
            {
                arrivalNoticeReportData.ArrivalNoticeData.PRemark = reportConfigure.Parameters.Find(delegate(ReportParameterList f) { return f.Code.ToUpper() == "REMARK"; }).ParameterValue.ToString();
            }

            List<ContainerInfoReportData> dsContainer = arrivalNoticeReportData.ContainerList;
            if (arrivalNoticeReportData.ArrivalNoticeFees.Count == 0)
            {
                ArrivalNoticeFee arrivalNoticeFee = new ArrivalNoticeFee();
                arrivalNoticeFee.BillNo = string.Empty;
                arrivalNoticeFee.ChargeItemDescription = string.Empty;
                arrivalNoticeFee.PAmount = 0.0M;
                arrivalNoticeFee.CAmount = 0.0M;
                arrivalNoticeFee.EName = string.Empty;
                arrivalNoticeReportData.ArrivalNoticeFees.Add(arrivalNoticeFee);
            }

            if (arrivalNoticeReportData.ArrivalNoticeFeeAmounts.Count == 0)
            {

                ArrivalNoticeFeeAmount arrivalNoticeFeeAmount = new ArrivalNoticeFeeAmount();
                arrivalNoticeFeeAmount.Amount = 0.0M;
                arrivalNoticeFeeAmount.EName = string.Empty;
                arrivalNoticeReportData.ArrivalNoticeFeeAmounts.Add(arrivalNoticeFeeAmount);
            }
            else
            {
                arrivalNoticeReportData.ArrivalNoticeFeeAmounts[0].LabelText = "TOTAL  AMOUNT";
            }

            string Description = string.Empty;

            string PAmount = string.Empty;
            string CAmount = string.Empty;
            string SumAmount = string.Empty;//目前用来存储币种           

            arrivalNoticeReportData.ArrivalNoticeData.Show = "false";
            arrivalNoticeReportData.ArrivalNoticeData.ShowFeeAttachement = "false";
            arrivalNoticeReportData.ArrivalNoticeData.SeeAttachementFee = string.Empty;

            if (this.cboReportTitle.Text.ToString().Contains("INVOICE") == true)
            {
                arrivalNoticeReportData.ArrivalNoticeData.Show = "true";
            }
            else
            {
                arrivalNoticeReportData.ArrivalNoticeData.Show = "false";
            }

            if (arrivalNoticeReportData.ArrivalNoticeFees.Count > 10)
            {
                arrivalNoticeReportData.ArrivalNoticeData.ShowFeeAttachement = "true";
                arrivalNoticeReportData.ArrivalNoticeData.SeeAttachementFee = "See attachement";
            }

            if (arrivalNoticeReportData.ArrivalNoticeFees.Count == 0)
            {
                arrivalNoticeReportData.ArrivalNoticeData.ShowNoRows = "true";
            }
            else
            {
                arrivalNoticeReportData.ArrivalNoticeData.ShowNoRows = "false";
            }

            bool isTopShipping = false;
            string topShippingTitle = "ARRIVAL NOTICE/FREIGHT INVOICE";
            if (this.cboReportTitle.Text.Contains("SHIPPING"))
            {
                isTopShipping = true;
                arrivalNoticeReportData.ArrivalNoticeData.Show = "true";
            }

            arrivalNoticeReportData.ArrivalNoticeData.Title = isTopShipping ? topShippingTitle : this.cboReportTitle.Text;
            arrivalNoticeReportData.ArrivalNoticeData.CurrentDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            if (isTopShipping)
            {
                arrivalNoticeReportData.ArrivalNoticeData.companyName = this.cboReportTitle.Text;
            }

            string distinctInvoiceNos = arrivalNoticeReportData.ArrivalNoticeFees.Select(fee => fee.BillNo).Distinct().Aggregate((element1, element2) => element1 + "," + element1);
            arrivalNoticeReportData.ArrivalNoticeData.InvoiceNo = distinctInvoiceNos;

            string ContainerSizeInfo = string.Empty;

            string ContainerInfo = string.Empty;
            int RowCount = dsContainer.Count;
            if (RowCount < 10)
            {
                foreach (ContainerInfoReportData ContainerInfoData in dsContainer)
                {
                    ContainerInfo += ContainerInfoData.ContainerNo + " ".PadRight(Math.Abs(14 - ContainerInfoData.ContainerNo.Length))
                      + ContainerInfoData.Size + ContainerInfoData.Type + " ".PadRight(5 - ContainerInfoData.Type.Length) + "\r\n"
                      + ContainerInfoData.SealNo + " ".PadRight(Math.Abs(19 - ContainerInfoData.SealNo.Length)) + "\r\n";
                }
            }
            else
            {
                ContainerInfo = "See Attachement";
            }

            //arrivalNoticeReportData.ArrivalNoticeData.Marks = "\r\n" + arrivalNoticeReportData.ArrivalNoticeData.Marks;
            arrivalNoticeReportData.ArrivalNoticeData.Marks = "\r\n" + "N/M";
            arrivalNoticeReportData.ArrivalNoticeData.ContainerInfo = ContainerInfo;
            if (ContainerSizeInfo != string.Empty)
            {
                arrivalNoticeReportData.ArrivalNoticeData.NoOfPackages = arrivalNoticeReportData.ArrivalNoticeData.NoOfPackages + "\r\n" + ContainerSizeInfo;
            }

            List<ArrivalNoticeData> ArrivalNoticeDatas = new List<ArrivalNoticeData>();

            if (rdoGrpTornoto.Visible == true)
            {
                if (rdoGrpTornoto.SelectedIndex == 0)
                {
                    arrivalNoticeReportData.ArrivalNoticeData.companyName = "Harvest Logistic Corporation Toronto Office";
                    arrivalNoticeReportData.ArrivalNoticeData.companyAddress = "Rm22, 201-70 East Beaver Creek Rd, Richmond Hill, ON, L4B 1B3";
                    arrivalNoticeReportData.ArrivalNoticeData.CompanyTelFax = "Tel: (905)-597-0282, Fax: (905)-597-0284";
                }
                else
                {
                    arrivalNoticeReportData.ArrivalNoticeData.companyName = "Harvest Logistic Corporation";
                    arrivalNoticeReportData.ArrivalNoticeData.companyAddress = "621-470 Granville Street.Vancouver,B.C.,V6C 1V5";
                    arrivalNoticeReportData.ArrivalNoticeData.CompanyTelFax = "Tel: 604-687-6372; Fax: 604-687-6305";
                }
            }

            if (_currentBusinessInfo.ETD != null && _currentBusinessInfo.ETD >= Convert.ToDateTime("2015-05-10")
              && ((_currentBusinessInfo.CompanyID.ToString().ToUpper() == "2B109BA9-D770-419D-9323-34EE1553FC2E") || _currentBusinessInfo.CompanyID.ToString().ToUpper() == "3835F05C-2779-E511-99BA-0026551CA878"))
            {
                arrivalNoticeReportData.ArrivalNoticeData.Remark = @"Please allow 24-48 hours for freight release.
Please make sure to report the empty ready time before 4 P.M BY EMAIL to the trukcing company and cc us in. Otherwise, the trucking company will need two more business days to schdule for empty return.
Per diem --- 5 calendar days free in base port, 2 calendar days free in inland ( Applied to all carriers) ;
Chassis ---  $30/calendar day, min 2 days in LA; $35/calendar day, min 4 days in Oak; $30/calendar day, min 3 days in others ;
If Trucking service needed, 2 hours free warehouse waiting, USD75 per hour after 2 hours .";
            }

            ArrivalNoticeDatas.Add(arrivalNoticeReportData.ArrivalNoticeData);
            //ArrivalNoticeDatas.Add(newArrivalNoticeData);

            arrivalNoticeReportData.ArrivalNoticeData.SumAmount = SumAmount;
            arrivalNoticeReportData.ArrivalNoticeData.IsEnghish = LocalData.IsEnglish.ToString();

            //string reportFilePath = System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanImport\\" + "RptOIArrivalNotice.frx";
            string reportFilePath = System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanImport\\";
            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(_currentBusinessInfo.CompanyID);
            if (configureInfo.SolutionID == new Guid("b6e4dded-4359-456a-b835-e8401c910fd0"))
            {
                //国内A4纸
                reportFilePath += "RptOIArrivalNotice.frx";
            }
            else
            {
                //国外letter纸
                reportFilePath += "OIArrivalNoticeLetter.frx";
            }

            BindingSource bs = new BindingSource();
            bs.DataSource = ArrivalNoticeDatas;
            BindingSource bsContainerInfo = new BindingSource();
            bsContainerInfo.DataSource = dsContainer;
            BindingSource bsFeeAmounts = new BindingSource();
            bsFeeAmounts.DataSource = arrivalNoticeReportData.ArrivalNoticeFeeAmounts;

            Dictionary<string, object> dicData = new Dictionary<string, object>();
            dicData.Add("AGTArrivalNotice_ArrivalNotice", ArrivalNoticeDatas);
            dicData.Add("LongWin_Forwarding_ServiceInterface_OceanImport_ContainerInfoReportData", bsContainerInfo);
            dicData.Add("AGTArrivalNotice_ArrivalNoticeFeeAmount", bsFeeAmounts);
            dicData.Add(CommonConstants.DocumentNameKey, "ArrivalNotice1");
            dicData.Add(CommonConstants.DocumentTypeKey, DocumentType.AN);

            if (arrivalNoticeReportData.ArrivalNoticeFees.Count <= 10)
            {
                BindingSource bsFees = new BindingSource();
                bsFees.DataSource = arrivalNoticeReportData.ArrivalNoticeFees;
                dicData.Add("AGTArrivalNotice_ArrivalNoticeFee", bsFees);
            }
            else
            {
                ArrivalNoticeFee fee = new ArrivalNoticeFee();
                List<ArrivalNoticeFee> feelist = new List<ArrivalNoticeFee>();
                feelist.Add(fee);

                BindingSource bsFees = new BindingSource();
                bsFees.DataSource = feelist;

                dicData.Add("AGTArrivalNotice_ArrivalNoticeFee", bsFees);
                BindingSource bsFee3 = new BindingSource();

                bsFee3.DataSource = arrivalNoticeReportData.ArrivalNoticeFees;
                dicData.Add("AGTArrivalNotice_ArrivalNoticeFee3", bsFee3);
            }

            try
            {

                if (_message == null)
                {
                    CustomerInfo customerinfo = GetCustomerInfo(_currentBusinessInfo.ID);

                    if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
                    {
                        dicData.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
                    }

                    if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
                    {
                        dicData.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
                    }
                    reportViewer.BindData(reportFilePath, dicData, null, GetOperationInfo(_currentBusinessData.No));
                }
                else
                {
                    reportViewer.BindData(reportFilePath, dicData, null, _message);
                }

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }
        protected override void AfterSendFax()
        {
            SendEmail("Fax");
        }

        //protected override bool BeforeSendEmail()
        //{
        //    bool cancel = true;
        //    SendEmail("Email");
        //    return cancel;

        //}

        private void SendEmail(string mode)
        {
            if (_currentBusinessInfo.IsSentAN == true)
                return;
            try
            {
                SingleResultData resultData = OIReportDataService.ArrivalNoticeSent(_currentBusinessData.ID, LocalData.UserInfo.LoginID);
                SendArrivalNoticeMailToSales(mode);
                _currentBusinessInfo.IsSentAN = true;
                _currentBusinessInfo.UpdateDate = resultData.UpdateDate;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);

            }
        }


        protected override void AfterPrint()
        {
            if (_currentBusinessInfo.IsSentAN == true) return;

            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure update Arrival Notice?" : "是否更新到港通知?"
                         , LocalData.IsEnglish ? "Tip" : "提示"
                         , MessageBoxButtons.YesNo
                         , MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    SingleResultData resultData = OIReportDataService.ArrivalNoticeSent(_currentBusinessData.ID, LocalData.UserInfo.LoginID);
                    SendArrivalNoticeMailToSales("Print");
                    _currentBusinessInfo.IsSentAN = true;
                    _currentBusinessInfo.UpdateDate = resultData.UpdateDate;
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                }
            }
        }
        private void SendArrivalNoticeMailToSales(string mode)
        {
            if (_currentBusinessInfo.SalesID == null || _currentBusinessInfo.SalesID == Guid.Empty)
            {
                return;
            }

            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    UserInfo userInfo = UserService.GetUserInfo(_currentBusinessInfo.SalesID.Value);
                    string email = userInfo == null ? string.Empty : userInfo.EMail;
                    if (_currentBusinessInfo.POLFilerID != null && _currentBusinessInfo.SalesID.Value != _currentBusinessInfo.POLFilerID.Value)
                    {
                        UserInfo polFileruserInfo = UserService.GetUserInfo(_currentBusinessInfo.POLFilerID.Value);
                        email += polFileruserInfo == null ? string.Empty : ";" + polFileruserInfo.EMail;
                    }

                    if (!string.IsNullOrEmpty(email))
                    {

                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Sending Email to salesman..." : "正在发送邮件给业务员...");
                        string subject = (LocalData.IsEnglish ? "ArrivalNotice " : "到港通知 ") + "HBL NO:" + _currentBusinessData.SubNo;
                        System.Text.StringBuilder body = new System.Text.StringBuilder();
                        body.Append("Dear " + (LocalData.IsEnglish ? userInfo.EName : userInfo.CName) + System.Environment.NewLine);
                        body.Append("Arrival Notice: " + _currentBusinessData.SubNo + System.Environment.NewLine);
                        body.Append("Sent Mode: " + mode + System.Environment.NewLine);
                        body.Append("Sent Date: " + DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).ToString("MM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo) + System.Environment.NewLine);
                        body.Append("Operator: " + _currentBusinessData.FilerName + System.Environment.NewLine);

                        string attachmentName = "attachment1.pdf";
                        ICP.Message.ServiceInterface.Message message = GetOperationInfo(subject);
                        message.SendFrom = LocalData.UserInfo.EmailAddress;
                        message.SendTo = email;
                        message.HasAttachment = true;
                        message.Subject = subject;
                        message.Body = body.ToString();
                        message.BodyFormat = BodyFormat.olFormatHTML;
                        byte[] infbytes = null;
                        using (FileStream fs = new FileStream(reportViewer.ExportFilePath, FileMode.Open, FileAccess.Read))
                        {
                            infbytes = new byte[(int)fs.Length];
                            fs.Read(infbytes, 0, infbytes.Length);
                            fs.Close();
                        }
                        AttachmentContent attachment = new AttachmentContent { DisplayName = attachmentName, Name = attachmentName, Content = infbytes };
                        message.Attachments.Add(attachment);
                        MessageService.Send(message);



                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Email sent sucessfully." : "邮件发送成功");
                    }
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }
        #region IPart 成员

        OceanBusinessList _currentBusinessData = null;
        OceanBusinessInfo _currentBusinessInfo = null;
        ICP.Message.ServiceInterface.Message _message = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "OceanBusinessList")
                {
                    _currentBusinessData = item.Value as OceanBusinessList;
                    _currentBusinessInfo = OceanImportService.GetBusinessInfo(_currentBusinessData.ID);
                }
                if (item.Key == "Message")
                {

                    _message = item.Value as ICP.Message.ServiceInterface.Message;
                }
            }
        }

        private ICP.Message.ServiceInterface.Message GetOperationInfo(string subject)
        {
            if (_currentBusinessData == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            if (_currentBusinessInfo.SalesID != null)
            {
                Guid salesID = (Guid)_currentBusinessInfo.SalesID;
                UserInfo userInfo = ServiceClient.GetService<IUserService>().GetUserInfo(salesID);
                string email = userInfo == null ? string.Empty : userInfo.EMail;
                message.CC = email;
            }
            if (!string.IsNullOrEmpty(subject))
            {
                message.Subject = (LocalData.IsEnglish ? "ArrivalNotice " : "到港通知 ") + "NO:" + subject;
            }
            message.UserProperties = new MessageUserPropertiesObject();
            message.UserProperties.OperationId = _currentBusinessInfo.ID;
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanImport;

            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            return message;
        }
        #endregion

        private CustomerInfo GetCustomerInfo(Guid operationID)
        {
            OceanBusinessInfo info = OceanImportService.GetBusinessInfo(operationID);
            CustomerInfo customerInfo = CustomerService.GetCustomerInfo(info.CustomerID);
            return customerInfo;
        }
    }

    enum ArrivalNoticeTitle
    {
        [MemberDescription("ARRIVAL NOTICE/FREIGHT INVOICE")]
        FREIGHTINVOICE = 0,
        [MemberDescription("ARRIVAL NOTICE")]
        NOTICE,
        [MemberDescription("REVISED ARRIVAL NOTICE")]
        REVISED,
        [MemberDescription("REVISED ARRIVAL NOTICE/FREIGHT INVOICE")]
        REVISEDARRIVAL,
        [MemberDescription("I.T.NOTIFICATION")]
        NOTIFICATION,
        [MemberDescription("ARRIVAL CONFIRMATION NOTIC")]
        NOTIC,
        [MemberDescription("GENERAL ORDER NOTICE")]
        ORDERNOTICE,
        [MemberDescription("TOP SHIPPING LOGISTICS CO., LTD")]
        LTD,
    }

    enum NDArrivalNoticeTitle
    {
        [MemberDescription("AVAILABILITY NOTICE")]
        AVAILABILITY = 0,
        [MemberDescription("PICK UP NUMBER NOTICE")]
        NOTICE,
    }
}
