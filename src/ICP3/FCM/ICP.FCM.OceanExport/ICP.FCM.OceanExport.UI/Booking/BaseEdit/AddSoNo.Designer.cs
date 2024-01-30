namespace ICP.FCM.OceanExport.UI.Booking.BaseEdit
{
    partial class AddSoNo
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
            this.labOrderNo = new DevExpress.XtraEditors.LabelControl();
            this.txtSoNo = new DevExpress.XtraEditors.TextEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labOrderNo
            // 
            this.labOrderNo.Location = new System.Drawing.Point(16, 18);
            this.labOrderNo.Name = "labOrderNo";
            this.labOrderNo.Size = new System.Drawing.Size(69, 14);
            this.labOrderNo.TabIndex = 544;
            this.labOrderNo.Text = "SONo/RefNo";
            // 
            // txtSoNo
            // 
            this.txtSoNo.Location = new System.Drawing.Point(99, 14);
            this.txtSoNo.Name = "txtSoNo";
            this.txtSoNo.Size = new System.Drawing.Size(155, 21);
            this.txtSoNo.TabIndex = 545;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(34, 50);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 546;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(156, 50);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 547;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // AddSoNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtSoNo);
            this.Controls.Add(this.labOrderNo);
            this.Name = "AddSoNo";
            this.Size = new System.Drawing.Size(274, 90);
            ((System.ComponentModel.ISupportInitialize)(this.txtSoNo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labOrderNo;
        private DevExpress.XtraEditors.TextEdit txtSoNo;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClose;
    }
}
