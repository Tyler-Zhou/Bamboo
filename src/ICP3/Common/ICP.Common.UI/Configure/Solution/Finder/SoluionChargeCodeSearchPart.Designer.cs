namespace ICP.Common.UI.Configure.Solution
{
    partial class SoluionChargeCodeSearchPart
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
            this.pnlButtom = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.chkIsAgent = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.chkIsCommission = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.ckbValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.labIsAgent = new DevExpress.XtraEditors.LabelControl();
            this.labIsCommission = new DevExpress.XtraEditors.LabelControl();
            this.numMaxRow = new DevExpress.XtraEditors.SpinEdit();
            this.labValid = new DevExpress.XtraEditors.LabelControl();
            this.labMaxCount = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtom)).BeginInit();
            this.pnlButtom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxRow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlButtom
            // 
            this.pnlButtom.Controls.Add(this.btnSearch);
            this.pnlButtom.Controls.Add(this.btnClear);
            this.pnlButtom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtom.Location = new System.Drawing.Point(0, 483);
            this.pnlButtom.Name = "pnlButtom";
            this.pnlButtom.Size = new System.Drawing.Size(233, 52);
            this.pnlButtom.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(119, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(26, 15);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupInterval = 10;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 10;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarBase});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 233;
            this.navBarControl1.Size = new System.Drawing.Size(233, 267);
            this.navBarControl1.TabIndex = 2;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarBase
            // 
            this.navBarBase.Caption = "Base Info";
            this.navBarBase.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarBase.Expanded = true;
            this.navBarBase.GroupClientHeight = 149;
            this.navBarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBase.Name = "navBarBase";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.chkIsAgent);
            this.navBarGroupControlContainer1.Controls.Add(this.chkIsCommission);
            this.navBarGroupControlContainer1.Controls.Add(this.ckbValid);
            this.navBarGroupControlContainer1.Controls.Add(this.labIsAgent);
            this.navBarGroupControlContainer1.Controls.Add(this.labIsCommission);
            this.navBarGroupControlContainer1.Controls.Add(this.numMaxRow);
            this.navBarGroupControlContainer1.Controls.Add(this.labValid);
            this.navBarGroupControlContainer1.Controls.Add(this.labMaxCount);
            this.navBarGroupControlContainer1.Controls.Add(this.labName);
            this.navBarGroupControlContainer1.Controls.Add(this.txtName);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(209, 147);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // chkIsAgent
            // 
            this.chkIsAgent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIsAgent.Checked = null;
            this.chkIsAgent.CheckedText = "TRUE";
            this.chkIsAgent.Location = new System.Drawing.Point(83, 36);
            this.chkIsAgent.Name = "chkIsAgent";
            this.chkIsAgent.NULLText = "ALL";
            this.chkIsAgent.Size = new System.Drawing.Size(118, 21);
            this.chkIsAgent.TabIndex = 8;
            this.chkIsAgent.UnCheckedText = "FALSE";
            // 
            // chkIsCommission
            // 
            this.chkIsCommission.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIsCommission.Checked = null;
            this.chkIsCommission.CheckedText = "TRUE";
            this.chkIsCommission.Location = new System.Drawing.Point(83, 63);
            this.chkIsCommission.Name = "chkIsCommission";
            this.chkIsCommission.NULLText = "ALL";
            this.chkIsCommission.Size = new System.Drawing.Size(118, 21);
            this.chkIsCommission.TabIndex = 8;
            this.chkIsCommission.UnCheckedText = "FALSE";
            // 
            // ckbValid
            // 
            this.ckbValid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbValid.Checked = true;
            this.ckbValid.CheckedText = "TRUE";
            this.ckbValid.Enabled = false;
            this.ckbValid.Location = new System.Drawing.Point(83, 90);
            this.ckbValid.Name = "ckbValid";
            this.ckbValid.NULLText = "ALL";
            this.ckbValid.Size = new System.Drawing.Size(118, 21);
            this.ckbValid.TabIndex = 8;
            this.ckbValid.UnCheckedText = "FALSE";
            // 
            // labIsAgent
            // 
            this.labIsAgent.Location = new System.Drawing.Point(7, 39);
            this.labIsAgent.Name = "labIsAgent";
            this.labIsAgent.Size = new System.Drawing.Size(43, 14);
            this.labIsAgent.TabIndex = 6;
            this.labIsAgent.Text = "IsAgent";
            // 
            // labIsCommission
            // 
            this.labIsCommission.Location = new System.Drawing.Point(7, 66);
            this.labIsCommission.Name = "labIsCommission";
            this.labIsCommission.Size = new System.Drawing.Size(71, 14);
            this.labIsCommission.TabIndex = 6;
            this.labIsCommission.Text = "IsCommission";
            // 
            // numMaxRow
            // 
            this.numMaxRow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numMaxRow.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMaxRow.Location = new System.Drawing.Point(83, 117);
            this.numMaxRow.Name = "numMaxRow";
            this.numMaxRow.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMaxRow.Size = new System.Drawing.Size(118, 21);
            this.numMaxRow.TabIndex = 9;
            // 
            // labValid
            // 
            this.labValid.Location = new System.Drawing.Point(7, 93);
            this.labValid.Name = "labValid";
            this.labValid.Size = new System.Drawing.Size(25, 14);
            this.labValid.TabIndex = 6;
            this.labValid.Text = "Valid";
            // 
            // labMaxCount
            // 
            this.labMaxCount.Location = new System.Drawing.Point(7, 118);
            this.labMaxCount.Name = "labMaxCount";
            this.labMaxCount.Size = new System.Drawing.Size(58, 14);
            this.labMaxCount.TabIndex = 7;
            this.labMaxCount.Text = "Max Count";
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(7, 12);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(31, 14);
            this.labName.TabIndex = 1;
            this.labName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(83, 9);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(118, 21);
            this.txtName.TabIndex = 0;
            // 
            // SoluionChargeCodeSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.navBarControl1);
            this.Controls.Add(this.pnlButtom);
            this.Name = "SoluionChargeCodeSearchPart";
            this.Size = new System.Drawing.Size(233, 535);
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtom)).EndInit();
            this.pnlButtom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxRow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlButtom;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton chkIsAgent;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton ckbValid;
        private DevExpress.XtraEditors.LabelControl labIsAgent;
        private DevExpress.XtraEditors.SpinEdit numMaxRow;
        private DevExpress.XtraEditors.LabelControl labValid;
        private DevExpress.XtraEditors.LabelControl labMaxCount;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton chkIsCommission;
        private DevExpress.XtraEditors.LabelControl labIsCommission;
    }
}
