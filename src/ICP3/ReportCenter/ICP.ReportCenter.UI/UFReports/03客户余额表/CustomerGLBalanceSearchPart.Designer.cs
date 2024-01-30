namespace ICP.ReportCenter.UI.UFReports
{
    partial class CustomerGLBalanceSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerGLBalanceSearchPart));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerID = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labGLCode = new DevExpress.XtraEditors.LabelControl();
            this.txtGLCode = new DevExpress.XtraEditors.ButtonEdit();
            this.labBalanceDirection = new DevExpress.XtraEditors.LabelControl();
            this.cmbBalanceDirection = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.chkcmbCompany = new ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl();
            this.lblReportFormat = new DevExpress.XtraEditors.LabelControl();
            this.cmbReportFormat = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.operationDatePart1 = new ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbCurrencyList = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGLCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBalanceDirection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportFormat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrencyList.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnSearch);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 452);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(197, 51);
            this.panelControl2.TabIndex = 8;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(43, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(87, 27);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&R)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(3, 127);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(24, 14);
            this.labCompany.TabIndex = 55;
            this.labCompany.Text = "公司";
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomerID.FinderName = "CustomerFinder";
            this.txtCustomerID.Location = new System.Drawing.Point(57, 151);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCustomerID.Size = new System.Drawing.Size(135, 21);
            this.txtCustomerID.TabIndex = 2;
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(3, 154);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(24, 14);
            this.labCustomer.TabIndex = 55;
            this.labCustomer.Text = "客户";
            // 
            // labGLCode
            // 
            this.labGLCode.Location = new System.Drawing.Point(3, 181);
            this.labGLCode.Name = "labGLCode";
            this.labGLCode.Size = new System.Drawing.Size(24, 14);
            this.labGLCode.TabIndex = 55;
            this.labGLCode.Text = "科目";
            // 
            // txtGLCode
            // 
            this.txtGLCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGLCode.Location = new System.Drawing.Point(57, 178);
            this.txtGLCode.Name = "txtGLCode";
            this.txtGLCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtGLCode.Size = new System.Drawing.Size(135, 21);
            this.txtGLCode.TabIndex = 4;
            // 
            // labBalanceDirection
            // 
            this.labBalanceDirection.Location = new System.Drawing.Point(9, 427);
            this.labBalanceDirection.Name = "labBalanceDirection";
            this.labBalanceDirection.Size = new System.Drawing.Size(48, 14);
            this.labBalanceDirection.TabIndex = 63;
            this.labBalanceDirection.Text = "余额方向";
            this.labBalanceDirection.Visible = false;
            // 
            // cmbBalanceDirection
            // 
            this.cmbBalanceDirection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBalanceDirection.Location = new System.Drawing.Point(65, 425);
            this.cmbBalanceDirection.Name = "cmbBalanceDirection";
            this.cmbBalanceDirection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBalanceDirection.Size = new System.Drawing.Size(121, 21);
            this.cmbBalanceDirection.TabIndex = 300;
            this.cmbBalanceDirection.Visible = false;
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.EditText = "";
            this.chkcmbCompany.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("chkcmbCompany.EditValue")));
            this.chkcmbCompany.Location = new System.Drawing.Point(57, 124);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.ReadOnly = false;
            this.chkcmbCompany.ShowDepartment = false;
            this.chkcmbCompany.Size = new System.Drawing.Size(135, 21);
            this.chkcmbCompany.SplitString = ",";
            this.chkcmbCompany.TabIndex = 1;
            // 
            // lblReportFormat
            // 
            this.lblReportFormat.Location = new System.Drawing.Point(3, 208);
            this.lblReportFormat.Name = "lblReportFormat";
            this.lblReportFormat.Size = new System.Drawing.Size(48, 14);
            this.lblReportFormat.TabIndex = 65;
            this.lblReportFormat.Text = "报表格式";
            // 
            // cmbReportFormat
            // 
            this.cmbReportFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbReportFormat.Location = new System.Drawing.Point(57, 205);
            this.cmbReportFormat.Name = "cmbReportFormat";
            this.cmbReportFormat.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbReportFormat.Properties.Appearance.Options.UseBackColor = true;
            this.cmbReportFormat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReportFormat.Size = new System.Drawing.Size(135, 21);
            this.cmbReportFormat.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbReportFormat.TabIndex = 5;
            this.cmbReportFormat.SelectedIndexChanged += new System.EventHandler(this.cmbReportFormat_SelectedIndexChanged);
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
            this.operationDatePart1.Size = new System.Drawing.Size(197, 120);
            this.operationDatePart1.TabIndex = 0;
            this.operationDatePart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("operationDatePart1.UsedMessages")));
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 235);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 302;
            this.labelControl1.Text = "币种";
            // 
            // cmbCurrencyList
            // 
            this.cmbCurrencyList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCurrencyList.Location = new System.Drawing.Point(57, 232);
            this.cmbCurrencyList.Name = "cmbCurrencyList";
            this.cmbCurrencyList.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCurrencyList.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCurrencyList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrencyList.Size = new System.Drawing.Size(135, 21);
            this.cmbCurrencyList.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCurrencyList.TabIndex = 303;
            // 
            // CustomerGLBalanceSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbCurrencyList);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.operationDatePart1);
            this.Controls.Add(this.lblReportFormat);
            this.Controls.Add(this.cmbReportFormat);
            this.Controls.Add(this.chkcmbCompany);
            this.Controls.Add(this.cmbBalanceDirection);
            this.Controls.Add(this.labBalanceDirection);
            this.Controls.Add(this.txtGLCode);
            this.Controls.Add(this.txtCustomerID);
            this.Controls.Add(this.labGLCode);
            this.Controls.Add(this.labCustomer);
            this.Controls.Add(this.labCompany);
            this.Controls.Add(this.panelControl2);
            this.Name = "CustomerGLBalanceSearchPart";
            this.Size = new System.Drawing.Size(197, 503);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGLCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBalanceDirection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportFormat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrencyList.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit txtCustomerID;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.LabelControl labGLCode;
        private DevExpress.XtraEditors.ButtonEdit txtGLCode;
        private DevExpress.XtraEditors.LabelControl labBalanceDirection;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbBalanceDirection;
        private ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl chkcmbCompany;
        private DevExpress.XtraEditors.LabelControl lblReportFormat;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbReportFormat;
        private Comm.Controls.OperationDateByMonthPart operationDatePart1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCurrencyList;
    }
}
