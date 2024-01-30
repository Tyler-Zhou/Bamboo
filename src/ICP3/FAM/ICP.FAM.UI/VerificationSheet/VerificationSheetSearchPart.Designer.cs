namespace ICP.FAM.UI.VerificationSheet
{
    partial class VerificationSheetSearchPart
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
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBaseInfo = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.txtOperationNo = new DevExpress.XtraEditors.TextEdit();
            this.labOperationNo = new DevExpress.XtraEditors.LabelControl();
            this.nudTotalRecords = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtExpressNO = new DevExpress.XtraEditors.TextEdit();
            this.labExpressNO = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerName = new DevExpress.XtraEditors.TextEdit();
            this.labCustomerName = new DevExpress.XtraEditors.LabelControl();
            this.txtSheetNo = new DevExpress.XtraEditors.TextEdit();
            this.labSheetNo = new DevExpress.XtraEditors.LabelControl();
            this.chkIsFreightArrive = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBaseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalRecords.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpressNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSheetNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsFreightArrive.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 448);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(222, 56);
            this.panel1.TabIndex = 4;
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
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.Controls.Add(this.navBarGroupBaseInfo);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupInterval = 2;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(222, 442);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "基本信息";
            this.navBarGroup1.ControlContainer = this.navBarGroupBaseInfo;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 414;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarGroupBaseInfo
            // 
            this.navBarGroupBaseInfo.Controls.Add(this.chkIsFreightArrive);
            this.navBarGroupBaseInfo.Controls.Add(this.txtOperationNo);
            this.navBarGroupBaseInfo.Controls.Add(this.labOperationNo);
            this.navBarGroupBaseInfo.Controls.Add(this.nudTotalRecords);
            this.navBarGroupBaseInfo.Controls.Add(this.labelControl10);
            this.navBarGroupBaseInfo.Controls.Add(this.txtExpressNO);
            this.navBarGroupBaseInfo.Controls.Add(this.labExpressNO);
            this.navBarGroupBaseInfo.Controls.Add(this.txtCustomerName);
            this.navBarGroupBaseInfo.Controls.Add(this.labCustomerName);
            this.navBarGroupBaseInfo.Controls.Add(this.txtSheetNo);
            this.navBarGroupBaseInfo.Controls.Add(this.labSheetNo);
            this.navBarGroupBaseInfo.Name = "navBarGroupBaseInfo";
            this.navBarGroupBaseInfo.Size = new System.Drawing.Size(214, 412);
            this.navBarGroupBaseInfo.TabIndex = 3;
            // 
            // txtOperationNo
            // 
            this.txtOperationNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOperationNo.Location = new System.Drawing.Point(69, 7);
            this.txtOperationNo.Name = "txtOperationNo";
            this.txtOperationNo.Size = new System.Drawing.Size(142, 21);
            this.txtOperationNo.TabIndex = 36;
            // 
            // labOperationNo
            // 
            this.labOperationNo.Location = new System.Drawing.Point(4, 10);
            this.labOperationNo.Name = "labOperationNo";
            this.labOperationNo.Size = new System.Drawing.Size(69, 14);
            this.labOperationNo.TabIndex = 35;
            this.labOperationNo.Text = "OperationNo";
            // 
            // nudTotalRecords
            // 
            this.nudTotalRecords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nudTotalRecords.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudTotalRecords.Location = new System.Drawing.Point(69, 147);
            this.nudTotalRecords.Name = "nudTotalRecords";
            this.nudTotalRecords.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.nudTotalRecords.Size = new System.Drawing.Size(142, 21);
            this.nudTotalRecords.TabIndex = 4;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(4, 150);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(48, 14);
            this.labelControl10.TabIndex = 33;
            this.labelControl10.Text = "最大行数";
            // 
            // txtExpressNO
            // 
            this.txtExpressNO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpressNO.Location = new System.Drawing.Point(69, 88);
            this.txtExpressNO.Name = "txtExpressNO";
            this.txtExpressNO.Size = new System.Drawing.Size(142, 21);
            this.txtExpressNO.TabIndex = 3;
            // 
            // labExpressNO
            // 
            this.labExpressNO.Location = new System.Drawing.Point(4, 91);
            this.labExpressNO.Name = "labExpressNO";
            this.labExpressNO.Size = new System.Drawing.Size(58, 14);
            this.labExpressNO.TabIndex = 31;
            this.labExpressNO.Text = "ExpressNO";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomerName.Location = new System.Drawing.Point(69, 61);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(142, 21);
            this.txtCustomerName.TabIndex = 2;
            // 
            // labCustomerName
            // 
            this.labCustomerName.Location = new System.Drawing.Point(4, 64);
            this.labCustomerName.Name = "labCustomerName";
            this.labCustomerName.Size = new System.Drawing.Size(83, 14);
            this.labCustomerName.TabIndex = 29;
            this.labCustomerName.Text = "CustomerName";
            // 
            // txtSheetNo
            // 
            this.txtSheetNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSheetNo.Location = new System.Drawing.Point(69, 34);
            this.txtSheetNo.Name = "txtSheetNo";
            this.txtSheetNo.Size = new System.Drawing.Size(142, 21);
            this.txtSheetNo.TabIndex = 1;
            // 
            // labSheetNo
            // 
            this.labSheetNo.Location = new System.Drawing.Point(4, 37);
            this.labSheetNo.Name = "labSheetNo";
            this.labSheetNo.Size = new System.Drawing.Size(48, 14);
            this.labSheetNo.TabIndex = 25;
            this.labSheetNo.Text = "SheetNo";
            // 
            // chkIsFreightArrive
            // 
            this.chkIsFreightArrive.Location = new System.Drawing.Point(4, 118);
            this.chkIsFreightArrive.Name = "chkIsFreightArrive";
            this.chkIsFreightArrive.Properties.Caption = "IsFreightArrive";
            this.chkIsFreightArrive.Size = new System.Drawing.Size(146, 19);
            this.chkIsFreightArrive.TabIndex = 37;
            // 
            // VerificationSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.navBarControl1);
            this.Controls.Add(this.panel1);
            this.Name = "VerificationSearchPart";
            this.Size = new System.Drawing.Size(222, 504);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBaseInfo.ResumeLayout(false);
            this.navBarGroupBaseInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalRecords.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpressNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSheetNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsFreightArrive.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBaseInfo;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraEditors.TextEdit txtExpressNO;
        private DevExpress.XtraEditors.LabelControl labExpressNO;
        private DevExpress.XtraEditors.TextEdit txtCustomerName;
        private DevExpress.XtraEditors.LabelControl labCustomerName;
        private DevExpress.XtraEditors.TextEdit txtSheetNo;
        private DevExpress.XtraEditors.LabelControl labSheetNo;
        private DevExpress.XtraEditors.SpinEdit nudTotalRecords;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labOperationNo;
        private DevExpress.XtraEditors.TextEdit txtOperationNo;
        private DevExpress.XtraEditors.CheckEdit chkIsFreightArrive;
    }
}
