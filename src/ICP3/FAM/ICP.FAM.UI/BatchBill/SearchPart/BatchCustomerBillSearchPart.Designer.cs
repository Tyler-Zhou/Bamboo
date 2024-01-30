namespace ICP.FAM.UI.BatchBill
{
    partial class BatchCustomerBillSearchPart
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
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelButton = new DevExpress.XtraEditors.PanelControl();
            this.navBarMain = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panelBase = new System.Windows.Forms.Panel();
            this.cmbState = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.lwchkIsValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.labIsValid = new DevExpress.XtraEditors.LabelControl();
            this.chkcmbCompany = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labState = new DevExpress.XtraEditors.LabelControl();
            this.numMax = new DevExpress.XtraEditors.SpinEdit();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labMax = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labInvoiceNo = new DevExpress.XtraEditors.LabelControl();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.stxtCustomer = new DevExpress.XtraEditors.TextEdit();
            this.navBarGroupDate = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panelDate = new System.Windows.Forms.Panel();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.fromToDateMonthControl1 = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.nbarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.panelMain = new DevExpress.XtraEditors.XtraScrollableControl();
            this.labDueDate = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelButton)).BeginInit();
            this.panelButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarMain)).BeginInit();
            this.navBarMain.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            this.panelBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).BeginInit();
            this.navBarGroupDate.SuspendLayout();
            this.panelDate.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(21, 9);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "C&lear";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(107, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "&Search";
            // 
            // panelButton
            // 
            this.panelButton.Controls.Add(this.btnClear);
            this.panelButton.Controls.Add(this.btnSearch);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButton.Location = new System.Drawing.Point(0, 602);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(229, 54);
            this.panelButton.TabIndex = 6;
            // 
            // navBarMain
            // 
            this.navBarMain.ActiveGroup = this.nbarBase;
            this.navBarMain.Controls.Add(this.navBarGroupBase);
            this.navBarMain.Controls.Add(this.navBarGroupDate);
            this.navBarMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarMain.ExplorerBarGroupInterval = 2;
            this.navBarMain.ExplorerBarGroupOuterIndent = 2;
            this.navBarMain.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase,
            this.nbarDate});
            this.navBarMain.Location = new System.Drawing.Point(0, 0);
            this.navBarMain.Name = "navBarMain";
            this.navBarMain.OptionsNavPane.ExpandedWidth = 140;
            this.navBarMain.Size = new System.Drawing.Size(229, 602);
            this.navBarMain.TabIndex = 1;
            this.navBarMain.Text = "navBarControl1";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "Base";
            this.nbarBase.ControlContainer = this.navBarGroupBase;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 252;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.panelBase);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(221, 250);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // panelBase
            // 
            this.panelBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panelBase.Controls.Add(this.cmbState);
            this.panelBase.Controls.Add(this.cmbType);
            this.panelBase.Controls.Add(this.lwchkIsValid);
            this.panelBase.Controls.Add(this.labIsValid);
            this.panelBase.Controls.Add(this.chkcmbCompany);
            this.panelBase.Controls.Add(this.labState);
            this.panelBase.Controls.Add(this.numMax);
            this.panelBase.Controls.Add(this.labType);
            this.panelBase.Controls.Add(this.labCustomer);
            this.panelBase.Controls.Add(this.labMax);
            this.panelBase.Controls.Add(this.labCompany);
            this.panelBase.Controls.Add(this.labInvoiceNo);
            this.panelBase.Controls.Add(this.txtNo);
            this.panelBase.Controls.Add(this.stxtCustomer);
            this.panelBase.Location = new System.Drawing.Point(0, 0);
            this.panelBase.Name = "panelBase";
            this.panelBase.Size = new System.Drawing.Size(220, 248);
            this.panelBase.TabIndex = 0;
            // 
            // cmbState
            // 
            this.cmbState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbState.Location = new System.Drawing.Point(91, 83);
            this.cmbState.Name = "cmbState";
            this.cmbState.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbState.Properties.Appearance.Options.UseBackColor = true;
            this.cmbState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbState.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbState.Size = new System.Drawing.Size(105, 21);
            this.cmbState.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbState.TabIndex = 18;
            // 
            // cmbType
            // 
            this.cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType.Location = new System.Drawing.Point(90, 110);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbType.Size = new System.Drawing.Size(105, 21);
            this.cmbType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbType.TabIndex = 18;
            // 
            // lwchkIsValid
            // 
            this.lwchkIsValid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lwchkIsValid.Checked = true;
            this.lwchkIsValid.CheckedText = "Valid";
            this.lwchkIsValid.Location = new System.Drawing.Point(91, 137);
            this.lwchkIsValid.Name = "lwchkIsValid";
            this.lwchkIsValid.NULLText = "ALL";
            this.lwchkIsValid.Size = new System.Drawing.Size(105, 21);
            this.lwchkIsValid.TabIndex = 19;
            this.lwchkIsValid.UnCheckedText = "UnValid";
            // 
            // labIsValid
            // 
            this.labIsValid.Location = new System.Drawing.Point(7, 140);
            this.labIsValid.Name = "labIsValid";
            this.labIsValid.Size = new System.Drawing.Size(34, 14);
            this.labIsValid.TabIndex = 40;
            this.labIsValid.Text = "IsValid";
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.Location = new System.Drawing.Point(90, 3);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkcmbCompany.Size = new System.Drawing.Size(105, 21);
            this.chkcmbCompany.TabIndex = 1;
            // 
            // labState
            // 
            this.labState.Location = new System.Drawing.Point(8, 86);
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(30, 14);
            this.labState.TabIndex = 1;
            this.labState.Text = "State";
            // 
            // numMax
            // 
            this.numMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numMax.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMax.Location = new System.Drawing.Point(91, 164);
            this.numMax.Name = "numMax";
            this.numMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMax.Properties.IsFloatValue = false;
            this.numMax.Properties.Mask.EditMask = "N00";
            this.numMax.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numMax.Size = new System.Drawing.Size(105, 21);
            this.numMax.TabIndex = 20;
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(7, 113);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(28, 14);
            this.labType.TabIndex = 1;
            this.labType.Text = "Type";
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(7, 57);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 1;
            this.labCustomer.Text = "Customer";
            // 
            // labMax
            // 
            this.labMax.Location = new System.Drawing.Point(8, 167);
            this.labMax.Name = "labMax";
            this.labMax.Size = new System.Drawing.Size(58, 14);
            this.labMax.TabIndex = 1;
            this.labMax.Text = "Max Count";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(7, 6);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 1;
            this.labCompany.Text = "Company";
            // 
            // labInvoiceNo
            // 
            this.labInvoiceNo.Location = new System.Drawing.Point(7, 30);
            this.labInvoiceNo.Name = "labInvoiceNo";
            this.labInvoiceNo.Size = new System.Drawing.Size(58, 14);
            this.labInvoiceNo.TabIndex = 1;
            this.labInvoiceNo.Text = "Invoice No";
            // 
            // txtNo
            // 
            this.txtNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNo.Location = new System.Drawing.Point(90, 27);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(105, 21);
            this.txtNo.TabIndex = 2;
            // 
            // stxtCustomer
            // 
            this.stxtCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stxtCustomer.Location = new System.Drawing.Point(90, 54);
            this.stxtCustomer.Name = "stxtCustomer";
            this.stxtCustomer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtCustomer.Properties.Appearance.Options.UseBackColor = true;
            this.stxtCustomer.Size = new System.Drawing.Size(105, 21);
            this.stxtCustomer.TabIndex = 7;
            // 
            // navBarGroupDate
            // 
            this.navBarGroupDate.Controls.Add(this.panelDate);
            this.navBarGroupDate.Name = "navBarGroupDate";
            this.navBarGroupDate.Size = new System.Drawing.Size(221, 171);
            this.navBarGroupDate.TabIndex = 1;
            // 
            // panelDate
            // 
            this.panelDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panelDate.Controls.Add(this.labTo);
            this.panelDate.Controls.Add(this.labDueDate);
            this.panelDate.Controls.Add(this.labFrom);
            this.panelDate.Controls.Add(this.fromToDateMonthControl1);
            this.panelDate.Location = new System.Drawing.Point(0, 0);
            this.panelDate.Name = "panelDate";
            this.panelDate.Size = new System.Drawing.Size(220, 171);
            this.panelDate.TabIndex = 1;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(8, 124);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 13;
            this.labTo.Text = "To";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(8, 97);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(27, 14);
            this.labFrom.TabIndex = 12;
            this.labFrom.Text = "From";
            // 
            // fromToDateMonthControl1
            // 
            this.fromToDateMonthControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fromToDateMonthControl1.From = null;
            this.fromToDateMonthControl1.IsEngish = true;
            this.fromToDateMonthControl1.Location = new System.Drawing.Point(91, 13);
            this.fromToDateMonthControl1.Name = "fromToDateMonthControl1";
            this.fromToDateMonthControl1.Size = new System.Drawing.Size(105, 133);
            this.fromToDateMonthControl1.TabIndex = 2;
            this.fromToDateMonthControl1.To = null;
            // 
            // nbarDate
            // 
            this.nbarDate.Caption = "Date";
            this.nbarDate.ControlContainer = this.navBarGroupDate;
            this.nbarDate.Expanded = true;
            this.nbarDate.GroupClientHeight = 173;
            this.nbarDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarDate.Name = "nbarDate";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.navBarMain);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(229, 602);
            this.panelMain.TabIndex = 0;
            // 
            // labDueDate
            // 
            this.labDueDate.Location = new System.Drawing.Point(8, 23);
            this.labDueDate.Name = "labDueDate";
            this.labDueDate.Size = new System.Drawing.Size(52, 14);
            this.labDueDate.TabIndex = 12;
            this.labDueDate.Text = "Due Date";
            // 
            // BatchCustomerBillSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButton);
            this.Name = "BatchCustomerBillSearchPart";
            this.Size = new System.Drawing.Size(229, 656);
            ((System.ComponentModel.ISupportInitialize)(this.panelButton)).EndInit();
            this.panelButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarMain)).EndInit();
            this.navBarMain.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.panelBase.ResumeLayout(false);
            this.panelBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).EndInit();
            this.navBarGroupDate.ResumeLayout(false);
            this.panelDate.ResumeLayout(false);
            this.panelDate.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraEditors.SimpleButton btnClear;
        protected DevExpress.XtraEditors.SimpleButton btnSearch;
        protected DevExpress.XtraEditors.PanelControl panelButton;
        protected DevExpress.XtraNavBar.NavBarControl navBarMain;
        protected DevExpress.XtraNavBar.NavBarGroup nbarBase;
        protected DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private System.Windows.Forms.Panel panelBase;
        protected Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbType;
        private Framework.ClientComponents.Controls.LWCheckButton lwchkIsValid;
        private DevExpress.XtraEditors.LabelControl labIsValid;
        protected DevExpress.XtraEditors.CheckedComboBoxEdit chkcmbCompany;
        protected DevExpress.XtraEditors.SpinEdit numMax;
        protected DevExpress.XtraEditors.LabelControl labType;
        protected DevExpress.XtraEditors.LabelControl labCustomer;
        protected DevExpress.XtraEditors.LabelControl labMax;
        protected DevExpress.XtraEditors.LabelControl labCompany;
        protected DevExpress.XtraEditors.LabelControl labInvoiceNo;
        protected DevExpress.XtraEditors.TextEdit txtNo;
        protected DevExpress.XtraEditors.TextEdit stxtCustomer;
        protected DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupDate;
        private System.Windows.Forms.Panel panelDate;
        protected DevExpress.XtraEditors.LabelControl labTo;
        protected DevExpress.XtraEditors.LabelControl labFrom;
        protected Framework.ClientComponents.Controls.DateMonthControl fromToDateMonthControl1;
        protected DevExpress.XtraNavBar.NavBarGroup nbarDate;
        protected DevExpress.XtraEditors.XtraScrollableControl panelMain;
        protected Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbState;
        protected DevExpress.XtraEditors.LabelControl labState;
        protected DevExpress.XtraEditors.LabelControl labDueDate;
    }
}
