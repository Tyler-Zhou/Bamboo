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
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.OceanExport.UI.Common;
using ICP.Common.UI;
using ICP.Framework.ClientComponents.Service;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.FCM.Common.UI;


namespace ICP.FCM.OceanExport.UI.Booking
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
        public IDataFindClientService dfService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IConfigureService configureService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ITransportFoundationService tfService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IGeographyService geographyService { get; set; }

        [ServiceDependency]
        public IOceanExportService oeService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ICustomerService customerService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelper { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public OceanExportPrintHelper OceanExportPrintHelper { get; set; }

        [ServiceDependency]
        public ICP.Framework.CommonLibrary.Client.IErrorTraceService errorService { get; set; }

        #endregion

        #region 本地变量

        CustomerType customerType = CustomerType.Unknown;

        OceanBookingInfo _oceanBookingInfo = null;

        /// <summary>
        /// 原始合约号
        /// </summary>
        Guid? origContract = null;
        /// <summary>
        /// 合约号发生改变
        /// </summary>
        bool chargeContract = false;

        //List<OceanBookingPOList> _oceanBookingPOInfo = null;        

        /// <summary>
        /// 缓存国家列表,只获取一次.现只用于客户弹出式描述框
        /// </summary>
        List<CountryList> _countryList = null;
        // private VoyageDateInfoHelper voyageDateHelper = null;

        /// <summary>
        /// 代理下拉数据源
        /// </summary>
        List<CustomerList> _agentCustomersList = null;

        /// <summary>
        /// 是否保存订舱
        /// </summary>
        bool isSave = false;
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

            if (LocalData.IsEnglish == false)
            {
                SetCnText();
            }
            // voyageDateHelper = new VoyageDateInfoHelper();
            // voyageDateHelper.Init(VoyageFormType.Booking, stxtPreVoyage, stxtVoyage, stxtPlaceOfReceipt,
            //    stxtPOL, stxtPOD, dtstxtPlaceOfReceipt, dtstxtPOL, dtstxtPOD, dteClosingDate, dteCYClosingDate, dteDOCClosingDate, null, null);

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
            labClosingDate.Text = "截关日";
            labCommodity.Text = "品名";
            labCompany.Text = "操作口岸";
            labConsignee.Text = "收货人";
            labCustomer.Text = "客户";
            labEstimatedDeliveryDate.Text = "估计交货";
            labExpectedArriveDate.Text = "期望到达";
            labExpectedShipDate.Text = "期望出运";

            labHBLPaymentTerm.Text = "付款方式";
            labMBLPaymentTerm.Text = "付款方式";
            labHBLReleaseType.Text = "放单类型";
            labMBLReleaseType.Text = "放单类型";

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
            labTradeTerm.Text = "贸易条款";
            labTransportClause.Text = "运输条款";
            labReturnLocation.Text = "还柜地点";
            labType.Text = "业务类型";
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
            this.labOverseasFiler.Text = "海外部客服";
            this.labBookinger.Text = "订舱";
            labFiler.Text = "文件";
            labState.Text = "状态";
            labOrderNo.Text = "订舱号";
            labSODate.Text = "确认日期";
            labVoyage.Text = "大船";
            labPreVoyage.Text = "驳船";
            labPlaceOfReceipt.Text = "收货地";
            labFinalDestination.Text = "最终目的地";

            this.labETD.Text = "离港日";
            this.labETA.Text = "到港日";
            this.labelControl1.Text = "离港日";

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
            OceanBookingInfo newData = new OceanBookingInfo();
            //newData.SalesID = LocalData.UserInfo.LoginID;
            //newData.SalesName = LocalData.UserInfo.LoginName;
            newData.OEOperationType = FCMOperationType.FCL;
            newData.BookingDate = newData.CreateDate = DateTime.Now;
            //确认日期为今天
            newData.SODate = DateTime.Now;
            newData.BookingMode = FCMBookingMode.Fax;
            newData.State = OEOrderState.NewOrder;
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

            this._oceanBookingInfo = newData;

            _oceanBookingInfo.HBLReleaseType = _oceanBookingInfo.MBLReleaseType = FCMReleaseType.Unknown;

            // TODO: 这种Guard型的逻辑要在最开始的时候完成
            Utility.EnsureDefaultCompanyExists(this.userService);

            this._oceanBookingInfo.CompanyID = LocalData.UserInfo.DefaultCompanyID;
            this._oceanBookingInfo.CompanyName = LocalData.UserInfo.DefaultCompanyName;


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
            this._oceanBookingInfo.OceanShippingOrderID = Guid.Empty;
            this._oceanBookingInfo.OceanShippingOrderNo = string.Empty;
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
            //业务类型
            this.cmbType.ShowSelectedValue(this._oceanBookingInfo.OEOperationType,
                EnumHelper.GetDescription<FCMOperationType>(this._oceanBookingInfo.OEOperationType, LocalData.IsEnglish, true));
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
                 && cmbMeasurementUnit.EditValue != null && this.cmbQuantityUnit.EditValue != DBNull.Value)
            {
                this._oceanBookingInfo.QuantityUnitID = (Guid)this.cmbQuantityUnit.EditValue;
            }
            //重量
            cmbWeightUnit.ShowSelectedValue(this._oceanBookingInfo.WeightUnitID, this._oceanBookingInfo.WeightUnitName);
            if (Utility.GuidIsNullOrEmpty(this._oceanBookingInfo.WeightUnitID)
                 && cmbMeasurementUnit.EditValue != null && this.cmbWeightUnit.EditValue != DBNull.Value)
            {
                this._oceanBookingInfo.WeightUnitID = (Guid)this.cmbWeightUnit.EditValue;
            }
            //体积
            cmbMeasurementUnit.ShowSelectedValue(this._oceanBookingInfo.MeasurementUnitID, this._oceanBookingInfo.MeasurementUnitName);
            if (Utility.GuidIsNullOrEmpty(this._oceanBookingInfo.MeasurementUnitID)
                && cmbMeasurementUnit.EditValue != null && cmbMeasurementUnit.EditValue != DBNull.Value)
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
            cmbMBLPaymentTerm.ShowSelectedValue(this._oceanBookingInfo.MBLPaymentTermID, this._oceanBookingInfo.MBLPaymentTermName);
            cmbHBLPaymentTerm.ShowSelectedValue(this._oceanBookingInfo.HBLPaymentTermID, this._oceanBookingInfo.HBLPaymentTermName);
            //航线
            cmbShippingLine.ShowSelectedValue(this._oceanBookingInfo.ShippingLineID, this._oceanBookingInfo.ShippingLineName);
            //运输条款
            this.cmbTransportClause.ShowSelectedValue(this._oceanBookingInfo.TransportClauseID, this._oceanBookingInfo.TransportClauseName);
            //船公司
            this.mcmbCarrier.ShowSelectedValue(this._oceanBookingInfo.CarrierID, this._oceanBookingInfo.CarrierName);

            this.cmbMBLReleaseType.ShowSelectedValue(this._oceanBookingInfo.MBLReleaseType, EnumHelper.GetDescription<FCMReleaseType>(this._oceanBookingInfo.MBLReleaseType.Value, LocalData.IsEnglish));
            this.cmbHBLReleaseType.ShowSelectedValue(this._oceanBookingInfo.HBLReleaseType, EnumHelper.GetDescription<FCMReleaseType>(this._oceanBookingInfo.HBLReleaseType.Value, LocalData.IsEnglish));

            //箱需求
            if (_oceanBookingInfo.ContainerDescription != null)
            {
                this.containerDemandControl1.Text = _oceanBookingInfo.ContainerDescription.ToString();
            }
            this.dxErrorProvider1.SetIconAlignment(containerDemandControl1.ErrorHost, ErrorIconAlignment.MiddleRight);

            //货物描述
            if (_oceanBookingInfo.CargoDescription != null
                && _oceanBookingInfo.CargoDescription.Cargo != null)
            {
                txtCargoDescription.Text = _oceanBookingInfo.CargoDescription.Cargo.ToString(LocalData.IsEnglish);
            }
            //驳船
            this.stxtPreVoyage.ShowSelectedValue(_oceanBookingInfo.PreVoyageID, _oceanBookingInfo.PreVoyageName);
            //大船
            this.stxtVoyage.ShowSelectedValue(_oceanBookingInfo.VoyageID, _oceanBookingInfo.VoyageName);

            this.orderFeeEditPart1.SetCompanyID(this._oceanBookingInfo.CompanyID);

            this.mcmbFiler.ShowSelectedValue(this._oceanBookingInfo.FilerId, this._oceanBookingInfo.FilerName);

            this.mcmbOverseasFiler.ShowSelectedValue(this._oceanBookingInfo.OverSeasFilerID, this._oceanBookingInfo.OverSeasFilerName);

            this.mcmbBookinger.ShowSelectedValue(this._oceanBookingInfo.BookingerID, this._oceanBookingInfo.BookingerName);

            if (this._oceanBookingInfo.CargoType.HasValue)
            {
                this.cmbCargoType.ShowSelectedValue(this._oceanBookingInfo.CargoType,
                    EnumHelper.GetDescription<CargoType>(this._oceanBookingInfo.CargoType.Value, LocalData.IsEnglish));
            }

            this.stxtReturnLocation.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;

            this.gvOrders.DoubleClick += new System.EventHandler(this.gvOrders_DoubleClick);
        }

        /// <summary>
        /// 初始化提示信息
        /// </summary>
        private void InitMessage()
        {
            this.RegisterMessage("CreateBills", LocalData.IsEnglish ? "According to the contract system is generated with the bills" : "系统已根据合约生成了应付账单");
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

                          //CustomerCodeApplyState? approved = (CustomerCodeApplyState?)resultData[8];
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

                          if (resultData[4] != null)
                          {
                              this.customerType = (CustomerType)resultData[4];
                          }

                          stxtCustomer.Tag = _oceanBookingInfo.CustomerID = new Guid(resultData[0].ToString());

                          stxtCustomer.EditValue = _oceanBookingInfo.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();

                          if (resultData[5] != null && (Guid)resultData[5] != Guid.Empty && resultData[6] != null)
                          {
                              this.cmbTradeTerm.ShowSelectedValue(resultData[5], resultData[6].ToString());
                          }

                          if (resultData[5] != null
                      && (Guid)resultData[5] != Guid.Empty
                      && resultData[6] != null)
                          {
                              this.cmbTradeTerm.ShowSelectedValue(resultData[5], resultData[6].ToString());
                          }

                          if (oldCustomerId != Guid.Empty && _oceanBookingInfo.CustomerID == oldCustomerId)
                          {
                              return;
                          };

                          CustomerChanged(customerType);


                      }, delegate
                      {
                          stxtCustomer.Text = _oceanBookingInfo.CustomerName = string.Empty;
                          stxtCustomer.Tag = _oceanBookingInfo.CustomerID = Guid.Empty;
                          stxtCustomer.ClosePopup();
                          CustomerChanged(null);
                      },
                      ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            //仓库
            this.dfService.Register(this.stxtWarehouse, CommonFinderConstants.CustoemrFinder,
                SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                //this.GetConditionsForWarehouse,
                delegate(object inputSource, object[] resultData)
                {
                    stxtWarehouse.Tag = this._oceanBookingInfo.WarehouseID = (Guid)resultData[0];
                    stxtWarehouse.EditValue = this._oceanBookingInfo.WarehouseName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate
                {
                    stxtWarehouse.Tag = this._oceanBookingInfo.WarehouseID = null;
                    stxtWarehouse.EditValue = this._oceanBookingInfo.WarehouseName = string.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            this.dfService.Register(this.stxtReturnLocation, CommonFinderConstants.CustoemrFinder,
                SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                GetConditionsForReturnLocation,
                delegate(object inputSouce, object[] resultData)
                {
                    stxtReturnLocation.Tag = this._oceanBookingInfo.ReturnLocationID = (Guid)resultData[0];
                    stxtReturnLocation.EditValue = this._oceanBookingInfo.ReturnLocationName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate
                {
                    this.stxtReturnLocation.Tag = this._oceanBookingInfo.ReturnLocationID = null;
                    this.stxtReturnLocation.EditValue = this._oceanBookingInfo.ReturnLocationName = string.Empty;
                },
            ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            dfService.Register(stxtAgentOfCarrier, CommonFinderConstants.CustomerAgentOfCarrierFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
               delegate(object inputSource, object[] resultData)
               {
                   stxtAgentOfCarrier.Text = _oceanBookingInfo.AgentOfCarrierName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                   stxtAgentOfCarrier.Tag = _oceanBookingInfo.AgentOfCarrierID = new Guid(resultData[0].ToString());
               }, Guid.Empty, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            #endregion

            #region 订舱客户
            dfService.Register(stxtBookingCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.CustomerResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          Guid oldBookingCustomerID = _oceanBookingInfo.BookingCustomerID;
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
               ICPCommUIHelper,
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
                ICPCommUIHelper,
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
            //    ICPCommUIHelper,
            //    LocalData.IsEnglish);
            //    bookingCustomerPartyBridge.Init();
            //});
            //stxtBookingCustomer.OnOk += new EventHandler(stxtBookingCustomer_OnOk);

            #endregion

            #region Port

            pfbPlaceOfReceipt = new LocationFinderBridge(this.stxtPlaceOfReceipt, this.dfService, LocalData.IsEnglish);
           
          
            PortFinderBridge pfbPOL = new PortFinderBridge(this.stxtPOL, this.dfService, LocalData.IsEnglish);

            PortFinderBridge pfbPOD = new PortFinderBridge(this.stxtPOD, this.dfService, LocalData.IsEnglish);
            
            //LocationFinderBridge pfbPlaceOfDelivery = new LocationFinderBridge(this.stxtPlaceOfDelivery, this.dfService, LocalData.IsEnglish);

            dfService.Register(stxtPlaceOfDelivery, CommonFinderConstants.LocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                     delegate(object inputSource, object[] resultData)
                      {
                          Guid oldPlaceOfDeliveryID = _oceanBookingInfo.PlaceOfDeliveryID;
                          Guid newPlaceOfDeliveryID = new Guid(resultData[0].ToString());
                          if (oldPlaceOfDeliveryID == newPlaceOfDeliveryID)
                          {
                              return;
                          }

                          stxtPlaceOfDelivery.Tag = _oceanBookingInfo.PlaceOfDeliveryID = newPlaceOfDeliveryID;
                          stxtPlaceOfDelivery.Text = _oceanBookingInfo.PlaceOfDeliveryName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                          this.SetFinalDestinationByTransportClause();
                          this.SetAgetnEnabledByPlaceOfDeliveryAndCompany(); 
                      }, delegate
                      {
                          stxtPlaceOfDelivery.Tag = _oceanBookingInfo.PlaceOfDeliveryID = Guid.Empty;
                          stxtPlaceOfDelivery.Text = _oceanBookingInfo.PlaceOfDeliveryName = string.Empty;
                      },
                      ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            LocationFinderBridge pfbFinalDestination = new LocationFinderBridge(this.stxtFinalDestination, this.dfService, LocalData.IsEnglish);

            #endregion
        }

        void stxtBookingCustomer_OnOk(object sender, EventArgs e)
        {
            if (stxtBookingCustomer.CustomerDescription != null && _oceanBookingInfo != null)
            {
                _oceanBookingInfo.BookingCustomerDescription = stxtBookingCustomer.CustomerDescription;
            }
        }

        void stxtConsignee_OnOk(object sender, EventArgs e)
        {
            if (stxtConsignee.CustomerDescription != null && _oceanBookingInfo != null)
            {
                _oceanBookingInfo.ConsigneeDescription = stxtConsignee.CustomerDescription;
            }
        }

        void stxtShipper_OnOk(object sender, EventArgs e)
        {
            if (stxtShipper.CustomerDescription != null && _oceanBookingInfo != null)
            {
                _oceanBookingInfo.ShipperDescription = stxtShipper.CustomerDescription;
            }
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
            _weightUnits = ICPCommUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit, DataBindType.EName);
            //包装
            Utility.SetEnterToExecuteOnec(cmbQuantityUnit, delegate
            {
                ICPCommUIHelper.SetCmbDataDictionary(cmbQuantityUnit, DataDictionaryType.QuantityUnit, DataBindType.EName);
            });

            //重量
            Utility.SetEnterToExecuteOnec(cmbWeightUnit, delegate
            {
                _weightUnits = ICPCommUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit, DataBindType.EName);
            });

            //体积
            Utility.SetEnterToExecuteOnec(cmbMeasurementUnit, delegate
            {
                List<DataDictionaryList> volUnitss = ICPCommUIHelper.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit, DataBindType.EName);
            });
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
                ICPCommUIHelper.BindCompanyByUser(this.cmbCompany, false);

                if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.CompanyID))
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

                        ICPCommUIHelper.SetCustomerDesByID(id, _oceanBookingInfo.AgentDescription);
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
                ICPCommUIHelper.SetCmbDataDictionary(cmbTradeTerm, DataDictionaryType.TradeTerm);
            });

            //揽货方式
            Utility.SetEnterToExecuteOnec(cmbSalesType, delegate
            {
                List<DataDictionaryList> salesTypes = ICPCommUIHelper.SetCmbDataDictionary(cmbSalesType, DataDictionaryType.SalesType);
            });

            //3个付款方式的下拉列表
            Utility.SetEnterToExecuteOnec(cmbPaymentTerm, delegate
            {
                List<DataDictionaryList> payments = ICPCommUIHelper.SetCmbDataDictionary(
                                                    cmbPaymentTerm,
                                                    DataDictionaryType.PaymentTerm, DataBindType.EName, true);
            });
            Utility.SetEnterToExecuteOnec(cmbMBLPaymentTerm, delegate
            {
                List<DataDictionaryList> payments = ICPCommUIHelper.SetCmbDataDictionary(
                                                    cmbMBLPaymentTerm,
                                                    DataDictionaryType.PaymentTerm, DataBindType.EName, true);
            });
            Utility.SetEnterToExecuteOnec(cmbHBLPaymentTerm, delegate
            {
                List<DataDictionaryList> payments = ICPCommUIHelper.SetCmbDataDictionary(
                                                    cmbHBLPaymentTerm,
                                                    DataDictionaryType.PaymentTerm, DataBindType.EName, true);
            });
            //航线
            Utility.SetEnterToExecuteOnec(cmbShippingLine, delegate
            {
                List<ShippingLineList> shippingLines = ICPCommUIHelper.SetCmbShippingLine(cmbShippingLine);
            });

            //船公司
            Utility.SetEnterToExecuteOnec(mcmbCarrier, delegate
            {
                ICPCommUIHelper.BindCustomerList(this.mcmbCarrier, CustomerType.Carrier);
            });

            //运输条款
            Utility.SetEnterToExecuteOnec(cmbTransportClause, delegate
            {
                List<TransportClauseList> transportClauseList = ICPCommUIHelper.SetCmbTransportClause(cmbTransportClause);
                //if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.TransportClauseID))
                //{
                //    _oceanBookingInfo.TransportClauseID = transportClauseList[0].ID;
                //}
            });


            Utility.SetEnterToExecuteOnec(cmbMBLReleaseType, delegate
            {
                ICPCommUIHelper.SetComboxByEnum<FCMReleaseType>(this.cmbMBLReleaseType, true);
            });

            Utility.SetEnterToExecuteOnec(cmbHBLReleaseType, delegate
            {
                ICPCommUIHelper.SetComboxByEnum<FCMReleaseType>(this.cmbHBLReleaseType, true);
            });

            Utility.SetEnterToExecuteOnec(this.mcmbSales, delegate
            {
                //ICPCommUIHelper.SetMcmbUsersByCompanys(this.mcmbSales);
                List<UserList> userList = userService.GetUnderlingUserList(null, new string[] { "海外拓展", "销售代表", "拓展员", "总裁", "副总裁", "总经理助理", "分公司总经理" }, null, true);

                UserList insertItem = new UserList();
                insertItem.ID = Guid.Empty;
                insertItem.EName = insertItem.CName = string.Empty;
                userList.Insert(0, insertItem);

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                mcmbSales.InitSource<UserList>(userList, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });
            //业务类型
            Utility.SetEnterToExecuteOnec(this.cmbType, delegate
            {
                ICPCommUIHelper.SetComboxByEnum<FCMOperationType>(this.cmbType, true);
            });
            //委托方式
            Utility.SetEnterToExecuteOnec(this.cmbBookingMode, delegate
            {
                ICPCommUIHelper.SetComboxByEnum<FCMBookingMode>(this.cmbBookingMode, false);
            });

            //货物描述
            Utility.SetEnterToExecuteOnec(this.cmbCargoType, delegate
            {
                ICPCommUIHelper.SetComboxByEnum<CargoType>(this.cmbCargoType, true, true);
            });

            mcmbFiler.Enter += new EventHandler(mcmFiler_Click);
            this.mcmbBookinger.Enter += new EventHandler(mcmbBookinger_Click);
            this.mcmbOverseasFiler.Enter += new EventHandler(mcmbOverseasFiler_Enter);
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
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = (Guid)this.cmbCompany.EditValue;
            }

            ICPCommUIHelper.SetComboxUsersByRoles(this.mcmbBookinger, depID, new string[] { "订舱", "客服" }, false);
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
                depID = (Guid)this.cmbCompany.EditValue;
            }

            ICPCommUIHelper.SetComboxUsersByRoles(this.mcmbFiler, depID, new string[] { "文件", "客服" }, true);
        }

        /// <summary>
        /// 填充“海外部客服”的用户列表供选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mcmbOverseasFiler_Enter(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = (Guid)this.cmbCompany.EditValue;
            }

            ICPCommUIHelper.SetComboxUsersByRole(this.mcmbOverseasFiler, depID, "海外拓展", true);
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
            cmbTradeTerm.SelectedIndexChanged += new EventHandler(cmbTradeTerm_SelectedIndexChanged);

            cmbTransportClause.SelectedIndexChanged += delegate
            {
                if (this._shown)
                {
                    this._oceanBookingInfo.TransportClauseName = this.cmbTransportClause.Text;
                    SetPlaceOfDeliveryByTransportClause();
                    SetFinalDestinationByTransportClause();
                }
            };

            if (_oceanBookingInfo.ID == Guid.Empty)
            {

                this.stxtCustomer.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.stxtCustomer_ButtonClick);
            }

            this.cmbOrderNo.Enter += new EventHandler(cmbOrderNo_Enter);
            this.cmbOrderNo.LostFocus += new EventHandler(cmbOrderNo_SelectedIndexChanged);

            this.cmbCargoType.Click += new EventHandler(cmbCargoType_Enter);
            this.cmbCargoType.SelectedIndexChanged += new EventHandler(cmbCargoType_EditValueChanged);
            this.mcmbSales.SelectedRow += new EventHandler(mcmbSales_SelectedRow);

            
            //this.stxtPlaceOfDelivery.TextChanged += new EventHandler(stxtPlaceOfDelivery_TextChanged);
            this.trsSalesDep.Enter += new EventHandler(trsSalesDep_Enter);
           
            this.stxtBookingCustomer.TextChanged += new EventHandler(stxtBookingCustomer_TextChanged);

           
        }

      

     


       

    

     

      

        /// <summary>
        /// 数据时选大船/驳船的时候得来的，所以清空大船/驳船的时候也要清空数据，以保证一致性;
        /// </summary>
        void ClearFourDays()
        {
            dteClosingDate.EditValue = _oceanBookingInfo.ClosingDate = null;
            dteDOCClosingDate.EditValue = _oceanBookingInfo.DOCClosingDate = null;
            dteCYClosingDate.EditValue = _oceanBookingInfo.CYClosingDate = null;
            dtstxtPlaceOfReceipt.EditValue = _oceanBookingInfo.ETD = null;
        }

     

        /// <summary>
        /// 注册界面控件之间联动的事件并立即执行一次
        /// </summary>
        void RegisterRelativeEventsAndRunOnce()
        {
            this.cmbType.SelectedIndexChanged += new EventHandler(cmbType_SelectedIndexChanged);

            this.cmbOrderNo.TextChanged += new EventHandler(cmbOrderNo_TextChanged);
            this.chkIsOnlyMBL.CheckedChanged += new System.EventHandler(this.chkIsOnlyMBL_CheckedChanged);
            this.chkHasContract.CheckedChanged += new System.EventHandler(this.chkHasContract_CheckedChanged);
            this.txtContractNo.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtContractNo_ButtonClick);
            this.cmbSalesType.SelectedIndexChanged += new EventHandler(cmbSalesType_SelectedIndexChanged);

            this.RunAtOnce();
        }

        void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetContainerDemandByBookingType();

            this.chkIsTruck.Enabled = (this._oceanBookingInfo.OEOperationType == FCMOperationType.FCL
                || this._oceanBookingInfo.OEOperationType == FCMOperationType.LCL);

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

        void cmbSalesType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this._shown)
            //{
            this._oceanBookingInfo.SalesTypeName = this.cmbSalesType.Text;
            if (this._oceanBookingInfo.SalesTypeName.Contains("指定货"))//TODO: 英文怎么办?
            {
                this.mcmbOverseasFiler.Enabled = true;
                this.mcmbOverseasFiler.SpecifiedBackColor = Color.White;
            }
            else
            {
                this.mcmbOverseasFiler.SpecifiedBackColor = this.txtNo.BackColor;
                this.mcmbOverseasFiler.Enabled = false;
                this._oceanBookingInfo.OverSeasFilerID = Guid.Empty;
                this._oceanBookingInfo.OverSeasFilerName = string.Empty;
                this.mcmbOverseasFiler.Refresh();
            }
            //}

            SetAgentSourceByCompanyID(_oceanBookingInfo.CompanyID);
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

            if (_oceanBookingInfo.ID != Guid.Empty)
            {
                // 如果订单.订舱单.SO号已产生，则不允许更改业务类型，否则允许更改业务类型。
                if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.OceanShippingOrderID) == false)
                    cmbType.Enabled = false;
                else
                    cmbType.Enabled = true;

                SetContainerDemandByBookingType();
            }

            SetHBLEnabledByIsOnlyMBL(_oceanBookingInfo.IsOnlyMBL);
            SetContractBoxByHasContract(_oceanBookingInfo.IsContract);

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
                //this.stxtReturnLocation.BackColor = Color.White;
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
            SetHBLEnabledByIsOnlyMBL(chkIsOnlyMBL.Checked);
        }
        /// <summary>
        ///IsOnlyMBL如果为选择状态，则HBL相关输入项设为启用状态，否则设为禁用状态
        /// </summary>
        private void SetHBLEnabledByIsOnlyMBL(bool isOnlyMBL)
        {
            if (isOnlyMBL)
            {
                this.groupHBL.Enabled = false;
                _oceanBookingInfo.HBLPaymentTermID = null;
                _oceanBookingInfo.HBLReleaseType = FCMReleaseType.Unknown;
                _oceanBookingInfo.HBLRequirements = string.Empty;
            }
            else
            {
                this.groupHBL.Enabled = true;
                //if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.HBLPaymentTermID))
                //    _oceanBookingInfo.HBLPaymentTermID = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.Payment);
            }
        }

        #endregion

        #region 如果业务类型不是整箱，那么箱描述就不可编辑，否则可编辑

        /// <summary>
        /// 如果业务类型不是整箱，那么箱描述就不可编辑，否则可编辑
        /// </summary>
        private void SetContainerDemandByBookingType()
        {
            if (_oceanBookingInfo.OEOperationType == FCMOperationType.FCL)
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
            if (_oceanBookingInfo.OEOperationType == FCMOperationType.LCL)
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
            this.barAuditAndSave.Enabled = this._oceanBookingInfo.State == OEOrderState.NewOrder;

            if (_oceanBookingInfo.ID == Guid.Empty)
            {

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
                barReject.Enabled = _oceanBookingInfo.State == OEOrderState.NewOrder;

                if (string.IsNullOrEmpty(this._oceanBookingInfo.OceanShippingOrderNo))
                {
                    this.barTruck.Enabled = false;
                    cmbType.Enabled = true;
                }
                else
                {
                    cmbType.Enabled = false;
                    barTruck.Enabled = this._oceanBookingInfo.OEOperationType == FCMOperationType.FCL
                        || this._oceanBookingInfo.OEOperationType == FCMOperationType.LCL;
                }

                this.barRefresh.Enabled = true;

                if (string.IsNullOrEmpty(this._oceanBookingInfo.OceanShippingOrderNo))
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

                if (_oceanBookingInfo.State == OEOrderState.LoadPreVoyage ||
                    _oceanBookingInfo.State == OEOrderState.LoadVoyage ||
                    _oceanBookingInfo.State == OEOrderState.Closed ||
                    _oceanBookingInfo.State == OEOrderState.Rejected)
                {
                    barApplyAgent.Enabled = false;
                }
                else
                {
                    barApplyAgent.Enabled = true;
                }
            }

            this.txtState.Text = EnumHelper.GetDescription<OEOrderState>(this._oceanBookingInfo.State, LocalData.IsEnglish);
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
            //if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.VoyageID))
            //{
            //    this.ClearFourDays(); ;
            //    dteETA.EditValue = _oceanBookingInfo.ETA = null;
            //    return;
            //}

            //VoyageInfo voyageInfo = tfService.GetVoyageInfo(_oceanBookingInfo.VoyageID.Value);
            //dteETA.EditValue = _oceanBookingInfo.ETA = voyageInfo.ETA;

            //if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.PreVoyageID))
            //{
            //    dteClosingDate.EditValue = _oceanBookingInfo.ClosingDate = voyageInfo.ClosingDate;
            //    dteDOCClosingDate.EditValue = _oceanBookingInfo.DOCClosingDate = voyageInfo.DOCClosingDate;
            //    dteCYClosingDate.EditValue = _oceanBookingInfo.CYClosingDate = voyageInfo.CYClosingDate;
            //    dteETD.EditValue = _oceanBookingInfo.ETD = voyageInfo.ETD;
            //}
        }

        /// <summary>
        /// 驳船改变,填充ETA， 截柜日，截关日,截文件日
        /// </summary>
        private void PreVoyageChanged()
        {
            //if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.PreVoyageID))
            //{
            //    this.ClearFourDays();
            //    return;
            //}

            //VoyageInfo voyageInfo = tfService.GetVoyageInfo(_oceanBookingInfo.PreVoyageID.Value);
            //dteClosingDate.EditValue = _oceanBookingInfo.ClosingDate = voyageInfo.ClosingDate;
            //dteDOCClosingDate.EditValue = _oceanBookingInfo.DOCClosingDate = voyageInfo.DOCClosingDate;
            //dteCYClosingDate.EditValue = _oceanBookingInfo.CYClosingDate = voyageInfo.CYClosingDate;
            //dteETD.EditValue = _oceanBookingInfo.ETD = voyageInfo.ETD;
        }

        /// <summary>
        /// 交货地 如果目的港运输条款<>Door，那么就为卸货港
        /// </summary>
        private void SetPlaceOfDeliveryByTransportClause()
        {
            if (!Utility.GuidIsNullOrEmpty(this._oceanBookingInfo.PlaceOfDeliveryID)
                || Utility.GuidIsNullOrEmpty(this._oceanBookingInfo.TransportClauseID)) return;

            if (_oceanBookingInfo.TransportClauseName.Contains("-DOOR") == false)
            {
                stxtPlaceOfDelivery.Tag = _oceanBookingInfo.PlaceOfDeliveryID = _oceanBookingInfo.PODID;
                stxtPlaceOfDelivery.Text = _oceanBookingInfo.PlaceOfDeliveryName = _oceanBookingInfo.PODName;
            }
        }

        /// <summary>
        /// 最终目的地 如果目的港运输条款<>Door，那么就为卸货港
        /// </summary>
        private void SetFinalDestinationByTransportClause()
        {
            if (!Utility.GuidIsNullOrEmpty(_oceanBookingInfo.FinalDestinationID)
                || Utility.GuidIsNullOrEmpty(this._oceanBookingInfo.TransportClauseID)) return;

            if (_oceanBookingInfo.TransportClauseName.Contains("-DOOR") == false)
            {
                stxtFinalDestination.Tag = _oceanBookingInfo.FinalDestinationID = _oceanBookingInfo.PlaceOfDeliveryID;
                stxtFinalDestination.Text = _oceanBookingInfo.FinalDestinationName = _oceanBookingInfo.PlaceOfDeliveryName;
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
            _oceanBookingInfo.SalesDepartmentID = Guid.Empty;
            //cmbOperator.Properties.Items.Clear();
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
                DataDictionaryInfo salesType = oeService.GetSalesType(_oceanBookingInfo.CustomerID, _oceanBookingInfo.CompanyID);
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
                //mcmbFiler.ClearItems();
                //mcmbBookiner.ClearItems();
            }
            else
            {
                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                List<UserList> operators = userService.GetUnderlingUserList(new Guid[] { _oceanBookingInfo.CompanyID }, new string[] { "订舱" }, null, true);
                List<UserList> filers = userService.GetUnderlingUserList(new Guid[] { _oceanBookingInfo.CompanyID }, new string[] { "文件" }, null, true);
                List<UserList> overSeasFilers = userService.GetUnderlingUserList(new Guid[] { _oceanBookingInfo.CompanyID }, new string[] { "海外部客服" }, null, true);

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
            List<UserInfo> users = this.oeService.GetOverseasFilersList(this._oceanBookingInfo.CustomerID, this._oceanBookingInfo.SalesID,
                DateTime.Now.AddDays(-30), DateTime.Now, 1);

            if (users.Count > 0)
            {
                this.mcmbOverseasFiler.ShowSelectedValue(users[0].ID, LocalData.IsEnglish ? users[0].EName : users[0].CName);
            }
        }

        /// <summary>
        /// 当前客户最近业务所对应的文件or 当前客户为新客户and当前揽货人最近业务所对应的文件
        /// </summary>
        void SetDefaultFiler()
        {
            List<UserInfo> users = this.oeService.GetFilersList(this._oceanBookingInfo.CustomerID, this._oceanBookingInfo.SalesID,
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
            SetAgentSourceByCompanyID(_oceanBookingInfo.CompanyID);
            //SetAgetnEnabledByPlaceOfDeliveryAndCompany();

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
                && !oeService.IsCustomerAndCompanySameCountry(_oceanBookingInfo.CustomerID, _oceanBookingInfo.CompanyID))
            {
                stxtAgent.Text = _oceanBookingInfo.AgentName = _oceanBookingInfo.CustomerName;
                stxtAgent.EditValue = _oceanBookingInfo.AgentID = _oceanBookingInfo.CustomerID;
                ICPCommUIHelper.SetCustomerDesByID(_oceanBookingInfo.AgentID, _oceanBookingInfo.AgentDescription);
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
                || oeService.IsPortCountryExistCompanyConfig(_oceanBookingInfo.PlaceOfDeliveryID, null))
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

            bool isFirst = false;
            if (_agentCustomersList == null)
            {
                _agentCustomersList = configureService.GetCompanyAgentList(_oceanBookingInfo.CompanyID, true);
                isFirst = true;
            }

            if (!Utility.GuidIsNullOrEmpty(_oceanBookingInfo.CustomerID) &&
                (string.IsNullOrEmpty(this._oceanBookingInfo.SalesTypeName) == false &&
                        (this._oceanBookingInfo.SalesTypeName.Contains("指定货") ||
                        this._oceanBookingInfo.SalesTypeName.ToUpper().Contains("AGENT"))))
            {
                CustomerList find = (from d in _agentCustomersList where d.ID == _oceanBookingInfo.CustomerID select d).Take(1).SingleOrDefault();
                if (find == null)
                {

                    CustomerList opCustomer = new CustomerList();  //业务的客户
                    opCustomer.ID = _oceanBookingInfo.CustomerID;
                    opCustomer.IsDangerous = true;
                    opCustomer.EName = _oceanBookingInfo.CustomerName;
                    _agentCustomersList.Insert(1, opCustomer);
                }
            }
            else
            {
                CustomerList CustomerFind = (from d in _agentCustomersList where d.IsDangerous == true select d).Take(1).SingleOrDefault();
                if (CustomerFind != null)
                {
                    _agentCustomersList.Remove(CustomerFind);
                    if (_oceanBookingInfo.AgentID == CustomerFind.ID)
                    {
                        _oceanBookingInfo.AgentID = null;
                        _oceanBookingInfo.AgentName = string.Empty;
                        _oceanBookingInfo.AgentDescription = new CustomerDescription();
                    }
                }
            }

            if (isFirst)
            {
                CustomerList emptyCustomer = new CustomerList();
                emptyCustomer.CName = emptyCustomer.EName = string.Empty;
                emptyCustomer.ID = Guid.Empty;
                _agentCustomersList.Insert(0, emptyCustomer);
            }

            SetAgentSource(_agentCustomersList);
        }

        private void SetAgentSource(List<CustomerList> agentCustomers)
        {
            stxtAgent.SetLanguage(LocalData.IsEnglish);
            stxtAgent.DataSource = agentCustomers;
            if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.AgentID))
            {
                _oceanBookingInfo.AgentID = agentCustomers[0].ID;
                stxtAgent.EditValue = _oceanBookingInfo.AgentID;
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
        /// 
        int i = 0;
        private void BulidAgentDescriqitonByID(Guid? id)
        {
            if (Utility.GuidIsNullOrEmpty(id))
            {
                stxtAgent.CustomerDescription = _oceanBookingInfo.AgentDescription = new CustomerDescription();
            }
            else
            {
                if (i != 0)
                {
                    ICPCommUIHelper.SetCustomerDesByID(id, _oceanBookingInfo.AgentDescription);
                }

                stxtAgent.CustomerDescription = _oceanBookingInfo.AgentDescription;
            }
            i++;
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
                ICPCommUIHelper.SetCustomerDesByID(_oceanBookingInfo.ShipperID, _oceanBookingInfo.ShipperDescription);
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
                ICPCommUIHelper.SetCustomerDesByID(_oceanBookingInfo.ConsigneeID, _oceanBookingInfo.ConsigneeDescription);
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
                ICPCommUIHelper.SetCustomerDesByID(_oceanBookingInfo.CustomerID, _oceanBookingInfo.BookingCustomerDescription);
            }
        }

        /// <summary>
        /// 最近业务
        /// </summary>
        private void SetRecentlyOrderListByCustomerAndCompany()
        {
            if (_oceanBookingInfo.CompanyID == Guid.Empty || _oceanBookingInfo.CustomerID == Guid.Empty)
            {
                bsRecentTenOrders.Clear();
            }
            else
            {
                bsRecentTenOrders.Clear();
                List<OceanOrderList> orderList = oeService.GetRecentlyOceanOrderList(_oceanBookingInfo.CompanyID, _oceanBookingInfo.CustomerID, 10);
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
            if (e.Page == tabPagePO)
            {
                this.xtraTabControl1.SelectedPageChanged -= new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControl1_SelectedPageChanged);

                //if (_oceanBookingInfo.ID == Guid.Empty)
                //    _oceanBookingPOInfo = new List<OceanBookingPOList>();
                //else
                //    _oceanBookingPOInfo = oeService.GetOceanOrderPOList(_oceanBookingInfo.ID);

                //this.bookingPOEditPart1.SetSource(_oceanBookingPOInfo);

                this.bookingPOEditPart1.InitData(this._oceanBookingInfo.ID);
                this.bookingPOEditPart1.IsOrderPO = false;
            }
        }

        #endregion

        #region 最近十票业务

        OceanOrderList CurrentOrderList
        {
            get
            {
                if (bsRecentTenOrders.List == null || bsRecentTenOrders.Current == null) return null;
                return bsRecentTenOrders.Current as OceanOrderList;
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
                                                                                              , _oceanBookingInfo.POLID
                                                                                              , _oceanBookingInfo.PODID
                                                                                              , _oceanBookingInfo.PlaceOfDeliveryID
                                                                                              , LocalData.UserInfo.LoginID
                                                                                              , DateTime.Now.AddDays(-30).Date
                                                                                              , Utility.GetEndDate(DateTime.Now)
                                                                                              , 0);

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
            if (this._oceanBookingInfo.IsDirty)
            {
                if (this.cmbOrderNo.SelectedItem != null)
                {
                    ShippingOrderList list = this.cmbOrderNo.SelectedItem as ShippingOrderList;
                    if (list != null)
                    {
                        this._oceanBookingInfo.SODate = list.SODate;
                        this._oceanBookingInfo.CarrierID = list.CarrierID;
                        this._oceanBookingInfo.CarrierName = list.CarrierName;
                        if (list.AgentofcarrierID.HasValue)
                        {
                            this._oceanBookingInfo.AgentOfCarrierID = list.AgentofcarrierID.Value;
                        }
                        this._oceanBookingInfo.AgentOfCarrierName = list.AgentOfCarrierName;
                        this._oceanBookingInfo.ContractID = list.FreightRateID;
                        this._oceanBookingInfo.ContractNo = list.ContractNo;
                        this._oceanBookingInfo.PreVoyageID = list.PreVoyageID;
                        this._oceanBookingInfo.PreVoyageName = list.PreVoyageName;
                        this._oceanBookingInfo.VoyageID = list.VoyageID;
                        this._oceanBookingInfo.VoyageName = list.VoyageName;
                        this._oceanBookingInfo.ETD = list.ETD;
                        this._oceanBookingInfo.ETA = list.ETA;
                        this._oceanBookingInfo.ClosingDate = list.ClosingDate;
                        this._oceanBookingInfo.CYClosingDate = list.CYClosingDate;
                        this._oceanBookingInfo.DOCClosingDate = list.DOCClosingDate;
                        this._oceanBookingInfo.ReturnLocationID = list.ReturnLocationID;
                        this._oceanBookingInfo.ReturnLocationName = list.ReturnLocationName;
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
                OceanBookingInfo order = oeService.GetOceanBookingInfo(CurrentOrderList.ID);
                if (order == null) return;

                if (_oceanBookingInfo!=null&&!Utility.GuidIsNullOrEmpty(_oceanBookingInfo.ID))
                {
                    order.ID = _oceanBookingInfo.ID;
                    order.No = _oceanBookingInfo.No;
                    order.UpdateDate = _oceanBookingInfo.UpdateDate;
                }
                else
                {
                    order.ID = Guid.Empty;
                    order.No = string.Empty;
                    order.State = OEOrderState.NewOrder;
                    order.OceanShippingOrderID = Guid.Empty;
                    order.OceanShippingOrderNo = string.Empty;
                    order.SalesID = LocalData.UserInfo.LoginID;
                    order.SalesName = LocalData.UserInfo.LoginName;
                    order.UpdateDate = null;
                    order.CreateDate = DateTime.Now;
                }



                #region 设置默认值, modifyBy pearl for bug1821:从最近10票业务导入，不需要导入:数量，重量，体积

                order.Quantity = 0;
                DataDictionaryList normalDictionary = null;
                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
                order.QuantityUnitID = normalDictionary.ID;
                order.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                order.Weight = 0.0m;
                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
                order.WeightUnitID = normalDictionary.ID;
                order.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                order.Measurement = 0.0m;
                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
                order.MeasurementUnitID = normalDictionary.ID;
                order.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
                #endregion

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
            //if (this._shown)
            //{
            if (cmbCargoType.EditValue == null)
            {
                this._oceanBookingInfo.CargoDescription = null;
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
            //if (this._oceanBookingInfo.CargoDescription == null)
            //{
            //    this.cmbCargoType.SelectedIndexChanged -= new EventHandler(cmbCargoType_EditValueChanged);
            //    this._oceanBookingInfo.CargoDescription = new CargoDescription();
            //    this.cmbCargoType.SelectedIndexChanged += new EventHandler(cmbCargoType_EditValueChanged);
            //}
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

                if (cargoDescriptionPart1 is ICP.FCM.OceanExport.UI.Common.Controls.AwkwardDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.OceanExport.UI.Common.Controls.AwkwardDescriptionPart();
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

                if (cargoDescriptionPart1 is ICP.FCM.OceanExport.UI.Common.Controls.DangerousDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.OceanExport.UI.Common.Controls.DangerousDescriptionPart();
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

                if (cargoDescriptionPart1 is ICP.FCM.OceanExport.UI.Common.Controls.DryDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.OceanExport.UI.Common.Controls.DryDescriptionPart();
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

                if (cargoDescriptionPart1 is ICP.FCM.OceanExport.UI.Common.Controls.ReeferDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ICP.FCM.OceanExport.UI.Common.Controls.ReeferDescriptionPart();
                    this.navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            cargoDescriptionPart1.SetParentControl(sender, _oceanBookingInfo.CargoDescription, txtCargoDescription);
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
            this._oceanBookingInfo = oeService.GetOceanBookingInfo(orderId);
            origContract = this._oceanBookingInfo.ContractID;
            chargeContract = false;
        }

        void ShowOrder()
        {
            InitData();

            _oceanBookingInfo.BeginEdit();

            this.bsBookingInfo.DataSource = _oceanBookingInfo;
            bsBookingInfo.ResetBindings(false);


            InitMessage();
            InitControls();

            List<OceanBookingFeeList> feelist = null;

            if (_oceanBookingInfo.ID == Guid.Empty)
            {
                feelist = new List<OceanBookingFeeList>();
            }
            else
            {
                feelist = oeService.GetOceanOrderFeeList(_oceanBookingInfo.ID);
            }

            this.orderFeeEditPart1.SetSource(feelist);
        }

        public void BindingData(object data)
        {
            this.SuspendLayout();
            this.orderFeeEditPart1.SetService(Workitem);
            this.bookingPOEditPart1.SetService(Workitem);

            OceanBookingList listInfo = data as OceanBookingList;

            if (listInfo == null)
            {
                //新建
                this._oceanBookingInfo = new OceanBookingInfo();
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

            this.InitalComboxes();

            this.ShowOrder();

            this.SearchRegister();
            this.SetLazyLoaders();

            _oceanBookingInfo.CancelEdit();
            _oceanBookingInfo.BeginEdit();

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

            List<bool> isScrrs = new List<bool> { true, true, true };

            isScrrs[0] = _oceanBookingInfo.Validate
               (
                   delegate(ValidateEventArgs e)
                   {
                       if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.VoyageID) == false || Utility.GuidIsNullOrEmpty(_oceanBookingInfo.PreVoyageID) == false)
                       {
                           //if (string.IsNullOrEmpty(_OceanBookingInfo.ShippingOrderNo))
                           //    e.SetErrorInfo("ShippingOrderNo", LocalData.IsEnglish ? "ShippingOrderNo Must Input" : "订舱号必须输入.");
                       }

                       if (string.IsNullOrEmpty(_oceanBookingInfo.OceanShippingOrderNo) == false)
                       {
                           //if (Utility.GuidIsNullOrEmpty(this._oceanBookingInfo.ReturnLocationID))
                           //{
                           //    e.SetErrorInfo("ReturnLocationID", LocalData.IsEnglish ? "ReturnLocation Must Input" : "还柜地必须输入.");
                           //} 

                           if (Utility.GuidIsNullOrEmpty(this._oceanBookingInfo.AgentOfCarrierID))
                           {
                               e.SetErrorInfo("AgentOfCarrierID", LocalData.IsEnglish ? "AgentOfCarrier Must Input" : "承运人必须输入.");
                           }
                       }

                       if (string.IsNullOrEmpty(_oceanBookingInfo.OceanShippingOrderNo) == false)
                       {
                           if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.VoyageID) && Utility.GuidIsNullOrEmpty(_oceanBookingInfo.PreVoyageID))
                           {
                               e.SetErrorInfo("VoyageID", LocalData.IsEnglish ? "Voyage or PreVoyage Must Input" : "大船和驳船至少要填写一个");
                           }
                       }

                       if (!string.IsNullOrEmpty(this._oceanBookingInfo.OceanShippingOrderNo))
                       {
                           if (Utility.GuidIsNullOrEmpty(this._oceanBookingInfo.CarrierID))
                           {
                               e.SetErrorInfo("CarrierID", LocalData.IsEnglish ? "Carrier Must Input" : "船公司必须输入.");
                           }

                           if (!this._oceanBookingInfo.SODate.HasValue)
                           {
                               e.SetErrorInfo("SODate", LocalData.IsEnglish ? "SODate Must Input" : "确认日期必须输入.");
                           }
                       }

                       if (_oceanBookingInfo.IsContract)
                       {
                           if (Utility.GuidIsNullOrEmpty(_oceanBookingInfo.ContractID))
                           {
                               e.SetErrorInfo("ContractID", LocalData.IsEnglish ? "Contract Must Input" : "合约必须输入.");
                           }
                       }

                       if (_oceanBookingInfo.POLID != Guid.Empty && _oceanBookingInfo.POLID == _oceanBookingInfo.PODID)
                           e.SetErrorInfo("PODID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");

                       if (_oceanBookingInfo.ETA != null && _oceanBookingInfo.ETD != null
                           && _oceanBookingInfo.ETD >= _oceanBookingInfo.ETA)
                           e.SetErrorInfo("ETA", LocalData.IsEnglish ? "ETD can't bigger ETA." : "ETD不能大于ETA.");

                       if (_oceanBookingInfo.ExpectedShipDate != null && _oceanBookingInfo.ExpectedShipDate.Value != null && _oceanBookingInfo.ExpectedArriveDate != null
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

                       if (this._oceanBookingInfo.SODate.HasValue && this._oceanBookingInfo.CYClosingDate.HasValue)
                       {
                           if (this._oceanBookingInfo.SODate.Value >= this._oceanBookingInfo.CYClosingDate.Value)
                           {
                               e.SetErrorInfo("SODate", LocalData.IsEnglish ? "Confirmed data must ealier than CYClosing date." : "确认日必须小于截柜日.");
                           }
                       }

                       if (this._oceanBookingInfo.ContainerDescription != null)
                       {
                           if (this._oceanBookingInfo.ContainerDescription.ToString() != this.containerDemandControl1.Text)
                           {
                               this._oceanBookingInfo.IsDirty = true;
                           }
                       }
                       //果选择整箱业务类型，箱需求必输；箱需求逻辑,点击对应的箱型n次,则显示n*箱型
                       if (_oceanBookingInfo.OEOperationType == FCMOperationType.FCL)
                       {
                           if (this.containerDemandControl1.Text.Trim().Length == 0)
                           {
                               e.SetErrorInfo("ContainerDescription", LocalData.IsEnglish ? "FCL Bussines Must Input." : "整箱业务必须输入箱需求.");
                               isScrrs[0] = false;
                           }

                       }
                       //把箱需求转换成对象
                       _oceanBookingInfo.ContainerDescription = new ContainerDescription(this.containerDemandControl1.Text.Trim());
                   }
               );

            #region ContainerDemand




            #endregion

            #region childParts

            if (bookingPOEditPart1.ValidateData() == false)
            {
                isScrrs[1] = false;
                xtraTabControl1.SelectedTabPageIndex = 1;
            }

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
            Save(this._oceanBookingInfo, false);
        }

        private bool Save(OceanBookingInfo currentData, bool isSavingAs)
        {
            if (ValidateData() == false)
            {
                return false;
            }

            if (!currentData.IsDirty && !currentData.IsNew && !this.orderFeeEditPart1.IsChanged && !this.bookingPOEditPart1.IsChanged)
            {
                return true;
            }

            try
            {
                barSave.Enabled = false;

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Bookins Saveing......" : "订舱信息保存中.....");

                BookingSaveRequest originalBooking = null;
                if (Utility.GuidIsNullOrEmpty(currentData.ID) || Utility.GuidIsNullOrEmpty(currentData.OceanShippingOrderID))
                {
                    originalBooking = SaveOceanBooking(currentData);
                }
                else if (_oceanBookingInfo.IsDirty)
                {
                    if (_oceanBookingInfo.BookingDate != null)
                        _oceanBookingInfo.State = OEOrderState.BookingConfirmed;

                    originalBooking = SaveOceanBooking(_oceanBookingInfo);

                }

                List<FeeSaveRequest> originalFees = this.orderFeeEditPart1.SaveFee(currentData.ID);

                List<POSaveRequest> originalPos = this.bookingPOEditPart1.SavePO(currentData.ID);

                Dictionary<Guid, SaveResponse> saved = this.oeService.SaveOceanBookingWithTrans(originalBooking,
                    originalFees, originalPos);

                isSave = true;
                _oceanBookingInfo.IsDirty = false;

                if (originalBooking != null)
                {
                    if (originalBooking.freightRateID != origContract)
                    {
                        origContract = originalBooking.freightRateID;
                        chargeContract = true;
                    }

                    SaveResponse.Analyze(new List<SaveRequest> { originalBooking }, saved, true);
                    this.RefreshUI(originalBooking);

                }

                if (originalFees != null)
                {
                    SaveResponse.Analyze(originalFees.Cast<SaveRequest>().ToList(), saved, true);
                    this.orderFeeEditPart1.RefreshUI(originalFees);
                }

                if (originalPos != null)
                {
                    SaveResponse.Analyze(originalPos.Cast<SaveRequest>().ToList(), saved, true);
                    this.bookingPOEditPart1.RefreshUI(originalPos);
                }

                #region 合约号发生改变时,更新账单
                if (chargeContract && originalBooking.IsCreateBill)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Bill Saveing......" : "正在生成账单.....");

                    try
                    {
                        SingleResult result = this.oeService.CreateBill(originalBooking.id.Value, LocalData.UserInfo.LoginID);
                        if (result != null)
                        {
                            int s = result.GetValue<Byte>("State");
                            if (s == 1)
                            {
                                Utility.ShowMessage(NativeLanguageService.GetText(this, "CreateBills"));

                                chargeContract = false;
                            }
                            else if (s == 2)
                            {
                                string message = result.GetValue<string>("Message");

                                string title = LocalData.IsEnglish ? "Generate the bill Error:" : "生成账单失败：";
                                Utility.ShowMessage(title + message);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "系统根据合约生成账单的时,遇到错误：" + ex.Message);
                    }

                }
                chargeContract = false;
                #endregion

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
            finally { barSave.Enabled = true; }
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
                this._oceanBookingInfo.CarrierName = this._oceanBookingInfo.CarrierID == Guid.Empty ?
                    string.Empty : this.mcmbCarrier.EditText;
                this._oceanBookingInfo.OEOperationTypeDescription = EnumHelper.GetDescription<FCMOperationType>(this._oceanBookingInfo.OEOperationType, LocalData.IsEnglish);
                this._oceanBookingInfo.FilerName = this._oceanBookingInfo.FilerId.ToGuid() == Guid.Empty ?
                    string.Empty : this.mcmbFiler.Text;
                this._oceanBookingInfo.BookingerName = this._oceanBookingInfo.BookingerID.ToGuid() == Guid.Empty ?
                    string.Empty : this.mcmbBookinger.Text;
                this._oceanBookingInfo.OverSeasFilerName = this._oceanBookingInfo.OverSeasFilerID.ToGuid() == Guid.Empty ?
                    string.Empty : this.mcmbOverseasFiler.Text;
                this._oceanBookingInfo.VesselVoyage = this.stxtPreVoyage.Text + (this.stxtPreVoyage.Text.Trim().Length > 0 ? ";" : "") + this.stxtVoyage.Text;
                Saved(new object[] { (OceanBookingList)_oceanBookingInfo });

                this._oceanBookingInfo.IsDirty = false;
            }
        }

        /// <summary>
        /// 客户参考号暂时传空值
        /// </summary>
        /// <param name="currentData"></param>
        private BookingSaveRequest SaveOceanBooking(OceanBookingInfo currentData)
        {
            this.EndEdit();

            if (currentData.IsDirty == true || currentData.IsNew)
            {
                BookingSaveRequest saveRequest = new BookingSaveRequest();

                saveRequest.id = currentData.ID;
                saveRequest.customerRefNo = string.Empty;
                saveRequest.customerID = currentData.CustomerID;
                saveRequest.tradeTermID = currentData.TradeTermID;
                saveRequest.oeOperationType = currentData.OEOperationType;
                saveRequest.companyID = currentData.CompanyID;
                saveRequest.bookingerID = currentData.BookingerID;
                saveRequest.filerID = currentData.FilerId;
                saveRequest.overSeasFilerId = currentData.OverSeasFilerID;
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
                saveRequest.mblPaymentTermID = currentData.MBLPaymentTermID;
                saveRequest.hblPaymentTermID = currentData.HBLPaymentTermID;
                saveRequest.isTruck = currentData.IsTruck;
                saveRequest.isCustoms = currentData.IsCustoms;
                saveRequest.isCommodityInspection = currentData.IsCommodityInspection;
                saveRequest.isQuarantineInspection = currentData.IsQuarantineInspection;
                saveRequest.isWarehouse = currentData.IsWareHouse;
                saveRequest.isOnlyMBL = currentData.IsOnlyMBL;
                saveRequest.mblReleaseType = currentData.MBLReleaseType;
                saveRequest.hblReleaseType = currentData.HBLReleaseType;
                saveRequest.mblRequirements = currentData.MBLRequirements;
                saveRequest.hblRequirements = currentData.HBLRequirements;
                saveRequest.remark = currentData.Remark;
                saveRequest.finalDestinationID = currentData.FinalDestinationID;
                saveRequest.returnLocationID = currentData.ReturnLocationID;
                saveRequest.warehouseID = currentData.WarehouseID;
                saveRequest.closingWarehousedate = currentData.ClosingWarehousedate;
                saveRequest.saveByID = LocalData.UserInfo.LoginID;
                saveRequest.oceanShippingOrderUpdateDate = currentData.OceanShippingOrderUpdateDate;
                saveRequest.oceanOrderUpdateDate = currentData.UpdateDate;
                saveRequest.ClosingDate = currentData.ClosingDate;
                saveRequest.CYClosingDate = currentData.CYClosingDate;
                saveRequest.DOCClosingDate = currentData.DOCClosingDate;
                saveRequest.ETA = currentData.ETA;
                saveRequest.ETD = currentData.ETD;
                saveRequest.PreETD = currentData.PORETD;
                if (!saveRequest.isContract)
                {
                    saveRequest.freightRateID = currentData.ContractID = null;
                }

                if (currentData.IsNew)
                {
                    ///新增的订舱单，不需要产生账单
                    saveRequest.IsCreateBill = false;
                }
                else
                {
                    saveRequest.IsCreateBill = currentData.IsCreateBill;
                }
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

            OceanBookingInfo currentData = saveRequest.UnBoxInvolvedObject<OceanBookingInfo>()[0];

            currentData.ID = result.GetValue<Guid>("ID");
            currentData.No = result.GetValue<string>("No");
            currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            currentData.OceanShippingOrderUpdateDate = result.GetValue<DateTime?>("OceanShippingOrderUpdateDate");
            currentData.OceanShippingOrderID = result.GetValue<Guid>("OceanShippingOrderID");
            currentData.State = (OEOrderState)result.GetValue<byte>("State");


            currentData.IsDirty = false;
            if (currentData.ShipperDescription != null)
            {
                currentData.ShipperDescription.IsDirty = false;
            }
            if (currentData.ConsigneeDescription != null)
            {
                currentData.ConsigneeDescription.IsDirty = false;
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
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save as a new booking order successfully. Ref. NO. is " + this._oceanBookingInfo.No + "." : "已成功另存为一票新订单，业务号为" + this._oceanBookingInfo.No + "。");
                if (Saved != null)
                {
                    Saved(new object[] { this._oceanBookingInfo });
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

            OceanBookingInfo orderInfo = Utility.Clone<OceanBookingInfo>(this._oceanBookingInfo);
            orderInfo.ID = Guid.Empty;
            orderInfo.No = string.Empty;

            if (string.IsNullOrEmpty(orderInfo.OceanShippingOrderNo))
            {
                orderInfo.State = OEOrderState.NewOrder;
            }
            else
            {
                orderInfo.State = OEOrderState.BookingConfirmed;
            }

            orderInfo.CreateByID = LocalData.UserInfo.LoginID;
            orderInfo.CreateByName = LocalData.UserInfo.LoginName;
            orderInfo.CreateDate = DateTime.Now;
            orderInfo.BookingDate = DateTime.Now;
            orderInfo.UpdateDate = null;

            if (orderInfo.OEOperationType == FCMOperationType.LCL)
            {
            }
            else
            {
                orderInfo.OceanShippingOrderID = Guid.Empty;
                orderInfo.OceanShippingOrderNo = string.Empty;
            }

            orderInfo.IsDirty = true;
            this.bookingPOEditPart1.InitData(this._oceanBookingInfo.ID);

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
            if (!this.Save(this._oceanBookingInfo, false))
            {
                return;
            }

            if (this._oceanBookingInfo.State != OEOrderState.NewOrder)
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
                    SingleResult result = oeService.ChangeOceanOrderStateWithTargetState(_oceanBookingInfo.ID, OEOrderState.Checked, prams[0].ToString(), LocalData.UserInfo.LoginID, _oceanBookingInfo.UpdateDate);

                    ICP.Framework.CommonLibrary.Logger.Log.Info(result);

                    this._oceanBookingInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    this._oceanBookingInfo.State = (OEOrderState)result.GetValue<byte>("State");

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

            OceanExportPrintHelper.PrintOEBookingConfirmation(_oceanBookingInfo.ID);
        }

        /// <summary>
        /// 利润表打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barProfit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!SaveData())
            //{
            //    return;
            //}
            //OceanExportPrintHelper.PrintOEBookingProfit(_oceanBookingInfo);
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

            OceanExportPrintHelper.PrintOEOrder(this._oceanBookingInfo.ID);
        }

        private void barPrintInWarehouse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!SaveData())
            {
                return;
            }

            throw new NotImplementedException(LocalData.IsEnglish ? "To be defined on next version." : "本版本暂不提供入仓通知单打印功能。");
        }

        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (_oceanBookingInfo == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.AirImport;
            message.UserProperties.OperationId = _oceanBookingInfo.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = _oceanBookingInfo.ID;

            return message;
        }

        #endregion

        #region 打回

        private void barReject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_oceanBookingInfo.State == OEOrderState.Rejected) return;

            Common.Parts.EditRemarkPart editRemarkPart = Workitem.Items.AddNew<Common.Parts.EditRemarkPart>();
            editRemarkPart.LabRemark = LocalData.IsEnglish ? "Reject reason" : "打回原因";
            editRemarkPart.RemartRequired = true;
            editRemarkPart.Saved += delegate(object[] prams)
            {
                try
                {
                    bool isDirty = _oceanBookingInfo.IsDirty;
                    SingleResult result = oeService.ChangeOceanOrderStateWithTargetState(_oceanBookingInfo.ID, OEOrderState.Rejected, prams[0].ToString(), LocalData.UserInfo.LoginID, _oceanBookingInfo.UpdateDate);

                    this._oceanBookingInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    this._oceanBookingInfo.State = (OEOrderState)result.GetValue<byte>("State");

                    this.TriggerSavedEvent();

                    RefreshBarEnabled();
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
            };
            string title = LocalData.IsEnglish ? "Reject Order" : "打回订单";
            PartLoader.ShowDialog(editRemarkPart, title);
        }

        #endregion

        #region EDI格式
        /// <summary>
        /// EDI格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                RefreshData(this._oceanBookingInfo.ID);
                chargeContract = false;
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
            if (this._oceanBookingInfo.IsDirty && isSave == false)
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
                stxtWarehouse,
                stxtReturnLocation,
            });

            Utility.SetPortTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtPlaceOfDelivery,
                stxtPlaceOfReceipt ,
                stxtFinalDestination ,
                stxtPOD,
                stxtPOL,
            });


            this.SmartPartClosing += new EventHandler<WorkspaceCancelEventArgs>(BookingBaseEditPart_SmartPartClosing);
            this.ActivateSmartPartClosingEvent(this.Workitem);


            _oceanBookingInfo.IsDirty = false;

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
                fcmCommonClientService.OpenAgentRequestPart(this._oceanBookingInfo.ID, ICP.Framework.CommonLibrary.Common.OperationType.OceanExport, Workitem);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
        }

        private void barTruck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            List<OceanTruckInfo> truckList = oeService.GetOceanTruckServiceList(this._oceanBookingInfo.ID);
            SingleResult recentData = oeService.GetTruckRecentData(this._oceanBookingInfo.ID);

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
            PartLoader.ShowEditPart<Booking.OceanTruckEditPart>(Workitem,
                truckList,
                stateValues,
                Booking.BookingListPart.GetLineNo(this._oceanBookingInfo),
                null,
                Booking.OEBookingCommandConstants.Command_Truck + _oceanBookingInfo.ID.ToString());
        }

        private void txtContractNo_Click(object sender, EventArgs e)
        {
            Guid? placeOfReceiptID = Guid.Empty;
            Guid polid = Guid.Empty;
            Guid podid = Guid.Empty;
            Guid? deleveryid = Guid.Empty;
            Guid? finalDestinationID = Guid.Empty;
            Guid? freightId = _oceanBookingInfo.ContractID;
            if (_oceanBookingInfo.POLID == Guid.Empty || _oceanBookingInfo.POLID == null ||
                _oceanBookingInfo.PODID == Guid.Empty || _oceanBookingInfo.PODID == null ||
                _oceanBookingInfo.PlaceOfDeliveryID == Guid.Empty || _oceanBookingInfo.PlaceOfDeliveryID == null)
            {
                Utility.ShowMessage(LocalData.IsEnglish ? "pol、pod、delivery must  input" : "起运港、目的港、交货地必须填写"); return;
            }
            else
            {
                if (!Utility.GuidIsNullOrEmpty(_oceanBookingInfo.PlaceOfReceiptID))
                {
                    placeOfReceiptID = _oceanBookingInfo.PlaceOfReceiptID.Value;
                }
                polid = _oceanBookingInfo.POLID;
                podid = _oceanBookingInfo.PODID;
                deleveryid = _oceanBookingInfo.PlaceOfDeliveryID;
                finalDestinationID = _oceanBookingInfo.FinalDestinationID;
            }
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {

                    FreightDataList freightDataList =
                        oeService.GetFreight(_oceanBookingInfo.ContractNo == "无" ? string.Empty : _oceanBookingInfo.ContractNo,
                        _oceanBookingInfo.CarrierID,
                        placeOfReceiptID,
                        polid,
                        deleveryid,
                        null,
                        string.Empty,
                        DateTime.Now,
                        DateTime.Now,
                        freightId);

                    FreightInfo freight = Workitem.Items.AddNew<FreightInfo>();

                    FreightParameter freightParameter = new FreightParameter();
                    freightParameter.prices = freightDataList.DataList;
                    freightParameter.unitList = freightDataList.UnitList;
                    freightParameter.scno = _oceanBookingInfo.ContractNo;
                    freightParameter.shipownerid = _oceanBookingInfo.CarrierID;
                    freightParameter.placeOfReceiptID = placeOfReceiptID;
                    freightParameter.polid = polid;
                    freightParameter.podid = deleveryid;
                    freightParameter.finalDestinationID = finalDestinationID;
                    freightParameter.goodsdes = string.Empty;
                    freightParameter.freightID = _oceanBookingInfo.ContractID;
                    freightParameter.etd = this.dtstxtPOL.DateTime;


                   freight.SetDataSource(freightParameter);

                    string title = LocalData.IsEnglish ? "Select Price" : "选择运价";

                    if (DialogResult.OK == PartLoader.ShowDialog(freight, title, FormBorderStyle.FixedSingle, FormWindowState.Maximized, true, false))
                    {
                        FreightList SelectedPrice = freight.SelectedPrice;
                        if (SelectedPrice != null)
                        {
                            bool needChangeCNS = (_oceanBookingInfo.ContractID != SelectedPrice.OceanId);

                            _oceanBookingInfo.ContractID = SelectedPrice.ID;
                            txtContractNo.Text = _oceanBookingInfo.ContractNo = SelectedPrice.FreightNo;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }

        }       
    }
}