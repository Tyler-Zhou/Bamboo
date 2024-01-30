namespace ICP.ReportCenter.UI.FinanceOEReports
{
   partial class DebitNoteForProfitSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebitNoteForProfitSearchPart));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.operationDatePart1 = new ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.labCurrency2 = new DevExpress.XtraEditors.LabelControl();
            this.labCurrency = new DevExpress.XtraEditors.LabelControl();
            this.cmbIsAttach = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.txtChargingCode = new DevExpress.XtraEditors.ButtonEdit();
            this.txtBillTo = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.labChargingCode = new DevExpress.XtraEditors.LabelControl();
            this.labIsAttach = new DevExpress.XtraEditors.LabelControl();
            this.labBillTo = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.treeBoxSalesDep = new ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl();
            this.reportOperationTypePart1 = new ICP.ReportCenter.UI.Comm.Controls.ReportOperationTypePart();
            this.labOperation = new DevExpress.XtraEditors.LabelControl();
            this.rdoOperationOrgial = new System.Windows.Forms.RadioButton();
            this.txtSales = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.rdoOperationDepartment = new System.Windows.Forms.RadioButton();
            this.labSales = new DevExpress.XtraEditors.LabelControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navOther = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIsAttach.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChargingCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillTo.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSales.Properties)).BeginInit();
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
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarDate,
            this.nbarBase,
            this.navOther});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(240, 525);
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarDate
            // 
            this.nbarDate.Caption = "日期";
            this.nbarDate.ControlContainer = this.navBarGroupBase;
            this.nbarDate.Expanded = true;
            this.nbarDate.GroupClientHeight = 128;
            this.nbarDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarDate.Name = "nbarDate";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.operationDatePart1);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(232, 126);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // operationDatePart1
            // 
            this.operationDatePart1.BaseMultiLanguageList = null;
            this.operationDatePart1.BasePartList = null;
            this.operationDatePart1.CodeValuePairs = null;
            this.operationDatePart1.ControlsList = null;
            this.operationDatePart1.Dock = System.Windows.Forms.DockStyle.Top;
            this.operationDatePart1.FormName = "OperationDatePart";
            this.operationDatePart1.IsMultiLanguage = true;
            this.operationDatePart1.Location = new System.Drawing.Point(0, 0);
            this.operationDatePart1.Name = "operationDatePart1";
            this.operationDatePart1.Resources = null;
            this.operationDatePart1.Size = new System.Drawing.Size(232, 122);
            this.operationDatePart1.TabIndex = 0;
            this.operationDatePart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("operationDatePart1.UsedMessages")));
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.labCurrency2);
            this.navBarGroupControlContainer1.Controls.Add(this.labCurrency);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbIsAttach);
            this.navBarGroupControlContainer1.Controls.Add(this.txtChargingCode);
            this.navBarGroupControlContainer1.Controls.Add(this.txtBillTo);
            this.navBarGroupControlContainer1.Controls.Add(this.labChargingCode);
            this.navBarGroupControlContainer1.Controls.Add(this.labIsAttach);
            this.navBarGroupControlContainer1.Controls.Add(this.labBillTo);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(232, 108);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // labCurrency2
            // 
            this.labCurrency2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labCurrency2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labCurrency2.Appearance.ForeColor = System.Drawing.Color.Maroon;
            this.labCurrency2.Appearance.Options.UseFont = true;
            this.labCurrency2.Appearance.Options.UseForeColor = true;
            this.labCurrency2.Location = new System.Drawing.Point(177, 84);
            this.labCurrency2.Name = "labCurrency2";
            this.labCurrency2.Size = new System.Drawing.Size(28, 14);
            this.labCurrency2.TabIndex = 14;
            this.labCurrency2.Text = "RMB";
            // 
            // labCurrency
            // 
            this.labCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labCurrency.Location = new System.Drawing.Point(107, 84);
            this.labCurrency.Name = "labCurrency";
            this.labCurrency.Size = new System.Drawing.Size(52, 14);
            this.labCurrency.TabIndex = 13;
            this.labCurrency.Text = "折合币种:";
            // 
            // cmbIsAttach
            // 
            this.cmbIsAttach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbIsAttach.Location = new System.Drawing.Point(64, 57);
            this.cmbIsAttach.Name = "cmbIsAttach";
            this.cmbIsAttach.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbIsAttach.Size = new System.Drawing.Size(147, 21);
            this.cmbIsAttach.TabIndex = 4;
            // 
            // txtChargingCode
            // 
            this.txtChargingCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChargingCode.Location = new System.Drawing.Point(64, 30);
            this.txtChargingCode.Name = "txtChargingCode";
            this.txtChargingCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtChargingCode.Size = new System.Drawing.Size(147, 21);
            this.txtChargingCode.TabIndex = 3;
            // 
            // txtBillTo
            // 
            this.txtBillTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBillTo.FinderName = "CustomerFinder";
            this.txtBillTo.Location = new System.Drawing.Point(64, 3);
            this.txtBillTo.Name = "txtBillTo";
            this.txtBillTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtBillTo.Size = new System.Drawing.Size(147, 21);
            this.txtBillTo.TabIndex = 3;
            // 
            // labChargingCode
            // 
            this.labChargingCode.Location = new System.Drawing.Point(7, 33);
            this.labChargingCode.Name = "labChargingCode";
            this.labChargingCode.Size = new System.Drawing.Size(48, 14);
            this.labChargingCode.TabIndex = 12;
            this.labChargingCode.Text = "费用项目";
            // 
            // labIsAttach
            // 
            this.labIsAttach.Location = new System.Drawing.Point(7, 60);
            this.labIsAttach.Name = "labIsAttach";
            this.labIsAttach.Size = new System.Drawing.Size(48, 14);
            this.labIsAttach.TabIndex = 12;
            this.labIsAttach.Text = "是否改单";
            // 
            // labBillTo
            // 
            this.labBillTo.Location = new System.Drawing.Point(7, 6);
            this.labBillTo.Name = "labBillTo";
            this.labBillTo.Size = new System.Drawing.Size(48, 14);
            this.labBillTo.TabIndex = 12;
            this.labBillTo.Text = "往来单位";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.treeBoxSalesDep);
            this.navBarGroupControlContainer2.Controls.Add(this.reportOperationTypePart1);
            this.navBarGroupControlContainer2.Controls.Add(this.labOperation);
            this.navBarGroupControlContainer2.Controls.Add(this.rdoOperationOrgial);
            this.navBarGroupControlContainer2.Controls.Add(this.txtSales);
            this.navBarGroupControlContainer2.Controls.Add(this.rdoOperationDepartment);
            this.navBarGroupControlContainer2.Controls.Add(this.labSales);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(232, 130);
            this.navBarGroupControlContainer2.TabIndex = 2;
            // 
            // treeBoxSalesDep
            // 
            this.treeBoxSalesDep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeBoxSalesDep.EditText = "";
            this.treeBoxSalesDep.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("treeBoxSalesDep.EditValue")));
            this.treeBoxSalesDep.Location = new System.Drawing.Point(7, 102);
            this.treeBoxSalesDep.Name = "treeBoxSalesDep";
            this.treeBoxSalesDep.ReadOnly = false;
            this.treeBoxSalesDep.ShowDepartment = false;
            this.treeBoxSalesDep.Size = new System.Drawing.Size(204, 21);
            this.treeBoxSalesDep.SplitString = ",";
            this.treeBoxSalesDep.TabIndex = 2;
            // 
            // reportOperationTypePart1
            // 
            this.reportOperationTypePart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.reportOperationTypePart1.BaseMultiLanguageList = null;
            this.reportOperationTypePart1.BasePartList = null;
            this.reportOperationTypePart1.CodeValuePairs = null;
            this.reportOperationTypePart1.ControlsList = null;
            this.reportOperationTypePart1.FormName = "ReportOperationTypePart";
            this.reportOperationTypePart1.IsMultiLanguage = true;
            this.reportOperationTypePart1.Location = new System.Drawing.Point(64, 30);
            this.reportOperationTypePart1.Name = "reportOperationTypePart1";
            this.reportOperationTypePart1.Resources = null;
            this.reportOperationTypePart1.Size = new System.Drawing.Size(147, 21);
            this.reportOperationTypePart1.SplitString = ",";
            this.reportOperationTypePart1.TabIndex = 3;
            this.reportOperationTypePart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("reportOperationTypePart1.UsedMessages")));
            // 
            // labOperation
            // 
            this.labOperation.Location = new System.Drawing.Point(7, 33);
            this.labOperation.Name = "labOperation";
            this.labOperation.Size = new System.Drawing.Size(48, 14);
            this.labOperation.TabIndex = 14;
            this.labOperation.Text = "业务类型";
            // 
            // rdoOperationOrgial
            // 
            this.rdoOperationOrgial.AutoSize = true;
            this.rdoOperationOrgial.Checked = true;
            this.rdoOperationOrgial.Location = new System.Drawing.Point(9, 57);
            this.rdoOperationOrgial.Name = "rdoOperationOrgial";
            this.rdoOperationOrgial.Size = new System.Drawing.Size(85, 18);
            this.rdoOperationOrgial.TabIndex = 0;
            this.rdoOperationOrgial.TabStop = true;
            this.rdoOperationOrgial.Text = "业务发生地";
            this.rdoOperationOrgial.UseVisualStyleBackColor = true;
            // 
            // txtSales
            // 
            this.txtSales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSales.FinderName = "UserFinder";
            this.txtSales.Location = new System.Drawing.Point(64, 3);
            this.txtSales.Name = "txtSales";
            this.txtSales.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtSales.Size = new System.Drawing.Size(147, 21);
            this.txtSales.TabIndex = 2;
            // 
            // rdoOperationDepartment
            // 
            this.rdoOperationDepartment.AutoSize = true;
            this.rdoOperationDepartment.Location = new System.Drawing.Point(9, 81);
            this.rdoOperationDepartment.Name = "rdoOperationDepartment";
            this.rdoOperationDepartment.Size = new System.Drawing.Size(97, 18);
            this.rdoOperationDepartment.TabIndex = 1;
            this.rdoOperationDepartment.Text = "业务所属部门";
            this.rdoOperationDepartment.UseVisualStyleBackColor = true;
            // 
            // labSales
            // 
            this.labSales.Location = new System.Drawing.Point(7, 6);
            this.labSales.Name = "labSales";
            this.labSales.Size = new System.Drawing.Size(36, 14);
            this.labSales.TabIndex = 12;
            this.labSales.Text = "业务员";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "基本信息";
            this.nbarBase.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 110;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navOther
            // 
            this.navOther.Caption = "更多";
            this.navOther.ControlContainer = this.navBarGroupControlContainer2;
            this.navOther.Expanded = true;
            this.navOther.GroupClientHeight = 132;
            this.navOther.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navOther.Name = "navOther";
            // 
            // DebitNoteForProfitSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DebitNoteForProfitSearchPart";
            this.Size = new System.Drawing.Size(240, 656);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIsAttach.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChargingCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillTo.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSales.Properties)).EndInit();
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
        private DevExpress.XtraNavBar.NavBarGroup navOther;
        private DevExpress.XtraEditors.LabelControl labOperation;
        private System.Windows.Forms.RadioButton rdoOperationDepartment;
        private System.Windows.Forms.RadioButton rdoOperationOrgial;
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit txtBillTo;
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit txtSales;
        private DevExpress.XtraEditors.LabelControl labBillTo;
        private DevExpress.XtraEditors.LabelControl labSales;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbIsAttach;
        private DevExpress.XtraEditors.LabelControl labIsAttach;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart operationDatePart1;
        private ICP.ReportCenter.UI.Comm.Controls.ReportOperationTypePart reportOperationTypePart1;
        private ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl treeBoxSalesDep;
        private DevExpress.XtraEditors.ButtonEdit txtChargingCode;
        private DevExpress.XtraEditors.LabelControl labChargingCode;
        private DevExpress.XtraEditors.LabelControl labCurrency2;
        private DevExpress.XtraEditors.LabelControl labCurrency;
    }
}
