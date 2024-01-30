namespace ICP.FAM.UI.BatchBill
{
    partial class BatchCustomerBillListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchCustomerBillListPart));
            this.bsBillList = new System.Windows.Forms.BindingSource(this.components);
            this.gcBillList = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvBillList = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbOperationType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmountDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colPayAmountDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBalanceDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDueDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankDates = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckDates = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRefNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.imageListType = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsBillList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBillList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBillList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperationType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // bsBillList
            // 
            this.bsBillList.PositionChanged += new System.EventHandler(this.bsBillList_PositionChanged);
            // 
            // gcBillList
            // 
            this.gcBillList.DataSource = this.bsBillList;
            this.gcBillList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBillList.Location = new System.Drawing.Point(0, 0);
            this.gcBillList.MainView = this.gvBillList;
            this.gcBillList.Name = "gcBillList";
            this.gcBillList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.cmbType,
            this.cmbState,
            this.cmbOperationType});
            this.gcBillList.Size = new System.Drawing.Size(1043, 568);
            this.gcBillList.TabIndex = 0;
            this.gcBillList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBillList});
            this.gcBillList.Click += new System.EventHandler(this.bsBillList_PositionChanged);
            // 
            // gvBillList
            // 
            this.gvBillList.Appearance.Row.Options.UseTextOptions = true;
            this.gvBillList.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvBillList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSelected,
            this.colType,
            this.colState,
            this.colNo,
            this.colOperationNo,
            this.colOperationType,
            this.colCustomerName,
            this.colAmountDescription,
            this.colPayAmountDescription,
            this.colBalanceDescription,
            this.colAccountDate,
            this.colDueDate,
            this.colBankDates,
            this.colCheckDates,
            this.colRefNo,
            this.colInvoiceNo,
            this.colCheckNo,
            this.colCreateByName,
            this.colCreateDate});
            this.gvBillList.GridControl = this.gcBillList;
            this.gvBillList.IndicatorWidth = 35;
            this.gvBillList.Name = "gvBillList";
            this.gvBillList.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvBillList.OptionsBehavior.Editable = false;
            this.gvBillList.OptionsBehavior.ReadOnly = true;
            this.gvBillList.OptionsCustomization.AllowRowSizing = true;
            this.gvBillList.OptionsDetail.EnableMasterViewMode = false;
            this.gvBillList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvBillList.OptionsSelection.MultiSelect = true;
            this.gvBillList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvBillList.OptionsView.ColumnAutoWidth = false;
            this.gvBillList.OptionsView.RowAutoHeight = true;
            this.gvBillList.OptionsView.ShowGroupPanel = false;
            this.gvBillList.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvBillList_RowCellStyle);
            this.gvBillList.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            // 
            // colSelected
            // 
            this.colSelected.Caption = "Selected";
            this.colSelected.FieldName = "Selected";
            this.colSelected.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colSelected.Name = "colSelected";
            this.colSelected.OptionsColumn.AllowMove = false;
            this.colSelected.OptionsColumn.AllowSize = false;
            this.colSelected.Visible = true;
            this.colSelected.VisibleIndex = 0;
            this.colSelected.Width = 68;
            // 
            // colType
            // 
            this.colType.Caption = "Type";
            this.colType.ColumnEdit = this.cmbType;
            this.colType.FieldName = "Type";
            this.colType.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colType.Name = "colType";
            this.colType.OptionsColumn.AllowMove = false;
            this.colType.OptionsColumn.AllowSize = false;
            this.colType.Visible = true;
            this.colType.VisibleIndex = 1;
            this.colType.Width = 45;
            // 
            // cmbType
            // 
            this.cmbType.AutoHeight = false;
            this.cmbType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Name = "cmbType";
            // 
            // colState
            // 
            this.colState.Caption = "State";
            this.colState.ColumnEdit = this.cmbState;
            this.colState.FieldName = "State";
            this.colState.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colState.Name = "colState";
            this.colState.OptionsColumn.AllowMove = false;
            this.colState.Visible = true;
            this.colState.VisibleIndex = 2;
            this.colState.Width = 55;
            // 
            // cmbState
            // 
            this.cmbState.AutoHeight = false;
            this.cmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbState.Name = "cmbState";
            // 
            // colNo
            // 
            this.colNo.Caption = "No";
            this.colNo.FieldName = "No";
            this.colNo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colNo.Name = "colNo";
            this.colNo.OptionsColumn.AllowMove = false;
            this.colNo.OptionsColumn.AllowSize = false;
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 3;
            this.colNo.Width = 126;
            // 
            // colOperationNo
            // 
            this.colOperationNo.Caption = "OperationNo";
            this.colOperationNo.FieldName = "OperationNo";
            this.colOperationNo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colOperationNo.Name = "colOperationNo";
            this.colOperationNo.Visible = true;
            this.colOperationNo.VisibleIndex = 4;
            this.colOperationNo.Width = 126;
            // 
            // colOperationType
            // 
            this.colOperationType.Caption = "OperationType";
            this.colOperationType.ColumnEdit = this.cmbOperationType;
            this.colOperationType.FieldName = "OperationType";
            this.colOperationType.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colOperationType.Name = "colOperationType";
            this.colOperationType.Visible = true;
            this.colOperationType.VisibleIndex = 5;
            this.colOperationType.Width = 80;
            // 
            // cmbOperationType
            // 
            this.cmbOperationType.AutoHeight = false;
            this.cmbOperationType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOperationType.Name = "cmbOperationType";
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "Customer";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 6;
            // 
            // colAmountDescription
            // 
            this.colAmountDescription.Caption = "Amount";
            this.colAmountDescription.ColumnEdit = this.repositoryItemTextEdit1;
            this.colAmountDescription.FieldName = "AmountDescription";
            this.colAmountDescription.Name = "colAmountDescription";
            this.colAmountDescription.Visible = true;
            this.colAmountDescription.VisibleIndex = 7;
            this.colAmountDescription.Width = 83;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // colPayAmountDescription
            // 
            this.colPayAmountDescription.Caption = "PayAmount";
            this.colPayAmountDescription.ColumnEdit = this.repositoryItemTextEdit1;
            this.colPayAmountDescription.FieldName = "PayAmountDescription";
            this.colPayAmountDescription.Name = "colPayAmountDescription";
            this.colPayAmountDescription.Visible = true;
            this.colPayAmountDescription.VisibleIndex = 8;
            this.colPayAmountDescription.Width = 90;
            // 
            // colBalanceDescription
            // 
            this.colBalanceDescription.Caption = "Balance";
            this.colBalanceDescription.ColumnEdit = this.repositoryItemTextEdit1;
            this.colBalanceDescription.FieldName = "BalanceDescription";
            this.colBalanceDescription.Name = "colBalanceDescription";
            this.colBalanceDescription.Visible = true;
            this.colBalanceDescription.VisibleIndex = 9;
            // 
            // colAccountDate
            // 
            this.colAccountDate.Caption = "AccountDate";
            this.colAccountDate.FieldName = "AccountDate";
            this.colAccountDate.Name = "colAccountDate";
            this.colAccountDate.Visible = true;
            this.colAccountDate.VisibleIndex = 10;
            this.colAccountDate.Width = 97;
            // 
            // colDueDate
            // 
            this.colDueDate.Caption = "DueDate";
            this.colDueDate.FieldName = "DueDate";
            this.colDueDate.Name = "colDueDate";
            this.colDueDate.Visible = true;
            this.colDueDate.VisibleIndex = 11;
            // 
            // colBankDates
            // 
            this.colBankDates.Caption = "BankDates";
            this.colBankDates.FieldName = "BankDates";
            this.colBankDates.Name = "colBankDates";
            this.colBankDates.Visible = true;
            this.colBankDates.VisibleIndex = 12;
            // 
            // colCheckDates
            // 
            this.colCheckDates.Caption = "CheckDates";
            this.colCheckDates.FieldName = "CheckDates";
            this.colCheckDates.Name = "colCheckDates";
            this.colCheckDates.Visible = true;
            this.colCheckDates.VisibleIndex = 13;
            // 
            // colRefNo
            // 
            this.colRefNo.Caption = "RefNo";
            this.colRefNo.FieldName = "FormNo";
            this.colRefNo.Name = "colRefNo";
            this.colRefNo.Visible = true;
            this.colRefNo.VisibleIndex = 14;
            this.colRefNo.Width = 137;
            // 
            // colInvoiceNo
            // 
            this.colInvoiceNo.Caption = "InvoiceNo";
            this.colInvoiceNo.FieldName = "InvoiceNo";
            this.colInvoiceNo.Name = "colInvoiceNo";
            this.colInvoiceNo.Visible = true;
            this.colInvoiceNo.VisibleIndex = 15;
            // 
            // colCheckNo
            // 
            this.colCheckNo.Caption = "CheckNo";
            this.colCheckNo.FieldName = "CheckNo";
            this.colCheckNo.Name = "colCheckNo";
            this.colCheckNo.Visible = true;
            this.colCheckNo.VisibleIndex = 16;
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "CreateBy";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 17;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "CreateDate";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 18;
            this.colCreateDate.Width = 100;
            // 
            // imageListType
            // 
            this.imageListType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListType.ImageStream")));
            this.imageListType.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListType.Images.SetKeyName(0, "-.png");
            this.imageListType.Images.SetKeyName(1, "+.png");
            this.imageListType.Images.SetKeyName(2, "-.png");
            this.imageListType.Images.SetKeyName(3, "+-.png");
            // 
            // BatchCustomerBillListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.gcBillList);
            this.Name = "BatchCustomerBillListPart";
            this.Size = new System.Drawing.Size(1043, 568);
            ((System.ComponentModel.ISupportInitialize)(this.bsBillList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBillList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBillList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperationType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ICP.Framework.ClientComponents.Controls.LWGridControl gcBillList;
        private System.Windows.Forms.BindingSource bsBillList;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvBillList;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colRefNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountDate;
        private DevExpress.XtraGrid.Columns.GridColumn colDueDate;
        private DevExpress.XtraGrid.Columns.GridColumn colAmountDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colPayAmountDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colBalanceDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private System.Windows.Forms.ImageList imageListType;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colBankDates;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckDates;
        private DevExpress.XtraGrid.Columns.GridColumn colSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbState;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationNo;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbOperationType;
    }
}
