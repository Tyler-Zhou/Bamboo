namespace ICP.FRM.UI.BookingReport
{
    partial class BookingReportSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookingReportSearchPart));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dateMonthControl1 = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.labShipLine = new DevExpress.XtraEditors.LabelControl();
            this.cmbCompany = new ICP.FRM.UI.CheckBoxComboBox();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarBaseInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cmbShipLine = new ICP.Framework.ClientComponents.Controls.TreeCheckControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.chkCarrier = new DevExpress.XtraEditors.CheckEdit();
            this.chkShipLine = new DevExpress.XtraEditors.CheckEdit();
            this.chkVoyageName = new DevExpress.XtraEditors.CheckEdit();
            this.chkCompany = new DevExpress.XtraEditors.CheckEdit();
            this.chkbValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.stxtCarrier = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.cmbSalesType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labGroupBy = new DevExpress.XtraEditors.LabelControl();
            this.labValid = new DevExpress.XtraEditors.LabelControl();
            this.labSalesType = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.navBarDuration = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShipLine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVoyageName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalesType.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Input_16.png");
            this.imageList1.Images.SetKeyName(1, "EFFECTIVE.png");
            this.imageList1.Images.SetKeyName(2, "WILL BE EFFECTIVE.png");
            this.imageList1.Images.SetKeyName(3, "EXPIRED.png");
            // 
            // dateMonthControl1
            // 
            this.dateMonthControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateMonthControl1.From = null;
            this.dateMonthControl1.IsEngish = true;
            this.dateMonthControl1.Location = new System.Drawing.Point(55, 3);
            this.dateMonthControl1.Name = "dateMonthControl1";
            this.dateMonthControl1.Size = new System.Drawing.Size(166, 141);
            this.dateMonthControl1.TabIndex = 1;
            this.dateMonthControl1.To = null;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(12, 122);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 2;
            this.labTo.Text = "To";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(12, 94);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(27, 14);
            this.labFrom.TabIndex = 2;
            this.labFrom.Text = "From";
            // 
            // labShipLine
            // 
            this.labShipLine.Location = new System.Drawing.Point(4, 36);
            this.labShipLine.Name = "labShipLine";
            this.labShipLine.Size = new System.Drawing.Size(45, 14);
            this.labShipLine.TabIndex = 0;
            this.labShipLine.Text = "ShipLine";
            // 
            // cmbCompany
            // 
            this.cmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCompany.DataSource = null;
            this.cmbCompany.DisplayMember = "";
            this.cmbCompany.Location = new System.Drawing.Point(58, 5);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.NullText = "";
            this.cmbCompany.Size = new System.Drawing.Size(163, 21);
            this.cmbCompany.TabIndex = 0;
            this.cmbCompany.ValueMember = "";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(4, 7);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 0;
            this.labCompany.Text = "Company";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(149, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(41, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 505);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(232, 40);
            this.panelControl1.TabIndex = 0;
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.navBarControl1);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(232, 505);
            this.xtraScrollableControl1.TabIndex = 8;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarBaseInfo;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupInterval = 2;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarBaseInfo,
            this.navBarDuration});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 586;
            this.navBarControl1.Size = new System.Drawing.Size(232, 499);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarBaseInfo
            // 
            this.navBarBaseInfo.Caption = "BaseInfo";
            this.navBarBaseInfo.ControlContainer = this.navBarGroupBase;
            this.navBarBaseInfo.Expanded = true;
            this.navBarBaseInfo.GroupClientHeight = 280;
            this.navBarBaseInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBaseInfo.Name = "navBarBaseInfo";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.cmbShipLine);
            this.navBarGroupBase.Controls.Add(this.panelControl2);
            this.navBarGroupBase.Controls.Add(this.chkbValid);
            this.navBarGroupBase.Controls.Add(this.stxtCarrier);
            this.navBarGroupBase.Controls.Add(this.stxtCustomer);
            this.navBarGroupBase.Controls.Add(this.labCompany);
            this.navBarGroupBase.Controls.Add(this.cmbSalesType);
            this.navBarGroupBase.Controls.Add(this.labCarrier);
            this.navBarGroupBase.Controls.Add(this.cmbCompany);
            this.navBarGroupBase.Controls.Add(this.labCustomer);
            this.navBarGroupBase.Controls.Add(this.labShipLine);
            this.navBarGroupBase.Controls.Add(this.labGroupBy);
            this.navBarGroupBase.Controls.Add(this.labValid);
            this.navBarGroupBase.Controls.Add(this.labSalesType);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(224, 278);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // cmbShipLine
            // 
            this.cmbShipLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbShipLine.EditText = "";
            this.cmbShipLine.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("cmbShipLine.EditValue")));
            this.cmbShipLine.Location = new System.Drawing.Point(58, 33);
            this.cmbShipLine.Name = "cmbShipLine";
            this.cmbShipLine.ReadOnly = false;
            this.cmbShipLine.Size = new System.Drawing.Size(162, 21);
            this.cmbShipLine.SplitString = ";";
            this.cmbShipLine.TabIndex = 1;
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.chkCarrier);
            this.panelControl2.Controls.Add(this.chkShipLine);
            this.panelControl2.Controls.Add(this.chkVoyageName);
            this.panelControl2.Controls.Add(this.chkCompany);
            this.panelControl2.Location = new System.Drawing.Point(58, 169);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(163, 106);
            this.panelControl2.TabIndex = 10;
            // 
            // chkCarrier
            // 
            this.chkCarrier.Location = new System.Drawing.Point(2, 81);
            this.chkCarrier.Name = "chkCarrier";
            this.chkCarrier.Properties.Caption = "Carrier";
            this.chkCarrier.Size = new System.Drawing.Size(75, 19);
            this.chkCarrier.TabIndex = 3;
            this.chkCarrier.CheckedChanged += new System.EventHandler(this.chkCarrier_CheckedChanged);
            // 
            // chkShipLine
            // 
            this.chkShipLine.Location = new System.Drawing.Point(2, 56);
            this.chkShipLine.Name = "chkShipLine";
            this.chkShipLine.Properties.Caption = "ShipLine";
            this.chkShipLine.Size = new System.Drawing.Size(75, 19);
            this.chkShipLine.TabIndex = 2;
            this.chkShipLine.CheckedChanged += new System.EventHandler(this.chkShipLine_CheckedChanged);
            // 
            // chkVoyageName
            // 
            this.chkVoyageName.EditValue = true;
            this.chkVoyageName.Location = new System.Drawing.Point(2, 5);
            this.chkVoyageName.Name = "chkVoyageName";
            this.chkVoyageName.Properties.Caption = "Vessel&Voyage";
            this.chkVoyageName.Size = new System.Drawing.Size(111, 19);
            this.chkVoyageName.TabIndex = 0;
            this.chkVoyageName.CheckedChanged += new System.EventHandler(this.chkCompany_CheckedChanged);
            // 
            // chkCompany
            // 
            this.chkCompany.Location = new System.Drawing.Point(2, 31);
            this.chkCompany.Name = "chkCompany";
            this.chkCompany.Properties.Caption = "Company";
            this.chkCompany.Size = new System.Drawing.Size(75, 19);
            this.chkCompany.TabIndex = 1;
            this.chkCompany.CheckedChanged += new System.EventHandler(this.chkCompany_CheckedChanged);
            // 
            // chkbValid
            // 
            this.chkbValid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkbValid.Checked = null;
            this.chkbValid.CheckedText = "TRUE";
            this.chkbValid.Location = new System.Drawing.Point(58, 143);
            this.chkbValid.Name = "chkbValid";
            this.chkbValid.NULLText = "ALL";
            this.chkbValid.Size = new System.Drawing.Size(163, 22);
            this.chkbValid.TabIndex = 5;
            this.chkbValid.UnCheckedText = "FALSE";
            // 
            // stxtCarrier
            // 
            this.stxtCarrier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.stxtCarrier.Location = new System.Drawing.Point(56, 61);
            this.stxtCarrier.Name = "stxtCarrier";
            this.stxtCarrier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtCarrier.Size = new System.Drawing.Size(163, 21);
            this.stxtCarrier.TabIndex = 2;
            // 
            // stxtCustomer
            // 
            this.stxtCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.stxtCustomer.Location = new System.Drawing.Point(56, 88);
            this.stxtCustomer.Name = "stxtCustomer";
            this.stxtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtCustomer.Size = new System.Drawing.Size(163, 21);
            this.stxtCustomer.TabIndex = 3;
            // 
            // cmbSalesType
            // 
            this.cmbSalesType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSalesType.Location = new System.Drawing.Point(56, 116);
            this.cmbSalesType.Name = "cmbSalesType";
            this.cmbSalesType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbSalesType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbSalesType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSalesType.Size = new System.Drawing.Size(163, 21);
            this.cmbSalesType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbSalesType.TabIndex = 4;
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(4, 64);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(34, 14);
            this.labCarrier.TabIndex = 0;
            this.labCarrier.Text = "Carrier";
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(4, 91);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 0;
            this.labCustomer.Text = "Customer";
            // 
            // labGroupBy
            // 
            this.labGroupBy.Location = new System.Drawing.Point(4, 172);
            this.labGroupBy.Name = "labGroupBy";
            this.labGroupBy.Size = new System.Drawing.Size(46, 14);
            this.labGroupBy.TabIndex = 0;
            this.labGroupBy.Text = "GroupBy";
            // 
            // labValid
            // 
            this.labValid.Location = new System.Drawing.Point(4, 147);
            this.labValid.Name = "labValid";
            this.labValid.Size = new System.Drawing.Size(25, 14);
            this.labValid.TabIndex = 0;
            this.labValid.Text = "Valid";
            // 
            // labSalesType
            // 
            this.labSalesType.Location = new System.Drawing.Point(1, 121);
            this.labSalesType.Name = "labSalesType";
            this.labSalesType.Size = new System.Drawing.Size(55, 14);
            this.labSalesType.TabIndex = 0;
            this.labSalesType.Text = "SalesType";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.dateMonthControl1);
            this.navBarGroupControlContainer2.Controls.Add(this.labTo);
            this.navBarGroupControlContainer2.Controls.Add(this.labFrom);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(224, 148);
            this.navBarGroupControlContainer2.TabIndex = 0;
            // 
            // navBarDuration
            // 
            this.navBarDuration.Caption = "Date";
            this.navBarDuration.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarDuration.Expanded = true;
            this.navBarDuration.GroupClientHeight = 150;
            this.navBarDuration.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarDuration.Name = "navBarDuration";
            // 
            // BookingReportSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraScrollableControl1);
            this.Controls.Add(this.panelControl1);
            this.IsMultiLanguage = false;
            this.Name = "BookingReportSearchPart";
            this.Size = new System.Drawing.Size(232, 545);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.xtraScrollableControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShipLine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVoyageName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalesType.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            this.ResumeLayout(false);

        }      

        #endregion

        private DevExpress.XtraEditors.LabelControl labCompany;
        private CheckBoxComboBox cmbCompany;
        private DevExpress.XtraEditors.LabelControl labShipLine;
        private ICP.Framework.ClientComponents.Controls.DateMonthControl dateMonthControl1;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarBaseInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraNavBar.NavBarGroup navBarDuration;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbSalesType;
        private DevExpress.XtraEditors.LabelControl labSalesType;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.ButtonEdit stxtCustomer;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton chkbValid;
        private DevExpress.XtraEditors.LabelControl labValid;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labGroupBy;
        private DevExpress.XtraEditors.CheckEdit chkCarrier;
        private DevExpress.XtraEditors.CheckEdit chkShipLine;
        private DevExpress.XtraEditors.CheckEdit chkCompany;
        private ICP.Framework.ClientComponents.Controls.TreeCheckControl cmbShipLine;
        private DevExpress.XtraEditors.ButtonEdit stxtCarrier;
        private DevExpress.XtraEditors.LabelControl labCarrier;
        private DevExpress.XtraEditors.CheckEdit chkVoyageName;
    }
}
