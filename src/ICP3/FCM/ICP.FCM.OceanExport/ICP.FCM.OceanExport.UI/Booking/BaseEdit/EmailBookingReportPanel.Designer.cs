namespace ICP.FCM.OceanExport.UI.Booking.BaseEdit
{
    partial class EmailBookingReportPanel
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
            this.rdoDefault = new System.Windows.Forms.RadioButton();
            this.rdoCSCL = new System.Windows.Forms.RadioButton();
            this.pCSCL = new DevExpress.XtraEditors.PanelControl();
            this.rdoPayHK = new System.Windows.Forms.RadioButton();
            this.rdoPayChina = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pCSCL)).BeginInit();
            this.pCSCL.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdoDefault
            // 
            this.rdoDefault.AutoSize = true;
            this.rdoDefault.Checked = true;
            this.rdoDefault.Location = new System.Drawing.Point(84, 12);
            this.rdoDefault.Name = "rdoDefault";
            this.rdoDefault.Size = new System.Drawing.Size(65, 16);
            this.rdoDefault.TabIndex = 0;
            this.rdoDefault.TabStop = true;
            this.rdoDefault.Text = "Default";
            this.rdoDefault.UseVisualStyleBackColor = true;
            this.rdoDefault.CheckedChanged += new System.EventHandler(this.rdoDefault_CheckedChanged);
            // 
            // rdoCSCL
            // 
            this.rdoCSCL.AutoSize = true;
            this.rdoCSCL.Location = new System.Drawing.Point(207, 12);
            this.rdoCSCL.Name = "rdoCSCL";
            this.rdoCSCL.Size = new System.Drawing.Size(47, 16);
            this.rdoCSCL.TabIndex = 0;
            this.rdoCSCL.Text = "CSCL";
            this.rdoCSCL.UseVisualStyleBackColor = true;
            this.rdoCSCL.CheckedChanged += new System.EventHandler(this.rdoCSCL_CheckedChanged);
            // 
            // pCSCL
            // 
            this.pCSCL.Controls.Add(this.rdoPayHK);
            this.pCSCL.Controls.Add(this.rdoPayChina);
            this.pCSCL.Controls.Add(this.label1);
            this.pCSCL.Location = new System.Drawing.Point(12, 34);
            this.pCSCL.Name = "pCSCL";
            this.pCSCL.Size = new System.Drawing.Size(372, 70);
            this.pCSCL.TabIndex = 1;
            // 
            // rdoPayHK
            // 
            this.rdoPayHK.AutoSize = true;
            this.rdoPayHK.Location = new System.Drawing.Point(165, 24);
            this.rdoPayHK.Name = "rdoPayHK";
            this.rdoPayHK.Size = new System.Drawing.Size(77, 16);
            this.rdoPayHK.TabIndex = 3;
            this.rdoPayHK.Text = "Hong Kong";
            this.rdoPayHK.UseVisualStyleBackColor = true;
            // 
            // rdoPayChina
            // 
            this.rdoPayChina.AutoSize = true;
            this.rdoPayChina.Checked = true;
            this.rdoPayChina.Location = new System.Drawing.Point(86, 24);
            this.rdoPayChina.Name = "rdoPayChina";
            this.rdoPayChina.Size = new System.Drawing.Size(53, 16);
            this.rdoPayChina.TabIndex = 3;
            this.rdoPayChina.TabStop = true;
            this.rdoPayChina.Text = "China";
            this.rdoPayChina.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "付款地:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(309, 110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(228, 110);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // EmailBookingReportPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 147);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pCSCL);
            this.Controls.Add(this.rdoCSCL);
            this.Controls.Add(this.rdoDefault);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmailBookingReportPanel";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmailBookingReportPanel";
            ((System.ComponentModel.ISupportInitialize)(this.pCSCL)).EndInit();
            this.pCSCL.ResumeLayout(false);
            this.pCSCL.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoDefault;
        private System.Windows.Forms.RadioButton rdoCSCL;
        private DevExpress.XtraEditors.PanelControl pCSCL;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton rdoPayHK;
        private System.Windows.Forms.RadioButton rdoPayChina;
        private System.Windows.Forms.Label label1;
    }
}