using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using ICP.FCM.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using DevExpress.XtraGrid;
using ICP.Framework.ClientComponents.Controls;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Business.Common.ServiceInterface;
using ICP.FAM.ServiceInterface.CompositeObjects;

namespace ICP.FAM.UI.Bill
{
    /// <summary>
    /// 账单列表
    /// </summary>
    [ToolboxItem(false)]
    public partial class BillListPart : BaseListPart
    {
        #region 服务注入
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        IBusinessInfoProviderFactory BusinessInfoProviderFactory
        {
            get
            {
                return ServiceClient.GetClientService<IBusinessInfoProviderFactory>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IConfigureService configureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }


        RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }


        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }

        IICPCommonOperationService ICPCommonOperationService
        {
            get
            {
                return ServiceClient.GetService<IICPCommonOperationService>();
            }
        }

        #endregion

        #region 本地变量
        /// <summary>
        /// 当前公司下的汇率信息
        /// </summary>
        List<SolutionExchangeRateList> RateList = new List<SolutionExchangeRateList>();
        List<SolutionCurrencyList> CurrencyList = new List<SolutionCurrencyList>();
        Guid? DefaultCurrencyID;
        List<BillTotalInfo> ListProfit = new List<BillTotalInfo>();
        public override event InvokeGetDataHandler InvokeGetData;
        DataPageInfo _dataPageInfo = new DataPageInfo();

        #endregion

        #region 初始化
        /// <summary>
        /// 
        /// </summary>
        public BillListPart()
        {
            InitializeComponent();
            IsShowLanguageMenu = false;//不显示右键多语言菜单
            if (LocalData.IsEnglish == false) SetCnText();

            Disposed += delegate {
                RateList = null;
                CurrencyList = null;
                ListProfit = null;
                InvokeGetData = null;
                _dataPageInfo = null;
                _totalInfoList = null;
                CurrentChanged = null;
                KeyDown = null;
                Selected = null;
                gcMain.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                cmbCurrency.SelectedIndexChanged -= cmbCurrency_SelectedIndexChanged;
                selectDataList = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        private void SetCnText()
        {
            barUnSelect.Caption = "反选(&U)";
            barSelectAll.Caption = "全选(&A)";
            barOpenTaskCenter.Caption = "打开任务中心(&O)";
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

        private void InitMessage()
        {
            RegisterMessage("1108090001", LocalData.IsEnglish ? "Are you sure to un-lock the bill?" : "确认要解锁该账单？");
            RegisterMessage("1108090002", LocalData.IsEnglish ? "Are you sure to approve the bill?" : "确认要审核该账单？");
            RegisterMessage("1108090003", LocalData.IsEnglish ? "Are you sure to undo the approval of the bill?" : "确认取消审核该账单？");
            RegisterMessage("1108090004", LocalData.IsEnglish ? "The state of the bill is {0}, it could not be approved. " : "该账单已经是{0}状态,无法取消审核");

            RegisterMessage("1109070001", LocalData.IsEnglish ? "The bill is already paied." : "该账单已经销账!");
            RegisterMessage("1109070002", LocalData.IsEnglish ? "The bill is approved, The audit state of the selected bill must be same with following items." : "该账单已经审核,选择的账单必须与下面列表中的审核状态一致!");
            RegisterMessage("1109070003", LocalData.IsEnglish ? "The bill is not yet approved, The audit state of the selected bill must be same with following items." : "该账单还未审核,选择的账单必须与下面列表中的审核状态一致!");

            RegisterMessage("1109070004", LocalData.IsEnglish ? "The selected bills should be the same customer." : "不是相同客户的账单不能选择到一起!");
            RegisterMessage("1109070005", LocalData.IsEnglish ? "The selected bills should be the same branch/division." : "不是相同公司的账单不能选择到一起!");
            RegisterMessage("1109070006", LocalData.IsEnglish ? "The selected bills should be the same currency." : "不是相同币种的账单不能选择到一起!");
            RegisterMessage("1109070007", LocalData.IsEnglish ? "Only Commission Bill is allowed." : "只能选择业务管理成本的账单!");

            RegisterMessage("1110250001", LocalData.IsEnglish ? "The invoice of the [{0}] bill has been made. Clicks OK to select it. Clicks Cancel to give up." : "账单号[{0}]已开过发票，按确定选择此账单，取消则放弃选择!");

        }

        private void InitControls()
        {
            FAMUtility.ShowGridRowNo(gvMain);
            InitCurrencyInfo();
        }

        bool isShowTotal = false;

        /// <summary>
        /// 初始化币种及汇率信息
        /// </summary>
        private void InitCurrencyInfo()
        {
            if (isShowTotal)
            {
                RateList = configureService.GetCompanyExchangeRateList(LocalData.UserInfo.DefaultCompanyID, true);

                ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);

                //找到解决方案
                if (configureInfo != null)
                {
                    CurrencyList = configureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);
                }
                else
                {
                    return;
                }
                //填充下拉框与币种信息
                cmbCurrency.Properties.BeginUpdate();
                foreach (SolutionCurrencyList currency in CurrencyList)
                {
                    cmbCurrency.Properties.Items.Add(new ImageComboBoxItem(currency.CurrencyName, currency.CurrencyID));
                }
                cmbCurrency.Properties.EndUpdate();
                DefaultCurrencyID = configureInfo.DefaultCurrencyID;
                cmbCurrency.EditValue = configureInfo.DefaultCurrencyID;
            }
        }

        #endregion

        #region 属性

        private List<BillListTotalInfo> _totalInfoList = new List<BillListTotalInfo>();

        /// <summary>
        /// 方案
        /// </summary>
        public BillProgram Billprogram
        {
            get;
            set;
        }

        public Dictionary<string, CurrencyBillList> selectDataList = new Dictionary<string, CurrencyBillList>();

        #region 搜索器中使用

        protected virtual bool IsFinder { get { return false; } }

        /// <summary>
        /// 是否需要验证客户是否相同
        /// </summary>
        protected bool IsValidateCustomer { get; set; }

        /// <summary>
        /// 是否需要验证客户是否相同
        /// </summary>
        protected Guid? CustomerID { get; set; }

        /// <summary>
        /// 是否需要验证公司相同
        /// </summary>
        protected bool IsValidateCompany { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        protected Guid CompanyID { get; set; }

        /// <summary>
        /// 是否是发票界面的搜索器
        /// </summary>
        protected bool IsInvoiceSearch { get; set; }
        #endregion

        #endregion

        #region IListPart 成员
        /// <summary>
        /// 
        /// </summary>
        public override object Current
        {
            get { return bsList.Current; }
        }
        /// <summary>
        /// 
        /// </summary>
        protected CurrencyBillList CurrentRow
        {
            get { return Current as CurrencyBillList; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindingData(value);
                TotalAmount();
            }
        }

        private void BindingData(object value)
        {
            BillListAllData data = value as BillListAllData;
            List<CurrencyBillList> source = null;
            if (data != null)
            {
                source = data.PageList.GetList<CurrencyBillList>();
                _totalInfoList = data.TotalInfoList;
            }

            if (source == null || source.Count == 0)
            {
                bsList.DataSource = new List<CurrencyBillList>();
                pageControl1.TotalPage = 0;
                pageControl1.CurrentPage = 0;
                gvMain.SortInfo.Clear();
            }
            else
            {
                if (!IsFinder)
                {
                    bool isChangeSelctList = false;
                    foreach (CurrencyBillList item in source)
                    {
                        //如果当前数据在已选择的列表中，则选中，并更新已选中的列表
                        int i = (from e in selectDataList where e.Key == item.CurrentID select e.Key).Count();
                        if (i > 0)
                        {
                            item.Selected = true;
                            isChangeSelctList = true;

                            selectDataList[item.CurrentID] = item;
                        }
                    }
                    if (isChangeSelctList)
                    {
                        Selected(this, selectDataList.Values.ToList());
                    }
                }

                bsList.DataSource = source;
                bsList.ResetBindings(false);
                gvMain.BestFitColumns();

                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }


                _dataPageInfo = data.PageList.DataPageInfo;
                if (_dataPageInfo != null)
                {

                    int pageCount = _dataPageInfo.TotalCount / _dataPageInfo.PageSize;
                    if (pageCount == 1 && _dataPageInfo.TotalCount > _dataPageInfo.PageSize)
                    {
                        pageCount = 2;
                    }
                    if (pageCount == 0 && _dataPageInfo.TotalCount > 0)
                    {
                        pageCount = 1;
                    }
                    pageControl1.TotalPage = pageCount;

                    pageControl1.CurrentPage = _dataPageInfo.CurrentPage;
                    ColumnSortOrder sortOrder = _dataPageInfo.SortOrderType == SortOrderType.Asc ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending;
                    GridColumn col = gvMain.Columns.ColumnByFieldName(_dataPageInfo.SortByName);

                    if (col != null)
                    {
                        gvMain.SortInfo.Clear();
                        gvMain.SortInfo.Add(new GridColumnSortInfo(col, sortOrder));
                    }
                }
                else
                {
                    _dataPageInfo = new DataPageInfo();
                }
            }
        }

        private List<CurrencyBillList> DataList
        {
            get
            {
                List<CurrencyBillList> list = bsList.DataSource as List<CurrencyBillList>;
                if (list == null)
                {
                    list = new List<CurrencyBillList>();
                }
                return list;
            }
        }

        public override event SelectedHandler Selected;
        public override event CurrentChangedHandler CurrentChanged;

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }
        }
        #endregion

        #region Workitem Common

        #region 查看业务信息
        [CommandHandler(BillCommandConstants.Command_ViewBusinessInfo)]
        public void Command_ViewBusinessInfo(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }

                if (CurrentRow.OperType == OperationType.Other)
                {
                    FAMUtility.ShowMessage(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"其它业务不能查看业务信息!");
                    return;
                }
                IBusinessInfoProvider provider = BusinessInfoProviderFactory.GetBusinessInfoProvider(CurrentRow.OperType);
                provider.ShowBusinessInfo(CurrentRow.OperType, CurrentRow.OperationID, ClientConstants.MainWorkspace);
            }
        }
        #endregion

        #region 查看帐单列表
        [CommandHandler(BillCommandConstants.Command_ViewBillList)]
        public void Command_ViewBillList(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }

                OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(CurrentRow.OperationID, CurrentRow.OperType);
                if (operationCommonInfo != null)
                {
                    FinanceClientService.ShowBillList(operationCommonInfo, ClientConstants.MainWorkspace);
                }
                else
                {
                    FAMUtility.ShowMessage(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }
            }
        }

        #endregion

        #region 查看销账历史
        [CommandHandler(BillCommandConstants.Command_WriteOffHistory)]
        public void Command_WriteOffHistory(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string title = LocalData.IsEnglish ? "WriteOffHistory" : "销账历史";
                WriteOffHistoryForm wf = Workitem.Items.AddNew<WriteOffHistoryForm>();
                wf.RateList = RateList;
                wf.CurrencyList = CurrencyList;
                wf.DefaultCurrencyID = DefaultCurrencyID;
                wf.billList = CurrentRow;

                PartLoader.ShowDialog(wf, title);
            }
        }
        #endregion

        #region 费用明细
        [CommandHandler(BillCommandConstants.Command_FeeDetail)]
        public void Command_FeeDetail(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }

                string title = LocalData.IsEnglish ? "View ChargeDetail" : "查看费用明细";
                FeeDetailForm ff = Workitem.Items.AddNew<FeeDetailForm>();
                ff.BillDataSource = CurrentRow;

                if ((CurrencyList == null || CurrencyList.Count == 0) && !isShowTotal)
                {
                    isShowTotal = true;
                    InitCurrencyInfo();
                }
                ff.CurrencyList = CurrencyList;
                ff.RateList = RateList;

                ff.DefaultCurrencyID = DefaultCurrencyID;


                PartLoader.ShowDialog(ff, title);

            }
        }

        #endregion

        #region 显示合计
        [CommandHandler(BillCommandConstants.Command_ShowTotal)]
        public void Command_ShowTotal(object sender, EventArgs e)
        {
            if (!panelTotal.Visible && !isShowTotal)
            {
                isShowTotal = true;
                InitCurrencyInfo();
            }
            panelTotal.Visible = !panelTotal.Visible;
        }
        #endregion

        #region 设置发票号和快递单号
        [CommandHandler(BillCommandConstants.Command_SetInvoiceNo)]
        public void Command_SetInvoiceNo(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                SetInvoiceNoPart setInvoiceNoPart = Workitem.Items.AddNew<SetInvoiceNoPart>();
                List<InvoiceList> invoiceList = FinanceService.GetInvoiceList(CurrentRow.ID, CurrentRow.CurrencyID, CurrentRow.Way, CurrentRow.IsCommission);
                setInvoiceNoPart.CurrentBill = CurrentRow;
                setInvoiceNoPart.DataSource = invoiceList;
                //Dictionary<string, object> keyValue = new Dictionary<string, object>();
                //keyValue.Add("CurrencyBillList", CurrentRow);
                string title = LocalData.IsEnglish ? "Set Invoice No" : "设置发票号和快递单号";
                setInvoiceNoPart.Saved += delegate(object[] prams)
                {
                    if (prams != null && prams.Length > 0)
                    {
                        CurrentRow.InvoiceNo = prams[0].ToString();
                        CurrentRow.ExpressNo = prams[1].ToString();
                        bsList.ResetCurrentItem();
                    }
                };

                PartLoader.ShowDialog(setInvoiceNoPart, title);
            }
        }
        #endregion

        #region 查看发票
        /// <summary>
        /// 查看发票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BillCommandConstants.Command_ViewInvoice)]
        public void Command_ViewInvoice(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }

            List<InvoiceList> invoiceList = FinanceService.GetInvoiceList(CurrentRow.ID, CurrentRow.CurrencyID, CurrentRow.Way, CurrentRow.IsCommission);

            if (invoiceList == null || invoiceList.Count == 0)
            {
                return;
            }
            else if (invoiceList.Count == 1)
            {
                Guid invoice = invoiceList[0].ID;
                InvoiceInfo info = FinanceService.GetInvoiceInfo(invoice, LocalData.IsEnglish);
                InvoiceEditPart viewInvoice = Workitem.Items.AddNew<InvoiceEditPart>();

                IWorkspace mainWorkspace = Workitem.Workspaces[ClientConstants.MainWorkspace];

                viewInvoice.DataSource = info;
                viewInvoice.ReadOnly = true;

                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "View Invoice" : "查看发票";
                mainWorkspace.Show(viewInvoice, smartPartInfo);

            }
            else if (invoiceList.Count > 1)
            {
                DataPageInfo dp = new DataPageInfo();
                dp.TotalCount = invoiceList.Count;
                dp.CurrentPage = 1;
                dp.PageSize = int.MaxValue;
                PageList list = PageList.Create<InvoiceList>(invoiceList, dp);

                BillViewInvoiceList viewInvoice = Workitem.Items.AddNew<BillViewInvoiceList>();
                viewInvoice.DataSource = list;
                string title = LocalData.IsEnglish ? "View Invoice" : "查看发票";
                PartLoader.ShowDialog(viewInvoice, title);

            }
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
        }
        #endregion

        #region 改变方案发生改变时
        /// <summary>
        /// 改变方案发生改变时，清空已选择的列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BillCommandConstants.Command_ClareSelectListByProgram)]
        public void Command_ClareSelectListByProgram(object sender, EventArgs e)
        {
            selectDataList = new Dictionary<string, CurrencyBillList>();

            DataSource =PageList.Create<CurrencyBillList>(new List<CurrencyBillList>(), new DataPageInfo());

            if (IsValidateCustomer)
            {
                CustomerID = Guid.Empty;
            }
        }
        #endregion

        #region 全选
        [CommandHandler(BillCommandConstants.Command_AllCheck)]
        public void Command_AllCheck(object sender, EventArgs e)
        {
            SelectAll();
        }

        /// <summary>
        /// 全选(搜索器调用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BillCommandConstants.Command_FinderSelectAll)]
        public void Command_FinderSelectAll(object sender, EventArgs e)
        {
            SelectAll();
        }

        #endregion

        #region 
        /// <summary>
        /// 打开任务中心
        /// </summary>
        [CommandHandler(BillCommandConstants.Command_OpenTaskCenter)]
        public void Command_OpenTaskCenter(object sender, EventArgs e)
        {
            OperTaskCenter();
        }
        #endregion

        #region 应收账单转换成代理账单
        /// <summary>
        /// 应收账单转换成代理账单
        /// </summary>
        [CommandHandler(BillCommandConstants.Command_ConvertBillFromARToDN)]
        public void Command_ConvertBillFromARToDN(object sender, EventArgs e)
        {
            try
            {
                ConvertBillFromARToDN();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        #endregion

        #endregion

        #region 全选与反选

        private void barSelectAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            SelectAll();
        }

        private void barUnSelect_ItemClick(object sender, ItemClickEventArgs e)
        {
            UnSelect();
        }
        /// <summary>
        /// 反选
        /// </summary>
        private void UnSelect()
        {
            if (DataList == null || DataList.Count == 0)
            {
                return;
            }


            DataList.ForEach(o => o.Selected = !o.Selected);

            if (Selected != null)
            {
                selectDataList.Clear();

                selectDataList = (from d in DataList where d.Selected select d).ToDictionary(c => c.CurrentID, c => c);

                Selected(this, selectDataList.Values.ToList());
            }


            bsList.DataSource = DataList;
            bsList.ResetBindings(false);
            gvMain.BestFitColumns();

        }
        /// <summary>
        /// 全选
        /// </summary>
        private void SelectAll()
        {
            if (DataList == null || DataList.Count == 0)
            {
                return;
            }

            DataList.ForEach(o => o.Selected = true);

            if (Selected != null)
            {
                Guid customerID = Guid.Empty;
                if (Billprogram == BillProgram.CheckWriteOff || Billprogram == BillProgram.DepositWriteOff || (IsFinder && IsValidateCustomer))
                {
                    if (selectDataList.Count == 0)
                    {
                        customerID = DataList[0].CustomerID;
                    }
                    else
                    {
                        customerID = selectDataList.Values.First().CustomerID;
                    }
                }

                foreach (var item in DataList)
                {
                    if (!selectDataList.Keys.Contains(item.CurrentID) && (customerID == Guid.Empty || item.CustomerID == customerID))
                    {
                        selectDataList.Add(item.CurrentID, item);
                    }
                }

                Selected(this, selectDataList.Values.ToList());
            }

            bsList.ResetBindings(false);
            gvMain.BestFitColumns();
        }

        #endregion

        #region GridView Event

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

        private void gcMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                popupMenu1.ShowPopup(MousePosition);
            }
        }
        #endregion

        #region 点击单元格
        /// <summary>
        /// 链接单击时
        /// </summary>
        public event LinkClickedEventHandler LinkClickedEvent;
        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            #region 在账单列表中选择时
            if (e.Column == colSelected)
            {
                if (!CurrentRow.Selected)
                {
                    #region 选中
                    string message = string.Empty;

                    if (IsFinder)
                    {
                        #region 搜索器中判断
                        if (IsValidateCustomer && CurrentRow.CustomerID != CustomerID && CustomerID != null && CustomerID != Guid.Empty)
                        {
                            //不是相同客户的，不能选择到一起
                            message = LocalData.IsEnglish ? "Can not choose,Not the same Customer Bill" : "不是相同客户的账单的无法选择到一起";
                            FAMUtility.ShowMessage(message);

                            return;
                        }

                        if (IsValidateCompany && CurrentRow.CompanyID != CompanyID)
                        {
                            //不是相同公司的，不能选择到一起
                            message = LocalData.IsEnglish ? "Can not choose, Current Company Bill" : "不是相同公司的账单无法选择到一起";

                            FAMUtility.ShowMessage(message);

                            return;
                        }
                        if (CustomerID == null || CustomerID == Guid.Empty)
                        {
                            CustomerID = CurrentRow.CustomerID;
                        }
                        if (IsInvoiceSearch)
                        {
                            //发票的编辑界面上，使用搜索器时，如果已经开过发票，就提示
                            if (!string.IsNullOrEmpty(CurrentRow.InvoiceNo))
                            {
                                string billNoMessage = string.Format(NativeLanguageService.GetText(this, "1110250001"), CurrentRow.BillNO);
                                if (!FAMUtility.ShowResultMessage(billNoMessage))
                                {
                                    return;
                                }

                            }
                        }

                        #endregion
                    }
                    else
                    {
                        if (Billprogram == BillProgram.Auditor)
                        {
                            #region 审核模式下的验证
                            //已销账的，不能再选择了
                            if (CurrentRow.WriteOffAmount > 0)
                            {
                                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109070001"));
                                return;
                            }
                            //状态不一致的
                            int i = (from d in selectDataList where d.Value.Checked != CurrentRow.Checked select d.Value.ID).Count();
                            if (i > 0)
                            {
                                if (CurrentRow.Checked)
                                {
                                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109070002"));
                                    return;
                                }
                                else
                                {                                   
                                    //取消审核时，包含了未审核的
                                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109070003"));
                                    return;
                                }

                            }

                            #endregion
                        }
                        else if (Billprogram == BillProgram.DepositWriteOff || Billprogram == BillProgram.CheckWriteOff)
                        {
                            #region 应收/付 销账
                            if (!ValidateCustomer || !ValidateCompany)
                            //if (!ValidateCustomer)
                            {
                                return;
                            }

                            #endregion
                        }
                        else if (Billprogram == BillProgram.PaymentRequest)
                        {
                            #region 付款申请
                            if (!ValidateCustomer || !ValidateCompany)
                            {
                                return;
                            }
                            #endregion
                        }
                        else if (Billprogram == BillProgram.OperationManagement)
                        {
                            #region 业务管理成本
                            //客户、公司、币种
                            if (!ValidateCustomer || !ValidateCompany || !ValidateCurrency)
                            {
                                return;
                            }
                            //当前数据不是佣金
                            if (!CurrentRow.IsCommission)
                            {
                                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109070007"));
                                return;
                            }
                            #endregion
                        }
                        else if (Billprogram == BillProgram.Dun)
                        {
                            #region 催款单
                            if (!ValidateCustomer)
                            {
                                return;
                            }
                            #endregion
                        }
                    }
                    if (!selectDataList.Keys.Contains(CurrentRow.CurrentID))
                    {
                        selectDataList.Add(CurrentRow.CurrentID, CurrentRow);
                    }

                    #endregion
                }
                else
                {
                    ////取消选中，有可能是重新查询过的,指针已发生改变了，所以要根据ID来判断
                    //Guid removeID = new Guid(CurrentRow.ID.ToString());
                    //var items = (from w in DataList where w.ID == removeID select w);
                    //foreach (var itme in items)
                    //{
                    //    CurrencyBillList billList = itme as CurrencyBillList;
                    //    if (billList != null && selectDataList.Keys.Contains(billList.CurrentID))
                    //    {
                    //        selectDataList.Remove(billList.CurrentID);
                    //    }
                    //} 

                    if (selectDataList.Keys.Contains(CurrentRow.CurrentID))
                    {
                        selectDataList.Remove(CurrentRow.CurrentID);
                    }
                }

                CurrentRow.Selected = !CurrentRow.Selected;
                bsList.ResetCurrentItem();

                if (Selected != null)
                {
                    Selected(this, selectDataList.Values.ToList());
                }
                if (IsValidateCustomer && IsFinder && selectDataList.Values.Count == 0)
                {
                    CustomerID = Guid.Empty;
                }
            }
            #endregion

            #region 客户
            if (e.Column == colCustomerName)
            {
                if (LinkClickedEvent != null && CurrentRow != null)
                {
                    LinkClickedEvent(CurrentRow.CustomerName, null);
                }
            }
            #endregion
        }

        /// <summary>
        /// 验证客户与之前选择的是否一样
        /// </summary>
        private bool ValidateCustomer
        {
            get
            {
                if (selectDataList == null || selectDataList.Count == 0)
                {
                    return true;
                }

                if (CurrentRow == null)
                {
                    return true;
                }
           
                int i = 0;
                Guid customerID = selectDataList.Values.First().CustomerID;
                if (customerID != CurrentRow.CustomerID)
                {
                    List<Guid> combineCustomerIDList = CustomerService.GetCombineCustomerIDList(customerID);
                    if (combineCustomerIDList.Count > 1)
                    {
                        if (!combineCustomerIDList.Contains(CurrentRow.CustomerID))
                        {
                            i = 1;
                        }
                    }
                    else
                    {
                        List<Guid> iDList = CustomerService.GetCombineCustomerIDList(CurrentRow.CustomerID);
                        if (iDList.Count > 1)
                        {
                            if (!iDList.Contains(customerID))
                            {
                                i = 1;
                            }
                        }
                        else
                        {
                            i = 1;
                        }
                    }
                }                         

                //int i = (from d in selectDataList.Values where d.CustomerID != CurrentRow.CustomerID select d.ID).Count();
                if (i > 0)
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109070004"));
                    return false;
                }

                return true;
            }
        }
        /// <summary>
        /// 验证公司与之前选择的是否一样
        /// </summary>
        private bool ValidateCompany
        {
            get
            {
                if (selectDataList == null || selectDataList.Count == 0)
                {
                    return true;
                }
                if (CurrentRow == null)
                {
                    return true;
                }
                int i = (from d in selectDataList.Values where d.CompanyID != CurrentRow.CompanyID select d.ID).Count();
                if (i > 0)
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109070005"));
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 验证币种与之前选择的是否一样
        /// </summary>
        private bool ValidateCurrency
        {
            get
            {
                if (selectDataList == null || selectDataList.Count == 0)
                {
                    return true;
                }
                if (CurrentRow == null)
                {
                    return true;
                }
                int i = (from d in selectDataList.Values where d.CurrencyID != CurrentRow.CurrencyID select d.ID).Count();
                if (i > 0)
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109070006"));
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region 多选列表中移除与清空
        /// <summary>
        /// 多选列表中、移除或清空已选择的列表时，刷新主列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public void MultiListRemoveData(object sender, object data)
        {
            if (sender.ToString() == "Remove")
            {
                string removeID = data.ToString();

                var items = (from w in DataList where w.CurrentID == removeID select w);

                foreach (var itme in items)
                {
                    CurrencyBillList writeOffItem = itme as CurrencyBillList;
                    if (writeOffItem != null)
                    {
                        writeOffItem.Selected = false;
                        bsList.ResetBindings(false);
                    }
                }

                if (selectDataList.Keys.Contains(removeID))
                {
                    selectDataList.Remove(removeID);
                }
            }
            else if (sender.ToString() == "Clear")
            {
                selectDataList.Clear();

                DataList.ForEach(o => o.Selected = false);

                bsList.ResetBindings(false);
            }
            else
            {
                return;
            }
        }

        #endregion

        #region 统计
        /// <summary>
        /// 统计信息
        /// </summary>
        private void TotalAmount()
        {
            txtCR.Text = string.Empty;
            txtDR.Text = string.Empty;
            txtProfit.Text = string.Empty;


            #region 统计应收&应付
            //List<BillTotalInfo> list = (from d in DataList
            //                            group d by new { d.CurrencyID, d.CurrencyName, d.Way }
            //                                into g
            //                                select new BillTotalInfo
            //                                {
            //                                    CurrencyID = g.Key.CurrencyID,
            //                                    CurrencyName = g.Key.CurrencyName,
            //                                    Way = g.Key.Way,
            //                                    Amount = g.Sum(p => p.Amount)
            //                                }
            //                            ).ToList();

            //foreach (BillTotalInfo totalInfo in list)
            //{
            //if (totalInfo.Way == FeeWay.AP)
            //    {
            //        this.txtCR.Text += totalInfo.CurrencyName + ": " + totalInfo.Amount.ToString("n") + System.Environment.NewLine;
            //    }
            //    else if (totalInfo.Way == FeeWay.AR)
            //    {
            //        this.txtDR.Text += totalInfo.CurrencyName + ": " + totalInfo.Amount.ToString("n") + System.Environment.NewLine;
            //    }
            //}
            string txtDRText = string.Empty;
            string txtCRText = string.Empty;
            string txtProfitText = string.Empty;
            foreach (BillListTotalInfo totalInfo in _totalInfoList)
            {
                txtDRText += totalInfo.CurrencyName + ": " + totalInfo.DRAmount.ToString("n") + Environment.NewLine;
                txtCRText += totalInfo.CurrencyName + ": " + totalInfo.CRAmount.ToString("n") + Environment.NewLine;
                txtProfitText += totalInfo.CurrencyName + ": " + totalInfo.Balance.ToString("n") + Environment.NewLine;
            }

            txtDR.Text = txtDRText;
            txtCR.Text = txtCRText;
            txtProfit.Text = txtProfitText;

            #endregion

            #region 统计利润

            //ListProfit = (from l in list
            //              group l by new { l.CurrencyID, l.CurrencyName }
            //                  into g
            //                  select new BillTotalInfo
            //                  {
            //                      CurrencyID = g.Key.CurrencyID,
            //                      CurrencyName = g.Key.CurrencyName,
            //                      Amount = g.Sum(p => p.Amount)
            //                  }
            //                                      ).ToList();

            //foreach (BillTotalInfo totalInfo in ListProfit)
            //{
            //    this.txtProfit.Text += totalInfo.CurrencyName + ": " + totalInfo.Amount.ToString("n") + System.Environment.NewLine;
            //}

            TotalProfitByCurrency();

            #endregion
        }

        /// <summary>
        /// 根据选择的币种统计利润
        /// </summary>
        private void TotalProfitByCurrency()
        {
            txtTotalByCurrency.Text = "0";
            if (cmbCurrency.EditValue == null || (Guid)cmbCurrency.EditValue == Guid.Empty)
            {
                return;
            }

            Guid currencyID = (Guid)cmbCurrency.EditValue;
            decimal amount = 0;
            //foreach (BillTotalInfo item in ListProfit)
            //{
            //    if (Utility.GuidIsNullOrEmpty(item.CurrencyID))
            //    {
            //        continue;
            //    }
            //    if (currencyID == item.CurrencyID)
            //    {
            //        amount += item.Amount;
            //    }
            //    else
            //    {
            //        decimal rate = RateHelper.GetRate(item.CurrencyID, currencyID, DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified), RateList);
            //        if (rate > 0)
            //        {
            //            amount += item.Amount * rate;
            //        }
            //    }
            //}

            foreach (BillListTotalInfo item in _totalInfoList)
            {
                if (FAMUtility.GuidIsNullOrEmpty(item.CurrencyID))
                {
                    continue;
                }

                if (currencyID == item.CurrencyID)
                {
                    amount += item.Balance;
                }
                else
                {
                    decimal rate = RateHelper.GetRate(item.CurrencyID, currencyID, DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified), RateList);
                    if (rate > 0)
                    {
                        amount += item.Balance * rate;
                    }
                }
            }

            txtTotalByCurrency.Text = amount.ToString("n");
        }

        private void cmbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            TotalProfitByCurrency();
        }

        #endregion

        #region 热键
        public new event KeyEventHandler KeyDown;

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
            {
                //Workitem.Commands[BillCommandConstants.Command_EditData].Execute();
            }
            else if (KeyDown != null
               && e.KeyCode == Keys.F5
               && gvMain.FocusedColumn != null
               && gvMain.FocusedValue != null)
            {
                string text = gvMain.GetFocusedDisplayText();
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add(gvMain.FocusedColumn.FieldName, text);
                KeyDown(keyValue, null);
            }
            if (e.KeyCode == Keys.F6 && CurrentRow != null)
            {
                Workitem.Commands[BillCommandConstants.Command_ShowSearch].Execute();
            }
        }
        #endregion

        #region 分页

        private void pageControl1_PageChanged(object sender, PageChangedEventArgs e)
        {
            if (InvokeGetData != null)
            {
                _dataPageInfo.CurrentPage = e.CurrentPage;
                InvokeGetData(this, _dataPageInfo);
            }
        }

        private void gvMain_CustomerSorting(object sender, SortingCancelEventArgs e)
        {
            if (DataList == null || DataList.Count == 0)
            {
                return;
            }
            if (InvokeGetData != null)
            {
                e.Cancel = true;
                if (e.Column == colSelected)
                {
                    return;
                }
                _dataPageInfo.SortByName = e.Column.FieldName;
                if (e.ColumnSortOrder == ColumnSortOrder.Ascending ||
                    e.ColumnSortOrder == ColumnSortOrder.None)
                {
                    _dataPageInfo.SortOrderType = SortOrderType.Desc;
                }
                else
                {
                    _dataPageInfo.SortOrderType = SortOrderType.Asc;
                }
                InvokeGetData(this, _dataPageInfo);
            }
        }
        #endregion

        #region 刷新列表
        /// <summary>
        /// 刷新列表数据
        /// </summary>
        /// <param name="data"></param>
        public void RefreshUI(object operatingType, object data)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (operatingType.ToString() == "Check" || operatingType.ToString() == "Invoice")
                {
                    #region 销账与开发票

                    List<Guid> idList = data as List<Guid>;
                    if (idList == null || idList.Count == 0)
                    {
                        return;
                    }
                    //先查询出所有更新的纪录
                    List<CurrencyBillList> newList = FinanceService.GetBillListByIds(idList.ToArray(), LocalData.IsEnglish);
                    foreach (CurrencyBillList bill in newList)
                    {
                        //根据查询出的数据，找到列表中对应的数据

                        CurrencyBillList orgist = DataList.Find(delegate(CurrencyBillList item) { return item.CurrentID == bill.CurrentID; });

                        if (orgist != null)
                        {

                            FAMUtility.CopyToValue(bill, orgist, typeof(CurrencyBillList));
                        }
                    }

                    bsList.ResetBindings(false);

                    #endregion

                }
                else
                {
                    #region 审核与取消审核
                    ManyResult result = data as ManyResult;

                    if (result == null)
                    {
                        return;
                    }
                    foreach (var item in result.Items)
                    {
                        Guid id = item.GetValue<Guid>("ID");
                        DateTime? updateDate = item.GetValue<DateTime?>("UpdateDate");

                        List<CurrencyBillList> list = (from d in DataList where d.ID == id select d).ToList();

                        foreach (CurrencyBillList bill in list)
                        {
                            bill.UpdateDate = updateDate;

                            switch (operatingType.ToString())
                            {
                                case "Auditor":
                                    bill.State = BillState.Approved;
                                    bill.Checked = true;
                                    bill.CheckBy = LocalData.UserInfo.LoginName;
                                    bill.CheckDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                                    break;
                                case "UnAuditor":
                                    bill.State = BillState.Created;
                                    bill.Checked = false;
                                    bill.CheckBy = string.Empty;
                                    bill.CheckDate = null;
                                    break;
                            }
                        }
                        bsList.ResetBindings(false);
                        gvMain.BestFitColumns();
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region 打开任务中心
        private void barOpenTaskCenter_ItemClick(object sender, ItemClickEventArgs e)
        {
            OperTaskCenter();
        }
        private void OperTaskCenter()
        {
            BusinessOperationContext operationContext = new BusinessOperationContext();
            operationContext.OperationType = OperationType.Unknown;
            if (CurrentRow != null)
            {
                operationContext.OperationID = CurrentRow.OperationID;
                operationContext.OperationNO = CurrentRow.OperationNO;
                operationContext.OperationType = CurrentRow.OperType;
            }

            ICPCommonOperationService.OpenTaskCenter(operationContext);
        }
        #endregion

        #region ConvertBillFromARToDN
        /// <summary>
        /// 应收账单转换成代理账单
        /// </summary>
        void ConvertBillFromARToDN()
        {
            if(CurrentRow!=null)
            {
                SaveRequestBillConvert saveRequest = new SaveRequestBillConvert()
                {
                    BillID = CurrentRow.ID,
                    SaveByID = LocalData.UserInfo.LoginID,
                    UpdateDate = CurrentRow.UpdateDate,
                };
               SingleResult result= FinanceService.ConvertBillFromARToDN(saveRequest);
                if(result!=null)
                {
                    CurrentRow.UpdateDate = result.GetValue<DateTime>("UpdateDate");
                }
            }
        }
        #endregion


    }

   

    [ToolboxItem(false)]
    public class BillFinderListPart : BillListPart
    {
        protected override bool IsFinder { get { return true; } }

        bool _IsMulti = false;

        public override object DataSource
        {
            get
            {
                if (_IsMulti)
                {
                    List<CurrencyBillList> list = bsList.DataSource as List<CurrencyBillList>;
                    if (list != null)
                    {
                        return list.FindAll(delegate(CurrencyBillList item) { return item.Selected; });
                    }
                    else
                        return null;
                }
                else
                {
                    return base.Current;
                }
            }
            set
            {
                base.DataSource = value;
            }
        }

        public override void Init(IDictionary<string, object> values)
        {

            if (values == null) return;

            if (values.ContainsKey("IsValidateCustomer"))
            {

                IsValidateCustomer = (bool)values["IsValidateCustomer"];
            }
            if (values.ContainsKey("CustomerID"))
            {
                if (values["CustomerID"] != null && !string.IsNullOrEmpty(values["CustomerID"].ToString()))
                {
                    CustomerID = new Guid(values["CustomerID"].ToString());
                }
            }
            if (values.ContainsKey("IsValidateCompany"))
            {
                IsValidateCompany = (bool)values["IsValidateCompany"];
            }
            if (values.ContainsKey("CompanyID"))
            {
                if (values["CompanyID"] != null && !string.IsNullOrEmpty(values["CompanyID"].ToString()))
                {
                    CompanyID = new Guid(values["CompanyID"].ToString());
                }
            }
            if (values.ContainsKey("IsInvoiceSearch"))
            {
                if (values["IsInvoiceSearch"] != null)
                {
                    IsInvoiceSearch = bool.Parse(values["IsInvoiceSearch"].ToString());
                }
            }
            if (values.ContainsKey("IsMulti"))
            {
                _IsMulti = (bool)values["IsMulti"];
                //单选的搜索器需在此实现一下
            }

            //if (values.ContainsKey("BillProgram"))
            //{
            //    Billprogram = (BillProgram)values["BillProgram"];
            //}
        }
    }
}
