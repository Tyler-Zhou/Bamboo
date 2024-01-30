namespace ICP.WF.Activities
{
	partial class UCDepartment
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCDepartment));
            this.pnlButton = new DevExpress.XtraEditors.PanelControl();
            this.btnAllToLeft = new DevExpress.XtraEditors.SimpleButton();
            this.btnToLeft = new DevExpress.XtraEditors.SimpleButton();
            this.btnToRight = new DevExpress.XtraEditors.SimpleButton();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.pnlLeft = new DevExpress.XtraEditors.PanelControl();
            this.treeDptLeft = new DevExpress.XtraTreeList.TreeList();
            this.tcLeftOrgName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.cmbFilter = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.splitterControl2 = new DevExpress.XtraEditors.SplitterControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.pnlFill = new DevExpress.XtraEditors.PanelControl();
            this.treeDptRight = new DevExpress.XtraTreeList.TreeList();
            this.tcRightOrgName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.OrgsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gbxJob = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButton)).BeginInit();
            this.pnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).BeginInit();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeDptLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).BeginInit();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeDptRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbxJob)).BeginInit();
            this.gbxJob.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.btnAllToLeft);
            this.pnlButton.Controls.Add(this.btnToLeft);
            this.pnlButton.Controls.Add(this.btnToRight);
            resources.ApplyResources(this.pnlButton, "pnlButton");
            this.pnlButton.Name = "pnlButton";
            // 
            // btnAllToLeft
            // 
            resources.ApplyResources(this.btnAllToLeft, "btnAllToLeft");
            this.btnAllToLeft.Name = "btnAllToLeft";
            this.btnAllToLeft.Click += new System.EventHandler(this.btnAllToLeft_Click);
            // 
            // btnToLeft
            // 
            resources.ApplyResources(this.btnToLeft, "btnToLeft");
            this.btnToLeft.Name = "btnToLeft";
            this.btnToLeft.Click += new System.EventHandler(this.btnToLeft_Click);
            // 
            // btnToRight
            // 
            resources.ApplyResources(this.btnToRight, "btnToRight");
            this.btnToRight.Name = "btnToRight";
            this.btnToRight.Click += new System.EventHandler(this.btnToRight_Click);
            // 
            // splitterControl1
            // 
            resources.ApplyResources(this.splitterControl1, "splitterControl1");
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.TabStop = false;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.treeDptLeft);
            resources.ApplyResources(this.pnlLeft, "pnlLeft");
            this.pnlLeft.Name = "pnlLeft";
            // 
            // treeDptLeft
            // 
            this.treeDptLeft.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tcLeftOrgName});
            resources.ApplyResources(this.treeDptLeft, "treeDptLeft");
            this.treeDptLeft.Name = "treeDptLeft";
            this.treeDptLeft.OptionsBehavior.Editable = false;
            this.treeDptLeft.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeDptLeft_FocusedNodeChanged);
            this.treeDptLeft.DoubleClick += new System.EventHandler(this.treeDptLeft_DoubleClick);
            // 
            // tcLeftOrgName
            // 
            this.tcLeftOrgName.Name = "tcLeftOrgName";
            resources.ApplyResources(this.tcLeftOrgName, "tcLeftOrgName");
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.cmbFilter);
            resources.ApplyResources(this.pnlTop, "pnlTop");
            this.pnlTop.Name = "pnlTop";
            // 
            // cmbFilter
            // 
            resources.ApplyResources(this.cmbFilter, "cmbFilter");
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbFilter.Properties.Buttons"))))});
            this.cmbFilter.SelectedValueChanged += new System.EventHandler(this.imageComboBoxEdit1_SelectedValueChanged);
            // 
            // splitterControl2
            // 
            resources.ApplyResources(this.splitterControl2, "splitterControl2");
            this.splitterControl2.Name = "splitterControl2";
            this.splitterControl2.TabStop = false;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlFill);
            this.pnlMain.Controls.Add(this.splitterControl2);
            this.pnlMain.Controls.Add(this.pnlButton);
            this.pnlMain.Controls.Add(this.splitterControl1);
            this.pnlMain.Controls.Add(this.pnlLeft);
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.Name = "pnlMain";
            // 
            // pnlFill
            // 
            this.pnlFill.Controls.Add(this.treeDptRight);
            resources.ApplyResources(this.pnlFill, "pnlFill");
            this.pnlFill.Name = "pnlFill";
            // 
            // treeDptRight
            // 
            this.treeDptRight.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tcRightOrgName});
            this.treeDptRight.DataSource = this.OrgsBindingSource;
            resources.ApplyResources(this.treeDptRight, "treeDptRight");
            this.treeDptRight.Name = "treeDptRight";
            this.treeDptRight.OptionsBehavior.Editable = false;
            this.treeDptRight.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeDptRight_FocusedNodeChanged);
            this.treeDptRight.DoubleClick += new System.EventHandler(this.treeDptRight_DoubleClick);
            // 
            // tcRightOrgName
            // 
            this.tcRightOrgName.Name = "tcRightOrgName";
            resources.ApplyResources(this.tcRightOrgName, "tcRightOrgName");
            // 
            // gbxJob
            // 
            this.gbxJob.Controls.Add(this.pnlMain);
            this.gbxJob.Controls.Add(this.pnlTop);
            resources.ApplyResources(this.gbxJob, "gbxJob");
            this.gbxJob.Name = "gbxJob";
            // 
            // UCDepartment
            // 
            this.Controls.Add(this.gbxJob);
            this.Name = "UCDepartment";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.pnlButton)).EndInit();
            this.pnlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeDptLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).EndInit();
            this.pnlFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeDptRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbxJob)).EndInit();
            this.gbxJob.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private DevExpress.XtraEditors.PanelControl pnlButton;
        private DevExpress.XtraEditors.SimpleButton btnAllToLeft;
        private DevExpress.XtraEditors.SimpleButton btnToLeft;
        private DevExpress.XtraEditors.SimpleButton btnToRight;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.PanelControl pnlLeft;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.SplitterControl splitterControl2;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlFill;
        private DevExpress.XtraEditors.GroupControl gbxJob;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcLeftOrgName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcRightOrgName;
        public System.Windows.Forms.BindingSource OrgsBindingSource;
        public DevExpress.XtraTreeList.TreeList treeDptRight;
        public DevExpress.XtraTreeList.TreeList treeDptLeft;
        public DevExpress.XtraEditors.ImageComboBoxEdit cmbFilter;

    }
}
