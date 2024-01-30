namespace ICP.FCM.OceanExport.UI.BL
{
    partial class OEBLMainWorkspace
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
            this.FastSearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.xtab = new DevExpress.XtraTab.XtraTabControl();
            this.tabMemoList = new DevExpress.XtraTab.XtraTabPage();
            this.EventListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabFaxMailList = new DevExpress.XtraTab.XtraTabPage();
            this.FaxMailEDIListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabDocumentList = new DevExpress.XtraTab.XtraTabPage();
            this.DocumentListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabAcceptList = new DevExpress.XtraTab.XtraTabPage();
            this.AcceptWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabCargoTracking = new DevExpress.XtraTab.XtraTabPage();
            this.CargoTrackingWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.SearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.layoutContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtab)).BeginInit();
            this.xtab.SuspendLayout();
            this.tabMemoList.SuspendLayout();
            this.tabFaxMailList.SuspendLayout();
            this.tabDocumentList.SuspendLayout();
            this.tabAcceptList.SuspendLayout();
            this.tabCargoTracking.SuspendLayout();
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
            this.splitContainerControl1.Panel1.Controls.Add(this.FastSearchWorkspace);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.xtab);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(553, 444);
            this.splitContainerControl1.SplitterPosition = 265;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ListWorkspace
            // 
            this.ListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListWorkspace.Location = new System.Drawing.Point(0, 36);
            this.ListWorkspace.Name = "ListWorkspace";
            this.ListWorkspace.Size = new System.Drawing.Size(553, 137);
            this.ListWorkspace.TabIndex = 0;
            this.ListWorkspace.Text = "deckWorkspace1";
            // 
            // FastSearchWorkspace
            // 
            this.FastSearchWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.FastSearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.FastSearchWorkspace.Name = "FastSearchWorkspace";
            this.FastSearchWorkspace.Size = new System.Drawing.Size(553, 36);
            this.FastSearchWorkspace.TabIndex = 0;
            this.FastSearchWorkspace.Text = "deckWorkspace3";
            // 
            // xtab
            // 
            this.xtab.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.xtab.Appearance.Options.UseBackColor = true;
            this.xtab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtab.Location = new System.Drawing.Point(0, 0);
            this.xtab.Name = "xtab";
            this.xtab.SelectedTabPage = this.tabFaxMailList;
            this.xtab.Size = new System.Drawing.Size(553, 265);
            this.xtab.TabIndex = 0;
            this.xtab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabMemoList,
            this.tabFaxMailList,
            this.tabDocumentList,
            this.tabAcceptList,
            this.tabCargoTracking});
            this.xtab.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtab_SelectedPageChanged);
            // 
            // tabMemoList
            // 
            this.tabMemoList.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabMemoList.Appearance.PageClient.Options.UseBackColor = true;
            this.tabMemoList.Controls.Add(this.EventListWorkspace);
            this.tabMemoList.Name = "tabMemoList";
            this.tabMemoList.Size = new System.Drawing.Size(546, 235);
            this.tabMemoList.Text = "Event";
            // 
            // EventListWorkspace
            // 
            this.EventListWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.EventListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EventListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.EventListWorkspace.Name = "EventListWorkspace";
            this.EventListWorkspace.Size = new System.Drawing.Size(546, 235);
            this.EventListWorkspace.TabIndex = 1;
            this.EventListWorkspace.Text = "EventListWorkspace";
            // 
            // tabFaxMailList
            // 
            this.tabFaxMailList.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabFaxMailList.Appearance.PageClient.Options.UseBackColor = true;
            this.tabFaxMailList.Controls.Add(this.FaxMailEDIListWorkspace);
            this.tabFaxMailList.Name = "tabFaxMailList";
            this.tabFaxMailList.Size = new System.Drawing.Size(546, 235);
            this.tabFaxMailList.Text = "Fax/Mail/EDI";
            // 
            // FaxMailEDIListWorkspace
            // 
            this.FaxMailEDIListWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.FaxMailEDIListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FaxMailEDIListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.FaxMailEDIListWorkspace.Name = "FaxMailEDIListWorkspace";
            this.FaxMailEDIListWorkspace.Size = new System.Drawing.Size(546, 235);
            this.FaxMailEDIListWorkspace.TabIndex = 1;
            this.FaxMailEDIListWorkspace.Text = "FaxMailEDIListWorkspace";
            // 
            // tabDocumentList
            // 
            this.tabDocumentList.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabDocumentList.Appearance.PageClient.Options.UseBackColor = true;
            this.tabDocumentList.Controls.Add(this.DocumentListWorkspace);
            this.tabDocumentList.Name = "tabDocumentList";
            this.tabDocumentList.Size = new System.Drawing.Size(546, 235);
            this.tabDocumentList.Text = "Document List";
            // 
            // DocumentListWorkspace
            // 
            this.DocumentListWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.DocumentListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.DocumentListWorkspace.Name = "DocumentListWorkspace";
            this.DocumentListWorkspace.Size = new System.Drawing.Size(546, 235);
            this.DocumentListWorkspace.TabIndex = 2;
            this.DocumentListWorkspace.Text = "deckWorkspace1";
            // 
            // tabAcceptList
            // 
            this.tabAcceptList.Controls.Add(this.AcceptWorkspace);
            this.tabAcceptList.Name = "tabAcceptList";
            this.tabAcceptList.Size = new System.Drawing.Size(546, 235);
            this.tabAcceptList.Text = "Dispatch/Revise History";
            // 
            // AcceptWorkspace
            // 
            this.AcceptWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.AcceptWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AcceptWorkspace.Location = new System.Drawing.Point(0, 0);
            this.AcceptWorkspace.Name = "AcceptWorkspace";
            this.AcceptWorkspace.Size = new System.Drawing.Size(546, 235);
            this.AcceptWorkspace.TabIndex = 6;
            this.AcceptWorkspace.Text = "deckWorkspace1";
            // 
            // tabCargoTracking
            // 
            this.tabCargoTracking.Controls.Add(this.CargoTrackingWorkspace);
            this.tabCargoTracking.Name = "tabCargoTracking";
            this.tabCargoTracking.Size = new System.Drawing.Size(546, 235);
            this.tabCargoTracking.Text = "CargoTracking";
            // 
            // CargoTrackingWorkspace
            // 
            this.CargoTrackingWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.CargoTrackingWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CargoTrackingWorkspace.Location = new System.Drawing.Point(0, 0);
            this.CargoTrackingWorkspace.Name = "CargoTrackingWorkspace";
            this.CargoTrackingWorkspace.Size = new System.Drawing.Size(546, 235);
            this.CargoTrackingWorkspace.TabIndex = 7;
            this.CargoTrackingWorkspace.Text = "deckWorkspace1";
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
            // OEBLMainWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutContainerControl1);
            this.Controls.Add(this.ToolbarWorkspace);
            this.Name = "OEBLMainWorkspace";
            this.Size = new System.Drawing.Size(809, 471);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.layoutContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtab)).EndInit();
            this.xtab.ResumeLayout(false);
            this.tabMemoList.ResumeLayout(false);
            this.tabFaxMailList.ResumeLayout(false);
            this.tabDocumentList.ResumeLayout(false);
            this.tabAcceptList.ResumeLayout(false);
            this.tabCargoTracking.ResumeLayout(false);
            this.dpSearch.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ToolbarWorkspace;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl layoutContainerControl1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace FastSearchWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SearchWorkspace;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraTab.XtraTabControl xtab;
        private DevExpress.XtraTab.XtraTabPage tabMemoList;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace EventListWorkspace;
        private DevExpress.XtraTab.XtraTabPage tabFaxMailList;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace FaxMailEDIListWorkspace;
        private DevExpress.XtraTab.XtraTabPage tabDocumentList;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace DocumentListWorkspace;
        private DevExpress.XtraTab.XtraTabPage tabAcceptList;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace AcceptWorkspace;
        private DevExpress.XtraTab.XtraTabPage tabCargoTracking;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace CargoTrackingWorkspace;
    }
}
