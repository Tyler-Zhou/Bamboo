namespace ICP.FAM.UI.WriteOff
{
    partial class AccountListInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountListInfo));
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsCurrencyAmountList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbCurrency = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colBankAccount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbBankAccount = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.numAmount = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colStandardCurrencyAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReached = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ckbReached = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colReachedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalBill = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalFinanceCharges = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStandardCurrencyBillAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStandardCurrencyOtherAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.radCurrencyType = new DevExpress.XtraEditors.RadioGroup();
            this.imgHighlight = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCurrencyAmountList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBankAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbReached)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCurrencyType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsCurrencyAmountList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(85, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbCurrency,
            this.cmbBankAccount,
            this.ckbReached,
            this.numAmount});
            this.gcMain.Size = new System.Drawing.Size(862, 90);
            this.gcMain.TabIndex = 5;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCurrency,
            this.colBankAccount,
            this.colAmount,
            this.colStandardCurrencyAmount,
            this.colReached,
            this.colReachedDate,
            this.colTotalBill,
            this.colTotalFinanceCharges,
            this.colStandardCurrencyBillAmount,
            this.colStandardCurrencyOtherAmount});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsCustomization.AllowFilter = false;
            this.gvMain.OptionsCustomization.AllowGroup = false;
            this.gvMain.OptionsCustomization.AllowSort = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.RowAutoHeight = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            this.gvMain.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvMain_ShowingEditor);
            this.gvMain.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvMain_CellValueChanged);
            this.gvMain.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvMain_CellValueChanging);
            // 
            // colCurrency
            // 
            this.colCurrency.Caption = "币种";
            this.colCurrency.ColumnEdit = this.cmbCurrency;
            this.colCurrency.FieldName = "CurrencyID";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.OptionsColumn.AllowEdit = false;
            this.colCurrency.Width = 47;
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.AutoHeight = false;
            this.cmbCurrency.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Name = "cmbCurrency";
            // 
            // colBankAccount
            // 
            this.colBankAccount.Caption = "银行";
            this.colBankAccount.ColumnEdit = this.cmbBankAccount;
            this.colBankAccount.FieldName = "BankAccountID";
            this.colBankAccount.MaxWidth = 300;
            this.colBankAccount.MinWidth = 260;
            this.colBankAccount.Name = "colBankAccount";
            this.colBankAccount.Visible = true;
            this.colBankAccount.VisibleIndex = 0;
            this.colBankAccount.Width = 300;
            // 
            // cmbBankAccount
            // 
            this.cmbBankAccount.AutoHeight = false;
            this.cmbBankAccount.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBankAccount.Name = "cmbBankAccount";
            // 
            // colAmount
            // 
            this.colAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colAmount.Caption = "金额";
            this.colAmount.ColumnEdit = this.numAmount;
            this.colAmount.DisplayFormat.FormatString = "n";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "TotalAmount";
            this.colAmount.MaxWidth = 100;
            this.colAmount.MinWidth = 100;
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 1;
            this.colAmount.Width = 100;
            // 
            // numAmount
            // 
            this.numAmount.AutoHeight = false;
            this.numAmount.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numAmount.Mask.EditMask = "F2";
            this.numAmount.MaxLength = 20;
            this.numAmount.Name = "numAmount";
            // 
            // colStandardCurrencyAmount
            // 
            this.colStandardCurrencyAmount.Caption = "金额(本位币)";
            this.colStandardCurrencyAmount.ColumnEdit = this.numAmount;
            this.colStandardCurrencyAmount.DisplayFormat.FormatString = "n";
            this.colStandardCurrencyAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colStandardCurrencyAmount.FieldName = "StandardCurrencyAmount";
            this.colStandardCurrencyAmount.MaxWidth = 100;
            this.colStandardCurrencyAmount.MinWidth = 100;
            this.colStandardCurrencyAmount.Name = "colStandardCurrencyAmount";
            this.colStandardCurrencyAmount.OptionsColumn.AllowEdit = false;
            this.colStandardCurrencyAmount.Visible = true;
            this.colStandardCurrencyAmount.VisibleIndex = 2;
            this.colStandardCurrencyAmount.Width = 100;
            // 
            // colReached
            // 
            this.colReached.AppearanceHeader.Options.UseTextOptions = true;
            this.colReached.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colReached.Caption = "已到账";
            this.colReached.ColumnEdit = this.ckbReached;
            this.colReached.FieldName = "IsReached";
            this.colReached.MaxWidth = 49;
            this.colReached.MinWidth = 49;
            this.colReached.Name = "colReached";
            this.colReached.OptionsColumn.AllowEdit = false;
            this.colReached.OptionsFilter.AllowAutoFilter = false;
            this.colReached.Visible = true;
            this.colReached.VisibleIndex = 3;
            this.colReached.Width = 49;
            // 
            // ckbReached
            // 
            this.ckbReached.AutoHeight = false;
            this.ckbReached.Name = "ckbReached";
            // 
            // colReachedDate
            // 
            this.colReachedDate.Caption = "到账日期";
            this.colReachedDate.FieldName = "BankDate";
            this.colReachedDate.MaxWidth = 80;
            this.colReachedDate.MinWidth = 80;
            this.colReachedDate.Name = "colReachedDate";
            this.colReachedDate.Visible = true;
            this.colReachedDate.VisibleIndex = 4;
            this.colReachedDate.Width = 80;
            // 
            // colTotalBill
            // 
            this.colTotalBill.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalBill.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalBill.Caption = "账单汇总";
            this.colTotalBill.DisplayFormat.FormatString = "n";
            this.colTotalBill.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalBill.FieldName = "TotalBillAmount";
            this.colTotalBill.MaxWidth = 120;
            this.colTotalBill.MinWidth = 120;
            this.colTotalBill.Name = "colTotalBill";
            this.colTotalBill.Width = 120;
            // 
            // colTotalFinanceCharges
            // 
            this.colTotalFinanceCharges.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalFinanceCharges.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalFinanceCharges.Caption = "其他项目汇总";
            this.colTotalFinanceCharges.DisplayFormat.FormatString = "n";
            this.colTotalFinanceCharges.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalFinanceCharges.FieldName = "TotalOtherAmount";
            this.colTotalFinanceCharges.MinWidth = 10;
            this.colTotalFinanceCharges.Name = "colTotalFinanceCharges";
            this.colTotalFinanceCharges.Width = 10;
            // 
            // colStandardCurrencyBillAmount
            // 
            this.colStandardCurrencyBillAmount.Caption = "账单汇总(本位币)";
            this.colStandardCurrencyBillAmount.DisplayFormat.FormatString = "n";
            this.colStandardCurrencyBillAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colStandardCurrencyBillAmount.FieldName = "StandardCurrencyBillAmount";
            this.colStandardCurrencyBillAmount.MaxWidth = 120;
            this.colStandardCurrencyBillAmount.MinWidth = 120;
            this.colStandardCurrencyBillAmount.Name = "colStandardCurrencyBillAmount";
            this.colStandardCurrencyBillAmount.OptionsColumn.AllowEdit = false;
            this.colStandardCurrencyBillAmount.Visible = true;
            this.colStandardCurrencyBillAmount.VisibleIndex = 5;
            this.colStandardCurrencyBillAmount.Width = 120;
            // 
            // colStandardCurrencyOtherAmount
            // 
            this.colStandardCurrencyOtherAmount.Caption = "其他项目汇总(本位币)";
            this.colStandardCurrencyOtherAmount.DisplayFormat.FormatString = "n";
            this.colStandardCurrencyOtherAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colStandardCurrencyOtherAmount.FieldName = "StandardCurrencyOtherAmount";
            this.colStandardCurrencyOtherAmount.MaxWidth = 130;
            this.colStandardCurrencyOtherAmount.MinWidth = 130;
            this.colStandardCurrencyOtherAmount.Name = "colStandardCurrencyOtherAmount";
            this.colStandardCurrencyOtherAmount.OptionsColumn.AllowEdit = false;
            this.colStandardCurrencyOtherAmount.Visible = true;
            this.colStandardCurrencyOtherAmount.VisibleIndex = 6;
            this.colStandardCurrencyOtherAmount.Width = 130;
            // 
            // radCurrencyType
            // 
            this.radCurrencyType.Dock = System.Windows.Forms.DockStyle.Left;
            this.radCurrencyType.Location = new System.Drawing.Point(0, 0);
            this.radCurrencyType.Name = "radCurrencyType";
            this.radCurrencyType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Single", "单币种"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Multi", "多币种")});
            this.radCurrencyType.Size = new System.Drawing.Size(85, 90);
            this.radCurrencyType.TabIndex = 0;
            this.radCurrencyType.SelectedIndexChanged += new System.EventHandler(this.radCurrencyType_SelectedIndexChanged);
            // 
            // imgHighlight
            // 
            this.imgHighlight.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgHighlight.ImageStream")));
            this.imgHighlight.TransparentColor = System.Drawing.Color.Transparent;
            this.imgHighlight.Images.SetKeyName(0, "arrow_bold.gif");
            // 
            // AccountListInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.radCurrencyType);
            this.Name = "AccountListInfo";
            this.Size = new System.Drawing.Size(947, 90);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCurrencyAmountList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBankAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbReached)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCurrencyType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colBankAccount;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalBill;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalFinanceCharges;
        private System.Windows.Forms.BindingSource bsCurrencyAmountList;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbCurrency;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbBankAccount;
        private DevExpress.XtraGrid.Columns.GridColumn colReachedDate;
        public DevExpress.XtraEditors.RadioGroup radCurrencyType;
        private DevExpress.XtraGrid.Columns.GridColumn colReached;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ckbReached;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit numAmount;
        private System.Windows.Forms.ImageList imgHighlight;
        private DevExpress.XtraGrid.Columns.GridColumn colStandardCurrencyAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colStandardCurrencyBillAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colStandardCurrencyOtherAmount;
    }
}
