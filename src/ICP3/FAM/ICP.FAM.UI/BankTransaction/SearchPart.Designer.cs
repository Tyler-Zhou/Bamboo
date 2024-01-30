namespace ICP.FAM.UI.BankTransaction
{
    partial class SearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchPart));
            this.panelBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.GroupDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupDate = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.dmdDate = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupBaseInfo = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.rdoDirection = new DevExpress.XtraEditors.RadioGroup();
            this.numAmountMax = new DevExpress.XtraEditors.SpinEdit();
            this.numAmountMin = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cmbBankCode = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labBankName = new DevExpress.XtraEditors.LabelControl();
            this.cmbBankAccountID = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cmbCompanyID = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labCompanyID = new DevExpress.XtraEditors.LabelControl();
            this.labAccountName = new DevExpress.XtraEditors.LabelControl();
            this.txtBusinessNO = new DevExpress.XtraEditors.TextEdit();
            this.txtRelativeAccountName = new DevExpress.XtraEditors.TextEdit();
            this.labBusinessNO = new DevExpress.XtraEditors.LabelControl();
            this.labRelativeAccountName = new DevExpress.XtraEditors.LabelControl();
            this.nudTotalRecords = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.panelCenter = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).BeginInit();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupDate.SuspendLayout();
            this.navBarGroupBaseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoDirection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBankCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBankAccountID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompanyID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBusinessNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRelativeAccountName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalRecords.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCenter)).BeginInit();
            this.panelCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnClear);
            this.panelBottom.Controls.Add(this.btnSearch);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 470);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(222, 34);
            this.panelBottom.TabIndex = 4;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(26, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "清空(&L)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(133, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.GroupDate;
            this.navBarControl1.Controls.Add(this.navBarGroupDate);
            this.navBarControl1.Controls.Add(this.navBarGroupBaseInfo);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.ExplorerBarGroupInterval = 2;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.GroupDate});
            this.navBarControl1.Location = new System.Drawing.Point(2, 2);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(218, 466);
            this.navBarControl1.SkinExplorerBarViewScrollStyle = DevExpress.XtraNavBar.SkinExplorerBarViewScrollStyle.ScrollBar;
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // GroupDate
            // 
            this.GroupDate.Caption = "时间";
            this.GroupDate.ControlContainer = this.navBarGroupDate;
            this.GroupDate.Expanded = true;
            this.GroupDate.GroupClientHeight = 151;
            this.GroupDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.GroupDate.Name = "GroupDate";
            // 
            // navBarGroupDate
            // 
            this.navBarGroupDate.Controls.Add(this.dmdDate);
            this.navBarGroupDate.Controls.Add(this.labTo);
            this.navBarGroupDate.Controls.Add(this.labFrom);
            this.navBarGroupDate.Name = "navBarGroupDate";
            this.navBarGroupDate.Size = new System.Drawing.Size(210, 149);
            this.navBarGroupDate.TabIndex = 0;
            // 
            // dmdDate
            // 
            this.dmdDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dmdDate.From = null;
            this.dmdDate.IsEngish = true;
            this.dmdDate.Location = new System.Drawing.Point(71, 3);
            this.dmdDate.Name = "dmdDate";
            this.dmdDate.Size = new System.Drawing.Size(134, 142);
            this.dmdDate.TabIndex = 0;
            this.dmdDate.To = null;
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
            this.navBarGroupBaseInfo.Controls.Add(this.rdoDirection);
            this.navBarGroupBaseInfo.Controls.Add(this.numAmountMax);
            this.navBarGroupBaseInfo.Controls.Add(this.numAmountMin);
            this.navBarGroupBaseInfo.Controls.Add(this.labelControl4);
            this.navBarGroupBaseInfo.Controls.Add(this.labelControl3);
            this.navBarGroupBaseInfo.Controls.Add(this.cmbBankCode);
            this.navBarGroupBaseInfo.Controls.Add(this.labBankName);
            this.navBarGroupBaseInfo.Controls.Add(this.cmbBankAccountID);
            this.navBarGroupBaseInfo.Controls.Add(this.cmbCompanyID);
            this.navBarGroupBaseInfo.Controls.Add(this.labCompanyID);
            this.navBarGroupBaseInfo.Controls.Add(this.labAccountName);
            this.navBarGroupBaseInfo.Controls.Add(this.txtBusinessNO);
            this.navBarGroupBaseInfo.Controls.Add(this.txtRelativeAccountName);
            this.navBarGroupBaseInfo.Controls.Add(this.labBusinessNO);
            this.navBarGroupBaseInfo.Controls.Add(this.labRelativeAccountName);
            this.navBarGroupBaseInfo.Controls.Add(this.nudTotalRecords);
            this.navBarGroupBaseInfo.Controls.Add(this.labelControl10);
            this.navBarGroupBaseInfo.Name = "navBarGroupBaseInfo";
            this.navBarGroupBaseInfo.Size = new System.Drawing.Size(210, 251);
            this.navBarGroupBaseInfo.TabIndex = 3;
            // 
            // rdoDirection
            // 
            this.rdoDirection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoDirection.Location = new System.Drawing.Point(3, 3);
            this.rdoDirection.Name = "rdoDirection";
            this.rdoDirection.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rdoDirection.Properties.Appearance.Options.UseBackColor = true;
            this.rdoDirection.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rdoDirection.Properties.Columns = 3;
            this.rdoDirection.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("DirectionAll", "全部"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("DirectionCredit", "借"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("DirectionDebit", "贷")});
            this.rdoDirection.Size = new System.Drawing.Size(202, 26);
            this.rdoDirection.TabIndex = 48;
            // 
            // numAmountMax
            // 
            this.numAmountMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numAmountMax.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numAmountMax.Location = new System.Drawing.Point(71, 195);
            this.numAmountMax.Name = "numAmountMax";
            this.numAmountMax.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.numAmountMax.Properties.Appearance.Options.UseBackColor = true;
            this.numAmountMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numAmountMax.Properties.MaxValue = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.numAmountMax.Properties.MinValue = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            -2147483648});
            this.numAmountMax.Size = new System.Drawing.Size(131, 21);
            this.numAmountMax.TabIndex = 45;
            // 
            // numAmountMin
            // 
            this.numAmountMin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numAmountMin.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numAmountMin.Location = new System.Drawing.Point(71, 170);
            this.numAmountMin.Name = "numAmountMin";
            this.numAmountMin.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.numAmountMin.Properties.Appearance.Options.UseBackColor = true;
            this.numAmountMin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numAmountMin.Properties.MaxValue = new decimal(new int[] {
            1569325055,
            23283064,
            0,
            0});
            this.numAmountMin.Properties.MinValue = new decimal(new int[] {
            1569325055,
            23283064,
            0,
            -2147483648});
            this.numAmountMin.Size = new System.Drawing.Size(131, 21);
            this.numAmountMin.TabIndex = 44;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(7, 198);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 14);
            this.labelControl4.TabIndex = 47;
            this.labelControl4.Text = "最大";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(7, 173);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 46;
            this.labelControl3.Text = "最小";
            // 
            // cmbBankCode
            // 
            this.cmbBankCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBankCode.Location = new System.Drawing.Point(71, 35);
            this.cmbBankCode.Name = "cmbBankCode";
            this.cmbBankCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBankCode.Size = new System.Drawing.Size(130, 21);
            this.cmbBankCode.TabIndex = 42;
            // 
            // labBankName
            // 
            this.labBankName.Location = new System.Drawing.Point(7, 38);
            this.labBankName.Name = "labBankName";
            this.labBankName.Size = new System.Drawing.Size(60, 14);
            this.labBankName.TabIndex = 43;
            this.labBankName.Text = "银行开户地";
            // 
            // cmbBankAccountID
            // 
            this.cmbBankAccountID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBankAccountID.Location = new System.Drawing.Point(71, 89);
            this.cmbBankAccountID.Name = "cmbBankAccountID";
            this.cmbBankAccountID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBankAccountID.Size = new System.Drawing.Size(131, 21);
            this.cmbBankAccountID.TabIndex = 40;
            // 
            // cmbCompanyID
            // 
            this.cmbCompanyID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCompanyID.Location = new System.Drawing.Point(71, 62);
            this.cmbCompanyID.Name = "cmbCompanyID";
            this.cmbCompanyID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompanyID.Size = new System.Drawing.Size(131, 21);
            this.cmbCompanyID.TabIndex = 39;
            // 
            // labCompanyID
            // 
            this.labCompanyID.Location = new System.Drawing.Point(7, 65);
            this.labCompanyID.Name = "labCompanyID";
            this.labCompanyID.Size = new System.Drawing.Size(24, 14);
            this.labCompanyID.TabIndex = 41;
            this.labCompanyID.Text = "公司";
            // 
            // labAccountName
            // 
            this.labAccountName.Location = new System.Drawing.Point(7, 92);
            this.labAccountName.Name = "labAccountName";
            this.labAccountName.Size = new System.Drawing.Size(24, 14);
            this.labAccountName.TabIndex = 38;
            this.labAccountName.Text = "银行";
            // 
            // txtBusinessNO
            // 
            this.txtBusinessNO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBusinessNO.Location = new System.Drawing.Point(71, 116);
            this.txtBusinessNO.Name = "txtBusinessNO";
            this.txtBusinessNO.Size = new System.Drawing.Size(131, 21);
            this.txtBusinessNO.TabIndex = 0;
            // 
            // txtRelativeAccountName
            // 
            this.txtRelativeAccountName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRelativeAccountName.Location = new System.Drawing.Point(71, 143);
            this.txtRelativeAccountName.Name = "txtRelativeAccountName";
            this.txtRelativeAccountName.Size = new System.Drawing.Size(131, 21);
            this.txtRelativeAccountName.TabIndex = 3;
            // 
            // labBusinessNO
            // 
            this.labBusinessNO.Location = new System.Drawing.Point(7, 119);
            this.labBusinessNO.Name = "labBusinessNO";
            this.labBusinessNO.Size = new System.Drawing.Size(48, 14);
            this.labBusinessNO.TabIndex = 37;
            this.labBusinessNO.Text = "销账单号";
            // 
            // labRelativeAccountName
            // 
            this.labRelativeAccountName.Location = new System.Drawing.Point(7, 146);
            this.labRelativeAccountName.Name = "labRelativeAccountName";
            this.labRelativeAccountName.Size = new System.Drawing.Size(60, 14);
            this.labRelativeAccountName.TabIndex = 37;
            this.labRelativeAccountName.Text = "对方账号名";
            // 
            // nudTotalRecords
            // 
            this.nudTotalRecords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudTotalRecords.EditValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudTotalRecords.Location = new System.Drawing.Point(71, 222);
            this.nudTotalRecords.Name = "nudTotalRecords";
            this.nudTotalRecords.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.nudTotalRecords.Size = new System.Drawing.Size(130, 21);
            this.nudTotalRecords.TabIndex = 4;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(7, 225);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(36, 14);
            this.labelControl10.TabIndex = 33;
            this.labelControl10.Text = "记录数";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "基本条件";
            this.navBarGroup1.ControlContainer = this.navBarGroupBaseInfo;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 253;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.navBarControl1);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(0, 0);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(222, 470);
            this.panelCenter.TabIndex = 5;
            // 
            // SearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelBottom);
            this.Name = "SearchPart";
            this.Size = new System.Drawing.Size(222, 504);
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupDate.ResumeLayout(false);
            this.navBarGroupDate.PerformLayout();
            this.navBarGroupBaseInfo.ResumeLayout(false);
            this.navBarGroupBaseInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoDirection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBankCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBankAccountID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompanyID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBusinessNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRelativeAccountName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalRecords.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCenter)).EndInit();
            this.panelCenter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelBottom;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup GroupDate;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupDate;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBaseInfo;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraEditors.SpinEdit nudTotalRecords;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        protected DevExpress.XtraEditors.LabelControl labTo;
        protected DevExpress.XtraEditors.LabelControl labFrom;
        private ICP.Framework.ClientComponents.Controls.DateMonthControl dmdDate;
        private DevExpress.XtraEditors.PanelControl panelCenter;
        private DevExpress.XtraEditors.TextEdit txtRelativeAccountName;
        private DevExpress.XtraEditors.LabelControl labRelativeAccountName;
        private DevExpress.XtraEditors.TextEdit txtBusinessNO;
        private DevExpress.XtraEditors.LabelControl labBusinessNO;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbBankAccountID;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCompanyID;
        private DevExpress.XtraEditors.LabelControl labCompanyID;
        private DevExpress.XtraEditors.LabelControl labAccountName;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbBankCode;
        private DevExpress.XtraEditors.LabelControl labBankName;
        private DevExpress.XtraEditors.SpinEdit numAmountMax;
        private DevExpress.XtraEditors.SpinEdit numAmountMin;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.RadioGroup rdoDirection;
    }
}
