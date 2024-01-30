namespace LongWin.DataWarehouseReport.WinUI
{
    partial class DcNoteAgeForDRTotal_SearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DcNoteAgeForDRTotal_SearchPart));
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            this.ultraExplorerBarContainerControl1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.cmbViewType = new System.Windows.Forms.ComboBox();
            this.dtEtdEndTime = new System.Windows.Forms.DateTimePicker();
            this.IsDR = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ultraExplorerBarContainerControl2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.cmbGroupBy = new LongWin.DataWarehouseReport.WinUI.CheckComboBox();
            this.selectCustomer = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.mulBusiness = new LongWin.DataWarehouseReport.WinUI.SelectBussinessType();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSalesType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ucSelectCompany = new LongWin.DataWarehouseReport.WinUI.SelectJobPlaceCtl();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            this.ultraExplorerBarContainerControl1.Controls.Add(this.cmbViewType);
            this.ultraExplorerBarContainerControl1.Controls.Add(this.dtEtdEndTime);
            this.ultraExplorerBarContainerControl1.Controls.Add(this.IsDR);
            this.ultraExplorerBarContainerControl1.Controls.Add(this.label8);
            this.ultraExplorerBarContainerControl1.Controls.Add(this.label1);
            resources.ApplyResources(this.ultraExplorerBarContainerControl1, "ultraExplorerBarContainerControl1");
            this.ultraExplorerBarContainerControl1.Name = "ultraExplorerBarContainerControl1";
            // 
            // cmbViewType
            // 
            resources.ApplyResources(this.cmbViewType, "cmbViewType");
            this.cmbViewType.BackColor = System.Drawing.Color.White;
            this.cmbViewType.FormattingEnabled = true;
            this.cmbViewType.Items.AddRange(new object[] {
            resources.GetString("cmbViewType.Items"),
            resources.GetString("cmbViewType.Items1"),
            resources.GetString("cmbViewType.Items2")});
            this.cmbViewType.Name = "cmbViewType";
            // 
            // dtEtdEndTime
            // 
            resources.ApplyResources(this.dtEtdEndTime, "dtEtdEndTime");
            this.dtEtdEndTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtEtdEndTime.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtEtdEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEtdEndTime.Name = "dtEtdEndTime";
            this.dtEtdEndTime.Value = new System.DateTime(2006, 11, 21, 0, 0, 0, 0);
            // 
            // IsDR
            // 
            resources.ApplyResources(this.IsDR, "IsDR");
            this.IsDR.BackColor = System.Drawing.Color.White;
            this.IsDR.Name = "IsDR";
            this.IsDR.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Name = "label8";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // ultraExplorerBarContainerControl2
            // 
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbGroupBy);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.selectCustomer);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.mulBusiness);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label2);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbSalesType);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label9);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.ucSelectCompany);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label4);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label3);
            resources.ApplyResources(this.ultraExplorerBarContainerControl2, "ultraExplorerBarContainerControl2");
            this.ultraExplorerBarContainerControl2.Name = "ultraExplorerBarContainerControl2";
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
        "装货港"};
            this.cmbGroupBy.MaxChecked = 3;
            this.cmbGroupBy.Name = "cmbGroupBy";
            // 
            // selectCustomer
            // 
            resources.ApplyResources(this.selectCustomer, "selectCustomer");
            this.selectCustomer.Name = "selectCustomer";
            this.selectCustomer.SelectedText = "";
            this.selectCustomer.SelectedValue = null;
            this.selectCustomer.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.selectCustomer_DoSearching);
            // 
            // mulBusiness
            // 
            resources.ApplyResources(this.mulBusiness, "mulBusiness");
            this.mulBusiness.DataFilter = "";
            this.mulBusiness.Name = "mulBusiness";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Name = "label2";
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
            resources.GetString("cmbSalesType.Items3")});
            this.cmbSalesType.Name = "cmbSalesType";
            this.cmbSalesType.SelectedIndexChanged += new System.EventHandler(this.cmbSalesType_SelectedIndexChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Name = "label9";
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
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Name = "label3";
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
            ultraExplorerBarGroup1.Settings.ContainerHeight = 80;
            resources.ApplyResources(ultraExplorerBarGroup1, "ultraExplorerBarGroup1");
            ultraExplorerBarGroup2.Container = this.ultraExplorerBarContainerControl2;
            ultraExplorerBarGroup2.Settings.ContainerHeight = 180;
            resources.ApplyResources(ultraExplorerBarGroup2, "ultraExplorerBarGroup2");
            this.ultraExplorerBar1.Groups.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup[] {
            ultraExplorerBarGroup1,
            ultraExplorerBarGroup2});
            this.ultraExplorerBar1.GroupSettings.Style = Infragistics.Win.UltraWinExplorerBar.GroupStyle.ControlContainer;
            this.ultraExplorerBar1.Name = "ultraExplorerBar1";
            // 
            // DcNoteAgeForDRTotal_SearchPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.ultraExplorerBar1);
            this.Controls.Add(this.panel1);
            this.Name = "DcNoteAgeForDRTotal_SearchPart";
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
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl ultraExplorerBarContainerControl2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl ultraExplorerBarContainerControl1;
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar ultraExplorerBar1;
        private System.Windows.Forms.CheckBox IsDR;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbViewType;
        private System.Windows.Forms.DateTimePicker dtEtdEndTime;
        private SelectJobPlaceCtl ucSelectCompany;
        private System.Windows.Forms.ComboBox cmbSalesType;
        private System.Windows.Forms.Label label9;
        private SelectBussinessType mulBusiness;
        private utlMultiSelect selectCustomer;
        private System.Windows.Forms.Label label2;
        private CheckComboBox cmbGroupBy;
    }
}
