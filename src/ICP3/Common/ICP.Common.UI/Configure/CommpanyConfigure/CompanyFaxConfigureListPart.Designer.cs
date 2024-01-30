namespace ICP.Common.UI.Configure
{
    partial class CompanyFaxConfigureListPart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblEmailAddress = new DevExpress.XtraEditors.LabelControl();
            this.lblEmailPwd = new DevExpress.XtraEditors.LabelControl();
            this.lblEmailHost = new DevExpress.XtraEditors.LabelControl();
            this.lblEmail = new DevExpress.XtraEditors.LabelControl();
            this.txtEmailAddress = new DevExpress.XtraEditors.TextEdit();
            this.txtEmailHost = new DevExpress.XtraEditors.TextEdit();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.txtEmailPwd = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtReEmailPwd = new DevExpress.XtraEditors.TextEdit();
            this.lblReEmailPwd = new DevExpress.XtraEditors.LabelControl();
            this.faxErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblTaxNo = new DevExpress.XtraEditors.LabelControl();
            this.txtTaxNo = new DevExpress.XtraEditors.TextEdit();
            this.btnReset = new DevExpress.XtraEditors.SimpleButton();
            this.bsFax = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailHost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReEmailPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.faxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaxNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFax)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEmailAddress
            // 
            this.lblEmailAddress.Location = new System.Drawing.Point(19, 44);
            this.lblEmailAddress.Name = "lblEmailAddress";
            this.lblEmailAddress.Size = new System.Drawing.Size(52, 14);
            this.lblEmailAddress.TabIndex = 12;
            this.lblEmailAddress.Text = "邮件地址:";
            // 
            // lblEmailPwd
            // 
            this.lblEmailPwd.Location = new System.Drawing.Point(19, 132);
            this.lblEmailPwd.Name = "lblEmailPwd";
            this.lblEmailPwd.Size = new System.Drawing.Size(28, 14);
            this.lblEmailPwd.TabIndex = 9;
            this.lblEmailPwd.Text = "密码:";
            // 
            // lblEmailHost
            // 
            this.lblEmailHost.Location = new System.Drawing.Point(19, 73);
            this.lblEmailHost.Name = "lblEmailHost";
            this.lblEmailHost.Size = new System.Drawing.Size(64, 14);
            this.lblEmailHost.TabIndex = 11;
            this.lblEmailHost.Text = "邮件服务器:";
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(19, 102);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(40, 14);
            this.lblEmail.TabIndex = 10;
            this.lblEmail.Text = "用户名:";
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsFax, "EmailAddress", true));
            this.txtEmailAddress.Location = new System.Drawing.Point(124, 37);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtEmailAddress.Size = new System.Drawing.Size(289, 21);
            this.txtEmailAddress.TabIndex = 1;
            // 
            // txtEmailHost
            // 
            this.txtEmailHost.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsFax, "EmailHost", true));
            this.txtEmailHost.Location = new System.Drawing.Point(123, 66);
            this.txtEmailHost.Name = "txtEmailHost";
            this.txtEmailHost.Size = new System.Drawing.Size(290, 21);
            this.txtEmailHost.TabIndex = 2;
            // 
            // txtEmail
            // 
            this.txtEmail.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsFax, "Email", true));
            this.txtEmail.Location = new System.Drawing.Point(123, 95);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(290, 21);
            this.txtEmail.TabIndex = 3;
            // 
            // txtEmailPwd
            // 
            this.txtEmailPwd.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsFax, "EmailPassWord", true));
            this.txtEmailPwd.EditValue = global::ICP.Common.UI.Resources.Resource_EN.RERH;
            this.txtEmailPwd.Location = new System.Drawing.Point(123, 125);
            this.txtEmailPwd.Name = "txtEmailPwd";
            this.txtEmailPwd.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtEmailPwd.Properties.Appearance.Options.UseBackColor = true;
            this.txtEmailPwd.Properties.PasswordChar = '*';
            this.txtEmailPwd.Size = new System.Drawing.Size(290, 21);
            this.txtEmailPwd.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(326, 202);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存(&S)";
            // 
            // txtReEmailPwd
            // 
            this.txtReEmailPwd.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsFax, "EmailPassWord", true));
            this.txtReEmailPwd.Location = new System.Drawing.Point(123, 155);
            this.txtReEmailPwd.Name = "txtReEmailPwd";
            this.txtReEmailPwd.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtReEmailPwd.Properties.Appearance.Options.UseBackColor = true;
            this.txtReEmailPwd.Properties.PasswordChar = '*';
            this.txtReEmailPwd.Size = new System.Drawing.Size(290, 21);
            this.txtReEmailPwd.TabIndex = 5;
            // 
            // lblReEmailPwd
            // 
            this.lblReEmailPwd.Location = new System.Drawing.Point(19, 162);
            this.lblReEmailPwd.Name = "lblReEmailPwd";
            this.lblReEmailPwd.Size = new System.Drawing.Size(52, 14);
            this.lblReEmailPwd.TabIndex = 8;
            this.lblReEmailPwd.Text = "确认密码:";
            // 
            // faxErrorProvider
            // 
            this.faxErrorProvider.ContainerControl = this;
            // 
            // lblTaxNo
            // 
            this.lblTaxNo.Location = new System.Drawing.Point(19, 15);
            this.lblTaxNo.Name = "lblTaxNo";
            this.lblTaxNo.Size = new System.Drawing.Size(40, 14);
            this.lblTaxNo.TabIndex = 13;
            this.lblTaxNo.Text = "传真号:";
            // 
            // txtTaxNo
            // 
            this.txtTaxNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsFax, "TaxNo", true));
            this.txtTaxNo.Location = new System.Drawing.Point(124, 8);
            this.txtTaxNo.Name = "txtTaxNo";
            this.txtTaxNo.Size = new System.Drawing.Size(289, 21);
            this.txtTaxNo.TabIndex = 0;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(227, 202);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(83, 23);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "重置(&P)";
            //
            //bsFax
            //
            bsFax.DataSource = typeof(ICP.Message.ServiceInterface.ConfigureObjects);

            // 
            // CompanyFaxConfigureListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.txtTaxNo);
            this.Controls.Add(this.lblTaxNo);
            this.Controls.Add(this.lblReEmailPwd);
            this.Controls.Add(this.txtReEmailPwd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtEmailPwd);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtEmailHost);
            this.Controls.Add(this.txtEmailAddress);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblEmailHost);
            this.Controls.Add(this.lblEmailPwd);
            this.Controls.Add(this.lblEmailAddress);
            this.Name = "CompanyFaxConfigureListPart";
            this.Size = new System.Drawing.Size(472, 266);
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailHost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReEmailPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.faxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaxNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblEmailAddress;
        private DevExpress.XtraEditors.LabelControl lblEmailPwd;
        private DevExpress.XtraEditors.LabelControl lblEmailHost;
        private DevExpress.XtraEditors.LabelControl lblEmail;
        private DevExpress.XtraEditors.TextEdit txtEmailAddress;
        private DevExpress.XtraEditors.TextEdit txtEmailHost;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.TextEdit txtEmailPwd;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.BindingSource bsFax;
        private DevExpress.XtraEditors.TextEdit txtReEmailPwd;
        private DevExpress.XtraEditors.LabelControl lblReEmailPwd;
        private System.Windows.Forms.ErrorProvider faxErrorProvider;
        private DevExpress.XtraEditors.LabelControl lblTaxNo;
        private DevExpress.XtraEditors.TextEdit txtTaxNo;
        private DevExpress.XtraEditors.SimpleButton btnReset;
    }
}