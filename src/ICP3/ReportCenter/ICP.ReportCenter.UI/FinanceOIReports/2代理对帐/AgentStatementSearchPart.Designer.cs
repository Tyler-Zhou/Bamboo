using ICP.Framework.ClientComponents.Controls;
namespace ICP.ReportCenter.UI.FinanceOIReports
{
   partial class AgentStatementSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentStatementSearchPart));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.txtCustomer = new ICP.ReportCenter.UI.Comm.Controls.SingleCustomerFinderButtonEdit();
            this.chkcmbOperaionType = new ICP.ReportCenter.UI.Comm.Controls.OperationTypeCheckBoxComboBox();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labOperationType = new DevExpress.XtraEditors.LabelControl();
            this.chkcmbCompany = new ICP.ReportCenter.UI.Comm.Controls.CompanyCheckBoxComboBox();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cmbCurrency = new ICP.ReportCenter.UI.Comm.Controls.CurrencyLWImageComboBoxEdit();
            this.labCurrency = new DevExpress.XtraEditors.LabelControl();
            this.groupPeriod = new System.Windows.Forms.GroupBox();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.dteTo = new DevExpress.XtraEditors.DateEdit();
            this.dteFrom = new DevExpress.XtraEditors.DateEdit();
            this.rdoBillDate = new System.Windows.Forms.RadioButton();
            this.rdoETD = new System.Windows.Forms.RadioButton();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioGroup3 = new DevExpress.XtraEditors.RadioGroup();
            this.chkShowPaidStatus = new DevExpress.XtraEditors.CheckEdit();
            this.chkAttached = new DevExpress.XtraEditors.CheckEdit();
            this.groupFilter = new System.Windows.Forms.GroupBox();
            this.radioGroup2 = new DevExpress.XtraEditors.RadioGroup();
            this.groupStoryBy = new System.Windows.Forms.GroupBox();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.nbarDateType = new DevExpress.XtraNavBar.NavBarGroup();
            this.nBarOther = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).BeginInit();
            this.groupPeriod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).BeginInit();
            this.navBarGroupControlContainer3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowPaidStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAttached.Properties)).BeginInit();
            this.groupFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).BeginInit();
            this.groupStoryBy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 597);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 59);
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
            this.panel2.Size = new System.Drawing.Size(249, 597);
            this.panel2.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer3);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase,
            this.nbarDateType,
            this.nBarOther});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(249, 587);
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "Base";
            this.nbarBase.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 111;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.txtCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.chkcmbOperaionType);
            this.navBarGroupControlContainer1.Controls.Add(this.labCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.labOperationType);
            this.navBarGroupControlContainer1.Controls.Add(this.chkcmbCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.labCompany);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(241, 109);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // txtCustomer
            // 
            this.txtCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomer.FinderName = "CustomerFinder";
            this.txtCustomer.Location = new System.Drawing.Point(66, 3);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCustomer.Size = new System.Drawing.Size(170, 21);
            this.txtCustomer.TabIndex = 0;
            // 
            // chkcmbOperaionType
            // 
            this.chkcmbOperaionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbOperaionType.CheckedTypes = ((System.Collections.Generic.List<ICP.Framework.CommonLibrary.Common.OperationType>)(resources.GetObject("chkcmbOperaionType.CheckedTypes")));
            this.chkcmbOperaionType.Location = new System.Drawing.Point(11, 77);
            this.chkcmbOperaionType.Name = "chkcmbOperaionType";
            this.chkcmbOperaionType.NullText = "";
            this.chkcmbOperaionType.Size = new System.Drawing.Size(227, 21);
            this.chkcmbOperaionType.SplitText = ",";
            this.chkcmbOperaionType.TabIndex = 2;
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(9, 6);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 33;
            this.labCustomer.Text = "Customer";
            // 
            // labOperationType
            // 
            this.labOperationType.Location = new System.Drawing.Point(9, 57);
            this.labOperationType.Name = "labOperationType";
            this.labOperationType.Size = new System.Drawing.Size(82, 14);
            this.labOperationType.TabIndex = 31;
            this.labOperationType.Text = "OperationType";
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.Location = new System.Drawing.Point(66, 30);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.NullText = "";
            this.chkcmbCompany.Size = new System.Drawing.Size(170, 21);
            this.chkcmbCompany.SplitText = ",";
            this.chkcmbCompany.TabIndex = 1;
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(9, 33);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 31;
            this.labCompany.Text = "Company";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.cmbCurrency);
            this.navBarGroupControlContainer2.Controls.Add(this.labCurrency);
            this.navBarGroupControlContainer2.Controls.Add(this.groupPeriod);
            this.navBarGroupControlContainer2.Controls.Add(this.rdoBillDate);
            this.navBarGroupControlContainer2.Controls.Add(this.rdoETD);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(241, 118);
            this.navBarGroupControlContainer2.TabIndex = 2;
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.AddAllItem = true;
            this.cmbCurrency.AddItemsWhenLoad = false;
            this.cmbCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCurrency.EditValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.cmbCurrency.Location = new System.Drawing.Point(66, 27);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCurrency.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(" 全部", new System.Guid("00000000-0000-0000-0000-000000000000"), -1)});
            this.cmbCurrency.ShowAllItemWhenLoad = true;
            this.cmbCurrency.Size = new System.Drawing.Size(170, 21);
            this.cmbCurrency.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCurrency.TabIndex = 2;
            // 
            // labCurrency
            // 
            this.labCurrency.Location = new System.Drawing.Point(9, 30);
            this.labCurrency.Name = "labCurrency";
            this.labCurrency.Size = new System.Drawing.Size(48, 14);
            this.labCurrency.TabIndex = 41;
            this.labCurrency.Text = "Currency";
            // 
            // groupPeriod
            // 
            this.groupPeriod.Controls.Add(this.labFrom);
            this.groupPeriod.Controls.Add(this.labTo);
            this.groupPeriod.Controls.Add(this.dteTo);
            this.groupPeriod.Controls.Add(this.dteFrom);
            this.groupPeriod.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupPeriod.Location = new System.Drawing.Point(0, 49);
            this.groupPeriod.Name = "groupPeriod";
            this.groupPeriod.Size = new System.Drawing.Size(241, 69);
            this.groupPeriod.TabIndex = 43;
            this.groupPeriod.TabStop = false;
            this.groupPeriod.Text = "Period";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(9, 17);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(27, 14);
            this.labFrom.TabIndex = 42;
            this.labFrom.Text = "From";
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(9, 44);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 41;
            this.labTo.Text = "To";
            // 
            // dteTo
            // 
            this.dteTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dteTo.EditValue = new System.DateTime(2011, 12, 22, 0, 0, 0, 0);
            this.dteTo.Location = new System.Drawing.Point(66, 41);
            this.dteTo.Name = "dteTo";
            this.dteTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteTo.Size = new System.Drawing.Size(170, 21);
            this.dteTo.TabIndex = 1;
            // 
            // dteFrom
            // 
            this.dteFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dteFrom.EditValue = new System.DateTime(2011, 12, 22, 0, 0, 0, 0);
            this.dteFrom.Location = new System.Drawing.Point(66, 14);
            this.dteFrom.Name = "dteFrom";
            this.dteFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFrom.Size = new System.Drawing.Size(170, 21);
            this.dteFrom.TabIndex = 0;
            // 
            // rdoBillDate
            // 
            this.rdoBillDate.AutoSize = true;
            this.rdoBillDate.Checked = true;
            this.rdoBillDate.Location = new System.Drawing.Point(11, 3);
            this.rdoBillDate.Name = "rdoBillDate";
            this.rdoBillDate.Size = new System.Drawing.Size(64, 18);
            this.rdoBillDate.TabIndex = 0;
            this.rdoBillDate.TabStop = true;
            this.rdoBillDate.Text = "BillDate";
            this.rdoBillDate.UseVisualStyleBackColor = true;
            // 
            // rdoETD
            // 
            this.rdoETD.AutoSize = true;
            this.rdoETD.Location = new System.Drawing.Point(111, 3);
            this.rdoETD.Name = "rdoETD";
            this.rdoETD.Size = new System.Drawing.Size(48, 18);
            this.rdoETD.TabIndex = 1;
            this.rdoETD.Text = "ETD";
            this.rdoETD.UseVisualStyleBackColor = true;
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.groupBox1);
            this.navBarGroupControlContainer3.Controls.Add(this.chkShowPaidStatus);
            this.navBarGroupControlContainer3.Controls.Add(this.chkAttached);
            this.navBarGroupControlContainer3.Controls.Add(this.groupFilter);
            this.navBarGroupControlContainer3.Controls.Add(this.groupStoryBy);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(241, 262);
            this.navBarGroupControlContainer3.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioGroup3);
            this.groupBox1.Location = new System.Drawing.Point(6, 189);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 51);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Show Local A/R Paid Status";
            this.groupBox1.Visible = false;
            // 
            // radioGroup3
            // 
            this.radioGroup3.Location = new System.Drawing.Point(1, 16);
            this.radioGroup3.Name = "radioGroup3";
            this.radioGroup3.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "All"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Positive"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "negative")});
            this.radioGroup3.Size = new System.Drawing.Size(223, 28);
            this.radioGroup3.TabIndex = 5;
            // 
            // chkShowPaidStatus
            // 
            this.chkShowPaidStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowPaidStatus.Location = new System.Drawing.Point(3, 165);
            this.chkShowPaidStatus.Name = "chkShowPaidStatus";
            this.chkShowPaidStatus.Properties.Caption = "Show Local A/R Paid Status";
            this.chkShowPaidStatus.Size = new System.Drawing.Size(229, 19);
            this.chkShowPaidStatus.TabIndex = 3;
            this.chkShowPaidStatus.CheckedChanged += new System.EventHandler(this.chkShowPaidStatus_CheckedChanged);
            // 
            // chkAttached
            // 
            this.chkAttached.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAttached.Location = new System.Drawing.Point(3, 140);
            this.chkAttached.Name = "chkAttached";
            this.chkAttached.Properties.Caption = "Attached CR/DR Note(s)Invoice(s)";
            this.chkAttached.Size = new System.Drawing.Size(229, 19);
            this.chkAttached.TabIndex = 2;
            // 
            // groupFilter
            // 
            this.groupFilter.Controls.Add(this.radioGroup2);
            this.groupFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupFilter.Location = new System.Drawing.Point(0, 83);
            this.groupFilter.Name = "groupFilter";
            this.groupFilter.Size = new System.Drawing.Size(241, 51);
            this.groupFilter.TabIndex = 1;
            this.groupFilter.TabStop = false;
            this.groupFilter.Text = "Filter By";
            // 
            // radioGroup2
            // 
            this.radioGroup2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioGroup2.Location = new System.Drawing.Point(3, 18);
            this.radioGroup2.Name = "radioGroup2";
            this.radioGroup2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "All"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Open"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Paid")});
            this.radioGroup2.Size = new System.Drawing.Size(235, 30);
            this.radioGroup2.TabIndex = 0;
            // 
            // groupStoryBy
            // 
            this.groupStoryBy.Controls.Add(this.radioGroup1);
            this.groupStoryBy.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupStoryBy.Location = new System.Drawing.Point(0, 0);
            this.groupStoryBy.Name = "groupStoryBy";
            this.groupStoryBy.Size = new System.Drawing.Size(241, 83);
            this.groupStoryBy.TabIndex = 0;
            this.groupStoryBy.TabStop = false;
            this.groupStoryBy.Text = "Story By";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioGroup1.Location = new System.Drawing.Point(3, 18);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "ETD"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Our Ref.No"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Group By MBL"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Cr/DrDate"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Agent Ref No")});
            this.radioGroup1.Size = new System.Drawing.Size(235, 62);
            this.radioGroup1.TabIndex = 0;
            // 
            // nbarDateType
            // 
            this.nbarDateType.Caption = "DateType";
            this.nbarDateType.ControlContainer = this.navBarGroupControlContainer2;
            this.nbarDateType.Expanded = true;
            this.nbarDateType.GroupClientHeight = 120;
            this.nbarDateType.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarDateType.Name = "nbarDateType";
            // 
            // nBarOther
            // 
            this.nBarOther.Caption = "Other";
            this.nBarOther.ControlContainer = this.navBarGroupControlContainer3;
            this.nBarOther.Expanded = true;
            this.nBarOther.GroupClientHeight = 264;
            this.nBarOther.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nBarOther.Name = "nBarOther";
            // 
            // AgentStatementSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "AgentStatementSearchPart";
            this.Size = new System.Drawing.Size(249, 656);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).EndInit();
            this.groupPeriod.ResumeLayout(false);
            this.groupPeriod.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).EndInit();
            this.navBarGroupControlContainer3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowPaidStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAttached.Properties)).EndInit();
            this.groupFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).EndInit();
            this.groupStoryBy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.XtraScrollableControl panel2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private ICP.ReportCenter.UI.Comm.Controls.CompanyCheckBoxComboBox chkcmbCompany;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private ICP.ReportCenter.UI.Comm.Controls.SingleCustomerFinderButtonEdit txtCustomer;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private System.Windows.Forms.RadioButton rdoETD;
        private System.Windows.Forms.RadioButton rdoBillDate;
        private ICP.ReportCenter.UI.Comm.Controls.CurrencyLWImageComboBoxEdit cmbCurrency;
        private DevExpress.XtraEditors.DateEdit dteFrom;
        private DevExpress.XtraEditors.LabelControl labCurrency;
        private DevExpress.XtraEditors.DateEdit dteTo;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroup nbarDateType;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        private DevExpress.XtraNavBar.NavBarGroup nBarOther;
        private System.Windows.Forms.GroupBox groupPeriod;
        private System.Windows.Forms.GroupBox groupFilter;
        private ICP.ReportCenter.UI.Comm.Controls.OperationTypeCheckBoxComboBox chkcmbOperaionType;
        private DevExpress.XtraEditors.LabelControl labOperationType;
        private DevExpress.XtraEditors.RadioGroup radioGroup2;
        private System.Windows.Forms.GroupBox groupStoryBy;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.CheckEdit chkShowPaidStatus;
        private DevExpress.XtraEditors.CheckEdit chkAttached;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.RadioGroup radioGroup3;
    }
}
