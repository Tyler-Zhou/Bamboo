namespace ICP.ReportCenter.UI.FinanceOEReports
{
   partial class DcNoteCRSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DcNoteCRSearchPart));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.operationDatePart1 = new ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.rdoTotal = new System.Windows.Forms.RadioButton();
            this.rdoDetail = new System.Windows.Forms.RadioButton();
            this.cmbWriteOff = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.txtBillTo = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.txtSales = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.chkcmbGroupBy = new ICP.ReportCenter.UI.Comm.Controls.CheckBoxComboBox();
            this.labWriteOff = new DevExpress.XtraEditors.LabelControl();
            this.labBillTo = new DevExpress.XtraEditors.LabelControl();
            this.labSales = new DevExpress.XtraEditors.LabelControl();
            this.labGroupBy = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.labFeeType = new DevExpress.XtraEditors.LabelControl();
            this.txtCarrier = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.txtAgentOfCarrier = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.labAgentOfCarrier = new DevExpress.XtraEditors.LabelControl();
            this.labPOL = new DevExpress.XtraEditors.LabelControl();
            this.stxtPOL = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.stxtPOD = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.treeBoxSalesDep = new ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl();
            this.reportOperationTypePart1 = new ICP.ReportCenter.UI.Comm.Controls.ReportOperationTypePart();
            this.chkcmbShipLine = new ICP.ReportCenter.UI.Comm.Controls.ShipLineCheckBoxComboBox();
            this.labShipLine = new DevExpress.XtraEditors.LabelControl();
            this.labOperation = new DevExpress.XtraEditors.LabelControl();
            this.rdoOperationOrgial = new System.Windows.Forms.RadioButton();
            this.rdoOperationDepartment = new System.Windows.Forms.RadioButton();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navOther = new DevExpress.XtraNavBar.NavBarGroup();
            this.txtChargingCode = new DevExpress.XtraEditors.ButtonEdit();
            this.txtContractNo = new DevExpress.XtraEditors.TextEdit();
            this.labContractNo = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWriteOff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSales.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgentOfCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChargingCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 603);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 53);
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
            this.panel2.Size = new System.Drawing.Size(240, 603);
            this.panel2.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarDate;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarDate,
            this.nbarBase,
            this.navOther});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(223, 671);
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
            this.navBarGroupBase.Size = new System.Drawing.Size(215, 126);
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
            this.operationDatePart1.Size = new System.Drawing.Size(215, 122);
            this.operationDatePart1.TabIndex = 0;
            this.operationDatePart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("operationDatePart1.UsedMessages")));
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.rdoTotal);
            this.navBarGroupControlContainer1.Controls.Add(this.rdoDetail);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbWriteOff);
            this.navBarGroupControlContainer1.Controls.Add(this.txtBillTo);
            this.navBarGroupControlContainer1.Controls.Add(this.txtSales);
            this.navBarGroupControlContainer1.Controls.Add(this.chkcmbGroupBy);
            this.navBarGroupControlContainer1.Controls.Add(this.labWriteOff);
            this.navBarGroupControlContainer1.Controls.Add(this.labBillTo);
            this.navBarGroupControlContainer1.Controls.Add(this.labSales);
            this.navBarGroupControlContainer1.Controls.Add(this.labGroupBy);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(215, 136);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // rdoTotal
            // 
            this.rdoTotal.AutoSize = true;
            this.rdoTotal.Checked = true;
            this.rdoTotal.Location = new System.Drawing.Point(9, 3);
            this.rdoTotal.Name = "rdoTotal";
            this.rdoTotal.Size = new System.Drawing.Size(61, 18);
            this.rdoTotal.TabIndex = 0;
            this.rdoTotal.TabStop = true;
            this.rdoTotal.Text = "统计表";
            this.rdoTotal.UseVisualStyleBackColor = true;
            // 
            // rdoDetail
            // 
            this.rdoDetail.AutoSize = true;
            this.rdoDetail.Location = new System.Drawing.Point(80, 3);
            this.rdoDetail.Name = "rdoDetail";
            this.rdoDetail.Size = new System.Drawing.Size(61, 18);
            this.rdoDetail.TabIndex = 1;
            this.rdoDetail.Text = "详细表";
            this.rdoDetail.UseVisualStyleBackColor = true;
            // 
            // cmbWriteOff
            // 
            this.cmbWriteOff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWriteOff.Location = new System.Drawing.Point(64, 81);
            this.cmbWriteOff.Name = "cmbWriteOff";
            this.cmbWriteOff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWriteOff.Size = new System.Drawing.Size(145, 21);
            this.cmbWriteOff.TabIndex = 4;
            // 
            // txtBillTo
            // 
            this.txtBillTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBillTo.FinderName = "CustomerFinder";
            this.txtBillTo.Location = new System.Drawing.Point(64, 54);
            this.txtBillTo.Name = "txtBillTo";
            this.txtBillTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtBillTo.Size = new System.Drawing.Size(145, 21);
            this.txtBillTo.TabIndex = 3;
            // 
            // txtSales
            // 
            this.txtSales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSales.FinderName = "UserFinder";
            this.txtSales.Location = new System.Drawing.Point(64, 27);
            this.txtSales.Name = "txtSales";
            this.txtSales.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtSales.Size = new System.Drawing.Size(145, 21);
            this.txtSales.TabIndex = 2;
            // 
            // chkcmbGroupBy
            // 
            this.chkcmbGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbGroupBy.Location = new System.Drawing.Point(64, 108);
            this.chkcmbGroupBy.Name = "chkcmbGroupBy";
            this.chkcmbGroupBy.NullText = "";
            this.chkcmbGroupBy.Size = new System.Drawing.Size(146, 21);
            this.chkcmbGroupBy.SplitText = ",";
            this.chkcmbGroupBy.TabIndex = 5;
            // 
            // labWriteOff
            // 
            this.labWriteOff.Location = new System.Drawing.Point(7, 84);
            this.labWriteOff.Name = "labWriteOff";
            this.labWriteOff.Size = new System.Drawing.Size(48, 14);
            this.labWriteOff.TabIndex = 12;
            this.labWriteOff.Text = "是否核销";
            // 
            // labBillTo
            // 
            this.labBillTo.Location = new System.Drawing.Point(7, 57);
            this.labBillTo.Name = "labBillTo";
            this.labBillTo.Size = new System.Drawing.Size(48, 14);
            this.labBillTo.TabIndex = 12;
            this.labBillTo.Text = "往来单位";
            // 
            // labSales
            // 
            this.labSales.Location = new System.Drawing.Point(7, 30);
            this.labSales.Name = "labSales";
            this.labSales.Size = new System.Drawing.Size(36, 14);
            this.labSales.TabIndex = 12;
            this.labSales.Text = "业务员";
            // 
            // labGroupBy
            // 
            this.labGroupBy.Location = new System.Drawing.Point(7, 109);
            this.labGroupBy.Name = "labGroupBy";
            this.labGroupBy.Size = new System.Drawing.Size(48, 14);
            this.labGroupBy.TabIndex = 12;
            this.labGroupBy.Text = "分组方式";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.txtContractNo);
            this.navBarGroupControlContainer2.Controls.Add(this.labContractNo);
            this.navBarGroupControlContainer2.Controls.Add(this.txtChargingCode);
            this.navBarGroupControlContainer2.Controls.Add(this.labFeeType);
            this.navBarGroupControlContainer2.Controls.Add(this.txtCarrier);
            this.navBarGroupControlContainer2.Controls.Add(this.labCarrier);
            this.navBarGroupControlContainer2.Controls.Add(this.txtAgentOfCarrier);
            this.navBarGroupControlContainer2.Controls.Add(this.labAgentOfCarrier);
            this.navBarGroupControlContainer2.Controls.Add(this.labPOL);
            this.navBarGroupControlContainer2.Controls.Add(this.stxtPOL);
            this.navBarGroupControlContainer2.Controls.Add(this.labPOD);
            this.navBarGroupControlContainer2.Controls.Add(this.stxtPOD);
            this.navBarGroupControlContainer2.Controls.Add(this.treeBoxSalesDep);
            this.navBarGroupControlContainer2.Controls.Add(this.reportOperationTypePart1);
            this.navBarGroupControlContainer2.Controls.Add(this.chkcmbShipLine);
            this.navBarGroupControlContainer2.Controls.Add(this.labShipLine);
            this.navBarGroupControlContainer2.Controls.Add(this.labOperation);
            this.navBarGroupControlContainer2.Controls.Add(this.rdoOperationOrgial);
            this.navBarGroupControlContainer2.Controls.Add(this.rdoOperationDepartment);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(215, 316);
            this.navBarGroupControlContainer2.TabIndex = 2;
            // 
            // labFeeType
            // 
            this.labFeeType.Location = new System.Drawing.Point(6, 102);
            this.labFeeType.Name = "labFeeType";
            this.labFeeType.Size = new System.Drawing.Size(48, 14);
            this.labFeeType.TabIndex = 221;
            this.labFeeType.Text = "费用名称";
            // 
            // txtCarrier
            // 
            this.txtCarrier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCarrier.FinderName = "CustomerCarrierFinder";
            this.txtCarrier.Location = new System.Drawing.Point(64, 177);
            this.txtCarrier.Name = "txtCarrier";
            this.txtCarrier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCarrier.Size = new System.Drawing.Size(146, 21);
            this.txtCarrier.TabIndex = 211;
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(7, 180);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(36, 14);
            this.labCarrier.TabIndex = 219;
            this.labCarrier.Text = "船公司";
            // 
            // txtAgentOfCarrier
            // 
            this.txtAgentOfCarrier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAgentOfCarrier.FinderName = "CustomerAgentOfCarrierFinder";
            this.txtAgentOfCarrier.Location = new System.Drawing.Point(64, 151);
            this.txtAgentOfCarrier.Name = "txtAgentOfCarrier";
            this.txtAgentOfCarrier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtAgentOfCarrier.Size = new System.Drawing.Size(146, 21);
            this.txtAgentOfCarrier.TabIndex = 210;
            // 
            // labAgentOfCarrier
            // 
            this.labAgentOfCarrier.Location = new System.Drawing.Point(7, 154);
            this.labAgentOfCarrier.Name = "labAgentOfCarrier";
            this.labAgentOfCarrier.Size = new System.Drawing.Size(36, 14);
            this.labAgentOfCarrier.TabIndex = 218;
            this.labAgentOfCarrier.Text = "承运人";
            // 
            // labPOL
            // 
            this.labPOL.Location = new System.Drawing.Point(7, 206);
            this.labPOL.Name = "labPOL";
            this.labPOL.Size = new System.Drawing.Size(36, 14);
            this.labPOL.TabIndex = 216;
            this.labPOL.Text = "装货港";
            // 
            // stxtPOL
            // 
            this.stxtPOL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stxtPOL.FinderName = "OceanLocationFinder";
            this.stxtPOL.Location = new System.Drawing.Point(64, 203);
            this.stxtPOL.Name = "stxtPOL";
            this.stxtPOL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPOL.Size = new System.Drawing.Size(146, 21);
            this.stxtPOL.TabIndex = 213;
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(7, 232);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(36, 14);
            this.labPOD.TabIndex = 215;
            this.labPOD.Text = "卸货港";
            // 
            // stxtPOD
            // 
            this.stxtPOD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stxtPOD.FinderName = "OceanLocationFinder";
            this.stxtPOD.Location = new System.Drawing.Point(64, 229);
            this.stxtPOD.Name = "stxtPOD";
            this.stxtPOD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPOD.Size = new System.Drawing.Size(146, 21);
            this.stxtPOD.TabIndex = 214;
            // 
            // treeBoxSalesDep
            // 
            this.treeBoxSalesDep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeBoxSalesDep.EditText = "";
            this.treeBoxSalesDep.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("treeBoxSalesDep.EditValue")));
            this.treeBoxSalesDep.Location = new System.Drawing.Point(7, 48);
            this.treeBoxSalesDep.Name = "treeBoxSalesDep";
            this.treeBoxSalesDep.ReadOnly = false;
            this.treeBoxSalesDep.ShowDepartment = false;
            this.treeBoxSalesDep.Size = new System.Drawing.Size(203, 21);
            this.treeBoxSalesDep.SplitString = ",";
            this.treeBoxSalesDep.TabIndex = 2;
            // 
            // reportOperationTypePart1
            // 
            this.reportOperationTypePart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportOperationTypePart1.BaseMultiLanguageList = null;
            this.reportOperationTypePart1.BasePartList = null;
            this.reportOperationTypePart1.CodeValuePairs = null;
            this.reportOperationTypePart1.ControlsList = null;
            this.reportOperationTypePart1.FormName = "ReportOperationTypePart";
            this.reportOperationTypePart1.IsMultiLanguage = true;
            this.reportOperationTypePart1.Location = new System.Drawing.Point(64, 75);
            this.reportOperationTypePart1.Name = "reportOperationTypePart1";
            this.reportOperationTypePart1.Resources = null;
            this.reportOperationTypePart1.Size = new System.Drawing.Size(146, 21);
            this.reportOperationTypePart1.SplitString = ",";
            this.reportOperationTypePart1.TabIndex = 3;
            this.reportOperationTypePart1.UsedMessages = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("reportOperationTypePart1.UsedMessages")));
            // 
            // chkcmbShipLine
            // 
            this.chkcmbShipLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbShipLine.Location = new System.Drawing.Point(64, 125);
            this.chkcmbShipLine.Name = "chkcmbShipLine";
            this.chkcmbShipLine.NullText = "";
            this.chkcmbShipLine.Size = new System.Drawing.Size(146, 21);
            this.chkcmbShipLine.SplitText = ";";
            this.chkcmbShipLine.TabIndex = 4;
            // 
            // labShipLine
            // 
            this.labShipLine.Location = new System.Drawing.Point(7, 128);
            this.labShipLine.Name = "labShipLine";
            this.labShipLine.Size = new System.Drawing.Size(24, 14);
            this.labShipLine.TabIndex = 205;
            this.labShipLine.Text = "航线";
            // 
            // labOperation
            // 
            this.labOperation.Location = new System.Drawing.Point(7, 78);
            this.labOperation.Name = "labOperation";
            this.labOperation.Size = new System.Drawing.Size(48, 14);
            this.labOperation.TabIndex = 14;
            this.labOperation.Text = "业务类型";
            // 
            // rdoOperationOrgial
            // 
            this.rdoOperationOrgial.AutoSize = true;
            this.rdoOperationOrgial.Checked = true;
            this.rdoOperationOrgial.Location = new System.Drawing.Point(9, 3);
            this.rdoOperationOrgial.Name = "rdoOperationOrgial";
            this.rdoOperationOrgial.Size = new System.Drawing.Size(85, 18);
            this.rdoOperationOrgial.TabIndex = 0;
            this.rdoOperationOrgial.TabStop = true;
            this.rdoOperationOrgial.Text = "业务发生地";
            this.rdoOperationOrgial.UseVisualStyleBackColor = true;
            // 
            // rdoOperationDepartment
            // 
            this.rdoOperationDepartment.AutoSize = true;
            this.rdoOperationDepartment.Location = new System.Drawing.Point(9, 27);
            this.rdoOperationDepartment.Name = "rdoOperationDepartment";
            this.rdoOperationDepartment.Size = new System.Drawing.Size(97, 18);
            this.rdoOperationDepartment.TabIndex = 1;
            this.rdoOperationDepartment.Text = "业务所属部门";
            this.rdoOperationDepartment.UseVisualStyleBackColor = true;
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "基本信息";
            this.nbarBase.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 138;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navOther
            // 
            this.navOther.Caption = "更多";
            this.navOther.ControlContainer = this.navBarGroupControlContainer2;
            this.navOther.Expanded = true;
            this.navOther.GroupClientHeight = 318;
            this.navOther.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navOther.Name = "navOther";
            // 
            // txtChargingCode
            // 
            this.txtChargingCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChargingCode.Location = new System.Drawing.Point(64, 99);
            this.txtChargingCode.Name = "txtChargingCode";
            this.txtChargingCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtChargingCode.Size = new System.Drawing.Size(146, 21);
            this.txtChargingCode.TabIndex = 223;
            // 
            // txtContractNo
            // 
            this.txtContractNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContractNo.Location = new System.Drawing.Point(64, 256);
            this.txtContractNo.Name = "txtContractNo";
            this.txtContractNo.Size = new System.Drawing.Size(146, 21);
            this.txtContractNo.TabIndex = 212;
            // 
            // labContractNo
            // 
            this.labContractNo.Location = new System.Drawing.Point(6, 259);
            this.labContractNo.Name = "labContractNo";
            this.labContractNo.Size = new System.Drawing.Size(36, 14);
            this.labContractNo.TabIndex = 213;
            this.labContractNo.Text = "合约号";
            // 
            // DcNoteCRSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DcNoteCRSearchPart";
            this.Size = new System.Drawing.Size(240, 656);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWriteOff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSales.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgentOfCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChargingCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.XtraScrollableControl panel2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarDate;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraEditors.LabelControl labGroupBy;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraNavBar.NavBarGroup navOther;
        private DevExpress.XtraEditors.LabelControl labOperation;
        private Comm.Controls.CheckBoxComboBox chkcmbGroupBy;
        private System.Windows.Forms.RadioButton rdoOperationDepartment;
        private System.Windows.Forms.RadioButton rdoOperationOrgial;
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit txtBillTo;
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit txtSales;
        private DevExpress.XtraEditors.LabelControl labBillTo;
        private DevExpress.XtraEditors.LabelControl labSales;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbWriteOff;
        private DevExpress.XtraEditors.LabelControl labWriteOff;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private ICP.ReportCenter.UI.Comm.Controls.ShipLineCheckBoxComboBox chkcmbShipLine;
        private DevExpress.XtraEditors.LabelControl labShipLine;
        private ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart operationDatePart1;
        private ICP.ReportCenter.UI.Comm.Controls.ReportOperationTypePart reportOperationTypePart1;
        private ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl treeBoxSalesDep;
        private System.Windows.Forms.RadioButton rdoTotal;
        private System.Windows.Forms.RadioButton rdoDetail;
        private DevExpress.XtraEditors.LabelControl labFeeType;
        private Comm.Controls.MutipleCustomerFinderButtonEdit txtCarrier;
        private DevExpress.XtraEditors.LabelControl labCarrier;
        private Comm.Controls.MutipleCustomerFinderButtonEdit txtAgentOfCarrier;
        private DevExpress.XtraEditors.LabelControl labAgentOfCarrier;
        private DevExpress.XtraEditors.LabelControl labPOL;
        private Comm.Controls.MutipleCustomerFinderButtonEdit stxtPOL;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private Comm.Controls.MutipleCustomerFinderButtonEdit stxtPOD;
        private DevExpress.XtraEditors.ButtonEdit txtChargingCode;
        private DevExpress.XtraEditors.TextEdit txtContractNo;
        private DevExpress.XtraEditors.LabelControl labContractNo;
    }
}
