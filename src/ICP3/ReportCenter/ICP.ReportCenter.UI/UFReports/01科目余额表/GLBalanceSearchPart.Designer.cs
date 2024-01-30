namespace ICP.ReportCenter.UI.UFReports
{
    partial class GLBalanceSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GLBalanceSearchPart));
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labFromGLCode = new DevExpress.XtraEditors.LabelControl();
            this.labToGLCode = new DevExpress.XtraEditors.LabelControl();
            this.txtFromGLCode = new DevExpress.XtraEditors.ButtonEdit();
            this.txtToGLCode = new DevExpress.XtraEditors.ButtonEdit();
            this.labGlCodeType = new DevExpress.XtraEditors.LabelControl();
            this.cmbGlCodeType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labFromGLLevel = new DevExpress.XtraEditors.LabelControl();
            this.numFromGLLevel = new DevExpress.XtraEditors.SpinEdit();
            this.numToGLLevel = new DevExpress.XtraEditors.SpinEdit();
            this.labToGLLevel = new DevExpress.XtraEditors.LabelControl();
            this.chkShowEndLevel = new DevExpress.XtraEditors.CheckEdit();
            this.chkShowCumulation = new DevExpress.XtraEditors.CheckEdit();
            this.pnlGL = new DevExpress.XtraEditors.PanelControl();
            this.chkcmbCompany = new ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblReportFormat = new DevExpress.XtraEditors.LabelControl();
            this.cmbReportFormat = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.operationDatePart1 = new ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbCurrencyList = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromGLCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToGLCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGlCodeType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromGLLevel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToGLLevel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowEndLevel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowCumulation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGL)).BeginInit();
            this.pnlGL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportFormat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrencyList.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(3, 7);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(24, 14);
            this.labCompany.TabIndex = 35;
            this.labCompany.Text = "公司";
            // 
            // labFromGLCode
            // 
            this.labFromGLCode.Location = new System.Drawing.Point(3, 33);
            this.labFromGLCode.Name = "labFromGLCode";
            this.labFromGLCode.Size = new System.Drawing.Size(36, 14);
            this.labFromGLCode.TabIndex = 7;
            this.labFromGLCode.Text = "科目从";
            // 
            // labToGLCode
            // 
            this.labToGLCode.Location = new System.Drawing.Point(3, 61);
            this.labToGLCode.Name = "labToGLCode";
            this.labToGLCode.Size = new System.Drawing.Size(12, 14);
            this.labToGLCode.TabIndex = 35;
            this.labToGLCode.Text = "到";
            // 
            // txtFromGLCode
            // 
            this.txtFromGLCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFromGLCode.Location = new System.Drawing.Point(63, 30);
            this.txtFromGLCode.Name = "txtFromGLCode";
            this.txtFromGLCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtFromGLCode.Size = new System.Drawing.Size(155, 21);
            this.txtFromGLCode.TabIndex = 2;
            // 
            // txtToGLCode
            // 
            this.txtToGLCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToGLCode.Location = new System.Drawing.Point(63, 58);
            this.txtToGLCode.Name = "txtToGLCode";
            this.txtToGLCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtToGLCode.Size = new System.Drawing.Size(155, 21);
            this.txtToGLCode.TabIndex = 3;
            // 
            // labGlCodeType
            // 
            this.labGlCodeType.Location = new System.Drawing.Point(3, 89);
            this.labGlCodeType.Name = "labGlCodeType";
            this.labGlCodeType.Size = new System.Drawing.Size(48, 14);
            this.labGlCodeType.TabIndex = 35;
            this.labGlCodeType.Text = "科目类型";
            // 
            // cmbGlCodeType
            // 
            this.cmbGlCodeType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGlCodeType.Location = new System.Drawing.Point(63, 86);
            this.cmbGlCodeType.Name = "cmbGlCodeType";
            this.cmbGlCodeType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbGlCodeType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbGlCodeType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGlCodeType.Size = new System.Drawing.Size(155, 21);
            this.cmbGlCodeType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbGlCodeType.TabIndex = 4;
            // 
            // labFromGLLevel
            // 
            this.labFromGLLevel.Location = new System.Drawing.Point(3, 117);
            this.labFromGLLevel.Name = "labFromGLLevel";
            this.labFromGLLevel.Size = new System.Drawing.Size(48, 14);
            this.labFromGLLevel.TabIndex = 35;
            this.labFromGLLevel.Text = "科目级次";
            // 
            // numFromGLLevel
            // 
            this.numFromGLLevel.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFromGLLevel.Location = new System.Drawing.Point(63, 114);
            this.numFromGLLevel.Name = "numFromGLLevel";
            this.numFromGLLevel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numFromGLLevel.Size = new System.Drawing.Size(65, 21);
            this.numFromGLLevel.TabIndex = 5;
            // 
            // numToGLLevel
            // 
            this.numToGLLevel.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numToGLLevel.Location = new System.Drawing.Point(152, 114);
            this.numToGLLevel.Name = "numToGLLevel";
            this.numToGLLevel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numToGLLevel.Size = new System.Drawing.Size(65, 21);
            this.numToGLLevel.TabIndex = 6;
            // 
            // labToGLLevel
            // 
            this.labToGLLevel.Location = new System.Drawing.Point(135, 117);
            this.labToGLLevel.Name = "labToGLLevel";
            this.labToGLLevel.Size = new System.Drawing.Size(11, 14);
            this.labToGLLevel.TabIndex = 4;
            this.labToGLLevel.Text = "—";
            // 
            // chkShowEndLevel
            // 
            this.chkShowEndLevel.Location = new System.Drawing.Point(65, 140);
            this.chkShowEndLevel.Name = "chkShowEndLevel";
            this.chkShowEndLevel.Properties.Caption = "只显示末级科目";
            this.chkShowEndLevel.Size = new System.Drawing.Size(147, 19);
            this.chkShowEndLevel.TabIndex = 7;
            this.chkShowEndLevel.CheckedChanged += new System.EventHandler(this.chkShowEndLevel_CheckedChanged);
            // 
            // chkShowCumulation
            // 
            this.chkShowCumulation.EditValue = true;
            this.chkShowCumulation.Location = new System.Drawing.Point(6, 287);
            this.chkShowCumulation.Name = "chkShowCumulation";
            this.chkShowCumulation.Properties.Caption = "本期无发生额，累计有发生显示";
            this.chkShowCumulation.Size = new System.Drawing.Size(206, 19);
            this.chkShowCumulation.TabIndex = 8;
            // 
            // pnlGL
            // 
            this.pnlGL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGL.Controls.Add(this.txtToGLCode);
            this.pnlGL.Controls.Add(this.labToGLLevel);
            this.pnlGL.Controls.Add(this.labFromGLCode);
            this.pnlGL.Controls.Add(this.chkcmbCompany);
            this.pnlGL.Controls.Add(this.chkShowEndLevel);
            this.pnlGL.Controls.Add(this.labToGLCode);
            this.pnlGL.Controls.Add(this.numToGLLevel);
            this.pnlGL.Controls.Add(this.labCompany);
            this.pnlGL.Controls.Add(this.labGlCodeType);
            this.pnlGL.Controls.Add(this.numFromGLLevel);
            this.pnlGL.Controls.Add(this.labFromGLLevel);
            this.pnlGL.Controls.Add(this.cmbGlCodeType);
            this.pnlGL.Controls.Add(this.txtFromGLCode);
            this.pnlGL.Location = new System.Drawing.Point(0, 124);
            this.pnlGL.Name = "pnlGL";
            this.pnlGL.Size = new System.Drawing.Size(222, 161);
            this.pnlGL.TabIndex = 3;
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.EditText = "";
            this.chkcmbCompany.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("chkcmbCompany.EditValue")));
            this.chkcmbCompany.Location = new System.Drawing.Point(63, 4);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.ReadOnly = false;
            this.chkcmbCompany.ShowDepartment = false;
            this.chkcmbCompany.Size = new System.Drawing.Size(154, 21);
            this.chkcmbCompany.SplitString = ",";
            this.chkcmbCompany.TabIndex = 1;
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
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnSearch);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 462);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(225, 44);
            this.panelControl2.TabIndex = 6;
            // 
            // lblReportFormat
            // 
            this.lblReportFormat.Location = new System.Drawing.Point(6, 311);
            this.lblReportFormat.Name = "lblReportFormat";
            this.lblReportFormat.Size = new System.Drawing.Size(48, 14);
            this.lblReportFormat.TabIndex = 37;
            this.lblReportFormat.Text = "报表格式";
            // 
            // cmbReportFormat
            // 
            this.cmbReportFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbReportFormat.Location = new System.Drawing.Point(68, 308);
            this.cmbReportFormat.Name = "cmbReportFormat";
            this.cmbReportFormat.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbReportFormat.Properties.Appearance.Options.UseBackColor = true;
            this.cmbReportFormat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReportFormat.Size = new System.Drawing.Size(148, 21);
            this.cmbReportFormat.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbReportFormat.TabIndex = 9;
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
            this.operationDatePart1.Size = new System.Drawing.Size(225, 122);
            this.operationDatePart1.TabIndex = 0;
            this.operationDatePart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("operationDatePart1.UsedMessages")));
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 335);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 38;
            this.labelControl1.Text = "币种";
            // 
            // cmbCurrencyList
            // 
            this.cmbCurrencyList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCurrencyList.Location = new System.Drawing.Point(68, 335);
            this.cmbCurrencyList.Name = "cmbCurrencyList";
            this.cmbCurrencyList.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCurrencyList.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCurrencyList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrencyList.Size = new System.Drawing.Size(148, 21);
            this.cmbCurrencyList.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCurrencyList.TabIndex = 39;
            this.cmbCurrencyList.Visible = false;
            // 
            // GLBalanceSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbCurrencyList);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.operationDatePart1);
            this.Controls.Add(this.lblReportFormat);
            this.Controls.Add(this.cmbReportFormat);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.pnlGL);
            this.Controls.Add(this.chkShowCumulation);
            this.Name = "GLBalanceSearchPart";
            this.Size = new System.Drawing.Size(225, 506);
            ((System.ComponentModel.ISupportInitialize)(this.txtFromGLCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToGLCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGlCodeType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromGLLevel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToGLLevel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowEndLevel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowCumulation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGL)).EndInit();
            this.pnlGL.ResumeLayout(false);
            this.pnlGL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportFormat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrencyList.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraEditors.LabelControl labFromGLCode;
        private DevExpress.XtraEditors.LabelControl labToGLCode;
        private DevExpress.XtraEditors.ButtonEdit txtFromGLCode;
        private DevExpress.XtraEditors.ButtonEdit txtToGLCode;
        private DevExpress.XtraEditors.LabelControl labGlCodeType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbGlCodeType;
        private DevExpress.XtraEditors.LabelControl labFromGLLevel;
        private DevExpress.XtraEditors.SpinEdit numFromGLLevel;
        private DevExpress.XtraEditors.SpinEdit numToGLLevel;
        private DevExpress.XtraEditors.LabelControl labToGLLevel;
        private DevExpress.XtraEditors.CheckEdit chkShowEndLevel;
        private DevExpress.XtraEditors.CheckEdit chkShowCumulation;
        private DevExpress.XtraEditors.PanelControl pnlGL;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl chkcmbCompany;
        private DevExpress.XtraEditors.LabelControl lblReportFormat;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbReportFormat;
        private Comm.Controls.OperationDateByMonthPart operationDatePart1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCurrencyList;
    }
}
