namespace ICP.FAM.UI.ChargeConfigure
{
    partial class ChargeConfigListPart
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
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panelContainer1 = new ICP.Framework.ClientComponents.UIManagement.PanelContainer();
            this.cmbCompany = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labShipline = new DevExpress.XtraEditors.LabelControl();
            this.cmbShippingLine = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.CmbCarriers = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labCarrie = new DevExpress.XtraEditors.LabelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.CmbCharges = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labCharge = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChargeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyNames = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCarrierNames = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShippingLineNames = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationNames = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChargeUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrices = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.linkCustomerName = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelContainer1)).BeginInit();
            this.panelContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShippingLine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbCarriers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbCharges.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkCustomerName)).BeginInit();
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
            this.barEdit,
            this.barClose,
            this.barDelete});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 4;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEdit, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelete, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barClose, true)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "Add";
            this.barAdd.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            this.barAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barEdit
            // 
            this.barEdit.Caption = "Edit";
            this.barEdit.Glyph = global::ICP.FAM.UI.Properties.Resources.Edit_16;
            this.barEdit.Id = 1;
            this.barEdit.Name = "barEdit";
            this.barEdit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEdit_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "Delete";
            this.barDelete.Glyph = global::ICP.FAM.UI.Properties.Resources.Delete_16;
            this.barDelete.Id = 3;
            this.barDelete.Name = "barDelete";
            this.barDelete.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "Close";
            this.barClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Close_16;
            this.barClose.Id = 2;
            this.barClose.Name = "barClose";
            this.barClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(939, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 523);
            this.barDockControlBottom.Size = new System.Drawing.Size(939, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 497);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(939, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 497);
            // 
            // panelContainer1
            // 
            this.panelContainer1.Controls.Add(this.cmbCompany);
            this.panelContainer1.Controls.Add(this.labShipline);
            this.panelContainer1.Controls.Add(this.cmbShippingLine);
            this.panelContainer1.Controls.Add(this.CmbCarriers);
            this.panelContainer1.Controls.Add(this.labCarrie);
            this.panelContainer1.Controls.Add(this.btnSearch);
            this.panelContainer1.Controls.Add(this.CmbCharges);
            this.panelContainer1.Controls.Add(this.labCharge);
            this.panelContainer1.Controls.Add(this.labCompany);
            this.panelContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelContainer1.Location = new System.Drawing.Point(0, 26);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.Size = new System.Drawing.Size(939, 33);
            this.panelContainer1.TabIndex = 10;
            // 
            // cmbCompany
            // 
            this.cmbCompany.Location = new System.Drawing.Point(69, 7);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Size = new System.Drawing.Size(148, 21);
            this.cmbCompany.TabIndex = 839;
            // 
            // labShipline
            // 
            this.labShipline.Location = new System.Drawing.Point(628, 9);
            this.labShipline.Name = "labShipline";
            this.labShipline.Size = new System.Drawing.Size(45, 14);
            this.labShipline.TabIndex = 15;
            this.labShipline.Text = "ShipLine";
            // 
            // cmbShippingLine
            // 
            this.cmbShippingLine.Location = new System.Drawing.Point(685, 6);
            this.cmbShippingLine.Name = "cmbShippingLine";
            this.cmbShippingLine.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbShippingLine.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbShippingLine.Size = new System.Drawing.Size(130, 21);
            this.cmbShippingLine.TabIndex = 14;
            // 
            // CmbCarriers
            // 
            this.CmbCarriers.Location = new System.Drawing.Point(462, 6);
            this.CmbCarriers.Name = "CmbCarriers";
            this.CmbCarriers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbCarriers.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.CmbCarriers.Size = new System.Drawing.Size(151, 21);
            this.CmbCarriers.TabIndex = 13;
            // 
            // labCarrie
            // 
            this.labCarrie.Location = new System.Drawing.Point(422, 9);
            this.labCarrie.Name = "labCarrie";
            this.labCarrie.Size = new System.Drawing.Size(34, 14);
            this.labCarrie.TabIndex = 6;
            this.labCarrie.Text = "Carrier";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(862, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(65, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // CmbCharges
            // 
            this.CmbCharges.Location = new System.Drawing.Point(268, 6);
            this.CmbCharges.Name = "CmbCharges";
            this.CmbCharges.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.CmbCharges.Properties.Appearance.Options.UseBackColor = true;
            this.CmbCharges.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.CmbCharges.Size = new System.Drawing.Size(148, 21);
            this.CmbCharges.SpecifiedBackColor = System.Drawing.Color.White;
            this.CmbCharges.TabIndex = 1;
            this.CmbCharges.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.CmbCharges_ButtonClick);
            // 
            // labCharge
            // 
            this.labCharge.Location = new System.Drawing.Point(223, 9);
            this.labCharge.Name = "labCharge";
            this.labCharge.Size = new System.Drawing.Size(38, 14);
            this.labCharge.TabIndex = 0;
            this.labCharge.Text = "Charge";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(13, 9);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 0;
            this.labCompany.Text = "Company";
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bindingSource1;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcMain.Location = new System.Drawing.Point(0, 59);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.linkCustomerName});
            this.gcMain.Size = new System.Drawing.Size(939, 464);
            this.gcMain.TabIndex = 15;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            this.gcMain.Leave += new System.EventHandler(this.gcMain_Leave);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.LocalFeeConfigure);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNo,
            this.colChargeName,
            this.colCompanyNames,
            this.colCarrierNames,
            this.colShippingLineNames,
            this.colLocationNames,
            this.colChargeUnitName,
            this.colCurrencyName,
            this.colPrices,
            this.colCreateByName,
            this.colCreateDate,
            this.colUpdateByName,
            this.colUpdateDate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvMain_FocusedRowChanged);
            this.gvMain.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.gvMain_FocusedColumnChanged);
            this.gvMain.DoubleClick += new System.EventHandler(this.gvMain_DoubleClick);
            // 
            // colNo
            // 
            this.colNo.Caption = "No";
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 0;
            this.colNo.Width = 50;
            // 
            // colChargeName
            // 
            this.colChargeName.Caption = "Charge";
            this.colChargeName.FieldName = "ChargeName";
            this.colChargeName.Name = "colChargeName";
            this.colChargeName.Visible = true;
            this.colChargeName.VisibleIndex = 1;
            this.colChargeName.Width = 100;
            // 
            // colCompanyNames
            // 
            this.colCompanyNames.Caption = "Company";
            this.colCompanyNames.FieldName = "CompanyNames";
            this.colCompanyNames.Name = "colCompanyNames";
            this.colCompanyNames.Visible = true;
            this.colCompanyNames.VisibleIndex = 2;
            this.colCompanyNames.Width = 80;
            // 
            // colCarrierNames
            // 
            this.colCarrierNames.Caption = "Carrier";
            this.colCarrierNames.FieldName = "CarrierNames";
            this.colCarrierNames.Name = "colCarrierNames";
            this.colCarrierNames.Visible = true;
            this.colCarrierNames.VisibleIndex = 3;
            this.colCarrierNames.Width = 100;
            // 
            // colShippingLineNames
            // 
            this.colShippingLineNames.Caption = "ShippingLine";
            this.colShippingLineNames.FieldName = "ShippingLineNames";
            this.colShippingLineNames.Name = "colShippingLineNames";
            this.colShippingLineNames.Visible = true;
            this.colShippingLineNames.VisibleIndex = 4;
            this.colShippingLineNames.Width = 100;
            // 
            // colLocationNames
            // 
            this.colLocationNames.Caption = "Location";
            this.colLocationNames.FieldName = "LocationNames";
            this.colLocationNames.Name = "colLocationNames";
            this.colLocationNames.Visible = true;
            this.colLocationNames.VisibleIndex = 5;
            this.colLocationNames.Width = 100;
            // 
            // colChargeUnitName
            // 
            this.colChargeUnitName.Caption = "Unit";
            this.colChargeUnitName.FieldName = "ChargeUnitName";
            this.colChargeUnitName.Name = "colChargeUnitName";
            this.colChargeUnitName.Visible = true;
            this.colChargeUnitName.VisibleIndex = 6;
            // 
            // colCurrencyName
            // 
            this.colCurrencyName.Caption = "Currency";
            this.colCurrencyName.FieldName = "CurrencyName";
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.Visible = true;
            this.colCurrencyName.VisibleIndex = 7;
            // 
            // colPrices
            // 
            this.colPrices.Caption = "Price";
            this.colPrices.FieldName = "Prices";
            this.colPrices.Name = "colPrices";
            this.colPrices.Visible = true;
            this.colPrices.VisibleIndex = 8;
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "CreateBy";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 9;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "CreateDate";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 10;
            // 
            // colUpdateByName
            // 
            this.colUpdateByName.Caption = "UpdateBy";
            this.colUpdateByName.FieldName = "UpdateByName";
            this.colUpdateByName.Name = "colUpdateByName";
            this.colUpdateByName.Visible = true;
            this.colUpdateByName.VisibleIndex = 11;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.Caption = "UpdateDate";
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.Visible = true;
            this.colUpdateDate.VisibleIndex = 12;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // linkCustomerName
            // 
            this.linkCustomerName.AutoHeight = false;
            this.linkCustomerName.Name = "linkCustomerName";
            // 
            // ChargeConfigListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.panelContainer1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ChargeConfigListPart";
            this.Size = new System.Drawing.Size(939, 523);
            this.Load += new System.EventHandler(this.ChargeConfigListPart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelContainer1)).EndInit();
            this.panelContainer1.ResumeLayout(false);
            this.panelContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShippingLine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbCarriers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbCharges.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkCustomerName)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private Framework.ClientComponents.UIManagement.PanelContainer panelContainer1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit CmbCharges;
        private DevExpress.XtraEditors.LabelControl labCharge;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkCustomerName;
        private DevExpress.XtraEditors.LabelControl labCarrie;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barEdit;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private Framework.ClientComponents.Controls.LWGridView gvMain;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.Columns.GridColumn colChargeName;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyNames;
        private DevExpress.XtraGrid.Columns.GridColumn colCarrierNames;
        private DevExpress.XtraGrid.Columns.GridColumn colShippingLineNames;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationNames;
        private DevExpress.XtraGrid.Columns.GridColumn colChargeUnitName;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colPrices;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;
        private DevExpress.XtraEditors.CheckedComboBoxEdit CmbCarriers;
        private DevExpress.XtraEditors.LabelControl labShipline;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbShippingLine;
        protected DevExpress.XtraEditors.CheckedComboBoxEdit cmbCompany;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraBars.BarButtonItem barDelete;
    }
}
