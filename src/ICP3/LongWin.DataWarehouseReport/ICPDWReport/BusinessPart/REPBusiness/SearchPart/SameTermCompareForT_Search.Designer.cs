namespace LongWin.DataWarehouseReport.WinUI
{
    partial class SameTermCompareForT_Search
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SameTermCompareForT_Search));
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup3 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup4 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            this.ultraExplorerBarContainerControl1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.ucDateTime = new LongWin.DataWarehouseReport.WinUI.SameTermCompareDateTimeCtl();
            this.cmbDateType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ultraExplorerBarContainerControl2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.label5 = new System.Windows.Forms.Label();
            this.txSCNO = new System.Windows.Forms.TextBox();
            this.txtSelectSales = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbGroupByField = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
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
            this.ultraExplorerBarContainerControl1.AccessibleDescription = null;
            this.ultraExplorerBarContainerControl1.AccessibleName = null;
            resources.ApplyResources(this.ultraExplorerBarContainerControl1, "ultraExplorerBarContainerControl1");
            this.ultraExplorerBarContainerControl1.BackgroundImage = null;
            this.ultraExplorerBarContainerControl1.Controls.Add(this.ucDateTime);
            this.ultraExplorerBarContainerControl1.Controls.Add(this.cmbDateType);
            this.ultraExplorerBarContainerControl1.Controls.Add(this.label4);
            this.ultraExplorerBarContainerControl1.Font = null;
            this.ultraExplorerBarContainerControl1.Name = "ultraExplorerBarContainerControl1";
            // 
            // ucDateTime
            // 
            this.ucDateTime.AccessibleDescription = null;
            this.ucDateTime.AccessibleName = null;
            resources.ApplyResources(this.ucDateTime, "ucDateTime");
            this.ucDateTime.BackColor = System.Drawing.Color.White;
            this.ucDateTime.BackgroundImage = null;
            this.ucDateTime.DateTimeFrom = new System.DateTime(2008, 1, 1, 0, 0, 0, 0);
            this.ucDateTime.DateTimeTo = new System.DateTime(2009, 9, 28, 15, 5, 56, 328);
            this.ucDateTime.DefaultDateTimeType = LongWin.DataWarehouseReport.WinUI.SameTermCompareDateTimeCtl.DefaultType.ThisMonth;
            this.ucDateTime.DefaultFrom = new System.DateTime(((long)(0)));
            this.ucDateTime.DefaultTo = new System.DateTime(((long)(0)));
            this.ucDateTime.Font = null;
            this.ucDateTime.Name = "ucDateTime";
            // 
            // cmbDateType
            // 
            this.cmbDateType.AccessibleDescription = null;
            this.cmbDateType.AccessibleName = null;
            resources.ApplyResources(this.cmbDateType, "cmbDateType");
            this.cmbDateType.BackColor = System.Drawing.Color.White;
            this.cmbDateType.BackgroundImage = null;
            this.cmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateType.Font = null;
            this.cmbDateType.FormattingEnabled = true;
            this.cmbDateType.Items.AddRange(new object[] {
            resources.GetString("cmbDateType.Items"),
            resources.GetString("cmbDateType.Items1")});
            this.cmbDateType.Name = "cmbDateType";
            this.cmbDateType.SelectedIndexChanged += new System.EventHandler(this.cmbDateType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AccessibleDescription = null;
            this.label4.AccessibleName = null;
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = null;
            this.label4.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label4.Name = "label4";
            // 
            // ultraExplorerBarContainerControl2
            // 
            this.ultraExplorerBarContainerControl2.AccessibleDescription = null;
            this.ultraExplorerBarContainerControl2.AccessibleName = null;
            resources.ApplyResources(this.ultraExplorerBarContainerControl2, "ultraExplorerBarContainerControl2");
            this.ultraExplorerBarContainerControl2.BackgroundImage = null;
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label5);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txSCNO);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtSelectSales);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label9);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbGroupByField);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label8);
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
            this.ultraExplorerBarContainerControl2.Font = null;
            this.ultraExplorerBarContainerControl2.Name = "ultraExplorerBarContainerControl2";
            // 
            // label5
            // 
            this.label5.AccessibleDescription = null;
            this.label5.AccessibleName = null;
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = null;
            this.label5.Name = "label5";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // txSCNO
            // 
            this.txSCNO.AccessibleDescription = null;
            this.txSCNO.AccessibleName = null;
            resources.ApplyResources(this.txSCNO, "txSCNO");
            this.txSCNO.BackColor = System.Drawing.Color.White;
            this.txSCNO.BackgroundImage = null;
            this.txSCNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txSCNO.Font = null;
            this.txSCNO.Name = "txSCNO";
            // 
            // txtSelectSales
            // 
            this.txtSelectSales.AccessibleDescription = null;
            this.txtSelectSales.AccessibleName = null;
            resources.ApplyResources(this.txtSelectSales, "txtSelectSales");
            this.txtSelectSales.BackgroundImage = null;
            this.txtSelectSales.Font = null;
            this.txtSelectSales.Name = "txtSelectSales";
            this.txtSelectSales.SelectedText = "";
            this.txtSelectSales.SelectedValue = null;
            this.txtSelectSales.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtSelectSales_DoSearching);
            // 
            // label9
            // 
            this.label9.AccessibleDescription = null;
            this.label9.AccessibleName = null;
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = null;
            this.label9.Name = "label9";
            // 
            // cmbGroupByField
            // 
            this.cmbGroupByField.AccessibleDescription = null;
            this.cmbGroupByField.AccessibleName = null;
            resources.ApplyResources(this.cmbGroupByField, "cmbGroupByField");
            this.cmbGroupByField.BackColor = System.Drawing.Color.White;
            this.cmbGroupByField.BackgroundImage = null;
            this.cmbGroupByField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroupByField.Font = null;
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
            // label8
            // 
            this.label8.AccessibleDescription = null;
            this.label8.AccessibleName = null;
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = null;
            this.label8.Name = "label8";
            // 
            // txtAgent
            // 
            this.txtAgent.AccessibleDescription = null;
            this.txtAgent.AccessibleName = null;
            resources.ApplyResources(this.txtAgent, "txtAgent");
            this.txtAgent.BackColor = System.Drawing.SystemColors.Control;
            this.txtAgent.BackgroundImage = null;
            this.txtAgent.Font = null;
            this.txtAgent.Name = "txtAgent";
            this.txtAgent.SelectedText = "";
            this.txtAgent.SelectedValue = null;
            this.txtAgent.Load += new System.EventHandler(this.txtAgent_Load);
            this.txtAgent.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtAgent_DoSearching);
            // 
            // labAgent
            // 
            this.labAgent.AccessibleDescription = null;
            this.labAgent.AccessibleName = null;
            resources.ApplyResources(this.labAgent, "labAgent");
            this.labAgent.BackColor = System.Drawing.Color.Transparent;
            this.labAgent.Font = null;
            this.labAgent.Name = "labAgent";
            // 
            // chbLCL
            // 
            this.chbLCL.AccessibleDescription = null;
            this.chbLCL.AccessibleName = null;
            resources.ApplyResources(this.chbLCL, "chbLCL");
            this.chbLCL.BackColor = System.Drawing.Color.Transparent;
            this.chbLCL.BackgroundImage = null;
            this.chbLCL.Font = null;
            this.chbLCL.Name = "chbLCL";
            this.chbLCL.UseVisualStyleBackColor = false;
            this.chbLCL.CheckedChanged += new System.EventHandler(this.chbFCL_CheckedChanged);
            // 
            // chbCTM
            // 
            this.chbCTM.AccessibleDescription = null;
            this.chbCTM.AccessibleName = null;
            resources.ApplyResources(this.chbCTM, "chbCTM");
            this.chbCTM.BackColor = System.Drawing.Color.Transparent;
            this.chbCTM.BackgroundImage = null;
            this.chbCTM.Font = null;
            this.chbCTM.Name = "chbCTM";
            this.chbCTM.UseVisualStyleBackColor = false;
            this.chbCTM.CheckedChanged += new System.EventHandler(this.chbFCL_CheckedChanged);
            // 
            // chbFCL
            // 
            this.chbFCL.AccessibleDescription = null;
            this.chbFCL.AccessibleName = null;
            resources.ApplyResources(this.chbFCL, "chbFCL");
            this.chbFCL.BackColor = System.Drawing.Color.Transparent;
            this.chbFCL.BackgroundImage = null;
            this.chbFCL.Font = null;
            this.chbFCL.Name = "chbFCL";
            this.chbFCL.UseVisualStyleBackColor = false;
            this.chbFCL.CheckedChanged += new System.EventHandler(this.chbFCL_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AccessibleDescription = null;
            this.label16.AccessibleName = null;
            resources.ApplyResources(this.label16, "label16");
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = null;
            this.label16.Name = "label16";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // ccmbJobType
            // 
            this.ccmbJobType.AccessibleDescription = null;
            this.ccmbJobType.AccessibleName = null;
            resources.ApplyResources(this.ccmbJobType, "ccmbJobType");
            this.ccmbJobType.BackColor = System.Drawing.Color.White;
            this.ccmbJobType.BackgroundImage = null;
            this.ccmbJobType.Items = new string[] {
        "出口业务",
        "进口业务",
        "其他业务",
        "集运业务"};
            this.ccmbJobType.MaxChecked = 5;
            this.ccmbJobType.Name = "ccmbJobType";
            // 
            // txtShipOwner
            // 
            this.txtShipOwner.AccessibleDescription = null;
            this.txtShipOwner.AccessibleName = null;
            resources.ApplyResources(this.txtShipOwner, "txtShipOwner");
            this.txtShipOwner.BackgroundImage = null;
            this.txtShipOwner.Font = null;
            this.txtShipOwner.Name = "txtShipOwner";
            this.txtShipOwner.SelectedText = "";
            this.txtShipOwner.SelectedValue = null;
            this.txtShipOwner.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtShipOwner_DoSearching);
            // 
            // mulShippingLine
            // 
            this.mulShippingLine.AccessibleDescription = null;
            this.mulShippingLine.AccessibleName = null;
            resources.ApplyResources(this.mulShippingLine, "mulShippingLine");
            this.mulShippingLine.BackgroundImage = null;
            this.mulShippingLine.Font = null;
            this.mulShippingLine.Name = "mulShippingLine";
            // 
            // cmbSalesType
            // 
            this.cmbSalesType.AccessibleDescription = null;
            this.cmbSalesType.AccessibleName = null;
            resources.ApplyResources(this.cmbSalesType, "cmbSalesType");
            this.cmbSalesType.BackColor = System.Drawing.Color.White;
            this.cmbSalesType.BackgroundImage = null;
            this.cmbSalesType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSalesType.Font = null;
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
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = null;
            this.label2.Name = "label2";
            // 
            // label7
            // 
            this.label7.AccessibleDescription = null;
            this.label7.AccessibleName = null;
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Name = "label7";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // label6
            // 
            this.label6.AccessibleDescription = null;
            this.label6.AccessibleName = null;
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = null;
            this.label6.Name = "label6";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = null;
            this.label3.Name = "label3";
            // 
            // ucSelectCompany
            // 
            this.ucSelectCompany.AccessibleDescription = null;
            this.ucSelectCompany.AccessibleName = null;
            resources.ApplyResources(this.ucSelectCompany, "ucSelectCompany");
            this.ucSelectCompany.BackColor = System.Drawing.Color.Transparent;
            this.ucSelectCompany.BackgroundImage = null;
            this.ucSelectCompany.DataSource = null;
            this.ucSelectCompany.Font = null;
            this.ucSelectCompany.Name = "ucSelectCompany";
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = null;
            this.panel1.AccessibleName = null;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackgroundImage = null;
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Font = null;
            this.panel1.Name = "panel1";
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleDescription = null;
            this.btnSearch.AccessibleName = null;
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.BackgroundImage = null;
            this.btnSearch.Font = null;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ultraExplorerBar1
            // 
            this.ultraExplorerBar1.AccessibleDescription = null;
            this.ultraExplorerBar1.AccessibleName = null;
            resources.ApplyResources(this.ultraExplorerBar1, "ultraExplorerBar1");
            this.ultraExplorerBar1.Controls.Add(this.ultraExplorerBarContainerControl1);
            this.ultraExplorerBar1.Controls.Add(this.ultraExplorerBarContainerControl2);
            this.ultraExplorerBar1.Font = null;
            ultraExplorerBarGroup3.Container = this.ultraExplorerBarContainerControl1;
            ultraExplorerBarGroup3.Settings.ContainerHeight = 143;
            resources.ApplyResources(ultraExplorerBarGroup3, "ultraExplorerBarGroup3");
            ultraExplorerBarGroup4.Container = this.ultraExplorerBarContainerControl2;
            ultraExplorerBarGroup4.Settings.ContainerHeight = 318;
            resources.ApplyResources(ultraExplorerBarGroup4, "ultraExplorerBarGroup4");
            this.ultraExplorerBar1.Groups.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup[] {
            ultraExplorerBarGroup3,
            ultraExplorerBarGroup4});
            this.ultraExplorerBar1.GroupSettings.Style = Infragistics.Win.UltraWinExplorerBar.GroupStyle.ControlContainer;
            this.ultraExplorerBar1.Name = "ultraExplorerBar1";
            // 
            // SameTermCompareForT_Search
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = null;
            this.Controls.Add(this.ultraExplorerBar1);
            this.Controls.Add(this.panel1);
            this.Font = null;
            this.Name = "SameTermCompareForT_Search";
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
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl ultraExplorerBarContainerControl2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.ComboBox cmbSalesType;
        private System.Windows.Forms.ComboBox cmbGroupByField;
        private System.Windows.Forms.Label label8;
        private utlMultiSelect txtSelectSales;
        private System.Windows.Forms.Label label9;
        private SameTermCompareDateTimeCtl ucDateTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txSCNO;
    }
}
