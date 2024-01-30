namespace LongWin.DataWarehouseReport.WinUI
{
    partial class DirectionForT_Search
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirectionForT_Search));
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            this.ultraExplorerBarContainerControl1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.cmbDateType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ucDateTime = new LongWin.DataWarehouseReport.WinUI.DateTimeCtl();
            this.ultraExplorerBarContainerControl2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.label10 = new System.Windows.Forms.Label();
            this.txSCNO = new System.Windows.Forms.TextBox();
            this.cmbGroupByField = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSelectSales = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbViewMode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAgent = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.labAgent = new System.Windows.Forms.Label();
            this.chbLCL = new System.Windows.Forms.CheckBox();
            this.chbCTM = new System.Windows.Forms.CheckBox();
            this.chbFCL = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.ccmbJobType = new LongWin.DataWarehouseReport.WinUI.CheckComboBox();
            this.txtShipOwner = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.mulShippingLine = new LongWin.DataWarehouseReport.WinUI.SelectShippingLineCtl();
            this.cmbSalesType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ucSelectCompany = new LongWin.DataWarehouseReport.WinUI.SelectJobPlaceCtl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.ultraExplorerBar1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar();
            this.ultraExplorerBarContainerControl1.SuspendLayout();
            this.ultraExplorerBarContainerControl2.SuspendLayout();
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
            this.ucDateTime.BackColor = System.Drawing.SystemColors.Window;
            this.ucDateTime.ConditionType = LongWin.DataWarehouseReport.WinUI.ConditionDateType.Month;
            this.ucDateTime.DateTimeFrom = new System.DateTime(2009, 8, 1, 0, 0, 0, 0);
            this.ucDateTime.DateTimeTo = new System.DateTime(2009, 8, 31, 0, 0, 0, 0);
            this.ucDateTime.DefaultDateTimeType = LongWin.DataWarehouseReport.WinUI.DateTimeCtl.DefaultType.ThisMonth;
            this.ucDateTime.DefaultFrom = new System.DateTime(((long)(0)));
            this.ucDateTime.DefaultTo = new System.DateTime(((long)(0)));
            this.ucDateTime.Name = "ucDateTime";
            // 
            // ultraExplorerBarContainerControl2
            // 
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label10);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txSCNO);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbGroupByField);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label9);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtSelectSales);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label8);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbViewMode);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label5);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtAgent);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.labAgent);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.chbLCL);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.chbCTM);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.chbFCL);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label16);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.ccmbJobType);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtShipOwner);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.mulShippingLine);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbSalesType);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label2);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label7);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label1);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label6);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label3);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.ucSelectCompany);
            resources.ApplyResources(this.ultraExplorerBarContainerControl2, "ultraExplorerBarContainerControl2");
            this.ultraExplorerBarContainerControl2.Name = "ultraExplorerBarContainerControl2";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Name = "label10";
            // 
            // txSCNO
            // 
            resources.ApplyResources(this.txSCNO, "txSCNO");
            this.txSCNO.BackColor = System.Drawing.Color.White;
            this.txSCNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txSCNO.Name = "txSCNO";
            // 
            // cmbGroupByField
            // 
            resources.ApplyResources(this.cmbGroupByField, "cmbGroupByField");
            this.cmbGroupByField.BackColor = System.Drawing.Color.White;
            this.cmbGroupByField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroupByField.FormattingEnabled = true;
            this.cmbGroupByField.Items.AddRange(new object[] {
            resources.GetString("cmbGroupByField.Items"),
            resources.GetString("cmbGroupByField.Items1"),
            resources.GetString("cmbGroupByField.Items2"),
            resources.GetString("cmbGroupByField.Items3"),
            resources.GetString("cmbGroupByField.Items4"),
            resources.GetString("cmbGroupByField.Items5"),
            resources.GetString("cmbGroupByField.Items6")});
            this.cmbGroupByField.Name = "cmbGroupByField";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Name = "label9";
            // 
            // txtSelectSales
            // 
            resources.ApplyResources(this.txtSelectSales, "txtSelectSales");
            this.txtSelectSales.Name = "txtSelectSales";
            this.txtSelectSales.SelectedText = "";
            this.txtSelectSales.SelectedValue = null;
            this.txtSelectSales.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtSelectSales_DoSearching);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Name = "label8";
            // 
            // cmbViewMode
            // 
            resources.ApplyResources(this.cmbViewMode, "cmbViewMode");
            this.cmbViewMode.BackColor = System.Drawing.Color.White;
            this.cmbViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbViewMode.FormattingEnabled = true;
            this.cmbViewMode.Items.AddRange(new object[] {
            resources.GetString("cmbViewMode.Items"),
            resources.GetString("cmbViewMode.Items1")});
            this.cmbViewMode.Name = "cmbViewMode";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            this.label5.Click += new System.EventHandler(this.label5_Click);
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
            // labAgent
            // 
            resources.ApplyResources(this.labAgent, "labAgent");
            this.labAgent.BackColor = System.Drawing.Color.Transparent;
            this.labAgent.Name = "labAgent";
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
            this.chbCTM.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.chbCTM.Name = "chbCTM";
            this.chbCTM.UseVisualStyleBackColor = false;
            this.chbCTM.CheckedChanged += new System.EventHandler(this.chbFCL_CheckedChanged);
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
            // ccmbJobType
            // 
            resources.ApplyResources(this.ccmbJobType, "ccmbJobType");
            this.ccmbJobType.BackColor = System.Drawing.Color.White;
            this.ccmbJobType.Items = new string[] {
        "出口业务",
        "进口业务",
        "集运业务"};
            this.ccmbJobType.MaxChecked = 5;
            this.ccmbJobType.Name = "ccmbJobType";
            // 
            // txtShipOwner
            // 
            resources.ApplyResources(this.txtShipOwner, "txtShipOwner");
            this.txtShipOwner.Name = "txtShipOwner";
            this.txtShipOwner.SelectedText = "";
            this.txtShipOwner.SelectedValue = null;
            this.txtShipOwner.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtShipOwner_DoSearching);
            // 
            // mulShippingLine
            // 
            resources.ApplyResources(this.mulShippingLine, "mulShippingLine");
            this.mulShippingLine.Name = "mulShippingLine";
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
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Name = "label7";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Name = "label6";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // ucSelectCompany
            // 
            resources.ApplyResources(this.ucSelectCompany, "ucSelectCompany");
            this.ucSelectCompany.BackColor = System.Drawing.Color.Transparent;
            this.ucSelectCompany.DataSource = null;
            this.ucSelectCompany.Name = "ucSelectCompany";
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
            this.ultraExplorerBar1.Controls.Add(this.ultraExplorerBarContainerControl1);
            this.ultraExplorerBar1.Controls.Add(this.ultraExplorerBarContainerControl2);
            resources.ApplyResources(this.ultraExplorerBar1, "ultraExplorerBar1");
            ultraExplorerBarGroup1.Container = this.ultraExplorerBarContainerControl1;
            ultraExplorerBarGroup1.Settings.ContainerHeight = 217;
            resources.ApplyResources(ultraExplorerBarGroup1, "ultraExplorerBarGroup1");
            ultraExplorerBarGroup2.Container = this.ultraExplorerBarContainerControl2;
            ultraExplorerBarGroup2.Settings.ContainerHeight = 354;
            resources.ApplyResources(ultraExplorerBarGroup2, "ultraExplorerBarGroup2");
            this.ultraExplorerBar1.Groups.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup[] {
            ultraExplorerBarGroup1,
            ultraExplorerBarGroup2});
            this.ultraExplorerBar1.GroupSettings.Style = Infragistics.Win.UltraWinExplorerBar.GroupStyle.ControlContainer;
            this.ultraExplorerBar1.Name = "ultraExplorerBar1";
            // 
            // DirectionForT_Search
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ultraExplorerBar1);
            this.Controls.Add(this.panel1);
            this.Name = "DirectionForT_Search";
            this.ultraExplorerBarContainerControl1.ResumeLayout(false);
            this.ultraExplorerBarContainerControl1.PerformLayout();
            this.ultraExplorerBarContainerControl2.ResumeLayout(false);
            this.ultraExplorerBarContainerControl2.PerformLayout();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSalesType;
        private System.Windows.Forms.Label label2;
        private SelectJobPlaceCtl ucSelectCompany;
        private SelectShippingLineCtl mulShippingLine;
        private utlMultiSelect txtShipOwner;
        private System.Windows.Forms.ComboBox cmbDateType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chbLCL;
        private System.Windows.Forms.CheckBox chbCTM;
        private System.Windows.Forms.CheckBox chbFCL;
        private System.Windows.Forms.Label label16;
        private CheckComboBox ccmbJobType;
        private System.Windows.Forms.Label labAgent;
        private utlMultiSelect txtAgent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbViewMode;
        private utlMultiSelect txtSelectSales;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbGroupByField;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txSCNO;
    }
}
