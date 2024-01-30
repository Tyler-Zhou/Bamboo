using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Common.UI.ReportView;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Sys.ServiceInterface.DataObjects;
using System.IO;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.Message.ServiceInterface;

namespace ICP.FCM.AirImport.UI.Report
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OIArrivalNotice2 : CompositeReportViewPart
    {
        #region Service

        [ServiceDependency]
        public IAIReportDataService OIReportSrvice { get; set; }
        [ServiceDependency]
        public ICP.Message.ServiceInterface.IClientMessageService EMailService { get; set; }

        [ServiceDependency]
        public IAirImportService oiService { get; set; }
        #endregion

        public OIArrivalNotice2()
        {
            InitializeComponent();
        }
        protected override void LoadData()
        {
            rdoArrivalNotice.SelectedIndex = 0;
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<ArrivalNoticeTitle>> titles = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<ArrivalNoticeTitle>(LocalData.IsEnglish);
            foreach (var item in titles)
            {
                cboReportTitle.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            this.cboReportTitle.SelectedIndex = 0;
            this.rdoArrivalNotice.SelectedIndexChanged += new EventHandler(rdoArrivalNotice_SelectedIndexChanged);
        }
        #region 事件

        private void rdoArrivalNotice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoArrivalNotice.SelectedIndex == 0)
            {
                this.cboReportTitle.Properties.Items.Clear();
                List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<ArrivalNoticeTitle>> titles = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<ArrivalNoticeTitle>(LocalData.IsEnglish);
                foreach (var item in titles)
                {
                    cboReportTitle.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
                }

                this.cboReportTitle.SelectedIndex = 0;
            }
            else
            {
                this.cboReportTitle.Properties.Items.Clear();
                List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<NDArrivalNoticeTitle>> titles = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<NDArrivalNoticeTitle>(LocalData.IsEnglish);
                foreach (var item in titles)
                {
                    cboReportTitle.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
                }

                this.cboReportTitle.SelectedIndex = 0;
            }
        }
        #endregion
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
        protected override void AferSendEmail()
        {
            SendNotice();
        }
        protected override void AfterSendFax()
        {
            SendNotice();
        }
        void SendNotice()
        {
            if (_currentBusinessInfo.IsSentAN == true) return;
            try
            {
                SingleResultData resultData = OIReportSrvice.ArrivalNoticeSent(_currentBusinessData.ID, LocalData.UserInfo.LoginID);
                SendArrivalNoticeMailToSales("Email");
                _currentBusinessInfo.IsSentAN = true;
                _currentBusinessInfo.UpdateDate = resultData.UpdateDate;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }
        #region Print

        private void Print2ArrivalNotice()
        {
            ArrivalNoticeReportData arrivalNoticeReportData = OIReportSrvice.GetArrivalNoticeReportData(_currentBusinessData.ID);
            ArrivalNotice2ReportData arrivalNotice2ReportData = new ArrivalNotice2ReportData();
            arrivalNotice2ReportData.ArrivalNoticeData = arrivalNoticeReportData.ArrivalNoticeData;

            #region build parms

            string ContainerSizeInfo = string.Empty;
            if (ContainerSizeInfo != string.Empty)
            {
                arrivalNotice2ReportData.ArrivalNoticeData.NoOfPackages = arrivalNotice2ReportData.ArrivalNoticeData.NoOfPackages + "\r\n" + ContainerSizeInfo;
            }

            arrivalNotice2ReportData.ArrivalNoticeData.Title = this.cboReportTitle.Text.ToString();
            arrivalNotice2ReportData.ArrivalNoticeData.CurrentDate = DateTime.Now.ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            string Show = string.Empty;
            //pearl
            //if (arrivalNotice2ReportData.ArrivalNoticeContainers.Count > 10)
            //{
            //    arrivalNotice2ReportData.ArrivalNoticeData.Show = "true";
            //}
            //else
            //{
            //    arrivalNotice2ReportData.ArrivalNoticeData.Show = "false";
            //}

            List<ArrivalNoticeData> ArrivalNoticeDatas = new List<ArrivalNoticeData>();
            ArrivalNoticeDatas.Add(arrivalNotice2ReportData.ArrivalNoticeData);

            #endregion

            string reportFilePath = System.Windows.Forms.Application.StartupPath + "\\Reports\\AirImport\\" + "RptAIArrivalNotice_2th.frx";
            BindingSource bs = new BindingSource();
            bs.DataSource = ArrivalNoticeDatas;


            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("AGTArrivalNotice_ArrivalNotice", bs);
            reportViewer.BindData(reportFilePath, reportSource, null);

        }

        private void PrintArrivalNotice()
        {
            ArrivalNoticeReportData arrivalNoticeReportData = OIReportSrvice.GetArrivalNoticeReportData(_currentBusinessData.ID);
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
            string InvoiceNo = string.Empty;
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
            arrivalNoticeReportData.ArrivalNoticeData.CurrentDate = DateTime.Now.ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            if (isTopShipping)
            {
                arrivalNoticeReportData.ArrivalNoticeData.companyName = this.cboReportTitle.Text;
            }

            foreach (ArrivalNoticeFee fee in arrivalNoticeReportData.ArrivalNoticeFees)
            {
                if (!InvoiceNo.Contains(fee.BillNo))
                {
                    InvoiceNo += fee.BillNo + ",";
                }
            }

            InvoiceNo = InvoiceNo.TrimEnd(',');
            string[] InvoiceNoArray = InvoiceNo.Split(',');
            for (int i = 0; i < InvoiceNoArray.Length; i++)
            {
                for (int j = i + 1; j < InvoiceNoArray.Length; j++)
                {
                    if (InvoiceNoArray[i] != string.Empty && InvoiceNoArray[i] == InvoiceNoArray[j])
                    {
                        InvoiceNoArray[j] = string.Empty;
                    }
                }
            }

            InvoiceNo = string.Empty;
            foreach (string EachNO in InvoiceNoArray)
            {
                if (EachNO != string.Empty)
                {
                    InvoiceNo += EachNO + ",";
                }
            }

            InvoiceNo = InvoiceNo.TrimEnd(',');
            arrivalNoticeReportData.ArrivalNoticeData.InvoiceNo = InvoiceNo;
            arrivalNoticeReportData.ArrivalNoticeData.Marks = "\r\n" + arrivalNoticeReportData.ArrivalNoticeData.Marks;

            List<ArrivalNoticeData> ArrivalNoticeDatas = new List<ArrivalNoticeData>();
            ArrivalNoticeDatas.Add(arrivalNoticeReportData.ArrivalNoticeData);
            arrivalNoticeReportData.ArrivalNoticeData.InvoiceNo = InvoiceNo;
            arrivalNoticeReportData.ArrivalNoticeData.SumAmount = SumAmount;
            arrivalNoticeReportData.ArrivalNoticeData.IsEnghish = LocalData.IsEnglish.ToString();

            string reportFilePath = System.Windows.Forms.Application.StartupPath + "\\Reports\\AirImport\\" + "RptAIArrivalNotice.frx";
            BindingSource bs = new BindingSource();
            bs.DataSource = ArrivalNoticeDatas;
            BindingSource bsFeeAmounts = new BindingSource();
            bsFeeAmounts.DataSource = arrivalNoticeReportData.ArrivalNoticeFeeAmounts;

            Dictionary<string, object> dicData = new Dictionary<string, object>();
            dicData.Add("AGTArrivalNotice_ArrivalNotice", bs);
            dicData.Add("AGTArrivalNotice_ArrivalNoticeFeeAmount", bsFeeAmounts);


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
                reportViewer.BindData(reportFilePath, dicData, null, null);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        #endregion
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
                    SingleResultData resultData = OIReportSrvice.ArrivalNoticeSent(_currentBusinessData.ID, LocalData.UserInfo.LoginID);
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
        #region 私有方法


        private void SendArrivalNoticeMailToSales(string mode)
        {
            if (_currentBusinessInfo == null || _currentBusinessInfo.SalesID == null || _currentBusinessInfo.SalesID == Guid.Empty)
            {
                return;
            }

            try
            {
                UserInfo userInfo = UserService.GetUserInfo(_currentBusinessInfo.SalesID.Value);
                //////string email = userInfo == null ? string.Empty : userInfo.EMail;
                string email = "pearlluo@cityAir.com";
                if (!string.IsNullOrEmpty(email))
                {
                    string subject = (LocalData.IsEnglish ? "ArrivalNotice " : "到港通知 ") + "HBL NO:" + _currentBusinessData.SubNo;
                    System.Text.StringBuilder body = new System.Text.StringBuilder();
                    body.Append("Dear " + (LocalData.IsEnglish ? userInfo.EName : userInfo.CName) + "<br>");
                    body.Append("Arrival Notice: " + _currentBusinessData.SubNo + "<br>");
                    body.Append("Sent Mode: " + mode + "<br>");
                    body.Append("Sent Date: " + DateTime.Now.ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "<br>");
                    body.Append("Operator: " + _currentBusinessData.FilerName + "<br>");


                    //FileStream fs = new FileStream(reportViewer.ExportFilePath, FileMode.Open, FileAccess.Read);
                    //    byte[] infbytes = new byte[(int)fs.Length];
                    //    fs.Read(infbytes, 0, infbytes.Length);
                    //    fs.Close();

                    //ICP.OA.ServiceInterface.DataObjects.MailAttachmentInfo m = new ICP.OA.ServiceInterface.DataObjects.MailAttachmentInfo();
                    //m.Attachment = infbytes;
                    //m.AttachmentName = "attachment1.PDF";
                    //m.Size = infbytes.Length;

                    string attachmentName = "attachment1.PDF";

                    ICP.Message.ServiceInterface.Message message = GetOperationInfo();
                    message.SendTo = email;
                    message.Subject = subject;
                    message.Body = body.ToString();
                    message.BodyFormat = BodyFormat.olFormatHTML;
                    message.HasAttachment = true;
                    message.Attachments.Add(new AttachmentContent { ClientPath = reportViewer.ExportFilePath, DisplayName = attachmentName, Name = attachmentName });

                    EMailService.SendAndSaveLog(message);

                    //this.EMailService.SendEMail(LocalData.UserInfo.LoginID, new string[] { email }, new string[] { }
                    //                            , subject
                    //                            , System.Text.UnicodeEncoding.GetEncoding("GB2312").GetBytes(body.ToString())
                    //                            , null
                    //                            , ICP.OA.ServiceInterface.DataObjects.MailPriority.Normal
                    //                            , new ICP.OA.ServiceInterface.DataObjects.MailAttachmentInfo[] { m }); 

                    ////string[] toUsers = new string[1];
                    ////toUsers[0] = email;
                    ////this.EMailService.SendEMail(LocalData.UserInfo.LoginID, toUsers, null, subject, null, System.Text.UnicodeEncoding.GetEncoding("GB2312").GetBytes(body.ToString()), MailPriority.High, null);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        #endregion
        #region IPart 成员

        AirBusinessList _currentBusinessData = null;
        AirBusinessInfo _currentBusinessInfo = null;
        public override void Init(IDictionary<string, object> values)
        {

            _currentBusinessData = ICP.FCM.Common.UI.Utility.GetValue("AirBusinessList", values) as AirBusinessList;
            if (_currentBusinessData != null)
            {
                _currentBusinessInfo = oiService.GetBusinessInfo(_currentBusinessData.ID);
            }
        }

        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.AirImport;
            message.UserProperties.OperationId = _currentBusinessInfo.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = _currentBusinessInfo.ID;

            return message;
        }
        #endregion


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
