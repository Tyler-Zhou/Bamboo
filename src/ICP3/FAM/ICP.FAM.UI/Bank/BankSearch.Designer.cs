namespace ICP.FAM.UI
{
    partial class BankSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankSearch));
            this.pnlButtom = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnClare = new DevExpress.XtraEditors.SimpleButton();
            this.bgBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.bgcBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.chkcmbCompany = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.ckbValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.numMaxRow = new DevExpress.XtraEditors.SpinEdit();
            this.txtBankEName = new DevExpress.XtraEditors.TextEdit();
            this.txtBankCName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtShortName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.nbMain = new DevExpress.XtraNavBar.NavBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtom)).BeginInit();
            this.pnlButtom.SuspendLayout();
            this.bgcBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxRow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBankEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBankCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShortName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbMain)).BeginInit();
            this.nbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtom
            // 
            this.pnlButtom.Controls.Add(this.btnSearch);
            this.pnlButtom.Controls.Add(this.btnClare);
            this.pnlButtom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtom.Location = new System.Drawing.Point(0, 570);
            this.pnlButtom.Name = "pnlButtom";
            this.pnlButtom.Size = new System.Drawing.Size(222, 52);
            this.pnlButtom.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(124, 16);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClare
            // 
            this.btnClare.Location = new System.Drawing.Point(23, 16);
            this.btnClare.Name = "btnClare";
            this.btnClare.Size = new System.Drawing.Size(75, 23);
            this.btnClare.TabIndex = 0;
            this.btnClare.Text = "清空(&L)";
            this.btnClare.Click += new System.EventHandler(this.btnClare_Click);
            // 
            // bgBase
            // 
            this.bgBase.Caption = "基础";
            this.bgBase.ControlContainer = this.bgcBase;
            this.bgBase.Expanded = true;
            this.bgBase.GroupClientHeight = 175;
            this.bgBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgBase.Name = "bgBase";
            // 
            // bgcBase
            // 
            this.bgcBase.Controls.Add(this.chkcmbCompany);
            this.bgcBase.Controls.Add(this.ckbValid);
            this.bgcBase.Controls.Add(this.numMaxRow);
            this.bgcBase.Controls.Add(this.txtBankEName);
            this.bgcBase.Controls.Add(this.txtBankCName);
            this.bgcBase.Controls.Add(this.labelControl6);
            this.bgcBase.Controls.Add(this.labelControl5);
            this.bgcBase.Controls.Add(this.labelControl4);
            this.bgcBase.Controls.Add(this.txtShortName);
            this.bgcBase.Controls.Add(this.labelControl3);
            this.bgcBase.Controls.Add(this.labelControl2);
            this.bgcBase.Controls.Add(this.labelControl1);
            this.bgcBase.Name = "bgcBase";
            this.bgcBase.Size = new System.Drawing.Size(218, 173);
            this.bgcBase.TabIndex = 0;
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.Location = new System.Drawing.Point(68, 4);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkcmbCompany.Size = new System.Drawing.Size(137, 21);
            this.chkcmbCompany.TabIndex = 0;
            // 
            // ckbValid
            // 
            this.ckbValid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbValid.Checked = true;
            this.ckbValid.CheckedText = "TRUE";
            this.ckbValid.Location = new System.Drawing.Point(68, 111);
            this.ckbValid.Name = "ckbValid";
            this.ckbValid.NULLText = "ALL";
            this.ckbValid.Size = new System.Drawing.Size(136, 22);
            this.ckbValid.TabIndex = 4;
            this.ckbValid.UnCheckedText = "FALSE";
            // 
            // numMaxRow
            // 
            this.numMaxRow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numMaxRow.EditValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numMaxRow.Location = new System.Drawing.Point(68, 138);
            this.numMaxRow.Name = "numMaxRow";
            this.numMaxRow.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMaxRow.Properties.MaxValue = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numMaxRow.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaxRow.Size = new System.Drawing.Size(136, 21);
            this.numMaxRow.TabIndex = 5;
            // 
            // txtBankEName
            // 
            this.txtBankEName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBankEName.Location = new System.Drawing.Point(68, 85);
            this.txtBankEName.Name = "txtBankEName";
            this.txtBankEName.Size = new System.Drawing.Size(136, 21);
            this.txtBankEName.TabIndex = 3;
            // 
            // txtBankCName
            // 
            this.txtBankCName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBankCName.Location = new System.Drawing.Point(68, 58);
            this.txtBankCName.Name = "txtBankCName";
            this.txtBankCName.Size = new System.Drawing.Size(136, 21);
            this.txtBankCName.TabIndex = 2;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(4, 115);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 14);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "有效";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(4, 141);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "每页行数";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(4, 88);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "英文名称";
            // 
            // txtShortName
            // 
            this.txtShortName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShortName.Location = new System.Drawing.Point(68, 31);
            this.txtShortName.Name = "txtShortName";
            this.txtShortName.Size = new System.Drawing.Size(136, 21);
            this.txtShortName.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(4, 61);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "中文名称";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(4, 34);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "简称";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(4, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "所属公司";
            // 
            // nbMain
            // 
            this.nbMain.ActiveGroup = this.bgBase;
            this.nbMain.Controls.Add(this.bgcBase);
            this.nbMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.nbMain.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.bgBase});
            this.nbMain.Location = new System.Drawing.Point(0, 0);
            this.nbMain.Name = "nbMain";
            this.nbMain.OptionsNavPane.ExpandedWidth = 221;
            this.nbMain.Size = new System.Drawing.Size(222, 330);
            this.nbMain.TabIndex = 0;
            this.nbMain.Text = "navBarControl1";
            // 
            // BankSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.nbMain);
            this.Controls.Add(this.pnlButtom);
            this.Name = "BankSearch";
            this.Size = new System.Drawing.Size(222, 622);
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtom)).EndInit();
            this.pnlButtom.ResumeLayout(false);
            this.bgcBase.ResumeLayout(false);
            this.bgcBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxRow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBankEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBankCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShortName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbMain)).EndInit();
            this.nbMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlButtom;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnClare;
        private DevExpress.XtraNavBar.NavBarGroup bgBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer bgcBase;
        protected DevExpress.XtraEditors.CheckedComboBoxEdit chkcmbCompany;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton ckbValid;
        private DevExpress.XtraEditors.SpinEdit numMaxRow;
        private DevExpress.XtraEditors.TextEdit txtBankEName;
        private DevExpress.XtraEditors.TextEdit txtBankCName;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtShortName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraNavBar.NavBarControl nbMain;
    }
}
