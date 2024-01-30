namespace ICP.ReportCenter.UI.FinanceOIReports
{
   partial class LocalStatementSearchPart
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
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.PanelControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.txtCustomer = new ICP.ReportCenter.UI.Comm.Controls.SingleCustomerFinderButtonEdit();
            this.labOrderBy = new DevExpress.XtraEditors.LabelControl();
            this.labPeriod = new DevExpress.XtraEditors.LabelControl();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.rdoBillDate = new System.Windows.Forms.RadioButton();
            this.rdoETD = new System.Windows.Forms.RadioButton();
            this.dtePeriodTo = new DevExpress.XtraEditors.DateEdit();
            this.chkcmbCompany = new ICP.ReportCenter.UI.Comm.Controls.CompanyCheckBoxComboBox();
            this.dtePeriodFrom = new DevExpress.XtraEditors.DateEdit();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.groupOpenOrAll = new System.Windows.Forms.GroupBox();
            this.rdoAll = new System.Windows.Forms.RadioButton();
            this.rdoOpenWhitInvoice = new System.Windows.Forms.RadioButton();
            this.rdoOpen = new System.Windows.Forms.RadioButton();
            this.chkAttached = new DevExpress.XtraEditors.CheckEdit();
            this.chkETA = new DevExpress.XtraEditors.CheckEdit();
            this.panel3 = new DevExpress.XtraEditors.PanelControl();
            this.labReportType = new DevExpress.XtraEditors.LabelControl();
            this.chkAccountsPayable = new DevExpress.XtraEditors.CheckEdit();
            this.chkLocalInvoice = new DevExpress.XtraEditors.CheckEdit();
            this.chkETD = new DevExpress.XtraEditors.CheckEdit();
            this.dteETATo = new DevExpress.XtraEditors.DateEdit();
            this.dteETDTo = new DevExpress.XtraEditors.DateEdit();
            this.dteETAFrom = new DevExpress.XtraEditors.DateEdit();
            this.dteETDFrom = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.nbarOther = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePeriodTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePeriodTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePeriodFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePeriodFrom.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            this.groupOpenOrAll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAttached.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkETA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel3)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAccountsPayable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLocalInvoice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkETD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETATo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETATo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETDTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETDTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETAFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETAFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETDFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETDFrom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 597);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(242, 59);
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
            this.panel2.Size = new System.Drawing.Size(242, 597);
            this.panel2.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase,
            this.nbarOther});
            this.navBarControl1.Location = new System.Drawing.Point(2, 2);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(238, 524);
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "Base";
            this.nbarBase.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 192;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.txtCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.labOrderBy);
            this.navBarGroupControlContainer1.Controls.Add(this.labPeriod);
            this.navBarGroupControlContainer1.Controls.Add(this.labCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.rdoBillDate);
            this.navBarGroupControlContainer1.Controls.Add(this.rdoETD);
            this.navBarGroupControlContainer1.Controls.Add(this.dtePeriodTo);
            this.navBarGroupControlContainer1.Controls.Add(this.chkcmbCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.dtePeriodFrom);
            this.navBarGroupControlContainer1.Controls.Add(this.labCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.labelControl1);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(230, 190);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // txtCustomer
            // 
            this.txtCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomer.FinderName = "CustomerFinder";
            this.txtCustomer.Location = new System.Drawing.Point(9, 73);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCustomer.Size = new System.Drawing.Size(218, 21);
            this.txtCustomer.TabIndex = 1;
            // 
            // labOrderBy
            // 
            this.labOrderBy.Location = new System.Drawing.Point(9, 147);
            this.labOrderBy.Name = "labOrderBy";
            this.labOrderBy.Size = new System.Drawing.Size(66, 14);
            this.labOrderBy.TabIndex = 41;
            this.labOrderBy.Text = "Period Type";
            // 
            // labPeriod
            // 
            this.labPeriod.Location = new System.Drawing.Point(9, 100);
            this.labPeriod.Name = "labPeriod";
            this.labPeriod.Size = new System.Drawing.Size(34, 14);
            this.labPeriod.TabIndex = 41;
            this.labPeriod.Text = "Period";
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(9, 53);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 33;
            this.labCustomer.Text = "Customer";
            // 
            // rdoBillDate
            // 
            this.rdoBillDate.AutoSize = true;
            this.rdoBillDate.Checked = true;
            this.rdoBillDate.Location = new System.Drawing.Point(11, 167);
            this.rdoBillDate.Name = "rdoBillDate";
            this.rdoBillDate.Size = new System.Drawing.Size(68, 18);
            this.rdoBillDate.TabIndex = 2;
            this.rdoBillDate.TabStop = true;
            this.rdoBillDate.Text = "Bill Date";
            this.rdoBillDate.UseVisualStyleBackColor = true;
            // 
            // rdoETD
            // 
            this.rdoETD.AutoSize = true;
            this.rdoETD.Location = new System.Drawing.Point(111, 167);
            this.rdoETD.Name = "rdoETD";
            this.rdoETD.Size = new System.Drawing.Size(77, 18);
            this.rdoETD.TabIndex = 3;
            this.rdoETD.Text = "Due Date";
            this.rdoETD.UseVisualStyleBackColor = true;
            // 
            // dtePeriodTo
            // 
            this.dtePeriodTo.EditValue = new System.DateTime(2011, 12, 22, 0, 0, 0, 0);
            this.dtePeriodTo.Location = new System.Drawing.Point(137, 120);
            this.dtePeriodTo.Name = "dtePeriodTo";
            this.dtePeriodTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtePeriodTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtePeriodTo.Size = new System.Drawing.Size(90, 21);
            this.dtePeriodTo.TabIndex = 5;
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.Location = new System.Drawing.Point(9, 26);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.NullText = "";
            this.chkcmbCompany.Size = new System.Drawing.Size(218, 21);
            this.chkcmbCompany.SplitText = ",";
            this.chkcmbCompany.TabIndex = 0;
            // 
            // dtePeriodFrom
            // 
            this.dtePeriodFrom.EditValue = new System.DateTime(2011, 12, 22, 0, 0, 0, 0);
            this.dtePeriodFrom.Location = new System.Drawing.Point(9, 120);
            this.dtePeriodFrom.Name = "dtePeriodFrom";
            this.dtePeriodFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtePeriodFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtePeriodFrom.Size = new System.Drawing.Size(90, 21);
            this.dtePeriodFrom.TabIndex = 4;
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(9, 6);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 31;
            this.labCompany.Text = "Company";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(101, 123);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(21, 14);
            this.labelControl1.TabIndex = 41;
            this.labelControl1.Text = "--->";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.groupOpenOrAll);
            this.navBarGroupControlContainer2.Controls.Add(this.chkAttached);
            this.navBarGroupControlContainer2.Controls.Add(this.chkETA);
            this.navBarGroupControlContainer2.Controls.Add(this.panel3);
            this.navBarGroupControlContainer2.Controls.Add(this.chkETD);
            this.navBarGroupControlContainer2.Controls.Add(this.dteETATo);
            this.navBarGroupControlContainer2.Controls.Add(this.dteETDTo);
            this.navBarGroupControlContainer2.Controls.Add(this.dteETAFrom);
            this.navBarGroupControlContainer2.Controls.Add(this.dteETDFrom);
            this.navBarGroupControlContainer2.Controls.Add(this.labelControl3);
            this.navBarGroupControlContainer2.Controls.Add(this.labelControl2);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(230, 254);
            this.navBarGroupControlContainer2.TabIndex = 2;
            // 
            // groupOpenOrAll
            // 
            this.groupOpenOrAll.Controls.Add(this.rdoAll);
            this.groupOpenOrAll.Controls.Add(this.rdoOpenWhitInvoice);
            this.groupOpenOrAll.Controls.Add(this.rdoOpen);
            this.groupOpenOrAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupOpenOrAll.Location = new System.Drawing.Point(0, 46);
            this.groupOpenOrAll.Name = "groupOpenOrAll";
            this.groupOpenOrAll.Size = new System.Drawing.Size(230, 60);
            this.groupOpenOrAll.TabIndex = 1;
            this.groupOpenOrAll.TabStop = false;
            this.groupOpenOrAll.Text = "Open/All";
            // 
            // rdoAll
            // 
            this.rdoAll.AutoSize = true;
            this.rdoAll.Checked = true;
            this.rdoAll.Location = new System.Drawing.Point(6, 17);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new System.Drawing.Size(37, 18);
            this.rdoAll.TabIndex = 0;
            this.rdoAll.TabStop = true;
            this.rdoAll.Text = "All";
            this.rdoAll.UseVisualStyleBackColor = true;
            // 
            // rdoOpenWhitInvoice
            // 
            this.rdoOpenWhitInvoice.AutoSize = true;
            this.rdoOpenWhitInvoice.Location = new System.Drawing.Point(6, 39);
            this.rdoOpenWhitInvoice.Name = "rdoOpenWhitInvoice";
            this.rdoOpenWhitInvoice.Size = new System.Drawing.Size(181, 18);
            this.rdoOpenWhitInvoice.TabIndex = 2;
            this.rdoOpenWhitInvoice.Text = "Open With Invoice Received";
            this.rdoOpenWhitInvoice.UseVisualStyleBackColor = true;
            // 
            // rdoOpen
            // 
            this.rdoOpen.AutoSize = true;
            this.rdoOpen.Location = new System.Drawing.Point(98, 17);
            this.rdoOpen.Name = "rdoOpen";
            this.rdoOpen.Size = new System.Drawing.Size(55, 18);
            this.rdoOpen.TabIndex = 1;
            this.rdoOpen.Text = "Open";
            this.rdoOpen.UseVisualStyleBackColor = true;
            // 
            // chkAttached
            // 
            this.chkAttached.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAttached.Location = new System.Drawing.Point(9, 228);
            this.chkAttached.Name = "chkAttached";
            this.chkAttached.Properties.Caption = "Attached original invoice(s)";
            this.chkAttached.Size = new System.Drawing.Size(218, 19);
            this.chkAttached.TabIndex = 6;
            // 
            // chkETA
            // 
            this.chkETA.Location = new System.Drawing.Point(7, 164);
            this.chkETA.Name = "chkETA";
            this.chkETA.Properties.Caption = "ETA";
            this.chkETA.Size = new System.Drawing.Size(67, 19);
            this.chkETA.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.labReportType);
            this.panel3.Controls.Add(this.chkAccountsPayable);
            this.panel3.Controls.Add(this.chkLocalInvoice);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(230, 46);
            this.panel3.TabIndex = 43;
            // 
            // labReportType
            // 
            this.labReportType.Location = new System.Drawing.Point(9, 3);
            this.labReportType.Name = "labReportType";
            this.labReportType.Size = new System.Drawing.Size(65, 14);
            this.labReportType.TabIndex = 41;
            this.labReportType.Text = "ReportType";
            // 
            // chkAccountsPayable
            // 
            this.chkAccountsPayable.EditValue = true;
            this.chkAccountsPayable.Location = new System.Drawing.Point(102, 23);
            this.chkAccountsPayable.Name = "chkAccountsPayable";
            this.chkAccountsPayable.Properties.Caption = "Accounts Payable";
            this.chkAccountsPayable.Size = new System.Drawing.Size(123, 19);
            this.chkAccountsPayable.TabIndex = 1;
            // 
            // chkLocalInvoice
            // 
            this.chkLocalInvoice.EditValue = true;
            this.chkLocalInvoice.Location = new System.Drawing.Point(6, 23);
            this.chkLocalInvoice.Name = "chkLocalInvoice";
            this.chkLocalInvoice.Properties.Caption = "Local Invoice";
            this.chkLocalInvoice.Size = new System.Drawing.Size(92, 19);
            this.chkLocalInvoice.TabIndex = 0;
            // 
            // chkETD
            // 
            this.chkETD.Location = new System.Drawing.Point(7, 112);
            this.chkETD.Name = "chkETD";
            this.chkETD.Properties.Caption = "ETD";
            this.chkETD.Size = new System.Drawing.Size(67, 19);
            this.chkETD.TabIndex = 0;
            // 
            // dteETATo
            // 
            this.dteETATo.EditValue = new System.DateTime(2011, 12, 22, 0, 0, 0, 0);
            this.dteETATo.Enabled = false;
            this.dteETATo.Location = new System.Drawing.Point(137, 189);
            this.dteETATo.Name = "dteETATo";
            this.dteETATo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteETATo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteETATo.Size = new System.Drawing.Size(90, 21);
            this.dteETATo.TabIndex = 5;
            // 
            // dteETDTo
            // 
            this.dteETDTo.EditValue = new System.DateTime(2011, 12, 22, 0, 0, 0, 0);
            this.dteETDTo.Enabled = false;
            this.dteETDTo.Location = new System.Drawing.Point(137, 137);
            this.dteETDTo.Name = "dteETDTo";
            this.dteETDTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteETDTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteETDTo.Size = new System.Drawing.Size(90, 21);
            this.dteETDTo.TabIndex = 2;
            // 
            // dteETAFrom
            // 
            this.dteETAFrom.EditValue = new System.DateTime(2011, 12, 22, 0, 0, 0, 0);
            this.dteETAFrom.Enabled = false;
            this.dteETAFrom.Location = new System.Drawing.Point(9, 189);
            this.dteETAFrom.Name = "dteETAFrom";
            this.dteETAFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteETAFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteETAFrom.Size = new System.Drawing.Size(90, 21);
            this.dteETAFrom.TabIndex = 4;
            // 
            // dteETDFrom
            // 
            this.dteETDFrom.EditValue = new System.DateTime(2011, 12, 22, 0, 0, 0, 0);
            this.dteETDFrom.Enabled = false;
            this.dteETDFrom.Location = new System.Drawing.Point(9, 137);
            this.dteETDFrom.Name = "dteETDFrom";
            this.dteETDFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteETDFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteETDFrom.Size = new System.Drawing.Size(90, 21);
            this.dteETDFrom.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(101, 192);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(21, 14);
            this.labelControl3.TabIndex = 41;
            this.labelControl3.Text = "--->";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(101, 140);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(21, 14);
            this.labelControl2.TabIndex = 41;
            this.labelControl2.Text = "--->";
            // 
            // nbarOther
            // 
            this.nbarOther.Caption = "Other";
            this.nbarOther.ControlContainer = this.navBarGroupControlContainer2;
            this.nbarOther.Expanded = true;
            this.nbarOther.GroupClientHeight = 256;
            this.nbarOther.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarOther.Name = "nbarOther";
            // 
            // LocalStatementSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "LocalStatementSearchPart";
            this.Size = new System.Drawing.Size(242, 656);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel2)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePeriodTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePeriodTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePeriodFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePeriodFrom.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            this.groupOpenOrAll.ResumeLayout(false);
            this.groupOpenOrAll.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAttached.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkETA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel3)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAccountsPayable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLocalInvoice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkETD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETATo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETATo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETDTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETDTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETAFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETAFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETDFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETDFrom.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panel2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private ICP.ReportCenter.UI.Comm.Controls.CompanyCheckBoxComboBox chkcmbCompany;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private ICP.ReportCenter.UI.Comm.Controls.SingleCustomerFinderButtonEdit txtCustomer;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private System.Windows.Forms.RadioButton rdoETD;
        private System.Windows.Forms.RadioButton rdoBillDate;
        private DevExpress.XtraEditors.DateEdit dtePeriodFrom;
        private DevExpress.XtraEditors.LabelControl labReportType;
        private DevExpress.XtraEditors.DateEdit dtePeriodTo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroup nbarOther;
        private System.Windows.Forms.GroupBox groupOpenOrAll;
        private DevExpress.XtraEditors.CheckEdit chkAttached;
        private DevExpress.XtraEditors.LabelControl labPeriod;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit chkAccountsPayable;
        private DevExpress.XtraEditors.CheckEdit chkLocalInvoice;
        private System.Windows.Forms.RadioButton rdoAll;
        private System.Windows.Forms.RadioButton rdoOpenWhitInvoice;
        private System.Windows.Forms.RadioButton rdoOpen;
        private DevExpress.XtraEditors.CheckEdit chkETA;
        private DevExpress.XtraEditors.PanelControl panel3;
        private DevExpress.XtraEditors.CheckEdit chkETD;
        private DevExpress.XtraEditors.DateEdit dteETATo;
        private DevExpress.XtraEditors.DateEdit dteETDTo;
        private DevExpress.XtraEditors.DateEdit dteETAFrom;
        private DevExpress.XtraEditors.DateEdit dteETDFrom;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labOrderBy;
    }
}
