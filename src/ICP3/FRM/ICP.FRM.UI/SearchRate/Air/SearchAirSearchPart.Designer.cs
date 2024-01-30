namespace ICP.FRM.UI.SearchRate
{
    partial  class SearchAirSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchAirSearchPart));
            this.dateMonthControl1 = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.cmbScope = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.labScope = new DevExpress.XtraEditors.LabelControl();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.cmbCarrier = new ICP.FRM.UI.CheckBoxComboBox();
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.labPOL = new DevExpress.XtraEditors.LabelControl();
            this.cmbShipline = new ICP.FRM.UI.CheckBoxComboBox();
            this.labShipline = new DevExpress.XtraEditors.LabelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarBaseInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.navBarDuration = new DevExpress.XtraNavBar.NavBarGroup();
            this.panelScroll = new System.Windows.Forms.Panel();
            this.txtPOL = new DevExpress.XtraEditors.TextEdit();
            this.txtPOD = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbScope.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            this.navBarGroupControlContainer2.SuspendLayout();
            this.panelScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOD.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dateMonthControl1
            // 
            this.dateMonthControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateMonthControl1.IsEngish = true;
            this.dateMonthControl1.Location = new System.Drawing.Point(64, 3);
            this.dateMonthControl1.Name = "dateMonthControl1";
            this.dateMonthControl1.Size = new System.Drawing.Size(125, 141);
            this.dateMonthControl1.TabIndex = 3;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(12, 129);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 0;
            this.labTo.Text = "To";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(12, 100);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(27, 14);
            this.labFrom.TabIndex = 0;
            this.labFrom.Text = "From";
            // 
            // cmbScope
            // 
            this.cmbScope.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbScope.Location = new System.Drawing.Point(9, 182);
            this.cmbScope.Name = "cmbScope";
            this.cmbScope.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbScope.Properties.Appearance.Options.UseBackColor = true;
            this.cmbScope.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbScope.Properties.SmallImages = this.imageList1;
            this.cmbScope.Size = new System.Drawing.Size(180, 21);
            this.cmbScope.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbScope.TabIndex = 4;
            this.cmbScope.SelectedIndexChanged += new System.EventHandler(this.cmbScope_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "EFFECTIVE.png");
            this.imageList1.Images.SetKeyName(1, "WILL BE EFFECTIVE.png");
            this.imageList1.Images.SetKeyName(2, "EXPIRED.png");
            // 
            // labScope
            // 
            this.labScope.Location = new System.Drawing.Point(12, 166);
            this.labScope.Name = "labScope";
            this.labScope.Size = new System.Drawing.Size(34, 14);
            this.labScope.TabIndex = 0;
            this.labScope.Text = "Scope";
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(12, 126);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(15, 14);
            this.labPOD.TabIndex = 0;
            this.labPOD.Text = "To";
            // 
            // cmbCarrier
            // 
            this.cmbCarrier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCarrier.DataSource = null;
            this.cmbCarrier.DisplayMember = "";
            this.cmbCarrier.Location = new System.Drawing.Point(9, 58);
            this.cmbCarrier.Name = "cmbCarrier";
            this.cmbCarrier.NullText = "";
            this.cmbCarrier.Size = new System.Drawing.Size(180, 21);
            this.cmbCarrier.TabIndex = 1;
            this.cmbCarrier.ValueMember = "";
            this.cmbCarrier.Enter += new System.EventHandler(this.cmbCarrier_Enter);
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(12, 43);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(34, 14);
            this.labCarrier.TabIndex = 0;
            this.labCarrier.Text = "Carrier";
            // 
            // labPOL
            // 
            this.labPOL.Location = new System.Drawing.Point(12, 86);
            this.labPOL.Name = "labPOL";
            this.labPOL.Size = new System.Drawing.Size(27, 14);
            this.labPOL.TabIndex = 0;
            this.labPOL.Text = "From";
            // 
            // cmbShipline
            // 
            this.cmbShipline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbShipline.DataSource = null;
            this.cmbShipline.DisplayMember = "";
            this.cmbShipline.Location = new System.Drawing.Point(9, 18);
            this.cmbShipline.Name = "cmbShipline";
            this.cmbShipline.NullText = "";
            this.cmbShipline.Size = new System.Drawing.Size(180, 21);
            this.cmbShipline.TabIndex = 1;
            this.cmbShipline.ValueMember = "";
            this.cmbShipline.EditValueChanged += new System.EventHandler(this.cmbShipline_EditValueChanged);
            // 
            // labShipline
            // 
            this.labShipline.Location = new System.Drawing.Point(12, 3);
            this.labShipline.Name = "labShipline";
            this.labShipline.Size = new System.Drawing.Size(41, 14);
            this.labShipline.TabIndex = 0;
            this.labShipline.Text = "Shipline";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(152, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(44, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 562);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(235, 40);
            this.panelControl1.TabIndex = 7;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarBaseInfo;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupInterval = 10;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 10;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarBaseInfo,
            this.navBarDuration});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 307;
            this.navBarControl1.Size = new System.Drawing.Size(219, 571);
            this.navBarControl1.TabIndex = 9;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarBaseInfo
            // 
            this.navBarBaseInfo.Caption = "BaseInfo";
            this.navBarBaseInfo.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarBaseInfo.Expanded = true;
            this.navBarBaseInfo.GroupClientHeight = 215;
            this.navBarBaseInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBaseInfo.Name = "navBarBaseInfo";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.txtPOD);
            this.navBarGroupControlContainer1.Controls.Add(this.txtPOL);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbScope);
            this.navBarGroupControlContainer1.Controls.Add(this.labShipline);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbShipline);
            this.navBarGroupControlContainer1.Controls.Add(this.labPOL);
            this.navBarGroupControlContainer1.Controls.Add(this.labCarrier);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbCarrier);
            this.navBarGroupControlContainer1.Controls.Add(this.labScope);
            this.navBarGroupControlContainer1.Controls.Add(this.labPOD);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(195, 213);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.dateMonthControl1);
            this.navBarGroupControlContainer2.Controls.Add(this.labTo);
            this.navBarGroupControlContainer2.Controls.Add(this.labFrom);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(195, 148);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // navBarDuration
            // 
            this.navBarDuration.Caption = "Duration";
            this.navBarDuration.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarDuration.Expanded = true;
            this.navBarDuration.GroupClientHeight = 150;
            this.navBarDuration.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarDuration.Name = "navBarDuration";
            // 
            // panelScroll
            // 
            this.panelScroll.AutoScroll = true;
            this.panelScroll.Controls.Add(this.navBarControl1);
            this.panelScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScroll.Location = new System.Drawing.Point(0, 0);
            this.panelScroll.Name = "panelScroll";
            this.panelScroll.Size = new System.Drawing.Size(235, 562);
            this.panelScroll.TabIndex = 10;
            // 
            // txtPOL
            // 
            this.txtPOL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPOL.Location = new System.Drawing.Point(9, 101);
            this.txtPOL.Name = "txtPOL";
            this.txtPOL.Size = new System.Drawing.Size(180, 21);
            this.txtPOL.TabIndex = 5;
            // 
            // txtPOD
            // 
            this.txtPOD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPOD.Location = new System.Drawing.Point(9, 141);
            this.txtPOD.Name = "txtPOD";
            this.txtPOD.Size = new System.Drawing.Size(180, 21);
            this.txtPOD.TabIndex = 6;
            // 
            // SearchAirSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelScroll);
            this.Controls.Add(this.panelControl1);
            this.Name = "SearchAirSearchPart";
            this.IsMultiLanguage = false;
            this.Size = new System.Drawing.Size(235, 602);
            ((System.ComponentModel.ISupportInitialize)(this.cmbScope.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            this.panelScroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOD.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labShipline;
        private CheckBoxComboBox cmbShipline;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private CheckBoxComboBox cmbCarrier;
        private DevExpress.XtraEditors.LabelControl labCarrier;
        private DevExpress.XtraEditors.LabelControl labPOL;
        private ICP.Framework.ClientComponents.Controls.DateMonthControl dateMonthControl1;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbScope;
        private DevExpress.XtraEditors.LabelControl labScope;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarBaseInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroup navBarDuration;
        private System.Windows.Forms.Panel panelScroll;
        private DevExpress.XtraEditors.TextEdit txtPOD;
        private DevExpress.XtraEditors.TextEdit txtPOL;
    }
}
