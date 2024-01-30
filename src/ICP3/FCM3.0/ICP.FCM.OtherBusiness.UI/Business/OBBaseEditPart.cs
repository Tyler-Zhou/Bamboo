using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects;
using System.Drawing;
using DevExpress.XtraEditors.Controls;
using ICP.Common.UI;
using ICP.FCM.Common.ServiceInterface.Common;
using DevExpress.XtraBars;


namespace ICP.FCM.OtherBusiness.UI.Business
{
    /// <summary>
    /// 其他业务编辑界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class OBBaseEditPart : BaseEditPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IDataFindClientService dfService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IOrganizationService organizationService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ITransportFoundationService tfService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ICustomerService customerService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IGeographyService geographyService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ExportUIHelper ExportUIHelper { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelperService { get; set; }

        [ServiceDependency]
        public ICP.FCM.OtherBusiness.ServiceInterface.IOtherBusinessService OBService { get; set; }


        [ServiceDependency]
        public ICP.Common.ServiceInterface.IConfigureService configureService { get; set; }

        [ServiceDependency]
        public ICP.FAM.ServiceInterface.IFinanceClientService finClientService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonClientService fcmCommonClientService { get; set; }


        [ServiceDependency]
        public ICP.FCM.OceanImport.ServiceInterface.IOceanImportService oiService { get; set; }

        #endregion

        #region 本地变量

        /// <summary>
        /// 是否是订单编辑页面
        /// </summary>
        protected virtual bool IsOrderEditPart
        {
            get { return false; }
        }

        CustomerType customerType = CustomerType.Unknown;
        bool _isSearching = false;

        List<CountryList> _countryList = null;

        /// <summary>
        /// 是否是远东区解决方案(根据当前登录人的默认公司所属解决方案)
        /// </summary>
        bool _isFarEastSolution = false;

        OtherBusinessInfo _OBInfo = null;
        OtherBusinessInfo _currentData;
        List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList> _con;
        /// <summary>
        /// 明细列表
        /// </summary>
        public List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList> Details
        {


            get
            {
                this.bsContainer.EndEdit();
                List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList> list = bsContainer.DataSource as List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList>;
                if (list == null)
                {
                    list = new List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList>();
                }
                return list;
            }
            set { bsContainer.DataSource = value; }
        }

        ITransportFoundationService ttService
        {
            get
            {
                if (Workitem == null)
                {
                    return null;
                }
                else
                {
                    return Workitem.Services.Get<ITransportFoundationService>();
                }
            }
        }
        #endregion

        #region 构造函数

        public OBBaseEditPart()
        {
            InitializeComponent();

            this.Disposed += new EventHandler(OBBaseEditPart_Disposed);
        }
        private void InitMessage()
        {
            this.RegisterMessage("1110170001", LocalData.IsEnglish ? "Are you sure to deleted the selected cost" : "确认删除选中的费用?");
            this.RegisterMessage("1110170002", LocalData.IsEnglish ? "Are you sure clear all cost" : "确认清空所有费用?");
        }

        #endregion


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                if (configureInfo.SolutionID == new Guid("b6e4dded-4359-456a-b835-e8401c910fd0"))
                {
                    _isFarEastSolution = true;
                }

                InitMessage();

                this.SetTitle();
                this.RegisterRelativeEvents();
                this.RegisterRelativeEventsAndRunOnce();

                #region 设置水印
                Utility.SetCustomerTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtCustomer,
                stxtAgentOfCarrier,
                stxtShipper,
                customerPopupContainerEdit1,
                lwNotifyParty,
                btnWare,
                btnCustom
            });

                Utility.SetPortTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtDetination,
                stxtDeparture,
                stxtDes
            });
                //Utility.SetVoyageTextEditNullValuePrompt(new List<TextEdit>
                //{
                //   stxtVesselVoyage
                //});

                this.SmartPartClosing += new EventHandler<WorkspaceCancelEventArgs>(OtherBusinessEditPart_SmartPartClosing);
                this.ActivateSmartPartClosingEvent(this.Workitem);
                this.chkIsCustoms.CheckedChanged += delegate { SetLocalServiceEnable(); };
                this.chkIsWarehouse.CheckedChanged += delegate { SetLocalServiceEnable(); };
            }
                #endregion

            int count = gridView1.RowCount;
            if (count > 0)
            {
                txtCount.Text = count.ToString();
            }
            barTruck.Enabled = chkIsTruck.Checked;
            chkIsTruck.CheckedChanged += delegate { if (chkIsTruck.Checked) { barTruck.Enabled = true; } else { barTruck.Enabled = false; } };

            SetPermissions();
        }

        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            if (Workitem.State["AddType"] != null)
            {
                AddType type = (AddType)Workitem.State["AddType"];
                if (type == AddType.OtherBusinessOrder)
                {
                    if (!ICP.Framework.CommonLibrary.Client.LocalCommonServices.PermissionService.HaveActionPermission(FCMPermissionsConstants.FCM_EditOrder))
                    {
                        this.barSave.Visibility = BarItemVisibility.Never;
                        this.barSaveAs.Visibility = BarItemVisibility.Never;
                    }
                    if (ICP.Framework.CommonLibrary.Client.LocalCommonServices.PermissionService.HaveActionPermission(FCMPermissionsConstants.FCM_NAServices)
                    && _currentData != null && _currentData.CreateByID != LocalData.UserInfo.LoginID)
                    {
                        this.barSave.Enabled = false;
                    }
                }
            }
        }


        void RegisterRelativeEventsAndRunOnce()
        {
            this.RunAtOnce();
        }
        public override void EndEdit()
        {
            Guid? voyageId = Guid.Empty;
            if (this.stxtVesselVoyage.EditValue != null && this.stxtVesselVoyage.EditValue != System.DBNull.Value)
            {
                //voyageId = (Guid?)this.stxtVesselVoyage.EditValue;
                voyageId = new Guid(this.stxtVesselVoyage.EditValue.ToString());
            }
            this._currentData.VoyageID = voyageId;
            this.Validate();
            this.bsOtherInfo.EndEdit();
        }
        #region
        /// <summary>
        /// 根据业务信息显示下拉式控件及其它一些控件的值
        /// </summary>

        private void InitControls()
        {
            //业务类型
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OtOperationType>> Types = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OtOperationType>(LocalData.IsEnglish);
            foreach (var item in Types)
            {
                cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            //业务类型
            this.cmbType.ShowSelectedValue(this._currentData.OtOperationType,
                EnumHelper.GetDescription<OtOperationType>(this._currentData.OtOperationType, LocalData.IsEnglish, true));
            //操作口岸(公司)
            cmbCompany.ShowSelectedValue(this._currentData.CompanyID, this._currentData.CompanyName);
            //重量（毛重单位）
            cmbWeightUnit.ShowSelectedValue(this._currentData.WeightUnitID, this._currentData.WeightUnitName);
            if (Utility.GuidIsNullOrEmpty(this._currentData.WeightUnitID)
                && this.cmbWeightUnit.EditValue != null)
            {
                this._currentData.WeightUnitID = new Guid(this.cmbWeightUnit.EditValue.ToString());
            }
            //体积
            cmbMeasurementUnit.ShowSelectedValue(this._currentData.MeasurementUnitID, this._currentData.MeasurementUnitName);
            if (Utility.GuidIsNullOrEmpty(this._currentData.MeasurementUnitID)
                && cmbMeasurementUnit.EditValue != null)
            {
                this._currentData.MeasurementUnitID = new Guid(this.cmbMeasurementUnit.EditValue.ToString());
            }
            //揽货人
            Utility.SetEnterToExecuteOnec(this.cmbSales, delegate
            {
                ExportUIHelper.SetMcmbUsers(cmbSales, true, true);
            });
            Utility.SetEnterToExecuteOnec(this.treeSelectBox1, delegate
            {
                SetSalesDepartment();
            });
            //揽货人
            this.cmbSales.ShowSelectedValue(this._currentData.SalesID, this._currentData.SalesName);
            if (Utility.GuidIsNullOrEmpty(this._currentData.SalesID)
                && cmbSales.EditValue != null)
            {
                this._currentData.SalesID = new Guid(this.cmbSales.EditValue.ToString());
            }
            //揽货部门
            this.treeSelectBox1.ShowSelectedValue(this._currentData.SalesDepartmentID, this._currentData.SalesDepartmentName);

            if (Utility.GuidIsNullOrEmpty(this._currentData.SalesDepartmentID)
               && treeSelectBox1.EditValue != null)
            {
                this._currentData.SalesDepartmentID = new Guid(this.treeSelectBox1.EditValue.ToString());
            }
            ////3个付款方式
            //cmbPaymentTerm.ShowSelectedValue(this._currentData.PaymentTermID, this._currentData.PaymentTermName);
            //船东
            multiSearchCommonBox1.ShowSelectedValue(this._currentData.CarrierID, this._currentData.CarrierName);
            if (Utility.GuidIsNullOrEmpty(this._currentData.CarrierID)
                && multiSearchCommonBox1.EditValue != null)
            {
                this._currentData.CarrierID = new Guid(this.multiSearchCommonBox1.EditValue.ToString());
            }
            //包装件数
            lwPackages.ShowSelectedValue(_currentData.QuantityUnitID, _currentData.QuantityUnitName);
            if (Utility.GuidIsNullOrEmpty(this._currentData.QuantityUnitID)
               && lwPackages.EditValue != null)
            {
                this._currentData.QuantityUnitID = new Guid(this.lwPackages.EditValue.ToString());
            }
            //3个付款方式
            cmbPaymentTerm.ShowSelectedValue(_currentData.PaymentTypeID, _currentData.PaymentTypeName);
            //操作
            this.treeOperation.ShowSelectedValue(this._currentData.OperatorID, this._currentData.OperatorName);
            //海外部客服
            this.mcmbOverseasFiler.ShowSelectedValue(this._currentData.OverseasFilerID, this._currentData.OverseasFilerName);
            this.orderFeeEditPart1.SetCompanyID(_currentData.CompanyID);
            InitalComboxes();
            //this.stxtReturnLocation.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
            SetLocalServiceEnable();

            //箱型
            if (ttService != null)
            {
                List<ICP.Common.ServiceInterface.DataObjects.ContainerList> containerList = ttService.GetContainerList(string.Empty, true, 0);

                foreach (ICP.Common.ServiceInterface.DataObjects.ContainerList container in containerList)
                {
                    cmbBoxType.Properties.Items.Add(new ImageComboBoxItem(container.Code, container.ID));
                }
            }
            //3个付款方式的下拉列表
            Utility.SetEnterToExecuteOnec(cmbPaymentTerm, delegate
            {
                List<DataDictionaryList> payments = ExportUIHelper.SetCmbDataDictionary(
                                                    cmbPaymentTerm,
                                                    DataDictionaryType.PaymentTerm, false, true);
            });
            //重量单位
            ExportUIHelper.SetCmbDataDictionary(Unit, DataDictionaryType.QuantityUnit);
            if (this._currentData.VoyageID != null)
            {

                this.stxtVesselVoyage.ShowSelectedValue(this._currentData.VoyageID, this._currentData.VesselVoyage);
            }
        }
        void SetLocalServiceEnable()
        {
            btnWare.Enabled = chkIsWarehouse.Checked;
            btnCustom.Enabled = chkIsCustoms.Checked;
            btnCustom.Properties.ReadOnly = chkIsWarehouse.Checked;
        }
        /// <summary>
        /// 驳船
        /// 筛选：装货港=当前装货港and卸货港=当前卸货港
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForSearchPreVoyage()
        {
            Guid polId = Guid.Empty;
            Guid podId = Guid.Empty;

            try
            {
                podId = (Guid)this.stxtDeparture.Tag;
            }
            catch
            {
                throw new Exception(LocalData.IsEnglish ? "Please select POL at first." : "请先选择起运港！");
            }

            if (polId == Guid.Empty)
            {
                throw new Exception(LocalData.IsEnglish ? "Please select P.O.R. at first." : "请先选择目的港！");
            }

            if (podId == Guid.Empty)
            {
                throw new Exception(LocalData.IsEnglish ? "Please select POL at first." : "请先选择起运港！");
            }

            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("PODID", this.stxtDeparture.Tag, false);
            conditions.AddWithValue("PODName", this.stxtDeparture.Text, false);
            return conditions;
        }
        #endregion

        /// <summary>
        /// 客户改变后需设置"订舱客户","收货人","代理","揽货方式","最近业务"
        /// </summary>
        /// <param name="customerType">客户的类型,请在方法外部获取</param>
        private void CustomerChanged(CustomerType? customerType)
        {
            //SetBookingCustomerByCustomerAndTradeTerm();
            //SetConsigneeByCustomerAndTradeTerm();
            //SetSalesTypeByCustomerAndCompany();
            //SetRecentlyOrderListByCustomerAndCompany();

            //SetAgentSourceByCompanyID(_currentData.CompanyID);
            //SetAgetnByCustomerAndCompany(customerType);

            //SetDefaultOverseasFiler();
            //SetDefaultFiler();
            //SetShipperByBookingCustomerAndTradeTerm();
        }

        CustomerFinderBridge shipperBridge;

        CustomerFinderBridge consigneeBridge;

        CustomerFinderBridge notifyBridge;
        CustomerFinderBridge bookingCustomerPartyBridge;
        LocationFinderBridge pfbPlaceOfReceipt;
        /// <summary>
        /// 搜索器注册
        /// </summary>
        void SearchRegister()
        {
            #region 船名航次
            //   dfService.Register(stxtVesselVoyage,
            //CommonFinderConstants.VesselVoyageFinder,
            //SearchFieldConstants.VesselVoyage,
            //SearchFieldConstants.VesselResultValue,
            //delegate(object inputSource, object[] resultData)
            //{
            //    stxtVesselVoyage.Tag = _currentData.VoyageID = new Guid(resultData[0].ToString());
            //    stxtVesselVoyage.Text = _currentData.VesselVoyage = resultData[2].ToString() + "/" + resultData[1].ToString();
            //    VoyageChanged();
            //},
            //delegate
            //{
            //    stxtVesselVoyage.Text = _currentData.VesselVoyage = string.Empty;
            //    stxtVesselVoyage.Tag = _currentData.VoyageID = null;

            //    VoyageChanged();
            //},
            //ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion
            #region Customer

            //Customer
            dfService.Register(stxtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.ResultValue,
                GetConditionsForCustomer,
                      delegate(object inputSource, object[] resultData)
                      {
                          Guid? oldCustomerId = this._currentData.CustomerID;
                          stxtCustomer.ClosePopup();

                          CustomerStateType state = (CustomerStateType)resultData[5];
                          if (state == CustomerStateType.Invalid)
                          {
                              if (PartLoader.PopCustomerIsInvalid() != DialogResult.Yes)
                              {
                                  return;
                              }
                          }

                          CustomerCodeApplyState? approved = (CustomerCodeApplyState?)resultData[6];
                          if (!approved.HasValue
                              || (approved.HasValue && approved.Value != CustomerCodeApplyState.Passed))
                          {
                              if (_isFarEastSolution)
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
                                      if ((resultData[8] == null ||
                                          string.IsNullOrEmpty(resultData[8].ToString())) &&
                                          (resultData[9] == null ||
                                          string.IsNullOrEmpty(resultData[9].ToString())))
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
                                                                            (DateTime?)resultData[7]);
                                      }

                                      return;
                                  }
                                  else if (approved.Value == CustomerCodeApplyState.Unpassed)
                                  {
                                      if ((resultData[8] == null ||
                                          string.IsNullOrEmpty(resultData[8].ToString())) &&
                                          (resultData[9] == null ||
                                          string.IsNullOrEmpty(resultData[9].ToString())))
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
                                                                            (DateTime?)resultData[7]);
                                      }

                                      return;
                                  }
                              }
                              else
                              {
                                  if (approved.Value == CustomerCodeApplyState.Processing || approved.Value == CustomerCodeApplyState.Unpassed)
                                  {
                                      if (PartLoader.PopCustomerUnApproved() != DialogResult.Yes)
                                      {
                                          return;
                                      }
                                  }

                                  if (approved.Value == CustomerCodeApplyState.UnApply)
                                  {
                                      DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The customer have not yet applied for the code. Whether to apply the code?" : "该客户尚未申请代码，是否要申请代码?"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.YesNo
                  , MessageBoxIcon.Question);
                                      if (result == DialogResult.Yes)
                                      {
                                          customerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                            LocalData.UserInfo.LoginID,
                                                                            LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                            (DateTime?)resultData[7]);
                                      }
                                  }
                              }
                          }

                          stxtCustomer.EditValue = _currentData.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                          stxtCustomer.Tag = _currentData.CustomerID = new Guid(resultData[0].ToString());

                          if (oldCustomerId != Guid.Empty && _currentData.CustomerID == oldCustomerId) return;

                          CustomerType customerType = (CustomerType)resultData[4];

                          CustomerChanged(customerType);


                      }, delegate
                      {
                          stxtCustomer.Text = _currentData.CustomerName = string.Empty;
                          stxtCustomer.Tag = _currentData.CustomerID = Guid.Empty;
                          stxtCustomer.ClosePopup();
                          CustomerChanged(null);
                      },
                      ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            dfService.Register(stxtAgentOfCarrier, CommonFinderConstants.CustomerAgentOfCarrierFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
               delegate(object inputSource, object[] resultData)
               {
                   stxtAgentOfCarrier.Text = _currentData.AgengofCarrierName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                   stxtAgentOfCarrier.Tag = _currentData.AgentofCarrierID = new Guid(resultData[0].ToString());
               }, Guid.Empty, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

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
              _currentData.ShipperDescription,
               ICPCommUIHelperService,
               LocalData.IsEnglish);
                shipperBridge.Init();
            });
            stxtShipper.OnOk += new EventHandler(stxtShipper_OnOk);

            //lwNotifyParty
            Utility.SetEnterToExecuteOnec(lwNotifyParty, delegate
            {
                if (_countryList == null) _countryList = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                notifyBridge = new CustomerFinderBridge(
               this.lwNotifyParty,
               _countryList,
               this.dfService,
               this.customerService,
               _currentData.NotifyDescription,
               ICPCommUIHelperService,
               LocalData.IsEnglish);
                notifyBridge.Init();
            });

            lwNotifyParty.OnOk += new EventHandler(lwNotifyParty_OnOk);
            //Consignee
            Utility.SetEnterToExecuteOnec(this.customerPopupContainerEdit1, delegate
            {
                if (_countryList == null) _countryList = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                consigneeBridge = new CustomerFinderBridge(
                this.customerPopupContainerEdit1,
                _countryList,
                this.dfService,
                this.customerService,
                _currentData.ConsigneeDescription,
                ICPCommUIHelperService,
                LocalData.IsEnglish);
                consigneeBridge.Init();
            });

            customerPopupContainerEdit1.OnOk += new EventHandler(customerPopupContainerEdit1_OnOk);

            ////BookingCustomer
            //Utility.SetEnterToExecuteOnec(stxtBookingCustomer, delegate
            //{
            //    if (_countryList == null) _countryList = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);

            //    bookingCustomerPartyBridge = new CustomerFinderBridge(
            //    this.stxtBookingCustomer,
            //    _countryList,
            //    this.dfService,
            //    this.customerService,
            //    _currentData.BookingCustomerDescription,
            //    AirExportUIHelper,
            //    LocalData.IsEnglish);
            //    bookingCustomerPartyBridge.Init();
            //});
            //仓库
            this.dfService.Register(this.btnWare, CommonFinderConstants.CustoemrFinder,
                SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                this.GetConditionsForWarehouse,
                delegate(object inputSource, object[] resultData)
                {
                    btnWare.Tag = this._currentData.WarehouseID = (Guid)resultData[0];
                    btnWare.EditValue = this._currentData.WarehouseName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate
                {
                    btnWare.Tag = this._currentData.WarehouseID = null;
                    btnWare.EditValue = this._currentData.WarehouseName = string.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            //报关行
            this.dfService.Register(this.btnCustom, CommonFinderConstants.CustoemrFinder,
                SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                this.GetConditionsForBroker,
                delegate(object inputSource, object[] resultData)
                {
                    btnCustom.Tag = this._currentData.CustomsBrokerID = (Guid)resultData[0];
                    btnCustom.EditValue = this._currentData.CustomsBrokerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate
                {
                    btnCustom.Tag = this._currentData.CustomsBrokerID = null;
                    btnCustom.EditValue = this._currentData.CustomsBrokerName = string.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion

            #region Port
            PortFinderBridge pfbPOL = new PortFinderBridge(this.stxtDeparture, this.dfService, LocalData.IsEnglish);

            PortFinderBridge pfbPOD = new PortFinderBridge(this.stxtDetination, this.dfService, LocalData.IsEnglish);

            LocationFinderBridge pfbPlaceOfDelivery = new LocationFinderBridge(this.stxtDes, this.dfService, LocalData.IsEnglish);
            #endregion

            #region RefNo

            //业务搜索器       
            dfService.Register(stxtOperationNo, FCMFinderConstants.BusinessFinderForOI, SearchFieldConstants.BusinessNo, SearchFieldConstants.BusinessResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      //btnBusiness.Text = _currentData.OperationNo = resultData[1].ToString();
                      //btnBusiness.Tag = _currentData.OperationID = new Guid(resultData[0].ToString());
                      Guid bookingID = new Guid(resultData[0].ToString());
                      if (_currentData.OperationID != bookingID)
                      {
                          AfterSearchRefNo(bookingID);
                      }
                      else
                      {
                          LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ?
                        "You has been selected this booking." : "您已选择了该票业务!");
                      }
                  },
                  delegate
                  {
                      //btnBusiness.Text = _currentData.OperationNo = string.Empty;
                      //btnBusiness.Tag = _currentData.OperationID = Guid.Empty;
                      if (_currentData.IsNew)
                      {
                          stxtOperationNo.Tag = _currentData.OperationID = Guid.Empty;
                          stxtOperationNo.Text = _currentData.OperationNo = string.Empty;
                      }

                  }, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            #endregion
        }

        /// <summary>
        /// bug2972: 业务写订单时，选择客户，只能选择自己有权限的CRM关联的公共客户。
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForCustomer()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            if (IsOrderEditPart)
            {
                conditions.AddWithValue("IsFromOrder", true, false);
                conditions.AddWithValue("CurruntUserID", LocalData.UserInfo.LoginID, false);
            }

            return conditions;
        }

        void customerPopupContainerEdit1_OnOk(object sender, EventArgs e)
        {

            if (customerPopupContainerEdit1.CustomerDescription != null)
                _currentData.ConsigneeDescription = customerPopupContainerEdit1.CustomerDescription;
        }

        void lwNotifyParty_OnOk(object sender, EventArgs e)
        {
            if (lwNotifyParty.CustomerDescription != null)
                _currentData.NotifyDescription = lwNotifyParty.CustomerDescription;
        }

        void stxtShipper_OnOk(object sender, EventArgs e)
        {
            if (stxtShipper.CustomerDescription != null)
                _currentData.ShipperDescription = stxtShipper.CustomerDescription;
        }


        /// 值改变失去焦点后，需要刷新到港日，如果并且驳船为空，需要刷新离港日、截柜日、截关日、截文件日
        /// </summary>
        private void VoyageChanged()
        {
            if (Utility.GuidIsNullOrEmpty(_currentData.VoyageID))
            {
                dteETA.EditValue = _currentData.Eta = null;
                dteETD.EditValue = _currentData.Etd = null;
                return;
            }

            //VoyageInfo voyageInfo = tfService.GetVoyageInfo(_currentData.VoyageID.Value);
            //dteETA.EditValue = _currentData.Eta = voyageInfo.ETA;
            //dteETD.EditValue = _currentData.Etd = voyageInfo.ETD;

        }

        public void SetSource(object value)
        {
            if (value == null) return;
            _isChanged = false;
            this.bsContainer.DataSource = value; bsContainer.ResetBindings(false);
        }

        public void SetService(WorkItem workitem)
        {
            Workitem = workitem;
            InitControls();
        }
        public void BindingData1(object data)
        {
            _currentData = bsOtherInfo.DataSource as OtherBusinessInfo;
            List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList> list = data as List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList>;
            if (_currentData != null)
            {
                GetConData(_currentData.ID);
            }
        }
        public void BindingData(object data)
        {
            //AirBookingInfo currentData = bsBookingInfo.DataSource as AirBookingInfo;
            //currentData.ContractID = Guid.Empty;//合约号暂时没有
            this.SuspendLayout();
            this.orderFeeEditPart1.SetService(Workitem);
            //this.bookingPOEditPart1.SetService(Workitem);

            OtherBusinessList listInfo = data as OtherBusinessList;

            if (listInfo == null)
            {
                //新建
                this._currentData = new OtherBusinessInfo();
                //ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList con = new ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList();
                //bsContainer.DataSource = con;
                //this.cmbSales.Enabled = false;
                this.cmbSales.ShowSelectedValue(LocalData.UserInfo.LoginID, LocalData.IsEnglish ? LocalData.UserInfo.LoginName : LocalData.UserInfo.UserName);

                // _currentData.SalesID = LocalData.UserInfo.LoginID;
                //_currentData.SalesName = (LocalData.IsEnglish ? LocalData.UserInfo.LoginName : LocalData.UserInfo.UserName);
                SetCon();
                this.ReadyForNew();
            }
            else
            {
                this.GetData(listInfo.ID);
                GetConData(listInfo.ID);
                RefreshBarEnabled();

                if (listInfo.EditMode == EditMode.Copy)
                {
                    PrepareForCopyExistOrder();
                }
            }

            this._currentData.CancelEdit();

            this.InitalComboxes();
            ShowOrder();

            this.SearchRegister();
            SetLocalServiceEnable();
            this.SetLazyLoaders();

            this.ResumeLayout(true);
        }
        public override bool SaveData()
        {
            return this.Save(this._currentData, false);
        }
        void GetData(Guid orderId)
        {
            this._currentData = OBService.GetOtherBusinessInfo(orderId, LocalData.IsEnglish);
        }
        public object SetCon()
        {
            List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList> con = new List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList>();
            bsContainer.DataSource = con;
            return bsContainer.DataSource as List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList>;
        }
        List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList> GetConData(Guid orderId)
        {
            if (orderId != Guid.Empty && orderId != null)
            {
                _con = OBService.GetOtherContainerList(orderId, LocalData.IsEnglish);
                bsContainer.DataSource = _con;
            }
            return bsContainer.DataSource as List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList>;
        }

        #region 复制订单时的逻辑

        /// <summary>
        /// 复制订单时的逻辑
        /// </summary>
        void PrepareForCopyExistOrder()
        {
            this._currentData.ID = Guid.Empty;
            this._currentData.State = OBOrderState.NewOrder;
            this._currentData.NO = string.Empty;
            this._currentData.CreateByID = LocalData.UserInfo.LoginID;
            this._currentData.CreateByName = LocalData.UserInfo.LoginName;
            this._currentData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

            AddType type = (AddType)Workitem.State["AddType"];
            if (type == AddType.OtherBusinessOrder)
            {
                this._currentData.SalesID = LocalData.UserInfo.LoginID;
                this._currentData.SalesName = LocalData.UserInfo.LoginName;

                this._currentData.SalesDepartmentID = LocalData.UserInfo.DefaultDepartmentID;
                this._currentData.SalesDepartmentName = LocalData.UserInfo.DefaultDepartmentName;
            }
            else
            {


                this._currentData.OperationID = LocalData.UserInfo.DefaultCompanyID;
                this._currentData.OverseasFilerID = LocalData.UserInfo.LoginID;
                this._currentData.OverseasFilerName = LocalData.UserInfo.LoginName;
            }
        }

        #endregion
        #region 刷新工具栏按钮的可使用性
        /// <summary>
        /// 总调用处，会把其它方法都执行一遍
        /// </summary>
        void RunAtOnce()
        {
            stxtOperationNo.Enabled = false;
            if (!this.chkIsTruck.Enabled)
            {
                this.chkIsTruck.Checked = this._currentData.IsTruck = false;
            }

            this.RefreshBarEnabled();
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.EditValue != null && (OtOperationType)cmbType.EditValue == OtOperationType.Booking)
            {
                stxtOperationNo.Enabled = true;
            }
            else
            {
                stxtOperationNo.Enabled = false;
            }
        }

        void RefreshBarEnabled()
        {
            //this.barAuditAndSave.Enabled = this._currentData.State == OrderState.NewOrder;

            //if (_currentData.ID == Guid.Empty)
            //{
            //    barReject.Enabled = barE_Booking.Enabled = false;
            //    this.barTruck.Enabled = false;

            //    this.barApplyAgent.Enabled = false;
            //    this.barRefresh.Enabled = false;
            //}
            //else
            //{
            //    barReject.Enabled = _currentData.State == OrderState.NewOrder;

            //    this.barRefresh.Enabled = true;


            //}

            this.txtState.Text = EnumHelper.GetDescription<OBOrderState>(this._currentData.State, LocalData.IsEnglish);
            cmbType.Text = EnumHelper.GetDescription<OtOperationType>(this._currentData.OtOperationType, LocalData.IsEnglish);
        }

        #endregion

        void ShowOrder()
        {
            if (this._currentData.ConsigneeDescription == null)
            {
                this._currentData.ConsigneeDescription = new CustomerDescription();
            }
            if (this._currentData.ShipperDescription == null)
            {
                this._currentData.ShipperDescription = new CustomerDescription();
            }
            if (this._currentData.NotifyDescription == null)
            {
                this._currentData.NotifyDescription = new CustomerDescription();
            }

            this.bsOtherInfo.DataSource = _currentData;
            bsOtherInfo.ResetBindings(false);
            bsOtherInfo.CancelEdit();

            InitControls();

            List<OBFeeList> feelist = null;

            if (this._currentData.ID == Guid.Empty)
            {
                feelist = new List<OBFeeList>();
            }
            else
            {
                feelist = OBService.GetOBOrderFeeList(this._currentData.ID, LocalData.IsEnglish);
            }

            this.orderFeeEditPart1.SetSource(feelist);
        }


        #region 注册各种联动的事件

        /// <summary>
        /// 注册各种联动的事件
        /// </summary>
        void RegisterRelativeEvents()
        {
            this.panelScroll.Click += delegate
            {
                this.panelScroll.Focus();
            };

            if (this._currentData.ID == Guid.Empty)
            {

                this.stxtCustomer.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.stxtCustomer_ButtonClick);
            }


            this.cmbSales.SelectedRow += new EventHandler(mcmbSales_SelectedRow);
            this.treeSelectBox1.Enter += new EventHandler(treeSelectBox1_Enter);
        }
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
        SearchConditionCollection GetConditionsForBroker()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Broker, false);
            return conditions;
        }
        /// <summary>
        /// 当前用户所在的操作口岸和揽货人所在的部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void treeSelectBox1_Enter(object sender, EventArgs e)
        {
            List<OrganizationList> userCompanyList = new List<OrganizationList>();

            AddType type = (AddType)Workitem.State["AddType"];
            if (type == AddType.OtherBusinessOrder && this._currentData.SalesID != null)
            {
                userCompanyList = userService.GetUserCompanyList(this._currentData.SalesID.Value, null);
            }
            else
            {
                userCompanyList = organizationService.GetOrganizationList(string.Empty, string.Empty, true, 0);
            }

            treeSelectBox1.SetSource<OrganizationList>(userCompanyList, LocalData.IsEnglish ? "EShortName" : "CShortName");
        }
        void mcmbSales_SelectedRow(object sender, EventArgs e)
        {
            if (Utility.GuidIsNullOrEmpty(this._currentData.SalesID) == false)
            {
                List<UserOrganizationTreeList> orgList = userService.GetUserOrganizationTreeList(this._currentData.SalesID.Value);

                OrganizationList orginazation = orgList.Find(delegate(UserOrganizationTreeList item) { return item.IsDefault && item.Type == OrganizationType.Department; });
                if (orginazation != null)
                {
                    this.treeSelectBox1.ShowSelectedValue(orginazation.ID, LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName);
                    _currentData.SalesDepartmentID = orginazation.ID;
                    _currentData.SalesDepartmentName = LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName;
                }
                else
                {
                    this.treeSelectBox1.ShowSelectedValue(Guid.Empty, string.Empty);
                    _currentData.SalesDepartmentID = Guid.Empty;
                    _currentData.SalesDepartmentName = string.Empty;
                }
            }
        }
        #endregion
        /// <summary>
        /// 弹出最近十票业务
        /// </summary>
        private void stxtCustomer_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (e.Button.Kind != DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) return;

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

        #region 延迟加载的数据源

        List<DataDictionaryList> _weightUnits;
        void InitalComboxes()
        {
            //包装件数
            ExportUIHelper.SetCmbDataDictionary(lwPackages, DataDictionaryType.QuantityUnit);

            //重量
            _weightUnits = ExportUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit);
            //体积
            List<DataDictionaryList> volUnitss = ExportUIHelper.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit);
        }
        /// <summary>
        /// 为需要延迟加载数据源的控件注册事件
        /// 这些数据源不会和其它控件发生联动，加载一次即可
        /// </summary>
        void SetLazyLoaders()
        {
            //操作口岸列表   
            Utility.SetEnterToExecuteOnec(cmbCompany, delegate
            {
                ICPCommUIHelperService.BindCompanyByAll(cmbCompany, false);

                if (Utility.GuidIsNullOrEmpty(this._currentData.CompanyID))
                {
                    _currentData.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                }

                cmbCompany.SelectedIndexChanged += delegate
                {
                    CompanyChanged();
                };
            });


            ////3个付款方式的下拉列表
            //Utility.SetEnterToExecuteOnec(cmbPaymentTerm, delegate
            //{
            //    List<DataDictionaryList> payments = AirExportUIHelper.SetCmbDataDictionary(
            //                                        cmbPaymentTerm,
            //                                        DataDictionaryType.PaymentTerm, false, true);
            //});

            //船东
            Utility.SetEnterToExecuteOnec(multiSearchCommonBox1, delegate
            {
                List<CustomerList> carriers = ExportUIHelper.SetMCmbCarrier(multiSearchCommonBox1);
            });




            this.treeOperation.Enter += new EventHandler(treeOperation_Click);
            this.mcmbOverseasFiler.Enter += new EventHandler(mcmbOverseasFiler_Enter);
        }

        private void mcmbOverseasFiler_Enter(object sender, EventArgs e)
        {
            if (cmbCompany.EditValue == null) return;
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = new Guid(this.cmbCompany.EditValue.ToString());
            }

            ICPCommUIHelperService.SetComboxUsersByRole(this.mcmbOverseasFiler, depID, "海外拓展", true);
        }

        /// <summary>
        /// 填充“操作”的用户列表供选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void treeOperation_Click(object sender, EventArgs e)
        {
            //ExportUIHelper.SetUsersList(treeOperation, true,
            //    (lwImageComboBoxEdit3.EditValue == null || string.IsNullOrEmpty(lwImageComboBoxEdit3.EditValue.ToString())) ? Guid.Empty : (Guid)lwImageComboBoxEdit3.EditValue,
            //    "操作",
            //    false);   
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = new Guid(this.cmbCompany.EditValue.ToString());
            }

            ICPCommUIHelperService.SetComboxUsersByRoles(this.treeOperation, depID, new string[] { "订舱", "客服", "文件" }, true);
        }

        /// <summary>
        /// 公司改变
        /// </summary>
        private void CompanyChanged()
        {
            SetOperatorByCompany();

            this.orderFeeEditPart1.SetCompanyID(this._currentData.CompanyID);
        }
        /// <summary>
        /// 根据操作口岸ID设置操作和文件栏的数据源
        /// </summary>
        private void SetOperatorByCompany()
        {
            if (Utility.GuidIsNullOrEmpty(_currentData.CompanyID))
            {
            }
            else
            {
                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                List<UserList> operators = userService.GetUnderlingUserList(new Guid[] { _currentData.CompanyID }, new string[] { "操作" }, null, true);
            }
        }
        #endregion
        #region SetSalesDepartment

        /// <summary>
        /// 改变失去焦点后刷新揽货部门，如果有多个就清空，否则填充
        /// </summary>
        private void SetSalesDepartment()
        {
            List<OrganizationList> userOrganizationTreeLists1 = new List<OrganizationList>();
            if (Utility.GuidIsNullOrEmpty(this._currentData.SalesID) == false)
            {
                userOrganizationTreeLists1 = userService.GetUserCompanyList(this._currentData.SalesID.Value, null);

                OrganizationList orginazation = userOrganizationTreeLists1.Find(delegate(OrganizationList item) { return item.IsDefault && item.Type == OrganizationType.Department; });
                if (orginazation != null)
                {
                    this.treeSelectBox1.ShowSelectedValue(orginazation.ID, LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName);
                    _currentData.SalesDepartmentID = orginazation.ID;
                    _currentData.SalesDepartmentName = LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName;
                }
                else
                {
                    this.treeSelectBox1.ShowSelectedValue(Guid.Empty, string.Empty);
                    _currentData.SalesDepartmentID = Guid.Empty;
                    _currentData.SalesDepartmentName = string.Empty;
                }
            }
        }

        void cmbSalesDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 清空Sales后,自动清空Sales部门.操作
        /// </summary>
        private void ClearSalesDepartment()
        {
            this._currentData.SalesDepartmentID = Guid.Empty;
            _currentData.OperatorName = null;//操作
        }

        #endregion

        public override object DataSource
        {
            get { return this.bsOtherInfo.DataSource; }
            set
            {

                BindingData(value);
            }

        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //if (CurrentOrderList == null)
            //{
            //    return;
            //}

            //DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "是否覆盖当前页面数据?" : "是否覆盖当前页面数据?"
            //                  , LocalData.IsEnglish ? "Tip" : "提示"
            //                  , MessageBoxButtons.YesNo
            //                  , MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    AirBookingInfo order = aeService.GetAirBookingInfo(CurrentOrderList.ID);
            //    if (order == null) return;

            //    order.ID = Guid.Empty;

            //    order.No = string.Empty;

            //    order.State = OrderState.NewOrder;

            //    //order.AirShippingOrderID = Guid.Empty;
            //    //order.AirShippingOrderNo = string.Empty;
            //    order.SalesID = LocalData.UserInfo.LoginID;
            //    order.SalesName = LocalData.UserInfo.LoginName;
            //    order.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            //    this._oceanBookingInfo = order;

            //    this.ShowOrder();
            //    this.RunAtOnce();
            //    this.ResetDescription();

            //    this.EndEdit();

            //    this.Invalidate();
            //}
        }
        void ReadyForNew()
        {
            OtherBusinessInfo newData = new OtherBusinessInfo();
            //newData.SalesID = LocalData.UserInfo.LoginID;
            //newData.SalesName = LocalData.UserInfo.LoginName;

            //newData.ContactID = Guid.NewGuid();
            //newData.ContactName = Workitem.State["ContactName"] == null ? "" : Workitem.State["ContactName"].ToString();

            AddType type = (AddType)Workitem.State["AddType"];
            newData.CompanyID = LocalData.UserInfo.DefaultCompanyID;
            string userName = LocalData.UserInfo.LoginName;
            if (!LocalData.IsEnglish)
            {
                userName = LocalData.UserInfo.UserName;
            }
            newData.CompanyName = LocalData.UserInfo.DefaultCompanyName;
            //业务列表新增
            if (type == AddType.OtherBusiness)
            {
                newData.OperatorID = LocalData.UserInfo.LoginID;
                newData.OperatorName = userName;
                this.cmbSales.Enabled = true;
            }
            //其他业务订单新增
            else if (type == AddType.OtherBusinessOrder)
            {
                newData.SalesID = LocalData.UserInfo.LoginID;
                newData.SalesName = userName;
                newData.SalesDepartmentID = LocalData.UserInfo.DefaultDepartmentID;
                newData.SalesDepartmentName = LocalData.UserInfo.DefaultDepartmentName;
                this.cmbSales.Enabled = false;
            }

            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newData.State = OBOrderState.NewOrder;
            //newData.CargoDescription = new ICP.FCM.Common.ServiceInterface.DataObjects.CargoDescription();
            newData.IsValid = true;

            #region 设置默认值
            DataDictionaryList normalDictionary = null;
            //normalDictionary = AirExportUIHelper.GetNormalDictionary(DataDictionaryType.PaymentTerm);
            //newData.PaymentTermID = normalDictionary.ID;
            //newData.PaymentTermName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ExportUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
            newData.QuantityUnitID = normalDictionary.ID;
            newData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ExportUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
            newData.WeightUnitID = normalDictionary.ID;
            newData.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
            newData.OperationDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newData.Feta = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            normalDictionary = ExportUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
            newData.MeasurementUnitID = normalDictionary.ID;
            newData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
            #endregion

            this._currentData = newData;


            // TODO: 这种Guard型的逻辑要在最开始的时候完成
            Utility.EnsureDefaultCompanyExists(this.userService);


            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
        }
        #region 清理资源和避免多余操作

        void OBBaseEditPart_Disposed(object sender, EventArgs e)
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


        #region 关闭
        void OBBaseEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (this._currentData.IsDirty &&
                this.barSave.Visibility == BarItemVisibility.Always && 
                this.barSave.Enabled) 
            {
                DialogResult dr = PartLoader.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!this.Save(this._currentData, false))
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
        #endregion
        #region 验证界面输入

        private bool ValidateData()
        {
            this.EndEdit();
            this.dxErrorProvider1.ClearErrors();
            _currentData = bsOtherInfo.DataSource as OtherBusinessInfo;
            List<bool> isScrrs = new List<bool> { true, true };

            isScrrs[0] = _currentData.Validate
               (
                   delegate(ValidateEventArgs e)
                   {
                       if (_currentData.PolID == _currentData.PodID)
                       {

                           if (_currentData.PolID != Guid.Empty || _currentData.PodID != Guid.Empty)
                           {
                               if (_currentData.PolID != null && _currentData.PodID != null)
                               {
                                   e.SetErrorInfo("PODID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");
                               }
                           }

                       }
                       if (_currentData.Eta != null && _currentData.Etd != null
                           && _currentData.Etd >= _currentData.Eta)
                           e.SetErrorInfo("ETA", LocalData.IsEnglish ? "ETD can't bigger ETA." : "ETD不能大于ETA.");
                   }
               );

            #region ContainerDemand

            #endregion

            #region childParts

            #endregion

            bool isScrr = true;
            foreach (var item in isScrrs)
                if (item == false) isScrr = false;

            if (isScrrs[0] == false)
                xtraTabControl1.SelectedTabPageIndex = 0;

            if (!this.orderFeeEditPart1.ValidateData())
            {
                isScrr = false;
                xtraTabControl1.SelectedTabPageIndex = 0;
            }

            return isScrr;
        }

        #endregion
        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;
        private bool Save(OtherBusinessInfo currentData, bool isSavingAs)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //_currentData = bsOtherInfo.DataSource as OtherBusinessInfo;
                if (ValidateData() == false)
                {
                    return false;
                }
                if (ConValidateData() == false)
                {
                    return false;
                }
                if (!currentData.IsDirty && !currentData.IsNew && !this.orderFeeEditPart1.IsChanged && !IsChanged)
                {
                    return true;
                }
                try
                {
                    OtherBusinessSaveRequest originalBooking = null;
                    if (Utility.GuidIsNullOrEmpty(currentData.ID))
                    {
                        originalBooking = SaveOtherBusiness(currentData);
                    }
                    else if (_currentData.IsDirty)
                    {
                        //if (_currentData.OperationDate != null)
                        //    _currentData.State = OrderState.BookingConfirmed;

                        originalBooking = SaveOtherBusiness(_currentData);
                    }

                    List<FeeSaveRequest> originalFees = this.orderFeeEditPart1.SaveFee(_currentData.ID);
                    List<ContainerSaveRequest> _container = null;
                    //if (currentData.ID != Guid.Empty)
                    //{
                    _container = this.SaveContainer(_currentData.ID);
                    //}
                    //else { _container = new List<ContainerSaveRequest>(); }
                    Dictionary<Guid, SaveResponse> saved = this.OBService.SaveOtherBusinessWithTrans(originalBooking,
                        originalFees, _container, LocalData.IsEnglish);
                    //基本信息
                    if (originalBooking != null)
                    {
                        SaveResponse.Analyze(new List<SaveRequest> { originalBooking }, saved, true);
                        this.RefreshUI(originalBooking);
                    }
                    //费用信息
                    if (originalFees != null)
                    {
                        SaveResponse.Analyze(originalFees.Cast<SaveRequest>().ToList(), saved, true);
                        this.orderFeeEditPart1.RefreshUI(originalFees);
                    }
                    //箱信息
                    if (_container != null)
                    {
                        SaveResponse.Analyze(_container.Cast<SaveRequest>().ToList(), saved, true);
                        this.RefreshContainerUI(_container);
                    }
                    if (isSavingAs)
                    {
                    }
                    else
                    {
                        AfterSave();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                    return false;
                }
            }
        }

        void OtherBusinessEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (this._currentData.IsDirty)
                {
                    DialogResult dr = PartLoader.EnquireIsSaveCurrentDataByUpdated();

                    if (dr == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                    else if (dr == DialogResult.Yes)
                    {
                        if (!this.Save(this._currentData, false))
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
        }

        void TriggerSavedEvent()
        {
            if (Saved != null)
            {
                this._currentData.SalesName = this._currentData.SalesID.ToGuid() == Guid.Empty ?
                    string.Empty : this.cmbSales.EditText;
                this._currentData.OperatorName = this._currentData.OperatorID.ToGuid() == Guid.Empty ?
                    string.Empty : this.treeSelectBox1.Text;
                //this._currentData.BookingerName = this._currentData.BookingerID.ToGuid() == Guid.Empty ?
                //    string.Empty : this.mcmbBookinger.Text;
                //this._currentData.OverSeasFilerName = this._currentData.OverSeasFilerID.ToGuid() == Guid.Empty ?
                //    string.Empty : this.mcmbOverseasFiler.Text;
                //this._currentData.VesselVoyage = this.stxtPreVoyage.Text + (this.stxtPreVoyage.Text.Trim().Length > 0 ? ";" : "") + this.stxtVoyage.Text;
                Saved(new object[] { (OtherBusinessList)_currentData });

                this._currentData.IsDirty = false;
            }
        }
        private void AfterSave()
        {
            _currentData.CancelEdit();
            _currentData.BeginEdit();

            if (_currentData.ConsigneeDescription != null)
            {
                _currentData.ConsigneeDescription.IsDirty = false;
            }
            if (_currentData.ShipperDescription != null)
            {
                _currentData.ShipperDescription.IsDirty = false;
            }
            if (_currentData.NotifyDescription != null)
            {
                _currentData.NotifyDescription.IsDirty = false;
            }


            this.TriggerSavedEvent();
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

            this.SetTitle();
        }
        void RefreshUI(OtherBusinessSaveRequest saveRequest)
        {
            SingleResult result = saveRequest.SingleResult;

            OtherBusinessInfo currentData = saveRequest.UnBoxInvolvedObject<OtherBusinessInfo>()[0];

            currentData.ID = result.GetValue<Guid>("ID");
            currentData.NO = result.GetValue<string>("No");
            currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            currentData.State = (OBOrderState)result.GetValue<byte>("State");
            currentData.IsDirty = false;

            if (currentData.ConsigneeDescription != null)
            {
                currentData.ConsigneeDescription.IsDirty = false;
            }
            if (currentData.ShipperDescription != null)
            {
                currentData.ShipperDescription.IsDirty = false;
            }
            if (currentData.NotifyDescription != null)
            {
                currentData.NotifyDescription.IsDirty = false;
            }

        }
        void SetTitle()
        {
            if (this._currentData == null || this._currentData.ID == Guid.Empty)
            {
                this.Title = LocalData.IsEnglish ? "Add Business" : "新增业务信息";
            }
            else
            {
                string titleNo = string.Empty;

                if (this._currentData.NO.Length > 4)
                {
                    titleNo = this._currentData.NO.Substring(this._currentData.NO.Length - 4, 4);
                }
                else
                {
                    titleNo = this._currentData.NO;
                }

                this.Title = LocalData.IsEnglish ? "Edit Business " + titleNo : "编辑业务信息：" + titleNo;
            }
        }

        #region 搜索业务号后把Booking的数据填充到当前页面
        /// <summary>
        /// 搜索业务号后把Booking的数据填充到当前页面
        /// </summary>
        private void AfterSearchRefNo(Guid bookingID)
        {
            #region 	如果之前已选择业务号

            if (Utility.GuidIsNullOrEmpty(_currentData.OperationID) == false && _currentData.OperationID != bookingID)
            {
                ///	否则提示：“是否重新导入发货人、收货人、通知人、地点、货物信息？”，如果选择是，则继续执行下一步，否则退出。
                if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "是否重新导入发货人、收货人、通知人、地点、货物信息?"
                                 , LocalData.IsEnglish ? "Tip" : "提示"
                                 , MessageBoxButtons.YesNo
                                 , MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            #endregion

            #region 填充
            ICP.FCM.OceanImport.ServiceInterface.OceanBusinessInfo bookingInfo = oiService.GetBusinessInfo(bookingID);

            stxtOperationNo.Text = _currentData.OperationNo = bookingInfo.No;
            stxtOperationNo.Tag = _currentData.OperationID = bookingInfo.ID;

            _currentData.CustomerName = bookingInfo.CustomerName;
            _currentData.Mblno = bookingInfo.MBLNo;
            //_currentData.Hblno = bookingInfo.SubNo;
            _currentData.CompanyID = bookingInfo.CompanyID;

            _currentData.SalesDepartmentID = bookingInfo.SalesDepartmentID;
            _currentData.SalesDepartmentName = bookingInfo.SalesDepartmentName;
            treeSelectBox1.ShowSelectedValue(_currentData.SalesDepartmentID, _currentData.SalesDepartmentName);

            //_currentData.AgentOfCarrierName = bookingInfo.AgentOfCarrierName;
            //_currentData.AgentOfCarrierID = bookingInfo.AgentOfCarrierID;
            //_currentData.FilightNoID = bookingInfo.FilightId;
            //_currentData.FilightNo = bookingInfo.FilightNo;
            //cmbFlightNo.ShowSelectedValue(_currentData.FilightNoID, _currentData.FilightNo);

            //if (bookingInfo.AirCompanyId != null)
            //{
            //    _currentData.AirCompanyID = bookingInfo.AirCompanyId.Value;
            //    _currentData.AirCompanyName = bookingInfo.AirCompanyName;
            //}

            //this.mcmbAirCompany.ShowSelectedValue(this._currentData.AirCompanyID, this._currentData.AirCompanyName);

            _currentData.SalesID = bookingInfo.SalesID;
            _currentData.SalesName = bookingInfo.SalesName;
            //treeOperation.ShowSelectedValue(_currentData.SalesID, _currentData.SalesName);

            _currentData.OverseasFilerID = bookingInfo.OverSeasFilerId;
            _currentData.OverseasFilerName = bookingInfo.OverSeasFilerName;
            mcmbOverseasFiler.ShowSelectedValue(_currentData.OverseasFilerID, _currentData.OverseasFilerName);

            #region 收发通

            #region 发货人
            _currentData.ShipperID = bookingInfo.ShipperID == null ? Guid.Empty : bookingInfo.ShipperID.Value;
            _currentData.ShipperName = bookingInfo.ShipperName;
            stxtShipper.CustomerDescription = _currentData.ShipperDescription = bookingInfo.ShipperDescription;
            //if (_currentData.ShipperDescription != null)
            //    txtShipperDescription.Text = _currentData.ShipperDescription.ToString(LocalData.IsEnglish);
            #endregion

            #region 收货人
            _currentData.ConsigneeID = bookingInfo.ConsigneeID;
            _currentData.ConsigneeName = bookingInfo.ConsigneeName;
            customerPopupContainerEdit1.CustomerDescription = _currentData.ConsigneeDescription = bookingInfo.ConsigneeDescription;
            #endregion

            #region NotifyParty = 通知人描述 = “SAME AS CONSIGNEE”

            _currentData.NotifyPartyID = Guid.Empty;
            _currentData.NotifyPartyName = string.Empty;
            lwNotifyParty.CustomerDescription = _currentData.NotifyDescription = new CustomerDescription();

            #endregion

            #endregion

            #region Port

            _currentData.PolID = bookingInfo.POLID;
            _currentData.PolName = bookingInfo.POLName;

            _currentData.PodID = bookingInfo.PODID;
            _currentData.PodName = bookingInfo.PODName;

            _currentData.FinalDestinationID = bookingInfo.FinalDestinationID == null ? Guid.Empty : bookingInfo.FinalDestinationID.Value;
            _currentData.FinalDestinationName = bookingInfo.FinalDestinationName;

            #endregion

            #region Voyage, PreVoyage, ETD, ETA

            stxtVesselVoyage.Tag = _currentData.VoyageID = bookingInfo.VesselID;
            stxtVesselVoyage.Text = _currentData.VesselVoyage = bookingInfo.VesselVoyage;


            _currentData.Etd = bookingInfo.ETD;
            _currentData.Eta = bookingInfo.ETA;

            //if (bookingInfo.MBLReleaseType != null && bookingInfo.MBLReleaseType.Value != FCMReleaseType.Unknown)
            //    _currentData.ReleaseType = bookingInfo.MBLReleaseType.Value;

            #endregion

            #region 付款方式,运输条款,数量,重量,体积

            _currentData.PaymentTypeID = bookingInfo.PaymentTermID;
            cmbPaymentTerm.Text = _currentData.PaymentTypeName = bookingInfo.PaymentTermName;
            cmbPaymentTerm.ShowSelectedValue(_currentData.PaymentTypeID, _currentData.PaymentTypeName);

            //if (string.IsNullOrEmpty(_CurrentBLInfo.PaymentTermName) == false)
            //{
            //    if (_CurrentBLInfo.PaymentTermName == "CC" || _CurrentBLInfo.PaymentTermName == "到付")
            //        txtFreightDescription.Text = _CurrentBLInfo.FreightDescription = "FREIGHT COLLECT";
            //    else
            //        txtFreightDescription.Text = _CurrentBLInfo.FreightDescription = "FREIGHT PREPAID";
            //}
            //else
            //    txtFreightDescription.Text = _CurrentBLInfo.FreightDescription = string.Empty;


            _currentData.Quantity = bookingInfo.Quantity;
            if (Utility.GuidIsNullOrEmpty(bookingInfo.QuantityUnitID) == false)
            {
                lwPackages.Text = _currentData.QuantityUnitName = bookingInfo.QuantityUnitName;
                _currentData.QuantityUnitID = bookingInfo.QuantityUnitID;

                lwPackages.ShowSelectedValue(_currentData.QuantityUnitID, _currentData.QuantityUnitName);

            }

            _currentData.Weight = bookingInfo.Weight;
            if (Utility.GuidIsNullOrEmpty(bookingInfo.WeightUnitID) == false)
            {
                cmbWeightUnit.Text = _currentData.WeightUnitName = bookingInfo.WeightUnitName;
                _currentData.WeightUnitID = bookingInfo.WeightUnitID;

                cmbWeightUnit.ShowSelectedValue(_currentData.WeightUnitID, _currentData.WeightUnitName);
            }
            _currentData.Measurement = bookingInfo.Measurement;
            if (Utility.GuidIsNullOrEmpty(bookingInfo.MeasurementUnitID) == false)
            {
                cmbMeasurementUnit.Text = _currentData.MeasurementUnitName = bookingInfo.MeasurementUnitName;
                _currentData.MeasurementUnitID = bookingInfo.MeasurementUnitID;
                cmbMeasurementUnit.ShowSelectedValue(_currentData.MeasurementUnitID, _currentData.MeasurementUnitName);
            }


            #endregion

            bsOtherInfo.EndEdit();
            #endregion
        }

        #endregion

        private OtherBusinessSaveRequest SaveOtherBusiness(OtherBusinessInfo currentData)
        {
            this.EndEdit();
            if (currentData.IsDirty == true || currentData.IsNew)
            {
                OtherBusinessSaveRequest saveRequest = new OtherBusinessSaveRequest();

                saveRequest.id = currentData.ID;
                saveRequest.AgentOfCarrierID = currentData.AgentofCarrierID;
                saveRequest.CarrierID = currentData.CarrierID;
                saveRequest.Commodity = currentData.Commodity;
                saveRequest.CompanyID = currentData.CompanyID;
                saveRequest.ConsigneeID = currentData.ConsigneeID;
                saveRequest.CustomerID = currentData.CustomerID;
                saveRequest.CustomsBrokerID = currentData.CustomsBrokerID;
                saveRequest.ETA = currentData.Eta;
                saveRequest.ETD = currentData.Etd;
                saveRequest.FETA = currentData.Feta;

                saveRequest.FinalDestinationID = currentData.FinalDestinationID;


                saveRequest.FinalDestinationName = currentData.FinalDestinationName;
                saveRequest.HBLNO = currentData.Hblno;
                saveRequest.IsCommodityInspection = currentData.IsCommodityInspection;
                saveRequest.IsCustoms = currentData.IsCustoms;
                saveRequest.IsEnglish = LocalData.IsEnglish;
                saveRequest.IsQuarantineInspection = currentData.IsQuarantineInspection;
                saveRequest.IsTruck = currentData.IsTruck;
                saveRequest.IsWarehouse = currentData.IsWareHouse;
                saveRequest.MBLNO = currentData.Mblno;
                saveRequest.Measurement = currentData.Measurement;
                saveRequest.MeasurementUnitID = currentData.MeasurementUnitID;
                saveRequest.NotifyPartyID = currentData.NotifyPartyID;
                saveRequest.OperationDate = currentData.OperationDate;
                saveRequest.OperationID = currentData.OperationID;
                saveRequest.OperationNo = currentData.OperationNo;
                saveRequest.OperatorID = currentData.OperatorID;
                saveRequest.OTOperationType = currentData.OtOperationType;
                saveRequest.PaymentTypeID = currentData.PaymentTypeID;
                saveRequest.PODID = currentData.PodID;
                saveRequest.PODName = currentData.PodName;
                saveRequest.POLID = currentData.PolID;
                saveRequest.POLName = currentData.PolName;
                saveRequest.Quantity = currentData.Quantity;
                saveRequest.QuantityUnitID = currentData.QuantityUnitID;
                saveRequest.Remark = currentData.Remark;
                saveRequest.SalesDepartmentID = currentData.SalesDepartmentID;
                saveRequest.SalesID = currentData.SalesID;
                saveRequest.OverseasFilerID = currentData.OverseasFilerID;
                saveRequest.ShipperID = currentData.ShipperID;
                saveRequest.SONO = currentData.SoNo;
                saveRequest.UpdateDate = currentData.UpdateDate;
                saveRequest.VoyageID = currentData.VoyageID;
                saveRequest.WarehouseID = currentData.WarehouseID;
                saveRequest.Weight = currentData.Weight;
                saveRequest.WeightUnitID = currentData.WeightUnitID;
                saveRequest.SaveByID = LocalData.UserInfo.LoginID;
                saveRequest.AddInvolvedObject(currentData);


                return saveRequest;
            }
            else
            {
                return null;
            }
        }

        #region 箱信息
        public event EventHandler DataChanged;

        bool _isChanged = false;
        public bool IsChanged
        {
            get
            {
                if (_isChanged == false)
                {
                    List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList> source = this.bsContainer.DataSource as List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList>;
                    if (source != null)
                    {
                        foreach (var item in source)
                        {
                            if (item.IsDirty)
                            {
                                return true;
                            }
                        }
                    }
                }

                return _isChanged;
            }
            set
            {
                _isChanged = value;
            }
        }

        public bool ConValidateData()
        {
            //this.Validate();
            //this.bsContainer.EndEdit();

            //foreach (var item in bsContainer.DataSource as List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList>)
            //{
            //    if (item.Validate
            //        (
            //        ) == false) return false;
            //}

            //return true;


            bsContainer.EndEdit();
            gridView1.CloseEditor();

            List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList> list = Details;

            List<string> noList = new List<string>();

            foreach (ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList box in list)
            {
                if (!box.Validate())
                {
                    return false;
                }

                if (!noList.Contains(box.No))
                {
                    noList.Add(box.No);
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                   , (LocalData.IsEnglish ? "Box No. Not Repeat" : "箱号不能重复"));

                    return false;
                }
            }

            return true;
        }
        public void AfterSaved()
        {
            _isChanged = false;
        }
        #endregion
        #region 保存箱信息
        public List<ContainerSaveRequest> SaveContainer(Guid OrderID)
        {
            this.gridView1.CloseEditor();
            if (!this.ConValidateData()) { return null; }
            List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList> List = this.bsContainer.DataSource as List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList>;
            if (List.Count != 0)
            {
                List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList> changedList = List.FindAll(o => o.IsDirty);//明细无更改则不执行保存明细之过程
                if (OrderID == Guid.Empty)
                {
                    changedList = List;
                }

                if (changedList.Count > 0)
                {
                    List<ContainerSaveRequest> commands = new List<ContainerSaveRequest>();

                    List<Guid> IDs = new List<Guid>(); List<string> SONOS = new List<string>(); List<Guid?> TypeIDs = new List<Guid?>();
                    List<string> Nos = new List<string>(); List<Guid?> QuantityUnitIDs = new List<Guid?>(); List<Guid?> WeightUnitIDs = new List<Guid?>();
                    List<Guid?> MeasurementUnitIDs = new List<Guid?>(); List<DateTime?> UpdateDates = new List<DateTime?>(); List<decimal?> Measurements = new List<decimal?>();
                    List<decimal?> Weights = new List<decimal?>(); List<decimal?> Quantitys = new List<decimal?>(); List<string> Commoditys = new List<string>();
                    List<string> SealNos = new List<string>();
                    foreach (var item in changedList)
                    {
                        IDs.Add(item.ID);
                        SONOS.Add(item.SoNo);
                        TypeIDs.Add(item.TypeID);
                        Nos.Add(item.No);
                        QuantityUnitIDs.Add(item.QuantityUnitID);
                        WeightUnitIDs.Add(item.WeightUnitID);
                        MeasurementUnitIDs.Add(item.MeasurementUnitID);
                        UpdateDates.Add(item.UpdateDate);
                        Measurements.Add(item.Measurement);
                        Weights.Add(item.Weight);
                        Quantitys.Add(item.Quantity);
                        Commoditys.Add(item.Commodity);
                        SealNos.Add(item.SealNo);
                    }

                    ContainerSaveRequest feeInfo = new ContainerSaveRequest();
                    feeInfo.OtherBookingID = OrderID;
                    feeInfo.IDs = IDs.ToArray();
                    feeInfo.Commoditys = Commoditys.ToArray();
                    feeInfo.Measurements = Measurements.ToArray();
                    feeInfo.MeasurementUnitIDs = MeasurementUnitIDs.ToArray();
                    feeInfo.Nos = Nos.ToArray();
                    feeInfo.Quantitys = Quantitys.ToArray();
                    feeInfo.QuantityUnitIDs = QuantityUnitIDs.ToArray();
                    feeInfo.SealNos = SealNos.ToArray();
                    feeInfo.SONOS = SONOS.ToArray();
                    feeInfo.TypeIDs = TypeIDs.ToArray();
                    feeInfo.UpdateDates = UpdateDates.ToArray();
                    feeInfo.Weights = Weights.ToArray();
                    feeInfo.WeightUnitIDs = WeightUnitIDs.ToArray();
                    feeInfo.UpdateDates = UpdateDates.ToArray();
                    feeInfo.SaveByID = LocalData.UserInfo.LoginID;
                    feeInfo.isEnglish = LocalData.IsEnglish;
                    changedList.ForEach(o => feeInfo.AddInvolvedObject(o));

                    commands.Add(feeInfo);

                    return commands;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public void RefreshContainerUI(List<ContainerSaveRequest> list)
        {
            foreach (ContainerSaveRequest ContainerInfo in list)
            {
                List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList> changedContainer = ContainerInfo.UnBoxInvolvedObject<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList>();
                ManyResult result = ContainerInfo.ManyResult;
                for (int i = 0; i < changedContainer.Count; i++)
                {
                    changedContainer[i].ID = result.Items[i].GetValue<Guid>("ID");
                    changedContainer[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UPDATEDATE");
                    changedContainer[i].No = result.Items[i].GetValue<string>("NO");
                    changedContainer[i].IsDirty = false;
                }
            }
            this.AfterSaved();
        }

        #endregion
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                _currentData = bsOtherInfo.DataSource as OtherBusinessInfo;
                Save(this._currentData, false);
            }
        }

        int count = 0;
        #region 新增箱信息
        private void btnAdd_Click(object sender, EventArgs e)
        {

            ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList preRow = null;
            if (this.gridView1.RowCount > 0)
            {
                preRow = gridView1.GetRow(gridView1.RowCount - 1) as ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList;
            }
            ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList newBoxRow;

            if (preRow != null)
                newBoxRow = Utility.Clone<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList>(preRow);
            else
                newBoxRow = new ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList();

            newBoxRow.ID = Guid.Empty;
            newBoxRow.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newBoxRow.CreateByID = LocalData.UserInfo.LoginID;
            newBoxRow.CreateByName = LocalData.UserInfo.LoginName;
            newBoxRow.Measurement = 0;
            newBoxRow.Quantity = 0;
            newBoxRow.Weight = 0;
            gridView1.ClearSorting();
            (this.bsContainer.List as List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList>).Add(newBoxRow);
            bsContainer.ResetBindings(false);

            this.gridView1.MoveLast();
            _isChanged = true;
            count = gridView1.RowCount;
            if (count > 0)
            {
                txtCount.Text = count.ToString();
            }
        }
        #endregion

        #region 删除箱信息
        /// <summary>
        /// 明细当前行
        /// </summary>
        private ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList CurrentRow
        {
            get
            {
                return this.bsContainer.Current as ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList;
            }
        }
        /// <summary>
        /// 所有选择的行（箱信息）
        /// </summary>
        List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList> SelectRows
        {
            get
            {
                int[] indexs = this.gridView1.GetSelectedRows();
                if (indexs == null || indexs.Length == 0) return null;

                List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList> list = new List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList>();
                foreach (var item in indexs)
                {
                    ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList tager = gridView1.GetRow(item) as ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList;
                    if (tager != null) list.Add(tager);
                }
                return list;
            }
        }
        /// <summary>
        /// 删除箱信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            RemoveFee();
        }
        private void RemoveFee()
        {
            List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList> list = SelectRows;
            if (list == null || list.Count == 0) return;

            if (!Utility.EnquireIsDeleteCurrentData())
            {
                return;
            }

            List<Guid?> IDList = new List<Guid?>();
            List<DateTime?> DateList = new List<DateTime?>();

            foreach (ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList hbl in list)
            {
                if (!Utility.GuidIsNullOrEmpty(hbl.ID))
                {
                    IDList.Add(new Guid(hbl.ID.ToString()));
                    DateList.Add(hbl.UpdateDate);
                }
            }
            try
            {
                if (IDList.Count != 0)
                {
                    OBService.RemoveOtherContainerList(IDList.ToArray(), LocalData.UserInfo.LoginID, LocalData.IsEnglish, DateList.ToArray());
                    _isChanged = true;
                }
                this.gridView1.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                    , (LocalData.IsEnglish ? "Delete Faily" : "删除失败.") + ex.Message);
            }
            count = gridView1.RowCount;
            if (count > 0)
            {
                txtCount.Text = count.ToString();
            }
            else { txtCount.Text = "0"; }
        }
        #endregion

        #region refresh
        private void barRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                RefreshData(this._currentData.ID);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Refersh successfully." : "刷新成功.");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Refersh failed." + ex.Message : "刷新失败." + ex.Message);
            }
        }
        void RefreshData(Guid orderId)
        {
            this.GetData(this._currentData.ID);
            this.ShowOrder();
            this.RunAtOnce();
            this.SetTitle();
        }
        #endregion

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }
        #region 另存为(此处细节逻辑点待进一步确认）
        private void barSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (SaveAs())
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save as a new Business successfully. Ref. NO. is " + this._currentData.NO + "." : "已成功另存为一票新的业务，业务号为" + _currentData.NO + "。");
                    if (Saved != null)
                    {
                        Saved(new object[] { this._currentData });
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

            if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "是否另存为一票新的业务?",
                            LocalData.IsEnglish ? "Tip" : "提示",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }

            OtherBusinessInfo orderInfo = Utility.Clone<OtherBusinessInfo>(this._currentData);
            orderInfo.ID = Guid.Empty;
            orderInfo.NO = string.Empty;

            //if (string.IsNullOrEmpty(orderInfo.))
            //{
            //    orderInfo.State = OrderState.NewOrder;
            //}
            //else
            //{
            orderInfo.State = OBOrderState.NewOrder;
            //}

            orderInfo.CreateByID = LocalData.UserInfo.LoginID;
            orderInfo.CreateByName = LocalData.UserInfo.LoginName;
            orderInfo.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            orderInfo.OperationDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            orderInfo.UpdateDate = null;

            if (orderInfo.OtOperationType == OtOperationType.CustomsDeclaration)
            {
            }
            //else
            //{
            //    orderInfo.OceanShippingOrderID = Guid.Empty;
            //   orderInfo.OceanShippingOrderNo = string.Empty;
            //}

            orderInfo.IsDirty = true;
            //this.bsContainer.InitData(this._currentData.ID);

            this._currentData = orderInfo;

            if (this.Save(orderInfo, true))
            {
                this.RefreshData(orderInfo.ID);

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
        #region 派车
        private void barTruck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                _currentData = bsOtherInfo.DataSource as OtherBusinessInfo;
                this.ExportUIHelper.ShowTruckEdit(this._currentData.ID, this._currentData,
                    this.Workitem, this.OBService,
                   Business.OBListPart.GetLineNo(this._currentData));
            }
        }
        #endregion

        #region  账单
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_currentData == null)
                {
                    return;
                }

                OperationCommonInfo operationCommonInfo = fcmCommonService.GetOperationCommonInfo(_currentData.ID, OperationType.Other);
                if (operationCommonInfo != null)
                {
                    finClientService.ShowBillList(operationCommonInfo, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
                }
                else
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// 虚拟页面(其他业务--订单编辑）
    /// </summary>
    [ToolboxItem(false)]
    public partial class OBOrderBaseEditPart : OBBaseEditPart
    {
        protected override bool IsOrderEditPart
        {
            get
            {
                return true;
            }
        }
    }
}