namespace ICP.ReportCenter.UI.FinanceReports
{
    partial class ExpenseAnalysisSheetSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpenseAnalysisSheetSearchPart));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.lblExpenseType = new DevExpress.XtraEditors.LabelControl();
            this.cmbExpenseType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.operationDatePart1 = new ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart();
            this.chkCheckGL = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbHappen = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbExpenseType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheckGL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHappen.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnSearch);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 462);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(197, 44);
            this.panelControl2.TabIndex = 7;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(56, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&R)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblExpenseType
            // 
            this.lblExpenseType.Location = new System.Drawing.Point(5, 128);
            this.lblExpenseType.Name = "lblExpenseType";
            this.lblExpenseType.Size = new System.Drawing.Size(48, 14);
            this.lblExpenseType.TabIndex = 50;
            this.lblExpenseType.Text = "费用类型";
            // 
            // cmbExpenseType
            // 
            this.cmbExpenseType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbExpenseType.Location = new System.Drawing.Point(60, 125);
            this.cmbExpenseType.Name = "cmbExpenseType";
            this.cmbExpenseType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbExpenseType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbExpenseType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbExpenseType.Size = new System.Drawing.Size(134, 21);
            this.cmbExpenseType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbExpenseType.TabIndex = 0;
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
            this.operationDatePart1.Size = new System.Drawing.Size(197, 122);
            this.operationDatePart1.TabIndex = 0;
            this.operationDatePart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("operationDatePart1.UsedMessages")));
            // 
            // chkCheckGL
            // 
            this.chkCheckGL.Location = new System.Drawing.Point(60, 177);
            this.chkCheckGL.Name = "chkCheckGL";
            this.chkCheckGL.Properties.Caption = "只显示二级科目";
            this.chkCheckGL.Size = new System.Drawing.Size(132, 19);
            this.chkCheckGL.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 155);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 54;
            this.labelControl1.Text = "发生类型";
            // 
            // cmbHappen
            // 
            this.cmbHappen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbHappen.Location = new System.Drawing.Point(60, 152);
            this.cmbHappen.Name = "cmbHappen";
            this.cmbHappen.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbHappen.Properties.Appearance.Options.UseBackColor = true;
            this.cmbHappen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHappen.Size = new System.Drawing.Size(134, 21);
            this.cmbHappen.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbHappen.TabIndex = 1;
            // 
            // ExpenseAnalysisSheetSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cmbHappen);
            this.Controls.Add(this.chkCheckGL);
            this.Controls.Add(this.operationDatePart1);
            this.Controls.Add(this.lblExpenseType);
            this.Controls.Add(this.cmbExpenseType);
            this.Controls.Add(this.panelControl2);
            this.Name = "ExpenseAnalysisSheetSearchPart";
            this.Size = new System.Drawing.Size(197, 506);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbExpenseType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheckGL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHappen.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.LabelControl lblExpenseType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbExpenseType;
        private Comm.Controls.OperationDateByMonthPart operationDatePart1;
        private DevExpress.XtraEditors.CheckEdit chkCheckGL;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbHappen;
    }
}
