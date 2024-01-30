namespace ICP.FAM.UI.Bill
{
    partial class WriteOffHistoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WriteOffHistoryForm));
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheckNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaidDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaidAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPortBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.txtTotalByCurrency = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalAmount = new DevExpress.XtraEditors.TextEdit();
            this.labCurrency = new DevExpress.XtraEditors.LabelControl();
            this.cmbCurrency = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labTotalCount = new DevExpress.XtraEditors.LabelControl();
            this.labTotal = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalByCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(722, 314);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.WriteOffItemList);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCheckNo,
            this.colPaidDate,
            this.colPaidAmount,
            this.colCurrency,
            this.colPortBy,
            this.colBankDate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colCheckNo
            // 
            this.colCheckNo.Caption = "销账单号";
            this.colCheckNo.FieldName = "No";
            this.colCheckNo.Name = "colCheckNo";
            this.colCheckNo.Visible = true;
            this.colCheckNo.VisibleIndex = 0;
            // 
            // colPaidDate
            // 
            this.colPaidDate.Caption = "销账日期";
            this.colPaidDate.FieldName = "WriteOffDate";
            this.colPaidDate.Name = "colPaidDate";
            this.colPaidDate.Visible = true;
            this.colPaidDate.VisibleIndex = 1;
            // 
            // colPaidAmount
            // 
            this.colPaidAmount.Caption = "销账金额";
            this.colPaidAmount.DisplayFormat.FormatString = "n";
            this.colPaidAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPaidAmount.FieldName = "Amount";
            this.colPaidAmount.Name = "colPaidAmount";
            this.colPaidAmount.Visible = true;
            this.colPaidAmount.VisibleIndex = 2;
            // 
            // colCurrency
            // 
            this.colCurrency.Caption = "币种";
            this.colCurrency.FieldName = "Currency";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 3;
            // 
            // colPortBy
            // 
            this.colPortBy.Caption = "到账人";
            this.colPortBy.FieldName = "BankByName";
            this.colPortBy.Name = "colPortBy";
            this.colPortBy.Visible = true;
            this.colPortBy.VisibleIndex = 4;
            // 
            // colBankDate
            // 
            this.colBankDate.Caption = "到账时间";
            this.colBankDate.FieldName = "ReachedDate";
            this.colBankDate.Name = "colBankDate";
            this.colBankDate.Visible = true;
            this.colBankDate.VisibleIndex = 5;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.txtTotalByCurrency);
            this.pnlBottom.Controls.Add(this.txtTotalAmount);
            this.pnlBottom.Controls.Add(this.labCurrency);
            this.pnlBottom.Controls.Add(this.cmbCurrency);
            this.pnlBottom.Controls.Add(this.labTotalCount);
            this.pnlBottom.Controls.Add(this.labTotal);
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 314);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(722, 32);
            this.pnlBottom.TabIndex = 1;
            // 
            // txtTotalByCurrency
            // 
            this.txtTotalByCurrency.EditValue = "";
            this.txtTotalByCurrency.Location = new System.Drawing.Point(524, 6);
            this.txtTotalByCurrency.Name = "txtTotalByCurrency";
            this.txtTotalByCurrency.Properties.ReadOnly = true;
            this.txtTotalByCurrency.Size = new System.Drawing.Size(104, 21);
            this.txtTotalByCurrency.TabIndex = 75;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.EditValue = "";
            this.txtTotalAmount.Location = new System.Drawing.Point(163, 6);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Properties.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(231, 21);
            this.txtTotalAmount.TabIndex = 73;
            // 
            // labCurrency
            // 
            this.labCurrency.Location = new System.Drawing.Point(414, 9);
            this.labCurrency.Name = "labCurrency";
            this.labCurrency.Size = new System.Drawing.Size(24, 14);
            this.labCurrency.TabIndex = 78;
            this.labCurrency.Text = "币种";
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.Location = new System.Drawing.Point(453, 6);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Size = new System.Drawing.Size(65, 21);
            this.cmbCurrency.TabIndex = 74;
            this.cmbCurrency.SelectedIndexChanged += new System.EventHandler(this.cmbCurrency_SelectedIndexChanged);
            // 
            // labTotalCount
            // 
            this.labTotalCount.Location = new System.Drawing.Point(4, 9);
            this.labTotalCount.Name = "labTotalCount";
            this.labTotalCount.Size = new System.Drawing.Size(63, 14);
            this.labTotalCount.TabIndex = 76;
            this.labTotalCount.Text = "总 0 条数据";
            // 
            // labTotal
            // 
            this.labTotal.Location = new System.Drawing.Point(109, 9);
            this.labTotal.Name = "labTotal";
            this.labTotal.Size = new System.Drawing.Size(48, 14);
            this.labTotal.TabIndex = 77;
            this.labTotal.Text = "合计金额";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(637, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // WriteOffHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.pnlBottom);
            this.Name = "WriteOffHistoryForm";
            this.Size = new System.Drawing.Size(722, 346);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalByCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private System.Windows.Forms.Panel pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckNo;
        private DevExpress.XtraGrid.Columns.GridColumn colPaidDate;
        private DevExpress.XtraGrid.Columns.GridColumn colPaidAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colPortBy;
        private System.Windows.Forms.BindingSource bsList;
        protected DevExpress.XtraEditors.TextEdit txtTotalByCurrency;
        protected DevExpress.XtraEditors.TextEdit txtTotalAmount;
        protected DevExpress.XtraEditors.LabelControl labCurrency;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCurrency;
        protected DevExpress.XtraEditors.LabelControl labTotalCount;
        protected DevExpress.XtraEditors.LabelControl labTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colBankDate;
    }
}
