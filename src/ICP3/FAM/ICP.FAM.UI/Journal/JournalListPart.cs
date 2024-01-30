using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class JournalListPart : BaseListPart
    {

        public JournalListPart()
        {
            InitializeComponent();
            Disposed += delegate {
                InvokeGetData = null;
                CurrentChanged = null;
                CurrentChanging = null;
                KeyDown = null;
                gcMain.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                if (Workitem != null) 
                { 
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

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
        private void InitControls()
        {
            FAMUtility.ShowGridRowNo(gvMain);
        }

        private void InitMessage()
        { 
            RegisterMessage("1108100001", LocalData.IsEnglish ? "Are you sure to invalidate the selected time?" : "确认要作废该数据");
            RegisterMessage("1108100002", LocalData.IsEnglish ? "Are you sure to resume the selected time?" : "确认要激活该数据");
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
        #endregion

        #region 重写
        public override event CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;
        public override event InvokeGetDataHandler InvokeGetData;
        /// <summary>
        /// 当前行
        /// </summary>
        public override object Current
        {
            get { return bsList.Current; }
        }
        /// <summary>
        /// 当前行数据
        /// </summary>
        protected JournalList CurrentRow
        {
            get { return bsList.Current as JournalList; }
        }
        /// <summary>
        /// 当前数据列表
        /// </summary>
        public List<JournalList> DataSourceList
        {
            get
            {
                return bsList.DataSource as List<JournalList>;
            }
        }
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindingData(value);
            }

        }

        DataPageInfo _dataPageInfo = new DataPageInfo();
        private void BindingData(object value)
        {
            PageList source = value as PageList;
             
            if (source == null || source.GetList<JournalList>().Count == 0)
            {
                bsList.DataSource = typeof(JournalList);
                pageControl1.TotalPage = 0;
                pageControl1.CurrentPage = 0;
                gvMain.SortInfo.Clear();
            }
            else
            {
                bsList.DataSource = source.GetList<JournalList>();
                bsList.ResetBindings(false);
                _dataPageInfo = source.DataPageInfo;
                if (_dataPageInfo != null)
                {
                    int pageSize = _dataPageInfo.PageSize;
                    int totalCount = _dataPageInfo.TotalCount;
                    int pageCount = totalCount / pageSize;
                    if (pageCount == 1 && totalCount > pageSize)
                    {
                        pageCount = 2;
                    }
                    if (pageCount == 0 && totalCount > 0)
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
            }
        }

        #endregion

        #region 按钮方法
        /// <summary>
        ///新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(JournalCommandConstants.Command_JournalAdd)]
        public void Command_JournalAdd(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                PartLoader.ShowEditPart<JournalEditPart>(Workitem, null, LocalData.IsEnglish ? "Add Journal" : "新增日记帐", EditPartSaved);
            }
        }

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(JournalCommandConstants.Command_JournalCancel)]
        public void Command_JournalCancel(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {

                string message = string.Empty;
                if (CurrentRow.IsValid)
                    message = NativeLanguageService.GetText(this, "1108100001");
                else
                    message = NativeLanguageService.GetText(this, "1108100002");

                if (FAMUtility.ShowResultMessage(message))
                {
                    SingleResult result = FinanceService.CancelJournal(CurrentRow.ID,
                      CurrentRow.IsValid,
                      LocalData.UserInfo.LoginID,
                      CurrentRow.UpdateDate,
                      LocalData.IsEnglish);

                    JournalList currentRow = CurrentRow;
                    currentRow.IsValid = !CurrentRow.IsValid;
                    currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    bsList.ResetCurrentItem();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Changed Journal State Successfully" : "更改日记账状态成功");
                    if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed Journal State Failed" : "更改日记账状态失败") + ex.Message);
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        [CommandHandler(JournalCommandConstants.Command_JournalRefreshData)]
        public void Command_JournalRefreshData(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(JournalCommandConstants.Command_JournalEdit)]
        public void Command_JournalEdit(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow != null)
                {
                    PartLoader.ShowEditPart<JournalEditPart>(Workitem, CurrentRow, LocalData.IsEnglish ? "Edit Journal" : "编辑日志帐", EditPartSaved);
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
            if (prams == null || prams.Length == 0) return;

            JournalInfo data = prams[0] as JournalInfo;

            List<JournalList> source = DataSource as List<JournalList>;
            if (source == null || source.Count == 0)
            {
                bsList.Add(data);
                bsList.ResetBindings(false);
            }
            else
            {
                JournalList tager = source.Find(delegate(JournalList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    FAMUtility.CopyToValue(data, tager, typeof(JournalList));

                    bsList.ResetItem(bsList.IndexOf(tager));
                }

            }

        }
        #endregion

        #region 表格事件
        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (CurrentChanged != null)
                {
                    Workitem.Commands[JournalCommandConstants.Command_JournalEdit].Execute();
                }
            }
        }
        public new event KeyEventHandler KeyDown;

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
            {
                Workitem.Commands[JournalCommandConstants.Command_JournalEdit].Execute();
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
                Workitem.Commands[JournalCommandConstants.Command_JournalShowSearch].Execute();
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
        #endregion

        #region 分页

        /// <summary>
        /// 选择的页发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void pageControl1_PageChanged(object sender, PageChangedEventArgs e)
        {
            List<JournalList> blList = DataSource as List<JournalList>;
            if (blList == null || blList.Count == 0) return;

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
        private void gvMain_CustomerSorting(object sender,SortingCancelEventArgs e)
        {
            List<JournalList> blList = DataSource as List<JournalList>;
            if (blList == null || blList.Count == 0) return;
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

        #region Grid 事件
        /// <summary>
        /// 作废数据禁用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            JournalList list = gvMain.GetRow(e.RowHandle) as JournalList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
            }
        }
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        #endregion
    }
}
