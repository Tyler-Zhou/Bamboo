using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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

namespace ICP.FCM.AirExport.UI.Booking 
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class BookingBaseEditPart : BaseEditPart
    {
        #region 服务注入

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonClientService fcmCommonClientService { get; set; }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        //[ServiceDependency]
        //public IAEReportDataService oeReportSrvice { get; set; }


        [ServiceDependency]
        public IDataFindClientService dfService { get; set; }


        [ServiceDependency]
        public ICP.Common.ServiceInterface.IConfigureService configureService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IGeographyService geographyService { get; set; }

        [ServiceDependency]
        public IAirExportService aeService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ICustomerService customerService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelperService { get; set; }

        [ServiceDependency]
        public ITransportFoundationService ConfigureService { get; set; }

        #endregion

        #region 本地变量

        AirBookingInfo _oceanBookingInfo = null;
        AirBookingInfo _CurrentData;
        /// <summary>
        /// 缓存国家列表,只获取一次.现只用于客户弹出式描述框
        /// </summary>
        List<CountryList> _countryList = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BookingBaseEditPart()
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

            this.Disposed += new EventHandler(BookingBaseEditPart_Disposed);
            this.Load += new EventHandler(BookingBaseEditPart_Load);
        }

        #region 设置中文字符

        private void SetCnText()
        {
            groupRemark.Text = "备注";
            labBookingCustomer.Text = "订舱客户";
            labBookingDate.Text = "委托日期";
            labBookingMode.Text = "委托方式";

            labContractNo.Text = "合约号";
            labCargoType.Text = "货物类型";
            labAirCompany.Text = "航空公司";
            labClosingDate.Text = "截关日";
            labCommodity.Text = "品名";
            labCompany.Text = "操作口岸";
            labConsignee.Text = "收货人";
            labCustomer.Text = "客户";
            labEstimatedDeliveryDate.Text = "估计交货";
            labExpectedArriveDate.Text = "期望到达";
            labExpectedShipDate.Text = "期望出运";
            labDeparture.Text = "起运港";
            labDetination.Text = "目的港";
            labPlaceOfDelivery.Text = "交货地";
            labFlightNo.Text = "航班号";
            labAirCompany.Text = "航空公司";
            labETD.Text = "起航日";
            labETA.Text = "到达日";
            labMeasurement.Text = "体积";
            labNo.Text = "业务号";
            labPaymentTerm.Text = "付款方式";

            labQuantity.Text = "数量";
            labSales.Text = "揽货人";
            labSalesDepartment.Text = "揽货部门";
            labSalesType.Text = "揽货类型";
            labShipper.Text = "发货人";
            labTradeTerm.Text = "贸易条款";
            labTransportClause.Text = "运输条款";

            labWeight.Text = "重量";
            labDeliveryDate.Text = "交货日";

            chkIsTruck.Text = "拖车";
            chkIsCustoms.Text = "报关";
            chkIsCommodityInspection.Text = "商检";
            chkIsQuarantineInspection.Text = "质检";
            chkIsWarehouse.Text = "仓储";
            chkIsOnlyMBL.Text = "只出MBL";
            chkHasContract.Text = "合约";

            labAgent.Text = "代理";
            labAgentOfCarrier.Text = "承运人";
            this.labBookinger.Text = "订舱";
            labFiler.Text = "文件";
            labState.Text = "状态";
            labSODate.Text = "确认日期";
            navBarBase.Caption = "基本信息";
            navBarDelegate.Caption = "委托信息";
            navBarOther.Caption = "其它信息";
            navBarFee.Caption = "费用信息";


            groupLocalService.Text = "本地服务";

            labShippingLine.Text = "航线";
            labDOCClosingDate.Text = "截文件日";
            tabPageBase.Text = "基础";

            barRefresh.Caption = "刷新(&R)";
            barSave.Caption = "保存(&S)";
            barSaveAs.Caption = "另存为(&A)";
            this.barAuditAndSave.Caption = "审核并保存";

            this.barSubPrint.Caption = "打印";
            this.barPrintOrder.Caption = "业务联单";
            barPrintBookingConfirm.Caption = "订舱确认书";
            this.barPrintInWarehouse.Caption = "进仓通知书";
            barClose.Caption = "关闭(&C)";

            barReject.Caption = "打回(&J)";
            this.barTruck.Caption = "派车";
            this.barApplyAgent.Caption = "申请代理";
            barE_Booking.Caption = "电子订舱(&E)";

            colClosingDate.Caption = "截关日";
            colConsigneeName.Caption = "收货人";
            colShipperName.Caption = "发货人";
            colNo.Caption = "业务号";
            colPOLName.Caption = "起运港";
            colPODName.Caption = "目的港";
        }

        #endregion

        #endregion

        #region 新订舱单的特殊逻辑

        /// <summary>
        /// 
        /// </summary>
        void ReadyForNew()
        {
            AirBookingInfo newData = new AirBookingInfo();
            //newData.SalesID = LocalData.UserInfo.LoginID;
            //newData.SalesName = LocalData.UserInfo.LoginName;
            newData.BookingDate = newData.CreateDate = DateTime.Now;
            newData.BookingMode = FCMBookingMode.Fax;
            newData.State = AEOrderState.NewOrder;
            newData.AgentID = Guid.Empty;
            newData.BookingerID = LocalData.UserInfo.LoginID;
            newData.BookingerName = LocalData.UserInfo.LoginName;
            //newData.CargoDescription = new ICP.FCM.Common.ServiceInterface.DataObjects.CargoDescription();
            newData.IsContract = true;
            newData.IsValid = true;

            #region 设置默认值
            DataDictionaryList normalDictionary = null;
            //normalDictionary = ICPCommUIHelperService.GetNormalDictionary(DataDictionaryType.PaymentTerm);
            //newData.PaymentTermID = normalDictionary.ID;
            //newData.PaymentTermName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

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

            this._oceanBookingInfo = newData;

            _oceanBookingInfo.HBLReleaseType = _oceanBookingInfo.MBLReleaseType = FCMReleaseType.Unknown;

            // TODO: 这种Guard型的逻辑要在最开始的时候完成
            Utility.EnsureDefaultCompanyExists(this.userService);

            this._oceanBookingInfo.CompanyID = LocalData.UserInfo.DefaultCompanyID;
            this._oceanBookingInfo.CompanyName = LocalData.UserInfo.DefaultCompanyName;

            this.gvOrders.DoubleClick += new System.EventHandler(this.gvOrders_DoubleClick);
        }

        #endregion

        #region 复制订舱单时的逻辑

        /// <summary>
        /// 复制订舱单时的逻辑
        /// </summary>
        void PrepareForCopyExistOrder()
        {
            this._oceanBookingInfo.ID = Guid.Empty;
            this._oceanBookingInfo.No = string.Empty;
            this._oceanBookingInfo.MBLNo = this._oceanBookingInfo.HBLNo = string.Empty;
            this._oceanBookingInfo.SalesID = LocalData.UserInfo.LoginID;
            this._oceanBookingInfo.SalesName = LocalData.UserInfo.LoginName;
            this._oceanBookingInfo.CreateDate = DateTime.Now;
            //this._oceanBookingInfo.AirShippingOrderID = Guid.Empty;
            //this._oceanBookingInfo.AirShippingOrderNo = string.Empty;
            this._oceanBookingInfo.AgentID = Guid.Empty;
            this._oceanBookingInfo.IsContract = false;
            this._oceanBookingInfo.BookingerID = LocalData.UserInfo.LoginID;
        }

        #endregion

        #region UI逻辑

        #region 初始化下拉式控件的ITEM以及其它一些控件的默认值

        #region 不能为空的一些属性和数据

        /// <summary>
        /// 有些实体性的属性如果为空，将引起界面异常
        /// 在这里统一对其初始化
        /// </summary>
        private void InitData()
        {
            InitCargoObject();

            if (_oceanBookingInfo.ShipperDescription == null)
            {
                _oceanBookingInfo.ShipperDescription = new CustomerDescription();
            }

            if (_oceanBookingInfo.ConsigneeDescription == null)
            {
                _oceanBookingInfo.ConsigneeDescription = new CustomerDescription();
            }

            if (_oceanBookingInfo.AgentDescription == null)
            {
                _oceanBookingInfo.AgentDescription = new CustomerDescription();
            }

            if (_oceanBookingInfo.BookingCustomerDescription == null)
            {
                _oceanBookingInfo.BookingCustomerDescription = new CustomerDescription();
            }

            if (_oceanBookingInfo.CargoDescription == null)
            {
                //_oceanBookingInfo.CargoDescription = new CargoDescription();
            }
        }

        #region 初始化货物对象

        /// <summary>
        /// 初始化货物对象
        /// </summary>
        private void InitCargoObject()
        {
            if (this._oceanBookingInfo.CargoType.HasValue
                && _oceanBookingInfo.CargoDescription != null
                && _oceanBookingInfo.CargoDescription.Cargo != null)
            {
                if (_oceanBookingInfo.CargoDescription.Cargo is DangerousCargo)
                    cmbCargoType.EditValue = CargoType.Dangerous;
                else if (_oceanBookingInfo.CargoDescription.Cargo is AwkwardCargo)
                    cmbCargoType.EditValue = CargoType.Awkward;
                else if (_oceanBookingInfo.CargoDescription.Cargo is ReeferCargo)
                    cmbCargoType.EditValue = CargoType.Reefer;
                else if (_oceanBookingInfo.CargoDescription.Cargo is DryCargo)
                    cmbCargoType.EditValue = CargoType.Dry;

            }
        }

        #endregion

        #endregion
     
        /// <summary>
        /// 根据订舱单信息显示下拉式控件及其它一些控件的值
        /// </summary>
        private void InitControls()
        {
            ////业务类型
            //this.cmbType.ShowSelectedValue(this._oceanBookingInfo.OEOperationType,
            //    EnumHelper.GetDescription<OEOperationType>(this._oceanBookingInfo.OEOperationType, LocalData.IsEnglish, true));
            //委托方式
            this.cmbBookingMode.ShowSelectedValue(this._oceanBookingInfo.BookingMode,
                EnumHelper.GetDescription<FCMBookingMode>(this._oceanBookingInfo.BookingMode, LocalData.IsEnglish));
            //操作口岸
            cmbCompany.ShowSelectedValue(this._oceanBookingInfo.CompanyID, this._oceanBookingInfo.CompanyName);
            //贸易条款
            cmbTradeTerm.ShowSelectedValue(this._oceanBookingInfo.TradeTermID, this._oceanBookingInfo.TradeTermName);
            //包装
            cmbQuantityUnit.ShowSelectedValue(this._oceanBookingInfo.QuantityUnitID, this._oceanBookingInfo.QuantityUnitName);
            if (Utility.GuidIsNullOrEmpty(this._oceanBookingInfo.QuantityUnitID)
                && this.cmbQuantityUnit.EditValue != null)
            {
                this._oceanBookingInfo.QuantityUnitID = (Guid)this.cmbQuantityUnit.EditValue;
            }
            //重量
            cmbWeightUnit.ShowSelectedValue(this._oceanBookingInfo.WeightUnitID, this._oceanBookingInfo.WeightUnitName);
            if (Utility.GuidIsNullOrEmpty(this._oceanBookingInfo.WeightUnitID)
                && this.cmbWeightUnit.EditValue != null)
            {
                this._oceanBookingInfo.WeightUnitID = (Guid)this.cmbWeightUnit.EditValue;
            }
            //体积
            cmbMeasurementUnit.ShowSelectedValue(this._oceanBookingInfo.MeasurementUnitID, this._oceanBookingInfo.MeasurementUnitName);
            if (Utility.GuidIsNullOrEmpty(this._oceanBookingInfo.MeasurementUnitID)
                && cmbMeasurementUnit.EditValue != null)
            {
                this._oceanBookingInfo.MeasurementUnitID = (Guid)this.cmbMeasurementUnit.EditValue;
            }
            this.mcmbSales.ShowSelectedValue(this._oceanBookingInfo.SalesID, this._oceanBookingInfo.SalesName);
            //揽货类型
            this.cmbSalesType.ShowSelectedValue(this._oceanBookingInfo.SalesTypeID, this._oceanBookingInfo.SalesTypeName);
            //揽货部门
            this.trsSalesDep.ShowSelectedValue(this._oceanBookingInfo.SalesDepartmentID, this._oceanBookingInfo.SalesDepartmentName);
            //3个付款方式
            cmbPaymentTerm.ShowSelectedValue(this._oceanBookingInfo.PaymentTermID, this._oceanBookingInfo.PaymentTermName);
            //航线
            cmbShippingLine.ShowSelectedValue(this._oceanBookingInfo.ShippingLineID, this._oceanBookingInfo.ShippingLineName);
            //航空公司
            mcmbAirCompany.ShowSelectedValue(this._oceanBookingInfo.AirCompanyId, this._oceanBookingInfo.AirCompanyName);
            //航班号
            cmbFlightNo.ShowSelectedValue(this._oceanBookingInfo.FilightId, this._oceanBookingInfo.FilightNo);
            //运输条款
            this.cmbTransportClause.ShowSelectedValue(this._oceanBookingInfo.TransportClauseID, this._oceanBookingInfo.TransportClauseName);

            //货物描述
            if (_oceanBookingInfo.CargoDescription != null
                && _oceanBookingInfo.CargoDescription.Cargo != null)
            {
                txtCargoDescription.Text = _oceanBookingInfo.CargoDescription.Cargo.ToString(LocalData.IsEnglish);
            }

            this.orderFeeEditPart1.SetCompanyID(this._oceanBookingInfo.CompanyID);

            this.mcmbFiler.ShowSelectedValue(this._oceanBookingInfo.FilerId, this._oceanBookingInfo.FilerName);

            this.mcmbBookinger.ShowSelectedValue(this._oceanBookingInfo.BookingerID, this._oceanBookingInfo.BookingerName);

            if (this._oceanBookingInfo.CargoType.HasValue)
            {
                this.cmbCargoType.ShowSelectedValue(this._oceanBookingInfo.CargoType,
                    EnumHelper.GetDescription<CargoType>(this._oceanBookingInfo.CargoType.Value, LocalData.IsEnglish));
            }
            InitalComboxes();
            //this.stxtReturnLocation.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
        }


        #endregion

        #region 搜索器注册

        CustomerFinderBridge shipperBridge;

        CustomerFinderBridge consigneeBridge;

        CustomerFinderBridge bookingCustomerPartyBridge;
        LocationFinderBridge pfbPlaceOfReceipt;

        /// <summary>
        /// 搜索器注册
        /// </summary>
        void SearchRegister()
        {
            #region Customer

            //Customer
            dfService.Register(stxtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.CustomerResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          Guid oldCustomerId = _oceanBookingInfo.CustomerID;
                          stxtCustomer.ClosePopup();

                          CustomerStateType state = (CustomerStateType)resultData[7];
                          if (state == CustomerStateType.Invalid)
                          {
                              if (PartLoader.PopCustomerIsInvalid() != DialogResult.Yes)
                              {
                                  return;
                              }
                          }

                          //CustomerCodeApplyState? approved = (CustomerCodeApplyState?)resultData[6];
                          //if (!approved.HasValue
                          //    || (approved.HasValue && approved.Value != CustomerCodeApplyState.Passed))
                          //{
                          //    if (PartLoader.PopCustomerUnApproved() != DialogResult.Yes)
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

                          stxtCustomer.EditValue = _oceanBookingInfo.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                          stxtCustomer.Tag = _oceanBookingInfo.CustomerID = new Guid(resultData[0].ToString());

                          if (oldCustomerId != Guid.Empty && _oceanBookingInfo.CustomerID == oldCustomerId) return;

                          CustomerType customerType = (CustomerType)resultData[4];

                          CustomerChanged(customerType);


                      }, delegate
                      {
                          stxtCustomer.Text = _oceanBookingInfo.CustomerName = string.Empty;
                          stxtCustomer.Tag = _oceanBookingInfo.CustomerID = Guid.Empty;
                          stxtCustomer.ClosePopup();
                          CustomerChanged(null);
                      },
                      ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            dfService.Register(stxtAgentOfCarrier, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                GetConditionsForAgentOfCarrier,
                delegate(object inputSource, object[] resultData)
               {
                   stxtAgentOfCarrier.Text = _oceanBookingInfo.AgentOfCarrierName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                   stxtAgentOfCarrier.Tag = _oceanBookingInfo.AgentOfCarrierID = new Guid(resultData[0].ToString());
               },
               delegate()
               {
                   this.stxtAgentOfCarrier.Text = string.Empty;
                   this.stxtAgentOfCarrier.Tag = Guid.Empty;
               }, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            #endregion

            #region 订舱客户
            dfService.Register(stxtBookingCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.CustomerResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          Guid oldBookingCustomerID = _oceanBookingInfo.BookingCustomerID == null? Guid.Empty: _oceanBookingInfo.BookingCustomerID.Value;
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

                          stxtBookingCustomer.Tag = _oceanBookingInfo.BookingCustomerID = new Guid(resultData[0].ToString());
                          stxtBookingCustomer.Text = _oceanBookingInfo.BookingCustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();

                      }, delegate
                      {
                          stxtBookingCustomer.Tag = _oceanBookingInfo.BookingCustomerID = Guid.Empty;
                          stxtBookingCustomer.Text = _oceanBookingInfo.BookingCustomerName = string.Empty;
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
               _oceanBookingInfo.ShipperDescription,
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
                _oceanBookingInfo.ConsigneeDescription,
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
            //    _oceanBookingInfo.BookingCustomerDescription,
            //    ICPCommUIHelperService,
            //    LocalData.IsEnglish);
            //    bookingCustomerPartyBridge.Init();
            //});

            //stxtBookingCustomer.OnOk += new EventHandler(stxtBookingCustomer_OnOk);

            #endregion

            #region Port
            //PortFinderBridge pfbPOL = new PortFinderBridge(this.stxtDeparture, this.dfService, LocalData.IsEnglish);

            //PortFinderBridge pfbPOD = new PortFinderBridge(this.stxtDetination, this.dfService, LocalData.IsEnglish);

            //LocationFinderBridge pfbPlaceOfDelivery = new LocationFinderBridge(this.stxtPlaceOfDelivery, this.dfService, LocalData.IsEnglish);

            #region POL

            dfService.Register(stxtDeparture, CommonFinderConstants.AirLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid portID = new Guid(resultData[0].ToString());
                      if (_oceanBookingInfo.POLID != portID)
                      {
                          stxtDeparture.Tag = _oceanBookingInfo.POLID = portID;
                          stxtDeparture.Text = _oceanBookingInfo.DepartureName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                      }
                  },
                  delegate
                  {
                      stxtDeparture.Tag = _oceanBookingInfo.POLID = Guid.Empty;
                      stxtDeparture.Text = _oceanBookingInfo.DepartureName = string.Empty;
                  },
                  ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion
            #region POD
            dfService.Register(stxtDetination, CommonFinderConstants.AirLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid portID = new Guid(resultData[0].ToString());
                      if (_oceanBookingInfo.PODID != portID)
                      {
                          stxtDetination.Tag = _oceanBookingInfo.PODID = portID;
                          stxtDetination.Text = _oceanBookingInfo.DetinationName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                      }
                  },
                  delegate
                  {
                      stxtDetination.Tag = _oceanBookingInfo.PODID = Guid.Empty;
                      stxtDetination.Text = _oceanBookingInfo.DetinationName = string.Empty;
                  },
                  ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion
            #region PlaceOfDelivery
            dfService.Register(stxtPlaceOfDelivery, CommonFinderConstants.AirLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      stxtPlaceOfDelivery.Tag = _oceanBookingInfo.PlaceOfDeliveryID = new Guid(resultData[0].ToString());
                      stxtPlaceOfDelivery.Text = _oceanBookingInfo.PlaceOfDeliveryName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                  },
                  delegate
                  {
                      stxtPlaceOfDelivery.Tag = _oceanBookingInfo.PlaceOfDeliveryID = Guid.Empty;
                      stxtPlaceOfDelivery.Text = _oceanBookingInfo.PlaceOfDeliveryName = string.Empty;
                  },
                  ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion
            #endregion
        }

        /// <summary>
        /// “承运人”是类型为“货代”或“航空公司”的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForAgentOfCarrier()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Airline, false);
            conditions.AddWithValue("CustomerType", CustomerType.Forwarding, false);
           
            return conditions;
        }

        void stxtBookingCustomer_OnOk(object sender, EventArgs e)
        {
            if (_oceanBookingInfo != null && stxtBookingCustomer.CustomerDescription != null)
            {
                _oceanBookingInfo.BookingCustomerDescription = stxtBookingCustomer.CustomerDescription;
            }
        }

        void stxtConsignee_OnOk(object sender, EventArgs e)
        {
            if (_oceanBookingInfo != null && stxtConsignee.CustomerDescription != null)
            {
                _oceanBookingInfo.ConsigneeDescription = stxtConsignee.CustomerDescription;
            }
        }

        void stxtShipper_OnOk(object sender, EventArgs e)
        {
            if (_oceanBookingInfo != null && stxtShipper.CustomerDescription != null)
            {
                _oceanBookingInfo.ShipperDescription = stxtShipper.CustomerDescription;
            }
        }

        void pfbPOD_Cleared(object sender, EventArgs e)
        {

        }

        void pfbPlaceOfReceipt_Cleard(object sender, EventArgs e)
        {
            this.ClearPreVoyage();
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

        void ResetDescription()
        {
            if (this.shipperBridge != null)
            {
                this.shipperBridge.SetCustomerDescription(this._oceanBookingInfo.ShipperDescription);
            }

            if (this.consigneeBridge != null)
            {
                this.consigneeBridge.SetCustomerDescription(this._oceanBookingInfo.ConsigneeDescription);
            }

            if (this.bookingCustomerPartyBridge != null)
            {
                this.bookingCustomerPartyBridge.SetCustomerDescription(this._oceanBookingInfo.BookingCustomerDescription);
            }
        }

        #endregion

        #region 延迟加载的数据源

        List<DataDictionaryList> _weightUnits;

        void InitalComboxes()
        {
            ICPCommUIHelperService.SetCmbDataDictionary(cmbQuantityUnit, DataDictionaryType.QuantityUnit);

            //重量
            _weightUnits = ICPCommUIHelperService.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit);

            List<DataDictionaryList> volUnitss = ICPCommUIHelperService.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit);
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
                ICPCommUIHelperService.BindCompanyByUser(this.cmbCompany, true);

                if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.CompanyID) && LocalData.UserInfo.UserOrganizationList.Count > 0)
                {
                    _oceanBookingInfo.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                }

                cmbCompany.SelectedIndexChanged += delegate
                {
                    CompanyChanged();
                };
            });

            #region Agent
            if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.AgentID) == false)
            {
                List<CustomerList> agentCustomers = new List<CustomerList>();
                CustomerList agentCustomer = new CustomerList();
                agentCustomer.CName = agentCustomer.EName = _oceanBookingInfo.AgentName;
                agentCustomer.ID = _oceanBookingInfo.AgentID.Value;
                agentCustomers.Insert(0, agentCustomer);
                SetAgentSource(agentCustomers);
            }
            Utility.SetEnterToExecuteOnec(stxtAgent, delegate
            {
                SetAgentSourceByCompanyID(_oceanBookingInfo.CompanyID);
                stxtAgent.EditValueChanged += delegate
                {
                    if (stxtAgent.EditValue != null && stxtAgent.EditValue.ToString().Length > 0)
                    {
                        Guid id = new Guid(stxtAgent.EditValue.ToString());

                        ICPCommUIHelperService.SetCustomerDesByID(id, _oceanBookingInfo.AgentDescription);
                        stxtAgent.CustomerDescription = _oceanBookingInfo.AgentDescription;
                        stxtAgent.EditValueChanged -= new EventHandler(stxtAgent_EditValueChanged);
                        stxtAgent.EditValueChanged += new EventHandler(stxtAgent_EditValueChanged);
                        stxtAgent.OnOk += new EventHandler(stxtAgent_OnOk);
                    }
                };
            });
            #endregion

            //贸易条款
            Utility.SetEnterToExecuteOnec(cmbTradeTerm, delegate
            {
                ICPCommUIHelperService.SetCmbDataDictionary(cmbTradeTerm, DataDictionaryType.TradeTerm);
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
                                                    DataDictionaryType.PaymentTerm);
            });
            //航线
            Utility.SetEnterToExecuteOnec(cmbShippingLine, delegate
            {
                List<ShippingLineList> shippingLines = ICPCommUIHelperService.SetCmbShippingLine(cmbShippingLine);
            });

            //船公司
            Utility.SetEnterToExecuteOnec(mcmbAirCompany, delegate
            {
                ICPCommUIHelperService.BindCustomerList(mcmbAirCompany, CustomerType.Airline);
            });

            //运输条款
            Utility.SetEnterToExecuteOnec(cmbTransportClause, delegate
            {
                List<TransportClauseList> transportClauseList = ICPCommUIHelperService.SetCmbTransportClause(cmbTransportClause);
            });
            Utility.SetEnterToExecuteOnec(this.mcmbSales, delegate
            {
                ICPCommUIHelperService.SetMcmbUsersByCommand(mcmbSales, CommandConstants.FCM_AE_ORDERLIST, true, true);
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

            Utility.SetEnterToExecuteOnec(cmbFlightNo, delegate
            {
                List<FlightList> flightList = ConfigureService.GetFlightList(null, string.Empty, true, 0);
                if (flightList != null && flightList.Count > 0)
                {
                    Dictionary<string, string> col = new Dictionary<string, string>();
                    col.Add("No", "航班号");
                    col.Add("AirlineName", "航空公司");
                    cmbFlightNo.InitSource<FlightList>(flightList, col, "No", "ID");
                }
            });
           
            mcmbFiler.Enter += new EventHandler(mcmFiler_Click);
            this.mcmbBookinger.Enter += new EventHandler(mcmbBookinger_Click);
        }

        void stxtAgent_OnOk(object sender, EventArgs e)
        {
            if (_oceanBookingInfo != null && stxtAgent.CustomerDescription != null)
            {
                _oceanBookingInfo.AgentDescription = stxtAgent.CustomerDescription;
            }
        }

        /// <summary>
        /// 填充“订舱”的用户列表供选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mcmbBookinger_Click(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = (Guid)cmbCompany.EditValue;
            }

            ICPCommUIHelperService.SetComboxUsersByRole(mcmbBookinger, depID, "订舱", true);
        }

        /// <summary>
        /// 填充“文件”的用户列表供选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mcmFiler_Click(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = (Guid)cmbCompany.EditValue;
            }

            ICPCommUIHelperService.SetComboxUsersByRole(mcmbFiler, depID, "文件", true);


        }

        #endregion

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
            //this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControl1_SelectedPageChanged);
            //订舱客户,如果贸易条款为CIF，那么就为客户，否则为空白
            cmbTradeTerm.SelectedIndexChanged += new EventHandler(cmbTradeTerm_SelectedIndexChanged);

            cmbTransportClause.SelectedIndexChanged += delegate
            {
                if (this._shown)
                {
                    this._oceanBookingInfo.TransportClauseName = this.cmbTransportClause.Text;
                    SetFinalDestinationByTransportClause();
                }
            };

            if (_oceanBookingInfo.ID == Guid.Empty)
            {

                this.stxtCustomer.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.stxtCustomer_ButtonClick);
            }

            this.cmbFlightNo.SelectedRow += new EventHandler(cmbFlightNo_SelectedRow);
            this.cmbCargoType.Click += new EventHandler(cmbCargoType_Enter);
            this.cmbCargoType.SelectedIndexChanged += new EventHandler(cmbCargoType_EditValueChanged);
            this.mcmbSales.SelectedRow += new EventHandler(mcmbSales_SelectedRow);

            this.stxtDeparture.TextChanged += new EventHandler(stxtPOL_TextChanged);
            this.stxtDetination.TextChanged += new EventHandler(stxtPOD_TextChanged);
            this.trsSalesDep.Enter += new EventHandler(trsSalesDep_Enter);
            this.trsSalesDep.Selected += new EventHandler(trsSalesDep_Selected);
            this.stxtBookingCustomer.TextChanged += new EventHandler(stxtBookingCustomer_TextChanged);
        }


        void trsSalesDep_Selected(object sender, EventArgs e)
        {
        }


        void pfbPlaceOfReceipt_ValueChanged(object sender, EventArgs e)
        {
            if (this._shown)
            {
                this.ClearPreVoyage();
            }
        }

        void stxtPOL_TextChanged(object sender, EventArgs e)
        {
            if (this._shown)
            {
                //this._oceanBookingInfo.POLName = this.stxtDeparture.Text;
                this.ClearPreVoyage();
            }
        }

        void stxtPOD_TextChanged(object sender, EventArgs e)
        {
            if (this._shown && !Utility.GuidIsNullOrEmpty(this._oceanBookingInfo.PODID))
            {
                //this._oceanBookingInfo.PODName = this.stxtDetination.Text;
                //this.SetPlaceOfDeliveryByTransportClause();
                //this.ClearVoyage();
            }
        }

        /// <summary>
        /// 为了确保驳船的港口是符合要求的
        /// </summary>
        void ClearPreVoyage()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        void ClearFourDays()
        {
            dteClosingDate.EditValue = _oceanBookingInfo.ClosingDate = null;
            dteDOCClosingDate.EditValue = _oceanBookingInfo.DOCClosingDate = null;
            //dteCYClosingDate.EditValue = _oceanBookingInfo.CYClosingDate = null;
            dteETD.EditValue = _oceanBookingInfo.ETD = null;
        }


        /// <summary>
        /// 注册界面控件之间联动的事件并立即执行一次
        /// </summary>
        void RegisterRelativeEventsAndRunOnce()
        {
            this.chkIsOnlyMBL.CheckedChanged += new System.EventHandler(this.chkIsOnlyMBL_CheckedChanged);
            this.chkHasContract.CheckedChanged += new System.EventHandler(this.chkHasContract_CheckedChanged);
            this.txtContractNo.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtContractNo_ButtonClick);
            //this.cmbSalesType.SelectedIndexChanged += new EventHandler(cmbSalesType_SelectedIndexChanged);

            this.RunAtOnce();
        }

        void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!this.chkIsTruck.Enabled)
            {
                this.chkIsTruck.Checked = this._oceanBookingInfo.IsTruck = false;
            }
        }

        void stxtBookingCustomer_TextChanged(object sender, EventArgs e)
        {
            if (this._shown)
            {
                this._oceanBookingInfo.BookingCustomerName = this.stxtBookingCustomer.Text;
                this.SetShipperByBookingCustomerAndTradeTerm();
            }
        }

        /// <summary>
        /// 当前用户所在的操作口岸和揽货人所在的部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void trsSalesDep_Enter(object sender, EventArgs e)
        {
            List<OrganizationList> userOrganizationTreeLists = new List<OrganizationList>();
            if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.SalesID) == false)
            {
                userOrganizationTreeLists = userService.GetUserCompanyList(_oceanBookingInfo.SalesID.Value, null);
            }

            List<OrganizationList> saleOrgrnazitionTreeList = userService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
            foreach (OrganizationList dept in saleOrgrnazitionTreeList)
            {
                if (userOrganizationTreeLists.FindAll(o => o.ID == dept.ID).Count == 0)
                {
                    userOrganizationTreeLists.Add(dept);
                }
            }

            trsSalesDep.SetSource<OrganizationList>(userOrganizationTreeLists, LocalData.IsEnglish ? "EShortName" : "CShortName", "HasPermission");
        }

        void stxtPlaceOfDelivery_TextChanged(object sender, EventArgs e)
        {
            if (this._shown)
            {
                this.SetFinalDestinationByTransportClause();
                this.SetAgetnEnabledByPlaceOfDeliveryAndCompany();
            }
        }

        void cmbTradeTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._shown)
            {
                _oceanBookingInfo.TradeTermName = cmbTradeTerm.Text;
                this.SetBookingCustomerByCustomerAndTradeTerm();
                this.SetConsigneeByCustomerAndTradeTerm();
                this.SetShipperByBookingCustomerAndTradeTerm();
            }
        }

        #region 主要是设置控件的颜色、可使用性等属性

        /// <summary>
        /// 总调用处，会把其它方法都执行一遍
        /// </summary>
        void RunAtOnce()
        {
            this.cmbType_SelectedIndexChanged(null, null);
            //this.cmbOrderNo_TextChanged(this, null);
            this.chkHasContract_CheckedChanged(null, null);

            #region 根据数据 设置 控件可操作
            SetHBLEnabledByIsOnlyMBL(_oceanBookingInfo.IsOnlyMBL);
            SetContractBoxByHasContract(_oceanBookingInfo.IsContract);

            #endregion

            this.RefreshBarEnabled();
        }       

        #region 合约选择规则

        private void txtContractNo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //UNDONE:合约选择规则
        }

        #endregion

        #region IsContract合约

        private void chkHasContract_CheckedChanged(object sender, EventArgs e)
        {
            SetContractBoxByHasContract(chkHasContract.Checked);
        }

        private void SetContractBoxByHasContract(bool hasContract)
        {
            this.txtContractNo.Enabled = hasContract;
            this.txtContractNo.BackColor = hasContract ? SystemColors.Info : this.txtNo.BackColor;
        }

        #endregion

        #region IsOnlyMBL 如果选择只出MBL，则HBL要求录入栏位不可以录入

        void chkIsOnlyMBL_CheckedChanged(object sender, EventArgs e)
        {
            SetHBLEnabledByIsOnlyMBL(chkIsOnlyMBL.Checked);
        }
        /// <summary>
        ///IsOnlyMBL如果为选择状态，则HBL相关输入项设为启用状态，否则设为禁用状态
        /// </summary>
        private void SetHBLEnabledByIsOnlyMBL(bool isOnlyMBL)
        {
            if (isOnlyMBL)
            {
                _oceanBookingInfo.HBLPaymentTermID = null;
                _oceanBookingInfo.HBLReleaseType = FCMReleaseType.Unknown;
                _oceanBookingInfo.HBLRequirements = string.Empty;
            }
            else
            {
                //if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.HBLPaymentTermID))
                //    _oceanBookingInfo.HBLPaymentTermID = ICPCommUIHelperService.GetNormalDictionary(DataDictionaryType.Payment);
            }
        }

        #endregion


        #region 刷新工具栏按钮的可使用性

        void RefreshBarEnabled()
        {
            this.barAuditAndSave.Enabled = this._oceanBookingInfo.State == AEOrderState.NewOrder;

            if (_oceanBookingInfo.ID == Guid.Empty)
            {
                barReject.Enabled = barE_Booking.Enabled = false;
                this.barTruck.Enabled = false;

                this.barApplyAgent.Enabled = false;
                this.barRefresh.Enabled = false;
            }
            else
            {
                barReject.Enabled = _oceanBookingInfo.State == AEOrderState.NewOrder;

                this.barRefresh.Enabled = true;


            }

            this.txtState.Text = EnumHelper.GetDescription<AEOrderState>(this._oceanBookingInfo.State, LocalData.IsEnglish);
        }

        #endregion

        #endregion

        #region 控件联动

        #region 数据变动填充控件默认值 客户变了就刷新揽货方式等逻辑

        #region Port And Voyage


        ///// <summary>
        ///// 交货地 如果目的港运输条款<>Door，那么就为卸货港
        ///// </summary>
        //private void SetPlaceOfDeliveryByTransportClause()
        //{
        //    if (!Utility.GuidIsNullOrEmpty(this._oceanBookingInfo.PlaceOfDeliveryID)
        //        || Utility.GuidIsNullOrEmpty(this._oceanBookingInfo.TransportClauseID)) return;

        //    if (_oceanBookingInfo.TransportClauseName.Contains("-DOOR") == false)
        //    {
        //        stxtPlaceOfDelivery.Tag = _oceanBookingInfo.PlaceOfDeliveryID = _oceanBookingInfo.PODID;
        //        stxtPlaceOfDelivery.Text = _oceanBookingInfo.PlaceOfDeliveryName = _oceanBookingInfo.PODName;
        //    }
        //}

        /// <summary>
        /// 最终目的地 如果目的港运输条款<>Door，那么就为卸货港
        /// </summary>
        private void SetFinalDestinationByTransportClause()
        {
            if (!Utility.GuidIsNullOrEmpty(_oceanBookingInfo.FinalDestinationID)
                || Utility.GuidIsNullOrEmpty(this._oceanBookingInfo.TransportClauseID)) return;

            if (_oceanBookingInfo.TransportClauseName.Contains("-DOOR") == false)
            {
                //stxtFinalDestination.Tag = _oceanBookingInfo.FinalDestinationID = _oceanBookingInfo.PlaceOfDeliveryID;
                //stxtFinalDestination.Text = _oceanBookingInfo.FinalDestinationName = _oceanBookingInfo.PlaceOfDeliveryName;
            }
        }

        #endregion

        #region SetSalesDepartment

        /// <summary>
        /// 改变失去焦点后刷新揽货部门，如果有多个就清空，否则填充
        /// </summary>
        private void SetSalesDepartment()
        {
            List<UserOrganizationTreeList> userOrganizationTreeLists = new List<UserOrganizationTreeList>();
            if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.SalesID) == false)
            {
                userOrganizationTreeLists = userService.GetUserOrganizationTreeList(_oceanBookingInfo.SalesID.Value);

                UserOrganizationTreeList orginazation = userOrganizationTreeLists.Find(o => o.IsDefault);
                if (orginazation != null)
                {
                    this.trsSalesDep.ShowSelectedValue(orginazation.ID, LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName);
                    _oceanBookingInfo.SalesDepartmentID = orginazation.ID;
                    _oceanBookingInfo.SalesDepartmentName = LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName;
                }
                else
                {
                    this.trsSalesDep.ShowSelectedValue(Guid.Empty, string.Empty);
                    _oceanBookingInfo.SalesDepartmentID = Guid.Empty;
                    _oceanBookingInfo.SalesDepartmentName = string.Empty;
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
            _oceanBookingInfo.SalesDepartmentID = Guid.Empty;
            _oceanBookingInfo.BookingerID = null;
        }

        #endregion

        #region Other

        /// <summary>
        /// 根据公司和客户设置揽货方式
        /// </summary>
        private void SetSalesTypeByCustomerAndCompany()
        {
            if (_oceanBookingInfo.CompanyID != Guid.Empty && _oceanBookingInfo.CustomerID != Guid.Empty)
            {
                DataDictionaryInfo salesType = aeService.GetSalesType(_oceanBookingInfo.CustomerID, _oceanBookingInfo.CompanyID);
                if (salesType != null)
                {
                    _oceanBookingInfo.SalesTypeID = salesType.ID;
                    _oceanBookingInfo.SalesTypeName = LocalData.IsEnglish ? salesType.EName : salesType.CName;

                    this.cmbSalesType.ShowSelectedValue(_oceanBookingInfo.SalesTypeID, _oceanBookingInfo.SalesTypeName);
                }
            }
        }

        /// <summary>
        /// 根据操作口岸ID设置操作和文件栏的数据源
        /// </summary>
        private void SetOperatorByCompany()
        {
            if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.CompanyID))
            {
            }
            else
            {
                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                List<UserList> operators = userService.GetUnderlingUserList(new Guid[] { _oceanBookingInfo.CompanyID }, new string[] { "订舱" }, null, true);
                List<UserList> filers = userService.GetUnderlingUserList(new Guid[] { _oceanBookingInfo.CompanyID }, new string[] { "文件" }, null, true);
                List<UserList> overSeasFilers = userService.GetUnderlingUserList(new Guid[] { _oceanBookingInfo.CompanyID }, new string[] { "海外部客服" }, null, true);
            }
        }

        #endregion

        #region 设置默认海外部客服

        /// <summary>
        /// 当前客户最近业务所对应的海外部客服or 当前客户为新客户and当前揽货人最近业务所对应的海外部客服
        /// </summary>
        void SetDefaultOverseasFiler()
        {
            List<UserInfo> users = this.aeService.GetOverseasFilersList(this._oceanBookingInfo.CustomerID, this._oceanBookingInfo.SalesID,
                DateTime.Now.AddDays(-30), DateTime.Now, 1);

            if (users.Count > 0)
            {
            }
        }

        /// <summary>
        /// 当前客户最近业务所对应的文件or 当前客户为新客户and当前揽货人最近业务所对应的文件
        /// </summary>
        void SetDefaultFiler()
        {
            //List<UserInfo> users = this.aeService.GetFilersList(this._oceanBookingInfo.CustomerID, this._oceanBookingInfo.SalesID,
            //       DateTime.Now.AddDays(-30), DateTime.Now, 1);

            //if (users.Count > 0)
            //{
            //    this.mcmbFiler.ShowSelectedValue(users[0].ID, LocalData.IsEnglish ? users[0].EName : users[0].CName);
            //}
        }

        /// <summary>
        /// TODO: 和FSD作者仔细核对这个逻辑？
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mcmbSales_SelectedRow(object sender, EventArgs e)
        {
            //SetDefaultOverseasFiler();
            //SetDefaultFiler();

            this.SetSalesDepartment();
        }

        #endregion

        #region Customers

        /// <summary>
        /// 公司改变
        /// </summary>
        private void CompanyChanged()
        {
            SetSalesTypeByCustomerAndCompany();
            SetOperatorByCompany();
            SetAgentSourceByCompanyID(_oceanBookingInfo.CompanyID);
            SetAgetnEnabledByPlaceOfDeliveryAndCompany();

            this.orderFeeEditPart1.SetCompanyID(this._oceanBookingInfo.CompanyID);
        }

        /// <summary>
        /// 客户改变后需设置"订舱客户","收货人","代理","揽货方式","最近业务"
        /// </summary>
        /// <param name="customerType">客户的类型,请在方法外部获取</param>
        private void CustomerChanged(CustomerType? customerType)
        {
            SetBookingCustomerByCustomerAndTradeTerm();
            SetConsigneeByCustomerAndTradeTerm();
            SetSalesTypeByCustomerAndCompany();
            SetRecentlyOrderListByCustomerAndCompany();

            SetAgentSourceByCompanyID(_oceanBookingInfo.CompanyID);
            SetAgetnByCustomerAndCompany(customerType);

            SetDefaultOverseasFiler();
            SetDefaultFiler();
            SetShipperByBookingCustomerAndTradeTerm();
        }

        #region Agent
        /// <summary>
        /// 如果客户类型为货代和客户所在国家与操作口岸所在国家不同就填充代理
        /// "填充"的意思,就是把"代理"的ID和描述设置成和"客户"一样
        /// TODO:这里的客户描述信息应该从“客户”处复制，为什么要从服务里面获取？
        /// </summary>
        /// <param name="customerType">客户的类型,请在方法外部获取</param>
        private void SetAgetnByCustomerAndCompany(CustomerType? customerType)
        {
            if (customerType == null
                || Utility.GuidIsNullOrEmpty(_oceanBookingInfo.CompanyID)
                || Utility.GuidIsNullOrEmpty(_oceanBookingInfo.CustomerID))
            {
                return;
            }

            if (customerType.Value == CustomerType.Forwarding
                && !aeService.IsCustomerAndCompanySameCountry(_oceanBookingInfo.CustomerID, _oceanBookingInfo.CompanyID))
            {
                stxtAgent.Text = _oceanBookingInfo.AgentName = _oceanBookingInfo.CustomerName;
                stxtAgent.EditValue = _oceanBookingInfo.AgentID = _oceanBookingInfo.CustomerID;
                ICPCommUIHelperService.SetCustomerDesByID(_oceanBookingInfo.AgentID, _oceanBookingInfo.AgentDescription);
            }
        }

        /// <summary>
        /// 如果交货地所在的国家不存在于公司配置中客户对应的国家，那么就为只读，否则可以输入
        /// </summary>
        private void SetAgetnEnabledByPlaceOfDeliveryAndCompany()
        {
            if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.PlaceOfDeliveryID))
            {
                return;
            }

            // TODO:“指定货”目前在数据库的字典表里面还没有对应的英文
            if (
                    (
                        string.IsNullOrEmpty(this._oceanBookingInfo.SalesTypeName) == false &&
                        (this._oceanBookingInfo.SalesTypeName.Contains("指定货") || this._oceanBookingInfo.SalesTypeName.ToUpper().Contains("AGENT"))
                    )
                || aeService.IsPortCountryExistCompanyConfig(_oceanBookingInfo.PlaceOfDeliveryID, this._oceanBookingInfo.CompanyID))
            {
                stxtAgent.Enabled = true;
            }
            else
            {
                stxtAgent.Enabled = false;
                stxtAgent.Text = _oceanBookingInfo.AgentName = string.Empty;
                stxtAgent.EditValue = _oceanBookingInfo.AgentID = Guid.Empty;
                _oceanBookingInfo.AgentDescription = new CustomerDescription();
            }
        }


        /// <summary>
        /// 设置Agent数据源
        /// </summary>
        private void SetAgentSourceByCompanyID(Guid companyID)
        {
            stxtAgent.DataSource = null;
            if (Utility.GuidIsNullOrEmpty(companyID))
            {
                stxtAgent.Enabled = false;
                return;
            }

            List<CustomerList> agentCustomers = configureService.GetCompanyAgentList(_oceanBookingInfo.CompanyID, true);
            CustomerList emptyCustomer = new CustomerList();
            emptyCustomer.CName = emptyCustomer.EName = string.Empty;
            emptyCustomer.ID = Guid.Empty;
            agentCustomers.Insert(0, emptyCustomer);
            SetAgentSource(agentCustomers);
        }
        private void SetAgentSource(List<CustomerList> agentCustomers)
        {
            stxtAgent.SetLanguage(LocalData.IsEnglish);
            stxtAgent.DataSource = agentCustomers;
            if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.AgentID))
            {
                stxtAgent.EditValue = _oceanBookingInfo.AgentID = agentCustomers[0].ID;
            }
            stxtAgent.EditValueChanged -= new EventHandler(stxtAgent_EditValueChanged);
            stxtAgent.EditValueChanged += new EventHandler(stxtAgent_EditValueChanged);
        }
        void stxtAgent_EditValueChanged(object sender, EventArgs e)
        {
            if (stxtAgent.EditValue != null && stxtAgent.EditValue.ToString().Length > 0)
            {
                Guid id = new Guid(stxtAgent.EditValue.ToString());
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
                stxtAgent.CustomerDescription = _oceanBookingInfo.AgentDescription = new CustomerDescription();
            }
            else
            {
                ICPCommUIHelperService.SetCustomerDesByID(id, _oceanBookingInfo.AgentDescription);
                stxtAgent.CustomerDescription = _oceanBookingInfo.AgentDescription;
            }
        }

        #endregion


        /// <summary>
        /// 设置发货人 如果贸易条款为CIF，那么就为订舱客户
        /// </summary>
        private void SetShipperByBookingCustomerAndTradeTerm()
        {
            if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.ShipperID) == false
                || Utility.GuidIsNullOrEmpty(this._oceanBookingInfo.TradeTermID))
            {
                return;
            }

            if (_oceanBookingInfo.TradeTermName.Contains("CIF"))
            {
                stxtShipper.Tag = _oceanBookingInfo.ShipperID = _oceanBookingInfo.BookingCustomerID;
                stxtShipper.Text = _oceanBookingInfo.ShipperName = _oceanBookingInfo.BookingCustomerName;
                ICPCommUIHelperService.SetCustomerDesByID(_oceanBookingInfo.ShipperID, _oceanBookingInfo.ShipperDescription);
            }
        }

        /// <summary>
        /// 收货人:如果贸易条款为FOB或EXWORK，那么就为客户
        /// </summary>
        private void SetConsigneeByCustomerAndTradeTerm()
        {
            if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.ConsigneeID) == false) return;

            if (this._oceanBookingInfo.TradeTermName == "FOB" || this._oceanBookingInfo.TradeTermName == "EXWORK")
            {
                this.stxtConsignee.Tag = this._oceanBookingInfo.ConsigneeID = this._oceanBookingInfo.CustomerID;
                this.stxtConsignee.Text = this._oceanBookingInfo.ConsigneeName = this._oceanBookingInfo.CustomerName;
                ICPCommUIHelperService.SetCustomerDesByID(_oceanBookingInfo.ConsigneeID, _oceanBookingInfo.ConsigneeDescription);
            }

            this.ResetDescription();
        }

        /// <summary>
        /// 设置订舱客户和发货（订舱客户:如果贸易条款为CIF，那么就为客户
        /// </summary>
        private void SetBookingCustomerByCustomerAndTradeTerm()
        {
            if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.BookingCustomerID) == false)
            {
                return;
            }

            if (!string.IsNullOrEmpty(_oceanBookingInfo.TradeTermName)
                && _oceanBookingInfo.TradeTermName.Contains("CIF"))
            {
                stxtBookingCustomer.Tag = _oceanBookingInfo.BookingCustomerID = _oceanBookingInfo.CustomerID;
                stxtBookingCustomer.Text = _oceanBookingInfo.BookingCustomerName = _oceanBookingInfo.CustomerName;
                ICPCommUIHelperService.SetCustomerDesByID(_oceanBookingInfo.CustomerID, _oceanBookingInfo.BookingCustomerDescription);
            }
        }

        /// <summary>
        /// 最近业务
        /// </summary>
        private void SetRecentlyOrderListByCustomerAndCompany()
        {
            if (_oceanBookingInfo.ID != Guid.Empty || _oceanBookingInfo.CompanyID == Guid.Empty || _oceanBookingInfo.CustomerID == Guid.Empty)
            {
                bsRecentTenOrders.Clear();
            }
            else
            {
                bsRecentTenOrders.Clear();
                //小颜说取消此功能
                //List<AirOrderList> orderList = aeService.GetRecentlyAirOrderList(_oceanBookingInfo.CompanyID, _oceanBookingInfo.CustomerID, 10);
                //if (orderList != null && orderList.Count > 0)
                //{
                //    bsRecentTenOrders.DataSource = orderList;
                //    stxtCustomer.ShowPopup();
                //}
            }
        }

        #endregion

        #endregion

        #region 最近十票业务

        AirOrderList CurrentOrderList
        {
            get
            {
                if (bsRecentTenOrders.List == null || bsRecentTenOrders.Current == null) return null;
                return bsRecentTenOrders.Current as AirOrderList;
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
                AirBookingInfo order = aeService.GetAirBookingInfo(CurrentOrderList.ID);
                if (order == null) return;

                order.ID = Guid.Empty;

                order.No = string.Empty;

                order.State = AEOrderState.NewOrder;

                //order.AirShippingOrderID = Guid.Empty;
                //order.AirShippingOrderNo = string.Empty;
                order.SalesID = LocalData.UserInfo.LoginID;
                order.SalesName = LocalData.UserInfo.LoginName;
                order.CreateDate = DateTime.Now;
                this._oceanBookingInfo = order;

                this.ShowOrder();
                this.RunAtOnce();
                this.ResetDescription();

                this.EndEdit();

                this.Invalidate();
            }
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

        private void cmbCargoType_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbCargoType.EditValue == null)
            {
                this._oceanBookingInfo.CargoDescription = null;
                RemoveCargoPart();
                return;
            }

            CargoType cargoType = (CargoType)Enum.Parse(typeof(CargoType), cmbCargoType.EditValue.ToString());
            RemoveCargoPart();
            SetCargo(sender as Control, cargoType);

        }
        /// <summary>
        /// 删除线前打开的货物描述
        /// </summary>
        private void RemoveCargoPart()
        {
            if (cargoDescriptionPart1 != null)
            {
                cargoDescriptionPart1.Hide();
                this.navBarGroupControlContainer2.Controls.Remove(cargoDescriptionPart1);
                cargoDescriptionPart1.Dispose();
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region IEditPart 成员

        void GetData(Guid orderId)
        {
            this._oceanBookingInfo = aeService.GetAirBookingInfo(orderId);
        }

        void ShowOrder()
        {
            InitData();

            this.bsBookingInfo.DataSource = _oceanBookingInfo;
            bsBookingInfo.ResetBindings(false);
            this.bsBookingInfo.CancelEdit();

            InitControls();

            List<AirBookingFeeList> feelist = null;

            if (_oceanBookingInfo.ID == Guid.Empty)
            {
                feelist = new List<AirBookingFeeList>();
            }
            else
            {
                feelist = aeService.GetAirOrderFeeList(_oceanBookingInfo.ID);
            }

            this.orderFeeEditPart1.SetSource(feelist);
        }
        #region 货物描述
        private void SetCargo(Control sender, CargoType cargoType)
        {
            if (!this.cmbCargoType.Focused)
            {
                return;
            }
            if (cargoType == CargoType.Awkward)
            {
                if (_oceanBookingInfo.CargoDescription == null
                    || _oceanBookingInfo.CargoDescription.Cargo == null || _oceanBookingInfo.CargoDescription.Cargo is AwkwardCargo == false)
                {
                    AwkwardCargo cargo = new AwkwardCargo();
                    cargo.NetWeightUnit = this.cmbWeightUnit.Text;
                    cargo.GrossWeightUnit = this.cmbWeightUnit.Text;
                    cargo.Quantity = (int)this.numQuantity.Value;
                    _oceanBookingInfo.CargoDescription = new CargoDescription(cargo);
                    _oceanBookingInfo.IsDirty = true;
                }

                if (cargoDescriptionPart1 is ICP.FCM.AirExport.UI.Common.Controls.AwkwardDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.AirExport.UI.Common.Controls.AwkwardDescriptionPart();
                    cargoDescriptionPart1.ShowWeightUnit(this._weightUnits);
                    this.navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Dangerous)
            {
                if (_oceanBookingInfo.CargoDescription == null
                    || _oceanBookingInfo.CargoDescription.Cargo == null || _oceanBookingInfo.CargoDescription.Cargo is DangerousCargo == false)
                {
                    _oceanBookingInfo.CargoDescription = new CargoDescription(new DangerousCargo());
                    _oceanBookingInfo.IsDirty = true;
                }

                if (cargoDescriptionPart1 is ICP.FCM.AirExport.UI.Common.Controls.DangerousDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.AirExport.UI.Common.Controls.DangerousDescriptionPart();
                    this.navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Dry)
            {
                if (_oceanBookingInfo.CargoDescription == null
                    || _oceanBookingInfo.CargoDescription.Cargo == null || _oceanBookingInfo.CargoDescription.Cargo is DryCargo == false)
                {
                    _oceanBookingInfo.CargoDescription = new CargoDescription(new DryCargo());
                    _oceanBookingInfo.IsDirty = true;
                }

                if (cargoDescriptionPart1 is ICP.FCM.AirExport.UI.Common.Controls.DryDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.AirExport.UI.Common.Controls.DryDescriptionPart();
                    this.navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Reefer)
            {
                if (_oceanBookingInfo.CargoDescription == null
                    || _oceanBookingInfo.CargoDescription.Cargo == null || _oceanBookingInfo.CargoDescription.Cargo is ReeferCargo == false)
                {
                    _oceanBookingInfo.CargoDescription = new CargoDescription(new ReeferCargo());
                    _oceanBookingInfo.IsDirty = true;
                }

                if (cargoDescriptionPart1 is ICP.FCM.AirExport.UI.Common.Controls.ReeferDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.AirExport.UI.Common.Controls.ReeferDescriptionPart();
                    this.navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            cargoDescriptionPart1.SetParentControl(sender, _oceanBookingInfo.CargoDescription, txtCargoDescription);
            AirBookingInfo currentData = bsBookingInfo.DataSource as AirBookingInfo;
            currentData.IsDirty = true;

        }
        #endregion

        public void BindingData(object data)
        {
            //AirBookingInfo currentData = bsBookingInfo.DataSource as AirBookingInfo;
            //currentData.ContractID = Guid.Empty;//合约号暂时没有
            this.SuspendLayout();
            this.orderFeeEditPart1.SetService(Workitem);
            //this.bookingPOEditPart1.SetService(Workitem);

            AirBookingList listInfo = data as AirBookingList;

            if (listInfo == null)
            {
                //新建
                this._oceanBookingInfo = new AirBookingInfo();
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

            _oceanBookingInfo.CancelEdit();

            this.InitalComboxes();

            this.ShowOrder();

            this.SearchRegister();
            this.SetLazyLoaders();

            this.ResumeLayout(true);
        }
        public override object DataSource
        {
            get { return bsBookingInfo.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return this.Save(this._oceanBookingInfo, false);
        }

        public override void EndEdit()
        {
            this.Validate();
            bsBookingInfo.EndEdit();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region 工具栏事件

        #region 验证界面输入

        private bool ValidateData()
        {
            this.EndEdit();
            this.dxErrorProvider1.ClearErrors();

            List<bool> isScrrs = new List<bool> { true, true };

            isScrrs[0] = _oceanBookingInfo.Validate
               (
                   delegate(ValidateEventArgs e)
                   {


                       if (_oceanBookingInfo.POLID != Guid.Empty && _oceanBookingInfo.POLID == _oceanBookingInfo.PODID)
                           e.SetErrorInfo("PODID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");

                       if (_oceanBookingInfo.ETA != null && _oceanBookingInfo.ETD != null
                           && _oceanBookingInfo.ETD > _oceanBookingInfo.ETA)
                           e.SetErrorInfo("ETA", LocalData.IsEnglish ? "ETD can't bigger ETA." : "ETD不能大于ETA.");

                       if (_oceanBookingInfo.ExpectedShipDate != null && _oceanBookingInfo.ExpectedArriveDate != null
                           && _oceanBookingInfo.ExpectedShipDate.Value.Date >= _oceanBookingInfo.ExpectedArriveDate.Value.Date)
                           e.SetErrorInfo("ExpectedShipDate", LocalData.IsEnglish ? "ExpectedShipDate can't bigger ExpectedArriveDate." : "期望出运日不能大于期望到达日.");

                       // 小赵说这个逻辑是不需要的
                       //if (_oceanBookingInfo.EstimatedDeliveryDate != null && _oceanBookingInfo.DeliveryDate != null
                       //    && _oceanBookingInfo.EstimatedDeliveryDate.Value.Date >= _oceanBookingInfo.DeliveryDate.Value.Date)
                       //    e.SetErrorInfo("EstimatedDeliveryDate", LocalData.IsEnglish ? "EstimatedDeliveryDate can't bigger DeliveryDate." : "估计交货日不能大于实际交货日.");

                       if (this._oceanBookingInfo.SODate.HasValue && this._oceanBookingInfo.ClosingDate.HasValue)
                       {
                           if (this._oceanBookingInfo.SODate.Value >= this._oceanBookingInfo.ClosingDate.Value)
                           {
                               e.SetErrorInfo("SODate", LocalData.IsEnglish ? "Confirmed data must ealier than Closing date." : "确认日必须小于截关日.");
                           }
                       }

                       //if (this._oceanBookingInfo.SODate.HasValue && this._oceanBookingInfo.CYClosingDate.HasValue)
                       //{
                       //    if (this._oceanBookingInfo.SODate.Value >= this._oceanBookingInfo.CYClosingDate.Value)
                       //    {
                       //        e.SetErrorInfo("SODate", LocalData.IsEnglish ? "Confirmed data must ealier than CYClosing date." : "确认日必须小于截柜日.");
                       //    }
                       //}

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

        #region 保存

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Save(this._oceanBookingInfo, false);
            }
        }

        private bool Save(AirBookingInfo currentData, bool isSavingAs)
        {
            if (ValidateData() == false)
            {
                return false;
            }
            if (!currentData.IsDirty && !currentData.IsNew && !this.orderFeeEditPart1.IsChanged)
            {
                return true;
            }

            try
            {
                BookingSaveRequest originalBooking = null;
                if (Utility.GuidIsNullOrEmpty(currentData.ID) || Utility.GuidIsNullOrEmpty(currentData.FilightId))
                {
                    originalBooking = SaveAirBooking(currentData);
                }
                else if (_oceanBookingInfo.IsDirty)
                {
                    if (_oceanBookingInfo.BookingDate != null)
                        _oceanBookingInfo.State = AEOrderState.BookingConfirmed;

                    originalBooking = SaveAirBooking(_oceanBookingInfo);
                }

                List<FeeSaveRequest> originalFees = this.orderFeeEditPart1.SaveFee(currentData.ID);

                Dictionary<Guid, SaveResponse> saved = this.aeService.SaveAirBookingWithTrans(originalBooking,
                    originalFees);

                if (originalBooking != null)
                {
                    SaveResponse.Analyze(new List<SaveRequest> { originalBooking }, saved, true);
                    this.RefreshUI(originalBooking);
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

                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                return false;
            }
        }

        private void AfterSave()
        {
            _oceanBookingInfo.CancelEdit();
            _oceanBookingInfo.BeginEdit();

            this.TriggerSavedEvent();

            this.gvOrders.DoubleClick -= new System.EventHandler(this.gvOrders_DoubleClick);
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

            this.SetTitle();
        }

        void SetTitle()
        {
            if (this._oceanBookingInfo.ID == Guid.Empty)
            {
                this.Title = LocalData.IsEnglish ? "Add Booking" : "新增订舱";
            }
            else
            {
                string titleNo = string.Empty;

                if (this._oceanBookingInfo.No.Length > 4)
                {
                    titleNo = this._oceanBookingInfo.No.Substring(this._oceanBookingInfo.No.Length - 4, 4);
                }
                else
                {
                    titleNo = this._oceanBookingInfo.No;
                }

                this.Title = LocalData.IsEnglish ? "Edit Booking " + titleNo : "编辑订舱：" + titleNo;
            }
        }

        void TriggerSavedEvent()
        {
            if (Saved != null)
            {
                this._oceanBookingInfo.SalesName = this._oceanBookingInfo.SalesID.ToGuid() == Guid.Empty ?
                    string.Empty : this.mcmbSales.EditText;
                //this._oceanBookingInfo.CarrierName = this._oceanBookingInfo.CarrierID == Guid.Empty ?
                //    string.Empty : this.mcmbCarrier.EditText;
                this._oceanBookingInfo.FilerName = this._oceanBookingInfo.FilerId.ToGuid() == Guid.Empty ?
                    string.Empty : this.mcmbFiler.Text;
                this._oceanBookingInfo.BookingerName = this._oceanBookingInfo.BookingerID.ToGuid() == Guid.Empty ?
                    string.Empty : this.mcmbBookinger.Text;
                //this._oceanBookingInfo.OverSeasFilerName = this._oceanBookingInfo.OverSeasFilerID.ToGuid() == Guid.Empty ?
                //    string.Empty : this.mcmbOverseasFiler.Text;
                //this._oceanBookingInfo.VesselVoyage = this.stxtPreVoyage.Text + (this.stxtPreVoyage.Text.Trim().Length > 0 ? ";" : "") + this.stxtVoyage.Text;
                Saved(new object[] { (AirBookingList)_oceanBookingInfo });

                this._oceanBookingInfo.IsDirty = false;
            }
        }

        /// <summary>
        /// 客户参考号暂时传空值
        /// </summary>
        /// <param name="currentData"></param>
        private BookingSaveRequest SaveAirBooking(AirBookingInfo currentData)
        {
            this.EndEdit();
            if (chkHasContract.Checked) { currentData.IsContract = true; }
            else { currentData.IsContract = false; }
            if (currentData.IsDirty == true || currentData.IsNew)
            {
                BookingSaveRequest saveRequest = new BookingSaveRequest();

                saveRequest.id = currentData.ID;
                saveRequest.customerRefNo = string.Empty;
                saveRequest.customerID = currentData.CustomerID;
                saveRequest.tradeTermID = currentData.TradeTermID;
                saveRequest.companyID = currentData.CompanyID;
                saveRequest.bookingerID = currentData.BookingerID;
                saveRequest.filerID = currentData.FilerId;
                saveRequest.salesDepartmentID = currentData.SalesDepartmentID;
                saveRequest.salesID = currentData.SalesID;
                saveRequest.salesTypeID = currentData.SalesTypeID;
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
                saveRequest.finalDestinationID = currentData.PlaceOfDeliveryID;
                //saveRequest.placeOfDeliveryID = currentData.PlaceOfDeliveryID;
                saveRequest.agentID = currentData.AgentID;
                saveRequest.agentDescription = currentData.AgentDescription;
                //saveRequest.carrierID = currentData.CarrierID;
                saveRequest.agentOfCarrierID = currentData.AgentOfCarrierID;
                saveRequest.isContract = currentData.IsContract;
                //saveRequest.freightRateID = currentData.ContractID;//合约号暂时没有
                if (currentData.IsContract == false)
                {
                    saveRequest.freightRateID = Guid.Empty;
                }
                saveRequest.soDate = currentData.SODate;
                saveRequest.estimatedDeliveryDate = currentData.EstimatedDeliveryDate;
                saveRequest.actualDeliveryDate = currentData.DeliveryDate;
                saveRequest.expectedShipDate = currentData.ExpectedShipDate;
                saveRequest.expectedArriveDate = currentData.ExpectedArriveDate;
                saveRequest.ETD = currentData.ETD;
                saveRequest.ETA = currentData.ETA;
                saveRequest.dOCClosingDate = currentData.DOCClosingDate;
                saveRequest.closingDate = currentData.ClosingDate;
                saveRequest.paymentTermID = currentData.PaymentTermID;
                saveRequest.transportClauseID = currentData.TransportClauseID;
                saveRequest.shippingLineID = currentData.ShippingLineID;
                saveRequest.commodity = currentData.Commodity;
                saveRequest.quantity = currentData.Quantity;
                saveRequest.quantityUnitID = currentData.QuantityUnitID;
                saveRequest.weight = currentData.Weight;
                saveRequest.weightUnitID = currentData.WeightUnitID;
                saveRequest.measurement = currentData.Measurement;
                saveRequest.measurementUnitID = currentData.MeasurementUnitID;
                saveRequest.cargoDescription = currentData.CargoDescription;
                saveRequest.isTruck = currentData.IsTruck;
                saveRequest.isCustoms = currentData.IsCustoms;
                saveRequest.isCommodityInspection = currentData.IsCommodityInspection;
                saveRequest.isQuarantineInspection = currentData.IsQuarantineInspection;
                saveRequest.isWarehouse = currentData.IsWareHouse;
                saveRequest.isOnlyMBL = currentData.IsOnlyMBL;
                saveRequest.remark = currentData.Remark;
                saveRequest.finalDestinationID = currentData.PlaceOfDeliveryID;
                saveRequest.warehouseID = currentData.WarehouseID;
                saveRequest.saveByID = LocalData.UserInfo.LoginID;
                saveRequest.airShippingOrderUpdateDate = currentData.UpdateDate;
                saveRequest.oceanOrderUpdateDate = currentData.AirShippingOrderUpdateDate;
                saveRequest.airWayBillID = currentData.AirShippingOrderID;
                saveRequest.AirlineID = currentData.AirCompanyId;
                saveRequest.FlightID = currentData.FilightId;

                saveRequest.AddInvolvedObject(currentData);

                return saveRequest;
            }
            else
            {
                return null;
            }
        }

        void RefreshUI(BookingSaveRequest saveRequest)
        {
            SingleResult result = saveRequest.SingleResult;

            AirBookingInfo currentData = saveRequest.UnBoxInvolvedObject<AirBookingInfo>()[0];

            currentData.ID = result.GetValue<Guid>("ID");
            currentData.No = result.GetValue<String>("No");
            currentData.UpdateDate = result.GetValue<DateTime?>("AirwayBillIDUpdateDate");
            currentData.AirShippingOrderID = result.GetValue<Guid?>("AirwayBillID");
            currentData.State = (AEOrderState)result.GetValue<byte>("State");
            currentData.AirShippingOrderUpdateDate = result.GetValue<DateTime?>("UpdateDate");
            currentData.IsDirty = false;

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
            if (currentData.BookingCustomerDescription != null)
            {
                currentData.BookingCustomerDescription.IsDirty = false;
            }

        }

        #endregion

        #region 另存为

        private void barSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (SaveAs())
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save as a new booking order successfully. Ref. NO. is " + this._oceanBookingInfo.No + "." : "已成功另存为一票新订单，业务号为" + this._oceanBookingInfo.No + "。");
                    if (Saved != null)
                    {
                        Saved(new object[] { this._oceanBookingInfo });
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

            if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "是否另存为一票新的订舱单?",
                            LocalData.IsEnglish ? "Tip" : "提示",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }

            AirBookingInfo orderInfo = Utility.Clone<AirBookingInfo>(this._oceanBookingInfo);
            orderInfo.ID = Guid.Empty;
            orderInfo.No = string.Empty;

            if (string.IsNullOrEmpty(orderInfo.FilightNo))
            {
                orderInfo.State = AEOrderState.NewOrder;
            }
            else
            {
                orderInfo.State = AEOrderState.BookingConfirmed;
            }

            orderInfo.CreateByID = LocalData.UserInfo.LoginID;
            orderInfo.CreateByName = LocalData.UserInfo.LoginName;
            orderInfo.CreateDate = DateTime.Now;
            orderInfo.BookingDate = DateTime.Now;
            orderInfo.UpdateDate = null;

            orderInfo.IsDirty = true;

            this._oceanBookingInfo = orderInfo;

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

        #region 审核并保存

        /// <summary>
        /// 审核并保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAuditAndSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (!this.Save(this._oceanBookingInfo, false))
                {
                    return;
                }

                if (this._oceanBookingInfo.State != AEOrderState.NewOrder)
                {
                    return;
                }

                Common.Parts.EditRemarkPart editRemarkPart = Workitem.Items.AddNew<Common.Parts.EditRemarkPart>();
                editRemarkPart.LabRemark = LocalData.IsEnglish ? "Audit memo" : "审核意见";
                editRemarkPart.RemartRequired = true;
                editRemarkPart.Saved += delegate(object[] prams)
                {
                    try
                    {
                        SingleResult result = aeService.ChangeAirOrderStateWithTargetState(_oceanBookingInfo.ID, AEOrderState.Checked, prams[0].ToString(), LocalData.UserInfo.LoginID, _oceanBookingInfo.UpdateDate);

                        ICP.Framework.CommonLibrary.Logger.Log.Info(result);

                        this._oceanBookingInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                        this._oceanBookingInfo.State = (AEOrderState)result.GetValue<byte>("State");

                        this.TriggerSavedEvent();

                        RefreshBarEnabled();

                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Audit successfully!" : "审核订单成功！");
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                    }
                };
                string title = LocalData.IsEnglish ? "Audit Order" : "审核订单";
                PartLoader.ShowDialog(editRemarkPart, title);
            }
        }

        #endregion

        #region 打印

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!SaveData())
            {
                return;
            }

            //AirExportPrintHelper.PrintOEBookingConfirmation(_oceanBookingInfo.ID);
        }

        /// <summary>
        /// 打印业务联单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barPrintOrder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!SaveData())
            {
                return;
            }

            //AirExportPrintHelper.PrintOEOrder(this._oceanBookingInfo.ID);
        }

        private void barPrintInWarehouse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!SaveData())
            {
                return;
            }

            throw new NotImplementedException(LocalData.IsEnglish ? "To be defined on next version." : "本版本暂不提供入仓通知单打印功能。");
        }

        #endregion

        #region 打回

        private void barReject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_oceanBookingInfo.State == AEOrderState.Rejected) return;

            Common.Parts.EditRemarkPart editRemarkPart = Workitem.Items.AddNew<Common.Parts.EditRemarkPart>();
            editRemarkPart.LabRemark = LocalData.IsEnglish ? "Reject reason" : "打回原因";
            editRemarkPart.RemartRequired = true;
            editRemarkPart.Saved += delegate(object[] prams)
            {
                try
                {
                    bool isDirty = _oceanBookingInfo.IsDirty;
                    SingleResult result = aeService.ChangeAirOrderStateWithTargetState(_oceanBookingInfo.ID, AEOrderState.Rejected, prams[0].ToString(), LocalData.UserInfo.LoginID, _oceanBookingInfo.UpdateDate);

                    this._oceanBookingInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    this._oceanBookingInfo.State = (AEOrderState)result.GetValue<byte>("State");

                    this.TriggerSavedEvent();

                    RefreshBarEnabled();
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
            };
            string title = LocalData.IsEnglish ? "Reject Order" : "打回订单";
            PartLoader.ShowDialog(editRemarkPart, title);
        }

        #endregion

        #region 电子订舱

        private void barE_Booking_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        #endregion

        #region 刷新

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
                //                                   LocalData.IsEnglish ? "Tip" : "提示",
                //                                   MessageBoxButtons.YesNo,
                //                                   MessageBoxIcon.Question);
                //if (dialogResult == DialogResult.Yes)
                //{
                try
                {
                    RefreshData(this._oceanBookingInfo.ID);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Refersh successfully." : "刷新成功.");
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Refersh failed." + ex.Message : "刷新失败." + ex.Message);
                }
                //}
            }
        }

        void RefreshData(Guid orderId)
        {
            this.GetData(this._oceanBookingInfo.ID);
            this.ShowOrder();
            this.RunAtOnce();
            this.ResetDescription();
            this.SetTitle();
        }

        #endregion

        #endregion

        #region 清理资源和避免多余操作

        void BookingBaseEditPart_Disposed(object sender, EventArgs e)
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

        void BookingBaseEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (this._oceanBookingInfo.IsDirty)
            {
                DialogResult dr = PartLoader.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!this.Save(this._oceanBookingInfo, false))
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

        void BookingBaseEditPart_Load(object sender, EventArgs e)
        {
            this.SetTitle();
            this.RegisterRelativeEvents();
            this.RegisterRelativeEventsAndRunOnce();

            Utility.SetCustomerTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtCustomer,
                stxtBookingCustomer,
                stxtAgent.lookUpEdit1,
                stxtAgentOfCarrier,
                //mcmbAirCompany.popEdit1,
            });

            Utility.SetPortTextEditNullValuePrompt(new List<TextEdit>
            {
                //stxtPlaceOfDelivery,
                //stxtPlaceOfReceipt ,
                //stxtFinalDestination ,
                stxtDetination,
                stxtDeparture,
            });
            Utility.SetVoyageTextEditNullValuePrompt(new List<TextEdit>
            {
                //stxtPreVoyage ,
                //stxtVoyage ,
            });

            this.SmartPartClosing += new EventHandler<WorkspaceCancelEventArgs>(BookingBaseEditPart_SmartPartClosing);
            this.ActivateSmartPartClosingEvent(this.Workitem);
        }



        /// <summary>
        /// 由于初次加载的时候IsDirty已经确保为false
        /// 故很可靠。
        /// 要保证这一点：数据最早进来的时候，立刻CancelEdit。因为可能在网格控件中触发过BeginEdit
        /// </summary>
        bool _shown
        {
            get
            {
                return this._oceanBookingInfo.IsDirty;
            }
        }

        private void barApplyAgent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //fcmCommonClientService.OpenAgentRequestPart(this._oceanBookingInfo.ID, ICP.Framework.CommonLibrary.Common.OperationType.AirExport, Workitem);
                fcmCommonClientService.OpenAgentRequestPart(this._oceanBookingInfo.ID, ICP.Framework.CommonLibrary.Common.OperationType.AirExport, Workitem);         
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
        }
        private void mcmbAirCompany_SelectedRow(object sender, EventArgs e)
        {
        }
        private void barTruck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<AirTruckInfo> truckList = aeService.GetAirTruckServiceList(this._oceanBookingInfo.ID);
            SingleResult recentData = aeService.GetTruckRecentData(this._oceanBookingInfo.ID);

            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            stateValues.Add("Booking", this._oceanBookingInfo);

            if (recentData != null)
            {
                stateValues.Add("RecentTruckerID", recentData.GetValue<Guid?>("TruckerID"));
                stateValues.Add("RecentShipperID", recentData.GetValue<Guid?>("ShipperID"));
                stateValues.Add("ReturnLocationID", recentData.GetValue<Guid?>("ReturnLocationID"));
                stateValues.Add("ContainerDescription", SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), recentData.GetValue<string>("ContainerDescription")));
                stateValues.Add("CustomsBrokerID", recentData.GetValue<Guid?>("CustomsBrokerID"));
                stateValues.Add("IsDrivingLicence", recentData.GetValue<bool?>("IsDrivingLicence"));
                stateValues.Add("Remark", recentData.GetValue<string>("Remark"));
            }

            string title = LocalData.IsEnglish ? "Truck Service" : "拖车服务";
            PartLoader.ShowEditPart<Booking.AirTruckEditPart>(Workitem,
                truckList,
                stateValues,
                Booking.BookingListPart.GetLineNo(this._oceanBookingInfo),
                null,
                Booking.AEBookingCommandConstants.Command_Truck + _oceanBookingInfo.ID.ToString());

        }    

        private void cmbFlightNo_SelectedRow(object sender, EventArgs e)
        {
            if (cmbFlightNo.EditValue != null && cmbFlightNo.EditValue.ToString().Length > 0)
            {
                _CurrentData = bsBookingInfo.DataSource as AirBookingInfo;
                FlightInfo flightInfo = ConfigureService.GetFilghtInfo(new Guid(cmbFlightNo.EditValue.ToString()));

                if (mcmbAirCompany.EditValue != null && mcmbAirCompany.EditValue.ToString().Length> 0)
                {
                    if ((Guid)mcmbAirCompany.EditValue != flightInfo.AirlineID)
                    {
                        DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show("是否替换原有航空公司?",
                                                            "提示",
                                                           MessageBoxButtons.YesNo,
                                                           MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            _CurrentData.AirCompanyId = flightInfo.AirlineID;
                            _CurrentData.AirCompanyName = flightInfo.AirlineName;
                            this.mcmbAirCompany.ShowSelectedValue(this._CurrentData.AirCompanyId, this._CurrentData.AirCompanyName);
                        }
                    }
                }
                else
                {
                    _CurrentData.AirCompanyId = flightInfo.AirlineID;
                    _CurrentData.AirCompanyName = flightInfo.AirlineName;
                    this.mcmbAirCompany.ShowSelectedValue(this._CurrentData.AirCompanyId, this._CurrentData.AirCompanyName);
                }

                //有了航班号后，航空公司和确认日期要求必填
                mcmbAirCompany.SpecifiedBackColor = SystemColors.Info;
                this.dteSODate.BackColor = SystemColors.Info;
                this.stxtAgentOfCarrier.BackColor = SystemColors.Info;
            }
            else
            {
                mcmbAirCompany.SpecifiedBackColor = Color.White;
                this.dteSODate.BackColor = Color.White;
                this.stxtAgentOfCarrier.BackColor = Color.White;
            }
        }    
    }
}
