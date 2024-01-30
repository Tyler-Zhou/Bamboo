namespace ICP.ReportCenter.UI.UFReports
{
    partial class GLDetailDataSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GLDetailDataSearchPart));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labGLCode = new DevExpress.XtraEditors.LabelControl();
            this.txtGLCode = new DevExpress.XtraEditors.ButtonEdit();
            this.chkcmbCompany = new ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl();
            this.lblReportFormat = new DevExpress.XtraEditors.LabelControl();
            this.cmbReportFormat = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.operationDatePart1 = new ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGLCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportFormat.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnSearch);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 462);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(229, 44);
            this.panelControl2.TabIndex = 7;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(68, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&R)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(1, 129);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(24, 14);
            this.labCompany.TabIndex = 44;
            this.labCompany.Text = "公司";
            // 
            // labGLCode
            // 
            this.labGLCode.Location = new System.Drawing.Point(1, 156);
            this.labGLCode.Name = "labGLCode";
            this.labGLCode.Size = new System.Drawing.Size(24, 14);
            this.labGLCode.TabIndex = 51;
            this.labGLCode.Text = "科目";
            // 
            // txtGLCode
            // 
            this.txtGLCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGLCode.Location = new System.Drawing.Point(59, 153);
            this.txtGLCode.Name = "txtGLCode";
            this.txtGLCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtGLCode.Size = new System.Drawing.Size(167, 21);
            this.txtGLCode.TabIndex = 3;
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.EditText = "";
            this.chkcmbCompany.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("chkcmbCompany.EditValue")));
            this.chkcmbCompany.Location = new System.Drawing.Point(59, 126);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.ReadOnly = false;
            this.chkcmbCompany.ShowDepartment = false;
            this.chkcmbCompany.Size = new System.Drawing.Size(167, 21);
            this.chkcmbCompany.SplitString = ",";
            this.chkcmbCompany.TabIndex = 1;
            // 
            // lblReportFormat
            // 
            this.lblReportFormat.Location = new System.Drawing.Point(1, 183);
            this.lblReportFormat.Name = "lblReportFormat";
            this.lblReportFormat.Size = new System.Drawing.Size(48, 14);
            this.lblReportFormat.TabIndex = 53;
            this.lblReportFormat.Text = "报表格式";
            // 
            // cmbReportFormat
            // 
            this.cmbReportFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbReportFormat.Location = new System.Drawing.Point(59, 180);
            this.cmbReportFormat.Name = "cmbReportFormat";
            this.cmbReportFormat.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbReportFormat.Properties.Appearance.Options.UseBackColor = true;
            this.cmbReportFormat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReportFormat.Size = new System.Drawing.Size(167, 21);
            this.cmbReportFormat.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbReportFormat.TabIndex = 4;
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
            this.operationDatePart1.Size = new System.Drawing.Size(229, 120);
            this.operationDatePart1.TabIndex = 0;
            this.operationDatePart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("operationDatePart1.UsedMessages")));
            // 
            // GLDetailDataSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.operationDatePart1);
            this.Controls.Add(this.lblReportFormat);
            this.Controls.Add(this.cmbReportFormat);
            this.Controls.Add(this.chkcmbCompany);
            this.Controls.Add(this.labGLCode);
            this.Controls.Add(this.txtGLCode);
            this.Controls.Add(this.labCompany);
            this.Controls.Add(this.panelControl2);
            this.Name = "GLDetailDataSearchPart";
            this.Size = new System.Drawing.Size(229, 506);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtGLCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportFormat.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraEditors.LabelControl labGLCode;
        private DevExpress.XtraEditors.ButtonEdit txtGLCode;
        private ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl chkcmbCompany;
        private DevExpress.XtraEditors.LabelControl lblReportFormat;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbReportFormat;
        private Comm.Controls.OperationDateByMonthPart operationDatePart1;
    }
}
