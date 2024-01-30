namespace ICP.ReportCenter.UI.FCMReports
{
   partial class JobInfoForCargoTrackingSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobInfoForCargoTrackingSearchPart));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.operationDatePart1 = new ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.seETDToLoadShip = new DevExpress.XtraEditors.SpinEdit();
            this.seETDToETA = new DevExpress.XtraEditors.SpinEdit();
            this.seSODateToETD = new DevExpress.XtraEditors.SpinEdit();
            this.labDay3 = new DevExpress.XtraEditors.LabelControl();
            this.labETDToLoadShip = new DevExpress.XtraEditors.LabelControl();
            this.labDay2 = new DevExpress.XtraEditors.LabelControl();
            this.labETDToETA = new DevExpress.XtraEditors.LabelControl();
            this.labDay1 = new DevExpress.XtraEditors.LabelControl();
            this.labSODateToETD = new DevExpress.XtraEditors.LabelControl();
            this.txtContractNo = new DevExpress.XtraEditors.TextEdit();
            this.labContractNo = new DevExpress.XtraEditors.LabelControl();
            this.treeBoxSalesDep = new ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl();
            this.txtSales = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.txtCarrier = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.labPOL = new DevExpress.XtraEditors.LabelControl();
            this.rdoOperationDepartment = new System.Windows.Forms.RadioButton();
            this.stxtPOL = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.rdoOperationOrgial = new System.Windows.Forms.RadioButton();
            this.stxtPOD = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.chkcmbShipLine = new ICP.ReportCenter.UI.Comm.Controls.ShipLineCheckBoxComboBox();
            this.labShipLine = new DevExpress.XtraEditors.LabelControl();
            this.labSales = new DevExpress.XtraEditors.LabelControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seETDToLoadShip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seETDToETA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seSODateToETD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSales.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOD.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 597);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 59);
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
            this.panel2.Size = new System.Drawing.Size(240, 597);
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
            this.navBarControl1.Size = new System.Drawing.Size(240, 531);
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarDate
            // 
            this.nbarDate.Caption = "业务时间";
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
            this.navBarGroupControlContainer1.Controls.Add(this.seETDToLoadShip);
            this.navBarGroupControlContainer1.Controls.Add(this.seETDToETA);
            this.navBarGroupControlContainer1.Controls.Add(this.seSODateToETD);
            this.navBarGroupControlContainer1.Controls.Add(this.labDay3);
            this.navBarGroupControlContainer1.Controls.Add(this.labETDToLoadShip);
            this.navBarGroupControlContainer1.Controls.Add(this.labDay2);
            this.navBarGroupControlContainer1.Controls.Add(this.labETDToETA);
            this.navBarGroupControlContainer1.Controls.Add(this.labDay1);
            this.navBarGroupControlContainer1.Controls.Add(this.labSODateToETD);
            this.navBarGroupControlContainer1.Controls.Add(this.txtContractNo);
            this.navBarGroupControlContainer1.Controls.Add(this.labContractNo);
            this.navBarGroupControlContainer1.Controls.Add(this.treeBoxSalesDep);
            this.navBarGroupControlContainer1.Controls.Add(this.txtSales);
            this.navBarGroupControlContainer1.Controls.Add(this.txtCarrier);
            this.navBarGroupControlContainer1.Controls.Add(this.labCarrier);
            this.navBarGroupControlContainer1.Controls.Add(this.labPOL);
            this.navBarGroupControlContainer1.Controls.Add(this.rdoOperationDepartment);
            this.navBarGroupControlContainer1.Controls.Add(this.stxtPOL);
            this.navBarGroupControlContainer1.Controls.Add(this.labPOD);
            this.navBarGroupControlContainer1.Controls.Add(this.rdoOperationOrgial);
            this.navBarGroupControlContainer1.Controls.Add(this.stxtPOD);
            this.navBarGroupControlContainer1.Controls.Add(this.chkcmbShipLine);
            this.navBarGroupControlContainer1.Controls.Add(this.labShipLine);
            this.navBarGroupControlContainer1.Controls.Add(this.labSales);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(232, 320);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // seETDToLoadShip
            // 
            this.seETDToLoadShip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.seETDToLoadShip.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seETDToLoadShip.Location = new System.Drawing.Point(109, 291);
            this.seETDToLoadShip.Name = "seETDToLoadShip";
            this.seETDToLoadShip.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seETDToLoadShip.Properties.DisplayFormat.FormatString = "N00";
            this.seETDToLoadShip.Properties.EditFormat.FormatString = "N00";
            this.seETDToLoadShip.Properties.Mask.EditMask = "N00";
            this.seETDToLoadShip.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.seETDToLoadShip.Size = new System.Drawing.Size(93, 21);
            this.seETDToLoadShip.TabIndex = 11;
            // 
            // seETDToETA
            // 
            this.seETDToETA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.seETDToETA.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seETDToETA.Location = new System.Drawing.Point(109, 264);
            this.seETDToETA.Name = "seETDToETA";
            this.seETDToETA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seETDToETA.Properties.DisplayFormat.FormatString = "N00";
            this.seETDToETA.Properties.EditFormat.FormatString = "N00";
            this.seETDToETA.Properties.Mask.EditMask = "N00";
            this.seETDToETA.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.seETDToETA.Size = new System.Drawing.Size(93, 21);
            this.seETDToETA.TabIndex = 10;
            // 
            // seSODateToETD
            // 
            this.seSODateToETD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.seSODateToETD.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seSODateToETD.Location = new System.Drawing.Point(109, 237);
            this.seSODateToETD.Name = "seSODateToETD";
            this.seSODateToETD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seSODateToETD.Properties.DisplayFormat.FormatString = "N00";
            this.seSODateToETD.Properties.EditFormat.FormatString = "N00";
            this.seSODateToETD.Properties.Mask.EditMask = "N00";
            this.seSODateToETD.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.seSODateToETD.Size = new System.Drawing.Size(93, 21);
            this.seSODateToETD.TabIndex = 9;
            // 
            // labDay3
            // 
            this.labDay3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labDay3.Location = new System.Drawing.Point(208, 294);
            this.labDay3.Name = "labDay3";
            this.labDay3.Size = new System.Drawing.Size(12, 14);
            this.labDay3.TabIndex = 212;
            this.labDay3.Text = "天";
            // 
            // labETDToLoadShip
            // 
            this.labETDToLoadShip.Location = new System.Drawing.Point(5, 294);
            this.labETDToLoadShip.Name = "labETDToLoadShip";
            this.labETDToLoadShip.Size = new System.Drawing.Size(83, 14);
            this.labETDToLoadShip.TabIndex = 212;
            this.labETDToLoadShip.Text = "装船和ETD间隔";
            // 
            // labDay2
            // 
            this.labDay2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labDay2.Location = new System.Drawing.Point(208, 267);
            this.labDay2.Name = "labDay2";
            this.labDay2.Size = new System.Drawing.Size(12, 14);
            this.labDay2.TabIndex = 212;
            this.labDay2.Text = "天";
            // 
            // labETDToETA
            // 
            this.labETDToETA.Location = new System.Drawing.Point(5, 267);
            this.labETDToETA.Name = "labETDToETA";
            this.labETDToETA.Size = new System.Drawing.Size(82, 14);
            this.labETDToETA.TabIndex = 212;
            this.labETDToETA.Text = "ETA和ETD间隔";
            // 
            // labDay1
            // 
            this.labDay1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labDay1.Location = new System.Drawing.Point(208, 240);
            this.labDay1.Name = "labDay1";
            this.labDay1.Size = new System.Drawing.Size(12, 14);
            this.labDay1.TabIndex = 212;
            this.labDay1.Text = "天";
            // 
            // labSODateToETD
            // 
            this.labSODateToETD.Location = new System.Drawing.Point(5, 240);
            this.labSODateToETD.Name = "labSODateToETD";
            this.labSODateToETD.Size = new System.Drawing.Size(101, 14);
            this.labSODateToETD.TabIndex = 212;
            this.labSODateToETD.Text = "ETD和SODate间隔";
            // 
            // txtContractNo
            // 
            this.txtContractNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContractNo.Location = new System.Drawing.Point(64, 210);
            this.txtContractNo.Name = "txtContractNo";
            this.txtContractNo.Size = new System.Drawing.Size(162, 21);
            this.txtContractNo.TabIndex = 8;
            // 
            // labContractNo
            // 
            this.labContractNo.Location = new System.Drawing.Point(6, 213);
            this.labContractNo.Name = "labContractNo";
            this.labContractNo.Size = new System.Drawing.Size(36, 14);
            this.labContractNo.TabIndex = 211;
            this.labContractNo.Text = "合约号";
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
            this.treeBoxSalesDep.Size = new System.Drawing.Size(220, 21);
            this.treeBoxSalesDep.SplitString = ",";
            this.treeBoxSalesDep.TabIndex = 2;
            // 
            // txtSales
            // 
            this.txtSales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSales.FinderName = "UserFinder";
            this.txtSales.Location = new System.Drawing.Point(64, 75);
            this.txtSales.Name = "txtSales";
            this.txtSales.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtSales.Size = new System.Drawing.Size(162, 21);
            this.txtSales.TabIndex = 3;
            // 
            // txtCarrier
            // 
            this.txtCarrier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCarrier.FinderName = "CustomerCarrierFinder";
            this.txtCarrier.Location = new System.Drawing.Point(64, 102);
            this.txtCarrier.Name = "txtCarrier";
            this.txtCarrier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCarrier.Size = new System.Drawing.Size(162, 21);
            this.txtCarrier.TabIndex = 4;
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(7, 105);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(36, 14);
            this.labCarrier.TabIndex = 209;
            this.labCarrier.Text = "船公司";
            // 
            // labPOL
            // 
            this.labPOL.Location = new System.Drawing.Point(5, 158);
            this.labPOL.Name = "labPOL";
            this.labPOL.Size = new System.Drawing.Size(36, 14);
            this.labPOL.TabIndex = 202;
            this.labPOL.Text = "装货港";
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
            // stxtPOL
            // 
            this.stxtPOL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.stxtPOL.FinderName = "OceanLocationFinder";
            this.stxtPOL.Location = new System.Drawing.Point(64, 156);
            this.stxtPOL.Name = "stxtPOL";
            this.stxtPOL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPOL.Size = new System.Drawing.Size(162, 21);
            this.stxtPOL.TabIndex = 6;
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(6, 185);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(36, 14);
            this.labPOD.TabIndex = 201;
            this.labPOD.Text = "卸货港";
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
            // stxtPOD
            // 
            this.stxtPOD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.stxtPOD.FinderName = "OceanLocationFinder";
            this.stxtPOD.Location = new System.Drawing.Point(64, 183);
            this.stxtPOD.Name = "stxtPOD";
            this.stxtPOD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPOD.Size = new System.Drawing.Size(162, 21);
            this.stxtPOD.TabIndex = 7;
            // 
            // chkcmbShipLine
            // 
            this.chkcmbShipLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbShipLine.Location = new System.Drawing.Point(64, 129);
            this.chkcmbShipLine.Name = "chkcmbShipLine";
            this.chkcmbShipLine.NullText = "";
            this.chkcmbShipLine.Size = new System.Drawing.Size(162, 21);
            this.chkcmbShipLine.SplitText = ";";
            this.chkcmbShipLine.TabIndex = 5;
            // 
            // labShipLine
            // 
            this.labShipLine.Location = new System.Drawing.Point(6, 131);
            this.labShipLine.Name = "labShipLine";
            this.labShipLine.Size = new System.Drawing.Size(24, 14);
            this.labShipLine.TabIndex = 205;
            this.labShipLine.Text = "航线";
            // 
            // labSales
            // 
            this.labSales.Location = new System.Drawing.Point(7, 78);
            this.labSales.Name = "labSales";
            this.labSales.Size = new System.Drawing.Size(36, 14);
            this.labSales.TabIndex = 12;
            this.labSales.Text = "业务员";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "基本信息";
            this.nbarBase.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 322;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // JobInfoForCargoTrackingSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "JobInfoForCargoTrackingSearchPart";
            this.Size = new System.Drawing.Size(240, 656);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seETDToLoadShip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seETDToETA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seSODateToETD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSales.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOD.Properties)).EndInit();
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
        private System.Windows.Forms.RadioButton rdoOperationDepartment;
        private System.Windows.Forms.RadioButton rdoOperationOrgial;
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit txtSales;
        private DevExpress.XtraEditors.LabelControl labSales;
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit txtCarrier;
        private DevExpress.XtraEditors.LabelControl labCarrier;
        private ICP.ReportCenter.UI.Comm.Controls.ShipLineCheckBoxComboBox chkcmbShipLine;
        private DevExpress.XtraEditors.LabelControl labShipLine;
        private DevExpress.XtraEditors.LabelControl labPOL;
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit stxtPOL;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit stxtPOD;
        private DevExpress.XtraEditors.TextEdit txtContractNo;
        private DevExpress.XtraEditors.LabelControl labContractNo;
        private ICP.ReportCenter.UI.Comm.Controls.OperationDateByMonthPart operationDatePart1;
        private ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl treeBoxSalesDep;
        private DevExpress.XtraEditors.SpinEdit seETDToLoadShip;
        private DevExpress.XtraEditors.SpinEdit seETDToETA;
        private DevExpress.XtraEditors.SpinEdit seSODateToETD;
        private DevExpress.XtraEditors.LabelControl labDay3;
        private DevExpress.XtraEditors.LabelControl labETDToLoadShip;
        private DevExpress.XtraEditors.LabelControl labDay2;
        private DevExpress.XtraEditors.LabelControl labETDToETA;
        private DevExpress.XtraEditors.LabelControl labDay1;
        private DevExpress.XtraEditors.LabelControl labSODateToETD;
    }
}
