using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FCM.OceanExport.UI.Order
{
    [ToolboxItem(false)]
    public partial class OrderFeeEditPart : BasePart , IChildPart
    {
        #region 服务

        WorkItem Workitem = null;
        IConfigureService configureService { get { return Workitem.Services.Get<IConfigureService>(); } }

        #endregion

        #region 本地变量

        Guid _CompanyID = Guid.Empty;
        bool isLoad = false;
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

            this.Load += new EventHandler(OrderFeeEditPart_Load);
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
            cmbCurrency.Properties.BeginUpdate();
            foreach (var item in _currencyList)
            {
                cmbCurrency.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Code, item.ID));
            }

            cmbCurrency.Properties.EndUpdate();
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
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), ex.Message.ToString());
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
                List<OceanBookingFeeList> list = new List<OceanBookingFeeList>();
                if (feePartAR.DataSource != null)
                    list.AddRange(feePartAR.DataSource as List<OceanBookingFeeList>);
                
                return list;
            }
        }
        public void SetSource(object value) 
        {
            if (DesignMode|| value == null) return;
            List<OceanBookingFeeList> list = value as List<OceanBookingFeeList>;
            //List<OceanBookingFeeList> arFees = list.FindAll(delegate(OceanBookingFeeList item) { return item.Way == FeeWay.DR; });
            //List<OceanBookingFeeList> apFees = list.FindAll(delegate(OceanBookingFeeList item) { return item.Way == FeeWay.CR; });
            //if (arFees == null) arFees = new List<OceanBookingFeeList>();
            //if (apFees == null) apFees = new List<OceanBookingFeeList>();

            this.feePartAR.SetSource(list);
            //this.feePartAR.SetFeeWay(FeeWay.DR);
            //this.feePartAP.SetSource(apFees);
            //this.feePartAP.SetFeeWay(FeeWay.CR);

            this.feePartAR.DataChanged -= new EventHandler(feePart_FeeChanged);
            //this.feePartAP.DataChanged -= new EventHandler(feePart_FeeChanged);
            this.feePartAR.DataChanged += new EventHandler(feePart_FeeChanged);
            //this.feePartAP.DataChanged += new EventHandler(feePart_FeeChanged);

            this.FeeChangedAmount();
            isLoad = true;
        }
        public void SetService(WorkItem workitem)
        {
            this.Workitem = workitem;
            this.feePartAR.SetService(workitem);
            //this.feePartAP.SetService(workitem);
            InitControls();
        }
        /// <summary>
        /// 公司发生改变时
        /// </summary>
        /// <param name="companyID"></param>
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
                    //feePartAP.SetExchangeRateAndCurrency(_RatList, currency, configureInfo.DefaultCurrencyID);
                    cmbCurrency.EditValue = configureInfo.DefaultCurrencyID;
                }
                if (isLoad)
                {
                    FeeChangedAmount();
                }
            }
        }

        #endregion        


        /// <summary>
        /// 另存为的时候需要
        /// </summary>
        public void ResetPrimaryKeys()
        {
            List<OceanBookingFeeList> list = this.DataSource as List<OceanBookingFeeList>;
            if (list != null)
            {
                list.ForEach(o => o.ID = Guid.NewGuid());
            }
        }

        #region 保存费用

        public List<FeeSaveRequest> SaveFee(Guid OrderID)
        {
            this.feePartAR.gridViewFee.CloseEditor();
            List<OceanBookingFeeList> fees = this.DataSource as List<OceanBookingFeeList>;
            if (fees.Count != 0)
            {
                List<OceanBookingFeeList> changedFees = fees.FindAll(o => o.IsDirty);
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
                List<OceanBookingFeeList> changedFees = feeInfo.UnBoxInvolvedObject<OceanBookingFeeList>();
                ManyResult result = feeInfo.ManyResult;

                //ManyResult result = this.oceanExportService.SaveOceanOrderFeeList(feeInfo);

                for (int i = 0; i < changedFees.Count; i++)
                {
                    changedFees[i].ID = result.Items[i].GetValue<Guid>("ID");
                    changedFees[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                    changedFees[i].IsDirty = false;
                }
            }
            //this.SetSource(fees);
            this.AfterSaved();
        }

        #endregion
    }
}

