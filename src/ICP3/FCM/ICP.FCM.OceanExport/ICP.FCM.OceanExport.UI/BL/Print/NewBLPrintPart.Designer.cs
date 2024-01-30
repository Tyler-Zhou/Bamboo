namespace ICP.FCM.OceanExport.UI.BL
{
    partial class NewBLPrintPart
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
            this.chkOrderNo = new DevExpress.XtraEditors.CheckEdit();
            this.chkShowAMS = new DevExpress.XtraEditors.CheckEdit();
            this.labStyle = new DevExpress.XtraEditors.LabelControl();
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
            ((System.ComponentModel.ISupportInitialize)(this.chkOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowAMS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportStyle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHead.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlQuery
            // 
            this.pnlQuery.Location = new System.Drawing.Point(0, 441);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(69, 3);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "查询";
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
            this.groupStyle.Controls.Add(this.chkOrderNo);
            this.groupStyle.Controls.Add(this.chkShowAMS);
            this.groupStyle.Controls.Add(this.labStyle);
            this.groupStyle.Controls.Add(this.cmbReportStyle);
            this.groupStyle.Controls.Add(this.cmbCompany);
            this.groupStyle.Controls.Add(this.labTitle);
            this.groupStyle.Controls.Add(this.labHead);
            this.groupStyle.Controls.Add(this.cmbHead);
            this.groupStyle.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupStyle.Location = new System.Drawing.Point(0, 0);
            this.groupStyle.Name = "groupStyle";
            this.groupStyle.Size = new System.Drawing.Size(210, 125);
            this.groupStyle.TabIndex = 3;
            this.groupStyle.TabStop = false;
            this.groupStyle.Text = "Style";
            // 
            // chkOrderNo
            // 
            this.chkOrderNo.Location = new System.Drawing.Point(6, 102);
            this.chkOrderNo.Name = "chkOrderNo";
            this.chkOrderNo.Properties.Caption = "Show OrderNo";
            this.chkOrderNo.Size = new System.Drawing.Size(105, 19);
            this.chkOrderNo.TabIndex = 3;
            // 
            // chkShowAMS
            // 
            this.chkShowAMS.Location = new System.Drawing.Point(110, 102);
            this.chkShowAMS.Name = "chkShowAMS";
            this.chkShowAMS.Properties.Caption = "Show AMS";
            this.chkShowAMS.Size = new System.Drawing.Size(99, 19);
            this.chkShowAMS.TabIndex = 3;
            // 
            // labStyle
            // 
            this.labStyle.Location = new System.Drawing.Point(6, 24);
            this.labStyle.Name = "labStyle";
            this.labStyle.Size = new System.Drawing.Size(27, 14);
            this.labStyle.TabIndex = 0;
            this.labStyle.Text = "Style";
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
            this.cmbCompany.Location = new System.Drawing.Point(65, 48);
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
            this.labTitle.Location = new System.Drawing.Point(6, 51);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(24, 14);
            this.labTitle.TabIndex = 0;
            this.labTitle.Text = "Title";
            // 
            // labHead
            // 
            this.labHead.Location = new System.Drawing.Point(6, 78);
            this.labHead.Name = "labHead";
            this.labHead.Size = new System.Drawing.Size(28, 14);
            this.labHead.TabIndex = 0;
            this.labHead.Text = "Head";
            // 
            // cmbHead
            // 
            this.cmbHead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbHead.Location = new System.Drawing.Point(65, 75);
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
            // NewBLPrintPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "NewBLPrintPart";
            this.pnlQuery.ResumeLayout(false);
            this.pnlCondition.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            this.groupStyle.ResumeLayout(false);
            this.groupStyle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowAMS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportStyle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHead.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupStyle;
        private DevExpress.XtraEditors.CheckEdit chkShowAMS;
        private DevExpress.XtraEditors.LabelControl labStyle;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbReportStyle;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.LabelControl labTitle;
        private DevExpress.XtraEditors.LabelControl labHead;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbHead;
        private DevExpress.XtraEditors.CheckEdit chkOrderNo;
    }
}
