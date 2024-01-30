namespace ICP.FCM.AirImport.UI.Report
{
    partial class OIReleaseOrder2
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
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.labSpecialInstructions = new DevExpress.XtraEditors.LabelControl();
            this.txtSpecialInstructions = new DevExpress.XtraEditors.MemoEdit();
            this.txtReleaseTo = new DevExpress.XtraEditors.MemoEdit();
            this.labReleaseTo = new DevExpress.XtraEditors.LabelControl();
            this.txtSendTo = new DevExpress.XtraEditors.MemoEdit();
            this.labSendTo = new DevExpress.XtraEditors.LabelControl();
            this.pnlQuery.SuspendLayout();
            this.pnlCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpecialInstructions.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReleaseTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendTo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlQuery
            // 
            this.pnlQuery.Location = new System.Drawing.Point(0, 433);
            this.pnlQuery.Size = new System.Drawing.Size(210, 38);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(50, 7);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "查询";
            // 
            // pnlCondition
            // 
            this.pnlCondition.Controls.Add(this.navBarControl1);
            this.pnlCondition.Size = new System.Drawing.Size(210, 471);
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Size = new System.Drawing.Size(791, 471);
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 197;
            this.navBarControl1.Size = new System.Drawing.Size(210, 471);
            this.navBarControl1.TabIndex = 1;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "ReleaseOrder";
            this.navBarGroup1.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 423;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.labSpecialInstructions);
            this.navBarGroupControlContainer1.Controls.Add(this.txtSpecialInstructions);
            this.navBarGroupControlContainer1.Controls.Add(this.txtReleaseTo);
            this.navBarGroupControlContainer1.Controls.Add(this.labReleaseTo);
            this.navBarGroupControlContainer1.Controls.Add(this.txtSendTo);
            this.navBarGroupControlContainer1.Controls.Add(this.labSendTo);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(206, 421);
            this.navBarGroupControlContainer1.TabIndex = 0;
            this.navBarGroupControlContainer1.Text = "放货通知书";
            // 
            // labSpecialInstructions
            // 
            this.labSpecialInstructions.Location = new System.Drawing.Point(3, 276);
            this.labSpecialInstructions.Name = "labSpecialInstructions";
            this.labSpecialInstructions.Size = new System.Drawing.Size(105, 14);
            this.labSpecialInstructions.TabIndex = 5;
            this.labSpecialInstructions.Text = "Special Instructions";
            // 
            // txtSpecialInstructions
            // 
            this.txtSpecialInstructions.Location = new System.Drawing.Point(3, 296);
            this.txtSpecialInstructions.Name = "txtSpecialInstructions";
            this.txtSpecialInstructions.Size = new System.Drawing.Size(187, 96);
            this.txtSpecialInstructions.TabIndex = 4;
            // 
            // txtReleaseTo
            // 
            this.txtReleaseTo.Location = new System.Drawing.Point(3, 163);
            this.txtReleaseTo.Name = "txtReleaseTo";
            this.txtReleaseTo.Size = new System.Drawing.Size(187, 96);
            this.txtReleaseTo.TabIndex = 3;
            // 
            // labReleaseTo
            // 
            this.labReleaseTo.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Underline);
            this.labReleaseTo.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labReleaseTo.Appearance.Options.UseFont = true;
            this.labReleaseTo.Appearance.Options.UseForeColor = true;
            this.labReleaseTo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labReleaseTo.Location = new System.Drawing.Point(3, 143);
            this.labReleaseTo.Name = "labReleaseTo";
            this.labReleaseTo.Size = new System.Drawing.Size(61, 14);
            this.labReleaseTo.TabIndex = 2;
            this.labReleaseTo.Text = "Release to ";
            this.labReleaseTo.Click += new System.EventHandler(this.labReleaseTo_Click);
            // 
            // txtSendTo
            // 
            this.txtSendTo.Location = new System.Drawing.Point(3, 32);
            this.txtSendTo.Name = "txtSendTo";
            this.txtSendTo.Size = new System.Drawing.Size(187, 96);
            this.txtSendTo.TabIndex = 1;
            // 
            // labSendTo
            // 
            this.labSendTo.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Underline);
            this.labSendTo.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labSendTo.Appearance.Options.UseFont = true;
            this.labSendTo.Appearance.Options.UseForeColor = true;
            this.labSendTo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labSendTo.Location = new System.Drawing.Point(3, 12);
            this.labSendTo.Name = "labSendTo";
            this.labSendTo.Size = new System.Drawing.Size(43, 14);
            this.labSendTo.TabIndex = 0;
            this.labSendTo.Text = "SendTo";
            this.labSendTo.Click += new System.EventHandler(this.labSendTo_Click);
            // 
            // OIReleaseOrder2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "OIReleaseOrder2";
            this.pnlQuery.ResumeLayout(false);
            this.pnlCondition.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpecialInstructions.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReleaseTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendTo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraEditors.LabelControl labSpecialInstructions;
        private DevExpress.XtraEditors.MemoEdit txtSpecialInstructions;
        private DevExpress.XtraEditors.MemoEdit txtReleaseTo;
        private DevExpress.XtraEditors.LabelControl labReleaseTo;
        private DevExpress.XtraEditors.MemoEdit txtSendTo;
        private DevExpress.XtraEditors.LabelControl labSendTo;
    }
}
