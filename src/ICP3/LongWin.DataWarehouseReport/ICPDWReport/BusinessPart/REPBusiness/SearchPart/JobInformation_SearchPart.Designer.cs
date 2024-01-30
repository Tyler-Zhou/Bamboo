namespace LongWin.DataWarehouseReport.WinUI
{
    partial class JobInformation_SearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobInformation_SearchPart));
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup3 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            this.ultraExplorerBarContainerControl1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.cmbDateType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ucDateTime = new LongWin.DataWarehouseReport.WinUI.DateTimeCtl();
            this.ultraExplorerBarContainerControl2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.mulBusiness = new LongWin.DataWarehouseReport.WinUI.SelectBussinessType();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSelectCustomer = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.txtSelectSales = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.ucSelectCompany = new LongWin.DataWarehouseReport.WinUI.SelectCompanyCtl();
            this.ultraExplorerBarContainerControl3 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.txtSearchField = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSearchType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rabProfit = new System.Windows.Forms.RadioButton();
            this.rabAll = new System.Windows.Forms.RadioButton();
            this.rabNotProfit = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.ultraExplorerBar1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar();
            this.ultraExplorerBarContainerControl1.SuspendLayout();
            this.ultraExplorerBarContainerControl2.SuspendLayout();
            this.ultraExplorerBarContainerControl3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraExplorerBar1)).BeginInit();
            this.ultraExplorerBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraExplorerBarContainerControl1
            // 
            this.ultraExplorerBarContainerControl1.Controls.Add(this.cmbDateType);
            this.ultraExplorerBarContainerControl1.Controls.Add(this.label10);
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
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Name = "label10";
            // 
            // ucDateTime
            // 
            resources.ApplyResources(this.ucDateTime, "ucDateTime");
            this.ucDateTime.BackColor = System.Drawing.Color.Transparent;
            this.ucDateTime.ConditionType = LongWin.DataWarehouseReport.WinUI.ConditionDateType.Month;
            this.ucDateTime.DateTimeFrom = new System.DateTime(2009, 9, 1, 0, 0, 0, 0);
            this.ucDateTime.DateTimeTo = new System.DateTime(2009, 9, 30, 0, 0, 0, 0);
            this.ucDateTime.DefaultDateTimeType = LongWin.DataWarehouseReport.WinUI.DateTimeCtl.DefaultType.ThisMonth;
            this.ucDateTime.DefaultFrom = new System.DateTime(((long)(0)));
            this.ucDateTime.DefaultTo = new System.DateTime(((long)(0)));
            this.ucDateTime.Name = "ucDateTime";
            // 
            // ultraExplorerBarContainerControl2
            // 
            this.ultraExplorerBarContainerControl2.Controls.Add(this.mulBusiness);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label9);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label7);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label6);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label3);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label2);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label8);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtSelectCustomer);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtSelectSales);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.ucSelectCompany);
            resources.ApplyResources(this.ultraExplorerBarContainerControl2, "ultraExplorerBarContainerControl2");
            this.ultraExplorerBarContainerControl2.Name = "ultraExplorerBarContainerControl2";
            // 
            // mulBusiness
            // 
            resources.ApplyResources(this.mulBusiness, "mulBusiness");
            this.mulBusiness.DataFilter = "";
            this.mulBusiness.Name = "mulBusiness";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Name = "label9";
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
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
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
            // ucSelectCompany
            // 
            resources.ApplyResources(this.ucSelectCompany, "ucSelectCompany");
            this.ucSelectCompany.DataSource = null;
            this.ucSelectCompany.IsDisplayDepartment = true;
            this.ucSelectCompany.Name = "ucSelectCompany";
            // 
            // ultraExplorerBarContainerControl3
            // 
            this.ultraExplorerBarContainerControl3.Controls.Add(this.txtSearchField);
            this.ultraExplorerBarContainerControl3.Controls.Add(this.label5);
            this.ultraExplorerBarContainerControl3.Controls.Add(this.label4);
            this.ultraExplorerBarContainerControl3.Controls.Add(this.cmbSearchType);
            this.ultraExplorerBarContainerControl3.Controls.Add(this.label1);
            this.ultraExplorerBarContainerControl3.Controls.Add(this.panel2);
            resources.ApplyResources(this.ultraExplorerBarContainerControl3, "ultraExplorerBarContainerControl3");
            this.ultraExplorerBarContainerControl3.Name = "ultraExplorerBarContainerControl3";
            // 
            // txtSearchField
            // 
            resources.ApplyResources(this.txtSearchField, "txtSearchField");
            this.txtSearchField.BackColor = System.Drawing.Color.White;
            this.txtSearchField.Name = "txtSearchField";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // cmbSearchType
            // 
            this.cmbSearchType.BackColor = System.Drawing.Color.White;
            this.cmbSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchType.FormattingEnabled = true;
            this.cmbSearchType.Items.AddRange(new object[] {
            resources.GetString("cmbSearchType.Items"),
            resources.GetString("cmbSearchType.Items1"),
            resources.GetString("cmbSearchType.Items2"),
            resources.GetString("cmbSearchType.Items3"),
            resources.GetString("cmbSearchType.Items4")});
            resources.ApplyResources(this.cmbSearchType, "cmbSearchType");
            this.cmbSearchType.Name = "cmbSearchType";
            this.cmbSearchType.SelectedIndexChanged += new System.EventHandler(this.cmbSearchType_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.rabProfit);
            this.panel2.Controls.Add(this.rabAll);
            this.panel2.Controls.Add(this.rabNotProfit);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // rabProfit
            // 
            resources.ApplyResources(this.rabProfit, "rabProfit");
            this.rabProfit.BackColor = System.Drawing.Color.White;
            this.rabProfit.Name = "rabProfit";
            this.rabProfit.UseVisualStyleBackColor = false;
            // 
            // rabAll
            // 
            resources.ApplyResources(this.rabAll, "rabAll");
            this.rabAll.BackColor = System.Drawing.Color.White;
            this.rabAll.Checked = true;
            this.rabAll.Name = "rabAll";
            this.rabAll.TabStop = true;
            this.rabAll.UseVisualStyleBackColor = false;
            // 
            // rabNotProfit
            // 
            resources.ApplyResources(this.rabNotProfit, "rabNotProfit");
            this.rabNotProfit.BackColor = System.Drawing.Color.White;
            this.rabNotProfit.Name = "rabNotProfit";
            this.rabNotProfit.UseVisualStyleBackColor = false;
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
            ultraExplorerBarGroup1.Container = this.ultraExplorerBarContainerControl1;
            ultraExplorerBarGroup1.Settings.ContainerHeight = 198;
            resources.ApplyResources(ultraExplorerBarGroup1, "ultraExplorerBarGroup1");
            ultraExplorerBarGroup2.Container = this.ultraExplorerBarContainerControl2;
            ultraExplorerBarGroup2.Settings.ContainerHeight = 134;
            resources.ApplyResources(ultraExplorerBarGroup2, "ultraExplorerBarGroup2");
            ultraExplorerBarGroup3.Container = this.ultraExplorerBarContainerControl3;
            ultraExplorerBarGroup3.Settings.ContainerHeight = 96;
            resources.ApplyResources(ultraExplorerBarGroup3, "ultraExplorerBarGroup3");
            this.ultraExplorerBar1.Groups.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup[] {
            ultraExplorerBarGroup1,
            ultraExplorerBarGroup2,
            ultraExplorerBarGroup3});
            this.ultraExplorerBar1.GroupSettings.Style = Infragistics.Win.UltraWinExplorerBar.GroupStyle.ControlContainer;
            this.ultraExplorerBar1.Name = "ultraExplorerBar1";
            // 
            // JobInformation_SearchPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.ultraExplorerBar1);
            this.Controls.Add(this.panel1);
            this.Name = "JobInformation_SearchPart";
            this.ultraExplorerBarContainerControl1.ResumeLayout(false);
            this.ultraExplorerBarContainerControl1.PerformLayout();
            this.ultraExplorerBarContainerControl2.ResumeLayout(false);
            this.ultraExplorerBarContainerControl2.PerformLayout();
            this.ultraExplorerBarContainerControl3.ResumeLayout(false);
            this.ultraExplorerBarContainerControl3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.Label label2;
        
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl ultraExplorerBarContainerControl3;
        private System.Windows.Forms.RadioButton rabProfit;
        private System.Windows.Forms.RadioButton rabNotProfit;
        private System.Windows.Forms.RadioButton rabAll;
        private SelectCompanyCtl ucSelectCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbSearchType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSearchField;
        private System.Windows.Forms.Label label8;
        private utlMultiSelect txtSelectCustomer;
        private utlMultiSelect txtSelectSales;
        private SelectBussinessType mulBusiness;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbDateType;
        private System.Windows.Forms.Label label10;
    }
}
