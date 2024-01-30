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
using ICP.Common.ServiceInterface;
using ICP.OA.ServiceInterface.DataObjects;

namespace ICP.FCM.AirImport.UI.Report
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OIReleaseOrder2 : CompositeReportViewPart
    {
        #region Service

        [ServiceDependency]
        public IAIReportDataService OIReportSrvice { get; set; }

        [ServiceDependency]
        public IDataFinderFactory _dataFinderFactory { get; set; }

        #endregion
        public OIReleaseOrder2()
        {
            InitializeComponent();
        }
        private void SearchCustomer(string ctrlName)
        {
            string[] returnFields = { "Id", "EName", "EAddress", "Tel1", "Fax" };
            string searchValue = string.Empty;
            IDataFinder customerFinder = _dataFinderFactory.GetDataFinder(CommonFinderConstants.CustoemrFinder);
            customerFinder.DataChoosed += delegate(object sender, DataFindEventArgs e)
            {
                this.BindSelectData(e.Data, ctrlName);
            };

            customerFinder.PickOne(searchValue, "EName", new SearchConditionCollection(), returnFields, FinderTriggerType.ClickButton, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
        }

        private void BindSelectData(object[] returnValues, string ctrlName)
        {
            if (returnValues != null)
            {
                string info = string.Empty;
                info += returnValues[1].ToString() + "\r\n";
                info += returnValues[2].ToString() + "\r\n";
                info += "TEL:" + returnValues[3].ToString() + "\r\n";
                info += "FAX:" + returnValues[4].ToString();
                if (ctrlName == labSendTo.Name)
                {
                    this.txtSendTo.Text = info;
                }
                else if (ctrlName == labReleaseTo.Name)
                {
                    this.txtReleaseTo.Text = info;

                }
            }
        }
        void labSendTo_Click(object sender, System.EventArgs e)
        {
            Control ctrl = sender as Control;

            SearchCustomer(ctrl.Name);
        }

        void labReleaseTo_Click(object sender, System.EventArgs e)
        {
            Control ctrl = sender as Control;

            SearchCustomer(ctrl.Name);
        }
        protected override void Query()
        {
            try
            {
                ReleaseOrderReportData releaseOrderReportData = OIReportSrvice.GetReleaseOrderInfoByBusinessID(_currentBusinessData.ID);
                //string Remark = string.Empty;
                //int RowCount = releaseOrderReportData.ContainerInfos.Count;
                //if (RowCount != 0 && RowCount < 5)
                //{
                //    for (int i = 0; i < RowCount - 1; i++)
                //    {
                //        Remark += releaseOrderReportData.ContainerInfos[i].ContainerNo + this.Blank(17 - releaseOrderReportData.ContainerInfos[i].ContainerNo.Length)
                //          + releaseOrderReportData.ContainerInfos[i].Size + releaseOrderReportData.ContainerInfos[i].Type + this.Blank(8 - releaseOrderReportData.ContainerInfos[i].Type.Length)
                //            + releaseOrderReportData.ContainerInfos[i].SealNo + this.Blank(8 - releaseOrderReportData.ContainerInfos[i].SealNo.Length) + "\r\n";
                //    }

                //    Remark += releaseOrderReportData.ContainerInfos[RowCount - 1].ContainerNo.ToString()
                //        + this.Blank(17 - releaseOrderReportData.ContainerInfos[RowCount - 1].ContainerNo.ToString().Length)
                //        + releaseOrderReportData.ContainerInfos[RowCount - 1].Size.ToString()
                //        + releaseOrderReportData.ContainerInfos[RowCount - 1].Type.ToString()
                //        + this.Blank(8 - releaseOrderReportData.ContainerInfos[RowCount - 1].Type.ToString().Length)
                //        + releaseOrderReportData.ContainerInfos[RowCount - 1].SealNo.ToString()
                //        + this.Blank(8 - releaseOrderReportData.ContainerInfos[RowCount - 1].SealNo.ToString().Length)
                //        + "\r\n";

                //}
                //else if (RowCount >= 5)
                //{
                //    Remark = "See attachement";
                //}

                //releaseOrderReportData.ReleaseOrderData.Remark = Remark;
                releaseOrderReportData.ReleaseOrderData.CurrentUser = LocalData.UserInfo.LoginName;
                //releaseOrderReportData.ReleaseOrderData.CurrentDate = DateTime.Now.ToShortDateString();
                releaseOrderReportData.ReleaseOrderData.CurrentDate = DateTime.Now.ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);


                releaseOrderReportData.ReleaseOrderData.SpecialInstruction = this.txtSpecialInstructions.Text.Trim();
                if (this.txtSendTo.Text.Trim().Length > 0)
                {
                    releaseOrderReportData.ReleaseOrderData.FinalWareHouseDescription = this.txtSendTo.Text.Trim();
                }

                if (this.txtReleaseTo.Text.Trim().Length > 0)
                {
                    releaseOrderReportData.ReleaseOrderData.ConsigneeDescription = this.txtReleaseTo.Text.Trim();
                }

                List<ReleaseOrderData> ReleaseOrderDatas = new List<ReleaseOrderData>();
                ReleaseOrderDatas.Add(releaseOrderReportData.ReleaseOrderData);
                string reportFilePath = System.Windows.Forms.Application.StartupPath + "\\Reports\\AirImport\\" + "RptAIReleaseOrder.frx";

                BindingSource bs = new BindingSource();
                bs.DataSource = ReleaseOrderDatas;
                Dictionary<string, object> dicData = new Dictionary<string, object>();
                dicData.Add("AGTReleaseOrder_AGTReleaseOrder", bs);
                reportViewer.BindData(reportFilePath, dicData, null, GetOperationInfo());
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
        }
        #region IPart 成员
        AirBusinessList _currentBusinessData = null;
        public override void Init(IDictionary<string, object> values)
        {
            _currentBusinessData = ICP.FCM.Common.UI.Utility.GetValue("AirBusinessList", values) as AirBusinessList;
        }

        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (_currentBusinessData == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.AirImport;
            message.UserProperties.OperationId = _currentBusinessData.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = _currentBusinessData.ID;

            return message;
        }

        #endregion      
    }
}
