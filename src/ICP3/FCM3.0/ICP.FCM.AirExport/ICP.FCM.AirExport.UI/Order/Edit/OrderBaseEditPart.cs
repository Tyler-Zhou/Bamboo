using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface.CompositeObjects;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using ICP.Framework.ClientComponents.Controls;
using ICP.Common.UI;
using ICP.Framework.ClientComponents.Service;
using ICP.OA.ServiceInterface.DataObjects;
using System.IO;
using ICP.OA.ServiceInterface;
using System.Threading;
using ICP.FCM.AirExport.UI.Common;
using DevExpress.XtraBars;
using ICP.FCM.Common.ServiceInterface.Common;
using ICP.Message.ServiceInterface;

namespace ICP.FCM.AirExport.UI.Order
{
    /// <summary>
    /// 空运订单编辑界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class OrderBaseEditPart : BaseEditPart
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
        public IConfigureService configureService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IGeographyService geographyService { get; set; }

        [ServiceDependency]
        public IAirExportService oeService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IPermissionService permissionService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelperService { get; set; }


        [ServiceDependency]
        public IClientMessageService ClientMessageService { get; set; }

        //[ServiceDependency]
        //public ICP.Message.ServiceInterface.IMessageService emailService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public AirExportPrintHelper AirExportPrintHelper { get; set; }
        #endregion

        #region 本地变量

        CustomerType customerType = CustomerType.Unknown;
        bool _isSearching = false;

        List<CountryList> _countryList = null;

        AirOrderList listInfo = null;

        /// <summary>
        /// 是否是远东区解决方案(根据当前登录人的默认公司所属解决方案)
        /// </summary>
        bool _isFarEastSolution = false;

        AirOrderInfo _orderInfo = null;

        AirOrderList CurrentOrderList
        {
            get
            {
                if (bsOrders.List == null || bsOrders.Current == null) return null;
                return bsOrders.Current as AirOrderList;
            }
        }

        #endregion

        #region 构造函数

        public OrderBaseEditPart()
        {
            InitializeComponent();
            if (DesignMode)
            {
                return;
            }

            if (LocalData.IsEnglish == false)
            {
                SetCnText();
            }
            InitMessage();
            this.Disposed += new EventHandler(OrderBaseEditPart_Disposed);
            this.Load += new EventHandler(OrderBaseEditPart_Load);
        }
        private void InitMessage()
        {
            this.RegisterMessage("1110170001", LocalData.IsEnglish ? "Are you sure to deleted the selected cost" : "确认删除选中的费用?");
            this.RegisterMessage("1110170002", LocalData.IsEnglish ? "Are you sure clear all cost" : "确认清空所有费用?");
            this.RegisterMessage("OrderSendEMailStyle", "我在 {0} 新增了一个业务号为 {1} 的订单,请留意.");
        }

        private void SetCnText()
        {
            navBarBaseInfo.Caption = "基本信息";
            navBarDelegateInfo.Caption = "委托信息";
            groupRemark.Text = "备注";
            labAgent.Text = "代理";
            this.labAbroadOP.Text = "文件";
            this.labState.Text = "状态";

            groupLocalService.Text = "本地服务";

            labBookingCustomer.Text = "订舱客户";
            labOrderDate.Text = "委托日期";
            labBookingMode.Text = "委托方式";

            labCargoType.Text = "货物类型";
            labCarrier.Text = "航空公司";
            labCompany.Text = "操作口岸";
            labConsignee.Text = "收货人";
            labCustomer.Text = "客户";
            labEstimatedDeliveryDate.Text = "估计交货";
            labExpectedArriveDate.Text = "期望到达";
            labExpectedShipDate.Text = "期望出运";

            labMeasurement.Text = "体积";
            labNo.Text = "业务号";
            labOperatorName.Text = "订舱";
            labPaymentTerm.Text = "付款方式";
            checkEdit1.Text = "是否有效";
            labCommodity.Text = "品名";
            labPOD.Text = "目的港";
            labPOL.Text = "起运港";
            labQuantity.Text = "数量";
            labSales.Text = "揽货人";
            labSalesDepartment.Text = "揽货部门";
            labSalesType.Text = "揽货类型";
            labShipper.Text = "发货人";
            labTradeTerm.Text = "贸易条款";
            labTransportClause.Text = "运输条款";
            labWeight.Text = "重量";

            chkIsTruck.Text = "拖车";
            chkIsCustoms.Text = "报关";
            chkIsCommodityInspection.Text = "商检";
            chkIsQuarantineInspection.Text = "质检";
            chkIsWarehouse.Text = "仓储";
            chkIsOnlyMBL.Text = "只出MBL";
            labPlaceOfDelivery.Text = "交货地";
            barRefresh.Caption = "刷新(&R)";
            barSave.Caption = "保存(&S)";
            barSaveAs.Caption = "另存为(&A)";
            //barSendEmail.Caption = "发送邮件(&E)";
            barPrint.Caption = "打印(&P)";
            //barConfirmOrder.Caption = "订单确认(&O)";
            barClose.Caption = "关闭(&C)";

            tabPageBase.Text = "基础";
            navFee.Caption = "费用信息";

            colClosingDate.Caption = "截关日";
            colConsigneeName.Caption = "收货人";
            colShipperName.Caption = "发货人";
            colNo.Caption = "业务号";
            colPOLName.Caption = "装货地";
            colPODName.Caption = "卸货地";
        }

        #endregion

        #region 新订单的逻辑

        void ReadyForNew()
        {
            AirOrderInfo newData = new AirOrderInfo();
            newData.State = AEOrderState.NewOrder;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.Now;
            newData.BookingDate = DateTime.Now;
            newData.SalesID = LocalData.UserInfo.LoginID;
            newData.SalesName = LocalData.UserInfo.LoginName;
            //newData.CargoDescription = new ICP.FCM.Common.ServiceInterface.DataObjects.CargoDescription();
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

            this._orderInfo = newData;

            this._orderInfo.HBLReleaseType = this._orderInfo.MBLReleaseType = FCMReleaseType.Unknown;
            //this._orderInfo.OEOperationType = OEOperationType.FCL;

            barSaveAs.Enabled = false;
            this.gvOrders.DoubleClick += new System.EventHandler(this.gvOrders_DoubleClick);
            this.stxtCustomer.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.stxtCustomer_ButtonClick);

            Utility.EnsureDefaultCompanyExists(this.userService);
            Utility.EnsureDefaultDepartmentExists(this.userService);


            this._orderInfo.CompanyID = LocalData.UserInfo.DefaultCompanyID;
            this._orderInfo.CompanyName = LocalData.UserInfo.DefaultCompanyName;

            this._orderInfo.SalesDepartmentID = LocalData.UserInfo.DefaultDepartmentID;
            this._orderInfo.SalesDepartmentName = LocalData.UserInfo.DefaultDepartmentName;
        }

        #endregion

        #region 复制订单时的逻辑

        /// <summary>
        /// 复制订单时的逻辑
        /// </summary>
        void PrepareForCopyExistOrder()
        {
            //this._orderInfo.ID = Guid.Empty;
            this._orderInfo.State = AEOrderState.NewOrder;
            this._orderInfo.RefNo = string.Empty;

            this._orderInfo.CreateByID = LocalData.UserInfo.LoginID;
            this._orderInfo.CreateByName = LocalData.UserInfo.LoginName;
            this._orderInfo.CreateDate = DateTime.Now;
            this._orderInfo.SalesID = LocalData.UserInfo.LoginID;
            this._orderInfo.SalesName = LocalData.UserInfo.LoginName;
        }

        #endregion

        #region 不能为空的一些属性和数据

        /// <summary>
        /// 不能为空的一些属性和数据
        /// </summary>
        /// <param name="info"></param>
        private void InitData(AirOrderInfo info)
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

            if (this._orderInfo.BookingCustomerDescription == null)
                this._orderInfo.BookingCustomerDescription = new CustomerDescription();
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
            this.txtState.Text = EnumHelper.GetDescription<AEOrderState>(this._orderInfo.State, LocalData.IsEnglish);
        }

        /// <summary>
        /// 显示订单信息
        /// </summary>
        private void InitControls()
        {
            this.SetState();
            //操作口岸
            this.cmbCompany.ShowSelectedValue(this._orderInfo.CompanyID, this._orderInfo.CompanyName);
            this.trsSalesDep.ShowSelectedValue(this._orderInfo.SalesDepartmentID, this._orderInfo.SalesDepartmentName);

            orderFeeEditPart1.SetCompanyID(this._orderInfo.CompanyID);

            //运输条款
            this.cmbTransportClause.ShowSelectedValue(this._orderInfo.TransportClauseID, this._orderInfo.TransportClauseName);

            this.cmbTradeTerm.ShowSelectedValue(this._orderInfo.TradeTermID, this._orderInfo.TradeTermName);
            this.cmbQuantityUnit.ShowSelectedValue(this._orderInfo.QuantityUnitID, this._orderInfo.QuantityUnitName);
            this.cmbMeasurementUnit.ShowSelectedValue(this._orderInfo.MeasurementUnitID, this._orderInfo.MeasurementUnitName);
            this.cmbWeightUnit.ShowSelectedValue(this._orderInfo.WeightUnitID, this._orderInfo.WeightUnitName);
            //航空公司
            Utility.SetEnterToExecuteOnec(mcmbCarrier, delegate
            {
                ICPCommUIHelperService.BindCustomerList(this.mcmbCarrier, CustomerType.Airline);
            });
            mcmbCarrier.ShowSelectedValue(this._orderInfo.CarrierID, this._orderInfo.CarrierName);
            //货物类型
            if (this._orderInfo.CargoType.HasValue)
            {
                this.cmbCargoType.ShowSelectedValue(this._orderInfo.CargoType,
                    EnumHelper.GetDescription<CargoType>(this._orderInfo.CargoType.Value, LocalData.IsEnglish));
            }

            //揽货类型
            this.cmbSalesType.ShowSelectedValue(this._orderInfo.SalesTypeID, this._orderInfo.SalesTypeName);

            //揽货人
            this.mcmbSales.ShowSelectedValue(this._orderInfo.SalesID, this._orderInfo.SalesName);

            //船公司
            this.mcmbCarrier.ShowSelectedValue(this._orderInfo.CarrierID, this._orderInfo.CarrierName);

            //委托方式
            this.cmbBookingMode.ShowSelectedValue(this._orderInfo.BookingMode,
                EnumHelper.GetDescription<FCMBookingMode>(this._orderInfo.BookingMode, LocalData.IsEnglish));

            #region CustomerDescription/CargoDescription/ContainerDescription

            if (this._orderInfo.CargoDescription != null && this._orderInfo.CargoDescription.Cargo != null)
            {
                txtCargoDescription.Text = this._orderInfo.CargoDescription.Cargo.ToString(LocalData.IsEnglish);
            }


            #endregion

            this.mcmOverseasFiler.ShowSelectedValue(this._orderInfo.OverSeasFilerId, this._orderInfo.OverSeasFilerName);

            this.mcmbBookinger.ShowSelectedValue(this._orderInfo.BookingerID, this._orderInfo.BookingerName);

        }

        #endregion

        #region 注册搜索器

        CustomerFinderBridge shipperBridge;
        CustomerFinderBridge consigneeBridge;
        CustomerFinderBridge bookingCustomerPartyBridge;

        /// <summary>
        /// 注册搜索器
        /// </summary>
        void SearchRegister()
        {
            AirOrderInfo currentData = bsOrderInfo.DataSource as AirOrderInfo;

            #region Customer

            //Customer
            dfService.Register(stxtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.CustomerResultValue,
                GetConditionsForCustomer,
                      delegate(object inputSource, object[] resultData)
                      {
                          Guid oldCustomerId = currentData.CustomerID;
                          stxtCustomer.ClosePopup();

                          CustomerStateType state = (CustomerStateType)resultData[7];
                          if (state == CustomerStateType.Invalid)
                          {
                              if (PartLoader.PopCustomerIsInvalid() != DialogResult.Yes)
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
                                      DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show("该客户尚未通过审核!"
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
                              this._isSearching = true;
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

                          SetRecentlyOrderListByCustomerAndCompany();
                          SetSalesTypeByCustomerAndCompany();
                          this.SetConsigneeByCustomerAndTradeTerm();
                          this.SetShipperByBookingCustomerAndTradeTerm();
                          this.SetBookingCustomer();
                      }, delegate
                      {
                          stxtCustomer.ClosePopup();
                          stxtCustomer.Text = currentData.CustomerName = string.Empty;
                          stxtCustomer.Tag = currentData.CustomerID = Guid.Empty;
                          SetSalesTypeByCustomerAndCompany();
                          SetRecentlyOrderListByCustomerAndCompany();
                          this.SetConsigneeByCustomerAndTradeTerm();
                          this.SetShipperByBookingCustomerAndTradeTerm();
                          this.SetBookingCustomer();
                      },
                      ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            #region 订舱客户
            dfService.Register(stxtBookingCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.CustomerResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          Guid oldBookingCustomerID = currentData.BookingCustomerID;
                          Guid customerID = new Guid(resultData[0].ToString());
                          if (customerID == oldBookingCustomerID)
                          {
                              return;
                          }

                          CustomerStateType state = (CustomerStateType)resultData[7];
                          if (state == CustomerStateType.Invalid)
                          {
                              if (PartLoader.PopCustomerIsInvalid() != DialogResult.Yes)
                              {
                                  return;
                              }
                          }

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

                          stxtBookingCustomer.Tag = currentData.BookingCustomerID = new Guid(resultData[0].ToString());
                          stxtBookingCustomer.Text = currentData.BookingCustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();

                      }, delegate
                      {
                          stxtBookingCustomer.Tag = currentData.BookingCustomerID = Guid.Empty;
                          stxtBookingCustomer.Text = currentData.BookingCustomerName = string.Empty;
                      },
                      ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
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

            ////BookingCustomer 
            //Utility.SetEnterToExecuteOnec(stxtBookingCustomer, delegate
            //{
            //    if (_countryList == null) _countryList = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);

            //    bookingCustomerPartyBridge = new CustomerFinderBridge(
            //    this.stxtBookingCustomer,
            //    _countryList,
            //    this.dfService,
            //    this.customerService,
            //    this._orderInfo.BookingCustomerDescription,
            //    ICPCommUIHelperService,
            //    LocalData.IsEnglish);
            //    bookingCustomerPartyBridge.Init();
            //});

            //stxtBookingCustomer.OnOk += new EventHandler(stxtBookingCustomer_OnOk);
            #endregion

            #endregion

            #region Port

            PortFinderBridge pfbPOL = new PortFinderBridge(this.stxtPOL, this.dfService, LocalData.IsEnglish);

            PortFinderBridge pfbPOD = new PortFinderBridge(this.stxtPOD, this.dfService, LocalData.IsEnglish);

            LocationFinderBridge pfbPlaceOfDelivery = new LocationFinderBridge(this.stxtPlaceOfDelivery, this.dfService, LocalData.IsEnglish);

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

        void stxtBookingCustomer_OnOk(object sender, EventArgs e)
        {
            if (_orderInfo != null && stxtBookingCustomer.CustomerDescription != null)
            {
                _orderInfo.BookingCustomerDescription = stxtBookingCustomer.CustomerDescription;
            }
        }

        void stxtConsignee_OnOk(object sender, EventArgs e)
        {
            if (_orderInfo != null && stxtConsignee.CustomerDescription != null)
            {
                _orderInfo.ConsigneeDescription = stxtConsignee.CustomerDescription;
            }
        }

        void stxtShipper_OnOk(object sender, EventArgs e)
        {
            if (_orderInfo != null && stxtShipper.CustomerDescription != null)
            {
                _orderInfo.ShipperDescription = stxtShipper.CustomerDescription;
            }
        }

        /// <summary>
        /// 当前客户最近业务所对应的海外部客服or 当前客户为新客户and当前揽货人最近业务所对应的海外部客服
        /// </summary>
        void SetDefaultOverseasFiler()
        {
            //List<UserInfo> users = this.oeService.GetOverseasFilersList(this._orderInfo.CustomerID, this._orderInfo.SalesID,
            //    DateTime.Now.AddDays(-30), DateTime.Now, 1);

            //if (users.Count > 0)
            //{
            //    this.mcmOverseasFiler.ShowSelectedValue(users[0].ID, LocalData.IsEnglish ? users[0].EName : users[0].CName);
            //}
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

            if (this.bookingCustomerPartyBridge != null)
            {
                this.bookingCustomerPartyBridge.SetCustomerDescription(this._orderInfo.BookingCustomerDescription);
            }
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
                ICPCommUIHelperService.BindCompanyByAll(this.cmbCompany, true);
            });
            //运输条款
            Utility.SetEnterToExecuteOnec(cmbTransportClause, delegate
            {
                List<TransportClauseList> transportClauseList = ICPCommUIHelperService.SetCmbTransportClause(cmbTransportClause);
            });

            //贸易条款
            Utility.SetEnterToExecuteOnec(cmbTradeTerm, delegate
            {
                ICPCommUIHelperService.SetCmbDataDictionary(cmbTradeTerm, DataDictionaryType.TradeTerm);
            });

            //包装
            Utility.SetEnterToExecuteOnec(cmbQuantityUnit, delegate
            {
                ICPCommUIHelperService.SetCmbDataDictionary(cmbQuantityUnit, DataDictionaryType.QuantityUnit);
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
                //List<DataDictionaryList> payments = ICPCommUIHelperService.SetCmbDataDictionary(
                //                                    cmbPaymentTerm,
                //                                    DataDictionaryType.PaymentTerm,false,false);
                List<DataDictionaryList> payments = ICPCommUIHelperService.SetCmbDataDictionary(
                                                    cmbPaymentTerm,
                                                    DataDictionaryType.PaymentTerm);
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

            ////船公司
            //Utility.SetEnterToExecuteOnec(mcmbCarrier, delegate
            //{
            //    List<CustomerList> carriers = ICPCommUIHelperService.SetMCmbCarrier(mcmbCarrier);
            //});
        }

        /// <summary>
        /// 延迟加载，而且条件是动态的
        /// </summary>
        void SetLazyDataLodersWithDynamicCondition()
        {
            //mcmOverseasFiler.Enter += new EventHandler(mcmOverseasFiler_Click);
            this.mcmbBookinger.Enter += new EventHandler(mcmbOperator_Click);
            this.trsSalesDep.Enter += new EventHandler(treeBoxSalesDep_Enter);
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

            this.trsSalesDep.Selected += new EventHandler(cmbSalesDepartment_SelectedIndexChanged);
            this.cmbCargoType.Click += new System.EventHandler(this.cmbCargoType_Enter);
            this.cmbCargoType.SelectedIndexChanged += new System.EventHandler(this.cmbCargoType_EditValueChanged);

            this.stxtCustomer.EditValueChanged += new EventHandler(stxtCustomer_EditValueChanged);

            this.stxtCustomer.LostFocus += new EventHandler(stxtCustomer_LostFocus);
            this.cmbTradeTerm.SelectedIndexChanged += new EventHandler(cmbTradeTerm_SelectedIndexChanged);
            this.cmbTransportClause.SelectedIndexChanged += new EventHandler(cmbTransportClause_SelectedIndexChanged);
            this.stxtPOD.TextChanged += new EventHandler(stxtPOD_TextChanged);
            this.stxtPlaceOfDelivery.TextChanged += new EventHandler(stxtPlaceOfDelivery_TextChanged);
        }

        void stxtPlaceOfDelivery_TextChanged(object sender, EventArgs e)
        {
            if (this._shown)
            {
                _orderInfo.FinalDestinationName = this.stxtPlaceOfDelivery.Text;
            }
        }//交货地

        void stxtPOD_TextChanged(object sender, EventArgs e)
        {
            if (this._shown && !Utility.GuidIsNullOrEmpty(this._orderInfo.PODID))
            {
                _orderInfo.PODName = this.stxtPOD.Text;
                this.SetPlaceOfDeliveryByTransportClause();
            }
        }//目的港

        void cmbTransportClause_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._shown)
            {
                this._orderInfo.TransportClauseName = this.cmbTransportClause.Text;
                SetPlaceOfDeliveryByTransportClause();
            }
        }

        /// <summary>
        /// 交货地 如果目的港运输条款<>Door，那么就为卸货港
        /// </summary>
        private void SetPlaceOfDeliveryByTransportClause()
        {
            if (!Utility.GuidIsNullOrEmpty(_orderInfo.FinalDestinationId)
                || Utility.GuidIsNullOrEmpty(this._orderInfo.TransportClauseID)) return;

            if (_orderInfo.TransportClauseName.Contains("-DOOR") == false)
            {
                stxtPlaceOfDelivery.Tag = _orderInfo.FinalDestinationId = _orderInfo.PODID;
                stxtPlaceOfDelivery.Text = _orderInfo.FinalDestinationName = _orderInfo.PODName;
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbTradeTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._shown)
            {
                this._orderInfo.TradeTermName = this.cmbTradeTerm.Text;
                this.SetConsigneeByCustomerAndTradeTerm();
                this.SetShipperByBookingCustomerAndTradeTerm();
                this.SetBookingCustomer();
            }
        }

        void SetBookingCustomer()
        {
            if (!Utility.GuidIsNullOrEmpty(this._orderInfo.BookingCustomerID))
            {
                return;
            }

            if (this._orderInfo.TradeTermName == "CIF")
            {
                this.stxtBookingCustomer.Tag = this._orderInfo.BookingCustomerID = this._orderInfo.CustomerID;
                this.stxtBookingCustomer.Text = this._orderInfo.BookingCustomerName = this._orderInfo.CustomerName;
                ICPCommUIHelperService.SetCustomerDesByID(this._orderInfo.BookingCustomerID, this._orderInfo.BookingCustomerDescription);
            }
        }



        /// <summary>
        /// 设置发货人 如果贸易条款为CIF，那么就为订舱客户
        /// </summary>
        private void SetShipperByBookingCustomerAndTradeTerm()
        {
            if (this._shown)
            {
                if (Utility.GuidIsNullOrEmpty(this._orderInfo.ShipperID) == false) return;

                if (!string.IsNullOrEmpty(this._orderInfo.TradeTermName) && this._orderInfo.TradeTermName.Contains("CIF"))
                {
                    stxtShipper.Tag = this._orderInfo.ShipperID = this._orderInfo.CustomerID;
                    stxtShipper.Text = this._orderInfo.ShipperName = this._orderInfo.CustomerName;
                    ICPCommUIHelperService.SetCustomerDesByID(this._orderInfo.CustomerID, this._orderInfo.ShipperDescription);
                }
            }
        }

        /// <summary>
        /// 收货人:如果贸易条款为FOB或EXWORK，那么就为客户
        /// </summary>
        private void SetConsigneeByCustomerAndTradeTerm()
        {
            if (this._shown)
            {
                if (Utility.GuidIsNullOrEmpty(this._orderInfo.ConsigneeID) == false) return;

                if (this._orderInfo.TradeTermName == "FOB" || this._orderInfo.TradeTermName == "EXWORK")
                {
                    this.stxtConsignee.Tag = this._orderInfo.ConsigneeID = this._orderInfo.CustomerID;
                    this.stxtConsignee.Text = this._orderInfo.ConsigneeName = this._orderInfo.CustomerName;
                    ICPCommUIHelperService.SetCustomerDesByID(_orderInfo.ConsigneeID, _orderInfo.ConsigneeDescription);
                }

                this.ResetDescription();
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
            this.SetAgentByCompany();
            this.cmbSalesType_EditValueChanged(this, null);
            RefreshBarEnabled();
        }

        void cmbSalesType_EditValueChanged(object sender, EventArgs e)
        {
            SetOverseasFilerEnabledBySalesType();
        }

        private void SetOverseasFilerEnabledBySalesType()
        {
            //if (this._shown)
            //{
            this.mcmOverseasFiler.Enabled = (this.cmbSalesType.Text == "指定货" || this.cmbSalesType.Text == "Agent");

            if (!this.mcmOverseasFiler.Enabled)
            {
                this.mcmOverseasFiler.ShowSelectedValue(Guid.Empty, string.Empty);//清除现有值
                this.mcmOverseasFiler.SpecifiedBackColor = this.txtNo.BackColor;
            }
            else
            {
                this.mcmOverseasFiler.SpecifiedBackColor = System.Drawing.Color.White;
            }
            //}
        }

        void RefreshBarEnabled()
        {

            if (Utility.GuidIsNullOrEmpty(this._orderInfo.ID))
            {
                this.barRefresh.Enabled = false;

                //this.barPrint.Enabled = false;
            }
            else
            {
                this.barRefresh.Enabled = true;

                //this.barPrint.Enabled = true;
            }
        }

        #endregion

        #region 界面控件联动

        #region 业务类型和集装箱信息

        void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.chkIsTruck.Enabled = this._orderInfo.OEOperationType == OEOperationType.FCL;

            if (!this.chkIsTruck.Enabled)
            {
                this.chkIsTruck.Checked = this._orderInfo.IsTruck = false;
            }
        }


        #endregion

        #region 客户显示代理

        void stxtCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (this._shown)
            {
                this.SetAgentByCompany();
            }
        }

        /// <summary>
        /// 根据公司ID设置代理Combobox的Item
        /// </summary>
        private void SetAgentByCompany()
        {
            if (string.IsNullOrEmpty(this._orderInfo.AgentName))
            {
                if (!Utility.GuidIsNullOrEmpty(this._orderInfo.CustomerID)
                           && !Utility.GuidIsNullOrEmpty(this._orderInfo.CompanyID))
                {
                    if (!oeService.IsCustomerAndCompanySameCountry(this._orderInfo.CustomerID, this._orderInfo.CompanyID))
                    {
                        if (!this._isSearching)
                        {
                            ICP.Common.ServiceInterface.DataObjects.CustomerInfo customerInfo
                            = customerService.GetCustomerInfo(this._orderInfo.CustomerID);
                            customerType = customerInfo.Type;
                        }

                        this._isSearching = false;

                        bool isDirty = this._orderInfo.IsDirty;

                        if (this.customerType == CustomerType.Forwarding)
                        {
                            this.txtAgent.Text = this._orderInfo.AgentName =
                                string.IsNullOrEmpty(this.stxtCustomer.Text) ? this._orderInfo.CustomerName
                                : this.stxtCustomer.Text;
                            this.txtAgent.Tag = this._orderInfo.AgentID = null;
                        }
                        else
                        {
                            this.txtAgent.Text = this._orderInfo.AgentName = string.Empty;
                            this.txtAgent.Tag = this._orderInfo.AgentID = null;
                        }

                        this._orderInfo.IsDirty = isDirty;//做一个缓存IsDirty的属性
                    }
                }
            }
        }

        #endregion

        void mcmbOperator_Click(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = (Guid)cmbCompany.EditValue;
            }

            ICPCommUIHelperService.SetComboxUsersByRole(mcmbBookinger, depID, "订舱", true);

        }

        void mcmOverseasFiler_Click(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = (Guid)cmbCompany.EditValue;
            }

            ICPCommUIHelperService.SetComboxUsersByRole(mcmOverseasFiler, depID, "客服", true);


        }

        /// <summary>
        /// 
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
                orderFeeEditPart1.SetCompanyID(this._orderInfo.CompanyID);
                SetSalesTypeByCustomerAndCompany();
                SetRecentlyOrderListByCustomerAndCompany();
                SetAgentByCompany();
            }
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
                DataDictionaryInfo salesType = oeService.GetSalesType(_orderInfo.CustomerID, _orderInfo.CompanyID);
                if (salesType != null)
                {
                    _orderInfo.SalesTypeID = salesType.ID;
                    _orderInfo.SalesTypeName = LocalData.IsEnglish ? salesType.EName : salesType.CName;

                    this.cmbSalesType.ShowSelectedValue(_orderInfo.SalesTypeID, _orderInfo.SalesTypeName);
                }
            }

            SetOverseasFilerEnabledBySalesType();
        }

        #region 最近10票业务

        /// <summary>
        /// 最近业务
        /// </summary>
        private void SetRecentlyOrderListByCustomerAndCompany()
        {
            if (_orderInfo.ID != Guid.Empty || _orderInfo.CompanyID == Guid.Empty || _orderInfo.CustomerID == Guid.Empty)
            {
                bsOrders.Clear();
            }
            else
            {
                bsOrders.Clear();
                List<AirOrderList> orderList = oeService.GetRecentlyAirOrderList(_orderInfo.CompanyID, _orderInfo.CustomerID, 10);
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
                AirOrderInfo order = oeService.GetAirOrderInfo(CurrentOrderList.ID);
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

            trsSalesDep.SetSource<OrganizationList>(userCompanyList, LocalData.IsEnglish ? "EShortName" : "CShortName");

            return userCompanyList;
        }

        void cmbSalesDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._shown)
            {
                if (trsSalesDep.EditValue == Guid.Empty) return;

                Guid salesDepartmentID = trsSalesDep.EditValue;
                SetOperatorBySalesDepartmentID(salesDepartmentID);
            }
        }

        /// <summary>
        /// 设置了Sales后部门,动带出部门下的用户
        /// </summary>
        private void SetOperatorBySalesDepartmentID(Guid salesDepartmentID)
        {
            //cmbOperator.Properties.Items.Clear();
            if (salesDepartmentID == Guid.Empty) return;

            //List<ModuleUserList> operators = permissionService.GetModuleUserList(ModuleCodeConstants.AirExport
            //                                                                    , ModuleCommandCodeConstants.AirExport_Order
            //                                                                    , salesDepartmentID, 0);

            List<ModuleUserList> operators = permissionService.GetModuleUserList(CommandConstants.FCM_AE_BLLIST
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
            trsSalesDep.ClearItems();
            _orderInfo.SalesDepartmentID = Guid.Empty;
            //cmbOperator.Properties.Items.Clear();
            _orderInfo.BookingerID = null;
        }

        #endregion


        #region Cargo

        private void cmbCargoType_Enter(object sender, EventArgs e)
        {
            if (cmbCargoType.EditValue == null
                || cmbCargoType.EditValue.ToString() == string.Empty)
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
            if (cmbCargoType.EditValue == null)
            {
                this._orderInfo.CargoDescription = null;
                RemoveCargoPart();
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
            if (!this.cmbCargoType.Focused)
            {
                return;
            }
            //if (this._orderInfo.CargoDescription == null)
            //{
            //    this.cmbCargoType.SelectedIndexChanged -= new EventHandler(cmbCargoType_EditValueChanged);
            //    this._orderInfo.CargoDescription = new CargoDescription();
            //    this.cmbCargoType.SelectedIndexChanged += new EventHandler(cmbCargoType_EditValueChanged);
            //}
            if (cargoType == CargoType.Awkward)
            {
                if (_orderInfo.CargoDescription == null
                    || _orderInfo.CargoDescription.Cargo == null || _orderInfo.CargoDescription.Cargo is AwkwardCargo == false)
                {
                    AwkwardCargo cargo = new AwkwardCargo();
                    cargo.NetWeightUnit = this.cmbWeightUnit.Text;
                    cargo.GrossWeightUnit = this.cmbWeightUnit.Text;
                    cargo.Quantity = (int)this.numQuantity.Value;
                    this._orderInfo.CargoDescription = new CargoDescription(cargo);
                    _orderInfo.IsDirty = true;
                }

                if (cargoDescriptionPart1 is ICP.FCM.AirExport.UI.Common.Controls.AwkwardDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.AirExport.UI.Common.Controls.AwkwardDescriptionPart();
                    cargoDescriptionPart1.ShowWeightUnit(this._weightUnits);
                    panel2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Dangerous)
            {
                if (_orderInfo.CargoDescription == null
                    || _orderInfo.CargoDescription.Cargo == null || _orderInfo.CargoDescription.Cargo is DangerousCargo == false)
                {
                    _orderInfo.CargoDescription = new CargoDescription(new DangerousCargo());
                    _orderInfo.IsDirty = true;
                }


                if (cargoDescriptionPart1 is ICP.FCM.AirExport.UI.Common.Controls.DangerousDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.AirExport.UI.Common.Controls.DangerousDescriptionPart();
                    panel2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Dry)
            {
                if (_orderInfo.CargoDescription == null
                    || _orderInfo.CargoDescription.Cargo == null || _orderInfo.CargoDescription.Cargo is DryCargo == false)
                {
                    _orderInfo.CargoDescription = new CargoDescription(new DryCargo());
                    _orderInfo.IsDirty = true;
                }

                if (cargoDescriptionPart1 is ICP.FCM.AirExport.UI.Common.Controls.DryDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.AirExport.UI.Common.Controls.DryDescriptionPart();
                    panel2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Reefer)
            {
                if (_orderInfo.CargoDescription == null
                    || _orderInfo.CargoDescription.Cargo == null || _orderInfo.CargoDescription.Cargo is ReeferCargo == false)
                {
                    _orderInfo.CargoDescription = new CargoDescription(new ReeferCargo());
                    _orderInfo.IsDirty = true;
                }

                if (cargoDescriptionPart1 is ICP.FCM.AirExport.UI.Common.Controls.ReeferDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.AirExport.UI.Common.Controls.ReeferDescriptionPart();
                    panel2.Controls.Add(cargoDescriptionPart1);
                }
            }
            cargoDescriptionPart1.SetParentControl(sender, _orderInfo.CargoDescription, txtCargoDescription);
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

        #region IEditPart 成员

        void RefreshData(Guid orderId)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                this.GetData(this._orderInfo.ID);
                this.ShowOrder();
                this.TriggerEventsAtOnce();
                this.ResetDescription();
                this.SetTitle();
            }
        }

        /// <summary>
        /// 从服务端重新获取订单信息
        /// </summary>
        /// <param name="orderId"></param>
        void GetData(Guid orderId)
        {
            this._orderInfo = oeService.GetAirOrderInfo(orderId);
        }

        void ShowOrder()
        {
            InitData(_orderInfo);

            this.bsOrderInfo.DataSource = _orderInfo;
            bsOrderInfo.ResetBindings(false);
            this._orderInfo.CancelEdit();

            InitControls();

            //List<AirBookingPOList> pos = null;

            List<AirBookingFeeList> feelist = null;

            if (_orderInfo.ID == Guid.Empty)
            {
                //pos = new List<AirBookingPOList>();
                feelist = new List<AirBookingFeeList>();
            }
            else
            {
                //pos = oeService.GetAirOrderPOList(_orderInfo.ID);
                feelist = oeService.GetAirOrderFeeList(_orderInfo.ID);
            }

            if (listInfo != null)
            {
                //请空主键ID，则表示新增的订单
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
        }


        void BindingData(object data)
        {
            this.SuspendLayout();
            this.orderFeeEditPart1.SetService(Workitem);
            listInfo = data as AirOrderList;

            if (listInfo == null)
            {
                this._orderInfo = new AirOrderInfo();
                this.ReadyForNew();
            }
            else
            {
                this.GetData(listInfo.ID);
                if (listInfo.EditMode == EditMode.Edit)
                {
                }
                else if (listInfo.EditMode == EditMode.Copy)
                {
                    this.PrepareForCopyExistOrder();
                }
            }

            this._orderInfo.CancelEdit();

            this.ShowOrder();

            SearchRegister();

            this.SetLazyLoaders();

            this.SetLazyDataLodersWithDynamicCondition();

            this.ResumeLayout();
            AirOrderInfo currentData = bsOrderInfo.DataSource as AirOrderInfo;
            cmbPaymentTerm.ShowSelectedValue(currentData.PaymentTermID, currentData.PaymentTermName);
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
            cmbCargoType.ClosePopup();
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
                            e.SetErrorInfo("PODID", LocalData.IsEnglish ? "POD can't Same as POL." : "起运港不能和目的港相同.");
                        }

                        if (_orderInfo.EstimatedDeliveryDate != null && _orderInfo.ExpectedShipDate != null)
                        {
                            if (_orderInfo.ExpectedShipDate.Value.Date < _orderInfo.EstimatedDeliveryDate.Value.Date)
                            {
                                e.SetErrorInfo("ExpectedArriveDate",
                                    LocalData.IsEnglish ? "ExpectedArriveDate can't less than ExpectedShipDate" : "期望出运日不能小于估计交货日.");
                            }
                        }


                        if (_orderInfo.ExpectedShipDate != null && _orderInfo.ExpectedArriveDate != null)
                        {
                            if (_orderInfo.ExpectedArriveDate.Value.Date < _orderInfo.ExpectedShipDate.Value.Date)
                            {
                                e.SetErrorInfo("ExpectedArriveDate",
                                    LocalData.IsEnglish ? "ExpectedArriveDate can't less than ExpectedShipDate" : "期望到达日不能小于期望出运日.");
                            }
                        }

                    }
                );

            #region childParts
            if (orderFeeEditPart1.ValidateData() == false)
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
                xtraTabControl1.SelectedTabPageIndex = 0;
            //else if (isScrrs[1] == false)
            //    xtraTabControl1.SelectedTabPageIndex = 1;


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
        private bool Save(AirOrderInfo currentData, bool isSavingAs)
        {
            if (ValidateData() == false || !orderFeeEditPart1.Validate())
            {
                return false;
            }

            if (!currentData.IsDirty
                && !currentData.IsNew
                && !this.orderFeeEditPart1.IsChanged)
            {
                return true;
            }

            try
            {

                OrderSaveRequest originalOrder = null;

                bool IsSendMail = false;
                Guid id = currentData.ID;
                if (currentData.ID == Guid.Empty || currentData.IsDirty)
                {
                    originalOrder = SaveOrder(currentData);
                }

                if (originalOrder != null && Utility.GuidIsNullOrEmpty(id))
                {
                    IsSendMail = true;
                }

                List<FeeSaveRequest> originalFees = this.orderFeeEditPart1.SaveFee(currentData.ID);

                Dictionary<Guid, SaveResponse> saved = this.oeService.SaveAirOrderWithTrans(originalOrder,
                    originalFees, null);

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

                if (isSavingAs)
                {
                }
                else
                {
                    AfterSave();
                }

                //发送邮件
                if (IsSendMail)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Sending email..." : "正在发送邮件...");

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
            if (Utility.GuidIsNullOrEmpty(_orderInfo.BookingerID))
            {
                return;
            }
            this.barSave.Enabled = false;
            try
            {
                //邮件标题
                string title = _orderInfo.RefNo;
                //邮件内容
                string content = string.Format(NativeLanguageService.GetText(this, "OrderSendEMailStyle"), DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).ToString(), _orderInfo.RefNo);

                content = "Hi " + _orderInfo.BookingerName + ":" + System.Environment.NewLine + content;

                content = content + System.Environment.NewLine + System.Environment.NewLine + LocalData.UserInfo.UserName + System.Environment.NewLine + DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).ToShortDateString();
                //转换为文件流
                byte[] byteContent = string.IsNullOrEmpty(content) ? null : System.Text.UnicodeEncoding.GetEncoding("GB2312").GetBytes(content);
                //邮件接收人
                List<Guid> lstToUsers = new List<Guid>();
                lstToUsers.Add(_orderInfo.BookingerID.Value);
                //CC
                List<Guid> lstCC = new List<Guid>();
                lstCC.Add(_orderInfo.SalesID.Value);
                //模板路径
                string fileName = System.Windows.Forms.Application.StartupPath + "\\Reports\\AirExport\\";
                //if (LocalData.IsEnglish) fileName = Path.Combine(fileName, "AI_OrderInfo_EN.frx");
                //else fileName = Path.Combine(fileName, "AI_OrderInfo_CN.frx");
                ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(_orderInfo.CompanyID);
                if (configureInfo.SolutionID == new Guid("b6e4dded-4359-456a-b835-e8401c910fd0"))
                {
                    //越南，泰国，马来西亚，用英文模板
                    if (_orderInfo.CompanyID == new Guid("5a827adf-38c7-4a2f-99a7-ad717ce91718") ||
                        _orderInfo.CompanyID == new Guid("13C26E30-F2AD-4D94-B13D-5E337EA97936") ||
                        _orderInfo.CompanyID == new Guid("1DBF7671-0D2D-4F08-A8A9-3663A0DB0037"))
                    {
                        fileName = Path.Combine(fileName, "OE_OrderInfo_EN.frx");
                    }
                    else
                    {
                        //国内，用中文模板
                        fileName = Path.Combine(fileName, "OE_OrderInfo_CN.frx");
                    }
                }
                else
                {
                    //北美和加拿大解决方案，用英文模板
                    fileName = Path.Combine(fileName, "OE_OrderInfo_EN.frx");
                }

                //数据源
                // OIOrderReportData data = OIReportSrvice.GetAIOrderReportData(_orderInfo.ID);
                // if (data == null) return;
                //Dictionary<string, object> reportSource = new Dictionary<string, object>();
                //reportSource.Add("ReportSource", data);
                //reportSource.Add("OrderFee", data.Fees);

                ICP.Message.ServiceInterface.Message message = CreateMessageInfo(title,content);
 
                ClientMessageService.SendAndSaveLog(message);

                ////只发送邮件
                //emailService.SendEMailByAttachment(
                //    LocalData.UserInfo.LoginID,
                //    lstToUsers.ToArray(),
                //    lstCC.ToArray(),
                //    title,
                //    false,
                //    null,
                //    byteContent,
                //    ICP.OA.ServiceInterface.DataObjects.MailPriority.Normal,
                //    null,
                //    null);
                //生成pdf附件，后发送邮件
                // ReportService.SendReport(lstToUsers.ToArray(), null, title, content, fileName, reportSource);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Sent Successfully..." : "发送成功...");
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

        ICP.Message.ServiceInterface.Message CreateMessageInfo(string title, string content)
        {
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.Subject = title;
            message.Body = content;
            message.BodyFormat = BodyFormat.olFormatHTML;

            message.SendFrom = LocalData.UserInfo.EmailAddress;
            UserInfo toUserInfo = userService.GetUserInfo(_orderInfo.BookingerID.Value);
            if (toUserInfo != null && !string.IsNullOrEmpty(toUserInfo.EMail))
            {
                message.SendTo = toUserInfo.EMail;
            }

            UserInfo ccUserInfo = userService.GetUserInfo(_orderInfo.SalesID.Value);
            if (ccUserInfo != null && !string.IsNullOrEmpty(ccUserInfo.EMail))
            {
                message.CC = ccUserInfo.EMail;
            }

            MessageUserPropertiesObject userPropertiesObject = message.UserProperties;
            userPropertiesObject.OperationId = _orderInfo.ID;
            userPropertiesObject.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.AirExport;
            userPropertiesObject.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            userPropertiesObject.FormId = _orderInfo.ID;

            return message;
        }


        private void AfterSave()
        {
            //if (!orderFeeEditPart1.Validate() == false) { return; }
            RefreshBarEnabled();
            this._orderInfo.BeginEdit();

            this.stxtCustomer.Properties.Buttons[2].Visible = false;
            this.gvOrders.DoubleClick -= new System.EventHandler(this.gvOrders_DoubleClick);
            this.stxtCustomer.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.stxtCustomer_ButtonClick);
            barSaveAs.Enabled = true;

            if (this._orderInfo.State == AEOrderState.Rejected)
            {
                string title = LocalData.IsEnglish ? "Please confirma" : "请确认";
                string message = LocalData.IsEnglish ? "Do you want to submit to operators?" : "将这张单提交给操作吗？";
                DialogResult dr = XtraMessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        SingleResult result = this.oeService.ChangeAirOrderStateWithTargetState(this._orderInfo.ID, AEOrderState.NewOrder, "Changed state by user with prompt confirmation.", LocalData.UserInfo.LoginID, this._orderInfo.UpdateDate);

                        this._orderInfo.State = (AEOrderState)result.GetValue<byte>("State");
                        this._orderInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                        this.SetState();
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully." : "保存成功");
                    }
                    catch
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully.But failed to change order state from Rejected to NewOrder." : "保存成功,但是试图修改订单状态时失败.");
                    }
                }
            }
            else
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully." : "保存成功");
            }

            if (Saved != null)
            {
                this._orderInfo.BookingerName = this._orderInfo.BookingerID.ToGuid() == Guid.Empty ?
                    string.Empty : this.mcmbBookinger.Text;
                this._orderInfo.OverSeasFilerName = this._orderInfo.OverSeasFilerId.ToGuid() == Guid.Empty ?
                    string.Empty : this.mcmOverseasFiler.Text;
                //this._orderInfo.BookingTypeDescription = EnumHelper.GetDescription<OEOperationType>(this._orderInfo.OEOperationType, LocalData.IsEnglish);

                Saved(new object[] { _orderInfo });

                this._orderInfo.IsDirty = false;
            }

            this._orderInfo.IsDirty = false;

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
        private OrderSaveRequest SaveOrder(AirOrderInfo currentData)
        {
            this.EndEdit();
            if (currentData.IsDirty == true || currentData.IsNew)
            {
                OrderSaveRequest saveRequest = new OrderSaveRequest();

                saveRequest.id = currentData.ID;
                saveRequest.refNo = currentData.RefNo;
                saveRequest.companyID = currentData.CompanyID;
                //saveRequest.oeOperationType = currentData.OEOperationType;
                saveRequest.customerID = currentData.CustomerID;
                saveRequest.tradeTermID = currentData.TradeTermID;
                saveRequest.salesTypeID = currentData.SalesTypeID;
                saveRequest.salesID = currentData.SalesID;
                saveRequest.salesDepartmentID = currentData.SalesDepartmentID;
                saveRequest.transportClauseID = currentData.TransportClauseID;
                saveRequest.paymentTermID = currentData.PaymentTermID;
                //saveRequest.overSeasFilerId = currentData.OverSeasFilerId;//客服
                saveRequest.bookingerID = currentData.BookingerID;
                saveRequest.bookingMode = currentData.BookingMode;
                saveRequest.bookingDate = currentData.BookingDate;
                saveRequest.bookingCustomerID = currentData.BookingCustomerID;
                saveRequest.bookingCustomerDescription = currentData.BookingCustomerDescription;
                saveRequest.shipperID = currentData.ShipperID;
                saveRequest.shipperDescription = currentData.ShipperDescription;
                saveRequest.consigneeID = currentData.ConsigneeID;
                saveRequest.consigneeDescription = currentData.ConsigneeDescription;
                //saveRequest.placeOfReceiptID = currentData.PlaceOfReceiptID;
                saveRequest.polID = currentData.POLID;
                saveRequest.podID = currentData.PODID;
                saveRequest.finalDestinationID = currentData.PlaceOfReceiptID;
                saveRequest.commodity = currentData.Commodity;
                saveRequest.quantity = currentData.Quantity;
                saveRequest.quantityUnitID = currentData.QuantityUnitID;
                saveRequest.weight = currentData.Weight;
                saveRequest.weightUnitID = currentData.WeightUnitID;
                saveRequest.measurement = currentData.Measurement;
                saveRequest.measurementUnitID = currentData.MeasurementUnitID;
                saveRequest.cargoDescription = currentData.CargoDescription;
                //saveRequest.containerDescription = currentData.ContainerDescription;
                saveRequest.carrierID = currentData.CarrierID;
                ////saveRequest.State = (OrderState)currentData.State;
                saveRequest.estimatedDeliveryDate = currentData.EstimatedDeliveryDate;
                saveRequest.expectedShipDate = currentData.ExpectedShipDate;
                saveRequest.expectedArriveDate = currentData.ExpectedArriveDate;
                saveRequest.isTruck = currentData.IsTruck;
                saveRequest.isCustoms = currentData.IsCustoms;
                saveRequest.isCommodityInspection = currentData.IsCommodityInspection;
                saveRequest.isQuarantineInspection = currentData.IsQuarantineInspection;
                saveRequest.isWarehouse = currentData.IsWarehouse;
                saveRequest.isOnlyMBL = currentData.IsOnlyMBL;
                ////saveRequest.isValid = currentData.IsValid;
                //saveRequest.mblpaymentTermID = currentData.MBLPaymentTermID;
                //saveRequest.hblpaymentTermID = currentData.HBLPaymentTermID;
                //saveRequest.mblReleaseType = currentData.MBLReleaseType;
                //saveRequest.hblReleaseType = currentData.HBLReleaseType;
                //saveRequest.mblRequirements = currentData.MBLRequirements;
                //saveRequest.hblRequirements = currentData.HBLRequirements;
                saveRequest.remark = currentData.Remark;
                saveRequest.saveByID = LocalData.UserInfo.LoginID;
                saveRequest.updateDate = currentData.UpdateDate;


                //SingleResult result = oeService.SaveAirOrderInfo(saveRequest);
                saveRequest.AddInvolvedObject(currentData);
                return saveRequest;
            }
            else { return null; }
        }

        void RefreshUI(OrderSaveRequest saveRequest)
        {
            SingleResult result = saveRequest.SingleResult;
            AirOrderInfo currentData = saveRequest.UnBoxInvolvedObject<AirOrderInfo>()[0];


            currentData.ID = result.GetValue<Guid>("ID");
            currentData.RefNo = result.GetValue<string>("NO");
            currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            currentData.State = (AEOrderState)result.GetValue<byte>("State");


            currentData.IsDirty = false;

            if (currentData.BookingCustomerDescription != null)
            {
                currentData.BookingCustomerDescription.IsDirty = false;
            }
            if (currentData.ShipperDescription != null)
            {
                currentData.ShipperDescription.IsDirty = false;
            }
            if (currentData.ConsigneeDescription != null)
            {
                currentData.ConsigneeDescription.IsDirty = false;
            }
            if (currentData.AgentDescription != null)
            {
                currentData.AgentDescription.IsDirty = false;
            }



            //不工作，而且以前状态丢失，可能会有问题
            //this.Workitem.ID = OEOrderCommandConstants.Command_EditData + currentData.ID.ToString();
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
                    //Utility.ShowMessage(LocalData.IsEnglish ? "Save as a new order successfully. Ref. NO. is " + this._orderInfo.RefNo + "." : "已成功另存为一票新订单，业务号为" + this._orderInfo.RefNo + "。");
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

            AirOrderInfo orderInfo = Utility.Clone<AirOrderInfo>(_orderInfo);
            orderInfo.ID = Guid.Empty;
            orderInfo.RefNo = string.Empty;
            orderInfo.State = AEOrderState.NewOrder;
            orderInfo.CreateByID = LocalData.UserInfo.LoginID;
            orderInfo.CreateByName = LocalData.UserInfo.LoginName;
            orderInfo.CreateDate = DateTime.Now;
            orderInfo.BookingDate = DateTime.Now;
            orderInfo.UpdateDate = null;
            orderInfo.SODate = null;


            orderInfo.IsDirty = true;
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

        #region 打印

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Print();
            }
        }

        private void Print()
        {
            if (_orderInfo.ID == Guid.Empty || _orderInfo.IsDirty)
            {
                if (SaveData() == false)
                {
                    return;
                }
            }

            this.AirExportPrintHelper.PrintAEOrder(_orderInfo.ID);
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
                //DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Sure Refresh Data?" : "是否刷新数据?",
                //                                LocalData.IsEnglish ? "Tip" : "提示",
                //                                MessageBoxButtons.YesNo,
                //                                MessageBoxIcon.Question);
                //if (dialogResult == DialogResult.Yes)
                //{
                try
                {
                    this.RefreshData(this._orderInfo.ID);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Refersh successfully." : "刷新成功.");
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Refersh failed." + ex.Message : "刷新失败." + ex.Message);
                }
                //}
            }
        }

        #endregion

        #region 关闭

        void OrderBaseEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (this._orderInfo.IsDirty &&
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
                    if (!this.Save(this._orderInfo, false))
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
            this.DisableControl();
        }

        /// <summary>
        /// 禁用控件
        /// </summary>
        private void DisableControl()
        {
            //如果状态为已订舱，禁用面板
            if (this._orderInfo.State == AEOrderState.BookingConfirmed)
            {
                navBarGroupControlContainer1.Enabled = false;
                navBarGroupControlContainer2.Enabled = false;
            }
        }

        bool _shown
        {
            get
            {
                return this._orderInfo.IsDirty;
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

            Utility.SetCustomerTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtCustomer,
                stxtBookingCustomer,
                //mcmbCarrier.popEdit1,
            });

            Utility.SetPortTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtPlaceOfDelivery,
                stxtPOD,
                stxtPOL,
            });
            this.mcmbSales.SpecifiedBackColor = this.txtNo.BackColor;
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
            }
            if (ICP.Framework.CommonLibrary.Client.LocalCommonServices.PermissionService.HaveActionPermission(FCMPermissionsConstants.FCM_NAServices)
                && _orderInfo != null && _orderInfo.CreateByID != LocalData.UserInfo.LoginID)
            {
                this.barSave.Enabled = false;
            }



        }
        #endregion


    }
}
