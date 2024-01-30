namespace ICP.FAM.UI.ReleaseBL
{
    partial class ReleaseBLMainWorkspace
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
            this.ToolbarWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.layoutContainerControl1 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.xtab = new DevExpress.XtraTab.XtraTabControl();
            this.tabNextJobs = new DevExpress.XtraTab.XtraTabPage();
            this.NextJobsWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabMemo = new DevExpress.XtraTab.XtraTabPage();
            this.EventWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabBillList = new DevExpress.XtraTab.XtraTabPage();
            this.BillListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabDebt = new DevExpress.XtraTab.XtraTabPage();
            this.DebtWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabContact = new DevExpress.XtraTab.XtraTabPage();
            this.ContactListspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.DocumentListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.SearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.FaxMailEDIListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.layoutContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtab)).BeginInit();
            this.xtab.SuspendLayout();
            this.tabNextJobs.SuspendLayout();
            this.tabMemo.SuspendLayout();
            this.tabBillList.SuspendLayout();
            this.tabDebt.SuspendLayout();
            this.tabContact.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.dpSearch.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolbarWorkspace
            // 
            this.ToolbarWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolbarWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ToolbarWorkspace.Name = "ToolbarWorkspace";
            this.ToolbarWorkspace.Size = new System.Drawing.Size(809, 27);
            this.ToolbarWorkspace.TabIndex = 0;
            this.ToolbarWorkspace.Text = "deckWorkspace1";
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
            this.splitContainerControl1.Collapsed = true;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(256, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.ListWorkspace);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.xtab);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(553, 444);
            this.splitContainerControl1.SplitterPosition = 233;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ListWorkspace
            // 
            this.ListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ListWorkspace.Name = "ListWorkspace";
            this.ListWorkspace.Size = new System.Drawing.Size(553, 205);
            this.ListWorkspace.TabIndex = 0;
            this.ListWorkspace.Text = "deckWorkspace1";
            // 
            // xtab
            // 
            this.xtab.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.xtab.Appearance.Options.UseBackColor = true;
            this.xtab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtab.Location = new System.Drawing.Point(0, 0);
            this.xtab.Name = "xtab";
            this.xtab.SelectedTabPage = this.tabNextJobs;
            this.xtab.Size = new System.Drawing.Size(553, 233);
            this.xtab.TabIndex = 0;
            this.xtab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabMemo,
            this.tabNextJobs,
            this.tabBillList,
            this.tabDebt,
            this.tabContact,
            this.xtraTabPage1,
            this.xtraTabPage2});
            this.xtab.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtab_SelectedPageChanged);
            // 
            // tabNextJobs
            // 
            this.tabNextJobs.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabNextJobs.Appearance.PageClient.Options.UseBackColor = true;
            this.tabNextJobs.Controls.Add(this.NextJobsWorkspace);
            this.tabNextJobs.Name = "tabNextJobs";
            this.tabNextJobs.Size = new System.Drawing.Size(546, 203);
            this.tabNextJobs.Text = "NextJobs";
            // 
            // NextJobsWorkspace
            // 
            this.NextJobsWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.NextJobsWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NextJobsWorkspace.Location = new System.Drawing.Point(0, 0);
            this.NextJobsWorkspace.Name = "NextJobsWorkspace";
            this.NextJobsWorkspace.Size = new System.Drawing.Size(546, 203);
            this.NextJobsWorkspace.TabIndex = 1;
            this.NextJobsWorkspace.Text = "deckWorkspace1";
            // 
            // tabMemo
            // 
            this.tabMemo.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabMemo.Appearance.PageClient.Options.UseBackColor = true;
            this.tabMemo.Controls.Add(this.EventWorkspace);
            this.tabMemo.Name = "tabMemo";
            this.tabMemo.Size = new System.Drawing.Size(546, 203);
            this.tabMemo.Text = "Event";
            // 
            // EventWorkspace
            // 
            this.EventWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.EventWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EventWorkspace.Location = new System.Drawing.Point(0, 0);
            this.EventWorkspace.Name = "EventWorkspace";
            this.EventWorkspace.Size = new System.Drawing.Size(546, 203);
            this.EventWorkspace.TabIndex = 1;
            this.EventWorkspace.Text = "deckWorkspace1";
            // 
            // tabBillList
            // 
            this.tabBillList.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabBillList.Appearance.PageClient.Options.UseBackColor = true;
            this.tabBillList.Controls.Add(this.BillListWorkspace);
            this.tabBillList.Name = "tabBillList";
            this.tabBillList.Size = new System.Drawing.Size(546, 203);
            this.tabBillList.Text = "Bill List";
            // 
            // BillListWorkspace
            // 
            this.BillListWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.BillListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BillListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.BillListWorkspace.Name = "BillListWorkspace";
            this.BillListWorkspace.Size = new System.Drawing.Size(546, 203);
            this.BillListWorkspace.TabIndex = 2;
            this.BillListWorkspace.Text = "deckWorkspace1";
            // 
            // tabDebt
            // 
            this.tabDebt.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabDebt.Appearance.PageClient.Options.UseBackColor = true;
            this.tabDebt.Controls.Add(this.DebtWorkspace);
            this.tabDebt.Name = "tabDebt";
            this.tabDebt.Size = new System.Drawing.Size(546, 203);
            this.tabDebt.Text = "Debt";
            // 
            // DebtWorkspace
            // 
            this.DebtWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.DebtWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DebtWorkspace.Location = new System.Drawing.Point(0, 0);
            this.DebtWorkspace.Name = "DebtWorkspace";
            this.DebtWorkspace.Size = new System.Drawing.Size(546, 203);
            this.DebtWorkspace.TabIndex = 1;
            this.DebtWorkspace.Text = "deckWorkspace1";
            // 
            // tabContact
            // 
            this.tabContact.Controls.Add(this.ContactListspace);
            this.tabContact.Name = "tabContact";
            this.tabContact.Size = new System.Drawing.Size(546, 203);
            this.tabContact.Text = "ContactEmailList";
            // 
            // ContactListspace
            // 
            this.ContactListspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.ContactListspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContactListspace.Location = new System.Drawing.Point(0, 0);
            this.ContactListspace.Name = "ContactListspace";
            this.ContactListspace.Size = new System.Drawing.Size(546, 203);
            this.ContactListspace.TabIndex = 3;
            this.ContactListspace.Text = "deckWorkspace1";
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.DocumentListWorkspace);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(546, 203);
            this.xtraTabPage1.Text = "Document List";
            // 
            // DocumentListWorkspace
            // 
            this.DocumentListWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.DocumentListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.DocumentListWorkspace.Name = "DocumentListWorkspace";
            this.DocumentListWorkspace.Size = new System.Drawing.Size(546, 203);
            this.DocumentListWorkspace.TabIndex = 4;
            this.DocumentListWorkspace.Text = "Document List";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.FaxMailEDIListWorkspace);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(546, 203);
            this.xtraTabPage2.Text = "EmailList";
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
            this.dockPanel1_Container.Controls.Add(this.SearchWorkspace);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(250, 416);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // SearchWorkspace
            // 
            this.SearchWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.SearchWorkspace.Name = "SearchWorkspace";
            this.SearchWorkspace.Size = new System.Drawing.Size(250, 416);
            this.SearchWorkspace.TabIndex = 1;
            this.SearchWorkspace.Text = "deckWorkspace1";
            // 
            // FaxMailEDIListWorkspace
            // 
            this.FaxMailEDIListWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.FaxMailEDIListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FaxMailEDIListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.FaxMailEDIListWorkspace.Name = "FaxMailEDIListWorkspace";
            this.FaxMailEDIListWorkspace.Size = new System.Drawing.Size(546, 203);
            this.FaxMailEDIListWorkspace.TabIndex = 5;
            this.FaxMailEDIListWorkspace.Text = "Document List";
            // 
            // ReleaseBLMainWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutContainerControl1);
            this.Controls.Add(this.ToolbarWorkspace);
            this.Name = "ReleaseBLMainWorkspace";
            this.Size = new System.Drawing.Size(809, 471);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.layoutContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtab)).EndInit();
            this.xtab.ResumeLayout(false);
            this.tabNextJobs.ResumeLayout(false);
            this.tabMemo.ResumeLayout(false);
            this.tabBillList.ResumeLayout(false);
            this.tabDebt.ResumeLayout(false);
            this.tabContact.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.dpSearch.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ToolbarWorkspace;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl layoutContainerControl1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SearchWorkspace;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTab.XtraTabControl xtab;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraTab.XtraTabPage tabMemo;
        private DevExpress.XtraTab.XtraTabPage tabNextJobs;
        private DevExpress.XtraTab.XtraTabPage tabBillList;
        private DevExpress.XtraTab.XtraTabPage tabDebt;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace EventWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace NextJobsWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace DebtWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace BillListWorkspace;
        private DevExpress.XtraTab.XtraTabPage tabContact;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ContactListspace;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace DocumentListWorkspace;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace FaxMailEDIListWorkspace;
    }
}
