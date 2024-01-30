using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.Common.UI.ReportView;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.AirImport.UI.Report
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OIBLPrintPart2 : CompositeReportViewPart
    {
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IAIReportDataService AIReportDataService
        {
            get
            {
                return ServiceClient.GetService<IAIReportDataService>();
            }
        }
        public IAirImportService AirImportService
        {
            get
            {
                return ServiceClient.GetService<IAirImportService>();
            }
        }

        public OIBLPrintPart2()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this._currentAirBusinessList = null;
                
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;

                }
            
            };
        }
        protected override void Locale()
        {
            base.Locale();
            labHBLNo.Text = "HBL提单号";
        }
        protected override void LoadData()
        {
            AirBusinessInfo businessInfo = AirImportService.GetBusinessInfoByEdit(_currentAirBusinessList.ID);
            foreach (var item in businessInfo.HBLList)
            {
              cmbHBLNo.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.HBLNo, item.ID));
            }

            cmbHBLNo.SelectedIndex = 0;      
         
        }
        protected override void Query()
        {
            if (cmbHBLNo.EditValue == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "" : "没有提单，不能打印!", LocalData.IsEnglish ? "Tip" : "提示");
                return;
            }

            AIArrivalNoticeReportData arrivalNoticeReportData = AIReportDataService.GetAuthorityToMakeEntry((Guid)cmbHBLNo.EditValue);
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

            string Description = string.Empty;
            string InvoiceNo = string.Empty;
            string PAmount = string.Empty;
            string CAmount = string.Empty;
            decimal SumAmount = 0;

            arrivalNoticeReportData.ArrivalNoticeData.Show = "false";
            arrivalNoticeReportData.ArrivalNoticeData.ShowFeeAttachement = "false";
            arrivalNoticeReportData.ArrivalNoticeData.SeeAttachementFee = string.Empty;

            //if (this.cboReportTitle.Text.ToString().Contains("INVOICE") == true)
            //{
            //    arrivalNoticeReportData.ArrivalNoticeData.Show = "true";
            //}
            //else
            //{
            //    arrivalNoticeReportData.ArrivalNoticeData.Show = "false";
            //}

            //if (arrivalNoticeReportData.ArrivalNoticeFees.Count > 10)
            //{
            //    arrivalNoticeReportData.ArrivalNoticeData.ShowFeeAttachement = "true";
            //    arrivalNoticeReportData.ArrivalNoticeData.SeeAttachementFee = "See attachement";
            //}

            //if (arrivalNoticeReportData.ArrivalNoticeFees.Count == 0)
            //{
            //    arrivalNoticeReportData.ArrivalNoticeData.ShowNoRows = "true";
            //}
            //else
            //{
            //    arrivalNoticeReportData.ArrivalNoticeData.ShowNoRows = "false";
            //}

            //bool isTopShipping = false;
            //string topShippingTitle = "ARRIVAL NOTICE/FREIGHT INVOICE";
            //if (this.cboReportTitle.Text.Contains("SHIPPING"))
            //{
            //    isTopShipping = true;
            //    arrivalNoticeReportData.ArrivalNoticeData.Show = "true";
            //}

            //arrivalNoticeReportData.ArrivalNoticeData.Title = isTopShipping ? topShippingTitle : this.cboReportTitle.Text;
            arrivalNoticeReportData.ArrivalNoticeData.CurrentDate = DateTime.Now.ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            //if (isTopShipping)
            //{
            //    arrivalNoticeReportData.ArrivalNoticeData.companyName = this.cboReportTitle.Text;
            //}

            foreach (ArrivalNoticeFee fee in arrivalNoticeReportData.ArrivalNoticeFees)
            {
                if (!InvoiceNo.Contains(fee.BillNo))
                {
                    InvoiceNo += fee.BillNo + ",";
                }

                SumAmount += fee.CAmount.Value;
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

            List<AIArrivalNoticeData> ArrivalNoticeDatas = new List<AIArrivalNoticeData>();
            ArrivalNoticeDatas.Add(arrivalNoticeReportData.ArrivalNoticeData);
            arrivalNoticeReportData.ArrivalNoticeData.InvoiceNo = InvoiceNo;
            arrivalNoticeReportData.ArrivalNoticeData.SumAmount = SumAmount.ToString();
            arrivalNoticeReportData.ArrivalNoticeData.IsEnghish = LocalData.IsEnglish.ToString();

            string reportFilePath = System.Windows.Forms.Application.StartupPath + "\\Reports\\AirImport\\" + "RptAuthorityToMakeEntry.frx";
            BindingSource bs = new BindingSource();
            bs.DataSource = ArrivalNoticeDatas;

            BindingSource bsFees = new BindingSource();
            bsFees.DataSource = arrivalNoticeReportData.ArrivalNoticeFees;

            Dictionary<string, object> dicData = new Dictionary<string, object>();
            dicData.Add("AGTArrivalNotice_ArrivalNotice", bs);
            dicData.Add("AGTArrivalNotice_ArrivalNoticeFee",bsFees);
            //this.report1.RegisterData(bsFeeAmounts, "AGTArrivalNotice_ArrivalNoticeFeeAmount");

            //if (arrivalNoticeReportData.ArrivalNoticeFees.Count <= 10)
            //{

           
            //}
            //else
            //{
            //    ArrivalNoticeFee fee = new ArrivalNoticeFee();
            //    List<ArrivalNoticeFee> feelist = new List<ArrivalNoticeFee>();
            //    feelist.Add(fee);

            //    BindingSource bsFees = new BindingSource();
            //    bsFees.DataSource = feelist;
            //    this.report1.RegisterData(bsFees, "AGTArrivalNotice_ArrivalNoticeFee");
            //    BindingSource bsFee3 = new BindingSource();

            //    bsFee3.DataSource = arrivalNoticeReportData.ArrivalNoticeFees;
            //    this.report1.RegisterData(bsFee3, "AGTArrivalNotice_ArrivalNoticeFee3");
            //}

            try
            {
                reportViewer.BindData(reportFilePath, dicData, null, GetOperationInfo());
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }  
        #region IPart 成员

        AirBusinessList _currentAirBusinessList = null;
        public override void Init(IDictionary<string, object> values)
        {  
            _currentAirBusinessList = ICP.FCM.Common.UI.FCMUIUtility.GetValue("AirBusinessList", values) as AirBusinessList;
          
        }


        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (_currentAirBusinessList == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties = new ICP.Message.ServiceInterface.MessageUserPropertiesObject();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.AirImport;
            message.UserProperties.OperationId = _currentAirBusinessList.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = _currentAirBusinessList.ID;

            return message;
        }
        #endregion   
    }
}
