namespace ICP.ReportCenter.UI.FinanceOEReports
{
    partial class UFServicePart
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
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.cmbServerList = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.labServer = new DevExpress.XtraEditors.LabelControl();
            this.labUserName = new DevExpress.XtraEditors.LabelControl();
            this.labPassword = new DevExpress.XtraEditors.LabelControl();
            this.labAccount = new DevExpress.XtraEditors.LabelControl();
            this.chkSavePassword = new DevExpress.XtraEditors.CheckEdit();
            this.cmbAccount = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cmbServerList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSavePassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(138, 162);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(229, 162);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbServerList
            // 
            this.cmbServerList.Location = new System.Drawing.Point(72, 11);
            this.cmbServerList.Name = "cmbServerList";
            this.cmbServerList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbServerList.Size = new System.Drawing.Size(232, 21);
            this.cmbServerList.TabIndex = 0;
            this.cmbServerList.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cmbServerList_ButtonClick);
            // 
            // txtPassword
            // 
            this.txtPassword.EditValue = "";
            this.txtPassword.Location = new System.Drawing.Point(72, 65);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(232, 21);
            this.txtPassword.TabIndex = 2;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(72, 38);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(232, 21);
            this.txtUserName.TabIndex = 1;
            // 
            // labServer
            // 
            this.labServer.Location = new System.Drawing.Point(18, 14);
            this.labServer.Name = "labServer";
            this.labServer.Size = new System.Drawing.Size(36, 14);
            this.labServer.TabIndex = 3;
            this.labServer.Text = "服务器";
            // 
            // labUserName
            // 
            this.labUserName.Location = new System.Drawing.Point(18, 41);
            this.labUserName.Name = "labUserName";
            this.labUserName.Size = new System.Drawing.Size(36, 14);
            this.labUserName.TabIndex = 3;
            this.labUserName.Text = "登录名";
            // 
            // labPassword
            // 
            this.labPassword.Location = new System.Drawing.Point(18, 68);
            this.labPassword.Name = "labPassword";
            this.labPassword.Size = new System.Drawing.Size(24, 14);
            this.labPassword.TabIndex = 3;
            this.labPassword.Text = "密码";
            // 
            // labAccount
            // 
            this.labAccount.Location = new System.Drawing.Point(18, 125);
            this.labAccount.Name = "labAccount";
            this.labAccount.Size = new System.Drawing.Size(24, 14);
            this.labAccount.TabIndex = 3;
            this.labAccount.Text = "帐套";
            // 
            // chkSavePassword
            // 
            this.chkSavePassword.EditValue = true;
            this.chkSavePassword.Location = new System.Drawing.Point(70, 92);
            this.chkSavePassword.Name = "chkSavePassword";
            this.chkSavePassword.Properties.Caption = "保存密码";
            this.chkSavePassword.Size = new System.Drawing.Size(75, 19);
            this.chkSavePassword.TabIndex = 3;
            // 
            // cmbAccount
            // 
            this.cmbAccount.Location = new System.Drawing.Point(72, 122);
            this.cmbAccount.Name = "cmbAccount";
            this.cmbAccount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbAccount.Size = new System.Drawing.Size(232, 21);
            this.cmbAccount.TabIndex = 7;
            this.cmbAccount.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cmbAccount_ButtonClick);
            this.cmbAccount.Enter += new System.EventHandler(this.cmbAccount_Enter);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // UFServicePart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbAccount);
            this.Controls.Add(this.chkSavePassword);
            this.Controls.Add(this.labAccount);
            this.Controls.Add(this.labPassword);
            this.Controls.Add(this.labUserName);
            this.Controls.Add(this.labServer);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.cmbServerList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "UFServicePart";
            this.Size = new System.Drawing.Size(331, 198);
            ((System.ComponentModel.ISupportInitialize)(this.cmbServerList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSavePassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.ComboBoxEdit cmbServerList;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.LabelControl labServer;
        private DevExpress.XtraEditors.LabelControl labUserName;
        private DevExpress.XtraEditors.LabelControl labPassword;
        private DevExpress.XtraEditors.LabelControl labAccount;
        private DevExpress.XtraEditors.CheckEdit chkSavePassword;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbAccount;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}
