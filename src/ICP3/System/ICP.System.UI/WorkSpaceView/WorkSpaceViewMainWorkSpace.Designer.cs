namespace ICP.Sys.UI.WorkSpaceView
{
    partial class WorkSpaceViewMainWorkSpace
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
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.ToolWorkSpace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.pnlList = new DevExpress.XtraEditors.PanelControl();
            this.ListWorkSpace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.pnlDetail = new DevExpress.XtraEditors.PanelControl();
            this.UserWorkspace = new DevExpress.XtraTab.XtraTabControl();
            this.pagView = new DevExpress.XtraTab.XtraTabPage();
            this.OPListWorkSpace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.pagRole = new DevExpress.XtraTab.XtraTabPage();
            this.pagUser = new DevExpress.XtraTab.XtraTabPage();
            this.UserListWorkSpace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.RoleListWorkSpace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlList)).BeginInit();
            this.pnlList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDetail)).BeginInit();
            this.pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UserWorkspace)).BeginInit();
            this.UserWorkspace.SuspendLayout();
            this.pagView.SuspendLayout();
            this.pagRole.SuspendLayout();
            this.pagUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.ToolWorkSpace);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(685, 25);
            this.pnlTop.TabIndex = 0;
            // 
            // ToolWorkSpace
            // 
            this.ToolWorkSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolWorkSpace.Location = new System.Drawing.Point(2, 2);
            this.ToolWorkSpace.Name = "ToolWorkSpace";
            this.ToolWorkSpace.Size = new System.Drawing.Size(681, 21);
            this.ToolWorkSpace.TabIndex = 1;
            this.ToolWorkSpace.Text = "deckWorkspace1";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.splitterControl1);
            this.pnlMain.Controls.Add(this.pnlList);
            this.pnlMain.Controls.Add(this.pnlDetail);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 25);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(685, 575);
            this.pnlMain.TabIndex = 1;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterControl1.Location = new System.Drawing.Point(2, 171);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(681, 6);
            this.splitterControl1.TabIndex = 2;
            this.splitterControl1.TabStop = false;
            // 
            // pnlList
            // 
            this.pnlList.Controls.Add(this.ListWorkSpace);
            this.pnlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlList.Location = new System.Drawing.Point(2, 2);
            this.pnlList.Name = "pnlList";
            this.pnlList.Size = new System.Drawing.Size(681, 175);
            this.pnlList.TabIndex = 0;
            // 
            // ListWorkSpace
            // 
            this.ListWorkSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListWorkSpace.Location = new System.Drawing.Point(2, 2);
            this.ListWorkSpace.Name = "ListWorkSpace";
            this.ListWorkSpace.Size = new System.Drawing.Size(677, 171);
            this.ListWorkSpace.TabIndex = 1;
            this.ListWorkSpace.Text = "deckWorkspace1";
            // 
            // pnlDetail
            // 
            this.pnlDetail.Controls.Add(this.UserWorkspace);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDetail.Location = new System.Drawing.Point(2, 177);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(681, 396);
            this.pnlDetail.TabIndex = 1;
            // 
            // UserWorkspace
            // 
            this.UserWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserWorkspace.Location = new System.Drawing.Point(2, 2);
            this.UserWorkspace.Name = "UserWorkspace";
            this.UserWorkspace.SelectedTabPage = this.pagView;
            this.UserWorkspace.Size = new System.Drawing.Size(677, 392);
            this.UserWorkspace.TabIndex = 0;
            this.UserWorkspace.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pagView,
            this.pagRole,
            this.pagUser});
            // 
            // pagView
            // 
            this.pagView.Controls.Add(this.OPListWorkSpace);
            this.pagView.Name = "pagView";
            this.pagView.Size = new System.Drawing.Size(670, 362);
            this.pagView.Text = "功能项";
            // 
            // OPListWorkSpace
            // 
            this.OPListWorkSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OPListWorkSpace.Location = new System.Drawing.Point(0, 0);
            this.OPListWorkSpace.Name = "OPListWorkSpace";
            this.OPListWorkSpace.Size = new System.Drawing.Size(670, 362);
            this.OPListWorkSpace.TabIndex = 1;
            this.OPListWorkSpace.Text = "deckWorkspace1";
            // 
            // pagRole
            // 
            this.pagRole.Controls.Add(this.RoleListWorkSpace);
            this.pagRole.Name = "pagRole";
            this.pagRole.Size = new System.Drawing.Size(670, 362);
            this.pagRole.Text = "角色";
            // 
            // pagUser
            // 
            this.pagUser.Controls.Add(this.UserListWorkSpace);
            this.pagUser.Name = "pagUser";
            this.pagUser.Size = new System.Drawing.Size(670, 362);
            this.pagUser.Text = "用户";
            // 
            // UserListWorkSpace
            // 
            this.UserListWorkSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserListWorkSpace.Location = new System.Drawing.Point(0, 0);
            this.UserListWorkSpace.Name = "UserListWorkSpace";
            this.UserListWorkSpace.Size = new System.Drawing.Size(670, 362);
            this.UserListWorkSpace.TabIndex = 1;
            this.UserListWorkSpace.Text = "deckWorkspace1";
            // 
            // RoleListWorkSpace
            // 
            this.RoleListWorkSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoleListWorkSpace.Location = new System.Drawing.Point(0, 0);
            this.RoleListWorkSpace.Name = "RoleListWorkSpace";
            this.RoleListWorkSpace.Size = new System.Drawing.Size(670, 362);
            this.RoleListWorkSpace.TabIndex = 2;
            this.RoleListWorkSpace.Text = "deckWorkspace1";
            // 
            // WorkSpaceViewMainWorkSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Name = "WorkSpaceViewMainWorkSpace";
            this.Size = new System.Drawing.Size(685, 600);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlList)).EndInit();
            this.pnlList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlDetail)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UserWorkspace)).EndInit();
            this.UserWorkspace.ResumeLayout(false);
            this.pagView.ResumeLayout(false);
            this.pagRole.ResumeLayout(false);
            this.pagUser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlList;
        private DevExpress.XtraEditors.PanelControl pnlDetail;
        private DevExpress.XtraTab.XtraTabControl UserWorkspace;
        private DevExpress.XtraTab.XtraTabPage pagView;
        private DevExpress.XtraTab.XtraTabPage pagUser;
        public Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ToolWorkSpace;
        public Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkSpace;
        public Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace OPListWorkSpace;
        public Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace UserListWorkSpace;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraTab.XtraTabPage pagRole;
        public Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace RoleListWorkSpace;
    }
}
