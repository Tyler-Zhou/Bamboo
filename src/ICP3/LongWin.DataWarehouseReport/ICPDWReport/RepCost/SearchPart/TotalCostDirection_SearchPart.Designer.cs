﻿namespace  LongWin.DataWarehouseReport.WinUI
{
    partial class TotalCostDirection_SearchPart
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TotalCostDirection_SearchPart));
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            this.ultraExplorerBarContainerControl1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.dateTimeCostCtl1 = new LongWin.DataWarehouseReport.WinUI.DateTimeCostCtl();
            this.ultraExplorerBarContainerControl2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.cmbTotalType = new System.Windows.Forms.ComboBox();
            this.cmbGroupByField = new System.Windows.Forms.ComboBox();
            this.txtSales = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.ucSelectCostItem = new LongWin.DataWarehouseReport.WinUI.SelectCostItemCtl();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucSelectCompany = new LongWin.DataWarehouseReport.WinUI.SelectCompanyCtl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.ultraExplorerBar1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ultraExplorerBarContainerControl1.SuspendLayout();
            this.ultraExplorerBarContainerControl2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraExplorerBar1)).BeginInit();
            this.ultraExplorerBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraExplorerBarContainerControl1
            // 
            this.ultraExplorerBarContainerControl1.Controls.Add(this.dateTimeCostCtl1);
            resources.ApplyResources(this.ultraExplorerBarContainerControl1, "ultraExplorerBarContainerControl1");
            this.ultraExplorerBarContainerControl1.Name = "ultraExplorerBarContainerControl1";
            // 
            // dateTimeCostCtl1
            // 
            this.dateTimeCostCtl1.BackColor = System.Drawing.Color.White;
            this.dateTimeCostCtl1.DateTimeFrom = new System.DateTime(2009, 7, 1, 0, 0, 0, 0);
            this.dateTimeCostCtl1.DateTimeTo = new System.DateTime(2009, 7, 31, 0, 0, 0, 0);
            this.dateTimeCostCtl1.DefaultDateTimeType = LongWin.DataWarehouseReport.WinUI.DateTimeCostCtl.DefaultType.LastMonth;
            this.dateTimeCostCtl1.DefaultFrom = new System.DateTime(((long)(0)));
            this.dateTimeCostCtl1.DefaultTo = new System.DateTime(((long)(0)));
            resources.ApplyResources(this.dateTimeCostCtl1, "dateTimeCostCtl1");
            this.dateTimeCostCtl1.Name = "dateTimeCostCtl1";
            // 
            // ultraExplorerBarContainerControl2
            // 
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbTotalType);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbGroupByField);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtSales);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.ucSelectCostItem);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label3);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label5);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label2);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label4);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label1);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.ucSelectCompany);
            resources.ApplyResources(this.ultraExplorerBarContainerControl2, "ultraExplorerBarContainerControl2");
            this.ultraExplorerBarContainerControl2.Name = "ultraExplorerBarContainerControl2";
            // 
            // cmbTotalType
            // 
            resources.ApplyResources(this.cmbTotalType, "cmbTotalType");
            this.cmbTotalType.BackColor = System.Drawing.Color.White;
            this.cmbTotalType.FormattingEnabled = true;
            this.cmbTotalType.Items.AddRange(new object[] {
            resources.GetString("cmbTotalType.Items"),
            resources.GetString("cmbTotalType.Items1")});
            this.cmbTotalType.Name = "cmbTotalType";
            // 
            // cmbGroupByField
            // 
            resources.ApplyResources(this.cmbGroupByField, "cmbGroupByField");
            this.cmbGroupByField.BackColor = System.Drawing.Color.White;
            this.cmbGroupByField.FormattingEnabled = true;
            this.cmbGroupByField.Items.AddRange(new object[] {
            resources.GetString("cmbGroupByField.Items"),
            resources.GetString("cmbGroupByField.Items1")});
            this.cmbGroupByField.Name = "cmbGroupByField";
            // 
            // txtSales
            // 
            resources.ApplyResources(this.txtSales, "txtSales");
            this.txtSales.Name = "txtSales";
            this.txtSales.SelectedText = "";
            this.txtSales.SelectedValue = null;
            this.txtSales.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtSales_DoSearching);
            // 
            // ucSelectCostItem
            // 
            resources.ApplyResources(this.ucSelectCostItem, "ucSelectCostItem");
            this.ucSelectCostItem.DataSource = null;
            this.ucSelectCostItem.Name = "ucSelectCostItem";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.SystemColors.Window;
            this.label2.Name = "label2";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // ucSelectCompany
            // 
            resources.ApplyResources(this.ucSelectCompany, "ucSelectCompany");
            this.ucSelectCompany.DataSource = null;
            this.ucSelectCompany.IsDisplayDepartment = true;
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
            ultraExplorerBarGroup1.Settings.ContainerHeight = 200;
            resources.ApplyResources(ultraExplorerBarGroup1, "ultraExplorerBarGroup1");
            ultraExplorerBarGroup2.Container = this.ultraExplorerBarContainerControl2;
            ultraExplorerBarGroup2.Settings.ContainerHeight = 123;
            resources.ApplyResources(ultraExplorerBarGroup2, "ultraExplorerBarGroup2");
            this.ultraExplorerBar1.Groups.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup[] {
            ultraExplorerBarGroup1,
            ultraExplorerBarGroup2});
            this.ultraExplorerBar1.GroupSettings.Style = Infragistics.Win.UltraWinExplorerBar.GroupStyle.ControlContainer;
            this.ultraExplorerBar1.Name = "ultraExplorerBar1";
            // 
            // TotalCostDirection_SearchPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.ultraExplorerBar1);
            this.Controls.Add(this.panel1);
            this.Name = "TotalCostDirection_SearchPart";
            this.Load += new System.EventHandler(this.Cost_Detail_SearchPart_Load);
            this.ultraExplorerBarContainerControl1.ResumeLayout(false);
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
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DateTimeCostCtl dateTimeCostCtl1;
        private SelectCompanyCtl ucSelectCompany;
        //private SelectCostItemCtl ucSelectCostItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private SelectCostItemCtl ucSelectCostItem;
        private utlMultiSelect txtSales;
        private System.Windows.Forms.ComboBox cmbGroupByField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbTotalType;
        private System.Windows.Forms.Label label5;
        
        
    }
}
