namespace LongWin.DataWarehouseReport.WinUI
{
    partial class UFServerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbServerList = new System.Windows.Forms.ComboBox();
            this.tbFindServer = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbpswd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbAccount = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btAccount = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器";
            // 
            // cmbServerList
            // 
            this.cmbServerList.BackColor = System.Drawing.Color.White;
            this.cmbServerList.FormattingEnabled = true;
            this.cmbServerList.Location = new System.Drawing.Point(51, 17);
            this.cmbServerList.Name = "cmbServerList";
            this.cmbServerList.Size = new System.Drawing.Size(170, 20);
            this.cmbServerList.TabIndex = 1;
            this.cmbServerList.SelectedIndexChanged += new System.EventHandler(this.cmbServerList_SelectedIndexChanged);
            // 
            // tbFindServer
            // 
            this.tbFindServer.Location = new System.Drawing.Point(227, 17);
            this.tbFindServer.Name = "tbFindServer";
            this.tbFindServer.Size = new System.Drawing.Size(26, 20);
            this.tbFindServer.TabIndex = 2;
            this.tbFindServer.Text = "...";
            this.tbFindServer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tbFindServer.UseVisualStyleBackColor = true;
            this.tbFindServer.Click += new System.EventHandler(this.tbFindServer_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "登录名";
            // 
            // tbUser
            // 
            this.tbUser.BackColor = System.Drawing.Color.White;
            this.tbUser.Location = new System.Drawing.Point(51, 41);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(170, 21);
            this.tbUser.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "密码";
            // 
            // tbpswd
            // 
            this.tbpswd.BackColor = System.Drawing.Color.White;
            this.tbpswd.Location = new System.Drawing.Point(51, 66);
            this.tbpswd.Name = "tbpswd";
            this.tbpswd.PasswordChar = '*';
            this.tbpswd.Size = new System.Drawing.Size(170, 21);
            this.tbpswd.TabIndex = 3;
            this.tbpswd.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "帐套";
            // 
            // cmbAccount
            // 
            this.cmbAccount.BackColor = System.Drawing.Color.White;
            this.cmbAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccount.FormattingEnabled = true;
            this.cmbAccount.Location = new System.Drawing.Point(51, 113);
            this.cmbAccount.Name = "cmbAccount";
            this.cmbAccount.Size = new System.Drawing.Size(170, 20);
            this.cmbAccount.TabIndex = 1;
            this.cmbAccount.DropDown += new System.EventHandler(this.cmbAccount_DropDown);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(51, 93);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "保存密码";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(39, 146);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(61, 20);
            this.btOK.TabIndex = 2;
            this.btOK.Text = "确定(&Y)";
            this.btOK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(180, 146);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(61, 20);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "取消(&C)";
            this.btCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btAccount
            // 
            this.btAccount.Location = new System.Drawing.Point(227, 112);
            this.btAccount.Name = "btAccount";
            this.btAccount.Size = new System.Drawing.Size(26, 20);
            this.btAccount.TabIndex = 2;
            this.btAccount.Text = "...";
            this.btAccount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btAccount.UseVisualStyleBackColor = true;
            this.btAccount.Click += new System.EventHandler(this.btAccount_Click);
            // 
            // UFServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(278, 178);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.tbpswd);
            this.Controls.Add(this.tbUser);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btAccount);
            this.Controls.Add(this.tbFindServer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbAccount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbServerList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "UFServerForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UFServerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbServerList;
        private System.Windows.Forms.Button tbFindServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbpswd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbAccount;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btAccount;
    }
}