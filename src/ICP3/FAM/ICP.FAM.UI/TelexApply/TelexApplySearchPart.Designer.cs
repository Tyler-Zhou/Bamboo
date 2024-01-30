namespace ICP.FAM.UI.TelexApply
{
    partial class TelexApplySearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelexApplySearchPart));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup3 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.dmdApplyDate = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupBaseInfo = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.chkcmbCompany = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.nudTotalRecords = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtApplicant = new DevExpress.XtraEditors.TextEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.txtConsigneeName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer3.SuspendLayout();
            this.navBarGroupBaseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalRecords.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtApplicant.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 448);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(222, 56);
            this.panel1.TabIndex = 4;
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
            this.navBarControl1.Size = new System.Drawing.Size(222, 442);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup3
            // 
            this.navBarGroup3.Caption = "申请时间";
            this.navBarGroup3.ControlContainer = this.navBarGroupControlContainer3;
            this.navBarGroup3.Expanded = true;
            this.navBarGroup3.GroupClientHeight = 151;
            this.navBarGroup3.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup3.Name = "navBarGroup3";
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.dmdApplyDate);
            this.navBarGroupControlContainer3.Controls.Add(this.labTo);
            this.navBarGroupControlContainer3.Controls.Add(this.labFrom);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(214, 149);
            this.navBarGroupControlContainer3.TabIndex = 0;
            // 
            // dmdApplyDate
            // 
            this.dmdApplyDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dmdApplyDate.IsEngish = true;
            this.dmdApplyDate.Location = new System.Drawing.Point(71, 3);
            this.dmdApplyDate.Name = "dmdApplyDate";
            this.dmdApplyDate.Size = new System.Drawing.Size(138, 142);
            this.dmdApplyDate.TabIndex = 0;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(7, 123);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(48, 14);
            this.labTo.TabIndex = 37;
            this.labTo.Text = "结束日期";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(7, 95);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(48, 14);
            this.labFrom.TabIndex = 36;
            this.labFrom.Text = "开始日期";
            // 
            // navBarGroupBaseInfo
            // 
            this.navBarGroupBaseInfo.Controls.Add(this.chkcmbCompany);
            this.navBarGroupBaseInfo.Controls.Add(this.labelControl1);
            this.navBarGroupBaseInfo.Controls.Add(this.nudTotalRecords);
            this.navBarGroupBaseInfo.Controls.Add(this.labelControl10);
            this.navBarGroupBaseInfo.Controls.Add(this.txtApplicant);
            this.navBarGroupBaseInfo.Controls.Add(this.labelControl11);
            this.navBarGroupBaseInfo.Controls.Add(this.txtConsigneeName);
            this.navBarGroupBaseInfo.Controls.Add(this.labelControl12);
            this.navBarGroupBaseInfo.Controls.Add(this.txtCustomerName);
            this.navBarGroupBaseInfo.Controls.Add(this.labelControl2);
            this.navBarGroupBaseInfo.Name = "navBarGroupBaseInfo";
            this.navBarGroupBaseInfo.Size = new System.Drawing.Size(214, 147);
            this.navBarGroupBaseInfo.TabIndex = 3;
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.Location = new System.Drawing.Point(70, 8);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkcmbCompany.Size = new System.Drawing.Size(134, 21);
            this.chkcmbCompany.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 10);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 35;
            this.labelControl1.Text = "所属公司";
            // 
            // nudTotalRecords
            // 
            this.nudTotalRecords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nudTotalRecords.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudTotalRecords.Location = new System.Drawing.Point(70, 118);
            this.nudTotalRecords.Name = "nudTotalRecords";
            this.nudTotalRecords.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.nudTotalRecords.Size = new System.Drawing.Size(134, 21);
            this.nudTotalRecords.TabIndex = 4;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(6, 121);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(36, 14);
            this.labelControl10.TabIndex = 33;
            this.labelControl10.Text = "记录数";
            // 
            // txtApplicant
            // 
            this.txtApplicant.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApplicant.Location = new System.Drawing.Point(70, 92);
            this.txtApplicant.Name = "txtApplicant";
            this.txtApplicant.Size = new System.Drawing.Size(134, 21);
            this.txtApplicant.TabIndex = 3;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(6, 95);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(36, 14);
            this.labelControl11.TabIndex = 31;
            this.labelControl11.Text = "申请人";
            // 
            // txtConsigneeName
            // 
            this.txtConsigneeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConsigneeName.Location = new System.Drawing.Point(70, 64);
            this.txtConsigneeName.Name = "txtConsigneeName";
            this.txtConsigneeName.Size = new System.Drawing.Size(134, 21);
            this.txtConsigneeName.TabIndex = 2;
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(6, 67);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(36, 14);
            this.labelControl12.TabIndex = 29;
            this.labelControl12.Text = "收货人";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomerName.Location = new System.Drawing.Point(70, 37);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(134, 21);
            this.txtCustomerName.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(6, 40);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 25;
            this.labelControl2.Text = "客户";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "基本条件";
            this.navBarGroup1.ControlContainer = this.navBarGroupBaseInfo;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 149;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // TelexApplySearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.navBarControl1);
            this.Controls.Add(this.panel1);
            this.Name = "TelexApplySearchPart";
            this.Size = new System.Drawing.Size(222, 504);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer3.ResumeLayout(false);
            this.navBarGroupControlContainer3.PerformLayout();
            this.navBarGroupBaseInfo.ResumeLayout(false);
            this.navBarGroupBaseInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalRecords.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtApplicant.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup3;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBaseInfo;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraEditors.TextEdit txtApplicant;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.TextEdit txtConsigneeName;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.TextEdit txtCustomerName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SpinEdit nudTotalRecords;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        protected DevExpress.XtraEditors.LabelControl labTo;
        protected DevExpress.XtraEditors.LabelControl labFrom;
        private ICP.Framework.ClientComponents.Controls.DateMonthControl dmdApplyDate;
        protected DevExpress.XtraEditors.CheckedComboBoxEdit chkcmbCompany;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
