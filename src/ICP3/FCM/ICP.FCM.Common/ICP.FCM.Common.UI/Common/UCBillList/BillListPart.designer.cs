namespace ICP.FCM.Common.UI.UCBillList
{
    partial class UCBillListPart
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
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colBillType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbBillType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colStatue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colBillNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.linkCustomerName = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWriteOffAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBalanceDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDueDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankDates = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckDates = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillRefNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.cmbPaidStatue = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBillType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkCustomerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaidStatue)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.BillList);
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
            this.rcmbState,
            this.linkCustomerName,
            this.cmbPaidStatue,
            this.cmbBillType});
            this.gcMain.Size = new System.Drawing.Size(761, 397);
            this.gcMain.TabIndex = 2;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            this.gcMain.DoubleClick += new System.EventHandler(this.gcMain_DoubleClick);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBillType,
            this.colStatue,
            this.colBillNO,
            this.colCustomerName,
            this.colAmount,
            this.colWriteOffAmount,
            this.colBalanceDescription,
            this.colBankDate,
            this.colDueDate,
            this.colBankDates,
            this.colCheckDates,
            this.colBillRefNO,
            this.colInvoiceNo,
            this.colCheckNo,
            this.colCreateBy,
            this.colCreateDate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsDetail.AllowExpandEmptyDetails = true;
            this.gvMain.OptionsDetail.EnableMasterViewMode = false;
            this.gvMain.OptionsDetail.ShowDetailTabs = false;
            this.gvMain.OptionsDetail.SmartDetailExpand = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colBillType
            // 
            this.colBillType.Caption = "类型";
            this.colBillType.ColumnEdit = this.cmbBillType;
            this.colBillType.FieldName = "Type";
            this.colBillType.Name = "colBillType";
            this.colBillType.Visible = true;
            this.colBillType.VisibleIndex = 0;
            this.colBillType.Width = 52;
            // 
            // cmbBillType
            // 
            this.cmbBillType.AutoHeight = false;
            this.cmbBillType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBillType.Name = "cmbBillType";
            // 
            // colStatue
            // 
            this.colStatue.Caption = "状态";
            this.colStatue.ColumnEdit = this.rcmbState;
            this.colStatue.FieldName = "State";
            this.colStatue.Name = "colStatue";
            this.colStatue.Visible = true;
            this.colStatue.VisibleIndex = 1;
            this.colStatue.Width = 56;
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.Name = "rcmbState";
            // 
            // colBillNO
            // 
            this.colBillNO.Caption = "账单号";
            this.colBillNO.FieldName = "No";
            this.colBillNO.Name = "colBillNO";
            this.colBillNO.Visible = true;
            this.colBillNO.VisibleIndex = 2;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "客户";
            this.colCustomerName.ColumnEdit = this.linkCustomerName;
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.OptionsColumn.AllowEdit = false;
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 3;
            this.colCustomerName.Width = 182;
            // 
            // linkCustomerName
            // 
            this.linkCustomerName.AutoHeight = false;
            this.linkCustomerName.Name = "linkCustomerName";
            // 
            // colAmount
            // 
            this.colAmount.Caption = "金额";
            this.colAmount.DisplayFormat.FormatString = "n";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "AmountDescription";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 4;
            // 
            // colWriteOffAmount
            // 
            this.colWriteOffAmount.Caption = "已销账金额";
            this.colWriteOffAmount.DisplayFormat.FormatString = "n";
            this.colWriteOffAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWriteOffAmount.FieldName = "PayAmountDescription";
            this.colWriteOffAmount.Name = "colWriteOffAmount";
            this.colWriteOffAmount.OptionsColumn.AllowEdit = false;
            this.colWriteOffAmount.Visible = true;
            this.colWriteOffAmount.VisibleIndex = 5;
            // 
            // colBalanceDescription
            // 
            this.colBalanceDescription.Caption = "余额";
            this.colBalanceDescription.FieldName = "BalanceDescription";
            this.colBalanceDescription.Name = "colBalanceDescription";
            this.colBalanceDescription.Visible = true;
            this.colBalanceDescription.VisibleIndex = 6;
            // 
            // colBankDate
            // 
            this.colBankDate.Caption = "计费日期";
            this.colBankDate.FieldName = "AccountDate";
            this.colBankDate.Name = "colBankDate";
            this.colBankDate.OptionsColumn.AllowEdit = false;
            this.colBankDate.Visible = true;
            this.colBankDate.VisibleIndex = 7;
            // 
            // colDueDate
            // 
            this.colDueDate.Caption = "到期日";
            this.colDueDate.FieldName = "DueDate";
            this.colDueDate.Name = "colDueDate";
            this.colDueDate.Visible = true;
            this.colDueDate.VisibleIndex = 8;
            // 
            // colBankDates
            // 
            this.colBankDates.Caption = "到账日期";
            this.colBankDates.FieldName = "BankDates";
            this.colBankDates.Name = "colBankDates";
            this.colBankDates.Visible = true;
            this.colBankDates.VisibleIndex = 9;
            // 
            // colCheckDates
            // 
            this.colCheckDates.Caption = "销帐日期";
            this.colCheckDates.FieldName = "CheckDates";
            this.colCheckDates.Name = "colCheckDates";
            this.colCheckDates.Visible = true;
            this.colCheckDates.VisibleIndex = 10;
            // 
            // colBillRefNO
            // 
            this.colBillRefNO.Caption = "参考号";
            this.colBillRefNO.FieldName = "FormNo";
            this.colBillRefNO.Name = "colBillRefNO";
            this.colBillRefNO.OptionsColumn.AllowEdit = false;
            this.colBillRefNO.Visible = true;
            this.colBillRefNO.VisibleIndex = 11;
            this.colBillRefNO.Width = 100;
            // 
            // colInvoiceNo
            // 
            this.colInvoiceNo.Caption = "发票号";
            this.colInvoiceNo.FieldName = "InvoiceNo";
            this.colInvoiceNo.Name = "colInvoiceNo";
            this.colInvoiceNo.OptionsColumn.AllowEdit = false;
            this.colInvoiceNo.Visible = true;
            this.colInvoiceNo.VisibleIndex = 12;
            // 
            // colCheckNo
            // 
            this.colCheckNo.Caption = "支票号";
            this.colCheckNo.FieldName = "CheckNo";
            this.colCheckNo.Name = "colCheckNo";
            this.colCheckNo.Visible = true;
            this.colCheckNo.VisibleIndex = 13;
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
            // colCreateDate
            // 
            this.colCreateDate.Caption = "创建日期";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 15;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // cmbPaidStatue
            // 
            this.cmbPaidStatue.AutoHeight = false;
            this.cmbPaidStatue.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPaidStatue.Name = "cmbPaidStatue";
            // 
            // UCBillListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Enabled = false;
            this.Name = "UCBillListPart";
            this.Size = new System.Drawing.Size(761, 397);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBillType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkCustomerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaidStatue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.GridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private DevExpress.XtraGrid.Columns.GridColumn colBillRefNO;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colWriteOffAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colBankDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateBy;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbPaidStatue;
        private DevExpress.XtraGrid.Columns.GridColumn colBillType;
        private DevExpress.XtraGrid.Columns.GridColumn colStatue;
        private DevExpress.XtraGrid.Columns.GridColumn colBillNO;
        private DevExpress.XtraGrid.Columns.GridColumn colBalanceDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colDueDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colBankDates;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckDates;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbBillType;


    }
}
