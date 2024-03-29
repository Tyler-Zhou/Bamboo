﻿namespace ICP.FCM.OceanImport.UI
{
    partial class OIOrderMainWorkSpace
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.FastSearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.xtab = new DevExpress.XtraTab.XtraTabControl();
            this.tabMemoList = new DevExpress.XtraTab.XtraTabPage();
            this.MemoListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabFaxMailList = new DevExpress.XtraTab.XtraTabPage();
            this.FaxMailEDIListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabDocumentList = new DevExpress.XtraTab.XtraTabPage();
            this.DocumentListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.SearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtab)).BeginInit();
            this.xtab.SuspendLayout();
            this.tabMemoList.SuspendLayout();
            this.tabFaxMailList.SuspendLayout();
            this.tabDocumentList.SuspendLayout();
            this.dpSearch.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolbarWorkspace
            // 
            this.ToolbarWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolbarWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ToolbarWorkspace.Name = "ToolbarWorkspace";
            this.ToolbarWorkspace.Size = new System.Drawing.Size(913, 27);
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
            this.panel1.Controls.Add(this.splitContainerControl1);
            this.panel1.Controls.Add(this.dpSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(913, 673);
            this.panel1.TabIndex = 2;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(230, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.ListWorkspace);
            this.splitContainerControl1.Panel1.Controls.Add(this.FastSearchWorkspace);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.xtab);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(683, 673);
            this.splitContainerControl1.SplitterPosition = 434;
            this.splitContainerControl1.TabIndex = 4;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ListWorkspace
            // 
            this.ListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListWorkspace.Location = new System.Drawing.Point(0, 36);
            this.ListWorkspace.Name = "ListWorkspace";
            this.ListWorkspace.Size = new System.Drawing.Size(683, 398);
            this.ListWorkspace.TabIndex = 3;
            this.ListWorkspace.Text = "deckWorkspace4";
            // 
            // FastSearchWorkspace
            // 
            this.FastSearchWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.FastSearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.FastSearchWorkspace.Name = "FastSearchWorkspace";
            this.FastSearchWorkspace.Size = new System.Drawing.Size(683, 36);
            this.FastSearchWorkspace.TabIndex = 2;
            this.FastSearchWorkspace.Text = "deckWorkspace3";
            // 
            // xtab
            // 
            this.xtab.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.xtab.Appearance.Options.UseBackColor = true;
            this.xtab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtab.Location = new System.Drawing.Point(0, 0);
            this.xtab.Name = "xtab";
            this.xtab.SelectedTabPage = this.tabMemoList;
            this.xtab.Size = new System.Drawing.Size(683, 233);
            this.xtab.TabIndex = 2;
            this.xtab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabMemoList,
            this.tabFaxMailList,
            this.tabDocumentList});
            // 
            // tabMemoList
            // 
            this.tabMemoList.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabMemoList.Appearance.PageClient.Options.UseBackColor = true;
            this.tabMemoList.Controls.Add(this.MemoListWorkspace);
            this.tabMemoList.Name = "tabMemoList";
            this.tabMemoList.Size = new System.Drawing.Size(676, 203);
            this.tabMemoList.Text = "Memo";
            // 
            // MemoListWorkspace
            // 
            this.MemoListWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.MemoListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MemoListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.MemoListWorkspace.Name = "MemoListWorkspace";
            this.MemoListWorkspace.Size = new System.Drawing.Size(676, 203);
            this.MemoListWorkspace.TabIndex = 1;
            this.MemoListWorkspace.Text = "MemoListWorkspace";
            // 
            // tabFaxMailList
            // 
            this.tabFaxMailList.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabFaxMailList.Appearance.PageClient.Options.UseBackColor = true;
            this.tabFaxMailList.Controls.Add(this.FaxMailEDIListWorkspace);
            this.tabFaxMailList.Name = "tabFaxMailList";
            this.tabFaxMailList.Size = new System.Drawing.Size(676, 203);
            this.tabFaxMailList.Text = "Fax/Mail/EDI";
            // 
            // FaxMailEDIListWorkspace
            // 
            this.FaxMailEDIListWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.FaxMailEDIListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FaxMailEDIListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.FaxMailEDIListWorkspace.Name = "FaxMailEDIListWorkspace";
            this.FaxMailEDIListWorkspace.Size = new System.Drawing.Size(676, 203);
            this.FaxMailEDIListWorkspace.TabIndex = 1;
            this.FaxMailEDIListWorkspace.Text = "FaxMailEDIListWorkspace";
            // 
            // tabDocumentList
            // 
            this.tabDocumentList.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabDocumentList.Appearance.PageClient.Options.UseBackColor = true;
            this.tabDocumentList.Controls.Add(this.DocumentListWorkspace);
            this.tabDocumentList.Name = "tabDocumentList";
            this.tabDocumentList.Size = new System.Drawing.Size(676, 203);
            this.tabDocumentList.Text = "Document List";
            // 
            // DocumentListWorkspace
            // 
            this.DocumentListWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.DocumentListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.DocumentListWorkspace.Name = "DocumentListWorkspace";
            this.DocumentListWorkspace.Size = new System.Drawing.Size(676, 203);
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
            this.dpSearch.OriginalSize = new System.Drawing.Size(230, 200);
            this.dpSearch.Size = new System.Drawing.Size(230, 673);
            this.dpSearch.Text = "Search";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.SearchWorkspace);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(224, 645);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // SearchWorkspace
            // 
            this.SearchWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.SearchWorkspace.Name = "SearchWorkspace";
            this.SearchWorkspace.Size = new System.Drawing.Size(224, 645);
            this.SearchWorkspace.TabIndex = 0;
            this.SearchWorkspace.Text = "deckWorkspace1";
            // 
            // OIOrderMainWorkSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ToolbarWorkspace);
            this.Name = "OIOrderMainWorkSpace";
            this.Size = new System.Drawing.Size(913, 700);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtab)).EndInit();
            this.xtab.ResumeLayout(false);
            this.tabMemoList.ResumeLayout(false);
            this.tabFaxMailList.ResumeLayout(false);
            this.tabDocumentList.ResumeLayout(false);
            this.dpSearch.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ToolbarWorkspace;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl panel1;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SearchWorkspace;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace FastSearchWorkspace;
        private DevExpress.XtraTab.XtraTabControl xtab;
        private DevExpress.XtraTab.XtraTabPage tabMemoList;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace MemoListWorkspace;
        private DevExpress.XtraTab.XtraTabPage tabFaxMailList;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace FaxMailEDIListWorkspace;
        private DevExpress.XtraTab.XtraTabPage tabDocumentList;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace DocumentListWorkspace;
    }
}
