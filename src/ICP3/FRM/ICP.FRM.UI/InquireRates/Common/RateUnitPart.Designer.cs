namespace ICP.FRM.UI.InquireRates
{
    partial class RateUnitPart
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barInsertRateUnit = new DevExpress.XtraBars.BarButtonItem();
            this.barRemoveRateUnit = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gcRate = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsRateUnit = new System.Windows.Forms.BindingSource(this.components);
            this.gvRate = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUnitID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbRateUnit = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsRateUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbRateUnit)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barInsertRateUnit,
            this.barRemoveRateUnit});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 2;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barInsertRateUnit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barRemoveRateUnit)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barInsertRateUnit
            // 
            this.barInsertRateUnit.Caption = "I&nsert";
            this.barInsertRateUnit.Id = 4;
            this.barInsertRateUnit.Name = "barInsertRateUnit";
            this.barInsertRateUnit.ItemClick+=new DevExpress.XtraBars.ItemClickEventHandler(barInsertRateUnit_ItemClick);
            // 
            // barRemoveRateUnit
            // 
            this.barRemoveRateUnit.Caption = "Re&move";
            this.barRemoveRateUnit.Id = 3;
            this.barRemoveRateUnit.Name = "barRemoveRateUnit";
            this.barRemoveRateUnit.ItemClick+=new DevExpress.XtraBars.ItemClickEventHandler(barRemoveRateUnit_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(371, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 280);
            this.barDockControlBottom.Size = new System.Drawing.Size(371, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 256);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(371, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 256);
            // 
            // gcRate
            // 
            this.gcRate.DataSource = this.bsRateUnit;
            this.gcRate.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcRate.Location = new System.Drawing.Point(0, 24);
            this.gcRate.MainView = this.gvRate;
            this.gcRate.Name = "gcRate";
            this.gcRate.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbRateUnit});
            this.gcRate.Size = new System.Drawing.Size(371, 201);
            this.gcRate.TabIndex = 0;
            this.gcRate.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRate});
            // 
            // bsRateUnit
            // 
            this.bsRateUnit.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.ReportParameterList);
            // 
            // gvRate
            // 
            this.gvRate.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUnitID});
            this.gvRate.GridControl = this.gcRate;
            this.gvRate.Name = "gvRate";
            this.gvRate.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvRate.OptionsSelection.MultiSelect = true;
            this.gvRate.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvRate.OptionsView.EnableAppearanceEvenRow = true;
            this.gvRate.OptionsView.ShowGroupPanel = false;
            // 
            // colUnitID
            // 
            this.colUnitID.Caption = "Unit";
            this.colUnitID.ColumnEdit = this.rcmbRateUnit;
            this.colUnitID.FieldName = "UnitID";
            this.colUnitID.Name = "colUnitID";
            this.colUnitID.Visible = true;
            this.colUnitID.VisibleIndex = 0;
            // 
            // rcmbRateUnit
            // 
            this.rcmbRateUnit.AutoHeight = false;
            this.rcmbRateUnit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbRateUnit.Name = "rcmbRateUnit";
            this.rcmbRateUnit.SelectedIndexChanged += new System.EventHandler(rcmbRateUnit_SelectedIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(283, 241);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 21);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "&Close";
            this.btnClose.Click+=new System.EventHandler(btnClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(188, 241);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 21);
            this.btnOk.TabIndex = 22;
            this.btnOk.Text = "O&k";
            this.btnOk.Click += new System.EventHandler(btnOk_Click);
            // 
            // RateUnitPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gcRate);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "RateUnitPart";
            this.IsMultiLanguage = false;
            this.Size = new System.Drawing.Size(371, 280);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsRateUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbRateUnit)).EndInit();
            this.ResumeLayout(false);

        }   

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barInsertRateUnit;
        private DevExpress.XtraBars.BarButtonItem barRemoveRateUnit;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcRate;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRate;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitID;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbRateUnit;
        private System.Windows.Forms.BindingSource bsRateUnit;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOk;
    }
}
