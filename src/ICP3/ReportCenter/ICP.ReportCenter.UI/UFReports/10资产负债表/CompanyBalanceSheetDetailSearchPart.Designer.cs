namespace ICP.ReportCenter.UI.FinanceReports
{
    partial class CompanyBalanceSheetDetailSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompanyBalanceSheetDetailSearchPart));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.chkcmbCompany = new ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl();
            this.operationDatePart1 = new ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart();
            this.chkTotal = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkTotal.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnSearch);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 462);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(190, 44);
            this.panelControl2.TabIndex = 7;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(61, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&R)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(3, 128);
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
            this.chkcmbCompany.Location = new System.Drawing.Point(61, 125);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.ReadOnly = false;
            this.chkcmbCompany.ShowDepartment = false;
            this.chkcmbCompany.Size = new System.Drawing.Size(126, 21);
            this.chkcmbCompany.SplitString = ",";
            this.chkcmbCompany.TabIndex = 1;
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
            this.operationDatePart1.Size = new System.Drawing.Size(190, 122);
            this.operationDatePart1.TabIndex = 0;
            this.operationDatePart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("operationDatePart1.UsedMessages")));
            // 
            // chkTotal
            // 
            this.chkTotal.Location = new System.Drawing.Point(59, 154);
            this.chkTotal.Name = "chkTotal";
            this.chkTotal.Properties.Caption = "只显示远东区汇总表";
            this.chkTotal.Size = new System.Drawing.Size(141, 19);
            this.chkTotal.TabIndex = 2;
            this.chkTotal.CheckedChanged += new System.EventHandler(this.chkTotal_CheckedChanged);
            // 
            // CompanyBalanceSheetDetailSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkTotal);
            this.Controls.Add(this.operationDatePart1);
            this.Controls.Add(this.chkcmbCompany);
            this.Controls.Add(this.labCompany);
            this.Controls.Add(this.panelControl2);
            this.Name = "CompanyBalanceSheetDetailSearchPart";
            this.Size = new System.Drawing.Size(190, 506);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkTotal.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl chkcmbCompany;
        private Comm.Controls.OperationDateByMonthPart operationDatePart1;
        private DevExpress.XtraEditors.CheckEdit chkTotal;
    }
}
