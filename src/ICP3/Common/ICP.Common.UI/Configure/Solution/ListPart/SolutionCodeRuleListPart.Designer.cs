namespace ICP.Common.UI.Configure.Solution
{
    partial class SolutionCodeRuleListPart
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colConfigureKeyID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbConfigureKey = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colCodePrefix = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsIncludeCompanyCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCodeYear = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbCodeYear = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colCodeMonth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCodeSNLength = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDisuse = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbConfigureKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCodeYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 26);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbConfigureKey,
            this.cmbCodeYear,
            this.rSpinEdit1,
            this.repositoryItemMemoExEdit1,
            this.repositoryItemTextEdit1});
            this.gridControl1.Size = new System.Drawing.Size(851, 353);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.SolutionCodeRuleList);
            this.bindingSource1.PositionChanged += new System.EventHandler(this.bindingSource1_PositionChanged);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colConfigureKeyID,
            this.colCodePrefix,
            this.colIsIncludeCompanyCode,
            this.colCodeYear,
            this.colCodeMonth,
            this.colCodeSNLength,
            this.colDescription,
            this.colCreateByName,
            this.colCreateDate});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView1.OptionsView.ShowGroupPanel = false;

            this.gridView1.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsSelection.UseIndicatorForSelection = true;
            this.gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView1_InitNewRow);
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.IndicatorWidth = 35;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.mainGridView_CustomDrawRowIndicator);
            // 
            // colConfigureKeyID
            // 
            this.colConfigureKeyID.Caption = "ConfigureKey";
            this.colConfigureKeyID.ColumnEdit = this.cmbConfigureKey;
            this.colConfigureKeyID.FieldName = "ConfigureKeyID";
            this.colConfigureKeyID.Name = "colConfigureKeyID";
            this.colConfigureKeyID.Visible = true;
            this.colConfigureKeyID.VisibleIndex = 0;
            this.colConfigureKeyID.Width = 98;
            // 
            // cmbConfigureKey
            // 
            this.cmbConfigureKey.AutoHeight = false;
            this.cmbConfigureKey.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbConfigureKey.Name = "cmbConfigureKey";
            // 
            // colCodePrefix
            // 
            this.colCodePrefix.Caption = "Prefix";
            this.colCodePrefix.ColumnEdit = this.repositoryItemTextEdit1;
            this.colCodePrefix.FieldName = "CodePrefix";
            this.colCodePrefix.Name = "colCodePrefix";
            this.colCodePrefix.Visible = true;
            this.colCodePrefix.VisibleIndex = 1;
            this.colCodePrefix.Width = 98;
            // 
            // colIsIncludeCompanyCode
            // 
            this.colIsIncludeCompanyCode.Caption = "IncludeCompanyCode";
            this.colIsIncludeCompanyCode.FieldName = "IsIncludeCompanyCode";
            this.colIsIncludeCompanyCode.Name = "colIsIncludeCompanyCode";
            this.colIsIncludeCompanyCode.Visible = true;
            this.colIsIncludeCompanyCode.VisibleIndex = 2;
            this.colIsIncludeCompanyCode.Width = 64;
            // 
            // colCodeYear
            // 
            this.colCodeYear.Caption = "CodeYear";
            this.colCodeYear.ColumnEdit = this.cmbCodeYear;
            this.colCodeYear.FieldName = "CodeYear";
            this.colCodeYear.Name = "colCodeYear";
            this.colCodeYear.Visible = true;
            this.colCodeYear.VisibleIndex = 3;
            this.colCodeYear.Width = 104;
            // 
            // cmbCodeYear
            // 
            this.cmbCodeYear.AutoHeight = false;
            this.cmbCodeYear.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCodeYear.Name = "cmbCodeYear";
            // 
            // colCodeMonth
            // 
            this.colCodeMonth.Caption = "Month";
            this.colCodeMonth.FieldName = "CodeMonth";
            this.colCodeMonth.Name = "colCodeMonth";
            this.colCodeMonth.Visible = true;
            this.colCodeMonth.VisibleIndex = 4;
            this.colCodeMonth.Width = 64;
            // 
            // colCodeSNLength
            // 
            this.colCodeSNLength.Caption = "SNLength";
            this.colCodeSNLength.ColumnEdit = this.rSpinEdit1;
            this.colCodeSNLength.FieldName = "CodeSNLength";
            this.colCodeSNLength.Name = "colCodeSNLength";
            this.colCodeSNLength.Visible = true;
            this.colCodeSNLength.VisibleIndex = 5;
            this.colCodeSNLength.Width = 80;
            // 
            // rSpinEdit1
            // 
            this.rSpinEdit1.AutoHeight = false;
            this.rSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rSpinEdit1.IsFloatValue = false;
            this.rSpinEdit1.Mask.EditMask = "N00";
            this.rSpinEdit1.Name = "rSpinEdit1";
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Description";
            this.colDescription.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 6;
            this.colDescription.Width = 125;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.MaxLength = 200;
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            this.repositoryItemMemoExEdit1.ShowIcon = false;
            this.repositoryItemMemoExEdit1.Popup += new System.EventHandler(repositoryItemMemoExEdit1_Popup);
            this.repositoryItemMemoExEdit1.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(repositoryItemMemoExEdit1_Closed);
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "CreateBy";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.OptionsColumn.AllowEdit = false;
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 7;
            this.colCreateByName.Width = 94;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "CreateDate";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.OptionsColumn.AllowEdit = false;
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 8;
            this.colCreateDate.Width = 103;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
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
            this.barDisuse});
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDisuse, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = global::ICP.Common.UI.Properties.Resources.Save_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barDisuse
            // 
            this.barDisuse.Caption = "&Disuse";
            this.barDisuse.Glyph = global::ICP.Common.UI.Properties.Resources.Disuse_16;
            this.barDisuse.Id = 1;
            this.barDisuse.Name = "barDisuse";
            this.barDisuse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDisuse_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(851, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 379);
            this.barDockControlBottom.Size = new System.Drawing.Size(851, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 353);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(851, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 353);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.MaxLength = 6;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // SolutionCodeRuleListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "SolutionCodeRuleListPart";
            this.Size = new System.Drawing.Size(851, 379);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbConfigureKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCodeYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }     

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbConfigureKey;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraGrid.Columns.GridColumn colConfigureKeyID;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colIsIncludeCompanyCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCodePrefix;
        private DevExpress.XtraGrid.Columns.GridColumn colCodeYear;
        private DevExpress.XtraGrid.Columns.GridColumn colCodeMonth;
        private DevExpress.XtraGrid.Columns.GridColumn colCodeSNLength;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbCodeYear;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rSpinEdit1;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barDisuse;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
    }
}
