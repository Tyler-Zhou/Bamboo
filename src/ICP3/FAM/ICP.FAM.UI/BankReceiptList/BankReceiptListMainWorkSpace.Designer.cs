namespace ICP.FAM.UI.BankReceiptList
{
    partial class BankReceiptListMainWorkSpace
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.BankReceiptList_ToolbarWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.layoutContainerControl1 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.BankReceiptList_ListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabSubcontainer = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageDocument = new DevExpress.XtraTab.XtraTabPage();
            this.BankReceiptList_DocumentListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.BankReceiptList_SearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.xtab = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.DocumentListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.layoutContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabSubcontainer)).BeginInit();
            this.tabSubcontainer.SuspendLayout();
            this.tabPageDocument.SuspendLayout();
            this.dpSearch.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtab)).BeginInit();
            this.xtab.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 600);
            this.panel1.TabIndex = 6;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.BankReceiptList_ToolbarWorkspace);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.layoutContainerControl1);
            this.splitContainer1.Panel2.Controls.Add(this.xtab);
            this.splitContainer1.Size = new System.Drawing.Size(800, 600);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 8;
            // 
            // BankReceiptList_ToolbarWorkspace
            // 
            this.BankReceiptList_ToolbarWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BankReceiptList_ToolbarWorkspace.Location = new System.Drawing.Point(0, 0);
            this.BankReceiptList_ToolbarWorkspace.Name = "BankReceiptList_ToolbarWorkspace";
            this.BankReceiptList_ToolbarWorkspace.Size = new System.Drawing.Size(800, 25);
            this.BankReceiptList_ToolbarWorkspace.TabIndex = 6;
            this.BankReceiptList_ToolbarWorkspace.Text = "deckWorkspace1";
            // 
            // layoutContainerControl1
            // 
            this.layoutContainerControl1.Controls.Add(this.splitContainerControl1);
            this.layoutContainerControl1.Controls.Add(this.dpSearch);
            this.layoutContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutContainerControl1.Name = "layoutContainerControl1";
            this.layoutContainerControl1.Size = new System.Drawing.Size(800, 574);
            this.layoutContainerControl1.TabIndex = 4;
            this.layoutContainerControl1.Text = "layoutContainerControl1";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(230, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.BankReceiptList_ListWorkspace);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.tabSubcontainer);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(570, 574);
            this.splitContainerControl1.SplitterPosition = 222;
            this.splitContainerControl1.TabIndex = 6;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // BankReceiptList_ListWorkspace
            // 
            this.BankReceiptList_ListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BankReceiptList_ListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.BankReceiptList_ListWorkspace.Name = "BankReceiptList_ListWorkspace";
            this.BankReceiptList_ListWorkspace.Size = new System.Drawing.Size(570, 346);
            this.BankReceiptList_ListWorkspace.TabIndex = 1;
            this.BankReceiptList_ListWorkspace.Text = "deckWorkspace4";
            // 
            // tabSubcontainer
            // 
            this.tabSubcontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSubcontainer.Location = new System.Drawing.Point(0, 0);
            this.tabSubcontainer.Name = "tabSubcontainer";
            this.tabSubcontainer.SelectedTabPage = this.tabPageDocument;
            this.tabSubcontainer.Size = new System.Drawing.Size(570, 222);
            this.tabSubcontainer.TabIndex = 7;
            this.tabSubcontainer.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageDocument});
            // 
            // tabPageDocument
            // 
            this.tabPageDocument.Controls.Add(this.BankReceiptList_DocumentListWorkspace);
            this.tabPageDocument.Name = "tabPageDocument";
            this.tabPageDocument.Size = new System.Drawing.Size(563, 192);
            this.tabPageDocument.Text = "Document";
            // 
            // BankReceiptList_DocumentListWorkspace
            // 
            this.BankReceiptList_DocumentListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BankReceiptList_DocumentListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.BankReceiptList_DocumentListWorkspace.Name = "BankReceiptList_DocumentListWorkspace";
            this.BankReceiptList_DocumentListWorkspace.Size = new System.Drawing.Size(563, 192);
            this.BankReceiptList_DocumentListWorkspace.TabIndex = 7;
            this.BankReceiptList_DocumentListWorkspace.Text = "deckWorkspace4";
            // 
            // dpSearch
            // 
            this.dpSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpSearch.Appearance.Options.UseBackColor = true;
            this.dpSearch.Controls.Add(this.dockPanel1_Container);
            this.dpSearch.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpSearch.ID = new System.Guid("ff37cc96-0c82-4ae1-88a7-639795c16054");
            this.dpSearch.Location = new System.Drawing.Point(0, 0);
            this.dpSearch.Name = "dpSearch";
            this.dpSearch.OriginalSize = new System.Drawing.Size(230, 200);
            this.dpSearch.Size = new System.Drawing.Size(230, 574);
            this.dpSearch.Text = "查询";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.BankReceiptList_SearchWorkspace);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(224, 546);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // BankReceiptList_SearchWorkspace
            // 
            this.BankReceiptList_SearchWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BankReceiptList_SearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.BankReceiptList_SearchWorkspace.Name = "BankReceiptList_SearchWorkspace";
            this.BankReceiptList_SearchWorkspace.Size = new System.Drawing.Size(224, 546);
            this.BankReceiptList_SearchWorkspace.TabIndex = 0;
            this.BankReceiptList_SearchWorkspace.Text = "deckWorkspace1";
            // 
            // xtab
            // 
            this.xtab.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.xtab.Appearance.Options.UseBackColor = true;
            this.xtab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtab.Location = new System.Drawing.Point(0, 0);
            this.xtab.Name = "xtab";
            this.xtab.SelectedTabPage = this.xtraTabPage1;
            this.xtab.Size = new System.Drawing.Size(800, 574);
            this.xtab.TabIndex = 2;
            this.xtab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.DocumentListWorkspace);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(793, 544);
            this.xtraTabPage1.Text = "Document List";
            // 
            // DocumentListWorkspace
            // 
            this.DocumentListWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.DocumentListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.DocumentListWorkspace.Name = "DocumentListWorkspace";
            this.DocumentListWorkspace.Size = new System.Drawing.Size(793, 544);
            this.DocumentListWorkspace.TabIndex = 4;
            this.DocumentListWorkspace.Text = "Document List";
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
            // BankReceiptListMainWorkSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "BankReceiptListMainWorkSpace";
            this.Size = new System.Drawing.Size(800, 600);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.layoutContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabSubcontainer)).EndInit();
            this.tabSubcontainer.ResumeLayout(false);
            this.tabPageDocument.ResumeLayout(false);
            this.dpSearch.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtab)).EndInit();
            this.xtab.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace BankReceiptList_ToolbarWorkspace;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl layoutContainerControl1;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace BankReceiptList_SearchWorkspace;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraTab.XtraTabControl xtab;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace DocumentListWorkspace;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace BankReceiptList_ListWorkspace;
        private DevExpress.XtraTab.XtraTabControl tabSubcontainer;
        private DevExpress.XtraTab.XtraTabPage tabPageDocument;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace BankReceiptList_DocumentListWorkspace;
        //private DevExpress.XtraRichEdit.RichEditControl richEditControl1;
    }
}
