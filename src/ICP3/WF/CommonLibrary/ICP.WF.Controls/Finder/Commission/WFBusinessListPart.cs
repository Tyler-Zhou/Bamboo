using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Common;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using ICP.WF.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Service;

namespace ICP.WF.Controls.Form.Commission
{
    /// <summary>
    /// 业务列表
    /// </summary>
    [ToolboxItem(false)]
    public partial class WFBusinessListPart : BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 初始化

        public WFBusinessListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.CurrentChanged = null;
                this.InvokeGetData = null;
                this.Selected = null;
                this.gvMain.CustomDrawRowIndicator -= this.gvMain_CustomDrawRowIndicator;
                this.gvMain.CustomerSorting -= this.gvMain_CustomerSorting;
                this.gvMain.DoubleClick -= this.gvMain_DoubleClick;
                this.gcMain.DataSource = null;
                this.bsList.PositionChanged -= this.bsList_PositionChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.pageControl1.PageChanged -= this.pageControl1_PageChanged;
                this._dataPageInfo = null;
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

            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<PaidStatus>> currencyPaidStatue = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<PaidStatus>(LocalData.IsEnglish);
            foreach (var item in currencyPaidStatue)
            {
                cmbPaidStatue.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            this.RegisterMessage("140102001", LocalData.IsEnglish ? "Commission has been paid in full, please contact the financial revoked check" : "佣金已经全部支付,请先联系财务撤销销帐!!!");


        }

        #endregion

        #region 命令


        [CommandHandler(WFBusinessCommandConstants.Command_Select)]
        public void Command_Select(object sender, EventArgs e)
        {
            SelectData();
        }
        /// <summary>
        /// 选择数据
        /// </summary>
        private void SelectData()
        {
            if (CurrentRow != null && this.Selected != null)
            {
                if (CurrentRow.CommissionAmount == CurrentRow.CommissionPayAmount && CurrentRow.CommissionPayAmount>0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(NativeLanguageService.GetText(this, "140102001"));
                    return;
                }

                Selected(this, CurrentRow);
            }

        }

        /// <summary>
        /// 双击选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gvMain_DoubleClick(object sender, System.EventArgs e)
        {
            SelectData();
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected WFBusinessList CurrentRow
        {
            get { return Current as WFBusinessList; }
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

        public List<WFBusinessList> DataSourceList
        {
            get
            {
                List<WFBusinessList> list = bsList.DataSource as List<WFBusinessList>;
                if (list == null)
                {
                    list = new List<WFBusinessList>();
                }
                return list;
            }
        }

        DataPageInfo _dataPageInfo = new DataPageInfo();
        private void BindingData(object value)
        {
            PageList source = value as PageList;
            List<WFBusinessList> list = new List<WFBusinessList>();
            if (source != null) list = source.GetList<WFBusinessList>();
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
                    this.pageControl1.TotalPage = pageCount;
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

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

        public override event InvokeGetDataHandler InvokeGetData;

        public override event SelectedHandler Selected;

        #endregion

        #region gridview event

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }
        #endregion

        #region 事件发布
        private void gvMain_CustomerSorting(object sender, ICP.Framework.ClientComponents.Controls.SortingCancelEventArgs e)
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
        private void gvMain_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1));
            }
        }
        #endregion

        #region 如果不是全部到帐的，则用红色显示
        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            WFBusinessList list = gvMain.GetRow(e.RowHandle) as WFBusinessList;
            if (list != null)
            {
                if (list.PaidStatus != PaidStatus.All &&
                list.PaidStatus != PaidStatus.Unknown)
                {
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        #endregion

    }
}
