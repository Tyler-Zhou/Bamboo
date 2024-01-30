namespace ICP.FRM.UI.SearchRate
{
    partial class SearchTruckListPart
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchTruckListPart));
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.pageControl1 = new ICP.Framework.ClientComponents.Controls.PageControl();
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colStatue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbStatue = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.colCarrierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colZipCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFUEL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDurationFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDurationTo = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbStatue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FRM.ServiceInterface.DataObjects.SearchTruckRateList);
            // 
            // pageControl1
            // 
            this.pageControl1.CurrentPage = 0;
            this.pageControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pageControl1.Location = new System.Drawing.Point(0, 377);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.Size = new System.Drawing.Size(900, 26);
            this.pageControl1.TabIndex = 4;
            this.pageControl1.TotalPage = 0;
            this.pageControl1.PageChanged += new ICP.Framework.ClientComponents.Controls.PageChangedHandler(this.pageControl1_PageChanged);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbStatue,
            this.rSpinEdit1});
            this.gcMain.Size = new System.Drawing.Size(900, 377);
            this.gcMain.TabIndex = 5;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStatue,
            this.colCarrierName,
            this.colFrom,
            this.colTo,
            this.colCurrency,
            this.colRate,
            this.colZipCode,
            this.colFUEL,
            this.colTotal,
            this.colDurationFrom,
            this.colDurationTo});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 27;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsBehavior.KeepGroupExpandedOnSorting = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.CustomerSorting += new ICP.Framework.ClientComponents.Controls.CustomerSortingHandler(this.gvMain_CustomerSorting);
            this.gvMain.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvMain_RowCellStyle);
            // 
            // colStatue
            // 
            this.colStatue.ColumnEdit = this.rcmbStatue;
            this.colStatue.FieldName = "Statue";
            this.colStatue.Name = "colStatue";
            this.colStatue.Visible = true;
            this.colStatue.VisibleIndex = 0;
            // 
            // rcmbStatue
            // 
            this.rcmbStatue.AutoHeight = false;
            this.rcmbStatue.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbStatue.Name = "rcmbStatue";
            this.rcmbStatue.SmallImages = this.imageList1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "EFFECTIVE.png");
            this.imageList1.Images.SetKeyName(1, "WILL BE EFFECTIVE.png");
            this.imageList1.Images.SetKeyName(2, "EXPIRED.png");
            // 
            // colCarrierName
            // 
            this.colCarrierName.Caption = "Carrier";
            this.colCarrierName.FieldName = "CarrierName";
            this.colCarrierName.Name = "colCarrierName";
            this.colCarrierName.Visible = true;
            this.colCarrierName.VisibleIndex = 1;
            this.colCarrierName.Width = 117;
            // 
            // colFrom
            // 
            this.colFrom.Caption = "From";
            this.colFrom.FieldName = "From";
            this.colFrom.Name = "colFrom";
            this.colFrom.Visible = true;
            this.colFrom.VisibleIndex = 2;
            this.colFrom.Width = 116;
            // 
            // colTo
            // 
            this.colTo.Caption = "To";
            this.colTo.FieldName = "To";
            this.colTo.Name = "colTo";
            this.colTo.Visible = true;
            this.colTo.VisibleIndex = 3;
            this.colTo.Width = 124;
            // 
            // colCurrency
            // 
            this.colCurrency.FieldName = "Currency";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 5;
            this.colCurrency.Width = 70;
            // 
            // colRate
            // 
            this.colRate.ColumnEdit = this.rSpinEdit1;
            this.colRate.FieldName = "Rate";
            this.colRate.Name = "colRate";
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 6;
            // 
            // rSpinEdit1
            // 
            this.rSpinEdit1.AutoHeight = false;
            this.rSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rSpinEdit1.DisplayFormat.FormatString = "F2";
            this.rSpinEdit1.Name = "rSpinEdit1";
            // 
            // colZipCode
            // 
            this.colZipCode.Caption = "Zip Code";
            this.colZipCode.FieldName = "ZipCode";
            this.colZipCode.Name = "colZipCode";
            this.colZipCode.Visible = true;
            this.colZipCode.VisibleIndex = 4;
            // 
            // colFUEL
            // 
            this.colFUEL.ColumnEdit = this.rSpinEdit1;
            this.colFUEL.FieldName = "FUEL";
            this.colFUEL.Name = "colFUEL";
            this.colFUEL.Visible = true;
            this.colFUEL.VisibleIndex = 7;
            // 
            // colTotal
            // 
            this.colTotal.ColumnEdit = this.rSpinEdit1;
            this.colTotal.FieldName = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 8;
            // 
            // colDurationFrom
            // 
            this.colDurationFrom.FieldName = "DurationFrom";
            this.colDurationFrom.Name = "colDurationFrom";
            this.colDurationFrom.Visible = true;
            this.colDurationFrom.VisibleIndex = 9;
            this.colDurationFrom.Width = 99;
            // 
            // colDurationTo
            // 
            this.colDurationTo.FieldName = "DurationTo";
            this.colDurationTo.Name = "colDurationTo";
            this.colDurationTo.Visible = true;
            this.colDurationTo.VisibleIndex = 10;
            this.colDurationTo.Width = 118;
            // 
            // SearchTruckListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.pageControl1);
            this.IsMultiLanguage = false;
            this.Name = "SearchTruckListPart";
            this.Size = new System.Drawing.Size(900, 403);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbStatue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private ICP.Framework.ClientComponents.Controls.PageControl pageControl1;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraGrid.Columns.GridColumn colStatue;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbStatue;
        private DevExpress.XtraGrid.Columns.GridColumn colCarrierName;
        private DevExpress.XtraGrid.Columns.GridColumn colFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colTo;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rSpinEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colDurationFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colDurationTo;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private DevExpress.XtraGrid.Columns.GridColumn colFUEL;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colZipCode;
    }
}
