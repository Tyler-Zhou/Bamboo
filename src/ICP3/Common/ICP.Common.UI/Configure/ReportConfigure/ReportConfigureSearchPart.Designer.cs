namespace ICP.Common.UI.Configure.ReportConfigure
{
    partial class ReportConfigureSearchPart
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtCDescription = new DevExpress.XtraEditors.TextEdit();
            this.labCName = new DevExpress.XtraEditors.LabelControl();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.labEName = new DevExpress.XtraEditors.LabelControl();
            this.txtEDescription = new DevExpress.XtraEditors.TextEdit();
            this.labReportType = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.lwchkIsValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.labIsValid = new DevExpress.XtraEditors.LabelControl();
            this.numMax = new DevExpress.XtraEditors.SpinEdit();
            this.labMax = new DevExpress.XtraEditors.LabelControl();
            this.bsChargingGroup = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsChargingGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 374);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(200, 59);
            this.panelControl1.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(21, 18);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(104, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Sea&rch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panel1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(200, 374);
            this.panelControl2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.navBarControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(196, 370);
            this.panel1.TabIndex = 0;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupInterval = 10;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 10;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(196, 366);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "Base";
            this.nbarBase.ControlContainer = this.navBarGroupBase;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 280;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.txtCode);
            this.navBarGroupBase.Controls.Add(this.txtCDescription);
            this.navBarGroupBase.Controls.Add(this.labCName);
            this.navBarGroupBase.Controls.Add(this.labCode);
            this.navBarGroupBase.Controls.Add(this.labEName);
            this.navBarGroupBase.Controls.Add(this.txtEDescription);
            this.navBarGroupBase.Controls.Add(this.labReportType);
            this.navBarGroupBase.Controls.Add(this.comboBoxType);
            this.navBarGroupBase.Controls.Add(this.lwchkIsValid);
            this.navBarGroupBase.Controls.Add(this.labIsValid);
            this.navBarGroupBase.Controls.Add(this.numMax);
            this.navBarGroupBase.Controls.Add(this.labMax);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(172, 278);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(63, 4);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(104, 21);
            this.txtCode.TabIndex = 0;
            // 
            // txtCDescription
            // 
            this.txtCDescription.Location = new System.Drawing.Point(63, 29);
            this.txtCDescription.Name = "txtCDescription";
            this.txtCDescription.Size = new System.Drawing.Size(104, 21);
            this.txtCDescription.TabIndex = 1;
            this.txtCDescription.TabStop = false;
            // 
            // labCName
            // 
            this.labCName.Location = new System.Drawing.Point(2, 32);
            this.labCName.Name = "labCName";
            this.labCName.Size = new System.Drawing.Size(48, 14);
            this.labCName.TabIndex = 53;
            this.labCName.Text = "中文描述";
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(2, 7);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(24, 14);
            this.labCode.TabIndex = 46;
            this.labCode.Text = "代码";
            // 
            // labEName
            // 
            this.labEName.Location = new System.Drawing.Point(2, 57);
            this.labEName.Name = "labEName";
            this.labEName.Size = new System.Drawing.Size(48, 14);
            this.labEName.TabIndex = 54;
            this.labEName.Text = "英文描述";
            // 
            // txtEDescription
            // 
            this.txtEDescription.Location = new System.Drawing.Point(63, 54);
            this.txtEDescription.Name = "txtEDescription";
            this.txtEDescription.Size = new System.Drawing.Size(104, 21);
            this.txtEDescription.TabIndex = 2;
            this.txtEDescription.TabStop = false;
            // 
            // labReportType
            // 
            this.labReportType.Location = new System.Drawing.Point(2, 82);
            this.labReportType.Name = "labReportType";
            this.labReportType.Size = new System.Drawing.Size(24, 14);
            this.labReportType.TabIndex = 46;
            this.labReportType.Text = "类型";
            // 
            // comboBoxType
            // 
            this.comboBoxType.Location = new System.Drawing.Point(63, 79);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxType.Size = new System.Drawing.Size(104, 21);
            this.comboBoxType.SpecifiedBackColor = System.Drawing.Color.White;
            this.comboBoxType.TabIndex = 36;
            // 
            // lwchkIsValid
            // 
            this.lwchkIsValid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lwchkIsValid.Checked = true;
            this.lwchkIsValid.CheckedText = "Valid";
            this.lwchkIsValid.Location = new System.Drawing.Point(63, 106);
            this.lwchkIsValid.Name = "lwchkIsValid";
            this.lwchkIsValid.NULLText = "ALL";
            this.lwchkIsValid.Size = new System.Drawing.Size(104, 21);
            this.lwchkIsValid.TabIndex = 2;
            this.lwchkIsValid.UnCheckedText = "UnValid";
            // 
            // labIsValid
            // 
            this.labIsValid.Location = new System.Drawing.Point(2, 109);
            this.labIsValid.Name = "labIsValid";
            this.labIsValid.Size = new System.Drawing.Size(34, 14);
            this.labIsValid.TabIndex = 4;
            this.labIsValid.Text = "IsValid";
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
            this.numMax.Location = new System.Drawing.Point(63, 131);
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
            this.numMax.Size = new System.Drawing.Size(104, 21);
            this.numMax.TabIndex = 3;
            // 
            // labMax
            // 
            this.labMax.Location = new System.Drawing.Point(2, 134);
            this.labMax.Name = "labMax";
            this.labMax.Size = new System.Drawing.Size(58, 14);
            this.labMax.TabIndex = 1;
            this.labMax.Text = "Max Count";
            // 
            // bsChargingGroup
            // 
            this.bsChargingGroup.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.ChargingGroupList);
            // 
            // ReportConfigureSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "ReportConfigureSearchPart";
            this.Size = new System.Drawing.Size(200, 433);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsChargingGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

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
        private ICP.Framework.ClientComponents.Controls.LWCheckButton lwchkIsValid;
        private DevExpress.XtraEditors.LabelControl labIsValid;
        private System.Windows.Forms.BindingSource bsChargingGroup;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.LabelControl labReportType;
        private DevExpress.XtraEditors.LabelControl labEName;
        private DevExpress.XtraEditors.TextEdit txtCDescription;
        private DevExpress.XtraEditors.LabelControl labCName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtEDescription;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit comboBoxType;
    }
}
