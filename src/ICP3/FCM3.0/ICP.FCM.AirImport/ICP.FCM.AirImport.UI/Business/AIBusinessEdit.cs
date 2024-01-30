using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.FAM.ServiceInterface;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.FCM.AirImport.UI.Report;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using ICP.OA.ServiceInterface.DataObjects;

namespace ICP.FCM.AirImport.UI
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class OIBusinessEdit : BaseEditPart
    { 
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IDataFindClientService dfService { get; set; }

        [ServiceDependency]
        public ITransportFoundationService tfService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IGeographyService geographyService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ICustomerService customerService { get; set; }

        [ServiceDependency]
        public IAirImportService oiService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }


        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelperService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IConfigureService configureService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }

        [ServiceDependency]
        public IFinanceClientService finClientService { get; set; }

        [ServiceDependency]
        public IAIReportDataService OIReportSrvice { get; set; }

        [ServiceDependency]
        public IReportViewService ReportViewService { get; set; }

        #endregion

        #region 本地变量

        CustomerType customerType = CustomerType.Unknown;

        List<CountryList> _countryList = null;

        AirBusinessInfo _businessInfo = null;
        string _currentMBLno = string.Empty;
        Guid? _currentAirCompanyID = null;

        List<AirBusinessMBLList> _mblNoList = new List<AirBusinessMBLList>();

        AirOrderInfo CurrentBusinessList
        {
            get
            {
                if (bsRecentTenOrders.List == null || bsRecentTenOrders.Current == null) return null;
                return bsRecentTenOrders.Current as AirOrderInfo;
            }
        }

        /// <summary>
        /// 是否有数据发生改变
        /// </summary>
        private bool IsChanged
        {
            get
            {
                bool isCharge = false;
                if (_businessInfo.IsDirty
                    || this._businessInfo.MBLInfo.IsDirty
                    || this.UCHBLList.IsChanged
                    || this.UCOIOrderFeeEdit.IsChanged)
                {
                    isCharge = true;
                }


                return isCharge;
            }
        }

        #endregion

        #region 构造函数

        public OIBusinessEdit()
        {
            InitializeComponent();
            if (DesignMode)
            {
                return;
            }

            if (LocalData.IsEnglish == false)
            {
                //SetCnText();
            }

            barBill.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barBill_ItemClick);

            barPrintArrivalNotice.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barPrintArrivalNotice_ItemClick);
            barReleaseOrder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barReleaseOrder_ItemClick);
            barPrintProfit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barPrintProfit_ItemClick);
            barPrintAuthority.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barPrintAuthority_ItemClick);

            this.Disposed += new EventHandler(OIBusinessEdit_Disposed);
            this.Load += new EventHandler(OIBusinessEdit_Load);
        }

        #endregion

        #region IEditPart 成员

        void RefreshData(Guid orderId, bool isShowBusiness)
        {
            this.GetData(orderId);
            if (isShowBusiness)
            {
                this.ShowBusiness();
            }

            this.TriggerEventsAtOnce();
            this.ResetDescription();
            this.SetTitle();
            _currentMBLno = this._businessInfo.MBLInfo.MBLNo;
            _currentAirCompanyID = this._businessInfo.MBLInfo.AirCompanyID;
        }

        void GetData(Guid businessId)
        {
            this._businessInfo = oiService.GetBusinessInfoByEdit(businessId);
        }

        void ShowBusiness()
        {
            InitData(_businessInfo);

            this.bsBusiness.DataSource = _businessInfo;
            this.bsBusiness.ResetBindings(false);
            if (_businessInfo.MBLInfo.ID != null)
            {
                this.bsMBLInfo.DataSource = _businessInfo.MBLInfo;
                this.bsMBLInfo.ResetBindings(false);

            }

            InitControls();

            List<AirImportFeeList> feelist = null;
            List<AirBusinessHBLList> hBLList = null;

            if (_businessInfo.ID == Guid.Empty)
            {
                feelist = new List<AirImportFeeList>();
                hBLList = new List<AirBusinessHBLList>();
            }
            else
            {
                feelist = _businessInfo.FeeList;
                hBLList = _businessInfo.HBLList;
            }

            if (Utility.GuidIsNullOrEmpty(_businessInfo.CustomerID) == false)
            {
                this.UCOIOrderFeeEdit.DefaultCustomerID = _businessInfo.CustomerID;
                this.UCOIOrderFeeEdit.DefaultCustomerName = _businessInfo.CustomerName;
            }

            this.UCOIOrderFeeEdit.SetSource(feelist);
            this.UCHBLList.BindHBLList(hBLList);
            this.UCHBLList.BusinessID = _businessInfo.ID;
        }

        void BindingData(object data)
        {
            this.SuspendLayout();

            //HBL信息服务
            UCHBLList.SetService(Workitem);

            this.UCOIOrderFeeEdit.SetService(Workitem);

            AirBusinessList listInfo = data as AirBusinessList;
            AirBusinessInfo info = data as AirBusinessInfo;

            if (info != null)
            {
                if (info.ID == Guid.Empty)
                {
                    _businessInfo = info;
                }
                else
                {
                    //this.RefreshData(info.ID);
                    this.GetData(info.ID);
                }
            }
            else if (listInfo == null)
            {
                this._businessInfo = new AirBusinessInfo();
                this.ReadyForNew();
            }
            else
            {
                this.RefreshData(listInfo.ID, false);
            }

            this.ShowBusiness();

            SearchRegister();

            this.SetLazyLoaders();

            this.SetLazyDataLodersWithDynamicCondition();

            this.ResumeLayout();
        }

        public override object DataSource
        {
            get { return bsBusiness.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return this.Save(this._businessInfo, false);
        }

        public override void EndEdit()
        {
            // cmbCargoType.ClosePopup();
            this.Validate();
            bsBusiness.EndEdit();
            bsMBLInfo.EndEdit();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region 新业务的逻辑/默认值

        void ReadyForNew()
        {
            if (this._businessInfo.ID == Guid.Empty)
            {
                AirBusinessInfo newData = new AirBusinessInfo();
                newData.State = AIOrderState.NewOrder;
                newData.CreateID = LocalData.UserInfo.LoginID;
                newData.CreateByName = LocalData.UserInfo.LoginName;
                newData.CreateDate = DateTime.Now;
                newData.BookingDate = DateTime.Now;
                newData.BookingMode = FCMBookingMode.EMail;
                newData.IsValid = true;

                #region 设置默认值

                DataDictionaryList normalDictionary = null;
                normalDictionary = ICPCommUIHelperService.GetNormalDictionary(DataDictionaryType.PaymentTerm);
                newData.PaymentTermID = normalDictionary.ID;
                newData.PaymentTermName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                normalDictionary = ICPCommUIHelperService.GetNormalDictionary(DataDictionaryType.QuantityUnit);
                newData.QuantityUnitID = normalDictionary.ID;
                newData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                normalDictionary = ICPCommUIHelperService.GetNormalDictionary(DataDictionaryType.WeightUnit);
                newData.WeightUnitID = normalDictionary.ID;
                newData.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                normalDictionary = ICPCommUIHelperService.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
                newData.MeasurementUnitID = normalDictionary.ID;
                newData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                #endregion

                this._businessInfo = newData;

                barSaveAs.Enabled = false;
                this.stxtCustomer.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.stxtCustomer_ButtonClick);
                this.gvOrders.DoubleClick += new System.EventHandler(this.gvOrders_DoubleClick);

                Utility.EnsureDefaultCompanyExists(this.userService);
                Utility.EnsureDefaultDepartmentExists(this.userService);


                this._businessInfo.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                this._businessInfo.CompanyName = LocalData.UserInfo.DefaultCompanyName;


                this._businessInfo.FilerId = LocalData.UserInfo.LoginID;
                this._businessInfo.FilerName = LocalData.UserInfo.LoginName;
            }
            else
            {
                this.stxtCustomer.Properties.Buttons[2].Visible = false;
                this.stxtCustomer.Properties.Buttons[2].Visible = false;
                this.gvOrders.DoubleClick -= new System.EventHandler(this.gvOrders_DoubleClick);
                this.stxtCustomer.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.stxtCustomer_ButtonClick);
            }
        }

        #endregion

        #region 不能为空的一些属性和数据

        /// <summary>
        /// 不能为空的一些属性和数据
        /// </summary>
        /// <param name="info"></param>
        private void InitData(AirBusinessInfo info)
        {
            if (!Utility.GuidIsNullOrEmpty(info.ID)
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

            if (Utility.GuidIsNullOrEmpty(info.ID))
            {
                info.BookingDate = DateTime.Today;
            }

            if (this._businessInfo.ConsigneeDescription == null)
            {
                this._businessInfo.ConsigneeDescription = new CustomerDescription();
            }
            if (this._businessInfo.ShipperDescription == null)
            {
                this._businessInfo.ShipperDescription = new CustomerDescription();
            }
            if (this._businessInfo.NotifyPartyDescription == null)
            {
                this._businessInfo.NotifyPartyDescription = new CustomerDescription();
            }
            if (this._businessInfo.AgentDescription == null)
            {
                this._businessInfo.AgentDescription = new CustomerDescription();
            }
            if (this._businessInfo.MBLInfo == null)
            {
                this._businessInfo.MBLInfo = new AirBusinessMBLList();
            }
        }

        #endregion

        #region 初始化下拉控件

        /// <summary>
        /// 初始化下拉控件
        /// </summary>
        private void InitControls()
        {
            this.SetState();
            if (this._businessInfo.State == AIOrderState.Release)
            {
                ckbIsTelex.Properties.ReadOnly = true;
            }
            else
            {
                ckbIsTelex.Properties.ReadOnly = false;
            }

            DevHelper.FormatSpinEditForInteger(this.numQuantity);
            DevHelper.FormatSpinEdit(this.numWeight, 3);
            DevHelper.FormatSpinEdit(this.numMeasurement, 3);

            //操作口岸
            this.cmbCompany.ShowSelectedValue(this._businessInfo.CompanyID, this._businessInfo.CompanyName);
            this.treeBoxSalesDep.ShowSelectedValue(this._businessInfo.SalesDepartmentID, this._businessInfo.SalesDepartmentName);

            UCOIOrderFeeEdit.SetCompanyID(this._businessInfo.CompanyID);

            //运输条款
            this.cmbTransportClause.ShowSelectedValue(this._businessInfo.TransportClauseID, this._businessInfo.TransportClauseName);

            this.cmbTradeTerm.ShowSelectedValue(this._businessInfo.TradeTermID, this._businessInfo.TradeTermName);
            this.cmbQuantityunit.ShowSelectedValue(this._businessInfo.QuantityUnitID, this._businessInfo.QuantityUnitName);
            this.cmbMeasurementUnit.ShowSelectedValue(this._businessInfo.MeasurementUnitID, this._businessInfo.MeasurementUnitName);
            this.cmbWeightUnit.ShowSelectedValue(this._businessInfo.WeightUnitID, this._businessInfo.WeightUnitName);

            //付款方式
            this.cmbPaymentTerm.ShowSelectedValue(this._businessInfo.PaymentTermID, this._businessInfo.PaymentTermName);

            //货物类型
            if (this._businessInfo.CargoType.HasValue)
            {
                this.cmbCargoType.ShowSelectedValue(this._businessInfo.CargoType,
                    EnumHelper.GetDescription<CargoType>(this._businessInfo.CargoType.Value, LocalData.IsEnglish));
            }

            //揽货类型
            this.cmbSalesType.ShowSelectedValue(this._businessInfo.SalesTypeID, this._businessInfo.SalesTypeName);

            //揽货人
            this.cmbSales.ShowSelectedValue(this._businessInfo.SalesID, this._businessInfo.SalesName);

            //委托方式
            this.cmbBookingMode.ShowSelectedValue(this._businessInfo.BookingMode,
                EnumHelper.GetDescription<FCMBookingMode>(this._businessInfo.BookingMode, LocalData.IsEnglish));

            //////主提单号
            ////this.stxtMBLNo.ShowSelectedValue(this._businessInfo.MBLInfo.ID, this._businessInfo.MBLInfo.MBLNo);

            InitMBLControl();

            #region CustomerDescription/CargoDescription/ContainerDescription

            if (this._businessInfo.CargoDescription != null && this._businessInfo.CargoDescription.Cargo != null)
            {
                txtCargoDescription.Text = this._businessInfo.CargoDescription.Cargo.ToString(LocalData.IsEnglish);
            }

            #endregion
            this.cmbFile.ShowSelectedValue(this._businessInfo.FilerId, this._businessInfo.FilerName);
            this.cmbCustomerService.ShowSelectedValue(this._businessInfo.customerService, this._businessInfo.CustomerServiceName);
        }

        private void InitMBLControl()
        {
            if (this._businessInfo.State == AIOrderState.Release)
            {
                cmbReleaseType.Properties.ReadOnly = true;
            }
            else
            {
                cmbReleaseType.Properties.ReadOnly = false;
            }

            //航空公司
            this.mcmbAirCompany.ShowSelectedValue(this._businessInfo.MBLInfo.AirCompanyID, this._businessInfo.MBLInfo.AirCompanyName);

            if (this._businessInfo.MBLInfo.FlightID != null)
                cmbFlightNo.ShowSelectedValue(this._businessInfo.MBLInfo.FlightID, this._businessInfo.MBLInfo.FlightNo);

            //放货类型
            this.cmbReleaseType.ShowSelectedValue(this._businessInfo.MBLInfo.ReleaseType,
                EnumHelper.GetDescription<FCMReleaseType>(this._businessInfo.MBLInfo.ReleaseType, LocalData.IsEnglish));

            this.UCHBLList.SetReceive(this._businessInfo.MBLInfo.ReleaseType);

            //MBL运输条款
            if (Utility.GuidIsNullOrEmpty(_businessInfo.ID))
            {
                List<TransportClauseList> transportClauses = ICPCommUIHelperService.SetCmbTransportClause(cmbMBLTransportClause);
                if (transportClauses != null && transportClauses.Count > 0)
                {
                    _businessInfo.MBLInfo.MBLTransportClauseID = transportClauses[0].ID;
                }
            }
            else
            {
                this.cmbMBLTransportClause.ShowSelectedValue(this._businessInfo.MBLInfo.MBLTransportClauseID, this._businessInfo.MBLInfo.MBLTransportClauseName);
            }
        }

        #endregion

        #region 注册搜索器

        CustomerFinderBridge shipperBridge;
        CustomerFinderBridge consigneeBridge;
        CustomerFinderBridge notifyPartyBridge;

        /// <summary>
        /// 注册搜索器
        /// </summary>
        void SearchRegister()
        {
            #region Customer

            //Customer
            dfService.Register(stxtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                ICP.FCM.AirImport.UI.Common.SearchConstants.CustomerResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          Guid oldCustomerId = _businessInfo.CustomerID;
                          stxtCustomer.ClosePopup();

                          CustomerStateType state = (CustomerStateType)resultData[7];
                          if (state == CustomerStateType.Invalid)
                          {
                              if (!Utility.PopCustomerIsInvalid())
                              {
                                  return;
                              }
                          }

                          //CustomerCodeApplyState? approved = (CustomerCodeApplyState?)resultData[8];
                          //if (!approved.HasValue
                          //    || (approved.HasValue && approved.Value != CustomerCodeApplyState.Passed))
                          //{
                          //    if (!Utility.PopCustomerUnApproved())
                          //    {
                          //        return;
                          //    }
                          //} 
                          CustomerCodeApplyState? approved = (CustomerCodeApplyState?)resultData[8];
                          if (!approved.HasValue
                              || (approved.HasValue && approved.Value != CustomerCodeApplyState.Passed))
                          {
                              if (approved.Value == CustomerCodeApplyState.Processing)
                              {
                                  DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The customers has not been approved!" : "该客户尚未通过审核!"
                   , LocalData.IsEnglish ? "Tip" : "提示"
                   , MessageBoxButtons.OK
                   , MessageBoxIcon.Question);

                                  return;
                              }
                              else if (approved.Value == CustomerCodeApplyState.UnApply)
                              {
                                  if ((resultData[10] == null ||
                                      string.IsNullOrEmpty(resultData[10].ToString())) &&
                                      (resultData[11] == null ||
                                      string.IsNullOrEmpty(resultData[11].ToString())))
                                  {
                                      DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.OK
                  , MessageBoxIcon.Question);
                                      return;
                                  }

                                  DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The customer have not yet applied for the code. Whether to apply the code?" : "该客户尚未申请代码，是否要申请代码?"
              , LocalData.IsEnglish ? "Tip" : "提示"
              , MessageBoxButtons.YesNo
              , MessageBoxIcon.Question);
                                  if (result == DialogResult.Yes)
                                  {
                                      customerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                        LocalData.UserInfo.LoginID,
                                                                        LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                        (DateTime?)resultData[9]);
                                  }

                                  return;
                              }
                              else if (approved.Value == CustomerCodeApplyState.Unpassed)
                              {
                                  if ((resultData[10] == null ||
                                      string.IsNullOrEmpty(resultData[10].ToString())) &&
                                      (resultData[11] == null ||
                                      string.IsNullOrEmpty(resultData[11].ToString())))
                                  {
                                      DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.OK
                  , MessageBoxIcon.Question);
                                      return;
                                  }

                                  DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show("该客户尚未通过审核，若重新申请代码需要去完善客户资料。是否重新申请代码?"
              , LocalData.IsEnglish ? "Tip" : "提示"
              , MessageBoxButtons.YesNo
              , MessageBoxIcon.Question);
                                  if (result == DialogResult.Yes)
                                  {
                                      customerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                        LocalData.UserInfo.LoginID,
                                                                        LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                        (DateTime?)resultData[9]);
                                  }

                                  return;
                              }
                          }

                          if (resultData[4] != null)
                          {
                              this.customerType = (CustomerType)resultData[4];
                          }

                          stxtCustomer.Tag = _businessInfo.CustomerID = new Guid(resultData[0].ToString());
                          stxtCustomer.EditValue = _businessInfo.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();

                          if (resultData[5] != null
                              && (Guid)resultData[5] != Guid.Empty
                              && resultData[6] != null)
                          {
                              this.cmbTradeTerm.ShowSelectedValue(resultData[5], resultData[6].ToString());
                          }

                          if (oldCustomerId != Guid.Empty && _businessInfo.CustomerID == oldCustomerId)
                          {
                              return;
                          }

                          CustomerChanged(customerType);


                      }, delegate
                      {
                          stxtCustomer.Text = _businessInfo.CustomerName = string.Empty;
                          stxtCustomer.Tag = _businessInfo.CustomerID = Guid.Empty;
                          stxtCustomer.ClosePopup();
                          CustomerChanged(null);
                      },
                      ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            //仓库
            this.dfService.Register(this.cmbWareHouse, CommonFinderConstants.CustoemrFinder,
                SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                this.GetConditionsForWarehouse,
                delegate(object inputSource, object[] resultData)
                {
                    cmbWareHouse.Tag = this._businessInfo.WareHouseID = (Guid)resultData[0];
                    cmbWareHouse.EditValue = this._businessInfo.WareHouseName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate
                {
                    cmbWareHouse.Tag = this._businessInfo.WareHouseID = null;
                    cmbWareHouse.EditValue = this._businessInfo.WareHouseName = string.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            //报关行
            this.dfService.Register(this.cmbCustoms, CommonFinderConstants.CustoemrFinder,
                SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                this.GetConditionsForCustoms,
                delegate(object inputSource, object[] resultData)
                {
                    cmbCustoms.Tag = this._businessInfo.CustomsID = (Guid)resultData[0];
                    cmbCustoms.EditValue = this._businessInfo.CustomsName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate
                {
                    cmbCustoms.Tag = this._businessInfo.CustomsID = null;
                    cmbCustoms.EditValue = this._businessInfo.CustomsName = string.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            //MBL提货地
            this.dfService.Register(this.stxtFinalWareHouse, CommonFinderConstants.CustoemrFinder,
                SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                GetConditionsForFinalWareHouse,
                delegate(object inputSouce, object[] resultData)
                {
                    stxtFinalWareHouse.Tag = this._businessInfo.MBLInfo.FinalWareHouseID = (Guid)resultData[0];
                    stxtFinalWareHouse.EditValue = this._businessInfo.MBLInfo.FinalWareHouseName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate
                {
                    this.stxtFinalWareHouse.Tag = this._businessInfo.MBLInfo.FinalWareHouseID = Guid.Empty;
                    this.stxtFinalWareHouse.EditValue = this._businessInfo.MBLInfo.FinalWareHouseName = string.Empty;
                },
            ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            //MBL承运人
            dfService.Register(stxtAgentOfCarrier, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
             delegate(object inputSource, object[] resultData)
             {
                 stxtAgentOfCarrier.Text = _businessInfo.MBLInfo.AgentOfCarrierName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                 stxtAgentOfCarrier.Tag = _businessInfo.MBLInfo.AgentOfCarrierID = new Guid(resultData[0].ToString());
             },
             delegate()
             {
                 stxtAgentOfCarrier.Text = string.Empty;
                 stxtAgentOfCarrier.Tag = Guid.Empty;
             }
             , ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            #endregion

            #region SCNA

            //shipper
            Utility.SetEnterToExecuteOnec(stxtShipper, delegate
            {
                if (_countryList == null) _countryList = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                shipperBridge = new CustomerFinderBridge(
               this.stxtShipper,
               _countryList,
               this.dfService,
               this.customerService,
               this._businessInfo.ShipperDescription,
               ICPCommUIHelperService,
               LocalData.IsEnglish);
                shipperBridge.Init();
            });

            stxtShipper.OnOk += new EventHandler(stxtShipper_OnOk);

            //Consignee
            Utility.SetEnterToExecuteOnec(stxtConsignee, delegate
            {
                if (_countryList == null) _countryList = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                consigneeBridge = new CustomerFinderBridge(
                this.stxtConsignee,
                _countryList,
                this.dfService,
                this.customerService,
                this._businessInfo.ConsigneeDescription,
                ICPCommUIHelperService,
                LocalData.IsEnglish);
                consigneeBridge.Init();
            });

            stxtConsignee.OnOk += new EventHandler(stxtConsignee_OnOk);

            //NotifyParty
            Utility.SetEnterToExecuteOnec(stxtNotifyParty, delegate
            {
                if (_countryList == null) _countryList = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                notifyPartyBridge = new CustomerFinderBridge(
                this.stxtNotifyParty,
                _countryList,
                this.dfService,
                this.customerService,
                this._businessInfo.NotifyPartyDescription,
                ICPCommUIHelperService,
                LocalData.IsEnglish);
                notifyPartyBridge.Init();
            });

            stxtNotifyParty.OnOk += new EventHandler(stxtNotifyParty_OnOk);

            #endregion

            //#region Port


            //PortFinderBridge pfbPOL = new PortFinderBridge(this.stxtDeparture, this.dfService, true);

            //PortFinderBridge pfbPOD = new PortFinderBridge(this.stxtDetination, this.dfService, true);

            //LocationFinderBridge pfbPlaceOfDelivery = new LocationFinderBridge(this.stxtPlaceOfDelivery, this.dfService, true);

            //#endregion

            #region Port

            #region POL

            dfService.Register(stxtDeparture, CommonFinderConstants.AirLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid portID = new Guid(resultData[0].ToString());
                      if (_businessInfo.POLID != portID)
                      {
                          stxtDeparture.Tag = _businessInfo.POLID = portID;
                          stxtDeparture.Text = _businessInfo.POLName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                      }
                  },
                  delegate
                  {
                      stxtDeparture.Tag = _businessInfo.POLID = Guid.Empty;
                      stxtDeparture.Text = _businessInfo.POLName = string.Empty;
                  },
                  ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion
            #region POD
            dfService.Register(stxtDetination, CommonFinderConstants.AirLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid portID = new Guid(resultData[0].ToString());
                      if (_businessInfo.PODID != portID)
                      {
                          stxtDetination.Tag = _businessInfo.PODID = portID;
                          stxtDetination.Text = _businessInfo.PODName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                      }
                  },
                  delegate
                  {
                      stxtDetination.Tag = _businessInfo.PODID = Guid.Empty;
                      stxtDetination.Text = _businessInfo.PODName = string.Empty;
                  },
                  ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion
            #region PlaceOfDelivery
            dfService.Register(stxtPlaceOfDelivery, CommonFinderConstants.AirLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      stxtPlaceOfDelivery.Tag = _businessInfo.PlaceOfDeliveryID = new Guid(resultData[0].ToString());
                      stxtPlaceOfDelivery.Text = _businessInfo.PlaceOfDeliveryName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                  },
                  delegate
                  {
                      stxtPlaceOfDelivery.Tag = _businessInfo.PlaceOfDeliveryID = Guid.Empty;
                      stxtPlaceOfDelivery.Text = _businessInfo.PlaceOfDeliveryName = string.Empty;
                  },
                  ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion
            #endregion
        }

        void stxtNotifyParty_OnOk(object sender, EventArgs e)
        {
            if (stxtNotifyParty.CustomerDescription != null)
            {
                _businessInfo.NotifyPartyDescription = stxtNotifyParty.CustomerDescription;
            }
        }

        void stxtConsignee_OnOk(object sender, EventArgs e)
        {
            if (stxtConsignee.CustomerDescription != null)
            {
                _businessInfo.ConsigneeDescription = stxtConsignee.CustomerDescription;
            }
        }

        void stxtShipper_OnOk(object sender, EventArgs e)
        {
            if (stxtShipper.CustomerDescription != null)
            {
                _businessInfo.ShipperDescription = stxtShipper.CustomerDescription;
            }
        }

        void ResetDescription()
        {
            if (this.shipperBridge != null)
            {
                this.shipperBridge.SetCustomerDescription(this._businessInfo.ShipperDescription);
            }

            if (this.consigneeBridge != null)
            {
                this.consigneeBridge.SetCustomerDescription(this._businessInfo.ConsigneeDescription);
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
        /// 筛选：搜索的默认条件为 装货港=空运进口业务. 卸货港 and卸货港=空运进口业务.交收货地
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForSearchPreVoyage()
        {
            Guid polId = Guid.Empty;
            Guid podId = Guid.Empty;
            try
            {
                polId = (Guid)this.stxtDetination.Tag;
            }
            catch
            {
                throw new Exception(LocalData.IsEnglish ? "Please select P.O.R. at first." : "请先选择收货地！");
            }

            try
            {
                podId = (Guid)this.stxtPlaceOfDelivery.Tag;
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
            conditions.AddWithValue("POLID", this.stxtDetination.Tag, false);
            conditions.AddWithValue("POLName", this.stxtDetination.Text, false);
            conditions.AddWithValue("PODID", this.stxtPlaceOfDelivery.Tag, false);
            conditions.AddWithValue("PODName", this.stxtPlaceOfDelivery.Text, false);

            return conditions;
        }

        /// <summary>
        /// 大船
        /// 筛选：装货港=当前装货港and卸货港=当前卸货港
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForSearchVoyage()
        {
            Guid polId = Guid.Empty;
            Guid podId = Guid.Empty;
            try
            {
                polId = (Guid)this.stxtDeparture.Tag;
            }
            catch
            {
                throw new Exception(LocalData.IsEnglish ? "Please select POL at first." : "请先选择装货港！");
            }

            try
            {
                podId = (Guid)this.stxtDetination.Tag;
            }
            catch
            {
                throw new Exception(LocalData.IsEnglish ? "Please select POD at first." : "请先选择卸货港！");
            }

            if (polId == Guid.Empty)
            {
                throw new Exception(LocalData.IsEnglish ? "Please select POL at first." : "请先选择装货港！");
            }

            if (podId == Guid.Empty)
            {
                throw new Exception(LocalData.IsEnglish ? "Please select POD at first." : "请先选择卸货港！");
            }

            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("POLID", this.stxtDeparture.Tag, false);
            conditions.AddWithValue("POLName", this.stxtDeparture.Text, false);
            conditions.AddWithValue("PODID", this.stxtDetination.Tag, false);
            conditions.AddWithValue("PODName", this.stxtDetination.Text, false);

            return conditions;
        }

        #endregion

        #region 注册延迟加载的数据源
        List<DataDictionaryList> _weightUnits;
        /// <summary>
        /// 注册延迟加载的数据源
        /// </summary>
        void SetLazyLoaders()
        {
            _weightUnits = ICPCommUIHelperService.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit);


            //操作口岸列表   
            Utility.SetEnterToExecuteOnec(cmbCompany, delegate
            {
                ICPCommUIHelperService.BindCompanyByUser(cmbCompany, false);
            });
            //运输条款
            Utility.SetEnterToExecuteOnec(cmbTransportClause, delegate
            {
                List<TransportClauseList> transportClauseList = ICPCommUIHelperService.SetCmbTransportClause(cmbTransportClause);
            });

            ////运输条款
            if (Utility.GuidIsNullOrEmpty(_businessInfo.ID) == false)
            {
                Utility.SetEnterToExecuteOnec(cmbMBLTransportClause, delegate
                {
                    ICPCommUIHelperService.SetCmbTransportClause(cmbMBLTransportClause);
                });
            }

            //贸易条款
            Utility.SetEnterToExecuteOnec(cmbTradeTerm, delegate
            {
                ICPCommUIHelperService.SetCmbDataDictionary(cmbTradeTerm, DataDictionaryType.TradeTerm);
            });

            //包装
            Utility.SetEnterToExecuteOnec(cmbQuantityunit, delegate
            {
                ICPCommUIHelperService.SetCmbDataDictionary(cmbQuantityunit, DataDictionaryType.QuantityUnit);
            });

            //重量
            Utility.SetEnterToExecuteOnec(cmbWeightUnit, delegate
            {
                ICPCommUIHelperService.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit);
            });

            //体积
            Utility.SetEnterToExecuteOnec(cmbMeasurementUnit, delegate
            {
                List<DataDictionaryList> volUnitss = ICPCommUIHelperService.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit);
            });

            //揽货方式
            Utility.SetEnterToExecuteOnec(cmbSalesType, delegate
            {
                List<DataDictionaryList> salesTypes = ICPCommUIHelperService.SetCmbDataDictionary(cmbSalesType, DataDictionaryType.SalesType);
            });
            //3个付款方式的下拉列表
            Utility.SetEnterToExecuteOnec(cmbPaymentTerm, delegate
            {
                List<DataDictionaryList> payments = ICPCommUIHelperService.SetCmbDataDictionary(
                                                    cmbPaymentTerm,
                                                    DataDictionaryType.PaymentTerm, DataBindType.EName, true);
            });

            //揽货人
            Utility.SetEnterToExecuteOnec(this.cmbSales, delegate
            {
                ICPCommUIHelperService.SetMcmbUsers(cmbSales, _businessInfo.CompanyID, "", "", true);
            });

            //放货方式
            //Utility.SetEnterToExecuteOnec(cmbReleaseType, delegate
            //{
            ICPCommUIHelperService.SetComboxByEnum<FCMReleaseType>(this.cmbReleaseType, false);
            //});    


            //委托方式
            Utility.SetEnterToExecuteOnec(this.cmbBookingMode, delegate
            {
                ICPCommUIHelperService.SetComboxByEnum<FCMBookingMode>(this.cmbBookingMode, false);
            });


            #region 代理
            if (Utility.GuidIsNullOrEmpty(_businessInfo.AgentID) == false)
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
            });
            #endregion

            //货物描述
            Utility.SetEnterToExecuteOnec(this.cmbCargoType, delegate
            {
                ICPCommUIHelperService.SetComboxByEnum<CargoType>(this.cmbCargoType, true, true);
            });

            //航空公司
            Utility.SetEnterToExecuteOnec(mcmbAirCompany, delegate
            {
                ICPCommUIHelperService.BindCustomerList(mcmbAirCompany, CustomerType.Airline);
            });

            Utility.SetEnterToExecuteOnec(cmbFlightNo, delegate
            {
                List<FlightList> flightList = tfService.GetFlightList(null, string.Empty, true, 0);
                if (flightList != null && flightList.Count > 0)
                {
                    Dictionary<string, string> col = new Dictionary<string, string>();
                    col.Add("No", "航班号");
                    col.Add("AirlineName", "航空公司");
                    cmbFlightNo.InitSource<FlightList>(flightList, col, "No", "ID");
                }
            });

            //主提单号
            Utility.SetEnterToExecuteOnec(stxtMBLNo, delegate
            {
                //if (_businessInfo.OIOperationType == OIOperationType.LCL)
                //{
                _mblNoList = oiService.GetAIMBLList();

                if (_mblNoList.Count > 0)
                {
                    stxtMBLNo.Properties.Items.Clear();
                    foreach (var item in _mblNoList)
                    {
                        stxtMBLNo.Properties.Items.Add(item.MBLNo);
                    }

                }
                //}
            });
        }

        /// <summary>
        /// 延迟加载，而且条件是动态的
        /// </summary>
        void SetLazyDataLodersWithDynamicCondition()
        {
            cmbFile.Enter += new EventHandler(cmbFile_Enter);
            cmbCustomerService.Enter += new EventHandler(cmbCustomerService_Enter);

            treeBoxSalesDep.Enter += new EventHandler(treeBoxSalesDep_Enter);

        }

        void treeBoxSalesDep_Enter(object sender, EventArgs e)
        {
            List<OrganizationList> userOrganizationTreeLists = new List<OrganizationList>();
            if (Utility.GuidIsNullOrEmpty(_businessInfo.SalesID) == false)
            {
                userOrganizationTreeLists = userService.GetUserCompanyList(_businessInfo.SalesID.Value, null);
            }

            List<OrganizationList> saleOrgrnazitionTreeList = userService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
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

        /// <summary>
        /// 注册界面控件之间联动的事件
        /// 一般一个控件的值改变，要影响别的控件的值，就注册到这里。如果同时还要改变其它控件的颜色、可用状态之类的逻辑，要拆分开。
        /// </summary>
        void RegisterRelativeEvents()
        {
            this.pnlMain.Click += delegate { this.pnlMain.Focus(); };
            this.cmbCompany.SelectedIndexChanged += new EventHandler(cmbCompany_SelectedIndexChanged);
            this.cmbCustomerService.SelectedIndexChanged += new EventHandler(cmbCustomerService_SelectedIndexChanged);
            this.cmbCargoType.EditValueChanged += new System.EventHandler(this.cmbCargoType_EditValueChanged);

            this.cmbSales.SelectedRow += new EventHandler(cmbSales_SelectedRow);

            this.cmbReleaseType.EditValueChanged += new EventHandler(cmbReleaseType_EditValueChanged);

            this.stxtMBLNo.EditValueChanged += new EventHandler(stxtMBLNo_EditValueChanged);
            mcmbAirCompany.SelectedRow += new EventHandler(mcmbCarrier_SelectedRow);
            this.cmbFlightNo.SelectedRow += new EventHandler(cmbFlightNo_SelectedRow);
            //this.cmbFlightNo.Enter += new EventHandler(cmbFlightNo_Enter);

            this.stxtCustomer.LostFocus += new EventHandler(stxtCustomer_LostFocus);
            this.cmbTradeTerm.SelectedIndexChanged += new EventHandler(cmbTradeTerm_SelectedIndexChanged);
            this.cmbTransportClause.SelectedIndexChanged += new EventHandler(cmbTransportClause_SelectedIndexChanged);

            ///this.stxtPOD.TextChanged += new EventHandler(stxtPOD_TextChanged);
            this.stxtPlaceOfDelivery.EditValueChanged += new EventHandler(stxtPlaceOfDelivery_TextChanged);

            this.stxtDetination.EditValueChanged += new EventHandler(stxtPOD_TextChanged);

            this.dtpETA.EditValueChanged += delegate { SetDETA(); };
            //this.dtpDETA.EditValueChanged += delegate { SetFETA(); };


        }

        void cmbCustomerService_SelectedIndexChanged(object sender, EventArgs e)
        {
            _businessInfo.CustomerServiceName = cmbCustomerService.Text;
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
            if (this._shown)
            {
                this._businessInfo.PlaceOfDeliveryName = this.stxtPlaceOfDelivery.Text;
                this.SetDETA();
            }
        }

        void stxtPOD_TextChanged(object sender, EventArgs e)
        {
            if (this._shown)
            {
                _businessInfo.PODName = this.stxtDetination.Text;
                this.SetPlaceOfDeliveryByTransportClause();
            }
        }

        void cmbTransportClause_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._shown)
            {
                this._businessInfo.TransportClauseName = this.cmbTransportClause.Text;
                SetPlaceOfDeliveryByTransportClause();
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
            this.cmbSalesType.EditValueChanged += new EventHandler(cmbSalesType_EditValueChanged);
            this.TriggerEventsAtOnce();
        }
        /// <summary>
        /// 设置Agent数据源
        /// </summary>
        private void SetAgentSourceByCompanyID()
        {
            Guid companyID = _businessInfo.CompanyID;
            txtAgent.DataSource = null;
            if (Utility.GuidIsNullOrEmpty(companyID))
            {
                txtAgent.Enabled = false;
                return;
            }

            List<CustomerList> agentCustomers = configureService.GetCompanyAgentList(_businessInfo.CompanyID, true);
            CustomerList emptyCustomer = new CustomerList();
            emptyCustomer.CName = emptyCustomer.EName = string.Empty;
            emptyCustomer.ID = Guid.Empty;
            agentCustomers.Insert(0, emptyCustomer);

            //将公司配置中对应的客户从代理列表中去掉
            ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(companyID);
            if (configureInfo != null)
            {
                agentCustomers = (from a in agentCustomers where a.ID != configureInfo.CustomerID select a).ToList();
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
            if (Utility.GuidIsNullOrEmpty(_businessInfo.AgentID))
            {
                txtAgent.EditValue = _businessInfo.AgentID = agentCustomers[0].ID;
            }
            txtAgent.EditValueChanged -= new EventHandler(stxtAgent_EditValueChanged);
            txtAgent.EditValueChanged += new EventHandler(stxtAgent_EditValueChanged);
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

        /// <summary>
        /// 根据ID生成代理的描述和把描述填充到描述框
        /// </summary>
        private void BulidAgentDescriqitonByID(Guid? id)
        {
            if (Utility.GuidIsNullOrEmpty(id))
            {
                txtAgent.CustomerDescription = _businessInfo.AgentDescription = new CustomerDescription();
            }
            else
            {
                ICPCommUIHelperService.SetCustomerDesByID(id, _businessInfo.AgentDescription);
                txtAgent.CustomerDescription = _businessInfo.AgentDescription;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbTradeTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._businessInfo.TradeTermName = this.cmbTradeTerm.Text;
        }



        /// <summary>
        /// 收货人:默认为客户
        /// </summary>
        private void SetConsigneeByCustomer()
        {
            if (Utility.GuidIsNullOrEmpty(this._businessInfo.ConsigneeID))
            {
                this.stxtConsignee.Tag = this._businessInfo.ConsigneeID = this._businessInfo.CustomerID;
                this.stxtConsignee.Text = this._businessInfo.ConsigneeName = this._businessInfo.CustomerName;
                ICPCommUIHelperService.SetCustomerDesByID(_businessInfo.ConsigneeID, _businessInfo.ConsigneeDescription);
            }

            this.ResetDescription();

        }

        #region Port And Voyage     

        /// <summary>
        /// 交货地 如果目的港运输条款不等于Door，那么就为卸货港
        /// </summary>
        private void SetPlaceOfDeliveryByTransportClause()
        {
            if (!Utility.GuidIsNullOrEmpty(this._businessInfo.PlaceOfDeliveryID)
                || Utility.GuidIsNullOrEmpty(this._businessInfo.TransportClauseID)) return;

            if (_businessInfo.TransportClauseName.Contains("-DOOR") == false)
            {
                stxtPlaceOfDelivery.Text = _businessInfo.PlaceOfDeliveryName = _businessInfo.PODName;
                stxtPlaceOfDelivery.Tag = _businessInfo.PlaceOfDeliveryID = _businessInfo.PODID;
            }
        }

        /// <summary>
        /// 设置默认DETA
        /// </summary>
        private void SetDETA()
        {
            //输入的时候，ID是空，只能要怕NAME去判断了
            if (this._shown && _businessInfo.ETA != null && _businessInfo.PODName == _businessInfo.PlaceOfDeliveryName)
            {
                this.dtpDETA.DateTime = Convert.ToDateTime(_businessInfo.DETA = _businessInfo.ETA);
            }
        }

        #endregion

        private void mcmbCarrier_SelectedRow(object sender, EventArgs e)
        {
            AirBusinessMBLList mbl = Utility.Clone<AirBusinessMBLList>(this._businessInfo.MBLInfo);
            mbl.ID = Guid.Empty;
            mbl.MBLNo = this.stxtMBLNo.EditValue.ToString();
            if (this.mcmbAirCompany.EditValue != null)
            {
                mbl.AirCompanyID = new Guid(this.mcmbAirCompany.EditValue.ToString());
            }
            else
            {
                mbl.AirCompanyID = Guid.Empty;
            }
            mbl.UpdateDate = null;

            this._businessInfo.MBLInfo = mbl;
            this.bsMBLInfo.DataSource = this._businessInfo.MBLInfo;
            this.bsMBLInfo.ResetBindings(false);
            this._businessInfo.MBLInfo.IsDirty = true;
            _businessInfo.IsDirty = true;
        }

        private void stxtMBLNo_EditValueChanged(object sender, EventArgs e)
        {
            string curruntNo = this.stxtMBLNo.EditValue.ToString();
            if (curruntNo != this._businessInfo.MBLInfo.MBLNo)
            {
                AirBusinessMBLList mbl = null;
                foreach (var item in _mblNoList)
                {
                    if (item.MBLNo == curruntNo)
                    {
                        mbl = item;
                        break;
                    }
                }

                if (mbl != null)
                {
                    mbl = oiService.GetAIMBLInfo(mbl.ID);
                    if (mbl != null)
                    {
                        this._businessInfo.MBLInfo = mbl;
                        this.bsMBLInfo.DataSource = this._businessInfo.MBLInfo;
                        this.bsMBLInfo.ResetBindings(false);
                        this._businessInfo.MBLInfo.IsDirty = true;
                        _businessInfo.IsDirty = true;
                        InitMBLControl();
                    }
                }
                else
                {
                    mbl = Utility.Clone<AirBusinessMBLList>(this._businessInfo.MBLInfo);
                    mbl.ID = Guid.Empty;
                    mbl.MBLNo = curruntNo;
                    mbl.UpdateDate = null;
                    this._businessInfo.MBLInfo = mbl;
                    this.bsMBLInfo.DataSource = this._businessInfo.MBLInfo;
                    this.bsMBLInfo.ResetBindings(false);
                    this._businessInfo.MBLInfo.IsDirty = true;

                    _businessInfo.IsDirty = true;
                }
            }
        }

        #endregion

        #region 主要是控件属性的改变（颜色、可操作性等）

        /// <summary>
        /// 主要是控件属性的改变（颜色、可操作性等）
        /// 包含下面所有的方法，逐一调用
        /// </summary>
        void TriggerEventsAtOnce()
        {
            this.cmbType_SelectedIndexChanged(this, null);
            this.cmbSalesType_EditValueChanged(this, null);
            RefreshBarEnabled();
        }

        void cmbSalesType_EditValueChanged(object sender, EventArgs e)
        {
            if (this._shown)
            {

                #region <设置揽货人>
                if (this.cmbSalesType.Text == "公司货")
                {
                    this.cmbSales.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    this.cmbSales.BackColor = System.Drawing.SystemColors.Info;
                }

                #endregion
            }
        }

        void RefreshBarEnabled()
        {
            if (Utility.GuidIsNullOrEmpty(this._businessInfo.ID))
            {
                this.barRefresh.Enabled = false;
                this.barPrint.Enabled = false;
                this.barReturn.Enabled = false;
                this.barBill.Enabled = false;
            }
            else
            {
                this.barRefresh.Enabled = true;
                this.barPrint.Enabled = true;
                this.barReturn.Enabled = true;
                this.barBill.Enabled = true;
            }

            if (this._businessInfo.State != AIOrderState.NewOrder)
            {
                barReturn.Enabled = false;
            }
        }

        #endregion

        #region 界面控件联动

        private void SetState()
        {
            this.txtState.Text = EnumHelper.GetDescription<AIOrderState>(this._businessInfo.State, LocalData.IsEnglish);
        }

        /// <summary>
        /// 客户改变后需设置"订舱客户","收货人","代理","揽货方式","最近业务"
        /// </summary>
        /// <param name="customerType">客户的类型,请在方法外部获取</param>
        private void CustomerChanged(CustomerType? customerType)
        {
            SetConsigneeByCustomer();
            SetSalesTypeByCustomerAndCompany();
            SetRecentlyOrderListByCustomerAndCompany();
        }

        #region 业务类型和集装箱信息

        void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.chkIsTruck.Enabled = this._businessInfo.OIOperationType == OIOperationType.FCL;--pearl

            if (!this.chkIsTruck.Enabled)
            {
                this.chkIsTruck.Checked = this._businessInfo.IsTruck = false;
            }
        }


        #endregion


        void cmbCustomerService_Enter(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(this.cmbCompany.EditValue.ToString()))
            {
                depID = new Guid(cmbCompany.EditValue.ToString());
            }

            ICPCommUIHelperService.SetComboxUsersByRole(cmbCustomerService, depID, "客服", false);

        }

        void cmbFile_Enter(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(this.cmbCompany.EditValue.ToString()))
            {
                depID = new Guid(cmbCompany.EditValue.ToString());
            }

            ICPCommUIHelperService.SetComboxUsersByRole(cmbCustomerService, depID, "文件", false);

        }

        /// <summary>
        /// 操作口岸改变时，刷新关联的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._shown)
            {
                if (cmbCompany.EditValue == null || cmbCompany.EditValue.ToString().Length == 0)
                {
                    return;
                }

                Guid companyID = new Guid(cmbCompany.EditValue.ToString());
                UCOIOrderFeeEdit.SetCompanyID(this._businessInfo.CompanyID);
                SetSalesTypeByCustomerAndCompany();
                SetRecentlyOrderListByCustomerAndCompany();

                //值改变时需要刷新[文件和客服]下拉框。
                ICPCommUIHelperService.SetComboxUsersByRole(cmbFile, (Guid)cmbCompany.EditValue, "文件", true);
                ICPCommUIHelperService.SetComboxUsersByRole(cmbCustomerService, (Guid)cmbCompany.EditValue, "客服", true);
                //值改变时，绑定揽货人
                ICPCommUIHelperService.SetMcmbUsers(cmbSales, _businessInfo.CompanyID, string.Empty, string.Empty, true);

                //操作公司发生改变时，绑定代理
                SetAgentSourceByCompanyID();

                //刷新最近
                SetRecentlyOrderListByCustomerAndCompany();
            }
        }

        /// <summary>
        /// 弹出最近十票业务
        /// </summary>
        private void stxtCustomer_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind != DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) return;

            if (bsRecentTenOrders.DataSource == null || bsRecentTenOrders.List == null || bsRecentTenOrders.List.Count == 0)
            {
                stxtCustomer.ShowToolTips = true;
                return;
            }

            stxtCustomer.ShowToolTips = false;

            if (stxtCustomer.Properties.PopupControl.Visible == false)
                stxtCustomer.ShowPopup();
            else
                stxtCustomer.ClosePopup();
        }

        /// <summary>
        /// “客户”失去焦点的时候自动设置揽货方式
        /// </summary>
        private void SetSalesTypeByCustomerAndCompany()
        {
            if (_businessInfo.CompanyID != Guid.Empty && _businessInfo.CustomerID != Guid.Empty)
            {
                DataDictionaryInfo salesType = oiService.GetSalesType(_businessInfo.CustomerID, _businessInfo.CompanyID);
                if (salesType != null)
                {
                    _businessInfo.SalesTypeID = salesType.ID;
                    _businessInfo.SalesTypeName = salesType.EName;

                    this.cmbSalesType.ShowSelectedValue(_businessInfo.SalesTypeID, _businessInfo.SalesTypeName);
                }
            }
        }

        #region 最近10票业务

        /// <summary>
        /// 最近业务
        /// </summary>
        private void SetRecentlyOrderListByCustomerAndCompany()
        {
            if (_businessInfo.ID != Guid.Empty || _businessInfo.CompanyID == Guid.Empty || _businessInfo.CustomerID == Guid.Empty)
            {
                bsRecentTenOrders.Clear();
            }
            else
            {
                bsRecentTenOrders.Clear();
                List<AirOrderInfo> orderList = oiService.GetAIRecentlyOrderList(_businessInfo.CompanyID, _businessInfo.CustomerID, LocalData.UserInfo.LoginID, 10);
                if (orderList != null && orderList.Count > 0)
                {
                    bsRecentTenOrders.DataSource = orderList;
                    stxtCustomer.ShowPopup();
                    //设置仓库、报关的默认值为最近的纪录
                    _businessInfo.WareHouseID = orderList[0].WareHouseID;
                    _businessInfo.WareHouseName = orderList[0].WareHouseName;

                    _businessInfo.CustomsID = orderList[0].CustomsID;
                    _businessInfo.CustomsName = orderList[0].CustomsName;


                }
            }
        }

        private void gvOrders_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentBusinessList == null)
            {
                return;
            }

            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "是否覆盖当前页面数据?" : "是否覆盖当前页面数据?"
                              , LocalData.IsEnglish ? "Tip" : "提示"
                              , MessageBoxButtons.YesNo
                              , MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                AirBusinessInfo business = oiService.GetBusinessInfoByEdit(CurrentBusinessList.ID);
                if (business == null)
                {
                    return;
                }

                business.ID = Guid.Empty;
                business.SalesID = LocalData.UserInfo.LoginID;
                business.SalesName = LocalData.UserInfo.LoginName;
                business.CreateDate = DateTime.Now;
                _businessInfo = business;

                this.ShowBusiness();
                this.TriggerEventsAtOnce();
                this.ResetDescription();
                this.EndEdit();

                this.Invalidate();
            }
        }

        #endregion

        /// <summary>
        /// TODO: 很多客户都不能弹出描述信息了
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="customerDescription"></param>
        private void SetCustomerDesByID(Guid? customerID, CustomerDescription customerDescription)
        {
            if (Utility.GuidIsNullOrEmpty(customerID)) return;

            CustomerInfo info = customerService.GetCustomerInfo(customerID.Value);
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
            if (!Utility.GuidIsNullOrEmpty(_businessInfo.SalesID))
            {
                userOrganizationTreeLists = userService.GetUserOrganizationTreeList(_businessInfo.SalesID.Value);
                UserOrganizationTreeList orginazation = userOrganizationTreeLists.Find(o => o.IsDefault);
                if (orginazation != null)
                {
                    this.treeBoxSalesDep.ShowSelectedValue(orginazation.ID, LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName);
                    _businessInfo.SalesDepartmentID = orginazation.ID;
                    _businessInfo.SalesDepartmentName = LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName;

                }
                else
                {
                    this.treeBoxSalesDep.ShowSelectedValue(Guid.Empty, string.Empty);
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
                this._businessInfo.CargoDescription = null;
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
            this.cmbCargoType.EditValueChanged -= new System.EventHandler(this.cmbCargoType_EditValueChanged);

            if (this._businessInfo.CargoDescription == null)
            {
                this._businessInfo.CargoDescription = new CargoDescription();
            }
            if (cargoType == CargoType.Awkward)
            {
                if (_businessInfo.CargoDescription.Cargo == null || _businessInfo.CargoDescription.Cargo is AwkwardCargo == false)
                {
                    _businessInfo.CargoDescription.Cargo = new AwkwardCargo();
                }

                if (cargoDescriptionPart1 is ICP.FCM.AirImport.UI.Common.Controls.AwkwardDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.AirImport.UI.Common.Controls.AwkwardDescriptionPart();
                    pnlMain.Controls.Add(cargoDescriptionPart1);

                    cargoDescriptionPart1.ShowWeightUnit(this._weightUnits);
                }
            }
            else if (cargoType == CargoType.Dangerous)
            {
                if (_businessInfo.CargoDescription.Cargo == null || _businessInfo.CargoDescription.Cargo is DangerousCargo == false)
                {
                    _businessInfo.CargoDescription.Cargo = new DangerousCargo();
                }


                if (cargoDescriptionPart1 is ICP.FCM.AirImport.UI.Common.Controls.DangerousDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.AirImport.UI.Common.Controls.DangerousDescriptionPart();
                    pnlMain.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Dry)
            {
                if (_businessInfo.CargoDescription.Cargo == null || _businessInfo.CargoDescription.Cargo is DryCargo == false)
                {
                    _businessInfo.CargoDescription.Cargo = new DryCargo();
                }

                if (cargoDescriptionPart1 is ICP.FCM.AirImport.UI.Common.Controls.DryDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.AirImport.UI.Common.Controls.DryDescriptionPart();
                    pnlMain.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Reefer)
            {
                if (_businessInfo.CargoDescription.Cargo == null || _businessInfo.CargoDescription.Cargo is ReeferCargo == false)
                {
                    _businessInfo.CargoDescription.Cargo = new ReeferCargo();
                }

                if (cargoDescriptionPart1 is ICP.FCM.AirImport.UI.Common.Controls.ReeferDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.AirImport.UI.Common.Controls.ReeferDescriptionPart();
                    pnlMain.Controls.Add(cargoDescriptionPart1);
                }
            }

            cargoDescriptionPart1.SetParentControl(sender, _businessInfo.CargoDescription, txtCargoDescription);
            this.cmbCargoType.EditValueChanged += new System.EventHandler(this.cmbCargoType_EditValueChanged);
        }

        private void RemoveCargoPart()
        {
            if (cargoDescriptionPart1 != null)
            {
                cargoDescriptionPart1.Hide();
                pnlMain.Controls.Remove(cargoDescriptionPart1);
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
            this.dtpClearanceDate.Enabled = this.ckbIsClearance.Checked;
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
            this.UCHBLList.SetReceive(rType);

        }
        #endregion

        #region 工具栏事件

        #region 界面输入验证

        bool ValidateData()
        {
            this.EndEdit();
            List<bool> isScrrs = new List<bool> { true, true };
            isScrrs[0] = _businessInfo.Validate
                (
                    delegate(ValidateEventArgs e)
                    {
                        if (_businessInfo.POLID != Guid.Empty && _businessInfo.POLID == _businessInfo.PODID)
                        {
                            e.SetErrorInfo("PODID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");
                        }

                        if (_businessInfo.ETA != null && _businessInfo.ETD != null
                            && _businessInfo.ETD > _businessInfo.ETA)
                            e.SetErrorInfo("ETA", LocalData.IsEnglish ? "ETD can't bigger ETA." : "ETD不能大于ETA.");

                        //if (this.cmbSalesType.Text == "公司货" && (_businessInfo.SalesID==null||_businessInfo.SalesID == Guid.Empty))
                        if (this.cmbSalesType.Text != "公司货" && (_businessInfo.SalesID == null || _businessInfo.SalesID == Guid.Empty))
                        {
                            e.SetErrorInfo("SalesID", LocalData.IsEnglish ? "please input Sales." : "揽货人不能为空");
                        }

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

            if (!_businessInfo.MBLInfo.Validate())
            {
                isScrrs[0] = false;
            }

            if (_businessInfo.MBLInfo.ReleaseType == FCMReleaseType.Unknown)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                  , (LocalData.IsEnglish ? "Delivery Type is necessary" : "放货类型不能为空"));
                isScrrs[0] = false;
            }

            #endregion

            #region 验证 HBL
            if (!this.UCHBLList.ValidateData())
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

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Save(this._businessInfo, false);
            }
        }
        private bool Save(AirBusinessInfo currentData, bool isSavingAs)
        {
            if (ValidateData() == false) return false;
            try
            {
                BusinessSaveRequest originalBusiness = SaveBusiness(currentData);

                List<FeeSaveRequest> originalFees = this.UCOIOrderFeeEdit.GetFeeList(currentData.ID);

                MBLInfoSaveRequest originalMBL = GetMBLInfo(currentData.ID);
                List<HBLInfoSaveRequest> originalHBLs = UCHBLList.GetHBLSaveInfo();
                Dictionary<Guid, SaveResponse> saved = this.oiService.SaveAIBusinessWithTrans(
                    originalBusiness,
                    originalMBL,
                    originalHBLs,
                    originalFees);


                if (originalBusiness != null)
                {
                    SaveResponse.Analyze(new List<SaveRequest> { originalBusiness }, saved, true);
                    this.RefreshUI(originalBusiness);
                }

                if (originalFees != null)
                {
                    SaveResponse.Analyze(originalFees.Cast<SaveRequest>().ToList(), saved, true);
                    this.UCOIOrderFeeEdit.RefreshUI(originalFees);
                }

                if (originalMBL != null)
                {
                    SaveResponse.Analyze(new List<SaveRequest> { originalMBL }, saved, true);
                    this.RefreshMBLUI(originalMBL);
                }

                if (originalHBLs != null)
                {
                    SaveResponse.Analyze(originalHBLs.Cast<SaveRequest>().ToList(), saved, true);
                    this.UCHBLList.RefreshUI(originalHBLs);
                    this.UCHBLList.BusinessID = _businessInfo.ID;
                }

                if (isSavingAs)
                {
                    this._businessInfo.IsDirty = _businessInfo.MBLInfo.IsDirty = this.UCHBLList.IsChanged = false;


                }
                else
                {
                    AfterSave();
                }

                if (this._businessInfo.AgentDescription != null)
                {
                    this._businessInfo.AgentDescription.IsDirty = false;
                }
                if (this._businessInfo.ConsigneeDescription != null)
                {
                    this._businessInfo.ConsigneeDescription.IsDirty = false;
                }
                if (this._businessInfo.ShipperDescription != null)
                {
                    this._businessInfo.ShipperDescription.IsDirty = false;
                }
                if (this._businessInfo.NotifyPartyDescription != null)
                {
                    this._businessInfo.NotifyPartyDescription.IsDirty = false;
                }


                return true;
            }
            catch (Exception ex)
            {
                if (isSavingAs)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "另存为失败!");
                }

                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                return false;
            }
        }

        private void AfterSave()
        {
            _currentMBLno = this._businessInfo.MBLInfo.MBLNo;
            _currentAirCompanyID = this._businessInfo.MBLInfo.AirCompanyID;
            RefreshBarEnabled();
            this._businessInfo.BeginEdit();

            this.stxtCustomer.Properties.Buttons[2].Visible = false;
            this.gvOrders.DoubleClick -= new System.EventHandler(this.gvOrders_DoubleClick);
            this.stxtCustomer.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.stxtCustomer_ButtonClick);
            barSaveAs.Enabled = true;

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

            if (Saved != null)
            {

                Saved(new object[] { _businessInfo, this.UCHBLList.GetHBLNo(), this.UCHBLList.GetReceiverState() });

                this._businessInfo.IsDirty = false;
            }

            this._businessInfo.IsDirty = false;

            this.TriggerEventsAtOnce();

            this.SetTitle();
        }

        void SetTitle()
        {
            if (this._businessInfo.ID == Guid.Empty)
            {
                this.Title = LocalData.IsEnglish ? "Add Business" : "新增业务";
            }
            else
            {
                string titleNo = string.Empty;

                if (this._businessInfo.No.Length > 4)
                {
                    titleNo = this._businessInfo.No.Substring(this._businessInfo.No.Length - 4, 4);
                }
                else
                {
                    titleNo = this._businessInfo.No;
                }

                this.Title = LocalData.IsEnglish ? "Business " + titleNo : "业务：" + titleNo;
            }
        }

        /// <summary>
        /// 获得保存数据实体
        /// </summary>
        /// <param name="currentData"></param>
        private BusinessSaveRequest SaveBusiness(AirBusinessInfo currentData)
        {
            this.EndEdit();

            BusinessSaveRequest saveRequest = new BusinessSaveRequest();
            saveRequest.ID = currentData.ID;
            saveRequest.No = currentData.No;

            saveRequest.MBLID = currentData.MBLID.ToGuid();
            saveRequest.CompanyID = currentData.CompanyID;
            saveRequest.CustomerID = currentData.CustomerID;
            saveRequest.CustomerDescription = currentData.CustomerDescription;
            saveRequest.CustomerNo = currentData.CustomerNo;
            saveRequest.TradeTermID = currentData.TradeTermID;
            saveRequest.SalesTypeID = currentData.SalesTypeID;
            saveRequest.SalesID = currentData.SalesID;
            saveRequest.SalesDepartmentID = currentData.SalesDepartmentID;
            saveRequest.TransportClauseID = currentData.TransportClauseID;
            saveRequest.FilerId = currentData.FilerId;
            saveRequest.PaymentTermID = currentData.PaymentTermID;
            saveRequest.CustomerServiceID = currentData.customerService;
            saveRequest.BookingMode = currentData.BookingMode;
            saveRequest.POLFilerID = currentData.POLFilerID;
            saveRequest.POLFilerName = currentData.POLFilerName;
            saveRequest.BookingDate = currentData.BookingDate;
            saveRequest.AgentID = currentData.AgentID;
            saveRequest.AgentDescription = currentData.AgentDescription;
            saveRequest.AgentNo = currentData.AgentNo;
            saveRequest.ShipperID = currentData.ShipperID;
            saveRequest.ShipperDescription = currentData.ShipperDescription;
            saveRequest.ConsigneeID = currentData.ConsigneeID;
            saveRequest.ConsigneeDescription = currentData.ConsigneeDescription;
            saveRequest.NotifyPartyID = currentData.NotifyPartyID;
            saveRequest.NotifyPartyDescription = currentData.NotifyPartyDescription;
            saveRequest.POLID = currentData.POLID;
            saveRequest.PODID = currentData.PODID;
            saveRequest.PlaceOfDeliveryID = currentData.PlaceOfDeliveryID;
            saveRequest.ETD = currentData.ETD;
            saveRequest.ETA = currentData.ETA;
            saveRequest.DETA = currentData.dETA;
            saveRequest.DOCPickupDate = currentData.DOCPickupDate;
            saveRequest.DOCPickupBy = currentData.DOCPickupBy;
            saveRequest.StorageStartDate = currentData.StorageStartDate;
            saveRequest.WarehouseArrivedON = currentData.WarehouseArrivedON;

            saveRequest.IsTruck = currentData.IsTruck;
            saveRequest.IsCustoms = currentData.IsCustoms;
            saveRequest.IsCommodityInspection = currentData.IsCommodityInspection;
            saveRequest.IsQuarantineInspection = currentData.IsQuarantineInspection;
            saveRequest.IsWareHouse = currentData.IsWareHouse;
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
            saveRequest.WareHouseID = currentData.WareHouseID;
            saveRequest.WareHouseDate = currentData.WareHouseDate;
            saveRequest.CustomsID = currentData.CustomsID;
            saveRequest.IsClearance = currentData.IsClearance;
            saveRequest.ClearanceDate = currentData.ClearanceDate;
            saveRequest.ReleaseType = currentData.IsTelex ? FCMReleaseType.Telex : FCMReleaseType.Original;
            saveRequest.ReleaseDate = currentData.ReleaseDate;
            saveRequest.Remark = currentData.Remark;
            saveRequest.saveByID = LocalData.UserInfo.LoginID;
            saveRequest.Updatedate = currentData.UpdateDate;

            AirBusinessMBLList mblList = bsMBLInfo.DataSource as AirBusinessMBLList;
            if (mblList != null)
            {
                saveRequest.AirCompanyID = mblList.AirCompanyID;
            }

            saveRequest.AddInvolvedObject(currentData);
            return saveRequest;
        }

        #region 保存MBL

        public MBLInfoSaveRequest GetMBLInfo(Guid BusinessID)
        {
            AirBusinessMBLList mblInfo = bsMBLInfo.DataSource as AirBusinessMBLList;
            if (mblInfo != null && mblInfo.IsDirty)
            {
                MBLInfoSaveRequest mblInfoToSave = new MBLInfoSaveRequest();

                mblInfoToSave.ID = mblInfo.ID;
                mblInfoToSave.UpdateDates = mblInfo.UpdateDate;
                mblInfoToSave.MBLNo = mblInfo.MBLNo;
                mblInfoToSave.SubNo = mblInfo.SubNo;
                mblInfoToSave.AirCompanyID = mblInfo.AirCompanyID;
                mblInfoToSave.AgentOfCarrierID = mblInfo.AgentOfCarrierID;
                mblInfoToSave.FlightID = mblInfo.FlightID;
                mblInfoToSave.FlightFlag = mblInfo.FlightFlag;
                mblInfoToSave.ETD = _businessInfo.ETD;
                mblInfoToSave.ETA = _businessInfo.ETA;
                mblInfoToSave.FlightCountry = mblInfo.FlightCountry;
                mblInfoToSave.ManifestNO = mblInfo.ManifestNO;
                mblInfoToSave.GODate = mblInfo.GODate;
                mblInfoToSave.FinalWareHouseID = mblInfo.FinalWareHouseID;
                mblInfoToSave.ITNO = mblInfo.ITNO;
                mblInfoToSave.ITDate = mblInfo.ITDate;
                mblInfoToSave.ITPalce = mblInfo.ITPalce;
                mblInfoToSave.ReleaseType = mblInfo.ReleaseType;
                mblInfoToSave.MBLTransportClauseID = mblInfo.MBLTransportClauseID;
                mblInfoToSave.SaveByID = LocalData.UserInfo.LoginID;

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
            AirBusinessMBLList currentData = saveRequest.UnBoxInvolvedObject<AirBusinessMBLList>()[0];
            currentData.ID = result.GetValue<Guid>("ID");
            currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            currentData.IsDirty = false;
        }

        #endregion

        void RefreshUI(BusinessSaveRequest saveRequest)
        {
            SingleResult result = saveRequest.SingleResult;
            AirBusinessInfo currentData = saveRequest.UnBoxInvolvedObject<AirBusinessInfo>()[0];
            currentData.ID = result.GetValue<Guid>("ID");
            currentData.No = result.GetValue<string>("NO");
            //currentData.RefNo = result.GetValue<string>("NO");
            currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            currentData.State = (AIOrderState)result.GetValue<byte>("State");
        }

        #endregion

        #region 另存为

        /// <summary>
        /// TODO: 不应该用ErrorTrace来实现这个消息。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (SaveAs())
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save as a new order successfully. Ref. NO. is " + this._businessInfo.No + "." : "已成功另存为一票新业务，业务号为" + this._businessInfo.No + "。");

                    if (Saved != null)
                    {
                        Saved(new object[] { _businessInfo, this.UCHBLList.GetHBLNo(), null, this.UCHBLList.GetReceiverState() });
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

            if (_currentAirCompanyID == (Guid)mcmbAirCompany.EditValue && _currentMBLno == this.stxtMBLNo.EditValue.ToString())
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "MAWB Same,To Save?" : "航空公司和主提单号不能都相同,请先修改航空公司或主提单号!", LocalData.IsEnglish ? "Tip" : "提示");
                return false;
            }

            string saveAsMessage = LocalData.IsEnglish ? "Un Done" : "是否另存为一票新的业务?";
            if (!Utility.ShowResultMessage(saveAsMessage))
            {
                return false;
            }

            AirBusinessInfo businessInfo = Utility.Clone<AirBusinessInfo>(_businessInfo);
            businessInfo.ID = Guid.Empty;
            businessInfo.No = string.Empty;
            businessInfo.State = AIOrderState.NewOrder;
            businessInfo.CreateID = LocalData.UserInfo.LoginID;
            businessInfo.CreateByName = LocalData.UserInfo.LoginName;
            businessInfo.CreateDate = DateTime.Now;
            businessInfo.BookingDate = DateTime.Now;
            businessInfo.UpdateDate = null;

            businessInfo.IsDirty = true;
            this._businessInfo = businessInfo;

            if (this.Save(businessInfo, true))
            {
                this.RefreshData(businessInfo.ID, true);
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
        private void barReturn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_businessInfo.State == AIOrderState.NewOrder)
            {
                ReturnOrder returnOrder = Workitem.Items.AddNew<ReturnOrder>();

                string title = LocalData.IsEnglish ? "Return Order" : "打回订单";

                if (Utility.ShowDialog(returnOrder, title) == DialogResult.OK)
                {
                    SingleResultData result = oiService.ChangeAIOrderState(_businessInfo.ID, AIOrderState.Rejected, returnOrder.ReturnRemark, LocalData.UserInfo.LoginID, _businessInfo.UpdateDate);

                    _businessInfo.State = AIOrderState.Rejected;
                    _businessInfo.UpdateDate = result.UpdateDate;
                    this.SetState();
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "" : "打回成功");
                    Saved(_businessInfo, this.UCHBLList.GetHBLNo(), null, this.UCHBLList.GetReceiverState());
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
        private void barAuditor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (!this.Save(this._businessInfo, false))
                {
                    return;
                }

                if (this._businessInfo.State != AIOrderState.NewOrder)
                {
                    return;
                }

                ConfirmBookingForm memoContentForm = Workitem.Items.AddNew<ConfirmBookingForm>();
                memoContentForm.Text = LocalData.IsEnglish ? "Audit Order" : "审核业务";
                memoContentForm.LabelText = LocalData.IsEnglish ? "Audit memo" : "审核意见";

                DialogResult dlg = memoContentForm.ShowDialog();
                if (dlg != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    SingleResultData result = oiService.ChangeAIOrderState(_businessInfo.ID, AIOrderState.Checked, memoContentForm.Memo, LocalData.UserInfo.LoginID, _businessInfo.UpdateDate);
                    _businessInfo.State = AIOrderState.Checked;
                    _businessInfo.UpdateDate = result.UpdateDate;
                    if (this.Saved != null)
                    {
                        this.Saved(_businessInfo, this.UCHBLList.GetHBLNo(), null, this.UCHBLList.GetReceiverState());
                    }

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Audit Successfully" : "审核成功!");
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            }
        }

        #endregion

        #region  刷新

        /// <summary>
        /// 数据刷新到初始或保存后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                this.RefreshData(this._businessInfo.ID, true);
                ////this.ShowBusiness();
                ////this.TriggerEventsAtOnce();
                ////this.ResetDescription();
                this.SetState();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Refresh Successfully" : "刷新成功");
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
                    if (!this.Save(this._businessInfo, false))
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        #endregion

        #region 打印报表

        /// <summary>
        /// 打印到港通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barPrintArrivalNotice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_businessInfo == null || _businessInfo.ID == Guid.Empty) return;
            if (IsChanged)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Current data has changed, want to print please save." : "当前数据有更改,欲打印请先保存.", LocalData.IsEnglish ? "Tip" : "提示");
                return;
            }

            if (_businessInfo.IsSentAN == false && (_businessInfo.SalesID == null || _businessInfo.SalesID == Guid.Empty))
            {
                if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Sales name is null,and Sales can not receive the Arrival Notice ,Sure to continue?" : "由于未填写业务员,港后通知邮件将无法通知到业务员.是否继续?",
                                    LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            stateValues.Add("AirBusinessList", _businessInfo);
            string no = _businessInfo.No.Length <= 4 ? _businessInfo.No : _businessInfo.No.Substring(_businessInfo.No.Length - 4, 4);
            string title = (LocalData.IsEnglish ? "Print Arrival Notice" : "到港通知书") + ("-" + no);
            PartLoader.ShowEditPart<OIArrivalNotice2>(Workitem, null, stateValues, title, null, null);
        }

        /// <summary>
        /// 打印放货通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barReleaseOrder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_businessInfo == null || _businessInfo.ID == Guid.Empty) return;
            if (IsChanged)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Current data has changed, want to print please save." : "当前数据有更改,欲打印请先保存.", LocalData.IsEnglish ? "Tip" : "提示");
                return;
            }

            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            stateValues.Add("AirBusinessList", _businessInfo);
            string no = _businessInfo.No.Length <= 4 ? _businessInfo.No : _businessInfo.No.Substring(_businessInfo.No.Length - 4, 4);
            string title = (LocalData.IsEnglish ? "Print Release Order" : "放货通知书") + ("-" + no);
            PartLoader.ShowEditPart<OIReleaseOrder2>(Workitem, null, stateValues, title, null, null);
        }

        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barPrintProfit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_businessInfo == null || _businessInfo.ID == Guid.Empty) return;
            if (IsChanged)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Current data has changed, want to print please save." : "当前数据有更改,欲打印请先保存.", LocalData.IsEnglish ? "Tip" : "提示");
                return;
            }

            ProfitReportData data = OIReportSrvice.GetProfitReportData(_businessInfo.ID);
            if (data == null) return;

            data.PrintDate = DateTime.Now.ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            if (data.Fees != null && data.Fees.Count > 0)
            {
                data.TotalRevenue = 0.0M;
                data.TotalCost = 0.0M;
                data.TotalAgent = 0.0M;
                data.Profit = 0.0M;
                foreach (var item in data.Fees)
                {
                    data.TotalRevenue += (item.Revenue != null ? item.Revenue : 0.0M);
                    data.TotalCost += (item.Cost != null ? item.Cost : 0.0M);
                    data.TotalAgent += (item.agent != null ? item.agent : 0.0M);
                }

                data.Profit = data.TotalRevenue - data.TotalCost + data.TotalAgent;
            }

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Profit Report" : "打印利润表", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);
            string fileName = System.Windows.Forms.Application.StartupPath + "\\Reports\\AirImport\\";
            fileName += "RptProfit.frx";

            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("Common_ProfitReportData", data);
            reportSource.Add("Common_ProfitReportFeeData", data.Fees);

            viewer.BindData(fileName, reportSource, null);
        }

        /// <summary>
        /// 打印Authority
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barPrintAuthority_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_businessInfo == null || _businessInfo.ID == Guid.Empty) return;
            if (IsChanged)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Current data has changed, want to print please save." : "当前数据有更改,欲打印请先保存.", LocalData.IsEnglish ? "Tip" : "提示");
                return;
            }

            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            stateValues.Add("AirBusinessList", _businessInfo);
            string no = _businessInfo.No.Length <= 4 ? _businessInfo.No : _businessInfo.No.Substring(_businessInfo.No.Length - 4, 4);
            string title = (LocalData.IsEnglish ? "Print Authority To Make Entry" : "利润打印") + ("-" + no);
            PartLoader.ShowEditPart<OIBLPrintPart2>(Workitem, null, stateValues, title, null, null);
        }

        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (_businessInfo == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.AirImport;
            message.UserProperties.OperationId = _businessInfo.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = _businessInfo.ID;

            return message;
        }

        /// <summary>
        /// 账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_businessInfo == null || _businessInfo.ID == Guid.Empty) return;
                //if (_businessInfo.IsDirty)
                //{
                //    DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Current data has changed, want to print please save." : "当前数据有更改,欲打印请先保存.", LocalData.IsEnglish ? "Tip" : "提示");
                //    return;
                //}

                if (_businessInfo.MBLID == null || _businessInfo.MBLID.ToGuid() == Guid.Empty)
                {
                    Utility.ShowMessage(NativeLanguageService.GetText(this, "11091600001"));
                    return;
                }

                OperationCommonInfo operationCommonInfo = fcmCommonService.GetOperationCommonInfo(_businessInfo.ID, ICP.Framework.CommonLibrary.Common.OperationType.AirImport);
                if (operationCommonInfo != null)
                {
                    operationCommonInfo.CurrentFormID = _businessInfo.MBLID.ToGuid();
                    finClientService.ShowBillList(operationCommonInfo, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
                }
                else
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }
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
            this.cmbWareHouse.Properties.ReadOnly = !chkIsWarehouse.Checked;
            if (chkIsWarehouse.Checked)
            {
                this.cmbWareHouse.BackColor = System.Drawing.SystemColors.Info;
            }
            else
            {
                this.cmbWareHouse.BackColor = System.Drawing.Color.White;
            }
        }

        #region FilightNo Changed

        private void cmbFlightNo_SelectedRow(object sender, EventArgs e)
        {
            if (cmbFlightNo.EditValue != null && cmbFlightNo.EditValue.ToString().Length > 0)
            {
                FlightInfo flightInfo = tfService.GetFilghtInfo(new Guid(cmbFlightNo.EditValue.ToString()));
                //dtpETD.EditValue = _businessInfo.ETD = flightInfo.ETD;
                //dtpETA.EditValue = _businessInfo.ETA = flightInfo.ETA;

                if (mcmbAirCompany.EditValue != null && mcmbAirCompany.EditValue.ToString().Length > 0)
                {
                    if ((Guid)mcmbAirCompany.EditValue != flightInfo.AirlineID)
                    {
                        DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show("是否替换原有航空公司?",
                                                            "提示",
                                                           MessageBoxButtons.YesNo,
                                                           MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            _businessInfo.MBLInfo.AirCompanyID = flightInfo.AirlineID;
                            _businessInfo.MBLInfo.AirCompanyName = flightInfo.AirlineName;
                            this.mcmbAirCompany.ShowSelectedValue(this._businessInfo.MBLInfo.AirCompanyID, this._businessInfo.MBLInfo.AirCompanyName);
                        }
                    }
                }
                else
                {
                    _businessInfo.MBLInfo.AirCompanyID = flightInfo.AirlineID;
                    _businessInfo.MBLInfo.AirCompanyName = flightInfo.AirlineName;
                    this.mcmbAirCompany.ShowSelectedValue(this._businessInfo.MBLInfo.AirCompanyID, this._businessInfo.MBLInfo.AirCompanyName);
                }
            }

            if (!_businessInfo.IsDirty)
            {
                _businessInfo.IsDirty = true;
            }
        }

        #endregion

        #endregion

        #region 资源回收

        void OIBusinessEdit_Load(object sender, EventArgs e)
        {
            this.cmbCompany.Focus();
            this.SetTitle();
            this.RegisterRelativeEvents();
            this.RegisterRelativeEventsAndRunOnce();

            ///设置客户水印
            Utility.SetCustomerTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtCustomer,
                stxtConsignee,
                stxtShipper,
                stxtNotifyParty,
                stxtAgentOfCarrier,
                stxtFinalWareHouse,
                cmbWareHouse,
                cmbCustoms
            });

            //设置地点水印
            Utility.SetPortTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtPlaceOfDelivery,
                stxtDeparture,
                stxtDetination,
            });

            this.SmartPartClosing += new EventHandler<WorkspaceCancelEventArgs>(BusinessBaseEditPart_SmartPartClosing);
            this.ActivateSmartPartClosingEvent(this.Workitem);


            this.bsBusiness.BindingComplete += new BindingCompleteEventHandler(bsBusiness_BindingComplete);

        }

        void bsBusiness_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            _shown = true;
            this.bsBusiness.BindingComplete -= new BindingCompleteEventHandler(bsBusiness_BindingComplete);
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
    }    
}
