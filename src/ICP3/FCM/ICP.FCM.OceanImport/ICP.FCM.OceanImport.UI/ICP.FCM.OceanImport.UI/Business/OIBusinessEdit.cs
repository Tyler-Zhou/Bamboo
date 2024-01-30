using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using ICP.Business.Common.ServiceInterface;
using ICP.Business.Common.UI;
using ICP.Business.Common.UI.Common;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.DataCache.ServiceInterface;
using ICP.FAM.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FCM.OceanImport.UI.Common;
using ICP.FCM.OceanImport.UI.Common.Controls;
using ICP.FCM.OceanImport.UI.Report;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ActionType = ICP.Common.ServiceInterface.DataObjects.ActionType;

namespace ICP.FCM.OceanImport.UI
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class OIBusinessEdit : BaseEditPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }

        public IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }
        }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }

        public OceanImportPrintHelper OceanImportPrintHelper
        {
            get
            {
                return ClientHelper.Get<OceanImportPrintHelper, OceanImportPrintHelper>();
            }
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }

        }

        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }

        public IClientOceanImportService ClientOceanImportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanImportService>();
            }
        }
        public IOperationAgentService OperationAgentService
        {
            get
            {
                return ServiceClient.GetService<IOperationAgentService>();

            }
        }


        private UCContactAndDocumentPart ucBusinessOtherPart;
        public UCContactAndDocumentPart UCBusinessOtherPart
        {
            get
            {
                if (ucBusinessOtherPart != null)
                {
                    return ucBusinessOtherPart;
                }
                else
                {
                    ucBusinessOtherPart = Workitem.Items.AddNew<UCContactAndDocumentPart>();
                    return ucBusinessOtherPart;
                }
            }
        }

        #endregion

        #region 本地变量

        CustomerType customerType = CustomerType.Unknown;

        List<CountryList> _countryList = null;

        private bool isSaveOperationContact = false;

        OceanBusinessInfo _businessInfo = null;
        OceanBusinessInfo oldBusinessInfo = new OceanBusinessInfo();
        string _currentMBLno = string.Empty;
        Guid? _currentCarrierID = null;

        Guid _VietnamCompanyId = new Guid("5a827adf-38c7-4a2f-99a7-ad717ce91718");//越南公司

        List<OceanBusinessMBLList> _mblNoList = new List<OceanBusinessMBLList>();

        private POItemEditPart UCOIOrderPOItemEdit2;
        DateTime? updatedate;
        Guid? FinalWareHouseID = Guid.Empty;
        Guid? ReturnLocationID = Guid.Empty;
        /// <summary>
        /// 是否发送发送到港通知书给客户
        /// </summary>
        public bool Ansc = false;
        /// <summary>
        /// 是否有数据发生改变
        /// </summary>
        private bool IsChanged
        {
            get
            {
                bool isCharge = false;
                if (_businessInfo.IsDirty
                    || _businessInfo.MBLInfo.IsDirty
                    || UCBoxList.IsChanged
                    || UCHBLList.IsChanged
                    || UCOIOrderFeeEdit.IsChanged
                    || (UCOIOrderPOItemEdit2 != null && UCOIOrderPOItemEdit2.IsChanged))
                {
                    isCharge = true;
                }


                return isCharge;
            }
        }
        // private VoyageDateInfoHelper voyageDateHelper = null;

        /// <summary>
        /// 邮件中心与ICP业务关联信息
        /// </summary>
        BusinessOperationParameter _businessOperationParameter = null;

        BusinessOperationContext OperationContext = new BusinessOperationContext();

        public override event SavedHandler Saved;


        #endregion

        #region 构造函数

        public OIBusinessEdit()
        {
            InitializeComponent();
            SyncLocalData = true;
            if (LocalData.IsDesignMode)
            {
                return;
            }

            if (!LocalData.IsDesignMode)
            {
                Load += (sender, e) =>
                {
                    UCBusinessOtherPart.Dock = DockStyle.Fill;
                    navGroupContactInfo.Controls.Clear();
                    navGroupContactInfo.Controls.Add(UCBusinessOtherPart);
                    labGateINDate.Text = LocalData.IsEnglish ? "GInD" : "进港日";
                };
            }

            Disposed += delegate
            {
                _countryList = null;
                _businessInfo = null;
                _currentMBLno = null;
                _currentCarrierID = null;
                _mblNoList = null;
                gridControl1.DataSource = null;
                gvOrders.DoubleClick -= gvOrders_DoubleClick;

                bsBusiness.BindingComplete -= bsBusiness_BindingComplete;
                bsBusiness.DataSource = null;
                bsBusiness.Dispose();

                bsMBLInfo.DataSource = null;
                bsMBLInfo.Dispose();

                RemoveEventHandle();

                if (shipperBridge != null)
                {
                    shipperBridge.Dispose();
                }
                if (consigneeBridge != null)
                {
                    consigneeBridge.Dispose();
                }
                if (notifyPartyBridge != null)
                {
                    notifyPartyBridge.Dispose();
                }
                if (customsBridge != null)
                {
                    customsBridge.Dispose();
                }

                if (customerFinderBridge != null)
                {
                    customerFinderBridge.Dispose();
                }

                if (consigneeFinderBridge != null)
                {
                    consigneeFinderBridge.Dispose();
                }

                if (agentOfCarrierFinderBridge != null)
                {
                    agentOfCarrierFinderBridge.Dispose();
                }

                if (wareHouseFinderBridge != null)
                {
                    wareHouseFinderBridge.Dispose();
                }

                if (customsFinderBridge != null)
                {
                    customsFinderBridge.Dispose();
                }


                if (pfbPlaceOfReceipt != null)
                {
                    pfbPlaceOfReceipt.Dispose();
                }
                if (pfbPOL != null)
                {
                    pfbPOL.Dispose();
                }
                if (pfbPOD != null)
                {
                    pfbPOD.Dispose();
                }
                if (pfbPlaceOfDelivery != null)
                {
                    pfbPlaceOfDelivery.Dispose();
                }
                if (pfbFinalDestination != null)
                {
                    pfbFinalDestination.Dispose();
                }


                if (UCBoxList != null)
                {
                    UCBoxList.Dispose();
                }
                if (UCOIOrderFeeEdit != null)
                {
                    UCOIOrderFeeEdit.Dispose();
                }
                if (UCHBLList != null)
                {
                    UCHBLList.Dispose();
                }

                if (UCOIOrderFeeEdit != null)
                {
                    UCOIOrderFeeEdit.Dispose();
                }
                if (UCOIOrderPOItemEdit2 != null)
                {
                    Workitem.Items.Remove(UCOIOrderPOItemEdit2);
                    UCOIOrderPOItemEdit2.Dispose();
                    UCOIOrderPOItemEdit2 = null;
                }

            };

            barBill.ItemClick += new ItemClickEventHandler(barBill_ItemClick);
            barPrintArrivalNotice.ItemClick += new ItemClickEventHandler(barPrintArrivalNotice_ItemClick);
            barReleaseOrder.ItemClick += new ItemClickEventHandler(barReleaseOrder_ItemClick);
            barPrintWorkSheet.ItemClick += new ItemClickEventHandler(barPrintWorkSheet_ItemClick);
            barPrintProfit.ItemClick += new ItemClickEventHandler(barPrintProfit_ItemClick);
            barPrintForwardingBill.ItemClick += new ItemClickEventHandler(barPrintForwardingBill_ItemClick);

            barMailA_NToChs.ItemClick += new ItemClickEventHandler(barMailA_NToChs_ItemClick);
            barMailA_NToEng.ItemClick += new ItemClickEventHandler(barMailA_NToEng_ItemClick);

            Disposed += new EventHandler(OIBusinessEdit_Disposed);
            Load += new EventHandler(OIBusinessEdit_Load);
        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key.ToUpper() == "BusinessOperationParameter".ToUpper())
                {
                    _businessOperationParameter = item.Value as BusinessOperationParameter;
                    break;

                }
            }
        }
        /// <summary>
        /// 移除控件的事件处理程序
        /// </summary>
        private void RemoveEventHandle()
        {
            SmartPartClosing -= BusinessBaseEditPart_SmartPartClosing;
            Saved = null;
            UCBoxList.UpdateTotalQWM -= UCBoxList_UpdateTotalQWM;

            stxtConsignee.EditValueChanged -= stxtConsignee_EditValueChanged;
            stxtConsignee.OnOk -= stxtConsignee_OnOk;

            stxtCustomer.LostFocus -= stxtCustomer_LostFocus;
            stxtFinalDestination.TextChanged -= stxtFinalDestination_TextChanged;

            stxtMBLNo.EditValueChanged -= stxtMBLNo_EditValueChanged;
            stxtMBLNo.Leave -= stxtMBLNo_Leave;
            stxtMBLNo.SelectedIndexChanged -= stxtMBLNo_SelectedIndexChanged;

            stxtNotifyParty.OnOk -= stxtNotifyParty_OnOk;

            stxtPlaceOfDelivery.TextChanged -= stxtPlaceOfDelivery_TextChanged;

            stxtPOD.TextChanged -= stxtPOD_TextChanged;

            stxtShipper.OnOk -= stxtShipper_OnOk;

            treeBoxSalesDep.Enter -= treeBoxSalesDep_Enter;

            txtAgent.EditValueChanged -= stxtAgent_EditValueChanged;

            cmbCargoType.EditValueChanged -= cmbCargoType_EditValueChanged;
            cmbCargoType.Enter -= cmbCargoType_Enter;

            cmbCompany.SelectedIndexChanged -= cmbCompany_SelectedIndexChanged;

            cmbCustomerService.Enter -= cmbCustomerService_Enter;
            cmbCustomerService.SelectedIndexChanged -= cmbCustomerService_SelectedIndexChanged;
            cmbFile.Enter -= cmbFile_Enter;
            cmbReleaseType.EditValueChanged -= cmbReleaseType_EditValueChanged;
            cmbSales.SelectedRow -= cmbSales_SelectedRow;
            cmbSalesType.EditValueChanged -= cmbSalesType_EditValueChanged;

            cmbTradeTerm.SelectedIndexChanged -= cmbTradeTerm_SelectedIndexChanged;
            cmbTransportClause.SelectedIndexChanged -= cmbTransportClause_SelectedIndexChanged;
            cmbType.SelectedIndexChanged -= cmbType_SelectedIndexChanged;
            mcmbCarrier.SelectedRow -= mcmbCarrier_SelectedRow;

            cmbOverSeasFiler.Enter -= mcmOverseasFiler_Click;

            dtpETA.EditValueChanged -= dtpETA_EditValueChanged;
            dtpDETA.EditValueChanged -= dtpDETA_EditValueChanged;

            ckbIsClearance.CheckedChanged -= ckbIsClearance_CheckedChanged;
            panelTabBase.Click -= OnpnlMainClick;

            stxtNotifyParty.OnFirstEnter -= OnstxtNotifyPartyFirstEnter;
            stxtShipper.OnFirstEnter -= OnstxtShipperFirstEnter;

            //操作口岸列表  
            cmbCompany.OnFirstEnter -= OncmbCompanyFirstEnter;

            //运输条款
            cmbTransportClause.OnFirstEnter -= OncmbTransportClauseFirstEnter;

            //运输条款
            cmbMBLTransportClause.OnFirstEnter -= OncmbMBLTransportClauseFirstEnter;

            //贸易条款
            cmbTradeTerm.OnFirstEnter -= OncmbTradeTermFirstEnter;

            //包装
            cmbQuantityunit.OnFirstEnter -= OncmbQuantityunitFirstEnter;

            //重量
            cmbWeightUnit.OnFirstEnter -= OncmbWeightUnitFirstEnter;

            //体积
            cmbMeasurementUnit.OnFirstEnter -= OncmbMeasurementUnitFirstEnter;

            //揽货方式
            cmbSalesType.OnFirstEnter -= OncmbSalesTypeFirstEnter;

            //3个付款方式的下拉列表
            cmbPaymentTerm.OnFirstEnter -= OncmbPaymentTermFirstEnter;

            //揽货人
            cmbSales.OnFirstEnter -= OncmbSalesFirstEnter;

            //放货方式
            cmbReleaseType.OnFirstEnter -= OncmbReleaseTypeFirstEnter;

            //业务类型
            cmbType.OnFirstEnter -= OncmbTypeFirstEnter;
            //委托方式
            cmbBookingMode.OnFirstEnter -= OncmbBookingModeFirstEnter;

            //货物描述
            cmbCargoType.OnFirstEnter -= OncmbCargoTypeFirstEnter;

            //船公司
            mcmbCarrier.OnFirstEnter -= OnmcmbCarrierFirstEnter;

            //主提单号
            stxtMBLNo.Enter -= OnstxtMBLNoEnter;
        }

        #endregion

        #region IEditPart 成员

        void RefreshData(Guid orderId, bool isShowBusiness)
        {
            GetData(orderId);
            if (isShowBusiness)
            {
                ShowBusiness();
            }

            TriggerEventsAtOnce();
            ResetDescription();
            SetTitle();
            _currentMBLno = _businessInfo.MBLInfo == null ? string.Empty : _businessInfo.MBLInfo.MBLNo;
            _currentCarrierID = _businessInfo.MBLInfo == null ? null : _businessInfo.MBLInfo.CarrierID;
        }

        void GetData(Guid businessId)
        {
            _businessInfo = OceanImportService.GetBusinessInfoByEdit(businessId);
            oldBusinessInfo = OceanImportService.GetBusinessInfoByEdit(businessId);
        }

        void ShowBusiness()
        {
            InitData(_businessInfo);
            if (_businessInfo.ID != Guid.Empty && OperationAgentService.BusinessIsDownLoad(_businessInfo.ID))
            {
                txtAgent.Enabled = false;
            }

            bsBusiness.DataSource = _businessInfo;
            bsBusiness.ResetBindings(false);
            if (_businessInfo.MBLInfo.ID != null)
            {
                OceanBusinessMBLList mbl;
                bsMBLInfo.DataSource = mbl = OceanImportService.GetOIMBLInfo(_businessInfo.MBLInfo.ID);
                FinalWareHouseID = mbl.FinalWareHouseID;
                ReturnLocationID = mbl.ReturnLocationID;
                bsMBLInfo.ResetBindings(false);
                updatedate = _businessInfo.MBLInfo.UpdateDate;

            }
            InitControls();

            List<OceanImportFeeList> feelist = null;
            List<OIBusinessContainerList> containerList = null;
            List<OceanBusinessHBLList> hBLList = null;

            if (EditMode == EditMode.New)
            {
                feelist = new List<OceanImportFeeList>();
                hBLList = new List<OceanBusinessHBLList>();
                containerList = new List<OIBusinessContainerList>();
                OperationContext = new BusinessOperationContext();
            }
            else
            {
                //if (!OceanImportService.CheckIsInternalAgent(_businessInfo.AgentID))
                //{
                //    this.ckbIsTelex.Enabled = false;
                //}
                feelist = _businessInfo.FeeList;
                hBLList = _businessInfo.HBLList;
                containerList = _businessInfo.ContainerList;
                OperationContext = GetContext(_businessInfo);
            }

            UCOIOrderFeeEdit.SetSource(feelist);
            UCHBLList.BindHBLList(hBLList);
            UCHBLList.BusinessID = _businessInfo.ID;
            UCBoxList.BindContainerList(containerList);
            UCBoxList.BusinessID = _businessInfo.ID;
            UCBoxList.MBLID = _businessInfo.MBLID.ToGuid();
            ucBusinessOtherPart.SetContext = OperationContext;
            UCBusinessOtherPart.BindData(OperationContext);

            UCOIOrderFeeEdit.SetChargeCodeDataSource(_businessInfo.FreightIncludedIds, _businessInfo.FreightIncludedCodes);
        }

        void BindingData(object data)
        {
            SuspendLayout();

            //HBL信息服务
            UCHBLList.SetService(Workitem);
            //集装箱信息
            UCBoxList.SetService(Workitem, true);
            UCBoxList.UpdateTotalQWM += new SelectedHandler(UCBoxList_UpdateTotalQWM);

            UCOIOrderFeeEdit.SetService(Workitem);
            EditPartShowCriteria criteria = data as EditPartShowCriteria;

            if (EditMode == EditMode.New)
            {
                _businessInfo = new OceanBusinessInfo();
                ReadyForNew();
            }
            else
            {
                GetData((Guid)criteria.BillNo);
                if (EditMode == EditMode.Edit)
                {

                }
                else if (EditMode == EditMode.Copy)
                {
                    PrepareForCopyExistBusiness();
                }
            }

            ShowBusiness();

            #region 判断邮件地址是否存在联系人列表中
            if (_businessOperationParameter != null)
            {
                if (_businessOperationParameter.Message != null)
                {
                    if (_businessOperationParameter.Message.Attachments.Count != 0)
                    {
                        List<string> strFiles = new List<string>();
                        foreach (var item in _businessOperationParameter.Message.Attachments)
                        {
                            strFiles.Add(item.ClientPath);
                        }
                        UCBusinessOtherPart.AddDocuments(strFiles, DocumentType.AN);
                    }
                    if (_businessOperationParameter.ActionType ==
                        ActionType.Create)
                    {
                        List<CustomerCarrierObjects> dataList = new List<CustomerCarrierObjects>();
                        List<MailContactInfo> contactList = MailContactInfo.Convert(_businessOperationParameter.Message);
                        foreach (var item in contactList)
                        {

                            if (!FCM.Common.ServiceInterface.FCMInterfaceUtility.ExsitsInternalContact(item.EmailAddress))
                            {
                                dataList.Add(FCM.Common.ServiceInterface.FCMInterfaceUtility.CreateCustomerCarrierInfo(true, false, false,
                                                                               item.EmailAddress,
                                                                               item.Name));
                            }

                        }

                        UCBusinessOtherPart.InsertContactList(dataList);
                        dataList.Clear();
                        dataList = null;
                        //UCBookingOtherPart.InsertCustomerInfo(customerCarrier);
                        isSaveOperationContact = true;
                    }
                }
                else
                {
                    if (EditMode == EditMode.Copy)
                    {
                        ContactObjects contactInfo = ServiceClient.GetService<IFCMCommonService>().GetContactList(_businessOperationParameter.Context.OperationID, _businessOperationParameter.Context.OperationType);
                        if (contactInfo != null && contactInfo.CustomerCarrier != null)
                        {
                            UCBusinessOtherPart.InnerBindData(contactInfo.CustomerCarrier);
                            isSaveOperationContact = true;
                        }
                    }
                }
            }
            #endregion

            SearchRegister();
            SetLazyLoaders();
            SetLazyDataLodersWithDynamicCondition();

            if (!LocalData.IsDesignMode)
            {
                cmbCompany.Focus();
                SetTitle();
                RegisterRelativeEvents();
                RegisterRelativeEventsAndRunOnce();
            }
            ResumeLayout();

            if (_businessInfo.MBLInfo.FreightRateID != null)
            {
                txtContractNo.Enabled = true;
            }
        }

        public override object DataSource
        {
            get { return bsBusiness.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return Save(_businessInfo, false);
        }

        public override void EndEdit()
        {
            // cmbCargoType.ClosePopup();
            Validate();
            bsBusiness.EndEdit();
            bsMBLInfo.EndEdit();
        }



        #endregion

        #region 新业务的逻辑/默认值

        void ReadyForNew()
        {
            if (_businessInfo.ID == Guid.Empty)
            {
                OceanBusinessInfo newData = new OceanBusinessInfo();
                newData.State = OIOrderState.NewOrder;
                newData.CreateID = LocalData.UserInfo.LoginID;
                newData.CreateByName = LocalData.UserInfo.LoginName;
                newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                newData.BookingDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                newData.BookingMode = FCMBookingMode.EMail;
                newData.IsValid = true;

                #region 设置默认值
                DataDictionaryList normalDictionary = null;
                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.PaymentTerm);
                newData.PaymentTermID = normalDictionary.ID;
                newData.PaymentTermName = normalDictionary.EName;

                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
                newData.QuantityUnitID = normalDictionary.ID;
                newData.QuantityUnitName = normalDictionary.EName;

                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
                newData.WeightUnitID = normalDictionary.ID;
                newData.WeightUnitName = normalDictionary.EName;

                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
                newData.MeasurementUnitID = normalDictionary.ID;
                newData.MeasurementUnitName = normalDictionary.EName;
                #endregion

                _businessInfo = newData;
                _businessInfo.OIOperationType = FCMOperationType.FCL;
                barSaveAs.Enabled = false;
                //this.stxtCustomer.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.stxtCustomer_ButtonClick);
                gvOrders.DoubleClick += new EventHandler(gvOrders_DoubleClick);
                Utility.EnsureDefaultCompanyExists(UserService);
                Utility.EnsureDefaultDepartmentExists(UserService);
                _businessInfo.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                _businessInfo.CompanyName = LocalData.UserInfo.DefaultCompanyName;
                _businessInfo.FilerId = LocalData.UserInfo.LoginID;
                _businessInfo.FilerName = LocalData.UserInfo.LoginName;
            }
            else
            {
                //this.stxtCustomer.Properties.Buttons[2].Visible = false;
                //this.stxtCustomer.Properties.Buttons[2].Visible = false;
                //this.gvOrders.DoubleClick -= new System.EventHandler(this.gvOrders_DoubleClick);
                //this.stxtCustomer.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.stxtCustomer_ButtonClick);
            }
        }

        #endregion


        /// <summary>
        /// 复制业务时的逻辑
        /// </summary>
        void PrepareForCopyExistBusiness()
        {
            _businessInfo.ID = Guid.Empty;
            _businessInfo.State = OIOrderState.NewOrder;
            _businessInfo.No = string.Empty;
            _businessInfo.CreateID = LocalData.UserInfo.LoginID;
            _businessInfo.CreateByName = LocalData.UserInfo.LoginName;
            _businessInfo.CreateDate = DateTime.Now;
            _businessInfo.SalesID = LocalData.UserInfo.LoginID;
            _businessInfo.SalesName = LocalData.UserInfo.LoginName;
            _businessInfo.MBLID = Guid.Empty;
            _businessInfo.HBLID = Guid.Empty;
            _businessInfo.MBLNo = string.Empty;
            if (_businessInfo.HBLList != null && _businessInfo.HBLList.Count > 0)
            {
                //_businessInfo.HBLList.Clear();
                _businessInfo.HBLList.ForEach(h =>
                {
                    h.ID = Guid.Empty;
                    h.HBLNo = string.Empty;
                });
            }

            if (_businessInfo.MBLInfo != null)
            {
                _businessInfo.MBLInfo.ID = Guid.Empty;
                _businessInfo.MBLInfo.MBLNo = string.Empty;
            }

            if (_businessInfo.ContainerList != null && _businessInfo.ContainerList.Count > 0)
            {
                _businessInfo.ContainerList.Clear();
            }
        }

        #region 不能为空的一些属性和数据

        /// <summary>
        /// 不能为空的一些属性和数据
        /// </summary>
        /// <param name="info"></param>
        private void InitData(OceanBusinessInfo info)
        {
            if (!ArgumentHelper.GuidIsNullOrEmpty(info.ID)
               && info.CargoDescription != null
               && info.CargoDescription.Cargo != null)
            {
                if (info.CargoDescription.Cargo is DangerousCargo)
                    cmbCargoType.EditValue = CargoType.Dangerous;
                else if (info.CargoDescription.Cargo is AwkwardCargo)
                    cmbCargoType.EditValue = CargoType.Awkward;
                else if (info.CargoDescription.Cargo is ReeferCargo)
                    cmbCargoType.EditValue = CargoType.Reefer;
                else if (info.CargoDescription.Cargo is DryCargo)
                    cmbCargoType.EditValue = CargoType.Dry;
            }

            if (ArgumentHelper.GuidIsNullOrEmpty(info.ID))
            {
                info.BookingDate = DateTime.Today;
            }

            if (_businessInfo.ConsigneeDescription == null)
            {
                _businessInfo.ConsigneeDescription = new CustomerDescription();
            }
            if (_businessInfo.ShipperDescription == null)
            {
                _businessInfo.ShipperDescription = new CustomerDescription();
            }
            if (_businessInfo.NotifyPartyDescription == null)
            {
                _businessInfo.NotifyPartyDescription = new CustomerDescription();
            }
            if (_businessInfo.AgentDescription == null)
            {
                _businessInfo.AgentDescription = new CustomerDescription();
            }
            if (_businessInfo.MBLInfo == null)
            {
                _businessInfo.MBLInfo = new OceanBusinessMBLList();
            }
            if (_businessInfo.CustomsDescription == null)
            {
                _businessInfo.CustomsDescription = new CustomerDescription();
            }
        }

        #endregion

        #region 初始化下拉控件

        /// <summary>
        /// 初始化下拉控件
        /// </summary>
        private void InitControls()
        {
            SetState();
            if (_businessInfo.State == OIOrderState.Release)
            {
                ckbIsTelex.Properties.ReadOnly = true;
            }
            else
            {
                ckbIsTelex.Properties.ReadOnly = false;
            }

            DevHelper.FormatSpinEditForInteger(numQuantity);
            DevHelper.FormatSpinEdit(numWeight, 3);
            DevHelper.FormatSpinEdit(numMeasurement, 3);

            //操作口岸
            cmbCompany.ShowSelectedValue(_businessInfo.CompanyID, _businessInfo.CompanyName);
            treeBoxSalesDep.ShowSelectedValue(_businessInfo.SalesDepartmentID, _businessInfo.SalesDepartmentName);

            UCOIOrderFeeEdit.SetCompanyID(_businessInfo.CompanyID);

            //运输条款
            cmbTransportClause.ShowSelectedValue(_businessInfo.TransportClauseID, _businessInfo.TransportClauseName);

            cmbTradeTerm.ShowSelectedValue(_businessInfo.TradeTermID, _businessInfo.TradeTermName);
            cmbQuantityunit.ShowSelectedValue(_businessInfo.QuantityUnitID, _businessInfo.QuantityUnitName);
            cmbMeasurementUnit.ShowSelectedValue(_businessInfo.MeasurementUnitID, _businessInfo.MeasurementUnitName);
            cmbWeightUnit.ShowSelectedValue(_businessInfo.WeightUnitID, _businessInfo.WeightUnitName);

            //付款方式
            cmbPaymentTerm.ShowSelectedValue(_businessInfo.PaymentTermID, _businessInfo.PaymentTermName);

            //货物类型
            if (_businessInfo.CargoType.HasValue)
            {
                cmbCargoType.ShowSelectedValue(_businessInfo.CargoType,
                    EnumHelper.GetDescription<CargoType>(_businessInfo.CargoType.Value, LocalData.IsEnglish));
            }

            //揽货类型
            cmbSalesType.ShowSelectedValue(_businessInfo.SalesTypeID, _businessInfo.SalesTypeName);

            //揽货人
            cmbSales.ShowSelectedValue(_businessInfo.SalesID, _businessInfo.SalesName);

            //业务类型
            cmbType.ShowSelectedValue(_businessInfo.OIOperationType,
                EnumHelper.GetDescription<FCMOperationType>(_businessInfo.OIOperationType, LocalData.IsEnglish));

            //委托方式
            cmbBookingMode.ShowSelectedValue(_businessInfo.BookingMode,
                EnumHelper.GetDescription<FCMBookingMode>(_businessInfo.BookingMode, LocalData.IsEnglish));

            //////主提单号
            ////this.stxtMBLNo.ShowSelectedValue(this._businessInfo.MBLInfo.ID, this._businessInfo.MBLInfo.MBLNo);

            InitMBLControl();

            #region CustomerDescription/CargoDescription/ContainerDescription

            if (_businessInfo.CargoDescription != null && _businessInfo.CargoDescription.Cargo != null)
            {
                txtCargoDescription.Text = _businessInfo.CargoDescription.Cargo.ToString(LocalData.IsEnglish);
            }

            #endregion
            cmbFile.ShowSelectedValue(_businessInfo.FilerId, _businessInfo.FilerName);
            cmbCustomerService.ShowSelectedValue(_businessInfo.CustomerService, _businessInfo.CustomerServiceName);
            cmbOverSeasFiler.ShowSelectedValue(_businessInfo.OverSeasFilerId, _businessInfo.OverSeasFilerName);
            cmbLcs.ShowSelectedValue(_businessInfo.LocalCSId, _businessInfo.LocalCSName);


            if (_businessInfo != null && _businessInfo.MBLInfo != null)
            {
                if (_businessInfo.MBLInfo.PreVoyageID != null)
                {
                    stxtPreVoyage.ShowSelectedValue(_businessInfo.MBLInfo.PreVoyageID, _businessInfo.MBLInfo.PreVoyageName);
                }
                if (_businessInfo.MBLInfo.VoyageID != null)
                {
                    stxtVoyage.ShowSelectedValue(_businessInfo.MBLInfo.VoyageID, _businessInfo.MBLInfo.VoyageName);
                }
            }

            if (LocalData.IsEnglish)
            {
                labContractNo.Text = "SC NO";
            }
        }

        private void InitMBLControl()
        {
            if (_businessInfo.State == OIOrderState.Release)
            {
                cmbReleaseType.Properties.ReadOnly = true;
            }
            else
            {
                cmbReleaseType.Properties.ReadOnly = false;
            }

            //船公司
            mcmbCarrier.ShowSelectedValue(_businessInfo.MBLInfo.CarrierID, _businessInfo.MBLInfo.CarrierName);

            //放货类型
            cmbReleaseType.ShowSelectedValue(_businessInfo.MBLInfo.ReleaseType,
                EnumHelper.GetDescription<FCMReleaseType>(_businessInfo.MBLInfo.ReleaseType, LocalData.IsEnglish));

            UCHBLList.SetReceive(_businessInfo.MBLInfo.ReleaseType);

            //MBL运输条款
            cmbMBLTransportClause.ShowSelectedValue(_businessInfo.MBLInfo.MBLTransportClauseID, _businessInfo.MBLInfo.MBLTransportClauseName);
        }

        #endregion

        #region 注册搜索器

        CustomerFinderBridge shipperBridge;
        CustomerFinderBridge consigneeBridge;
        CustomerFinderBridge notifyPartyBridge;
        CustomerFinderBridge customsBridge;
        CustomerContactFinderBridge customerFinderBridge;
        CustomerContactFinderBridge consigneeFinderBridge;
        CustomerContactFinderBridge agentOfCarrierFinderBridge;
        CustomerContactFinderBridge wareHouseFinderBridge;
        CustomerContactFinderBridge customsFinderBridge;

        private void OnstxtShipperFirstEnter(object sender, EventArgs e)
        {
            if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

            shipperBridge = new CustomerFinderBridge(
           stxtShipper,
           _countryList,
           DataFindClientService,
           CustomerService,
           _businessInfo.ShipperDescription,
           ICPCommUIHelper,
           LocalData.IsEnglish);
            shipperBridge.Init();
        }

        private void OnstxtNotifyPartyFirstEnter(object sender, EventArgs e)
        {
            if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
            notifyPartyBridge = new CustomerFinderBridge(
            stxtNotifyParty,
            _countryList,
            DataFindClientService,
            CustomerService,
            _businessInfo.NotifyPartyDescription,
            ICPCommUIHelper,
            LocalData.IsEnglish);
            notifyPartyBridge.Init();
        }
        /// <summary>
        /// 注册搜索器
        /// </summary>
        void SearchRegister()
        {
            #region Customer

            //客户
            Utility.SetEnterToExecuteOnec(stxtCustomer, delegate
            {
                List<CustomerCarrierObjects> contactList = UCBusinessOtherPart.CurrentContactList.FindAll(item => item.CustomerID == _businessInfo.CustomerID);
                stxtCustomer.CustomerID = _businessInfo.CustomerID;
                stxtCustomer.SetOperationContext(OperationContext);
                stxtCustomer.CompanyID = _businessInfo.CompanyID;
                stxtCustomer.ContactStage = ContactStage.AN;
                stxtCustomer.ContactType = ContactType.Customer;
                stxtCustomer.ContactList = contactList;
                stxtCustomer.EditValueChanging += new ChangingEventHandler(stxtCustomer_EditValueChanging);
                stxtCustomer.EditValueChanged += new EventHandler(stxtCustomer_EditValueChanged);
                stxtCustomer.SelectChanged += new EventHandler<CommonEventArgs<OceanOrderInfo>>(stxtCustomer_SelectChanged);
            });

            stxtCustomer.OnOk += new EventHandler(stxtCustomer_OnOk);
            stxtCustomer.OnRefresh += new EventHandler(stxtCustomer_OnRefresh);

            #region 仓库

            DataFindClientService.Register(cmbWareHouse, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
            SearchFieldConstants.ResultValue,
          delegate(object inputSource, object[] resultData)
          {
              cmbWareHouse.Text = _businessInfo.WareHouseName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
              cmbWareHouse.Tag = _businessInfo.WareHouseID = new Guid(resultData[0].ToString());

              Guid id = new Guid(resultData[0].ToString());
              cmbWareHouse.SetCustomerID(id);
              CustomerDescription _customerDescription = new CustomerDescription();
              ICPCommUIHelper.SetCustomerDesByID(id, _customerDescription);
              cmbWareHouse.CustomerDescription = _businessInfo.WareHouseDescription = _customerDescription;
          }, delegate
          {
              cmbWareHouse.Tag = _businessInfo.WareHouseID = Guid.Empty;
              cmbWareHouse.Text = _businessInfo.WareHouseName = string.Empty;
              cmbWareHouse.SetCustomerID(Guid.Empty);
              cmbWareHouse.ContactList = new List<CustomerCarrierObjects>();
              cmbWareHouse.CustomerDescription = _businessInfo.WareHouseDescription = new CustomerDescription();
          }, ClientConstants.MainWorkspace);


            Utility.SetEnterToExecuteOnec(cmbWareHouse, delegate
            {
                List<CustomerCarrierObjects> contactList = UCBusinessOtherPart.CurrentContactList.FindAll(item => item.CustomerID == _businessInfo.WareHouseID);
                cmbWareHouse.SetOperationContext(OperationContext);
                wareHouseFinderBridge = new CustomerContactFinderBridge(cmbWareHouse, _businessInfo.WareHouseDescription, contactList, ContactStage.AN, _businessInfo.WareHouseID == null ? Guid.Empty : (Guid)_businessInfo.WareHouseID, true, ContactType.Customer);
                wareHouseFinderBridge.Init();
                cmbWareHouse.OnOk += new EventHandler(cmbWareHouse_OnOk);
                cmbWareHouse.BeforeEditValueChanged += new ChangingEventHandler(cmbWareHouse_EditValueChanging);
                cmbWareHouse.OnRefresh += new EventHandler(cmbWareHouse_OnRefresh);
                cmbWareHouse.AfterEditValueChanged += new EventHandler(cmbWareHouse_EditValueChanged);
            });
            #endregion

            #region 报关行

            DataFindClientService.Register(stxtCustoms, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
          SearchFieldConstants.ResultValue,
        delegate(object inputSource, object[] resultData)
        {
            stxtCustoms.Text = _businessInfo.CustomsName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
            stxtCustoms.Tag = _businessInfo.CustomsID = new Guid(resultData[0].ToString());

            Guid id = new Guid(resultData[0].ToString());
            stxtCustoms.SetCustomerID(id);
            CustomerDescription _customerDescription = new CustomerDescription();
            ICPCommUIHelper.SetCustomerDesByID(id, _customerDescription);
            stxtCustoms.CustomerDescription = _businessInfo.CustomsDescription = _customerDescription;
        }, delegate
        {
            stxtCustoms.Tag = _businessInfo.CustomsID = Guid.Empty;
            stxtCustoms.Text = _businessInfo.CustomsName = string.Empty;
            stxtCustoms.SetCustomerID(Guid.Empty);
            stxtCustoms.ContactList = new List<CustomerCarrierObjects>();
            stxtCustoms.CustomerDescription = _businessInfo.CustomsDescription = new CustomerDescription();
        }, ClientConstants.MainWorkspace);


            Utility.SetEnterToExecuteOnec(stxtCustoms, delegate
            {
                List<CustomerCarrierObjects> contactList = UCBusinessOtherPart.CurrentContactList.FindAll(item => item.CustomerID == _businessInfo.CustomsID);
                stxtCustoms.SetOperationContext(OperationContext);
                customsFinderBridge = new CustomerContactFinderBridge(stxtCustoms, _businessInfo.CustomsDescription, contactList, ContactStage.AN, _businessInfo.CustomsID == null ? Guid.Empty : (Guid)_businessInfo.CustomsID, true, ContactType.Customer);
                customsFinderBridge.Init();
                stxtCustoms.OnOk += new EventHandler(stxtCustoms_OnOk);
                stxtCustoms.BeforeEditValueChanged += new ChangingEventHandler(stxtCustoms_EditValueChanging);
                stxtCustoms.OnRefresh += new EventHandler(stxtCustoms_OnRefresh);
                stxtCustoms.AfterEditValueChanged += new EventHandler(stxtCustoms_EditValueChanged);
            });

            #endregion

            //MBL还柜地
            DataFindClientService.Register(stxtReturnLocation, CommonFinderConstants.CustoemrFinder,
                SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                GetConditionsForReturnLocation,
                delegate(object inputSouce, object[] resultData)
                {
                    stxtReturnLocation.Tag = ReturnLocationID = _businessInfo.MBLInfo.ReturnLocationID = new Guid(resultData[0].ToString());
                    stxtReturnLocation.EditValue = _businessInfo.MBLInfo.ReturnLocationName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate
                {
                    stxtReturnLocation.Tag = ReturnLocationID = _businessInfo.MBLInfo.ReturnLocationID = null;
                    stxtReturnLocation.EditValue = _businessInfo.MBLInfo.ReturnLocationName = string.Empty;
                },
            ClientConstants.MainWorkspace);

            //MBL提货地
            DataFindClientService.Register(stxtFinalWareHouse, CommonFinderConstants.CustoemrFinder,
              SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
               GetConditionsForFinalWareHouse,
               delegate(object inputSouce, object[] resultData)
               {
                   stxtFinalWareHouse.Tag = FinalWareHouseID = _businessInfo.MBLInfo.FinalWareHouseID = new Guid(resultData[0].ToString());
                   stxtFinalWareHouse.EditValue = _businessInfo.MBLInfo.FinalWareHouseName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
               },
               delegate
               {
                   stxtFinalWareHouse.Tag = FinalWareHouseID = _businessInfo.MBLInfo.FinalWareHouseID = Guid.Empty;
                   stxtFinalWareHouse.EditValue = _businessInfo.MBLInfo.FinalWareHouseName = string.Empty;
               },
           ClientConstants.MainWorkspace);

            #endregion

            #region 收货人  stxtConsignee

            DataFindClientService.Register(stxtConsignee, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
            SearchFieldConstants.ResultValue,
              delegate(object inputSource, object[] resultData)
              {
                  stxtConsignee.Text = _businessInfo.ConsigneeName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                  stxtConsignee.Tag = _businessInfo.ConsigneeID = new Guid(resultData[0].ToString());

                  Guid id = new Guid(resultData[0].ToString());
                  stxtConsignee.SetCustomerID(id);
                  CustomerDescription _customerDescription = new CustomerDescription();
                  ICPCommUIHelper.SetCustomerDesByID(id, _customerDescription);
                  stxtConsignee.CustomerDescription = _businessInfo.ConsigneeDescription = _customerDescription;

              }, delegate
              {
                  stxtConsignee.Tag = _businessInfo.ConsigneeID = Guid.Empty;
                  stxtConsignee.Text = _businessInfo.ConsigneeName = string.Empty;
                  stxtConsignee.SetCustomerID(Guid.Empty);
                  stxtConsignee.ContactList = new List<CustomerCarrierObjects>();
                  stxtConsignee.CustomerDescription = _businessInfo.ConsigneeDescription = new CustomerDescription();
              }, ClientConstants.MainWorkspace);


            Utility.SetEnterToExecuteOnec(stxtConsignee, delegate
            {
                List<CustomerCarrierObjects> contactList = UCBusinessOtherPart.CurrentContactList.FindAll(item => item.CustomerID == _businessInfo.ConsigneeID);
                stxtConsignee.SetOperationContext(OperationContext);
                consigneeFinderBridge = new CustomerContactFinderBridge(stxtConsignee, _businessInfo.ConsigneeDescription, contactList, ContactStage.AN, _businessInfo.ConsigneeID == null ? Guid.Empty : (Guid)_businessInfo.ConsigneeID, true, ContactType.Customer);
                consigneeFinderBridge.Init();
                stxtConsignee.OnOk += new EventHandler(stxtConsignee_OnOk);
                stxtConsignee.BeforeEditValueChanged += new ChangingEventHandler(stxtConsignee_EditValueChanging);
                stxtConsignee.OnRefresh += new EventHandler(stxtConsignee_OnRefresh);
                stxtConsignee.AfterEditValueChanged += new EventHandler(stxtConsignee_EditValueChanged);
            });
            #endregion


            #region MBL 承运人

            //MBL承运人
            DataFindClientService.Register(stxtAgentOfCarrier, CommonFinderConstants.CustomerAgentOfCarrierFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
             delegate(object inputSource, object[] resultData)
             {
                 stxtAgentOfCarrier.Text = _businessInfo.MBLInfo.AgentOfCarrierName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                 stxtAgentOfCarrier.Tag = _businessInfo.MBLInfo.AgentOfCarrierID = new Guid(resultData[0].ToString());

                 Guid id = new Guid(resultData[0].ToString());
                 stxtAgentOfCarrier.SetCustomerID(id);
                 CustomerDescription _customerDescription = new CustomerDescription();
                 ICPCommUIHelper.SetCustomerDesByID(id, _customerDescription);
                 stxtAgentOfCarrier.CustomerDescription = _businessInfo.MBLInfo.AgentOfCarrierDescription = _customerDescription;

             }, delegate
              {
                  stxtAgentOfCarrier.Tag = _businessInfo.MBLInfo.AgentOfCarrierID = Guid.Empty;
                  stxtAgentOfCarrier.Text = _businessInfo.MBLInfo.AgentOfCarrierName = string.Empty;
                  stxtAgentOfCarrier.SetCustomerID(Guid.Empty);
                  stxtAgentOfCarrier.ContactList = new List<CustomerCarrierObjects>();
                  stxtAgentOfCarrier.CustomerDescription = new CustomerDescription();
              }, ClientConstants.MainWorkspace);

            Utility.SetEnterToExecuteOnec(stxtAgentOfCarrier, delegate
            {
                List<CustomerCarrierObjects> contactList = UCBusinessOtherPart.CurrentContactList.FindAll(item => item.CustomerID == _businessInfo.MBLInfo.AgentOfCarrierID);
                stxtAgentOfCarrier.SetOperationContext(OperationContext);
                agentOfCarrierFinderBridge = new CustomerContactFinderBridge(stxtAgentOfCarrier, _businessInfo.MBLInfo.AgentOfCarrierDescription, contactList, ContactStage.AN, _businessInfo.MBLInfo.AgentOfCarrierID, true, ContactType.Carrier);
                agentOfCarrierFinderBridge.Init();
                stxtAgentOfCarrier.OnOk += new EventHandler(stxtAgentOfCarrier_OnOk);
                stxtAgentOfCarrier.BeforeEditValueChanged += new ChangingEventHandler(stxtAgentOfCarrier_EditValueChanging);
                stxtAgentOfCarrier.OnRefresh += new EventHandler(stxtAgentOfCarrier_OnRefresh);
                stxtAgentOfCarrier.AfterEditValueChanged += new EventHandler(stxtAgentOfCarrier_EditValueChanged);
            });
            #endregion

            #region SCNA

            //shipper
            stxtShipper.OnFirstEnter += OnstxtShipperFirstEnter;
            stxtShipper.OnOk += new EventHandler(stxtShipper_OnOk);

            //NotifyParty
            stxtNotifyParty.OnFirstEnter += OnstxtNotifyPartyFirstEnter;
            stxtNotifyParty.OnOk += new EventHandler(stxtNotifyParty_OnOk);
            #endregion

            #region Port
            pfbPlaceOfReceipt = new LocationFinderBridge(stxtPlaceOfReceipt, DataFindClientService, LocalData.IsEnglish);
            pfbPOL = new PortFinderBridge(stxtPOL, DataFindClientService, true);
            pfbPOD = new PortFinderBridge(stxtPOD, DataFindClientService, true);
            pfbPlaceOfDelivery = new LocationFinderBridge(stxtPlaceOfDelivery, DataFindClientService, true);
            pfbFinalDestination = new LocationFinderBridge(stxtFinalDestination, DataFindClientService, true);
            #endregion
        }

        LocationFinderBridge pfbPlaceOfReceipt;
        PortFinderBridge pfbPOL;
        PortFinderBridge pfbPOD;
        LocationFinderBridge pfbPlaceOfDelivery;
        LocationFinderBridge pfbFinalDestination;

        void stxtCustomer_OnOk(object sender, EventArgs e)
        {
            if (stxtCustomer.CustomerDescription != null && _businessInfo != null)
            {
                _businessInfo.CustomsDescription = stxtCustomer.CustomerDescription;
            }
            List<CustomerCarrierObjects> currentList = stxtCustomer.ContactList;

            UCBusinessOtherPart.RemoveContactList(_businessInfo.CustomerID, ContactType.Customer);
            if (currentList.Count > 0)
            {
                UCBusinessOtherPart.InsertContactList(currentList);
            }
            SetContactList(_businessInfo.CustomerID, currentList);
        }

        void stxtCustomer_EditValueChanging(object sender, EventArgs e)
        {
            RemoveContactList(_businessInfo.CustomerID, "CustomerID");
        }

        void stxtCustomer_OnRefresh(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> temp = new List<CustomerCarrierObjects>();
            if (EditMode == EditMode.New)
            {
                temp = FCMCommonService.GetLatestContactList(OperationType.OceanImport, _businessInfo.CompanyID, _businessInfo.CustomerID, ContactType.Customer, ContactStage.Unknown);

            }
            else
            {
                temp = FCMCommonService.GetContactListByContactStage(_businessInfo.ID, OperationType.OceanImport, ContactType.Customer, ContactStage.Unknown, _businessInfo.CustomerID);
            }
            UCBusinessOtherPart.RemoveContactList(_businessInfo.CustomerID, ContactType.Customer);
            UCBusinessOtherPart.InsertContactList(temp);
            SetContactList(_businessInfo.CustomerID, temp);
        }

        void stxtCustomer_EditValueChanged(object sender, EventArgs e)
        {
            stxtCustomer.EditValueChanged -= stxtCustomer_EditValueChanged;
            customerType = stxtCustomer.CustomerType;
            Guid customerId = stxtCustomer.CustomerID;
            _businessInfo.CustomerID = customerId;
            _businessInfo.CustomerName = stxtCustomer.CustomerName;
            cmbTradeTerm.ShowSelectedValue(stxtCustomer.TradeTermID, stxtCustomer.TradeTermName);

            AddLastestContact(_businessInfo.CustomerID, stxtCustomer, ContactType.Customer);
            stxtCustomer.CustomerID = customerId;
            CustomerDescription _customerDescription = new CustomerDescription();
            ICPCommUIHelper.SetCustomerDesByID(customerId, _customerDescription);
            stxtCustomer.CustomerDescription = _customerDescription;

            if (_businessInfo.CustomerID != Guid.Empty)
            {
                CustomerChanged(customerType);
            }
            else
            {
                CustomerChanged(null);
            }
            stxtCustomer.EditValueChanged += stxtCustomer_EditValueChanged;
        }

        void stxtConsignee_OnOk(object sender, EventArgs e)
        {
            if (stxtConsignee.CustomerDescription != null && _businessInfo != null)
            {
                _businessInfo.ConsigneeDescription = stxtConsignee.CustomerDescription;
            }
            List<CustomerCarrierObjects> currentList = stxtConsignee.ContactList;

            if (!ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.ConsigneeID))
            {
                UCBusinessOtherPart.RemoveContactList((Guid)_businessInfo.ConsigneeID, ContactType.Customer);

                if (currentList.Count > 0)
                {
                    UCBusinessOtherPart.InsertContactList(currentList);
                }
                SetContactList((Guid)_businessInfo.ConsigneeID, currentList);
            }

        }

        void stxtConsignee_EditValueChanging(object sender, EventArgs e)
        {
            if (!ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.ConsigneeID))
            {
                RemoveContactList((Guid)_businessInfo.ConsigneeID, "ConsigneeID");
            }
        }

        void stxtConsignee_OnRefresh(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> temp = new List<CustomerCarrierObjects>();
            if (ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.ConsigneeID))
            {
                return;
            }
            if (EditMode == EditMode.New)
            {
                temp = FCMCommonService.GetLatestContactList(OperationType.OceanImport, _businessInfo.CompanyID, (Guid)_businessInfo.ConsigneeID, ContactType.Customer, ContactStage.Unknown);

            }
            else
            {
                temp = FCMCommonService.GetContactListByContactStage(_businessInfo.ID, OperationType.OceanImport, ContactType.Customer, ContactStage.Unknown, (Guid)_businessInfo.ConsigneeID);
            }
            UCBusinessOtherPart.RemoveContactList((Guid)_businessInfo.ConsigneeID, ContactType.Customer);
            UCBusinessOtherPart.InsertContactList(temp);
            SetContactList((Guid)_businessInfo.ConsigneeID, temp);
        }

        void stxtConsignee_EditValueChanged(object sender, EventArgs e)
        {
            if (stxtConsignee.CustomerDescription != null && _businessInfo != null)
            {
                _businessInfo.ConsigneeDescription = stxtConsignee.CustomerDescription;
            }
        }


        void cmbWareHouse_OnOk(object sender, EventArgs e)
        {
            if (cmbWareHouse.CustomerDescription != null && _businessInfo != null)
            {
                _businessInfo.WareHouseDescription = cmbWareHouse.CustomerDescription;
            }
            List<CustomerCarrierObjects> currentList = cmbWareHouse.ContactList;

            if (!ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.WareHouseID))
            {
                UCBusinessOtherPart.RemoveContactList((Guid)_businessInfo.WareHouseID, ContactType.Customer);

                if (currentList.Count > 0)
                {
                    UCBusinessOtherPart.InsertContactList(currentList);
                }
                SetContactList((Guid)_businessInfo.WareHouseID, currentList);
            }

        }

        void cmbWareHouse_EditValueChanging(object sender, EventArgs e)
        {
            if (!ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.WareHouseID))
            {
                RemoveContactList((Guid)_businessInfo.WareHouseID, "WareHouseID");
            }
        }

        void cmbWareHouse_OnRefresh(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> temp = new List<CustomerCarrierObjects>();
            if (ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.WareHouseID))
            {
                return;
            }
            if (EditMode == EditMode.New)
            {
                temp = FCMCommonService.GetLatestContactList(OperationType.OceanImport, _businessInfo.CompanyID, (Guid)_businessInfo.WareHouseID, ContactType.Customer, ContactStage.Unknown);

            }
            else
            {
                temp = FCMCommonService.GetContactListByContactStage(_businessInfo.ID, OperationType.OceanImport, ContactType.Customer, ContactStage.Unknown, (Guid)_businessInfo.WareHouseID);
            }
            UCBusinessOtherPart.RemoveContactList((Guid)_businessInfo.WareHouseID, ContactType.Customer);
            UCBusinessOtherPart.InsertContactList(temp);
            SetContactList((Guid)_businessInfo.WareHouseID, temp);
        }

        void cmbWareHouse_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbWareHouse.CustomerDescription != null && _businessInfo != null)
            {
                _businessInfo.WareHouseDescription = cmbWareHouse.CustomerDescription;
            }
        }


        void stxtCustoms_OnOk(object sender, EventArgs e)
        {
            if (stxtCustoms.CustomerDescription != null && _businessInfo != null)
            {
                _businessInfo.CustomsDescription = stxtCustoms.CustomerDescription;
            }
            List<CustomerCarrierObjects> currentList = stxtCustoms.ContactList;

            if (!ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.CustomsID))
            {
                UCBusinessOtherPart.RemoveContactList((Guid)_businessInfo.CustomsID, ContactType.Customer);

                if (currentList.Count > 0)
                {
                    UCBusinessOtherPart.InsertContactList(currentList);
                }
                SetContactList((Guid)_businessInfo.CustomsID, currentList);
            }

        }

        void stxtCustoms_EditValueChanging(object sender, EventArgs e)
        {
            if (!ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.CustomsID))
            {
                RemoveContactList((Guid)_businessInfo.CustomsID, "CustomsID");
            }
        }

        void stxtCustoms_OnRefresh(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> temp = new List<CustomerCarrierObjects>();
            if (ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.CustomsID))
            {
                return;
            }
            if (EditMode == EditMode.New)
            {
                temp = FCMCommonService.GetLatestContactList(OperationType.OceanImport, _businessInfo.CompanyID, (Guid)_businessInfo.CustomsID, ContactType.Customer, ContactStage.Unknown);

            }
            else
            {
                temp = FCMCommonService.GetContactListByContactStage(_businessInfo.ID, OperationType.OceanImport, ContactType.Customer, ContactStage.Unknown, (Guid)_businessInfo.CustomsID);
            }
            UCBusinessOtherPart.RemoveContactList((Guid)_businessInfo.CustomsID, ContactType.Customer);
            UCBusinessOtherPart.InsertContactList(temp);
            SetContactList((Guid)_businessInfo.CustomsID, temp);
        }

        void stxtCustoms_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbWareHouse.CustomerDescription != null && _businessInfo != null)
            {
                _businessInfo.CustomsDescription = stxtCustoms.CustomerDescription;
            }
        }

        void stxtAgentOfCarrier_OnOk(object sender, EventArgs e)
        {
            if (stxtAgentOfCarrier.CustomerDescription != null && _businessInfo != null)
            {
                _businessInfo.MBLInfo.AgentOfCarrierDescription = stxtAgentOfCarrier.CustomerDescription;
            }
            List<CustomerCarrierObjects> currentList = stxtAgentOfCarrier.ContactList;

            UCBusinessOtherPart.RemoveContactList(_businessInfo.MBLInfo.AgentOfCarrierID, ContactType.Carrier);

            if (currentList.Count > 0)
            {
                UCBusinessOtherPart.InsertContactList(currentList);
            }
            SetContactList(_businessInfo.MBLInfo.AgentOfCarrierID, currentList);
        }

        void stxtAgentOfCarrier_EditValueChanging(object sender, EventArgs e)
        {
            RemoveContactList(_businessInfo.MBLInfo.AgentOfCarrierID, "AgentOfCarrierID");
        }

        void stxtAgentOfCarrier_OnRefresh(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> temp = new List<CustomerCarrierObjects>();
            if (EditMode == EditMode.New)
            {
                temp = FCMCommonService.GetLatestContactList(OperationType.OceanImport, _businessInfo.CompanyID, _businessInfo.MBLInfo.AgentOfCarrierID, ContactType.Carrier, ContactStage.Unknown);
            }
            else
            {
                temp = FCMCommonService.GetContactListByContactStage(_businessInfo.ID, OperationType.OceanImport, ContactType.Carrier, ContactStage.Unknown, _businessInfo.MBLInfo.AgentOfCarrierID);
            }
            UCBusinessOtherPart.RemoveContactList(_businessInfo.MBLInfo.AgentOfCarrierID, ContactType.Carrier);
            UCBusinessOtherPart.InsertContactList(temp);
            SetContactList(_businessInfo.MBLInfo.AgentOfCarrierID, temp);
        }

        void stxtAgentOfCarrier_EditValueChanged(object sender, EventArgs e)
        {
            if (stxtAgentOfCarrier.CustomerDescription != null && _businessInfo != null)
            {
                _businessInfo.MBLInfo.AgentOfCarrierDescription = stxtAgentOfCarrier.CustomerDescription;
            }
        }

        void cmbCustoms_OnOk(object sender, EventArgs e)
        {
            if (stxtCustoms.CustomerDescription != null && _businessInfo != null)
            {
                _businessInfo.CustomsDescription = stxtCustoms.CustomerDescription;
            }
        }

        void stxtNotifyParty_OnOk(object sender, EventArgs e)
        {
            if (stxtNotifyParty.CustomerDescription != null && _businessInfo != null)
            {
                _businessInfo.NotifyPartyDescription = stxtNotifyParty.CustomerDescription;
            }
        }


        void stxtShipper_OnOk(object sender, EventArgs e)
        {
            if (stxtShipper.CustomerDescription != null && _businessInfo != null)
            {
                _businessInfo.ShipperDescription = stxtShipper.CustomerDescription;
            }
        }

        /// <summary>
        /// 添加对应客户的上一票业务的对应沟通阶段的联系人
        /// </summary>
        /// <param name="customerID"></param>
        private void AddLastestContact(Guid customerID, object customerControl, ContactType contactType)
        {
            if (customerID == Guid.Empty)
            {
                return;
            }
            List<CustomerCarrierObjects> contactList = new List<CustomerCarrierObjects>();
            if (!UCBusinessOtherPart.CurrentContactList.Exists(item => item.CustomerID == customerID))
            {
                contactList = FCMCommonService.GetLatestContactList(OperationType.OceanExport, _businessInfo.CompanyID, customerID, contactType, ContactStage.Unknown);
                if (contactList == null || contactList.Count <= 0)
                    return;
                for (int i = 0; i < contactList.Count; i++)
                {
                    contactList[i].Id = Guid.Empty;

                }
                List<CustomerCarrierObjects> currentContactList = UCBusinessOtherPart.CurrentContactList;
                if (currentContactList == null || currentContactList.Count <= 0)
                {
                    UCBusinessOtherPart.InsertContactList(contactList);
                }
                else
                {
                    List<string> nameList = (from item in currentContactList select item.Name).ToList();
                    List<string> emailList = (from item in currentContactList select item.Mail).ToList();

                    contactList = contactList.FindAll(item => !nameList.Contains(item.Name) && !emailList.Contains(item.Mail));
                    UCBusinessOtherPart.InsertContactList(contactList);
                }
            }
            else
            {
                contactList = UCBusinessOtherPart.CurrentContactList.FindAll(item => item.CustomerID == customerID);
            }
            SetContactList(customerID, contactList);
        }

        private void SetContactList(Guid customerID, List<CustomerCarrierObjects> contactList)
        {
            if (_businessInfo.CustomerID == customerID)
            {
                stxtCustomer.ContactList = contactList;
            }

            if (_businessInfo.ConsigneeID == customerID)
            {
                stxtConsignee.ContactList = contactList;
            }

            if (_businessInfo.MBLInfo.AgentOfCarrierID == customerID)
            {
                stxtAgentOfCarrier.ContactList = contactList;
            }

            if (_businessInfo.WareHouseID == customerID)
            {
                cmbWareHouse.ContactList = contactList;
            }

            if (_businessInfo.AgentID == customerID)
            {
                txtAgent.ContactList = contactList;
            }

            if (_businessInfo.CustomsID == customerID)
            {
                stxtCustoms.ContactList = contactList;
            }
        }

        /// <summary>
        /// 判断当前引用的客户是否被其他栏位引用，否则移除相关客户的业务联系人
        /// </summary>
        /// <param name="changeID"></param>
        /// <param name="sourcePropertyName"></param>
        private void RemoveContactList(Guid changeID, string sourcePropertyName)
        {
            if (changeID == null || changeID == Guid.Empty)
                return;
            List<string> relativePropertyNames = new List<string> { "CustomerID", "BookingCustomerID", "AgentID", "AgentOfCarrierID", "WareHouseID", "CustomsID" };
            relativePropertyNames.Remove(sourcePropertyName);
            List<PropertyInfo> properties = typeof(OceanBusinessInfo).GetProperties().Where(p => relativePropertyNames.Contains(p.Name)).ToList();
            if (properties == null || properties.Count <= 0)
            {
                return;
            }
            if (!properties.Exists(p => p.GetValue(_businessInfo, null) != null && (Guid)p.GetValue(_businessInfo, null) == changeID))
            {
                UCBusinessOtherPart.RemoveContactList(changeID, null);
            }

        }

        void stxtCustomer_SelectChanged(object sender, CommonEventArgs<OceanOrderInfo> e)
        {
            if (e.Data == null)
            {
                return;
            }
            OceanOrderInfo currentOrderList = e.Data;


            DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "是否覆盖当前页面数据?" : "是否覆盖当前页面数据?"
                              , LocalData.IsEnglish ? "Tip" : "提示"
                              , MessageBoxButtons.YesNo
                              , MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                OceanBusinessInfo business = OceanImportService.GetBusinessInfoByEdit(currentOrderList.ID);
                if (business == null)
                {
                    return;
                }

                business.ID = Guid.Empty;
                business.SalesID = LocalData.UserInfo.LoginID;
                business.SalesName = LocalData.UserInfo.LoginName;
                business.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                _businessInfo = business;

                ShowBusiness();
                TriggerEventsAtOnce();
                ResetDescription();
                EndEdit();
                Invalidate();
            }
        }


        /// <summary>
        /// 当前客户最近业务所对应的海外部客服or 当前客户为新客户and当前揽货人最近业务所对应的海外部客服
        /// </summary>
        void SetDefaultOverseasFiler()
        {
            List<UserInfo> users = OceanImportService.GetOIOverseasFilersList(_businessInfo.CustomerID, _businessInfo.SalesID,
                DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).AddDays(-30), DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified), 1);

            if (users.Count > 0)
            {
                cmbOverSeasFiler.ShowSelectedValue(users[0].ID, LocalData.IsEnglish ? users[0].EName : users[0].CName);
            }
        }


        void ResetDescription()
        {
            if (shipperBridge != null)
            {
                shipperBridge.SetCustomerDescription(_businessInfo.ShipperDescription);
            }

            if (consigneeBridge != null)
            {
                consigneeBridge.SetCustomerDescription(_businessInfo.ConsigneeDescription);
            }
            if (customsBridge != null)
            {
                customsBridge.SetCustomerDescription(_businessInfo.CustomsDescription);
            }
        }

        #endregion

        #region 设置搜索器条件

        /// <summary>
        /// 搜索类型为“仓库”型的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForWarehouse()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Warehouse, false);

            return conditions;
        }

        /// <summary>
        /// 搜索类型为“报关行”型的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForCustoms()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Broker, false);

            return conditions;
        }

        /// <summary>
        /// “还柜地点”是类型为“码头”或“堆场”的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForReturnLocation()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Storage, false);
            conditions.AddWithValue("CustomerType", CustomerType.Terminal, false);

            return conditions;
        }

        /// <summary>
        /// “提货地”是类型为“码头”或“堆场”的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForFinalWareHouse()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Storage, false);
            conditions.AddWithValue("CustomerType", CustomerType.Terminal, false);

            return conditions;
        }

        /// <summary>
        /// 驳船
        /// 筛选：搜索的默认条件为 装货港=海运进口业务. 卸货港 and卸货港=海运进口业务.交收货地
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForSearchPreVoyage()
        {
            Guid polId = Guid.Empty;
            Guid podId = Guid.Empty;
            try
            {
                polId = (Guid)stxtPOD.Tag;
            }
            catch
            {
                throw new Exception(LocalData.IsEnglish ? "Please select P.O.R. at first." : "请先选择收货地！");
            }

            try
            {
                podId = (Guid)stxtPlaceOfDelivery.Tag;
            }
            catch
            {
                throw new Exception(LocalData.IsEnglish ? "Please select POL at first." : "请先选择装货港！");
            }

            if (polId == Guid.Empty)
            {
                throw new Exception(LocalData.IsEnglish ? "Please select P.O.R. at first." : "请先选择收货地！");
            }

            if (podId == Guid.Empty)
            {
                throw new Exception(LocalData.IsEnglish ? "Please select POL at first." : "请先选择装货港！");
            }

            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("POLID", stxtPOD.Tag, false);
            conditions.AddWithValue("POLName", stxtPOD.Text, false);
            conditions.AddWithValue("PODID", stxtPlaceOfDelivery.Tag, false);
            conditions.AddWithValue("PODName", stxtPlaceOfDelivery.Text, false);

            return conditions;
        }

        /// <summary>
        /// 大船
        /// 筛选：装货港=当前装货港and卸货港=当前交货地
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForSearchVoyage()
        {
            Guid polId = Guid.Empty;
            Guid podID = Guid.Empty;
            try
            {
                polId = (Guid)stxtPOL.Tag;
            }
            catch
            {
                stxtPOL.Focus();
                throw new Exception(LocalData.IsEnglish ? "Please select POL at first." : "请先选择装货港！");
            }

            try
            {
                podID = (Guid)stxtPOD.Tag;
            }
            catch
            {
                stxtPOD.Focus();
                throw new Exception(LocalData.IsEnglish ? "Please select the place of Pod at first." : "请先选择卸货港！");
            }

            if (polId == Guid.Empty)
            {
                stxtPOL.Focus();
                throw new Exception(LocalData.IsEnglish ? "Please select POL at first." : "请先选择装货港！");
            }

            if (podID == Guid.Empty)
            {

                stxtPOD.Focus();
                throw new Exception(LocalData.IsEnglish ? "Please select the place of Pod at first." : "请先选择卸货港！");
            }

            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("POLID", stxtPOL.Tag, false);
            conditions.AddWithValue("POLName", stxtPOL.Text, false);
            conditions.AddWithValue("PODID", stxtPOD.Tag, false);
            conditions.AddWithValue("PODName", stxtPOD.Text, false);

            return conditions;
        }

        #endregion

        #region Port And Voyage
        /// <summary>
        /// 交货地 如果目的港运输条款不等于Door，那么就为卸货港
        /// </summary>
        private void SetPlaceOfDeliveryByTransportClause()
        {
            //if (!ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(this._businessInfo.PlaceOfDeliveryID)
            //    || ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(this._businessInfo.TransportClauseID)) return;

            //if (_businessInfo.TransportClauseName.Contains("-DOOR") == false)
            //{
            //    stxtPlaceOfDelivery.Text = _businessInfo.PlaceOfDeliveryName = _businessInfo.PODName;
            //    stxtPlaceOfDelivery.Tag = _businessInfo.PlaceOfDeliveryID = _businessInfo.PODID;
            //}
        }

        /// <summary>
        /// 最终目的地 如果目的港运输条款不等于Door，那么就为交货地
        /// </summary>
        private void SetFinalDestinationByTransportClause()
        {
            if (!ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.FinalDestinationID)
                || ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.TransportClauseID)) return;

            if (_businessInfo.TransportClauseName.Contains("-DOOR") == false)
            {
                stxtFinalDestination.Text = _businessInfo.FinalDestinationName = _businessInfo.PlaceOfDeliveryName;
                stxtFinalDestination.Tag = _businessInfo.FinalDestinationID = _businessInfo.PlaceOfDeliveryID;
            }
        }

        /// <summary>
        /// 设置默认DETA
        /// </summary>
        private void SetDETA()
        {
            DateTime? eta = dtpETA.DateTime;
            if (eta == null || eta == DateTime.MinValue || eta == DateTime.MaxValue)
            {
                eta = _businessInfo.ETA;
            }
            //输入的时候，ID是空，只能要怕NAME去判断了
            if (_shown && _businessInfo.ETA != null && _businessInfo.PODName == _businessInfo.PlaceOfDeliveryName)
            {
                dtpDETA.DateTime = Convert.ToDateTime(_businessInfo.DETA = _businessInfo.ETA = eta);
            }
        }

        private void SetFETA()
        {
            DateTime? deta = dtpDETA.DateTime;
            if (deta == null || deta == DateTime.MinValue || deta == DateTime.MaxValue)
            {
                deta = _businessInfo.DETA;
            }
            if (_shown && _businessInfo.DETA != null && _businessInfo.PlaceOfDeliveryName == _businessInfo.FinalDestinationName)
            {
                dtpFETA.DateTime = Convert.ToDateTime(_businessInfo.FETA = _businessInfo.DETA = deta);
            }
        }

        #endregion

        #region 注册延迟加载的数据源
        private void OncmbCompanyFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.BindCompanyByAll(cmbCompany, false);
        }
        private void OncmbTransportClauseFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetCmbTransportClause(cmbTransportClause);
        }
        private void OncmbMBLTransportClauseFirstEnter(object sender, EventArgs e)
        {
            //MBL
            ICPCommUIHelper.SetCmbTransportClause(cmbMBLTransportClause);
        }
        private void OncmbTradeTermFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetCmbDataDictionary(cmbTradeTerm, DataDictionaryType.TradeTerm);
        }
        private void OncmbQuantityunitFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetCmbDataDictionary(cmbQuantityunit, DataDictionaryType.QuantityUnit, DataBindType.EName);
        }
        private void OncmbWeightUnitFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit, DataBindType.EName);
        }
        private void OncmbMeasurementUnitFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit, DataBindType.EName);
        }
        private void OncmbSalesTypeFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetCmbDataDictionary(cmbSalesType, DataDictionaryType.SalesType);
        }
        private void OncmbPaymentTermFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetCmbDataDictionary(cmbPaymentTerm, DataDictionaryType.PaymentTerm, DataBindType.EName, true);
        }
        private void OncmbSalesFirstEnter(object sender, EventArgs e)
        {
            List<UserList> userList = UserService.GetUserListByList(string.Empty, string.Empty, null, null, null, null, null, true, 0);

            UserList insertItem = new UserList();
            insertItem.ID = Guid.Empty;
            insertItem.EName = insertItem.CName = string.Empty;
            userList.Insert(0, insertItem);

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
            col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

            cmbSales.InitSource<UserList>(userList, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
        }
        private void OncmbReleaseTypeFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetComboxByEnum<FCMReleaseType>(cmbReleaseType, false);
        }
        private void OncmbTypeFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetComboxByEnum<FCMOperationType>(cmbType, false);
        }
        private void OncmbBookingModeFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetComboxByEnum<FCMBookingMode>(cmbBookingMode, false);
        }
        private void OntxtAgentFirstEnter(object sender, EventArgs e)
        {
            SetAgentSourceByCompanyID();
        }
        private void OncmbCargoTypeFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetComboxByEnum<CargoType>(cmbCargoType, true, true);
        }
        private void OnmcmbCarrierFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.BindCustomerList(mcmbCarrier, CustomerType.Carrier);
        }
        List<DataDictionaryList> _weightUnits;
        /// <summary>
        /// 注册延迟加载的数据源
        /// </summary>
        void SetLazyLoaders()
        {
            _weightUnits = ICPCommUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit, DataBindType.EName);


            //操作口岸列表  
            cmbCompany.OnFirstEnter += OncmbCompanyFirstEnter;

            //运输条款
            cmbTransportClause.OnFirstEnter += OncmbTransportClauseFirstEnter;


            //运输条款
            cmbMBLTransportClause.OnFirstEnter += OncmbMBLTransportClauseFirstEnter;



            //贸易条款
            cmbTradeTerm.OnFirstEnter += OncmbTradeTermFirstEnter;


            //包装
            cmbQuantityunit.OnFirstEnter += OncmbQuantityunitFirstEnter;

            //重量
            cmbWeightUnit.OnFirstEnter += OncmbWeightUnitFirstEnter;

            //体积
            cmbMeasurementUnit.OnFirstEnter += OncmbMeasurementUnitFirstEnter;


            //揽货方式
            cmbSalesType.OnFirstEnter += OncmbSalesTypeFirstEnter;

            //3个付款方式的下拉列表
            cmbPaymentTerm.OnFirstEnter += OncmbPaymentTermFirstEnter;

            //揽货人
            cmbSales.OnFirstEnter += OncmbSalesFirstEnter;


            //放货方式
            cmbReleaseType.OnFirstEnter += OncmbReleaseTypeFirstEnter;

            //业务类型
            cmbType.OnFirstEnter += OncmbTypeFirstEnter;
            //委托方式
            cmbBookingMode.OnFirstEnter += OncmbBookingModeFirstEnter;

            #region Agent 代理

            if (ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.AgentID) == false)
            {
                List<CustomerList> agentCustomers = new List<CustomerList>();
                CustomerList agentCustomer = new CustomerList();
                agentCustomer.CName = agentCustomer.EName = _businessInfo.AgentName;
                agentCustomer.ID = _businessInfo.AgentID.Value;
                agentCustomers.Insert(0, agentCustomer);
                SetAgentSource(agentCustomers);
            }
            Utility.SetEnterToExecuteOnec(txtAgent, delegate
            {
                SetAgentSourceByCompanyID();
                txtAgent.CustomerID = _businessInfo.AgentID ?? Guid.Empty;
                txtAgent.ContactStage = ContactStage.SO;
                txtAgent.ContactType = ContactType.Customer;
                txtAgent.CustomerDescription = _businessInfo.AgentDescription;
                List<CustomerCarrierObjects> contactList = UCBusinessOtherPart.CurrentContactList.FindAll(item => item.CustomerID == _businessInfo.AgentID);
                txtAgent.ContactList = contactList;
                txtAgent.OnOk += new EventHandler(stxtAgent_OnOk);
                txtAgent.OnRefresh += new EventHandler(stxtAgent_OnRefresh);
                txtAgent.EditValueChanging += new ChangingEventHandler(stxtAgent_EditValueChanging);

                txtAgent.EditValueChanged += delegate
                {
                    txtAgent.EditValueChanged -= new EventHandler(stxtAgent_EditValueChanged);
                    if (txtAgent.EditValue != null && txtAgent.EditValue.ToString().Length > 0)
                    {
                        Guid id = new Guid(txtAgent.EditValue.ToString());

                        ICPCommUIHelper.SetCustomerDesByID(id, _businessInfo.AgentDescription);
                        txtAgent.CustomerDescription = _businessInfo.AgentDescription;
                        AddLastestContact(id, txtAgent, ContactType.Customer);
                    }
                    txtAgent.EditValueChanged += new EventHandler(stxtAgent_EditValueChanged);
                };
            });
            #endregion

            //货物描述
            cmbCargoType.OnFirstEnter += OncmbCargoTypeFirstEnter;

            //船公司
            mcmbCarrier.OnFirstEnter += OnmcmbCarrierFirstEnter;

            //主提单号
            stxtMBLNo.Enter += OnstxtMBLNoEnter;

        }
        private void OnstxtMBLNoEnter(object sender, EventArgs e)
        {
            if (!isstxtMBLNoFirstEnter)
                return;
            _mblNoList = OceanImportService.GetOIMBLList();
            if (_businessInfo.OIOperationType == FCMOperationType.LCL)
            {
                if (_mblNoList.Count > 0)
                {
                    stxtMBLNo.Properties.BeginUpdate();
                    stxtMBLNo.Properties.Items.Clear();
                    foreach (var item in _mblNoList)
                    {
                        stxtMBLNo.Properties.Items.Add(item.MBLNo);
                    }
                    stxtMBLNo.Properties.EndUpdate();

                }
            }
            isstxtMBLNoFirstEnter = false;
        }
        private bool isstxtMBLNoFirstEnter = true;

        /// <summary>
        /// 延迟加载，而且条件是动态的
        /// </summary>
        void SetLazyDataLodersWithDynamicCondition()
        {
            cmbFile.Enter += new EventHandler(cmbFile_Enter);
            cmbCustomerService.Enter += new EventHandler(cmbCustomerService_Enter);
            cmbOverSeasFiler.Enter += new EventHandler(mcmOverseasFiler_Click);
            cmbLcs.Enter += new EventHandler(cmbLcs_Enter);

            treeBoxSalesDep.Enter += new EventHandler(treeBoxSalesDep_Enter);

        }

        void treeBoxSalesDep_Enter(object sender, EventArgs e)
        {
            List<OrganizationList> userOrganizationTreeLists = new List<OrganizationList>();
            if (ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.SalesID) == false)
            {
                userOrganizationTreeLists = UserService.GetUserCompanyList(_businessInfo.SalesID.Value, null);
            }

            List<OrganizationList> saleOrgrnazitionTreeList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
            foreach (OrganizationList dept in saleOrgrnazitionTreeList)
            {
                if (userOrganizationTreeLists.FindAll(o => o.ID == dept.ID).Count == 0)
                {
                    userOrganizationTreeLists.Add(dept);
                }
            }

            treeBoxSalesDep.SetSource<OrganizationList>(userOrganizationTreeLists, LocalData.IsEnglish ? "EShortName" : "CShortName", "HasPermission");
        }

        #endregion

        #region 注册界面控件之间联动的事件
        private void OnpnlMainClick(object sender, EventArgs e)
        {
            panelTabBase.Focus();
        }
        /// <summary>
        /// 注册界面控件之间联动的事件
        /// 一般一个控件的值改变，要影响别的控件的值，就注册到这里。如果同时还要改变其它控件的颜色、可用状态之类的逻辑，要拆分开。
        /// </summary>
        void RegisterRelativeEvents()
        {
            panelTabBase.Click += OnpnlMainClick;
            cmbCompany.SelectedIndexChanged += new EventHandler(cmbCompany_SelectedIndexChanged);
            cmbCustomerService.SelectedIndexChanged += new EventHandler(cmbCustomerService_SelectedIndexChanged);
            cmbCargoType.EditValueChanged += new EventHandler(cmbCargoType_EditValueChanged);

            cmbSales.SelectedRow += new EventHandler(cmbSales_SelectedRow);

            cmbReleaseType.EditValueChanged += new EventHandler(cmbReleaseType_EditValueChanged);

            //this.stxtMBLNo.SelectedIndexChanged += new EventHandler(stxtMBLNo_SelectedIndexChanged);
            stxtMBLNo.Leave += new EventHandler(stxtMBLNo_Leave);
            stxtMBLNo.EditValueChanged += new EventHandler(stxtMBLNo_EditValueChanged);
            mcmbCarrier.SelectedRow += new EventHandler(mcmbCarrier_SelectedRow);

            stxtCustomer.LostFocus += new EventHandler(stxtCustomer_LostFocus);
            cmbTradeTerm.SelectedIndexChanged += new EventHandler(cmbTradeTerm_SelectedIndexChanged);
            cmbTransportClause.SelectedIndexChanged += new EventHandler(cmbTransportClause_SelectedIndexChanged);

            ///this.stxtPOD.TextChanged += new EventHandler(stxtPOD_TextChanged);
            stxtPlaceOfDelivery.EditValueChanged += new EventHandler(stxtPlaceOfDelivery_TextChanged);
            stxtFinalDestination.EditValueChanged += new EventHandler(stxtFinalDestination_TextChanged);


            stxtPOD.EditValueChanged += new EventHandler(stxtPOD_TextChanged);

            dtpETA.EditValueChanged += new EventHandler(dtpETA_EditValueChanged);
            dtpDETA.EditValueChanged += new EventHandler(dtpDETA_EditValueChanged);


        }
        void dtpETA_EditValueChanged(object sender, EventArgs e)
        {
            SetDETA();
        }
        void dtpDETA_EditValueChanged(object sender, EventArgs e)
        {
            SetFETA();
        }



        void cmbCustomerService_SelectedIndexChanged(object sender, EventArgs e)
        {
            _businessInfo.CustomerServiceName = cmbCustomerService.Text;

            if (_businessInfo.TransportClauseID != new Guid("34F3CE9A-B2A4-4096-B5BA-1E2744401467"))
            {
                _businessInfo.LocalCSId = _businessInfo.customerService;
                cmbLcs.Text = _businessInfo.LocalCSName = _businessInfo.CustomerServiceName;
            }
        }


        void cmbSales_SelectedRow(object sender, EventArgs e)
        {
            if (_businessInfo.SalesID != null && _businessInfo.SalesID.ToGuid() != Guid.Empty)
            {
                SetSalesDepartment();
            }
        }

        void stxtPlaceOfDelivery_TextChanged(object sender, EventArgs e)
        {
            if (_shown)
            {
                _businessInfo.PlaceOfDeliveryName = stxtPlaceOfDelivery.Text;
                SetFinalDestinationByTransportClause();
                SetDETA();
            }
        }

        void stxtFinalDestination_TextChanged(object sender, EventArgs e)
        {
            if (_shown)
            {
                _businessInfo.FinalDestinationName = stxtFinalDestination.Text;
                SetFETA();
            }
        }

        void stxtPOD_TextChanged(object sender, EventArgs e)
        {
            if (_shown)
            {
                _businessInfo.PODName = stxtPOD.Text;
                SetPlaceOfDeliveryByTransportClause();
            }
        }

        void cmbTransportClause_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_shown)
            {
                _businessInfo.TransportClauseName = cmbTransportClause.Text;
                SetPlaceOfDeliveryByTransportClause();
                SetFinalDestinationByTransportClause();
            }
        }

        void stxtCustomer_LostFocus(object sender, EventArgs e)
        {
            //TODO: 看来Clone还是有必要保留，方便对比，避免不必要的服务交互
            //if (oldCustomerId != Guid.Empty && currentData.CustomerID == oldCustomerId) return;
            SetSalesTypeByCustomerAndCompany();
        }

        /// <summary>
        /// 注册界面控件之间联动的事件并立即执行一次
        /// 一般设置颜色、可用状态之类的，就注册到这里
        /// </summary>
        void RegisterRelativeEventsAndRunOnce()
        {
            cmbType.SelectedIndexChanged += new EventHandler(cmbType_SelectedIndexChanged);


            cmbSalesType.EditValueChanged += new EventHandler(cmbSalesType_EditValueChanged);
            TriggerEventsAtOnce();
        }
        /// <summary>
        /// 设置Agent数据源
        /// </summary>
        private void SetAgentSourceByCompanyID()
        {
            Guid companyID = _businessInfo.CompanyID;
            txtAgent.DataSource = null;
            if (ArgumentHelper.GuidIsNullOrEmpty(companyID))
            {
                txtAgent.Enabled = false;
                return;
            }

            List<CustomerList> agentCustomers = ConfigureService.GetCompanyAgentList(_businessInfo.CompanyID, true);
            CustomerList emptyCustomer = new CustomerList();
            emptyCustomer.CName = emptyCustomer.EName = string.Empty;
            emptyCustomer.ID = Guid.Empty;
            agentCustomers.Insert(0, emptyCustomer);

            //将公司配置中对应的客户从代理列表中去掉
            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(companyID);
            if (configureInfo != null)
            {
                agentCustomers = (from a in agentCustomers where a.ID != configureInfo.CustomerID select a).ToList();
            }

            if (_businessInfo.ID == Guid.Empty)
            {
                List<Guid> lstGuid = OceanImportService.GetInnerCustomers();
                agentCustomers.RemoveAll(ss => { return lstGuid.Contains(ss.ID); });
            }
            SetAgentSource(agentCustomers);
        }
        /// <summary>
        /// 设置代理的数据源
        /// </summary>
        /// <param name="agentCustomers"></param>
        private void SetAgentSource(List<CustomerList> agentCustomers)
        {
            txtAgent.SetLanguage(LocalData.IsEnglish);
            txtAgent.DataSource = agentCustomers;
            if (ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.AgentID))
            {
                txtAgent.EditValue = _businessInfo.AgentID = agentCustomers[0].ID;
            }
            txtAgent.EditValueChanged -= stxtAgent_EditValueChanged;
            txtAgent.EditValueChanged += stxtAgent_EditValueChanged;
        }
        /// <summary>
        /// 代理的值发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void stxtAgent_EditValueChanged(object sender, EventArgs e)
        {
            if (txtAgent.EditValue != null && txtAgent.EditValue.ToString().Length > 0)
            {
                Guid id = new Guid(txtAgent.EditValue.ToString());
                BulidAgentDescriqitonByID(id);
            }
        }

        void stxtAgent_OnOk(object sender, EventArgs e)
        {
            if (txtAgent.EditValue != null && txtAgent.EditValue.ToString().Length > 0)
            {
                Guid id = new Guid(txtAgent.EditValue.ToString());
                BulidAgentDescriqitonByID(id);
            }
        }

        void stxtAgent_OnRefresh(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> temp = new List<CustomerCarrierObjects>();
            if (EditMode == EditMode.New)
            {
                if (_businessInfo.AgentID == null)
                {
                    temp = FCMCommonService.GetLatestContactList(OperationType.OceanImport, _businessInfo.CompanyID, _businessInfo.AgentID.Value, ContactType.Customer, ContactStage.Unknown);
                }
            }
            else
            {
                if (_businessInfo.AgentID == null)
                {
                    temp = new List<CustomerCarrierObjects>();
                }
                else
                {
                    temp = FCMCommonService.GetContactListByContactStage(_businessInfo.ID, OperationType.OceanImport, ContactType.Customer, ContactStage.Unknown, _businessInfo.AgentID.Value);
                }
            }
            UCBusinessOtherPart.RemoveContactList(_businessInfo.AgentID.Value, ContactType.Customer);
            UCBusinessOtherPart.InsertContactList(temp);
            SetContactList(_businessInfo.AgentID.Value, temp);
        }

        void stxtAgent_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (_businessInfo.AgentID.HasValue)
            {
                txtAgent.EditValueChanging -= stxtAgent_EditValueChanging;
                RemoveContactList(_businessInfo.AgentID.Value, "AgentID");
                txtAgent.EditValueChanging += stxtAgent_EditValueChanging;
            }
        }



        /// <summary>
        /// 根据ID生成代理的描述和把描述填充到描述框
        /// </summary>
        int i = 0;
        private void BulidAgentDescriqitonByID(Guid? id)
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(id))
            {
                txtAgent.CustomerDescription = _businessInfo.AgentDescription = new CustomerDescription();
            }
            else
            {
                if (i != 0)
                {
                    ICPCommUIHelper.SetCustomerDesByID(id, _businessInfo.AgentDescription);
                }

                txtAgent.CustomerDescription = _businessInfo.AgentDescription;
            }
            i++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbTradeTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            _businessInfo.TradeTermName = cmbTradeTerm.Text;
        }



        /// <summary>
        /// 收货人:默认为客户
        /// </summary>
        private void SetConsigneeByCustomer()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.ConsigneeID))
            {
                stxtConsignee.Tag = _businessInfo.ConsigneeID = _businessInfo.CustomerID;
                stxtConsignee.Text = _businessInfo.ConsigneeName = _businessInfo.CustomerName;
                ICPCommUIHelper.SetCustomerDesByID(_businessInfo.ConsigneeID, _businessInfo.ConsigneeDescription);
            }

            ResetDescription();

        }
        /// <summary>
        /// 船东发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mcmbCarrier_SelectedRow(object sender, EventArgs e)
        {
            OceanBusinessMBLList mbl = Utility.Clone<OceanBusinessMBLList>(_businessInfo.MBLInfo);
            mbl.ID = Guid.Empty;
            mbl.MBLNo = stxtMBLNo.EditValue.ToString();
            if (mcmbCarrier.EditValue != null)
            {
                mbl.CarrierID = new Guid(mcmbCarrier.EditValue.ToString());
            }
            else
            {
                mbl.CarrierID = Guid.Empty;
            }

            mbl.UpdateDate = null;

            _businessInfo.MBLInfo = mbl;
            bsMBLInfo.DataSource = _businessInfo.MBLInfo;
            bsMBLInfo.ResetBindings(false);
            _businessInfo.MBLInfo.IsDirty = true;
            UCBoxList.MBLID = mbl.ID;
            List<OIBusinessContainerList> boxList = new List<OIBusinessContainerList>();
            UCBoxList.BindContainerList(boxList);
            _businessInfo.IsDirty = true;
            //InitMBLControl();
        }
        /// <summary>
        /// MBLNo发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stxtMBLNo_EditValueChanged(object sender, EventArgs e)
        {
            //MBLChange
        }
        string mblNo = string.Empty;
        /// <summary>
        /// MBL发生改变
        /// </summary>
        private void MBLChange()
        {
            string curruntNo = stxtMBLNo.EditValue.ToString();
            if (curruntNo != _currentMBLno)
            {
                OceanBusinessMBLList mbl = null;

                if (_mblNoList == null)
                {
                    _mblNoList = OceanImportService.GetOIMBLList();
                }

                mbl = _mblNoList.Find(mb => mb.MBLNo == curruntNo);

                List<OIBusinessContainerList> boxList = new List<OIBusinessContainerList>();
                if (mbl != null)
                {
                    mbl = OceanImportService.GetOIMBLInfo(mbl.ID);

                    if (mbl != null)
                    {
                        FinalWareHouseID = mbl.FinalWareHouseID;
                        ReturnLocationID = mbl.ReturnLocationID;
                        boxList = OceanImportService.GetOIContainerListByMBL(mbl.ID);
                        _businessInfo.MBLInfo = mbl;
                        bsMBLInfo.DataSource = _businessInfo.MBLInfo;
                        bsMBLInfo.ResetBindings(false);
                        _businessInfo.MBLInfo.IsDirty = true;
                        UCBoxList.MBLID = mbl.ID;
                        UCBoxList.BindContainerList(boxList);
                        updatedate = mbl.UpdateDate;
                        _businessInfo.IsDirty = true;
                        InitMBLControl();
                    }
                }
                else
                {
                    DialogResult result = XtraMessageBox.Show(
                    LocalData.IsEnglish ? "Please confirm whether changes to the master bill of lading number, click Yes to change the original bill of lading number, click No new master bill of lading。" : "请确认是否更改主提题单号，点击是更改原来的提单号，点击否新增主提单。"
                           , LocalData.IsEnglish ? "Tip" : "提示"
                           , MessageBoxButtons.YesNo
                           , MessageBoxIcon.Question);

                    //更新MBLNO
                    if (result == DialogResult.Yes)
                    {
                        _businessInfo.MBLNo = curruntNo;
                        _businessInfo.MBLInfo.MBLNo = curruntNo;
                        _businessInfo.MBLInfo.IsDirty = true;
                    }
                    else
                    {
                        _businessInfo.MBLNo = curruntNo;
                        _businessInfo.MBLID = Guid.Empty;
                        _businessInfo.MBLInfo.ID = Guid.Empty;
                        _businessInfo.MBLInfo.MBLNo = curruntNo;
                        //_businessInfo.HBLID = Guid.Empty;
                        //if (_businessInfo.HBLList != null && _businessInfo.HBLList.Count > 0)
                        //{
                        //    _businessInfo.HBLList.Clear();
                        //}
                        bsMBLInfo.ResetBindings(false);
                        //UCHBLList.BindHBLList(_businessInfo.HBLList);     
                        UCBoxList.MBLID = Guid.Empty;
                        UCBoxList.BindContainerList(boxList);
                        updatedate = null;
                    }
                }
                _currentMBLno = curruntNo;
            }
        }
        /// <summary>
        /// 选择的MBL发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void stxtMBLNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MBLChange();
        }
        /// <summary>
        /// 选择的MBL发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stxtMBLNo_Leave(object sender, EventArgs e)
        {
            MBLChange();
        }

        #endregion

        #region 主要是控件属性的改变（颜色、可操作性等）

        /// <summary>
        /// 主要是控件属性的改变（颜色、可操作性等）
        /// 包含下面所有的方法，逐一调用
        /// </summary>
        void TriggerEventsAtOnce()
        {
            cmbType_SelectedIndexChanged(this, null);
            cmbSalesType_EditValueChanged(this, null);
            RefreshBarEnabled();
        }

        void cmbSalesType_EditValueChanged(object sender, EventArgs e)
        {
            if (_shown)
            {
                #region 指定货--自揽货<设置海外部客服>
                Guid typeID = Guid.Empty;
                if (cmbSalesType.EditValue != null && (cmbSalesType.EditValue.ToString().ToUpper() == "E34BDCAA-4253-41C0-B14B-E38111CF2FC8" || cmbSalesType.EditValue.ToString().ToUpper() == "6B74A76C-74C9-4147-A3EC-A602C0F9D49B"))
                {
                    cmbOverSeasFiler.Enabled = true;
                }
                else
                {
                    cmbOverSeasFiler.Enabled = false;
                }


                if (!cmbOverSeasFiler.Enabled)
                {
                    cmbOverSeasFiler.ShowSelectedValue(Guid.Empty, string.Empty);//清除现有值
                    cmbOverSeasFiler.SpecifiedBackColor = txtNo.BackColor;


                    cmbSales.BackColor = Color.White;
                }
                else
                {
                    cmbOverSeasFiler.SpecifiedBackColor = Color.White;

                    cmbSales.BackColor = SystemColors.Info;
                }
                #endregion


            }
        }

        void RefreshBarEnabled()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.ID))
            {
                barRefresh.Enabled = false;
                barPrint.Enabled = false;
                barReturn.Enabled = false;
                barBill.Enabled = false;
            }
            else
            {
                barRefresh.Enabled = true;
                barPrint.Enabled = true;
                barReturn.Enabled = true;
                barBill.Enabled = true;
            }

            if (_businessInfo.State != OIOrderState.NewOrder)
            {
                barReturn.Enabled = false;
            }
        }

        #endregion

        #region 界面控件联动

        private void SetState()
        {
            txtState.Text = EnumHelper.GetDescription<OIOrderState>(_businessInfo.State, LocalData.IsEnglish);
        }

        /// <summary>
        /// 客户改变后需设置"订舱客户","收货人","代理","揽货方式","最近业务"
        /// </summary>
        /// <param name="customerType">客户的类型,请在方法外部获取</param>
        private void CustomerChanged(CustomerType? customerType)
        {
            SetConsigneeByCustomer();
            SetSalesTypeByCustomerAndCompany();
            //SetRecentlyOrderListByCustomerAndCompany();
            SetDefaultOverseasFiler();
        }

        #region 业务类型和集装箱信息

        void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkIsTruck.Enabled = _businessInfo.OIOperationType == FCMOperationType.FCL;

            if (!chkIsTruck.Enabled)
            {
                chkIsTruck.Checked = _businessInfo.IsTruck = false;
            }
        }


        #endregion



        void mcmOverseasFiler_Click(object sender, EventArgs e)
        {
            Guid OverseasFilerID = ConvObjToGuid(cmbCompany.EditValue);

            ICPCommUIHelper.SetComboxUsersByRole(cmbOverSeasFiler, OverseasFilerID, "海外拓展", true);
        }

        void cmbCustomerService_Enter(object sender, EventArgs e)
        {
            Guid CustomerServiceID = ConvObjToGuid(cmbCompany.EditValue);

            ICPCommUIHelper.SetComboxUsersByRole(cmbCustomerService, CustomerServiceID, "客服", true);
        }

        void cmbFile_Enter(object sender, EventArgs e)
        {
            Guid FileID = ConvObjToGuid(cmbCompany.EditValue);

            ICPCommUIHelper.SetComboxUsersByRole(cmbFile, FileID, "文件", true);
        }

        void cmbLcs_Enter(object sender, EventArgs e)
        {
            //List<Guid> Companylist = new List<Guid>();
            //Companylist.Add(new Guid("85A7D77F-2070-43E0-B866-FFF151BDCC5A"));
            //Companylist.Add(new Guid("699F3FAF-35F2-E411-B6DB-0026551CA878"));

            //ICPCommUIHelper.SetComboxUsersByRole(cmbLcs, Companylist, "文件", true);

            Guid CustomerServiceID = ConvObjToGuid(cmbCompany.EditValue);

            ICPCommUIHelper.SetComboxUsersByRole(cmbLcs, CustomerServiceID, "客服", true);
        }

        //将Ojbect类型转换为Guid类型
        Guid ConvObjToGuid(object guidID)
        {
            Guid GuidID = Guid.Empty;

            if (guidID != null)
            {
                GuidID = new Guid(guidID.ToString());
            }

            return GuidID;
        }


        /// <summary>
        /// 操作口岸改变时，刷新关联的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_shown)
            {
                if (cmbCompany.EditValue == null || cmbCompany.EditValue.ToString().Length == 0)
                {
                    return;
                }

                Guid companyID = new Guid(cmbCompany.EditValue.ToString());
                UCOIOrderFeeEdit.SetCompanyID(_businessInfo.CompanyID);
                SetSalesTypeByCustomerAndCompany();
                SetRecentlyOrderListByCustomerAndCompany();

                //值改变时需要刷新[文件和客服]下拉框。

                ICPCommUIHelper.SetComboxUsersByRole(cmbFile, ConvObjToGuid(cmbCompany.EditValue), "文件", true);

                ICPCommUIHelper.SetComboxUsersByRole(cmbCustomerService, ConvObjToGuid(cmbCompany.EditValue), "客服", true);
                //值改变时，绑定揽货人
                ICPCommUIHelper.SetMcmbUsers(cmbSales, _businessInfo.CompanyID, string.Empty, string.Empty, true);

                //操作公司发生改变时，绑定代理
                SetAgentSourceByCompanyID();

                //刷新最近
                SetRecentlyOrderListByCustomerAndCompany();
            }
        }

        /// <summary>
        /// 弹出最近十票业务
        /// </summary>
        private void stxtCustomer_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind != ButtonPredefines.Combo) return;

            //if (bsRecentTenOrders.DataSource == null || bsRecentTenOrders.List == null || bsRecentTenOrders.List.Count == 0)
            //{
            //    stxtCustomer.ShowToolTips = true;
            //    return;
            //}

            //stxtCustomer.ShowToolTips = false;

            //if (stxtCustomer.Properties.PopupControl.Visible == false)
            //    stxtCustomer.ShowPopup();
            //else
            //    stxtCustomer.ClosePopup();
        }

        /// <summary>
        /// “客户”失去焦点的时候自动设置揽货方式
        /// </summary>
        private void SetSalesTypeByCustomerAndCompany()
        {
            //if (_businessInfo.CompanyID != Guid.Empty && _businessInfo.CustomerID != Guid.Empty)
            //{
            //    DataDictionaryInfo salesType = OceanImportService.GetSalesType(_businessInfo.CustomerID, _businessInfo.CompanyID);
            //    if (salesType != null)
            //    {
            //        _businessInfo.SalesTypeID = salesType.ID;
            //        _businessInfo.SalesTypeName = salesType.EName;

            //        this.cmbSalesType.ShowSelectedValue(_businessInfo.SalesTypeID, _businessInfo.SalesTypeName);
            //    }
            //}
        }

        #region 最近10票业务

        /// <summary>
        /// 最近业务
        /// </summary>
        private void SetRecentlyOrderListByCustomerAndCompany()
        {
            //if (_businessInfo.ID != Guid.Empty || _businessInfo.CompanyID == Guid.Empty || _businessInfo.CustomerID == Guid.Empty)
            //{
            //    bsRecentTenOrders.Clear();
            //}
            //else
            //{
            //    bsRecentTenOrders.Clear();
            //    List<OceanOrderInfo> orderList = OceanImportService.GetOIRecentlyOrderList(_businessInfo.CompanyID, _businessInfo.CustomerID, LocalData.UserInfo.LoginID, 10);
            //    if (orderList != null && orderList.Count > 0)
            //    {
            //        bsRecentTenOrders.DataSource = orderList;
            //        stxtCustomer.ShowPopup();
            //        //设置仓库、报关的默认值为最近的纪录
            //        _businessInfo.WareHouseID = orderList[0].WareHouseID;
            //        _businessInfo.WareHouseName = orderList[0].WareHouseName;

            //        _businessInfo.CustomsID = orderList[0].CustomsID;
            //        _businessInfo.CustomsName = orderList[0].CustomsName;


            //    }
            //}
        }

        private void gvOrders_DoubleClick(object sender, EventArgs e)
        {
            //if (CurrentBusinessList == null)
            //{
            //    return;
            //}

            //DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "是否覆盖当前页面数据?" : "是否覆盖当前页面数据?"
            //                  , LocalData.IsEnglish ? "Tip" : "提示"
            //                  , MessageBoxButtons.YesNo
            //                  , MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    OceanBusinessInfo business = OceanImportService.GetBusinessInfoByEdit(CurrentBusinessList.ID);
            //    if (business == null)
            //    {
            //        return;
            //    }

            //    business.ID = Guid.Empty;
            //    business.SalesID = LocalData.UserInfo.LoginID;
            //    business.SalesName = LocalData.UserInfo.LoginName;
            //    business.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            //    _businessInfo = business;

            //    this.ShowBusiness();
            //    this.TriggerEventsAtOnce();
            //    this.ResetDescription();
            //    this.EndEdit();

            //    this.Invalidate();
            //}
        }

        #endregion

        /// <summary>
        /// TODO: 很多客户都不能弹出描述信息了
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="customerDescription"></param>
        private void SetCustomerDesByID(Guid? customerID, CustomerDescription customerDescription)
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(customerID)) return;

            CustomerInfo info = CustomerService.GetCustomerInfo(customerID.Value);
            customerDescription.Address = info.EAddress ?? string.Empty;
            customerDescription.City = info.CityName ?? string.Empty;
            customerDescription.Contact = string.Empty;
            customerDescription.Country = info.CountryName ?? string.Empty;
            customerDescription.Fax = info.Fax ?? string.Empty;
            customerDescription.Name = info.EName ?? string.Empty;
            customerDescription.Tel = info.Tel1 ?? string.Empty;
        }

        #region SetSalesDepartment

        /// <summary>
        /// 设置了Sales后,自动带出Sales部门
        /// </summary>
        private void SetSalesDepartment()
        {
            List<UserOrganizationTreeList> userOrganizationTreeLists = new List<UserOrganizationTreeList>();
            if (!ArgumentHelper.GuidIsNullOrEmpty(_businessInfo.SalesID))
            {
                userOrganizationTreeLists = UserService.GetUserOrganizationTreeList(_businessInfo.SalesID.Value);

                UserOrganizationTreeList orginazation = userOrganizationTreeLists.Find(o => o.IsDefault);
                if (orginazation != null)
                {
                    treeBoxSalesDep.ShowSelectedValue(orginazation.ID, LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName);
                    _businessInfo.SalesDepartmentID = orginazation.ID;
                    _businessInfo.SalesDepartmentName = LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName;
                }
                else
                {
                    treeBoxSalesDep.ShowSelectedValue(Guid.Empty, string.Empty);
                    _businessInfo.SalesDepartmentID = Guid.Empty;
                    _businessInfo.SalesDepartmentName = string.Empty;
                }
            }
        }

        /// <summary>
        /// 清空Sales后,自动清空Sales部门.操作
        /// </summary>
        private void ClearSalesDepartment()
        {
            treeBoxSalesDep.ClearItems();
            _businessInfo.SalesDepartmentID = Guid.Empty;
        }

        #endregion

        #region Cargo

        private void cmbCargoType_Enter(object sender, EventArgs e)
        {
            if (cmbCargoType.EditValue == null || cmbCargoType.EditValue.ToString() == string.Empty)
            {
                return;
            }
            CargoType cargoType = (CargoType)Enum.Parse(typeof(CargoType), cmbCargoType.EditValue.ToString());
            SetCargo(sender as Control, cargoType);
        }

        /// <summary>
        /// 货物类型的下拉列表的值发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCargoType_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbCargoType.EditValue == null || string.IsNullOrEmpty(cmbCargoType.EditValue.ToString()))
            {
                _businessInfo.CargoDescription = null;
                return;
            }

            if (cmbCargoType.Focused == false)
            {
                return;
            }

            CargoType cargoType = (CargoType)Enum.Parse(typeof(CargoType), cmbCargoType.EditValue.ToString());
            RemoveCargoPart();
            SetCargo(sender as Control, cargoType);
        }

        /// <summary>
        /// 货物类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cargoType"></param>
        private void SetCargo(Control sender, CargoType cargoType)
        {
            cmbCargoType.EditValueChanged -= new EventHandler(cmbCargoType_EditValueChanged);

            if (_businessInfo.CargoDescription == null)
            {
                _businessInfo.CargoDescription = new CargoDescription();
            }
            if (cargoType == CargoType.Awkward)
            {
                if (_businessInfo.CargoDescription.Cargo == null || _businessInfo.CargoDescription.Cargo is AwkwardCargo == false)
                {
                    _businessInfo.CargoDescription.Cargo = new AwkwardCargo();
                }

                if (cargoDescriptionPart1 is AwkwardDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new AwkwardDescriptionPart();
                    panelTabBase.Controls.Add(cargoDescriptionPart1);

                    cargoDescriptionPart1.ShowWeightUnit(_weightUnits);
                }
            }
            else if (cargoType == CargoType.Dangerous)
            {
                if (_businessInfo.CargoDescription.Cargo == null || _businessInfo.CargoDescription.Cargo is DangerousCargo == false)
                {
                    _businessInfo.CargoDescription.Cargo = new DangerousCargo();
                }

                if (cargoDescriptionPart1 is DangerousDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new DangerousDescriptionPart();
                    panelTabBase.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Dry)
            {
                if (_businessInfo.CargoDescription.Cargo == null || _businessInfo.CargoDescription.Cargo is DryCargo == false)
                {
                    _businessInfo.CargoDescription.Cargo = new DryCargo();
                }

                if (cargoDescriptionPart1 is DryDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new DryDescriptionPart();
                    panelTabBase.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Reefer)
            {
                if (_businessInfo.CargoDescription.Cargo == null || _businessInfo.CargoDescription.Cargo is ReeferCargo == false)
                {
                    _businessInfo.CargoDescription.Cargo = new ReeferCargo();
                }

                if (cargoDescriptionPart1 is ReeferDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ReeferDescriptionPart();
                    panelTabBase.Controls.Add(cargoDescriptionPart1);
                }
            }

            cargoDescriptionPart1.SetParentControl(sender, _businessInfo.CargoDescription, txtCargoDescription);
            cmbCargoType.EditValueChanged += new EventHandler(cmbCargoType_EditValueChanged);
        }

        private void RemoveCargoPart()
        {
            if (cargoDescriptionPart1 != null)
            {
                cargoDescriptionPart1.Hide();
                panelTabBase.Controls.Remove(cargoDescriptionPart1);
                cargoDescriptionPart1.Dispose();
            }
        }

        #endregion

        /// <summary>
        /// 转关
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbIsClearance_CheckedChanged(object sender, EventArgs e)
        {
            dtpClearanceDate.Enabled = ckbIsClearance.Checked;
        }

        ///// <summary>
        ///// 电放
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ckbIsTelex_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ckbIsTelex.Checked)
        //    {
        //        _businessInfo.MBLInfo.ReleaseType = ReleaseType.Telex;
        //    }
        //    else
        //    {
        //        _businessInfo.MBLInfo.ReleaseType = ReleaseType.Original;
        //    }
        //}

        /// <summary>
        /// 放货类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbReleaseType_EditValueChanged(object sender, EventArgs e)
        {
            //if (cmbReleaseType.EditValue == null || cmbReleaseType.EditValue.ToString() != ReleaseType.Telex.ToString())
            //{
            //    ckbIsTelex.Checked = false;
            //}
            //else
            //{
            //    ckbIsTelex.Checked = true;
            //}
            FCMReleaseType rType = (FCMReleaseType)cmbReleaseType.EditValue;
            UCHBLList.SetReceive(rType);

        }
        #endregion

        #region 工具栏事件

        #region 界面输入验证

        bool ValidateData()
        {
            EndEdit();
            List<bool> isScrrs = new List<bool> { true, true };
            isScrrs[0] = _businessInfo.Validate
                (
                    delegate(ValidateEventArgs e)
                    {
                        if (_businessInfo.POLID != Guid.Empty && _businessInfo.POLID == _businessInfo.PODID)
                        {
                            e.SetErrorInfo("PODID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");
                        }
                        //if (this.cmbSalesType.Text == "公司货" && (_businessInfo.SalesID==null||_businessInfo.SalesID == Guid.Empty))
                        //if (this.cmbSalesType.Text != "公司货" && (_businessInfo.SalesID == null || _businessInfo.SalesID == Guid.Empty))
                        //{
                        //    e.SetErrorInfo("SalesID", LocalData.IsEnglish ? "please input Sales." : "揽货人不能为空");
                        //}

                        #region ContainerDemand

                        ////果选择整箱业务类型，箱需求必输；箱需求逻辑,点击对应的箱型n次,则显示n*箱型
                        //if (_businessInfo.OIOperationType == OIOperationType.FCL)
                        //{
                        //    //if (this.containerDemandControl1.Text.Trim().Length == 0)
                        //    //{
                        //    //    e.SetErrorInfo("ConsigneeDescription", LocalData.IsEnglish ? "FCL Bussines Must Input." : "整箱业务必须输入箱需求.");
                        //    //    this.dxErrorProvider1.SetError(containerDemandControl1.ErrorHost, LocalData.IsEnglish ? "FCL Bussines Must Input." : "整箱业务必须输入箱需求.");
                        //    //    isScrrs[0] = false;
                        //    //}
                        //    //else
                        //    //{
                        //    //    this.dxErrorProvider1.SetError(containerDemandControl1.ErrorHost, string.Empty);
                        //    //}
                        //}
                        ////把箱需求转换成对象
                        ////_businessInfo.ContainerDescription = new ContainerDescription(this.containerDemandControl1.Text.Trim());


                        #endregion
                    }
                );

            #region 验证 MBL

            OceanBusinessMBLList originalMBL = bsMBLInfo.DataSource as OceanBusinessMBLList;

            if (!_businessInfo.MBLInfo.Validate())
            {
                isScrrs[0] = false;
            }

            if (originalMBL.ReleaseType == FCMReleaseType.Unknown)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm()
                  , (LocalData.IsEnglish ? "Release Type is necessary" : "放货类型不能为空"));
                isScrrs[0] = false;
            }

            if (_businessInfo.ETD < originalMBL.GateInDate)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm()
                  , (LocalData.IsEnglish ? "GateInDate greater than ETD" : "您输入的进港日大于离港日."));
                isScrrs[0] = false;
            }

            #endregion

            #region 验证 HBL
            if (!UCHBLList.ValidateData())
            {
                isScrrs[0] = false;
            }

            #endregion

            #region 验证集装箱
            if (!UCBoxList.ValidateData())
            {
                isScrrs[0] = false;
            }

            #endregion

            #region 验证费用
            if (!UCOIOrderFeeEdit.ValidateData())
            {
                isScrrs[0] = false;
            }
            #endregion

            #region 验证PO
            if (UCOIOrderPOItemEdit2 != null && !UCOIOrderPOItemEdit2.ValidateData())
            {
                isScrrs[1] = false;
            }
            #endregion



            bool isScrr = true;
            foreach (var item in isScrrs)
            {
                if (item == false) isScrr = false;
            }

            if (isScrrs[0] == false)
                TabMain.SelectedTabPageIndex = 0;
            else if (isScrrs[1] == false)
                TabMain.SelectedTabPageIndex = 1;


            return isScrr;

        }

        #endregion

        #region 保存

        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                if (Save(_businessInfo, false) == true)
                {
                    SaveOtherPart();

                }
                MethodBase method = MethodBase.GetCurrentMethod();
                StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "VIEW-MBL", string.Format("海进业务编辑保存;MBL No[{0}]", _currentMBLno));
            }
        }

        private bool Save(OceanBusinessInfo currentData, bool isSavingAs)
        {

            if (ValidateData() == false) return false;
            try
            {
                BusinessSaveRequest originalBusiness = SaveBusiness(currentData);

                List<FeeSaveRequest> originalFees = UCOIOrderFeeEdit.BuildFeeList(currentData.ID, Guid.Empty);
                List<POSaveRequest> originalPos = null;
                if (UCOIOrderPOItemEdit2 != null)
                {
                    originalPos = UCOIOrderPOItemEdit2.BuildPoList(currentData.ID, Guid.Empty);
                }

                MBLInfoSaveRequest originalMBL = GetMBLInfo(currentData.ID);
                List<HBLInfoSaveRequest> originalHBLs = UCHBLList.GetHBLSaveInfo();
                List<ContainerSaveRequest> originalcontainers = UCBoxList.GetContainerList();
                Dictionary<Guid, SaveResponse> saved = OceanImportService.SaveOIBusinessWithTrans(
                    originalBusiness,
                    originalMBL,
                    originalHBLs,
                    originalcontainers,
                    originalFees,
                    originalPos);

                if (originalBusiness != null)
                {
                    SaveResponse.Analyze(new List<SaveRequest> { originalBusiness }, saved, true);
                    RefreshUI(originalBusiness);
                }

                if (originalFees != null)
                {
                    SaveResponse.Analyze(originalFees.Cast<SaveRequest>().ToList(), saved, true);
                    UCOIOrderFeeEdit.RefreshUI(originalFees);
                }

                if (originalPos != null)
                {
                    SaveResponse.Analyze(originalPos.Cast<SaveRequest>().ToList(), saved, true);
                    UCOIOrderPOItemEdit2.RefreshUI(originalPos);
                }

                if (originalMBL != null)
                {
                    SaveResponse.Analyze(new List<SaveRequest> { originalMBL }, saved, true);
                    RefreshMBLUI(originalMBL);
                }

                if (originalHBLs != null)
                {
                    SaveResponse.Analyze(originalHBLs.Cast<SaveRequest>().ToList(), saved, true);
                    UCHBLList.RefreshUI(originalHBLs);
                    UCHBLList.BusinessID = _businessInfo.ID;
                }

                if (originalcontainers != null)
                {
                    SaveResponse.Analyze(originalcontainers.Cast<SaveRequest>().ToList(), saved, true);
                    UCBoxList.RefreshUI(originalcontainers);
                    UCBoxList.BusinessID = _businessInfo.ID;
                    UCBoxList.MBLID = _businessInfo.MBLID.ToGuid();

                    //保存箱号与业务的关联
                    List<Guid> idList = UCBoxList.GetRelation();
                    if (idList != null && idList.Count > 0)
                    {
                        OceanImportService.SaveOIContainerAndBusiness(originalBusiness.SingleResult.GetValue<Guid>("ID"), idList.ToArray(), LocalData.UserInfo.LoginID);
                    }
                }

                if (isSavingAs)
                {
                    _businessInfo.IsDirty = false;
                    if (_businessInfo.ShipperDescription != null)
                    {
                        _businessInfo.ShipperDescription.IsDirty = false;
                    }
                    if (_businessInfo.ConsigneeDescription != null)
                    {
                        _businessInfo.ConsigneeDescription.IsDirty = false;
                    }
                    if (_businessInfo.NotifyPartyDescription != null)
                    {
                        _businessInfo.NotifyPartyDescription.IsDirty = false;
                    }
                    if (_businessInfo.AgentDescription != null)
                    {
                        _businessInfo.AgentDescription.IsDirty = false;
                    }
                }
                else
                {
                    AfterSave();
                }
                OiAftertheSave(Ansc);
                RefreshData(_businessInfo.ID, true);
                return true;
            }
            catch (Exception ex)
            {

                if (isSavingAs)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "另存为失败!");
                }

                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return false;
            }
        }
        #region   保存后数据对比
        /// <summary>
        /// 保存后 对数据进行对比进程对应的操作
        /// </summary>
        public void OiAftertheSave(bool ansc)
        {

            StringBuilder content = new StringBuilder();
            int pickUpDateCount = 0;
            int returnDateCount = 0;
            int deliveryTimeCount = 0;
            if (oldBusinessInfo.ETA.ToString() != _businessInfo.ETA.ToString())
            {
                if (ansc)
                {
                    ServiceClient.GetClientService<IClientOceanImportService>().MailEtachange(oldBusinessInfo.ID, null);
                }
            }
            List<OIBusinessContainerList> cargoTrackingContainer = oldBusinessInfo.ContainerList;
            List<OIBusinessContainerList> newCargoTrackingContainerInfos = _businessInfo.ContainerList;

            foreach (OIBusinessContainerList old in cargoTrackingContainer)
            {
                foreach (OIBusinessContainerList news in newCargoTrackingContainerInfos)
                {
                    if (old.PickUpDate != news.PickUpDate)
                    {
                        pickUpDateCount++;
                        Saveevents("FCP", news.No, oldBusinessInfo.ID);
                    }
                    if (old.ReturnDate != news.ReturnDate)
                    {
                        returnDateCount++;
                        Saveevents("ECR", news.No, oldBusinessInfo.ID);

                    }
                    if (old.DeliveryTime != news.DeliveryTime)
                    {
                        deliveryTimeCount++;
                        Saveevents("FCD", news.No, oldBusinessInfo.ID);
                    }
                    if (old.LFDate != news.LFDate)
                    {
                        if (news.LFDate != null)
                        {
                            DateTime LFDate = (DateTime)news.LFDate;
                            content.Append(news.No + ":" + LFDate.ToString("yyyy-MM-dd") + "<br/>");
                        }

                    }
                }
            }
            if (!string.IsNullOrEmpty(content.ToString()))
            {
                if (ansc)
                {
                    ServiceClient.GetClientService<IICPCommonOperationService>().MailLfdnotice(oldBusinessInfo.ID, string.Empty, content.ToString());
                }
            }
            if (pickUpDateCount > 0)
            {
                pickUpDateCount = pickUpDateCount == cargoTrackingContainer.Count ? 3 : 2;
                Updatestate("FCP", pickUpDateCount, oldBusinessInfo.ID);
            }
            if (returnDateCount > 0)
            {
                returnDateCount = returnDateCount == cargoTrackingContainer.Count ? 3 : 2;
                Updatestate("ECR", returnDateCount, oldBusinessInfo.ID);
            }
            if (deliveryTimeCount > 0)
            {
                deliveryTimeCount = deliveryTimeCount == cargoTrackingContainer.Count ? 3 : 2;
                Updatestate("FCD", deliveryTimeCount, oldBusinessInfo.ID);
            }


        }
        /// <summary>
        /// 产生事件
        /// </summary>
        /// <param name="eventsCode">事件Code</param>
        /// <param name="containerNo">箱号</param>
        /// <param name="operationId"></param>
        public void Saveevents(string eventsCode, string containerNo, Guid operationId)
        {
            var memoList = FCMCommonService.GetMemoList(operationId, null).FirstOrDefault(n => n.Code == eventsCode);
            if (memoList == null)
            {
                EventCode eventCode = EventCodeList(eventsCode);
                var eventObjects = new EventObjects
                {
                    OperationID = operationId,
                    OperationType = OperationType.OceanImport,
                    FormID = operationId,
                    FormType = FormType.Unknown,
                    Code = eventCode.Code,
                    Description = "[" + containerNo + "]" + eventCode.Subject,
                    Subject = eventCode.Subject,
                    Priority = MemoPriority.Normal,
                    UpdateDate = DateTime.Now,
                    Owner = LocalData.UserInfo.LoginName,
                    UpdateBy = LocalData.UserInfo.LoginID,
                    CategoryName = eventCode.Category,
                    IsShowAgent = true,
                    IsShowCustomer = true,
                    Type = MemoType.EmailLog
                };
                FCMCommonService.SaveMemoInfo(eventObjects);
            }
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        public void Updatestate(string action, int updateVlues, Guid operationId)
        {
            if (!string.IsNullOrEmpty(action) && updateVlues > 1)
            {
                List<BusinessSaveParameter> listBusinessParameter = new List<BusinessSaveParameter>();
                BusinessSaveParameter parameter = new BusinessSaveParameter();
                parameter["OceanBookingID"] = operationId;
                parameter["OperationType"] = (int)OperationType.OceanImport;
                parameter[action] = updateVlues;
                listBusinessParameter.Add(parameter);
                ServiceClient.GetService<IBusinessQueryService>().Save(listBusinessParameter);
            }
        }
        /// <summary>
        /// 事件集合列表
        /// </summary>
        private List<EventCode> _eventCodeList = new List<EventCode>();
        /// <summary>
        /// 返回当前CODE的事件详细信息
        /// </summary>
        /// <returns></returns>
        public EventCode EventCodeList(string code)
        {
            if (_eventCodeList.Any() == false)
            {
                _eventCodeList = FCMCommonService.GetEventCodeList(OperationType.OceanImport);
            }
            return _eventCodeList.FirstOrDefault(n => n.Code == code);
        }
        #endregion

        private bool SaveOtherPart()
        {
            if (UCBusinessOtherPart.IsChanged || isSaveOperationContact)
            {
                if (_businessInfo != null)
                {
                    //保存联系人列表及附件
                    UCBusinessOtherPart.SetContext = GetContext(_businessInfo);
                    UCBusinessOtherPart.Save(_businessInfo.UpdateDate);
                    UpdateContactControlData();
                    if (Saved != null)
                    {
                        if (_businessOperationParameter == null)
                        {
                            _businessOperationParameter = new BusinessOperationParameter();
                        }
                        _businessOperationParameter.Context = GetContext(_businessInfo);
                        Saved(new object[] { _businessInfo, _businessOperationParameter, _businessOperationParameter.Context });
                    }

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "数据保存成功");
                }
            }
            return true;
        }

        private void UpdateContactControlData()
        {
            if (stxtCustomer.IsContactDataChanged)
            {
                stxtCustomer.ContactList = GetCurrentContactListByCustomerID(_businessInfo.CustomerID, ContactType.Customer);
            }
            if (stxtConsignee.IsContactDataChanged && _businessInfo.ConsigneeID != null)
            {
                stxtConsignee.ContactList = GetCurrentContactListByCustomerID(_businessInfo.ConsigneeID.Value, ContactType.Customer);
            }
            if (stxtAgentOfCarrier.IsContactDataChanged)
            {
                stxtAgentOfCarrier.ContactList = GetCurrentContactListByCustomerID(_businessInfo.MBLInfo.AgentOfCarrierID, ContactType.Customer);
            }
        }

        private List<CustomerCarrierObjects> GetCurrentContactListByCustomerID(Guid customerID, ContactType contactType)
        {
            List<CustomerCarrierObjects> contactList = UCBusinessOtherPart.CurrentContactList.FindAll(item => item.CustomerID == customerID && item.Type == contactType);
            return contactList;
        }

        private void AfterSave()
        {
            _currentMBLno = _businessInfo.MBLInfo.MBLNo;
            _currentCarrierID = _businessInfo.MBLInfo.CarrierID;
            RefreshBarEnabled();
            _businessInfo.BeginEdit();

            //this.stxtCustomer.Properties.Buttons[2].Visible = false;
            gvOrders.DoubleClick -= new EventHandler(gvOrders_DoubleClick);
            //this.stxtCustomer.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.stxtCustomer_ButtonClick);
            barSaveAs.Enabled = true;
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

            _businessInfo.OverSeasFilerName = cmbOverSeasFiler.Text;

            if (!UCBusinessOtherPart.IsChanged && !isSaveOperationContact)
            {
                if (Saved != null)
                {
                    if (_businessOperationParameter == null)
                    {
                        _businessOperationParameter = new BusinessOperationParameter();
                    }
                    _businessOperationParameter.Context = GetContext(_businessInfo);
                    Saved(new object[] { _businessInfo, _businessOperationParameter, _businessOperationParameter.Context });
                }
            }

            _businessInfo.IsDirty = false;
            if (EditMode == EditMode.New)
            {
                EditMode = EditMode.Edit;
            }

            if (_businessInfo.ShipperDescription != null)
            {
                _businessInfo.ShipperDescription.IsDirty = false;
            }
            if (_businessInfo.ConsigneeDescription != null)
            {
                _businessInfo.ConsigneeDescription.IsDirty = false;
            }
            if (_businessInfo.NotifyPartyDescription != null)
            {
                _businessInfo.NotifyPartyDescription.IsDirty = false;
            }
            if (_businessInfo.AgentDescription != null)
            {
                _businessInfo.AgentDescription.IsDirty = false;
            }

            TriggerEventsAtOnce();
            SetTitle();
        }

        void SetTitle()
        {
            if (_businessInfo.ID == Guid.Empty)
            {
                Title = LocalData.IsEnglish ? "Add Business" : "新增业务";
            }
            else
            {
                string titleNo = string.Empty;

                if (_businessInfo.No.Length > 4)
                {
                    titleNo = _businessInfo.No.Substring(_businessInfo.No.Length - 4, 4);
                }
                else
                {
                    titleNo = _businessInfo.No;
                }

                Title = LocalData.IsEnglish ? "Business " + titleNo : "业务：" + titleNo;
            }
        }

        /// <summary>
        /// 构造BusinessOperationContext上下文对象
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        private BusinessOperationContext GetContext(OceanBusinessInfo businessInfo)
        {
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationID = businessInfo.ID;
            context.OperationNO = businessInfo.No;
            context.OperationType = OperationType.OceanImport;
            context.FormId = businessInfo.ID;
            context.FormType = FormType.ShippingOrder;
            context["UpdateDate"] = businessInfo.UpdateDate;
            return context;
        }

        /// <summary>
        /// 获得保存数据实体
        /// </summary>
        /// <param name="currentData"></param>
        private BusinessSaveRequest SaveBusiness(OceanBusinessInfo currentData)
        {
            EndEdit();

            BusinessSaveRequest saveRequest = new BusinessSaveRequest();
            saveRequest.ID = currentData.ID;
            saveRequest.No = currentData.No;

            saveRequest.MBLID = currentData.MBLID.ToGuid();
            saveRequest.CompanyID = currentData.CompanyID;
            saveRequest.OperationType = currentData.OIOperationType;
            saveRequest.CustomerID = currentData.CustomerID;
            saveRequest.CustomerNo = currentData.CustomerNo;
            saveRequest.TradeTermID = currentData.TradeTermID;
            saveRequest.SalesTypeID = currentData.SalesTypeID;
            saveRequest.SalesID = currentData.SalesID;
            saveRequest.SalesDepartmentID = currentData.SalesDepartmentID;
            saveRequest.TransportClauseID = currentData.TransportClauseID;
            saveRequest.FilerId = currentData.FilerId;
            saveRequest.PaymentTermID = currentData.PaymentTermID;
            saveRequest.CustomerServiceID = currentData.CustomerService;
            saveRequest.BookingMode = currentData.BookingMode;
            saveRequest.POLFilerID = currentData.POLFilerID;
            saveRequest.POLFilerName = currentData.POLFilerName;
            saveRequest.BookingDate = currentData.BookingDate;
            saveRequest.OverSeasFilerId = currentData.OverSeasFilerId;
            saveRequest.LocalCSID = currentData.LocalCSId;
            saveRequest.AgentID = currentData.AgentID;
            saveRequest.AgentNo = currentData.AgentNo;
            saveRequest.ShipperID = currentData.ShipperID;
            saveRequest.ShipperDescription = currentData.ShipperDescription;
            saveRequest.ConsigneeID = currentData.ConsigneeID;
            saveRequest.ConsigneeDescription = currentData.ConsigneeDescription;
            saveRequest.NotifyPartyID = currentData.NotifyPartyID;
            saveRequest.NotifyPartyDescription = currentData.NotifyPartyDescription;
            saveRequest.PlaceOfReceiptID = currentData.PlaceOfReceiptID;
            saveRequest.POLID = currentData.POLID;
            saveRequest.PODID = currentData.PODID;
            saveRequest.PlaceOfDeliveryID = currentData.PlaceOfDeliveryID;
            saveRequest.FinalDestinationID = currentData.FinalDestinationID;
            saveRequest.ETD = currentData.ETD;
            saveRequest.ETA = currentData.ETA;
            saveRequest.DETA = currentData.dETA;
            saveRequest.FETA = currentData.FETA;
            saveRequest.IsTruck = currentData.IsTruck;
            saveRequest.IsCustoms = currentData.IsCustoms;
            saveRequest.IsCommodityInspection = currentData.IsCommodityInspection;
            saveRequest.IsQuarantineInspection = currentData.IsQuarantineInspection;
            saveRequest.IsWareHouse = currentData.IsWareHouse;
            //saveRequest.IsTelex = currentData.IsTelex;
            saveRequest.IsReleaseNotify = currentData.IsReleaseNotify;
            saveRequest.IsTransport = currentData.IsTransport;
            saveRequest.Commodity = currentData.Commodity;
            saveRequest.Quantity = currentData.Quantity;
            saveRequest.QuantityUnitID = currentData.QuantityUnitID;
            saveRequest.Weight = currentData.Weight;
            saveRequest.WeightUnitID = currentData.WeightUnitID;
            saveRequest.Measurement = currentData.Measurement;
            saveRequest.MeasurementUnitID = currentData.MeasurementUnitID;
            saveRequest.CargoDescription = currentData.CargoDescription;
            saveRequest.ContainerDescription = currentData.ContainerDescription;
            saveRequest.WareHouseID = currentData.WareHouseID;
            saveRequest.CustomsID = currentData.CustomsID;
            saveRequest.CustomsDescription = currentData.CustomsDescription;
            saveRequest.IsClearance = currentData.IsClearance;
            saveRequest.ClearanceDate = currentData.ClearanceDate;
            saveRequest.ReleaseType = currentData.IsTelex ? FCMReleaseType.Telex : FCMReleaseType.Original;
            saveRequest.ReleaseDate = currentData.ReleaseDate;
            saveRequest.Remark = currentData.Remark;
            saveRequest.saveByID = LocalData.UserInfo.LoginID;
            saveRequest.Updatedate = currentData.UpdateDate;
            saveRequest.GoodDescription = currentData.GoodDescription;
            saveRequest.ClearanceNo = currentData.ClearanceNo;

            saveRequest.FreightIncludedIds = UCOIOrderFeeEdit.SelectChargeCodeIds;

            OceanBusinessMBLList mblList = bsMBLInfo.DataSource as OceanBusinessMBLList;
            if (mblList != null)
            {
                saveRequest.CarrierID = mblList.CarrierID;
            }

            saveRequest.AddInvolvedObject(currentData);
            return saveRequest;
        }

        #region 保存MBL

        public MBLInfoSaveRequest GetMBLInfo(Guid BusinessID)
        {
            OceanBusinessMBLList mblInfo = bsMBLInfo.DataSource as OceanBusinessMBLList;
            if (mblInfo != null && mblInfo.IsDirty)
            {
                MBLInfoSaveRequest mblInfoToSave = new MBLInfoSaveRequest();

                mblInfoToSave.ID = mblInfo.ID;
                mblInfoToSave.UpdateDates = updatedate;
                mblInfoToSave.MBLNo = mblInfo.MBLNo;
                mblInfoToSave.SubNo = mblInfo.SubNo;
                mblInfoToSave.CarrierID = mblInfo.CarrierID;
                mblInfoToSave.AgentOfCarrierID = mblInfo.AgentOfCarrierID;
                mblInfoToSave.VesselID = mblInfo.VoyageID;
                mblInfoToSave.PreVoyageID = mblInfo.PreVoyageID;
                mblInfoToSave.FinalWareHouseID = FinalWareHouseID;
                mblInfoToSave.ReturnLocationID = ReturnLocationID;
                mblInfoToSave.ITNO = mblInfo.ITNO;
                mblInfoToSave.ITDate = mblInfo.ITDate;
                mblInfoToSave.ITPalce = mblInfo.ITPalce;
                mblInfoToSave.ReleaseType = mblInfo.ReleaseType;
                mblInfoToSave.MBLTransportClauseID = mblInfo.MBLTransportClauseID;
                mblInfoToSave.SaveByID = LocalData.UserInfo.LoginID;
                mblInfoToSave.PreVoyageID = mblInfo.PreVoyageID;

                mblInfoToSave.ETD = _businessInfo.ETD;
                mblInfoToSave.ETA = _businessInfo.ETA;
                mblInfoToSave.GateInDate = mblInfo.GateInDate;

                //mblInfoToSave.UpdateDates = mblInfo.UpdateDate;

                mblInfoToSave.AddInvolvedObject(mblInfo);
                return mblInfoToSave;
            }
            else
            {
                return null;
            }
        }

        public void RefreshMBLUI(MBLInfoSaveRequest saveRequest)
        {
            SingleResult result = saveRequest.SingleResult;
            OceanBusinessMBLList currentData = saveRequest.UnBoxInvolvedObject<OceanBusinessMBLList>()[0];
            currentData.ID = result.GetValue<Guid>("ID");
            currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            currentData.IsDirty = false;
        }

        #endregion


        void RefreshUI(BusinessSaveRequest saveRequest)
        {
            SingleResult result = saveRequest.SingleResult;
            OceanBusinessInfo currentData = saveRequest.UnBoxInvolvedObject<OceanBusinessInfo>()[0];
            currentData.ID = result.GetValue<Guid>("ID");
            currentData.No = result.GetValue<string>("NO");
            //currentData.RefNo = result.GetValue<string>("NO");
            currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            currentData.State = (OIOrderState)result.GetValue<byte>("State");
            Ansc = result.GetValue<bool>("ANSC");
        }

        #endregion

        #region 另存为

        /// <summary>
        /// TODO: 不应该用ErrorTrace来实现这个消息。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSaveAs_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (SaveAs())
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save as a new order successfully. Ref. NO. is " + _businessInfo.No + "." : "已成功另存为一票新业务，业务号为" + _businessInfo.No + "。");

                    if (Saved != null)
                    {
                        if (_businessOperationParameter == null)
                        {
                            _businessOperationParameter = new BusinessOperationParameter();
                        }
                        _businessOperationParameter.Context = GetContext(_businessInfo);
                        Saved(new object[] { _businessInfo, _businessOperationParameter, _businessOperationParameter.Context });
                    }
                }
            }
        }

        bool SaveAs()
        {
            if (ValidateData() == false)
            {
                return false;
            }

            if (_currentCarrierID == ConvObjToGuid(mcmbCarrier.EditValue) && _currentMBLno == stxtMBLNo.EditValue.ToString())
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "MBL Same,To Save?" : "船公司和主提单号不能都相同,请先修改船公司或主提单号!", LocalData.IsEnglish ? "Tip" : "提示");
                return false;
            }

            string saveAsMessage = LocalData.IsEnglish ? "Un Done" : "是否另存为一票新的业务?";
            if (!Utility.ShowResultMessage(saveAsMessage))
            {
                return false;
            }

            OceanBusinessInfo businessInfo = Utility.Clone<OceanBusinessInfo>(_businessInfo);
            businessInfo.ID = Guid.Empty;
            businessInfo.No = string.Empty;
            businessInfo.State = OIOrderState.NewOrder;
            businessInfo.CreateID = LocalData.UserInfo.LoginID;
            businessInfo.CreateByName = LocalData.UserInfo.LoginName;
            businessInfo.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            businessInfo.BookingDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            businessInfo.UpdateDate = null;

            businessInfo.IsDirty = true;

            _businessInfo = businessInfo;
            if (UCOIOrderPOItemEdit2 == null)
            {
                InitPOItemEditControl();
                UCOIOrderPOItemEdit2.InitData(_businessInfo.ID);
            }

            if (Save(businessInfo, true))
            {
                RefreshData(businessInfo.ID, true);

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 打回
        /// <summary>
        /// 打回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barReturn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_businessInfo.State == OIOrderState.NewOrder)
            {
                ReturnOrder returnOrder = Workitem.Items.AddNew<ReturnOrder>();

                string title = LocalData.IsEnglish ? "Return Order" : "打回订单";

                if (PartLoader.ShowDialog(returnOrder, title) == DialogResult.OK)
                {
                    SingleResultData result = OceanImportService.ChangeOIOrderState(_businessInfo.ID, OIOrderState.Rejected, returnOrder.ReturnRemark, LocalData.UserInfo.LoginID, _businessInfo.UpdateDate);

                    _businessInfo.State = OIOrderState.Rejected;
                    _businessInfo.UpdateDate = result.UpdateDate;
                    SetState();
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "" : "打回成功");
                    if (Saved != null)
                    {
                        if (_businessOperationParameter == null)
                        {
                            _businessOperationParameter = new BusinessOperationParameter();
                        }
                        _businessOperationParameter.Context = GetContext(_businessInfo);
                        Saved(new object[] { _businessInfo, _businessOperationParameter, _businessOperationParameter.Context });
                    }
                }
            }
        }
        #endregion

        #region 审核并保存

        /// <summary>
        /// 审核并保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAuditor_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!Save(_businessInfo, false))
            {
                return;
            }
            if (_businessInfo.State != OIOrderState.NewOrder)
            {
                return;
            }
            string title = LocalData.IsEnglish ? "Audit Order" : "审核业务";
            string labelText = LocalData.IsEnglish ? "Audit memo" : "审核意见";
            ClientOceanImportService.CommonConfirmForm(labelText, null, title, AfterCheck);
        }

        private void AfterCheck(object[] prams)
        {
            if (prams == null || prams.Length == 0)
            {
                return;
            }
            string memo = prams[0] as string;
            if (string.IsNullOrEmpty(memo))
            {
                return;
            }
            try
            {
                SingleResultData result = OceanImportService.ChangeOIOrderState(_businessInfo.ID, OIOrderState.BookingConfirmed, memo, LocalData.UserInfo.LoginID, _businessInfo.UpdateDate);
                _businessInfo.State = OIOrderState.BookingConfirmed;
                _businessInfo.UpdateDate = result.UpdateDate;
                if (Saved != null)
                {
                    if (_businessOperationParameter == null)
                    {
                        _businessOperationParameter = new BusinessOperationParameter();
                    }
                    _businessOperationParameter.Context = GetContext(_businessInfo);
                    Saved(new object[] { _businessInfo, _businessOperationParameter, _businessOperationParameter.Context });
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Disuse Successfully" : "确认订舱成功!");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        #endregion

        #region  刷新

        /// <summary>
        /// 数据刷新到初始或保存后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                RefreshData(_businessInfo.ID, true);
                if (TabMain.SelectedTabPage == TabPoItem && EditMode != EditMode.New)
                {
                    UCOIOrderPOItemEdit2.InitData(_businessInfo.ID);
                }
                ////this.ShowBusiness();
                ////this.TriggerEventsAtOnce();
                ////this.ResetDescription();
                SetState();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Refresh Successfully" : "刷新成功");
            }
        }

        #endregion

        #region 关闭

        void BusinessBaseEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (IsChanged)
            {
                DialogResult dr = Utility.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!Save(_businessInfo, false))
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        #endregion

        #region 打印报表
        /// <summary>
        /// 打印到港通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barPrintArrivalNotice_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_businessInfo == null || _businessInfo.ID == Guid.Empty) return;

                ClientOceanImportService.MailAnToCustomer(_businessInfo.ID, true);
            }
            //using (new CursorHelper(Cursors.WaitCursor))
            //{
            //    if (_businessInfo == null || _businessInfo.ID == Guid.Empty) return;
            //    if (_businessInfo.IsDirty)
            //    {
            //        DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Current data has changed, want to print please save." : "当前数据有更改,欲打印请先保存.", LocalData.IsEnglish ? "Tip" : "提示");
            //        return;
            //    }

            //    if (_businessInfo.IsSentAN == false && (_businessInfo.SalesID == null || _businessInfo.SalesID == Guid.Empty))
            //    {
            //        if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Sales name is null,and Sales can not receive the Arrival Notice ,Sure to continue?" : "由于未填写业务员,港后通知邮件将无法通知到业务员.是否继续?",
            //                            LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //        {
            //            return;
            //        }
            //    }

            //    if (_businessInfo.CompanyID == new Guid("0501D29D-0EFE-E111-B376-0026551CA87B")) //巴西到港通知
            //    {
            //        ICP.Message.ServiceInterface.Message operationInfo = GetOperationInfo();
            //        OceanImportPrintHelper.PrintArrivalNoticeReportForBrazil(_businessInfo, operationInfo);
            //    }
            //    else
            //    {
            //        Dictionary<string, object> stateValues = new Dictionary<string, object>();
            //        stateValues.Add("OceanBusinessList", _businessInfo);
            //        string no = _businessInfo.No.Length <= 4 ? _businessInfo.No : _businessInfo.No.Substring(_businessInfo.No.Length - 4, 4);
            //        string title = (LocalData.IsEnglish ? "Print Arrival Notice" : "到港通知书") + ("-" + no);
            //        PartLoader.ShowEditPart<OIArrivalNotice2>(Workitem, null, stateValues, title, null, null);
            //    }
            //}
        }

        /// <summary>
        /// 打印放货通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barReleaseOrder_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_businessInfo == null || _businessInfo.ID == Guid.Empty) return;
                if (_businessInfo.IsDirty)
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ? "Current data has changed, want to print please save." : "当前数据有更改,欲打印请先保存.", LocalData.IsEnglish ? "Tip" : "提示");
                    return;
                }

                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                stateValues.Add("OceanBusinessList", _businessInfo);
                string no = _businessInfo.No.Length <= 4 ? _businessInfo.No : _businessInfo.No.Substring(_businessInfo.No.Length - 4, 4);
                string title = (LocalData.IsEnglish ? "Print Release Order" : "放货通知书") + ("-" + no);
                PartLoader.ShowEditPart<OIReleaseOrder2>(Workitem, null, stateValues, title, null, null);
            }
        }

        private void barPrintProfit_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_businessInfo == null || _businessInfo.ID == Guid.Empty) return;
                if (_businessInfo.IsDirty)
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ? "Current data has changed, want to print please save." : "当前数据有更改,欲打印请先保存.", LocalData.IsEnglish ? "Tip" : "提示");
                    return;
                }


                Message.ServiceInterface.Message operationInfo = GetOperationInfo();
                OceanImportPrintHelper.PrintProfit(_businessInfo, operationInfo);
            }
        }

        private void barPrintWorkSheet_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_businessInfo == null || _businessInfo.ID == Guid.Empty) return;
                if (_businessInfo.IsDirty)
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ? "Current data has changed, want to print please save." : "当前数据有更改,欲打印请先保存.", LocalData.IsEnglish ? "Tip" : "提示");
                    return;
                }

                Message.ServiceInterface.Message operationInfo = GetOperationInfo();
                OceanImportPrintHelper.PrintWorkSheet(_businessInfo, operationInfo);
            }
        }

        /// <summary>
        /// 账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBill_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_businessInfo == null || _businessInfo.ID == Guid.Empty) return;

                if (_businessInfo.MBLID == null || _businessInfo.MBLID.ToGuid() == Guid.Empty)
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please save MBL info" : "请先保存MBL信息");
                    return;
                }

                OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(_businessInfo.ID, OperationType.OceanImport);
                if (operationCommonInfo != null)
                {
                    operationCommonInfo.CurrentFormID = _businessInfo.MBLID.ToGuid();
                    FinanceClientService.ShowBillList(operationCommonInfo, ClientConstants.MainWorkspace);
                }
                else
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }
            }
        }

        /// <summary>
        /// 打印货代提单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barPrintForwardingBill_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_businessInfo == null || _businessInfo.ID == Guid.Empty) return;
                if (_businessInfo.IsDirty)
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ? "Current data has changed, want to print please save." : "当前数据有更改,欲打印请先保存.", LocalData.IsEnglish ? "Tip" : "提示");
                    return;
                }

                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                stateValues.Add("OceanBusinessList", _businessInfo);
                string no = _businessInfo.No.Length <= 4 ? _businessInfo.No : _businessInfo.No.Substring(_businessInfo.No.Length - 4, 4);
                string title = (LocalData.IsEnglish ? "Print Forwarding Bill" : "货代提单") + ("-" + no);
                PartLoader.ShowEditPart<OIBLPrintPart2>(Workitem, null, stateValues, title, null, null);
            }
        }

        private Message.ServiceInterface.Message GetOperationInfo()
        {
            if (_businessInfo == null)
                return null;
            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanImport;
            message.UserProperties.OperationId = _businessInfo.ID;
            message.UserProperties.FormType = FormType.Booking;
            message.UserProperties.FormId = _businessInfo.ID;

            return message;
        }

        #endregion

        #region 发送到港通知书
        /// <summary>
        /// 发送到港通知书给客户(中文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barMailA_NToChs_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_businessInfo == null || _businessInfo.ID == Guid.Empty) return;

                ClientOceanImportService.MailAnToCustomer(_businessInfo.ID, false);
            }
        }

        /// <summary>
        /// 发送到港通知书给客户(英文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barMailA_NToEng_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_businessInfo == null || _businessInfo.ID == Guid.Empty) return;

                ClientOceanImportService.MailAnToCustomer(_businessInfo.ID, true);
            }
        }

        #endregion

        #endregion

        #region 方法和事件

        private void navBarGroupControlContainer2_Click(object sender, EventArgs e)
        {

        }

        private void chkIsWarehouse_CheckedChanged(object sender, EventArgs e)
        {
            cmbWareHouse.Properties.ReadOnly = !chkIsWarehouse.Checked;
            if (chkIsWarehouse.Checked)
            {
                cmbWareHouse.BackColor = SystemColors.Info;
            }
            else
            {
                cmbWareHouse.BackColor = Color.White;
            }
        }

        private void UCBoxList_UpdateTotalQWM(object sender, object data)
        {
            int qty = 0;
            decimal weight = 0m, measurement = 0m;
            foreach (var item in UCBoxList.DataSource)
            {
                if (item.IsRelation == false) continue;

                qty += (item.Quantity == null ? 0 : item.Quantity.Value);
                weight += (item.Weight == null ? 0m : item.Weight.Value);
                measurement += (item.Measurement == null ? 0m : item.Measurement.Value);
            }

            numQuantity.Value = _businessInfo.Quantity = qty;
            numWeight.Value = _businessInfo.Weight = weight;
            numMeasurement.Value = _businessInfo.Measurement = measurement;
        }

        #endregion

        #region 资源回收

        void OIBusinessEdit_Load(object sender, EventArgs e)
        {
            ///设置客户水印
            ///
            Utility.SetCustomerTextEditNullValuePrompt(new List<TextEdit>
            {
                //stxtCustomer,
                stxtConsignee,
                stxtShipper,
                stxtNotifyParty,
                stxtAgentOfCarrier,
                stxtFinalWareHouse,
                stxtReturnLocation,
                cmbWareHouse,
                stxtCustoms
            });

            //设置地点水印
            Utility.SetPortTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtPlaceOfDelivery,
                stxtPlaceOfReceipt,
                stxtFinalDestination,
                stxtPOL,
                stxtPOD,
            });

            //设置船名航次水印
            //Utility.SetVoyageTextEditNullValuePrompt(new List<TextEdit>
            //{
            //    stxtPreVoyage,
            //    stxtVoyage
            //});


            SmartPartClosing += new EventHandler<WorkspaceCancelEventArgs>(BusinessBaseEditPart_SmartPartClosing);
            ActivateSmartPartClosingEvent(Workitem);


            bsBusiness.BindingComplete += new BindingCompleteEventHandler(bsBusiness_BindingComplete);

            //越南公司只能编辑箱的毛件体，总值不给编辑
            if (LocalData.UserInfo.DefaultCompanyID == _VietnamCompanyId)
            {
                numQuantity.Properties.ReadOnly = numWeight.Properties.ReadOnly = numMeasurement.Properties.ReadOnly = true;
            }
        }

        void bsBusiness_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            _shown = true;
            bsBusiness.BindingComplete -= new BindingCompleteEventHandler(bsBusiness_BindingComplete);
        }

        bool _shown = false;

        /// <summary>
        /// 从工作项集合中移除自己
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OIBusinessEdit_Disposed(object sender, EventArgs e)
        {
            if (Workitem != null)
            {
                Workitem.Items.Remove(this);
                Workitem = null;
            }
        }

        /// <summary>
        /// 防闪烁
        /// </summary>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x0014) return;// 禁掉清除背景消息

            base.WndProc(ref m);
        }

        #endregion

        /// <summary>
        /// 页签面板改变时，动态加载PO面板控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (e.Page == TabPoItem)
            {
                if (UCOIOrderPOItemEdit2 == null)
                {
                    InitPOItemEditControl();

                    UCOIOrderPOItemEdit2.InitData(_businessInfo.ID);
                    //this.UCOIOrderPOItemEdit2.SetSource();

                }

            }
        }

        private void InitPOItemEditControl()
        {
            UCOIOrderPOItemEdit2 = Workitem.Items.AddNew<POItemEditPart>();
            //暂且这样处理，后续统一调整
            if (LocalData.IsEnglish == false)
            {

                UCOIOrderPOItemEdit2.SetCnText();
            }
            UCOIOrderPOItemEdit2.IsOrderPO = true;
            UCOIOrderPOItemEdit2.Dock = DockStyle.Fill;
            TabPoItem.Controls.Add(UCOIOrderPOItemEdit2);
        }

        private void OIBusinessEdit_Closing(object sender, FormClosingEventArgs e)
        {
            stxtMBLNo.Leave -= new EventHandler(stxtMBLNo_Leave);
        }

        private void barUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {

            UpdateETAInfo updateETAinfo = new UpdateETAInfo();

            updateETAinfo.VoyageID = _businessInfo.MBLInfo.VoyageID;
            updateETAinfo.CompanyID = _businessInfo.CompanyID;

            FCM.Common.UI.FCMUIUtility.ShowUpdateETA(Workitem, updateETAinfo, null, null);

        }

        private void stxtFinalWareHouse_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(stxtFinalWareHouse.Text))
            {
                FinalWareHouseID = null;
            }

        }

        private void stxtReturnLocation_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(stxtReturnLocation.Text))
            {
                ReturnLocationID = null;
            }
        }

        private void txtContractNo_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            SelectContract();
        }

        private void txtContractNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //SelectContract();
            }
        }

        private void txtContractNo_Click(object sender, EventArgs e)
        {
            SelectContract();
        }

        private void SelectContract()
        {
            ClientOceanExportService.SelectContract((Guid)_businessInfo.MBLInfo.FreightRateID);
        }


    }
}
