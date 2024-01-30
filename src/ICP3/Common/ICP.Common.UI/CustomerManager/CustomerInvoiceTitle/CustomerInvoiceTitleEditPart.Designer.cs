namespace ICP.Common.UI.CustomerManager
{
    partial class CustomerInvoiceTitleEditPart
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
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.labTaxNo = new DevExpress.XtraEditors.LabelControl();
            this.labAddressTel = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtTaxNo = new DevExpress.XtraEditors.MemoEdit();
            this.txtAddressTel = new DevExpress.XtraEditors.MemoEdit();
            this.txtBankAccountNo = new DevExpress.XtraEditors.MemoEdit();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.cmbType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.chkIsValid = new DevExpress.XtraEditors.CheckEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerCode = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaxNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddressTel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBankAccountNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsValid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(32, 35);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(24, 14);
            this.labName.TabIndex = 0;
            this.labName.Text = "名称";
            // 
            // txtName
            // 
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsList, "Name", true));
            this.txtName.Location = new System.Drawing.Point(59, 32);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtName.Properties.Appearance.Options.UseBackColor = true;
            this.txtName.Size = new System.Drawing.Size(309, 21);
            this.txtName.TabIndex = 2;
            // 
            // labTaxNo
            // 
            this.labTaxNo.Location = new System.Drawing.Point(32, 63);
            this.labTaxNo.Name = "labTaxNo";
            this.labTaxNo.Size = new System.Drawing.Size(24, 14);
            this.labTaxNo.TabIndex = 0;
            this.labTaxNo.Text = "税号";
            // 
            // labAddressTel
            // 
            this.labAddressTel.Location = new System.Drawing.Point(8, 105);
            this.labAddressTel.Name = "labAddressTel";
            this.labAddressTel.Size = new System.Drawing.Size(48, 14);
            this.labAddressTel.TabIndex = 0;
            this.labAddressTel.Text = "地址电话";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(8, 159);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "银行帐号";
            // 
            // txtTaxNo
            // 
            this.txtTaxNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsList, "TaxNo", true));
            this.txtTaxNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "TaxNo", true));
            this.txtTaxNo.Location = new System.Drawing.Point(59, 59);
            this.txtTaxNo.Name = "txtTaxNo";
            this.txtTaxNo.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtTaxNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtTaxNo.Size = new System.Drawing.Size(309, 35);
            this.txtTaxNo.TabIndex = 3;
            // 
            // txtAddressTel
            // 
            this.txtAddressTel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "AddressTel", true));
            this.txtAddressTel.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsList, "AddressTel", true));
            this.txtAddressTel.Location = new System.Drawing.Point(59, 100);
            this.txtAddressTel.Name = "txtAddressTel";
            this.txtAddressTel.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtAddressTel.Properties.Appearance.Options.UseBackColor = true;
            this.txtAddressTel.Size = new System.Drawing.Size(309, 48);
            this.txtAddressTel.TabIndex = 4;
            // 
            // txtBankAccountNo
            // 
            this.txtBankAccountNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsList, "BankAccountNo", true));
            this.txtBankAccountNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "BankAccountNo", true));
            this.txtBankAccountNo.Location = new System.Drawing.Point(59, 155);
            this.txtBankAccountNo.Name = "txtBankAccountNo";
            this.txtBankAccountNo.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtBankAccountNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtBankAccountNo.Size = new System.Drawing.Size(309, 66);
            this.txtBankAccountNo.TabIndex = 5;
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(231, 7);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(24, 14);
            this.labType.TabIndex = 0;
            this.labType.Text = "类型";
            // 
            // cmbType
            // 
            this.cmbType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsList, "InvoiceType", true));
            this.cmbType.Location = new System.Drawing.Point(261, 5);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Size = new System.Drawing.Size(107, 21);
            this.cmbType.TabIndex = 1;
            // 
            // chkIsValid
            // 
            this.chkIsValid.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsList, "IsValid", true));
            this.chkIsValid.Location = new System.Drawing.Point(59, 234);
            this.chkIsValid.Name = "chkIsValid";
            this.chkIsValid.Properties.Caption = "是否有效";
            this.chkIsValid.Size = new System.Drawing.Size(85, 19);
            this.chkIsValid.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(195, 232);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(293, 232);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(32, 8);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(24, 14);
            this.labCode.TabIndex = 0;
            this.labCode.Text = "代码";
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "Code", true));
            this.txtCustomerCode.Location = new System.Drawing.Point(60, 5);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(141, 21);
            this.txtCustomerCode.TabIndex = 0;
            //
            //bsList
            //
            this.bsList.DataSource=typeof(ICP.Common.ServiceInterface.DataObjects.CustomerInvoiceTitleInfo);
            // 
            // CustomerInvoiceTitleEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkIsValid);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.txtBankAccountNo);
            this.Controls.Add(this.txtAddressTel);
            this.Controls.Add(this.txtTaxNo);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labAddressTel);
            this.Controls.Add(this.labTaxNo);
            this.Controls.Add(this.txtCustomerCode);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.labType);
            this.Controls.Add(this.labCode);
            this.Controls.Add(this.labName);
            this.Name = "CustomerInvoiceTitleEditPart";
            this.Size = new System.Drawing.Size(391, 267);
            this.Load += new System.EventHandler(this.CustomerInvoiceTitle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaxNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddressTel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBankAccountNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsValid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerCode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labTaxNo;
        private DevExpress.XtraEditors.LabelControl labAddressTel;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.MemoEdit txtTaxNo;
        private DevExpress.XtraEditors.MemoEdit txtAddressTel;
        private DevExpress.XtraEditors.MemoEdit txtBankAccountNo;
        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbType;
        private DevExpress.XtraEditors.CheckEdit chkIsValid;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.TextEdit txtCustomerCode;
        private System.Windows.Forms.BindingSource bsList;
    }
}
