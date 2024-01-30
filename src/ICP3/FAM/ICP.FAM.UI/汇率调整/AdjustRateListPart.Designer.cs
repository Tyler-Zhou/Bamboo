namespace ICP.FAM.UI
{
    partial class AdjustRateListPart
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
            this.barCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barDisuse = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colSourceCurrencyID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbCruuency = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colTargetCurrencyID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFromDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panelContainer1 = new ICP.Framework.ClientComponents.UIManagement.PanelContainer();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.dteToDate = new DevExpress.XtraEditors.DateEdit();
            this.dteFromDate = new DevExpress.XtraEditors.DateEdit();
            this.cmbTargetCurrency = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbSourceCurrencyID = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCruuency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDateEdit1.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelContainer1)).BeginInit();
            this.panelContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteToDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFromDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTargetCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSourceCurrencyID.Properties)).BeginInit();
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
            this.barAdd,
            this.barCopy,
            this.barSave,
            this.barDisuse,
            this.barClose});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 5;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(479, 205);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCopy, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDisuse, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "新增(&N)";
            this.barAdd.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_16;
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barCopy
            // 
            this.barCopy.Caption = "复制(&O)";
            this.barCopy.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.barCopy.Id = 1;
            this.barCopy.Name = "barCopy";
            this.barCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCopy_ItemClick);
            // 
            // barDisuse
            // 
            this.barDisuse.Caption = "作废(&D)";
            this.barDisuse.Glyph = global::ICP.FAM.UI.Properties.Resources.Empty;
            this.barDisuse.Id = 3;
            this.barDisuse.Name = "barDisuse";
            this.barDisuse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barInvalid_ItemClick);
            // 
            // barSave
            // 
            this.barSave.Caption = "保存(&S)";
            this.barSave.Glyph = global::ICP.FAM.UI.Properties.Resources.Save_Blue_16;
            this.barSave.Id = 2;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭(&C)";
            this.barClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Close_16;
            this.barClose.Id = 4;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(788, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 466);
            this.barDockControlBottom.Size = new System.Drawing.Size(788, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 440);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(788, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 440);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 59);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.cmbCruuency,
            this.rSpinEdit1,
            this.rDateEdit1});
            this.gcMain.Size = new System.Drawing.Size(788, 407);
            this.gcMain.TabIndex = 4;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.SolutionExchangeRateList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSourceCurrencyID,
            this.colTargetCurrencyID,
            this.colFromDate,
            this.colRate,
            this.colCreateByName,
            this.colCreateDate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.RowAutoHeight = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colSourceCurrencyID
            // 
            this.colSourceCurrencyID.Caption = "源币种";
            this.colSourceCurrencyID.ColumnEdit = this.cmbCruuency;
            this.colSourceCurrencyID.FieldName = "SourceCurrencyID";
            this.colSourceCurrencyID.Name = "colSourceCurrencyID";
            this.colSourceCurrencyID.Visible = true;
            this.colSourceCurrencyID.VisibleIndex = 0;
            // 
            // cmbCruuency
            // 
            this.cmbCruuency.AutoHeight = false;
            this.cmbCruuency.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCruuency.Name = "cmbCruuency";
            // 
            // colTargetCurrencyID
            // 
            this.colTargetCurrencyID.Caption = "目标币种";
            this.colTargetCurrencyID.ColumnEdit = this.cmbCruuency;
            this.colTargetCurrencyID.FieldName = "TargetCurrencyID";
            this.colTargetCurrencyID.Name = "colTargetCurrencyID";
            this.colTargetCurrencyID.Visible = true;
            this.colTargetCurrencyID.VisibleIndex = 1;
            // 
            // colFromDate
            // 
            this.colFromDate.Caption = "开始日期";
            this.colFromDate.ColumnEdit = this.rDateEdit1;
            this.colFromDate.FieldName = "FromDate";
            this.colFromDate.Name = "colFromDate";
            this.colFromDate.Visible = true;
            this.colFromDate.VisibleIndex = 2;
            // 
            // rDateEdit1
            // 
            this.rDateEdit1.AutoHeight = false;
            this.rDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rDateEdit1.Name = "rDateEdit1";
            this.rDateEdit1.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // colRate
            // 
            this.colRate.Caption = "汇率";
            this.colRate.ColumnEdit = this.rSpinEdit1;
            this.colRate.FieldName = "Rate";
            this.colRate.Name = "colRate";
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 3;
            // 
            // rSpinEdit1
            // 
            this.rSpinEdit1.AutoHeight = false;
            this.rSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rSpinEdit1.Mask.EditMask = "F7";
            this.rSpinEdit1.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.rSpinEdit1.Name = "rSpinEdit1";
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "创建人";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 4;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "创建时间";
            this.colCreateDate.ColumnEdit = this.rDateEdit1;
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 5;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // panelContainer1
            // 
            this.panelContainer1.Controls.Add(this.btnSearch);
            this.panelContainer1.Controls.Add(this.dteToDate);
            this.panelContainer1.Controls.Add(this.dteFromDate);
            this.panelContainer1.Controls.Add(this.cmbTargetCurrency);
            this.panelContainer1.Controls.Add(this.cmbSourceCurrencyID);
            this.panelContainer1.Controls.Add(this.labelControl4);
            this.panelContainer1.Controls.Add(this.labelControl3);
            this.panelContainer1.Controls.Add(this.labelControl2);
            this.panelContainer1.Controls.Add(this.labelControl1);
            this.panelContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelContainer1.Location = new System.Drawing.Point(0, 26);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.Size = new System.Drawing.Size(788, 33);
            this.panelContainer1.TabIndex = 9;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(671, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(65, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dteToDate
            // 
            this.dteToDate.EditValue = null;
            this.dteToDate.Location = new System.Drawing.Point(532, 6);
            this.dteToDate.Name = "dteToDate";
            this.dteToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteToDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteToDate.Size = new System.Drawing.Size(100, 21);
            this.dteToDate.TabIndex = 3;
            // 
            // dteFromDate
            // 
            this.dteFromDate.EditValue = null;
            this.dteFromDate.Location = new System.Drawing.Point(353, 6);
            this.dteFromDate.MenuManager = this.barManager1;
            this.dteFromDate.Name = "dteFromDate";
            this.dteFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFromDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFromDate.Size = new System.Drawing.Size(100, 21);
            this.dteFromDate.TabIndex = 2;
            // 
            // cmbTargetCurrency
            // 
            this.cmbTargetCurrency.Location = new System.Drawing.Point(205, 6);
            this.cmbTargetCurrency.Name = "cmbTargetCurrency";
            this.cmbTargetCurrency.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbTargetCurrency.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTargetCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTargetCurrency.Size = new System.Drawing.Size(73, 21);
            this.cmbTargetCurrency.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbTargetCurrency.TabIndex = 1;
            // 
            // cmbSourceCurrencyID
            // 
            this.cmbSourceCurrencyID.Location = new System.Drawing.Point(55, 6);
            this.cmbSourceCurrencyID.MenuManager = this.barManager1;
            this.cmbSourceCurrencyID.Name = "cmbSourceCurrencyID";
            this.cmbSourceCurrencyID.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbSourceCurrencyID.Properties.Appearance.Options.UseBackColor = true;
            this.cmbSourceCurrencyID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSourceCurrencyID.Size = new System.Drawing.Size(81, 21);
            this.cmbSourceCurrencyID.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbSourceCurrencyID.TabIndex = 0;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(476, 9);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "结束日期";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(295, 9);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "开始日期";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(151, 9);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "目标币种";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "源币种";
            // 
            // AdjustRateListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.panelContainer1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "AdjustRateListPart";
            this.Size = new System.Drawing.Size(788, 466);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCruuency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDateEdit1.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelContainer1)).EndInit();
            this.panelContainer1.ResumeLayout(false);
            this.panelContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteToDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFromDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTargetCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSourceCurrencyID.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barCopy;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barDisuse;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.Columns.GridColumn colSourceCurrencyID;
        private DevExpress.XtraGrid.Columns.GridColumn colTargetCurrencyID;
        private DevExpress.XtraGrid.Columns.GridColumn colFromDate;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbCruuency;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rSpinEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rDateEdit1;
        private ICP.Framework.ClientComponents.UIManagement.PanelContainer panelContainer1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dteToDate;
        private DevExpress.XtraEditors.DateEdit dteFromDate;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTargetCurrency;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbSourceCurrencyID;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
    }
}
