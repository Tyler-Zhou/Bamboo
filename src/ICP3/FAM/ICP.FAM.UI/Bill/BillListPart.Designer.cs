namespace ICP.FAM.UI.Bill
{
    partial class BillListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillListPart));
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillListState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerRefNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillRefNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.linkCustomerName = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWriteOffAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExpressNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.labProfit = new DevExpress.XtraEditors.LabelControl();
            this.txtProfit = new DevExpress.XtraEditors.MemoEdit();
            this.txtCR = new DevExpress.XtraEditors.MemoEdit();
            this.txtDR = new DevExpress.XtraEditors.MemoEdit();
            this.txtTotalByCurrency = new DevExpress.XtraEditors.TextEdit();
            this.labCurrencyProfit = new DevExpress.XtraEditors.LabelControl();
            this.labCR = new DevExpress.XtraEditors.LabelControl();
            this.labDR = new DevExpress.XtraEditors.LabelControl();
            this.cmbCurrency = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.panelTotal = new DevExpress.XtraEditors.PanelControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barSelectAll = new DevExpress.XtraBars.BarButtonItem();
            this.barUnSelect = new DevExpress.XtraBars.BarButtonItem();
            this.barOpenTaskCenter = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.pageControl1 = new ICP.Framework.ClientComponents.Controls.PageControl();
            this.colRBLD = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkCustomerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProfit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalByCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTotal)).BeginInit();
            this.panelTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.linkCustomerName});
            this.gcMain.Size = new System.Drawing.Size(714, 250);
            this.gcMain.TabIndex = 1;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            this.gcMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gcMain_MouseClick);
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.CurrencyBillList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSelected,
            this.colOperationNO,
            this.colBillNO,
            this.colBillListState,
            this.colCustomerRefNo,
            this.colBillRefNO,
            this.colCustomerName,
            this.colCurrencyName,
            this.colAmount,
            this.colWriteOffAmount,
            this.colRBLD,
            this.colCheckBy,
            this.colCheckDate,
            this.colBankDate,
            this.colCreateBy,
            this.colInvoiceNo,
            this.colExpressNo});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.CustomerSorting += new ICP.Framework.ClientComponents.Controls.CustomerSortingHandler(this.gvMain_CustomerSorting);
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            // 
            // colSelected
            // 
            this.colSelected.Caption = "选择";
            this.colSelected.FieldName = "Selected";
            this.colSelected.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colSelected.Name = "colSelected";
            this.colSelected.Visible = true;
            this.colSelected.VisibleIndex = 0;
            this.colSelected.Width = 45;
            // 
            // colOperationNO
            // 
            this.colOperationNO.Caption = "业务号";
            this.colOperationNO.FieldName = "OperationNO";
            this.colOperationNO.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colOperationNO.Name = "colOperationNO";
            this.colOperationNO.OptionsColumn.AllowEdit = false;
            this.colOperationNO.Visible = true;
            this.colOperationNO.VisibleIndex = 1;
            this.colOperationNO.Width = 110;
            // 
            // colBillNO
            // 
            this.colBillNO.Caption = "帐单号";
            this.colBillNO.FieldName = "BillNO";
            this.colBillNO.Name = "colBillNO";
            this.colBillNO.OptionsColumn.AllowEdit = false;
            this.colBillNO.OptionsColumn.AllowMove = false;
            this.colBillNO.Visible = true;
            this.colBillNO.VisibleIndex = 3;
            this.colBillNO.Width = 120;
            // 
            // colBillListState
            // 
            this.colBillListState.Caption = "账单状态";
            this.colBillListState.FieldName = "BillListState";
            this.colBillListState.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colBillListState.Name = "colBillListState";
            this.colBillListState.OptionsColumn.AllowEdit = false;
            this.colBillListState.Visible = true;
            this.colBillListState.VisibleIndex = 2;
            this.colBillListState.Width = 110;
            // 
            // colCustomerRefNo
            // 
            this.colCustomerRefNo.Caption = "客户参考号";
            this.colCustomerRefNo.FieldName = "CustomerRefNo";
            this.colCustomerRefNo.Name = "colCustomerRefNo";
            this.colCustomerRefNo.Visible = true;
            this.colCustomerRefNo.VisibleIndex = 5;
            // 
            // colBillRefNO
            // 
            this.colBillRefNO.Caption = "参考号";
            this.colBillRefNO.FieldName = "BillRefNO";
            this.colBillRefNO.Name = "colBillRefNO";
            this.colBillRefNO.OptionsColumn.AllowEdit = false;
            this.colBillRefNO.Visible = true;
            this.colBillRefNO.VisibleIndex = 4;
            this.colBillRefNO.Width = 100;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "客户";
            this.colCustomerName.ColumnEdit = this.linkCustomerName;
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.OptionsColumn.AllowEdit = false;
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 6;
            this.colCustomerName.Width = 150;
            // 
            // linkCustomerName
            // 
            this.linkCustomerName.AutoHeight = false;
            this.linkCustomerName.Name = "linkCustomerName";
            // 
            // colCurrencyName
            // 
            this.colCurrencyName.Caption = "币种";
            this.colCurrencyName.FieldName = "CurrencyName";
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.OptionsColumn.AllowEdit = false;
            this.colCurrencyName.Visible = true;
            this.colCurrencyName.VisibleIndex = 7;
            // 
            // colAmount
            // 
            this.colAmount.Caption = "金额";
            this.colAmount.DisplayFormat.FormatString = "n";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 8;
            // 
            // colWriteOffAmount
            // 
            this.colWriteOffAmount.Caption = "已销账金额";
            this.colWriteOffAmount.DisplayFormat.FormatString = "n";
            this.colWriteOffAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWriteOffAmount.FieldName = "WriteOffAmount";
            this.colWriteOffAmount.Name = "colWriteOffAmount";
            this.colWriteOffAmount.OptionsColumn.AllowEdit = false;
            this.colWriteOffAmount.Visible = true;
            this.colWriteOffAmount.VisibleIndex = 9;
            // 
            // colCheckBy
            // 
            this.colCheckBy.Caption = "审核人";
            this.colCheckBy.FieldName = "CheckBy";
            this.colCheckBy.Name = "colCheckBy";
            this.colCheckBy.OptionsColumn.AllowEdit = false;
            this.colCheckBy.Visible = true;
            this.colCheckBy.VisibleIndex = 11;
            // 
            // colCheckDate
            // 
            this.colCheckDate.Caption = "审核日期";
            this.colCheckDate.FieldName = "CheckDate";
            this.colCheckDate.Name = "colCheckDate";
            this.colCheckDate.OptionsColumn.AllowEdit = false;
            this.colCheckDate.Visible = true;
            this.colCheckDate.VisibleIndex = 12;
            // 
            // colBankDate
            // 
            this.colBankDate.Caption = "计费日期";
            this.colBankDate.FieldName = "AccountDate";
            this.colBankDate.Name = "colBankDate";
            this.colBankDate.OptionsColumn.AllowEdit = false;
            this.colBankDate.Visible = true;
            this.colBankDate.VisibleIndex = 13;
            // 
            // colCreateBy
            // 
            this.colCreateBy.Caption = "创建人";
            this.colCreateBy.FieldName = "CreateByName";
            this.colCreateBy.Name = "colCreateBy";
            this.colCreateBy.OptionsColumn.AllowEdit = false;
            this.colCreateBy.Visible = true;
            this.colCreateBy.VisibleIndex = 14;
            // 
            // colInvoiceNo
            // 
            this.colInvoiceNo.Caption = "发票号";
            this.colInvoiceNo.FieldName = "InvoiceNo";
            this.colInvoiceNo.Name = "colInvoiceNo";
            this.colInvoiceNo.OptionsColumn.AllowEdit = false;
            this.colInvoiceNo.Visible = true;
            this.colInvoiceNo.VisibleIndex = 15;
            // 
            // colExpressNo
            // 
            this.colExpressNo.Caption = "快递单号";
            this.colExpressNo.FieldName = "ExpressNo";
            this.colExpressNo.Name = "colExpressNo";
            this.colExpressNo.Visible = true;
            this.colExpressNo.VisibleIndex = 16;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // labProfit
            // 
            this.labProfit.Location = new System.Drawing.Point(383, 6);
            this.labProfit.Name = "labProfit";
            this.labProfit.Size = new System.Drawing.Size(24, 14);
            this.labProfit.TabIndex = 76;
            this.labProfit.Text = "利润";
            // 
            // txtProfit
            // 
            this.txtProfit.EditValue = "";
            this.txtProfit.Location = new System.Drawing.Point(419, 4);
            this.txtProfit.Name = "txtProfit";
            this.txtProfit.Size = new System.Drawing.Size(126, 48);
            this.txtProfit.TabIndex = 75;
            // 
            // txtCR
            // 
            this.txtCR.EditValue = "";
            this.txtCR.Location = new System.Drawing.Point(241, 4);
            this.txtCR.Name = "txtCR";
            this.txtCR.Size = new System.Drawing.Size(126, 48);
            this.txtCR.TabIndex = 75;
            // 
            // txtDR
            // 
            this.txtDR.EditValue = "";
            this.txtDR.Location = new System.Drawing.Point(54, 4);
            this.txtDR.Name = "txtDR";
            this.txtDR.Size = new System.Drawing.Size(126, 48);
            this.txtDR.TabIndex = 75;
            // 
            // txtTotalByCurrency
            // 
            this.txtTotalByCurrency.EditValue = "";
            this.txtTotalByCurrency.Location = new System.Drawing.Point(567, 31);
            this.txtTotalByCurrency.Name = "txtTotalByCurrency";
            this.txtTotalByCurrency.Size = new System.Drawing.Size(126, 21);
            this.txtTotalByCurrency.TabIndex = 74;
            // 
            // labCurrencyProfit
            // 
            this.labCurrencyProfit.Location = new System.Drawing.Point(567, 6);
            this.labCurrencyProfit.Name = "labCurrencyProfit";
            this.labCurrencyProfit.Size = new System.Drawing.Size(48, 14);
            this.labCurrencyProfit.TabIndex = 72;
            this.labCurrencyProfit.Text = "折合利润";
            // 
            // labCR
            // 
            this.labCR.Location = new System.Drawing.Point(190, 6);
            this.labCR.Name = "labCR";
            this.labCR.Size = new System.Drawing.Size(48, 14);
            this.labCR.TabIndex = 70;
            this.labCR.Text = "应付合计";
            // 
            // labDR
            // 
            this.labDR.Location = new System.Drawing.Point(4, 6);
            this.labDR.Name = "labDR";
            this.labDR.Size = new System.Drawing.Size(48, 14);
            this.labDR.TabIndex = 70;
            this.labDR.Text = "应收合计";
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.Location = new System.Drawing.Point(628, 3);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Size = new System.Drawing.Size(65, 21);
            this.cmbCurrency.TabIndex = 73;
            this.cmbCurrency.SelectedIndexChanged += new System.EventHandler(this.cmbCurrency_SelectedIndexChanged);
            // 
            // panelTotal
            // 
            this.panelTotal.Controls.Add(this.labProfit);
            this.panelTotal.Controls.Add(this.txtDR);
            this.panelTotal.Controls.Add(this.txtProfit);
            this.panelTotal.Controls.Add(this.cmbCurrency);
            this.panelTotal.Controls.Add(this.txtCR);
            this.panelTotal.Controls.Add(this.labDR);
            this.panelTotal.Controls.Add(this.labCR);
            this.panelTotal.Controls.Add(this.txtTotalByCurrency);
            this.panelTotal.Controls.Add(this.labCurrencyProfit);
            this.panelTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTotal.Location = new System.Drawing.Point(0, 276);
            this.panelTotal.Name = "panelTotal";
            this.panelTotal.Size = new System.Drawing.Size(714, 57);
            this.panelTotal.TabIndex = 5;
            this.panelTotal.Visible = false;
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSelectAll,
            this.barUnSelect,
            this.barOpenTaskCenter});
            this.barManager1.MaxItemId = 2;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(714, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 333);
            this.barDockControlBottom.Size = new System.Drawing.Size(714, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 333);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(714, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 333);
            // 
            // barSelectAll
            // 
            this.barSelectAll.Caption = "Select&All";
            this.barSelectAll.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.barSelectAll.Id = 0;
            this.barSelectAll.Name = "barSelectAll";
            this.barSelectAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSelectAll_ItemClick);
            // 
            // barUnSelect
            // 
            this.barUnSelect.Caption = "&UnSelect";
            this.barUnSelect.Glyph = global::ICP.FAM.UI.Properties.Resources.Return_16;
            this.barUnSelect.Id = 1;
            this.barUnSelect.Name = "barUnSelect";
            this.barUnSelect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barUnSelect_ItemClick);
            // 
            // barOpenTaskCenter
            // 
            this.barOpenTaskCenter.Caption = "&Open Task Center";
            this.barOpenTaskCenter.Glyph = global::ICP.FAM.UI.Properties.Resources.BillDetails;
            this.barOpenTaskCenter.Id = 0;
            this.barOpenTaskCenter.Name = "barOpenTaskCenter";
            this.barOpenTaskCenter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barOpenTaskCenter_ItemClick);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSelectAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.barUnSelect),
            new DevExpress.XtraBars.LinkPersistInfo(this.barOpenTaskCenter)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // pageControl1
            // 
            this.pageControl1.CurrentPage = 0;
            this.pageControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pageControl1.Location = new System.Drawing.Point(0, 250);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.Size = new System.Drawing.Size(714, 26);
            this.pageControl1.TabIndex = 10;
            this.pageControl1.TotalPage = 0;
            this.pageControl1.PageChanged += new ICP.Framework.ClientComponents.Controls.PageChangedHandler(this.pageControl1_PageChanged);
            // 
            // colRBLD
            // 
            this.colRBLD.Caption = "RBLD";
            this.colRBLD.FieldName = "RBLD";
            this.colRBLD.Name = "colRBLD";
            this.colRBLD.Visible = true;
            this.colRBLD.VisibleIndex = 10;
            this.colRBLD.Width = 50;
            // 
            // BillListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.pageControl1);
            this.Controls.Add(this.panelTotal);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BillListPart";
            this.Size = new System.Drawing.Size(714, 333);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkCustomerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProfit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalByCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTotal)).EndInit();
            this.panelTotal.ResumeLayout(false);
            this.panelTotal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        protected System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.Columns.GridColumn colSelected;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationNO;
        private DevExpress.XtraGrid.Columns.GridColumn colBillNO;
        private DevExpress.XtraGrid.Columns.GridColumn colBillRefNO;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colWriteOffAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckBy;
        private DevExpress.XtraGrid.Columns.GridColumn colBankDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateBy;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckDate;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceNo;
        protected DevExpress.XtraEditors.TextEdit txtTotalByCurrency;
        protected DevExpress.XtraEditors.LabelControl labCurrencyProfit;
        protected DevExpress.XtraEditors.LabelControl labDR;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCurrency;
        private DevExpress.XtraEditors.MemoEdit txtDR;
        protected DevExpress.XtraEditors.LabelControl labCR;
        private DevExpress.XtraEditors.MemoEdit txtCR;
        protected DevExpress.XtraEditors.LabelControl labProfit;
        private DevExpress.XtraEditors.MemoEdit txtProfit;
        private DevExpress.XtraEditors.PanelControl panelTotal;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarButtonItem barSelectAll;
        private DevExpress.XtraBars.BarButtonItem barUnSelect;
        private DevExpress.XtraBars.BarButtonItem barOpenTaskCenter;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;      
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkCustomerName;
        private ICP.Framework.ClientComponents.Controls.PageControl pageControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colBillListState;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerRefNo;
        private DevExpress.XtraGrid.Columns.GridColumn colExpressNo;
        private DevExpress.XtraGrid.Columns.GridColumn colRBLD;
    }
}
