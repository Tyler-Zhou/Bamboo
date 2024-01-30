namespace ICP.ReportCenter.UI.BusinessReports
{
   partial class AnalysisOfOperatingConditionsSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalysisOfOperatingConditionsSearchPart));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.operationDatePart1 = new ICP.ReportCenter.UI.Comm.Controls.OperationDateCustomMonthPart();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.labShipLine = new DevExpress.XtraEditors.LabelControl();
            this.seOrderByTop = new DevExpress.XtraEditors.SpinEdit();
            this.cmbOrderByOP = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cmbReportType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labReportType = new DevExpress.XtraEditors.LabelControl();
            this.treeDepartment = new ICP.ReportCenter.UI.Comm.Controls.BusinessCompanyTreeSelectBox();
            this.cmbGroupBy = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labDepartment = new DevExpress.XtraEditors.LabelControl();
            this.labOrderBy = new DevExpress.XtraEditors.LabelControl();
            this.labGroupBy = new DevExpress.XtraEditors.LabelControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.chkcmbShipLine = new ICP.ReportCenter.UI.Comm.Controls.BusinessShipLineTreeCheckControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seOrderByTop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOrderByOP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGroupBy.Properties)).BeginInit();
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
            this.navBarControl1.Size = new System.Drawing.Size(240, 381);
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarDate
            // 
            this.nbarDate.Caption = "日期";
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
            this.navBarGroupControlContainer1.Controls.Add(this.chkcmbShipLine);
            this.navBarGroupControlContainer1.Controls.Add(this.labShipLine);
            this.navBarGroupControlContainer1.Controls.Add(this.seOrderByTop);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbOrderByOP);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbReportType);
            this.navBarGroupControlContainer1.Controls.Add(this.labReportType);
            this.navBarGroupControlContainer1.Controls.Add(this.treeDepartment);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbGroupBy);
            this.navBarGroupControlContainer1.Controls.Add(this.labDepartment);
            this.navBarGroupControlContainer1.Controls.Add(this.labOrderBy);
            this.navBarGroupControlContainer1.Controls.Add(this.labGroupBy);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(232, 167);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // labShipLine
            // 
            this.labShipLine.Location = new System.Drawing.Point(8, 86);
            this.labShipLine.Name = "labShipLine";
            this.labShipLine.Size = new System.Drawing.Size(24, 14);
            this.labShipLine.TabIndex = 221;
            this.labShipLine.Text = "航线";
            // 
            // seOrderByTop
            // 
            this.seOrderByTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.seOrderByTop.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.seOrderByTop.Location = new System.Drawing.Point(66, 138);
            this.seOrderByTop.Name = "seOrderByTop";
            this.seOrderByTop.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seOrderByTop.Size = new System.Drawing.Size(163, 21);
            this.seOrderByTop.TabIndex = 5;
            this.seOrderByTop.Visible = false;
            // 
            // cmbOrderByOP
            // 
            this.cmbOrderByOP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbOrderByOP.Location = new System.Drawing.Point(66, 111);
            this.cmbOrderByOP.Name = "cmbOrderByOP";
            this.cmbOrderByOP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOrderByOP.Size = new System.Drawing.Size(163, 21);
            this.cmbOrderByOP.TabIndex = 4;
            this.cmbOrderByOP.Visible = false;
            // 
            // cmbReportType
            // 
            this.cmbReportType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbReportType.Location = new System.Drawing.Point(66, 3);
            this.cmbReportType.Name = "cmbReportType";
            this.cmbReportType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReportType.Size = new System.Drawing.Size(163, 21);
            this.cmbReportType.TabIndex = 0;
            // 
            // labReportType
            // 
            this.labReportType.Location = new System.Drawing.Point(9, 6);
            this.labReportType.Name = "labReportType";
            this.labReportType.Size = new System.Drawing.Size(48, 14);
            this.labReportType.TabIndex = 218;
            this.labReportType.Text = "报表类型";
            // 
            // treeDepartment
            // 
            this.treeDepartment.AllText = "Selecte ALL";
            this.treeDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeDepartment.EditValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.treeDepartment.Location = new System.Drawing.Point(66, 57);
            this.treeDepartment.Name = "treeDepartment";
            this.treeDepartment.ReadOnly = false;
            this.treeDepartment.Size = new System.Drawing.Size(163, 21);
            this.treeDepartment.SpecifiedBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.treeDepartment.TabIndex = 2;
            // 
            // cmbGroupBy
            // 
            this.cmbGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGroupBy.Enabled = false;
            this.cmbGroupBy.Location = new System.Drawing.Point(66, 30);
            this.cmbGroupBy.Name = "cmbGroupBy";
            this.cmbGroupBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGroupBy.Size = new System.Drawing.Size(163, 21);
            this.cmbGroupBy.TabIndex = 1;
            // 
            // labDepartment
            // 
            this.labDepartment.Location = new System.Drawing.Point(9, 60);
            this.labDepartment.Name = "labDepartment";
            this.labDepartment.Size = new System.Drawing.Size(24, 14);
            this.labDepartment.TabIndex = 209;
            this.labDepartment.Text = "部门";
            // 
            // labOrderBy
            // 
            this.labOrderBy.Location = new System.Drawing.Point(9, 114);
            this.labOrderBy.Name = "labOrderBy";
            this.labOrderBy.Size = new System.Drawing.Size(44, 14);
            this.labOrderBy.TabIndex = 12;
            this.labOrderBy.Text = "OrderBy";
            this.labOrderBy.Visible = false;
            // 
            // labGroupBy
            // 
            this.labGroupBy.Location = new System.Drawing.Point(9, 32);
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
            this.nbarBase.GroupClientHeight = 169;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // chkcmbShipLine
            // 
            this.chkcmbShipLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbShipLine.EditText = "";
            this.chkcmbShipLine.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("chkcmbShipLine.EditValue")));
            this.chkcmbShipLine.Location = new System.Drawing.Point(66, 84);
            this.chkcmbShipLine.Name = "chkcmbShipLine";
            this.chkcmbShipLine.ReadOnly = false;
            this.chkcmbShipLine.Size = new System.Drawing.Size(163, 21);
            this.chkcmbShipLine.SplitString = ",";
            this.chkcmbShipLine.TabIndex = 222;
            // 
            // AnalysisOfOperatingConditionsSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "AnalysisOfOperatingConditionsSearchPart";
            this.Size = new System.Drawing.Size(240, 621);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seOrderByTop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOrderByOP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGroupBy.Properties)).EndInit();
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
        private ICP.ReportCenter.UI.Comm.Controls.OperationDateCustomMonthPart operationDatePart1;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbGroupBy;
        private DevExpress.XtraEditors.LabelControl labDepartment;
        private ICP.ReportCenter.UI.Comm.Controls.BusinessCompanyTreeSelectBox treeDepartment;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbReportType;
        private DevExpress.XtraEditors.LabelControl labReportType;
        private DevExpress.XtraEditors.SpinEdit seOrderByTop;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbOrderByOP;
        private DevExpress.XtraEditors.LabelControl labOrderBy;
        private DevExpress.XtraEditors.LabelControl labShipLine;
        private ICP.ReportCenter.UI.Comm.Controls.BusinessShipLineTreeCheckControl chkcmbShipLine;
    }
}
