namespace ICP.FAM.UI
{
    partial class LedgerListSearchPart
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
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.lblCopany = new DevExpress.XtraEditors.LabelControl();
            this.txtRemark = new DevExpress.XtraEditors.TextEdit();
            this.ckbValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.lblIsV = new DevExpress.XtraEditors.LabelControl();
            this.numAmountMax = new DevExpress.XtraEditors.SpinEdit();
            this.mscCreator = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labMax = new DevExpress.XtraEditors.LabelControl();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.numAmountMin = new DevExpress.XtraEditors.SpinEdit();
            this.mscAuditor = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labMin = new DevExpress.XtraEditors.LabelControl();
            this.lblCashier = new DevExpress.XtraEditors.LabelControl();
            this.rgpAmountType = new DevExpress.XtraEditors.RadioGroup();
            this.mscCashier = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.chcCompany = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lblAuditor = new DevExpress.XtraEditors.LabelControl();
            this.lblType = new DevExpress.XtraEditors.LabelControl();
            this.txtRefNo = new DevExpress.XtraEditors.TextEdit();
            this.lblCreateUser = new DevExpress.XtraEditors.LabelControl();
            this.cmbStatus = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labRefNo = new DevExpress.XtraEditors.LabelControl();
            this.chcType = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lblLine = new System.Windows.Forms.Label();
            this.lblLedgerNo = new DevExpress.XtraEditors.LabelControl();
            this.txtNoFrom = new DevExpress.XtraEditors.TextEdit();
            this.txtNoTo = new DevExpress.XtraEditors.TextEdit();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel2 = new DevExpress.XtraEditors.PanelControl();
            this.dmdDate = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.navDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnClare = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.pnlScroll = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.navBarSearch)).BeginInit();
            this.navBarSearch.SuspendLayout();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgpAmountType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chcCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRefNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chcType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoTo.Properties)).BeginInit();
            this.navBarGroupControlContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlScroll)).BeginInit();
            this.pnlScroll.SuspendLayout();
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
            this.navBarSearch.Size = new System.Drawing.Size(228, 558);
            this.navBarSearch.SkinExplorerBarViewScrollStyle = DevExpress.XtraNavBar.SkinExplorerBarViewScrollStyle.ScrollBar;
            this.navBarSearch.TabIndex = 2;
            this.navBarSearch.Text = "navBarControl1";
            this.navBarSearch.Click += new System.EventHandler(this.navBarSearch_Click);
            // 
            // navLegderInfo
            // 
            this.navLegderInfo.Caption = "凭证信息";
            this.navLegderInfo.ControlContainer = this.navBarGroupControlContainer2;
            this.navLegderInfo.Expanded = true;
            this.navLegderInfo.GroupClientHeight = 385;
            this.navLegderInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navLegderInfo.Name = "navLegderInfo";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.panel1);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(207, 383);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCopany);
            this.panel1.Controls.Add(this.txtRemark);
            this.panel1.Controls.Add(this.ckbValid);
            this.panel1.Controls.Add(this.labRemark);
            this.panel1.Controls.Add(this.lblIsV);
            this.panel1.Controls.Add(this.numAmountMax);
            this.panel1.Controls.Add(this.mscCreator);
            this.panel1.Controls.Add(this.labMax);
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.numAmountMin);
            this.panel1.Controls.Add(this.mscAuditor);
            this.panel1.Controls.Add(this.labMin);
            this.panel1.Controls.Add(this.lblCashier);
            this.panel1.Controls.Add(this.rgpAmountType);
            this.panel1.Controls.Add(this.mscCashier);
            this.panel1.Controls.Add(this.chcCompany);
            this.panel1.Controls.Add(this.lblAuditor);
            this.panel1.Controls.Add(this.lblType);
            this.panel1.Controls.Add(this.txtRefNo);
            this.panel1.Controls.Add(this.lblCreateUser);
            this.panel1.Controls.Add(this.cmbStatus);
            this.panel1.Controls.Add(this.labRefNo);
            this.panel1.Controls.Add(this.chcType);
            this.panel1.Controls.Add(this.lblLine);
            this.panel1.Controls.Add(this.lblLedgerNo);
            this.panel1.Controls.Add(this.txtNoFrom);
            this.panel1.Controls.Add(this.txtNoTo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 383);
            this.panel1.TabIndex = 58;
            // 
            // lblCopany
            // 
            this.lblCopany.Location = new System.Drawing.Point(12, 9);
            this.lblCopany.Name = "lblCopany";
            this.lblCopany.Size = new System.Drawing.Size(48, 14);
            this.lblCopany.TabIndex = 42;
            this.lblCopany.Text = "所属公司";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(64, 219);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(131, 21);
            this.txtRemark.TabIndex = 57;
            // 
            // ckbValid
            // 
            this.ckbValid.Checked = true;
            this.ckbValid.CheckedText = "TRUE";
            this.ckbValid.Location = new System.Drawing.Point(64, 349);
            this.ckbValid.Name = "ckbValid";
            this.ckbValid.NULLText = "ALL";
            this.ckbValid.Size = new System.Drawing.Size(131, 22);
            this.ckbValid.TabIndex = 47;
            this.ckbValid.UnCheckedText = "FALSE";
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(12, 222);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(24, 14);
            this.labRemark.TabIndex = 56;
            this.labRemark.Text = "摘要";
            // 
            // lblIsV
            // 
            this.lblIsV.Location = new System.Drawing.Point(12, 352);
            this.lblIsV.Name = "lblIsV";
            this.lblIsV.Size = new System.Drawing.Size(36, 14);
            this.lblIsV.TabIndex = 48;
            this.lblIsV.Text = "有效性";
            // 
            // numAmountMax
            // 
            this.numAmountMax.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numAmountMax.Location = new System.Drawing.Point(64, 192);
            this.numAmountMax.Name = "numAmountMax";
            this.numAmountMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numAmountMax.Size = new System.Drawing.Size(131, 21);
            this.numAmountMax.TabIndex = 54;
            // 
            // mscCreator
            // 
            this.mscCreator.EditText = "";
            this.mscCreator.EditValue = null;
            this.mscCreator.Location = new System.Drawing.Point(64, 271);
            this.mscCreator.Name = "mscCreator";
            this.mscCreator.ReadOnly = false;
            this.mscCreator.RefreshButtonToolTip = "";
            this.mscCreator.ShowRefreshButton = false;
            this.mscCreator.Size = new System.Drawing.Size(131, 21);
            this.mscCreator.SpecifiedBackColor = System.Drawing.Color.White;
            this.mscCreator.TabIndex = 38;
            this.mscCreator.ToolTip = "";
            // 
            // labMax
            // 
            this.labMax.Location = new System.Drawing.Point(12, 194);
            this.labMax.Name = "labMax";
            this.labMax.Size = new System.Drawing.Size(24, 14);
            this.labMax.TabIndex = 55;
            this.labMax.Text = "最大";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(12, 248);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(24, 14);
            this.lblStatus.TabIndex = 46;
            this.lblStatus.Text = "状态";
            // 
            // numAmountMin
            // 
            this.numAmountMin.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numAmountMin.Location = new System.Drawing.Point(64, 165);
            this.numAmountMin.Name = "numAmountMin";
            this.numAmountMin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numAmountMin.Size = new System.Drawing.Size(131, 21);
            this.numAmountMin.TabIndex = 53;
            // 
            // mscAuditor
            // 
            this.mscAuditor.EditText = "";
            this.mscAuditor.EditValue = null;
            this.mscAuditor.Location = new System.Drawing.Point(64, 297);
            this.mscAuditor.Name = "mscAuditor";
            this.mscAuditor.ReadOnly = false;
            this.mscAuditor.RefreshButtonToolTip = "";
            this.mscAuditor.ShowRefreshButton = false;
            this.mscAuditor.Size = new System.Drawing.Size(131, 21);
            this.mscAuditor.SpecifiedBackColor = System.Drawing.Color.White;
            this.mscAuditor.TabIndex = 39;
            this.mscAuditor.ToolTip = "";
            // 
            // labMin
            // 
            this.labMin.Location = new System.Drawing.Point(12, 167);
            this.labMin.Name = "labMin";
            this.labMin.Size = new System.Drawing.Size(24, 14);
            this.labMin.TabIndex = 52;
            this.labMin.Text = "最小";
            // 
            // lblCashier
            // 
            this.lblCashier.Location = new System.Drawing.Point(12, 326);
            this.lblCashier.Name = "lblCashier";
            this.lblCashier.Size = new System.Drawing.Size(36, 14);
            this.lblCashier.TabIndex = 43;
            this.lblCashier.Text = "出纳员";
            // 
            // rgpAmountType
            // 
            this.rgpAmountType.Location = new System.Drawing.Point(12, 133);
            this.rgpAmountType.Name = "rgpAmountType";
            this.rgpAmountType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "金额"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "外币金额")});
            this.rgpAmountType.Size = new System.Drawing.Size(183, 27);
            this.rgpAmountType.TabIndex = 51;
            // 
            // mscCashier
            // 
            this.mscCashier.EditText = "";
            this.mscCashier.EditValue = null;
            this.mscCashier.Location = new System.Drawing.Point(64, 323);
            this.mscCashier.Name = "mscCashier";
            this.mscCashier.ReadOnly = false;
            this.mscCashier.RefreshButtonToolTip = "";
            this.mscCashier.ShowRefreshButton = false;
            this.mscCashier.Size = new System.Drawing.Size(131, 21);
            this.mscCashier.SpecifiedBackColor = System.Drawing.Color.White;
            this.mscCashier.TabIndex = 41;
            this.mscCashier.ToolTip = "";
            // 
            // chcCompany
            // 
            this.chcCompany.EditValue = "";
            this.chcCompany.EnterMoveNextControl = true;
            this.chcCompany.Location = new System.Drawing.Point(64, 6);
            this.chcCompany.Name = "chcCompany";
            this.chcCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chcCompany.Size = new System.Drawing.Size(131, 21);
            this.chcCompany.TabIndex = 31;
            // 
            // lblAuditor
            // 
            this.lblAuditor.Location = new System.Drawing.Point(12, 300);
            this.lblAuditor.Name = "lblAuditor";
            this.lblAuditor.Size = new System.Drawing.Size(36, 14);
            this.lblAuditor.TabIndex = 40;
            this.lblAuditor.Text = "审核人";
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(12, 111);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(24, 14);
            this.lblType.TabIndex = 49;
            this.lblType.Text = "类型";
            // 
            // txtRefNo
            // 
            this.txtRefNo.Location = new System.Drawing.Point(64, 82);
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.Size = new System.Drawing.Size(130, 21);
            this.txtRefNo.TabIndex = 35;
            // 
            // lblCreateUser
            // 
            this.lblCreateUser.Location = new System.Drawing.Point(12, 274);
            this.lblCreateUser.Name = "lblCreateUser";
            this.lblCreateUser.Size = new System.Drawing.Size(36, 14);
            this.lblCreateUser.TabIndex = 36;
            this.lblCreateUser.Text = "制单人";
            // 
            // cmbStatus
            // 
            this.cmbStatus.Location = new System.Drawing.Point(64, 245);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbStatus.Size = new System.Drawing.Size(131, 21);
            this.cmbStatus.TabIndex = 45;
            // 
            // labRefNo
            // 
            this.labRefNo.Location = new System.Drawing.Point(12, 84);
            this.labRefNo.Name = "labRefNo";
            this.labRefNo.Size = new System.Drawing.Size(36, 14);
            this.labRefNo.TabIndex = 37;
            this.labRefNo.Text = "参考号";
            // 
            // chcType
            // 
            this.chcType.EditValue = "";
            this.chcType.EnterMoveNextControl = true;
            this.chcType.Location = new System.Drawing.Point(64, 108);
            this.chcType.Name = "chcType";
            this.chcType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chcType.Size = new System.Drawing.Size(131, 21);
            this.chcType.TabIndex = 44;
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.BackColor = System.Drawing.Color.Transparent;
            this.lblLine.Location = new System.Drawing.Point(40, 61);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(18, 14);
            this.lblLine.TabIndex = 50;
            this.lblLine.Text = "—";
            // 
            // lblLedgerNo
            // 
            this.lblLedgerNo.Location = new System.Drawing.Point(12, 34);
            this.lblLedgerNo.Name = "lblLedgerNo";
            this.lblLedgerNo.Size = new System.Drawing.Size(36, 14);
            this.lblLedgerNo.TabIndex = 32;
            this.lblLedgerNo.Text = "凭证号";
            // 
            // txtNoFrom
            // 
            this.txtNoFrom.Location = new System.Drawing.Point(64, 31);
            this.txtNoFrom.Name = "txtNoFrom";
            this.txtNoFrom.Properties.Mask.EditMask = "f0";
            this.txtNoFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtNoFrom.Properties.MaxLength = 4;
            this.txtNoFrom.Size = new System.Drawing.Size(130, 21);
            this.txtNoFrom.TabIndex = 33;
            // 
            // txtNoTo
            // 
            this.txtNoTo.Location = new System.Drawing.Point(64, 58);
            this.txtNoTo.Name = "txtNoTo";
            this.txtNoTo.Properties.Mask.EditMask = "f0";
            this.txtNoTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtNoTo.Properties.MaxLength = 4;
            this.txtNoTo.Size = new System.Drawing.Size(130, 21);
            this.txtNoTo.TabIndex = 34;
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.panel2);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(207, 155);
            this.navBarGroupControlContainer3.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dmdDate);
            this.panel2.Controls.Add(this.labTo);
            this.panel2.Controls.Add(this.labFrom);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(207, 155);
            this.panel2.TabIndex = 45;
            // 
            // dmdDate
            // 
            this.dmdDate.From = null;
            this.dmdDate.IsEngish = false;
            this.dmdDate.Location = new System.Drawing.Point(63, 3);
            this.dmdDate.Name = "dmdDate";
            this.dmdDate.Size = new System.Drawing.Size(130, 142);
            this.dmdDate.TabIndex = 44;
            this.dmdDate.To = null;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(12, 121);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 43;
            this.labTo.Text = "To";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(12, 96);
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
            this.navDate.GroupClientHeight = 157;
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
            this.panelControl2.Size = new System.Drawing.Size(232, 38);
            this.panelControl2.TabIndex = 5;
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
            // pnlScroll
            // 
            this.pnlScroll.Controls.Add(this.navBarSearch);
            this.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScroll.Location = new System.Drawing.Point(0, 0);
            this.pnlScroll.Name = "pnlScroll";
            this.pnlScroll.Size = new System.Drawing.Size(232, 562);
            this.pnlScroll.TabIndex = 6;
            // 
            // LedgerListSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlScroll);
            this.Controls.Add(this.panelControl2);
            this.Name = "LedgerListSearchPart";
            this.Size = new System.Drawing.Size(232, 600);
            ((System.ComponentModel.ISupportInitialize)(this.navBarSearch)).EndInit();
            this.navBarSearch.ResumeLayout(false);
            this.navBarGroupControlContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgpAmountType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chcCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRefNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chcType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoTo.Properties)).EndInit();
            this.navBarGroupControlContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlScroll)).EndInit();
            this.pnlScroll.ResumeLayout(false);
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
        private DevExpress.XtraEditors.PanelControl pnlScroll;
        private DevExpress.XtraEditors.TextEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private DevExpress.XtraEditors.SpinEdit numAmountMax;
        private DevExpress.XtraEditors.LabelControl labMax;
        private DevExpress.XtraEditors.SpinEdit numAmountMin;
        private DevExpress.XtraEditors.LabelControl labMin;
        private DevExpress.XtraEditors.RadioGroup rgpAmountType;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chcCompany;
        private DevExpress.XtraEditors.LabelControl lblCopany;
        private DevExpress.XtraEditors.TextEdit txtRefNo;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbStatus;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chcType;
        private DevExpress.XtraEditors.LabelControl lblLedgerNo;
        private DevExpress.XtraEditors.TextEdit txtNoTo;
        private DevExpress.XtraEditors.TextEdit txtNoFrom;
        private System.Windows.Forms.Label lblLine;
        private DevExpress.XtraEditors.LabelControl labRefNo;
        private DevExpress.XtraEditors.LabelControl lblCreateUser;
        private DevExpress.XtraEditors.LabelControl lblType;
        private DevExpress.XtraEditors.LabelControl lblAuditor;
        private Framework.ClientComponents.Controls.MultiSearchCommonBox mscCashier;
        private DevExpress.XtraEditors.LabelControl lblCashier;
        private Framework.ClientComponents.Controls.MultiSearchCommonBox mscAuditor;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private Framework.ClientComponents.Controls.MultiSearchCommonBox mscCreator;
        private DevExpress.XtraEditors.LabelControl lblIsV;
        private Framework.ClientComponents.Controls.LWCheckButton ckbValid;
        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.PanelControl panel2;
    }
}
