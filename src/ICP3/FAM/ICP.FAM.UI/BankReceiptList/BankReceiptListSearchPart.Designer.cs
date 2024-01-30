namespace ICP.FAM.UI.BankReceiptList
{
    partial class BankReceiptListSearchPart
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
            this.navBarSearch = new DevExpress.XtraNavBar.NavBarControl();
            this.navLegderInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cmbStatue = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lblCopany = new DevExpress.XtraEditors.LabelControl();
            this.chcCompany = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.lblIsV = new DevExpress.XtraEditors.LabelControl();
            this.ckbValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.dmdDate = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.navDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnClare = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.txtReceiptNO = new DevExpress.XtraEditors.TextEdit();
            this.labReceiptNO = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.navBarSearch)).BeginInit();
            this.navBarSearch.SuspendLayout();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chcCompany.Properties)).BeginInit();
            this.navBarGroupControlContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiptNO.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // navBarSearch
            // 
            this.navBarSearch.ActiveGroup = this.navLegderInfo;
            this.navBarSearch.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarSearch.Controls.Add(this.navBarGroupControlContainer3);
            this.navBarSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarSearch.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navLegderInfo,
            this.navDate});
            this.navBarSearch.Location = new System.Drawing.Point(2, 2);
            this.navBarSearch.Name = "navBarSearch";
            this.navBarSearch.OptionsNavPane.ExpandedWidth = 222;
            this.navBarSearch.Size = new System.Drawing.Size(218, 558);
            this.navBarSearch.TabIndex = 0;
            this.navBarSearch.Text = "navBarControl1";
            this.navBarSearch.Click += new System.EventHandler(this.navBarSearch_Click);
            // 
            // navLegderInfo
            // 
            this.navLegderInfo.Caption = "水单信息";
            this.navLegderInfo.ControlContainer = this.navBarGroupControlContainer2;
            this.navLegderInfo.Expanded = true;
            this.navLegderInfo.GroupClientHeight = 146;
            this.navLegderInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navLegderInfo.Name = "navLegderInfo";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.txtReceiptNO);
            this.navBarGroupControlContainer2.Controls.Add(this.labReceiptNO);
            this.navBarGroupControlContainer2.Controls.Add(this.cmbStatue);
            this.navBarGroupControlContainer2.Controls.Add(this.lblCopany);
            this.navBarGroupControlContainer2.Controls.Add(this.chcCompany);
            this.navBarGroupControlContainer2.Controls.Add(this.lblStatus);
            this.navBarGroupControlContainer2.Controls.Add(this.lblIsV);
            this.navBarGroupControlContainer2.Controls.Add(this.ckbValid);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(214, 144);
            this.navBarGroupControlContainer2.TabIndex = 0;
            // 
            // cmbStatue
            // 
            this.cmbStatue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStatue.Location = new System.Drawing.Point(57, 60);
            this.cmbStatue.Name = "cmbStatue";
            this.cmbStatue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbStatue.Size = new System.Drawing.Size(150, 21);
            this.cmbStatue.TabIndex = 2;
            // 
            // lblCopany
            // 
            this.lblCopany.Location = new System.Drawing.Point(5, 38);
            this.lblCopany.Name = "lblCopany";
            this.lblCopany.Size = new System.Drawing.Size(48, 14);
            this.lblCopany.TabIndex = 42;
            this.lblCopany.Text = "所属公司";
            // 
            // chcCompany
            // 
            this.chcCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chcCompany.EditValue = "";
            this.chcCompany.EnterMoveNextControl = true;
            this.chcCompany.Location = new System.Drawing.Point(57, 35);
            this.chcCompany.Name = "chcCompany";
            this.chcCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chcCompany.Size = new System.Drawing.Size(150, 21);
            this.chcCompany.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(5, 63);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(24, 14);
            this.lblStatus.TabIndex = 46;
            this.lblStatus.Text = "状态";
            // 
            // lblIsV
            // 
            this.lblIsV.Location = new System.Drawing.Point(5, 88);
            this.lblIsV.Name = "lblIsV";
            this.lblIsV.Size = new System.Drawing.Size(36, 14);
            this.lblIsV.TabIndex = 48;
            this.lblIsV.Text = "有效性";
            // 
            // ckbValid
            // 
            this.ckbValid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbValid.Checked = true;
            this.ckbValid.CheckedText = "TRUE";
            this.ckbValid.Location = new System.Drawing.Point(57, 84);
            this.ckbValid.Name = "ckbValid";
            this.ckbValid.NULLText = "ALL";
            this.ckbValid.ReadOnly = false;
            this.ckbValid.Size = new System.Drawing.Size(150, 21);
            this.ckbValid.TabIndex = 3;
            this.ckbValid.UnCheckedText = "FALSE";
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.labTo);
            this.navBarGroupControlContainer3.Controls.Add(this.dmdDate);
            this.navBarGroupControlContainer3.Controls.Add(this.labFrom);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(214, 149);
            this.navBarGroupControlContainer3.TabIndex = 1;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(6, 120);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 43;
            this.labTo.Text = "To";
            // 
            // dmdDate
            // 
            this.dmdDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dmdDate.From = null;
            this.dmdDate.IsEngish = false;
            this.dmdDate.Location = new System.Drawing.Point(57, 2);
            this.dmdDate.Name = "dmdDate";
            this.dmdDate.Size = new System.Drawing.Size(150, 142);
            this.dmdDate.TabIndex = 0;
            this.dmdDate.To = null;
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(6, 95);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(27, 14);
            this.labFrom.TabIndex = 42;
            this.labFrom.Text = "From";
            // 
            // navDate
            // 
            this.navDate.Caption = "日期查询";
            this.navDate.ControlContainer = this.navBarGroupControlContainer3;
            this.navDate.Expanded = true;
            this.navDate.GroupClientHeight = 151;
            this.navDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navDate.Name = "navDate";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnClare);
            this.panelControl2.Controls.Add(this.btnSearch);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 562);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(222, 38);
            this.panelControl2.TabIndex = 0;
            // 
            // btnClare
            // 
            this.btnClare.Location = new System.Drawing.Point(22, 9);
            this.btnClare.Name = "btnClare";
            this.btnClare.Size = new System.Drawing.Size(75, 23);
            this.btnClare.TabIndex = 1;
            this.btnClare.Text = "清空(&L)";
            this.btnClare.Click += new System.EventHandler(this.btnClare_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(113, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.navBarSearch);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(222, 562);
            this.pnlMain.TabIndex = 6;
            // 
            // txtReceiptNO
            // 
            this.txtReceiptNO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceiptNO.Location = new System.Drawing.Point(57, 8);
            this.txtReceiptNO.Name = "txtReceiptNO";
            this.txtReceiptNO.Size = new System.Drawing.Size(150, 21);
            this.txtReceiptNO.TabIndex = 0;
            // 
            // labReceiptNO
            // 
            this.labReceiptNO.Location = new System.Drawing.Point(5, 11);
            this.labReceiptNO.Name = "labReceiptNO";
            this.labReceiptNO.Size = new System.Drawing.Size(48, 14);
            this.labReceiptNO.TabIndex = 51;
            this.labReceiptNO.Text = "水单单号";
            // 
            // BankReceiptListSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panelControl2);
            this.Name = "BankReceiptListSearchPart";
            this.Size = new System.Drawing.Size(222, 600);
            ((System.ComponentModel.ISupportInitialize)(this.navBarSearch)).EndInit();
            this.navBarSearch.ResumeLayout(false);
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chcCompany.Properties)).EndInit();
            this.navBarGroupControlContainer3.ResumeLayout(false);
            this.navBarGroupControlContainer3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiptNO.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarSearch;
        private DevExpress.XtraNavBar.NavBarGroup navLegderInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        private Framework.ClientComponents.Controls.DateMonthControl dmdDate;
        protected DevExpress.XtraEditors.LabelControl labTo;
        protected DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraNavBar.NavBarGroup navDate;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnClare;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl pnlMain;

        private DevExpress.XtraEditors.LabelControl lblCopany;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chcCompany;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.LabelControl lblIsV;
        private Framework.ClientComponents.Controls.LWCheckButton ckbValid;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbStatue;
        private DevExpress.XtraEditors.TextEdit txtReceiptNO;
        private DevExpress.XtraEditors.LabelControl labReceiptNO;
    }
}
