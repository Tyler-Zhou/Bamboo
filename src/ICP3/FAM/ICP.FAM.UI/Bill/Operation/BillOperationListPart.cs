
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.Controls;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraGrid;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.WF.ServiceInterface;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.Client;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FAM.UI.Bill
{
    [ToolboxItem(false)]
    public partial class BillOperationListPart : BaseListPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }
        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }
        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }

        IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        /// <summary>
        /// 工作流接口服务
        /// </summary>
        public IWorkflowClientService WorkflowClientService
        {
            get
            {
                return ServiceClient.GetClientService<IWorkflowClientService>();
            }
        }

        /// <summary>
        /// 客户管理服务接口
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        /// <summary>
        /// 工作流拓展服务
        /// </summary>
        public IWorkFlowExtendService WorkFlowExtendService
        {
            get
            {
                return ServiceClient.GetService<IWorkFlowExtendService>();
            }
        }

        #endregion

        #region 属性
        List<SolutionExchangeRateList> RateList = new List<SolutionExchangeRateList>();
        /// <summary>
        /// 列表中是否包含多币种
        /// </summary>
        public bool isMultiCurrency
        {
            get
            {
                if (DataSourceList == null || DataSourceList.Count == 0)
                {
                    return false;
                }
                else
                {
                    int i = (from d in DataSourceList group d by d.CurrencyID into g select new { g.Key }).Count();
                    if (i > 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 当前列表数据源
        /// </summary>
        public List<CurrencyBillList> CurrentList
        {
            get
            {
                return bsList.DataSource as List<CurrencyBillList>;
                //int[] rowIndexs = gvMain.GetSelectedRows();

                //if (rowIndexs.Length == 0) return null;

                //List<CurrencyBillList> tagers = new List<CurrencyBillList>();
                //foreach (var item in rowIndexs)
                //{
                //    CurrencyBillList dr = gvMain.GetRow(item) as CurrencyBillList;
                //    if (dr != null) tagers.Add(dr);

                //}
                //return tagers;
            }
        }

        /// <summary>
        /// 币种名称
        /// </summary>
        public string currencyName
        {
            get;
            set;
        }

        /// <summary>
        /// 人民币的币种ID
        /// </summary>
        public Guid CurrentRMBID
        {
            get;
            set;
        }

        /// <summary>
        /// 方案
        /// </summary>
        public BillProgram Billprogram
        {
            get;
            set;
        }
        /// <summary>
        /// 银行流水
        /// </summary>
        BankTransactionInfo BankTransaction { get; set; }

        #endregion

        #region 初始化
        public BillOperationListPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                RateList = null;
                selectIdList = null;
                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList.Dispose();
                Selected = null;
                CurrentChanged = null;
                cmbCurrency.SelectedIndexChanged -= cmbCurrency_SelectedIndexChanged;
                gvMain.RowCellClick -= gvMain_RowCellClick;
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
                InitMessage();
                InitControls();
                BulidRowCellStyle();
            }
        }
        /// <summary>
        /// 初始化消息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("1108020001", LocalData.IsEnglish ? "Only with a customer 's bill reconciliation" : "只能销账同一个客户的账单!");
            RegisterMessage("1108020002", LocalData.IsEnglish ? "Some bills are paid which among the selected bills. " : "选择的账单中包含已销账的账单!");

            RegisterMessage("1108260001", LocalData.IsEnglish ? "All selected bills should be the same customer when apply Pay Commission." : "发起业务管理成本只能针对一个客户的账单!");
            RegisterMessage("1108260002", LocalData.IsEnglish ? "All selected bills should be approved when apply Pay Commission." : "发起业务管理成本必须是已审核的账单!");
            RegisterMessage("1108260003", LocalData.IsEnglish ? "All selected bills should be the same currency when apply Pay Commissio" : "发起业务管理成本只能针对同一币种的账单!");
            RegisterMessage("1108260004", LocalData.IsEnglish ? "All selected bills should be type Commission when apply Pay Commission" : "发起业务管理成本只能针对业务管理成本的账单!");

            RegisterMessage("1109050001", LocalData.IsEnglish ? "The bill is already paid." : "该账单已经销账!");
            RegisterMessage("1109050002", LocalData.IsEnglish ? "It contains approved bills." : "列表中包含已审核的账单!");
            RegisterMessage("1109050003", LocalData.IsEnglish ? "It contains un-approved bills." : "列表中包含未审核的账单!");

            RegisterMessage("1109050004", LocalData.IsEnglish ? "Comfirm Approval?" : "确认审核?");
            RegisterMessage("1109050005", LocalData.IsEnglish ? "Comfirm cancel?" : "确认取消?");

            RegisterMessage("1109050006", LocalData.IsEnglish ? "Could not un-approve the bill because it's {0}. For adjusting the bill is to add a new bill. " : "不能取消审核，因为账单的状态为{0}。如有更改的必要，请通过新增账单进行更正!");

            RegisterMessage("1109060001", LocalData.IsEnglish ? "Only one Credit Bill could be selected." : "只能选择一个应付账单!");
            RegisterMessage("1109060002", LocalData.IsEnglish ? "Only Credit Bills could be selected." : "只能选择应付账单!");

            RegisterMessage("1109060003", LocalData.IsEnglish ? "Could not found the exchange rate matchs {RMB} and {0}" : "没有找到{ RMB }与{0}之间的汇率");
            RegisterMessage("1109060004", LocalData.IsEnglish ? "The exchange rate of {RMB} is not found." : "没有找到{ RMB }的币种信息");

            RegisterMessage("1109060005", LocalData.IsEnglish ? "The selected bill is defined with that the bill will be paid with one currency. example: The bill {0} contains {1}. Are you sure to continue to pay the bill?" : "选择的账单中包含了按一种币种支付的定义,如:账单号:{0}中定义了{1},是否继续销账?");
            RegisterMessage("1109060007", LocalData.IsEnglish ? "No bill can initiate processes." : "没有可以发起流程的账单!");

            RegisterMessage("1303210001", LocalData.IsEnglish ? "The invoice of the [{0}] bill has been made. Clicks OK to select it. Clicks Cancel to give up." : "账单号[{0}]已开过发票,是否继续再发开票?");


        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {

            List<CurrencyBillList> source = new List<CurrencyBillList>();
            bsList.DataSource = source;


            FAMUtility.ShowGridRowNo(gvMain);

            //InitCurrencyInfo();
        }

        private void InitBillState()
        {
            if (isInitBillState)
            {
                return;
            }
            //帐单类型
            List<EnumHelper.ListItem<BillState>> currencyBillStates
                = EnumHelper.GetEnumValues<BillState>(LocalData.IsEnglish);
            rcmbState.BeginUpdate();
            foreach (var item in currencyBillStates)
            {
                rcmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            rcmbState.EndUpdate();
            isInitBillState = true;
        }
        private bool isInitBillState = false;
        /// <summary>
        /// 初始化币种信息
        /// </summary>
        private void InitCurrencyInfo(object value)
        {
            if (isInitCurrency)
            {
                return;
            }

            List<CurrencyBillList> list = value as List<CurrencyBillList>;
            Guid companyID;
            if (list == null && list.Count > 0)
            {
                companyID = LocalData.UserInfo.DefaultCompanyID;
            }
            else
                companyID = list[0].CompanyID == Guid.Empty ? LocalData.UserInfo.DefaultCompanyID : list[0].CompanyID;

            List<SolutionCurrencyList> _currencyList = new List<SolutionCurrencyList>();
            RateList = ConfigureService.GetCompanyExchangeRateList(companyID, true);

            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(companyID);
            //找到解决方案
            if (configureInfo != null)
            {
                _currencyList = ConfigureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);
            }
            else
            {
                return;
            }

            //填充下拉框与币种信息
            foreach (SolutionCurrencyList currency in _currencyList)
            {
                cmbCurrency.Properties.Items.Add(new ImageComboBoxItem(currency.CurrencyName, currency.CurrencyID));
            }

            if (configureInfo != null)
            {
                cmbCurrency.EditValue = configureInfo.DefaultCurrencyID;

                currencyName = configureInfo.DefaultCurrency;
            }

            List<CurrencyList> currentList = ConfigureService.GetCurrencyList("RMB", "RMB", null, true, 0);
            if (currentList != null && currentList.Count > 0)
            {
                CurrentRMBID = currentList[0].ID;
            }


        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                switch (item.Key.ToUpper())
                {
                    case "BANKTRANSACTION":
                        #region 银行流水
                        BankTransaction = item.Value as BankTransactionInfo;
                        #endregion
                        break;
                }
            }
        }
        #endregion

        #region 本地变量
        List<Guid> selectIdList = new List<Guid>();
        bool isInitCurrency = false;
        #endregion

        #region Workitem Common

        #region 移除与清空
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BillCommandConstants.Command_Remove)]
        public void Command_Remove(object sender, EventArgs e)
        {
            CurrencyBillList item = CurrentRow;

            if (DataSourceList.Contains(item))
            {
                DataSourceList.Remove(item);
                DataSource = DataSourceList;
                bsList.ResetBindings(false);

                if (Selected != null)
                {
                    Selected("Remove", item.CurrentID);
                }
            }
        }

        /// <summary>
        /// 清空(搜索器调用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BillCommandConstants.Command_FinderClearAll)]
        public void Command_FinderClearAll(object sender, EventArgs e)
        {
            string message = LocalData.IsEnglish ? "Sure Clare Data" : "确认清空数据";
            if (!FAMUtility.ShowResultMessage(message))
            {
                return;
            }

            ClareList();
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BillCommandConstants.Command_Clear)]
        public void Command_Clear(object sender, EventArgs e)
        {
            string message = LocalData.IsEnglish ? "Sure Clare Data" : "确认清空数据";
            if (!FAMUtility.ShowResultMessage(message))
            {
                return;
            }

            ClareList();

        }
        /// <summary>
        /// 清空列表
        /// </summary>
        private void ClareList()
        {
            DataSourceList.Clear();
            DataSource = DataSourceList;
            bsList.ResetBindings(false);

            if (Selected != null)
            {
                Selected("Clear", null);
            }
        }

        #endregion

        #region 销账

        #region 单币种销账
        /// <summary>
        /// 单币种销账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BillCommandConstants.Command_WriteOff)]
        public void Command_WriteOff(object sender, EventArgs e)
        {
            if (!isWriteOff())
            {
                return;
            }

            Dictionary<string, object> dicList = new Dictionary<string, object>();
            List<Guid> currencyIDList = new List<Guid>();
            Guid billID = Guid.Empty;

            if (CurrentRow != null)
            {
                currencyIDList.Add(CurrentRow.CurrencyID);

                dicList.Add("CustomerID", CurrentRow.CustomerID);
                dicList.Add("CustomerName", CurrentRow.CustomerName);
                dicList.Add("CompanyID", CurrentRow.CompanyID);
                dicList.Add("CompanyName", CurrentRow.CompanyName);
                dicList.Add("CurrencyIDList", currencyIDList);

                billID = CurrentRow.ID;
            }
            else
            {
                currencyIDList.Add(DataSourceList[0].CurrencyID);

                dicList.Add("CustomerID", DataSourceList[0].CustomerID);
                dicList.Add("CustomerName", DataSourceList[0].CustomerName);
                dicList.Add("CompanyID", DataSourceList[0].CompanyID);
                dicList.Add("CompanyName", DataSourceList[0].CompanyName);
                dicList.Add("CurrencyIDList", currencyIDList);
                billID = DataSourceList[0].ID;
            }

            dicList.Add("PayCurrencyID", Guid.Empty);
            #region 是否都定义按同一币种支付的
            //先判断列表中有没有空的，没有空的，才继续执行
            int i = (from d in DataSourceList where d.PayCurrencyID == null || d.PayCurrencyID == Guid.Empty select d).Count();
            if (i == 0)
            {
                //判断是不是都是同一种支付方式的
                int n = (from d in DataSourceList group d by d.PayCurrencyID into g select g.Key).Count();
                if (n == 1)
                {
                    currencyIDList = new List<Guid>();

                    currencyIDList.Add(DataSourceList[0].PayCurrencyID.Value);

                    dicList["PayCurrencyID"] = DataSourceList[0].PayCurrencyID.Value;

                    dicList["CurrencyIDList"] = currencyIDList;

                    List<CurrencyRateData> currencyRateList = new List<CurrencyRateData>();

                    BillInfo billInfo = FinanceService.GetBillInfo(billID);
                    if (billInfo != null)
                    {
                        currencyRateList = billInfo.CurrencyRates;
                        dicList.Add("CurrencyRateList", currencyRateList);
                    }
                }
            }

            #endregion


            dicList.Add("WriteOffType", WriteOffType.Single);
            dicList.Add("BillList", DataSourceList);
            if(BankTransaction!=null)
            {
                dicList.Add("BankTransactionInfo", BankTransaction);
            }

            string title = string.Empty;

            decimal totalAmount = DataSourceList.Select(o =>
                (o.Way == FeeWay.AR ? Math.Abs(o.WriteOffAmount) : -Math.Abs(o.WriteOffAmount)) -
                (o.Way == FeeWay.AR ? Math.Abs(o.Amount) : -Math.Abs(o.Amount))).Sum();

            if (totalAmount < 0)
            {
                dicList.Add("FeeWay", FeeWay.AR);
                title = LocalData.IsEnglish ? "Collection" : "收款";
            }
            else
            {
                dicList.Add("FeeWay", FeeWay.AP);
                title = LocalData.IsEnglish ? "Payment" : "付款";
            }

            FinanceClientService.ShowWriteOffEditor(title, dicList, ClientConstants.MainWorkspace);
            ClareList();
        }

        #endregion

        #region 多币种销账

        /// <summary>
        /// 多币种销账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BillCommandConstants.Command_MultiCurrencyWriteOff)]
        public void Command_MultiCurrencyWriteOff(object sender, EventArgs e)
        {
            if (!isWriteOff())
            {
                return;
            }

            #region 是否有按同一币种付款的
            string payCurrencyName = string.Empty;
            string billNo = string.Empty;

            foreach (CurrencyBillList item in DataSourceList)
            {
                if (!string.IsNullOrEmpty(item.PayCurrencyName))
                {
                    payCurrencyName = item.PayCurrencyName;
                    billNo = item.BillNO;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(payCurrencyName) && !string.IsNullOrEmpty(billNo))
            {
                string message = NativeLanguageService.GetText(this, "1109060005");
                message = string.Format(message, billNo, payCurrencyName);

                if (!FAMUtility.ShowResultMessage(message))
                {
                    return;
                }

            }
            #endregion

            string title = string.Empty;

            Dictionary<string, object> dicList = new Dictionary<string, object>();
            List<Guid> currencyIDList = (from d in DataSourceList group d by d.CurrencyID into g select g.Key).ToList();

            if (currencyIDList == null)
            {
                currencyIDList = new List<Guid>();
            }

            if (CurrentRow != null)
            {
                dicList.Add("CustomerID", CurrentRow.CustomerID);
                dicList.Add("CustomerName", CurrentRow.CustomerName);
                dicList.Add("CurrencyIDList", currencyIDList);
            }
            else
            {
                dicList.Add("CustomerID", DataSourceList[0].CustomerID);
                dicList.Add("CustomerName", DataSourceList[0].CustomerName);
                dicList.Add("CurrencyIDList", currencyIDList);
            }

            dicList.Add("WriteOffType", WriteOffType.Muitl);
            dicList.Add("BillList", DataSourceList);
            dicList.Add("CompanyID", DataSourceList[0].CompanyID);
            dicList.Add("CompanyName", DataSourceList[0].CompanyName);

            int wayCount = (from d in DataSourceList group d by d.Way into g select g.Key).Count();

            if (wayCount == 1)
            {
                //选取的账单中，只包含了一个付款方向，则默认付款方向
                if (DataSourceList[0].Way == FeeWay.AR)
                {
                    dicList.Add("FeeWay", FeeWay.AR);
                    title = LocalData.IsEnglish ? "Collection" : "收款";
                }
                else
                {
                    dicList.Add("FeeWay", FeeWay.AP);
                    title = LocalData.IsEnglish ? "Payment" : "付款";
                }
            }
            else
            {
                decimal totalAmount = 0m;
                Guid targetCurrencyID = DataSourceList[0].CurrencyID;
                foreach (var bill in DataSourceList)
                {
                    decimal writeOffAmountByOneCurrency = RateHelper.GetAmountByRate(bill.WriteOffAmount, bill.CurrencyID, targetCurrencyID, RateList, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                    decimal amountByOneCurrency = RateHelper.GetAmountByRate(bill.Amount, bill.CurrencyID, targetCurrencyID, RateList, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                    totalAmount += (bill.Way == FeeWay.AR ? Math.Abs(writeOffAmountByOneCurrency) : -Math.Abs(writeOffAmountByOneCurrency)) - (bill.Way == FeeWay.AR ? Math.Abs(amountByOneCurrency) : -Math.Abs(amountByOneCurrency));
                }
                if (totalAmount < 0)
                {
                    dicList.Add("FeeWay", FeeWay.AR);
                    title = LocalData.IsEnglish ? "Collection" : "收款";
                }
                else
                {
                    dicList.Add("FeeWay", FeeWay.AP);
                    title = LocalData.IsEnglish ? "Payment" : "付款";
                }
            }
            //decimal totalAmount = DataSourceList.Select(o => o.Amount).Sum(); 
            //if (totalAmount > 0)
            //{
            //    dicList.Add("FeeWay", ICP.FAM.ServiceInterface.DataObjects.FeeWay.AR);
            //    title = LocalData.IsEnglish ? "Collection" : "收款";
            //}
            //else
            //{
            //    dicList.Add("FeeWay", ICP.FAM.ServiceInterface.DataObjects.FeeWay.AP);
            //    title = LocalData.IsEnglish ? "Payment" : "付款";
            //}

            FinanceClientService.ShowWriteOffEditor(title, dicList, ClientConstants.MainWorkspace);

            //selectIdList = (from d in DataSourceList group d by d.ID into g select g.Key).ToList();

            ClareList();
        }
        /// <summary>
        /// 销账时，更新账单中的列表数据
        /// </summary>
        /// <param name="prams"></param>
        private void CheckPartSaved(object[] prams)
        {
            ListOperating("Check", selectIdList);
        }

        #endregion

        #region 验证销账
        /// <summary>
        /// 是否允许销账
        /// </summary>
        /// <returns></returns>
        private bool isWriteOff()
        {
            //空数据
            if (DataSourceList == null || DataSourceList.Count == 0)
            {
                return false;
            }

            List<Guid> differentCustomerIDList = new List<Guid>();
            foreach (var item in DataSourceList)
            {
                if (!differentCustomerIDList.Contains(item.CustomerID))
                {
                    differentCustomerIDList.Add(item.CustomerID);
                }
            }

            int count = 1;
            if (differentCustomerIDList.Count > 1)
            {
                List<Guid> customerIDList = FAMUtility.Clone<List<Guid>>(differentCustomerIDList);
                foreach (var item in differentCustomerIDList)
                {
                    List<Guid> combineCustomerIDList = CustomerService.GetCombineCustomerIDList(item);
                    if (combineCustomerIDList.Count > 1)
                    {
                        foreach (var customerID in differentCustomerIDList)
                        {
                            if (item != customerID && combineCustomerIDList.Contains(customerID))
                            {
                                customerIDList.Remove(customerID);
                            }
                        }
                    }
                }

                count = customerIDList.Count;
            }


            //不是同一个客户的
            //int i = (from d in DataSourceList group d by d.CustomerID into g select new { g.Key }).Count();
            //if (i > 1)
            if (count > 1)
            {
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1108020001"));
                return false;
            }
            ////包含已销账账单的
            //int n = (from d in DataSourceList where d.WriteOffAmount > 0 select d.ID).Count();
            //if (n > 0)
            //{
            //    Utility.ShowMessage(NativeLanguageService.GetText(this, "1108020002"));
            //    return false;
            //}

            return true;
        }
        #endregion

        #endregion

        #region 改变方案发生改变时，清空列表
        /// <summary>
        /// 改变方案发生改变时，清空列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BillCommandConstants.Command_ClareDetailByProgram)]
        public void Command_ClareDetailByProgram(object sender, EventArgs e)
        {
            DataSource = new List<CurrencyBillList>();
        }
        #endregion

        #region 发起业务管理成本
        /// <summary>
        /// 发起业务管理成本 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BillCommandConstants.Command_BusinessCost)]
        public void Command_BusinessCost(object sender, EventArgs e)
        {
            if (DataSourceList == null || DataSourceList.Count == 0)
            {
                return;
            }

            int i = (from d in DataSourceList group d by d.CustomerID into g select g.Key).Count();
            if (i > 1)
            {
                //不是同一客户的，不能同时发起业务成本管理
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1108260001"));
                return;
            }

            int n = (from d in DataSourceList where !d.Checked select d.ID).Count();
            if (n > 0)
            {
                //不是审核状态的，不能发起业务管理成本 
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1108260002"));
                return;
            }


            int m = (from d in DataSourceList group d by d.CurrencyID into g select g.Key).Count();
            if (m > 1)
            {
                //不是相同币种的，不能同时发起业务成本管理
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1108260003"));
                return;
            }

            int h = (from d in DataSourceList where !d.IsCommission select d.CurrentID).Count();
            if (h > 0)
            {
                //不是业务管理成本的账单，不能发起业务成本管理
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1108260004"));
                return;
            }

            List<Guid> operationIDList = (from d in DataSourceList group d by d.OperationID into g select g.Key).ToList();
            string operationIDs = string.Empty;
            foreach (Guid oid in operationIDList)
            {
                operationIDs = operationIDs + oid.ToString() + ",";
            }
            if (!string.IsNullOrEmpty(operationIDs))
            {
                operationIDs = operationIDs.Substring(0, operationIDs.Length - 1);
            }

            //已发起过的，不能再重复申请
            List<WFCommissionLogList> logList = WorkFlowExtendService.GetCommissionLogList(operationIDList.ToArray(), LocalData.IsEnglish);
            if (logList != null && logList.Count > 0)
            {
                string omessage = LocalData.IsEnglish ? "Select list of business, including the Commission record has been retired,Whether to continue?" : "选择的业务列表中,包含了已退佣的纪录,是否继续?";
                omessage = omessage + Environment.NewLine;
                foreach (WFCommissionLogList logitem in logList)
                {
                    string strInfo = logitem.OperactioNo + ":  ApplyName:[" + logitem.CreateName + "] ApplyDate:[" + logitem.CreateDate.ToShortDateString() + "] WorkFlowNo:[" + logitem.WorkFlowNo + "] ";
                    omessage = omessage + strInfo + Environment.NewLine;
                }

                DialogResult result = XtraMessageBox.Show(omessage,
                          LocalData.IsEnglish ? "Tip" : "提示",
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }


            Guid companyId = LocalData.UserInfo.DefaultCompanyID;

            if (string.IsNullOrEmpty(currencyName))
            {
                currencyName = "RMB";
            }
            decimal operateRate = (decimal)Workitem.State["BusinessCostRate"];

            ComissionData data = FinanceService.GetComissionDataByCodition(operationIDList, DataSourceList[0].CustomerID, operateRate, LocalData.IsEnglish);

            Guid userId = LocalData.UserInfo.LoginID;
            Guid deptId = DataSourceList[0].CompanyID;
            string workName = DataSourceList[0].CustomerName + (LocalData.IsEnglish ? "-Operation management fee" : "-业务管理成本申请流程");
            string formTitle = LocalData.IsEnglish ? "The operation management fee" : "业务管理成本申请";

            //确认工作名

            BillPaymentRequest confirmWorkName = Workitem.Items.AddNew<BillPaymentRequest>();
            confirmWorkName.billList = null;//通用
            confirmWorkName.useCommon = true;
            confirmWorkName.WorkName = workName;
            string title = LocalData.IsEnglish ? "Work Name" : "输入流程名称";
            DialogResult dr = PartLoader.ShowDialog(confirmWorkName, title);
            if (confirmWorkName.dialogResult != DialogResult.OK) { return; }
            workName = confirmWorkName.WorkName;
            Guid WorkFlowID = WorkflowClientService.StartReturnCommissionWorkFlow
                (userId,
                deptId,
                workName,
                operationIDs,
                formTitle,
                string.Empty,
                DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
                deptId,
                data.OperationNos,
                data.PaymentType,
                data.CommissionAmount,
                currencyName,
                DataSourceList[0].CustomerName,
                DataSourceList[0].CustomerID,
                data.BlNos,
                string.Empty,
                data.Profit,
                data.IsPaid,
                LocalData.UserInfo.LoginName,
                data.Debit,
                data.Credit,
                data.Remark,true);


        }


        #endregion

        #region 审核
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BillCommandConstants.Command_Auditor)]
        public void Command_Auditor(object sender, EventArgs e)
        {
            if (FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1109050004")))
            {
                Auditor(true);
            }
        }

        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BillCommandConstants.Command_UnAuditor)]
        public void Command_UnAuditor(object sender, EventArgs e)
        {
            if (DataSourceList.Count(r => r.InvoiceNo != "" && r.InvoiceNo != null) > 0)
            {
                if (FAMUtility.ShowResultMessage(LocalData.IsEnglish ? "Srue Void Current Data?" : "此账单已经生成发票，解锁后会导致发票金额和账单金额不一致，是否继续解锁?"))
                {
                    Auditor(false);
                }

            }
            else
            {
                if (FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1109050005")))
                {
                    Auditor(false);
                }
            }
        }

        /// <summary>
        /// 审核&反审账单
        /// </summary>
        /// <param name="isAuditor"></param>
        public void Auditor(bool isAuditor)
        {
            //列表中包含不同类型的
            int i = (from d in DataSourceList where d.Checked == isAuditor select d.CurrentID).Count();
            if (i > 0)
            {
                if (isAuditor)
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109050002"));
                }
                else
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109050003"));
                }
                return;
            }
            //取消审核时，如果不是已审核的状态，则无法取消
            if (!isAuditor)
            {
                List<CurrencyBillList> list = (from d in DataSourceList where d.State != BillState.Approved select d).ToList();

                if (list != null && list.Count > 0)
                {
                    string message = NativeLanguageService.GetText(this, "1109050006");
                    message = string.Format(message, list[0].State.ToString());
                    FAMUtility.ShowMessage(message);
                    return;
                }

            }


            try
            {
                List<Guid> idList = new List<Guid>();
                List<DateTime?> updateList = new List<DateTime?>();

                foreach (CurrencyBillList item in DataSourceList)
                {
                    if (!idList.Contains(item.ID))
                    {
                        idList.Add(item.ID);
                        updateList.Add(item.UpdateDate);
                    }
                }

                ManyResult result = FinanceService.AuditorBill(
                                 idList.ToArray(),
                                 isAuditor,
                                 LocalData.UserInfo.LoginID,
                                 updateList.ToArray(),
                                 LocalData.IsEnglish);

                if (ListOperating != null)
                {
                    if (isAuditor)
                    {
                        ListOperating("Auditor", result);
                    }
                    else
                    {
                        ListOperating("UnAuditor", result);
                    }
                }

                ClareList();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return;
            }
        }



        #endregion

        #region 付款申请
        /// <summary>
        /// 付款申请
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BillCommandConstants.Command_PaymentRequest)]
        public void Command_PaymentRequest(object sender, EventArgs e)
        {
            if (CurrentList == null || CurrentList.Count < 1)
            {
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109060007"));
                return;
            }
            Guid customerID = CurrentList[0].CustomerID;

            foreach (var item in CurrentList)
            {
                if (!item.CustomerID.Equals(customerID))
                {
                    string message = LocalData.IsEnglish ? "Customers must be the same！" : "客户必须相同！";
                    FAMUtility.ShowMessage(message);
                    return;
                }
            }

            int i = (from d in CurrentList where d.Way != FeeWay.AP select d.ID).Count();
            if (i > 0)
            {
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109060002"));
                return;
            }
            //没有汇率列表
            if (RateList == null || RateList.Count == 0)
            {
                string message = LocalData.IsEnglish ? "Have no rate at current company." : "找不到当前公司下的汇率.";
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);
                return;
            }
            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(CurrentRow.CompanyID);
            decimal rate;
            rate = RateHelper.GetRate(CurrentRow.CurrencyID, configureInfo.StandardCurrencyID, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified), RateList);
            decimal amountt = CurrentList.Sum(am => am.Amount);
            CurrentRow.Amount = amountt;

            //原币金额
            string original = string.Empty;
            string billNOs = string.Empty;
            string billRefNOs = string.Empty;

            CurrentList.ForEach(delegate(CurrencyBillList or)
          {
              original += or.CurrencyName + ":" + or.Amount + ",";
              if (!string.IsNullOrEmpty(billNOs))
              {
                  billNOs += or.BillNO + "/";
              }
              if (!string.IsNullOrEmpty(or.BillRefNO))
              {
                  billRefNOs += or.BillRefNO + "/";
              }
          });

            if (!string.IsNullOrEmpty(original))
            {
                original = original.Substring(0, original.Length - 1);
                CurrentRow.CurrencyName = original;
            }

            if (!string.IsNullOrEmpty(billNOs))
            {
                billNOs = billNOs.Substring(0, billNOs.Length - 1);
                CurrentRow.BillNO = billNOs;
            }

            if (!string.IsNullOrEmpty(billRefNOs))
            {
                billRefNOs = billRefNOs.Substring(0, billRefNOs.Length - 1);
                CurrentRow.BillRefNO = billRefNOs;
            }


            BillPaymentRequest billPay = Workitem.Items.AddNew<BillPaymentRequest>();
            billPay.billList = CurrentRow;
            billPay.Rate = rate;
            billPay.ConfigureInfo = configureInfo;
            string formTitle = LocalData.IsEnglish ? "Work Name" : "输入流程名称";
            PartLoader.ShowDialog(billPay, formTitle);

        }

        #endregion

        #region 开发票
        /// <summary>
        /// 开发票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BillCommandConstants.Command_AddInvoice)]
        public void Command_AddInvoice(object sender, EventArgs e)
        {
            if (DataSourceList == null && DataSourceList.Count == 0)
            {
                return;
            }
            if (CurrentRow == null)
            {
                return;
            }

            string message = string.Empty;
            List<string> billNoList = (from d in DataSourceList where !string.IsNullOrEmpty(d.InvoiceNo) select d.BillNO).ToList();
            if (billNoList != null && billNoList.Count > 0)
            {
                message = string.Format(NativeLanguageService.GetText(this, "1303210001"), billNoList[0]);
                if (!FAMUtility.ShowResultMessage(message))
                {
                    return;
                }

            }



            List<Guid> oid = new List<Guid>();
            List<Guid> bid = new List<Guid>();

            foreach (var item in DataSourceList)
            {
                oid.Add(item.OperationID);
                bid.Add(item.ID);
            }


            BusinessByInvoice business = FinanceService.GetBusinessByInvoice(oid.ToArray(), bid.ToArray(), LocalData.IsEnglish);
            if (business == null)
            {
                business = new BusinessByInvoice();
            }

            InvoiceEditPart invoicePart = Workitem.Items.AddNew<InvoiceEditPart>();

            InvoiceInfo newData = new InvoiceInfo();

            newData.CustomerID = business.CustomerID;
            newData.CustomerName = LocalData.IsEnglish ? business.CustomerEName : business.CustomerCName;
            newData.CompanyID = business.CompanyID;
            newData.CompanyName = business.CompanyName;

            List<CustomerInvoiceTitleInfo> invoiceTitleList = CustomerService.GetCustomerInvoiceTitleList(newData.CustomerID.ToGuid(), LocalData.UserInfo.DefaultCompanyID);
            if (invoiceTitleList != null && invoiceTitleList.Count > 0)
            {
                CustomerInvoiceTitleInfo last = (from d in invoiceTitleList orderby d.LastUseDate descending select d).Take(1).SingleOrDefault();
                if (last != null)
                {
                    string name = string.Empty, taxNo = string.Empty, addressTel = string.Empty, bankAccountNo = string.Empty;
                    #region 抬头
                    if (last.Name.Contains(Environment.NewLine))
                    {
                        string[] nameList = last.Name.Split(Environment.NewLine.ToCharArray());
                        if (nameList != null && nameList.Length > 0)
                        {
                            name = nameList[0];
                        }
                    }
                    else
                    {
                        name = last.Name;
                    }
                    #endregion

                    #region 税号
                    if (last.TaxNo.Contains(Environment.NewLine))
                    {
                        string[] taxNoList = last.TaxNo.Split(Environment.NewLine.ToCharArray());
                        if (taxNoList != null && taxNoList.Length > 0)
                        {
                            taxNo = taxNoList[0];
                        }
                    }
                    else
                    {
                        taxNo = last.TaxNo;
                    }
                    #endregion

                    #region 地址
                    if (last.AddressTel.Contains(Environment.NewLine))
                    {
                        string[] addressTelList = last.AddressTel.Split(Environment.NewLine.ToCharArray());
                        if (addressTelList != null && addressTelList.Length > 0)
                        {
                            addressTel = addressTelList[0];
                        }
                    }
                    else
                    {
                        addressTel = last.AddressTel;
                    }
                    #endregion

                    #region 银行帐号
                    if (last.BankAccountNo.Contains(Environment.NewLine))
                    {
                        string[] bankAccountNoList = last.BankAccountNo.Split(Environment.NewLine.ToCharArray());
                        if (bankAccountNoList != null && bankAccountNoList.Length > 0)
                        {
                            bankAccountNo = bankAccountNoList[0];
                        }
                    }
                    else
                    {
                        bankAccountNo = last.BankAccountNo;
                    }
                    #endregion

                    newData.TitleCName = name;
                    newData.CustomerTaxIDNo = taxNo;
                    newData.CustomerAddressTel = addressTel;
                    newData.CustomerBankAccountNo = bankAccountNo;
                    newData.InvoiceType = last.InvoiceType;
                }
            }
            ///这几个公司只开普票
            if (TasSystemCommon.GetOrdinaryInvoiceCompanyIDList.Contains(newData.CompanyID))
            {
                newData.InvoiceType = CustomerInvoiceType.Ordinary;
            }

            newData.PODName = business.PODName;
            newData.POLName = business.POLName;
            newData.TitleEName = business.CustomerEName;
            newData.SONo = business.SoNo;
            newData.BLNo = business.BLNo;
            if (business.ETD != null)
            {
                newData.ETD = business.ETD.Value;
            }
            newData.PlaceOfDeliveryName = business.PlaceOfDeliveryName;
            newData.Vessel = business.Vessel;
            newData.CtnTypeName = business.CtnTypeName;
            newData.ContainerNo = business.CtnNo;
            newData.Voyage = business.Voyage;
            newData.CtnTypeName = business.CtnTypeName;
            newData.CtnTypeName = business.CtnTypeName;
            newData.InvoiceDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newData.ExpressDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newData.IsValid = true;

            if (ClientConfig.Current.Contains(InvoiceCommandConstants.InvoiceReceivablesName))
            {
                newData.ReceivablesName = ClientConfig.Current.GetValue(InvoiceCommandConstants.InvoiceReceivablesName);
            }
            if (ClientConfig.Current.Contains(InvoiceCommandConstants.InvoiceReviewName))
            {
                newData.ReviewName = ClientConfig.Current.GetValue(InvoiceCommandConstants.InvoiceReviewName);
            }


            #region 转换账单数据
            List<BillList> billList = new List<BillList>();
            List<InvoiceFeeDate> chrgeaList = new List<InvoiceFeeDate>();

            List<SolutionExchangeRateList> rate;
            DateTime time = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

            //获取发票汇率列表
            rate = FinanceService.GetInvoiceExchangeRateList(true);

            Guid TargetCurrencyId = new Guid("DEB5F402-B6C0-4491-B247-B75C3EDA7976");//RMB为目标币种

            foreach (var item in DataSourceList)
            {
                #region 把帐单业务号填充到备注
                if (!string.IsNullOrEmpty(newData.Remark))
                {
                    newData.Remark += " ";
                }

                newData.Remark += item.OperationNO;
                #endregion

                BillList billItem = new BillList();
                billItem.ID = item.ID;
                billItem.CustomerName = item.CustomerName;
                billItem.No = item.BillNO;
                billItem.CompanyID = item.CompanyID;
                billItem.IsDirty = false;

                List<ChargeList> fees = FinanceService.GetChargeList(item.ID, item.CurrencyID, item.Way, item.IsCommission);
                if (fees != null)
                {
                    foreach (ChargeList chargeItem in fees)
                    {
                        chargeItem.Selected = true;

                        InvoiceFeeDate data = new InvoiceFeeDate();
                        decimal _amount = 0;
                        if (chargeItem.IsCommission || chargeItem.Way == FeeWay.AP)  //佣金和应付费用开票金额为负数
                            _amount = -chargeItem.Amount;
                        else
                            _amount = chargeItem.Amount;
                        //data.Amount = _amount - chargeItem.InvoiceAmount > 0 ? _amount - chargeItem.InvoiceAmount : 0;
                        data.Amount = _amount - chargeItem.InvoiceAmount;
                        data.BillFeeId = chargeItem.ID;
                        data.ChargingCode = chargeItem.ChargingCode;
                        data.ChargingCodeID = chargeItem.ChargingCodeID;
                        data.CreateByID = LocalData.UserInfo.LoginID;
                        data.CreateByName = LocalData.UserInfo.LoginName;
                        data.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                        data.CurrencyID = chargeItem.CurrencyID;
                        data.CurrencyName = chargeItem.CurrencyName;
                        data.Quantity = chargeItem.Quantity;
                        //data.Remark = chargeItem.Remark;
                        data.Remark = chargeItem.BillNo;

                        decimal rateValue = decimal.Round(RateHelper.GetRate(data.CurrencyID, TargetCurrencyId, time, rate), 4);
                        data.Rate = rateValue;

                        chrgeaList.Add(data);
                    }
                    billItem.Fees = fees;
                }
                else
                {
                    billItem.Fees = new List<ChargeList>();
                }

                billList.Add(billItem);
            }

            newData.BillList = billList;
            newData.Fees = chrgeaList;

            newData.EndEdit();
            #endregion


            PartLoader.ShowEditPart<InvoiceEditPart>(Workitem, newData, LocalData.IsEnglish ? "Add Invoice" : "新增发票", InvoicePartSaved);

            selectIdList = (from d in DataSourceList group d by d.ID into g select g.Key).ToList();


            ClareList();
        }

        /// <summary>
        /// 更新列表数据
        /// </summary>
        /// <param name="prams"></param>
        private void InvoicePartSaved(object[] prams)
        {
            ListOperating("Invoice", selectIdList);
        }
        #endregion

        #region 发票模板
        [CommandHandler(BillCommandConstants.Command_InvoiceContract)]
        public void Command_InvoiceContract(object sender, EventArgs e)
        {
            if (CurrentList == null || CurrentList.Count == 0)
            {
                return;
            }
            Guid[] ids = (from d in CurrentList select d.ID).ToArray();

            InvoiceFreeReportData baseReport = new InvoiceFreeReportData();
            baseReport = FinanceService.GetInvoiceContractReportt(ids, CurrentRow.CurrencyID);
            if (baseReport == null)
            {
                return;
            }
            try
            {
                IReportViewer viewer = ReportViewService.ShowReportViewer("发票合同", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
                string fileName = Application.StartupPath + "\\Reports\\FAM\\Invoice\\";

                fileName += "InvoiceContractl.frx";

                Dictionary<string, object> reportSource = new Dictionary<string, object>();
                reportSource.Add("InvoiceData", baseReport.DataList);
                reportSource.Add("InvoiceTotal", baseReport.TotalInfo);

                viewer.BindData(fileName, reportSource, null);

                ClareList();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        #endregion

        private List<Guid> GetIdList()
        {
            List<Guid> idList = new List<Guid>();
            if (DataSourceList == null || DataSourceList.Count == 0)
            {
                return idList;
            }
            idList = (from d in DataSourceList select d.ID).ToList();

            return idList;
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected CurrencyBillList CurrentRow
        {
            get { return Current as CurrencyBillList; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {

                List<CurrencyBillList> list = value as List<CurrencyBillList>;

                if (list != null && list.Count > 0)
                {
                    InitBillState();
                    InitCurrencyInfo(value);
                }

                gvMain.BeginUpdate();

                bsList.DataSource = value;

                bsList.ResetBindings(false);
                gvMain.BestFitColumns();

                TotalAmount();

                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }
                gvMain.EndUpdate();
            }
        }

        public List<CurrencyBillList> DataSourceList
        {
            get
            {
                return DataSource as List<CurrencyBillList>;
            }
        }

        public override event CurrentChangedHandler CurrentChanged;

        public override event SelectedHandler Selected;

        /// <summary>
        /// 列表操作事件
        /// </summary>
        public event SelectedHandler ListOperating;
        #endregion

        #region Gv Event

        void BulidRowCellStyle()
        {
            StyleFormatCondition commStyleFormatCondition = new StyleFormatCondition();
            gvMain.FormatConditions.Add(commStyleFormatCondition);

            commStyleFormatCondition.Appearance.ForeColor = Color.Red;
            commStyleFormatCondition.Condition = FormatConditionEnum.None;
            commStyleFormatCondition.Expression = "[" + colAmount.FieldName + "] < 0";
            commStyleFormatCondition.Condition = FormatConditionEnum.Expression;
            commStyleFormatCondition.ApplyToRow = false;
            commStyleFormatCondition.Column = colAmount;

            StyleFormatCondition commStyleFormatCondition2 = new StyleFormatCondition();
            gvMain.FormatConditions.Add(commStyleFormatCondition2);
            commStyleFormatCondition2.Appearance.ForeColor = Color.Blue;
            commStyleFormatCondition2.Condition = FormatConditionEnum.None;
            commStyleFormatCondition2.Expression = "[IsCommission] == True";
            commStyleFormatCondition2.Condition = FormatConditionEnum.Expression;
            commStyleFormatCondition2.ApplyToRow = false;
            commStyleFormatCondition2.Column = colAmount;
        }

        //private void bsList_PositionChanged(object sender, EventArgs e)
        //{
        //    //if (CurrentChanged != null)
        //    //{
        //    //    CurrentChanged(this, CurrentRow);
        //    //}
        //}

        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {

        }

        #endregion

        #region 统计
        /// <summary>
        /// 统计金额 
        /// </summary>
        private void TotalAmount()
        {
            txtAmount.Text = string.Empty;

            if (DataSourceList == null || DataSourceList.Count == 0)
            {
                TotalAmountByCurrency();
                return;
            }

            Dictionary<string, Decimal> dicTotal = (from d in DataSourceList group d by new { d.CurrencyID, d.CurrencyName } into g select new { g.Key, TotalAmount = g.Sum(p => p.Amount) }).ToDictionary(c => c.Key.CurrencyName, c => c.TotalAmount);
            if (dicTotal == null || dicTotal.Count == 0)
            {
                return;
            }

            foreach (KeyValuePair<string, Decimal> item in dicTotal)
            {
                txtAmount.Text += item.Key + ": " + item.Value.ToString("n") + " ";
            }

            TotalAmountByCurrency();
        }

        /// <summary>
        /// 按某一个币种进行统计
        /// </summary>
        private void TotalAmountByCurrency()
        {
            txtTotalByCurrency.Text = string.Empty;
            if (cmbCurrency.EditValue == null || (Guid)(cmbCurrency.EditValue) == Guid.Empty)
            {
                return;
            }

            Guid currencyID = (Guid)cmbCurrency.EditValue;
            decimal amount = 0;


            if (FAMUtility.GuidIsNullOrEmpty(currencyID) || RateList == null || RateList.Count == 0)
            {
                string message = LocalData.IsEnglish ? "Have no rate at current company." : "找不到当前公司下的汇率.";

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);

                return;
            }


            //循环账单的信息
            Dictionary<Guid, Decimal> dicTotal = (from d in DataSourceList group d by d.CurrencyID into g select new { g.Key, TotalAmount = g.Sum(p => p.Amount) }).ToDictionary(c => c.Key, c => c.TotalAmount);
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
                    decimal rate = RateHelper.GetRate(item.Key, currencyID, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified), RateList);
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

        #region 网格按键
        private void gcMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Command_Remove(null, null);
            }
        } 
        #endregion
    }
}
