using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using DevExpress.XtraGrid.Views.Base;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.FAM.UI.CustomerBill.Print;
using ICP.Framework.ClientComponents.Service;
using System.Drawing;
using DevExpress.XtraBars;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Common.ServiceInterface.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents;

namespace ICP.FAM.UI.CustomerBill
{
    [ToolboxItem(false)]
    public partial class CustomerBillListPart : BaseListPart
    {
        #region 服务
        /// <summary>
        /// 
        /// </summary>
        RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
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
        /// <summary>
        /// 
        /// </summary>
        IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        BillControllerFactory BillControllerFactory
        {
            get
            {
                return ClientHelper.Get<BillControllerFactory, BillControllerFactory>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        RefereshBillService RefereshBillService
        {
            get
            {
                return ClientHelper.Get<RefereshBillService, RefereshBillService>();
            }
        }

        ///<summary>
        /// FCM海出
        ///</summary>
        IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }

        #endregion

        #region Init

        public CustomerBillListPart()
        {
            InitializeComponent();
            if (DesignMode) return;
            Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode)
            {
                return;
            }
            InitMessage();

            barAdditionalWF.Visibility = BarItemVisibility.Never;

            #region currency
            foreach (var item in _CurrencyList)
            {
                cmbProfitCurrency.Properties.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.CurrencyID));
            }
            cmbProfitCurrency.SelectedIndexChanged += delegate { RefreshBillProfit(); };
            SolutionCurrencyList tager = _CurrencyList.Find(delegate(SolutionCurrencyList item) { return item.CurrencyID == _ConfigureInfo.DefaultCurrencyID; });
            if (tager != null)
                cmbProfitCurrency.SelectedIndex = _CurrencyList.IndexOf(tager);
            #endregion

            //State
            List<EnumHelper.ListItem<BillState>> billStates = EnumHelper.GetEnumValues<BillState>(LocalData.IsEnglish);
            foreach (var item in billStates)
            {
                rcmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            //BillType
            List<EnumHelper.ListItem<BillType>> billTypes = EnumHelper.GetEnumValues<BillType>(LocalData.IsEnglish);
            foreach (var item in billTypes)
            {
                if (item.Value == BillType.None) continue;
                cmbType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value, (short)item.Value));
            }

            bsBillList.ResetBindings(false);
            RefreshBillBarEnabled();
        }
        private void InitMessage()
        {
            RegisterMessage("DeleteBillErrorByState", LocalData.IsEnglish ? "Failed to delete, delete only the state [ created ] bill" : "删除失败,只能删除状态为[已创建]的账单");
            RegisterMessage("DeleteBillErrorByType", LocalData.IsEnglish ? "Failed to delete, cannot be removed by contract generation bill" : "删除失败,无法删除由合约生成的账单");
            RegisterMessage("DeleteBillErrorByAgent", LocalData.IsEnglish ? "Failed to delete, cannot be removed by contract generation bill" : "删除失败,账单中包含签收代理修改的账单！");
        }
        #endregion

        #region 本地变量/属性
        /// <summary>
        /// 
        /// </summary>
        OperationCommonInfo _OperationCommonInfo = null;
        /// <summary>
        /// 
        /// </summary>
        ConfigureInfo _ConfigureInfo = null;
        /// <summary>
        /// 
        /// </summary>
        List<SolutionExchangeRateList> _RateList = null;
        /// <summary>
        /// 
        /// </summary>
        List<SolutionCurrencyList> _CurrencyList = null;
        /// <summary>
        /// 
        /// </summary>
        AgentBillCheckStatusEnum _AgentBillCheckStatusEnum = AgentBillCheckStatusEnum.None;
        /// <summary>
        /// 
        /// </summary>
        BillList CurrentRow
        {
            get
            {
                if (bsBillList.Current == null) return null;
                else return bsBillList.Current as BillList;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        List<BillList> SelectedBillList
        {
            get
            {
                GridCell[] cells = gvBillList.GetSelectedCells();
                List<int> rowIndexs = new List<int>();
                foreach (var item in cells)
                {
                    if (rowIndexs.Contains(item.RowHandle) == false)
                        rowIndexs.Add(item.RowHandle);
                }

                List<BillList> tagers = new List<BillList>();
                foreach (var item in rowIndexs)
                {
                    BillList data = gvBillList.GetRow(item) as BillList;
                    if (data != null) tagers.Add(data);
                }

                if (tagers.Count == 0) { if (CurrentRow != null) tagers.Add(CurrentRow); }

                return tagers;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        List<BillList> DataList
        {
            get
            {
                List<BillList> list = bsBillList.DataSource as List<BillList>;
                if (list == null)
                {
                    list = new List<BillList>();
                }
                return list;
            }
        }
        /// <summary>
        /// 操作类型
        /// </summary>
        public OperationType OperationType
        {
            get;
            set;
        }
        /// <summary>
        /// 编辑框
        /// </summary>
        public CustomerBillEditPart CustomerBillEditPart
        {
            get;
            set;
        }
        #endregion

        #region Control Event
        #region BarItem Click
        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            //AddData();
        }
        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteData();
        }
        private void barPrintBill_ItemClick(object sender, ItemClickEventArgs e)
        {
            PrintBill();
        }
        private void barPrintCombinCharge_ItemClick(object sender, ItemClickEventArgs e)
        {
            PrintCombinCharge();
        }
        private void barPritnFeeList_ItemClick(object sender, ItemClickEventArgs e)
        {
            PritnFeeList();
        }
        private void barAdditionalWF_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[CustomerBillCommands.Commond_AdditionalWF].Execute();
        }
        private void barPayoffWF_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[CustomerBillCommands.Commond_PayoffWF].Execute();
        }
        private void barDeficit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[CustomerBillCommands.Commond_Deficit].Execute();
        }
        private void barAddBillFromHistory_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddBillFromHistory();
        }
        private void barAddLocalFee_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddLocalFee();
        }
        private void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            RefreshData();
        }
        private void barShowTotal_ItemClick(object sender, ItemClickEventArgs e)
        {
            panelListTotal.Visible = !panelListTotal.Visible;
        }
        private void barAddAR_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddData(BillType.AR);
        }
        private void barAddAP_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddData(BillType.AP);
        }
        private void barAddDC_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddData(BillType.DC);
        }
        /// <summary>
        /// 重新生成账单
        /// </summary>
        private void ToRegenerateBill_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                SingleResult result = new SingleResult();
                if (_OperationCommonInfo != null && _OperationCommonInfo.OperationID != Guid.Empty && _OperationCommonInfo.OperationType == OperationType.OceanExport)
                {
                    result = OceanExportService.CreateBill(_OperationCommonInfo.OperationID, LocalData.UserInfo.LoginID);
                }
                else
                {
                    return;
                }
                if (result == null || string.IsNullOrEmpty(result.GetValue<string>("message")))
                {
                    RefreshData();
                    Framework.ClientComponents.Controls.Utility.ShowMessage("重新生成账单成功！");
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), NativeLanguageService.GetText(this, result.GetValue<string>("message")));
                    return;
                }
            }
        }

        private void barRegenerateDHF_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                SingleResult result = new SingleResult();
                if (_OperationCommonInfo != null && _OperationCommonInfo.OperationID != Guid.Empty && _OperationCommonInfo.OperationType == OperationType.OceanExport)
                {
                    result = FinanceService.SaveRegenerateDeliveryHandlingFee(_OperationCommonInfo.OperationID, LocalData.UserInfo.LoginID);
                }else
                {
                    return;
                }
                if (result == null || result.GetValue<bool>("State"))
                {
                    RefreshData();
                    Framework.ClientComponents.Controls.Utility.ShowMessage("重新生成账单成功！");
                }
            }
        }

        /// <summary>
        /// 打印发票合同
        /// </summary>
        private void barInvoiceContract_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (SelectedBillList == null || SelectedBillList.Count == 0)
            {
                return;
            }
            Guid[] ids = (from d in SelectedBillList select d.ID).ToArray();

            InvoiceFreeReportData baseReport = new InvoiceFreeReportData();
            baseReport = FinanceService.GetInvoiceContractReportt(ids, Guid.Empty);
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
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }
        #endregion

        #region GridView Event
        private void gvBillList_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            BillList list = gvBillList.GetRow(e.RowHandle) as BillList;
            if (list == null || list.Type == BillType.DC)
            {
                e.Appearance.ForeColor = Color.FromArgb(0, 0, 155);
                e.Appearance.Options.UseForeColor = true;
            }
        }
        private void gvMain_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
        }

        private void bsBillList_PositionChanged(object sender, EventArgs e)
        {

            RefreshBillBarEnabled();
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }
        private void gcBillList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.Alt == false)
            {
                switch (e.KeyCode)
                {
                    case Keys.F2:
                        AddData(BillType.AR);
                        break;
                    case Keys.F3:
                        AddData(BillType.AP);
                        break;
                    case Keys.F4:
                        AddData(BillType.DC);
                        break;
                }
            }
        }
        #endregion 
        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsBillList.Current; }
        }

        public override object DataSource
        {
            get
            {
                return bsBillList.DataSource;
            }
            set
            {
                bsBillList.DataSource = value;
                bsBillList.ResetBindings(false);

                SortBillList(value as List<BillList>);
                gvBillList.BestFitColumns();

                RefreshBillProfit();

                if (CurrentChanged != null) CurrentChanged(this, Current);

                FitHeight();
                gvBillList.Focus();

            }
        }

        private void SortBillList(List<BillList> billList)
        {
            if (billList == null || billList.Count <= 1) return;

            //bug2849: 如果账单总数>=20，则按创建日期降序排序，否则按应付、应收、代理账单类型排序 
            if (billList.Count < 20)
            {
                gvBillList.SortInfo.AddRange(new GridColumnSortInfo[] {
            new GridColumnSortInfo(colBizType, ColumnSortOrder.Ascending)});
            }
            else
            {
                gvBillList.SortInfo.AddRange(new GridColumnSortInfo[] {
            new GridColumnSortInfo(colCreateDate, ColumnSortOrder.Descending)});
            }
        }

        public override void Refresh(object items)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<BillList> list = DataSource as List<BillList>;
                if (list == null) return;

                BillList newLists = items as BillList;

                BillList currentRow = CurrentRow;
                if (currentRow != null)
                {
                    currentRow.CurrencyAmounts = new List<CurrencyAmountData>();
                    FAMUtility.CopyToValue(newLists, currentRow, typeof(BillList));
                }

                bsBillList.ResetBindings(false);
                RefreshBillBarEnabled();
                RefreshBillProfit();
            }
        }
        public override void RemoveItem(int index)
        {
            bsBillList.RemoveAt(index);
            RefreshBillProfit();
            FitHeight();
            if (CurrentChanged != null)
            {
                CurrentChanged(this, Current);
            }
        }

        public override void RemoveItem(object item)
        {
            bsBillList.Remove(item);
            RefreshBillProfit();
            FitHeight();
            if (CurrentChanged != null)
            {
                CurrentChanged(this, Current);
            }
        }

        public override event CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region IPart 成员
        
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "BLCommonInfo")
                {
                    _OperationCommonInfo = item.Value as OperationCommonInfo;
                    txtOperationNo.Text = _OperationCommonInfo.OperationNo;
                }
                else if (item.Key == "ConfigureInfo")
                {
                    _ConfigureInfo = item.Value as ConfigureInfo;
                }
                else if (item.Key == "SolutionExchangeRateList")
                {
                    _RateList = item.Value as List<SolutionExchangeRateList>;
                }
                else if (item.Key == "SolutionCurrencyList")
                {
                    _CurrencyList = item.Value as List<SolutionCurrencyList>;
                }
                else if (item.Key == "AgentBillCheckStatus")
                {
                    _AgentBillCheckStatusEnum = (AgentBillCheckStatusEnum)Enum.Parse(typeof(AgentBillCheckStatusEnum), item.Value.ToString());
                }

            }
        }
        #endregion

        #region 本地方法
        /// <summary>
        /// 刷新利润
        /// </summary>
        void RefreshBillProfit()
        {
            try
            {
                Dictionary<Guid, decimal> dic = new Dictionary<Guid, decimal>();
                #region 构建币种字典
                List<BillList> billlist = bsBillList.DataSource as List<BillList>;
                //用于统计增值税
                Decimal vat = 0m;
                foreach (var item in billlist)
                {
                    if (FAMUtility.GuidIsNullOrEmpty(item.ID) || item.CurrencyAmounts == null || item.CurrencyAmounts.Count == 0) continue;
                    foreach (var fItem in item.CurrencyAmounts)
                    {
                        if (dic.Keys.Contains(fItem.CurrencyID) == false) dic.Add(fItem.CurrencyID, 0m);
                        dic[fItem.CurrencyID] += fItem.Amount;
                    }

                    if (item.VATAmout != null && item.VATAmout != Decimal.Zero)
                    {
                        vat += (decimal)item.VATAmout;
                    }
                }
                #endregion
                #region 构建合计字符串
                StringBuilder strbulider = new StringBuilder();
                foreach (var item in dic)
                {
                    if (strbulider.Length > 0) strbulider.Append(" ");

                    strbulider.Append(RateHelper.GetCurrencyNameByCurrencyID(item.Key));
                    strbulider.Append(":" + item.Value.ToString("N"));
                }
                txtTotal.Text = strbulider.ToString();
                #endregion
                string stprofitCurrencyID = "" + cmbProfitCurrency.EditValue;
                Guid standardCurrencyID = _ConfigureInfo.StandardCurrencyID;
                Guid profitCurrencyID = stprofitCurrencyID.IsNullOrEmpty() ? standardCurrencyID : stprofitCurrencyID.ToGuid();
                decimal profitAmount = 0m;
                decimal adjustAmount = 0m;
                decimal ecCostAmount = 0m;
                decimal vatAmount = 0m;
                foreach (var item in dic)
                {
                    profitAmount += RateHelper.GetAmountByRate(
                            RateHelper.GetAmountByRate(item.Value, item.Key, standardCurrencyID, _RateList, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified))
                            , standardCurrencyID, profitCurrencyID, _RateList, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                }

                vatAmount = RateHelper.GetAmountByRate(vat, _ConfigureInfo.StandardCurrencyID, profitCurrencyID, _RateList, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                //调整费用
                if (_OperationCommonInfo.AdjustmentAmount != 0 && _OperationCommonInfo.AdjustmentCurrencyID != null)
                {
                    adjustAmount = RateHelper.GetAmountByRate(_OperationCommonInfo.AdjustmentAmount, _OperationCommonInfo.AdjustmentCurrencyID.Value, profitCurrencyID, _RateList,
                        DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                }
                //电商成本
                if (_OperationCommonInfo.ECommerceAmount != 0 && _OperationCommonInfo.ECommerceCurrencyID != null)
                {
                    ecCostAmount = RateHelper.GetAmountByRate(_OperationCommonInfo.ECommerceAmount, _OperationCommonInfo.ECommerceCurrencyID.Value, profitCurrencyID, _RateList,
                        DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                }

                StringBuilder stadjustment = new StringBuilder();
                //拼接调整费用
                if (_OperationCommonInfo.AdjustmentAmount != 0)
                {
                    stadjustment.Append(LocalData.IsEnglish ? "Ratio:" : "配比:");
                    stadjustment.Append(_OperationCommonInfo.AdjustmentCurrencyName);
                    stadjustment.Append(":" + _OperationCommonInfo.AdjustmentAmount.ToString("N"));
                    stadjustment.Append(" ");
                }

                //拼接电商成本
                if (_OperationCommonInfo.ECommerceAmount != 0)
                {
                    stadjustment.Append(LocalData.IsEnglish ? "EC:" : "电商:");
                    stadjustment.Append(_OperationCommonInfo.ECommerceCurrencyName);
                    stadjustment.Append(":" + _OperationCommonInfo.ECommerceAmount.ToString("N"));
                    stadjustment.Append(" ");
                }
                //拼接增值税
                if (vat != 0)
                {
                    stadjustment.Append(LocalData.IsEnglish ? "VAT:" : "增值税:");
                    stadjustment.Append(_ConfigureInfo.StandardCurrency);
                    stadjustment.Append(":" + vat.ToString("N"));
                }
                txtAdjustmentFee.Text = stadjustment.ToString();

                txtProfitByCurrency.Text = profitAmount.ToString("N");
                //添加利润为负数时显示红色 joe 2014-01-01
                if (profitAmount < 0)
                {
                    txtProfitByCurrency.ForeColor = Color.Red;
                    txtProfitByCurrency.Font = new Font("Tahoma", 9F, FontStyle.Bold);
                }
                else
                {
                    txtProfitByCurrency.ForeColor = Color.Black;
                    txtProfitByCurrency.Font = new Font("Tahoma", 9F);
                }
                txtnoVatProfit.Text = cmbProfitCurrency.Text + ":" + (profitAmount - adjustAmount - ecCostAmount - vatAmount).ToString("N");
            }
            catch (Exception ex)
            {
                txtnoVatProfit.Text = (LocalData.IsEnglish ? "Refresh Profit Failly." : "刷新利润失败.") + ex.Message;
                txtTotal.Text = (LocalData.IsEnglish ? "Refresh Profit Failly." : "刷新利润失败.") + ex.Message;
                txtProfitByCurrency.Text = (LocalData.IsEnglish ? "Refresh Profit Failly." : "刷新利润失败.") + ex.Message;
            }
        }
        void RefreshBillBarEnabled()
        {
            barAdd.Enabled = true;
            barAddBillFromHistory.Enabled = true;
            barDeficit.Enabled = true;
            barDelete.Enabled = true;
            barPayoffWF.Enabled = true;
            barSubPrint.Enabled = true;

            if (CurrentRow == null || FAMUtility.GuidIsNullOrEmpty(CurrentRow.ID))
            {
                barDeficit.Enabled = false;
                barPayoffWF.Enabled = false;
                barSubPrint.Enabled = false;
            }
            else
            {
                if (CurrentRow.Type == BillType.AR) barPayoffWF.Enabled = false;

                barSubPrint.Enabled = true;
            }


            if (_AgentBillCheckStatusEnum == AgentBillCheckStatusEnum.Checking
                || _AgentBillCheckStatusEnum == AgentBillCheckStatusEnum.StartCheck
                || DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified) < _ConfigureInfo.ChargingClosingdate)
            {
                barAdd.Enabled = false;
                barAddBillFromHistory.Enabled = false;
                barDelete.Enabled = false;

                if (DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified) < _ConfigureInfo.ChargingClosingdate)
                {
                    string message = LocalData.IsEnglish ? "" : "当前日期在计费关账期[" + _ConfigureInfo.ChargingClosingdate.Value.ToShortDateString() + "]内 , 不允许更改帐单";
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);
                }
                else
                {
                    string message = LocalData.IsEnglish ? "The current bill is reconciled, the bill can not be modified" : "当前账单正在对账中,无法对账单进行修改";
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);
                }

            }
            else
            {
                barAdd.Enabled = barAddBillFromHistory.Enabled = true;
                if (CurrentRow == null || (short)CurrentRow.State >= (short)BillState.Approved) barDelete.Enabled = false;
                //else
                //    barDelete.Enabled = true;
            }

            //if (CurrentRow != null)
            //{
            //    if (CurrentRow.Type == BillType.DC && this.OperationType == Framework.CommonLibrary.Common.OperationType.OceanImport)
            //    {
            //        barDelete.Enabled = false;
            //    }
            //}

        }
        void AddBillFromHistory()
        {
            AddBillFromHistoryPart addbill = Workitem.Items.AddNew<AddBillFromHistoryPart>();

            Dictionary<string, object> keyValue = new Dictionary<string, object>();
            keyValue.Add("BLCommonInfo", _OperationCommonInfo);
            keyValue.Add("ConfigureInfo", _ConfigureInfo);
            keyValue.Add("SolutionExchangeRateList", _RateList);
            keyValue.Add("SolutionCurrencyList", _CurrencyList);

            addbill.Init(keyValue);

            if (PartLoader.ShowDialog(addbill
                , LocalData.IsEnglish ? "Select Bill From History Business" : "从历史业务中选择帐单"
                , FormBorderStyle.Sizable) != DialogResult.OK) return;

            RefreshData();

        }
        void AddLocalFee()
        {
            AddLocalFee addbill = Workitem.Items.AddNew<AddLocalFee>();

            addbill.SetOperation(_OperationCommonInfo);

            if (PartLoader.ShowDialog(addbill
                , LocalData.IsEnglish ? "Add LocalFee" : "新增本地费用"
                , FormBorderStyle.Sizable) != DialogResult.OK) return;

            RefreshData();

        }
        /// <summary>
        /// 新增账单时，如果其它账单已全部审核，则提示：“因为其它账单已全部审核，系统将新增的账单通知计费部门。”
        /// </summary>
        void NotifyAssessor()
        {
            List<BillList> source = bsBillList.DataSource as List<BillList>;
            if (source != null && source.Count > 0)
            {
                bool needNotify = true;
                foreach (var item in source)
                {
                    if ((short)item.State <= (short)BillState.Created)
                    {
                        needNotify = false;
                        break;
                    }
                }
                if (needNotify)
                {
                    MessageBoxService.ShowInfo("因为其它账单已全部审核，系统将新增的账单通知计费部门。");
                    //TO DO:3.5任务管理器中实现通知功能
                }
            }
        }
        void DeleteData()
        {
            List<BillList> tager = SelectedBillList;
            if (tager == null || tager.Count == 0) return;

            int n = (from d in tager where d.State != BillState.Created select d).Count();
            if (n > 0)
            {
                //删除失败，无法删除状态不为已创建的账单
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), NativeLanguageService.GetText(this, "DeleteBillErrorByState"));
                return;
            }

            List<Guid> idList = (from d in tager select d.ID).ToList();
            if (idList != null && idList.Count > 0)
            {
                List<ChargeList> chargeList = FinanceService.GetChargeList(idList.ToArray());

                if (chargeList != null && chargeList.Count > 0)
                {
                    int m = (from d in chargeList where d.Type == FeeType.Price || d.Type==FeeType.Rebate select d).Count();
                    if (m > 0)
                    {
                        //删除失败，无法删除由合约产生的账单 
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), NativeLanguageService.GetText(this, "DeleteBillErrorByType"));
                        return;
                    }


                    int a = 0;
                    foreach (var item in chargeList)
                    {
                        if (item.FromType != null && ((item.FromType == 1 && OperationType == OperationType.OceanImport) ||
                            (item.FromType == 2 && OperationType == OperationType.OceanExport))
                            )
                        {
                            a = 1;
                        }
                    }

                    if (a > 0)
                    {
                        //删除失败，无法删除由合约产生的账单 
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), NativeLanguageService.GetText(this, "DeleteBillErrorByAgent"));
                        return;
                    }
                }
            }
            if (FAMUtility.EnquireIsDeleteCurrentData() == false) return;

            bsBillList.PositionChanged -= new EventHandler(bsBillList_PositionChanged);
            gvBillList.BeforeLeaveRow -= new RowAllowEventHandler(gvMain_BeforeLeaveRow);

            try
            {
                int result = -1;
                List<Guid> needRemoveIDs = new List<Guid>();
                StringBuilder needRemoveNos = new StringBuilder();

                List<DateTime?> updateDates = new List<DateTime?>();
                foreach (var item in tager)
                {
                    if (item.IsNew || needRemoveIDs.Contains(item.ID)) continue;

                    needRemoveIDs.Add(item.ID);
                    updateDates.Add(item.UpdateDate);

                    if (needRemoveNos.Length > 0) needRemoveNos.Append(",");
                    needRemoveNos.Append(item.No);
                }
                if (needRemoveIDs.Count != 0)
                    result = FinanceService.RemoveBillInfo(needRemoveIDs.ToArray(), LocalData.UserInfo.LoginID, updateDates.ToArray());

                List<BillList> list = bsBillList.DataSource as List<BillList>;
                foreach (var item in SelectedBillList)
                {
                    list.Remove(item);
                }
                bsBillList.DataSource = list;
                bsBillList.ResetBindings(false);
                //已成功删除账单[xxxxx]。”

                RefreshBillBarEnabled();
                if (CurrentChanged != null) CurrentChanged(this, CurrentRow);

                if (result == -1) LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), (LocalData.IsEnglish ? "Delete Successfully " : "已成功删除账单 ") + needRemoveNos.ToString());

                FAMUtility.ShowDispacthReviseResult(result, Workitem, _OperationCommonInfo.OperationID);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }

            gvBillList.BeforeLeaveRow += new RowAllowEventHandler(gvMain_BeforeLeaveRow);
            bsBillList.PositionChanged += new EventHandler(bsBillList_PositionChanged);
        }
        
        #region Print

        private void PrintBill()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<BillList> list = bsBillList.DataSource as List<BillList>;
                if (list == null || list.Count == 0) return;
                foreach (var item in list)
                {
                    if (item.IsNew) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "" : "请先保存数据."); return; }
                }

                IBillController controller = BillControllerFactory.GetBillController(_OperationCommonInfo.OperationType);
                controller.UpdatedBill += delegate (object o, ManyResult result)
                {
                    if (result != null && result.Items != null && result.Items.Count > 0 && DataSource != null)
                    {
                        List<BillList> bills = DataSource as List<BillList>;
                        if (bills != null)
                        {
                            foreach (var item in result.Items)
                            {
                                Guid id = item.GetValue<Guid>("ID");
                                BillList bill = bills.Find(b => b.ID == id);
                                if (bill != null) bill.UpdateDate = item.GetValue<DateTime?>("UpdateDate");
                            }
                            DataSource = bills;
                        }
                        RefreshBillBarEnabled();
                    }
                };
                controller.PrintBill(CurrentRow, CurrentRow.Type, _OperationCommonInfo, _ConfigureInfo, _RateList);

            }
        }
        private void PrintCombinCharge()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<BillList> list = bsBillList.DataSource as List<BillList>;
                if (list == null || list.Count == 0) return;
                foreach (var item in list)
                {
                    if (item.IsNew) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "" : "请先保存数据."); return; }
                }

                SelectBillPart selectBillPart = Workitem.Items.AddNew<SelectBillPart>();
                selectBillPart.DataSource = list;
                if (FAMUtility.ShowDialog(selectBillPart, LocalData.IsEnglish ? "Select Bill" : "选择帐单") != DialogResult.OK) return;

                List<BillList> combinBill = selectBillPart.DataSource as List<BillList>;
                if (combinBill == null || combinBill.Count == 0) return;

                List<BillList> selectBill = (from d in combinBill where d.Selected select d).ToList();
                if ((selectBill == null || selectBill.Count == 0) && CurrentRow != null)
                {
                    selectBill.Add(CurrentRow);
                }


                IBillController controller = BillControllerFactory.GetBillController(_OperationCommonInfo.OperationType);
                controller.UpdatedBill += delegate { RefreshData(); };
                controller.PrintCombinBill(selectBill, _OperationCommonInfo, _ConfigureInfo, _RateList);
            }
        }

        private void PritnFeeList()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<BillList> list = bsBillList.DataSource as List<BillList>;
                if (list == null || list.Count == 0) return;
                foreach (var item in list)
                {
                    if (item.IsNew) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "" : "请先保存数据."); return; }
                }
                IBillController controller = BillControllerFactory.GetBillController(_OperationCommonInfo.OperationType);
                controller.PrintFeeList(_OperationCommonInfo.OperationID);
            }
        }

        #endregion

        private void RefreshData()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<BillList> bills = FinanceService.GetBillListByOperactioID(_OperationCommonInfo.OperationID);
                DataSource = bills;
                RefreshBillBarEnabled();
            }
        }

        /// <summary>
        /// 自动调整行高
        /// </summary>
        void FitHeight()
        {
            int rowHeight = 22;
            int defaultHeight = 240;

            try
            {
                if (bsBillList != null && bsBillList.Count >= 1 && bsBillList.Count <= 6)
                {
                    ((SplitContainerControl)Parent.Parent.Parent).SplitterPosition = 130 + rowHeight * bsBillList.Count;
                }
                else
                {
                    ((SplitContainerControl)Parent.Parent.Parent).SplitterPosition = defaultHeight;
                }

            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 根据账单类型添加账单
        /// </summary>
        /// <param name="billType"></param>
        public void AddData(BillType billType)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //是否已关帐
                bool isAccountClose = false;
                if (CurrentRow != null && CurrentRow.IsNew) return;

                DateTime operationDate = Convert.ToDateTime(_OperationCommonInfo.OperationDate.ToShortDateString());
                DateTime chargingCloseDate = Convert.ToDateTime(_ConfigureInfo.ChargingClosingdate.Value.ToShortDateString());
                isAccountClose = operationDate < chargingCloseDate ? true : false;

                barDelete.Enabled = true;
                NotifyAssessor();


                BillList newData = new BillList
                {
                    CreateByID = LocalData.UserInfo.LoginID,
                    CreateByName = LocalData.UserInfo.LoginName,
                    CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified)
                };

                if (operationDate <= chargingCloseDate)
                {
                    newData.DueDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    newData.AccountDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                }
                else
                {
                    newData.DueDate = DateTime.SpecifyKind(operationDate, DateTimeKind.Unspecified);
                    newData.AccountDate = DateTime.SpecifyKind(operationDate, DateTimeKind.Unspecified);
                }

                newData.CompanyID = _OperationCommonInfo.CompanyID;
                newData.State = BillState.Created;
                newData.Type = billType;
                newData.FromType = (int)OperationType;
                newData.AgentID = _OperationCommonInfo.AgentID;

                if (billType == BillType.DC)
                {

                    foreach (var item in _OperationCommonInfo.Forms)
                    {
                        if (item.Type == FormType.HBL)
                        {
                            newData.FormNo = item.No;
                            break;
                        }

                    }

                    if (newData.FormNo == "")
                    {
                        foreach (var item in _OperationCommonInfo.Forms)
                        {

                            newData.FormNo = item.No;
                            break;
                        }
                    }

                }

                newData.EndEdit();
                gvBillList.BeforeLeaveRow -= gvMain_BeforeLeaveRow;
                gvBillList.SortInfo.Clear();
                bsBillList.Insert(0, newData);
                gvBillList.BeforeLeaveRow += gvMain_BeforeLeaveRow;

                if (gvBillList.FocusedRowHandle > 0)
                {

                    int rowhandle = gvBillList.GetRowHandle(0);
                    gvBillList.FocusedRowHandle = rowhandle;
                    gvBillList.ClearSelection();
                    gvBillList.SelectCell(rowhandle, gvBillList.Columns[0]);
                }


                FitHeight();

            }

        }
        #endregion

        
    }
}
