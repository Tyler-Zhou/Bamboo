namespace ICP.FAM.UI.BankTransaction
{
    partial class MainWorkspace
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
            this.BankTransactionToolbarWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.layoutContainerControl1 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.BankTransactionListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.xtab = new DevExpress.XtraTab.XtraTabControl();
            this.tabNextJobs = new DevExpress.XtraTab.XtraTabPage();
            this.BankTransactionEditWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.BankTransactionSearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.layoutContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtab)).BeginInit();
            this.xtab.SuspendLayout();
            this.tabNextJobs.SuspendLayout();
            this.dpSearch.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // BankTransactionToolbarWorkspace
            // 
            this.BankTransactionToolbarWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.BankTransactionToolbarWorkspace.Location = new System.Drawing.Point(0, 0);
            this.BankTransactionToolbarWorkspace.Name = "BankTransactionToolbarWorkspace";
            this.BankTransactionToolbarWorkspace.Size = new System.Drawing.Size(809, 27);
            this.BankTransactionToolbarWorkspace.TabIndex = 0;
            this.BankTransactionToolbarWorkspace.Text = "Toolbar";
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this.layoutContainerControl1;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpSearch});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // layoutContainerControl1
            // 
            this.layoutContainerControl1.Controls.Add(this.splitContainerControl1);
            this.layoutContainerControl1.Controls.Add(this.dpSearch);
            this.layoutContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutContainerControl1.Location = new System.Drawing.Point(0, 27);
            this.layoutContainerControl1.Name = "layoutContainerControl1";
            this.layoutContainerControl1.Size = new System.Drawing.Size(809, 444);
            this.layoutContainerControl1.TabIndex = 1;
            this.layoutContainerControl1.Text = "layoutContainerControl1";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(256, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.BankTransactionListWorkspace);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.xtab);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(553, 444);
            this.splitContainerControl1.SplitterPosition = 300;
            this.splitContainerControl1.TabIndex = 2;
            this.splitContainerControl1.Text = "分割控件";
            // 
            // BankTransactionListWorkspace
            // 
            this.BankTransactionListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BankTransactionListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.BankTransactionListWorkspace.Name = "BankTransactionListWorkspace";
            this.BankTransactionListWorkspace.Size = new System.Drawing.Size(553, 138);
            this.BankTransactionListWorkspace.TabIndex = 0;
            this.BankTransactionListWorkspace.Text = "List";
            // 
            // xtab
            // 
            this.xtab.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.xtab.Appearance.Options.UseBackColor = true;
            this.xtab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtab.Location = new System.Drawing.Point(0, 0);
            this.xtab.Name = "xtab";
            this.xtab.SelectedTabPage = this.tabNextJobs;
            this.xtab.Size = new System.Drawing.Size(553, 300);
            this.xtab.TabIndex = 2;
            this.xtab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabNextJobs});
            // 
            // tabNextJobs
            // 
            this.tabNextJobs.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabNextJobs.Appearance.PageClient.Options.UseBackColor = true;
            this.tabNextJobs.Controls.Add(this.BankTransactionEditWorkspace);
            this.tabNextJobs.Name = "tabNextJobs";
            this.tabNextJobs.Size = new System.Drawing.Size(546, 270);
            this.tabNextJobs.Text = "Edit";
            // 
            // BankTransactionEditWorkspace
            // 
            this.BankTransactionEditWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.BankTransactionEditWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BankTransactionEditWorkspace.Location = new System.Drawing.Point(0, 0);
            this.BankTransactionEditWorkspace.Name = "BankTransactionEditWorkspace";
            this.BankTransactionEditWorkspace.Size = new System.Drawing.Size(546, 270);
            this.BankTransactionEditWorkspace.TabIndex = 6;
            this.BankTransactionEditWorkspace.Text = "Checks";
            // 
            // dpSearch
            // 
            this.dpSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpSearch.Appearance.Options.UseBackColor = true;
            this.dpSearch.Controls.Add(this.dockPanel1_Container);
            this.dpSearch.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpSearch.ID = new System.Guid("567e8ba5-0436-4f57-bafd-9f0a1f68f0f2");
            this.dpSearch.Location = new System.Drawing.Point(0, 0);
            this.dpSearch.Name = "dpSearch";
            this.dpSearch.OriginalSize = new System.Drawing.Size(256, 200);
            this.dpSearch.Size = new System.Drawing.Size(256, 444);
            this.dpSearch.Text = "Search";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.BankTransactionSearchWorkspace);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(250, 416);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // BankTransactionSearchWorkspace
            // 
            this.BankTransactionSearchWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BankTransactionSearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.BankTransactionSearchWorkspace.Name = "BankTransactionSearchWorkspace";
            this.BankTransactionSearchWorkspace.Size = new System.Drawing.Size(250, 416);
            this.BankTransactionSearchWorkspace.TabIndex = 1;
            this.BankTransactionSearchWorkspace.Text = "Search";
            // 
            // MainWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutContainerControl1);
            this.Controls.Add(this.BankTransactionToolbarWorkspace);
            this.Name = "MainWorkspace";
            this.Size = new System.Drawing.Size(809, 471);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.layoutContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtab)).EndInit();
            this.xtab.ResumeLayout(false);
            this.tabNextJobs.ResumeLayout(false);
            this.dpSearch.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace BankTransactionToolbarWorkspace;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl layoutContainerControl1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace BankTransactionListWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace BankTransactionSearchWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTab.XtraTabControl xtab;
        private DevExpress.XtraTab.XtraTabPage tabNextJobs;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace BankTransactionEditWorkspace;
    }
}
