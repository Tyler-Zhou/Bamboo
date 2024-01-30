namespace ICP.ReportCenter.UI.FinanceOIReports
{
   partial class CheckDepositSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckDepositSearchPart));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.rdoAP = new System.Windows.Forms.RadioButton();
            this.rdoAll = new System.Windows.Forms.RadioButton();
            this.rdoAR = new System.Windows.Forms.RadioButton();
            this.cmbSortByType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labSortByType = new DevExpress.XtraEditors.LabelControl();
            this.cmbGroupBy = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labGroupBy = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cmbDateType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.dteFrom = new DevExpress.XtraEditors.DateEdit();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labDateType = new DevExpress.XtraEditors.LabelControl();
            this.dteTo = new DevExpress.XtraEditors.DateEdit();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.navBarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.treeBoxSalesDep = new ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSortByType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGroupBy.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 597);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(227, 59);
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
            this.panel2.Size = new System.Drawing.Size(227, 597);
            this.panel2.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase,
            this.navBarDate});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(227, 284);
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "基本信息";
            this.nbarBase.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 116;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.treeBoxSalesDep);
            this.navBarGroupControlContainer1.Controls.Add(this.rdoAP);
            this.navBarGroupControlContainer1.Controls.Add(this.rdoAll);
            this.navBarGroupControlContainer1.Controls.Add(this.rdoAR);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbSortByType);
            this.navBarGroupControlContainer1.Controls.Add(this.labSortByType);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbGroupBy);
            this.navBarGroupControlContainer1.Controls.Add(this.labGroupBy);
            this.navBarGroupControlContainer1.Controls.Add(this.labCompany);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(219, 114);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // rdoAP
            // 
            this.rdoAP.AutoSize = true;
            this.rdoAP.Checked = true;
            this.rdoAP.Location = new System.Drawing.Point(159, 33);
            this.rdoAP.Name = "rdoAP";
            this.rdoAP.Size = new System.Drawing.Size(49, 18);
            this.rdoAP.TabIndex = 3;
            this.rdoAP.TabStop = true;
            this.rdoAP.Text = "应付";
            this.rdoAP.UseVisualStyleBackColor = true;
            // 
            // rdoAll
            // 
            this.rdoAll.AutoSize = true;
            this.rdoAll.Location = new System.Drawing.Point(14, 33);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new System.Drawing.Size(49, 18);
            this.rdoAll.TabIndex = 1;
            this.rdoAll.Text = "全部";
            this.rdoAll.UseVisualStyleBackColor = true;
            // 
            // rdoAR
            // 
            this.rdoAR.AutoSize = true;
            this.rdoAR.Location = new System.Drawing.Point(84, 33);
            this.rdoAR.Name = "rdoAR";
            this.rdoAR.Size = new System.Drawing.Size(49, 18);
            this.rdoAR.TabIndex = 2;
            this.rdoAR.Text = "应收";
            this.rdoAR.UseVisualStyleBackColor = true;
            // 
            // cmbSortByType
            // 
            this.cmbSortByType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSortByType.Location = new System.Drawing.Point(70, 85);
            this.cmbSortByType.Name = "cmbSortByType";
            this.cmbSortByType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSortByType.Size = new System.Drawing.Size(135, 21);
            this.cmbSortByType.TabIndex = 5;
            // 
            // labSortByType
            // 
            this.labSortByType.Location = new System.Drawing.Point(7, 88);
            this.labSortByType.Name = "labSortByType";
            this.labSortByType.Size = new System.Drawing.Size(60, 14);
            this.labSortByType.TabIndex = 31;
            this.labSortByType.Text = "详细表排序";
            // 
            // cmbGroupBy
            // 
            this.cmbGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGroupBy.Location = new System.Drawing.Point(70, 57);
            this.cmbGroupBy.Name = "cmbGroupBy";
            this.cmbGroupBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGroupBy.Size = new System.Drawing.Size(135, 21);
            this.cmbGroupBy.TabIndex = 4;
            // 
            // labGroupBy
            // 
            this.labGroupBy.Location = new System.Drawing.Point(7, 60);
            this.labGroupBy.Name = "labGroupBy";
            this.labGroupBy.Size = new System.Drawing.Size(24, 14);
            this.labGroupBy.TabIndex = 31;
            this.labGroupBy.Text = "分组";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(7, 9);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(24, 14);
            this.labCompany.TabIndex = 31;
            this.labCompany.Text = "公司";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.cmbDateType);
            this.navBarGroupControlContainer2.Controls.Add(this.dteFrom);
            this.navBarGroupControlContainer2.Controls.Add(this.labTo);
            this.navBarGroupControlContainer2.Controls.Add(this.labDateType);
            this.navBarGroupControlContainer2.Controls.Add(this.dteTo);
            this.navBarGroupControlContainer2.Controls.Add(this.labFrom);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(219, 87);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // cmbDateType
            // 
            this.cmbDateType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDateType.Location = new System.Drawing.Point(70, 5);
            this.cmbDateType.Name = "cmbDateType";
            this.cmbDateType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDateType.Size = new System.Drawing.Size(135, 21);
            this.cmbDateType.TabIndex = 0;
            // 
            // dteFrom
            // 
            this.dteFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dteFrom.EditValue = new System.DateTime(2012, 10, 23, 0, 0, 0, 0);
            this.dteFrom.Location = new System.Drawing.Point(70, 32);
            this.dteFrom.Name = "dteFrom";
            this.dteFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFrom.Size = new System.Drawing.Size(135, 21);
            this.dteFrom.TabIndex = 1;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(7, 62);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(12, 14);
            this.labTo.TabIndex = 41;
            this.labTo.Text = "到";
            // 
            // labDateType
            // 
            this.labDateType.Location = new System.Drawing.Point(7, 8);
            this.labDateType.Name = "labDateType";
            this.labDateType.Size = new System.Drawing.Size(48, 14);
            this.labDateType.TabIndex = 31;
            this.labDateType.Text = "日期类型";
            // 
            // dteTo
            // 
            this.dteTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dteTo.EditValue = new System.DateTime(2012, 10, 23, 0, 0, 0, 0);
            this.dteTo.Location = new System.Drawing.Point(70, 59);
            this.dteTo.Name = "dteTo";
            this.dteTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteTo.Size = new System.Drawing.Size(135, 21);
            this.dteTo.TabIndex = 2;
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(7, 35);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(12, 14);
            this.labFrom.TabIndex = 42;
            this.labFrom.Text = "从";
            // 
            // navBarDate
            // 
            this.navBarDate.Caption = "日期";
            this.navBarDate.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarDate.Expanded = true;
            this.navBarDate.GroupClientHeight = 89;
            this.navBarDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarDate.Name = "navBarDate";
            // 
            // treeBoxSalesDep
            // 
            this.treeBoxSalesDep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeBoxSalesDep.EditText = "";
            this.treeBoxSalesDep.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("treeBoxSalesDep.EditValue")));
            this.treeBoxSalesDep.Location = new System.Drawing.Point(70, 6);
            this.treeBoxSalesDep.Name = "treeBoxSalesDep";
            this.treeBoxSalesDep.ReadOnly = false;
            this.treeBoxSalesDep.Size = new System.Drawing.Size(135, 21);
            this.treeBoxSalesDep.SplitString = ",";
            this.treeBoxSalesDep.TabIndex = 1;
            // 
            // CheckDepositSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CheckDepositSearchPart";
            this.Size = new System.Drawing.Size(227, 656);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSortByType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGroupBy.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.XtraScrollableControl panel2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraEditors.DateEdit dteFrom;
        private DevExpress.XtraEditors.DateEdit dteTo;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbGroupBy;
        private DevExpress.XtraEditors.LabelControl labGroupBy;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbDateType;
        private DevExpress.XtraEditors.LabelControl labDateType;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbSortByType;
        private DevExpress.XtraEditors.LabelControl labSortByType;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroup navBarDate;
        private System.Windows.Forms.RadioButton rdoAP;
        private System.Windows.Forms.RadioButton rdoAll;
        private System.Windows.Forms.RadioButton rdoAR;
        private ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl treeBoxSalesDep;
    }
}
