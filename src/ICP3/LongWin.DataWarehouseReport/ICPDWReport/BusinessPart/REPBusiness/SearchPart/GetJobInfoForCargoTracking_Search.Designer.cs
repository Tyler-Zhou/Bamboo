namespace LongWin.DataWarehouseReport.WinUI
{
    partial class GetJobInfoForCargoTracking_Search
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetJobInfoForCargoTracking_Search));
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            this.ultraExplorerBarContainerControl1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.cmbDateType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ucDateTime = new LongWin.DataWarehouseReport.WinUI.DateTimeCtl();
            this.ultraExplorerBarContainerControl2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nuLoadingToETD = new System.Windows.Forms.NumericUpDown();
            this.nuETDToETA = new System.Windows.Forms.NumericUpDown();
            this.nuSoDateToETD = new System.Windows.Forms.NumericUpDown();
            this.txtSelectSales = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDiscPort = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.ucSelectCompany = new LongWin.DataWarehouseReport.WinUI.SelectJobPlaceCtl();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labDiscPort = new System.Windows.Forms.Label();
            this.txSCNO = new System.Windows.Forms.TextBox();
            this.txtShipOwner = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.labCarrier = new System.Windows.Forms.Label();
            this.txtLoadPort = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.labLoadPort = new System.Windows.Forms.Label();
            this.mulShippingLine = new LongWin.DataWarehouseReport.WinUI.SelectShippingLineCtl();
            this.labShippingLine = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.ultraExplorerBar1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar();
            this.ultraExplorerBarContainerControl1.SuspendLayout();
            this.ultraExplorerBarContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuLoadingToETD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuETDToETA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuSoDateToETD)).BeginInit();
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
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label9);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label6);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label3);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.nuLoadingToETD);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.nuETDToETA);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.nuSoDateToETD);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtSelectSales);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label2);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtDiscPort);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.ucSelectCompany);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label7);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label5);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label8);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label1);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.labDiscPort);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txSCNO);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtShipOwner);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.labCarrier);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtLoadPort);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.labLoadPort);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.mulShippingLine);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.labShippingLine);
            resources.ApplyResources(this.ultraExplorerBarContainerControl2, "ultraExplorerBarContainerControl2");
            this.ultraExplorerBarContainerControl2.Name = "ultraExplorerBarContainerControl2";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Name = "label9";
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
            // nuLoadingToETD
            // 
            this.nuLoadingToETD.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.nuLoadingToETD, "nuLoadingToETD");
            this.nuLoadingToETD.Name = "nuLoadingToETD";
            // 
            // nuETDToETA
            // 
            this.nuETDToETA.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.nuETDToETA, "nuETDToETA");
            this.nuETDToETA.Name = "nuETDToETA";
            // 
            // nuSoDateToETD
            // 
            this.nuSoDateToETD.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.nuSoDateToETD, "nuSoDateToETD");
            this.nuSoDateToETD.Name = "nuSoDateToETD";
            // 
            // txtSelectSales
            // 
            resources.ApplyResources(this.txtSelectSales, "txtSelectSales");
            this.txtSelectSales.Name = "txtSelectSales";
            this.txtSelectSales.SelectedText = "";
            this.txtSelectSales.SelectedValue = null;
            this.txtSelectSales.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtSelectSales_DoSearching);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
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
            // ucSelectCompany
            // 
            resources.ApplyResources(this.ucSelectCompany, "ucSelectCompany");
            this.ucSelectCompany.BackColor = System.Drawing.Color.Transparent;
            this.ucSelectCompany.DataSource = null;
            this.ucSelectCompany.Name = "ucSelectCompany";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Name = "label7";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Name = "label8";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // labDiscPort
            // 
            resources.ApplyResources(this.labDiscPort, "labDiscPort");
            this.labDiscPort.BackColor = System.Drawing.Color.Transparent;
            this.labDiscPort.Name = "labDiscPort";
            // 
            // txSCNO
            // 
            resources.ApplyResources(this.txSCNO, "txSCNO");
            this.txSCNO.BackColor = System.Drawing.Color.White;
            this.txSCNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txSCNO.Name = "txSCNO";
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
            // labCarrier
            // 
            resources.ApplyResources(this.labCarrier, "labCarrier");
            this.labCarrier.BackColor = System.Drawing.Color.Transparent;
            this.labCarrier.Name = "labCarrier";
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
            // labLoadPort
            // 
            resources.ApplyResources(this.labLoadPort, "labLoadPort");
            this.labLoadPort.BackColor = System.Drawing.Color.Transparent;
            this.labLoadPort.Name = "labLoadPort";
            // 
            // mulShippingLine
            // 
            resources.ApplyResources(this.mulShippingLine, "mulShippingLine");
            this.mulShippingLine.BackColor = System.Drawing.SystemColors.Control;
            this.mulShippingLine.Name = "mulShippingLine";
            // 
            // labShippingLine
            // 
            resources.ApplyResources(this.labShippingLine, "labShippingLine");
            this.labShippingLine.BackColor = System.Drawing.Color.Transparent;
            this.labShippingLine.Name = "labShippingLine";
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
            ultraExplorerBarGroup1.Settings.ContainerHeight = 150;
            resources.ApplyResources(ultraExplorerBarGroup1, "ultraExplorerBarGroup1");
            ultraExplorerBarGroup2.Container = this.ultraExplorerBarContainerControl2;
            ultraExplorerBarGroup2.Settings.ContainerHeight = 316;
            resources.ApplyResources(ultraExplorerBarGroup2, "ultraExplorerBarGroup2");
            this.ultraExplorerBar1.Groups.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup[] {
            ultraExplorerBarGroup1,
            ultraExplorerBarGroup2});
            this.ultraExplorerBar1.GroupSettings.Style = Infragistics.Win.UltraWinExplorerBar.GroupStyle.ControlContainer;
            this.ultraExplorerBar1.Name = "ultraExplorerBar1";
            // 
            // GetJobInfoForCargoTracking_Search
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.ultraExplorerBar1);
            this.Controls.Add(this.panel1);
            this.Name = "GetJobInfoForCargoTracking_Search";
            this.ultraExplorerBarContainerControl1.ResumeLayout(false);
            this.ultraExplorerBarContainerControl1.PerformLayout();
            this.ultraExplorerBarContainerControl2.ResumeLayout(false);
            this.ultraExplorerBarContainerControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuLoadingToETD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuETDToETA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuSoDateToETD)).EndInit();
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
        private utlMultiSelect txtSelectSales;
        private System.Windows.Forms.Label label2;
        private SelectJobPlaceCtl ucSelectCompany;
        private utlMultiSelect txtDiscPort;
        private utlMultiSelect txtLoadPort;
        private SelectShippingLineCtl mulShippingLine;
        private utlMultiSelect txtShipOwner;
        private System.Windows.Forms.TextBox txSCNO;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nuLoadingToETD;
        private System.Windows.Forms.NumericUpDown nuETDToETA;
        private System.Windows.Forms.NumericUpDown nuSoDateToETD;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labDiscPort;
        private System.Windows.Forms.Label labCarrier;
        private System.Windows.Forms.Label labLoadPort;
        private System.Windows.Forms.Label labShippingLine;
    }
}
