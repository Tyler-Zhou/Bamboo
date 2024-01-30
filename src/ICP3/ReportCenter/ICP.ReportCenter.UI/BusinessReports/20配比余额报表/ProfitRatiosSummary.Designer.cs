namespace ICP.ReportCenter.UI.BusinessReports
{
    partial class ProfitRatiosSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfitRatiosSummary));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.PanelControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.chkcmbCompany = new ICP.ReportCenter.UI.Comm.Controls.CompanyCheckBoxComboBox();
            this.txtRatioBy = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.labRatioBy = new DevExpress.XtraEditors.LabelControl();
            this.txtCarrier = new DevExpress.XtraEditors.TextEdit();
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.txtcontractNo = new DevExpress.XtraEditors.TextEdit();
            this.labContractNo = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.operationDatePart1 = new ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart();
            this.chkcmbShipLine = new ICP.ReportCenter.UI.Comm.Controls.BusinessShipLineTreeCheckControl();
            this.labShipLine = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRatioBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcontractNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 520);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 59);
            this.panel1.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(78, 16);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
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
            this.panel2.Size = new System.Drawing.Size(232, 520);
            this.panel2.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarDate;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarDate});
            this.navBarControl1.Location = new System.Drawing.Point(2, 2);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(228, 516);
            this.navBarControl1.SkinExplorerBarViewScrollStyle = DevExpress.XtraNavBar.SkinExplorerBarViewScrollStyle.ScrollBar;
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarDate
            // 
            this.nbarDate.Caption = "业务时间";
            this.nbarDate.ControlContainer = this.navBarGroupBase;
            this.nbarDate.Expanded = true;
            this.nbarDate.GroupClientHeight = 424;
            this.nbarDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarDate.Name = "nbarDate";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.chkcmbShipLine);
            this.navBarGroupBase.Controls.Add(this.chkcmbCompany);
            this.navBarGroupBase.Controls.Add(this.txtRatioBy);
            this.navBarGroupBase.Controls.Add(this.labShipLine);
            this.navBarGroupBase.Controls.Add(this.labRatioBy);
            this.navBarGroupBase.Controls.Add(this.txtCarrier);
            this.navBarGroupBase.Controls.Add(this.labCarrier);
            this.navBarGroupBase.Controls.Add(this.txtcontractNo);
            this.navBarGroupBase.Controls.Add(this.labContractNo);
            this.navBarGroupBase.Controls.Add(this.labCompany);
            this.navBarGroupBase.Controls.Add(this.operationDatePart1);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(220, 422);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.Location = new System.Drawing.Point(6, 251);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.NullText = "";
            this.chkcmbCompany.Size = new System.Drawing.Size(211, 21);
            this.chkcmbCompany.SplitText = ",";
            this.chkcmbCompany.TabIndex = 218;
            // 
            // txtRatioBy
            // 
            this.txtRatioBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRatioBy.FinderName = "UserFinder";
            this.txtRatioBy.Location = new System.Drawing.Point(6, 302);
            this.txtRatioBy.Name = "txtRatioBy";
            this.txtRatioBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtRatioBy.Size = new System.Drawing.Size(211, 21);
            this.txtRatioBy.TabIndex = 216;
            // 
            // labRatioBy
            // 
            this.labRatioBy.Location = new System.Drawing.Point(3, 282);
            this.labRatioBy.Name = "labRatioBy";
            this.labRatioBy.Size = new System.Drawing.Size(44, 14);
            this.labRatioBy.TabIndex = 217;
            this.labRatioBy.Text = "Ratio By";
            // 
            // txtCarrier
            // 
            this.txtCarrier.EditValue = "";
            this.txtCarrier.Location = new System.Drawing.Point(6, 155);
            this.txtCarrier.Name = "txtCarrier";
            this.txtCarrier.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCarrier.Size = new System.Drawing.Size(211, 21);
            this.txtCarrier.TabIndex = 214;
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(3, 135);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(69, 14);
            this.labCarrier.TabIndex = 215;
            this.labCarrier.Text = "Carrier Name";
            // 
            // txtcontractNo
            // 
            this.txtcontractNo.EditValue = "";
            this.txtcontractNo.Location = new System.Drawing.Point(6, 202);
            this.txtcontractNo.Name = "txtcontractNo";
            this.txtcontractNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcontractNo.Size = new System.Drawing.Size(211, 21);
            this.txtcontractNo.TabIndex = 214;
            // 
            // labContractNo
            // 
            this.labContractNo.Location = new System.Drawing.Point(3, 182);
            this.labContractNo.Name = "labContractNo";
            this.labContractNo.Size = new System.Drawing.Size(72, 14);
            this.labContractNo.TabIndex = 215;
            this.labContractNo.Text = "Contract NO.";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(2, 231);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 212;
            this.labCompany.Text = "Company";
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
            this.operationDatePart1.Size = new System.Drawing.Size(220, 122);
            this.operationDatePart1.TabIndex = 0;
            this.operationDatePart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("operationDatePart1.UsedMessages")));
            // 
            // chkcmbShipLine
            // 
            this.chkcmbShipLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbShipLine.EditText = "";
            this.chkcmbShipLine.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("chkcmbShipLine.EditValue")));
            this.chkcmbShipLine.Location = new System.Drawing.Point(6, 355);
            this.chkcmbShipLine.Name = "chkcmbShipLine";
            this.chkcmbShipLine.ReadOnly = false;
            this.chkcmbShipLine.Size = new System.Drawing.Size(211, 21);
            this.chkcmbShipLine.SplitString = ",";
            this.chkcmbShipLine.TabIndex = 219;
            // 
            // labShipLine
            // 
            this.labShipLine.Location = new System.Drawing.Point(8, 335);
            this.labShipLine.Name = "labShipLine";
            this.labShipLine.Size = new System.Drawing.Size(45, 14);
            this.labShipLine.TabIndex = 217;
            this.labShipLine.Text = "ShipLine";
            // 
            // ProfitRatiosSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ProfitRatiosSummary";
            this.Size = new System.Drawing.Size(232, 579);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel2)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRatioBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcontractNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panel2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarDate;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart operationDatePart1;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraEditors.TextEdit txtcontractNo;
        private DevExpress.XtraEditors.LabelControl labContractNo;
        private Comm.Controls.MutipleCustomerFinderButtonEdit txtRatioBy;
        private DevExpress.XtraEditors.LabelControl labRatioBy;
        private DevExpress.XtraEditors.TextEdit txtCarrier;
        private DevExpress.XtraEditors.LabelControl labCarrier;
        private Comm.Controls.CompanyCheckBoxComboBox chkcmbCompany;
        private Comm.Controls.BusinessShipLineTreeCheckControl chkcmbShipLine;
        private DevExpress.XtraEditors.LabelControl labShipLine;
    }
}
