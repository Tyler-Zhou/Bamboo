namespace ICP.FCM.Common.UI.BillRevise
{
    partial class OEBillRevisePart
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gcRemark = new DevExpress.XtraEditors.GroupControl();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.lblDisptDate = new System.Windows.Forms.Label();
            this.lblDisptDateValue = new System.Windows.Forms.Label();
            this.lblDisptUserValue = new System.Windows.Forms.Label();
            this.lblDisptUser = new System.Windows.Forms.Label();
            this.pnlCenter = new DevExpress.XtraEditors.PanelControl();
            this.butReject = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRemark)).BeginInit();
            this.gcRemark.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCenter)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.butReject);
            this.panelControl1.Controls.Add(this.gcRemark);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 519);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(802, 71);
            this.panelControl1.TabIndex = 1;
            // 
            // gcRemark
            // 
            this.gcRemark.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gcRemark.Controls.Add(this.txtRemark);
            this.gcRemark.Location = new System.Drawing.Point(109, 4);
            this.gcRemark.Name = "gcRemark";
            this.gcRemark.Size = new System.Drawing.Size(690, 67);
            this.gcRemark.TabIndex = 2;
            this.gcRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRemark.Enabled = false;
            this.txtRemark.Location = new System.Drawing.Point(2, 23);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(686, 42);
            this.txtRemark.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(14, 9);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(82, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "签     收";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblDisptDate);
            this.pnlTop.Controls.Add(this.lblDisptDateValue);
            this.pnlTop.Controls.Add(this.lblDisptUserValue);
            this.pnlTop.Controls.Add(this.lblDisptUser);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(802, 34);
            this.pnlTop.TabIndex = 2;
            // 
            // lblDisptDate
            // 
            this.lblDisptDate.AutoSize = true;
            this.lblDisptDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDisptDate.Location = new System.Drawing.Point(215, 10);
            this.lblDisptDate.Name = "lblDisptDate";
            this.lblDisptDate.Size = new System.Drawing.Size(67, 14);
            this.lblDisptDate.TabIndex = 11;
            this.lblDisptDate.Text = "申请时间：";
            // 
            // lblDisptDateValue
            // 
            this.lblDisptDateValue.AutoSize = true;
            this.lblDisptDateValue.BackColor = System.Drawing.Color.Transparent;
            this.lblDisptDateValue.Location = new System.Drawing.Point(302, 10);
            this.lblDisptDateValue.Name = "lblDisptDateValue";
            this.lblDisptDateValue.Size = new System.Drawing.Size(129, 14);
            this.lblDisptDateValue.TabIndex = 12;
            this.lblDisptDateValue.Text = "2013-06-14  12:23:34";
            // 
            // lblDisptUserValue
            // 
            this.lblDisptUserValue.AutoSize = true;
            this.lblDisptUserValue.BackColor = System.Drawing.Color.Transparent;
            this.lblDisptUserValue.Location = new System.Drawing.Point(106, 10);
            this.lblDisptUserValue.Name = "lblDisptUserValue";
            this.lblDisptUserValue.Size = new System.Drawing.Size(24, 14);
            this.lblDisptUserValue.TabIndex = 10;
            this.lblDisptUserValue.Text = "joe";
            // 
            // lblDisptUser
            // 
            this.lblDisptUser.AutoSize = true;
            this.lblDisptUser.BackColor = System.Drawing.Color.Transparent;
            this.lblDisptUser.Location = new System.Drawing.Point(29, 10);
            this.lblDisptUser.Name = "lblDisptUser";
            this.lblDisptUser.Size = new System.Drawing.Size(55, 14);
            this.lblDisptUser.TabIndex = 9;
            this.lblDisptUser.Text = "申请人：";
            // 
            // pnlCenter
            // 
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(0, 34);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(802, 485);
            this.pnlCenter.TabIndex = 3;
            // 
            // butReject
            // 
            this.butReject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.butReject.Location = new System.Drawing.Point(14, 43);
            this.butReject.Name = "butReject";
            this.butReject.Size = new System.Drawing.Size(82, 23);
            this.butReject.TabIndex = 4;
            this.butReject.Text = "拒    签";
            this.butReject.Click += new System.EventHandler(this.butReject_Click);
            // 
            // OEBillRevisePart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.panelControl1);
            this.Name = "OEBillRevisePart";
            this.Size = new System.Drawing.Size(802, 590);
            this.Load += new System.EventHandler(this.OIDispachCompare_Load);
            this.Resize += new System.EventHandler(this.OIDispachCompare_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRemark)).EndInit();
            this.gcRemark.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCenter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Microsoft.Practices.CompositeUI.WinForms.TabWorkspace tabWsCampare;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.GroupControl gcRemark;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.PanelControl pnlCenter;
        private System.Windows.Forms.Label lblDisptDate;
        private System.Windows.Forms.Label lblDisptDateValue;
        private System.Windows.Forms.Label lblDisptUserValue;
        private System.Windows.Forms.Label lblDisptUser;
        private DevExpress.XtraEditors.SimpleButton butReject;

    }
}
