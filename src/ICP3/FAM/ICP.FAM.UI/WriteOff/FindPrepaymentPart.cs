using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FAM.UI.WriteOff
{
    public partial class FindPrepaymentPart : BaseListPart
    {
        public FindPrepaymentPart()
        {
            InitializeComponent();
        }

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public IDataFinderFactory dataFinderFactory
        {
            get
            {
                return ServiceClient.GetClientService<IDataFinderFactory>();
            }
        }

        public RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }
        IDataFindClientService dfService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        #endregion

        #region 关闭
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().DialogResult = DialogResult.Cancel;
            FindForm().Close();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 币种ID与币种名称信息
        /// </summary>
        public Dictionary<Guid, String> CurrencyList = new Dictionary<Guid, String>();
       /// <summary>
       /// 公司ID
       /// </summary>
        public Guid CompanyID
        {
            get;
            set;
        }
        public Guid CustomerID
        {
            get;
            set;
        }
        public PrepaymentList CurrentRow
        {
            get
            {
                return bsMain.Current as PrepaymentList;
            }
        }
        public List<PrepaymentList> SelectDataList
        {
            get
            {
                List<PrepaymentList> list=bsList.DataSource as List<PrepaymentList>;
                if (list == null)
                {
                    list = new List<PrepaymentList>();
                }
                return list;
            }
        }

        public List<PrepaymentList> MainDataList
        {
            get
            {
                List<PrepaymentList> list = bsMain.DataSource as List<PrepaymentList>;
                if (list == null)
                {
                    list = new List<PrepaymentList>();
                }
                return list;
            }
        }
        #endregion

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                BindData();
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitControls()
        {
            //方向
            cmbWay.Items.Add(new ImageComboBoxItem("", FeeWay.AR, 0));
            cmbWay.Items.Add(new ImageComboBoxItem("", FeeWay.AP, 1));

            cmbListWay.Items.Add(new ImageComboBoxItem("", FeeWay.AR, 0));
            cmbListWay.Items.Add(new ImageComboBoxItem("", FeeWay.AP, 1));
       
            //币种
            foreach (KeyValuePair<Guid, string> item in CurrencyList)
            {
                cmbCurrencyID.Properties.Items.Add(new ImageComboBoxItem(item.Value, item.Key));
                cmbListCurrencyID.Properties.Items.Add(new ImageComboBoxItem(item.Value, item.Key));
            }


        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
           List<PrepaymentList> list= FinanceService.GetPrepaymentList(CompanyID,CustomerID);
        

           if (SelectDataList.Count == 0)
           {
               bsList.DataSource = new List<PrepaymentList>();
               bsList.ResetBindings(false);
           }
           else
           {
               //更新已选择状态
               foreach (PrepaymentList item in SelectDataList)
               {
                   PrepaymentList findItem = list.Find(delegate(PrepaymentList data) { return data.ID == item.ID; });
                   if (findItem != null)
                   {
                       findItem.IsCheck = true;
                   }
               }
           }
           bsMain.DataSource = list;
           bsMain.ResetBindings(false);
        }


        #endregion
        
        #region 刷新
        private void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            BindData();
        }
        #endregion

        #region 列表事件
        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column == colIsCheck)
            {
                CurrentRow.IsCheck = !CurrentRow.IsCheck;
                bsMain.ResetBindings(false);

                BindSelectList(CurrentRow.IsCheck);
            }
        }
        /// <summary>
        /// 绑定已选择的列表
        /// </summary>
        private void BindSelectList(bool isCheck)
        {
            if (isCheck)
            {
               PrepaymentList item=FAMUtility.Clone<PrepaymentList>(CurrentRow);
               item.CheckAmount = CurrentRow.Amount - CurrentRow.PayAmount;
               SelectDataList.Add(item);
            }
            else
            {
                PrepaymentList item = SelectDataList.Find(delegate(PrepaymentList data) { return data.ID == CurrentRow.ID; });
                if (item != null)
                {
                    SelectDataList.Remove(item);
                }
            }
            bsList.ResetBindings(false);

            TotalInfo();
        }
        /// <summary>
        /// 合计信息
        /// </summary>
        private void TotalInfo()
        {
            txtAmount.Text = string.Empty;

            Dictionary<Guid, decimal> totalList = new Dictionary<Guid, decimal>();
            totalList = (from d in SelectDataList group d by d.CurrencyID into g select new { g.Key, TotalAcmount = g.Sum(p => p.Way == FeeWay.AR ? p.CheckAmount : -p.CheckAmount) }).ToDictionary(c => c.Key, c => c.TotalAcmount);

            foreach (KeyValuePair<Guid, decimal> item in totalList)
            {
                string currencyName = string.Empty;
                if (CurrencyList.Keys.Contains(item.Key))
                {
                    currencyName = CurrencyList[item.Key];
                }
                else
                {
                    currencyName = LocalData.IsEnglish ? "Unknown" : "未知";
                }
                txtAmount.Text += currencyName + ":" + item.Value.ToString("n") + " ";

            }
        }
        #endregion

        #region 金额发生改变时
        private void gvList_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column == colListAmount)
            {
                TotalInfo();
            }
        }
        #endregion

        #region 全选
        private void barAllSelect_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (PrepaymentList item in MainDataList)
            {
                 item.IsCheck = true;
                 PrepaymentList findItem = SelectDataList.Find(delegate(PrepaymentList data) { return data.ID == CurrentRow.ID; });
                 if (findItem == null)
                 {
                     item.CheckAmount = item.Amount - item.PayAmount;
                     SelectDataList.Add(item);
                 }
            }
            bsList.ResetBindings(false);
            bsMain.ResetBindings(false);

            TotalInfo();
        }
        #endregion

        #region 清空
        private void barClear_ItemClick(object sender, ItemClickEventArgs e)
        {
            bsList.DataSource = new List<PrepaymentList>();

            MainDataList.ForEach(o => o.IsCheck = false);
            bsMain.ResetBindings(false);
            TotalInfo();
        }
        #endregion

        #region 确定
        private void barOK_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }
            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            if(SelectDataList.Count==0)
            {
                XtraMessageBox.Show(LocalData.IsEnglish?"Not select data":"未选择任何数据");
                return false;
            }
            foreach (PrepaymentList item in SelectDataList)
            {
                if (item.Amount <(item.CheckAmount+ item.PayAmount))
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ? "Reversal (Receipts / prepaid)can not exceed the original amount" : "冲销(预收/预付)不能超过了原始金额");
                    return false; 
                }
            }

            return true;            
        }
        #endregion

    }
}
