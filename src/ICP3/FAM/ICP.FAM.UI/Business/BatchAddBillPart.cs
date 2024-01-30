using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using DevExpress.XtraEditors.Controls;

namespace ICP.FAM.UI.Business
{
    [ToolboxItem(false)]
    public partial class BatchAddBillPart : BaseEditPart
    {
        #region 服务


        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }


        public RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }

        #endregion

        #region 初始化

        public BatchAddBillPart()
        {
            InitializeComponent();
            if (DesignMode) return;
            Disposed += delegate {
                if (chargingCodeFinder != null)
                {
                    chargingCodeFinder.Dispose();
                    chargingCodeFinder = null;
                }
                _BussinessSource = null;
                _RatList = null;
                gcMain.DataSource = null;
                bsChargeList.DataSource = null;
                bsChargeList.Dispose();
                Saved = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
     
        #endregion

        #region  本地变量

        /// <summary>
        /// 数据源
        /// </summary>
        List<BusinessList> _BussinessSource = null;

        /// <summary>
        /// 汇率
        /// </summary>
        List<SolutionExchangeRateList> _RatList = null;

        #endregion

        #region InitControls

        private void InitControls()
        {
            InitComboboxSource();
            SearchRegister();
        }

        void InitComboboxSource()
        {
            #region Bank
            List<BankAccountList> banks = FinanceService.GetCompanyBankAccounts(_BussinessSource[0].CompanyID, LocalData.IsEnglish);
            foreach (var item in banks)
            {
                cmbBank.Properties.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.ID));
            }
            #endregion

            #region Currency

            List<SolutionCurrencyList> _CurrencyList = ConfigureService.GetSolutionCurrencyList(_BussinessSource[0].SolutionID, true);
            if (_CurrencyList == null || _CurrencyList.Count == 0)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Currency Not Find." : "找不到币种.");
                return;
            }
            cmbCurrency.Properties.BeginUpdate();
            foreach (var item in _CurrencyList)
            {
                cmbCurrency.Properties.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.CurrencyID));
            }
            cmbCurrency.Properties.EndUpdate();
            cmbCurrency.EditValue = _BussinessSource[0].DefaultCurrencyID;
            #endregion

            colProfit.Caption += "(" + _BussinessSource[0].DefaultCurrencyName + ")";
            colOriginalProfit.Caption += "(" + _BussinessSource[0].DefaultCurrencyName + ")";
            _RatList = ConfigureService.GetSolutionExchangeRateList(_BussinessSource[0].SolutionID, true);

        }
        private IDisposable chargingCodeFinder;
        /// <summary>
        /// 注册搜索器
        /// </summary>
        void SearchRegister()
        {
            #region ChargingCode

          chargingCodeFinder=  DataFindClientService.Register(stxtChargingCode,
               CommonFinderConstants.SolutionChargingCodeFinder,
               SearchFieldConstants.CodeName,
               SearchFieldConstants.ChargeCodeResultValue,
               GetConditionsForSearchChargingCode,
                      delegate(object inputSource, object[] resultData)
                      {
                          stxtChargingCode.Tag = resultData[1];
                          stxtChargingCode.Text = resultData[2].ToString();
                      },
                      delegate
                      {
                          stxtChargingCode.Tag = null;
                          stxtChargingCode.Text = string.Empty;
                      },
                      ClientConstants.MainWorkspace);

            #endregion
        }

        /// <summary>
        /// ChargingCode
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForSearchChargingCode()
        {
            Guid solutionID = _BussinessSource[0].SolutionID;

            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", solutionID, false);
            return conditions;
        }

        #endregion

        #region Event

        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveData();
        }

        public override bool SaveData()
        {
            if (ValidateData() == false) return false;

            try
            {
                List<Guid> operationIDs = new List<Guid>(), bankIDs = new List<Guid>(), currencyIDs = new List<Guid>(), chargingCodeIDs = new List<Guid>();
                List<FeeWay> feeWays = new List<FeeWay>();
                List<decimal> amounts = new List<decimal>();

                List<BatchChargeList> source = bsChargeList.DataSource as List<BatchChargeList>;
                Guid bankID = new Guid(cmbBank.EditValue.ToString());
                Guid currencyID = new Guid(cmbCurrency.EditValue.ToString());
                Guid chargingCodeID = new Guid(stxtChargingCode.Tag.ToString());

                foreach (var item in source)
                {
                    operationIDs.Add(item.OperationID);
                    currencyIDs.Add(currencyID);
                    chargingCodeIDs.Add(chargingCodeID);
                    feeWays.Add(FeeWay.AP);
                    amounts.Add(item.Amount);
                }

                ManyResult result = FinanceService.BatchSaveBillInfo(operationIDs.ToArray()
                                                             , bankID
                                                             , currencyIDs.ToArray()
                                                             , chargingCodeIDs.ToArray()
                                                             , feeWays.ToArray()
                                                             , amounts.ToArray()
                                                             , LocalData.UserInfo.LoginID);
                List<Guid> needUpdateIDs = new List<Guid>();
                foreach (var item in result.Items)
                {
                    needUpdateIDs.Add(item.GetValue<Guid>("ID"));
                }

                btnBulid.Enabled = barRemove.Enabled = barSave.Enabled = false;
                if (Saved != null) Saved(needUpdateIDs);
                return true;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return false; }
        }
       

        private void btnBulidBill_Click(object sender, EventArgs e)
        {
            if(ValidateBulid()==false) return;

            List<BatchChargeList> source = bsChargeList.DataSource as List<BatchChargeList>;
            if (source == null || source.Count == 0) return;

            //是否按百分比计算
            bool isPercentage = rdoProfitType.SelectedIndex == 0;
            decimal percentage = sePercentage.Value/100m;
            decimal afterBulidAmount = seAmount.Value;
            Guid currencyID = new Guid(cmbCurrency.EditValue.ToString());
            decimal rate = RateHelper.GetRate(currencyID, _BussinessSource[0].DefaultCurrencyID, FAMUtility.GetEndDate(DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified)), _RatList);

            foreach (var item in source)
            {
                if (isPercentage)
                {
                    item.Profit = item.OriginalProfit * percentage;
                    decimal tempAmount = item.OriginalProfit -item.Profit;
                    item.Amount = tempAmount * rate;
                }
                else
                {
                    item.Profit = afterBulidAmount;
                    decimal tempAmount = item.OriginalProfit - item.Profit;
                    item.Amount = tempAmount / rate;
                }
            }
            bsChargeList.DataSource = source;
            bsChargeList.ResetBindings(false);

            colAmount.Caption = LocalData.IsEnglish ? "Amount" : "金额";
            colAmount.Caption += "(" + cmbCurrency.Text + ")";

        }

        bool ValidateData()
        {
            dxErrorProvider1.ClearErrors();
            Validate();
            bsChargeList.EndEdit();
            bool isScrr = true;
            if (stxtChargingCode.Tag == null || stxtChargingCode.Tag.ToString() == string.Empty || new Guid(stxtChargingCode.Tag.ToString()).IsNullOrEmpty())
            {
                dxErrorProvider1.SetError(stxtChargingCode, LocalData.IsEnglish ? "ChargingCode Must Input." : "费用项目必须输入.");
                isScrr = false;
            }

            if (cmbBank.EditValue == null)
            {
                dxErrorProvider1.SetError(cmbBank, LocalData.IsEnglish ? "Bank Must Input." : "银行帐号必须输入.");
                isScrr = false;
            }
            List<BatchChargeList> source = bsChargeList.DataSource as List<BatchChargeList>;
            foreach (var item in source)
            {
                if (item.Validate() == false) isScrr = false;
            }

            return isScrr;
        }
        private bool ValidateBulid()
        {
            dxErrorProvider1.ClearErrors();
            Validate();

            bool isScrr = true;
            if (cmbCurrency.EditValue ==null)
            {
                dxErrorProvider1.SetError(cmbCurrency, LocalData.IsEnglish ? "Must Input." : "必需输入.");
                isScrr = false;
            }
            if (cmbBank.EditValue == null)
            {
                dxErrorProvider1.SetError(cmbBank, LocalData.IsEnglish ? "Must Input." : "必需输入.");
                isScrr = false;
            }
            if (rdoProfitType.SelectedIndex == 1 && seAmount.Value <= 0)
            {
                dxErrorProvider1.SetError(seAmount, LocalData.IsEnglish ? "Amount can't be 0." : "费用不能为0.");
                isScrr = false;
            }
            return isScrr;
        }

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        private void gvMain_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0 || e.Column != colAmount) return;

            BatchChargeList fee = gvMain.GetRow(e.RowHandle) as BatchChargeList;
            fee.Profit = fee.OriginalProfit - fee.Amount;
        }
        #endregion

        #region IEditPart 成员

        void BindingData(object data)
        {
            _BussinessSource = data as List<BusinessList>;
            if (_BussinessSource == null || _BussinessSource.Count == 0)
            {
                FindForm().Close();
            }

            InitControls();

            #region Bulid Bill
            List<BatchChargeList> list = new List<BatchChargeList>();
            foreach (var item in _BussinessSource)
            {
                BatchChargeList temp = new BatchChargeList();
                temp.OperationID = item.ID;
                temp.OperationNO = item.OperationNO;
                temp.Amount = 0;                
                temp.OriginalProfit = temp.Profit = item.Profit;
                list.Add(temp);
            }
            bsChargeList.DataSource = list;
            bsChargeList.ResetBindings(false);
            #endregion
        }

        public override object DataSource
        {
            get { return _BussinessSource; }
            set { BindingData(value); }
        }

        public override void EndEdit()
        {
            Validate();
            bsChargeList.EndEdit();
        }

        public override event SavedHandler Saved;

        #endregion
    }
}
