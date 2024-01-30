namespace ICP.FAM.UI
{
    partial class GLCodeSearhcPart
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
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.barSearch = new DevExpress.XtraEditors.SimpleButton();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.chbIsValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.cmbGlCodeType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labValid = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cbtnIsCustomerCheck = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.lblIsCustomerCheck = new DevExpress.XtraEditors.LabelControl();
            this.cbtnIsPersonalCheck = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.lblIsPersonalCheck = new DevExpress.XtraEditors.LabelControl();
            this.cbtnIsDepartmentCheck = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.lblIsDepartmentCheck = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cbtnIsFee = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.lblIsFee = new DevExpress.XtraEditors.LabelControl();
            this.cbtnIsBankAccount = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.lblIsBankAccount = new DevExpress.XtraEditors.LabelControl();
            this.cbtnIsJournal = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.lblIsJournal = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup3 = new DevExpress.XtraNavBar.NavBarGroup();
            this.chkcmbCompany = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGlCodeType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            this.navBarGroupControlContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbCompany.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(10, 11);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "清空(&L)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.barSearch);
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 471);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(217, 41);
            this.panelControl1.TabIndex = 4;
            // 
            // barSearch
            // 
            this.barSearch.Location = new System.Drawing.Point(101, 11);
            this.barSearch.Name = "barSearch";
            this.barSearch.Size = new System.Drawing.Size(75, 23);
            this.barSearch.TabIndex = 1;
            this.barSearch.Text = "查询(&S)";
            this.barSearch.Click += new System.EventHandler(this.barSearch_Click);
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer3);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.navBarGroup2,
            this.navBarGroup3});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 217;
            this.navBarControl1.Size = new System.Drawing.Size(217, 471);
            this.navBarControl1.TabIndex = 8;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "基本信息";
            this.navBarGroup1.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 145;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.chkcmbCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.labelControl1);
            this.navBarGroupControlContainer1.Controls.Add(this.chbIsValid);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbGlCodeType);
            this.navBarGroupControlContainer1.Controls.Add(this.txtName);
            this.navBarGroupControlContainer1.Controls.Add(this.labValid);
            this.navBarGroupControlContainer1.Controls.Add(this.txtCode);
            this.navBarGroupControlContainer1.Controls.Add(this.labType);
            this.navBarGroupControlContainer1.Controls.Add(this.labName);
            this.navBarGroupControlContainer1.Controls.Add(this.labCode);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(213, 143);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // chbIsValid
            // 
            this.chbIsValid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chbIsValid.Checked = true;
            this.chbIsValid.CheckedText = "TRUE";
            this.chbIsValid.Location = new System.Drawing.Point(59, 114);
            this.chbIsValid.Name = "chbIsValid";
            this.chbIsValid.NULLText = "ALL";
            this.chbIsValid.Size = new System.Drawing.Size(150, 22);
            this.chbIsValid.TabIndex = 4;
            this.chbIsValid.UnCheckedText = "FALSE";
            // 
            // cmbGlCodeType
            // 
            this.cmbGlCodeType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGlCodeType.Location = new System.Drawing.Point(59, 87);
            this.cmbGlCodeType.Name = "cmbGlCodeType";
            this.cmbGlCodeType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGlCodeType.Size = new System.Drawing.Size(150, 21);
            this.cmbGlCodeType.TabIndex = 3;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(59, 60);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 21);
            this.txtName.TabIndex = 2;
            // 
            // labValid
            // 
            this.labValid.Location = new System.Drawing.Point(5, 118);
            this.labValid.Name = "labValid";
            this.labValid.Size = new System.Drawing.Size(36, 14);
            this.labValid.TabIndex = 10;
            this.labValid.Text = "有效性";
            // 
            // txtCode
            // 
            this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCode.Location = new System.Drawing.Point(59, 33);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(150, 21);
            this.txtCode.TabIndex = 1;
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(5, 90);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(24, 14);
            this.labType.TabIndex = 7;
            this.labType.Text = "类型";
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(5, 63);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(24, 14);
            this.labName.TabIndex = 8;
            this.labName.Text = "名称";
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(5, 36);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(24, 14);
            this.labCode.TabIndex = 9;
            this.labCode.Text = "编码";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.cbtnIsCustomerCheck);
            this.navBarGroupControlContainer2.Controls.Add(this.lblIsCustomerCheck);
            this.navBarGroupControlContainer2.Controls.Add(this.cbtnIsPersonalCheck);
            this.navBarGroupControlContainer2.Controls.Add(this.lblIsPersonalCheck);
            this.navBarGroupControlContainer2.Controls.Add(this.cbtnIsDepartmentCheck);
            this.navBarGroupControlContainer2.Controls.Add(this.lblIsDepartmentCheck);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(213, 104);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // cbtnIsCustomerCheck
            // 
            this.cbtnIsCustomerCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbtnIsCustomerCheck.Checked = null;
            this.cbtnIsCustomerCheck.CheckedText = "TRUE";
            this.cbtnIsCustomerCheck.Location = new System.Drawing.Point(59, 73);
            this.cbtnIsCustomerCheck.Name = "cbtnIsCustomerCheck";
            this.cbtnIsCustomerCheck.NULLText = "ALL";
            this.cbtnIsCustomerCheck.Size = new System.Drawing.Size(150, 22);
            this.cbtnIsCustomerCheck.TabIndex = 2;
            this.cbtnIsCustomerCheck.UnCheckedText = "FALSE";
            // 
            // lblIsCustomerCheck
            // 
            this.lblIsCustomerCheck.Location = new System.Drawing.Point(5, 69);
            this.lblIsCustomerCheck.Name = "lblIsCustomerCheck";
            this.lblIsCustomerCheck.Size = new System.Drawing.Size(48, 14);
            this.lblIsCustomerCheck.TabIndex = 16;
            this.lblIsCustomerCheck.Text = "客户往来";
            // 
            // cbtnIsPersonalCheck
            // 
            this.cbtnIsPersonalCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbtnIsPersonalCheck.Checked = null;
            this.cbtnIsPersonalCheck.CheckedText = "TRUE";
            this.cbtnIsPersonalCheck.Location = new System.Drawing.Point(59, 40);
            this.cbtnIsPersonalCheck.Name = "cbtnIsPersonalCheck";
            this.cbtnIsPersonalCheck.NULLText = "ALL";
            this.cbtnIsPersonalCheck.Size = new System.Drawing.Size(150, 22);
            this.cbtnIsPersonalCheck.TabIndex = 1;
            this.cbtnIsPersonalCheck.UnCheckedText = "FALSE";
            // 
            // lblIsPersonalCheck
            // 
            this.lblIsPersonalCheck.Location = new System.Drawing.Point(5, 41);
            this.lblIsPersonalCheck.Name = "lblIsPersonalCheck";
            this.lblIsPersonalCheck.Size = new System.Drawing.Size(48, 14);
            this.lblIsPersonalCheck.TabIndex = 14;
            this.lblIsPersonalCheck.Text = "个人往来";
            // 
            // cbtnIsDepartmentCheck
            // 
            this.cbtnIsDepartmentCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbtnIsDepartmentCheck.Checked = null;
            this.cbtnIsDepartmentCheck.CheckedText = "TRUE";
            this.cbtnIsDepartmentCheck.Location = new System.Drawing.Point(59, 8);
            this.cbtnIsDepartmentCheck.Name = "cbtnIsDepartmentCheck";
            this.cbtnIsDepartmentCheck.NULLText = "ALL";
            this.cbtnIsDepartmentCheck.Size = new System.Drawing.Size(150, 22);
            this.cbtnIsDepartmentCheck.TabIndex = 0;
            this.cbtnIsDepartmentCheck.UnCheckedText = "FALSE";
            // 
            // lblIsDepartmentCheck
            // 
            this.lblIsDepartmentCheck.Location = new System.Drawing.Point(5, 13);
            this.lblIsDepartmentCheck.Name = "lblIsDepartmentCheck";
            this.lblIsDepartmentCheck.Size = new System.Drawing.Size(48, 14);
            this.lblIsDepartmentCheck.TabIndex = 12;
            this.lblIsDepartmentCheck.Text = "部门核算";
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.cbtnIsFee);
            this.navBarGroupControlContainer3.Controls.Add(this.lblIsFee);
            this.navBarGroupControlContainer3.Controls.Add(this.cbtnIsBankAccount);
            this.navBarGroupControlContainer3.Controls.Add(this.lblIsBankAccount);
            this.navBarGroupControlContainer3.Controls.Add(this.cbtnIsJournal);
            this.navBarGroupControlContainer3.Controls.Add(this.lblIsJournal);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(213, 104);
            this.navBarGroupControlContainer3.TabIndex = 2;
            // 
            // cbtnIsFee
            // 
            this.cbtnIsFee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbtnIsFee.Checked = null;
            this.cbtnIsFee.CheckedText = "TRUE";
            this.cbtnIsFee.Location = new System.Drawing.Point(59, 73);
            this.cbtnIsFee.Name = "cbtnIsFee";
            this.cbtnIsFee.NULLText = "ALL";
            this.cbtnIsFee.Size = new System.Drawing.Size(150, 22);
            this.cbtnIsFee.TabIndex = 2;
            this.cbtnIsFee.UnCheckedText = "FALSE";
            // 
            // lblIsFee
            // 
            this.lblIsFee.Location = new System.Drawing.Point(5, 69);
            this.lblIsFee.Name = "lblIsFee";
            this.lblIsFee.Size = new System.Drawing.Size(48, 28);
            this.lblIsFee.TabIndex = 16;
            this.lblIsFee.Text = "流程报销\r\n费用";
            // 
            // cbtnIsBankAccount
            // 
            this.cbtnIsBankAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbtnIsBankAccount.Checked = null;
            this.cbtnIsBankAccount.CheckedText = "TRUE";
            this.cbtnIsBankAccount.Location = new System.Drawing.Point(59, 40);
            this.cbtnIsBankAccount.Name = "cbtnIsBankAccount";
            this.cbtnIsBankAccount.NULLText = "ALL";
            this.cbtnIsBankAccount.Size = new System.Drawing.Size(150, 22);
            this.cbtnIsBankAccount.TabIndex = 1;
            this.cbtnIsBankAccount.UnCheckedText = "FALSE";
            // 
            // lblIsBankAccount
            // 
            this.lblIsBankAccount.Location = new System.Drawing.Point(5, 41);
            this.lblIsBankAccount.Name = "lblIsBankAccount";
            this.lblIsBankAccount.Size = new System.Drawing.Size(36, 14);
            this.lblIsBankAccount.TabIndex = 14;
            this.lblIsBankAccount.Text = "银行帐";
            // 
            // cbtnIsJournal
            // 
            this.cbtnIsJournal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbtnIsJournal.Checked = null;
            this.cbtnIsJournal.CheckedText = "TRUE";
            this.cbtnIsJournal.Location = new System.Drawing.Point(59, 8);
            this.cbtnIsJournal.Name = "cbtnIsJournal";
            this.cbtnIsJournal.NULLText = "ALL";
            this.cbtnIsJournal.Size = new System.Drawing.Size(150, 22);
            this.cbtnIsJournal.TabIndex = 0;
            this.cbtnIsJournal.UnCheckedText = "FALSE";
            // 
            // lblIsJournal
            // 
            this.lblIsJournal.Location = new System.Drawing.Point(5, 13);
            this.lblIsJournal.Name = "lblIsJournal";
            this.lblIsJournal.Size = new System.Drawing.Size(36, 14);
            this.lblIsJournal.TabIndex = 12;
            this.lblIsJournal.Text = "日记账";
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Caption = "辅助核算";
            this.navBarGroup2.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarGroup2.Expanded = true;
            this.navBarGroup2.GroupClientHeight = 106;
            this.navBarGroup2.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup2.Name = "navBarGroup2";
            // 
            // navBarGroup3
            // 
            this.navBarGroup3.Caption = "账簿类型";
            this.navBarGroup3.ControlContainer = this.navBarGroupControlContainer3;
            this.navBarGroup3.Expanded = true;
            this.navBarGroup3.GroupClientHeight = 106;
            this.navBarGroup3.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup3.Name = "navBarGroup3";
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.Location = new System.Drawing.Point(61, 5);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkcmbCompany.Size = new System.Drawing.Size(148, 21);
            this.chkcmbCompany.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "公司";
            // 
            // GLCodeSearhcPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.navBarControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "GLCodeSearhcPart";
            this.Size = new System.Drawing.Size(217, 512);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGlCodeType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            this.navBarGroupControlContainer3.ResumeLayout(false);
            this.navBarGroupControlContainer3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbCompany.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton barSearch;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton chbIsValid;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbGlCodeType;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labValid;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup3;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton cbtnIsCustomerCheck;
        private DevExpress.XtraEditors.LabelControl lblIsCustomerCheck;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton cbtnIsPersonalCheck;
        private DevExpress.XtraEditors.LabelControl lblIsPersonalCheck;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton cbtnIsDepartmentCheck;
        private DevExpress.XtraEditors.LabelControl lblIsDepartmentCheck;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton cbtnIsFee;
        private DevExpress.XtraEditors.LabelControl lblIsFee;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton cbtnIsBankAccount;
        private DevExpress.XtraEditors.LabelControl lblIsBankAccount;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton cbtnIsJournal;
        private DevExpress.XtraEditors.LabelControl lblIsJournal;
        protected DevExpress.XtraEditors.CheckedComboBoxEdit chkcmbCompany;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
