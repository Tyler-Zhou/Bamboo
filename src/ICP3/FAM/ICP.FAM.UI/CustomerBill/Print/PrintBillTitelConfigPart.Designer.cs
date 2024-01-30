namespace ICP.FAM.UI.CustomerBill.Print
{
    partial class PrintBillTitelConfigPart
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
            this.labTitelCompany = new DevExpress.XtraEditors.LabelControl();
            this.txtTitelCompanyDes = new DevExpress.XtraEditors.MemoEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.cmbCompany = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.cmbTitelCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.lblMessage = new DevExpress.XtraEditors.LabelControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtTitelCompanyDes.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTitelCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // labTitelCompany
            // 
            this.labTitelCompany.Location = new System.Drawing.Point(15, 11);
            this.labTitelCompany.Name = "labTitelCompany";
            this.labTitelCompany.Size = new System.Drawing.Size(24, 14);
            this.labTitelCompany.TabIndex = 6;
            this.labTitelCompany.Text = "Titel";
            // 
            // txtTitelCompanyDes
            // 
            this.txtTitelCompanyDes.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CompanyDsc", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtTitelCompanyDes.EditValue = "";
            this.txtTitelCompanyDes.Location = new System.Drawing.Point(72, 36);
            this.txtTitelCompanyDes.Name = "txtTitelCompanyDes";
            this.txtTitelCompanyDes.Size = new System.Drawing.Size(286, 122);
            this.txtTitelCompanyDes.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 212);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 36);
            this.panel1.TabIndex = 18;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(279, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(173, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cmbCompany
            // 
            this.cmbCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CompanyBank", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbCompany.Location = new System.Drawing.Point(72, 164);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Size = new System.Drawing.Size(286, 21);
            this.cmbCompany.TabIndex = 20;
            this.cmbCompany.Visible = false;
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(15, 167);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 19;
            this.labCompany.Text = "Company";
            this.labCompany.Visible = false;
            // 
            // cmbTitelCompany
            // 
            this.cmbTitelCompany.Location = new System.Drawing.Point(72, 9);
            this.cmbTitelCompany.Name = "cmbTitelCompany";
            this.cmbTitelCompany.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbTitelCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTitelCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTitelCompany.Size = new System.Drawing.Size(286, 21);
            this.cmbTitelCompany.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbTitelCompany.TabIndex = 21;
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(18, 191);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(57, 14);
            this.lblMessage.TabIndex = 17;
            this.lblMessage.Text = "lblMessage";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FAM.UI.CustomerBill.Print.PrintBillTitelConfigData);
            // 
            // PrintBillTitelConfigPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.cmbTitelCompany);
            this.Controls.Add(this.cmbCompany);
            this.Controls.Add(this.labCompany);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labTitelCompany);
            this.Controls.Add(this.txtTitelCompanyDes);
            this.Name = "PrintBillTitelConfigPart";
            this.Size = new System.Drawing.Size(374, 248);
            ((System.ComponentModel.ISupportInitialize)(this.txtTitelCompanyDes.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTitelCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labTitelCompany;
        private DevExpress.XtraEditors.MemoEdit txtTitelCompanyDes;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTitelCompany;
        private DevExpress.XtraEditors.LabelControl lblMessage;
    }
}
