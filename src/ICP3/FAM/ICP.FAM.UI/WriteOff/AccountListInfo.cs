using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Controls;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Service;
using ICP.FAM.ServiceInterface.CompositeObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FAM.UI.WriteOff
{
    /// <summary>
    /// 银行交易信息
    /// </summary>
    [ToolboxItem(false)]
    public partial class AccountListInfo : BaseListPart
    {
        #region 构造函数
        /// <summary>
        /// 银行交易信息
        /// </summary>
        public AccountListInfo()
        {
            InitializeComponent();
            Disposed += delegate
            {
                TotalAmountUnequal = null;
                BankAccountSelected = null;
                Selected = null;
                BankList = null;
                _currencyList = null;
                gcMain.DataSource = null;
                bsCurrencyAmountList.DataSource = null;
                bsCurrencyAmountList.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            cmbBankAccount.SmallImages = imgHighlight;
        } 
        #endregion

        #region 服务
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        private WorkItem Workitem
        {
            get;
            set;
        }
        /// <summary>
        /// 财务服务方法
        /// </summary>
        IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        /// <summary>
        /// 汇率服务
        /// </summary>
        RateHelper RateService
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }
        #endregion

        #region 成员
        
        /// <summary>
        /// 销账公司本位币ID
        /// </summary>
        public Guid StandardCurrencyID
        {
            get;
            set;
        }
        /// <summary>
        /// 公司
        /// </summary>
        public Guid CompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// 单币种时，获得币种ID
        /// </summary>
        public Guid CurrencyID
        {
            get
            {
                if (DataList == null || DataList.Count == 0)
                {
                    return Guid.Empty;
                }
                else
                {
                    return DataList[0].CurrencyID;
                }
            }
        }
        public DateTime CheckDate
        {
            get;
            set;
        }
        /// <summary>
        /// 核销币种类型
        /// </summary>
        public WriteOffType CurrencyType
        {
            get
            {
                try
                {

                    return (WriteOffType)radCurrencyType.SelectedIndex;
                }
                catch
                {
                    return WriteOffType.Single;
                }
            }
            set
            {
                if (!radCurrencyType.Properties.Enabled)
                {
                    return;
                }
                radCurrencyType.SelectedIndex = value.GetHashCode();
            }
        }
        /// <summary>
        /// 当前行数据
        /// </summary>
        public OperationCurrencyAmountList CurrentRow
        {
            get
            {
                return bsCurrencyAmountList.Current as OperationCurrencyAmountList;
            }
        }
        /// <summary>
        /// 银行流水
        /// </summary>
        public BankTransactionInfo BankTransaction { get; set; }
        #region 汇率信息
        private List<SolutionExchangeRateList> _RateList = new List<SolutionExchangeRateList>();
        /// <summary>
        /// 汇率信息
        /// </summary>
        public List<SolutionExchangeRateList> RateList
        {
            get
            {
                if (_RateList == null)
                    _RateList = new List<SolutionExchangeRateList>();
                return _RateList;
            }
            set
            {
                _RateList = value;
            }
        } 
        #endregion
        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsCurrencyAmountList.DataSource;
            }
            set
            {
                bsCurrencyAmountList.DataSource = value;
            }
        }
        /// <summary>
        /// 数据源
        /// </summary>
        public List<OperationCurrencyAmountList> DataList
        {
            get
            {
                List<OperationCurrencyAmountList> list = bsCurrencyAmountList.DataSource as List<OperationCurrencyAmountList>;
                if (list == null)
                {
                    list = new List<OperationCurrencyAmountList>();
                }
                return list;
            }
        }
        private bool isCharge = false;
        /// <summary>
        /// 是否有数据发生改变
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (isCharge)
                {
                    return true;
                }
                foreach (OperationCurrencyAmountList item in DataList)
                {
                    if (item.IsDirty)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        /// <summary>
        /// 银行列表
        /// </summary>
        public List<BankAccountList> BankList { get; private set; }
        /// <summary>
        /// 选择
        /// </summary>
        public override event SelectedHandler Selected;
        /// <summary>
        /// 银行发生改变时
        /// </summary>
        public event SelectedHandler BankAccountSelected;
        /// <summary>
        /// 金额不平时触发
        /// </summary>
        public event SelectedHandler TotalAmountUnequal;
        /// <summary>
        /// 解决方案下币种列表
        /// </summary>
        List<SolutionCurrencyList> _currencyList = new List<SolutionCurrencyList>();
        #endregion

        #region 事件
        /// <summary>
        /// 重写加载
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessage();
            }
        }
        /// <summary>
        /// 币种发生改变(单/多币种)
        /// </summary>
        private void radCurrencyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Selected != null)
            {
                Selected(this, CurrencyType);
            }
            BankAccountIDCharge();
            isCharge = true;
            if (CurrencyType == WriteOffType.Single)
            {
                LoadAllBankComBox();
            }
        }
        /// <summary>
        /// 列值发生改变时
        /// </summary>
        private void gvMain_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            #region 银行改变
            if (e.Column == colBankAccount)
            {
                if (e.Value == null)
                {
                    return;
                }
                Guid bankAccountID = new Guid(e.Value.ToString());
                if (CurrentRow == null)
                {
                    return;
                }
                BankAccountInfo accountInfo = FinanceService.GetBankAccountInfo(bankAccountID, LocalData.IsEnglish);
                if (accountInfo != null)
                {
                    CurrentRow.CurrencyID = accountInfo.CurrencyID;
                    CurrentRow.IsSupportDirectBank = accountInfo.IsSupportDirectBank;
                    //银行发生改变事件
                    BankAccountIDCharge();
                }
            } 
            #endregion
        }
        /// <summary>
        /// 列值改变后
        /// </summary>
        private void gvMain_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            #region 金额变更
            if (e.Column == colAmount)
            {
                if (CurrentRow == null)
                {
                    return;
                }
                decimal ExchangeRate = RateService.GetRate(CurrentRow.CurrencyID, StandardCurrencyID, DateTime.SpecifyKind(CheckDate, DateTimeKind.Unspecified), RateList);
                CurrentRow.StandardCurrencyAmount = CurrentRow.TotalAmount * ExchangeRate;//金额改变时，更新本位币金额

                bsCurrencyAmountList.ResetCurrentItem();

                if (DataList.Count == 1 &&//单币种模式下才会有汇总损益
                    !FAMUtility.GuidIsNullOrEmpty(DataList[0].BankAccountID) &&//银行不为空
                    (decimal.Round(CurrentRow.StandardCurrencyAmount, 2, MidpointRounding.AwayFromZero) != decimal.Round(CurrentRow.StandardCurrencyBillAmount + CurrentRow.StandardCurrencyOtherAmount, 2, MidpointRounding.AwayFromZero)) &&//总金额不等于账单金额+其它
                    TotalAmountUnequal != null)//事件不为空
                {
                    TotalAmountUnequal(null, null);
                }
            } 
            #endregion
        }
        /// <summary>
        /// 网格行点击
        /// </summary>
        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            #region 到账
            if (e.Column == colReached)
            {
                OperationCurrencyAmountList list = gvMain.GetRow(e.RowHandle) as OperationCurrencyAmountList;

                if (list != null)
                {
                    if (list.IsReached)
                    {
                        list.IsReached = false;
                        list.BankDate = null;
                        list.BankByID = null;
                    }
                    else
                    {
                        list.IsReached = true;
                        list.BankDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                        list.BankByID = LocalData.UserInfo.LoginID;
                    }

                    bsCurrencyAmountList.ResetBindings(false);
                }
            } 
            #endregion
        }
        /// <summary>
        /// 显示编辑
        /// </summary>
        private void gvMain_ShowingEditor(object sender, CancelEventArgs e)
        {

            if (gvMain.FocusedColumn == colBankAccount && CurrencyType == WriteOffType.Muitl && BankList != null)
            {
                if (cmbBankAccount.Items.Count > 3)
                {
                    cmbBankAccount.Items[3].ImageIndex = 0;
                }
                Guid currencyID = ((OperationCurrencyAmountList)bsCurrencyAmountList.Current).CurrencyID;
                for (int i = 0; i < cmbBankAccount.Items.Count; i++)
                {
                    var sub = cmbBankAccount.Items[i];
                    if (BankList[i].CurrencyID == currencyID)
                        sub.ImageIndex = 0;
                    else
                        sub.ImageIndex = -1;
                }
            }
            e.Cancel = false;
        }
        #endregion

        #region 方法
        #region 初始化
        /// <summary>
        /// 设置WorkItem服务
        /// </summary>
        /// <param name="workitem"></param>
        public void SetService(WorkItem workitem)
        {
            Workitem = workitem;
        }
        /// <summary>
        /// 禁止切换单/多币种
        /// </summary>
        public void SetradCurrencyTypeEnable()
        {
            radCurrencyType.Properties.ReadOnly = true;
        }
        /// <summary>
        /// 设置币种下拉框
        /// </summary>
        /// <param name="list">币种列表</param>
        public void SetCurrencyComBox(List<SolutionCurrencyList> list)
        {
            cmbCurrency.Items.Clear();
            cmbCurrency.Items.Clear();
            foreach (SolutionCurrencyList item in list)
            {
                cmbCurrency.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.CurrencyID));
            }
        }
        /// <summary>
        /// 设置银行下拉数据源
        /// </summary>
        /// <param name="companyID"></param>
        public void SetBankComBox(Guid companyID)
        {
            cmbBankAccount.Items.Clear();
            List<BankAccountList> BankAccountList = FinanceService.GetCompanyBankAccounts(companyID, LocalData.IsEnglish).OrderBy(i => i.CurrencyName).ToList();
            if(BankTransaction!=null)
                BankAccountList = BankAccountList.Where(fItem => fItem.IsSupportDirectBank && fItem.AccountNo == BankTransaction.AccountNO).ToList();
            BankList = BankAccountList;
            //特殊处理
            //当打开旧销账单时，如果当时选择的银行，现在已不在银行下拉列表中（通常原因有两个：1. 该银行已作废。2.该银行已移到另一个分公司。）
            //在这个情况下，应该允许并保证当时选择的银行，仍然能在销账单中显示，而不是消失。
            List<BankAccountList> foundBankAccount;
            List<OperationCurrencyAmountList> list = new List<OperationCurrencyAmountList>();
            if (bsCurrencyAmountList.DataSource != null)
            {
                list = (List<OperationCurrencyAmountList>)bsCurrencyAmountList.DataSource;
            }
            foreach (var sub in list)
            {
                foundBankAccount = (from d in BankList where d.ID == sub.BankAccountID select d).ToList();
                if (foundBankAccount == null || foundBankAccount.Count == 0)
                {
                    BankList.Add(new BankAccountList()
                    {
                        ID = sub.BankAccountID,
                        CurrencyName = sub.BankName,
                        CurrencyID = sub.CurrencyID,
                    });
                }
            }

            if (BankList != null)
            {
                cmbBankAccount.Items.Clear();

                foreach (BankAccountList item in BankList)
                {
                    cmbBankAccount.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.ID));
                }
            }

        }
        /// <summary>
        /// 清空选择的银行
        /// </summary>
        public void ResetbAankAccountID()
        {
            List<OperationCurrencyAmountList> list = new List<OperationCurrencyAmountList>();
            if (bsCurrencyAmountList != null && bsCurrencyAmountList.List != null && bsCurrencyAmountList.List.Count > 0)
            {
                list = (List<OperationCurrencyAmountList>)bsCurrencyAmountList.List;
            }
            foreach (var sub in list)
            {
                sub.BankAccountID = Guid.Empty;
            }
        }
        
        /// <summary>
        /// 初始化控件
        /// </summary>
        public void InitControls()
        {
            if (bsCurrencyAmountList.DataSource == null)
            {
                bsCurrencyAmountList.DataSource = new List<OperationCurrencyAmountList>();
            }
            SetBankComBox(CompanyID);
        }
        /// <summary>
        /// 初始化消息
        /// </summary>
        public void InitMessage()
        {
            RegisterMessage("1109290001", LocalData.IsEnglish ? "The currency writeoff bill already Account, unable to delete" : "该币种的销账单已经到账,无法删除");
        }
        /// <summary>
        /// 设置币种信息
        /// </summary>
        /// <param name="currencyIDList"></param>
        public void SetCurrenyList(List<CurrentAndAmountTotalInfo> list, bool updateTotalAmount)
        {
            if (list == null || list.Count == 0)
            {
                bsCurrencyAmountList.DataSource = new List<OperationCurrencyAmountList>();
            }
            else
            {
                foreach (CurrentAndAmountTotalInfo totalInfo in list)
                {
                    if (totalInfo.CurrentID == Guid.Empty)
                    {
                        continue;
                    }

                    OperationCurrencyAmountList currencyAmount = (from d in DataList where d.CurrencyID == totalInfo.CurrentID select d).Take(1).SingleOrDefault();
                    if (currencyAmount == null)
                    {
                        //不存在该币种的，则新增
                        currencyAmount = new OperationCurrencyAmountList();
                        currencyAmount.SourceCurrencyID = totalInfo.CurrentID;
                        currencyAmount.CurrencyID = totalInfo.CurrentID;
                        //账单金额
                        currencyAmount.TotalBillAmount = totalInfo.TotalBillAmount;
                        //账单金额(本位币)
                        currencyAmount.StandardCurrencyBillAmount = totalInfo.TotalBillAmountToStandardCurrency;
                        //其它项目
                        currencyAmount.TotalOtherAmount = totalInfo.TotalOtherAmount;
                        //其它项目(本位币)
                        currencyAmount.StandardCurrencyOtherAmount = totalInfo.TotalOtherAmountToStandardCurrency;

                        //销账金额
                        currencyAmount.TotalAmount = totalInfo.TotalBillAmount + totalInfo.TotalOtherAmount;
                        //销账金额(本位币) 
                        decimal rate = RateService.GetRate(currencyAmount.CurrencyID, StandardCurrencyID, DateTime.SpecifyKind(CheckDate, DateTimeKind.Unspecified), RateList);
                        currencyAmount.StandardCurrencyAmount = decimal.Round(currencyAmount.TotalAmount * rate, 2, MidpointRounding.AwayFromZero);
                        bsCurrencyAmountList.Add(currencyAmount);

                    }
                    else
                    {
                        //已存在的，更新金额信息

                        //账单金额
                        currencyAmount.TotalBillAmount = totalInfo.TotalBillAmount;
                        //账单金额(本位币)
                        currencyAmount.StandardCurrencyBillAmount = totalInfo.TotalBillAmountToStandardCurrency;
                        //其它项目
                        currencyAmount.TotalOtherAmount = totalInfo.TotalOtherAmount;
                        //其它项目(本位币)
                        currencyAmount.StandardCurrencyOtherAmount = totalInfo.TotalOtherAmountToStandardCurrency;

                        if (updateTotalAmount)
                        {
                            //销账金额
                            currencyAmount.TotalAmount = totalInfo.TotalBillAmount + totalInfo.TotalOtherAmount;
                            //销账金额(本位币) 
                            decimal rate = RateService.GetRate(currencyAmount.CurrencyID, StandardCurrencyID, DateTime.SpecifyKind(CheckDate, DateTimeKind.Unspecified), RateList);
                            currencyAmount.StandardCurrencyAmount = decimal.Round(currencyAmount.TotalAmount * rate, 2, MidpointRounding.AwayFromZero);
                        }
                    }
                    if (BankTransaction != null)
                    {
                        currencyAmount.IsReached = true;
                        currencyAmount.BankDate = BankTransaction.OperationDateTime;
                        currencyAmount.BankByID = LocalData.UserInfo.LoginID;
                    }
                }

                if (DataList.Count > list.Count)
                {
                    foreach (var item in DataList)
                    {
                        CurrentAndAmountTotalInfo currencyItem = (from d in list where d.CurrentID == item.SourceCurrencyID select d).Take(1).SingleOrDefault();
                        if (currencyItem == null)
                        {
                            bsCurrencyAmountList.Remove(item);
                            break;
                        }
                    }
                }
            }

            isCharge = true;
            bsCurrencyAmountList.ResetBindings(false);
        }
        /// <summary>
        /// 单币种核销时加载所有银行
        /// </summary>
        void LoadAllBankComBox()
        {
            if (BankList != null)
            {
                cmbBankAccount.Items.Clear();

                foreach (BankAccountList item in BankList)
                {
                    cmbBankAccount.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.ID));
                }
            }
        }
        #endregion
        #region 外部调用
        /// <summary>
        /// 刷新保存
        /// </summary>
        public void AfterSave()
        {
            isCharge = false;

            foreach (OperationCurrencyAmountList item in DataList)
            {
                item.IsDirty = false;
            }
        }
        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        public void ValidateData()
        {
            bsCurrencyAmountList.EndEdit();
        }
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <returns></returns>
        public List<CheckAmount> GetCurrencySaveRequest()
        {
            List<CheckAmount> list = new List<CheckAmount>();

            foreach (OperationCurrencyAmountList item in DataList)
            {
                CheckAmount saveRequest = new CheckAmount();

                saveRequest.ID = item.ID;
                saveRequest.CurrencyID = item.CurrencyID;
                saveRequest.Amount = item.TotalAmount;
                saveRequest.StandardCurrencyAmount = item.StandardCurrencyAmount;
                saveRequest.BankAccountID = item.BankAccountID;
                saveRequest.BankBy = item.BankByID;
                saveRequest.BankDate = item.BankDate.HasValue ? DateTime.SpecifyKind(item.BankDate.Value, DateTimeKind.Unspecified) : item.BankDate;
                saveRequest.BillAmount = item.TotalBillAmount;
                saveRequest.StandardCurrencyBillAmount = item.StandardCurrencyBillAmount;
                saveRequest.ExpensesAmount = item.TotalOtherAmount;
                saveRequest.StandardCurrencyOtherAmount = item.StandardCurrencyOtherAmount;
                saveRequest.VoucherSeqNo = item.VoucherSeqNo;
                saveRequest.BankTransactionID = item.BankTransactionID;
                saveRequest.BankTransactionNO = item.BankTransactionNO;
                saveRequest.AssociationType = item.AssociationType;
                saveRequest.UpdateDate = item.UpdateDate == null ? string.Empty : item.UpdateDate.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
                saveRequest.AddInvolvedObject(item);

                list.Add(saveRequest);
            }

            return list;
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefreshUI(List<CheckAmount> saveRequestList, ManyResult currencyMany)
        {
            for (int n = 0; n < saveRequestList.Count; n++)
            {
                CheckAmount expenseRequest = saveRequestList[n];
                List<OperationCurrencyAmountList> currencys = expenseRequest.UnBoxInvolvedObject<OperationCurrencyAmountList>();
                for (int i = 0; i < currencys.Count; i++)
                {
                    currencys[i].ID = currencyMany.Items[n].GetValue<Guid>("ID");
                    currencys[i].UpdateDate = currencyMany.Items[n].GetValue<DateTime?>("UpdateDate");
                    currencys[i].IsDirty = false;
                }
            }
        }
        /// <summary>
        /// 刷新UI
        /// </summary>
        public void RefreshUI()
        {
            bsCurrencyAmountList.ResetBindings(false);
        }

        
        #endregion

        /// <summary>
        /// 银行改变时，验证是否需要触发事件 
        /// </summary>
        private void BankAccountIDCharge()
        {
            if (CurrencyType == WriteOffType.Single && CurrentRow != null && !FAMUtility.GuidIsNullOrEmpty(CurrentRow.CurrencyID))
            {
                if (BankAccountSelected != null)
                {
                    BankAccountSelected(null, CurrentRow.CurrencyID);
                }
            }
        }
        /// <summary>
        /// 设置币种是否可见
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetCurrency(bool isVisible)
        {
            colCurrency.Visible = isVisible;
        }
        #endregion
    }
}
