namespace ICP.ReportCenter.UI.BusinessReports
{
   partial class PorfitForTSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PorfitForTSearchPart));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.operationDatePart1 = new ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.ucBusinessOrganizationSelect = new ICP.ReportCenter.UI.Comm.Controls.UCBusinessOrganizationSelect();
            this.chkcmbShipLine = new ICP.ReportCenter.UI.Comm.Controls.BusinessShipLineTreeCheckControl();
            this.cmbGroupBy = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.txtContractNo = new DevExpress.XtraEditors.TextEdit();
            this.labContractNo = new DevExpress.XtraEditors.LabelControl();
            this.reportOperationTypePart1 = new ICP.ReportCenter.UI.Comm.Controls.ReportOperationTypePart();
            this.labAgent = new DevExpress.XtraEditors.LabelControl();
            this.txtCarrier = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.labShipLine = new DevExpress.XtraEditors.LabelControl();
            this.cmbSalesType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.txtAgent = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.txtSales = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.labUSD = new DevExpress.XtraEditors.LabelControl();
            this.labCurrency = new DevExpress.XtraEditors.LabelControl();
            this.labSalesType = new DevExpress.XtraEditors.LabelControl();
            this.labOperation = new DevExpress.XtraEditors.LabelControl();
            this.labSales = new DevExpress.XtraEditors.LabelControl();
            this.labGroupBy = new DevExpress.XtraEditors.LabelControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGroupBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalesType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgent.Properties)).BeginInit();
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
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarDate,
            this.nbarBase});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(240, 538);
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
            this.navBarGroupControlContainer1.Controls.Add(this.ucBusinessOrganizationSelect);
            this.navBarGroupControlContainer1.Controls.Add(this.chkcmbShipLine);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbGroupBy);
            this.navBarGroupControlContainer1.Controls.Add(this.txtContractNo);
            this.navBarGroupControlContainer1.Controls.Add(this.labContractNo);
            this.navBarGroupControlContainer1.Controls.Add(this.reportOperationTypePart1);
            this.navBarGroupControlContainer1.Controls.Add(this.labAgent);
            this.navBarGroupControlContainer1.Controls.Add(this.txtCarrier);
            this.navBarGroupControlContainer1.Controls.Add(this.labCarrier);
            this.navBarGroupControlContainer1.Controls.Add(this.labShipLine);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbSalesType);
            this.navBarGroupControlContainer1.Controls.Add(this.txtAgent);
            this.navBarGroupControlContainer1.Controls.Add(this.txtSales);
            this.navBarGroupControlContainer1.Controls.Add(this.labUSD);
            this.navBarGroupControlContainer1.Controls.Add(this.labCurrency);
            this.navBarGroupControlContainer1.Controls.Add(this.labSalesType);
            this.navBarGroupControlContainer1.Controls.Add(this.labOperation);
            this.navBarGroupControlContainer1.Controls.Add(this.labSales);
            this.navBarGroupControlContainer1.Controls.Add(this.labGroupBy);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(232, 324);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // ucBusinessOrganizationSelect
            // 
            this.ucBusinessOrganizationSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucBusinessOrganizationSelect.Location = new System.Drawing.Point(0, 52);
            this.ucBusinessOrganizationSelect.Name = "ucBusinessOrganizationSelect";
            this.ucBusinessOrganizationSelect.Size = new System.Drawing.Size(229, 80);
            this.ucBusinessOrganizationSelect.TabIndex = 2;
            // 
            // chkcmbShipLine
            // 
            this.chkcmbShipLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbShipLine.EditText = "";
            this.chkcmbShipLine.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("chkcmbShipLine.EditValue")));
            this.chkcmbShipLine.Location = new System.Drawing.Point(66, 191);
            this.chkcmbShipLine.Name = "chkcmbShipLine";
            this.chkcmbShipLine.ReadOnly = false;
            this.chkcmbShipLine.Size = new System.Drawing.Size(163, 21);
            this.chkcmbShipLine.SplitString = ",";
            this.chkcmbShipLine.TabIndex = 5;
            // 
            // cmbGroupBy
            // 
            this.cmbGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGroupBy.Location = new System.Drawing.Point(66, 28);
            this.cmbGroupBy.Name = "cmbGroupBy";
            this.cmbGroupBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGroupBy.Size = new System.Drawing.Size(163, 21);
            this.cmbGroupBy.TabIndex = 1;
            // 
            // txtContractNo
            // 
            this.txtContractNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContractNo.Location = new System.Drawing.Point(66, 272);
            this.txtContractNo.Name = "txtContractNo";
            this.txtContractNo.Size = new System.Drawing.Size(163, 21);
            this.txtContractNo.TabIndex = 8;
            // 
            // labContractNo
            // 
            this.labContractNo.Location = new System.Drawing.Point(9, 275);
            this.labContractNo.Name = "labContractNo";
            this.labContractNo.Size = new System.Drawing.Size(36, 14);
            this.labContractNo.TabIndex = 211;
            this.labContractNo.Text = "合约号";
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
            // labAgent
            // 
            this.labAgent.Location = new System.Drawing.Point(9, 166);
            this.labAgent.Name = "labAgent";
            this.labAgent.Size = new System.Drawing.Size(24, 14);
            this.labAgent.TabIndex = 211;
            this.labAgent.Text = "代理";
            // 
            // txtCarrier
            // 
            this.txtCarrier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCarrier.FinderName = "CustomerCarrierFinder";
            this.txtCarrier.Location = new System.Drawing.Point(66, 135);
            this.txtCarrier.Name = "txtCarrier";
            this.txtCarrier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCarrier.Size = new System.Drawing.Size(163, 21);
            this.txtCarrier.TabIndex = 3;
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(9, 138);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(36, 14);
            this.labCarrier.TabIndex = 209;
            this.labCarrier.Text = "船公司";
            // 
            // labShipLine
            // 
            this.labShipLine.Location = new System.Drawing.Point(9, 193);
            this.labShipLine.Name = "labShipLine";
            this.labShipLine.Size = new System.Drawing.Size(24, 14);
            this.labShipLine.TabIndex = 205;
            this.labShipLine.Text = "航线";
            // 
            // cmbSalesType
            // 
            this.cmbSalesType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSalesType.Location = new System.Drawing.Point(66, 245);
            this.cmbSalesType.Name = "cmbSalesType";
            this.cmbSalesType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSalesType.Size = new System.Drawing.Size(163, 21);
            this.cmbSalesType.TabIndex = 7;
            // 
            // txtAgent
            // 
            this.txtAgent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAgent.FinderName = "CustomerAgentFinder";
            this.txtAgent.Location = new System.Drawing.Point(66, 163);
            this.txtAgent.Name = "txtAgent";
            this.txtAgent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtAgent.Size = new System.Drawing.Size(163, 21);
            this.txtAgent.TabIndex = 4;
            // 
            // txtSales
            // 
            this.txtSales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSales.FinderName = "UserFinder";
            this.txtSales.Location = new System.Drawing.Point(66, 218);
            this.txtSales.Name = "txtSales";
            this.txtSales.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtSales.Size = new System.Drawing.Size(163, 21);
            this.txtSales.TabIndex = 6;
            // 
            // labUSD
            // 
            this.labUSD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labUSD.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labUSD.Appearance.ForeColor = System.Drawing.Color.Maroon;
            this.labUSD.Appearance.Options.UseFont = true;
            this.labUSD.Appearance.Options.UseForeColor = true;
            this.labUSD.Location = new System.Drawing.Point(191, 299);
            this.labUSD.Name = "labUSD";
            this.labUSD.Size = new System.Drawing.Size(25, 14);
            this.labUSD.TabIndex = 12;
            this.labUSD.Text = "USD";
            // 
            // labCurrency
            // 
            this.labCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labCurrency.Location = new System.Drawing.Point(121, 299);
            this.labCurrency.Name = "labCurrency";
            this.labCurrency.Size = new System.Drawing.Size(52, 14);
            this.labCurrency.TabIndex = 12;
            this.labCurrency.Text = "折合币种:";
            // 
            // labSalesType
            // 
            this.labSalesType.Location = new System.Drawing.Point(9, 248);
            this.labSalesType.Name = "labSalesType";
            this.labSalesType.Size = new System.Drawing.Size(48, 14);
            this.labSalesType.TabIndex = 12;
            this.labSalesType.Text = "揽货方式";
            // 
            // labOperation
            // 
            this.labOperation.Location = new System.Drawing.Point(9, 6);
            this.labOperation.Name = "labOperation";
            this.labOperation.Size = new System.Drawing.Size(48, 14);
            this.labOperation.TabIndex = 14;
            this.labOperation.Text = "业务类型";
            // 
            // labSales
            // 
            this.labSales.Location = new System.Drawing.Point(9, 221);
            this.labSales.Name = "labSales";
            this.labSales.Size = new System.Drawing.Size(36, 14);
            this.labSales.TabIndex = 12;
            this.labSales.Text = "业务员";
            // 
            // labGroupBy
            // 
            this.labGroupBy.Location = new System.Drawing.Point(9, 31);
            this.labGroupBy.Name = "labGroupBy";
            this.labGroupBy.Size = new System.Drawing.Size(48, 14);
            this.labGroupBy.TabIndex = 12;
            this.labGroupBy.Text = "分组方式";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "基本信息";
            this.nbarBase.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 326;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // PorfitForTSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "PorfitForTSearchPart";
            this.Size = new System.Drawing.Size(240, 656);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGroupBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalesType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgent.Properties)).EndInit();
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
        private DevExpress.XtraEditors.LabelControl labGroupBy;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraEditors.LabelControl labOperation;
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit txtSales;
        private DevExpress.XtraEditors.LabelControl labSales;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbSalesType;
        private DevExpress.XtraEditors.LabelControl labSalesType;
        private DevExpress.XtraEditors.LabelControl labUSD;
        private DevExpress.XtraEditors.LabelControl labCurrency;
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit txtCarrier;
        private DevExpress.XtraEditors.LabelControl labCarrier;
        private DevExpress.XtraEditors.LabelControl labShipLine;
        private DevExpress.XtraEditors.TextEdit txtContractNo;
        private DevExpress.XtraEditors.LabelControl labContractNo;
        private DevExpress.XtraEditors.LabelControl labAgent;
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit txtAgent;
        private ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart operationDatePart1;
        private ICP.ReportCenter.UI.Comm.Controls.ReportOperationTypePart reportOperationTypePart1;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbGroupBy;
        private ICP.ReportCenter.UI.Comm.Controls.BusinessShipLineTreeCheckControl chkcmbShipLine;
        private ICP.ReportCenter.UI.Comm.Controls.UCBusinessOrganizationSelect ucBusinessOrganizationSelect;
    }
}
