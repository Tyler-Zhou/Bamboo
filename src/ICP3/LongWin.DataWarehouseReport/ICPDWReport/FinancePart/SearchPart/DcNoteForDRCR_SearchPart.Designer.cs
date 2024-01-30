namespace LongWin.DataWarehouseReport.WinUI
{
    partial class DcNoteForDRCR_SearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DcNoteForDRCR_SearchPart));
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup3 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            this.ultraExplorerBarContainerControl1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.ucDateTime = new LongWin.DataWarehouseReport.WinUI.DateTimeCtl();
            this.ultraExplorerBarContainerControl2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.txtShipTo = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.txtSales = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.cmbGroupBy = new LongWin.DataWarehouseReport.WinUI.CheckComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbRecoupFlag = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rabTotal = new System.Windows.Forms.RadioButton();
            this.rabDetail = new System.Windows.Forms.RadioButton();
            this.ultraExplorerBarContainerControl3 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.mulShippingLine = new LongWin.DataWarehouseReport.WinUI.SelectShippingLineCtl();
            this.mulBusiness = new LongWin.DataWarehouseReport.WinUI.SelectBussinessType();
            this.ucSelectCompany = new LongWin.DataWarehouseReport.WinUI.SelectJobPlaceCtl();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.ultraExplorerBar1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar();
            this.ultraExplorerBarContainerControl1.SuspendLayout();
            this.ultraExplorerBarContainerControl2.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtShipTo);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtSales);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbGroupBy);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label1);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbRecoupFlag);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label5);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label3);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label2);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.panel2);
            resources.ApplyResources(this.ultraExplorerBarContainerControl2, "ultraExplorerBarContainerControl2");
            this.ultraExplorerBarContainerControl2.Name = "ultraExplorerBarContainerControl2";
            // 
            // txtShipTo
            // 
            resources.ApplyResources(this.txtShipTo, "txtShipTo");
            this.txtShipTo.Name = "txtShipTo";
            this.txtShipTo.SelectedText = "";
            this.txtShipTo.SelectedValue = null;
            this.txtShipTo.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtShipTo_DoSearching);
            // 
            // txtSales
            // 
            resources.ApplyResources(this.txtSales, "txtSales");
            this.txtSales.Name = "txtSales";
            this.txtSales.SelectedText = "";
            this.txtSales.SelectedValue = null;
            this.txtSales.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtSales_DoSearching);
            // 
            // cmbGroupBy
            // 
            resources.ApplyResources(this.cmbGroupBy, "cmbGroupBy");
            this.cmbGroupBy.BackColor = System.Drawing.Color.White;
            this.cmbGroupBy.Items = new string[0];
            this.cmbGroupBy.MaxChecked = 0;
            this.cmbGroupBy.Name = "cmbGroupBy";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // cmbRecoupFlag
            // 
            resources.ApplyResources(this.cmbRecoupFlag, "cmbRecoupFlag");
            this.cmbRecoupFlag.BackColor = System.Drawing.Color.White;
            this.cmbRecoupFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecoupFlag.FormattingEnabled = true;
            this.cmbRecoupFlag.Items.AddRange(new object[] {
            resources.GetString("cmbRecoupFlag.Items"),
            resources.GetString("cmbRecoupFlag.Items1"),
            resources.GetString("cmbRecoupFlag.Items2")});
            this.cmbRecoupFlag.Name = "cmbRecoupFlag";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Name = "label5";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Name = "label2";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.rabTotal);
            this.panel2.Controls.Add(this.rabDetail);
            this.panel2.Name = "panel2";
            // 
            // rabTotal
            // 
            resources.ApplyResources(this.rabTotal, "rabTotal");
            this.rabTotal.BackColor = System.Drawing.Color.White;
            this.rabTotal.Checked = true;
            this.rabTotal.Name = "rabTotal";
            this.rabTotal.TabStop = true;
            this.rabTotal.UseVisualStyleBackColor = false;
            this.rabTotal.CheckedChanged += new System.EventHandler(this.rabTotal_CheckedChanged);
            // 
            // rabDetail
            // 
            resources.ApplyResources(this.rabDetail, "rabDetail");
            this.rabDetail.BackColor = System.Drawing.Color.White;
            this.rabDetail.Name = "rabDetail";
            this.rabDetail.UseVisualStyleBackColor = false;
            this.rabDetail.CheckedChanged += new System.EventHandler(this.rabDetail_CheckedChanged);
            // 
            // ultraExplorerBarContainerControl3
            // 
            this.ultraExplorerBarContainerControl3.Controls.Add(this.mulShippingLine);
            this.ultraExplorerBarContainerControl3.Controls.Add(this.mulBusiness);
            this.ultraExplorerBarContainerControl3.Controls.Add(this.ucSelectCompany);
            this.ultraExplorerBarContainerControl3.Controls.Add(this.label6);
            this.ultraExplorerBarContainerControl3.Controls.Add(this.label4);
            resources.ApplyResources(this.ultraExplorerBarContainerControl3, "ultraExplorerBarContainerControl3");
            this.ultraExplorerBarContainerControl3.Name = "ultraExplorerBarContainerControl3";
            // 
            // mulShippingLine
            // 
            resources.ApplyResources(this.mulShippingLine, "mulShippingLine");
            this.mulShippingLine.Name = "mulShippingLine";
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
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Name = "label6";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Name = "label4";
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
            ultraExplorerBarGroup1.Settings.ContainerHeight = 124;
            resources.ApplyResources(ultraExplorerBarGroup1, "ultraExplorerBarGroup1");
            ultraExplorerBarGroup2.Container = this.ultraExplorerBarContainerControl2;
            ultraExplorerBarGroup2.Settings.ContainerHeight = 120;
            resources.ApplyResources(ultraExplorerBarGroup2, "ultraExplorerBarGroup2");
            ultraExplorerBarGroup3.Container = this.ultraExplorerBarContainerControl3;
            ultraExplorerBarGroup3.Settings.ContainerHeight = 132;
            resources.ApplyResources(ultraExplorerBarGroup3, "ultraExplorerBarGroup3");
            this.ultraExplorerBar1.Groups.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup[] {
            ultraExplorerBarGroup1,
            ultraExplorerBarGroup2,
            ultraExplorerBarGroup3});
            this.ultraExplorerBar1.GroupSettings.Style = Infragistics.Win.UltraWinExplorerBar.GroupStyle.ControlContainer;
            this.ultraExplorerBar1.Name = "ultraExplorerBar1";
            // 
            // DcNoteForDRCR_SearchPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.ultraExplorerBar1);
            this.Controls.Add(this.panel1);
            this.Name = "DcNoteForDRCR_SearchPart";
            this.ultraExplorerBarContainerControl1.ResumeLayout(false);
            this.ultraExplorerBarContainerControl2.ResumeLayout(false);
            this.ultraExplorerBarContainerControl2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        
        private System.Windows.Forms.ComboBox cmbRecoupFlag;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rabTotal;
        private System.Windows.Forms.RadioButton rabDetail;
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl ultraExplorerBarContainerControl3;
        private CheckComboBox cmbGroupBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private SelectJobPlaceCtl ucSelectCompany;
        private SelectBussinessType mulBusiness;
        private SelectShippingLineCtl mulShippingLine;
        private utlMultiSelect txtShipTo;
        private utlMultiSelect txtSales;
    }
}
