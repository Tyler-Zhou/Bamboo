namespace ICP.FAM.UI.CustomerBill
{
    partial class AddLocalFee
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
            this.bsBillList = new System.Windows.Forms.BindingSource(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barSelectText = new DevExpress.XtraBars.BarStaticItem();
            this.barCustomer = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barOK = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barText = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barListItem1 = new DevExpress.XtraBars.BarListItem();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colChargeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChargeEname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChargeCname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colqty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFeeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillWay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rcmbCompany = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.rlueCompany = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barFreightCollect = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.bsBillList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlueCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // bsBillList
            // 
            this.bsBillList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.AddLocalFeeList);
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
            this.barClose,
            this.barListItem1,
            this.barText,
            this.barCustomer,
            this.barSelectText,
            this.barOK,
            this.barFreightCollect,
            this.barRefresh});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 9;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1});
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSelectText, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.barCustomer, "", false, true, true, 228),
            new DevExpress.XtraBars.LinkPersistInfo(this.barOK),
            new DevExpress.XtraBars.LinkPersistInfo(this.barFreightCollect),
            new DevExpress.XtraBars.LinkPersistInfo(this.barRefresh, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barClose, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barText, true)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "Save";
            this.barSave.Glyph = global::ICP.FAM.UI.Properties.Resources.Save_Blue_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barSelectText
            // 
            this.barSelectText.Caption = "SelectCustomer";
            this.barSelectText.Glyph = global::ICP.FAM.UI.Properties.Resources.Sarch_16;
            this.barSelectText.Id = 5;
            this.barSelectText.Name = "barSelectText";
            this.barSelectText.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSelectText.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barCustomer
            // 
            this.barCustomer.Caption = "CheckCustomer";
            this.barCustomer.Edit = this.repositoryItemComboBox1;
            this.barCustomer.Id = 4;
            this.barCustomer.Name = "barCustomer";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // barOK
            // 
            this.barOK.Caption = "Ok";
            this.barOK.Glyph = global::ICP.FAM.UI.Properties.Resources.Check_16;
            this.barOK.Id = 6;
            this.barOK.Name = "barOK";
            this.barOK.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barOK.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barOK_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "Close";
            this.barClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Close_16;
            this.barClose.Id = 1;
            this.barClose.Name = "barClose";
            this.barClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barText
            // 
            this.barText.Caption = "The local fee already create a bill(Color Red), modify bills yourself please";
            this.barText.Glyph = global::ICP.FAM.UI.Properties.Resources.Tips;
            this.barText.Id = 3;
            this.barText.Name = "barText";
            this.barText.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barText.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1031, 58);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 568);
            this.barDockControlBottom.Size = new System.Drawing.Size(1031, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 58);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 510);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1031, 58);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 510);
            // 
            // barListItem1
            // 
            this.barListItem1.Caption = "已做过账单的本地费用请自行到账单内更新";
            this.barListItem1.Id = 2;
            this.barListItem1.Name = "barListItem1";
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsBillList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 58);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.rcmbCompany,
            this.rlueCompany,
            this.repositoryItemLookUpEdit1,
            this.repositoryItemComboBox2,
            this.repositoryItemImageComboBox1});
            this.gcMain.Size = new System.Drawing.Size(1031, 510);
            this.gcMain.TabIndex = 7;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colChargeID,
            this.colIsSelected,
            this.colCode,
            this.colChargeEname,
            this.colChargeCname,
            this.colCurrencyCode,
            this.colCurrencyID,
            this.colPrice,
            this.colqty,
            this.colAmount,
            this.colCustomerID,
            this.colCustomerName,
            this.colWay,
            this.colBillID,
            this.colFeeID,
            this.colBillNo,
            this.colBillAmount,
            this.colBillWay});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.GroupCount = 1;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsPrint.EnableAppearanceOddRow = true;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colWay, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            this.gvMain.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvMain_CellValueChanged);
            this.gvMain.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvMain_CustomColumnDisplayText);
            // 
            // colChargeID
            // 
            this.colChargeID.FieldName = "ChargeID";
            this.colChargeID.Name = "colChargeID";
            // 
            // colIsSelected
            // 
            this.colIsSelected.Caption = "Selected";
            this.colIsSelected.FieldName = "IsSelected";
            this.colIsSelected.Name = "colIsSelected";
            this.colIsSelected.Visible = true;
            this.colIsSelected.VisibleIndex = 0;
            this.colIsSelected.Width = 60;
            // 
            // colCode
            // 
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowEdit = false;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 1;
            this.colCode.Width = 50;
            // 
            // colChargeEname
            // 
            this.colChargeEname.Caption = "Charge";
            this.colChargeEname.FieldName = "ChargeEname";
            this.colChargeEname.Name = "colChargeEname";
            this.colChargeEname.OptionsColumn.AllowEdit = false;
            this.colChargeEname.Visible = true;
            this.colChargeEname.VisibleIndex = 2;
            this.colChargeEname.Width = 100;
            // 
            // colChargeCname
            // 
            this.colChargeCname.Caption = "Charge";
            this.colChargeCname.FieldName = "ChargeCname";
            this.colChargeCname.Name = "colChargeCname";
            this.colChargeCname.OptionsColumn.AllowEdit = false;
            this.colChargeCname.Visible = true;
            this.colChargeCname.VisibleIndex = 3;
            this.colChargeCname.Width = 100;
            // 
            // colCurrencyCode
            // 
            this.colCurrencyCode.Caption = "Currency";
            this.colCurrencyCode.FieldName = "CurrencyCode";
            this.colCurrencyCode.Name = "colCurrencyCode";
            this.colCurrencyCode.Width = 60;
            // 
            // colCurrencyID
            // 
            this.colCurrencyID.Caption = "Currency";
            this.colCurrencyID.ColumnEdit = this.repositoryItemImageComboBox1;
            this.colCurrencyID.FieldName = "CurrencyID";
            this.colCurrencyID.Name = "colCurrencyID";
            this.colCurrencyID.Visible = true;
            this.colCurrencyID.VisibleIndex = 4;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // colPrice
            // 
            this.colPrice.Caption = "Price";
            this.colPrice.FieldName = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 5;
            // 
            // colqty
            // 
            this.colqty.Caption = "Quantity";
            this.colqty.FieldName = "qty";
            this.colqty.Name = "colqty";
            this.colqty.Visible = true;
            this.colqty.VisibleIndex = 6;
            // 
            // colAmount
            // 
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 7;
            // 
            // colCustomerID
            // 
            this.colCustomerID.FieldName = "CustomerID";
            this.colCustomerID.Name = "colCustomerID";
            this.colCustomerID.OptionsColumn.AllowEdit = false;
            // 
            // colCustomerName
            // 
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.OptionsColumn.AllowEdit = false;
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 8;
            this.colCustomerName.Width = 200;
            // 
            // colWay
            // 
            this.colWay.FieldName = "Way";
            this.colWay.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.DisplayText;
            this.colWay.Name = "colWay";
            this.colWay.OptionsColumn.AllowEdit = false;
            // 
            // colBillID
            // 
            this.colBillID.FieldName = "BillID";
            this.colBillID.Name = "colBillID";
            this.colBillID.OptionsColumn.AllowEdit = false;
            // 
            // colFeeID
            // 
            this.colFeeID.FieldName = "FeeID";
            this.colFeeID.Name = "colFeeID";
            this.colFeeID.OptionsColumn.AllowEdit = false;
            // 
            // colBillNo
            // 
            this.colBillNo.FieldName = "BillNo";
            this.colBillNo.Name = "colBillNo";
            this.colBillNo.OptionsColumn.AllowEdit = false;
            this.colBillNo.Visible = true;
            this.colBillNo.VisibleIndex = 9;
            this.colBillNo.Width = 100;
            // 
            // colBillAmount
            // 
            this.colBillAmount.FieldName = "BillAmount";
            this.colBillAmount.Name = "colBillAmount";
            this.colBillAmount.OptionsColumn.AllowEdit = false;
            this.colBillAmount.Visible = true;
            this.colBillAmount.VisibleIndex = 11;
            // 
            // colBillWay
            // 
            this.colBillWay.FieldName = "BillWay";
            this.colBillWay.Name = "colBillWay";
            this.colBillWay.OptionsColumn.AllowEdit = false;
            this.colBillWay.Visible = true;
            this.colBillWay.VisibleIndex = 10;
            this.colBillWay.Width = 100;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // rcmbCompany
            // 
            this.rcmbCompany.AutoHeight = false;
            this.rcmbCompany.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbCompany.Name = "rcmbCompany";
            // 
            // rlueCompany
            // 
            this.rlueCompany.AutoHeight = false;
            this.rlueCompany.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rlueCompany.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "公司", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CShortName", 120, "公司")});
            this.rlueCompany.Name = "rlueCompany";
            this.rlueCompany.NullText = "";
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            // 
            // barFreightCollect
            // 
            this.barFreightCollect.Caption = "FreightCollect";
            this.barFreightCollect.Glyph = global::ICP.FAM.UI.Properties.Resources.Transfer_16;
            this.barFreightCollect.Id = 7;
            this.barFreightCollect.Name = "barFreightCollect";
            this.barFreightCollect.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barFreightCollect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barFreightCollect_ItemClick);
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "Refresh";
            this.barRefresh.Glyph = global::ICP.FAM.UI.Properties.Resources.Refresh_16;
            this.barRefresh.Id = 8;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // AddLocalFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "AddLocalFee";
            this.Size = new System.Drawing.Size(1031, 568);
            this.Load += new System.EventHandler(this.AddLocalFee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsBillList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlueCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsBillList;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarStaticItem barText;
        private DevExpress.XtraBars.BarListItem barListItem1;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rlueCompany;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rcmbCompany;
        private DevExpress.XtraGrid.Columns.GridColumn colChargeID;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSelected;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colChargeEname;
        private DevExpress.XtraGrid.Columns.GridColumn colChargeCname;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyID;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colqty;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerID;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colWay;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colBillID;
        private DevExpress.XtraGrid.Columns.GridColumn colFeeID;
        private DevExpress.XtraGrid.Columns.GridColumn colBillNo;
        private DevExpress.XtraGrid.Columns.GridColumn colBillAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colBillWay;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraBars.BarStaticItem barSelectText;
        private DevExpress.XtraBars.BarEditItem barCustomer;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraBars.BarButtonItem barOK;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraBars.BarButtonItem barFreightCollect;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
    }
}
