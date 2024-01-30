namespace LongWin.DataWarehouseReport.WinUI.BusinessPart.REPBusiness.SearchPart
{
    partial class AgentForOperator_SearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentForOperator_SearchPart));
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            this.ultraExplorerBarContainerControl1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.txtSelectCustomer = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSelectSales = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.cmbGoodsType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ultraExplorerBarContainerControl2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.label4 = new System.Windows.Forms.Label();
            this.ucDateETD = new LongWin.DataWarehouseReport.WinUI.DateTimeCtl();
            this.cobSearchDate = new System.Windows.Forms.ComboBox();
            this.ultraExplorerBar1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSeach = new System.Windows.Forms.Button();
            this.ultraExplorerBarContainerControl1.SuspendLayout();
            this.ultraExplorerBarContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraExplorerBar1)).BeginInit();
            this.ultraExplorerBar1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraExplorerBarContainerControl1
            // 
            this.ultraExplorerBarContainerControl1.Controls.Add(this.txtSelectCustomer);
            this.ultraExplorerBarContainerControl1.Controls.Add(this.label3);
            this.ultraExplorerBarContainerControl1.Controls.Add(this.txtSelectSales);
            this.ultraExplorerBarContainerControl1.Controls.Add(this.cmbGoodsType);
            this.ultraExplorerBarContainerControl1.Controls.Add(this.label2);
            this.ultraExplorerBarContainerControl1.Controls.Add(this.label1);
            resources.ApplyResources(this.ultraExplorerBarContainerControl1, "ultraExplorerBarContainerControl1");
            this.ultraExplorerBarContainerControl1.Name = "ultraExplorerBarContainerControl1";
            // 
            // txtSelectCustomer
            // 
            resources.ApplyResources(this.txtSelectCustomer, "txtSelectCustomer");
            this.txtSelectCustomer.Name = "txtSelectCustomer";
            this.txtSelectCustomer.SelectedText = "";
            this.txtSelectCustomer.SelectedValue = null;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // txtSelectSales
            // 
            resources.ApplyResources(this.txtSelectSales, "txtSelectSales");
            this.txtSelectSales.Name = "txtSelectSales";
            this.txtSelectSales.SelectedText = "";
            this.txtSelectSales.SelectedValue = null;
            // 
            // cmbGoodsType
            // 
            resources.ApplyResources(this.cmbGoodsType, "cmbGoodsType");
            this.cmbGoodsType.BackColor = System.Drawing.Color.White;
            this.cmbGoodsType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGoodsType.FormattingEnabled = true;
            this.cmbGoodsType.Name = "cmbGoodsType";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // ultraExplorerBarContainerControl2
            // 
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label4);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.ucDateETD);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cobSearchDate);
            resources.ApplyResources(this.ultraExplorerBarContainerControl2, "ultraExplorerBarContainerControl2");
            this.ultraExplorerBarContainerControl2.Name = "ultraExplorerBarContainerControl2";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // ucDateETD
            // 
            this.ucDateETD.BackColor = System.Drawing.Color.Transparent;
            this.ucDateETD.ConditionType = LongWin.DataWarehouseReport.WinUI.ConditionDateType.Month;
            this.ucDateETD.DateTimeFrom = new System.DateTime(2009, 9, 1, 0, 0, 0, 0);
            this.ucDateETD.DateTimeTo = new System.DateTime(2009, 9, 30, 0, 0, 0, 0);
            this.ucDateETD.DefaultDateTimeType = LongWin.DataWarehouseReport.WinUI.DateTimeCtl.DefaultType.ThisMonth;
            this.ucDateETD.DefaultFrom = new System.DateTime(((long)(0)));
            this.ucDateETD.DefaultTo = new System.DateTime(((long)(0)));
            resources.ApplyResources(this.ucDateETD, "ucDateETD");
            this.ucDateETD.Name = "ucDateETD";
            // 
            // cobSearchDate
            // 
            resources.ApplyResources(this.cobSearchDate, "cobSearchDate");
            this.cobSearchDate.BackColor = System.Drawing.Color.White;
            this.cobSearchDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobSearchDate.FormattingEnabled = true;
            this.cobSearchDate.Name = "cobSearchDate";
            // 
            // ultraExplorerBar1
            // 
            this.ultraExplorerBar1.Controls.Add(this.ultraExplorerBarContainerControl1);
            this.ultraExplorerBar1.Controls.Add(this.ultraExplorerBarContainerControl2);
            resources.ApplyResources(this.ultraExplorerBar1, "ultraExplorerBar1");
            ultraExplorerBarGroup1.Container = this.ultraExplorerBarContainerControl1;
            ultraExplorerBarGroup1.Settings.ContainerHeight = 138;
            resources.ApplyResources(ultraExplorerBarGroup1, "ultraExplorerBarGroup1");
            ultraExplorerBarGroup2.Container = this.ultraExplorerBarContainerControl2;
            ultraExplorerBarGroup2.Settings.ContainerHeight = 190;
            resources.ApplyResources(ultraExplorerBarGroup2, "ultraExplorerBarGroup2");
            this.ultraExplorerBar1.Groups.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup[] {
            ultraExplorerBarGroup1,
            ultraExplorerBarGroup2});
            this.ultraExplorerBar1.GroupSettings.Style = Infragistics.Win.UltraWinExplorerBar.GroupStyle.ControlContainer;
            this.ultraExplorerBar1.Name = "ultraExplorerBar1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSeach);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnSeach
            // 
            resources.ApplyResources(this.btnSeach, "btnSeach");
            this.btnSeach.Name = "btnSeach";
            this.btnSeach.UseVisualStyleBackColor = true;
            // 
            // AgentForOperator_SearchPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.ultraExplorerBar1);
            this.Controls.Add(this.panel1);
            this.Name = "AgentForOperator_SearchPart";
            this.ultraExplorerBarContainerControl1.ResumeLayout(false);
            this.ultraExplorerBarContainerControl1.PerformLayout();
            this.ultraExplorerBarContainerControl2.ResumeLayout(false);
            this.ultraExplorerBarContainerControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraExplorerBar1)).EndInit();
            this.ultraExplorerBar1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar ultraExplorerBar1;
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl ultraExplorerBarContainerControl1;
        private System.Windows.Forms.ComboBox cmbGoodsType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl ultraExplorerBarContainerControl2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSeach;
        private System.Windows.Forms.Label label3;
        private utlMultiSelect txtSelectCustomer;
        private utlMultiSelect txtSelectSales;
        private System.Windows.Forms.ComboBox cobSearchDate;
        private System.Windows.Forms.Label label4;
        private DateTimeCtl ucDateETD;
    }
}
