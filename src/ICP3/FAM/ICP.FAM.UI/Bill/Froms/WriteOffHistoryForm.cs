using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Controls;
using ICP.FAM.ServiceInterface;

namespace ICP.FAM.UI.Bill
{
    [ToolboxItem(false)]
    public partial class WriteOffHistoryForm : BaseListPart
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
        public WriteOffHistoryForm()
        {
            InitializeComponent();
            Disposed += delegate {
                gcMain.DataSource = null;
                cmbCurrency.SelectedIndexChanged -= cmbCurrency_SelectedIndexChanged;
                bsList.DataSource = null;
                bsList.Dispose();

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                FAMUtility.ShowGridRowNo(gvMain);
                InitControls();
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitControls()
        {


            # region 绑定列表数据
            List<WriteOffItemList> list = FinanceService.GetWriteOffListByBill(
                billList.ID,
                billList.CurrencyID,
                billList.Way,
                billList.IsCommission,
                LocalData.IsEnglish);
            DataSource = list;


            gvMain.BestFitColumns();
            #endregion

            InitCurrency();
        }
        /// <summary>
        /// 初始化币种信息
        /// </summary>
        private void InitCurrency()
        {
            cmbCurrency.Properties.Items.Clear();

            foreach (SolutionCurrencyList currency in CurrencyList)
            {
                cmbCurrency.Properties.Items.Add(new ImageComboBoxItem(currency.CurrencyName, currency.CurrencyID));
            }

            cmbCurrency.EditValue = DefaultCurrencyID;

        }

        #endregion

        #region 本地变量
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                List<WriteOffItemList> list=value as List<WriteOffItemList>;

                bsList.DataSource = list;
                bsList.ResetBindings(false);

                TotalAmount();

                string s=list.Count.ToString();
                labTotalCount.Text = LocalData.IsEnglish ? "Total " + s + " Count Data" : "共 " + s + " 条数据";
            }
        }
        #endregion

        #region 属性
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
        /// 销账List
        /// </summary>
        public List<WriteOffItemList> WriteOffList
        {
            get
            {
                return bsList.DataSource as List<WriteOffItemList>;
            }
        }

        /// <summary>
        /// 账单信息
        /// </summary>
        public CurrencyBillList billList
        {
            get;
            set;
        }



        #endregion


        #region 统计信息
        /// <summary>
        /// 统计所有的币种信息
        /// </summary>
        private void TotalAmount()
        {
            txtTotalAmount.Text = string.Empty;
            Dictionary<string, decimal> dicTotal = (from d in WriteOffList group d by d.Currency into g select new { g.Key, TotalAmount = g.Sum(d => d.Amount) }).ToDictionary(c => c.Key, c => c.TotalAmount);

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
            
            if (cmbCurrency.EditValue == null || (Guid)cmbCurrency.EditValue == Guid.Empty)
            {
                return;
            }
            Guid currencyID = (Guid)cmbCurrency.EditValue;
            if (WriteOffList == null)
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
            Dictionary<Guid, Decimal> dicTotal = (from d in WriteOffList group d by d.CurrencyID into g select new { g.Key, TotalAmount = g.Sum(p => p.Amount) }).ToDictionary(c => c.Key, c => c.TotalAmount);
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
