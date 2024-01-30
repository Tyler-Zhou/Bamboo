namespace LongWin.DataWarehouseReport.WinUI
{
    partial class WorkLoadForOperator_SearchPart
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
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            this.ultraExplorerBarContainerControl1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.ucDateTime = new LongWin.DataWarehouseReport.WinUI.DateTimeCtl();
            this.ultraExplorerBarContainerControl2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.cmbUserState = new System.Windows.Forms.ComboBox();
            this.ucSelectCompany = new LongWin.DataWarehouseReport.WinUI.SelectCompanyCtl();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.ultraExplorerBar1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar();
            this.cmbGroupBy = new LongWin.DataWarehouseReport.WinUI.CheckComboBox();
            this.ultraExplorerBarContainerControl1.SuspendLayout();
            this.ultraExplorerBarContainerControl2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraExplorerBar1)).BeginInit();
            this.ultraExplorerBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraExplorerBarContainerControl1
            // 
            this.ultraExplorerBarContainerControl1.Controls.Add(this.ucDateTime);
            this.ultraExplorerBarContainerControl1.Location = new System.Drawing.Point(28, 49);
            this.ultraExplorerBarContainerControl1.Name = "ultraExplorerBarContainerControl1";
            this.ultraExplorerBarContainerControl1.Size = new System.Drawing.Size(192, 127);
            this.ultraExplorerBarContainerControl1.TabIndex = 0;
            // 
            // ucDateTime
            // 
            this.ucDateTime.BackColor = System.Drawing.Color.Transparent;
            this.ucDateTime.ConditionType = LongWin.DataWarehouseReport.WinUI.ConditionDateType.Month;
            this.ucDateTime.DateTimeFrom = new System.DateTime(2011, 4, 1, 0, 0, 0, 0);
            this.ucDateTime.DateTimeTo = new System.DateTime(2011, 4, 30, 0, 0, 0, 0);
            this.ucDateTime.DefaultDateTimeType = LongWin.DataWarehouseReport.WinUI.DateTimeCtl.DefaultType.ThisMonth;
            this.ucDateTime.DefaultFrom = new System.DateTime(((long)(0)));
            this.ucDateTime.DefaultTo = new System.DateTime(((long)(0)));
            this.ucDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDateTime.Location = new System.Drawing.Point(0, 0);
            this.ucDateTime.Name = "ucDateTime";
            this.ucDateTime.Size = new System.Drawing.Size(192, 127);
            this.ucDateTime.TabIndex = 0;
            // 
            // ultraExplorerBarContainerControl2
            // 
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbGroupBy);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbUserState);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.ucSelectCompany);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label1);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label3);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label2);
            this.ultraExplorerBarContainerControl2.Location = new System.Drawing.Point(28, 235);
            this.ultraExplorerBarContainerControl2.Name = "ultraExplorerBarContainerControl2";
            this.ultraExplorerBarContainerControl2.Size = new System.Drawing.Size(192, 77);
            this.ultraExplorerBarContainerControl2.TabIndex = 1;
            // 
            // cmbUserState
            // 
            this.cmbUserState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbUserState.BackColor = System.Drawing.Color.White;
            this.cmbUserState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserState.FormattingEnabled = true;
            this.cmbUserState.Items.AddRange(new object[] {
            "无效",
            "有效",
            "所有"});
            this.cmbUserState.Location = new System.Drawing.Point(51, 49);
            this.cmbUserState.Name = "cmbUserState";
            this.cmbUserState.Size = new System.Drawing.Size(138, 20);
            this.cmbUserState.TabIndex = 23;
            // 
            // ucSelectCompany
            // 
            this.ucSelectCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucSelectCompany.DataSource = null;
            this.ucSelectCompany.IsDisplayDepartment = true;
            this.ucSelectCompany.Location = new System.Drawing.Point(51, 0);
            this.ucSelectCompany.Name = "ucSelectCompany";
            this.ucSelectCompany.Size = new System.Drawing.Size(138, 21);
            this.ucSelectCompany.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(-2, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "用户状态";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(-2, 4);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "公司";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(-2, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "分组条件";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 430);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(241, 38);
            this.panel1.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(173, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(65, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ultraExplorerBar1
            // 
            this.ultraExplorerBar1.Controls.Add(this.ultraExplorerBarContainerControl1);
            this.ultraExplorerBar1.Controls.Add(this.ultraExplorerBarContainerControl2);
            this.ultraExplorerBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            ultraExplorerBarGroup1.Container = this.ultraExplorerBarContainerControl1;
            ultraExplorerBarGroup1.Settings.ContainerHeight = 127;
            ultraExplorerBarGroup1.Text = "期间";
            ultraExplorerBarGroup2.Container = this.ultraExplorerBarContainerControl2;
            ultraExplorerBarGroup2.Settings.ContainerHeight = 77;
            ultraExplorerBarGroup2.Text = "常规";
            this.ultraExplorerBar1.Groups.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup[] {
            ultraExplorerBarGroup1,
            ultraExplorerBarGroup2});
            this.ultraExplorerBar1.GroupSettings.Style = Infragistics.Win.UltraWinExplorerBar.GroupStyle.ControlContainer;
            this.ultraExplorerBar1.Location = new System.Drawing.Point(0, 0);
            this.ultraExplorerBar1.Name = "ultraExplorerBar1";
            this.ultraExplorerBar1.Size = new System.Drawing.Size(241, 430);
            this.ultraExplorerBar1.TabIndex = 5;
            // 
            // cmbGroupBy
            // 
            this.cmbGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGroupBy.BackColor = System.Drawing.Color.White;
            this.cmbGroupBy.Font = new System.Drawing.Font("宋体", 9F);
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
            this.cmbGroupBy.Location = new System.Drawing.Point(51, 23);
            this.cmbGroupBy.MaxChecked = 3;
            this.cmbGroupBy.Name = "cmbGroupBy";
            this.cmbGroupBy.Size = new System.Drawing.Size(138, 22);
            this.cmbGroupBy.TabIndex = 56;
            this.cmbGroupBy.Load += new System.EventHandler(this.cmbGroupBy_Load);
            // 
            // WorkLoadForOperator_SearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.ultraExplorerBar1);
            this.Controls.Add(this.panel1);
            this.Name = "WorkLoadForOperator_SearchPart";
            this.Size = new System.Drawing.Size(241, 468);
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
        private DateTimeCtl ucDateTime;
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl ultraExplorerBarContainerControl2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private SelectCompanyCtl ucSelectCompany;
        private System.Windows.Forms.ComboBox cmbUserState;
        private System.Windows.Forms.Label label1;
        private CheckComboBox cmbGroupBy;
    }
}
