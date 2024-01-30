namespace ICP.Common.UI.TransportFoundation.VesselVoyage
{
    partial class VoyageSearchPart
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
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.lwchkIsValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.labIsValid = new DevExpress.XtraEditors.LabelControl();
            this.numMax = new DevExpress.XtraEditors.SpinEdit();
            this.labMax = new DevExpress.XtraEditors.LabelControl();
            this.labVesselName = new DevExpress.XtraEditors.LabelControl();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.txtVesselName = new DevExpress.XtraEditors.TextEdit();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.dteEtdFrom = new DevExpress.XtraEditors.DateEdit();
            this.dteEtaFrom = new DevExpress.XtraEditors.DateEdit();
            this.labEtdFrom = new DevExpress.XtraEditors.LabelControl();
            this.labEtaFrom = new DevExpress.XtraEditors.LabelControl();
            this.dteEtdTo = new DevExpress.XtraEditors.DateEdit();
            this.dteEtaTo = new DevExpress.XtraEditors.DateEdit();
            this.labEtdTo = new DevExpress.XtraEditors.LabelControl();
            this.labEtaTo = new DevExpress.XtraEditors.LabelControl();
            this.chkEtd = new DevExpress.XtraEditors.CheckEdit();
            this.chkEta = new DevExpress.XtraEditors.CheckEdit();
            this.navDate = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVesselName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteEtdFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEtdFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEtaFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEtaFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEtdTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEtdTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEtaTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEtaTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEtd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEta.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(207, 155);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "Date";
            this.navBarGroup1.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 157;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 409);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(200, 55);
            this.panelControl1.TabIndex = 5;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(25, 15);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(106, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Sea&rch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panel1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(200, 409);
            this.panelControl2.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.navBarControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(196, 405);
            this.panel1.TabIndex = 0;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupInterval = 10;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 10;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase,
            this.navDate});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(196, 401);
            this.navBarControl1.TabIndex = 1;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "Base";
            this.nbarBase.ControlContainer = this.navBarGroupBase;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 159;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.lwchkIsValid);
            this.navBarGroupBase.Controls.Add(this.labIsValid);
            this.navBarGroupBase.Controls.Add(this.numMax);
            this.navBarGroupBase.Controls.Add(this.labMax);
            this.navBarGroupBase.Controls.Add(this.labVesselName);
            this.navBarGroupBase.Controls.Add(this.labNo);
            this.navBarGroupBase.Controls.Add(this.txtVesselName);
            this.navBarGroupBase.Controls.Add(this.txtNo);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(172, 157);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // lwchkIsValid
            // 
            this.lwchkIsValid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lwchkIsValid.Checked = true;
            this.lwchkIsValid.CheckedText = "Valid";
            this.lwchkIsValid.Location = new System.Drawing.Point(69, 59);
            this.lwchkIsValid.Name = "lwchkIsValid";
            this.lwchkIsValid.NULLText = "ALL";
            this.lwchkIsValid.Size = new System.Drawing.Size(98, 21);
            this.lwchkIsValid.TabIndex = 3;
            this.lwchkIsValid.UnCheckedText = "UnValid";
            // 
            // labIsValid
            // 
            this.labIsValid.Location = new System.Drawing.Point(4, 62);
            this.labIsValid.Name = "labIsValid";
            this.labIsValid.Size = new System.Drawing.Size(25, 14);
            this.labIsValid.TabIndex = 14;
            this.labIsValid.Text = "Valid";
            // 
            // numMax
            // 
            this.numMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numMax.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMax.Location = new System.Drawing.Point(74, 83);
            this.numMax.Name = "numMax";
            this.numMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMax.Properties.IsFloatValue = false;
            this.numMax.Properties.Mask.EditMask = "N00";
            this.numMax.Properties.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMax.Size = new System.Drawing.Size(93, 21);
            this.numMax.TabIndex = 4;
            // 
            // labMax
            // 
            this.labMax.Location = new System.Drawing.Point(3, 86);
            this.labMax.Name = "labMax";
            this.labMax.Size = new System.Drawing.Size(58, 14);
            this.labMax.TabIndex = 13;
            this.labMax.Text = "Max Count";
            // 
            // labVesselName
            // 
            this.labVesselName.Location = new System.Drawing.Point(5, 10);
            this.labVesselName.Name = "labVesselName";
            this.labVesselName.Size = new System.Drawing.Size(48, 14);
            this.labVesselName.TabIndex = 6;
            this.labVesselName.Text = "Vov/Voe";
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(5, 34);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(15, 14);
            this.labNo.TabIndex = 6;
            this.labNo.Text = "No";
            // 
            // txtVesselName
            // 
            this.txtVesselName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVesselName.Location = new System.Drawing.Point(76, 7);
            this.txtVesselName.Name = "txtVesselName";
            this.txtVesselName.Size = new System.Drawing.Size(93, 21);
            this.txtVesselName.TabIndex = 0;
            // 
            // txtNo
            // 
            this.txtNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNo.Location = new System.Drawing.Point(76, 31);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(93, 21);
            this.txtNo.TabIndex = 1;
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.dteEtdFrom);
            this.navBarGroupControlContainer2.Controls.Add(this.dteEtaFrom);
            this.navBarGroupControlContainer2.Controls.Add(this.labEtdFrom);
            this.navBarGroupControlContainer2.Controls.Add(this.labEtaFrom);
            this.navBarGroupControlContainer2.Controls.Add(this.dteEtdTo);
            this.navBarGroupControlContainer2.Controls.Add(this.dteEtaTo);
            this.navBarGroupControlContainer2.Controls.Add(this.labEtdTo);
            this.navBarGroupControlContainer2.Controls.Add(this.labEtaTo);
            this.navBarGroupControlContainer2.Controls.Add(this.chkEtd);
            this.navBarGroupControlContainer2.Controls.Add(this.chkEta);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(172, 149);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // dteEtdFrom
            // 
            this.dteEtdFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dteEtdFrom.EditValue = null;
            this.dteEtdFrom.Enabled = false;
            this.dteEtdFrom.Location = new System.Drawing.Point(76, 25);
            this.dteEtdFrom.Name = "dteEtdFrom";
            this.dteEtdFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEtdFrom.Properties.Mask.EditMask = global::ICP.Common.UI.Resources.Resource_EN.RERH;
            this.dteEtdFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteEtdFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteEtdFrom.Size = new System.Drawing.Size(93, 21);
            this.dteEtdFrom.TabIndex = 49;
            // 
            // dteEtaFrom
            // 
            this.dteEtaFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dteEtaFrom.EditValue = null;
            this.dteEtaFrom.Enabled = false;
            this.dteEtaFrom.Location = new System.Drawing.Point(76, 96);
            this.dteEtaFrom.Name = "dteEtaFrom";
            this.dteEtaFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEtaFrom.Properties.Mask.EditMask = global::ICP.Common.UI.Resources.Resource_EN.RERH;
            this.dteEtaFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteEtaFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteEtaFrom.Size = new System.Drawing.Size(93, 21);
            this.dteEtaFrom.TabIndex = 49;
            // 
            // labEtdFrom
            // 
            this.labEtdFrom.Location = new System.Drawing.Point(5, 28);
            this.labEtdFrom.Name = "labEtdFrom";
            this.labEtdFrom.Size = new System.Drawing.Size(27, 14);
            this.labEtdFrom.TabIndex = 51;
            this.labEtdFrom.Text = "From";
            // 
            // labEtaFrom
            // 
            this.labEtaFrom.Location = new System.Drawing.Point(5, 99);
            this.labEtaFrom.Name = "labEtaFrom";
            this.labEtaFrom.Size = new System.Drawing.Size(27, 14);
            this.labEtaFrom.TabIndex = 51;
            this.labEtaFrom.Text = "From";
            // 
            // dteEtdTo
            // 
            this.dteEtdTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dteEtdTo.EditValue = null;
            this.dteEtdTo.Enabled = false;
            this.dteEtdTo.Location = new System.Drawing.Point(76, 49);
            this.dteEtdTo.Name = "dteEtdTo";
            this.dteEtdTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEtdTo.Properties.Mask.EditMask = global::ICP.Common.UI.Resources.Resource_EN.RERH;
            this.dteEtdTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteEtdTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteEtdTo.Size = new System.Drawing.Size(93, 21);
            this.dteEtdTo.TabIndex = 50;
            // 
            // dteEtaTo
            // 
            this.dteEtaTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dteEtaTo.EditValue = null;
            this.dteEtaTo.Enabled = false;
            this.dteEtaTo.Location = new System.Drawing.Point(76, 120);
            this.dteEtaTo.Name = "dteEtaTo";
            this.dteEtaTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEtaTo.Properties.Mask.EditMask = global::ICP.Common.UI.Resources.Resource_EN.RERH;
            this.dteEtaTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteEtaTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteEtaTo.Size = new System.Drawing.Size(93, 21);
            this.dteEtaTo.TabIndex = 50;
            // 
            // labEtdTo
            // 
            this.labEtdTo.Location = new System.Drawing.Point(5, 52);
            this.labEtdTo.Name = "labEtdTo";
            this.labEtdTo.Size = new System.Drawing.Size(15, 14);
            this.labEtdTo.TabIndex = 52;
            this.labEtdTo.Text = "To";
            // 
            // labEtaTo
            // 
            this.labEtaTo.Location = new System.Drawing.Point(5, 123);
            this.labEtaTo.Name = "labEtaTo";
            this.labEtaTo.Size = new System.Drawing.Size(15, 14);
            this.labEtaTo.TabIndex = 52;
            this.labEtaTo.Text = "To";
            // 
            // chkEtd
            // 
            this.chkEtd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEtd.Location = new System.Drawing.Point(5, 3);
            this.chkEtd.Name = "chkEtd";
            this.chkEtd.Properties.Caption = "ETD";
            this.chkEtd.Size = new System.Drawing.Size(125, 19);
            this.chkEtd.TabIndex = 48;
            this.chkEtd.CheckedChanged += new System.EventHandler(this.chkETD_CheckedChanged);
            // 
            // chkEta
            // 
            this.chkEta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEta.Location = new System.Drawing.Point(5, 74);
            this.chkEta.Name = "chkEta";
            this.chkEta.Properties.Caption = "ETA";
            this.chkEta.Size = new System.Drawing.Size(125, 19);
            this.chkEta.TabIndex = 48;
            this.chkEta.CheckedChanged += new System.EventHandler(this.chkETA_CheckedChanged);
            // 
            // navDate
            // 
            this.navDate.Caption = "Date";
            this.navDate.ControlContainer = this.navBarGroupControlContainer2;
            this.navDate.Expanded = true;
            this.navDate.GroupClientHeight = 151;
            this.navDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navDate.Name = "navDate";
            this.navDate.Visible = false;
            // 
            // VoyageSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "VoyageSearchPart";
            this.Size = new System.Drawing.Size(200, 464);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVesselName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteEtdFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEtdFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEtaFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEtaFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEtdTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEtdTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEtaTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEtaTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEtd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEta.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraEditors.SpinEdit numMax;
        private DevExpress.XtraEditors.LabelControl labMax;
        private DevExpress.XtraEditors.LabelControl labVesselName;
        private DevExpress.XtraEditors.LabelControl labNo;
        protected DevExpress.XtraEditors.TextEdit txtVesselName;
        protected DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraEditors.DateEdit dteEtdFrom;
        private DevExpress.XtraEditors.DateEdit dteEtaFrom;
        private DevExpress.XtraEditors.LabelControl labEtdFrom;
        private DevExpress.XtraEditors.LabelControl labEtaFrom;
        private DevExpress.XtraEditors.DateEdit dteEtdTo;
        private DevExpress.XtraEditors.DateEdit dteEtaTo;
        private DevExpress.XtraEditors.LabelControl labEtdTo;
        private DevExpress.XtraEditors.LabelControl labEtaTo;
        private DevExpress.XtraEditors.CheckEdit chkEtd;
        private DevExpress.XtraEditors.CheckEdit chkEta;
        private DevExpress.XtraNavBar.NavBarGroup navDate;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton lwchkIsValid;
        private DevExpress.XtraEditors.LabelControl labIsValid;
    }
}
