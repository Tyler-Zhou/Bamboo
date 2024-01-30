namespace ICP.ReportCenter.UI.CRMReports
{
   partial class CustomerStateSearchPart
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
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cmbCustomerType = new ICP.ReportCenter.UI.Comm.Controls.CustomerTypeLWImageComboBoxEdit();
            this.labCustomerType = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomer = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.treeDepartment = new ICP.ReportCenter.UI.Comm.Controls.CRMCompanyComboBox();
            this.labDepartment = new DevExpress.XtraEditors.LabelControl();
            this.txtSales = new ICP.ReportCenter.UI.Comm.Controls.CRMSalesButtonEdit();
            this.labSales = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSales.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 562);
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
            this.panel2.Size = new System.Drawing.Size(240, 562);
            this.panel2.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(240, 184);
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "基本信息";
            this.nbarBase.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 119;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.cmbCustomerType);
            this.navBarGroupControlContainer1.Controls.Add(this.labCustomerType);
            this.navBarGroupControlContainer1.Controls.Add(this.txtCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.labCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.treeDepartment);
            this.navBarGroupControlContainer1.Controls.Add(this.labDepartment);
            this.navBarGroupControlContainer1.Controls.Add(this.txtSales);
            this.navBarGroupControlContainer1.Controls.Add(this.labSales);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(232, 117);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // cmbCustomerType
            // 
            this.cmbCustomerType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCustomerType.Location = new System.Drawing.Point(66, 3);
            this.cmbCustomerType.Name = "cmbCustomerType";
            this.cmbCustomerType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCustomerType.Size = new System.Drawing.Size(163, 21);
            this.cmbCustomerType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCustomerType.TabIndex = 0;
            // 
            // labCustomerType
            // 
            this.labCustomerType.Location = new System.Drawing.Point(9, 6);
            this.labCustomerType.Name = "labCustomerType";
            this.labCustomerType.Size = new System.Drawing.Size(48, 14);
            this.labCustomerType.TabIndex = 213;
            this.labCustomerType.Text = "客户类型";
            // 
            // txtCustomer
            // 
            this.txtCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomer.FinderName = "CustomerFinder";
            this.txtCustomer.Location = new System.Drawing.Point(66, 84);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCustomer.Size = new System.Drawing.Size(163, 21);
            this.txtCustomer.TabIndex = 3;
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(9, 87);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(24, 14);
            this.labCustomer.TabIndex = 211;
            this.labCustomer.Text = "客户";
            // 
            // treeDepartment
            // 
            this.treeDepartment.AllText = "Selecte ALL";
            this.treeDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeDepartment.EditValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.treeDepartment.Location = new System.Drawing.Point(66, 30);
            this.treeDepartment.Name = "treeDepartment";
            this.treeDepartment.ReadOnly = false;
            this.treeDepartment.Size = new System.Drawing.Size(163, 21);
            this.treeDepartment.SpecifiedBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.treeDepartment.TabIndex = 1;
            // 
            // labDepartment
            // 
            this.labDepartment.Location = new System.Drawing.Point(9, 33);
            this.labDepartment.Name = "labDepartment";
            this.labDepartment.Size = new System.Drawing.Size(24, 14);
            this.labDepartment.TabIndex = 209;
            this.labDepartment.Text = "部门";
            // 
            // txtSales
            // 
            this.txtSales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSales.Location = new System.Drawing.Point(66, 57);
            this.txtSales.Name = "txtSales";
            this.txtSales.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtSales.Properties.Appearance.Options.UseBackColor = true;
            this.txtSales.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtSales.Size = new System.Drawing.Size(163, 21);
            this.txtSales.TabIndex = 2;
            // 
            // labSales
            // 
            this.labSales.Location = new System.Drawing.Point(9, 60);
            this.labSales.Name = "labSales";
            this.labSales.Size = new System.Drawing.Size(36, 14);
            this.labSales.TabIndex = 12;
            this.labSales.Text = "业务员";
            // 
            // CustomerStateSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CustomerStateSearchPart";
            this.Size = new System.Drawing.Size(240, 621);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSales.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.XtraScrollableControl panel2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private ICP.ReportCenter.UI.Comm.Controls.CRMSalesButtonEdit txtSales;
        private DevExpress.XtraEditors.LabelControl labSales;
        private DevExpress.XtraEditors.LabelControl labDepartment;
        private ICP.ReportCenter.UI.Comm.Controls.CRMCompanyComboBox treeDepartment;
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit txtCustomer;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private ICP.ReportCenter.UI.Comm.Controls.CustomerTypeLWImageComboBoxEdit cmbCustomerType;
        private DevExpress.XtraEditors.LabelControl labCustomerType;
    }
}
