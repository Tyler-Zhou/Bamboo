namespace ICP.WF.Controls.Form.CustomerExpense
{
    partial class WFCustomerExpenseSearchPart
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
            this.panelBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarBaseInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.numMaxCount = new DevExpress.XtraEditors.SpinEdit();
            this.labMaxRow = new DevExpress.XtraEditors.LabelControl();
            this.labCountry = new DevExpress.XtraEditors.LabelControl();
            this.labEMail = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labEName = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labKeyWord = new DevExpress.XtraEditors.LabelControl();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.txtCountry = new DevExpress.XtraEditors.TextEdit();
            this.txtEMail = new DevExpress.XtraEditors.TextEdit();
            this.txtContact = new DevExpress.XtraEditors.TextEdit();
            this.txtEName = new DevExpress.XtraEditors.TextEdit();
            this.txtCName = new DevExpress.XtraEditors.TextEdit();
            this.txtKeyWord = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEMail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContact.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyWord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnClear);
            this.panelBottom.Controls.Add(this.btnSearch);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 572);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(224, 56);
            this.panelBottom.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(26, 19);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(126, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.navBarControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(224, 572);
            this.panel2.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarBaseInfo;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupInterval = 2;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarBaseInfo});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 247;
            this.navBarControl1.Size = new System.Drawing.Size(224, 349);
            this.navBarControl1.TabIndex = 2;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarBaseInfo
            // 
            this.navBarBaseInfo.Caption = "Base Info";
            this.navBarBaseInfo.ControlContainer = this.navBarGroupBase;
            this.navBarBaseInfo.Expanded = true;
            this.navBarBaseInfo.GroupClientHeight = 237;
            this.navBarBaseInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBaseInfo.Name = "navBarBaseInfo";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.numMaxCount);
            this.navBarGroupBase.Controls.Add(this.labMaxRow);
            this.navBarGroupBase.Controls.Add(this.labCountry);
            this.navBarGroupBase.Controls.Add(this.labEMail);
            this.navBarGroupBase.Controls.Add(this.labelControl1);
            this.navBarGroupBase.Controls.Add(this.labEName);
            this.navBarGroupBase.Controls.Add(this.labelControl2);
            this.navBarGroupBase.Controls.Add(this.labKeyWord);
            this.navBarGroupBase.Controls.Add(this.labCode);
            this.navBarGroupBase.Controls.Add(this.txtCountry);
            this.navBarGroupBase.Controls.Add(this.txtEMail);
            this.navBarGroupBase.Controls.Add(this.txtContact);
            this.navBarGroupBase.Controls.Add(this.txtEName);
            this.navBarGroupBase.Controls.Add(this.txtCName);
            this.navBarGroupBase.Controls.Add(this.txtKeyWord);
            this.navBarGroupBase.Controls.Add(this.txtCode);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(216, 235);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // numMaxCount
            // 
            this.numMaxCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numMaxCount.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMaxCount.Location = new System.Drawing.Point(73, 196);
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
            this.numMaxCount.Size = new System.Drawing.Size(136, 21);
            this.numMaxCount.TabIndex = 7;
            // 
            // labMaxRow
            // 
            this.labMaxRow.Location = new System.Drawing.Point(4, 199);
            this.labMaxRow.Name = "labMaxRow";
            this.labMaxRow.Size = new System.Drawing.Size(49, 14);
            this.labMaxRow.TabIndex = 47;
            this.labMaxRow.Text = "Max Row";
            // 
            // labCountry
            // 
            this.labCountry.Location = new System.Drawing.Point(4, 171);
            this.labCountry.Name = "labCountry";
            this.labCountry.Size = new System.Drawing.Size(43, 14);
            this.labCountry.TabIndex = 32;
            this.labCountry.Text = "Country";
            // 
            // labEMail
            // 
            this.labEMail.Location = new System.Drawing.Point(4, 143);
            this.labEMail.Name = "labEMail";
            this.labEMail.Size = new System.Drawing.Size(26, 14);
            this.labEMail.TabIndex = 32;
            this.labEMail.Text = "EMail";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(4, 116);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(43, 14);
            this.labelControl1.TabIndex = 32;
            this.labelControl1.Text = "Contact";
            // 
            // labEName
            // 
            this.labEName.Location = new System.Drawing.Point(4, 89);
            this.labEName.Name = "labEName";
            this.labEName.Size = new System.Drawing.Size(38, 14);
            this.labEName.TabIndex = 32;
            this.labEName.Text = "EName";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(4, 62);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(38, 14);
            this.labelControl2.TabIndex = 32;
            this.labelControl2.Text = "CName";
            // 
            // labKeyWord
            // 
            this.labKeyWord.Location = new System.Drawing.Point(4, 35);
            this.labKeyWord.Name = "labKeyWord";
            this.labKeyWord.Size = new System.Drawing.Size(54, 14);
            this.labKeyWord.TabIndex = 32;
            this.labKeyWord.Text = "Key Word";
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(4, 8);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(28, 14);
            this.labCode.TabIndex = 32;
            this.labCode.Text = "Code";
            // 
            // txtCountry
            // 
            this.txtCountry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCountry.Location = new System.Drawing.Point(73, 168);
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.Size = new System.Drawing.Size(136, 21);
            this.txtCountry.TabIndex = 6;
            // 
            // txtEMail
            // 
            this.txtEMail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEMail.Location = new System.Drawing.Point(73, 140);
            this.txtEMail.Name = "txtEMail";
            this.txtEMail.Size = new System.Drawing.Size(136, 21);
            this.txtEMail.TabIndex = 5;
            // 
            // txtContact
            // 
            this.txtContact.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContact.Location = new System.Drawing.Point(73, 113);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(136, 21);
            this.txtContact.TabIndex = 4;
            // 
            // txtEName
            // 
            this.txtEName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEName.Location = new System.Drawing.Point(73, 86);
            this.txtEName.Name = "txtEName";
            this.txtEName.Size = new System.Drawing.Size(136, 21);
            this.txtEName.TabIndex = 3;
            // 
            // txtCName
            // 
            this.txtCName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCName.Location = new System.Drawing.Point(73, 59);
            this.txtCName.Name = "txtCName";
            this.txtCName.Size = new System.Drawing.Size(136, 21);
            this.txtCName.TabIndex = 2;
            // 
            // txtKeyWord
            // 
            this.txtKeyWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyWord.Location = new System.Drawing.Point(73, 32);
            this.txtKeyWord.Name = "txtKeyWord";
            this.txtKeyWord.Size = new System.Drawing.Size(136, 21);
            this.txtKeyWord.TabIndex = 1;
            // 
            // txtCode
            // 
            this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCode.Location = new System.Drawing.Point(73, 5);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(136, 21);
            this.txtCode.TabIndex = 0;
            // 
            // WFCustomerExpenseSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelBottom);
            this.Name = "WFCustomerExpenseSearchPart";
            this.Size = new System.Drawing.Size(224, 628);
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEMail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContact.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyWord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelBottom;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.XtraScrollableControl panel2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarBaseInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        protected DevExpress.XtraEditors.LabelControl labCode;
        protected DevExpress.XtraEditors.TextEdit txtCode;
        protected DevExpress.XtraEditors.TextEdit txtKeyWord;
        protected DevExpress.XtraEditors.LabelControl labEName;
        protected DevExpress.XtraEditors.LabelControl labelControl2;
        protected DevExpress.XtraEditors.LabelControl labKeyWord;
        protected DevExpress.XtraEditors.TextEdit txtEName;
        protected DevExpress.XtraEditors.TextEdit txtCName;
        protected DevExpress.XtraEditors.LabelControl labEMail;
        protected DevExpress.XtraEditors.LabelControl labelControl1;
        protected DevExpress.XtraEditors.TextEdit txtEMail;
        protected DevExpress.XtraEditors.TextEdit txtContact;
        protected DevExpress.XtraEditors.LabelControl labCountry;
        protected DevExpress.XtraEditors.TextEdit txtCountry;
        protected DevExpress.XtraEditors.SpinEdit numMaxCount;
        protected DevExpress.XtraEditors.LabelControl labMaxRow;
    }
}
