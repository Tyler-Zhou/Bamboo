namespace ICP.FCM.DomesticTrade.UI.Booking
{
    partial class DTTruckPrintPart
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupStyle = new System.Windows.Forms.GroupBox();
            this.labCompanyName = new DevExpress.XtraEditors.LabelControl();
            this.labAddress = new DevExpress.XtraEditors.LabelControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnShow = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.previewControl1 = new ICP.Common.UI.ReportView.FastReportViewerPart();
            this.report1 = new FastReport.Report();
            this.groupGeneral = new System.Windows.Forms.GroupBox();
            this.rdoStyle = new DevExpress.XtraEditors.RadioGroup();
            this.chkManualTitle = new DevExpress.XtraEditors.CheckEdit();
            this.txtCompanyName = new DevExpress.XtraEditors.TextEdit();
            this.txtAddress = new DevExpress.XtraEditors.MemoEdit();
            this.labTel = new DevExpress.XtraEditors.LabelControl();
            this.txtTel = new DevExpress.XtraEditors.TextEdit();
            this.labFax = new DevExpress.XtraEditors.LabelControl();
            this.txtFax = new DevExpress.XtraEditors.TextEdit();
            this.labEmail = new DevExpress.XtraEditors.LabelControl();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.paneTitle = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupStyle.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.report1)).BeginInit();
            this.groupGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoStyle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkManualTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            this.paneTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.groupGeneral);
            this.panel1.Controls.Add(this.groupStyle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(236, 449);
            this.panel1.TabIndex = 0;
            // 
            // groupStyle
            // 
            this.groupStyle.Controls.Add(this.rdoStyle);
            this.groupStyle.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupStyle.Location = new System.Drawing.Point(0, 0);
            this.groupStyle.Name = "groupStyle";
            this.groupStyle.Size = new System.Drawing.Size(236, 82);
            this.groupStyle.TabIndex = 2;
            this.groupStyle.TabStop = false;
            this.groupStyle.Text = "Style";
            // 
            // labCompanyName
            // 
            this.labCompanyName.Location = new System.Drawing.Point(3, 3);
            this.labCompanyName.Name = "labCompanyName";
            this.labCompanyName.Size = new System.Drawing.Size(85, 14);
            this.labCompanyName.TabIndex = 0;
            this.labCompanyName.Text = "Company Name";
            // 
            // labAddress
            // 
            this.labAddress.Location = new System.Drawing.Point(3, 44);
            this.labAddress.Name = "labAddress";
            this.labAddress.Size = new System.Drawing.Size(43, 14);
            this.labAddress.TabIndex = 0;
            this.labAddress.Text = "Address";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnShow);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 449);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(236, 47);
            this.panel2.TabIndex = 0;
            // 
            // btnShow
            // 
            this.btnShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShow.Location = new System.Drawing.Point(65, 11);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.TabIndex = 1;
            this.btnShow.Text = "&Show";
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.panel3);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.panel4);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(802, 496);
            this.splitContainerControl1.SplitterPosition = 236;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(236, 496);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.previewControl1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(560, 496);
            this.panel4.TabIndex = 3;
            // 
            // previewControl1
            // 
            this.previewControl1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.previewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewControl1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.previewControl1.Location = new System.Drawing.Point(0, 0);
            this.previewControl1.Name = "previewControl1";
            this.previewControl1.Size = new System.Drawing.Size(560, 496);
            this.previewControl1.TabIndex = 0;
            // 
            // report1
            // 
            this.report1.ReportResourceString = "﻿<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Report ReportInfo.Created=\"09/14/2010 1" +
                "5:52:34\" ReportInfo.Modified=\"06/22/2011 17:09:08\" ReportInfo.CreatorVersion=\"1." +
                "2.47.0\">\r\n  <Dictionary/>\r\n</Report>\r\n";
            // 
            // groupGeneral
            // 
            this.groupGeneral.Controls.Add(this.paneTitle);
            this.groupGeneral.Controls.Add(this.chkManualTitle);
            this.groupGeneral.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupGeneral.Location = new System.Drawing.Point(0, 82);
            this.groupGeneral.Name = "groupGeneral";
            this.groupGeneral.Size = new System.Drawing.Size(236, 296);
            this.groupGeneral.TabIndex = 3;
            this.groupGeneral.TabStop = false;
            this.groupGeneral.Text = "General";
            // 
            // rdoStyle
            // 
            this.rdoStyle.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdoStyle.Location = new System.Drawing.Point(3, 18);
            this.rdoStyle.Name = "rdoStyle";
            this.rdoStyle.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Pick Up_Delivery"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Pick Up_Delivery(Short)")});
            this.rdoStyle.Size = new System.Drawing.Size(230, 58);
            this.rdoStyle.TabIndex = 0;
            // 
            // chkManualTitle
            // 
            this.chkManualTitle.Location = new System.Drawing.Point(6, 16);
            this.chkManualTitle.Name = "chkManualTitle";
            this.chkManualTitle.Properties.Caption = "Manual Title";
            this.chkManualTitle.Size = new System.Drawing.Size(157, 19);
            this.chkManualTitle.TabIndex = 1;
            this.chkManualTitle.CheckedChanged += new System.EventHandler(this.chkManualTitle_CheckedChanged);
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(3, 20);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(222, 21);
            this.txtCompanyName.TabIndex = 2;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(3, 64);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(222, 55);
            this.txtAddress.TabIndex = 3;
            // 
            // labTel
            // 
            this.labTel.Location = new System.Drawing.Point(3, 124);
            this.labTel.Name = "labTel";
            this.labTel.Size = new System.Drawing.Size(17, 14);
            this.labTel.TabIndex = 0;
            this.labTel.Text = "Tel";
            // 
            // txtTel
            // 
            this.txtTel.Location = new System.Drawing.Point(3, 141);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(222, 21);
            this.txtTel.TabIndex = 2;
            // 
            // labFax
            // 
            this.labFax.Location = new System.Drawing.Point(3, 168);
            this.labFax.Name = "labFax";
            this.labFax.Size = new System.Drawing.Size(18, 14);
            this.labFax.TabIndex = 0;
            this.labFax.Text = "Fax";
            // 
            // txtFax
            // 
            this.txtFax.Location = new System.Drawing.Point(3, 185);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(222, 21);
            this.txtFax.TabIndex = 2;
            // 
            // labEmail
            // 
            this.labEmail.Location = new System.Drawing.Point(3, 212);
            this.labEmail.Name = "labEmail";
            this.labEmail.Size = new System.Drawing.Size(27, 14);
            this.labEmail.TabIndex = 0;
            this.labEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(3, 229);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(222, 21);
            this.txtEmail.TabIndex = 2;
            // 
            // paneTitle
            // 
            this.paneTitle.Controls.Add(this.labCompanyName);
            this.paneTitle.Controls.Add(this.txtAddress);
            this.paneTitle.Controls.Add(this.labAddress);
            this.paneTitle.Controls.Add(this.txtEmail);
            this.paneTitle.Controls.Add(this.labTel);
            this.paneTitle.Controls.Add(this.txtFax);
            this.paneTitle.Controls.Add(this.labFax);
            this.paneTitle.Controls.Add(this.txtTel);
            this.paneTitle.Controls.Add(this.labEmail);
            this.paneTitle.Controls.Add(this.txtCompanyName);
            this.paneTitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.paneTitle.Enabled = false;
            this.paneTitle.Location = new System.Drawing.Point(3, 34);
            this.paneTitle.Name = "paneTitle";
            this.paneTitle.Size = new System.Drawing.Size(230, 259);
            this.paneTitle.TabIndex = 4;
            // 
            // OceanTruckPrintPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "OceanTruckPrintPart";
            this.Size = new System.Drawing.Size(802, 496);
            this.panel1.ResumeLayout(false);
            this.groupStyle.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.report1)).EndInit();
            this.groupGeneral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rdoStyle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkManualTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            this.paneTitle.ResumeLayout(false);
            this.paneTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        protected DevExpress.XtraEditors.SimpleButton btnShow;
        private DevExpress.XtraEditors.LabelControl labCompanyName;
        private DevExpress.XtraEditors.LabelControl labAddress;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.Panel panel3;
        private FastReport.Report report1;
        private System.Windows.Forms.Panel panel4;
        private ICP.Common.UI.ReportView.FastReportViewerPart previewControl1;
        private System.Windows.Forms.GroupBox groupStyle;
        private System.Windows.Forms.GroupBox groupGeneral;
        private DevExpress.XtraEditors.CheckEdit chkManualTitle;
        private DevExpress.XtraEditors.RadioGroup rdoStyle;
        private System.Windows.Forms.Panel paneTitle;
        private DevExpress.XtraEditors.MemoEdit txtAddress;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl labTel;
        private DevExpress.XtraEditors.TextEdit txtFax;
        private DevExpress.XtraEditors.LabelControl labFax;
        private DevExpress.XtraEditors.TextEdit txtTel;
        private DevExpress.XtraEditors.LabelControl labEmail;
        private DevExpress.XtraEditors.TextEdit txtCompanyName;
    }
}
