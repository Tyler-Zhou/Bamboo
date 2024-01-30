namespace ICP.Common.UI.Configure.ChargingCode
{
    partial class ChargingCodeFinderSearchPart
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
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.numMax = new DevExpress.XtraEditors.SpinEdit();
            this.chkValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.chkIsConnission = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.chkIsAgent = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labMaxCount = new DevExpress.XtraEditors.LabelControl();
            this.labValid = new DevExpress.XtraEditors.LabelControl();
            this.labIsConnission = new DevExpress.XtraEditors.LabelControl();
            this.labIsAgent = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupInterval = 2;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase});
            this.navBarControl1.Location = new System.Drawing.Point(2, 2);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(197, 221);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "Base";
            this.nbarBase.ControlContainer = this.navBarGroupBase;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 161;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.numMax);
            this.navBarGroupBase.Controls.Add(this.chkValid);
            this.navBarGroupBase.Controls.Add(this.chkIsConnission);
            this.navBarGroupBase.Controls.Add(this.chkIsAgent);
            this.navBarGroupBase.Controls.Add(this.txtName);
            this.navBarGroupBase.Controls.Add(this.labMaxCount);
            this.navBarGroupBase.Controls.Add(this.labValid);
            this.navBarGroupBase.Controls.Add(this.labIsConnission);
            this.navBarGroupBase.Controls.Add(this.labIsAgent);
            this.navBarGroupBase.Controls.Add(this.labName);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(189, 159);
            this.navBarGroupBase.TabIndex = 0;
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
            this.numMax.Location = new System.Drawing.Point(72, 123);
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
            this.numMax.Size = new System.Drawing.Size(112, 21);
            this.numMax.TabIndex = 4;
            // 
            // chkValid
            // 
            this.chkValid.Checked = null;
            this.chkValid.CheckedText = "TRUE";
            this.chkValid.Location = new System.Drawing.Point(69, 95);
            this.chkValid.Name = "chkValid";
            this.chkValid.NULLText = "ALL";
            this.chkValid.Size = new System.Drawing.Size(114, 22);
            this.chkValid.TabIndex = 3;
            this.chkValid.UnCheckedText = "FALSE";
            // 
            // chkIsConnission
            // 
            this.chkIsConnission.Checked = null;
            this.chkIsConnission.CheckedText = "TRUE";
            this.chkIsConnission.Location = new System.Drawing.Point(69, 67);
            this.chkIsConnission.Name = "chkIsConnission";
            this.chkIsConnission.NULLText = "ALL";
            this.chkIsConnission.Size = new System.Drawing.Size(114, 22);
            this.chkIsConnission.TabIndex = 2;
            this.chkIsConnission.UnCheckedText = "FALSE";
            // 
            // chkIsAgent
            // 
            this.chkIsAgent.Checked = null;
            this.chkIsAgent.CheckedText = "TRUE";
            this.chkIsAgent.Location = new System.Drawing.Point(69, 39);
            this.chkIsAgent.Name = "chkIsAgent";
            this.chkIsAgent.NULLText = "ALL";
            this.chkIsAgent.Size = new System.Drawing.Size(114, 22);
            this.chkIsAgent.TabIndex = 1;
            this.chkIsAgent.UnCheckedText = "FALSE";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(72, 11);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(112, 21);
            this.txtName.TabIndex = 0;
            // 
            // labMaxCount
            // 
            this.labMaxCount.Location = new System.Drawing.Point(10, 126);
            this.labMaxCount.Name = "labMaxCount";
            this.labMaxCount.Size = new System.Drawing.Size(48, 14);
            this.labMaxCount.TabIndex = 0;
            this.labMaxCount.Text = "最大行数";
            // 
            // labValid
            // 
            this.labValid.Location = new System.Drawing.Point(10, 99);
            this.labValid.Name = "labValid";
            this.labValid.Size = new System.Drawing.Size(24, 14);
            this.labValid.TabIndex = 0;
            this.labValid.Text = "有效";
            // 
            // labIsConnission
            // 
            this.labIsConnission.Location = new System.Drawing.Point(10, 71);
            this.labIsConnission.Name = "labIsConnission";
            this.labIsConnission.Size = new System.Drawing.Size(24, 14);
            this.labIsConnission.TabIndex = 0;
            this.labIsConnission.Text = "佣金";
            // 
            // labIsAgent
            // 
            this.labIsAgent.Location = new System.Drawing.Point(10, 43);
            this.labIsAgent.Name = "labIsAgent";
            this.labIsAgent.Size = new System.Drawing.Size(24, 14);
            this.labIsAgent.TabIndex = 0;
            this.labIsAgent.Text = "代理";
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(10, 13);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(24, 14);
            this.labName.TabIndex = 0;
            this.labName.Text = "名称";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.navBarControl1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(201, 466);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnSearch);
            this.pnlBottom.Controls.Add(this.btnClear);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 466);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(201, 45);
            this.pnlBottom.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(114, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(22, 9);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // ChargingCodeFinderSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Name = "ChargingCodeFinderSearchPart";
            this.Size = new System.Drawing.Size(201, 511);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.LabelControl labMaxCount;
        private DevExpress.XtraEditors.LabelControl labValid;
        private DevExpress.XtraEditors.LabelControl labIsConnission;
        private DevExpress.XtraEditors.LabelControl labIsAgent;
        private DevExpress.XtraEditors.LabelControl labName;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton chkValid;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton chkIsConnission;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton chkIsAgent;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.SpinEdit numMax;




    }
}
