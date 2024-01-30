namespace ICP.FAM.UI.WriteOff.Dialogs
{
    partial class ConfirmBullions
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.imageComboBoxEdit1 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillRefNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWriteOffAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChecked = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHasInvoice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(55, 55);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "实收金额";
            // 
            // spinEdit1
            // 
            this.spinEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(109, 48);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit1.Size = new System.Drawing.Size(124, 21);
            this.spinEdit1.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(55, 25);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "到账时间";
            // 
            // dateEdit1
            // 
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(109, 22);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit1.Size = new System.Drawing.Size(124, 21);
            this.dateEdit1.TabIndex = 3;
            // 
            // imageComboBoxEdit1
            // 
            this.imageComboBoxEdit1.Location = new System.Drawing.Point(109, 75);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imageComboBoxEdit1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("联通支行RMB", null, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("联通支行USD", null, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("香港汇丰沙田HKD", null, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("香港汇丰沙田USD", null, -1)});
            this.imageComboBoxEdit1.Size = new System.Drawing.Size(221, 21);
            this.imageComboBoxEdit1.TabIndex = 25;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(79, 78);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(24, 14);
            this.labelControl9.TabIndex = 24;
            this.labelControl9.Text = "银行";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(109, 261);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 26;
            this.simpleButton1.Text = "确定";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(202, 261);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 27;
            this.simpleButton2.Text = "取消";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gcMain);
            this.groupControl1.Location = new System.Drawing.Point(34, 111);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(512, 133);
            this.groupControl1.TabIndex = 28;
            this.groupControl1.Text = "账单列表";
            // 
            // gcMain
            // 
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(2, 23);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcMain.Size = new System.Drawing.Size(508, 108);
            this.gcMain.TabIndex = 2;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSelected,
            this.colOperationNO,
            this.colBillNO,
            this.colBillRefNO,
            this.colCustomerName,
            this.colCurrencyName,
            this.colAmount,
            this.colWriteOffAmount,
            this.colCheckBy,
            this.colBankDate,
            this.colCreateBy,
            this.colChecked,
            this.colPaid,
            this.colHasInvoice,
            this.colCheckDate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colSelected
            // 
            this.colSelected.Caption = "选择";
            this.colSelected.FieldName = "Selected";
            this.colSelected.Name = "colSelected";
            this.colSelected.Visible = true;
            this.colSelected.VisibleIndex = 0;
            // 
            // colOperationNO
            // 
            this.colOperationNO.Caption = "业务号";
            this.colOperationNO.FieldName = "OperationNO";
            this.colOperationNO.Name = "colOperationNO";
            this.colOperationNO.OptionsColumn.AllowEdit = false;
            this.colOperationNO.Visible = true;
            this.colOperationNO.VisibleIndex = 1;
            // 
            // colBillNO
            // 
            this.colBillNO.Caption = "帐单号";
            this.colBillNO.FieldName = "BillNO";
            this.colBillNO.Name = "colBillNO";
            this.colBillNO.OptionsColumn.AllowEdit = false;
            this.colBillNO.Visible = true;
            this.colBillNO.VisibleIndex = 2;
            // 
            // colBillRefNO
            // 
            this.colBillRefNO.Caption = "参考号";
            this.colBillRefNO.FieldName = "BillRefNO";
            this.colBillRefNO.Name = "colBillRefNO";
            this.colBillRefNO.OptionsColumn.AllowEdit = false;
            this.colBillRefNO.Visible = true;
            this.colBillRefNO.VisibleIndex = 3;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "客户";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.OptionsColumn.AllowEdit = false;
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 4;
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
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 6;
            // 
            // colWriteOffAmount
            // 
            this.colWriteOffAmount.Caption = "已核销金额";
            this.colWriteOffAmount.FieldName = "WriteOffAmount";
            this.colWriteOffAmount.Name = "colWriteOffAmount";
            this.colWriteOffAmount.OptionsColumn.AllowEdit = false;
            this.colWriteOffAmount.Visible = true;
            this.colWriteOffAmount.VisibleIndex = 7;
            // 
            // colCheckBy
            // 
            this.colCheckBy.Caption = "审核人";
            this.colCheckBy.FieldName = "CheckBy";
            this.colCheckBy.Name = "colCheckBy";
            this.colCheckBy.OptionsColumn.AllowEdit = false;
            this.colCheckBy.Visible = true;
            this.colCheckBy.VisibleIndex = 8;
            // 
            // colBankDate
            // 
            this.colBankDate.Caption = "计费日期";
            this.colBankDate.FieldName = "BankDate";
            this.colBankDate.Name = "colBankDate";
            this.colBankDate.OptionsColumn.AllowEdit = false;
            this.colBankDate.Visible = true;
            this.colBankDate.VisibleIndex = 9;
            // 
            // colCreateBy
            // 
            this.colCreateBy.Caption = "制单人";
            this.colCreateBy.FieldName = "CreateBy";
            this.colCreateBy.Name = "colCreateBy";
            this.colCreateBy.OptionsColumn.AllowEdit = false;
            this.colCreateBy.Visible = true;
            this.colCreateBy.VisibleIndex = 10;
            // 
            // colChecked
            // 
            this.colChecked.Caption = "已审核";
            this.colChecked.FieldName = "Checked";
            this.colChecked.Name = "colChecked";
            this.colChecked.OptionsColumn.AllowEdit = false;
            this.colChecked.Visible = true;
            this.colChecked.VisibleIndex = 11;
            // 
            // colPaid
            // 
            this.colPaid.Caption = "已付";
            this.colPaid.FieldName = "Paid";
            this.colPaid.Name = "colPaid";
            this.colPaid.OptionsColumn.AllowEdit = false;
            this.colPaid.Visible = true;
            this.colPaid.VisibleIndex = 12;
            // 
            // colHasInvoice
            // 
            this.colHasInvoice.Caption = "已出发票";
            this.colHasInvoice.FieldName = "HasInvoice";
            this.colHasInvoice.Name = "colHasInvoice";
            this.colHasInvoice.OptionsColumn.AllowEdit = false;
            this.colHasInvoice.Visible = true;
            this.colHasInvoice.VisibleIndex = 13;
            // 
            // colCheckDate
            // 
            this.colCheckDate.Caption = "审核日期";
            this.colCheckDate.FieldName = "CheckDate";
            this.colCheckDate.Name = "colCheckDate";
            this.colCheckDate.OptionsColumn.AllowEdit = false;
            this.colCheckDate.Visible = true;
            this.colCheckDate.VisibleIndex = 14;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // ConfirmBullions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.imageComboBoxEdit1);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.dateEdit1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.spinEdit1);
            this.Controls.Add(this.labelControl1);
            this.Name = "ConfirmBullions";
            this.Size = new System.Drawing.Size(581, 306);
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraEditors.ImageComboBoxEdit imageComboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
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
        private DevExpress.XtraGrid.Columns.GridColumn colChecked;
        private DevExpress.XtraGrid.Columns.GridColumn colPaid;
        private DevExpress.XtraGrid.Columns.GridColumn colHasInvoice;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
    }
}
