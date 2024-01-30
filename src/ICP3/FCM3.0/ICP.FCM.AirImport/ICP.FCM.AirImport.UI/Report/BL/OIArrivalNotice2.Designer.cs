namespace ICP.FCM.AirImport.UI.Report
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
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cboReportTitle = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.rdoArrivalNotice = new DevExpress.XtraEditors.RadioGroup();
            this.pnlQuery.SuspendLayout();
            this.pnlCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboReportTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoArrivalNotice.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlQuery
            // 
            this.pnlQuery.Location = new System.Drawing.Point(0, 434);
            this.pnlQuery.Size = new System.Drawing.Size(210, 37);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(51, 6);
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
            this.navBarGroup1.Caption = "Arrival Notice";
            this.navBarGroup1.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarGroup1.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.AllowDrag;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 423;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.cboReportTitle);
            this.navBarGroupControlContainer1.Controls.Add(this.labelControl1);
            this.navBarGroupControlContainer1.Controls.Add(this.rdoArrivalNotice);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(206, 421);
            this.navBarGroupControlContainer1.TabIndex = 0;
            this.navBarGroupControlContainer1.Text = "放货通知书";
            // 
            // cboReportTitle
            // 
            this.cboReportTitle.Location = new System.Drawing.Point(3, 139);
            this.cboReportTitle.Name = "cboReportTitle";
            this.cboReportTitle.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cboReportTitle.Properties.Appearance.Options.UseBackColor = true;
            this.cboReportTitle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboReportTitle.Size = new System.Drawing.Size(187, 21);
            this.cboReportTitle.SpecifiedBackColor = System.Drawing.Color.White;
            this.cboReportTitle.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 119);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(99, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Arrival Notice Title";
            // 
            // rdoArrivalNotice
            // 
            this.rdoArrivalNotice.Location = new System.Drawing.Point(3, 4);
            this.rdoArrivalNotice.Name = "rdoArrivalNotice";
            this.rdoArrivalNotice.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "Arrival Notice"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "2ND Arrival Notice")});
            this.rdoArrivalNotice.Size = new System.Drawing.Size(187, 96);
            this.rdoArrivalNotice.TabIndex = 0;
            this.rdoArrivalNotice.SelectedIndexChanged += new System.EventHandler(this.rdoArrivalNotice_SelectedIndexChanged);
            // 
            // OIArrivalNotice2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "OIArrivalNotice2";
            this.pnlQuery.ResumeLayout(false);
            this.pnlCondition.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboReportTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoArrivalNotice.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cboReportTitle;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.RadioGroup rdoArrivalNotice;
    }
}
