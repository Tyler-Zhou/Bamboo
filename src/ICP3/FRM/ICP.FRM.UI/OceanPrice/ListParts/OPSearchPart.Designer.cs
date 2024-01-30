namespace ICP.FRM.UI.OceanPrice
{
    partial class OPSearchPart
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClean = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.labState = new DevExpress.XtraEditors.LabelControl();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.labShipLine = new DevExpress.XtraEditors.LabelControl();
            this.labNO = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.labVIA = new DevExpress.XtraEditors.LabelControl();
            this.labPublisher = new DevExpress.XtraEditors.LabelControl();
            this.labDelivery = new DevExpress.XtraEditors.LabelControl();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.labPOL = new DevExpress.XtraEditors.LabelControl();
            this.txtNO = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtVia = new DevExpress.XtraEditors.TextEdit();
            this.txtPublisher = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.txtDelivery = new DevExpress.XtraEditors.TextEdit();
            this.txtPOD = new DevExpress.XtraEditors.TextEdit();
            this.txtPOL = new DevExpress.XtraEditors.TextEdit();
            this.txtCarrier = new DevExpress.XtraEditors.TextEdit();
            this.cmbState = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cmbType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cmbShipLine = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.dteTo = new DevExpress.XtraEditors.DateEdit();
            this.dteFrom = new DevExpress.XtraEditors.DateEdit();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.chkDate = new DevExpress.XtraEditors.CheckEdit();
            this.nbarDuration = new DevExpress.XtraNavBar.NavBarGroup();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShipLine.Properties)).BeginInit();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClean);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 506);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 59);
            this.panel1.TabIndex = 0;
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(17, 16);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(75, 23);
            this.btnClean.TabIndex = 1;
            this.btnClean.Text = "C&lean";
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(106, 16);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Sea&rch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.navBarControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 506);
            this.panel2.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.ExplorerBarGroupInterval = 2;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase,
            this.nbarDuration});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(220, 506);
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "Base";
            this.nbarBase.ControlContainer = this.navBarGroupBase;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 303;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.labState);
            this.navBarGroupBase.Controls.Add(this.labType);
            this.navBarGroupBase.Controls.Add(this.labShipLine);
            this.navBarGroupBase.Controls.Add(this.labNO);
            this.navBarGroupBase.Controls.Add(this.labName);
            this.navBarGroupBase.Controls.Add(this.labVIA);
            this.navBarGroupBase.Controls.Add(this.labPublisher);
            this.navBarGroupBase.Controls.Add(this.labDelivery);
            this.navBarGroupBase.Controls.Add(this.labPOD);
            this.navBarGroupBase.Controls.Add(this.labCarrier);
            this.navBarGroupBase.Controls.Add(this.labPOL);
            this.navBarGroupBase.Controls.Add(this.txtNO);
            this.navBarGroupBase.Controls.Add(this.txtName);
            this.navBarGroupBase.Controls.Add(this.txtVia);
            this.navBarGroupBase.Controls.Add(this.txtPublisher);
            this.navBarGroupBase.Controls.Add(this.txtDelivery);
            this.navBarGroupBase.Controls.Add(this.txtPOD);
            this.navBarGroupBase.Controls.Add(this.txtPOL);
            this.navBarGroupBase.Controls.Add(this.txtCarrier);
            this.navBarGroupBase.Controls.Add(this.cmbState);
            this.navBarGroupBase.Controls.Add(this.cmbType);
            this.navBarGroupBase.Controls.Add(this.cmbShipLine);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(212, 301);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // labState
            // 
            this.labState.Location = new System.Drawing.Point(5, 247);
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(30, 14);
            this.labState.TabIndex = 12;
            this.labState.Text = "State";
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(5, 221);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(28, 14);
            this.labType.TabIndex = 12;
            this.labType.Text = "Type";
            // 
            // labShipLine
            // 
            this.labShipLine.Location = new System.Drawing.Point(5, 86);
            this.labShipLine.Name = "labShipLine";
            this.labShipLine.Size = new System.Drawing.Size(45, 14);
            this.labShipLine.TabIndex = 12;
            this.labShipLine.Text = "ShipLine";
            // 
            // labNO
            // 
            this.labNO.Location = new System.Drawing.Point(5, 6);
            this.labNO.Name = "labNO";
            this.labNO.Size = new System.Drawing.Size(17, 14);
            this.labNO.TabIndex = 13;
            this.labNO.Text = "NO";
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(5, 32);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(31, 14);
            this.labName.TabIndex = 13;
            this.labName.Text = "Name";
            // 
            // labVIA
            // 
            this.labVIA.Location = new System.Drawing.Point(5, 140);
            this.labVIA.Name = "labVIA";
            this.labVIA.Size = new System.Drawing.Size(20, 14);
            this.labVIA.TabIndex = 14;
            this.labVIA.Text = "VIA";
            // 
            // labPublisher
            // 
            this.labPublisher.Location = new System.Drawing.Point(5, 273);
            this.labPublisher.Name = "labPublisher";
            this.labPublisher.Size = new System.Drawing.Size(48, 14);
            this.labPublisher.TabIndex = 14;
            this.labPublisher.Text = "Publisher";
            // 
            // labDelivery
            // 
            this.labDelivery.Location = new System.Drawing.Point(5, 194);
            this.labDelivery.Name = "labDelivery";
            this.labDelivery.Size = new System.Drawing.Size(42, 14);
            this.labDelivery.TabIndex = 14;
            this.labDelivery.Text = "Delivery";
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(5, 167);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(24, 14);
            this.labPOD.TabIndex = 14;
            this.labPOD.Text = "POD";
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(5, 59);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(34, 14);
            this.labCarrier.TabIndex = 14;
            this.labCarrier.Text = "Carrier";
            // 
            // labPOL
            // 
            this.labPOL.Location = new System.Drawing.Point(5, 113);
            this.labPOL.Name = "labPOL";
            this.labPOL.Size = new System.Drawing.Size(22, 14);
            this.labPOL.TabIndex = 14;
            this.labPOL.Text = "POL";
            // 
            // txtNO
            // 
            this.txtNO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNO.Location = new System.Drawing.Point(66, 3);
            this.txtNO.Name = "txtNO";
            this.txtNO.Size = new System.Drawing.Size(139, 21);
            this.txtNO.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(66, 29);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(139, 21);
            this.txtName.TabIndex = 1;
            // 
            // txtVia
            // 
            this.txtVia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVia.EditValue = "";
            this.txtVia.Location = new System.Drawing.Point(66, 137);
            this.txtVia.Name = "txtVia";
            this.txtVia.Size = new System.Drawing.Size(139, 21);
            this.txtVia.TabIndex = 5;
            // 
            // txtPublisher
            // 
            this.txtPublisher.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPublisher.EditText = "";
            this.txtPublisher.EditValue = null;
            this.txtPublisher.Location = new System.Drawing.Point(66, 270);
            this.txtPublisher.Name = "txtPublisher";
            this.txtPublisher.ReadOnly = false;
            this.txtPublisher.RefreshButtonToolTip = "";
            this.txtPublisher.ShowRefreshButton = false;
            this.txtPublisher.Size = new System.Drawing.Size(139, 21);
            this.txtPublisher.SpecifiedBackColor = System.Drawing.Color.White;
            this.txtPublisher.TabIndex = 10;
            this.txtPublisher.ToolTip = "";
            // 
            // txtDelivery
            // 
            this.txtDelivery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDelivery.Location = new System.Drawing.Point(66, 191);
            this.txtDelivery.Name = "txtDelivery";
            this.txtDelivery.Size = new System.Drawing.Size(139, 21);
            this.txtDelivery.TabIndex = 7;
            // 
            // txtPOD
            // 
            this.txtPOD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPOD.Location = new System.Drawing.Point(66, 164);
            this.txtPOD.Name = "txtPOD";
            this.txtPOD.Size = new System.Drawing.Size(139, 21);
            this.txtPOD.TabIndex = 6;
            // 
            // txtPOL
            // 
            this.txtPOL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPOL.Location = new System.Drawing.Point(66, 110);
            this.txtPOL.Name = "txtPOL";
            this.txtPOL.Size = new System.Drawing.Size(139, 21);
            this.txtPOL.TabIndex = 4;
            // 
            // txtCarrier
            // 
            this.txtCarrier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCarrier.Location = new System.Drawing.Point(66, 56);
            this.txtCarrier.Name = "txtCarrier";
            this.txtCarrier.Size = new System.Drawing.Size(139, 21);
            this.txtCarrier.TabIndex = 2;
            // 
            // cmbState
            // 
            this.cmbState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbState.Location = new System.Drawing.Point(66, 244);
            this.cmbState.Name = "cmbState";
            this.cmbState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbState.Size = new System.Drawing.Size(139, 21);
            this.cmbState.TabIndex = 9;
            // 
            // cmbType
            // 
            this.cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType.Location = new System.Drawing.Point(66, 218);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Size = new System.Drawing.Size(139, 21);
            this.cmbType.TabIndex = 8;
            // 
            // cmbShipLine
            // 
            this.cmbShipLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbShipLine.Location = new System.Drawing.Point(66, 83);
            this.cmbShipLine.Name = "cmbShipLine";
            this.cmbShipLine.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbShipLine.Size = new System.Drawing.Size(139, 21);
            this.cmbShipLine.TabIndex = 3;
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.dteTo);
            this.navBarGroupControlContainer1.Controls.Add(this.dteFrom);
            this.navBarGroupControlContainer1.Controls.Add(this.labTo);
            this.navBarGroupControlContainer1.Controls.Add(this.labFrom);
            this.navBarGroupControlContainer1.Controls.Add(this.chkDate);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(212, 85);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // dteTo
            // 
            this.dteTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dteTo.EditValue = null;
            this.dteTo.Enabled = false;
            this.dteTo.Location = new System.Drawing.Point(66, 55);
            this.dteTo.Name = "dteTo";
            this.dteTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteTo.Size = new System.Drawing.Size(139, 21);
            this.dteTo.TabIndex = 2;
            // 
            // dteFrom
            // 
            this.dteFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dteFrom.EditValue = null;
            this.dteFrom.Enabled = false;
            this.dteFrom.Location = new System.Drawing.Point(66, 28);
            this.dteFrom.Name = "dteFrom";
            this.dteFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFrom.Size = new System.Drawing.Size(139, 21);
            this.dteFrom.TabIndex = 1;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(5, 58);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 12;
            this.labTo.Text = "To";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(5, 31);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(27, 14);
            this.labFrom.TabIndex = 12;
            this.labFrom.Text = "From";
            // 
            // chkDate
            // 
            this.chkDate.EditValue = true;
            this.chkDate.Location = new System.Drawing.Point(3, 3);
            this.chkDate.Name = "chkDate";
            this.chkDate.Properties.Caption = "Date";
            this.chkDate.Size = new System.Drawing.Size(182, 19);
            this.chkDate.TabIndex = 0;
            this.chkDate.CheckedChanged += new System.EventHandler(this.chkDate_CheckedChanged);
            // 
            // nbarDuration
            // 
            this.nbarDuration.Caption = "Duration";
            this.nbarDuration.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarDuration.Expanded = true;
            this.nbarDuration.GroupClientHeight = 87;
            this.nbarDuration.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarDuration.Name = "nbarDuration";
            // 
            // OPSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.IsMultiLanguage = false;
            this.Name = "OPSearchPart";
            this.Size = new System.Drawing.Size(220, 565);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShipLine.Properties)).EndInit();
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraEditors.LabelControl labShipLine;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labNO;
        private DevExpress.XtraEditors.TextEdit txtNO;
        private DevExpress.XtraEditors.LabelControl labVIA;
        private DevExpress.XtraEditors.TextEdit txtVia;
        private DevExpress.XtraEditors.LabelControl labCarrier;
        private DevExpress.XtraEditors.TextEdit txtCarrier;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private DevExpress.XtraEditors.LabelControl labPOL;
        private DevExpress.XtraEditors.TextEdit txtPOD;
        private DevExpress.XtraEditors.TextEdit txtPOL;
        private DevExpress.XtraEditors.LabelControl labState;
        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbState;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbType;
        private DevExpress.XtraEditors.LabelControl labPublisher;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox txtPublisher;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroup nbarDuration;
        private DevExpress.XtraEditors.DateEdit dteTo;
        private DevExpress.XtraEditors.DateEdit dteFrom;
        private DevExpress.XtraEditors.CheckEdit chkDate;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraEditors.SimpleButton btnClean;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbShipLine;
        private DevExpress.XtraEditors.LabelControl labDelivery;
        private DevExpress.XtraEditors.TextEdit txtDelivery;
    }
}
