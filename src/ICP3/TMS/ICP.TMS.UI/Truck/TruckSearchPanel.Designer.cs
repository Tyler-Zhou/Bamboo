namespace ICP.TMS.UI
{
    partial class TruckSearchPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TruckSearchPanel));
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup3 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cmbDataType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.dmcTime = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labDataType = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupBaseInfo = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.ckbValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.txtType = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labTruckType = new DevExpress.XtraEditors.LabelControl();
            this.txtTruckNo = new DevExpress.XtraEditors.TextEdit();
            this.labTruckNo = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDataType.Properties)).BeginInit();
            this.navBarGroupBaseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTruckNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup3;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer3);
            this.navBarControl1.Controls.Add(this.navBarGroupBaseInfo);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupInterval = 2;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.navBarGroup3});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(209, 442);
            this.navBarControl1.TabIndex = 6;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup3
            // 
            this.navBarGroup3.Caption = "时间查询";
            this.navBarGroup3.ControlContainer = this.navBarGroupControlContainer3;
            this.navBarGroup3.Expanded = true;
            this.navBarGroup3.GroupClientHeight = 187;
            this.navBarGroup3.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup3.Name = "navBarGroup3";
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.cmbDataType);
            this.navBarGroupControlContainer3.Controls.Add(this.dmcTime);
            this.navBarGroupControlContainer3.Controls.Add(this.labTo);
            this.navBarGroupControlContainer3.Controls.Add(this.labDataType);
            this.navBarGroupControlContainer3.Controls.Add(this.labFrom);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(201, 185);
            this.navBarGroupControlContainer3.TabIndex = 0;
            // 
            // cmbDataType
            // 
            this.cmbDataType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDataType.Location = new System.Drawing.Point(80, 6);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbDataType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbDataType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDataType.Size = new System.Drawing.Size(116, 21);
            this.cmbDataType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbDataType.TabIndex = 38;
            // 
            // dmcTime
            // 
            this.dmcTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dmcTime.IsEngish = false;
            this.dmcTime.Location = new System.Drawing.Point(80, 30);
            this.dmcTime.Name = "dmcTime";
            this.dmcTime.Size = new System.Drawing.Size(116, 142);
            this.dmcTime.TabIndex = 0;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(5, 145);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(48, 14);
            this.labTo.TabIndex = 37;
            this.labTo.Text = "结束日期";
            // 
            // labDataType
            // 
            this.labDataType.Location = new System.Drawing.Point(5, 9);
            this.labDataType.Name = "labDataType";
            this.labDataType.Size = new System.Drawing.Size(24, 14);
            this.labDataType.TabIndex = 36;
            this.labDataType.Text = "类型";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(5, 121);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(48, 14);
            this.labFrom.TabIndex = 36;
            this.labFrom.Text = "开始日期";
            // 
            // navBarGroupBaseInfo
            // 
            this.navBarGroupBaseInfo.Controls.Add(this.ckbValid);
            this.navBarGroupBaseInfo.Controls.Add(this.txtType);
            this.navBarGroupBaseInfo.Controls.Add(this.labelControl1);
            this.navBarGroupBaseInfo.Controls.Add(this.labTruckType);
            this.navBarGroupBaseInfo.Controls.Add(this.txtTruckNo);
            this.navBarGroupBaseInfo.Controls.Add(this.labTruckNo);
            this.navBarGroupBaseInfo.Name = "navBarGroupBaseInfo";
            this.navBarGroupBaseInfo.Size = new System.Drawing.Size(201, 96);
            this.navBarGroupBaseInfo.TabIndex = 3;
            // 
            // ckbValid
            // 
            this.ckbValid.Checked = null;
            this.ckbValid.CheckedText = "TRUE";
            this.ckbValid.Location = new System.Drawing.Point(80, 62);
            this.ckbValid.Name = "ckbValid";
            this.ckbValid.NULLText = "ALL";
            this.ckbValid.Size = new System.Drawing.Size(116, 22);
            this.ckbValid.TabIndex = 2;
            this.ckbValid.UnCheckedText = "FALSE";
            // 
            // txtType
            // 
            this.txtType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtType.Location = new System.Drawing.Point(80, 34);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(122, 21);
            this.txtType.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 66);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 31;
            this.labelControl1.Text = "有效性";
            // 
            // labTruckType
            // 
            this.labTruckType.Location = new System.Drawing.Point(5, 37);
            this.labTruckType.Name = "labTruckType";
            this.labTruckType.Size = new System.Drawing.Size(24, 14);
            this.labTruckType.TabIndex = 31;
            this.labTruckType.Text = "型号";
            // 
            // txtTruckNo
            // 
            this.txtTruckNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTruckNo.Location = new System.Drawing.Point(80, 7);
            this.txtTruckNo.Name = "txtTruckNo";
            this.txtTruckNo.Size = new System.Drawing.Size(124, 21);
            this.txtTruckNo.TabIndex = 0;
            // 
            // labTruckNo
            // 
            this.labTruckNo.Location = new System.Drawing.Point(5, 7);
            this.labTruckNo.Name = "labTruckNo";
            this.labTruckNo.Size = new System.Drawing.Size(36, 14);
            this.labTruckNo.TabIndex = 25;
            this.labTruckNo.Text = "车牌号";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "基本条件";
            this.navBarGroup1.ControlContainer = this.navBarGroupBaseInfo;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 98;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 476);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 56);
            this.panel1.TabIndex = 7;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(26, 19);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "清空(&L)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(126, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // TruckSearchPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.navBarControl1);
            this.Name = "TruckSearchPanel";
            this.Size = new System.Drawing.Size(209, 532);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer3.ResumeLayout(false);
            this.navBarGroupControlContainer3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDataType.Properties)).EndInit();
            this.navBarGroupBaseInfo.ResumeLayout(false);
            this.navBarGroupBaseInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTruckNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup3;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        protected DevExpress.XtraEditors.LabelControl labTo;
        protected DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBaseInfo;
        private DevExpress.XtraEditors.TextEdit txtType;
        private DevExpress.XtraEditors.LabelControl labTruckType;
        private DevExpress.XtraEditors.TextEdit txtTruckNo;
        private DevExpress.XtraEditors.LabelControl labTruckNo;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private ICP.Framework.ClientComponents.Controls.DateMonthControl dmcTime;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbDataType;
        protected DevExpress.XtraEditors.LabelControl labDataType;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton ckbValid;
    }
}
