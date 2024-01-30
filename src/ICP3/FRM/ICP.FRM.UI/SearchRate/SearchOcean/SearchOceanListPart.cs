using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace ICP.FRM.UI.SearchRate
{
    [ToolboxItem(false)]
    public partial class SearchOceanListPart : BaseListPart
    {
        public SearchOceanListPart()
        {
            InitializeComponent();
            Disposed += delegate {
                KeyDown = null;
                CurrentChanged = null;
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

        #region 禁止复制
        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                e.Handled = true;
            }
        }


        #endregion

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

        #region 自有属性
        /// <summary>
        /// 当前行数据
        /// </summary>
        public SearchOceanRateList CurrentRow
        {
            get
            {
                return bsList.Current as SearchOceanRateList;
            }
        }
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<SearchOceanRateList> DataList
        {
            get
            {
                List<SearchOceanRateList> list = bsList.DataSource as List<SearchOceanRateList>;
                if (list == null)
                {
                    list = new List<SearchOceanRateList>();
                }
                return list;
            }
        }
        bool IsViewReserve = false;

        List<string> rateColumn = new List<string>();

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
            PageList list = data as PageList;
            if (list == null)
            {
                list = new PageList();
            }
            #region 数据源
            List<SearchOceanRateList> dataSourceList = list.GetList<SearchOceanRateList>();
            if (dataSourceList == null)
            {
                dataSourceList = new List<SearchOceanRateList>();
            }
            #endregion

            #region 已选择的
            List<SearchOceanRateList> selectDataList = (from d in DataList where d.IsCheck select d).ToList();
            if (selectDataList == null)
            {
                selectDataList = new List<SearchOceanRateList>();
            }
            foreach (SearchOceanRateList item in selectDataList)
            {
                SearchOceanRateList oldItem = dataSourceList.Find(delegate(SearchOceanRateList soitem) { return soitem.ID == item.ID; });
                if (oldItem != null && dataSourceList.Contains(oldItem))
                {
                    dataSourceList.Remove(oldItem);
                }
                item.IsCheck = true;
                dataSourceList.Insert(0, item);
            }
            #endregion

            if (dataSourceList.Count == selectDataList.Count)
            {
                bsList.DataSource = dataSourceList;
                pageControl1.TotalPage = 0;
                pageControl1.CurrentPage = 0;
                gvMain.SortInfo.Clear();
            }
            else
            {
                TransSearchOceanRateList(dataSourceList);

                bsList.DataSource = dataSourceList;
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
                    if (colRate_40GP.Visible = true && colRate_40GP.VisibleIndex > 0)
                    {
                        gvMain.SortInfo.Add(new GridColumnSortInfo(colRate_OrdreRate, ColumnSortOrder.Ascending));

                    }
                    else  if (col != null)
                    {
                        gvMain.SortInfo.Add(new GridColumnSortInfo(col, sortOrder));
                    }
       
                }
            }
            this.colRate_OrdreRate.Visible = false;
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
        /// <summary>
        /// 价格列名
        /// </summary>
        List<string> rateColumnNameList = new List<string>();
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
                cmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value, (short)item.Value - 1));
            }
            #endregion

            #region 到账状态
            List<EnumHelper.ListItem<SearchRateType>> currencyPaidStatue
                = EnumHelper.GetEnumValues<SearchRateType>(LocalData.IsEnglish);
            foreach (var item in currencyPaidStatue)
            {
                cmbType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            #endregion

            #region 添加列名
            rateColumnNameList.Add("Rate_20HQ");
            rateColumnNameList.Add("Rate_20HT");
            rateColumnNameList.Add("Rate_20OT");
            rateColumnNameList.Add("Rate_20FR");
            rateColumnNameList.Add("Rate_20RF");
            rateColumnNameList.Add("Rate_20RH");
            rateColumnNameList.Add("Rate_20TK");
            rateColumnNameList.Add("Rate_40FR");
            rateColumnNameList.Add("Rate_40HT");
            rateColumnNameList.Add("Rate_40OT");
            rateColumnNameList.Add("Rate_40RF");
            rateColumnNameList.Add("Rate_40RH");
            rateColumnNameList.Add("Rate_40TK");
            rateColumnNameList.Add("Rate_45FR");
            rateColumnNameList.Add("Rate_45GP");
            rateColumnNameList.Add("Rate_45HT");
            rateColumnNameList.Add("Rate_45RF");
            rateColumnNameList.Add("Rate_45RH");
            rateColumnNameList.Add("Rate_45TK");
            rateColumnNameList.Add("Rate_45OT");
            rateColumnNameList.Add("Rate_40NOR");
            rateColumnNameList.Add("Rate_20GP");
            rateColumnNameList.Add("Rate_40HQ");
            rateColumnNameList.Add("Rate_45HQ");
            rateColumnNameList.Add("Rate_20NOR");
            rateColumnNameList.Add("Rate_40GP");
            rateColumnNameList.Add("Rate_53HQ");
            #endregion


            IsViewReserve = LocalCommonServices.PermissionService.HaveActionPermission(PermissionCommandConstants.SEARCHOCEAN_VIEWRESERVE);

            #region 添加列
            rateColumn = new List<string>();
            rateColumn.Add(colRate_20FR.Name);
            rateColumn.Add(colRate_20GP.Name);
            rateColumn.Add(colRate_20HQ.Name);
            rateColumn.Add(colRate_20HT.Name);
            rateColumn.Add(colRate_20NOR.Name);
            rateColumn.Add(colRate_20OT.Name);
            rateColumn.Add(colRate_20RF.Name);
            rateColumn.Add(colRate_20RH.Name);
            rateColumn.Add(colRate_20TK.Name);
            rateColumn.Add(colRate_40FR.Name);
            rateColumn.Add(colRate_40GP.Name);
            rateColumn.Add(colRate_40HQ.Name);
            rateColumn.Add(colRate_40HT.Name);
            rateColumn.Add(colRate_40NOR.Name);
            rateColumn.Add(colRate_40OT.Name);
            rateColumn.Add(colRate_40RF.Name);
            rateColumn.Add(colRate_40RH.Name);
            rateColumn.Add(colRate_40TK.Name);
            rateColumn.Add(colRate_45FR.Name);
            rateColumn.Add(colRate_45FR.Name);
            rateColumn.Add(colRate_45GP.Name);
            rateColumn.Add(colRate_45HQ.Name);
            rateColumn.Add(colRate_45HT.Name);
            rateColumn.Add(colRate_45OT.Name);
            rateColumn.Add(colRate_45RF.Name);
            rateColumn.Add(colRate_45RH.Name);
            rateColumn.Add(colRate_45TK.Name);
            rateColumn.Add(colRate_53HQ.Name);
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

        //private void gvMain_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (CurrentRow == null)
        //    {
        //        return;
        //    }
        //    if (this.KeyDown != null
        //        && e.KeyCode == Keys.F5
        //        && this.gvMain.FocusedColumn != null
        //        && this.gvMain.FocusedValue != null)
        //    {
        //        string text = gvMain.GetFocusedDisplayText();
        //        Dictionary<string, object> keyValue = new Dictionary<string, object>();
        //        keyValue.Add(gvMain.FocusedColumn.FieldName, text);
        //        this.KeyDown(keyValue, null);
        //    }

        //}
        #endregion

        #region 分页
        /// <summary>
        /// 排序分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_CustomerSorting(object sender, SortingCancelEventArgs e)
        {
            if (DataList == null || DataList.Count == 0 || e.Column == colState)
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

        #region 控制列的显示/隐藏
        public void TransSearchOceanRateList(List<SearchOceanRateList> list)
        {
            List<string> unitList = new List<string>();

            #region  SetVisible= false;
            colRate_45FR.Visible = false;
            colRate_40RF.Visible = false;
            colRate_45HT.Visible = false;
            colRate_20RF.Visible = false;
            colRate_20HQ.Visible = false;
            colRate_20TK.Visible = false;
            colRate_20GP.Visible = false;
            colRate_40TK.Visible = false;
            colRate_40OT.Visible = false;
            colRate_20FR.Visible = false;
            colRate_45GP.Visible = false;
            colRate_40GP.Visible = false;
            colRate_45RF.Visible = false;
            colRate_20RH.Visible = false;
            colRate_45OT.Visible = false;
            colRate_40NOR.Visible = false;
            colRate_40FR.Visible = false;
            colRate_20OT.Visible = false;
            colRate_45TK.Visible = false;
            colRate_20NOR.Visible = false;
            colRate_40HT.Visible = false;
            colRate_40RH.Visible = false;
            colRate_45RH.Visible = false;
            colRate_45HQ.Visible = false;
            colRate_20HT.Visible = false;
            colRate_40HQ.Visible = false;
            colRate_53HQ.Visible = false;
            #endregion

            #region Set Rate And Price
            int VisibleIndex = 9;
            foreach (SearchOceanRateList item in list)
            {
                foreach (FrmUnitRateInfo unit in item.UnitList)
                {
                    switch (unit.UnitName)
                    {
                        #region 常用
                        case "20GP":
                            if (IsViewReserve)
                            {
                                item.Rate_20GP = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_20GP = unit.SalesRate;
                            }
                            item.Rate_20GP_Sales = unit.SalesRate;
                            if (!unitList.Contains("20GP"))
                            {
                                unitList.Add("20GP");
                            }
                            break;
                        case "40GP":
                            if (IsViewReserve)
                            {
                                item.Rate_40GP = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_40GP = unit.SalesRate;
                            }
                            item.OrdreRate = unit.ReserveRate;
                            item.Rate_40GP_Sales = unit.SalesRate;
                            if (!unitList.Contains("40GP"))
                            {
                                unitList.Add("40GP");
                            }
                            break;
                        case "40HQ":
                            if (IsViewReserve)
                            {
                                item.Rate_40HQ = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_40HQ = unit.SalesRate;
                            }
                            item.Rate_40HQ_Sales = unit.SalesRate;

                            if (!unitList.Contains("40HQ"))
                            {
                                unitList.Add("40HQ");
                            }
                            break;
                        case "45HQ":
                            if (IsViewReserve)
                            {
                                item.Rate_45HQ = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_45HQ = unit.SalesRate;
                            }
                            item.Rate_45HQ_Sales = unit.SalesRate;
                            if (!unitList.Contains("45HQ"))
                            {
                                unitList.Add("45HQ");
                            }
                            break;
                        case "20NOR":
                            if (IsViewReserve)
                            {
                                item.Rate_20NOR = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_20NOR = unit.SalesRate;
                            }
                            item.Rate_20NOR_Sales = unit.SalesRate;
                            if (!unitList.Contains("20NOR"))
                            {
                                unitList.Add("20NOR");
                            }
                            break;
                        case "40NOR":
                            if (IsViewReserve)
                            {
                                item.Rate_40NOR = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_40NOR = unit.SalesRate;
                            }
                            item.Rate_40NOR_Sales = unit.SalesRate;
                            if (!unitList.Contains("40NOR"))
                            {
                                unitList.Add("40NOR");
                            }
                            break;
                        #endregion

                        #region 20
                        case "20FR":
                            if (IsViewReserve)
                            {
                                item.Rate_20FR = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_20FR = unit.SalesRate;
                            }
                            item.Rate_20FR_Sales = unit.SalesRate;
                            item.Rate_40NOR_Sales = unit.SalesRate;
                            if (!unitList.Contains("20FR"))
                            {
                                unitList.Add("20FR");
                            }
                            break;
                        case "20RH":
                            if (IsViewReserve)
                            {
                                item.Rate_20RH = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_20RH = unit.SalesRate;
                            }
                            item.Rate_20RH_Sales = unit.SalesRate;
                            item.Rate_40NOR_Sales = unit.SalesRate;
                            if (!unitList.Contains("20RH"))
                            {
                                unitList.Add("20RH");
                            }
                            break;
                        case "20RF":
                            if (IsViewReserve)
                            {
                                item.Rate_20RF = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_20RF = unit.SalesRate;
                            }
                            item.Rate_20RF_Sales = unit.SalesRate;
                            item.Rate_40NOR_Sales = unit.SalesRate;
                            if (!unitList.Contains("20RF"))
                            {
                                unitList.Add("20RF");
                            }
                            break;
                        case "20HQ":
                            if (IsViewReserve)
                            {
                                item.Rate_20HQ = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_20HQ = unit.SalesRate;
                            }
                            item.Rate_20HQ_Sales = unit.SalesRate;
                            item.Rate_40NOR_Sales = unit.SalesRate;
                            if (!unitList.Contains("20HQ"))
                            {
                                unitList.Add("20HQ");
                            }
                            break;
                        case "20TK":
                            if (IsViewReserve)
                            {
                                item.Rate_20TK = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_20TK = unit.SalesRate;
                            }
                            item.Rate_20TK_Sales = unit.SalesRate;
                            item.Rate_40NOR_Sales = unit.SalesRate;
                            if (!unitList.Contains("20TK"))
                            {
                                unitList.Add("20TK");
                            }
                            break;
                        case "20OT":
                            if (IsViewReserve)
                            {
                                item.Rate_20OT_Sales = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_20OT = unit.SalesRate;
                            }
                            item.Rate_20OT = unit.SalesRate;
                            item.Rate_40NOR_Sales = unit.SalesRate;
                            if (!unitList.Contains("20OT"))
                            {
                                unitList.Add("20OT");
                            }
                            break;
                        case "20HT":
                            if (IsViewReserve)
                            {
                                item.Rate_20HT = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_20HT = unit.SalesRate;
                            }
                            item.Rate_20HT_Sales = unit.SalesRate;
                            item.Rate_40NOR_Sales = unit.SalesRate;
                            if (!unitList.Contains("20HT"))
                            {
                                unitList.Add("20HT");
                            }
                            break;
                        #endregion

                        #region 40
                        case "40TK":
                            if (IsViewReserve)
                            {
                                item.Rate_40TK = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_40TK = unit.SalesRate;
                            }
                            item.Rate_40TK_Sales = unit.SalesRate;
                            if (!unitList.Contains("40TK"))
                            {
                                unitList.Add("40TK");
                            }
                            break;
                        case "40OT":
                            if (IsViewReserve)
                            {
                                item.Rate_40OT = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_40OT = unit.SalesRate;
                            }
                            item.Rate_40OT_Sales = unit.SalesRate;
                            if (!unitList.Contains("40OT"))
                            {
                                unitList.Add("40OT");
                            }
                            break;
                        case "40FR":
                            if (IsViewReserve)
                            {
                                item.Rate_40FR = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_40FR = unit.SalesRate;
                            }
                            item.Rate_40FR_Sales = unit.SalesRate;
                            if (!unitList.Contains("40FR"))
                            {
                                unitList.Add("40FR");
                            }
                            break;
                        case "40HT":
                            if (IsViewReserve)
                            {
                                item.Rate_40HT = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_40HT = unit.SalesRate;
                            }
                            item.Rate_40HT_Sales = unit.SalesRate;
                            if (!unitList.Contains("40HT"))
                            {
                                unitList.Add("40HT");
                            }
                            break;
                        case "40RH":
                            if (IsViewReserve)
                            {
                                item.Rate_40RH = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_40RH = unit.SalesRate;
                            }
                            item.Rate_40RH_Sales = unit.SalesRate;
                            if (!unitList.Contains("40RH"))
                            {
                                unitList.Add("40RH");
                            }
                            break;
                        case "40RF":
                            if (IsViewReserve)
                            {
                                item.Rate_40RF = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_40RF = unit.SalesRate;
                            }
                            item.Rate_40RF_Sales = unit.SalesRate;
                            if (!unitList.Contains("40RF"))
                            {
                                unitList.Add("40RF");
                            }
                            break;

                        #endregion

                        #region 45
                        case "45GP":
                            if (IsViewReserve)
                            {
                                item.Rate_45GP = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_45GP = unit.SalesRate;
                            }
                            item.Rate_45GP_Sales = unit.SalesRate;
                            if (!unitList.Contains("45GP"))
                            {
                                unitList.Add("45GP");
                            }
                            break;
                        case "45RF":
                            if (IsViewReserve)
                            {
                                item.Rate_45RF = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_45RF = unit.SalesRate;
                            }
                            item.Rate_45RF_Sales = unit.SalesRate;
                            if (!unitList.Contains("45RF"))
                            {
                                unitList.Add("45RF");
                            }
                            break;
                        case "45HT":
                            if (IsViewReserve)
                            {
                                item.Rate_45HT = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_45HT = unit.SalesRate;
                            }
                            item.Rate_45HT_Sales = unit.SalesRate;
                            if (!unitList.Contains("45HT"))
                            {
                                unitList.Add("45HT");
                            }
                            break;
                        case "45FR":
                            if (IsViewReserve)
                            {
                                item.Rate_45FR = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_45FR = unit.SalesRate;
                            }
                            item.Rate_45FR_Sales = unit.SalesRate;
                            if (!unitList.Contains("45FR"))
                            {
                                unitList.Add("45FR");
                            }
                            break;
                        case "45OT":
                            if (IsViewReserve)
                            {
                                item.Rate_45OT = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_45OT = unit.SalesRate;
                            }
                            item.Rate_45OT_Sales = unit.SalesRate;
                            if (!unitList.Contains("45OT"))
                            {
                                unitList.Add("45OT");
                            }
                            break;
                        case "45TK":
                            if (IsViewReserve)
                            {
                                item.Rate_45TK = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_45TK = unit.SalesRate;
                            }
                            item.Rate_45TK_Sales = unit.SalesRate;
                            if (!unitList.Contains("45TK"))
                            {
                                unitList.Add("45TK");
                            }
                            break;
                        case "45RH":
                            if (IsViewReserve)
                            {
                                item.Rate_45RH = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_45RH = unit.SalesRate;
                            }
                            item.Rate_45RH_Sales = unit.SalesRate;
                            if (!unitList.Contains("45RH"))
                            {
                                unitList.Add("45RH");
                            }
                            break;
                        case "53HQ":
                            if (IsViewReserve)
                            {
                                item.Rate_53HQ = unit.ReserveRate;
                            }
                            else
                            {
                                item.Rate_53HQ = unit.SalesRate;
                            }
                            item.Rate_53HQ_Sales = unit.SalesRate;
                            if (!unitList.Contains("53HQ"))
                            {
                                unitList.Add("53HQ");
                            }
                            break;
                        #endregion
                    }
                }
            }
            #endregion

            #region 设置显示列
            #region 常用
            if (unitList.Contains("20GP"))
            {
                colRate_20GP.Visible = true;
                colRate_20GP.VisibleIndex = VisibleIndex + 1;
            }
            if (unitList.Contains("40GP"))
            {
                colRate_40GP.Visible = true;
                colRate_40GP.VisibleIndex = VisibleIndex + 2;
            }
            if (unitList.Contains("40HQ"))
            {
                colRate_40HQ.Visible = true;
                colRate_40HQ.VisibleIndex = VisibleIndex + 3;
            }
            if (unitList.Contains("45HQ"))
            {
                colRate_45HQ.Visible = true;
                colRate_45HQ.VisibleIndex = VisibleIndex + 4;
            }
            if (unitList.Contains("20NOR"))
            {
                colRate_20NOR.Visible = true;
                colRate_20NOR.VisibleIndex = VisibleIndex + 5;
            }
            if (unitList.Contains("40NOR"))
            {
                colRate_40NOR.Visible = true;
                colRate_40NOR.VisibleIndex = VisibleIndex + 6;
            }
            #endregion

            #region 20
            if (unitList.Contains("20FR"))
            {
                colRate_20FR.Visible = true;
                colRate_20FR.VisibleIndex = VisibleIndex + 7;
            }
            if (unitList.Contains("20RH"))
            {
                colRate_20RH.Visible = true;
                colRate_20RH.VisibleIndex = VisibleIndex + 8;
            }
            if (unitList.Contains("20RF"))
            {
                colRate_20RF.Visible = true;
                colRate_20RF.VisibleIndex = VisibleIndex + 9;
            }
            if (unitList.Contains("20HQ"))
            {
                colRate_20HQ.Visible = true;
                colRate_20HQ.VisibleIndex = VisibleIndex + 10;
            }
            if (unitList.Contains("20TK"))
            {
                colRate_20TK.Visible = true;
                colRate_20TK.VisibleIndex = VisibleIndex + 11;
            }
            if (unitList.Contains("20OT"))
            {
                colRate_20OT.Visible = true;
                colRate_20OT.VisibleIndex = VisibleIndex + 12;
            }
            if (unitList.Contains("20HT"))
            {
                colRate_20HT.Visible = true;
                colRate_20HT.VisibleIndex = VisibleIndex + 13;
            }
            #endregion

            #region 40
            if (unitList.Contains("40TK"))
            {
                colRate_40TK.Visible = true;
                colRate_40TK.VisibleIndex = VisibleIndex + 14;
            }
            if (unitList.Contains("40OT"))
            {
                colRate_40OT.Visible = true;
                colRate_40OT.VisibleIndex = VisibleIndex + 15;
            }
            if (unitList.Contains("40FR"))
            {
                colRate_40FR.Visible = true;
                colRate_40FR.VisibleIndex = VisibleIndex + 16;
            }
            if (unitList.Contains("40HT"))
            {
                colRate_40HT.Visible = true;
                colRate_40HT.VisibleIndex = VisibleIndex + 17;
            }
            if (unitList.Contains("40RH"))
            {
                colRate_40RH.Visible = true;
                colRate_40RH.VisibleIndex = VisibleIndex + 18;
            }
            if (unitList.Contains("40RF"))
            {
                colRate_40RF.Visible = true;
                colRate_40RF.VisibleIndex = VisibleIndex + 19;
            }

            #endregion

            #region 45
            if (unitList.Contains("45GP"))
            {
                colRate_45GP.Visible = true;
                colRate_45GP.VisibleIndex = VisibleIndex + 20;
            }
            if (unitList.Contains("45RF"))
            {
                colRate_45RF.Visible = true;
                colRate_45RF.VisibleIndex = VisibleIndex + 21;
            }
            if (unitList.Contains("45HT"))
            {
                colRate_45HT.Visible = true;
                colRate_45HT.VisibleIndex = VisibleIndex + 22;
            }
            if (unitList.Contains("45FR"))
            {
                colRate_45FR.Visible = true;
                colRate_45FR.VisibleIndex = VisibleIndex + 23;
            }
            if (unitList.Contains("45OT"))
            {
                colRate_45OT.Visible = true;
                colRate_45OT.VisibleIndex = VisibleIndex + 24;
            }
            if (unitList.Contains("45TK"))
            {
                colRate_45TK.Visible = true;
                colRate_45TK.VisibleIndex = VisibleIndex + 25;
            }
            if (unitList.Contains("45RH"))
            {
                colRate_45RH.Visible = true;
                colRate_45RH.VisibleIndex = VisibleIndex + 26;
            }
            if (unitList.Contains("53HQ"))
            {
                colRate_53HQ.Visible = true;
                colRate_53HQ.VisibleIndex = VisibleIndex + 27;
            }
            #endregion


            #endregion

            colCommodity.VisibleIndex = VisibleIndex + 28;
            colTerm.VisibleIndex = VisibleIndex + 29;
            colSurCharge.VisibleIndex = VisibleIndex + 30;
            colCLS.VisibleIndex = VisibleIndex + 31;
            colTT.VisibleIndex = VisibleIndex + 32;
            //colDurationFrom.VisibleIndex = VisibleIndex + 33;
            //colDurationTo.VisibleIndex = VisibleIndex + 34;
            colDescription.VisibleIndex = VisibleIndex + 33;
            colCurrency.VisibleIndex = VisibleIndex + 34;



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
        /// 设置最价格为粗体、特价时船东为红色背景
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //是否为建议卖价
            SearchOceanRateList data = gvMain.GetRow(e.RowHandle) as SearchOceanRateList;
            if (data != null && data.PROCount > 0 && e.Column == colCarrier)
            {
                e.Appearance.BackColor = Color.Pink;
            }

            //是否为特价
            if (IsLowestPrice(e))
            {
                e.Appearance.Options.UseFont = true;
                e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
            }

            if (e.Column == colPOL || e.Column == colPOD || e.Column == colDelivery)
            {
                if (data != null)
                {
                    if (data.Statue == SearchPriceStatus.EXPIRED)
                    {
                        //无效的
                        GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
                        e.Appearance.BackColor = Color.LightGray;
                    }
                    else if (data.Statue == SearchPriceStatus.WILLBEEFFECTIVE)
                    {
                        //将要生效的
                        e.Appearance.Options.UseFont = true;
                        e.Appearance.BackColor = Color.ForestGreen;
                    }

                }
            }

        }

        private bool IsLowestPrice(RowCellStyleEventArgs e)
        {
            if (!rateColumnNameList.Contains(e.Column.FieldName))
            {
                return false;
            }
            if (e.CellValue == null)
            {
                return false;
            }
            decimal value = decimal.Parse(e.CellValue.ToString());
            decimal minValue = 0;
            if (value == 0)
            {
                return false;
            }

            switch (e.Column.FieldName)
            {
                case "Rate_20HQ":
                    minValue = (from d in DataList where d.Rate_20HQ > 0 select d.Rate_20HQ).Count() == 0 ? 0 : (from d in DataList where d.Rate_20HQ > 0 select d.Rate_20HQ).Min().Value;
                    break;
                case "Rate_20HT":
                    minValue = (from d in DataList where d.Rate_20HT > 0 select d.Rate_20HT).Count() == 0 ? 0 : (from d in DataList where d.Rate_20HT > 0 select d.Rate_20HT).Min().Value;
                    break;
                case "Rate_20OT":
                    minValue = (from d in DataList where d.Rate_20OT > 0 select d.Rate_20OT).Count() == 0 ? 0 : (from d in DataList where d.Rate_20OT > 0 select d.Rate_20OT).Min().Value;
                    break;
                case "Rate_20FR":
                    minValue = (from d in DataList where d.Rate_20FR > 0 select d.Rate_20FR).Count() == 0 ? 0 : (from d in DataList where d.Rate_20FR > 0 select d.Rate_20FR).Min().Value;
                    break; ;
                case "Rate_20RF":
                    minValue = (from d in DataList where d.Rate_20RF > 0 select d.Rate_20RF).Count() == 0 ? 0 : (from d in DataList where d.Rate_20RF > 0 select d.Rate_20RF).Min().Value;
                    break;
                case "Rate_20RH":
                    minValue = (from d in DataList where d.Rate_20RH > 0 select d.Rate_20RH).Count() == 0 ? 0 : (from d in DataList where d.Rate_20RH > 0 select d.Rate_20RH).Min().Value;
                    break;
                case "Rate_20TK":
                    minValue = (from d in DataList where d.Rate_20TK > 0 select d.Rate_20TK).Count() == 0 ? 0 : (from d in DataList where d.Rate_20TK > 0 select d.Rate_20TK).Min().Value;
                    break;
                case "Rate_40FR":
                    minValue = (from d in DataList where d.Rate_40FR > 0 select d.Rate_40FR).Count() == 0 ? 0 : (from d in DataList where d.Rate_40FR > 0 select d.Rate_40FR).Min().Value;
                    break;
                case "Rate_40HT":
                    minValue = (from d in DataList where d.Rate_40HT > 0 select d.Rate_40HT).Count() == 0 ? 0 : (from d in DataList where d.Rate_40HT > 0 select d.Rate_40HT).Min().Value;
                    break;
                case "Rate_40OT":
                    minValue = (from d in DataList where d.Rate_40OT > 0 select d.Rate_40OT).Count() == 0 ? 0 : (from d in DataList where d.Rate_40OT > 0 select d.Rate_40OT).Min().Value;
                    break;
                case "Rate_40RF":
                    minValue = (from d in DataList where d.Rate_40RF > 0 select d.Rate_40RF).Count() == 0 ? 0 : (from d in DataList where d.Rate_40RF > 0 select d.Rate_40RF).Min().Value;
                    break;
                case "Rate_40RH":
                    minValue = (from d in DataList where d.Rate_40RH > 0 select d.Rate_40RH).Count() == 0 ? 0 : (from d in DataList where d.Rate_40RH > 0 select d.Rate_40RH).Min().Value;
                    break;
                case "Rate_40TK":
                    minValue = (from d in DataList where d.Rate_40TK > 0 select d.Rate_40TK).Count() == 0 ? 0 : (from d in DataList where d.Rate_40TK > 0 select d.Rate_40TK).Min().Value;
                    break;
                case "Rate_45FR":
                    minValue = (from d in DataList where d.Rate_45FR > 0 select d.Rate_45FR).Count() == 0 ? 0 : (from d in DataList where d.Rate_45FR > 0 select d.Rate_45FR).Min().Value;
                    break;
                case "Rate_45GP":
                    minValue = (from d in DataList where d.Rate_45GP > 0 select d.Rate_45GP).Count() == 0 ? 0 : (from d in DataList where d.Rate_45GP > 0 select d.Rate_45GP).Min().Value;
                    break;
                case "Rate_45HT":
                    minValue = (from d in DataList where d.Rate_45HT > 0 select d.Rate_45HT).Count() == 0 ? 0 : (from d in DataList where d.Rate_45HT > 0 select d.Rate_45HT).Min().Value;
                    break;
                case "Rate_45RF":
                    minValue = (from d in DataList where d.Rate_45RF > 0 select d.Rate_45RF).Count() == 0 ? 0 : (from d in DataList where d.Rate_45RF > 0 select d.Rate_45RF).Min().Value;
                    break;
                case "Rate_45RH":
                    minValue = (from d in DataList where d.Rate_45RH > 0 select d.Rate_45RH).Count() == 0 ? 0 : (from d in DataList where d.Rate_45RH > 0 select d.Rate_45RH).Min().Value;
                    break;
                case "Rate_45TK":
                    minValue = (from d in DataList where d.Rate_45TK > 0 select d.Rate_45TK).Count() == 0 ? 0 : (from d in DataList where d.Rate_45TK > 0 select d.Rate_45TK).Min().Value;
                    break;
                case "Rate_45OT":
                    minValue = (from d in DataList where d.Rate_45OT > 0 select d.Rate_45OT).Count() == 0 ? 0 : (from d in DataList where d.Rate_45OT > 0 select d.Rate_45OT).Min().Value;
                    break;
                case "Rate_40NOR":
                    minValue = (from d in DataList where d.Rate_40NOR > 0 select d.Rate_40NOR).Count() == 0 ? 0 : (from d in DataList where d.Rate_40NOR > 0 select d.Rate_40NOR).Min().Value;
                    break;
                case "Rate_20GP":
                    minValue = (from d in DataList where d.Rate_20GP > 0 select d.Rate_20GP).Count() == 0 ? 0 : (from d in DataList where d.Rate_20GP > 0 select d.Rate_20GP).Min().Value;
                    break;
                case "Rate_40HQ":
                    minValue = (from d in DataList where d.Rate_40HQ > 0 select d.Rate_40HQ).Count() == 0 ? 0 : (from d in DataList where d.Rate_40HQ > 0 select d.Rate_40HQ).Min().Value;
                    break;
                case "Rate_45HQ":
                    minValue = (from d in DataList where d.Rate_45HQ > 0 select d.Rate_45HQ).Count() == 0 ? 0 : (from d in DataList where d.Rate_45HQ > 0 select d.Rate_45HQ).Min().Value;
                    break;
                case "Rate_20NOR":
                    minValue = (from d in DataList where d.Rate_20NOR > 0 select d.Rate_20NOR).Count() == 0 ? 0 : (from d in DataList where d.Rate_20NOR > 0 select d.Rate_20NOR).Min().Value;
                    break;
                case "Rate_40GP":
                    minValue = (from d in DataList where d.Rate_40GP > 0 select d.Rate_40GP).Count() == 0 ? 0 : (from d in DataList where d.Rate_40GP > 0 select d.Rate_40GP).Min().Value;
                    break;
                case "Rate_53HQ":
                    minValue = (from d in DataList where d.Rate_53HQ > 0 select d.Rate_53HQ).Count() == 0 ? 0 : (from d in DataList where d.Rate_53HQ > 0 select d.Rate_53HQ).Min().Value;
                    break;

            }
            if (value == minValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 命令

        [CommandHandler(SearchOceanCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object o, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<SearchOceanRateList> list = DataList;
                if (list == null || list.Count == 0) return;

                List<Guid> ids = new List<Guid>();
                foreach (var item in list) { ids.Add(item.ID); }

                SearchPriceStatus status = list[0].Statue;

                List<SearchOceanRateList> newList = SearchRatesService.GetSearchOceanList(ids.ToArray());
                #region 根据权限生定义是否显示底价
                foreach (var item in newList)
                {
                    if (Utility.SearchOceanPermissionType == SearchPricePermission.ViewReserve)
                    {
                        foreach (var unit in item.UnitList) unit.Rate = unit.ReserveRate;
                    }
                    else
                    {
                        foreach (var unit in item.UnitList) unit.Rate = unit.SalesRate;
                    }
                    //还原状态
                    item.Statue = status;
                }
                #endregion

                #region 转换运价
                foreach (var item in newList)
                {
                    foreach (var unitItem in item.UnitList)
                    {
                        switch (unitItem.UnitName)
                        {
                            case "45FR":
                                if (IsViewReserve)
                                {
                                    item.Rate_45FR = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_45FR = unitItem.SalesRate;
                                }
                                item.Rate_45FR_Sales = unitItem.SalesRate;
                                break;
                            case "40RF":
                                if (IsViewReserve)
                                {
                                    item.Rate_40RF = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_40RF = unitItem.SalesRate;
                                }
                                item.Rate_40RF_Sales = unitItem.SalesRate;
                                break;
                            case "45HT":
                                if (IsViewReserve)
                                {
                                    item.Rate_45HT = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_45HT = unitItem.SalesRate;
                                }
                                item.Rate_45HT_Sales = unitItem.SalesRate;
                                break;
                            case "20RF":
                                if (IsViewReserve)
                                {
                                    item.Rate_20RF = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_20RF = unitItem.SalesRate;
                                }
                                item.Rate_20RF_Sales = unitItem.SalesRate;
                                break;
                            case "20HQ":
                                if (IsViewReserve)
                                {
                                    item.Rate_20HQ = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_20HQ = unitItem.SalesRate;
                                }
                                item.Rate_20HQ_Sales = unitItem.SalesRate;
                                break;
                            case "20TK":
                                if (IsViewReserve)
                                {
                                    item.Rate_20TK = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_20TK = unitItem.SalesRate;
                                }
                                item.Rate_20TK_Sales = unitItem.SalesRate;
                                break;
                            case "20GP":
                                if (IsViewReserve)
                                {
                                    item.Rate_20GP = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_20GP = unitItem.SalesRate;
                                }
                                item.Rate_20GP_Sales = unitItem.SalesRate;
                                break;
                            case "40TK":
                                if (IsViewReserve)
                                {
                                    item.Rate_40TK = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_40TK = unitItem.SalesRate;
                                }
                                item.Rate_40TK_Sales = unitItem.SalesRate;
                                break;
                            case "40OT":
                                if (IsViewReserve)
                                {
                                    item.Rate_40OT = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_40OT = unitItem.SalesRate;
                                }
                                item.Rate_40OT_Sales = unitItem.SalesRate;
                                break;
                            case "20FR":
                                if (IsViewReserve)
                                {
                                    item.Rate_20FR = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_20FR = unitItem.SalesRate;
                                }
                                item.Rate_20FR_Sales = unitItem.SalesRate;
                                break;
                            case "45GP":
                                if (IsViewReserve)
                                {
                                    item.Rate_45GP = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_45GP = unitItem.SalesRate;
                                }
                                item.Rate_45GP_Sales = unitItem.SalesRate;
                                break;
                            case "40GP":
                                if (IsViewReserve)
                                {
                                    item.Rate_40GP = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_40GP = unitItem.SalesRate;
                                }
                                item.Rate_40GP_Sales = unitItem.SalesRate;
                                break;
                            case "45RF":
                                if (IsViewReserve)
                                {
                                    item.Rate_45RF = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_45RF = unitItem.SalesRate;
                                }
                                item.Rate_45RF_Sales = unitItem.SalesRate;
                                break;
                            case "20RH":

                                if (IsViewReserve)
                                {
                                    item.Rate_20RH = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_20RH = unitItem.SalesRate;
                                }
                                item.Rate_20RH_Sales = unitItem.SalesRate;
                                break;
                            case "45OT":
                                if (IsViewReserve)
                                {
                                    item.Rate_45OT = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_45OT = unitItem.SalesRate;
                                }
                                item.Rate_45OT_Sales = unitItem.SalesRate;
                                break;
                            case "40NOR":
                                if (IsViewReserve)
                                {
                                    item.Rate_40NOR = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_40NOR = unitItem.SalesRate;
                                }
                                item.Rate_40NOR_Sales = unitItem.SalesRate;
                                break;
                            case "40FR":
                                if (IsViewReserve)
                                {
                                    item.Rate_40FR = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_40FR = unitItem.SalesRate;
                                }
                                item.Rate_40FR_Sales = unitItem.SalesRate;
                                break;
                            case "20OT":
                                if (IsViewReserve)
                                {
                                    item.Rate_20OT = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_20OT = unitItem.SalesRate;
                                }
                                item.Rate_20OT_Sales = unitItem.SalesRate;
                                break;
                            case "45TK":
                                if (IsViewReserve)
                                {
                                    item.Rate_45TK = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_45TK = unitItem.SalesRate;
                                }
                                item.Rate_45TK_Sales = unitItem.SalesRate;
                                break;
                            case "20NOR":
                                if (IsViewReserve)
                                {
                                    item.Rate_20NOR = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_20NOR = unitItem.SalesRate;
                                }
                                item.Rate_20NOR_Sales = unitItem.SalesRate;
                                break;
                            case "40HT":
                                if (IsViewReserve)
                                {
                                    item.Rate_40HT = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_40HT = unitItem.SalesRate;
                                }
                                item.Rate_40HT_Sales = unitItem.SalesRate;
                                break;
                            case "40RH":
                                if (IsViewReserve)
                                {
                                    item.Rate_40RH = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_40RH = unitItem.SalesRate;
                                }
                                item.Rate_40RH_Sales = unitItem.SalesRate;
                                break;
                            case "45RH":
                                if (IsViewReserve)
                                {
                                    item.Rate_45RH = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_45RH = unitItem.SalesRate;
                                }

                                item.Rate_45RH_Sales = unitItem.SalesRate;
                                break;
                            case "45HQ":
                                if (IsViewReserve)
                                {
                                    item.Rate_45HQ = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_45HQ = unitItem.SalesRate;
                                }
                                item.Rate_45HQ_Sales = unitItem.SalesRate;
                                break;
                            case "20HT":
                                if (IsViewReserve)
                                {
                                    item.Rate_20HT = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_20HT = unitItem.SalesRate;
                                }
                                item.Rate_20HT = unitItem.SalesRate;
                                break;
                            case "40HQ":
                                if (IsViewReserve)
                                {
                                    item.Rate_40HQ = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_40HQ = unitItem.SalesRate;
                                }
                                item.Rate_40HQ_Sales = unitItem.SalesRate;
                                break;
                            case "53HQ":
                                if (IsViewReserve)
                                {
                                    item.Rate_53HQ = unitItem.ReserveRate;
                                }
                                else
                                {
                                    item.Rate_53HQ = unitItem.SalesRate;
                                }

                                item.Rate_53HQ_Sales = unitItem.SalesRate;
                                break;
                        }
                    }
                }
                #endregion

                bsList.DataSource = newList;
                bsList.ResetBindings(false);
            }
        }

        #endregion

        #region 显示底价
        private void gvMain_MouseMove(object sender, MouseEventArgs e)
        {
            GridHitInfo hitInfo = gvMain.CalcHitInfo(e.Location);

            if (!IsViewReserve)
            {
                return;
            }
            if (hitInfo.InRowCell == false || hitInfo.RowHandle < 0)
            {
                return;
            }
            if (!rateColumn.Contains(hitInfo.Column.Name))
            {
                return;
            }
            string message = LocalData.IsEnglish ? "SalesRate:" : "建议卖价:";

            SearchOceanRateList item = gvMain.GetRow(hitInfo.RowHandle) as SearchOceanRateList;
            if (item == null)
            {
                return;
            }
            string s = toolTip1.GetToolTip(this);
            //if (toolTip1.Active)
            //{
            //    return;
            //}
            decimal rate = GetSalsTest(item, hitInfo.Column.Name);
            if (rate == 0)
            {
                return;
            }
            message = message + rate.ToString("F2");
            Point pt = gcMain.PointToClient(MousePosition);
            pt.X += 20;
            pt.Y += 30;
            toolTip1.Show(message, this, pt, 5000);

        }
        private decimal GetSalsTest(SearchOceanRateList item, string colName)
        {
            decimal? rate = 0;

            #region 找价格
            switch (colName)
            {
                case "colRate_45FR":
                    if (item.Rate_45FR_Sales != item.Rate_45FR)
                    {
                        rate = item.Rate_45FR_Sales;
                    }
                    break;
                case "colRate_40RF":
                    if (item.Rate_40RF_Sales != item.Rate_40RF)
                    {
                        rate = item.Rate_40RF_Sales;
                    }
                    break;
                case "colRate_45HT":
                    if (item.Rate_45HT_Sales != item.Rate_45HT)
                    {
                        rate = item.Rate_45HT_Sales;
                    }
                    break;
                case "colRate_20RF":
                    if (item.Rate_20RF_Sales != item.Rate_20RF)
                    {
                        rate = item.Rate_20RF_Sales;
                    }
                    break;
                case "colRate_20HQ":
                    if (item.Rate_20HQ_Sales != item.Rate_20HQ)
                    {
                        rate = item.Rate_20HQ_Sales;
                    }
                    break;
                case "colRate_20TK":
                    if (item.Rate_20TK_Sales != item.Rate_20TK)
                    {
                        rate = item.Rate_20TK_Sales;
                    }
                    break;
                case "colRate_20GP":
                    if (item.Rate_20GP_Sales != item.Rate_20GP)
                    {
                        rate = item.Rate_20GP_Sales;
                    }
                    break;
                case "colRate_40TK":
                    if (item.Rate_40TK_Sales != item.Rate_40TK)
                    {
                        rate = item.Rate_40TK_Sales;
                    }
                    break;
                case "colRate_40OT":
                    if (item.Rate_40OT_Sales != item.Rate_40OT)
                    {
                        rate = item.Rate_40OT_Sales;
                    }
                    break;
                case "colRate_20FR":
                    if (item.Rate_20FR_Sales != item.Rate_20FR)
                    {
                        rate = item.Rate_20FR_Sales;
                    }
                    break;
                case "colRate_45GP":
                    if (item.Rate_45GP_Sales != item.Rate_45GP)
                    {
                        rate = item.Rate_45GP_Sales;
                    }
                    break;
                case "colRate_40GP":
                    if (item.Rate_40GP_Sales != item.Rate_40GP)
                    {
                        rate = item.Rate_40GP_Sales;
                    }
                    break;
                case "colRate_45RF":
                    if (item.Rate_45RF_Sales != item.Rate_45RF)
                    {
                        rate = item.Rate_45RF_Sales;
                    }
                    break;
                case "colRate_20RH":
                    if (item.Rate_20RH_Sales != item.Rate_20RH)
                    {
                        rate = item.Rate_20RH_Sales;
                    }
                    break;
                case "colRate_45OT":
                    if (item.Rate_45OT_Sales != item.Rate_45OT)
                    {
                        rate = item.Rate_45OT_Sales;
                    }
                    break;
                case "colRate_40NOR":
                    if (item.Rate_40NOR_Sales != item.Rate_40NOR)
                    {
                        rate = item.Rate_40NOR_Sales;
                    }
                    break;
                case "colRate_40FR":
                    if (item.Rate_40FR_Sales != item.Rate_40FR)
                    {
                        rate = item.Rate_40FR_Sales;
                    }
                    break;
                case "colRate_20OT":
                    if (item.Rate_20OT_Sales != item.Rate_20OT)
                    {
                        rate = item.Rate_20OT_Sales;
                    }
                    break;
                case "colRate_45TK":
                    if (item.Rate_45TK_Sales != item.Rate_45TK)
                    {
                        rate = item.Rate_45TK_Sales;
                    }
                    break;
                case "colRate_20NOR":
                    if (item.Rate_20NOR_Sales != item.Rate_20NOR)
                    {
                        rate = item.Rate_20NOR_Sales;
                    }
                    break;
                case "colRate_40HT":
                    if (item.Rate_40HT_Sales != item.Rate_40HT)
                    {
                        rate = item.Rate_40HT_Sales;
                    }
                    break;
                case "colRate_40RH":
                    if (item.Rate_40RH_Sales != item.Rate_40RH)
                    {
                        rate = item.Rate_40RH_Sales;
                    }
                    break;
                case "colRate_45RH":
                    if (item.Rate_45RH_Sales != item.Rate_45RH)
                    {
                        rate = item.Rate_45RH_Sales;
                    }
                    break;
                case "colRate_45HQ":
                    if (item.Rate_45HQ_Sales != item.Rate_45HQ)
                    {
                        rate = item.Rate_45HQ_Sales;
                    }
                    break;
                case "colRate_20HT":
                    if (item.Rate_20HT_Sales != item.Rate_20HT)
                    {
                        rate = item.Rate_20HT_Sales;
                    }
                    break;
                case "colRate_40HQ":
                    if (item.Rate_40HQ_Sales != item.Rate_40HQ)
                    {
                        rate = item.Rate_40HQ_Sales;
                    }
                    break;
                case "colRate_53HQ":
                    if (item.Rate_53HQ_Sales != item.Rate_53HQ)
                    {
                        rate = item.Rate_53HQ_Sales;
                    }
                    break;
            }
            #endregion;

            if (rate == null || rate == 0)
            {
                return 0;
            }
            else
            {
                return rate.Value;
            }

        }
        #endregion

        #region 单元格被点击时
        /// <summary>
        /// 单元格被点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (CurrentRow != null && e.Column == colCheck)
            {
                CurrentRow.IsCheck = !CurrentRow.IsCheck;
                bsList.ResetCurrentItem();
            }
        }
        #endregion
    }
}
