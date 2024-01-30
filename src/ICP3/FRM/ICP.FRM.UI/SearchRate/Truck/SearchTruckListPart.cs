using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.FRM.ServiceInterface;

namespace ICP.FRM.UI.SearchRate
{
    /// <summary>
    /// 拖车运价列表
    /// </summary>
    [ToolboxItem(false)]
    public partial class SearchTruckListPart : BaseListPart
    {
        #region Fields & Property & Service & Delegate
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        decimal _MinRate = -1;
        /// <summary>
        /// 按键事件
        /// </summary>
        public new event KeyEventHandler KeyDown;
        /// <summary>
        /// 分页类
        /// </summary>
        DataPageInfo _dataPageInfo = new DataPageInfo();
        #endregion

        #region 服务
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 查询服务
        /// </summary>
        public ISearchRatesService SearchRatesService
        {
            get
            {
                return ServiceClient.GetService<ISearchRatesService>();
            }
        }

        #endregion

        #region 属性
        /// <summary>
        /// 当前行数据
        /// </summary>
        public SearchTruckRateList CurrentRow
        {
            get
            {
                return bsList.Current as SearchTruckRateList;
            }
        }
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<SearchTruckRateList> DataList
        {
            get
            {
                List<SearchTruckRateList> list = bsList.DataSource as List<SearchTruckRateList>;
                return list;
            }
        }
        /// <summary>
        /// 数据源
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
            }
        }
        #endregion

        #region Delegate
        /// <summary>
        /// 行改变
        /// </summary>
        public override event CurrentChangedHandler CurrentChanged;
        /// <summary>
        /// 
        /// </summary>
        public override event InvokeGetDataHandler InvokeGetData;
        #endregion 
        #endregion

        #region init
        /// <summary>
        /// 拖车运价列表
        /// </summary>
        public SearchTruckListPart()
        {
            InitializeComponent();
            Disposed += delegate {
                CurrentChanged = null;
                KeyDown = null;
                InvokeGetData = null;
                gcMain.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
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
            if (!DesignMode)
            {
                InitControls();
            }
        }
        #endregion

        #region 继承属性
        
        private void BindingData(object data)
        {
            PageList orglist = data as PageList;

            if (orglist == null || orglist.GetList <SearchTruckRateList>().Count == 0)
            {
                bsList.DataSource = orglist;
                pageControl1.TotalPage = 0;
                pageControl1.CurrentPage = 0;
                gvMain.SortInfo.Clear();
            }
            else
            {
                _MinRate = -1;
                PageList list = orglist;
                foreach (var item in list.GetList<SearchTruckRateList>())
                {
                    if (item.Total < _MinRate) _MinRate = item.Total;
                }

                bsList.DataSource = list.GetList<SearchTruckRateList>();
                bsList.ResetBindings(false);
                _dataPageInfo = list.DataPageInfo;

                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }

                gvMain.BestFitColumns();

                if (list.DataPageInfo != null)
                {
                    int pageCount = list.DataPageInfo.TotalCount / list.DataPageInfo.PageSize;
                    if (pageCount == 1 && list.DataPageInfo.TotalCount > list.DataPageInfo.PageSize)
                    {
                        pageCount = 2;
                    }
                    if (pageCount == 0 && list.DataPageInfo.TotalCount > 0)
                    {
                        pageCount = 1;
                    }
                    pageControl1.TotalPage = pageCount;

                    pageControl1.CurrentPage = list.DataPageInfo.CurrentPage;
                    gvMain.SortInfo.Clear();
                    ColumnSortOrder sortOrder = list.DataPageInfo.SortOrderType == SortOrderType.Asc ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending;
                    GridColumn col = null;
                    for (int i = 0; i < gvMain.Columns.Count; i++)
                    {
                        if (gvMain.Columns[i].FieldName == list.DataPageInfo.SortByName)
                        {
                            col = gvMain.Columns[i];
                            break;
                        }
                    }
                    gvMain.SortInfo.Clear();
                    if (col != null)
                        gvMain.SortInfo.Add(new GridColumnSortInfo(col, sortOrder));
                }

            }
        }

        #endregion

        #region 初始化
        
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            Utility.ShowGridRowNo(gvMain);

            #region 查询运价状态
            List<EnumHelper.ListItem<SearchPriceStatus>> searchPriceStatus
                = EnumHelper.GetEnumValues<SearchPriceStatus>(LocalData.IsEnglish);
            foreach (var item in searchPriceStatus)
            {
                rcmbStatue.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value, (short)item.Value - 1));
            }
            #endregion

            bsList.PositionChanged += bsList_PositionChanged;

        }
        #endregion


        #region 热键查询

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }
            if (KeyDown != null
                && e.KeyCode == Keys.F5
                && gvMain.FocusedColumn != null
                && gvMain.FocusedValue != null)
            {
                string text = gvMain.GetFocusedDisplayText();
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add(gvMain.FocusedColumn.FieldName, text);
                KeyDown(keyValue, null);
            }

        }
        #endregion

        #region 分页
        /// <summary>
        /// 排序分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_CustomerSorting(object sender, SortingCancelEventArgs e)
        {
            if (DataList == null || DataList.Count == 0 || e.Column == colStatue)
            {
                return;
            }
            if (InvokeGetData != null)
            {
                e.Cancel = true;

                _dataPageInfo.CurrentPage = 1;
                _dataPageInfo.SortByName =  e.Column.FieldName;
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
        /// <summary>
        /// 分页控件分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pageControl1_PageChanged(object sender, PageChangedEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (InvokeGetData != null)
                {
                    _dataPageInfo.CurrentPage = e.CurrentPage;
                    InvokeGetData(this, _dataPageInfo);
                }
            }
        }

        #endregion

        #region 当前行发生改变
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }
        }
        #endregion

        #region 设置单元格样式

        /// <summary>
        /// 设置最价格为粗体
        /// </summary>
        private void gvMain_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            if (e.Column != colTotal) return;

            if (e.CellValue == null) return;

            decimal value = decimal.Parse(e.CellValue.ToString());
            if (value == 0) return;

            if (value == _MinRate)
            {
                e.Appearance.Options.UseFont = true;
                e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
            }
        }

        #endregion

        #region 命令

        [CommandHandler(SearchTruckCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object o, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<SearchTruckRateList> list = DataList;
                if (list == null || list.Count == 0) return;

                List<Guid> ids = new List<Guid>();
                foreach (var item in list) { ids.Add(item.ID); }

                List<SearchTruckRateList> resluts = SearchRatesService.GetSearchTruckList(ids.ToArray());

                bsList.DataSource = resluts;
                bsList.ResetBindings(false);
            }
        }

        #endregion
    }
}
