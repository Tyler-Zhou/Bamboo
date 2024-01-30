﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;
using ICP.Common.UI;
using ICP.Framework.ClientComponents.Service;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.UI.Common;
using System.Threading;
using DevExpress.XtraBars;
using ICP.FCM.Common.ServiceInterface.Common;
using ICP.Message.ServiceInterface;

namespace ICP.FCM.OceanImport.UI
{
    /// <summary>
    /// 海运进口订单编辑界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class OIOrderBaseEdit : BaseEditPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IDataFindClientService dfService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }


        [ServiceDependency]
        public ICP.Common.ServiceInterface.ICustomerService customerService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IGeographyService geographyService { get; set; }

        //[Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        //public ICPCommUIHelperService ICPCommUIHelperService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public OceanImportPrintHelper OceanImportPrintHelper { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelperService { get; set; }

        [ServiceDependency]
        public IOceanImportService oiService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IConfigureService configureService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IPermissionService permissionService { get; set; }


        [ServiceDependency]
        public ICP.Message.ServiceInterface.IClientMessageService eMailClientService { get; set; }


        [ServiceDependency]
        public ICP.Common.ServiceInterface.Client.IReportViewService reportService { get; set; }

        [ServiceDependency]
        public IOIReportDataService OIReportSrvice { get; set; }

        #endregion

        #region 本地变量

        CustomerType customerType = CustomerType.Unknown;

        List<CountryList> _countryList = null;

        OceanOrderList listInfo = null;

        /// <summary>
        /// 是否是远东区解决方案(根据当前登录人的默认公司所属解决方案)
        /// </summary>
        bool _isFarEastSolution = false;

        OceanOrderInfo _orderInfo = null;

        OceanOrderList CurrentOrderList
        {
            get
            {
                if (bsOrders.List == null || bsOrders.Current == null) return null;
                return bsOrders.Current as OceanOrderList;
            }
        }

        /// <summary>
        /// 是否有数据发生改变
        /// </summary>
        public bool IsDirty
        {
            get
            {
                if (_orderInfo.IsDirty)
                {
                    return true;
                }
                if (this.orderFeeEditPart1.IsChanged)
                {
                    return true;
                }
                if (this.orderPOEditPart1.IsChanged)
                {
                    return true;
                }

                return false;
            }
        }


        List<DataDictionaryList> _weightUnits;

        #endregion

        #region 构造函数

        public OIOrderBaseEdit()
        {
            InitializeComponent();
            if (DesignMode)
            {
                return;
            }

            InitMessage();
            barPrint.ItemClick += delegate { Workitem.Commands[OIOrderCommandConstants.Command_Print].Execute(); };
            this.Disposed += new EventHandler(OrderBaseEditPart_Disposed);
            this.Load += new EventHandler(OrderBaseEditPart_Load);
        }


        #endregion

        #region 注册发送消息
        /// <summary>
        /// 注册消息
        /// </summary>
        private void InitMessage()
        {
            this.RegisterMessage("OrderSendEMailStyle", "我在 {0} 新增了一个业务号为 {1} 的订单,请留意.");
        }

        #endregion

        #region 新订单的逻辑

        void ReadyForNew()
        {
            if (this._orderInfo.ID == Guid.Empty)
            {
                OceanOrderInfo newData = new OceanOrderInfo();
                // newData.ContactName = Workitem.State["ContactName"] == null ? "" : Workitem.State["ContactName"].ToString();
                //newData.ContactID = Guid.NewGuid();

                newData.State = OIOrderState.NewOrder;
                newData.CreateByID = LocalData.UserInfo.LoginID;
                newData.CreateByName = LocalData.UserInfo.LoginName;
                newData.CreateDate = DateTime.Now;
                newData.BookingDate = DateTime.Now;
                newData.SalesID = LocalData.UserInfo.LoginID;
                newData.SalesName = LocalData.UserInfo.LoginName;
                //newData.CargoDescription = new ICP.FCM.Common.ServiceInterface.DataObjects.CargoDescription();
                newData.BookingMode = FCMBookingMode.Fax;
                newData.IsValid = true;

                #region 设置默认值
                DataDictionaryList normalDictionary = null;
                normalDictionary = ICPCommUIHelperService.GetNormalDictionary(DataDictionaryType.PaymentTerm);
                newData.PaymentTermID = normalDictionary.ID;
                newData.PaymentTermName = normalDictionary.EName;

                normalDictionary = ICPCommUIHelperService.GetNormalDictionary(DataDictionaryType.QuantityUnit);
                newData.QuantityUnitID = normalDictionary.ID;
                newData.QuantityUnitName = normalDictionary.EName;

                normalDictionary = ICPCommUIHelperService.GetNormalDictionary(DataDictionaryType.WeightUnit);
                newData.WeightUnitID = normalDictionary.ID;
                newData.WeightUnitName = normalDictionary.EName;

                normalDictionary = ICPCommUIHelperService.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
                newData.MeasurementUnitID = normalDictionary.ID;
                newData.MeasurementUnitName = normalDictionary.EName;
                #endregion

                this._orderInfo = newData;

                this._orderInfo.HBLReleaseType = FCMReleaseType.Unknown;
                this._orderInfo.OIOperationType = FCMOperationType.FCL;

                barSaveAs.Enabled = false;
                barConfirmBooking.Enabled = false;
                barConfirmBookingShip.Enabled = false;
                this.gvOrders.DoubleClick += new System.EventHandler(this.gvOrders_DoubleClick);
                this.stxtCustomer.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.stxtCustomer_ButtonClick);

                Utility.EnsureDefaultCompanyExists(this.userService);
                Utility.EnsureDefaultDepartmentExists(this.userService);


                this._orderInfo.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                this._orderInfo.CompanyName = LocalData.UserInfo.DefaultCompanyName;

                this._orderInfo.SalesDepartmentID = LocalData.UserInfo.DefaultDepartmentID;
                this._orderInfo.SalesDepartmentName = LocalData.UserInfo.DefaultDepartmentName;


                this._orderInfo.IsDirty = false;
            }
            else
            {
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
        private void InitData(OceanOrderInfo info)
        {
            this.txtState.Text = EnumHelper.GetDescription<OIOrderState>(this._orderInfo.State, LocalData.IsEnglish);

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

            if (this._orderInfo.ConsigneeDescription == null)
                this._orderInfo.ConsigneeDescription = new CustomerDescription();
            if (this._orderInfo.ShipperDescription == null)
                this._orderInfo.ShipperDescription = new CustomerDescription();
            if (this._orderInfo.AgentDescription == null)
            {
                this._orderInfo.AgentDescription = new CustomerDescription();
            }

            //if (this._orderInfo.CargoDescription == null)
            //    this._orderInfo.CargoDescription = new CargoDescription();            
        }

        #endregion

        #region 显示订单信息

        void SetState()
        {
            this.txtState.Text = EnumHelper.GetDescription<OIOrderState>(this._orderInfo.State, LocalData.IsEnglish);
        }

        /// <summary>
        /// 显示订单信息
        /// </summary>
        private void InitControls()
        {
            this.SetState();

            DevHelper.FormatSpinEditForInteger(this.numQuantity);
            DevHelper.FormatSpinEdit(this.numWeight, 3);
            DevHelper.FormatSpinEdit(this.numMeasurement, 3);



            //口岸公司
            this.cmbCompany.ShowSelectedValue(this._orderInfo.CompanyID, this._orderInfo.CompanyName);
            this.treeBoxSalesDep.ShowSelectedValue(this._orderInfo.SalesDepartmentID.ToGuid(), this._orderInfo.SalesDepartmentName);

            orderFeeEditPart1.SetCompanyID(this._orderInfo.CompanyID);

            //运输条款
            this.cmbTransportClause.ShowSelectedValue(this._orderInfo.TransportClauseID, this._orderInfo.TransportClauseName);

            this.cmbTradeTerm.ShowSelectedValue(this._orderInfo.TradeTermID, this._orderInfo.TradeTermName);
            this.cmbQuantityUnit.ShowSelectedValue(this._orderInfo.QuantityUnitID, this._orderInfo.QuantityUnitName);
            this.cmbMeasurementUnit.ShowSelectedValue(this._orderInfo.MeasurementUnitID, this._orderInfo.MeasurementUnitName);
            this.cmbWeightUnit.ShowSelectedValue(this._orderInfo.WeightUnitID, this._orderInfo.WeightUnitName);

            //付款方式
            this.cmbPaymentTerm.ShowSelectedValue(this._orderInfo.PaymentTermID, this._orderInfo.PaymentTermName);

            //货物类型
            if (this._orderInfo.CargoType.HasValue)
            {
                this.cmbCargoType.ShowSelectedValue(this._orderInfo.CargoType,
                    EnumHelper.GetDescription<CargoType>(this._orderInfo.CargoType.Value, LocalData.IsEnglish));
            }

            //类型
            this.cmbHBLReleaseType.ShowSelectedValue(this._orderInfo.HBLReleaseType,
                EnumHelper.GetDescription<FCMReleaseType>(this._orderInfo.HBLReleaseType.Value, LocalData.IsEnglish));

            //揽货类型
            this.cmbSalesType.ShowSelectedValue(this._orderInfo.SalesTypeID, this._orderInfo.SalesTypeName);

            //揽货人
            this.mcmbSales.ShowSelectedValue(this._orderInfo.SalesID, this._orderInfo.SalesName);

            //客服
            this.mcmbCustomerContact.ShowSelectedValue(this._orderInfo.CustomerContactID, this._orderInfo.CustomerContactName);

            //船公司
            this.mcmbCarrier.ShowSelectedValue(this._orderInfo.CarrierID, this._orderInfo.CarrierName);

            //业务类型
            this.cmbType.ShowSelectedValue(this._orderInfo.OIOperationType,
                EnumHelper.GetDescription<FCMOperationType>(this._orderInfo.OIOperationType, LocalData.IsEnglish));

            //委托方式
            this.cmbBookingMode.ShowSelectedValue(this._orderInfo.BookingMode,
                EnumHelper.GetDescription<FCMBookingMode>(this._orderInfo.BookingMode, LocalData.IsEnglish));

            #region CustomerDescription/CargoDescription/ContainerDescription

            if (this._orderInfo.CargoDescription != null && this._orderInfo.CargoDescription.Cargo != null)
            {
                txtCargoDescription.Text = this._orderInfo.CargoDescription.Cargo.ToString(LocalData.IsEnglish);
            }

            if (this._orderInfo.ContainerDescription != null)
            {
                this.containerDemandControl1.Text = this._orderInfo.ContainerDescription.ToString();
            }

            this.dxErrorProvider1.SetIconAlignment(containerDemandControl1.ErrorHost, ErrorIconAlignment.MiddleRight);

            #endregion

            this.mcmOverseasFiler.ShowSelectedValue(this._orderInfo.OverSeasFilerId, this._orderInfo.OverSeasFilerName);
        }

        #endregion

        #region 注册搜索器

        CustomerFinderBridge shipperBridge;
        CustomerFinderBridge consigneeBridge;

        /// <summary>
        /// 注册搜索器
        /// </summary>
        void SearchRegister()
        {
            OceanOrderInfo currentData = bsOrderInfo.DataSource as OceanOrderInfo;

            #region Customer

            //Customer
            dfService.Register(stxtCustomer, CommonFinderConstants.CustoemrFinder, ICP.Common.UI.SearchFieldConstants.CodeName,
                ICP.FCM.OceanImport.UI.Common.SearchConstants.CustomerResultValue,
                GetConditionsForCustomer,
                      delegate(object inputSource, object[] resultData)
                      {
                          Guid oldCustomerId = currentData.CustomerID;
                          stxtCustomer.ClosePopup();

                          CustomerStateType state = (CustomerStateType)resultData[7];
                          if (state == CustomerStateType.Invalid)
                          {
                              if (!Utility.PopCustomerIsInvalid())
                              {
                                  return;
                              }
                          }

                          CustomerCodeApplyState? approved = (CustomerCodeApplyState?)resultData[8];
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
                                                                            (DateTime?)resultData[9]);
                                      }
                                  }
                              }
                          }

                          if (resultData[4] != null)
                          {
                              this.customerType = (CustomerType)resultData[4];
                          }

                          stxtCustomer.Tag = currentData.CustomerID = new Guid(resultData[0].ToString());
                          stxtCustomer.EditValue = currentData.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();

                          if (resultData[5] != null
                              && (Guid)resultData[5] != Guid.Empty
                              && resultData[6] != null)
                          {
                              this.cmbTradeTerm.ShowSelectedValue(resultData[5], resultData[6].ToString());
                          }

                          if (oldCustomerId != Guid.Empty && currentData.CustomerID == oldCustomerId)
                          {
                              return;
                          }

                          SetRecentlyOrderList();
                          SetSalesTypeByCustomerAndCompany();
                          this.SetConsigneeByCustomer();
                      }, delegate
                      {
                          stxtCustomer.ClosePopup();
                          stxtCustomer.Text = currentData.CustomerName = string.Empty;
                          stxtCustomer.Tag = currentData.CustomerID = Guid.Empty;
                          SetSalesTypeByCustomerAndCompany();
                          SetRecentlyOrderList();
                          this.SetConsigneeByCustomer();
                      },
                      ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

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
               this._orderInfo.ShipperDescription,
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
                this._orderInfo.ConsigneeDescription,
                ICPCommUIHelperService,
                LocalData.IsEnglish);
                consigneeBridge.Init();
            });
            stxtConsignee.OnOk += new EventHandler(stxtConsignee_OnOk);

            #endregion

            #endregion

            #region Port

            //不论中英文环境，名称一律用英文名称
            LocationFinderBridge pfbPlaceOfReceipt = new LocationFinderBridge(this.stxtPlaceOfReceipt, this.dfService, true);

            PortFinderBridge pfbPOL = new PortFinderBridge(this.stxtPOL, this.dfService, true);

            PortFinderBridge pfbPOD = new PortFinderBridge(this.stxtPOD, this.dfService, true);

            LocationFinderBridge pfbPlaceOfDelivery = new LocationFinderBridge(this.stxtPlaceOfDelivery, this.dfService, true);

            LocationFinderBridge pfbFinalDestination = new LocationFinderBridge(this.stxtFinalDestination, this.dfService, true);

            #endregion
        }

        /// <summary>
        /// bug2972: 业务写订单时，选择客户，只能选择自己有权限的CRM关联的公共客户。
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForCustomer()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("IsFromOrder", true, false);
            conditions.AddWithValue("CurruntUserID", LocalData.UserInfo.LoginID, false);
            return conditions;
        }

        void stxtConsignee_OnOk(object sender, EventArgs e)
        {
            if (stxtConsignee.CustomerDescription != null)
            {
                _orderInfo.ConsigneeDescription = stxtConsignee.CustomerDescription;
            }
        }

        void stxtShipper_OnOk(object sender, EventArgs e)
        {
            if (stxtShipper.CustomerDescription != null)
            {
                _orderInfo.ShipperDescription = stxtShipper.CustomerDescription;
            }
        }

        /// <summary>
        /// 当前客户最近业务所对应的海外部客服or 当前客户为新客户and当前揽货人最近业务所对应的海外部客服
        /// </summary>
        void SetDefaultOverseasFiler()
        {
            List<UserInfo> users = this.oiService.GetOIOverseasFilersList(this._orderInfo.CustomerID, this._orderInfo.SalesID,
                DateTime.Now.AddDays(-30), DateTime.Now, 1);

            if (users.Count > 0)
            {
                this.mcmOverseasFiler.ShowSelectedValue(users[0].ID, LocalData.IsEnglish ? users[0].EName : users[0].CName);
            }
        }

        void ResetDescription()
        {
            if (this.shipperBridge != null)
            {
                this.shipperBridge.SetCustomerDescription(this._orderInfo.ShipperDescription);
            }

            if (this.consigneeBridge != null)
            {
                this.consigneeBridge.SetCustomerDescription(this._orderInfo.ConsigneeDescription);
            }
        }

        #endregion

        #region 注册延迟加载的数据源

        /// <summary>
        /// 注册延迟加载的数据源
        /// </summary>
        void SetLazyLoaders()
        {
            _weightUnits = ICPCommUIHelperService.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit, DataBindType.EName);

            //操作口岸列表   
            Utility.SetEnterToExecuteOnec(cmbCompany, delegate
            {
                ICPCommUIHelperService.BindCompanyByAll(this.cmbCompany, true);
            });
            //运输条款
            Utility.SetEnterToExecuteOnec(cmbTransportClause, delegate
            {
                ICPCommUIHelperService.SetCmbTransportClause(cmbTransportClause);
            });

            //贸易条款
            Utility.SetEnterToExecuteOnec(cmbTradeTerm, delegate
            {
                ICPCommUIHelperService.SetCmbDataDictionary(cmbTradeTerm, DataDictionaryType.TradeTerm, DataBindType.EName);
            });

            //包装
            Utility.SetEnterToExecuteOnec(cmbQuantityUnit, delegate
            {
                ICPCommUIHelperService.SetCmbDataDictionary(cmbQuantityUnit, DataDictionaryType.QuantityUnit, DataBindType.EName);
            });

            //重量
            Utility.SetEnterToExecuteOnec(cmbWeightUnit, delegate
            {
                ICPCommUIHelperService.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit, DataBindType.EName);
            });

            //体积
            Utility.SetEnterToExecuteOnec(cmbMeasurementUnit, delegate
            {
                ICPCommUIHelperService.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit, DataBindType.EName);
            });

            //揽货方式
            Utility.SetEnterToExecuteOnec(cmbSalesType, delegate
            {
                ICPCommUIHelperService.SetCmbDataDictionary(cmbSalesType, DataDictionaryType.SalesType);
            });
            //3个付款方式的下拉列表
            Utility.SetEnterToExecuteOnec(cmbPaymentTerm, delegate
            {
                ICPCommUIHelperService.SetCmbDataDictionary(
                                                    cmbPaymentTerm,
                                                    DataDictionaryType.PaymentTerm, DataBindType.EName, true);
            });

            #region 代理
            if (Utility.GuidIsNullOrEmpty(_orderInfo.AgentID) == false)
            {
                List<CustomerList> agentCustomers = new List<CustomerList>();
                CustomerList agentCustomer = new CustomerList();
                agentCustomer.CName = agentCustomer.EName = _orderInfo.AgentName;
                agentCustomer.ID = _orderInfo.AgentID.Value;
                agentCustomers.Insert(0, agentCustomer);
                SetAgentSource(agentCustomers);
            }
            Utility.SetEnterToExecuteOnec(txtAgent, delegate
            {
                SetAgentSourceByCompanyID();
            });
            #endregion



            Utility.SetEnterToExecuteOnec(cmbHBLReleaseType, delegate
            {
                ICPCommUIHelperService.SetComboxByEnum<FCMReleaseType>(this.cmbHBLReleaseType, true);
            });

            //业务类型
            Utility.SetEnterToExecuteOnec(this.cmbType, delegate
            {
                ICPCommUIHelperService.SetComboxByEnum<FCMOperationType>(this.cmbType, false);
            });
            //委托方式
            Utility.SetEnterToExecuteOnec(this.cmbBookingMode, delegate
            {
                ICPCommUIHelperService.SetComboxByEnum<FCMBookingMode>(this.cmbBookingMode, false);
            });

            //货物描述
            Utility.SetEnterToExecuteOnec(this.cmbCargoType, delegate
            {
                ICPCommUIHelperService.SetComboxByEnum<CargoType>(this.cmbCargoType, true, true);
            });

            //船公司
            Utility.SetEnterToExecuteOnec(mcmbCarrier, delegate
            {
                ICPCommUIHelperService.BindCustomerList(mcmbCarrier, CustomerType.Carrier);
            });
        }

        /// <summary>
        /// 设置Agent数据源
        /// </summary>
        private void SetAgentSourceByCompanyID()
        {
            Guid companyID = _orderInfo.CompanyID;
            txtAgent.DataSource = null;
            if (Utility.GuidIsNullOrEmpty(companyID))
            {
                txtAgent.Enabled = false;
                return;
            }

            List<CustomerList> agentCustomers = configureService.GetCompanyAgentList(_orderInfo.CompanyID, true);
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
            if (Utility.GuidIsNullOrEmpty(_orderInfo.AgentID))
            {
                txtAgent.EditValue = _orderInfo.AgentID = agentCustomers[0].ID;
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
                txtAgent.CustomerDescription = _orderInfo.AgentDescription = new CustomerDescription();
            }
            else
            {
                ICPCommUIHelperService.SetCustomerDesByID(id, _orderInfo.AgentDescription);
                txtAgent.CustomerDescription = _orderInfo.AgentDescription;
            }
        }

        /// <summary>
        /// 延迟加载，而且条件是动态的
        /// </summary>
        void SetLazyDataLodersWithDynamicCondition()
        {
            mcmOverseasFiler.Enter += new EventHandler(mcmOverseasFiler_Click);
            this.treeBoxSalesDep.Enter += new EventHandler(treeBoxSalesDep_Enter);
            this.mcmbCustomerContact.Enter += new EventHandler(mcmbCustomerContact_Enter);
        }
        //绑定客服
        void mcmbCustomerContact_Enter(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = (Guid)cmbCompany.EditValue;
            }

            ICPCommUIHelperService.SetComboxUsersByRole(mcmbCustomerContact, depID, "文件", true);
        }

        void treeBoxSalesDep_Enter(object sender, EventArgs e)
        {
            if (!Utility.GuidIsNullOrEmpty(this._orderInfo.SalesID))
            {
                SetSalesDepartment(this._orderInfo.SalesID.Value);
            }
        }

        #endregion

        #region 注册界面控件之间联动的事件

        /// <summary>
        /// 注册界面控件之间联动的事件
        /// 一般一个控件的值改变，要影响别的控件的值，就注册到这里。如果同时还要改变其它控件的颜色、可用状态之类的逻辑，要拆分开。
        /// </summary>
        void RegisterRelativeEvents()
        {
            this.panelScroll.Click += delegate { this.panelScroll.Focus(); };
            this.cmbCompany.SelectedIndexChanged += new EventHandler(cmbCompany_SelectedIndexChanged);

            this.treeBoxSalesDep.Selected += new EventHandler(treeBoxSalesDep_Selected);
            this.treeBoxSalesDep.Selected += new EventHandler(cmbSalesDepartment_SelectedIndexChanged);
            this.cmbCargoType.Enter += new System.EventHandler(this.cmbCargoType_Enter);
            this.cmbCargoType.EditValueChanged += new System.EventHandler(this.cmbCargoType_EditValueChanged);

            this.stxtCustomer.LostFocus += new EventHandler(stxtCustomer_LostFocus);

            this.cmbTradeTerm.SelectedIndexChanged += new EventHandler(cmbTradeTerm_SelectedIndexChanged);
            this.cmbTransportClause.SelectedIndexChanged += new EventHandler(cmbTransportClause_SelectedIndexChanged);
            this.stxtPOD.TextChanged += new EventHandler(stxtPOD_TextChanged);
            this.stxtPlaceOfDelivery.TextChanged += new EventHandler(stxtPlaceOfDelivery_TextChanged);
        }



        void stxtPlaceOfDelivery_TextChanged(object sender, EventArgs e)
        {
            _orderInfo.PlaceOfDeliveryName = this.stxtPlaceOfDelivery.Text;
            this.SetFinalDestinationByTransportClause();
        }

        void stxtPOD_TextChanged(object sender, EventArgs e)
        {
            _orderInfo.PODName = this.stxtPOD.Text;
            this.SetPlaceOfDeliveryByTransportClause();
        }

        void cmbTransportClause_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._orderInfo.TransportClauseName = this.cmbTransportClause.Text;
            SetPlaceOfDeliveryByTransportClause();
            SetFinalDestinationByTransportClause();
        }

        /// <summary>
        /// 交货地 如果目的港运输条款<>Door，那么就为卸货港
        /// </summary>
        private void SetPlaceOfDeliveryByTransportClause()
        {
            if (!Utility.GuidIsNullOrEmpty(_orderInfo.PlaceOfDeliveryID)
                || Utility.GuidIsNullOrEmpty(this._orderInfo.TransportClauseID)) return;

            if (_orderInfo.TransportClauseName.Contains("-DOOR") == false)
            {
                stxtPlaceOfDelivery.Text = _orderInfo.PlaceOfDeliveryName = _orderInfo.PODName;
                stxtPlaceOfDelivery.Tag = _orderInfo.PlaceOfDeliveryID = _orderInfo.PODID;
            }
        }

        /// <summary>
        /// 最终目的地 如果目的港运输条款<>Door，那么就为卸货港
        /// </summary>
        private void SetFinalDestinationByTransportClause()
        {
            if (!Utility.GuidIsNullOrEmpty(_orderInfo.FinalDestinationId)
                || Utility.GuidIsNullOrEmpty(this._orderInfo.TransportClauseID)) return;

            if (_orderInfo.TransportClauseName.Contains("-DOOR") == false)
            {
                stxtFinalDestination.Text = _orderInfo.FinalDestinationName = _orderInfo.PlaceOfDeliveryName;
                stxtFinalDestination.Tag = _orderInfo.FinalDestinationId = _orderInfo.PlaceOfDeliveryID;
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
            this.stxtPOL.TextChanged += new EventHandler(stxtPOL_TextChanged);
            this.cmbSalesType.EditValueChanged += new EventHandler(cmbSalesType_EditValueChanged);
            this.TriggerEventsAtOnce();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbTradeTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._orderInfo.TradeTermName = this.cmbTradeTerm.Text;
        }


        /// <summary>
        /// 收货人默认为客户
        /// </summary>
        private void SetConsigneeByCustomer()
        {
            if (Utility.GuidIsNullOrEmpty(this._orderInfo.ConsigneeID) == false) return;

            this.stxtConsignee.Tag = this._orderInfo.ConsigneeID = this._orderInfo.CustomerID;
            this.stxtConsignee.Text = this._orderInfo.ConsigneeName = this._orderInfo.CustomerName;
            ICPCommUIHelperService.SetCustomerDesByID(_orderInfo.ConsigneeID, _orderInfo.ConsigneeDescription);


            this.ResetDescription();
        }

        /// <summary>
        /// 当收货地为空时，输入装货港自动赋值给收货地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void stxtPOL_TextChanged(object sender, EventArgs e)
        {
            //if (Utility.GuidIsNullOrEmpty(this._orderInfo.PlaceOfReceiptID))
            //{
            //    stxtPlaceOfReceipt.Text = this._orderInfo.PlaceOfReceiptName = this._orderInfo.POLName;
            //    stxtPlaceOfReceipt.Tag = this._orderInfo.PlaceOfReceiptID = this._orderInfo.POLID;
            //}           
            this._orderInfo.POLName = this.stxtPOL.Text;
            if (Utility.GuidIsNullOrEmpty(this._orderInfo.PlaceOfReceiptID))
            {
                stxtPlaceOfReceipt.Tag = this._orderInfo.PlaceOfReceiptID = this._orderInfo.POLID;
                stxtPlaceOfReceipt.Text = this._orderInfo.PlaceOfReceiptName = this._orderInfo.POLName;
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

        }

        void cmbSalesType_EditValueChanged(object sender, EventArgs e)
        {

            this.mcmOverseasFiler.Enabled = this.cmbSalesType.EditValue == "E34BDCAA-4253-41C0-B14B-E38111CF2FC8";

            if (!this.mcmOverseasFiler.Enabled)
            {
                this.mcmOverseasFiler.ShowSelectedValue(Guid.Empty, string.Empty);//清除现有值
                this.mcmOverseasFiler.SpecifiedBackColor = this.txtNo.BackColor;
            }
            else
            {
                this.mcmOverseasFiler.SpecifiedBackColor = System.Drawing.Color.White;
            }
        }


        #endregion

        #region 界面控件联动

        #region 业务类型和集装箱信息

        void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetContainerDemandByBookingType();
            this.chkIsTruck.Enabled = this._orderInfo.OIOperationType == FCMOperationType.FCL;

            if (!this.chkIsTruck.Enabled)
            {
                this.chkIsTruck.Checked = this._orderInfo.IsTruck = false;
            }
        }

        /// <summary>
        /// 如果业务类型不是整箱，那么箱描述就不可编辑，否则可编辑
        /// </summary>
        private void SetContainerDemandByBookingType()
        {
            if (_orderInfo.OIOperationType == FCMOperationType.FCL)
            {
                containerDemandControl1.Enabled = true;
                this.containerDemandControl1.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            }
            else
            {
                containerDemandControl1.Enabled = false;
                containerDemandControl1.Text = string.Empty;
                this.containerDemandControl1.SpecifiedBackColor = this.txtNo.BackColor;
            }
        }

        #endregion



        void mcmbOperator_Click(object sender, EventArgs e)
        {
            //ICPCommUIHelperService.SetUsersList(mcmbBookinger, true,
            //    (cmbCompany.EditValue == null || string.IsNullOrEmpty(cmbCompany.EditValue.ToString())) ? Guid.Empty : (Guid)cmbCompany.EditValue,
            //    "订舱",
            //    true);
        }

        void mcmOverseasFiler_Click(object sender, EventArgs e)
        {
            ICPCommUIHelperService.SetComboxUsersByRole(mcmOverseasFiler, (Guid)treeBoxSalesDep.EditValue, "海外拓展", true);
        }

        void treeBoxSalesDep_Selected(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.EditValue == null || cmbCompany.EditValue.ToString().Length == 0)
            {
                return;
            }

            Guid companyID = new Guid(cmbCompany.EditValue.ToString());
            orderFeeEditPart1.SetCompanyID(this._orderInfo.CompanyID);
            SetSalesTypeByCustomerAndCompany();
            SetAgentSourceByCompanyID();

            SetRecentlyOrderList();
        }

        private void stxtCustomer_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind != DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) return;

            if (bsOrders.DataSource == null || bsOrders.List == null || bsOrders.List.Count == 0)
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
            if (_orderInfo.CompanyID != Guid.Empty && _orderInfo.CustomerID != Guid.Empty)
            {
                DataDictionaryInfo salesType = oiService.GetSalesType(_orderInfo.CustomerID, _orderInfo.CompanyID);
                if (salesType != null)
                {
                    _orderInfo.SalesTypeID = salesType.ID;
                    _orderInfo.SalesTypeName = salesType.EName;

                    this.cmbSalesType.ShowSelectedValue(_orderInfo.SalesTypeID, _orderInfo.SalesTypeName);

                    if (_orderInfo.SalesTypeID == new Guid("E34BDCAA-4253-41C0-B14B-E38111CF2FC8"))
                    {
                        this.cmbSalesType_EditValueChanged(null, null);
                    }
                }
            }
        }

        #region 最近10票业务

        /// <summary>
        /// 最近业务
        /// </summary>
        private void SetRecentlyOrderList()
        {
            if (_orderInfo.ID != Guid.Empty || _orderInfo.CompanyID == Guid.Empty || _orderInfo.CustomerID == Guid.Empty)
            {
                bsOrders.Clear();
            }
            else
            {
                bsOrders.Clear();
                List<OceanOrderInfo> orderList = oiService.GetOIRecentlyOrderList(_orderInfo.CompanyID, _orderInfo.CustomerID, LocalData.UserInfo.LoginID, 10);
                if (orderList != null && orderList.Count > 0)
                {
                    bsOrders.DataSource = orderList;
                    stxtCustomer.ShowPopup();
                }
            }
        }

        private void gvOrders_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentOrderList == null)
            {
                return;
            }

            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "是否覆盖当前页面数据?" : "是否覆盖当前页面数据?"
                              , LocalData.IsEnglish ? "Tip" : "提示"
                              , MessageBoxButtons.YesNo
                              , MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                OceanOrderInfo order = oiService.GetOIOrderInfo(CurrentOrderList.ID);
                if (order == null)
                {
                    return;
                }

                order.ID = Guid.Empty;
                order.RefNo = string.Empty;
                order.SalesID = LocalData.UserInfo.LoginID;
                order.SalesName = LocalData.UserInfo.LoginName;
                order.CreateDate = DateTime.Now;
                _orderInfo = order;
                //bsOrderInfo.DataSource = _orderInfo;
                //bsOrderInfo.ResetBindings(false);

                //this.InitControls();

                this.ShowOrder();
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
        private List<OrganizationList> SetSalesDepartment(Guid salesId)
        {
            List<OrganizationList> userCompanyList = userService.GetUserCompanyList(salesId, null);

            treeBoxSalesDep.SetSource<OrganizationList>(userCompanyList, LocalData.IsEnglish ? "EShortName" : "CShortName");

            return userCompanyList;
        }

        void cmbSalesDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (treeBoxSalesDep.EditValue == Guid.Empty) return;

            Guid salesDepartmentID = treeBoxSalesDep.EditValue;
            SetOperatorBySalesDepartmentID(salesDepartmentID);
        }

        /// <summary>
        /// 设置了Sales后部门,动带出部门下的用户
        /// </summary>
        private void SetOperatorBySalesDepartmentID(Guid salesDepartmentID)
        {
            //cmbOperator.Properties.Items.Clear();
            if (salesDepartmentID == Guid.Empty) return;

            //List<ModuleUserList> operators = permissionService.GetModuleUserList(ModuleCodeConstants.OceanExport
            //                                                                    , ModuleCommandCodeConstants.OceanExport_Order
            //                                                                    , salesDepartmentID, 0);

            List<ModuleUserList> operators = permissionService.GetModuleUserList(ICP.FCM.OceanImport.UI.Common.CommandConstants.OceanImport_BLList
                                                                                 , salesDepartmentID, 0);

            if (operators.Count == 0) return;

            //foreach (var item in operators)
            //{
            //    cmbOperator.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
            //        (LocalData.IsEnglish? item.EName :item.CName, item.ID));
            //}
            //cmbOperator.SelectedIndex =0;
        }

        /// <summary>
        /// 清空Sales后,自动清空Sales部门.操作
        /// </summary>
        private void ClearSalesDepartment()
        {
            treeBoxSalesDep.ClearItems();
            _orderInfo.SalesDepartmentID = Guid.Empty;
            //cmbOperator.Properties.Items.Clear();
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
                this._orderInfo.CargoDescription = null;
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

            if (this._orderInfo.CargoDescription == null)
            {
                this._orderInfo.CargoDescription = new CargoDescription();
            }
            if (cargoType == CargoType.Awkward)
            {
                if (_orderInfo.CargoDescription.Cargo == null || _orderInfo.CargoDescription.Cargo is AwkwardCargo == false)
                {
                    _orderInfo.CargoDescription.Cargo = new AwkwardCargo();
                }

                if (cargoDescriptionPart1 is ICP.FCM.OceanImport.UI.Common.Controls.AwkwardDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.OceanImport.UI.Common.Controls.AwkwardDescriptionPart();
                    panel2.Controls.Add(cargoDescriptionPart1);

                    cargoDescriptionPart1.ShowWeightUnit(this._weightUnits);
                }
            }
            else if (cargoType == CargoType.Dangerous)
            {
                if (_orderInfo.CargoDescription.Cargo == null || _orderInfo.CargoDescription.Cargo is DangerousCargo == false)
                {
                    _orderInfo.CargoDescription.Cargo = new DangerousCargo();
                }


                if (cargoDescriptionPart1 is ICP.FCM.OceanImport.UI.Common.Controls.DangerousDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.OceanImport.UI.Common.Controls.DangerousDescriptionPart();
                    panel2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Dry)
            {
                if (_orderInfo.CargoDescription.Cargo == null || _orderInfo.CargoDescription.Cargo is DryCargo == false)
                {
                    _orderInfo.CargoDescription.Cargo = new DryCargo();
                }

                if (cargoDescriptionPart1 is ICP.FCM.OceanImport.UI.Common.Controls.DryDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.OceanImport.UI.Common.Controls.DryDescriptionPart();
                    panel2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Reefer)
            {
                if (_orderInfo.CargoDescription.Cargo == null || _orderInfo.CargoDescription.Cargo is ReeferCargo == false)
                {
                    _orderInfo.CargoDescription.Cargo = new ReeferCargo();
                }

                if (cargoDescriptionPart1 is ICP.FCM.OceanImport.UI.Common.Controls.ReeferDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.OceanImport.UI.Common.Controls.ReeferDescriptionPart();
                    panel2.Controls.Add(cargoDescriptionPart1);
                }
            }
            cargoDescriptionPart1.SetParentControl(sender, _orderInfo.CargoDescription, txtCargoDescription);
            this.cmbCargoType.EditValueChanged += new System.EventHandler(this.cmbCargoType_EditValueChanged);
        }

        private void RemoveCargoPart()
        {
            if (cargoDescriptionPart1 != null)
            {
                cargoDescriptionPart1.Hide();
                panel2.Controls.Remove(cargoDescriptionPart1);
                cargoDescriptionPart1.Dispose();
            }
        }

        #endregion

        #endregion

        #region 复制订单时的逻辑

        /// <summary>
        /// 复制订单时的逻辑
        /// </summary>
        void PrepareForCopyExistOrder()
        {
            //this._orderInfo.ID = Guid.Empty;            
            this._orderInfo.State = OIOrderState.NewOrder;
            this._orderInfo.RefNo = string.Empty;
            this._orderInfo.CreateByID = LocalData.UserInfo.LoginID;
            this._orderInfo.CreateByName = LocalData.UserInfo.LoginName;
            this._orderInfo.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            this._orderInfo.SalesID = LocalData.UserInfo.LoginID;
            this._orderInfo.SalesName = LocalData.UserInfo.LoginName;
        }

        #endregion

        #region IEditPart 成员

        void RefreshData(Guid orderId)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                GetDataList(orderId);

                if (listInfo != null)
                {
                    if (listInfo.EditMode == EditMode.Copy)
                    {
                        this.PrepareForCopyExistOrder();
                    }
                }

                this.ShowOrder();
                this.TriggerEventsAtOnce();
                this.ResetDescription();
                this.SetTitle();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Refresh Successfully" : "刷新成功");
            }
        }

        void GetDataList(Guid orderId)
        {
            this._orderInfo = oiService.GetOIOrderInfo(orderId);
        }

        void ShowOrder()
        {
            InitData(_orderInfo);

            this.bsOrderInfo.DataSource = _orderInfo;
            bsOrderInfo.ResetBindings(false);

            InitControls();

            List<OceanImportPOList> pos = null;

            List<OceanImportFeeList> feelist = null;

            if (_orderInfo.ID == Guid.Empty)
            {
                pos = new List<OceanImportPOList>();
                feelist = new List<OceanImportFeeList>();
            }
            else
            {
                pos = oiService.GetOIOrderPOList(_orderInfo.ID);
                feelist = oiService.GetOIOrderFeeList(_orderInfo.ID);
            }



            //请空主键ID，则表示新增的订单
            if (listInfo != null)
            {
                if (listInfo.EditMode == EditMode.Copy)
                {
                    _orderInfo.ID = Guid.Empty;
                    _orderInfo.IsDirty = true;
                }
                else
                {
                    _orderInfo.IsDirty = false;
                }
            }
            else
            {
                _orderInfo.IsDirty = false;
            }

            this.orderFeeEditPart1.SetSource(feelist);
            this.orderPOEditPart1.SetSource(pos);

            SetTools();
        }


        void BindingData(object data)
        {
            this.SuspendLayout();

            this.orderFeeEditPart1.SetService(Workitem);
            this.orderPOEditPart1.SetService(Workitem);
            orderPOEditPart1.IsOrderPO = true;

            listInfo = data as OceanOrderList;
            //OceanOrderInfo info = data as OceanOrderInfo;

            //if (info != null)
            //{
            //    if (info.ID == Guid.Empty)
            //    {
            //        _orderInfo = info;
            //    }
            //    else
            //    {
            //        this.RefreshData(info.ID);
            //    }
            //}
            //else if
            if (listInfo == null)
            {
                this._orderInfo = new OceanOrderInfo();
                this.ReadyForNew();
            }
            else
            {
                this.GetDataList(listInfo.ID);
            }

            this.ShowOrder();

            SearchRegister();

            this.SetLazyLoaders();

            this.SetLazyDataLodersWithDynamicCondition();

            this.ResumeLayout();

            this._orderInfo.IsDirty = false;
        }

        public override object DataSource
        {
            get { return bsOrderInfo.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return this.Save(this._orderInfo, false);
        }

        public override void EndEdit()
        {
            this.Validate();
            bsOrderInfo.EndEdit();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region 工具栏事件

        #region 界面输入验证

        bool ValidateData()
        {
            this.EndEdit();
            List<bool> isScrrs = new List<bool> { true, true };
            isScrrs[0] = _orderInfo.Validate
                (
                    delegate(ValidateEventArgs e)
                    {
                        if (_orderInfo.POLID != Guid.Empty && _orderInfo.POLID == _orderInfo.PODID)
                        {
                            e.SetErrorInfo("PODID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");
                        }

                        if (_orderInfo.ExpectedShipDate != null && _orderInfo.ExpectedArriveDate != null)
                        {
                            if (_orderInfo.ExpectedArriveDate.Value.Date < _orderInfo.ExpectedShipDate.Value.Date)
                            {
                                e.SetErrorInfo("ExpectedArriveDate",
                                    LocalData.IsEnglish ? "ExpectedArriveDate can't less than ExpectedShipDate" : "期望到达日不能小于期望出运日.");
                            }
                        }

                        #region ContainerDemand

                        //如果选择整箱业务类型，箱需求必输；箱需求逻辑,点击对应的箱型n次,则显示n*箱型
                        if (_orderInfo.OIOperationType == FCMOperationType.FCL)
                        {
                            if (this.containerDemandControl1.Text.Trim().Length == 0)
                            {
                                e.SetErrorInfo("ConsigneeDescription", LocalData.IsEnglish ? "FCL Bussines Must Input." : "整箱业务必须输入箱需求.");
                                this.dxErrorProvider1.SetError(containerDemandControl1.ErrorHost, LocalData.IsEnglish ? "FCL Bussines Must Input." : "整箱业务必须输入箱需求.");
                                isScrrs[0] = false;
                            }
                            else
                            {
                                this.dxErrorProvider1.SetError(containerDemandControl1.ErrorHost, string.Empty);
                            }
                        }
                        //把箱需求转换成对象
                        _orderInfo.ContainerDescription = new ContainerDescription(this.containerDemandControl1.Text.Trim());


                        #endregion
                    }
                );

            #region childParts
            if (orderFeeEditPart1.ValidateData() == false)
            {
                isScrrs[0] = false;
            }
            if (orderPOEditPart1.ValidateData() == false)
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
                xtraTabControl1.SelectedTabPageIndex = 0;
            else if (isScrrs[1] == false)
                xtraTabControl1.SelectedTabPageIndex = 1;


            return isScrr;

        }

        #endregion

        #region 保存

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Save(this._orderInfo, false);
            }
        }
        private bool Save(OceanOrderInfo currentData, bool isSavingAs)
        {
            if (ValidateData() == false) return false;

            try
            {
                bool isSendMail = false;
                Guid orderID = currentData.ID;

                OrderSaveRequest originalOrder = SaveOrder(currentData);

                if (originalOrder != null && Utility.GuidIsNullOrEmpty(orderID))
                {
                    isSendMail = true;
                }


                List<FeeSaveRequest> originalFees = this.orderFeeEditPart1.GetFeeList(_orderInfo.ID);
                List<POSaveRequest> originalPos = this.orderPOEditPart1.GetPoList(_orderInfo.ID);

                Dictionary<Guid, SaveResponse> saved = this.oiService.SaveOIOrderWithTrans(originalOrder,
                    originalFees, originalPos);

                if (originalOrder != null)
                {
                    SaveResponse.Analyze(new List<SaveRequest> { originalOrder }, saved, true);
                    this.RefreshUI(originalOrder);
                }

                if (originalFees != null)
                {
                    SaveResponse.Analyze(originalFees.Cast<SaveRequest>().ToList(), saved, true);
                    this.orderFeeEditPart1.RefreshUI(originalFees);
                }

                if (originalPos != null)
                {
                    SaveResponse.Analyze(originalPos.Cast<SaveRequest>().ToList(), saved, true);
                    this.orderPOEditPart1.RefreshUI(originalPos);
                }

                if (!isSavingAs)
                {
                    AfterSave();
                }


                //发送邮件
                if (isSendMail)
                {
                    WaitCallback callback = (obj) =>
                    {
                        SendOrderEMail();
                    };
                    try { ThreadPool.QueueUserWorkItem(callback); }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);

                return false;
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        private void SendOrderEMail()
        {
            if (Utility.GuidIsNullOrEmpty(_orderInfo.CustomerContactID))
            {
                return;
            }
            this.barSave.Enabled = false;
            try
            {
                ////邮件标题
                //string title = _orderInfo.RefNo;
                ////邮件内容
                //string content = string.Format(NativeLanguageService.GetText(this, "OrderSendEMailStyle"), DateTime.Now.ToString(), _orderInfo.RefNo);
                //content = "Hi " + _orderInfo.CustomerContactName + ":" + System.Environment.NewLine + content;
                //content = content + System.Environment.NewLine + System.Environment.NewLine + LocalData.UserInfo.UserName + System.Environment.NewLine + DateTime.Now.ToShortDateString();
                //邮件接收人
                //List<Guid> lstToUsers = new List<Guid>();
                //lstToUsers.Add(_orderInfo.CustomerContactID.Value);               
                //CC
                //List<Guid> lstCC = new List<Guid>();
                //lstCC.Add(_orderInfo.SalesID.Value);
                //模板路径
                string fileName = OceanImportPrintHelper.GetOIReportPath();
                //if (LocalData.IsEnglish) fileName = System.IO.Path.Combine(fileName, "OI_OrderInfo_EN.frx");
                //else fileName = System.IO.Path.Combine(fileName, "OI_OrderInfo_CN.frx"); 
                ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(_orderInfo.CompanyID);
                if (configureInfo.SolutionID == new Guid("b6e4dded-4359-456a-b835-e8401c910fd0"))
                {
                    //越南，泰国，马来西亚，用英文模板
                    if (_orderInfo.CompanyID == new Guid("5a827adf-38c7-4a2f-99a7-ad717ce91718") ||
                        _orderInfo.CompanyID == new Guid("13C26E30-F2AD-4D94-B13D-5E337EA97936") ||
                        _orderInfo.CompanyID == new Guid("1DBF7671-0D2D-4F08-A8A9-3663A0DB0037"))
                    {
                        fileName = System.IO.Path.Combine(fileName, "OI_OrderInfo_EN.frx");
                    }
                    else
                    {
                        //国内，用中文模板
                        fileName = System.IO.Path.Combine(fileName, "OI_OrderInfo_CN.frx");
                    }
                }
                else
                {
                    //北美和加拿大解决方案，用英文模板
                    fileName = System.IO.Path.Combine(fileName, "OI_OrderInfo_EN.frx");
                }

                //数据源
                OIOrderReportData data = OIReportSrvice.GetOIOrderReportData(_orderInfo.ID);
                if (data == null) return;
                Dictionary<string, object> reportSource = new Dictionary<string, object>();
                reportSource.Add("ReportSource", data);
                reportSource.Add("OrderFee", data.Fees);

                ICP.Message.ServiceInterface.Message message = CreateMessageInfo();
                //生成pdf附件，后发送邮件
                reportService.SendReport(message, fileName, reportSource);


                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Sent Successfully..." : "发送成功...");
                //eMailClientService.ShowEMailForm(LocalData.UserInfo.LoginID, new Guid[1] { _orderInfo.CustomerID }, new Guid[0], title, content);

                eMailClientService.SendAndSaveLog(message);

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
            finally
            {
                barSave.Enabled = true;
            }
        }


        ICP.Message.ServiceInterface.Message CreateMessageInfo()
        {
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.SendFrom = LocalData.UserInfo.EmailAddress;


            UserInfo receiveInfo = userService.GetUserInfo(_orderInfo.CustomerContactID.Value);
            if (receiveInfo != null && !string.IsNullOrEmpty(receiveInfo.EMail))
            {
                message.SendTo = receiveInfo.EMail;
            }

            UserInfo ccInfo = userService.GetUserInfo(_orderInfo.SalesID.Value);
            if (ccInfo != null && !string.IsNullOrEmpty(ccInfo.EMail))
            {
                message.CC = ccInfo.EMail;
            }
            //邮件标题
            string title = _orderInfo.RefNo;
            //邮件内容
            string content = string.Format(NativeLanguageService.GetText(this, "OrderSendEMailStyle"), DateTime.Now.ToString(), _orderInfo.RefNo);
            content = "Hi " + _orderInfo.CustomerContactName + ":" + System.Environment.NewLine + content;
            content = content + System.Environment.NewLine + System.Environment.NewLine + LocalData.UserInfo.UserName + System.Environment.NewLine + DateTime.Now.ToShortDateString();
            message.Subject = title;
            message.Body = content;
            message.BodyFormat = BodyFormat.olFormatHTML;
            MessageUserPropertiesObject userPropertiesObject = message.UserProperties;
            userPropertiesObject.FormId = _orderInfo.ID;
            userPropertiesObject.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            userPropertiesObject.OperationId = _orderInfo.ID;
            userPropertiesObject.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanImport;


            return message;
        }
        private void AfterSave()
        {
            SetTools();

            this._orderInfo.BeginEdit();
            this.stxtCustomer.Properties.Buttons[2].Visible = false;
            this.gvOrders.DoubleClick -= new System.EventHandler(this.gvOrders_DoubleClick);
            this.stxtCustomer.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.stxtCustomer_ButtonClick);
            barSaveAs.Enabled = true;
            barConfirmBooking.Enabled = true;
            barConfirmBookingShip.Enabled = true;

            if (this._orderInfo.State == OIOrderState.Rejected)
            {
                string title = LocalData.IsEnglish ? "Please confirma" : "请确认";
                string message = LocalData.IsEnglish ? "Do you want to submit to operators?" : "将这张单提交给操作吗？";
                DialogResult dr = XtraMessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        SingleResultData result = this.oiService.ChangeOIOrderState(this._orderInfo.ID, OIOrderState.NewOrder, "Changed state by user with prompt confirmation.", LocalData.UserInfo.LoginID, this._orderInfo.UpdateDate);
                        this._orderInfo.State = OIOrderState.NewOrder;
                        this._orderInfo.UpdateDate = result.UpdateDate;

                        this.SetState();
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully." : "保存成功");
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully.But failed to change order state from Rejected to NewOrder." : "保存成功,但是试图修改订单状态时失败.");
                    }
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                }
            }
            else
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
            }

            if (Saved != null)
            {
                this._orderInfo.OverSeasFilerName = this.mcmOverseasFiler.Text;
                Saved(new object[] { _orderInfo });
                this._orderInfo.IsDirty = false;
            }

            this._orderInfo.IsDirty = false;
            if (_orderInfo.ShipperDescription != null)
            {
                _orderInfo.ShipperDescription.IsDirty = false;
            }
            if (_orderInfo.ConsigneeDescription != null)
            {
                _orderInfo.ConsigneeDescription.IsDirty = false;
            }
            if (_orderInfo.AgentDescription != null)
            {
                _orderInfo.AgentDescription.IsDirty = false;
            }

            this.TriggerEventsAtOnce();

            this.SetTitle();
        }

        void SetTitle()
        {
            if (this._orderInfo.ID == Guid.Empty)
            {
                this.Title = LocalData.IsEnglish ? "Add Order" : "新增订单";
            }
            else
            {
                string titleNo = string.Empty;

                if (this._orderInfo.RefNo.Length > 4)
                {
                    titleNo = this._orderInfo.RefNo.Substring(this._orderInfo.RefNo.Length - 4, 4);
                }
                else
                {
                    titleNo = this._orderInfo.RefNo;
                }

                this.Title = LocalData.IsEnglish ? "Order " + titleNo : "订单：" + titleNo;
            }
        }

        /// <summary>
        /// TODO: 大bug，保存之后没有取到NO
        /// </summary>
        /// <param name="currentData"></param>
        private OrderSaveRequest SaveOrder(OceanOrderInfo currentData)
        {
            this.EndEdit();

            OrderSaveRequest saveRequest = new OrderSaveRequest();
            saveRequest.ID = currentData.ID;
            saveRequest.RefNo = currentData.RefNo;
            saveRequest.CompanyID = currentData.CompanyID;
            saveRequest.OIOperationType = currentData.OIOperationType;
            saveRequest.CustomerID = currentData.CustomerID;
            saveRequest.CustomerDescription = new CustomerDescription();
            saveRequest.TradeTermID = currentData.TradeTermID;
            saveRequest.SalesTypeID = currentData.SalesTypeID;
            saveRequest.SalesID = currentData.SalesID;
            saveRequest.SalesDepartmentID = currentData.SalesDepartmentID;
            saveRequest.TransportClauseID = currentData.TransportClauseID;
            saveRequest.PaymentTermID = currentData.PaymentTermID;
            saveRequest.OverSeasFilerId = currentData.OverSeasFilerId;
            saveRequest.BookingMode = currentData.BookingMode;
            saveRequest.BookingDate = currentData.BookingDate;
            saveRequest.AgentID = currentData.AgentID;
            saveRequest.AgentDescription = currentData.AgentDescription;
            saveRequest.ShipperID = currentData.ShipperID;
            saveRequest.ShipperDescription = currentData.ShipperDescription;
            saveRequest.ConsigneeID = currentData.ConsigneeID;
            saveRequest.ConsigneeDescription = currentData.ConsigneeDescription;
            saveRequest.PlaceOfReceiptID = currentData.PlaceOfReceiptID;
            saveRequest.POLID = currentData.POLID;
            saveRequest.PODID = currentData.PODID;
            saveRequest.PlaceOfDeliveryID = currentData.PlaceOfDeliveryID;
            saveRequest.FinalDestinationId = currentData.FinalDestinationId;
            saveRequest.Commodity = currentData.Commodity;
            saveRequest.Quantity = currentData.Quantity;
            saveRequest.QuantityUnitID = currentData.QuantityUnitID;
            saveRequest.Weight = currentData.Weight;
            saveRequest.WeightUnitID = currentData.WeightUnitID;
            saveRequest.Measurement = currentData.Measurement;
            saveRequest.MeasurementUnitID = currentData.MeasurementUnitID;
            saveRequest.CargoDescription = currentData.CargoDescription;
            saveRequest.ContainerDescription = currentData.ContainerDescription;
            saveRequest.CarrierID = currentData.CarrierID;
            saveRequest.ExpectedShipDate = currentData.ExpectedShipDate;
            saveRequest.ExpectedArriveDate = currentData.ExpectedArriveDate;
            saveRequest.IsTruck = currentData.IsTruck;
            saveRequest.IsCustoms = currentData.IsCustoms;
            saveRequest.IsCommodityInspection = currentData.IsCommodityInspection;
            saveRequest.IsQuarantineInspection = currentData.IsQuarantineInspection;
            saveRequest.IsWarehouse = currentData.IsWarehouse;
            saveRequest.FilerID = currentData.CustomerContactID;
            saveRequest.HBLReleaseType = currentData.HBLReleaseType;
            saveRequest.Remark = currentData.Remark;
            saveRequest.SaveByID = LocalData.UserInfo.LoginID;
            saveRequest.UpdateDate = currentData.UpdateDate;

            saveRequest.AddInvolvedObject(currentData);
            return saveRequest;
        }

        void RefreshUI(OrderSaveRequest saveRequest)
        {
            SingleResult result = saveRequest.SingleResult;
            OceanOrderInfo currentData = saveRequest.UnBoxInvolvedObject<OceanOrderInfo>()[0];


            currentData.ID = result.GetValue<Guid>("ID");
            currentData.RefNo = result.GetValue<string>("NO");
            currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            currentData.State = (OIOrderState)result.GetValue<byte>("State");
        }

        #endregion

        #region 确认订舱和确认装船
        /// <summary>
        /// 确认订舱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barConfirmBooking_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_orderInfo == null) return;

            ConfirmBookingForm memoContentForm = Workitem.Items.AddNew<ConfirmBookingForm>();
            string title = LocalData.IsEnglish ? "Risk Clients" : "确认订舱";
            memoContentForm.LabelText = LocalData.IsEnglish ? "Memo for cancel" : "确认订舱备注";

            if (PartLoader.ShowDialog(memoContentForm, title) != DialogResult.OK)
            {
                return;
            }

            try
            {
                SingleResultData result = oiService.ChangeOIOrderState(_orderInfo.ID, OIOrderState.BookingConfirmed, memoContentForm.Memo, LocalData.UserInfo.LoginID, _orderInfo.UpdateDate);
                _orderInfo.State = OIOrderState.BookingConfirmed;
                _orderInfo.UpdateDate = result.UpdateDate;
                if (this.Saved != null)
                {
                    this.Saved(_orderInfo);
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Disuse Successfully" : "确认订舱成功!");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }
        /// <summary>
        /// 确认装船
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barConfirmBookingShip_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_orderInfo == null) return;

            ConfirmBookingForm memoContentForm = Workitem.Items.AddNew<ConfirmBookingForm>();
            string title = LocalData.IsEnglish ? "Risk Clients" : "确认装船";
            memoContentForm.LabelText = LocalData.IsEnglish ? "Memo for Set to Risk Clients" : "确认装船备注";

            if (PartLoader.ShowDialog(memoContentForm, title) != DialogResult.OK)
            {
                return;
            }

            try
            {
                SingleResultData result = oiService.ChangeOIOrderState(_orderInfo.ID, OIOrderState.LoadVoyage, memoContentForm.Memo, LocalData.UserInfo.LoginID, _orderInfo.UpdateDate);
                _orderInfo.State = OIOrderState.LoadVoyage;
                _orderInfo.UpdateDate = result.UpdateDate;

                if (this.Saved != null)
                {
                    this.Saved(_orderInfo);
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Disuse Successfully" : "确认装船成功!");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
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
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save as a new order successfully. Ref. NO. is " + this._orderInfo.RefNo + "." : "已成功另存为一票新订单，业务号为" + this._orderInfo.RefNo + "。");
                    if (Saved != null)
                    {
                        Saved(new object[] { _orderInfo });
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

            if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "是否另存为一票新的订单?",
                            LocalData.IsEnglish ? "Tip" : "提示",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }

            OceanOrderInfo orderInfo = Utility.Clone<OceanOrderInfo>(_orderInfo);
            orderInfo.ID = Guid.Empty;
            orderInfo.RefNo = string.Empty;
            orderInfo.State = OIOrderState.NewOrder;
            orderInfo.CreateByID = LocalData.UserInfo.LoginID;
            orderInfo.CreateByName = LocalData.UserInfo.LoginName;
            orderInfo.CreateDate = DateTime.Now;
            orderInfo.BookingDate = DateTime.Now;
            orderInfo.UpdateDate = null;

            orderInfo.IsDirty = true;
            this.orderPOEditPart1.InitData(this._orderInfo.ID);
            this._orderInfo = orderInfo;

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
                this.RefreshData(this._orderInfo.ID);
                this.ShowOrder();
                this.TriggerEventsAtOnce();
                this.ResetDescription();
            }
        }

        #endregion

        #region 关闭

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        #endregion

        #endregion

        #region 资源回收

        void OrderBaseEditPart_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                if (configureInfo.SolutionID == new Guid("b6e4dded-4359-456a-b835-e8401c910fd0"))
                {
                    _isFarEastSolution = true;
                }

                cmbCompany.Focus();
                this.SetTitle();
                this.RegisterRelativeEvents();
                this.RegisterRelativeEventsAndRunOnce();
            }
        }

        /// <summary>
        /// 从工作项集合中移除自己
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OrderBaseEditPart_Disposed(object sender, EventArgs e)
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //设置客户控件水印
            Utility.SetCustomerTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtCustomer
            });
            //设置港口控件水印
            Utility.SetPortTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtPlaceOfDelivery,
                stxtPlaceOfReceipt ,
                stxtFinalDestination ,
                stxtPOD,
                stxtPOL,
            });
            this.mcmbSales.SpecifiedBackColor = this.txtNo.BackColor;
            _orderInfo.BeginEdit();

            SetPermissions();

            this.SmartPartClosing += new EventHandler<WorkspaceCancelEventArgs>(OrderBaseEditPart_SmartPartClosing);
            this.ActivateSmartPartClosingEvent(this.Workitem);

        }



        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            if (!ICP.Framework.CommonLibrary.Client.LocalCommonServices.PermissionService.HaveActionPermission(FCMPermissionsConstants.FCM_EditOrder))
            {
                this.barSave.Visibility = BarItemVisibility.Never;
                this.barSaveAs.Visibility = BarItemVisibility.Never;
                this.barConfirmBooking.Visibility = BarItemVisibility.Never;


            }
            if (ICP.Framework.CommonLibrary.Client.LocalCommonServices.PermissionService.HaveActionPermission(FCMPermissionsConstants.FCM_NAServices)
                && _orderInfo != null && _orderInfo.CreateByID != LocalData.UserInfo.LoginID)
            {
                this.barSave.Enabled = false;
            }
        }

        /// <summary>
        /// 设置工具栏的是否可用
        /// </summary>
        public void SetTools()
        {
            barSave.Enabled = true;

            #region 无效的
            if (!_orderInfo.IsValid)
            {
                barSave.Enabled = false;
                barSaveAs.Enabled = false;
            }
            #endregion

            #region 新增的
            if (_orderInfo.IsNew)
            {

                barSaveAs.Enabled = false;
                barPrint.Enabled = false;
                barRefresh.Enabled = false;
                barConfirmBooking.Enabled = false;
                barConfirmBookingShip.Enabled = false;

            }

            #endregion


            #region 符合订舱的
            if ((_orderInfo.State == OIOrderState.NewOrder || _orderInfo.State == OIOrderState.Checked) && _orderInfo.IsValid && !_orderInfo.IsNew)
            {
                barConfirmBooking.Enabled = true;
            }
            else
            {
                barConfirmBooking.Enabled = false;
            }

            #endregion

            #region 符合装船的
            if ((_orderInfo.State == OIOrderState.NewOrder || _orderInfo.State == OIOrderState.Checked || _orderInfo.State == OIOrderState.BookingConfirmed) && _orderInfo.IsValid && !_orderInfo.IsNew)
            {
                barConfirmBookingShip.Enabled = true;
            }
            else
            {
                barConfirmBookingShip.Enabled = false;
            }
            #endregion




        }

        void OrderBaseEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (IsDirty &&
                this.barSave.Visibility == BarItemVisibility.Always &&
                this.barSave.Enabled)
            {
                DialogResult dr = Utility.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!this.Save(this._orderInfo, false))
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        #endregion
    }
}
