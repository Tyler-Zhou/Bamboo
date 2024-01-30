namespace ICP.FAM.UI
{
    partial class AgentBillCheckingSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentBillCheckingSearch));
            this.nbMain = new DevExpress.XtraNavBar.NavBarControl();
            this.bgBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.bgcBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.lblFinish = new DevExpress.XtraEditors.LabelControl();
            this.lblCheck = new DevExpress.XtraEditors.LabelControl();
            this.lblNotice = new DevExpress.XtraEditors.LabelControl();
            this.cmbCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbCreate = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.numMaxCount = new DevExpress.XtraEditors.SpinEdit();
            this.labMaxCount = new DevExpress.XtraEditors.LabelControl();
            this.pnlNotifiedBillOwner = new System.Windows.Forms.Panel();
            this.pnlCompleted = new System.Windows.Forms.Panel();
            this.pnlChecking = new System.Windows.Forms.Panel();
            this.mcDates = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.ckbCompleted = new DevExpress.XtraEditors.CheckEdit();
            this.cmbBillCheckType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbCheckCompanyID = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.lblBillCheckType = new DevExpress.XtraEditors.LabelControl();
            this.labCreateDate = new DevExpress.XtraEditors.LabelControl();
            this.labCreateName = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.pnlButtom = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnClare = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.nbMain)).BeginInit();
            this.nbMain.SuspendLayout();
            this.bgcBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbCompleted.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBillCheckType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckCompanyID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtom)).BeginInit();
            this.pnlButtom.SuspendLayout();
            this.SuspendLayout();
            // 
            // nbMain
            // 
            this.nbMain.ActiveGroup = this.bgBase;
            this.nbMain.Controls.Add(this.bgcBase);
            this.nbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nbMain.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.bgBase});
            this.nbMain.Location = new System.Drawing.Point(0, 0);
            this.nbMain.Name = "nbMain";
            this.nbMain.OptionsNavPane.ExpandedWidth = 221;
            this.nbMain.Size = new System.Drawing.Size(217, 508);
            this.nbMain.TabIndex = 1;
            this.nbMain.Text = "navBarControl1";
            // 
            // bgBase
            // 
            this.bgBase.Caption = "基础";
            this.bgBase.ControlContainer = this.bgcBase;
            this.bgBase.Expanded = true;
            this.bgBase.GroupClientHeight = 364;
            this.bgBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgBase.Name = "bgBase";
            // 
            // bgcBase
            // 
            this.bgcBase.Controls.Add(this.lblFinish);
            this.bgcBase.Controls.Add(this.lblCheck);
            this.bgcBase.Controls.Add(this.lblNotice);
            this.bgcBase.Controls.Add(this.cmbCompany);
            this.bgcBase.Controls.Add(this.cmbCreate);
            this.bgcBase.Controls.Add(this.numMaxCount);
            this.bgcBase.Controls.Add(this.labMaxCount);
            this.bgcBase.Controls.Add(this.pnlNotifiedBillOwner);
            this.bgcBase.Controls.Add(this.pnlCompleted);
            this.bgcBase.Controls.Add(this.pnlChecking);
            this.bgcBase.Controls.Add(this.mcDates);
            this.bgcBase.Controls.Add(this.ckbCompleted);
            this.bgcBase.Controls.Add(this.cmbBillCheckType);
            this.bgcBase.Controls.Add(this.cmbCheckCompanyID);
            this.bgcBase.Controls.Add(this.lblBillCheckType);
            this.bgcBase.Controls.Add(this.labCreateDate);
            this.bgcBase.Controls.Add(this.labCreateName);
            this.bgcBase.Controls.Add(this.labelControl1);
            this.bgcBase.Controls.Add(this.txtNo);
            this.bgcBase.Controls.Add(this.labCompany);
            this.bgcBase.Controls.Add(this.labNo);
            this.bgcBase.Name = "bgcBase";
            this.bgcBase.Size = new System.Drawing.Size(213, 362);
            this.bgcBase.TabIndex = 0;
            // 
            // lblFinish
            // 
            this.lblFinish.Appearance.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblFinish.Appearance.Options.UseForeColor = true;
            this.lblFinish.Location = new System.Drawing.Point(172, 335);
            this.lblFinish.Name = "lblFinish";
            this.lblFinish.Size = new System.Drawing.Size(24, 14);
            this.lblFinish.TabIndex = 22;
            this.lblFinish.Text = "完成";
            // 
            // lblCheck
            // 
            this.lblCheck.Appearance.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCheck.Appearance.Options.UseForeColor = true;
            this.lblCheck.Location = new System.Drawing.Point(27, 335);
            this.lblCheck.Name = "lblCheck";
            this.lblCheck.Size = new System.Drawing.Size(24, 14);
            this.lblCheck.TabIndex = 21;
            this.lblCheck.Text = "核对";
            // 
            // lblNotice
            // 
            this.lblNotice.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblNotice.Appearance.Options.UseForeColor = true;
            this.lblNotice.Location = new System.Drawing.Point(85, 335);
            this.lblNotice.Name = "lblNotice";
            this.lblNotice.Size = new System.Drawing.Size(48, 14);
            this.lblNotice.TabIndex = 20;
            this.lblNotice.Text = "通知修改";
            // 
            // cmbCompany
            // 
            this.cmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCompany.Location = new System.Drawing.Point(63, 33);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Size = new System.Drawing.Size(136, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCompany.TabIndex = 1;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // cmbCreate
            // 
            this.cmbCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCreate.EditText = "";
            this.cmbCreate.EditValue = null;
            this.cmbCreate.Location = new System.Drawing.Point(63, 142);
            this.cmbCreate.Name = "cmbCreate";
            this.cmbCreate.ReadOnly = false;
            this.cmbCreate.Size = new System.Drawing.Size(137, 21);
            this.cmbCreate.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCreate.TabIndex = 5;
            this.cmbCreate.ToolTip = "";
            // 
            // numMaxCount
            // 
            this.numMaxCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numMaxCount.EditValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numMaxCount.Location = new System.Drawing.Point(63, 87);
            this.numMaxCount.Name = "numMaxCount";
            this.numMaxCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMaxCount.Properties.IsFloatValue = false;
            this.numMaxCount.Properties.Mask.EditMask = "N00";
            this.numMaxCount.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numMaxCount.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaxCount.Size = new System.Drawing.Size(136, 21);
            this.numMaxCount.TabIndex = 3;
            // 
            // labMaxCount
            // 
            this.labMaxCount.Location = new System.Drawing.Point(5, 90);
            this.labMaxCount.Name = "labMaxCount";
            this.labMaxCount.Size = new System.Drawing.Size(48, 14);
            this.labMaxCount.TabIndex = 19;
            this.labMaxCount.Text = "每页显示";
            // 
            // pnlNotifiedBillOwner
            // 
            this.pnlNotifiedBillOwner.BackColor = System.Drawing.Color.Red;
            this.pnlNotifiedBillOwner.Location = new System.Drawing.Point(67, 334);
            this.pnlNotifiedBillOwner.Name = "pnlNotifiedBillOwner";
            this.pnlNotifiedBillOwner.Size = new System.Drawing.Size(15, 15);
            this.pnlNotifiedBillOwner.TabIndex = 6;
            // 
            // pnlCompleted
            // 
            this.pnlCompleted.BackColor = System.Drawing.Color.DarkGreen;
            this.pnlCompleted.Location = new System.Drawing.Point(152, 334);
            this.pnlCompleted.Name = "pnlCompleted";
            this.pnlCompleted.Size = new System.Drawing.Size(15, 15);
            this.pnlCompleted.TabIndex = 6;
            // 
            // pnlChecking
            // 
            this.pnlChecking.BackColor = System.Drawing.Color.MediumBlue;
            this.pnlChecking.Location = new System.Drawing.Point(9, 334);
            this.pnlChecking.Name = "pnlChecking";
            this.pnlChecking.Size = new System.Drawing.Size(15, 15);
            this.pnlChecking.TabIndex = 6;
            // 
            // mcDates
            // 
            this.mcDates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mcDates.From = null;
            this.mcDates.IsEngish = true;
            this.mcDates.Location = new System.Drawing.Point(63, 170);
            this.mcDates.Name = "mcDates";
            this.mcDates.Size = new System.Drawing.Size(136, 152);
            this.mcDates.TabIndex = 6;
            this.mcDates.To = null;
            // 
            // ckbCompleted
            // 
            this.ckbCompleted.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbCompleted.Location = new System.Drawing.Point(63, 116);
            this.ckbCompleted.Name = "ckbCompleted";
            this.ckbCompleted.Properties.Caption = "已完成对账";
            this.ckbCompleted.Size = new System.Drawing.Size(136, 19);
            this.ckbCompleted.TabIndex = 4;
            // 
            // cmbBillCheckType
            // 
            this.cmbBillCheckType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBillCheckType.Location = new System.Drawing.Point(63, 60);
            this.cmbBillCheckType.Name = "cmbBillCheckType";
            this.cmbBillCheckType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbBillCheckType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbBillCheckType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBillCheckType.Size = new System.Drawing.Size(136, 21);
            this.cmbBillCheckType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbBillCheckType.TabIndex = 2;
            // 
            // cmbCheckCompanyID
            // 
            this.cmbCheckCompanyID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCheckCompanyID.Location = new System.Drawing.Point(63, 405);
            this.cmbCheckCompanyID.Name = "cmbCheckCompanyID";
            this.cmbCheckCompanyID.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCheckCompanyID.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCheckCompanyID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCheckCompanyID.Size = new System.Drawing.Size(136, 21);
            this.cmbCheckCompanyID.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCheckCompanyID.TabIndex = 2;
            // 
            // lblBillCheckType
            // 
            this.lblBillCheckType.Location = new System.Drawing.Point(5, 63);
            this.lblBillCheckType.Name = "lblBillCheckType";
            this.lblBillCheckType.Size = new System.Drawing.Size(48, 14);
            this.lblBillCheckType.TabIndex = 2;
            this.lblBillCheckType.Text = "对账类型";
            // 
            // labCreateDate
            // 
            this.labCreateDate.Location = new System.Drawing.Point(5, 176);
            this.labCreateDate.Name = "labCreateDate";
            this.labCreateDate.Size = new System.Drawing.Size(48, 14);
            this.labCreateDate.TabIndex = 2;
            this.labCreateDate.Text = "创建时间";
            // 
            // labCreateName
            // 
            this.labCreateName.Location = new System.Drawing.Point(5, 145);
            this.labCreateName.Name = "labCreateName";
            this.labCreateName.Size = new System.Drawing.Size(36, 14);
            this.labCreateName.TabIndex = 2;
            this.labCreateName.Text = "创建人";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 408);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "核对公司";
            // 
            // txtNo
            // 
            this.txtNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNo.Location = new System.Drawing.Point(63, 6);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(136, 21);
            this.txtNo.TabIndex = 0;
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(6, 36);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(24, 14);
            this.labCompany.TabIndex = 2;
            this.labCompany.Text = "公司";
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(6, 9);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(48, 14);
            this.labNo.TabIndex = 2;
            this.labNo.Text = "对账单号";
            // 
            // pnlButtom
            // 
            this.pnlButtom.Controls.Add(this.btnSearch);
            this.pnlButtom.Controls.Add(this.btnClare);
            this.pnlButtom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtom.Location = new System.Drawing.Point(0, 508);
            this.pnlButtom.Name = "pnlButtom";
            this.pnlButtom.Size = new System.Drawing.Size(217, 43);
            this.pnlButtom.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(123, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClare
            // 
            this.btnClare.Location = new System.Drawing.Point(22, 10);
            this.btnClare.Name = "btnClare";
            this.btnClare.Size = new System.Drawing.Size(75, 23);
            this.btnClare.TabIndex = 0;
            this.btnClare.Text = "清空(&L)";
            this.btnClare.Click += new System.EventHandler(this.btnClare_Click);
            // 
            // AgentBillCheckingSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.nbMain);
            this.Controls.Add(this.pnlButtom);
            this.Name = "AgentBillCheckingSearch";
            this.Size = new System.Drawing.Size(217, 551);
            ((System.ComponentModel.ISupportInitialize)(this.nbMain)).EndInit();
            this.nbMain.ResumeLayout(false);
            this.bgcBase.ResumeLayout(false);
            this.bgcBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbCompleted.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBillCheckType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckCompanyID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtom)).EndInit();
            this.pnlButtom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl nbMain;
        private DevExpress.XtraNavBar.NavBarGroup bgBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer bgcBase;
        private DevExpress.XtraEditors.PanelControl pnlButtom;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnClare;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.LabelControl labNo;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbBillCheckType;
        private DevExpress.XtraEditors.LabelControl lblBillCheckType;
        private DevExpress.XtraEditors.CheckEdit ckbCompleted;
        private DevExpress.XtraEditors.LabelControl labCreateDate;
        private DevExpress.XtraEditors.LabelControl labCreateName;
        private ICP.Framework.ClientComponents.Controls.DateMonthControl mcDates;
        private System.Windows.Forms.Panel pnlNotifiedBillOwner;
        private System.Windows.Forms.Panel pnlChecking;
        private System.Windows.Forms.Panel pnlCompleted;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCheckCompanyID;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        protected DevExpress.XtraEditors.SpinEdit numMaxCount;
        protected DevExpress.XtraEditors.LabelControl labMaxCount;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox cmbCreate;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.LabelControl lblNotice;
        private DevExpress.XtraEditors.LabelControl lblCheck;
        private DevExpress.XtraEditors.LabelControl lblFinish;
    }
}
