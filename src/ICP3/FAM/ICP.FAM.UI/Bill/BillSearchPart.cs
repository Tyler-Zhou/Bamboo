using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.ClientComponents.Controls;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface;
using ICP.FAM.UI.Comm.OperationTypeSearchParts;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;
using ICP.Framework.CommonLibrary;
using ICP.Common.UI;

namespace ICP.FAM.UI.Bill
{
    /// <summary>
    /// 查询面板(账单列表)
    /// </summary>
    [ToolboxItem(false)]
    public partial class BillSearchPart : BaseSearchPart
    {
        #region 服务注入
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 财务服务
        /// </summary>
        IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        /// <summary>
        /// 基础数据服务
        /// </summary>
        ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }

        #endregion

        #region 属性&变量
        /// <summary>
        /// 账单查询参数
        /// </summary>
        BillSearchParameter searchParameter = new BillSearchParameter();
        /// <summary>
        /// 页面加载查询账单列表
        /// </summary>
        public BillListQueryCriteria querycriteria = new BillListQueryCriteria();

        /// <summary>
        /// 选择的方案发生改变时
        /// </summary>
        public event SelectedHandler ProgramSelectedChanged;

        /// <summary>
        /// 是否需要隐藏显示记录条数的信息
        /// </summary>
        private bool _isNeedHideMessage = false;

        /// <summary>
        /// 银行流水
        /// </summary>
        BankTransactionInfo BankTransaction { get; set; }
        /// <summary>
        /// 查询业务类型
        /// </summary>
        OperationType? SearchOperationType
        {
            get
            {
                if (cmbOperationType.EditValue != null && cmbOperationType.EditValue != DBNull.Value) return (OperationType)cmbOperationType.EditValue;
                else return null;
            }
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        public override event SearchResultHandler OnSearched;

        #endregion

        #region 初始化

        public BillSearchPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                RemoveKeyDownHandle();
                searchParameter = null;
                ProgramSelectedChanged = null;
                OnSearched = null;
                chkBankDate.CheckedChanged -= OnchkBankDateCheckedChanged;

                chkBillAmount.CheckedChanged -= OnchkBillAmountCheckedChanged;
                cmbOperationType.SelectedIndexChanged -= new EventHandler(cmbOperationType_SelectedIndexChanged);
                cmbOperationType.OnFirstEnter -= OncmbOperationTypeFirstEnter;
                cmbProgram.SelectedIndexChanged -= cmbProgram_SelectedIndexChanged;
                cmbProgram.OnFirstEnter -= OncmbProgramFirstEnter;
                cmbCompany.OnFirstEnter -= OncmbCompanyFirstEnter;

                MscSales.OnFirstEnter -= OnMscSalesFirstEnter;
                MscOperate.OnFirstEnter -= OnMscOperateFirstEnter;
                cmbCurrencyList.OnFirstEnter -= OncmbCurrencyListFirstEnter;
                if (_searchPart != null)
                {
                    _searchPart.Dispose();
                    _searchPart = null;
                }
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            rdoCheck.SelectedIndex = rdoInvoice.SelectedIndex = rdoType.SelectedIndex = rdoWriteOff.SelectedIndex = 0;
        }
        /// <summary>
        /// 面板加载(重写)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControls();
                SetKeyDownToSearch();

                #region ChkEvent
                chkBankDate.CheckedChanged += OnchkBankDateCheckedChanged;
                chkBillAmount.CheckedChanged += OnchkBillAmountCheckedChanged;
                #endregion
            }
        }
        /// <summary>
        /// 实现快速键
        /// </summary>
        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
            {
                btnSearch.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnClear.PerformClick();
            }
        }
        /// <summary>
        /// 根据计费时间复选框控制计费时间控件可用状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnchkBankDateCheckedChanged(object sender, EventArgs e)
        {
            dteBankDateFrom.Enabled = dteBankDateTo.Enabled = chkBankDate.Checked;
        }
        /// <summary>
        /// 根据账单金额复选框控制账单金额控件可用状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnchkBillAmountCheckedChanged(object sender, EventArgs e)
        {
            numMaxAmount.Enabled = numMinAmount.Enabled = chkBillAmount.Checked;
        }
        /// <summary>
        /// 填充业务类型到下拉控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OncmbOperationTypeFirstEnter(object sender, EventArgs e)
        {
            List<EnumHelper.ListItem<OperationType>> operationTypes = EnumHelper.GetEnumValues<OperationType>(LocalData.IsEnglish);
            cmbOperationType.Properties.BeginUpdate();
            cmbOperationType.Properties.Items.Clear();
            foreach (var item in operationTypes)
            {
                if (item.Value == OperationType.Unknown)
                {
                    cmbOperationType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "All" : "全部", item.Value));
                }
                else
                {
                    cmbOperationType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
            }
            cmbOperationType.Properties.EndUpdate();
        }
        /// <summary>
        /// 填充账单方案到下拉控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OncmbProgramFirstEnter(object sender, EventArgs e)
        {
            List<EnumHelper.ListItem<BillProgram>> billPrograms = EnumHelper.GetEnumValues<BillProgram>(LocalData.IsEnglish);
            cmbProgram.Properties.BeginUpdate();
            cmbProgram.Properties.Items.Clear();
            foreach (var item in billPrograms)
            {
                cmbProgram.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbProgram.Properties.EndUpdate();
        }
        /// <summary>
        /// 通过用户绑定操作口岸
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OncmbCompanyFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.BindCompanyByUser(cmbCompany, true);
        }
        /// <summary>
        /// 揽货人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMscSalesFirstEnter(object sender, EventArgs e)
        {
            FAMUtility.SetMcmbUsers(MscSales, GetCompanyIDs(), "", "");
        }
        /// <summary>
        /// 操作人(筛选职位为文件操作)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMscOperateFirstEnter(object sender, EventArgs e)
        {
            FAMUtility.SetMcmbUsers(MscOperate, GetCompanyIDs(), "文件", "");
        }
        /// <summary>
        /// 币种
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OncmbCurrencyListFirstEnter(object sender, EventArgs e)
        {

            List<CurrencyList> currencyList = ConfigureService.GetCurrencyList(null, null, null, true, 0);
            cmbCurrencyList.Properties.BeginUpdate();
            cmbCurrencyList.Properties.Items.Clear();
            foreach (var item in currencyList)
            {
                cmbCurrencyList.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }
            cmbCurrencyList.Properties.Items.Insert(0, new ImageComboBoxItem("", null));
            cmbCurrencyList.Properties.EndUpdate();
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            cmbOperationType.SelectedIndexChanged += new EventHandler(cmbOperationType_SelectedIndexChanged);
            //业务类型
            cmbOperationType.ShowSelectedValue(OperationType.Unknown, LocalData.IsEnglish ? "All" : "全部");
            cmbOperationType.OnFirstEnter += OncmbOperationTypeFirstEnter;


            searchParameter.DataPageInfo = new DataPageInfo();
            //账单方案
            cmbProgram.SelectedIndexChanged -= cmbProgram_SelectedIndexChanged;
            cmbProgram.ShowSelectedValue(BillProgram.Custom, EnumHelper.GetDescription<BillProgram>(BillProgram.Custom, LocalData.IsEnglish));
            SetCustomer();
            cmbProgram.SelectedIndexChanged += cmbProgram_SelectedIndexChanged;
            cmbProgram.OnFirstEnter += OncmbProgramFirstEnter;


            //绑定公司
            cmbCompany.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.DefaultCompanyName);
            cmbCompany.OnFirstEnter += OncmbCompanyFirstEnter;
            //绑定用户
            MscSales.OnFirstEnter += OnMscSalesFirstEnter;

            MscOperate.OnFirstEnter += OnMscOperateFirstEnter;

            cmbCurrencyList.OnFirstEnter += OncmbCurrencyListFirstEnter;
            DevHelper.FormatMoney(numMaxAmount);
            DevHelper.FormatMoney(numMinAmount);

        }
        /// <summary>
        /// 定义快捷键
        /// </summary>
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                item.KeyDown += new KeyEventHandler(item_KeyDown);
            }
        }
        /// <summary>
        /// 移除快捷键
        /// </summary>
        private void RemoveKeyDownHandle()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                item.KeyDown -= item_KeyDown;
            }
        }
        #endregion

        #region event
        OperationTypeSearchPart _searchPart;
        void cmbOperationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BulidChildSearchPart(SearchOperationType);
        }
        int _orgNavbarHeight = 750;
        private void BulidChildSearchPart(OperationType? operationType)
        {
            panelType.Controls.Clear();
            if (_searchPart != null) _searchPart.Dispose();
            _searchPart = OperationTypeSearchPartFactory.GetSearchPart(operationType);
            navBarOther.GroupClientHeight = panelOperationType.Height + _searchPart.Height;
            navBarControl1.Height = _orgNavbarHeight + _searchPart.Height;

            panelType.Controls.Add(_searchPart);
            _searchPart.Dock = DockStyle.Fill;

            _searchPart.SetTextBoxLocation(labOperationType.Location.X, cmbOperationType.Location.X, cmbOperationType.Width);
        }

        /// <summary>
        /// 销帐成功后刷新帐单列表面板
        /// </summary>
        [EventSubscription(ActionsConstants.FAM_REFRESHBILLLISTPART)]
        public void RefreshBillListPartAfterWriteoff(object sender, DataEventArgs<object> e)
        {
            _isNeedHideMessage = true;
            RaiseSearched();
        }
        private void cmbProgram_SelectedIndexChanged(object sender, EventArgs e)
        {

            ClareList();

            BillProgram program = (BillProgram)cmbProgram.SelectedIndex;

            switch (program)
            {
                case BillProgram.Custom:
                    SetCustomer();
                    break;
                case BillProgram.DepositWriteOff:
                    SetDepositWriteOff();
                    break;
                case BillProgram.CheckWriteOff:
                    SetCheckWriteOff();
                    break;
                case BillProgram.PaymentRequest:
                    SetPaymentRequest();
                    break;
                case BillProgram.OperationManagement:
                    SetOperationManagement();
                    break;
                case BillProgram.Invoicing:
                    SetInvoicing();
                    break;
                case BillProgram.Auditor:
                    SetAuditor();
                    break;
                case BillProgram.Dun:
                    SetDunStyle();
                    break;
                default:
                    break;
            }

            if (ProgramSelectedChanged != null)
            {
                ProgramSelectedChanged(sender, program);
            }
        }
        #endregion

        #region ISearchPart 成员
        /// <summary>
        /// 
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public override void RaiseSearched(object data)
        {
            DataPageInfo dataPageInfo = data as DataPageInfo;
            searchParameter.DataPageInfo = dataPageInfo;
            if (OnSearched != null)
            {
                OnSearched(this, GetData());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;

            txtBillNo.Text = string.Empty;
            txtCustomer.Text = string.Empty;
            txtInvoiceNo.Text = string.Empty;
            txtRefNo.Text = string.Empty;

            foreach (var item in values)
            {
                switch(item.Key.ToUpper())
                {
                    case "OPERATIONNO":
                        txtBillNo.Text = item.Value.ToString();
                        break;
                    case "CUSTOMERNAME":
                        txtCustomer.Text = item.Value.ToString();
                        break;
                    case "BILLREFNO":
                        txtRefNo.Text = item.Value.ToString();
                        break;
                    case "INVOICENO":
                        txtInvoiceNo.Text = item.Value.ToString();
                        break;
                    case "BANKTRANSACTION":
                        #region 银行流水
                        BankTransaction = item.Value as BankTransactionInfo;
                        if (BankTransaction != null)
                        {
                            BankAccountList accountList = FinanceService.GetBankAccountByNO(BankTransaction.AccountNO);
                            if (!accountList.ID.IsNullOrEmpty())
                            {
                                cmbCompany.EditValue = accountList.CompanyID;
                                cmbCompany.Properties.ReadOnly = true;
                            }
                            cmbProgram.Focus();
                            cmbProgram.SelectedIndex = 2;
                            cmbCurrencyList.Focus();
                            cmbCurrencyList.EditValue = new Guid("DEB5F402-B6C0-4491-B247-B75C3EDA7976");
                            chkBillAmount.Checked = true;


                            numMaxAmount.Value = BankTransaction.TransactionAmount;
                            chkBillAmount.Properties.ReadOnly
                                                = numMinAmount.Properties.ReadOnly
                                                = numMaxAmount.Properties.ReadOnly
                                                = cmbProgram.Properties.ReadOnly
                                                = cmbCurrencyList.Properties.ReadOnly
                                                = true;
                        } 
                        #endregion
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            try
            {
                BillListAllData list = FinanceService.GetBillListByCurrency(
                                                  searchParameter.AuditorStatue,
                                                  searchParameter.WriteOffStatue,
                                                  searchParameter.FeeWayStatue,
                                                  searchParameter.InvoiceStatue,
                                                  searchParameter.IsCommission,
                                                  searchParameter.CompanyID,
                                                  searchParameter.OperationID,
                                                  searchParameter.BillNo,
                                                  searchParameter.CustomerName,
                                                  searchParameter.RefNo,
                                                  searchParameter.InvoiceNo,
                                                  searchParameter.SalesID,
                                                  searchParameter.OperateID,
                                                  searchParameter.BillingStartDate,
                                                  searchParameter.BillingEndDate,
                                                  searchParameter.AmountMin,
                                                  searchParameter.AmountMax,
                                                  searchParameter.DataPageInfo,
                                                  searchParameter.OperType,
                                                  searchParameter.CurrencyID,
                                                  searchParameter.operationParameter,
                                                  LocalData.IsEnglish);

                return list;

            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerName"></param>
        public void CustomerNameSearch(string customerName)
        {
            txtCustomer.Text = customerName;
        }

        #endregion

        #region btn

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                SearchData();
            }
        }

        public void SeaDate()
        {
            SearchBillListData();
        }

        protected virtual void SearchData()
        {
            querycriteria = new BillListQueryCriteria();

            SearchBillListData();
        }


        private void SearchBillListData() 
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                searchParameter.AuditorStatue = (BillSearchAuditorStatue)rdoCheck.SelectedIndex;
                searchParameter.WriteOffStatue = (BillSearchWriteOffStatue)rdoWriteOff.SelectedIndex;
                searchParameter.InvoiceStatue = (BillSearchInvoiceStatue)rdoInvoice.SelectedIndex;
                searchParameter.IsCommission = ckbIsCommission.Checked;

                ///公司
                if (cmbCompany.EditValue != null)
                {
                    string companyID = cmbCompany.EditValue.ToString().Trim();

                    searchParameter.CompanyID = companyID.Replace(", ", GlobalConstants.DividedSymbol);
                }
                else
                {
                    searchParameter.CompanyID = FAMUtility.GetCompanyIDList().ToArray().Join();
                }

                if (querycriteria != null && querycriteria.OperationIds != null && querycriteria.OperationIds.Count > 0)
                {
                    searchParameter.OperationID = querycriteria.OperationIds.ToArray().Join();
                    searchParameter.FeeWayStatue = BillSearchFeeWay.AP;
                }
                else
                {
                    searchParameter.OperationID = string.Empty;
                    searchParameter.FeeWayStatue = (BillSearchFeeWay)rdoType.SelectedIndex;
                }


                ///揽货人
                if (MscSales.EditValue != null && new Guid(MscSales.EditValue.ToString()) != Guid.Empty)
                {
                    searchParameter.SalesID = new Guid(MscSales.EditValue.ToString());
                }
                else
                {
                    searchParameter.SalesID = null;
                }
                ///操作
                if (MscOperate.EditValue != null && new Guid(MscOperate.EditValue.ToString()) != Guid.Empty)
                {
                    searchParameter.OperateID = new Guid(MscOperate.EditValue.ToString());
                }
                else
                {
                    searchParameter.OperateID = null;
                }
                ///开始时间
                if (chkBankDate.Checked && dteBankDateFrom.Enabled && dteBankDateFrom.EditValue != null)
                {
                    searchParameter.BillingStartDate = (DateTime)dteBankDateFrom.EditValue;
                }
                else
                {
                    searchParameter.BillingStartDate = null;
                }
                ///结束时间
                if (chkBankDate.Checked && dteBankDateTo.Enabled && dteBankDateTo.EditValue != null)
                {
                    searchParameter.BillingEndDate = (DateTime)dteBankDateTo.EditValue;
                }
                else
                {
                    searchParameter.BillingEndDate = null;
                }
                ///最小金额
                if (chkBillAmount.Checked && numMinAmount.Enabled)
                {
                    searchParameter.AmountMin = numMinAmount.Value;
                }
                else
                {
                    searchParameter.AmountMin = null;
                }
                ///最大金额
                if (chkBillAmount.Checked && numMaxAmount.Enabled)
                {
                    searchParameter.AmountMax = numMaxAmount.Value;
                }
                else
                {
                    searchParameter.AmountMax = null;
                }

                searchParameter.BillNo = txtBillNo.Text;
                searchParameter.BLNo = txtBLNo.Text;
                searchParameter.CtnNo = txtCtnNo.Text;
                searchParameter.CustomerName = txtCustomer.Text;
                searchParameter.RefNo = txtRefNo.Text;
                searchParameter.InvoiceNo = txtInvoiceNo.Text;

                searchParameter.DataPageInfo.PageSize = (int)numMaxCount.Value;
                searchParameter.DataPageInfo.CurrentPage = 1;
                if (string.IsNullOrEmpty(searchParameter.DataPageInfo.SortByName))
                {
                    searchParameter.DataPageInfo.SortByName = "BillNo";
                    searchParameter.DataPageInfo.SortOrderType = SortOrderType.Desc;
                }

                if (_searchPart != null)
                {
                    _searchPart.BLNo = searchParameter.BLNo;
                    _searchPart.CtnNo = searchParameter.CtnNo;
                    _searchPart.ChargeCodeIDs = cbcbChargeCode.SelectValuesToGuid.Join();
                }

                searchParameter.OperType = (OperationType)cmbOperationType.SelectedIndex;
                if (cmbCurrencyList.SelectedIndex > 0)
                {
                    searchParameter.CurrencyID = new Guid(cmbCurrencyList.Properties.Items[cmbCurrencyList.SelectedIndex].Value.ToString());
                }
                else
                {
                    searchParameter.CurrencyID = null;
                }
                searchParameter.operationParameter = _searchPart == null ? null : _searchPart.GetOperationParameter();

                if (OnSearched != null)
                {
                    BillListAllData result = GetData() as BillListAllData;
                    if (!_isNeedHideMessage && result != null && result.PageList != null && result.PageList.DataPageInfo != null)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "total search " + result.PageList.DataPageInfo.TotalCount.ToString() + " data." : "总共查询到 "
                                                    + result.PageList.DataPageInfo.TotalCount.ToString() + " 条数据.");
                    }

                    if (_isNeedHideMessage)
                    {
                        _isNeedHideMessage = false;
                    }
                    OnSearched(this, result);
                }
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void ClearControls()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is LWImageComboBoxEdit)
                {
                    (item as LWImageComboBoxEdit).SelectedIndex = 0;
                }
                if (item is MultiSearchCommonBox)
                {
                    (item as MultiSearchCommonBox).EditText = string.Empty;
                    (item as MultiSearchCommonBox).EditValue = null;
                }
                else if (item is TextEdit
                     && (item is SpinEdit) == false
                     && item.Enabled == true
                     && (item as TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }

            chkBillAmount.Checked = chkBankDate.Checked = false;
        }

        #endregion

        #region 绑定揽货人与操作

        private List<Guid> GetCompanyIDs()
        {
            List<Guid> compaynIDs = new List<Guid>();
            if (cmbCompany.EditValue == null)
            {
                compaynIDs = FAMUtility.GetCompanyIDList();
            }
            else
            {
                compaynIDs.Add(new Guid(cmbCompany.EditValue.ToString()));
            }
            return compaynIDs;
        }
        #endregion

        #region 方案发生改变时
        /// <summary>
        /// 清空列表
        /// </summary>
        private void ClareList()
        {
            Workitem.Commands[BillCommandConstants.Command_ClareSelectListByProgram].Execute();
            Workitem.Commands[BillCommandConstants.Command_ClareDetailByProgram].Execute();
        }
        /// <summary>
        /// 设置自定义方案的样式
        /// </summary>
        private void SetCustomer()
        {
            ckbIsCommission.Enabled = true;
            ckbIsCommission.Checked = null;

            rdoType.Enabled = true;
            rdoType.SelectedIndex = 0;

            rdoCheck.Enabled = true;
            rdoCheck.SelectedIndex = 0;

            rdoWriteOff.Enabled = true;
            rdoWriteOff.SelectedIndex = 0;

            rdoInvoice.Enabled = true;
            rdoInvoice.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置应收销账方案的样式
        /// </summary>
        private void SetDepositWriteOff()
        {
            ckbIsCommission.Enabled = false;
            ckbIsCommission.Checked = null;

            rdoType.Enabled = false;
            rdoType.SelectedIndex = 1;

            rdoCheck.Enabled = true;
            rdoCheck.SelectedIndex = 0;

            rdoWriteOff.Enabled = false;
            rdoWriteOff.SelectedIndex = 2;

            rdoInvoice.Enabled = true;
            rdoInvoice.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置应付销账方案的样式
        /// </summary>
        private void SetCheckWriteOff()
        {
            ckbIsCommission.Enabled = false;
            ckbIsCommission.Checked = null;

            rdoType.Enabled = false;
            rdoType.SelectedIndex = 2;

            rdoCheck.Enabled = true;
            rdoCheck.SelectedIndex = 0;

            rdoWriteOff.Enabled = false;
            rdoWriteOff.SelectedIndex = 2;

            rdoInvoice.Enabled = false;
            rdoInvoice.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置付款申请方案的样式
        /// </summary>
        private void SetPaymentRequest()
        {
            ckbIsCommission.Enabled = false;
            ckbIsCommission.Checked = false;

            rdoType.Enabled = false;
            rdoType.SelectedIndex = 2;

            rdoCheck.Enabled = true;
            rdoCheck.SelectedIndex = 0;

            rdoWriteOff.Enabled = false;
            rdoWriteOff.SelectedIndex = 2;

            rdoInvoice.Enabled = false;
            rdoInvoice.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置审核方案的样式
        /// </summary>
        private void SetAuditor()
        {
            ckbIsCommission.Enabled = true;
            ckbIsCommission.Checked = null;

            rdoType.Enabled = true;
            rdoType.SelectedIndex = 0;

            rdoCheck.Enabled = true;
            rdoCheck.SelectedIndex = 2;

            rdoWriteOff.Enabled = false;
            rdoWriteOff.SelectedIndex = 2;

            rdoInvoice.Enabled = true;
            rdoInvoice.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置业务管理成本方案的样式
        /// </summary>
        private void SetOperationManagement()
        {
            ckbIsCommission.Enabled = true;
            ckbIsCommission.Checked = true;

            rdoType.Enabled = false;
            rdoType.SelectedIndex = 0;

            rdoCheck.Enabled = false;
            rdoCheck.SelectedIndex = 1;

            rdoWriteOff.Enabled = false;
            rdoWriteOff.SelectedIndex = 2;

            rdoInvoice.Enabled = false;
            rdoInvoice.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置开发票样式
        /// </summary>
        private void SetInvoicing()
        {
            ckbIsCommission.Enabled = false;
            ckbIsCommission.Checked = null;

            rdoType.Enabled = false;
            rdoType.SelectedIndex = 1;

            rdoCheck.Enabled = false;
            rdoCheck.SelectedIndex = 1;

            rdoWriteOff.Enabled = false;
            rdoWriteOff.SelectedIndex = 2;

            rdoInvoice.Enabled = false;
            rdoInvoice.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置催款单样式
        /// </summary>
        private void SetDunStyle()
        {
            rdoType.Enabled = false;
            rdoType.SelectedIndex = 1;

            rdoCheck.Enabled = true;
            rdoCheck.SelectedIndex = 0;

            rdoWriteOff.Enabled = false;
            rdoWriteOff.SelectedIndex = 2;

            rdoInvoice.Enabled = true;
            rdoInvoice.SelectedIndex = 0;
        }

        #endregion
    }

    /// <summary>
    /// 账单查找
    /// </summary>
    public class BillFinderSearchPart : BillSearchPart
    {
        public override void InitialValues(string searchValue, string property
                                       , SearchConditionCollection conditions
                                       , FinderTriggerType triggerType)
        {
            ClearControls();

            if (triggerType == FinderTriggerType.KeyEnter)
            {
                if (property.Contains(SearchFieldConstants.BillNo))
                    txtBillNo.Text = searchValue;
                else if (property.Contains(SearchFieldConstants.BLNo))
                    txtBLNo.Text = searchValue;
            }

            if (conditions != null)
            {
                cmbProgram.Enabled = false;

                if (conditions.Contain("BillNo"))
                {
                    txtBillNo.Text = conditions.GetValue("BillNo").Value.ToString();
                }

                if (conditions.Contain("BLNO"))
                {
                    txtBLNo.Text = conditions.GetValue("BLNO").Value.ToString();
                }
                if (conditions.Contain("CustomerName"))
                {
                    txtCustomer.Text = conditions.GetValue("CustomerName").Value.ToString();
                    if (!string.IsNullOrEmpty(txtCustomer.Text))
                    {
                        if (!conditions.GetValue("CustomerName").CanChange)
                        {
                            txtCustomer.Properties.ReadOnly = true;
                        }
                    }
                }

                if (conditions.Contain("BillRefNO"))
                {
                    txtRefNo.Text = conditions.GetValue("BillRefNO").Value.ToString();
                }

                if (conditions.Contain("InvoiceNo"))
                {
                    txtInvoiceNo.Text = conditions.GetValue("InvoiceNo").Value.ToString();
                }
                if (conditions.Contain("IsCheck"))
                {
                    if ((bool)conditions.GetValue("InvoiceNo").Value)
                    {
                        rdoCheck.SelectedIndex = 1;
                    }
                    else
                    {
                        rdoCheck.SelectedIndex = 2;
                    }
                }
                if (conditions.Contain("Way"))
                {
                    FeeWay way = (FeeWay)conditions.GetValue("Way").Value;
                    rdoType.SelectedIndex = way.GetHashCode();
                }
                if (conditions.Contain("CompanyID"))
                {
                    new ICPCommUIHelper().BindCompanyByUser(cmbCompany, true);
                    cmbCompany.OnFirstEnter -= OncmbCompanyFirstEnter;

                    cmbCompany.EditValue = conditions.GetValue("CompanyID").Value;
                    string companyName = string.Empty;
                    if (conditions.Contain("CompanyName"))
                    {
                        cmbCompany.Text = DataTypeHelper.GetString(conditions.GetValue("CompanyName").Value);
                    }
                    cmbCompany.Properties.ReadOnly = true;
                }
                if (conditions.Contain("CheckStatus"))
                {
                    BillSearchWriteOffStatue status = (BillSearchWriteOffStatue)conditions.GetValue("CheckStatus").Value;
                    rdoWriteOff.SelectedIndex = status.GetHashCode();
                    rdoWriteOff.Enabled = false;
                }
            }

        }
    }

    /// <summary>
    /// 账单查询参数实体
    /// </summary>
    public class BillSearchParameter
    {
        public BillSearchAuditorStatue AuditorStatue { get; set; }
        public BillSearchWriteOffStatue WriteOffStatue { get; set; }
        public BillSearchFeeWay FeeWayStatue { get; set; }
        public BillSearchInvoiceStatue InvoiceStatue { get; set; }
        public bool? IsCommission { get; set; }
        public string CompanyID { get; set; }

        /// <summary>
        /// 业务号集合
        /// </summary>
        public string OperationID { get; set; }

        public string BillNo { get; set; }
        public string BLNo { get; set; }
        public string CtnNo { get; set; }
        public string CustomerName { get; set; }
        public string RefNo { get; set; }
        public string InvoiceNo { get; set; }
        public Guid? SalesID { get; set; }
        public Guid? OperateID { get; set; }
        public DateTime? BillingStartDate { get; set; }
        public DateTime? BillingEndDate { get; set; }
        public Decimal? AmountMin { get; set; }
        public Decimal? AmountMax { get; set; }
        public DataPageInfo DataPageInfo { get; set; }
        public OperationType OperType { get; set; }
        public Guid? CurrencyID { get; set; }
        public OperationParameter operationParameter { get; set; }
    }
}
