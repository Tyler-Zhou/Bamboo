namespace ICP.ReportCenter.UI.FinanceOEReports
{
   partial class VoucherInfoSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoucherInfoSearchPart));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnCheckLedger = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnCheckFinanceCode = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.operationDatePart1 = new ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.chkcmbVoucherType = new ICP.ReportCenter.UI.Comm.Controls.CheckBoxComboBox();
            this.labVoucherType = new DevExpress.XtraEditors.LabelControl();
            this.treeBoxSalesDep = new ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl();
            this.chkTotalForGL = new DevExpress.XtraEditors.CheckEdit();
            this.chkPay = new DevExpress.XtraEditors.CheckEdit();
            this.chkReceive = new DevExpress.XtraEditors.CheckEdit();
            this.txtGLNo = new DevExpress.XtraEditors.TextEdit();
            this.txtVoucherNo = new DevExpress.XtraEditors.TextEdit();
            this.cmbInnerType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.txtBillTo = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.labGLNo = new DevExpress.XtraEditors.LabelControl();
            this.labVoucherNo = new DevExpress.XtraEditors.LabelControl();
            this.labInnerType = new DevExpress.XtraEditors.LabelControl();
            this.labBillTo = new DevExpress.XtraEditors.LabelControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkTotalForGL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReceive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGLNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucherNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbInnerType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillTo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.btnCheckLedger);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnCheckFinanceCode);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 552);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 104);
            this.panel1.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(11, 74);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(91, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "备份凭证";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnCheckLedger
            // 
            this.btnCheckLedger.Location = new System.Drawing.Point(11, 10);
            this.btnCheckLedger.Name = "btnCheckLedger";
            this.btnCheckLedger.Size = new System.Drawing.Size(91, 23);
            this.btnCheckLedger.TabIndex = 1;
            this.btnCheckLedger.Text = "检测凭证";
            this.btnCheckLedger.Click += new System.EventHandler(this.btnCheckLedger_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(132, 43);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(91, 23);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "导出凭证";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnCheckFinanceCode
            // 
            this.btnCheckFinanceCode.Location = new System.Drawing.Point(11, 43);
            this.btnCheckFinanceCode.Name = "btnCheckFinanceCode";
            this.btnCheckFinanceCode.Size = new System.Drawing.Size(91, 23);
            this.btnCheckFinanceCode.TabIndex = 0;
            this.btnCheckFinanceCode.Text = "验证财务数据";
            this.btnCheckFinanceCode.Click += new System.EventHandler(this.btnCheckFinanceCode_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(132, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(91, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&R)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.navBarControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 552);
            this.panel2.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarDate;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarDate,
            this.nbarBase});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(240, 525);
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarDate
            // 
            this.nbarDate.Caption = "日期";
            this.nbarDate.ControlContainer = this.navBarGroupBase;
            this.nbarDate.Expanded = true;
            this.nbarDate.GroupClientHeight = 128;
            this.nbarDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarDate.Name = "nbarDate";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.operationDatePart1);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(232, 126);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // operationDatePart1
            // 
            this.operationDatePart1.BaseMultiLanguageList = null;
            this.operationDatePart1.BasePartList = null;
            this.operationDatePart1.CodeValuePairs = null;
            this.operationDatePart1.ControlsList = null;
            this.operationDatePart1.Dock = System.Windows.Forms.DockStyle.Top;
            this.operationDatePart1.FormName = "OperationDatePart";
            this.operationDatePart1.IsMultiLanguage = true;
            this.operationDatePart1.Location = new System.Drawing.Point(0, 0);
            this.operationDatePart1.Name = "operationDatePart1";
            this.operationDatePart1.Resources = null;
            this.operationDatePart1.Size = new System.Drawing.Size(232, 122);
            this.operationDatePart1.TabIndex = 0;
            this.operationDatePart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("operationDatePart1.UsedMessages")));
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.chkcmbVoucherType);
            this.navBarGroupControlContainer1.Controls.Add(this.labVoucherType);
            this.navBarGroupControlContainer1.Controls.Add(this.treeBoxSalesDep);
            this.navBarGroupControlContainer1.Controls.Add(this.chkTotalForGL);
            this.navBarGroupControlContainer1.Controls.Add(this.chkPay);
            this.navBarGroupControlContainer1.Controls.Add(this.chkReceive);
            this.navBarGroupControlContainer1.Controls.Add(this.txtGLNo);
            this.navBarGroupControlContainer1.Controls.Add(this.txtVoucherNo);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbInnerType);
            this.navBarGroupControlContainer1.Controls.Add(this.txtBillTo);
            this.navBarGroupControlContainer1.Controls.Add(this.labGLNo);
            this.navBarGroupControlContainer1.Controls.Add(this.labVoucherNo);
            this.navBarGroupControlContainer1.Controls.Add(this.labInnerType);
            this.navBarGroupControlContainer1.Controls.Add(this.labBillTo);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(232, 230);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // chkcmbVoucherType
            // 
            this.chkcmbVoucherType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbVoucherType.Location = new System.Drawing.Point(64, 30);
            this.chkcmbVoucherType.Name = "chkcmbVoucherType";
            this.chkcmbVoucherType.NullText = "";
            this.chkcmbVoucherType.Size = new System.Drawing.Size(163, 21);
            this.chkcmbVoucherType.SplitText = ",";
            this.chkcmbVoucherType.TabIndex = 17;
            // 
            // labVoucherType
            // 
            this.labVoucherType.Location = new System.Drawing.Point(7, 31);
            this.labVoucherType.Name = "labVoucherType";
            this.labVoucherType.Size = new System.Drawing.Size(48, 14);
            this.labVoucherType.TabIndex = 18;
            this.labVoucherType.Text = "凭证类型";
            // 
            // treeBoxSalesDep
            // 
            this.treeBoxSalesDep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeBoxSalesDep.EditText = "";
            this.treeBoxSalesDep.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("treeBoxSalesDep.EditValue")));
            this.treeBoxSalesDep.Location = new System.Drawing.Point(7, 192);
            this.treeBoxSalesDep.Name = "treeBoxSalesDep";
            this.treeBoxSalesDep.ReadOnly = false;
            this.treeBoxSalesDep.ShowDepartment = false;
            this.treeBoxSalesDep.Size = new System.Drawing.Size(220, 21);
            this.treeBoxSalesDep.SplitString = ",";
            this.treeBoxSalesDep.TabIndex = 2;
            // 
            // chkTotalForGL
            // 
            this.chkTotalForGL.Location = new System.Drawing.Point(64, 167);
            this.chkTotalForGL.Name = "chkTotalForGL";
            this.chkTotalForGL.Properties.Caption = "按会计科目分组统计";
            this.chkTotalForGL.Size = new System.Drawing.Size(147, 19);
            this.chkTotalForGL.TabIndex = 16;
            // 
            // chkPay
            // 
            this.chkPay.Enabled = false;
            this.chkPay.Location = new System.Drawing.Point(136, 142);
            this.chkPay.Name = "chkPay";
            this.chkPay.Properties.Caption = "实付凭证";
            this.chkPay.Size = new System.Drawing.Size(75, 19);
            this.chkPay.TabIndex = 16;
            // 
            // chkReceive
            // 
            this.chkReceive.Enabled = false;
            this.chkReceive.Location = new System.Drawing.Point(64, 142);
            this.chkReceive.Name = "chkReceive";
            this.chkReceive.Properties.Caption = "实收凭证";
            this.chkReceive.Size = new System.Drawing.Size(75, 19);
            this.chkReceive.TabIndex = 16;
            // 
            // txtGLNo
            // 
            this.txtGLNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGLNo.Location = new System.Drawing.Point(64, 88);
            this.txtGLNo.Name = "txtGLNo";
            this.txtGLNo.Size = new System.Drawing.Size(163, 21);
            this.txtGLNo.TabIndex = 15;
            // 
            // txtVoucherNo
            // 
            this.txtVoucherNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVoucherNo.Location = new System.Drawing.Point(64, 61);
            this.txtVoucherNo.Name = "txtVoucherNo";
            this.txtVoucherNo.Size = new System.Drawing.Size(163, 21);
            this.txtVoucherNo.TabIndex = 15;
            // 
            // cmbInnerType
            // 
            this.cmbInnerType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInnerType.Enabled = false;
            this.cmbInnerType.Location = new System.Drawing.Point(64, 115);
            this.cmbInnerType.Name = "cmbInnerType";
            this.cmbInnerType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbInnerType.Size = new System.Drawing.Size(163, 21);
            this.cmbInnerType.TabIndex = 4;
            // 
            // txtBillTo
            // 
            this.txtBillTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBillTo.FinderName = "CustomerFinder";
            this.txtBillTo.Location = new System.Drawing.Point(64, 3);
            this.txtBillTo.Name = "txtBillTo";
            this.txtBillTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtBillTo.Size = new System.Drawing.Size(163, 21);
            this.txtBillTo.TabIndex = 3;
            // 
            // labGLNo
            // 
            this.labGLNo.Location = new System.Drawing.Point(5, 91);
            this.labGLNo.Name = "labGLNo";
            this.labGLNo.Size = new System.Drawing.Size(48, 14);
            this.labGLNo.TabIndex = 12;
            this.labGLNo.Text = "会计科目";
            // 
            // labVoucherNo
            // 
            this.labVoucherNo.Location = new System.Drawing.Point(5, 64);
            this.labVoucherNo.Name = "labVoucherNo";
            this.labVoucherNo.Size = new System.Drawing.Size(36, 14);
            this.labVoucherNo.TabIndex = 12;
            this.labVoucherNo.Text = "凭证号";
            // 
            // labInnerType
            // 
            this.labInnerType.Location = new System.Drawing.Point(7, 118);
            this.labInnerType.Name = "labInnerType";
            this.labInnerType.Size = new System.Drawing.Size(48, 14);
            this.labInnerType.TabIndex = 12;
            this.labInnerType.Text = "内部往来";
            // 
            // labBillTo
            // 
            this.labBillTo.Location = new System.Drawing.Point(7, 6);
            this.labBillTo.Name = "labBillTo";
            this.labBillTo.Size = new System.Drawing.Size(48, 14);
            this.labBillTo.TabIndex = 12;
            this.labBillTo.Text = "往来单位";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "基本信息";
            this.nbarBase.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 232;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // VoucherInfoSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "VoucherInfoSearchPart";
            this.Size = new System.Drawing.Size(240, 656);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkTotalForGL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReceive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGLNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucherNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbInnerType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillTo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.XtraScrollableControl panel2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarDate;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit txtBillTo;
        private DevExpress.XtraEditors.LabelControl labBillTo;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbInnerType;
        private DevExpress.XtraEditors.LabelControl labInnerType;
        private ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart operationDatePart1;
        private ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl treeBoxSalesDep;
        private DevExpress.XtraEditors.CheckEdit chkTotalForGL;
        private DevExpress.XtraEditors.CheckEdit chkPay;
        private DevExpress.XtraEditors.CheckEdit chkReceive;
        private DevExpress.XtraEditors.TextEdit txtGLNo;
        private DevExpress.XtraEditors.TextEdit txtVoucherNo;
        private DevExpress.XtraEditors.LabelControl labGLNo;
        private DevExpress.XtraEditors.LabelControl labVoucherNo;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.SimpleButton btnCheckFinanceCode;
        private ICP.ReportCenter.UI.Comm.Controls.CheckBoxComboBox chkcmbVoucherType;
        private DevExpress.XtraEditors.LabelControl labVoucherType;
        private DevExpress.XtraEditors.SimpleButton btnCheckLedger;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}
