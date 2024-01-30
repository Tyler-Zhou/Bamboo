namespace ICP.FAM.UI.Business
{
    partial class BusinessSearchPart
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
            this.mcmbOperate = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.mcmbSales = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.chkcmbCompany = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labBLNO = new DevExpress.XtraEditors.LabelControl();
            this.labSales = new DevExpress.XtraEditors.LabelControl();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labCtnNo = new DevExpress.XtraEditors.LabelControl();
            this.txtBLNo = new DevExpress.XtraEditors.TextEdit();
            this.txtCustomer = new DevExpress.XtraEditors.TextEdit();
            this.txtCtnNo = new DevExpress.XtraEditors.TextEdit();
            this.seMax = new DevExpress.XtraEditors.SpinEdit();
            this.numpageCount = new DevExpress.XtraEditors.SpinEdit();
            this.seMin = new DevExpress.XtraEditors.SpinEdit();
            this.labPageSize = new DevExpress.XtraEditors.LabelControl();
            this.labMax = new DevExpress.XtraEditors.LabelControl();
            this.labOperate = new DevExpress.XtraEditors.LabelControl();
            this.labMin = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labOperationNo = new DevExpress.XtraEditors.LabelControl();
            this.chkProfit = new DevExpress.XtraEditors.CheckEdit();
            this.txtOperationNo = new DevExpress.XtraEditors.TextEdit();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panelType = new System.Windows.Forms.Panel();
            this.panelOperationType = new System.Windows.Forms.Panel();
            this.cmbOperationType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labOperationType = new DevExpress.XtraEditors.LabelControl();
            this.navBarOther = new DevExpress.XtraNavBar.NavBarGroup();
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
            ((System.ComponentModel.ISupportInitialize)(this.seMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numpageCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkProfit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationNo.Properties)).BeginInit();
            this.navBarGroupControlContainer1.SuspendLayout();
            this.panelOperationType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperationType.Properties)).BeginInit();
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
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupInterval = 2;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarBaseInfo,
            this.navBarOther});
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
            this.navBarBaseInfo.GroupClientHeight = 273;
            this.navBarBaseInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBaseInfo.Name = "navBarBaseInfo";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.mcmbOperate);
            this.navBarGroupBase.Controls.Add(this.mcmbSales);
            this.navBarGroupBase.Controls.Add(this.chkcmbCompany);
            this.navBarGroupBase.Controls.Add(this.labBLNO);
            this.navBarGroupBase.Controls.Add(this.labSales);
            this.navBarGroupBase.Controls.Add(this.labCustomer);
            this.navBarGroupBase.Controls.Add(this.labCtnNo);
            this.navBarGroupBase.Controls.Add(this.txtBLNo);
            this.navBarGroupBase.Controls.Add(this.txtCustomer);
            this.navBarGroupBase.Controls.Add(this.txtCtnNo);
            this.navBarGroupBase.Controls.Add(this.seMax);
            this.navBarGroupBase.Controls.Add(this.numpageCount);
            this.navBarGroupBase.Controls.Add(this.seMin);
            this.navBarGroupBase.Controls.Add(this.labPageSize);
            this.navBarGroupBase.Controls.Add(this.labMax);
            this.navBarGroupBase.Controls.Add(this.labOperate);
            this.navBarGroupBase.Controls.Add(this.labMin);
            this.navBarGroupBase.Controls.Add(this.labCompany);
            this.navBarGroupBase.Controls.Add(this.labOperationNo);
            this.navBarGroupBase.Controls.Add(this.chkProfit);
            this.navBarGroupBase.Controls.Add(this.txtOperationNo);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(216, 271);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // mcmbOperate
            // 
            this.mcmbOperate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mcmbOperate.EditText = "";
            this.mcmbOperate.EditValue = null;
            this.mcmbOperate.Location = new System.Drawing.Point(72, 147);
            this.mcmbOperate.Name = "mcmbOperate";
            this.mcmbOperate.ReadOnly = false;
            this.mcmbOperate.Size = new System.Drawing.Size(136, 21);
            this.mcmbOperate.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbOperate.TabIndex = 6;
            this.mcmbOperate.ToolTip = "";
            // 
            // mcmbSales
            // 
            this.mcmbSales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mcmbSales.EditText = "";
            this.mcmbSales.EditValue = null;
            this.mcmbSales.Location = new System.Drawing.Point(72, 123);
            this.mcmbSales.Name = "mcmbSales";
            this.mcmbSales.ReadOnly = false;
            this.mcmbSales.Size = new System.Drawing.Size(136, 21);
            this.mcmbSales.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbSales.TabIndex = 5;
            this.mcmbSales.ToolTip = "";
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
            // labSales
            // 
            this.labSales.Location = new System.Drawing.Point(3, 126);
            this.labSales.Name = "labSales";
            this.labSales.Size = new System.Drawing.Size(27, 14);
            this.labSales.TabIndex = 49;
            this.labSales.Text = "Sales";
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
            // seMax
            // 
            this.seMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.seMax.EditValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.seMax.Enabled = false;
            this.seMax.Location = new System.Drawing.Point(72, 220);
            this.seMax.Name = "seMax";
            this.seMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seMax.Size = new System.Drawing.Size(136, 21);
            this.seMax.TabIndex = 8;
            // 
            // numpageCount
            // 
            this.numpageCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numpageCount.EditValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numpageCount.Location = new System.Drawing.Point(72, 244);
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
            this.numpageCount.TabIndex = 9;
            // 
            // seMin
            // 
            this.seMin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.seMin.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seMin.Enabled = false;
            this.seMin.Location = new System.Drawing.Point(72, 196);
            this.seMin.Name = "seMin";
            this.seMin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seMin.Size = new System.Drawing.Size(136, 21);
            this.seMin.TabIndex = 7;
            // 
            // labPageSize
            // 
            this.labPageSize.Location = new System.Drawing.Point(6, 247);
            this.labPageSize.Name = "labPageSize";
            this.labPageSize.Size = new System.Drawing.Size(52, 14);
            this.labPageSize.TabIndex = 45;
            this.labPageSize.Text = "Page Size";
            // 
            // labMax
            // 
            this.labMax.Location = new System.Drawing.Point(6, 223);
            this.labMax.Name = "labMax";
            this.labMax.Size = new System.Drawing.Size(21, 14);
            this.labMax.TabIndex = 37;
            this.labMax.Text = "Max";
            // 
            // labOperate
            // 
            this.labOperate.Location = new System.Drawing.Point(3, 150);
            this.labOperate.Name = "labOperate";
            this.labOperate.Size = new System.Drawing.Size(45, 14);
            this.labOperate.TabIndex = 36;
            this.labOperate.Text = "Operate";
            // 
            // labMin
            // 
            this.labMin.Location = new System.Drawing.Point(6, 199);
            this.labMin.Name = "labMin";
            this.labMin.Size = new System.Drawing.Size(18, 14);
            this.labMin.TabIndex = 35;
            this.labMin.Text = "Min";
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
            // chkProfit
            // 
            this.chkProfit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkProfit.Location = new System.Drawing.Point(6, 174);
            this.chkProfit.Name = "chkProfit";
            this.chkProfit.Properties.Caption = "Profit";
            this.chkProfit.Size = new System.Drawing.Size(202, 19);
            this.chkProfit.TabIndex = 31;
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
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.panelType);
            this.navBarGroupControlContainer1.Controls.Add(this.panelOperationType);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(216, 28);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // panelType
            // 
            this.panelType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelType.Location = new System.Drawing.Point(0, 25);
            this.panelType.Name = "panelType";
            this.panelType.Size = new System.Drawing.Size(216, 3);
            this.panelType.TabIndex = 11;
            // 
            // panelOperationType
            // 
            this.panelOperationType.Controls.Add(this.cmbOperationType);
            this.panelOperationType.Controls.Add(this.labOperationType);
            this.panelOperationType.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOperationType.Location = new System.Drawing.Point(0, 0);
            this.panelOperationType.Name = "panelOperationType";
            this.panelOperationType.Size = new System.Drawing.Size(216, 25);
            this.panelOperationType.TabIndex = 10;
            // 
            // cmbOperationType
            // 
            this.cmbOperationType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbOperationType.Location = new System.Drawing.Point(72, 3);
            this.cmbOperationType.Name = "cmbOperationType";
            this.cmbOperationType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOperationType.Size = new System.Drawing.Size(136, 21);
            this.cmbOperationType.TabIndex = 0;
            // 
            // labOperationType
            // 
            this.labOperationType.Location = new System.Drawing.Point(5, 6);
            this.labOperationType.Name = "labOperationType";
            this.labOperationType.Size = new System.Drawing.Size(28, 14);
            this.labOperationType.TabIndex = 7;
            this.labOperationType.Text = "Type";
            // 
            // navBarOther
            // 
            this.navBarOther.Caption = "Other";
            this.navBarOther.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarOther.Expanded = true;
            this.navBarOther.GroupClientHeight = 30;
            this.navBarOther.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarOther.Name = "navBarOther";
            // 
            // BusinessSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelBottom);
            this.Name = "BusinessSearchPart";
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
            ((System.ComponentModel.ISupportInitialize)(this.seMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numpageCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkProfit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationNo.Properties)).EndInit();
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.panelOperationType.ResumeLayout(false);
            this.panelOperationType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperationType.Properties)).EndInit();
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
        protected DevExpress.XtraEditors.LabelControl labSales;
        protected DevExpress.XtraEditors.LabelControl labCustomer;
        protected DevExpress.XtraEditors.LabelControl labCtnNo;
        protected DevExpress.XtraEditors.TextEdit txtBLNo;
        protected DevExpress.XtraEditors.TextEdit txtCustomer;
        protected DevExpress.XtraEditors.TextEdit txtCtnNo;
        private DevExpress.XtraEditors.SpinEdit seMax;
        protected DevExpress.XtraEditors.SpinEdit numpageCount;
        private DevExpress.XtraEditors.SpinEdit seMin;
        protected DevExpress.XtraEditors.LabelControl labPageSize;
        protected DevExpress.XtraEditors.LabelControl labMax;
        protected DevExpress.XtraEditors.LabelControl labOperate;
        protected DevExpress.XtraEditors.LabelControl labMin;
        protected DevExpress.XtraEditors.LabelControl labCompany;
        protected DevExpress.XtraEditors.LabelControl labOperationNo;
        private DevExpress.XtraEditors.CheckEdit chkProfit;
        protected DevExpress.XtraEditors.TextEdit txtOperationNo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private System.Windows.Forms.Panel panelOperationType;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbOperationType;
        protected DevExpress.XtraEditors.LabelControl labOperationType;
        private DevExpress.XtraNavBar.NavBarGroup navBarOther;
        private System.Windows.Forms.Panel panelType;
        protected DevExpress.XtraEditors.CheckedComboBoxEdit chkcmbCompany;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbOperate;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbSales;
    }
}
