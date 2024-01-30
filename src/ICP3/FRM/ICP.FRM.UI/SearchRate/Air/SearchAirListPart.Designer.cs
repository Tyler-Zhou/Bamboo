namespace ICP.FRM.UI.SearchRate
{
    partial class SearchAirListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchAirListPart));
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
            this.colRate45 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colRate100 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate300 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate500 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate800 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate1000 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate1300 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCommodity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSchedule = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRouting = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.bsList.DataSource = typeof(ICP.FRM.UI.SearchRate.ClientSearchAirRateList);
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
            this.colRate45,
            this.colRate100,
            this.colRate300,
            this.colRate500,
            this.colRate800,
            this.colRate1000,
            this.colRate1300,
            this.colCommodity,
            this.colSchedule,
            this.colRouting,
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
            this.colFrom.Caption = "From(Airport)";
            this.colFrom.FieldName = "From";
            this.colFrom.Name = "colFrom";
            this.colFrom.Visible = true;
            this.colFrom.VisibleIndex = 2;
            this.colFrom.Width = 116;
            // 
            // colTo
            // 
            this.colTo.Caption = "To(Airport)";
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
            this.colCurrency.VisibleIndex = 4;
            this.colCurrency.Width = 70;
            // 
            // colRate45
            // 
            this.colRate45.Caption = "+45";
            this.colRate45.ColumnEdit = this.rSpinEdit1;
            this.colRate45.FieldName = "Rate_45";
            this.colRate45.Name = "colRate45";
            // 
            // rSpinEdit1
            // 
            this.rSpinEdit1.AutoHeight = false;
            this.rSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rSpinEdit1.DisplayFormat.FormatString = "F2";
            this.rSpinEdit1.Name = "rSpinEdit1";
            // 
            // colRate100
            // 
            this.colRate100.Caption = "+100";
            this.colRate100.ColumnEdit = this.rSpinEdit1;
            this.colRate100.FieldName = "Rate_100";
            this.colRate100.Name = "colRate100";
            // 
            // colRate300
            // 
            this.colRate300.Caption = "+300";
            this.colRate300.ColumnEdit = this.rSpinEdit1;
            this.colRate300.FieldName = "Rate_300";
            this.colRate300.Name = "colRate300";
            // 
            // colRate500
            // 
            this.colRate500.Caption = "+500";
            this.colRate500.ColumnEdit = this.rSpinEdit1;
            this.colRate500.FieldName = "Rate_500";
            this.colRate500.Name = "colRate500";
            // 
            // colRate800
            // 
            this.colRate800.Caption = "+800";
            this.colRate800.ColumnEdit = this.rSpinEdit1;
            this.colRate800.FieldName = "Rate_800";
            this.colRate800.Name = "colRate800";
            // 
            // colRate1000
            // 
            this.colRate1000.Caption = "+1000";
            this.colRate1000.ColumnEdit = this.rSpinEdit1;
            this.colRate1000.FieldName = "Rate_1000";
            this.colRate1000.Name = "colRate1000";
            // 
            // colRate1300
            // 
            this.colRate1300.Caption = "+1300";
            this.colRate1300.ColumnEdit = this.rSpinEdit1;
            this.colRate1300.FieldName = "Rate_1300";
            this.colRate1300.Name = "colRate1300";
            // 
            // colCommodity
            // 
            this.colCommodity.FieldName = "Commodity";
            this.colCommodity.Name = "colCommodity";
            this.colCommodity.Visible = true;
            this.colCommodity.VisibleIndex = 5;
            this.colCommodity.Width = 98;
            // 
            // colSchedule
            // 
            this.colSchedule.FieldName = "Schedule";
            this.colSchedule.Name = "colSchedule";
            this.colSchedule.Visible = true;
            this.colSchedule.VisibleIndex = 6;
            this.colSchedule.Width = 89;
            // 
            // colRouting
            // 
            this.colRouting.FieldName = "Routing";
            this.colRouting.Name = "colRouting";
            this.colRouting.Visible = true;
            this.colRouting.VisibleIndex = 7;
            this.colRouting.Width = 85;
            // 
            // colDurationFrom
            // 
            this.colDurationFrom.FieldName = "DurationFrom";
            this.colDurationFrom.Name = "colDurationFrom";
            this.colDurationFrom.Visible = true;
            this.colDurationFrom.VisibleIndex = 8;
            this.colDurationFrom.Width = 99;
            // 
            // colDurationTo
            // 
            this.colDurationTo.FieldName = "DurationTo";
            this.colDurationTo.Name = "colDurationTo";
            this.colDurationTo.Visible = true;
            this.colDurationTo.VisibleIndex = 9;
            this.colDurationTo.Width = 118;
            // 
            // SearchAirListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.pageControl1);
            this.IsMultiLanguage = false;
            this.Name = "SearchAirListPart";
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
        private DevExpress.XtraGrid.Columns.GridColumn colRate45;
        private DevExpress.XtraGrid.Columns.GridColumn colRate100;
        private DevExpress.XtraGrid.Columns.GridColumn colRate300;
        private DevExpress.XtraGrid.Columns.GridColumn colRate500;
        private DevExpress.XtraGrid.Columns.GridColumn colRate800;
        private DevExpress.XtraGrid.Columns.GridColumn colRate1000;
        private DevExpress.XtraGrid.Columns.GridColumn colRate1300;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rSpinEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colCommodity;
        private DevExpress.XtraGrid.Columns.GridColumn colSchedule;
        private DevExpress.XtraGrid.Columns.GridColumn colRouting;
        private DevExpress.XtraGrid.Columns.GridColumn colDurationFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colDurationTo;
    }
}
