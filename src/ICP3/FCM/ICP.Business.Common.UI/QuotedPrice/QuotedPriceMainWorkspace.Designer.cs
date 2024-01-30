namespace ICP.Business.Common.UI.QuotedPrice
{
    partial class QuotedPriceMainWorkspace
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dockManager2 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.panel1 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ListPartWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.FastSearchPartWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.xtab = new DevExpress.XtraTab.XtraTabControl();
            this.tabMemoList = new DevExpress.XtraTab.XtraTabPage();
            this.RatesListPartWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabFaxMailEDIList = new DevExpress.XtraTab.XtraTabPage();
            this.CommunicationPartWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabDocumentList = new DevExpress.XtraTab.XtraTabPage();
            this.DocumentListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.SearchPartWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.ToolBarPartWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtab)).BeginInit();
            this.xtab.SuspendLayout();
            this.tabMemoList.SuspendLayout();
            this.tabFaxMailEDIList.SuspendLayout();
            this.tabDocumentList.SuspendLayout();
            this.dpSearch.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager2
            // 
            this.dockManager2.Form = this.panel1;
            this.dockManager2.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpSearch});
            this.dockManager2.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainerControl1);
            this.panel1.Controls.Add(this.dpSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1056, 654);
            this.panel1.TabIndex = 4;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(260, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.ListPartWorkspace);
            this.splitContainerControl1.Panel1.Controls.Add(this.FastSearchPartWorkspace);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.xtab);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(796, 654);
            this.splitContainerControl1.SplitterPosition = 363;
            this.splitContainerControl1.TabIndex = 4;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ListPartWorkspace
            // 
            this.ListPartWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListPartWorkspace.Location = new System.Drawing.Point(0, 36);
            this.ListPartWorkspace.Name = "ListPartWorkspace";
            this.ListPartWorkspace.Size = new System.Drawing.Size(796, 327);
            this.ListPartWorkspace.TabIndex = 7;
            this.ListPartWorkspace.Text = "ListPartWorkspace";
            // 
            // FastSearchPartWorkspace
            // 
            this.FastSearchPartWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.FastSearchPartWorkspace.Location = new System.Drawing.Point(0, 0);
            this.FastSearchPartWorkspace.Name = "FastSearchPartWorkspace";
            this.FastSearchPartWorkspace.Size = new System.Drawing.Size(796, 36);
            this.FastSearchPartWorkspace.TabIndex = 6;
            this.FastSearchPartWorkspace.Text = "FastSearchPartWorkspace";
            // 
            // xtab
            // 
            this.xtab.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.xtab.Appearance.Options.UseBackColor = true;
            this.xtab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtab.Location = new System.Drawing.Point(0, 0);
            this.xtab.Name = "xtab";
            this.xtab.SelectedTabPage = this.tabMemoList;
            this.xtab.Size = new System.Drawing.Size(796, 285);
            this.xtab.TabIndex = 2;
            this.xtab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabMemoList,
            this.tabFaxMailEDIList,
            this.tabDocumentList});
            // 
            // tabMemoList
            // 
            this.tabMemoList.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabMemoList.Appearance.PageClient.Options.UseBackColor = true;
            this.tabMemoList.Controls.Add(this.RatesListPartWorkspace);
            this.tabMemoList.Name = "tabMemoList";
            this.tabMemoList.Size = new System.Drawing.Size(789, 255);
            this.tabMemoList.Text = "Rates";
            // 
            // RatesListPartWorkspace
            // 
            this.RatesListPartWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.RatesListPartWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RatesListPartWorkspace.Location = new System.Drawing.Point(0, 0);
            this.RatesListPartWorkspace.Name = "RatesListPartWorkspace";
            this.RatesListPartWorkspace.Size = new System.Drawing.Size(789, 255);
            this.RatesListPartWorkspace.TabIndex = 1;
            this.RatesListPartWorkspace.Text = "RatesListPartWorkspace";
            // 
            // tabFaxMailEDIList
            // 
            this.tabFaxMailEDIList.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabFaxMailEDIList.Appearance.PageClient.Options.UseBackColor = true;
            this.tabFaxMailEDIList.Controls.Add(this.CommunicationPartWorkspace);
            this.tabFaxMailEDIList.Name = "tabFaxMailEDIList";
            this.tabFaxMailEDIList.Size = new System.Drawing.Size(1049, 255);
            this.tabFaxMailEDIList.Text = "Fax/Mail/EDI";
            // 
            // CommunicationPartWorkspace
            // 
            this.CommunicationPartWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.CommunicationPartWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CommunicationPartWorkspace.Location = new System.Drawing.Point(0, 0);
            this.CommunicationPartWorkspace.Name = "CommunicationPartWorkspace";
            this.CommunicationPartWorkspace.Size = new System.Drawing.Size(1049, 255);
            this.CommunicationPartWorkspace.TabIndex = 1;
            this.CommunicationPartWorkspace.Text = "CommunicationPartWorkspace";
            // 
            // tabDocumentList
            // 
            this.tabDocumentList.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabDocumentList.Appearance.PageClient.Options.UseBackColor = true;
            this.tabDocumentList.Controls.Add(this.DocumentListWorkspace);
            this.tabDocumentList.Name = "tabDocumentList";
            this.tabDocumentList.Size = new System.Drawing.Size(1049, 255);
            this.tabDocumentList.Text = "Document List";
            // 
            // DocumentListWorkspace
            // 
            this.DocumentListWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.DocumentListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.DocumentListWorkspace.Name = "DocumentListWorkspace";
            this.DocumentListWorkspace.Size = new System.Drawing.Size(1049, 255);
            this.DocumentListWorkspace.TabIndex = 2;
            this.DocumentListWorkspace.Text = "deckWorkspace1";
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
            this.dpSearch.OriginalSize = new System.Drawing.Size(260, 200);
            this.dpSearch.Size = new System.Drawing.Size(260, 654);
            this.dpSearch.Text = "Search";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.SearchPartWorkspace);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(254, 626);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // SearchPartWorkspace
            // 
            this.SearchPartWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchPartWorkspace.Location = new System.Drawing.Point(0, 0);
            this.SearchPartWorkspace.Name = "SearchPartWorkspace";
            this.SearchPartWorkspace.Size = new System.Drawing.Size(254, 626);
            this.SearchPartWorkspace.TabIndex = 0;
            this.SearchPartWorkspace.Text = "SearchPartWorkspace";
            // 
            // ToolBarPartWorkspace
            // 
            this.ToolBarPartWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolBarPartWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ToolBarPartWorkspace.Name = "ToolBarPartWorkspace";
            this.ToolBarPartWorkspace.Size = new System.Drawing.Size(1056, 27);
            this.ToolBarPartWorkspace.TabIndex = 3;
            this.ToolBarPartWorkspace.Text = "ToolBarPartWorkspace";
            // 
            // QuotedPriceMainWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ToolBarPartWorkspace);
            this.Name = "QuotedPriceMainWorkspace";
            this.Size = new System.Drawing.Size(1056, 681);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager2)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtab)).EndInit();
            this.xtab.ResumeLayout(false);
            this.tabMemoList.ResumeLayout(false);
            this.tabFaxMailEDIList.ResumeLayout(false);
            this.tabDocumentList.ResumeLayout(false);
            this.dpSearch.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager2;
        private Framework.ClientComponents.UIFramework.LayoutContainerControl panel1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListPartWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace FastSearchPartWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SearchPartWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ToolBarPartWorkspace;
        private DevExpress.XtraTab.XtraTabControl xtab;
        private DevExpress.XtraTab.XtraTabPage tabMemoList;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace RatesListPartWorkspace;
        private DevExpress.XtraTab.XtraTabPage tabFaxMailEDIList;
        private DevExpress.XtraTab.XtraTabPage tabDocumentList;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace DocumentListWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace CommunicationPartWorkspace;



    }
}