namespace ICP.FRM.UI
{
    partial class WeeklyReportPart
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
            this.components = new System.ComponentModel.Container();
            this.tabWeeklyReport = new DevExpress.XtraTab.XtraTabControl();
            this.tabGeneralWeeklyReport = new DevExpress.XtraTab.XtraTabPage();
            this.pnlGeneral = new DevExpress.XtraEditors.PanelControl();
            this.tabCompanyWeeklyReport = new DevExpress.XtraTab.XtraTabPage();
            this.pnlCompany = new DevExpress.XtraEditors.PanelControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.rgGroup = new DevExpress.XtraEditors.RadioGroup();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAppend = new DevExpress.XtraBars.BarButtonItem();
            this.barRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.cmbWeekDate = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labGroupBy = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.tabWeeklyReport)).BeginInit();
            this.tabWeeklyReport.SuspendLayout();
            this.tabGeneralWeeklyReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGeneral)).BeginInit();
            this.tabCompanyWeeklyReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeekDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tabWeeklyReport
            // 
            this.tabWeeklyReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabWeeklyReport.Location = new System.Drawing.Point(0, 38);
            this.tabWeeklyReport.Name = "tabWeeklyReport";
            this.tabWeeklyReport.SelectedTabPage = this.tabGeneralWeeklyReport;
            this.tabWeeklyReport.Size = new System.Drawing.Size(913, 522);
            this.tabWeeklyReport.TabIndex = 1;
            this.tabWeeklyReport.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabGeneralWeeklyReport,
            this.tabCompanyWeeklyReport});
            this.tabWeeklyReport.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabWeeklyReport_SelectedPageChanged);
            // 
            // tabGeneralWeeklyReport
            // 
            this.tabGeneralWeeklyReport.Controls.Add(this.pnlGeneral);
            this.tabGeneralWeeklyReport.Name = "tabGeneralWeeklyReport";
            this.tabGeneralWeeklyReport.Size = new System.Drawing.Size(906, 492);
            this.tabGeneralWeeklyReport.Text = "General Weekly Report";
            // 
            // pnlGeneral
            // 
            this.pnlGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGeneral.Location = new System.Drawing.Point(0, 0);
            this.pnlGeneral.Name = "pnlGeneral";
            this.pnlGeneral.Size = new System.Drawing.Size(906, 492);
            this.pnlGeneral.TabIndex = 0;
            // 
            // tabCompanyWeeklyReport
            // 
            this.tabCompanyWeeklyReport.Controls.Add(this.pnlCompany);
            this.tabCompanyWeeklyReport.Controls.Add(this.barDockControlLeft);
            this.tabCompanyWeeklyReport.Controls.Add(this.barDockControlRight);
            this.tabCompanyWeeklyReport.Controls.Add(this.barDockControlBottom);
            this.tabCompanyWeeklyReport.Controls.Add(this.barDockControlTop);
            this.tabCompanyWeeklyReport.Name = "tabCompanyWeeklyReport";
            this.tabCompanyWeeklyReport.Size = new System.Drawing.Size(906, 492);
            this.tabCompanyWeeklyReport.Text = " Weekly Report";
            // 
            // pnlCompany
            // 
            this.pnlCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCompany.Location = new System.Drawing.Point(0, 26);
            this.pnlCompany.Name = "pnlCompany";
            this.pnlCompany.Size = new System.Drawing.Size(906, 466);
            this.pnlCompany.TabIndex = 9;
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 466);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(906, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 466);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 492);
            this.barDockControlBottom.Size = new System.Drawing.Size(906, 0);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(906, 26);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.labGroupBy);
            this.pnlTop.Controls.Add(this.rgGroup);
            this.pnlTop.Controls.Add(this.cmbWeekDate);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(913, 38);
            this.pnlTop.TabIndex = 2;
            // 
            // rgGroup
            // 
            this.rgGroup.Location = new System.Drawing.Point(331, 5);
            this.rgGroup.MenuManager = this.barManager1;
            this.rgGroup.Name = "rgGroup";
            this.rgGroup.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Trading area"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Loading port")});
            this.rgGroup.Size = new System.Drawing.Size(227, 28);
            this.rgGroup.TabIndex = 1;
            this.rgGroup.SelectedIndexChanged += new System.EventHandler(this.rgGroup_SelectedIndexChanged);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this.tabCompanyWeeklyReport;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSave,
            this.barAppend,
            this.barRemove});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAppend, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRemove, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAppend
            // 
            this.barAppend.Caption = "&New";
            this.barAppend.Glyph = global::ICP.FRM.UI.Properties.Resources.Add_16;
            this.barAppend.Id = 1;
            this.barAppend.Name = "barAppend";
            this.barAppend.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAppend_ItemClick);
            // 
            // barRemove
            // 
            this.barRemove.Caption = "&Remove";
            this.barRemove.Glyph = global::ICP.FRM.UI.Properties.Resources.Delete_16;
            this.barRemove.Id = 2;
            this.barRemove.Name = "barRemove";
            this.barRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRemove_ItemClick);
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = global::ICP.FRM.UI.Properties.Resources.Save_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // cmbWeekDate
            // 
            this.cmbWeekDate.Location = new System.Drawing.Point(17, 10);
            this.cmbWeekDate.Name = "cmbWeekDate";
            this.cmbWeekDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbWeekDate.Properties.Appearance.Options.UseBackColor = true;
            this.cmbWeekDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWeekDate.Size = new System.Drawing.Size(241, 21);
            this.cmbWeekDate.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbWeekDate.TabIndex = 0;
            this.cmbWeekDate.SelectedIndexChanged += new System.EventHandler(this.cmbWeekDate_SelectedIndexChanged);
            this.cmbWeekDate.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.cmbWeekDate_EditValueChanging);
            // 
            // labGroupBy
            // 
            this.labGroupBy.Location = new System.Drawing.Point(268, 13);
            this.labGroupBy.Name = "labGroupBy";
            this.labGroupBy.Size = new System.Drawing.Size(50, 14);
            this.labGroupBy.TabIndex = 2;
            this.labGroupBy.Text = "Group By";
            // 
            // WeeklyReportPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabWeeklyReport);
            this.Controls.Add(this.pnlTop);
            this.IsMultiLanguage = false;
            this.Name = "WeeklyReportPart";
            this.Size = new System.Drawing.Size(913, 560);
            ((System.ComponentModel.ISupportInitialize)(this.tabWeeklyReport)).EndInit();
            this.tabWeeklyReport.ResumeLayout(false);
            this.tabGeneralWeeklyReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGeneral)).EndInit();
            this.tabCompanyWeeklyReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeekDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabWeeklyReport;
        private DevExpress.XtraTab.XtraTabPage tabCompanyWeeklyReport;
        private DevExpress.XtraTab.XtraTabPage tabGeneralWeeklyReport;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbWeekDate;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barAppend;
        private DevExpress.XtraBars.BarButtonItem barRemove;
        private DevExpress.XtraEditors.PanelControl pnlGeneral;
        private DevExpress.XtraEditors.PanelControl pnlCompany;
        private DevExpress.XtraEditors.RadioGroup rgGroup;
        private DevExpress.XtraEditors.LabelControl labGroupBy;
    }
}
