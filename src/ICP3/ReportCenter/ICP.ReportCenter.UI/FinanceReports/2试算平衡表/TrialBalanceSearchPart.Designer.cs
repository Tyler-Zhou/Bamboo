﻿namespace ICP.ReportCenter.UI.FinanceReports
{
   partial class TrialBalanceSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrialBalanceSearchPart));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBaseInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.dteTo = new DevExpress.XtraEditors.DateEdit();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.ucReportType = new ICP.ReportCenter.UI.Comm.Controls.UCReportType();
            this.nbarReportType = new DevExpress.XtraNavBar.NavBarGroup();
            this.chkcmbCompany = new ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).BeginInit();
            this.navBarGroupControlContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 580);
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
            this.panel2.Size = new System.Drawing.Size(240, 580);
            this.panel2.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBaseInfo;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBaseInfo,
            this.nbarReportType});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(240, 284);
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarBaseInfo
            // 
            this.nbarBaseInfo.Caption = "基本信息";
            this.nbarBaseInfo.ControlContainer = this.navBarGroupBase;
            this.nbarBaseInfo.Expanded = true;
            this.nbarBaseInfo.GroupClientHeight = 62;
            this.nbarBaseInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBaseInfo.Name = "nbarBaseInfo";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.chkcmbCompany);
            this.navBarGroupBase.Controls.Add(this.dteTo);
            this.navBarGroupBase.Controls.Add(this.labCompany);
            this.navBarGroupBase.Controls.Add(this.labTo);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(232, 60);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // dteTo
            // 
            this.dteTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dteTo.EditValue = new System.DateTime(2011, 12, 22, 0, 0, 0, 0);
            this.dteTo.Location = new System.Drawing.Point(66, 4);
            this.dteTo.Name = "dteTo";
            this.dteTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTo.Properties.DisplayFormat.FormatString = "s";
            this.dteTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dteTo.Properties.EditFormat.FormatString = "s";
            this.dteTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dteTo.Properties.Mask.EditMask = "s";
            this.dteTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteTo.Size = new System.Drawing.Size(163, 21);
            this.dteTo.TabIndex = 0;
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(9, 34);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(24, 14);
            this.labCompany.TabIndex = 29;
            this.labCompany.Text = "公司";
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(9, 7);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(48, 14);
            this.labTo.TabIndex = 26;
            this.labTo.Text = "结束日期";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.ucReportType);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(232, 131);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // ucReportType
            // 
            this.ucReportType.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucReportType.Location = new System.Drawing.Point(0, 0);
            this.ucReportType.Name = "ucReportType";
            this.ucReportType.Size = new System.Drawing.Size(232, 131);
            this.ucReportType.TabIndex = 0;
            // 
            // nbarReportType
            // 
            this.nbarReportType.Caption = "报表类型";
            this.nbarReportType.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarReportType.Expanded = true;
            this.nbarReportType.GroupClientHeight = 133;
            this.nbarReportType.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarReportType.Name = "nbarReportType";
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.EditText = "";
            this.chkcmbCompany.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("chkcmbCompany.EditValue")));
            this.chkcmbCompany.Location = new System.Drawing.Point(66, 31);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.ReadOnly = false;
            this.chkcmbCompany.ShowDepartment = false;
            this.chkcmbCompany.Size = new System.Drawing.Size(163, 21);
            this.chkcmbCompany.SplitString = ",";
            this.chkcmbCompany.TabIndex = 30;
            // 
            // TrialBalanceSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "TrialBalanceSearchPart";
            this.Size = new System.Drawing.Size(240, 639);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).EndInit();
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.XtraScrollableControl panel2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBaseInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroup nbarReportType;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraEditors.DateEdit dteTo;
        private ICP.ReportCenter.UI.Comm.Controls.UCReportType ucReportType;
        private Comm.Controls.CompanyTreeCheckControl chkcmbCompany;
    }
}
