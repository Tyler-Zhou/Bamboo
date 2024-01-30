namespace ICP.WF.Activities
{
	partial class UCJobList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCJobList));
            this.gbxJob = new DevExpress.XtraEditors.GroupControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.pnlFill = new DevExpress.XtraEditors.PanelControl();
            this.listJobRight = new DevExpress.XtraEditors.ListBoxControl();
            this.JobsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitterControl2 = new DevExpress.XtraEditors.SplitterControl();
            this.pnlButton = new DevExpress.XtraEditors.PanelControl();
            this.btnAllToLeft = new DevExpress.XtraEditors.SimpleButton();
            this.btnToLeft = new DevExpress.XtraEditors.SimpleButton();
            this.btnToRight = new DevExpress.XtraEditors.SimpleButton();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.pnlLeft = new DevExpress.XtraEditors.PanelControl();
            this.listJobLeft = new DevExpress.XtraEditors.ListBoxControl();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.txtJobSearch = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gbxJob)).BeginInit();
            this.gbxJob.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).BeginInit();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listJobRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JobsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButton)).BeginInit();
            this.pnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).BeginInit();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listJobLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtJobSearch.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxJob
            // 
            this.gbxJob.Controls.Add(this.pnlMain);
            this.gbxJob.Controls.Add(this.pnlTop);
            resources.ApplyResources(this.gbxJob, "gbxJob");
            this.gbxJob.Name = "gbxJob";
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
            this.pnlFill.Controls.Add(this.listJobRight);
            resources.ApplyResources(this.pnlFill, "pnlFill");
            this.pnlFill.Name = "pnlFill";
            // 
            // listJobRight
            // 
            this.listJobRight.DataSource = this.JobsBindingSource;
            resources.ApplyResources(this.listJobRight, "listJobRight");
            this.listJobRight.Name = "listJobRight";
            this.listJobRight.DoubleClick += new System.EventHandler(this.listJobRight_DoubleClick);
            // 
            // splitterControl2
            // 
            resources.ApplyResources(this.splitterControl2, "splitterControl2");
            this.splitterControl2.Name = "splitterControl2";
            this.splitterControl2.TabStop = false;
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
            this.pnlLeft.Controls.Add(this.listJobLeft);
            resources.ApplyResources(this.pnlLeft, "pnlLeft");
            this.pnlLeft.Name = "pnlLeft";
            // 
            // listJobLeft
            // 
            resources.ApplyResources(this.listJobLeft, "listJobLeft");
            this.listJobLeft.Name = "listJobLeft";
            this.listJobLeft.DoubleClick += new System.EventHandler(this.listJobLeft_DoubleClick);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.txtJobSearch);
            resources.ApplyResources(this.pnlTop, "pnlTop");
            this.pnlTop.Name = "pnlTop";
            // 
            // txtJobSearch
            // 
            resources.ApplyResources(this.txtJobSearch, "txtJobSearch");
            this.txtJobSearch.Name = "txtJobSearch";
            this.txtJobSearch.EditValueChanged += new System.EventHandler(this.txtJobSearch_EditValueChanged);
            // 
            // UCJobList
            // 
            this.Controls.Add(this.gbxJob);
            this.Name = "UCJobList";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.gbxJob)).EndInit();
            this.gbxJob.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).EndInit();
            this.pnlFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listJobRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JobsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButton)).EndInit();
            this.pnlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listJobLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtJobSearch.Properties)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private DevExpress.XtraEditors.GroupControl gbxJob;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlFill;
        private DevExpress.XtraEditors.ListBoxControl listJobRight;
        private DevExpress.XtraEditors.SplitterControl splitterControl2;
        private DevExpress.XtraEditors.PanelControl pnlButton;
        private DevExpress.XtraEditors.SimpleButton btnAllToLeft;
        private DevExpress.XtraEditors.SimpleButton btnToLeft;
        private DevExpress.XtraEditors.SimpleButton btnToRight;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.PanelControl pnlLeft;
        private DevExpress.XtraEditors.ListBoxControl listJobLeft;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.TextEdit txtJobSearch;
        public System.Windows.Forms.BindingSource JobsBindingSource;
	}
}
