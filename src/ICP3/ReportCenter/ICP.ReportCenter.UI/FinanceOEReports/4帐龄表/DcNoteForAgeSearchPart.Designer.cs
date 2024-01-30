namespace ICP.ReportCenter.UI.FinanceOEReports
{
   partial class DcNoteForAgeSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DcNoteForAgeSearchPart));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.chkIsBalance = new DevExpress.XtraEditors.CheckEdit();
            this.dteTo = new DevExpress.XtraEditors.DateEdit();
            this.labETDTo = new DevExpress.XtraEditors.LabelControl();
            this.cmbViewType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labViewType = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cmbSalesType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labSalesType = new DevExpress.XtraEditors.LabelControl();
            this.chkcmbGroupBy = new ICP.ReportCenter.UI.Comm.Controls.CheckBoxComboBox();
            this.labGroupBy = new DevExpress.XtraEditors.LabelControl();
            this.rdoOperationOrgial = new System.Windows.Forms.RadioButton();
            this.rdoOperationDepartment = new System.Windows.Forms.RadioButton();
            this.treeBoxSalesDep = new ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl();
            this.labUSD = new DevExpress.XtraEditors.LabelControl();
            this.reportOperationTypePart1 = new ICP.ReportCenter.UI.Comm.Controls.ReportOperationTypePart();
            this.labCurrency = new DevExpress.XtraEditors.LabelControl();
            this.labOperation = new DevExpress.XtraEditors.LabelControl();
            this.txtBillTo = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.labBillTo = new DevExpress.XtraEditors.LabelControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.labDateUp = new DevExpress.XtraEditors.LabelControl();
            this.labAmountUp = new DevExpress.XtraEditors.LabelControl();
            this.txtDays = new DevExpress.XtraEditors.TextEdit();
            this.txtAmount = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsBalance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbViewType.Properties)).BeginInit();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalesType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).BeginInit();
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
            this.navBarControl1.Size = new System.Drawing.Size(240, 530);
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarDate
            // 
            this.nbarDate.Caption = "日期";
            this.nbarDate.ControlContainer = this.navBarGroupBase;
            this.nbarDate.Expanded = true;
            this.nbarDate.GroupClientHeight = 137;
            this.nbarDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarDate.Name = "nbarDate";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.labelControl3);
            this.navBarGroupBase.Controls.Add(this.txtAmount);
            this.navBarGroupBase.Controls.Add(this.txtDays);
            this.navBarGroupBase.Controls.Add(this.labAmountUp);
            this.navBarGroupBase.Controls.Add(this.labDateUp);
            this.navBarGroupBase.Controls.Add(this.chkIsBalance);
            this.navBarGroupBase.Controls.Add(this.dteTo);
            this.navBarGroupBase.Controls.Add(this.labETDTo);
            this.navBarGroupBase.Controls.Add(this.cmbViewType);
            this.navBarGroupBase.Controls.Add(this.labViewType);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(232, 135);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // chkIsBalance
            // 
            this.chkIsBalance.Location = new System.Drawing.Point(102, 113);
            this.chkIsBalance.Name = "chkIsBalance";
            this.chkIsBalance.Properties.Caption = "与应付相抵";
            this.chkIsBalance.Size = new System.Drawing.Size(86, 19);
            this.chkIsBalance.TabIndex = 2;
            // 
            // dteTo
            // 
            this.dteTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dteTo.EditValue = null;
            this.dteTo.Location = new System.Drawing.Point(72, 30);
            this.dteTo.Name = "dteTo";
            this.dteTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteTo.Size = new System.Drawing.Size(155, 21);
            this.dteTo.TabIndex = 1;
            // 
            // labETDTo
            // 
            this.labETDTo.Location = new System.Drawing.Point(7, 33);
            this.labETDTo.Name = "labETDTo";
            this.labETDTo.Size = new System.Drawing.Size(59, 14);
            this.labETDTo.TabIndex = 55;
            this.labETDTo.Text = "ETD截止至";
            // 
            // cmbViewType
            // 
            this.cmbViewType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbViewType.Location = new System.Drawing.Point(72, 3);
            this.cmbViewType.Name = "cmbViewType";
            this.cmbViewType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbViewType.Size = new System.Drawing.Size(155, 21);
            this.cmbViewType.TabIndex = 0;
            // 
            // labViewType
            // 
            this.labViewType.Location = new System.Drawing.Point(5, 6);
            this.labViewType.Name = "labViewType";
            this.labViewType.Size = new System.Drawing.Size(48, 14);
            this.labViewType.TabIndex = 14;
            this.labViewType.Text = "显示类型";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.cmbSalesType);
            this.navBarGroupControlContainer1.Controls.Add(this.labSalesType);
            this.navBarGroupControlContainer1.Controls.Add(this.chkcmbGroupBy);
            this.navBarGroupControlContainer1.Controls.Add(this.labGroupBy);
            this.navBarGroupControlContainer1.Controls.Add(this.rdoOperationOrgial);
            this.navBarGroupControlContainer1.Controls.Add(this.rdoOperationDepartment);
            this.navBarGroupControlContainer1.Controls.Add(this.treeBoxSalesDep);
            this.navBarGroupControlContainer1.Controls.Add(this.labUSD);
            this.navBarGroupControlContainer1.Controls.Add(this.reportOperationTypePart1);
            this.navBarGroupControlContainer1.Controls.Add(this.labCurrency);
            this.navBarGroupControlContainer1.Controls.Add(this.labOperation);
            this.navBarGroupControlContainer1.Controls.Add(this.txtBillTo);
            this.navBarGroupControlContainer1.Controls.Add(this.labBillTo);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(232, 252);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // cmbSalesType
            // 
            this.cmbSalesType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSalesType.Location = new System.Drawing.Point(72, 84);
            this.cmbSalesType.Name = "cmbSalesType";
            this.cmbSalesType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSalesType.Size = new System.Drawing.Size(155, 21);
            this.cmbSalesType.TabIndex = 4;
            // 
            // labSalesType
            // 
            this.labSalesType.Location = new System.Drawing.Point(7, 87);
            this.labSalesType.Name = "labSalesType";
            this.labSalesType.Size = new System.Drawing.Size(48, 14);
            this.labSalesType.TabIndex = 52;
            this.labSalesType.Text = "揽货方式";
            // 
            // chkcmbGroupBy
            // 
            this.chkcmbGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbGroupBy.Location = new System.Drawing.Point(72, 3);
            this.chkcmbGroupBy.Name = "chkcmbGroupBy";
            this.chkcmbGroupBy.NullText = "";
            this.chkcmbGroupBy.Size = new System.Drawing.Size(155, 21);
            this.chkcmbGroupBy.SplitText = ",";
            this.chkcmbGroupBy.TabIndex = 1;
            // 
            // labGroupBy
            // 
            this.labGroupBy.Location = new System.Drawing.Point(7, 4);
            this.labGroupBy.Name = "labGroupBy";
            this.labGroupBy.Size = new System.Drawing.Size(48, 14);
            this.labGroupBy.TabIndex = 0;
            this.labGroupBy.Text = "分组方式";
            // 
            // rdoOperationOrgial
            // 
            this.rdoOperationOrgial.AutoSize = true;
            this.rdoOperationOrgial.Checked = true;
            this.rdoOperationOrgial.Location = new System.Drawing.Point(7, 111);
            this.rdoOperationOrgial.Name = "rdoOperationOrgial";
            this.rdoOperationOrgial.Size = new System.Drawing.Size(85, 18);
            this.rdoOperationOrgial.TabIndex = 5;
            this.rdoOperationOrgial.TabStop = true;
            this.rdoOperationOrgial.Text = "业务发生地";
            this.rdoOperationOrgial.UseVisualStyleBackColor = true;
            // 
            // rdoOperationDepartment
            // 
            this.rdoOperationDepartment.AutoSize = true;
            this.rdoOperationDepartment.Location = new System.Drawing.Point(7, 132);
            this.rdoOperationDepartment.Name = "rdoOperationDepartment";
            this.rdoOperationDepartment.Size = new System.Drawing.Size(97, 18);
            this.rdoOperationDepartment.TabIndex = 6;
            this.rdoOperationDepartment.Text = "业务所属部门";
            this.rdoOperationDepartment.UseVisualStyleBackColor = true;
            // 
            // treeBoxSalesDep
            // 
            this.treeBoxSalesDep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeBoxSalesDep.EditText = "";
            this.treeBoxSalesDep.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("treeBoxSalesDep.EditValue")));
            this.treeBoxSalesDep.Location = new System.Drawing.Point(5, 156);
            this.treeBoxSalesDep.Name = "treeBoxSalesDep";
            this.treeBoxSalesDep.ReadOnly = false;
            this.treeBoxSalesDep.ShowDepartment = false;
            this.treeBoxSalesDep.Size = new System.Drawing.Size(220, 21);
            this.treeBoxSalesDep.SplitString = ",";
            this.treeBoxSalesDep.TabIndex = 7;
            // 
            // labUSD
            // 
            this.labUSD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labUSD.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labUSD.Appearance.ForeColor = System.Drawing.Color.Maroon;
            this.labUSD.Appearance.Options.UseFont = true;
            this.labUSD.Appearance.Options.UseForeColor = true;
            this.labUSD.Location = new System.Drawing.Point(188, 183);
            this.labUSD.Name = "labUSD";
            this.labUSD.Size = new System.Drawing.Size(25, 14);
            this.labUSD.TabIndex = 9;
            this.labUSD.Text = "USD";
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
            this.reportOperationTypePart1.Location = new System.Drawing.Point(72, 57);
            this.reportOperationTypePart1.Name = "reportOperationTypePart1";
            this.reportOperationTypePart1.Resources = null;
            this.reportOperationTypePart1.Size = new System.Drawing.Size(155, 21);
            this.reportOperationTypePart1.SplitString = ",";
            this.reportOperationTypePart1.TabIndex = 3;
            this.reportOperationTypePart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("reportOperationTypePart1.UsedMessages")));
            // 
            // labCurrency
            // 
            this.labCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labCurrency.Location = new System.Drawing.Point(118, 183);
            this.labCurrency.Name = "labCurrency";
            this.labCurrency.Size = new System.Drawing.Size(52, 14);
            this.labCurrency.TabIndex = 8;
            this.labCurrency.Text = "折合币种:";
            // 
            // labOperation
            // 
            this.labOperation.Location = new System.Drawing.Point(7, 60);
            this.labOperation.Name = "labOperation";
            this.labOperation.Size = new System.Drawing.Size(48, 14);
            this.labOperation.TabIndex = 14;
            this.labOperation.Text = "业务类型";
            // 
            // txtBillTo
            // 
            this.txtBillTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBillTo.FinderName = "CustomerFinder";
            this.txtBillTo.Location = new System.Drawing.Point(72, 30);
            this.txtBillTo.Name = "txtBillTo";
            this.txtBillTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtBillTo.Size = new System.Drawing.Size(155, 21);
            this.txtBillTo.TabIndex = 2;
            // 
            // labBillTo
            // 
            this.labBillTo.Location = new System.Drawing.Point(7, 33);
            this.labBillTo.Name = "labBillTo";
            this.labBillTo.Size = new System.Drawing.Size(48, 14);
            this.labBillTo.TabIndex = 12;
            this.labBillTo.Text = "往来单位";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "基本信息";
            this.nbarBase.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 254;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // labDateUp
            // 
            this.labDateUp.Location = new System.Drawing.Point(7, 62);
            this.labDateUp.Name = "labDateUp";
            this.labDateUp.Size = new System.Drawing.Size(48, 14);
            this.labDateUp.TabIndex = 56;
            this.labDateUp.Text = "日期大于";
            // 
            // labAmountUp
            // 
            this.labAmountUp.Location = new System.Drawing.Point(7, 90);
            this.labAmountUp.Name = "labAmountUp";
            this.labAmountUp.Size = new System.Drawing.Size(48, 14);
            this.labAmountUp.TabIndex = 57;
            this.labAmountUp.Text = "金额大于";
            // 
            // txtDays
            // 
            this.txtDays.Location = new System.Drawing.Point(72, 59);
            this.txtDays.Name = "txtDays";
            this.txtDays.Properties.MaxLength = 200;
            this.txtDays.Size = new System.Drawing.Size(132, 21);
            this.txtDays.TabIndex = 58;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(71, 86);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Properties.MaxLength = 200;
            this.txtAmount.Size = new System.Drawing.Size(155, 21);
            this.txtAmount.TabIndex = 59;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(210, 62);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(12, 14);
            this.labelControl3.TabIndex = 60;
            this.labelControl3.Text = "天";
            // 
            // DcNoteForAgeSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DcNoteForAgeSearchPart";
            this.Size = new System.Drawing.Size(240, 639);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsBalance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbViewType.Properties)).EndInit();
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalesType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).EndInit();
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
        private DevExpress.XtraEditors.LabelControl labOperation;
        private System.Windows.Forms.RadioButton rdoOperationDepartment;
        private System.Windows.Forms.RadioButton rdoOperationOrgial;
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit txtBillTo;
        private DevExpress.XtraEditors.LabelControl labBillTo;
        private ICP.ReportCenter.UI.Comm.Controls.ReportOperationTypePart reportOperationTypePart1;
        private ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl treeBoxSalesDep;
        private DevExpress.XtraEditors.LabelControl labUSD;
        private DevExpress.XtraEditors.LabelControl labCurrency;
        private ICP.ReportCenter.UI.Comm.Controls.CheckBoxComboBox chkcmbGroupBy;
        private DevExpress.XtraEditors.LabelControl labGroupBy;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbSalesType;
        private DevExpress.XtraEditors.LabelControl labSalesType;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbViewType;
        private DevExpress.XtraEditors.LabelControl labViewType;
        private DevExpress.XtraEditors.CheckEdit chkIsBalance;
        private DevExpress.XtraEditors.DateEdit dteTo;
        private DevExpress.XtraEditors.LabelControl labETDTo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtAmount;
        private DevExpress.XtraEditors.TextEdit txtDays;
        private DevExpress.XtraEditors.LabelControl labAmountUp;
        private DevExpress.XtraEditors.LabelControl labDateUp;
    }
}
