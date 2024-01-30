namespace ICP.TMS.UI
{
    partial class DriverSearchPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DriverSearchPanel));
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup3 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.dmcTime = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupBaseInfo = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.ckbValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.txtCardNo = new DevExpress.XtraEditors.TextEdit();
            this.labValid = new DevExpress.XtraEditors.LabelControl();
            this.labCardNo = new DevExpress.XtraEditors.LabelControl();
            this.txtMobile = new DevExpress.XtraEditors.TextEdit();
            this.labMobile = new DevExpress.XtraEditors.LabelControl();
            this.txtDriverName = new DevExpress.XtraEditors.TextEdit();
            this.labDriverName = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer3.SuspendLayout();
            this.navBarGroupBaseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCardNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDriverName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup3;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer3);
            this.navBarControl1.Controls.Add(this.navBarGroupBaseInfo);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupInterval = 2;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.navBarGroup3});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(195, 442);
            this.navBarControl1.TabIndex = 6;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup3
            // 
            this.navBarGroup3.Caption = "创建时间";
            this.navBarGroup3.ControlContainer = this.navBarGroupControlContainer3;
            this.navBarGroup3.Expanded = true;
            this.navBarGroup3.GroupClientHeight = 152;
            this.navBarGroup3.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup3.Name = "navBarGroup3";
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.dmcTime);
            this.navBarGroupControlContainer3.Controls.Add(this.labTo);
            this.navBarGroupControlContainer3.Controls.Add(this.labFrom);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(187, 150);
            this.navBarGroupControlContainer3.TabIndex = 2;
            // 
            // dmcTime
            // 
            this.dmcTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dmcTime.IsEngish = false;
            this.dmcTime.Location = new System.Drawing.Point(73, 3);
            this.dmcTime.Name = "dmcTime";
            this.dmcTime.Size = new System.Drawing.Size(109, 142);
            this.dmcTime.TabIndex = 0;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(4, 118);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(48, 14);
            this.labTo.TabIndex = 37;
            this.labTo.Text = "结束日期";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(4, 94);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(48, 14);
            this.labFrom.TabIndex = 36;
            this.labFrom.Text = "开始日期";
            // 
            // navBarGroupBaseInfo
            // 
            this.navBarGroupBaseInfo.Controls.Add(this.ckbValid);
            this.navBarGroupBaseInfo.Controls.Add(this.txtCardNo);
            this.navBarGroupBaseInfo.Controls.Add(this.labValid);
            this.navBarGroupBaseInfo.Controls.Add(this.labCardNo);
            this.navBarGroupBaseInfo.Controls.Add(this.txtMobile);
            this.navBarGroupBaseInfo.Controls.Add(this.labMobile);
            this.navBarGroupBaseInfo.Controls.Add(this.txtDriverName);
            this.navBarGroupBaseInfo.Controls.Add(this.labDriverName);
            this.navBarGroupBaseInfo.Name = "navBarGroupBaseInfo";
            this.navBarGroupBaseInfo.Size = new System.Drawing.Size(187, 117);
            this.navBarGroupBaseInfo.TabIndex = 0;
            // 
            // ckbValid
            // 
            this.ckbValid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbValid.Checked = null;
            this.ckbValid.CheckedText = "TRUE";
            this.ckbValid.Location = new System.Drawing.Point(77, 88);
            this.ckbValid.Name = "ckbValid";
            this.ckbValid.NULLText = "ALL";
            this.ckbValid.Size = new System.Drawing.Size(104, 22);
            this.ckbValid.TabIndex = 3;
            this.ckbValid.UnCheckedText = "FALSE";
            // 
            // txtCardNo
            // 
            this.txtCardNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCardNo.Location = new System.Drawing.Point(78, 61);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(104, 21);
            this.txtCardNo.TabIndex = 2;
            // 
            // labValid
            // 
            this.labValid.Location = new System.Drawing.Point(4, 92);
            this.labValid.Name = "labValid";
            this.labValid.Size = new System.Drawing.Size(36, 14);
            this.labValid.TabIndex = 31;
            this.labValid.Text = "有效性";
            // 
            // labCardNo
            // 
            this.labCardNo.Location = new System.Drawing.Point(4, 64);
            this.labCardNo.Name = "labCardNo";
            this.labCardNo.Size = new System.Drawing.Size(36, 14);
            this.labCardNo.TabIndex = 31;
            this.labCardNo.Text = "身份ID";
            // 
            // txtMobile
            // 
            this.txtMobile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMobile.Location = new System.Drawing.Point(78, 34);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(104, 21);
            this.txtMobile.TabIndex = 1;
            // 
            // labMobile
            // 
            this.labMobile.Location = new System.Drawing.Point(4, 37);
            this.labMobile.Name = "labMobile";
            this.labMobile.Size = new System.Drawing.Size(24, 14);
            this.labMobile.TabIndex = 31;
            this.labMobile.Text = "手机";
            // 
            // txtDriverName
            // 
            this.txtDriverName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDriverName.Location = new System.Drawing.Point(78, 7);
            this.txtDriverName.Name = "txtDriverName";
            this.txtDriverName.Size = new System.Drawing.Size(104, 21);
            this.txtDriverName.TabIndex = 0;
            // 
            // labDriverName
            // 
            this.labDriverName.Location = new System.Drawing.Point(4, 7);
            this.labDriverName.Name = "labDriverName";
            this.labDriverName.Size = new System.Drawing.Size(36, 14);
            this.labDriverName.TabIndex = 25;
            this.labDriverName.Text = "司机名";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "基本条件";
            this.navBarGroup1.ControlContainer = this.navBarGroupBaseInfo;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 119;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 476);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(195, 56);
            this.panel1.TabIndex = 7;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(26, 19);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "清空(&L)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(126, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // DriverSearchPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.navBarControl1);
            this.Name = "DriverSearchPanel";
            this.Size = new System.Drawing.Size(195, 532);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer3.ResumeLayout(false);
            this.navBarGroupControlContainer3.PerformLayout();
            this.navBarGroupBaseInfo.ResumeLayout(false);
            this.navBarGroupBaseInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCardNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDriverName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup3;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        protected DevExpress.XtraEditors.LabelControl labTo;
        protected DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBaseInfo;
        private DevExpress.XtraEditors.TextEdit txtMobile;
        private DevExpress.XtraEditors.LabelControl labMobile;
        private DevExpress.XtraEditors.TextEdit txtDriverName;
        private DevExpress.XtraEditors.LabelControl labDriverName;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private ICP.Framework.ClientComponents.Controls.DateMonthControl dmcTime;
        private DevExpress.XtraEditors.TextEdit txtCardNo;
        private DevExpress.XtraEditors.LabelControl labCardNo;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton ckbValid;
        private DevExpress.XtraEditors.LabelControl labValid;
    }
}
