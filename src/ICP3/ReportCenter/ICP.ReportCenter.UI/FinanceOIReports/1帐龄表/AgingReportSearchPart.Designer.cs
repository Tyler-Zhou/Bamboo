using ICP.Framework.ClientComponents.Controls;
namespace ICP.ReportCenter.UI.FinanceOIReports
{
   partial class AgingReportSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgingReportSearchPart));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelFill = new DevExpress.XtraEditors.PanelControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarStyle = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.rdoCA_Detail = new System.Windows.Forms.RadioButton();
            this.rdoDetail = new System.Windows.Forms.RadioButton();
            this.rdoSummary = new System.Windows.Forms.RadioButton();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.rdoGroupInsured = new DevExpress.XtraEditors.RadioGroup();
            this.rdoGroupTerm = new DevExpress.XtraEditors.RadioGroup();
            this.labOperation = new DevExpress.XtraEditors.LabelControl();
            this.chkDRCR = new DevExpress.XtraEditors.CheckEdit();
            this.chkAP = new DevExpress.XtraEditors.CheckEdit();
            this.chkAR = new DevExpress.XtraEditors.CheckEdit();
            this.txtCustomer = new ICP.ReportCenter.UI.Comm.Controls.SingleCustomerFinderButtonEdit();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.chkcmbOperaionType = new ICP.ReportCenter.UI.Comm.Controls.OperationTypeCheckBoxComboBox();
            this.chkcmbCompany = new ICP.ReportCenter.UI.Comm.Controls.CompanyCheckBoxComboBox();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panelDate = new DevExpress.XtraEditors.XtraScrollableControl();
            this.rdoAgingDateState = new DevExpress.XtraEditors.RadioGroup();
            this.rdoFinanceDate = new System.Windows.Forms.RadioButton();
            this.dteEndingDate = new DevExpress.XtraEditors.DateEdit();
            this.rdoDueDate = new System.Windows.Forms.RadioButton();
            this.labEndingDate = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panelETD = new DevExpress.XtraEditors.XtraScrollableControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.chkOverPaid = new DevExpress.XtraEditors.CheckEdit();
            this.cmbCurrency = new ICP.ReportCenter.UI.Comm.Controls.CurrencyLWImageComboBoxEdit();
            this.labAmountTo = new DevExpress.XtraEditors.LabelControl();
            this.dteTo = new DevExpress.XtraEditors.DateEdit();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.dteFrom = new DevExpress.XtraEditors.DateEdit();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.nBarOther = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelFill)).BeginInit();
            this.panelFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoGroupInsured.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoGroupTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDRCR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            this.panelDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoAgingDateState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndingDate.Properties)).BeginInit();
            this.navBarGroupControlContainer3.SuspendLayout();
            this.panelETD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkOverPaid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 597);
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
            // panelFill
            // 
            this.panelFill.Controls.Add(this.navBarControl1);
            this.panelFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFill.Location = new System.Drawing.Point(0, 0);
            this.panelFill.Name = "panelFill";
            this.panelFill.Size = new System.Drawing.Size(240, 597);
            this.panelFill.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarStyle;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer3);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarStyle,
            this.nbarBase,
            this.nbarDate,
            this.nBarOther});
            this.navBarControl1.Location = new System.Drawing.Point(2, 2);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(236, 593);
            this.navBarControl1.SkinExplorerBarViewScrollStyle = DevExpress.XtraNavBar.SkinExplorerBarViewScrollStyle.ScrollBar;
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarStyle
            // 
            this.nbarStyle.Caption = "Style";
            this.nbarStyle.ControlContainer = this.navBarGroupBase;
            this.nbarStyle.Expanded = true;
            this.nbarStyle.GroupClientHeight = 29;
            this.nbarStyle.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarStyle.Name = "nbarStyle";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.rdoCA_Detail);
            this.navBarGroupBase.Controls.Add(this.rdoDetail);
            this.navBarGroupBase.Controls.Add(this.rdoSummary);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(228, 27);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // rdoCA_Detail
            // 
            this.rdoCA_Detail.AutoSize = true;
            this.rdoCA_Detail.Location = new System.Drawing.Point(137, 3);
            this.rdoCA_Detail.Name = "rdoCA_Detail";
            this.rdoCA_Detail.Size = new System.Drawing.Size(74, 18);
            this.rdoCA_Detail.TabIndex = 0;
            this.rdoCA_Detail.Text = "CA-Detail";
            this.rdoCA_Detail.UseVisualStyleBackColor = true;
            // 
            // rdoDetail
            // 
            this.rdoDetail.AutoSize = true;
            this.rdoDetail.Location = new System.Drawing.Point(84, 3);
            this.rdoDetail.Name = "rdoDetail";
            this.rdoDetail.Size = new System.Drawing.Size(55, 18);
            this.rdoDetail.TabIndex = 0;
            this.rdoDetail.Text = "Detail";
            this.rdoDetail.UseVisualStyleBackColor = true;
            // 
            // rdoSummary
            // 
            this.rdoSummary.AutoSize = true;
            this.rdoSummary.Checked = true;
            this.rdoSummary.Location = new System.Drawing.Point(3, 3);
            this.rdoSummary.Name = "rdoSummary";
            this.rdoSummary.Size = new System.Drawing.Size(75, 18);
            this.rdoSummary.TabIndex = 0;
            this.rdoSummary.TabStop = true;
            this.rdoSummary.Text = "Summary";
            this.rdoSummary.UseVisualStyleBackColor = true;
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.rdoGroupInsured);
            this.navBarGroupControlContainer1.Controls.Add(this.rdoGroupTerm);
            this.navBarGroupControlContainer1.Controls.Add(this.labOperation);
            this.navBarGroupControlContainer1.Controls.Add(this.chkDRCR);
            this.navBarGroupControlContainer1.Controls.Add(this.chkAP);
            this.navBarGroupControlContainer1.Controls.Add(this.chkAR);
            this.navBarGroupControlContainer1.Controls.Add(this.txtCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.labCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.chkcmbOperaionType);
            this.navBarGroupControlContainer1.Controls.Add(this.chkcmbCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.labCompany);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(228, 178);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // rdoGroupInsured
            // 
            this.rdoGroupInsured.EditValue = "insuredALL";
            this.rdoGroupInsured.Location = new System.Drawing.Point(6, 114);
            this.rdoGroupInsured.Name = "rdoGroupInsured";
            this.rdoGroupInsured.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("insuredALL", "All"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("insuredInsured", "Insured"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("insuredUnInsured", "Un Insured")});
            this.rdoGroupInsured.Size = new System.Drawing.Size(220, 27);
            this.rdoGroupInsured.TabIndex = 38;
            // 
            // rdoGroupTerm
            // 
            this.rdoGroupTerm.EditValue = "termAll";
            this.rdoGroupTerm.Location = new System.Drawing.Point(7, 81);
            this.rdoGroupTerm.Name = "rdoGroupTerm";
            this.rdoGroupTerm.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("termAll", "All"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("termTerm", "Term"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("termUnTerm", "Un Term")});
            this.rdoGroupTerm.Size = new System.Drawing.Size(220, 27);
            this.rdoGroupTerm.TabIndex = 37;
            // 
            // labOperation
            // 
            this.labOperation.Location = new System.Drawing.Point(7, 31);
            this.labOperation.Name = "labOperation";
            this.labOperation.Size = new System.Drawing.Size(54, 14);
            this.labOperation.TabIndex = 36;
            this.labOperation.Text = "Operation";
            // 
            // chkDRCR
            // 
            this.chkDRCR.Location = new System.Drawing.Point(120, 3);
            this.chkDRCR.Name = "chkDRCR";
            this.chkDRCR.Properties.Caption = "DRCR";
            this.chkDRCR.Size = new System.Drawing.Size(63, 19);
            this.chkDRCR.TabIndex = 34;
            // 
            // chkAP
            // 
            this.chkAP.Location = new System.Drawing.Point(64, 3);
            this.chkAP.Name = "chkAP";
            this.chkAP.Properties.Caption = "AP";
            this.chkAP.Size = new System.Drawing.Size(50, 19);
            this.chkAP.TabIndex = 34;
            // 
            // chkAR
            // 
            this.chkAR.EditValue = true;
            this.chkAR.Location = new System.Drawing.Point(7, 3);
            this.chkAR.Name = "chkAR";
            this.chkAR.Properties.Caption = "AR";
            this.chkAR.Size = new System.Drawing.Size(50, 19);
            this.chkAR.TabIndex = 34;
            // 
            // txtCustomer
            // 
            this.txtCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomer.FinderName = "CustomerFinder";
            this.txtCustomer.Location = new System.Drawing.Point(66, 55);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCustomer.Size = new System.Drawing.Size(157, 21);
            this.txtCustomer.TabIndex = 32;
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(7, 58);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 33;
            this.labCustomer.Text = "Customer";
            // 
            // chkcmbOperaionType
            // 
            this.chkcmbOperaionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbOperaionType.CheckedTypes = ((System.Collections.Generic.List<ICP.Framework.CommonLibrary.Common.OperationType>)(resources.GetObject("chkcmbOperaionType.CheckedTypes")));
            this.chkcmbOperaionType.Location = new System.Drawing.Point(66, 28);
            this.chkcmbOperaionType.Name = "chkcmbOperaionType";
            this.chkcmbOperaionType.NullText = "";
            this.chkcmbOperaionType.Size = new System.Drawing.Size(157, 21);
            this.chkcmbOperaionType.SplitText = ",";
            this.chkcmbOperaionType.TabIndex = 30;
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.Location = new System.Drawing.Point(66, 147);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.NullText = "";
            this.chkcmbCompany.Size = new System.Drawing.Size(157, 21);
            this.chkcmbCompany.SplitText = ",";
            this.chkcmbCompany.TabIndex = 30;
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(7, 150);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 31;
            this.labCompany.Text = "Company";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.panelDate);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(228, 152);
            this.navBarGroupControlContainer2.TabIndex = 2;
            // 
            // panelDate
            // 
            this.panelDate.Controls.Add(this.rdoAgingDateState);
            this.panelDate.Controls.Add(this.rdoFinanceDate);
            this.panelDate.Controls.Add(this.dteEndingDate);
            this.panelDate.Controls.Add(this.rdoDueDate);
            this.panelDate.Controls.Add(this.labEndingDate);
            this.panelDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDate.Location = new System.Drawing.Point(0, 0);
            this.panelDate.Name = "panelDate";
            this.panelDate.Size = new System.Drawing.Size(228, 152);
            this.panelDate.TabIndex = 39;
            // 
            // rdoAgingDateState
            // 
            this.rdoAgingDateState.EditValue = "pastDueAll";
            this.rdoAgingDateState.Location = new System.Drawing.Point(9, 52);
            this.rdoAgingDateState.Name = "rdoAgingDateState";
            this.rdoAgingDateState.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("pastDueAll", "All"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("pastDueOver90", "Over90"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("pastDueOver60", "Over60"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("pastDueOver45", "Over45"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("pastDueOver30", "Over30"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("pastDueLess30", "Less30")});
            this.rdoAgingDateState.Size = new System.Drawing.Size(202, 87);
            this.rdoAgingDateState.TabIndex = 37;
            // 
            // rdoFinanceDate
            // 
            this.rdoFinanceDate.AutoSize = true;
            this.rdoFinanceDate.Checked = true;
            this.rdoFinanceDate.Location = new System.Drawing.Point(6, 3);
            this.rdoFinanceDate.Name = "rdoFinanceDate";
            this.rdoFinanceDate.Size = new System.Drawing.Size(92, 18);
            this.rdoFinanceDate.TabIndex = 0;
            this.rdoFinanceDate.TabStop = true;
            this.rdoFinanceDate.Text = "FinanceDate";
            this.rdoFinanceDate.UseVisualStyleBackColor = true;
            // 
            // dteEndingDate
            // 
            this.dteEndingDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dteEndingDate.EditValue = new System.DateTime(2011, 12, 22, 0, 0, 0, 0);
            this.dteEndingDate.Location = new System.Drawing.Point(69, 24);
            this.dteEndingDate.Name = "dteEndingDate";
            this.dteEndingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEndingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteEndingDate.Size = new System.Drawing.Size(138, 21);
            this.dteEndingDate.TabIndex = 37;
            // 
            // rdoDueDate
            // 
            this.rdoDueDate.AutoSize = true;
            this.rdoDueDate.Location = new System.Drawing.Point(106, 3);
            this.rdoDueDate.Name = "rdoDueDate";
            this.rdoDueDate.Size = new System.Drawing.Size(73, 18);
            this.rdoDueDate.TabIndex = 0;
            this.rdoDueDate.Text = "DueDate";
            this.rdoDueDate.UseVisualStyleBackColor = true;
            // 
            // labEndingDate
            // 
            this.labEndingDate.Location = new System.Drawing.Point(10, 27);
            this.labEndingDate.Name = "labEndingDate";
            this.labEndingDate.Size = new System.Drawing.Size(51, 14);
            this.labEndingDate.TabIndex = 38;
            this.labEndingDate.Text = "End Date";
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.panelETD);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(228, 116);
            this.navBarGroupControlContainer3.TabIndex = 3;
            // 
            // panelETD
            // 
            this.panelETD.Controls.Add(this.labFrom);
            this.panelETD.Controls.Add(this.chkOverPaid);
            this.panelETD.Controls.Add(this.cmbCurrency);
            this.panelETD.Controls.Add(this.labAmountTo);
            this.panelETD.Controls.Add(this.dteTo);
            this.panelETD.Controls.Add(this.labTo);
            this.panelETD.Controls.Add(this.dteFrom);
            this.panelETD.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelETD.Enabled = false;
            this.panelETD.Location = new System.Drawing.Point(0, 0);
            this.panelETD.Name = "panelETD";
            this.panelETD.Size = new System.Drawing.Size(228, 113);
            this.panelETD.TabIndex = 44;
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(3, 6);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(27, 14);
            this.labFrom.TabIndex = 42;
            this.labFrom.Text = "From";
            // 
            // chkOverPaid
            // 
            this.chkOverPaid.Location = new System.Drawing.Point(64, 91);
            this.chkOverPaid.Name = "chkOverPaid";
            this.chkOverPaid.Properties.Caption = "Over Paid";
            this.chkOverPaid.Size = new System.Drawing.Size(131, 19);
            this.chkOverPaid.TabIndex = 34;
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.AddAllItem = true;
            this.cmbCurrency.AddItemsWhenLoad = false;
            this.cmbCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCurrency.EditValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.cmbCurrency.Location = new System.Drawing.Point(62, 57);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCurrency.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("All", new System.Guid("00000000-0000-0000-0000-000000000000"), -1)});
            this.cmbCurrency.ShowAllItemWhenLoad = true;
            this.cmbCurrency.Size = new System.Drawing.Size(125, 21);
            this.cmbCurrency.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCurrency.TabIndex = 43;
            // 
            // labAmountTo
            // 
            this.labAmountTo.Location = new System.Drawing.Point(3, 60);
            this.labAmountTo.Name = "labAmountTo";
            this.labAmountTo.Size = new System.Drawing.Size(42, 14);
            this.labAmountTo.TabIndex = 41;
            this.labAmountTo.Text = "Amt.To";
            // 
            // dteTo
            // 
            this.dteTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dteTo.EditValue = new System.DateTime(2011, 12, 22, 0, 0, 0, 0);
            this.dteTo.Location = new System.Drawing.Point(62, 30);
            this.dteTo.Name = "dteTo";
            this.dteTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteTo.Size = new System.Drawing.Size(125, 21);
            this.dteTo.TabIndex = 40;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(3, 33);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 41;
            this.labTo.Text = "To";
            // 
            // dteFrom
            // 
            this.dteFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dteFrom.EditValue = new System.DateTime(2011, 12, 22, 0, 0, 0, 0);
            this.dteFrom.Location = new System.Drawing.Point(62, 3);
            this.dteFrom.Name = "dteFrom";
            this.dteFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFrom.Size = new System.Drawing.Size(125, 21);
            this.dteFrom.TabIndex = 39;
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "Base";
            this.nbarBase.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 180;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // nbarDate
            // 
            this.nbarDate.Caption = "Date";
            this.nbarDate.ControlContainer = this.navBarGroupControlContainer2;
            this.nbarDate.Expanded = true;
            this.nbarDate.GroupClientHeight = 154;
            this.nbarDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarDate.Name = "nbarDate";
            // 
            // nBarOther
            // 
            this.nBarOther.Caption = "Other";
            this.nBarOther.ControlContainer = this.navBarGroupControlContainer3;
            this.nBarOther.Expanded = true;
            this.nBarOther.GroupClientHeight = 118;
            this.nBarOther.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nBarOther.Name = "nBarOther";
            // 
            // AgingReportSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelFill);
            this.Controls.Add(this.panel1);
            this.Name = "AgingReportSearchPart";
            this.Size = new System.Drawing.Size(240, 656);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelFill)).EndInit();
            this.panelFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoGroupInsured.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoGroupTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDRCR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.panelDate.ResumeLayout(false);
            this.panelDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoAgingDateState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndingDate.Properties)).EndInit();
            this.navBarGroupControlContainer3.ResumeLayout(false);
            this.panelETD.ResumeLayout(false);
            this.panelETD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkOverPaid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelFill;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarStyle;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private ICP.ReportCenter.UI.Comm.Controls.CompanyCheckBoxComboBox chkcmbCompany;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private System.Windows.Forms.RadioButton rdoCA_Detail;
        private System.Windows.Forms.RadioButton rdoDetail;
        private System.Windows.Forms.RadioButton rdoSummary;
        private DevExpress.XtraEditors.CheckEdit chkAP;
        private DevExpress.XtraEditors.CheckEdit chkAR;
        private ICP.ReportCenter.UI.Comm.Controls.SingleCustomerFinderButtonEdit txtCustomer;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.CheckEdit chkDRCR;
        private DevExpress.XtraEditors.LabelControl labOperation;
        private System.Windows.Forms.RadioButton rdoDueDate;
        private System.Windows.Forms.RadioButton rdoFinanceDate;
        private DevExpress.XtraEditors.DateEdit dteEndingDate;
        private DevExpress.XtraEditors.LabelControl labEndingDate;
        private ICP.ReportCenter.UI.Comm.Controls.CurrencyLWImageComboBoxEdit cmbCurrency;
        private DevExpress.XtraEditors.DateEdit dteFrom;
        private DevExpress.XtraEditors.LabelControl labAmountTo;
        private DevExpress.XtraEditors.DateEdit dteTo;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroup nbarDate;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        private DevExpress.XtraNavBar.NavBarGroup nBarOther;
        private DevExpress.XtraEditors.CheckEdit chkOverPaid;
        private DevExpress.XtraEditors.XtraScrollableControl panelDate;
        private DevExpress.XtraEditors.XtraScrollableControl panelETD;
        private ICP.ReportCenter.UI.Comm.Controls.OperationTypeCheckBoxComboBox chkcmbOperaionType;
        private DevExpress.XtraEditors.RadioGroup rdoAgingDateState;
        private DevExpress.XtraEditors.RadioGroup rdoGroupTerm;
        private DevExpress.XtraEditors.RadioGroup rdoGroupInsured;
    }
}
