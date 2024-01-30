namespace ICP.ReportCenter.UI.FinanceReports
{
    partial class ExpenseAnalysisSheetDetailSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpenseAnalysisSheetDetailSearchPart));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.chkcmbCompany = new ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl();
            this.lblExpenseType = new DevExpress.XtraEditors.LabelControl();
            this.cmbExpenseType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.operationDatePart1 = new ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart();
            this.chkCheckGL = new DevExpress.XtraEditors.CheckEdit();
            this.btnBudget = new DevExpress.XtraEditors.SimpleButton();
            this.chkTotal = new DevExpress.XtraEditors.CheckEdit();
            this.labHappenType = new DevExpress.XtraEditors.LabelControl();
            this.cmbHappen = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbExpenseType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheckGL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHappen.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnSearch);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 462);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(198, 44);
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
            this.labCompany.Location = new System.Drawing.Point(27, 129);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(24, 14);
            this.labCompany.TabIndex = 44;
            this.labCompany.Text = "公司";
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.EditText = "";
            this.chkcmbCompany.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("chkcmbCompany.EditValue")));
            this.chkcmbCompany.Location = new System.Drawing.Point(60, 126);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.ReadOnly = false;
            this.chkcmbCompany.ShowDepartment = false;
            this.chkcmbCompany.Size = new System.Drawing.Size(130, 21);
            this.chkcmbCompany.SplitString = ",";
            this.chkcmbCompany.TabIndex = 1;
            // 
            // lblExpenseType
            // 
            this.lblExpenseType.Location = new System.Drawing.Point(3, 154);
            this.lblExpenseType.Name = "lblExpenseType";
            this.lblExpenseType.Size = new System.Drawing.Size(48, 14);
            this.lblExpenseType.TabIndex = 50;
            this.lblExpenseType.Text = "费用类型";
            // 
            // cmbExpenseType
            // 
            this.cmbExpenseType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbExpenseType.Location = new System.Drawing.Point(60, 151);
            this.cmbExpenseType.Name = "cmbExpenseType";
            this.cmbExpenseType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbExpenseType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbExpenseType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbExpenseType.Size = new System.Drawing.Size(130, 21);
            this.cmbExpenseType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbExpenseType.TabIndex = 2;
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
            this.operationDatePart1.Size = new System.Drawing.Size(198, 122);
            this.operationDatePart1.TabIndex = 0;
            this.operationDatePart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("operationDatePart1.UsedMessages")));
            // 
            // chkCheckGL
            // 
            this.chkCheckGL.Location = new System.Drawing.Point(58, 176);
            this.chkCheckGL.Name = "chkCheckGL";
            this.chkCheckGL.Properties.Caption = "只显示二级科目";
            this.chkCheckGL.Size = new System.Drawing.Size(132, 19);
            this.chkCheckGL.TabIndex = 3;
            // 
            // btnBudget
            // 
            this.btnBudget.Location = new System.Drawing.Point(60, 201);
            this.btnBudget.Name = "btnBudget";
            this.btnBudget.Size = new System.Drawing.Size(130, 23);
            this.btnBudget.TabIndex = 4;
            this.btnBudget.Text = "设置月预算金额";
            this.btnBudget.Click += new System.EventHandler(this.btnBudget_Click);
            // 
            // chkTotal
            // 
            this.chkTotal.Location = new System.Drawing.Point(58, 230);
            this.chkTotal.Name = "chkTotal";
            this.chkTotal.Properties.Caption = "只显示远东区汇总表";
            this.chkTotal.Size = new System.Drawing.Size(141, 19);
            this.chkTotal.TabIndex = 5;
            this.chkTotal.CheckedChanged += new System.EventHandler(this.chkTotal_CheckedChanged);
            // 
            // labHappenType
            // 
            this.labHappenType.Location = new System.Drawing.Point(1, 254);
            this.labHappenType.Name = "labHappenType";
            this.labHappenType.Size = new System.Drawing.Size(48, 14);
            this.labHappenType.TabIndex = 56;
            this.labHappenType.Text = "发生类型";
            // 
            // cmbHappen
            // 
            this.cmbHappen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbHappen.Location = new System.Drawing.Point(60, 251);
            this.cmbHappen.Name = "cmbHappen";
            this.cmbHappen.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbHappen.Properties.Appearance.Options.UseBackColor = true;
            this.cmbHappen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHappen.Size = new System.Drawing.Size(130, 21);
            this.cmbHappen.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbHappen.TabIndex = 6;
            // 
            // ExpenseAnalysisSheetDetailSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labHappenType);
            this.Controls.Add(this.cmbHappen);
            this.Controls.Add(this.chkTotal);
            this.Controls.Add(this.btnBudget);
            this.Controls.Add(this.chkCheckGL);
            this.Controls.Add(this.operationDatePart1);
            this.Controls.Add(this.lblExpenseType);
            this.Controls.Add(this.cmbExpenseType);
            this.Controls.Add(this.chkcmbCompany);
            this.Controls.Add(this.labCompany);
            this.Controls.Add(this.panelControl2);
            this.Name = "ExpenseAnalysisSheetDetailSearchPart";
            this.Size = new System.Drawing.Size(198, 506);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbExpenseType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheckGL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHappen.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl chkcmbCompany;
        private DevExpress.XtraEditors.LabelControl lblExpenseType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbExpenseType;
        private Comm.Controls.OperationDateByMonthPart operationDatePart1;
        private DevExpress.XtraEditors.CheckEdit chkCheckGL;
        private DevExpress.XtraEditors.SimpleButton btnBudget;
        private DevExpress.XtraEditors.CheckEdit chkTotal;
        private DevExpress.XtraEditors.LabelControl labHappenType;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbHappen;
    }
}
