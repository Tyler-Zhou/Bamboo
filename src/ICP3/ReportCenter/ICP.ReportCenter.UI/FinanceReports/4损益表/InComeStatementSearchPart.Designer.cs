namespace ICP.ReportCenter.UI.FinanceReports
{
   partial class InComeStatementSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InComeStatementSearchPart));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBaseInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.dteFrom = new DevExpress.XtraEditors.DateEdit();
            this.dteTo = new DevExpress.XtraEditors.DateEdit();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.rbCashBasis = new System.Windows.Forms.RadioButton();
            this.rgAccrualBasis = new System.Windows.Forms.RadioButton();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.rbYearly = new System.Windows.Forms.RadioButton();
            this.rbPrevious = new System.Windows.Forms.RadioButton();
            this.rbStandard = new System.Windows.Forms.RadioButton();
            this.bgAccountingType = new DevExpress.XtraNavBar.NavBarGroup();
            this.bgReportType = new DevExpress.XtraNavBar.NavBarGroup();
            this.chkcmbCompany = new ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).BeginInit();
            this.navBarGroupControlContainer1.SuspendLayout();
            this.navBarGroupControlContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 580);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 59);
            this.panel1.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(78, 16);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&R)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.navBarControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 580);
            this.panel2.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBaseInfo;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBaseInfo,
            this.bgAccountingType,
            this.bgReportType});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(240, 422);
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarBaseInfo
            // 
            this.nbarBaseInfo.Caption = "基本信息";
            this.nbarBaseInfo.ControlContainer = this.navBarGroupBase;
            this.nbarBaseInfo.Expanded = true;
            this.nbarBaseInfo.GroupClientHeight = 89;
            this.nbarBaseInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBaseInfo.Name = "nbarBaseInfo";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.chkcmbCompany);
            this.navBarGroupBase.Controls.Add(this.dteFrom);
            this.navBarGroupBase.Controls.Add(this.dteTo);
            this.navBarGroupBase.Controls.Add(this.labCompany);
            this.navBarGroupBase.Controls.Add(this.labTo);
            this.navBarGroupBase.Controls.Add(this.labFrom);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(232, 87);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // dteFrom
            // 
            this.dteFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dteFrom.EditValue = new System.DateTime(2011, 12, 22, 0, 0, 0, 0);
            this.dteFrom.Location = new System.Drawing.Point(66, 3);
            this.dteFrom.Name = "dteFrom";
            this.dteFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFrom.Size = new System.Drawing.Size(163, 21);
            this.dteFrom.TabIndex = 0;
            // 
            // dteTo
            // 
            this.dteTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dteTo.EditValue = new System.DateTime(2011, 12, 22, 0, 0, 0, 0);
            this.dteTo.Location = new System.Drawing.Point(66, 30);
            this.dteTo.Name = "dteTo";
            this.dteTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteTo.Size = new System.Drawing.Size(163, 21);
            this.dteTo.TabIndex = 1;
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(9, 60);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(24, 14);
            this.labCompany.TabIndex = 29;
            this.labCompany.Text = "公司";
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(9, 33);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(12, 14);
            this.labTo.TabIndex = 26;
            this.labTo.Text = "到";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(9, 6);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(12, 14);
            this.labFrom.TabIndex = 27;
            this.labFrom.Text = "从";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.rbCashBasis);
            this.navBarGroupControlContainer1.Controls.Add(this.rgAccrualBasis);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(232, 63);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // rbCashBasis
            // 
            this.rbCashBasis.AutoSize = true;
            this.rbCashBasis.Location = new System.Drawing.Point(9, 37);
            this.rbCashBasis.Name = "rbCashBasis";
            this.rbCashBasis.Size = new System.Drawing.Size(79, 18);
            this.rbCashBasis.TabIndex = 0;
            this.rbCashBasis.Text = "Cash Basis";
            this.rbCashBasis.UseVisualStyleBackColor = true;
            // 
            // rgAccrualBasis
            // 
            this.rgAccrualBasis.AutoSize = true;
            this.rgAccrualBasis.Checked = true;
            this.rgAccrualBasis.Location = new System.Drawing.Point(9, 8);
            this.rgAccrualBasis.Name = "rgAccrualBasis";
            this.rgAccrualBasis.Size = new System.Drawing.Size(93, 18);
            this.rgAccrualBasis.TabIndex = 0;
            this.rgAccrualBasis.TabStop = true;
            this.rgAccrualBasis.Text = "Accrual Basis";
            this.rgAccrualBasis.UseVisualStyleBackColor = true;
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.rbYearly);
            this.navBarGroupControlContainer2.Controls.Add(this.rbPrevious);
            this.navBarGroupControlContainer2.Controls.Add(this.rbStandard);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(232, 88);
            this.navBarGroupControlContainer2.TabIndex = 2;
            // 
            // rbYearly
            // 
            this.rbYearly.AutoSize = true;
            this.rbYearly.Location = new System.Drawing.Point(9, 55);
            this.rbYearly.Name = "rbYearly";
            this.rbYearly.Size = new System.Drawing.Size(124, 18);
            this.rbYearly.TabIndex = 0;
            this.rbYearly.Text = "Yearly Comparison";
            this.rbYearly.UseVisualStyleBackColor = true;
            // 
            // rbPrevious
            // 
            this.rbPrevious.AutoSize = true;
            this.rbPrevious.Location = new System.Drawing.Point(9, 31);
            this.rbPrevious.Name = "rbPrevious";
            this.rbPrevious.Size = new System.Drawing.Size(165, 18);
            this.rbPrevious.TabIndex = 0;
            this.rbPrevious.Text = "Previous Year Comparison";
            this.rbPrevious.UseVisualStyleBackColor = true;
            // 
            // rbStandard
            // 
            this.rbStandard.AutoSize = true;
            this.rbStandard.Checked = true;
            this.rbStandard.Location = new System.Drawing.Point(9, 6);
            this.rbStandard.Name = "rbStandard";
            this.rbStandard.Size = new System.Drawing.Size(93, 18);
            this.rbStandard.TabIndex = 0;
            this.rbStandard.TabStop = true;
            this.rbStandard.Text = "Accrual Basis";
            this.rbStandard.UseVisualStyleBackColor = true;
            // 
            // bgAccountingType
            // 
            this.bgAccountingType.Caption = "Accounting Type";
            this.bgAccountingType.ControlContainer = this.navBarGroupControlContainer1;
            this.bgAccountingType.Expanded = true;
            this.bgAccountingType.GroupClientHeight = 65;
            this.bgAccountingType.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgAccountingType.Name = "bgAccountingType";
            this.bgAccountingType.Visible = false;
            // 
            // bgReportType
            // 
            this.bgReportType.Caption = "Report Type";
            this.bgReportType.ControlContainer = this.navBarGroupControlContainer2;
            this.bgReportType.Expanded = true;
            this.bgReportType.GroupClientHeight = 90;
            this.bgReportType.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgReportType.Name = "bgReportType";
            this.bgReportType.Visible = false;
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.EditText = "";
            this.chkcmbCompany.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("chkcmbCompany.EditValue")));
            this.chkcmbCompany.Location = new System.Drawing.Point(66, 57);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.ReadOnly = false;
            this.chkcmbCompany.ShowDepartment = false;
            this.chkcmbCompany.Size = new System.Drawing.Size(163, 21);
            this.chkcmbCompany.SplitString = ",";
            this.chkcmbCompany.TabIndex = 30;
            // 
            // InComeStatementSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "InComeStatementSearchPart";
            this.Size = new System.Drawing.Size(240, 639);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).EndInit();
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.XtraScrollableControl panel2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBaseInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraEditors.DateEdit dteFrom;
        private DevExpress.XtraEditors.DateEdit dteTo;
        private DevExpress.XtraNavBar.NavBarGroup bgAccountingType;
        private DevExpress.XtraNavBar.NavBarGroup bgReportType;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private System.Windows.Forms.RadioButton rgAccrualBasis;
        private System.Windows.Forms.RadioButton rbCashBasis;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private System.Windows.Forms.RadioButton rbYearly;
        private System.Windows.Forms.RadioButton rbPrevious;
        private System.Windows.Forms.RadioButton rbStandard;
        private Comm.Controls.CompanyTreeCheckControl chkcmbCompany;
    }
}
