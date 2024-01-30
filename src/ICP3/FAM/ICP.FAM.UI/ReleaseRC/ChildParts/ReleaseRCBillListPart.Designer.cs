namespace ICP.FAM.UI.ReleaseRC
{
    partial class ReleaseRCBillListPart
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
            this.colStatue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colOperationNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillRefNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.linkCustomerName = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWriteOffAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaidStatue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.cmbPaidStatue = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkCustomerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaidStatue)).BeginInit();
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
            this.cmbPaidStatue});
            this.gcMain.Size = new System.Drawing.Size(761, 397);
            this.gcMain.TabIndex = 2;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStatue,
            this.colOperationNO,
            this.colBillNO,
            this.colBillRefNO,
            this.colCustomerName,
            this.colCurrencyName,
            this.colAmount,
            this.colWriteOffAmount,
            this.colPaid,
            this.colPaidStatue,
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
            // 
            // colStatue
            // 
            this.colStatue.Caption = "状态";
            this.colStatue.ColumnEdit = this.rcmbState;
            this.colStatue.FieldName = "State";
            this.colStatue.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colStatue.Name = "colStatue";
            this.colStatue.OptionsColumn.AllowEdit = false;
            this.colStatue.Visible = true;
            this.colStatue.VisibleIndex = 0;
            this.colStatue.Width = 70;
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.Name = "rcmbState";
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
            this.colBillNO.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colBillNO.Name = "colBillNO";
            this.colBillNO.OptionsColumn.AllowEdit = false;
            this.colBillNO.OptionsColumn.AllowMove = false;
            this.colBillNO.Visible = true;
            this.colBillNO.VisibleIndex = 2;
            this.colBillNO.Width = 120;
            // 
            // colBillRefNO
            // 
            this.colBillRefNO.Caption = "参考号";
            this.colBillRefNO.FieldName = "BillRefNO";
            this.colBillRefNO.Name = "colBillRefNO";
            this.colBillRefNO.OptionsColumn.AllowEdit = false;
            this.colBillRefNO.Visible = true;
            this.colBillRefNO.VisibleIndex = 3;
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
            this.colCustomerName.VisibleIndex = 4;
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
            this.colCurrencyName.VisibleIndex = 5;
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
            this.colAmount.VisibleIndex = 6;
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
            this.colWriteOffAmount.VisibleIndex = 7;
            // 
            // colPaid
            // 
            this.colPaid.Caption = "已到帐";
            this.colPaid.FieldName = "IsPaid";
            this.colPaid.Name = "colPaid";
            this.colPaid.OptionsColumn.AllowEdit = false;
            this.colPaid.Visible = true;
            this.colPaid.VisibleIndex = 8;
            // 
            // colPaidStatue
            // 
            this.colPaidStatue.Caption = "到账状态";
            this.colPaidStatue.ColumnEdit = this.cmbPaidStatue;
            this.colPaidStatue.FieldName = "Paid";
            this.colPaidStatue.Name = "colPaidStatue";
            this.colPaidStatue.Visible = true;
            this.colPaidStatue.VisibleIndex = 9;
            // 
            // colCheckBy
            // 
            this.colCheckBy.Caption = "审核人";
            this.colCheckBy.FieldName = "CheckBy";
            this.colCheckBy.Name = "colCheckBy";
            this.colCheckBy.OptionsColumn.AllowEdit = false;
            this.colCheckBy.Visible = true;
            this.colCheckBy.VisibleIndex = 10;
            // 
            // colCheckDate
            // 
            this.colCheckDate.Caption = "审核日期";
            this.colCheckDate.FieldName = "CheckDate";
            this.colCheckDate.Name = "colCheckDate";
            this.colCheckDate.OptionsColumn.AllowEdit = false;
            this.colCheckDate.Visible = true;
            this.colCheckDate.VisibleIndex = 11;
            // 
            // colBankDate
            // 
            this.colBankDate.Caption = "计费日期";
            this.colBankDate.FieldName = "AccountDate";
            this.colBankDate.Name = "colBankDate";
            this.colBankDate.OptionsColumn.AllowEdit = false;
            this.colBankDate.Visible = true;
            this.colBankDate.VisibleIndex = 12;
            // 
            // colCreateBy
            // 
            this.colCreateBy.Caption = "创建人";
            this.colCreateBy.FieldName = "CreateByName";
            this.colCreateBy.Name = "colCreateBy";
            this.colCreateBy.OptionsColumn.AllowEdit = false;
            this.colCreateBy.Visible = true;
            this.colCreateBy.VisibleIndex = 13;
            // 
            // colInvoiceNo
            // 
            this.colInvoiceNo.Caption = "发票号";
            this.colInvoiceNo.FieldName = "InvoiceNo";
            this.colInvoiceNo.Name = "colInvoiceNo";
            this.colInvoiceNo.OptionsColumn.AllowEdit = false;
            this.colInvoiceNo.Visible = true;
            this.colInvoiceNo.VisibleIndex = 14;
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
            // ReleaseBLBillListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Enabled = false;
            this.Name = "ReleaseBLBillListPart";
            this.Size = new System.Drawing.Size(761, 397);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn colStatue;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationNO;
        private DevExpress.XtraGrid.Columns.GridColumn colBillNO;
        private DevExpress.XtraGrid.Columns.GridColumn colBillRefNO;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colWriteOffAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colPaid;
        private DevExpress.XtraGrid.Columns.GridColumn colPaidStatue;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckBy;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckDate;
        private DevExpress.XtraGrid.Columns.GridColumn colBankDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateBy;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbPaidStatue;


    }
}
