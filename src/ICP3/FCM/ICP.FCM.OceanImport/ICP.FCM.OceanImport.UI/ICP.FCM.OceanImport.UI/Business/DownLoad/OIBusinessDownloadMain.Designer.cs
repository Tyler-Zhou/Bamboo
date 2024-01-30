namespace ICP.FCM.OceanImport.UI
{
    partial class OIBusinessDownloadMain
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
            this.panel1 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.xtab = new DevExpress.XtraTab.XtraTabControl();
            this.tabDocumentList = new DevExpress.XtraTab.XtraTabPage();
            this.DocumentListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.OperationPartWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabFeeList = new DevExpress.XtraTab.XtraTabPage();
            this.FeeWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabAcceptList = new DevExpress.XtraTab.XtraTabPage();
            this.AcceptWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.ListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.SearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtab)).BeginInit();
            this.xtab.SuspendLayout();
            this.tabDocumentList.SuspendLayout();
            this.tabFeeList.SuspendLayout();
            this.tabAcceptList.SuspendLayout();
            this.dpSearch.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolbarWorkspace
            // 
            this.ToolbarWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolbarWorkspace.Location = new System.Drawing.Point(2, 2);
            this.ToolbarWorkspace.Name = "ToolbarWorkspace";
            this.ToolbarWorkspace.Size = new System.Drawing.Size(700, 27);
            this.ToolbarWorkspace.TabIndex = 0;
            this.ToolbarWorkspace.Text = "deckWorkspace1";
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this.panel1;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpSearch});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlMain);
            this.panel1.Controls.Add(this.dpSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(913, 589);
            this.panel1.TabIndex = 2;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.xtab);
            this.pnlMain.Controls.Add(this.splitterControl1);
            this.pnlMain.Controls.Add(this.ListWorkspace);
            this.pnlMain.Controls.Add(this.ToolbarWorkspace);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(209, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(704, 589);
            this.pnlMain.TabIndex = 2;
            // 
            // xtab
            // 
            this.xtab.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.xtab.Appearance.Options.UseBackColor = true;
            this.xtab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtab.Location = new System.Drawing.Point(2, 372);
            this.xtab.Name = "xtab";
            this.xtab.SelectedTabPage = this.tabDocumentList;
            this.xtab.Size = new System.Drawing.Size(700, 215);
            this.xtab.TabIndex = 4;
            this.xtab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabFeeList,
            this.tabDocumentList,
            this.tabAcceptList});
            // 
            // tabDocumentList
            // 
            this.tabDocumentList.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabDocumentList.Appearance.PageClient.Options.UseBackColor = true;
            this.tabDocumentList.Controls.Add(this.DocumentListWorkspace);
            this.tabDocumentList.Controls.Add(this.OperationPartWorkspace);
            this.tabDocumentList.Name = "tabDocumentList";
            this.tabDocumentList.Size = new System.Drawing.Size(693, 185);
            this.tabDocumentList.Text = "Document List";
            // 
            // DocumentListWorkspace
            // 
            this.DocumentListWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.DocumentListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentListWorkspace.Location = new System.Drawing.Point(0, 40);
            this.DocumentListWorkspace.Name = "DocumentListWorkspace";
            this.DocumentListWorkspace.Size = new System.Drawing.Size(693, 145);
            this.DocumentListWorkspace.TabIndex = 2;
            this.DocumentListWorkspace.Text = "DocumentListWorkSpace";
            // 
            // OperationPartWorkspace
            // 
            this.OperationPartWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.OperationPartWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.OperationPartWorkspace.Location = new System.Drawing.Point(0, 0);
            this.OperationPartWorkspace.Name = "OperationPartWorkspace";
            this.OperationPartWorkspace.Size = new System.Drawing.Size(693, 40);
            this.OperationPartWorkspace.TabIndex = 2;
            this.OperationPartWorkspace.Text = "OperationPartWorkspace";
            // 
            // tabFeeList
            // 
            this.tabFeeList.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabFeeList.Appearance.PageClient.Options.UseBackColor = true;
            this.tabFeeList.Controls.Add(this.FeeWorkspace);
            this.tabFeeList.Name = "tabFeeList";
            this.tabFeeList.Size = new System.Drawing.Size(693, 185);
            this.tabFeeList.Text = "Fee List";
            // 
            // FeeWorkspace
            // 
            this.FeeWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.FeeWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FeeWorkspace.Location = new System.Drawing.Point(0, 0);
            this.FeeWorkspace.Name = "FeeWorkspace";
            this.FeeWorkspace.Size = new System.Drawing.Size(693, 185);
            this.FeeWorkspace.TabIndex = 3;
            this.FeeWorkspace.Text = "FeeWorkspace";
            // 
            // tabAcceptList
            // 
            this.tabAcceptList.Controls.Add(this.AcceptWorkspace);
            this.tabAcceptList.Name = "tabAcceptList";
            this.tabAcceptList.Size = new System.Drawing.Size(693, 185);
            this.tabAcceptList.Text = "dispatch/revise history";
            // 
            // AcceptWorkspace
            // 
            this.AcceptWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.AcceptWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AcceptWorkspace.Location = new System.Drawing.Point(0, 0);
            this.AcceptWorkspace.Name = "AcceptWorkspace";
            this.AcceptWorkspace.Size = new System.Drawing.Size(693, 185);
            this.AcceptWorkspace.TabIndex = 4;
            this.AcceptWorkspace.Text = "AcceptWorkspace";
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(2, 366);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(700, 6);
            this.splitterControl1.TabIndex = 5;
            this.splitterControl1.TabStop = false;
            // 
            // ListWorkspace
            // 
            this.ListWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.ListWorkspace.Location = new System.Drawing.Point(2, 29);
            this.ListWorkspace.Name = "ListWorkspace";
            this.ListWorkspace.Size = new System.Drawing.Size(700, 337);
            this.ListWorkspace.TabIndex = 1;
            this.ListWorkspace.Text = "deckWorkspace4";
            // 
            // dpSearch
            // 
            this.dpSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpSearch.Appearance.Options.UseBackColor = true;
            this.dpSearch.Controls.Add(this.dockPanel1_Container);
            this.dpSearch.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpSearch.ID = new System.Guid("43e61d70-cc84-4c5f-ab28-b5b19a125190");
            this.dpSearch.Location = new System.Drawing.Point(0, 0);
            this.dpSearch.Name = "dpSearch";
            this.dpSearch.OriginalSize = new System.Drawing.Size(209, 200);
            this.dpSearch.Size = new System.Drawing.Size(209, 589);
            this.dpSearch.Text = "Search";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.SearchWorkspace);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(203, 561);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // SearchWorkspace
            // 
            this.SearchWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.SearchWorkspace.Name = "SearchWorkspace";
            this.SearchWorkspace.Size = new System.Drawing.Size(203, 561);
            this.SearchWorkspace.TabIndex = 0;
            this.SearchWorkspace.Text = "deckWorkspace1";
            // 
            // OIBusinessDownloadMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "OIBusinessDownloadMain";
            this.Size = new System.Drawing.Size(913, 589);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtab)).EndInit();
            this.xtab.ResumeLayout(false);
            this.tabDocumentList.ResumeLayout(false);
            this.tabFeeList.ResumeLayout(false);
            this.tabAcceptList.ResumeLayout(false);
            this.dpSearch.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ToolbarWorkspace;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkspace;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl panel1;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SearchWorkspace;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraTab.XtraTabControl xtab;
        private DevExpress.XtraTab.XtraTabPage tabDocumentList;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace DocumentListWorkspace;
        private DevExpress.XtraTab.XtraTabPage tabFeeList;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace FeeWorkspace;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace OperationPartWorkspace;
        private DevExpress.XtraTab.XtraTabPage tabAcceptList;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace AcceptWorkspace;
    }
}
