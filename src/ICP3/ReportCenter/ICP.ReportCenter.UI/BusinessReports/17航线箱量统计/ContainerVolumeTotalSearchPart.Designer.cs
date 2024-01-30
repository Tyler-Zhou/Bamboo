namespace ICP.ReportCenter.UI.BusinessReports
{
    partial class ContainerVolumeTotalSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContainerVolumeTotalSearchPart));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.rdoDefault = new System.Windows.Forms.RadioButton();
            this.dateTo = new DevExpress.XtraEditors.DateEdit();
            this.rdoCustomizing = new System.Windows.Forms.RadioButton();
            this.dateFrom = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbWeekDate = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.lblGroupType = new DevExpress.XtraEditors.LabelControl();
            this.rdoGroup = new DevExpress.XtraEditors.RadioGroup();
            this.lblCompany = new DevExpress.XtraEditors.LabelControl();
            this.trCompany = new ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl();
            this.lblSales = new DevExpress.XtraEditors.LabelControl();
            this.txtSales = new DevExpress.XtraEditors.ButtonEdit();
            this.chkcmbShipLine = new ICP.ReportCenter.UI.Comm.Controls.BusinessShipLineTreeCheckControl();
            this.labShipLine = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomer = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeekDate.Properties)).BeginInit();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSales.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
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
            // panel2
            // 
            this.panel2.Controls.Add(this.navBarControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 597);
            this.panel2.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarDate;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarDate,
            this.nbarBase});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(223, 782);
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarDate
            // 
            this.nbarDate.Caption = "业务时间";
            this.nbarDate.ControlContainer = this.navBarGroupBase;
            this.nbarDate.Expanded = true;
            this.nbarDate.GroupClientHeight = 126;
            this.nbarDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarDate.Name = "nbarDate";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.rdoDefault);
            this.navBarGroupBase.Controls.Add(this.dateTo);
            this.navBarGroupBase.Controls.Add(this.rdoCustomizing);
            this.navBarGroupBase.Controls.Add(this.dateFrom);
            this.navBarGroupBase.Controls.Add(this.labelControl1);
            this.navBarGroupBase.Controls.Add(this.labelControl2);
            this.navBarGroupBase.Controls.Add(this.cmbWeekDate);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(215, 124);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // rdoDefault
            // 
            this.rdoDefault.AutoSize = true;
            this.rdoDefault.Checked = true;
            this.rdoDefault.Location = new System.Drawing.Point(7, 16);
            this.rdoDefault.Name = "rdoDefault";
            this.rdoDefault.Size = new System.Drawing.Size(14, 13);
            this.rdoDefault.TabIndex = 221;
            this.rdoDefault.TabStop = true;
            this.rdoDefault.UseVisualStyleBackColor = true;
            // 
            // dateTo
            // 
            this.dateTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTo.EditValue = null;
            this.dateTo.Enabled = false;
            this.dateTo.Location = new System.Drawing.Point(54, 90);
            this.dateTo.Name = "dateTo";
            this.dateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTo.Size = new System.Drawing.Size(154, 21);
            this.dateTo.TabIndex = 217;
            // 
            // rdoCustomizing
            // 
            this.rdoCustomizing.AutoSize = true;
            this.rdoCustomizing.Location = new System.Drawing.Point(7, 42);
            this.rdoCustomizing.Name = "rdoCustomizing";
            this.rdoCustomizing.Size = new System.Drawing.Size(61, 18);
            this.rdoCustomizing.TabIndex = 220;
            this.rdoCustomizing.Text = "自定义";
            this.rdoCustomizing.UseVisualStyleBackColor = true;
            // 
            // dateFrom
            // 
            this.dateFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateFrom.EditValue = null;
            this.dateFrom.Enabled = false;
            this.dateFrom.Location = new System.Drawing.Point(54, 63);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateFrom.Size = new System.Drawing.Size(154, 21);
            this.dateFrom.TabIndex = 216;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(27, 92);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(12, 14);
            this.labelControl1.TabIndex = 218;
            this.labelControl1.Text = "到";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(27, 66);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(12, 14);
            this.labelControl2.TabIndex = 219;
            this.labelControl2.Text = "从";
            // 
            // cmbWeekDate
            // 
            this.cmbWeekDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWeekDate.Location = new System.Drawing.Point(29, 12);
            this.cmbWeekDate.Name = "cmbWeekDate";
            this.cmbWeekDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbWeekDate.Properties.Appearance.Options.UseBackColor = true;
            this.cmbWeekDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWeekDate.Size = new System.Drawing.Size(179, 21);
            this.cmbWeekDate.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbWeekDate.TabIndex = 215;
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.lblGroupType);
            this.navBarGroupControlContainer1.Controls.Add(this.rdoGroup);
            this.navBarGroupControlContainer1.Controls.Add(this.lblCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.trCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.lblSales);
            this.navBarGroupControlContainer1.Controls.Add(this.txtSales);
            this.navBarGroupControlContainer1.Controls.Add(this.chkcmbShipLine);
            this.navBarGroupControlContainer1.Controls.Add(this.labShipLine);
            this.navBarGroupControlContainer1.Controls.Add(this.txtCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.labCustomer);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(215, 327);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // lblGroupType
            // 
            this.lblGroupType.Location = new System.Drawing.Point(9, 131);
            this.lblGroupType.Name = "lblGroupType";
            this.lblGroupType.Size = new System.Drawing.Size(60, 14);
            this.lblGroupType.TabIndex = 225;
            this.lblGroupType.Text = "分组方式：";
            // 
            // rdoGroup
            // 
            this.rdoGroup.Location = new System.Drawing.Point(9, 151);
            this.rdoGroup.Name = "rdoGroup";
            this.rdoGroup.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "客户名称"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2", "口岸公司"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2", "POL"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("4", "POD"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("5", "Sales")});
            this.rdoGroup.Size = new System.Drawing.Size(85, 96);
            this.rdoGroup.TabIndex = 224;
            // 
            // lblCompany
            // 
            this.lblCompany.Location = new System.Drawing.Point(9, 106);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(48, 14);
            this.lblCompany.TabIndex = 211;
            this.lblCompany.Text = "口岸公司";
            // 
            // trCompany
            // 
            this.trCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trCompany.EditText = "";
            this.trCompany.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("trCompany.EditValue")));
            this.trCompany.Location = new System.Drawing.Point(66, 101);
            this.trCompany.Name = "trCompany";
            this.trCompany.ReadOnly = false;
            this.trCompany.ShowDepartment = false;
            this.trCompany.Size = new System.Drawing.Size(145, 21);
            this.trCompany.SplitString = ";";
            this.trCompany.TabIndex = 210;
            // 
            // lblSales
            // 
            this.lblSales.Location = new System.Drawing.Point(9, 73);
            this.lblSales.Name = "lblSales";
            this.lblSales.Size = new System.Drawing.Size(36, 14);
            this.lblSales.TabIndex = 209;
            this.lblSales.Text = "业务员";
            // 
            // txtSales
            // 
            this.txtSales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSales.Location = new System.Drawing.Point(66, 70);
            this.txtSales.Name = "txtSales";
            this.txtSales.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtSales.Size = new System.Drawing.Size(145, 21);
            this.txtSales.TabIndex = 208;
            // 
            // chkcmbShipLine
            // 
            this.chkcmbShipLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbShipLine.EditText = "";
            this.chkcmbShipLine.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("chkcmbShipLine.EditValue")));
            this.chkcmbShipLine.Location = new System.Drawing.Point(66, 40);
            this.chkcmbShipLine.Name = "chkcmbShipLine";
            this.chkcmbShipLine.ReadOnly = false;
            this.chkcmbShipLine.Size = new System.Drawing.Size(145, 21);
            this.chkcmbShipLine.SplitString = ",";
            this.chkcmbShipLine.TabIndex = 206;
            // 
            // labShipLine
            // 
            this.labShipLine.Location = new System.Drawing.Point(9, 43);
            this.labShipLine.Name = "labShipLine";
            this.labShipLine.Size = new System.Drawing.Size(24, 14);
            this.labShipLine.TabIndex = 207;
            this.labShipLine.Text = "航线";
            // 
            // txtCustomer
            // 
            this.txtCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomer.FinderName = "CustomerFinder";
            this.txtCustomer.Location = new System.Drawing.Point(66, 9);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCustomer.Size = new System.Drawing.Size(145, 21);
            this.txtCustomer.TabIndex = 4;
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(9, 12);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(24, 14);
            this.labCustomer.TabIndex = 12;
            this.labCustomer.Text = "客户";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "基本信息";
            this.nbarBase.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 329;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // ContainerVolumeTotalSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ContainerVolumeTotalSearchPart";
            this.Size = new System.Drawing.Size(240, 656);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeekDate.Properties)).EndInit();
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSales.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.XtraScrollableControl panel2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarDate;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit txtCustomer;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private ICP.ReportCenter.UI.Comm.Controls.BusinessShipLineTreeCheckControl chkcmbShipLine;
        private DevExpress.XtraEditors.LabelControl labShipLine;
        private System.Windows.Forms.RadioButton rdoDefault;
        private DevExpress.XtraEditors.DateEdit dateTo;
        private System.Windows.Forms.RadioButton rdoCustomizing;
        private DevExpress.XtraEditors.DateEdit dateFrom;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbWeekDate;
        private DevExpress.XtraEditors.LabelControl lblSales;
        private DevExpress.XtraEditors.ButtonEdit txtSales;
        private DevExpress.XtraEditors.LabelControl lblCompany;
        private Comm.Controls.CompanyTreeCheckControl trCompany;
        private DevExpress.XtraEditors.RadioGroup rdoGroup;
        private DevExpress.XtraEditors.LabelControl lblGroupType;
    }
}
