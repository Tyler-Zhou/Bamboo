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
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.OA.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanImport.UI.Report
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OIReleaseOrder2 : CompositeReportViewPart
    {
        #region Service

        [ServiceDependency]
        public IOIReportDataService OIReportSrvice { get; set; }

        [ServiceDependency]
        public IDataFinderFactory _dataFinderFactory { get; set; }

        #endregion

        #region 本地变量

        Guid _VietnamCompanyId = new Guid("5a827adf-38c7-4a2f-99a7-ad717ce91718");//越南 公司

        #endregion

        public OIReleaseOrder2()
        {
            InitializeComponent();
            if (LocalData.UserInfo.DefaultCompanyID != _VietnamCompanyId)
            {
                this.labMarks.Visible = false;
                this.txtMarks.Visible = false;
            }
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

        private string Blank(int num)
        {
            return " ".PadRight(Math.Abs(num));
            //return " ".PadRight(num);          
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
            Print();
        }
        #region Print

        private void Print()
        {
           

            try
            {
                ReleaseOrderReportData releaseOrderReportData = OIReportSrvice.GetReleaseOrderInfoByBusinessID(_currentBusinessData.ID);
                string Remark = string.Empty;
                int RowCount = releaseOrderReportData.ContainerInfos.Count;
                if (RowCount != 0 && RowCount < 5)
                {
                    for (int i = 0; i < RowCount - 1; i++)
                    {
                        Remark += releaseOrderReportData.ContainerInfos[i].ContainerNo + this.Blank(17 - releaseOrderReportData.ContainerInfos[i].ContainerNo.Length)
                          + releaseOrderReportData.ContainerInfos[i].Size + releaseOrderReportData.ContainerInfos[i].Type + this.Blank(8 - releaseOrderReportData.ContainerInfos[i].Type.Length)
                            + releaseOrderReportData.ContainerInfos[i].SealNo + this.Blank(8 - releaseOrderReportData.ContainerInfos[i].SealNo.Length) + "\r\n";
                            //+ releaseOrderReportData.ContainerInfos[i].Quantity + this.Blank(8 - releaseOrderReportData.ContainerInfos[i].Quantity.Length) + "\r\n";
                    }

                    Remark += releaseOrderReportData.ContainerInfos[RowCount - 1].ContainerNo.ToString()
                        + this.Blank(17 - releaseOrderReportData.ContainerInfos[RowCount - 1].ContainerNo.ToString().Length)
                        + releaseOrderReportData.ContainerInfos[RowCount - 1].Size.ToString()
                        + releaseOrderReportData.ContainerInfos[RowCount - 1].Type.ToString()
                        + this.Blank(8 - releaseOrderReportData.ContainerInfos[RowCount - 1].Type.ToString().Length)
                        + releaseOrderReportData.ContainerInfos[RowCount - 1].SealNo.ToString()
                        + this.Blank(8 - releaseOrderReportData.ContainerInfos[RowCount - 1].SealNo.ToString().Length)
                        //+ releaseOrderReportData.ContainerInfos[RowCount - 1].Quantity.ToString()
                        //+ this.Blank(8 - releaseOrderReportData.ContainerInfos[RowCount - 1].Quantity.ToString().Length)
                        + "\r\n";

                }
                else if (RowCount >= 5)
                {
                    Remark = "See attachement";
                }

                releaseOrderReportData.ReleaseOrderData.Remark = Remark;
                releaseOrderReportData.ReleaseOrderData.CurrentUser = LocalData.UserInfo.LoginName;
                //releaseOrderReportData.ReleaseOrderData.CurrentDate = DateTime.Now.ToShortDateString();
                releaseOrderReportData.ReleaseOrderData.CurrentDate = DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);


                releaseOrderReportData.ReleaseOrderData.SpecialInstruction = this.txtSpecialInstructions.Text.Trim();
                releaseOrderReportData.ReleaseOrderData.Marks = this.txtMarks.Text.Trim();
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

                string reportFilePath = System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanImport\\";
                if (LocalData.UserInfo.DefaultCompanyID == _VietnamCompanyId)
                {
                    reportFilePath += "RptOIReleaseOrderForVietnam.frx";
                }
                else
                {
                    reportFilePath += "RptOIReleaseOrder.frx";
                }

                BindingSource bs = new BindingSource();
                bs.DataSource = ReleaseOrderDatas;
                BindingSource bsContainerInfo = new BindingSource();
                bsContainerInfo.DataSource = releaseOrderReportData.ContainerInfos;

                Dictionary<string, object> dicData = new Dictionary<string, object>();
                dicData.Add("AGTReleaseOrder_AGTReleaseOrder", bs);
                dicData.Add("LongWin_Forwarding_ServiceInterface_OceanImport_ContainerInfoReportData", bsContainerInfo);
                reportViewer.BindData(reportFilePath, dicData, null, GetOperationInfo());
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
            
        }

        #endregion
        #region IPart 成员
        OceanBusinessList _currentBusinessData = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            string key="OceanBusinessList";
            if (values.ContainsKey(key))

            {
                _currentBusinessData = values[key] as OceanBusinessList;
            }
        }
        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (_currentBusinessData == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanImport;
            message.UserProperties.OperationId = _currentBusinessData.ID;
           message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = _currentBusinessData.ID;
            return message;
        }
        #endregion   
    }
}
