using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.AirExport.ServiceInterface.CompositeObjects;
using ICP.FAM.ServiceInterface.DataObjects;


namespace ICP.FCM.AirExport.UI.Order
{
    [ToolboxItem(false)]
    public partial class OrderFeeEditPart : XtraUserControl, IChildPart
    {
        #region 服务

        WorkItem Workitem = null;
        IConfigureService ConfigureService 
        { 
            get {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        #region 本地变量

        Guid _CompanyID = Guid.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        public OrderFeeEditPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false)
            {
                SetCnText();
            }
            Disposed += delegate
            {
                DataChanged = null;
                _currencyList = null;
               
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            };

            Load += new EventHandler(OrderFeeEditPart_Load);
        }

        void OrderFeeEditPart_Load(object sender, EventArgs e)
        {
            cmbCurrency.SelectedIndexChanged += delegate
            {
                FeeChangedAmount();
            };
            //cmbCurrency.SelectedIndex = 0;
        }

        #endregion

        #region 初始

        private void SetCnText()
        {
            labAPAmount.Text = "应付";
            labARAmount.Text = "应收";
            labProfit.Text = "利润";
        }

        #endregion

        #region 
        List<CurrencyList> _currencyList = new List<CurrencyList>();
        private void InitControls()
        {
            _currencyList = ConfigureService.GetCurrencyList(string.Empty, string.Empty, null, true, 0);
            if (_currencyList.Count == 0)
            {
                throw new ApplicationException("找不到币种.");
            }

            foreach (var item in _currencyList)
            {
                cmbCurrency.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }
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
            if (cmbCurrency.EditValue == null || cmbCurrency.EditValue.ToString ().Length ==0)
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
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), ex.Message.ToString());
                    txtProfit.ForeColor = Color.Black;
                    txtProfit.Text = "0";
                }
            }
        }

        #endregion

        #region IChildPart 成员

        public event EventHandler DataChanged;
        public bool IsChanged
        {
            get { return feePartAR.IsChanged; }
        }
        public bool ValidateData()
        {
            if ( feePartAR.ValidateData())
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
                List<AirBookingFeeList> list = new List<AirBookingFeeList>();
                if (feePartAR.DataSource != null)
                    list.AddRange(feePartAR.DataSource as List<AirBookingFeeList>);
                
                return list;
            }
        }
        public void SetSource(object value) 
        {
            if (DesignMode|| value == null) return;
            List<AirBookingFeeList> list = value as List<AirBookingFeeList>;
            //List<AirBookingFeeList> arFees = list.FindAll(delegate(AirBookingFeeList item) { return item.Way == FeeWay.DR; });
            //List<AirBookingFeeList> apFees = list.FindAll(delegate(AirBookingFeeList item) { return item.Way == FeeWay.CR; });
            //if (arFees == null) arFees = new List<AirBookingFeeList>();
            //if (apFees == null) apFees = new List<AirBookingFeeList>();

            feePartAR.SetSource(list);
            //this.feePartAR.SetFeeWay(FeeWay.DR);
            //this.feePartAP.SetSource(apFees);
            //this.feePartAP.SetFeeWay(FeeWay.CR);

            feePartAR.DataChanged -= new EventHandler(feePart_FeeChanged);
            //this.feePartAP.DataChanged -= new EventHandler(feePart_FeeChanged);
            feePartAR.DataChanged += new EventHandler(feePart_FeeChanged);
            //this.feePartAP.DataChanged += new EventHandler(feePart_FeeChanged);

            FeeChangedAmount();
        }
        public void SetService(WorkItem workitem)
        {
            Workitem = workitem;
            feePartAR.SetService(workitem);
            //this.feePartAP.SetService(workitem);
            InitControls();
        }

        public void SetCompanyID(Guid companyID)
        {

            if (companyID != Guid.Empty && _CompanyID != companyID)
            {
                _CompanyID = companyID;
                feePartAR.SetCompanyID(_CompanyID);
                List<SolutionExchangeRateList> _RatList = ConfigureService.GetCompanyExchangeRateList(_CompanyID, true);
                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(_CompanyID);

                if (configureInfo != null)
                {
                    List<SolutionCurrencyList> currency = ConfigureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);

                    feePartAR.SetExchangeRateAndCurrency(_RatList, currency, configureInfo.DefaultCurrencyID, configureInfo.SolutionID);
                    //feePartAP.SetExchangeRateAndCurrency(_RatList, currency, configureInfo.DefaultCurrencyID);
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
            List<AirBookingFeeList> list = DataSource as List<AirBookingFeeList>;
            if (list != null)
            {
                list.ForEach(o => o.ID = Guid.NewGuid());
            }
        }

        #region 保存费用

        public List<FeeSaveRequest> SaveFee(Guid OrderID,Guid companyID)
        {
            feePartAR.gridViewFee.CloseEditor();
            if (!Validate()) { return null; }
            List<AirBookingFeeList> fees = DataSource as List<AirBookingFeeList>;
            if (fees.Count != 0)
            {
                List<AirBookingFeeList> changedFees = fees.FindAll(o => o.IsDirty);//明细无更改则不执行保存明细之过程
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
                        if (item.UnitPrice <= Convert.ToDecimal(0)) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "The unit price can not less than zero" : "单价必须大于0"); return null; }
                        unitPrices.Add(item.UnitPrice);
                        amounts.Add(item.Amount);
                        ways.Add(item.Way);
                        remarks.Add(item.Remark);
                        updateDates.Add(item.UpdateDate);
                    }

                    FeeSaveRequest feeInfo = new FeeSaveRequest();
                    feeInfo.orderID = OrderID;
                    feeInfo.companyID = companyID;
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
                List<AirBookingFeeList> changedFees = feeInfo.UnBoxInvolvedObject<AirBookingFeeList>();
                ManyResult result = feeInfo.ManyResult;

                //ManyResult result = this.oceanExportService.SaveAirOrderFeeList(feeInfo);

                for (int i = 0; i < changedFees.Count; i++)
                {
                    changedFees[i].ID = result.Items[i].GetValue<Guid>("ID");
                    changedFees[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                    changedFees[i].IsDirty = false;
                }
            }
            //this.SetSource(fees);
            AfterSaved();
        }

        #endregion
    }
}

