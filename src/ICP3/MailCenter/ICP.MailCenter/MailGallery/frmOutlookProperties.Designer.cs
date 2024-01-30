namespace ICP.MailCenter.UI
{
    partial class frmOutlookProperties
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
            this.picProperties = new System.Windows.Forms.PictureBox();
            this.lblNick = new System.Windows.Forms.Label();
            this.txtNickName = new System.Windows.Forms.TextBox();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.lblMail = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.btnAddContact = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtType = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // picProperties
            // 
            this.picProperties.Image = global::ICP.MailCenter.UI.Properties.Resources.olProperties;
            this.picProperties.Location = new System.Drawing.Point(13, 13);
            this.picProperties.Name = "picProperties";
            this.picProperties.Size = new System.Drawing.Size(51, 55);
            this.picProperties.TabIndex = 0;
            this.picProperties.TabStop = false;
            // 
            // lblNick
            // 
            this.lblNick.AutoSize = true;
            this.lblNick.Location = new System.Drawing.Point(79, 18);
            this.lblNick.Name = "lblNick";
            this.lblNick.Size = new System.Drawing.Size(77, 12);
            this.lblNick.TabIndex = 1;
            this.lblNick.Text = "显示名称(&D):";
            // 
            // txtNickName
            // 
            this.txtNickName.Location = new System.Drawing.Point(188, 13);
            this.txtNickName.Name = "txtNickName";
            this.txtNickName.Size = new System.Drawing.Size(260, 21);
            this.txtNickName.TabIndex = 2;
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(188, 45);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(260, 21);
            this.txtMail.TabIndex = 3;
            // 
            // lblMail
            // 
            this.lblMail.AutoSize = true;
            this.lblMail.Location = new System.Drawing.Point(79, 49);
            this.lblMail.Name = "lblMail";
            this.lblMail.Size = new System.Drawing.Size(101, 12);
            this.lblMail.TabIndex = 5;
            this.lblMail.Text = "电子邮件地址(&M):";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(79, 82);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(101, 12);
            this.lblType.TabIndex = 6;
            this.lblType.Text = "电子邮件类型(&Y):";
            // 
            // btnAddContact
            // 
            this.btnAddContact.Location = new System.Drawing.Point(236, 135);
            this.btnAddContact.Name = "btnAddContact";
            this.btnAddContact.Size = new System.Drawing.Size(120, 23);
            this.btnAddContact.TabIndex = 8;
            this.btnAddContact.Text = "添加到通讯录(&A)";
            this.btnAddContact.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(365, 135);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(79, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // txtType
            // 
            this.txtType.Enabled = false;
            this.txtType.Location = new System.Drawing.Point(188, 79);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(256, 21);
            this.txtType.TabIndex = 10;
            // 
            // frmOutlookProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 171);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddContact);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblMail);
            this.Controls.Add(this.txtMail);
            this.Controls.Add(this.txtNickName);
            this.Controls.Add(this.lblNick);
            this.Controls.Add(this.picProperties);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOutlookProperties";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "电子邮件属性";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOutlookProperties_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picProperties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picProperties;
        private System.Windows.Forms.Label lblNick;
        private System.Windows.Forms.TextBox txtNickName;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.Label lblMail;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Button btnAddContact;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtType;
    }
}