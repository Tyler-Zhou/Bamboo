namespace ICP.TMS.UI
{
    partial class TruckBookingsSearchPart
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
            this.ngBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.bgcBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cbValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.numMaxCount = new DevExpress.XtraEditors.SpinEdit();
            this.labValid = new DevExpress.XtraEditors.LabelControl();
            this.labMaxRowCount = new DevExpress.XtraEditors.LabelControl();
            this.cmbState = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtCustomerName = new DevExpress.XtraEditors.TextEdit();
            this.labState = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerRefNo = new DevExpress.XtraEditors.TextEdit();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.labCustomerName = new DevExpress.XtraEditors.LabelControl();
            this.txtMBLNo = new DevExpress.XtraEditors.TextEdit();
            this.labCustomerRefNo = new DevExpress.XtraEditors.LabelControl();
            this.txtContainerNo = new DevExpress.XtraEditors.TextEdit();
            this.labMBLNo = new DevExpress.XtraEditors.LabelControl();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.labContainerNo = new DevExpress.XtraEditors.LabelControl();
            this.chkcmbCompany = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.bgcDate = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.dateMonthControl1 = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.cmbDateType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labEndDate = new DevExpress.XtraEditors.LabelControl();
            this.labBeginDate = new DevExpress.XtraEditors.LabelControl();
            this.labDate = new DevExpress.XtraEditors.LabelControl();
            this.ngDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.bgcBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerRefNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMBLNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContainerNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbCompany.Properties)).BeginInit();
            this.bgcDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.ngBase;
            this.navBarControl1.Controls.Add(this.bgcDate);
            this.navBarControl1.Controls.Add(this.bgcBase);
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.ngBase,
            this.ngDate});
            this.navBarControl1.Location = new System.Drawing.Point(0, 3);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 250;
            this.navBarControl1.Size = new System.Drawing.Size(202, 534);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // ngBase
            // 
            this.ngBase.Caption = "Base";
            this.ngBase.ControlContainer = this.bgcBase;
            this.ngBase.Expanded = true;
            this.ngBase.GroupClientHeight = 264;
            this.ngBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.ngBase.Name = "ngBase";
            // 
            // bgcBase
            // 
            this.bgcBase.Controls.Add(this.cbValid);
            this.bgcBase.Controls.Add(this.numMaxCount);
            this.bgcBase.Controls.Add(this.labValid);
            this.bgcBase.Controls.Add(this.labMaxRowCount);
            this.bgcBase.Controls.Add(this.cmbState);
            this.bgcBase.Controls.Add(this.cmbType);
            this.bgcBase.Controls.Add(this.txtCustomerName);
            this.bgcBase.Controls.Add(this.labState);
            this.bgcBase.Controls.Add(this.txtCustomerRefNo);
            this.bgcBase.Controls.Add(this.labType);
            this.bgcBase.Controls.Add(this.labCustomerName);
            this.bgcBase.Controls.Add(this.txtMBLNo);
            this.bgcBase.Controls.Add(this.labCustomerRefNo);
            this.bgcBase.Controls.Add(this.txtContainerNo);
            this.bgcBase.Controls.Add(this.labMBLNo);
            this.bgcBase.Controls.Add(this.txtNo);
            this.bgcBase.Controls.Add(this.labContainerNo);
            this.bgcBase.Controls.Add(this.chkcmbCompany);
            this.bgcBase.Controls.Add(this.labNo);
            this.bgcBase.Controls.Add(this.labCompany);
            this.bgcBase.Name = "bgcBase";
            this.bgcBase.Size = new System.Drawing.Size(198, 262);
            this.bgcBase.TabIndex = 1;
            // 
            // cbValid
            // 
            this.cbValid.Checked = true;
            this.cbValid.CheckedText = "TRUE";
            this.cbValid.Location = new System.Drawing.Point(85, 212);
            this.cbValid.Name = "cbValid";
            this.cbValid.NULLText = "ALL";
            this.cbValid.Size = new System.Drawing.Size(103, 21);
            this.cbValid.TabIndex = 9;
            this.cbValid.UnCheckedText = "FALSE";
            // 
            // numMaxCount
            // 
            this.numMaxCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numMaxCount.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMaxCount.Location = new System.Drawing.Point(86, 238);
            this.numMaxCount.Name = "numMaxCount";
            this.numMaxCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMaxCount.Size = new System.Drawing.Size(102, 21);
            this.numMaxCount.TabIndex = 8;
            // 
            // labValid
            // 
            this.labValid.Location = new System.Drawing.Point(3, 215);
            this.labValid.Name = "labValid";
            this.labValid.Size = new System.Drawing.Size(36, 14);
            this.labValid.TabIndex = 7;
            this.labValid.Text = "用效性";
            // 
            // labMaxRowCount
            // 
            this.labMaxRowCount.Location = new System.Drawing.Point(5, 241);
            this.labMaxRowCount.Name = "labMaxRowCount";
            this.labMaxRowCount.Size = new System.Drawing.Size(48, 14);
            this.labMaxRowCount.TabIndex = 7;
            this.labMaxRowCount.Text = "最大行数";
            // 
            // cmbState
            // 
            this.cmbState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbState.Location = new System.Drawing.Point(86, 187);
            this.cmbState.Name = "cmbState";
            this.cmbState.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbState.Properties.Appearance.Options.UseBackColor = true;
            this.cmbState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbState.Size = new System.Drawing.Size(102, 21);
            this.cmbState.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbState.TabIndex = 7;
            // 
            // cmbType
            // 
            this.cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType.Location = new System.Drawing.Point(86, 163);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Size = new System.Drawing.Size(102, 21);
            this.cmbType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbType.TabIndex = 6;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomerName.Location = new System.Drawing.Point(86, 136);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(102, 21);
            this.txtCustomerName.TabIndex = 5;
            // 
            // labState
            // 
            this.labState.Location = new System.Drawing.Point(5, 190);
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(24, 14);
            this.labState.TabIndex = 0;
            this.labState.Text = "状态";
            // 
            // txtCustomerRefNo
            // 
            this.txtCustomerRefNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomerRefNo.Location = new System.Drawing.Point(86, 109);
            this.txtCustomerRefNo.Name = "txtCustomerRefNo";
            this.txtCustomerRefNo.Size = new System.Drawing.Size(102, 21);
            this.txtCustomerRefNo.TabIndex = 4;
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(5, 166);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(29, 14);
            this.labType.TabIndex = 0;
            this.labType.Text = "进/出";
            // 
            // labCustomerName
            // 
            this.labCustomerName.Location = new System.Drawing.Point(5, 139);
            this.labCustomerName.Name = "labCustomerName";
            this.labCustomerName.Size = new System.Drawing.Size(48, 14);
            this.labCustomerName.TabIndex = 0;
            this.labCustomerName.Text = "客户名称";
            // 
            // txtMBLNo
            // 
            this.txtMBLNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMBLNo.Location = new System.Drawing.Point(86, 83);
            this.txtMBLNo.Name = "txtMBLNo";
            this.txtMBLNo.Size = new System.Drawing.Size(102, 21);
            this.txtMBLNo.TabIndex = 3;
            // 
            // labCustomerRefNo
            // 
            this.labCustomerRefNo.Location = new System.Drawing.Point(5, 112);
            this.labCustomerRefNo.Name = "labCustomerRefNo";
            this.labCustomerRefNo.Size = new System.Drawing.Size(60, 14);
            this.labCustomerRefNo.TabIndex = 0;
            this.labCustomerRefNo.Text = "客户参考号";
            // 
            // txtContainerNo
            // 
            this.txtContainerNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContainerNo.Location = new System.Drawing.Point(86, 57);
            this.txtContainerNo.Name = "txtContainerNo";
            this.txtContainerNo.Size = new System.Drawing.Size(102, 21);
            this.txtContainerNo.TabIndex = 2;
            // 
            // labMBLNo
            // 
            this.labMBLNo.Location = new System.Drawing.Point(5, 86);
            this.labMBLNo.Name = "labMBLNo";
            this.labMBLNo.Size = new System.Drawing.Size(60, 14);
            this.labMBLNo.TabIndex = 0;
            this.labMBLNo.Text = "船东提单号";
            // 
            // txtNo
            // 
            this.txtNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNo.Location = new System.Drawing.Point(86, 32);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(102, 21);
            this.txtNo.TabIndex = 1;
            // 
            // labContainerNo
            // 
            this.labContainerNo.Location = new System.Drawing.Point(5, 60);
            this.labContainerNo.Name = "labContainerNo";
            this.labContainerNo.Size = new System.Drawing.Size(24, 14);
            this.labContainerNo.TabIndex = 0;
            this.labContainerNo.Text = "箱号";
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.Location = new System.Drawing.Point(86, 6);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkcmbCompany.Size = new System.Drawing.Size(102, 21);
            this.chkcmbCompany.TabIndex = 0;
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(5, 35);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(36, 14);
            this.labNo.TabIndex = 0;
            this.labNo.Text = "业务号";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(5, 9);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(24, 14);
            this.labCompany.TabIndex = 0;
            this.labCompany.Text = "公司";
            // 
            // bgcDate
            // 
            this.bgcDate.Controls.Add(this.dateMonthControl1);
            this.bgcDate.Controls.Add(this.cmbDateType);
            this.bgcDate.Controls.Add(this.labEndDate);
            this.bgcDate.Controls.Add(this.labBeginDate);
            this.bgcDate.Controls.Add(this.labDate);
            this.bgcDate.Name = "bgcDate";
            this.bgcDate.Size = new System.Drawing.Size(198, 181);
            this.bgcDate.TabIndex = 0;
            // 
            // dateMonthControl1
            // 
            this.dateMonthControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateMonthControl1.From = null;
            this.dateMonthControl1.IsEngish = true;
            this.dateMonthControl1.Location = new System.Drawing.Point(85, 28);
            this.dateMonthControl1.Name = "dateMonthControl1";
            this.dateMonthControl1.Size = new System.Drawing.Size(103, 137);
            this.dateMonthControl1.TabIndex = 1;
            this.dateMonthControl1.To = null;
            // 
            // cmbDateType
            // 
            this.cmbDateType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDateType.Location = new System.Drawing.Point(86, 3);
            this.cmbDateType.Name = "cmbDateType";
            this.cmbDateType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbDateType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbDateType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDateType.Size = new System.Drawing.Size(102, 21);
            this.cmbDateType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbDateType.TabIndex = 0;
            // 
            // labEndDate
            // 
            this.labEndDate.Location = new System.Drawing.Point(5, 142);
            this.labEndDate.Name = "labEndDate";
            this.labEndDate.Size = new System.Drawing.Size(12, 14);
            this.labEndDate.TabIndex = 0;
            this.labEndDate.Text = "到";
            // 
            // labBeginDate
            // 
            this.labBeginDate.Location = new System.Drawing.Point(5, 113);
            this.labBeginDate.Name = "labBeginDate";
            this.labBeginDate.Size = new System.Drawing.Size(12, 14);
            this.labBeginDate.TabIndex = 0;
            this.labBeginDate.Text = "从";
            // 
            // labDate
            // 
            this.labDate.Location = new System.Drawing.Point(5, 6);
            this.labDate.Name = "labDate";
            this.labDate.Size = new System.Drawing.Size(24, 14);
            this.labDate.TabIndex = 0;
            this.labDate.Text = "日期";
            // 
            // ngDate
            // 
            this.ngDate.Caption = "Date";
            this.ngDate.ControlContainer = this.bgcDate;
            this.ngDate.Expanded = true;
            this.ngDate.GroupClientHeight = 183;
            this.ngDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.ngDate.Name = "ngDate";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnSearch);
            this.pnlBottom.Controls.Add(this.btnClear);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 548);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(208, 43);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(108, 14);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(12, 14);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "清空(&L)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.navBarControl1);
            this.xtraScrollableControl1.Location = new System.Drawing.Point(0, 2);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(205, 540);
            this.xtraScrollableControl1.TabIndex = 2;
            // 
            // TruckBookingsSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.xtraScrollableControl1);
            this.Name = "TruckBookingsSearchPart";
            this.Size = new System.Drawing.Size(208, 591);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.bgcBase.ResumeLayout(false);
            this.bgcBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerRefNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMBLNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContainerNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbCompany.Properties)).EndInit();
            this.bgcDate.ResumeLayout(false);
            this.bgcDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.xtraScrollableControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup ngBase;
        private DevExpress.XtraNavBar.NavBarGroup ngDate;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer bgcDate;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer bgcBase;
        private DevExpress.XtraEditors.LabelControl labCompany;
        protected DevExpress.XtraEditors.CheckedComboBoxEdit chkcmbCompany;
        private DevExpress.XtraEditors.LabelControl labNo;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.TextEdit txtContainerNo;
        private DevExpress.XtraEditors.LabelControl labContainerNo;
        private DevExpress.XtraEditors.TextEdit txtCustomerRefNo;
        private DevExpress.XtraEditors.TextEdit txtMBLNo;
        private DevExpress.XtraEditors.LabelControl labCustomerRefNo;
        private DevExpress.XtraEditors.LabelControl labMBLNo;
        private DevExpress.XtraEditors.TextEdit txtCustomerName;
        private DevExpress.XtraEditors.LabelControl labCustomerName;
        private DevExpress.XtraEditors.LabelControl labType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbDateType;
        private DevExpress.XtraEditors.LabelControl labDate;
        private ICP.Framework.ClientComponents.Controls.DateMonthControl dateMonthControl1;
        private DevExpress.XtraEditors.LabelControl labEndDate;
        private DevExpress.XtraEditors.LabelControl labBeginDate;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbState;
        private DevExpress.XtraEditors.LabelControl labState;
        private DevExpress.XtraEditors.SpinEdit numMaxCount;
        private DevExpress.XtraEditors.LabelControl labMaxRowCount;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton cbValid;
        private DevExpress.XtraEditors.LabelControl labValid;
    }
}
