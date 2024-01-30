namespace ICP.FCM.OceanImport.UI.Report
{
    partial class OIBLPrintPart2
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
            this.groupStyle = new System.Windows.Forms.GroupBox();
            this.labStyle = new DevExpress.XtraEditors.LabelControl();
            this.labHBLNo = new DevExpress.XtraEditors.LabelControl();
            this.cmbHBLNo = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbReportStyle = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labTitle = new DevExpress.XtraEditors.LabelControl();
            this.labHead = new DevExpress.XtraEditors.LabelControl();
            this.cmbHead = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.pnlQuery.SuspendLayout();
            this.pnlCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            this.groupStyle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportStyle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHead.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlQuery
            // 
            this.pnlQuery.Location = new System.Drawing.Point(0, 430);
            this.pnlQuery.Size = new System.Drawing.Size(210, 41);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(62, 8);
            this.btnQuery.TabIndex = 0;
            // 
            // pnlCondition
            // 
            this.pnlCondition.Controls.Add(this.groupStyle);
            this.pnlCondition.Size = new System.Drawing.Size(210, 471);
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Size = new System.Drawing.Size(791, 471);
            // 
            // groupStyle
            // 
            this.groupStyle.Controls.Add(this.labStyle);
            this.groupStyle.Controls.Add(this.labHBLNo);
            this.groupStyle.Controls.Add(this.cmbHBLNo);
            this.groupStyle.Controls.Add(this.cmbReportStyle);
            this.groupStyle.Controls.Add(this.cmbCompany);
            this.groupStyle.Controls.Add(this.labTitle);
            this.groupStyle.Controls.Add(this.labHead);
            this.groupStyle.Controls.Add(this.cmbHead);
            this.groupStyle.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupStyle.Location = new System.Drawing.Point(0, 0);
            this.groupStyle.Name = "groupStyle";
            this.groupStyle.Size = new System.Drawing.Size(210, 161);
            this.groupStyle.TabIndex = 3;
            this.groupStyle.TabStop = false;
            this.groupStyle.Text = "Style";
            // 
            // labStyle
            // 
            this.labStyle.Location = new System.Drawing.Point(10, 24);
            this.labStyle.Name = "labStyle";
            this.labStyle.Size = new System.Drawing.Size(27, 14);
            this.labStyle.TabIndex = 0;
            this.labStyle.Text = "Style";
            // 
            // labHBLNo
            // 
            this.labHBLNo.Location = new System.Drawing.Point(10, 108);
            this.labHBLNo.Name = "labHBLNo";
            this.labHBLNo.Size = new System.Drawing.Size(40, 14);
            this.labHBLNo.TabIndex = 0;
            this.labHBLNo.Text = "HBL No";
            // 
            // cmbHBLNo
            // 
            this.cmbHBLNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbHBLNo.Location = new System.Drawing.Point(65, 105);
            this.cmbHBLNo.Name = "cmbHBLNo";
            this.cmbHBLNo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbHBLNo.Properties.Appearance.Options.UseBackColor = true;
            this.cmbHBLNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHBLNo.Size = new System.Drawing.Size(137, 21);
            this.cmbHBLNo.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbHBLNo.TabIndex = 3;
            // 
            // cmbReportStyle
            // 
            this.cmbReportStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbReportStyle.Location = new System.Drawing.Point(65, 21);
            this.cmbReportStyle.Name = "cmbReportStyle";
            this.cmbReportStyle.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbReportStyle.Properties.Appearance.Options.UseBackColor = true;
            this.cmbReportStyle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReportStyle.Size = new System.Drawing.Size(137, 21);
            this.cmbReportStyle.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbReportStyle.TabIndex = 0;
            this.cmbReportStyle.SelectedIndexChanged += new System.EventHandler(this.cmbReportStyle_SelectedIndexChanged);
            // 
            // cmbCompany
            // 
            this.cmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCompany.Location = new System.Drawing.Point(65, 49);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Size = new System.Drawing.Size(137, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCompany.TabIndex = 1;
            // 
            // labTitle
            // 
            this.labTitle.Location = new System.Drawing.Point(10, 53);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(24, 14);
            this.labTitle.TabIndex = 0;
            this.labTitle.Text = "Title";
            // 
            // labHead
            // 
            this.labHead.Location = new System.Drawing.Point(10, 80);
            this.labHead.Name = "labHead";
            this.labHead.Size = new System.Drawing.Size(28, 14);
            this.labHead.TabIndex = 0;
            this.labHead.Text = "Head";
            // 
            // cmbHead
            // 
            this.cmbHead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbHead.Location = new System.Drawing.Point(65, 77);
            this.cmbHead.Name = "cmbHead";
            this.cmbHead.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbHead.Properties.Appearance.Options.UseBackColor = true;
            this.cmbHead.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHead.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("BILL OF LADING", "BILL OF LADING", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("CARGO RECEIPT", "CARGO RECEIPT", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Bill of Lading Master", "Bill of Lading Master", -1)});
            this.cmbHead.Size = new System.Drawing.Size(137, 21);
            this.cmbHead.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbHead.TabIndex = 2;
            // 
            // OIBLPrintPart2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "OIBLPrintPart2";
            this.pnlQuery.ResumeLayout(false);
            this.pnlCondition.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            this.groupStyle.ResumeLayout(false);
            this.groupStyle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportStyle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHead.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupStyle;
        private DevExpress.XtraEditors.LabelControl labStyle;
        private DevExpress.XtraEditors.LabelControl labHBLNo;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbHBLNo;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbReportStyle;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.LabelControl labTitle;
        private DevExpress.XtraEditors.LabelControl labHead;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbHead;
    }
}
