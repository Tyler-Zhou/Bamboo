namespace ICP.Document
{
    partial class FormException
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
            this.lblMessage = new DevExpress.XtraEditors.LabelControl();
            this.btnShowDetail = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtErrorInfo = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtErrorInfo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(12, 12);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(192, 14);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "System Error,Please Call  IT Crowd.";
            // 
            // btnShowDetail
            // 
            this.btnShowDetail.Location = new System.Drawing.Point(16, 56);
            this.btnShowDetail.Name = "btnShowDetail";
            this.btnShowDetail.Size = new System.Drawing.Size(105, 23);
            this.btnShowDetail.TabIndex = 1;
            this.btnShowDetail.Text = "Detail ↓";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(205, 56);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            // 
            // txtErrorInfo
            // 
            this.txtErrorInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtErrorInfo.Location = new System.Drawing.Point(0, 97);
            this.txtErrorInfo.Name = "txtErrorInfo";
            this.txtErrorInfo.Size = new System.Drawing.Size(344, 228);
            this.txtErrorInfo.TabIndex = 2;
            // 
            // FormException
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 323);
            this.Controls.Add(this.txtErrorInfo);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnShowDetail);
            this.Controls.Add(this.btnClose);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(350, 350);
            this.MinimizeBox = false;
            this.Name = "FormException";
            this.ShowInTaskbar = false;
            this.Text = "System Exception";
            ((System.ComponentModel.ISupportInitialize)(this.txtErrorInfo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblMessage;
        private DevExpress.XtraEditors.SimpleButton btnShowDetail;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.MemoEdit txtErrorInfo;
    }
}