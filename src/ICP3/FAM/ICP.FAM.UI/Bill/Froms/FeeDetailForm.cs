using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Controls;
using ICP.FAM.ServiceInterface;

namespace ICP.FAM.UI.Bill
{
    [ToolboxItem(false)]
    public partial class FeeDetailForm : BaseListPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

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

        public FeeDetailForm()
        {
            InitializeComponent();

            if (DesignMode) return;

            Disposed += delegate {
                
                cmbCurrency.SelectedIndexChanged -= cmbCurrency_SelectedIndexChanged;
                gcChargeList.DataSource = null;
                bsBill.DataSource = null;
                bsBill.Dispose();
                bsChargeList.DataSource = null;
                bsChargeList.Dispose();
                
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        private void InitControls()
        {
           
            DevHelper.FormatMoney(seAmount);
            DevHelper.FormatMoney(seWriteOffAmount);

            //帐单状态
            List<EnumHelper.ListItem<BillState>> currencyBillStates
                = EnumHelper.GetEnumValues<BillState>(LocalData.IsEnglish);
            cmbState.Properties.BeginUpdate();
            foreach (var item in currencyBillStates)
            {
                cmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbState.Properties.EndUpdate();

            InitCurrency();

            cmbWay.Items.Add(new ImageComboBoxItem("", FeeWay.AR, 0));
            cmbWay.Items.Add(new ImageComboBoxItem("", FeeWay.AP, 1));

            //绑定费用明细列表
            List<ChargeList> chargeList = FinanceService.GetChargeList(BillDataSource.ID, BillDataSource.CurrencyID, BillDataSource.Way, BillDataSource.IsCommission);

            ChargeDataSource=chargeList;

            cmbState.BackColor = dteCheckDate.BackColor = dteBankDate.BackColor = txtBillNO.BackColor;


        }
        /// <summary>
        /// 初始化币种信息
        /// </summary>
        private void InitCurrency()
        {
            cmbCurrency.Properties.BeginUpdate();
            cmbCurrency.Properties.Items.Clear();

            foreach (SolutionCurrencyList currency in CurrencyList)
            {
                cmbCurrency.Properties.Items.Add(new ImageComboBoxItem(currency.CurrencyName, currency.CurrencyID));
            }
            cmbCurrency.Properties.EndUpdate();

            cmbCurrency.EditValue = DefaultCurrencyID;
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControls();
            }
        }

        #endregion

        #region 属性

        /// <summary>
        /// 账单数据
        /// </summary>
        public CurrencyBillList BillDataSource 
        {
            get
            {
                return bsBill.DataSource as CurrencyBillList;
            }
            set
            {
                bsBill.DataSource = value;

                bsBill.ResetBindings(false);
                    
            }
        }
        /// <summary>
        /// 费用数据
        /// </summary>
        public object ChargeDataSource
        {
            get
            {
                return bsChargeList.DataSource;
            }
            set
            {
                List<ChargeList> list = value as List<ChargeList>;

                gvChargeList.BeginUpdate();
                bsChargeList.DataSource = list;

                bsChargeList.ResetBindings(false);

                gvChargeList.EndUpdate();

                TotalAmount();

                labTotalCount.Text = LocalData.IsEnglish ? "Total " + list .Count.ToString()+ " count Data" : "共 "+list.Count.ToString()+" 条数据";
            }
        }

        /// <summary>
        /// 汇率列表
        /// </summary>
        public List<SolutionExchangeRateList> RateList
        {
            get;
            set;
        }
        /// <summary>
        /// 币种列表
        /// </summary>
        public List<SolutionCurrencyList> CurrencyList
        {
            get;
            set;
        }
        /// <summary>
        /// 默认币种
        /// </summary>
        public Guid? DefaultCurrencyID
        {
            get;
            set;
        }

        /// <summary>
        /// 费用数据List
        /// </summary>
        public List<ChargeList> ChargeDataList
        {
            get
            {
                return bsChargeList.DataSource as List<ChargeList>;
            }
        }


        #endregion

        #region 统计信息
        /// <summary>
        /// 统计所有的币种信息
        /// </summary>
        private void TotalAmount()
        {
            txtTotalAmount.Text = string.Empty;
            Dictionary<string, decimal> dicTotal = (from d in ChargeDataList group d by d.CurrencyName into g select new { g.Key, TotalAmount = g.Sum(d => d.Amount) }).ToDictionary(c => c.Key, c => c.TotalAmount);

            if (dicTotal == null || dicTotal.Count == 0)
            {
                txtTotalByCurrency.Text = string.Empty;
                return;
            }

            foreach (KeyValuePair<string, Decimal> item in dicTotal)
            {
                txtTotalAmount.Text += item.Key + ": " + item.Value.ToString("n") + "  ";
            }

            TotalAmountByCurrency();
        }
        /// <summary>
        /// 合计成为一种币种时的统计
        /// </summary>
        private void TotalAmountByCurrency()
        {
            if (cmbCurrency.EditValue == null)
            {
                return;
            }
            Guid currencyID = (Guid)cmbCurrency.EditValue;
            if (cmbCurrency.EditValue == null || (Guid)cmbCurrency.EditValue == Guid.Empty)
            {
                return;
            }

            if (ChargeDataList == null)
            {
                return;
            }
            decimal amount = 0;


            if (FAMUtility.GuidIsNullOrEmpty(currencyID) || RateList == null || RateList.Count == 0)
            {
                string message = LocalData.IsEnglish ? "Have no rate at current company." : "找不到当前公司下的汇率.";

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);

                return;
            }

            //循环账单的信息
            Dictionary<Guid, Decimal> dicTotal = (from d in ChargeDataList group d by d.CurrencyID into g select new { g.Key, TotalAmount = g.Sum(p => p.Amount) }).ToDictionary(c => c.Key, c => c.TotalAmount);

            foreach (KeyValuePair<Guid, Decimal> item in dicTotal)
            {
                if (FAMUtility.GuidIsNullOrEmpty(item.Key))
                {
                    continue;
                }
                if (currencyID == item.Key)
                {
                    amount += item.Value;
                }
                else
                {
                    decimal rate = RateHelper.GetRate(item.Key, currencyID, DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified), RateList);
                    if (rate > 0)
                    {
                        amount += item.Value * rate;
                    }
                }
            }

            txtTotalByCurrency.Text = amount.ToString("n");
        }

        private void cmbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            TotalAmountByCurrency();
        }

        #endregion

        #region 窗体事件
        private void btnClose_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }
        #endregion

     

    
    }
}
