namespace ICP.FAM.UI
{
    partial class ImportUFBeginBalanceListPart
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
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDetail = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barSpreadsheet = new DevExpress.XtraBars.BarButtonItem();
            this.barExport = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colGLCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGLName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrgAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colBalanceDirection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbBalanceDirection = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colBalanceAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbError = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rcmbChangeState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cmbCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.cmbYear = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labYear = new DevExpress.XtraEditors.LabelControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBalanceDirection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbChangeState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
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
            this.barSave,
            this.barAdd,
            this.barClose,
            this.barDetail,
            this.barSpreadsheet,
            this.barExport,
            this.barRefresh});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 10;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(135, 145);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDetail, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSpreadsheet, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barExport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "新增(&A)";
            this.barAdd.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.barAdd.Id = 4;
            this.barAdd.Name = "barAdd";
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barSave
            // 
            this.barSave.Caption = "保存(&S)";
            this.barSave.Glyph = global::ICP.FAM.UI.Properties.Resources.Save_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barDetail
            // 
            this.barDetail.Caption = "详细";
            this.barDetail.Glyph = global::ICP.FAM.UI.Properties.Resources.BillDetails;
            this.barDetail.Id = 6;
            this.barDetail.Name = "barDetail";
            this.barDetail.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDetail_ItemClick);
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "刷新";
            this.barRefresh.Glyph = global::ICP.FAM.UI.Properties.Resources.Refresh_16;
            this.barRefresh.Id = 9;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barSpreadsheet
            // 
            this.barSpreadsheet.Caption = "试算";
            this.barSpreadsheet.Glyph = global::ICP.FAM.UI.Properties.Resources.ChangeReleaseType;
            this.barSpreadsheet.Id = 7;
            this.barSpreadsheet.Name = "barSpreadsheet";
            this.barSpreadsheet.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSpreadsheet_ItemClick);
            // 
            // barExport
            // 
            this.barExport.Caption = "导出";
            this.barExport.Glyph = global::ICP.FAM.UI.Properties.Resources.Transfer_16;
            this.barExport.Id = 8;
            this.barExport.Name = "barExport";
            this.barExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barExport_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭(&C)";
            this.barClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Close_16;
            this.barClose.Id = 5;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(993, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 541);
            this.barDockControlBottom.Size = new System.Drawing.Size(993, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 515);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(993, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 515);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(2, 2);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rSpinEdit1,
            this.cmbBalanceDirection,
            this.cmbError,
            this.rcmbChangeState});
            this.gcMain.Size = new System.Drawing.Size(989, 483);
            this.gcMain.TabIndex = 5;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.BeginBalances);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGLCode,
            this.colGLName,
            this.colOrgAmt,
            this.colBalanceDirection,
            this.colBalanceAmount});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.RowAutoHeight = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvMain.DoubleClick += new System.EventHandler(this.gvMain_DoubleClick);
            // 
            // colGLCode
            // 
            this.colGLCode.Caption = "科目代码";
            this.colGLCode.FieldName = "GLCode";
            this.colGLCode.Name = "colGLCode";
            this.colGLCode.Visible = true;
            this.colGLCode.VisibleIndex = 0;
            this.colGLCode.Width = 159;
            // 
            // colGLName
            // 
            this.colGLName.Caption = "科目名称";
            this.colGLName.FieldName = "GLName";
            this.colGLName.Name = "colGLName";
            this.colGLName.OptionsColumn.AllowEdit = false;
            this.colGLName.Visible = true;
            this.colGLName.VisibleIndex = 1;
            this.colGLName.Width = 415;
            // 
            // colOrgAmt
            // 
            this.colOrgAmt.Caption = "原币金额";
            this.colOrgAmt.ColumnEdit = this.rSpinEdit1;
            this.colOrgAmt.DisplayFormat.FormatString = "n";
            this.colOrgAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colOrgAmt.FieldName = "OrgAmt";
            this.colOrgAmt.Name = "colOrgAmt";
            this.colOrgAmt.Visible = true;
            this.colOrgAmt.VisibleIndex = 2;
            this.colOrgAmt.Width = 135;
            // 
            // rSpinEdit1
            // 
            this.rSpinEdit1.AutoHeight = false;
            this.rSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rSpinEdit1.Mask.EditMask = "F2";
            this.rSpinEdit1.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.rSpinEdit1.Name = "rSpinEdit1";
            // 
            // colBalanceDirection
            // 
            this.colBalanceDirection.Caption = "余额方向";
            this.colBalanceDirection.ColumnEdit = this.cmbBalanceDirection;
            this.colBalanceDirection.FieldName = "GLCodeProperty";
            this.colBalanceDirection.Name = "colBalanceDirection";
            this.colBalanceDirection.Visible = true;
            this.colBalanceDirection.VisibleIndex = 3;
            this.colBalanceDirection.Width = 81;
            // 
            // cmbBalanceDirection
            // 
            this.cmbBalanceDirection.AutoHeight = false;
            this.cmbBalanceDirection.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBalanceDirection.Name = "cmbBalanceDirection";
            // 
            // colBalanceAmount
            // 
            this.colBalanceAmount.Caption = "余额";
            this.colBalanceAmount.ColumnEdit = this.rSpinEdit1;
            this.colBalanceAmount.DisplayFormat.FormatString = "n";
            this.colBalanceAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBalanceAmount.FieldName = "Balance";
            this.colBalanceAmount.Name = "colBalanceAmount";
            this.colBalanceAmount.Visible = true;
            this.colBalanceAmount.VisibleIndex = 4;
            this.colBalanceAmount.Width = 161;
            // 
            // cmbError
            // 
            this.cmbError.AutoHeight = false;
            this.cmbError.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbError.Name = "cmbError";
            // 
            // rcmbChangeState
            // 
            this.rcmbChangeState.AllowFocused = false;
            this.rcmbChangeState.AutoHeight = false;
            this.rcmbChangeState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbChangeState.Name = "rcmbChangeState";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.cmbCompany);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.labCompany);
            this.panelControl1.Controls.Add(this.cmbYear);
            this.panelControl1.Controls.Add(this.labYear);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 26);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(993, 28);
            this.panelControl1.TabIndex = 10;
            // 
            // cmbCompany
            // 
            this.cmbCompany.Location = new System.Drawing.Point(40, 4);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Size = new System.Drawing.Size(125, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCompany.TabIndex = 64;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(323, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 67;
            this.btnSearch.Text = "查询(&Q)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(10, 7);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(24, 14);
            this.labCompany.TabIndex = 68;
            this.labCompany.Text = "公司";
            // 
            // cmbYear
            // 
            this.cmbYear.Location = new System.Drawing.Point(213, 4);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbYear.Properties.Appearance.Options.UseBackColor = true;
            this.cmbYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbYear.Size = new System.Drawing.Size(82, 21);
            this.cmbYear.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbYear.TabIndex = 65;
            // 
            // labYear
            // 
            this.labYear.Location = new System.Drawing.Point(181, 7);
            this.labYear.Name = "labYear";
            this.labYear.Size = new System.Drawing.Size(24, 14);
            this.labYear.TabIndex = 69;
            this.labYear.Text = "年份";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gcMain);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(993, 487);
            this.pnlMain.TabIndex = 11;
            // 
            // ImportUFBeginBalanceListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ImportUFBeginBalanceListPart";
            this.Size = new System.Drawing.Size(993, 541);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBalanceDirection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbChangeState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colGLCode;
        private DevExpress.XtraGrid.Columns.GridColumn colOrgAmt;
        private DevExpress.XtraGrid.Columns.GridColumn colBalanceDirection;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rSpinEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colGLName;
        private DevExpress.XtraGrid.Columns.GridColumn colBalanceAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbBalanceDirection;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbError;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbChangeState;
        private DevExpress.XtraBars.BarButtonItem barDetail;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraBars.BarButtonItem barSpreadsheet;
        private DevExpress.XtraBars.BarButtonItem barExport;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        public Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        protected DevExpress.XtraEditors.LabelControl labCompany;
        public Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbYear;
        protected DevExpress.XtraEditors.LabelControl labYear;
    }
}
