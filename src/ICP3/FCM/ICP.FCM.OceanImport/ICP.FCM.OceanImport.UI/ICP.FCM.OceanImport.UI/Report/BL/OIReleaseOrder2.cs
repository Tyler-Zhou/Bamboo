using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Common.UI.ReportView;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanImport.UI.Report
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OIReleaseOrder2 : CompositeReportViewPart
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

        public IDataFinderFactory DataFinderFactory
        {
            get
            {
                return ServiceClient.GetClientService<IDataFinderFactory>();
            }
        }

        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
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

        #region 本地变量

        Guid _VietnamCompanyId = new Guid("5a827adf-38c7-4a2f-99a7-ad717ce91718");//越南 公司
        Guid ausCompanyID = new Guid("82D15564-0786-E211-BCDE-0026551CA87B");//澳大利亚公司
        #endregion

        public OIReleaseOrder2()
        {
            InitializeComponent();

            if (!LocalData.IsDesignMode)
            {
                this.Disposed += delegate
                {
                    _currentBusinessData = null;
                    if (this.WorkItem != null)
                    {
                        this.WorkItem.Items.Remove(this);
                        this.WorkItem = null;
                    }
                };
            }
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
            IDataFinder customerFinder = DataFinderFactory.GetDataFinder(CommonFinderConstants.CustoemrFinder);
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
                bool isVietnamCompany = LocalData.UserInfo.DefaultCompanyID == _VietnamCompanyId ? true : false;
                string blanks = "  ";
                ReleaseOrderReportData releaseOrderReportData = OIReportDataService.GetReleaseOrderInfoByBusinessID(_currentBusinessData.ID);
                string Remark = string.Empty;
                int RowCount = releaseOrderReportData.ContainerInfos.Count;
                if (RowCount != 0 && RowCount < 5)
                {
                    for (int i = 0; i < RowCount - 1; i++)
                    {
                        string quantityString = string.Empty;
                        if (isVietnamCompany)
                        {
                            quantityString += (string.IsNullOrEmpty(releaseOrderReportData.ContainerInfos[i].Quantity) ? string.Empty : releaseOrderReportData.ContainerInfos[i].Quantity + blanks)
                                + (string.IsNullOrEmpty(releaseOrderReportData.ContainerInfos[i].Weight) ? string.Empty : releaseOrderReportData.ContainerInfos[i].Weight + blanks);
                        }

                        Remark += releaseOrderReportData.ContainerInfos[i].ContainerNo + blanks
                          + releaseOrderReportData.ContainerInfos[i].Size + releaseOrderReportData.ContainerInfos[i].Type + blanks
                            + releaseOrderReportData.ContainerInfos[i].SealNo + blanks + quantityString + "\r\n";
                    }

                    Remark += releaseOrderReportData.ContainerInfos[RowCount - 1].ContainerNo.ToString()
                        + blanks
                        + releaseOrderReportData.ContainerInfos[RowCount - 1].Size.ToString()
                        + releaseOrderReportData.ContainerInfos[RowCount - 1].Type.ToString()
                        + blanks
                        + releaseOrderReportData.ContainerInfos[RowCount - 1].SealNo.ToString()
                        + blanks
                        + ((isVietnamCompany && !string.IsNullOrEmpty(releaseOrderReportData.ContainerInfos[RowCount - 1].Quantity)) ? releaseOrderReportData.ContainerInfos[RowCount - 1].Quantity.ToString() + blanks : string.Empty)
                        + ((isVietnamCompany && !string.IsNullOrEmpty(releaseOrderReportData.ContainerInfos[RowCount - 1].Weight)) ? releaseOrderReportData.ContainerInfos[RowCount - 1].Weight.ToString() + blanks : string.Empty)
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
                if (isVietnamCompany)
                {
                    releaseOrderReportData.ReleaseOrderData.NoOfPackages += "\r\n" + releaseOrderReportData.ReleaseOrderData.Weight;
                }

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
                else if (LocalData.UserInfo.DefaultCompanyID == ausCompanyID)
                {
                    reportFilePath += "RptOIReleaseOrderForAUS.frx";
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
          
                if (_message == null)
                {
                    CustomerInfo customerinfo = GetCustomerInfo(_currentBusinessData.ID);

                    if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
                    {
                        dicData.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
                    }

                    if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
                    {
                        dicData.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
                    }
                    reportViewer.BindData(reportFilePath, dicData, null, GetOperationInfo());
                }
                else
                {
                   
                    reportViewer.BindData(reportFilePath, dicData, null, _message);
                }

            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }

        }

        #endregion
        #region IPart 成员
        OceanBusinessList _currentBusinessData = null;
        Message.ServiceInterface.Message _message = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            string key = "OceanBusinessList";
            string keymessage = "Message";
            if (values.ContainsKey(key))
            {
                _currentBusinessData = values[key] as OceanBusinessList;
            }

            if (values.ContainsKey(keymessage))
            {
                _message = values[keymessage] as Message.ServiceInterface.Message;
            }
        }
        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (_currentBusinessData == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties = new ICP.Message.ServiceInterface.MessageUserPropertiesObject();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanImport;
            message.UserProperties.OperationId = _currentBusinessData.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = _currentBusinessData.ID;
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
}
