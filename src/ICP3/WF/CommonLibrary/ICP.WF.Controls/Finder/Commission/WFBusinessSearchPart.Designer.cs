namespace ICP.WF.Controls.Form.Commission
{
    partial class WFBusinessSearchPart
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
            this.ckbisApply = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.chkcmbCompany = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labBLNO = new DevExpress.XtraEditors.LabelControl();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labCtnNo = new DevExpress.XtraEditors.LabelControl();
            this.txtBLNo = new DevExpress.XtraEditors.TextEdit();
            this.txtCustomer = new DevExpress.XtraEditors.TextEdit();
            this.txtCtnNo = new DevExpress.XtraEditors.TextEdit();
            this.numpageCount = new DevExpress.XtraEditors.SpinEdit();
            this.labApply = new DevExpress.XtraEditors.LabelControl();
            this.labPageSize = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labOperationNo = new DevExpress.XtraEditors.LabelControl();
            this.txtOperationNo = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBLNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCtnNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numpageCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationNo.Properties)).BeginInit();
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
            this.navBarControl1.Size = new System.Drawing.Size(224, 380);
            this.navBarControl1.TabIndex = 2;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarBaseInfo
            // 
            this.navBarBaseInfo.Caption = "Base Info";
            this.navBarBaseInfo.ControlContainer = this.navBarGroupBase;
            this.navBarBaseInfo.Expanded = true;
            this.navBarBaseInfo.GroupClientHeight = 182;
            this.navBarBaseInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBaseInfo.Name = "navBarBaseInfo";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.ckbisApply);
            this.navBarGroupBase.Controls.Add(this.chkcmbCompany);
            this.navBarGroupBase.Controls.Add(this.labBLNO);
            this.navBarGroupBase.Controls.Add(this.labCustomer);
            this.navBarGroupBase.Controls.Add(this.labCtnNo);
            this.navBarGroupBase.Controls.Add(this.txtBLNo);
            this.navBarGroupBase.Controls.Add(this.txtCustomer);
            this.navBarGroupBase.Controls.Add(this.txtCtnNo);
            this.navBarGroupBase.Controls.Add(this.numpageCount);
            this.navBarGroupBase.Controls.Add(this.labApply);
            this.navBarGroupBase.Controls.Add(this.labPageSize);
            this.navBarGroupBase.Controls.Add(this.labCompany);
            this.navBarGroupBase.Controls.Add(this.labOperationNo);
            this.navBarGroupBase.Controls.Add(this.txtOperationNo);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(216, 180);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // ckbisApply
            // 
            this.ckbisApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbisApply.Checked = null;
            this.ckbisApply.CheckedText = "TRUE";
            this.ckbisApply.Location = new System.Drawing.Point(72, 124);
            this.ckbisApply.Name = "ckbisApply";
            this.ckbisApply.NULLText = "ALL";
            this.ckbisApply.Size = new System.Drawing.Size(136, 22);
            this.ckbisApply.TabIndex = 5;
            this.ckbisApply.UnCheckedText = "FALSE";
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.Location = new System.Drawing.Point(72, 3);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkcmbCompany.Size = new System.Drawing.Size(136, 21);
            this.chkcmbCompany.TabIndex = 0;
            // 
            // labBLNO
            // 
            this.labBLNO.Location = new System.Drawing.Point(3, 54);
            this.labBLNO.Name = "labBLNO";
            this.labBLNO.Size = new System.Drawing.Size(34, 14);
            this.labBLNO.TabIndex = 53;
            this.labBLNO.Text = "BL NO";
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(3, 102);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 47;
            this.labCustomer.Text = "Customer";
            // 
            // labCtnNo
            // 
            this.labCtnNo.Location = new System.Drawing.Point(3, 78);
            this.labCtnNo.Name = "labCtnNo";
            this.labCtnNo.Size = new System.Drawing.Size(40, 14);
            this.labCtnNo.TabIndex = 48;
            this.labCtnNo.Text = "Ctn.NO";
            // 
            // txtBLNo
            // 
            this.txtBLNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBLNo.Location = new System.Drawing.Point(72, 51);
            this.txtBLNo.Name = "txtBLNo";
            this.txtBLNo.Size = new System.Drawing.Size(136, 21);
            this.txtBLNo.TabIndex = 2;
            // 
            // txtCustomer
            // 
            this.txtCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomer.Location = new System.Drawing.Point(72, 99);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(136, 21);
            this.txtCustomer.TabIndex = 4;
            // 
            // txtCtnNo
            // 
            this.txtCtnNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCtnNo.Location = new System.Drawing.Point(72, 75);
            this.txtCtnNo.Name = "txtCtnNo";
            this.txtCtnNo.Size = new System.Drawing.Size(136, 21);
            this.txtCtnNo.TabIndex = 3;
            // 
            // numpageCount
            // 
            this.numpageCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numpageCount.EditValue = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numpageCount.Location = new System.Drawing.Point(72, 150);
            this.numpageCount.Name = "numpageCount";
            this.numpageCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numpageCount.Properties.IsFloatValue = false;
            this.numpageCount.Properties.Mask.EditMask = "N00";
            this.numpageCount.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numpageCount.Size = new System.Drawing.Size(136, 21);
            this.numpageCount.TabIndex = 6;
            // 
            // labApply
            // 
            this.labApply.Location = new System.Drawing.Point(1, 127);
            this.labApply.Name = "labApply";
            this.labApply.Size = new System.Drawing.Size(43, 14);
            this.labApply.TabIndex = 45;
            this.labApply.Text = "Is Apply";
            // 
            // labPageSize
            // 
            this.labPageSize.Location = new System.Drawing.Point(3, 153);
            this.labPageSize.Name = "labPageSize";
            this.labPageSize.Size = new System.Drawing.Size(52, 14);
            this.labPageSize.TabIndex = 45;
            this.labPageSize.Text = "Page Size";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(3, 6);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 39;
            this.labCompany.Text = "Company";
            // 
            // labOperationNo
            // 
            this.labOperationNo.Location = new System.Drawing.Point(3, 30);
            this.labOperationNo.Name = "labOperationNo";
            this.labOperationNo.Size = new System.Drawing.Size(69, 14);
            this.labOperationNo.TabIndex = 32;
            this.labOperationNo.Text = "OperationNo";
            // 
            // txtOperationNo
            // 
            this.txtOperationNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOperationNo.Location = new System.Drawing.Point(72, 27);
            this.txtOperationNo.Name = "txtOperationNo";
            this.txtOperationNo.Size = new System.Drawing.Size(136, 21);
            this.txtOperationNo.TabIndex = 1;
            // 
            // WFBusinessSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelBottom);
            this.Name = "WFBusinessSearchPart";
            this.Size = new System.Drawing.Size(224, 628);
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBLNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCtnNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numpageCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationNo.Properties)).EndInit();
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
        protected DevExpress.XtraEditors.LabelControl labBLNO;
        protected DevExpress.XtraEditors.LabelControl labCustomer;
        protected DevExpress.XtraEditors.LabelControl labCtnNo;
        protected DevExpress.XtraEditors.TextEdit txtBLNo;
        protected DevExpress.XtraEditors.TextEdit txtCustomer;
        protected DevExpress.XtraEditors.TextEdit txtCtnNo;
        protected DevExpress.XtraEditors.SpinEdit numpageCount;
        protected DevExpress.XtraEditors.LabelControl labPageSize;
        protected DevExpress.XtraEditors.LabelControl labCompany;
        protected DevExpress.XtraEditors.LabelControl labOperationNo;
        protected DevExpress.XtraEditors.TextEdit txtOperationNo;
        protected DevExpress.XtraEditors.CheckedComboBoxEdit chkcmbCompany;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton ckbisApply;
        protected DevExpress.XtraEditors.LabelControl labApply;
    }
}
