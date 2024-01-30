namespace ICP.FRM.UI.OceanPrice
{
    partial class OPMainWorkspace
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
            this.panel1 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.xtabMain = new DevExpress.XtraTab.XtraTabControl();
            this.pageContract = new DevExpress.XtraTab.XtraTabPage();
            this.ContractWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.pageBasePortRates = new DevExpress.XtraTab.XtraTabPage();
            this.BPRWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.pageArbitraryRates = new DevExpress.XtraTab.XtraTabPage();
            this.ARWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.pageAdditionalFees = new DevExpress.XtraTab.XtraTabPage();
            this.AFWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.pagePermissions = new DevExpress.XtraTab.XtraTabPage();
            this.PermissionsWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.pageAttachment = new DevExpress.XtraTab.XtraTabPage();
            this.AttachmentWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.SearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.ToolBarWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtabMain)).BeginInit();
            this.xtabMain.SuspendLayout();
            this.pageContract.SuspendLayout();
            this.pageBasePortRates.SuspendLayout();
            this.pageArbitraryRates.SuspendLayout();
            this.pageAdditionalFees.SuspendLayout();
            this.pagePermissions.SuspendLayout();
            this.pageAttachment.SuspendLayout();
            this.dpSearch.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainerControl1);
            this.panel1.Controls.Add(this.dpSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1121, 599);
            this.panel1.TabIndex = 0;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(220, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.ListWorkspace);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.xtabMain);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(901, 599);
            this.splitContainerControl1.SplitterPosition = 135;
            this.splitContainerControl1.TabIndex = 18;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ListWorkspace
            // 
            this.ListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ListWorkspace.Name = "ListWorkspace";
            this.ListWorkspace.Size = new System.Drawing.Size(901, 135);
            this.ListWorkspace.TabIndex = 10;
            this.ListWorkspace.Text = "deckWorkspace1";
            // 
            // xtabMain
            // 
            this.xtabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtabMain.Location = new System.Drawing.Point(0, 0);
            this.xtabMain.Name = "xtabMain";
            this.xtabMain.SelectedTabPage = this.pageContract;
            this.xtabMain.Size = new System.Drawing.Size(901, 458);
            this.xtabMain.TabIndex = 0;
            this.xtabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageContract,
            this.pageBasePortRates,
            this.pageArbitraryRates,
            this.pageAdditionalFees,
            this.pagePermissions,
            this.pageAttachment});
            this.xtabMain.SelectedPageChanging += new DevExpress.XtraTab.TabPageChangingEventHandler(this.xtabMain_SelectedPageChanging);
            this.xtabMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtabMain_SelectedPageChanged);
            // 
            // pageContract
            // 
            this.pageContract.Controls.Add(this.ContractWorkspace);
            this.pageContract.Name = "pageContract";
            this.pageContract.Size = new System.Drawing.Size(894, 428);
            this.pageContract.Text = "Contract";
            // 
            // ContractWorkspace
            // 
            this.ContractWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.ContractWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContractWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ContractWorkspace.Name = "ContractWorkspace";
            this.ContractWorkspace.Size = new System.Drawing.Size(894, 428);
            this.ContractWorkspace.TabIndex = 11;
            this.ContractWorkspace.Text = "deckWorkspace1";
            // 
            // pageBasePortRates
            // 
            this.pageBasePortRates.Controls.Add(this.BPRWorkspace);
            this.pageBasePortRates.Name = "pageBasePortRates";
            this.pageBasePortRates.Size = new System.Drawing.Size(894, 428);
            this.pageBasePortRates.Text = "BasePortRates";
            // 
            // BPRWorkspace
            // 
            this.BPRWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.BPRWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BPRWorkspace.Location = new System.Drawing.Point(0, 0);
            this.BPRWorkspace.Name = "BPRWorkspace";
            this.BPRWorkspace.Size = new System.Drawing.Size(894, 428);
            this.BPRWorkspace.TabIndex = 11;
            this.BPRWorkspace.Text = "deckWorkspace1";
            // 
            // pageArbitraryRates
            // 
            this.pageArbitraryRates.Controls.Add(this.ARWorkspace);
            this.pageArbitraryRates.Name = "pageArbitraryRates";
            this.pageArbitraryRates.Size = new System.Drawing.Size(894, 428);
            this.pageArbitraryRates.Text = "Arbitrary Rates";
            // 
            // ARWorkspace
            // 
            this.ARWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.ARWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ARWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ARWorkspace.Name = "ARWorkspace";
            this.ARWorkspace.Size = new System.Drawing.Size(894, 428);
            this.ARWorkspace.TabIndex = 11;
            this.ARWorkspace.Text = "deckWorkspace1";
            // 
            // pageAdditionalFees
            // 
            this.pageAdditionalFees.Controls.Add(this.AFWorkspace);
            this.pageAdditionalFees.Name = "pageAdditionalFees";
            this.pageAdditionalFees.Size = new System.Drawing.Size(894, 428);
            this.pageAdditionalFees.Text = "Additional Fees";
            // 
            // AFWorkspace
            // 
            this.AFWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.AFWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AFWorkspace.Location = new System.Drawing.Point(0, 0);
            this.AFWorkspace.Name = "AFWorkspace";
            this.AFWorkspace.Size = new System.Drawing.Size(894, 428);
            this.AFWorkspace.TabIndex = 11;
            this.AFWorkspace.Text = "deckWorkspace1";
            // 
            // pagePermissions
            // 
            this.pagePermissions.Controls.Add(this.PermissionsWorkspace);
            this.pagePermissions.Name = "pagePermissions";
            this.pagePermissions.Size = new System.Drawing.Size(894, 428);
            this.pagePermissions.Text = "Permissions";
            // 
            // PermissionsWorkspace
            // 
            this.PermissionsWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.PermissionsWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PermissionsWorkspace.Location = new System.Drawing.Point(0, 0);
            this.PermissionsWorkspace.Name = "PermissionsWorkspace";
            this.PermissionsWorkspace.Size = new System.Drawing.Size(894, 428);
            this.PermissionsWorkspace.TabIndex = 11;
            this.PermissionsWorkspace.Text = "deckWorkspace1";
            // 
            // pageAttachment
            // 
            this.pageAttachment.Controls.Add(this.AttachmentWorkspace);
            this.pageAttachment.Name = "pageAttachment";
            this.pageAttachment.Size = new System.Drawing.Size(894, 428);
            this.pageAttachment.Text = "Attachment";
            // 
            // AttachmentWorkspace
            // 
            this.AttachmentWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.AttachmentWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AttachmentWorkspace.Location = new System.Drawing.Point(0, 0);
            this.AttachmentWorkspace.Name = "AttachmentWorkspace";
            this.AttachmentWorkspace.Size = new System.Drawing.Size(894, 428);
            this.AttachmentWorkspace.TabIndex = 11;
            this.AttachmentWorkspace.Text = "deckWorkspace1";
            // 
            // dpSearch
            // 
            this.dpSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpSearch.Appearance.Options.UseBackColor = true;
            this.dpSearch.Controls.Add(this.dockPanel1_Container);
            this.dpSearch.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpSearch.ID = new System.Guid("53eeb54d-d0c0-492d-9990-38a1d8dbba3b");
            this.dpSearch.Location = new System.Drawing.Point(0, 0);
            this.dpSearch.Name = "dpSearch";
            this.dpSearch.OriginalSize = new System.Drawing.Size(220, 200);
            this.dpSearch.Size = new System.Drawing.Size(220, 599);
            this.dpSearch.Text = "Search";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.SearchWorkspace);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(214, 571);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // SearchWorkspace
            // 
            this.SearchWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.SearchWorkspace.Name = "SearchWorkspace";
            this.SearchWorkspace.Size = new System.Drawing.Size(214, 571);
            this.SearchWorkspace.TabIndex = 1;
            this.SearchWorkspace.Text = "deckWorkspace1";
            // 
            // ToolBarWorkspace
            // 
            this.ToolBarWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolBarWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ToolBarWorkspace.Name = "ToolBarWorkspace";
            this.ToolBarWorkspace.Size = new System.Drawing.Size(1121, 30);
            this.ToolBarWorkspace.TabIndex = 13;
            this.ToolBarWorkspace.Text = "deckWorkspace1";
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
            // OPMainWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ToolBarWorkspace);
            this.IsMultiLanguage = false;
            this.Name = "OPMainWorkspace";
            this.Size = new System.Drawing.Size(1121, 629);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtabMain)).EndInit();
            this.xtabMain.ResumeLayout(false);
            this.pageContract.ResumeLayout(false);
            this.pageBasePortRates.ResumeLayout(false);
            this.pageArbitraryRates.ResumeLayout(false);
            this.pageAdditionalFees.ResumeLayout(false);
            this.pagePermissions.ResumeLayout(false);
            this.pageAttachment.ResumeLayout(false);
            this.dpSearch.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl panel1;
        
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        
        public  DevExpress.XtraTab.XtraTabControl xtabMain;
        private DevExpress.XtraTab.XtraTabPage pageContract;
        private DevExpress.XtraTab.XtraTabPage pageBasePortRates;
        private DevExpress.XtraTab.XtraTabPage pageArbitraryRates;
        private DevExpress.XtraTab.XtraTabPage pageAdditionalFees;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraTab.XtraTabPage pagePermissions;
        private DevExpress.XtraTab.XtraTabPage pageAttachment;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ToolBarWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SearchWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ContractWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace BPRWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ARWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace AFWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace PermissionsWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace AttachmentWorkspace;
    }
}
