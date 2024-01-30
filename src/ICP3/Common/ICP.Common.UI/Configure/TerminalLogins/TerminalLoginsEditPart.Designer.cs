namespace ICP.Common.UI.Configure.TerminalLogins
{
    partial class TerminalLoginsEditPart
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
            this.labPassword = new DevExpress.XtraEditors.LabelControl();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.labUserID = new DevExpress.XtraEditors.LabelControl();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtUserID = new DevExpress.XtraEditors.TextEdit();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // labPassword
            // 
            this.labPassword.Location = new System.Drawing.Point(5, 51);
            this.labPassword.Name = "labPassword";
            this.labPassword.Size = new System.Drawing.Size(51, 14);
            this.labPassword.TabIndex = 15;
            this.labPassword.Text = "Password";
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(5, 5);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(28, 14);
            this.labCode.TabIndex = 13;
            this.labCode.Text = "Code";
            // 
            // labUserID
            // 
            this.labUserID.Location = new System.Drawing.Point(5, 28);
            this.labUserID.Name = "labUserID";
            this.labUserID.Size = new System.Drawing.Size(36, 14);
            this.labUserID.TabIndex = 14;
            this.labUserID.Text = "UserID";
            // 
            // txtPassword
            // 
            this.txtPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Password", true));
            this.txtPassword.Location = new System.Drawing.Point(64, 49);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.MaxLength = 50;
            this.txtPassword.Size = new System.Drawing.Size(213, 21);
            this.txtPassword.TabIndex = 12;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins);
            // 
            // txtCode
            // 
            this.txtCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Code", true));
            this.txtCode.Location = new System.Drawing.Point(64, 3);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaxLength = 10;
            this.txtCode.Size = new System.Drawing.Size(213, 21);
            this.txtCode.TabIndex = 10;
            // 
            // txtUserID
            // 
            this.txtUserID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "UserID", true));
            this.txtUserID.Location = new System.Drawing.Point(64, 26);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Properties.MaxLength = 50;
            this.txtUserID.Size = new System.Drawing.Size(213, 21);
            this.txtUserID.TabIndex = 11;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // TerminalLoginsEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labPassword);
            this.Controls.Add(this.labCode);
            this.Controls.Add(this.labUserID);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtUserID);
            this.Name = "TerminalLoginsEditPart";
            this.Size = new System.Drawing.Size(280, 100);
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labPassword;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.LabelControl labUserID;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtUserID;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}
