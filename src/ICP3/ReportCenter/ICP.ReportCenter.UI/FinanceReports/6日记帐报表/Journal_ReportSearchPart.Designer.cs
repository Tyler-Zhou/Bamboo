namespace ICP.ReportCenter.UI.FinanceReports
{
   partial class Journal_ReportSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Journal_ReportSearchPart));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBaseInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.seMaxAmount = new DevExpress.XtraEditors.SpinEdit();
            this.seMinAmount = new DevExpress.XtraEditors.SpinEdit();
            this.dteFrom = new DevExpress.XtraEditors.DateEdit();
            this.dteTo = new DevExpress.XtraEditors.DateEdit();
            this.chkAmount = new DevExpress.XtraEditors.CheckEdit();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labMaxAmount = new DevExpress.XtraEditors.LabelControl();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.chkCreateDate = new DevExpress.XtraEditors.CheckEdit();
            this.labMinAmount = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.chkcmbCompany = new ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seMaxAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seMinAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCreateDate.Properties)).BeginInit();
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
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBaseInfo});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(240, 241);
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarBaseInfo
            // 
            this.nbarBaseInfo.Caption = "基本信息";
            this.nbarBaseInfo.ControlContainer = this.navBarGroupBase;
            this.nbarBaseInfo.Expanded = true;
            this.nbarBaseInfo.GroupClientHeight = 196;
            this.nbarBaseInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBaseInfo.Name = "nbarBaseInfo";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.chkcmbCompany);
            this.navBarGroupBase.Controls.Add(this.seMaxAmount);
            this.navBarGroupBase.Controls.Add(this.seMinAmount);
            this.navBarGroupBase.Controls.Add(this.dteFrom);
            this.navBarGroupBase.Controls.Add(this.dteTo);
            this.navBarGroupBase.Controls.Add(this.chkAmount);
            this.navBarGroupBase.Controls.Add(this.labCompany);
            this.navBarGroupBase.Controls.Add(this.labMaxAmount);
            this.navBarGroupBase.Controls.Add(this.labTo);
            this.navBarGroupBase.Controls.Add(this.chkCreateDate);
            this.navBarGroupBase.Controls.Add(this.labMinAmount);
            this.navBarGroupBase.Controls.Add(this.labFrom);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(232, 194);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // seMaxAmount
            // 
            this.seMaxAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seMaxAmount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seMaxAmount.Enabled = false;
            this.seMaxAmount.Location = new System.Drawing.Point(66, 158);
            this.seMaxAmount.Name = "seMaxAmount";
            this.seMaxAmount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seMaxAmount.Properties.DisplayFormat.FormatString = "F3";
            this.seMaxAmount.Properties.Mask.EditMask = "F3";
            this.seMaxAmount.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.seMaxAmount.Size = new System.Drawing.Size(163, 21);
            this.seMaxAmount.TabIndex = 30;
            // 
            // seMinAmount
            // 
            this.seMinAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seMinAmount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seMinAmount.Enabled = false;
            this.seMinAmount.Location = new System.Drawing.Point(66, 131);
            this.seMinAmount.Name = "seMinAmount";
            this.seMinAmount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seMinAmount.Properties.DisplayFormat.FormatString = "F3";
            this.seMinAmount.Properties.Mask.EditMask = "F3";
            this.seMinAmount.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.seMinAmount.Size = new System.Drawing.Size(163, 21);
            this.seMinAmount.TabIndex = 30;
            // 
            // dteFrom
            // 
            this.dteFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dteFrom.EditValue = new System.DateTime(2011, 12, 22, 0, 0, 0, 0);
            this.dteFrom.Enabled = false;
            this.dteFrom.Location = new System.Drawing.Point(66, 55);
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
            this.dteTo.Enabled = false;
            this.dteTo.Location = new System.Drawing.Point(66, 82);
            this.dteTo.Name = "dteTo";
            this.dteTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteTo.Size = new System.Drawing.Size(163, 21);
            this.dteTo.TabIndex = 1;
            // 
            // chkAmount
            // 
            this.chkAmount.Location = new System.Drawing.Point(14, 109);
            this.chkAmount.Name = "chkAmount";
            this.chkAmount.Properties.Caption = "金额";
            this.chkAmount.Size = new System.Drawing.Size(89, 19);
            this.chkAmount.TabIndex = 3;
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(9, 6);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(24, 14);
            this.labCompany.TabIndex = 29;
            this.labCompany.Text = "公司";
            // 
            // labMaxAmount
            // 
            this.labMaxAmount.Location = new System.Drawing.Point(9, 161);
            this.labMaxAmount.Name = "labMaxAmount";
            this.labMaxAmount.Size = new System.Drawing.Size(48, 14);
            this.labMaxAmount.TabIndex = 26;
            this.labMaxAmount.Text = "最大金额";
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(9, 85);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(12, 14);
            this.labTo.TabIndex = 26;
            this.labTo.Text = "到";
            // 
            // chkCreateDate
            // 
            this.chkCreateDate.Location = new System.Drawing.Point(14, 30);
            this.chkCreateDate.Name = "chkCreateDate";
            this.chkCreateDate.Properties.Caption = "创建日期";
            this.chkCreateDate.Size = new System.Drawing.Size(84, 19);
            this.chkCreateDate.TabIndex = 0;
            // 
            // labMinAmount
            // 
            this.labMinAmount.Location = new System.Drawing.Point(9, 134);
            this.labMinAmount.Name = "labMinAmount";
            this.labMinAmount.Size = new System.Drawing.Size(48, 14);
            this.labMinAmount.TabIndex = 27;
            this.labMinAmount.Text = "最小金额";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(9, 58);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(12, 14);
            this.labFrom.TabIndex = 27;
            this.labFrom.Text = "从";
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.EditText = "";
            this.chkcmbCompany.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("chkcmbCompany.EditValue")));
            this.chkcmbCompany.Location = new System.Drawing.Point(66, 3);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.ReadOnly = false;
            this.chkcmbCompany.ShowDepartment = false;
            this.chkcmbCompany.Size = new System.Drawing.Size(163, 21);
            this.chkcmbCompany.SplitString = ",";
            this.chkcmbCompany.TabIndex = 31;
            // 
            // Journal_ReportSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Journal_ReportSearchPart";
            this.Size = new System.Drawing.Size(240, 639);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seMaxAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seMinAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCreateDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.XtraScrollableControl panel2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBaseInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraEditors.CheckEdit chkCreateDate;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraEditors.DateEdit dteFrom;
        private DevExpress.XtraEditors.DateEdit dteTo;
        private DevExpress.XtraEditors.CheckEdit chkAmount;
        private DevExpress.XtraEditors.SpinEdit seMaxAmount;
        private DevExpress.XtraEditors.SpinEdit seMinAmount;
        private DevExpress.XtraEditors.LabelControl labMaxAmount;
        private DevExpress.XtraEditors.LabelControl labMinAmount;
        private Comm.Controls.CompanyTreeCheckControl chkcmbCompany;
    }
}
