namespace ICP.WF.Activities
{
	partial class UCUserList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCUserList));
            this.gbxUser = new DevExpress.XtraEditors.GroupControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.pnlFill = new DevExpress.XtraEditors.PanelControl();
            this.listUserRight = new DevExpress.XtraEditors.ListBoxControl();
            this.UsersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitterControl2 = new DevExpress.XtraEditors.SplitterControl();
            this.pnlButton = new DevExpress.XtraEditors.PanelControl();
            this.btnAllToLeft = new DevExpress.XtraEditors.SimpleButton();
            this.btnToLeft = new DevExpress.XtraEditors.SimpleButton();
            this.btnToRight = new DevExpress.XtraEditors.SimpleButton();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.pnlLeft = new DevExpress.XtraEditors.PanelControl();
            this.listUserLeft = new DevExpress.XtraEditors.ListBoxControl();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.txtUserSearch = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gbxUser)).BeginInit();
            this.gbxUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).BeginInit();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listUserRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButton)).BeginInit();
            this.pnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).BeginInit();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listUserLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserSearch.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxUser
            // 
            this.gbxUser.Controls.Add(this.pnlMain);
            this.gbxUser.Controls.Add(this.pnlTop);
            resources.ApplyResources(this.gbxUser, "gbxUser");
            this.gbxUser.Name = "gbxUser";
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
            this.pnlFill.Controls.Add(this.listUserRight);
            resources.ApplyResources(this.pnlFill, "pnlFill");
            this.pnlFill.Name = "pnlFill";
            // 
            // listUserRight
            // 
            this.listUserRight.DataSource = this.UsersBindingSource;
            resources.ApplyResources(this.listUserRight, "listUserRight");
            this.listUserRight.Name = "listUserRight";
            this.listUserRight.DoubleClick += new System.EventHandler(this.listUserRight_DoubleClick);
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
            this.pnlLeft.Controls.Add(this.listUserLeft);
            resources.ApplyResources(this.pnlLeft, "pnlLeft");
            this.pnlLeft.Name = "pnlLeft";
            // 
            // listUserLeft
            // 
            resources.ApplyResources(this.listUserLeft, "listUserLeft");
            this.listUserLeft.Name = "listUserLeft";
            this.listUserLeft.DoubleClick += new System.EventHandler(this.listUserLeft_DoubleClick);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.txtUserSearch);
            resources.ApplyResources(this.pnlTop, "pnlTop");
            this.pnlTop.Name = "pnlTop";
            // 
            // txtUserSearch
            // 
            resources.ApplyResources(this.txtUserSearch, "txtUserSearch");
            this.txtUserSearch.Name = "txtUserSearch";
            this.txtUserSearch.EditValueChanged += new System.EventHandler(this.txtUserSearch_EditValueChanged);
            // 
            // UCUserList
            // 
            this.Controls.Add(this.gbxUser);
            this.Name = "UCUserList";
            resources.ApplyResources(this, "$this");
            this.Load += new System.EventHandler(this.UCUserList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gbxUser)).EndInit();
            this.gbxUser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).EndInit();
            this.pnlFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listUserRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButton)).EndInit();
            this.pnlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listUserLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtUserSearch.Properties)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private DevExpress.XtraEditors.GroupControl gbxUser;
        private DevExpress.XtraEditors.TextEdit txtUserSearch;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.PanelControl pnlLeft;
        private DevExpress.XtraEditors.PanelControl pnlButton;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.SplitterControl splitterControl2;
        private DevExpress.XtraEditors.PanelControl pnlFill;
        private DevExpress.XtraEditors.SimpleButton btnToRight;
        private DevExpress.XtraEditors.SimpleButton btnAllToLeft;
        private DevExpress.XtraEditors.SimpleButton btnToLeft;
        private DevExpress.XtraEditors.ListBoxControl listUserLeft;
        private DevExpress.XtraEditors.ListBoxControl listUserRight;
        public System.Windows.Forms.BindingSource UsersBindingSource;
	}
}
