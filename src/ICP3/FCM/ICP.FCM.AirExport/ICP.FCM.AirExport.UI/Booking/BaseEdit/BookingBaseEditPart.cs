using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.AirExport.UI.Common.Controls;
using ICP.FCM.AirExport.UI.Common.Parts;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface.CompositeObjects;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.Controls;
using ICP.Common.UI;
using ICP.Operation.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;

namespace ICP.FCM.AirExport.UI.Booking
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class BookingBaseEditPart : BaseEditPart
    {
        #region 服务注入

        [ServiceDependency]
        public IFCMCommonClientService FCMCommonClientService { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IAirExportService AirExportService
        {
            get
            {
                return ServiceClient.GetService<IAirExportService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        ISystemService SystemService
        {
            get
            {
                return ServiceClient.GetService<ISystemService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        #endregion

        #region 本地变量
        /// <summary>
        /// 委托方式是否已被初始化
        /// </summary>
        bool _IsInitBookingMode = false;
        /// <summary>
        /// 贸易条款是否已被初始化
        /// </summary>
        bool _IsInitTradeTerm = false;
        /// <summary>
        /// 业务员是否变更
        /// </summary>
        bool isChangeSales = false;
        /// <summary>
        /// 当前编辑数据
        /// </summary>
        AirBookingInfo _CurrentData = null;
        /// <summary>
        /// 缓存当前编辑数据
        /// </summary>
        AirBookingInfo _CacheCurrentData;
        /// <summary>
        /// 缓存国家列表,只获取一次.现只用于客户弹出式描述框
        /// </summary>
        List<CountryList> _countryList = null;
        /// <summary>
        /// 邮件中心与ICP业务关联信息
        /// </summary>
        BusinessOperationParameter _businessOperationParameter = null;
        /// <summary>
        /// 委托列表
        /// </summary>
        List<BookingDelegate> _CacheDelegate = null;
        /// <summary>
        /// 委托列表
        /// </summary>
        List<BookingDelegate> CacheDelegate
        {
            get
            {
                if (_CacheDelegate == null)
                {
                    _CacheDelegate = new List<BookingDelegate>();
                }
                return _CacheDelegate;
            }
            set
            {
                _CacheDelegate = value;
            }
        }
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BookingBaseEditPart()
        {
            InitializeComponent();
            SyncLocalData = true;
            if (DesignMode)
            {
                return;
            }
            if (LocalData.IsEnglish == false)
            {
                SetCnText();
            }

            Disposed += new EventHandler(BookingBaseEditPart_Disposed);
            Load += new EventHandler(BookingBaseEditPart_Load);
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
            labBookinger.Text = "订舱";
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
            barAuditAndSave.Caption = "审核并保存";

            barSubPrint.Caption = "打印";
            barPrintOrder.Caption = "业务联单";
            barPrintBookingConfirm.Caption = "订舱确认书";
            barPrintInWarehouse.Caption = "进仓通知书";
            barClose.Caption = "关闭(&C)";

            barReject.Caption = "打回(&J)";
            barTruck.Caption = "派车";
            barApplyAgent.Caption = "申请代理";
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
            newData.BookingDate = newData.CreateDate = DateTime.Now;
            newData.BookingMode = FCMBookingMode.Fax;
            newData.State = AEOrderState.NewOrder;
            newData.AgentID = Guid.Empty;
            newData.BookingerID = LocalData.UserInfo.LoginID;
            newData.BookingerName = LocalData.UserInfo.LoginName;
            newData.IsContract = true;
            newData.IsValid = true;

            #region 设置默认值
            DataDictionaryList normalDictionary = null;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
            newData.QuantityUnitID = normalDictionary.ID;
            newData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
            newData.WeightUnitID = normalDictionary.ID;
            newData.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
            newData.MeasurementUnitID = normalDictionary.ID;
            newData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
            #endregion

            _CurrentData = newData;

            _CurrentData.HBLReleaseType = _CurrentData.MBLReleaseType = FCMReleaseType.Unknown;

            // TODO: 这种Guard型的逻辑要在最开始的时候完成
            Utility.EnsureDefaultCompanyExists(UserService);

            _CurrentData.CompanyID = LocalData.UserInfo.DefaultCompanyID;
            _CurrentData.CompanyName = LocalData.UserInfo.DefaultCompanyName;

            gvOrders.DoubleClick += new EventHandler(gvOrders_DoubleClick);
        }

        #endregion

        #region 复制订舱单时的逻辑

        /// <summary>
        /// 复制订舱单时的逻辑
        /// </summary>
        void PrepareForCopyExistOrder()
        {
            _CurrentData.ID = Guid.Empty;
            _CurrentData.No = string.Empty;
            _CurrentData.MBLNo = _CurrentData.HBLNo = string.Empty;
            _CurrentData.SalesID = LocalData.UserInfo.LoginID;
            _CurrentData.SalesName = LocalData.UserInfo.LoginName;
            _CurrentData.CreateDate = DateTime.Now;
            _CurrentData.AgentID = Guid.Empty;
            _CurrentData.IsContract = false;
            _CurrentData.BookingerID = LocalData.UserInfo.LoginID;
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

            if (_CurrentData.ShipperDescription == null)
            {
                _CurrentData.ShipperDescription = new CustomerDescription();
            }

            if (_CurrentData.ConsigneeDescription == null)
            {
                _CurrentData.ConsigneeDescription = new CustomerDescription();
            }

            if (_CurrentData.AgentDescription == null)
            {
                _CurrentData.AgentDescription = new CustomerDescription();
            }

            if (_CurrentData.BookingCustomerDescription == null)
            {
                _CurrentData.BookingCustomerDescription = new CustomerDescription();
            }
        }

        #region 初始化货物对象

        /// <summary>
        /// 初始化货物对象
        /// </summary>
        private void InitCargoObject()
        {
            if (_CurrentData.CargoType.HasValue
                && _CurrentData.CargoDescription != null
                && _CurrentData.CargoDescription.Cargo != null)
            {
                if (_CurrentData.CargoDescription.Cargo is DangerousCargo)
                    cmbCargoType.EditValue = CargoType.Dangerous;
                else if (_CurrentData.CargoDescription.Cargo is AwkwardCargo)
                    cmbCargoType.EditValue = CargoType.Awkward;
                else if (_CurrentData.CargoDescription.Cargo is ReeferCargo)
                    cmbCargoType.EditValue = CargoType.Reefer;
                else if (_CurrentData.CargoDescription.Cargo is DryCargo)
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
            //委托方式
            cmbBookingMode.ShowSelectedValue(_CurrentData.BookingMode,
                EnumHelper.GetDescription<FCMBookingMode>(_CurrentData.BookingMode, LocalData.IsEnglish));
            //操作口岸
            cmbCompany.ShowSelectedValue(_CurrentData.CompanyID, _CurrentData.CompanyName);
            //贸易条款
            cmbTradeTerm.ShowSelectedValue(_CurrentData.TradeTermID, _CurrentData.TradeTermName);
            //包装
            cmbQuantityUnit.ShowSelectedValue(_CurrentData.QuantityUnitID, _CurrentData.QuantityUnitName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.QuantityUnitID)
                && cmbQuantityUnit.EditValue != null)
            {
                _CurrentData.QuantityUnitID = (Guid)cmbQuantityUnit.EditValue;
            }
            //重量
            cmbWeightUnit.ShowSelectedValue(_CurrentData.WeightUnitID, _CurrentData.WeightUnitName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.WeightUnitID)
                && cmbWeightUnit.EditValue != null)
            {
                _CurrentData.WeightUnitID = (Guid)cmbWeightUnit.EditValue;
            }
            //体积
            cmbMeasurementUnit.ShowSelectedValue(_CurrentData.MeasurementUnitID, _CurrentData.MeasurementUnitName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.MeasurementUnitID)
                && cmbMeasurementUnit.EditValue != null)
            {
                _CurrentData.MeasurementUnitID = (Guid)cmbMeasurementUnit.EditValue;
            }
            mcmbSales.ShowSelectedValue(_CurrentData.SalesID, _CurrentData.SalesName);
            //揽货类型
            cmbSalesType.ShowSelectedValue(_CurrentData.SalesTypeID, _CurrentData.SalesTypeName);
            //揽货部门
            trsSalesDep.ShowSelectedValue(_CurrentData.SalesDepartmentID, _CurrentData.SalesDepartmentName);
            //3个付款方式
            cmbPaymentTerm.ShowSelectedValue(_CurrentData.PaymentTermID, _CurrentData.PaymentTermName);
            //航线
            cmbShippingLine.ShowSelectedValue(_CurrentData.ShippingLineID, _CurrentData.ShippingLineName);
            //航空公司
            mcmbAirCompany.ShowSelectedValue(_CurrentData.AirCompanyId, _CurrentData.AirCompanyName);
            //航班号
            cmbFlightNo.ShowSelectedValue(_CurrentData.FilightId, _CurrentData.FilightNo);
            //运输条款
            cmbTransportClause.ShowSelectedValue(_CurrentData.TransportClauseID, _CurrentData.TransportClauseName);

            //货物描述
            if (_CurrentData.CargoDescription != null
                && _CurrentData.CargoDescription.Cargo != null)
            {
                txtCargoDescription.Text = _CurrentData.CargoDescription.Cargo.ToString(LocalData.IsEnglish);
            }

            orderFeeEditPart1.SetCompanyID(_CurrentData.CompanyID);

            mcmbFiler.ShowSelectedValue(_CurrentData.FilerId, _CurrentData.FilerName);

            mcmbBookinger.ShowSelectedValue(_CurrentData.BookingerID, _CurrentData.BookingerName);

            if (_CurrentData.CargoType.HasValue)
            {
                cmbCargoType.ShowSelectedValue(_CurrentData.CargoType,
                    EnumHelper.GetDescription<CargoType>(_CurrentData.CargoType.Value, LocalData.IsEnglish));
            }

            txtState.Text = EnumHelper.GetDescription<AEOrderState>(_CurrentData.State, LocalData.IsEnglish);
            InitalComboxes();
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
            DataFindClientService.Register(stxtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.CustomerResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          Guid oldCustomerId = _CurrentData.CustomerID;
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
                              if (approved.Value == CustomerCodeApplyState.Processing)
                              {
                                  DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "The customers has not been approved!" : "该客户尚未通过审核!"
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
                                      XtraMessageBox.Show(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.OK
                  , MessageBoxIcon.Question);
                                      return;
                                  }

                                  DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "The customer have not yet applied for the code. Whether to apply the code?" : "该客户尚未申请代码，是否要申请代码?"
              , LocalData.IsEnglish ? "Tip" : "提示"
              , MessageBoxButtons.YesNo
              , MessageBoxIcon.Question);
                                  if (result == DialogResult.Yes)
                                  {
                                      CustomerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
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
                                      XtraMessageBox.Show(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.OK
                  , MessageBoxIcon.Question);
                                      return;
                                  }

                                  DialogResult result = XtraMessageBox.Show("该客户尚未通过审核，若重新申请代码需要去完善客户资料。是否重新申请代码?"
              , LocalData.IsEnglish ? "Tip" : "提示"
              , MessageBoxButtons.YesNo
              , MessageBoxIcon.Question);
                                  if (result == DialogResult.Yes)
                                  {
                                      CustomerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                        LocalData.UserInfo.LoginID,
                                                                        LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                        (DateTime?)resultData[9]);
                                  }

                                  return;
                              }
                          }

                          stxtCustomer.EditValue = _CurrentData.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                          stxtCustomer.Tag = _CurrentData.CustomerID = new Guid(resultData[0].ToString());

                          if (oldCustomerId != Guid.Empty && _CurrentData.CustomerID == oldCustomerId) return;

                          CustomerType customerType = (CustomerType)resultData[4];

                          CustomerChanged(customerType);


                      }, delegate
                      {
                          stxtCustomer.Text = _CurrentData.CustomerName = string.Empty;
                          stxtCustomer.Tag = _CurrentData.CustomerID = Guid.Empty;
                          stxtCustomer.ClosePopup();
                          CustomerChanged(null);
                      },
                      ClientConstants.MainWorkspace);

            DataFindClientService.Register(stxtAgentOfCarrier, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                GetConditionsForAgentOfCarrier,
                delegate(object inputSource, object[] resultData)
                {
                    stxtAgentOfCarrier.Text = _CurrentData.AgentOfCarrierName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    stxtAgentOfCarrier.Tag = _CurrentData.AgentOfCarrierID = new Guid(resultData[0].ToString());
                },
               delegate()
               {
                   stxtAgentOfCarrier.Text = string.Empty;
                   stxtAgentOfCarrier.Tag = Guid.Empty;
               }, ClientConstants.MainWorkspace);

            #endregion

            #region 订舱客户
            DataFindClientService.Register(stxtBookingCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.CustomerResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          Guid oldBookingCustomerID = _CurrentData.BookingCustomerID == null ? Guid.Empty : _CurrentData.BookingCustomerID.Value;
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
                                  DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "The customers has not been approved!" : "该客户尚未通过审核!"
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
                                      XtraMessageBox.Show(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.OK
                  , MessageBoxIcon.Question);

                                      return;
                                  }

                                  DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "The customer have not yet applied for the code. Whether to apply the code?" : "该客户尚未申请代码，是否要申请代码?"
              , LocalData.IsEnglish ? "Tip" : "提示"
              , MessageBoxButtons.YesNo
              , MessageBoxIcon.Question);
                                  if (result == DialogResult.Yes)
                                  {
                                      CustomerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
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
                                      XtraMessageBox.Show(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.OK
                  , MessageBoxIcon.Question);

                                      return;
                                  }

                                  DialogResult result = XtraMessageBox.Show("该客户尚未通过审核，若重新申请代码需要去完善客户资料。是否重新申请代码?"
              , LocalData.IsEnglish ? "Tip" : "提示"
              , MessageBoxButtons.YesNo
              , MessageBoxIcon.Question);
                                  if (result == DialogResult.Yes)
                                  {
                                      CustomerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                        LocalData.UserInfo.LoginID,
                                                                        LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                        (DateTime?)resultData[9]);
                                  }

                                  return;
                              }
                          }

                          stxtBookingCustomer.Tag = _CurrentData.BookingCustomerID = new Guid(resultData[0].ToString());
                          stxtBookingCustomer.Text = _CurrentData.BookingCustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();

                      }, delegate
                      {
                          stxtBookingCustomer.Tag = _CurrentData.BookingCustomerID = Guid.Empty;
                          stxtBookingCustomer.Text = _CurrentData.BookingCustomerName = string.Empty;
                      },
                      ClientConstants.MainWorkspace);
            #endregion

            #region SCNA

            //shipper
            Utility.SetEnterToExecuteOnec(stxtShipper, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                shipperBridge = new CustomerFinderBridge(
               stxtShipper,
               _countryList,
               DataFindClientService,
               CustomerService,
               _CurrentData.ShipperDescription,
               ICPCommUIHelper,
               LocalData.IsEnglish);
                shipperBridge.Init();
            });
            stxtShipper.OnOk += new EventHandler(stxtShipper_OnOk);

            //Consignee
            Utility.SetEnterToExecuteOnec(stxtConsignee, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                consigneeBridge = new CustomerFinderBridge(
                stxtConsignee,
                _countryList,
                DataFindClientService,
                CustomerService,
                _CurrentData.ConsigneeDescription,
                ICPCommUIHelper,
                LocalData.IsEnglish);
                consigneeBridge.Init();
            });

            stxtConsignee.OnOk += new EventHandler(stxtConsignee_OnOk);

            #endregion

            #region Port
          
            #region POL

            DataFindClientService.Register(stxtDeparture, CommonFinderConstants.AirLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid portID = new Guid(resultData[0].ToString());
                      if (_CurrentData.POLID != portID)
                      {
                          stxtDeparture.Tag = _CurrentData.POLID = portID;
                          stxtDeparture.Text = _CurrentData.DepartureName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                      }
                  },
                  delegate
                  {
                      stxtDeparture.Tag = _CurrentData.POLID = Guid.Empty;
                      stxtDeparture.Text = _CurrentData.DepartureName = string.Empty;
                  },
                  ClientConstants.MainWorkspace);
            #endregion
            #region POD
            DataFindClientService.Register(stxtDetination, CommonFinderConstants.AirLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid portID = new Guid(resultData[0].ToString());
                      if (_CurrentData.PODID != portID)
                      {
                          stxtDetination.Tag = _CurrentData.PODID = portID;
                          stxtDetination.Text = _CurrentData.DetinationName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                      }
                  },
                  delegate
                  {
                      stxtDetination.Tag = _CurrentData.PODID = Guid.Empty;
                      stxtDetination.Text = _CurrentData.DetinationName = string.Empty;
                  },
                  ClientConstants.MainWorkspace);
            #endregion
            #region PlaceOfDelivery
            DataFindClientService.Register(stxtPlaceOfDelivery, CommonFinderConstants.AirLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      stxtPlaceOfDelivery.Tag = _CurrentData.PlaceOfDeliveryID = new Guid(resultData[0].ToString());
                      stxtPlaceOfDelivery.Text = _CurrentData.PlaceOfDeliveryName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                  },
                  delegate
                  {
                      stxtPlaceOfDelivery.Tag = _CurrentData.PlaceOfDeliveryID = Guid.Empty;
                      stxtPlaceOfDelivery.Text = _CurrentData.PlaceOfDeliveryName = string.Empty;
                  },
                  ClientConstants.MainWorkspace);
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
            if (_CurrentData != null && stxtBookingCustomer.CustomerDescription != null)
            {
                _CurrentData.BookingCustomerDescription = stxtBookingCustomer.CustomerDescription;
            }
        }

        void stxtConsignee_OnOk(object sender, EventArgs e)
        {
            if (_CurrentData != null && stxtConsignee.CustomerDescription != null)
            {
                _CurrentData.ConsigneeDescription = stxtConsignee.CustomerDescription;
            }
        }

        void stxtShipper_OnOk(object sender, EventArgs e)
        {
            if (_CurrentData != null && stxtShipper.CustomerDescription != null)
            {
                _CurrentData.ShipperDescription = stxtShipper.CustomerDescription;
            }
        }

        void pfbPOD_Cleared(object sender, EventArgs e)
        {

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
                podId = (Guid)stxtDeparture.Tag;
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
            conditions.AddWithValue("PODID", stxtDeparture.Tag, false);
            conditions.AddWithValue("PODName", stxtDeparture.Text, false);
            return conditions;
        }

        void ResetDescription()
        {
            if (shipperBridge != null)
            {
                shipperBridge.SetCustomerDescription(_CurrentData.ShipperDescription);
            }

            if (consigneeBridge != null)
            {
                consigneeBridge.SetCustomerDescription(_CurrentData.ConsigneeDescription);
            }

            if (bookingCustomerPartyBridge != null)
            {
                bookingCustomerPartyBridge.SetCustomerDescription(_CurrentData.BookingCustomerDescription);
            }
        }

        #endregion

        #region 延迟加载的数据源

        List<DataDictionaryList> _weightUnits;

        void InitalComboxes()
        {
            ICPCommUIHelper.SetCmbDataDictionary(cmbQuantityUnit, DataDictionaryType.QuantityUnit);

            //重量
            _weightUnits = ICPCommUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit);

            List<DataDictionaryList> volUnitss = ICPCommUIHelper.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit);
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
                ICPCommUIHelper.BindCompanyByUser(cmbCompany, true);

                if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.CompanyID) && LocalData.UserInfo.UserOrganizationList.Count > 0)
                {
                    _CurrentData.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                }

                cmbCompany.SelectedIndexChanged += delegate
                {
                    CompanyChanged();
                };
            });

            #region Agent
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.AgentID) == false)
            {
                List<CustomerList> agentCustomers = new List<CustomerList>();
                CustomerList agentCustomer = new CustomerList();
                agentCustomer.CName = agentCustomer.EName = _CurrentData.AgentName;
                agentCustomer.ID = _CurrentData.AgentID.Value;
                agentCustomers.Insert(0, agentCustomer);
                SetAgentSource(agentCustomers);
            }
            Utility.SetEnterToExecuteOnec(stxtAgent, delegate
            {
                SetAgentSourceByCompanyID(_CurrentData.CompanyID);
                stxtAgent.EditValueChanged += delegate
                {
                    if (stxtAgent.EditValue != null && stxtAgent.EditValue.ToString().Length > 0)
                    {
                        Guid id = new Guid(stxtAgent.EditValue.ToString());

                        ICPCommUIHelper.SetCustomerDesByID(id, _CurrentData.AgentDescription);
                        stxtAgent.CustomerDescription = _CurrentData.AgentDescription;
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
                if (_IsInitTradeTerm)
                    return;
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
                                                    DataDictionaryType.PaymentTerm);
            });
            //航线
            Utility.SetEnterToExecuteOnec(cmbShippingLine, delegate
            {
                List<ShippingLineList> shippingLines = ICPCommUIHelper.SetCmbShippingLine(cmbShippingLine);
            });

            //船公司
            Utility.SetEnterToExecuteOnec(mcmbAirCompany, delegate
            {
                ICPCommUIHelper.BindCustomerList(mcmbAirCompany, CustomerType.Airline);
            });

            //运输条款
            Utility.SetEnterToExecuteOnec(cmbTransportClause, delegate
            {
                List<TransportClauseList> transportClauseList = ICPCommUIHelper.SetCmbTransportClause(cmbTransportClause);
            });
            Utility.SetEnterToExecuteOnec(mcmbSales, delegate
            {
                ICPCommUIHelper.SetMcmbUsersByCommand(mcmbSales, CommandConstants.FCM_AE_ORDERLIST, true, true);
            });

            //委托方式
            Utility.SetEnterToExecuteOnec(cmbBookingMode, delegate
            {
                if (_IsInitBookingMode)
                    return;
                ICPCommUIHelper.SetComboxByEnum<FCMBookingMode>(cmbBookingMode, false);
            });

            //货物描述
            Utility.SetEnterToExecuteOnec(cmbCargoType, delegate
            {
                ICPCommUIHelper.SetComboxByEnum<CargoType>(cmbCargoType, true, true);
            });

            Utility.SetEnterToExecuteOnec(cmbFlightNo, delegate
            {
                List<FlightList> flightList = TransportFoundationService.GetFlightList(null, string.Empty, true, 0);
                if (flightList != null && flightList.Count > 0)
                {
                    Dictionary<string, string> col = new Dictionary<string, string>();
                    col.Add("No", "航班号");
                    col.Add("AirlineName", "航空公司");
                    cmbFlightNo.InitSource<FlightList>(flightList, col, "No", "ID");
                }
            });

            mcmbFiler.Enter += new EventHandler(mcmFiler_Click);
            mcmbBookinger.Enter += new EventHandler(mcmbBookinger_Click);
        }

        void stxtAgent_OnOk(object sender, EventArgs e)
        {
            if (_CurrentData != null && stxtAgent.CustomerDescription != null)
            {
                _CurrentData.AgentDescription = stxtAgent.CustomerDescription;
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

            ICPCommUIHelper.SetComboxUsersByRoles(mcmbBookinger, depID, new string[] { "订舱", "客服" }, false);
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

            ICPCommUIHelper.SetComboxUsersByRoles(mcmbFiler, depID, new string[] { "文件", "客服" }, false);


        }

        #endregion

        #region 注册各种联动的事件

        /// <summary>
        /// 注册各种联动的事件
        /// </summary>
        void RegisterRelativeEvents()
        {
            panelScroll.Click += delegate
            {
                panelScroll.Focus();
            };
            //订舱客户,如果贸易条款为CIF，那么就为客户，否则为空白
            cmbTradeTerm.SelectedIndexChanged += new EventHandler(cmbTradeTerm_SelectedIndexChanged);

            cmbTransportClause.SelectedIndexChanged += delegate
            {
                if (_shown)
                {
                    _CurrentData.TransportClauseName = cmbTransportClause.Text;
                }
            };

            if (_CurrentData != null)
            {
                if (_CurrentData.ID == Guid.Empty)
                {

                    stxtCustomer.ButtonClick += new ButtonPressedEventHandler(stxtCustomer_ButtonClick);
                }
            }
            cmbFlightNo.SelectedRow += new EventHandler(cmbFlightNo_SelectedRow);
            cmbCargoType.Click += new EventHandler(cmbCargoType_Enter);
            cmbCargoType.SelectedIndexChanged += new EventHandler(cmbCargoType_EditValueChanged);
            mcmbSales.SelectedRow += new EventHandler(mcmbSales_SelectedRow);

            trsSalesDep.Enter += new EventHandler(trsSalesDep_Enter);
            trsSalesDep.Selected += new EventHandler(trsSalesDep_Selected);
            stxtBookingCustomer.TextChanged += new EventHandler(stxtBookingCustomer_TextChanged);
        }


        void trsSalesDep_Selected(object sender, EventArgs e)
        {
        }


        /// <summary>
        /// 注册界面控件之间联动的事件并立即执行一次
        /// </summary>
        void RegisterRelativeEventsAndRunOnce()
        {
            chkIsOnlyMBL.CheckedChanged += new EventHandler(chkIsOnlyMBL_CheckedChanged);
            chkHasContract.CheckedChanged += new EventHandler(chkHasContract_CheckedChanged);
            RunAtOnce();
        }

        void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!chkIsTruck.Enabled)
            {
                chkIsTruck.Checked = _CurrentData.IsTruck = false;
            }
        }

        void stxtBookingCustomer_TextChanged(object sender, EventArgs e)
        {
            if (_shown)
            {
                _CurrentData.BookingCustomerName = stxtBookingCustomer.Text;
                SetShipperByBookingCustomerAndTradeTerm();
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
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.SalesID) == false)
            {
                userOrganizationTreeLists = UserService.GetUserCompanyList(_CurrentData.SalesID.Value, null);
            }

            List<OrganizationList> saleOrgrnazitionTreeList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
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
            if (_shown)
            {
                SetAgetnEnabledByPlaceOfDeliveryAndCompany();
            }
        }

        void cmbTradeTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_shown)
            {
                _CurrentData.TradeTermName = cmbTradeTerm.Text;
                SetBookingCustomerByCustomerAndTradeTerm();
                SetConsigneeByCustomerAndTradeTerm();
                SetShipperByBookingCustomerAndTradeTerm();
            }
        }

        #region 主要是设置控件的颜色、可使用性等属性

        /// <summary>
        /// 总调用处，会把其它方法都执行一遍
        /// </summary>
        void RunAtOnce()
        {
            cmbType_SelectedIndexChanged(null, null);
            //this.cmbOrderNo_TextChanged(this, null);
            chkHasContract_CheckedChanged(null, null);

            #region 根据数据 设置 控件可操作
            if (_CurrentData != null)
            {
                SetHBLEnabledByIsOnlyMBL(_CurrentData.IsOnlyMBL);
                SetContractBoxByHasContract(_CurrentData.IsContract);
            }
            #endregion

            if (_CurrentData != null)
            {
                RefreshBarEnabled();
            }
        }

        #region 合约选择规则

        #endregion

        #region IsContract合约

        private void chkHasContract_CheckedChanged(object sender, EventArgs e)
        {
            SetContractBoxByHasContract(chkHasContract.Checked);
        }

        private void SetContractBoxByHasContract(bool hasContract)
        {
            txtContractNo.Enabled = hasContract;
            txtContractNo.BackColor = hasContract ? SystemColors.Info : txtNo.BackColor;
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
                _CurrentData.HBLPaymentTermID = null;
                _CurrentData.HBLReleaseType = FCMReleaseType.Unknown;
                _CurrentData.HBLRequirements = string.Empty;
            }
        }

        #endregion


        #region 刷新工具栏按钮的可使用性

        void RefreshBarEnabled()
        {

            barAuditAndSave.Enabled = _CurrentData.State == AEOrderState.NewOrder;

            if (_CurrentData.ID == Guid.Empty)
            {
                barReject.Enabled = barE_Booking.Enabled = false;
                barTruck.Enabled = false;

                barApplyAgent.Enabled = false;
                barRefresh.Enabled = false;
            }
            else
            {
                barReject.Enabled = _CurrentData.State == AEOrderState.NewOrder;

                barRefresh.Enabled = true;


            }


        }

        #endregion

        #endregion

        #region 控件联动

        #region 数据变动填充控件默认值 客户变了就刷新揽货方式等逻辑

        #region SetSalesDepartment

        /// <summary>
        /// 改变失去焦点后刷新揽货部门，如果有多个就清空，否则填充
        /// </summary>
        private void SetSalesDepartment()
        {
            List<UserOrganizationTreeList> userOrganizationTreeLists = new List<UserOrganizationTreeList>();
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.SalesID) == false)
            {
                userOrganizationTreeLists = UserService.GetUserOrganizationTreeList(_CurrentData.SalesID.Value);

                UserOrganizationTreeList orginazation = userOrganizationTreeLists.Find(o => o.IsDefault);
                if (orginazation != null)
                {
                    trsSalesDep.ShowSelectedValue(orginazation.ID, LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName);
                    _CurrentData.SalesDepartmentID = orginazation.ID;
                    _CurrentData.SalesDepartmentName = LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName;
                }
                else
                {
                    trsSalesDep.ShowSelectedValue(Guid.Empty, string.Empty);
                    _CurrentData.SalesDepartmentID = Guid.Empty;
                    _CurrentData.SalesDepartmentName = string.Empty;
                }
            }
        }

        #endregion

        #region Other

        /// <summary>
        /// 根据公司和客户设置揽货方式
        /// </summary>
        private void SetSalesTypeByCustomerAndCompany()
        {
            if (_CurrentData.CompanyID != Guid.Empty && _CurrentData.CustomerID != Guid.Empty)
            {
                DataDictionaryInfo salesType = AirExportService.GetSalesType(_CurrentData.CustomerID, _CurrentData.CompanyID);
                if (salesType != null)
                {
                    _CurrentData.SalesTypeID = salesType.ID;
                    _CurrentData.SalesTypeName = LocalData.IsEnglish ? salesType.EName : salesType.CName;

                    cmbSalesType.ShowSelectedValue(_CurrentData.SalesTypeID, _CurrentData.SalesTypeName);
                }
            }
        }

        /// <summary>
        /// 根据操作口岸ID设置操作和文件栏的数据源
        /// </summary>
        private void SetOperatorByCompany()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.CompanyID))
            {
            }
            else
            {
                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                List<UserList> operators = UserService.GetUnderlingUserList(new Guid[] { _CurrentData.CompanyID }, new string[] { "订舱" }, null, true);
                List<UserList> filers = UserService.GetUnderlingUserList(new Guid[] { _CurrentData.CompanyID }, new string[] { "文件" }, null, true);
                List<UserList> overSeasFilers = UserService.GetUnderlingUserList(new Guid[] { _CurrentData.CompanyID }, new string[] { "海外部客服" }, null, true);
            }
        }

        #endregion

        #region 设置默认海外部客服

        /// <summary>
        /// 当前客户最近业务所对应的海外部客服or 当前客户为新客户and当前揽货人最近业务所对应的海外部客服
        /// </summary>
        void SetDefaultOverseasFiler()
        {
            List<UserInfo> users = AirExportService.GetOverseasFilersList(_CurrentData.CustomerID, _CurrentData.SalesID,
                DateTime.Now.AddDays(-30), DateTime.Now, 1);

            if (users.Count > 0)
            {
            }
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

            SetSalesDepartment();
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
            SetAgentSourceByCompanyID(_CurrentData.CompanyID);
            SetAgetnEnabledByPlaceOfDeliveryAndCompany();

            orderFeeEditPart1.SetCompanyID(_CurrentData.CompanyID);
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

            SetAgentSourceByCompanyID(_CurrentData.CompanyID);
            SetAgetnByCustomerAndCompany(customerType);

            SetDefaultOverseasFiler();
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
                || ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.CompanyID)
                || ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.CustomerID))
            {
                return;
            }

            if (customerType.Value == CustomerType.Forwarding
                && !AirExportService.IsCustomerAndCompanySameCountry(_CurrentData.CustomerID, _CurrentData.CompanyID))
            {
                stxtAgent.Text = _CurrentData.AgentName = _CurrentData.CustomerName;
                stxtAgent.EditValue = _CurrentData.AgentID = _CurrentData.CustomerID;
                ICPCommUIHelper.SetCustomerDesByID(_CurrentData.AgentID, _CurrentData.AgentDescription);
            }
        }

        /// <summary>
        /// 如果交货地所在的国家不存在于公司配置中客户对应的国家，那么就为只读，否则可以输入
        /// </summary>
        private void SetAgetnEnabledByPlaceOfDeliveryAndCompany()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.PlaceOfDeliveryID))
            {
                return;
            }

            // TODO:“指定货”目前在数据库的字典表里面还没有对应的英文
            if (
                    (
                        string.IsNullOrEmpty(_CurrentData.SalesTypeName) == false &&
                        (_CurrentData.SalesTypeName.Contains("指定货") || _CurrentData.SalesTypeName.ToUpper().Contains("AGENT"))
                    )
                || AirExportService.IsPortCountryExistCompanyConfig(_CurrentData.PlaceOfDeliveryID, _CurrentData.CompanyID))
            {
                stxtAgent.Enabled = true;
            }
            else
            {
                stxtAgent.Enabled = false;
                stxtAgent.Text = _CurrentData.AgentName = string.Empty;
                stxtAgent.EditValue = _CurrentData.AgentID = Guid.Empty;
                _CurrentData.AgentDescription = new CustomerDescription();
            }
        }


        /// <summary>
        /// 设置Agent数据源
        /// </summary>
        private void SetAgentSourceByCompanyID(Guid companyID)
        {
            stxtAgent.DataSource = null;
            if (ArgumentHelper.GuidIsNullOrEmpty(companyID))
            {
                stxtAgent.Enabled = false;
                return;
            }

            List<CustomerList> agentCustomers = ConfigureService.GetCompanyAgentList(_CurrentData.CompanyID, true);
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
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.AgentID))
            {
                stxtAgent.EditValue = _CurrentData.AgentID = agentCustomers[0].ID;
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
            if (ArgumentHelper.GuidIsNullOrEmpty(id))
            {
                stxtAgent.CustomerDescription = _CurrentData.AgentDescription = new CustomerDescription();
            }
            else
            {
                ICPCommUIHelper.SetCustomerDesByID(id, _CurrentData.AgentDescription);
                stxtAgent.CustomerDescription = _CurrentData.AgentDescription;
            }
        }

        #endregion


        /// <summary>
        /// 设置发货人 如果贸易条款为CIF，那么就为订舱客户
        /// </summary>
        private void SetShipperByBookingCustomerAndTradeTerm()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.ShipperID) == false
                || ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.TradeTermID))
            {
                return;
            }

            if (_CurrentData.TradeTermName.Contains("CIF"))
            {
                stxtShipper.Tag = _CurrentData.ShipperID = _CurrentData.BookingCustomerID;
                stxtShipper.Text = _CurrentData.ShipperName = _CurrentData.BookingCustomerName;
                ICPCommUIHelper.SetCustomerDesByID(_CurrentData.ShipperID, _CurrentData.ShipperDescription);
            }
        }

        /// <summary>
        /// 收货人:如果贸易条款为FOB或EXWORK，那么就为客户
        /// </summary>
        private void SetConsigneeByCustomerAndTradeTerm()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.ConsigneeID) == false) return;

            if (_CurrentData.TradeTermName == "FOB" || _CurrentData.TradeTermName == "EXWORK")
            {
                stxtConsignee.Tag = _CurrentData.ConsigneeID = _CurrentData.CustomerID;
                stxtConsignee.Text = _CurrentData.ConsigneeName = _CurrentData.CustomerName;
                ICPCommUIHelper.SetCustomerDesByID(_CurrentData.ConsigneeID, _CurrentData.ConsigneeDescription);
            }

            ResetDescription();
        }

        /// <summary>
        /// 设置订舱客户和发货（订舱客户:如果贸易条款为CIF，那么就为客户
        /// </summary>
        private void SetBookingCustomerByCustomerAndTradeTerm()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.BookingCustomerID) == false)
            {
                return;
            }

            if (!string.IsNullOrEmpty(_CurrentData.TradeTermName)
                && _CurrentData.TradeTermName.Contains("CIF"))
            {
                stxtBookingCustomer.Tag = _CurrentData.BookingCustomerID = _CurrentData.CustomerID;
                stxtBookingCustomer.Text = _CurrentData.BookingCustomerName = _CurrentData.CustomerName;
                ICPCommUIHelper.SetCustomerDesByID(_CurrentData.CustomerID, _CurrentData.BookingCustomerDescription);
            }
        }

        /// <summary>
        /// 最近业务
        /// </summary>
        private void SetRecentlyOrderListByCustomerAndCompany()
        {
            if (_CurrentData.ID != Guid.Empty || _CurrentData.CompanyID == Guid.Empty || _CurrentData.CustomerID == Guid.Empty)
            {
                bsRecentTenOrders.Clear();
            }
            else
            {
                bsRecentTenOrders.Clear();
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
        private void stxtCustomer_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind != ButtonPredefines.Combo) return;

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

            DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "是否覆盖当前页面数据?" : "是否覆盖当前页面数据?"
                              , LocalData.IsEnglish ? "Tip" : "提示"
                              , MessageBoxButtons.YesNo
                              , MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                AirBookingInfo order = AirExportService.GetAirBookingInfo(CurrentOrderList.ID);
                if (order == null) return;

                order.ID = Guid.Empty;

                order.No = string.Empty;

                order.State = AEOrderState.NewOrder;

                order.SalesID = LocalData.UserInfo.LoginID;
                order.SalesName = LocalData.UserInfo.LoginName;
                order.CreateDate = DateTime.Now;
                _CurrentData = order;

                ShowOrder();
                RunAtOnce();
                ResetDescription();

                EndEdit();

                Invalidate();
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
                _CurrentData.CargoDescription = null;
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
                navBarGroupControlContainer2.Controls.Remove(cargoDescriptionPart1);
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
            _CurrentData = AirExportService.GetAirBookingInfo(orderId);
        }

        void ShowOrder()
        {
            InitData();

            bsBookingInfo.DataSource = _CurrentData;
            bsBookingInfo.ResetBindings(false);
            bsBookingInfo.CancelEdit();

            InitControls();

            List<AirBookingFeeList> feelist = null;

            if (_CurrentData.ID == Guid.Empty)
            {
                feelist = new List<AirBookingFeeList>();
                if (CacheDelegate.Count > 0)
                {
                    FillCSPBookingToCurrent();
                }
            }
            else
            {
                feelist = AirExportService.GetAirOrderFeeList(_CurrentData.ID, _CurrentData.CompanyID);
                CacheDelegate = FCMCommonService.GetBookingDelegateList(new SearchParameterBookingDelegate() { OperationID = _CurrentData.ID });
            }
            partDelegate.DataChanged += (sender, e) =>
            {
                CacheDelegate = sender as List<BookingDelegate>;
                FillCSPBookingToCurrent();
            };
            partDelegate.SetSource(CacheDelegate);
            orderFeeEditPart1.SetSource(feelist);
        }

        void FillCSPBookingToCurrent()
        {
            BookingDelegate cspPBooking = CacheDelegate.SingleOrDefault(fitem => fitem.IsDefault && fitem.CancelRemark.IsNullOrEmpty());
            if (cspPBooking == null)
                cspPBooking = CacheDelegate.First();
            if (!cspPBooking.CancelRemark.IsNullOrEmpty())
            {
                return;
            }

            //初始化
            ICPCommUIHelper.SetComboxByEnum<FCMBookingMode>(cmbBookingMode, false);
            _IsInitBookingMode = true;
            cmbBookingMode.SelectedIndex = 3;

            dteBookingDate.EditValue = _CurrentData.BookingDate = cspPBooking.BookingDate;
            ICPCommUIHelper.SetCmbDataDictionary(cmbTradeTerm, DataDictionaryType.TradeTerm);
            _IsInitTradeTerm = true;
            cmbTradeTerm.EditValue = cspPBooking.IncoTermID;
            //Customer
     
            stxtCustomer.Tag = _CurrentData.CustomerID = cspPBooking.CustomerID;
            stxtCustomer.EditValue = _CurrentData.CustomerName = cspPBooking.CustomerName;
            //BookingCustomer
            stxtBookingCustomer.Tag = _CurrentData.BookingCustomerID = cspPBooking.CustomerID;
            stxtBookingCustomer.EditValue = _CurrentData.BookingCustomerName = cspPBooking.CustomerName;
            stxtBookingCustomer.CustomerDescription = _CurrentData.BookingCustomerDescription = cspPBooking.CustomerDescription;
            //Shipper
            stxtShipper.Tag = _CurrentData.ShipperID = cspPBooking.ShipperID;
            stxtShipper.Text = _CurrentData.ShipperName = cspPBooking.ShipperName;
            stxtShipper.CustomerDescription = _CurrentData.ShipperDescription = cspPBooking.ShipperDescription;
            //Consignee
            stxtConsignee.Tag = _CurrentData.ConsigneeID = cspPBooking.ConsigneeID;
            stxtConsignee.Text = _CurrentData.ConsigneeName = cspPBooking.ConsigneeName;
            stxtConsignee.CustomerDescription = _CurrentData.ConsigneeDescription = cspPBooking.ConsigneeDescription;

            _CurrentData.TransportClauseID = cspPBooking.TransportClauseID;
            _CurrentData.TransportClauseName = cspPBooking.TransportClauseName;
            cmbTransportClause.ShowSelectedValue(_CurrentData.TransportClauseID, _CurrentData.TransportClauseName);
            mcmbSales.EditValue = _CurrentData.SalesID = cspPBooking.SalesID;
            _CurrentData.SalesName = cspPBooking.SalesName;
            mcmbSales.ShowSelectedValue(_CurrentData.SalesID, _CurrentData.SalesName);
            SetDefaultOverseasFiler();
            SetSalesDepartment();
            _CurrentData.IsContract = false;
            _CurrentData.IsTruck = cspPBooking.IsTruck;
            _CurrentData.IsCustoms = cspPBooking.IsDeclaration;
            if (!cspPBooking.POLID.IsNullOrEmpty())
            {
                _CurrentData.POLID = cspPBooking.POLID.Value;
                _CurrentData.DepartureName = cspPBooking.POLName;
            }
            //_CurrentData.PlaceOfReceiptAddress = cspPBooking.POLAddress;
            if (!cspPBooking.PODID.IsNullOrEmpty())
            {
                _CurrentData.PODID = _CurrentData.PlaceOfDeliveryID = cspPBooking.PODID.Value;
                _CurrentData.DetinationName = _CurrentData.PlaceOfDeliveryName = cspPBooking.PODName;
            }
            //_CurrentData.PlaceOfDeliveryAddress = cspPBooking.PODAddress;
            _CurrentData.ETD = cspPBooking.ETDForPOL;
            if (cspPBooking.ETAForPOD != null) _CurrentData.ETA = cspPBooking.ETAForPOD.Value;
            cmbQuantityUnit.EditValue = cspPBooking.QuantityUnitID;
            cmbWeightUnit.EditValue = cspPBooking.WeightUnitID;
            cmbMeasurementUnit.EditValue = cspPBooking.MeasurementUnitID;
            //毛件体汇总
            _CurrentData.Quantity = CacheDelegate.Sum(fitem => fitem.Quantity);
            _CurrentData.Weight = CacheDelegate.Sum(fitem => fitem.Weight);
            _CurrentData.Measurement = CacheDelegate.Sum(fitem => fitem.Measurement);
        }
        #region 货物描述
        private void SetCargo(Control sender, CargoType cargoType)
        {
            if (!cmbCargoType.Focused)
            {
                return;
            }
            if (cargoType == CargoType.Awkward)
            {
                if (_CurrentData.CargoDescription == null
                    || _CurrentData.CargoDescription.Cargo == null || _CurrentData.CargoDescription.Cargo is AwkwardCargo == false)
                {
                    AwkwardCargo cargo = new AwkwardCargo();
                    cargo.NetWeightUnit = cmbWeightUnit.Text;
                    cargo.GrossWeightUnit = cmbWeightUnit.Text;
                    cargo.Quantity = (int)numQuantity.Value;
                    _CurrentData.CargoDescription = new CargoDescription(cargo);
                    _CurrentData.IsDirty = true;
                }

                if (cargoDescriptionPart1 is AwkwardDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new AwkwardDescriptionPart();
                    cargoDescriptionPart1.ShowWeightUnit(_weightUnits);
                    navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Dangerous)
            {
                if (_CurrentData.CargoDescription == null
                    || _CurrentData.CargoDescription.Cargo == null || _CurrentData.CargoDescription.Cargo is DangerousCargo == false)
                {
                    _CurrentData.CargoDescription = new CargoDescription(new DangerousCargo());
                    _CurrentData.IsDirty = true;
                }

                if (cargoDescriptionPart1 is DangerousDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new DangerousDescriptionPart();
                    navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Dry)
            {
                if (_CurrentData.CargoDescription == null
                    || _CurrentData.CargoDescription.Cargo == null || _CurrentData.CargoDescription.Cargo is DryCargo == false)
                {
                    _CurrentData.CargoDescription = new CargoDescription(new DryCargo());
                    _CurrentData.IsDirty = true;
                }

                if (cargoDescriptionPart1 is DryDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new DryDescriptionPart();
                    navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Reefer)
            {
                if (_CurrentData.CargoDescription == null
                    || _CurrentData.CargoDescription.Cargo == null || _CurrentData.CargoDescription.Cargo is ReeferCargo == false)
                {
                    _CurrentData.CargoDescription = new CargoDescription(new ReeferCargo());
                    _CurrentData.IsDirty = true;
                }

                if (cargoDescriptionPart1 is ReeferDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ReeferDescriptionPart();
                    navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            cargoDescriptionPart1.SetParentControl(sender, _CurrentData.CargoDescription, txtCargoDescription);
            AirBookingInfo currentData = bsBookingInfo.DataSource as AirBookingInfo;
            currentData.IsDirty = true;

        }
        #endregion

        public void BindingData(object data)
        {
            SuspendLayout();
            orderFeeEditPart1.SetService(Workitem);
            AirBookingList listInfo = data as AirBookingList;

            if (listInfo == null)
            {
                //新建
                _CurrentData = new AirBookingInfo();
                ReadyForNew();
            }
            else
            {
                GetData(listInfo.ID);

                if (listInfo.EditMode == EditMode.Edit)
                {
                }
                else if (listInfo.EditMode == EditMode.Copy)
                {
                    PrepareForCopyExistOrder();
                }
            }

            _CurrentData.CancelEdit();

            InitalComboxes();

            ShowOrder();

            SearchRegister();
            SetLazyLoaders();
            RefreshBarEnabled();
            ResumeLayout(true);
        }
        public override object DataSource
        {
            get { return bsBookingInfo.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return Save(_CurrentData, false);
        }

        public override void EndEdit()
        {
            Validate();
            bsBookingInfo.EndEdit();
        }

        public override event SavedHandler Saved;

        #endregion

        #region 工具栏事件

        #region 验证界面输入

        private bool ValidateData()
        {
            EndEdit();
            dxErrorProvider1.ClearErrors();

            List<bool> isScrrs = new List<bool> { true, true };

            isScrrs[0] = _CurrentData.Validate
               (
                   delegate(ValidateEventArgs e)
                   {
                       if (chkHasContract.Checked == true && string.IsNullOrEmpty(_CurrentData.ContractNo))
                       {
                           e.SetErrorInfo("ContractNo", LocalData.IsEnglish ? "Checked contract, contract number can not be empty!" : "已勾选合约，合约号不能为空！");
                       }

                       if (_CurrentData.POLID != Guid.Empty && _CurrentData.POLID == _CurrentData.PODID)
                           e.SetErrorInfo("PODID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");

                       if (_CurrentData.ETA != null && _CurrentData.ETD != null
                           && _CurrentData.ETD > _CurrentData.ETA)
                           e.SetErrorInfo("ETA", LocalData.IsEnglish ? "ETD can't bigger ETA." : "ETD不能大于ETA.");

                       if (_CurrentData.ExpectedShipDate != null && _CurrentData.ExpectedArriveDate != null
                           && _CurrentData.ExpectedShipDate.Value.Date >= _CurrentData.ExpectedArriveDate.Value.Date)
                           e.SetErrorInfo("ExpectedShipDate", LocalData.IsEnglish ? "ExpectedShipDate can't bigger ExpectedArriveDate." : "期望出运日不能大于期望到达日.");

                       if (_CurrentData.SODate.HasValue && _CurrentData.ClosingDate.HasValue)
                       {
                           if (_CurrentData.SODate.Value >= _CurrentData.ClosingDate.Value)
                           {
                               e.SetErrorInfo("SODate", LocalData.IsEnglish ? "Confirmed data must ealier than Closing date." : "确认日必须小于截关日.");
                           }
                       }
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

            if (!orderFeeEditPart1.ValidateData())
            {
                isScrr = false;
                xtraTabControl1.SelectedTabPageIndex = 0;
            }

            return isScrr;
        }

        #endregion

        #region 保存

        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Save(_CurrentData, false);
            }
        }

        private bool Save(AirBookingInfo currentData, bool isSavingAs)
        {
            if (ValidateData() == false)
            {
                return false;
            }
            if (!currentData.IsDirty && !currentData.IsNew && !orderFeeEditPart1.IsChanged)
            {
                return true;
            }

            try
            {
                BookingSaveRequest originalBooking = null;
                if (ArgumentHelper.GuidIsNullOrEmpty(currentData.ID) || ArgumentHelper.GuidIsNullOrEmpty(currentData.FilightId))
                {
                    originalBooking = BuildBookingSaveRequest(currentData);
                }
                else if (_CurrentData.IsDirty)
                {
                    if (_CurrentData.BookingDate != null)
                        _CurrentData.State = AEOrderState.BookingConfirmed;

                    originalBooking = BuildBookingSaveRequest(_CurrentData);
                }

                if (isChangeSales)
                {
                    if (_CurrentData.ID != Guid.Empty)
                    {
                        Guid[] checkIds = { _CurrentData.ID };
                        SystemService.SaveUntieLockInfo(UntieLockType.Sales, checkIds, LocalData.UserInfo.LoginID);
                    }
                }

                List<FeeSaveRequest> originalFees = orderFeeEditPart1.SaveFee(currentData.ID, Guid.Empty);
                List<SaveRequestBookingDelegate> originalDelegates = partDelegate.BuildSaveRequest(currentData.ID, OperationType.AirExport);
                Dictionary<Guid, SaveResponse> saved = AirExportService.SaveAirBookingWithTrans(originalBooking,
                    originalFees, originalDelegates);

                if (originalBooking != null)
                {
                    SaveResponse.Analyze(new List<SaveRequest> { originalBooking }, saved, true);
                    RefreshUI(originalBooking);
                }
                if (originalFees != null)
                {
                    SaveResponse.Analyze(originalFees.Cast<SaveRequest>().ToList(), saved, true);
                    orderFeeEditPart1.RefreshUI(originalFees);
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
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return false;
            }
        }

        private void AfterSave()
        {
            _CurrentData.CancelEdit();
            _CurrentData.BeginEdit();

            TriggerSavedEvent();

            gvOrders.DoubleClick -= new EventHandler(gvOrders_DoubleClick);
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

            SetTitle();
        }

        void SetTitle()
        {
            if (_CurrentData.ID == Guid.Empty)
            {
                Title = LocalData.IsEnglish ? "Add Booking" : "新增订舱";
            }
            else
            {
                string titleNo = string.Empty;

                if (_CurrentData.No.Length > 4)
                {
                    titleNo = _CurrentData.No.Substring(_CurrentData.No.Length - 4, 4);
                }
                else
                {
                    titleNo = _CurrentData.No;
                }
                Title = LocalData.IsEnglish ? "Edit Booking " + titleNo : "编辑订舱：" + titleNo;
            }
        }

        void TriggerSavedEvent()
        {
            if (Saved != null)
            {
                _CurrentData.SalesName = _CurrentData.SalesID.ToGuid() == Guid.Empty ?
                    string.Empty : mcmbSales.EditText;
                _CurrentData.FilerName = _CurrentData.FilerId.ToGuid() == Guid.Empty ?
                    string.Empty : mcmbFiler.Text;
                _CurrentData.BookingerName = _CurrentData.BookingerID.ToGuid() == Guid.Empty ?
                    string.Empty : mcmbBookinger.Text;

                #region  刷新任务中心
                if (_businessOperationParameter == null)
                {
                    _businessOperationParameter = new BusinessOperationParameter();
                }
                _businessOperationParameter.Context = GetContext(_CurrentData);
                Saved(new object[] { _CurrentData, _businessOperationParameter, _businessOperationParameter.Context });
                #endregion

                _CurrentData.IsDirty = false;
            }
        }


        private BusinessOperationContext GetContext(AirBookingList orderInfo)
        {
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationID = (Guid)orderInfo.ID;
            context.OperationNO = orderInfo.No;
            context.OperationType = OperationType.AirExport;
            context.FormId = (Guid)orderInfo.ID;
            context.FormType = FormType.ShippingOrder;
            context["UpdateDate"] = orderInfo.UpdateDate;
            return context;
        }

        /// <summary>
        /// 客户参考号暂时传空值
        /// </summary>
        /// <param name="currentData"></param>
        private BookingSaveRequest BuildBookingSaveRequest(AirBookingInfo currentData)
        {
            EndEdit();
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
                saveRequest.polID = currentData.POLID;
                saveRequest.podID = currentData.PODID;
                saveRequest.finalDestinationID = currentData.PlaceOfDeliveryID;
                saveRequest.agentID = currentData.AgentID;
                saveRequest.agentDescription = currentData.AgentDescription;
                saveRequest.agentOfCarrierID = currentData.AgentOfCarrierID;
                saveRequest.isContract = currentData.IsContract;
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
                saveRequest.IsSyncCSP = CacheDelegate.Count > 0;
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

        private void barSaveAs_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (SaveAs())
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save as a new booking order successfully. Ref. NO. is " + _CurrentData.No + "." : "已成功另存为一票新订单，业务号为" + _CurrentData.No + "。");
                    if (Saved != null)
                    {
                        Saved(new object[] { _CurrentData });
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

            if (XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "是否另存为一票新的订舱单?",
                            LocalData.IsEnglish ? "Tip" : "提示",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }

            AirBookingInfo orderInfo = Utility.Clone<AirBookingInfo>(_CurrentData);
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

            _CurrentData = orderInfo;

            if (Save(orderInfo, true))
            {
                RefreshData(orderInfo.ID);

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
        private void barAuditAndSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (!Save(_CurrentData, false))
                {
                    return;
                }

                if (_CurrentData.State != AEOrderState.NewOrder)
                {
                    return;
                }

                EditRemarkPart editRemarkPart = Workitem.Items.AddNew<EditRemarkPart>();
                editRemarkPart.LabRemark = LocalData.IsEnglish ? "Audit memo" : "审核意见";
                editRemarkPart.RemartRequired = true;
                editRemarkPart.Saved += delegate(object[] prams)
                {
                    try
                    {
                        SingleResult result = AirExportService.ChangeAirOrderStateWithTargetState(_CurrentData.ID, Guid.Empty, AEOrderState.Checked, prams[0].ToString(), LocalData.UserInfo.LoginID, _CurrentData.UpdateDate);

                        Logger.Log.Info(result);

                        _CurrentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                        _CurrentData.State = (AEOrderState)result.GetValue<byte>("State");

                        TriggerSavedEvent();

                        RefreshBarEnabled();

                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Audit successfully!" : "审核订单成功！");
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                    }
                };
                string title = LocalData.IsEnglish ? "Audit Order" : "审核订单";
                PartLoader.ShowDialog(editRemarkPart, title);
            }
        }

        #endregion

        #region 打印

        private void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!SaveData())
            {
                return;
            }
        }

        /// <summary>
        /// 打印业务联单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barPrintOrder_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!SaveData())
            {
                return;
            }
        }

        private void barPrintInWarehouse_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!SaveData())
            {
                return;
            }

            throw new NotImplementedException(LocalData.IsEnglish ? "To be defined on next version." : "本版本暂不提供入仓通知单打印功能。");
        }

        #endregion

        #region 打回

        private void barReject_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_CurrentData.State == AEOrderState.Rejected) return;

            EditRemarkPart editRemarkPart = Workitem.Items.AddNew<EditRemarkPart>();
            editRemarkPart.LabRemark = LocalData.IsEnglish ? "Reject reason" : "打回原因";
            editRemarkPart.RemartRequired = true;
            editRemarkPart.Saved += delegate(object[] prams)
            {
                try
                {
                    bool isDirty = _CurrentData.IsDirty;
                    SingleResult result = AirExportService.ChangeAirOrderStateWithTargetState(_CurrentData.ID, Guid.Empty, AEOrderState.Rejected, prams[0].ToString(), LocalData.UserInfo.LoginID, _CurrentData.UpdateDate);

                    _CurrentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    _CurrentData.State = (AEOrderState)result.GetValue<byte>("State");

                    TriggerSavedEvent();

                    RefreshBarEnabled();
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
            };
            string title = LocalData.IsEnglish ? "Reject Order" : "打回订单";
            PartLoader.ShowDialog(editRemarkPart, title);
        }

        #endregion

        #region 电子订舱

        private void barE_Booking_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        #endregion

        #region 刷新

        /// <summary>
        /// 数据刷新到初始或保存后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    RefreshData(_CurrentData.ID);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Refersh successfully." : "刷新成功.");
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Refersh failed." + ex.Message : "刷新失败." + ex.Message);
                }
            }
        }

        void RefreshData(Guid orderId)
        {
            GetData(_CurrentData.ID);
            ShowOrder();
            RunAtOnce();
            ResetDescription();
            SetTitle();
        }

        #endregion

        #endregion

        #region 清理资源和避免多余操作

        void BookingBaseEditPart_Disposed(object sender, EventArgs e)
        {
            _countryList = null;
            _CacheCurrentData = null;
            _CurrentData = null;
            _weightUnits = null;
            bsBookingInfo.DataSource = null;
            bsBookingInfo.Dispose();
            bsRecentTenOrders.DataSource = null;
            bsRecentTenOrders.Dispose();
            Saved = null;
            stxtAgent.EditValueChanged -= stxtAgent_EditValueChanged;
            stxtAgent.OnOk -= stxtAgent_OnOk;
            stxtBookingCustomer.OnOk -= stxtBookingCustomer_OnOk;
            stxtBookingCustomer.TextChanged -= stxtBookingCustomer_TextChanged;
            stxtConsignee.OnOk -= stxtConsignee_OnOk;
            stxtPlaceOfDelivery.TextChanged -= stxtPlaceOfDelivery_TextChanged;
            mcmbSales.SelectedRow -= mcmbSales_SelectedRow;
            mcmbAirCompany.SelectedRow -= mcmbAirCompany_SelectedRow;
            mcmbBookinger.Click -= mcmbBookinger_Click;
            mcmbFiler.Click -= mcmFiler_Click;


            cmbFlightNo.SelectedRow -= cmbFlightNo_SelectedRow;
            cmbCargoType.Click -= cmbCargoType_Enter;
            cmbCargoType.SelectedIndexChanged -= cmbCargoType_EditValueChanged;

            trsSalesDep.Enter -= trsSalesDep_Enter;
            trsSalesDep.Selected -= trsSalesDep_Selected;


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

        #region 关闭

        void BookingBaseEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (_CurrentData.IsDirty)
            {
                DialogResult dr = PartLoader.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!Save(_CurrentData, false))
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

        void BookingBaseEditPart_Load(object sender, EventArgs e)
        {
            //this.SetTitle();
            RegisterRelativeEvents();
            RegisterRelativeEventsAndRunOnce();

            Utility.SetCustomerTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtCustomer,
                stxtBookingCustomer,
                stxtAgent.lookUpEdit1,
                stxtAgentOfCarrier,
            });

            Utility.SetPortTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtDetination,
                stxtDeparture,
            });
            SmartPartClosing += new EventHandler<WorkspaceCancelEventArgs>(BookingBaseEditPart_SmartPartClosing);
            ActivateSmartPartClosingEvent(Workitem);
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
                return _CurrentData.IsDirty;
            }
        }

        private void barApplyAgent_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                FCMCommonClientService.OpenAgentRequestPart(_CurrentData.ID, OperationType.AirExport, null, null);

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        private void mcmbAirCompany_SelectedRow(object sender, EventArgs e)
        {
        }
        private void barTruck_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<AirTruckInfo> truckList = AirExportService.GetAirTruckServiceList(_CurrentData.ID);
            SingleResult recentData = AirExportService.GetTruckRecentData(_CurrentData.ID);

            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            stateValues.Add("Booking", _CurrentData);

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
            PartLoader.ShowEditPart<AirTruckEditPart>(Workitem,
                truckList,
                stateValues,
                BookingListPart.GetLineNo(_CurrentData),
                null,
                AEBookingCommandConstants.Command_Truck + _CurrentData.ID.ToString());

        }

        private void cmbFlightNo_SelectedRow(object sender, EventArgs e)
        {
            if (cmbFlightNo.EditValue != null && cmbFlightNo.EditValue.ToString().Length > 0)
            {
                _CacheCurrentData = bsBookingInfo.DataSource as AirBookingInfo;
                FlightInfo flightInfo = TransportFoundationService.GetFilghtInfo(new Guid(cmbFlightNo.EditValue.ToString()));

                if (mcmbAirCompany.EditValue != null && mcmbAirCompany.EditValue.ToString().Length > 0)
                {
                    if ((Guid)mcmbAirCompany.EditValue != flightInfo.AirlineID)
                    {
                        DialogResult dialogResult = XtraMessageBox.Show("是否替换原有航空公司?",
                                                            "提示",
                                                           MessageBoxButtons.YesNo,
                                                           MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            _CacheCurrentData.AirCompanyId = flightInfo.AirlineID;
                            _CacheCurrentData.AirCompanyName = flightInfo.AirlineName;
                            mcmbAirCompany.ShowSelectedValue(_CacheCurrentData.AirCompanyId, _CacheCurrentData.AirCompanyName);
                        }
                    }
                }
                else
                {
                    _CacheCurrentData.AirCompanyId = flightInfo.AirlineID;
                    _CacheCurrentData.AirCompanyName = flightInfo.AirlineName;
                    mcmbAirCompany.ShowSelectedValue(_CacheCurrentData.AirCompanyId, _CacheCurrentData.AirCompanyName);
                }

                //有了航班号后，航空公司和确认日期要求必填
                mcmbAirCompany.SpecifiedBackColor = SystemColors.Info;
                dteSODate.BackColor = SystemColors.Info;
                stxtAgentOfCarrier.BackColor = SystemColors.Info;
            }
            else
            {
                mcmbAirCompany.SpecifiedBackColor = Color.White;
                dteSODate.BackColor = Color.White;
                stxtAgentOfCarrier.BackColor = Color.White;
            }
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
                if (item.Key.ToUpper() == "BOOKINGINFOFORCSP".ToUpper())
                {
                    CacheDelegate = item.Value as List<BookingDelegate>;
                    break;
                }
            }
        }

        private void mcmbSales_EditValueChanged(object sender, EventArgs e)
        {
            List<User2OrganizationJobList> jobList = UserService.GetUser2OrganizationJobList(LocalData.UserInfo.LoginID);
            foreach (User2OrganizationJobList job in jobList)
            {
                if (job.OrganizationJobName.Contains("操作部经理") || job.OrganizationJobName.Contains("操作经理") || job.OrganizationJobName.Contains("操作部->经理"))
                {
                    isChangeSales = true;
                }
            }
        }
    }
}
