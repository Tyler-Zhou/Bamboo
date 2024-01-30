namespace ICP.FCM.OceanImport.UI.Report
{
    partial class OIArrivalNotice2
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
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.cboReportTitle = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.rdoArrivalNotice = new DevExpress.XtraEditors.RadioGroup();
            this.labHBLNo = new DevExpress.XtraEditors.LabelControl();
            this.cmbHBLNo = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.pnlQuery.SuspendLayout();
            this.pnlCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboReportTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoArrivalNotice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlQuery
            // 
            this.pnlQuery.Location = new System.Drawing.Point(0, 435);
            this.pnlQuery.Size = new System.Drawing.Size(210, 36);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(50, 7);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "查询";
            // 
            // pnlCondition
            // 
            this.pnlCondition.Controls.Add(this.cmbHBLNo);
            this.pnlCondition.Controls.Add(this.labHBLNo);
            this.pnlCondition.Controls.Add(this.cboReportTitle);
            this.pnlCondition.Controls.Add(this.labelControl1);
            this.pnlCondition.Controls.Add(this.rdoArrivalNotice);
            this.pnlCondition.Size = new System.Drawing.Size(210, 471);
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Size = new System.Drawing.Size(791, 471);
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "Arrival Notice";
            this.navBarGroup1.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.AllowDrag;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 423;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // cboReportTitle
            // 
            this.cboReportTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboReportTitle.Location = new System.Drawing.Point(4, 137);
            this.cboReportTitle.Name = "cboReportTitle";
            this.cboReportTitle.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cboReportTitle.Properties.Appearance.Options.UseBackColor = true;
            this.cboReportTitle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboReportTitle.Size = new System.Drawing.Size(203, 21);
            this.cboReportTitle.SpecifiedBackColor = System.Drawing.Color.White;
            this.cboReportTitle.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 117);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(99, 14);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Arrival Notice Title";
            // 
            // rdoArrivalNotice
            // 
            this.rdoArrivalNotice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoArrivalNotice.Location = new System.Drawing.Point(4, 2);
            this.rdoArrivalNotice.Name = "rdoArrivalNotice";
            this.rdoArrivalNotice.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "Arrival Notice"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "2ND Arrival Notice")});
            this.rdoArrivalNotice.Size = new System.Drawing.Size(203, 95);
            this.rdoArrivalNotice.TabIndex = 0;
            // 
            // labHBLNo
            // 
            this.labHBLNo.Location = new System.Drawing.Point(7, 167);
            this.labHBLNo.Name = "labHBLNo";
            this.labHBLNo.Size = new System.Drawing.Size(40, 14);
            this.labHBLNo.TabIndex = 5;
            this.labHBLNo.Text = "HBL No";
            // 
            // cmbHBLNo
            // 
            this.cmbHBLNo.Location = new System.Drawing.Point(5, 186);
            this.cmbHBLNo.Name = "cmbHBLNo";
            this.cmbHBLNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHBLNo.Size = new System.Drawing.Size(202, 21);
            this.cmbHBLNo.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbHBLNo.TabIndex = 6;
            // 
            // OIArrivalNotice2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "OIArrivalNotice2";
            this.pnlQuery.ResumeLayout(false);
            this.pnlCondition.ResumeLayout(false);
            this.pnlCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboReportTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoArrivalNotice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cboReportTitle;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.RadioGroup rdoArrivalNotice;
        private DevExpress.XtraEditors.LabelControl labHBLNo;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbHBLNo;

    }
}
