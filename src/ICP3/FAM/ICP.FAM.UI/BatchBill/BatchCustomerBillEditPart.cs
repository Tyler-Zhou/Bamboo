using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ICP.FAM.UI.BatchBill
{
    /// <summary>
    /// 批量编辑(应收)账单界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class BatchCustomerBillEditPart : BaseEditPart
    {
        #region Fields
        /// <summary>
        /// 需注册的搜索器
        /// </summary>
        private IDisposable customerFinder, chargingCodeFinder;
        /// <summary>
        /// 当前选中业务
        /// </summary>
        OperationCommonInfo _OperationCommonInfo = null;
        /// <summary>
        /// 当前选中业务配置
        /// </summary>
        ConfigureInfo _ConfigureInfo = null;
        /// <summary>
        /// 当前选中业务的汇率集合
        /// </summary>
        List<SolutionExchangeRateList> _RateList = null;
        /// <summary>
        /// 当前选中业务的币种集合
        /// </summary>
        List<SolutionCurrencyList> _CurrencyList = null;
        /// <summary>
        /// 当前业务的客户列表(收发通)包括帐单已选的新客户
        /// </summary>
        List<DataDictionaryList> _packTypes = null;
        /// <summary>
        /// 保存的账单列表
        /// </summary>
        List<BillInfo> savedBillInfos = null;
        #endregion

        #region Services

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 利率辅助类
        /// </summary>
        public RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }

        /// <summary>
        /// 搜索器客户端服务
        /// </summary>
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        /// <summary>
        /// 财务服务
        /// </summary>
        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        /// <summary>
        /// 基础数据服务
        /// </summary>
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        /// <summary>
        /// 公共客户管理服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        /// <summary>
        /// 配置管理服务
        /// </summary>
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        /// <summary>
        /// 财务客户端服务
        /// </summary>
        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }

        #endregion

        #region Property

        /// <summary>
        /// 当前帐单对象
        /// </summary>
        BillInfo _CurrentData
        {
            get
            {
                if (bsBillInfo.DataSource == null)
                    return null;
                return bsBillInfo.DataSource as BillInfo;
            }
        }
        /// <summary>
        /// 费用当前行
        /// </summary>
        ChargeList _CurrentCharge
        {
            get
            {
                if (bsChargeList.Current == null) return null;
                return bsChargeList.Current as ChargeList;
            }
        }
        /// <summary>
        /// 当前行的业务ID
        /// </summary>
        Guid _CurrentOperationID
        {
            get
            {
                if (_CurrentCharge != null
                    && _CurrentCharge.ChooseOperationInfo != null
                    &&
                    !ArgumentHelper.GuidIsNullOrEmpty(_CurrentCharge.ChooseOperationInfo.OperationID))
                    return _CurrentCharge.ChooseOperationInfo.OperationID;
                return Guid.Empty;
            }
        }
        /// <summary>
        ///当前行的业务操作类型
        /// </summary>
        OperationType _CurrentOperationType
        {
            get
            {
                if (_CurrentCharge != null && _CurrentCharge.ChooseOperationInfo != null)
                    return _CurrentCharge.ChooseOperationInfo.OperationType;
                return OperationType.Unknown;
            }
        }
        /// <summary>
        /// 当前数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsBillInfo.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }
        #endregion

        #region Delegate
        public override event SavedHandler Saved;
        #endregion 

        #region Init
        /// <summary>
        /// 批量编辑(应收)账单界面
        /// </summary>
        public BatchCustomerBillEditPart()
        {
            InitializeComponent();
            if (DesignMode) return;
            RegisterEvent();
            Disposed += delegate
            {
                UnRegisterEvent();
                if (customerFinder != null)
                {
                    customerFinder.Dispose();
                    customerFinder = null;
                }
                if (chargingCodeFinder != null)
                {
                    chargingCodeFinder.Dispose();
                    chargingCodeFinder = null;
                }
                gcChargeList.DataSource = null;
                bsBillInfo.DataSource = null;
                bsBillInfo.Dispose();
                bsChargeList.DataSource = null;
                bsChargeList.Dispose();
                Saved = null;
                _ConfigureInfo = null;
                _CurrencyList = null;
                _OperationCommonInfo = null;
                _packTypes = null;
                _RateList = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
        
        /// <summary>
        /// OnLoad
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessage();
                InitControls();
            }
        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
        }
        #endregion

        #region Controls Event

        #region GvEvent

        private void gvChargeList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (gvChargeList.FocusedColumn == colOperationNo)
                {
                    rbtnBusinessInfo_ButtonClick(null, null);
                }
                if (gvChargeList.FocusedColumn == colContainerNo)
                {
                    rbtnConrainerNo_ButtonClick(null, null);
                }
                if (gvChargeList.FocusedColumn == colChargingCode
                    || gvChargeList.FocusedColumn == colChargingDescription
                    || gvChargeList.FocusedColumn == colUnitPrice
                    || gvChargeList.FocusedColumn == colQuantity
                    || gvChargeList.FocusedColumn == colUnitID
                    || gvChargeList.FocusedColumn == colCurrencyID
                    || gvChargeList.FocusedColumn == colContainerNo)
                {
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    if (colIsAgent.OptionsColumn.AllowEdit == true)
                    {
                        if (gvChargeList.FocusedColumn == colRemark) SendKeys.Send("{TAB}");
                        else if (gvChargeList.FocusedColumn == colIsAgent)
                        {
                            if (gvChargeList.FocusedRowHandle >= 0 && gvChargeList.FocusedRowHandle == gvChargeList.RowCount - 1)
                                NewFee();
                        }
                    }
                    else if (gvChargeList.FocusedColumn == colRemark)
                    {
                        if (gvChargeList.FocusedRowHandle >= 0 && gvChargeList.FocusedRowHandle == gvChargeList.RowCount - 1)
                            NewFee();
                    }
                }

            }

        }

        private void bsChargeList_PositionChanged(object sender, EventArgs e)
        {
            RefreshChargeBillBarEnabled();
        }

        private void gvChargeList_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;
            //Amount
            if (e.Column.Name == colUnitPrice.Name || e.Column.Name == colQuantity.Name)
            {
                object row = gvChargeList.GetRow(e.RowHandle);

                if (row != null && row as ChargeList != null)
                {
                    ChargeList fee = row as ChargeList;

                    if (fee.UnitPrice < 0)
                    {
                        fee.UnitPrice = 1;
                        //小于0时反置方向
                        //fee.UnitPrice = 0 - fee.UnitPrice;
                        //if (fee.Way == FeeWay.AP) fee.Way = FeeWay.AR;
                        //else fee.Way = FeeWay.AP;
                    }

                    fee.Amount = decimal.Parse((fee.Quantity * fee.UnitPrice).ToString("n"));
                    RefreshBillFeesTatol();
                }

            }

            if (_CurrentData != null) _CurrentData.IsDirty = true;
        }

        private void gvChargeList_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentOperationID))
                return;
            if (e.Value == null)
            {
                return;
            }

            if (e.Column == colCurrencyID)
            {
                object row = gvChargeList.GetRow(e.RowHandle);
                Guid curID = new Guid(e.Value.ToString());
                if (row != null && row as ChargeList != null)
                {
                    ChargeList fee = row as ChargeList;

                    fee.Rate = 0;
                    fee.Rate = RateHelper.GetRate(curID, _ConfigureInfo.StandardCurrencyID, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified), _RateList);
                    fee.CurrencyID = curID;
                    RefreshBillFeesTatol();
                }

                if (_CurrentData != null) _CurrentData.IsDirty = true;
            }
        }

        private void gvChargeList_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 10000);
            }
        }

        #endregion

        #region BarItem Click
        /// <summary>
        /// 保存
        /// </summary>
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveData();
        }
        /// <summary>
        /// 打印
        /// </summary>
        private void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                IEnumerable<Guid> billIDs = savedBillInfos.Select(billItem => billItem.ID);
                FinanceClientService.PrintBatchBill(savedBillInfos[0].CustomerID,savedBillInfos[0].CompanyID,billIDs.ToArray(), LocalData.UserInfo.LoginID);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }
        /// <summary>
        /// 新增费用
        /// </summary>
        private void barNewFee_ItemClick(object sender, ItemClickEventArgs e)
        {
            NewFee();
        }
        /// <summary>
        /// 移除费用
        /// </summary>
        private void barRemove_ItemClick(object sender, ItemClickEventArgs e)
        {
            RemoveFee();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            var findForm = FindForm();
            if (findForm != null) findForm.Close();
        }
        #endregion

        /// <summary>
        /// 选择业务
        /// </summary>
        void rbtnBusinessInfo_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                bsChargeList.EndEdit();
                bsChargeList.ResetBindings(false);
                string oldOperationNo = string.Empty;
                if (_OperationCommonInfo != null)
                {
                    oldOperationNo = _OperationCommonInfo.OperationNo;
                }
                string title = LocalData.IsEnglish ? "Select Business Info" : "选择业务";
                OperationListFinderPart selectDataForm = Workitem.Items.AddNew<OperationListFinderPart>();
                string text = gvChargeList.GetFocusedDisplayText();
                selectDataForm.SearchOperationNo = text;
                selectDataForm.CustomerID = _CurrentData.CustomerID;
                selectDataForm.AccountDate = _CurrentData.AccountDate;

                if (DialogResult.OK !=
                    PartLoader.ShowDialog(selectDataForm, title, FormBorderStyle.FixedSingle, FormWindowState.Normal,
                        true,
                        false))
                    return;
                _OperationCommonInfo = selectDataForm.SelectedBusiness;
                if (_OperationCommonInfo != null)
                {
                    _CurrentCharge.ChooseOperationInfo = new OperationCommonInfo();
                    FAMUtility.CopyToValue(_OperationCommonInfo, _CurrentCharge.ChooseOperationInfo,
                        typeof(OperationCommonInfo));

                    _ConfigureInfo = ConfigureService.GetCompanyConfigureInfo(_OperationCommonInfo.CompanyID);
                    _RateList = ConfigureService.GetCompanyExchangeRateList(_OperationCommonInfo.CompanyID, true);
                    ReLoadBasicData();

                    _CurrentCharge.OperationNo = _OperationCommonInfo.OperationNo;
                    _CurrentCharge.FromType = (int) _OperationCommonInfo.OperationType;
                    _CurrentCharge.CurrencyID = _ConfigureInfo.DefaultCurrencyID;
                    _CurrentCharge.CurrencyName = _ConfigureInfo.DefaultCurrency;
                    _CurrentCharge.Rate = RateHelper.GetRate(_ConfigureInfo.DefaultCurrencyID,
                        _ConfigureInfo.StandardCurrencyID, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
                        _RateList);
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    if (!string.IsNullOrEmpty(oldOperationNo))
                    {
                        _CurrentCharge.OperationNo = oldOperationNo;
                    }
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 选择箱
        /// </summary>
        void rbtnConrainerNo_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentOperationID))
                    return;
                bsChargeList.EndEdit();
                bsChargeList.ResetBindings(false);
                string title = LocalData.IsEnglish ? "Select Container" : "选择箱";
                ContainersFinderPart selectDataForm = Workitem.Items.AddNew<ContainersFinderPart>();
                selectDataForm.OperationID = _CurrentOperationID;
                selectDataForm.OperationType = _CurrentOperationType;
                if (DialogResult.OK !=
                    PartLoader.ShowDialog(selectDataForm, title, FormBorderStyle.FixedSingle, FormWindowState.Normal,
                        true,
                        false))
                    return;
                FormData fromData = selectDataForm.SelectContainer;
                if (fromData != null)
                {
                    _CurrentCharge.ContainerID = fromData.ID;
                    _CurrentCharge.ContainerNo = fromData.No;
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 输入法更改
        /// </summary>
        private void gvChargeList_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentOperationID)&&e.FocusedColumn!=colOperationNo)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "Not Select Business Data" : "请选择一个业务单");
                gvChargeList.SelectCell(bsChargeList.Position, colOperationNo);
                gvChargeList.FocusedColumn = colOperationNo;
                return;
            }
            if (e.FocusedColumn == colUnitPrice || e.FocusedColumn == colQuantity)
            {
                //切换到英文输入法
                try
                {
                    var enInput = InputLanguage.FromCulture(CultureInfo.GetCultureInfo("en-US"));

                    if (enInput != null)
                    {
                        InputLanguage.CurrentInputLanguage = enInput;
                    }
                    else
                    {
                        InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                    }
                }
                catch
                {

                }
            }
            try
            {
            }
            catch
            {
            }
        }
        /// <summary>
        /// 列点击
        /// </summary>
        private void gvChargeList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentOperationID))
                return;
            if (!_ConfigureInfo.IsVATinvoice && e.Column == colIsInvoicedVAT)
            {
                //公司配置为不开增值税发票，不能选择
                return;
            }

            if (e.Column == colIsInvoicedVAT && _CurrentCharge.ChargingCodeID == _ConfigureInfo.VATFeeID)
            {
                FAMUtility.ShowMessage(LocalData.IsEnglish ? "have no choice for VAT cost" : "增值税费用不能选择！");
                return;
            }

            //账单中的是否开增值税发票没有选择  勾选上
            if (e.Column == colIsInvoicedVAT && _CurrentData.IsVATInvoiced == false)
            {
            }

            if (e.Column == colIsAgent)
            {
                _CurrentCharge.IsAgent = !_CurrentCharge.IsAgent;
            }
            else if (e.Column == colIsInvoicedVAT)
            {
                _CurrentCharge.IsVATInvoiced = !_CurrentCharge.IsVATInvoiced;
            }
            else if (e.Column == colIsSecondSale)
            {
                _CurrentCharge.IsSecondSale = !_CurrentCharge.IsSecondSale;
            }
            else if (e.Column == colIsGST)
            {
                _CurrentCharge.IsGST = !_CurrentCharge.IsGST;
            }

            bsChargeList.ResetCurrentItem();
        }
        #endregion

        #region Custom Method
        public override void EndEdit()
        {
            Validate();
            bsBillInfo.EndEdit();
            bsChargeList.EndEdit();
            bsCurrencyRateData.EndEdit();
        }

        private void InitMessage()
        {
        }

        private void InitControls()
        {
            FAMUtility.SetGridViewColumnAllowEditColor(gvChargeList);

            InitComboboxSource();
            SearchRegister();
            RefreshButtonEnable();
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        void RegisterEvent()
        {
            rbtnBusinessInfo.ButtonClick += rbtnBusinessInfo_ButtonClick;
            rbtnConrainerNo.ButtonClick += rbtnConrainerNo_ButtonClick;
        }
        /// <summary>
        /// 移除事件
        /// </summary>
        void UnRegisterEvent()
        {
            bsChargeList.PositionChanged -= bsChargeList_PositionChanged;
            rbtnBusinessInfo.ButtonClick -= rbtnBusinessInfo_ButtonClick;
            rbtnConrainerNo.ButtonClick -= rbtnConrainerNo_ButtonClick;
        }
        /// <summary>
        /// 初始化Combobox的数据源
        /// </summary>
        private void InitComboboxSource()
        {
            #region Currency
            lkeCurrency.BestFitMode = BestFitMode.BestFit;
            lkeCurrency.DisplayMember = "CurrencyName";
            lkeCurrency.ValueMember = "CurrencyID";
            lkeCurrency.TextEditStyle = TextEditStyles.DisableTextEditor;
            lkeCurrency.UseCtrlScroll = false;
            lkeCurrency.Columns.Add(new LookUpColumnInfo("CurrencyName", LocalData.IsEnglish ? "Currency" : "币种"));

            #endregion

            #region Unit
            _packTypes = TransportFoundationService.GetDataDictionaryList(string.Empty, string.Empty, DataDictionaryType.ValuationUnit, true, 0);
            foreach (var item in _packTypes)
            {
                cmbUnit.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }
            //付款单位更改事件
            cmbUnit.SelectedIndexChanged += (sender, e) =>
            {
                Guid guidUnitID = (Guid)((ImageComboBoxEdit)sender).EditValue;
                if (!"E0EBD8DD-90FC-462A-B51D-B4B1D06539D4".Equals(guidUnitID.ToString()))
                {
                    _CurrentCharge.ContainerID = null;
                    _CurrentCharge.ContainerNo = string.Empty;
                }
            };
            #endregion

            #region emun

            //FeeWays
            List<EnumHelper.ListItem<FeeWay>> feeWays = EnumHelper.GetEnumValues<FeeWay>(LocalData.IsEnglish);

            foreach (var item in feeWays)
            {
                if (item.Value == FeeWay.None) continue;
                cmbWay.Items.Add(new ImageComboBoxItem(item.Name, item.Value, (short)item.Value));
            }

            #endregion
        }

        private void BindingData(object data)
        {
            if (EditMode == EditMode.New)
            {
                BillInfo billInfo = new BillInfo
                {
                    DueDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
                    AccountDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
                    State = BillState.Created,
                    Type = BillType.AR,
                    Fees = new List<ChargeList>(),
                    CompanyID = Guid.Empty,
                    CustomerID = Guid.Empty,
                    CreateByID = LocalData.UserInfo.LoginID,
                    CreateByName = LocalData.UserInfo.LoginName,
                    CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified)
                };
                bsBillInfo.DataSource = billInfo;
                bsChargeList.DataSource = null;
                bsChargeList.DataSource = billInfo.Fees;

                seTrem.Value = (_CurrentData.DueDate.Date - _CurrentData.AccountDate.Date).Days;

                InitCustomerPopup(_CurrentData.Type);
                seTrem.Properties.ReadOnly = !LocalCommonServices.PermissionService.HaveActionPermission(ActionsConstants.FAM_TREMMANAGER);
            }

            RefreshBillFeesTatol();
        }


        /// <summary>
        /// 重新加载基本数据
        /// </summary>
        void ReLoadBasicData()
        {
            #region Currency
            _CurrencyList = ConfigureService.GetSolutionCurrencyList(_ConfigureInfo.SolutionID, true);

            lkeCurrency.DataSource = _CurrencyList;
            lkeCurrency.BestFit();
            colCurrencyID.BestFit(); 
            #endregion
            
            #region 根据业务类型判断是否启用备注
            if (_OperationCommonInfo.OperationType == OperationType.Truck ||
                (_OperationCommonInfo.OperationType == OperationType.OceanImport &&
                _OperationCommonInfo.CompanyID == new Guid("85A7D77F-2070-43E0-B866-FFF151BDCC5A")))
            {
            }
            else
            {
                colRemark.ColumnEdit = rtxtRemark;
            } 
            #endregion
        }
        
        /// <summary>
        /// 注册搜索器
        /// </summary>
        private void SearchRegister()
        {
            #region Customer
            //注册客户搜索器
            customerFinder = DataFindClientService.Register(stxtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.CustomerResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          Guid oldCustomerID = _CurrentData.CustomerID;
                          Guid customerID = new Guid(resultData[0].ToString());
                          if (customerID == oldCustomerID)
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
                                  DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "The customers has not been approved!" : "该客户尚未通过审核!"
                   , LocalData.IsEnglish ? "Tip" : "提示"
                   , MessageBoxButtons.OK);

                                  return;
                              }
                              else if (approved.Value == CustomerCodeApplyState.UnApply)
                              {
                                  if ((resultData[10] == null ||
                                      string.IsNullOrEmpty(resultData[10].ToString())) &&
                                      (resultData[11] == null ||
                                      string.IsNullOrEmpty(resultData[11].ToString())))
                                  {
                                      MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.OK);

                                      return;
                                  }

                                  DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "The customer have not yet applied for the code. Whether to apply the code?" : "该客户尚未申请代码，是否要申请代码?"
              , LocalData.IsEnglish ? "Tip" : "提示"
              , MessageBoxButtons.YesNo
              );
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
                                      MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.OK
                  );

                                      return;
                                  }

                                  DialogResult result = MessageBoxService.ShowQuestion("该客户尚未通过审核，若重新申请代码需要去完善客户资料。是否重新申请代码?"
              , LocalData.IsEnglish ? "Tip" : "提示"
              , MessageBoxButtons.YesNo
              );
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
                          CustomerInfo customer = CustomerService.GetCustomerInfo(customerID);

                          stxtCustomer.Tag = _CurrentData.CustomerID = customerID;
                          stxtCustomer.Text = _CurrentData.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                          if (_CurrentData.CustomerDescription == null) _CurrentData.CustomerDescription = new FAMCustomerDescription();
                          _CurrentData.CustomerDescription.Name = LocalData.IsEnglish ? customer.EName : customer.CName;
                          _CurrentData.CustomerDescription.Address = LocalData.IsEnglish ? customer.EAddress : customer.CAddress;
                          _CurrentData.CustomerDescription.Tel = customer.Tel1;
                          _CurrentData.CustomerDescription.Fax = customer.Fax;
                          stxtCustomer.CustomerDescription = _CurrentData.CustomerDescription;

                      }, delegate
                      {
                          stxtCustomer.Tag = _CurrentData.CustomerID = Guid.Empty;
                          stxtCustomer.Text = _CurrentData.CustomerName = string.Empty;
                          stxtCustomer.CustomerDescription = new FAMCustomerDescription();
                      },
                      ClientConstants.MainWorkspace);
            #endregion

            #region ChargingCode
            chargingCodeFinder = DataFindClientService.RegisterGridColumnFinder(colChargingCode
                                                 , CommonFinderConstants.SolutionChargingCodeFinder
                                                 , new string[] { "ChargingCodeID", "FeeCode", "ChargingDescription", "IsAgent" }
                                                 , new string[] { "ChargingCodeID", "Code", "ChargingCodeName", "IsAgent" }
                                                   , GetSolutionChargingCodeSearchCondition);

            #endregion
        }

        SearchConditionCollection GetSolutionChargingCodeSearchCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            if (_ConfigureInfo != null)
                conditions.AddWithValue("SolutionID", _ConfigureInfo.SolutionID, false);
            return conditions;
        }

        #region Save
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public bool SaveData()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                StopwatchSaveData = StopwatchHelper.StartStopwatch();
                OperationLogID = Guid.NewGuid();

                StopwatchHelper.CustomRecordStopwatch(StopwatchSaveData, OperationLogID, DateTime.Now,
                        BaseFormID, "SAVE-BATCH-BILL",
                        string.Format("账单保存;OperationID[{0}]Bill ID[{1}]", _CurrentData.OperationID, _CurrentData.ID));

                if (ValidateData() == false)
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:数据未通过验证");
                    return false;
                }
                List<BillInfo> billInfos = new List<BillInfo>();

                #region 构建 账单 费用列表参数


                foreach (var item in _CurrentData.Fees)
                {
                    BillInfo billInfo = new BillInfo
                    {
                        OperationID = item.ChooseOperationInfo.OperationID,
                        OperationType = item.ChooseOperationInfo.OperationType,
                        CompanyID = item.ChooseOperationInfo.CompanyID,
                        Type = BillType.AR,
                        FormID = item.ChooseOperationInfo.Forms[0].ID,
                        FormType = item.ChooseOperationInfo.Forms[0].Type,
                        FormNo = item.ChooseOperationInfo.Forms[0].No,

                        CustomerID = _CurrentData.CustomerID,
                        CustomerName = _CurrentData.CustomerName,
                        CustomerDescription = _CurrentData.CustomerDescription,
                        AccountDate = _CurrentData.AccountDate,
                        DueDate = _CurrentData.DueDate,
                        Fees = new List<ChargeList>()
                    };

                    billInfo.Fees.Add(item);
                    billInfos.Add(billInfo);
                }
                #endregion
                try
                {
                    #region 调用服务接口

                    List<HierarchyManyResult> results = FinanceService.BatchSaveBillInfos(billInfos
                        , LocalData.UserInfo.LoginID, DateTime.Now
                        );
                    for (int i = 0; i < billInfos.Count; i++)
                    {
                        billInfos[i].ID = results[i].GetValue<Guid>("ID");
                        billInfos[i].No = results[i].GetValue<string>("No");
                        billInfos[i].UpdateDate = results[i].GetValue<DateTime?>("UpdateDate");

                        for (int j = 0; j < billInfos[i].Fees.Count; j++)
                        {
                            billInfos[i].Fees[j].ID = results[i].Childs[j].GetValue<Guid>("ID");
                            billInfos[i].Fees[j].UpdateDate = results[i].Childs[j].GetValue<DateTime?>("UpdateDate");

                        }
                    }
                    savedBillInfos = billInfos;
                    #endregion
                    //更新业务的运费已包含项目
                    foreach (BillInfo itemInfo in billInfos)
                    {
                        FinanceService.UpdateOperationFreightIncluded(itemInfo.OperationID, "", itemInfo.OperationType);
                    }


                    AfterSaveData();
                    bsChargeList.ResetBindings(false);
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "账单保存成功");
                    RefreshButtonEnable();
                    return true;
                }
                catch (Exception ex)
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Format("账单保存失败SessionId:[{0}]", LocalData.SessionId));
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), string.Format("{0}\r\nOperationLogID[{1}]", ex.Message, OperationLogID));
                    return false;
                }
            }
        }

        private void AfterSaveData()
        {
            _CurrentData.CancelEdit();
            _CurrentData.BeginEdit();

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
        }

        bool ValidateData()
        {
            EndEdit();

            bool isSrcc = true;

            //if (_CurrentData.Validate
            //    (
            //        delegate(ValidateEventArgs e)
            //        {
            //            if (_CurrentData.FormID.IsNullOrEmpty())
            //            {
            //                e.SetErrorInfo("FormID", LocalData.IsEnglish ? "At least associated with a manifest." : "至少关联一个联单");
            //            }
            //            //IF当前业务.离港日 >= 公司配置.计费关账日 AND 账单. 计费日期 <=公司配置.计费关账日 Then 该账单的业务已在计费关账期[xxxxxxx]内，所以计费日期只能大于计费关账期。
            //            if (_OperationCommonInfo.OperationDate >= _ConfigureInfo.ChargingClosingdate && _CurrentData.AccountDate <= _ConfigureInfo.ChargingClosingdate)
            //            {
            //                e.SetErrorInfo("AccountDate", (LocalData.IsEnglish ? "The business of the bill has been off in the Charging Closing date [" : "该账单的业务已在计费关账期[") + _ConfigureInfo.ChargingClosingdate.Value.ToShortDateString() + (LocalData.IsEnglish ? "], so the billing date can only be greater than the Charging Closing date" : "]内，所以计费日期只能大于计费关账期"));
            //            }
            //        }
            //    ) == false)
            //    isSrcc = false;

            List<ChargeList> chargeList = bsChargeList.DataSource as List<ChargeList>;
            if (chargeList == null || chargeList.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "Enter at least one fee" : "至少输入一条费用");
                return false;
            }

            foreach (var item in chargeList)
            {
                ChargeList item1 = item;
                if (item.Validate
                    (
                        delegate(ValidateEventArgs e)
                        {

                            decimal de = decimal.Parse((item1.Quantity * item1.UnitPrice).ToString("n"));
                            if (item1.Amount != decimal.Parse((item1.Quantity * item1.UnitPrice).ToString("n")))
                            {
                                e.SetErrorInfo("Amount", LocalData.IsEnglish ? "The amount is invalid." : "金额输入有误.");
                            }

                            if (item1.Rate == 0m)
                            {
                                e.SetErrorInfo("Rate", LocalData.IsEnglish ? "Exchange rate is invalid." : "汇率输入有误.");
                            }
                        }
                    ) == false)
                    isSrcc = false;
            }
            return isSrcc;
        }

        #endregion

        #region Fee

        private void NewFee()
        {
            NewFee(true);
        }
        private void NewFee(bool direction)
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.CustomerID))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "Please selected Customer." : "请选择客户.");
                return;
            }
            _OperationCommonInfo = null;
            ChargeList newFeeRow = new ChargeList();
            newFeeRow.ID = Guid.Empty;
            newFeeRow.Way = FeeWay.AR;
            newFeeRow.IsVATInvoiced = false;
            newFeeRow.Quantity = 1;
            newFeeRow.UnitID = _packTypes[0].ID;
            newFeeRow.UnitName = LocalData.IsEnglish ? _packTypes[0].EName : _packTypes[0].CName;

            newFeeRow.UnitID = new Guid("E4143DCD-A8F3-E011-8ED9-001321CC6D9F");
            newFeeRow.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newFeeRow.CreateByID = LocalData.UserInfo.LoginID;
            newFeeRow.CreateByName = LocalData.UserInfo.LoginName;

            newFeeRow.Amount = newFeeRow.Quantity * newFeeRow.UnitPrice;

            (bsChargeList.List as List<ChargeList>).Add(newFeeRow);
            bsChargeList.ResetBindings(false);
            if (bsChargeList.Count > 0)
                bsChargeList.Position = bsChargeList.Count - 1;
            gcChargeList.Focus();

            gvChargeList.ClearSelection();
            gvChargeList.SelectCell(bsChargeList.Position, colOperationNo);
            gvChargeList.FocusedColumn = colOperationNo;
            _CurrentData.IsDirty = true;
        }

        private void RemoveFee()
        {
            if (_CurrentCharge == null || gvChargeList.FocusedRowHandle < 0) return;

            int[] selectRowsHandle = gvChargeList.GetSelectedRows();

            if (selectRowsHandle == null || selectRowsHandle.Length == 0) return;

            bool isDelete = true;
            for (int i = 0; i < selectRowsHandle.Length; i++)
            {
                int row = selectRowsHandle[i] - i;

                ChargeList chargeItem = gvChargeList.GetRow(row) as ChargeList;

                if (chargeItem == null)
                {
                    continue;
                }
                if (chargeItem.Type == FeeType.Price || chargeItem.Type == FeeType.Rebate)
                {
                    isDelete = false;
                }
            }
            if (isDelete)
            {
                gvChargeList.DeleteSelectedRows();
            }
            else
            {
                //删除失败，无法删除由合约产生的费用
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), NativeLanguageService.GetText(this, "DeleteChargeError"));

            }


            _CurrentData.IsDirty = true;
        }
        #endregion

        #region Customer

        void InitCustomerPopup(BillType type)
        {
            if (_CurrentData == null) return;

            if (_CurrentData.CustomerDescription == null) _CurrentData.CustomerDescription = new FAMCustomerDescription();

        }

        private void seTrem_EditValueChanged(object sender, EventArgs e)
        {
            bsBillInfo.EndEdit();
            if (_CurrentData == null || _CurrentData.AccountDate == null) return;
            dteDueDate.DateTime = _CurrentData.DueDate = _CurrentData.AccountDate.AddDays((int)seTrem.Value);
        }

        #endregion

        /// <summary>
        /// 刷新费用合计
        /// </summary>
        void RefreshBillFeesTatol()
        {
            List<ChargeList> list = bsChargeList.DataSource as List<ChargeList>;
            if (list == null || list.Count == 0)
            {
                txtChargeTotal.Text = string.Empty;
                txtApTotal.Text = string.Empty;
                txtARTotal.Text = string.Empty;
            }
            else
            {
                Dictionary<Guid, decimal> dic = new Dictionary<Guid, decimal>();
                #region 构建币种字典
                foreach (var item in list)
                {
                    if (dic.Keys.Contains(item.CurrencyID) == false)
                    {
                        dic.Add(item.CurrencyID, 0m);
                    }
                    if (item.Way == FeeWay.AR)
                    {
                        dic[item.CurrencyID] += item.Amount;
                    }
                    else
                    {
                        dic[item.CurrencyID] -= item.Amount;
                    }
                }
                #endregion

                #region 构建合计字符串
                StringBuilder strbulider = new StringBuilder();
                StringBuilder apstring = new StringBuilder();
                StringBuilder arstring = new StringBuilder();
                foreach (var item in dic)
                {
                    string currencyName = RateHelper.GetCurrencyNameByCurrencyID(item.Key);

                    if (strbulider.Length > 0) strbulider.Append(" ");
                    strbulider.Append(currencyName);
                    strbulider.Append(":" + item.Value.ToString("n"));

                    //统计应收项
                    decimal apAmount = (from d in list where d.Way == FeeWay.AP && d.CurrencyID == item.Key select d.Amount).Sum();
                    if (apstring.Length > 0) apstring.Append(" ");
                    apstring.Append(currencyName);
                    apstring.Append(":" + apAmount.ToString("n"));

                    //统计应付项
                    decimal arAmount = (from d in list where d.Way == FeeWay.AR && d.CurrencyID == item.Key select d.Amount).Sum();
                    if (arstring.Length > 0) arstring.Append(" ");
                    arstring.Append(currencyName);
                    arstring.Append(":" + arAmount.ToString("n"));


                }
                txtChargeTotal.Text = strbulider.ToString();
                txtApTotal.Text = apstring.ToString();
                txtARTotal.Text = arstring.ToString();
                #endregion
            }
        }
        /// <summary>
        /// 刷新按钮可用状态
        /// </summary>
        void RefreshChargeBillBarEnabled()
        {
            if (_CurrentCharge == null)
            {
                barRemove.Enabled = false;
            }
        }

        void RefreshButtonEnable()
        {
            barSave.Enabled = savedBillInfos == null;
            barPrint.Enabled = savedBillInfos != null;

        }

        #endregion
    }
}
