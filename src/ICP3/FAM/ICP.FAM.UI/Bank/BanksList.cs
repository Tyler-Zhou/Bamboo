using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class BanksList : BaseListPart
    {

        public BanksList()
        {
            InitializeComponent();
            Disposed += delegate {
                CurrentChanged = null;
                CurrentChanging = null;
                InvokeGetData = null;
                KeyDown = null;
                gcMain.DataSource = null;
                bsBanksList.CurrentChanged -= bsBanksList_CurrentChanged;
                bsBanksList.PositionChanged -= bsBanksList_PositionChanged;
                bsBanksList.DataSource = null;
                _dataPageInfo = null;
                bsBanksList.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            };
        }

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        #endregion

        #region 初始化 
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessage();
                InitControls();
            }
        }

        private void InitMessage()
        {
            RegisterMessage("1108100001", LocalData.IsEnglish ? "Are you sure to invalidate the selected time?" : "确认要作废该数据？");
            RegisterMessage("1108100002", LocalData.IsEnglish ? "Are you sure to resume the selected time?" : "确认要激活该数据？");
        }

        private void InitControls()
        {
            FAMUtility.ShowGridRowNo(gvMain);
        }

        #endregion

        #region IListPart 成员
        public override event InvokeGetDataHandler InvokeGetData;

        public override object Current
        {
            get { return bsBanksList.Current; }
        }
        protected BankList CurrentRow
        {
            get { return Current as BankList; }
        }

        public override object DataSource
        {
            get
            {
                return bsBanksList.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        public List<BankList> DataSourceList
        {
            get
            {
                List<BankList> list = DataSource as List<BankList>;
                if (list == null)
                {
                    list = new List<BankList>();
                }
                return list;
            }
        }

        DataPageInfo _dataPageInfo = new DataPageInfo();
        private void BindingData(object value)
        {
            PageList source = value as PageList;
            List<BankList> list = new List<BankList>();
            if (source != null) list = source.GetList<BankList>();
            if (source == null || list == null || list.Count == 0)
            {
                bsBanksList.DataSource = list;
                UCPageControl.TotalPage = 0;
                UCPageControl.CurrentPage = 0;
                gvMain.SortInfo.Clear();
            }
            else
            {
                bsBanksList.DataSource = list;
                bsBanksList.ResetBindings(false);

                if (CurrentChanged != null)
                {
                    CurrentChanged(this, Current);
                }

                _dataPageInfo = source.DataPageInfo;

                if (_dataPageInfo != null)
                {
                    int totalCount = _dataPageInfo.TotalCount;
                    int pageSize = _dataPageInfo.PageSize;

                    int pageCount = totalCount / pageSize;
                    if (pageCount == 1 && totalCount > pageSize)
                    {
                        pageCount = 2;
                    }
                    if (pageCount == 0 && totalCount > 0)
                    {
                        pageCount = 1;
                    }
                    UCPageControl.TotalPage = pageCount;

                    UCPageControl.CurrentPage = _dataPageInfo.CurrentPage;
                    ColumnSortOrder sortOrder = _dataPageInfo.SortOrderType == SortOrderType.Asc ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending;
                    GridColumn col = gvMain.Columns.ColumnByFieldName(_dataPageInfo.SortByName);
                    gvMain.SortInfo.Clear();
                    if(col !=null)
                        gvMain.SortInfo.Add(new GridColumnSortInfo(col, sortOrder));
                }
            }
        }
        public override event CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;
        #endregion

        #region 按钮方法
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BankCommandConstants.Command_BankAdd)]
        public void Command_BankAdd(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                PartLoader.ShowEditPart<BankEdit>(Workitem, null, LocalData.IsEnglish ? "Add Bank" : "新增银行", EditPartSaved);
            }
        }

        [CommandHandler(BankCommandConstants.Command_BankCancel)]
        public void Command_BankCancel(object sender, EventArgs e)
        {
            if (CurrentRow != null)
            {
                if (CurrentRow.IsValid)
                {
                    if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1108100001")))
                    {
                        return;
                    }
                }
                else
                {
                    if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1108100002")))
                    {
                        return;
                    }
                }


                string failureMessage = string.Empty;
                if (CurrentRow.IsValid)
                    failureMessage = LocalData.IsEnglish ? "Cancel Booking State Failed." : "作废银行信息失败.";
                else
                    failureMessage = LocalData.IsEnglish ? "Available Booking State Failed." : "激活银行信息失败.";

                try
                {
                    SingleResult result = FinanceService.ChangeBankValidity(CurrentRow.ID,
                        CurrentRow.IsValid,
                        LocalData.UserInfo.LoginID,
                        CurrentRow.UpdateDate,
                        LocalData.IsEnglish);

                    CurrentRow.IsValid = !CurrentRow.IsValid;
                    CurrentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, CurrentRow);
                    }

                    if (CurrentRow.IsValid)
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Cancel Booking Successfully." : "银行信息已经成功作废.");
                    else
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Available Booking Successfully." : "银行信息已经成功激活.");

                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), failureMessage + ex.Message);
                }
            }
        }

        [CommandHandler(BankCommandConstants.Command_BankEdit)]
        public void Command_BankEdit(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow != null)
                {
                    PartLoader.ShowEditPart<BankEdit>(Workitem, CurrentRow, LocalData.IsEnglish ? "Edit Bank" : "编辑银行", EditPartSaved);
                }
            }
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 编辑界面保存数据
        /// </summary>
        /// <param name="prams"></param>
        private void EditPartSaved(object[] prams)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (prams == null || prams.Length == 0) return;

                BankInfo data = prams[0] as BankInfo;



                List<BankList> source = DataSource as List<BankList>;
                if (source == null || source.Count == 0)
                {
                    bsBanksList.Add(data);
                    bsBanksList.ResetBindings(false);
                }
                else
                {
                    BankList tager = source.Find(delegate(BankList item) { return item.ID == data.ID; });
                    if (tager == null)
                    {
                        bsBanksList.Insert(0, data);
                        bsBanksList.ResetBindings(false);
                    }
                    else
                    {
                        FAMUtility.CopyToValue(data, tager, typeof(BankList));

                        bsBanksList.ResetItem(bsBanksList.IndexOf(tager));
                    }

                }
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }
            }
        }
        #endregion

        #region Grid Event
        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            BankList list = gvMain.GetRow(e.RowHandle) as BankList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
            }

        }
        private void bsBanksList_CurrentChanged(object sender, EventArgs e)
        {

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

        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (CurrentChanged != null)
                {
                    Workitem.Commands[BankCommandConstants.Command_BankEdit].Execute();
                }
            }
        }

        private void bsBanksList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }
        public new event KeyEventHandler KeyDown;
        private void gcMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
            {
                Workitem.Commands[BankCommandConstants.Command_BankEdit].Execute();
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
                Workitem.Commands[BankCommandConstants.Command_BankShowSearch].Execute();
            }
        }
        #endregion

        #region 分页

        /// <summary>
        /// 选择的页发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPageControl_PageChanged(object sender, PageChangedEventArgs e)
        {
            if (InvokeGetData != null)
            {
                _dataPageInfo.CurrentPage = e.CurrentPage;
                InvokeGetData(this, _dataPageInfo);
            }
        }

        /// <summary>
        /// 点击列头排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="column"></param>
        private void gvMain_CustomerSorting(object sender, SortingCancelEventArgs e)
        {
            if (DataSourceList == null || DataSourceList.Count==0)
            {
                return;
            }

            if (InvokeGetData != null)
            {
                e.Cancel = true;
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

    

    }
}
