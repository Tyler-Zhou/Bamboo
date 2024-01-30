using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.DomesticTrade.ServiceInterface;
using ICP.FCM.DomesticTrade.ServiceInterface.CompositeObjects;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using ICP.Framework.ClientComponents.Controls;
using DevExpress.XtraEditors.Controls;
using ICP.Common.UI;
using ICP.FCM.Common.UI;
namespace ICP.FCM.DomesticTrade.UI.Booking
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

        [ServiceDependency]
        public IReportViewService reportViewService { get; set; }

        [ServiceDependency]
        public IDTReportDataService DTReportSrvice { get; set; }

        [ServiceDependency]
        public IDataFindClientService dfService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IOrganizationService organizationService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IConfigureService configureService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ITransportFoundationService tfService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IGeographyService geographyService { get; set; }

        [ServiceDependency]
        public IDomesticTradeService oeService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ICustomerService customerService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IPermissionService permissionService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelperService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public DomesticTradePrintHelper ExportPrintHelper { get; set; }

        [ServiceDependency]
        public ICP.Framework.CommonLibrary.Client.IErrorTraceService errorService { get; set; }

        #endregion

        #region 本地变量

        DTBookingInfo _DTBookingInfo = null;

        //List<OceanBookingPOList> _DTBookingPOInfo = null;        

        /// <summary>
        /// 缓存国家列表,只获取一次.现只用于客户弹出式描述框
        /// </summary>
        List<CountryList> _countryList = null;


        // private VoyageDateInfoHelper voyageDateHelper = null;


        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BookingBaseEditPart()
        {
            InitializeComponent();

            if (LocalData.IsDesignMode)
            {
                return;
            }
            // voyageDateHelper = new VoyageDateInfoHelper();
            // voyageDateHelper.Init(VoyageFormType.DomesticTrade, stxtPreVoyage, stxtVoyage, stxtPlaceOfReceipt, stxtPOL, stxtPOD, dtPreETD, dteETD, dteETA, null, dteCYClosingDate, dteDOCClosingDate,null,null);
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
            labCarrier.Text = "船公司";
            labCommodity.Text = "品名";
            labCompany.Text = "操作口岸";
            labConsignee.Text = "收货人";
            labCustomer.Text = "客户";
            labEstimatedDeliveryDate.Text = "估计交货";
            labExpectedArriveDate.Text = "期望到达";
            labExpectedShipDate.Text = "期望出运";

            labMeasurement.Text = "体积";
            labNo.Text = "业务号";
            labPaymentTerm.Text = "付款方式";
            labPlaceOfDelivery.Text = "交货地";
            labPOD.Text = "卸货港";
            labQuantity.Text = "数量";
            labSales.Text = "揽货人";
            labSalesDepartment.Text = "揽货部门";
            labSalesType.Text = "揽货类型";
            labShipper.Text = "发货人";
            labTransportClause.Text = "运输条款";
            labReturnLocation.Text = "还柜地点";
            labType.Text = "业务类型";
            labWeight.Text = "重量";
            labDeliveryDate.Text = "交货日";

            chkIsTruck.Text = "拖车";
            chkIsWarehouse.Text = "仓储";
            chkHasContract.Text = "合约";

            labAgent.Text = "代理";
            labAgentOfCarrier.Text = "承运人";
            this.labBookinger.Text = "订舱";
            labFiler.Text = "文件";
            labState.Text = "状态";
            labOrderNo.Text = "订舱号";
            labSODate.Text = "确认日期";
            labVoyage.Text = "大船";
            labPreVoyage.Text = "驳船";
            labPlaceOfReceipt.Text = "收货地";

            this.labETD.Text = "离港日";
            this.labETA.Text = "到港日";
            this.lblPreETD.Text = "离港日";

            navBarBase.Caption = "基本信息";
            navBarDelegate.Caption = "委托信息";
            navBarOther.Caption = "其它信息";
            navBarFee.Caption = "费用信息";


            groupLocalService.Text = "本地服务";

            labShippingLine.Text = "航线";
            labCYClosingDate.Text = "截柜日";
            labDOCClosingDate.Text = "截文件日";
            this.labCloseWarehouse.Text = "截仓日";
            this.labWarehouse.Text = "仓库";
            labPOL2.Text = "装货港";
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
            colPOLName.Caption = "装货港";
            colPODName.Caption = "卸货港";
        }

        #endregion

        #endregion

        #region 新订舱单的特殊逻辑

        /// <summary>
        /// 
        /// </summary>
        void ReadyForNew()
        {
            DTBookingInfo newData = new DTBookingInfo();
            //newData.SalesID = LocalData.UserInfo.LoginID;
            //newData.SalesName = LocalData.UserInfo.LoginName;
            newData.DTOperationType = FCMOperationType.FCL;
            newData.BookingDate = newData.CreateDate = DateTime.Now;
            newData.BookingMode = FCMBookingMode.Fax;
            newData.State = DTOrderState.NewOrder;
            newData.AgentID = Guid.Empty;
            newData.BookingerID = LocalData.UserInfo.LoginID;
            newData.BookingerName = LocalData.UserInfo.LoginName;
            //newData.CargoDescription = new ICP.FCM.Common.ServiceInterface.DataObjects.CargoDescription();
            newData.IsContract = true;
            newData.IsValid = true;

            #region 设置默认值
            DataDictionaryList normalDictionary = null;
            //normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.PaymentTerm);
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

            this._DTBookingInfo = newData;

            _DTBookingInfo.HBLReleaseType = _DTBookingInfo.MBLReleaseType = FCMReleaseType.Unknown;

            // TODO: 这种Guard型的逻辑要在最开始的时候完成
            Utility.EnsureDefaultCompanyExists(this.userService);

            this._DTBookingInfo.CompanyID = LocalData.UserInfo.DefaultCompanyID;
            this._DTBookingInfo.CompanyName = LocalData.UserInfo.DefaultCompanyName;

            this.gvOrders.DoubleClick += new System.EventHandler(this.gvOrders_DoubleClick);
        }

        #endregion

        #region 复制订舱单时的逻辑

        /// <summary>
        /// 复制订舱单时的逻辑
        /// </summary>
        void PrepareForCopyExistOrder()
        {
            this._DTBookingInfo.ID = Guid.Empty;
            this._DTBookingInfo.No = string.Empty;
            this._DTBookingInfo.MBLNo = this._DTBookingInfo.HBLNo = string.Empty;
            this._DTBookingInfo.SalesID = LocalData.UserInfo.LoginID;
            this._DTBookingInfo.SalesName = LocalData.UserInfo.LoginName;
            this._DTBookingInfo.CreateDate = DateTime.Now;
            this._DTBookingInfo.OceanShippingOrderID = Guid.Empty;
            this._DTBookingInfo.OceanShippingOrderNo = string.Empty;
            this._DTBookingInfo.AgentID = Guid.Empty;
            this._DTBookingInfo.IsContract = false;
            this._DTBookingInfo.BookingerID = LocalData.UserInfo.LoginID;
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

            if (_DTBookingInfo.ShipperDescription == null)
            {
                _DTBookingInfo.ShipperDescription = new CustomerDescription();
            }

            if (_DTBookingInfo.ConsigneeDescription == null)
            {
                _DTBookingInfo.ConsigneeDescription = new CustomerDescription();
            }

            if (_DTBookingInfo.AgentDescription == null)
            {
                _DTBookingInfo.AgentDescription = new CustomerDescription();
            }

            if (_DTBookingInfo.BookingCustomerDescription == null)
            {
                _DTBookingInfo.BookingCustomerDescription = new CustomerDescription();
            }

            if (_DTBookingInfo.CargoDescription == null)
            {
                //_DTBookingInfo.CargoDescription = new CargoDescription();
            }
        }

        #region 初始化货物对象

        /// <summary>
        /// 初始化货物对象
        /// </summary>
        private void InitCargoObject()
        {
            if (this._DTBookingInfo.CargoType.HasValue
                && _DTBookingInfo.CargoDescription != null
                && _DTBookingInfo.CargoDescription.Cargo != null)
            {
                if (_DTBookingInfo.CargoDescription.Cargo is DangerousCargo)
                    cmbCargoType.EditValue = CargoType.Dangerous;
                else if (_DTBookingInfo.CargoDescription.Cargo is AwkwardCargo)
                    cmbCargoType.EditValue = CargoType.Awkward;
                else if (_DTBookingInfo.CargoDescription.Cargo is ReeferCargo)
                    cmbCargoType.EditValue = CargoType.Reefer;
                else if (_DTBookingInfo.CargoDescription.Cargo is DryCargo)
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
            //业务类型
            this.cmbType.ShowSelectedValue(this._DTBookingInfo.DTOperationType,
                EnumHelper.GetDescription<FCMOperationType>(this._DTBookingInfo.DTOperationType, LocalData.IsEnglish, true));
            //委托方式
            this.cmbBookingMode.ShowSelectedValue(this._DTBookingInfo.BookingMode,
                EnumHelper.GetDescription<FCMBookingMode>(this._DTBookingInfo.BookingMode, LocalData.IsEnglish));
            //操作口岸
            cmbCompany.ShowSelectedValue(this._DTBookingInfo.CompanyID, this._DTBookingInfo.CompanyName);
            //包装
            cmbQuantityUnit.ShowSelectedValue(this._DTBookingInfo.QuantityUnitID, this._DTBookingInfo.QuantityUnitName);
            if (Utility.GuidIsNullOrEmpty(this._DTBookingInfo.QuantityUnitID)
                && this.cmbQuantityUnit.EditValue != null)
            {

                this._DTBookingInfo.QuantityUnitID = new Guid(this.cmbQuantityUnit.EditValue.ToString());
            }
            //重量
            cmbWeightUnit.ShowSelectedValue(this._DTBookingInfo.WeightUnitID, this._DTBookingInfo.WeightUnitName);
            if (Utility.GuidIsNullOrEmpty(this._DTBookingInfo.WeightUnitID)
                && this.cmbWeightUnit.EditValue != null)
            {
                this._DTBookingInfo.WeightUnitID = new Guid(this.cmbWeightUnit.EditValue.ToString());
            }
            //体积
            cmbMeasurementUnit.ShowSelectedValue(this._DTBookingInfo.MeasurementUnitID, this._DTBookingInfo.MeasurementUnitName);
            if (Utility.GuidIsNullOrEmpty(this._DTBookingInfo.MeasurementUnitID)
                && cmbMeasurementUnit.EditValue != null)
            {
                this._DTBookingInfo.MeasurementUnitID = new Guid(this.cmbMeasurementUnit.EditValue.ToString());
            }
            this.mcmbSales.ShowSelectedValue(this._DTBookingInfo.SalesID, this._DTBookingInfo.SalesName);
            //揽货类型
            this.cmbSalesType.ShowSelectedValue(this._DTBookingInfo.SalesTypeID, this._DTBookingInfo.SalesTypeName);

            //揽货部门
            this.trsSalesDep.ShowSelectedValue(this._DTBookingInfo.SalesDepartmentID, this._DTBookingInfo.SalesDepartmentName);
            //3个付款方式
            cmbPaymentTerm.ShowSelectedValue(this._DTBookingInfo.PaymentTermID, this._DTBookingInfo.PaymentTermName);
            //航线
            cmbShippingLine.ShowSelectedValue(this._DTBookingInfo.ShippingLineID, this._DTBookingInfo.ShippingLineName);
            //运输条款
            this.cmbTransportClause.ShowSelectedValue(this._DTBookingInfo.TransportClauseID, this._DTBookingInfo.TransportClauseName);
            //船公司
            this.mcmbCarrier.ShowSelectedValue(this._DTBookingInfo.CarrierID, this._DTBookingInfo.CarrierName);

            //this.cmbMBLReleaseType.ShowSelectedValue(this._DTBookingInfo.MBLReleaseType, EnumHelper.GetDescription<ReleaseType>(this._DTBookingInfo.MBLReleaseType.Value, LocalData.IsEnglish));
            //this.cmbHBLReleaseType.ShowSelectedValue(this._DTBookingInfo.HBLReleaseType, EnumHelper.GetDescription<ReleaseType>(this._DTBookingInfo.HBLReleaseType.Value, LocalData.IsEnglish));

            //箱需求
            if (_DTBookingInfo.ContainerDescription != null)
            {
                this.containerDemandControl1.Text = _DTBookingInfo.ContainerDescription.ToString();
            }
            this.dxErrorProvider1.SetIconAlignment(containerDemandControl1.ErrorHost, ErrorIconAlignment.MiddleRight);

            //货物描述
            if (_DTBookingInfo.CargoDescription != null
                && _DTBookingInfo.CargoDescription.Cargo != null)
            {
                txtCargoDescription.Text = _DTBookingInfo.CargoDescription.Cargo.ToString(LocalData.IsEnglish);
            }

            this.orderFeeEditPart1.SetCompanyID(this._DTBookingInfo.CompanyID);

            this.mcmbFiler.ShowSelectedValue(this._DTBookingInfo.FilerId, this._DTBookingInfo.FilerName);

            //this.mcmbOverseasFiler.ShowSelectedValue(this._DTBookingInfo.OverSeasFilerID, this._DTBookingInfo.OverSeasFilerName);

            this.mcmbBookinger.ShowSelectedValue(this._DTBookingInfo.BookingerID, this._DTBookingInfo.BookingerName);

            if (this._DTBookingInfo.CargoType.HasValue)
            {
                this.cmbCargoType.ShowSelectedValue(this._DTBookingInfo.CargoType,
                    EnumHelper.GetDescription<CargoType>(this._DTBookingInfo.CargoType.Value, LocalData.IsEnglish));
            }
            if (this._DTBookingInfo.PreVoyageID != null)
            {
                this.stxtPreVoyage.ShowSelectedValue(this._DTBookingInfo.PreVoyageID, this._DTBookingInfo.PreVoyageName);
            }
            if (this._DTBookingInfo.VoyageID != null)
            {
                this.stxtVoyage.ShowSelectedValue(this._DTBookingInfo.VoyageID, this._DTBookingInfo.VoyageName);
            }
            this.stxtReturnLocation.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
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
                          Guid oldCustomerId = _DTBookingInfo.CustomerID;
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

                          stxtCustomer.EditValue = _DTBookingInfo.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                          stxtCustomer.Tag = _DTBookingInfo.CustomerID = new Guid(resultData[0].ToString());

                          if (oldCustomerId != Guid.Empty && _DTBookingInfo.CustomerID == oldCustomerId) return;

                          CustomerType customerType = (CustomerType)resultData[4];

                          CustomerChanged(customerType);


                      }, delegate
                      {
                          stxtCustomer.Text = _DTBookingInfo.CustomerName = string.Empty;
                          stxtCustomer.Tag = _DTBookingInfo.CustomerID = Guid.Empty;
                          stxtCustomer.ClosePopup();
                          CustomerChanged(null);
                      },
                      ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            //仓库
            this.dfService.Register(this.stxtWarehouse, CommonFinderConstants.CustoemrFinder,
                SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                this.GetConditionsForWarehouse,
                delegate(object inputSource, object[] resultData)
                {


                    stxtWarehouse.Tag = this._DTBookingInfo.WarehouseID = ConvGuid(resultData[0]);
                    stxtWarehouse.EditValue = this._DTBookingInfo.WarehouseName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate
                {
                    stxtWarehouse.Tag = this._DTBookingInfo.WarehouseID = null;
                    stxtWarehouse.EditValue = this._DTBookingInfo.WarehouseName = string.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            this.dfService.Register(this.stxtReturnLocation, CommonFinderConstants.CustoemrFinder,
                SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                GetConditionsForReturnLocation,
                delegate(object inputSouce, object[] resultData)
                {
                    stxtReturnLocation.Tag = this._DTBookingInfo.ReturnLocationID = ConvGuid(resultData[0]);
                    stxtReturnLocation.EditValue = this._DTBookingInfo.ReturnLocationName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate
                {
                    this.stxtReturnLocation.Tag = this._DTBookingInfo.ReturnLocationID = null;
                    this.stxtReturnLocation.EditValue = this._DTBookingInfo.ReturnLocationName = string.Empty;
                },
            ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            dfService.Register(stxtAgentOfCarrier, CommonFinderConstants.CustomerAgentOfCarrierFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
               delegate(object inputSource, object[] resultData)
               {
                   stxtAgentOfCarrier.Text = _DTBookingInfo.AgentOfCarrierName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                   stxtAgentOfCarrier.Tag = _DTBookingInfo.AgentOfCarrierID = new Guid(resultData[0].ToString());
               }, Guid.Empty, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            #endregion

            #region 订舱客户
            dfService.Register(stxtBookingCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.CustomerResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          Guid oldBookingCustomerID = _DTBookingInfo.BookingCustomerID;
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

                          stxtBookingCustomer.Tag = _DTBookingInfo.BookingCustomerID = new Guid(resultData[0].ToString());
                          stxtBookingCustomer.Text = _DTBookingInfo.BookingCustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();

                      }, delegate
                      {
                          stxtBookingCustomer.Tag = _DTBookingInfo.BookingCustomerID = Guid.Empty;
                          stxtBookingCustomer.Text = _DTBookingInfo.BookingCustomerName = string.Empty;
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
               _DTBookingInfo.ShipperDescription,
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
                _DTBookingInfo.ConsigneeDescription,
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
            //    _DTBookingInfo.BookingCustomerDescription,
            //    ICPCommUIHelperService,
            //    LocalData.IsEnglish);
            //    bookingCustomerPartyBridge.Init();
            //});

            //stxtBookingCustomer.OnOk += new EventHandler(stxtBookingCustomer_OnOk);

            #endregion

            #region Port

            pfbPlaceOfReceipt = new LocationFinderBridge(this.stxtPlaceOfReceipt, this.dfService, LocalData.IsEnglish);
            pfbPlaceOfReceipt.ValueChanged += new EventHandler(pfbPlaceOfReceipt_ValueChanged);
            pfbPlaceOfReceipt.Cleard += new EventHandler(pfbPlaceOfReceipt_Cleard);
            PortFinderBridge pfbPOL = new PortFinderBridge(this.stxtPOL, this.dfService, LocalData.IsEnglish);

            PortFinderBridge pfbPOD = new PortFinderBridge(this.stxtPOD, this.dfService, LocalData.IsEnglish);
            pfbPOD.Cleared += new EventHandler(pfbPOD_Cleared);

            LocationFinderBridge pfbPlaceOfDelivery = new LocationFinderBridge(this.stxtPlaceOfDelivery, this.dfService, LocalData.IsEnglish);


            #endregion

            #region Voyage

            //驳船 搜索的默认条件为 装货港=当前收货地and卸货港=当前装货港
            //dfService.Register(stxtPreVoyage,
            //    CommonFinderConstants.VesselVoyageFinder,
            //    SearchFieldConstants.VesselVoyage,
            //    SearchFieldConstants.VesselResultValue,
            //    this.GetConditionsForSearchPreVoyage,
            //    delegate(object inputSource, object[] resultData)
            //    {
            //        stxtPreVoyage.Text = _DTBookingInfo.PreVoyageName = resultData[1].ToString() + "/" + resultData[2].ToString();
            //        stxtPreVoyage.Tag = _DTBookingInfo.PreVoyageID = new Guid(resultData[0].ToString());

            //        PreVoyageChanged();
            //    },
            //    delegate
            //    {
            //        stxtPreVoyage.Text = _DTBookingInfo.PreVoyageName = string.Empty;
            //        stxtPreVoyage.Tag = _DTBookingInfo.PreVoyageID = null;

            //        PreVoyageChanged();
            //    },
            //    ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            ////大船 筛选：装货港=当前装货港and卸货港=当前卸货港
            //dfService.Register(stxtVoyage,
            //    CommonFinderConstants.VesselVoyageFinder,
            //    SearchFieldConstants.VesselVoyage,
            //    SearchFieldConstants.VesselResultValue,
            //    this.GetConditionsForSearchVoyage,
            //    delegate(object inputSource, object[] resultData)
            //    {
            //        stxtVoyage.Text = _DTBookingInfo.VoyageName = resultData[1].ToString() + "/" + resultData[2].ToString();
            //        stxtVoyage.Tag = _DTBookingInfo.VoyageID = new Guid(resultData[0].ToString());

            //        VoyageChanged();

            //    },
            //    delegate
            //    {
            //        stxtVoyage.Text = _DTBookingInfo.VoyageName = string.Empty;
            //        stxtVoyage.Tag = _DTBookingInfo.VoyageID = null;

            //        VoyageChanged();
            //    },
            //    ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            #endregion
        }

        private Guid ConvGuid(object value)
        {
            Guid guid = Guid.Empty;

            if (value != null)
            {
                guid = new Guid(value.ToString());
            }

            return guid;
        }

        void stxtBookingCustomer_OnOk(object sender, EventArgs e)
        {
            if (stxtBookingCustomer.CustomerDescription != null)
            {
                _DTBookingInfo.BookingCustomerDescription = stxtBookingCustomer.CustomerDescription;
            }
        }

        void stxtConsignee_OnOk(object sender, EventArgs e)
        {
            if (stxtConsignee.CustomerDescription != null)
            {
                _DTBookingInfo.ConsigneeDescription = stxtConsignee.CustomerDescription;
            }
        }

        void stxtShipper_OnOk(object sender, EventArgs e)
        {
            if (stxtShipper.CustomerDescription != null)
            {
                _DTBookingInfo.ShipperDescription = stxtShipper.CustomerDescription;
            }
        }

        void pfbPOD_Cleared(object sender, EventArgs e)
        {
            this.ClearVoyage();
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
                polId = (Guid)this.stxtPlaceOfReceipt.Tag;
            }
            catch
            {
                throw new Exception(LocalData.IsEnglish ? "Please select P.O.R. at first." : "请先选择收货地！");
            }

            try
            {
                podId = (Guid)this.stxtPOL.Tag;
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
            conditions.AddWithValue("POLID", this.stxtPlaceOfReceipt.Tag, false);
            conditions.AddWithValue("POLName", this.stxtPlaceOfReceipt.Text, false);
            conditions.AddWithValue("PODID", this.stxtPOL.Tag, false);
            conditions.AddWithValue("PODName", this.stxtPOL.Text, false);
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
                polId = (Guid)this.stxtPOL.Tag;
            }
            catch
            {
                throw new Exception(LocalData.IsEnglish ? "Please select POL at first." : "请先选择装货港！");
            }

            try
            {
                podId = (Guid)this.stxtPOD.Tag;
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
            conditions.AddWithValue("POLID", this.stxtPOL.Tag, false);
            conditions.AddWithValue("POLName", this.stxtPOL.Text, false);
            conditions.AddWithValue("PODID", this.stxtPOD.Tag, false);
            conditions.AddWithValue("PODName", this.stxtPOD.Text, false);

            return conditions;
        }

        void ResetDescription()
        {
            if (this.shipperBridge != null)
            {
                this.shipperBridge.SetCustomerDescription(this._DTBookingInfo.ShipperDescription);
            }

            if (this.consigneeBridge != null)
            {
                this.consigneeBridge.SetCustomerDescription(this._DTBookingInfo.ConsigneeDescription);
            }

            if (this.bookingCustomerPartyBridge != null)
            {
                this.bookingCustomerPartyBridge.SetCustomerDescription(this._DTBookingInfo.BookingCustomerDescription);
            }
        }

        #endregion
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
        #region 延迟加载的数据源

        List<DataDictionaryList> _weightUnits;

        void InitalComboxes()
        {
            //List<DataDictionaryList> salesTypes = ICPCommUIHelper.SetCmbDataDictionary(cmbSalesType, DataDictionaryType.SalesType);

            //包装
            //Utility.SetEnterToExecuteOnec(cmbQuantityUnit, delegate
            //{
            ICPCommUIHelperService.SetCmbDataDictionary(cmbQuantityUnit, DataDictionaryType.QuantityUnit);
            //});

            //重量
            //Utility.SetEnterToExecuteOnec(cmbWeightUnit, delegate
            //{
            _weightUnits = ICPCommUIHelperService.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit);
            //});

            ////体积
            //Utility.SetEnterToExecuteOnec(cmbMeasurementUnit, delegate
            //{
            List<DataDictionaryList> volUnitss = ICPCommUIHelperService.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit);
            //});


            //箱型
            if (ttService != null)
            {
                List<ICP.Common.ServiceInterface.DataObjects.ContainerList> containerList = ttService.GetContainerList(string.Empty, true, 0);

                foreach (ICP.Common.ServiceInterface.DataObjects.ContainerList container in containerList)
                {
                    this.cmbConType.Properties.Items.Add(new ImageComboBoxItem(container.Code, container.ID));
                }
            }
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
                ICPCommUIHelperService.BindCompanyByUser(cmbCompany, false);

                if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.CompanyID) && LocalData.UserInfo.UserOrganizationList.Count() > 0)
                {
                    _DTBookingInfo.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                }

                cmbCompany.SelectedIndexChanged += delegate
                {
                    CompanyChanged();
                };
            });

            #region Agent
            if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.AgentID) == false)
            {
                List<CustomerList> agentCustomers = new List<CustomerList>();
                CustomerList agentCustomer = new CustomerList();
                agentCustomer.CName = agentCustomer.EName = _DTBookingInfo.AgentName;
                agentCustomer.ID = _DTBookingInfo.AgentID.Value;
                agentCustomers.Insert(0, agentCustomer);
                SetAgentSource(agentCustomers);
            }
            Utility.SetEnterToExecuteOnec(stxtAgent, delegate
            {
                SetAgentSourceByCompanyID(_DTBookingInfo.CompanyID);
                stxtAgent.EditValueChanged += delegate
                {
                    if (stxtAgent.EditValue != null && stxtAgent.EditValue.ToString().Length > 0)
                    {
                        Guid id = new Guid(stxtAgent.EditValue.ToString());

                        ICPCommUIHelperService.SetCustomerDesByID(id, _DTBookingInfo.AgentDescription);
                        stxtAgent.CustomerDescription = _DTBookingInfo.AgentDescription;
                        stxtAgent.EditValueChanged -= new EventHandler(stxtAgent_EditValueChanged);
                        stxtAgent.EditValueChanged += new EventHandler(stxtAgent_EditValueChanged);
                    }
                };
            });
            #endregion


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
            //航线
            Utility.SetEnterToExecuteOnec(cmbShippingLine, delegate
            {
                List<ShippingLineList> shippingLines = ICPCommUIHelperService.SetCmbShippingLine(cmbShippingLine);
            });

            //船公司
            Utility.SetEnterToExecuteOnec(mcmbCarrier, delegate
            {
                ICPCommUIHelperService.BindCustomerList(mcmbCarrier, CustomerType.Carrier);
            });

            //运输条款
            Utility.SetEnterToExecuteOnec(cmbTransportClause, delegate
            {
                List<TransportClauseList> transportClauseList = ICPCommUIHelperService.SetCmbTransportClause(cmbTransportClause);
                //if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.TransportClauseID))
                //{
                //    _DTBookingInfo.TransportClauseID = transportClauseList[0].ID;
                //}
            });


            Utility.SetEnterToExecuteOnec(this.mcmbSales, delegate
            {
                ICPCommUIHelperService.SetMcmbUsersByCompanys(mcmbSales);
            });
            //业务类型
            Utility.SetEnterToExecuteOnec(this.cmbType, delegate
            {
                ICPCommUIHelperService.SetComboxByEnum<FCMOperationType>(this.cmbType, true);
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

            mcmbFiler.Enter += new EventHandler(mcmFiler_Click);
            this.mcmbBookinger.Enter += new EventHandler(mcmbBookinger_Click);
        }

        /// <summary>
        /// 填充“订舱”的用户列表供选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mcmbBookinger_Click(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (this.cmbCompany.EditValue != null && !string.IsNullOrEmpty(this.cmbCompany.EditValue.ToString()))
            {
                depID = new Guid(this.cmbCompany.EditValue.ToString());
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
            if (this.cmbCompany.EditValue != null && !string.IsNullOrEmpty(this.cmbCompany.EditValue.ToString()))
            {
                depID = new Guid(this.cmbCompany.EditValue.ToString());
            }
            ICPCommUIHelperService.SetComboxUsersByRole(mcmbFiler, depID, "文件", true);

        }

        /// <summary>
        /// 填充“海外部客服”的用户列表供选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mcmbOverseasFiler_Enter(object sender, EventArgs e)
        {

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
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControl1_SelectedPageChanged);
            //订舱客户,如果贸易条款为CIF，那么就为客户，否则为空白


            cmbTransportClause.SelectedIndexChanged += delegate
            {
                if (this._shown)
                {
                    this._DTBookingInfo.TransportClauseName = this.cmbTransportClause.Text;
                    SetPlaceOfDeliveryByTransportClause();
                    SetFinalDestinationByTransportClause();
                }
            };

            if (_DTBookingInfo.ID == Guid.Empty)
            {

                this.stxtCustomer.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.stxtCustomer_ButtonClick);
            }

            this.cmbOrderNo.Enter += new EventHandler(cmbOrderNo_Enter);
            this.cmbOrderNo.LostFocus += new EventHandler(cmbOrderNo_SelectedIndexChanged);

            this.cmbCargoType.Click += new EventHandler(cmbCargoType_Enter);
            this.cmbCargoType.SelectedIndexChanged += new EventHandler(cmbCargoType_EditValueChanged);
            this.mcmbSales.SelectedRow += new EventHandler(mcmbSales_SelectedRow);

            this.stxtPOL.TextChanged += new EventHandler(stxtPOL_TextChanged);
            this.stxtPOD.TextChanged += new EventHandler(stxtPOD_TextChanged);
            this.stxtPlaceOfDelivery.TextChanged += new EventHandler(stxtPlaceOfDelivery_TextChanged);
            this.trsSalesDep.Enter += new EventHandler(trsSalesDep_Enter);
            this.trsSalesDep.Selected += new EventHandler(trsSalesDep_Selected);
            this.stxtBookingCustomer.TextChanged += new EventHandler(stxtBookingCustomer_TextChanged);

            this.containerDemandControl1.TextChanged += new EventHandler(containerDemandControl1_TextChanged);
        }

        void containerDemandControl1_TextChanged(object sender, EventArgs e)
        {
            //_DTBookingInfo.ContainerDescription = new ContainerDescription(this.containerDemandControl1.Text.Trim());
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
                this._DTBookingInfo.POLName = this.stxtPOL.Text;
                this.ClearPreVoyage();
                this.ClearVoyage();
            }
        }

        void stxtPOD_TextChanged(object sender, EventArgs e)
        {
            if (this._shown && !Utility.GuidIsNullOrEmpty(this._DTBookingInfo.PODID))
            {
                this._DTBookingInfo.PODName = this.stxtPOD.Text;
                this.SetPlaceOfDeliveryByTransportClause();
                this.ClearVoyage();
            }
        }

        /// <summary>
        /// 为了确保驳船的港口是符合要求的
        /// </summary>
        void ClearPreVoyage()
        {
            this.stxtPreVoyage.Tag = this._DTBookingInfo.PreVoyageID = Guid.Empty;
            this.stxtPreVoyage.Text = this._DTBookingInfo.PreVoyageName = string.Empty;

            this.PreVoyageChanged();
            this.VoyageChanged();
        }

        /// <summary>
        /// 数据时选大船/驳船的时候得来的，所以清空大船/驳船的时候也要清空数据，以保证一致性;
        /// </summary>
        void ClearFourDays()
        {
            dteDOCClosingDate.EditValue = _DTBookingInfo.DOCClosingDate = null;
            dteCYClosingDate.EditValue = _DTBookingInfo.CYClosingDate = null;
            dteETD.EditValue = _DTBookingInfo.ETD = null;
        }

        /// <summary>
        /// 为了确保大船的港口是符合要求的
        /// </summary>
        void ClearVoyage()
        {
            this.stxtVoyage.Tag = this._DTBookingInfo.VoyageID = Guid.Empty;
            this.stxtVoyage.Text = this._DTBookingInfo.VoyageName = string.Empty;

            this.VoyageChanged();
            this.PreVoyageChanged();
        }

        /// <summary>
        /// 注册界面控件之间联动的事件并立即执行一次
        /// </summary>
        void RegisterRelativeEventsAndRunOnce()
        {
            this.cmbType.SelectedIndexChanged += new EventHandler(cmbType_SelectedIndexChanged);

            this.cmbOrderNo.TextChanged += new EventHandler(cmbOrderNo_TextChanged);
            this.chkHasContract.CheckedChanged += new System.EventHandler(this.chkHasContract_CheckedChanged);
            this.txtContractNo.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtContractNo_ButtonClick);
            this.cmbSalesType.SelectedIndexChanged += new EventHandler(cmbSalesType_SelectedIndexChanged);

            this.RunAtOnce();
        }

        void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetContainerDemandByBookingType();

            this.chkIsTruck.Enabled = (this._DTBookingInfo.DTOperationType == FCMOperationType.FCL
                || this._DTBookingInfo.DTOperationType == FCMOperationType.LCL);

            if (!this.chkIsTruck.Enabled)
            {
                this.chkIsTruck.Checked = this._DTBookingInfo.IsTruck = false;
            }
        }

        void stxtBookingCustomer_TextChanged(object sender, EventArgs e)
        {
            if (this._shown)
            {
                this._DTBookingInfo.BookingCustomerName = this.stxtBookingCustomer.Text;
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
            if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.SalesID) == false)
            {
                userOrganizationTreeLists = userService.GetUserCompanyList(_DTBookingInfo.SalesID.Value, null);
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
                this._DTBookingInfo.PlaceOfDeliveryName = this.stxtPlaceOfDelivery.Text;
                this.SetFinalDestinationByTransportClause();
                this.SetAgetnEnabledByPlaceOfDeliveryAndCompany();
            }
        }

        void cmbTradeTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._shown)
            {
                this.SetBookingCustomerByCustomerAndTradeTerm();
                this.SetConsigneeByCustomerAndTradeTerm();
                this.SetShipperByBookingCustomerAndTradeTerm();
            }
        }

        void cmbSalesType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this._shown)
            //{
            this._DTBookingInfo.SalesTypeName = this.cmbSalesType.Text;
            if (this._DTBookingInfo.SalesTypeName.Contains("指定货"))//TODO: 英文怎么办?
            {

            }
            else
            {

            }
            //}
        }

        #region 主要是设置控件的颜色、可使用性等属性

        /// <summary>
        /// 总调用处，会把其它方法都执行一遍
        /// </summary>
        void RunAtOnce()
        {
            this.cmbType_SelectedIndexChanged(null, null);
            this.cmbOrderNo_TextChanged(this, null);
            this.cmbSalesType_SelectedIndexChanged(null, null);
            this.chkHasContract_CheckedChanged(null, null);

            #region 根据数据 设置 控件可操作

            if (_DTBookingInfo.ID != Guid.Empty)
            {
                // 如果订单.订舱单.SO号已产生，则不允许更改业务类型，否则允许更改业务类型。
                if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.OceanShippingOrderID) == false)
                    cmbType.Enabled = false;
                else
                    cmbType.Enabled = true;

                SetContainerDemandByBookingType();
            }

            SetHBLEnabledByIsOnlyMBL(_DTBookingInfo.IsOnlyMBL);
            SetContractBoxByHasContract(_DTBookingInfo.IsContract);

            #endregion

            this.RefreshBarEnabled();
        }

        #region 有了订舱号后，船公司和确认日期要求必填

        void cmbOrderNo_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cmbOrderNo.Text))
            {
                mcmbCarrier.SpecifiedBackColor = Color.White;
                this.dteSODate.BackColor = Color.White;
                // this.stxtReturnLocation.BackColor = Color.White;
                this.stxtAgentOfCarrier.BackColor = Color.White;
            }
            else
            {
                mcmbCarrier.SpecifiedBackColor = SystemColors.Info;
                this.dteSODate.BackColor = SystemColors.Info;
                //this.stxtReturnLocation.BackColor = SystemColors.Info;
                this.stxtAgentOfCarrier.BackColor = SystemColors.Info;
            }
        }

        #endregion

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
        }
        /// <summary>
        ///IsOnlyMBL如果为选择状态，则HBL相关输入项设为启用状态，否则设为禁用状态
        /// </summary>
        private void SetHBLEnabledByIsOnlyMBL(bool isOnlyMBL)
        {
            if (isOnlyMBL)
            {
                _DTBookingInfo.HBLPaymentTermID = null;
                _DTBookingInfo.HBLReleaseType = FCMReleaseType.Unknown;
                _DTBookingInfo.HBLRequirements = string.Empty;
            }
            else
            {
            }
        }

        #endregion

        #region 如果业务类型不是整箱，那么箱描述就不可编辑，否则可编辑

        /// <summary>
        /// 如果业务类型不是整箱，那么箱描述就不可编辑，否则可编辑
        /// </summary>
        private void SetContainerDemandByBookingType()
        {
            if (_DTBookingInfo.DTOperationType == FCMOperationType.FCL)
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

        /// <summary>
        /// 如果业务类型不是整箱，那么箱描述就不可编辑，否则可编辑
        /// </summary>
        private void SetLocalServiceByBookingType()
        {
            if (_DTBookingInfo.DTOperationType == FCMOperationType.LCL)
                chkIsTruck.Enabled = true;
            else
            {
                chkIsTruck.Enabled = false;
                chkIsTruck.Checked = false;
            }
        }

        #endregion

        #region 刷新工具栏按钮的可使用性

        void RefreshBarEnabled()
        {
            this.barAuditAndSave.Enabled = this._DTBookingInfo.State == DTOrderState.NewOrder;

            if (_DTBookingInfo.ID == Guid.Empty)
            {
                barReject.Enabled = barE_Booking.Enabled = false;
                cmbType.Enabled = true;

                this.barTruck.Enabled = false;

                this.barApplyAgent.Enabled = false;
                this.barRefresh.Enabled = false;

                this.barSubPrint.Enabled = false;
                this.barPrintBookingConfirm.Enabled = false;
                this.barPrintInWarehouse.Enabled = false;
                this.barPrintOrder.Enabled = false;
            }
            else
            {
                barReject.Enabled = _DTBookingInfo.State == DTOrderState.NewOrder;

                if (string.IsNullOrEmpty(this._DTBookingInfo.OceanShippingOrderNo))
                {
                    barE_Booking.Enabled = false;
                    this.barTruck.Enabled = false;
                    cmbType.Enabled = true;
                }
                else
                {
                    cmbType.Enabled = false;
                    barTruck.Enabled = this._DTBookingInfo.DTOperationType == FCMOperationType.FCL
                        || this._DTBookingInfo.DTOperationType == FCMOperationType.LCL;
                    this.barE_Booking.Enabled = false;// oeService.IsCarrierInCompanyEdiBooking(this._DTBookingInfo.CarrierID, this._DTBookingInfo.CompanyID);
                }

                this.barRefresh.Enabled = true;

                if (string.IsNullOrEmpty(this._DTBookingInfo.OceanShippingOrderNo))
                {
                    this.barSubPrint.Enabled = false;
                    this.barPrintBookingConfirm.Enabled = false;
                    this.barPrintInWarehouse.Enabled = false;
                    this.barPrintOrder.Enabled = false;
                }
                else
                {
                    this.barSubPrint.Enabled = true;
                    this.barPrintBookingConfirm.Enabled = true;
                    this.barPrintInWarehouse.Enabled = true;
                    this.barPrintOrder.Enabled = true;
                }
            }

            this.txtState.Text = EnumHelper.GetDescription<DTOrderState>(this._DTBookingInfo.State, LocalData.IsEnglish);
        }

        #endregion

        #endregion

        #region 控件联动

        #region 数据变动填充控件默认值 客户变了就刷新揽货方式等逻辑

        #region Port And Voyage

        /// <summary>
        /// 值改变失去焦点后，需要刷新到港日，如果并且驳船为空，需要刷新离港日、截柜日、截关日、截文件日
        /// </summary>
        private void VoyageChanged()
        {
            //if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.VoyageID))
            //{
            //    this.ClearFourDays(); ;
            //    dteETA.EditValue = _DTBookingInfo.ETA = null;
            //    return;
            //}

            //VoyageInfo voyageInfo = tfService.GetVoyageInfo(_DTBookingInfo.VoyageID.Value);
            //dteETA.EditValue = _DTBookingInfo.ETA = voyageInfo.ETA;

            //if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.PreVoyageID))
            //{
            //    dteDOCClosingDate.EditValue = _DTBookingInfo.DOCClosingDate = voyageInfo.DOCClosingDate;
            //    dteCYClosingDate.EditValue = _DTBookingInfo.CYClosingDate = voyageInfo.CYClosingDate;
            //    dteETD.EditValue = _DTBookingInfo.ETD = voyageInfo.ETD;
            //}
        }

        /// <summary>
        /// 驳船改变,填充ETA， 截柜日，截关日,截文件日
        /// </summary>
        private void PreVoyageChanged()
        {
            //if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.PreVoyageID))
            //{
            //    this.ClearFourDays();
            //    return;
            //}

            //VoyageInfo voyageInfo = tfService.GetVoyageInfo(_DTBookingInfo.PreVoyageID.Value);
            //dteDOCClosingDate.EditValue = _DTBookingInfo.DOCClosingDate = voyageInfo.DOCClosingDate;
            //dteCYClosingDate.EditValue = _DTBookingInfo.CYClosingDate = voyageInfo.CYClosingDate;
            //dteETD.EditValue = _DTBookingInfo.ETD = voyageInfo.ETD;
        }

        /// <summary>
        /// 交货地 如果目的港运输条款<>Door，那么就为卸货港
        /// </summary>
        private void SetPlaceOfDeliveryByTransportClause()
        {
            if (!Utility.GuidIsNullOrEmpty(this._DTBookingInfo.PlaceOfDeliveryID)
                || Utility.GuidIsNullOrEmpty(this._DTBookingInfo.TransportClauseID)) return;

            if (_DTBookingInfo.TransportClauseName.Contains("-DOOR") == false)
            {
                stxtPlaceOfDelivery.Tag = _DTBookingInfo.PlaceOfDeliveryID = _DTBookingInfo.PODID;
                stxtPlaceOfDelivery.Text = _DTBookingInfo.PlaceOfDeliveryName = _DTBookingInfo.PODName;
            }
        }

        /// <summary>
        /// 最终目的地 如果目的港运输条款<>Door，那么就为卸货港
        /// </summary>
        private void SetFinalDestinationByTransportClause()
        {
            if (!Utility.GuidIsNullOrEmpty(_DTBookingInfo.FinalDestinationID)
                || Utility.GuidIsNullOrEmpty(this._DTBookingInfo.TransportClauseID)) return;

            if (_DTBookingInfo.TransportClauseName.Contains("-DOOR") == false)
            {

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
            if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.SalesID) == false)
            {
                userOrganizationTreeLists = userService.GetUserOrganizationTreeList(_DTBookingInfo.SalesID.Value);

                UserOrganizationTreeList orginazation = userOrganizationTreeLists.Find(o => o.IsDefault);
                if (orginazation != null)
                {
                    this.trsSalesDep.ShowSelectedValue(orginazation.ID, LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName);
                    _DTBookingInfo.SalesDepartmentID = orginazation.ID;
                    _DTBookingInfo.SalesDepartmentName = LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName;
                }
                else
                {
                    this.trsSalesDep.ShowSelectedValue(Guid.Empty, string.Empty);
                    _DTBookingInfo.SalesDepartmentID = Guid.Empty;
                    _DTBookingInfo.SalesDepartmentName = string.Empty;
                }
            }
        }

        void cmbSalesDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbSalesDepartment.EditValue == null || cmbSalesDepartment.EditValue.ToString() == string.Empty) return;
            //Guid salesDepartmentID = new Guid(cmbSalesDepartment.EditValue.ToString());
            //SetOperatorBySalesDepartmentID(salesDepartmentID);
        }

        /// <summary>
        /// 清空Sales后,自动清空Sales部门.操作
        /// </summary>
        private void ClearSalesDepartment()
        {
            //cmbSalesDepartment.Properties.Items.Clear();
            _DTBookingInfo.SalesDepartmentID = Guid.Empty;
            //cmbOperator.Properties.Items.Clear();
            _DTBookingInfo.BookingerID = null;
        }

        #endregion

        #region Other

        /// <summary>
        /// 根据公司和客户设置揽货方式
        /// </summary>
        private void SetSalesTypeByCustomerAndCompany()
        {
            if (_DTBookingInfo.CompanyID != Guid.Empty && _DTBookingInfo.CustomerID != Guid.Empty)
            {
                DataDictionaryInfo salesType = oeService.GetSalesType(_DTBookingInfo.CustomerID, _DTBookingInfo.CompanyID, LocalData.IsEnglish);
                if (salesType != null)
                {
                    _DTBookingInfo.SalesTypeID = salesType.ID;
                    _DTBookingInfo.SalesTypeName = LocalData.IsEnglish ? salesType.EName : salesType.CName;

                    this.cmbSalesType.ShowSelectedValue(_DTBookingInfo.SalesTypeID, _DTBookingInfo.SalesTypeName);
                }
            }
        }

        /// <summary>
        /// 根据操作口岸ID设置操作和文件栏的数据源
        /// </summary>
        private void SetOperatorByCompany()
        {
            if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.CompanyID))
            {
                //mcmbFiler.ClearItems();
                //mcmbBookiner.ClearItems();
            }
            else
            {
                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                List<UserList> operators = userService.GetUnderlingUserList(new Guid[] { _DTBookingInfo.CompanyID }, new string[] { "订舱" }, null, true);
                List<UserList> filers = userService.GetUnderlingUserList(new Guid[] { _DTBookingInfo.CompanyID }, new string[] { "文件" }, null, true);
                List<UserList> overSeasFilers = userService.GetUnderlingUserList(new Guid[] { _DTBookingInfo.CompanyID }, new string[] { "海外部客服" }, null, true);

                //mcmbBookiner.InitSource<UserList>(operators, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
                //mcmbFiler.InitSource<UserList>(filers, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            }
        }

        #endregion

        #region 设置默认海外部客服

        /// <summary>
        /// 当前客户最近业务所对应的海外部客服or 当前客户为新客户and当前揽货人最近业务所对应的海外部客服
        /// </summary>
        void SetDefaultOverseasFiler()
        {
            //List<UserInfo> users = this.oeService.GetOverseasFilersList(this._DTBookingInfo.CustomerID, this._DTBookingInfo.SalesID,
            //    DateTime.Now.AddDays(-30), DateTime.Now, 1);

            //if (users.Count > 0)
            //{
            //    this.mcmbOverseasFiler.ShowSelectedValue(users[0].ID, LocalData.IsEnglish ? users[0].EName : users[0].CName);
            //}
        }

        /// <summary>
        /// 当前客户最近业务所对应的文件or 当前客户为新客户and当前揽货人最近业务所对应的文件
        /// </summary>
        void SetDefaultFiler()
        {
            List<UserInfo> users = this.oeService.GetFilersList(this._DTBookingInfo.CustomerID, this._DTBookingInfo.SalesID,
                   DateTime.Now.AddDays(-30), DateTime.Now, 1);

            if (users.Count > 0)
            {
                this.mcmbFiler.ShowSelectedValue(users[0].ID, LocalData.IsEnglish ? users[0].EName : users[0].CName);
            }
        }

        /// <summary>
        /// TODO: 和FSD作者仔细核对这个逻辑？
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mcmbSales_SelectedRow(object sender, EventArgs e)
        {
            SetDefaultOverseasFiler();
            SetDefaultFiler();

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
            SetAgentSourceByCompanyID(_DTBookingInfo.CompanyID);
            SetAgetnEnabledByPlaceOfDeliveryAndCompany();

            this.orderFeeEditPart1.SetCompanyID(this._DTBookingInfo.CompanyID);
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

            SetAgentSourceByCompanyID(_DTBookingInfo.CompanyID);
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
                || Utility.GuidIsNullOrEmpty(_DTBookingInfo.CompanyID)
                || Utility.GuidIsNullOrEmpty(_DTBookingInfo.CustomerID))
            {
                return;
            }

            if (customerType.Value == CustomerType.Forwarding
                && !oeService.IsCustomerAndCompanySameCountry(_DTBookingInfo.CustomerID, _DTBookingInfo.CompanyID, LocalData.IsEnglish))
            {
                stxtAgent.Text = _DTBookingInfo.AgentName = _DTBookingInfo.CustomerName;
                stxtAgent.EditValue = _DTBookingInfo.AgentID = _DTBookingInfo.CustomerID;
                ICPCommUIHelperService.SetCustomerDesByID(_DTBookingInfo.AgentID, _DTBookingInfo.AgentDescription);
            }
        }

        /// <summary>
        /// 如果交货地所在的国家不存在于公司配置中客户对应的国家，那么就为只读，否则可以输入
        /// </summary>
        private void SetAgetnEnabledByPlaceOfDeliveryAndCompany()
        {
            if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.PlaceOfDeliveryID))
            {
                return;
            }

            // TODO:“指定货”目前在数据库的字典表里面还没有对应的英文
            if (
                    (
                        string.IsNullOrEmpty(this._DTBookingInfo.SalesTypeName) == false &&
                        (this._DTBookingInfo.SalesTypeName.Contains("指定货") || this._DTBookingInfo.SalesTypeName.ToUpper().Contains("AGENT"))
                    )
                || oeService.IsPortCountryExistCompanyConfig(_DTBookingInfo.PlaceOfDeliveryID, this._DTBookingInfo.CompanyID, LocalData.IsEnglish))
            {
                stxtAgent.Enabled = true;
            }
            else
            {
                stxtAgent.Enabled = false;
                stxtAgent.Text = _DTBookingInfo.AgentName = string.Empty;
                stxtAgent.EditValue = _DTBookingInfo.AgentID = Guid.Empty;
                _DTBookingInfo.AgentDescription = new CustomerDescription();
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

            List<CustomerList> agentCustomers = configureService.GetCompanyAgentList(_DTBookingInfo.CompanyID, true);
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
            if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.AgentID))
            {
                stxtAgent.EditValue = _DTBookingInfo.AgentID = agentCustomers[0].ID;
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
                stxtAgent.CustomerDescription = _DTBookingInfo.AgentDescription = new CustomerDescription();
            }
            else
            {
                ICPCommUIHelperService.SetCustomerDesByID(id, _DTBookingInfo.AgentDescription);
                stxtAgent.CustomerDescription = _DTBookingInfo.AgentDescription;
            }
        }

        #endregion


        /// <summary>
        /// 设置发货人 如果贸易条款为CIF，那么就为订舱客户
        /// </summary>
        private void SetShipperByBookingCustomerAndTradeTerm()
        {
            //if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.ShipperID) == false
            //    || Utility.GuidIsNullOrEmpty(this._DTBookingInfo.TradeTermID))
            //{
            //    return;
            //}

            //if (_DTBookingInfo.TradeTermName.Contains("CIF"))
            //{
            //    stxtShipper.Tag = _DTBookingInfo.ShipperID = _DTBookingInfo.BookingCustomerID;
            //    stxtShipper.Text = _DTBookingInfo.ShipperName = _DTBookingInfo.BookingCustomerName;
            //    ICPCommUIHelper.SetCustomerDesByID(_DTBookingInfo.ShipperID, _DTBookingInfo.ShipperDescription);
            //}
        }

        /// <summary>
        /// 收货人:如果贸易条款为FOB或EXWORK，那么就为客户
        /// </summary>
        private void SetConsigneeByCustomerAndTradeTerm()
        {
            if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.ConsigneeID) == false) return;

            if (this._DTBookingInfo.TradeTermName == "FOB" || this._DTBookingInfo.TradeTermName == "EXWORK")
            {
                this.stxtConsignee.Tag = this._DTBookingInfo.ConsigneeID = this._DTBookingInfo.CustomerID;
                this.stxtConsignee.Text = this._DTBookingInfo.ConsigneeName = this._DTBookingInfo.CustomerName;
                ICPCommUIHelperService.SetCustomerDesByID(_DTBookingInfo.ConsigneeID, _DTBookingInfo.ConsigneeDescription);
            }

            this.ResetDescription();
        }

        /// <summary>
        /// 设置订舱客户和发货（订舱客户:如果贸易条款为CIF，那么就为客户
        /// </summary>
        private void SetBookingCustomerByCustomerAndTradeTerm()
        {
            if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.BookingCustomerID) == false)
            {
                return;
            }

            if (!string.IsNullOrEmpty(_DTBookingInfo.TradeTermName)
                && _DTBookingInfo.TradeTermName.Contains("CIF"))
            {
                stxtBookingCustomer.Tag = _DTBookingInfo.BookingCustomerID = _DTBookingInfo.CustomerID;
                stxtBookingCustomer.Text = _DTBookingInfo.BookingCustomerName = _DTBookingInfo.CustomerName;
                ICPCommUIHelperService.SetCustomerDesByID(_DTBookingInfo.CustomerID, _DTBookingInfo.BookingCustomerDescription);
            }
        }

        /// <summary>
        /// 最近业务
        /// </summary>
        private void SetRecentlyOrderListByCustomerAndCompany()
        {
            if (_DTBookingInfo.ID != Guid.Empty || _DTBookingInfo.CompanyID == Guid.Empty || _DTBookingInfo.CustomerID == Guid.Empty)
            {
                bsRecentTenOrders.Clear();
            }
            else
            {
                bsRecentTenOrders.Clear();
                List<DTOrderList> orderList = oeService.GetRecentlyDTOrderList(_DTBookingInfo.CompanyID, _DTBookingInfo.CustomerID, 10, LocalData.IsEnglish);
                if (orderList != null && orderList.Count > 0)
                {
                    bsRecentTenOrders.DataSource = orderList;
                    stxtCustomer.ShowPopup();
                }
            }
        }

        #endregion

        #endregion

        #region 基础信息和PO的Tab之间切换时

        /// <summary>
        /// 控制了延迟加载PO的数据源
        /// </summary>
        void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {

        }

        #endregion

        #region 最近十票业务

        DTOrderList CurrentOrderList
        {
            get
            {
                if (bsRecentTenOrders.List == null || bsRecentTenOrders.Current == null) return null;
                return bsRecentTenOrders.Current as DTOrderList;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbOrderNo_Enter(object sender, EventArgs e)
        {
            List<ShippingOrderList> shippingOrderList = oeService.GetShippingOrderList(FCMOperationType.LCL
                                                                                              , _DTBookingInfo.POLID
                                                                                              , _DTBookingInfo.PODID
                                                                                              , _DTBookingInfo.PlaceOfDeliveryID
                                                                                              , LocalData.UserInfo.LoginID
                                                                                              , DateTime.Now.AddDays(-30).Date
                                                                                              , Utility.GetEndDate(DateTime.Now)
                                                                                              , 0
                                                                                              , LocalData.IsEnglish);

            if (shippingOrderList.Count > 0)
            {
                //Dictionary<Guid, Guid> tempListToAvoidRepeatRecords = new Dictionary<Guid, Guid>();

                this.cmbOrderNo.Properties.Items.Clear();
                foreach (var item in shippingOrderList)
                {
                    //if (!tempListToAvoidRepeatRecords.ContainsKey(item.ID))
                    //{
                    //tempListToAvoidRepeatRecords.Add(item.ID, item.ID);
                    cmbOrderNo.Properties.Items.Add(item);
                    //}
                }

                this.cmbOrderNo.Invalidate();
            }
        }


        /// <summary>
        /// 如果是选择已有订舱号失去焦点后，链接确认日期、承运人、船公司、合约号、驳船、大船、离港日、到港日、截关日、截柜日、截文件日、还柜地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._DTBookingInfo.IsDirty)
            {
                if (this.cmbOrderNo.SelectedItem != null)
                {
                    ShippingOrderList list = this.cmbOrderNo.SelectedItem as ShippingOrderList;
                    if (list != null)
                    {
                        this._DTBookingInfo.SODate = list.SODate;
                        this._DTBookingInfo.CarrierID = list.CarrierID;
                        this._DTBookingInfo.CarrierName = list.CarrierName;
                        if (list.AgentofcarrierID.HasValue)
                        {
                            this._DTBookingInfo.AgentOfCarrierID = list.AgentofcarrierID.Value;
                        }
                        this._DTBookingInfo.AgentOfCarrierName = list.AgentOfCarrierName;
                        this._DTBookingInfo.ContractID = list.FreightRateID;
                        this._DTBookingInfo.ContractNo = list.ContractNo;
                        this._DTBookingInfo.PreVoyageID = list.PreVoyageID;
                        this._DTBookingInfo.PreVoyageName = list.PreVoyageName;
                        this._DTBookingInfo.VoyageID = list.VoyageID;
                        this._DTBookingInfo.VoyageName = list.VoyageName;
                        this._DTBookingInfo.ETD = list.ETD;
                        this._DTBookingInfo.ETA = list.ETA;
                        this._DTBookingInfo.ClosingDate = list.ClosingDate;
                        this._DTBookingInfo.CYClosingDate = list.CYClosingDate;
                        this._DTBookingInfo.DOCClosingDate = list.DOCClosingDate;
                        this._DTBookingInfo.ReturnLocationID = list.ReturnLocationID;
                        this._DTBookingInfo.ReturnLocationName = list.ReturnLocationName;
                    }
                }
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

                DTBookingInfo order = oeService.GetDTBookingInfo(CurrentOrderList.ID, LocalData.IsEnglish);
                if (order == null) return;

                order.ID = Guid.Empty;

                order.No = string.Empty;

                order.State = DTOrderState.NewOrder;

                order.OceanShippingOrderID = Guid.Empty;
                order.OceanShippingOrderNo = string.Empty;
                order.SalesID = LocalData.UserInfo.LoginID;
                order.SalesName = LocalData.UserInfo.LoginName;
                order.CreateDate = DateTime.Now;
                this._DTBookingInfo = order;

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
            //if (this._shown)
            //{
            if (cmbCargoType.EditValue == null)
            {
                this._DTBookingInfo.CargoDescription = null;
                RemoveCargoPart();
                return;
            }

            CargoType cargoType = (CargoType)Enum.Parse(typeof(CargoType), cmbCargoType.EditValue.ToString());
            RemoveCargoPart();
            SetCargo(sender as Control, cargoType);
            //}
        }

        private void SetCargo(Control sender, CargoType cargoType)
        {
            if (!this.cmbCargoType.Focused)
            {
                return;
            }
            //if (this._DTBookingInfo.CargoDescription == null)
            //{
            //    this.cmbCargoType.SelectedIndexChanged -= new EventHandler(cmbCargoType_EditValueChanged);
            //    this._DTBookingInfo.CargoDescription = new CargoDescription();
            //    this.cmbCargoType.SelectedIndexChanged += new EventHandler(cmbCargoType_EditValueChanged);
            //}
            if (cargoType == CargoType.Awkward)
            {
                if (_DTBookingInfo.CargoDescription == null
                    || _DTBookingInfo.CargoDescription.Cargo == null || _DTBookingInfo.CargoDescription.Cargo is AwkwardCargo == false)
                {
                    AwkwardCargo cargo = new AwkwardCargo();
                    cargo.NetWeightUnit = this.cmbWeightUnit.Text;
                    cargo.GrossWeightUnit = this.cmbWeightUnit.Text;
                    cargo.Quantity = (int)this.numQuantity.Value;
                    _DTBookingInfo.CargoDescription = new CargoDescription(cargo);
                    _DTBookingInfo.IsDirty = true;
                }

                if (cargoDescriptionPart1 is ICP.FCM.DomesticTrade.UI.Common.Controls.AwkwardDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.DomesticTrade.UI.Common.Controls.AwkwardDescriptionPart();
                    cargoDescriptionPart1.ShowWeightUnit(this._weightUnits);
                    this.navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Dangerous)
            {
                if (_DTBookingInfo.CargoDescription == null
                    || _DTBookingInfo.CargoDescription.Cargo == null || _DTBookingInfo.CargoDescription.Cargo is DangerousCargo == false)
                {
                    _DTBookingInfo.CargoDescription = new CargoDescription(new DangerousCargo());
                    _DTBookingInfo.IsDirty = true;
                }

                if (cargoDescriptionPart1 is ICP.FCM.DomesticTrade.UI.Common.Controls.DangerousDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.DomesticTrade.UI.Common.Controls.DangerousDescriptionPart();
                    this.navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Dry)
            {
                if (_DTBookingInfo.CargoDescription == null
                    || _DTBookingInfo.CargoDescription.Cargo == null || _DTBookingInfo.CargoDescription.Cargo is DryCargo == false)
                {
                    _DTBookingInfo.CargoDescription = new CargoDescription(new DryCargo());
                    _DTBookingInfo.IsDirty = true;
                }

                if (cargoDescriptionPart1 is ICP.FCM.DomesticTrade.UI.Common.Controls.DryDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.DomesticTrade.UI.Common.Controls.DryDescriptionPart();
                    this.navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Reefer)
            {
                if (_DTBookingInfo.CargoDescription == null
                    || _DTBookingInfo.CargoDescription.Cargo == null || _DTBookingInfo.CargoDescription.Cargo is ReeferCargo == false)
                {
                    _DTBookingInfo.CargoDescription = new CargoDescription(new ReeferCargo());
                    _DTBookingInfo.IsDirty = true;
                }

                if (cargoDescriptionPart1 is ICP.FCM.DomesticTrade.UI.Common.Controls.ReeferDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.DomesticTrade.UI.Common.Controls.ReeferDescriptionPart();
                    this.navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            cargoDescriptionPart1.SetParentControl(sender, _DTBookingInfo.CargoDescription, txtCargoDescription);
        }

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
            this._DTBookingInfo = oeService.GetDTBookingInfo(orderId, LocalData.IsEnglish);
        }

        void ShowOrder()
        {
            InitData();

            this.bsBookingInfo.DataSource = _DTBookingInfo;
            bsBookingInfo.ResetBindings(false);
            this.bsBookingInfo.CancelEdit();

            InitControls();

            List<DTBookingFeeList> feelist = null;

            if (_DTBookingInfo.ID == Guid.Empty)
            {
                feelist = new List<DTBookingFeeList>();
            }
            else
            {
                feelist = oeService.GetDTOrderFeeList(_DTBookingInfo.ID, LocalData.IsEnglish);
            }

            this.orderFeeEditPart1.SetSource(feelist);
        }
        public List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList> Details
        {


            get
            {
                this.bsContainer.EndEdit();
                List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList> list = bsContainer.DataSource as List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList>;
                if (list == null)
                {
                    list = new List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList>();
                }
                return list;
            }
            set { bsContainer.DataSource = value; }
        }
        public bool ConValidateData()
        {
            bsContainer.EndEdit();
            gridView1.CloseEditor();

            List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList> list = Details;

            List<string> noList = new List<string>();

            foreach (ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList box in list)
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


        bool _isChanged = false;
        public bool IsChanged
        {
            get
            {
                if (_isChanged == false)
                {
                    List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList> source = this.bsContainer.DataSource as List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList>;
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

        List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList> _con;
        List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList> GetConData(Guid orderId)
        {
            _con = oeService.GetDTContainerList(orderId, LocalData.IsEnglish);
            bsContainer.DataSource = _con;
            return bsContainer.DataSource as List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList>;
        }
        public void BindingData(object data)
        {

            this.SuspendLayout();
            this.orderFeeEditPart1.SetService(Workitem);

            DTBookingList listInfo = data as DTBookingList;

            if (listInfo == null)
            {
                //新建
                this._DTBookingInfo = new DTBookingInfo();
                this.ReadyForNew();
            }
            else
            {
                this.GetData(listInfo.ID);
                GetConData(listInfo.ID);
                if (listInfo.EditMode == EditMode.Edit)
                {
                }
                else if (listInfo.EditMode == EditMode.Copy)
                {
                    this.PrepareForCopyExistOrder();
                }
            }

            _DTBookingInfo.CancelEdit();

            this.InitalComboxes();

            this.ShowOrder();

            this.SearchRegister();
            this.SetLazyLoaders();

            this.ResumeLayout(true);
            int count = gridView1.RowCount;
            if (count > 0)
            {
                txtCount.Text = count.ToString();
            }


        }

        public override object DataSource
        {
            get { return bsBookingInfo.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return this.Save(this._DTBookingInfo, false);
        }

        public override void EndEdit()
        {
            if (this.stxtVoyage.EditValue != null)
            {
                this._DTBookingInfo.VoyageID = new Guid(this.stxtVoyage.EditValue.ToString());
            }

            if (this.stxtPreVoyage.EditValue != null)
            {
                this._DTBookingInfo.PreVoyageID = new Guid(this.stxtPreVoyage.EditValue.ToString());
            }

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

            List<bool> isScrrs = new List<bool> { true, true, true };

            isScrrs[0] = _DTBookingInfo.Validate
               (
                   delegate(ValidateEventArgs e)
                   {
                       if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.VoyageID) == false || Utility.GuidIsNullOrEmpty(_DTBookingInfo.PreVoyageID) == false)
                       {
                           //if (string.IsNullOrEmpty(_DTBookingInfo.ShippingOrderNo))
                           //    e.SetErrorInfo("ShippingOrderNo", LocalData.IsEnglish ? "ShippingOrderNo Must Input" : "订舱号必须输入.");
                       }

                       if (string.IsNullOrEmpty(_DTBookingInfo.OceanShippingOrderNo) == false)
                       {
                           //if (Utility.GuidIsNullOrEmpty(this._DTBookingInfo.ReturnLocationID))
                           //{
                           //    e.SetErrorInfo("ReturnLocationID", LocalData.IsEnglish ? "ReturnLocation Must Input" : "还柜地必须输入.");
                           //}

                           if (Utility.GuidIsNullOrEmpty(this._DTBookingInfo.AgentOfCarrierID))
                           {
                               e.SetErrorInfo("AgentOfCarrierID", LocalData.IsEnglish ? "AgentOfCarrier Must Input" : "承运人必须输入.");
                           }
                       }

                       if (string.IsNullOrEmpty(_DTBookingInfo.OceanShippingOrderNo) == false)
                       {
                           if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.VoyageID) && Utility.GuidIsNullOrEmpty(_DTBookingInfo.PreVoyageID))
                           {
                               e.SetErrorInfo("VoyageID", LocalData.IsEnglish ? "Voyage or PreVoyage Must Input" : "大船和驳船至少要填写一个");
                           }
                       }

                       if (!string.IsNullOrEmpty(this._DTBookingInfo.OceanShippingOrderNo))
                       {
                           if (Utility.GuidIsNullOrEmpty(this._DTBookingInfo.CarrierID))
                           {
                               e.SetErrorInfo("CarrierID", LocalData.IsEnglish ? "Carrier Must Input" : "船公司必须输入.");
                           }

                           if (!this._DTBookingInfo.SODate.HasValue)
                           {
                               e.SetErrorInfo("SODate", LocalData.IsEnglish ? "SODate Must Input" : "确认日期必须输入.");
                           }
                       }

                       if (_DTBookingInfo.IsContract && !string.IsNullOrEmpty(_DTBookingInfo.OceanShippingOrderNo))
                       {
                           if (Utility.GuidIsNullOrEmpty(_DTBookingInfo.ContractID))
                           {
                               e.SetErrorInfo("ContractID", LocalData.IsEnglish ? "Contract Must Input" : "合约必须输入.");
                           }
                       }

                       if (_DTBookingInfo.POLID != Guid.Empty && _DTBookingInfo.POLID == _DTBookingInfo.PODID)
                           e.SetErrorInfo("PODID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");

                       if (_DTBookingInfo.ETA != null && _DTBookingInfo.ETD != null
                           && _DTBookingInfo.ETD >= _DTBookingInfo.ETA)
                           e.SetErrorInfo("ETA", LocalData.IsEnglish ? "ETD can't bigger ETA." : "ETD不能大于ETA.");

                       if (_DTBookingInfo.ExpectedShipDate != null && _DTBookingInfo.ExpectedArriveDate != null
                           && _DTBookingInfo.ExpectedShipDate.Value.Date >= _DTBookingInfo.ExpectedArriveDate.Value.Date)
                           e.SetErrorInfo("ExpectedShipDate", LocalData.IsEnglish ? "ExpectedShipDate can't bigger ExpectedArriveDate." : "期望出运日不能大于期望到达日.");

                       // 小赵说这个逻辑是不需要的
                       //if (_DTBookingInfo.EstimatedDeliveryDate != null && _DTBookingInfo.DeliveryDate != null
                       //    && _DTBookingInfo.EstimatedDeliveryDate.Value.Date >= _DTBookingInfo.DeliveryDate.Value.Date)
                       //    e.SetErrorInfo("EstimatedDeliveryDate", LocalData.IsEnglish ? "EstimatedDeliveryDate can't bigger DeliveryDate." : "估计交货日不能大于实际交货日.");

                       if (this._DTBookingInfo.SODate.HasValue && this._DTBookingInfo.ClosingDate.HasValue)
                       {
                           if (this._DTBookingInfo.SODate.Value >= this._DTBookingInfo.ClosingDate.Value)
                           {
                               e.SetErrorInfo("SODate", LocalData.IsEnglish ? "Confirmed data must ealier than Closing date." : "确认日必须小于截关日.");
                           }
                       }

                       if (this._DTBookingInfo.SODate.HasValue && this._DTBookingInfo.CYClosingDate.HasValue)
                       {
                           if (this._DTBookingInfo.SODate.Value >= this._DTBookingInfo.CYClosingDate.Value)
                           {
                               e.SetErrorInfo("SODate", LocalData.IsEnglish ? "Confirmed data must ealier than CYClosing date." : "确认日必须小于截柜日.");
                           }
                       }

                       if (this._DTBookingInfo.ContainerDescription != null)
                       {
                           if (this._DTBookingInfo.ContainerDescription.ToString() != this.containerDemandControl1.Text)
                           {
                               this._DTBookingInfo.IsDirty = true;
                           }
                       }
                       //把箱需求转换成对象
                       _DTBookingInfo.ContainerDescription = new ContainerDescription(this.containerDemandControl1.Text.Trim());
                   }
               );

            #region ContainerDemand

            //果选择整箱业务类型，箱需求必输；箱需求逻辑,点击对应的箱型n次,则显示n*箱型
            if (_DTBookingInfo.DTOperationType == FCMOperationType.FCL)
            {
                if (this.containerDemandControl1.Text.Trim().Length == 0)
                {
                    this.dxErrorProvider1.SetError(containerDemandControl1.ErrorHost, LocalData.IsEnglish ? "FCL Bussines Must Input." : "整箱业务必须输入箱需求.");
                    isScrrs[0] = false;
                }
                else
                {
                    this.dxErrorProvider1.SetError(containerDemandControl1.ErrorHost, string.Empty);
                }
            }


            #endregion

            #region childParts

            //if (bookingPOEditPart1.ValidateData() == false)
            //{
            //    isScrrs[1] = false;
            //    xtraTabControl1.SelectedTabPageIndex = 1;
            //}

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
            Save(this._DTBookingInfo, false);
        }

        private bool Save(DTBookingInfo currentData, bool isSavingAs)
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
                if (Utility.GuidIsNullOrEmpty(currentData.ID) || Utility.GuidIsNullOrEmpty(currentData.OceanShippingOrderID))
                {
                    originalBooking = SaveDTBooking(currentData);
                }
                else if (_DTBookingInfo.IsDirty)
                {
                    if (_DTBookingInfo.BookingDate != null)
                        _DTBookingInfo.State = DTOrderState.BookingConfirmed;

                    originalBooking = SaveDTBooking(_DTBookingInfo);
                }

                List<FeeSaveRequest> originalFees = this.orderFeeEditPart1.SaveFee(currentData.ID);
                List<ContainerSaveRequest> _container = this.SaveContainer(currentData.ID);

                Dictionary<Guid, SaveResponse> saved = this.oeService.SaveDTBookingWithTrans(originalBooking,
                    originalFees, _container, LocalData.IsEnglish);

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
                //箱信息
                if (_container != null)
                {
                    SaveResponse.Analyze(_container.Cast<SaveRequest>().ToList(), saved, true);
                    this.RefreshContainerUI(_container);
                }
                //if (originalPos != null)
                //{
                //    SaveResponse.Analyze(originalPos.Cast<SaveRequest>().ToList(), saved, true);
                //    this.bookingPOEditPart1.RefreshUI(originalPos);
                //}


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
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                return false;
            }
            //}
            //catch (Exception ex)
            //{
            //    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            //    return false;
            //}
        }

        private void AfterSave()
        {
            _DTBookingInfo.CancelEdit();
            _DTBookingInfo.BeginEdit();

            if (_DTBookingInfo.AgentDescription != null)
            {
                _DTBookingInfo.AgentDescription.IsDirty = false;
            }
            if (_DTBookingInfo.ConsigneeDescription != null)
            {
                _DTBookingInfo.ConsigneeDescription.IsDirty = false;
            }
            if (_DTBookingInfo.ShipperDescription != null)
            {
                _DTBookingInfo.ShipperDescription.IsDirty = false;
            }
            if (_DTBookingInfo.BookingCustomerDescription != null)
            {
                _DTBookingInfo.BookingCustomerDescription.IsDirty = false;
            }

            this.TriggerSavedEvent();

            this.gvOrders.DoubleClick -= new System.EventHandler(this.gvOrders_DoubleClick);
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

            this.SetTitle();
        }

        void SetTitle()
        {
            if (this._DTBookingInfo.ID == Guid.Empty)
            {
                this.Title = LocalData.IsEnglish ? "Add Booking" : "新增订舱";
            }
            else
            {
                string titleNo = string.Empty;

                if (this._DTBookingInfo.No.Length > 4)
                {
                    titleNo = this._DTBookingInfo.No.Substring(this._DTBookingInfo.No.Length - 4, 4);
                }
                else
                {
                    titleNo = this._DTBookingInfo.No;
                }

                this.Title = LocalData.IsEnglish ? "Edit Booking " + titleNo : "编辑订舱：" + titleNo;
            }
        }

        void TriggerSavedEvent()
        {
            if (Saved != null)
            {
                this._DTBookingInfo.SalesName = this._DTBookingInfo.SalesID.ToGuid() == Guid.Empty ?
                    string.Empty : this.mcmbSales.EditText;
                this._DTBookingInfo.CarrierName = this._DTBookingInfo.CarrierID == Guid.Empty ?
                    string.Empty : this.mcmbCarrier.EditText;
                this._DTBookingInfo.OEOperationTypeDescription = EnumHelper.GetDescription<FCMOperationType>(this._DTBookingInfo.DTOperationType, LocalData.IsEnglish);
                this._DTBookingInfo.FilerName = this._DTBookingInfo.FilerId.ToGuid() == Guid.Empty ?
                    string.Empty : this.mcmbFiler.Text;
                this._DTBookingInfo.BookingerName = this._DTBookingInfo.BookingerID.ToGuid() == Guid.Empty ?
                    string.Empty : this.mcmbBookinger.Text;
                this._DTBookingInfo.VesselVoyage = this.stxtPreVoyage.Text + (this.stxtPreVoyage.Text.Trim().Length > 0 ? ";" : "") + this.stxtVoyage.Text;
                Saved(new object[] { (DTBookingList)_DTBookingInfo });

                this._DTBookingInfo.IsDirty = false;
            }
        }

        /// <summary>
        /// 客户参考号暂时传空值
        /// </summary>
        /// <param name="currentData"></param>
        private BookingSaveRequest SaveDTBooking(DTBookingInfo currentData)
        {
            this.EndEdit();

            if (currentData.IsDirty == true || currentData.IsNew)
            {
                BookingSaveRequest saveRequest = new BookingSaveRequest();

                saveRequest.id = currentData.ID;
                saveRequest.customerRefNo = string.Empty;
                saveRequest.customerID = currentData.CustomerID;
                //saveRequest.tradeTermID = currentData.TradeTermID;
                saveRequest.dtOperationType = currentData.DTOperationType;
                saveRequest.companyID = currentData.CompanyID;
                saveRequest.bookingerID = currentData.BookingerID;
                saveRequest.filerID = currentData.FilerId;
                //saveRequest.overSeasFilerId = currentData.OverSeasFilerID;
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
                saveRequest.placeOfReceiptID = currentData.PlaceOfReceiptID;
                saveRequest.polID = currentData.POLID;
                saveRequest.podID = currentData.PODID;
                saveRequest.placeOfDeliveryID = currentData.PlaceOfDeliveryID;
                saveRequest.agentID = currentData.AgentID;
                saveRequest.agentDescription = currentData.AgentDescription;
                saveRequest.carrierID = currentData.CarrierID;
                saveRequest.agentOfCarrierID = currentData.AgentOfCarrierID;
                saveRequest.isContract = currentData.IsContract;
                saveRequest.freightRateID = currentData.ContractID;
                saveRequest.oceanShippingOrderID = currentData.OceanShippingOrderID;
                saveRequest.oceanShippingOrderNo = currentData.OceanShippingOrderNo;
                saveRequest.soDate = currentData.SODate;
                saveRequest.estimatedDeliveryDate = currentData.EstimatedDeliveryDate;
                saveRequest.actualDeliveryDate = currentData.DeliveryDate;
                saveRequest.expectedShipDate = currentData.ExpectedShipDate;
                saveRequest.expectedArriveDate = currentData.ExpectedArriveDate;
                saveRequest.closingDate = currentData.ClosingDate;
                saveRequest.paymentTermID = currentData.PaymentTermID;
                saveRequest.transportClauseID = currentData.TransportClauseID;
                saveRequest.shippingLineID = currentData.ShippingLineID;
                saveRequest.preVoyageID = currentData.PreVoyageID;
                saveRequest.voyageID = currentData.VoyageID;
                saveRequest.commodity = currentData.Commodity;
                saveRequest.quantity = currentData.Quantity;
                saveRequest.quantityUnitID = currentData.QuantityUnitID;
                saveRequest.weight = currentData.Weight;
                saveRequest.weightUnitID = currentData.WeightUnitID;
                saveRequest.measurement = currentData.Measurement;
                saveRequest.measurementUnitID = currentData.MeasurementUnitID;
                saveRequest.cargoDescription = currentData.CargoDescription;
                saveRequest.containerDescription = currentData.ContainerDescription;
                //saveRequest.mblPaymentTermID = currentData.MBLPaymentTermID;
                //saveRequest.hblPaymentTermID = currentData.HBLPaymentTermID;
                saveRequest.isTruck = currentData.IsTruck;
                //saveRequest.isCustoms = currentData.IsCustoms;
                //saveRequest.isCommodityInspection = currentData.IsCommodityInspection;
                //saveRequest.isQuarantineInspection = currentData.IsQuarantineInspection;
                saveRequest.isWarehouse = currentData.IsWareHouse;
                //saveRequest.isOnlyMBL = currentData.IsOnlyMBL;
                //saveRequest.mblReleaseType = currentData.MBLReleaseType;
                //saveRequest.hblReleaseType = currentData.HBLReleaseType;
                //saveRequest.mblRequirements = currentData.MBLRequirements;
                //saveRequest.hblRequirements = currentData.HBLRequirements;
                saveRequest.remark = currentData.Remark;
                //saveRequest.finalDestinationID = currentData.FinalDestinationID;
                saveRequest.returnLocationID = currentData.ReturnLocationID;
                saveRequest.warehouseID = currentData.WarehouseID;
                saveRequest.closingWarehousedate = currentData.ClosingWarehousedate;
                saveRequest.saveByID = LocalData.UserInfo.LoginID;
                saveRequest.oceanShippingOrderUpdateDate = currentData.OceanShippingOrderUpdateDate;
                saveRequest.oceanOrderUpdateDate = currentData.UpdateDate;
                saveRequest.DocClosingDate = currentData.DOCClosingDate;
                saveRequest.CYClosingDate = currentData.CYClosingDate;
                saveRequest.PreETD = currentData.PreETD;
                saveRequest.ETA = currentData.ETA;
                saveRequest.ETD = currentData.ETD;
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

            DTBookingInfo currentData = saveRequest.UnBoxInvolvedObject<DTBookingInfo>()[0];

            currentData.ID = result.GetValue<Guid>("ID");
            currentData.No = result.GetValue<string>("No");
            currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            currentData.OceanShippingOrderUpdateDate = result.GetValue<DateTime?>("InternalTradeShippingOrderUpdateDate");
            currentData.OceanShippingOrderID = result.GetValue<Guid>("InternalTradeShippingOrderID");
            currentData.State = (DTOrderState)result.GetValue<byte>("State");

            currentData.IsDirty = false;

            if (currentData.AgentDescription != null)
            {
                currentData.AgentDescription.IsDirty = false;
            }
            if (currentData.ConsigneeDescription != null)
            {
                currentData.ConsigneeDescription.IsDirty = false;
            }
            if (currentData.ShipperDescription != null)
            {
                currentData.ShipperDescription.IsDirty = false;
            }
            if (currentData.BookingCustomerDescription != null)
            {
                currentData.BookingCustomerDescription.IsDirty = false;
            }

            RefreshBarEnabled();

            //不工作，而且以前状态丢失，可能会有问题
            //this.Workitem.ID = OEBookingCommandConstants.Command_EditData + currentData.ID.ToString();
        }

        #endregion

        #region 另存为

        private void barSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveAs())
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save as a new booking order successfully. Ref. NO. is " + this._DTBookingInfo.No + "." : "已成功另存为一票新订单，业务号为" + this._DTBookingInfo.No + "。");
                if (Saved != null)
                {
                    Saved(new object[] { this._DTBookingInfo });
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

            DTBookingInfo orderInfo = Utility.Clone<DTBookingInfo>(this._DTBookingInfo);
            orderInfo.ID = Guid.Empty;
            orderInfo.No = string.Empty;

            if (string.IsNullOrEmpty(orderInfo.OceanShippingOrderNo))
            {
                orderInfo.State = DTOrderState.NewOrder;
            }
            else
            {
                orderInfo.State = DTOrderState.BookingConfirmed;
            }

            orderInfo.CreateByID = LocalData.UserInfo.LoginID;
            orderInfo.CreateByName = LocalData.UserInfo.LoginName;
            orderInfo.CreateDate = DateTime.Now;
            orderInfo.BookingDate = DateTime.Now;
            orderInfo.UpdateDate = null;

            if (orderInfo.DTOperationType == FCMOperationType.LCL)
            {
            }
            else
            {
                orderInfo.OceanShippingOrderID = Guid.Empty;
                orderInfo.OceanShippingOrderNo = string.Empty;
            }

            orderInfo.IsDirty = true;

            this._DTBookingInfo = orderInfo;

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
            if (!this.Save(this._DTBookingInfo, false))
            {
                return;
            }

            if (this._DTBookingInfo.State != DTOrderState.NewOrder)
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
                    SingleResult result = oeService.ChangeDTOrderStateWithTargetState(_DTBookingInfo.ID, DTOrderState.Checked, prams[0].ToString(), LocalData.UserInfo.LoginID, _DTBookingInfo.UpdateDate, LocalData.IsEnglish);

                    ICP.Framework.CommonLibrary.Logger.Log.Info(result);

                    this._DTBookingInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    this._DTBookingInfo.State = (DTOrderState)result.GetValue<byte>("State");

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

        #endregion

        #region 打印

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!SaveData())
            {
                return;
            }

            ExportPrintHelper.PrintOEBookingConfirmation(_DTBookingInfo.ID);
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

            ExportPrintHelper.PrintOEOrder(this._DTBookingInfo.ID);
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
            if (_DTBookingInfo.State == DTOrderState.Rejected) return;

            Common.Parts.EditRemarkPart editRemarkPart = Workitem.Items.AddNew<Common.Parts.EditRemarkPart>();
            editRemarkPart.LabRemark = LocalData.IsEnglish ? "Reject reason" : "打回原因";
            editRemarkPart.RemartRequired = true;
            editRemarkPart.Saved += delegate(object[] prams)
            {
                try
                {
                    bool isDirty = _DTBookingInfo.IsDirty;
                    SingleResult result = oeService.ChangeDTOrderStateWithTargetState(_DTBookingInfo.ID, DTOrderState.Rejected, prams[0].ToString(), LocalData.UserInfo.LoginID, _DTBookingInfo.UpdateDate, LocalData.IsEnglish);

                    this._DTBookingInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    this._DTBookingInfo.State = (DTOrderState)result.GetValue<byte>("State");

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
            //DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Sure Refresh Data?" : "是否刷新数据?",
            //                                   LocalData.IsEnglish ? "Tip" : "提示",
            //                                   MessageBoxButtons.YesNo,
            //                                   MessageBoxIcon.Question);
            //if (dialogResult == DialogResult.Yes)
            //{
            try
            {

                RefreshData(this._DTBookingInfo.ID);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Refersh successfully." : "刷新成功.");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Refersh failed." + ex.Message : "刷新失败." + ex.Message);
            }

            //}
        }

        void RefreshData(Guid orderId)
        {
            this.GetData(this._DTBookingInfo.ID);
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
            if (this._DTBookingInfo.IsDirty)
            {
                DialogResult dr = PartLoader.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!this.Save(this._DTBookingInfo, false))
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
               
                stxtWarehouse,
                stxtReturnLocation,
            });

            Utility.SetPortTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtPlaceOfDelivery,
                stxtPlaceOfReceipt ,
                stxtPOD,
                stxtPOL,
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
                return this._DTBookingInfo.IsDirty;
            }
        }

        private void barApplyAgent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                fcmCommonClientService.OpenAgentRequestPart(this._DTBookingInfo.ID, ICP.Framework.CommonLibrary.Common.OperationType.Unknown, Workitem);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
        }

        private void barTruck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<DTTruckInfo> truckList = oeService.GetDTTruckServiceList(_DTBookingInfo.ID, LocalData.IsEnglish);
            SingleResult recentData = oeService.GetTruckRecentData(_DTBookingInfo.ID, LocalData.IsEnglish);

            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            stateValues.Add("Booking", _DTBookingInfo);

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
            PartLoader.ShowEditPart<Booking.DTTruckEditPart>(Workitem, truckList, stateValues, title + Booking.BookingListPart.GetLineNo(this._DTBookingInfo), null,
                Booking.DTBookingCommandConstants.Command_Truck + CurrentRow.ID.ToString());


        }
        int count = 0;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList preRow = null;
            if (this.gridView1.RowCount > 0)
            {
                preRow = gridView1.GetRow(gridView1.RowCount - 1) as ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList;
            }
            ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList newBoxRow;

            if (preRow != null)
                newBoxRow = Utility.Clone<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList>(preRow);
            else
                newBoxRow = new ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList();

            newBoxRow.ID = Guid.Empty;
            newBoxRow.CreateDate = DateTime.Now;
            newBoxRow.CreateByID = LocalData.UserInfo.LoginID;
            //(this.bsContainer.List as List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList>).Add(newBoxRow);
            bsContainer.Add(newBoxRow);
            bsContainer.ResetBindings(false);

            this.gridView1.MoveLast();
            _isChanged = true;
            count = gridView1.RowCount;
            if (count > 0)
            {
                txtCount.Text = count.ToString();
            }
        }

        #region 删除箱信息
        /// <summary>
        /// 明细当前行
        /// </summary>
        private ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList CurrentRow
        {
            get
            {
                return this.bsContainer.Current as ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList;
            }
        }
        /// <summary>
        /// 所有选择的行（箱信息）
        /// </summary>
        List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList> SelectRows
        {
            get
            {
                int[] indexs = this.gridView1.GetSelectedRows();
                if (indexs == null || indexs.Length == 0) return null;

                List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList> list = new List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList>();
                foreach (var item in indexs)
                {
                    ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList tager = gridView1.GetRow(item) as ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList;
                    if (tager != null) list.Add(tager);
                }
                return list;
            }
        }
        private void RemoveFee()
        {
            List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList> list = SelectRows;
            if (list == null || list.Count == 0) return;

            if (!Utility.EnquireIsDeleteCurrentData())
            {
                return;
            }

            List<Guid> IDList = new List<Guid>();
            List<DateTime?> DateList = new List<DateTime?>();

            foreach (ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList hbl in list)
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
                    oeService.RemoveDTContaierInfo(IDList.ToArray(), LocalData.UserInfo.LoginID, DateList.ToArray(), LocalData.IsEnglish);
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
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            RemoveFee();
        }
        #endregion


        #region 保存箱信息
        public List<ContainerSaveRequest> SaveContainer(Guid OrderID)
        {
            this.gridView1.CloseEditor();
            if (!this.ConValidateData()) { return null; }
            List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList> List = this.bsContainer.DataSource as List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList>;
            if (List != null && List.Count > 0)
            {
                List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList> changedList = List.FindAll(o => o.IsDirty);//明细无更改则不执行保存明细之过程
                if (OrderID == Guid.Empty)
                {
                    changedList = List;
                }
                if (changedList.Count > 0)
                {
                    List<ContainerSaveRequest> commands = new List<ContainerSaveRequest>();

                    List<Guid?> IDs = new List<Guid?>(); List<string> Nos = new List<string>(); List<Guid> TypeIDs = new List<Guid>();
                    List<string> ShippingOrderNos = new List<string>(); List<string> DriverNames = new List<string>();
                    List<DateTime?> ArriveDates = new List<DateTime?>(); List<string> CarNos = new List<string>();
                    List<DateTime?> ReturnDates = new List<DateTime?>(); List<DateTime?> UpdateDates = new List<DateTime?>();
                    List<string> SealNos = new List<string>(); List<DateTime?> DeliveryDates = new List<DateTime?>();
                    foreach (var item in changedList)
                    {
                        IDs.Add(item.ID);
                        Nos.Add(item.No);
                        TypeIDs.Add(item.TypeID);
                        ShippingOrderNos.Add(item.ShippingOrderNo);
                        DriverNames.Add(item.DriverName);
                        ArriveDates.Add(item.ArriveDate);
                        CarNos.Add(item.CarNo);
                        UpdateDates.Add(item.UpdateDate);
                        ReturnDates.Add(item.ReturnDate);
                        SealNos.Add(item.SealNo);
                        DeliveryDates.Add(item.DeliveryDate);
                    }

                    ContainerSaveRequest feeInfo = new ContainerSaveRequest();
                    feeInfo.oceanBookingID = OrderID;
                    feeInfo.containerIDs = IDs.ToArray();
                    feeInfo.containerNos = Nos.ToArray();
                    feeInfo.containerTypeIDs = TypeIDs.ToArray();
                    feeInfo.containerShippingOrderNos = ShippingOrderNos.ToArray();
                    feeInfo.driverNames = DriverNames.ToArray();
                    feeInfo.arriveDates = ArriveDates.ToArray();
                    feeInfo.carNos = CarNos.ToArray();
                    feeInfo.returnDates = ReturnDates.ToArray();
                    feeInfo.containerUpdateDates = UpdateDates.ToArray();
                    feeInfo.containerSealNos = SealNos.ToArray();
                    feeInfo.deliveryDates = DeliveryDates.ToArray();
                    feeInfo.saveByID = LocalData.UserInfo.LoginID;
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
                List<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList> changedContainer = ContainerInfo.UnBoxInvolvedObject<ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTContainerList>();
                ManyResult result = ContainerInfo.ManyResult;
                for (int i = 0; i < changedContainer.Count; i++)
                {
                    changedContainer[i].ID = result.Items[i].GetValue<Guid>("ID");
                    changedContainer[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                    changedContainer[i].IsDirty = false;
                }
            }
            this.AfterSave();
        }

        #endregion
    }
}