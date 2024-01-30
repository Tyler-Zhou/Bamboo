namespace LongWin.DataWarehouseReport.WinUI
{
    partial class TotalForBox_Search
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TotalForBox_Search));
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup3 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            this.ultraExplorerBarContainerControl1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.cmbDateType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ucDateTime = new LongWin.DataWarehouseReport.WinUI.DateTimeCtl();
            this.ultraExplorerBarContainerControl2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.label3 = new System.Windows.Forms.Label();
            this.chbBulk = new System.Windows.Forms.CheckBox();
            this.chbLCL = new System.Windows.Forms.CheckBox();
            this.chbCTM = new System.Windows.Forms.CheckBox();
            this.chbOther = new System.Windows.Forms.CheckBox();
            this.chbAIR = new System.Windows.Forms.CheckBox();
            this.chbFCL = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSelectCustomer = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.txtSelectSales = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.ccmbJobType = new LongWin.DataWarehouseReport.WinUI.CheckComboBox();
            this.cmbGroupBy = new LongWin.DataWarehouseReport.WinUI.CheckComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ucSelectCompany = new LongWin.DataWarehouseReport.WinUI.SelectJobPlaceCtl();
            this.cmbSalesType = new System.Windows.Forms.ComboBox();
            this.cmbIsProfit = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ultraExplorerBarContainerControl3 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtCarrier = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.labShipAgent = new System.Windows.Forms.Label();
            this.txtDiscPort = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.labDiscPort = new System.Windows.Forms.Label();
            this.txtLoadPort = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.labCarrier = new System.Windows.Forms.Label();
            this.mulShippingLine = new LongWin.DataWarehouseReport.WinUI.SelectShippingLineCtl();
            this.txtShipOwner = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.labShippingLine = new System.Windows.Forms.Label();
            this.labLoadPort = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txSCNO = new System.Windows.Forms.TextBox();
            this.labAgent = new System.Windows.Forms.Label();
            this.txtAgent = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDestPort = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.ultraExplorerBar1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar();
            this.ultraExplorerBarContainerControl1.SuspendLayout();
            this.ultraExplorerBarContainerControl2.SuspendLayout();
            this.ultraExplorerBarContainerControl3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraExplorerBar1)).BeginInit();
            this.ultraExplorerBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraExplorerBarContainerControl1
            // 
            this.ultraExplorerBarContainerControl1.Controls.Add(this.cmbDateType);
            this.ultraExplorerBarContainerControl1.Controls.Add(this.label4);
            this.ultraExplorerBarContainerControl1.Controls.Add(this.ucDateTime);
            resources.ApplyResources(this.ultraExplorerBarContainerControl1, "ultraExplorerBarContainerControl1");
            this.ultraExplorerBarContainerControl1.Name = "ultraExplorerBarContainerControl1";
            // 
            // cmbDateType
            // 
            resources.ApplyResources(this.cmbDateType, "cmbDateType");
            this.cmbDateType.BackColor = System.Drawing.Color.White;
            this.cmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateType.FormattingEnabled = true;
            this.cmbDateType.Items.AddRange(new object[] {
            resources.GetString("cmbDateType.Items"),
            resources.GetString("cmbDateType.Items1")});
            this.cmbDateType.Name = "cmbDateType";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label4.Name = "label4";
            // 
            // ucDateTime
            // 
            resources.ApplyResources(this.ucDateTime, "ucDateTime");
            this.ucDateTime.BackColor = System.Drawing.Color.Transparent;
            this.ucDateTime.ConditionType = LongWin.DataWarehouseReport.WinUI.ConditionDateType.Month;
            this.ucDateTime.DateTimeFrom = new System.DateTime(2009, 10, 1, 0, 0, 0, 0);
            this.ucDateTime.DateTimeTo = new System.DateTime(2009, 10, 31, 0, 0, 0, 0);
            this.ucDateTime.DefaultDateTimeType = LongWin.DataWarehouseReport.WinUI.DateTimeCtl.DefaultType.ThisMonth;
            this.ucDateTime.DefaultFrom = new System.DateTime(((long)(0)));
            this.ucDateTime.DefaultTo = new System.DateTime(((long)(0)));
            this.ucDateTime.Name = "ucDateTime";
            // 
            // ultraExplorerBarContainerControl2
            // 
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label3);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.chbBulk);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.chbLCL);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.chbCTM);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.chbOther);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.chbAIR);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.chbFCL);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label16);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtSelectCustomer);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtSelectSales);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.ccmbJobType);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbGroupBy);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label7);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label6);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label2);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.ucSelectCompany);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbSalesType);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbIsProfit);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label9);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label5);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label1);
            resources.ApplyResources(this.ultraExplorerBarContainerControl2, "ultraExplorerBarContainerControl2");
            this.ultraExplorerBarContainerControl2.Name = "ultraExplorerBarContainerControl2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // chbBulk
            // 
            resources.ApplyResources(this.chbBulk, "chbBulk");
            this.chbBulk.BackColor = System.Drawing.Color.Transparent;
            this.chbBulk.Name = "chbBulk";
            this.chbBulk.UseVisualStyleBackColor = false;
            this.chbBulk.CheckedChanged += new System.EventHandler(this.chbFCL_CheckedChanged);
            // 
            // chbLCL
            // 
            resources.ApplyResources(this.chbLCL, "chbLCL");
            this.chbLCL.BackColor = System.Drawing.Color.Transparent;
            this.chbLCL.Name = "chbLCL";
            this.chbLCL.UseVisualStyleBackColor = false;
            this.chbLCL.CheckedChanged += new System.EventHandler(this.chbFCL_CheckedChanged);
            // 
            // chbCTM
            // 
            resources.ApplyResources(this.chbCTM, "chbCTM");
            this.chbCTM.BackColor = System.Drawing.Color.Transparent;
            this.chbCTM.Name = "chbCTM";
            this.chbCTM.UseVisualStyleBackColor = false;
            this.chbCTM.CheckedChanged += new System.EventHandler(this.chbFCL_CheckedChanged);
            // 
            // chbOther
            // 
            resources.ApplyResources(this.chbOther, "chbOther");
            this.chbOther.BackColor = System.Drawing.Color.Transparent;
            this.chbOther.Name = "chbOther";
            this.chbOther.UseVisualStyleBackColor = false;
            this.chbOther.CheckedChanged += new System.EventHandler(this.chbFCL_CheckedChanged);
            // 
            // chbAIR
            // 
            resources.ApplyResources(this.chbAIR, "chbAIR");
            this.chbAIR.BackColor = System.Drawing.Color.Transparent;
            this.chbAIR.Name = "chbAIR";
            this.chbAIR.UseVisualStyleBackColor = false;
            this.chbAIR.CheckedChanged += new System.EventHandler(this.chbFCL_CheckedChanged);
            // 
            // chbFCL
            // 
            resources.ApplyResources(this.chbFCL, "chbFCL");
            this.chbFCL.BackColor = System.Drawing.Color.Transparent;
            this.chbFCL.Name = "chbFCL";
            this.chbFCL.UseVisualStyleBackColor = false;
            this.chbFCL.CheckedChanged += new System.EventHandler(this.chbFCL_CheckedChanged);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Name = "label16";
            // 
            // txtSelectCustomer
            // 
            resources.ApplyResources(this.txtSelectCustomer, "txtSelectCustomer");
            this.txtSelectCustomer.Name = "txtSelectCustomer";
            this.txtSelectCustomer.SelectedText = "";
            this.txtSelectCustomer.SelectedValue = null;
            this.txtSelectCustomer.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtSelectCustomer_DoSearching);
            // 
            // txtSelectSales
            // 
            resources.ApplyResources(this.txtSelectSales, "txtSelectSales");
            this.txtSelectSales.Name = "txtSelectSales";
            this.txtSelectSales.SelectedText = "";
            this.txtSelectSales.SelectedValue = null;
            this.txtSelectSales.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtSelectSales_DoSearching);
            // 
            // ccmbJobType
            // 
            resources.ApplyResources(this.ccmbJobType, "ccmbJobType");
            this.ccmbJobType.BackColor = System.Drawing.Color.White;
            this.ccmbJobType.Items = new string[] {
        "出口业务",
        "进口业务",
        "其他业务",
        "集运业务"};
            this.ccmbJobType.MaxChecked = 5;
            this.ccmbJobType.Name = "ccmbJobType";
            // 
            // cmbGroupBy
            // 
            resources.ApplyResources(this.cmbGroupBy, "cmbGroupBy");
            this.cmbGroupBy.BackColor = System.Drawing.Color.White;
            this.cmbGroupBy.Items = new string[] {
        "业务员",
        "客户",
        "承运人",
        "航线",
        "船公司",
        "业务类型",
        "业务发生地",
        "揽货方式",
        "装货港",
        "合约号"};
            this.cmbGroupBy.MaxChecked = 3;
            this.cmbGroupBy.Name = "cmbGroupBy";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Name = "label6";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // ucSelectCompany
            // 
            resources.ApplyResources(this.ucSelectCompany, "ucSelectCompany");
            this.ucSelectCompany.BackColor = System.Drawing.Color.Transparent;
            this.ucSelectCompany.DataSource = null;
            this.ucSelectCompany.Name = "ucSelectCompany";
            // 
            // cmbSalesType
            // 
            resources.ApplyResources(this.cmbSalesType, "cmbSalesType");
            this.cmbSalesType.BackColor = System.Drawing.Color.White;
            this.cmbSalesType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSalesType.FormattingEnabled = true;
            this.cmbSalesType.Items.AddRange(new object[] {
            resources.GetString("cmbSalesType.Items"),
            resources.GetString("cmbSalesType.Items1"),
            resources.GetString("cmbSalesType.Items2"),
            resources.GetString("cmbSalesType.Items3"),
            resources.GetString("cmbSalesType.Items4")});
            this.cmbSalesType.Name = "cmbSalesType";
            // 
            // cmbIsProfit
            // 
            resources.ApplyResources(this.cmbIsProfit, "cmbIsProfit");
            this.cmbIsProfit.BackColor = System.Drawing.Color.White;
            this.cmbIsProfit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIsProfit.FormattingEnabled = true;
            this.cmbIsProfit.Items.AddRange(new object[] {
            resources.GetString("cmbIsProfit.Items"),
            resources.GetString("cmbIsProfit.Items1"),
            resources.GetString("cmbIsProfit.Items2")});
            this.cmbIsProfit.Name = "cmbIsProfit";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Name = "label9";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // ultraExplorerBarContainerControl3
            // 
            this.ultraExplorerBarContainerControl3.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.ultraExplorerBarContainerControl3, "ultraExplorerBarContainerControl3");
            this.ultraExplorerBarContainerControl3.Name = "ultraExplorerBarContainerControl3";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.Controls.Add(this.txtCarrier, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labShipAgent, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtDiscPort, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.labDiscPort, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtLoadPort, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labCarrier, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.mulShippingLine, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtShipOwner, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labShippingLine, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labLoadPort, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.txSCNO, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.labAgent, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtAgent, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtDestPort, 1, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // txtCarrier
            // 
            resources.ApplyResources(this.txtCarrier, "txtCarrier");
            this.txtCarrier.BackColor = System.Drawing.SystemColors.Control;
            this.txtCarrier.Name = "txtCarrier";
            this.txtCarrier.SelectedText = "";
            this.txtCarrier.SelectedValue = null;
            this.txtCarrier.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtCarrier_DoSearching);
            // 
            // labShipAgent
            // 
            resources.ApplyResources(this.labShipAgent, "labShipAgent");
            this.labShipAgent.BackColor = System.Drawing.Color.Transparent;
            this.labShipAgent.Name = "labShipAgent";
            // 
            // txtDiscPort
            // 
            resources.ApplyResources(this.txtDiscPort, "txtDiscPort");
            this.txtDiscPort.BackColor = System.Drawing.SystemColors.Control;
            this.txtDiscPort.Name = "txtDiscPort";
            this.txtDiscPort.SelectedText = "";
            this.txtDiscPort.SelectedValue = null;
            this.txtDiscPort.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtDiscPort_DoSearching);
            // 
            // labDiscPort
            // 
            resources.ApplyResources(this.labDiscPort, "labDiscPort");
            this.labDiscPort.BackColor = System.Drawing.Color.Transparent;
            this.labDiscPort.Name = "labDiscPort";
            // 
            // txtLoadPort
            // 
            resources.ApplyResources(this.txtLoadPort, "txtLoadPort");
            this.txtLoadPort.BackColor = System.Drawing.SystemColors.Control;
            this.txtLoadPort.Name = "txtLoadPort";
            this.txtLoadPort.SelectedText = "";
            this.txtLoadPort.SelectedValue = null;
            this.txtLoadPort.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtLoadPort_DoSearching);
            // 
            // labCarrier
            // 
            resources.ApplyResources(this.labCarrier, "labCarrier");
            this.labCarrier.BackColor = System.Drawing.Color.Transparent;
            this.labCarrier.Name = "labCarrier";
            // 
            // mulShippingLine
            // 
            resources.ApplyResources(this.mulShippingLine, "mulShippingLine");
            this.mulShippingLine.BackColor = System.Drawing.SystemColors.Control;
            this.mulShippingLine.Name = "mulShippingLine";
            // 
            // txtShipOwner
            // 
            resources.ApplyResources(this.txtShipOwner, "txtShipOwner");
            this.txtShipOwner.BackColor = System.Drawing.SystemColors.Control;
            this.txtShipOwner.Name = "txtShipOwner";
            this.txtShipOwner.SelectedText = "";
            this.txtShipOwner.SelectedValue = null;
            this.txtShipOwner.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtShipOwner_DoSearching);
            // 
            // labShippingLine
            // 
            resources.ApplyResources(this.labShippingLine, "labShippingLine");
            this.labShippingLine.BackColor = System.Drawing.Color.Transparent;
            this.labShippingLine.Name = "labShippingLine";
            // 
            // labLoadPort
            // 
            resources.ApplyResources(this.labLoadPort, "labLoadPort");
            this.labLoadPort.BackColor = System.Drawing.Color.Transparent;
            this.labLoadPort.Name = "labLoadPort";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Name = "label8";
            // 
            // txSCNO
            // 
            resources.ApplyResources(this.txSCNO, "txSCNO");
            this.txSCNO.BackColor = System.Drawing.Color.White;
            this.txSCNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txSCNO.Name = "txSCNO";
            // 
            // labAgent
            // 
            resources.ApplyResources(this.labAgent, "labAgent");
            this.labAgent.BackColor = System.Drawing.Color.Transparent;
            this.labAgent.Name = "labAgent";
            // 
            // txtAgent
            // 
            resources.ApplyResources(this.txtAgent, "txtAgent");
            this.txtAgent.BackColor = System.Drawing.SystemColors.Control;
            this.txtAgent.Name = "txtAgent";
            this.txtAgent.SelectedText = "";
            this.txtAgent.SelectedValue = null;
            this.txtAgent.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtAgent_DoSearching);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Name = "label11";
            // 
            // txtDestPort
            // 
            resources.ApplyResources(this.txtDestPort, "txtDestPort");
            this.txtDestPort.BackColor = System.Drawing.SystemColors.Control;
            this.txtDestPort.Name = "txtDestPort";
            this.txtDestPort.SelectedText = "";
            this.txtDestPort.SelectedValue = null;
            this.txtDestPort.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtDestPort_DoSearching);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ultraExplorerBar1
            // 
            this.ultraExplorerBar1.Controls.Add(this.ultraExplorerBarContainerControl3);
            this.ultraExplorerBar1.Controls.Add(this.ultraExplorerBarContainerControl1);
            this.ultraExplorerBar1.Controls.Add(this.ultraExplorerBarContainerControl2);
            resources.ApplyResources(this.ultraExplorerBar1, "ultraExplorerBar1");
            ultraExplorerBarGroup1.Container = this.ultraExplorerBarContainerControl1;
            ultraExplorerBarGroup1.Settings.ContainerHeight = 167;
            resources.ApplyResources(ultraExplorerBarGroup1, "ultraExplorerBarGroup1");
            ultraExplorerBarGroup2.Container = this.ultraExplorerBarContainerControl2;
            ultraExplorerBarGroup2.Settings.ContainerHeight = 290;
            resources.ApplyResources(ultraExplorerBarGroup2, "ultraExplorerBarGroup2");
            ultraExplorerBarGroup3.Container = this.ultraExplorerBarContainerControl3;
            ultraExplorerBarGroup3.Settings.ContainerHeight = 244;
            resources.ApplyResources(ultraExplorerBarGroup3, "ultraExplorerBarGroup3");
            this.ultraExplorerBar1.Groups.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup[] {
            ultraExplorerBarGroup1,
            ultraExplorerBarGroup2,
            ultraExplorerBarGroup3});
            this.ultraExplorerBar1.GroupSettings.Style = Infragistics.Win.UltraWinExplorerBar.GroupStyle.ControlContainer;
            this.ultraExplorerBar1.Name = "ultraExplorerBar1";
            // 
            // TotalForBox_Search
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.ultraExplorerBar1);
            this.Controls.Add(this.panel1);
            this.Name = "TotalForBox_Search";
            this.ultraExplorerBarContainerControl1.ResumeLayout(false);
            this.ultraExplorerBarContainerControl1.PerformLayout();
            this.ultraExplorerBarContainerControl2.ResumeLayout(false);
            this.ultraExplorerBarContainerControl2.PerformLayout();
            this.ultraExplorerBarContainerControl3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraExplorerBar1)).EndInit();
            this.ultraExplorerBar1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearch;
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar ultraExplorerBar1;
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl ultraExplorerBarContainerControl1;
        private DateTimeCtl ucDateTime;
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl ultraExplorerBarContainerControl2;
        private System.Windows.Forms.ComboBox cmbDateType;
        private System.Windows.Forms.Label label4;
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl ultraExplorerBarContainerControl3;
        private System.Windows.Forms.CheckBox chbBulk;
        private System.Windows.Forms.CheckBox chbLCL;
        private System.Windows.Forms.CheckBox chbCTM;
        private System.Windows.Forms.CheckBox chbOther;
        private System.Windows.Forms.CheckBox chbAIR;
        private System.Windows.Forms.CheckBox chbFCL;
        private System.Windows.Forms.Label label16;
        private utlMultiSelect txtSelectCustomer;
        private utlMultiSelect txtSelectSales;
        private CheckComboBox ccmbJobType;
        private CheckComboBox cmbGroupBy;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private SelectJobPlaceCtl ucSelectCompany;
        private System.Windows.Forms.ComboBox cmbSalesType;
        private System.Windows.Forms.ComboBox cmbIsProfit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private utlMultiSelect txtAgent;
        private System.Windows.Forms.Label labShipAgent;
        private utlMultiSelect txtDiscPort;
        private utlMultiSelect txtCarrier;
        private System.Windows.Forms.Label labDiscPort;
        private utlMultiSelect txtLoadPort;
        private System.Windows.Forms.Label labCarrier;
        private SelectShippingLineCtl mulShippingLine;
        private utlMultiSelect txtShipOwner;
        private System.Windows.Forms.Label labShippingLine;
        private System.Windows.Forms.Label labLoadPort;
        private System.Windows.Forms.Label labAgent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txSCNO;
        private System.Windows.Forms.Label label11;
        private utlMultiSelect txtDestPort;
    }
}
