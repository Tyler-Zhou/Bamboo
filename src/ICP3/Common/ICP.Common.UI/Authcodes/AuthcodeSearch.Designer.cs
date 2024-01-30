namespace ICP.Common.UI.Authcodes
{
    partial class AuthcodeSearch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlButtom = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnClare = new DevExpress.XtraEditors.SimpleButton();
            this.chkIsValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.bgBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.bgcBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.labIsValid = new DevExpress.XtraEditors.LabelControl();
            this.nbMain = new DevExpress.XtraNavBar.NavBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtom)).BeginInit();
            this.pnlButtom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbMain)).BeginInit();
            this.nbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtom
            // 
            this.pnlButtom.Controls.Add(this.btnSearch);
            this.pnlButtom.Controls.Add(this.btnClare);
            this.pnlButtom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtom.Location = new System.Drawing.Point(0, 512);
            this.pnlButtom.Name = "pnlButtom";
            this.pnlButtom.Size = new System.Drawing.Size(199, 52);
            this.pnlButtom.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(114, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClare
            // 
            this.btnClare.Location = new System.Drawing.Point(13, 15);
            this.btnClare.Name = "btnClare";
            this.btnClare.Size = new System.Drawing.Size(75, 23);
            this.btnClare.TabIndex = 0;
            this.btnClare.Text = "清空(&L)";
            // 
            // chkIsValid
            // 
            this.chkIsValid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIsValid.Checked = null;
            this.chkIsValid.CheckedText = "TRUE";
            this.chkIsValid.Location = new System.Drawing.Point(58, 51);
            this.chkIsValid.Name = "chkIsValid";
            this.chkIsValid.NULLText = "ALL";
            this.chkIsValid.Size = new System.Drawing.Size(130, 22);
            this.chkIsValid.TabIndex = 10;
            this.chkIsValid.UnCheckedText = "FALSE";
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
            this.bgcBase.Name = "bgcBase";
            this.bgcBase.Size = new System.Drawing.Size(175, 173);
            this.bgcBase.TabIndex = 0;
            // 
            // txtCode
            // 
            this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCode.Location = new System.Drawing.Point(58, 16);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(130, 21);
            this.txtCode.TabIndex = 6;
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(8, 19);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(24, 14);
            this.labCode.TabIndex = 11;
            this.labCode.Text = "代码";
            // 
            // labIsValid
            // 
            this.labIsValid.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labIsValid.Appearance.Options.UseBackColor = true;
            this.labIsValid.Location = new System.Drawing.Point(8, 54);
            this.labIsValid.Name = "labIsValid";
            this.labIsValid.Size = new System.Drawing.Size(36, 14);
            this.labIsValid.TabIndex = 7;
            this.labIsValid.Text = "有效性";
            // 
            // nbMain
            // 
            this.nbMain.ActiveGroup = this.bgBase;
            this.nbMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nbMain.Controls.Add(this.bgcBase);
            this.nbMain.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.bgBase});
            this.nbMain.Location = new System.Drawing.Point(0, -1);
            this.nbMain.Name = "nbMain";
            this.nbMain.OptionsNavPane.ExpandedWidth = 221;
            this.nbMain.Size = new System.Drawing.Size(179, 330);
            this.nbMain.TabIndex = 0;
            this.nbMain.Text = "navBarControl1";
            // 
            // AuthcodeSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlButtom);
            this.Controls.Add(this.chkIsValid);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.labCode);
            this.Controls.Add(this.labIsValid);
            this.Name = "AuthcodeSearch";
            this.Size = new System.Drawing.Size(199, 564);
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtom)).EndInit();
            this.pnlButtom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbMain)).EndInit();
            this.nbMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlButtom;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnClare;
        private Framework.ClientComponents.Controls.LWCheckButton chkIsValid;
        private DevExpress.XtraNavBar.NavBarGroup bgBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer bgcBase;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.LabelControl labIsValid;
        private DevExpress.XtraNavBar.NavBarControl nbMain;
    }
}