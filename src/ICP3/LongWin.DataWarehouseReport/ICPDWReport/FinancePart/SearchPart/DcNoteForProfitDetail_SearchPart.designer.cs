namespace LongWin.DataWarehouseReport.WinUI
{
    partial class DcNoteForProfitDetailSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DcNoteForProfitDetailSearchPart));
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup4 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup5 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup6 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            this.ultraExplorerBarContainerControl1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.ucDateTime = new LongWin.DataWarehouseReport.WinUI.DateTimeCtl();
            this.ultraExplorerBarContainerControl2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.cmbChangeData = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ucSelectChargeCode = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.txtShipTo = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ultraExplorerBarContainerControl3 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.txtSales = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.mulBusiness = new LongWin.DataWarehouseReport.WinUI.SelectBussinessType();
            this.ucSelectCompany = new LongWin.DataWarehouseReport.WinUI.SelectJobPlaceCtl();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.ultraExplorerBar1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar();
            this.ultraExplorerBarContainerControl1.SuspendLayout();
            this.ultraExplorerBarContainerControl2.SuspendLayout();
            this.ultraExplorerBarContainerControl3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraExplorerBar1)).BeginInit();
            this.ultraExplorerBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraExplorerBarContainerControl1
            // 
            this.ultraExplorerBarContainerControl1.Controls.Add(this.ucDateTime);
            resources.ApplyResources(this.ultraExplorerBarContainerControl1, "ultraExplorerBarContainerControl1");
            this.ultraExplorerBarContainerControl1.Name = "ultraExplorerBarContainerControl1";
            // 
            // ucDateTime
            // 
            this.ucDateTime.BackColor = System.Drawing.SystemColors.Window;
            this.ucDateTime.ConditionType = LongWin.DataWarehouseReport.WinUI.ConditionDateType.Month;
            this.ucDateTime.DateTimeFrom = new System.DateTime(2009, 8, 1, 0, 0, 0, 0);
            this.ucDateTime.DateTimeTo = new System.DateTime(2009, 8, 31, 0, 0, 0, 0);
            this.ucDateTime.DefaultDateTimeType = LongWin.DataWarehouseReport.WinUI.DateTimeCtl.DefaultType.ThisMonth;
            this.ucDateTime.DefaultFrom = new System.DateTime(((long)(0)));
            this.ucDateTime.DefaultTo = new System.DateTime(((long)(0)));
            resources.ApplyResources(this.ucDateTime, "ucDateTime");
            this.ucDateTime.Name = "ucDateTime";
            // 
            // ultraExplorerBarContainerControl2
            // 
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbChangeData);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label10);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.ucSelectChargeCode);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtShipTo);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label8);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label7);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label6);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label3);
            resources.ApplyResources(this.ultraExplorerBarContainerControl2, "ultraExplorerBarContainerControl2");
            this.ultraExplorerBarContainerControl2.Name = "ultraExplorerBarContainerControl2";
            this.ultraExplorerBarContainerControl2.Paint += new System.Windows.Forms.PaintEventHandler(this.ultraExplorerBarContainerControl2_Paint);
            // 
            // cmbChangeData
            // 
            resources.ApplyResources(this.cmbChangeData, "cmbChangeData");
            this.cmbChangeData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChangeData.FormattingEnabled = true;
            this.cmbChangeData.Items.AddRange(new object[] {
            resources.GetString("cmbChangeData.Items"),
            resources.GetString("cmbChangeData.Items1"),
            resources.GetString("cmbChangeData.Items2")});
            this.cmbChangeData.Name = "cmbChangeData";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.BackColor = System.Drawing.SystemColors.Window;
            this.label10.Name = "label10";
            // 
            // ucSelectChargeCode
            // 
            resources.ApplyResources(this.ucSelectChargeCode, "ucSelectChargeCode");
            this.ucSelectChargeCode.Name = "ucSelectChargeCode";
            this.ucSelectChargeCode.SelectedText = "";
            this.ucSelectChargeCode.SelectedValue = null;
            this.ucSelectChargeCode.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.ucSelectChargeCode_DoSearching);
            // 
            // txtShipTo
            // 
            resources.ApplyResources(this.txtShipTo, "txtShipTo");
            this.txtShipTo.Name = "txtShipTo";
            this.txtShipTo.SelectedText = "";
            this.txtShipTo.SelectedValue = null;
            this.txtShipTo.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtShipTo_DoSearching);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.SystemColors.Window;
            this.label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.SystemColors.Window;
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.SystemColors.Window;
            this.label6.Name = "label6";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.SystemColors.Window;
            this.label3.Name = "label3";
            // 
            // ultraExplorerBarContainerControl3
            // 
            this.ultraExplorerBarContainerControl3.Controls.Add(this.txtSales);
            this.ultraExplorerBarContainerControl3.Controls.Add(this.mulBusiness);
            this.ultraExplorerBarContainerControl3.Controls.Add(this.ucSelectCompany);
            this.ultraExplorerBarContainerControl3.Controls.Add(this.label4);
            this.ultraExplorerBarContainerControl3.Controls.Add(this.label2);
            resources.ApplyResources(this.ultraExplorerBarContainerControl3, "ultraExplorerBarContainerControl3");
            this.ultraExplorerBarContainerControl3.Name = "ultraExplorerBarContainerControl3";
            // 
            // txtSales
            // 
            resources.ApplyResources(this.txtSales, "txtSales");
            this.txtSales.Name = "txtSales";
            this.txtSales.SelectedText = "";
            this.txtSales.SelectedValue = null;
            this.txtSales.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtSales_DoSearching);
            // 
            // mulBusiness
            // 
            resources.ApplyResources(this.mulBusiness, "mulBusiness");
            this.mulBusiness.DataFilter = "";
            this.mulBusiness.Name = "mulBusiness";
            // 
            // ucSelectCompany
            // 
            resources.ApplyResources(this.ucSelectCompany, "ucSelectCompany");
            this.ucSelectCompany.BackColor = System.Drawing.Color.Transparent;
            this.ucSelectCompany.DataSource = null;
            this.ucSelectCompany.Name = "ucSelectCompany";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
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
            this.ultraExplorerBar1.Controls.Add(this.ultraExplorerBarContainerControl3);
            resources.ApplyResources(this.ultraExplorerBar1, "ultraExplorerBar1");
            ultraExplorerBarGroup4.Container = this.ultraExplorerBarContainerControl1;
            ultraExplorerBarGroup4.Settings.ContainerHeight = 133;
            ultraExplorerBarGroup5.Container = this.ultraExplorerBarContainerControl2;
            ultraExplorerBarGroup5.Settings.ContainerHeight = 94;
            ultraExplorerBarGroup6.Container = this.ultraExplorerBarContainerControl3;
            ultraExplorerBarGroup6.Settings.ContainerHeight = 127;
            this.ultraExplorerBar1.Groups.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup[] {
            ultraExplorerBarGroup4,
            ultraExplorerBarGroup5,
            ultraExplorerBarGroup6});
            this.ultraExplorerBar1.GroupSettings.Style = Infragistics.Win.UltraWinExplorerBar.GroupStyle.ControlContainer;
            this.ultraExplorerBar1.Name = "ultraExplorerBar1";
            // 
            // DcNoteForProfitDetailSearchPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.ultraExplorerBar1);
            this.Controls.Add(this.panel1);
            this.Name = "DcNoteForProfitDetailSearchPart";
            this.ultraExplorerBarContainerControl1.ResumeLayout(false);
            this.ultraExplorerBarContainerControl2.ResumeLayout(false);
            this.ultraExplorerBarContainerControl2.PerformLayout();
            this.ultraExplorerBarContainerControl3.ResumeLayout(false);
            this.ultraExplorerBarContainerControl3.PerformLayout();
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl ultraExplorerBarContainerControl3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private SelectJobPlaceCtl ucSelectCompany;
        private SelectBussinessType mulBusiness;
        private utlMultiSelect txtShipTo;
        private utlMultiSelect txtSales;
        private utlMultiSelect ucSelectChargeCode;
        private System.Windows.Forms.ComboBox cmbChangeData;
        private System.Windows.Forms.Label label10;
    }
}
