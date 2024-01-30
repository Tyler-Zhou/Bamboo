namespace ICP.FAM.UI.Bill
{
    partial class BillOperationListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillOperationListPart));
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillRefNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerRefNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWriteOffAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.rcmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labCurrency = new DevExpress.XtraEditors.LabelControl();
            this.cmbCurrency = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.txtAmount = new DevExpress.XtraEditors.TextEdit();
            this.labARAmount = new DevExpress.XtraEditors.LabelControl();
            this.txtTotalByCurrency = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalByCurrency.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.CurrencyBillList);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.rcmbState});
            this.gcMain.Size = new System.Drawing.Size(1011, 434);
            this.gcMain.TabIndex = 2;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            this.gcMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcMain_KeyDown);
           
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colState,
            this.colOperationNO,
            this.colBillNO,
            this.colBillRefNO,
            this.colCustomerRefNo,
            this.colCustomerName,
            this.colCurrencyName,
            this.colAmount,
            this.colWriteOffAmount,
            this.colCheckBy,
            this.colCheckDate,
            this.colBankDate,
            this.colCreateBy,
            this.colInvoiceNo});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            // 
            // colState
            // 
            this.colState.Caption = "状态";
            this.colState.FieldName = "BillListState";
            this.colState.Name = "colState";
            this.colState.Visible = true;
            this.colState.VisibleIndex = 0;
            // 
            // colOperationNO
            // 
            this.colOperationNO.Caption = "业务号";
            this.colOperationNO.FieldName = "OperationNO";
            this.colOperationNO.Name = "colOperationNO";
            this.colOperationNO.Visible = true;
            this.colOperationNO.VisibleIndex = 1;
            // 
            // colBillNO
            // 
            this.colBillNO.Caption = "帐单号";
            this.colBillNO.FieldName = "BillNO";
            this.colBillNO.Name = "colBillNO";
            this.colBillNO.Visible = true;
            this.colBillNO.VisibleIndex = 2;
            // 
            // colBillRefNO
            // 
            this.colBillRefNO.Caption = "参考号";
            this.colBillRefNO.FieldName = "BillRefNO";
            this.colBillRefNO.Name = "colBillRefNO";
            this.colBillRefNO.Visible = true;
            this.colBillRefNO.VisibleIndex = 3;
            // 
            // colCustomerRefNo
            // 
            this.colCustomerRefNo.Caption = "客户参考号";
            this.colCustomerRefNo.FieldName = "CustomerRefNo";
            this.colCustomerRefNo.Name = "colCustomerRefNo";
            this.colCustomerRefNo.Visible = true;
            this.colCustomerRefNo.VisibleIndex = 4;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "客户";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 5;
            // 
            // colCurrencyName
            // 
            this.colCurrencyName.Caption = "币种";
            this.colCurrencyName.FieldName = "CurrencyName";
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.Visible = true;
            this.colCurrencyName.VisibleIndex = 6;
            // 
            // colAmount
            // 
            this.colAmount.Caption = "金额";
            this.colAmount.DisplayFormat.FormatString = "n";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 7;
            // 
            // colWriteOffAmount
            // 
            this.colWriteOffAmount.Caption = "已销账金额";
            this.colWriteOffAmount.DisplayFormat.FormatString = "n";
            this.colWriteOffAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWriteOffAmount.FieldName = "WriteOffAmount";
            this.colWriteOffAmount.Name = "colWriteOffAmount";
            this.colWriteOffAmount.Visible = true;
            this.colWriteOffAmount.VisibleIndex = 8;
            // 
            // colCheckBy
            // 
            this.colCheckBy.Caption = "审核人";
            this.colCheckBy.FieldName = "CheckBy";
            this.colCheckBy.Name = "colCheckBy";
            this.colCheckBy.Visible = true;
            this.colCheckBy.VisibleIndex = 9;
            // 
            // colCheckDate
            // 
            this.colCheckDate.Caption = "审核日期";
            this.colCheckDate.FieldName = "CheckDate";
            this.colCheckDate.Name = "colCheckDate";
            this.colCheckDate.Visible = true;
            this.colCheckDate.VisibleIndex = 10;
            // 
            // colBankDate
            // 
            this.colBankDate.Caption = "计费日期";
            this.colBankDate.FieldName = "AccountDate";
            this.colBankDate.Name = "colBankDate";
            this.colBankDate.Visible = true;
            this.colBankDate.VisibleIndex = 11;
            // 
            // colCreateBy
            // 
            this.colCreateBy.Caption = "创建人";
            this.colCreateBy.FieldName = "CreateByName";
            this.colCreateBy.Name = "colCreateBy";
            this.colCreateBy.Visible = true;
            this.colCreateBy.VisibleIndex = 12;
            // 
            // colInvoiceNo
            // 
            this.colInvoiceNo.Caption = "发票号";
            this.colInvoiceNo.FieldName = "InvoiceNo";
            this.colInvoiceNo.Name = "colInvoiceNo";
            this.colInvoiceNo.Visible = true;
            this.colInvoiceNo.VisibleIndex = 13;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.Name = "rcmbState";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labCurrency);
            this.panelControl1.Controls.Add(this.cmbCurrency);
            this.panelControl1.Controls.Add(this.txtAmount);
            this.panelControl1.Controls.Add(this.labARAmount);
            this.panelControl1.Controls.Add(this.txtTotalByCurrency);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 434);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1011, 31);
            this.panelControl1.TabIndex = 10;
            // 
            // labCurrency
            // 
            this.labCurrency.Location = new System.Drawing.Point(533, 8);
            this.labCurrency.Name = "labCurrency";
            this.labCurrency.Size = new System.Drawing.Size(24, 14);
            this.labCurrency.TabIndex = 88;
            this.labCurrency.Text = "币种";
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.Location = new System.Drawing.Point(564, 5);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Size = new System.Drawing.Size(65, 21);
            this.cmbCurrency.TabIndex = 89;
            this.cmbCurrency.SelectedIndexChanged += new System.EventHandler(this.cmbCurrency_SelectedIndexChanged);
            // 
            // txtAmount
            // 
            this.txtAmount.EditValue = "";
            this.txtAmount.Location = new System.Drawing.Point(35, 5);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtAmount.Properties.Appearance.Options.UseForeColor = true;
            this.txtAmount.Properties.DisplayFormat.FormatString = "N";
            this.txtAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAmount.Properties.EditFormat.FormatString = "N";
            this.txtAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAmount.Properties.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(491, 21);
            this.txtAmount.TabIndex = 87;
            // 
            // labARAmount
            // 
            this.labARAmount.Location = new System.Drawing.Point(5, 8);
            this.labARAmount.Name = "labARAmount";
            this.labARAmount.Size = new System.Drawing.Size(24, 14);
            this.labARAmount.TabIndex = 86;
            this.labARAmount.Text = "合计";
            // 
            // txtTotalByCurrency
            // 
            this.txtTotalByCurrency.EditValue = "0";
            this.txtTotalByCurrency.Location = new System.Drawing.Point(632, 5);
            this.txtTotalByCurrency.Name = "txtTotalByCurrency";
            this.txtTotalByCurrency.Properties.ReadOnly = true;
            this.txtTotalByCurrency.Size = new System.Drawing.Size(131, 21);
            this.txtTotalByCurrency.TabIndex = 85;
            // 
            // BillOperationListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.panelControl1);
            this.Name = "BillOperationListPart";
            this.Size = new System.Drawing.Size(1011, 465);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalByCurrency.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
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
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        protected DevExpress.XtraEditors.LabelControl labCurrency;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCurrency;
        private DevExpress.XtraEditors.TextEdit txtAmount;
        private DevExpress.XtraEditors.LabelControl labARAmount;
        protected DevExpress.XtraEditors.TextEdit txtTotalByCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerRefNo;
    }
}
