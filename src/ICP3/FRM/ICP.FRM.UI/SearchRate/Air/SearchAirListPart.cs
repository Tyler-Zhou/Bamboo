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
    [ToolboxItem(false)]
    public partial class SearchAirListPart : BaseListPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ISearchRatesService SearchRatesService
        {
            get
            {
                return ServiceClient.GetService<ISearchRatesService>();
            }
        }

        #endregion

        #region init

        public SearchAirListPart()
        {
            InitializeComponent();
            Disposed += delegate {
                KeyDown = null;
                InvokeGetData = null;
                gcMain.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                _dataPageInfo = null;
                _UnitMinValue = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        #endregion

        #region 自有属性
        /// <summary>
        /// 当前行数据
        /// </summary>
        public ClientSearchAirRateList CurrentRow
        {
            get
            {
                return bsList.Current as ClientSearchAirRateList;
            }
        }
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<ClientSearchAirRateList> DataList
        {
            get
            {
                List<ClientSearchAirRateList> list = bsList.DataSource as List<ClientSearchAirRateList>;
                if (list == null)
                {
                    list = new List<ClientSearchAirRateList>();
                }
                return list;
            }
        }

        private Dictionary<string, decimal> _UnitMinValue = new Dictionary<string, decimal>();

        #endregion

        #region 继承属性

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
        private void BindingData(object data)
        {
            PageList orglist = data as PageList;

            if (orglist == null || orglist.GetList<SearchAirRateList>().Count == 0)
            {
                bsList.DataSource = orglist;
                pageControl1.TotalPage = 0;
                pageControl1.CurrentPage = 0;
                gvMain.SortInfo.Clear();
            }
            else
            {
                _UnitMinValue = new Dictionary<string, decimal>();
                PageList list = SearchPriceTransformHelper.TransformS2C(orglist, ref  _UnitMinValue);
                BulidGridViewColumnsByUnits(_UnitMinValue);

                bsList.DataSource = list.GetList <ClientSearchAirRateList>();
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

        private void BulidGridViewColumnsByUnits(Dictionary<string, decimal> unitKeys)
        {
            #region  SetVisible= false;
            colRate45.Visible = false;
            colRate100.Visible = false;
            colRate300.Visible = false;
            colRate500.Visible = false;
            colRate800.Visible = false;
            colRate1000.Visible = false;
            colRate1300.Visible = false;
            #endregion

            int visibleIndex = 5;

            foreach (var item in unitKeys)
            {
                #region  SetVisible= true;
                switch (item.Key)
                {
                    case "+45": colRate45.VisibleIndex = visibleIndex; break;
                    case "+100": colRate100.VisibleIndex = visibleIndex; break;
                    case "+300": colRate300.VisibleIndex = visibleIndex; break;
                    case "+500": colRate500.VisibleIndex = visibleIndex; break;
                    case "+800": colRate800.VisibleIndex = visibleIndex; break;
                    case "+1000": colRate1000.VisibleIndex = visibleIndex; break;
                    case "+1300": colRate1300.VisibleIndex = visibleIndex; break;

                }
                visibleIndex++;
                #endregion
            }

            colCommodity.VisibleIndex = visibleIndex + 1;
            colSchedule.VisibleIndex = colCommodity.VisibleIndex + 1;
            colRouting.VisibleIndex = colSchedule.VisibleIndex + 1;
            colDurationFrom.VisibleIndex = colRouting.VisibleIndex + 1;
            colDurationTo.VisibleIndex = colDurationFrom.VisibleIndex + 1;
        }

        #endregion

        #region  私有字段
        /// <summary>
        /// 按键事件
        /// </summary>
        public new event KeyEventHandler KeyDown;
        /// <summary>
        /// 分页类
        /// </summary>
        DataPageInfo _dataPageInfo = new DataPageInfo();
        #endregion

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }
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
        }
        #endregion

        #region 重写
        /// <summary>
        /// 行改变
        /// </summary>
        public override event CurrentChangedHandler CurrentChanged;

        public override event InvokeGetDataHandler InvokeGetData;
        #endregion

        #region 热键查询

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
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
        /// <summary>
        /// 分页控件分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pageControl1_PageChanged(object sender, PageChangedEventArgs e)
        {
            if (InvokeGetData != null)
            {
                _dataPageInfo.CurrentPage = e.CurrentPage;
                InvokeGetData(this, _dataPageInfo);
            }
        }

        #endregion

        #region 当前行发生改变
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }
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

            if (e.Column != colRate100 || e.Column != colRate1000
                || e.Column != colRate1300  || e.Column != colRate300 
                || e.Column != colRate45  || e.Column != colRate500 
                || e.Column != colRate800)
            {
                return;
            }

            decimal value = decimal.Parse(e.CellValue.ToString());
            if (value == 0) return;

            bool isMinValue = false;
            if (e.Column == colRate100)
            {
                if (_UnitMinValue.ContainsKey("+100") &&  value == _UnitMinValue["+100"]) isMinValue = true;
            }
            else if (e.Column == colRate1000)
            {
                if (_UnitMinValue.ContainsKey("+1000") && value == _UnitMinValue["+1000"]) isMinValue = true;
            }
            else if (e.Column == colRate1300 )
            {
                if (_UnitMinValue.ContainsKey("+1300") && value == _UnitMinValue["+1300"]) isMinValue = true;
            }
            else if (e.Column == colRate300 )
            {
                if (_UnitMinValue.ContainsKey("+300") && value == _UnitMinValue["+300"]) isMinValue = true;
            }
            else if (e.Column == colRate45 )
            {
                if (_UnitMinValue.ContainsKey("+45") && value == _UnitMinValue["+45"]) isMinValue = true;
            }
            else if (e.Column == colRate500 )
            {
                if (_UnitMinValue.ContainsKey("+500") && value == _UnitMinValue["+500"]) isMinValue = true;
            }
            else if (e.Column == colRate800 )
            {
                if (_UnitMinValue.ContainsKey("+800") && value == _UnitMinValue["+800"]) isMinValue = true;
            }

            if (isMinValue)
            {
                e.Appearance.Options.UseFont = true;
                e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
            }
        }

        #endregion

        #region 命令

        [CommandHandler(SearchAirCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object o, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<ClientSearchAirRateList> list = DataList;
                if (list == null || list.Count == 0) return;

                List<Guid> ids = new List<Guid>();
                foreach (var item in list) { ids.Add(item.ID); }

                List<SearchAirRateList> resluts = SearchRatesService.GetSearchAirList(ids.ToArray());
                List<ClientSearchAirRateList> newList = SearchPriceTransformHelper.TransformListS2C(resluts, ref _UnitMinValue);

                bsList.DataSource = newList;
                bsList.ResetBindings(false);
            }
        }

        #endregion
    }
}
