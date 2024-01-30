namespace ICP.ReportCenter.UI.CRMReports
{
   partial class JobInformationSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobInformationSearchPart));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.operationDatePart1 = new ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.treeBoxSalesDep = new ICP.Framework.ClientComponents.Controls.TreeCheckControl();
            this.reportOperationTypePart1 = new ICP.ReportCenter.UI.Comm.Controls.ReportOperationTypePart();
            this.txtCustomer = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.txtSales = new DevExpress.XtraEditors.ButtonEdit();
            this.labUSD = new DevExpress.XtraEditors.LabelControl();
            this.labSalesDep = new DevExpress.XtraEditors.LabelControl();
            this.labCurrency = new DevExpress.XtraEditors.LabelControl();
            this.labOperation = new DevExpress.XtraEditors.LabelControl();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labSales = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.rdoLoss = new System.Windows.Forms.RadioButton();
            this.rdoProfit = new System.Windows.Forms.RadioButton();
            this.rdoAll = new System.Windows.Forms.RadioButton();
            this.cmbSearchType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.txtSearchText = new DevExpress.XtraEditors.TextEdit();
            this.labSearch = new DevExpress.XtraEditors.LabelControl();
            this.labOn = new DevExpress.XtraEditors.LabelControl();
            this.labSearchText = new DevExpress.XtraEditors.LabelControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navOther = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSales.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSearchType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchText.Properties)).BeginInit();
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
            this.navBarControl1.Size = new System.Drawing.Size(240, 499);
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarDate
            // 
            this.nbarDate.Caption = "业务时间";
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
            this.navBarGroupControlContainer1.Controls.Add(this.treeBoxSalesDep);
            this.navBarGroupControlContainer1.Controls.Add(this.reportOperationTypePart1);
            this.navBarGroupControlContainer1.Controls.Add(this.txtCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.txtSales);
            this.navBarGroupControlContainer1.Controls.Add(this.labUSD);
            this.navBarGroupControlContainer1.Controls.Add(this.labSalesDep);
            this.navBarGroupControlContainer1.Controls.Add(this.labCurrency);
            this.navBarGroupControlContainer1.Controls.Add(this.labOperation);
            this.navBarGroupControlContainer1.Controls.Add(this.labCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.labSales);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(232, 154);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // treeBoxSalesDep
            // 
            this.treeBoxSalesDep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeBoxSalesDep.EditText = "";
            this.treeBoxSalesDep.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("treeBoxSalesDep.EditValue")));
            this.treeBoxSalesDep.Location = new System.Drawing.Point(7, 105);
            this.treeBoxSalesDep.Name = "treeBoxSalesDep";
            this.treeBoxSalesDep.ReadOnly = false;
            this.treeBoxSalesDep.Size = new System.Drawing.Size(220, 21);
            this.treeBoxSalesDep.SplitString = ",";
            this.treeBoxSalesDep.TabIndex = 4;
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
            this.reportOperationTypePart1.Location = new System.Drawing.Point(66, 3);
            this.reportOperationTypePart1.Name = "reportOperationTypePart1";
            this.reportOperationTypePart1.Resources = null;
            this.reportOperationTypePart1.Size = new System.Drawing.Size(163, 21);
            this.reportOperationTypePart1.SplitString = ",";
            this.reportOperationTypePart1.TabIndex = 0;
            this.reportOperationTypePart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("reportOperationTypePart1.UsedMessages")));
            // 
            // txtCustomer
            // 
            this.txtCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomer.FinderName = "CustomerFinder";
            this.txtCustomer.Location = new System.Drawing.Point(66, 57);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCustomer.Size = new System.Drawing.Size(162, 21);
            this.txtCustomer.TabIndex = 6;
            // 
            // txtSales
            // 
            this.txtSales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSales.Location = new System.Drawing.Point(66, 30);
            this.txtSales.Name = "txtSales";
            this.txtSales.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtSales.Size = new System.Drawing.Size(162, 21);
            this.txtSales.TabIndex = 5;
            // 
            // labUSD
            // 
            this.labUSD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labUSD.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labUSD.Appearance.ForeColor = System.Drawing.Color.Maroon;
            this.labUSD.Appearance.Options.UseFont = true;
            this.labUSD.Appearance.Options.UseForeColor = true;
            this.labUSD.Location = new System.Drawing.Point(174, 132);
            this.labUSD.Name = "labUSD";
            this.labUSD.Size = new System.Drawing.Size(25, 14);
            this.labUSD.TabIndex = 10;
            this.labUSD.Text = "USD";
            // 
            // labSalesDep
            // 
            this.labSalesDep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labSalesDep.Location = new System.Drawing.Point(9, 85);
            this.labSalesDep.Name = "labSalesDep";
            this.labSalesDep.Size = new System.Drawing.Size(84, 14);
            this.labSalesDep.TabIndex = 9;
            this.labSalesDep.Text = "业务员所属部门";
            // 
            // labCurrency
            // 
            this.labCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labCurrency.Location = new System.Drawing.Point(104, 132);
            this.labCurrency.Name = "labCurrency";
            this.labCurrency.Size = new System.Drawing.Size(52, 14);
            this.labCurrency.TabIndex = 9;
            this.labCurrency.Text = "折合币种:";
            // 
            // labOperation
            // 
            this.labOperation.Location = new System.Drawing.Point(9, 6);
            this.labOperation.Name = "labOperation";
            this.labOperation.Size = new System.Drawing.Size(48, 14);
            this.labOperation.TabIndex = 14;
            this.labOperation.Text = "业务类型";
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(9, 60);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(24, 14);
            this.labCustomer.TabIndex = 12;
            this.labCustomer.Text = "客户";
            // 
            // labSales
            // 
            this.labSales.Location = new System.Drawing.Point(9, 33);
            this.labSales.Name = "labSales";
            this.labSales.Size = new System.Drawing.Size(36, 14);
            this.labSales.TabIndex = 12;
            this.labSales.Text = "业务员";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.rdoLoss);
            this.navBarGroupControlContainer2.Controls.Add(this.rdoProfit);
            this.navBarGroupControlContainer2.Controls.Add(this.rdoAll);
            this.navBarGroupControlContainer2.Controls.Add(this.cmbSearchType);
            this.navBarGroupControlContainer2.Controls.Add(this.txtSearchText);
            this.navBarGroupControlContainer2.Controls.Add(this.labSearch);
            this.navBarGroupControlContainer2.Controls.Add(this.labOn);
            this.navBarGroupControlContainer2.Controls.Add(this.labSearchText);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(232, 89);
            this.navBarGroupControlContainer2.TabIndex = 2;
            // 
            // rdoLoss
            // 
            this.rdoLoss.AutoSize = true;
            this.rdoLoss.Location = new System.Drawing.Point(120, 7);
            this.rdoLoss.Name = "rdoLoss";
            this.rdoLoss.Size = new System.Drawing.Size(49, 18);
            this.rdoLoss.TabIndex = 213;
            this.rdoLoss.Text = "亏损";
            this.rdoLoss.UseVisualStyleBackColor = true;
            // 
            // rdoProfit
            // 
            this.rdoProfit.AutoSize = true;
            this.rdoProfit.Location = new System.Drawing.Point(65, 7);
            this.rdoProfit.Name = "rdoProfit";
            this.rdoProfit.Size = new System.Drawing.Size(49, 18);
            this.rdoProfit.TabIndex = 213;
            this.rdoProfit.Text = "盈利";
            this.rdoProfit.UseVisualStyleBackColor = true;
            // 
            // rdoAll
            // 
            this.rdoAll.AutoSize = true;
            this.rdoAll.Checked = true;
            this.rdoAll.Location = new System.Drawing.Point(9, 7);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new System.Drawing.Size(49, 18);
            this.rdoAll.TabIndex = 213;
            this.rdoAll.TabStop = true;
            this.rdoAll.Text = "全部";
            this.rdoAll.UseVisualStyleBackColor = true;
            // 
            // cmbSearchType
            // 
            this.cmbSearchType.Location = new System.Drawing.Point(65, 31);
            this.cmbSearchType.Name = "cmbSearchType";
            this.cmbSearchType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSearchType.Size = new System.Drawing.Size(100, 21);
            this.cmbSearchType.TabIndex = 212;
            // 
            // txtSearchText
            // 
            this.txtSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchText.Location = new System.Drawing.Point(65, 58);
            this.txtSearchText.Name = "txtSearchText";
            this.txtSearchText.Size = new System.Drawing.Size(162, 21);
            this.txtSearchText.TabIndex = 7;
            // 
            // labSearch
            // 
            this.labSearch.Location = new System.Drawing.Point(171, 34);
            this.labSearch.Name = "labSearch";
            this.labSearch.Size = new System.Drawing.Size(24, 14);
            this.labSearch.TabIndex = 211;
            this.labSearch.Text = "检索";
            // 
            // labOn
            // 
            this.labOn.Location = new System.Drawing.Point(9, 34);
            this.labOn.Name = "labOn";
            this.labOn.Size = new System.Drawing.Size(12, 14);
            this.labOn.TabIndex = 211;
            this.labOn.Text = "按";
            // 
            // labSearchText
            // 
            this.labSearchText.Location = new System.Drawing.Point(7, 61);
            this.labSearchText.Name = "labSearchText";
            this.labSearchText.Size = new System.Drawing.Size(12, 14);
            this.labSearchText.TabIndex = 211;
            this.labSearchText.Text = "值";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "基本信息";
            this.nbarBase.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 156;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navOther
            // 
            this.navOther.Caption = "更多";
            this.navOther.ControlContainer = this.navBarGroupControlContainer2;
            this.navOther.Expanded = true;
            this.navOther.GroupClientHeight = 91;
            this.navOther.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navOther.Name = "navOther";
            // 
            // JobInformationSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "JobInformationSearchPart";
            this.Size = new System.Drawing.Size(240, 656);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSales.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSearchType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchText.Properties)).EndInit();
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
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit txtCustomer;
        private DevExpress.XtraEditors.ButtonEdit txtSales;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.LabelControl labSales;
        private DevExpress.XtraEditors.LabelControl labUSD;
        private DevExpress.XtraEditors.LabelControl labCurrency;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraEditors.TextEdit txtSearchText;
        private DevExpress.XtraEditors.LabelControl labSearchText;
        private ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart operationDatePart1;
        private ICP.ReportCenter.UI.Comm.Controls.ReportOperationTypePart reportOperationTypePart1;
        private ICP.Framework.ClientComponents.Controls.TreeCheckControl treeBoxSalesDep;
        private DevExpress.XtraEditors.LabelControl labSalesDep;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbSearchType;
        private DevExpress.XtraEditors.LabelControl labOn;
        private System.Windows.Forms.RadioButton rdoLoss;
        private System.Windows.Forms.RadioButton rdoProfit;
        private System.Windows.Forms.RadioButton rdoAll;
        private DevExpress.XtraEditors.LabelControl labSearch;
    }
}
