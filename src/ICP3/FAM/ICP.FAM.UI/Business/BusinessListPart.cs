using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Common;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;

namespace ICP.FAM.UI.Business
{
    [ToolboxItem(false)]
    public partial class BusinessListPart : BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IBusinessInfoProviderFactory BusinessInfoProviderFactory
        {
            get
            {
                return ServiceClient.GetClientService<IBusinessInfoProviderFactory>();
            }
        }

        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        #endregion

        #region 初始化

        public BusinessListPart()
        {
            InitializeComponent();
            Disposed += delegate {
                gcMain.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                Selecting = null;
                CurrentChanged = null;
                InvokeGetData = null;
                _dataPageInfo = null;
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
        }

        #endregion

        #region 命令

        [CommandHandler(BusinessCommandConstants.Command_ViewBusinessInfo)]
        public void Command_ViewBusinessInfo(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                IBusinessInfoProvider provider = BusinessInfoProviderFactory.GetBusinessInfoProvider(CurrentRow.OperationType);
                provider.ShowBusinessInfo(OperationType.OceanExport, CurrentRow.ID, ClientConstants.MainWorkspace);
            }
        }

        [CommandHandler(BusinessCommandConstants.Command_Bill)]
        public void Command_Bill(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(CurrentRow.ID, CurrentRow.OperationType);
                FinanceClientService.ShowBillList(operationCommonInfo, ClientConstants.MainWorkspace);
            }
        }

        [CommandHandler(BusinessCommandConstants.Command_SelectAll)]
        public void Command_SelectAll(object sender, EventArgs e)
        {
            gvMain.CloseEditor();
            List<BusinessList> source = bsList.DataSource as List<BusinessList>;
            if (source == null || source.Count == 0) return;
            foreach (var item in source)
            {
                if (item.CompanyID == source[0].CompanyID) item.Selected = true;
                else item.Selected = false;
            }
            bsList.EndEdit();
            bsList.DataSource = source;
            bsList.ResetBindings(false);
            List<BusinessList> selected = source.FindAll(delegate(BusinessList item) { return item.Selected; });
            if (Selecting != null) Selecting(this, selected, null);
        }

        [CommandHandler(BusinessCommandConstants.Command_BatchAddBill)]
        public void Command_BatchAddBill(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                gvMain.CloseEditor();
                List<BusinessList> source = bsList.DataSource as List<BusinessList>;
                if (source == null || source.Count == 0) return;
                List<BusinessList> selected = new List<BusinessList>();
                foreach (var item in source)
                {
                    if (item.Selected) selected.Add(item);
                }

                if (selected.Count == 0) return;

                string title = LocalData.IsEnglish ? "Batch Bill" : "批量帐单";
                PartLoader.ShowEditPart<BatchAddBillPart>(Workitem, selected, title, AfterBatchAddBill);
            }
        }

        void AfterBatchAddBill(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;
            List<Guid> needUpdateIDs = prams[0] as List<Guid>;
            List<BusinessList> source = DataSource as List<BusinessList>;
            if (source == null || source.Count == 0)
            {
                source = new List<BusinessList>();
                PageList list=FinanceService.GetBusinessListByIDs(needUpdateIDs.ToArray());
                List<BusinessList> updated = list.GetList<BusinessList>();
                source.AddRange(updated);
                bsList.DataSource = source;
                bsList.ResetBindings(false);
            }
            else
            {
                List<BusinessList> tagers = source.FindAll(delegate(BusinessList item) { return needUpdateIDs.Contains(item.ID); });
                if (tagers != null && tagers.Count > 0)
                {
                    foreach (var item in tagers)
                    {
                        source.Remove(item);
                    }
                }

                List<BusinessList> updated = FinanceService.GetBusinessListByIDs(needUpdateIDs.ToArray()).GetList<BusinessList>();
                source.InsertRange(0, updated);
                bsList.DataSource = source;
                bsList.ResetBindings(false);
            }
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }

        [CommandHandler(BusinessCommandConstants.Command_Refresh)]
        public void Command_Refresh(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    List<BusinessList> blList = DataSource as List<BusinessList>;
                    if (blList == null || blList.Count == 0) return;

                    List<Guid> ids = new List<Guid>();
                    foreach (var item in blList) ids.Add(item.ID);

                    List<BusinessList> list = FinanceService.GetBusinessListByIDs(ids.ToArray()).GetList<BusinessList>();
                    DataSource = list;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
            }
        }
        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected BusinessList CurrentRow
        {
            get { return Current as BusinessList; }
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

        public List<BusinessList> DataSourceList
        {
            get
            {
                List<BusinessList> list = bsList.DataSource as List<BusinessList>;
                if (list == null)
                {
                    list = new List<BusinessList>();
                }
                return list;
            }
        }

        DataPageInfo _dataPageInfo = new DataPageInfo();
        private void BindingData(object value)
        {
            PageList source = value as PageList;
            List<BusinessList> list = new List<BusinessList>();
            if (source != null) list = source.GetList<BusinessList>();
            if (source == null || list == null || list.Count == 0)
            {
                bsList.DataSource = list;
                pageControl1.TotalPage = 0;
                pageControl1.CurrentPage = 0;
                gvMain.SortInfo.Clear();
            }
            else
            {
                gvMain.BeginUpdate();
                gvMain.BeginDataUpdate();

                bsList.DataSource = list;
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
                    gvMain.SortInfo.Clear();
                    if (col != null)
                        gvMain.SortInfo.Add(new GridColumnSortInfo(col, sortOrder));
                }

                gvMain.EndDataUpdate();
                gvMain.EndUpdate();
            }
        }

        public override event CurrentChangedHandler CurrentChanged;

        public override event InvokeGetDataHandler InvokeGetData;

        public override event SelectingHandler Selecting;

        #endregion

        #region gridview event

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0 || e.Button != MouseButtons.Left || e.Column == colSelected) return;

            if (e.Clicks == 2 || e.Column == colOperationNO)
            {
                IBusinessInfoProvider provider = BusinessInfoProviderFactory.GetBusinessInfoProvider(CurrentRow.OperationType);

                if (provider != null)
                {
                    provider.ShowBusinessInfo(CurrentRow.OperationType, CurrentRow.ID, ClientConstants.MainWorkspace);
                }
            }
        }


        private void gvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        /// <summary>
        /// 选择时的控制
        /// </summary>
        private void gvMain_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0 || e.Column != colSelected) return;

            List<BusinessList> list = bsList.DataSource as List<BusinessList>;
            if ((bool)e.Value == true)
            {
                foreach (var item in list)
                {
                    if (item.CompanyID != CurrentRow.CompanyID) item.Selected = false;
                }
                bsList.DataSource = list;
                bsList.ResetBindings(false);
                if (Selecting != null) Selecting(this, new List<BusinessList> { CurrentRow }, null);
            }
            else
            {
                List<BusinessList> selected = list.FindAll(delegate(BusinessList item) { return item.Selected && item.ID != CurrentRow.ID; });
                if (Selecting != null) Selecting(this, selected, null);
            }
        }

        #endregion

        #region 事件发布

        private void gvMain_CustomerSorting(object sender, SortingCancelEventArgs e)
        {
            if (DataSourceList == null || DataSourceList.Count == 0)
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

        private void pageControl1_PageChanged(object sender, PageChangedEventArgs e)
        {
            if (InvokeGetData != null)
            {
                _dataPageInfo.CurrentPage = e.CurrentPage;
                InvokeGetData(this, _dataPageInfo);
            }
        }

        #endregion

    }
}
