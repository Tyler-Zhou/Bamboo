namespace ICP.ReportCenter.UI.UFReports
{
    partial class Customer3ColumnGLBalanceSearchPart
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.chkNoAccounting = new DevExpress.XtraEditors.CheckEdit();
            this.chkcmbCompany = new ICP.ReportCenter.UI.Comm.Controls.CompanyCheckBoxComboBox();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labGLCode = new DevExpress.XtraEditors.LabelControl();
            this.txtGLCode = new DevExpress.XtraEditors.ButtonEdit();
            this.cmbYears = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labYears = new DevExpress.XtraEditors.LabelControl();
            this.txtCusomer = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkNoAccounting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGLCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYears.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCusomer.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnSearch);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 452);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(195, 51);
            this.panelControl2.TabIndex = 8;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(69, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(87, 27);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&R)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chkNoAccounting
            // 
            this.chkNoAccounting.EditValue = true;
            this.chkNoAccounting.Location = new System.Drawing.Point(43, 115);
            this.chkNoAccounting.Name = "chkNoAccounting";
            this.chkNoAccounting.Properties.Caption = "包含未记账凭证";
            this.chkNoAccounting.Size = new System.Drawing.Size(142, 19);
            this.chkNoAccounting.TabIndex = 4;
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.Location = new System.Drawing.Point(46, 7);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.NullText = "";
            this.chkcmbCompany.Size = new System.Drawing.Size(142, 21);
            this.chkcmbCompany.SplitText = ",";
            this.chkcmbCompany.TabIndex = 0;
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(10, 10);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(24, 14);
            this.labCompany.TabIndex = 55;
            this.labCompany.Text = "公司";
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(10, 36);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(24, 14);
            this.labCustomer.TabIndex = 55;
            this.labCustomer.Text = "客户";
            // 
            // labGLCode
            // 
            this.labGLCode.Location = new System.Drawing.Point(10, 63);
            this.labGLCode.Name = "labGLCode";
            this.labGLCode.Size = new System.Drawing.Size(24, 14);
            this.labGLCode.TabIndex = 55;
            this.labGLCode.Text = "科目";
            // 
            // txtGLCode
            // 
            this.txtGLCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGLCode.Location = new System.Drawing.Point(46, 60);
            this.txtGLCode.Name = "txtGLCode";
            this.txtGLCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtGLCode.Size = new System.Drawing.Size(142, 21);
            this.txtGLCode.TabIndex = 2;
            // 
            // cmbYears
            // 
            this.cmbYears.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbYears.Location = new System.Drawing.Point(46, 88);
            this.cmbYears.Name = "cmbYears";
            this.cmbYears.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbYears.Properties.Appearance.Options.UseBackColor = true;
            this.cmbYears.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbYears.Size = new System.Drawing.Size(142, 21);
            this.cmbYears.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbYears.TabIndex = 3;
            // 
            // labYears
            // 
            this.labYears.Location = new System.Drawing.Point(10, 91);
            this.labYears.Name = "labYears";
            this.labYears.Size = new System.Drawing.Size(24, 14);
            this.labYears.TabIndex = 63;
            this.labYears.Text = "年份";
            // 
            // txtCusomer
            // 
            this.txtCusomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCusomer.Location = new System.Drawing.Point(46, 34);
            this.txtCusomer.Name = "txtCusomer";
            this.txtCusomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCusomer.Size = new System.Drawing.Size(142, 21);
            this.txtCusomer.TabIndex = 1;
            // 
            // Customer3ColumnGLBalanceSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labYears);
            this.Controls.Add(this.cmbYears);
            this.Controls.Add(this.txtCusomer);
            this.Controls.Add(this.txtGLCode);
            this.Controls.Add(this.chkNoAccounting);
            this.Controls.Add(this.labGLCode);
            this.Controls.Add(this.chkcmbCompany);
            this.Controls.Add(this.labCustomer);
            this.Controls.Add(this.labCompany);
            this.Controls.Add(this.panelControl2);
            this.Name = "Customer3ColumnGLBalanceSearchPart";
            this.Size = new System.Drawing.Size(195, 503);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkNoAccounting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGLCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYears.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCusomer.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.CheckEdit chkNoAccounting;
        private ICP.ReportCenter.UI.Comm.Controls.CompanyCheckBoxComboBox chkcmbCompany;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.LabelControl labGLCode;
        private DevExpress.XtraEditors.ButtonEdit txtGLCode;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbYears;
        private DevExpress.XtraEditors.LabelControl labYears;
        private DevExpress.XtraEditors.ButtonEdit txtCusomer;
    }
}
