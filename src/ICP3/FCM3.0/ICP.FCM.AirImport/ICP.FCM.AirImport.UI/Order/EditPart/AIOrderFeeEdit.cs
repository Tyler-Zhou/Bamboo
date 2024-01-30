using System;
using System.Collections.Generic;
using System.Drawing;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FCM.AirImport.UI
{
    public partial class OIOrderFeeEdit : DevExpress.XtraEditors.XtraUserControl
    {
        #region 服务

        WorkItem Workitem = null;


        IConfigureService configureService { get { return Workitem.Services.Get<IConfigureService>(); } }

        #endregion

        #region 本地变量

        Guid _CompanyID = Guid.Empty;

        public Guid DefaultCustomerID
        {
            get;
            set;
        }

        public string DefaultCustomerName
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        public OIOrderFeeEdit()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false)
            {
                SetCnText();
            }

            this.Load += new EventHandler(OrderFeeEditPart_Load);
        }

        void OrderFeeEditPart_Load(object sender, EventArgs e)
        {
            cmbCurrency.SelectedIndexChanged += delegate
            {
                FeeChangedAmount();
            };
            //cmbCurrency.SelectedIndex = 0;


            if (FormType == "BusinessDownload")
            {
                this.feePartAR.bar2.Visible = false;
                this.feePartAR.gridViewFee.OptionsBehavior.Editable = false;
            }
        }

        #endregion

        #region 初始

        private void SetCnText()
        {
            this.labAPAmount.Text = "应付";
            this.labARAmount.Text = "应收";
            this.labProfit.Text = "利润";
        }

        #endregion

        #region
        List<CurrencyList> _currencyList = new List<CurrencyList>();
        private void InitControls()
        {
            _currencyList = configureService.GetCurrencyList(string.Empty, string.Empty, null, true, 0);
            if (_currencyList.Count == 0)
            {
                throw new ApplicationException("找不到币种.");
            }

            foreach (var item in _currencyList)
            {
                cmbCurrency.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Code, item.ID));
            }

            cmbCurrency.SelectedIndex = 0;
            cmbCurrency.SelectedIndexChanged += delegate
            {
                FeeChangedAmount();
            };
        }

        void feePart_FeeChanged(object sender, EventArgs e)
        {
            FeeChangedAmount();
        }

        /// <summary>
        /// TODO: 初次填充的时候执行了多次！
        /// </summary>
        private void FeeChangedAmount()
        {
            if (cmbCurrency.EditValue == null || cmbCurrency.EditValue.ToString().Length == 0)
            {
                txtProfit.Text = txtAPAmount.Text = txtARAmount.Text = string.Empty;
                txtProfit.ForeColor = Color.Black;
            }
            else
            {
                Guid currencyID = new Guid(cmbCurrency.EditValue.ToString());
                if (currencyID == Guid.Empty) return;

                try
                {
                    decimal arAmount = feePartAR.GetProfit(currencyID);
                    txtProfit.Text = arAmount.ToString("N");
                    if (arAmount < 0m) txtProfit.ForeColor = Color.Red;
                    else txtProfit.ForeColor = Color.Black;


                    txtARAmount.Text = feePartAR.GetDRAmountString();
                    txtAPAmount.Text = feePartAR.GetCRAmountString();

                }
                catch (ApplicationException ex)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), ex.Message.ToString());
                    txtProfit.ForeColor = Color.Black;
                    txtProfit.Text = "0";
                }
            }
        }

        #endregion

        #region 属性
        public string FormType
        {
            get;
            set;
        }
        #endregion

        #region IChildPart 成员

        public bool IsChanged
        {
            get { return feePartAR.IsChanged; }
        }
        public bool ValidateData()
        {
            if (feePartAR.ValidateData())
                return true;
            else
                return false;
        }
        public void AfterSaved()
        {
            feePartAR.AfterSaved();
        }

        public object DataSource
        {
            get
            {
                List<AirImportFeeList> list = new List<AirImportFeeList>();
                if (feePartAR.DataSource != null)
                    list.AddRange(feePartAR.DataSource as List<AirImportFeeList>);

                return list;
            }
        }
        public void SetSource(object value)
        {
            if (DesignMode || value == null) return;
            List<AirImportFeeList> list = value as List<AirImportFeeList>;

            this.feePartAR.SetSource(list);
            this.feePartAR.DefaultCustomerID = DefaultCustomerID;
            this.feePartAR.DefaultCustomerName = DefaultCustomerName;

            this.feePartAR.DataChanged -= new EventHandler(feePart_FeeChanged);
            this.feePartAR.DataChanged += new EventHandler(feePart_FeeChanged);

            this.FeeChangedAmount();
        }
        public void SetService(WorkItem workitem)
        {
            this.Workitem = workitem;
            this.feePartAR.SetService(workitem);
            InitControls();
        }      

        public void SetCompanyID(Guid companyID)
        {

            if (companyID != Guid.Empty && _CompanyID != companyID)
            {
                _CompanyID = companyID;

                List<SolutionExchangeRateList> _RatList = configureService.GetCompanyExchangeRateList(_CompanyID, true);
                ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(_CompanyID);

                if (configureInfo != null)
                {
                    List<SolutionCurrencyList> currency = configureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);

                    feePartAR.SetExchangeRateAndCurrency(_RatList, currency, configureInfo.DefaultCurrencyID, configureInfo.SolutionID);
                    cmbCurrency.EditValue = configureInfo.DefaultCurrencyID;
                }

                FeeChangedAmount();
            }
        }

        #endregion


        /// <summary>
        /// 另存为的时候需要
        /// </summary>
        public void ResetPrimaryKeys()
        {
            List<AirImportFeeList> list = this.DataSource as List<AirImportFeeList>;
            if (list != null)
            {
                list.ForEach(o => o.ID = Guid.NewGuid());
            }
        }

        #region 保存费用

        public List<FeeSaveRequest> GetFeeList(Guid OrderID)
        {
            this.feePartAR.gridViewFee.CloseEditor();
            List<AirImportFeeList> fees = this.DataSource as List<AirImportFeeList>;
            if (fees.Count != 0)
            {
                List<AirImportFeeList> changedFees = fees.FindAll(o => o.IsDirty);
                if (OrderID == Guid.Empty)
                {
                    changedFees = fees;
                }

                if (changedFees.Count > 0)
                {
                    List<FeeSaveRequest> commands = new List<FeeSaveRequest>();

                    List<Guid?> ids = new List<Guid?>();
                    List<Guid> customerIDs = new List<Guid>(), chargeCodeIDs = new List<Guid>(), currencieIDs = new List<Guid>();
                    List<decimal> quantities = new List<decimal>(), unitPrices = new List<decimal>(), amounts = new List<decimal>();
                    List<string> remarks = new List<string>();
                    List<FeeWay> ways = new List<FeeWay>();
                    List<DateTime?> updateDates = new List<DateTime?>();
                    foreach (var item in changedFees)
                    {
                        ids.Add(item.ID);
                        customerIDs.Add(item.CustomerID);
                        chargeCodeIDs.Add(item.ChargingCodeID);
                        currencieIDs.Add(item.CurrencyID);
                        quantities.Add(item.Quantity);
                        unitPrices.Add(item.UnitPrice);
                        amounts.Add(item.Amount);
                        ways.Add(item.Way);
                        remarks.Add(item.Remark);
                        updateDates.Add(item.UpdateDate);
                    }

                    FeeSaveRequest feeInfo = new FeeSaveRequest();
                    feeInfo.orderID = OrderID;
                    feeInfo.ids = ids.ToArray();
                    feeInfo.customerIDs = customerIDs.ToArray();
                    feeInfo.chargingCodeIDs = chargeCodeIDs.ToArray();
                    feeInfo.currencyIDs = currencieIDs.ToArray();
                    feeInfo.quantities = quantities.ToArray();
                    feeInfo.unitPrices = unitPrices.ToArray();
                    feeInfo.ways = ways.ToArray();
                    feeInfo.amounts = amounts.ToArray();
                    feeInfo.remarks = remarks.ToArray();
                    feeInfo.saveByID = LocalData.UserInfo.LoginID;
                    feeInfo.updateDates = updateDates.ToArray();

                    changedFees.ForEach(o => feeInfo.AddInvolvedObject(o));

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

        public void RefreshUI(List<FeeSaveRequest> list)
        {
            foreach (FeeSaveRequest feeInfo in list)
            {
                List<AirImportFeeList> changedFees = feeInfo.UnBoxInvolvedObject<AirImportFeeList>();
                ManyResult result = feeInfo.ManyResult;


                for (int i = 0; i < changedFees.Count; i++)
                {
                    changedFees[i].ID = result.Items[i].GetValue<Guid>("ID");
                    changedFees[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                    changedFees[i].IsDirty = false;
                }
            }
            this.AfterSaved();
        }

        #endregion
    }
}

